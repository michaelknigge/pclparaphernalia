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
        // u n i c o d e M a p _ 9 J                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       9J                                                        //
        // Kind1    298                                                       //
        // Name     PC-1004 Windows Extended                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_9J ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_9J;

            const Int32 rangeCt = 4;

            UInt16 [] [] rangeData = new UInt16 [rangeCt] []
            {
                new UInt16 [2] {0x00, 0x1f},
                new UInt16 [2] {0x20, 0x7f},
                new UInt16 [2] {0x80, 0x9f},
                new UInt16 [2] {0xa0, 0xff}
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

            rangeMin = rangeData[0][0];
            rangeMax = rangeData[0][1];
            rangeSize = rangeSizes[0];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataPCL[0] [i - rangeMin] = 0xffff;    //<not a character> //
            }

            mapDataPCL [0] [0x04 - rangeMin] = 0x02c9;
            mapDataPCL [0] [0x05 - rangeMin] = 0x02d8;
            mapDataPCL [0] [0x06 - rangeMin] = 0x02d9;
            mapDataPCL [0] [0x08 - rangeMin] = 0x02da;
            mapDataPCL [0] [0x0a - rangeMin] = 0x02dd;
            mapDataPCL [0] [0x0b - rangeMin] = 0x02db;
            mapDataPCL [0] [0x0c - rangeMin] = 0x02c7;

            //----------------------------------------------------------------//
            //                                                                //
            // Range 1                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            rangeMin = rangeData [1] [0];
            rangeMax = rangeData [1] [1];
            rangeSize = rangeSizes [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataPCL [1] [i - rangeMin] = i;
            }

            mapDataPCL [1] [0x7f - rangeMin] = 0x2302;

            //----------------------------------------------------------------//
            //                                                                //
            // Range 2                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            rangeMin = rangeData [2] [0];
            rangeMax = rangeData [2] [1];
            rangeSize = rangeSizes [2];

            mapDataPCL [2] [0x80 - rangeMin] = 0xffff;    //<not a character> //
            mapDataPCL [2] [0x81 - rangeMin] = 0xffff;    //<not a character> //
            mapDataPCL [2] [0x82 - rangeMin] = 0x201a;
            mapDataPCL [2] [0x83 - rangeMin] = 0xffff;    //<not a character> //
            mapDataPCL [2] [0x84 - rangeMin] = 0x201e;
            mapDataPCL [2] [0x85 - rangeMin] = 0x2026;
            mapDataPCL [2] [0x86 - rangeMin] = 0x2020;
            mapDataPCL [2] [0x87 - rangeMin] = 0x2021;
            mapDataPCL [2] [0x88 - rangeMin] = 0x02c6;
            mapDataPCL [2] [0x89 - rangeMin] = 0x2030;
            mapDataPCL [2] [0x8a - rangeMin] = 0x0160;
            mapDataPCL [2] [0x8b - rangeMin] = 0x2039;
            mapDataPCL [2] [0x8c - rangeMin] = 0x0152;
            mapDataPCL [2] [0x8d - rangeMin] = 0xffff;    //<not a character> //
            mapDataPCL [2] [0x8e - rangeMin] = 0xffff;    //<not a character> //
            mapDataPCL [2] [0x8f - rangeMin] = 0xffff;    //<not a character> //

            mapDataPCL [2] [0x90 - rangeMin] = 0xffff;    //<not a character> //
            mapDataPCL [2] [0x91 - rangeMin] = 0x2018;
            mapDataPCL [2] [0x92 - rangeMin] = 0x2019;
            mapDataPCL [2] [0x93 - rangeMin] = 0x201c;
            mapDataPCL [2] [0x94 - rangeMin] = 0x201d;
            mapDataPCL [2] [0x95 - rangeMin] = 0x2022;
            mapDataPCL [2] [0x96 - rangeMin] = 0x2013;
            mapDataPCL [2] [0x97 - rangeMin] = 0x2014;
            mapDataPCL [2] [0x98 - rangeMin] = 0x02dc;
            mapDataPCL [2] [0x99 - rangeMin] = 0x2122;
            mapDataPCL [2] [0x9a - rangeMin] = 0x0161;
            mapDataPCL [2] [0x9b - rangeMin] = 0x203a;
            mapDataPCL [2] [0x9c - rangeMin] = 0x0153;
            mapDataPCL [2] [0x9d - rangeMin] = 0xffff;    //<not a character> //
            mapDataPCL [2] [0x9e - rangeMin] = 0xffff;    //<not a character> //
            mapDataPCL [2] [0x9f - rangeMin] = 0x0178;

            //----------------------------------------------------------------//
            //                                                                //
            // Range 3                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            rangeMin = rangeData [3] [0];
            rangeMax = rangeData [3] [1];
            rangeSize = rangeSizes [3];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataPCL [3] [i - rangeMin] = i;
            }

            //----------------------------------------------------------------//

            _sets.Add (new PCLSymSetMap (mapId,
                                         rangeCt,
                                         rangeData,
                                         null,
                                         mapDataPCL));
        }
    }
}