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

    class PCLComplexSeq
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Byte    _keyPChar;
        private Byte    _keyGChar;
        private Byte    _keyTChar;

        private String  _description;

        private Boolean _flagDiscrete;
        private Boolean _flagNilGChar;
        private Boolean _flagNilValue;
        private Boolean _flagObsolete;
        private Boolean _flagResetGL2;
        private Boolean _flagValIsLen;

        private Boolean _flagDisplayHexVal;

        private Boolean _flagValGeneric;
        private Boolean _flagValVarious;

        private Int32   _value;
        private Int32   _statsCtParent;
        private Int32   _statsCtChild;

        private PrnParseConstants.eActPCL _actionType;

        private PrnParseConstants.eOvlAct _makeOvlAct;

        private PrnParseConstants.eSeqGrp _seqGrp;
      
        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L C o m p l e x S e q                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PCLComplexSeq(
            Byte    keyPChar,
            Byte    keyGChar,
            Byte    keyTChar,
            Int32   value,
            Boolean flagDiscrete,
            Boolean flagNilGChar,
            Boolean flagNilValue,
            Boolean flagValIsLen,
            Boolean flagObsolete,
            Boolean flagResetGL2,
            Boolean flagDisplayHexVal,
            PrnParseConstants.eActPCL actionType,
            PrnParseConstants.eOvlAct makeOvlAct,
            PrnParseConstants.eSeqGrp seqGrp,
            String description)
        {
            _keyPChar          = keyPChar;
            _keyGChar          = keyGChar;
            _keyTChar          = keyTChar;

            _value             = value;
            _actionType        = actionType;
 
            _description       = description;

            _flagDiscrete = flagDiscrete;
            _flagNilGChar = flagNilGChar;
            _flagNilValue = flagNilValue;
            _flagValIsLen = flagValIsLen;
            _flagObsolete = flagObsolete;
            _flagResetGL2 = flagResetGL2;

            _flagDisplayHexVal = flagDisplayHexVal;

            _makeOvlAct = makeOvlAct;
            _seqGrp     = seqGrp;

            if (value == PCLComplexSeqs._valueGeneric)
                _flagValGeneric = true;
            else
                _flagValGeneric = false;

            if (value == PCLComplexSeqs._valueVarious)
                _flagValVarious = true;
            else
                _flagValVarious = false;

            _statsCtParent = 0;
            _statsCtChild  = 0;
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // A c t i o n T y p e                                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PrnParseConstants.eActPCL ActionType
        {
            get { return _actionType; }
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
        // F l a g D i s c r e t e                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagDiscrete
        {
            get { return _flagDiscrete; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g D i s p l a y H e x V a l                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagDisplayHexVal
        {
            get { return _flagDisplayHexVal; }
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
        // F l a g N i l G C h a r                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagNilGChar
        {
            get { return _flagNilGChar; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g N i l V a l u e                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagNilValue
        {
            get { return _flagNilValue; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g R e s e t G L 2                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagResetGL2
        {
            get { return _flagResetGL2; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g V a l G e n e r i c                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagValGeneric
        {
            get { return _flagValGeneric; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g V a l I s L e n                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagValIsLen
        {
            get { return _flagValIsLen; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g V a l V a r i o u s                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagValVarious
        {
            get { return _flagValVarious; }
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
            _statsCtChild  = 0;
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // S e q u e n c e                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String Sequence
        {
            get
            {
                String seq;
                String value;

                if ((_flagDiscrete) && (!_flagValGeneric) && (!_flagValVarious))
                    value = " (#=" + _value.ToString() + ")";
                else
                    value = "";                
                if (_flagNilValue)
                {
                    if (_flagNilGChar)
                        seq = "<Esc>" + (Char) _keyPChar +
                                        (Char) _keyTChar;
                    else
                        seq = "<Esc>" + (Char) _keyPChar +
                                        (Char) _keyGChar +
                                        (Char) _keyTChar;
                }
                else if (_flagNilGChar)
                {
                    seq = "<Esc>" + (Char) _keyPChar + "#" +
                                    (Char) _keyTChar +
                                    value;
                }
                else
                {
                    seq = "<Esc>" + (Char) _keyPChar +
                                    (Char) _keyGChar + "#" +
                                    (Char) _keyTChar +
                                    value;
                }
                
                return seq;
            }
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
            get { return "Complex"; }
        }
    }
}