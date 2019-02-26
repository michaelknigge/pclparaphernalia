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
        // u n i c o d e M a p _ 6 N                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       6N                                                        //
        // Kind1    206                                                       //
        // Name     ISO 8859-10 Latin 6                                       //
        //          Nordic                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_6N ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_6N;

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

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [1] [i - rangeMin] = i;
            }

            mapDataStd [1] [0xa1 - rangeMin] = 0x0104;
            mapDataStd [1] [0xa2 - rangeMin] = 0x0112;
            mapDataStd [1] [0xa3 - rangeMin] = 0x0122;
            mapDataStd [1] [0xa4 - rangeMin] = 0x012a;
            mapDataStd [1] [0xa5 - rangeMin] = 0x0128;
            mapDataStd [1] [0xa6 - rangeMin] = 0x0136;
            mapDataStd [1] [0xa8 - rangeMin] = 0x013b;
            mapDataStd [1] [0xa9 - rangeMin] = 0x0110;
            mapDataStd [1] [0xaa - rangeMin] = 0x0160;
            mapDataStd [1] [0xab - rangeMin] = 0x0166;
            mapDataStd [1] [0xac - rangeMin] = 0x017d;
            mapDataStd [1] [0xae - rangeMin] = 0x016a;
            mapDataStd [1] [0xaf - rangeMin] = 0x014a;

            mapDataStd [1] [0xb1 - rangeMin] = 0x0105;
            mapDataStd [1] [0xb2 - rangeMin] = 0x0113;
            mapDataStd [1] [0xb3 - rangeMin] = 0x0123;
            mapDataStd [1] [0xb4 - rangeMin] = 0x012b;
            mapDataStd [1] [0xb5 - rangeMin] = 0x0129;
            mapDataStd [1] [0xb6 - rangeMin] = 0x0137;
            mapDataStd [1] [0xb8 - rangeMin] = 0x013c;
            mapDataStd [1] [0xb9 - rangeMin] = 0x0111;
            mapDataStd [1] [0xba - rangeMin] = 0x0161;
            mapDataStd [1] [0xbb - rangeMin] = 0x0167;
            mapDataStd [1] [0xbc - rangeMin] = 0x017e;
            mapDataStd [1] [0xbd - rangeMin] = 0x2015;
            mapDataStd [1] [0xbe - rangeMin] = 0x016b;
            mapDataStd [1] [0xbf - rangeMin] = 0x014b;

            mapDataStd [1] [0xc0 - rangeMin] = 0x0100;
            mapDataStd [1] [0xc7 - rangeMin] = 0x012e;
            mapDataStd [1] [0xc8 - rangeMin] = 0x010c;
            mapDataStd [1] [0xca - rangeMin] = 0x0118;
            mapDataStd [1] [0xcc - rangeMin] = 0x0116;

            mapDataStd [1] [0xd1 - rangeMin] = 0x0145;
            mapDataStd [1] [0xd2 - rangeMin] = 0x014c;
            mapDataStd [1] [0xd7 - rangeMin] = 0x0168;
            mapDataStd [1] [0xd9 - rangeMin] = 0x0172;

            mapDataStd [1] [0xe0 - rangeMin] = 0x0101;
            mapDataStd [1] [0xe7 - rangeMin] = 0x012f;
            mapDataStd [1] [0xe8 - rangeMin] = 0x010d;
            mapDataStd [1] [0xea - rangeMin] = 0x0119;
            mapDataStd [1] [0xec - rangeMin] = 0x0117;

            mapDataStd [1] [0xf1 - rangeMin] = 0x0146;
            mapDataStd [1] [0xf2 - rangeMin] = 0x014d;
            mapDataStd [1] [0xf7 - rangeMin] = 0x0169;
            mapDataStd [1] [0xf9 - rangeMin] = 0x0173;
            mapDataStd [1] [0xff - rangeMin] = 0x0138;

            //----------------------------------------------------------------//

            for (UInt16 i = 0; i < rangeSize; i++)
            {
                mapDataPCL [1] [i] = mapDataStd [1] [i];
            }

            mapDataPCL [1] [0xbd - rangeMin] = 0x2014;

            //----------------------------------------------------------------//

            _sets.Add (new PCLSymSetMap (mapId,
                                         rangeCt,
                                         rangeData,
                                         mapDataStd,
                                         mapDataPCL));
        }
    }
}