using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines a set of PCL Raster Resolution objects.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class PCLRasterResolutions
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static PCLRasterResolution[] _rasterResolutions =
        {
            new PCLRasterResolution(75),
            new PCLRasterResolution(100),
            new PCLRasterResolution(150),
            new PCLRasterResolution(200),
            new PCLRasterResolution(300),
            new PCLRasterResolution(600)
        };

        private static Int32 _rasterResolutionCount =
            _rasterResolutions.GetUpperBound(0) + 1;

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C o u n t                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return count of Raster Resolution definitions.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 getCount()
        {
            return _rasterResolutionCount;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t V a l u e                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return value associated with specified Raster Resolution index.    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt16 getValue(Int32 selection)
        {
            return _rasterResolutions[selection].getValue();
        }
    }
}