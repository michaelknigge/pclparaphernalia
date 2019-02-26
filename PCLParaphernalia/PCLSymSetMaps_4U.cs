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
        // u n i c o d e M a p _ 4 U                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       4U                                                        //
        // Kind1    149                                                       //
        // Name     Roman-9                                                   //
        //          As Roman-8 but with Euro instead of currency symbol       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_4U ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_4U;

            const Int32 rangeCt = 2;

            UInt16 [] [] rangeData = new UInt16 [rangeCt] []
            {
                new UInt16 [2] {0x20, 0x7f},
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

            mapDataStd [1] [0xa0 - rangeMin] = 0x00a0;
            mapDataStd [1] [0xa1 - rangeMin] = 0x00c0;
            mapDataStd [1] [0xa2 - rangeMin] = 0x00c2;
            mapDataStd [1] [0xa3 - rangeMin] = 0x00c8;
            mapDataStd [1] [0xa4 - rangeMin] = 0x00ca;
            mapDataStd [1] [0xa5 - rangeMin] = 0x00cb;
            mapDataStd [1] [0xa6 - rangeMin] = 0x00ce;
            mapDataStd [1] [0xa7 - rangeMin] = 0x00cf;
            mapDataStd [1] [0xa8 - rangeMin] = 0x00b4;
            mapDataStd [1] [0xa9 - rangeMin] = 0x0060;
            mapDataStd [1] [0xaa - rangeMin] = 0x02c6;
            mapDataStd [1] [0xab - rangeMin] = 0x00a8;
            mapDataStd [1] [0xac - rangeMin] = 0x02dc;
            mapDataStd [1] [0xad - rangeMin] = 0x00d9;
            mapDataStd [1] [0xae - rangeMin] = 0x00db;
            mapDataStd [1] [0xaf - rangeMin] = 0x20a4;

            mapDataStd [1] [0xb0 - rangeMin] = 0x203e;
            mapDataStd [1] [0xb1 - rangeMin] = 0x00dd;
            mapDataStd [1] [0xb2 - rangeMin] = 0x00fd;
            mapDataStd [1] [0xb3 - rangeMin] = 0x02da;
            mapDataStd [1] [0xb4 - rangeMin] = 0x00c7;
            mapDataStd [1] [0xb5 - rangeMin] = 0x00e7;
            mapDataStd [1] [0xb6 - rangeMin] = 0x00d1;
            mapDataStd [1] [0xb7 - rangeMin] = 0x00f1;
            mapDataStd [1] [0xb8 - rangeMin] = 0x00a1;
            mapDataStd [1] [0xb9 - rangeMin] = 0x00bf;
            mapDataStd [1] [0xba - rangeMin] = 0x20ac;
            mapDataStd [1] [0xbb - rangeMin] = 0x00a3;
            mapDataStd [1] [0xbc - rangeMin] = 0x00a5;
            mapDataStd [1] [0xbd - rangeMin] = 0x00a7;
            mapDataStd [1] [0xbe - rangeMin] = 0x0192;
            mapDataStd [1] [0xbf - rangeMin] = 0x00a2;

            mapDataStd [1] [0xc0 - rangeMin] = 0x00e2;
            mapDataStd [1] [0xc1 - rangeMin] = 0x00ea;
            mapDataStd [1] [0xc2 - rangeMin] = 0x00f4;
            mapDataStd [1] [0xc3 - rangeMin] = 0x00fb;
            mapDataStd [1] [0xc4 - rangeMin] = 0x00e1;
            mapDataStd [1] [0xc5 - rangeMin] = 0x00e9;
            mapDataStd [1] [0xc6 - rangeMin] = 0x00f3;
            mapDataStd [1] [0xc7 - rangeMin] = 0x00fa;
            mapDataStd [1] [0xc8 - rangeMin] = 0x00e0;
            mapDataStd [1] [0xc9 - rangeMin] = 0x00e8;
            mapDataStd [1] [0xca - rangeMin] = 0x00f2;
            mapDataStd [1] [0xcb - rangeMin] = 0x00f9;
            mapDataStd [1] [0xcc - rangeMin] = 0x00e4;
            mapDataStd [1] [0xcd - rangeMin] = 0x00eb;
            mapDataStd [1] [0xce - rangeMin] = 0x00f6;
            mapDataStd [1] [0xcf - rangeMin] = 0x00fc;

            mapDataStd [1] [0xd0 - rangeMin] = 0x00c5;
            mapDataStd [1] [0xd1 - rangeMin] = 0x00ee;
            mapDataStd [1] [0xd2 - rangeMin] = 0x00d8;
            mapDataStd [1] [0xd3 - rangeMin] = 0x00c6;
            mapDataStd [1] [0xd4 - rangeMin] = 0x00e5;
            mapDataStd [1] [0xd5 - rangeMin] = 0x00ed;
            mapDataStd [1] [0xd6 - rangeMin] = 0x00f8;
            mapDataStd [1] [0xd7 - rangeMin] = 0x00e6;
            mapDataStd [1] [0xd8 - rangeMin] = 0x00c4;
            mapDataStd [1] [0xd9 - rangeMin] = 0x00ec;
            mapDataStd [1] [0xda - rangeMin] = 0x00d6;
            mapDataStd [1] [0xdb - rangeMin] = 0x00dc;
            mapDataStd [1] [0xdc - rangeMin] = 0x00c9;
            mapDataStd [1] [0xdd - rangeMin] = 0x00ef;
            mapDataStd [1] [0xde - rangeMin] = 0x00df;
            mapDataStd [1] [0xdf - rangeMin] = 0x00d4;

            mapDataStd [1] [0xe0 - rangeMin] = 0x00c1;
            mapDataStd [1] [0xe1 - rangeMin] = 0x00c3;
            mapDataStd [1] [0xe2 - rangeMin] = 0x00e3;
            mapDataStd [1] [0xe3 - rangeMin] = 0x00d0;
            mapDataStd [1] [0xe4 - rangeMin] = 0x00f0;
            mapDataStd [1] [0xe5 - rangeMin] = 0x00cd;
            mapDataStd [1] [0xe6 - rangeMin] = 0x00cc;
            mapDataStd [1] [0xe7 - rangeMin] = 0x00d3;
            mapDataStd [1] [0xe8 - rangeMin] = 0x00d2;
            mapDataStd [1] [0xe9 - rangeMin] = 0x00d5;
            mapDataStd [1] [0xea - rangeMin] = 0x00f5;
            mapDataStd [1] [0xeb - rangeMin] = 0x0160;
            mapDataStd [1] [0xec - rangeMin] = 0x0161;
            mapDataStd [1] [0xed - rangeMin] = 0x00da;
            mapDataStd [1] [0xee - rangeMin] = 0x0178;
            mapDataStd [1] [0xef - rangeMin] = 0x00ff;

            mapDataStd [1] [0xf0 - rangeMin] = 0x00de;
            mapDataStd [1] [0xf1 - rangeMin] = 0x00fe;
            mapDataStd [1] [0xf2 - rangeMin] = 0x00b7;
            mapDataStd [1] [0xf3 - rangeMin] = 0x03bc;
            mapDataStd [1] [0xf4 - rangeMin] = 0x00b6;
            mapDataStd [1] [0xf5 - rangeMin] = 0x00be;
            mapDataStd [1] [0xf6 - rangeMin] = 0x2212;
            mapDataStd [1] [0xf7 - rangeMin] = 0x00bc;
            mapDataStd [1] [0xf8 - rangeMin] = 0x00bd;
            mapDataStd [1] [0xf9 - rangeMin] = 0x00aa;
            mapDataStd [1] [0xfa - rangeMin] = 0x00ba;
            mapDataStd [1] [0xfb - rangeMin] = 0x00ab;
            mapDataStd [1] [0xfc - rangeMin] = 0x25a0;
            mapDataStd [1] [0xfd - rangeMin] = 0x00bb;
            mapDataStd [1] [0xfe - rangeMin] = 0x00b1;
            mapDataStd [1] [0xff - rangeMin] = 0xffff;    //<not a character> //

            //----------------------------------------------------------------//

            for (UInt16 i = 0; i < rangeSize; i++)
            {
                mapDataPCL [1] [i] = mapDataStd [1] [i];
            }

            mapDataPCL[1][0xaf - rangeMin] = 0x00a3;
            mapDataPCL[1][0xb0 - rangeMin] = 0x00af;
            mapDataPCL[1][0xb3 - rangeMin] = 0x00b0;

            //----------------------------------------------------------------//

            _sets.Add (new PCLSymSetMap (mapId,
                                         rangeCt,
                                         rangeData,
                                         mapDataStd,
                                         mapDataPCL));
        }
    }
}