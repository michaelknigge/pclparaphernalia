using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class handles a PCL Palette object.
    /// 
    /// © Chris Hutchinson 2014
    /// 
    /// </summary>

    class PCLPalette
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private String _name;

        private Boolean _flagMonochrome;

        private Int16  _paletteId;
        private Byte   _ctClrItems;
        private Byte   _crntClrItem;

        private Byte   _clrItemWhite;
        private Byte   _clrItemBlack;

        private Byte [] _colourIds;
        private String [] _colourNames;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L P a l e t t e                                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PCLPalette (String    name,
                           Boolean   flagMonochrome,
                           Int16     paletteId,
                           Byte      ctClrItems)
        {
            _name           = name;
            _flagMonochrome = flagMonochrome;
            _paletteId      = paletteId;
            _ctClrItems     = ctClrItems;

            _colourIds   = new Byte[ctClrItems];
            _colourNames = new String[ctClrItems];

            _crntClrItem = 0;
            _clrItemWhite = 0;
            _clrItemBlack   = 0;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // a d d C o l o u r                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Add a colour to the next slot in the palette.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void addColour (String name,
                               Byte   id)
        {
            _colourNames[_crntClrItem] = name;
            _colourIds  [_crntClrItem] = id;

            _crntClrItem++;
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // C l r I t e m B l a c k                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Byte ClrItemBlack
        {
            get { return _clrItemBlack; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // C l r I t e m W h i t e                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Byte ClrItemWhite
        {
            get { return _clrItemWhite; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // C t C l r I t e m s                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the count of colour items in the palette.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Byte CtClrItems
        {
            get { return _ctClrItems; }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C o l o u r I d                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Get the PCL colour identifier for the specified colour entry.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Byte getColourId (Int32 item)
        {
            return _colourIds[item];
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C o l o u r N a m e                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Get the colour name for the specified colour entry.                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String getColourName (Int32 item)
        {
            return _colourNames[item];
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // M o n o c h r o m e                                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean Monochrome
        {
            get { return _flagMonochrome; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // P a l e t t e I d                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the PCL identifier value for the palette.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int16 PaletteId
        {
            get { return _paletteId; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // P a l e t t e N a m e                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the palette name.                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String PaletteName
        {
            get { return _name; }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t C l r I t e m B l a c k                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set the Black index to the current entry.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void setClrItemBlack ()
        {
            _clrItemBlack = (Byte) (_crntClrItem - 1); 
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t C l r I t e m W h i t e                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set the White index to the current entry.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void setClrItemWhite ()
        {
            _clrItemWhite = (Byte) (_crntClrItem - 1);
        }
    }
}