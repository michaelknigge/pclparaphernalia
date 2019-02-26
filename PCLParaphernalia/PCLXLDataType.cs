using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines a PCL XL Data Type tag.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    // [System.Reflection.ObfuscationAttribute(Feature = "properties renaming")]
    [System.Reflection.ObfuscationAttribute (
        Feature = "renaming",
        ApplyToMembers = true)]

    class PCLXLDataType
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Byte _tag;

        private String  _description;

        private Boolean _flagReserved;
        private Boolean _flagArray;

        private Int32 _groupSize;
        private Int32 _unitSize;

        private Int32 _statsCtParent;
        private Int32 _statsCtChild;

        private PCLXLDataTypes.eBaseType _baseType;
      
        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L X L D a t a T y p e                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

	    public PCLXLDataType(Byte                     tag,
                             Boolean                  flagReserved,
                             Boolean                  flagArray,
                             Int32                    groupSize,
                             Int32                    unitSize,  
                             PCLXLDataTypes.eBaseType baseType,
	                         String                   description)
	    {
            _tag           = tag;
            _flagReserved  = flagReserved;
            _flagArray     = flagArray;
            _groupSize     = groupSize;
            _unitSize      = unitSize;
            _baseType      = baseType;
	        _description   = description;

            _statsCtParent = 0;
            _statsCtChild = 0;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t D e t a i l s                                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void getDetails (ref Boolean flagReserved,
                                ref Boolean flagArray,
                                ref Int32 groupSize,
                                ref Int32 unitSize,
                                ref PCLXLDataTypes.eBaseType baseType,
                                ref String description)
        {
            flagReserved  = _flagReserved;
            flagArray     = _flagArray;
            groupSize     = _groupSize;
            unitSize      = _unitSize;
            baseType      = _baseType;
	        description   = _description;
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
            get { return "Data Type"; }
        }
    }
}