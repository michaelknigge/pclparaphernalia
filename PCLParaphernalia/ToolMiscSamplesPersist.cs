using Microsoft.Win32;
using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class manages persistent storage of options for the MiscSamples tool.
    /// 
    /// © Chris Hutchinson 2014
    /// 
    /// </summary>

    static class ToolMiscSamplesPersist
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        const String _mainKey                 = MainForm._regMainKey;

        const String _subKeyTools             = "Tools";
        const String _subKeyToolsMiscSamples  = "MiscSamples";
        const String _subKeyPCL               = "PCL";
        const String _subKeyPCLXL             = "PCLXL";

        const String _subKeyCommon            = "Common";

        const String _subKeyColour            = "Colour";
        const String _subKeyLogOper           = "LogOper";
        const String _subKeyLogPage           = "LogPage";
        const String _subKeyPattern           = "Pattern";
        const String _subKeyTxtMod            = "TxtMod";
        const String _subKeyUnicode           = "Unicode";

        const String _nameCaptureFile         = "CaptureFile";
        const String _nameFlagAddStdPage      = "FlagAddStdPage";
        const String _nameFlagFormAsMacro     = "FlagFormAsMacro";
        const String _nameFlagMapHex          = "FlagMapHex";
        const String _nameFlagSrcTextPat      = "FlagSrcTextPat";
        const String _nameFlagUseColour       = "FlagUseColour";
        const String _nameFlagUseMacros       = "FlagUseMacros";
        
        const String _nameIndxColourMode      = "IndxColourMode";
        const String _nameIndxColourD1        = "IndxColourD1";
        const String _nameIndxColourD2        = "IndxColourD2";
        const String _nameIndxColourS1        = "IndxColourS1";
        const String _nameIndxColourS2        = "IndxColourS2";
        const String _nameIndxColourT1        = "IndxColourT1";
        const String _nameIndxColourT2        = "IndxColourT2";
        const String _nameIndxMonoD1          = "IndxMonoD1";
        const String _nameIndxMonoD2          = "IndxMonoD2";
        const String _nameIndxMonoS1          = "IndxMonoS1";
        const String _nameIndxMonoS2          = "IndxMonoS2";
        const String _nameIndxMonoT1          = "IndxMonoT1";
        const String _nameIndxMonoT2          = "IndxMonoT2";
        const String _nameIndxFont            = "IndxFont";
        const String _nameIndxOrientation     = "IndxOrientation";
        const String _nameIndxPaperSize       = "IndxPaperSize";
        const String _nameIndxPaperType       = "IndxPaperType";
        const String _nameIndxPDL             = "IndxPDL";
        const String _nameIndxPatternType     = "IndxPatternType";
        const String _nameIndxROPFrom         = "IndxROPFrom";
        const String _nameIndxROPTo           = "IndxROPTo";
        const String _nameIndxSampleType      = "IndxSampleType";
        const String _nameIndxTxtModType      = "IndxTxtModType";
        const String _nameIndxVariant         = "IndxVariant";

        const String _nameValueRoot           = "_Value_";
        const String _nameCodePoint           = "CodePoint";
        const String _nameOffsetLeft          = "OffsetLeft";
        const String _nameOffsetTop           = "OffsetTop";
        const String _namePageHeight          = "PageHeight";
        const String _namePageWidth           = "PageWidth";

        const Int32 _flagFalse                = 0;
        const Int32 _flagTrue                 = 1;
        const Int32 _indexZero                = 0;
        const Int32 _indexNeg                 = -1;

        const String _defaultCaptureFileRoot  = "CaptureFile_MiscSamples_";

        const Int32 _defaultColour_0          = 0xff0000;
        const Int32 _defaultColour_1          = 0x00ff00;
        const Int32 _defaultColour_2          = 0x0000ff;
        const Int32 _defaultColour_3          = 0xffb450;
        const Int32 _defaultShade_0           = 0x20;
        const Int32 _defaultShade_1           = 0x40;
        const Int32 _defaultShade_2           = 0x80;
        const Int32 _defaultShade_3           = 0xc0;
        const Int32 _defaultCodePoint         = 0x20ac;
        const Int32 _defaultIndxVariant = (Int32)(PCLFonts.eVariant.Regular);

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a C a p t u r e                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored Misc Samples capture file data.                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataCapture(
            ToolCommonData.eToolSubIds crntToolSubId, 
            ToolCommonData.ePrintLang  crntPDL,
            ref String                 captureFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String subKeyType = "";
            String defFileBase = "";

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            if (crntToolSubId == ToolCommonData.eToolSubIds.Colour)
            {
                subKeyType = "\\" + _subKeyColour;
                defFileBase = _defaultCaptureFileRoot + _subKeyColour; 
            }
            else if (crntToolSubId == ToolCommonData.eToolSubIds.LogOper)
            {
                subKeyType = "\\" + _subKeyLogOper;
                defFileBase = _defaultCaptureFileRoot + _subKeyLogOper; 
            }
            else if (crntToolSubId == ToolCommonData.eToolSubIds.LogPage)
            {
                subKeyType = "\\" + _subKeyLogPage;
                defFileBase = _defaultCaptureFileRoot + _subKeyLogPage; 
            }
            else if (crntToolSubId == ToolCommonData.eToolSubIds.Pattern)
            {
                subKeyType = "\\" + _subKeyPattern;
                defFileBase = _defaultCaptureFileRoot + _subKeyPattern; 
            }
            else if (crntToolSubId == ToolCommonData.eToolSubIds.TxtMod)
            {
                subKeyType = "\\" + _subKeyTxtMod;
                defFileBase = _defaultCaptureFileRoot + _subKeyTxtMod; 
            }
            else if (crntToolSubId == ToolCommonData.eToolSubIds.Unicode)
            {
                subKeyType = "\\" + _subKeyUnicode;
                defFileBase = _defaultCaptureFileRoot + _subKeyUnicode; 
            }

            if (crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                String key = _subKeyTools + "\\" + _subKeyToolsMiscSamples +
                                            "\\" + subKeyType +
                                            "\\" + _subKeyPCL;

                using (RegistryKey subKey = keyMain.CreateSubKey(key))
                {
                    captureFile = (String)subKey.GetValue(
                        _nameCaptureFile,
                        defWorkFolder + "\\" + defFileBase + _subKeyPCL + ".prn");
                }
            }
            else if (crntPDL == ToolCommonData.ePrintLang.PCLXL)
            {
                String key = _subKeyTools + "\\" + _subKeyToolsMiscSamples +
                                            "\\" + subKeyType +
                                            "\\" + _subKeyPCLXL;

                using (RegistryKey subKey = keyMain.CreateSubKey(key))
                {
                    captureFile = (String)subKey.GetValue(
                        _nameCaptureFile,
                        defWorkFolder + "\\" + defFileBase + _subKeyPCLXL + ".prn");
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a C o m m o n                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored Misc Samples common data.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataCommon(ref Int32   indxPDL,
                                          ref Int32   indxSampleType)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsMiscSamples;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                indxPDL = (Int32)subKey.GetValue(_nameIndxPDL,
                                                 _indexZero);

                indxSampleType = (Int32)subKey.GetValue (_nameIndxSampleType,
                                                         _indexZero);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a C o m m o n P D L                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored Misc Samples PCL or PCL XL common data.            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataCommonPDL (String      pdlName,
                                              ref Int32   indxOrientation,
                                              ref Int32   indxPaperSize,
                                              ref Int32   indxPaperType)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsMiscSamples +
                                 "\\" + _subKeyCommon +
                                 "\\" + pdlName;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                indxOrientation = (Int32)subKey.GetValue(_nameIndxOrientation,
                                                         _indexZero);

                indxPaperSize   = (Int32)subKey.GetValue(_nameIndxPaperSize,
                                                         _indexZero);

                indxPaperType   = (Int32)subKey.GetValue(_nameIndxPaperType,
                                                         _indexZero);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a T y p e C o l o u r                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored Misc Samples - Colour data.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataTypeColour (String pdlName,
                                               ref Int32   indxColourMode,
                                               ref Boolean flagFormAsMacro,
                                               ref Boolean flagMapHex)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key;

            Int32 tmpInt;

            key = _subKeyTools + "\\" + _subKeyToolsMiscSamples +
                                 "\\" + _subKeyColour +
                                 "\\" + pdlName;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                indxColourMode = (Int32) subKey.GetValue (_nameIndxColourMode,
                                                          _indexZero);

                tmpInt = (Int32) subKey.GetValue (_nameFlagFormAsMacro,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagFormAsMacro = false;
                else
                    flagFormAsMacro = true;

                tmpInt = (Int32) subKey.GetValue (_nameFlagMapHex,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagMapHex = false;
                else
                    flagMapHex = true;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a T y p e C o l o u r S a m p l e                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored Misc Samples - Colour data sample.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataTypeColourSample (String pdlName,
                                                     String sampleName,
                                                     Int32  sampleCt,
                                                     ref Int32 [] values,
                                                     Boolean monochrome)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsMiscSamples +
                                 "\\" + _subKeyColour +
                                 "\\" + pdlName;

            if (monochrome)
            {
                using (RegistryKey subKey = keyMain.CreateSubKey (key))
                {
                    Int32 defVal;

                    for (Int32 i = 0; i < sampleCt; i++)
                    {
                        if (i == 0)
                            defVal = _defaultShade_0;
                        else if (i == 1)
                            defVal = _defaultShade_1;
                        else if (i == 2)
                            defVal = _defaultShade_2;
                        else
                            defVal = _defaultShade_3;

                        values[i] = (Int32) subKey.GetValue (
                            sampleName + _nameValueRoot + i, defVal);
                    }
                }
            }
            else
            {
                using (RegistryKey subKey = keyMain.CreateSubKey (key))
                {
                    for (Int32 i = 0; i < sampleCt; i++)
                    {
                        Int32 defVal;

                        if (i == 0)
                            defVal = _defaultColour_0;
                        else if (i == 1)
                            defVal = _defaultColour_1;
                        else if (i == 2)
                            defVal = _defaultColour_2;
                        else
                            defVal = _defaultColour_3;

                        values[i] = (Int32) subKey.GetValue (
                            sampleName + _nameValueRoot + i, defVal);
                    }
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a T y p e L o g O p e r                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored Misc Samples - Logical Operations data.            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataTypeLogOper (String pdlName,
                                                ref Int32 indxMode,
                                                ref Int32 indxROPFrom,
                                                ref Int32 indxROPTo, 
                                                ref Int32 indxClrD1,
                                                ref Int32 indxClrD2,
                                                ref Int32 indxClrS1,
                                                ref Int32 indxClrS2,
                                                ref Int32 indxClrT1,
                                                ref Int32 indxClrT2,
                                                ref Int32 indxMonoD1,
                                                ref Int32 indxMonoD2,
                                                ref Int32 indxMonoS1,
                                                ref Int32 indxMonoS2,
                                                ref Int32 indxMonoT1,
                                                ref Int32 indxMonoT2,
                                                ref Boolean flagUseMacros,
                                                ref Boolean flagSrcTextPat)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key;

            Int32 tmpInt;
            Int32 indxClrBlack,
                  indxClrWhite,
                  indxMonoBlack,
                  indxMonoWhite;

            key = _subKeyTools + "\\" + _subKeyToolsMiscSamples +
                                 "\\" + _subKeyLogOper +
                                 "\\" + pdlName;

            if (pdlName == _subKeyPCL)
            {
                Byte indxPalCMY =
                    (Byte) PCLPalettes.eIndex.PCLSimpleColourCMY;

                indxClrBlack = PCLPalettes.getClrItemBlack (indxPalCMY);
                indxClrWhite = PCLPalettes.getClrItemWhite (indxPalCMY);

                Byte indxPalMono =
                    (Byte) PCLPalettes.eIndex.PCLMonochrome;

                indxMonoBlack = PCLPalettes.getClrItemBlack (indxPalMono);
                indxMonoWhite = PCLPalettes.getClrItemWhite (indxPalMono);
            }
            else // if (pdlName == _subKeyPCLXL)
            {
                Byte indxPalRGB =
                    (Byte) PCLXLPalettes.eIndex.PCLXLRGB;

                indxClrBlack = PCLXLPalettes.getClrItemBlack (indxPalRGB);
                indxClrWhite = PCLXLPalettes.getClrItemWhite (indxPalRGB);

                indxMonoBlack = 0;
                indxMonoWhite = 255;
            }

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                indxMode = (Int32) subKey.GetValue (_nameIndxColourMode,
                                                    _indexZero);

                indxROPFrom = (Int32) subKey.GetValue (_nameIndxROPFrom,
                                                       _indexZero);

                indxROPTo = (Int32) subKey.GetValue (_nameIndxROPTo,
                                                     _indexNeg);

                indxClrD1 = (Int32) subKey.GetValue (_nameIndxColourD1,
                                                     indxClrBlack);

                indxClrD2 = (Int32) subKey.GetValue (_nameIndxColourD2,
                                                     indxClrWhite);

                indxClrS1 = (Int32) subKey.GetValue (_nameIndxColourS1,
                                                     indxClrBlack);

                indxClrS2 = (Int32) subKey.GetValue (_nameIndxColourS2,
                                                     indxClrWhite);

                indxClrT1 = (Int32) subKey.GetValue (_nameIndxColourT1,
                                                     indxClrBlack);

                indxClrT2 = (Int32) subKey.GetValue (_nameIndxColourT2,
                                                     indxClrWhite);

                indxMonoD1 = (Int32) subKey.GetValue (_nameIndxMonoD1,
                                                     indxMonoBlack);

                indxMonoD2 = (Int32) subKey.GetValue (_nameIndxMonoD2,
                                                     indxMonoWhite);

                indxMonoS1 = (Int32) subKey.GetValue (_nameIndxMonoS1,
                                                     indxMonoBlack);

                indxMonoS2 = (Int32) subKey.GetValue (_nameIndxMonoS2,
                                                     indxMonoWhite);

                indxMonoT1 = (Int32) subKey.GetValue (_nameIndxMonoT1,
                                                     indxMonoBlack);

                indxMonoT2 = (Int32) subKey.GetValue (_nameIndxMonoT2,
                                                     indxMonoWhite);

                tmpInt = (Int32) subKey.GetValue (_nameFlagUseMacros,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagUseMacros = false;
                else
                    flagUseMacros = true;

                if (pdlName == _subKeyPCLXL)
                {
                    tmpInt = (Int32) subKey.GetValue (_nameFlagSrcTextPat,
                                                      _flagTrue);

                    if (tmpInt == _flagFalse)
                        flagSrcTextPat = false;
                    else
                        flagSrcTextPat = true;
                }
                else
                {
                    flagSrcTextPat = true;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a T y p e L o g P a g e                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored Misc Samples - Logical Page data.                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataTypeLogPage (String      pdlName,
                                                ref Int32   offsetLeft,
                                                ref Int32   offsetTop,
                                                ref Int32   pageHeight,
                                                ref Int32   pageWidth,
                                                ref Boolean flagFormAsMacro,
                                                ref Boolean flagAddStdPage)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            Int32 tmpInt;

            key = _subKeyTools + "\\" + _subKeyToolsMiscSamples +
                                 "\\" + _subKeyLogPage +
                                 "\\" + pdlName;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                offsetLeft = (Int32)subKey.GetValue(_nameOffsetLeft,
                                                         _indexZero);

                offsetTop  = (Int32)subKey.GetValue(_nameOffsetTop,
                                                         _indexZero);

                pageHeight = (Int32)subKey.GetValue(_namePageHeight,
                                                         _indexZero);

                pageWidth  = (Int32)subKey.GetValue(_namePageWidth,
                                                         _indexZero);

                tmpInt = (Int32) subKey.GetValue (_nameFlagFormAsMacro,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagFormAsMacro = false;
                else
                    flagFormAsMacro = true;

                tmpInt = (Int32)subKey.GetValue(_nameFlagAddStdPage,
                                                         _flagTrue);

                if (tmpInt == _flagFalse)
                    flagAddStdPage = false;
                else
                    flagAddStdPage = true;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a T y p e P a t t e r n                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored Misc Samples - Pattern data.                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataTypePattern(String      pdlName,
                                               ref Int32   indxPatternType,
                                               ref Boolean flagFormAsMacro)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            Int32 tmpInt;

            key = _subKeyTools + "\\" + _subKeyToolsMiscSamples +
                                 "\\" + _subKeyPattern +
                                 "\\" + pdlName;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                indxPatternType = (Int32)subKey.GetValue(_nameIndxPatternType,
                                                         _indexZero);

                tmpInt = (Int32) subKey.GetValue (_nameFlagFormAsMacro,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagFormAsMacro = false;
                else
                    flagFormAsMacro = true;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a T y p e T x t M o d                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored Misc Samples - Txt Mod data.                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataTypeTxtMod(String       pdlName,
                                               ref Int32   indxTxtModType,
                                               ref Boolean flagFormAsMacro)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            Int32 tmpInt;

            key = _subKeyTools + "\\" + _subKeyToolsMiscSamples +
                                 "\\" + _subKeyTxtMod +
                                 "\\" + pdlName;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                indxTxtModType = (Int32)subKey.GetValue(_nameIndxTxtModType,
                                                         _indexZero);

                tmpInt = (Int32) subKey.GetValue (_nameFlagFormAsMacro,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagFormAsMacro = false;
                else
                    flagFormAsMacro = true;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a T y p e U n i c o d e                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored Misc Samples - Unicode data.                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataTypeUnicode(String      pdlName,
                                               ref Int32   indxFont,
                                               ref PCLFonts.eVariant variant,
                                               ref Int32   codePoint,
                                               ref Boolean flagFormAsMacro)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            Int32 tmpInt;

            key = _subKeyTools + "\\" + _subKeyToolsMiscSamples +
                                 "\\" + _subKeyUnicode +
                                 "\\" + pdlName;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                indxFont = (Int32)subKey.GetValue(_nameIndxFont,
                                                  _indexZero);

                tmpInt = (Int32)subKey.GetValue(_nameIndxVariant,
                                                      _defaultIndxVariant);
                variant = (PCLFonts.eVariant)tmpInt;

                codePoint = (Int32)subKey.GetValue(_nameCodePoint,
                                                   _defaultCodePoint);

                tmpInt = (Int32) subKey.GetValue (_nameFlagFormAsMacro,
                                                  _flagTrue);

                if (tmpInt == _flagFalse)
                    flagFormAsMacro = false;
                else
                    flagFormAsMacro = true;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a C a p t u r e                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current Misc Samples capture file data.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataCapture(
            ToolCommonData.eToolSubIds crntToolSubId,
            ToolCommonData.ePrintLang  crntPDL,
            String                     captureFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String subKeyType = "";

            if (crntToolSubId == ToolCommonData.eToolSubIds.Colour)
                subKeyType = "\\" + _subKeyColour;
            else if (crntToolSubId == ToolCommonData.eToolSubIds.LogOper)
                subKeyType = "\\" + _subKeyLogOper;
            else if (crntToolSubId == ToolCommonData.eToolSubIds.LogPage)
                subKeyType = "\\" + _subKeyLogPage;
            else if (crntToolSubId == ToolCommonData.eToolSubIds.Pattern)
                subKeyType = "\\" + _subKeyPattern;
            else if (crntToolSubId == ToolCommonData.eToolSubIds.TxtMod)
                subKeyType = "\\" + _subKeyTxtMod;
            else if (crntToolSubId == ToolCommonData.eToolSubIds.Unicode)
                subKeyType = "\\" + _subKeyUnicode;

            if (crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                String key = _subKeyTools + "\\" + _subKeyToolsMiscSamples +
                                            "\\" + subKeyType +
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
                String key = _subKeyTools + "\\" + _subKeyToolsMiscSamples +
                                            "\\" + subKeyType +
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
        // Store current Misc Samples common data.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataCommon(Int32   indxPDL,
                                          Int32   indxSampleType)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsMiscSamples;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue(_nameIndxPDL,
                                indxPDL,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxSampleType,
                                indxSampleType,
                                RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a C o m m o n P D L                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current Misc Samples PCL or PCL XL common data.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataCommonPDL(String    pdlName,
                                             Int32     indxOrientation,
                                             Int32     indxPaperSize,
                                             Int32     indxPaperType)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);
            
            String key;

            key = _subKeyTools + "\\" + _subKeyToolsMiscSamples +
                                 "\\" + _subKeyCommon +
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
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a T y p e C o l o u r                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current Misc Samples - Colour data.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataTypeColour (String  pdlName,
                                               Int32   indxColourMode,
                                               Boolean flagFormAsMacro,
                                               Boolean flagMapHex)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);
            
            String key;

            key = _subKeyTools + "\\" + _subKeyToolsMiscSamples +
                                 "\\" + _subKeyColour +
                                 "\\" + pdlName;
            
            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue(_nameIndxColourMode,
                                indxColourMode,
                                RegistryValueKind.DWord);

                if (flagFormAsMacro)
                    subKey.SetValue(_nameFlagFormAsMacro,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagFormAsMacro,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagMapHex)
                    subKey.SetValue(_nameFlagMapHex,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagMapHex,
                                    _flagFalse,
                                    RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a T y p e C o l o u r S a m p l e                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current Misc Samples - Colour data sample.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataTypeColourSample (String   pdlName,
                                                     String   sampleName,
                                                     Int32    sampleCt,
                                                     Int32 [] values)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);
            
            String key;

            key = _subKeyTools + "\\" + _subKeyToolsMiscSamples +
                                 "\\" + _subKeyColour +
                                 "\\" + pdlName;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                for (Int32 i = 0; i < sampleCt; i++)
                {
                    subKey.SetValue (sampleName + _nameValueRoot + i,
                                    values [i],
                                    RegistryValueKind.DWord);
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a T y p e L o g O p e r                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current Misc Samples - Logical Operations data.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataTypeLogOper (String pdlName,
                                                Int32 indxMode,
                                                Int32 indxROPFrom,
                                                Int32 indxROPTo,
                                                Int32 indxClrD1,
                                                Int32 indxClrD2,
                                                Int32 indxClrS1,
                                                Int32 indxClrS2,
                                                Int32 indxClrT1,
                                                Int32 indxClrT2,
                                                Int32 indxMonoD1,
                                                Int32 indxMonoD2,
                                                Int32 indxMonoS1,
                                                Int32 indxMonoS2,
                                                Int32 indxMonoT1,
                                                Int32 indxMonoT2,
                                                Boolean flagUseMacros,
                                                Boolean flagSrcTextPat)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsMiscSamples +
                                 "\\" + _subKeyLogOper +
                                 "\\" + pdlName;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                subKey.SetValue (_nameIndxColourMode,
                                 indxMode,
                                 RegistryValueKind.DWord);

                subKey.SetValue (_nameIndxROPFrom,
                                 indxROPFrom,
                                 RegistryValueKind.DWord);

                subKey.SetValue (_nameIndxROPTo,
                                 indxROPTo,
                                 RegistryValueKind.DWord);

                subKey.SetValue (_nameIndxColourD1,
                                 indxClrD1,
                                 RegistryValueKind.DWord);

                subKey.SetValue (_nameIndxColourD2,
                                 indxClrD2,
                                 RegistryValueKind.DWord);

                subKey.SetValue (_nameIndxColourS1,
                                 indxClrS1,
                                 RegistryValueKind.DWord);

                subKey.SetValue (_nameIndxColourS2,
                                 indxClrS2,
                                 RegistryValueKind.DWord);

                subKey.SetValue (_nameIndxColourT1,
                                 indxClrT1,
                                 RegistryValueKind.DWord);

                subKey.SetValue (_nameIndxColourT2,
                                 indxClrT2,
                                 RegistryValueKind.DWord);

                subKey.SetValue (_nameIndxMonoD1,
                                 indxMonoD1,
                                 RegistryValueKind.DWord);

                subKey.SetValue (_nameIndxMonoD2,
                                 indxMonoD2,
                                 RegistryValueKind.DWord);

                subKey.SetValue (_nameIndxMonoS1,
                                 indxMonoS1,
                                 RegistryValueKind.DWord);

                subKey.SetValue (_nameIndxMonoS2,
                                 indxMonoS2,
                                 RegistryValueKind.DWord);

                subKey.SetValue (_nameIndxMonoT1,
                                 indxMonoT1,
                                 RegistryValueKind.DWord);

                subKey.SetValue (_nameIndxMonoT2,
                                 indxMonoT2,
                                 RegistryValueKind.DWord);

                if (flagUseMacros)
                    subKey.SetValue (_nameFlagUseMacros,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagUseMacros,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (pdlName == _subKeyPCLXL)
                {
                    if (flagSrcTextPat)
                        subKey.SetValue (_nameFlagSrcTextPat,
                                        _flagTrue,
                                        RegistryValueKind.DWord);
                    else
                        subKey.SetValue (_nameFlagSrcTextPat,
                                        _flagFalse,
                                        RegistryValueKind.DWord);
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a T y p e L o g P a g e                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current Misc Samples - Log Page data.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataTypeLogPage(String  pdlName,
                                               Int32   offsetLeft,
                                               Int32   offsetTop,
                                               Int32   pageHeight,
                                               Int32   pageWidth,
                                               Boolean flagFormAsMacro,
                                               Boolean flagAddStdPage)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);
            
            String key;

            key = _subKeyTools + "\\" + _subKeyToolsMiscSamples +
                                 "\\" + _subKeyLogPage +
                                 "\\" + pdlName;
            
            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue(_nameOffsetLeft,
                                offsetLeft,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameOffsetTop,
                                offsetTop,
                                RegistryValueKind.DWord);

                subKey.SetValue(_namePageHeight,
                                pageHeight,
                                RegistryValueKind.DWord);

                subKey.SetValue(_namePageWidth,
                                pageWidth,
                                RegistryValueKind.DWord);

                if (flagFormAsMacro)
                    subKey.SetValue(_nameFlagFormAsMacro,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagFormAsMacro,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagAddStdPage)
                    subKey.SetValue(_nameFlagAddStdPage,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagAddStdPage,
                                    _flagFalse,
                                    RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a T y p e P a t t e r n                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current Misc Samples - Pattern data.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataTypePattern(String  pdlName,
                                               Int32   indxPatternType,
                                               Boolean flagFormAsMacro)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);
            
            String key;

            key = _subKeyTools + "\\" + _subKeyToolsMiscSamples +
                                 "\\" + _subKeyPattern +
                                 "\\" + pdlName;
            
            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue(_nameIndxPatternType,
                                indxPatternType,
                                RegistryValueKind.DWord);

                if (flagFormAsMacro)
                    subKey.SetValue(_nameFlagFormAsMacro,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagFormAsMacro,
                                    _flagFalse,
                                    RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a T y p e T x t M o d                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current Misc Samples - Txt Mod data.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataTypeTxtMod(String  pdlName,
                                              Int32   indxTxtModType,
                                              Boolean flagFormAsMacro)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);
            
            String key;

            key = _subKeyTools + "\\" + _subKeyToolsMiscSamples +
                                 "\\" + _subKeyTxtMod +
                                 "\\" + pdlName;
            
            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue(_nameIndxTxtModType,
                                indxTxtModType,
                                RegistryValueKind.DWord);

                if (flagFormAsMacro)
                    subKey.SetValue(_nameFlagFormAsMacro,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagFormAsMacro,
                                    _flagFalse,
                                    RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a T y p e U n i c o d e                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current Misc Samples - Unicode data.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataTypeUnicode(String  pdlName,
                                               Int32   indxFont,
                                               PCLFonts.eVariant variant,
                                               Int32   codePoint,
                                               Boolean flagFormAsMacro)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);
            
            String key;

            key = _subKeyTools + "\\" + _subKeyToolsMiscSamples +
                                 "\\" + _subKeyUnicode +
                                 "\\" + pdlName;
            
            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue(_nameIndxFont,
                                indxFont,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxVariant,
                                (Int32)variant,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameCodePoint,
                                codePoint,
                                RegistryValueKind.DWord);

                if (flagFormAsMacro)
                    subKey.SetValue(_nameFlagFormAsMacro,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagFormAsMacro,
                                    _flagFalse,
                                    RegistryValueKind.DWord);
            }
        }
    }
}
