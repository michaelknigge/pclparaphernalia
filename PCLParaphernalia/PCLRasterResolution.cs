using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class handles a PCL Raster Resolution object.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    class PCLRasterResolution
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private UInt16 _resolution;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L R a s t e r R e s o l u t i o n                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PCLRasterResolution(UInt16 value)
        {
            _resolution = value;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t V a l u e                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the resolution value.                                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public UInt16 getValue()
        {
            return _resolution;
        }
    }
}