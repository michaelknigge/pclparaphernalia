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
        // u n i c o d e M a p _ 8 H                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       8H                                                        //
        // Kind1    264                                                       //
        // Name     Hebrew-8                                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_8H ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_8H;

            const Int32 rangeCt = 3;

            UInt16 [] [] rangeData = new UInt16 [rangeCt] []
            {
                new UInt16 [2] {0x20, 0x7f},
                new UInt16 [2] {0xa0, 0xdf},
                new UInt16 [2] {0xe0, 0xff}
            };

            UInt16 [] rangeSizes = new UInt16 [rangeCt];

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
            mapDataPCL [0] [0x60 - rangeMin] = 0x2019;
            mapDataPCL [0] [0x7f - rangeMin] = 0x2592;

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

            mapDataPCL[1][0xa0 - rangeMin] = 0x0020;
            mapDataPCL[1][0xa1 - rangeMin] = 0x0021;
            mapDataPCL[1][0xa2 - rangeMin] = 0x0022;
            mapDataPCL[1][0xa3 - rangeMin] = 0x0023;
            mapDataPCL[1][0xa4 - rangeMin] = 0x0024;
            mapDataPCL[1][0xa5 - rangeMin] = 0x0025;
            mapDataPCL[1][0xa6 - rangeMin] = 0x0026;
            mapDataPCL[1][0xa7 - rangeMin] = 0x2019;
            mapDataPCL[1][0xa8 - rangeMin] = 0x0029;
            mapDataPCL[1][0xa9 - rangeMin] = 0x0028;
            mapDataPCL[1][0xaa - rangeMin] = 0x002a;
            mapDataPCL[1][0xab - rangeMin] = 0x002b;
            mapDataPCL[1][0xac - rangeMin] = 0x002c;
            mapDataPCL[1][0xad - rangeMin] = 0x002d;
            mapDataPCL[1][0xae - rangeMin] = 0x002e;
            mapDataPCL[1][0xaf - rangeMin] = 0x002f;

            mapDataPCL[1][0xba - rangeMin] = 0x003a;
            mapDataPCL[1][0xbb - rangeMin] = 0x003b;
            mapDataPCL[1][0xbc - rangeMin] = 0x003e;
            mapDataPCL[1][0xbd - rangeMin] = 0x003d;
            mapDataPCL[1][0xbe - rangeMin] = 0x003c;
            mapDataPCL[1][0xbf - rangeMin] = 0x003f;

            mapDataPCL[1][0xc0 - rangeMin] = 0x0040;

            mapDataPCL[1][0xd0 - rangeMin] = 0x2019;
            mapDataPCL[1][0xd1 - rangeMin] = 0x20aa;
            mapDataPCL[1][0xdb - rangeMin] = 0x005d;
            mapDataPCL[1][0xdc - rangeMin] = 0x005c;
            mapDataPCL[1][0xdd - rangeMin] = 0x005b;
            mapDataPCL[1][0xde - rangeMin] = 0x005e;
            mapDataPCL[1][0xdf - rangeMin] = 0x005f;

            //----------------------------------------------------------------//
            //                                                                //
            // Range 2                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            rangeMin = rangeData [2] [0];
            rangeMax = rangeData [2] [1];
            rangeSize = rangeSizes [2];

            offset = 0x05d0 - 0x00e0;     // 0x05d0 - 0x00e0 = 0x04f0

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataPCL[2][i - rangeMin] = (UInt16)(offset + i);
            }

            mapDataPCL [2] [0xfb - rangeMin] = 0x007d;
            mapDataPCL [2] [0xfc - rangeMin] = 0x007c;
            mapDataPCL [2] [0xfd - rangeMin] = 0x007b;
            mapDataPCL [2] [0xfe - rangeMin] = 0x007e;
            mapDataPCL [2] [0xff - rangeMin] = 0xffff;    //<not a character> //

            //----------------------------------------------------------------//

            _sets.Add (new PCLSymSetMap (mapId,
                                         rangeCt,
                                         rangeData,
                                         null,
                                         mapDataPCL));
        }
    }
}