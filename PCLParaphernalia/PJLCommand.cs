using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class handles a PJL 'status readback' Command object.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    [System.Reflection.ObfuscationAttribute (Feature = "properties renaming")]

    class PJLCommand
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private PJLCommands.eRequestType _reqType;
        private PJLCommands.eCmdFormat   _cmdFormat;
        private String _cmdName;
        private String _cmdDesc;

        private Int32 _statsCtParent;
        private Int32 _statsCtChild;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P J L C o m m a n d                                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PJLCommand(PJLCommands.eCmdIndex    indx,
                          PJLCommands.eCmdFormat   format,
                          PJLCommands.eRequestType type,
                          String                   desc)
        {
            if (indx == PJLCommands.eCmdIndex.Null)
                _cmdName = PJLCommands.nullCmdKey;
            else
                _cmdName   = indx.ToString ();

            _cmdDesc   = desc;
            _cmdFormat = format;
            _reqType   = type;

            _statsCtParent = 0;
            _statsCtChild = 0;
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // D e s c r i p t i o n                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String Description
        {
            get { return _cmdDesc; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F o r m a t                                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PJLCommands.eCmdFormat Format
        {
            get { return _cmdFormat; }
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
        // N a m e                                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String Name
        {
            get { return _cmdName; }
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
        // T y p e                                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PJLCommands.eRequestType Type
        {
            get { return _reqType; }
        }
    }
}