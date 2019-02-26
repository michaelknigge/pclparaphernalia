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
        // u n i c o d e M a p _ 9 V                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       9V                                                        //
        // Kind1    310                                                       //
        // Name     Windows Latin/Arabic                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_9V ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_9V;

            const Int32 rangeCt = 3;

            UInt16 [] [] rangeData = new UInt16 [rangeCt] []
            {
                new UInt16 [2] {0x10, 0x1f},
                new UInt16 [2] {0x20, 0x7f},
                new UInt16 [2] {0x80, 0xff}
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
                mapDataPCL[0][i - rangeMin] = i;
            }

            //----------------------------------------------------------------//

            mapDataPCL [0] [0x10 - rangeMin] = 0xef1a;
            mapDataPCL [0] [0x11 - rangeMin] = 0xef1b;
            mapDataPCL [0] [0x12 - rangeMin] = 0xef1c;
            mapDataPCL [0] [0x13 - rangeMin] = 0xef1d;
            mapDataPCL [0] [0x14 - rangeMin] = 0xef1e;
            mapDataPCL [0] [0x15 - rangeMin] = 0xef1f;
            mapDataPCL [0] [0x16 - rangeMin] = 0xef20;
            mapDataPCL [0] [0x17 - rangeMin] = 0xef21;
            mapDataPCL [0] [0x18 - rangeMin] = 0xef22;
            mapDataPCL [0] [0x19 - rangeMin] = 0xfeca;
            mapDataPCL [0] [0x1a - rangeMin] = 0xfedf;
            mapDataPCL [0] [0x1b - rangeMin] = 0xffff;    //<not a character> //
            mapDataPCL [0] [0x1c - rangeMin] = 0xfefc;
            mapDataPCL [0] [0x1d - rangeMin] = 0xfeea;
            mapDataPCL [0] [0x1e - rangeMin] = 0xffff;    //<not a character> //
            mapDataPCL [0] [0x1f - rangeMin] = 0xffff;    //<not a character> //

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
                mapDataPCL [1] [i - rangeMin] = i;
            }

            mapDataPCL [1] [0x2a - rangeMin] = 0x066d;
            mapDataPCL [1] [0x5e - rangeMin] = 0x02c6;
            mapDataPCL [1] [0x7e - rangeMin] = 0x02dc;
            mapDataPCL [1] [0x7f - rangeMin] = 0xfea7;

            //----------------------------------------------------------------//
            //                                                                //
            // Range 2                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            rangeMin = rangeData [2] [0];
            rangeMax = rangeData [2] [1];
            rangeSize = rangeSizes [2];

            mapDataPCL [2] [0x80 - rangeMin] = 0xfef8;
            mapDataPCL [2] [0x81 - rangeMin] = 0xef0d;
            mapDataPCL [2] [0x82 - rangeMin] = 0xef0e;
            mapDataPCL [2] [0x83 - rangeMin] = 0xfd3c;
            mapDataPCL [2] [0x84 - rangeMin] = 0xef0f;
            mapDataPCL [2] [0x85 - rangeMin] = 0xef10;
            mapDataPCL [2] [0x86 - rangeMin] = 0xef11;
            mapDataPCL [2] [0x87 - rangeMin] = 0xef12;
            mapDataPCL [2] [0x88 - rangeMin] = 0xef13;
            mapDataPCL [2] [0x89 - rangeMin] = 0xef14;
            mapDataPCL [2] [0x8a - rangeMin] = 0xef15;
            mapDataPCL [2] [0x8b - rangeMin] = 0xef16;
            mapDataPCL [2] [0x8c - rangeMin] = 0xef17;
            mapDataPCL [2] [0x8d - rangeMin] = 0xef18;
            mapDataPCL [2] [0x8e - rangeMin] = 0xfd3d;
            mapDataPCL [2] [0x8f - rangeMin] = 0xef19;

            mapDataPCL [2] [0x90 - rangeMin] = 0xfe9d;
            mapDataPCL [2] [0x91 - rangeMin] = 0xfea1;
            mapDataPCL [2] [0x92 - rangeMin] = 0xfea5;
            mapDataPCL [2] [0x93 - rangeMin] = 0xfe94;
            mapDataPCL [2] [0x94 - rangeMin] = 0xfe8e;
            mapDataPCL [2] [0x95 - rangeMin] = 0xfef2;
            mapDataPCL [2] [0x96 - rangeMin] = 0xfe8a;
            mapDataPCL [2] [0x97 - rangeMin] = 0xfef0;
            mapDataPCL [2] [0x98 - rangeMin] = 0xfef7;
            mapDataPCL [2] [0x99 - rangeMin] = 0xfef5;
            mapDataPCL [2] [0x9a - rangeMin] = 0xfef9;
            mapDataPCL [2] [0x9b - rangeMin] = 0xfefb;
            mapDataPCL [2] [0x9c - rangeMin] = 0xfee0;
            mapDataPCL [2] [0x9d - rangeMin] = 0xfe84;
            mapDataPCL [2] [0x9e - rangeMin] = 0xfe82;
            mapDataPCL [2] [0x9f - rangeMin] = 0xfe88;

            mapDataPCL [2] [0xa0 - rangeMin] = 0x00a0;
            mapDataPCL [2] [0xa1 - rangeMin] = 0xef42;
            mapDataPCL [2] [0xa2 - rangeMin] = 0xef43;
            mapDataPCL [2] [0xa3 - rangeMin] = 0xef44;
            mapDataPCL [2] [0xa4 - rangeMin] = 0xef45;
            mapDataPCL [2] [0xa5 - rangeMin] = 0xef46;
            mapDataPCL [2] [0xa6 - rangeMin] = 0xef47;
            mapDataPCL [2] [0xa7 - rangeMin] = 0xef48;
            mapDataPCL [2] [0xa8 - rangeMin] = 0xef49;
            mapDataPCL [2] [0xa9 - rangeMin] = 0xfcf2;
            mapDataPCL [2] [0xaa - rangeMin] = 0xfe9f;
            mapDataPCL [2] [0xab - rangeMin] = 0xfea3;
            mapDataPCL [2] [0xac - rangeMin] = 0x060c;
            mapDataPCL [2] [0xad - rangeMin] = 0xfecc;
            mapDataPCL [2] [0xae - rangeMin] = 0xfed0;
            mapDataPCL [2] [0xaf - rangeMin] = 0xfeec;

            mapDataPCL [2] [0xb0 - rangeMin] = 0x0660;
            mapDataPCL [2] [0xb1 - rangeMin] = 0x0661;
            mapDataPCL [2] [0xb2 - rangeMin] = 0x0662;
            mapDataPCL [2] [0xb3 - rangeMin] = 0x0663;
            mapDataPCL [2] [0xb4 - rangeMin] = 0x0664;
            mapDataPCL [2] [0xb5 - rangeMin] = 0x0665;
            mapDataPCL [2] [0xb6 - rangeMin] = 0x0666;
            mapDataPCL [2] [0xb7 - rangeMin] = 0x0667;
            mapDataPCL [2] [0xb8 - rangeMin] = 0x0668;
            mapDataPCL [2] [0xb9 - rangeMin] = 0x0669;
            mapDataPCL [2] [0xba - rangeMin] = 0xfece;
            mapDataPCL [2] [0xbb - rangeMin] = 0x061b;
            mapDataPCL [2] [0xbc - rangeMin] = 0xef40;
            mapDataPCL [2] [0xbd - rangeMin] = 0xef41;
            mapDataPCL [2] [0xbe - rangeMin] = 0xfefa;
            mapDataPCL [2] [0xbf - rangeMin] = 0x061f;

            mapDataPCL [2] [0xc0 - rangeMin] = 0xfef6;
            mapDataPCL [2] [0xc1 - rangeMin] = 0xfe80;
            mapDataPCL [2] [0xc2 - rangeMin] = 0xfe81;
            mapDataPCL [2] [0xc3 - rangeMin] = 0xfe83;
            mapDataPCL [2] [0xc4 - rangeMin] = 0xef28;
            mapDataPCL [2] [0xc5 - rangeMin] = 0xfe87;
            mapDataPCL [2] [0xc6 - rangeMin] = 0xef34;
            mapDataPCL [2] [0xc7 - rangeMin] = 0xfe8d;
            mapDataPCL [2] [0xc8 - rangeMin] = 0xef39;
            mapDataPCL [2] [0xc9 - rangeMin] = 0xfe93;
            mapDataPCL [2] [0xca - rangeMin] = 0xef3a;
            mapDataPCL [2] [0xcb - rangeMin] = 0xef3b;
            mapDataPCL [2] [0xcc - rangeMin] = 0xef3d;
            mapDataPCL [2] [0xcd - rangeMin] = 0xef3e;
            mapDataPCL [2] [0xce - rangeMin] = 0xef3f;
            mapDataPCL [2] [0xcf - rangeMin] = 0xef29;

            mapDataPCL [2] [0xd0 - rangeMin] = 0xef2a;
            mapDataPCL [2] [0xd1 - rangeMin] = 0xef2b;
            mapDataPCL [2] [0xd2 - rangeMin] = 0xef2c;
            mapDataPCL [2] [0xd3 - rangeMin] = 0xef35;
            mapDataPCL [2] [0xd4 - rangeMin] = 0xef36;
            mapDataPCL [2] [0xd5 - rangeMin] = 0xef37;
            mapDataPCL [2] [0xd6 - rangeMin] = 0xef38;
            mapDataPCL [2] [0xd7 - rangeMin] = 0xef32;
            mapDataPCL [2] [0xd8 - rangeMin] = 0xef33;
            mapDataPCL [2] [0xd9 - rangeMin] = 0xfec9;
            mapDataPCL [2] [0xda - rangeMin] = 0xfecd;
            mapDataPCL [2] [0xdb - rangeMin] = 0xfe9e;
            mapDataPCL [2] [0xdc - rangeMin] = 0xfea2;
            mapDataPCL [2] [0xdd - rangeMin] = 0xfea6;
            mapDataPCL [2] [0xde - rangeMin] = 0xfe7d;
            mapDataPCL [2] [0xdf - rangeMin] = 0xfe89;

            mapDataPCL [2] [0xe0 - rangeMin] = 0x0640;
            mapDataPCL [2] [0xe1 - rangeMin] = 0xef3c;
            mapDataPCL [2] [0xe2 - rangeMin] = 0xef27;
            mapDataPCL [2] [0xe3 - rangeMin] = 0xef23;
            mapDataPCL [2] [0xe4 - rangeMin] = 0xef24;
            mapDataPCL [2] [0xe5 - rangeMin] = 0xef25;
            mapDataPCL [2] [0xe6 - rangeMin] = 0xef26;
            mapDataPCL [2] [0xe7 - rangeMin] = 0xfee9;
            mapDataPCL [2] [0xe8 - rangeMin] = 0xef2d;
            mapDataPCL [2] [0xe9 - rangeMin] = 0xfeef;
            mapDataPCL [2] [0xea - rangeMin] = 0xfef1;
            mapDataPCL [2] [0xeb - rangeMin] = 0xfe70;
            mapDataPCL [2] [0xec - rangeMin] = 0xfe72;
            mapDataPCL [2] [0xed - rangeMin] = 0xfe74;
            mapDataPCL [2] [0xee - rangeMin] = 0xfe76;
            mapDataPCL [2] [0xef - rangeMin] = 0xfe78;

            mapDataPCL [2] [0xf0 - rangeMin] = 0xfe7a;
            mapDataPCL [2] [0xf1 - rangeMin] = 0xfe7c;
            mapDataPCL [2] [0xf2 - rangeMin] = 0xfe7e;
            mapDataPCL [2] [0xf3 - rangeMin] = 0xfecb;
            mapDataPCL [2] [0xf4 - rangeMin] = 0xfecf;
            mapDataPCL [2] [0xf5 - rangeMin] = 0xfeeb;
            mapDataPCL [2] [0xf6 - rangeMin] = 0xfc60;
            mapDataPCL [2] [0xf7 - rangeMin] = 0xfc61;
            mapDataPCL [2] [0xf8 - rangeMin] = 0xfc62;
            mapDataPCL [2] [0xf9 - rangeMin] = 0xfcf3;
            mapDataPCL [2] [0xfa - rangeMin] = 0xfcf4;
            mapDataPCL [2] [0xfb - rangeMin] = 0xfe77;
            mapDataPCL [2] [0xfc - rangeMin] = 0xfe79;
            mapDataPCL [2] [0xfd - rangeMin] = 0xfe7b;
            mapDataPCL [2] [0xfe - rangeMin] = 0xfe7f;
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