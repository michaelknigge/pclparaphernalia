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
        // u n i c o d e M a p _ 7 J                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       7J                                                        //
        // Kind1    234                                                       //
        // Name     Desktop                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_7J ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_7J;

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
            mapDataPCL [0] [0x7f - rangeMin] = 0x2592;

            //----------------------------------------------------------------//
            //                                                                //
            // Range 1                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            rangeMin = rangeData [1] [0];
            rangeMax = rangeData [1] [1];
            rangeSize = rangeSizes [1];

            mapDataPCL [1] [0xa0 - rangeMin] = 0x00a0;
            mapDataPCL [1] [0xa1 - rangeMin] = 0x00b6;
            mapDataPCL [1] [0xa2 - rangeMin] = 0x00a7;
            mapDataPCL [1] [0xa3 - rangeMin] = 0x2020;
            mapDataPCL [1] [0xa4 - rangeMin] = 0x2021;
            mapDataPCL [1] [0xa5 - rangeMin] = 0x00a9;
            mapDataPCL [1] [0xa6 - rangeMin] = 0x00ae;
            mapDataPCL [1] [0xa7 - rangeMin] = 0x2122;
            mapDataPCL [1] [0xa8 - rangeMin] = 0x2105;
            mapDataPCL [1] [0xa9 - rangeMin] = 0x00a2;
            mapDataPCL [1] [0xaa - rangeMin] = 0x2013;
            mapDataPCL [1] [0xab - rangeMin] = 0x2014;
            mapDataPCL [1] [0xac - rangeMin] = 0x2026;
            mapDataPCL [1] [0xad - rangeMin] = 0xf001;
            mapDataPCL [1] [0xae - rangeMin] = 0xf002;
            mapDataPCL [1] [0xaf - rangeMin] = 0xffff;    //<not a character> //

            mapDataPCL [1] [0xb0 - rangeMin] = 0x201c;
            mapDataPCL [1] [0xb1 - rangeMin] = 0x201d;
            mapDataPCL [1] [0xb2 - rangeMin] = 0x00b5;
            mapDataPCL [1] [0xb3 - rangeMin] = 0x2030;
            mapDataPCL [1] [0xb4 - rangeMin] = 0x2022;
            mapDataPCL [1] [0xb5 - rangeMin] = 0x25cf;
            mapDataPCL [1] [0xb6 - rangeMin] = 0x25e6;
            mapDataPCL [1] [0xb7 - rangeMin] = 0x25cb;
            mapDataPCL [1] [0xb8 - rangeMin] = 0x25aa;
            mapDataPCL [1] [0xb9 - rangeMin] = 0x25a0;
            mapDataPCL [1] [0xba - rangeMin] = 0x25ab;
            mapDataPCL [1] [0xbb - rangeMin] = 0x25a1;
            mapDataPCL [1] [0xbc - rangeMin] = 0x0027;
            mapDataPCL [1] [0xbd - rangeMin] = 0x00ac;
            mapDataPCL [1] [0xbe - rangeMin] = 0x00a6;
            mapDataPCL [1] [0xbf - rangeMin] = 0x2017;

            mapDataPCL [1] [0xc0 - rangeMin] = 0x2212;
            mapDataPCL [1] [0xc1 - rangeMin] = 0x00b1;
            mapDataPCL [1] [0xc2 - rangeMin] = 0x00d7;
            mapDataPCL [1] [0xc3 - rangeMin] = 0x00f7;
            mapDataPCL [1] [0xc4 - rangeMin] = 0x00b0;
            mapDataPCL [1] [0xc5 - rangeMin] = 0x2032;
            mapDataPCL [1] [0xc6 - rangeMin] = 0x2033;
            mapDataPCL [1] [0xc7 - rangeMin] = 0x00bc;
            mapDataPCL [1] [0xc8 - rangeMin] = 0x00bd;
            mapDataPCL [1] [0xc9 - rangeMin] = 0x00be;
            mapDataPCL [1] [0xca - rangeMin] = 0x00b9;
            mapDataPCL [1] [0xcb - rangeMin] = 0x00b2;
            mapDataPCL [1] [0xcc - rangeMin] = 0x00b3;
            mapDataPCL [1] [0xcd - rangeMin] = 0x2215;
            mapDataPCL [1] [0xce - rangeMin] = 0xffff;    //<not a character> //
            mapDataPCL [1] [0xcf - rangeMin] = 0xffff;    //<not a character> //

            mapDataPCL [1] [0xd0 - rangeMin] = 0x2039;
            mapDataPCL [1] [0xd1 - rangeMin] = 0x203a;
            mapDataPCL [1] [0xd2 - rangeMin] = 0x00ab;
            mapDataPCL [1] [0xd3 - rangeMin] = 0x00bb;
            mapDataPCL [1] [0xd4 - rangeMin] = 0x201a;
            mapDataPCL [1] [0xd5 - rangeMin] = 0x201e;
            mapDataPCL [1] [0xd6 - rangeMin] = 0x2219;
            mapDataPCL [1] [0xd7 - rangeMin] = 0x00a1;
            mapDataPCL [1] [0xd8 - rangeMin] = 0x00bf;
            mapDataPCL [1] [0xd9 - rangeMin] = 0x20a7;
            mapDataPCL [1] [0xda - rangeMin] = 0x2113;
            mapDataPCL [1] [0xdb - rangeMin] = 0x00a3;
            mapDataPCL [1] [0xdc - rangeMin] = 0x00a5;
            mapDataPCL [1] [0xdd - rangeMin] = 0x00a4;
            mapDataPCL [1] [0xde - rangeMin] = 0x0192;
            mapDataPCL [1] [0xdf - rangeMin] = 0x00df;

            mapDataPCL [1] [0xe0 - rangeMin] = 0x00aa;
            mapDataPCL [1] [0xe1 - rangeMin] = 0x00ba;
            mapDataPCL [1] [0xe2 - rangeMin] = 0x00e6;
            mapDataPCL [1] [0xe3 - rangeMin] = 0x00c6;
            mapDataPCL [1] [0xe4 - rangeMin] = 0x00f0;
            mapDataPCL [1] [0xe5 - rangeMin] = 0x00d0;
            mapDataPCL [1] [0xe6 - rangeMin] = 0x0133;
            mapDataPCL [1] [0xe7 - rangeMin] = 0x0132;
            mapDataPCL [1] [0xe8 - rangeMin] = 0x0142;
            mapDataPCL [1] [0xe9 - rangeMin] = 0x0141;
            mapDataPCL [1] [0xea - rangeMin] = 0x0153;
            mapDataPCL [1] [0xeb - rangeMin] = 0x0152;
            mapDataPCL [1] [0xec - rangeMin] = 0x00f8;
            mapDataPCL [1] [0xed - rangeMin] = 0x00d8;
            mapDataPCL [1] [0xee - rangeMin] = 0x00fe;
            mapDataPCL [1] [0xef - rangeMin] = 0x00de;

            mapDataPCL [1] [0xf0 - rangeMin] = 0x00b4;
            mapDataPCL [1] [0xf1 - rangeMin] = 0x0060;
            mapDataPCL [1] [0xf2 - rangeMin] = 0x02c6;
            mapDataPCL [1] [0xf3 - rangeMin] = 0x00a8;
            mapDataPCL [1] [0xf4 - rangeMin] = 0x02dc;
            mapDataPCL [1] [0xf5 - rangeMin] = 0x02c7;
            mapDataPCL [1] [0xf6 - rangeMin] = 0x02d8;
            mapDataPCL [1] [0xf7 - rangeMin] = 0x02dd;
            mapDataPCL [1] [0xf8 - rangeMin] = 0x02da;
            mapDataPCL [1] [0xf9 - rangeMin] = 0x02d9;
            mapDataPCL [1] [0xfa - rangeMin] = 0x02c9;
            mapDataPCL [1] [0xfb - rangeMin] = 0x00b8;
            mapDataPCL [1] [0xfc - rangeMin] = 0x02db;
            mapDataPCL [1] [0xfd - rangeMin] = 0x00b7;
            mapDataPCL [1] [0xfe - rangeMin] = 0x0131;
            mapDataPCL [1] [0xff - rangeMin] = 0xffff;    //<not a character> //

            //----------------------------------------------------------------//

            for (UInt16 i = 0; i < rangeSize; i++)
            {
                mapDataPCL [1] [i] = mapDataPCL [1] [i];
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