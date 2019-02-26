using Microsoft.Win32;
using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class manages persistent storage of options for the PrinterInfo tool.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class ToolPrintLangPersist
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        const String _mainKey                 = MainForm._regMainKey;

        const String _subKeyTools             = "Tools";
        const String _subKeyToolsPDLData      = "PdlData";
        const String _subKeyToolsPrintLang    = "PrintLang";
        const String _subKeyPCL               = "PCL";
        const String _subKeyPCLXL             = "PCLXL"; 
        const String _subKeyPML               = "PML";
        const String _subKeySymSets           = "SymSets";
        const String _subKeyFonts             = "Fonts";

        const String _nameIndxInfoType        = "IndxInfoType";
        const String _nameReportFile          = "ReportFile";
        const String _nameFlagOptDiscrete     = "FlagOptDiscrete";
        const String _nameFlagOptMapping      = "FlagOptMapping";
        const String _nameFlagOptMapDuo       = "FlagOptMapDuo";
        const String _nameFlagOptObsolete     = "FlagOptObsolete";
        const String _nameFlagOptReserved     = "FlagOptReserved";
        const String _nameFlagOptRptWrap      = "FlagOptRptWrap";
        const String _nameFlagSeqControl      = "FlagSeqControl";
        const String _nameFlagSeqComplex      = "FlagSeqComplex";
        const String _nameFlagSeqSimple       = "FlagSeqSimple";
        const String _nameFlagTagAction       = "FlagTagAction";
        const String _nameFlagTagAttrDefiner  = "FlagTagAttrDefiner";
        const String _nameFlagTagAttribute    = "FlagTagAttribute";
        const String _nameFlagTagDataType     = "FlagTagDataType";
        const String _nameFlagTagEmbedDataDef = "FlagTagEmbedDataDef";
        const String _nameFlagTagOperator     = "FlagTagOperator";
        const String _nameFlagTagOutcome      = "FlagTagOutcome";
        const String _nameFlagTagWhitespace   = "FlagTagWhitespace";
        const String _nameIndxRptFileFmt      = "IndxRptFileFmt";
        const String _nameIndxRptChkMarks     = "IndxRptChkMarks";
        const String _nameSymSetMapType       = "SymSetMapType";

        const Int32 _flagFalse                = 0;
        const Int32 _flagTrue                 = 1;
        const Int32 _indexZero                = 0;

        const String _defaultFilename         = "DefaultPDLReportFile.txt";

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a C o m m o n                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored PDLData common data.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataCommon(ref Int32  indxInfoType,
                                          ref String reportFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsPrintLang;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            using (RegistryKey subKey = keyMain.CreateSubKey(_subKeyTools))
            {
                if (Helper_RegKey.keyExists(subKey, _subKeyToolsPDLData))
                    // update from v2_5_0_0
                    Helper_RegKey.renameKey(subKey,
                                           _subKeyToolsPDLData,
                                           _subKeyToolsPrintLang);
            }

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                indxInfoType = (Int32)subKey.GetValue(_nameIndxInfoType,
                                                      _indexZero);

                reportFile = (String) subKey.GetValue(_nameReportFile,
                                                      defWorkFolder + "\\" +
                                                      _defaultFilename);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a F o n t s                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored PDLData Fonts data.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataFonts (ref Boolean flagOptMap)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            Int32 tmpInt;

            key = _subKeyTools + "\\" + _subKeyToolsPrintLang +
                                 "\\" + _subKeyFonts;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                tmpInt = (Int32)subKey.GetValue(_nameFlagOptMapping,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagOptMap = false;
                else
                    flagOptMap = true;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a P C L                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored PDLData PCL data.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataPCL(ref Boolean flagSeqControl,
                                       ref Boolean flagSeqSimple,
                                       ref Boolean flagSeqComplex,
                                       ref Boolean flagOptObsolete,
                                       ref Boolean flagOptDiscrete)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            Int32 tmpInt;

            key = _subKeyTools + "\\" + _subKeyToolsPrintLang +
                                 "\\" + _subKeyPCL;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                tmpInt = (Int32)subKey.GetValue(_nameFlagSeqControl,
                                                _flagTrue);

                if (tmpInt == _flagFalse)
                    flagSeqControl = false;
                else
                    flagSeqControl = true;

                tmpInt = (Int32) subKey.GetValue(_nameFlagSeqSimple,
                                                _flagTrue);

                if (tmpInt == _flagFalse)
                    flagSeqSimple = false;
                else
                    flagSeqSimple = true;

                tmpInt = (Int32) subKey.GetValue(_nameFlagSeqComplex,
                                                _flagTrue);

                if (tmpInt == _flagFalse)
                    flagSeqComplex = false;
                else
                    flagSeqComplex = true;

                tmpInt = (Int32) subKey.GetValue(_nameFlagOptObsolete,
                                                _flagTrue);

                if (tmpInt == _flagFalse)
                    flagOptObsolete = false;
                else
                    flagOptObsolete = true;

                tmpInt = (Int32) subKey.GetValue(_nameFlagOptDiscrete,
                                                _flagTrue);

                if (tmpInt == _flagFalse)
                    flagOptDiscrete = false;
                else
                    flagOptDiscrete = true;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a P C L X L                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored PDLData PCLXL state.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataPCLXL(ref Boolean flagTagDataType,
                                         ref Boolean flagTagAttribute,
                                         ref Boolean flagTagOperator,
                                         ref Boolean flagTagAttrDefiner,
                                         ref Boolean flagTagEmbedDataDef,
                                         ref Boolean flagTagWhitespace,
                                         ref Boolean flagOptReserved)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            Int32 tmpInt;

            key = _subKeyTools + "\\" + _subKeyToolsPrintLang +
                                 "\\" + _subKeyPCLXL;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                tmpInt = (Int32) subKey.GetValue(_nameFlagTagDataType,
                                                _flagTrue);

                if (tmpInt == _flagFalse)
                    flagTagDataType = false;
                else
                    flagTagDataType = true;

                tmpInt = (Int32) subKey.GetValue(_nameFlagTagAttribute,
                                                _flagTrue);

                if (tmpInt == _flagFalse)
                    flagTagAttribute = false;
                else
                    flagTagAttribute = true;

                tmpInt = (Int32) subKey.GetValue(_nameFlagTagOperator,
                                                _flagTrue);

                if (tmpInt == _flagFalse)
                    flagTagOperator = false;
                else
                    flagTagOperator = true;

                tmpInt = (Int32) subKey.GetValue(_nameFlagTagAttrDefiner,
                                                _flagTrue);

                if (tmpInt == _flagFalse)
                    flagTagAttrDefiner = false;
                else
                    flagTagAttrDefiner = true;

                tmpInt = (Int32) subKey.GetValue(_nameFlagTagEmbedDataDef,
                                                _flagTrue);

                if (tmpInt == _flagFalse)
                    flagTagEmbedDataDef = false;
                else
                    flagTagEmbedDataDef = true;

                tmpInt = (Int32) subKey.GetValue(_nameFlagTagWhitespace,
                                                _flagTrue);

                if (tmpInt == _flagFalse)
                    flagTagWhitespace = false;
                else
                    flagTagWhitespace = true;

                tmpInt = (Int32) subKey.GetValue(_nameFlagOptReserved,
                                                _flagTrue);

                if (tmpInt == _flagFalse)
                    flagOptReserved = false;
                else
                    flagOptReserved = true;

            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a P M L                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored PDLData PML state.                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataPML (ref Boolean flagTagDataType,
                                        ref Boolean flagTagAction,
                                        ref Boolean flagTagOutcome)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key;

            Int32 tmpInt;

            key = _subKeyTools + "\\" + _subKeyToolsPrintLang +
                                 "\\" + _subKeyPML;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                tmpInt = (Int32) subKey.GetValue (_nameFlagTagDataType,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagTagDataType = false;
                else
                    flagTagDataType = true;

                tmpInt = (Int32) subKey.GetValue (_nameFlagTagAction,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagTagAction = false;
                else
                    flagTagAction = true;

                tmpInt = (Int32) subKey.GetValue (_nameFlagTagOutcome,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagTagOutcome = false;
                else
                    flagTagOutcome = true;
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

        public static void loadDataRpt (ref Int32   indxRptFileFmt,
                                        ref Int32   indxRptChkMarks, 
                                        ref Boolean flagOptRptWrap)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsPrintLang;

            Int32 tmpInt;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                indxRptFileFmt = (Int32)subKey.GetValue (_nameIndxRptFileFmt,
                                                         _indexZero);

                indxRptChkMarks = (Int32)subKey.GetValue (_nameIndxRptChkMarks,
                                                          _indexZero);

                tmpInt = (Int32)subKey.GetValue (_nameFlagOptRptWrap,
                                _flagTrue);

                if (tmpInt == _flagFalse)
                    flagOptRptWrap = false;
                else
                    flagOptRptWrap = true;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a S y m S e t s                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored PDLData Symbol Set data.                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataSymSets(ref Boolean flagOptMap,
                                           ref Int32   mapType)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key;

            Int32 tmpInt;

            key = _subKeyTools + "\\" + _subKeyToolsPrintLang +
                                 "\\" + _subKeySymSets;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                tmpInt = (Int32) subKey.GetValue (_nameFlagOptMapping,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagOptMap = false;
                else
                    flagOptMap = true;

                mapType = (Int32) subKey.GetValue (_nameSymSetMapType,
                                                   _indexZero);
            }
        }
 
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a C o m m o n                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current PDLData common data.                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataCommon(Int32  indxInfoType,
                                          String reportFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsPrintLang;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue(_nameIndxInfoType,
                                indxInfoType,
                                RegistryValueKind.DWord);

                if (reportFile != null)
                {
                    subKey.SetValue (_nameReportFile,
                                    reportFile,
                                    RegistryValueKind.String);
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a F o n t s                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current PDLData Fonts data.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataFonts (Boolean flagOptMap)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsPrintLang +
                                 "\\" + _subKeyFonts;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                if (flagOptMap)
                    subKey.SetValue(_nameFlagOptMapping,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagOptMapping,
                                    _flagFalse,
                                    RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a P C L                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current PDLData PCL data.                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataPCL(Boolean flagSeqControl,
                                       Boolean flagSeqSimple,
                                       Boolean flagSeqComplex,
                                       Boolean flagOptObsolete,
                                       Boolean flagOptDiscrete)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsPrintLang +
                                 "\\" + _subKeyPCL;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                if (flagSeqControl)
                    subKey.SetValue(_nameFlagSeqControl,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagSeqControl,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagSeqSimple)
                    subKey.SetValue(_nameFlagSeqSimple,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagSeqSimple,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagSeqComplex)
                    subKey.SetValue(_nameFlagSeqComplex,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagSeqComplex,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagOptObsolete)
                    subKey.SetValue(_nameFlagOptObsolete,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagOptObsolete,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagOptDiscrete)
                    subKey.SetValue(_nameFlagOptDiscrete,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagOptDiscrete,
                                    _flagFalse,
                                    RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a P C L X L                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current PDLData PCLXL data.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataPCLXL(Boolean flagTagDataType,
                                         Boolean flagTagAttribute,
                                         Boolean flagTagOperator,
                                         Boolean flagTagAttrDefiner,
                                         Boolean flagTagEmbedDataDef,
                                         Boolean flagTagWhitespace,
                                         Boolean flagOptReserved)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsPrintLang +
                                 "\\" + _subKeyPCLXL;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                if (flagTagDataType)
                    subKey.SetValue(_nameFlagTagDataType,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagTagDataType,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagTagAttribute)
                    subKey.SetValue(_nameFlagTagAttribute,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagTagAttribute,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagTagOperator)
                    subKey.SetValue(_nameFlagTagOperator,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagTagOperator,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagTagAttrDefiner)
                    subKey.SetValue(_nameFlagTagAttrDefiner,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagTagAttrDefiner,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagTagEmbedDataDef)
                    subKey.SetValue(_nameFlagTagEmbedDataDef,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagTagEmbedDataDef,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagTagWhitespace)
                    subKey.SetValue(_nameFlagTagWhitespace,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagTagWhitespace,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagOptReserved)
                    subKey.SetValue(_nameFlagOptReserved,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagOptReserved,
                                    _flagFalse,
                                    RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a P M L                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current PDLData PML data.                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataPML (Boolean flagTagDataType,
                                        Boolean flagTagAction,
                                        Boolean flagTagOutcome)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsPrintLang +
                                 "\\" + _subKeyPML;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                if (flagTagDataType)
                    subKey.SetValue (_nameFlagTagDataType,
                                     _flagTrue,
                                     RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagTagDataType,
                                     _flagFalse,
                                     RegistryValueKind.DWord);

                if (flagTagAction)
                    subKey.SetValue (_nameFlagTagAction,
                                     _flagTrue,
                                     RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagTagAction,
                                     _flagFalse,
                                     RegistryValueKind.DWord);

                if (flagTagOutcome)
                    subKey.SetValue (_nameFlagTagOutcome,
                                     _flagTrue,
                                     RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagTagOutcome,
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

        public static void saveDataRpt (Int32   indxRptFileFmt,
                                        Int32   indxRptChkMarks,
                                        Boolean flagOptRptWrap)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsPrintLang;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                subKey.SetValue (_nameIndxRptFileFmt,
                                indxRptFileFmt,
                                RegistryValueKind.DWord);

                subKey.SetValue (_nameIndxRptChkMarks,
                                indxRptChkMarks,
                                RegistryValueKind.DWord);

                if (flagOptRptWrap)
                    subKey.SetValue (_nameFlagOptRptWrap,
                                     _flagTrue,
                                     RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagOptRptWrap,
                                     _flagFalse,
                                     RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a S y m S e t s                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current PDLData Symbol Set data.                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataSymSets(Boolean flagOptMap,
                                           Int32   mapType)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsPrintLang +
                                 "\\" + _subKeySymSets;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                if (flagOptMap)
                    subKey.SetValue (_nameFlagOptMapping,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagOptMapping,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                subKey.SetValue (_nameSymSetMapType,
                                 mapType,
                                 RegistryValueKind.DWord);
            }
        }
    }
}
