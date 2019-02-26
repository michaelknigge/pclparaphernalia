using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class handles a PCLXL Palette object.
    /// 
    /// © Chris Hutchinson 2014
    /// 
    /// </summary>

    class PCLXLPalette
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private String _name;

        private Boolean _flagMonochrome;

        private Byte   _ctClrItems;
        private Byte   _crntClrItem;

        private Byte   _clrItemWhite;
        private Byte   _clrItemBlack;

        private Byte [] _colourIds;
        private Int32 [] _colourRGBs;
        private String [] _colourNames;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L X L P a l e t t e                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PCLXLPalette (String    name,
                             Boolean   flagMonochrome,
                             Byte      ctClrItems)
        {
            _name           = name;
            _flagMonochrome = flagMonochrome;
            _ctClrItems     = ctClrItems;

            _colourIds   = new Byte [ctClrItems];
            _colourNames = new String [ctClrItems];
            _colourRGBs  = new Int32[ctClrItems];

            _crntClrItem  = 0;
            _clrItemBlack = 0;
            _clrItemWhite = 0;
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
                               Int32  RGB,
                               Byte   id)
        {
            _colourNames[_crntClrItem] = name;
            _colourRGBs[_crntClrItem]  = RGB;
            _colourIds [_crntClrItem]  = id;

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
        // Return the count of colour items.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Byte CtClrItems
        {
            get { return _ctClrItems; }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C o l o u r N a m e                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Get the colour name for the specified colour item.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String getColourName (Int32 item)
        {
            return _colourNames[item];
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C o l o u r I d                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Get the colour identifier for the specified colour item.           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Byte getColourId (Int32 item)
        {
            return _colourIds [item];
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C o l o u r R G B                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Get the RGB value for the specified colour item.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int32 getColourRGB (Int32 item)
        {
            return _colourRGBs[item];
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t G r a y L e v e l                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Get the name of the gray level associated with the specified value.//
        // This is only relevant to the Gray palette.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String getGrayLevel (Byte item)
        {
            if (item == 0)
            {
                return item.ToString() +
                       " (" + _colourNames[_clrItemBlack] + ")";
            }
            else if (item == 255)
            {
                return item.ToString() +
                       " (" + _colourNames[_clrItemWhite] + ")";
            }
            else
            {
                return item.ToString();
            }
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