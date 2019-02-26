using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines a PCL XL Attribute tag.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    // [System.Reflection.ObfuscationAttribute(Feature = "properties renaming")]
    [System.Reflection.ObfuscationAttribute (
        Feature = "renaming",
        ApplyToMembers = true)]

    class PCLXLAttribute
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Int32 _tagLen;
        private Byte _tagA;
        private Byte _tagB;

        private String  _description;

        private Boolean _flagReserved;
        private Boolean _flagAttrEnum;
        private Boolean _flagOperEnum;
        private Boolean _flagUbyteTxt;
        private Boolean _flagUintTxt;
        private Boolean _flagValIsLen;
        private Boolean _flagValIsPCL;

        private PrnParseConstants.eActPCLXL _actionType;
        private PrnParseConstants.eOvlAct _makeOvlAct;

        private Int32 _statsCtParent;
        private Int32 _statsCtChild;
      
        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L X L A t t r i b u t e                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PCLXLAttribute(Int32   tagLen,
                              Byte    tagA,
                              Byte    tagB,
                              Boolean flagReserved,
                              Boolean flagAttrEnum,
                              Boolean flagOperEnum,
                              Boolean flagUbyteTxt,
                              Boolean flagUintTxt,
                              Boolean flagValIsLen,
                              Boolean flagValIsPCL,
                              PrnParseConstants.eActPCLXL actionType,
                              PrnParseConstants.eOvlAct makeOvlAct,
                              String description)
	    {
            _tagLen            = tagLen;
            _tagA              = tagA;
            _tagB              = tagB;
            _flagReserved      = flagReserved;
            _flagAttrEnum      = flagAttrEnum;
            _flagOperEnum      = flagOperEnum;
            _flagUbyteTxt      = flagUbyteTxt;
            _flagUintTxt       = flagUintTxt;
            _flagValIsLen      = flagValIsLen;
            _flagValIsPCL      = flagValIsPCL;
            _actionType        = actionType;
            _makeOvlAct        = makeOvlAct;
            _description       = description;

            _statsCtParent = 0;
            _statsCtChild  = 0;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t D e t a i l s                                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void getDetails (
            ref Boolean flagReserved,
            ref Boolean flagAttrEnum,
            ref Boolean flagOperEnum,
            ref Boolean flagUbyteTxt,
            ref Boolean flagUintTxt,
            ref Boolean flagValIsLen,
            ref Boolean flagValIsPCL,
            ref PrnParseConstants.eActPCLXL actionType,
            ref PrnParseConstants.eOvlAct makeOvlAct,
            ref String  description)
        {
            flagReserved      = _flagReserved;
            flagAttrEnum      = _flagAttrEnum;
            flagOperEnum      = _flagOperEnum;
            flagUbyteTxt      = _flagUbyteTxt;
            flagUintTxt       = _flagUintTxt;
            flagValIsLen      = _flagValIsLen;
            flagValIsPCL      = _flagValIsPCL;
            makeOvlAct = _makeOvlAct; 
            description       = _description;
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
        // F l a g A t t r E n u m                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagAttrEnum
        {
            get { return _flagAttrEnum; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g O p e r E n u m                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagOperEnum
        {
            get { return _flagOperEnum; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g R e s e r v e d                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagReserved
        {
            get { return _flagReserved; }
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
            _statsCtChild = 0;
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
        // T a g                                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String Tag
        {
            get
            {
                if (_tagLen == 1)
                    return "0x" + _tagA.ToString("x2");
                else
                    return "0x" + _tagA.ToString("x2") + _tagB.ToString("x2");
            }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // T y p e                                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String Type
        {
            get { return "Attribute"; }
        }
    }
}