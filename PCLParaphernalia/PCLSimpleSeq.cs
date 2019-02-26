using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines a PCL Simple Escape Sequence.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    // [System.Reflection.ObfuscationAttribute(Feature = "properties renaming")]
    [System.Reflection.ObfuscationAttribute (
        Feature = "renaming",
        ApplyToMembers = true)]

    class PCLSimpleSeq
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Byte    _keySChar;
        private String  _description;

        private Boolean _flagObsolete;
        private Boolean _flagResetHPGL2;

        private Int32 _statsCtParent;
        private Int32 _statsCtChild;

        private PrnParseConstants.eOvlAct _makeOvlAct;

        private PrnParseConstants.eSeqGrp _seqGrp;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L S i m p l e S e q                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PCLSimpleSeq(
            Byte    keySChar,
            Boolean flagObsolete,
            Boolean flagResetHPGL2,
            PrnParseConstants.eOvlAct makeOvlAct,
            PrnParseConstants.eSeqGrp seqGrp,
            String description)
        {
            _keySChar        = keySChar;
            _description     = description;
 
            _flagObsolete    = flagObsolete;
            _flagResetHPGL2  = flagResetHPGL2;

            _makeOvlAct      = makeOvlAct;
            _seqGrp          = seqGrp;

            _statsCtParent   = 0;
            _statsCtChild    = 0;
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // D e s c r i p t i o n                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String Description
        {
            get { return _description; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g O b s o l e t e                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagObsolete
        {
            get { return _flagObsolete; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g R e s e t H P G L 2                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagResetHPGL2
        {
            get { return _flagResetHPGL2; }
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
        // M a k e M a c r o A c t                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PrnParseConstants.eOvlAct makeOvlAct
        {
            get { return _makeOvlAct; }
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
            get { return "<Esc>" + (Char) _keySChar; }
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
            get { return "Simple"; }
        }
    }
}