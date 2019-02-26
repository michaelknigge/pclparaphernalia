using Microsoft.Win32;
using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class manages persistent storage of options for the AnalysePRN tool.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class ToolPrnAnalysePersist
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const String _mainKey                   = MainForm._regMainKey;

        const String _subKeyTools               = "Tools";
        const String _subKeyToolsAnalyse        = "PrnAnalyse";
        const String _subKeyOptCharSet          = "OptCharSet"; 
        const String _subKeyOptClrMap           = "OptClrMap"; 
        const String _subKeyOptGeneral          = "OptGeneral"; 
        const String _subKeyOptHPGL2            = "OptHPGL2"; 
        const String _subKeyOptPCL              = "OptPCL"; 
        const String _subKeyOptPCLXL            = "OptPCLXL"; 
        const String _subKeyOptPML              = "OptPML"; 
        const String _subKeyOptStats            = "OptStats"; 

        const String _subKeyCrnt                = "Crnt";
        const String _subKeyUserThemeRoot       = "UserTheme_";

        const String _nameFilename              = "Filename";
        const String _nameTheme                 = "Name";
        const String _nameFlagAutoAnalyse       = "FlagAutoAnalyse";
        const String _nameFlagDiagFileAccess    = "FlagDiagFileAccess";
        const String _nameFlagBinData           = "FlagBinData";
        
        const String _nameFlagAlphaNumId        = "FlagAlphaNumId";
        const String _nameFlagColourLookup      = "FlagColourLookup";
        const String _nameFlagConfIO            = "FlagConfIO";
        const String _nameFlagConfImageData     = "FlagCIData";
        const String _nameFlagConfRasterData    = "FlagCRData";
        const String _nameFlagDefLogPage        = "FlagLogPageData";
        const String _nameFlagDefSymSet         = "FlagDefSymSet";
        const String _nameFlagDitherMatrix      = "FlagDitherMatrix";
        const String _nameFlagDriverConf        = "FlagDriverConf";
        const String _nameFlagEscEncText        = "FlagEscEncText";
        const String _nameFlagPaletteConf       = "FlagPaletteConf";
        const String _nameFlagUserPattern       = "FlagUserPattern";
        const String _nameFlagViewIlluminant    = "FlagViewIlluminant";

        const String _nameFlagClrMapUseClr      = "FlagUseClr";
        const String _nameFlagMacroDisplay      = "FlagMacroDisplay";
        const String _nameFlagOperPos           = "FlagOperPos";
        const String _nameFlagPCLFontSelect     = "FlagPCLFontSelect";
        const String _nameFlagPCLPassThrough    = "FlagPCLPassThrough";
        const String _nameFlagWithinPCL         = "FlagWithinPCL";
        const String _nameFlagWithinPJL         = "FlagWithinPJL";
        const String _nameFlagFontChar          = "FlagFontChar";
        const String _nameFlagFontHddr          = "FlagFontHddr";
        const String _nameFlagFontDraw          = "FlagFontDraw";
        const String _nameFlagExcUnusedPCLObs   = "FlagExcUnusedPCLObs";
        const String _nameFlagExcUnusedPCLXLRes = "FlagExcUnusedPCLXLRes";
        const String _nameFlagStyleData         = "FlagStyleData";
        const String _nameFlagUserStream        = "FlagUserStream";
        const String _nameFlagVerbose           = "FlagVerbose";

        const String _nameFontDrawHeight        = "FontDrawHeight";
        const String _nameFontDrawWidth         = "FontDrawWidth";
        const String _nameIndxClrMapRootBack    = "IndxBack_";
        const String _nameIndxClrMapRootFore    = "IndxFore_";
        const String _nameIndxName              = "IndxName";
        const String _nameIndxOffsetType        = "IndxOffsetType";
        const String _nameIndxRptFileFmt        = "IndxRptFileFmt";
        const String _nameIndxStatsLevel        = "IndxStatsLevel";
        const String _nameIndxSubAct            = "IndxSubAct";
        const String _nameIndxSubCode           = "IndxSubCode";

        const String _defaultFilename           = "DefaultPrintFile.prn";
        const String _defaultThemeName          = "NoName";

        const Int32 _defaultSubCode         = 191;

        const Int32 _flagFalse = 0;
        const Int32 _flagTrue = 1;
        const Int32 _indexZero = 0;
        
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored print file data.                                   //
        // Missing items are given default values.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadData(ref String filename)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsAnalyse;

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

            String key = _subKeyTools + "\\" + _subKeyToolsAnalyse;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                indxRptFileFmt = (Int32)subKey.GetValue (_nameIndxRptFileFmt,
                                                         _indexZero);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d O p t C h a r S e t                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored 'Character Set' options.                           //
        // Missing items are given default values.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadOptCharSet(ref Int32 indxName,
                                          ref Int32 indxSubAct,
                                          ref Int32 indxSubCode)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsAnalyse +
                                        "\\" + _subKeyOptCharSet;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                indxName        = (Int32) subKey.GetValue (_nameIndxName,
                                                           _indexZero);
                indxSubAct      = (Int32) subKey.GetValue (_nameIndxSubAct,
                                                           _indexZero);
                indxSubCode = (Int32) subKey.GetValue (_nameIndxSubCode,
                                                           _defaultSubCode);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d O p t C l r M a p                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored 'colour map' options.                              //
        // Missing items are given default values.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadOptClrMap (ref Boolean  flagClrMapUseClr)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsAnalyse +
                                        "\\" + _subKeyOptClrMap;

            Int32 tmpInt;

            //----------------------------------------------------------------//

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                tmpInt = (Int32)subKey.GetValue (_nameFlagClrMapUseClr,
                                                 _flagTrue);

                if (tmpInt == _flagFalse)
                    flagClrMapUseClr = false;
                else
                    flagClrMapUseClr = true;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d O p t C l r M a p C r n t                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored 'colour map' current colours.                      //
        // Missing items are given default values.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadOptClrMapCrnt (ref Int32[] indxClrMapBack,
                                              ref Int32[] indxClrMapFore)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsAnalyse +
                                        "\\" + _subKeyOptClrMap +
                                        "\\" + _subKeyCrnt;

            Int32 tmpInt;

            Int32 ctIndx = PrnParseRowTypes.getCount ();

            //----------------------------------------------------------------//

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                for (Int32 i = 0; i < ctIndx; i++)
                {
                    tmpInt = indxClrMapBack[i]; // current or initial value;

                    indxClrMapBack[i] =
                        (Int32)subKey.GetValue (
                                    _nameIndxClrMapRootBack + i.ToString ("D2"),
                                    tmpInt);

                    tmpInt = indxClrMapFore[i]; // current or initial value;

                    indxClrMapFore[i] =
                        (Int32)subKey.GetValue (
                                    _nameIndxClrMapRootFore + i.ToString ("D2"),
                                    tmpInt);
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d O p t C l r M a p T h e m e                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored 'colour map' user theme values for specified theme //
        // number.                                                            //
        // Missing items are given default values.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadOptClrMapTheme (Int32 number,
                                               ref Int32[] indxClrMapBack,
                                               ref Int32[] indxClrMapFore)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String themeNo = number.ToString ("D2");

            String key = _subKeyTools + "\\" + _subKeyToolsAnalyse +
                                        "\\" + _subKeyOptClrMap +
                                        "\\" + _subKeyUserThemeRoot + themeNo;

            Int32 tmpInt;

            Int32 ctIndx = PrnParseRowTypes.getCount ();

            //----------------------------------------------------------------//

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                for (Int32 i = 0; i < ctIndx; i++)
                {
                    tmpInt = indxClrMapBack[i]; // current or initial value;

                    indxClrMapBack[i] =
                        (Int32)subKey.GetValue (
                                    _nameIndxClrMapRootBack + i.ToString ("D2"),
                                    tmpInt);

                    tmpInt = indxClrMapFore[i]; // current or initial value;

                    indxClrMapFore[i] =
                        (Int32)subKey.GetValue (
                                    _nameIndxClrMapRootFore + i.ToString ("D2"),
                                    tmpInt);
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d O p t C l r M a p T h e m e N a m e                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored 'colour map' user theme name for specified theme   //
        // number.                                                            //
        // Missing items are given default values.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadOptClrMapThemeName (Int32      number,
                                                   ref String name)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String themeNo = number.ToString ("D2");

            String key = _subKeyTools + "\\" + _subKeyToolsAnalyse +
                                        "\\" + _subKeyOptClrMap +
                                        "\\" + _subKeyUserThemeRoot + themeNo; 

            //----------------------------------------------------------------//

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                name = (String)subKey.GetValue (_nameTheme,
                                                _defaultThemeName);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d O p t G e n e r a l                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored 'General' options.                                 //
        // Missing items are given default values.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadOptGeneral(ref Int32   indxOffsetType,
                                          ref Boolean flagMiscAutoAnalyse,
                                          ref Boolean flagDiagFileAccess)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            Int32 tmpInt;

            String key = _subKeyTools + "\\" + _subKeyToolsAnalyse +
                                        "\\" + _subKeyOptGeneral;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                indxOffsetType  = (Int32) subKey.GetValue (_nameIndxOffsetType,
                                                           _indexZero);

                tmpInt          = (Int32) subKey.GetValue (_nameFlagAutoAnalyse,
                                                           _flagTrue);

                if (tmpInt == _flagFalse)
                    flagMiscAutoAnalyse = false;
                else
                    flagMiscAutoAnalyse = true;

                tmpInt          = (Int32) subKey.GetValue (_nameFlagDiagFileAccess,
                                                           _flagFalse);

                if (tmpInt == _flagFalse)
                    flagDiagFileAccess = false;
                else
                    flagDiagFileAccess = true;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d O p t H P G L 2                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored 'HP-GL/2' options.                                 //
        // Missing items are given default values.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadOptHPGL2(ref Boolean flagMiscBinData)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            Int32 tmpInt;

            String key = _subKeyTools + "\\" + _subKeyToolsAnalyse +
                                        "\\" + _subKeyOptHPGL2;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                tmpInt = (Int32) subKey.GetValue (_nameFlagBinData,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagMiscBinData = false;
                else
                    flagMiscBinData = true;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d O p t P C L                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored 'PCL' options.                                     //
        // Missing items are given default values.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadOptPCL(ref Boolean flagFontHddr,
                                      ref Boolean flagFontChar,
                                      ref Boolean flagFontDraw,
                                      ref Int32   valFontDrawHeight,
                                      ref Int32   valFontDrawWidth,
                                      ref Boolean flagMacroDisplay,
                                      ref Boolean flagMiscStyleData,
                                      ref Boolean flagMiscBinData,
                                      ref Boolean flagTransAlphaNumId,
                                      ref Boolean flagTransColourLookup,
                                      ref Boolean flagTransConfIO,
                                      ref Boolean flagTransConfImageData,
                                      ref Boolean flagTransConfRasterData,
                                      ref Boolean flagTransDefLogPage,
                                      ref Boolean flagTransDefSymSet,
                                      ref Boolean flagTransDitherMatrix,
                                      ref Boolean flagTransDriverConf,
                                      ref Boolean flagTransEscEncText,
                                      ref Boolean flagTransPaletteConf,
                                      ref Boolean flagTransUserPattern,
                                      ref Boolean flagTransViewIlluminant)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            Int32 tmpInt;

            String key = _subKeyTools + "\\" + _subKeyToolsAnalyse +
                                        "\\" + _subKeyOptPCL;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                tmpInt = (Int32) subKey.GetValue (_nameFlagFontHddr,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagFontHddr = false;
                else
                    flagFontHddr = true;

                //------------------------------------------------------------//

                tmpInt = (Int32) subKey.GetValue (_nameFlagFontChar,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagFontChar = false;
                else
                    flagFontChar = true;

                //------------------------------------------------------------//

                tmpInt = (Int32) subKey.GetValue (_nameFlagFontDraw,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagFontDraw = false;
                else
                    flagFontDraw = true;

                valFontDrawHeight =
                    (Int32) subKey.GetValue (_nameFontDrawHeight,
                                             75);

                valFontDrawWidth =
                    (Int32) subKey.GetValue (_nameFontDrawWidth,
                                             50);

                //------------------------------------------------------------//

                tmpInt = (Int32) subKey.GetValue (_nameFlagMacroDisplay,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagMacroDisplay = false;
                else
                    flagMacroDisplay = true;

                //------------------------------------------------------------//

                tmpInt = (Int32) subKey.GetValue (_nameFlagStyleData,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagMiscStyleData = false;
                else
                    flagMiscStyleData = true;

                //------------------------------------------------------------//

                tmpInt = (Int32) subKey.GetValue (_nameFlagBinData,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagMiscBinData = false;
                else
                    flagMiscBinData = true;

                //------------------------------------------------------------//

                tmpInt = (Int32) subKey.GetValue (_nameFlagAlphaNumId,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagTransAlphaNumId = false;
                else
                    flagTransAlphaNumId = true;

                //------------------------------------------------------------//

                tmpInt = (Int32)subKey.GetValue (_nameFlagColourLookup,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagTransColourLookup = false;
                else
                    flagTransColourLookup = true;

                //------------------------------------------------------------//

                tmpInt = (Int32)subKey.GetValue (_nameFlagConfIO,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagTransConfIO = false;
                else
                    flagTransConfIO = true;

                //------------------------------------------------------------//

                tmpInt = (Int32) subKey.GetValue (_nameFlagConfImageData,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagTransConfImageData = false;
                else
                    flagTransConfImageData = true;

                //------------------------------------------------------------//

                tmpInt = (Int32) subKey.GetValue (_nameFlagConfRasterData,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagTransConfRasterData = false;
                else
                    flagTransConfRasterData = true;

                //------------------------------------------------------------//

                tmpInt = (Int32) subKey.GetValue (_nameFlagDefLogPage,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagTransDefLogPage = false;
                else
                    flagTransDefLogPage = true;

                //------------------------------------------------------------//

                tmpInt = (Int32) subKey.GetValue (_nameFlagDefSymSet,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagTransDefSymSet = false;
                else
                    flagTransDefSymSet = true;

                //------------------------------------------------------------//

                tmpInt = (Int32)subKey.GetValue (_nameFlagDitherMatrix,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagTransDitherMatrix = false;
                else
                    flagTransDitherMatrix = true;

                //------------------------------------------------------------//

                tmpInt = (Int32)subKey.GetValue (_nameFlagDriverConf,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagTransDriverConf = false;
                else
                    flagTransDriverConf = true;

                //------------------------------------------------------------//

                tmpInt = (Int32)subKey.GetValue (_nameFlagEscEncText,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagTransEscEncText = false;
                else
                    flagTransEscEncText = true;

                //------------------------------------------------------------//

                tmpInt = (Int32) subKey.GetValue (_nameFlagPaletteConf,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagTransPaletteConf = false;
                else
                    flagTransPaletteConf = true;

                //------------------------------------------------------------//

                tmpInt = (Int32) subKey.GetValue (_nameFlagUserPattern,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagTransUserPattern = false;
                else
                    flagTransUserPattern = true;

                //------------------------------------------------------------//

                tmpInt = (Int32)subKey.GetValue (_nameFlagViewIlluminant,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagTransViewIlluminant = false;
                else
                    flagTransViewIlluminant = true;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d O p t P C L X L                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored 'PCL XL' options.                                  //
        // Missing items are given default values.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadOptPCLXL(ref Boolean flagFontHddr,
                                        ref Boolean flagFontChar,
                                        ref Boolean flagFontDraw,
                                        ref Int32 valFontDrawHeight,
                                        ref Int32 valFontDrawWidth,
                                        ref Boolean flagEncUserStream,
                                        ref Boolean flagEncPCLPassThrough,
                                        ref Boolean flagEncPCLFontSelect,
                                        ref Boolean flagMiscOperPos,
                                        ref Boolean flagMiscBinData,
                                        ref Boolean flagMiscVerbose)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            Int32 tmpInt;

            String key = _subKeyTools + "\\" + _subKeyToolsAnalyse +
                                        "\\" + _subKeyOptPCLXL;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                tmpInt = (Int32) subKey.GetValue (_nameFlagFontHddr,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagFontHddr = false;
                else
                    flagFontHddr = true;

                //------------------------------------------------------------//

                tmpInt = (Int32) subKey.GetValue (_nameFlagFontChar,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagFontChar = false;
                else
                    flagFontChar = true;

                //------------------------------------------------------------//

                tmpInt = (Int32) subKey.GetValue (_nameFlagFontDraw,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagFontDraw = false;
                else
                    flagFontDraw = true;

                valFontDrawHeight =
                    (Int32) subKey.GetValue (_nameFontDrawHeight,
                                             75);

                valFontDrawWidth =
                    (Int32) subKey.GetValue (_nameFontDrawWidth,
                                             50);

                //------------------------------------------------------------//

                tmpInt = (Int32) subKey.GetValue (_nameFlagUserStream,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagEncUserStream = false;
                else
                    flagEncUserStream = true;

                //------------------------------------------------------------//

                tmpInt = (Int32) subKey.GetValue (_nameFlagPCLPassThrough,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagEncPCLPassThrough = false;
                else
                    flagEncPCLPassThrough = true;

                //------------------------------------------------------------//

                tmpInt = (Int32) subKey.GetValue (_nameFlagPCLFontSelect,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagEncPCLFontSelect = false;
                else
                    flagEncPCLFontSelect = true;

                //------------------------------------------------------------//

                tmpInt = (Int32) subKey.GetValue (_nameFlagPCLFontSelect,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagEncPCLFontSelect = false;
                else
                    flagEncPCLFontSelect = true;

                //------------------------------------------------------------//

                tmpInt = (Int32) subKey.GetValue (_nameFlagOperPos,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagMiscOperPos = false;
                else
                    flagMiscOperPos = true;

                //------------------------------------------------------------//

                tmpInt = (Int32) subKey.GetValue (_nameFlagBinData,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagMiscBinData = false;
                else
                    flagMiscBinData = true;

                //------------------------------------------------------------//

                tmpInt = (Int32) subKey.GetValue (_nameFlagVerbose,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagMiscVerbose = false;
                else
                    flagMiscVerbose = true;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d O p t P M L                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored 'PML' options.                                     //
        // Missing items are given default values.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadOptPML(ref Boolean flagWithinPCL,
                                      ref Boolean flagWithinPJL,
                                      ref Boolean flagVerbose)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            Int32 tmpInt;

            String key = _subKeyTools + "\\" + _subKeyToolsAnalyse +
                                        "\\" + _subKeyOptPML;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                tmpInt = (Int32) subKey.GetValue (_nameFlagWithinPCL,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagWithinPCL = false;
                else
                    flagWithinPCL = true;

                tmpInt = (Int32) subKey.GetValue (_nameFlagWithinPJL,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagWithinPJL = false;
                else
                    flagWithinPJL = true;

                tmpInt = (Int32) subKey.GetValue (_nameFlagVerbose,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagVerbose = false;
                else
                    flagVerbose = true;

            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d O p t S t a t s                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored 'Statistics' options.                              //
        // Missing items are given default values.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadOptStats (
            ref Int32 indxLevel,
            ref Boolean flagExcUnusedPCLObs,
            ref Boolean flagExcUnusedPCLXLRes)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);
            
            Int32 tmpInt; 
            
            String key = _subKeyTools + "\\" + _subKeyToolsAnalyse +
                                        "\\" + _subKeyOptStats;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                indxLevel       = (Int32) subKey.GetValue (_nameIndxStatsLevel,
                                                           _indexZero);

                tmpInt = (Int32) subKey.GetValue (_nameFlagExcUnusedPCLObs,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagExcUnusedPCLObs = false;
                else
                    flagExcUnusedPCLObs = true;

                tmpInt = (Int32) subKey.GetValue (_nameFlagExcUnusedPCLXLRes,
                                                  _flagFalse);

                if (tmpInt == _flagFalse)
                    flagExcUnusedPCLXLRes = false;
                else
                    flagExcUnusedPCLXLRes = true;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current print file data.                                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveData(String filename)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsAnalyse;

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

            String key = _subKeyTools + "\\" + _subKeyToolsAnalyse;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                subKey.SetValue (_nameIndxRptFileFmt,
                                indxRptFileFmt,
                                RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e O p t C h a r S e t                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current 'Character Set' option data.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveOptCharSet(Int32 indxName,
                                          Int32 indxSubAct,
                                          Int32 indxSubCode)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsAnalyse +
                                 "\\" + _subKeyOptCharSet;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                subKey.SetValue (_nameIndxName,
                                indxName,
                                RegistryValueKind.DWord);

                subKey.SetValue (_nameIndxSubAct,
                                 indxSubAct,
                                 RegistryValueKind.DWord);

                subKey.SetValue (_nameIndxSubCode,
                                indxSubCode,
                                RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e O p t C l r M a p                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current 'colour map' option data.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveOptClrMap (Boolean flagClrMapUseClr)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsAnalyse +
                                 "\\" + _subKeyOptClrMap;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                if (flagClrMapUseClr)
                    subKey.SetValue (_nameFlagClrMapUseClr,
                                     _flagTrue,
                                     RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagClrMapUseClr,
                                     _flagFalse,
                                     RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e O p t C l r M a p C r n t                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current 'colour map' current colour data.                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveOptClrMapCrnt (Int32[] indxClrMapBack,
                                              Int32[] indxClrMapFore)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsAnalyse +
                                 "\\" + _subKeyOptClrMap +
                                 "\\" + _subKeyCrnt;

            Int32 ctIndx;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                ctIndx = indxClrMapBack.Length;

                for (Int32 i = 0; i < ctIndx; i++)
                {
                    subKey.SetValue (_nameIndxClrMapRootBack + i.ToString ("D2"),
                                    indxClrMapBack[i],
                                    RegistryValueKind.DWord);
                }

                ctIndx = indxClrMapFore.Length;

                for (Int32 i = 0; i < ctIndx; i++)
                {
                    subKey.SetValue (_nameIndxClrMapRootFore + i.ToString ("D2"),
                                    indxClrMapFore[i],
                                    RegistryValueKind.DWord);
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e O p t C l r M a p T h e m e                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current 'colour map' user theme values for specified theme   //
        // number.                                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveOptClrMapTheme (Int32 number,
                                               Int32[] indxClrMapBack,
                                               Int32[] indxClrMapFore,
                                               String name)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String themeNo = number.ToString ("D2");

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsAnalyse +
                                 "\\" + _subKeyOptClrMap +
                                 "\\" + _subKeyUserThemeRoot + themeNo;

            Int32 ctIndx;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                ctIndx = indxClrMapBack.Length;

                for (Int32 i = 0; i < ctIndx; i++)
                {
                    subKey.SetValue (_nameIndxClrMapRootBack + i.ToString ("D2"),
                                    indxClrMapBack[i],
                                    RegistryValueKind.DWord);
                }

                ctIndx = indxClrMapFore.Length;

                for (Int32 i = 0; i < ctIndx; i++)
                {
                    subKey.SetValue (_nameIndxClrMapRootFore + i.ToString ("D2"),
                                    indxClrMapFore[i],
                                    RegistryValueKind.DWord);
                }

                if (name != null)
                {
                    subKey.SetValue (_nameTheme,
                                     name,
                                     RegistryValueKind.String);
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e O p t G e n e r a l                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current 'General' option data.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveOptGeneral(Int32   indxOffsetType,
                                          Boolean flagMiscAutoAnalyse,
                                          Boolean flagDiagFileAccess)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsAnalyse +
                                 "\\" + _subKeyOptGeneral;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                subKey.SetValue (_nameIndxOffsetType,
                                indxOffsetType,
                                RegistryValueKind.DWord);

                if (flagMiscAutoAnalyse)
                    subKey.SetValue (_nameFlagAutoAnalyse,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagAutoAnalyse,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagDiagFileAccess)
                    subKey.SetValue (_nameFlagDiagFileAccess,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagDiagFileAccess,
                                    _flagFalse,
                                    RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e O p t H P G L 2                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current 'HP-GL/2' option data.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveOptHPGL2 (Boolean flagMiscBinData)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsAnalyse +
                                 "\\" + _subKeyOptHPGL2;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                if (flagMiscBinData)
                    subKey.SetValue (_nameFlagBinData,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagBinData,
                                    _flagFalse,
                                    RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e O p t P C L                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current 'PCL' option data.                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveOptPCL(Boolean flagFontHddr,
                                      Boolean flagFontChar,
                                      Boolean flagFontDraw,
                                      Int32   valFontDrawHeight,
                                      Int32   valFontDrawWidth,
                                      Boolean flagMacroDisplay,
                                      Boolean flagMiscStyleData,
                                      Boolean flagMiscBinData,
                                      Boolean flagTransAlphaNumId,
                                      Boolean flagTransColourLookup,
                                      Boolean flagTransConfIO,
                                      Boolean flagTransConfImageData,
                                      Boolean flagTransConfRasterData,
                                      Boolean flagTransDefLogPage,
                                      Boolean flagTransDefSymSet,
                                      Boolean flagTransDitherMatrix,
                                      Boolean flagTransDriverConf,
                                      Boolean flagTransEscEncText,
                                      Boolean flagTransPaletteConf,
                                      Boolean flagTransUserPattern,
                                      Boolean flagTransViewIlluminant)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsAnalyse +
                                 "\\" + _subKeyOptPCL;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                if (flagFontHddr)
                    subKey.SetValue (_nameFlagFontHddr,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagFontHddr,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagFontChar)
                    subKey.SetValue (_nameFlagFontChar,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagFontChar,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagFontDraw)
                    subKey.SetValue (_nameFlagFontDraw,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagFontDraw,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                subKey.SetValue (_nameFontDrawHeight,
                                 valFontDrawHeight,
                                 RegistryValueKind.DWord);

                subKey.SetValue (_nameFontDrawWidth,
                                 valFontDrawWidth,
                                 RegistryValueKind.DWord);

                if (flagMacroDisplay)
                    subKey.SetValue (_nameFlagMacroDisplay,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagMacroDisplay,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagMiscStyleData)
                    subKey.SetValue (_nameFlagStyleData,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagStyleData,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagMiscBinData)
                    subKey.SetValue (_nameFlagBinData,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagBinData,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagTransAlphaNumId)
                    subKey.SetValue (_nameFlagAlphaNumId,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagAlphaNumId,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagTransColourLookup)
                    subKey.SetValue(_nameFlagColourLookup,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagColourLookup,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagTransConfIO)
                    subKey.SetValue(_nameFlagConfIO,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagConfIO,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagTransConfImageData)
                    subKey.SetValue (_nameFlagConfImageData,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagConfImageData,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagTransConfRasterData)
                    subKey.SetValue(_nameFlagConfRasterData,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagConfRasterData,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagTransDefLogPage)
                    subKey.SetValue (_nameFlagDefLogPage,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagDefLogPage,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagTransDefSymSet)
                    subKey.SetValue (_nameFlagDefSymSet,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagDefSymSet,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagTransDitherMatrix)
                    subKey.SetValue(_nameFlagDitherMatrix,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagDitherMatrix,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagTransDriverConf)
                    subKey.SetValue(_nameFlagDriverConf,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagDriverConf,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagTransEscEncText)
                    subKey.SetValue(_nameFlagEscEncText,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagEscEncText,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagTransPaletteConf)
                    subKey.SetValue (_nameFlagPaletteConf,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagPaletteConf,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagTransUserPattern)
                    subKey.SetValue(_nameFlagUserPattern,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagUserPattern,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagTransViewIlluminant)
                    subKey.SetValue(_nameFlagViewIlluminant,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagViewIlluminant,
                                    _flagFalse,
                                    RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e O p t P C L X L                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current 'PCL XL' option data.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveOptPCLXL(Boolean flagFontHddr,
                                        Boolean flagFontChar,
                                        Boolean flagFontDraw,
                                        Int32   valFontDrawHeight,
                                        Int32   valFontDrawWidth,
                                        Boolean flagEncUserStream,
                                        Boolean flagEncPCLPassThrough,
                                        Boolean flagEncPCLFontSelect,
                                        Boolean flagMiscOperPos,
                                        Boolean flagMiscBinData,
                                        Boolean flagMiscVerbose)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsAnalyse +
                                 "\\" + _subKeyOptPCLXL;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                if (flagFontHddr)
                    subKey.SetValue (_nameFlagFontHddr,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagFontHddr,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagFontChar)
                    subKey.SetValue (_nameFlagFontChar,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagFontChar,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagFontDraw)
                    subKey.SetValue (_nameFlagFontDraw,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagFontDraw,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                subKey.SetValue (_nameFontDrawHeight,
                                 valFontDrawHeight,
                                 RegistryValueKind.DWord);

                subKey.SetValue (_nameFontDrawWidth,
                                 valFontDrawWidth,
                                 RegistryValueKind.DWord);

                if (flagEncUserStream)
                    subKey.SetValue (_nameFlagUserStream,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagUserStream,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagEncPCLPassThrough)
                    subKey.SetValue (_nameFlagPCLPassThrough,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagPCLPassThrough,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagEncPCLFontSelect)
                    subKey.SetValue (_nameFlagPCLFontSelect,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagPCLFontSelect,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagMiscOperPos)
                    subKey.SetValue (_nameFlagOperPos,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagOperPos,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagMiscBinData)
                    subKey.SetValue (_nameFlagBinData,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagBinData,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagMiscVerbose)
                    subKey.SetValue (_nameFlagVerbose,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagVerbose,
                                    _flagFalse,
                                    RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e O p t P M L                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current 'PML' option data.                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveOptPML(Boolean flagWithinPCL,
                                      Boolean flagWithinPJL,
                                      Boolean flagVerbose)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsAnalyse +
                                 "\\" + _subKeyOptPML;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                if (flagWithinPCL)
                    subKey.SetValue (_nameFlagWithinPCL,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagWithinPCL,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagWithinPJL)
                    subKey.SetValue (_nameFlagWithinPJL,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagWithinPJL,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagVerbose)
                    subKey.SetValue (_nameFlagVerbose,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagVerbose,
                                    _flagFalse,
                                    RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e O p t S t a t s                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current 'Statistics' option data.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveOptStats (Int32 indxLevel,
                                         Boolean flagExcUnusedPCLObs,
                                         Boolean flagExcUnusedPCLXLRes)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsAnalyse +
                                 "\\" + _subKeyOptStats;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                subKey.SetValue (_nameIndxStatsLevel,
                                indxLevel,
                                RegistryValueKind.DWord);

                if (flagExcUnusedPCLObs)
                    subKey.SetValue (_nameFlagExcUnusedPCLObs,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagExcUnusedPCLObs,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagExcUnusedPCLXLRes)
                    subKey.SetValue (_nameFlagExcUnusedPCLXLRes,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagExcUnusedPCLXLRes,
                                    _flagFalse,
                                    RegistryValueKind.DWord);
            }
        }
    }
}
