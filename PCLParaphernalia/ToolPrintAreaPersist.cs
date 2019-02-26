using Microsoft.Win32;
using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class manages persistent storage of options for the PrintArea tool.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class ToolPrintAreaPersist
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        const String _mainKey                 = MainForm._regMainKey;

        const String _subKeyTools             = "Tools";
        const String _subKeyToolsPrintArea    = "PrintArea";
        const String _subKeyPCL5              = "PCL5";
        const String _subKeyPCL6              = "PCL6";
        const String _subKeyPCL               = "PCL";
        const String _subKeyPCLXL             = "PCLXL";

        const String _nameCaptureFile         = "CaptureFile";
        const String _nameFlagFormAsMacro     = "FlagFormAsMacro";
        const String _nameFlagCustomUseMetric = "FlagCustomUseMetric";
        const String _nameCustomShortEdge     = "CustomShortEdge";
        const String _nameCustomLongEdge      = "CustomLongEdge";
        const String _nameIndxOrientation     = "IndxOrientation";
        const String _nameIndxPaperSize       = "IndxPaperSize";
        const String _nameIndxPaperType       = "IndxPaperType";
        const String _nameIndxPDL             = "IndxPDL";
        const String _nameIndxPJLCommand      = "IndxPJLCommand";
        const String _nameIndxPlexMode        = "IndxPlexMode";

        const Int32 _flagFalse                = 0;
        const Int32 _flagTrue                 = 1;
        const Int32 _indexZero                = 0;

        const Int32 _customShortEdgeDefault = 4960;    // A4 dots @ 600 dpi 
        const Int32 _customLongEdgeDefault  = 7014;    // A4 dots @ 600 dpi 

        const String _defaultCaptureFilePCL   = "CaptureFile_PrintAreaPCL.prn";
        const String _defaultCaptureFilePCLXL = "CaptureFile_PrintAreaPCLXL.prn";

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a C a p t u r e                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored Print Area capture file data.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataCapture(ToolCommonData.ePrintLang crntPDL,
                                            ref String captureFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String oldKey = _subKeyTools + "\\" + _subKeyToolsPrintArea;
            String oldFile;

            Boolean update_from_v2_5_0_0 = false;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            using (RegistryKey subKey = keyMain.OpenSubKey(oldKey, true))
            {
                oldFile = (String)subKey.GetValue(_nameCaptureFile);

                if (oldFile != null)
                {
                    update_from_v2_5_0_0 = true;

                    subKey.DeleteValue(_nameCaptureFile);
                }
            }

            if (update_from_v2_5_0_0)
            {
                String keyPCL = _subKeyTools +
                                 "\\" + _subKeyToolsPrintArea +
                                 "\\" + _subKeyPCL;

                using (RegistryKey subKey = keyMain.CreateSubKey(keyPCL))
                {
                    subKey.SetValue(_nameCaptureFile,
                                     oldFile,
                                     RegistryValueKind.String);
                }

                String keyPCLXL = _subKeyTools +
                                 "\\" + _subKeyToolsPrintArea +
                                 "\\" + _subKeyPCLXL;

                using (RegistryKey subKey = keyMain.CreateSubKey(keyPCLXL))
                {
                    subKey.SetValue(_nameCaptureFile,
                                     oldFile,
                                     RegistryValueKind.String);
                }
            }

            if (crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                String key = _subKeyTools + "\\" + _subKeyToolsPrintArea +
                                            "\\" + _subKeyPCL;

                using (RegistryKey subKey = keyMain.CreateSubKey(key))
                {
                    captureFile = (String)subKey.GetValue(
                        _nameCaptureFile,
                        defWorkFolder + "\\" + _defaultCaptureFilePCL);
                }
            }
            else if (crntPDL == ToolCommonData.ePrintLang.PCLXL)
            {
                String key = _subKeyTools + "\\" + _subKeyToolsPrintArea +
                                            "\\" + _subKeyPCLXL;

                using (RegistryKey subKey = keyMain.CreateSubKey(key))
                {
                    captureFile = (String)subKey.GetValue(
                        _nameCaptureFile,
                        defWorkFolder + "\\" + _defaultCaptureFilePCLXL);
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a C o m m o n                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored PrintArea common data.                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataCommon(ref Int32 indxPDL)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsPrintArea;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                if (Helper_RegKey.keyExists(subKey, _subKeyPCL5))
                    // update from v2_5_0_0
                    Helper_RegKey.renameKey(subKey, _subKeyPCL5, _subKeyPCL);

                if (Helper_RegKey.keyExists(subKey, _subKeyPCL6))
                    // update from v2_5_0_0
                    Helper_RegKey.renameKey(subKey, _subKeyPCL6, _subKeyPCLXL);

                indxPDL = (Int32)subKey.GetValue(_nameIndxPDL,
                                                 _indexZero);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a P C L                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored PrintArea PCL or PCLXL data.                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataPCL(String      pdlName,
                                       ref Int32   indxOrientation,
                                       ref Int32   indxPaperSize,
                                       ref Int32   indxPaperType,
                                       ref Int32   indxPlexMode,
                                       ref Int32   indxPJLCommand,
                                       ref Boolean flagFormAsMacro,
                                       ref Boolean flagCustomUseMetric,
                                       ref UInt16  customShortEdge,
                                       ref UInt16  customLongEdge)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            Int32 tmpInt;

            Byte[] buffer = { 0x00 };

            key = _subKeyTools + "\\" + _subKeyToolsPrintArea +
                                 "\\" + pdlName;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                indxOrientation = (Int32)subKey.GetValue(_nameIndxOrientation,
                                                         _indexZero);

                indxPaperSize   = (Int32)subKey.GetValue(_nameIndxPaperSize,
                                                         _indexZero);

                indxPaperType   = (Int32)subKey.GetValue(_nameIndxPaperType,
                                                         _indexZero);

                indxPlexMode    = (Int32) subKey.GetValue(_nameIndxPlexMode,
                                                          _indexZero);

                indxPJLCommand = (Int32)subKey.GetValue(_nameIndxPJLCommand,
                                                         _indexZero);

                tmpInt          = (Int32)subKey.GetValue(_nameFlagFormAsMacro,
                                                         _flagTrue);

                if (tmpInt == _flagFalse)
                    flagFormAsMacro = false;
                else
                    flagFormAsMacro = true;

                tmpInt          = (Int32)subKey.GetValue(_nameFlagCustomUseMetric,
                                                         _flagTrue);

                if (tmpInt == _flagFalse)
                    flagCustomUseMetric = false;
                else
                    flagCustomUseMetric = true;

                tmpInt = (Int32)subKey.GetValue(_nameCustomShortEdge,
                                                _customShortEdgeDefault);

                customShortEdge = (UInt16)tmpInt;

                tmpInt = (Int32)subKey.GetValue(_nameCustomLongEdge,
                                                _customLongEdgeDefault);

                customLongEdge  = (UInt16)tmpInt;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a C a p t u r e                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current Print Area capture file data.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataCapture(ToolCommonData.ePrintLang crntPDL,
                                            String captureFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            if (crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                String key = _subKeyTools + "\\" + _subKeyToolsPrintArea +
                                            "\\" + _subKeyPCL;

                using (RegistryKey subKey = keyMain.CreateSubKey(key))
                {
                    if (captureFile != null)
                    {
                        subKey.SetValue(_nameCaptureFile,
                                         captureFile,
                                         RegistryValueKind.String);
                    }
                }
            }
            else if (crntPDL == ToolCommonData.ePrintLang.PCLXL)
            {
                String key = _subKeyTools + "\\" + _subKeyToolsPrintArea +
                                            "\\" + _subKeyPCLXL;

                using (RegistryKey subKey = keyMain.CreateSubKey(key))
                {
                    if (captureFile != null)
                    {
                        subKey.SetValue(_nameCaptureFile,
                                         captureFile,
                                         RegistryValueKind.String);
                    }
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a C o m m o n                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current PrintArea common data.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataCommon(Int32 indxPDL)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsPrintArea;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue(_nameIndxPDL,
                                indxPDL,
                                RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a P C L                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current PrintArea PCL or PCLXL data.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataPCL(String    pdlName,
                                       Int32     indxOrientation,
                                       Int32     indxPaperSize,
                                       Int32     indxPaperType,
                                       Int32     indxPlexMode,
                                       Int32     indxPJLCommand,
                                       Boolean   flagFormAsMacro,
                                       Boolean   flagCustomUseMetric,
                                       UInt16    customShortEdge,
                                       UInt16    customLongEdge)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);
            
            String key;

            key = _subKeyTools + "\\" + _subKeyToolsPrintArea +
                                 "\\" + pdlName;
            
            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue(_nameIndxOrientation,
                                indxOrientation,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxPaperSize,
                                indxPaperSize,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxPaperType,
                                indxPaperType,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxPlexMode,
                                indxPlexMode,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxPJLCommand,
                                indxPJLCommand,
                                RegistryValueKind.DWord);

                if (flagFormAsMacro)
                    subKey.SetValue(_nameFlagFormAsMacro,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagFormAsMacro,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagCustomUseMetric)
                    subKey.SetValue(_nameFlagCustomUseMetric,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagCustomUseMetric,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                subKey.SetValue(_nameCustomShortEdge,
                                customShortEdge,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameCustomLongEdge,
                                customLongEdge,
                                RegistryValueKind.DWord);
            }
        }
    }
}
