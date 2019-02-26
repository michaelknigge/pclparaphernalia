using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class handles a PCL Input Tray (Paper Source) object.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    class PCLTrayData
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Int16 _idAutoSelect;
        private Int16 _idDefault;
        private Int16 _idMaximum;
        private Int16 _idNotSet;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L T r a y D a t a                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PCLTrayData(Int16 idAutoSelect,
                           Int16 idDefault,
                           Int16 idMaximum,
                           Int16 idNotSet)
        {
            _idDefault    = idDefault;
            _idAutoSelect = idAutoSelect;
            _idMaximum    = idMaximum;
            _idNotSet     = idNotSet;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I d A u t o S e l e c t                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the auto-select tray identifier.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int16 getIdAutoSelect()
        {
            return _idAutoSelect;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I d D e f a u l t                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the default tray identifier.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int16 getIdDefault()
        {
            return _idDefault;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I d M a x i m u m                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the maximum tray identifier.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int16 getIdMaximum()
        {
            return _idMaximum;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I d N o t S e t                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the dummy 'not set' tray identifier.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int16 getIdNotSet()
        {
            return _idNotSet;
        }
    }
}