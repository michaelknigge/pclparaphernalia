using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides TTF Table handling for the Soft Font Generate tool.
    /// 
    /// © Chris Hutchinson 2012
    /// 
    /// </summary>

    class ToolSoftFontGenTTFTable
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private UInt32 _tag;
        private UInt32 _checksum;
        private UInt32 _offset;
        private UInt32 _length;
        private Int32  _padBytes;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // T o o l S o f t G e n T T F T a b l e                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public ToolSoftFontGenTTFTable(UInt32 tag)
        {
            _tag      = tag;
            _checksum = 0;
            _offset   = 0;
            _length   = 0;
            _padBytes = 0;
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // T a b l e C h e c k s u m                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the checksum of the table in the TTF file.                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public UInt32 TableChecksum
        {
            get { return _checksum; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // T a b l e L e n g t h                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the (unpadded) length of the table in the TTF file.         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public UInt32 TableLength
        {
            get { return _length; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // T a b l e O f f s e t                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the offset (start position) of the table in the TTF file.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public UInt32 TableOffset
        {
            get { return _offset; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // T a b l e P a d B y t e s                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the number (0->3) of pad bytes required for the table from  //
        // the TTF file.                                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int32 TablePadBytes
        {
            get { return _padBytes; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // T a b l e P a d L e n                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the (padded) length of the table in the TTF file.           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public UInt32 TablePadLen
        {
            get { return (UInt32) (_length + _padBytes); }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // T a b l e T a g                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the tag of the table in the TTF file.                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public UInt32 TableTag
        {
            get { return _tag; }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t M e t r i c s                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return metrics (size and position) of the table.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void getByteRange(ref UInt32 offset,
                                 ref UInt32 length)
        {
            offset   = _offset;
            length   = _length;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n i t i a l i s e                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Reset tables details (except tag).                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void initialise ()
        {
            _checksum = 0;
            _offset   = 0;
            _length   = 0;
            _padBytes = 0;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t D e t a i l s                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set details (except tag) of the table.                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void setMetrics(UInt32 checksum,
                               UInt32 offset,
                               UInt32 length,
                               Int32 padBytes)
        {
            _checksum = checksum;
            _offset   = offset;
            _length   = length;
            _padBytes = padBytes;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // z e r o L e n g t h                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return value indicating whether table is present (non-zero length) //
        // or not.                                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean zeroLength()
        {
            if (_length == 0)
                return true;
            else
                return false;
        }
    }
}
