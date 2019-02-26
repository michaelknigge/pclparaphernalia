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
        // u n i c o d e M a p _ 1 2 N                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       12N                                                       //
        // Kind1    398                                                       //
        // Name     ISO 8859-7 Latin/Greek                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_12N ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_12N;

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

            offset = 0x0370 - 0xa0;             // 0x0370 - 0x00a0 = 0x02d0

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [1] [i - rangeMin] = (UInt16) (offset + i);
            }

            mapDataStd [1] [0xa0 - rangeMin] = 0x00a0;
            mapDataStd [1] [0xa1 - rangeMin] = 0x2018;
            mapDataStd [1] [0xa2 - rangeMin] = 0x2019;
            mapDataStd [1] [0xa3 - rangeMin] = 0x00a3;
            mapDataStd [1] [0xa4 - rangeMin] = 0x20ac;
            mapDataStd [1] [0xa5 - rangeMin] = 0x20af;
            mapDataStd [1] [0xa6 - rangeMin] = 0x00a6;
            mapDataStd [1] [0xa7 - rangeMin] = 0x00a7;
            mapDataStd [1] [0xa8 - rangeMin] = 0x00a8;
            mapDataStd [1] [0xa9 - rangeMin] = 0x00a9;
            mapDataStd [1] [0xaa - rangeMin] = 0x037a;
            mapDataStd [1] [0xab - rangeMin] = 0x00ab;
            mapDataStd [1] [0xac - rangeMin] = 0x00ac;
            mapDataStd [1] [0xad - rangeMin] = 0x00ad;
            mapDataStd [1] [0xae - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [1] [0xaf - rangeMin] = 0x2015;

            mapDataStd [1] [0xb0 - rangeMin] = 0x00b0;
            mapDataStd [1] [0xb1 - rangeMin] = 0x00b1;
            mapDataStd [1] [0xb2 - rangeMin] = 0x00b2;
            mapDataStd [1] [0xb3 - rangeMin] = 0x00b3;
            mapDataStd [1] [0xb7 - rangeMin] = 0x00b7;
            mapDataStd [1] [0xbb - rangeMin] = 0x00bb;
            mapDataStd [1] [0xbd - rangeMin] = 0x00bd;

            mapDataStd [1] [0xd2 - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [1] [0xff - rangeMin] = 0xffff;    //<not a character> //

            //----------------------------------------------------------------//

            for (UInt16 i = 0; i < rangeSize; i++)
            {
                mapDataPCL [1] [i] = mapDataStd [1] [i];
            }

            mapDataPCL [1] [0xa1 - rangeMin] = 0x1ffe;
            mapDataPCL [1] [0xa2 - rangeMin] = 0x1fbf;
            mapDataPCL [1] [0xae - rangeMin] = 0x003b;
            mapDataPCL [1] [0xaf - rangeMin] = 0x2014;

            //----------------------------------------------------------------//

            _sets.Add (new PCLSymSetMap (mapId,
                                         rangeCt,
                                         rangeData,
                                         mapDataStd,
                                         mapDataPCL));
        }
    }
}