using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides definitions for simple colour palettes.
    /// 
    /// © Chris Hutchinson 2014
    /// 
    /// </summary>

    public static class PCLPalettes
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public enum eIndex : byte
        {
            PCLMonochrome = 0,
            PCLSimpleColourCMY,
            PCLSimpleColourRGB
        }

        private enum eSimpleMono : byte
        {
            White = 0,
            Black
        }

        private enum eSimpleRGB : byte
        {
            Black = 0,
            Red,
            Green,
            Yellow,
            Blue,
            Magenta,
            Cyan,
            White
        }

        private enum eSimpleCMY : byte
        {
            White = 0,
            Cyan,
            Magenta,
            Blue,
            Yellow,
            Green,
            Red,
            Black
        }

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static PCLPalette[] _palettes = 
        {
            new PCLPalette ("Monochrome",
                            true,
                            1,
                            2),

            new PCLPalette ("Simple Colour CMY",
                            false,
                            -3,
                            8),

            new PCLPalette ("Simple Colour RGB",
                            false, 
                            3,
                            8)
        };

        private static Int32 _paletteCount =
            _palettes.GetUpperBound (0) + 1;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L P a l e t t e s                                              //
        //                                                                    //
        // In each palette, the colours are added in alphabetic order, rather //
        // than the order of the colour indices.                              //
        // This is in order to to make it easier to keep the same list of     //
        // available colours in client interfaces when switching between the  //
        // different palettes.                                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        static PCLPalettes ()
        {
            Int32 crntIndex;
            
            //----------------------------------------------------------------//

            crntIndex = (Int32) eIndex.PCLMonochrome;

            _palettes[crntIndex].addColour ("Black", (Byte) eSimpleMono.Black);
            _palettes[crntIndex].setClrItemBlack ();
            _palettes[crntIndex].addColour ("White", (Byte) eSimpleMono.White);
            _palettes[crntIndex].setClrItemWhite ();
            
            //----------------------------------------------------------------//

            crntIndex = (Int32) eIndex.PCLSimpleColourCMY;

            _palettes[crntIndex].addColour ("Black",
                                            (Byte) eSimpleCMY.Black);
            _palettes[crntIndex].setClrItemBlack ();
            _palettes[crntIndex].addColour ("Blue",
                                            (Byte) eSimpleCMY.Blue);
            _palettes[crntIndex].addColour ("Cyan",
                                            (Byte) eSimpleCMY.Cyan);
            _palettes[crntIndex].addColour ("Green",
                                            (Byte) eSimpleCMY.Green);
            _palettes[crntIndex].addColour ("Magenta",
                                            (Byte) eSimpleCMY.Magenta);
            _palettes[crntIndex].addColour ("Red",
                                            (Byte) eSimpleCMY.Red);
            _palettes[crntIndex].addColour ("White",
                                            (Byte) eSimpleCMY.White);
            _palettes[crntIndex].setClrItemWhite ();
            _palettes[crntIndex].addColour ("Yellow",
                                            (Byte) eSimpleCMY.Yellow);
            
            //----------------------------------------------------------------//

            crntIndex = (Int32) eIndex.PCLSimpleColourRGB;

            _palettes[crntIndex].addColour ("Black",
                                            (Byte) eSimpleRGB.Black);
            _palettes[crntIndex].setClrItemBlack ();
            _palettes[crntIndex].addColour ("Blue",
                                            (Byte) eSimpleRGB.Blue);
            _palettes[crntIndex].addColour ("Cyan",
                                            (Byte) eSimpleRGB.Cyan);
            _palettes[crntIndex].addColour ("Green",
                                            (Byte) eSimpleRGB.Green);
            _palettes[crntIndex].addColour ("Magenta",
                                            (Byte) eSimpleRGB.Magenta);
            _palettes[crntIndex].addColour ("Red",
                                            (Byte) eSimpleRGB.Red);
            _palettes[crntIndex].addColour ("White",
                                            (Byte) eSimpleRGB.White);
            _palettes[crntIndex].setClrItemWhite ();
            _palettes[crntIndex].addColour ("Yellow",
                                            (Byte) eSimpleRGB.Yellow);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C o l o u r I d                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Get the PCL id for the specified colour in the specified palette.  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Byte getColourId (Int32 paletteIndex,
                                        Int32 colourIndex)
        {
            return _palettes[paletteIndex].getColourId (colourIndex);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C o l o u r N a m e                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Get the colour name for the specified palette index.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getColourName (Int32 paletteIndex,
                                            Int32 colourIndex)
        {
            return _palettes[paletteIndex].getColourName (colourIndex);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C l r I t e m B l a c k                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Get the index of the black colour in the specified palette.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Byte getClrItemBlack (Int32 paletteIndex)
        {
            return _palettes[paletteIndex].ClrItemBlack;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C l r I t e m W h i t e                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Get the index of the white colour in the specified palette.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Byte getClrItemWhite (Int32 paletteIndex)
        {
            return _palettes[paletteIndex].ClrItemWhite;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C t C l r I t e m s                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Get the count of colour items in the specified palette.            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Byte getCtClrItems (Int32 paletteIndex)
        {
            return _palettes[paletteIndex].CtClrItems;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t P a l e t t e I d                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Get the PCL identifier for the specified (simple) palette index.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int16 getPaletteId (Int32 paletteIndex)
        {
            return _palettes[paletteIndex].PaletteId;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t P a l e t t e N a m e                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Get the name for the specified palette index.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getPaletteName (Int32 paletteIndex)
        {
            return _palettes[paletteIndex].PaletteName;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i s M o n o c h r o m e                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Is the selected palette monochrome?                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean isMonochrome (Int32 paletteIndex)
        {
            return _palettes[paletteIndex].Monochrome;
        }
    }
}
