using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines constants used by print file 'parse' mechanisms.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>
    
    [System.Reflection.ObfuscationAttribute(Feature = "properties renaming")]

    public static class PrnParseConstants
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public enum eContType
        {
            None = 0,
            Abort,
            Reset,
            Unknown,
            Special,
            PCLAlphaNumericID,
            PCLColourLookup,
            PCLConfigurationIO,
            PCLConfigurationIOVal,
            PCLConfigureImageData,
            PCLConfigureRasterData,
            PCLComplex,
            PCLDitherMatrix,
            PCLDitherMatrixPlane,
            PCLDitherMatrixPlaneData,
            PCLDownload,
            PCLDownloadCombo,
            PCLDriverConfiguration,
            PCLEmbeddedData,
            PCLEmbeddedPML,
            PCLEscEncText,
            PCLEscEncTextData,
            PCLExtension,
            PCLFontChar,
            PCLFontHddr,
            PCLLogicalPageData,
            PCLDefineSymbolSet,
            PCLDefineSymbolSetMap,
            PCLMultiByteData,
            PCLPaletteConfiguration,
            PCLUserDefPatternHddr,
            PCLUserDefPatternData,
            PCLViewIlluminant,
            HPGL2,
            HPGL2Binary,
            HPGL2Label,
            HPGL2Long,
            HPGL2LongQuote,
            PJL,
            PostScript,
            PCLXL,
            PCLXLEmbed,
            PCLXLFontChar,
            PCLXLFontHddr,
            Prescribe
        }

        public enum eActPCL
        {
            None,
            AlphaNumericID,
            CheckForPML,
            ColourLookup,
            ConfigurationIO,
            ConfigureImageData,
            ConfigureRasterData,
            DefineSymbolSet,
            DitherMatrix,
            DriverConfiguration,
            EmbeddedData,
            EscEncText,
            FontChar,
            FontHddr,
            LogicalPageData,
            MacroStart,
            MacroStop,
            PaletteConfiguration,
            StyleData,
            TextParsing,
            SwitchToHPGL2,
            SwitchToPJL,
            UserDefinedPattern,
            ViewIlluminant
        }

        public enum eActPCLXL
        {
            None,
            CharSize,
            Measure,
            UnitsPerMeasure
        }
        public enum eOvlAct
        {
            None = 0,
            Adjust,         // PCL    only
            Download,       // PCL    only
            DownloadDelete, // PCL    only
            EndOfFile,      // PCL    only ?
            IdFont,         // PCL    only
            IdPalette,      // PCL    only
            IdPattern,      // PCL    only
            IdSymSet,       // PCL    only
            IdMacro,        // PCL    only
            IgnorePage,
            Illegal,        // PCL XL only ?
            Insert,
            PageBegin,      // PCL XL only  or PCL as well
            PageEnd,        // PCL XL only  or PCL as well
            PageBoundary,   // PCL    only
            PageChange,     // PCL    only
            PageMark,       // PCL    only
            PushGS,         // PCL XL only
            Remove,
            Replace_0x77,   // PCL XL only
            Reset,          // PCL    only 
            Terminate
        }

        public enum eOvlPos
        {
            BeforeFirstPage = 0,    // keep these entries in this logical order
            WithinFirstPage,
            BetweenPages,
            WithinOtherPages,
            AfterPages
        }

        [System.Reflection.ObfuscationAttribute(Exclude = true)]

        public enum eOvlShow
        {
            None = 0,
            Insert,
            Modify,
            Remove,
            Illegal,
            Terminate
        }

        public enum eSeqGrp
        {
            Unknown = 0,
            Colour,
            CursorPositioning,
            FontManagement,
            FontSelection,
            JobControl,
            Macros,
            Misc,
            PageControl,
            PictureFrame,
            PrintModel,
            RasterGraphics,
            RectangularAreaFill,
            SoftFontCreation,
            StatusReadback,
            UserPattern
        }

        public enum ePCLXLBinding
        {
            Unknown = 0,
            BinaryLSFirst,
            BinaryMSFirst,
            ASCII
        }

        public enum eOptCharSets
        {
            ASCII = 0,
            ISO_8859_1,
            Win_ANSI,
            Max     // limit 
        }

        public enum eOptCharSetSubActs
        {
            Mnemonics = 0,
            MnemonicsIncSpace,
            Hex,
            Dots,
            Spaces,
            Substitute,
            Max     // limit
        }

        public enum eOptOffsetFormats
        {
            Hexadecimal = 0,
            Decimal,
            Max     // limit
        }

        public enum eOptStatsLevel
        {
            ReferencedOnly = 0,
            All,
            Max     // limit
        }

        public enum ePMLSeqType
        {
            Hddr = 0,
            RequestAction,
            RequestTypeLen,
            RequestData,
            ReplyAction,
            ReplyOutcome,
            ReplyData
        }

        public enum eOffsetPosition
        {
            Unknown = -1,
            StartOfFile = -2,
            EndOfFile = -3,
            CrntPosition = -4
        }

        public enum eStdClrs
        {
            AliceBlue = 0,
            AntiqueWhite,
            Aqua,
            Aquamarine,
            Azure,
            Beige,
            Bisque,
            Black,
            BlanchedAlmond,
            Blue,
            BlueViolet,
            Brown,
            BurlyWood,
            CadetBlue,
            Chartreuse,
            Chocolate,
            Coral,
            CornflowerBlue,
            Cornsilk,
            Crimson,
            Cyan,
            DarkBlue,
            DarkCyan,
            DarkGoldenrod,
            DarkGray,
            DarkGreen,
            DarkKhaki,
            DarkMagenta,
            DarkOliveGreen,
            DarkOrange,
            DarkOrchid,
            DarkRed,
            DarkSalmon,
            DarkSeaGreen,
            DarkSlateBlue,
            DarkSlateGray,
            DarkTurquoise,
            DarkViolet,
            DeepPink,
            DeepSkyBlue,
            DimGray,
            DodgerBlue,
            Firebrick,
            FloralWhite,
            ForestGreen,
            Fuchsia,
            Gainsboro,
            GhostWhite,
            Gold,
            Goldenrod,
            Gray,
            Green,
            GreenYellow,
            Honeydew,
            HotPink,
            IndianRed,
            Indigo,
            Ivory,
            Khaki,
            Lavender,
            LavenderBlush,
            LawnGreen,
            LemonChiffon,
            LightBlue,
            LightCoral,
            LightCyan,
            LightGoldenrodYellow,
            LightGray,
            LightGreen,
            LightPink,
            LightSalmon,
            LightSeaGreen,
            LightSkyBlue,
            LightSlateGray,
            LightSteelBlue,
            LightYellow,
            Lime,
            LimeGreen,
            Linen,
            Magenta,
            Maroon,
            MediumAquamarine,
            MediumBlue,
            MediumOrchid,
            MediumPurple,
            MediumSeaGreen,
            MediumSlateBlue,
            MediumSpringGreen,
            MediumTurquoise,
            MediumVioletRed,
            MidnightBlue,
            MintCream,
            MistyRose,
            Moccasin,
            NavajoWhite,
            Navy,
            OldLace,
            Olive,
            OliveDrab,
            Orange,
            OrangeRed,
            Orchid,
            PaleGoldenrod,
            PaleGreen,
            PaleTurquoise,
            PaleVioletRed,
            PapayaWhip,
            PeachPuff,
            Peru,
            Pink,
            Plum,
            PowderBlue,
            Purple,
            Red,
            RosyBrown,
            RoyalBlue,
            SaddleBrown,
            Salmon,
            SandyBrown,
            SeaGreen,
            SeaShell,
            Sienna,
            Silver,
            SkyBlue,
            SlateBlue,
            SlateGray,
            Snow,
            SpringGreen,
            SteelBlue,
            Tan,
            Teal,
            Thistle,
            Tomato,
            Transparent,
            Turquoise,
            Violet,
            Wheat,
            White,
            WhiteSmoke,
            Yellow,
            YellowGreen
    	}

        public const Int32 bufSize = 2048;        // multiple of 16         
        public const Int32 viewBytesPerLine = 16; // divisor of 1024

        public const Byte asciiAngleLeft = 0x3c;
        public const Byte asciiAngleRight = 0x3e;
        public const Byte asciiAlphaLCMax = 0x7a;
        public const Byte asciiAlphaLCMin = 0x61;
        public const Byte asciiAlphaUCMax = 0x5a;
        public const Byte asciiAlphaUCMin = 0x41;
        public const Byte asciiApostrophe = 0x27;
        public const Byte asciiAtSign = 0x40;
        public const Byte asciiBEL = 0x07;
        public const Byte asciiCR = 0x0d;
        public const Byte asciiDEL = 0x7f;
        public const Byte asciiDigit0 = 0x30;
        public const Byte asciiGraphicMin = 0x21;
        public const Byte asciiDigit1 = 0x31;
        public const Byte asciiDigit9 = 0x39;
        public const Byte asciiEquals = 0x3d;
        public const Byte asciiEsc = 0x1b;
        public const Byte asciiETX = 0x03;
        public const Byte asciiExclamationMark = 0x21;
        public const Byte asciiFF = 0x0c;
        public const Byte asciiHT = 0x09;
        public const Byte asciiLF = 0x0a;
        public const Byte asciiMax8bit = 0xff;
        public const Byte asciiMinus = 0x2d;
        public const Byte asciiNUL = 0x00;
        public const Byte asciiPeriod = 0x2e;
        public const Byte asciiPipe = 0x7c;
        public const Byte asciiPlus = 0x2b;
        public const Byte asciiQuote = 0x22;
        public const Byte asciiSemiColon = 0x3b;
        public const Byte asciiSpace = 0x20;
        public const Byte asciiSUB = 0x1f;
        public const Byte asciiSubDefault = 0xbf;

        public const Byte pclSimpleICharLow = 0x30;
        public const Byte pclSimpleICharHigh = 0x7e;

        public const Byte pclComplexICharLow = 0x21;
        public const Byte pclComplexICharHigh = 0x2f;

        public const Byte pclComplexGCharLow = 0x60;
        public const Byte pclComplexGCharHigh = 0x7e;

        public const Byte pclComplexPCharLow = 0x60;
        public const Byte pclComplexPCharHigh = 0x7e;

        public const Byte pclComplexTCharLow = 0x40;
        public const Byte pclComplexTCharHigh = 0x5e;

        public const Byte pclxlAttrUbyte      = 0xf8;
        public const Byte pclxlAttrUint16     = 0xf9;
        public const Byte pclxlEmbedData      = 0xfa;
        public const Byte pclxlEmbedDataByte  = 0xfb;
        public const Byte pclxlDataTypeLow    = 0xc0;
        public const Byte pclxlDataTypeHigh   = 0xef;
        public const Byte pclxlDataTypeUbyte  = 0xc0;
        public const Byte pclxlDataTypeUint16 = 0xc1;
        public const Byte pclxlOperatorLow    = 0x41;
        public const Byte pclxlOperatorHigh   = 0xbf;

        public const Byte prescribeSCRCDefault   = 0x52;  // R //
        public const Byte prescribeSCRCDelimiter = 0x21;  // ! //

        public const String cRptA_colName_RowType = "RowType";  // not displayed
        public const String cRptA_colName_Action  = "Action";   // MakeOverlay only
        public const String cRptA_colName_Offset  = "Offset";
        public const String cRptA_colName_Type    = "Type";
        public const String cRptA_colName_Seq     = "Sequence";
        public const String cRptA_colName_Desc    = "Description";

        public const String cRptC_colName_Offset  = "Offset";
        public const String cRptC_colName_Hex     = "Hexadecimal";
        public const String cRptC_colName_Text    = "Text";

        public const String cRptS_colName_Seq     = "Sequence";
        public const String cRptS_colName_Desc    = "Description";
        public const String cRptS_colName_CtP     = "Parent";
        public const String cRptS_colName_CtE     = "Embedded";
        public const String cRptS_colName_CtT     = "Total";

        public const Int32 cRptA_colMax_RowType = -1;   // not displayed
        public const Int32 cRptA_colMax_Action  = 10;   // MakeOverlay only
        public const Int32 cRptA_colMax_Offset  = 13;
        public const Int32 cRptA_colMax_Type    = 21;
        public const Int32 cRptA_colMax_Seq     = 16;
        public const Int32 cRptA_colMax_Desc    = 52;

        public const Int32 cRptC_colMax_Offset  = 13;
        public const Int32 cRptC_colMax_Hex     = 48;
        public const Int32 cRptC_colMax_Text    = 16;

        public const Int32 cRptS_colMax_Seq     = 19;
        public const Int32 cRptS_colMax_Desc    = 52;
        public const Int32 cRptS_colMax_CtP     = 8;
        public const Int32 cRptS_colMax_CtE     = 8;
        public const Int32 cRptS_colMax_CtT     = 8;

        public const Int32 cColSeparatorLen = 2;

        public const Int32 pclDotResDefault = 300;

        public static Byte[] cHexBytes = {0x30, 0x31, 0x32, 0x33,  // 0123
                                          0x34, 0x35, 0x36, 0x37,  // 4567
                                          0x38, 0x39, 0x61, 0x62,  // 89ab
                                          0x63, 0x64, 0x65, 0x66}; // cdef;

        public static Char[] cHexChars = {'0', '1', '2', '3',
                                          '4', '5', '6', '7',
                                          '8', '9', 'a', 'b',
                                          'c', 'd', 'e', 'f'}; 
    }
}