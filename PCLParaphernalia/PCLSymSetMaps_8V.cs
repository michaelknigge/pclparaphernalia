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
        // u n i c o d e M a p _ 8 V                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       8V                                                        //
        // Kind1    278                                                       //
        // Name     Arabic-8                                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_8V ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_8V;

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

            mapDataPCL[0][0x20 - rangeMin] = 0x0020;
            mapDataPCL[0][0x21 - rangeMin] = 0xef4a;
            mapDataPCL[0][0x22 - rangeMin] = 0xef4b;
            mapDataPCL[0][0x23 - rangeMin] = 0xef4c;
            mapDataPCL[0][0x24 - rangeMin] = 0xef4d;
            mapDataPCL[0][0x25 - rangeMin] = 0x066a;
            mapDataPCL[0][0x26 - rangeMin] = 0xef4e;
            mapDataPCL[0][0x27 - rangeMin] = 0xef4f;
            mapDataPCL[0][0x28 - rangeMin] = 0xef50;
            mapDataPCL[0][0x29 - rangeMin] = 0xef51;
            mapDataPCL[0][0x2a - rangeMin] = 0x066d;
            mapDataPCL[0][0x2b - rangeMin] = 0xef52;
            mapDataPCL[0][0x2c - rangeMin] = 0x060c;
            mapDataPCL[0][0x2d - rangeMin] = 0xef53;
            mapDataPCL[0][0x2e - rangeMin] = 0xef54;
            mapDataPCL[0][0x2f - rangeMin] = 0xef55;

            mapDataPCL[0][0x30 - rangeMin] = 0x0660;
            mapDataPCL[0][0x31 - rangeMin] = 0x0661;
            mapDataPCL[0][0x32 - rangeMin] = 0x0662;
            mapDataPCL[0][0x33 - rangeMin] = 0x0663;
            mapDataPCL[0][0x34 - rangeMin] = 0x0664;
            mapDataPCL[0][0x35 - rangeMin] = 0x0665;
            mapDataPCL[0][0x36 - rangeMin] = 0x0666;
            mapDataPCL[0][0x37 - rangeMin] = 0x0667;
            mapDataPCL[0][0x38 - rangeMin] = 0x0668;
            mapDataPCL[0][0x39 - rangeMin] = 0x0669;
            mapDataPCL[0][0x3a - rangeMin] = 0xef56;
            mapDataPCL[0][0x3b - rangeMin] = 0x061b;
            mapDataPCL[0][0x3c - rangeMin] = 0xef57;
            mapDataPCL[0][0x3d - rangeMin] = 0xef58;
            mapDataPCL[0][0x3e - rangeMin] = 0xef59;
            mapDataPCL[0][0x3f - rangeMin] = 0x061f;

            mapDataPCL[0][0x40 - rangeMin] = 0xef5a;
            mapDataPCL[0][0x41 - rangeMin] = 0xfee0;
            mapDataPCL[0][0x42 - rangeMin] = 0xfedf;
            mapDataPCL[0][0x43 - rangeMin] = 0xef00;
            mapDataPCL[0][0x44 - rangeMin] = 0xef01;
            mapDataPCL[0][0x45 - rangeMin] = 0xef02;
            mapDataPCL[0][0x46 - rangeMin] = 0xfe84;
            mapDataPCL[0][0x47 - rangeMin] = 0xfe82;
            mapDataPCL[0][0x48 - rangeMin] = 0xfe88;
            mapDataPCL[0][0x49 - rangeMin] = 0xfefc;
            mapDataPCL[0][0x4a - rangeMin] = 0xfef8;
            mapDataPCL[0][0x4b - rangeMin] = 0xfef6;
            mapDataPCL[0][0x4c - rangeMin] = 0xfefa;
            mapDataPCL[0][0x4d - rangeMin] = 0xfefb;
            mapDataPCL[0][0x4e - rangeMin] = 0xfef7;
            mapDataPCL[0][0x4f - rangeMin] = 0xfef5;

            mapDataPCL[0][0x50 - rangeMin] = 0xfef9;
            mapDataPCL[0][0x51 - rangeMin] = 0xef03;
            mapDataPCL[0][0x52 - rangeMin] = 0xef04;
            mapDataPCL[0][0x53 - rangeMin] = 0xef05;
            mapDataPCL[0][0x54 - rangeMin] = 0xef06;
            mapDataPCL[0][0x55 - rangeMin] = 0xef07;
            mapDataPCL[0][0x56 - rangeMin] = 0xfee4;
            mapDataPCL[0][0x57 - rangeMin] = 0xef08;
            mapDataPCL[0][0x58 - rangeMin] = 0xef09;
            mapDataPCL[0][0x59 - rangeMin] = 0xef5b;
            mapDataPCL[0][0x5a - rangeMin] = 0xef5c;
            mapDataPCL[0][0x5b - rangeMin] = 0xef5d;
            mapDataPCL[0][0x5c - rangeMin] = 0xef5e;
            mapDataPCL[0][0x5d - rangeMin] = 0xef5f;
            mapDataPCL[0][0x5e - rangeMin] = 0xef60;
            mapDataPCL[0][0x5f - rangeMin] = 0x005f;

            mapDataPCL[0][0x60 - rangeMin] = 0xfe80;
            mapDataPCL[0][0x61 - rangeMin] = 0xfe70;
            mapDataPCL[0][0x62 - rangeMin] = 0xfe72;
            mapDataPCL[0][0x63 - rangeMin] = 0xfe74;
            mapDataPCL[0][0x64 - rangeMin] = 0xfc60;
            mapDataPCL[0][0x65 - rangeMin] = 0xfc61;
            mapDataPCL[0][0x66 - rangeMin] = 0xfc62;
            mapDataPCL[0][0x67 - rangeMin] = 0xfe76;
            mapDataPCL[0][0x68 - rangeMin] = 0xfe78;
            mapDataPCL[0][0x69 - rangeMin] = 0xfe7a;
            mapDataPCL[0][0x6a - rangeMin] = 0xfe7e;
            mapDataPCL[0][0x6b - rangeMin] = 0xfe7c;
            mapDataPCL[0][0x6c - rangeMin] = 0xfec9;
            mapDataPCL[0][0x6d - rangeMin] = 0xfecd;
            mapDataPCL[0][0x6e - rangeMin] = 0xfee9;
            mapDataPCL[0][0x6f - rangeMin] = 0xfe9d;

            mapDataPCL[0][0x70 - rangeMin] = 0xfea1;
            mapDataPCL[0][0x71 - rangeMin] = 0xfea5;
            mapDataPCL[0][0x72 - rangeMin] = 0xfe93;
            mapDataPCL[0][0x73 - rangeMin] = 0xfe94;
            mapDataPCL[0][0x74 - rangeMin] = 0xfe8e;
            mapDataPCL[0][0x75 - rangeMin] = 0xfef2;
            mapDataPCL[0][0x76 - rangeMin] = 0xfe8a;
            mapDataPCL[0][0x77 - rangeMin] = 0xfef0;
            mapDataPCL[0][0x78 - rangeMin] = 0xef0a;
            mapDataPCL[0][0x79 - rangeMin] = 0xef0b;
            mapDataPCL[0][0x7a - rangeMin] = 0xef0c;
            mapDataPCL[0][0x7b - rangeMin] = 0xef61;
            mapDataPCL[0][0x7c - rangeMin] = 0x007c;
            mapDataPCL[0][0x7d - rangeMin] = 0xef62;
            mapDataPCL[0][0x7e - rangeMin] = 0xef63;
            mapDataPCL[0][0x7f - rangeMin] = 0x2592;

            //----------------------------------------------------------------//
            //                                                                //
            // Range 1                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            rangeMin = rangeData [1] [0];
            rangeMax = rangeData [1] [1];
            rangeSize = rangeSizes [1];

            mapDataPCL[1][0xa0 - rangeMin] = 0x00a0;
            mapDataPCL[1][0xa1 - rangeMin] = 0xef0d;
            mapDataPCL[1][0xa2 - rangeMin] = 0xef0e;
            mapDataPCL[1][0xa3 - rangeMin] = 0xfd3c;
            mapDataPCL[1][0xa4 - rangeMin] = 0xef0f;
            mapDataPCL[1][0xa5 - rangeMin] = 0xef10;
            mapDataPCL[1][0xa6 - rangeMin] = 0xef11;
            mapDataPCL[1][0xa7 - rangeMin] = 0xef12;
            mapDataPCL[1][0xa8 - rangeMin] = 0xef13;
            mapDataPCL[1][0xa9 - rangeMin] = 0xef14;
            mapDataPCL[1][0xaa - rangeMin] = 0xef15;
            mapDataPCL[1][0xab - rangeMin] = 0xef16;
            mapDataPCL[1][0xac - rangeMin] = 0xef17;
            mapDataPCL[1][0xad - rangeMin] = 0xef18;
            mapDataPCL[1][0xae - rangeMin] = 0xfd3d;
            mapDataPCL[1][0xaf - rangeMin] = 0xef19;

            mapDataPCL[1][0xb0 - rangeMin] = 0xef1a;
            mapDataPCL[1][0xb1 - rangeMin] = 0xef1b;
            mapDataPCL[1][0xb2 - rangeMin] = 0xef1c;
            mapDataPCL[1][0xb3 - rangeMin] = 0xef1d;
            mapDataPCL[1][0xb4 - rangeMin] = 0xef1e;
            mapDataPCL[1][0xb5 - rangeMin] = 0xef1f;
            mapDataPCL[1][0xb6 - rangeMin] = 0xef20;
            mapDataPCL[1][0xb7 - rangeMin] = 0xef21;
            mapDataPCL[1][0xb8 - rangeMin] = 0xef22;
            mapDataPCL[1][0xb9 - rangeMin] = 0xfeca;
            mapDataPCL[1][0xba - rangeMin] = 0xfece;
            mapDataPCL[1][0xbb - rangeMin] = 0xfeea;
            mapDataPCL[1][0xbc - rangeMin] = 0xfe9e;
            mapDataPCL[1][0xbd - rangeMin] = 0xfea2;
            mapDataPCL[1][0xbe - rangeMin] = 0xfea6;
            mapDataPCL[1][0xbf - rangeMin] = 0xef23;

            mapDataPCL[1][0xc0 - rangeMin] = 0xef24;
            mapDataPCL[1][0xc1 - rangeMin] = 0xef25;
            mapDataPCL[1][0xc2 - rangeMin] = 0xef26;
            mapDataPCL[1][0xc3 - rangeMin] = 0xef27;
            mapDataPCL[1][0xc4 - rangeMin] = 0xfef1;
            mapDataPCL[1][0xc5 - rangeMin] = 0xfe89;
            mapDataPCL[1][0xc6 - rangeMin] = 0xfeef;
            mapDataPCL[1][0xc7 - rangeMin] = 0xef28;
            mapDataPCL[1][0xc8 - rangeMin] = 0xef29;
            mapDataPCL[1][0xc9 - rangeMin] = 0xef2a;
            mapDataPCL[1][0xca - rangeMin] = 0xef2b;
            mapDataPCL[1][0xcb - rangeMin] = 0xef2c;
            mapDataPCL[1][0xcc - rangeMin] = 0xef2d;
            mapDataPCL[1][0xcd - rangeMin] = 0xfe83;
            mapDataPCL[1][0xce - rangeMin] = 0xfe81;
            mapDataPCL[1][0xcf - rangeMin] = 0xfe87;

            mapDataPCL[1][0xd0 - rangeMin] = 0xfe8d;
            mapDataPCL[1][0xd1 - rangeMin] = 0xef2e;
            mapDataPCL[1][0xd2 - rangeMin] = 0xef2f;
            mapDataPCL[1][0xd3 - rangeMin] = 0xef30;
            mapDataPCL[1][0xd4 - rangeMin] = 0xef31;
            mapDataPCL[1][0xd5 - rangeMin] = 0xef32;
            mapDataPCL[1][0xd6 - rangeMin] = 0xef33;
            mapDataPCL[1][0xd7 - rangeMin] = 0x0640;
            mapDataPCL[1][0xd8 - rangeMin] = 0xef34;
            mapDataPCL[1][0xd9 - rangeMin] = 0xef35;
            mapDataPCL[1][0xda - rangeMin] = 0xef36;
            mapDataPCL[1][0xdb - rangeMin] = 0xef37;
            mapDataPCL[1][0xdc - rangeMin] = 0xef38;
            mapDataPCL[1][0xdd - rangeMin] = 0xfe7d;
            mapDataPCL[1][0xde - rangeMin] = 0xef39;
            mapDataPCL[1][0xdf - rangeMin] = 0xef3a;

            mapDataPCL[1][0xe0 - rangeMin] = 0xef3b;
            mapDataPCL[1][0xe1 - rangeMin] = 0xef3c;
            mapDataPCL[1][0xe2 - rangeMin] = 0xfecb;
            mapDataPCL[1][0xe3 - rangeMin] = 0xfecf;
            mapDataPCL[1][0xe4 - rangeMin] = 0xfeeb;
            mapDataPCL[1][0xe5 - rangeMin] = 0xfe9f;
            mapDataPCL[1][0xe6 - rangeMin] = 0xfea3;
            mapDataPCL[1][0xe7 - rangeMin] = 0xfea7;
            mapDataPCL[1][0xe8 - rangeMin] = 0xfecc;
            mapDataPCL[1][0xe9 - rangeMin] = 0xfed0;
            mapDataPCL[1][0xea - rangeMin] = 0xfeec;
            mapDataPCL[1][0xeb - rangeMin] = 0xef3d;
            mapDataPCL[1][0xec - rangeMin] = 0xef3e;
            mapDataPCL[1][0xed - rangeMin] = 0xef3f;
            mapDataPCL[1][0xee - rangeMin] = 0xef40;
            mapDataPCL[1][0xef - rangeMin] = 0xef41;

            mapDataPCL[1][0xf0 - rangeMin] = 0xef42;
            mapDataPCL[1][0xf1 - rangeMin] = 0xef43;
            mapDataPCL[1][0xf2 - rangeMin] = 0xef44;
            mapDataPCL[1][0xf3 - rangeMin] = 0xef45;
            mapDataPCL[1][0xf4 - rangeMin] = 0xef46;
            mapDataPCL[1][0xf5 - rangeMin] = 0xef47;
            mapDataPCL[1][0xf6 - rangeMin] = 0xef48;
            mapDataPCL[1][0xf7 - rangeMin] = 0xef49;
            mapDataPCL[1][0xf8 - rangeMin] = 0xfcf2;
            mapDataPCL[1][0xf9 - rangeMin] = 0xfcf3;
            mapDataPCL[1][0xfa - rangeMin] = 0xfcf4;
            mapDataPCL[1][0xfb - rangeMin] = 0xfe77;
            mapDataPCL[1][0xfc - rangeMin] = 0xfe79;
            mapDataPCL[1][0xfd - rangeMin] = 0xfe7b;
            mapDataPCL[1][0xfe - rangeMin] = 0xfe7f;
            mapDataPCL[1][0xff - rangeMin] = 0xffff;    //<not a character> //

            //----------------------------------------------------------------//

            _sets.Add (new PCLSymSetMap (mapId,
                                         rangeCt,
                                         rangeData,
                                         null,
                                         mapDataPCL));
        }
    }
}