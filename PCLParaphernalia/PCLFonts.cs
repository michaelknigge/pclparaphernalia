using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines a set of PCL Font objects.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class PCLFonts
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public enum eFontType
        {
            PresetTypeface,     // standard typeface
            PresetFamily,       // base typeface value or font with >1 versions
            PresetFamilyMember, // multiple versions (e.g. standard & condensed)
            Custom,             // <select by characteristics>
            Download,           // <download soft font>
            PrnDisk             // <printer disk store>
        }

        public enum eVariant
        {
            Regular,
            Italic,
            Bold,
            BoldItalic
        }

        public enum eWidthType :  SByte
        {
            UltraCompressed = -5,
            ExtraCompressed = -4,
            Compressed = -3,
            Condensed = -2,
            Normal = 0,
            Expanded = 2,
            ExtraExpanded = 3
        }

        public enum eStrokeWeight :  SByte
        {
            UltraThin = -7,
            ExtraThin = -6,
            Thin = -5,
            ExtraLight = -4,
            Light = -3,
            DemiLight = -2,
            SemiLight = -1,
            Medium = 0,
            SemiBold,
            DemiBold,
            Bold,
            ExtraBold,
            Black,
            ExtraBlack,
            UltraBlack
        }

        public enum eStylePosture :  Byte
        {
            Upright = 0,
            Italic,
            ItalicAlt,
            Reserved
        }

        public enum eStyleWidth : Byte
        {
             Normal = 0,
             Condensed,
             Compressed,
             ExtraCompressed,
             UltraCompressed,
             Reserved,
             Expanded,
             ExtraExpanded
        }

        public enum eStyleStructure :  UInt16
        {
            Solid = 0,
            Outline,
            Inline,
            Contour,
            Solid_Shadow,
            Outline_Shadow,
            Inline_Shadow,
            Contour_Shadow,
            Pattern,
            Pattern1,
            Pattern2,
            Pattern3,
            Pattern_Shadow,
            Pattern1_Shadow,
            Pattern2_Shadow,
            Pattern3_Shadow,
            Inverse,
            Inverse_Border,
            Unknown = 31
        }

        private static UInt16[] symSets_Dummy = new UInt16[]
            { 0xffff };

        private static UInt16[] symSets_Unicode = new UInt16[]
            { PCLSymbolSets.translateIdToKind1("18N") };

        private static UInt16 [] symSets_Europe = new UInt16 []
            { PCLSymbolSets.translateIdToKind1("0D"),
              PCLSymbolSets.translateIdToKind1("0H"),
              PCLSymbolSets.translateIdToKind1("0I"),
              PCLSymbolSets.translateIdToKind1("0N"),
              PCLSymbolSets.translateIdToKind1("0S"),
              PCLSymbolSets.translateIdToKind1("0U"),
              PCLSymbolSets.translateIdToKind1("1E"),
              PCLSymbolSets.translateIdToKind1("1F"),
              PCLSymbolSets.translateIdToKind1("1G"),
              PCLSymbolSets.translateIdToKind1("1U"),
              PCLSymbolSets.translateIdToKind1("2N"),
              PCLSymbolSets.translateIdToKind1("2S"),
              PCLSymbolSets.translateIdToKind1("3R"),
              PCLSymbolSets.translateIdToKind1("4N"),
              PCLSymbolSets.translateIdToKind1("4U"),
              PCLSymbolSets.translateIdToKind1("5M"),
              PCLSymbolSets.translateIdToKind1("5N"),
              PCLSymbolSets.translateIdToKind1("5T"),
              PCLSymbolSets.translateIdToKind1("6J"),
              PCLSymbolSets.translateIdToKind1("6N"),
              PCLSymbolSets.translateIdToKind1("7H"),
              PCLSymbolSets.translateIdToKind1("7J"),
              PCLSymbolSets.translateIdToKind1("8G"),
              PCLSymbolSets.translateIdToKind1("8H"),
              PCLSymbolSets.translateIdToKind1("8M"),
              PCLSymbolSets.translateIdToKind1("8U"),
              PCLSymbolSets.translateIdToKind1("9E"),
              PCLSymbolSets.translateIdToKind1("9G"),
              PCLSymbolSets.translateIdToKind1("9J"),
              PCLSymbolSets.translateIdToKind1("9N"),
              PCLSymbolSets.translateIdToKind1("9R"),
              PCLSymbolSets.translateIdToKind1("9T"),
              PCLSymbolSets.translateIdToKind1("9U"),
              PCLSymbolSets.translateIdToKind1("10G"),
              PCLSymbolSets.translateIdToKind1("10J"),
              PCLSymbolSets.translateIdToKind1("10N"),
              PCLSymbolSets.translateIdToKind1("10U"),
              PCLSymbolSets.translateIdToKind1("11U"),
              PCLSymbolSets.translateIdToKind1("12G"),
              PCLSymbolSets.translateIdToKind1("12J"),
              PCLSymbolSets.translateIdToKind1("12N"),
              PCLSymbolSets.translateIdToKind1("12U"),
              PCLSymbolSets.translateIdToKind1("13U"),
              PCLSymbolSets.translateIdToKind1("14R"),
              PCLSymbolSets.translateIdToKind1("15H"),
  //          PCLSymbolSets.translateIdToKind1("15Q"), // special: M553x / M475dn
              PCLSymbolSets.translateIdToKind1("15U"),
              PCLSymbolSets.translateIdToKind1("17U"),
              PCLSymbolSets.translateIdToKind1("19L"),
              PCLSymbolSets.translateIdToKind1("19U"),
              PCLSymbolSets.translateIdToKind1("26U")
            };

        private static UInt16[] symSets_Europe_Not_Hebrew_Greek_Cyrillic = new UInt16[]
            { PCLSymbolSets.translateIdToKind1("0D"),
              PCLSymbolSets.translateIdToKind1("0I"),
              PCLSymbolSets.translateIdToKind1("0N"),
              PCLSymbolSets.translateIdToKind1("0S"),
              PCLSymbolSets.translateIdToKind1("0U"),
              PCLSymbolSets.translateIdToKind1("1E"),
              PCLSymbolSets.translateIdToKind1("1F"),
              PCLSymbolSets.translateIdToKind1("1G"),
              PCLSymbolSets.translateIdToKind1("1U"),
              PCLSymbolSets.translateIdToKind1("2N"),
              PCLSymbolSets.translateIdToKind1("2S"),
              PCLSymbolSets.translateIdToKind1("4N"),
              PCLSymbolSets.translateIdToKind1("4U"),
              PCLSymbolSets.translateIdToKind1("5M"),
              PCLSymbolSets.translateIdToKind1("5N"),
              PCLSymbolSets.translateIdToKind1("5T"),
              PCLSymbolSets.translateIdToKind1("6J"),
              PCLSymbolSets.translateIdToKind1("6N"),
              PCLSymbolSets.translateIdToKind1("7H"),
              PCLSymbolSets.translateIdToKind1("7J"),
              PCLSymbolSets.translateIdToKind1("8M"),
              PCLSymbolSets.translateIdToKind1("8U"),
              PCLSymbolSets.translateIdToKind1("9E"),
              PCLSymbolSets.translateIdToKind1("9J"),
              PCLSymbolSets.translateIdToKind1("9N"),
              PCLSymbolSets.translateIdToKind1("9T"),
              PCLSymbolSets.translateIdToKind1("9U"),
              PCLSymbolSets.translateIdToKind1("10J"),
              PCLSymbolSets.translateIdToKind1("10U"),
              PCLSymbolSets.translateIdToKind1("11U"),
              PCLSymbolSets.translateIdToKind1("12J"),
              PCLSymbolSets.translateIdToKind1("12U"),
              PCLSymbolSets.translateIdToKind1("13U"),
              PCLSymbolSets.translateIdToKind1("15U"),
              PCLSymbolSets.translateIdToKind1("17U"),
              PCLSymbolSets.translateIdToKind1("19L"),
              PCLSymbolSets.translateIdToKind1("19U"),
              PCLSymbolSets.translateIdToKind1("26U")
            };

        private static UInt16[] symSets_Arabic = new UInt16[]
            { PCLSymbolSets.translateIdToKind1("0U"),
              PCLSymbolSets.translateIdToKind1("5M"),
              PCLSymbolSets.translateIdToKind1("8M"),
              PCLSymbolSets.translateIdToKind1("8V"),
              PCLSymbolSets.translateIdToKind1("9V"),
              PCLSymbolSets.translateIdToKind1("10V"),
              PCLSymbolSets.translateIdToKind1("11N")
            };

        private static UInt16[] symSets_Hebrew = new UInt16[]
            { PCLSymbolSets.translateIdToKind1("0H"),
              PCLSymbolSets.translateIdToKind1("0U"),
              PCLSymbolSets.translateIdToKind1("5M"),
              PCLSymbolSets.translateIdToKind1("7H"),
              PCLSymbolSets.translateIdToKind1("8H"),
              PCLSymbolSets.translateIdToKind1("8M"),
              PCLSymbolSets.translateIdToKind1("15H")
  //          PCLSymbolSets.translateIdToKind1("15Q"), // special: M553x / M475dn
            };

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static List<PCLFont> _fonts =
            new List<PCLFont> ();

        private static Int32 _fontsCount;
        private static Int32 _fontsCountCustom;
        private static Int32 _fontsCountDownload;
        private static Int32 _fontsCountPrnDisk;
        private static Int32 _fontsCountPreset;

        private static Int16 _indxFontArial;
        private static Int16 _indxFontCourier;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L F o n t s                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        static PCLFonts()
        {
            populateTable();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d i s p l a y F o n t L i s t                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Display list of commands in nominated data grid.                   //
        //                                                                    //
        // Used (only) by the Print Languages tool, so included are fonts of  //
        // types:                                                             //
        //                                                                    //
        //  PresetTypeface      - most fonts are of this type.                //
        //                                                                    //
        //  PresetFamilyMember  - fonts for which there is more than one with //
        //                        the same PCL typeface identifier.           //
        //                        e.g. Helvetica & Univers                    //
        //                             have Narrow & Condensed versions       //
        //                             (respectively) as well as Regular.     //
        //                        e.g. Line Printer                           //
        //                             has several versions bound to          //
        //                             different symbol sets.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 displayFontList(DataGrid grid)
        {
            Int32 count = 0;

            foreach (PCLFont v in _fonts)
            {
                if ((v.Type == eFontType.PresetTypeface) ||
                    (v.Type == eFontType.PresetFamilyMember))
                {
                    count++;
                    grid.Items.Add (v);
                }
            }

            return count;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C o u n t                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return count of Font definitions.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 getCount()
        {
            return _fontsCount;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C o u n t U n i q u e                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return count of Font definitions for individual or pseudo font     //
        // entries.                                                           //
        //                                                                    //
        // Used (only) by the Font Sample tool, so included are fonts of      //
        // types:                                                             //
        //                                                                    //
        //  Custom              - single identifying pseudo entry.            //
        //                                                                    //
        //  Download            - single identifying pseudo entry.            //
        //                                                                    //
        //  PrnDisk             - single identifying pseudo entry.            //
        //                                                                    //
        //  PresetTypeface      - most fonts are of this type.                //
        //                                                                    //
        //  PresetFamilyMember  - fonts for which there is more than one with //
        //                        the same PCL typeface identifier.           //
        //                        e.g. Helvetica & Univers                    //
        //                             have Narrow & Condensed versions       //
        //                             (respectively) as well as Regular.     //
        //                        e.g. Line Printer                           //
        //                             has several versions bound to          //
        //                             different symbol sets.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 getCountUnique()
        {
            return (_fontsCountCustom +
                    _fontsCountDownload +
                    _fontsCountPrnDisk +
                    _fontsCountPreset);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t H P G L 2 F o n t D e f                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the HP-GL/2 font definition command for the indicated       //
        // variant and size (height or pitch as appropriate) of the font      //
        // selected from the list of pre-defined fonts.                       //
        // The HP-GL/2 command mnemonic is not included, since this may be SD //
        // (for Standard font) or AD (for Alternatr font).                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getHPGL2FontDef (Int32    indxFont,
                                              eVariant variant,
                                              UInt16   symbolSet,
                                              Double   height,
                                              Double   pitch)
        {
            return _fonts [indxFont].getHPGL2FontDef (variant, symbolSet,
                                                      height, pitch);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I n d e x F o r F o n t A r i a l                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return imdex number of font Arial.                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int16 getIndexForFontArial ()
        {
            return _indxFontArial;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I n d e x F o r F o n t C o u r i e r                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return imdex number of font Courier.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int16 getIndexForFontCourier ()
        {
            return _indxFontCourier;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I n d e x F o r N a m e                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return index number of font with specified name.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int16 getIndexForName (String fontName)
        {
            Boolean fontNameKnown = false;

            Int16 indxFont = -1;

            foreach (PCLFont v in _fonts)
            {
                if (((v.Type == eFontType.PresetTypeface)
                                  ||
                     (v.Type == eFontType.PresetFamily))
                                  &&
                    (v.Name == fontName))
                {
                    fontNameKnown = true;
                    indxFont = v.IndexNo;
                    break;
                }
            }

            if (!fontNameKnown)
                indxFont = -1;

            return indxFont;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t N a m e                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return identifying (reference) name of the font.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getName(Int32 indxFont)
        {
            return _fonts[indxFont].Name;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t N a m e F o r I d P C L                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return identifying (reference) name of the font associated with    //
        // the specified PCL typeface identifier.                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean getNameForIdPCL (UInt16 typefaceId,
                                               ref String name)
        {
            Boolean typefaceKnown = false;

            foreach (PCLFont v in _fonts)
            {
                if (((v.Type == eFontType.PresetTypeface)
                                  ||
                     (v.Type == eFontType.PresetFamily))
                                  &&
                    (v.Typeface == typefaceId))
                {
                    typefaceKnown = true;
                    name = v.Name;
                    break;
                }
            }

            if (!typefaceKnown)
                name = "unknown";

            return typefaceKnown;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t P C L C o n t o u r R a t i o                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the PCL font contour ratio value.                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Byte getPCLContourRatio(Int32 indxFont)
        {
            return _fonts[indxFont].getPCLContourRatio();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t P C L F o n t S e l e c t                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the PCL font selection sequence for the indicated variant   //
        // and size (height or pitch as appropriate) of the font selected     //
        // from the list of pre-defined fonts.                                //
        // ... except for the root '<esc>(' prefix.                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getPCLFontSelect (Int32    indxFont,
                                               eVariant variant,
                                               Double   height,
                                               Double   pitch)
        {
            return _fonts[indxFont].getPCLFontSelect(variant, height, pitch);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t P C L H e i g h t                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the PCL font height characteristic value.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Double getPCLHeight(Int32 indxFont)
        {
            return _fonts[indxFont].getPCLHeight();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t P C L P i t c h                                       I      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the PCL font pitch characteristic value.                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Double getPCLPitch(Int32 indxFont)
        {
            return _fonts[indxFont].getPCLPitch();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t P C L P i t c h                                      I I     //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the PCL font pitch characteristic value equivalent to the   //
        // supplied height.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Double getPCLPitch(Int32  indxFont,
                                          Double ptSize)
        {
            return _fonts[indxFont].getPCLPitch(ptSize);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t P C L S p a c i n g                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the PCL font spacing characteristic value.                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Byte getPCLSpacing(Int32 indxFont)
        {
            return _fonts[indxFont].getPCLSpacing();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t P C L S t y l e                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the PCL font style characteristic value.                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt16 getPCLStyle(Int32    indxFont,
                                          eVariant variant)
        {
            return _fonts[indxFont].getPCLStyle(variant);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t P C L T y p e f a c e                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the PCL font typeface characteristic value.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt16 getPCLTypeface(Int32 indxFont)
        {
            return _fonts[indxFont].Typeface;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t P C L W e i g h t                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the PCL font weight characteristic value.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int16 getPCLWeight(Int32    indxFont,
                                          eVariant variant)
        {
            return _fonts[indxFont].getPCLWeight(variant);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t P C L X L H e i g h t                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the PCL XL font height characteristic value.                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Double getPCLXLHeight(Int32 indxFont)
        {
            return _fonts[indxFont].getPCLXLHeight();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t P C L X L N a m e                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the PCL XL font name for the selected font.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getPCLXLName (Int32    indxFont,
                                           eVariant variant)
        {
            return _fonts[indxFont].getPCLXLName(variant);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t P r e s e t F o n t D a t a                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return identifying (reference) name and PCL typeface identifier    //
        // of the font.                                                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean getPresetFontData(Int32 indxFont,
                                                ref UInt16 typeface,
                                                ref String fontName)
        {
            Boolean typefacePreset;

            typefacePreset = _fonts[indxFont].getPCLFontIdData (ref typeface,
                                                                ref fontName);

            return typefacePreset;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t S y m b o l S e t N u m b e r                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the PCL XL symbol set value.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt16 getSymbolSetNumber(Int32 indxFont)
        {
            return _fonts[indxFont].getSymbolSetNumber();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t T y p e                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return type of font.                                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static eFontType getType (Int32 indxFont)
        {
            return _fonts [indxFont].Type;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i s B o u n d F o n t                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return a value indicating whether or not the font is bound to a    //
        // particular symbol set.                                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean isBoundFont(Int32 indxFont)
        {
            return _fonts[indxFont].isBoundFont();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i s P r e s e t F o n t                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return a value indicating whether or not the font is one of the    //
        // preset ones (i.e. NOT the <custom> or <download>) entry).          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean isPresetFont(Int32 indxFont)
        {
            return _fonts[indxFont].isPresetFont();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i s P r o p o r t i o n a l F o n t                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return a value indicating if the font is proportionally-spaced, or //
        // fixed-pitch.                                                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean isProportionalFont(Int32 indxFont)
        {
            return _fonts[indxFont].isProportionalFont();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i s S c a l a b l e F o n t                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return a value indicating if the font is scalable, or is a bitmap  //
        // font (available only in a particular fixed size).                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean isScalableFont(Int32 indxFont)
        {
            return _fonts[indxFont].Scalable;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i s S y m S e t i n L i s t                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return a value indicating whether or not the specified symbol set  //
        // (Kind1) number is in the list of those (probably) supported by     //
        // the specified font.                                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean isSymSetInList (Int32 indxFont,
                                              UInt16 symSetNo)
        {
            return _fonts[indxFont].isSymSetInList(symSetNo);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p o p u l a t e T a b l e                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Populate the table of Fonts (Typefaces).                           //
        //                                                                    //
        // The font type fopr each entry is set to one of the following:      //
        //                                                                    //
        //  Custom              - for "<select by characteristics>" entry     //
        //                                                                    //
        //  Download            - for "<download soft font>" entry.           //
        //                                                                    //
        //  PrnDisk             - for "<load from printer storage>" entry.    //
        //                                                                    //
        //  PresetFamily        - either the old 'base' value font:           //
        //                        e.g. Courier base value = typeface 3        //
        //                                                                    // 
        //                      - or a font for which there is more than one  //
        //                        with the same PCL typeface identifier       // 
        //                        e.g. Helvetica & Univers                    //
        //                             Have Narrow & Condensed versions       //
        //                             (respectively) as well as standard.    //
        //                        e.g. Line Printer                           //
        //                             Has several versions bound to          //
        //                             different symbol sets.                 //     
        //                                                                    //
        //                       Fonts of this type are NOT included in the   //
        //                       set of Preset fonts used by the Font Sample  //
        //                       and Print Languages tools.                   //
        //                                                                    //
        //  PresetFamilyMember  - individual members of a family for which    //
        //                        there is more than one with the same PCL    //
        //                        typeface identifier.                        //
        //                        e.g. Helvetica & Univers                    //
        //                             Have Narrow & Condensed versions       //
        //                             (respectively) as well as the standard //
        //                             version.                               //
        //                             Each version may have the usual        //
        //                             variants (regular, italic, bold, and   //
        //                             bold italic).                          //
        //                                                                    //
        //                       Fonts of this type are NOT included in the   //
        //                       set of Preset fonts referenced by the        //
        //                       PRN File Analyse tool.                       //
        //                                                                    //
        //  PresetTypeface      - most fonts are of this type.                //
        //                                                                    //
        //  Soft                - for "<soft font>" entry                     //
        //                        this will usually be a soft font download,  //
        //                        but could also be a font loaded from        //
        //                        printer mass storage (disk, etc.).          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void populateTable()
        {
            Int16 fontIndex = 0;

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.Custom,
                "<select by characteristics>",
                false, false, false,
                    0,     0,     0,     0,     0,
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.Download,
                "<download soft font>",
                false, false, false,
                    0,     0,     0,     0,     0,
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PrnDisk,
                "<load from printer storage>",
                false, false, false,
                    0,     0,     0,     0,     0,
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Albertus (base value)",
                false, true , true ,
                    0,   266,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "Albertus",
                false, true , true ,
                    0,  4362,     0,     0,     0,
                true ,     0,     1,     "Albertus      Md",
                false,     0,     0,     "                ",
                true ,     0,     4,     "Albertus      Xb",
                false,     0,     0,     "                ",
                symSets_Europe_Not_Hebrew_Greek_Cyrillic));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Antique Olive (base value)",
                false, true , true ,
                    0,    72,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "Antique Olive",
                false, true , true ,
                    0,  4168,     0,     0,     0,
                true ,     0,     0,     "AntiqOlive      ",
                true ,     1,     0,     "AntiqOlive    It",
                true ,     0,     3,     "AntiqOlive    Bd",
                false,     1,     3,     "                ",
                symSets_Europe_Not_Hebrew_Greek_Cyrillic));

            _indxFontArial = fontIndex;

            _fonts.Add(new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "Andale Mono WT J",
                true , true, true,
                  590, 17004,     0,     0,     0,
                true ,     0,     0,     "Andale Mono WT J",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                symSets_Unicode));

            _fonts.Add(new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "Andale Mono WT K",
                true , true, true,
                  590, 17005,     0,     0,     0,
                true ,     0,     0,     "Andale Mono WT K",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                symSets_Unicode));

            _fonts.Add(new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "Andale Mono WT S",
                true , true, true,
                  590, 17007,     0,     0,     0,
                true ,     0,     0,     "Andale Mono WT S",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                symSets_Unicode));

            _fonts.Add(new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "Andale Mono WT T",
                true , true, true,
                  590, 17006,     0,     0,     0,
                true ,     0,     0,     "Andale Mono WT T",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                symSets_Unicode));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Arial (base value)",
                false, true , true ,
                    0,   218,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "Arial",
                 false, true , true ,
                     0, 16602,     0,     0,     0,
                 true ,     0,     0,     "Arial           ",
                 true ,     1,     0,     "Arial         It",
                 true ,     0,     3,     "Arial         Bd",
                 true ,     1,     3,     "Arial       BdIt",
                symSets_Europe_Not_Hebrew_Greek_Cyrillic));

            _fonts.Add(new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "Calibri",
                false, true, true,
                    0, 17329,     0,     0,     0,
                true ,     0,     0,     "Calibri         ",
                true ,     1,     0,     "Calibri       It",
                true ,     0,     3,     "Calibri       Bd",
                true ,     1,     3,     "Calibri     BdIt",
                symSets_Europe_Not_Hebrew_Greek_Cyrillic));

            _fonts.Add(new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "Cambria",
                false, true, true,
                    0, 17328,     0,     0,     0,
                true ,     0,     0,     "Cambria         ",
                true ,     1,     0,     "Cambria       It",
                true ,     0,     3,     "Cambria       Bd",
                true ,     1,     3,     "Cambria     BdIt",
                symSets_Europe_Not_Hebrew_Greek_Cyrillic));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Caslon (base value)",
                false, true , true ,
                    0,     9,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "CG Omega",
                false, true , true ,
                    0,  4113,     0,     0,     0,
                true ,     0,     0,     "CG Omega        ",
                true ,     1,     0,     "CG Omega      It",
                true ,     0,     3,     "CG Omega      Bd",
                true ,     1,     3,     "CG Omega    BdIt",
                symSets_Europe_Not_Hebrew_Greek_Cyrillic));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "CG Times",
                false, true , true ,
                    0,  4101,     0,     0,     0,
                true ,     0,     0,     "CG Times        ",
                true ,     1,     0,     "CG Times      It",
                true ,     0,     3,     "CG Times      Bd",
                true ,     1,     3,     "CG Times    BdIt",
                symSets_Europe));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Clarendon (base value)",
                false, true , true ,
                    0,    44,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "Clarendon Condensed",
                false, true , true ,
                    0,  4140,     0,     0,     0,
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                true ,     4,     3,     "Clarendon   CdBd",
                false,     0,     0,     "                ",
                symSets_Europe_Not_Hebrew_Greek_Cyrillic));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Coronet (base value)",
                false, true , true ,
                    0,    20,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "Coronet",
                false, true , true ,
                    0,  4116,     0,     0,     0,
                false,     0,     0,     "                ",
                true ,     1,     0,     "Coronet         ",
                false,     0,     3,     "                ",
                false,     1,     3,     "                ",
                symSets_Europe_Not_Hebrew_Greek_Cyrillic));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Courier (base value)",
                false, false, true ,
                    0,     3,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _indxFontCourier = fontIndex;

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "Courier",
                false, false, true ,
                    0,  4099,    60,     0,     0,
                true ,     0,     0,     "Courier         ",
                true ,     1,     0,     "Courier       It",
                true ,     0,     3,     "Courier       Bd",
                true ,     1,     3,     "Courier     BdIt",
                symSets_Europe));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "Courier PS",
                false, false, true ,
                    0, 24579,    60,     0,     0,
                true ,     0,     0,     "CourierPS       ",
                true ,     1,     0,     "CourierPS     Ob",
                true ,     0,     3,     "CourierPS     Bd",
                true ,     1,     3,     "CourierPS   BdOb",
                symSets_Europe_Not_Hebrew_Greek_Cyrillic));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Elite (base value)",
                false, false, true ,
                    0,     2,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Garamond (base value)",
                false, true , true ,
                    0,   101,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "Garamond",
                false, true , true ,
                    0,  4197,     0,     0,     0,
                true ,     0,     0,     "Garamond Antiqua",
                true ,     1,     0,     "Garamond    Krsv",
                true ,     0,     3,     "Garamond     Hlb",
                true ,     1,     3,     "Garamond KrsvHlb",
                symSets_Europe_Not_Hebrew_Greek_Cyrillic));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetTypeface,
                "GW-Kai",
                false, true, true,
                    0, 37357,     0,     0,     0,
                // remaining values are unknown for this entry
                true ,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                symSets_Unicode));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Helvetica (base value)",
                false, true , true ,
                    0,     4,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Helvetica",
                false, true , true ,
                    0, 24580,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetFamilyMember,
                "Helvetica",
                false, true , true ,
                    0, 24580,     0,     0,     0,
                true ,     0,     0,     "Helvetica       ",
                true ,     1,     0,     "Helvetica     Ob",
                true ,     0,     3,     "Helvetica     Bd",
                true ,     1,     3,     "Helvetica   BdOb",
                symSets_Europe_Not_Hebrew_Greek_Cyrillic));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetFamilyMember,
                "Helvetica Narrow",
                false, true , true ,
                    0, 24580,     0,     0,     0,
                true ,     4,     0,     "Helvetica     Nr",
                true ,     5,     0,     "Helvetica   NrOb",
                true ,     4,     3,     "Helvetica   NrBd",
                true ,     5,     3,     "Helvetica NrBdOb",
                symSets_Europe_Not_Hebrew_Greek_Cyrillic));

            _fonts.Add(new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "HP David",
                false, true, true,
                    0, 16585,     0,     0,     0,
                true ,     0,     0,     "Dorit           ",
                false,     0,     0,     "                ",
                true ,     0,     3,     "Dorit         Bd",
                false,     0,     0,     "                ",
                symSets_Hebrew));

            _fonts.Add(new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "HP Miryam",
                false, true, true,
                    0, 16584,     0,     0,     0,
                true ,     0,     0,     "Malka           ",
                true ,     1,     0,     "Malka         It",
                true ,     0,     3,     "Malka         Bd",
                false,     0,     0,     "                ",
                symSets_Hebrew));

            _fonts.Add(new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "HP Narkis Tam",
                false, true, true,
                    0, 16587,     0,     0,     0,
                true ,     0,     0,     "Naamit          ",
                false,     0,     0,     "                ",
                true ,     0,     3,     "Naamit        Bd",
                false,     0,     0,     "                ",
                symSets_Hebrew));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "ITC Avant Garde (base value)",
                false, true , true ,
                    0,    31,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "ITC Avant Garde",
                false, true , true ,
                    0, 24607,     0,     0,     0,
                true ,     0,     0,     "ITCAvantGard  Bk",
                true ,     1,     0,     "ITCAvantGardBkOb",
                true ,     0,     2,     "ITCAvantGard  Db",
                true ,     1,     2,     "ITCAvantGardDbOb",
                symSets_Europe_Not_Hebrew_Greek_Cyrillic));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "ITC Bookman (base value)",
                false, true , true ,
                    0,    47,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "ITC Bookman",
                false, true , true ,
                    0, 24623,     0,     0,     0,
                true ,     0,    -3,     "ITCBookman    Lt",
                true ,     1,    -3,     "ITCBookman    It",
                true ,     0,     2,     "ITCBookman    Bd",
                true ,     1,     2,     "ITCBookman  BdIt",
                symSets_Europe_Not_Hebrew_Greek_Cyrillic));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Koufi (base value)",
                false, true , true ,
                    0,   168,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "Koufi",
                false, false, true ,
                    0,  4264,    60,     0,     0,
                true ,     0,     0,     "Koufi           ",
                false,     0,     0,     "                ",
                true ,     0,     3,     "Koufi         Bd",
                false,     0,     3,     "                ",
                symSets_Arabic));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Letter Gothic (base value)",
                false, true , true ,
                    0,     6,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "Letter Gothic",
                false, false, true ,
                    0,  4102,    50,     0,     0,
                true ,     0,     0,     "LetterGothic    ",
                true ,     1,     0,     "LetterGothic  It",
                true ,     0,     3,     "LetterGothic  Bd",
                false,     0,     0,     "                ",
                symSets_Europe));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Line Printer",
                true, false, false,
                   14,     0,     0, 16.67,   8.5,
                // remaining values are dummy values for this entry
                true,      0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetFamilyMember,
                "Line Printer (0N)",
                true , false, false,
                   14,     0,     0, 16.67,   8.5,
                true ,     0,     0,     "Line Printer  0N",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                new UInt16[] { PCLSymbolSets.translateIdToKind1("0N") }));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetFamilyMember,
                "Line Printer (1U)",
                true , false, false,
                   53,     0,     0, 16.67,   8.5,
                true ,     0,     0,     "Line Printer  1U",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                new UInt16[] { PCLSymbolSets.translateIdToKind1("1U") }));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetFamilyMember,
                "Line Printer (2N)",
                true , false, false,
                   78,     0,     0, 16.67,   8.5,
                true ,     0,     0,     "Line Printer  2N",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                new UInt16[] { PCLSymbolSets.translateIdToKind1("2N") }));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetFamilyMember,
                "Line Printer (5N)",
                true , false, false,
                  174,     0,     0, 16.67,   8.5,
                true ,     0,     0,     "Line Printer  5N",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                new UInt16[] { PCLSymbolSets.translateIdToKind1("5N") }));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetFamilyMember,
                "Line Printer (6N)",
                true , false, false,
                  206,     0,     0, 16.67,   8.5,
                true ,     0,     0,     "Line Printer  6N",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                new UInt16[] { PCLSymbolSets.translateIdToKind1("6N") }));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetFamilyMember,
                "Line Printer (8U)",
                true , false, false,
                  277,     0,     0, 16.67,   8.5,
                true ,     0,     0,     "Line Printer  8U",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                new UInt16[] { PCLSymbolSets.translateIdToKind1("8U") }));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetFamilyMember,
                "Line Printer (9N)",
                true , false, false,
                  302,     0,     0, 16.67,   8.5,
                true ,     0,     0,     "Line Printer  9N",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                new UInt16[] { PCLSymbolSets.translateIdToKind1("9N") }));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetFamilyMember,
                "Line Printer (10N)",
                true , false, false,
                  334,     0,     0, 16.67,   8.5,
                true ,     0,     0,     "Line Printer 10N",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                new UInt16[] { PCLSymbolSets.translateIdToKind1("10N") }));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetFamilyMember,
                "Line Printer (10U)",
                true , false, false,
                  341,     0,     0, 16.67,   8.5,
                true ,     0,     0,     "Line Printer 10U",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                new UInt16[] { PCLSymbolSets.translateIdToKind1("10U") }));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetFamilyMember,
                "Line Printer (11U)",
                true, false, false,
                 373,     0,     0, 16.67,   8.5,
                true ,     0,     0,     "Line Printer 11U",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                new UInt16[] { PCLSymbolSets.translateIdToKind1("11U") }));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetFamilyMember,
                "Line Printer (12U)",
                true , false, false,
                  405,     0,     0, 16.67,   8.5,
                true ,     0,     0,     "Line Printer 12U",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                new UInt16[] { PCLSymbolSets.translateIdToKind1("12U") }));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Marigold (base value)",
                false, true , true ,
                    0,   201,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "Marigold",
                false, true , true ,
                    0,  4297,     0,     0,     0,
                true ,     0,     0,     "Marigold        ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                symSets_Europe_Not_Hebrew_Greek_Cyrillic));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetTypeface,
                "MS Gothic",
                false, true, true,
                    0, 28825,     0,     0,     0,
                // remaining values are unknown for this entry
                true ,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                symSets_Unicode));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetTypeface,
                "MS Mincho",
                false, true, true,
                    0, 28752,     0,     0,     0,
                // remaining values are unknown for this entry
                true ,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                symSets_Unicode));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Naskh (base value)",
                false, true , true ,
                    0,    28,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "Naskh",
                false, false,  true,
                    0,  4124,    60,     0,     0,
                true ,     0,     0,     "Naskh           ",
                false,     0,     0,     "                ",
                true ,     0,     3,     "Naskh         Bd",
                false,     0,     3,     "                ",
                symSets_Arabic));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "New Century Schoolbook (base value)",
                false, true , true ,
                    0,   127,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "New Century Schoolbook",
                false, true , true ,
                    0, 24703,     0,     0,     0,
                true ,     0,     0,     "NwCentSchlbk Rmn",
                true ,     1,     0,     "NwCentSchlbk  It",
                true ,     0,     3,     "NwCentSchlbk  Bd",
                true ,     1,     3,     "NwCentSchlbkBdIt",
                symSets_Europe_Not_Hebrew_Greek_Cyrillic));

            _fonts.Add(new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "OCR-A",
                true, false, true,
                    15, 104, 0, 0, 0,
                true,      0,     0,     "OCR-A           ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                new UInt16[] { PCLSymbolSets.translateIdToKind1("0O") }));

            _fonts.Add(new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "OCR-B",
                true, false, true,
                    47, 110, 0, 0, 0,
                true,      0,     0,     "OCR-B           ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                new UInt16[] { PCLSymbolSets.translateIdToKind1("1O") }));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Orator (base value)",
                false, true , true ,
                    0,    10,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Palatino (base value)",
                false, true , true ,
                    0,    15,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "Palatino",
                false, true , true ,
                    0, 24591,     0,     0,     0,
                true ,     0,     0,     "Palatino     Rmn",
                true ,     1,     0,     "Palatino      It",
                true ,     0,     3,     "Palatino      Bd",
                true ,     1,     3,     "Palatino    BdIt",
                symSets_Europe_Not_Hebrew_Greek_Cyrillic));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Presentation (base value)",
                false, true , true ,
                    0,    11,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Prestige (base value)",
                false, true , true ,
                    0,     8,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add(new PCLFont(
                fontIndex++,
                eFontType.PresetFamily,
                "Ryadh (base value)",
                false, true , true ,
                    0,   763,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add(new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "Ryadh",
                false, true, true,
                    0,  4859,     0,     0,     0,
                true ,     0,     0,     "Ryadh           ",
                false,     0,     0,     "                ",
                true ,     0,     3,     "Ryadh         Bd",
                false,     0,     0,     "                ",
                symSets_Arabic));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetTypeface,
                "SimHei",
                false, true, true,
                    0, 37110,     0,     0,     0,
                // remaining values are unknown for this entry
                true ,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                symSets_Unicode));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetTypeface,
                "SimSun",
                false, true, true,
                    0, 37058,     0,     0,     0,
                // remaining values are unknown for this entry
                true ,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                symSets_Unicode));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Symbol (base value)",
                false, true , true ,
                    0,   302,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "Symbol",
                true , true , true ,
                  621, 16686,     0,     0,     0,
                true ,     0,     0,     "Symbol          ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                new UInt16[] { PCLSymbolSets.translateIdToKind1("19M") }));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "Symbol PS",
                true , true , true ,
                  621, 45358,     0,     0,     0,
                true ,     0,     0,     "SymbolPS        ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                new UInt16[] { PCLSymbolSets.translateIdToKind1("19M") }));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Times (base value)",
                false, true , true ,
                    0,   517,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "Times",
                false, true , true ,
                    0, 25093,     0,     0,     0,
                true ,     0,     0,     "Times        Rmn",
                true ,     1,     0,     "Times         It",
                true ,     0,     3,     "Times         Bd",
                true ,     1,     3,     "Times       BdIt",
                symSets_Europe_Not_Hebrew_Greek_Cyrillic));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Times Roman (base value)",
                false, true , true ,
                    0,     5,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "Times New",
                false, true , true ,
                    0, 16901,     0,     0,     0,
                true ,     0,     0,     "TimesNewRmn     ",
                true ,     1,     0,     "TimesNewRmn   It",
                true ,     0,     3,     "TimesNewRmn   Bd",
                true ,     1,     3,     "TimesNewRmn BdIt",
                symSets_Europe_Not_Hebrew_Greek_Cyrillic));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Univers (base value)",
                false, true , true ,
                    0,    52,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Univers",
                false, true, true,
                    0,  4148,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetFamilyMember,
                "Univers",
                false, true , true ,
                    0,  4148,     0,     0,     0,
                true ,     0,     0,     "Univers       Md",
                true ,     1,     0,     "Univers     MdIt",
                true ,     0,     3,     "Univers       Bd",
                true ,     1,     3,     "Univers     BdIt",
                symSets_Europe));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetFamilyMember,
                "Univers Condensed",
                false, true , true ,
                    0,  4148,     0,     0,     0,
                true ,     4,     0,     "Univers     CdMd",
                true ,     5,     0,     "Univers   CdMdIt",
                true ,     4,     3,     "Univers     CdBd",
                true ,     5,     3,     "Univers   CdBdIt",
                symSets_Europe_Not_Hebrew_Greek_Cyrillic));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Wingdings (base value)",
                false, true , true ,
                    0,  2730,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "Wingdings",
                true , true , true ,
                18540, 31402,     0,     0,     0,
                true ,     0,     0,     "Wingdings       ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                new UInt16[] { PCLSymbolSets.translateIdToKind1("579L") }));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Zapf Chancery (base value)",
                false, true , true ,
                    0,    43,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));
        
            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "Zapf Chancery",
                false, true , true ,
                    0, 45099,     0,     0,     0,
                false,     0,     0,     "                ",
                true ,     1,     0,     "ZapfChanceryMdIt",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                symSets_Europe_Not_Hebrew_Greek_Cyrillic));

            _fonts.Add (new PCLFont (
                fontIndex++,
                eFontType.PresetFamily,
                "Zapf Dingbats (base value)",
                false, true , true ,
                    0,    45,     0,     0,     0,
                // remaining values are dummy values for this entry
                true ,     0,     0,     "                ",
                true ,     1,     0,     "                ",
                true ,     0,     3,     "                ",
                true ,     1,     3,     "                ",
                symSets_Dummy));

            _fonts.Add (new PCLFont(
                fontIndex++,
                eFontType.PresetTypeface,
                "Zapf Dingbats",
                true , true , true ,
                  460, 45101,     0,     0,     0,
                true ,     0,     0,     "ZapfDingbats    ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                false,     0,     0,     "                ",
                new UInt16[] { PCLSymbolSets.translateIdToKind1("14L") }));

            _fontsCount = _fonts.Count;

            _fontsCountCustom   = 0;
            _fontsCountDownload = 0;
            _fontsCountPrnDisk  = 0;
            _fontsCountPreset   = 0;

            foreach (PCLFont v in _fonts)
            {
                if ((v.Type == eFontType.PresetTypeface)
                                  ||
                     (v.Type == eFontType.PresetFamilyMember))
                {
                    _fontsCountPreset++;
                }
                else if (v.Type == eFontType.Custom)
                {
                    _fontsCountCustom++;
                }
                else if (v.Type == eFontType.Download)
                {
                    _fontsCountDownload++;
                }
                else if (v.Type == eFontType.PrnDisk)
                {
                    _fontsCountPrnDisk++;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t r a n s l a t e T y p e f a c e                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the HP vendor and base code equivalents of the supplied     //
        // typeface family number.                                            //
        // numeric identifier.                                                //
        //                                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void translateTypeface(UInt16 typeface,
                                             ref UInt16 vendor,
                                             ref UInt16 basecode)
        {
            vendor = (UInt16) (typeface >> 12);
            basecode = (UInt16) (typeface & 0xfff);

            return;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // v a r i a n t E x i s t s                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return a value indicating whether or not the specified variant of  //
        // the font is available.                                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean variantExists(Int32    indxFont,
                                            eVariant variant)
        {
            return _fonts[indxFont].variantAvailable(variant);
        }
    }
}