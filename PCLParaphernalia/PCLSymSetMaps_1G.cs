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
    /// © Chris Hutchinson 2016
    /// 
    /// </summary>

    static partial class PCLSymSetMaps
    {
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // u n i c o d e M a p _ 1 G                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       1G                                                        //
        // Kind1    39                                                        //
        // Name     ISO 21: German                                            //
        //          National Language Variant of (7-bit) US-ASCII set.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_1G ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_1G;

            const Int32 rangeCt = 1;

            UInt16 [] [] rangeData = new UInt16 [rangeCt] []
            {
                new UInt16 [2] {0x20, 0x7f}
            };

            UInt16 [] rangeSizes = new UInt16 [rangeCt];

            UInt16 [] [] mapDataStd = new UInt16 [rangeCt] [];
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
                mapDataStd [i] = new UInt16 [rangeSizes [i]];
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
                mapDataStd [0] [i - rangeMin] = i;
            }

            mapDataStd[0][0x23 - rangeMin] = 0x0023;
            mapDataStd[0][0x24 - rangeMin] = 0x0024;

            mapDataStd[0][0x40 - rangeMin] = 0x00a7;

            mapDataStd[0][0x5b - rangeMin] = 0x00c4;
            mapDataStd[0][0x5c - rangeMin] = 0x00d6;
            mapDataStd[0][0x5d - rangeMin] = 0x00dc;
            mapDataStd[0][0x5e - rangeMin] = 0x005e;

            mapDataStd[0][0x60 - rangeMin] = 0x0060;

            mapDataStd[0][0x7b - rangeMin] = 0x00e4;
            mapDataStd[0][0x7c - rangeMin] = 0x00f6;
            mapDataStd[0][0x7d - rangeMin] = 0x00fc;
            mapDataStd[0][0x7e - rangeMin] = 0x00df;

            mapDataStd[0] [0x7f - rangeMin] = 0xffff;    //<not a character> //

            //----------------------------------------------------------------//

            for (UInt16 i = 0; i < rangeSize; i++)
            {
                mapDataPCL [0] [i] = mapDataStd [0] [i];
            }

            mapDataPCL [0] [0x27 - rangeMin] = 0x2019;
            mapDataPCL [0] [0x5e - rangeMin] = 0x02c6;
            mapDataPCL [0] [0x7f - rangeMin] = 0x2592;

            //----------------------------------------------------------------//

            _sets.Add (new PCLSymSetMap (mapId,
                                         rangeCt,
                                         rangeData,
                                         mapDataStd,
                                         mapDataPCL));
        }
    }
}