using Microsoft.Win32;
using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class manages persistent storage of options for the Prn Print tool.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class ToolPrnPrintPersist
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        const String _mainKey                 = MainForm._regMainKey;

        const String _subKeyTools             = "Tools";
        const String _subKeyToolsPrnPrint     = "PrnPrint";

        const String _nameCaptureFile         = "CaptureFile";
        const String _nameFilename            = "Filename";

        const String _defaultCaptureFile      = "CaptureFile_PrnPrint.prn";  
        const String _defaultFilename         = "DefaultPrintFile.prn";

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a C a p t u r e                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored Prn Print capture file data.                       //
        // The 'current PDL' parameter is not relevant and is ignored.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataCapture (ToolCommonData.ePrintLang crntPDL,
                                            ref String captureFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsPrnPrint;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                captureFile = (String)subKey.GetValue (_nameCaptureFile,
                                                       defWorkFolder + "\\" +
                                                       _defaultCaptureFile);
            }
        }
        
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a G e n e r a l                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored print file data.                                   //
        // Missing items are given default values.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataGeneral (ref String filename)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsPrnPrint;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                filename      = (String)subKey.GetValue(_nameFilename,
                                                        defWorkFolder + "\\" +
                                                        _defaultFilename);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a C a p t u r e                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current Prn Print capture file data.                         //
        // The 'current PDL' parameter is not relevant and is ignored.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataCapture (ToolCommonData.ePrintLang crntPDL,
                                            String captureFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsPrnPrint;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                if (captureFile != null)
                {
                    subKey.SetValue (_nameCaptureFile,
                                     captureFile,
                                     RegistryValueKind.String);
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a G e n e r a l                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current print file data.                                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataGeneral (String filename)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsPrnPrint;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                if (filename != null)
                {
                    subKey.SetValue (_nameFilename,
                                    filename,
                                    RegistryValueKind.String);
                }
            }
        }
    }
}
