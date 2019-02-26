using System;
using System.IO;
using System.Windows;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class handles PCL (download) macros.
    /// 
    /// © Chris Hutchinson 2012
    /// 
    /// </summary>

    static class PCLDownloadMacro
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

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h e c k F o r M a c r o I d                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Check whether file starts with 'macro Id' escape sequence.         //
        // Format should be "<esc>&f#y" or "<esc>&f#Y".                       //
        // If it does, extract and return identifier value.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static Boolean checkForMacroId(String    fileName,
                                               Int64     fileSize,
                                               ref Int32 macroId)
        {
            const Int32 prefixLen = 3;

            Boolean flagOK = true;

            Int32 offset = 0;
            Int32 value = 0;

            Byte[] buf = new Byte[prefixLen];

            _binReader.Read (buf, 0, prefixLen);

            if ((buf[0] != '\x1b') ||
                (buf[1] != '&') ||
                (buf[2] != 'f'))
            {
                flagOK = false;
            }
            else
            {
                const Int32 maxRead = 12;
                Boolean foundTerm = false;

                Int32 pos,
                      maxPos;

                Byte x;

                offset += prefixLen;

                maxPos = offset + maxRead;

                if (fileSize <= maxPos)
                    maxPos = (Int32)(fileSize - 1);

                for (pos = offset;
                     (flagOK) && (!foundTerm) && (pos < maxPos);
                     pos++)
                {
                    x = _binReader.ReadByte ();

                    if (x == 'y')
                        foundTerm = true;
                    else if (x == 'Y')
                        foundTerm = true;
                    else if (x < '\x30')
                        flagOK = false;
                    else if (x > '\x39')
                        flagOK = false;
                    else
                        value = ((value * 10) + (x - '\x30'));
                }

                if (foundTerm)
                {
                    macroId = value;
                }
                else
                {
                    flagOK = false;
                }
            }

            return flagOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h e c k M a c r o F i l e                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Check macro file to see if it starts with a 'macro identifier'     //
        // sequence; if so, return the identifier.                            //
        //                                                                    //
        // TODO perhaps we ought to check that macro start and stop are also  //
        //      present?                                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean checkMacroFile(String    filename,
                                             ref Int32 macroId)
        {
            Boolean fileOpen = false;
            Boolean macroIdPresent = false;

            Int64  fileSize = 0;

            //----------------------------------------------------------------//
            //                                                                //
            // Read file to obtain characteristics.                           //
            //                                                                //
            //----------------------------------------------------------------//

            fileOpen = macroFileOpen (filename, ref fileSize);

            if (!fileOpen)
            {
                macroIdPresent = false;
            }
            else
            {
                macroIdPresent = checkForMacroId (filename,
                                                  fileSize,
                                                  ref macroId);

                macroFileClose ();
            }

            return macroIdPresent;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m a c r o F i l e C l o s e                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Close stream and file.                                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void macroFileClose()
        {
            _binReader.Close ();
            _ipStream.Close ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m a c r o F i l e C o p y                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Copy macro file contents to output stream.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean macroFileCopy(BinaryWriter prnWriter,
                                            String filename)
        {
            Boolean OK = true;

            Boolean fileOpen = false;

            Int64 fileSize = 0;

            fileOpen = macroFileOpen (filename, ref fileSize);

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
                    readSize = _binReader.Read (buf, 0, bufSize);

                    if (readSize == 0)
                        endLoop = true;
                    else
                        prnWriter.Write (buf, 0, readSize);
                }

                macroFileClose ();
            }

            return OK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m a c r o F i l e O p e n                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Open macro file, stream and reader.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static Boolean macroFileOpen(String fileName,
                                             ref Int64 fileSize)
        {
            Boolean open = false;

            if ((fileName == null) || (fileName == ""))
            {
                MessageBox.Show ("Download macro file name is null.",
                                "PCL macro file name invalid",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);

                return false;
            }
            else if (!File.Exists (fileName))
            {
                MessageBox.Show ("Download macro file '" + fileName +
                                "' does not exist.",
                                "PCL macro file name invalid",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);

                return false;
            }
            else
            {
                _ipStream = File.Open (fileName,
                                      FileMode.Open,
                                      FileAccess.Read,
                                      FileShare.None);

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
    }
}