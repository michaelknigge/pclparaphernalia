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
        // u n i c o d e M a p _ 6 J                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       6J                                                        //
        // Kind1    202                                                       //
        // Name     Microsoft Publishing                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_6J ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_6J;

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
                mapDataPCL [0] [i - rangeMin] = 0xffff;    //<not a character> //
            }

            mapDataPCL[0][0x20 - rangeMin] = 0x0020;
            mapDataPCL[0][0x21 - rangeMin] = 0x00b9;
            mapDataPCL[0][0x22 - rangeMin] = 0x201d;
            mapDataPCL[0][0x23 - rangeMin] = 0x00b3;
            mapDataPCL[0][0x24 - rangeMin] = 0x2074;
            mapDataPCL[0][0x25 - rangeMin] = 0x2075;
            mapDataPCL[0][0x26 - rangeMin] = 0x2077;
            mapDataPCL[0][0x27 - rangeMin] = 0x2019;
            mapDataPCL[0][0x28 - rangeMin] = 0x2079;
            mapDataPCL[0][0x29 - rangeMin] = 0x2070;
            mapDataPCL[0][0x2a - rangeMin] = 0x2078;
            mapDataPCL[0][0x2b - rangeMin] = 0x2020;
            mapDataPCL[0][0x2c - rangeMin] = 0x002c;
            mapDataPCL[0][0x2d - rangeMin] = 0x2212;
            mapDataPCL[0][0x2e - rangeMin] = 0x2026;
            mapDataPCL[0][0x2f - rangeMin] = 0x2215;

            mapDataPCL[0][0x3c - rangeMin] = 0x201e;
            mapDataPCL[0][0x3d - rangeMin] = 0x2021;

            mapDataPCL[0][0x40 - rangeMin] = 0x00b2;
            mapDataPCL[0][0x4c - rangeMin] = 0x013d;
            mapDataPCL[0][0x4d - rangeMin] = 0x2014;
            mapDataPCL[0][0x4e - rangeMin] = 0x2013;
            mapDataPCL[0][0x4f - rangeMin] = 0x0152;

            mapDataPCL[0][0x52 - rangeMin] = 0x211e;
            mapDataPCL[0][0x53 - rangeMin] = 0x0160;
            mapDataPCL[0][0x54 - rangeMin] = 0x2122;
            mapDataPCL[0][0x59 - rangeMin] = 0x0178;
            mapDataPCL[0][0x5a - rangeMin] = 0x017d;
            mapDataPCL[0][0x5e - rangeMin] = 0x2076;
            mapDataPCL[0][0x5f - rangeMin] = 0x2017;

            mapDataPCL[0][0x60 - rangeMin] = 0x2018;
            mapDataPCL[0][0x63 - rangeMin] = 0x2105;
            mapDataPCL[0][0x6c - rangeMin] = 0x2113;
            mapDataPCL[0][0x6d - rangeMin] = 0x2003;
            mapDataPCL[0][0x6e - rangeMin] = 0x2002;
            mapDataPCL[0][0x6f - rangeMin] = 0x0153;

            mapDataPCL[0][0x73 - rangeMin] = 0x0161;
            mapDataPCL[0][0x74 - rangeMin] = 0x2009;
            mapDataPCL[0][0x7a - rangeMin] = 0x017e;
            mapDataPCL[0][0x7e - rangeMin] = 0x201c;

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
                mapDataPCL[1][i - rangeMin] = 0xffff;    //<not a character> //
            }

            mapDataPCL [1] [0x9e - rangeMin] = 0x20a7;
            mapDataPCL [1] [0x9f - rangeMin] = 0x0192;

            //----------------------------------------------------------------//
            //                                                                //
            // Range 2                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            rangeMin = rangeData [2] [0];
            rangeMax = rangeData [2] [1];
            rangeSize = rangeSizes [2];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataPCL[2][i - rangeMin] = 0xffff;    //<not a character> //
            }

            mapDataPCL[2][0xa0 - rangeMin] = 0x00a0;
            mapDataPCL[2][0xa1 - rangeMin] = 0x2032;
            mapDataPCL[2][0xa2 - rangeMin] = 0x2033;
            mapDataPCL[2][0xa3 - rangeMin] = 0x005e;
            mapDataPCL[2][0xa4 - rangeMin] = 0x007e;
            mapDataPCL[2][0xa9 - rangeMin] = 0xfb01;
            mapDataPCL[2][0xaa - rangeMin] = 0xfb02;
            mapDataPCL[2][0xab - rangeMin] = 0xfb00;
            mapDataPCL[2][0xac - rangeMin] = 0xfb03;
            mapDataPCL[2][0xad - rangeMin] = 0xfb04;
            mapDataPCL[2][0xae - rangeMin] = 0x2039;
            mapDataPCL[2][0xaf - rangeMin] = 0x203a;

            mapDataPCL[2][0xb0 - rangeMin] = 0x00b0;
            mapDataPCL[2][0xb1 - rangeMin] = 0x2022;
            mapDataPCL[2][0xb2 - rangeMin] = 0x25cf;
            mapDataPCL[2][0xb3 - rangeMin] = 0xeffa;
            mapDataPCL[2][0xb4 - rangeMin] = 0x25e6;
            mapDataPCL[2][0xb5 - rangeMin] = 0x25cb;
            mapDataPCL[2][0xb6 - rangeMin] = 0xeffd;
            mapDataPCL[2][0xb7 - rangeMin] = 0x25aa;
            mapDataPCL[2][0xb8 - rangeMin] = 0x25a0;
            mapDataPCL[2][0xb9 - rangeMin] = 0xeffb;
            mapDataPCL[2][0xba - rangeMin] = 0x25ab;
            mapDataPCL[2][0xbb - rangeMin] = 0x25a1;
            mapDataPCL[2][0xbc - rangeMin] = 0xeffc;
            mapDataPCL[2][0xbd - rangeMin] = 0x2030;
            mapDataPCL[2][0xbe - rangeMin] = 0x25c6;
            mapDataPCL[2][0xbf - rangeMin] = 0x25c7;

            mapDataPCL[2][0xc1 - rangeMin] = 0xeff8;
            mapDataPCL[2][0xc2 - rangeMin] = 0xeff9;
            mapDataPCL[2][0xc3 - rangeMin] = 0xeff7;
            mapDataPCL[2][0xc4 - rangeMin] = 0xeff5;
            mapDataPCL[2][0xc5 - rangeMin] = 0xefef;
            mapDataPCL[2][0xc6 - rangeMin] = 0xefee;
            mapDataPCL[2][0xc7 - rangeMin] = 0xefed;
            mapDataPCL[2][0xc8 - rangeMin] = 0xeff6;
            mapDataPCL[2][0xca - rangeMin] = 0xeff3;
            mapDataPCL[2][0xcb - rangeMin] = 0xeff2;
            mapDataPCL[2][0xcd - rangeMin] = 0xeff0;
            mapDataPCL[2][0xce - rangeMin] = 0xeff1;
            mapDataPCL[2][0xcf - rangeMin] = 0xeff4;

            mapDataPCL[2][0xd1 - rangeMin] = 0x0060;
            mapDataPCL[2][0xd2 - rangeMin] = 0x00b4;
            mapDataPCL[2][0xd3 - rangeMin] = 0x02c6;
            mapDataPCL[2][0xd4 - rangeMin] = 0x02dc;
            mapDataPCL[2][0xd5 - rangeMin] = 0x02c9;
            mapDataPCL[2][0xd6 - rangeMin] = 0x02d8;
            mapDataPCL[2][0xd7 - rangeMin] = 0x02d9;
            mapDataPCL[2][0xd8 - rangeMin] = 0x00a8;
            mapDataPCL[2][0xda - rangeMin] = 0x02da;
            mapDataPCL[2][0xdb - rangeMin] = 0x00b8;
            mapDataPCL[2][0xdd - rangeMin] = 0x02dd;
            mapDataPCL[2][0xde - rangeMin] = 0x02db;
            mapDataPCL[2][0xdf - rangeMin] = 0x02c7;


            mapDataPCL[2][0xe0 - rangeMin] = 0x03a9;
            mapDataPCL[2][0xe6 - rangeMin] = 0x0132;
            mapDataPCL[2][0xe7 - rangeMin] = 0x013f;
            mapDataPCL[2][0xe8 - rangeMin] = 0x0141;
            mapDataPCL[2][0xef - rangeMin] = 0x0149;

            mapDataPCL[2][0xf5 - rangeMin] = 0x0131;
            mapDataPCL[2][0xf6 - rangeMin] = 0x0133;
            mapDataPCL[2][0xf7 - rangeMin] = 0x0140;
            mapDataPCL[2][0xf8 - rangeMin] = 0x0142;

            //----------------------------------------------------------------//

            _sets.Add (new PCLSymSetMap (mapId,
                                         rangeCt,
                                         rangeData,
                                         null,
                                         mapDataPCL));
        }
    }
}