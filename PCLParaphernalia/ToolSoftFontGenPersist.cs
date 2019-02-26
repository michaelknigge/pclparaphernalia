using Microsoft.Win32;
using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class manages persistent storage of options for the Soft Font
    ///	Generate tool.
    /// 
    /// © Chris Hutchinson 2012
    /// 
    /// </summary>

    static class ToolSoftFontGenPersist
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const String _mainKey                  = MainForm._regMainKey;

        const String _subKeyTools              = "Tools";
        const String _subKeyToolsSoftFontGen   = "SoftFontGen";
        const String _subKeyMapping            = "Mapping"; 
        const String _subKeyPCL5               = "PCL5"; 
        const String _subKeyPCL6               = "PCL6"; 
        const String _subKeyPCL                = "PCL"; 
        const String _subKeyPCLXL              = "PCLXL"; 
        const String _subKeyTTF                = "TTF"; 

        const String _nameTargetFolder         = "TargetFolder";
        const String _nameAdhocFontFile        = "AdhocFontFile";
        const String _nameSymSetUserFile       = "SymSetUserFile";
        const String _nameFlagLogVerbose       = "FlagLogVerbose";
        const String _nameFlagFormat16         = "FlagFormat16";
        const String _nameFlagSegGTLast        = "FlagSegGTLast";
        const String _nameFlagSymSetMapPCL     = "FlagSymSetMapPCL";
        const String _nameFlagSymSetUnbound    = "FlagSymSetUnbound";
        const String _nameFlagSymSetUserSet    = "FlagSymSetUserSet";
        const String _nameFlagCharCompSpecific = "FlagCharCompSpecific";
        const String _nameFlagUsePCLT          = "FlagUsePCLT";
        const String _nameCharCompUnicode      = "CharCompUnicode";
        const String _nameFlagVMetrics         = "FlagVMetrics";
        const String _nameIndxFont             = "IndxFont";
        const String _nameIndxPDL              = "IndxPDL";
        const String _nameIndxRptFileFmt       = "IndxRptFileFmt";
        const String _nameIndxRptChkMarks      = "indxRptChkMarks";
        const String _nameIndxSymSet           = "IndxSymSet";
        const String _nameIndxUsePCLT          = "IndxUsePCLT";

        const Int32 _flagFalse                 = 0;
        const Int32 _flagTrue                  = 1;
        const Int32 _indexZero                 = 0;
        const Int64 _defaultCompUnicode        = -2;    // 0xfffffffffffffffe //

        const String _defaultSymSetUserFile    = "DefaultSymSetFile.pcl";
        const String _defaultFontFileTTF       = "DefaultFontFile.ttf";

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a C o m m o n                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored FontSample common data.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataCommon (ref Int32   indxPDL,
                                           ref Boolean flagLogVerbose)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsSoftFontGen;

            Int32 tmpInt;

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

                tmpInt          = (Int32)subKey.GetValue(_nameFlagLogVerbose,
                                                         _flagTrue);

                if (tmpInt == _flagFalse)
                    flagLogVerbose = false;
                else
                    flagLogVerbose = true;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a M a p p i n g                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored Soft Font Generate mapping data.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataMapping (ref Int32   indxSymSet,
                                            ref Boolean flagSymSetMapPCL,
                                            ref Boolean flagSymSetUnbound,
                                            ref Boolean flagSymSetUserSet,
                                            ref String  symSetUserFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            Int32 tmpInt;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            key = _subKeyTools + "\\" + _subKeyToolsSoftFontGen +
                                 "\\" + _subKeyMapping;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                indxSymSet      = (Int32)subKey.GetValue(_nameIndxSymSet,
                                                         _indexZero);

                tmpInt = (Int32) subKey.GetValue (_nameFlagSymSetMapPCL,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagSymSetMapPCL = false;
                else
                    flagSymSetMapPCL = true;

                tmpInt = (Int32) subKey.GetValue (_nameFlagSymSetUnbound,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagSymSetUnbound = false;
                else
                    flagSymSetUnbound = true;

                tmpInt = (Int32) subKey.GetValue (_nameFlagSymSetUserSet,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagSymSetUserSet = false;
                else
                    flagSymSetUserSet = true;

                symSetUserFile = (String)subKey.GetValue (_nameSymSetUserFile,
                                                          defWorkFolder + "\\" +
                                                          _defaultSymSetUserFile);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a P C L                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored Soft Font Generate PCL data.                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataPCL (ref String  targetFolder,
                                        ref Boolean flagFormat16,
                                        ref Boolean flagCharCompSpecific,
                                        ref Boolean flagVMetrics,
                                        ref Boolean flagSegGTLast,
                                        ref UInt64  charCompUnicode)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            Int32 tmpInt;
            Int64 tmpInt64;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            key = _subKeyTools + "\\" + _subKeyToolsSoftFontGen +
                                 "\\" + _subKeyPCL; 
                  
            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                // update from v2_8_0_0 begin //

                if (subKey.GetValue(_nameIndxUsePCLT) != null)
                    subKey.DeleteValue(_nameIndxUsePCLT);

                // update from v2_8_0_0 end   //

                targetFolder = (String)subKey.GetValue(_nameTargetFolder,
                                                       defWorkFolder);
                
                tmpInt = (Int32) subKey.GetValue (_nameFlagLogVerbose,
                                                         _flagTrue);

                if (tmpInt == _flagFalse)
                    flagFormat16 = false;
                else
                    flagFormat16 = true;

                tmpInt = (Int32)subKey.GetValue (_nameFlagCharCompSpecific,
                                                 _flagFalse);

                if (tmpInt == _flagFalse)
                    flagCharCompSpecific = false;
                else
                    flagCharCompSpecific = true;

                tmpInt = (Int32) subKey.GetValue (_nameFlagVMetrics,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagVMetrics = false;
                else
                    flagVMetrics = true;

                tmpInt = (Int32)subKey.GetValue(_nameFlagSegGTLast,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagSegGTLast = false;
                else
                    flagSegGTLast = true;

                tmpInt64 = (Int64)subKey.GetValue (_nameCharCompUnicode,
                                                   _defaultCompUnicode);

                charCompUnicode = (UInt64) tmpInt64;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a P C L X L                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored Soft Font Generate PCLXL data.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataPCLXL (ref String targetFolder,
                                          ref Boolean flagVMetrics)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key;

            Int32 tmpInt;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            key = _subKeyTools + "\\" + _subKeyToolsSoftFontGen +
                                 "\\" + _subKeyPCLXL;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                // update from v2_8_0_0 begin //

                if (subKey.GetValue(_nameIndxUsePCLT) != null)
                    subKey.DeleteValue(_nameIndxUsePCLT);

                // update from v2_8_0_0 end   //

                targetFolder = (String) subKey.GetValue (_nameTargetFolder,
                                                         defWorkFolder);

                tmpInt = (Int32)subKey.GetValue (_nameFlagVMetrics,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagVMetrics = false;
                else
                    flagVMetrics = true;
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

        public static void loadDataRpt (ref Int32 indxRptFileFmt,
                                        ref Int32 indxRptChkMarks)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsSoftFontGen;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                indxRptFileFmt = (Int32)subKey.GetValue (_nameIndxRptFileFmt,
                                                         _indexZero);

                indxRptChkMarks = (Int32)subKey.GetValue (_nameIndxRptChkMarks,
                                                          _indexZero);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a T T F                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored Soft Font Generate TTF data.                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataTTF (ref Int32   indxFont,
                                        ref Boolean flagUsePCLT,
                                        ref string  adhocFontFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key;

            Int32 tmpInt;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            key = _subKeyTools + "\\" + _subKeyToolsSoftFontGen +
                                 "\\" + _subKeyTTF;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                indxFont   = (Int32) subKey.GetValue (_nameIndxFont,
                                                      _indexZero);

                tmpInt = (Int32)subKey.GetValue(_nameFlagUsePCLT,
                                                _flagTrue);

                if (tmpInt == _flagFalse)
                    flagUsePCLT = false;
                else
                    flagUsePCLT = true;

                adhocFontFile = (String)subKey.GetValue (_nameAdhocFontFile,
                                                         defWorkFolder + "\\" +
                                                         _defaultFontFileTTF);
            }
        }
        
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a C o m m o n                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current Soft Font Generate common data.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataCommon(Int32 indxPDL,
                                          Boolean flagLogVerbose)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsSoftFontGen;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue(_nameIndxPDL,
                                indxPDL,
                                RegistryValueKind.DWord);
                
                if (flagLogVerbose)
                    subKey.SetValue (_nameFlagLogVerbose,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagLogVerbose,
                                    _flagFalse,
                                    RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a M a p p i n g                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current Soft Font Generate mapping data.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataMapping (Int32 indxSymSet,
                                            Boolean flagSymSetMapPCL,
                                            Boolean flagSymSetUnbound,
                                            Boolean flagSymSetUserSet,
                                            String  symSetUserFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);
            
            String key;

            key = _subKeyTools + "\\" + _subKeyToolsSoftFontGen +
                                 "\\" + _subKeyMapping;
            
            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue(_nameIndxSymSet,
                                indxSymSet,
                                RegistryValueKind.DWord);

                if (flagSymSetMapPCL)
                    subKey.SetValue (_nameFlagSymSetMapPCL,
                                     _flagTrue,
                                     RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagSymSetMapPCL,
                                     _flagFalse,
                                     RegistryValueKind.DWord);

                if (flagSymSetUnbound)
                    subKey.SetValue (_nameFlagSymSetUnbound,
                                     _flagTrue,
                                     RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagSymSetUnbound,
                                     _flagFalse,
                                     RegistryValueKind.DWord);

                if (flagSymSetUserSet)
                    subKey.SetValue (_nameFlagSymSetUserSet,
                                     _flagTrue,
                                     RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagSymSetUserSet,
                                     _flagFalse,
                                     RegistryValueKind.DWord);

                if (symSetUserFile != null)
                {
                    subKey.SetValue (_nameSymSetUserFile,
                                     symSetUserFile,
                                     RegistryValueKind.String);
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a P C L                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store Soft Font Generate PCL data.                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataPCL(String  targetFolder,
                                        Boolean flagFormat16,
                                        Boolean flagCharCompSpecific,
                                        Boolean flagVMetrics,
                                        Boolean flagSegGTLast,
                                        UInt64 charCompUnicode)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            Int64 tmpInt64;

            key = _subKeyTools + "\\" + _subKeyToolsSoftFontGen +
                                 "\\" + _subKeyPCL;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                if (targetFolder != null)
                {
                    subKey.SetValue (_nameTargetFolder,
                                    targetFolder,
                                    RegistryValueKind.String);
                }

                if (flagFormat16)
                    subKey.SetValue (_nameFlagFormat16,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagFormat16,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagCharCompSpecific)
                    subKey.SetValue (_nameFlagCharCompSpecific,
                                     _flagTrue,
                                     RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagCharCompSpecific,
                                     _flagFalse,
                                     RegistryValueKind.DWord);

                if (flagVMetrics)
                    subKey.SetValue (_nameFlagVMetrics,
                                     _flagTrue,
                                     RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagVMetrics,
                                     _flagFalse,
                                     RegistryValueKind.DWord);

                if (flagSegGTLast)
                    subKey.SetValue(_nameFlagSegGTLast,
                                     _flagTrue,
                                     RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagSegGTLast,
                                     _flagFalse,
                                     RegistryValueKind.DWord);

                tmpInt64 = (Int64)charCompUnicode;

                subKey.SetValue (_nameCharCompUnicode,
                                 tmpInt64,
                                 RegistryValueKind.QWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a P C L X L                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store Soft Font Generate PCLXL data.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataPCLXL (String  targetFolder,
                                         Boolean flagVMetrics)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsSoftFontGen +
                                 "\\" + _subKeyPCLXL;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                if (targetFolder != null)
                {
                    subKey.SetValue (_nameTargetFolder,
                                    targetFolder,
                                    RegistryValueKind.String);
                }

                if (flagVMetrics)
                    subKey.SetValue (_nameFlagVMetrics,
                                     _flagTrue,
                                     RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagVMetrics,
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

        public static void saveDataRpt (Int32 indxRptFileFmt,
                                        Int32 indxRptChkMarks)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsSoftFontGen;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                subKey.SetValue (_nameIndxRptFileFmt,
                                indxRptFileFmt,
                                RegistryValueKind.DWord);

                subKey.SetValue (_nameIndxRptChkMarks,
                                indxRptChkMarks,
                                RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a T T F                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store Soft Font Generate TTF data.                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataTTF (Int32   indxFont,
                                        Boolean flagUsePCLT,
                                        String  adhocFontFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsSoftFontGen +
                                 "\\" + _subKeyTTF;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue(_nameIndxFont,
                                indxFont,
                                RegistryValueKind.DWord);

                if (flagUsePCLT)
                    subKey.SetValue (_nameFlagUsePCLT,
                                     _flagTrue,
                                     RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagUsePCLT,
                                     _flagFalse,
                                     RegistryValueKind.DWord);

                if (adhocFontFile != null)
                {
                    subKey.SetValue (_nameAdhocFontFile,
                                     adhocFontFile,
                                     RegistryValueKind.String);
                }
            }
        }
    }
}
