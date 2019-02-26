using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines an HP-GL/2 Command.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    [System.Reflection.ObfuscationAttribute(Feature = "properties renaming")]

    class HPGL2Command
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private String  _mnemonic;
        private String  _description;

        private Boolean _flagResetHPGL2;
        private Boolean _flagBinaryData;
        private Boolean _flagFlipTransp;
        private Boolean _flagSetLblTerm;
        private Boolean _flagUseLblTerm;
        private Boolean _flagUseStdTerm;
        private Boolean _flagQuotedData;
        private Boolean _flagSymbolMode;

        private Int32 _statsCtParent;
        private Int32 _statsCtChild;
        
        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // H P G L 2 C o m m a n d                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public HPGL2Command(String  mnemonic,
                            Boolean flagResetHPGL2,
                            Boolean flagBinaryData,
                            Boolean flagFlipTransp,
                            Boolean flagSetLblTerm,
                            Boolean flagUseLblTerm,
                            Boolean flagUseStdTerm,
                            Boolean flagQuotedData,
                            Boolean flagSymbolMode,
                            String  description)
        {
            _mnemonic       = mnemonic;
            _description    = description;
        
            _flagResetHPGL2 = flagResetHPGL2;
            _flagBinaryData = flagBinaryData;
            _flagFlipTransp = flagFlipTransp;
            _flagSetLblTerm = flagSetLblTerm;
            _flagUseLblTerm = flagUseLblTerm;
            _flagUseStdTerm = flagUseStdTerm;
            _flagQuotedData = flagQuotedData;
            _flagSymbolMode = flagSymbolMode;

            _statsCtParent = 0;
            _statsCtChild  = 0;
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
        // F l a g B i n a r y D a t a                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagBinaryData
        {
            get { return _flagBinaryData; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g F l i p T r a n s p                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagFlipTransp
        {
            get { return _flagFlipTransp; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g Q u o t e d D a t a                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagQuotedData
        {
            get { return _flagQuotedData; }
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
        // F l a g S e t L b l T e r m                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagSetLblTerm
        {
            get { return _flagSetLblTerm; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g S y m b o l M o d e                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagSymbolMode
        {
            get { return _flagSymbolMode; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g U s e L b l T e r m                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagUseLblTerm
        {
            get { return _flagUseLblTerm; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g U s e S t d T e r m                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagUseStdTerm
        {
            get { return _flagUseStdTerm; }
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
    }
}