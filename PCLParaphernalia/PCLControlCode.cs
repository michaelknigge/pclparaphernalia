using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines a PCL Control Code.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    // [System.Reflection.ObfuscationAttribute(Feature = "properties renaming")]
    [System.Reflection.ObfuscationAttribute (
        Feature = "renaming",
        ApplyToMembers = true)]

    class PCLControlCode
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Byte    _value;
        private String _mnemonic;
        private String  _description;

        private Boolean _flagLineTerm;

        private Int32 _statsCtParent;
        private Int32 _statsCtChild;

        private PrnParseConstants.eOvlAct _makeOvlAct;

        private PrnParseConstants.eSeqGrp _seqGrp;
        
        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L C o n t r o l C o d e                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PCLControlCode(
            Byte    value,
            Boolean flagLineTerm,
            String mnemonic,
            PrnParseConstants.eOvlAct makeOvlAct,
            PrnParseConstants.eSeqGrp seqGrp,
            String description)
        {
            _value             = value;
            _mnemonic          = mnemonic;
            _description       = description;
            _flagLineTerm      = flagLineTerm;
            _makeOvlAct        = makeOvlAct;
            _seqGrp            = seqGrp;

            _statsCtParent = 0;
            _statsCtChild = 0;
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // D e s c E x c M n e m o n i c                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String DescExcMnemonic
        {
            get { return _description; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // D e s c r i p t i o n                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String Description
        {
            get { return _mnemonic + ": " + _description; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g L i n e T e r m                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagLineTerm
        {
            get { return _flagLineTerm; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g O b s o l e t e                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagObsolete
        {
            get { return false; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g V a l I s L e n                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagValIsLen
        {
            get { return false; }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n c r e m e n t S t a t i s t i c s C o u n t                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Increment 'statistics' count.                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void incrementStatisticsCount(Int32 level)
        {
            if (level == 0)
                _statsCtParent++;
            else
                _statsCtChild++;
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // M a k e O v e r l a y A c t i o n                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PrnParseConstants.eOvlAct makeOvlAct
        {
            get { return _makeOvlAct; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // M n e m o n i c                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String Mnemonic
        {
            get { return _mnemonic; }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e s e t S t a t i s t i c s                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Reset 'statistics' counts.                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void resetStatistics()
        {
            _statsCtParent = 0;
            _statsCtChild = 0;
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // S e q u e n c e                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String Sequence
        {
            get { return "0x" + _value.ToString("x2"); }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // S t a t s C t C h i l d                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int32 StatsCtChild
        {
            get { return _statsCtChild; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // S t a t s C t P a r e n t                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int32 StatsCtParent
        {
            get { return _statsCtParent; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // S t a t s C t T o t a l                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int32 StatsCtTotal
        {
            get { return (_statsCtParent + _statsCtChild); }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // T y p e                                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String Type
        {
            get { return "Control"; }
        }
    }
}