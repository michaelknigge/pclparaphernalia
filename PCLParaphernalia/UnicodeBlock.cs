using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines a Unicode block.
    /// 
    /// © Chris Hutchinson 2017
    /// 
    /// </summary>

    class UnicodeBlock
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private UInt32 _rangeStart;
        private UInt32 _rangeEnd;
        private String  _name;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // U n i c o d e B l o c k                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public UnicodeBlock (
            UInt32  rangeStart,
            UInt32  rangeEnd,
            String name)
        {
            _rangeStart = rangeStart;
            _rangeEnd   = rangeEnd;
            _name       = name;
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // N a m e                                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String Name
        {
            get { return _name; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // R a n g e E n d                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public UInt32 RangeEnd
        {
            get { return _rangeEnd; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // R a n g e S t a r t                                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public UInt32 RangeStart
        {
            get { return _rangeStart; }
        }
    }
}