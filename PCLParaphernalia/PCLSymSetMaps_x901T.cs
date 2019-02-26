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
        // u n i c o d e M a p _ x 9 0 1 T                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       901T                                                      //
        // Kind1    28852                                                     //
        // Name     Windows Latin/Thai Code Page 874                          //
        // Note     As ISO-8859-11 (symbol set 1001T), but also includes 9    //
        //          additional symbols in code-point range 0x80 -> 0x9f       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_x901T ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_x901T;

            const Int32 rangeCt = 3;

            UInt16 [] [] rangeData = new UInt16 [rangeCt] []
            {
                new UInt16 [2] {0x20, 0x7f},
                new UInt16 [2] {0x80, 0x9f},
                new UInt16 [2] {0xa0, 0xff}
            };

            UInt16 [] rangeSizes = new UInt16 [rangeCt];

            UInt16 [] [] mapDataStd = new UInt16 [rangeCt] [];

            UInt16 rangeMin,
                   rangeMax,
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
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Range 0                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            rangeMin = rangeData [0] [0];
            rangeMax = rangeData [0] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [0] [i - rangeMin] = i;
            }

            mapDataStd [0] [0x7f - rangeMin] = 0xffff;    //<not a character> //

            //----------------------------------------------------------------//
            //                                                                //
            // Range 1                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            rangeMin = rangeData [1] [0];
            rangeMax = rangeData [1] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [1] [i - rangeMin] = 0xffff;    //<not a character> //
            }

            mapDataStd [1] [0x80 - rangeMin] = 0x20ac;
            mapDataStd [1] [0x85 - rangeMin] = 0x2026;

            mapDataStd [1] [0x91 - rangeMin] = 0x2018;
            mapDataStd [1] [0x92 - rangeMin] = 0x2019;
            mapDataStd [1] [0x93 - rangeMin] = 0x201c;
            mapDataStd [1] [0x94 - rangeMin] = 0x201d;
            mapDataStd [1] [0x95 - rangeMin] = 0x2022;
            mapDataStd [1] [0x96 - rangeMin] = 0x2013;
            mapDataStd [1] [0x97 - rangeMin] = 0x2014;

            //----------------------------------------------------------------//
            //                                                                //
            // Range 2                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            rangeMin = rangeData [2] [0];
            rangeMax = rangeData [2] [1];

            offset = 0x0e00 - 0x00a0;     // 0x0e00 - 0x00a0 = 0x0d60

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [2] [i - rangeMin] = (UInt16) (offset + i);
            }

            mapDataStd [2] [0xa0 - rangeMin] = 0x00a0;
            mapDataStd [2] [0xdb - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [2] [0xdc - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [2] [0xdd - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [2] [0xde - rangeMin] = 0xffff;    //<not a character> //

            mapDataStd [2] [0xfc - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [2] [0xfd - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [2] [0xfe - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [2] [0xff - rangeMin] = 0xffff;    //<not a character> //

            //----------------------------------------------------------------//

            _sets.Add (new PCLSymSetMap (mapId,
                                         rangeCt,
                                         rangeData,
                                         mapDataStd,
                                         null));
        }
    }
}