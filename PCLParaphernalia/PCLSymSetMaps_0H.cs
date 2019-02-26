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
        // u n i c o d e M a p _ 0 H                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       0H                                                        //
        // Kind1    8                                                         //
        // Name     Hebrew-7                                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_0H ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_0H;

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

            mapDataPCL[0][0x27 - rangeMin] = 0x2019;

            mapDataPCL[0][0x60 - rangeMin] = 0x05d0;
            mapDataPCL[0][0x61 - rangeMin] = 0x05d1;
            mapDataPCL[0][0x62 - rangeMin] = 0x05d2;
            mapDataPCL[0][0x63 - rangeMin] = 0x05d3;
            mapDataPCL[0][0x64 - rangeMin] = 0x05d4;
            mapDataPCL[0][0x65 - rangeMin] = 0x05d5;
            mapDataPCL[0][0x66 - rangeMin] = 0x05d6;
            mapDataPCL[0][0x67 - rangeMin] = 0x05d7;
            mapDataPCL[0][0x68 - rangeMin] = 0x05d8;
            mapDataPCL[0][0x69 - rangeMin] = 0x05d9;
            mapDataPCL[0][0x6a - rangeMin] = 0x05da;
            mapDataPCL[0][0x6b - rangeMin] = 0x05db;
            mapDataPCL[0][0x6c - rangeMin] = 0x05dc;
            mapDataPCL[0][0x6d - rangeMin] = 0x05dd;
            mapDataPCL[0][0x6e - rangeMin] = 0x05de;
            mapDataPCL[0][0x6f - rangeMin] = 0x05df;

            mapDataPCL[0][0x70 - rangeMin] = 0x05e0;
            mapDataPCL[0][0x71 - rangeMin] = 0x05e1;
            mapDataPCL[0][0x72 - rangeMin] = 0x05e2;
            mapDataPCL[0][0x73 - rangeMin] = 0x05e3;
            mapDataPCL[0][0x74 - rangeMin] = 0x05e4;
            mapDataPCL[0][0x75 - rangeMin] = 0x05e5;
            mapDataPCL[0][0x76 - rangeMin] = 0x05e6;
            mapDataPCL[0][0x77 - rangeMin] = 0x05e7;
            mapDataPCL[0][0x78 - rangeMin] = 0x05e8;
            mapDataPCL[0][0x79 - rangeMin] = 0x05e9;
            mapDataPCL[0][0x7a - rangeMin] = 0x05ea;

            mapDataPCL[0] [0x7f - rangeMin] = 0x2592;

            //----------------------------------------------------------------//

            _sets.Add (new PCLSymSetMap (mapId,
                                         rangeCt,
                                         rangeData,
                                         null,
                                         mapDataPCL));
        }
    }
}