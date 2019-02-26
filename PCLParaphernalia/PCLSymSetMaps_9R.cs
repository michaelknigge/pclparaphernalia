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
        // u n i c o d e M a p _ 9 R                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       9R                                                        //
        // Kind1    306                                                       //
        // Name     Windows Latin/Cyrillic                                    //
        //          Code Page 1251                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_9R ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_9R;

            const Int32 rangeCt = 3;

            UInt16 [] [] rangeData = new UInt16 [rangeCt] []
            {
                new UInt16 [2] {0x20, 0x7f},
                new UInt16 [2] {0x80, 0xbf},
                new UInt16 [2] {0xc0, 0xff}
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

            mapDataPCL [0] [0x5e - rangeMin] = 0x02c6;
            mapDataPCL [0] [0x7e - rangeMin] = 0x02dc;
            mapDataPCL [0] [0x7f - rangeMin] = 0x2592;

            //----------------------------------------------------------------//
            //                                                                //
            // Range 1                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            rangeMin = rangeData [1] [0];
            rangeMax = rangeData [1] [1];
            rangeSize = rangeSizes [1];

            mapDataStd [1] [0x80 - rangeMin] = 0x0402;
            mapDataStd [1] [0x81 - rangeMin] = 0x0403;
            mapDataStd [1] [0x82 - rangeMin] = 0x201a;
            mapDataStd [1] [0x83 - rangeMin] = 0x0453;
            mapDataStd [1] [0x84 - rangeMin] = 0x201e;
            mapDataStd [1] [0x85 - rangeMin] = 0x2026;
            mapDataStd [1] [0x86 - rangeMin] = 0x2020;
            mapDataStd [1] [0x87 - rangeMin] = 0x2021;
            mapDataStd [1] [0x88 - rangeMin] = 0x20ac;
            mapDataStd [1] [0x89 - rangeMin] = 0x2030;
            mapDataStd [1] [0x8a - rangeMin] = 0x0409;
            mapDataStd [1] [0x8b - rangeMin] = 0x2039;
            mapDataStd [1] [0x8c - rangeMin] = 0x040a;
            mapDataStd [1] [0x8d - rangeMin] = 0x040c;
            mapDataStd [1] [0x8e - rangeMin] = 0x040b;
            mapDataStd [1] [0x8f - rangeMin] = 0x040f;

            mapDataStd [1] [0x90 - rangeMin] = 0x0452;
            mapDataStd [1] [0x91 - rangeMin] = 0x2018;
            mapDataStd [1] [0x92 - rangeMin] = 0x2019;
            mapDataStd [1] [0x93 - rangeMin] = 0x201c;
            mapDataStd [1] [0x94 - rangeMin] = 0x201d;
            mapDataStd [1] [0x95 - rangeMin] = 0x2022;
            mapDataStd [1] [0x96 - rangeMin] = 0x2013;
            mapDataStd [1] [0x97 - rangeMin] = 0x2014;
            mapDataStd [1] [0x98 - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [1] [0x99 - rangeMin] = 0x2122;
            mapDataStd [1] [0x9a - rangeMin] = 0x0459;
            mapDataStd [1] [0x9b - rangeMin] = 0x203a;
            mapDataStd [1] [0x9c - rangeMin] = 0x045a;
            mapDataStd [1] [0x9d - rangeMin] = 0x045c;
            mapDataStd [1] [0x9e - rangeMin] = 0x045b;
            mapDataStd [1] [0x9f - rangeMin] = 0x045f;

            mapDataStd [1] [0xa0 - rangeMin] = 0x00a0;
            mapDataStd [1] [0xa1 - rangeMin] = 0x040e;
            mapDataStd [1] [0xa2 - rangeMin] = 0x045e;
            mapDataStd [1] [0xa3 - rangeMin] = 0x0408;
            mapDataStd [1] [0xa4 - rangeMin] = 0x00a4;
            mapDataStd [1] [0xa5 - rangeMin] = 0x0490;
            mapDataStd [1] [0xa6 - rangeMin] = 0x00a6;
            mapDataStd [1] [0xa7 - rangeMin] = 0x00a7;
            mapDataStd [1] [0xa8 - rangeMin] = 0x0401;
            mapDataStd [1] [0xa9 - rangeMin] = 0x00a9;
            mapDataStd [1] [0xaa - rangeMin] = 0x0404;
            mapDataStd [1] [0xab - rangeMin] = 0x00ab;
            mapDataStd [1] [0xac - rangeMin] = 0x00ac;
            mapDataStd [1] [0xad - rangeMin] = 0x00ad;
            mapDataStd [1] [0xae - rangeMin] = 0x00ae;
            mapDataStd [1] [0xaf - rangeMin] = 0x0407;

            mapDataStd [1] [0xb0 - rangeMin] = 0x00b0;
            mapDataStd [1] [0xb1 - rangeMin] = 0x00b1;
            mapDataStd [1] [0xb2 - rangeMin] = 0x0406;
            mapDataStd [1] [0xb3 - rangeMin] = 0x0456;
            mapDataStd [1] [0xb4 - rangeMin] = 0x0491;
            mapDataStd [1] [0xb5 - rangeMin] = 0x00b5;
            mapDataStd [1] [0xb6 - rangeMin] = 0x00b6;
            mapDataStd [1] [0xb7 - rangeMin] = 0x00b7;
            mapDataStd [1] [0xb8 - rangeMin] = 0x0451;
            mapDataStd [1] [0xb9 - rangeMin] = 0x2116;
            mapDataStd [1] [0xba - rangeMin] = 0x0454;
            mapDataStd [1] [0xbb - rangeMin] = 0x00bb;
            mapDataStd [1] [0xbc - rangeMin] = 0x0458;
            mapDataStd [1] [0xbd - rangeMin] = 0x0405;
            mapDataStd [1] [0xbe - rangeMin] = 0x0455;
            mapDataStd [1] [0xbf - rangeMin] = 0x0457;

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

            offset = 0x0410 - 0x00c0;     // 0x0410 - 0x00c0 = 0x0350

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd[2][i - rangeMin] = (UInt16)(offset + i);
            }

            //----------------------------------------------------------------//

            for (UInt16 i = 0; i < rangeSize; i++)
            {
                mapDataPCL [2] [i] = mapDataStd [2] [i];
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