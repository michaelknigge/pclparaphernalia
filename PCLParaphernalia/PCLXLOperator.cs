using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines a PCL XL Operator tag.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    // [System.Reflection.ObfuscationAttribute(Feature = "properties renaming")]
    [System.Reflection.ObfuscationAttribute (
        Feature = "renaming",
        ApplyToMembers = true)]

    class PCLXLOperator
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Byte _tag;

        private String  _description;

        private Boolean _flagReserved;
        private Boolean _flagEndSession;

        private PCLXLOperators.eEmbedDataType _embedDataType;

        private PrnParseConstants.eOvlAct _makeOvlAct;

        private Int32 _statsCtParent;
        private Int32 _statsCtChild;
      
        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L X L O p e r a t o r                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

	    public PCLXLOperator(Byte                      tag,
                             Boolean                   flagEndSession,
                             Boolean                   flagReserved,
                             PCLXLOperators.eEmbedDataType embedDataType,
                             PrnParseConstants.eOvlAct makeOvlAct,
                             String description)
	    {
            _tag               = tag;
            _flagEndSession    = flagEndSession;
            _flagReserved      = flagReserved;
	        _embedDataType     = embedDataType;
	        _description       = description;
            _makeOvlAct = makeOvlAct;

            _statsCtParent  = 0;
            _statsCtChild   = 0;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t D e t a i l s                                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void getDetails (
            ref Boolean flagEndSession,
            ref Boolean flagReserved,
            ref PCLXLOperators.eEmbedDataType embedDataType,
            ref PrnParseConstants.eOvlAct makeOvlAct,
            ref String description)
        {
            flagEndSession    = _flagEndSession;
            flagReserved      = _flagReserved;
            embedDataType     = _embedDataType;
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
        // E m b e d D a t a T y p e                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PCLXLOperators.eEmbedDataType EmbedDataType
        {
            get { return _embedDataType; }
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
        //                                                    P r o p e r t y //
        // F l a g E n d S e s s i o n                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagEndSession
        {
            get { return _flagEndSession; }
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
            get { return "0x" + _tag.ToString("x2"); }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // T y p e                                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String Type
        {
            get { return "Operator"; }
        }
    }
}