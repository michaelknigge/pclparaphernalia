using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides PCL handling for the Symbol Set Generate tool.
    /// 
    /// © Chris Hutchinson 2013
    /// 
    /// </summary>

    class ToolSymbolSetGenPCL
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const Int32 cSizeHddrFixed = 18;

        const Int32 cSizeCharSet_8bit = 256;
        const Int32 cCodePointUnused  = 65535;
        const Int32 cCodePointC0Min   = 0x00;
        const Int32 cCodePointC0Max   = 0x1f;
        const Int32 cCodePointC1Min   = 0x80;
        const Int32 cCodePointC1Max   = 0x9f;

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Stream _opStream = null;
        private BinaryWriter _binWriter = null;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // T o o l S y m b o l S e t G e n P C L                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public ToolSymbolSetGenPCL ()
        {

        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e S y m S e t                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate symbol set definition.                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean generateSymSet (ref String symSetFilename,
                                       Boolean flagIgnoreC0,
                                       Boolean flagIgnoreC1,
                                       UInt16 symSetNo,
                                       UInt16 codeMin,
                                       UInt16 codeMax,
                                       UInt64 charCollReq,
                                       UInt16 [] symSetMap,
                                       PCLSymSetTypes.eIndex symSetType)
        {
            Boolean flagOK = true;

            //----------------------------------------------------------------//
            //                                                                //
            // Open print file and stream.                                    //
            //                                                                //
            //----------------------------------------------------------------//

            try
            {
                streamOpen (ref symSetFilename,
                            ref _binWriter,
                            ref _opStream);
            }

            catch (Exception exc)
            {
                flagOK = false;

                MessageBox.Show (exc.ToString (),
                                "Failure to open symbol set file",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }

            if (flagOK)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Generate symbol set file contents.                         //
                //                                                            //
                //------------------------------------------------------------//

                try
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Write symbol set identifier sequence.                  //
                    //                                                        //
                    //--------------------------------------------------------//

                    PCLWriter.symSetDownloadCode (_binWriter,
                                                  (UInt16)symSetNo);

                    //--------------------------------------------------------//
                    //                                                        //
                    // Write symbol set descriptor header.                    //
                    //                                                        //
                    //--------------------------------------------------------//

                    writeHddr (symSetNo, codeMin, codeMax, charCollReq,
                               PCLSymSetTypes.getIdPCL((Int32) symSetType));

                    //--------------------------------------------------------//
                    //                                                        //
                    // Write symbol set map data.                             //
                    //                                                        //
                    //--------------------------------------------------------//

                    writeMapData (flagIgnoreC1, codeMin, codeMax, symSetMap);

                    //--------------------------------------------------------//
                    //                                                        //
                    // Write symbol set save sequence.                        //
                    //                                                        //
                    //--------------------------------------------------------//

                    PCLWriter.symSetDownloadSave (_binWriter, true);

                    //--------------------------------------------------------//
                    //                                                        //
                    // Close streams and files.                               //
                    //                                                        //
                    //--------------------------------------------------------//

                    _binWriter.Close ();
                    _opStream.Close ();
                }

                catch (Exception exc)
                {
                    flagOK = false;

                    MessageBox.Show (exc.ToString (),
                                    "Failure to write symbol set file",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                }
            }

            return flagOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l s B y t e                                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return low (least-significant) byte from supplied unsigned 16-bit  //
        // integer.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Byte lsByte (UInt16 value)
        {
            return (Byte)(value & 0x00ff);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l s U I n t 1 6                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return low (least-significant) unsigned 16-bit integer from        //
        // supplied unsigned 32-bit integer.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private UInt16 lsUInt16 (UInt32 value)
        {
            return (UInt16)(value & 0x0000ffff);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l s U I n t 3 2                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return low (least-significant) unsigned 32-bit integer from        //
        // supplied unsigned 64-bit integer.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private UInt32 lsUInt32 (UInt64 value)
        {
            return (UInt32)(value & 0x0000ffffffff);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m s B y t e                                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return high (most-significant) byte from supplied unsigned 16-bit  //
        // integer.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Byte msByte (UInt16 value)
        {
            return (Byte)((value & 0xff00) >> 8);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m s U I n t 1 6                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return high (most-significant) unsigned 16-bit integer from        //
        // supplied unsigned 32-bit integer.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private UInt16 msUInt16 (UInt32 value)
        {
            return (UInt16)((value & 0xffff0000) >> 16);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m s U I n t 3 2                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return high (most-significant) unsigned 32-bit integer from        //
        // supplied unsigned 64-bit integer.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private UInt32 msUInt32 (UInt64 value)
        {
            return (UInt32)((value & 0xffffffff00000000) >> 32);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s t r e a m O p e n                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Create output file via 'Save As' dialogue.                         //
        // Then open target stream and binary writer.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void streamOpen (ref String symSetFilename,
                                ref BinaryWriter binWriter,
                                ref Stream opStream)
        {
            SaveFileDialog saveDialog;

            Int32 ptr,
                  len;

            String saveDirectory;
            String tmpFilename;

            ptr = symSetFilename.LastIndexOf ("\\");

            if (ptr <= 0)
            {
                saveDirectory = "";
                tmpFilename = symSetFilename;
            }
            else
            {
                len = symSetFilename.Length;

                saveDirectory = symSetFilename.Substring (0, ptr);
                tmpFilename = symSetFilename.Substring (ptr + 1,
                                                          len - ptr - 1);
            }

            saveDialog = new SaveFileDialog ();

            saveDialog.Filter = "PCL Files | *.pcl";
            saveDialog.DefaultExt = "pcl";

            saveDialog.RestoreDirectory = true;
            saveDialog.InitialDirectory = saveDirectory;
            saveDialog.OverwritePrompt = true;
            saveDialog.FileName = tmpFilename;

            Nullable<Boolean> dialogResult = saveDialog.ShowDialog ();

            if (dialogResult == true)
            {
                symSetFilename = saveDialog.FileName;
                tmpFilename = symSetFilename;
            }

            opStream = File.Create (tmpFilename);

            if (opStream != null)
            {
                _binWriter = new BinaryWriter (opStream);
                binWriter = _binWriter;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e B u f f e r                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write contents of supplied buffer to output symbol set file.       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void writeBuffer (Int32 bufLen,
                                 Byte [] buffer)
        {
            _binWriter.Write (buffer, 0, bufLen);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e H d d r                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate symbol set download header sequence and fixed part of     //
        // header.                                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void writeHddr (UInt16 symSetNo,
                                UInt16 codeMin,
                                UInt16 codeMax,
                                UInt64 charCollReq,
                                Byte   symSetType)
        {
            Int32 mapSize;
            Int32 descSize;
       
            UInt16 valUInt16;

            UInt32 valUInt32;

            Byte [] hddrDesc = new Byte [cSizeHddrFixed];

            //----------------------------------------------------------------//
            //                                                                //
            // Calculate total size of header.                                //
            // Write PCL 'download header' escape sequence.                   //
            //                                                                //
            //----------------------------------------------------------------//

            mapSize = (codeMax - codeMin + 1) * 2;
            descSize = mapSize + cSizeHddrFixed;

            PCLWriter.symSetDownloadDesc (_binWriter, (UInt32) descSize);

            //----------------------------------------------------------------//
            //                                                                //
            // Write font header descriptor.                                  //
            //                                                                //
            //----------------------------------------------------------------//

            hddrDesc [0] = msByte (cSizeHddrFixed);
            hddrDesc [1] = lsByte (cSizeHddrFixed);

            hddrDesc [2] = msByte (symSetNo);   // Symbol set Kind1 Id MSB
            hddrDesc [3] = lsByte (symSetNo);   // Symbol set Kind1 Id LSB

            hddrDesc [4] = 3;                   // Format = Unicode
            hddrDesc [5] = symSetType;          // Type
            hddrDesc [6] = msByte (codeMin);    // First code MSB
            hddrDesc [7] = lsByte (codeMin);    // First code LSB
            hddrDesc [8] = msByte (codeMax);    // Last code MSB
            hddrDesc [9] = lsByte (codeMax);    // Last code LSB

            valUInt32 = msUInt32 (charCollReq);
            valUInt16 = msUInt16 (valUInt32);
            hddrDesc [10] = msByte (valUInt16); // Char. Req. byte 0
            hddrDesc [11] = lsByte (valUInt16); // Char. Req. byte 1

            valUInt16 = lsUInt16 (valUInt32);
            hddrDesc [12] = msByte (valUInt16); // Char. Req. byte 2
            hddrDesc [13] = lsByte (valUInt16); // Char. Req. byte 3

            valUInt32 = lsUInt32 (charCollReq);
            valUInt16 = msUInt16 (valUInt32);
            hddrDesc [14] = msByte (valUInt16); // Char. Req. byte 4
            hddrDesc [15] = lsByte (valUInt16); // Char. Req. byte 5

            valUInt16 = lsUInt16 (valUInt32);
            hddrDesc [16] = msByte (valUInt16); // Char. Req. byte 6
            hddrDesc [17] = lsByte (valUInt16); // Char. Req. byte 7

            writeBuffer (cSizeHddrFixed, hddrDesc);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e M a p H d d r                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate symbol set mapping data.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void writeMapData (Boolean   flagIgnoreC1,
                                   UInt16    codeMin,
                                   UInt16    codeMax,
                                   UInt16 [] symSetMap)
        {
            Int32 mapSize = (codeMax - codeMin + 1) * 2;

            Byte [] mapArray = new Byte [mapSize];

            for (Int32 i = codeMin; i <= codeMax; i++)
            {
                Int32 j = (i - codeMin) * 2;

                if ((flagIgnoreC1) &&
                    ((i >= cCodePointC1Min) && (i <= cCodePointC1Max)))
                {
                    mapArray [j]     = 0xff;
                    mapArray [j + 1] = 0xff;
                }
                else
                {
                    mapArray [j]     = msByte (symSetMap [i]);
                    mapArray [j + 1] = lsByte (symSetMap [i]);
                }
            }

            writeBuffer (mapSize, mapArray);
        }
    }
}
