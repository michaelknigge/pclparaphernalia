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
        // u n i c o d e M a p _ 4 N                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       4N                                                        //
        // Kind1    142                                                       //
        // Name     ISO 8859-4 Latin 4                                        //
        //          North European                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_4N ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_4N;

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

            mapDataStd[1][0xa0 - rangeMin] = 0x00a0;
            mapDataStd[1][0xa1 - rangeMin] = 0x0104;
            mapDataStd[1][0xa2 - rangeMin] = 0x0138;
            mapDataStd[1][0xa3 - rangeMin] = 0x0156;
            mapDataStd[1][0xa4 - rangeMin] = 0x00a4;
            mapDataStd[1][0xa5 - rangeMin] = 0x0128;
            mapDataStd[1][0xa6 - rangeMin] = 0x013b;
            mapDataStd[1][0xa7 - rangeMin] = 0x00a7;
            mapDataStd[1][0xa8 - rangeMin] = 0x00a8;
            mapDataStd[1][0xa9 - rangeMin] = 0x0160;
            mapDataStd[1][0xaa - rangeMin] = 0x0112;
            mapDataStd[1][0xab - rangeMin] = 0x0122;
            mapDataStd[1][0xac - rangeMin] = 0x0166;
            mapDataStd[1][0xad - rangeMin] = 0x00ad;
            mapDataStd[1][0xae - rangeMin] = 0x017d;
            mapDataStd[1][0xaf - rangeMin] = 0x00af;

            mapDataStd[1][0xb0 - rangeMin] = 0x00b0;
            mapDataStd[1][0xb1 - rangeMin] = 0x0105;
            mapDataStd[1][0xb2 - rangeMin] = 0x02db;
            mapDataStd[1][0xb3 - rangeMin] = 0x0157;
            mapDataStd[1][0xb4 - rangeMin] = 0x00b4;
            mapDataStd[1][0xb5 - rangeMin] = 0x0129;
            mapDataStd[1][0xb6 - rangeMin] = 0x013c;
            mapDataStd[1][0xb7 - rangeMin] = 0x02c7;
            mapDataStd[1][0xb8 - rangeMin] = 0x00b8;
            mapDataStd[1][0xb9 - rangeMin] = 0x0161;
            mapDataStd[1][0xba - rangeMin] = 0x0113;
            mapDataStd[1][0xbb - rangeMin] = 0x0123;
            mapDataStd[1][0xbc - rangeMin] = 0x0167;
            mapDataStd[1][0xbd - rangeMin] = 0x014a;
            mapDataStd[1][0xbe - rangeMin] = 0x017e;
            mapDataStd[1][0xbf - rangeMin] = 0x014b;

            mapDataStd[1][0xc0 - rangeMin] = 0x0100;
            mapDataStd[1][0xc1 - rangeMin] = 0x00c1;
            mapDataStd[1][0xc2 - rangeMin] = 0x00c2;
            mapDataStd[1][0xc3 - rangeMin] = 0x00c3;
            mapDataStd[1][0xc4 - rangeMin] = 0x00c4;
            mapDataStd[1][0xc5 - rangeMin] = 0x00c5;
            mapDataStd[1][0xc6 - rangeMin] = 0x00c6;
            mapDataStd[1][0xc7 - rangeMin] = 0x012e;
            mapDataStd[1][0xc8 - rangeMin] = 0x010c;
            mapDataStd[1][0xc9 - rangeMin] = 0x00c9;
            mapDataStd[1][0xca - rangeMin] = 0x0118;
            mapDataStd[1][0xcb - rangeMin] = 0x00cb;
            mapDataStd[1][0xcc - rangeMin] = 0x0116;
            mapDataStd[1][0xcd - rangeMin] = 0x00cd;
            mapDataStd[1][0xce - rangeMin] = 0x00ce;
            mapDataStd[1][0xcf - rangeMin] = 0x012a;

            mapDataStd[1][0xd0 - rangeMin] = 0x0110;
            mapDataStd[1][0xd1 - rangeMin] = 0x0145;
            mapDataStd[1][0xd2 - rangeMin] = 0x014c;
            mapDataStd[1][0xd3 - rangeMin] = 0x0136;
            mapDataStd[1][0xd4 - rangeMin] = 0x00d4;
            mapDataStd[1][0xd5 - rangeMin] = 0x00d5;
            mapDataStd[1][0xd6 - rangeMin] = 0x00f6;
            mapDataStd[1][0xd7 - rangeMin] = 0x00d7;
            mapDataStd[1][0xd8 - rangeMin] = 0x00d8;
            mapDataStd[1][0xd9 - rangeMin] = 0x0172;
            mapDataStd[1][0xda - rangeMin] = 0x00da;
            mapDataStd[1][0xdb - rangeMin] = 0x00db;
            mapDataStd[1][0xdc - rangeMin] = 0x00dc;
            mapDataStd[1][0xdd - rangeMin] = 0x0168;
            mapDataStd[1][0xde - rangeMin] = 0x016a;
            mapDataStd[1][0xdf - rangeMin] = 0x00df;

            mapDataStd[1][0xe0 - rangeMin] = 0x0101;
            mapDataStd[1][0xe1 - rangeMin] = 0x00e1;
            mapDataStd[1][0xe2 - rangeMin] = 0x00e2;
            mapDataStd[1][0xe3 - rangeMin] = 0x00e3;
            mapDataStd[1][0xe4 - rangeMin] = 0x00e4;
            mapDataStd[1][0xe5 - rangeMin] = 0x00e5;
            mapDataStd[1][0xe6 - rangeMin] = 0x00e6;
            mapDataStd[1][0xe7 - rangeMin] = 0x012f;
            mapDataStd[1][0xe8 - rangeMin] = 0x010d;
            mapDataStd[1][0xe9 - rangeMin] = 0x00e9;
            mapDataStd[1][0xea - rangeMin] = 0x0119;
            mapDataStd[1][0xeb - rangeMin] = 0x00eb;
            mapDataStd[1][0xec - rangeMin] = 0x0117;
            mapDataStd[1][0xed - rangeMin] = 0x00ed;
            mapDataStd[1][0xee - rangeMin] = 0x00ee;
            mapDataStd[1][0xef - rangeMin] = 0x012b;

            mapDataStd[1][0xf0 - rangeMin] = 0x0111;
            mapDataStd[1][0xf1 - rangeMin] = 0x0146;
            mapDataStd[1][0xf2 - rangeMin] = 0x014d;
            mapDataStd[1][0xf3 - rangeMin] = 0x0137;
            mapDataStd[1][0xf4 - rangeMin] = 0x00f4;
            mapDataStd[1][0xf5 - rangeMin] = 0x00f5;
            mapDataStd[1][0xf6 - rangeMin] = 0x00f6;
            mapDataStd[1][0xf7 - rangeMin] = 0x00f7;
            mapDataStd[1][0xf8 - rangeMin] = 0x00f8;
            mapDataStd[1][0xf9 - rangeMin] = 0x0173;
            mapDataStd[1][0xfa - rangeMin] = 0x00fa;
            mapDataStd[1][0xfb - rangeMin] = 0x00fb;
            mapDataStd[1][0xfc - rangeMin] = 0x00fc;
            mapDataStd[1][0xfd - rangeMin] = 0x0169;
            mapDataStd[1][0xfe - rangeMin] = 0x016b;
            mapDataStd[1][0xff - rangeMin] = 0x02d9;

            //----------------------------------------------------------------//

            for (UInt16 i = 0; i < rangeSize; i++)
            {
                mapDataPCL [1] [i] = mapDataStd [1] [i];
            }

            mapDataPCL[1][0xa4 - rangeMin] = 0x20ac;

            //----------------------------------------------------------------//

            _sets.Add (new PCLSymSetMap (mapId,
                                         rangeCt,
                                         rangeData,
                                         mapDataStd,
                                         mapDataPCL));
        }
    }
}