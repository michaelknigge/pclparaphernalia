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
        // u n i c o d e M a p _ 8 G                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       8G                                                        //
        // Kind1    263                                                       //
        // Name     Greek-8                                                   //
        //          Note that some web pages show this symbol set as CP869,   //
		//          but the Laserjet output is very different.                //
		//          The output from LJ 1320n and LJ M475dn is a subset of     //
		//          that obtained from the LJ M553x for the few fonts         //
		//          (CG Times, Courier, Letter Gothic, Univers) which support //
		//          this symbol	set.                                          //	
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_8G ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_8G;

            const Int32 rangeCt = 2;

            UInt16 [] [] rangeData = new UInt16 [rangeCt] []
            {
                new UInt16 [2] {0x20, 0x7f},
                new UInt16 [2] {0xbc, 0xff}
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
                mapDataPCL [0] [i - rangeMin] = i;
            }

            mapDataPCL[0][0x27 - rangeMin] = 0x1fbf;
            mapDataPCL[0][0x5f - rangeMin] = 0xffff;    //<not a character> //
            mapDataPCL[0][0x60 - rangeMin] = 0x1ffe;
            mapDataPCL[0][0x7f - rangeMin] = 0x2592;

            //----------------------------------------------------------------//
            //                                                                //
            // Range 1                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            rangeMin = rangeData [1] [0];
            rangeMax = rangeData [1] [1];
            rangeSize = rangeSizes [1];

            mapDataPCL [1] [0xbc - rangeMin] = 0x03ca;
            mapDataPCL [1] [0xbd - rangeMin] = 0xffff;    //<not a character> //
            mapDataPCL [1] [0xbe - rangeMin] = 0x03cb;
            mapDataPCL [1] [0xbf - rangeMin] = 0xffff;    //<not a character> //

            mapDataPCL [1] [0xc0 - rangeMin] = 0xffff;    //<not a character> //
            mapDataPCL [1] [0xc1 - rangeMin] = 0x0391;
            mapDataPCL [1] [0xc2 - rangeMin] = 0x0392;
            mapDataPCL [1] [0xc3 - rangeMin] = 0x0393;
            mapDataPCL [1] [0xc4 - rangeMin] = 0x0394;
            mapDataPCL [1] [0xc5 - rangeMin] = 0x0395;
            mapDataPCL [1] [0xc6 - rangeMin] = 0x0396;
            mapDataPCL [1] [0xc7 - rangeMin] = 0x0397;
            mapDataPCL [1] [0xc8 - rangeMin] = 0x0398;
            mapDataPCL [1] [0xc9 - rangeMin] = 0x0399;
            mapDataPCL [1] [0xca - rangeMin] = 0xffff;    //<not a character> //
            mapDataPCL [1] [0xcb - rangeMin] = 0x039a;
            mapDataPCL [1] [0xcc - rangeMin] = 0x039b;
            mapDataPCL [1] [0xcd - rangeMin] = 0x039c;
            mapDataPCL [1] [0xce - rangeMin] = 0x039d;
            mapDataPCL [1] [0xcf - rangeMin] = 0x039e;

            mapDataPCL [1] [0xd0 - rangeMin] = 0x039f;
            mapDataPCL [1] [0xd1 - rangeMin] = 0x03a0;
            mapDataPCL [1] [0xd2 - rangeMin] = 0x03a1;
            mapDataPCL [1] [0xd3 - rangeMin] = 0x03a3;
            mapDataPCL [1] [0xd4 - rangeMin] = 0x03a4;
            mapDataPCL [1] [0xd5 - rangeMin] = 0x03a5;
            mapDataPCL [1] [0xd6 - rangeMin] = 0x03a6;
            mapDataPCL [1] [0xd7 - rangeMin] = 0xffff;    //<not a character> //
            mapDataPCL [1] [0xd8 - rangeMin] = 0x03a7;
            mapDataPCL [1] [0xd9 - rangeMin] = 0x03a8;
            mapDataPCL [1] [0xda - rangeMin] = 0x03a9;
            mapDataPCL [1] [0xdb - rangeMin] = 0x03ac;
            mapDataPCL [1] [0xdc - rangeMin] = 0x03ae;
            mapDataPCL [1] [0xdd - rangeMin] = 0x03cc;
            mapDataPCL [1] [0xde - rangeMin] = 0xffff;    //<not a character> //
            mapDataPCL [1] [0xdf - rangeMin] = 0xffff;    //<not a character> //

            mapDataPCL [1] [0xe0 - rangeMin] = 0x03cd;
            mapDataPCL [1] [0xe1 - rangeMin] = 0x03b1;
            mapDataPCL [1] [0xe2 - rangeMin] = 0x03b2;
            mapDataPCL [1] [0xe3 - rangeMin] = 0x03b3;
            mapDataPCL [1] [0xe4 - rangeMin] = 0x03b4;
            mapDataPCL [1] [0xe5 - rangeMin] = 0x03b5;
            mapDataPCL [1] [0xe6 - rangeMin] = 0x03b6;
            mapDataPCL [1] [0xe7 - rangeMin] = 0x03b7;
            mapDataPCL [1] [0xe8 - rangeMin] = 0x03b8;
            mapDataPCL [1] [0xe9 - rangeMin] = 0x03b9;
            mapDataPCL [1] [0xea - rangeMin] = 0xffff;    //<not a character> //
            mapDataPCL [1] [0xeb - rangeMin] = 0x03ba;
            mapDataPCL [1] [0xec - rangeMin] = 0x03bb;
            mapDataPCL [1] [0xed - rangeMin] = 0x03bc;
            mapDataPCL [1] [0xee - rangeMin] = 0x03bd;
            mapDataPCL [1] [0xef - rangeMin] = 0x03be;

            mapDataPCL [1] [0xf0 - rangeMin] = 0x03bf;
            mapDataPCL [1] [0xf1 - rangeMin] = 0x03c0;
            mapDataPCL [1] [0xf2 - rangeMin] = 0x03c1;
            mapDataPCL [1] [0xf3 - rangeMin] = 0x03c3;
            mapDataPCL [1] [0xf4 - rangeMin] = 0x03c4;
            mapDataPCL [1] [0xf5 - rangeMin] = 0x03c5;
            mapDataPCL [1] [0xf6 - rangeMin] = 0x03c6;
            mapDataPCL [1] [0xf7 - rangeMin] = 0x03c2;
            mapDataPCL [1] [0xf8 - rangeMin] = 0x03c7;
            mapDataPCL [1] [0xf9 - rangeMin] = 0x03c8;
            mapDataPCL [1] [0xfa - rangeMin] = 0x03c9;
            mapDataPCL [1] [0xfb - rangeMin] = 0x03ad;
            mapDataPCL [1] [0xfc - rangeMin] = 0x03af;
            mapDataPCL [1] [0xfd - rangeMin] = 0x03ce;
            mapDataPCL [1] [0xfe - rangeMin] = 0x2219;
            mapDataPCL [1] [0xff - rangeMin] = 0xffff;    //<not a character> //

            //----------------------------------------------------------------//

            _sets.Add (new PCLSymSetMap (mapId,
                                         rangeCt,
                                         rangeData,
                                         null,
                                         mapDataPCL));
        }
    }
}