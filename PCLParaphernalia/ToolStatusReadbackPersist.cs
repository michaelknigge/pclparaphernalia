using Microsoft.Win32;
using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class manages persistent storage of options for the StatusReadback tool.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class ToolStatusReadbackPersist
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        const String _mainKey                   = MainForm._regMainKey;

        const String _subKeyTools               = "Tools";
        const String _subKeyToolsStatusReadback = "StatusReadback";
        const String _subKeyPCL                 = "PCL";
        const String _subKeyPJL                 = "PJL";
        const String _subKeyPJLFS               = "PJLFS";

        const String _nameBinSrcFile            = "BinSrcFile";
        const String _nameBinTgtFile            = "BinTgtFile";
        const String _nameCaptureFile           = "CaptureFile";
        const String _nameCustomCat             = "CustomCat";
        const String _nameCustomVar             = "CustomVar";
        const String _nameObjectPath            = "ObjectPath";
        const String _nameReportFile            = "ReportFile";
        const String _nameFlagPJLFS             = "flagPJLFS";
        const String _nameFlagPJLFSSecJob       = "flagPJLFSSecJob";
        const String _nameIndxCategory          = "IndxCategory";
        const String _nameIndxCommand           = "IndxCommand";
        const String _nameIndxEntityType        = "IndxEntityType";
        const String _nameIndxLocationType      = "IndxLocationType";
        const String _nameIndxPDL               = "IndxPDL";
        const String _nameIndxRptFileFmt        = "IndxRptFileFmt";
        const String _nameIndxVariable          = "IndxVariable";

        const Int32 _flagFalse                  = 0;
        const Int32 _flagTrue                   = 1;
        const Int32 _indexZero                  = 0;

        const String _defaultCaptureFilePCL     = "CaptureFile_StatusReadbackPCL.prn";
        const String _defaultCaptureFilePJL     = "CaptureFile_StatusReadbackPJL.prn";

        const String _defaultReportFilePCL      = "ReportFile_StatusReadbackPCL.txt";
        const String _defaultReportFilePJL      = "ReportFile_StatusReadbackPJL.txt";
        
        const String _defaultCustomCatPJL       = "CUSTOM_CAT_1";
        const String _defaultCustomVarPJL       = "CUSTOM_VAR_1";

        const String _defaultBinSrcFilePJLFS    = "BinSrcFile_PJLFS.pcl";
        const String _defaultBinTgtFilePJLFS    = "BinTgtFile_PJLFS.pcl";
        const String _defaultObjectPathPJLFS    = "0:\\pcl\\macros\\macro1";

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a C a p t u r e                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored Status Readback capture file data.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataCapture(ToolCommonData.ePrintLang crntPDL,
                                            ref String captureFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String oldKey = _subKeyTools + "\\" + _subKeyToolsStatusReadback;
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
                                 "\\" + _subKeyToolsStatusReadback +
                                 "\\" + _subKeyPCL;

                using (RegistryKey subKey = keyMain.CreateSubKey(keyPCL))
                {
                    subKey.SetValue(_nameCaptureFile,
                                     oldFile,
                                     RegistryValueKind.String);
                }

                String keyPJL = _subKeyTools +
                                 "\\" + _subKeyToolsStatusReadback +
                                 "\\" + _subKeyPJL;

                using (RegistryKey subKey = keyMain.CreateSubKey(keyPJL))
                {
                    subKey.SetValue(_nameCaptureFile,
                                     oldFile,
                                     RegistryValueKind.String);
                }
            }

            if (crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                String key = _subKeyTools + "\\" + _subKeyToolsStatusReadback +
                                            "\\" + _subKeyPCL;

                using (RegistryKey subKey = keyMain.CreateSubKey(key))
                {
                    captureFile = (String)subKey.GetValue(
                        _nameCaptureFile,
                        defWorkFolder + "\\" + _defaultCaptureFilePCL);
                }
            }
            else if (crntPDL == ToolCommonData.ePrintLang.PJL)
            {
                String key = _subKeyTools + "\\" + _subKeyToolsStatusReadback +
                                            "\\" + _subKeyPJL;

                using (RegistryKey subKey = keyMain.CreateSubKey(key))
                {
                    captureFile = (String)subKey.GetValue(
                        _nameCaptureFile,
                        defWorkFolder + "\\" + _defaultCaptureFilePJL);
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a C o m m o n                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored StatusReadback common data.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataCommon (ref Int32   indxPDL)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsStatusReadback;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                indxPDL = (Int32)subKey.GetValue(_nameIndxPDL,
                                                 _indexZero);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a P C L                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored StatusReadback PCL data.                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataPCL(ref Int32  indxEntityType,
                                       ref Int32  indxLocationType,
                                       ref String reportFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            String oldKey = _subKeyTools + "\\" + _subKeyToolsStatusReadback;
            String oldFile;

            Boolean update_from_v2_5_0_0 = false;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            using (RegistryKey subKey = keyMain.OpenSubKey(oldKey, true))
            {
                oldFile = (String)subKey.GetValue(_nameReportFile);

                if (oldFile != null)
                {
                    update_from_v2_5_0_0 = true;

                    subKey.DeleteValue(_nameReportFile);
                }
            }

            if (update_from_v2_5_0_0)
            {
                String keyPCL = _subKeyTools +
                                 "\\" + _subKeyToolsStatusReadback +
                                 "\\" + _subKeyPCL;

                using (RegistryKey subKey = keyMain.CreateSubKey(keyPCL))
                {
                    subKey.SetValue(_nameReportFile,
                                     oldFile,
                                     RegistryValueKind.String);
                }

                String keyPJL = _subKeyTools +
                                 "\\" + _subKeyToolsStatusReadback +
                                 "\\" + _subKeyPJL;

                using (RegistryKey subKey = keyMain.CreateSubKey(keyPJL))
                {
                    subKey.SetValue(_nameReportFile,
                                     oldFile,
                                     RegistryValueKind.String);
                }
            }

            key = _subKeyTools + "\\" + _subKeyToolsStatusReadback +
                                 "\\" + _subKeyPCL;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                indxEntityType = (Int32)subKey.GetValue(_nameIndxEntityType,
                                                        _indexZero);

                indxLocationType = (Int32)subKey.GetValue(_nameIndxLocationType,
                                                          _indexZero);

                reportFile = (String)subKey.GetValue(_nameReportFile,
                                                      defWorkFolder + "\\" +
                                                      _defaultReportFilePCL);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a P J L                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored StatusReadback PJL data.                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataPJL(ref Int32  indxCategory,
                                       ref Int32  indxCommand,
                                       ref Int32  indxVariable,
                                       ref String customCat,
                                       ref String customVar,
                                       ref String reportFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            String oldKey = _subKeyTools + "\\" + _subKeyToolsStatusReadback;
            String oldFile;

            Boolean update_from_v2_5_0_0 = false;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            using (RegistryKey subKey = keyMain.OpenSubKey(oldKey, true))
            {
                oldFile = (String)subKey.GetValue(_nameReportFile);

                if (oldFile != null)
                {
                    update_from_v2_5_0_0 = true;

                    subKey.DeleteValue(_nameCaptureFile);
                }
            }

            if (update_from_v2_5_0_0)
            {
                String keyPCL = _subKeyTools +
                                 "\\" + _subKeyToolsStatusReadback +
                                 "\\" + _subKeyPCL;

                using (RegistryKey subKey = keyMain.CreateSubKey(keyPCL))
                {
                    subKey.SetValue(_nameReportFile,
                                     oldFile,
                                     RegistryValueKind.String);
                }

                String keyPJL = _subKeyTools +
                                 "\\" + _subKeyToolsStatusReadback +
                                 "\\" + _subKeyPJL;

                using (RegistryKey subKey = keyMain.CreateSubKey(keyPJL))
                {
                    subKey.SetValue(_nameReportFile,
                                     oldFile,
                                     RegistryValueKind.String);
                }
            }

            key = _subKeyTools + "\\" + _subKeyToolsStatusReadback +
                                 "\\" + _subKeyPJL;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                indxCategory = (Int32)subKey.GetValue(_nameIndxCategory,
                                                      _indexZero);

                indxCommand  = (Int32)subKey.GetValue(_nameIndxCommand,
                                                      _indexZero);

                indxVariable = (Int32)subKey.GetValue(_nameIndxVariable,
                                                      _indexZero);

                customCat  = (String)subKey.GetValue(_nameCustomCat,
                                                     _defaultCustomCatPJL);

                customVar  = (String)subKey.GetValue(_nameCustomVar,
                                                     _defaultCustomVarPJL);

                reportFile = (String)subKey.GetValue(_nameReportFile,
                                                      defWorkFolder + "\\" +
                                                      _defaultReportFilePJL);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a P J L F S                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored StatusReadback PJL File System data.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataPJLFS (ref Int32 indxCommand,
                                          ref String objectPath,
                                          ref String binSrcFile,
                                          ref String binTgtFile,
                                          ref Boolean flagPJLFS,
                                          ref Boolean flagPJLFSSecJob)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key;

            Int32 tmpInt;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            key = _subKeyTools + "\\" + _subKeyToolsStatusReadback +
                                 "\\" + _subKeyPJLFS;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                indxCommand = (Int32) subKey.GetValue (_nameIndxCommand,
                                                      _indexZero);

                objectPath = (String) subKey.GetValue (_nameObjectPath,
                                                       _defaultObjectPathPJLFS);

                binSrcFile = (String) subKey.GetValue (_nameBinSrcFile,
                                                       _defaultBinSrcFilePJLFS);

                binTgtFile = (String) subKey.GetValue (_nameBinTgtFile,
                                                       _defaultBinTgtFilePJLFS);

                tmpInt = (Int32) subKey.GetValue (_nameFlagPJLFS,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagPJLFS = false;
                else
                    flagPJLFS = true;

                tmpInt = (Int32) subKey.GetValue (_nameFlagPJLFSSecJob,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagPJLFSSecJob = false;
                else
                    flagPJLFSSecJob = true;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a R p t                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored report file data.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataRpt (ref Int32 indxRptFileFmt)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsStatusReadback;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                indxRptFileFmt = (Int32)subKey.GetValue (_nameIndxRptFileFmt,
                                                         _indexZero);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a C a p t u r e                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current Status Readback capture file data.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataCapture(ToolCommonData.ePrintLang crntPDL,
                                            String captureFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            if (crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                String key = _subKeyTools + "\\" + _subKeyToolsStatusReadback +
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
            else if (crntPDL == ToolCommonData.ePrintLang.PJL)
            {
                String key = _subKeyTools + "\\" + _subKeyToolsStatusReadback +
                                            "\\" + _subKeyPJL;

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
        // Store current StatusReadback common data.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataCommon (Int32   indxPDL)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsStatusReadback;

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
        // Store current StatusReadback PCL data.                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataPCL(Int32  indxEntityType,
                                       Int32  indxLocType,
                                       String reportFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsStatusReadback +
                                 "\\" + _subKeyPCL;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue(_nameIndxEntityType,
                                indxEntityType,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxLocationType,
                                indxLocType,
                                RegistryValueKind.DWord);

                if (reportFile != null)
                {
                    subKey.SetValue(_nameReportFile,
                                    reportFile,
                                    RegistryValueKind.String);
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a P J L                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current StatusReadback PJL data.                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataPJL(Int32  indxCategory,
                                       Int32  indxCommand, 
                                       Int32  indxVariable,
                                       String customCat,
                                       String customVar,
                                       String reportFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsStatusReadback +
                                 "\\" + _subKeyPJL;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue(_nameIndxCategory,
                                indxCategory,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxCommand,
                                indxCommand,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxVariable,
                                indxVariable,
                                RegistryValueKind.DWord);

                if (customCat != null)
                {
                    subKey.SetValue(_nameCustomCat,
                                    customCat,
                                    RegistryValueKind.String);
                }

                if (customVar != null)
                {
                    subKey.SetValue(_nameCustomVar,
                                    customVar,
                                    RegistryValueKind.String);
                }

                if (reportFile != null)
                {
                    subKey.SetValue(_nameReportFile,
                                    reportFile,
                                    RegistryValueKind.String);
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a P J L F S                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current StatusReadback PJL File System data.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataPJLFS (Int32  indxCommand, 
                                          String objectPath,
                                          String binSrcFile,
                                          String binTgtFile,
                                          Boolean flagPJLFS,
                                          Boolean flagPJLFSSecJob)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsStatusReadback +
                                 "\\" + _subKeyPJLFS;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue(_nameIndxCommand,
                                indxCommand,
                                RegistryValueKind.DWord);

                if (objectPath != null)
                {
                    subKey.SetValue(_nameObjectPath,
                                    objectPath,
                                    RegistryValueKind.String);
                }

                if (binSrcFile != null)
                {
                    subKey.SetValue(_nameBinSrcFile,
                                    binSrcFile,
                                    RegistryValueKind.String);
                }

                if (binTgtFile != null)
                {
                    subKey.SetValue(_nameBinTgtFile,
                                    binTgtFile,
                                    RegistryValueKind.String);
                }

                if (flagPJLFS)
                    subKey.SetValue (_nameFlagPJLFS,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagPJLFS,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagPJLFSSecJob)
                    subKey.SetValue (_nameFlagPJLFSSecJob,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagPJLFSSecJob,
                                    _flagFalse,
                                    RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a R p t                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current report file data.                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataRpt (Int32 indxRptFileFmt)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsStatusReadback;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                subKey.SetValue (_nameIndxRptFileFmt,
                                indxRptFileFmt,
                                RegistryValueKind.DWord);
            }
        }
    }
}
