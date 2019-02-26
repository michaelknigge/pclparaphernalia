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
        // u n i c o d e M a p _ 2 N                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       2N                                                        //
        // Kind1    78                                                        //
        // Name     ISO 8859-2 Latin 2                                        //
        //          Eastern European                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_2N ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_2N;

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

            mapDataPCL[0][0x5e - rangeMin] = 0x02c6;
            mapDataPCL[0][0x7e - rangeMin] = 0x02dc;
            mapDataPCL[0][0x7f - rangeMin] = 0x2592;

            //----------------------------------------------------------------//
            //                                                                //
            // Range 1                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            rangeMin = rangeData [1] [0];
            rangeMax = rangeData [1] [1];
            rangeSize = rangeSizes [1];

            mapDataStd [1] [0xa0 - rangeMin] = 0x00a0;
            mapDataStd [1] [0xa1 - rangeMin] = 0x0104;
            mapDataStd [1] [0xa2 - rangeMin] = 0x02d8;
            mapDataStd [1] [0xa3 - rangeMin] = 0x0141;
            mapDataStd [1] [0xa4 - rangeMin] = 0x00a4;
            mapDataStd [1] [0xa5 - rangeMin] = 0x013d;
            mapDataStd [1] [0xa6 - rangeMin] = 0x015a;
            mapDataStd [1] [0xa7 - rangeMin] = 0x00a7;
            mapDataStd [1] [0xa8 - rangeMin] = 0x00a8;
            mapDataStd [1] [0xa9 - rangeMin] = 0x0160;
            mapDataStd [1] [0xaa - rangeMin] = 0x015e;
            mapDataStd [1] [0xab - rangeMin] = 0x0164;
            mapDataStd [1] [0xac - rangeMin] = 0x0179;
            mapDataStd [1] [0xad - rangeMin] = 0x00ad;
            mapDataStd [1] [0xae - rangeMin] = 0x017d;
            mapDataStd [1] [0xaf - rangeMin] = 0x017b;

            mapDataStd [1] [0xb0 - rangeMin] = 0x00b0;
            mapDataStd [1] [0xb1 - rangeMin] = 0x0105;
            mapDataStd [1] [0xb2 - rangeMin] = 0x02db;
            mapDataStd [1] [0xb3 - rangeMin] = 0x0142;
            mapDataStd [1] [0xb4 - rangeMin] = 0x00b4;
            mapDataStd [1] [0xb5 - rangeMin] = 0x013e;
            mapDataStd [1] [0xb6 - rangeMin] = 0x015b;
            mapDataStd [1] [0xb7 - rangeMin] = 0x02c7;
            mapDataStd [1] [0xb8 - rangeMin] = 0x00b8;
            mapDataStd [1] [0xb9 - rangeMin] = 0x0161;
            mapDataStd [1] [0xba - rangeMin] = 0x015f;
            mapDataStd [1] [0xbb - rangeMin] = 0x0165;
            mapDataStd [1] [0xbc - rangeMin] = 0x017a;
            mapDataStd [1] [0xbd - rangeMin] = 0x02dd;
            mapDataStd [1] [0xbe - rangeMin] = 0x017e;
            mapDataStd [1] [0xbf - rangeMin] = 0x017c;

            mapDataStd [1] [0xc0 - rangeMin] = 0x0154;
            mapDataStd [1] [0xc1 - rangeMin] = 0x00c1;
            mapDataStd [1] [0xc2 - rangeMin] = 0x00c2;
            mapDataStd [1] [0xc3 - rangeMin] = 0x0102;
            mapDataStd [1] [0xc4 - rangeMin] = 0x00c4;
            mapDataStd [1] [0xc5 - rangeMin] = 0x0139;
            mapDataStd [1] [0xc6 - rangeMin] = 0x0106;
            mapDataStd [1] [0xc7 - rangeMin] = 0x00c7;
            mapDataStd [1] [0xc8 - rangeMin] = 0x010c;
            mapDataStd [1] [0xc9 - rangeMin] = 0x00c9;
            mapDataStd [1] [0xca - rangeMin] = 0x0118;
            mapDataStd [1] [0xcb - rangeMin] = 0x00cb;
            mapDataStd [1] [0xcc - rangeMin] = 0x011a;
            mapDataStd [1] [0xcd - rangeMin] = 0x00cd;
            mapDataStd [1] [0xce - rangeMin] = 0x00ce;
            mapDataStd [1] [0xcf - rangeMin] = 0x010e;

            mapDataStd [1] [0xd0 - rangeMin] = 0x0110;
            mapDataStd [1] [0xd1 - rangeMin] = 0x0143;
            mapDataStd [1] [0xd2 - rangeMin] = 0x0147;
            mapDataStd [1] [0xd3 - rangeMin] = 0x00d3;
            mapDataStd [1] [0xd4 - rangeMin] = 0x00d4;
            mapDataStd [1] [0xd5 - rangeMin] = 0x0150;
            mapDataStd [1] [0xd6 - rangeMin] = 0x00d6;
            mapDataStd [1] [0xd7 - rangeMin] = 0x00d7;
            mapDataStd [1] [0xd8 - rangeMin] = 0x0158;
            mapDataStd [1] [0xd9 - rangeMin] = 0x016e;
            mapDataStd [1] [0xda - rangeMin] = 0x00da;
            mapDataStd [1] [0xdb - rangeMin] = 0x0170;
            mapDataStd [1] [0xdc - rangeMin] = 0x00dc;
            mapDataStd [1] [0xdd - rangeMin] = 0x00dd;
            mapDataStd [1] [0xde - rangeMin] = 0x0162;
            mapDataStd [1] [0xdf - rangeMin] = 0x00df;

            mapDataStd [1] [0xe0 - rangeMin] = 0x0155;
            mapDataStd [1] [0xe1 - rangeMin] = 0x00e1;
            mapDataStd [1] [0xe2 - rangeMin] = 0x00e2;
            mapDataStd [1] [0xe3 - rangeMin] = 0x0103;
            mapDataStd [1] [0xe4 - rangeMin] = 0x00e4;
            mapDataStd [1] [0xe5 - rangeMin] = 0x013a;
            mapDataStd [1] [0xe6 - rangeMin] = 0x0107;
            mapDataStd [1] [0xe7 - rangeMin] = 0x00e7;
            mapDataStd [1] [0xe8 - rangeMin] = 0x010d;
            mapDataStd [1] [0xe9 - rangeMin] = 0x00e9;
            mapDataStd [1] [0xea - rangeMin] = 0x0119;
            mapDataStd [1] [0xeb - rangeMin] = 0x00eb;
            mapDataStd [1] [0xec - rangeMin] = 0x011b;
            mapDataStd [1] [0xed - rangeMin] = 0x00ed;
            mapDataStd [1] [0xee - rangeMin] = 0x00ee;
            mapDataStd [1] [0xef - rangeMin] = 0x010f;

            mapDataStd [1] [0xf0 - rangeMin] = 0x0111;
            mapDataStd [1] [0xf1 - rangeMin] = 0x0144;
            mapDataStd [1] [0xf2 - rangeMin] = 0x0148;
            mapDataStd [1] [0xf3 - rangeMin] = 0x00f3;
            mapDataStd [1] [0xf4 - rangeMin] = 0x00f4;
            mapDataStd [1] [0xf5 - rangeMin] = 0x0151;
            mapDataStd [1] [0xf6 - rangeMin] = 0x00f6;
            mapDataStd [1] [0xf7 - rangeMin] = 0x00f7;
            mapDataStd [1] [0xf8 - rangeMin] = 0x0159;
            mapDataStd [1] [0xf9 - rangeMin] = 0x016f;
            mapDataStd [1] [0xfa - rangeMin] = 0x00fa;
            mapDataStd [1] [0xfb - rangeMin] = 0x0171;
            mapDataStd [1] [0xfc - rangeMin] = 0x00fc;
            mapDataStd [1] [0xfd - rangeMin] = 0x00fd;
            mapDataStd [1] [0xfe - rangeMin] = 0x0163;
            mapDataStd [1] [0xff - rangeMin] = 0x02d9;

            //----------------------------------------------------------------//

            for (UInt16 i = 0; i < rangeSize; i++)
            {
                mapDataPCL [1] [i] = mapDataStd [1] [i];
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