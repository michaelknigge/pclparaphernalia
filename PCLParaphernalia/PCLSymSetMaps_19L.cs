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
        // u n i c o d e M a p _ 1 9 L                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       19L                                                       //
        // Kind1    620                                                       //
        // Name     Windows Baltic                                            //
        //          Code Page 1257                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_19L ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_19L;

            const Int32 rangeCt = 3;

            UInt16 [] [] rangeData = new UInt16 [rangeCt] []
            {
                new UInt16 [2] {0x20, 0x7f},
                new UInt16 [2] {0x80, 0x9f},
                new UInt16 [2] {0xa0, 0xff}
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

            mapDataStd [0] [0x7f - rangeMin] = 0xffff;    //<not a character> //

            //----------------------------------------------------------------//

            for (UInt16 i = 0; i < rangeSize; i++)
            {
                mapDataPCL [0] [i] = mapDataStd [0] [i];
            }

            mapDataPCL [0] [0x7f - rangeMin] = 0x2592;

            //----------------------------------------------------------------//
            //                                                                //
            // Range 1                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            rangeMin = rangeData [1] [0];
            rangeMax = rangeData [1] [1];
            rangeSize = rangeSizes [1];

            mapDataStd [1] [0x80 - rangeMin] = 0x20ac;
            mapDataStd [1] [0x81 - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [1] [0x82 - rangeMin] = 0x201a;
            mapDataStd [1] [0x83 - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [1] [0x84 - rangeMin] = 0x201e;
            mapDataStd [1] [0x85 - rangeMin] = 0x2026;
            mapDataStd [1] [0x86 - rangeMin] = 0x2020;
            mapDataStd [1] [0x87 - rangeMin] = 0x2021;
            mapDataStd [1] [0x88 - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [1] [0x89 - rangeMin] = 0x2030;
            mapDataStd [1] [0x8a - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [1] [0x8b - rangeMin] = 0x2039;
            mapDataStd [1] [0x8c - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [1] [0x8d - rangeMin] = 0x00a8;
            mapDataStd [1] [0x8e - rangeMin] = 0x02c7;
            mapDataStd [1] [0x8f - rangeMin] = 0x00b8;

            mapDataStd [1] [0x90 - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [1] [0x91 - rangeMin] = 0x2018;
            mapDataStd [1] [0x92 - rangeMin] = 0x2019;
            mapDataStd [1] [0x93 - rangeMin] = 0x201c;
            mapDataStd [1] [0x94 - rangeMin] = 0x201d;
            mapDataStd [1] [0x95 - rangeMin] = 0x2022;
            mapDataStd [1] [0x96 - rangeMin] = 0x2013;
            mapDataStd [1] [0x97 - rangeMin] = 0x2014;
            mapDataStd [1] [0x98 - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [1] [0x99 - rangeMin] = 0x2122;
            mapDataStd [1] [0x9a - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [1] [0x9b - rangeMin] = 0x203a;
            mapDataStd [1] [0x9c - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [1] [0x9d - rangeMin] = 0x00af;
            mapDataStd [1] [0x9e - rangeMin] = 0x02db;
            mapDataStd [1] [0x9f - rangeMin] = 0xffff;    //<not a character> //

            //----------------------------------------------------------------//

            for (UInt16 i = 0; i < rangeSize; i++)
            {
                mapDataPCL [1] [i] = mapDataStd [1] [i];
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Range 2                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            rangeMin = rangeData [2] [0];
            rangeMax = rangeData [2] [1];
            rangeSize = rangeSizes [2];

            mapDataStd[2][0xa0 - rangeMin] = 0x00a0;
            mapDataStd[2][0xa1 - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd[2][0xa2 - rangeMin] = 0x00a2;
            mapDataStd[2][0xa3 - rangeMin] = 0x00a3;
            mapDataStd[2][0xa4 - rangeMin] = 0x00a4;
            mapDataStd[2][0xa5 - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd[2][0xa6 - rangeMin] = 0x00a6;
            mapDataStd[2][0xa7 - rangeMin] = 0x00a7;
            mapDataStd[2][0xa8 - rangeMin] = 0x00d8;
            mapDataStd[2][0xa9 - rangeMin] = 0x00a9;
            mapDataStd[2][0xaa - rangeMin] = 0x0156;
            mapDataStd[2][0xab - rangeMin] = 0x00ab;
            mapDataStd[2][0xac - rangeMin] = 0x00ac;
            mapDataStd[2][0xad - rangeMin] = 0x00ad;
            mapDataStd[2][0xae - rangeMin] = 0x00ae;
            mapDataStd[2][0xaf - rangeMin] = 0x00c6;

            mapDataStd[2][0xb0 - rangeMin] = 0x00b0;
            mapDataStd[2][0xb1 - rangeMin] = 0x00b1;
            mapDataStd[2][0xb2 - rangeMin] = 0x00b2;
            mapDataStd[2][0xb3 - rangeMin] = 0x00b3;
            mapDataStd[2][0xb4 - rangeMin] = 0x00b4;
            mapDataStd[2][0xb5 - rangeMin] = 0x00b5;
            mapDataStd[2][0xb6 - rangeMin] = 0x00b6;
            mapDataStd[2][0xb7 - rangeMin] = 0x00b7;
            mapDataStd[2][0xb8 - rangeMin] = 0x00f8;
            mapDataStd[2][0xb9 - rangeMin] = 0x00b9;
            mapDataStd[2][0xba - rangeMin] = 0x0157;
            mapDataStd[2][0xbb - rangeMin] = 0x00bb;
            mapDataStd[2][0xbc - rangeMin] = 0x00bc;
            mapDataStd[2][0xbd - rangeMin] = 0x00bd;
            mapDataStd[2][0xbe - rangeMin] = 0x00be;
            mapDataStd[2][0xbf - rangeMin] = 0x00e6;

            mapDataStd[2][0xc0 - rangeMin] = 0x0104;
            mapDataStd[2][0xc1 - rangeMin] = 0x012e;
            mapDataStd[2][0xc2 - rangeMin] = 0x0100;
            mapDataStd[2][0xc3 - rangeMin] = 0x0106;
            mapDataStd[2][0xc4 - rangeMin] = 0x00c4;
            mapDataStd[2][0xc5 - rangeMin] = 0x00c5;
            mapDataStd[2][0xc6 - rangeMin] = 0x0118;
            mapDataStd[2][0xc7 - rangeMin] = 0x0112;
            mapDataStd[2][0xc8 - rangeMin] = 0x010c;
            mapDataStd[2][0xc9 - rangeMin] = 0x00c9;
            mapDataStd[2][0xca - rangeMin] = 0x0179;
            mapDataStd[2][0xcb - rangeMin] = 0x0116;
            mapDataStd[2][0xcc - rangeMin] = 0x0122;
            mapDataStd[2][0xcd - rangeMin] = 0x0136;
            mapDataStd[2][0xce - rangeMin] = 0x012a;
            mapDataStd[2][0xcf - rangeMin] = 0x013b;

            mapDataStd[2][0xd0 - rangeMin] = 0x0160;
            mapDataStd[2][0xd1 - rangeMin] = 0x0143;
            mapDataStd[2][0xd2 - rangeMin] = 0x0145;
            mapDataStd[2][0xd3 - rangeMin] = 0x00d3;
            mapDataStd[2][0xd4 - rangeMin] = 0x014c;
            mapDataStd[2][0xd5 - rangeMin] = 0x00d5;
            mapDataStd[2][0xd6 - rangeMin] = 0x00d6;
            mapDataStd[2][0xd7 - rangeMin] = 0x00d7;
            mapDataStd[2][0xd8 - rangeMin] = 0x0172;
            mapDataStd[2][0xd9 - rangeMin] = 0x0141;
            mapDataStd[2][0xda - rangeMin] = 0x015a;
            mapDataStd[2][0xdb - rangeMin] = 0x016a;
            mapDataStd[2][0xdc - rangeMin] = 0x00dc;
            mapDataStd[2][0xdd - rangeMin] = 0x017b;
            mapDataStd[2][0xde - rangeMin] = 0x017d;
            mapDataStd[2][0xdf - rangeMin] = 0x00df;

            mapDataStd[2][0xe0 - rangeMin] = 0x0105;
            mapDataStd[2][0xe1 - rangeMin] = 0x012f;
            mapDataStd[2][0xe2 - rangeMin] = 0x0101;
            mapDataStd[2][0xe3 - rangeMin] = 0x0107;
            mapDataStd[2][0xe4 - rangeMin] = 0x00e4;
            mapDataStd[2][0xe5 - rangeMin] = 0x00e5;
            mapDataStd[2][0xe6 - rangeMin] = 0x0119;
            mapDataStd[2][0xe7 - rangeMin] = 0x0113;
            mapDataStd[2][0xe8 - rangeMin] = 0x010d;
            mapDataStd[2][0xe9 - rangeMin] = 0x00e9;
            mapDataStd[2][0xea - rangeMin] = 0x017a;
            mapDataStd[2][0xeb - rangeMin] = 0x0117;
            mapDataStd[2][0xec - rangeMin] = 0x0123;
            mapDataStd[2][0xed - rangeMin] = 0x0137;
            mapDataStd[2][0xee - rangeMin] = 0x012b;
            mapDataStd[2][0xef - rangeMin] = 0x013c;

            mapDataStd[2][0xf0 - rangeMin] = 0x0161;
            mapDataStd[2][0xf1 - rangeMin] = 0x0144;
            mapDataStd[2][0xf2 - rangeMin] = 0x0146;
            mapDataStd[2][0xf3 - rangeMin] = 0x00f3;
            mapDataStd[2][0xf4 - rangeMin] = 0x014d;
            mapDataStd[2][0xf5 - rangeMin] = 0x00f5;
            mapDataStd[2][0xf6 - rangeMin] = 0x00f6;
            mapDataStd[2][0xf7 - rangeMin] = 0x00f7;
            mapDataStd[2][0xf8 - rangeMin] = 0x0173;
            mapDataStd[2][0xf9 - rangeMin] = 0x0142;
            mapDataStd[2][0xfa - rangeMin] = 0x015b;
            mapDataStd[2][0xfb - rangeMin] = 0x016b;
            mapDataStd[2][0xfc - rangeMin] = 0x00fc;
            mapDataStd[2][0xfd - rangeMin] = 0x017c;
            mapDataStd[2][0xfe - rangeMin] = 0x017e;
            mapDataStd[2][0xff - rangeMin] = 0x02d9;

            //----------------------------------------------------------------//

            for (UInt16 i = 0; i < rangeSize; i++)
            {
                mapDataPCL[2][i] = mapDataStd[2][i];
            }

            //----------------------------------------------------------------//

            _sets.Add (new PCLSymSetMap (mapId,
                                         rangeCt,
                                         rangeData,
                                         mapDataStd,
                                         mapDataPCL));
        }
    }
}