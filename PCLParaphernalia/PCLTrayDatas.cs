using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines a set of PCL Input Tray (Paper Source) objects.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class PCLTrayDatas
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private enum ePDL
        {
            // must be in same order as _trayDatas array 
            PCL,
            PCLXL
        }

        private static PCLTrayData[] _trayDatas = 
        {
            // must be in same order as ePDL enumeration 
            new PCLTrayData(7, 0, 299, -1),
            new PCLTrayData(1, 0, 255, -1)
        };

        private static Int32 _trayDataCount = _trayDatas.GetUpperBound(0) + 1;

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I d A u t o S e l e c t P C L                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return PCL ID associated with auto-select tray.                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int16 getIdAutoSelectPCL()
        {
            return _trayDatas[(Int32)ePDL.PCL].getIdAutoSelect();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I d A u t o S e l e c t P C L X L                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return PCLXL ID associated with auto-select tray.                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int16 getIdAutoSelectPCLXL()
        {
            return _trayDatas[(Int32)ePDL.PCLXL].getIdAutoSelect();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I d D e f a u l t P C L                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return PCL ID associated with default tray.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int16 getIdDefaultPCL()
        {
            return _trayDatas[(Int32)ePDL.PCL].getIdDefault();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I d D e f a u l t P C L X L                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return PCLXL ID associated with default tray.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int16 getIdDefaultPCLXL()
        {
            return _trayDatas[(Int32)ePDL.PCLXL].getIdDefault();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I d M a x i m u m P C L                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return maximum PCL tray ID.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int16 getIdMaximumPCL()
        {
            return _trayDatas[(Int32)ePDL.PCL].getIdMaximum();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I d M a x i m u m P C L X L                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return maximum PCLXL tray ID.                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int16 getIdMaximumPCLXL()
        {
            return _trayDatas[(Int32)ePDL.PCLXL].getIdMaximum();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I d N o t S e t P C L                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return dummy 'not set' PCL tray ID.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int16 getIdNotSetPCL()
        {
            return _trayDatas[(Int32)ePDL.PCL].getIdNotSet();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I d N o t S e t P C L X L                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return dummy 'not set' PCLXL tray ID.                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int16 getIdNotSetPCLXL()
        {
            return _trayDatas[(Int32)ePDL.PCLXL].getIdNotSet();
        }
    }
}