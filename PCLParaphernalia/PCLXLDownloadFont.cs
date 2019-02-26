using System;
using System.IO;
using System.Windows;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class handles PCL XL downloadable soft fonts.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class PCLXLDownloadFont
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private const Int32 _minHddrDescLen = 8;

        private const UInt16 _defaultPCLDotRes = 600;

        private enum ePCLXLFontTechnology : byte
        {
            TrueType           = 1,
            Bitmap             = 254,
        }

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static Stream _ipStream = null;
        private static BinaryReader _binReader = null;

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // f o n t F i l e C l o s e                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Close stream and file.                                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void fontFileClose()
        {
            _binReader.Close();
            _ipStream.Close();
        }         

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // f o n t F i l e C o p y                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Copy font file contents to output stream.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean fontFileCopy(BinaryWriter prnWriter,
                                           String       fontFilename)
        {
            Boolean fileOpen = false;

            Boolean OK = true;

            Int64 fileSize = 0;

            fileOpen = fontFileOpen (fontFilename, ref fileSize);

            if (!fileOpen)
            {
                OK = false;
            }
            else
            {
                const Int32 bufSize = 2048;
                Int32 readSize;

                Boolean endLoop;
                                
                Byte[] buf = new Byte[bufSize];
                
                endLoop = false;

                while (!endLoop)
                {
                    readSize = _binReader.Read(buf, 0, bufSize);

                    if (readSize == 0)
                        endLoop = true;
                    else
                       prnWriter.Write(buf, 0, readSize);
                }

                fontFileClose ();
            }

            return OK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // f o n t F i l e O p e n                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Open soft font file, stream and reader.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static Boolean fontFileOpen(String fileName,
                                            ref Int64 fileSize)
        {
            Boolean open = false;


            if ((fileName == null) || (fileName == ""))
            {
                MessageBox.Show ("Download font file name is null.",
                                "PCL XL font selection attribute invalid",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);

                return false;
            }
            else if (!File.Exists (fileName))
            {
                MessageBox.Show ("Download font file '" + fileName +
                                "' does not exist.",
                                "PCL XL font selection attribute invalid",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);

                return false;
            }
            else
            {
                try
                {
                    _ipStream = File.Open (fileName,
                                           FileMode.Open,
                                           FileAccess.Read,
                                           FileShare.None);
                }

                catch (IOException e)
                {
                    MessageBox.Show ("IO Exception:\r\n" +
                                     e.Message + "\r\n" +
                                     "Opening soft font file '" +
                                     fileName + "'",
                                     "PCL XL soft font analysis",
                                     MessageBoxButton.OK,
                                     MessageBoxImage.Error);
                }

                if (_ipStream != null)
                {
                    open = true;

                    FileInfo fi = new FileInfo (fileName);

                    fileSize = fi.Length;

                    _binReader = new BinaryReader (_ipStream);
                }
            }

            return open;
        }         

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t F o n t C h a r a c t e r i s t i c s                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Validate font header and return font characteristics.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean getFontCharacteristics(String      fontFilename,
                                                     ref String  fontName,
                                                     ref Boolean scalable,
                                                     ref Boolean bound,
                                                     ref UInt16  symSetNo)
        {
            Boolean fileOpen = false;

            Boolean OK = true;

            UInt16 hddrOffset  = 0;
            Int64  fileSize    = 0;

            //----------------------------------------------------------------//
            //                                                                //
            // Read file to obtain characteristics.                           //
            //                                                                //
            //----------------------------------------------------------------//

            fileOpen = fontFileOpen (fontFilename, ref fileSize);

            if (! fileOpen)
            {
                OK = false;
            }
            else
            {
                OK = readHddrIntro (fontFilename,
                                    fileSize,
                                    ref fontName,
                                    ref hddrOffset);

                if (OK)
                {
                    OK = readHddrDescriptor (hddrOffset,
                                             ref scalable,
                                             ref bound,
                                             ref symSetNo);
                }

                fontFileClose ();
            }

            return OK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e a d H d d r D e s c r i p t o r                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Read PCL XL soft font descriptor.                                  //
        // We do minimal validation.                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static Boolean readHddrDescriptor(UInt16      hddrOffset,
                                                  ref Boolean scalable,
                                                  ref Boolean bound,
                                                  ref UInt16  symSetNo)
        {
            const UInt16 symSetUnicode = 590;
            
            const Byte techTrueType = 1;
        //  const Byte techBitmap   = 254;

            Boolean OK = true;

            Byte [] hddr = new Byte[_minHddrDescLen];

            Byte technology;

            _ipStream.Seek(hddrOffset, SeekOrigin.Begin);

            _binReader.Read(hddr, 0, _minHddrDescLen);

            //----------------------------------------------------------------//

            symSetNo = (UInt16)((hddr[2] * 256) + hddr[3]);
            
            if (symSetNo == symSetUnicode)
                bound = false;
            else
                bound = true;

            //----------------------------------------------------------------//
            
            technology = hddr[4];

            if (technology == techTrueType)
                scalable = true;
            else
                scalable = false;

            return OK;
        }         

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e a d H d d r I n t r o                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Read PCL XL BeginFontHeader and (initial) ReadFontHeader           //
        // operators, etc.                                                    //
        //                                                                    //
        // Analysis is minimal; we expect to see:                             //
        //                                                                    //
        // Attribute FontName          represented by ubyte_array of (fixed)  //
        //                             length 16.                             //
        // Attribute FontFormat        represented by ubyte value of zero.    //
        // Operator  BeginFontHeader   attributes may appear in either order. //
        //                                                                    //
        // Attribute FontHeaderLength  represented by uint16 value.           //
        // Operator  ReadFontHeader    first or only such operator, followed  //
        //                             by 'embedded data', which will be      //
        //                             introduced by one of:                  //
        //     embedded_data_byte tag  followed by ubyte length value         //
        //  OR embedded_data tag       followed by uint32 length value        //
        //                                                                    //
        // Then the specified (length) number of font header bytes; we need   //
        // a minimum of 8 bytes for the standard format-0 font header.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static Boolean readHddrIntro(String     fileName,
                                             Int64      fileSize,
                                             ref String fontName,
                                             ref UInt16 hddrOffset)
        {
            const UInt16 minFileSize = 64; // enough to read initial operators
            const Byte minFontNameLen = 12;
            const Byte maxFontNameLen = 20;

            String messHeader  = "Download font file '" + fileName + "':\n\n";
            String messTrailer = "\n\nYou will have to choose another file.";
                
            Boolean OK = true;
            Boolean beginFound = false;
            
            Int32 hddrDescLen = 0,
                  dataLen,
                  pos;

            if (fileSize < minFileSize)
            {
                MessageBox.Show(messHeader +
                                "File size < minimum (" + minFileSize +
                                " bytes)."                            +
                                messTrailer,
                                "PCL XL soft font file",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return false;
            }

            Byte[] buf = new Byte[minFileSize];

            _ipStream.Seek(0, SeekOrigin.Begin);

            _binReader.Read(buf, 0, minFileSize);

            pos = 0;

            while (OK && !beginFound)
            {
                if (buf[pos] == (Byte)PCLXLDataTypes.eTag.UbyteArray)
                {
                    // start of ubyte array for FontName attribute;

                    if (buf[pos + 1] == (Byte)PCLXLDataTypes.eTag.Uint16)
                    {
                        dataLen = (buf[pos + 3] * 256) + buf[pos + 2];
                        pos += 4;
                    }
                    else if (buf[pos + 1] == (Byte)PCLXLDataTypes.eTag.Ubyte)
                    {
                        dataLen = buf[pos + 2];
                        pos += 3;
                    }
                    else
                        dataLen = 0;

                    if ((dataLen < minFontNameLen) || (dataLen > maxFontNameLen))
                    {
                        OK = false;
                    }
                    else
                    {
                        char[] fontNameArray = new char[dataLen];

                        for (Int32 i = 0; i < dataLen; i++)
                        {
                            fontNameArray[i] = (char)buf[pos + i];
                        }

                        fontName = new String(fontNameArray);

                        pos += dataLen;

                        if ((buf[pos] != (Byte)PCLXLAttrDefiners.eTag.Ubyte) ||
                            (buf[pos + 1] != (Byte)PCLXLAttributes.eTag.FontName))
                            OK = false;
                        else
                            pos += 2;
                    }
                }
                else if (buf[pos] == (Byte)PCLXLDataTypes.eTag.Ubyte)
                {
                    // start of FontFormat attribute.

                    if ((buf[pos + 1] != 0) ||
                        (buf[pos + 2] != (Byte)PCLXLAttrDefiners.eTag.Ubyte) ||
                        (buf[pos + 3] != (Byte)PCLXLAttributes.eTag.FontFormat))
                    {
                        OK = false;
                    }
                    else
                        pos += 4;
                }
                else if (buf[pos] == (Byte)PCLXLOperators.eTag.BeginFontHeader)
                {
                    beginFound = true;
                    pos += 1;
                }
                else
                {
                    OK = false;
                }
            }

            if (OK)
            {
                if (buf[pos] == (Byte)PCLXLDataTypes.eTag.Uint16)
                {
                    dataLen = (UInt16)((buf[pos + 2] * 256) + buf[pos + 1]);

                    if ((dataLen < _minHddrDescLen)                           ||
                        (buf[pos+3] != (Byte)PCLXLAttrDefiners.eTag.Ubyte)            ||
                        (buf[pos+4] != 
                            (Byte)PCLXLAttributes.eTag.FontHeaderLength)          ||
                        (buf[pos+5] != (Byte)PCLXLOperators.eTag.ReadFontHeader))
                    {
                        OK = false;
                    }
                    else
                    {
                        pos += 6;

                        if (buf[pos] == (Byte)PCLXLEmbedDataDefs.eTag.Byte)
                        {
                            hddrDescLen = buf[pos + 1];

                            pos += 2;
                        }
                        else if (buf[pos] == (Byte) PCLXLEmbedDataDefs.eTag.Int)
                        {
                            hddrDescLen = (buf[pos + 4] * 256 * 256 * 256) +
                                          (buf[pos + 5] * 256 * 256) +
                                          (buf[pos + 6] * 256) +
                                          (buf[pos + 7]);

                            pos += 5;
                       
                            if (hddrDescLen < _minHddrDescLen)
                                OK = false;
                        }
                    }
                }
                else
                {
                    OK = false;
                }
            }

            if (OK)
            {
                hddrOffset = (UInt16)pos;
                
                return true;
            }
            else
            {
                MessageBox.Show(messHeader + 
                                "Font file format not recognised." +
                                 messTrailer,
                                "PCL XL soft font file",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return false;
            }   
        }
    }
}