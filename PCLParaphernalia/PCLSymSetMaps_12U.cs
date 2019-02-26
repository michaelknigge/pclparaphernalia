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
        // u n i c o d e M a p _ 1 2 U                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       12U                                                       //
        // Kind1    405                                                       //
        // Name     PC-850 Multilingual                                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_12U ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_12U;

            const Int32 rangeCt = 3;

            UInt16 [] [] rangeData = new UInt16 [rangeCt] []
            {
                new UInt16 [2] {0x01, 0x1f},
                new UInt16 [2] {0x20, 0x7f},
                new UInt16 [2] {0x80, 0xff}
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

            mapDataStd [0] [0x01 - rangeMin] = 0x263a;
            mapDataStd [0] [0x02 - rangeMin] = 0x263b;
            mapDataStd [0] [0x03 - rangeMin] = 0x2665;
            mapDataStd [0] [0x04 - rangeMin] = 0x2666;
            mapDataStd [0] [0x05 - rangeMin] = 0x2663;
            mapDataStd [0] [0x06 - rangeMin] = 0x2660;
            mapDataStd [0] [0x07 - rangeMin] = 0x2022;
            mapDataStd [0] [0x08 - rangeMin] = 0x25d8;
            mapDataStd [0] [0x09 - rangeMin] = 0x25cb;
            mapDataStd [0] [0x0a - rangeMin] = 0x25d9;
            mapDataStd [0] [0x0b - rangeMin] = 0x2642;
            mapDataStd [0] [0x0c - rangeMin] = 0x2640;
            mapDataStd [0] [0x0d - rangeMin] = 0x266a;
            mapDataStd [0] [0x0e - rangeMin] = 0x266b;
            mapDataStd [0] [0x0f - rangeMin] = 0x263c;

            mapDataStd [0] [0x10 - rangeMin] = 0x25ba;
            mapDataStd [0] [0x11 - rangeMin] = 0x25c4;
            mapDataStd [0] [0x12 - rangeMin] = 0x2195;
            mapDataStd [0] [0x13 - rangeMin] = 0x203c;
            mapDataStd [0] [0x14 - rangeMin] = 0x00b6;
            mapDataStd [0] [0x15 - rangeMin] = 0x00a7;
            mapDataStd [0] [0x16 - rangeMin] = 0x25ac;
            mapDataStd [0] [0x17 - rangeMin] = 0x21a8;
            mapDataStd [0] [0x18 - rangeMin] = 0x2191;
            mapDataStd [0] [0x19 - rangeMin] = 0x2193;
            mapDataStd [0] [0x1a - rangeMin] = 0x2192;
            mapDataStd [0] [0x1b - rangeMin] = 0x2190;
            mapDataStd [0] [0x1c - rangeMin] = 0x221f;
            mapDataStd [0] [0x1d - rangeMin] = 0x2194;
            mapDataStd [0] [0x1e - rangeMin] = 0x25b2;
            mapDataStd [0] [0x1f - rangeMin] = 0x25bc;

            //----------------------------------------------------------------//

            for (UInt16 i = 0; i < rangeSize; i++)
            {
                mapDataPCL [0] [i] = mapDataStd [0] [i];
            }

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
                mapDataStd [1] [i - rangeMin] = i;
            }

            mapDataStd [1] [0x7f - rangeMin] = 0xffff;    //<not a character> //

            //----------------------------------------------------------------//

            for (UInt16 i = 0; i < rangeSize; i++)
            {
                mapDataPCL [1] [i] = mapDataStd [1] [i];
            }

            mapDataPCL [1] [0x5e - rangeMin] = 0x02c6;
            mapDataPCL [1] [0x7e - rangeMin] = 0x02dc;
            mapDataPCL [1] [0x7f - rangeMin] = 0x2302;

            //----------------------------------------------------------------//
            //                                                                //
            // Range 2                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            rangeMin = rangeData [2] [0];
            rangeMax = rangeData [2] [1];
            rangeSize = rangeSizes [2];

            mapDataStd [2] [0x80 - rangeMin] = 0x00c7;
            mapDataStd [2] [0x81 - rangeMin] = 0x00fc;
            mapDataStd [2] [0x82 - rangeMin] = 0x00e9;
            mapDataStd [2] [0x83 - rangeMin] = 0x00e2;
            mapDataStd [2] [0x84 - rangeMin] = 0x00e4;
            mapDataStd [2] [0x85 - rangeMin] = 0x00e0;
            mapDataStd [2] [0x86 - rangeMin] = 0x00e5;
            mapDataStd [2] [0x87 - rangeMin] = 0x00e7;
            mapDataStd [2] [0x88 - rangeMin] = 0x00ea;
            mapDataStd [2] [0x89 - rangeMin] = 0x00eb;
            mapDataStd [2] [0x8a - rangeMin] = 0x00e8;
            mapDataStd [2] [0x8b - rangeMin] = 0x00ef;
            mapDataStd [2] [0x8c - rangeMin] = 0x00ee;
            mapDataStd [2] [0x8d - rangeMin] = 0x00ec;
            mapDataStd [2] [0x8e - rangeMin] = 0x00c4;
            mapDataStd [2] [0x8f - rangeMin] = 0x00c5;

            mapDataStd [2] [0x90 - rangeMin] = 0x00c9;
            mapDataStd [2] [0x91 - rangeMin] = 0x00e6;
            mapDataStd [2] [0x92 - rangeMin] = 0x00c6;
            mapDataStd [2] [0x93 - rangeMin] = 0x00f4;
            mapDataStd [2] [0x94 - rangeMin] = 0x00f6;
            mapDataStd [2] [0x95 - rangeMin] = 0x00f2;
            mapDataStd [2] [0x96 - rangeMin] = 0x00fb;
            mapDataStd [2] [0x97 - rangeMin] = 0x00f9;
            mapDataStd [2] [0x98 - rangeMin] = 0x00ff;
            mapDataStd [2] [0x99 - rangeMin] = 0x00d6;
            mapDataStd [2] [0x9a - rangeMin] = 0x00dc;
            mapDataStd [2] [0x9b - rangeMin] = 0x00f8;
            mapDataStd [2] [0x9c - rangeMin] = 0x00a3;
            mapDataStd [2] [0x9d - rangeMin] = 0x00d8;
            mapDataStd [2] [0x9e - rangeMin] = 0x00d7;
            mapDataStd [2] [0x9f - rangeMin] = 0x0192;

            mapDataStd [2] [0xa0 - rangeMin] = 0x00e1;
            mapDataStd [2] [0xa1 - rangeMin] = 0x00ed;
            mapDataStd [2] [0xa2 - rangeMin] = 0x00f3;
            mapDataStd [2] [0xa3 - rangeMin] = 0x00fa;
            mapDataStd [2] [0xa4 - rangeMin] = 0x00f1;
            mapDataStd [2] [0xa5 - rangeMin] = 0x00d1;
            mapDataStd [2] [0xa6 - rangeMin] = 0x00aa;
            mapDataStd [2] [0xa7 - rangeMin] = 0x00ba;
            mapDataStd [2] [0xa8 - rangeMin] = 0x00bf;
            mapDataStd [2] [0xa9 - rangeMin] = 0x00ae;
            mapDataStd [2] [0xaa - rangeMin] = 0x00ac;
            mapDataStd [2] [0xab - rangeMin] = 0x00bd;
            mapDataStd [2] [0xac - rangeMin] = 0x00bc;
            mapDataStd [2] [0xad - rangeMin] = 0x00a1;
            mapDataStd [2] [0xae - rangeMin] = 0x00ab;
            mapDataStd [2] [0xaf - rangeMin] = 0x00bb;

            mapDataStd [2] [0xb0 - rangeMin] = 0x2591;
            mapDataStd [2] [0xb1 - rangeMin] = 0x2592;
            mapDataStd [2] [0xb2 - rangeMin] = 0x2593;
            mapDataStd [2] [0xb3 - rangeMin] = 0x2502;
            mapDataStd [2] [0xb4 - rangeMin] = 0x2524;
            mapDataStd [2] [0xb5 - rangeMin] = 0x00c1;
            mapDataStd [2] [0xb6 - rangeMin] = 0x00c2;
            mapDataStd [2] [0xb7 - rangeMin] = 0x00c0;
            mapDataStd [2] [0xb8 - rangeMin] = 0x00a9;
            mapDataStd [2] [0xb9 - rangeMin] = 0x2563;
            mapDataStd [2] [0xba - rangeMin] = 0x2551;
            mapDataStd [2] [0xbb - rangeMin] = 0x2557;
            mapDataStd [2] [0xbc - rangeMin] = 0x255d;
            mapDataStd [2] [0xbd - rangeMin] = 0x00a2;
            mapDataStd [2] [0xbe - rangeMin] = 0x00a5;
            mapDataStd [2] [0xbf - rangeMin] = 0x2510;

            mapDataStd [2] [0xc0 - rangeMin] = 0x2514;
            mapDataStd [2] [0xc1 - rangeMin] = 0x2534;
            mapDataStd [2] [0xc2 - rangeMin] = 0x252c;
            mapDataStd [2] [0xc3 - rangeMin] = 0x251c;
            mapDataStd [2] [0xc4 - rangeMin] = 0x2500;
            mapDataStd [2] [0xc5 - rangeMin] = 0x253c;
            mapDataStd [2] [0xc6 - rangeMin] = 0x00e3;
            mapDataStd [2] [0xc7 - rangeMin] = 0x00c3;
            mapDataStd [2] [0xc8 - rangeMin] = 0x255a;
            mapDataStd [2] [0xc9 - rangeMin] = 0x2554;
            mapDataStd [2] [0xca - rangeMin] = 0x2569;
            mapDataStd [2] [0xcb - rangeMin] = 0x2566;
            mapDataStd [2] [0xcc - rangeMin] = 0x2560;
            mapDataStd [2] [0xcd - rangeMin] = 0x2550;
            mapDataStd [2] [0xce - rangeMin] = 0x256c;
            mapDataStd [2] [0xcf - rangeMin] = 0x00a4;

            mapDataStd [2] [0xd0 - rangeMin] = 0x00f0;
            mapDataStd [2] [0xd1 - rangeMin] = 0x00d0;
            mapDataStd [2] [0xd2 - rangeMin] = 0x00ca;
            mapDataStd [2] [0xd3 - rangeMin] = 0x00cb;
            mapDataStd [2] [0xd4 - rangeMin] = 0x00c8;
            mapDataStd [2] [0xd5 - rangeMin] = 0x0131;
            mapDataStd [2] [0xd6 - rangeMin] = 0x00cd;
            mapDataStd [2] [0xd7 - rangeMin] = 0x00ce;
            mapDataStd [2] [0xd8 - rangeMin] = 0x00cf;
            mapDataStd [2] [0xd9 - rangeMin] = 0x2518;
            mapDataStd [2] [0xda - rangeMin] = 0x250c;
            mapDataStd [2] [0xdb - rangeMin] = 0x2588;
            mapDataStd [2] [0xdc - rangeMin] = 0x2584;
            mapDataStd [2] [0xdd - rangeMin] = 0x00a6;
            mapDataStd [2] [0xde - rangeMin] = 0x00cc;
            mapDataStd [2] [0xdf - rangeMin] = 0x2580;

            mapDataStd [2] [0xe0 - rangeMin] = 0x00d3;
            mapDataStd [2] [0xe1 - rangeMin] = 0x00df;
            mapDataStd [2] [0xe2 - rangeMin] = 0x00d4;
            mapDataStd [2] [0xe3 - rangeMin] = 0x00d2;
            mapDataStd [2] [0xe4 - rangeMin] = 0x00f5;
            mapDataStd [2] [0xe5 - rangeMin] = 0x00d5;
            mapDataStd [2] [0xe6 - rangeMin] = 0x00b5;
            mapDataStd [2] [0xe7 - rangeMin] = 0x00fe;
            mapDataStd [2] [0xe8 - rangeMin] = 0x00de;
            mapDataStd [2] [0xe9 - rangeMin] = 0x00da;
            mapDataStd [2] [0xea - rangeMin] = 0x00db;
            mapDataStd [2] [0xeb - rangeMin] = 0x00d9;
            mapDataStd [2] [0xec - rangeMin] = 0x00fd;
            mapDataStd [2] [0xed - rangeMin] = 0x00dd;
            mapDataStd [2] [0xee - rangeMin] = 0x00af;
            mapDataStd [2] [0xef - rangeMin] = 0x00b4;

            mapDataStd [2] [0xf0 - rangeMin] = 0x00ad;
            mapDataStd [2] [0xf1 - rangeMin] = 0x00b1;
            mapDataStd [2] [0xf2 - rangeMin] = 0x2017;
            mapDataStd [2] [0xf3 - rangeMin] = 0x00be;
            mapDataStd [2] [0xf4 - rangeMin] = 0x00b6;
            mapDataStd [2] [0xf5 - rangeMin] = 0x00a7;
            mapDataStd [2] [0xf6 - rangeMin] = 0x00f7;
            mapDataStd [2] [0xf7 - rangeMin] = 0x00b8;
            mapDataStd [2] [0xf8 - rangeMin] = 0x00b0;
            mapDataStd [2] [0xf9 - rangeMin] = 0x00a8;
            mapDataStd [2] [0xfa - rangeMin] = 0x00b7;
            mapDataStd [2] [0xfb - rangeMin] = 0x00b9;
            mapDataStd [2] [0xfc - rangeMin] = 0x00b3;
            mapDataStd [2] [0xfd - rangeMin] = 0x00b2;
            mapDataStd [2] [0xfe - rangeMin] = 0x25a0;
            mapDataStd [2] [0xff - rangeMin] = 0x00a0;

            //----------------------------------------------------------------//

            for (UInt16 i = 0; i < rangeSize; i++)
            {
                mapDataPCL [2] [i] = mapDataStd [2] [i];
            }

            mapDataPCL [2] [0xee - rangeMin] = 0x02c9;
            mapDataPCL [2] [0xfe - rangeMin] = 0x25aa;

            //----------------------------------------------------------------//

            _sets.Add (new PCLSymSetMap (mapId,
                                         rangeCt,
                                         rangeData,
                                         mapDataStd,
                                         mapDataPCL));
        }
    }
}