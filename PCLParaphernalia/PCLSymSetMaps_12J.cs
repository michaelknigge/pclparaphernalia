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
        // u n i c o d e M a p _ 1 2 J                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       12J                                                       //
        // Kind1    394                                                       //
        // Name     MC Text                                                   //
        //          Macintosh                                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_12J ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_12J;

            const Int32 rangeCt = 3;

            UInt16 [] [] rangeData = new UInt16 [rangeCt] []
            {
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

            rangeMin = rangeData [0] [0];
            rangeMax = rangeData [0] [1];
            rangeSize = rangeSizes [0];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataPCL [0] [i - rangeMin] = i;
            }

            mapDataPCL [0] [0x7f - rangeMin] = 0xffff;    //<not a character> //

            //----------------------------------------------------------------//
            //                                                                //
            // Range 1                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            rangeMin = rangeData [1] [0];
            rangeMax = rangeData [1] [1];
            rangeSize = rangeSizes [1];

            mapDataPCL [1] [0x80 - rangeMin] = 0x00c4;
            mapDataPCL [1] [0x81 - rangeMin] = 0x00c5;
            mapDataPCL [1] [0x82 - rangeMin] = 0x00c7;
            mapDataPCL [1] [0x83 - rangeMin] = 0x00c9;
            mapDataPCL [1] [0x84 - rangeMin] = 0x00d1;
            mapDataPCL [1] [0x85 - rangeMin] = 0x00d6;
            mapDataPCL [1] [0x86 - rangeMin] = 0x00dc;
            mapDataPCL [1] [0x87 - rangeMin] = 0x00e1;
            mapDataPCL [1] [0x88 - rangeMin] = 0x00e0;
            mapDataPCL [1] [0x89 - rangeMin] = 0x00e2;
            mapDataPCL [1] [0x8a - rangeMin] = 0x00e4;
            mapDataPCL [1] [0x8b - rangeMin] = 0x00e3;
            mapDataPCL [1] [0x8c - rangeMin] = 0x00e5;
            mapDataPCL [1] [0x8d - rangeMin] = 0x00e7;
            mapDataPCL [1] [0x8e - rangeMin] = 0x00e9;
            mapDataPCL [1] [0x8f - rangeMin] = 0x00e8;

            mapDataPCL [1] [0x90 - rangeMin] = 0x00ea;
            mapDataPCL [1] [0x91 - rangeMin] = 0x00eb;
            mapDataPCL [1] [0x92 - rangeMin] = 0x00ed;
            mapDataPCL [1] [0x93 - rangeMin] = 0x00ec;
            mapDataPCL [1] [0x94 - rangeMin] = 0x00ee;
            mapDataPCL [1] [0x95 - rangeMin] = 0x00ef;
            mapDataPCL [1] [0x96 - rangeMin] = 0x00f1;
            mapDataPCL [1] [0x97 - rangeMin] = 0x00f3;
            mapDataPCL [1] [0x98 - rangeMin] = 0x00f2;
            mapDataPCL [1] [0x99 - rangeMin] = 0x00f4;
            mapDataPCL [1] [0x9a - rangeMin] = 0x00f6;
            mapDataPCL [1] [0x9b - rangeMin] = 0x00f5;
            mapDataPCL [1] [0x9c - rangeMin] = 0x00fa;
            mapDataPCL [1] [0x9d - rangeMin] = 0x00f9;
            mapDataPCL [1] [0x9e - rangeMin] = 0x00fb;
            mapDataPCL [1] [0x9f - rangeMin] = 0x00fc;

            //----------------------------------------------------------------//
            //                                                                //
            // Range 2                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            rangeMin = rangeData [2] [0];
            rangeMax = rangeData [2] [1];
            rangeSize = rangeSizes [2];

            mapDataPCL[2][0xa0 - rangeMin] = 0x2020;
            mapDataPCL[2][0xa1 - rangeMin] = 0x00b0;
            mapDataPCL[2][0xa2 - rangeMin] = 0x00a2;
            mapDataPCL[2][0xa3 - rangeMin] = 0x00a3;
            mapDataPCL[2][0xa4 - rangeMin] = 0x00a7;
            mapDataPCL[2][0xa5 - rangeMin] = 0x2022;
            mapDataPCL[2][0xa6 - rangeMin] = 0x00b6;
            mapDataPCL[2][0xa7 - rangeMin] = 0x00df;
            mapDataPCL[2][0xa8 - rangeMin] = 0x00ae;
            mapDataPCL[2][0xa9 - rangeMin] = 0x00a9;
            mapDataPCL[2][0xaa - rangeMin] = 0x2122;
            mapDataPCL[2][0xab - rangeMin] = 0x00b4;
            mapDataPCL[2][0xac - rangeMin] = 0x00a8;
            mapDataPCL[2][0xad - rangeMin] = 0x2260;
            mapDataPCL[2][0xae - rangeMin] = 0x00c6;
            mapDataPCL[2][0xaf - rangeMin] = 0x00d8;

            mapDataPCL[2][0xb0 - rangeMin] = 0x221e;
            mapDataPCL[2][0xb1 - rangeMin] = 0x00b1;
            mapDataPCL[2][0xb2 - rangeMin] = 0x2264;
            mapDataPCL[2][0xb3 - rangeMin] = 0x2265;
            mapDataPCL[2][0xb4 - rangeMin] = 0x00a5;
            mapDataPCL[2][0xb5 - rangeMin] = 0x00b5;
            mapDataPCL[2][0xb6 - rangeMin] = 0x2202;
            mapDataPCL[2][0xb7 - rangeMin] = 0x2211;
            mapDataPCL[2][0xb8 - rangeMin] = 0x220f;
            mapDataPCL[2][0xb9 - rangeMin] = 0x03c0;
            mapDataPCL[2][0xba - rangeMin] = 0x222b;
            mapDataPCL[2][0xbb - rangeMin] = 0x00aa;
            mapDataPCL[2][0xbc - rangeMin] = 0x00ba;
            mapDataPCL[2][0xbd - rangeMin] = 0x2126;
            mapDataPCL[2][0xbe - rangeMin] = 0x00e6;
            mapDataPCL[2][0xbf - rangeMin] = 0x00f8;

            mapDataPCL[2][0xc0 - rangeMin] = 0x00bf;
            mapDataPCL[2][0xc1 - rangeMin] = 0x00a1;
            mapDataPCL[2][0xc2 - rangeMin] = 0x00ac;
            mapDataPCL[2][0xc3 - rangeMin] = 0x221a;
            mapDataPCL[2][0xc4 - rangeMin] = 0x0192;
            mapDataPCL[2][0xc5 - rangeMin] = 0x2248;
            mapDataPCL[2][0xc6 - rangeMin] = 0x2206;
            mapDataPCL[2][0xc7 - rangeMin] = 0x00ab;
            mapDataPCL[2][0xc8 - rangeMin] = 0x00bb;
            mapDataPCL[2][0xc9 - rangeMin] = 0x2026;
            mapDataPCL[2][0xca - rangeMin] = 0x00a0;
            mapDataPCL[2][0xcb - rangeMin] = 0x00c0;
            mapDataPCL[2][0xcc - rangeMin] = 0x00c3;
            mapDataPCL[2][0xcd - rangeMin] = 0x00d5;
            mapDataPCL[2][0xce - rangeMin] = 0x0152;
            mapDataPCL[2][0xcf - rangeMin] = 0x0153;

            mapDataPCL[2][0xd0 - rangeMin] = 0x2013;
            mapDataPCL[2][0xd1 - rangeMin] = 0x2014;
            mapDataPCL[2][0xd2 - rangeMin] = 0x201c;
            mapDataPCL[2][0xd3 - rangeMin] = 0x201d;
            mapDataPCL[2][0xd4 - rangeMin] = 0x2018;
            mapDataPCL[2][0xd5 - rangeMin] = 0x2019;
            mapDataPCL[2][0xd6 - rangeMin] = 0x00f7;
            mapDataPCL[2][0xd7 - rangeMin] = 0x25ca;
            mapDataPCL[2][0xd8 - rangeMin] = 0x00ff;
            mapDataPCL[2][0xd9 - rangeMin] = 0x0178;
            mapDataPCL[2][0xda - rangeMin] = 0x2215;
            mapDataPCL[2][0xdb - rangeMin] = 0x20ac;
            mapDataPCL[2][0xdc - rangeMin] = 0x2039;
            mapDataPCL[2][0xdd - rangeMin] = 0x203a;
            mapDataPCL[2][0xde - rangeMin] = 0xf001;
            mapDataPCL[2][0xdf - rangeMin] = 0xf002;

            mapDataPCL[2][0xe0 - rangeMin] = 0x2021;
            mapDataPCL[2][0xe1 - rangeMin] = 0x2219;
            mapDataPCL[2][0xe2 - rangeMin] = 0x201a;
            mapDataPCL[2][0xe3 - rangeMin] = 0x201e;
            mapDataPCL[2][0xe4 - rangeMin] = 0x2030;
            mapDataPCL[2][0xe5 - rangeMin] = 0x00c2;
            mapDataPCL[2][0xe6 - rangeMin] = 0x00ca;
            mapDataPCL[2][0xe7 - rangeMin] = 0x00c1;
            mapDataPCL[2][0xe8 - rangeMin] = 0x00cb;
            mapDataPCL[2][0xe9 - rangeMin] = 0x00c8;
            mapDataPCL[2][0xea - rangeMin] = 0x00cd;
            mapDataPCL[2][0xeb - rangeMin] = 0x00ce;
            mapDataPCL[2][0xec - rangeMin] = 0x00cf;
            mapDataPCL[2][0xed - rangeMin] = 0x00cc;
            mapDataPCL[2][0xee - rangeMin] = 0x00d3;
            mapDataPCL[2][0xef - rangeMin] = 0x00d4;

            mapDataPCL[2][0xf0 - rangeMin] = 0xffff;    //<not a character> //
            mapDataPCL[2][0xf1 - rangeMin] = 0x00d2;
            mapDataPCL[2][0xf2 - rangeMin] = 0x00da;
            mapDataPCL[2][0xf3 - rangeMin] = 0x00db;
            mapDataPCL[2][0xf4 - rangeMin] = 0x00d9;
            mapDataPCL[2][0xf5 - rangeMin] = 0x0131;
            mapDataPCL[2][0xf6 - rangeMin] = 0x02c6;
            mapDataPCL[2][0xf7 - rangeMin] = 0x02dc;
            mapDataPCL[2][0xf8 - rangeMin] = 0x02c9;
            mapDataPCL[2][0xf9 - rangeMin] = 0x02d8;
            mapDataPCL[2][0xfa - rangeMin] = 0x02d9;
            mapDataPCL[2][0xfb - rangeMin] = 0x02da;
            mapDataPCL[2][0xfc - rangeMin] = 0x00b8;
            mapDataPCL[2][0xfd - rangeMin] = 0x02dd;
            mapDataPCL[2][0xfe - rangeMin] = 0x02db;
            mapDataPCL[2][0xff - rangeMin] = 0x02c7;

            //----------------------------------------------------------------//

            _sets.Add (new PCLSymSetMap (mapId,
                                         rangeCt,
                                         rangeData,
                                         null,
                                         mapDataPCL));
        }
    }
}