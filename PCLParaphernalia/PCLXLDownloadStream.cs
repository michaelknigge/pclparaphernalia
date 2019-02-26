using System;
using System.IO;
using System.Windows;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class handles PCL XL downloadable user stream.
    /// 
    /// © Chris Hutchinson 2012
    /// 
    /// </summary>

    static class PCLXLDownloadStream
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
        // c h e c k F o r S t r e a m N a m e                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Read PCL XL BeginStream operator, etc.                             //
        //                                                                    //
        // Analysis is minimal; we expect to see:                             //
        //                                                                    //
        // Attribute StreamName        represented by ubyte_array of variable //
        //                             length.                                //
        // Operator  BeginStream                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static Boolean checkForStreamName(String     fileName,
                                                  Int64      fileSize,
                                                  ref String streamName)
        {
            const UInt16 minFileSize = 128; // enough to read initial operators
            const Byte minStreamNameLen = 1;
            const Byte maxStreamNameLen = 80;

            Boolean OK = true;
            Boolean beginFound = false;

            Int32 dataLen,
                  pos;
            
            Byte[] buf = new Byte[minFileSize];

            _ipStream.Seek (0, SeekOrigin.Begin);

            if (fileSize < minFileSize)
                OK = false;
            else
                _binReader.Read (buf, 0, minFileSize);

            pos = 0;

            while (OK && !beginFound)
            {
                if (buf[pos] == (Byte) PCLXLDataTypes.eTag.UbyteArray)
                {
                    // start of ubyte array for StreamName attribute;

                    if (buf[pos + 1] == (Byte) PCLXLDataTypes.eTag.Uint16)
                    {
                        dataLen = (buf[pos + 3] * 256) + buf[pos + 2];
                        pos += 4;
                    }
                    else if (buf[pos + 1] == (Byte) PCLXLDataTypes.eTag.Ubyte)
                    {
                        dataLen = buf[pos + 2];
                        pos += 3;
                    }
                    else
                        dataLen = 0;

                    if ((dataLen < minStreamNameLen) || (dataLen > maxStreamNameLen))
                    {
                        OK = false;
                    }
                    else
                    {
                        char[] streamNameArray = new char[dataLen];

                        for (Int32 i = 0; i < dataLen; i++)
                        {
                            streamNameArray[i] = (char) buf[pos + i];
                        }

                        streamName = new String (streamNameArray);

                        pos += dataLen;
                        pos += 2;
                    }
                }
                else if (buf[pos] == (Byte) PCLXLOperators.eTag.BeginStream)
                {
                    beginFound = true;
                    pos += 1;
                }
                else
                {
                    OK = false;
                }
            }

            if (beginFound)
                return true;
            else
                return false;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h e c k S t r e a m F i l e                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Check macro file to see if it starts with a BeginStream operator   //
        // and associated attribute list; if so, return the stream name.      //
        //                                                                    //
        // TODO perhaps we ought to check that ReadStream and EndStream       //
        //      operators are also present?                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean checkStreamFile(String     filename,
                                              ref String streamName)
        {
            Boolean fileOpen = false;
            Boolean streamNamePresent = false;

            Int64 fileSize = 0;

            //----------------------------------------------------------------//
            //                                                                //
            // Read file to obtain characteristics.                           //
            //                                                                //
            //----------------------------------------------------------------//

            fileOpen = streamFileOpen (filename, ref fileSize);

            if (!fileOpen)
            {
                streamNamePresent = false;
            }
            else
            {
                streamNamePresent = checkForStreamName (filename, fileSize,
                                                        ref streamName);

                streamFileClose ();
            }

            return streamNamePresent;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s t r e a m F i l e C l o s e                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Close input (reader) stream and file.                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void streamFileClose()
        {
            _binReader.Close();

            try
            {
                _ipStream.Close();
            }

            catch (IOException e)
            {
                MessageBox.Show ("IO Exception:\r\n" +
                                 e.Message + "\r\n" +
                                 "Closing stream/file",
                                 "PCL XL user stream analysis",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
            }

        }         

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s t r e a m F i l e E m b e d                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Embed user-defined stream in output stream.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean streamFileEmbed(BinaryWriter prnWriter,
                                              String       filename,
                                              String       streamName,
                                              Boolean      encapsulated)
        {
            Boolean fileOpen = false;

            Boolean OK = true;

            Int64 fileSize = 0;

            fileOpen = streamFileOpen (filename, ref fileSize);

            if (!fileOpen)
            {
                OK = false;
            }
            else
            {
                const Int32 bufSize = 2048;
                Int32 readSize;

                Boolean endLoop;
                                
                Byte[] buffer = new Byte[bufSize];
                
                endLoop = false;

                if (! encapsulated)
                    PCLXLWriter.streamBegin (prnWriter, streamName);

                while (!endLoop)
                {
                    readSize = _binReader.Read(buffer, 0, bufSize);

                    if (readSize == 0)
                        endLoop = true;
                    else
                        PCLXLWriter.writeStreamBlock (prnWriter,
                                                      ! encapsulated,
                                                      buffer, ref readSize);
                }

                if (! encapsulated)
                    PCLXLWriter.streamEnd (prnWriter);

                streamFileClose ();
            }

            return OK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s t r e a m F i l e O p e n                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Open user-defined stream file, input stream and reader.            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static Boolean streamFileOpen(String fileName,
                                              ref Int64 fileSize)
        {
            Boolean open = false;


            if ((fileName == null) || (fileName == ""))
            {
                MessageBox.Show ("Download stream file name is null.",
                                "PCL XL stream invalid",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);

                return false;
            }
            else if (!File.Exists (fileName))
            {
                MessageBox.Show ("Download stream file '" + fileName +
                                "' does not exist.",
                                "PCL XL stream invalid",
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
                                     "Opening file '" +
                                     fileName + "'",
                                     "PCL XL user stream analysis",
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
    }
}