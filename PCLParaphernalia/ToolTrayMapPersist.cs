using Microsoft.Win32;
using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class manages persistent storage of options for the TrayMap tool.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class ToolTrayMapPersist
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        const String _mainKey             = MainForm._regMainKey;

        const String _subKeyTools         = "Tools";
        const String _subKeyToolsTrayMap  = "TrayMap";
        const String _subKeyPCL5          = "PCL5";
        const String _subKeyPCL6          = "PCL6";
        const String _subKeyPCL           = "PCL";
        const String _subKeyPCLXL         = "PCLXL";
        const String _subKeySheetRoot     = "Sheet_";

        const String _nameCaptureFile     = "CaptureFile";
        const String _nameFlagFormAsMacro = "FlagFormAsMacro";
        const String _nameIndxOrientation = "IndxOrientation"; // pre v2.8 //
        const String _nameIndxOrientFront = "IndxOrientFront";
        const String _nameIndxOrientRear  = "IndxOrientRear";
        const String _nameIndxPaperSize   = "IndxPaperSize";
        const String _nameIndxPaperType   = "IndxPaperType";
        const String _nameIndxPaperTray   = "IndxPaperTray";
        const String _nameIndxPDL         = "IndxPDL";
        const String _nameIndxPlexMode    = "IndxPlexMode";
        const String _nameIndxTrayIdOpt   = "IndxTrayIdOpt"; // pre v2.8 //
        const String _nameSheetCt         = "SheetCt";
        const String _nameTrayIdList      = "TrayIdList"; // pre v2.8 //

        const Int32 _flagFalse            = 0;
        const Int32 _flagTrue             = 1;
        const Int32 _indexZero            = 0;
        const Int32 _indexOne             = 1;

        const String _defaultCaptureFilePCL   = "CaptureFile_TrayMapPCL.prn";
        const String _defaultCaptureFilePCLXL = "CaptureFile_TrayMapPCLXL.prn";

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a C a p t u r e                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored Tray Map capture file data.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataCapture(ToolCommonData.ePrintLang crntPDL,
                                            ref String captureFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String oldKey = _subKeyTools + "\\" + _subKeyToolsTrayMap;
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
                                 "\\" + _subKeyToolsTrayMap +
                                 "\\" + _subKeyPCL;

                using (RegistryKey subKey = keyMain.CreateSubKey(keyPCL))
                {
                    subKey.SetValue(_nameCaptureFile,
                                     oldFile,
                                     RegistryValueKind.String);
                }

                String keyPCLXL = _subKeyTools +
                                 "\\" + _subKeyToolsTrayMap +
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
                String key = _subKeyTools + "\\" + _subKeyToolsTrayMap +
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
                String key = _subKeyTools + "\\" + _subKeyToolsTrayMap +
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
        // Retrieve stored TrayMap common data.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataCommon(ref Int32 indxPDL)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsTrayMap;

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
        // l o a d D a t a P C L O p t                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored TrayMap PCL options.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataPCLOpt (ref Boolean   flagFormAsMacro,
                                           ref Int32     sheetCt)
        { 
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            Int32 tmpInt;

            Byte[] buffer = {0x00};

            key = _subKeyTools + "\\" + _subKeyToolsTrayMap +
                                 "\\" + _subKeyPCL;

            if (MainFormData.VersionChange)
            {
                Boolean update_2_8 = false;

                Int32 vMaj = -1,
                      vMin = -1,
                      vBui = -1,
                      vRev = -1;

                MainFormData.getVersionData (false,
                                             ref vMaj, ref vMin,
                                             ref vBui, ref vRev);

                if ((vMaj == 2) && (vMin < 8))
                    update_2_8 = true;

                if (update_2_8)
                {
                    using (RegistryKey subKey = keyMain.CreateSubKey(key))
                    {
                        subKey.DeleteValue(_nameIndxOrientation);
                        subKey.DeleteValue(_nameIndxPaperSize);
                        subKey.DeleteValue(_nameIndxPaperType);
                        subKey.DeleteValue(_nameIndxTrayIdOpt);
                        subKey.DeleteValue(_nameTrayIdList);
                    }
                }
            }

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                tmpInt          = (Int32)subKey.GetValue(_nameFlagFormAsMacro,
                                                         _flagTrue);

                if (tmpInt == _flagFalse)
                    flagFormAsMacro = false;
                else
                    flagFormAsMacro = true;

                sheetCt = (Int32)subKey.GetValue(_nameSheetCt,
                                                 _indexOne);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a P C L X L O p t                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored TrayMap PCL XL options.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataPCLXLOpt (ref Boolean flagFormAsMacro,
                                             ref Int32   sheetCt)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            Int32 tmpInt;

            Byte[] buffer = { 0x00 };

            key = _subKeyTools + "\\" + _subKeyToolsTrayMap +
                                 "\\" + _subKeyPCLXL;

            if (MainFormData.VersionChange)
            {
                Boolean update_2_8 = false;

                Int32 vMaj = -1,
                      vMin = -1,
                      vBui = -1,
                      vRev = -1;

                MainFormData.getVersionData(false,
                                             ref vMaj, ref vMin,
                                             ref vBui, ref vRev);

                if ((vMaj == 2) && (vMin < 8))
                    update_2_8 = true;

                if (update_2_8)
                {
                    using (RegistryKey subKey = keyMain.CreateSubKey(key))
                    {
                        subKey.DeleteValue(_nameIndxOrientation);
                        subKey.DeleteValue(_nameIndxPaperSize);
                        subKey.DeleteValue(_nameIndxPaperType);
                        subKey.DeleteValue(_nameIndxTrayIdOpt);
                        subKey.DeleteValue(_nameTrayIdList);
                    }
                }
            }

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                tmpInt = (Int32)subKey.GetValue(_nameFlagFormAsMacro,
                                                         _flagTrue);

                if (tmpInt == _flagFalse)
                    flagFormAsMacro = false;
                else
                    flagFormAsMacro = true;

                sheetCt = (Int32)subKey.GetValue(_nameSheetCt,
                                                 _indexOne);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a S h e e t O p t                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored TrayMap Sheet option data for PCL or PCLXL.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataSheetOpt (String pdlName,
                                             Int32 sheetNo,
                                             ref Int32 indxPaperSize,
                                             ref Int32 indxPaperType,
                                             ref Int32 indxPaperTray,
                                             ref Int32 indxPlexMode,
                                             ref Int32 indxOrient_F,
                                             ref Int32 indxOrient_R)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsTrayMap +
                                 "\\" + pdlName +
                                 "\\" + _subKeySheetRoot +
                                 sheetNo.ToString ("D2");

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                indxPaperSize = (Int32)subKey.GetValue(_nameIndxPaperSize,
                                                         _indexZero);

                indxPaperType = (Int32)subKey.GetValue(_nameIndxPaperType,
                                                         _indexZero);

                indxPaperTray = (Int32)subKey.GetValue(_nameIndxPaperTray,
                                                       _indexZero);

                indxPlexMode = (Int32)subKey.GetValue(_nameIndxPlexMode,
                                                      _indexZero);

                indxOrient_F = (Int32)subKey.GetValue(_nameIndxOrientFront,
                                                      _indexZero);

                indxOrient_R = (Int32)subKey.GetValue(_nameIndxOrientRear,
                                                      _indexZero);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a C a p t u r e                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current Tray Map capture file data.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataCapture(ToolCommonData.ePrintLang crntPDL,
                                            String captureFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            if (crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                String key = _subKeyTools + "\\" + _subKeyToolsTrayMap +
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
                String key = _subKeyTools + "\\" + _subKeyToolsTrayMap +
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
        // Store current TrayMap common data.                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataCommon(Int32 indxPDL)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsTrayMap;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue(_nameIndxPDL,
                                indxPDL,
                                RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a P C L O p t                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current TrayMap PCL options.                                 //
        //--------------------------------------------------------------------//

        public static void saveDataPCLOpt (Boolean   flagFormAsMacro,
                                           Int32     sheetCt)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsTrayMap +
                                 "\\" + _subKeyPCL;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                if (flagFormAsMacro)
                    subKey.SetValue(_nameFlagFormAsMacro,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagFormAsMacro,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                subKey.SetValue(_nameSheetCt,
                                sheetCt,
                                RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a P C L X L O p t                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current TrayMap PCL XL options.                              //
        //--------------------------------------------------------------------//

        public static void saveDataPCLXLOpt (Boolean   flagFormAsMacro,
                                             Int32     sheetCt)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsTrayMap +
                                 "\\" + _subKeyPCLXL;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                if (flagFormAsMacro)
                    subKey.SetValue(_nameFlagFormAsMacro,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagFormAsMacro,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                subKey.SetValue(_nameSheetCt,
                                sheetCt,
                                RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a S h e e t O p t                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current TrayMap Sheet option data for PCL or PCLXL.          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataSheetOpt (String pdlName,
                                             Int32 sheetNo,
                                             Int32 indxPaperSize,
                                             Int32 indxPaperType,
                                             Int32 indxPaperTray,
                                             Int32 indxPlexMode,
                                             Int32 indxOrientFront,
                                             Int32 indxOrientRear)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsTrayMap +
                                 "\\" + pdlName +
                                 "\\" + _subKeySheetRoot +
                                 sheetNo.ToString("D2");

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue(_nameIndxPaperSize,
                                indxPaperSize,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxPaperType,
                                indxPaperType,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxPaperTray,
                                indxPaperTray,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxPlexMode,
                                indxPlexMode,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxOrientFront,
                                indxOrientFront,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxOrientRear,
                                indxOrientRear,
                                RegistryValueKind.DWord);
            }
        }
    }
}
