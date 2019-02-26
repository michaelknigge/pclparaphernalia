using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class handles Kyocera Prescribe command object.
    /// 
    /// © Chris Hutchinson 2017
    /// 
    /// </summary>

    [System.Reflection.ObfuscationAttribute (Feature = "properties renaming")]

    class PrescribeCommand
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private String _cmdName;
        private String _cmdDesc;
        private Boolean _flagCmdIntro;
        private Boolean _flagCmdExit;
        private Boolean _flagCmdSetCRC;

        private Int32 _statsCtParent;
        private Int32 _statsCtChild;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P r e s c r i b e C o m m a n d                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PrescribeCommand (String  name,
                                 String  desc,
                                 Boolean flagCmdIntro,
                                 Boolean flagCmdExit,
                                 Boolean flagCmdSetCRC)
        {
            _cmdName   = name;
            _cmdDesc   = desc;
            _flagCmdIntro  = flagCmdIntro;
            _flagCmdExit   = flagCmdExit;
            _flagCmdSetCRC = flagCmdSetCRC;

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
        // I s C m d E x i t                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Returns the 'is exit command' flag.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean IsCmdExit
        {
            get { return _flagCmdExit; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // I s C m d I n t r o                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Returns the 'is introduction command' flag.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean IsCmdIntro
        {
            get { return _flagCmdIntro; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // I s C m d S e t C R C                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Returns the 'is set CRC command' flag.                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean IsCmdSetCRC
        {
            get { return _flagCmdSetCRC; }
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
    }
}