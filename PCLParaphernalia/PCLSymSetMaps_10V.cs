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
        // u n i c o d e M a p _ 1 0 V                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       10V                                                       //
        // Kind1    342                                                       //
        // Name     PC-864 Latin/Arabic                                       //
        //                                                                    //
        // Note that the 'Laserjet' table (as per output on LaserJet M553x,   //
        // M475dn and 1320n0 is quite different from the 'strict' table (as   //
        // defined by Wikipedia / Unicode.                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_10V ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_10V;

            const Int32 rangeCt = 3;

            UInt16 [] [] rangeData = new UInt16 [rangeCt] []
            {
                new UInt16 [2] {0x01, 0x1f},
                new UInt16 [2] {0x20, 0x7f},
                new UInt16 [2] {0x80, 0xff}
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
                mapDataStd[0][i - rangeMin] = i;
            }

            //----------------------------------------------------------------//

            mapDataPCL [0] [0x01 - rangeMin] = 0x263a;
            mapDataPCL [0] [0x02 - rangeMin] = 0x266a;
            mapDataPCL [0] [0x03 - rangeMin] = 0x266b;
            mapDataPCL [0] [0x04 - rangeMin] = 0x263c;
            mapDataPCL [0] [0x05 - rangeMin] = 0x2550;
            mapDataPCL [0] [0x06 - rangeMin] = 0x2551;
            mapDataPCL [0] [0x07 - rangeMin] = 0x256c;
            mapDataPCL [0] [0x08 - rangeMin] = 0x2563;
            mapDataPCL [0] [0x09 - rangeMin] = 0x2566;
            mapDataPCL [0] [0x0a - rangeMin] = 0x2560;
            mapDataPCL [0] [0x0b - rangeMin] = 0x2569;
            mapDataPCL [0] [0x0c - rangeMin] = 0x2557;
            mapDataPCL [0] [0x0d - rangeMin] = 0x2554;
            mapDataPCL [0] [0x0e - rangeMin] = 0x255a;
            mapDataPCL [0] [0x0f - rangeMin] = 0x255d;

            mapDataPCL [0] [0x10 - rangeMin] = 0x25ba;
            mapDataPCL [0] [0x11 - rangeMin] = 0x25c4;
            mapDataPCL [0] [0x12 - rangeMin] = 0x2195;
            mapDataPCL [0] [0x13 - rangeMin] = 0x203c;
            mapDataPCL [0] [0x14 - rangeMin] = 0x00b6;
            mapDataPCL [0] [0x15 - rangeMin] = 0x00a7;
            mapDataPCL [0] [0x16 - rangeMin] = 0x25ac;
            mapDataPCL [0] [0x17 - rangeMin] = 0x21a8;
            mapDataPCL [0] [0x18 - rangeMin] = 0x2191;
            mapDataPCL [0] [0x19 - rangeMin] = 0x2193;
            mapDataPCL [0] [0x1a - rangeMin] = 0x2192;
            mapDataPCL [0] [0x1b - rangeMin] = 0x2190;
            mapDataPCL [0] [0x1c - rangeMin] = 0x221f;
            mapDataPCL [0] [0x1d - rangeMin] = 0x2194;
            mapDataPCL [0] [0x1e - rangeMin] = 0x25b2;
            mapDataPCL [0] [0x1f - rangeMin] = 0x25bc;

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

            mapDataStd [1] [0x25 - rangeMin] = 0x066a;

            //----------------------------------------------------------------//

            for (UInt16 i = 0; i < rangeSize; i++)
            {
                mapDataPCL [1] [i] = mapDataStd [1] [i];
            }

            mapDataPCL [1] [0x2a - rangeMin] = 0x066d;
            mapDataPCL [1] [0x5e - rangeMin] = 0x02c6;
            mapDataPCL [1] [0x7e - rangeMin] = 0x02dc;
            mapDataPCL [1] [0x7f - rangeMin] = 0x2302;

            //----------------------------------------------------------------//
            //                                                                //
            // Range 2                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            rangeMin = rangeData [2] [0];
            rangeMax = rangeData [2] [1];
            rangeSize = rangeSizes [2];

            mapDataStd [2] [0x80 - rangeMin] = 0x00b0;
            mapDataStd [2] [0x81 - rangeMin] = 0x00b7;
            mapDataStd [2] [0x82 - rangeMin] = 0x2219;
            mapDataStd [2] [0x83 - rangeMin] = 0x221a;
            mapDataStd [2] [0x84 - rangeMin] = 0x2592;
            mapDataStd [2] [0x85 - rangeMin] = 0x2500;
            mapDataStd [2] [0x86 - rangeMin] = 0x2502;
            mapDataStd [2] [0x87 - rangeMin] = 0x253c;
            mapDataStd [2] [0x88 - rangeMin] = 0x2524;
            mapDataStd [2] [0x89 - rangeMin] = 0x252c;
            mapDataStd [2] [0x8a - rangeMin] = 0x251c;
            mapDataStd [2] [0x8b - rangeMin] = 0x2534;
            mapDataStd [2] [0x8c - rangeMin] = 0x2510;
            mapDataStd [2] [0x8d - rangeMin] = 0x250c;
            mapDataStd [2] [0x8e - rangeMin] = 0x2514;
            mapDataStd [2] [0x8f - rangeMin] = 0x2518;

            mapDataStd [2] [0x90 - rangeMin] = 0x03b2;
            mapDataStd [2] [0x91 - rangeMin] = 0x221e;
            mapDataStd [2] [0x92 - rangeMin] = 0x03c6;
            mapDataStd [2] [0x93 - rangeMin] = 0x00b1;
            mapDataStd [2] [0x94 - rangeMin] = 0x00bd;
            mapDataStd [2] [0x95 - rangeMin] = 0x00bc;
            mapDataStd [2] [0x96 - rangeMin] = 0x2248;
            mapDataStd [2] [0x97 - rangeMin] = 0x00ab;
            mapDataStd [2] [0x98 - rangeMin] = 0x00bb;
            mapDataStd [2] [0x99 - rangeMin] = 0xfef7;
            mapDataStd [2] [0x9a - rangeMin] = 0xfef8;
            mapDataStd [2] [0x9b - rangeMin] = 0xffff;
            mapDataStd [2] [0x9c - rangeMin] = 0xffff;
            mapDataStd [2] [0x9d - rangeMin] = 0xfefb;
            mapDataStd [2] [0x9e - rangeMin] = 0xfefc;
            mapDataStd [2] [0x9f - rangeMin] = 0xffff;

            mapDataStd [2] [0xa0 - rangeMin] = 0x00a0;
            mapDataStd [2] [0xa1 - rangeMin] = 0x00ad;
            mapDataStd [2] [0xa2 - rangeMin] = 0xfe82;
            mapDataStd [2] [0xa3 - rangeMin] = 0x00a3;
            mapDataStd [2] [0xa4 - rangeMin] = 0x00a4;
            mapDataStd [2] [0xa5 - rangeMin] = 0xfe84;
            mapDataStd [2] [0xa6 - rangeMin] = 0xffff;
            mapDataStd [2] [0xa7 - rangeMin] = 0x20ac;
            mapDataStd [2] [0xa8 - rangeMin] = 0xfe8e;
            mapDataStd [2] [0xa9 - rangeMin] = 0xfe8f;
            mapDataStd [2] [0xaa - rangeMin] = 0xfe95;
            mapDataStd [2] [0xab - rangeMin] = 0xfe99;
            mapDataStd [2] [0xac - rangeMin] = 0x060c;
            mapDataStd [2] [0xad - rangeMin] = 0xfe9d;
            mapDataStd [2] [0xae - rangeMin] = 0xfea1;
            mapDataStd [2] [0xaf - rangeMin] = 0xfea5;

            mapDataStd [2] [0xb0 - rangeMin] = 0x0660;
            mapDataStd [2] [0xb1 - rangeMin] = 0x0661;
            mapDataStd [2] [0xb2 - rangeMin] = 0x0662;
            mapDataStd [2] [0xb3 - rangeMin] = 0x0663;
            mapDataStd [2] [0xb4 - rangeMin] = 0x0664;
            mapDataStd [2] [0xb5 - rangeMin] = 0x0665;
            mapDataStd [2] [0xb6 - rangeMin] = 0x0666;
            mapDataStd [2] [0xb7 - rangeMin] = 0x0667;
            mapDataStd [2] [0xb8 - rangeMin] = 0x0668;
            mapDataStd [2] [0xb9 - rangeMin] = 0x0669;
            mapDataStd [2] [0xba - rangeMin] = 0xfed1;
            mapDataStd [2] [0xbb - rangeMin] = 0x061b;
            mapDataStd [2] [0xbc - rangeMin] = 0xfeb1;
            mapDataStd [2] [0xbd - rangeMin] = 0xfeb5;
            mapDataStd [2] [0xbe - rangeMin] = 0xfeb9;
            mapDataStd [2] [0xbf - rangeMin] = 0x061f;

            mapDataStd [2] [0xc0 - rangeMin] = 0x00a2;
            mapDataStd [2] [0xc1 - rangeMin] = 0xfe80;
            mapDataStd [2] [0xc2 - rangeMin] = 0xfe81;
            mapDataStd [2] [0xc3 - rangeMin] = 0xfe83;
            mapDataStd [2] [0xc4 - rangeMin] = 0xfe85;
            mapDataStd [2] [0xc5 - rangeMin] = 0xfeca;
            mapDataStd [2] [0xc6 - rangeMin] = 0xfe8b;
            mapDataStd [2] [0xc7 - rangeMin] = 0xfe8d;
            mapDataStd [2] [0xc8 - rangeMin] = 0xfe91;
            mapDataStd [2] [0xc9 - rangeMin] = 0xfe93;
            mapDataStd [2] [0xca - rangeMin] = 0xfe97;
            mapDataStd [2] [0xcb - rangeMin] = 0xfe9b;
            mapDataStd [2] [0xcc - rangeMin] = 0xfe9f;
            mapDataStd [2] [0xcd - rangeMin] = 0xfea3;
            mapDataStd [2] [0xce - rangeMin] = 0xfea7;
            mapDataStd [2] [0xcf - rangeMin] = 0xfea9;

            mapDataStd [2] [0xd0 - rangeMin] = 0xfeab;
            mapDataStd [2] [0xd1 - rangeMin] = 0xfead;
            mapDataStd [2] [0xd2 - rangeMin] = 0xfeaf;
            mapDataStd [2] [0xd3 - rangeMin] = 0xfeb3;
            mapDataStd [2] [0xd4 - rangeMin] = 0xfeb7;
            mapDataStd [2] [0xd5 - rangeMin] = 0xfebb;
            mapDataStd [2] [0xd6 - rangeMin] = 0xfebf;
            mapDataStd [2] [0xd7 - rangeMin] = 0xfec1;
            mapDataStd [2] [0xd8 - rangeMin] = 0xfec5;
            mapDataStd [2] [0xd9 - rangeMin] = 0xfecb;
            mapDataStd [2] [0xda - rangeMin] = 0xfecf;
            mapDataStd [2] [0xdb - rangeMin] = 0x00a6;
            mapDataStd [2] [0xdc - rangeMin] = 0x00ac;
            mapDataStd [2] [0xdd - rangeMin] = 0x00f7;
            mapDataStd [2] [0xde - rangeMin] = 0x00d7;
            mapDataStd [2] [0xdf - rangeMin] = 0xfec9;

            mapDataStd [2] [0xe0 - rangeMin] = 0x0640;
            mapDataStd [2] [0xe1 - rangeMin] = 0xfed3;
            mapDataStd [2] [0xe2 - rangeMin] = 0xfed7;
            mapDataStd [2] [0xe3 - rangeMin] = 0xfedb;
            mapDataStd [2] [0xe4 - rangeMin] = 0xfedf;
            mapDataStd [2] [0xe5 - rangeMin] = 0xfee3;
            mapDataStd [2] [0xe6 - rangeMin] = 0xfee7;
            mapDataStd [2] [0xe7 - rangeMin] = 0xfeeb;
            mapDataStd [2] [0xe8 - rangeMin] = 0xfeed;
            mapDataStd [2] [0xe9 - rangeMin] = 0xfeef;
            mapDataStd [2] [0xea - rangeMin] = 0xfef3;
            mapDataStd [2] [0xeb - rangeMin] = 0xfebd;
            mapDataStd [2] [0xec - rangeMin] = 0xfecc;
            mapDataStd [2] [0xed - rangeMin] = 0xfece;
            mapDataStd [2] [0xee - rangeMin] = 0xfecd;
            mapDataStd [2] [0xef - rangeMin] = 0xfee1;

            mapDataStd [2] [0xf0 - rangeMin] = 0xfe7d;
            mapDataStd [2] [0xf1 - rangeMin] = 0x0651;
            mapDataStd [2] [0xf2 - rangeMin] = 0xfee5;
            mapDataStd [2] [0xf3 - rangeMin] = 0xfee9;
            mapDataStd [2] [0xf4 - rangeMin] = 0xfeec;
            mapDataStd [2] [0xf5 - rangeMin] = 0xfef0;
            mapDataStd [2] [0xf6 - rangeMin] = 0xfef2;
            mapDataStd [2] [0xf7 - rangeMin] = 0xfed0;
            mapDataStd [2] [0xf8 - rangeMin] = 0xfed5;
            mapDataStd [2] [0xf9 - rangeMin] = 0xfef5;
            mapDataStd [2] [0xfa - rangeMin] = 0xfef6;
            mapDataStd [2] [0xfb - rangeMin] = 0xfedd;
            mapDataStd [2] [0xfc - rangeMin] = 0xfed9;
            mapDataStd [2] [0xfd - rangeMin] = 0xfef1;
            mapDataStd [2] [0xfe - rangeMin] = 0x25a0;
            mapDataStd [2] [0xff - rangeMin] = 0xffff;

            //----------------------------------------------------------------//

            for (UInt16 i = 0; i < rangeSize; i++)
            {
                mapDataPCL [2] [i] = mapDataStd [2] [i];
            }

            mapDataPCL[2][0x81 - rangeMin] = 0x2219;

            mapDataPCL[2][0x9f - rangeMin] = 0xef0e;

            mapDataPCL[2][0xa9 - rangeMin] = 0xef03;
            mapDataPCL[2][0xaa - rangeMin] = 0xef04;
            mapDataPCL[2][0xab - rangeMin] = 0xef05;
            mapDataPCL[2][0xad - rangeMin] = 0xef02;
            mapDataPCL[2][0xae - rangeMin] = 0xef07;
            mapDataPCL[2][0xaf - rangeMin] = 0xef0a;

            mapDataPCL[2][0xba - rangeMin] = 0xef06;
            mapDataPCL[2][0xbc - rangeMin] = 0xef35;
            mapDataPCL[2][0xbd - rangeMin] = 0xef36;
            mapDataPCL[2][0xbe - rangeMin] = 0xef37;

            mapDataPCL[2][0xc4 - rangeMin] = 0xef28;
            mapDataPCL[2][0xc6 - rangeMin] = 0xef34;
            mapDataPCL[2][0xc8 - rangeMin] = 0xef39;
            mapDataPCL[2][0xc9 - rangeMin] = 0xef09;
            mapDataPCL[2][0xca - rangeMin] = 0xef3a;
            mapDataPCL[2][0xcb - rangeMin] = 0xef3b;
            mapDataPCL[2][0xcc - rangeMin] = 0xef3d;
            mapDataPCL[2][0xcd - rangeMin] = 0xef3e;
            mapDataPCL[2][0xce - rangeMin] = 0xef3f;
            mapDataPCL[2][0xcf - rangeMin] = 0xef29;

            mapDataPCL[2][0xd0 - rangeMin] = 0xef2a;
            mapDataPCL[2][0xd1 - rangeMin] = 0xef2b;
            mapDataPCL[2][0xd2 - rangeMin] = 0xef2c;
            mapDataPCL[2][0xd3 - rangeMin] = 0xef46;
            mapDataPCL[2][0xd4 - rangeMin] = 0xef47;
            mapDataPCL[2][0xd5 - rangeMin] = 0xef48;
            mapDataPCL[2][0xd6 - rangeMin] = 0xef49;
            mapDataPCL[2][0xd7 - rangeMin] = 0xef32;
            mapDataPCL[2][0xd8 - rangeMin] = 0xef33;

            mapDataPCL[2][0xe1 - rangeMin] = 0xef3c;
            mapDataPCL[2][0xe2 - rangeMin] = 0xef44;
            mapDataPCL[2][0xe3 - rangeMin] = 0xef40;
            mapDataPCL[2][0xe4 - rangeMin] = 0xef41;
            mapDataPCL[2][0xe5 - rangeMin] = 0xef42;
            mapDataPCL[2][0xe6 - rangeMin] = 0xef43;
            mapDataPCL[2][0xe8 - rangeMin] = 0xef2d;
            mapDataPCL[2][0xea - rangeMin] = 0xef45;
            mapDataPCL[2][0xeb - rangeMin] = 0xef38;
            mapDataPCL[2][0xef - rangeMin] = 0xef25;

            mapDataPCL[2][0xf1 - rangeMin] = 0xfe7c;
            mapDataPCL[2][0xf2 - rangeMin] = 0xef26;
            mapDataPCL[2][0xf3 - rangeMin] = 0xef08;
            mapDataPCL[2][0xf8 - rangeMin] = 0xef27;
            mapDataPCL[2][0xfb - rangeMin] = 0xef24;
            mapDataPCL[2][0xfc - rangeMin] = 0xef23;
            mapDataPCL[2][0xfe - rangeMin] = 0x25aa;

            //----------------------------------------------------------------//

            _sets.Add (new PCLSymSetMap (mapId,
                                         rangeCt,
                                         rangeData,
                                         mapDataStd,
                                         mapDataPCL));
        }
    }
}