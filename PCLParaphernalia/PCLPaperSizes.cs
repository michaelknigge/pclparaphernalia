using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines a set of PCL Paper Size objects.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class PCLPaperSizes
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        // Note that the length of the index array must be the same as that   //
        // of the definition array; the entries must be in the same order.    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public const UInt16 _paperSizeUPI = 1200;

        private const Byte _unknownID      = 0xff;
        private const Byte _unknownEnum    = 0xff;

        public enum eIndex
        {
            Custom,
            Default,
            ANSI_A_Letter,
            ANSI_B_Ledger_Tabloid,
            ANSI_C,
            ANSI_D,
            ANSI_E,
            Card_10x15cm,
            Card_3x5,
            Card_4x6,
            Card_5x7,
            Card_5x8,
            Card_A6,
            Card_Custom,
            CN_8K,
            CN_8K_260x368,
            CN_8K_270x390,
            CN_16K,
            CN_16K_184x260,
            CN_16K_195x270,
            EDP_Europe,
            EDP_US,
            Env_Baronial_5_5,
            Env_Cat1,
            Env_Com9,
            Env_Com10,
            Env_Com11,
            Env_Custom,
            Env_Intl_B5,
            Env_Intl_C4,
            Env_Intl_C5,
            Env_Intl_C6,
            Env_Intl_DL,
            Env_JP_Long_3,
            Env_JP_Long_4,
            Env_Letter,
            Env_Monarch,
            Env_Postfix,
            Env_US_C5,
            Executive,
            Folio,
            Foolscap,
            ISO_2A0,
            ISO_4A0,
            ISO_A0,
            ISO_A1,
            ISO_A2,
            ISO_A3,
            ISO_A4,
            ISO_A5,
            ISO_A6,
            ISO_A7,
            ISO_A8,
            ISO_A9,
            ISO_A10,
            ISO_B0,
            ISO_B1,
            ISO_B2,
            ISO_B3,
            ISO_B4,
            ISO_B5,
            ISO_B6,
            ISO_B7,
            ISO_B8,
            ISO_B9,
            ISO_B10,
            JIS_B0,
            JIS_B1,
            JIS_B2,
            JIS_B3,
            JIS_B4,
            JIS_B5,
            JIS_B6,
            JIS_B7,
            JIS_B8,
            JIS_B9,
            JIS_B10,
            JIS_Executive,
            JP_Postcard,
            JP_PostcardDouble,
            Ledger_Alt,
            Legal,
            Oficio_8p5x13,
            Oficio_216x340,
            Oficio_216x343,
            Oficio_216x347,
            RA3,
            RA4,
            SRA3,
            SRA4,
            Statement,
            USGovt_Legal,
            USGovt_Letter
        }

        private static Int32 _eIndexCt = Enum.GetNames(typeof(eIndex)).Length;

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static PCLPaperSize[] _paperSizes;

        private static Int32 _paperSizeCount;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L P a p e r S i z e s                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        static PCLPaperSizes()
        {
            populateTable();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c u s t o m D a t a C o p y                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Copy the various size data fields to the 'custom' entry.           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void customDataCopy (Int32 index)
        {
            _paperSizes[index].customDataCopy (
                _paperSizes[(Int32)eIndex.Custom]);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d i s p l a y P a p e r S i z e L i s t                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Display list of paper sizes in nominated data grid.                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 displayPaperSizeList(DataGrid grid)
        {
            Int32 count = 0;

            foreach (PCLPaperSize v in _paperSizes)
            {
                    count++;
                    grid.Items.Add(v);
            }

            return count;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C o u n t                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return count of Paper Size definitions.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 getCount()
        {
            return _paperSizeCount;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t D e s c                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return description associated with specified PaperSize index.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getDesc(Int32 index)
        {
            return _paperSizes[index].getDesc();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I d P C L                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return PCL ID associated with specified PaperSize index.           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Byte getIdPCL(Int32 index)
        {
            return _paperSizes[index].getIdPCL();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I d P C L X L                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return PCL XL ID associated with specified PaperSize index.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Byte getIdPCLXL(Int32 index)
        {
            return _paperSizes[index].getIdPCLXL();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t L o g i c a l O f f s e t                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the logical page offset of the paper associated with a      //
        // specified PaperSize index, for a given aspect.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt16 getLogicalOffset(Int32  index,
                                              UInt16 sessionUPI,
                                              PCLOrientations.eAspect aspect)
        {
            return _paperSizes[index].getLogicalOffset(sessionUPI, aspect);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t L o g P a g e L e n g t h                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the length of the (PLC5) logical page associated with a     //
        // specified PaperSize index, for a given aspect.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt16 getLogPageLength (Int32 index,
                                               UInt16 sessionUPI,
                                               PCLOrientations.eAspect aspect)
        {
            return _paperSizes[index].getLogPageLength(sessionUPI, aspect);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t L o g P a g e W i d t h                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the width of the (PLC5) logical page associated with a      //
        // specified PaperSize index, for a given aspect.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt16 getLogPageWidth (Int32 index,
                                              UInt16 sessionUPI,
                                              PCLOrientations.eAspect aspect)
        {
            return _paperSizes[index].getLogPageWidth(sessionUPI, aspect);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t M a r g i n s L o g i c a l L a n d                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the size of the PCL Landscape logical margins associated    //
        // with specified PaperSize index.                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt16 getMarginsLogicalLand(Int32  index,
                                                   UInt16 sessionUPI)
        {
            return _paperSizes[index].getMarginsLogicalLand(sessionUPI);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t M a r g i n s L o g i c a l P o r t                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the size of the PCL Portrait logical margins associated     //
        // with specified PaperSize index.                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt16 getMarginsLogicalPort(Int32  index,
                                                   UInt16 sessionUPI)
        {
            return _paperSizes[index].getMarginsLogicalPort(sessionUPI);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t M a r g i n s U n p r i n t a b l e                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the size of the unprintable margins associated with         //
        // specified PaperSize index; these are the same for both standard    //
        // orientations.                                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt16 getMarginsUnprintable(Int32  index,
                                                   UInt16 sessionUPI)
        {
            return _paperSizes[index].getMarginsUnprintable(sessionUPI);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t N a m e                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return name associated with specified PaperSize index.             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getName(Int32 index)
        {
            return _paperSizes[index].getName();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t N a m e A n d D e s c                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return name and description associated with specified PaperSize    //
        // index.                                                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getNameAndDesc(Int32 index)
        {
            String name = _paperSizes[index].getName();
            String desc = _paperSizes[index].getDesc();

            if (desc == "")
                return name;
            else
                return (name + ": " + desc);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t N a m e P C L X L                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the PCL XL string name associated with specified PaperSize  //
        // index.                                                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getNamePCLXL(Int32 index)
        {
            return _paperSizes[index].getNamePCLXL();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t P a p e r L e n g t h                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the length of the paper associated with a specified         //
        // PaperSize index, for a given aspect.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt16 getPaperLength(Int32  index,
                                            UInt16 sessionUPI,
                                            PCLOrientations.eAspect aspect)
        {
            return _paperSizes[index].getPaperLength(sessionUPI, aspect);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t P a p e r W i d t h                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the width of the paper associated with a specified          //
        // PaperSize index, for a given aspect.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt16 getPaperWidth(Int32  index,
                                           UInt16 sessionUPI,
                                           PCLOrientations.eAspect aspect)
        {
            return _paperSizes[index].getPaperWidth(sessionUPI, aspect);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t S i z e L o n g E d g e                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the size of the long edge of the paper associated with      //
        // specified PaperSize index.                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt16 getSizeLongEdge(Int32  index,
                                             UInt16 sessionUPI)
        {
            return _paperSizes[index].getSizeLongEdge(sessionUPI);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t S i z e S h o r t E d g e                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the size of the short edge of the paper associated with     //
        // specified PaperSize index.                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt16 getSizeShortEdge(Int32  index,
                                              UInt16 sessionUPI)
        {
            return _paperSizes[index].getSizeShortEdge(sessionUPI);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i s C u s t o m S i z e                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return value indicating whether or not the paper size is Custom.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean isCustomSize(Int32 index)
        {
            return (_paperSizes[index].FlagCustomSize);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // I s E n u m U n k n o w n P C L X L                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // is the PCL XL enumeration the 'unknown' value.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean IsEnumUnknownPCLXL(Int32 index)
        {
            if (_paperSizes[index].getIdPCLXL() == _unknownEnum)
                return true;
            else
                return false;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // I s I d U n k n o w n P C L                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // is the PCL identifier the 'unknown' value.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean IsIdUnknownPCL(Int32 index)
        {
            if (_paperSizes[index].getIdPCL() == _unknownID)
                return true;
            else
                return false;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i s R a r e S i z e                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return value indicating whether or not the paper size is rare (or  //
        // obsolete).                                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean isRareSize(Int32 index)
        {
            return (_paperSizes[index].IsRareSize);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p o p u l a t e T a b l e                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Populate the table of PCL paper sizes.                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void populateTable()
        {
            Boolean sizeIsMetric   = true;
            Boolean sizeIsImperial = false;

            Boolean sizeIsRare     = true;
            Boolean sizeIsCommon   = false;

            _paperSizes = new PCLPaperSize []
            {
                new PCLPaperSize(eIndex.Custom, // Custom - sizes can be overridden
                                 "<Custom>",    // name
                                 "",            // description
                                 101,           // PCL identifier
                                 _unknownEnum,  // PCLXL paper size enumeration,
                                 "",            // PCLXL paper size name
                                 sizeIsMetric,  // is metric or imperial size?  
                                 sizeIsCommon,  // is common or rare size?  
                                 _paperSizeUPI, // sizes are in these units
                                 9920,          // size short-edge
                                 14030,         // size long-edge
                                 284,           // logical margins portrait
                                 236,           // logical margins landscape
                                 200),          // unprintable margins

                new PCLPaperSize(eIndex.Default,
                                 "<Default>",
                                 "",
                                 0xff,
                                 (byte)PCLXLAttrEnums.eVal.eDefaultPapersize,
                                 "",
                                 sizeIsImperial,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 10200,     // size is variable
                                 13200,     // size is variable
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.ANSI_A_Letter,
                                 "Letter",
                                 "(ANSI A): 8.5\" x 11\"",
                                 2,
                                 (byte)PCLXLAttrEnums.eVal.eLetterPaper,
                                 "LETTER",
                                 sizeIsImperial,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 10200,     // 8.5"
                                 13200,     // 11"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.ANSI_B_Ledger_Tabloid,
                                 "Ledger",
                                 "(ANSI B, Tabloid): 11\" x 17\"",
                                 6,
                                 (byte)PCLXLAttrEnums.eVal.eLedgerPaper,
                                 "LEDGER",
                                 sizeIsImperial,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 13200,     // 11"
                                 20400,     // 17"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.ANSI_C,
                                 "ANSI C",
                                 "17\" x 22\"",
                                 12,
                                 _unknownEnum,
                                 "",
                                 sizeIsImperial,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 20400,     // 17"
                                 26400,     // 22"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.ANSI_D,
                                 "ANSI D",
                                 "22\" x 34\"",
                                 13,
                                 _unknownEnum,
                                 "",
                                 sizeIsImperial,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 26400,     // 22"
                                 40800,     // 34"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.ANSI_E,
                                 "ANSI E",
                                 "34\" x 44\"",
                                 14,
                                 _unknownEnum,
                                 "",
                                 sizeIsImperial,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 40800,     // 34"
                                 52800,     // 44"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.Card_10x15cm,
                                 "Card: 10 x 15 cm",
                                 "100 mm x 150 mm",
                                 _unknownID,
                                 _unknownEnum,
                                 "10x15 cm",
                                 sizeIsMetric,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 4724,      // 100 mm
                                 7086,      // 150 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.Card_3x5,
                                 "Card: 3\" x 5\"",
                                 "",
                                 78,
                                 _unknownEnum,
                                 "",
                                 sizeIsImperial,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 3600,      // 3"
                                 6000,      // 5"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.Card_4x6,
                                 "Card: 4\" x 6\"",
                                 "",
                                 74,
                                 _unknownEnum,
                                 "",
                                 sizeIsImperial,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 4800,      // 4"
                                 7200,      // 6"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.Card_5x7,
                                 "Card: 5\" x 7\"",
                                 "",
                                 _unknownID,
                                 _unknownEnum,
                                 "",
                                 sizeIsImperial,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 6000,      // 5"
                                 8400,      // 7"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.Card_5x8,
                                 "Card: 5\" x 8\"",
                                 "",
                                 75,
                                 _unknownEnum,
                                 "5x8",
                                 sizeIsImperial,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 6000,      // 5"
                                 9600,      // 8"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.Card_A6,
                                 "Card A6",
                                 "105 mm x 148 mm",
                                 73,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 4960,      // 105 mm 
                                 6992,      // 148 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.Card_Custom,
                                 "Card Custom",
                                 "variable size",
                                 108,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 4800,      // various 
                                 6000,      // various
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.CN_8K,
                                 "8K",
                                 "10.75\" x 15.50\"",
                                 19,
                                 _unknownEnum,
                                 "ROC8K",
                                 sizeIsImperial,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 12900,     // 10.75"
                                 18600,     // 15.50"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.CN_8K_260x368,
                                 "8K_260x368",
                                 "260 mm x 368 mm",
                                 _unknownID,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 12283,     // 260 mm
                                 17385,     // 368 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.CN_8K_270x390,
                                 "8K_270x390",
                                 "270 mm x 390 mm",
                                 _unknownID,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 12755,     // 270 mm
                                 18425,     // 390 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.CN_16K,
                                 "16K",
                                 "7.75\" x 10.75\"",
                                 17,
                                 _unknownEnum,
                                 "ROC16K",
                                 sizeIsImperial,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 9300,      // 7.75"
                                 12900,     // 10.75"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.CN_16K_184x260,
                                 "16K_184x260",
                                 "184 mm x 260 mm",
                                 _unknownID,
                                 _unknownEnum,
                                 "16K 184X260MM",
                                 sizeIsMetric,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 8692,      // 184 mm
                                 12283,     // 260 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.CN_16K_195x270,
                                 "16K_195x270",
                                 "195 mm x 270 mm",
                                 _unknownID,
                                 _unknownEnum,
                                 "16K 195X270MM",
                                 sizeIsMetric,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 9212,      // 195 mm
                                 12755,     // 270 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.EDP_Europe,
                                 "EDP Europe",
                                 "12\" x 14\"",
                                 5,
                                 _unknownEnum,
                                 "",
                                 sizeIsImperial,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 14400,     // 12"
                                 16800,     // 14"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.EDP_US,
                                 "EDP US",
                                 "11\" x 14\"",
                                 4,
                                 _unknownEnum,
                                 "",
                                 sizeIsImperial,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 13200,     // 11"
                                 16800,     // 14"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.Env_Baronial_5_5,
                                 "Envelope Baronial 5.5",
                                 "4.375\" x 5.75\"",
                                 109,
                                 _unknownEnum,
                                 "",
                                 sizeIsImperial,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 5250,      // 4.375"
                                 6900,      // 5.75"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.Env_Cat1,
                                 "Envelope Catalog 1",
                                 "6\" x 9\"",
                                 82,
                                 _unknownEnum,
                                 "",
                                 sizeIsImperial,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 7200,      // 6"
                                 10800,     // 9"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.Env_Com9,
                                 "Envelope Commercial 9",
                                 "3.875\" x 8.875\"",
                                 102,
                                 _unknownEnum,
                                 "COM9",
                                 sizeIsImperial,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 4650,      // 3.875"
                                 10650,     // 8.875"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.Env_Com10,
                                 "Envelope Commercial 10",
                                 "4.125\" x 9.5\"",
                                 81,
                                 (byte)PCLXLAttrEnums.eVal.eCOM10Envelope,
                                 "COM10",
                                 sizeIsImperial,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 4950,      // 4.125"
                                 11400,     // 9.5"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.Env_Com11,
                                 "Envelope Commercial 11",
                                 "4.5\" x 10.375\"",
                                 103,
                                 _unknownEnum,
                                 "",
                                 sizeIsImperial,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 5400,      // 4.5"
                                 12450,     // 10.375"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.Env_Custom,
                                 "Envelope Custom",
                                 "variable size",
                                 107,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 4800,      // various 
                                 6000,      // various
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.Env_Intl_B5,
                                 "Envelope B5",
                                 "176 mm x 250 mm",
                                 100,
                                 (byte)PCLXLAttrEnums.eVal.eB5Envelope,
                                 "B5 ENV",
                                 sizeIsMetric,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 8314,      // 176 mm 
                                 11811,     // 250 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.Env_Intl_C4,
                                 "Envelope C4",
                                 "229 mm x 324 mm",
                                 93,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 10818,     // 229 mm 
                                 15307,     // 324 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.Env_Intl_C5,
                                 "Envelope C5",
                                 "162 mm x 229 mm",
                                 91,
                                 (byte)PCLXLAttrEnums.eVal.eC5Envelope,
                                 "C5",
                                 sizeIsMetric,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 7653,      // 162 mm 
                                 10818,     // 229 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.Env_Intl_C6,
                                 "Envelope C6",
                                 "114 mm x 162 mm",
                                 92,
                                 _unknownEnum,
                                 "C6",
                                 sizeIsMetric,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 5385,      // 114 mm 
                                 7653,      // 162 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.Env_Intl_DL,
                                 "Envelope DL",
                                 "110 mm x 220 mm",
                                 90,
                                 (byte)PCLXLAttrEnums.eVal.eDLEnvelope,
                                 "DL",
                                 sizeIsMetric,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 5196,      // 110 mm 
                                 10393,     // 220 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.Env_JP_Long_3,
                                 "Envelope Japanese Long 3",
                                 "120 mm x 235 mm",
                                 110,
                                 _unknownEnum,
                                 "JCHOU3",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 5669,      // 120 mm 
                                 11102,     // 235 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.Env_JP_Long_4,
                                 "Envelope Japanese Long 4",
                                 "90 mm x 205 mm",
                                 111,
                                 _unknownEnum,
                                 "JCHOU4",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 4251,      // 90 mm 
                                 9685,      // 205 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.Env_Letter,
                                 "Envelope Letter",
                                 "8.5\" x 11\"",
                                 104,
                                 _unknownEnum,
                                 "",
                                 sizeIsImperial,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 10200,     // 8.5"
                                 13200,     // 11"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.Env_Monarch,
                                 "Envelope Monarch",
                                 "3.875\" x 7.5\"",
                                 80,
                                 (byte)PCLXLAttrEnums.eVal.eMonarchEnvelope,
                                 "MONARCH",
                                 sizeIsImperial,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 4650,      // 3.875"
                                 9000,      // 7.5"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.Env_Postfix,
                                 "Envelope Postfix",
                                 "114 mm x 229 mm",
                                 106,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 5385,      // 114 mm 
                                 10818,     // 229 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.Env_US_C5,
                                 "Envelope US C5",
                                 "6.5\" x 9.5\"",
                                 105,
                                 _unknownEnum,
                                 "",
                                 sizeIsImperial,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 7800,      // 6.5"
                                 11400,     // 9.5"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.Executive,
                                 "Executive",
                                 "7.25\" x 10.5\"",
                                 1,
                                 (byte)PCLXLAttrEnums.eVal.eExecPaper,
                                 "EXEC",
                                 sizeIsImperial,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 8700,      // 7.25"
                                 12600,     // 10.5"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.Folio,
                                 "Folio",
                                 "8.3\" x 13\"",
                                 9,
                                 _unknownEnum,
                                 "",
                                 sizeIsImperial,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 9960,      // 8.3"
                                 15600,     // 13"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.Foolscap,
                                 "Foolscap",
                                 "8.5\" x 13\"",
                                 10,
                                 _unknownEnum,
                                 "8.5X13",
                                 sizeIsImperial,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 10200,     // 8.5"
                                 15600,     // 13"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.ISO_2A0,
                                 "2A0",
                                 "1189 mm x 1682 mm",
                                 31,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 56173,     // 1189 mm 
                                 79464,     // 1682 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.ISO_4A0,
                                 "4A0",
                                 "1682 mm x 2378 mm",
                                 32,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 79464,     // 1682 mm 
                                 112346,    // 2378 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.ISO_A0,
                                 "A0",
                                 "841 mm x 1189 mm",
                                 30,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 39732,     // 841 mm 
                                 56173,     // 1189 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.ISO_A1,
                                 "A1",
                                 "594 mm x 841 mm",
                                 29,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 28062,     // 594 mm 
                                 39732,     // 841 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.ISO_A2,
                                 "A2",
                                 "420 mm x 594 mm",
                                 28,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 19842,     // 420 mm 
                                 28062,     // 594 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.ISO_A3,
                                 "A3",
                                 "297 mm x 420 mm",
                                 27,
                                 (byte)PCLXLAttrEnums.eVal.eA3Paper,
                                 "A3",
                                 sizeIsMetric,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 14031,     // 297 mm 
                                 19842,     // 420 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.ISO_A4,
                                 "A4",
                                 "210 mm x 297 mm",
                                 26,
                                 (byte)PCLXLAttrEnums.eVal.eA4Paper,
                                 "A4",
                                 sizeIsMetric,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 9921,      // 210 mm
                                 14031,     // 297 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.ISO_A5,
                                 "A5",
                                 "148 mm x 210 mm",
                                 25,
                                 (byte)PCLXLAttrEnums.eVal.eA5Paper,
                                 "A5",
                                 sizeIsMetric,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 6992,      // 148 mm
                                 9921,      // 210 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.ISO_A6,
                                 "A6",
                                 "105 mm x 148 mm",
                                 24,
                                 (byte)PCLXLAttrEnums.eVal.eA6Paper,
                                 "A6",
                                 sizeIsMetric,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 4960,      // 105 mm
                                 6992,      // 148 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.ISO_A7,
                                 "A7",
                                 "74 mm x 105 mm",
                                 23,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 3496,      // 74 mm
                                 4960,      // 105 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.ISO_A8,
                                 "A8",
                                 "52 mm x 74 mm",
                                 22,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 2456,      // 52 mm
                                 3496,      // 74 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.ISO_A9,
                                 "A9",
                                 "37 mm x 52 mm",
                                 21,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 1748,      // 37 mm
                                 2456,      // 52 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.ISO_A10,
                                 "A10",
                                 "26 mm x 37 mm",
                                 20,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 1228,      // 26 mm
                                 1748,      // 37 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.ISO_B0,
                                 "ISO B0",
                                 "1000 mm x 1414 mm",
                                 70,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 47244,     // 1000 mm
                                 66803,     // 1414 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.ISO_B1,
                                 "ISO B1",
                                 "707 mm x 1000 mm",
                                 69,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 33401,     // 707 mm
                                 47244,     // 1000 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.ISO_B2,
                                 "ISO B2",
                                 "500 mm x 707 mm",
                                 68,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 23622,     // 500 mm
                                 33401,     // 707 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.ISO_B3,
                                 "ISO B3",
                                 "353 mm x 500 mm",
                                 67,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 16677,     // 353 mm
                                 23622,     // 500 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.ISO_B4,
                                 "ISO B4",
                                 "250 mm x 353 mm",
                                 66,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 11811,     // 250 mm
                                 16677,     // 353 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.ISO_B5,
                                 "ISO B5",
                                 "176 mm x 250 mm",
                                 65,
                                 (byte)PCLXLAttrEnums.eVal.eB5Paper,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 8314,      // 176 mm
                                 11811,     // 250 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.ISO_B6,
                                 "ISO B6",
                                 "125 mm x 176 mm",
                                 64,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 5905,      // 125 mm
                                 8314,      // 176 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.ISO_B7,
                                 "ISO B7",
                                 "88 mm x 125 mm",
                                 63,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 4157,      // 88 mm
                                 5905,      // 125 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.ISO_B8,
                                 "ISO B8",
                                 "62 mm x 88 mm",
                                 62,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 2929,      // 62 mm
                                 4157,      // 88 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.ISO_B9,
                                 "ISO B9",
                                 "44 mm x 62 mm",
                                 61,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 2078,      // 44 mm
                                 2929,      // 62 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.ISO_B10,
                                 "ISO B10",
                                 "31 mm x 44 mm",
                                 60,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 1464,      // 31 mm
                                 2078,      // 44 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.JIS_B0,
                                 "JIS B0",
                                 "1030 mm x 1456 mm",
                                 50,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 48661,     // 1030 mm
                                 68787,     // 1456 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.JIS_B1,
                                 "JIS B1",
                                 "728 mm x 1030 mm",
                                 49,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 34393,     // 728 mm
                                 48661,     // 1030 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.JIS_B2,
                                 "JIS B2",
                                 "515 mm x 728 mm",
                                 48,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 24330,     // 515 mm
                                 34393,     // 728 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.JIS_B3,
                                 "JIS B3",
                                 "364 mm x 515 mm",
                                 47,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 17196,     // 364 mm
                                 24330,     // 515 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.JIS_B4,
                                 "JIS B4",
                                 "257 mm x 364 mm",
                                 46,
                                 (byte)PCLXLAttrEnums.eVal.eJB4Paper,
                                 "JIS_B4",
                                 sizeIsMetric,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 12141,     // 257 mm
                                 17196,     // 364 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.JIS_B5,
                                 "JIS B5",
                                 "182 mm x 257 mm",
                                 45,
                                (byte)PCLXLAttrEnums.eVal.eJB5Paper,
                                 "JIS B5",
                                 sizeIsMetric,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 8598,      // 182 mm
                                 12141,     // 257 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.JIS_B6,
                                 "JIS B6",
                                 "128 mm x 182 mm",
                                 44,
                                 (byte)PCLXLAttrEnums.eVal.eJB6Paper,
                                 "JIS B6",
                                 sizeIsMetric,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 6047,      // 128 mm
                                 8598,      // 182 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.JIS_B7,
                                 "JIS B7",
                                 "91 mm x 128 mm",
                                 43,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 4299,      // 91 mm
                                 6047,      // 128 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.JIS_B8,
                                 "JIS B8",
                                 "64 mm x 91 mm",
                                 42,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 3023,      // 64 mm
                                 4299,      // 91 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.JIS_B9,
                                 "JIS B9",
                                 "45 mm x 64 mm",
                                 41,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 2125,      // 45 mm
                                 3023,      // 64 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.JIS_B10,
                                 "JIS B10",
                                 "32 mm x 45 mm",
                                 40,
                                 _unknownEnum,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 1511,      // 32 mm
                                 2125,      // 45 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.JIS_Executive,
                                 "JIS Executive",
                                 "216 mm x 330 mm",
                                 18,
                                 (byte)PCLXLAttrEnums.eVal.eJISExecPaper,
                                 "",
                                 sizeIsMetric,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 10204,     // 216 mm 
                                 15590,     // 330 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.JP_Postcard,
                                 "Japanese Postcard",
                                 "(Hagaki): 100 mm x 148 mm",
                                 71,
                                 (byte)PCLXLAttrEnums.eVal.eJPostcard,
                                 "JPOST",
                                 sizeIsMetric,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 4724,      // 100 mm 
                                 6992,      // 148 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.JP_PostcardDouble,
                                 "Japanese Double Postcard",
                                 "(Oufuku-Hagaki): 148 mm x 200 mm",
                                 72,
                                 (byte)PCLXLAttrEnums.eVal.eJDoublePostcard,
                                 "JPOSTD",
                                 sizeIsMetric,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 6992,      // 148 mm 
                                 9448,      // 200 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.Ledger_Alt,
                                 "Ledger (alternative)",
                                 "11\" x 17\"",
                                 11,
                                 _unknownEnum,
                                 "",
                                 sizeIsImperial,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 13200,     // 11"
                                 20400,     // 17"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.Legal,
                                 "Legal",
                                 "8.5\" x 14\"",
                                 3,
                                 (byte)PCLXLAttrEnums.eVal.eLegalPaper,
                                 "LEGAL",
                                 sizeIsImperial,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 10200,     // 8.5"
                                 16800,     // 14"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.Oficio_8p5x13,
                                 "Oficio 8.5x13",
                                 "8.5\" x 13\"",
                                 _unknownID,
                                _unknownEnum,
                                 "8.5X13",
                                 sizeIsImperial,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 10200,     // 8.5"
                                 15600,     // 13"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.Oficio_216x340,
                                 "Oficio 216x340",
                                 "216 mm x 340 mm",
                                 _unknownID,
                                 _unknownEnum,
                                 "OFICIO 216X340MM",
                                 sizeIsMetric,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 10204,     // 216 mm
                                 16062,     // 340 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.Oficio_216x343,
                                 "Oficio 216x343",
                                 "216 mm x 343 mm",
                                 _unknownID,
                                 _unknownEnum,
                                 "OFICIO 216X343MM",
                                 sizeIsMetric,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 10204,     // 216 mm
                                 16204,     // 343 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.Oficio_216x347,
                                 "Oficio 216x347",
                                 "216 mm x 347 mm",
                                 _unknownID,
                                 _unknownEnum,
                                 "OFICIO 216X347MM",
                                 sizeIsMetric,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 10204,     // 216 mm
                                 16393,     // 347 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.RA3,
                                 "RA3",
                                 "Raw format A3: 305 mm x 430 mm",
                                 _unknownID,
                                 _unknownEnum,
                                 "RA3",
                                 sizeIsMetric,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 14409,     // 305 mm 
                                 20314,     // 430 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.RA4,
                                 "RA4",
                                 "Raw format A4: 215 mm x 305 mm",
                                 _unknownID,
                                 _unknownEnum,
                                 "RA4",
                                 sizeIsMetric,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 10157,     // 215 mm
                                 14409,     // 305 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.SRA3,
                                 "SRA3",
                                 "Supplmentary raw format A3: 320 mm x 450 mm",
                                 _unknownID,
                                 _unknownEnum,
                                 "SRA3",
                                 sizeIsMetric,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 15118,     // 320 mm 
                                 21259,     // 450 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.SRA4,
                                 "SRA4",
                                 "Supplementary raw format A4: 225 mm x 320 mm",
                                 _unknownID,
                                 _unknownEnum,
                                 "SRA4",
                                 sizeIsMetric,
                                 sizeIsCommon,
                                 _paperSizeUPI,
                                 10629,     // 225 mm
                                 15118,     // 320 mm
                                 284,
                                 236,
                                 200),

                new PCLPaperSize(eIndex.Statement,
                                 "Statement",
                                 "(Memo, Half-Letter): 5.5\" x 8.5\"",
                                 15,
                                 _unknownEnum,
                                 "STATEMENT",
                                 sizeIsImperial,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 6600,      // 5.5"
                                 10200,     // 8.5"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.USGovt_Legal,
                                 "US Government Legal",
                                 "8.5\" x 13\"",
                                 8,
                                 _unknownEnum,
                                 "",
                                 sizeIsImperial,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 10200,     // 8.5"
                                 15600,     // 13"
                                 300,
                                 240,
                                 200),

                new PCLPaperSize(eIndex.USGovt_Letter,
                                 "US Government Letter",
                                 "8\" x 10.5\"",
                                 7,
                                 _unknownEnum,
                                 "",
                                 sizeIsImperial,
                                 sizeIsRare,
                                 _paperSizeUPI,
                                 9600,      // 8"
                                 12600,     // 10.5"
                                 300,
                                 240,
                                 200)            };
        
            _paperSizeCount = _paperSizes.GetUpperBound(0) + 1;

            if (_paperSizeCount != _eIndexCt)
            {
                MessageBox.Show("Count '" + _paperSizeCount + "' of defined" +
                                " paper sizes is not equal to count '" +
                                _eIndexCt + "' of index items!",
                                "Internal design-time logic error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e s e t C u s t o m D e s c                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Reset the description field associated with the Custom paper size  //
        // to a blank string.                                                 //
        // This is only called when the paper size selected is <Custom>.      //
        // It is necessary because, with other paper sizes, the 'custom'      //
        // mechanism is invoked if no (PDL-specific) paper identifier code is //
        // known, and the custom 'desc' field is used to temporarily store    //
        // the name of the sledcted paper size.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void resetCustomDesc()
        {
            _paperSizes[(Int32)eIndex.Custom].CustomDesc = "";
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t C u s t o m L o n g E d g e                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store value defining the size of the 'long edge' of the Custom     //
        // paper size.                                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void setCustomLongEdge (UInt16 size,
                                              UInt16 sessionUPI)
        {
            _paperSizes[(Int32)eIndex.Custom].CustomLongEdge =
                (UInt32) ((size * _paperSizeUPI) / sessionUPI);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t C u s t o m S h o r t E d g e                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store value defining the size of the 'short edge' of the Custom    //
        // paper size.                                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void setCustomShortEdge (UInt16 size,
                                               UInt16 sessionUPI)
        {
            _paperSizes[(Int32)eIndex.Custom].CustomShortEdge =
                (UInt32) ((size * _paperSizeUPI) / sessionUPI);
        }
    }
}