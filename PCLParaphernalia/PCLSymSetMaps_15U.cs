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
        // u n i c o d e M a p _ 1 5 U                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       15U                                                       //
        // Kind1    501                                                       //
        // Name     Pi Font                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_15U ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_15U;

            const Int32 rangeCt = 1;

            UInt16 [] [] rangeData = new UInt16 [rangeCt] []
            {
                new UInt16 [2] {0x20, 0x7f}
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
            mapDataPCL[0][0x22 - rangeMin] = 0x201e;
            mapDataPCL[0][0x23 - rangeMin] = 0x201a;
            mapDataPCL[0][0x24 - rangeMin] = 0x201c;
            mapDataPCL[0][0x25 - rangeMin] = 0x201d;
            mapDataPCL[0][0x26 - rangeMin] = 0x2018;
            mapDataPCL[0][0x27 - rangeMin] = 0x2019;
            mapDataPCL[0][0x28 - rangeMin] = 0x2329;
            mapDataPCL[0][0x29 - rangeMin] = 0x232a;
            mapDataPCL[0][0x2a - rangeMin] = 0x2122;
            mapDataPCL[0][0x2b - rangeMin] = 0x2120;
            mapDataPCL[0][0x2c - rangeMin] = 0x00ae;
            mapDataPCL[0][0x2d - rangeMin] = 0x00a9;
            mapDataPCL[0][0x2e - rangeMin] = 0xefff;

            mapDataPCL[0][0x30 - rangeMin] = 0x02c9;
            mapDataPCL[0][0x31 - rangeMin] = 0x02d8;
            mapDataPCL[0][0x32 - rangeMin] = 0x02c7;
            mapDataPCL[0][0x33 - rangeMin] = 0x02d9;
            mapDataPCL[0][0x34 - rangeMin] = 0x2197;
            mapDataPCL[0][0x35 - rangeMin] = 0x2198;
            mapDataPCL[0][0x36 - rangeMin] = 0x2199;
            mapDataPCL[0][0x37 - rangeMin] = 0x2196;
            mapDataPCL[0][0x38 - rangeMin] = 0x25b5;
            mapDataPCL[0][0x39 - rangeMin] = 0x25b9;
            mapDataPCL[0][0x3a - rangeMin] = 0x25bf;
            mapDataPCL[0][0x3b - rangeMin] = 0x25c3;
            mapDataPCL[0][0x3c - rangeMin] = 0x226a;
            mapDataPCL[0][0x3d - rangeMin] = 0x00a7;
            mapDataPCL[0][0x3e - rangeMin] = 0x226b;
            mapDataPCL[0][0x3f - rangeMin] = 0x00b6;

            mapDataPCL[0][0x40 - rangeMin] = 0x2237;
            mapDataPCL[0][0x41 - rangeMin] = 0xefca;
            mapDataPCL[0][0x46 - rangeMin] = 0xefd5;
            mapDataPCL[0][0x48 - rangeMin] = 0x210f;
            mapDataPCL[0][0x4c - rangeMin] = 0x2112;
            mapDataPCL[0][0x4d - rangeMin] = 0x2113;

            mapDataPCL[0][0x50 - rangeMin] = 0xeffe;
            mapDataPCL[0][0x51 - rangeMin] = 0x2118;
            mapDataPCL[0][0x52 - rangeMin] = 0x211e;
            mapDataPCL[0][0x53 - rangeMin] = 0x2211;
            mapDataPCL[0][0x5b - rangeMin] = 0x301a;
            mapDataPCL[0][0x5c - rangeMin] = 0xefc9;
            mapDataPCL[0][0x5d - rangeMin] = 0x301b;
            mapDataPCL[0][0x5e - rangeMin] = 0x2039;
            mapDataPCL[0][0x5f - rangeMin] = 0x203a;

            mapDataPCL[0][0x60 - rangeMin] = 0x250c;
            mapDataPCL[0][0x61 - rangeMin] = 0x2514;
            mapDataPCL[0][0x62 - rangeMin] = 0x256d;
            mapDataPCL[0][0x63 - rangeMin] = 0x2570;
            mapDataPCL[0][0x64 - rangeMin] = 0x253c;
            mapDataPCL[0][0x65 - rangeMin] = 0x251c;
            mapDataPCL[0][0x66 - rangeMin] = 0x2500;
            mapDataPCL[0][0x67 - rangeMin] = 0xefc8;
            mapDataPCL[0][0x68 - rangeMin] = 0xefc7;
            mapDataPCL[0][0x69 - rangeMin] = 0xefc6;
            mapDataPCL[0][0x6a - rangeMin] = 0xefc5;
            mapDataPCL[0][0x6b - rangeMin] = 0xefc4;
            mapDataPCL[0][0x6c - rangeMin] = 0xeffc;
            mapDataPCL[0][0x6d - rangeMin] = 0x25c7;

            mapDataPCL[0][0x70 - rangeMin] = 0x2510;
            mapDataPCL[0][0x71 - rangeMin] = 0x2518;
            mapDataPCL[0][0x72 - rangeMin] = 0x256e;
            mapDataPCL[0][0x73 - rangeMin] = 0x256f;
            mapDataPCL[0][0x74 - rangeMin] = 0x252c;
            mapDataPCL[0][0x75 - rangeMin] = 0x2524;
            mapDataPCL[0][0x76 - rangeMin] = 0x2534;
            mapDataPCL[0][0x77 - rangeMin] = 0x2502;
            mapDataPCL[0][0x78 - rangeMin] = 0xefc3;
            mapDataPCL[0][0x79 - rangeMin] = 0xefc2;
            mapDataPCL[0][0x7a - rangeMin] = 0xefc1;
            mapDataPCL[0][0x7b - rangeMin] = 0xefc0;
            mapDataPCL[0][0x7c - rangeMin] = 0xeffb;
            mapDataPCL[0][0x7d - rangeMin] = 0x25c6;
            mapDataPCL[0][0x7f - rangeMin] = 0x2592;

            //----------------------------------------------------------------//

            _sets.Add (new PCLSymSetMap (mapId,
                                         rangeCt,
                                         rangeData,
                                         null,
                                         mapDataPCL));
        }
    }
}