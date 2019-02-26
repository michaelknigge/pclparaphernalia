using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines a HP-GL/2 control code character.
    /// 
    /// © Chris Hutchinson 2013
    /// 
    /// </summary>

    // [System.Reflection.ObfuscationAttribute(Feature = "properties renaming")]
    [System.Reflection.ObfuscationAttribute (
        Feature = "renaming",
        ApplyToMembers = true)]

    class HPGL2ControlCode
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Byte _tag;

        private String _description;
        private String _mnemonic;

        private Boolean _noOp;

        private Int32 _statsCtParent;
        private Int32 _statsCtChild;
      
        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // H P G L 2 C o n t r o l C o d e                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

	    public HPGL2ControlCode( Byte   tag,
                                 Boolean noOp, 
                                 String mnemonic,
                                 String description)
	    {
            _tag           = tag;
            _noOp          = noOp;
            _mnemonic      = mnemonic;
            _description   = description;

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
        // M n e m o n i c                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String Mnemonic
        {
            get { return _mnemonic; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // N o O p                                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean NoOp
        {
            get { return _noOp; }
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
        // S e q u e n c e                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String Sequence
        {
            get { return "0x" + _tag.ToString ("x2"); }
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
            get { return "Control Code"; }
        }
    }
}