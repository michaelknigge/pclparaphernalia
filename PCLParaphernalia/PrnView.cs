using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class handles viewing of content of (print) file.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>
    
    [System.Reflection.ObfuscationAttribute(Feature = "properties renaming")]

    class PrnView
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

      
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static Stream _ipStream = null;
        private static BinaryReader _binReader = null;

        private static Int64 _fileSize = 0;

        private Boolean _splitSlices = false;

        PrnParseConstants.eOptCharSetSubActs _indxCharSetSubAct = 0;
        PrnParseConstants.eOptCharSets _indxCharSetName = 0;
        Int32 _valCharSetSubCode = 0;
        
        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P r n V i e w                                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PrnView()
        {
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // a d d R o w                                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Adds a row to the output grid.                                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void addRow(DataTable table,
                            String offset,
                            String hexVal,
                            String textVal)
        {
            const Int32 colOffset = 0;
            const Int32 colHex = 1;
            const Int32 colText = 2;
            
            DataRow row;

            //  Int32 rowNo = _fixedRows + _rowCt;

            row = table.NewRow ();

            row[colOffset] = offset;
            row[colHex]    = hexVal;
            row[colText]   = textVal;

            table.Rows.Add (row);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c l o s e I n p u t P r n                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Close stream and file.                                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void closeInputPrn()
        {
            _binReader.Close ();
            _ipStream.Close ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // o p e n I n p u t P r n                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Open read stream for specified print file.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean openInputPrn(String    filename,
                                     ref Int64 fileSize)
        {
            Boolean open = false;

            if ((filename == null) || (filename == ""))
            {
                MessageBox.Show ("Print file name is null.",
                                "Print file selection",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);

                return false;
            }
            else if (!File.Exists (filename))
            {
                MessageBox.Show ("Print file '" + filename +
                                "' does not exist.",
                                "Print file selection",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);

                return false;
            }
            else
            {
                try
                {
                    _ipStream = File.Open (filename,
                                           FileMode.Open,
                                           FileAccess.Read,
                                           FileShare.None);
                }

                catch (IOException e)
                {
                    MessageBox.Show ("IO Exception:\r\n" +
                                     e.Message + "\r\n" +
                                     "Opening file '" +
                                     filename + "'",
                                     "Print file content",
                                     MessageBoxButton.OK,
                                     MessageBoxImage.Error);
                }

                if (_ipStream != null)
                {
                    FileInfo fi = new FileInfo (filename);

                    fileSize = fi.Length;

                    open = true;

                    _binReader = new BinaryReader (_ipStream);
                }
            }

            return open;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // v i e w F i l e                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Open print file and show content.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean viewFile(String prnFilename,
                                PrnParseOptions options,
                                DataTable table)
        {
            Boolean OK = true;

            Boolean ipOpen = false;

            ipOpen = openInputPrn (prnFilename, ref _fileSize);

            if (!ipOpen)
            {
                OK = false;
            }
            else
            {
                viewFileAction (options, table);

                closeInputPrn ();
            }

            return OK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // v i e w F i l e A c t i o n                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // View file contents.                                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void viewFileAction(PrnParseOptions options,
                                    DataTable table)
        {
            Int32 blockLen,
                  sliceLen,
                  blockStart = 0;

            Int32 offsetStart = 0,
                  offsetEnd = -1,
                  offsetCrnt;

            String offsetFormat;
            String offsetStr;

            Boolean rowLimitReached = false;
            Boolean endReached = false;

            Byte[] buf = new Byte[PrnParseConstants.bufSize];

            //----------------------------------------------------------------//

            if (options.IndxGenOffsetFormat ==
                PrnParseConstants.eOptOffsetFormats.Hexadecimal)
                offsetFormat = "{0:x8}";
            else
                offsetFormat = "{0:d10}";

            options.getOptCharSet (ref _indxCharSetName,
                                   ref _indxCharSetSubAct,
                                   ref _valCharSetSubCode);

            //----------------------------------------------------------------//
            //                                                                //
            // Check for start conditions specific to current file.           //
            //                                                                //
            //----------------------------------------------------------------//

            options.getOptCurFOffsets (ref offsetStart,
                                       ref offsetEnd);

            blockStart = offsetStart;
            _ipStream.Seek (offsetStart, SeekOrigin.Begin);

            if (offsetStart != 0)
                addRow (table,
                        "Comment",
                        "Start Offset   = " + offsetStart +
                        " (0x" + offsetStart.ToString ("X8") +
                        ") requested",
                        "");

            if (offsetEnd != -1)
                addRow (table,
                        "Comment",
                        "End   Offset   = " + offsetEnd +
                        " (0x" + offsetEnd.ToString ("X8") +
                        ") requested",
                        "");

            //----------------------------------------------------------------//

            while (!endReached && !rowLimitReached)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Read next 'block' of file.                                 //
                // If end-of-file detected, block will be less than full.     //
                //                                                            //
                //------------------------------------------------------------//

                blockLen = _binReader.Read (buf, 0, PrnParseConstants.bufSize);

                if (blockLen == 0)
                {
                    endReached = true;
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Split the current 'block' into 'slices'.               //
                    // Each 'slice' will provide one line of the output       //
                    // display; the last 'slice' may be less than a full line.//
                    // Other slices may also be less than a full line if the  //
                    // option to split slices when LineFeed or FormFeed       //
                    // characters are encountered is in force.                //
                    //                                                        //
                    //--------------------------------------------------------//

                    sliceLen = PrnParseConstants.viewBytesPerLine;

                    for (int i = 0;
                         ((i < blockLen) && (!endReached));
                         i += sliceLen)
                    {
                        if ((i + PrnParseConstants.viewBytesPerLine) > blockLen)
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Last slice of data is less than full.          //
                            //                                                //
                            //------------------------------------------------//

                            sliceLen = blockLen - i;
                        }
                        else
                        {
                            sliceLen = PrnParseConstants.viewBytesPerLine;
                        }

                        //----------------------------------------------------//
                        //                                                    //
                        // Extract required details from current slice.       //
                        //                                                    //
                        //----------------------------------------------------//
                        
                        offsetCrnt = blockStart + i;

                        offsetStr  = String.Format (offsetFormat, offsetCrnt);

                        sliceLen = viewFileSlice (buf,
                                                  offsetStr,
                                                  i,
                                                  sliceLen,
                                                  table);

                        if ((offsetEnd != -1) && (offsetCrnt > offsetEnd))
                            endReached = true;
                    }

                    //--------------------------------------------------------//
                    //                                                        //
                    // Increment 'block' offset value.                        //
                    //                                                        //
                    //--------------------------------------------------------//

                    blockStart = blockStart + blockLen;

                    if ((offsetEnd != -1) && (blockStart > offsetEnd))
                        endReached = true;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // f o r m a t S l i c e                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Format a 'slice' of the input PCL file contents in hexadecimal &   //
        // character formats, prefixed with the offset value.                 //
        //                                                                    //
        // The 'slice' is normally a fixed number of characters, but if the   //
        // option to split slices when LineFeed or FormFeed characters are    //
        // encountered is in force, the slice may be less; the actual length  //
        // processed is returned.                                             //
        //                                                                    //
        // 'Unprintable' characters are shown as '.'.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Int32 viewFileSlice(Byte []   buf,
                                    String    crntOffset,
                                    Int32     blockOffset,
                                    Int32     sliceMax,
            //                      Int32     rowNo,
                                    DataTable table)
        {
            Int32 sliceLen;

            Boolean endSlice;

            Byte crntByte;

            Char cx;

            Int32 sub;

            StringBuilder hexBuf = new StringBuilder ();
            StringBuilder strBuf = new StringBuilder ();

            sliceLen = sliceMax;

            //----------------------------------------------------------------//
            //                                                                //
            // Construct hexadecimal and text representations of slice.       //
            //                                                                //
            //----------------------------------------------------------------//

            endSlice = false;

            for (int j = blockOffset;
                    (j < (blockOffset + sliceLen) && (!endSlice));
                    j++)
            {
                crntByte = buf[j];

                if (((crntByte < 32) || (crntByte == 0x7f)) ||
                    ((_indxCharSetName ==
                        PrnParseConstants.eOptCharSets.ASCII)
                                           &&
                     (crntByte >= 0x80)) ||
                    ((_indxCharSetName ==
                        PrnParseConstants.eOptCharSets.ISO_8859_1)
                                           &&
                     (crntByte >= 0x80) && (crntByte <= 0x9f)))
                {
                    switch (_indxCharSetSubAct)
                    {
                        case PrnParseConstants.eOptCharSetSubActs.Spaces:

                            strBuf.Append (' ');
                            break;

                        case PrnParseConstants.eOptCharSetSubActs.Substitute:

                            strBuf.Append ((Char) _valCharSetSubCode);
                            break;

                        default:

                            strBuf.Append ('.');
                            break;
                    }
                }
                else
                {
                    strBuf.Append ((Char) crntByte);
                }

                sub = crntByte;
                sub = sub >> 4;

                cx = PrnParseConstants.cHexChars[sub];

                hexBuf.Append (cx);

                sub = (crntByte & 0x0f);

                cx = PrnParseConstants.cHexChars[sub];

                hexBuf.Append (cx);
                hexBuf.Append (' ');

                //------------------------------------------------------------//
                //                                                            //
                // Terminate the slice if a LineFeed or FormFeed character    //
                // has been encountered and the option to split slices has    //
                // been selected.                                             //
                //                                                            //
                //------------------------------------------------------------//

                if (_splitSlices && ((crntByte == 0x0a) || (crntByte == 0x0c)))
                {
                    endSlice = true;
                    sliceLen = j - blockOffset + 1;
                }
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Add row to table and return count of characters processed.     //
            //                                                                //
            //----------------------------------------------------------------//

            addRow (table, crntOffset, hexBuf.ToString (), strBuf.ToString ());

            return sliceLen;
        }
    }
}