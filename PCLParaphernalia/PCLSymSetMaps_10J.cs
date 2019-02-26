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
        // u n i c o d e M a p _ 1 0 J                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       10J                                                       //
        // Kind1    330                                                       //
        // Name     PS Text                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_10J ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_10J;

            const Int32 rangeCt = 2;

            UInt16 [] [] rangeData = new UInt16 [rangeCt] []
            {
                new UInt16 [2] {0x20, 0x7f},
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

            rangeMin = rangeData [0] [0];
            rangeMax = rangeData [0] [1];
            rangeSize = rangeSizes [0];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataPCL [0] [i - rangeMin] = i;
            }

            mapDataPCL [0] [0x27 - rangeMin] = 0x2019;
            mapDataPCL [0] [0x60 - rangeMin] = 0x2018;
            mapDataPCL [0] [0x7f - rangeMin] = 0xffff;    //<not a character> //

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
                mapDataPCL [1] [i - rangeMin] = 0xffff;    //<not a character> //
            }

            mapDataPCL [1] [0xa0 - rangeMin] = 0x00a0;
            mapDataPCL [1] [0xa1 - rangeMin] = 0x00a1;
            mapDataPCL [1] [0xa2 - rangeMin] = 0x00a2;
            mapDataPCL [1] [0xa3 - rangeMin] = 0x00a3;
            mapDataPCL [1] [0xa4 - rangeMin] = 0x2215;
            mapDataPCL [1] [0xa5 - rangeMin] = 0x00a5;
            mapDataPCL [1] [0xa6 - rangeMin] = 0x0192;
            mapDataPCL [1] [0xa7 - rangeMin] = 0x00a7;
            mapDataPCL [1] [0xa8 - rangeMin] = 0x00a4;
            mapDataPCL [1] [0xa9 - rangeMin] = 0x0027;
            mapDataPCL [1] [0xaa - rangeMin] = 0x201c;
            mapDataPCL [1] [0xab - rangeMin] = 0x00ab;
            mapDataPCL [1] [0xac - rangeMin] = 0x2039;
            mapDataPCL [1] [0xad - rangeMin] = 0x203a;
            mapDataPCL [1] [0xae - rangeMin] = 0xf001;
            mapDataPCL [1] [0xaf - rangeMin] = 0xf002;

            mapDataPCL [1] [0xb1 - rangeMin] = 0x2013;
            mapDataPCL [1] [0xb2 - rangeMin] = 0x2020;
            mapDataPCL [1] [0xb3 - rangeMin] = 0x2021;
            mapDataPCL [1] [0xb4 - rangeMin] = 0x2219;
            mapDataPCL [1] [0xb6 - rangeMin] = 0x00b6;
            mapDataPCL [1] [0xb7 - rangeMin] = 0x2022;
            mapDataPCL [1] [0xb8 - rangeMin] = 0x201a;
            mapDataPCL [1] [0xb9 - rangeMin] = 0x201e;
            mapDataPCL [1] [0xba - rangeMin] = 0x201d;
            mapDataPCL [1] [0xbb - rangeMin] = 0x00bb;
            mapDataPCL [1] [0xbc - rangeMin] = 0x2026;
            mapDataPCL [1] [0xbd - rangeMin] = 0x2030;
            mapDataPCL [1] [0xbf - rangeMin] = 0x00bf;

            mapDataPCL [1] [0xc1 - rangeMin] = 0x0060;
            mapDataPCL [1] [0xc2 - rangeMin] = 0x00b4;
            mapDataPCL [1] [0xc3 - rangeMin] = 0x02c6;
            mapDataPCL [1] [0xc4 - rangeMin] = 0x02dc;
            mapDataPCL [1] [0xc5 - rangeMin] = 0x02c9;
            mapDataPCL [1] [0xc6 - rangeMin] = 0x02d8;
            mapDataPCL [1] [0xc7 - rangeMin] = 0x02d9;
            mapDataPCL [1] [0xc8 - rangeMin] = 0x00a8;
            mapDataPCL [1] [0xca - rangeMin] = 0x02da;
            mapDataPCL [1] [0xcb - rangeMin] = 0x00b8;
            mapDataPCL [1] [0xcd - rangeMin] = 0x02dd;
            mapDataPCL [1] [0xce - rangeMin] = 0x02db;
            mapDataPCL [1] [0xcf - rangeMin] = 0x02c7;

            mapDataPCL [1] [0xd0 - rangeMin] = 0x2014;

            mapDataPCL [1] [0xe1 - rangeMin] = 0x00c6;
            mapDataPCL [1] [0xe3 - rangeMin] = 0x00aa;
            mapDataPCL [1] [0xe8 - rangeMin] = 0x0141;
            mapDataPCL [1] [0xe9 - rangeMin] = 0x00d8;
            mapDataPCL [1] [0xea - rangeMin] = 0x0152;
            mapDataPCL [1] [0xeb - rangeMin] = 0x00ba;

            mapDataPCL [1] [0xf1 - rangeMin] = 0x00e6;
            mapDataPCL [1] [0xf5 - rangeMin] = 0x0131;
            mapDataPCL [1] [0xf8 - rangeMin] = 0x0142;
            mapDataPCL [1] [0xf9 - rangeMin] = 0x00f8;
            mapDataPCL [1] [0xfa - rangeMin] = 0x0153;
            mapDataPCL [1] [0xfb - rangeMin] = 0x00df;

            //----------------------------------------------------------------//

            _sets.Add (new PCLSymSetMap (mapId,
                                         rangeCt,
                                         rangeData,
                                         null,
                                         mapDataPCL));
        }
    }
}