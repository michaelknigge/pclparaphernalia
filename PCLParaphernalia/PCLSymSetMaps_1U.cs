using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines a set of PCL Symbol Set map objects.
    /// 
    /// © Chris Hutchinson 2015
    /// 
    /// </summary>

    static partial class PCLSymSetMaps
    {
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // u n i c o d e M a p _ 1 U                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       1U                                                        //
        // Kind1    53                                                        //
        // Name     Legal                                                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_1U ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_1U;

            const Int32 rangeCt = 1;

            UInt16 [] [] rangeData = new UInt16 [rangeCt] []
            {
                new UInt16 [2] {0x20, 0x7f}
            };

            UInt16 [] rangeSizes = new UInt16 [rangeCt];

            UInt16 [] [] mapDataPCL = new UInt16 [rangeCt] [];

            UInt16 rangeMin,
                   rangeMax,
                   rangeSize;

            //----------------------------------------------------------------//

            for (Int32 i = 0; i < rangeCt; i++)
            {
                rangeSizes [i] = (UInt16) (rangeData [i] [1] -
                                           rangeData [i] [0] + 1);
            }

            for (Int32 i = 0; i < rangeCt; i++)
            {
                mapDataPCL [i] = new UInt16 [rangeSizes [i]];
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Range 0                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            rangeMin = rangeData [0] [0];
            rangeMax = rangeData [0] [1];
            rangeSize = rangeSizes [0];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataPCL [0] [i - rangeMin] = i;
            }

            mapDataPCL [0] [0x22 - rangeMin] = 0x2033;
            mapDataPCL [0] [0x27 - rangeMin] = 0x2032;

            mapDataPCL [0] [0x3c - rangeMin] = 0x2017;
            mapDataPCL [0] [0x3e - rangeMin] = 0x00a2;

            mapDataPCL [0] [0x5c - rangeMin] = 0x00ae;
            mapDataPCL [0] [0x5e - rangeMin] = 0x00a9;

            mapDataPCL [0] [0x60 - rangeMin] = 0x00b0;

            mapDataPCL [0] [0x7b - rangeMin] = 0x00a7;
            mapDataPCL [0] [0x7c - rangeMin] = 0x00b6;
            mapDataPCL [0] [0x7d - rangeMin] = 0x2020;
            mapDataPCL [0] [0x7e - rangeMin] = 0x2122;
            mapDataPCL [0] [0x7f - rangeMin] = 0x2592;

            //----------------------------------------------------------------//

            _sets.Add (new PCLSymSetMap (mapId,
                                         rangeCt,
                                         rangeData,
                                         null,
                                         mapDataPCL));
        }
    }
}