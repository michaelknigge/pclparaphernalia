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
        // u n i c o d e M a p _ x 1 0 0 1 T                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Maps characters in symbol set to Unicode (UCS-2) code-points.      //
        //                                                                    //
        // ID       1001T                                                     //
        // Kind1    32052                                                     //
        // Name     ISO-8859-11 Latin/Thai                                    //
        // Note     As TIS-620 (symbol set 1T) but also includes              //
        //          no-break-space at code-point 0xA0                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_x1001T ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_x1001T;

            const Int32 rangeCt = 2;

            UInt16 [] [] rangeData = new UInt16 [rangeCt] []
            {
                new UInt16 [2] {0x20, 0x7f},
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

            offset = 0x0e00 - 0x00a0;     // 0x0e00 - 0x00a0 = 0x0d60

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [1] [i - rangeMin] = (UInt16) (offset + i);
            }

            mapDataStd [1] [0xa0 - rangeMin] = 0x00a0;
            mapDataStd [1] [0xdb - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [1] [0xdc - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [1] [0xdd - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [1] [0xde - rangeMin] = 0xffff;    //<not a character> //

            mapDataStd [1] [0xfc - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [1] [0xfd - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [1] [0xfe - rangeMin] = 0xffff;    //<not a character> //
            mapDataStd [1] [0xff - rangeMin] = 0xffff;    //<not a character> //

            //----------------------------------------------------------------//

            _sets.Add (new PCLSymSetMap (mapId,
                                         rangeCt,
                                         rangeData,
                                         mapDataStd,
                                         null));
        }
    }
}