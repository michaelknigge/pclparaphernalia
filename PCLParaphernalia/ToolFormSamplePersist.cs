using Microsoft.Win32;
using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class manages persistent storage of options for the FormSample tool.
    /// 
    /// © Chris Hutchinson 2012
    /// 
    /// </summary>

    static class ToolFormSamplePersist
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const String _mainKey                 = MainForm._regMainKey;

        const String _subKeyTools             = "Tools";
        const String _subKeyToolsFormSample   = "FormSample";
        const String _subKeyPCL5              = "PCL5"; 
        const String _subKeyPCL6              = "PCL6";
        const String _subKeyPCL               = "PCL"; 
        const String _subKeyPCLXL             = "PCLXL";

        const String _nameCaptureFile         = "CaptureFile";
        const String _nameFormNameMain        = "FormNameMain";
        const String _nameFormNameRear        = "FormNameRear";
        const String _nameFormFileMain        = "FormFileMain";
        const String _nameFormFileRear        = "FormFileRear";
        const String _namePrnDiskFileMain     = "PrnDiskFileMain";
        const String _namePrnDiskFileRear     = "PrnDiskFileRear";
        const String _nameMacroIdMain         = "MacroIdMain";
        const String _nameMacroIdRear         = "MacroIdRear";
        const String _nameFlagMacroRemove     = "FlagMacroRemove";
        const String _nameFlagMainForm        = "FlagMainForm";
        const String _nameFlagRearForm        = "FlagRearForm";
        const String _nameFlagMainOnPrnDisk   = "FlagMainOnPrnDisk";
        const String _nameFlagRearOnPrnDisk   = "FlagRearOnPrnDisk";
        const String _nameFlagRearBPlate      = "FlagRearBPlate";
        const String _nameFlagGSPushPop       = "FlagGSPushPop";
        const String _nameFlagPrintDescText   = "FlagPrintDescText";
        const String _nameTestPageCount       = "TestPageCount";
        const String _nameIndxMethod          = "IndxMethod";
        const String _nameIndxOrientation     = "IndxOrientation";
        const String _nameIndxOrientRear      = "IndxOrientationRear";
        const String _nameIndxPaperSize       = "IndxPaperSize";
        const String _nameIndxPaperType       = "IndxPaperType";
        const String _nameIndxPDL             = "IndxPDL";
        const String _nameIndxPlexMode        = "IndxPlexMode";

        const Int32 _flagFalse                = 0;
        const Int32 _flagTrue                 = 1;
        const Int32 _indexZero                = 0;

        const String _defaultCaptureFile      = "Capture_FormSample.prn";
        const String _defaultCaptureFilePCL   = "CaptureFile_FormSamplePCL.prn";
        const String _defaultCaptureFilePCLXL = "CaptureFile_FormSamplePCLXL.prn";
        const String _defaultFilePCLMain      = "DefaultFilePCLMain.ovl";
        const String _defaultFilePCLRear      = "DefaultFilePCLRear.ovl";
        const String _defaultFilePCLXLMain    = "DefaultFilePCLXLMain.ovx";
        const String _defaultFilePCLXLRear    = "DefaultFilePCLXLRear.ovx";
        const String _defaultFormNameMain     = "TestFormMain";  
        const String _defaultFormNameRear     = "TestFormRear";

        const Int32 _defaultMacroIdMain       = 32767;
        const Int32 _defaultMacroIdRear       = 32766;
        const Int32 _defaultTestPageCount     = 3;

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a C a p t u r e                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored FormSample capture file data.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataCapture(ToolCommonData.ePrintLang crntPDL,
                                            ref String captureFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String oldKey = _subKeyTools + "\\" + _subKeyToolsFormSample;
            String oldFile;

            Boolean update_from_v2_5_0_0 = false;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            //----------------------------------------------------------------//

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
                                "\\" + _subKeyToolsFormSample +
                                "\\" + _subKeyPCL;

                using (RegistryKey subKey = keyMain.CreateSubKey(keyPCL))
                {
                    subKey.SetValue(_nameCaptureFile,
                                     oldFile,
                                     RegistryValueKind.String);
                }

                String keyPCLXL = _subKeyTools +
                                  "\\" + _subKeyToolsFormSample +
                                  "\\" + _subKeyPCLXL;

                using (RegistryKey subKey = keyMain.CreateSubKey(keyPCLXL))
                {
                    subKey.SetValue(_nameCaptureFile,
                                     oldFile,
                                     RegistryValueKind.String);
                }
            }

            //----------------------------------------------------------------//

            if (crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                String key = _subKeyTools + "\\" + _subKeyToolsFormSample +
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
                String key = _subKeyTools + "\\" + _subKeyToolsFormSample +
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
        // Retrieve stored FormSample common data.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataCommon (ref Int32 indxPDL)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsFormSample;

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
        // l o a d D a t a G e n e r a l                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored FormSample PCL or PCL XL general data.             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataGeneral (
            String       pdlName,
            ref Int32    indxPaperType,
            ref Int32    indxPaperSize,
            ref Int32    indxOrientation,
            ref Int32    indxPlexMode,
            ref Int32    indxOrientRear,
            ref Int32    indxMethod,
            ref Int32    testPageCount,
            ref Boolean  flagMacroRemove,
            ref Boolean  flagMainForm,
            ref Boolean  flagRearForm,
            ref Boolean  flagRearBPlate,
            ref Boolean  flagPrintDescText)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            Int32 tmpInt;

            key = _subKeyTools + "\\" + _subKeyToolsFormSample +
                                 "\\" + pdlName;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                indxPaperType   = (Int32)subKey.GetValue(_nameIndxPaperType,
                                                         _indexZero);
                indxPaperSize   = (Int32)subKey.GetValue(_nameIndxPaperSize,
                                                         _indexZero);
                indxOrientation = (Int32)subKey.GetValue(_nameIndxOrientation,
                                                         _indexZero);
                indxPlexMode    = (Int32)subKey.GetValue(_nameIndxPlexMode,
                                                         _indexZero);
                indxOrientRear  = (Int32)subKey.GetValue(_nameIndxOrientRear,
                                                         _indexZero);
                indxMethod      = (Int32)subKey.GetValue(_nameIndxMethod,
                                                         _indexZero);
                testPageCount   = (Int32)subKey.GetValue(_nameTestPageCount,
                                                         _defaultTestPageCount);

                tmpInt = (Int32) subKey.GetValue (_nameFlagMacroRemove,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagMacroRemove = false;
                else
                    flagMacroRemove = true;

                tmpInt = (Int32) subKey.GetValue (_nameFlagMainForm,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagMainForm = false;
                else
                    flagMainForm = true;

                tmpInt = (Int32) subKey.GetValue (_nameFlagRearForm,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagRearForm = false;
                else
                    flagRearForm = true;

                tmpInt = (Int32) subKey.GetValue (_nameFlagRearBPlate,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagRearBPlate = false;
                else
                    flagRearBPlate = true;

                tmpInt = (Int32) subKey.GetValue (_nameFlagPrintDescText,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagPrintDescText = false;
                else
                    flagPrintDescText = true;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a P C L                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored FormSample PCL form data.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataPCL (ref Boolean  flagMainOnPrnDisk,
                                        ref Boolean  flagRearOnPrnDisk,
                                        ref String   formFileMain,
                                        ref String   formFileRear,
                                        ref String   prnDiskFileMain,
                                        ref String   prnDiskFileRear,
                                        ref Int32    macroIdMain,
                                        ref Int32    macroIdRear)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            Int32 tmpInt;

            key = _subKeyTools + "\\" + _subKeyToolsFormSample +
                                 "\\" + _subKeyPCL;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                tmpInt = (Int32)subKey.GetValue(_nameFlagMainOnPrnDisk,
                                                _flagFalse);

                if (tmpInt == _flagFalse)
                    flagMainOnPrnDisk = false;
                else
                    flagMainOnPrnDisk = true;

                tmpInt = (Int32)subKey.GetValue(_nameFlagRearOnPrnDisk,
                                                _flagFalse);

                if (tmpInt == _flagFalse)
                    flagRearOnPrnDisk = false;
                else
                    flagRearOnPrnDisk = true;

                formFileMain = (String) subKey.GetValue (_nameFormFileMain,
                                                          defWorkFolder + "\\" +
                                                          _defaultFilePCLMain);
                formFileRear  = (String) subKey.GetValue (_nameFormFileRear,
                                                          defWorkFolder + "\\" +
                                                          _defaultFilePCLRear);

                prnDiskFileMain = (String) subKey.GetValue (_namePrnDiskFileMain,
                                                            _defaultFilePCLMain);
                prnDiskFileRear = (String) subKey.GetValue (_namePrnDiskFileRear,
                                                            _defaultFilePCLRear);

                macroIdMain   = (Int32) subKey.GetValue (_nameMacroIdMain,
                                                         _defaultMacroIdMain);
                macroIdRear   = (Int32) subKey.GetValue (_nameMacroIdRear,
                                                         _defaultMacroIdRear);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a P C L X L                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored FormSample PCL XL form data.                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataPCLXL (ref String formFileMain,
                                          ref String formFileRear,
                                          ref String formNameMain,
                                          ref String formNameRear,
                                          ref Boolean flagGSPushPop)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            Int32 tmpInt;

            String key;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            key = _subKeyTools + "\\" + _subKeyToolsFormSample +
                                 "\\" + _subKeyPCLXL;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                formFileMain = (String) subKey.GetValue (_nameFormFileMain,
                                                         defWorkFolder + "\\" +
                                                         _defaultFilePCLXLMain);
                formFileRear = (String) subKey.GetValue (_nameFormFileRear,
                                                         defWorkFolder + "\\" +
                                                         _defaultFilePCLXLRear);
                formNameMain = (String) subKey.GetValue (_nameFormNameMain,
                                                         _defaultFormNameMain);
                formNameRear = (String) subKey.GetValue (_nameFormNameRear,
                                                         _defaultFormNameRear);

                tmpInt = (Int32) subKey.GetValue (_nameFlagGSPushPop,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagGSPushPop = false;
                else
                    flagGSPushPop = true;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a C a p t u r e                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current FormSample capture file data.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataCapture(ToolCommonData.ePrintLang crntPDL,
                                            String captureFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            if (crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                String key = _subKeyTools + "\\" + _subKeyToolsFormSample +
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
                String key = _subKeyTools + "\\" + _subKeyToolsFormSample +
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
        // Store current FormSample common data.                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataCommon (Int32 indxPDL)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsFormSample;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue(_nameIndxPDL,
                                indxPDL,
                                RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a G e n e r a l                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current FormSample PCL or PCL XL general data.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataGeneral (String    pdlName,
                                            Int32     indxPaperType,
                                            Int32     indxPaperSize,
                                            Int32     indxOrientation,
                                            Int32     indxPlexMode,
                                            Int32     indxOrientRear,
                                            Int32     indxMethod,
                                            Int32     testPageCount,
                                            Boolean   flagMacroRemove,
                                            Boolean   flagMainForm,
                                            Boolean   flagRearForm,
                                            Boolean   flagRearBPlate,
                                            Boolean   flagPrintDescText)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);
            
            String key;

            key = _subKeyTools + "\\" + _subKeyToolsFormSample +
                                 "\\" + pdlName;
            
            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue(_nameIndxPaperType,
                                indxPaperType,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxPaperSize,
                                indxPaperSize,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxOrientation,
                                indxOrientation,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxPlexMode,
                                indxPlexMode,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxOrientRear,
                                indxOrientRear,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxMethod,
                                indxMethod,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameTestPageCount,
                                testPageCount,
                                RegistryValueKind.DWord);

                if (flagMacroRemove)
                    subKey.SetValue (_nameFlagMacroRemove,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagMacroRemove,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagMainForm)
                    subKey.SetValue (_nameFlagMainForm,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagMainForm,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagRearForm)
                    subKey.SetValue (_nameFlagRearForm,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagRearForm,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagRearBPlate)
                    subKey.SetValue (_nameFlagRearBPlate,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagRearBPlate,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagPrintDescText)
                    subKey.SetValue (_nameFlagPrintDescText,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagPrintDescText,
                                    _flagFalse,
                                    RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a P C L                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store FormSample PCL form data.                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataPCL (Boolean flagMainOnPrnDisk,
                                        Boolean flagRearOnPrnDisk,
                                        String  formFileMain,
                                        String  formFileRear,
                                        String  prnDiskFileMain,
                                        String  prnDiskFileRear,
                                        Int32   macroIdMain,
                                        Int32   macroIdRear)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsFormSample +
                                 "\\" + _subKeyPCL;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                if (flagMainOnPrnDisk)
                    subKey.SetValue(_nameFlagMainOnPrnDisk,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagMainOnPrnDisk,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagRearOnPrnDisk)
                    subKey.SetValue(_nameFlagRearOnPrnDisk,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagRearOnPrnDisk,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (formFileMain != null)
                {
                    subKey.SetValue (_nameFormFileMain,
                                    formFileMain,
                                    RegistryValueKind.String);
                }

                if (formFileRear != null)
                {
                    subKey.SetValue (_nameFormFileRear,
                                    formFileRear,
                                    RegistryValueKind.String);
                }

                if (prnDiskFileMain != null)
                {
                    subKey.SetValue (_namePrnDiskFileMain,
                                     prnDiskFileMain,
                                     RegistryValueKind.String);
                }

                if (prnDiskFileRear != null)
                {
                    subKey.SetValue (_namePrnDiskFileRear,
                                     prnDiskFileRear,
                                     RegistryValueKind.String);
                }

                subKey.SetValue(_nameMacroIdMain,
                                macroIdMain,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameMacroIdRear,
                                macroIdRear,
                                RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a P C L X L                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store FormSample PCL XL form data.                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataPCLXL (String formFileMain,
                                          String formFileRear,
                                          String formNameMain,
                                          String formNameRear,
                                          Boolean flagGSPushPop)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsFormSample +
                                 "\\" + _subKeyPCLXL;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                if (formFileMain != null)
                {
                    subKey.SetValue (_nameFormFileMain,
                                    formFileMain,
                                    RegistryValueKind.String);
                }

                if (formFileRear != null)
                {
                    subKey.SetValue (_nameFormFileRear,
                                    formFileRear,
                                    RegistryValueKind.String);
                }

                if (formNameMain != null)
                {
                    subKey.SetValue (_nameFormNameMain,
                                    formNameMain,
                                    RegistryValueKind.String);
                }

                if (formNameRear != null)
                {
                    subKey.SetValue (_nameFormNameRear,
                                    formNameRear,
                                    RegistryValueKind.String);
                }

                if (flagGSPushPop)
                    subKey.SetValue (_nameFlagGSPushPop,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagGSPushPop,
                                    _flagFalse,
                                    RegistryValueKind.DWord);
            }
        }
    }
}
