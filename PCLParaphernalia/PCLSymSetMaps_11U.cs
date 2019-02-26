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
        // u n i c o d e M a p _ 1 1 U                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       11U                                                       //
        // Kind1    373                                                       //
        // Name     PC-8 Danish/Norwegian                                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_11U()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_11U;

            const Int32 rangeCt = 3;

            UInt16 [] [] rangeData = new UInt16 [rangeCt] []
            {
                new UInt16 [2] {0x01, 0x1f},
                new UInt16 [2] {0x20, 0x7f},
                new UInt16 [2] {0x80, 0xff}
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

            mapDataPCL [0] [0x01 - rangeMin] = 0x263a;
            mapDataPCL [0] [0x02 - rangeMin] = 0x263b;
            mapDataPCL [0] [0x03 - rangeMin] = 0x2665;
            mapDataPCL [0] [0x04 - rangeMin] = 0x2666;
            mapDataPCL [0] [0x05 - rangeMin] = 0x2663;
            mapDataPCL [0] [0x06 - rangeMin] = 0x2660;
            mapDataPCL [0] [0x07 - rangeMin] = 0x2022;
            mapDataPCL [0] [0x08 - rangeMin] = 0x25d8;
            mapDataPCL [0] [0x09 - rangeMin] = 0x25cb;
            mapDataPCL [0] [0x0a - rangeMin] = 0x25d9;
            mapDataPCL [0] [0x0b - rangeMin] = 0x2642;
            mapDataPCL [0] [0x0c - rangeMin] = 0x2640;
            mapDataPCL [0] [0x0d - rangeMin] = 0x266a;
            mapDataPCL [0] [0x0e - rangeMin] = 0x266b;
            mapDataPCL [0] [0x0f - rangeMin] = 0x263c;

            mapDataPCL [0] [0x10 - rangeMin] = 0x25ba;
            mapDataPCL [0] [0x11 - rangeMin] = 0x25c4;
            mapDataPCL [0] [0x12 - rangeMin] = 0x2195;
            mapDataPCL [0] [0x13 - rangeMin] = 0x203c;
            mapDataPCL [0] [0x14 - rangeMin] = 0x00b6;
            mapDataPCL [0] [0x15 - rangeMin] = 0x00a7;
            mapDataPCL [0] [0x16 - rangeMin] = 0x25ac;
            mapDataPCL [0] [0x17 - rangeMin] = 0x21a8;
            mapDataPCL [0] [0x18 - rangeMin] = 0x2191;
            mapDataPCL [0] [0x19 - rangeMin] = 0x2193;
            mapDataPCL [0] [0x1a - rangeMin] = 0x2192;
            mapDataPCL [0] [0x1b - rangeMin] = 0x2190;
            mapDataPCL [0] [0x1c - rangeMin] = 0x221f;
            mapDataPCL [0] [0x1d - rangeMin] = 0x2194;
            mapDataPCL [0] [0x1e - rangeMin] = 0x25b2;
            mapDataPCL [0] [0x1f - rangeMin] = 0x25bc;

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

            mapDataPCL [1] [0x5e - rangeMin] = 0x02c6;
            mapDataPCL [1] [0x7e - rangeMin] = 0x02dc;
            mapDataPCL [1] [0x7f - rangeMin] = 0x2302;

            //----------------------------------------------------------------//
            //                                                                //
            // Range 2                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            rangeMin = rangeData[2][0];
            rangeMax = rangeData[2][1];
            rangeSize = rangeSizes[2];

            mapDataPCL[2][0x80 - rangeMin] = 0x00c7;
            mapDataPCL[2][0x81 - rangeMin] = 0x00fc;
            mapDataPCL[2][0x82 - rangeMin] = 0x00e9;
            mapDataPCL[2][0x83 - rangeMin] = 0x00e2;
            mapDataPCL[2][0x84 - rangeMin] = 0x00e4;
            mapDataPCL[2][0x85 - rangeMin] = 0x00e0;
            mapDataPCL[2][0x86 - rangeMin] = 0x00e5;
            mapDataPCL[2][0x87 - rangeMin] = 0x00e7;
            mapDataPCL[2][0x88 - rangeMin] = 0x00ea;
            mapDataPCL[2][0x89 - rangeMin] = 0x00eb;
            mapDataPCL[2][0x8a - rangeMin] = 0x00e8;
            mapDataPCL[2][0x8b - rangeMin] = 0x00ef;
            mapDataPCL[2][0x8c - rangeMin] = 0x00ee;
            mapDataPCL[2][0x8d - rangeMin] = 0x00ec;
            mapDataPCL[2][0x8e - rangeMin] = 0x00c4;
            mapDataPCL[2][0x8f - rangeMin] = 0x00c5;

            mapDataPCL[2][0x90 - rangeMin] = 0x00c9;
            mapDataPCL[2][0x91 - rangeMin] = 0x00e6;
            mapDataPCL[2][0x92 - rangeMin] = 0x00c6;
            mapDataPCL[2][0x93 - rangeMin] = 0x00f4;
            mapDataPCL[2][0x94 - rangeMin] = 0x00f6;
            mapDataPCL[2][0x95 - rangeMin] = 0x00f2;
            mapDataPCL[2][0x96 - rangeMin] = 0x00fb;
            mapDataPCL[2][0x97 - rangeMin] = 0x00f9;
            mapDataPCL[2][0x98 - rangeMin] = 0x00ff;
            mapDataPCL[2][0x99 - rangeMin] = 0x00d6;
            mapDataPCL[2][0x9a - rangeMin] = 0x00dc;
            mapDataPCL[2][0x9b - rangeMin] = 0x00f8;
            mapDataPCL[2][0x9c - rangeMin] = 0x00a3;
            mapDataPCL[2][0x9d - rangeMin] = 0x00d8;
            mapDataPCL[2][0x9e - rangeMin] = 0x013f;
            mapDataPCL[2][0x9f - rangeMin] = 0x0140;

            mapDataPCL[2][0xa0 - rangeMin] = 0x00e1;
            mapDataPCL[2][0xa1 - rangeMin] = 0x00ed;
            mapDataPCL[2][0xa2 - rangeMin] = 0x00f3;
            mapDataPCL[2][0xa3 - rangeMin] = 0x00fa;
            mapDataPCL[2][0xa4 - rangeMin] = 0x00f1;
            mapDataPCL[2][0xa5 - rangeMin] = 0x00d1;
            mapDataPCL[2][0xa6 - rangeMin] = 0x00f5;
            mapDataPCL[2][0xa7 - rangeMin] = 0x00d5;
            mapDataPCL[2][0xa8 - rangeMin] = 0x00bf;
            mapDataPCL[2][0xa9 - rangeMin] = 0x00e3;
            mapDataPCL[2][0xaa - rangeMin] = 0x00c3;
            mapDataPCL[2][0xab - rangeMin] = 0x2113;
            mapDataPCL[2][0xac - rangeMin] = 0x0149;
            mapDataPCL[2][0xad - rangeMin] = 0x00a1;
            mapDataPCL[2][0xae - rangeMin] = 0x00b3;
            mapDataPCL[2][0xaf - rangeMin] = 0x00a4;

            mapDataPCL[2][0xb0 - rangeMin] = 0x2591;
            mapDataPCL[2][0xb1 - rangeMin] = 0x2592;
            mapDataPCL[2][0xb2 - rangeMin] = 0x2593;
            mapDataPCL[2][0xb3 - rangeMin] = 0x2502;
            mapDataPCL[2][0xb4 - rangeMin] = 0x2524;
            mapDataPCL[2][0xb5 - rangeMin] = 0x2561;
            mapDataPCL[2][0xb6 - rangeMin] = 0x2562;
            mapDataPCL[2][0xb7 - rangeMin] = 0x2556;
            mapDataPCL[2][0xb8 - rangeMin] = 0x2555;
            mapDataPCL[2][0xb9 - rangeMin] = 0x2563;
            mapDataPCL[2][0xba - rangeMin] = 0x2551;
            mapDataPCL[2][0xbb - rangeMin] = 0x2557;
            mapDataPCL[2][0xbc - rangeMin] = 0x255d;
            mapDataPCL[2][0xbd - rangeMin] = 0x255c;
            mapDataPCL[2][0xbe - rangeMin] = 0x255b;
            mapDataPCL[2][0xbf - rangeMin] = 0x2510;

            mapDataPCL[2][0xc0 - rangeMin] = 0x2514;
            mapDataPCL[2][0xc1 - rangeMin] = 0x2534;
            mapDataPCL[2][0xc2 - rangeMin] = 0x252c;
            mapDataPCL[2][0xc3 - rangeMin] = 0x251c;
            mapDataPCL[2][0xc4 - rangeMin] = 0x2500;
            mapDataPCL[2][0xc5 - rangeMin] = 0x253c;
            mapDataPCL[2][0xc6 - rangeMin] = 0x255e;
            mapDataPCL[2][0xc7 - rangeMin] = 0x255f;
            mapDataPCL[2][0xc8 - rangeMin] = 0x255a;
            mapDataPCL[2][0xc9 - rangeMin] = 0x2554;
            mapDataPCL[2][0xca - rangeMin] = 0x2569;
            mapDataPCL[2][0xcb - rangeMin] = 0x2566;
            mapDataPCL[2][0xcc - rangeMin] = 0x2560;
            mapDataPCL[2][0xcd - rangeMin] = 0x2550;
            mapDataPCL[2][0xce - rangeMin] = 0x256c;
            mapDataPCL[2][0xcf - rangeMin] = 0x2567;

            mapDataPCL[2][0xd0 - rangeMin] = 0x2568;
            mapDataPCL[2][0xd1 - rangeMin] = 0x2564;
            mapDataPCL[2][0xd2 - rangeMin] = 0x2565;
            mapDataPCL[2][0xd3 - rangeMin] = 0x2559;
            mapDataPCL[2][0xd4 - rangeMin] = 0x2558;
            mapDataPCL[2][0xd5 - rangeMin] = 0x2552;
            mapDataPCL[2][0xd6 - rangeMin] = 0x2553;
            mapDataPCL[2][0xd7 - rangeMin] = 0x256b;
            mapDataPCL[2][0xd8 - rangeMin] = 0x256a;
            mapDataPCL[2][0xd9 - rangeMin] = 0x2518;
            mapDataPCL[2][0xda - rangeMin] = 0x250c;
            mapDataPCL[2][0xdb - rangeMin] = 0x2588;
            mapDataPCL[2][0xdc - rangeMin] = 0x2584;
            mapDataPCL[2][0xdd - rangeMin] = 0x258c;
            mapDataPCL[2][0xde - rangeMin] = 0x2590;
            mapDataPCL[2][0xdf - rangeMin] = 0x2580;

            mapDataPCL[2][0xe0 - rangeMin] = 0x03b1;
            mapDataPCL[2][0xe1 - rangeMin] = 0x00df;
            mapDataPCL[2][0xe2 - rangeMin] = 0x0490;
            mapDataPCL[2][0xe3 - rangeMin] = 0x03c0;
            mapDataPCL[2][0xe4 - rangeMin] = 0x03a3;
            mapDataPCL[2][0xe5 - rangeMin] = 0x03c3;
            mapDataPCL[2][0xe6 - rangeMin] = 0x03bc;
            mapDataPCL[2][0xe7 - rangeMin] = 0x03c4;
            mapDataPCL[2][0xe8 - rangeMin] = 0x03a6;
            mapDataPCL[2][0xe9 - rangeMin] = 0x0398;
            mapDataPCL[2][0xea - rangeMin] = 0x03a9;
            mapDataPCL[2][0xeb - rangeMin] = 0x03b4;
            mapDataPCL[2][0xec - rangeMin] = 0x221e;
            mapDataPCL[2][0xed - rangeMin] = 0x03c6;
            mapDataPCL[2][0xee - rangeMin] = 0x03b5;
            mapDataPCL[2][0xef - rangeMin] = 0x2229;

            mapDataPCL[2][0xf0 - rangeMin] = 0x2261;
            mapDataPCL[2][0xf1 - rangeMin] = 0x00b1;
            mapDataPCL[2][0xf2 - rangeMin] = 0x2265;
            mapDataPCL[2][0xf3 - rangeMin] = 0x2264;
            mapDataPCL[2][0xf4 - rangeMin] = 0x2320;
            mapDataPCL[2][0xf5 - rangeMin] = 0x2321;
            mapDataPCL[2][0xf6 - rangeMin] = 0x00f7;
            mapDataPCL[2][0xf7 - rangeMin] = 0x2248;
            mapDataPCL[2][0xf8 - rangeMin] = 0x00b0;
            mapDataPCL[2][0xf9 - rangeMin] = 0x2219;
            mapDataPCL[2][0xfa - rangeMin] = 0x00b7;
            mapDataPCL[2][0xfb - rangeMin] = 0x221a;
            mapDataPCL[2][0xfc - rangeMin] = 0x207f;
            mapDataPCL[2][0xfd - rangeMin] = 0x00b2;
            mapDataPCL[2][0xfe - rangeMin] = 0x25aa;
            mapDataPCL[2][0xff - rangeMin] = 0xffff;    //<not a character> //

            //----------------------------------------------------------------//

            _sets.Add (new PCLSymSetMap (mapId,
                                         rangeCt,
                                         rangeData,
                                         null,
                                         mapDataPCL));
        }
    }
}