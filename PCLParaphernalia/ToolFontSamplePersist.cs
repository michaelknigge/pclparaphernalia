using Microsoft.Win32;
using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class manages persistent storage of options for the FontSample tool.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class ToolFontSamplePersist
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const String _mainKey                 = MainForm._regMainKey;

        const String _subKeyTools             = "Tools";
        const String _subKeyToolsFontSample   = "FontSample";
        const String _subKeyPCL5              = "PCL5"; 
        const String _subKeyPCL6              = "PCL6"; 
        const String _subKeyPCL               = "PCL"; 
        const String _subKeyPCLXL             = "PCLXL"; 
        const String _subKeyCustom            = "Custom"; 
        const String _subKeyDownload          = "Download"; 
        const String _subKeyPreset            = "Preset"; 
        const String _subKeyPrnDisk           = "PrnMassStore"; 

        const String _nameCaptureFile         = "CaptureFile";
        const String _nameDownloadFile        = "DownloadFile";
        const String _nameDownloadId          = "DownloadId";
        const String _nameMassStoreFontFile   = "Filename";
        const String _nameMassStoreFontId     = "AssociateFontId";
        const String _nameMassStoreMacroId    = "AssociateMacroId";
        const String _nameFlagBound           = "FlagBound";
        const String _nameFlagMassStoreViaMacro = "FlagLoadViaMacro";
        const String _nameFlagRamDataRemove   = "FlagRamDataRemove";
        const String _nameFlagDownloadRemove  = "FlagDownloadRemove";
        const String _nameFlagDetailsKnown    = "FlagDetailsKnown";
        const String _nameFlagFormAsMacro     = "FlagFormAsMacro";
        const String _nameFlagProportional    = "FlagProportional";
        const String _nameFlagScalable        = "FlagScalable";
        const String _nameFlagSelectById      = "FlagSelectById";
        const String _nameFlagShowC0Chars     = "FlagShowC0Chars";
        const String _nameFlagShowUserMapCodes   = "FlagShowUserMapCodes";
        const String _nameFlagShowMapCodesUCS2   = "FlagShowMapCodesUCS2";
        const String _nameFlagShowMapCodesUTF8   = "FlagShowMapCodesUTF8";
        const String _nameFlagOptGridVertical    = "FlagOptGridVertical";
        const String _nameFlagSymSetUserActEmbed = "FlagSymSetUserActEmbed";
        const String _nameFontName            = "FontName";
        const String _nameHeight              = "Height";
        const String _nameIndxFont            = "IndxFont";
        const String _nameIndxOrientation     = "IndxOrientation";
        const String _nameIndxPaperSize       = "IndxPaperSize";
        const String _nameIndxPaperType       = "IndxPaperType";
        const String _nameIndxPDL             = "IndxPDL";
        const String _nameIndxSymSet          = "IndxSymSet";
        const String _nameIndxVariant         = "IndxVariant";
        const String _namePitch               = "Pitch";
        const String _nameStyle               = "Style";
        const String _nameSymSetNumber        = "SymSetNumber";
        const String _nameSymSetUserFile      = "SymSetUserFile";
        const String _nameTypeface            = "Typeface";
        const String _nameWeight              = "Weight";

        const Int32 _flagFalse                = 0;
        const Int32 _flagTrue                 = 1;
        const Int32 _indexZero                = 0;

        const String _defaultCaptureFilePCL   = "CaptureFile_FontSamplePCL.prn";  
        const String _defaultCaptureFilePCLXL = "CaptureFile_FontSamplePCLXL.prn";  
        const String _defaultFontFilePCL      = "DefaultFontFile.sft";  
        const String _defaultFontFilePCLXL    = "DefaultFontFile.sfx";  
        const String _defaultFontName         = "Arial";
        const String _defaultSymSetUserFile   = "DefaultSymSetFile.pcl";  
        const String _defaultMassStoreFontFile = "DefaultFont.sfp";  

        const Int32 _sizeFactor                = 1000;
        const Int32 _defaultHeightPtsK         = 15 * _sizeFactor;
        const Int32 _defaultPitchPtsK          = 8  * _sizeFactor;
        const Int32 _defaultSymSetNo           = 14;
        const Int32 _defaultSoftFontIdPCL      = 101;
        const Int32 _defaultSoftFontIdMacroPCL = 201;
        const Int32 _defaultIndxVariant  = (Int32)(PCLFonts.eVariant.Regular);
        
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a C a p t u r e                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored FontSample capture file data.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataCapture (ToolCommonData.ePrintLang crntPDL,
                                            ref String captureFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String oldKey = _subKeyTools + "\\" + _subKeyToolsFontSample;
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
                                 "\\" + _subKeyToolsFontSample +
                                 "\\" + _subKeyPCL;

                using (RegistryKey subKey = keyMain.CreateSubKey(keyPCL))
                {
                    subKey.SetValue(_nameCaptureFile,
                                     oldFile,
                                     RegistryValueKind.String);
                }

                String keyPCLXL = _subKeyTools +
                                 "\\" + _subKeyToolsFontSample +
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
                String key = _subKeyTools + "\\" + _subKeyToolsFontSample +
                                            "\\" + _subKeyPCL;

                using (RegistryKey subKey = keyMain.CreateSubKey(key))
                {
                    captureFile = (String)subKey.GetValue (
                        _nameCaptureFile,
                        defWorkFolder + "\\" + _defaultCaptureFilePCL);
                }
            }
            else if (crntPDL == ToolCommonData.ePrintLang.PCLXL)
            {
                String key = _subKeyTools + "\\" + _subKeyToolsFontSample +
                                            "\\" + _subKeyPCLXL;

                using (RegistryKey subKey = keyMain.CreateSubKey(key))
                {
                    captureFile = (String)subKey.GetValue (
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
        // Retrieve stored FontSample common data.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataCommon (ref Int32 indxPDL,
                                           ref Boolean flagOptGridVertical)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            Int32 tmpInt;

            String key = _subKeyTools + "\\" + _subKeyToolsFontSample;

            //----------------------------------------------------------------//

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                if (Helper_RegKey.keyExists (subKey, _subKeyPCL5))
                    // update from v2_5_0_0
                    Helper_RegKey.renameKey(subKey, _subKeyPCL5, _subKeyPCL);

                if (Helper_RegKey.keyExists (subKey, _subKeyPCL6))
                    // update from v2_5_0_0
                    Helper_RegKey.renameKey(subKey, _subKeyPCL6, _subKeyPCLXL);

                indxPDL = (Int32)subKey.GetValue (_nameIndxPDL,
                                                 _indexZero);

                tmpInt = (Int32)subKey.GetValue(_nameFlagOptGridVertical,
                                                           _flagFalse);

                if (tmpInt == _flagFalse)
                    flagOptGridVertical = false;
                else
                    flagOptGridVertical = true;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a G e n e r a l                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored FontSample PCL or PCLXL general data.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataGeneral (String      pdlName,
                                            ref Int32   indxOrientation,
                                            ref Int32   indxPaperSize,
                                            ref Int32   indxPaperType,
                                            ref Int32   indxFont,
                                            ref Boolean flagFormAsMacro,
                                            ref Boolean flagShowC0Chars,
                                            ref Boolean flagShowMapCodesUCS2,
                                            ref Boolean flagShowMapCodesUTF8,
                                            ref Boolean flagSymSetUserActEmbed)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            Int32 tmpInt;

            key = _subKeyTools + "\\" + _subKeyToolsFontSample +
                                 "\\" + pdlName;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                //------------------------------------------------------------//
                // update from version 2.8.0 0                                //
                //------------------------------------------------------------//

                if (subKey.GetValue (_nameFlagShowUserMapCodes) != null)
                {
                    if (subKey.GetValue (_nameFlagShowMapCodesUCS2) == null)
                    {
                        subKey.SetValue (
                            _nameFlagShowMapCodesUCS2,
                            subKey.GetValue(_nameFlagShowUserMapCodes),
                            RegistryValueKind.DWord);
                    }

                    subKey.DeleteValue(_nameFlagShowUserMapCodes);
                }

                //------------------------------------------------------------//

                indxOrientation = (Int32)subKey.GetValue(_nameIndxOrientation,
                                                          _indexZero);
                indxPaperSize = (Int32)subKey.GetValue(_nameIndxPaperSize,
                                                          _indexZero);
                indxPaperType = (Int32)subKey.GetValue(_nameIndxPaperType,
                                                          _indexZero);
                indxFont = (Int32)subKey.GetValue(_nameIndxFont,
                                                          _indexZero);

                tmpInt = (Int32)subKey.GetValue(_nameFlagFormAsMacro,
                                                          _flagTrue);

                if (tmpInt == _flagFalse)
                    flagFormAsMacro = false;
                else
                    flagFormAsMacro = true;

                tmpInt = (Int32)subKey.GetValue(_nameFlagShowC0Chars,
                                                           _flagFalse);

                if (tmpInt == _flagFalse)
                    flagShowC0Chars = false;
                else
                    flagShowC0Chars = true;

                tmpInt = (Int32)subKey.GetValue(_nameFlagShowMapCodesUCS2,
                                                _flagFalse);

                if (tmpInt == _flagFalse)
                    flagShowMapCodesUCS2 = false;
                else
                    flagShowMapCodesUCS2 = true;

                tmpInt = (Int32)subKey.GetValue(_nameFlagShowMapCodesUTF8,
                                                _flagFalse);

                if (tmpInt == _flagFalse)
                    flagShowMapCodesUTF8 = false;
                else
                    flagShowMapCodesUTF8 = true;

                if (pdlName == _subKeyPCL)
                {
                    tmpInt = (Int32)subKey.GetValue(_nameFlagSymSetUserActEmbed,
                                                    _flagTrue);

                    if (tmpInt == _flagFalse)
                        flagSymSetUserActEmbed = false;
                    else
                        flagSymSetUserActEmbed = true;
                }
                else
                {
                    flagSymSetUserActEmbed = false;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a P C L C u s t o m                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored FontSample PCL data for 'custom' font.            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataPCLCustom  (ref Boolean flagProportional,
                                               ref Boolean flagScalable,
                                               ref Boolean flagBound,
                                               ref UInt16  style,
                                               ref UInt16  typeface,
                                               ref Int16   weight,
                                               ref Double  height,
                                               ref Double  pitch,
                                               ref Int32   indxSymSet,
                                               ref UInt16  symSetCustom,
                                               ref String  symSetUserFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            Int32 tmpInt;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            //----------------------------------------------------------------//

            key = _subKeyTools + "\\" + _subKeyToolsFontSample +
                                 "\\" + _subKeyPCL +
                                 "\\" + _subKeyCustom; 
                  
            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                tmpInt = (Int32)subKey.GetValue(_nameFlagProportional,
                                                _flagTrue);

                if (tmpInt == _flagFalse)
                    flagProportional = false;
                else
                    flagProportional = true;

                tmpInt = (Int32)subKey.GetValue(_nameFlagScalable,
                                                _flagTrue);

                if (tmpInt == _flagFalse)
                    flagScalable = false;
                else
                    flagScalable = true;

                tmpInt = (Int32)subKey.GetValue(_nameFlagBound,
                                                _flagTrue);

                if (tmpInt == _flagFalse)
                    flagBound = false;
                else
                    flagBound = true;

                tmpInt          = (Int32)subKey.GetValue(_nameStyle,
                                                         0);
                style           = (UInt16)tmpInt;

                tmpInt          = (Int32)subKey.GetValue(_nameTypeface,
                                                         0);
                typeface        = (UInt16)tmpInt;

                tmpInt          = (Int32)subKey.GetValue(_nameWeight,
                                                         0);
                weight          = (Int16)tmpInt;

                tmpInt          = (Int32)subKey.GetValue(_nameHeight,
                                                         _defaultHeightPtsK);
                height          = (Double)(tmpInt / _sizeFactor);

                tmpInt          = (Int32)subKey.GetValue(_namePitch,
                                                         _defaultPitchPtsK);
                pitch           = (Double)(tmpInt / _sizeFactor);

                indxSymSet      = (Int32)subKey.GetValue(_nameIndxSymSet,
                                                         _indexZero);

                tmpInt          = (Int32)subKey.GetValue(_nameSymSetNumber,
                                                         _defaultSymSetNo);
                symSetCustom    = (UInt16)tmpInt;

                symSetUserFile = (String)subKey.GetValue (_nameSymSetUserFile,
                                                          defWorkFolder + "\\" +
                                                          _defaultSymSetUserFile);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a P C L D o w n l o a d                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored FontSample PCL data for 'download' font.          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataPCLDownload  (ref String  downloadFile,
                                                 ref UInt16  downloadId,
                                                 ref Boolean flagDownloadRemove,
                                                 ref Boolean flagSelectById,
                                                 ref Double  height,
                                                 ref Double  pitch,
                                                 ref Int32   indxSymSet,
                                                 ref UInt16  symSetCustom,
                                                 ref String  symSetUserFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            Int32 tmpInt;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            //----------------------------------------------------------------//

            key = _subKeyTools + "\\" + _subKeyToolsFontSample +
                                 "\\" + _subKeyPCL + 
                                 "\\" + _subKeyDownload; 
                  
            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                downloadFile = (String)subKey.GetValue(_nameDownloadFile,
                                                       defWorkFolder + "\\" +
                                                       _defaultFontFilePCL);
                
                tmpInt       = (Int32)subKey.GetValue(_nameDownloadId,
                                                      1);
                downloadId   = (UInt16)tmpInt;

                tmpInt       = (Int32)subKey.GetValue(_nameFlagSelectById,
                                                      _flagTrue);
                if (tmpInt == _flagFalse)
                    flagSelectById = false;
                else
                    flagSelectById = true;

                tmpInt       = (Int32)subKey.GetValue(_nameFlagDownloadRemove,
                                                      _flagTrue);
                if (tmpInt == _flagFalse)
                    flagDownloadRemove = false;
                else
                    flagDownloadRemove = true;

                tmpInt       = (Int32)subKey.GetValue(_nameHeight,
                                                      _defaultHeightPtsK);
                height       = (Double)(tmpInt / _sizeFactor);

                tmpInt       = (Int32)subKey.GetValue(_namePitch,
                                                      _defaultPitchPtsK);
                pitch        = (Double)(tmpInt / _sizeFactor);

                indxSymSet   = (Int32)subKey.GetValue(_nameIndxSymSet,
                                                      _indexZero);

                tmpInt       = (Int32)subKey.GetValue(_nameSymSetNumber,
                                                      _defaultSymSetNo);
                symSetCustom = (UInt16)tmpInt;

                symSetUserFile = (String)subKey.GetValue (_nameSymSetUserFile,
                                                          defWorkFolder + "\\" +
                                                          _defaultSymSetUserFile);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a P C L P r e s e t                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored FontSample PCL data for 'preset' font.            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataPCLPreset  (ref Int32   indxFont,
                                               ref PCLFonts.eVariant variant,
                                               ref Double  height,
                                               ref Double  pitch,
                                               ref Int32   indxSymSet,
                                               ref UInt16  symSetCustom,
                                               ref String  symSetUserFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            Int32 tmpInt;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            //----------------------------------------------------------------//

            key = _subKeyTools + "\\" + _subKeyToolsFontSample +
                                 "\\" + _subKeyPCL +
                                 "\\" + _subKeyPreset; 
                  
            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                indxFont     = (Int32)subKey.GetValue(_nameIndxFont,
                                                      _indexZero);

                tmpInt       = (Int32)subKey.GetValue(_nameIndxVariant,
                                                      _defaultIndxVariant);
                variant      = (PCLFonts.eVariant)tmpInt;

                tmpInt       = (Int32)subKey.GetValue(_nameHeight,
                                                      _defaultHeightPtsK);
                height       = (Double)(tmpInt / _sizeFactor);

                tmpInt       = (Int32)subKey.GetValue(_namePitch,
                                                      _defaultPitchPtsK);
                pitch        = (Double)(tmpInt / _sizeFactor);

                indxSymSet   = (Int32)subKey.GetValue(_nameIndxSymSet,
                                                      _indexZero);

                tmpInt       = (Int32)subKey.GetValue(_nameSymSetNumber,
                                                      _defaultSymSetNo);
                symSetCustom = (UInt16)tmpInt;

                symSetUserFile = (String)subKey.GetValue (_nameSymSetUserFile,
                                                          defWorkFolder + "\\" +
                                                          _defaultSymSetUserFile);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a P C L P r n D i s k                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored FontSample PCL data for 'printer mass storage'     //
        // font.                                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataPCLPrnDisk (ref String  fontFilename,
                                               ref UInt16  fontId,
                                               ref UInt16  macroId,
                                               ref Boolean flagRamDataRemove,
                                               ref Boolean flagSelById,
                                               ref Boolean flagLoadViaMacro,
                                               ref Boolean flagDetailsKnown,
                                               ref Boolean flagProportional,
                                               ref Boolean flagScalable,
                                               ref Boolean flagBound,
                                               ref UInt16  style,
                                               ref UInt16  typeface,
                                               ref Int16   weight,
                                               ref Double  height,
                                               ref Double  pitch,
                                               ref Int32   indxSymSet,
                                               ref UInt16  symSetCustom,
                                               ref String  symSetUserFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            Int32 tmpInt;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            //----------------------------------------------------------------//

            key = _subKeyTools + "\\" + _subKeyToolsFontSample +
                                 "\\" + _subKeyPCL +
                                 "\\" + _subKeyPrnDisk;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                fontFilename = (String)subKey.GetValue(_nameMassStoreFontFile,
                                                       _defaultMassStoreFontFile);

                //------------------------------------------------------------//

                tmpInt = (Int32)subKey.GetValue(_nameMassStoreFontId,
                                                _defaultSoftFontIdPCL);
                fontId = (UInt16)tmpInt;

                //------------------------------------------------------------//

                tmpInt = (Int32)subKey.GetValue(_nameMassStoreMacroId,
                                                _defaultSoftFontIdMacroPCL);
                macroId = (UInt16)tmpInt;

                //------------------------------------------------------------//

                tmpInt = (Int32)subKey.GetValue (_nameFlagRamDataRemove,
                                                _flagFalse);

                if (tmpInt == _flagFalse)
                    flagRamDataRemove = false;
                else
                    flagRamDataRemove = true;

                //------------------------------------------------------------//

                tmpInt = (Int32)subKey.GetValue (_nameFlagSelectById,
                                                _flagFalse);

                if (tmpInt == _flagFalse)
                    flagSelById = false;
                else
                    flagSelById = true;

                //------------------------------------------------------------//

                tmpInt = (Int32)subKey.GetValue(_nameFlagMassStoreViaMacro,
                                                _flagFalse);

                if (tmpInt == _flagFalse)
                    flagLoadViaMacro = false;
                else
                    flagLoadViaMacro = true;

                //------------------------------------------------------------//

                tmpInt = (Int32)subKey.GetValue(_nameFlagDetailsKnown,
                                                _flagFalse);

                if (tmpInt == _flagFalse)
                    flagDetailsKnown = false;
                else
                    flagDetailsKnown = true;

                //------------------------------------------------------------//

                tmpInt = (Int32)subKey.GetValue(_nameFlagProportional,
                                                _flagFalse);

                if (tmpInt == _flagFalse)
                    flagProportional = false;
                else
                    flagProportional = true;

                //------------------------------------------------------------//

                tmpInt = (Int32)subKey.GetValue(_nameFlagScalable,
                                                _flagFalse);

                if (tmpInt == _flagFalse)
                    flagScalable = false;
                else
                    flagScalable = true;

                //------------------------------------------------------------//

                tmpInt = (Int32)subKey.GetValue(_nameFlagBound,
                                                _flagTrue);

                if (tmpInt == _flagFalse)
                    flagBound = false;
                else
                    flagBound = true;

                //------------------------------------------------------------//

                tmpInt = (Int32)subKey.GetValue(_nameStyle, 0);
                style = (UInt16)tmpInt;

                tmpInt = (Int32)subKey.GetValue(_nameTypeface, 3);
                typeface = (UInt16)tmpInt;

                tmpInt = (Int32)subKey.GetValue(_nameWeight, 0);
                weight = (Int16)tmpInt;

                tmpInt = (Int32)subKey.GetValue(_nameHeight, _defaultHeightPtsK);
                height = (Double)(tmpInt / _sizeFactor);

                tmpInt = (Int32)subKey.GetValue(_namePitch, _defaultPitchPtsK);
                pitch = (Double)(tmpInt / _sizeFactor);

                //------------------------------------------------------------//

                indxSymSet = (Int32)subKey.GetValue(_nameIndxSymSet, _indexZero);

                tmpInt = (Int32)subKey.GetValue(_nameSymSetNumber, _defaultSymSetNo);
                symSetCustom = (UInt16)tmpInt;

                symSetUserFile = (String)subKey.GetValue(_nameSymSetUserFile,
                                                          defWorkFolder + "\\" +
                                                          _defaultSymSetUserFile);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a P C L X L C u s t o m                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored FontSample PCLXL data for 'custom' font.            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataPCLXLCustom (ref String  fontName,
                                                ref Double  height,
                                                ref Int32   indxSymSet,
                                                ref UInt16  symSetCustom,
                                                ref String  symSetUserFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            Int32 tmpInt;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            key = _subKeyTools + "\\" + _subKeyToolsFontSample +
                                 "\\" + _subKeyPCLXL +
                                 "\\" + _subKeyCustom; 
                  
            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                fontName     = (String)subKey.GetValue(_nameFontName,
                                                       _defaultFontName);

                tmpInt       = (Int32)subKey.GetValue(_nameHeight,
                                                      _defaultHeightPtsK);
                height       = (Double)(tmpInt / _sizeFactor);

                indxSymSet   = (Int32)subKey.GetValue(_nameIndxSymSet,
                                                      _indexZero);

                tmpInt       = (Int32)subKey.GetValue(_nameSymSetNumber,
                                                      _defaultSymSetNo);
                symSetCustom = (UInt16)tmpInt;

                symSetUserFile = (String)subKey.GetValue(_nameSymSetUserFile,
                                                          defWorkFolder + "\\" +
                                                          _defaultSymSetUserFile);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a P C L X L D o w n l o a d                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored FontSample PCLXL data for 'download' font.          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataPCLXLDownload (ref String  downloadFile,
                                                  ref Boolean flagDownloadRemove,
                                                  ref Double  height,
                                                  ref Int32   indxSymSet,
                                                  ref UInt16  symSetCustom,
                                                  ref String  symSetUserFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            Int32 tmpInt;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            //----------------------------------------------------------------//

            key = _subKeyTools + "\\" + _subKeyToolsFontSample +
                                 "\\" + _subKeyPCLXL +
                                 "\\" + _subKeyDownload; 
                  
            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                downloadFile = (String)subKey.GetValue(_nameDownloadFile,
                                                       defWorkFolder + "\\" + 
                                                       _defaultFontFilePCLXL);

                tmpInt       = (Int32) subKey.GetValue(_nameFlagDownloadRemove,
                                                       _flagTrue);
                if (tmpInt == _flagFalse)
                    flagDownloadRemove = false;
                else
                    flagDownloadRemove = true;

                tmpInt       = (Int32)subKey.GetValue(_nameHeight,
                                                      _defaultHeightPtsK);
                height       = (Double)(tmpInt / _sizeFactor);

                indxSymSet   = (Int32)subKey.GetValue(_nameIndxSymSet,
                                                      _indexZero);

                tmpInt       = (Int32)subKey.GetValue(_nameSymSetNumber,
                                                      _defaultSymSetNo);
                symSetCustom = (UInt16)tmpInt;

                symSetUserFile = (String)subKey.GetValue(_nameSymSetUserFile,
                                          defWorkFolder + "\\" +
                                          _defaultSymSetUserFile);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a P C L X L P r e s e t                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored FontSample PCLXL data for 'preset' font.            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataPCLXLPreset (ref Int32   indxFont,
                                                ref PCLFonts.eVariant variant,
                                                ref Double  height,
                                                ref Int32   indxSymSet,
                                                ref UInt16  symSetCustom,
                                                ref String  symSetUserFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            Int32 tmpInt;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            key = _subKeyTools + "\\" + _subKeyToolsFontSample +
                                 "\\" + _subKeyPCLXL +
                                 "\\" + _subKeyPreset; 
                  
            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                indxFont     = (Int32)subKey.GetValue(_nameIndxFont,
                                                      _indexZero);

                tmpInt       = (Int32)subKey.GetValue(_nameIndxVariant,
                                                      _defaultIndxVariant);
                variant      = (PCLFonts.eVariant)tmpInt;

                tmpInt       = (Int32)subKey.GetValue(_nameHeight,
                                                      _defaultHeightPtsK);
                height       = (Double)(tmpInt / _sizeFactor);

                indxSymSet   = (Int32)subKey.GetValue(_nameIndxSymSet,
                                                      _indexZero);

                tmpInt       = (Int32)subKey.GetValue(_nameSymSetNumber,
                                                      _defaultSymSetNo);
                symSetCustom = (UInt16)tmpInt;

                symSetUserFile = (String)subKey.GetValue(_nameSymSetUserFile,
                                                          defWorkFolder + "\\" +
                                                          _defaultSymSetUserFile);
            }
        }
       
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a C a p t u r e                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current FontSample capture file data.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataCapture (ToolCommonData.ePrintLang crntPDL,
                                            String captureFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            if (crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                String key = _subKeyTools + "\\" + _subKeyToolsFontSample +
                                            "\\" + _subKeyPCL;

                using (RegistryKey subKey = keyMain.CreateSubKey(key))
                {
                    if (captureFile != null)
                    {
                        subKey.SetValue (_nameCaptureFile,
                                         captureFile,
                                         RegistryValueKind.String);
                    }
                }
            }
            else if (crntPDL == ToolCommonData.ePrintLang.PCLXL)
            {
                String key = _subKeyTools + "\\" + _subKeyToolsFontSample +
                                            "\\" + _subKeyPCLXL;

                using (RegistryKey subKey = keyMain.CreateSubKey(key))
                {
                    if (captureFile != null)
                    {
                        subKey.SetValue (_nameCaptureFile,
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
        // Store current FontSample common data.                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataCommon (Int32 indxPDL,
                                           Boolean flagOptGridVertical)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsFontSample;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue(_nameIndxPDL,
                                indxPDL,
                                RegistryValueKind.DWord);

                if (flagOptGridVertical)
                    subKey.SetValue(_nameFlagOptGridVertical,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagOptGridVertical,
                                    _flagFalse,
                                    RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a G e n e r a l                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current FontSample PCL or PCLXL general data.                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataGeneral (String    pdlName,
                                            Int32     indxOrientation,
                                            Int32     indxPaperSize,
                                            Int32     indxPaperType,
                                            Int32     indxFont,
                                            Boolean   flagFormAsMacro,
                                            Boolean   flagShowC0Chars,
                                            Boolean   flagShowMapCodesUCS2,
                                            Boolean   flagShowMapCodesUTF8,
                                            Boolean   flagSymSetUserActEmbed)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);
            
            String key;

            key = _subKeyTools + "\\" + _subKeyToolsFontSample +
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

                subKey.SetValue(_nameIndxFont,
                                indxFont,
                                RegistryValueKind.DWord);

                if (flagFormAsMacro)
                    subKey.SetValue(_nameFlagFormAsMacro,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagFormAsMacro,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagShowC0Chars)
                    subKey.SetValue(_nameFlagShowC0Chars,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagShowC0Chars,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagShowMapCodesUCS2)
                    subKey.SetValue(_nameFlagShowMapCodesUCS2,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagShowMapCodesUCS2,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagShowMapCodesUTF8)
                    subKey.SetValue(_nameFlagShowMapCodesUTF8,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagShowMapCodesUTF8,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (pdlName == _subKeyPCL)
                {
                    if (flagSymSetUserActEmbed)
                        subKey.SetValue(_nameFlagSymSetUserActEmbed,
                                        _flagTrue,
                                        RegistryValueKind.DWord);
                    else
                        subKey.SetValue(_nameFlagSymSetUserActEmbed,
                                        _flagFalse,
                                        RegistryValueKind.DWord);
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a P C L C u s t o m                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store FontSample PCL data for 'custom' font.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataPCLCustom (Boolean flagProportional,
                                              Boolean flagScalable,
                                              Boolean flagBound,
                                              UInt16  style,
                                              UInt16  typeface,
                                              Int16   weight,
                                              Double  height,
                                              Double  pitch,
                                              Int32   indxSymSet,
                                              UInt16  symSetCustom,
                                              String  symSetUserFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsFontSample +
                                 "\\" + _subKeyPCL +
                                 "\\" + _subKeyCustom;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                if (flagProportional)
                    subKey.SetValue(_nameFlagProportional,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagProportional,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagScalable)
                    subKey.SetValue(_nameFlagScalable,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagScalable,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagBound)
                    subKey.SetValue(_nameFlagBound,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagBound,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                subKey.SetValue(_nameStyle,
                                style,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameTypeface,
                                typeface,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameWeight,
                                weight,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameHeight,
                                height * _sizeFactor,
                                RegistryValueKind.DWord);

                subKey.SetValue(_namePitch,
                                pitch * _sizeFactor,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxSymSet,
                                indxSymSet,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameSymSetNumber,
                                symSetCustom,
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
        // s a v e D a t a P C L D o w n l o a d                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store FontSample PCL data for 'download' font.                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataPCLDownload (String  downloadFile,
                                                UInt16  downloadId,
                                                Boolean flagDownloadRemove,
                                                Boolean flagSelectById,
                                                Double  height,
                                                Double  pitch,
                                                Int32   indxSymSet,
                                                UInt16  symSetCustom,
                                                String  symSetUserFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsFontSample +
                                 "\\" + _subKeyPCL +
                                 "\\" + _subKeyDownload;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                if (downloadFile != null)
                {
                    subKey.SetValue (_nameDownloadFile,
                                    downloadFile,
                                    RegistryValueKind.String);
                }

                subKey.SetValue(_nameDownloadId,
                                downloadId,
                                RegistryValueKind.DWord);

                if (flagDownloadRemove)
                    subKey.SetValue(_nameFlagDownloadRemove,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagDownloadRemove,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagSelectById)
                    subKey.SetValue(_nameFlagSelectById,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagSelectById,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                subKey.SetValue(_nameHeight,
                                height * _sizeFactor,
                                RegistryValueKind.DWord);

                subKey.SetValue(_namePitch,
                                pitch * _sizeFactor,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxSymSet,
                                indxSymSet,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameSymSetNumber,
                                symSetCustom,
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
        // s a v e D a t a P C L P r e s e t                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store FontSample PCL data for 'preset' font.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataPCLPreset (Int32             indxFont,
                                              PCLFonts.eVariant variant,
                                              Double            height,
                                              Double            pitch,
                                              Int32             indxSymSet,
                                              UInt16            symSetCustom,
                                              String            symSetUserFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsFontSample +
                                 "\\" + _subKeyPCL +
                                 "\\" + _subKeyPreset;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue(_nameIndxFont,
                                indxFont,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxVariant,
                                (Int32)variant,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameHeight,
                                height * _sizeFactor,
                                RegistryValueKind.DWord);

                subKey.SetValue(_namePitch,
                                pitch * _sizeFactor,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxSymSet,
                                indxSymSet,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameSymSetNumber,
                                symSetCustom,
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
        // s a v e D a t a P C L P r n D i s k                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store FontSample PCL data for 'printer mass storage' font.         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataPCLPrnDisk (String  fontFilename,
                                               UInt16  fontId,
                                               UInt16  macroId,
                                               Boolean flagRamDataRemove,
                                               Boolean flagSelById,
                                               Boolean flagLoadViaMacro, 
                                               Boolean flagDetailsKnown,
                                               Boolean flagProportional,
                                               Boolean flagScalable,
                                               Boolean flagBound,
                                               UInt16  style,
                                               UInt16  typeface,
                                               Int16   weight,
                                               Double  height,
                                               Double  pitch,
                                               Int32   indxSymSet,
                                               UInt16  symSetCustom,
                                               String  symSetUserFile)
        {

            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsFontSample +
                                 "\\" + _subKeyPCL +
                                 "\\" + _subKeyPrnDisk;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                if (fontFilename != null)
                {
                    subKey.SetValue(_nameMassStoreFontFile,
                                     fontFilename,
                                     RegistryValueKind.String);
                }

                subKey.SetValue(_nameMassStoreFontId,
                                fontId,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameMassStoreMacroId,
                                macroId,
                                RegistryValueKind.DWord);

                if (flagRamDataRemove)
                    subKey.SetValue (_nameFlagRamDataRemove,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagRamDataRemove,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagSelById)
                    subKey.SetValue (_nameFlagSelectById,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue (_nameFlagSelectById,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagLoadViaMacro)
                    subKey.SetValue(_nameFlagMassStoreViaMacro,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagMassStoreViaMacro,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagDetailsKnown)
                    subKey.SetValue(_nameFlagDetailsKnown,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagDetailsKnown,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagProportional)
                    subKey.SetValue(_nameFlagProportional,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagProportional,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagScalable)
                    subKey.SetValue(_nameFlagScalable,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagScalable,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                if (flagBound)
                    subKey.SetValue(_nameFlagBound,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagBound,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                subKey.SetValue(_nameStyle,
                                style,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameTypeface,
                                typeface,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameWeight,
                                weight,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameHeight,
                                height * _sizeFactor,
                                RegistryValueKind.DWord);

                subKey.SetValue(_namePitch,
                                pitch * _sizeFactor,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxSymSet,
                                indxSymSet,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameSymSetNumber,
                                symSetCustom,
                                RegistryValueKind.DWord);

                if (symSetUserFile != null)
                {
                    subKey.SetValue(_nameSymSetUserFile,
                                     symSetUserFile,
                                     RegistryValueKind.String);
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a P C L X L C u s t o m                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store FontSample PCLXL data for 'custom' font.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataPCLXLCustom (String  fontName,
                                                Double  height,
                                                Int32   indxSymSet,
                                                UInt16  symSetCustom,
                                                String  symSetUserFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsFontSample +
                                 "\\" + _subKeyPCLXL +
                                 "\\" + _subKeyCustom;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                if (fontName != null)
                {
                    subKey.SetValue (_nameFontName,
                                    fontName,
                                    RegistryValueKind.String);
                }

                subKey.SetValue(_nameHeight,
                                height * _sizeFactor,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxSymSet,
                                indxSymSet,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameSymSetNumber,
                                symSetCustom,
                                RegistryValueKind.DWord);

                if (symSetUserFile != null)
                {
                    subKey.SetValue(_nameSymSetUserFile,
                                     symSetUserFile,
                                     RegistryValueKind.String);
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a P C L X L D o w n l o a d                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store FontSample PCLXL data for 'download' font.                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataPCLXLDownload (String  downloadFile,
                                                  Boolean flagDownloadRemove,
                                                  Double  height,
                                                  Int32   indxSymSet,
                                                  UInt16  symSetCustom,
                                                  String  symSetUserFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsFontSample +
                                 "\\" + _subKeyPCLXL +
                                 "\\" + _subKeyDownload;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                if (downloadFile != null)
                {
                    subKey.SetValue (_nameDownloadFile,
                                    downloadFile,
                                    RegistryValueKind.String);
                }

                if (flagDownloadRemove)
                    subKey.SetValue(_nameFlagDownloadRemove,
                                    _flagTrue,
                                    RegistryValueKind.DWord);
                else
                    subKey.SetValue(_nameFlagDownloadRemove,
                                    _flagFalse,
                                    RegistryValueKind.DWord);

                subKey.SetValue(_nameHeight,
                                height * _sizeFactor,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxSymSet,
                                indxSymSet,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameSymSetNumber,
                                symSetCustom,
                                RegistryValueKind.DWord);

                if (symSetUserFile != null)
                {
                    subKey.SetValue(_nameSymSetUserFile,
                                     symSetUserFile,
                                     RegistryValueKind.String);
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a P C L X L P r e s e t                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store FontSample PCLXL data for 'preset' font.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataPCLXLPreset (Int32             indxFont,
                                                PCLFonts.eVariant variant,
                                                Double            height,
                                                Int32             indxSymSet,
                                                UInt16            symSetCustom,
                                                String            symSetUserFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsFontSample +
                                 "\\" + _subKeyPCLXL +
                                 "\\" + _subKeyPreset;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue(_nameIndxFont,
                                indxFont,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxVariant,
                                (Int32)variant,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameHeight,
                                height * _sizeFactor,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxSymSet,
                                indxSymSet,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameSymSetNumber,
                                symSetCustom,
                                RegistryValueKind.DWord);

                if (symSetUserFile != null)
                {
                    subKey.SetValue(_nameSymSetUserFile,
                                     symSetUserFile,
                                     RegistryValueKind.String);
                }
            }
        }
    }
}
