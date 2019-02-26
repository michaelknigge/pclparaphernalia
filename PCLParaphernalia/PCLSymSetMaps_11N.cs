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
        // u n i c o d e M a p _ 1 1 N                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       11N                                                       //
        // Kind1    366                                                       //
        // Name     ISO 8859-6 Latin/Arabic                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_11N ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_11N;

            const Int32 rangeCt = 3;

            UInt16 [] [] rangeData = new UInt16 [rangeCt] []
            {
                new UInt16 [2] {0x20, 0x7f},
                new UInt16 [2] {0xa0, 0xbf},
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
                mapDataPCL[0][i] = mapDataStd[0][i];
            }

            mapDataPCL[0][0x2a - rangeMin] = 0x066d;
            mapDataPCL[0][0x5e - rangeMin] = 0x02c6;
            mapDataPCL[0][0x7e - rangeMin] = 0x02dc;

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
                mapDataStd [1] [i - rangeMin] = 0xffff;    //<not a character> //
            }

            mapDataStd [1] [0xa0 - rangeMin] = 0x00a0;
            mapDataStd [1] [0xa4 - rangeMin] = 0x00a4;
            mapDataStd [1] [0xac - rangeMin] = 0x060c;
            mapDataStd [1] [0xad - rangeMin] = 0x00ad;

            mapDataStd [1] [0xbb - rangeMin] = 0x061b;
            mapDataStd [1] [0xbf - rangeMin] = 0x061f;

            //----------------------------------------------------------------//

            for (UInt16 i = 0; i < rangeSize; i++)
            {
                mapDataPCL[1][i] = mapDataStd[1][i];
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Range 2                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            rangeMin = rangeData [2] [0];
            rangeMax = rangeData [2] [1];
            rangeSize = rangeSizes [2];

            offset = 0x0620 - 0xc0;             // 0x0620 - 0x00c0 = 0x0560

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [2] [i - rangeMin] = (UInt16) (offset + i);
            }

            mapDataStd [2] [0xdb - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [2] [0xdc - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [2] [0xdd - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [2] [0xde - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [2] [0xdf - rangeMin] = 0xffff;    //<not a character> //

            mapDataStd [2] [0xf3 - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [2] [0xf4 - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [2] [0xf5 - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [2] [0xf6 - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [2] [0xf7 - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [2] [0xf8 - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [2] [0xf9 - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [2] [0xfa - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [2] [0xfb - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [2] [0xfc - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [2] [0xfd - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [2] [0xfe - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [2] [0xff - rangeMin] = 0xffff;    //<not a character> //

            //----------------------------------------------------------------//

            for (UInt16 i = 0; i < rangeSize; i++)
            {
                mapDataPCL[2][i] = mapDataStd[2][i];
            }

            mapDataPCL[2][0xc0 - rangeMin] = 0xffff;    //<not a character> //
            mapDataPCL[2][0xc1 - rangeMin] = 0xfe80;
            mapDataPCL[2][0xc2 - rangeMin] = 0xfe81;
            mapDataPCL[2][0xc3 - rangeMin] = 0xfe83;
            mapDataPCL[2][0xc4 - rangeMin] = 0xef28;
            mapDataPCL[2][0xc5 - rangeMin] = 0xfe87;
            mapDataPCL[2][0xc6 - rangeMin] = 0xfe8a;
            mapDataPCL[2][0xc7 - rangeMin] = 0xfe8d;
            mapDataPCL[2][0xc8 - rangeMin] = 0xef03;
            mapDataPCL[2][0xc9 - rangeMin] = 0xef09;
            mapDataPCL[2][0xca - rangeMin] = 0xef04;
            mapDataPCL[2][0xcb - rangeMin] = 0xef05;
            mapDataPCL[2][0xcc - rangeMin] = 0xfe9d;
            mapDataPCL[2][0xcd - rangeMin] = 0xfea1;
            mapDataPCL[2][0xce - rangeMin] = 0xfea5;
            mapDataPCL[2][0xcf - rangeMin] = 0xef29;

            mapDataPCL[2][0xd0 - rangeMin] = 0xef2a;
            mapDataPCL[2][0xd1 - rangeMin] = 0xef2b;
            mapDataPCL[2][0xd2 - rangeMin] = 0xef2c;
            mapDataPCL[2][0xd3 - rangeMin] = 0xef0b;
            mapDataPCL[2][0xd4 - rangeMin] = 0xef0c;
            mapDataPCL[2][0xd5 - rangeMin] = 0xef37;
            mapDataPCL[2][0xd6 - rangeMin] = 0xef38;
            mapDataPCL[2][0xd7 - rangeMin] = 0xef32;
            mapDataPCL[2][0xd8 - rangeMin] = 0xef33;
            mapDataPCL[2][0xd9 - rangeMin] = 0xfec9;
            mapDataPCL[2][0xda - rangeMin] = 0xfecd;
            mapDataPCL[2][0xdb - rangeMin] = 0x0640;

            mapDataPCL[2][0xe0 - rangeMin] = 0xffff;    //<not a character> //
            mapDataPCL[2][0xe1 - rangeMin] = 0xef06;
            mapDataPCL[2][0xe2 - rangeMin] = 0xef27;
            mapDataPCL[2][0xe3 - rangeMin] = 0xef23;
            mapDataPCL[2][0xe4 - rangeMin] = 0xef24;
            mapDataPCL[2][0xe5 - rangeMin] = 0xef25;
            mapDataPCL[2][0xe6 - rangeMin] = 0xef26;
            mapDataPCL[2][0xe7 - rangeMin] = 0xfee9;
            mapDataPCL[2][0xe8 - rangeMin] = 0xef2d;
            mapDataPCL[2][0xe9 - rangeMin] = 0xfeef;
            mapDataPCL[2][0xea - rangeMin] = 0xfef1;
            mapDataPCL[2][0xeb - rangeMin] = 0xfe70;
            mapDataPCL[2][0xec - rangeMin] = 0xfe72;
            mapDataPCL[2][0xed - rangeMin] = 0xfe74;
            mapDataPCL[2][0xee - rangeMin] = 0xfe76;
            mapDataPCL[2][0xef - rangeMin] = 0xfe78;

            mapDataPCL[2][0xf0 - rangeMin] = 0xfe7a;
            mapDataPCL[2][0xf1 - rangeMin] = 0xfe7c;
            mapDataPCL[2][0xf2 - rangeMin] = 0xfe7e;

            //----------------------------------------------------------------//

            _sets.Add (new PCLSymSetMap (mapId,
                                         rangeCt,
                                         rangeData,
                                         mapDataStd,
                                         mapDataPCL));
        }
    }
}