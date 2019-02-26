using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides definitions for standard PCL patterns.
    /// 
    /// © Chris Hutchinson 2016
    /// 
    /// </summary>

    public static class PCLPatternDefs
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const Byte cPatternHeight = 16;
        const Byte cPatternWidth  = 16;

        public enum eType : byte
        {
            Shading = 0,
            CrossHatch
        }

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        // All of the patterns defined here are monochrome, for a square of   //
        // width and height 16 pixels (= 256 pixels, so 64 byte definitions). //
        //                                                                    //
        //--------------------------------------------------------------------//

        static Byte[] _shade_1 = { 0x00, 0x00, 0x00, 0x00,
                                   0x40, 0x40, 0x00, 0x00,
                                   0x00, 0x00, 0x00, 0x00,
                                   0x00, 0x00, 0x00, 0x00,
                                   0x00, 0x00, 0x00, 0x00,
                                   0x04, 0x04, 0x00, 0x00,
                                   0x00, 0x00, 0x00, 0x00,
                                   0x00, 0x00, 0x00, 0x00 };

        static Byte[] _shade_2 = { 0x00, 0x00, 0x00, 0x00,
                                   0x40, 0x40, 0x00, 0x00,
                                   0x00, 0x00, 0x00, 0x00,
                                   0x04, 0x04, 0x00, 0x00,
                                   0x00, 0x00, 0x00, 0x00,
                                   0x40, 0x40, 0x00, 0x00,
                                   0x00, 0x00, 0x00, 0x00,
                                   0x04, 0x04, 0x00, 0x00 };

        static Byte[] _shade_3 = { 0x00, 0x00, 0x60, 0x60,
                                   0x60, 0x60, 0x00, 0x00,
                                   0x00, 0x00, 0x06, 0x06,
                                   0x06, 0x06, 0x00, 0x00,
                                   0x00, 0x00, 0x60, 0x60,
                                   0x60, 0x60, 0x00, 0x00,
                                   0x00, 0x00, 0x06, 0x06,
                                   0x06, 0x06, 0x00, 0x00 };

        static Byte[] _shade_4 = { 0xC1, 0xC1, 0xC1, 0xC1,
                                   0x80, 0x80, 0x08, 0x08,
                                   0x1C, 0x1C, 0x1C, 0x1C,
                                   0x08, 0x08, 0x80, 0x80,
                                   0xC1, 0xC1, 0xC1, 0xC1,
                                   0x80, 0x80, 0x08, 0x08,
                                   0x1C, 0x1C, 0x1C, 0x1C,
                                   0x08, 0x08, 0x80, 0x80 };

        static Byte[] _shade_5 = { 0xC1, 0xC1, 0xEB, 0xEB,
                                   0xC1, 0xC1, 0x88, 0x88,
                                   0x1C, 0x1C, 0xBE, 0xBE,
                                   0x1C, 0x1C, 0x88, 0x88,
                                   0xC1, 0xC1, 0xEB, 0xEB,
                                   0xC1, 0xC1, 0x88, 0x88,
                                   0x1C, 0x1C, 0xBE, 0xBE,
                                   0x1C, 0x1C, 0x88, 0x88 };

        static Byte[] _shade_6 = { 0xE3, 0xE3, 0xE3, 0xE3,
                                   0xE3, 0xE3, 0xFF, 0xFF,
                                   0x3E, 0x3E, 0x3E, 0x3E,
                                   0x3E, 0x3E, 0xFF, 0xFF,
                                   0xE3, 0xE3, 0xE3, 0xE3,
                                   0xE3, 0xE3, 0xFF, 0xFF,
                                   0x3E, 0x3E, 0x3E, 0x3E,
                                   0x3E, 0x3E, 0xFF, 0xFF };

        static Byte[] _shade_7 = { 0xF7, 0xF7, 0xE3, 0xE3,
                                   0xF7, 0xF7, 0xFF, 0xFF,
                                   0x7F, 0x7F, 0x3E, 0x3E,
                                   0x7F, 0x7F, 0xFF, 0xFF,
                                   0xF7, 0xF7, 0xE3, 0xE3,
                                   0xF7, 0xF7, 0xFF, 0xFF,
                                   0x7F, 0x7F, 0x3E, 0x3E,
                                   0x7F, 0x7F, 0xFF, 0xFF };

        static Byte[] _shade_8 = { 0xFF, 0xFF, 0xFF, 0xFF,
                                   0xFF, 0xFF, 0xFF, 0xFF,
                                   0xFF, 0xFF, 0xFF, 0xFF,
                                   0xFF, 0xFF, 0xFF, 0xFF,
                                   0xFF, 0xFF, 0xFF, 0xFF,
                                   0xFF, 0xFF, 0xFF, 0xFF,
                                   0xFF, 0xFF, 0xFF, 0xFF,
                                   0xFF, 0xFF, 0xFF, 0xFF };

        //--------------------------------------------------------------------//

        static Byte[] _hatch_1 = { 0x00, 0x00, 0x00, 0x00,
                                   0x00, 0x00, 0x00, 0x00,
                                   0x00, 0x00, 0x00, 0x00,
                                   0x00, 0x00, 0xFF, 0xFF,
                                   0xFF, 0xFF, 0x00, 0x00,
                                   0x00, 0x00, 0x00, 0x00,
                                   0x00, 0x00, 0x00, 0x00,
                                   0x00, 0x00, 0x00, 0x00 };

        static Byte[] _hatch_2 = { 0x01, 0x80, 0x01, 0x80,
                                   0x01, 0x80, 0x01, 0x80,
                                   0x01, 0x80, 0x01, 0x80,
                                   0x01, 0x80, 0x01, 0x80,
                                   0x01, 0x80, 0x01, 0x80,
                                   0x01, 0x80, 0x01, 0x80,
                                   0x01, 0x80, 0x01, 0x80,
                                   0x01, 0x80, 0x01, 0x80 };

        static Byte[] _hatch_3 = { 0x80, 0x03, 0x00, 0x07,
                                   0x00, 0x0E, 0x00, 0x1C,
                                   0x00, 0x38, 0x00, 0x70,
                                   0x00, 0xE0, 0x01, 0xC0,
                                   0x03, 0x80, 0x07, 0x00,
                                   0x0E, 0x00, 0x1C, 0x00,
                                   0x38, 0x00, 0x70, 0x00,
                                   0xE0, 0x00, 0xC0, 0x01 };

        static Byte[] _hatch_4 = { 0xC0, 0x01, 0xE0, 0x00,
                                   0x70, 0x00, 0x38, 0x00,
                                   0x1C, 0x00, 0x0E, 0x00,
                                   0x07, 0x00, 0x03, 0x80,
                                   0x01, 0xC0, 0x00, 0xE0,
                                   0x00, 0x70, 0x00, 0x38,
                                   0x00, 0x1C, 0x00, 0x0E,
                                   0x00, 0x07, 0x80, 0x03 };

        static Byte[] _hatch_5 = { 0x01, 0x80, 0x01, 0x80,
                                   0x01, 0x80, 0x01, 0x80,
                                   0x01, 0x80, 0x01, 0x80,
                                   0x01, 0x80, 0xFF, 0xFF,
                                   0xFF, 0xFF, 0x01, 0x80,
                                   0x01, 0x80, 0x01, 0x80,
                                   0x01, 0x80, 0x01, 0x80,
                                   0x01, 0x80, 0x01, 0x80 };

        static Byte[] _hatch_6 = { 0xC0, 0x03, 0xE0, 0x07,
                                   0x70, 0x0E, 0x38, 0x1C,
                                   0x1C, 0x38, 0x0E, 0x70,
                                   0x07, 0xE0, 0x03, 0xC0,
                                   0x03, 0xC0, 0x07, 0xE0,
                                   0x0E, 0x70, 0x1C, 0x38,
                                   0x38, 0x1C, 0x70, 0x0E,
                                   0xE0, 0x07, 0xC0, 0x03 };

        //--------------------------------------------------------------------//

        private static PCLPatternDef[] _patternsShade =
        {
            new PCLPatternDef ("1 - 2%",
                               eType.Shading,
                               1,
                               2,
                               cPatternHeight,
                               cPatternWidth,
							   _shade_1),

            new PCLPatternDef ("3 - 10%",
                               eType.Shading,
                               3,
                               10,
                               cPatternHeight,
                               cPatternWidth,
							   _shade_2),

            new PCLPatternDef ("11 - 20%",
                               eType.Shading,
                               11,
                               20,
                               cPatternHeight,
                               cPatternWidth,
                               _shade_3),

            new PCLPatternDef ("21 - 35%",
                               eType.Shading,
                               21,
                               35,
                               cPatternHeight,
                               cPatternWidth,
                               _shade_4),

            new PCLPatternDef ("36 - 55%",
                               eType.Shading,
                               36,
                               55,
                               cPatternHeight,
                               cPatternWidth,
                               _shade_5),

            new PCLPatternDef ("56 - 80%",
                               eType.Shading,
                               56,
                               80,
                               cPatternHeight,
                               cPatternWidth,
                               _shade_6),

            new PCLPatternDef ("81 - 99%",
                               eType.Shading,
                               81,
                               99,
                               cPatternHeight,
                               cPatternWidth,
                               _shade_7),

            new PCLPatternDef ("100%",
                               eType.Shading,
                               100,
                               100,
                               cPatternHeight,
                               cPatternWidth,
                               _shade_8)
        };

        //--------------------------------------------------------------------//

        private static PCLPatternDef[] _patternsHatch =
        {
            new PCLPatternDef ("horizontal lines",
                               eType.CrossHatch,
                               1,
                               0,   // not relevant
                               cPatternHeight,
                               cPatternWidth,
							   _hatch_1),

            new PCLPatternDef ("vertical lines",
                               eType.CrossHatch,
                               2,
                               0,   // not relevant
                               cPatternHeight,
                               cPatternWidth,
							   _hatch_2),

            new PCLPatternDef ("ascending diagonal lines",
                               eType.CrossHatch,
                               3,
                               0,   // not relevant
                               cPatternHeight,
                               cPatternWidth,
                               _hatch_3),

            new PCLPatternDef ("descending diagonal lines",
                               eType.CrossHatch,
                               4,
                               0,   // not relevant
                               cPatternHeight,
                               cPatternWidth,
                               _hatch_4),

            new PCLPatternDef ("crossed lines",
                               eType.CrossHatch,
                               5,
                               0,   // not relevant
                               cPatternHeight,
                               cPatternWidth,
                               _hatch_5),

            new PCLPatternDef ("crossed diagonal lines",
                               eType.CrossHatch,
                               6,
                               0,   // not relevant
                               cPatternHeight,
                               cPatternWidth,
                               _hatch_6)
        };

        //--------------------------------------------------------------------//

        private static Int32 _patternsCtShade =
            _patternsShade.GetUpperBound (0) + 1;

        private static Int32 _patternsCtHatch =
            _patternsHatch.GetUpperBound (0) + 1;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L P a t t e r n D e f s                                        //
        //                                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        static PCLPatternDefs ()
        {
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C o u n t                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Get the count of defined patterns of the specified type.           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 getCount (eType type)
        {
            if (type == eType.Shading)
                return _patternsCtShade;
            else if (type == eType.CrossHatch)
                return _patternsCtHatch;
            else
                return -1; // should never occur
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t B y t e s                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Get the bytes which define the pattern.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Byte[] getBytes (eType type,
                                       Int32 patternDefIndex)
        {
            if (type == eType.Shading)
                return _patternsShade[patternDefIndex].Pattern;
            else if (type == eType.CrossHatch)
                return _patternsHatch[patternDefIndex].Pattern;
            else
                return null; // should never occur
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t D e s c                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Get the description of the speciifed pattern.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getDesc (eType type,
                                      Int32 patternDefIndex)
        {
            if (type == eType.Shading)
                return _patternsShade[patternDefIndex].Desc;
            else if (type == eType.CrossHatch)
                return _patternsHatch[patternDefIndex].Desc;
            else
                return null; // should never occur
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t H e i g h t                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Get the height of the pattern definition.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt16 getHeight (eType type,
                                       Int32 patternDefIndex)
        {
            if (type == eType.Shading)
                return _patternsShade[patternDefIndex].Height;
            else if (type == eType.CrossHatch)
                return _patternsHatch[patternDefIndex].Height;
            else
                return 0; // should never occur
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I d                                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Get the primary identifier of the specified pattern.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt16 getId (eType type,
                                   Int32 patternDefIndex)
        {
            if (type == eType.Shading)
                return _patternsShade[patternDefIndex].Id;
            else if (type == eType.CrossHatch)
                return _patternsHatch[patternDefIndex].Id;
            else
                return 0; // should never occur
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I d S e c                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Get the secondary identifier of the specified pattern.             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt16 getIdSec (eType type,
                                      Int32 patternDefIndex)
        {
            if (type == eType.Shading)
                return _patternsShade[patternDefIndex].IdSec;
            else if (type == eType.CrossHatch)
                return _patternsHatch[patternDefIndex].IdSec;
            else
                return 0; // should never occur
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t W i d t h                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Get the width of the pattern definition.                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt16 getWidth (eType type,
                                      Int32 patternDefIndex)
        {
            if (type == eType.Shading)
                return _patternsShade[patternDefIndex].Width;
            else if (type == eType.CrossHatch)
                return _patternsHatch[patternDefIndex].Width;
            else
                return 0; // should never occur
        }
    }
}
