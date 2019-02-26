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
        // u n i c o d e M a p _ 1 5 H                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       15H                                                       //
        // Kind1    488                                                       //
        // Name     PC-862 Latin/Hebrew                                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_15H ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_15H;

            const Int32 rangeCt = 4;

            UInt16 [] [] rangeData = new UInt16 [rangeCt] []
            {
                new UInt16 [2] {0x01, 0x1f},
                new UInt16 [2] {0x20, 0x7f},
                new UInt16 [2] {0x80, 0x9f},
                new UInt16 [2] {0xa0, 0xff}
            };

            UInt16 [] rangeSizes = new UInt16 [rangeCt];

            UInt16 [] [] mapDataStd = new UInt16 [rangeCt] [];
            UInt16 [] [] mapDataPCL = new UInt16 [rangeCt] [];

            UInt16 rangeMin,
                   rangeMax,
                   rangeSize,
                   offset;

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
                mapDataPCL[0][i] = mapDataStd[0][i];
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

            mapDataStd [1] [0x7f - rangeMin] = 0x2302;

            //----------------------------------------------------------------//

            for (UInt16 i = 0; i < rangeSize; i++)
            {
                mapDataPCL[1][i] = mapDataStd[1][i];
            }

            mapDataPCL[1][0x5e - rangeMin] = 0x02c6;
            mapDataPCL[1][0x7e - rangeMin] = 0x02dc;

            //----------------------------------------------------------------//
            //                                                                //
            // Range 2                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            rangeMin = rangeData[2][0];
            rangeMax = rangeData[2][1];
            rangeSize = rangeSizes[2];

            offset = 0x05d0 - 0x0080;     // 0x05d0 - 0x0080 = 0x0550

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd[2][i - rangeMin] = (UInt16)(offset + i);
            }

            mapDataStd[2][0x9b - rangeMin] = 0x00a2;
            mapDataStd[2][0x9c - rangeMin] = 0x00a3;
            mapDataStd[2][0x9d - rangeMin] = 0x00a5;
            mapDataStd[2][0x9e - rangeMin] = 0x20a7;
            mapDataStd[2][0x9f - rangeMin] = 0x0192;

            //----------------------------------------------------------------//

            for (UInt16 i = 0; i < rangeSize; i++)
            {
                mapDataPCL[2][i] = mapDataStd[2][i];
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Range 3                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            rangeMin = rangeData [3] [0];
            rangeMax = rangeData [3] [1];
            rangeSize = rangeSizes [3];

            mapDataStd [3] [0xa0 - rangeMin] = 0x00e1;
            mapDataStd [3] [0xa1 - rangeMin] = 0x00ed;
            mapDataStd [3] [0xa2 - rangeMin] = 0x00f3;
            mapDataStd [3] [0xa3 - rangeMin] = 0x00fa;
            mapDataStd [3] [0xa4 - rangeMin] = 0x00f1;
            mapDataStd [3] [0xa5 - rangeMin] = 0x00d1;
            mapDataStd [3] [0xa6 - rangeMin] = 0x00aa;
            mapDataStd [3] [0xa7 - rangeMin] = 0x00ba;
            mapDataStd [3] [0xa8 - rangeMin] = 0x00bf;
            mapDataStd [3] [0xa9 - rangeMin] = 0x2310;
            mapDataStd [3] [0xaa - rangeMin] = 0x00ac;
            mapDataStd [3] [0xab - rangeMin] = 0x00bd;
            mapDataStd [3] [0xac - rangeMin] = 0x00bc;
            mapDataStd [3] [0xad - rangeMin] = 0x00a1;
            mapDataStd [3] [0xae - rangeMin] = 0x00ab;
            mapDataStd [3] [0xaf - rangeMin] = 0x00bb;

            mapDataStd [3] [0xb0 - rangeMin] = 0x2591;
            mapDataStd [3] [0xb1 - rangeMin] = 0x2592;
            mapDataStd [3] [0xb2 - rangeMin] = 0x2593;
            mapDataStd [3] [0xb3 - rangeMin] = 0x2502;
            mapDataStd [3] [0xb4 - rangeMin] = 0x2524;
            mapDataStd [3] [0xb5 - rangeMin] = 0x2561;
            mapDataStd [3] [0xb6 - rangeMin] = 0x2562;
            mapDataStd [3] [0xb7 - rangeMin] = 0x2556;
            mapDataStd [3] [0xb8 - rangeMin] = 0x2555;
            mapDataStd [3] [0xb9 - rangeMin] = 0x2563;
            mapDataStd [3] [0xba - rangeMin] = 0x2551;
            mapDataStd [3] [0xbb - rangeMin] = 0x2557;
            mapDataStd [3] [0xbc - rangeMin] = 0x255d;
            mapDataStd [3] [0xbd - rangeMin] = 0x255c;
            mapDataStd [3] [0xbe - rangeMin] = 0x255b;
            mapDataStd [3] [0xbf - rangeMin] = 0x2510;

            mapDataStd [3] [0xc0 - rangeMin] = 0x2514;
            mapDataStd [3] [0xc1 - rangeMin] = 0x2534;
            mapDataStd [3] [0xc2 - rangeMin] = 0x252c;
            mapDataStd [3] [0xc3 - rangeMin] = 0x251c;
            mapDataStd [3] [0xc4 - rangeMin] = 0x2500;
            mapDataStd [3] [0xc5 - rangeMin] = 0x253c;
            mapDataStd [3] [0xc6 - rangeMin] = 0x255e;
            mapDataStd [3] [0xc7 - rangeMin] = 0x255f;
            mapDataStd [3] [0xc8 - rangeMin] = 0x255a;
            mapDataStd [3] [0xc9 - rangeMin] = 0x2554;
            mapDataStd [3] [0xca - rangeMin] = 0x2569;
            mapDataStd [3] [0xcb - rangeMin] = 0x2566;
            mapDataStd [3] [0xcc - rangeMin] = 0x2560;
            mapDataStd [3] [0xcd - rangeMin] = 0x2550;
            mapDataStd [3] [0xce - rangeMin] = 0x256c;
            mapDataStd [3] [0xcf - rangeMin] = 0x2567;

            mapDataStd [3] [0xd0 - rangeMin] = 0x2568;
            mapDataStd [3] [0xd1 - rangeMin] = 0x2564;
            mapDataStd [3] [0xd2 - rangeMin] = 0x2565;
            mapDataStd [3] [0xd3 - rangeMin] = 0x2559;
            mapDataStd [3] [0xd4 - rangeMin] = 0x2558;
            mapDataStd [3] [0xd5 - rangeMin] = 0x2552;
            mapDataStd [3] [0xd6 - rangeMin] = 0x2553;
            mapDataStd [3] [0xd7 - rangeMin] = 0x256b;
            mapDataStd [3] [0xd8 - rangeMin] = 0x256a;
            mapDataStd [3] [0xd9 - rangeMin] = 0x2518;
            mapDataStd [3] [0xda - rangeMin] = 0x250c;
            mapDataStd [3] [0xdb - rangeMin] = 0x2588;
            mapDataStd [3] [0xdc - rangeMin] = 0x2584;
            mapDataStd [3] [0xdd - rangeMin] = 0x258c;
            mapDataStd [3] [0xde - rangeMin] = 0x2590;
            mapDataStd [3] [0xdf - rangeMin] = 0x2580;

            mapDataStd [3] [0xe0 - rangeMin] = 0x03b1;
            mapDataStd [3] [0xe1 - rangeMin] = 0x00df;
            mapDataStd [3] [0xe2 - rangeMin] = 0x0393;
            mapDataStd [3] [0xe3 - rangeMin] = 0x03c0;
            mapDataStd [3] [0xe4 - rangeMin] = 0x03a3;
            mapDataStd [3] [0xe5 - rangeMin] = 0x03c3;
            mapDataStd [3] [0xe6 - rangeMin] = 0x00b5;
            mapDataStd [3] [0xe7 - rangeMin] = 0x03c4;
            mapDataStd [3] [0xe8 - rangeMin] = 0x03a6;
            mapDataStd [3] [0xe9 - rangeMin] = 0x0398;
            mapDataStd [3] [0xea - rangeMin] = 0x03a9;
            mapDataStd [3] [0xeb - rangeMin] = 0x03b4;
            mapDataStd [3] [0xec - rangeMin] = 0x221e;
            mapDataStd [3] [0xed - rangeMin] = 0x03c6;
            mapDataStd [3] [0xee - rangeMin] = 0x03b5;
            mapDataStd [3] [0xef - rangeMin] = 0x2229;

            mapDataStd [3] [0xf0 - rangeMin] = 0x2261;
            mapDataStd [3] [0xf1 - rangeMin] = 0x00b1;
            mapDataStd [3] [0xf2 - rangeMin] = 0x2265;
            mapDataStd [3] [0xf3 - rangeMin] = 0x2264;
            mapDataStd [3] [0xf4 - rangeMin] = 0x2320;
            mapDataStd [3] [0xf5 - rangeMin] = 0x2321;
            mapDataStd [3] [0xf6 - rangeMin] = 0x00f7;
            mapDataStd [3] [0xf7 - rangeMin] = 0x2248;
            mapDataStd [3] [0xf8 - rangeMin] = 0x00b0;
            mapDataStd [3] [0xf9 - rangeMin] = 0x2219;
            mapDataStd [3] [0xfa - rangeMin] = 0x00b7;
            mapDataStd [3] [0xfb - rangeMin] = 0x221a;
            mapDataStd [3] [0xfc - rangeMin] = 0x207f;
            mapDataStd [3] [0xfd - rangeMin] = 0x00b2;
            mapDataStd [3] [0xfe - rangeMin] = 0x25aa;
            mapDataStd [3] [0xff - rangeMin] = 0x00a0;

            //----------------------------------------------------------------//

            for (UInt16 i = 0; i < rangeSize; i++)
            {
                mapDataPCL[3][i] = mapDataStd[3][i];
            }

            mapDataPCL [3] [0xfa - rangeMin] = 0x2219;

            //----------------------------------------------------------------//

            _sets.Add (new PCLSymSetMap (mapId,
                                         rangeCt,
                                         rangeData,
                                         mapDataStd,
                                         mapDataPCL));
        }
    }
}