using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class handles a PCL Logical Operation object.
    /// 
    /// © Chris Hutchinson 2014
    /// 
    /// </summary>

    class PCLLogicalOperation
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        const Int32 _maxPFLen = 11;

        Int16 _opId;
        Int16 _opCode;

        private String _actPostfix;
        private String _actInfix;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L L o g i c a l O p e r a t i o n                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PCLLogicalOperation (Int16  opId,
                                    Int16  opCode,
                                    String actPostfix,
                                    String actInfix)
        {
            _opId   = opId;
            _opCode = opCode;       // not used at present //

            _actPostfix  = actPostfix;
            _actInfix    = actInfix;
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // A c t I n f i x                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the Infix representation of the action associated with this //
        // operator.                                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String ActInfix
        {
            get { return _actInfix; }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t D e s c L o n g                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the long form of the ROP description; this includes:        //
        //  - the ROP index                                                   //
        //  - the Postfix representation of the action                        //
        //  - the Infix representation of the action                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String getDescLong()
        {
            String prefix;

            if (_opId < 10)
                prefix = "  ";
            else if (_opId < 100)
                prefix = " ";
            else
                prefix = "";

            return prefix + _opId.ToString () +
                   ": " + _actPostfix.PadRight (_maxPFLen) +
                   " = " + _actInfix;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t D e s c S h o r t                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the short form of the ROP description; this includes:       //
        //  - the ROP index                                                   //
        //  - the Postfix representation of the action                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String getDescShort()
        {
            String prefix;

            if (_opId < 10)
                prefix = "  ";
            else if (_opId < 100)
                prefix = " ";
            else
                prefix = "";

            return prefix + _opId.ToString () +
                   ": " + _actPostfix;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t R O P I d                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the identifier of the ROP.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int16 getROPId ()
        {
            return _opId;
        }
    }
}