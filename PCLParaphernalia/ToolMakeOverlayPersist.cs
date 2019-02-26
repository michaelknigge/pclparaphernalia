using Microsoft.Win32;
using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class manages persistent storage of options for the Make Overlay tool.
    /// 
    /// © Chris Hutchinson 2012
    /// 
    /// </summary>

    static class ToolMakeOverlayPersist
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        const String _mainKey                 = MainForm._regMainKey;

        const String _subKeyTools             = "Tools";
        const String _subKeyToolsMakeOverlay  = "MakeOverlay";
        const String _subKeyPCL5              = "PCL5";
        const String _subKeyPCL6              = "PCL6";
        const String _subKeyPCL               = "PCL";
        const String _subKeyPCLXL             = "PCLXL";
        
        const String _namePrnFilename         = "PrintFilename";
        const String _nameOvlFilename         = "OverlayFilename";
        const String _nameMacroId             = "MacroId";
        const String _nameStreamName          = "StreamName";
        const String _nameFlagEncapsulated    = "FlagEncapsulated";
        const String _nameFlagRestoreCursor   = "FlagRestoreCursor";
        const String _nameFlagRestoreGS       = "FlagRestoreGS";
        const String _nameIndxRptFileFmt      = "IndxRptFileFmt";

        const String _defaultPrnFilename      = "DefaultPrintFile.prn";
        const String _defaultOvlFilename      = "DefaultOverlayFile";
        const String _defaultStreamName       = "Stream 001";

        const Int32 _defaultMacroId           = 101;

        const Int32 _flagFalse = 0;
        const Int32 _flagTrue  = 1;
        const Int32 _indexZero = 0;
        
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a C o m m o n                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored common data.                                       //
        // Missing items are given default values.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataCommon(ref String prnFilename)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsMakeOverlay;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                if (Helper_RegKey.keyExists(subKey, _subKeyPCL5))
                    // update from v2_5_0_0
                    Helper_RegKey.renameKey(subKey, _subKeyPCL5, _subKeyPCL);

                if (Helper_RegKey.keyExists(subKey, _subKeyPCL6))
                    // update from v2_5_0_0
                    Helper_RegKey.renameKey(subKey, _subKeyPCL6, _subKeyPCLXL);

                prnFilename = (String)subKey.GetValue(_namePrnFilename,
                                                      defWorkFolder + "\\" +
                                                      _defaultPrnFilename);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a P C L                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored PCL data.                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataPCL(ref String  ovlFilename,
                                        ref Boolean flagRestoreCursor,
                                        ref Boolean flagEncapsulated,
                                        ref Int32   macroId)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            Int32 tmpInt;

            String key;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            key = _subKeyTools + "\\" + _subKeyToolsMakeOverlay +
                                 "\\" + _subKeyPCL;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                ovlFilename  = (String) subKey.GetValue (_nameOvlFilename,
                                                         defWorkFolder + "\\" +
                                                         _defaultOvlFilename + ".ovl");
                macroId      = (Int32) subKey.GetValue (_nameMacroId,
                                                        _defaultMacroId);

                tmpInt = (Int32) subKey.GetValue (_nameFlagEncapsulated,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagEncapsulated = false;
                else
                    flagEncapsulated = true;

                tmpInt = (Int32) subKey.GetValue (_nameFlagRestoreCursor,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagRestoreCursor = false;
                else
                    flagRestoreCursor = true;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a P C L X L                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored PCLXL data.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataPCLXL(ref String ovlFilename,
                                        ref Boolean flagRestoreGS,
                                        ref Boolean flagEncapsulated,
                                        ref String streamName)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            Int32 tmpInt;

            String key;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            key = _subKeyTools + "\\" + _subKeyToolsMakeOverlay +
                                 "\\" + _subKeyPCLXL;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                ovlFilename = (String) subKey.GetValue (_nameOvlFilename,
                                                        defWorkFolder + "\\" +
                                                        _defaultOvlFilename + ".ovx");
                streamName  = (String) subKey.GetValue (_nameStreamName,
                                                        _defaultStreamName);

                tmpInt = (Int32) subKey.GetValue (_nameFlagEncapsulated,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagEncapsulated = false;
                else
                    flagEncapsulated = true;

                tmpInt = (Int32) subKey.GetValue (_nameFlagRestoreGS,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagRestoreGS = false;
                else
                    flagRestoreGS = true;
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

            String key = _subKeyTools + "\\" + _subKeyToolsMakeOverlay;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                indxRptFileFmt = (Int32)subKey.GetValue (_nameIndxRptFileFmt,
                                                         _indexZero);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a C o m m o n                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current common data.                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataCommon(String prnFilename)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsMakeOverlay;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                if (prnFilename != null)
                {
                    subKey.SetValue (_namePrnFilename,
                                    prnFilename,
                                    RegistryValueKind.String);
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a P C L                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store PCL data.                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataPCL(String ovlFilename,
                                        Boolean flagRestoreCursor,
                                        Boolean flagEncapsulated,
                                        Int32 macroId)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsMakeOverlay +
                                 "\\" + _subKeyPCL;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                if (ovlFilename != null)
                {
                    subKey.SetValue (_nameOvlFilename,
                                    ovlFilename,
                                    RegistryValueKind.String);
                }

                subKey.SetValue (_nameMacroId,
                                macroId,
                                RegistryValueKind.DWord);

                if (flagEncapsulated)
                    subKey.SetValue (_nameFlagEncapsulated,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagEncapsulated,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagRestoreCursor)
                    subKey.SetValue (_nameFlagRestoreCursor,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagRestoreCursor,
                                    _flagFalse,
                                    RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a P C L X L                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store PCLXL data.                                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataPCLXL(String ovlFilename,
                                        Boolean flagRestoreGS,
                                        Boolean flagEncapsulated,
                                        String streamName)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsMakeOverlay +
                                 "\\" + _subKeyPCLXL;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                if (ovlFilename != null)
                {
                    subKey.SetValue (_nameOvlFilename,
                                    ovlFilename,
                                    RegistryValueKind.String);
                }

                if (flagEncapsulated)
                    subKey.SetValue (_nameFlagEncapsulated,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagEncapsulated,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagRestoreGS)
                    subKey.SetValue (_nameFlagRestoreGS,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagRestoreGS,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (streamName != null)
                {
                    subKey.SetValue (_nameStreamName,
                                     streamName,
                                     RegistryValueKind.String);
                }
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

            String key = _subKeyTools + "\\" + _subKeyToolsMakeOverlay;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                subKey.SetValue (_nameIndxRptFileFmt,
                                indxRptFileFmt,
                                RegistryValueKind.DWord);
            }
        }
    }
}
