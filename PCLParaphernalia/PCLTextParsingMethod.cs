using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class handles a PCL Text Parsing Method object.
    /// 
    /// © Chris Hutchinson 2015
    /// 
    /// </summary>

    class PCLTextParsingMethod
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private PCLTextParsingMethods.eIndex _indxMethod;

        private Int16 _value;

        private String _desc;

        private UInt16 [] _rangeDataSingle;
        private UInt16 [] _rangeDataDouble;


        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L T e x t P a r s i n g M e t h o d                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PCLTextParsingMethod (
            PCLTextParsingMethods.eIndex indxMethod,
            Int16                         value,
            String                        desc,
            UInt16 []                     rangeDataSingle,
            UInt16 []                     rangeDataDouble)
        {
            _indxMethod = indxMethod;
            _value      = value;
            _desc       = desc;
            
            _rangeDataSingle = rangeDataSingle;
            _rangeDataDouble = rangeDataDouble;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t D e s c                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the description.                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String getDesc ()
        {
            return _desc;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t D e s c L o n g                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the PCL identifier value and description, except for the    //
        // "<not specified>" entry (which has a dummy negative value).        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String getDescLong ()
        {
            if (_value < 0)
                return _desc;
            else
                return (_value.ToString () + ": " + _desc);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t M e t h o d T y p e                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the method index.                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PCLTextParsingMethods.eIndex getMethodType ()
        {
            return _indxMethod;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t R a n g e D a t a D o u b l e                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the double-byte range(s).                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public UInt16 [] getRangeDataDouble ()
        {
            return _rangeDataDouble;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t R a n g e D a t a D o u b l e C t                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the count of double-byte range(s).                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int32 getRangeDataDoubleCt ()
        {
            if (_rangeDataDouble == null)
                return 0;
            else
                return (_rangeDataDouble.Length / 2);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t R a n g e D a t a S i n g l e                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the single-byte range(s).                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public UInt16 [] getRangeDataSingle ()
        {
            return _rangeDataSingle;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t R a n g e D a t a S i n g l e C t                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the count of single-byte range(s).                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int32 getRangeDataSingleCt ()
        {
            if (_rangeDataSingle == null)
                return 0;
            else
                return (_rangeDataSingle.Length / 2);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t V a l u e                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the PCL identifier value.                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int32 getValue ()
        {
            return _value;
        }
    }
}