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
        // u n i c o d e M a p _ 8 M                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       8M                                                        //
        // Kind1    269                                                       //
        // Name     Math-8                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_8M ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_8M;

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

            mapDataPCL[0][0x20 - rangeMin] = 0x0020;
            mapDataPCL[0][0x21 - rangeMin] = 0xefbf;
            mapDataPCL[0][0x22 - rangeMin] = 0x2033;
            mapDataPCL[0][0x23 - rangeMin] = 0x00b0;
            mapDataPCL[0][0x24 - rangeMin] = 0x221e;
            mapDataPCL[0][0x25 - rangeMin] = 0x00f7;
            mapDataPCL[0][0x26 - rangeMin] = 0x221d;
            mapDataPCL[0][0x27 - rangeMin] = 0x2032;
            mapDataPCL[0][0x28 - rangeMin] = 0x0028;
            mapDataPCL[0][0x29 - rangeMin] = 0x0029;
            mapDataPCL[0][0x2a - rangeMin] = 0x00d7;
            mapDataPCL[0][0x2b - rangeMin] = 0x002b;
            mapDataPCL[0][0x2c - rangeMin] = 0x002c;
            mapDataPCL[0][0x2d - rangeMin] = 0x2212;
            mapDataPCL[0][0x2e - rangeMin] = 0x002e;
            mapDataPCL[0][0x2f - rangeMin] = 0x002f;

            mapDataPCL[0][0x30 - rangeMin] = 0x0030;
            mapDataPCL[0][0x31 - rangeMin] = 0x0031;
            mapDataPCL[0][0x32 - rangeMin] = 0x0032;
            mapDataPCL[0][0x33 - rangeMin] = 0x0033;
            mapDataPCL[0][0x34 - rangeMin] = 0x0034;
            mapDataPCL[0][0x35 - rangeMin] = 0x0035;
            mapDataPCL[0][0x36 - rangeMin] = 0x0036;
            mapDataPCL[0][0x37 - rangeMin] = 0x0037;
            mapDataPCL[0][0x38 - rangeMin] = 0x0038;
            mapDataPCL[0][0x39 - rangeMin] = 0x0039;
            mapDataPCL[0][0x3a - rangeMin] = 0x212f;
            mapDataPCL[0][0x3b - rangeMin] = 0xefec;
            mapDataPCL[0][0x3c - rangeMin] = 0x003c;
            mapDataPCL[0][0x3d - rangeMin] = 0x003d;
            mapDataPCL[0][0x3e - rangeMin] = 0x003e;
            mapDataPCL[0][0x3f - rangeMin] = 0x2248;

            mapDataPCL[0][0x40 - rangeMin] = 0x2234;
            mapDataPCL[0][0x41 - rangeMin] = 0x0041;
            mapDataPCL[0][0x42 - rangeMin] = 0x0042;
            mapDataPCL[0][0x43 - rangeMin] = 0x0393;
            mapDataPCL[0][0x44 - rangeMin] = 0x2206;
            mapDataPCL[0][0x45 - rangeMin] = 0x0045;
            mapDataPCL[0][0x46 - rangeMin] = 0x005a;
            mapDataPCL[0][0x47 - rangeMin] = 0x0048;
            mapDataPCL[0][0x48 - rangeMin] = 0x0398;
            mapDataPCL[0][0x49 - rangeMin] = 0x0049;
            mapDataPCL[0][0x4a - rangeMin] = 0x0048;
            mapDataPCL[0][0x4b - rangeMin] = 0x039b;
            mapDataPCL[0][0x4c - rangeMin] = 0x004d;
            mapDataPCL[0][0x4d - rangeMin] = 0x004e;
            mapDataPCL[0][0x4e - rangeMin] = 0x039e;
            mapDataPCL[0][0x4f - rangeMin] = 0x004f;

            mapDataPCL[0][0x50 - rangeMin] = 0x03a0;
            mapDataPCL[0][0x51 - rangeMin] = 0x0050;
            mapDataPCL[0][0x52 - rangeMin] = 0x03a3;
            mapDataPCL[0][0x53 - rangeMin] = 0x0054;
            mapDataPCL[0][0x54 - rangeMin] = 0x03a5;
            mapDataPCL[0][0x55 - rangeMin] = 0x03a6;
            mapDataPCL[0][0x56 - rangeMin] = 0x0058;
            mapDataPCL[0][0x57 - rangeMin] = 0x03a8;
            mapDataPCL[0][0x58 - rangeMin] = 0x03a9;
            mapDataPCL[0][0x59 - rangeMin] = 0x2207;
            mapDataPCL[0][0x5a - rangeMin] = 0x2202;
            mapDataPCL[0][0x5b - rangeMin] = 0x03c2;
            mapDataPCL[0][0x5c - rangeMin] = 0x2264;
            mapDataPCL[0][0x5d - rangeMin] = 0x2260;
            mapDataPCL[0][0x5e - rangeMin] = 0x2265;
            mapDataPCL[0][0x5f - rangeMin] = 0xefeb;

            mapDataPCL[0][0x60 - rangeMin] = 0x2235;
            mapDataPCL[0][0x61 - rangeMin] = 0x03b1;
            mapDataPCL[0][0x62 - rangeMin] = 0x03b2;
            mapDataPCL[0][0x63 - rangeMin] = 0x03b3;
            mapDataPCL[0][0x64 - rangeMin] = 0x03b4;
            mapDataPCL[0][0x65 - rangeMin] = 0x03b5;
            mapDataPCL[0][0x66 - rangeMin] = 0x03b6;
            mapDataPCL[0][0x67 - rangeMin] = 0x03b7;
            mapDataPCL[0][0x68 - rangeMin] = 0x03b8;
            mapDataPCL[0][0x69 - rangeMin] = 0x03b9;
            mapDataPCL[0][0x6a - rangeMin] = 0x03ba;
            mapDataPCL[0][0x6b - rangeMin] = 0x03bb;
            mapDataPCL[0][0x6c - rangeMin] = 0x03bc;
            mapDataPCL[0][0x6d - rangeMin] = 0x03bd;
            mapDataPCL[0][0x6e - rangeMin] = 0x03be;
            mapDataPCL[0][0x6f - rangeMin] = 0x03bf;

            mapDataPCL[0][0x70 - rangeMin] = 0x03c0;
            mapDataPCL[0][0x71 - rangeMin] = 0x03c1;
            mapDataPCL[0][0x72 - rangeMin] = 0x03c3;
            mapDataPCL[0][0x73 - rangeMin] = 0x03c4;
            mapDataPCL[0][0x74 - rangeMin] = 0x03c5;
            mapDataPCL[0][0x75 - rangeMin] = 0x03c6;
            mapDataPCL[0][0x76 - rangeMin] = 0x03c7;
            mapDataPCL[0][0x77 - rangeMin] = 0x03c8;
            mapDataPCL[0][0x78 - rangeMin] = 0x03c9;
            mapDataPCL[0][0x79 - rangeMin] = 0x03d1;
            mapDataPCL[0][0x7a - rangeMin] = 0x03d5;
            mapDataPCL[0][0x7b - rangeMin] = 0x03d6;
            mapDataPCL[0][0x7c - rangeMin] = 0x2243;
            mapDataPCL[0][0x7d - rangeMin] = 0x2261;
            mapDataPCL[0][0x7e - rangeMin] = 0x2262;
            mapDataPCL[0][0x7f - rangeMin] = 0x2592;

            //----------------------------------------------------------------//
            //                                                                //
            // Range 1                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            rangeMin = rangeData [1] [0];
            rangeMax = rangeData [1] [1];
            rangeSize = rangeSizes [1];

            mapDataPCL[1][0xa0 - rangeMin] = 0x00a0;
            mapDataPCL[1][0xa1 - rangeMin] = 0x2191;
            mapDataPCL[1][0xa2 - rangeMin] = 0x2192;
            mapDataPCL[1][0xa3 - rangeMin] = 0x2193;
            mapDataPCL[1][0xa4 - rangeMin] = 0x2190;
            mapDataPCL[1][0xa5 - rangeMin] = 0x21d1;
            mapDataPCL[1][0xa6 - rangeMin] = 0x21d2;
            mapDataPCL[1][0xa7 - rangeMin] = 0x21d3;
            mapDataPCL[1][0xa8 - rangeMin] = 0x21d0;
            mapDataPCL[1][0xa9 - rangeMin] = 0x2195;
            mapDataPCL[1][0xaa - rangeMin] = 0x2194;
            mapDataPCL[1][0xab - rangeMin] = 0x21d5;
            mapDataPCL[1][0xac - rangeMin] = 0x21d4;
            mapDataPCL[1][0xad - rangeMin] = 0x21c4;
            mapDataPCL[1][0xae - rangeMin] = 0x21c6;
            mapDataPCL[1][0xaf - rangeMin] = 0xefe9;

            mapDataPCL[1][0xb0 - rangeMin] = 0xefea;
            mapDataPCL[1][0xb1 - rangeMin] = 0x2200;
            mapDataPCL[1][0xb2 - rangeMin] = 0x2203;
            mapDataPCL[1][0xb3 - rangeMin] = 0x22a4;
            mapDataPCL[1][0xb4 - rangeMin] = 0x22a5;
            mapDataPCL[1][0xb5 - rangeMin] = 0x222a;
            mapDataPCL[1][0xb6 - rangeMin] = 0x2229;
            mapDataPCL[1][0xb7 - rangeMin] = 0x2208;
            mapDataPCL[1][0xb8 - rangeMin] = 0x220b;
            mapDataPCL[1][0xb9 - rangeMin] = 0x2209;
            mapDataPCL[1][0xba - rangeMin] = 0x2282;
            mapDataPCL[1][0xbb - rangeMin] = 0x2283;
            mapDataPCL[1][0xbc - rangeMin] = 0x2284;
            mapDataPCL[1][0xbd - rangeMin] = 0x2285;
            mapDataPCL[1][0xbe - rangeMin] = 0x2286;
            mapDataPCL[1][0xbf - rangeMin] = 0x2287;

            mapDataPCL[1][0xc0 - rangeMin] = 0x2295;
            mapDataPCL[1][0xc1 - rangeMin] = 0x2299;
            mapDataPCL[1][0xc2 - rangeMin] = 0x2297;
            mapDataPCL[1][0xc3 - rangeMin] = 0x2296;
            mapDataPCL[1][0xc4 - rangeMin] = 0x2298;
            mapDataPCL[1][0xc5 - rangeMin] = 0x2227;
            mapDataPCL[1][0xc6 - rangeMin] = 0x2228;
            mapDataPCL[1][0xc7 - rangeMin] = 0x22bb;
            mapDataPCL[1][0xc8 - rangeMin] = 0x00ac;
            mapDataPCL[1][0xc9 - rangeMin] = 0x2218;
            mapDataPCL[1][0xca - rangeMin] = 0x2219;
            mapDataPCL[1][0xcb - rangeMin] = 0x2022;
            mapDataPCL[1][0xcc - rangeMin] = 0x25cf;
            mapDataPCL[1][0xcd - rangeMin] = 0x20dd;
            mapDataPCL[1][0xce - rangeMin] = 0x2020;
            mapDataPCL[1][0xcf - rangeMin] = 0x2021;

            mapDataPCL[1][0xd0 - rangeMin] = 0x00c5;
            mapDataPCL[1][0xd1 - rangeMin] = 0x22a3;
            mapDataPCL[1][0xd2 - rangeMin] = 0x22a2;
            mapDataPCL[1][0xd3 - rangeMin] = 0x221f;
            mapDataPCL[1][0xd4 - rangeMin] = 0x220d;
            mapDataPCL[1][0xd5 - rangeMin] = 0x222b;
            mapDataPCL[1][0xd6 - rangeMin] = 0x222e;
            mapDataPCL[1][0xd7 - rangeMin] = 0x2220;
            mapDataPCL[1][0xd8 - rangeMin] = 0x2205;
            mapDataPCL[1][0xd9 - rangeMin] = 0x2135;
            mapDataPCL[1][0xda - rangeMin] = 0x2136;
            mapDataPCL[1][0xdb - rangeMin] = 0x2137;
            mapDataPCL[1][0xdc - rangeMin] = 0x212d;
            mapDataPCL[1][0xdd - rangeMin] = 0x2111;
            mapDataPCL[1][0xde - rangeMin] = 0x211c;
            mapDataPCL[1][0xdf - rangeMin] = 0x2128;

            mapDataPCL[1][0xe0 - rangeMin] = 0xefe7;
            mapDataPCL[1][0xe1 - rangeMin] = 0xefe6;
            mapDataPCL[1][0xe2 - rangeMin] = 0xefe3;
            mapDataPCL[1][0xe3 - rangeMin] = 0xefe2;
            mapDataPCL[1][0xe4 - rangeMin] = 0xefe1;
            mapDataPCL[1][0xe5 - rangeMin] = 0xefe3;
            mapDataPCL[1][0xe6 - rangeMin] = 0xefd4;
            mapDataPCL[1][0xe7 - rangeMin] = 0x2321;
            mapDataPCL[1][0xe8 - rangeMin] = 0xefd3;
            mapDataPCL[1][0xe9 - rangeMin] = 0xefc9;
            mapDataPCL[1][0xea - rangeMin] = 0xefd2;
            mapDataPCL[1][0xeb - rangeMin] = 0xefd1;
            mapDataPCL[1][0xec - rangeMin] = 0xefe8;
            mapDataPCL[1][0xed - rangeMin] = 0xefcb;
            mapDataPCL[1][0xee - rangeMin] = 0x2217;
            mapDataPCL[1][0xef - rangeMin] = 0x2245;

            mapDataPCL[1][0xf0 - rangeMin] = 0xefe5;
            mapDataPCL[1][0xf1 - rangeMin] = 0xefe4;
            mapDataPCL[1][0xf2 - rangeMin] = 0xefe0;
            mapDataPCL[1][0xf3 - rangeMin] = 0xefdf;
            mapDataPCL[1][0xf4 - rangeMin] = 0xefde;
            mapDataPCL[1][0xf5 - rangeMin] = 0xefdd;
            mapDataPCL[1][0xf6 - rangeMin] = 0x2223;
            mapDataPCL[1][0xf7 - rangeMin] = 0xefdc;
            mapDataPCL[1][0xf8 - rangeMin] = 0xefd0;
            mapDataPCL[1][0xf9 - rangeMin] = 0xefcf;
            mapDataPCL[1][0xfa - rangeMin] = 0xefce;
            mapDataPCL[1][0xfb - rangeMin] = 0xefcd;
            mapDataPCL[1][0xfc - rangeMin] = 0xefcc;
            mapDataPCL[1][0xfd - rangeMin] = 0x2213;
            mapDataPCL[1][0xfe - rangeMin] = 0x00b1;
            mapDataPCL[1][0xff - rangeMin] = 0xffff;    //<not a character> //

            //----------------------------------------------------------------//

            _sets.Add (new PCLSymSetMap (mapId,
                                         rangeCt,
                                         rangeData,
                                         null,
                                         mapDataPCL));
        }
    }
}