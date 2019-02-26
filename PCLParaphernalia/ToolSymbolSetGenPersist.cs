using Microsoft.Win32;
using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class manages persistent storage of options for the SymbolSetGenerate
    /// tool.
    /// 
    /// © Chris Hutchinson 2013
    /// 
    /// </summary>

    static class ToolSymbolSetGenPersist
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const String _mainKey                 = MainForm._regMainKey;

        const String _subKeyTools             = "Tools";
        const String _subKeyToolsSymSetGen    = "SymSetGen";
        const String _subKeyDonor             = "Donor"; 
        const String _subKeyTarget            = "Target"; 

        const String _nameFlagSymSetUserSet   = "FlagSymSetUserSet";
        const String _nameFlagSymSetMapPCL    = "FlagSymSetMapPCL";
        const String _nameFlagIgnoreC0        = "FlagIgnoreC0";
        const String _nameFlagIgnoreC1        = "FlagIgnoreC1";
        const String _nameFlagMapHex          = "FlagMapHex";
        const String _nameFlagIndexUnicode    = "FlagIndexUnicode";
        const String _nameFlagCharReqSpecific = "FlagCharReqSpecific";
        const String _nameCharReqMSL          = "CharReqMSL";
        const String _nameCharReqUnicode      = "CharReqUnicode";
        const String _nameIndxRptFileFmt      = "IndxRptFileFmt";
        const String _nameIndxSymSet          = "IndxSymSet";
        const String _nameSymSetFile          = "SymSetFile";
        const String _nameSymSetFolder        = "SymSetFolder";

        const Int32  _flagFalse               = 0;
        const Int32  _flagTrue                = 1;
        const Int32  _indexZero               = 0;
        const Int64  _defaultReqMSL           = 0;
        const Int64  _defaultReqUnicode       = 1;

        const String _defaultSymSetFile       = "DefaultSymSetFile.pcl";  

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a D o n o r                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored SymbolSetGenerate data for donor.                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataDonor (ref Int32   indxSymSet,
                                          ref Boolean flagSymSetUserSet, 
                                          ref Boolean flagSymSetMapPCL, 
                                          ref String  symSetFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            Int32 tmpInt;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            key = _subKeyTools + "\\" + _subKeyToolsSymSetGen +
                                 "\\" + _subKeyDonor; 
                  
            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                indxSymSet   = (Int32) subKey.GetValue(_nameIndxSymSet,
                                                       _indexZero);
                 
                tmpInt       = (Int32) subKey.GetValue(_nameFlagSymSetUserSet,
                                                       _flagFalse);

                if (tmpInt == _flagFalse)
                    flagSymSetUserSet = false;
                else
                    flagSymSetUserSet = true;
                 
                tmpInt       = (Int32) subKey.GetValue(_nameFlagSymSetMapPCL,
                                                       _flagFalse);

                if (tmpInt == _flagFalse)
                    flagSymSetMapPCL = false;
                else
                    flagSymSetMapPCL = true;

                symSetFile = (String)subKey.GetValue (_nameSymSetFile,
                                                      defWorkFolder + "\\" +
                                                      _defaultSymSetFile);
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

            String key = _subKeyTools + "\\" + _subKeyToolsSymSetGen;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                indxRptFileFmt = (Int32)subKey.GetValue (_nameIndxRptFileFmt,
                                                         _indexZero);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a T a r g e t                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored SymbolSetGenerate data for target.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataTarget (ref Boolean flagMapHex,
                                           ref Boolean flagIgnoreC0,
                                           ref Boolean flagIgnoreC1,
                                           ref Boolean flagIndexUnicode,
                                           ref Boolean flagCharReqSpecific,
                                           ref UInt64  charReqUnicode,
                                           ref UInt64  charReqMSL,
                                           ref String  symSetFolder)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key;

            Int32 tmpInt;
            Int64 tmpInt64;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            key = _subKeyTools + "\\" + _subKeyToolsSymSetGen +
                                 "\\" + _subKeyTarget;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                tmpInt = (Int32)subKey.GetValue (_nameFlagMapHex,
                                                       _flagTrue);

                if (tmpInt == _flagFalse)
                    flagMapHex = false;
                else
                    flagMapHex = true;

                tmpInt = (Int32)subKey.GetValue (_nameFlagIgnoreC0,
                                                       _flagTrue);

                if (tmpInt == _flagFalse)
                    flagIgnoreC0 = false;
                else
                    flagIgnoreC0 = true;

                tmpInt = (Int32)subKey.GetValue (_nameFlagIgnoreC1,
                                                       _flagFalse);

                if (tmpInt == _flagFalse)
                    flagIgnoreC1 = false;
                else
                    flagIgnoreC1 = true;

                tmpInt = (Int32)subKey.GetValue (_nameFlagIndexUnicode,
                                                       _flagTrue);

                if (tmpInt == _flagFalse)
                    flagIndexUnicode = false;
                else
                    flagIndexUnicode = true;

                tmpInt = (Int32)subKey.GetValue (_nameFlagCharReqSpecific,
                                                       _flagFalse);

                if (tmpInt == _flagFalse)
                    flagCharReqSpecific = false;
                else
                    flagCharReqSpecific = true;

                tmpInt64      = (Int64)subKey.GetValue (_nameCharReqUnicode,
                                                        _defaultReqUnicode);

                charReqUnicode = (UInt64) tmpInt64;

                tmpInt64       = (Int64) subKey.GetValue (_nameCharReqMSL,
                                                        _defaultReqMSL);
                charReqMSL     = (UInt64) tmpInt64;
                
                symSetFolder = (String)subKey.GetValue (_nameSymSetFolder,
                                                        defWorkFolder);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a D o n o r                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current SymbolSetGenerate data for donor.                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataDonor (Int32   indxSymSet,
                                          Boolean flagSymSetUserSet, 
                                          Boolean flagSymSetMapPCL, 
                                          String  symSetFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);
            
            String key;

            key = _subKeyTools + "\\" + _subKeyToolsSymSetGen +
                                 "\\" + _subKeyDonor; 
            
            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue (_nameIndxSymSet,
                                indxSymSet,
                                RegistryValueKind.DWord);

                if (flagSymSetUserSet)
                    subKey.SetValue(_nameFlagSymSetUserSet,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagSymSetUserSet,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagSymSetMapPCL)
                    subKey.SetValue(_nameFlagSymSetMapPCL,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagSymSetMapPCL,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (symSetFile != null)
                {
                    subKey.SetValue (_nameSymSetFile,
                                     symSetFile,
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

            String key = _subKeyTools + "\\" + _subKeyToolsSymSetGen;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                subKey.SetValue (_nameIndxRptFileFmt,
                                indxRptFileFmt,
                                RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a T a r g e t                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current SymbolSetGenerate data for target.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataTarget (Boolean flagMapHex,
                                           Boolean flagIgnoreC0,
                                           Boolean flagIgnoreC1,
                                           Boolean flagIndexUnicode,
                                           Boolean flagCharReqSpecific,
                                           UInt64  charReqUnicode,
                                           UInt64  charReqMSL,
                                           String  symSetFolder)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key;

            Int64 tmpInt64;

            key = _subKeyTools + "\\" + _subKeyToolsSymSetGen +
                                 "\\" + _subKeyTarget;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                if (flagMapHex)
                    subKey.SetValue (_nameFlagMapHex,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagMapHex,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagIgnoreC0)
                    subKey.SetValue (_nameFlagIgnoreC0,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagIgnoreC0,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagIgnoreC1)
                    subKey.SetValue (_nameFlagIgnoreC1,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagIgnoreC1,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagIndexUnicode)
                    subKey.SetValue (_nameFlagIndexUnicode,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagIndexUnicode,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagCharReqSpecific)
                    subKey.SetValue (_nameFlagCharReqSpecific,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagCharReqSpecific,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                tmpInt64 = (Int64)charReqUnicode;

                subKey.SetValue (_nameCharReqUnicode,
                                 tmpInt64,
                                 RegistryValueKind.QWord);

                tmpInt64 = (Int64)charReqMSL;

                subKey.SetValue (_nameCharReqMSL,
                                 tmpInt64,
                                 RegistryValueKind.QWord);

                if (symSetFolder != null)
                {
                    subKey.SetValue (_nameSymSetFolder,
                                     symSetFolder,
                                     RegistryValueKind.String);
                }
            }
        }
    }
}
