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
        // u n i c o d e M a p _ x 1 0 1 8 T                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // ID       1018T                                                     //
        // Kind1    32596                                                     //
        // Name     Big5 Traditional Chinese                                  //
        // Note     Non-standard set - may be same as standard set 18T ?      //
        //          Details taken from the "BIG5 to Unicode" table supplied   //
        //          by Unicode.org (in the Obsolete' sets list).              //
        //          Code-points are provided in the range 0x2121 -> 0x777E;   //
        //          0x8080 is added to these to provide the required          //
        //          hexadecimal values.                                       //
        //          The ASCII range 0x20->0x7e could be added as the first    //
        //          range?                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void unicodeMap_x1018T ()
        {
            const eSymSetMapId mapId = eSymSetMapId.map_x1018T;

            const Int32 rangeCt = 176;

        //  const UInt16 offset = 0x8080;

            Int32 rangeNo;

            UInt16 [] [] rangeData = new UInt16 [rangeCt] []
            {
            //  new UInt16 [2] {0x0020, 0x007e},
                new UInt16 [2] {0xa140, 0xa17e},
                new UInt16 [2] {0xa1a1, 0xa1fe},
                new UInt16 [2] {0xa240, 0xa27e},
                new UInt16 [2] {0xa2a1, 0xa2fe},
                new UInt16 [2] {0xa340, 0xa37e},
                new UInt16 [2] {0xa3a1, 0xa3fe},
                new UInt16 [2] {0xa440, 0xa47e},
                new UInt16 [2] {0xa4a1, 0xa4fe},
                new UInt16 [2] {0xa540, 0xa57e},
                new UInt16 [2] {0xa5a1, 0xa5fe},
                new UInt16 [2] {0xa640, 0xa67e},
                new UInt16 [2] {0xa6a1, 0xa6fe},
                new UInt16 [2] {0xa740, 0xa77e},
                new UInt16 [2] {0xa7a1, 0xa7fe},
                new UInt16 [2] {0xa840, 0xa87e},
                new UInt16 [2] {0xa8a1, 0xa8fe},
                new UInt16 [2] {0xa940, 0xa97e},
                new UInt16 [2] {0xa9a1, 0xa9fe},
                new UInt16 [2] {0xaa40, 0xaa7e},
                new UInt16 [2] {0xaaa1, 0xaafe},
                new UInt16 [2] {0xab40, 0xab7e},
                new UInt16 [2] {0xaba1, 0xabfe},
                new UInt16 [2] {0xac40, 0xac7e},
                new UInt16 [2] {0xaca1, 0xacfe},
                new UInt16 [2] {0xad40, 0xad7e},
                new UInt16 [2] {0xada1, 0xadfe},
                new UInt16 [2] {0xae40, 0xae7e},
                new UInt16 [2] {0xaea1, 0xaefe},
                new UInt16 [2] {0xaf40, 0xaf7e},
                new UInt16 [2] {0xafa1, 0xaffe},

                new UInt16 [2] {0xb040, 0xb07e},
                new UInt16 [2] {0xb0a1, 0xb0fe},
                new UInt16 [2] {0xb140, 0xb17e},
                new UInt16 [2] {0xb1a1, 0xb1fe},
                new UInt16 [2] {0xb240, 0xb27e},
                new UInt16 [2] {0xb2a1, 0xb2fe},
                new UInt16 [2] {0xb340, 0xb37e},
                new UInt16 [2] {0xb3a1, 0xb3fe},
                new UInt16 [2] {0xb440, 0xb47e},
                new UInt16 [2] {0xb4a1, 0xb4fe},
                new UInt16 [2] {0xb540, 0xb57e},
                new UInt16 [2] {0xb5a1, 0xb5fe},
                new UInt16 [2] {0xb640, 0xb67e},
                new UInt16 [2] {0xb6a1, 0xb6fe},
                new UInt16 [2] {0xb740, 0xb77e},
                new UInt16 [2] {0xb7a1, 0xb7fe},
                new UInt16 [2] {0xb840, 0xb87e},
                new UInt16 [2] {0xb8a1, 0xb8fe},
                new UInt16 [2] {0xb940, 0xb97e},
                new UInt16 [2] {0xb9a1, 0xb9fe},
                new UInt16 [2] {0xba40, 0xba7e},
                new UInt16 [2] {0xbaa1, 0xbafe},
                new UInt16 [2] {0xbb40, 0xbb7e},
                new UInt16 [2] {0xbba1, 0xbbfe},
                new UInt16 [2] {0xbc40, 0xbc7e},
                new UInt16 [2] {0xbca1, 0xbcfe},
                new UInt16 [2] {0xbd40, 0xbd7e},
                new UInt16 [2] {0xbda1, 0xbdfe},
                new UInt16 [2] {0xbe40, 0xbe7e},
                new UInt16 [2] {0xbea1, 0xbefe},
                new UInt16 [2] {0xbf40, 0xbf7e},
                new UInt16 [2] {0xbfa1, 0xbffe},

                new UInt16 [2] {0xc040, 0xc07e},
                new UInt16 [2] {0xc0a1, 0xc0fe},
                new UInt16 [2] {0xc140, 0xc17e},
                new UInt16 [2] {0xc1a1, 0xc1fe},
                new UInt16 [2] {0xc240, 0xc27e},
                new UInt16 [2] {0xc2a1, 0xc2fe},
                new UInt16 [2] {0xc340, 0xc37e},
                new UInt16 [2] {0xc3a1, 0xc3fe},
                new UInt16 [2] {0xc440, 0xc47e},
                new UInt16 [2] {0xc4a1, 0xc4fe},
                new UInt16 [2] {0xc540, 0xc57e},
                new UInt16 [2] {0xc5a1, 0xc5fe},
                new UInt16 [2] {0xc640, 0xc67e},
                new UInt16 [2] {0xc6a1, 0xc6fe},
                new UInt16 [2] {0xc740, 0xc77e},
                new UInt16 [2] {0xc7a1, 0xc7fe},
       //       new UInt16 [2] {0xc840, 0xc87e},
       //       new UInt16 [2] {0xc8a1, 0xc8fe},
                new UInt16 [2] {0xc940, 0xc97e},
                new UInt16 [2] {0xc9a1, 0xc9fe},
                new UInt16 [2] {0xca40, 0xca7e},
                new UInt16 [2] {0xcaa1, 0xcafe},
                new UInt16 [2] {0xcb40, 0xcb7e},
                new UInt16 [2] {0xcba1, 0xcbfe},
                new UInt16 [2] {0xcc40, 0xcc7e},
                new UInt16 [2] {0xcca1, 0xccfe},
                new UInt16 [2] {0xcd40, 0xcd7e},
                new UInt16 [2] {0xcda1, 0xcdfe},
                new UInt16 [2] {0xce40, 0xce7e},
                new UInt16 [2] {0xcea1, 0xcefe},
                new UInt16 [2] {0xcf40, 0xcf7e},
                new UInt16 [2] {0xcfa1, 0xcffe},

                new UInt16 [2] {0xd040, 0xd07e},
                new UInt16 [2] {0xd0a1, 0xd0fe},
                new UInt16 [2] {0xd140, 0xd17e},
                new UInt16 [2] {0xd1a1, 0xd1fe},
                new UInt16 [2] {0xd240, 0xd27e},
                new UInt16 [2] {0xd2a1, 0xd2fe},
                new UInt16 [2] {0xd340, 0xd37e},
                new UInt16 [2] {0xd3a1, 0xd3fe},
                new UInt16 [2] {0xd440, 0xd47e},
                new UInt16 [2] {0xd4a1, 0xd4fe},
                new UInt16 [2] {0xd540, 0xd57e},
                new UInt16 [2] {0xd5a1, 0xd5fe},
                new UInt16 [2] {0xd640, 0xd67e},
                new UInt16 [2] {0xd6a1, 0xd6fe},
                new UInt16 [2] {0xd740, 0xd77e},
                new UInt16 [2] {0xd7a1, 0xd7fe},
                new UInt16 [2] {0xd840, 0xd87e},
                new UInt16 [2] {0xd8a1, 0xd8fe},
                new UInt16 [2] {0xd940, 0xd97e},
                new UInt16 [2] {0xd9a1, 0xd9fe},
                new UInt16 [2] {0xda40, 0xda7e},
                new UInt16 [2] {0xdaa1, 0xdafe},
                new UInt16 [2] {0xdb40, 0xdb7e},
                new UInt16 [2] {0xdba1, 0xdbfe},
                new UInt16 [2] {0xdc40, 0xdc7e},
                new UInt16 [2] {0xdca1, 0xdcfe},
                new UInt16 [2] {0xdd40, 0xdd7e},
                new UInt16 [2] {0xdda1, 0xddfe},
                new UInt16 [2] {0xde40, 0xde7e},
                new UInt16 [2] {0xdea1, 0xdefe},
                new UInt16 [2] {0xdf40, 0xdf7e},
                new UInt16 [2] {0xdfa1, 0xdffe},

                new UInt16 [2] {0xe040, 0xe07e},
                new UInt16 [2] {0xe0a1, 0xe0fe},
                new UInt16 [2] {0xe140, 0xe17e},
                new UInt16 [2] {0xe1a1, 0xe1fe},
                new UInt16 [2] {0xe240, 0xe27e},
                new UInt16 [2] {0xe2a1, 0xe2fe},
                new UInt16 [2] {0xe340, 0xe37e},
                new UInt16 [2] {0xe3a1, 0xe3fe},
                new UInt16 [2] {0xe440, 0xe47e},
                new UInt16 [2] {0xe4a1, 0xe4fe},
                new UInt16 [2] {0xe540, 0xe57e},
                new UInt16 [2] {0xe5a1, 0xe5fe},
                new UInt16 [2] {0xe640, 0xe67e},
                new UInt16 [2] {0xe6a1, 0xe6fe},
                new UInt16 [2] {0xe740, 0xe77e},
                new UInt16 [2] {0xe7a1, 0xe7fe},
                new UInt16 [2] {0xe840, 0xe87e},
                new UInt16 [2] {0xe8a1, 0xe8fe},
                new UInt16 [2] {0xe940, 0xe97e},
                new UInt16 [2] {0xe9a1, 0xe9fe},
                new UInt16 [2] {0xea40, 0xea7e},
                new UInt16 [2] {0xeaa1, 0xeafe},
                new UInt16 [2] {0xeb40, 0xeb7e},
                new UInt16 [2] {0xeba1, 0xebfe},
                new UInt16 [2] {0xec40, 0xec7e},
                new UInt16 [2] {0xeca1, 0xecfe},
                new UInt16 [2] {0xed40, 0xed7e},
                new UInt16 [2] {0xeda1, 0xedfe},
                new UInt16 [2] {0xee40, 0xee7e},
                new UInt16 [2] {0xeea1, 0xeefe},
                new UInt16 [2] {0xef40, 0xef7e},
                new UInt16 [2] {0xefa1, 0xeffe},

                new UInt16 [2] {0xf040, 0xf07e},
                new UInt16 [2] {0xf0a1, 0xf0fe},
                new UInt16 [2] {0xf140, 0xf17e},
                new UInt16 [2] {0xf1a1, 0xf1fe},
                new UInt16 [2] {0xf240, 0xf27e},
                new UInt16 [2] {0xf2a1, 0xf2fe},
                new UInt16 [2] {0xf340, 0xf37e},
                new UInt16 [2] {0xf3a1, 0xf3fe},
                new UInt16 [2] {0xf440, 0xf47e},
                new UInt16 [2] {0xf4a1, 0xf4fe},
                new UInt16 [2] {0xf540, 0xf57e},
                new UInt16 [2] {0xf5a1, 0xf5fe},
                new UInt16 [2] {0xf640, 0xf67e},
                new UInt16 [2] {0xf6a1, 0xf6fe},
                new UInt16 [2] {0xf740, 0xf77e},
                new UInt16 [2] {0xf7a1, 0xf7fe},
                new UInt16 [2] {0xf840, 0xf87e},
                new UInt16 [2] {0xf8a1, 0xf8fe},
                new UInt16 [2] {0xf940, 0xf97e},
                new UInt16 [2] {0xf9a1, 0xf9d5}
            };

            UInt16 [] rangeSizes = new UInt16 [rangeCt];

            UInt16 [] [] mapDataStd = new UInt16 [rangeCt] [];

            UInt16 rangeMin,
                   rangeMax;

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

            rangeNo = 0;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            //----------------------------------------------------------------//

            mapDataStd [rangeNo] [0xA140 - rangeMin] = 0x3000;
            mapDataStd [rangeNo] [0xA141 - rangeMin] = 0xFF0C;
            mapDataStd [rangeNo] [0xA142 - rangeMin] = 0x3001;
            mapDataStd [rangeNo] [0xA143 - rangeMin] = 0x3002;
            mapDataStd [rangeNo] [0xA144 - rangeMin] = 0xFF0E;
            mapDataStd [rangeNo] [0xA145 - rangeMin] = 0x2022;
            mapDataStd [rangeNo] [0xA146 - rangeMin] = 0xFF1B;
            mapDataStd [rangeNo] [0xA147 - rangeMin] = 0xFF1A;
            mapDataStd [rangeNo] [0xA148 - rangeMin] = 0xFF1F;
            mapDataStd [rangeNo] [0xA149 - rangeMin] = 0xFF01;
            mapDataStd [rangeNo] [0xA14A - rangeMin] = 0xFE30;
            mapDataStd [rangeNo] [0xA14B - rangeMin] = 0x2026;
            mapDataStd [rangeNo] [0xA14C - rangeMin] = 0x2025;
            mapDataStd [rangeNo] [0xA14D - rangeMin] = 0xFE50;
            mapDataStd [rangeNo] [0xA14E - rangeMin] = 0xFF64;
            mapDataStd [rangeNo] [0xA14F - rangeMin] = 0xFE52;

            mapDataStd [rangeNo] [0xA150 - rangeMin] = 0x00B7;
            mapDataStd [rangeNo] [0xA151 - rangeMin] = 0xFE54;
            mapDataStd [rangeNo] [0xA152 - rangeMin] = 0xFE55;
            mapDataStd [rangeNo] [0xA153 - rangeMin] = 0xFE56;
            mapDataStd [rangeNo] [0xA154 - rangeMin] = 0xFE57;
            mapDataStd [rangeNo] [0xA155 - rangeMin] = 0xFF5C;
            mapDataStd [rangeNo] [0xA156 - rangeMin] = 0x2013;
            mapDataStd [rangeNo] [0xA157 - rangeMin] = 0xFE31;
            mapDataStd [rangeNo] [0xA158 - rangeMin] = 0x2014;
            mapDataStd [rangeNo] [0xA159 - rangeMin] = 0xFE33;
            mapDataStd [rangeNo] [0xA15A - rangeMin] = 0xFFFD;
            mapDataStd [rangeNo] [0xA15B - rangeMin] = 0xFE34;
            mapDataStd [rangeNo] [0xA15C - rangeMin] = 0xFE4F;
            mapDataStd [rangeNo] [0xA15D - rangeMin] = 0xFF08;
            mapDataStd [rangeNo] [0xA15E - rangeMin] = 0xFF09;
            mapDataStd [rangeNo] [0xA15F - rangeMin] = 0xFE35;

            mapDataStd [rangeNo] [0xA160 - rangeMin] = 0xFE36;
            mapDataStd [rangeNo] [0xA161 - rangeMin] = 0xFF5B;
            mapDataStd [rangeNo] [0xA162 - rangeMin] = 0xFF5D;
            mapDataStd [rangeNo] [0xA163 - rangeMin] = 0xFE37;
            mapDataStd [rangeNo] [0xA164 - rangeMin] = 0xFE38;
            mapDataStd [rangeNo] [0xA165 - rangeMin] = 0x3014;
            mapDataStd [rangeNo] [0xA166 - rangeMin] = 0x3015;
            mapDataStd [rangeNo] [0xA167 - rangeMin] = 0xFE39;
            mapDataStd [rangeNo] [0xA168 - rangeMin] = 0xFE3A;
            mapDataStd [rangeNo] [0xA169 - rangeMin] = 0x3010;
            mapDataStd [rangeNo] [0xA16A - rangeMin] = 0x3011;
            mapDataStd [rangeNo] [0xA16B - rangeMin] = 0xFE3B;
            mapDataStd [rangeNo] [0xA16C - rangeMin] = 0xFE3C;
            mapDataStd [rangeNo] [0xA16D - rangeMin] = 0x300A;
            mapDataStd [rangeNo] [0xA16E - rangeMin] = 0x300B;
            mapDataStd [rangeNo] [0xA16F - rangeMin] = 0xFE3D;

            mapDataStd [rangeNo] [0xA170 - rangeMin] = 0xFE3E;
            mapDataStd [rangeNo] [0xA171 - rangeMin] = 0x3008;
            mapDataStd [rangeNo] [0xA172 - rangeMin] = 0x3009;
            mapDataStd [rangeNo] [0xA173 - rangeMin] = 0xFE3F;
            mapDataStd [rangeNo] [0xA174 - rangeMin] = 0xFE40;
            mapDataStd [rangeNo] [0xA175 - rangeMin] = 0x300C;
            mapDataStd [rangeNo] [0xA176 - rangeMin] = 0x300D;
            mapDataStd [rangeNo] [0xA177 - rangeMin] = 0xFE41;
            mapDataStd [rangeNo] [0xA178 - rangeMin] = 0xFE42;
            mapDataStd [rangeNo] [0xA179 - rangeMin] = 0x300E;
            mapDataStd [rangeNo] [0xA17A - rangeMin] = 0x300F;
            mapDataStd [rangeNo] [0xA17B - rangeMin] = 0xFE43;
            mapDataStd [rangeNo] [0xA17C - rangeMin] = 0xFE44;
            mapDataStd [rangeNo] [0xA17D - rangeMin] = 0xFE59;
            mapDataStd [rangeNo] [0xA17E - rangeMin] = 0xFE5A;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xA1A1 - rangeMin] = 0xFE5B;
            mapDataStd [rangeNo] [0xA1A2 - rangeMin] = 0xFE5C;
            mapDataStd [rangeNo] [0xA1A3 - rangeMin] = 0xFE5D;
            mapDataStd [rangeNo] [0xA1A4 - rangeMin] = 0xFE5E;
            mapDataStd [rangeNo] [0xA1A5 - rangeMin] = 0x2018;
            mapDataStd [rangeNo] [0xA1A6 - rangeMin] = 0x2019;
            mapDataStd [rangeNo] [0xA1A7 - rangeMin] = 0x201C;
            mapDataStd [rangeNo] [0xA1A8 - rangeMin] = 0x201D;
            mapDataStd [rangeNo] [0xA1A9 - rangeMin] = 0x301D;
            mapDataStd [rangeNo] [0xA1AA - rangeMin] = 0x301E;
            mapDataStd [rangeNo] [0xA1AB - rangeMin] = 0x2035;
            mapDataStd [rangeNo] [0xA1AC - rangeMin] = 0x2032;
            mapDataStd [rangeNo] [0xA1AD - rangeMin] = 0xFF03;
            mapDataStd [rangeNo] [0xA1AE - rangeMin] = 0xFF06;
            mapDataStd [rangeNo] [0xA1AF - rangeMin] = 0xFF0A;

            mapDataStd [rangeNo] [0xA1B0 - rangeMin] = 0x203B;
            mapDataStd [rangeNo] [0xA1B1 - rangeMin] = 0x00A7;
            mapDataStd [rangeNo] [0xA1B2 - rangeMin] = 0x3003;
            mapDataStd [rangeNo] [0xA1B3 - rangeMin] = 0x25CB;
            mapDataStd [rangeNo] [0xA1B4 - rangeMin] = 0x25CF;
            mapDataStd [rangeNo] [0xA1B5 - rangeMin] = 0x25B3;
            mapDataStd [rangeNo] [0xA1B6 - rangeMin] = 0x25B2;
            mapDataStd [rangeNo] [0xA1B7 - rangeMin] = 0x25CE;
            mapDataStd [rangeNo] [0xA1B8 - rangeMin] = 0x2606;
            mapDataStd [rangeNo] [0xA1B9 - rangeMin] = 0x2605;
            mapDataStd [rangeNo] [0xA1BA - rangeMin] = 0x25C7;
            mapDataStd [rangeNo] [0xA1BB - rangeMin] = 0x25C6;
            mapDataStd [rangeNo] [0xA1BC - rangeMin] = 0x25A1;
            mapDataStd [rangeNo] [0xA1BD - rangeMin] = 0x25A0;
            mapDataStd [rangeNo] [0xA1BE - rangeMin] = 0x25BD;
            mapDataStd [rangeNo] [0xA1BF - rangeMin] = 0x25BC;

            mapDataStd [rangeNo] [0xA1C0 - rangeMin] = 0x32A3;
            mapDataStd [rangeNo] [0xA1C1 - rangeMin] = 0x2105;
            mapDataStd [rangeNo] [0xA1C2 - rangeMin] = 0x203E;
            mapDataStd [rangeNo] [0xA1C3 - rangeMin] = 0xFFFD;
            mapDataStd [rangeNo] [0xA1C4 - rangeMin] = 0xFF3F;
            mapDataStd [rangeNo] [0xA1C5 - rangeMin] = 0xFFFD;
            mapDataStd [rangeNo] [0xA1C6 - rangeMin] = 0xFE49;
            mapDataStd [rangeNo] [0xA1C7 - rangeMin] = 0xFE4A;
            mapDataStd [rangeNo] [0xA1C8 - rangeMin] = 0xFE4D;
            mapDataStd [rangeNo] [0xA1C9 - rangeMin] = 0xFE4E;
            mapDataStd [rangeNo] [0xA1CA - rangeMin] = 0xFE4B;
            mapDataStd [rangeNo] [0xA1CB - rangeMin] = 0xFE4C;
            mapDataStd [rangeNo] [0xA1CC - rangeMin] = 0xFE5F;
            mapDataStd [rangeNo] [0xA1CD - rangeMin] = 0xFE60;
            mapDataStd [rangeNo] [0xA1CE - rangeMin] = 0xFE61;
            mapDataStd [rangeNo] [0xA1CF - rangeMin] = 0xFF0B;

            mapDataStd [rangeNo] [0xA1D0 - rangeMin] = 0xFF0D;
            mapDataStd [rangeNo] [0xA1D1 - rangeMin] = 0x00D7;
            mapDataStd [rangeNo] [0xA1D2 - rangeMin] = 0x00F7;
            mapDataStd [rangeNo] [0xA1D3 - rangeMin] = 0x00B1;
            mapDataStd [rangeNo] [0xA1D4 - rangeMin] = 0x221A;
            mapDataStd [rangeNo] [0xA1D5 - rangeMin] = 0xFF1C;
            mapDataStd [rangeNo] [0xA1D6 - rangeMin] = 0xFF1E;
            mapDataStd [rangeNo] [0xA1D7 - rangeMin] = 0xFF1D;
            mapDataStd [rangeNo] [0xA1D8 - rangeMin] = 0x2266;
            mapDataStd [rangeNo] [0xA1D9 - rangeMin] = 0x2267;
            mapDataStd [rangeNo] [0xA1DA - rangeMin] = 0x2260;
            mapDataStd [rangeNo] [0xA1DB - rangeMin] = 0x221E;
            mapDataStd [rangeNo] [0xA1DC - rangeMin] = 0x2252;
            mapDataStd [rangeNo] [0xA1DD - rangeMin] = 0x2261;
            mapDataStd [rangeNo] [0xA1DE - rangeMin] = 0xFE62;
            mapDataStd [rangeNo] [0xA1DF - rangeMin] = 0xFE63;

            mapDataStd [rangeNo] [0xA1E0 - rangeMin] = 0xFE64;
            mapDataStd [rangeNo] [0xA1E1 - rangeMin] = 0xFE65;
            mapDataStd [rangeNo] [0xA1E2 - rangeMin] = 0xFE66;
            mapDataStd [rangeNo] [0xA1E3 - rangeMin] = 0x223C;
            mapDataStd [rangeNo] [0xA1E4 - rangeMin] = 0x2229;
            mapDataStd [rangeNo] [0xA1E5 - rangeMin] = 0x222A;
            mapDataStd [rangeNo] [0xA1E6 - rangeMin] = 0x22A5;
            mapDataStd [rangeNo] [0xA1E7 - rangeMin] = 0x2220;
            mapDataStd [rangeNo] [0xA1E8 - rangeMin] = 0x221F;
            mapDataStd [rangeNo] [0xA1E9 - rangeMin] = 0x22BF;
            mapDataStd [rangeNo] [0xA1EA - rangeMin] = 0x33D2;
            mapDataStd [rangeNo] [0xA1EB - rangeMin] = 0x33D1;
            mapDataStd [rangeNo] [0xA1EC - rangeMin] = 0x222B;
            mapDataStd [rangeNo] [0xA1ED - rangeMin] = 0x222E;
            mapDataStd [rangeNo] [0xA1EE - rangeMin] = 0x2235;
            mapDataStd [rangeNo] [0xA1EF - rangeMin] = 0x2234;

            mapDataStd [rangeNo] [0xA1F0 - rangeMin] = 0x2640;
            mapDataStd [rangeNo] [0xA1F1 - rangeMin] = 0x2642;
            mapDataStd [rangeNo] [0xA1F2 - rangeMin] = 0x2641;
            mapDataStd [rangeNo] [0xA1F3 - rangeMin] = 0x2609;
            mapDataStd [rangeNo] [0xA1F4 - rangeMin] = 0x2191;
            mapDataStd [rangeNo] [0xA1F5 - rangeMin] = 0x2193;
            mapDataStd [rangeNo] [0xA1F6 - rangeMin] = 0x2190;
            mapDataStd [rangeNo] [0xA1F7 - rangeMin] = 0x2192;
            mapDataStd [rangeNo] [0xA1F8 - rangeMin] = 0x2196;
            mapDataStd [rangeNo] [0xA1F9 - rangeMin] = 0x2197;
            mapDataStd [rangeNo] [0xA1FA - rangeMin] = 0x2199;
            mapDataStd [rangeNo] [0xA1FB - rangeMin] = 0x2198;
            mapDataStd [rangeNo] [0xA1FC - rangeMin] = 0x2225;
            mapDataStd [rangeNo] [0xA1FD - rangeMin] = 0x2223;
            mapDataStd [rangeNo] [0xA1FE - rangeMin] = 0xFFFD;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xA240 - rangeMin] = 0xFFFD;
            mapDataStd [rangeNo] [0xA241 - rangeMin] = 0xFF0F;
            mapDataStd [rangeNo] [0xA242 - rangeMin] = 0xFF3C;
            mapDataStd [rangeNo] [0xA243 - rangeMin] = 0xFF04;
            mapDataStd [rangeNo] [0xA244 - rangeMin] = 0x00A5;
            mapDataStd [rangeNo] [0xA245 - rangeMin] = 0x3012;
            mapDataStd [rangeNo] [0xA246 - rangeMin] = 0x00A2;
            mapDataStd [rangeNo] [0xA247 - rangeMin] = 0x00A3;
            mapDataStd [rangeNo] [0xA248 - rangeMin] = 0xFF05;
            mapDataStd [rangeNo] [0xA249 - rangeMin] = 0xFF20;
            mapDataStd [rangeNo] [0xA24A - rangeMin] = 0x2103;
            mapDataStd [rangeNo] [0xA24B - rangeMin] = 0x2109;
            mapDataStd [rangeNo] [0xA24C - rangeMin] = 0xFE69;
            mapDataStd [rangeNo] [0xA24D - rangeMin] = 0xFE6A;
            mapDataStd [rangeNo] [0xA24E - rangeMin] = 0xFE6B;
            mapDataStd [rangeNo] [0xA24F - rangeMin] = 0x33D5;

            mapDataStd [rangeNo] [0xA250 - rangeMin] = 0x339C;
            mapDataStd [rangeNo] [0xA251 - rangeMin] = 0x339D;
            mapDataStd [rangeNo] [0xA252 - rangeMin] = 0x339E;
            mapDataStd [rangeNo] [0xA253 - rangeMin] = 0x33CE;
            mapDataStd [rangeNo] [0xA254 - rangeMin] = 0x33A1;
            mapDataStd [rangeNo] [0xA255 - rangeMin] = 0x338E;
            mapDataStd [rangeNo] [0xA256 - rangeMin] = 0x338F;
            mapDataStd [rangeNo] [0xA257 - rangeMin] = 0x33C4;
            mapDataStd [rangeNo] [0xA258 - rangeMin] = 0x00B0;
            mapDataStd [rangeNo] [0xA259 - rangeMin] = 0x5159;
            mapDataStd [rangeNo] [0xA25A - rangeMin] = 0x515B;
            mapDataStd [rangeNo] [0xA25B - rangeMin] = 0x515E;
            mapDataStd [rangeNo] [0xA25C - rangeMin] = 0x515D;
            mapDataStd [rangeNo] [0xA25D - rangeMin] = 0x5161;
            mapDataStd [rangeNo] [0xA25E - rangeMin] = 0x5163;
            mapDataStd [rangeNo] [0xA25F - rangeMin] = 0x55E7;

            mapDataStd [rangeNo] [0xA260 - rangeMin] = 0x74E9;
            mapDataStd [rangeNo] [0xA261 - rangeMin] = 0x7CCE;
            mapDataStd [rangeNo] [0xA262 - rangeMin] = 0x2581;
            mapDataStd [rangeNo] [0xA263 - rangeMin] = 0x2582;
            mapDataStd [rangeNo] [0xA264 - rangeMin] = 0x2583;
            mapDataStd [rangeNo] [0xA265 - rangeMin] = 0x2584;
            mapDataStd [rangeNo] [0xA266 - rangeMin] = 0x2585;
            mapDataStd [rangeNo] [0xA267 - rangeMin] = 0x2586;
            mapDataStd [rangeNo] [0xA268 - rangeMin] = 0x2587;
            mapDataStd [rangeNo] [0xA269 - rangeMin] = 0x2588;
            mapDataStd [rangeNo] [0xA26A - rangeMin] = 0x258F;
            mapDataStd [rangeNo] [0xA26B - rangeMin] = 0x258E;
            mapDataStd [rangeNo] [0xA26C - rangeMin] = 0x258D;
            mapDataStd [rangeNo] [0xA26D - rangeMin] = 0x258C;
            mapDataStd [rangeNo] [0xA26E - rangeMin] = 0x258B;
            mapDataStd [rangeNo] [0xA26F - rangeMin] = 0x258A;

            mapDataStd [rangeNo] [0xA270 - rangeMin] = 0x2589;
            mapDataStd [rangeNo] [0xA271 - rangeMin] = 0x253C;
            mapDataStd [rangeNo] [0xA272 - rangeMin] = 0x2534;
            mapDataStd [rangeNo] [0xA273 - rangeMin] = 0x252C;
            mapDataStd [rangeNo] [0xA274 - rangeMin] = 0x2524;
            mapDataStd [rangeNo] [0xA275 - rangeMin] = 0x251C;
            mapDataStd [rangeNo] [0xA276 - rangeMin] = 0x2594;
            mapDataStd [rangeNo] [0xA277 - rangeMin] = 0x2500;
            mapDataStd [rangeNo] [0xA278 - rangeMin] = 0x2502;
            mapDataStd [rangeNo] [0xA279 - rangeMin] = 0x2595;
            mapDataStd [rangeNo] [0xA27A - rangeMin] = 0x250C;
            mapDataStd [rangeNo] [0xA27B - rangeMin] = 0x2510;
            mapDataStd [rangeNo] [0xA27C - rangeMin] = 0x2514;
            mapDataStd [rangeNo] [0xA27D - rangeMin] = 0x2518;
            mapDataStd [rangeNo] [0xA27E - rangeMin] = 0x256D;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xA2A1 - rangeMin] = 0x256E;
            mapDataStd [rangeNo] [0xA2A2 - rangeMin] = 0x2570;
            mapDataStd [rangeNo] [0xA2A3 - rangeMin] = 0x256F;
            mapDataStd [rangeNo] [0xA2A4 - rangeMin] = 0x2550;
            mapDataStd [rangeNo] [0xA2A5 - rangeMin] = 0x255E;
            mapDataStd [rangeNo] [0xA2A6 - rangeMin] = 0x256A;
            mapDataStd [rangeNo] [0xA2A7 - rangeMin] = 0x2561;
            mapDataStd [rangeNo] [0xA2A8 - rangeMin] = 0x25E2;
            mapDataStd [rangeNo] [0xA2A9 - rangeMin] = 0x25E3;
            mapDataStd [rangeNo] [0xA2AA - rangeMin] = 0x25E5;
            mapDataStd [rangeNo] [0xA2AB - rangeMin] = 0x25E4;
            mapDataStd [rangeNo] [0xA2AC - rangeMin] = 0x2571;
            mapDataStd [rangeNo] [0xA2AD - rangeMin] = 0x2572;
            mapDataStd [rangeNo] [0xA2AE - rangeMin] = 0x2573;
            mapDataStd [rangeNo] [0xA2AF - rangeMin] = 0xFF10;

            mapDataStd [rangeNo] [0xA2B0 - rangeMin] = 0xFF11;
            mapDataStd [rangeNo] [0xA2B1 - rangeMin] = 0xFF12;
            mapDataStd [rangeNo] [0xA2B2 - rangeMin] = 0xFF13;
            mapDataStd [rangeNo] [0xA2B3 - rangeMin] = 0xFF14;
            mapDataStd [rangeNo] [0xA2B4 - rangeMin] = 0xFF15;
            mapDataStd [rangeNo] [0xA2B5 - rangeMin] = 0xFF16;
            mapDataStd [rangeNo] [0xA2B6 - rangeMin] = 0xFF17;
            mapDataStd [rangeNo] [0xA2B7 - rangeMin] = 0xFF18;
            mapDataStd [rangeNo] [0xA2B8 - rangeMin] = 0xFF19;
            mapDataStd [rangeNo] [0xA2B9 - rangeMin] = 0x2160;
            mapDataStd [rangeNo] [0xA2BA - rangeMin] = 0x2161;
            mapDataStd [rangeNo] [0xA2BB - rangeMin] = 0x2162;
            mapDataStd [rangeNo] [0xA2BC - rangeMin] = 0x2163;
            mapDataStd [rangeNo] [0xA2BD - rangeMin] = 0x2164;
            mapDataStd [rangeNo] [0xA2BE - rangeMin] = 0x2165;
            mapDataStd [rangeNo] [0xA2BF - rangeMin] = 0x2166;

            mapDataStd [rangeNo] [0xA2C0 - rangeMin] = 0x2167;
            mapDataStd [rangeNo] [0xA2C1 - rangeMin] = 0x2168;
            mapDataStd [rangeNo] [0xA2C2 - rangeMin] = 0x2169;
            mapDataStd [rangeNo] [0xA2C3 - rangeMin] = 0x3021;
            mapDataStd [rangeNo] [0xA2C4 - rangeMin] = 0x3022;
            mapDataStd [rangeNo] [0xA2C5 - rangeMin] = 0x3023;
            mapDataStd [rangeNo] [0xA2C6 - rangeMin] = 0x3024;
            mapDataStd [rangeNo] [0xA2C7 - rangeMin] = 0x3025;
            mapDataStd [rangeNo] [0xA2C8 - rangeMin] = 0x3026;
            mapDataStd [rangeNo] [0xA2C9 - rangeMin] = 0x3027;
            mapDataStd [rangeNo] [0xA2CA - rangeMin] = 0x3028;
            mapDataStd [rangeNo] [0xA2CB - rangeMin] = 0x3029;
            mapDataStd [rangeNo] [0xA2CC - rangeMin] = 0xFFFD;
            mapDataStd [rangeNo] [0xA2CD - rangeMin] = 0x5344;
            mapDataStd [rangeNo] [0xA2CE - rangeMin] = 0xFFFD;
            mapDataStd [rangeNo] [0xA2CF - rangeMin] = 0xFF21;

            mapDataStd [rangeNo] [0xA2D0 - rangeMin] = 0xFF22;
            mapDataStd [rangeNo] [0xA2D1 - rangeMin] = 0xFF23;
            mapDataStd [rangeNo] [0xA2D2 - rangeMin] = 0xFF24;
            mapDataStd [rangeNo] [0xA2D3 - rangeMin] = 0xFF25;
            mapDataStd [rangeNo] [0xA2D4 - rangeMin] = 0xFF26;
            mapDataStd [rangeNo] [0xA2D5 - rangeMin] = 0xFF27;
            mapDataStd [rangeNo] [0xA2D6 - rangeMin] = 0xFF28;
            mapDataStd [rangeNo] [0xA2D7 - rangeMin] = 0xFF29;
            mapDataStd [rangeNo] [0xA2D8 - rangeMin] = 0xFF2A;
            mapDataStd [rangeNo] [0xA2D9 - rangeMin] = 0xFF2B;
            mapDataStd [rangeNo] [0xA2DA - rangeMin] = 0xFF2C;
            mapDataStd [rangeNo] [0xA2DB - rangeMin] = 0xFF2D;
            mapDataStd [rangeNo] [0xA2DC - rangeMin] = 0xFF2E;
            mapDataStd [rangeNo] [0xA2DD - rangeMin] = 0xFF2F;
            mapDataStd [rangeNo] [0xA2DE - rangeMin] = 0xFF30;
            mapDataStd [rangeNo] [0xA2DF - rangeMin] = 0xFF31;

            mapDataStd [rangeNo] [0xA2E0 - rangeMin] = 0xFF32;
            mapDataStd [rangeNo] [0xA2E1 - rangeMin] = 0xFF33;
            mapDataStd [rangeNo] [0xA2E2 - rangeMin] = 0xFF34;
            mapDataStd [rangeNo] [0xA2E3 - rangeMin] = 0xFF35;
            mapDataStd [rangeNo] [0xA2E4 - rangeMin] = 0xFF36;
            mapDataStd [rangeNo] [0xA2E5 - rangeMin] = 0xFF37;
            mapDataStd [rangeNo] [0xA2E6 - rangeMin] = 0xFF38;
            mapDataStd [rangeNo] [0xA2E7 - rangeMin] = 0xFF39;
            mapDataStd [rangeNo] [0xA2E8 - rangeMin] = 0xFF3A;
            mapDataStd [rangeNo] [0xA2E9 - rangeMin] = 0xFF41;
            mapDataStd [rangeNo] [0xA2EA - rangeMin] = 0xFF42;
            mapDataStd [rangeNo] [0xA2EB - rangeMin] = 0xFF43;
            mapDataStd [rangeNo] [0xA2EC - rangeMin] = 0xFF44;
            mapDataStd [rangeNo] [0xA2ED - rangeMin] = 0xFF45;
            mapDataStd [rangeNo] [0xA2EE - rangeMin] = 0xFF46;
            mapDataStd [rangeNo] [0xA2EF - rangeMin] = 0xFF47;

            mapDataStd [rangeNo] [0xA2F0 - rangeMin] = 0xFF48;
            mapDataStd [rangeNo] [0xA2F1 - rangeMin] = 0xFF49;
            mapDataStd [rangeNo] [0xA2F2 - rangeMin] = 0xFF4A;
            mapDataStd [rangeNo] [0xA2F3 - rangeMin] = 0xFF4B;
            mapDataStd [rangeNo] [0xA2F4 - rangeMin] = 0xFF4C;
            mapDataStd [rangeNo] [0xA2F5 - rangeMin] = 0xFF4D;
            mapDataStd [rangeNo] [0xA2F6 - rangeMin] = 0xFF4E;
            mapDataStd [rangeNo] [0xA2F7 - rangeMin] = 0xFF4F;
            mapDataStd [rangeNo] [0xA2F8 - rangeMin] = 0xFF50;
            mapDataStd [rangeNo] [0xA2F9 - rangeMin] = 0xFF51;
            mapDataStd [rangeNo] [0xA2FA - rangeMin] = 0xFF52;
            mapDataStd [rangeNo] [0xA2FB - rangeMin] = 0xFF53;
            mapDataStd [rangeNo] [0xA2FC - rangeMin] = 0xFF54;
            mapDataStd [rangeNo] [0xA2FD - rangeMin] = 0xFF55;
            mapDataStd [rangeNo] [0xA2FE - rangeMin] = 0xFF56;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xA340 - rangeMin] = 0xFF57;
            mapDataStd [rangeNo] [0xA341 - rangeMin] = 0xFF58;
            mapDataStd [rangeNo] [0xA342 - rangeMin] = 0xFF59;
            mapDataStd [rangeNo] [0xA343 - rangeMin] = 0xFF5A;
            mapDataStd [rangeNo] [0xA344 - rangeMin] = 0x0391;
            mapDataStd [rangeNo] [0xA345 - rangeMin] = 0x0392;
            mapDataStd [rangeNo] [0xA346 - rangeMin] = 0x0393;
            mapDataStd [rangeNo] [0xA347 - rangeMin] = 0x0394;
            mapDataStd [rangeNo] [0xA348 - rangeMin] = 0x0395;
            mapDataStd [rangeNo] [0xA349 - rangeMin] = 0x0396;
            mapDataStd [rangeNo] [0xA34A - rangeMin] = 0x0397;
            mapDataStd [rangeNo] [0xA34B - rangeMin] = 0x0398;
            mapDataStd [rangeNo] [0xA34C - rangeMin] = 0x0399;
            mapDataStd [rangeNo] [0xA34D - rangeMin] = 0x039A;
            mapDataStd [rangeNo] [0xA34E - rangeMin] = 0x039B;
            mapDataStd [rangeNo] [0xA34F - rangeMin] = 0x039C;

            mapDataStd [rangeNo] [0xA350 - rangeMin] = 0x039D;
            mapDataStd [rangeNo] [0xA351 - rangeMin] = 0x039E;
            mapDataStd [rangeNo] [0xA352 - rangeMin] = 0x039F;
            mapDataStd [rangeNo] [0xA353 - rangeMin] = 0x03A0;
            mapDataStd [rangeNo] [0xA354 - rangeMin] = 0x03A1;
            mapDataStd [rangeNo] [0xA355 - rangeMin] = 0x03A3;
            mapDataStd [rangeNo] [0xA356 - rangeMin] = 0x03A4;
            mapDataStd [rangeNo] [0xA357 - rangeMin] = 0x03A5;
            mapDataStd [rangeNo] [0xA358 - rangeMin] = 0x03A6;
            mapDataStd [rangeNo] [0xA359 - rangeMin] = 0x03A7;
            mapDataStd [rangeNo] [0xA35A - rangeMin] = 0x03A8;
            mapDataStd [rangeNo] [0xA35B - rangeMin] = 0x03A9;
            mapDataStd [rangeNo] [0xA35C - rangeMin] = 0x03B1;
            mapDataStd [rangeNo] [0xA35D - rangeMin] = 0x03B2;
            mapDataStd [rangeNo] [0xA35E - rangeMin] = 0x03B3;
            mapDataStd [rangeNo] [0xA35F - rangeMin] = 0x03B4;

            mapDataStd [rangeNo] [0xA360 - rangeMin] = 0x03B5;
            mapDataStd [rangeNo] [0xA361 - rangeMin] = 0x03B6;
            mapDataStd [rangeNo] [0xA362 - rangeMin] = 0x03B7;
            mapDataStd [rangeNo] [0xA363 - rangeMin] = 0x03B8;
            mapDataStd [rangeNo] [0xA364 - rangeMin] = 0x03B9;
            mapDataStd [rangeNo] [0xA365 - rangeMin] = 0x03BA;
            mapDataStd [rangeNo] [0xA366 - rangeMin] = 0x03BB;
            mapDataStd [rangeNo] [0xA367 - rangeMin] = 0x03BC;
            mapDataStd [rangeNo] [0xA368 - rangeMin] = 0x03BD;
            mapDataStd [rangeNo] [0xA369 - rangeMin] = 0x03BE;
            mapDataStd [rangeNo] [0xA36A - rangeMin] = 0x03BF;
            mapDataStd [rangeNo] [0xA36B - rangeMin] = 0x03C0;
            mapDataStd [rangeNo] [0xA36C - rangeMin] = 0x03C1;
            mapDataStd [rangeNo] [0xA36D - rangeMin] = 0x03C3;
            mapDataStd [rangeNo] [0xA36E - rangeMin] = 0x03C4;
            mapDataStd [rangeNo] [0xA36F - rangeMin] = 0x03C5;

            mapDataStd [rangeNo] [0xA370 - rangeMin] = 0x03C6;
            mapDataStd [rangeNo] [0xA371 - rangeMin] = 0x03C7;
            mapDataStd [rangeNo] [0xA372 - rangeMin] = 0x03C8;
            mapDataStd [rangeNo] [0xA373 - rangeMin] = 0x03C9;
            mapDataStd [rangeNo] [0xA374 - rangeMin] = 0x3105;
            mapDataStd [rangeNo] [0xA375 - rangeMin] = 0x3106;
            mapDataStd [rangeNo] [0xA376 - rangeMin] = 0x3107;
            mapDataStd [rangeNo] [0xA377 - rangeMin] = 0x3108;
            mapDataStd [rangeNo] [0xA378 - rangeMin] = 0x3109;
            mapDataStd [rangeNo] [0xA379 - rangeMin] = 0x310A;
            mapDataStd [rangeNo] [0xA37A - rangeMin] = 0x310B;
            mapDataStd [rangeNo] [0xA37B - rangeMin] = 0x310C;
            mapDataStd [rangeNo] [0xA37C - rangeMin] = 0x310D;
            mapDataStd [rangeNo] [0xA37D - rangeMin] = 0x310E;
            mapDataStd [rangeNo] [0xA37E - rangeMin] = 0x310F;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xA3A1 - rangeMin] = 0x3110;
            mapDataStd [rangeNo] [0xA3A2 - rangeMin] = 0x3111;
            mapDataStd [rangeNo] [0xA3A3 - rangeMin] = 0x3112;
            mapDataStd [rangeNo] [0xA3A4 - rangeMin] = 0x3113;
            mapDataStd [rangeNo] [0xA3A5 - rangeMin] = 0x3114;
            mapDataStd [rangeNo] [0xA3A6 - rangeMin] = 0x3115;
            mapDataStd [rangeNo] [0xA3A7 - rangeMin] = 0x3116;
            mapDataStd [rangeNo] [0xA3A8 - rangeMin] = 0x3117;
            mapDataStd [rangeNo] [0xA3A9 - rangeMin] = 0x3118;
            mapDataStd [rangeNo] [0xA3AA - rangeMin] = 0x3119;
            mapDataStd [rangeNo] [0xA3AB - rangeMin] = 0x311A;
            mapDataStd [rangeNo] [0xA3AC - rangeMin] = 0x311B;
            mapDataStd [rangeNo] [0xA3AD - rangeMin] = 0x311C;
            mapDataStd [rangeNo] [0xA3AE - rangeMin] = 0x311D;
            mapDataStd [rangeNo] [0xA3AF - rangeMin] = 0x311E;

            mapDataStd [rangeNo] [0xA3B0 - rangeMin] = 0x311F;
            mapDataStd [rangeNo] [0xA3B1 - rangeMin] = 0x3120;
            mapDataStd [rangeNo] [0xA3B2 - rangeMin] = 0x3121;
            mapDataStd [rangeNo] [0xA3B3 - rangeMin] = 0x3122;
            mapDataStd [rangeNo] [0xA3B4 - rangeMin] = 0x3123;
            mapDataStd [rangeNo] [0xA3B5 - rangeMin] = 0x3124;
            mapDataStd [rangeNo] [0xA3B6 - rangeMin] = 0x3125;
            mapDataStd [rangeNo] [0xA3B7 - rangeMin] = 0x3126;
            mapDataStd [rangeNo] [0xA3B8 - rangeMin] = 0x3127;
            mapDataStd [rangeNo] [0xA3B9 - rangeMin] = 0x3128;
            mapDataStd [rangeNo] [0xA3BA - rangeMin] = 0x3129;
            mapDataStd [rangeNo] [0xA3BB - rangeMin] = 0x02D9;
            mapDataStd [rangeNo] [0xA3BC - rangeMin] = 0x02C9;
            mapDataStd [rangeNo] [0xA3BD - rangeMin] = 0x02CA;
            mapDataStd [rangeNo] [0xA3BE - rangeMin] = 0x02C7;
            mapDataStd [rangeNo] [0xA3BF - rangeMin] = 0x02CB;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xA440 - rangeMin] = 0x4E00;
            mapDataStd [rangeNo] [0xA441 - rangeMin] = 0x4E59;
            mapDataStd [rangeNo] [0xA442 - rangeMin] = 0x4E01;
            mapDataStd [rangeNo] [0xA443 - rangeMin] = 0x4E03;
            mapDataStd [rangeNo] [0xA444 - rangeMin] = 0x4E43;
            mapDataStd [rangeNo] [0xA445 - rangeMin] = 0x4E5D;
            mapDataStd [rangeNo] [0xA446 - rangeMin] = 0x4E86;
            mapDataStd [rangeNo] [0xA447 - rangeMin] = 0x4E8C;
            mapDataStd [rangeNo] [0xA448 - rangeMin] = 0x4EBA;
            mapDataStd [rangeNo] [0xA449 - rangeMin] = 0x513F;
            mapDataStd [rangeNo] [0xA44A - rangeMin] = 0x5165;
            mapDataStd [rangeNo] [0xA44B - rangeMin] = 0x516B;
            mapDataStd [rangeNo] [0xA44C - rangeMin] = 0x51E0;
            mapDataStd [rangeNo] [0xA44D - rangeMin] = 0x5200;
            mapDataStd [rangeNo] [0xA44E - rangeMin] = 0x5201;
            mapDataStd [rangeNo] [0xA44F - rangeMin] = 0x529B;

            mapDataStd [rangeNo] [0xA450 - rangeMin] = 0x5315;
            mapDataStd [rangeNo] [0xA451 - rangeMin] = 0x5341;
            mapDataStd [rangeNo] [0xA452 - rangeMin] = 0x535C;
            mapDataStd [rangeNo] [0xA453 - rangeMin] = 0x53C8;
            mapDataStd [rangeNo] [0xA454 - rangeMin] = 0x4E09;
            mapDataStd [rangeNo] [0xA455 - rangeMin] = 0x4E0B;
            mapDataStd [rangeNo] [0xA456 - rangeMin] = 0x4E08;
            mapDataStd [rangeNo] [0xA457 - rangeMin] = 0x4E0A;
            mapDataStd [rangeNo] [0xA458 - rangeMin] = 0x4E2B;
            mapDataStd [rangeNo] [0xA459 - rangeMin] = 0x4E38;
            mapDataStd [rangeNo] [0xA45A - rangeMin] = 0x51E1;
            mapDataStd [rangeNo] [0xA45B - rangeMin] = 0x4E45;
            mapDataStd [rangeNo] [0xA45C - rangeMin] = 0x4E48;
            mapDataStd [rangeNo] [0xA45D - rangeMin] = 0x4E5F;
            mapDataStd [rangeNo] [0xA45E - rangeMin] = 0x4E5E;
            mapDataStd [rangeNo] [0xA45F - rangeMin] = 0x4E8E;

            mapDataStd [rangeNo] [0xA460 - rangeMin] = 0x4EA1;
            mapDataStd [rangeNo] [0xA461 - rangeMin] = 0x5140;
            mapDataStd [rangeNo] [0xA462 - rangeMin] = 0x5203;
            mapDataStd [rangeNo] [0xA463 - rangeMin] = 0x52FA;
            mapDataStd [rangeNo] [0xA464 - rangeMin] = 0x5343;
            mapDataStd [rangeNo] [0xA465 - rangeMin] = 0x53C9;
            mapDataStd [rangeNo] [0xA466 - rangeMin] = 0x53E3;
            mapDataStd [rangeNo] [0xA467 - rangeMin] = 0x571F;
            mapDataStd [rangeNo] [0xA468 - rangeMin] = 0x58EB;
            mapDataStd [rangeNo] [0xA469 - rangeMin] = 0x5915;
            mapDataStd [rangeNo] [0xA46A - rangeMin] = 0x5927;
            mapDataStd [rangeNo] [0xA46B - rangeMin] = 0x5973;
            mapDataStd [rangeNo] [0xA46C - rangeMin] = 0x5B50;
            mapDataStd [rangeNo] [0xA46D - rangeMin] = 0x5B51;
            mapDataStd [rangeNo] [0xA46E - rangeMin] = 0x5B53;
            mapDataStd [rangeNo] [0xA46F - rangeMin] = 0x5BF8;

            mapDataStd [rangeNo] [0xA470 - rangeMin] = 0x5C0F;
            mapDataStd [rangeNo] [0xA471 - rangeMin] = 0x5C22;
            mapDataStd [rangeNo] [0xA472 - rangeMin] = 0x5C38;
            mapDataStd [rangeNo] [0xA473 - rangeMin] = 0x5C71;
            mapDataStd [rangeNo] [0xA474 - rangeMin] = 0x5DDD;
            mapDataStd [rangeNo] [0xA475 - rangeMin] = 0x5DE5;
            mapDataStd [rangeNo] [0xA476 - rangeMin] = 0x5DF1;
            mapDataStd [rangeNo] [0xA477 - rangeMin] = 0x5DF2;
            mapDataStd [rangeNo] [0xA478 - rangeMin] = 0x5DF3;
            mapDataStd [rangeNo] [0xA479 - rangeMin] = 0x5DFE;
            mapDataStd [rangeNo] [0xA47A - rangeMin] = 0x5E72;
            mapDataStd [rangeNo] [0xA47B - rangeMin] = 0x5EFE;
            mapDataStd [rangeNo] [0xA47C - rangeMin] = 0x5F0B;
            mapDataStd [rangeNo] [0xA47D - rangeMin] = 0x5F13;
            mapDataStd [rangeNo] [0xA47E - rangeMin] = 0x624D;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xA4A1 - rangeMin] = 0x4E11;
            mapDataStd [rangeNo] [0xA4A2 - rangeMin] = 0x4E10;
            mapDataStd [rangeNo] [0xA4A3 - rangeMin] = 0x4E0D;
            mapDataStd [rangeNo] [0xA4A4 - rangeMin] = 0x4E2D;
            mapDataStd [rangeNo] [0xA4A5 - rangeMin] = 0x4E30;
            mapDataStd [rangeNo] [0xA4A6 - rangeMin] = 0x4E39;
            mapDataStd [rangeNo] [0xA4A7 - rangeMin] = 0x4E4B;
            mapDataStd [rangeNo] [0xA4A8 - rangeMin] = 0x5C39;
            mapDataStd [rangeNo] [0xA4A9 - rangeMin] = 0x4E88;
            mapDataStd [rangeNo] [0xA4AA - rangeMin] = 0x4E91;
            mapDataStd [rangeNo] [0xA4AB - rangeMin] = 0x4E95;
            mapDataStd [rangeNo] [0xA4AC - rangeMin] = 0x4E92;
            mapDataStd [rangeNo] [0xA4AD - rangeMin] = 0x4E94;
            mapDataStd [rangeNo] [0xA4AE - rangeMin] = 0x4EA2;
            mapDataStd [rangeNo] [0xA4AF - rangeMin] = 0x4EC1;

            mapDataStd [rangeNo] [0xA4B0 - rangeMin] = 0x4EC0;
            mapDataStd [rangeNo] [0xA4B1 - rangeMin] = 0x4EC3;
            mapDataStd [rangeNo] [0xA4B2 - rangeMin] = 0x4EC6;
            mapDataStd [rangeNo] [0xA4B3 - rangeMin] = 0x4EC7;
            mapDataStd [rangeNo] [0xA4B4 - rangeMin] = 0x4ECD;
            mapDataStd [rangeNo] [0xA4B5 - rangeMin] = 0x4ECA;
            mapDataStd [rangeNo] [0xA4B6 - rangeMin] = 0x4ECB;
            mapDataStd [rangeNo] [0xA4B7 - rangeMin] = 0x4EC4;
            mapDataStd [rangeNo] [0xA4B8 - rangeMin] = 0x5143;
            mapDataStd [rangeNo] [0xA4B9 - rangeMin] = 0x5141;
            mapDataStd [rangeNo] [0xA4BA - rangeMin] = 0x5167;
            mapDataStd [rangeNo] [0xA4BB - rangeMin] = 0x516D;
            mapDataStd [rangeNo] [0xA4BC - rangeMin] = 0x516E;
            mapDataStd [rangeNo] [0xA4BD - rangeMin] = 0x516C;
            mapDataStd [rangeNo] [0xA4BE - rangeMin] = 0x5197;
            mapDataStd [rangeNo] [0xA4BF - rangeMin] = 0x51F6;

            mapDataStd [rangeNo] [0xA4C0 - rangeMin] = 0x5206;
            mapDataStd [rangeNo] [0xA4C1 - rangeMin] = 0x5207;
            mapDataStd [rangeNo] [0xA4C2 - rangeMin] = 0x5208;
            mapDataStd [rangeNo] [0xA4C3 - rangeMin] = 0x52FB;
            mapDataStd [rangeNo] [0xA4C4 - rangeMin] = 0x52FE;
            mapDataStd [rangeNo] [0xA4C5 - rangeMin] = 0x52FF;
            mapDataStd [rangeNo] [0xA4C6 - rangeMin] = 0x5316;
            mapDataStd [rangeNo] [0xA4C7 - rangeMin] = 0x5339;
            mapDataStd [rangeNo] [0xA4C8 - rangeMin] = 0x5348;
            mapDataStd [rangeNo] [0xA4C9 - rangeMin] = 0x5347;
            mapDataStd [rangeNo] [0xA4CA - rangeMin] = 0x5345;
            mapDataStd [rangeNo] [0xA4CB - rangeMin] = 0x535E;
            mapDataStd [rangeNo] [0xA4CC - rangeMin] = 0x5384;
            mapDataStd [rangeNo] [0xA4CD - rangeMin] = 0x53CB;
            mapDataStd [rangeNo] [0xA4CE - rangeMin] = 0x53CA;
            mapDataStd [rangeNo] [0xA4CF - rangeMin] = 0x53CD;

            mapDataStd [rangeNo] [0xA4D0 - rangeMin] = 0x58EC;
            mapDataStd [rangeNo] [0xA4D1 - rangeMin] = 0x5929;
            mapDataStd [rangeNo] [0xA4D2 - rangeMin] = 0x592B;
            mapDataStd [rangeNo] [0xA4D3 - rangeMin] = 0x592A;
            mapDataStd [rangeNo] [0xA4D4 - rangeMin] = 0x592D;
            mapDataStd [rangeNo] [0xA4D5 - rangeMin] = 0x5B54;
            mapDataStd [rangeNo] [0xA4D6 - rangeMin] = 0x5C11;
            mapDataStd [rangeNo] [0xA4D7 - rangeMin] = 0x5C24;
            mapDataStd [rangeNo] [0xA4D8 - rangeMin] = 0x5C3A;
            mapDataStd [rangeNo] [0xA4D9 - rangeMin] = 0x5C6F;
            mapDataStd [rangeNo] [0xA4DA - rangeMin] = 0x5DF4;
            mapDataStd [rangeNo] [0xA4DB - rangeMin] = 0x5E7B;
            mapDataStd [rangeNo] [0xA4DC - rangeMin] = 0x5EFF;
            mapDataStd [rangeNo] [0xA4DD - rangeMin] = 0x5F14;
            mapDataStd [rangeNo] [0xA4DE - rangeMin] = 0x5F15;
            mapDataStd [rangeNo] [0xA4DF - rangeMin] = 0x5FC3;

            mapDataStd [rangeNo] [0xA4E0 - rangeMin] = 0x6208;
            mapDataStd [rangeNo] [0xA4E1 - rangeMin] = 0x6236;
            mapDataStd [rangeNo] [0xA4E2 - rangeMin] = 0x624B;
            mapDataStd [rangeNo] [0xA4E3 - rangeMin] = 0x624E;
            mapDataStd [rangeNo] [0xA4E4 - rangeMin] = 0x652F;
            mapDataStd [rangeNo] [0xA4E5 - rangeMin] = 0x6587;
            mapDataStd [rangeNo] [0xA4E6 - rangeMin] = 0x6597;
            mapDataStd [rangeNo] [0xA4E7 - rangeMin] = 0x65A4;
            mapDataStd [rangeNo] [0xA4E8 - rangeMin] = 0x65B9;
            mapDataStd [rangeNo] [0xA4E9 - rangeMin] = 0x65E5;
            mapDataStd [rangeNo] [0xA4EA - rangeMin] = 0x66F0;
            mapDataStd [rangeNo] [0xA4EB - rangeMin] = 0x6708;
            mapDataStd [rangeNo] [0xA4EC - rangeMin] = 0x6728;
            mapDataStd [rangeNo] [0xA4ED - rangeMin] = 0x6B20;
            mapDataStd [rangeNo] [0xA4EE - rangeMin] = 0x6B62;
            mapDataStd [rangeNo] [0xA4EF - rangeMin] = 0x6B79;

            mapDataStd [rangeNo] [0xA4F0 - rangeMin] = 0x6BCB;
            mapDataStd [rangeNo] [0xA4F1 - rangeMin] = 0x6BD4;
            mapDataStd [rangeNo] [0xA4F2 - rangeMin] = 0x6BDB;
            mapDataStd [rangeNo] [0xA4F3 - rangeMin] = 0x6C0F;
            mapDataStd [rangeNo] [0xA4F4 - rangeMin] = 0x6C34;
            mapDataStd [rangeNo] [0xA4F5 - rangeMin] = 0x706B;
            mapDataStd [rangeNo] [0xA4F6 - rangeMin] = 0x722A;
            mapDataStd [rangeNo] [0xA4F7 - rangeMin] = 0x7236;
            mapDataStd [rangeNo] [0xA4F8 - rangeMin] = 0x723B;
            mapDataStd [rangeNo] [0xA4F9 - rangeMin] = 0x7247;
            mapDataStd [rangeNo] [0xA4FA - rangeMin] = 0x7259;
            mapDataStd [rangeNo] [0xA4FB - rangeMin] = 0x725B;
            mapDataStd [rangeNo] [0xA4FC - rangeMin] = 0x72AC;
            mapDataStd [rangeNo] [0xA4FD - rangeMin] = 0x738B;
            mapDataStd [rangeNo] [0xA4FE - rangeMin] = 0x4E19;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xA540 - rangeMin] = 0x4E16;
            mapDataStd [rangeNo] [0xA541 - rangeMin] = 0x4E15;
            mapDataStd [rangeNo] [0xA542 - rangeMin] = 0x4E14;
            mapDataStd [rangeNo] [0xA543 - rangeMin] = 0x4E18;
            mapDataStd [rangeNo] [0xA544 - rangeMin] = 0x4E3B;
            mapDataStd [rangeNo] [0xA545 - rangeMin] = 0x4E4D;
            mapDataStd [rangeNo] [0xA546 - rangeMin] = 0x4E4F;
            mapDataStd [rangeNo] [0xA547 - rangeMin] = 0x4E4E;
            mapDataStd [rangeNo] [0xA548 - rangeMin] = 0x4EE5;
            mapDataStd [rangeNo] [0xA549 - rangeMin] = 0x4ED8;
            mapDataStd [rangeNo] [0xA54A - rangeMin] = 0x4ED4;
            mapDataStd [rangeNo] [0xA54B - rangeMin] = 0x4ED5;
            mapDataStd [rangeNo] [0xA54C - rangeMin] = 0x4ED6;
            mapDataStd [rangeNo] [0xA54D - rangeMin] = 0x4ED7;
            mapDataStd [rangeNo] [0xA54E - rangeMin] = 0x4EE3;
            mapDataStd [rangeNo] [0xA54F - rangeMin] = 0x4EE4;

            mapDataStd [rangeNo] [0xA550 - rangeMin] = 0x4ED9;
            mapDataStd [rangeNo] [0xA551 - rangeMin] = 0x4EDE;
            mapDataStd [rangeNo] [0xA552 - rangeMin] = 0x5145;
            mapDataStd [rangeNo] [0xA553 - rangeMin] = 0x5144;
            mapDataStd [rangeNo] [0xA554 - rangeMin] = 0x5189;
            mapDataStd [rangeNo] [0xA555 - rangeMin] = 0x518A;
            mapDataStd [rangeNo] [0xA556 - rangeMin] = 0x51AC;
            mapDataStd [rangeNo] [0xA557 - rangeMin] = 0x51F9;
            mapDataStd [rangeNo] [0xA558 - rangeMin] = 0x51FA;
            mapDataStd [rangeNo] [0xA559 - rangeMin] = 0x51F8;
            mapDataStd [rangeNo] [0xA55A - rangeMin] = 0x520A;
            mapDataStd [rangeNo] [0xA55B - rangeMin] = 0x52A0;
            mapDataStd [rangeNo] [0xA55C - rangeMin] = 0x529F;
            mapDataStd [rangeNo] [0xA55D - rangeMin] = 0x5305;
            mapDataStd [rangeNo] [0xA55E - rangeMin] = 0x5306;
            mapDataStd [rangeNo] [0xA55F - rangeMin] = 0x5317;

            mapDataStd [rangeNo] [0xA560 - rangeMin] = 0x531D;
            mapDataStd [rangeNo] [0xA561 - rangeMin] = 0x4EDF;
            mapDataStd [rangeNo] [0xA562 - rangeMin] = 0x534A;
            mapDataStd [rangeNo] [0xA563 - rangeMin] = 0x5349;
            mapDataStd [rangeNo] [0xA564 - rangeMin] = 0x5361;
            mapDataStd [rangeNo] [0xA565 - rangeMin] = 0x5360;
            mapDataStd [rangeNo] [0xA566 - rangeMin] = 0x536F;
            mapDataStd [rangeNo] [0xA567 - rangeMin] = 0x536E;
            mapDataStd [rangeNo] [0xA568 - rangeMin] = 0x53BB;
            mapDataStd [rangeNo] [0xA569 - rangeMin] = 0x53EF;
            mapDataStd [rangeNo] [0xA56A - rangeMin] = 0x53E4;
            mapDataStd [rangeNo] [0xA56B - rangeMin] = 0x53F3;
            mapDataStd [rangeNo] [0xA56C - rangeMin] = 0x53EC;
            mapDataStd [rangeNo] [0xA56D - rangeMin] = 0x53EE;
            mapDataStd [rangeNo] [0xA56E - rangeMin] = 0x53E9;
            mapDataStd [rangeNo] [0xA56F - rangeMin] = 0x53E8;

            mapDataStd [rangeNo] [0xA570 - rangeMin] = 0x53FC;
            mapDataStd [rangeNo] [0xA571 - rangeMin] = 0x53F8;
            mapDataStd [rangeNo] [0xA572 - rangeMin] = 0x53F5;
            mapDataStd [rangeNo] [0xA573 - rangeMin] = 0x53EB;
            mapDataStd [rangeNo] [0xA574 - rangeMin] = 0x53E6;
            mapDataStd [rangeNo] [0xA575 - rangeMin] = 0x53EA;
            mapDataStd [rangeNo] [0xA576 - rangeMin] = 0x53F2;
            mapDataStd [rangeNo] [0xA577 - rangeMin] = 0x53F1;
            mapDataStd [rangeNo] [0xA578 - rangeMin] = 0x53F0;
            mapDataStd [rangeNo] [0xA579 - rangeMin] = 0x53E5;
            mapDataStd [rangeNo] [0xA57A - rangeMin] = 0x53ED;
            mapDataStd [rangeNo] [0xA57B - rangeMin] = 0x53FB;
            mapDataStd [rangeNo] [0xA57C - rangeMin] = 0x56DB;
            mapDataStd [rangeNo] [0xA57D - rangeMin] = 0x56DA;
            mapDataStd [rangeNo] [0xA57E - rangeMin] = 0x5916;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xA5A1 - rangeMin] = 0x592E;
            mapDataStd [rangeNo] [0xA5A2 - rangeMin] = 0x5931;
            mapDataStd [rangeNo] [0xA5A3 - rangeMin] = 0x5974;
            mapDataStd [rangeNo] [0xA5A4 - rangeMin] = 0x5976;
            mapDataStd [rangeNo] [0xA5A5 - rangeMin] = 0x5B55;
            mapDataStd [rangeNo] [0xA5A6 - rangeMin] = 0x5B83;
            mapDataStd [rangeNo] [0xA5A7 - rangeMin] = 0x5C3C;
            mapDataStd [rangeNo] [0xA5A8 - rangeMin] = 0x5DE8;
            mapDataStd [rangeNo] [0xA5A9 - rangeMin] = 0x5DE7;
            mapDataStd [rangeNo] [0xA5AA - rangeMin] = 0x5DE6;
            mapDataStd [rangeNo] [0xA5AB - rangeMin] = 0x5E02;
            mapDataStd [rangeNo] [0xA5AC - rangeMin] = 0x5E03;
            mapDataStd [rangeNo] [0xA5AD - rangeMin] = 0x5E73;
            mapDataStd [rangeNo] [0xA5AE - rangeMin] = 0x5E7C;
            mapDataStd [rangeNo] [0xA5AF - rangeMin] = 0x5F01;

            mapDataStd [rangeNo] [0xA5B0 - rangeMin] = 0x5F18;
            mapDataStd [rangeNo] [0xA5B1 - rangeMin] = 0x5F17;
            mapDataStd [rangeNo] [0xA5B2 - rangeMin] = 0x5FC5;
            mapDataStd [rangeNo] [0xA5B3 - rangeMin] = 0x620A;
            mapDataStd [rangeNo] [0xA5B4 - rangeMin] = 0x6253;
            mapDataStd [rangeNo] [0xA5B5 - rangeMin] = 0x6254;
            mapDataStd [rangeNo] [0xA5B6 - rangeMin] = 0x6252;
            mapDataStd [rangeNo] [0xA5B7 - rangeMin] = 0x6251;
            mapDataStd [rangeNo] [0xA5B8 - rangeMin] = 0x65A5;
            mapDataStd [rangeNo] [0xA5B9 - rangeMin] = 0x65E6;
            mapDataStd [rangeNo] [0xA5BA - rangeMin] = 0x672E;
            mapDataStd [rangeNo] [0xA5BB - rangeMin] = 0x672C;
            mapDataStd [rangeNo] [0xA5BC - rangeMin] = 0x672A;
            mapDataStd [rangeNo] [0xA5BD - rangeMin] = 0x672B;
            mapDataStd [rangeNo] [0xA5BE - rangeMin] = 0x672D;
            mapDataStd [rangeNo] [0xA5BF - rangeMin] = 0x6B63;

            mapDataStd [rangeNo] [0xA5C0 - rangeMin] = 0x6BCD;
            mapDataStd [rangeNo] [0xA5C1 - rangeMin] = 0x6C11;
            mapDataStd [rangeNo] [0xA5C2 - rangeMin] = 0x6C10;
            mapDataStd [rangeNo] [0xA5C3 - rangeMin] = 0x6C38;
            mapDataStd [rangeNo] [0xA5C4 - rangeMin] = 0x6C41;
            mapDataStd [rangeNo] [0xA5C5 - rangeMin] = 0x6C40;
            mapDataStd [rangeNo] [0xA5C6 - rangeMin] = 0x6C3E;
            mapDataStd [rangeNo] [0xA5C7 - rangeMin] = 0x72AF;
            mapDataStd [rangeNo] [0xA5C8 - rangeMin] = 0x7384;
            mapDataStd [rangeNo] [0xA5C9 - rangeMin] = 0x7389;
            mapDataStd [rangeNo] [0xA5CA - rangeMin] = 0x74DC;
            mapDataStd [rangeNo] [0xA5CB - rangeMin] = 0x74E6;
            mapDataStd [rangeNo] [0xA5CC - rangeMin] = 0x7518;
            mapDataStd [rangeNo] [0xA5CD - rangeMin] = 0x751F;
            mapDataStd [rangeNo] [0xA5CE - rangeMin] = 0x7528;
            mapDataStd [rangeNo] [0xA5CF - rangeMin] = 0x7529;

            mapDataStd [rangeNo] [0xA5D0 - rangeMin] = 0x7530;
            mapDataStd [rangeNo] [0xA5D1 - rangeMin] = 0x7531;
            mapDataStd [rangeNo] [0xA5D2 - rangeMin] = 0x7532;
            mapDataStd [rangeNo] [0xA5D3 - rangeMin] = 0x7533;
            mapDataStd [rangeNo] [0xA5D4 - rangeMin] = 0x758B;
            mapDataStd [rangeNo] [0xA5D5 - rangeMin] = 0x767D;
            mapDataStd [rangeNo] [0xA5D6 - rangeMin] = 0x76AE;
            mapDataStd [rangeNo] [0xA5D7 - rangeMin] = 0x76BF;
            mapDataStd [rangeNo] [0xA5D8 - rangeMin] = 0x76EE;
            mapDataStd [rangeNo] [0xA5D9 - rangeMin] = 0x77DB;
            mapDataStd [rangeNo] [0xA5DA - rangeMin] = 0x77E2;
            mapDataStd [rangeNo] [0xA5DB - rangeMin] = 0x77F3;
            mapDataStd [rangeNo] [0xA5DC - rangeMin] = 0x793A;
            mapDataStd [rangeNo] [0xA5DD - rangeMin] = 0x79BE;
            mapDataStd [rangeNo] [0xA5DE - rangeMin] = 0x7A74;
            mapDataStd [rangeNo] [0xA5DF - rangeMin] = 0x7ACB;

            mapDataStd [rangeNo] [0xA5E0 - rangeMin] = 0x4E1E;
            mapDataStd [rangeNo] [0xA5E1 - rangeMin] = 0x4E1F;
            mapDataStd [rangeNo] [0xA5E2 - rangeMin] = 0x4E52;
            mapDataStd [rangeNo] [0xA5E3 - rangeMin] = 0x4E53;
            mapDataStd [rangeNo] [0xA5E4 - rangeMin] = 0x4E69;
            mapDataStd [rangeNo] [0xA5E5 - rangeMin] = 0x4E99;
            mapDataStd [rangeNo] [0xA5E6 - rangeMin] = 0x4EA4;
            mapDataStd [rangeNo] [0xA5E7 - rangeMin] = 0x4EA6;
            mapDataStd [rangeNo] [0xA5E8 - rangeMin] = 0x4EA5;
            mapDataStd [rangeNo] [0xA5E9 - rangeMin] = 0x4EFF;
            mapDataStd [rangeNo] [0xA5EA - rangeMin] = 0x4F09;
            mapDataStd [rangeNo] [0xA5EB - rangeMin] = 0x4F19;
            mapDataStd [rangeNo] [0xA5EC - rangeMin] = 0x4F0A;
            mapDataStd [rangeNo] [0xA5ED - rangeMin] = 0x4F15;
            mapDataStd [rangeNo] [0xA5EE - rangeMin] = 0x4F0D;
            mapDataStd [rangeNo] [0xA5EF - rangeMin] = 0x4F10;

            mapDataStd [rangeNo] [0xA5F0 - rangeMin] = 0x4F11;
            mapDataStd [rangeNo] [0xA5F1 - rangeMin] = 0x4F0F;
            mapDataStd [rangeNo] [0xA5F2 - rangeMin] = 0x4EF2;
            mapDataStd [rangeNo] [0xA5F3 - rangeMin] = 0x4EF6;
            mapDataStd [rangeNo] [0xA5F4 - rangeMin] = 0x4EFB;
            mapDataStd [rangeNo] [0xA5F5 - rangeMin] = 0x4EF0;
            mapDataStd [rangeNo] [0xA5F6 - rangeMin] = 0x4EF3;
            mapDataStd [rangeNo] [0xA5F7 - rangeMin] = 0x4EFD;
            mapDataStd [rangeNo] [0xA5F8 - rangeMin] = 0x4F01;
            mapDataStd [rangeNo] [0xA5F9 - rangeMin] = 0x4F0B;
            mapDataStd [rangeNo] [0xA5FA - rangeMin] = 0x5149;
            mapDataStd [rangeNo] [0xA5FB - rangeMin] = 0x5147;
            mapDataStd [rangeNo] [0xA5FC - rangeMin] = 0x5146;
            mapDataStd [rangeNo] [0xA5FD - rangeMin] = 0x5148;
            mapDataStd [rangeNo] [0xA5FE - rangeMin] = 0x5168;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xA640 - rangeMin] = 0x5171;
            mapDataStd [rangeNo] [0xA641 - rangeMin] = 0x518D;
            mapDataStd [rangeNo] [0xA642 - rangeMin] = 0x51B0;
            mapDataStd [rangeNo] [0xA643 - rangeMin] = 0x5217;
            mapDataStd [rangeNo] [0xA644 - rangeMin] = 0x5211;
            mapDataStd [rangeNo] [0xA645 - rangeMin] = 0x5212;
            mapDataStd [rangeNo] [0xA646 - rangeMin] = 0x520E;
            mapDataStd [rangeNo] [0xA647 - rangeMin] = 0x5216;
            mapDataStd [rangeNo] [0xA648 - rangeMin] = 0x52A3;
            mapDataStd [rangeNo] [0xA649 - rangeMin] = 0x5308;
            mapDataStd [rangeNo] [0xA64A - rangeMin] = 0x5321;
            mapDataStd [rangeNo] [0xA64B - rangeMin] = 0x5320;
            mapDataStd [rangeNo] [0xA64C - rangeMin] = 0x5370;
            mapDataStd [rangeNo] [0xA64D - rangeMin] = 0x5371;
            mapDataStd [rangeNo] [0xA64E - rangeMin] = 0x5409;
            mapDataStd [rangeNo] [0xA64F - rangeMin] = 0x540F;

            mapDataStd [rangeNo] [0xA650 - rangeMin] = 0x540C;
            mapDataStd [rangeNo] [0xA651 - rangeMin] = 0x540A;
            mapDataStd [rangeNo] [0xA652 - rangeMin] = 0x5410;
            mapDataStd [rangeNo] [0xA653 - rangeMin] = 0x5401;
            mapDataStd [rangeNo] [0xA654 - rangeMin] = 0x540B;
            mapDataStd [rangeNo] [0xA655 - rangeMin] = 0x5404;
            mapDataStd [rangeNo] [0xA656 - rangeMin] = 0x5411;
            mapDataStd [rangeNo] [0xA657 - rangeMin] = 0x540D;
            mapDataStd [rangeNo] [0xA658 - rangeMin] = 0x5408;
            mapDataStd [rangeNo] [0xA659 - rangeMin] = 0x5403;
            mapDataStd [rangeNo] [0xA65A - rangeMin] = 0x540E;
            mapDataStd [rangeNo] [0xA65B - rangeMin] = 0x5406;
            mapDataStd [rangeNo] [0xA65C - rangeMin] = 0x5412;
            mapDataStd [rangeNo] [0xA65D - rangeMin] = 0x56E0;
            mapDataStd [rangeNo] [0xA65E - rangeMin] = 0x56DE;
            mapDataStd [rangeNo] [0xA65F - rangeMin] = 0x56DD;

            mapDataStd [rangeNo] [0xA660 - rangeMin] = 0x5733;
            mapDataStd [rangeNo] [0xA661 - rangeMin] = 0x5730;
            mapDataStd [rangeNo] [0xA662 - rangeMin] = 0x5728;
            mapDataStd [rangeNo] [0xA663 - rangeMin] = 0x572D;
            mapDataStd [rangeNo] [0xA664 - rangeMin] = 0x572C;
            mapDataStd [rangeNo] [0xA665 - rangeMin] = 0x572F;
            mapDataStd [rangeNo] [0xA666 - rangeMin] = 0x5729;
            mapDataStd [rangeNo] [0xA667 - rangeMin] = 0x5919;
            mapDataStd [rangeNo] [0xA668 - rangeMin] = 0x591A;
            mapDataStd [rangeNo] [0xA669 - rangeMin] = 0x5937;
            mapDataStd [rangeNo] [0xA66A - rangeMin] = 0x5938;
            mapDataStd [rangeNo] [0xA66B - rangeMin] = 0x5984;
            mapDataStd [rangeNo] [0xA66C - rangeMin] = 0x5978;
            mapDataStd [rangeNo] [0xA66D - rangeMin] = 0x5983;
            mapDataStd [rangeNo] [0xA66E - rangeMin] = 0x597D;
            mapDataStd [rangeNo] [0xA66F - rangeMin] = 0x5979;

            mapDataStd [rangeNo] [0xA670 - rangeMin] = 0x5982;
            mapDataStd [rangeNo] [0xA671 - rangeMin] = 0x5981;
            mapDataStd [rangeNo] [0xA672 - rangeMin] = 0x5B57;
            mapDataStd [rangeNo] [0xA673 - rangeMin] = 0x5B58;
            mapDataStd [rangeNo] [0xA674 - rangeMin] = 0x5B87;
            mapDataStd [rangeNo] [0xA675 - rangeMin] = 0x5B88;
            mapDataStd [rangeNo] [0xA676 - rangeMin] = 0x5B85;
            mapDataStd [rangeNo] [0xA677 - rangeMin] = 0x5B89;
            mapDataStd [rangeNo] [0xA678 - rangeMin] = 0x5BFA;
            mapDataStd [rangeNo] [0xA679 - rangeMin] = 0x5C16;
            mapDataStd [rangeNo] [0xA67A - rangeMin] = 0x5C79;
            mapDataStd [rangeNo] [0xA67B - rangeMin] = 0x5DDE;
            mapDataStd [rangeNo] [0xA67C - rangeMin] = 0x5E06;
            mapDataStd [rangeNo] [0xA67D - rangeMin] = 0x5E76;
            mapDataStd [rangeNo] [0xA67E - rangeMin] = 0x5E74;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xA6A1 - rangeMin] = 0x5F0F;
            mapDataStd [rangeNo] [0xA6A2 - rangeMin] = 0x5F1B;
            mapDataStd [rangeNo] [0xA6A3 - rangeMin] = 0x5FD9;
            mapDataStd [rangeNo] [0xA6A4 - rangeMin] = 0x5FD6;
            mapDataStd [rangeNo] [0xA6A5 - rangeMin] = 0x620E;
            mapDataStd [rangeNo] [0xA6A6 - rangeMin] = 0x620C;
            mapDataStd [rangeNo] [0xA6A7 - rangeMin] = 0x620D;
            mapDataStd [rangeNo] [0xA6A8 - rangeMin] = 0x6210;
            mapDataStd [rangeNo] [0xA6A9 - rangeMin] = 0x6263;
            mapDataStd [rangeNo] [0xA6AA - rangeMin] = 0x625B;
            mapDataStd [rangeNo] [0xA6AB - rangeMin] = 0x6258;
            mapDataStd [rangeNo] [0xA6AC - rangeMin] = 0x6536;
            mapDataStd [rangeNo] [0xA6AD - rangeMin] = 0x65E9;
            mapDataStd [rangeNo] [0xA6AE - rangeMin] = 0x65E8;
            mapDataStd [rangeNo] [0xA6AF - rangeMin] = 0x65EC;

            mapDataStd [rangeNo] [0xA6B0 - rangeMin] = 0x65ED;
            mapDataStd [rangeNo] [0xA6B1 - rangeMin] = 0x66F2;
            mapDataStd [rangeNo] [0xA6B2 - rangeMin] = 0x66F3;
            mapDataStd [rangeNo] [0xA6B3 - rangeMin] = 0x6709;
            mapDataStd [rangeNo] [0xA6B4 - rangeMin] = 0x673D;
            mapDataStd [rangeNo] [0xA6B5 - rangeMin] = 0x6734;
            mapDataStd [rangeNo] [0xA6B6 - rangeMin] = 0x6731;
            mapDataStd [rangeNo] [0xA6B7 - rangeMin] = 0x6735;
            mapDataStd [rangeNo] [0xA6B8 - rangeMin] = 0x6B21;
            mapDataStd [rangeNo] [0xA6B9 - rangeMin] = 0x6B64;
            mapDataStd [rangeNo] [0xA6BA - rangeMin] = 0x6B7B;
            mapDataStd [rangeNo] [0xA6BB - rangeMin] = 0x6C16;
            mapDataStd [rangeNo] [0xA6BC - rangeMin] = 0x6C5D;
            mapDataStd [rangeNo] [0xA6BD - rangeMin] = 0x6C57;
            mapDataStd [rangeNo] [0xA6BE - rangeMin] = 0x6C59;
            mapDataStd [rangeNo] [0xA6BF - rangeMin] = 0x6C5F;

            mapDataStd [rangeNo] [0xA6C0 - rangeMin] = 0x6C60;
            mapDataStd [rangeNo] [0xA6C1 - rangeMin] = 0x6C50;
            mapDataStd [rangeNo] [0xA6C2 - rangeMin] = 0x6C55;
            mapDataStd [rangeNo] [0xA6C3 - rangeMin] = 0x6C61;
            mapDataStd [rangeNo] [0xA6C4 - rangeMin] = 0x6C5B;
            mapDataStd [rangeNo] [0xA6C5 - rangeMin] = 0x6C4D;
            mapDataStd [rangeNo] [0xA6C6 - rangeMin] = 0x6C4E;
            mapDataStd [rangeNo] [0xA6C7 - rangeMin] = 0x7070;
            mapDataStd [rangeNo] [0xA6C8 - rangeMin] = 0x725F;
            mapDataStd [rangeNo] [0xA6C9 - rangeMin] = 0x725D;
            mapDataStd [rangeNo] [0xA6CA - rangeMin] = 0x767E;
            mapDataStd [rangeNo] [0xA6CB - rangeMin] = 0x7AF9;
            mapDataStd [rangeNo] [0xA6CC - rangeMin] = 0x7C73;
            mapDataStd [rangeNo] [0xA6CD - rangeMin] = 0x7CF8;
            mapDataStd [rangeNo] [0xA6CE - rangeMin] = 0x7F36;
            mapDataStd [rangeNo] [0xA6CF - rangeMin] = 0x7F8A;

            mapDataStd [rangeNo] [0xA6D0 - rangeMin] = 0x7FBD;
            mapDataStd [rangeNo] [0xA6D1 - rangeMin] = 0x8001;
            mapDataStd [rangeNo] [0xA6D2 - rangeMin] = 0x8003;
            mapDataStd [rangeNo] [0xA6D3 - rangeMin] = 0x800C;
            mapDataStd [rangeNo] [0xA6D4 - rangeMin] = 0x8012;
            mapDataStd [rangeNo] [0xA6D5 - rangeMin] = 0x8033;
            mapDataStd [rangeNo] [0xA6D6 - rangeMin] = 0x807F;
            mapDataStd [rangeNo] [0xA6D7 - rangeMin] = 0x8089;
            mapDataStd [rangeNo] [0xA6D8 - rangeMin] = 0x808B;
            mapDataStd [rangeNo] [0xA6D9 - rangeMin] = 0x808C;
            mapDataStd [rangeNo] [0xA6DA - rangeMin] = 0x81E3;
            mapDataStd [rangeNo] [0xA6DB - rangeMin] = 0x81EA;
            mapDataStd [rangeNo] [0xA6DC - rangeMin] = 0x81F3;
            mapDataStd [rangeNo] [0xA6DD - rangeMin] = 0x81FC;
            mapDataStd [rangeNo] [0xA6DE - rangeMin] = 0x820C;
            mapDataStd [rangeNo] [0xA6DF - rangeMin] = 0x821B;

            mapDataStd [rangeNo] [0xA6E0 - rangeMin] = 0x821F;
            mapDataStd [rangeNo] [0xA6E1 - rangeMin] = 0x826E;
            mapDataStd [rangeNo] [0xA6E2 - rangeMin] = 0x8272;
            mapDataStd [rangeNo] [0xA6E3 - rangeMin] = 0x827E;
            mapDataStd [rangeNo] [0xA6E4 - rangeMin] = 0x866B;
            mapDataStd [rangeNo] [0xA6E5 - rangeMin] = 0x8840;
            mapDataStd [rangeNo] [0xA6E6 - rangeMin] = 0x884C;
            mapDataStd [rangeNo] [0xA6E7 - rangeMin] = 0x8863;
            mapDataStd [rangeNo] [0xA6E8 - rangeMin] = 0x897F;
            mapDataStd [rangeNo] [0xA6E9 - rangeMin] = 0x9621;
            mapDataStd [rangeNo] [0xA6EA - rangeMin] = 0x4E32;
            mapDataStd [rangeNo] [0xA6EB - rangeMin] = 0x4EA8;
            mapDataStd [rangeNo] [0xA6EC - rangeMin] = 0x4F4D;
            mapDataStd [rangeNo] [0xA6ED - rangeMin] = 0x4F4F;
            mapDataStd [rangeNo] [0xA6EE - rangeMin] = 0x4F47;
            mapDataStd [rangeNo] [0xA6EF - rangeMin] = 0x4F57;

            mapDataStd [rangeNo] [0xA6F0 - rangeMin] = 0x4F5E;
            mapDataStd [rangeNo] [0xA6F1 - rangeMin] = 0x4F34;
            mapDataStd [rangeNo] [0xA6F2 - rangeMin] = 0x4F5B;
            mapDataStd [rangeNo] [0xA6F3 - rangeMin] = 0x4F55;
            mapDataStd [rangeNo] [0xA6F4 - rangeMin] = 0x4F30;
            mapDataStd [rangeNo] [0xA6F5 - rangeMin] = 0x4F50;
            mapDataStd [rangeNo] [0xA6F6 - rangeMin] = 0x4F51;
            mapDataStd [rangeNo] [0xA6F7 - rangeMin] = 0x4F3D;
            mapDataStd [rangeNo] [0xA6F8 - rangeMin] = 0x4F3A;
            mapDataStd [rangeNo] [0xA6F9 - rangeMin] = 0x4F38;
            mapDataStd [rangeNo] [0xA6FA - rangeMin] = 0x4F43;
            mapDataStd [rangeNo] [0xA6FB - rangeMin] = 0x4F54;
            mapDataStd [rangeNo] [0xA6FC - rangeMin] = 0x4F3C;
            mapDataStd [rangeNo] [0xA6FD - rangeMin] = 0x4F46;
            mapDataStd [rangeNo] [0xA6FE - rangeMin] = 0x4F63;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xA740 - rangeMin] = 0x4F5C;
            mapDataStd [rangeNo] [0xA741 - rangeMin] = 0x4F60;
            mapDataStd [rangeNo] [0xA742 - rangeMin] = 0x4F2F;
            mapDataStd [rangeNo] [0xA743 - rangeMin] = 0x4F4E;
            mapDataStd [rangeNo] [0xA744 - rangeMin] = 0x4F36;
            mapDataStd [rangeNo] [0xA745 - rangeMin] = 0x4F59;
            mapDataStd [rangeNo] [0xA746 - rangeMin] = 0x4F5D;
            mapDataStd [rangeNo] [0xA747 - rangeMin] = 0x4F48;
            mapDataStd [rangeNo] [0xA748 - rangeMin] = 0x4F5A;
            mapDataStd [rangeNo] [0xA749 - rangeMin] = 0x514C;
            mapDataStd [rangeNo] [0xA74A - rangeMin] = 0x514B;
            mapDataStd [rangeNo] [0xA74B - rangeMin] = 0x514D;
            mapDataStd [rangeNo] [0xA74C - rangeMin] = 0x5175;
            mapDataStd [rangeNo] [0xA74D - rangeMin] = 0x51B6;
            mapDataStd [rangeNo] [0xA74E - rangeMin] = 0x51B7;
            mapDataStd [rangeNo] [0xA74F - rangeMin] = 0x5225;

            mapDataStd [rangeNo] [0xA750 - rangeMin] = 0x5224;
            mapDataStd [rangeNo] [0xA751 - rangeMin] = 0x5229;
            mapDataStd [rangeNo] [0xA752 - rangeMin] = 0x522A;
            mapDataStd [rangeNo] [0xA753 - rangeMin] = 0x5228;
            mapDataStd [rangeNo] [0xA754 - rangeMin] = 0x52AB;
            mapDataStd [rangeNo] [0xA755 - rangeMin] = 0x52A9;
            mapDataStd [rangeNo] [0xA756 - rangeMin] = 0x52AA;
            mapDataStd [rangeNo] [0xA757 - rangeMin] = 0x52AC;
            mapDataStd [rangeNo] [0xA758 - rangeMin] = 0x5323;
            mapDataStd [rangeNo] [0xA759 - rangeMin] = 0x5373;
            mapDataStd [rangeNo] [0xA75A - rangeMin] = 0x5375;
            mapDataStd [rangeNo] [0xA75B - rangeMin] = 0x541D;
            mapDataStd [rangeNo] [0xA75C - rangeMin] = 0x542D;
            mapDataStd [rangeNo] [0xA75D - rangeMin] = 0x541E;
            mapDataStd [rangeNo] [0xA75E - rangeMin] = 0x543E;
            mapDataStd [rangeNo] [0xA75F - rangeMin] = 0x5426;

            mapDataStd [rangeNo] [0xA760 - rangeMin] = 0x544E;
            mapDataStd [rangeNo] [0xA761 - rangeMin] = 0x5427;
            mapDataStd [rangeNo] [0xA762 - rangeMin] = 0x5446;
            mapDataStd [rangeNo] [0xA763 - rangeMin] = 0x5443;
            mapDataStd [rangeNo] [0xA764 - rangeMin] = 0x5433;
            mapDataStd [rangeNo] [0xA765 - rangeMin] = 0x5448;
            mapDataStd [rangeNo] [0xA766 - rangeMin] = 0x5442;
            mapDataStd [rangeNo] [0xA767 - rangeMin] = 0x541B;
            mapDataStd [rangeNo] [0xA768 - rangeMin] = 0x5429;
            mapDataStd [rangeNo] [0xA769 - rangeMin] = 0x544A;
            mapDataStd [rangeNo] [0xA76A - rangeMin] = 0x5439;
            mapDataStd [rangeNo] [0xA76B - rangeMin] = 0x543B;
            mapDataStd [rangeNo] [0xA76C - rangeMin] = 0x5438;
            mapDataStd [rangeNo] [0xA76D - rangeMin] = 0x542E;
            mapDataStd [rangeNo] [0xA76E - rangeMin] = 0x5435;
            mapDataStd [rangeNo] [0xA76F - rangeMin] = 0x5436;

            mapDataStd [rangeNo] [0xA770 - rangeMin] = 0x5420;
            mapDataStd [rangeNo] [0xA771 - rangeMin] = 0x543C;
            mapDataStd [rangeNo] [0xA772 - rangeMin] = 0x5440;
            mapDataStd [rangeNo] [0xA773 - rangeMin] = 0x5431;
            mapDataStd [rangeNo] [0xA774 - rangeMin] = 0x542B;
            mapDataStd [rangeNo] [0xA775 - rangeMin] = 0x541F;
            mapDataStd [rangeNo] [0xA776 - rangeMin] = 0x542C;
            mapDataStd [rangeNo] [0xA777 - rangeMin] = 0x56EA;
            mapDataStd [rangeNo] [0xA778 - rangeMin] = 0x56F0;
            mapDataStd [rangeNo] [0xA779 - rangeMin] = 0x56E4;
            mapDataStd [rangeNo] [0xA77A - rangeMin] = 0x56EB;
            mapDataStd [rangeNo] [0xA77B - rangeMin] = 0x574A;
            mapDataStd [rangeNo] [0xA77C - rangeMin] = 0x5751;
            mapDataStd [rangeNo] [0xA77D - rangeMin] = 0x5740;
            mapDataStd [rangeNo] [0xA77E - rangeMin] = 0x574D;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xA7A1 - rangeMin] = 0x5747;
            mapDataStd [rangeNo] [0xA7A2 - rangeMin] = 0x574E;
            mapDataStd [rangeNo] [0xA7A3 - rangeMin] = 0x573E;
            mapDataStd [rangeNo] [0xA7A4 - rangeMin] = 0x5750;
            mapDataStd [rangeNo] [0xA7A5 - rangeMin] = 0x574F;
            mapDataStd [rangeNo] [0xA7A6 - rangeMin] = 0x573B;
            mapDataStd [rangeNo] [0xA7A7 - rangeMin] = 0x58EF;
            mapDataStd [rangeNo] [0xA7A8 - rangeMin] = 0x593E;
            mapDataStd [rangeNo] [0xA7A9 - rangeMin] = 0x599D;
            mapDataStd [rangeNo] [0xA7AA - rangeMin] = 0x5992;
            mapDataStd [rangeNo] [0xA7AB - rangeMin] = 0x59A8;
            mapDataStd [rangeNo] [0xA7AC - rangeMin] = 0x599E;
            mapDataStd [rangeNo] [0xA7AD - rangeMin] = 0x59A3;
            mapDataStd [rangeNo] [0xA7AE - rangeMin] = 0x5999;
            mapDataStd [rangeNo] [0xA7AF - rangeMin] = 0x5996;

            mapDataStd [rangeNo] [0xA7B0 - rangeMin] = 0x598D;
            mapDataStd [rangeNo] [0xA7B1 - rangeMin] = 0x59A4;
            mapDataStd [rangeNo] [0xA7B2 - rangeMin] = 0x5993;
            mapDataStd [rangeNo] [0xA7B3 - rangeMin] = 0x598A;
            mapDataStd [rangeNo] [0xA7B4 - rangeMin] = 0x59A5;
            mapDataStd [rangeNo] [0xA7B5 - rangeMin] = 0x5B5D;
            mapDataStd [rangeNo] [0xA7B6 - rangeMin] = 0x5B5C;
            mapDataStd [rangeNo] [0xA7B7 - rangeMin] = 0x5B5A;
            mapDataStd [rangeNo] [0xA7B8 - rangeMin] = 0x5B5B;
            mapDataStd [rangeNo] [0xA7B9 - rangeMin] = 0x5B8C;
            mapDataStd [rangeNo] [0xA7BA - rangeMin] = 0x5B8B;
            mapDataStd [rangeNo] [0xA7BB - rangeMin] = 0x5B8F;
            mapDataStd [rangeNo] [0xA7BC - rangeMin] = 0x5C2C;
            mapDataStd [rangeNo] [0xA7BD - rangeMin] = 0x5C40;
            mapDataStd [rangeNo] [0xA7BE - rangeMin] = 0x5C41;
            mapDataStd [rangeNo] [0xA7BF - rangeMin] = 0x5C3F;

            mapDataStd [rangeNo] [0xA7C0 - rangeMin] = 0x5C3E;
            mapDataStd [rangeNo] [0xA7C1 - rangeMin] = 0x5C90;
            mapDataStd [rangeNo] [0xA7C2 - rangeMin] = 0x5C91;
            mapDataStd [rangeNo] [0xA7C3 - rangeMin] = 0x5C94;
            mapDataStd [rangeNo] [0xA7C4 - rangeMin] = 0x5C8C;
            mapDataStd [rangeNo] [0xA7C5 - rangeMin] = 0x5DEB;
            mapDataStd [rangeNo] [0xA7C6 - rangeMin] = 0x5E0C;
            mapDataStd [rangeNo] [0xA7C7 - rangeMin] = 0x5E8F;
            mapDataStd [rangeNo] [0xA7C8 - rangeMin] = 0x5E87;
            mapDataStd [rangeNo] [0xA7C9 - rangeMin] = 0x5E8A;
            mapDataStd [rangeNo] [0xA7CA - rangeMin] = 0x5EF7;
            mapDataStd [rangeNo] [0xA7CB - rangeMin] = 0x5F04;
            mapDataStd [rangeNo] [0xA7CC - rangeMin] = 0x5F1F;
            mapDataStd [rangeNo] [0xA7CD - rangeMin] = 0x5F64;
            mapDataStd [rangeNo] [0xA7CE - rangeMin] = 0x5F62;
            mapDataStd [rangeNo] [0xA7CF - rangeMin] = 0x5F77;

            mapDataStd [rangeNo] [0xA7D0 - rangeMin] = 0x5F79;
            mapDataStd [rangeNo] [0xA7D1 - rangeMin] = 0x5FD8;
            mapDataStd [rangeNo] [0xA7D2 - rangeMin] = 0x5FCC;
            mapDataStd [rangeNo] [0xA7D3 - rangeMin] = 0x5FD7;
            mapDataStd [rangeNo] [0xA7D4 - rangeMin] = 0x5FCD;
            mapDataStd [rangeNo] [0xA7D5 - rangeMin] = 0x5FF1;
            mapDataStd [rangeNo] [0xA7D6 - rangeMin] = 0x5FEB;
            mapDataStd [rangeNo] [0xA7D7 - rangeMin] = 0x5FF8;
            mapDataStd [rangeNo] [0xA7D8 - rangeMin] = 0x5FEA;
            mapDataStd [rangeNo] [0xA7D9 - rangeMin] = 0x6212;
            mapDataStd [rangeNo] [0xA7DA - rangeMin] = 0x6211;
            mapDataStd [rangeNo] [0xA7DB - rangeMin] = 0x6284;
            mapDataStd [rangeNo] [0xA7DC - rangeMin] = 0x6297;
            mapDataStd [rangeNo] [0xA7DD - rangeMin] = 0x6296;
            mapDataStd [rangeNo] [0xA7DE - rangeMin] = 0x6280;
            mapDataStd [rangeNo] [0xA7DF - rangeMin] = 0x6276;

            mapDataStd [rangeNo] [0xA7E0 - rangeMin] = 0x6289;
            mapDataStd [rangeNo] [0xA7E1 - rangeMin] = 0x626D;
            mapDataStd [rangeNo] [0xA7E2 - rangeMin] = 0x628A;
            mapDataStd [rangeNo] [0xA7E3 - rangeMin] = 0x627C;
            mapDataStd [rangeNo] [0xA7E4 - rangeMin] = 0x627E;
            mapDataStd [rangeNo] [0xA7E5 - rangeMin] = 0x6279;
            mapDataStd [rangeNo] [0xA7E6 - rangeMin] = 0x6273;
            mapDataStd [rangeNo] [0xA7E7 - rangeMin] = 0x6292;
            mapDataStd [rangeNo] [0xA7E8 - rangeMin] = 0x626F;
            mapDataStd [rangeNo] [0xA7E9 - rangeMin] = 0x6298;
            mapDataStd [rangeNo] [0xA7EA - rangeMin] = 0x626E;
            mapDataStd [rangeNo] [0xA7EB - rangeMin] = 0x6295;
            mapDataStd [rangeNo] [0xA7EC - rangeMin] = 0x6293;
            mapDataStd [rangeNo] [0xA7ED - rangeMin] = 0x6291;
            mapDataStd [rangeNo] [0xA7EE - rangeMin] = 0x6286;
            mapDataStd [rangeNo] [0xA7EF - rangeMin] = 0x6539;

            mapDataStd [rangeNo] [0xA7F0 - rangeMin] = 0x653B;
            mapDataStd [rangeNo] [0xA7F1 - rangeMin] = 0x6538;
            mapDataStd [rangeNo] [0xA7F2 - rangeMin] = 0x65F1;
            mapDataStd [rangeNo] [0xA7F3 - rangeMin] = 0x66F4;
            mapDataStd [rangeNo] [0xA7F4 - rangeMin] = 0x675F;
            mapDataStd [rangeNo] [0xA7F5 - rangeMin] = 0x674E;
            mapDataStd [rangeNo] [0xA7F6 - rangeMin] = 0x674F;
            mapDataStd [rangeNo] [0xA7F7 - rangeMin] = 0x6750;
            mapDataStd [rangeNo] [0xA7F8 - rangeMin] = 0x6751;
            mapDataStd [rangeNo] [0xA7F9 - rangeMin] = 0x675C;
            mapDataStd [rangeNo] [0xA7FA - rangeMin] = 0x6756;
            mapDataStd [rangeNo] [0xA7FB - rangeMin] = 0x675E;
            mapDataStd [rangeNo] [0xA7FC - rangeMin] = 0x6749;
            mapDataStd [rangeNo] [0xA7FD - rangeMin] = 0x6746;
            mapDataStd [rangeNo] [0xA7FE - rangeMin] = 0x6760;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xA840 - rangeMin] = 0x6753;
            mapDataStd [rangeNo] [0xA841 - rangeMin] = 0x6757;
            mapDataStd [rangeNo] [0xA842 - rangeMin] = 0x6B65;
            mapDataStd [rangeNo] [0xA843 - rangeMin] = 0x6BCF;
            mapDataStd [rangeNo] [0xA844 - rangeMin] = 0x6C42;
            mapDataStd [rangeNo] [0xA845 - rangeMin] = 0x6C5E;
            mapDataStd [rangeNo] [0xA846 - rangeMin] = 0x6C99;
            mapDataStd [rangeNo] [0xA847 - rangeMin] = 0x6C81;
            mapDataStd [rangeNo] [0xA848 - rangeMin] = 0x6C88;
            mapDataStd [rangeNo] [0xA849 - rangeMin] = 0x6C89;
            mapDataStd [rangeNo] [0xA84A - rangeMin] = 0x6C85;
            mapDataStd [rangeNo] [0xA84B - rangeMin] = 0x6C9B;
            mapDataStd [rangeNo] [0xA84C - rangeMin] = 0x6C6A;
            mapDataStd [rangeNo] [0xA84D - rangeMin] = 0x6C7A;
            mapDataStd [rangeNo] [0xA84E - rangeMin] = 0x6C90;
            mapDataStd [rangeNo] [0xA84F - rangeMin] = 0x6C70;

            mapDataStd [rangeNo] [0xA850 - rangeMin] = 0x6C8C;
            mapDataStd [rangeNo] [0xA851 - rangeMin] = 0x6C68;
            mapDataStd [rangeNo] [0xA852 - rangeMin] = 0x6C96;
            mapDataStd [rangeNo] [0xA853 - rangeMin] = 0x6C92;
            mapDataStd [rangeNo] [0xA854 - rangeMin] = 0x6C7D;
            mapDataStd [rangeNo] [0xA855 - rangeMin] = 0x6C83;
            mapDataStd [rangeNo] [0xA856 - rangeMin] = 0x6C72;
            mapDataStd [rangeNo] [0xA857 - rangeMin] = 0x6C7E;
            mapDataStd [rangeNo] [0xA858 - rangeMin] = 0x6C74;
            mapDataStd [rangeNo] [0xA859 - rangeMin] = 0x6C86;
            mapDataStd [rangeNo] [0xA85A - rangeMin] = 0x6C76;
            mapDataStd [rangeNo] [0xA85B - rangeMin] = 0x6C8D;
            mapDataStd [rangeNo] [0xA85C - rangeMin] = 0x6C94;
            mapDataStd [rangeNo] [0xA85D - rangeMin] = 0x6C98;
            mapDataStd [rangeNo] [0xA85E - rangeMin] = 0x6C82;
            mapDataStd [rangeNo] [0xA85F - rangeMin] = 0x7076;

            mapDataStd [rangeNo] [0xA860 - rangeMin] = 0x707C;
            mapDataStd [rangeNo] [0xA861 - rangeMin] = 0x707D;
            mapDataStd [rangeNo] [0xA862 - rangeMin] = 0x7078;
            mapDataStd [rangeNo] [0xA863 - rangeMin] = 0x7262;
            mapDataStd [rangeNo] [0xA864 - rangeMin] = 0x7261;
            mapDataStd [rangeNo] [0xA865 - rangeMin] = 0x7260;
            mapDataStd [rangeNo] [0xA866 - rangeMin] = 0x72C4;
            mapDataStd [rangeNo] [0xA867 - rangeMin] = 0x72C2;
            mapDataStd [rangeNo] [0xA868 - rangeMin] = 0x7396;
            mapDataStd [rangeNo] [0xA869 - rangeMin] = 0x752C;
            mapDataStd [rangeNo] [0xA86A - rangeMin] = 0x752B;
            mapDataStd [rangeNo] [0xA86B - rangeMin] = 0x7537;
            mapDataStd [rangeNo] [0xA86C - rangeMin] = 0x7538;
            mapDataStd [rangeNo] [0xA86D - rangeMin] = 0x7682;
            mapDataStd [rangeNo] [0xA86E - rangeMin] = 0x76EF;
            mapDataStd [rangeNo] [0xA86F - rangeMin] = 0x77E3;

            mapDataStd [rangeNo] [0xA870 - rangeMin] = 0x79C1;
            mapDataStd [rangeNo] [0xA871 - rangeMin] = 0x79C0;
            mapDataStd [rangeNo] [0xA872 - rangeMin] = 0x79BF;
            mapDataStd [rangeNo] [0xA873 - rangeMin] = 0x7A76;
            mapDataStd [rangeNo] [0xA874 - rangeMin] = 0x7CFB;
            mapDataStd [rangeNo] [0xA875 - rangeMin] = 0x7F55;
            mapDataStd [rangeNo] [0xA876 - rangeMin] = 0x8096;
            mapDataStd [rangeNo] [0xA877 - rangeMin] = 0x8093;
            mapDataStd [rangeNo] [0xA878 - rangeMin] = 0x809D;
            mapDataStd [rangeNo] [0xA879 - rangeMin] = 0x8098;
            mapDataStd [rangeNo] [0xA87A - rangeMin] = 0x809B;
            mapDataStd [rangeNo] [0xA87B - rangeMin] = 0x809A;
            mapDataStd [rangeNo] [0xA87C - rangeMin] = 0x80B2;
            mapDataStd [rangeNo] [0xA87D - rangeMin] = 0x826F;
            mapDataStd [rangeNo] [0xA87E - rangeMin] = 0x8292;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xA8A1 - rangeMin] = 0x828B;
            mapDataStd [rangeNo] [0xA8A2 - rangeMin] = 0x828D;
            mapDataStd [rangeNo] [0xA8A3 - rangeMin] = 0x898B;
            mapDataStd [rangeNo] [0xA8A4 - rangeMin] = 0x89D2;
            mapDataStd [rangeNo] [0xA8A5 - rangeMin] = 0x8A00;
            mapDataStd [rangeNo] [0xA8A6 - rangeMin] = 0x8C37;
            mapDataStd [rangeNo] [0xA8A7 - rangeMin] = 0x8C46;
            mapDataStd [rangeNo] [0xA8A8 - rangeMin] = 0x8C55;
            mapDataStd [rangeNo] [0xA8A9 - rangeMin] = 0x8C9D;
            mapDataStd [rangeNo] [0xA8AA - rangeMin] = 0x8D64;
            mapDataStd [rangeNo] [0xA8AB - rangeMin] = 0x8D70;
            mapDataStd [rangeNo] [0xA8AC - rangeMin] = 0x8DB3;
            mapDataStd [rangeNo] [0xA8AD - rangeMin] = 0x8EAB;
            mapDataStd [rangeNo] [0xA8AE - rangeMin] = 0x8ECA;
            mapDataStd [rangeNo] [0xA8AF - rangeMin] = 0x8F9B;

            mapDataStd [rangeNo] [0xA8B0 - rangeMin] = 0x8FB0;
            mapDataStd [rangeNo] [0xA8B1 - rangeMin] = 0x8FC2;
            mapDataStd [rangeNo] [0xA8B2 - rangeMin] = 0x8FC6;
            mapDataStd [rangeNo] [0xA8B3 - rangeMin] = 0x8FC5;
            mapDataStd [rangeNo] [0xA8B4 - rangeMin] = 0x8FC4;
            mapDataStd [rangeNo] [0xA8B5 - rangeMin] = 0x5DE1;
            mapDataStd [rangeNo] [0xA8B6 - rangeMin] = 0x9091;
            mapDataStd [rangeNo] [0xA8B7 - rangeMin] = 0x90A2;
            mapDataStd [rangeNo] [0xA8B8 - rangeMin] = 0x90AA;
            mapDataStd [rangeNo] [0xA8B9 - rangeMin] = 0x90A6;
            mapDataStd [rangeNo] [0xA8BA - rangeMin] = 0x90A3;
            mapDataStd [rangeNo] [0xA8BB - rangeMin] = 0x9149;
            mapDataStd [rangeNo] [0xA8BC - rangeMin] = 0x91C6;
            mapDataStd [rangeNo] [0xA8BD - rangeMin] = 0x91CC;
            mapDataStd [rangeNo] [0xA8BE - rangeMin] = 0x9632;
            mapDataStd [rangeNo] [0xA8BF - rangeMin] = 0x962E;

            mapDataStd [rangeNo] [0xA8C0 - rangeMin] = 0x9631;
            mapDataStd [rangeNo] [0xA8C1 - rangeMin] = 0x962A;
            mapDataStd [rangeNo] [0xA8C2 - rangeMin] = 0x962C;
            mapDataStd [rangeNo] [0xA8C3 - rangeMin] = 0x4E26;
            mapDataStd [rangeNo] [0xA8C4 - rangeMin] = 0x4E56;
            mapDataStd [rangeNo] [0xA8C5 - rangeMin] = 0x4E73;
            mapDataStd [rangeNo] [0xA8C6 - rangeMin] = 0x4E8B;
            mapDataStd [rangeNo] [0xA8C7 - rangeMin] = 0x4E9B;
            mapDataStd [rangeNo] [0xA8C8 - rangeMin] = 0x4E9E;
            mapDataStd [rangeNo] [0xA8C9 - rangeMin] = 0x4EAB;
            mapDataStd [rangeNo] [0xA8CA - rangeMin] = 0x4EAC;
            mapDataStd [rangeNo] [0xA8CB - rangeMin] = 0x4F6F;
            mapDataStd [rangeNo] [0xA8CC - rangeMin] = 0x4F9D;
            mapDataStd [rangeNo] [0xA8CD - rangeMin] = 0x4F8D;
            mapDataStd [rangeNo] [0xA8CE - rangeMin] = 0x4F73;
            mapDataStd [rangeNo] [0xA8CF - rangeMin] = 0x4F7F;

            mapDataStd [rangeNo] [0xA8D0 - rangeMin] = 0x4F6C;
            mapDataStd [rangeNo] [0xA8D1 - rangeMin] = 0x4F9B;
            mapDataStd [rangeNo] [0xA8D2 - rangeMin] = 0x4F8B;
            mapDataStd [rangeNo] [0xA8D3 - rangeMin] = 0x4F86;
            mapDataStd [rangeNo] [0xA8D4 - rangeMin] = 0x4F83;
            mapDataStd [rangeNo] [0xA8D5 - rangeMin] = 0x4F70;
            mapDataStd [rangeNo] [0xA8D6 - rangeMin] = 0x4F75;
            mapDataStd [rangeNo] [0xA8D7 - rangeMin] = 0x4F88;
            mapDataStd [rangeNo] [0xA8D8 - rangeMin] = 0x4F69;
            mapDataStd [rangeNo] [0xA8D9 - rangeMin] = 0x4F7B;
            mapDataStd [rangeNo] [0xA8DA - rangeMin] = 0x4F96;
            mapDataStd [rangeNo] [0xA8DB - rangeMin] = 0x4F7E;
            mapDataStd [rangeNo] [0xA8DC - rangeMin] = 0x4F8F;
            mapDataStd [rangeNo] [0xA8DD - rangeMin] = 0x4F91;
            mapDataStd [rangeNo] [0xA8DE - rangeMin] = 0x4F7A;
            mapDataStd [rangeNo] [0xA8DF - rangeMin] = 0x5154;

            mapDataStd [rangeNo] [0xA8E0 - rangeMin] = 0x5152;
            mapDataStd [rangeNo] [0xA8E1 - rangeMin] = 0x5155;
            mapDataStd [rangeNo] [0xA8E2 - rangeMin] = 0x5169;
            mapDataStd [rangeNo] [0xA8E3 - rangeMin] = 0x5177;
            mapDataStd [rangeNo] [0xA8E4 - rangeMin] = 0x5176;
            mapDataStd [rangeNo] [0xA8E5 - rangeMin] = 0x5178;
            mapDataStd [rangeNo] [0xA8E6 - rangeMin] = 0x51BD;
            mapDataStd [rangeNo] [0xA8E7 - rangeMin] = 0x51FD;
            mapDataStd [rangeNo] [0xA8E8 - rangeMin] = 0x523B;
            mapDataStd [rangeNo] [0xA8E9 - rangeMin] = 0x5238;
            mapDataStd [rangeNo] [0xA8EA - rangeMin] = 0x5237;
            mapDataStd [rangeNo] [0xA8EB - rangeMin] = 0x523A;
            mapDataStd [rangeNo] [0xA8EC - rangeMin] = 0x5230;
            mapDataStd [rangeNo] [0xA8ED - rangeMin] = 0x522E;
            mapDataStd [rangeNo] [0xA8EE - rangeMin] = 0x5236;
            mapDataStd [rangeNo] [0xA8EF - rangeMin] = 0x5241;

            mapDataStd [rangeNo] [0xA8F0 - rangeMin] = 0x52BE;
            mapDataStd [rangeNo] [0xA8F1 - rangeMin] = 0x52BB;
            mapDataStd [rangeNo] [0xA8F2 - rangeMin] = 0x5352;
            mapDataStd [rangeNo] [0xA8F3 - rangeMin] = 0x5354;
            mapDataStd [rangeNo] [0xA8F4 - rangeMin] = 0x5353;
            mapDataStd [rangeNo] [0xA8F5 - rangeMin] = 0x5351;
            mapDataStd [rangeNo] [0xA8F6 - rangeMin] = 0x5366;
            mapDataStd [rangeNo] [0xA8F7 - rangeMin] = 0x5377;
            mapDataStd [rangeNo] [0xA8F8 - rangeMin] = 0x5378;
            mapDataStd [rangeNo] [0xA8F9 - rangeMin] = 0x5379;
            mapDataStd [rangeNo] [0xA8FA - rangeMin] = 0x53D6;
            mapDataStd [rangeNo] [0xA8FB - rangeMin] = 0x53D4;
            mapDataStd [rangeNo] [0xA8FC - rangeMin] = 0x53D7;
            mapDataStd [rangeNo] [0xA8FD - rangeMin] = 0x5473;
            mapDataStd [rangeNo] [0xA8FE - rangeMin] = 0x5475;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xA940 - rangeMin] = 0x5496;
            mapDataStd [rangeNo] [0xA941 - rangeMin] = 0x5478;
            mapDataStd [rangeNo] [0xA942 - rangeMin] = 0x5495;
            mapDataStd [rangeNo] [0xA943 - rangeMin] = 0x5480;
            mapDataStd [rangeNo] [0xA944 - rangeMin] = 0x547B;
            mapDataStd [rangeNo] [0xA945 - rangeMin] = 0x5477;
            mapDataStd [rangeNo] [0xA946 - rangeMin] = 0x5484;
            mapDataStd [rangeNo] [0xA947 - rangeMin] = 0x5492;
            mapDataStd [rangeNo] [0xA948 - rangeMin] = 0x5486;
            mapDataStd [rangeNo] [0xA949 - rangeMin] = 0x547C;
            mapDataStd [rangeNo] [0xA94A - rangeMin] = 0x5490;
            mapDataStd [rangeNo] [0xA94B - rangeMin] = 0x5471;
            mapDataStd [rangeNo] [0xA94C - rangeMin] = 0x5476;
            mapDataStd [rangeNo] [0xA94D - rangeMin] = 0x548C;
            mapDataStd [rangeNo] [0xA94E - rangeMin] = 0x549A;
            mapDataStd [rangeNo] [0xA94F - rangeMin] = 0x5462;

            mapDataStd [rangeNo] [0xA950 - rangeMin] = 0x5468;
            mapDataStd [rangeNo] [0xA951 - rangeMin] = 0x548B;
            mapDataStd [rangeNo] [0xA952 - rangeMin] = 0x547D;
            mapDataStd [rangeNo] [0xA953 - rangeMin] = 0x548E;
            mapDataStd [rangeNo] [0xA954 - rangeMin] = 0x56FA;
            mapDataStd [rangeNo] [0xA955 - rangeMin] = 0x5783;
            mapDataStd [rangeNo] [0xA956 - rangeMin] = 0x5777;
            mapDataStd [rangeNo] [0xA957 - rangeMin] = 0x576A;
            mapDataStd [rangeNo] [0xA958 - rangeMin] = 0x5769;
            mapDataStd [rangeNo] [0xA959 - rangeMin] = 0x5761;
            mapDataStd [rangeNo] [0xA95A - rangeMin] = 0x5766;
            mapDataStd [rangeNo] [0xA95B - rangeMin] = 0x5764;
            mapDataStd [rangeNo] [0xA95C - rangeMin] = 0x577C;
            mapDataStd [rangeNo] [0xA95D - rangeMin] = 0x591C;
            mapDataStd [rangeNo] [0xA95E - rangeMin] = 0x5949;
            mapDataStd [rangeNo] [0xA95F - rangeMin] = 0x5947;

            mapDataStd [rangeNo] [0xA960 - rangeMin] = 0x5948;
            mapDataStd [rangeNo] [0xA961 - rangeMin] = 0x5944;
            mapDataStd [rangeNo] [0xA962 - rangeMin] = 0x5954;
            mapDataStd [rangeNo] [0xA963 - rangeMin] = 0x59BE;
            mapDataStd [rangeNo] [0xA964 - rangeMin] = 0x59BB;
            mapDataStd [rangeNo] [0xA965 - rangeMin] = 0x59D4;
            mapDataStd [rangeNo] [0xA966 - rangeMin] = 0x59B9;
            mapDataStd [rangeNo] [0xA967 - rangeMin] = 0x59AE;
            mapDataStd [rangeNo] [0xA968 - rangeMin] = 0x59D1;
            mapDataStd [rangeNo] [0xA969 - rangeMin] = 0x59C6;
            mapDataStd [rangeNo] [0xA96A - rangeMin] = 0x59D0;
            mapDataStd [rangeNo] [0xA96B - rangeMin] = 0x59CD;
            mapDataStd [rangeNo] [0xA96C - rangeMin] = 0x59CB;
            mapDataStd [rangeNo] [0xA96D - rangeMin] = 0x59D3;
            mapDataStd [rangeNo] [0xA96E - rangeMin] = 0x59CA;
            mapDataStd [rangeNo] [0xA96F - rangeMin] = 0x59AF;

            mapDataStd [rangeNo] [0xA970 - rangeMin] = 0x59B3;
            mapDataStd [rangeNo] [0xA971 - rangeMin] = 0x59D2;
            mapDataStd [rangeNo] [0xA972 - rangeMin] = 0x59C5;
            mapDataStd [rangeNo] [0xA973 - rangeMin] = 0x5B5F;
            mapDataStd [rangeNo] [0xA974 - rangeMin] = 0x5B64;
            mapDataStd [rangeNo] [0xA975 - rangeMin] = 0x5B63;
            mapDataStd [rangeNo] [0xA976 - rangeMin] = 0x5B97;
            mapDataStd [rangeNo] [0xA977 - rangeMin] = 0x5B9A;
            mapDataStd [rangeNo] [0xA978 - rangeMin] = 0x5B98;
            mapDataStd [rangeNo] [0xA979 - rangeMin] = 0x5B9C;
            mapDataStd [rangeNo] [0xA97A - rangeMin] = 0x5B99;
            mapDataStd [rangeNo] [0xA97B - rangeMin] = 0x5B9B;
            mapDataStd [rangeNo] [0xA97C - rangeMin] = 0x5C1A;
            mapDataStd [rangeNo] [0xA97D - rangeMin] = 0x5C48;
            mapDataStd [rangeNo] [0xA97E - rangeMin] = 0x5C45;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xA9A1 - rangeMin] = 0x5C46;
            mapDataStd [rangeNo] [0xA9A2 - rangeMin] = 0x5CB7;
            mapDataStd [rangeNo] [0xA9A3 - rangeMin] = 0x5CA1;
            mapDataStd [rangeNo] [0xA9A4 - rangeMin] = 0x5CB8;
            mapDataStd [rangeNo] [0xA9A5 - rangeMin] = 0x5CA9;
            mapDataStd [rangeNo] [0xA9A6 - rangeMin] = 0x5CAB;
            mapDataStd [rangeNo] [0xA9A7 - rangeMin] = 0x5CB1;
            mapDataStd [rangeNo] [0xA9A8 - rangeMin] = 0x5CB3;
            mapDataStd [rangeNo] [0xA9A9 - rangeMin] = 0x5E18;
            mapDataStd [rangeNo] [0xA9AA - rangeMin] = 0x5E1A;
            mapDataStd [rangeNo] [0xA9AB - rangeMin] = 0x5E16;
            mapDataStd [rangeNo] [0xA9AC - rangeMin] = 0x5E15;
            mapDataStd [rangeNo] [0xA9AD - rangeMin] = 0x5E1B;
            mapDataStd [rangeNo] [0xA9AE - rangeMin] = 0x5E11;
            mapDataStd [rangeNo] [0xA9AF - rangeMin] = 0x5E78;

            mapDataStd [rangeNo] [0xA9B0 - rangeMin] = 0x5E9A;
            mapDataStd [rangeNo] [0xA9B1 - rangeMin] = 0x5E97;
            mapDataStd [rangeNo] [0xA9B2 - rangeMin] = 0x5E9C;
            mapDataStd [rangeNo] [0xA9B3 - rangeMin] = 0x5E95;
            mapDataStd [rangeNo] [0xA9B4 - rangeMin] = 0x5E96;
            mapDataStd [rangeNo] [0xA9B5 - rangeMin] = 0x5EF6;
            mapDataStd [rangeNo] [0xA9B6 - rangeMin] = 0x5F26;
            mapDataStd [rangeNo] [0xA9B7 - rangeMin] = 0x5F27;
            mapDataStd [rangeNo] [0xA9B8 - rangeMin] = 0x5F29;
            mapDataStd [rangeNo] [0xA9B9 - rangeMin] = 0x5F80;
            mapDataStd [rangeNo] [0xA9BA - rangeMin] = 0x5F81;
            mapDataStd [rangeNo] [0xA9BB - rangeMin] = 0x5F7F;
            mapDataStd [rangeNo] [0xA9BC - rangeMin] = 0x5F7C;
            mapDataStd [rangeNo] [0xA9BD - rangeMin] = 0x5FDD;
            mapDataStd [rangeNo] [0xA9BE - rangeMin] = 0x5FE0;
            mapDataStd [rangeNo] [0xA9BF - rangeMin] = 0x5FFD;

            mapDataStd [rangeNo] [0xA9C0 - rangeMin] = 0x5FF5;
            mapDataStd [rangeNo] [0xA9C1 - rangeMin] = 0x5FFF;
            mapDataStd [rangeNo] [0xA9C2 - rangeMin] = 0x600F;
            mapDataStd [rangeNo] [0xA9C3 - rangeMin] = 0x6014;
            mapDataStd [rangeNo] [0xA9C4 - rangeMin] = 0x602F;
            mapDataStd [rangeNo] [0xA9C5 - rangeMin] = 0x6035;
            mapDataStd [rangeNo] [0xA9C6 - rangeMin] = 0x6016;
            mapDataStd [rangeNo] [0xA9C7 - rangeMin] = 0x602A;
            mapDataStd [rangeNo] [0xA9C8 - rangeMin] = 0x6015;
            mapDataStd [rangeNo] [0xA9C9 - rangeMin] = 0x6021;
            mapDataStd [rangeNo] [0xA9CA - rangeMin] = 0x6027;
            mapDataStd [rangeNo] [0xA9CB - rangeMin] = 0x6029;
            mapDataStd [rangeNo] [0xA9CC - rangeMin] = 0x602B;
            mapDataStd [rangeNo] [0xA9CD - rangeMin] = 0x601B;
            mapDataStd [rangeNo] [0xA9CE - rangeMin] = 0x6216;
            mapDataStd [rangeNo] [0xA9CF - rangeMin] = 0x6215;

            mapDataStd [rangeNo] [0xA9D0 - rangeMin] = 0x623F;
            mapDataStd [rangeNo] [0xA9D1 - rangeMin] = 0x623E;
            mapDataStd [rangeNo] [0xA9D2 - rangeMin] = 0x6240;
            mapDataStd [rangeNo] [0xA9D3 - rangeMin] = 0x627F;
            mapDataStd [rangeNo] [0xA9D4 - rangeMin] = 0x62C9;
            mapDataStd [rangeNo] [0xA9D5 - rangeMin] = 0x62CC;
            mapDataStd [rangeNo] [0xA9D6 - rangeMin] = 0x62C4;
            mapDataStd [rangeNo] [0xA9D7 - rangeMin] = 0x62BF;
            mapDataStd [rangeNo] [0xA9D8 - rangeMin] = 0x62C2;
            mapDataStd [rangeNo] [0xA9D9 - rangeMin] = 0x62B9;
            mapDataStd [rangeNo] [0xA9DA - rangeMin] = 0x62D2;
            mapDataStd [rangeNo] [0xA9DB - rangeMin] = 0x62DB;
            mapDataStd [rangeNo] [0xA9DC - rangeMin] = 0x62AB;
            mapDataStd [rangeNo] [0xA9DD - rangeMin] = 0x62D3;
            mapDataStd [rangeNo] [0xA9DE - rangeMin] = 0x62D4;
            mapDataStd [rangeNo] [0xA9DF - rangeMin] = 0x62CB;

            mapDataStd [rangeNo] [0xA9E0 - rangeMin] = 0x62C8;
            mapDataStd [rangeNo] [0xA9E1 - rangeMin] = 0x62A8;
            mapDataStd [rangeNo] [0xA9E2 - rangeMin] = 0x62BD;
            mapDataStd [rangeNo] [0xA9E3 - rangeMin] = 0x62BC;
            mapDataStd [rangeNo] [0xA9E4 - rangeMin] = 0x62D0;
            mapDataStd [rangeNo] [0xA9E5 - rangeMin] = 0x62D9;
            mapDataStd [rangeNo] [0xA9E6 - rangeMin] = 0x62C7;
            mapDataStd [rangeNo] [0xA9E7 - rangeMin] = 0x62CD;
            mapDataStd [rangeNo] [0xA9E8 - rangeMin] = 0x62B5;
            mapDataStd [rangeNo] [0xA9E9 - rangeMin] = 0x62DA;
            mapDataStd [rangeNo] [0xA9EA - rangeMin] = 0x62B1;
            mapDataStd [rangeNo] [0xA9EB - rangeMin] = 0x62D8;
            mapDataStd [rangeNo] [0xA9EC - rangeMin] = 0x62D6;
            mapDataStd [rangeNo] [0xA9ED - rangeMin] = 0x62D7;
            mapDataStd [rangeNo] [0xA9EE - rangeMin] = 0x62C6;
            mapDataStd [rangeNo] [0xA9EF - rangeMin] = 0x62AC;

            mapDataStd [rangeNo] [0xA9F0 - rangeMin] = 0x62CE;
            mapDataStd [rangeNo] [0xA9F1 - rangeMin] = 0x653E;
            mapDataStd [rangeNo] [0xA9F2 - rangeMin] = 0x65A7;
            mapDataStd [rangeNo] [0xA9F3 - rangeMin] = 0x65BC;
            mapDataStd [rangeNo] [0xA9F4 - rangeMin] = 0x65FA;
            mapDataStd [rangeNo] [0xA9F5 - rangeMin] = 0x6614;
            mapDataStd [rangeNo] [0xA9F6 - rangeMin] = 0x6613;
            mapDataStd [rangeNo] [0xA9F7 - rangeMin] = 0x660C;
            mapDataStd [rangeNo] [0xA9F8 - rangeMin] = 0x6606;
            mapDataStd [rangeNo] [0xA9F9 - rangeMin] = 0x6602;
            mapDataStd [rangeNo] [0xA9FA - rangeMin] = 0x660E;
            mapDataStd [rangeNo] [0xA9FB - rangeMin] = 0x6600;
            mapDataStd [rangeNo] [0xA9FC - rangeMin] = 0x660F;
            mapDataStd [rangeNo] [0xA9FD - rangeMin] = 0x6615;
            mapDataStd [rangeNo] [0xA9FE - rangeMin] = 0x660A;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xAA40 - rangeMin] = 0x6607;
            mapDataStd [rangeNo] [0xAA41 - rangeMin] = 0x670D;
            mapDataStd [rangeNo] [0xAA42 - rangeMin] = 0x670B;
            mapDataStd [rangeNo] [0xAA43 - rangeMin] = 0x676D;
            mapDataStd [rangeNo] [0xAA44 - rangeMin] = 0x678B;
            mapDataStd [rangeNo] [0xAA45 - rangeMin] = 0x6795;
            mapDataStd [rangeNo] [0xAA46 - rangeMin] = 0x6771;
            mapDataStd [rangeNo] [0xAA47 - rangeMin] = 0x679C;
            mapDataStd [rangeNo] [0xAA48 - rangeMin] = 0x6773;
            mapDataStd [rangeNo] [0xAA49 - rangeMin] = 0x6777;
            mapDataStd [rangeNo] [0xAA4A - rangeMin] = 0x6787;
            mapDataStd [rangeNo] [0xAA4B - rangeMin] = 0x679D;
            mapDataStd [rangeNo] [0xAA4C - rangeMin] = 0x6797;
            mapDataStd [rangeNo] [0xAA4D - rangeMin] = 0x676F;
            mapDataStd [rangeNo] [0xAA4E - rangeMin] = 0x6770;
            mapDataStd [rangeNo] [0xAA4F - rangeMin] = 0x677F;

            mapDataStd [rangeNo] [0xAA50 - rangeMin] = 0x6789;
            mapDataStd [rangeNo] [0xAA51 - rangeMin] = 0x677E;
            mapDataStd [rangeNo] [0xAA52 - rangeMin] = 0x6790;
            mapDataStd [rangeNo] [0xAA53 - rangeMin] = 0x6775;
            mapDataStd [rangeNo] [0xAA54 - rangeMin] = 0x679A;
            mapDataStd [rangeNo] [0xAA55 - rangeMin] = 0x6793;
            mapDataStd [rangeNo] [0xAA56 - rangeMin] = 0x677C;
            mapDataStd [rangeNo] [0xAA57 - rangeMin] = 0x676A;
            mapDataStd [rangeNo] [0xAA58 - rangeMin] = 0x6772;
            mapDataStd [rangeNo] [0xAA59 - rangeMin] = 0x6B23;
            mapDataStd [rangeNo] [0xAA5A - rangeMin] = 0x6B66;
            mapDataStd [rangeNo] [0xAA5B - rangeMin] = 0x6B67;
            mapDataStd [rangeNo] [0xAA5C - rangeMin] = 0x6B7F;
            mapDataStd [rangeNo] [0xAA5D - rangeMin] = 0x6C13;
            mapDataStd [rangeNo] [0xAA5E - rangeMin] = 0x6C1B;
            mapDataStd [rangeNo] [0xAA5F - rangeMin] = 0x6CE3;

            mapDataStd [rangeNo] [0xAA60 - rangeMin] = 0x6CE8;
            mapDataStd [rangeNo] [0xAA61 - rangeMin] = 0x6CF3;
            mapDataStd [rangeNo] [0xAA62 - rangeMin] = 0x6CB1;
            mapDataStd [rangeNo] [0xAA63 - rangeMin] = 0x6CCC;
            mapDataStd [rangeNo] [0xAA64 - rangeMin] = 0x6CE5;
            mapDataStd [rangeNo] [0xAA65 - rangeMin] = 0x6CB3;
            mapDataStd [rangeNo] [0xAA66 - rangeMin] = 0x6CBD;
            mapDataStd [rangeNo] [0xAA67 - rangeMin] = 0x6CBE;
            mapDataStd [rangeNo] [0xAA68 - rangeMin] = 0x6CBC;
            mapDataStd [rangeNo] [0xAA69 - rangeMin] = 0x6CE2;
            mapDataStd [rangeNo] [0xAA6A - rangeMin] = 0x6CAB;
            mapDataStd [rangeNo] [0xAA6B - rangeMin] = 0x6CD5;
            mapDataStd [rangeNo] [0xAA6C - rangeMin] = 0x6CD3;
            mapDataStd [rangeNo] [0xAA6D - rangeMin] = 0x6CB8;
            mapDataStd [rangeNo] [0xAA6E - rangeMin] = 0x6CC4;
            mapDataStd [rangeNo] [0xAA6F - rangeMin] = 0x6CB9;

            mapDataStd [rangeNo] [0xAA70 - rangeMin] = 0x6CC1;
            mapDataStd [rangeNo] [0xAA71 - rangeMin] = 0x6CAE;
            mapDataStd [rangeNo] [0xAA72 - rangeMin] = 0x6CD7;
            mapDataStd [rangeNo] [0xAA73 - rangeMin] = 0x6CC5;
            mapDataStd [rangeNo] [0xAA74 - rangeMin] = 0x6CF1;
            mapDataStd [rangeNo] [0xAA75 - rangeMin] = 0x6CBF;
            mapDataStd [rangeNo] [0xAA76 - rangeMin] = 0x6CBB;
            mapDataStd [rangeNo] [0xAA77 - rangeMin] = 0x6CE1;
            mapDataStd [rangeNo] [0xAA78 - rangeMin] = 0x6CDB;
            mapDataStd [rangeNo] [0xAA79 - rangeMin] = 0x6CCA;
            mapDataStd [rangeNo] [0xAA7A - rangeMin] = 0x6CAC;
            mapDataStd [rangeNo] [0xAA7B - rangeMin] = 0x6CEF;
            mapDataStd [rangeNo] [0xAA7C - rangeMin] = 0x6CDC;
            mapDataStd [rangeNo] [0xAA7D - rangeMin] = 0x6CD6;
            mapDataStd [rangeNo] [0xAA7E - rangeMin] = 0x6CE0;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xAAA1 - rangeMin] = 0x7095;
            mapDataStd [rangeNo] [0xAAA2 - rangeMin] = 0x708E;
            mapDataStd [rangeNo] [0xAAA3 - rangeMin] = 0x7092;
            mapDataStd [rangeNo] [0xAAA4 - rangeMin] = 0x708A;
            mapDataStd [rangeNo] [0xAAA5 - rangeMin] = 0x7099;
            mapDataStd [rangeNo] [0xAAA6 - rangeMin] = 0x722C;
            mapDataStd [rangeNo] [0xAAA7 - rangeMin] = 0x722D;
            mapDataStd [rangeNo] [0xAAA8 - rangeMin] = 0x7238;
            mapDataStd [rangeNo] [0xAAA9 - rangeMin] = 0x7248;
            mapDataStd [rangeNo] [0xAAAA - rangeMin] = 0x7267;
            mapDataStd [rangeNo] [0xAAAB - rangeMin] = 0x7269;
            mapDataStd [rangeNo] [0xAAAC - rangeMin] = 0x72C0;
            mapDataStd [rangeNo] [0xAAAD - rangeMin] = 0x72CE;
            mapDataStd [rangeNo] [0xAAAE - rangeMin] = 0x72D9;
            mapDataStd [rangeNo] [0xAAAF - rangeMin] = 0x72D7;

            mapDataStd [rangeNo] [0xAAB0 - rangeMin] = 0x72D0;
            mapDataStd [rangeNo] [0xAAB1 - rangeMin] = 0x73A9;
            mapDataStd [rangeNo] [0xAAB2 - rangeMin] = 0x73A8;
            mapDataStd [rangeNo] [0xAAB3 - rangeMin] = 0x739F;
            mapDataStd [rangeNo] [0xAAB4 - rangeMin] = 0x73AB;
            mapDataStd [rangeNo] [0xAAB5 - rangeMin] = 0x73A5;
            mapDataStd [rangeNo] [0xAAB6 - rangeMin] = 0x753D;
            mapDataStd [rangeNo] [0xAAB7 - rangeMin] = 0x759D;
            mapDataStd [rangeNo] [0xAAB8 - rangeMin] = 0x7599;
            mapDataStd [rangeNo] [0xAAB9 - rangeMin] = 0x759A;
            mapDataStd [rangeNo] [0xAABA - rangeMin] = 0x7684;
            mapDataStd [rangeNo] [0xAABB - rangeMin] = 0x76C2;
            mapDataStd [rangeNo] [0xAABC - rangeMin] = 0x76F2;
            mapDataStd [rangeNo] [0xAABD - rangeMin] = 0x76F4;
            mapDataStd [rangeNo] [0xAABE - rangeMin] = 0x77E5;
            mapDataStd [rangeNo] [0xAABF - rangeMin] = 0x77FD;

            mapDataStd [rangeNo] [0xAAC0 - rangeMin] = 0x793E;
            mapDataStd [rangeNo] [0xAAC1 - rangeMin] = 0x7940;
            mapDataStd [rangeNo] [0xAAC2 - rangeMin] = 0x7941;
            mapDataStd [rangeNo] [0xAAC3 - rangeMin] = 0x79C9;
            mapDataStd [rangeNo] [0xAAC4 - rangeMin] = 0x79C8;
            mapDataStd [rangeNo] [0xAAC5 - rangeMin] = 0x7A7A;
            mapDataStd [rangeNo] [0xAAC6 - rangeMin] = 0x7A79;
            mapDataStd [rangeNo] [0xAAC7 - rangeMin] = 0x7AFA;
            mapDataStd [rangeNo] [0xAAC8 - rangeMin] = 0x7CFE;
            mapDataStd [rangeNo] [0xAAC9 - rangeMin] = 0x7F54;
            mapDataStd [rangeNo] [0xAACA - rangeMin] = 0x7F8C;
            mapDataStd [rangeNo] [0xAACB - rangeMin] = 0x7F8B;
            mapDataStd [rangeNo] [0xAACC - rangeMin] = 0x8005;
            mapDataStd [rangeNo] [0xAACD - rangeMin] = 0x80BA;
            mapDataStd [rangeNo] [0xAACE - rangeMin] = 0x80A5;
            mapDataStd [rangeNo] [0xAACF - rangeMin] = 0x80A2;

            mapDataStd [rangeNo] [0xAAD0 - rangeMin] = 0x80B1;
            mapDataStd [rangeNo] [0xAAD1 - rangeMin] = 0x80A1;
            mapDataStd [rangeNo] [0xAAD2 - rangeMin] = 0x80AB;
            mapDataStd [rangeNo] [0xAAD3 - rangeMin] = 0x80A9;
            mapDataStd [rangeNo] [0xAAD4 - rangeMin] = 0x80B4;
            mapDataStd [rangeNo] [0xAAD5 - rangeMin] = 0x80AA;
            mapDataStd [rangeNo] [0xAAD6 - rangeMin] = 0x80AF;
            mapDataStd [rangeNo] [0xAAD7 - rangeMin] = 0x81E5;
            mapDataStd [rangeNo] [0xAAD8 - rangeMin] = 0x81FE;
            mapDataStd [rangeNo] [0xAAD9 - rangeMin] = 0x820D;
            mapDataStd [rangeNo] [0xAADA - rangeMin] = 0x82B3;
            mapDataStd [rangeNo] [0xAADB - rangeMin] = 0x829D;
            mapDataStd [rangeNo] [0xAADC - rangeMin] = 0x8299;
            mapDataStd [rangeNo] [0xAADD - rangeMin] = 0x82AD;
            mapDataStd [rangeNo] [0xAADE - rangeMin] = 0x82BD;
            mapDataStd [rangeNo] [0xAADF - rangeMin] = 0x829F;

            mapDataStd [rangeNo] [0xAAE0 - rangeMin] = 0x82B9;
            mapDataStd [rangeNo] [0xAAE1 - rangeMin] = 0x82B1;
            mapDataStd [rangeNo] [0xAAE2 - rangeMin] = 0x82AC;
            mapDataStd [rangeNo] [0xAAE3 - rangeMin] = 0x82A5;
            mapDataStd [rangeNo] [0xAAE4 - rangeMin] = 0x82AF;
            mapDataStd [rangeNo] [0xAAE5 - rangeMin] = 0x82B8;
            mapDataStd [rangeNo] [0xAAE6 - rangeMin] = 0x82A3;
            mapDataStd [rangeNo] [0xAAE7 - rangeMin] = 0x82B0;
            mapDataStd [rangeNo] [0xAAE8 - rangeMin] = 0x82BE;
            mapDataStd [rangeNo] [0xAAE9 - rangeMin] = 0x82B7;
            mapDataStd [rangeNo] [0xAAEA - rangeMin] = 0x864E;
            mapDataStd [rangeNo] [0xAAEB - rangeMin] = 0x8671;
            mapDataStd [rangeNo] [0xAAEC - rangeMin] = 0x521D;
            mapDataStd [rangeNo] [0xAAED - rangeMin] = 0x8868;
            mapDataStd [rangeNo] [0xAAEE - rangeMin] = 0x8ECB;
            mapDataStd [rangeNo] [0xAAEF - rangeMin] = 0x8FCE;

            mapDataStd [rangeNo] [0xAAF0 - rangeMin] = 0x8FD4;
            mapDataStd [rangeNo] [0xAAF1 - rangeMin] = 0x8FD1;
            mapDataStd [rangeNo] [0xAAF2 - rangeMin] = 0x90B5;
            mapDataStd [rangeNo] [0xAAF3 - rangeMin] = 0x90B8;
            mapDataStd [rangeNo] [0xAAF4 - rangeMin] = 0x90B1;
            mapDataStd [rangeNo] [0xAAF5 - rangeMin] = 0x90B6;
            mapDataStd [rangeNo] [0xAAF6 - rangeMin] = 0x91C7;
            mapDataStd [rangeNo] [0xAAF7 - rangeMin] = 0x91D1;
            mapDataStd [rangeNo] [0xAAF8 - rangeMin] = 0x9577;
            mapDataStd [rangeNo] [0xAAF9 - rangeMin] = 0x9580;
            mapDataStd [rangeNo] [0xAAFA - rangeMin] = 0x961C;
            mapDataStd [rangeNo] [0xAAFB - rangeMin] = 0x9640;
            mapDataStd [rangeNo] [0xAAFC - rangeMin] = 0x963F;
            mapDataStd [rangeNo] [0xAAFD - rangeMin] = 0x963B;
            mapDataStd [rangeNo] [0xAAFE - rangeMin] = 0x9644;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xAB40 - rangeMin] = 0x9642;
            mapDataStd [rangeNo] [0xAB41 - rangeMin] = 0x96B9;
            mapDataStd [rangeNo] [0xAB42 - rangeMin] = 0x96E8;
            mapDataStd [rangeNo] [0xAB43 - rangeMin] = 0x9752;
            mapDataStd [rangeNo] [0xAB44 - rangeMin] = 0x975E;
            mapDataStd [rangeNo] [0xAB45 - rangeMin] = 0x4E9F;
            mapDataStd [rangeNo] [0xAB46 - rangeMin] = 0x4EAD;
            mapDataStd [rangeNo] [0xAB47 - rangeMin] = 0x4EAE;
            mapDataStd [rangeNo] [0xAB48 - rangeMin] = 0x4FE1;
            mapDataStd [rangeNo] [0xAB49 - rangeMin] = 0x4FB5;
            mapDataStd [rangeNo] [0xAB4A - rangeMin] = 0x4FAF;
            mapDataStd [rangeNo] [0xAB4B - rangeMin] = 0x4FBF;
            mapDataStd [rangeNo] [0xAB4C - rangeMin] = 0x4FE0;
            mapDataStd [rangeNo] [0xAB4D - rangeMin] = 0x4FD1;
            mapDataStd [rangeNo] [0xAB4E - rangeMin] = 0x4FCF;
            mapDataStd [rangeNo] [0xAB4F - rangeMin] = 0x4FDD;

            mapDataStd [rangeNo] [0xAB50 - rangeMin] = 0x4FC3;
            mapDataStd [rangeNo] [0xAB51 - rangeMin] = 0x4FB6;
            mapDataStd [rangeNo] [0xAB52 - rangeMin] = 0x4FD8;
            mapDataStd [rangeNo] [0xAB53 - rangeMin] = 0x4FDF;
            mapDataStd [rangeNo] [0xAB54 - rangeMin] = 0x4FCA;
            mapDataStd [rangeNo] [0xAB55 - rangeMin] = 0x4FD7;
            mapDataStd [rangeNo] [0xAB56 - rangeMin] = 0x4FAE;
            mapDataStd [rangeNo] [0xAB57 - rangeMin] = 0x4FD0;
            mapDataStd [rangeNo] [0xAB58 - rangeMin] = 0x4FC4;
            mapDataStd [rangeNo] [0xAB59 - rangeMin] = 0x4FC2;
            mapDataStd [rangeNo] [0xAB5A - rangeMin] = 0x4FDA;
            mapDataStd [rangeNo] [0xAB5B - rangeMin] = 0x4FCE;
            mapDataStd [rangeNo] [0xAB5C - rangeMin] = 0x4FDE;
            mapDataStd [rangeNo] [0xAB5D - rangeMin] = 0x4FB7;
            mapDataStd [rangeNo] [0xAB5E - rangeMin] = 0x5157;
            mapDataStd [rangeNo] [0xAB5F - rangeMin] = 0x5192;

            mapDataStd [rangeNo] [0xAB60 - rangeMin] = 0x5191;
            mapDataStd [rangeNo] [0xAB61 - rangeMin] = 0x51A0;
            mapDataStd [rangeNo] [0xAB62 - rangeMin] = 0x524E;
            mapDataStd [rangeNo] [0xAB63 - rangeMin] = 0x5243;
            mapDataStd [rangeNo] [0xAB64 - rangeMin] = 0x524A;
            mapDataStd [rangeNo] [0xAB65 - rangeMin] = 0x524D;
            mapDataStd [rangeNo] [0xAB66 - rangeMin] = 0x524C;
            mapDataStd [rangeNo] [0xAB67 - rangeMin] = 0x524B;
            mapDataStd [rangeNo] [0xAB68 - rangeMin] = 0x5247;
            mapDataStd [rangeNo] [0xAB69 - rangeMin] = 0x52C7;
            mapDataStd [rangeNo] [0xAB6A - rangeMin] = 0x52C9;
            mapDataStd [rangeNo] [0xAB6B - rangeMin] = 0x52C3;
            mapDataStd [rangeNo] [0xAB6C - rangeMin] = 0x52C1;
            mapDataStd [rangeNo] [0xAB6D - rangeMin] = 0x530D;
            mapDataStd [rangeNo] [0xAB6E - rangeMin] = 0x5357;
            mapDataStd [rangeNo] [0xAB6F - rangeMin] = 0x537B;

            mapDataStd [rangeNo] [0xAB70 - rangeMin] = 0x539A;
            mapDataStd [rangeNo] [0xAB71 - rangeMin] = 0x53DB;
            mapDataStd [rangeNo] [0xAB72 - rangeMin] = 0x54AC;
            mapDataStd [rangeNo] [0xAB73 - rangeMin] = 0x54C0;
            mapDataStd [rangeNo] [0xAB74 - rangeMin] = 0x54A8;
            mapDataStd [rangeNo] [0xAB75 - rangeMin] = 0x54CE;
            mapDataStd [rangeNo] [0xAB76 - rangeMin] = 0x54C9;
            mapDataStd [rangeNo] [0xAB77 - rangeMin] = 0x54B8;
            mapDataStd [rangeNo] [0xAB78 - rangeMin] = 0x54A6;
            mapDataStd [rangeNo] [0xAB79 - rangeMin] = 0x54B3;
            mapDataStd [rangeNo] [0xAB7A - rangeMin] = 0x54C7;
            mapDataStd [rangeNo] [0xAB7B - rangeMin] = 0x54C2;
            mapDataStd [rangeNo] [0xAB7C - rangeMin] = 0x54BD;
            mapDataStd [rangeNo] [0xAB7D - rangeMin] = 0x54AA;
            mapDataStd [rangeNo] [0xAB7E - rangeMin] = 0x54C1;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xABA1 - rangeMin] = 0x54C4;
            mapDataStd [rangeNo] [0xABA2 - rangeMin] = 0x54C8;
            mapDataStd [rangeNo] [0xABA3 - rangeMin] = 0x54AF;
            mapDataStd [rangeNo] [0xABA4 - rangeMin] = 0x54AB;
            mapDataStd [rangeNo] [0xABA5 - rangeMin] = 0x54B1;
            mapDataStd [rangeNo] [0xABA6 - rangeMin] = 0x54BB;
            mapDataStd [rangeNo] [0xABA7 - rangeMin] = 0x54A9;
            mapDataStd [rangeNo] [0xABA8 - rangeMin] = 0x54A7;
            mapDataStd [rangeNo] [0xABA9 - rangeMin] = 0x54BF;
            mapDataStd [rangeNo] [0xABAA - rangeMin] = 0x56FF;
            mapDataStd [rangeNo] [0xABAB - rangeMin] = 0x5782;
            mapDataStd [rangeNo] [0xABAC - rangeMin] = 0x578B;
            mapDataStd [rangeNo] [0xABAD - rangeMin] = 0x57A0;
            mapDataStd [rangeNo] [0xABAE - rangeMin] = 0x57A3;
            mapDataStd [rangeNo] [0xABAF - rangeMin] = 0x57A2;

            mapDataStd [rangeNo] [0xABB0 - rangeMin] = 0x57CE;
            mapDataStd [rangeNo] [0xABB1 - rangeMin] = 0x57AE;
            mapDataStd [rangeNo] [0xABB2 - rangeMin] = 0x5793;
            mapDataStd [rangeNo] [0xABB3 - rangeMin] = 0x5955;
            mapDataStd [rangeNo] [0xABB4 - rangeMin] = 0x5951;
            mapDataStd [rangeNo] [0xABB5 - rangeMin] = 0x594F;
            mapDataStd [rangeNo] [0xABB6 - rangeMin] = 0x594E;
            mapDataStd [rangeNo] [0xABB7 - rangeMin] = 0x5950;
            mapDataStd [rangeNo] [0xABB8 - rangeMin] = 0x59DC;
            mapDataStd [rangeNo] [0xABB9 - rangeMin] = 0x59D8;
            mapDataStd [rangeNo] [0xABBA - rangeMin] = 0x59FF;
            mapDataStd [rangeNo] [0xABBB - rangeMin] = 0x59E3;
            mapDataStd [rangeNo] [0xABBC - rangeMin] = 0x59E8;
            mapDataStd [rangeNo] [0xABBD - rangeMin] = 0x5A03;
            mapDataStd [rangeNo] [0xABBE - rangeMin] = 0x59E5;
            mapDataStd [rangeNo] [0xABBF - rangeMin] = 0x59EA;

            mapDataStd [rangeNo] [0xABC0 - rangeMin] = 0x59DA;
            mapDataStd [rangeNo] [0xABC1 - rangeMin] = 0x59E6;
            mapDataStd [rangeNo] [0xABC2 - rangeMin] = 0x5A01;
            mapDataStd [rangeNo] [0xABC3 - rangeMin] = 0x59FB;
            mapDataStd [rangeNo] [0xABC4 - rangeMin] = 0x5B69;
            mapDataStd [rangeNo] [0xABC5 - rangeMin] = 0x5BA3;
            mapDataStd [rangeNo] [0xABC6 - rangeMin] = 0x5BA6;
            mapDataStd [rangeNo] [0xABC7 - rangeMin] = 0x5BA4;
            mapDataStd [rangeNo] [0xABC8 - rangeMin] = 0x5BA2;
            mapDataStd [rangeNo] [0xABC9 - rangeMin] = 0x5BA5;
            mapDataStd [rangeNo] [0xABCA - rangeMin] = 0x5C01;
            mapDataStd [rangeNo] [0xABCB - rangeMin] = 0x5C4E;
            mapDataStd [rangeNo] [0xABCC - rangeMin] = 0x5C4F;
            mapDataStd [rangeNo] [0xABCD - rangeMin] = 0x5C4D;
            mapDataStd [rangeNo] [0xABCE - rangeMin] = 0x5C4B;
            mapDataStd [rangeNo] [0xABCF - rangeMin] = 0x5CD9;

            mapDataStd [rangeNo] [0xABD0 - rangeMin] = 0x5CD2;
            mapDataStd [rangeNo] [0xABD1 - rangeMin] = 0x5DF7;
            mapDataStd [rangeNo] [0xABD2 - rangeMin] = 0x5E1D;
            mapDataStd [rangeNo] [0xABD3 - rangeMin] = 0x5E25;
            mapDataStd [rangeNo] [0xABD4 - rangeMin] = 0x5E1F;
            mapDataStd [rangeNo] [0xABD5 - rangeMin] = 0x5E7D;
            mapDataStd [rangeNo] [0xABD6 - rangeMin] = 0x5EA0;
            mapDataStd [rangeNo] [0xABD7 - rangeMin] = 0x5EA6;
            mapDataStd [rangeNo] [0xABD8 - rangeMin] = 0x5EFA;
            mapDataStd [rangeNo] [0xABD9 - rangeMin] = 0x5F08;
            mapDataStd [rangeNo] [0xABDA - rangeMin] = 0x5F2D;
            mapDataStd [rangeNo] [0xABDB - rangeMin] = 0x5F65;
            mapDataStd [rangeNo] [0xABDC - rangeMin] = 0x5F88;
            mapDataStd [rangeNo] [0xABDD - rangeMin] = 0x5F85;
            mapDataStd [rangeNo] [0xABDE - rangeMin] = 0x5F8A;
            mapDataStd [rangeNo] [0xABDF - rangeMin] = 0x5F8B;

            mapDataStd [rangeNo] [0xABE0 - rangeMin] = 0x5F87;
            mapDataStd [rangeNo] [0xABE1 - rangeMin] = 0x5F8C;
            mapDataStd [rangeNo] [0xABE2 - rangeMin] = 0x5F89;
            mapDataStd [rangeNo] [0xABE3 - rangeMin] = 0x6012;
            mapDataStd [rangeNo] [0xABE4 - rangeMin] = 0x601D;
            mapDataStd [rangeNo] [0xABE5 - rangeMin] = 0x6020;
            mapDataStd [rangeNo] [0xABE6 - rangeMin] = 0x6025;
            mapDataStd [rangeNo] [0xABE7 - rangeMin] = 0x600E;
            mapDataStd [rangeNo] [0xABE8 - rangeMin] = 0x6028;
            mapDataStd [rangeNo] [0xABE9 - rangeMin] = 0x604D;
            mapDataStd [rangeNo] [0xABEA - rangeMin] = 0x6070;
            mapDataStd [rangeNo] [0xABEB - rangeMin] = 0x6068;
            mapDataStd [rangeNo] [0xABEC - rangeMin] = 0x6062;
            mapDataStd [rangeNo] [0xABED - rangeMin] = 0x6046;
            mapDataStd [rangeNo] [0xABEE - rangeMin] = 0x6043;
            mapDataStd [rangeNo] [0xABEF - rangeMin] = 0x606C;

            mapDataStd [rangeNo] [0xABF0 - rangeMin] = 0x606B;
            mapDataStd [rangeNo] [0xABF1 - rangeMin] = 0x606A;
            mapDataStd [rangeNo] [0xABF2 - rangeMin] = 0x6064;
            mapDataStd [rangeNo] [0xABF3 - rangeMin] = 0x6241;
            mapDataStd [rangeNo] [0xABF4 - rangeMin] = 0x62DC;
            mapDataStd [rangeNo] [0xABF5 - rangeMin] = 0x6316;
            mapDataStd [rangeNo] [0xABF6 - rangeMin] = 0x6309;
            mapDataStd [rangeNo] [0xABF7 - rangeMin] = 0x62FC;
            mapDataStd [rangeNo] [0xABF8 - rangeMin] = 0x62ED;
            mapDataStd [rangeNo] [0xABF9 - rangeMin] = 0x6301;
            mapDataStd [rangeNo] [0xABFA - rangeMin] = 0x62EE;
            mapDataStd [rangeNo] [0xABFB - rangeMin] = 0x62FD;
            mapDataStd [rangeNo] [0xABFC - rangeMin] = 0x6307;
            mapDataStd [rangeNo] [0xABFD - rangeMin] = 0x62F1;
            mapDataStd [rangeNo] [0xABFE - rangeMin] = 0x62F7;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xAC40 - rangeMin] = 0x62EF;
            mapDataStd [rangeNo] [0xAC41 - rangeMin] = 0x62EC;
            mapDataStd [rangeNo] [0xAC42 - rangeMin] = 0x62FE;
            mapDataStd [rangeNo] [0xAC43 - rangeMin] = 0x62F4;
            mapDataStd [rangeNo] [0xAC44 - rangeMin] = 0x6311;
            mapDataStd [rangeNo] [0xAC45 - rangeMin] = 0x6302;
            mapDataStd [rangeNo] [0xAC46 - rangeMin] = 0x653F;
            mapDataStd [rangeNo] [0xAC47 - rangeMin] = 0x6545;
            mapDataStd [rangeNo] [0xAC48 - rangeMin] = 0x65AB;
            mapDataStd [rangeNo] [0xAC49 - rangeMin] = 0x65BD;
            mapDataStd [rangeNo] [0xAC4A - rangeMin] = 0x65E2;
            mapDataStd [rangeNo] [0xAC4B - rangeMin] = 0x6625;
            mapDataStd [rangeNo] [0xAC4C - rangeMin] = 0x662D;
            mapDataStd [rangeNo] [0xAC4D - rangeMin] = 0x6620;
            mapDataStd [rangeNo] [0xAC4E - rangeMin] = 0x6627;
            mapDataStd [rangeNo] [0xAC4F - rangeMin] = 0x662F;

            mapDataStd [rangeNo] [0xAC50 - rangeMin] = 0x661F;
            mapDataStd [rangeNo] [0xAC51 - rangeMin] = 0x6628;
            mapDataStd [rangeNo] [0xAC52 - rangeMin] = 0x6631;
            mapDataStd [rangeNo] [0xAC53 - rangeMin] = 0x6624;
            mapDataStd [rangeNo] [0xAC54 - rangeMin] = 0x66F7;
            mapDataStd [rangeNo] [0xAC55 - rangeMin] = 0x67FF;
            mapDataStd [rangeNo] [0xAC56 - rangeMin] = 0x67D3;
            mapDataStd [rangeNo] [0xAC57 - rangeMin] = 0x67F1;
            mapDataStd [rangeNo] [0xAC58 - rangeMin] = 0x67D4;
            mapDataStd [rangeNo] [0xAC59 - rangeMin] = 0x67D0;
            mapDataStd [rangeNo] [0xAC5A - rangeMin] = 0x67EC;
            mapDataStd [rangeNo] [0xAC5B - rangeMin] = 0x67B6;
            mapDataStd [rangeNo] [0xAC5C - rangeMin] = 0x67AF;
            mapDataStd [rangeNo] [0xAC5D - rangeMin] = 0x67F5;
            mapDataStd [rangeNo] [0xAC5E - rangeMin] = 0x67E9;
            mapDataStd [rangeNo] [0xAC5F - rangeMin] = 0x67EF;

            mapDataStd [rangeNo] [0xAC60 - rangeMin] = 0x67C4;
            mapDataStd [rangeNo] [0xAC61 - rangeMin] = 0x67D1;
            mapDataStd [rangeNo] [0xAC62 - rangeMin] = 0x67B4;
            mapDataStd [rangeNo] [0xAC63 - rangeMin] = 0x67DA;
            mapDataStd [rangeNo] [0xAC64 - rangeMin] = 0x67E5;
            mapDataStd [rangeNo] [0xAC65 - rangeMin] = 0x67B8;
            mapDataStd [rangeNo] [0xAC66 - rangeMin] = 0x67CF;
            mapDataStd [rangeNo] [0xAC67 - rangeMin] = 0x67DE;
            mapDataStd [rangeNo] [0xAC68 - rangeMin] = 0x67F3;
            mapDataStd [rangeNo] [0xAC69 - rangeMin] = 0x67B0;
            mapDataStd [rangeNo] [0xAC6A - rangeMin] = 0x67D9;
            mapDataStd [rangeNo] [0xAC6B - rangeMin] = 0x67E2;
            mapDataStd [rangeNo] [0xAC6C - rangeMin] = 0x67DD;
            mapDataStd [rangeNo] [0xAC6D - rangeMin] = 0x67D2;
            mapDataStd [rangeNo] [0xAC6E - rangeMin] = 0x6B6A;
            mapDataStd [rangeNo] [0xAC6F - rangeMin] = 0x6B83;

            mapDataStd [rangeNo] [0xAC70 - rangeMin] = 0x6B86;
            mapDataStd [rangeNo] [0xAC71 - rangeMin] = 0x6BB5;
            mapDataStd [rangeNo] [0xAC72 - rangeMin] = 0x6BD2;
            mapDataStd [rangeNo] [0xAC73 - rangeMin] = 0x6BD7;
            mapDataStd [rangeNo] [0xAC74 - rangeMin] = 0x6C1F;
            mapDataStd [rangeNo] [0xAC75 - rangeMin] = 0x6CC9;
            mapDataStd [rangeNo] [0xAC76 - rangeMin] = 0x6D0B;
            mapDataStd [rangeNo] [0xAC77 - rangeMin] = 0x6D32;
            mapDataStd [rangeNo] [0xAC78 - rangeMin] = 0x6D2A;
            mapDataStd [rangeNo] [0xAC79 - rangeMin] = 0x6D41;
            mapDataStd [rangeNo] [0xAC7A - rangeMin] = 0x6D25;
            mapDataStd [rangeNo] [0xAC7B - rangeMin] = 0x6D0C;
            mapDataStd [rangeNo] [0xAC7C - rangeMin] = 0x6D31;
            mapDataStd [rangeNo] [0xAC7D - rangeMin] = 0x6D1E;
            mapDataStd [rangeNo] [0xAC7E - rangeMin] = 0x6D17;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xACA1 - rangeMin] = 0x6D3B;
            mapDataStd [rangeNo] [0xACA2 - rangeMin] = 0x6D3D;
            mapDataStd [rangeNo] [0xACA3 - rangeMin] = 0x6D3E;
            mapDataStd [rangeNo] [0xACA4 - rangeMin] = 0x6D36;
            mapDataStd [rangeNo] [0xACA5 - rangeMin] = 0x6D1B;
            mapDataStd [rangeNo] [0xACA6 - rangeMin] = 0x6CF5;
            mapDataStd [rangeNo] [0xACA7 - rangeMin] = 0x6D39;
            mapDataStd [rangeNo] [0xACA8 - rangeMin] = 0x6D27;
            mapDataStd [rangeNo] [0xACA9 - rangeMin] = 0x6D38;
            mapDataStd [rangeNo] [0xACAA - rangeMin] = 0x6D29;
            mapDataStd [rangeNo] [0xACAB - rangeMin] = 0x6D2E;
            mapDataStd [rangeNo] [0xACAC - rangeMin] = 0x6D35;
            mapDataStd [rangeNo] [0xACAD - rangeMin] = 0x6D0E;
            mapDataStd [rangeNo] [0xACAE - rangeMin] = 0x6D2B;
            mapDataStd [rangeNo] [0xACAF - rangeMin] = 0x70AB;

            mapDataStd [rangeNo] [0xACB0 - rangeMin] = 0x70BA;
            mapDataStd [rangeNo] [0xACB1 - rangeMin] = 0x70B3;
            mapDataStd [rangeNo] [0xACB2 - rangeMin] = 0x70AC;
            mapDataStd [rangeNo] [0xACB3 - rangeMin] = 0x70AF;
            mapDataStd [rangeNo] [0xACB4 - rangeMin] = 0x70AD;
            mapDataStd [rangeNo] [0xACB5 - rangeMin] = 0x70B8;
            mapDataStd [rangeNo] [0xACB6 - rangeMin] = 0x70AE;
            mapDataStd [rangeNo] [0xACB7 - rangeMin] = 0x70A4;
            mapDataStd [rangeNo] [0xACB8 - rangeMin] = 0x7230;
            mapDataStd [rangeNo] [0xACB9 - rangeMin] = 0x7272;
            mapDataStd [rangeNo] [0xACBA - rangeMin] = 0x726F;
            mapDataStd [rangeNo] [0xACBB - rangeMin] = 0x7274;
            mapDataStd [rangeNo] [0xACBC - rangeMin] = 0x72E9;
            mapDataStd [rangeNo] [0xACBD - rangeMin] = 0x72E0;
            mapDataStd [rangeNo] [0xACBE - rangeMin] = 0x72E1;
            mapDataStd [rangeNo] [0xACBF - rangeMin] = 0x73B7;

            mapDataStd [rangeNo] [0xACC0 - rangeMin] = 0x73CA;
            mapDataStd [rangeNo] [0xACC1 - rangeMin] = 0x73BB;
            mapDataStd [rangeNo] [0xACC2 - rangeMin] = 0x73B2;
            mapDataStd [rangeNo] [0xACC3 - rangeMin] = 0x73CD;
            mapDataStd [rangeNo] [0xACC4 - rangeMin] = 0x73C0;
            mapDataStd [rangeNo] [0xACC5 - rangeMin] = 0x73B3;
            mapDataStd [rangeNo] [0xACC6 - rangeMin] = 0x751A;
            mapDataStd [rangeNo] [0xACC7 - rangeMin] = 0x752D;
            mapDataStd [rangeNo] [0xACC8 - rangeMin] = 0x754F;
            mapDataStd [rangeNo] [0xACC9 - rangeMin] = 0x754C;
            mapDataStd [rangeNo] [0xACCA - rangeMin] = 0x754E;
            mapDataStd [rangeNo] [0xACCB - rangeMin] = 0x754B;
            mapDataStd [rangeNo] [0xACCC - rangeMin] = 0x75AB;
            mapDataStd [rangeNo] [0xACCD - rangeMin] = 0x75A4;
            mapDataStd [rangeNo] [0xACCE - rangeMin] = 0x75A5;
            mapDataStd [rangeNo] [0xACCF - rangeMin] = 0x75A2;

            mapDataStd [rangeNo] [0xACD0 - rangeMin] = 0x75A3;
            mapDataStd [rangeNo] [0xACD1 - rangeMin] = 0x7678;
            mapDataStd [rangeNo] [0xACD2 - rangeMin] = 0x7686;
            mapDataStd [rangeNo] [0xACD3 - rangeMin] = 0x7687;
            mapDataStd [rangeNo] [0xACD4 - rangeMin] = 0x7688;
            mapDataStd [rangeNo] [0xACD5 - rangeMin] = 0x76C8;
            mapDataStd [rangeNo] [0xACD6 - rangeMin] = 0x76C6;
            mapDataStd [rangeNo] [0xACD7 - rangeMin] = 0x76C3;
            mapDataStd [rangeNo] [0xACD8 - rangeMin] = 0x76C5;
            mapDataStd [rangeNo] [0xACD9 - rangeMin] = 0x7701;
            mapDataStd [rangeNo] [0xACDA - rangeMin] = 0x76F9;
            mapDataStd [rangeNo] [0xACDB - rangeMin] = 0x76F8;
            mapDataStd [rangeNo] [0xACDC - rangeMin] = 0x7709;
            mapDataStd [rangeNo] [0xACDD - rangeMin] = 0x770B;
            mapDataStd [rangeNo] [0xACDE - rangeMin] = 0x76FE;
            mapDataStd [rangeNo] [0xACDF - rangeMin] = 0x76FC;

            mapDataStd [rangeNo] [0xACE0 - rangeMin] = 0x7707;
            mapDataStd [rangeNo] [0xACE1 - rangeMin] = 0x77DC;
            mapDataStd [rangeNo] [0xACE2 - rangeMin] = 0x7802;
            mapDataStd [rangeNo] [0xACE3 - rangeMin] = 0x7814;
            mapDataStd [rangeNo] [0xACE4 - rangeMin] = 0x780C;
            mapDataStd [rangeNo] [0xACE5 - rangeMin] = 0x780D;
            mapDataStd [rangeNo] [0xACE6 - rangeMin] = 0x7946;
            mapDataStd [rangeNo] [0xACE7 - rangeMin] = 0x7949;
            mapDataStd [rangeNo] [0xACE8 - rangeMin] = 0x7948;
            mapDataStd [rangeNo] [0xACE9 - rangeMin] = 0x7947;
            mapDataStd [rangeNo] [0xACEA - rangeMin] = 0x79B9;
            mapDataStd [rangeNo] [0xACEB - rangeMin] = 0x79BA;
            mapDataStd [rangeNo] [0xACEC - rangeMin] = 0x79D1;
            mapDataStd [rangeNo] [0xACED - rangeMin] = 0x79D2;
            mapDataStd [rangeNo] [0xACEE - rangeMin] = 0x79CB;
            mapDataStd [rangeNo] [0xACEF - rangeMin] = 0x7A7F;

            mapDataStd [rangeNo] [0xACF0 - rangeMin] = 0x7A81;
            mapDataStd [rangeNo] [0xACF1 - rangeMin] = 0x7AFF;
            mapDataStd [rangeNo] [0xACF2 - rangeMin] = 0x7AFD;
            mapDataStd [rangeNo] [0xACF3 - rangeMin] = 0x7C7D;
            mapDataStd [rangeNo] [0xACF4 - rangeMin] = 0x7D02;
            mapDataStd [rangeNo] [0xACF5 - rangeMin] = 0x7D05;
            mapDataStd [rangeNo] [0xACF6 - rangeMin] = 0x7D00;
            mapDataStd [rangeNo] [0xACF7 - rangeMin] = 0x7D09;
            mapDataStd [rangeNo] [0xACF8 - rangeMin] = 0x7D07;
            mapDataStd [rangeNo] [0xACF9 - rangeMin] = 0x7D04;
            mapDataStd [rangeNo] [0xACFA - rangeMin] = 0x7D06;
            mapDataStd [rangeNo] [0xACFB - rangeMin] = 0x7F38;
            mapDataStd [rangeNo] [0xACFC - rangeMin] = 0x7F8E;
            mapDataStd [rangeNo] [0xACFD - rangeMin] = 0x7FBF;
            mapDataStd [rangeNo] [0xACFE - rangeMin] = 0x8004;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xAD40 - rangeMin] = 0x8010;
            mapDataStd [rangeNo] [0xAD41 - rangeMin] = 0x800D;
            mapDataStd [rangeNo] [0xAD42 - rangeMin] = 0x8011;
            mapDataStd [rangeNo] [0xAD43 - rangeMin] = 0x8036;
            mapDataStd [rangeNo] [0xAD44 - rangeMin] = 0x80D6;
            mapDataStd [rangeNo] [0xAD45 - rangeMin] = 0x80E5;
            mapDataStd [rangeNo] [0xAD46 - rangeMin] = 0x80DA;
            mapDataStd [rangeNo] [0xAD47 - rangeMin] = 0x80C3;
            mapDataStd [rangeNo] [0xAD48 - rangeMin] = 0x80C4;
            mapDataStd [rangeNo] [0xAD49 - rangeMin] = 0x80CC;
            mapDataStd [rangeNo] [0xAD4A - rangeMin] = 0x80E1;
            mapDataStd [rangeNo] [0xAD4B - rangeMin] = 0x80DB;
            mapDataStd [rangeNo] [0xAD4C - rangeMin] = 0x80CE;
            mapDataStd [rangeNo] [0xAD4D - rangeMin] = 0x80DE;
            mapDataStd [rangeNo] [0xAD4E - rangeMin] = 0x80E4;
            mapDataStd [rangeNo] [0xAD4F - rangeMin] = 0x80DD;

            mapDataStd [rangeNo] [0xAD50 - rangeMin] = 0x81F4;
            mapDataStd [rangeNo] [0xAD51 - rangeMin] = 0x8222;
            mapDataStd [rangeNo] [0xAD52 - rangeMin] = 0x82E7;
            mapDataStd [rangeNo] [0xAD53 - rangeMin] = 0x8303;
            mapDataStd [rangeNo] [0xAD54 - rangeMin] = 0x8305;
            mapDataStd [rangeNo] [0xAD55 - rangeMin] = 0x82E3;
            mapDataStd [rangeNo] [0xAD56 - rangeMin] = 0x82DB;
            mapDataStd [rangeNo] [0xAD57 - rangeMin] = 0x82E6;
            mapDataStd [rangeNo] [0xAD58 - rangeMin] = 0x8304;
            mapDataStd [rangeNo] [0xAD59 - rangeMin] = 0x82E5;
            mapDataStd [rangeNo] [0xAD5A - rangeMin] = 0x8302;
            mapDataStd [rangeNo] [0xAD5B - rangeMin] = 0x8309;
            mapDataStd [rangeNo] [0xAD5C - rangeMin] = 0x82D2;
            mapDataStd [rangeNo] [0xAD5D - rangeMin] = 0x82D7;
            mapDataStd [rangeNo] [0xAD5E - rangeMin] = 0x82F1;
            mapDataStd [rangeNo] [0xAD5F - rangeMin] = 0x8301;

            mapDataStd [rangeNo] [0xAD60 - rangeMin] = 0x82DC;
            mapDataStd [rangeNo] [0xAD61 - rangeMin] = 0x82D4;
            mapDataStd [rangeNo] [0xAD62 - rangeMin] = 0x82D1;
            mapDataStd [rangeNo] [0xAD63 - rangeMin] = 0x82DE;
            mapDataStd [rangeNo] [0xAD64 - rangeMin] = 0x82D3;
            mapDataStd [rangeNo] [0xAD65 - rangeMin] = 0x82DF;
            mapDataStd [rangeNo] [0xAD66 - rangeMin] = 0x82EF;
            mapDataStd [rangeNo] [0xAD67 - rangeMin] = 0x8306;
            mapDataStd [rangeNo] [0xAD68 - rangeMin] = 0x8650;
            mapDataStd [rangeNo] [0xAD69 - rangeMin] = 0x8679;
            mapDataStd [rangeNo] [0xAD6A - rangeMin] = 0x867B;
            mapDataStd [rangeNo] [0xAD6B - rangeMin] = 0x867A;
            mapDataStd [rangeNo] [0xAD6C - rangeMin] = 0x884D;
            mapDataStd [rangeNo] [0xAD6D - rangeMin] = 0x886B;
            mapDataStd [rangeNo] [0xAD6E - rangeMin] = 0x8981;
            mapDataStd [rangeNo] [0xAD6F - rangeMin] = 0x89D4;
            mapDataStd [rangeNo] [0xAD70 - rangeMin] = 0x8A08;

            mapDataStd [rangeNo] [0xAD71 - rangeMin] = 0x8A02;
            mapDataStd [rangeNo] [0xAD72 - rangeMin] = 0x8A03;
            mapDataStd [rangeNo] [0xAD73 - rangeMin] = 0x8C9E;
            mapDataStd [rangeNo] [0xAD74 - rangeMin] = 0x8CA0;
            mapDataStd [rangeNo] [0xAD75 - rangeMin] = 0x8D74;
            mapDataStd [rangeNo] [0xAD76 - rangeMin] = 0x8D73;
            mapDataStd [rangeNo] [0xAD77 - rangeMin] = 0x8DB4;
            mapDataStd [rangeNo] [0xAD78 - rangeMin] = 0x8ECD;
            mapDataStd [rangeNo] [0xAD79 - rangeMin] = 0x8ECC;
            mapDataStd [rangeNo] [0xAD7A - rangeMin] = 0x8FF0;
            mapDataStd [rangeNo] [0xAD7B - rangeMin] = 0x8FE6;
            mapDataStd [rangeNo] [0xAD7C - rangeMin] = 0x8FE2;
            mapDataStd [rangeNo] [0xAD7D - rangeMin] = 0x8FEA;
            mapDataStd [rangeNo] [0xAD7E - rangeMin] = 0x8FE5;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xADA1 - rangeMin] = 0x8FED;
            mapDataStd [rangeNo] [0xADA2 - rangeMin] = 0x8FEB;
            mapDataStd [rangeNo] [0xADA3 - rangeMin] = 0x8FE4;
            mapDataStd [rangeNo] [0xADA4 - rangeMin] = 0x8FE8;
            mapDataStd [rangeNo] [0xADA5 - rangeMin] = 0x90CA;
            mapDataStd [rangeNo] [0xADA6 - rangeMin] = 0x90CE;
            mapDataStd [rangeNo] [0xADA7 - rangeMin] = 0x90C1;
            mapDataStd [rangeNo] [0xADA8 - rangeMin] = 0x90C3;
            mapDataStd [rangeNo] [0xADA9 - rangeMin] = 0x914B;
            mapDataStd [rangeNo] [0xADAA - rangeMin] = 0x914A;
            mapDataStd [rangeNo] [0xADAB - rangeMin] = 0x91CD;
            mapDataStd [rangeNo] [0xADAC - rangeMin] = 0x9582;
            mapDataStd [rangeNo] [0xADAD - rangeMin] = 0x9650;
            mapDataStd [rangeNo] [0xADAE - rangeMin] = 0x964B;
            mapDataStd [rangeNo] [0xADAF - rangeMin] = 0x964C;

            mapDataStd [rangeNo] [0xADB0 - rangeMin] = 0x964D;
            mapDataStd [rangeNo] [0xADB1 - rangeMin] = 0x9762;
            mapDataStd [rangeNo] [0xADB2 - rangeMin] = 0x9769;
            mapDataStd [rangeNo] [0xADB3 - rangeMin] = 0x97CB;
            mapDataStd [rangeNo] [0xADB4 - rangeMin] = 0x97ED;
            mapDataStd [rangeNo] [0xADB5 - rangeMin] = 0x97F3;
            mapDataStd [rangeNo] [0xADB6 - rangeMin] = 0x9801;
            mapDataStd [rangeNo] [0xADB7 - rangeMin] = 0x98A8;
            mapDataStd [rangeNo] [0xADB8 - rangeMin] = 0x98DB;
            mapDataStd [rangeNo] [0xADB9 - rangeMin] = 0x98DF;
            mapDataStd [rangeNo] [0xADBA - rangeMin] = 0x9996;
            mapDataStd [rangeNo] [0xADBB - rangeMin] = 0x9999;
            mapDataStd [rangeNo] [0xADBC - rangeMin] = 0x4E58;
            mapDataStd [rangeNo] [0xADBD - rangeMin] = 0x4EB3;
            mapDataStd [rangeNo] [0xADBE - rangeMin] = 0x500C;
            mapDataStd [rangeNo] [0xADBF - rangeMin] = 0x500D;

            mapDataStd [rangeNo] [0xADC0 - rangeMin] = 0x5023;
            mapDataStd [rangeNo] [0xADC1 - rangeMin] = 0x4FEF;
            mapDataStd [rangeNo] [0xADC2 - rangeMin] = 0x5026;
            mapDataStd [rangeNo] [0xADC3 - rangeMin] = 0x5025;
            mapDataStd [rangeNo] [0xADC4 - rangeMin] = 0x4FF8;
            mapDataStd [rangeNo] [0xADC5 - rangeMin] = 0x5029;
            mapDataStd [rangeNo] [0xADC6 - rangeMin] = 0x5016;
            mapDataStd [rangeNo] [0xADC7 - rangeMin] = 0x5006;
            mapDataStd [rangeNo] [0xADC8 - rangeMin] = 0x503C;
            mapDataStd [rangeNo] [0xADC9 - rangeMin] = 0x501F;
            mapDataStd [rangeNo] [0xADCA - rangeMin] = 0x501A;
            mapDataStd [rangeNo] [0xADCB - rangeMin] = 0x5012;
            mapDataStd [rangeNo] [0xADCC - rangeMin] = 0x5011;
            mapDataStd [rangeNo] [0xADCD - rangeMin] = 0x4FFA;
            mapDataStd [rangeNo] [0xADCE - rangeMin] = 0x5000;
            mapDataStd [rangeNo] [0xADCF - rangeMin] = 0x5014;

            mapDataStd [rangeNo] [0xADD0 - rangeMin] = 0x5028;
            mapDataStd [rangeNo] [0xADD1 - rangeMin] = 0x4FF1;
            mapDataStd [rangeNo] [0xADD2 - rangeMin] = 0x5021;
            mapDataStd [rangeNo] [0xADD3 - rangeMin] = 0x500B;
            mapDataStd [rangeNo] [0xADD4 - rangeMin] = 0x5019;
            mapDataStd [rangeNo] [0xADD5 - rangeMin] = 0x5018;
            mapDataStd [rangeNo] [0xADD6 - rangeMin] = 0x4FF3;
            mapDataStd [rangeNo] [0xADD7 - rangeMin] = 0x4FEE;
            mapDataStd [rangeNo] [0xADD8 - rangeMin] = 0x502D;
            mapDataStd [rangeNo] [0xADD9 - rangeMin] = 0x502A;
            mapDataStd [rangeNo] [0xADDA - rangeMin] = 0x4FFE;
            mapDataStd [rangeNo] [0xADDB - rangeMin] = 0x502B;
            mapDataStd [rangeNo] [0xADDC - rangeMin] = 0x5009;
            mapDataStd [rangeNo] [0xADDD - rangeMin] = 0x517C;
            mapDataStd [rangeNo] [0xADDE - rangeMin] = 0x51A4;
            mapDataStd [rangeNo] [0xADDF - rangeMin] = 0x51A5;

            mapDataStd [rangeNo] [0xADE0 - rangeMin] = 0x51A2;
            mapDataStd [rangeNo] [0xADE1 - rangeMin] = 0x51CD;
            mapDataStd [rangeNo] [0xADE2 - rangeMin] = 0x51CC;
            mapDataStd [rangeNo] [0xADE3 - rangeMin] = 0x51C6;
            mapDataStd [rangeNo] [0xADE4 - rangeMin] = 0x51CB;
            mapDataStd [rangeNo] [0xADE5 - rangeMin] = 0x5256;
            mapDataStd [rangeNo] [0xADE6 - rangeMin] = 0x525C;
            mapDataStd [rangeNo] [0xADE7 - rangeMin] = 0x5254;
            mapDataStd [rangeNo] [0xADE8 - rangeMin] = 0x525B;
            mapDataStd [rangeNo] [0xADE9 - rangeMin] = 0x525D;
            mapDataStd [rangeNo] [0xADEA - rangeMin] = 0x532A;
            mapDataStd [rangeNo] [0xADEB - rangeMin] = 0x537F;
            mapDataStd [rangeNo] [0xADEC - rangeMin] = 0x539F;
            mapDataStd [rangeNo] [0xADED - rangeMin] = 0x539D;
            mapDataStd [rangeNo] [0xADEE - rangeMin] = 0x53DF;
            mapDataStd [rangeNo] [0xADEF - rangeMin] = 0x54E8;

            mapDataStd [rangeNo] [0xADF0 - rangeMin] = 0x5510;
            mapDataStd [rangeNo] [0xADF1 - rangeMin] = 0x5501;
            mapDataStd [rangeNo] [0xADF2 - rangeMin] = 0x5537;
            mapDataStd [rangeNo] [0xADF3 - rangeMin] = 0x54FC;
            mapDataStd [rangeNo] [0xADF4 - rangeMin] = 0x54E5;
            mapDataStd [rangeNo] [0xADF5 - rangeMin] = 0x54F2;
            mapDataStd [rangeNo] [0xADF6 - rangeMin] = 0x5506;
            mapDataStd [rangeNo] [0xADF7 - rangeMin] = 0x54FA;
            mapDataStd [rangeNo] [0xADF8 - rangeMin] = 0x5514;
            mapDataStd [rangeNo] [0xADF9 - rangeMin] = 0x54E9;
            mapDataStd [rangeNo] [0xADFA - rangeMin] = 0x54ED;
            mapDataStd [rangeNo] [0xADFB - rangeMin] = 0x54E1;
            mapDataStd [rangeNo] [0xADFC - rangeMin] = 0x5509;
            mapDataStd [rangeNo] [0xADFD - rangeMin] = 0x54EE;
            mapDataStd [rangeNo] [0xADFE - rangeMin] = 0x54EA;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xAE40 - rangeMin] = 0x54E6;
            mapDataStd [rangeNo] [0xAE41 - rangeMin] = 0x5527;
            mapDataStd [rangeNo] [0xAE42 - rangeMin] = 0x5507;
            mapDataStd [rangeNo] [0xAE43 - rangeMin] = 0x54FD;
            mapDataStd [rangeNo] [0xAE44 - rangeMin] = 0x550F;
            mapDataStd [rangeNo] [0xAE45 - rangeMin] = 0x5703;
            mapDataStd [rangeNo] [0xAE46 - rangeMin] = 0x5704;
            mapDataStd [rangeNo] [0xAE47 - rangeMin] = 0x57C2;
            mapDataStd [rangeNo] [0xAE48 - rangeMin] = 0x57D4;
            mapDataStd [rangeNo] [0xAE49 - rangeMin] = 0x57CB;
            mapDataStd [rangeNo] [0xAE4A - rangeMin] = 0x57C3;
            mapDataStd [rangeNo] [0xAE4B - rangeMin] = 0x5809;
            mapDataStd [rangeNo] [0xAE4C - rangeMin] = 0x590F;
            mapDataStd [rangeNo] [0xAE4D - rangeMin] = 0x5957;
            mapDataStd [rangeNo] [0xAE4E - rangeMin] = 0x5958;
            mapDataStd [rangeNo] [0xAE4F - rangeMin] = 0x595A;

            mapDataStd [rangeNo] [0xAE50 - rangeMin] = 0x5A11;
            mapDataStd [rangeNo] [0xAE51 - rangeMin] = 0x5A18;
            mapDataStd [rangeNo] [0xAE52 - rangeMin] = 0x5A1C;
            mapDataStd [rangeNo] [0xAE53 - rangeMin] = 0x5A1F;
            mapDataStd [rangeNo] [0xAE54 - rangeMin] = 0x5A1B;
            mapDataStd [rangeNo] [0xAE55 - rangeMin] = 0x5A13;
            mapDataStd [rangeNo] [0xAE56 - rangeMin] = 0x59EC;
            mapDataStd [rangeNo] [0xAE57 - rangeMin] = 0x5A20;
            mapDataStd [rangeNo] [0xAE58 - rangeMin] = 0x5A23;
            mapDataStd [rangeNo] [0xAE59 - rangeMin] = 0x5A29;
            mapDataStd [rangeNo] [0xAE5A - rangeMin] = 0x5A25;
            mapDataStd [rangeNo] [0xAE5B - rangeMin] = 0x5A0C;
            mapDataStd [rangeNo] [0xAE5C - rangeMin] = 0x5A09;
            mapDataStd [rangeNo] [0xAE5D - rangeMin] = 0x5B6B;
            mapDataStd [rangeNo] [0xAE5E - rangeMin] = 0x5C58;
            mapDataStd [rangeNo] [0xAE5F - rangeMin] = 0x5BB0;

            mapDataStd [rangeNo] [0xAE60 - rangeMin] = 0x5BB3;
            mapDataStd [rangeNo] [0xAE61 - rangeMin] = 0x5BB6;
            mapDataStd [rangeNo] [0xAE62 - rangeMin] = 0x5BB4;
            mapDataStd [rangeNo] [0xAE63 - rangeMin] = 0x5BAE;
            mapDataStd [rangeNo] [0xAE64 - rangeMin] = 0x5BB5;
            mapDataStd [rangeNo] [0xAE65 - rangeMin] = 0x5BB9;
            mapDataStd [rangeNo] [0xAE66 - rangeMin] = 0x5BB8;
            mapDataStd [rangeNo] [0xAE67 - rangeMin] = 0x5C04;
            mapDataStd [rangeNo] [0xAE68 - rangeMin] = 0x5C51;
            mapDataStd [rangeNo] [0xAE69 - rangeMin] = 0x5C55;
            mapDataStd [rangeNo] [0xAE6A - rangeMin] = 0x5C50;
            mapDataStd [rangeNo] [0xAE6B - rangeMin] = 0x5CED;
            mapDataStd [rangeNo] [0xAE6C - rangeMin] = 0x5CFD;
            mapDataStd [rangeNo] [0xAE6D - rangeMin] = 0x5CFB;
            mapDataStd [rangeNo] [0xAE6E - rangeMin] = 0x5CEA;
            mapDataStd [rangeNo] [0xAE6F - rangeMin] = 0x5CE8;

            mapDataStd [rangeNo] [0xAE70 - rangeMin] = 0x5CF0;
            mapDataStd [rangeNo] [0xAE71 - rangeMin] = 0x5CF6;
            mapDataStd [rangeNo] [0xAE72 - rangeMin] = 0x5D01;
            mapDataStd [rangeNo] [0xAE73 - rangeMin] = 0x5CF4;
            mapDataStd [rangeNo] [0xAE74 - rangeMin] = 0x5DEE;
            mapDataStd [rangeNo] [0xAE75 - rangeMin] = 0x5E2D;
            mapDataStd [rangeNo] [0xAE76 - rangeMin] = 0x5E2B;
            mapDataStd [rangeNo] [0xAE77 - rangeMin] = 0x5EAB;
            mapDataStd [rangeNo] [0xAE78 - rangeMin] = 0x5EAD;
            mapDataStd [rangeNo] [0xAE79 - rangeMin] = 0x5EA7;
            mapDataStd [rangeNo] [0xAE7A - rangeMin] = 0x5F31;
            mapDataStd [rangeNo] [0xAE7B - rangeMin] = 0x5F92;
            mapDataStd [rangeNo] [0xAE7C - rangeMin] = 0x5F91;
            mapDataStd [rangeNo] [0xAE7D - rangeMin] = 0x5F90;
            mapDataStd [rangeNo] [0xAE7E - rangeMin] = 0x6059;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xAEA1 - rangeMin] = 0x6063;
            mapDataStd [rangeNo] [0xAEA2 - rangeMin] = 0x6065;
            mapDataStd [rangeNo] [0xAEA3 - rangeMin] = 0x6050;
            mapDataStd [rangeNo] [0xAEA4 - rangeMin] = 0x6055;
            mapDataStd [rangeNo] [0xAEA5 - rangeMin] = 0x606D;
            mapDataStd [rangeNo] [0xAEA6 - rangeMin] = 0x6069;
            mapDataStd [rangeNo] [0xAEA7 - rangeMin] = 0x606F;
            mapDataStd [rangeNo] [0xAEA8 - rangeMin] = 0x6084;
            mapDataStd [rangeNo] [0xAEA9 - rangeMin] = 0x609F;
            mapDataStd [rangeNo] [0xAEAA - rangeMin] = 0x609A;
            mapDataStd [rangeNo] [0xAEAB - rangeMin] = 0x608D;
            mapDataStd [rangeNo] [0xAEAC - rangeMin] = 0x6094;
            mapDataStd [rangeNo] [0xAEAD - rangeMin] = 0x608C;
            mapDataStd [rangeNo] [0xAEAE - rangeMin] = 0x6085;
            mapDataStd [rangeNo] [0xAEAF - rangeMin] = 0x6096;

            mapDataStd [rangeNo] [0xAEB0 - rangeMin] = 0x6247;
            mapDataStd [rangeNo] [0xAEB1 - rangeMin] = 0x62F3;
            mapDataStd [rangeNo] [0xAEB2 - rangeMin] = 0x6308;
            mapDataStd [rangeNo] [0xAEB3 - rangeMin] = 0x62FF;
            mapDataStd [rangeNo] [0xAEB4 - rangeMin] = 0x634E;
            mapDataStd [rangeNo] [0xAEB5 - rangeMin] = 0x633E;
            mapDataStd [rangeNo] [0xAEB6 - rangeMin] = 0x632F;
            mapDataStd [rangeNo] [0xAEB7 - rangeMin] = 0x6355;
            mapDataStd [rangeNo] [0xAEB8 - rangeMin] = 0x6342;
            mapDataStd [rangeNo] [0xAEB9 - rangeMin] = 0x6346;
            mapDataStd [rangeNo] [0xAEBA - rangeMin] = 0x634F;
            mapDataStd [rangeNo] [0xAEBB - rangeMin] = 0x6349;
            mapDataStd [rangeNo] [0xAEBC - rangeMin] = 0x633A;
            mapDataStd [rangeNo] [0xAEBD - rangeMin] = 0x6350;
            mapDataStd [rangeNo] [0xAEBE - rangeMin] = 0x633D;
            mapDataStd [rangeNo] [0xAEBF - rangeMin] = 0x632A;

            mapDataStd [rangeNo] [0xAEC0 - rangeMin] = 0x632B;
            mapDataStd [rangeNo] [0xAEC1 - rangeMin] = 0x6328;
            mapDataStd [rangeNo] [0xAEC2 - rangeMin] = 0x634D;
            mapDataStd [rangeNo] [0xAEC3 - rangeMin] = 0x634C;
            mapDataStd [rangeNo] [0xAEC4 - rangeMin] = 0x6548;
            mapDataStd [rangeNo] [0xAEC5 - rangeMin] = 0x6549;
            mapDataStd [rangeNo] [0xAEC6 - rangeMin] = 0x6599;
            mapDataStd [rangeNo] [0xAEC7 - rangeMin] = 0x65C1;
            mapDataStd [rangeNo] [0xAEC8 - rangeMin] = 0x65C5;
            mapDataStd [rangeNo] [0xAEC9 - rangeMin] = 0x6642;
            mapDataStd [rangeNo] [0xAECA - rangeMin] = 0x6649;
            mapDataStd [rangeNo] [0xAECB - rangeMin] = 0x664F;
            mapDataStd [rangeNo] [0xAECC - rangeMin] = 0x6643;
            mapDataStd [rangeNo] [0xAECD - rangeMin] = 0x6652;
            mapDataStd [rangeNo] [0xAECE - rangeMin] = 0x664C;
            mapDataStd [rangeNo] [0xAECF - rangeMin] = 0x6645;

            mapDataStd [rangeNo] [0xAED0 - rangeMin] = 0x6641;
            mapDataStd [rangeNo] [0xAED1 - rangeMin] = 0x66F8;
            mapDataStd [rangeNo] [0xAED2 - rangeMin] = 0x6714;
            mapDataStd [rangeNo] [0xAED3 - rangeMin] = 0x6715;
            mapDataStd [rangeNo] [0xAED4 - rangeMin] = 0x6717;
            mapDataStd [rangeNo] [0xAED5 - rangeMin] = 0x6821;
            mapDataStd [rangeNo] [0xAED6 - rangeMin] = 0x6838;
            mapDataStd [rangeNo] [0xAED7 - rangeMin] = 0x6848;
            mapDataStd [rangeNo] [0xAED8 - rangeMin] = 0x6846;
            mapDataStd [rangeNo] [0xAED9 - rangeMin] = 0x6853;
            mapDataStd [rangeNo] [0xAEDA - rangeMin] = 0x6839;
            mapDataStd [rangeNo] [0xAEDB - rangeMin] = 0x6842;
            mapDataStd [rangeNo] [0xAEDC - rangeMin] = 0x6854;
            mapDataStd [rangeNo] [0xAEDD - rangeMin] = 0x6829;
            mapDataStd [rangeNo] [0xAEDE - rangeMin] = 0x68B3;
            mapDataStd [rangeNo] [0xAEDF - rangeMin] = 0x6817;

            mapDataStd [rangeNo] [0xAEE0 - rangeMin] = 0x684C;
            mapDataStd [rangeNo] [0xAEE1 - rangeMin] = 0x6851;
            mapDataStd [rangeNo] [0xAEE2 - rangeMin] = 0x683D;
            mapDataStd [rangeNo] [0xAEE3 - rangeMin] = 0x67F4;
            mapDataStd [rangeNo] [0xAEE4 - rangeMin] = 0x6850;
            mapDataStd [rangeNo] [0xAEE5 - rangeMin] = 0x6840;
            mapDataStd [rangeNo] [0xAEE6 - rangeMin] = 0x683C;
            mapDataStd [rangeNo] [0xAEE7 - rangeMin] = 0x6843;
            mapDataStd [rangeNo] [0xAEE8 - rangeMin] = 0x682A;
            mapDataStd [rangeNo] [0xAEE9 - rangeMin] = 0x6845;
            mapDataStd [rangeNo] [0xAEEA - rangeMin] = 0x6813;
            mapDataStd [rangeNo] [0xAEEB - rangeMin] = 0x6818;
            mapDataStd [rangeNo] [0xAEEC - rangeMin] = 0x6841;
            mapDataStd [rangeNo] [0xAEED - rangeMin] = 0x6B8A;
            mapDataStd [rangeNo] [0xAEEE - rangeMin] = 0x6B89;
            mapDataStd [rangeNo] [0xAEEF - rangeMin] = 0x6BB7;

            mapDataStd [rangeNo] [0xAEF0 - rangeMin] = 0x6C23;
            mapDataStd [rangeNo] [0xAEF1 - rangeMin] = 0x6C27;
            mapDataStd [rangeNo] [0xAEF2 - rangeMin] = 0x6C28;
            mapDataStd [rangeNo] [0xAEF3 - rangeMin] = 0x6C26;
            mapDataStd [rangeNo] [0xAEF4 - rangeMin] = 0x6C24;
            mapDataStd [rangeNo] [0xAEF5 - rangeMin] = 0x6CF0;
            mapDataStd [rangeNo] [0xAEF6 - rangeMin] = 0x6D6A;
            mapDataStd [rangeNo] [0xAEF7 - rangeMin] = 0x6D95;
            mapDataStd [rangeNo] [0xAEF8 - rangeMin] = 0x6D88;
            mapDataStd [rangeNo] [0xAEF9 - rangeMin] = 0x6D87;
            mapDataStd [rangeNo] [0xAEFA - rangeMin] = 0x6D66;
            mapDataStd [rangeNo] [0xAEFB - rangeMin] = 0x6D78;
            mapDataStd [rangeNo] [0xAEFC - rangeMin] = 0x6D77;
            mapDataStd [rangeNo] [0xAEFD - rangeMin] = 0x6D59;
            mapDataStd [rangeNo] [0xAEFE - rangeMin] = 0x6D93;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xAF40 - rangeMin] = 0x6D6C;
            mapDataStd [rangeNo] [0xAF41 - rangeMin] = 0x6D89;
            mapDataStd [rangeNo] [0xAF42 - rangeMin] = 0x6D6E;
            mapDataStd [rangeNo] [0xAF43 - rangeMin] = 0x6D5A;
            mapDataStd [rangeNo] [0xAF44 - rangeMin] = 0x6D74;
            mapDataStd [rangeNo] [0xAF45 - rangeMin] = 0x6D69;
            mapDataStd [rangeNo] [0xAF46 - rangeMin] = 0x6D8C;
            mapDataStd [rangeNo] [0xAF47 - rangeMin] = 0x6D8A;
            mapDataStd [rangeNo] [0xAF48 - rangeMin] = 0x6D79;
            mapDataStd [rangeNo] [0xAF49 - rangeMin] = 0x6D85;
            mapDataStd [rangeNo] [0xAF4A - rangeMin] = 0x6D65;
            mapDataStd [rangeNo] [0xAF4B - rangeMin] = 0x6D94;
            mapDataStd [rangeNo] [0xAF4C - rangeMin] = 0x70CA;
            mapDataStd [rangeNo] [0xAF4D - rangeMin] = 0x70D8;
            mapDataStd [rangeNo] [0xAF4E - rangeMin] = 0x70E4;
            mapDataStd [rangeNo] [0xAF4F - rangeMin] = 0x70D9;

            mapDataStd [rangeNo] [0xAF50 - rangeMin] = 0x70C8;
            mapDataStd [rangeNo] [0xAF51 - rangeMin] = 0x70CF;
            mapDataStd [rangeNo] [0xAF52 - rangeMin] = 0x7239;
            mapDataStd [rangeNo] [0xAF53 - rangeMin] = 0x7279;
            mapDataStd [rangeNo] [0xAF54 - rangeMin] = 0x72FC;
            mapDataStd [rangeNo] [0xAF55 - rangeMin] = 0x72F9;
            mapDataStd [rangeNo] [0xAF56 - rangeMin] = 0x72FD;
            mapDataStd [rangeNo] [0xAF57 - rangeMin] = 0x72F8;
            mapDataStd [rangeNo] [0xAF58 - rangeMin] = 0x72F7;
            mapDataStd [rangeNo] [0xAF59 - rangeMin] = 0x7386;
            mapDataStd [rangeNo] [0xAF5A - rangeMin] = 0x73ED;
            mapDataStd [rangeNo] [0xAF5B - rangeMin] = 0x7409;
            mapDataStd [rangeNo] [0xAF5C - rangeMin] = 0x73EE;
            mapDataStd [rangeNo] [0xAF5D - rangeMin] = 0x73E0;
            mapDataStd [rangeNo] [0xAF5E - rangeMin] = 0x73EA;
            mapDataStd [rangeNo] [0xAF5F - rangeMin] = 0x73DE;

            mapDataStd [rangeNo] [0xAF60 - rangeMin] = 0x7554;
            mapDataStd [rangeNo] [0xAF61 - rangeMin] = 0x755D;
            mapDataStd [rangeNo] [0xAF62 - rangeMin] = 0x755C;
            mapDataStd [rangeNo] [0xAF63 - rangeMin] = 0x755A;
            mapDataStd [rangeNo] [0xAF64 - rangeMin] = 0x7559;
            mapDataStd [rangeNo] [0xAF65 - rangeMin] = 0x75BE;
            mapDataStd [rangeNo] [0xAF66 - rangeMin] = 0x75C5;
            mapDataStd [rangeNo] [0xAF67 - rangeMin] = 0x75C7;
            mapDataStd [rangeNo] [0xAF68 - rangeMin] = 0x75B2;
            mapDataStd [rangeNo] [0xAF69 - rangeMin] = 0x75B3;
            mapDataStd [rangeNo] [0xAF6A - rangeMin] = 0x75BD;
            mapDataStd [rangeNo] [0xAF6B - rangeMin] = 0x75BC;
            mapDataStd [rangeNo] [0xAF6C - rangeMin] = 0x75B9;
            mapDataStd [rangeNo] [0xAF6D - rangeMin] = 0x75C2;
            mapDataStd [rangeNo] [0xAF6E - rangeMin] = 0x75B8;
            mapDataStd [rangeNo] [0xAF6F - rangeMin] = 0x768B;

            mapDataStd [rangeNo] [0xAF70 - rangeMin] = 0x76B0;
            mapDataStd [rangeNo] [0xAF71 - rangeMin] = 0x76CA;
            mapDataStd [rangeNo] [0xAF72 - rangeMin] = 0x76CD;
            mapDataStd [rangeNo] [0xAF73 - rangeMin] = 0x76CE;
            mapDataStd [rangeNo] [0xAF74 - rangeMin] = 0x7729;
            mapDataStd [rangeNo] [0xAF75 - rangeMin] = 0x771F;
            mapDataStd [rangeNo] [0xAF76 - rangeMin] = 0x7720;
            mapDataStd [rangeNo] [0xAF77 - rangeMin] = 0x7728;
            mapDataStd [rangeNo] [0xAF78 - rangeMin] = 0x77E9;
            mapDataStd [rangeNo] [0xAF79 - rangeMin] = 0x7830;
            mapDataStd [rangeNo] [0xAF7A - rangeMin] = 0x7827;
            mapDataStd [rangeNo] [0xAF7B - rangeMin] = 0x7838;
            mapDataStd [rangeNo] [0xAF7C - rangeMin] = 0x781D;
            mapDataStd [rangeNo] [0xAF7D - rangeMin] = 0x7834;
            mapDataStd [rangeNo] [0xAF7E - rangeMin] = 0x7837;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xAFA1 - rangeMin] = 0x7825;
            mapDataStd [rangeNo] [0xAFA2 - rangeMin] = 0x782D;
            mapDataStd [rangeNo] [0xAFA3 - rangeMin] = 0x7820;
            mapDataStd [rangeNo] [0xAFA4 - rangeMin] = 0x781F;
            mapDataStd [rangeNo] [0xAFA5 - rangeMin] = 0x7832;
            mapDataStd [rangeNo] [0xAFA6 - rangeMin] = 0x7955;
            mapDataStd [rangeNo] [0xAFA7 - rangeMin] = 0x7950;
            mapDataStd [rangeNo] [0xAFA8 - rangeMin] = 0x7960;
            mapDataStd [rangeNo] [0xAFA9 - rangeMin] = 0x795F;
            mapDataStd [rangeNo] [0xAFAA - rangeMin] = 0x7956;
            mapDataStd [rangeNo] [0xAFAB - rangeMin] = 0x795E;
            mapDataStd [rangeNo] [0xAFAC - rangeMin] = 0x795D;
            mapDataStd [rangeNo] [0xAFAD - rangeMin] = 0x7957;
            mapDataStd [rangeNo] [0xAFAE - rangeMin] = 0x795A;
            mapDataStd [rangeNo] [0xAFAF - rangeMin] = 0x79E4;

            mapDataStd [rangeNo] [0xAFB0 - rangeMin] = 0x79E3;
            mapDataStd [rangeNo] [0xAFB1 - rangeMin] = 0x79E7;
            mapDataStd [rangeNo] [0xAFB2 - rangeMin] = 0x79DF;
            mapDataStd [rangeNo] [0xAFB3 - rangeMin] = 0x79E6;
            mapDataStd [rangeNo] [0xAFB4 - rangeMin] = 0x79E9;
            mapDataStd [rangeNo] [0xAFB5 - rangeMin] = 0x79D8;
            mapDataStd [rangeNo] [0xAFB6 - rangeMin] = 0x7A84;
            mapDataStd [rangeNo] [0xAFB7 - rangeMin] = 0x7A88;
            mapDataStd [rangeNo] [0xAFB8 - rangeMin] = 0x7AD9;
            mapDataStd [rangeNo] [0xAFB9 - rangeMin] = 0x7B06;
            mapDataStd [rangeNo] [0xAFBA - rangeMin] = 0x7B11;
            mapDataStd [rangeNo] [0xAFBB - rangeMin] = 0x7C89;
            mapDataStd [rangeNo] [0xAFBC - rangeMin] = 0x7D21;
            mapDataStd [rangeNo] [0xAFBD - rangeMin] = 0x7D17;
            mapDataStd [rangeNo] [0xAFBE - rangeMin] = 0x7D0B;
            mapDataStd [rangeNo] [0xAFBF - rangeMin] = 0x7D0A;

            mapDataStd [rangeNo] [0xAFC0 - rangeMin] = 0x7D20;
            mapDataStd [rangeNo] [0xAFC1 - rangeMin] = 0x7D22;
            mapDataStd [rangeNo] [0xAFC2 - rangeMin] = 0x7D14;
            mapDataStd [rangeNo] [0xAFC3 - rangeMin] = 0x7D10;
            mapDataStd [rangeNo] [0xAFC4 - rangeMin] = 0x7D15;
            mapDataStd [rangeNo] [0xAFC5 - rangeMin] = 0x7D1A;
            mapDataStd [rangeNo] [0xAFC6 - rangeMin] = 0x7D1C;
            mapDataStd [rangeNo] [0xAFC7 - rangeMin] = 0x7D0D;
            mapDataStd [rangeNo] [0xAFC8 - rangeMin] = 0x7D19;
            mapDataStd [rangeNo] [0xAFC9 - rangeMin] = 0x7D1B;
            mapDataStd [rangeNo] [0xAFCA - rangeMin] = 0x7F3A;
            mapDataStd [rangeNo] [0xAFCB - rangeMin] = 0x7F5F;
            mapDataStd [rangeNo] [0xAFCC - rangeMin] = 0x7F94;
            mapDataStd [rangeNo] [0xAFCD - rangeMin] = 0x7FC5;
            mapDataStd [rangeNo] [0xAFCE - rangeMin] = 0x7FC1;
            mapDataStd [rangeNo] [0xAFCF - rangeMin] = 0x8006;

            mapDataStd [rangeNo] [0xAFD0 - rangeMin] = 0x8018;
            mapDataStd [rangeNo] [0xAFD1 - rangeMin] = 0x8015;
            mapDataStd [rangeNo] [0xAFD2 - rangeMin] = 0x8019;
            mapDataStd [rangeNo] [0xAFD3 - rangeMin] = 0x8017;
            mapDataStd [rangeNo] [0xAFD4 - rangeMin] = 0x803D;
            mapDataStd [rangeNo] [0xAFD5 - rangeMin] = 0x803F;
            mapDataStd [rangeNo] [0xAFD6 - rangeMin] = 0x80F1;
            mapDataStd [rangeNo] [0xAFD7 - rangeMin] = 0x8102;
            mapDataStd [rangeNo] [0xAFD8 - rangeMin] = 0x80F0;
            mapDataStd [rangeNo] [0xAFD9 - rangeMin] = 0x8105;
            mapDataStd [rangeNo] [0xAFDA - rangeMin] = 0x80ED;
            mapDataStd [rangeNo] [0xAFDB - rangeMin] = 0x80F4;
            mapDataStd [rangeNo] [0xAFDC - rangeMin] = 0x8106;
            mapDataStd [rangeNo] [0xAFDD - rangeMin] = 0x80F8;
            mapDataStd [rangeNo] [0xAFDE - rangeMin] = 0x80F3;
            mapDataStd [rangeNo] [0xAFDF - rangeMin] = 0x8108;

            mapDataStd [rangeNo] [0xAFE0 - rangeMin] = 0x80FD;
            mapDataStd [rangeNo] [0xAFE1 - rangeMin] = 0x810A;
            mapDataStd [rangeNo] [0xAFE2 - rangeMin] = 0x80FC;
            mapDataStd [rangeNo] [0xAFE3 - rangeMin] = 0x80EF;
            mapDataStd [rangeNo] [0xAFE4 - rangeMin] = 0x81ED;
            mapDataStd [rangeNo] [0xAFE5 - rangeMin] = 0x81EC;
            mapDataStd [rangeNo] [0xAFE6 - rangeMin] = 0x8200;
            mapDataStd [rangeNo] [0xAFE7 - rangeMin] = 0x8210;
            mapDataStd [rangeNo] [0xAFE8 - rangeMin] = 0x822A;
            mapDataStd [rangeNo] [0xAFE9 - rangeMin] = 0x822B;
            mapDataStd [rangeNo] [0xAFEA - rangeMin] = 0x8228;
            mapDataStd [rangeNo] [0xAFEB - rangeMin] = 0x822C;
            mapDataStd [rangeNo] [0xAFEC - rangeMin] = 0x82BB;
            mapDataStd [rangeNo] [0xAFED - rangeMin] = 0x832B;
            mapDataStd [rangeNo] [0xAFEE - rangeMin] = 0x8352;
            mapDataStd [rangeNo] [0xAFEF - rangeMin] = 0x8354;

            mapDataStd [rangeNo] [0xAFF0 - rangeMin] = 0x834A;
            mapDataStd [rangeNo] [0xAFF1 - rangeMin] = 0x8338;
            mapDataStd [rangeNo] [0xAFF2 - rangeMin] = 0x8350;
            mapDataStd [rangeNo] [0xAFF3 - rangeMin] = 0x8349;
            mapDataStd [rangeNo] [0xAFF4 - rangeMin] = 0x8335;
            mapDataStd [rangeNo] [0xAFF5 - rangeMin] = 0x8334;
            mapDataStd [rangeNo] [0xAFF6 - rangeMin] = 0x834F;
            mapDataStd [rangeNo] [0xAFF7 - rangeMin] = 0x8332;
            mapDataStd [rangeNo] [0xAFF8 - rangeMin] = 0x8339;
            mapDataStd [rangeNo] [0xAFF9 - rangeMin] = 0x8336;
            mapDataStd [rangeNo] [0xAFFA - rangeMin] = 0x8317;
            mapDataStd [rangeNo] [0xAFFB - rangeMin] = 0x8340;
            mapDataStd [rangeNo] [0xAFFC - rangeMin] = 0x8331;
            mapDataStd [rangeNo] [0xAFFD - rangeMin] = 0x8328;
            mapDataStd [rangeNo] [0xAFFE - rangeMin] = 0x8343;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xB040 - rangeMin] = 0x8654;
            mapDataStd [rangeNo] [0xB041 - rangeMin] = 0x868A;
            mapDataStd [rangeNo] [0xB042 - rangeMin] = 0x86AA;
            mapDataStd [rangeNo] [0xB043 - rangeMin] = 0x8693;
            mapDataStd [rangeNo] [0xB044 - rangeMin] = 0x86A4;
            mapDataStd [rangeNo] [0xB045 - rangeMin] = 0x86A9;
            mapDataStd [rangeNo] [0xB046 - rangeMin] = 0x868C;
            mapDataStd [rangeNo] [0xB047 - rangeMin] = 0x86A3;
            mapDataStd [rangeNo] [0xB048 - rangeMin] = 0x869C;
            mapDataStd [rangeNo] [0xB049 - rangeMin] = 0x8870;
            mapDataStd [rangeNo] [0xB04A - rangeMin] = 0x8877;
            mapDataStd [rangeNo] [0xB04B - rangeMin] = 0x8881;
            mapDataStd [rangeNo] [0xB04C - rangeMin] = 0x8882;
            mapDataStd [rangeNo] [0xB04D - rangeMin] = 0x887D;
            mapDataStd [rangeNo] [0xB04E - rangeMin] = 0x8879;
            mapDataStd [rangeNo] [0xB04F - rangeMin] = 0x8A18;

            mapDataStd [rangeNo] [0xB050 - rangeMin] = 0x8A10;
            mapDataStd [rangeNo] [0xB051 - rangeMin] = 0x8A0E;
            mapDataStd [rangeNo] [0xB052 - rangeMin] = 0x8A0C;
            mapDataStd [rangeNo] [0xB053 - rangeMin] = 0x8A15;
            mapDataStd [rangeNo] [0xB054 - rangeMin] = 0x8A0A;
            mapDataStd [rangeNo] [0xB055 - rangeMin] = 0x8A17;
            mapDataStd [rangeNo] [0xB056 - rangeMin] = 0x8A13;
            mapDataStd [rangeNo] [0xB057 - rangeMin] = 0x8A16;
            mapDataStd [rangeNo] [0xB058 - rangeMin] = 0x8A0F;
            mapDataStd [rangeNo] [0xB059 - rangeMin] = 0x8A11;
            mapDataStd [rangeNo] [0xB05A - rangeMin] = 0x8C48;
            mapDataStd [rangeNo] [0xB05B - rangeMin] = 0x8C7A;
            mapDataStd [rangeNo] [0xB05C - rangeMin] = 0x8C79;
            mapDataStd [rangeNo] [0xB05D - rangeMin] = 0x8CA1;
            mapDataStd [rangeNo] [0xB05E - rangeMin] = 0x8CA2;
            mapDataStd [rangeNo] [0xB05F - rangeMin] = 0x8D77;

            mapDataStd [rangeNo] [0xB060 - rangeMin] = 0x8EAC;
            mapDataStd [rangeNo] [0xB061 - rangeMin] = 0x8ED2;
            mapDataStd [rangeNo] [0xB062 - rangeMin] = 0x8ED4;
            mapDataStd [rangeNo] [0xB063 - rangeMin] = 0x8ECF;
            mapDataStd [rangeNo] [0xB064 - rangeMin] = 0x8FB1;
            mapDataStd [rangeNo] [0xB065 - rangeMin] = 0x9001;
            mapDataStd [rangeNo] [0xB066 - rangeMin] = 0x9006;
            mapDataStd [rangeNo] [0xB067 - rangeMin] = 0x8FF7;
            mapDataStd [rangeNo] [0xB068 - rangeMin] = 0x9000;
            mapDataStd [rangeNo] [0xB069 - rangeMin] = 0x8FFA;
            mapDataStd [rangeNo] [0xB06A - rangeMin] = 0x8FF4;
            mapDataStd [rangeNo] [0xB06B - rangeMin] = 0x9003;
            mapDataStd [rangeNo] [0xB06C - rangeMin] = 0x8FFD;
            mapDataStd [rangeNo] [0xB06D - rangeMin] = 0x9005;
            mapDataStd [rangeNo] [0xB06E - rangeMin] = 0x8FF8;
            mapDataStd [rangeNo] [0xB06F - rangeMin] = 0x9095;

            mapDataStd [rangeNo] [0xB070 - rangeMin] = 0x90E1;
            mapDataStd [rangeNo] [0xB071 - rangeMin] = 0x90DD;
            mapDataStd [rangeNo] [0xB072 - rangeMin] = 0x90E2;
            mapDataStd [rangeNo] [0xB073 - rangeMin] = 0x9152;
            mapDataStd [rangeNo] [0xB074 - rangeMin] = 0x914D;
            mapDataStd [rangeNo] [0xB075 - rangeMin] = 0x914C;
            mapDataStd [rangeNo] [0xB076 - rangeMin] = 0x91D8;
            mapDataStd [rangeNo] [0xB077 - rangeMin] = 0x91DD;
            mapDataStd [rangeNo] [0xB078 - rangeMin] = 0x91D7;
            mapDataStd [rangeNo] [0xB079 - rangeMin] = 0x91DC;
            mapDataStd [rangeNo] [0xB07A - rangeMin] = 0x91D9;
            mapDataStd [rangeNo] [0xB07B - rangeMin] = 0x9583;
            mapDataStd [rangeNo] [0xB07C - rangeMin] = 0x9662;
            mapDataStd [rangeNo] [0xB07D - rangeMin] = 0x9663;
            mapDataStd [rangeNo] [0xB07E - rangeMin] = 0x9661;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xB0A1 - rangeMin] = 0x965B;
            mapDataStd [rangeNo] [0xB0A2 - rangeMin] = 0x965D;
            mapDataStd [rangeNo] [0xB0A3 - rangeMin] = 0x9664;
            mapDataStd [rangeNo] [0xB0A4 - rangeMin] = 0x9658;
            mapDataStd [rangeNo] [0xB0A5 - rangeMin] = 0x965E;
            mapDataStd [rangeNo] [0xB0A6 - rangeMin] = 0x96BB;
            mapDataStd [rangeNo] [0xB0A7 - rangeMin] = 0x98E2;
            mapDataStd [rangeNo] [0xB0A8 - rangeMin] = 0x99AC;
            mapDataStd [rangeNo] [0xB0A9 - rangeMin] = 0x9AA8;
            mapDataStd [rangeNo] [0xB0AA - rangeMin] = 0x9AD8;
            mapDataStd [rangeNo] [0xB0AB - rangeMin] = 0x9B25;
            mapDataStd [rangeNo] [0xB0AC - rangeMin] = 0x9B32;
            mapDataStd [rangeNo] [0xB0AD - rangeMin] = 0x9B3C;
            mapDataStd [rangeNo] [0xB0AE - rangeMin] = 0x4E7E;
            mapDataStd [rangeNo] [0xB0AF - rangeMin] = 0x507A;

            mapDataStd [rangeNo] [0xB0B0 - rangeMin] = 0x507D;
            mapDataStd [rangeNo] [0xB0B1 - rangeMin] = 0x505C;
            mapDataStd [rangeNo] [0xB0B2 - rangeMin] = 0x5047;
            mapDataStd [rangeNo] [0xB0B3 - rangeMin] = 0x5043;
            mapDataStd [rangeNo] [0xB0B4 - rangeMin] = 0x504C;
            mapDataStd [rangeNo] [0xB0B5 - rangeMin] = 0x505A;
            mapDataStd [rangeNo] [0xB0B6 - rangeMin] = 0x5049;
            mapDataStd [rangeNo] [0xB0B7 - rangeMin] = 0x5065;
            mapDataStd [rangeNo] [0xB0B8 - rangeMin] = 0x5076;
            mapDataStd [rangeNo] [0xB0B9 - rangeMin] = 0x504E;
            mapDataStd [rangeNo] [0xB0BA - rangeMin] = 0x5055;
            mapDataStd [rangeNo] [0xB0BB - rangeMin] = 0x5075;
            mapDataStd [rangeNo] [0xB0BC - rangeMin] = 0x5074;
            mapDataStd [rangeNo] [0xB0BD - rangeMin] = 0x5077;
            mapDataStd [rangeNo] [0xB0BE - rangeMin] = 0x504F;
            mapDataStd [rangeNo] [0xB0BF - rangeMin] = 0x500F;

            mapDataStd [rangeNo] [0xB0C0 - rangeMin] = 0x506F;
            mapDataStd [rangeNo] [0xB0C1 - rangeMin] = 0x506D;
            mapDataStd [rangeNo] [0xB0C2 - rangeMin] = 0x515C;
            mapDataStd [rangeNo] [0xB0C3 - rangeMin] = 0x5195;
            mapDataStd [rangeNo] [0xB0C4 - rangeMin] = 0x51F0;
            mapDataStd [rangeNo] [0xB0C5 - rangeMin] = 0x526A;
            mapDataStd [rangeNo] [0xB0C6 - rangeMin] = 0x526F;
            mapDataStd [rangeNo] [0xB0C7 - rangeMin] = 0x52D2;
            mapDataStd [rangeNo] [0xB0C8 - rangeMin] = 0x52D9;
            mapDataStd [rangeNo] [0xB0C9 - rangeMin] = 0x52D8;
            mapDataStd [rangeNo] [0xB0CA - rangeMin] = 0x52D5;
            mapDataStd [rangeNo] [0xB0CB - rangeMin] = 0x5310;
            mapDataStd [rangeNo] [0xB0CC - rangeMin] = 0x530F;
            mapDataStd [rangeNo] [0xB0CD - rangeMin] = 0x5319;
            mapDataStd [rangeNo] [0xB0CE - rangeMin] = 0x533F;
            mapDataStd [rangeNo] [0xB0CF - rangeMin] = 0x5340;

            mapDataStd [rangeNo] [0xB0D0 - rangeMin] = 0x533E;
            mapDataStd [rangeNo] [0xB0D1 - rangeMin] = 0x53C3;
            mapDataStd [rangeNo] [0xB0D2 - rangeMin] = 0x66FC;
            mapDataStd [rangeNo] [0xB0D3 - rangeMin] = 0x5546;
            mapDataStd [rangeNo] [0xB0D4 - rangeMin] = 0x556A;
            mapDataStd [rangeNo] [0xB0D5 - rangeMin] = 0x5566;
            mapDataStd [rangeNo] [0xB0D6 - rangeMin] = 0x5544;
            mapDataStd [rangeNo] [0xB0D7 - rangeMin] = 0x555E;
            mapDataStd [rangeNo] [0xB0D8 - rangeMin] = 0x5561;
            mapDataStd [rangeNo] [0xB0D9 - rangeMin] = 0x5543;
            mapDataStd [rangeNo] [0xB0DA - rangeMin] = 0x554A;
            mapDataStd [rangeNo] [0xB0DB - rangeMin] = 0x5531;
            mapDataStd [rangeNo] [0xB0DC - rangeMin] = 0x5556;
            mapDataStd [rangeNo] [0xB0DD - rangeMin] = 0x554F;
            mapDataStd [rangeNo] [0xB0DE - rangeMin] = 0x5555;
            mapDataStd [rangeNo] [0xB0DF - rangeMin] = 0x552F;

            mapDataStd [rangeNo] [0xB0E0 - rangeMin] = 0x5564;
            mapDataStd [rangeNo] [0xB0E1 - rangeMin] = 0x5538;
            mapDataStd [rangeNo] [0xB0E2 - rangeMin] = 0x552E;
            mapDataStd [rangeNo] [0xB0E3 - rangeMin] = 0x555C;
            mapDataStd [rangeNo] [0xB0E4 - rangeMin] = 0x552C;
            mapDataStd [rangeNo] [0xB0E5 - rangeMin] = 0x5563;
            mapDataStd [rangeNo] [0xB0E6 - rangeMin] = 0x5533;
            mapDataStd [rangeNo] [0xB0E7 - rangeMin] = 0x5541;
            mapDataStd [rangeNo] [0xB0E8 - rangeMin] = 0x5557;
            mapDataStd [rangeNo] [0xB0E9 - rangeMin] = 0x5708;
            mapDataStd [rangeNo] [0xB0EA - rangeMin] = 0x570B;
            mapDataStd [rangeNo] [0xB0EB - rangeMin] = 0x5709;
            mapDataStd [rangeNo] [0xB0EC - rangeMin] = 0x57DF;
            mapDataStd [rangeNo] [0xB0ED - rangeMin] = 0x5805;
            mapDataStd [rangeNo] [0xB0EE - rangeMin] = 0x580A;
            mapDataStd [rangeNo] [0xB0EF - rangeMin] = 0x5806;

            mapDataStd [rangeNo] [0xB0F0 - rangeMin] = 0x57E0;
            mapDataStd [rangeNo] [0xB0F1 - rangeMin] = 0x57E4;
            mapDataStd [rangeNo] [0xB0F2 - rangeMin] = 0x57FA;
            mapDataStd [rangeNo] [0xB0F3 - rangeMin] = 0x5802;
            mapDataStd [rangeNo] [0xB0F4 - rangeMin] = 0x5835;
            mapDataStd [rangeNo] [0xB0F5 - rangeMin] = 0x57F7;
            mapDataStd [rangeNo] [0xB0F6 - rangeMin] = 0x57F9;
            mapDataStd [rangeNo] [0xB0F7 - rangeMin] = 0x5920;
            mapDataStd [rangeNo] [0xB0F8 - rangeMin] = 0x5962;
            mapDataStd [rangeNo] [0xB0F9 - rangeMin] = 0x5A36;
            mapDataStd [rangeNo] [0xB0FA - rangeMin] = 0x5A41;
            mapDataStd [rangeNo] [0xB0FB - rangeMin] = 0x5A49;
            mapDataStd [rangeNo] [0xB0FC - rangeMin] = 0x5A66;
            mapDataStd [rangeNo] [0xB0FD - rangeMin] = 0x5A6A;
            mapDataStd [rangeNo] [0xB0FE - rangeMin] = 0x5A40;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xB140 - rangeMin] = 0x5A3C;
            mapDataStd [rangeNo] [0xB141 - rangeMin] = 0x5A62;
            mapDataStd [rangeNo] [0xB142 - rangeMin] = 0x5A5A;
            mapDataStd [rangeNo] [0xB143 - rangeMin] = 0x5A46;
            mapDataStd [rangeNo] [0xB144 - rangeMin] = 0x5A4A;
            mapDataStd [rangeNo] [0xB145 - rangeMin] = 0x5B70;
            mapDataStd [rangeNo] [0xB146 - rangeMin] = 0x5BC7;
            mapDataStd [rangeNo] [0xB147 - rangeMin] = 0x5BC5;
            mapDataStd [rangeNo] [0xB148 - rangeMin] = 0x5BC4;
            mapDataStd [rangeNo] [0xB149 - rangeMin] = 0x5BC2;
            mapDataStd [rangeNo] [0xB14A - rangeMin] = 0x5BBF;
            mapDataStd [rangeNo] [0xB14B - rangeMin] = 0x5BC6;
            mapDataStd [rangeNo] [0xB14C - rangeMin] = 0x5C09;
            mapDataStd [rangeNo] [0xB14D - rangeMin] = 0x5C08;
            mapDataStd [rangeNo] [0xB14E - rangeMin] = 0x5C07;
            mapDataStd [rangeNo] [0xB14F - rangeMin] = 0x5C60;

            mapDataStd [rangeNo] [0xB150 - rangeMin] = 0x5C5C;
            mapDataStd [rangeNo] [0xB151 - rangeMin] = 0x5C5D;
            mapDataStd [rangeNo] [0xB152 - rangeMin] = 0x5D07;
            mapDataStd [rangeNo] [0xB153 - rangeMin] = 0x5D06;
            mapDataStd [rangeNo] [0xB154 - rangeMin] = 0x5D0E;
            mapDataStd [rangeNo] [0xB155 - rangeMin] = 0x5D1B;
            mapDataStd [rangeNo] [0xB156 - rangeMin] = 0x5D16;
            mapDataStd [rangeNo] [0xB157 - rangeMin] = 0x5D22;
            mapDataStd [rangeNo] [0xB158 - rangeMin] = 0x5D11;
            mapDataStd [rangeNo] [0xB159 - rangeMin] = 0x5D29;
            mapDataStd [rangeNo] [0xB15A - rangeMin] = 0x5D14;
            mapDataStd [rangeNo] [0xB15B - rangeMin] = 0x5D19;
            mapDataStd [rangeNo] [0xB15C - rangeMin] = 0x5D24;
            mapDataStd [rangeNo] [0xB15D - rangeMin] = 0x5D27;
            mapDataStd [rangeNo] [0xB15E - rangeMin] = 0x5D17;
            mapDataStd [rangeNo] [0xB15F - rangeMin] = 0x5DE2;

            mapDataStd [rangeNo] [0xB160 - rangeMin] = 0x5E38;
            mapDataStd [rangeNo] [0xB161 - rangeMin] = 0x5E36;
            mapDataStd [rangeNo] [0xB162 - rangeMin] = 0x5E33;
            mapDataStd [rangeNo] [0xB163 - rangeMin] = 0x5E37;
            mapDataStd [rangeNo] [0xB164 - rangeMin] = 0x5EB7;
            mapDataStd [rangeNo] [0xB165 - rangeMin] = 0x5EB8;
            mapDataStd [rangeNo] [0xB166 - rangeMin] = 0x5EB6;
            mapDataStd [rangeNo] [0xB167 - rangeMin] = 0x5EB5;
            mapDataStd [rangeNo] [0xB168 - rangeMin] = 0x5EBE;
            mapDataStd [rangeNo] [0xB169 - rangeMin] = 0x5F35;
            mapDataStd [rangeNo] [0xB16A - rangeMin] = 0x5F37;
            mapDataStd [rangeNo] [0xB16B - rangeMin] = 0x5F57;
            mapDataStd [rangeNo] [0xB16C - rangeMin] = 0x5F6C;
            mapDataStd [rangeNo] [0xB16D - rangeMin] = 0x5F69;
            mapDataStd [rangeNo] [0xB16E - rangeMin] = 0x5F6B;
            mapDataStd [rangeNo] [0xB16F - rangeMin] = 0x5F97;

            mapDataStd [rangeNo] [0xB170 - rangeMin] = 0x5F99;
            mapDataStd [rangeNo] [0xB171 - rangeMin] = 0x5F9E;
            mapDataStd [rangeNo] [0xB172 - rangeMin] = 0x5F98;
            mapDataStd [rangeNo] [0xB173 - rangeMin] = 0x5FA1;
            mapDataStd [rangeNo] [0xB174 - rangeMin] = 0x5FA0;
            mapDataStd [rangeNo] [0xB175 - rangeMin] = 0x5F9C;
            mapDataStd [rangeNo] [0xB176 - rangeMin] = 0x607F;
            mapDataStd [rangeNo] [0xB177 - rangeMin] = 0x60A3;
            mapDataStd [rangeNo] [0xB178 - rangeMin] = 0x6089;
            mapDataStd [rangeNo] [0xB179 - rangeMin] = 0x60A0;
            mapDataStd [rangeNo] [0xB17A - rangeMin] = 0x60A8;
            mapDataStd [rangeNo] [0xB17B - rangeMin] = 0x60CB;
            mapDataStd [rangeNo] [0xB17C - rangeMin] = 0x60B4;
            mapDataStd [rangeNo] [0xB17D - rangeMin] = 0x60E6;
            mapDataStd [rangeNo] [0xB17E - rangeMin] = 0x60BD;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xB1A1 - rangeMin] = 0x60C5;
            mapDataStd [rangeNo] [0xB1A2 - rangeMin] = 0x60BB;
            mapDataStd [rangeNo] [0xB1A3 - rangeMin] = 0x60B5;
            mapDataStd [rangeNo] [0xB1A4 - rangeMin] = 0x60DC;
            mapDataStd [rangeNo] [0xB1A5 - rangeMin] = 0x60BC;
            mapDataStd [rangeNo] [0xB1A6 - rangeMin] = 0x60D8;
            mapDataStd [rangeNo] [0xB1A7 - rangeMin] = 0x60D5;
            mapDataStd [rangeNo] [0xB1A8 - rangeMin] = 0x60C6;
            mapDataStd [rangeNo] [0xB1A9 - rangeMin] = 0x60DF;
            mapDataStd [rangeNo] [0xB1AA - rangeMin] = 0x60B8;
            mapDataStd [rangeNo] [0xB1AB - rangeMin] = 0x60DA;
            mapDataStd [rangeNo] [0xB1AC - rangeMin] = 0x60C7;
            mapDataStd [rangeNo] [0xB1AD - rangeMin] = 0x621A;
            mapDataStd [rangeNo] [0xB1AE - rangeMin] = 0x621B;
            mapDataStd [rangeNo] [0xB1AF - rangeMin] = 0x6248;

            mapDataStd [rangeNo] [0xB1B0 - rangeMin] = 0x63A0;
            mapDataStd [rangeNo] [0xB1B1 - rangeMin] = 0x63A7;
            mapDataStd [rangeNo] [0xB1B2 - rangeMin] = 0x6372;
            mapDataStd [rangeNo] [0xB1B3 - rangeMin] = 0x6396;
            mapDataStd [rangeNo] [0xB1B4 - rangeMin] = 0x63A2;
            mapDataStd [rangeNo] [0xB1B5 - rangeMin] = 0x63A5;
            mapDataStd [rangeNo] [0xB1B6 - rangeMin] = 0x6377;
            mapDataStd [rangeNo] [0xB1B7 - rangeMin] = 0x6367;
            mapDataStd [rangeNo] [0xB1B8 - rangeMin] = 0x6398;
            mapDataStd [rangeNo] [0xB1B9 - rangeMin] = 0x63AA;
            mapDataStd [rangeNo] [0xB1BA - rangeMin] = 0x6371;
            mapDataStd [rangeNo] [0xB1BB - rangeMin] = 0x63A9;
            mapDataStd [rangeNo] [0xB1BC - rangeMin] = 0x6389;
            mapDataStd [rangeNo] [0xB1BD - rangeMin] = 0x6383;
            mapDataStd [rangeNo] [0xB1BE - rangeMin] = 0x639B;
            mapDataStd [rangeNo] [0xB1BF - rangeMin] = 0x636B;

            mapDataStd [rangeNo] [0xB1C0 - rangeMin] = 0x63A8;
            mapDataStd [rangeNo] [0xB1C1 - rangeMin] = 0x6384;
            mapDataStd [rangeNo] [0xB1C2 - rangeMin] = 0x6388;
            mapDataStd [rangeNo] [0xB1C3 - rangeMin] = 0x6399;
            mapDataStd [rangeNo] [0xB1C4 - rangeMin] = 0x63A1;
            mapDataStd [rangeNo] [0xB1C5 - rangeMin] = 0x63AC;
            mapDataStd [rangeNo] [0xB1C6 - rangeMin] = 0x6392;
            mapDataStd [rangeNo] [0xB1C7 - rangeMin] = 0x638F;
            mapDataStd [rangeNo] [0xB1C8 - rangeMin] = 0x6380;
            mapDataStd [rangeNo] [0xB1C9 - rangeMin] = 0x637B;
            mapDataStd [rangeNo] [0xB1CA - rangeMin] = 0x6369;
            mapDataStd [rangeNo] [0xB1CB - rangeMin] = 0x6368;
            mapDataStd [rangeNo] [0xB1CC - rangeMin] = 0x637A;
            mapDataStd [rangeNo] [0xB1CD - rangeMin] = 0x655D;
            mapDataStd [rangeNo] [0xB1CE - rangeMin] = 0x6556;
            mapDataStd [rangeNo] [0xB1CF - rangeMin] = 0x6551;

            mapDataStd [rangeNo] [0xB1D0 - rangeMin] = 0x6559;
            mapDataStd [rangeNo] [0xB1D1 - rangeMin] = 0x6557;
            mapDataStd [rangeNo] [0xB1D2 - rangeMin] = 0x555F;
            mapDataStd [rangeNo] [0xB1D3 - rangeMin] = 0x654F;
            mapDataStd [rangeNo] [0xB1D4 - rangeMin] = 0x6558;
            mapDataStd [rangeNo] [0xB1D5 - rangeMin] = 0x6555;
            mapDataStd [rangeNo] [0xB1D6 - rangeMin] = 0x6554;
            mapDataStd [rangeNo] [0xB1D7 - rangeMin] = 0x659C;
            mapDataStd [rangeNo] [0xB1D8 - rangeMin] = 0x659B;
            mapDataStd [rangeNo] [0xB1D9 - rangeMin] = 0x65AC;
            mapDataStd [rangeNo] [0xB1DA - rangeMin] = 0x65CF;
            mapDataStd [rangeNo] [0xB1DB - rangeMin] = 0x65CB;
            mapDataStd [rangeNo] [0xB1DC - rangeMin] = 0x65CC;
            mapDataStd [rangeNo] [0xB1DD - rangeMin] = 0x65CE;
            mapDataStd [rangeNo] [0xB1DE - rangeMin] = 0x665D;
            mapDataStd [rangeNo] [0xB1DF - rangeMin] = 0x665A;

            mapDataStd [rangeNo] [0xB1E0 - rangeMin] = 0x6664;
            mapDataStd [rangeNo] [0xB1E1 - rangeMin] = 0x6668;
            mapDataStd [rangeNo] [0xB1E2 - rangeMin] = 0x6666;
            mapDataStd [rangeNo] [0xB1E3 - rangeMin] = 0x665E;
            mapDataStd [rangeNo] [0xB1E4 - rangeMin] = 0x66F9;
            mapDataStd [rangeNo] [0xB1E5 - rangeMin] = 0x52D7;
            mapDataStd [rangeNo] [0xB1E6 - rangeMin] = 0x671B;
            mapDataStd [rangeNo] [0xB1E7 - rangeMin] = 0x6881;
            mapDataStd [rangeNo] [0xB1E8 - rangeMin] = 0x68AF;
            mapDataStd [rangeNo] [0xB1E9 - rangeMin] = 0x68A2;
            mapDataStd [rangeNo] [0xB1EA - rangeMin] = 0x6893;
            mapDataStd [rangeNo] [0xB1EB - rangeMin] = 0x68B5;
            mapDataStd [rangeNo] [0xB1EC - rangeMin] = 0x687F;
            mapDataStd [rangeNo] [0xB1ED - rangeMin] = 0x6876;
            mapDataStd [rangeNo] [0xB1EE - rangeMin] = 0x68B1;
            mapDataStd [rangeNo] [0xB1EF - rangeMin] = 0x68A7;

            mapDataStd [rangeNo] [0xB1F0 - rangeMin] = 0x6897;
            mapDataStd [rangeNo] [0xB1F1 - rangeMin] = 0x68B0;
            mapDataStd [rangeNo] [0xB1F2 - rangeMin] = 0x6883;
            mapDataStd [rangeNo] [0xB1F3 - rangeMin] = 0x68C4;
            mapDataStd [rangeNo] [0xB1F4 - rangeMin] = 0x68AD;
            mapDataStd [rangeNo] [0xB1F5 - rangeMin] = 0x6886;
            mapDataStd [rangeNo] [0xB1F6 - rangeMin] = 0x6885;
            mapDataStd [rangeNo] [0xB1F7 - rangeMin] = 0x6894;
            mapDataStd [rangeNo] [0xB1F8 - rangeMin] = 0x689D;
            mapDataStd [rangeNo] [0xB1F9 - rangeMin] = 0x68A8;
            mapDataStd [rangeNo] [0xB1FA - rangeMin] = 0x689F;
            mapDataStd [rangeNo] [0xB1FB - rangeMin] = 0x68A1;
            mapDataStd [rangeNo] [0xB1FC - rangeMin] = 0x6882;
            mapDataStd [rangeNo] [0xB1FD - rangeMin] = 0x6B32;
            mapDataStd [rangeNo] [0xB1FE - rangeMin] = 0x6BBA;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xB240 - rangeMin] = 0x6BEB;
            mapDataStd [rangeNo] [0xB241 - rangeMin] = 0x6BEC;
            mapDataStd [rangeNo] [0xB242 - rangeMin] = 0x6C2B;
            mapDataStd [rangeNo] [0xB243 - rangeMin] = 0x6D8E;
            mapDataStd [rangeNo] [0xB244 - rangeMin] = 0x6DBC;
            mapDataStd [rangeNo] [0xB245 - rangeMin] = 0x6DF3;
            mapDataStd [rangeNo] [0xB246 - rangeMin] = 0x6DD9;
            mapDataStd [rangeNo] [0xB247 - rangeMin] = 0x6DB2;
            mapDataStd [rangeNo] [0xB248 - rangeMin] = 0x6DE1;
            mapDataStd [rangeNo] [0xB249 - rangeMin] = 0x6DCC;
            mapDataStd [rangeNo] [0xB24A - rangeMin] = 0x6DE4;
            mapDataStd [rangeNo] [0xB24B - rangeMin] = 0x6DFB;
            mapDataStd [rangeNo] [0xB24C - rangeMin] = 0x6DFA;
            mapDataStd [rangeNo] [0xB24D - rangeMin] = 0x6E05;
            mapDataStd [rangeNo] [0xB24E - rangeMin] = 0x6DC7;
            mapDataStd [rangeNo] [0xB24F - rangeMin] = 0x6DCB;

            mapDataStd [rangeNo] [0xB250 - rangeMin] = 0x6DAF;
            mapDataStd [rangeNo] [0xB251 - rangeMin] = 0x6DD1;
            mapDataStd [rangeNo] [0xB252 - rangeMin] = 0x6DAE;
            mapDataStd [rangeNo] [0xB253 - rangeMin] = 0x6DDE;
            mapDataStd [rangeNo] [0xB254 - rangeMin] = 0x6DF9;
            mapDataStd [rangeNo] [0xB255 - rangeMin] = 0x6DB8;
            mapDataStd [rangeNo] [0xB256 - rangeMin] = 0x6DF7;
            mapDataStd [rangeNo] [0xB257 - rangeMin] = 0x6DF5;
            mapDataStd [rangeNo] [0xB258 - rangeMin] = 0x6DC5;
            mapDataStd [rangeNo] [0xB259 - rangeMin] = 0x6DD2;
            mapDataStd [rangeNo] [0xB25A - rangeMin] = 0x6E1A;
            mapDataStd [rangeNo] [0xB25B - rangeMin] = 0x6DB5;
            mapDataStd [rangeNo] [0xB25C - rangeMin] = 0x6DDA;
            mapDataStd [rangeNo] [0xB25D - rangeMin] = 0x6DEB;
            mapDataStd [rangeNo] [0xB25E - rangeMin] = 0x6DD8;
            mapDataStd [rangeNo] [0xB25F - rangeMin] = 0x6DEA;

            mapDataStd [rangeNo] [0xB260 - rangeMin] = 0x6DF1;
            mapDataStd [rangeNo] [0xB261 - rangeMin] = 0x6DEE;
            mapDataStd [rangeNo] [0xB262 - rangeMin] = 0x6DE8;
            mapDataStd [rangeNo] [0xB263 - rangeMin] = 0x6DC6;
            mapDataStd [rangeNo] [0xB264 - rangeMin] = 0x6DC4;
            mapDataStd [rangeNo] [0xB265 - rangeMin] = 0x6DAA;
            mapDataStd [rangeNo] [0xB266 - rangeMin] = 0x6DEC;
            mapDataStd [rangeNo] [0xB267 - rangeMin] = 0x6DBF;
            mapDataStd [rangeNo] [0xB268 - rangeMin] = 0x6DE6;
            mapDataStd [rangeNo] [0xB269 - rangeMin] = 0x70F9;
            mapDataStd [rangeNo] [0xB26A - rangeMin] = 0x7109;
            mapDataStd [rangeNo] [0xB26B - rangeMin] = 0x710A;
            mapDataStd [rangeNo] [0xB26C - rangeMin] = 0x70FD;
            mapDataStd [rangeNo] [0xB26D - rangeMin] = 0x70EF;
            mapDataStd [rangeNo] [0xB26E - rangeMin] = 0x723D;
            mapDataStd [rangeNo] [0xB26F - rangeMin] = 0x727D;

            mapDataStd [rangeNo] [0xB270 - rangeMin] = 0x7281;
            mapDataStd [rangeNo] [0xB271 - rangeMin] = 0x731C;
            mapDataStd [rangeNo] [0xB272 - rangeMin] = 0x731B;
            mapDataStd [rangeNo] [0xB273 - rangeMin] = 0x7316;
            mapDataStd [rangeNo] [0xB274 - rangeMin] = 0x7313;
            mapDataStd [rangeNo] [0xB275 - rangeMin] = 0x7319;
            mapDataStd [rangeNo] [0xB276 - rangeMin] = 0x7387;
            mapDataStd [rangeNo] [0xB277 - rangeMin] = 0x7405;
            mapDataStd [rangeNo] [0xB278 - rangeMin] = 0x740A;
            mapDataStd [rangeNo] [0xB279 - rangeMin] = 0x7403;
            mapDataStd [rangeNo] [0xB27A - rangeMin] = 0x7406;
            mapDataStd [rangeNo] [0xB27B - rangeMin] = 0x73FE;
            mapDataStd [rangeNo] [0xB27C - rangeMin] = 0x740D;
            mapDataStd [rangeNo] [0xB27D - rangeMin] = 0x74E0;
            mapDataStd [rangeNo] [0xB27E - rangeMin] = 0x74F6;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xB2A1 - rangeMin] = 0x74F7;
            mapDataStd [rangeNo] [0xB2A2 - rangeMin] = 0x751C;
            mapDataStd [rangeNo] [0xB2A3 - rangeMin] = 0x7522;
            mapDataStd [rangeNo] [0xB2A4 - rangeMin] = 0x7565;
            mapDataStd [rangeNo] [0xB2A5 - rangeMin] = 0x7566;
            mapDataStd [rangeNo] [0xB2A6 - rangeMin] = 0x7562;
            mapDataStd [rangeNo] [0xB2A7 - rangeMin] = 0x7570;
            mapDataStd [rangeNo] [0xB2A8 - rangeMin] = 0x758F;
            mapDataStd [rangeNo] [0xB2A9 - rangeMin] = 0x75D4;
            mapDataStd [rangeNo] [0xB2AA - rangeMin] = 0x75D5;
            mapDataStd [rangeNo] [0xB2AB - rangeMin] = 0x75B5;
            mapDataStd [rangeNo] [0xB2AC - rangeMin] = 0x75CA;
            mapDataStd [rangeNo] [0xB2AD - rangeMin] = 0x75CD;
            mapDataStd [rangeNo] [0xB2AE - rangeMin] = 0x768E;
            mapDataStd [rangeNo] [0xB2AF - rangeMin] = 0x76D4;

            mapDataStd [rangeNo] [0xB2B0 - rangeMin] = 0x76D2;
            mapDataStd [rangeNo] [0xB2B1 - rangeMin] = 0x76DB;
            mapDataStd [rangeNo] [0xB2B2 - rangeMin] = 0x7737;
            mapDataStd [rangeNo] [0xB2B3 - rangeMin] = 0x773E;
            mapDataStd [rangeNo] [0xB2B4 - rangeMin] = 0x773C;
            mapDataStd [rangeNo] [0xB2B5 - rangeMin] = 0x7736;
            mapDataStd [rangeNo] [0xB2B6 - rangeMin] = 0x7738;
            mapDataStd [rangeNo] [0xB2B7 - rangeMin] = 0x773A;
            mapDataStd [rangeNo] [0xB2B8 - rangeMin] = 0x786B;
            mapDataStd [rangeNo] [0xB2B9 - rangeMin] = 0x7843;
            mapDataStd [rangeNo] [0xB2BA - rangeMin] = 0x784E;
            mapDataStd [rangeNo] [0xB2BB - rangeMin] = 0x7965;
            mapDataStd [rangeNo] [0xB2BC - rangeMin] = 0x7968;
            mapDataStd [rangeNo] [0xB2BD - rangeMin] = 0x796D;
            mapDataStd [rangeNo] [0xB2BE - rangeMin] = 0x79FB;
            mapDataStd [rangeNo] [0xB2BF - rangeMin] = 0x7A92;

            mapDataStd [rangeNo] [0xB2C0 - rangeMin] = 0x7A95;
            mapDataStd [rangeNo] [0xB2C1 - rangeMin] = 0x7B20;
            mapDataStd [rangeNo] [0xB2C2 - rangeMin] = 0x7B28;
            mapDataStd [rangeNo] [0xB2C3 - rangeMin] = 0x7B1B;
            mapDataStd [rangeNo] [0xB2C4 - rangeMin] = 0x7B2C;
            mapDataStd [rangeNo] [0xB2C5 - rangeMin] = 0x7B26;
            mapDataStd [rangeNo] [0xB2C6 - rangeMin] = 0x7B19;
            mapDataStd [rangeNo] [0xB2C7 - rangeMin] = 0x7B1E;
            mapDataStd [rangeNo] [0xB2C8 - rangeMin] = 0x7B2E;
            mapDataStd [rangeNo] [0xB2C9 - rangeMin] = 0x7C92;
            mapDataStd [rangeNo] [0xB2CA - rangeMin] = 0x7C97;
            mapDataStd [rangeNo] [0xB2CB - rangeMin] = 0x7C95;
            mapDataStd [rangeNo] [0xB2CC - rangeMin] = 0x7D46;
            mapDataStd [rangeNo] [0xB2CD - rangeMin] = 0x7D43;
            mapDataStd [rangeNo] [0xB2CE - rangeMin] = 0x7D71;
            mapDataStd [rangeNo] [0xB2CF - rangeMin] = 0x7D2E;

            mapDataStd [rangeNo] [0xB2D0 - rangeMin] = 0x7D39;
            mapDataStd [rangeNo] [0xB2D1 - rangeMin] = 0x7D3C;
            mapDataStd [rangeNo] [0xB2D2 - rangeMin] = 0x7D40;
            mapDataStd [rangeNo] [0xB2D3 - rangeMin] = 0x7D30;
            mapDataStd [rangeNo] [0xB2D4 - rangeMin] = 0x7D33;
            mapDataStd [rangeNo] [0xB2D5 - rangeMin] = 0x7D44;
            mapDataStd [rangeNo] [0xB2D6 - rangeMin] = 0x7D2F;
            mapDataStd [rangeNo] [0xB2D7 - rangeMin] = 0x7D42;
            mapDataStd [rangeNo] [0xB2D8 - rangeMin] = 0x7D32;
            mapDataStd [rangeNo] [0xB2D9 - rangeMin] = 0x7D31;
            mapDataStd [rangeNo] [0xB2DA - rangeMin] = 0x7F3D;
            mapDataStd [rangeNo] [0xB2DB - rangeMin] = 0x7F9E;
            mapDataStd [rangeNo] [0xB2DC - rangeMin] = 0x7F9A;
            mapDataStd [rangeNo] [0xB2DD - rangeMin] = 0x7FCC;
            mapDataStd [rangeNo] [0xB2DE - rangeMin] = 0x7FCE;
            mapDataStd [rangeNo] [0xB2DF - rangeMin] = 0x7FD2;

            mapDataStd [rangeNo] [0xB2E0 - rangeMin] = 0x801C;
            mapDataStd [rangeNo] [0xB2E1 - rangeMin] = 0x804A;
            mapDataStd [rangeNo] [0xB2E2 - rangeMin] = 0x8046;
            mapDataStd [rangeNo] [0xB2E3 - rangeMin] = 0x812F;
            mapDataStd [rangeNo] [0xB2E4 - rangeMin] = 0x8116;
            mapDataStd [rangeNo] [0xB2E5 - rangeMin] = 0x8123;
            mapDataStd [rangeNo] [0xB2E6 - rangeMin] = 0x812B;
            mapDataStd [rangeNo] [0xB2E7 - rangeMin] = 0x8129;
            mapDataStd [rangeNo] [0xB2E8 - rangeMin] = 0x8130;
            mapDataStd [rangeNo] [0xB2E9 - rangeMin] = 0x8124;
            mapDataStd [rangeNo] [0xB2EA - rangeMin] = 0x8202;
            mapDataStd [rangeNo] [0xB2EB - rangeMin] = 0x8235;
            mapDataStd [rangeNo] [0xB2EC - rangeMin] = 0x8237;
            mapDataStd [rangeNo] [0xB2ED - rangeMin] = 0x8236;
            mapDataStd [rangeNo] [0xB2EE - rangeMin] = 0x8239;
            mapDataStd [rangeNo] [0xB2EF - rangeMin] = 0x838E;

            mapDataStd [rangeNo] [0xB2F0 - rangeMin] = 0x839E;
            mapDataStd [rangeNo] [0xB2F1 - rangeMin] = 0x8398;
            mapDataStd [rangeNo] [0xB2F2 - rangeMin] = 0x8378;
            mapDataStd [rangeNo] [0xB2F3 - rangeMin] = 0x83A2;
            mapDataStd [rangeNo] [0xB2F4 - rangeMin] = 0x8396;
            mapDataStd [rangeNo] [0xB2F5 - rangeMin] = 0x83BD;
            mapDataStd [rangeNo] [0xB2F6 - rangeMin] = 0x83AB;
            mapDataStd [rangeNo] [0xB2F7 - rangeMin] = 0x8392;
            mapDataStd [rangeNo] [0xB2F8 - rangeMin] = 0x838A;
            mapDataStd [rangeNo] [0xB2F9 - rangeMin] = 0x8393;
            mapDataStd [rangeNo] [0xB2FA - rangeMin] = 0x8389;
            mapDataStd [rangeNo] [0xB2FB - rangeMin] = 0x83A0;
            mapDataStd [rangeNo] [0xB2FC - rangeMin] = 0x8377;
            mapDataStd [rangeNo] [0xB2FD - rangeMin] = 0x837B;
            mapDataStd [rangeNo] [0xB2FE - rangeMin] = 0x837C;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xB340 - rangeMin] = 0x8386;
            mapDataStd [rangeNo] [0xB341 - rangeMin] = 0x83A7;
            mapDataStd [rangeNo] [0xB342 - rangeMin] = 0x8655;
            mapDataStd [rangeNo] [0xB343 - rangeMin] = 0x5F6A;
            mapDataStd [rangeNo] [0xB344 - rangeMin] = 0x86C7;
            mapDataStd [rangeNo] [0xB345 - rangeMin] = 0x86C0;
            mapDataStd [rangeNo] [0xB346 - rangeMin] = 0x86B6;
            mapDataStd [rangeNo] [0xB347 - rangeMin] = 0x86C4;
            mapDataStd [rangeNo] [0xB348 - rangeMin] = 0x86B5;
            mapDataStd [rangeNo] [0xB349 - rangeMin] = 0x86C6;
            mapDataStd [rangeNo] [0xB34A - rangeMin] = 0x86CB;
            mapDataStd [rangeNo] [0xB34B - rangeMin] = 0x86B1;
            mapDataStd [rangeNo] [0xB34C - rangeMin] = 0x86AF;
            mapDataStd [rangeNo] [0xB34D - rangeMin] = 0x86C9;
            mapDataStd [rangeNo] [0xB34E - rangeMin] = 0x8853;
            mapDataStd [rangeNo] [0xB34F - rangeMin] = 0x889E;

            mapDataStd [rangeNo] [0xB350 - rangeMin] = 0x8888;
            mapDataStd [rangeNo] [0xB351 - rangeMin] = 0x88AB;
            mapDataStd [rangeNo] [0xB352 - rangeMin] = 0x8892;
            mapDataStd [rangeNo] [0xB353 - rangeMin] = 0x8896;
            mapDataStd [rangeNo] [0xB354 - rangeMin] = 0x888D;
            mapDataStd [rangeNo] [0xB355 - rangeMin] = 0x888B;
            mapDataStd [rangeNo] [0xB356 - rangeMin] = 0x8993;
            mapDataStd [rangeNo] [0xB357 - rangeMin] = 0x898F;
            mapDataStd [rangeNo] [0xB358 - rangeMin] = 0x8A2A;
            mapDataStd [rangeNo] [0xB359 - rangeMin] = 0x8A1D;
            mapDataStd [rangeNo] [0xB35A - rangeMin] = 0x8A23;
            mapDataStd [rangeNo] [0xB35B - rangeMin] = 0x8A25;
            mapDataStd [rangeNo] [0xB35C - rangeMin] = 0x8A31;
            mapDataStd [rangeNo] [0xB35D - rangeMin] = 0x8A2D;
            mapDataStd [rangeNo] [0xB35E - rangeMin] = 0x8A1F;
            mapDataStd [rangeNo] [0xB35F - rangeMin] = 0x8A1B;

            mapDataStd [rangeNo] [0xB360 - rangeMin] = 0x8A22;
            mapDataStd [rangeNo] [0xB361 - rangeMin] = 0x8C49;
            mapDataStd [rangeNo] [0xB362 - rangeMin] = 0x8C5A;
            mapDataStd [rangeNo] [0xB363 - rangeMin] = 0x8CA9;
            mapDataStd [rangeNo] [0xB364 - rangeMin] = 0x8CAC;
            mapDataStd [rangeNo] [0xB365 - rangeMin] = 0x8CAB;
            mapDataStd [rangeNo] [0xB366 - rangeMin] = 0x8CA8;
            mapDataStd [rangeNo] [0xB367 - rangeMin] = 0x8CAA;
            mapDataStd [rangeNo] [0xB368 - rangeMin] = 0x8CA7;
            mapDataStd [rangeNo] [0xB369 - rangeMin] = 0x8D67;
            mapDataStd [rangeNo] [0xB36A - rangeMin] = 0x8D66;
            mapDataStd [rangeNo] [0xB36B - rangeMin] = 0x8DBE;
            mapDataStd [rangeNo] [0xB36C - rangeMin] = 0x8DBA;
            mapDataStd [rangeNo] [0xB36D - rangeMin] = 0x8EDB;
            mapDataStd [rangeNo] [0xB36E - rangeMin] = 0x8EDF;
            mapDataStd [rangeNo] [0xB36F - rangeMin] = 0x9019;

            mapDataStd [rangeNo] [0xB370 - rangeMin] = 0x900D;
            mapDataStd [rangeNo] [0xB371 - rangeMin] = 0x901A;
            mapDataStd [rangeNo] [0xB372 - rangeMin] = 0x9017;
            mapDataStd [rangeNo] [0xB373 - rangeMin] = 0x9023;
            mapDataStd [rangeNo] [0xB374 - rangeMin] = 0x901F;
            mapDataStd [rangeNo] [0xB375 - rangeMin] = 0x901D;
            mapDataStd [rangeNo] [0xB376 - rangeMin] = 0x9010;
            mapDataStd [rangeNo] [0xB377 - rangeMin] = 0x9015;
            mapDataStd [rangeNo] [0xB378 - rangeMin] = 0x901E;
            mapDataStd [rangeNo] [0xB379 - rangeMin] = 0x9020;
            mapDataStd [rangeNo] [0xB37A - rangeMin] = 0x900F;
            mapDataStd [rangeNo] [0xB37B - rangeMin] = 0x9022;
            mapDataStd [rangeNo] [0xB37C - rangeMin] = 0x9016;
            mapDataStd [rangeNo] [0xB37D - rangeMin] = 0x901B;
            mapDataStd [rangeNo] [0xB37E - rangeMin] = 0x9014;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xB3A1 - rangeMin] = 0x90E8;
            mapDataStd [rangeNo] [0xB3A2 - rangeMin] = 0x90ED;
            mapDataStd [rangeNo] [0xB3A3 - rangeMin] = 0x90FD;
            mapDataStd [rangeNo] [0xB3A4 - rangeMin] = 0x9157;
            mapDataStd [rangeNo] [0xB3A5 - rangeMin] = 0x91CE;
            mapDataStd [rangeNo] [0xB3A6 - rangeMin] = 0x91F5;
            mapDataStd [rangeNo] [0xB3A7 - rangeMin] = 0x91E6;
            mapDataStd [rangeNo] [0xB3A8 - rangeMin] = 0x91E3;
            mapDataStd [rangeNo] [0xB3A9 - rangeMin] = 0x91E7;
            mapDataStd [rangeNo] [0xB3AA - rangeMin] = 0x91ED;
            mapDataStd [rangeNo] [0xB3AB - rangeMin] = 0x91E9;
            mapDataStd [rangeNo] [0xB3AC - rangeMin] = 0x9589;
            mapDataStd [rangeNo] [0xB3AD - rangeMin] = 0x966A;
            mapDataStd [rangeNo] [0xB3AE - rangeMin] = 0x9675;
            mapDataStd [rangeNo] [0xB3AF - rangeMin] = 0x9673;

            mapDataStd [rangeNo] [0xB3B0 - rangeMin] = 0x9678;
            mapDataStd [rangeNo] [0xB3B1 - rangeMin] = 0x9670;
            mapDataStd [rangeNo] [0xB3B2 - rangeMin] = 0x9674;
            mapDataStd [rangeNo] [0xB3B3 - rangeMin] = 0x9676;
            mapDataStd [rangeNo] [0xB3B4 - rangeMin] = 0x9677;
            mapDataStd [rangeNo] [0xB3B5 - rangeMin] = 0x966C;
            mapDataStd [rangeNo] [0xB3B6 - rangeMin] = 0x96C0;
            mapDataStd [rangeNo] [0xB3B7 - rangeMin] = 0x96EA;
            mapDataStd [rangeNo] [0xB3B8 - rangeMin] = 0x96E9;
            mapDataStd [rangeNo] [0xB3B9 - rangeMin] = 0x7AE0;
            mapDataStd [rangeNo] [0xB3BA - rangeMin] = 0x7ADF;
            mapDataStd [rangeNo] [0xB3BB - rangeMin] = 0x9802;
            mapDataStd [rangeNo] [0xB3BC - rangeMin] = 0x9803;
            mapDataStd [rangeNo] [0xB3BD - rangeMin] = 0x9B5A;
            mapDataStd [rangeNo] [0xB3BE - rangeMin] = 0x9CE5;
            mapDataStd [rangeNo] [0xB3BF - rangeMin] = 0x9E75;

            mapDataStd [rangeNo] [0xB3C0 - rangeMin] = 0x9E7F;
            mapDataStd [rangeNo] [0xB3C1 - rangeMin] = 0x9EA5;
            mapDataStd [rangeNo] [0xB3C2 - rangeMin] = 0x9EBB;
            mapDataStd [rangeNo] [0xB3C3 - rangeMin] = 0x50A2;
            mapDataStd [rangeNo] [0xB3C4 - rangeMin] = 0x508D;
            mapDataStd [rangeNo] [0xB3C5 - rangeMin] = 0x5085;
            mapDataStd [rangeNo] [0xB3C6 - rangeMin] = 0x5099;
            mapDataStd [rangeNo] [0xB3C7 - rangeMin] = 0x5091;
            mapDataStd [rangeNo] [0xB3C8 - rangeMin] = 0x5080;
            mapDataStd [rangeNo] [0xB3C9 - rangeMin] = 0x5096;
            mapDataStd [rangeNo] [0xB3CA - rangeMin] = 0x5098;
            mapDataStd [rangeNo] [0xB3CB - rangeMin] = 0x509A;
            mapDataStd [rangeNo] [0xB3CC - rangeMin] = 0x6700;
            mapDataStd [rangeNo] [0xB3CD - rangeMin] = 0x51F1;
            mapDataStd [rangeNo] [0xB3CE - rangeMin] = 0x5272;
            mapDataStd [rangeNo] [0xB3CF - rangeMin] = 0x5274;

            mapDataStd [rangeNo] [0xB3D0 - rangeMin] = 0x5275;
            mapDataStd [rangeNo] [0xB3D1 - rangeMin] = 0x5269;
            mapDataStd [rangeNo] [0xB3D2 - rangeMin] = 0x52DE;
            mapDataStd [rangeNo] [0xB3D3 - rangeMin] = 0x52DD;
            mapDataStd [rangeNo] [0xB3D4 - rangeMin] = 0x52DB;
            mapDataStd [rangeNo] [0xB3D5 - rangeMin] = 0x535A;
            mapDataStd [rangeNo] [0xB3D6 - rangeMin] = 0x53A5;
            mapDataStd [rangeNo] [0xB3D7 - rangeMin] = 0x557B;
            mapDataStd [rangeNo] [0xB3D8 - rangeMin] = 0x5580;
            mapDataStd [rangeNo] [0xB3D9 - rangeMin] = 0x55A7;
            mapDataStd [rangeNo] [0xB3DA - rangeMin] = 0x557C;
            mapDataStd [rangeNo] [0xB3DB - rangeMin] = 0x558A;
            mapDataStd [rangeNo] [0xB3DC - rangeMin] = 0x559D;
            mapDataStd [rangeNo] [0xB3DD - rangeMin] = 0x5598;
            mapDataStd [rangeNo] [0xB3DE - rangeMin] = 0x5582;
            mapDataStd [rangeNo] [0xB3DF - rangeMin] = 0x559C;

            mapDataStd [rangeNo] [0xB3E0 - rangeMin] = 0x55AA;
            mapDataStd [rangeNo] [0xB3E1 - rangeMin] = 0x5594;
            mapDataStd [rangeNo] [0xB3E2 - rangeMin] = 0x5587;
            mapDataStd [rangeNo] [0xB3E3 - rangeMin] = 0x558B;
            mapDataStd [rangeNo] [0xB3E4 - rangeMin] = 0x5583;
            mapDataStd [rangeNo] [0xB3E5 - rangeMin] = 0x55B3;
            mapDataStd [rangeNo] [0xB3E6 - rangeMin] = 0x55AE;
            mapDataStd [rangeNo] [0xB3E7 - rangeMin] = 0x559F;
            mapDataStd [rangeNo] [0xB3E8 - rangeMin] = 0x553E;
            mapDataStd [rangeNo] [0xB3E9 - rangeMin] = 0x55B2;
            mapDataStd [rangeNo] [0xB3EA - rangeMin] = 0x559A;
            mapDataStd [rangeNo] [0xB3EB - rangeMin] = 0x55BB;
            mapDataStd [rangeNo] [0xB3EC - rangeMin] = 0x55AC;
            mapDataStd [rangeNo] [0xB3ED - rangeMin] = 0x55B1;
            mapDataStd [rangeNo] [0xB3EE - rangeMin] = 0x557E;
            mapDataStd [rangeNo] [0xB3EF - rangeMin] = 0x5589;

            mapDataStd [rangeNo] [0xB3F0 - rangeMin] = 0x55AB;
            mapDataStd [rangeNo] [0xB3F1 - rangeMin] = 0x5599;
            mapDataStd [rangeNo] [0xB3F2 - rangeMin] = 0x570D;
            mapDataStd [rangeNo] [0xB3F3 - rangeMin] = 0x582F;
            mapDataStd [rangeNo] [0xB3F4 - rangeMin] = 0x582A;
            mapDataStd [rangeNo] [0xB3F5 - rangeMin] = 0x5834;
            mapDataStd [rangeNo] [0xB3F6 - rangeMin] = 0x5824;
            mapDataStd [rangeNo] [0xB3F7 - rangeMin] = 0x5830;
            mapDataStd [rangeNo] [0xB3F8 - rangeMin] = 0x5831;
            mapDataStd [rangeNo] [0xB3F9 - rangeMin] = 0x5821;
            mapDataStd [rangeNo] [0xB3FA - rangeMin] = 0x581D;
            mapDataStd [rangeNo] [0xB3FB - rangeMin] = 0x5820;
            mapDataStd [rangeNo] [0xB3FC - rangeMin] = 0x58F9;
            mapDataStd [rangeNo] [0xB3FD - rangeMin] = 0x58FA;
            mapDataStd [rangeNo] [0xB3FE - rangeMin] = 0x5960;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xB440 - rangeMin] = 0x5A77;
            mapDataStd [rangeNo] [0xB441 - rangeMin] = 0x5A9A;
            mapDataStd [rangeNo] [0xB442 - rangeMin] = 0x5A7F;
            mapDataStd [rangeNo] [0xB443 - rangeMin] = 0x5A92;
            mapDataStd [rangeNo] [0xB444 - rangeMin] = 0x5A9B;
            mapDataStd [rangeNo] [0xB445 - rangeMin] = 0x5AA7;
            mapDataStd [rangeNo] [0xB446 - rangeMin] = 0x5B73;
            mapDataStd [rangeNo] [0xB447 - rangeMin] = 0x5B71;
            mapDataStd [rangeNo] [0xB448 - rangeMin] = 0x5BD2;
            mapDataStd [rangeNo] [0xB449 - rangeMin] = 0x5BCC;
            mapDataStd [rangeNo] [0xB44A - rangeMin] = 0x5BD3;
            mapDataStd [rangeNo] [0xB44B - rangeMin] = 0x5BD0;
            mapDataStd [rangeNo] [0xB44C - rangeMin] = 0x5C0A;
            mapDataStd [rangeNo] [0xB44D - rangeMin] = 0x5C0B;
            mapDataStd [rangeNo] [0xB44E - rangeMin] = 0x5C31;
            mapDataStd [rangeNo] [0xB44F - rangeMin] = 0x5D4C;

            mapDataStd [rangeNo] [0xB450 - rangeMin] = 0x5D50;
            mapDataStd [rangeNo] [0xB451 - rangeMin] = 0x5D34;
            mapDataStd [rangeNo] [0xB452 - rangeMin] = 0x5D47;
            mapDataStd [rangeNo] [0xB453 - rangeMin] = 0x5DFD;
            mapDataStd [rangeNo] [0xB454 - rangeMin] = 0x5E45;
            mapDataStd [rangeNo] [0xB455 - rangeMin] = 0x5E3D;
            mapDataStd [rangeNo] [0xB456 - rangeMin] = 0x5E40;
            mapDataStd [rangeNo] [0xB457 - rangeMin] = 0x5E43;
            mapDataStd [rangeNo] [0xB458 - rangeMin] = 0x5E7E;
            mapDataStd [rangeNo] [0xB459 - rangeMin] = 0x5ECA;
            mapDataStd [rangeNo] [0xB45A - rangeMin] = 0x5EC1;
            mapDataStd [rangeNo] [0xB45B - rangeMin] = 0x5EC2;
            mapDataStd [rangeNo] [0xB45C - rangeMin] = 0x5EC4;
            mapDataStd [rangeNo] [0xB45D - rangeMin] = 0x5F3C;
            mapDataStd [rangeNo] [0xB45E - rangeMin] = 0x5F6D;
            mapDataStd [rangeNo] [0xB45F - rangeMin] = 0x5FA9;

            mapDataStd [rangeNo] [0xB460 - rangeMin] = 0x5FAA;
            mapDataStd [rangeNo] [0xB461 - rangeMin] = 0x5FA8;
            mapDataStd [rangeNo] [0xB462 - rangeMin] = 0x60D1;
            mapDataStd [rangeNo] [0xB463 - rangeMin] = 0x60E1;
            mapDataStd [rangeNo] [0xB464 - rangeMin] = 0x60B2;
            mapDataStd [rangeNo] [0xB465 - rangeMin] = 0x60B6;
            mapDataStd [rangeNo] [0xB466 - rangeMin] = 0x60E0;
            mapDataStd [rangeNo] [0xB467 - rangeMin] = 0x611C;
            mapDataStd [rangeNo] [0xB468 - rangeMin] = 0x6123;
            mapDataStd [rangeNo] [0xB469 - rangeMin] = 0x60FA;
            mapDataStd [rangeNo] [0xB46A - rangeMin] = 0x6115;
            mapDataStd [rangeNo] [0xB46B - rangeMin] = 0x60F0;
            mapDataStd [rangeNo] [0xB46C - rangeMin] = 0x60FB;
            mapDataStd [rangeNo] [0xB46D - rangeMin] = 0x60F4;
            mapDataStd [rangeNo] [0xB46E - rangeMin] = 0x6168;
            mapDataStd [rangeNo] [0xB46F - rangeMin] = 0x60F1;

            mapDataStd [rangeNo] [0xB470 - rangeMin] = 0x610E;
            mapDataStd [rangeNo] [0xB471 - rangeMin] = 0x60F6;
            mapDataStd [rangeNo] [0xB472 - rangeMin] = 0x6109;
            mapDataStd [rangeNo] [0xB473 - rangeMin] = 0x6100;
            mapDataStd [rangeNo] [0xB474 - rangeMin] = 0x6112;
            mapDataStd [rangeNo] [0xB475 - rangeMin] = 0x621F;
            mapDataStd [rangeNo] [0xB476 - rangeMin] = 0x6249;
            mapDataStd [rangeNo] [0xB477 - rangeMin] = 0x63A3;
            mapDataStd [rangeNo] [0xB478 - rangeMin] = 0x638C;
            mapDataStd [rangeNo] [0xB479 - rangeMin] = 0x63CF;
            mapDataStd [rangeNo] [0xB47A - rangeMin] = 0x63C0;
            mapDataStd [rangeNo] [0xB47B - rangeMin] = 0x63E9;
            mapDataStd [rangeNo] [0xB47C - rangeMin] = 0x63C9;
            mapDataStd [rangeNo] [0xB47D - rangeMin] = 0x63C6;
            mapDataStd [rangeNo] [0xB47E - rangeMin] = 0x63CD;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xB4A1 - rangeMin] = 0x63D2;
            mapDataStd [rangeNo] [0xB4A2 - rangeMin] = 0x63E3;
            mapDataStd [rangeNo] [0xB4A3 - rangeMin] = 0x63D0;
            mapDataStd [rangeNo] [0xB4A4 - rangeMin] = 0x63E1;
            mapDataStd [rangeNo] [0xB4A5 - rangeMin] = 0x63D6;
            mapDataStd [rangeNo] [0xB4A6 - rangeMin] = 0x63ED;
            mapDataStd [rangeNo] [0xB4A7 - rangeMin] = 0x63EE;
            mapDataStd [rangeNo] [0xB4A8 - rangeMin] = 0x6376;
            mapDataStd [rangeNo] [0xB4A9 - rangeMin] = 0x63F4;
            mapDataStd [rangeNo] [0xB4AA - rangeMin] = 0x63EA;
            mapDataStd [rangeNo] [0xB4AB - rangeMin] = 0x63DB;
            mapDataStd [rangeNo] [0xB4AC - rangeMin] = 0x6452;
            mapDataStd [rangeNo] [0xB4AD - rangeMin] = 0x63DA;
            mapDataStd [rangeNo] [0xB4AE - rangeMin] = 0x63F9;
            mapDataStd [rangeNo] [0xB4AF - rangeMin] = 0x655E;

            mapDataStd [rangeNo] [0xB4B0 - rangeMin] = 0x6566;
            mapDataStd [rangeNo] [0xB4B1 - rangeMin] = 0x6562;
            mapDataStd [rangeNo] [0xB4B2 - rangeMin] = 0x6563;
            mapDataStd [rangeNo] [0xB4B3 - rangeMin] = 0x6591;
            mapDataStd [rangeNo] [0xB4B4 - rangeMin] = 0x6590;
            mapDataStd [rangeNo] [0xB4B5 - rangeMin] = 0x65AF;
            mapDataStd [rangeNo] [0xB4B6 - rangeMin] = 0x666E;
            mapDataStd [rangeNo] [0xB4B7 - rangeMin] = 0x6670;
            mapDataStd [rangeNo] [0xB4B8 - rangeMin] = 0x6674;
            mapDataStd [rangeNo] [0xB4B9 - rangeMin] = 0x6676;
            mapDataStd [rangeNo] [0xB4BA - rangeMin] = 0x666F;
            mapDataStd [rangeNo] [0xB4BB - rangeMin] = 0x6691;
            mapDataStd [rangeNo] [0xB4BC - rangeMin] = 0x667A;
            mapDataStd [rangeNo] [0xB4BD - rangeMin] = 0x667E;
            mapDataStd [rangeNo] [0xB4BE - rangeMin] = 0x6677;
            mapDataStd [rangeNo] [0xB4BF - rangeMin] = 0x66FE;

            mapDataStd [rangeNo] [0xB4C0 - rangeMin] = 0x66FF;
            mapDataStd [rangeNo] [0xB4C1 - rangeMin] = 0x671F;
            mapDataStd [rangeNo] [0xB4C2 - rangeMin] = 0x671D;
            mapDataStd [rangeNo] [0xB4C3 - rangeMin] = 0x68FA;
            mapDataStd [rangeNo] [0xB4C4 - rangeMin] = 0x68D5;
            mapDataStd [rangeNo] [0xB4C5 - rangeMin] = 0x68E0;
            mapDataStd [rangeNo] [0xB4C6 - rangeMin] = 0x68D8;
            mapDataStd [rangeNo] [0xB4C7 - rangeMin] = 0x68D7;
            mapDataStd [rangeNo] [0xB4C8 - rangeMin] = 0x6905;
            mapDataStd [rangeNo] [0xB4C9 - rangeMin] = 0x68DF;
            mapDataStd [rangeNo] [0xB4CA - rangeMin] = 0x68F5;
            mapDataStd [rangeNo] [0xB4CB - rangeMin] = 0x68EE;
            mapDataStd [rangeNo] [0xB4CC - rangeMin] = 0x68E7;
            mapDataStd [rangeNo] [0xB4CD - rangeMin] = 0x68F9;
            mapDataStd [rangeNo] [0xB4CE - rangeMin] = 0x68D2;
            mapDataStd [rangeNo] [0xB4CF - rangeMin] = 0x68F2;

            mapDataStd [rangeNo] [0xB4D0 - rangeMin] = 0x68E3;
            mapDataStd [rangeNo] [0xB4D1 - rangeMin] = 0x68CB;
            mapDataStd [rangeNo] [0xB4D2 - rangeMin] = 0x68CD;
            mapDataStd [rangeNo] [0xB4D3 - rangeMin] = 0x690D;
            mapDataStd [rangeNo] [0xB4D4 - rangeMin] = 0x6912;
            mapDataStd [rangeNo] [0xB4D5 - rangeMin] = 0x690E;
            mapDataStd [rangeNo] [0xB4D6 - rangeMin] = 0x68C9;
            mapDataStd [rangeNo] [0xB4D7 - rangeMin] = 0x68DA;
            mapDataStd [rangeNo] [0xB4D8 - rangeMin] = 0x696E;
            mapDataStd [rangeNo] [0xB4D9 - rangeMin] = 0x68FB;
            mapDataStd [rangeNo] [0xB4DA - rangeMin] = 0x6B3E;
            mapDataStd [rangeNo] [0xB4DB - rangeMin] = 0x6B3A;
            mapDataStd [rangeNo] [0xB4DC - rangeMin] = 0x6B3D;
            mapDataStd [rangeNo] [0xB4DD - rangeMin] = 0x6B98;
            mapDataStd [rangeNo] [0xB4DE - rangeMin] = 0x6B96;
            mapDataStd [rangeNo] [0xB4DF - rangeMin] = 0x6BBC;

            mapDataStd [rangeNo] [0xB4E0 - rangeMin] = 0x6BEF;
            mapDataStd [rangeNo] [0xB4E1 - rangeMin] = 0x6C2E;
            mapDataStd [rangeNo] [0xB4E2 - rangeMin] = 0x6C2F;
            mapDataStd [rangeNo] [0xB4E3 - rangeMin] = 0x6C2C;
            mapDataStd [rangeNo] [0xB4E4 - rangeMin] = 0x6E2F;
            mapDataStd [rangeNo] [0xB4E5 - rangeMin] = 0x6E38;
            mapDataStd [rangeNo] [0xB4E6 - rangeMin] = 0x6E54;
            mapDataStd [rangeNo] [0xB4E7 - rangeMin] = 0x6E21;
            mapDataStd [rangeNo] [0xB4E8 - rangeMin] = 0x6E32;
            mapDataStd [rangeNo] [0xB4E9 - rangeMin] = 0x6E67;
            mapDataStd [rangeNo] [0xB4EA - rangeMin] = 0x6E4A;
            mapDataStd [rangeNo] [0xB4EB - rangeMin] = 0x6E20;
            mapDataStd [rangeNo] [0xB4EC - rangeMin] = 0x6E25;
            mapDataStd [rangeNo] [0xB4ED - rangeMin] = 0x6E23;
            mapDataStd [rangeNo] [0xB4EE - rangeMin] = 0x6E1B;
            mapDataStd [rangeNo] [0xB4EF - rangeMin] = 0x6E5B;

            mapDataStd [rangeNo] [0xB4F0 - rangeMin] = 0x6E58;
            mapDataStd [rangeNo] [0xB4F1 - rangeMin] = 0x6E24;
            mapDataStd [rangeNo] [0xB4F2 - rangeMin] = 0x6E56;
            mapDataStd [rangeNo] [0xB4F3 - rangeMin] = 0x6E6E;
            mapDataStd [rangeNo] [0xB4F4 - rangeMin] = 0x6E2D;
            mapDataStd [rangeNo] [0xB4F5 - rangeMin] = 0x6E26;
            mapDataStd [rangeNo] [0xB4F6 - rangeMin] = 0x6E6F;
            mapDataStd [rangeNo] [0xB4F7 - rangeMin] = 0x6E34;
            mapDataStd [rangeNo] [0xB4F8 - rangeMin] = 0x6E4D;
            mapDataStd [rangeNo] [0xB4F9 - rangeMin] = 0x6E3A;
            mapDataStd [rangeNo] [0xB4FA - rangeMin] = 0x6E2C;
            mapDataStd [rangeNo] [0xB4FB - rangeMin] = 0x6E43;
            mapDataStd [rangeNo] [0xB4FC - rangeMin] = 0x6E1D;
            mapDataStd [rangeNo] [0xB4FD - rangeMin] = 0x6E3E;
            mapDataStd [rangeNo] [0xB4FE - rangeMin] = 0x6ECB;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xB540 - rangeMin] = 0x6E89;
            mapDataStd [rangeNo] [0xB541 - rangeMin] = 0x6E19;
            mapDataStd [rangeNo] [0xB542 - rangeMin] = 0x6E4E;
            mapDataStd [rangeNo] [0xB543 - rangeMin] = 0x6E63;
            mapDataStd [rangeNo] [0xB544 - rangeMin] = 0x6E44;
            mapDataStd [rangeNo] [0xB545 - rangeMin] = 0x6E72;
            mapDataStd [rangeNo] [0xB546 - rangeMin] = 0x6E69;
            mapDataStd [rangeNo] [0xB547 - rangeMin] = 0x6E5F;
            mapDataStd [rangeNo] [0xB548 - rangeMin] = 0x7119;
            mapDataStd [rangeNo] [0xB549 - rangeMin] = 0x711A;
            mapDataStd [rangeNo] [0xB54A - rangeMin] = 0x7126;
            mapDataStd [rangeNo] [0xB54B - rangeMin] = 0x7130;
            mapDataStd [rangeNo] [0xB54C - rangeMin] = 0x7121;
            mapDataStd [rangeNo] [0xB54D - rangeMin] = 0x7136;
            mapDataStd [rangeNo] [0xB54E - rangeMin] = 0x716E;
            mapDataStd [rangeNo] [0xB54F - rangeMin] = 0x711C;

            mapDataStd [rangeNo] [0xB550 - rangeMin] = 0x724C;
            mapDataStd [rangeNo] [0xB551 - rangeMin] = 0x7284;
            mapDataStd [rangeNo] [0xB552 - rangeMin] = 0x7280;
            mapDataStd [rangeNo] [0xB553 - rangeMin] = 0x7336;
            mapDataStd [rangeNo] [0xB554 - rangeMin] = 0x7325;
            mapDataStd [rangeNo] [0xB555 - rangeMin] = 0x7334;
            mapDataStd [rangeNo] [0xB556 - rangeMin] = 0x7329;
            mapDataStd [rangeNo] [0xB557 - rangeMin] = 0x743A;
            mapDataStd [rangeNo] [0xB558 - rangeMin] = 0x742A;
            mapDataStd [rangeNo] [0xB559 - rangeMin] = 0x7433;
            mapDataStd [rangeNo] [0xB55A - rangeMin] = 0x7422;
            mapDataStd [rangeNo] [0xB55B - rangeMin] = 0x7425;
            mapDataStd [rangeNo] [0xB55C - rangeMin] = 0x7435;
            mapDataStd [rangeNo] [0xB55D - rangeMin] = 0x7436;
            mapDataStd [rangeNo] [0xB55E - rangeMin] = 0x7434;
            mapDataStd [rangeNo] [0xB55F - rangeMin] = 0x742F;

            mapDataStd [rangeNo] [0xB560 - rangeMin] = 0x741B;
            mapDataStd [rangeNo] [0xB561 - rangeMin] = 0x7426;
            mapDataStd [rangeNo] [0xB562 - rangeMin] = 0x7428;
            mapDataStd [rangeNo] [0xB563 - rangeMin] = 0x7525;
            mapDataStd [rangeNo] [0xB564 - rangeMin] = 0x7526;
            mapDataStd [rangeNo] [0xB565 - rangeMin] = 0x756B;
            mapDataStd [rangeNo] [0xB566 - rangeMin] = 0x756A;
            mapDataStd [rangeNo] [0xB567 - rangeMin] = 0x75E2;
            mapDataStd [rangeNo] [0xB568 - rangeMin] = 0x75DB;
            mapDataStd [rangeNo] [0xB569 - rangeMin] = 0x75E3;
            mapDataStd [rangeNo] [0xB56A - rangeMin] = 0x75D9;
            mapDataStd [rangeNo] [0xB56B - rangeMin] = 0x75D8;
            mapDataStd [rangeNo] [0xB56C - rangeMin] = 0x75DE;
            mapDataStd [rangeNo] [0xB56D - rangeMin] = 0x75E0;
            mapDataStd [rangeNo] [0xB56E - rangeMin] = 0x767B;
            mapDataStd [rangeNo] [0xB56F - rangeMin] = 0x767C;

            mapDataStd [rangeNo] [0xB570 - rangeMin] = 0x7696;
            mapDataStd [rangeNo] [0xB571 - rangeMin] = 0x7693;
            mapDataStd [rangeNo] [0xB572 - rangeMin] = 0x76B4;
            mapDataStd [rangeNo] [0xB573 - rangeMin] = 0x76DC;
            mapDataStd [rangeNo] [0xB574 - rangeMin] = 0x774F;
            mapDataStd [rangeNo] [0xB575 - rangeMin] = 0x77ED;
            mapDataStd [rangeNo] [0xB576 - rangeMin] = 0x785D;
            mapDataStd [rangeNo] [0xB577 - rangeMin] = 0x786C;
            mapDataStd [rangeNo] [0xB578 - rangeMin] = 0x786F;
            mapDataStd [rangeNo] [0xB579 - rangeMin] = 0x7A0D;
            mapDataStd [rangeNo] [0xB57A - rangeMin] = 0x7A08;
            mapDataStd [rangeNo] [0xB57B - rangeMin] = 0x7A0B;
            mapDataStd [rangeNo] [0xB57C - rangeMin] = 0x7A05;
            mapDataStd [rangeNo] [0xB57D - rangeMin] = 0x7A00;
            mapDataStd [rangeNo] [0xB57E - rangeMin] = 0x7A98;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xB5A1 - rangeMin] = 0x7A97;
            mapDataStd [rangeNo] [0xB5A2 - rangeMin] = 0x7A96;
            mapDataStd [rangeNo] [0xB5A3 - rangeMin] = 0x7AE5;
            mapDataStd [rangeNo] [0xB5A4 - rangeMin] = 0x7AE3;
            mapDataStd [rangeNo] [0xB5A5 - rangeMin] = 0x7B49;
            mapDataStd [rangeNo] [0xB5A6 - rangeMin] = 0x7B56;
            mapDataStd [rangeNo] [0xB5A7 - rangeMin] = 0x7B46;
            mapDataStd [rangeNo] [0xB5A8 - rangeMin] = 0x7B50;
            mapDataStd [rangeNo] [0xB5A9 - rangeMin] = 0x7B52;
            mapDataStd [rangeNo] [0xB5AA - rangeMin] = 0x7B54;
            mapDataStd [rangeNo] [0xB5AB - rangeMin] = 0x7B4D;
            mapDataStd [rangeNo] [0xB5AC - rangeMin] = 0x7B4B;
            mapDataStd [rangeNo] [0xB5AD - rangeMin] = 0x7B4F;
            mapDataStd [rangeNo] [0xB5AE - rangeMin] = 0x7B51;
            mapDataStd [rangeNo] [0xB5AF - rangeMin] = 0x7C9F;

            mapDataStd [rangeNo] [0xB5B0 - rangeMin] = 0x7CA5;
            mapDataStd [rangeNo] [0xB5B1 - rangeMin] = 0x7D5E;
            mapDataStd [rangeNo] [0xB5B2 - rangeMin] = 0x7D50;
            mapDataStd [rangeNo] [0xB5B3 - rangeMin] = 0x7D68;
            mapDataStd [rangeNo] [0xB5B4 - rangeMin] = 0x7D55;
            mapDataStd [rangeNo] [0xB5B5 - rangeMin] = 0x7D2B;
            mapDataStd [rangeNo] [0xB5B6 - rangeMin] = 0x7D6E;
            mapDataStd [rangeNo] [0xB5B7 - rangeMin] = 0x7D72;
            mapDataStd [rangeNo] [0xB5B8 - rangeMin] = 0x7D61;
            mapDataStd [rangeNo] [0xB5B9 - rangeMin] = 0x7D66;
            mapDataStd [rangeNo] [0xB5BA - rangeMin] = 0x7D62;
            mapDataStd [rangeNo] [0xB5BB - rangeMin] = 0x7D70;
            mapDataStd [rangeNo] [0xB5BC - rangeMin] = 0x7D73;
            mapDataStd [rangeNo] [0xB5BD - rangeMin] = 0x5584;
            mapDataStd [rangeNo] [0xB5BE - rangeMin] = 0x7FD4;
            mapDataStd [rangeNo] [0xB5BF - rangeMin] = 0x7FD5;

            mapDataStd [rangeNo] [0xB5C0 - rangeMin] = 0x800B;
            mapDataStd [rangeNo] [0xB5C1 - rangeMin] = 0x8052;
            mapDataStd [rangeNo] [0xB5C2 - rangeMin] = 0x8085;
            mapDataStd [rangeNo] [0xB5C3 - rangeMin] = 0x8155;
            mapDataStd [rangeNo] [0xB5C4 - rangeMin] = 0x8154;
            mapDataStd [rangeNo] [0xB5C5 - rangeMin] = 0x814B;
            mapDataStd [rangeNo] [0xB5C6 - rangeMin] = 0x8151;
            mapDataStd [rangeNo] [0xB5C7 - rangeMin] = 0x814E;
            mapDataStd [rangeNo] [0xB5C8 - rangeMin] = 0x8139;
            mapDataStd [rangeNo] [0xB5C9 - rangeMin] = 0x8146;
            mapDataStd [rangeNo] [0xB5CA - rangeMin] = 0x813E;
            mapDataStd [rangeNo] [0xB5CB - rangeMin] = 0x814C;
            mapDataStd [rangeNo] [0xB5CC - rangeMin] = 0x8153;
            mapDataStd [rangeNo] [0xB5CD - rangeMin] = 0x8174;
            mapDataStd [rangeNo] [0xB5CE - rangeMin] = 0x8212;
            mapDataStd [rangeNo] [0xB5CF - rangeMin] = 0x821C;

            mapDataStd [rangeNo] [0xB5D0 - rangeMin] = 0x83E9;
            mapDataStd [rangeNo] [0xB5D1 - rangeMin] = 0x8403;
            mapDataStd [rangeNo] [0xB5D2 - rangeMin] = 0x83F8;
            mapDataStd [rangeNo] [0xB5D3 - rangeMin] = 0x840D;
            mapDataStd [rangeNo] [0xB5D4 - rangeMin] = 0x83E0;
            mapDataStd [rangeNo] [0xB5D5 - rangeMin] = 0x83C5;
            mapDataStd [rangeNo] [0xB5D6 - rangeMin] = 0x840B;
            mapDataStd [rangeNo] [0xB5D7 - rangeMin] = 0x83C1;
            mapDataStd [rangeNo] [0xB5D8 - rangeMin] = 0x83EF;
            mapDataStd [rangeNo] [0xB5D9 - rangeMin] = 0x83F1;
            mapDataStd [rangeNo] [0xB5DA - rangeMin] = 0x83F4;
            mapDataStd [rangeNo] [0xB5DB - rangeMin] = 0x8457;
            mapDataStd [rangeNo] [0xB5DC - rangeMin] = 0x840A;
            mapDataStd [rangeNo] [0xB5DD - rangeMin] = 0x83F0;
            mapDataStd [rangeNo] [0xB5DE - rangeMin] = 0x840C;
            mapDataStd [rangeNo] [0xB5DF - rangeMin] = 0x83CC;

            mapDataStd [rangeNo] [0xB5E0 - rangeMin] = 0x83FD;
            mapDataStd [rangeNo] [0xB5E1 - rangeMin] = 0x83F2;
            mapDataStd [rangeNo] [0xB5E2 - rangeMin] = 0x83CA;
            mapDataStd [rangeNo] [0xB5E3 - rangeMin] = 0x8438;
            mapDataStd [rangeNo] [0xB5E4 - rangeMin] = 0x840E;
            mapDataStd [rangeNo] [0xB5E5 - rangeMin] = 0x8404;
            mapDataStd [rangeNo] [0xB5E6 - rangeMin] = 0x83DC;
            mapDataStd [rangeNo] [0xB5E7 - rangeMin] = 0x8407;
            mapDataStd [rangeNo] [0xB5E8 - rangeMin] = 0x83D4;
            mapDataStd [rangeNo] [0xB5E9 - rangeMin] = 0x83DF;
            mapDataStd [rangeNo] [0xB5EA - rangeMin] = 0x865B;
            mapDataStd [rangeNo] [0xB5EB - rangeMin] = 0x86DF;
            mapDataStd [rangeNo] [0xB5EC - rangeMin] = 0x86D9;
            mapDataStd [rangeNo] [0xB5ED - rangeMin] = 0x86ED;
            mapDataStd [rangeNo] [0xB5EE - rangeMin] = 0x86D4;
            mapDataStd [rangeNo] [0xB5EF - rangeMin] = 0x86DB;

            mapDataStd [rangeNo] [0xB5F0 - rangeMin] = 0x86E4;
            mapDataStd [rangeNo] [0xB5F1 - rangeMin] = 0x86D0;
            mapDataStd [rangeNo] [0xB5F2 - rangeMin] = 0x86DE;
            mapDataStd [rangeNo] [0xB5F3 - rangeMin] = 0x8857;
            mapDataStd [rangeNo] [0xB5F4 - rangeMin] = 0x88C1;
            mapDataStd [rangeNo] [0xB5F5 - rangeMin] = 0x88C2;
            mapDataStd [rangeNo] [0xB5F6 - rangeMin] = 0x88B1;
            mapDataStd [rangeNo] [0xB5F7 - rangeMin] = 0x8983;
            mapDataStd [rangeNo] [0xB5F8 - rangeMin] = 0x8996;
            mapDataStd [rangeNo] [0xB5F9 - rangeMin] = 0x8A3B;
            mapDataStd [rangeNo] [0xB5FA - rangeMin] = 0x8A60;
            mapDataStd [rangeNo] [0xB5FB - rangeMin] = 0x8A55;
            mapDataStd [rangeNo] [0xB5FC - rangeMin] = 0x8A5E;
            mapDataStd [rangeNo] [0xB5FD - rangeMin] = 0x8A3C;
            mapDataStd [rangeNo] [0xB5FE - rangeMin] = 0x8A41;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xB640 - rangeMin] = 0x8A54;
            mapDataStd [rangeNo] [0xB641 - rangeMin] = 0x8A5B;
            mapDataStd [rangeNo] [0xB642 - rangeMin] = 0x8A50;
            mapDataStd [rangeNo] [0xB643 - rangeMin] = 0x8A46;
            mapDataStd [rangeNo] [0xB644 - rangeMin] = 0x8A34;
            mapDataStd [rangeNo] [0xB645 - rangeMin] = 0x8A3A;
            mapDataStd [rangeNo] [0xB646 - rangeMin] = 0x8A36;
            mapDataStd [rangeNo] [0xB647 - rangeMin] = 0x8A56;
            mapDataStd [rangeNo] [0xB648 - rangeMin] = 0x8C61;
            mapDataStd [rangeNo] [0xB649 - rangeMin] = 0x8C82;
            mapDataStd [rangeNo] [0xB64A - rangeMin] = 0x8CAF;
            mapDataStd [rangeNo] [0xB64B - rangeMin] = 0x8CBC;
            mapDataStd [rangeNo] [0xB64C - rangeMin] = 0x8CB3;
            mapDataStd [rangeNo] [0xB64D - rangeMin] = 0x8CBD;
            mapDataStd [rangeNo] [0xB64E - rangeMin] = 0x8CC1;
            mapDataStd [rangeNo] [0xB64F - rangeMin] = 0x8CBB;

            mapDataStd [rangeNo] [0xB650 - rangeMin] = 0x8CC0;
            mapDataStd [rangeNo] [0xB651 - rangeMin] = 0x8CB4;
            mapDataStd [rangeNo] [0xB652 - rangeMin] = 0x8CB7;
            mapDataStd [rangeNo] [0xB653 - rangeMin] = 0x8CB6;
            mapDataStd [rangeNo] [0xB654 - rangeMin] = 0x8CBF;
            mapDataStd [rangeNo] [0xB655 - rangeMin] = 0x8CB8;
            mapDataStd [rangeNo] [0xB656 - rangeMin] = 0x8D8A;
            mapDataStd [rangeNo] [0xB657 - rangeMin] = 0x8D85;
            mapDataStd [rangeNo] [0xB658 - rangeMin] = 0x8D81;
            mapDataStd [rangeNo] [0xB659 - rangeMin] = 0x8DCE;
            mapDataStd [rangeNo] [0xB65A - rangeMin] = 0x8DDD;
            mapDataStd [rangeNo] [0xB65B - rangeMin] = 0x8DCB;
            mapDataStd [rangeNo] [0xB65C - rangeMin] = 0x8DDA;
            mapDataStd [rangeNo] [0xB65D - rangeMin] = 0x8DD1;
            mapDataStd [rangeNo] [0xB65E - rangeMin] = 0x8DCC;
            mapDataStd [rangeNo] [0xB65F - rangeMin] = 0x8DDB;

            mapDataStd [rangeNo] [0xB660 - rangeMin] = 0x8DC6;
            mapDataStd [rangeNo] [0xB661 - rangeMin] = 0x8EFB;
            mapDataStd [rangeNo] [0xB662 - rangeMin] = 0x8EF8;
            mapDataStd [rangeNo] [0xB663 - rangeMin] = 0x8EFC;
            mapDataStd [rangeNo] [0xB664 - rangeMin] = 0x8F9C;
            mapDataStd [rangeNo] [0xB665 - rangeMin] = 0x902E;
            mapDataStd [rangeNo] [0xB666 - rangeMin] = 0x9035;
            mapDataStd [rangeNo] [0xB667 - rangeMin] = 0x9031;
            mapDataStd [rangeNo] [0xB668 - rangeMin] = 0x9038;
            mapDataStd [rangeNo] [0xB669 - rangeMin] = 0x9032;
            mapDataStd [rangeNo] [0xB66A - rangeMin] = 0x9036;
            mapDataStd [rangeNo] [0xB66B - rangeMin] = 0x9102;
            mapDataStd [rangeNo] [0xB66C - rangeMin] = 0x90F5;
            mapDataStd [rangeNo] [0xB66D - rangeMin] = 0x9109;
            mapDataStd [rangeNo] [0xB66E - rangeMin] = 0x90FE;
            mapDataStd [rangeNo] [0xB66F - rangeMin] = 0x9163;

            mapDataStd [rangeNo] [0xB670 - rangeMin] = 0x9165;
            mapDataStd [rangeNo] [0xB671 - rangeMin] = 0x91CF;
            mapDataStd [rangeNo] [0xB672 - rangeMin] = 0x9214;
            mapDataStd [rangeNo] [0xB673 - rangeMin] = 0x9215;
            mapDataStd [rangeNo] [0xB674 - rangeMin] = 0x9223;
            mapDataStd [rangeNo] [0xB675 - rangeMin] = 0x9209;
            mapDataStd [rangeNo] [0xB676 - rangeMin] = 0x921E;
            mapDataStd [rangeNo] [0xB677 - rangeMin] = 0x920D;
            mapDataStd [rangeNo] [0xB678 - rangeMin] = 0x9210;
            mapDataStd [rangeNo] [0xB679 - rangeMin] = 0x9207;
            mapDataStd [rangeNo] [0xB67A - rangeMin] = 0x9211;
            mapDataStd [rangeNo] [0xB67B - rangeMin] = 0x9594;
            mapDataStd [rangeNo] [0xB67C - rangeMin] = 0x958F;
            mapDataStd [rangeNo] [0xB67D - rangeMin] = 0x958B;
            mapDataStd [rangeNo] [0xB67E - rangeMin] = 0x9591;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xB6A1 - rangeMin] = 0x9593;
            mapDataStd [rangeNo] [0xB6A2 - rangeMin] = 0x9592;
            mapDataStd [rangeNo] [0xB6A3 - rangeMin] = 0x958E;
            mapDataStd [rangeNo] [0xB6A4 - rangeMin] = 0x968A;
            mapDataStd [rangeNo] [0xB6A5 - rangeMin] = 0x968E;
            mapDataStd [rangeNo] [0xB6A6 - rangeMin] = 0x968B;
            mapDataStd [rangeNo] [0xB6A7 - rangeMin] = 0x967D;
            mapDataStd [rangeNo] [0xB6A8 - rangeMin] = 0x9685;
            mapDataStd [rangeNo] [0xB6A9 - rangeMin] = 0x9686;
            mapDataStd [rangeNo] [0xB6AA - rangeMin] = 0x968D;
            mapDataStd [rangeNo] [0xB6AB - rangeMin] = 0x9672;
            mapDataStd [rangeNo] [0xB6AC - rangeMin] = 0x9684;
            mapDataStd [rangeNo] [0xB6AD - rangeMin] = 0x96C1;
            mapDataStd [rangeNo] [0xB6AE - rangeMin] = 0x96C5;
            mapDataStd [rangeNo] [0xB6AF - rangeMin] = 0x96C4;

            mapDataStd [rangeNo] [0xB6B0 - rangeMin] = 0x96C6;
            mapDataStd [rangeNo] [0xB6B1 - rangeMin] = 0x96C7;
            mapDataStd [rangeNo] [0xB6B2 - rangeMin] = 0x96EF;
            mapDataStd [rangeNo] [0xB6B3 - rangeMin] = 0x96F2;
            mapDataStd [rangeNo] [0xB6B4 - rangeMin] = 0x97CC;
            mapDataStd [rangeNo] [0xB6B5 - rangeMin] = 0x9805;
            mapDataStd [rangeNo] [0xB6B6 - rangeMin] = 0x9806;
            mapDataStd [rangeNo] [0xB6B7 - rangeMin] = 0x9808;
            mapDataStd [rangeNo] [0xB6B8 - rangeMin] = 0x98E7;
            mapDataStd [rangeNo] [0xB6B9 - rangeMin] = 0x98EA;
            mapDataStd [rangeNo] [0xB6BA - rangeMin] = 0x98EF;
            mapDataStd [rangeNo] [0xB6BB - rangeMin] = 0x98E9;
            mapDataStd [rangeNo] [0xB6BC - rangeMin] = 0x98F2;
            mapDataStd [rangeNo] [0xB6BD - rangeMin] = 0x98ED;
            mapDataStd [rangeNo] [0xB6BE - rangeMin] = 0x99AE;
            mapDataStd [rangeNo] [0xB6BF - rangeMin] = 0x99AD;

            mapDataStd [rangeNo] [0xB6C0 - rangeMin] = 0x9EC3;
            mapDataStd [rangeNo] [0xB6C1 - rangeMin] = 0x9ECD;
            mapDataStd [rangeNo] [0xB6C2 - rangeMin] = 0x9ED1;
            mapDataStd [rangeNo] [0xB6C3 - rangeMin] = 0x4E82;
            mapDataStd [rangeNo] [0xB6C4 - rangeMin] = 0x50AD;
            mapDataStd [rangeNo] [0xB6C5 - rangeMin] = 0x50B5;
            mapDataStd [rangeNo] [0xB6C6 - rangeMin] = 0x50B2;
            mapDataStd [rangeNo] [0xB6C7 - rangeMin] = 0x50B3;
            mapDataStd [rangeNo] [0xB6C8 - rangeMin] = 0x50C5;
            mapDataStd [rangeNo] [0xB6C9 - rangeMin] = 0x50BE;
            mapDataStd [rangeNo] [0xB6CA - rangeMin] = 0x50AC;
            mapDataStd [rangeNo] [0xB6CB - rangeMin] = 0x50B7;
            mapDataStd [rangeNo] [0xB6CC - rangeMin] = 0x50BB;
            mapDataStd [rangeNo] [0xB6CD - rangeMin] = 0x50AF;
            mapDataStd [rangeNo] [0xB6CE - rangeMin] = 0x50C7;
            mapDataStd [rangeNo] [0xB6CF - rangeMin] = 0x527F;

            mapDataStd [rangeNo] [0xB6D0 - rangeMin] = 0x5277;
            mapDataStd [rangeNo] [0xB6D1 - rangeMin] = 0x527D;
            mapDataStd [rangeNo] [0xB6D2 - rangeMin] = 0x52DF;
            mapDataStd [rangeNo] [0xB6D3 - rangeMin] = 0x52E6;
            mapDataStd [rangeNo] [0xB6D4 - rangeMin] = 0x52E4;
            mapDataStd [rangeNo] [0xB6D5 - rangeMin] = 0x52E2;
            mapDataStd [rangeNo] [0xB6D6 - rangeMin] = 0x52E3;
            mapDataStd [rangeNo] [0xB6D7 - rangeMin] = 0x532F;
            mapDataStd [rangeNo] [0xB6D8 - rangeMin] = 0x55DF;
            mapDataStd [rangeNo] [0xB6D9 - rangeMin] = 0x55E8;
            mapDataStd [rangeNo] [0xB6DA - rangeMin] = 0x55D3;
            mapDataStd [rangeNo] [0xB6DB - rangeMin] = 0x55E6;
            mapDataStd [rangeNo] [0xB6DC - rangeMin] = 0x55CE;
            mapDataStd [rangeNo] [0xB6DD - rangeMin] = 0x55DC;
            mapDataStd [rangeNo] [0xB6DE - rangeMin] = 0x55C7;
            mapDataStd [rangeNo] [0xB6DF - rangeMin] = 0x55D1;

            mapDataStd [rangeNo] [0xB6E0 - rangeMin] = 0x55E3;
            mapDataStd [rangeNo] [0xB6E1 - rangeMin] = 0x55E4;
            mapDataStd [rangeNo] [0xB6E2 - rangeMin] = 0x55EF;
            mapDataStd [rangeNo] [0xB6E3 - rangeMin] = 0x55DA;
            mapDataStd [rangeNo] [0xB6E4 - rangeMin] = 0x55E1;
            mapDataStd [rangeNo] [0xB6E5 - rangeMin] = 0x55C5;
            mapDataStd [rangeNo] [0xB6E6 - rangeMin] = 0x55C6;
            mapDataStd [rangeNo] [0xB6E7 - rangeMin] = 0x55E5;
            mapDataStd [rangeNo] [0xB6E8 - rangeMin] = 0x55C9;
            mapDataStd [rangeNo] [0xB6E9 - rangeMin] = 0x5712;
            mapDataStd [rangeNo] [0xB6EA - rangeMin] = 0x5713;
            mapDataStd [rangeNo] [0xB6EB - rangeMin] = 0x585E;
            mapDataStd [rangeNo] [0xB6EC - rangeMin] = 0x5851;
            mapDataStd [rangeNo] [0xB6ED - rangeMin] = 0x5858;
            mapDataStd [rangeNo] [0xB6EE - rangeMin] = 0x5857;
            mapDataStd [rangeNo] [0xB6EF - rangeMin] = 0x585A;

            mapDataStd [rangeNo] [0xB6F0 - rangeMin] = 0x5854;
            mapDataStd [rangeNo] [0xB6F1 - rangeMin] = 0x586B;
            mapDataStd [rangeNo] [0xB6F2 - rangeMin] = 0x584C;
            mapDataStd [rangeNo] [0xB6F3 - rangeMin] = 0x586D;
            mapDataStd [rangeNo] [0xB6F4 - rangeMin] = 0x584A;
            mapDataStd [rangeNo] [0xB6F5 - rangeMin] = 0x5862;
            mapDataStd [rangeNo] [0xB6F6 - rangeMin] = 0x5852;
            mapDataStd [rangeNo] [0xB6F7 - rangeMin] = 0x584B;
            mapDataStd [rangeNo] [0xB6F8 - rangeMin] = 0x5967;
            mapDataStd [rangeNo] [0xB6F9 - rangeMin] = 0x5AC1;
            mapDataStd [rangeNo] [0xB6FA - rangeMin] = 0x5AC9;
            mapDataStd [rangeNo] [0xB6FB - rangeMin] = 0x5ACC;
            mapDataStd [rangeNo] [0xB6FC - rangeMin] = 0x5ABE;
            mapDataStd [rangeNo] [0xB6FD - rangeMin] = 0x5ABD;
            mapDataStd [rangeNo] [0xB6FE - rangeMin] = 0x5ABC;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xB740 - rangeMin] = 0x5AB3;
            mapDataStd [rangeNo] [0xB741 - rangeMin] = 0x5AC2;
            mapDataStd [rangeNo] [0xB742 - rangeMin] = 0x5AB2;
            mapDataStd [rangeNo] [0xB743 - rangeMin] = 0x5D69;
            mapDataStd [rangeNo] [0xB744 - rangeMin] = 0x5D6F;
            mapDataStd [rangeNo] [0xB745 - rangeMin] = 0x5E4C;
            mapDataStd [rangeNo] [0xB746 - rangeMin] = 0x5E79;
            mapDataStd [rangeNo] [0xB747 - rangeMin] = 0x5EC9;
            mapDataStd [rangeNo] [0xB748 - rangeMin] = 0x5EC8;
            mapDataStd [rangeNo] [0xB749 - rangeMin] = 0x5F12;
            mapDataStd [rangeNo] [0xB74A - rangeMin] = 0x5F59;
            mapDataStd [rangeNo] [0xB74B - rangeMin] = 0x5FAC;
            mapDataStd [rangeNo] [0xB74C - rangeMin] = 0x5FAE;
            mapDataStd [rangeNo] [0xB74D - rangeMin] = 0x611A;
            mapDataStd [rangeNo] [0xB74E - rangeMin] = 0x610F;
            mapDataStd [rangeNo] [0xB74F - rangeMin] = 0x6148;

            mapDataStd [rangeNo] [0xB750 - rangeMin] = 0x611F;
            mapDataStd [rangeNo] [0xB751 - rangeMin] = 0x60F3;
            mapDataStd [rangeNo] [0xB752 - rangeMin] = 0x611B;
            mapDataStd [rangeNo] [0xB753 - rangeMin] = 0x60F9;
            mapDataStd [rangeNo] [0xB754 - rangeMin] = 0x6101;
            mapDataStd [rangeNo] [0xB755 - rangeMin] = 0x6108;
            mapDataStd [rangeNo] [0xB756 - rangeMin] = 0x614E;
            mapDataStd [rangeNo] [0xB757 - rangeMin] = 0x614C;
            mapDataStd [rangeNo] [0xB758 - rangeMin] = 0x6144;
            mapDataStd [rangeNo] [0xB759 - rangeMin] = 0x614D;
            mapDataStd [rangeNo] [0xB75A - rangeMin] = 0x613E;
            mapDataStd [rangeNo] [0xB75B - rangeMin] = 0x6134;
            mapDataStd [rangeNo] [0xB75C - rangeMin] = 0x6127;
            mapDataStd [rangeNo] [0xB75D - rangeMin] = 0x610D;
            mapDataStd [rangeNo] [0xB75E - rangeMin] = 0x6106;
            mapDataStd [rangeNo] [0xB75F - rangeMin] = 0x6137;

            mapDataStd [rangeNo] [0xB760 - rangeMin] = 0x6221;
            mapDataStd [rangeNo] [0xB761 - rangeMin] = 0x6222;
            mapDataStd [rangeNo] [0xB762 - rangeMin] = 0x6413;
            mapDataStd [rangeNo] [0xB763 - rangeMin] = 0x643E;
            mapDataStd [rangeNo] [0xB764 - rangeMin] = 0x641E;
            mapDataStd [rangeNo] [0xB765 - rangeMin] = 0x642A;
            mapDataStd [rangeNo] [0xB766 - rangeMin] = 0x642D;
            mapDataStd [rangeNo] [0xB767 - rangeMin] = 0x643D;
            mapDataStd [rangeNo] [0xB768 - rangeMin] = 0x642C;
            mapDataStd [rangeNo] [0xB769 - rangeMin] = 0x640F;
            mapDataStd [rangeNo] [0xB76A - rangeMin] = 0x641C;
            mapDataStd [rangeNo] [0xB76B - rangeMin] = 0x6414;
            mapDataStd [rangeNo] [0xB76C - rangeMin] = 0x640D;
            mapDataStd [rangeNo] [0xB76D - rangeMin] = 0x6436;
            mapDataStd [rangeNo] [0xB76E - rangeMin] = 0x6416;
            mapDataStd [rangeNo] [0xB76F - rangeMin] = 0x6417;

            mapDataStd [rangeNo] [0xB770 - rangeMin] = 0x6406;
            mapDataStd [rangeNo] [0xB771 - rangeMin] = 0x656C;
            mapDataStd [rangeNo] [0xB772 - rangeMin] = 0x659F;
            mapDataStd [rangeNo] [0xB773 - rangeMin] = 0x65B0;
            mapDataStd [rangeNo] [0xB774 - rangeMin] = 0x6697;
            mapDataStd [rangeNo] [0xB775 - rangeMin] = 0x6689;
            mapDataStd [rangeNo] [0xB776 - rangeMin] = 0x6687;
            mapDataStd [rangeNo] [0xB777 - rangeMin] = 0x6688;
            mapDataStd [rangeNo] [0xB778 - rangeMin] = 0x6696;
            mapDataStd [rangeNo] [0xB779 - rangeMin] = 0x6684;
            mapDataStd [rangeNo] [0xB77A - rangeMin] = 0x6698;
            mapDataStd [rangeNo] [0xB77B - rangeMin] = 0x668D;
            mapDataStd [rangeNo] [0xB77C - rangeMin] = 0x6703;
            mapDataStd [rangeNo] [0xB77D - rangeMin] = 0x6994;
            mapDataStd [rangeNo] [0xB77E - rangeMin] = 0x696D;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xB7A1 - rangeMin] = 0x695A;
            mapDataStd [rangeNo] [0xB7A2 - rangeMin] = 0x6977;
            mapDataStd [rangeNo] [0xB7A3 - rangeMin] = 0x6960;
            mapDataStd [rangeNo] [0xB7A4 - rangeMin] = 0x6954;
            mapDataStd [rangeNo] [0xB7A5 - rangeMin] = 0x6975;
            mapDataStd [rangeNo] [0xB7A6 - rangeMin] = 0x6930;
            mapDataStd [rangeNo] [0xB7A7 - rangeMin] = 0x6982;
            mapDataStd [rangeNo] [0xB7A8 - rangeMin] = 0x694A;
            mapDataStd [rangeNo] [0xB7A9 - rangeMin] = 0x6968;
            mapDataStd [rangeNo] [0xB7AA - rangeMin] = 0x696B;
            mapDataStd [rangeNo] [0xB7AB - rangeMin] = 0x695E;
            mapDataStd [rangeNo] [0xB7AC - rangeMin] = 0x6953;
            mapDataStd [rangeNo] [0xB7AD - rangeMin] = 0x6979;
            mapDataStd [rangeNo] [0xB7AE - rangeMin] = 0x6986;
            mapDataStd [rangeNo] [0xB7AF - rangeMin] = 0x695D;

            mapDataStd [rangeNo] [0xB7B0 - rangeMin] = 0x6963;
            mapDataStd [rangeNo] [0xB7B1 - rangeMin] = 0x695B;
            mapDataStd [rangeNo] [0xB7B2 - rangeMin] = 0x6B47;
            mapDataStd [rangeNo] [0xB7B3 - rangeMin] = 0x6B72;
            mapDataStd [rangeNo] [0xB7B4 - rangeMin] = 0x6BC0;
            mapDataStd [rangeNo] [0xB7B5 - rangeMin] = 0x6BBF;
            mapDataStd [rangeNo] [0xB7B6 - rangeMin] = 0x6BD3;
            mapDataStd [rangeNo] [0xB7B7 - rangeMin] = 0x6BFD;
            mapDataStd [rangeNo] [0xB7B8 - rangeMin] = 0x6EA2;
            mapDataStd [rangeNo] [0xB7B9 - rangeMin] = 0x6EAF;
            mapDataStd [rangeNo] [0xB7BA - rangeMin] = 0x6ED3;
            mapDataStd [rangeNo] [0xB7BB - rangeMin] = 0x6EB6;
            mapDataStd [rangeNo] [0xB7BC - rangeMin] = 0x6EC2;
            mapDataStd [rangeNo] [0xB7BD - rangeMin] = 0x6E90;
            mapDataStd [rangeNo] [0xB7BE - rangeMin] = 0x6E9D;
            mapDataStd [rangeNo] [0xB7BF - rangeMin] = 0x6EC7;

            mapDataStd [rangeNo] [0xB7C0 - rangeMin] = 0x6EC5;
            mapDataStd [rangeNo] [0xB7C1 - rangeMin] = 0x6EA5;
            mapDataStd [rangeNo] [0xB7C2 - rangeMin] = 0x6E98;
            mapDataStd [rangeNo] [0xB7C3 - rangeMin] = 0x6EBC;
            mapDataStd [rangeNo] [0xB7C4 - rangeMin] = 0x6EBA;
            mapDataStd [rangeNo] [0xB7C5 - rangeMin] = 0x6EAB;
            mapDataStd [rangeNo] [0xB7C6 - rangeMin] = 0x6ED1;
            mapDataStd [rangeNo] [0xB7C7 - rangeMin] = 0x6E96;
            mapDataStd [rangeNo] [0xB7C8 - rangeMin] = 0x6E9C;
            mapDataStd [rangeNo] [0xB7C9 - rangeMin] = 0x6EC4;
            mapDataStd [rangeNo] [0xB7CA - rangeMin] = 0x6ED4;
            mapDataStd [rangeNo] [0xB7CB - rangeMin] = 0x6EAA;
            mapDataStd [rangeNo] [0xB7CC - rangeMin] = 0x6EA7;
            mapDataStd [rangeNo] [0xB7CD - rangeMin] = 0x6EB4;
            mapDataStd [rangeNo] [0xB7CE - rangeMin] = 0x714E;
            mapDataStd [rangeNo] [0xB7CF - rangeMin] = 0x7159;

            mapDataStd [rangeNo] [0xB7D0 - rangeMin] = 0x7169;
            mapDataStd [rangeNo] [0xB7D1 - rangeMin] = 0x7164;
            mapDataStd [rangeNo] [0xB7D2 - rangeMin] = 0x7149;
            mapDataStd [rangeNo] [0xB7D3 - rangeMin] = 0x7167;
            mapDataStd [rangeNo] [0xB7D4 - rangeMin] = 0x715C;
            mapDataStd [rangeNo] [0xB7D5 - rangeMin] = 0x716C;
            mapDataStd [rangeNo] [0xB7D6 - rangeMin] = 0x7166;
            mapDataStd [rangeNo] [0xB7D7 - rangeMin] = 0x714C;
            mapDataStd [rangeNo] [0xB7D8 - rangeMin] = 0x7165;
            mapDataStd [rangeNo] [0xB7D9 - rangeMin] = 0x715E;
            mapDataStd [rangeNo] [0xB7DA - rangeMin] = 0x7146;
            mapDataStd [rangeNo] [0xB7DB - rangeMin] = 0x7168;
            mapDataStd [rangeNo] [0xB7DC - rangeMin] = 0x7156;
            mapDataStd [rangeNo] [0xB7DD - rangeMin] = 0x723A;
            mapDataStd [rangeNo] [0xB7DE - rangeMin] = 0x7252;
            mapDataStd [rangeNo] [0xB7DF - rangeMin] = 0x7337;

            mapDataStd [rangeNo] [0xB7E0 - rangeMin] = 0x7345;
            mapDataStd [rangeNo] [0xB7E1 - rangeMin] = 0x733F;
            mapDataStd [rangeNo] [0xB7E2 - rangeMin] = 0x733E;
            mapDataStd [rangeNo] [0xB7E3 - rangeMin] = 0x746F;
            mapDataStd [rangeNo] [0xB7E4 - rangeMin] = 0x745A;
            mapDataStd [rangeNo] [0xB7E5 - rangeMin] = 0x7455;
            mapDataStd [rangeNo] [0xB7E6 - rangeMin] = 0x745F;
            mapDataStd [rangeNo] [0xB7E7 - rangeMin] = 0x745E;
            mapDataStd [rangeNo] [0xB7E8 - rangeMin] = 0x7441;
            mapDataStd [rangeNo] [0xB7E9 - rangeMin] = 0x743F;
            mapDataStd [rangeNo] [0xB7EA - rangeMin] = 0x7459;
            mapDataStd [rangeNo] [0xB7EB - rangeMin] = 0x745B;
            mapDataStd [rangeNo] [0xB7EC - rangeMin] = 0x745C;
            mapDataStd [rangeNo] [0xB7ED - rangeMin] = 0x7576;
            mapDataStd [rangeNo] [0xB7EE - rangeMin] = 0x7578;
            mapDataStd [rangeNo] [0xB7EF - rangeMin] = 0x7600;

            mapDataStd [rangeNo] [0xB7F0 - rangeMin] = 0x75F0;
            mapDataStd [rangeNo] [0xB7F1 - rangeMin] = 0x7601;
            mapDataStd [rangeNo] [0xB7F2 - rangeMin] = 0x75F2;
            mapDataStd [rangeNo] [0xB7F3 - rangeMin] = 0x75F1;
            mapDataStd [rangeNo] [0xB7F4 - rangeMin] = 0x75FA;
            mapDataStd [rangeNo] [0xB7F5 - rangeMin] = 0x75FF;
            mapDataStd [rangeNo] [0xB7F6 - rangeMin] = 0x75F4;
            mapDataStd [rangeNo] [0xB7F7 - rangeMin] = 0x75F3;
            mapDataStd [rangeNo] [0xB7F8 - rangeMin] = 0x76DE;
            mapDataStd [rangeNo] [0xB7F9 - rangeMin] = 0x76DF;
            mapDataStd [rangeNo] [0xB7FA - rangeMin] = 0x775B;
            mapDataStd [rangeNo] [0xB7FB - rangeMin] = 0x776B;
            mapDataStd [rangeNo] [0xB7FC - rangeMin] = 0x7766;
            mapDataStd [rangeNo] [0xB7FD - rangeMin] = 0x775E;
            mapDataStd [rangeNo] [0xB7FE - rangeMin] = 0x7763;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xB840 - rangeMin] = 0x7779;
            mapDataStd [rangeNo] [0xB841 - rangeMin] = 0x776A;
            mapDataStd [rangeNo] [0xB842 - rangeMin] = 0x776C;
            mapDataStd [rangeNo] [0xB843 - rangeMin] = 0x775C;
            mapDataStd [rangeNo] [0xB844 - rangeMin] = 0x7765;
            mapDataStd [rangeNo] [0xB845 - rangeMin] = 0x7768;
            mapDataStd [rangeNo] [0xB846 - rangeMin] = 0x7762;
            mapDataStd [rangeNo] [0xB847 - rangeMin] = 0x77EE;
            mapDataStd [rangeNo] [0xB848 - rangeMin] = 0x788E;
            mapDataStd [rangeNo] [0xB849 - rangeMin] = 0x78B0;
            mapDataStd [rangeNo] [0xB84A - rangeMin] = 0x7897;
            mapDataStd [rangeNo] [0xB84B - rangeMin] = 0x7898;
            mapDataStd [rangeNo] [0xB84C - rangeMin] = 0x788C;
            mapDataStd [rangeNo] [0xB84D - rangeMin] = 0x7889;
            mapDataStd [rangeNo] [0xB84E - rangeMin] = 0x787C;
            mapDataStd [rangeNo] [0xB84F - rangeMin] = 0x7891;

            mapDataStd [rangeNo] [0xB850 - rangeMin] = 0x7893;
            mapDataStd [rangeNo] [0xB851 - rangeMin] = 0x787F;
            mapDataStd [rangeNo] [0xB852 - rangeMin] = 0x797A;
            mapDataStd [rangeNo] [0xB853 - rangeMin] = 0x797F;
            mapDataStd [rangeNo] [0xB854 - rangeMin] = 0x7981;
            mapDataStd [rangeNo] [0xB855 - rangeMin] = 0x842C;
            mapDataStd [rangeNo] [0xB856 - rangeMin] = 0x79BD;
            mapDataStd [rangeNo] [0xB857 - rangeMin] = 0x7A1C;
            mapDataStd [rangeNo] [0xB858 - rangeMin] = 0x7A1A;
            mapDataStd [rangeNo] [0xB859 - rangeMin] = 0x7A20;
            mapDataStd [rangeNo] [0xB85A - rangeMin] = 0x7A14;
            mapDataStd [rangeNo] [0xB85B - rangeMin] = 0x7A1F;
            mapDataStd [rangeNo] [0xB85C - rangeMin] = 0x7A1E;
            mapDataStd [rangeNo] [0xB85D - rangeMin] = 0x7A9F;
            mapDataStd [rangeNo] [0xB85E - rangeMin] = 0x7AA0;
            mapDataStd [rangeNo] [0xB85F - rangeMin] = 0x7B77;

            mapDataStd [rangeNo] [0xB860 - rangeMin] = 0x7BC0;
            mapDataStd [rangeNo] [0xB861 - rangeMin] = 0x7B60;
            mapDataStd [rangeNo] [0xB862 - rangeMin] = 0x7B6E;
            mapDataStd [rangeNo] [0xB863 - rangeMin] = 0x7B67;
            mapDataStd [rangeNo] [0xB864 - rangeMin] = 0x7CB1;
            mapDataStd [rangeNo] [0xB865 - rangeMin] = 0x7CB3;
            mapDataStd [rangeNo] [0xB866 - rangeMin] = 0x7CB5;
            mapDataStd [rangeNo] [0xB867 - rangeMin] = 0x7D93;
            mapDataStd [rangeNo] [0xB868 - rangeMin] = 0x7D79;
            mapDataStd [rangeNo] [0xB869 - rangeMin] = 0x7D91;
            mapDataStd [rangeNo] [0xB86A - rangeMin] = 0x7D81;
            mapDataStd [rangeNo] [0xB86B - rangeMin] = 0x7D8F;
            mapDataStd [rangeNo] [0xB86C - rangeMin] = 0x7D5B;
            mapDataStd [rangeNo] [0xB86D - rangeMin] = 0x7F6E;
            mapDataStd [rangeNo] [0xB86E - rangeMin] = 0x7F69;
            mapDataStd [rangeNo] [0xB86F - rangeMin] = 0x7F6A;

            mapDataStd [rangeNo] [0xB870 - rangeMin] = 0x7F72;
            mapDataStd [rangeNo] [0xB871 - rangeMin] = 0x7FA9;
            mapDataStd [rangeNo] [0xB872 - rangeMin] = 0x7FA8;
            mapDataStd [rangeNo] [0xB873 - rangeMin] = 0x7FA4;
            mapDataStd [rangeNo] [0xB874 - rangeMin] = 0x8056;
            mapDataStd [rangeNo] [0xB875 - rangeMin] = 0x8058;
            mapDataStd [rangeNo] [0xB876 - rangeMin] = 0x8086;
            mapDataStd [rangeNo] [0xB877 - rangeMin] = 0x8084;
            mapDataStd [rangeNo] [0xB878 - rangeMin] = 0x8171;
            mapDataStd [rangeNo] [0xB879 - rangeMin] = 0x8170;
            mapDataStd [rangeNo] [0xB87A - rangeMin] = 0x8178;
            mapDataStd [rangeNo] [0xB87B - rangeMin] = 0x8165;
            mapDataStd [rangeNo] [0xB87C - rangeMin] = 0x816E;
            mapDataStd [rangeNo] [0xB87D - rangeMin] = 0x8173;
            mapDataStd [rangeNo] [0xB87E - rangeMin] = 0x816B;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xB8A1 - rangeMin] = 0x8179;
            mapDataStd [rangeNo] [0xB8A2 - rangeMin] = 0x817A;
            mapDataStd [rangeNo] [0xB8A3 - rangeMin] = 0x8166;
            mapDataStd [rangeNo] [0xB8A4 - rangeMin] = 0x8205;
            mapDataStd [rangeNo] [0xB8A5 - rangeMin] = 0x8247;
            mapDataStd [rangeNo] [0xB8A6 - rangeMin] = 0x8482;
            mapDataStd [rangeNo] [0xB8A7 - rangeMin] = 0x8477;
            mapDataStd [rangeNo] [0xB8A8 - rangeMin] = 0x843D;
            mapDataStd [rangeNo] [0xB8A9 - rangeMin] = 0x8431;
            mapDataStd [rangeNo] [0xB8AA - rangeMin] = 0x8475;
            mapDataStd [rangeNo] [0xB8AB - rangeMin] = 0x8466;
            mapDataStd [rangeNo] [0xB8AC - rangeMin] = 0x846B;
            mapDataStd [rangeNo] [0xB8AD - rangeMin] = 0x8449;
            mapDataStd [rangeNo] [0xB8AE - rangeMin] = 0x846C;
            mapDataStd [rangeNo] [0xB8AF - rangeMin] = 0x845B;

            mapDataStd [rangeNo] [0xB8B0 - rangeMin] = 0x843C;
            mapDataStd [rangeNo] [0xB8B1 - rangeMin] = 0x8435;
            mapDataStd [rangeNo] [0xB8B2 - rangeMin] = 0x8461;
            mapDataStd [rangeNo] [0xB8B3 - rangeMin] = 0x8463;
            mapDataStd [rangeNo] [0xB8B4 - rangeMin] = 0x8469;
            mapDataStd [rangeNo] [0xB8B5 - rangeMin] = 0x846D;
            mapDataStd [rangeNo] [0xB8B6 - rangeMin] = 0x8446;
            mapDataStd [rangeNo] [0xB8B7 - rangeMin] = 0x865E;
            mapDataStd [rangeNo] [0xB8B8 - rangeMin] = 0x865C;
            mapDataStd [rangeNo] [0xB8B9 - rangeMin] = 0x865F;
            mapDataStd [rangeNo] [0xB8BA - rangeMin] = 0x86F9;
            mapDataStd [rangeNo] [0xB8BB - rangeMin] = 0x8713;
            mapDataStd [rangeNo] [0xB8BC - rangeMin] = 0x8708;
            mapDataStd [rangeNo] [0xB8BD - rangeMin] = 0x8707;
            mapDataStd [rangeNo] [0xB8BE - rangeMin] = 0x8700;
            mapDataStd [rangeNo] [0xB8BF - rangeMin] = 0x86FE;

            mapDataStd [rangeNo] [0xB8C0 - rangeMin] = 0x86FB;
            mapDataStd [rangeNo] [0xB8C1 - rangeMin] = 0x8702;
            mapDataStd [rangeNo] [0xB8C2 - rangeMin] = 0x8703;
            mapDataStd [rangeNo] [0xB8C3 - rangeMin] = 0x8706;
            mapDataStd [rangeNo] [0xB8C4 - rangeMin] = 0x870A;
            mapDataStd [rangeNo] [0xB8C5 - rangeMin] = 0x8859;
            mapDataStd [rangeNo] [0xB8C6 - rangeMin] = 0x88DF;
            mapDataStd [rangeNo] [0xB8C7 - rangeMin] = 0x88D4;
            mapDataStd [rangeNo] [0xB8C8 - rangeMin] = 0x88D9;
            mapDataStd [rangeNo] [0xB8C9 - rangeMin] = 0x88DC;
            mapDataStd [rangeNo] [0xB8CA - rangeMin] = 0x88D8;
            mapDataStd [rangeNo] [0xB8CB - rangeMin] = 0x88DD;
            mapDataStd [rangeNo] [0xB8CC - rangeMin] = 0x88E1;
            mapDataStd [rangeNo] [0xB8CD - rangeMin] = 0x88CA;
            mapDataStd [rangeNo] [0xB8CE - rangeMin] = 0x88D5;
            mapDataStd [rangeNo] [0xB8CF - rangeMin] = 0x88D2;

            mapDataStd [rangeNo] [0xB8D0 - rangeMin] = 0x899C;
            mapDataStd [rangeNo] [0xB8D1 - rangeMin] = 0x89E3;
            mapDataStd [rangeNo] [0xB8D2 - rangeMin] = 0x8A6B;
            mapDataStd [rangeNo] [0xB8D3 - rangeMin] = 0x8A72;
            mapDataStd [rangeNo] [0xB8D4 - rangeMin] = 0x8A73;
            mapDataStd [rangeNo] [0xB8D5 - rangeMin] = 0x8A66;
            mapDataStd [rangeNo] [0xB8D6 - rangeMin] = 0x8A69;
            mapDataStd [rangeNo] [0xB8D7 - rangeMin] = 0x8A70;
            mapDataStd [rangeNo] [0xB8D8 - rangeMin] = 0x8A87;
            mapDataStd [rangeNo] [0xB8D9 - rangeMin] = 0x8A7C;
            mapDataStd [rangeNo] [0xB8DA - rangeMin] = 0x8A63;
            mapDataStd [rangeNo] [0xB8DB - rangeMin] = 0x8AA0;
            mapDataStd [rangeNo] [0xB8DC - rangeMin] = 0x8A71;
            mapDataStd [rangeNo] [0xB8DD - rangeMin] = 0x8A85;
            mapDataStd [rangeNo] [0xB8DE - rangeMin] = 0x8A6D;
            mapDataStd [rangeNo] [0xB8DF - rangeMin] = 0x8A62;

            mapDataStd [rangeNo] [0xB8E0 - rangeMin] = 0x8A6E;
            mapDataStd [rangeNo] [0xB8E1 - rangeMin] = 0x8A6C;
            mapDataStd [rangeNo] [0xB8E2 - rangeMin] = 0x8A79;
            mapDataStd [rangeNo] [0xB8E3 - rangeMin] = 0x8A7B;
            mapDataStd [rangeNo] [0xB8E4 - rangeMin] = 0x8A3E;
            mapDataStd [rangeNo] [0xB8E5 - rangeMin] = 0x8A68;
            mapDataStd [rangeNo] [0xB8E6 - rangeMin] = 0x8C62;
            mapDataStd [rangeNo] [0xB8E7 - rangeMin] = 0x8C8A;
            mapDataStd [rangeNo] [0xB8E8 - rangeMin] = 0x8C89;
            mapDataStd [rangeNo] [0xB8E9 - rangeMin] = 0x8CCA;
            mapDataStd [rangeNo] [0xB8EA - rangeMin] = 0x8CC7;
            mapDataStd [rangeNo] [0xB8EB - rangeMin] = 0x8CC8;
            mapDataStd [rangeNo] [0xB8EC - rangeMin] = 0x8CC4;
            mapDataStd [rangeNo] [0xB8ED - rangeMin] = 0x8CB2;
            mapDataStd [rangeNo] [0xB8EE - rangeMin] = 0x8CC3;
            mapDataStd [rangeNo] [0xB8EF - rangeMin] = 0x8CC2;

            mapDataStd [rangeNo] [0xB8F0 - rangeMin] = 0x8CC5;
            mapDataStd [rangeNo] [0xB8F1 - rangeMin] = 0x8DE1;
            mapDataStd [rangeNo] [0xB8F2 - rangeMin] = 0x8DDF;
            mapDataStd [rangeNo] [0xB8F3 - rangeMin] = 0x8DE8;
            mapDataStd [rangeNo] [0xB8F4 - rangeMin] = 0x8DEF;
            mapDataStd [rangeNo] [0xB8F5 - rangeMin] = 0x8DF3;
            mapDataStd [rangeNo] [0xB8F6 - rangeMin] = 0x8DFA;
            mapDataStd [rangeNo] [0xB8F7 - rangeMin] = 0x8DEA;
            mapDataStd [rangeNo] [0xB8F8 - rangeMin] = 0x8DE4;
            mapDataStd [rangeNo] [0xB8F9 - rangeMin] = 0x8DE6;
            mapDataStd [rangeNo] [0xB8FA - rangeMin] = 0x8EB2;
            mapDataStd [rangeNo] [0xB8FB - rangeMin] = 0x8F03;
            mapDataStd [rangeNo] [0xB8FC - rangeMin] = 0x8F09;
            mapDataStd [rangeNo] [0xB8FD - rangeMin] = 0x8EFE;
            mapDataStd [rangeNo] [0xB8FE - rangeMin] = 0x8F0A;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xB940 - rangeMin] = 0x8F9F;
            mapDataStd [rangeNo] [0xB941 - rangeMin] = 0x8FB2;
            mapDataStd [rangeNo] [0xB942 - rangeMin] = 0x904B;
            mapDataStd [rangeNo] [0xB943 - rangeMin] = 0x904A;
            mapDataStd [rangeNo] [0xB944 - rangeMin] = 0x9053;
            mapDataStd [rangeNo] [0xB945 - rangeMin] = 0x9042;
            mapDataStd [rangeNo] [0xB946 - rangeMin] = 0x9054;
            mapDataStd [rangeNo] [0xB947 - rangeMin] = 0x903C;
            mapDataStd [rangeNo] [0xB948 - rangeMin] = 0x9055;
            mapDataStd [rangeNo] [0xB949 - rangeMin] = 0x9050;
            mapDataStd [rangeNo] [0xB94A - rangeMin] = 0x9047;
            mapDataStd [rangeNo] [0xB94B - rangeMin] = 0x904F;
            mapDataStd [rangeNo] [0xB94C - rangeMin] = 0x904E;
            mapDataStd [rangeNo] [0xB94D - rangeMin] = 0x904D;
            mapDataStd [rangeNo] [0xB94E - rangeMin] = 0x9051;
            mapDataStd [rangeNo] [0xB94F - rangeMin] = 0x903E;

            mapDataStd [rangeNo] [0xB950 - rangeMin] = 0x9041;
            mapDataStd [rangeNo] [0xB951 - rangeMin] = 0x9112;
            mapDataStd [rangeNo] [0xB952 - rangeMin] = 0x9117;
            mapDataStd [rangeNo] [0xB953 - rangeMin] = 0x916C;
            mapDataStd [rangeNo] [0xB954 - rangeMin] = 0x916A;
            mapDataStd [rangeNo] [0xB955 - rangeMin] = 0x9169;
            mapDataStd [rangeNo] [0xB956 - rangeMin] = 0x91C9;
            mapDataStd [rangeNo] [0xB957 - rangeMin] = 0x9237;
            mapDataStd [rangeNo] [0xB958 - rangeMin] = 0x9257;
            mapDataStd [rangeNo] [0xB959 - rangeMin] = 0x9238;
            mapDataStd [rangeNo] [0xB95A - rangeMin] = 0x923D;
            mapDataStd [rangeNo] [0xB95B - rangeMin] = 0x9240;
            mapDataStd [rangeNo] [0xB95C - rangeMin] = 0x923E;
            mapDataStd [rangeNo] [0xB95D - rangeMin] = 0x925B;
            mapDataStd [rangeNo] [0xB95E - rangeMin] = 0x924B;
            mapDataStd [rangeNo] [0xB95F - rangeMin] = 0x9264;

            mapDataStd [rangeNo] [0xB960 - rangeMin] = 0x9251;
            mapDataStd [rangeNo] [0xB961 - rangeMin] = 0x9234;
            mapDataStd [rangeNo] [0xB962 - rangeMin] = 0x9249;
            mapDataStd [rangeNo] [0xB963 - rangeMin] = 0x924D;
            mapDataStd [rangeNo] [0xB964 - rangeMin] = 0x9245;
            mapDataStd [rangeNo] [0xB965 - rangeMin] = 0x9239;
            mapDataStd [rangeNo] [0xB966 - rangeMin] = 0x923F;
            mapDataStd [rangeNo] [0xB967 - rangeMin] = 0x925A;
            mapDataStd [rangeNo] [0xB968 - rangeMin] = 0x9598;
            mapDataStd [rangeNo] [0xB969 - rangeMin] = 0x9698;
            mapDataStd [rangeNo] [0xB96A - rangeMin] = 0x9694;
            mapDataStd [rangeNo] [0xB96B - rangeMin] = 0x9695;
            mapDataStd [rangeNo] [0xB96C - rangeMin] = 0x96CD;
            mapDataStd [rangeNo] [0xB96D - rangeMin] = 0x96CB;
            mapDataStd [rangeNo] [0xB96E - rangeMin] = 0x96C9;
            mapDataStd [rangeNo] [0xB96F - rangeMin] = 0x96CA;

            mapDataStd [rangeNo] [0xB970 - rangeMin] = 0x96F7;
            mapDataStd [rangeNo] [0xB971 - rangeMin] = 0x96FB;
            mapDataStd [rangeNo] [0xB972 - rangeMin] = 0x96F9;
            mapDataStd [rangeNo] [0xB973 - rangeMin] = 0x96F6;
            mapDataStd [rangeNo] [0xB974 - rangeMin] = 0x9756;
            mapDataStd [rangeNo] [0xB975 - rangeMin] = 0x9774;
            mapDataStd [rangeNo] [0xB976 - rangeMin] = 0x9776;
            mapDataStd [rangeNo] [0xB977 - rangeMin] = 0x9810;
            mapDataStd [rangeNo] [0xB978 - rangeMin] = 0x9811;
            mapDataStd [rangeNo] [0xB979 - rangeMin] = 0x9813;
            mapDataStd [rangeNo] [0xB97A - rangeMin] = 0x980A;
            mapDataStd [rangeNo] [0xB97B - rangeMin] = 0x9812;
            mapDataStd [rangeNo] [0xB97C - rangeMin] = 0x980C;
            mapDataStd [rangeNo] [0xB97D - rangeMin] = 0x98FC;
            mapDataStd [rangeNo] [0xB97E - rangeMin] = 0x98F4;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xB9A1 - rangeMin] = 0x98FD;
            mapDataStd [rangeNo] [0xB9A2 - rangeMin] = 0x98FE;
            mapDataStd [rangeNo] [0xB9A3 - rangeMin] = 0x99B3;
            mapDataStd [rangeNo] [0xB9A4 - rangeMin] = 0x99B1;
            mapDataStd [rangeNo] [0xB9A5 - rangeMin] = 0x99B4;
            mapDataStd [rangeNo] [0xB9A6 - rangeMin] = 0x9AE1;
            mapDataStd [rangeNo] [0xB9A7 - rangeMin] = 0x9CE9;
            mapDataStd [rangeNo] [0xB9A8 - rangeMin] = 0x9E82;
            mapDataStd [rangeNo] [0xB9A9 - rangeMin] = 0x9F0E;
            mapDataStd [rangeNo] [0xB9AA - rangeMin] = 0x9F13;
            mapDataStd [rangeNo] [0xB9AB - rangeMin] = 0x9F20;
            mapDataStd [rangeNo] [0xB9AC - rangeMin] = 0x50E7;
            mapDataStd [rangeNo] [0xB9AD - rangeMin] = 0x50EE;
            mapDataStd [rangeNo] [0xB9AE - rangeMin] = 0x50E5;
            mapDataStd [rangeNo] [0xB9AF - rangeMin] = 0x50D6;

            mapDataStd [rangeNo] [0xB9B0 - rangeMin] = 0x50ED;
            mapDataStd [rangeNo] [0xB9B1 - rangeMin] = 0x50DA;
            mapDataStd [rangeNo] [0xB9B2 - rangeMin] = 0x50D5;
            mapDataStd [rangeNo] [0xB9B3 - rangeMin] = 0x50CF;
            mapDataStd [rangeNo] [0xB9B4 - rangeMin] = 0x50D1;
            mapDataStd [rangeNo] [0xB9B5 - rangeMin] = 0x50F1;
            mapDataStd [rangeNo] [0xB9B6 - rangeMin] = 0x50CE;
            mapDataStd [rangeNo] [0xB9B7 - rangeMin] = 0x50E9;
            mapDataStd [rangeNo] [0xB9B8 - rangeMin] = 0x5162;
            mapDataStd [rangeNo] [0xB9B9 - rangeMin] = 0x51F3;
            mapDataStd [rangeNo] [0xB9BA - rangeMin] = 0x5283;
            mapDataStd [rangeNo] [0xB9BB - rangeMin] = 0x5282;
            mapDataStd [rangeNo] [0xB9BC - rangeMin] = 0x5331;
            mapDataStd [rangeNo] [0xB9BD - rangeMin] = 0x53AD;
            mapDataStd [rangeNo] [0xB9BE - rangeMin] = 0x55FE;
            mapDataStd [rangeNo] [0xB9BF - rangeMin] = 0x5600;

            mapDataStd [rangeNo] [0xB9C0 - rangeMin] = 0x561B;
            mapDataStd [rangeNo] [0xB9C1 - rangeMin] = 0x5617;
            mapDataStd [rangeNo] [0xB9C2 - rangeMin] = 0x55FD;
            mapDataStd [rangeNo] [0xB9C3 - rangeMin] = 0x5614;
            mapDataStd [rangeNo] [0xB9C4 - rangeMin] = 0x5606;
            mapDataStd [rangeNo] [0xB9C5 - rangeMin] = 0x5609;
            mapDataStd [rangeNo] [0xB9C6 - rangeMin] = 0x560D;
            mapDataStd [rangeNo] [0xB9C7 - rangeMin] = 0x560E;
            mapDataStd [rangeNo] [0xB9C8 - rangeMin] = 0x55F7;
            mapDataStd [rangeNo] [0xB9C9 - rangeMin] = 0x5616;
            mapDataStd [rangeNo] [0xB9CA - rangeMin] = 0x561F;
            mapDataStd [rangeNo] [0xB9CB - rangeMin] = 0x5608;
            mapDataStd [rangeNo] [0xB9CC - rangeMin] = 0x5610;
            mapDataStd [rangeNo] [0xB9CD - rangeMin] = 0x55F6;
            mapDataStd [rangeNo] [0xB9CE - rangeMin] = 0x5718;
            mapDataStd [rangeNo] [0xB9CF - rangeMin] = 0x5716;

            mapDataStd [rangeNo] [0xB9D0 - rangeMin] = 0x5875;
            mapDataStd [rangeNo] [0xB9D1 - rangeMin] = 0x587E;
            mapDataStd [rangeNo] [0xB9D2 - rangeMin] = 0x5883;
            mapDataStd [rangeNo] [0xB9D3 - rangeMin] = 0x5893;
            mapDataStd [rangeNo] [0xB9D4 - rangeMin] = 0x588A;
            mapDataStd [rangeNo] [0xB9D5 - rangeMin] = 0x5879;
            mapDataStd [rangeNo] [0xB9D6 - rangeMin] = 0x5885;
            mapDataStd [rangeNo] [0xB9D7 - rangeMin] = 0x587D;
            mapDataStd [rangeNo] [0xB9D8 - rangeMin] = 0x58FD;
            mapDataStd [rangeNo] [0xB9D9 - rangeMin] = 0x5925;
            mapDataStd [rangeNo] [0xB9DA - rangeMin] = 0x5922;
            mapDataStd [rangeNo] [0xB9DB - rangeMin] = 0x5924;
            mapDataStd [rangeNo] [0xB9DC - rangeMin] = 0x596A;
            mapDataStd [rangeNo] [0xB9DD - rangeMin] = 0x5969;
            mapDataStd [rangeNo] [0xB9DE - rangeMin] = 0x5AE1;
            mapDataStd [rangeNo] [0xB9DF - rangeMin] = 0x5AE6;

            mapDataStd [rangeNo] [0xB9E0 - rangeMin] = 0x5AE9;
            mapDataStd [rangeNo] [0xB9E1 - rangeMin] = 0x5AD7;
            mapDataStd [rangeNo] [0xB9E2 - rangeMin] = 0x5AD6;
            mapDataStd [rangeNo] [0xB9E3 - rangeMin] = 0x5AD8;
            mapDataStd [rangeNo] [0xB9E4 - rangeMin] = 0x5AE3;
            mapDataStd [rangeNo] [0xB9E5 - rangeMin] = 0x5B75;
            mapDataStd [rangeNo] [0xB9E6 - rangeMin] = 0x5BDE;
            mapDataStd [rangeNo] [0xB9E7 - rangeMin] = 0x5BE7;
            mapDataStd [rangeNo] [0xB9E8 - rangeMin] = 0x5BE1;
            mapDataStd [rangeNo] [0xB9E9 - rangeMin] = 0x5BE5;
            mapDataStd [rangeNo] [0xB9EA - rangeMin] = 0x5BE6;
            mapDataStd [rangeNo] [0xB9EB - rangeMin] = 0x5BE8;
            mapDataStd [rangeNo] [0xB9EC - rangeMin] = 0x5BE2;
            mapDataStd [rangeNo] [0xB9ED - rangeMin] = 0x5BE4;
            mapDataStd [rangeNo] [0xB9EE - rangeMin] = 0x5BDF;
            mapDataStd [rangeNo] [0xB9EF - rangeMin] = 0x5C0D;

            mapDataStd [rangeNo] [0xB9F0 - rangeMin] = 0x5C62;
            mapDataStd [rangeNo] [0xB9F1 - rangeMin] = 0x5D84;
            mapDataStd [rangeNo] [0xB9F2 - rangeMin] = 0x5D87;
            mapDataStd [rangeNo] [0xB9F3 - rangeMin] = 0x5E5B;
            mapDataStd [rangeNo] [0xB9F4 - rangeMin] = 0x5E63;
            mapDataStd [rangeNo] [0xB9F5 - rangeMin] = 0x5E55;
            mapDataStd [rangeNo] [0xB9F6 - rangeMin] = 0x5E57;
            mapDataStd [rangeNo] [0xB9F7 - rangeMin] = 0x5E54;
            mapDataStd [rangeNo] [0xB9F8 - rangeMin] = 0x5ED3;
            mapDataStd [rangeNo] [0xB9F9 - rangeMin] = 0x5ED6;
            mapDataStd [rangeNo] [0xB9FA - rangeMin] = 0x5F0A;
            mapDataStd [rangeNo] [0xB9FB - rangeMin] = 0x5F46;
            mapDataStd [rangeNo] [0xB9FC - rangeMin] = 0x5F70;
            mapDataStd [rangeNo] [0xB9FD - rangeMin] = 0x5FB9;
            mapDataStd [rangeNo] [0xB9FE - rangeMin] = 0x6147;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xBA40 - rangeMin] = 0x613F;
            mapDataStd [rangeNo] [0xBA41 - rangeMin] = 0x614B;
            mapDataStd [rangeNo] [0xBA42 - rangeMin] = 0x6177;
            mapDataStd [rangeNo] [0xBA43 - rangeMin] = 0x6162;
            mapDataStd [rangeNo] [0xBA44 - rangeMin] = 0x6163;
            mapDataStd [rangeNo] [0xBA45 - rangeMin] = 0x615F;
            mapDataStd [rangeNo] [0xBA46 - rangeMin] = 0x615A;
            mapDataStd [rangeNo] [0xBA47 - rangeMin] = 0x6158;
            mapDataStd [rangeNo] [0xBA48 - rangeMin] = 0x6175;
            mapDataStd [rangeNo] [0xBA49 - rangeMin] = 0x622A;
            mapDataStd [rangeNo] [0xBA4A - rangeMin] = 0x6487;
            mapDataStd [rangeNo] [0xBA4B - rangeMin] = 0x6458;
            mapDataStd [rangeNo] [0xBA4C - rangeMin] = 0x6454;
            mapDataStd [rangeNo] [0xBA4D - rangeMin] = 0x64A4;
            mapDataStd [rangeNo] [0xBA4E - rangeMin] = 0x6478;
            mapDataStd [rangeNo] [0xBA4F - rangeMin] = 0x645F;

            mapDataStd [rangeNo] [0xBA50 - rangeMin] = 0x647A;
            mapDataStd [rangeNo] [0xBA51 - rangeMin] = 0x6451;
            mapDataStd [rangeNo] [0xBA52 - rangeMin] = 0x6467;
            mapDataStd [rangeNo] [0xBA53 - rangeMin] = 0x6434;
            mapDataStd [rangeNo] [0xBA54 - rangeMin] = 0x646D;
            mapDataStd [rangeNo] [0xBA55 - rangeMin] = 0x647B;
            mapDataStd [rangeNo] [0xBA56 - rangeMin] = 0x6572;
            mapDataStd [rangeNo] [0xBA57 - rangeMin] = 0x65A1;
            mapDataStd [rangeNo] [0xBA58 - rangeMin] = 0x65D7;
            mapDataStd [rangeNo] [0xBA59 - rangeMin] = 0x65D6;
            mapDataStd [rangeNo] [0xBA5A - rangeMin] = 0x66A2;
            mapDataStd [rangeNo] [0xBA5B - rangeMin] = 0x66A8;
            mapDataStd [rangeNo] [0xBA5C - rangeMin] = 0x669D;
            mapDataStd [rangeNo] [0xBA5D - rangeMin] = 0x699C;
            mapDataStd [rangeNo] [0xBA5E - rangeMin] = 0x69A8;
            mapDataStd [rangeNo] [0xBA5F - rangeMin] = 0x6995;

            mapDataStd [rangeNo] [0xBA60 - rangeMin] = 0x69C1;
            mapDataStd [rangeNo] [0xBA61 - rangeMin] = 0x69AE;
            mapDataStd [rangeNo] [0xBA62 - rangeMin] = 0x69D3;
            mapDataStd [rangeNo] [0xBA63 - rangeMin] = 0x69CB;
            mapDataStd [rangeNo] [0xBA64 - rangeMin] = 0x699B;
            mapDataStd [rangeNo] [0xBA65 - rangeMin] = 0x69B7;
            mapDataStd [rangeNo] [0xBA66 - rangeMin] = 0x69BB;
            mapDataStd [rangeNo] [0xBA67 - rangeMin] = 0x69AB;
            mapDataStd [rangeNo] [0xBA68 - rangeMin] = 0x69B4;
            mapDataStd [rangeNo] [0xBA69 - rangeMin] = 0x69D0;
            mapDataStd [rangeNo] [0xBA6A - rangeMin] = 0x69CD;
            mapDataStd [rangeNo] [0xBA6B - rangeMin] = 0x69AD;
            mapDataStd [rangeNo] [0xBA6C - rangeMin] = 0x69CC;
            mapDataStd [rangeNo] [0xBA6D - rangeMin] = 0x69A6;
            mapDataStd [rangeNo] [0xBA6E - rangeMin] = 0x69C3;
            mapDataStd [rangeNo] [0xBA6F - rangeMin] = 0x69A3;

            mapDataStd [rangeNo] [0xBA70 - rangeMin] = 0x6B49;
            mapDataStd [rangeNo] [0xBA71 - rangeMin] = 0x6B4C;
            mapDataStd [rangeNo] [0xBA72 - rangeMin] = 0x6C33;
            mapDataStd [rangeNo] [0xBA73 - rangeMin] = 0x6F33;
            mapDataStd [rangeNo] [0xBA74 - rangeMin] = 0x6F14;
            mapDataStd [rangeNo] [0xBA75 - rangeMin] = 0x6EFE;
            mapDataStd [rangeNo] [0xBA76 - rangeMin] = 0x6F13;
            mapDataStd [rangeNo] [0xBA77 - rangeMin] = 0x6EF4;
            mapDataStd [rangeNo] [0xBA78 - rangeMin] = 0x6F29;
            mapDataStd [rangeNo] [0xBA79 - rangeMin] = 0x6F3E;
            mapDataStd [rangeNo] [0xBA7A - rangeMin] = 0x6F20;
            mapDataStd [rangeNo] [0xBA7B - rangeMin] = 0x6F2C;
            mapDataStd [rangeNo] [0xBA7C - rangeMin] = 0x6F0F;
            mapDataStd [rangeNo] [0xBA7D - rangeMin] = 0x6F02;
            mapDataStd [rangeNo] [0xBA7E - rangeMin] = 0x6F22;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xBAA1 - rangeMin] = 0x6EFF;
            mapDataStd [rangeNo] [0xBAA2 - rangeMin] = 0x6EEF;
            mapDataStd [rangeNo] [0xBAA3 - rangeMin] = 0x6F06;
            mapDataStd [rangeNo] [0xBAA4 - rangeMin] = 0x6F31;
            mapDataStd [rangeNo] [0xBAA5 - rangeMin] = 0x6F38;
            mapDataStd [rangeNo] [0xBAA6 - rangeMin] = 0x6F32;
            mapDataStd [rangeNo] [0xBAA7 - rangeMin] = 0x6F23;
            mapDataStd [rangeNo] [0xBAA8 - rangeMin] = 0x6F15;
            mapDataStd [rangeNo] [0xBAA9 - rangeMin] = 0x6F2B;
            mapDataStd [rangeNo] [0xBAAA - rangeMin] = 0x6F2F;
            mapDataStd [rangeNo] [0xBAAB - rangeMin] = 0x6F88;
            mapDataStd [rangeNo] [0xBAAC - rangeMin] = 0x6F2A;
            mapDataStd [rangeNo] [0xBAAD - rangeMin] = 0x6EEC;
            mapDataStd [rangeNo] [0xBAAE - rangeMin] = 0x6F01;
            mapDataStd [rangeNo] [0xBAAF - rangeMin] = 0x6EF2;

            mapDataStd [rangeNo] [0xBAB0 - rangeMin] = 0x6ECC;
            mapDataStd [rangeNo] [0xBAB1 - rangeMin] = 0x6EF7;
            mapDataStd [rangeNo] [0xBAB2 - rangeMin] = 0x7194;
            mapDataStd [rangeNo] [0xBAB3 - rangeMin] = 0x7199;
            mapDataStd [rangeNo] [0xBAB4 - rangeMin] = 0x717D;
            mapDataStd [rangeNo] [0xBAB5 - rangeMin] = 0x718A;
            mapDataStd [rangeNo] [0xBAB6 - rangeMin] = 0x7184;
            mapDataStd [rangeNo] [0xBAB7 - rangeMin] = 0x7192;
            mapDataStd [rangeNo] [0xBAB8 - rangeMin] = 0x723E;
            mapDataStd [rangeNo] [0xBAB9 - rangeMin] = 0x7292;
            mapDataStd [rangeNo] [0xBABA - rangeMin] = 0x7296;
            mapDataStd [rangeNo] [0xBABB - rangeMin] = 0x7344;
            mapDataStd [rangeNo] [0xBABC - rangeMin] = 0x7350;
            mapDataStd [rangeNo] [0xBABD - rangeMin] = 0x7464;
            mapDataStd [rangeNo] [0xBABE - rangeMin] = 0x7463;
            mapDataStd [rangeNo] [0xBABF - rangeMin] = 0x746A;

            mapDataStd [rangeNo] [0xBAC0 - rangeMin] = 0x7470;
            mapDataStd [rangeNo] [0xBAC1 - rangeMin] = 0x746D;
            mapDataStd [rangeNo] [0xBAC2 - rangeMin] = 0x7504;
            mapDataStd [rangeNo] [0xBAC3 - rangeMin] = 0x7591;
            mapDataStd [rangeNo] [0xBAC4 - rangeMin] = 0x7627;
            mapDataStd [rangeNo] [0xBAC5 - rangeMin] = 0x760D;
            mapDataStd [rangeNo] [0xBAC6 - rangeMin] = 0x760B;
            mapDataStd [rangeNo] [0xBAC7 - rangeMin] = 0x7609;
            mapDataStd [rangeNo] [0xBAC8 - rangeMin] = 0x7613;
            mapDataStd [rangeNo] [0xBAC9 - rangeMin] = 0x76E1;
            mapDataStd [rangeNo] [0xBACA - rangeMin] = 0x76E3;
            mapDataStd [rangeNo] [0xBACB - rangeMin] = 0x7784;
            mapDataStd [rangeNo] [0xBACC - rangeMin] = 0x777D;
            mapDataStd [rangeNo] [0xBACD - rangeMin] = 0x777F;
            mapDataStd [rangeNo] [0xBACE - rangeMin] = 0x7761;
            mapDataStd [rangeNo] [0xBACF - rangeMin] = 0x78C1;

            mapDataStd [rangeNo] [0xBAD0 - rangeMin] = 0x789F;
            mapDataStd [rangeNo] [0xBAD1 - rangeMin] = 0x78A7;
            mapDataStd [rangeNo] [0xBAD2 - rangeMin] = 0x78B3;
            mapDataStd [rangeNo] [0xBAD3 - rangeMin] = 0x78A9;
            mapDataStd [rangeNo] [0xBAD4 - rangeMin] = 0x78A3;
            mapDataStd [rangeNo] [0xBAD5 - rangeMin] = 0x798E;
            mapDataStd [rangeNo] [0xBAD6 - rangeMin] = 0x798F;
            mapDataStd [rangeNo] [0xBAD7 - rangeMin] = 0x798D;
            mapDataStd [rangeNo] [0xBAD8 - rangeMin] = 0x7A2E;
            mapDataStd [rangeNo] [0xBAD9 - rangeMin] = 0x7A31;
            mapDataStd [rangeNo] [0xBADA - rangeMin] = 0x7AAA;
            mapDataStd [rangeNo] [0xBADB - rangeMin] = 0x7AA9;
            mapDataStd [rangeNo] [0xBADC - rangeMin] = 0x7AED;
            mapDataStd [rangeNo] [0xBADD - rangeMin] = 0x7AEF;
            mapDataStd [rangeNo] [0xBADE - rangeMin] = 0x7BA1;
            mapDataStd [rangeNo] [0xBADF - rangeMin] = 0x7B95;

            mapDataStd [rangeNo] [0xBAE0 - rangeMin] = 0x7B8B;
            mapDataStd [rangeNo] [0xBAE1 - rangeMin] = 0x7B75;
            mapDataStd [rangeNo] [0xBAE2 - rangeMin] = 0x7B97;
            mapDataStd [rangeNo] [0xBAE3 - rangeMin] = 0x7B9D;
            mapDataStd [rangeNo] [0xBAE4 - rangeMin] = 0x7B94;
            mapDataStd [rangeNo] [0xBAE5 - rangeMin] = 0x7B8F;
            mapDataStd [rangeNo] [0xBAE6 - rangeMin] = 0x7BB8;
            mapDataStd [rangeNo] [0xBAE7 - rangeMin] = 0x7B87;
            mapDataStd [rangeNo] [0xBAE8 - rangeMin] = 0x7B84;
            mapDataStd [rangeNo] [0xBAE9 - rangeMin] = 0x7CB9;
            mapDataStd [rangeNo] [0xBAEA - rangeMin] = 0x7CBD;
            mapDataStd [rangeNo] [0xBAEB - rangeMin] = 0x7CBE;
            mapDataStd [rangeNo] [0xBAEC - rangeMin] = 0x7DBB;
            mapDataStd [rangeNo] [0xBAED - rangeMin] = 0x7DB0;
            mapDataStd [rangeNo] [0xBAEE - rangeMin] = 0x7D9C;
            mapDataStd [rangeNo] [0xBAEF - rangeMin] = 0x7DBD;

            mapDataStd [rangeNo] [0xBAF0 - rangeMin] = 0x7DBE;
            mapDataStd [rangeNo] [0xBAF1 - rangeMin] = 0x7DA0;
            mapDataStd [rangeNo] [0xBAF2 - rangeMin] = 0x7DCA;
            mapDataStd [rangeNo] [0xBAF3 - rangeMin] = 0x7DB4;
            mapDataStd [rangeNo] [0xBAF4 - rangeMin] = 0x7DB2;
            mapDataStd [rangeNo] [0xBAF5 - rangeMin] = 0x7DB1;
            mapDataStd [rangeNo] [0xBAF6 - rangeMin] = 0x7DBA;
            mapDataStd [rangeNo] [0xBAF7 - rangeMin] = 0x7DA2;
            mapDataStd [rangeNo] [0xBAF8 - rangeMin] = 0x7DBF;
            mapDataStd [rangeNo] [0xBAF9 - rangeMin] = 0x7DB5;
            mapDataStd [rangeNo] [0xBAFA - rangeMin] = 0x7DB8;
            mapDataStd [rangeNo] [0xBAFB - rangeMin] = 0x7DAD;
            mapDataStd [rangeNo] [0xBAFC - rangeMin] = 0x7DD2;
            mapDataStd [rangeNo] [0xBAFD - rangeMin] = 0x7DC7;
            mapDataStd [rangeNo] [0xBAFE - rangeMin] = 0x7DAC;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xBB40 - rangeMin] = 0x7F70;
            mapDataStd [rangeNo] [0xBB41 - rangeMin] = 0x7FE0;
            mapDataStd [rangeNo] [0xBB42 - rangeMin] = 0x7FE1;
            mapDataStd [rangeNo] [0xBB43 - rangeMin] = 0x7FDF;
            mapDataStd [rangeNo] [0xBB44 - rangeMin] = 0x805E;
            mapDataStd [rangeNo] [0xBB45 - rangeMin] = 0x805A;
            mapDataStd [rangeNo] [0xBB46 - rangeMin] = 0x8087;
            mapDataStd [rangeNo] [0xBB47 - rangeMin] = 0x8150;
            mapDataStd [rangeNo] [0xBB48 - rangeMin] = 0x8180;
            mapDataStd [rangeNo] [0xBB49 - rangeMin] = 0x818F;
            mapDataStd [rangeNo] [0xBB4A - rangeMin] = 0x8188;
            mapDataStd [rangeNo] [0xBB4B - rangeMin] = 0x818A;
            mapDataStd [rangeNo] [0xBB4C - rangeMin] = 0x817F;
            mapDataStd [rangeNo] [0xBB4D - rangeMin] = 0x8182;
            mapDataStd [rangeNo] [0xBB4E - rangeMin] = 0x81E7;
            mapDataStd [rangeNo] [0xBB4F - rangeMin] = 0x81FA;

            mapDataStd [rangeNo] [0xBB50 - rangeMin] = 0x8207;
            mapDataStd [rangeNo] [0xBB51 - rangeMin] = 0x8214;
            mapDataStd [rangeNo] [0xBB52 - rangeMin] = 0x821E;
            mapDataStd [rangeNo] [0xBB53 - rangeMin] = 0x824B;
            mapDataStd [rangeNo] [0xBB54 - rangeMin] = 0x84C9;
            mapDataStd [rangeNo] [0xBB55 - rangeMin] = 0x84BF;
            mapDataStd [rangeNo] [0xBB56 - rangeMin] = 0x84C6;
            mapDataStd [rangeNo] [0xBB57 - rangeMin] = 0x84C4;
            mapDataStd [rangeNo] [0xBB58 - rangeMin] = 0x8499;
            mapDataStd [rangeNo] [0xBB59 - rangeMin] = 0x849E;
            mapDataStd [rangeNo] [0xBB5A - rangeMin] = 0x84B2;
            mapDataStd [rangeNo] [0xBB5B - rangeMin] = 0x849C;
            mapDataStd [rangeNo] [0xBB5C - rangeMin] = 0x84CB;
            mapDataStd [rangeNo] [0xBB5D - rangeMin] = 0x84B8;
            mapDataStd [rangeNo] [0xBB5E - rangeMin] = 0x84C0;
            mapDataStd [rangeNo] [0xBB5F - rangeMin] = 0x84D3;

            mapDataStd [rangeNo] [0xBB60 - rangeMin] = 0x8490;
            mapDataStd [rangeNo] [0xBB61 - rangeMin] = 0x84BC;
            mapDataStd [rangeNo] [0xBB62 - rangeMin] = 0x84D1;
            mapDataStd [rangeNo] [0xBB63 - rangeMin] = 0x84CA;
            mapDataStd [rangeNo] [0xBB64 - rangeMin] = 0x873F;
            mapDataStd [rangeNo] [0xBB65 - rangeMin] = 0x871C;
            mapDataStd [rangeNo] [0xBB66 - rangeMin] = 0x873B;
            mapDataStd [rangeNo] [0xBB67 - rangeMin] = 0x8722;
            mapDataStd [rangeNo] [0xBB68 - rangeMin] = 0x8725;
            mapDataStd [rangeNo] [0xBB69 - rangeMin] = 0x8734;
            mapDataStd [rangeNo] [0xBB6A - rangeMin] = 0x8718;
            mapDataStd [rangeNo] [0xBB6B - rangeMin] = 0x8755;
            mapDataStd [rangeNo] [0xBB6C - rangeMin] = 0x8737;
            mapDataStd [rangeNo] [0xBB6D - rangeMin] = 0x8729;
            mapDataStd [rangeNo] [0xBB6E - rangeMin] = 0x88F3;
            mapDataStd [rangeNo] [0xBB6F - rangeMin] = 0x8902;

            mapDataStd [rangeNo] [0xBB70 - rangeMin] = 0x88F4;
            mapDataStd [rangeNo] [0xBB71 - rangeMin] = 0x88F9;
            mapDataStd [rangeNo] [0xBB72 - rangeMin] = 0x88F8;
            mapDataStd [rangeNo] [0xBB73 - rangeMin] = 0x88FD;
            mapDataStd [rangeNo] [0xBB74 - rangeMin] = 0x88E8;
            mapDataStd [rangeNo] [0xBB75 - rangeMin] = 0x891A;
            mapDataStd [rangeNo] [0xBB76 - rangeMin] = 0x88EF;
            mapDataStd [rangeNo] [0xBB77 - rangeMin] = 0x8AA6;
            mapDataStd [rangeNo] [0xBB78 - rangeMin] = 0x8A8C;
            mapDataStd [rangeNo] [0xBB79 - rangeMin] = 0x8A9E;
            mapDataStd [rangeNo] [0xBB7A - rangeMin] = 0x8AA3;
            mapDataStd [rangeNo] [0xBB7B - rangeMin] = 0x8A8D;
            mapDataStd [rangeNo] [0xBB7C - rangeMin] = 0x8AA1;
            mapDataStd [rangeNo] [0xBB7D - rangeMin] = 0x8A93;
            mapDataStd [rangeNo] [0xBB7E - rangeMin] = 0x8AA4;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xBBA1 - rangeMin] = 0x8AAA;
            mapDataStd [rangeNo] [0xBBA2 - rangeMin] = 0x8AA5;
            mapDataStd [rangeNo] [0xBBA3 - rangeMin] = 0x8AA8;
            mapDataStd [rangeNo] [0xBBA4 - rangeMin] = 0x8A98;
            mapDataStd [rangeNo] [0xBBA5 - rangeMin] = 0x8A91;
            mapDataStd [rangeNo] [0xBBA6 - rangeMin] = 0x8A9A;
            mapDataStd [rangeNo] [0xBBA7 - rangeMin] = 0x8AA7;
            mapDataStd [rangeNo] [0xBBA8 - rangeMin] = 0x8C6A;
            mapDataStd [rangeNo] [0xBBA9 - rangeMin] = 0x8C8D;
            mapDataStd [rangeNo] [0xBBAA - rangeMin] = 0x8C8C;
            mapDataStd [rangeNo] [0xBBAB - rangeMin] = 0x8CD3;
            mapDataStd [rangeNo] [0xBBAC - rangeMin] = 0x8CD1;
            mapDataStd [rangeNo] [0xBBAD - rangeMin] = 0x8CD2;
            mapDataStd [rangeNo] [0xBBAE - rangeMin] = 0x8D6B;
            mapDataStd [rangeNo] [0xBBAF - rangeMin] = 0x8D99;

            mapDataStd [rangeNo] [0xBBB0 - rangeMin] = 0x8D95;
            mapDataStd [rangeNo] [0xBBB1 - rangeMin] = 0x8DFC;
            mapDataStd [rangeNo] [0xBBB2 - rangeMin] = 0x8F14;
            mapDataStd [rangeNo] [0xBBB3 - rangeMin] = 0x8F12;
            mapDataStd [rangeNo] [0xBBB4 - rangeMin] = 0x8F15;
            mapDataStd [rangeNo] [0xBBB5 - rangeMin] = 0x8F13;
            mapDataStd [rangeNo] [0xBBB6 - rangeMin] = 0x8FA3;
            mapDataStd [rangeNo] [0xBBB7 - rangeMin] = 0x9060;
            mapDataStd [rangeNo] [0xBBB8 - rangeMin] = 0x9058;
            mapDataStd [rangeNo] [0xBBB9 - rangeMin] = 0x905C;
            mapDataStd [rangeNo] [0xBBBA - rangeMin] = 0x9063;
            mapDataStd [rangeNo] [0xBBBB - rangeMin] = 0x9059;
            mapDataStd [rangeNo] [0xBBBC - rangeMin] = 0x905E;
            mapDataStd [rangeNo] [0xBBBD - rangeMin] = 0x9062;
            mapDataStd [rangeNo] [0xBBBE - rangeMin] = 0x905D;
            mapDataStd [rangeNo] [0xBBBF - rangeMin] = 0x905B;

            mapDataStd [rangeNo] [0xBBC0 - rangeMin] = 0x9119;
            mapDataStd [rangeNo] [0xBBC1 - rangeMin] = 0x9118;
            mapDataStd [rangeNo] [0xBBC2 - rangeMin] = 0x911E;
            mapDataStd [rangeNo] [0xBBC3 - rangeMin] = 0x9175;
            mapDataStd [rangeNo] [0xBBC4 - rangeMin] = 0x9178;
            mapDataStd [rangeNo] [0xBBC5 - rangeMin] = 0x9177;
            mapDataStd [rangeNo] [0xBBC6 - rangeMin] = 0x9174;
            mapDataStd [rangeNo] [0xBBC7 - rangeMin] = 0x9278;
            mapDataStd [rangeNo] [0xBBC8 - rangeMin] = 0x9280;
            mapDataStd [rangeNo] [0xBBC9 - rangeMin] = 0x9285;
            mapDataStd [rangeNo] [0xBBCA - rangeMin] = 0x9298;
            mapDataStd [rangeNo] [0xBBCB - rangeMin] = 0x9296;
            mapDataStd [rangeNo] [0xBBCC - rangeMin] = 0x927B;
            mapDataStd [rangeNo] [0xBBCD - rangeMin] = 0x9293;
            mapDataStd [rangeNo] [0xBBCE - rangeMin] = 0x929C;
            mapDataStd [rangeNo] [0xBBCF - rangeMin] = 0x92A8;

            mapDataStd [rangeNo] [0xBBD0 - rangeMin] = 0x927C;
            mapDataStd [rangeNo] [0xBBD1 - rangeMin] = 0x9291;
            mapDataStd [rangeNo] [0xBBD2 - rangeMin] = 0x95A1;
            mapDataStd [rangeNo] [0xBBD3 - rangeMin] = 0x95A8;
            mapDataStd [rangeNo] [0xBBD4 - rangeMin] = 0x95A9;
            mapDataStd [rangeNo] [0xBBD5 - rangeMin] = 0x95A3;
            mapDataStd [rangeNo] [0xBBD6 - rangeMin] = 0x95A5;
            mapDataStd [rangeNo] [0xBBD7 - rangeMin] = 0x95A4;
            mapDataStd [rangeNo] [0xBBD8 - rangeMin] = 0x9699;
            mapDataStd [rangeNo] [0xBBD9 - rangeMin] = 0x969C;
            mapDataStd [rangeNo] [0xBBDA - rangeMin] = 0x969B;
            mapDataStd [rangeNo] [0xBBDB - rangeMin] = 0x96CC;
            mapDataStd [rangeNo] [0xBBDC - rangeMin] = 0x96D2;
            mapDataStd [rangeNo] [0xBBDD - rangeMin] = 0x9700;
            mapDataStd [rangeNo] [0xBBDE - rangeMin] = 0x977C;
            mapDataStd [rangeNo] [0xBBDF - rangeMin] = 0x9785;

            mapDataStd [rangeNo] [0xBBE0 - rangeMin] = 0x97F6;
            mapDataStd [rangeNo] [0xBBE1 - rangeMin] = 0x9817;
            mapDataStd [rangeNo] [0xBBE2 - rangeMin] = 0x9818;
            mapDataStd [rangeNo] [0xBBE3 - rangeMin] = 0x98AF;
            mapDataStd [rangeNo] [0xBBE4 - rangeMin] = 0x98B1;
            mapDataStd [rangeNo] [0xBBE5 - rangeMin] = 0x9903;
            mapDataStd [rangeNo] [0xBBE6 - rangeMin] = 0x9905;
            mapDataStd [rangeNo] [0xBBE7 - rangeMin] = 0x990C;
            mapDataStd [rangeNo] [0xBBE8 - rangeMin] = 0x9909;
            mapDataStd [rangeNo] [0xBBE9 - rangeMin] = 0x99C1;
            mapDataStd [rangeNo] [0xBBEA - rangeMin] = 0x9AAF;
            mapDataStd [rangeNo] [0xBBEB - rangeMin] = 0x9AB0;
            mapDataStd [rangeNo] [0xBBEC - rangeMin] = 0x9AE6;
            mapDataStd [rangeNo] [0xBBED - rangeMin] = 0x9B41;
            mapDataStd [rangeNo] [0xBBEE - rangeMin] = 0x9B42;
            mapDataStd [rangeNo] [0xBBEF - rangeMin] = 0x9CF4;

            mapDataStd [rangeNo] [0xBBF0 - rangeMin] = 0x9CF6;
            mapDataStd [rangeNo] [0xBBF1 - rangeMin] = 0x9CF3;
            mapDataStd [rangeNo] [0xBBF2 - rangeMin] = 0x9EBC;
            mapDataStd [rangeNo] [0xBBF3 - rangeMin] = 0x9F3B;
            mapDataStd [rangeNo] [0xBBF4 - rangeMin] = 0x9F4A;
            mapDataStd [rangeNo] [0xBBF5 - rangeMin] = 0x5104;
            mapDataStd [rangeNo] [0xBBF6 - rangeMin] = 0x5100;
            mapDataStd [rangeNo] [0xBBF7 - rangeMin] = 0x50FB;
            mapDataStd [rangeNo] [0xBBF8 - rangeMin] = 0x50F5;
            mapDataStd [rangeNo] [0xBBF9 - rangeMin] = 0x50F9;
            mapDataStd [rangeNo] [0xBBFA - rangeMin] = 0x5102;
            mapDataStd [rangeNo] [0xBBFB - rangeMin] = 0x5108;
            mapDataStd [rangeNo] [0xBBFC - rangeMin] = 0x5109;
            mapDataStd [rangeNo] [0xBBFD - rangeMin] = 0x5105;
            mapDataStd [rangeNo] [0xBBFE - rangeMin] = 0x51DC;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xBC40 - rangeMin] = 0x5287;
            mapDataStd [rangeNo] [0xBC41 - rangeMin] = 0x5288;
            mapDataStd [rangeNo] [0xBC42 - rangeMin] = 0x5289;
            mapDataStd [rangeNo] [0xBC43 - rangeMin] = 0x528D;
            mapDataStd [rangeNo] [0xBC44 - rangeMin] = 0x528A;
            mapDataStd [rangeNo] [0xBC45 - rangeMin] = 0x52F0;
            mapDataStd [rangeNo] [0xBC46 - rangeMin] = 0x53B2;
            mapDataStd [rangeNo] [0xBC47 - rangeMin] = 0x562E;
            mapDataStd [rangeNo] [0xBC48 - rangeMin] = 0x563B;
            mapDataStd [rangeNo] [0xBC49 - rangeMin] = 0x5639;
            mapDataStd [rangeNo] [0xBC4A - rangeMin] = 0x5632;
            mapDataStd [rangeNo] [0xBC4B - rangeMin] = 0x563F;
            mapDataStd [rangeNo] [0xBC4C - rangeMin] = 0x5634;
            mapDataStd [rangeNo] [0xBC4D - rangeMin] = 0x5629;
            mapDataStd [rangeNo] [0xBC4E - rangeMin] = 0x5653;
            mapDataStd [rangeNo] [0xBC4F - rangeMin] = 0x564E;

            mapDataStd [rangeNo] [0xBC50 - rangeMin] = 0x5657;
            mapDataStd [rangeNo] [0xBC51 - rangeMin] = 0x5674;
            mapDataStd [rangeNo] [0xBC52 - rangeMin] = 0x5636;
            mapDataStd [rangeNo] [0xBC53 - rangeMin] = 0x562F;
            mapDataStd [rangeNo] [0xBC54 - rangeMin] = 0x5630;
            mapDataStd [rangeNo] [0xBC55 - rangeMin] = 0x5880;
            mapDataStd [rangeNo] [0xBC56 - rangeMin] = 0x589F;
            mapDataStd [rangeNo] [0xBC57 - rangeMin] = 0x589E;
            mapDataStd [rangeNo] [0xBC58 - rangeMin] = 0x58B3;
            mapDataStd [rangeNo] [0xBC59 - rangeMin] = 0x589C;
            mapDataStd [rangeNo] [0xBC5A - rangeMin] = 0x58AE;
            mapDataStd [rangeNo] [0xBC5B - rangeMin] = 0x58A9;
            mapDataStd [rangeNo] [0xBC5C - rangeMin] = 0x58A6;
            mapDataStd [rangeNo] [0xBC5D - rangeMin] = 0x596D;
            mapDataStd [rangeNo] [0xBC5E - rangeMin] = 0x5B09;
            mapDataStd [rangeNo] [0xBC5F - rangeMin] = 0x5AFB;

            mapDataStd [rangeNo] [0xBC60 - rangeMin] = 0x5B0B;
            mapDataStd [rangeNo] [0xBC61 - rangeMin] = 0x5AF5;
            mapDataStd [rangeNo] [0xBC62 - rangeMin] = 0x5B0C;
            mapDataStd [rangeNo] [0xBC63 - rangeMin] = 0x5B08;
            mapDataStd [rangeNo] [0xBC64 - rangeMin] = 0x5BEE;
            mapDataStd [rangeNo] [0xBC65 - rangeMin] = 0x5BEC;
            mapDataStd [rangeNo] [0xBC66 - rangeMin] = 0x5BE9;
            mapDataStd [rangeNo] [0xBC67 - rangeMin] = 0x5BEB;
            mapDataStd [rangeNo] [0xBC68 - rangeMin] = 0x5C64;
            mapDataStd [rangeNo] [0xBC69 - rangeMin] = 0x5C65;
            mapDataStd [rangeNo] [0xBC6A - rangeMin] = 0x5D9D;
            mapDataStd [rangeNo] [0xBC6B - rangeMin] = 0x5D94;
            mapDataStd [rangeNo] [0xBC6C - rangeMin] = 0x5E62;
            mapDataStd [rangeNo] [0xBC6D - rangeMin] = 0x5E5F;
            mapDataStd [rangeNo] [0xBC6E - rangeMin] = 0x5E61;
            mapDataStd [rangeNo] [0xBC6F - rangeMin] = 0x5EE2;

            mapDataStd [rangeNo] [0xBC70 - rangeMin] = 0x5EDA;
            mapDataStd [rangeNo] [0xBC71 - rangeMin] = 0x5EDF;
            mapDataStd [rangeNo] [0xBC72 - rangeMin] = 0x5EDD;
            mapDataStd [rangeNo] [0xBC73 - rangeMin] = 0x5EE3;
            mapDataStd [rangeNo] [0xBC74 - rangeMin] = 0x5EE0;
            mapDataStd [rangeNo] [0xBC75 - rangeMin] = 0x5F48;
            mapDataStd [rangeNo] [0xBC76 - rangeMin] = 0x5F71;
            mapDataStd [rangeNo] [0xBC77 - rangeMin] = 0x5FB7;
            mapDataStd [rangeNo] [0xBC78 - rangeMin] = 0x5FB5;
            mapDataStd [rangeNo] [0xBC79 - rangeMin] = 0x6176;
            mapDataStd [rangeNo] [0xBC7A - rangeMin] = 0x6167;
            mapDataStd [rangeNo] [0xBC7B - rangeMin] = 0x616E;
            mapDataStd [rangeNo] [0xBC7C - rangeMin] = 0x615D;
            mapDataStd [rangeNo] [0xBC7D - rangeMin] = 0x6155;
            mapDataStd [rangeNo] [0xBC7E - rangeMin] = 0x6182;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xBCA1 - rangeMin] = 0x617C;
            mapDataStd [rangeNo] [0xBCA2 - rangeMin] = 0x6170;
            mapDataStd [rangeNo] [0xBCA3 - rangeMin] = 0x616B;
            mapDataStd [rangeNo] [0xBCA4 - rangeMin] = 0x617E;
            mapDataStd [rangeNo] [0xBCA5 - rangeMin] = 0x61A7;
            mapDataStd [rangeNo] [0xBCA6 - rangeMin] = 0x6190;
            mapDataStd [rangeNo] [0xBCA7 - rangeMin] = 0x61AB;
            mapDataStd [rangeNo] [0xBCA8 - rangeMin] = 0x618E;
            mapDataStd [rangeNo] [0xBCA9 - rangeMin] = 0x61AC;
            mapDataStd [rangeNo] [0xBCAA - rangeMin] = 0x619A;
            mapDataStd [rangeNo] [0xBCAB - rangeMin] = 0x61A4;
            mapDataStd [rangeNo] [0xBCAC - rangeMin] = 0x6194;
            mapDataStd [rangeNo] [0xBCAD - rangeMin] = 0x61AE;
            mapDataStd [rangeNo] [0xBCAE - rangeMin] = 0x622E;
            mapDataStd [rangeNo] [0xBCAF - rangeMin] = 0x6469;

            mapDataStd [rangeNo] [0xBCB0 - rangeMin] = 0x646F;
            mapDataStd [rangeNo] [0xBCB1 - rangeMin] = 0x6479;
            mapDataStd [rangeNo] [0xBCB2 - rangeMin] = 0x649E;
            mapDataStd [rangeNo] [0xBCB3 - rangeMin] = 0x64B2;
            mapDataStd [rangeNo] [0xBCB4 - rangeMin] = 0x6488;
            mapDataStd [rangeNo] [0xBCB5 - rangeMin] = 0x6490;
            mapDataStd [rangeNo] [0xBCB6 - rangeMin] = 0x64B0;
            mapDataStd [rangeNo] [0xBCB7 - rangeMin] = 0x64A5;
            mapDataStd [rangeNo] [0xBCB8 - rangeMin] = 0x6493;
            mapDataStd [rangeNo] [0xBCB9 - rangeMin] = 0x6495;
            mapDataStd [rangeNo] [0xBCBA - rangeMin] = 0x64A9;
            mapDataStd [rangeNo] [0xBCBB - rangeMin] = 0x6492;
            mapDataStd [rangeNo] [0xBCBC - rangeMin] = 0x64AE;
            mapDataStd [rangeNo] [0xBCBD - rangeMin] = 0x64AD;
            mapDataStd [rangeNo] [0xBCBE - rangeMin] = 0x64AB;
            mapDataStd [rangeNo] [0xBCBF - rangeMin] = 0x649A;

            mapDataStd [rangeNo] [0xBCC0 - rangeMin] = 0x64AC;
            mapDataStd [rangeNo] [0xBCC1 - rangeMin] = 0x6499;
            mapDataStd [rangeNo] [0xBCC2 - rangeMin] = 0x64A2;
            mapDataStd [rangeNo] [0xBCC3 - rangeMin] = 0x64B3;
            mapDataStd [rangeNo] [0xBCC4 - rangeMin] = 0x6575;
            mapDataStd [rangeNo] [0xBCC5 - rangeMin] = 0x6577;
            mapDataStd [rangeNo] [0xBCC6 - rangeMin] = 0x6578;
            mapDataStd [rangeNo] [0xBCC7 - rangeMin] = 0x66AE;
            mapDataStd [rangeNo] [0xBCC8 - rangeMin] = 0x66AB;
            mapDataStd [rangeNo] [0xBCC9 - rangeMin] = 0x66B4;
            mapDataStd [rangeNo] [0xBCCA - rangeMin] = 0x66B1;
            mapDataStd [rangeNo] [0xBCCB - rangeMin] = 0x6A23;
            mapDataStd [rangeNo] [0xBCCC - rangeMin] = 0x6A1F;
            mapDataStd [rangeNo] [0xBCCD - rangeMin] = 0x69E8;
            mapDataStd [rangeNo] [0xBCCE - rangeMin] = 0x6A01;
            mapDataStd [rangeNo] [0xBCCF - rangeMin] = 0x6A1E;

            mapDataStd [rangeNo] [0xBCD0 - rangeMin] = 0x6A19;
            mapDataStd [rangeNo] [0xBCD1 - rangeMin] = 0x69FD;
            mapDataStd [rangeNo] [0xBCD2 - rangeMin] = 0x6A21;
            mapDataStd [rangeNo] [0xBCD3 - rangeMin] = 0x6A13;
            mapDataStd [rangeNo] [0xBCD4 - rangeMin] = 0x6A0A;
            mapDataStd [rangeNo] [0xBCD5 - rangeMin] = 0x69F3;
            mapDataStd [rangeNo] [0xBCD6 - rangeMin] = 0x6A02;
            mapDataStd [rangeNo] [0xBCD7 - rangeMin] = 0x6A05;
            mapDataStd [rangeNo] [0xBCD8 - rangeMin] = 0x69ED;
            mapDataStd [rangeNo] [0xBCD9 - rangeMin] = 0x6A11;
            mapDataStd [rangeNo] [0xBCDA - rangeMin] = 0x6B50;
            mapDataStd [rangeNo] [0xBCDB - rangeMin] = 0x6B4E;
            mapDataStd [rangeNo] [0xBCDC - rangeMin] = 0x6BA4;
            mapDataStd [rangeNo] [0xBCDD - rangeMin] = 0x6BC5;
            mapDataStd [rangeNo] [0xBCDE - rangeMin] = 0x6BC6;
            mapDataStd [rangeNo] [0xBCDF - rangeMin] = 0x6F3F;

            mapDataStd [rangeNo] [0xBCE0 - rangeMin] = 0x6F7C;
            mapDataStd [rangeNo] [0xBCE1 - rangeMin] = 0x6F84;
            mapDataStd [rangeNo] [0xBCE2 - rangeMin] = 0x6F51;
            mapDataStd [rangeNo] [0xBCE3 - rangeMin] = 0x6F66;
            mapDataStd [rangeNo] [0xBCE4 - rangeMin] = 0x6F54;
            mapDataStd [rangeNo] [0xBCE5 - rangeMin] = 0x6F86;
            mapDataStd [rangeNo] [0xBCE6 - rangeMin] = 0x6F6D;
            mapDataStd [rangeNo] [0xBCE7 - rangeMin] = 0x6F5B;
            mapDataStd [rangeNo] [0xBCE8 - rangeMin] = 0x6F78;
            mapDataStd [rangeNo] [0xBCE9 - rangeMin] = 0x6F6E;
            mapDataStd [rangeNo] [0xBCEA - rangeMin] = 0x6F8E;
            mapDataStd [rangeNo] [0xBCEB - rangeMin] = 0x6F7A;
            mapDataStd [rangeNo] [0xBCEC - rangeMin] = 0x6F70;
            mapDataStd [rangeNo] [0xBCED - rangeMin] = 0x6F64;
            mapDataStd [rangeNo] [0xBCEE - rangeMin] = 0x6F97;
            mapDataStd [rangeNo] [0xBCEF - rangeMin] = 0x6F58;

            mapDataStd [rangeNo] [0xBCF0 - rangeMin] = 0x6ED5;
            mapDataStd [rangeNo] [0xBCF1 - rangeMin] = 0x6F6F;
            mapDataStd [rangeNo] [0xBCF2 - rangeMin] = 0x6F60;
            mapDataStd [rangeNo] [0xBCF3 - rangeMin] = 0x6F5F;
            mapDataStd [rangeNo] [0xBCF4 - rangeMin] = 0x719F;
            mapDataStd [rangeNo] [0xBCF5 - rangeMin] = 0x71AC;
            mapDataStd [rangeNo] [0xBCF6 - rangeMin] = 0x71B1;
            mapDataStd [rangeNo] [0xBCF7 - rangeMin] = 0x71A8;
            mapDataStd [rangeNo] [0xBCF8 - rangeMin] = 0x7256;
            mapDataStd [rangeNo] [0xBCF9 - rangeMin] = 0x729B;
            mapDataStd [rangeNo] [0xBCFA - rangeMin] = 0x734E;
            mapDataStd [rangeNo] [0xBCFB - rangeMin] = 0x7357;
            mapDataStd [rangeNo] [0xBCFC - rangeMin] = 0x7469;
            mapDataStd [rangeNo] [0xBCFD - rangeMin] = 0x748B;
            mapDataStd [rangeNo] [0xBCFE - rangeMin] = 0x7483;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xBD40 - rangeMin] = 0x747E;
            mapDataStd [rangeNo] [0xBD41 - rangeMin] = 0x7480;
            mapDataStd [rangeNo] [0xBD42 - rangeMin] = 0x757F;
            mapDataStd [rangeNo] [0xBD43 - rangeMin] = 0x7620;
            mapDataStd [rangeNo] [0xBD44 - rangeMin] = 0x7629;
            mapDataStd [rangeNo] [0xBD45 - rangeMin] = 0x761F;
            mapDataStd [rangeNo] [0xBD46 - rangeMin] = 0x7624;
            mapDataStd [rangeNo] [0xBD47 - rangeMin] = 0x7626;
            mapDataStd [rangeNo] [0xBD48 - rangeMin] = 0x7621;
            mapDataStd [rangeNo] [0xBD49 - rangeMin] = 0x7622;
            mapDataStd [rangeNo] [0xBD4A - rangeMin] = 0x769A;
            mapDataStd [rangeNo] [0xBD4B - rangeMin] = 0x76BA;
            mapDataStd [rangeNo] [0xBD4C - rangeMin] = 0x76E4;
            mapDataStd [rangeNo] [0xBD4D - rangeMin] = 0x778E;
            mapDataStd [rangeNo] [0xBD4E - rangeMin] = 0x7787;
            mapDataStd [rangeNo] [0xBD4F - rangeMin] = 0x778C;

            mapDataStd [rangeNo] [0xBD50 - rangeMin] = 0x7791;
            mapDataStd [rangeNo] [0xBD51 - rangeMin] = 0x778B;
            mapDataStd [rangeNo] [0xBD52 - rangeMin] = 0x78CB;
            mapDataStd [rangeNo] [0xBD53 - rangeMin] = 0x78C5;
            mapDataStd [rangeNo] [0xBD54 - rangeMin] = 0x78BA;
            mapDataStd [rangeNo] [0xBD55 - rangeMin] = 0x78CA;
            mapDataStd [rangeNo] [0xBD56 - rangeMin] = 0x78BE;
            mapDataStd [rangeNo] [0xBD57 - rangeMin] = 0x78D5;
            mapDataStd [rangeNo] [0xBD58 - rangeMin] = 0x78BC;
            mapDataStd [rangeNo] [0xBD59 - rangeMin] = 0x78D0;
            mapDataStd [rangeNo] [0xBD5A - rangeMin] = 0x7A3F;
            mapDataStd [rangeNo] [0xBD5B - rangeMin] = 0x7A3C;
            mapDataStd [rangeNo] [0xBD5C - rangeMin] = 0x7A40;
            mapDataStd [rangeNo] [0xBD5D - rangeMin] = 0x7A3D;
            mapDataStd [rangeNo] [0xBD5E - rangeMin] = 0x7A37;
            mapDataStd [rangeNo] [0xBD5F - rangeMin] = 0x7A3B;

            mapDataStd [rangeNo] [0xBD60 - rangeMin] = 0x7AAF;
            mapDataStd [rangeNo] [0xBD61 - rangeMin] = 0x7AAE;
            mapDataStd [rangeNo] [0xBD62 - rangeMin] = 0x7BAD;
            mapDataStd [rangeNo] [0xBD63 - rangeMin] = 0x7BB1;
            mapDataStd [rangeNo] [0xBD64 - rangeMin] = 0x7BC4;
            mapDataStd [rangeNo] [0xBD65 - rangeMin] = 0x7BB4;
            mapDataStd [rangeNo] [0xBD66 - rangeMin] = 0x7BC6;
            mapDataStd [rangeNo] [0xBD67 - rangeMin] = 0x7BC7;
            mapDataStd [rangeNo] [0xBD68 - rangeMin] = 0x7BC1;
            mapDataStd [rangeNo] [0xBD69 - rangeMin] = 0x7BA0;
            mapDataStd [rangeNo] [0xBD6A - rangeMin] = 0x7BCC;
            mapDataStd [rangeNo] [0xBD6B - rangeMin] = 0x7CCA;
            mapDataStd [rangeNo] [0xBD6C - rangeMin] = 0x7DE0;
            mapDataStd [rangeNo] [0xBD6D - rangeMin] = 0x7DF4;
            mapDataStd [rangeNo] [0xBD6E - rangeMin] = 0x7DEF;
            mapDataStd [rangeNo] [0xBD6F - rangeMin] = 0x7DFB;

            mapDataStd [rangeNo] [0xBD70 - rangeMin] = 0x7DD8;
            mapDataStd [rangeNo] [0xBD71 - rangeMin] = 0x7DEC;
            mapDataStd [rangeNo] [0xBD72 - rangeMin] = 0x7DDD;
            mapDataStd [rangeNo] [0xBD73 - rangeMin] = 0x7DE8;
            mapDataStd [rangeNo] [0xBD74 - rangeMin] = 0x7DE3;
            mapDataStd [rangeNo] [0xBD75 - rangeMin] = 0x7DDA;
            mapDataStd [rangeNo] [0xBD76 - rangeMin] = 0x7DDE;
            mapDataStd [rangeNo] [0xBD77 - rangeMin] = 0x7DE9;
            mapDataStd [rangeNo] [0xBD78 - rangeMin] = 0x7D9E;
            mapDataStd [rangeNo] [0xBD79 - rangeMin] = 0x7DD9;
            mapDataStd [rangeNo] [0xBD7A - rangeMin] = 0x7DF2;
            mapDataStd [rangeNo] [0xBD7B - rangeMin] = 0x7DF9;
            mapDataStd [rangeNo] [0xBD7C - rangeMin] = 0x7F75;
            mapDataStd [rangeNo] [0xBD7D - rangeMin] = 0x7F77;
            mapDataStd [rangeNo] [0xBD7E - rangeMin] = 0x7FAF;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xBDA1 - rangeMin] = 0x7FE9;
            mapDataStd [rangeNo] [0xBDA2 - rangeMin] = 0x8026;
            mapDataStd [rangeNo] [0xBDA3 - rangeMin] = 0x819B;
            mapDataStd [rangeNo] [0xBDA4 - rangeMin] = 0x819C;
            mapDataStd [rangeNo] [0xBDA5 - rangeMin] = 0x819D;
            mapDataStd [rangeNo] [0xBDA6 - rangeMin] = 0x81A0;
            mapDataStd [rangeNo] [0xBDA7 - rangeMin] = 0x819A;
            mapDataStd [rangeNo] [0xBDA8 - rangeMin] = 0x8198;
            mapDataStd [rangeNo] [0xBDA9 - rangeMin] = 0x8517;
            mapDataStd [rangeNo] [0xBDAA - rangeMin] = 0x853D;
            mapDataStd [rangeNo] [0xBDAB - rangeMin] = 0x851A;
            mapDataStd [rangeNo] [0xBDAC - rangeMin] = 0x84EE;
            mapDataStd [rangeNo] [0xBDAD - rangeMin] = 0x852C;
            mapDataStd [rangeNo] [0xBDAE - rangeMin] = 0x852D;
            mapDataStd [rangeNo] [0xBDAF - rangeMin] = 0x8513;

            mapDataStd [rangeNo] [0xBDB0 - rangeMin] = 0x8511;
            mapDataStd [rangeNo] [0xBDB1 - rangeMin] = 0x8523;
            mapDataStd [rangeNo] [0xBDB2 - rangeMin] = 0x8521;
            mapDataStd [rangeNo] [0xBDB3 - rangeMin] = 0x8514;
            mapDataStd [rangeNo] [0xBDB4 - rangeMin] = 0x84EC;
            mapDataStd [rangeNo] [0xBDB5 - rangeMin] = 0x8525;
            mapDataStd [rangeNo] [0xBDB6 - rangeMin] = 0x84FF;
            mapDataStd [rangeNo] [0xBDB7 - rangeMin] = 0x8506;
            mapDataStd [rangeNo] [0xBDB8 - rangeMin] = 0x8782;
            mapDataStd [rangeNo] [0xBDB9 - rangeMin] = 0x8774;
            mapDataStd [rangeNo] [0xBDBA - rangeMin] = 0x8776;
            mapDataStd [rangeNo] [0xBDBB - rangeMin] = 0x8760;
            mapDataStd [rangeNo] [0xBDBC - rangeMin] = 0x8766;
            mapDataStd [rangeNo] [0xBDBD - rangeMin] = 0x8778;
            mapDataStd [rangeNo] [0xBDBE - rangeMin] = 0x8768;
            mapDataStd [rangeNo] [0xBDBF - rangeMin] = 0x8759;

            mapDataStd [rangeNo] [0xBDC0 - rangeMin] = 0x8757;
            mapDataStd [rangeNo] [0xBDC1 - rangeMin] = 0x874C;
            mapDataStd [rangeNo] [0xBDC2 - rangeMin] = 0x8753;
            mapDataStd [rangeNo] [0xBDC3 - rangeMin] = 0x885B;
            mapDataStd [rangeNo] [0xBDC4 - rangeMin] = 0x885D;
            mapDataStd [rangeNo] [0xBDC5 - rangeMin] = 0x8910;
            mapDataStd [rangeNo] [0xBDC6 - rangeMin] = 0x8907;
            mapDataStd [rangeNo] [0xBDC7 - rangeMin] = 0x8912;
            mapDataStd [rangeNo] [0xBDC8 - rangeMin] = 0x8913;
            mapDataStd [rangeNo] [0xBDC9 - rangeMin] = 0x8915;
            mapDataStd [rangeNo] [0xBDCA - rangeMin] = 0x890A;
            mapDataStd [rangeNo] [0xBDCB - rangeMin] = 0x8ABC;
            mapDataStd [rangeNo] [0xBDCC - rangeMin] = 0x8AD2;
            mapDataStd [rangeNo] [0xBDCD - rangeMin] = 0x8AC7;
            mapDataStd [rangeNo] [0xBDCE - rangeMin] = 0x8AC4;
            mapDataStd [rangeNo] [0xBDCF - rangeMin] = 0x8A95;

            mapDataStd [rangeNo] [0xBDD0 - rangeMin] = 0x8ACB;
            mapDataStd [rangeNo] [0xBDD1 - rangeMin] = 0x8AF8;
            mapDataStd [rangeNo] [0xBDD2 - rangeMin] = 0x8AB2;
            mapDataStd [rangeNo] [0xBDD3 - rangeMin] = 0x8AC9;
            mapDataStd [rangeNo] [0xBDD4 - rangeMin] = 0x8AC2;
            mapDataStd [rangeNo] [0xBDD5 - rangeMin] = 0x8ABF;
            mapDataStd [rangeNo] [0xBDD6 - rangeMin] = 0x8AB0;
            mapDataStd [rangeNo] [0xBDD7 - rangeMin] = 0x8AD6;
            mapDataStd [rangeNo] [0xBDD8 - rangeMin] = 0x8ACD;
            mapDataStd [rangeNo] [0xBDD9 - rangeMin] = 0x8AB6;
            mapDataStd [rangeNo] [0xBDDA - rangeMin] = 0x8AB9;
            mapDataStd [rangeNo] [0xBDDB - rangeMin] = 0x8ADB;
            mapDataStd [rangeNo] [0xBDDC - rangeMin] = 0x8C4C;
            mapDataStd [rangeNo] [0xBDDD - rangeMin] = 0x8C4E;
            mapDataStd [rangeNo] [0xBDDE - rangeMin] = 0x8C6C;
            mapDataStd [rangeNo] [0xBDDF - rangeMin] = 0x8CE0;

            mapDataStd [rangeNo] [0xBDE0 - rangeMin] = 0x8CDE;
            mapDataStd [rangeNo] [0xBDE1 - rangeMin] = 0x8CE6;
            mapDataStd [rangeNo] [0xBDE2 - rangeMin] = 0x8CE4;
            mapDataStd [rangeNo] [0xBDE3 - rangeMin] = 0x8CEC;
            mapDataStd [rangeNo] [0xBDE4 - rangeMin] = 0x8CED;
            mapDataStd [rangeNo] [0xBDE5 - rangeMin] = 0x8CE2;
            mapDataStd [rangeNo] [0xBDE6 - rangeMin] = 0x8CE3;
            mapDataStd [rangeNo] [0xBDE7 - rangeMin] = 0x8CDC;
            mapDataStd [rangeNo] [0xBDE8 - rangeMin] = 0x8CEA;
            mapDataStd [rangeNo] [0xBDE9 - rangeMin] = 0x8CE1;
            mapDataStd [rangeNo] [0xBDEA - rangeMin] = 0x8D6D;
            mapDataStd [rangeNo] [0xBDEB - rangeMin] = 0x8D9F;
            mapDataStd [rangeNo] [0xBDEC - rangeMin] = 0x8DA3;
            mapDataStd [rangeNo] [0xBDED - rangeMin] = 0x8E2B;
            mapDataStd [rangeNo] [0xBDEE - rangeMin] = 0x8E10;
            mapDataStd [rangeNo] [0xBDEF - rangeMin] = 0x8E1D;

            mapDataStd [rangeNo] [0xBDF0 - rangeMin] = 0x8E22;
            mapDataStd [rangeNo] [0xBDF1 - rangeMin] = 0x8E0F;
            mapDataStd [rangeNo] [0xBDF2 - rangeMin] = 0x8E29;
            mapDataStd [rangeNo] [0xBDF3 - rangeMin] = 0x8E1F;
            mapDataStd [rangeNo] [0xBDF4 - rangeMin] = 0x8E21;
            mapDataStd [rangeNo] [0xBDF5 - rangeMin] = 0x8E1E;
            mapDataStd [rangeNo] [0xBDF6 - rangeMin] = 0x8EBA;
            mapDataStd [rangeNo] [0xBDF7 - rangeMin] = 0x8F1D;
            mapDataStd [rangeNo] [0xBDF8 - rangeMin] = 0x8F1B;
            mapDataStd [rangeNo] [0xBDF9 - rangeMin] = 0x8F1F;
            mapDataStd [rangeNo] [0xBDFA - rangeMin] = 0x8F29;
            mapDataStd [rangeNo] [0xBDFB - rangeMin] = 0x8F26;
            mapDataStd [rangeNo] [0xBDFC - rangeMin] = 0x8F2A;
            mapDataStd [rangeNo] [0xBDFD - rangeMin] = 0x8F1C;
            mapDataStd [rangeNo] [0xBDFE - rangeMin] = 0x8F1E;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xBE40 - rangeMin] = 0x8F25;
            mapDataStd [rangeNo] [0xBE41 - rangeMin] = 0x9069;
            mapDataStd [rangeNo] [0xBE42 - rangeMin] = 0x906E;
            mapDataStd [rangeNo] [0xBE43 - rangeMin] = 0x9068;
            mapDataStd [rangeNo] [0xBE44 - rangeMin] = 0x906D;
            mapDataStd [rangeNo] [0xBE45 - rangeMin] = 0x9077;
            mapDataStd [rangeNo] [0xBE46 - rangeMin] = 0x9130;
            mapDataStd [rangeNo] [0xBE47 - rangeMin] = 0x912D;
            mapDataStd [rangeNo] [0xBE48 - rangeMin] = 0x9127;
            mapDataStd [rangeNo] [0xBE49 - rangeMin] = 0x9131;
            mapDataStd [rangeNo] [0xBE4A - rangeMin] = 0x9187;
            mapDataStd [rangeNo] [0xBE4B - rangeMin] = 0x9189;
            mapDataStd [rangeNo] [0xBE4C - rangeMin] = 0x918B;
            mapDataStd [rangeNo] [0xBE4D - rangeMin] = 0x9183;
            mapDataStd [rangeNo] [0xBE4E - rangeMin] = 0x92C5;
            mapDataStd [rangeNo] [0xBE4F - rangeMin] = 0x92BB;

            mapDataStd [rangeNo] [0xBE50 - rangeMin] = 0x92B7;
            mapDataStd [rangeNo] [0xBE51 - rangeMin] = 0x92EA;
            mapDataStd [rangeNo] [0xBE52 - rangeMin] = 0x92AC;
            mapDataStd [rangeNo] [0xBE53 - rangeMin] = 0x92E4;
            mapDataStd [rangeNo] [0xBE54 - rangeMin] = 0x92C1;
            mapDataStd [rangeNo] [0xBE55 - rangeMin] = 0x92B3;
            mapDataStd [rangeNo] [0xBE56 - rangeMin] = 0x92BC;
            mapDataStd [rangeNo] [0xBE57 - rangeMin] = 0x92D2;
            mapDataStd [rangeNo] [0xBE58 - rangeMin] = 0x92C7;
            mapDataStd [rangeNo] [0xBE59 - rangeMin] = 0x92F0;
            mapDataStd [rangeNo] [0xBE5A - rangeMin] = 0x92B2;
            mapDataStd [rangeNo] [0xBE5B - rangeMin] = 0x95AD;
            mapDataStd [rangeNo] [0xBE5C - rangeMin] = 0x95B1;
            mapDataStd [rangeNo] [0xBE5D - rangeMin] = 0x9704;
            mapDataStd [rangeNo] [0xBE5E - rangeMin] = 0x9706;
            mapDataStd [rangeNo] [0xBE5F - rangeMin] = 0x9707;

            mapDataStd [rangeNo] [0xBE60 - rangeMin] = 0x9709;
            mapDataStd [rangeNo] [0xBE61 - rangeMin] = 0x9760;
            mapDataStd [rangeNo] [0xBE62 - rangeMin] = 0x978D;
            mapDataStd [rangeNo] [0xBE63 - rangeMin] = 0x978B;
            mapDataStd [rangeNo] [0xBE64 - rangeMin] = 0x978F;
            mapDataStd [rangeNo] [0xBE65 - rangeMin] = 0x9821;
            mapDataStd [rangeNo] [0xBE66 - rangeMin] = 0x982B;
            mapDataStd [rangeNo] [0xBE67 - rangeMin] = 0x981C;
            mapDataStd [rangeNo] [0xBE68 - rangeMin] = 0x98B3;
            mapDataStd [rangeNo] [0xBE69 - rangeMin] = 0x990A;
            mapDataStd [rangeNo] [0xBE6A - rangeMin] = 0x9913;
            mapDataStd [rangeNo] [0xBE6B - rangeMin] = 0x9912;
            mapDataStd [rangeNo] [0xBE6C - rangeMin] = 0x9918;
            mapDataStd [rangeNo] [0xBE6D - rangeMin] = 0x99DD;
            mapDataStd [rangeNo] [0xBE6E - rangeMin] = 0x99D0;
            mapDataStd [rangeNo] [0xBE6F - rangeMin] = 0x99DF;

            mapDataStd [rangeNo] [0xBE70 - rangeMin] = 0x99DB;
            mapDataStd [rangeNo] [0xBE71 - rangeMin] = 0x99D1;
            mapDataStd [rangeNo] [0xBE72 - rangeMin] = 0x99D5;
            mapDataStd [rangeNo] [0xBE73 - rangeMin] = 0x99D2;
            mapDataStd [rangeNo] [0xBE74 - rangeMin] = 0x99D9;
            mapDataStd [rangeNo] [0xBE75 - rangeMin] = 0x9AB7;
            mapDataStd [rangeNo] [0xBE76 - rangeMin] = 0x9AEE;
            mapDataStd [rangeNo] [0xBE77 - rangeMin] = 0x9AEF;
            mapDataStd [rangeNo] [0xBE78 - rangeMin] = 0x9B27;
            mapDataStd [rangeNo] [0xBE79 - rangeMin] = 0x9B45;
            mapDataStd [rangeNo] [0xBE7A - rangeMin] = 0x9B44;
            mapDataStd [rangeNo] [0xBE7B - rangeMin] = 0x9B77;
            mapDataStd [rangeNo] [0xBE7C - rangeMin] = 0x9B6F;
            mapDataStd [rangeNo] [0xBE7D - rangeMin] = 0x9D06;
            mapDataStd [rangeNo] [0xBE7E - rangeMin] = 0x9D09;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xBEA1 - rangeMin] = 0x9D03;
            mapDataStd [rangeNo] [0xBEA2 - rangeMin] = 0x9EA9;
            mapDataStd [rangeNo] [0xBEA3 - rangeMin] = 0x9EBE;
            mapDataStd [rangeNo] [0xBEA4 - rangeMin] = 0x9ECE;
            mapDataStd [rangeNo] [0xBEA5 - rangeMin] = 0x58A8;
            mapDataStd [rangeNo] [0xBEA6 - rangeMin] = 0x9F52;
            mapDataStd [rangeNo] [0xBEA7 - rangeMin] = 0x5112;
            mapDataStd [rangeNo] [0xBEA8 - rangeMin] = 0x5118;
            mapDataStd [rangeNo] [0xBEA9 - rangeMin] = 0x5114;
            mapDataStd [rangeNo] [0xBEAA - rangeMin] = 0x5110;
            mapDataStd [rangeNo] [0xBEAB - rangeMin] = 0x5115;
            mapDataStd [rangeNo] [0xBEAC - rangeMin] = 0x5180;
            mapDataStd [rangeNo] [0xBEAD - rangeMin] = 0x51AA;
            mapDataStd [rangeNo] [0xBEAE - rangeMin] = 0x51DD;
            mapDataStd [rangeNo] [0xBEAF - rangeMin] = 0x5291;

            mapDataStd [rangeNo] [0xBEB0 - rangeMin] = 0x5293;
            mapDataStd [rangeNo] [0xBEB1 - rangeMin] = 0x52F3;
            mapDataStd [rangeNo] [0xBEB2 - rangeMin] = 0x5659;
            mapDataStd [rangeNo] [0xBEB3 - rangeMin] = 0x566B;
            mapDataStd [rangeNo] [0xBEB4 - rangeMin] = 0x5679;
            mapDataStd [rangeNo] [0xBEB5 - rangeMin] = 0x5669;
            mapDataStd [rangeNo] [0xBEB6 - rangeMin] = 0x5664;
            mapDataStd [rangeNo] [0xBEB7 - rangeMin] = 0x5678;
            mapDataStd [rangeNo] [0xBEB8 - rangeMin] = 0x566A;
            mapDataStd [rangeNo] [0xBEB9 - rangeMin] = 0x5668;
            mapDataStd [rangeNo] [0xBEBA - rangeMin] = 0x5665;
            mapDataStd [rangeNo] [0xBEBB - rangeMin] = 0x5671;
            mapDataStd [rangeNo] [0xBEBC - rangeMin] = 0x566F;
            mapDataStd [rangeNo] [0xBEBD - rangeMin] = 0x566C;
            mapDataStd [rangeNo] [0xBEBE - rangeMin] = 0x5662;
            mapDataStd [rangeNo] [0xBEBF - rangeMin] = 0x5676;

            mapDataStd [rangeNo] [0xBEC0 - rangeMin] = 0x58C1;
            mapDataStd [rangeNo] [0xBEC1 - rangeMin] = 0x58BE;
            mapDataStd [rangeNo] [0xBEC2 - rangeMin] = 0x58C7;
            mapDataStd [rangeNo] [0xBEC3 - rangeMin] = 0x58C5;
            mapDataStd [rangeNo] [0xBEC4 - rangeMin] = 0x596E;
            mapDataStd [rangeNo] [0xBEC5 - rangeMin] = 0x5B1D;
            mapDataStd [rangeNo] [0xBEC6 - rangeMin] = 0x5B34;
            mapDataStd [rangeNo] [0xBEC7 - rangeMin] = 0x5B78;
            mapDataStd [rangeNo] [0xBEC8 - rangeMin] = 0x5BF0;
            mapDataStd [rangeNo] [0xBEC9 - rangeMin] = 0x5C0E;
            mapDataStd [rangeNo] [0xBECA - rangeMin] = 0x5F4A;
            mapDataStd [rangeNo] [0xBECB - rangeMin] = 0x61B2;
            mapDataStd [rangeNo] [0xBECC - rangeMin] = 0x6191;
            mapDataStd [rangeNo] [0xBECD - rangeMin] = 0x61A9;
            mapDataStd [rangeNo] [0xBECE - rangeMin] = 0x618A;
            mapDataStd [rangeNo] [0xBECF - rangeMin] = 0x61CD;

            mapDataStd [rangeNo] [0xBED0 - rangeMin] = 0x61B6;
            mapDataStd [rangeNo] [0xBED1 - rangeMin] = 0x61BE;
            mapDataStd [rangeNo] [0xBED2 - rangeMin] = 0x61CA;
            mapDataStd [rangeNo] [0xBED3 - rangeMin] = 0x61C8;
            mapDataStd [rangeNo] [0xBED4 - rangeMin] = 0x6230;
            mapDataStd [rangeNo] [0xBED5 - rangeMin] = 0x64C5;
            mapDataStd [rangeNo] [0xBED6 - rangeMin] = 0x64C1;
            mapDataStd [rangeNo] [0xBED7 - rangeMin] = 0x64CB;
            mapDataStd [rangeNo] [0xBED8 - rangeMin] = 0x64BB;
            mapDataStd [rangeNo] [0xBED9 - rangeMin] = 0x64BC;
            mapDataStd [rangeNo] [0xBEDA - rangeMin] = 0x64DA;
            mapDataStd [rangeNo] [0xBEDB - rangeMin] = 0x64C4;
            mapDataStd [rangeNo] [0xBEDC - rangeMin] = 0x64C7;
            mapDataStd [rangeNo] [0xBEDD - rangeMin] = 0x64C2;
            mapDataStd [rangeNo] [0xBEDE - rangeMin] = 0x64CD;
            mapDataStd [rangeNo] [0xBEDF - rangeMin] = 0x64BF;

            mapDataStd [rangeNo] [0xBEE0 - rangeMin] = 0x64D2;
            mapDataStd [rangeNo] [0xBEE1 - rangeMin] = 0x64D4;
            mapDataStd [rangeNo] [0xBEE2 - rangeMin] = 0x64BE;
            mapDataStd [rangeNo] [0xBEE3 - rangeMin] = 0x6574;
            mapDataStd [rangeNo] [0xBEE4 - rangeMin] = 0x66C6;
            mapDataStd [rangeNo] [0xBEE5 - rangeMin] = 0x66C9;
            mapDataStd [rangeNo] [0xBEE6 - rangeMin] = 0x66B9;
            mapDataStd [rangeNo] [0xBEE7 - rangeMin] = 0x66C4;
            mapDataStd [rangeNo] [0xBEE8 - rangeMin] = 0x66C7;
            mapDataStd [rangeNo] [0xBEE9 - rangeMin] = 0x66B8;
            mapDataStd [rangeNo] [0xBEEA - rangeMin] = 0x6A3D;
            mapDataStd [rangeNo] [0xBEEB - rangeMin] = 0x6A38;
            mapDataStd [rangeNo] [0xBEEC - rangeMin] = 0x6A3A;
            mapDataStd [rangeNo] [0xBEED - rangeMin] = 0x6A59;
            mapDataStd [rangeNo] [0xBEEE - rangeMin] = 0x6A6B;
            mapDataStd [rangeNo] [0xBEEF - rangeMin] = 0x6A58;

            mapDataStd [rangeNo] [0xBEF0 - rangeMin] = 0x6A39;
            mapDataStd [rangeNo] [0xBEF1 - rangeMin] = 0x6A44;
            mapDataStd [rangeNo] [0xBEF2 - rangeMin] = 0x6A62;
            mapDataStd [rangeNo] [0xBEF3 - rangeMin] = 0x6A61;
            mapDataStd [rangeNo] [0xBEF4 - rangeMin] = 0x6A4B;
            mapDataStd [rangeNo] [0xBEF5 - rangeMin] = 0x6A47;
            mapDataStd [rangeNo] [0xBEF6 - rangeMin] = 0x6A35;
            mapDataStd [rangeNo] [0xBEF7 - rangeMin] = 0x6A5F;
            mapDataStd [rangeNo] [0xBEF8 - rangeMin] = 0x6A48;
            mapDataStd [rangeNo] [0xBEF9 - rangeMin] = 0x6B59;
            mapDataStd [rangeNo] [0xBEFA - rangeMin] = 0x6B77;
            mapDataStd [rangeNo] [0xBEFB - rangeMin] = 0x6C05;
            mapDataStd [rangeNo] [0xBEFC - rangeMin] = 0x6FC2;
            mapDataStd [rangeNo] [0xBEFD - rangeMin] = 0x6FB1;
            mapDataStd [rangeNo] [0xBEFE - rangeMin] = 0x6FA1;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xBF40 - rangeMin] = 0x6FC3;
            mapDataStd [rangeNo] [0xBF41 - rangeMin] = 0x6FA4;
            mapDataStd [rangeNo] [0xBF42 - rangeMin] = 0x6FC1;
            mapDataStd [rangeNo] [0xBF43 - rangeMin] = 0x6FA7;
            mapDataStd [rangeNo] [0xBF44 - rangeMin] = 0x6FB3;
            mapDataStd [rangeNo] [0xBF45 - rangeMin] = 0x6FC0;
            mapDataStd [rangeNo] [0xBF46 - rangeMin] = 0x6FB9;
            mapDataStd [rangeNo] [0xBF47 - rangeMin] = 0x6FB6;
            mapDataStd [rangeNo] [0xBF48 - rangeMin] = 0x6FA6;
            mapDataStd [rangeNo] [0xBF49 - rangeMin] = 0x6FA0;
            mapDataStd [rangeNo] [0xBF4A - rangeMin] = 0x6FB4;
            mapDataStd [rangeNo] [0xBF4B - rangeMin] = 0x71BE;
            mapDataStd [rangeNo] [0xBF4C - rangeMin] = 0x71C9;
            mapDataStd [rangeNo] [0xBF4D - rangeMin] = 0x71D0;
            mapDataStd [rangeNo] [0xBF4E - rangeMin] = 0x71D2;
            mapDataStd [rangeNo] [0xBF4F - rangeMin] = 0x71C8;

            mapDataStd [rangeNo] [0xBF50 - rangeMin] = 0x71D5;
            mapDataStd [rangeNo] [0xBF51 - rangeMin] = 0x71B9;
            mapDataStd [rangeNo] [0xBF52 - rangeMin] = 0x71CE;
            mapDataStd [rangeNo] [0xBF53 - rangeMin] = 0x71D9;
            mapDataStd [rangeNo] [0xBF54 - rangeMin] = 0x71DC;
            mapDataStd [rangeNo] [0xBF55 - rangeMin] = 0x71C3;
            mapDataStd [rangeNo] [0xBF56 - rangeMin] = 0x71C4;
            mapDataStd [rangeNo] [0xBF57 - rangeMin] = 0x7368;
            mapDataStd [rangeNo] [0xBF58 - rangeMin] = 0x749C;
            mapDataStd [rangeNo] [0xBF59 - rangeMin] = 0x74A3;
            mapDataStd [rangeNo] [0xBF5A - rangeMin] = 0x7498;
            mapDataStd [rangeNo] [0xBF5B - rangeMin] = 0x749F;
            mapDataStd [rangeNo] [0xBF5C - rangeMin] = 0x749E;
            mapDataStd [rangeNo] [0xBF5D - rangeMin] = 0x74E2;
            mapDataStd [rangeNo] [0xBF5E - rangeMin] = 0x750C;
            mapDataStd [rangeNo] [0xBF5F - rangeMin] = 0x750D;

            mapDataStd [rangeNo] [0xBF60 - rangeMin] = 0x7634;
            mapDataStd [rangeNo] [0xBF61 - rangeMin] = 0x7638;
            mapDataStd [rangeNo] [0xBF62 - rangeMin] = 0x763A;
            mapDataStd [rangeNo] [0xBF63 - rangeMin] = 0x76E7;
            mapDataStd [rangeNo] [0xBF64 - rangeMin] = 0x76E5;
            mapDataStd [rangeNo] [0xBF65 - rangeMin] = 0x77A0;
            mapDataStd [rangeNo] [0xBF66 - rangeMin] = 0x779E;
            mapDataStd [rangeNo] [0xBF67 - rangeMin] = 0x779F;
            mapDataStd [rangeNo] [0xBF68 - rangeMin] = 0x77A5;
            mapDataStd [rangeNo] [0xBF69 - rangeMin] = 0x78E8;
            mapDataStd [rangeNo] [0xBF6A - rangeMin] = 0x78DA;
            mapDataStd [rangeNo] [0xBF6B - rangeMin] = 0x78EC;
            mapDataStd [rangeNo] [0xBF6C - rangeMin] = 0x78E7;
            mapDataStd [rangeNo] [0xBF6D - rangeMin] = 0x79A6;
            mapDataStd [rangeNo] [0xBF6E - rangeMin] = 0x7A4D;
            mapDataStd [rangeNo] [0xBF6F - rangeMin] = 0x7A4E;

            mapDataStd [rangeNo] [0xBF70 - rangeMin] = 0x7A46;
            mapDataStd [rangeNo] [0xBF71 - rangeMin] = 0x7A4C;
            mapDataStd [rangeNo] [0xBF72 - rangeMin] = 0x7A4B;
            mapDataStd [rangeNo] [0xBF73 - rangeMin] = 0x7ABA;
            mapDataStd [rangeNo] [0xBF74 - rangeMin] = 0x7BD9;
            mapDataStd [rangeNo] [0xBF75 - rangeMin] = 0x7C11;
            mapDataStd [rangeNo] [0xBF76 - rangeMin] = 0x7BC9;
            mapDataStd [rangeNo] [0xBF77 - rangeMin] = 0x7BE4;
            mapDataStd [rangeNo] [0xBF78 - rangeMin] = 0x7BDB;
            mapDataStd [rangeNo] [0xBF79 - rangeMin] = 0x7BE1;
            mapDataStd [rangeNo] [0xBF7A - rangeMin] = 0x7BE9;
            mapDataStd [rangeNo] [0xBF7B - rangeMin] = 0x7BE6;
            mapDataStd [rangeNo] [0xBF7C - rangeMin] = 0x7CD5;
            mapDataStd [rangeNo] [0xBF7D - rangeMin] = 0x7CD6;
            mapDataStd [rangeNo] [0xBF7E - rangeMin] = 0x7E0A;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xBFA1 - rangeMin] = 0x7E11;
            mapDataStd [rangeNo] [0xBFA2 - rangeMin] = 0x7E08;
            mapDataStd [rangeNo] [0xBFA3 - rangeMin] = 0x7E1B;
            mapDataStd [rangeNo] [0xBFA4 - rangeMin] = 0x7E23;
            mapDataStd [rangeNo] [0xBFA5 - rangeMin] = 0x7E1E;
            mapDataStd [rangeNo] [0xBFA6 - rangeMin] = 0x7E1D;
            mapDataStd [rangeNo] [0xBFA7 - rangeMin] = 0x7E09;
            mapDataStd [rangeNo] [0xBFA8 - rangeMin] = 0x7E10;
            mapDataStd [rangeNo] [0xBFA9 - rangeMin] = 0x7F79;
            mapDataStd [rangeNo] [0xBFAA - rangeMin] = 0x7FB2;
            mapDataStd [rangeNo] [0xBFAB - rangeMin] = 0x7FF0;
            mapDataStd [rangeNo] [0xBFAC - rangeMin] = 0x7FF1;
            mapDataStd [rangeNo] [0xBFAD - rangeMin] = 0x7FEE;
            mapDataStd [rangeNo] [0xBFAE - rangeMin] = 0x8028;
            mapDataStd [rangeNo] [0xBFAF - rangeMin] = 0x81B3;

            mapDataStd [rangeNo] [0xBFB0 - rangeMin] = 0x81A9;
            mapDataStd [rangeNo] [0xBFB1 - rangeMin] = 0x81A8;
            mapDataStd [rangeNo] [0xBFB2 - rangeMin] = 0x81FB;
            mapDataStd [rangeNo] [0xBFB3 - rangeMin] = 0x8208;
            mapDataStd [rangeNo] [0xBFB4 - rangeMin] = 0x8258;
            mapDataStd [rangeNo] [0xBFB5 - rangeMin] = 0x8259;
            mapDataStd [rangeNo] [0xBFB6 - rangeMin] = 0x854A;
            mapDataStd [rangeNo] [0xBFB7 - rangeMin] = 0x8559;
            mapDataStd [rangeNo] [0xBFB8 - rangeMin] = 0x8548;
            mapDataStd [rangeNo] [0xBFB9 - rangeMin] = 0x8568;
            mapDataStd [rangeNo] [0xBFBA - rangeMin] = 0x8569;
            mapDataStd [rangeNo] [0xBFBB - rangeMin] = 0x8543;
            mapDataStd [rangeNo] [0xBFBC - rangeMin] = 0x8549;
            mapDataStd [rangeNo] [0xBFBD - rangeMin] = 0x856D;
            mapDataStd [rangeNo] [0xBFBE - rangeMin] = 0x856A;
            mapDataStd [rangeNo] [0xBFBF - rangeMin] = 0x855E;

            mapDataStd [rangeNo] [0xBFC0 - rangeMin] = 0x8783;
            mapDataStd [rangeNo] [0xBFC1 - rangeMin] = 0x879F;
            mapDataStd [rangeNo] [0xBFC2 - rangeMin] = 0x879E;
            mapDataStd [rangeNo] [0xBFC3 - rangeMin] = 0x87A2;
            mapDataStd [rangeNo] [0xBFC4 - rangeMin] = 0x878D;
            mapDataStd [rangeNo] [0xBFC5 - rangeMin] = 0x8861;
            mapDataStd [rangeNo] [0xBFC6 - rangeMin] = 0x892A;
            mapDataStd [rangeNo] [0xBFC7 - rangeMin] = 0x8932;
            mapDataStd [rangeNo] [0xBFC8 - rangeMin] = 0x8925;
            mapDataStd [rangeNo] [0xBFC9 - rangeMin] = 0x892B;
            mapDataStd [rangeNo] [0xBFCA - rangeMin] = 0x8921;
            mapDataStd [rangeNo] [0xBFCB - rangeMin] = 0x89AA;
            mapDataStd [rangeNo] [0xBFCC - rangeMin] = 0x89A6;
            mapDataStd [rangeNo] [0xBFCD - rangeMin] = 0x8AE6;
            mapDataStd [rangeNo] [0xBFCE - rangeMin] = 0x8AFA;
            mapDataStd [rangeNo] [0xBFCF - rangeMin] = 0x8AEB;

            mapDataStd [rangeNo] [0xBFD0 - rangeMin] = 0x8AF1;
            mapDataStd [rangeNo] [0xBFD1 - rangeMin] = 0x8B00;
            mapDataStd [rangeNo] [0xBFD2 - rangeMin] = 0x8ADC;
            mapDataStd [rangeNo] [0xBFD3 - rangeMin] = 0x8AE7;
            mapDataStd [rangeNo] [0xBFD4 - rangeMin] = 0x8AEE;
            mapDataStd [rangeNo] [0xBFD5 - rangeMin] = 0x8AFE;
            mapDataStd [rangeNo] [0xBFD6 - rangeMin] = 0x8B01;
            mapDataStd [rangeNo] [0xBFD7 - rangeMin] = 0x8B02;
            mapDataStd [rangeNo] [0xBFD8 - rangeMin] = 0x8AF7;
            mapDataStd [rangeNo] [0xBFD9 - rangeMin] = 0x8AED;
            mapDataStd [rangeNo] [0xBFDA - rangeMin] = 0x8AF3;
            mapDataStd [rangeNo] [0xBFDB - rangeMin] = 0x8AF6;
            mapDataStd [rangeNo] [0xBFDC - rangeMin] = 0x8AFC;
            mapDataStd [rangeNo] [0xBFDD - rangeMin] = 0x8C6B;
            mapDataStd [rangeNo] [0xBFDE - rangeMin] = 0x8C6D;
            mapDataStd [rangeNo] [0xBFDF - rangeMin] = 0x8C93;

            mapDataStd [rangeNo] [0xBFE0 - rangeMin] = 0x8CF4;
            mapDataStd [rangeNo] [0xBFE1 - rangeMin] = 0x8E44;
            mapDataStd [rangeNo] [0xBFE2 - rangeMin] = 0x8E31;
            mapDataStd [rangeNo] [0xBFE3 - rangeMin] = 0x8E34;
            mapDataStd [rangeNo] [0xBFE4 - rangeMin] = 0x8E42;
            mapDataStd [rangeNo] [0xBFE5 - rangeMin] = 0x8E39;
            mapDataStd [rangeNo] [0xBFE6 - rangeMin] = 0x8E35;
            mapDataStd [rangeNo] [0xBFE7 - rangeMin] = 0x8F3B;
            mapDataStd [rangeNo] [0xBFE8 - rangeMin] = 0x8F2F;
            mapDataStd [rangeNo] [0xBFE9 - rangeMin] = 0x8F38;
            mapDataStd [rangeNo] [0xBFEA - rangeMin] = 0x8F33;
            mapDataStd [rangeNo] [0xBFEB - rangeMin] = 0x8FA8;
            mapDataStd [rangeNo] [0xBFEC - rangeMin] = 0x8FA6;
            mapDataStd [rangeNo] [0xBFED - rangeMin] = 0x9075;
            mapDataStd [rangeNo] [0xBFEE - rangeMin] = 0x9074;
            mapDataStd [rangeNo] [0xBFEF - rangeMin] = 0x9078;

            mapDataStd [rangeNo] [0xBFF0 - rangeMin] = 0x9072;
            mapDataStd [rangeNo] [0xBFF1 - rangeMin] = 0x907C;
            mapDataStd [rangeNo] [0xBFF2 - rangeMin] = 0x907A;
            mapDataStd [rangeNo] [0xBFF3 - rangeMin] = 0x9134;
            mapDataStd [rangeNo] [0xBFF4 - rangeMin] = 0x9192;
            mapDataStd [rangeNo] [0xBFF5 - rangeMin] = 0x9320;
            mapDataStd [rangeNo] [0xBFF6 - rangeMin] = 0x9336;
            mapDataStd [rangeNo] [0xBFF7 - rangeMin] = 0x92F8;
            mapDataStd [rangeNo] [0xBFF8 - rangeMin] = 0x9333;
            mapDataStd [rangeNo] [0xBFF9 - rangeMin] = 0x932F;
            mapDataStd [rangeNo] [0xBFFA - rangeMin] = 0x9322;
            mapDataStd [rangeNo] [0xBFFB - rangeMin] = 0x92FC;
            mapDataStd [rangeNo] [0xBFFC - rangeMin] = 0x932B;
            mapDataStd [rangeNo] [0xBFFD - rangeMin] = 0x9304;
            mapDataStd [rangeNo] [0xBFFE - rangeMin] = 0x931A;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xC040 - rangeMin] = 0x9310;
            mapDataStd [rangeNo] [0xC041 - rangeMin] = 0x9326;
            mapDataStd [rangeNo] [0xC042 - rangeMin] = 0x9321;
            mapDataStd [rangeNo] [0xC043 - rangeMin] = 0x9315;
            mapDataStd [rangeNo] [0xC044 - rangeMin] = 0x932E;
            mapDataStd [rangeNo] [0xC045 - rangeMin] = 0x9319;
            mapDataStd [rangeNo] [0xC046 - rangeMin] = 0x95BB;
            mapDataStd [rangeNo] [0xC047 - rangeMin] = 0x96A7;
            mapDataStd [rangeNo] [0xC048 - rangeMin] = 0x96A8;
            mapDataStd [rangeNo] [0xC049 - rangeMin] = 0x96AA;
            mapDataStd [rangeNo] [0xC04A - rangeMin] = 0x96D5;
            mapDataStd [rangeNo] [0xC04B - rangeMin] = 0x970E;
            mapDataStd [rangeNo] [0xC04C - rangeMin] = 0x9711;
            mapDataStd [rangeNo] [0xC04D - rangeMin] = 0x9716;
            mapDataStd [rangeNo] [0xC04E - rangeMin] = 0x970D;
            mapDataStd [rangeNo] [0xC04F - rangeMin] = 0x9713;

            mapDataStd [rangeNo] [0xC050 - rangeMin] = 0x970F;
            mapDataStd [rangeNo] [0xC051 - rangeMin] = 0x975B;
            mapDataStd [rangeNo] [0xC052 - rangeMin] = 0x975C;
            mapDataStd [rangeNo] [0xC053 - rangeMin] = 0x9766;
            mapDataStd [rangeNo] [0xC054 - rangeMin] = 0x9798;
            mapDataStd [rangeNo] [0xC055 - rangeMin] = 0x9830;
            mapDataStd [rangeNo] [0xC056 - rangeMin] = 0x9838;
            mapDataStd [rangeNo] [0xC057 - rangeMin] = 0x983B;
            mapDataStd [rangeNo] [0xC058 - rangeMin] = 0x9837;
            mapDataStd [rangeNo] [0xC059 - rangeMin] = 0x982D;
            mapDataStd [rangeNo] [0xC05A - rangeMin] = 0x9839;
            mapDataStd [rangeNo] [0xC05B - rangeMin] = 0x9824;
            mapDataStd [rangeNo] [0xC05C - rangeMin] = 0x9910;
            mapDataStd [rangeNo] [0xC05D - rangeMin] = 0x9928;
            mapDataStd [rangeNo] [0xC05E - rangeMin] = 0x991E;
            mapDataStd [rangeNo] [0xC05F - rangeMin] = 0x991B;

            mapDataStd [rangeNo] [0xC060 - rangeMin] = 0x9921;
            mapDataStd [rangeNo] [0xC061 - rangeMin] = 0x991A;
            mapDataStd [rangeNo] [0xC062 - rangeMin] = 0x99ED;
            mapDataStd [rangeNo] [0xC063 - rangeMin] = 0x99E2;
            mapDataStd [rangeNo] [0xC064 - rangeMin] = 0x99F1;
            mapDataStd [rangeNo] [0xC065 - rangeMin] = 0x9AB8;
            mapDataStd [rangeNo] [0xC066 - rangeMin] = 0x9ABC;
            mapDataStd [rangeNo] [0xC067 - rangeMin] = 0x9AFB;
            mapDataStd [rangeNo] [0xC068 - rangeMin] = 0x9AED;
            mapDataStd [rangeNo] [0xC069 - rangeMin] = 0x9B28;
            mapDataStd [rangeNo] [0xC06A - rangeMin] = 0x9B91;
            mapDataStd [rangeNo] [0xC06B - rangeMin] = 0x9D15;
            mapDataStd [rangeNo] [0xC06C - rangeMin] = 0x9D23;
            mapDataStd [rangeNo] [0xC06D - rangeMin] = 0x9D26;
            mapDataStd [rangeNo] [0xC06E - rangeMin] = 0x9D28;
            mapDataStd [rangeNo] [0xC06F - rangeMin] = 0x9D12;

            mapDataStd [rangeNo] [0xC070 - rangeMin] = 0x9D1B;
            mapDataStd [rangeNo] [0xC071 - rangeMin] = 0x9ED8;
            mapDataStd [rangeNo] [0xC072 - rangeMin] = 0x9ED4;
            mapDataStd [rangeNo] [0xC073 - rangeMin] = 0x9F8D;
            mapDataStd [rangeNo] [0xC074 - rangeMin] = 0x9F9C;
            mapDataStd [rangeNo] [0xC075 - rangeMin] = 0x512A;
            mapDataStd [rangeNo] [0xC076 - rangeMin] = 0x511F;
            mapDataStd [rangeNo] [0xC077 - rangeMin] = 0x5121;
            mapDataStd [rangeNo] [0xC078 - rangeMin] = 0x5132;
            mapDataStd [rangeNo] [0xC079 - rangeMin] = 0x52F5;
            mapDataStd [rangeNo] [0xC07A - rangeMin] = 0x568E;
            mapDataStd [rangeNo] [0xC07B - rangeMin] = 0x5680;
            mapDataStd [rangeNo] [0xC07C - rangeMin] = 0x5690;
            mapDataStd [rangeNo] [0xC07D - rangeMin] = 0x5685;
            mapDataStd [rangeNo] [0xC07E - rangeMin] = 0x5687;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xC0A1 - rangeMin] = 0x568F;
            mapDataStd [rangeNo] [0xC0A2 - rangeMin] = 0x58D5;
            mapDataStd [rangeNo] [0xC0A3 - rangeMin] = 0x58D3;
            mapDataStd [rangeNo] [0xC0A4 - rangeMin] = 0x58D1;
            mapDataStd [rangeNo] [0xC0A5 - rangeMin] = 0x58CE;
            mapDataStd [rangeNo] [0xC0A6 - rangeMin] = 0x5B30;
            mapDataStd [rangeNo] [0xC0A7 - rangeMin] = 0x5B2A;
            mapDataStd [rangeNo] [0xC0A8 - rangeMin] = 0x5B24;
            mapDataStd [rangeNo] [0xC0A9 - rangeMin] = 0x5B7A;
            mapDataStd [rangeNo] [0xC0AA - rangeMin] = 0x5C37;
            mapDataStd [rangeNo] [0xC0AB - rangeMin] = 0x5C68;
            mapDataStd [rangeNo] [0xC0AC - rangeMin] = 0x5DBC;
            mapDataStd [rangeNo] [0xC0AD - rangeMin] = 0x5DBA;
            mapDataStd [rangeNo] [0xC0AE - rangeMin] = 0x5DBD;
            mapDataStd [rangeNo] [0xC0AF - rangeMin] = 0x5DB8;

            mapDataStd [rangeNo] [0xC0B0 - rangeMin] = 0x5E6B;
            mapDataStd [rangeNo] [0xC0B1 - rangeMin] = 0x5F4C;
            mapDataStd [rangeNo] [0xC0B2 - rangeMin] = 0x5FBD;
            mapDataStd [rangeNo] [0xC0B3 - rangeMin] = 0x61C9;
            mapDataStd [rangeNo] [0xC0B4 - rangeMin] = 0x61C2;
            mapDataStd [rangeNo] [0xC0B5 - rangeMin] = 0x61C7;
            mapDataStd [rangeNo] [0xC0B6 - rangeMin] = 0x61E6;
            mapDataStd [rangeNo] [0xC0B7 - rangeMin] = 0x61CB;
            mapDataStd [rangeNo] [0xC0B8 - rangeMin] = 0x6232;
            mapDataStd [rangeNo] [0xC0B9 - rangeMin] = 0x6234;
            mapDataStd [rangeNo] [0xC0BA - rangeMin] = 0x64CE;
            mapDataStd [rangeNo] [0xC0BB - rangeMin] = 0x64CA;
            mapDataStd [rangeNo] [0xC0BC - rangeMin] = 0x64D8;
            mapDataStd [rangeNo] [0xC0BD - rangeMin] = 0x64E0;
            mapDataStd [rangeNo] [0xC0BE - rangeMin] = 0x64F0;
            mapDataStd [rangeNo] [0xC0BF - rangeMin] = 0x64E6;

            mapDataStd [rangeNo] [0xC0C0 - rangeMin] = 0x64EC;
            mapDataStd [rangeNo] [0xC0C1 - rangeMin] = 0x64F1;
            mapDataStd [rangeNo] [0xC0C2 - rangeMin] = 0x64E2;
            mapDataStd [rangeNo] [0xC0C3 - rangeMin] = 0x64ED;
            mapDataStd [rangeNo] [0xC0C4 - rangeMin] = 0x6582;
            mapDataStd [rangeNo] [0xC0C5 - rangeMin] = 0x6583;
            mapDataStd [rangeNo] [0xC0C6 - rangeMin] = 0x66D9;
            mapDataStd [rangeNo] [0xC0C7 - rangeMin] = 0x66D6;
            mapDataStd [rangeNo] [0xC0C8 - rangeMin] = 0x6A80;
            mapDataStd [rangeNo] [0xC0C9 - rangeMin] = 0x6A94;
            mapDataStd [rangeNo] [0xC0CA - rangeMin] = 0x6A84;
            mapDataStd [rangeNo] [0xC0CB - rangeMin] = 0x6AA2;
            mapDataStd [rangeNo] [0xC0CC - rangeMin] = 0x6A9C;
            mapDataStd [rangeNo] [0xC0CD - rangeMin] = 0x6ADB;
            mapDataStd [rangeNo] [0xC0CE - rangeMin] = 0x6AA3;
            mapDataStd [rangeNo] [0xC0CF - rangeMin] = 0x6A7E;

            mapDataStd [rangeNo] [0xC0D0 - rangeMin] = 0x6A97;
            mapDataStd [rangeNo] [0xC0D1 - rangeMin] = 0x6A90;
            mapDataStd [rangeNo] [0xC0D2 - rangeMin] = 0x6AA0;
            mapDataStd [rangeNo] [0xC0D3 - rangeMin] = 0x6B5C;
            mapDataStd [rangeNo] [0xC0D4 - rangeMin] = 0x6BAE;
            mapDataStd [rangeNo] [0xC0D5 - rangeMin] = 0x6BDA;
            mapDataStd [rangeNo] [0xC0D6 - rangeMin] = 0x6C08;
            mapDataStd [rangeNo] [0xC0D7 - rangeMin] = 0x6FD8;
            mapDataStd [rangeNo] [0xC0D8 - rangeMin] = 0x6FF1;
            mapDataStd [rangeNo] [0xC0D9 - rangeMin] = 0x6FDF;
            mapDataStd [rangeNo] [0xC0DA - rangeMin] = 0x6FE0;
            mapDataStd [rangeNo] [0xC0DB - rangeMin] = 0x6FDB;
            mapDataStd [rangeNo] [0xC0DC - rangeMin] = 0x6FE4;
            mapDataStd [rangeNo] [0xC0DD - rangeMin] = 0x6FEB;
            mapDataStd [rangeNo] [0xC0DE - rangeMin] = 0x6FEF;
            mapDataStd [rangeNo] [0xC0DF - rangeMin] = 0x6F80;

            mapDataStd [rangeNo] [0xC0E0 - rangeMin] = 0x6FEC;
            mapDataStd [rangeNo] [0xC0E1 - rangeMin] = 0x6FE1;
            mapDataStd [rangeNo] [0xC0E2 - rangeMin] = 0x6FE9;
            mapDataStd [rangeNo] [0xC0E3 - rangeMin] = 0x6FD5;
            mapDataStd [rangeNo] [0xC0E4 - rangeMin] = 0x6FEE;
            mapDataStd [rangeNo] [0xC0E5 - rangeMin] = 0x6FF0;
            mapDataStd [rangeNo] [0xC0E6 - rangeMin] = 0x71E7;
            mapDataStd [rangeNo] [0xC0E7 - rangeMin] = 0x71DF;
            mapDataStd [rangeNo] [0xC0E8 - rangeMin] = 0x71EE;
            mapDataStd [rangeNo] [0xC0E9 - rangeMin] = 0x71E6;
            mapDataStd [rangeNo] [0xC0EA - rangeMin] = 0x71E5;
            mapDataStd [rangeNo] [0xC0EB - rangeMin] = 0x71ED;
            mapDataStd [rangeNo] [0xC0EC - rangeMin] = 0x71EC;
            mapDataStd [rangeNo] [0xC0ED - rangeMin] = 0x71F4;
            mapDataStd [rangeNo] [0xC0EE - rangeMin] = 0x71E0;
            mapDataStd [rangeNo] [0xC0EF - rangeMin] = 0x7235;

            mapDataStd [rangeNo] [0xC0F0 - rangeMin] = 0x7246;
            mapDataStd [rangeNo] [0xC0F1 - rangeMin] = 0x7370;
            mapDataStd [rangeNo] [0xC0F2 - rangeMin] = 0x7372;
            mapDataStd [rangeNo] [0xC0F3 - rangeMin] = 0x74A9;
            mapDataStd [rangeNo] [0xC0F4 - rangeMin] = 0x74B0;
            mapDataStd [rangeNo] [0xC0F5 - rangeMin] = 0x74A6;
            mapDataStd [rangeNo] [0xC0F6 - rangeMin] = 0x74A8;
            mapDataStd [rangeNo] [0xC0F7 - rangeMin] = 0x7646;
            mapDataStd [rangeNo] [0xC0F8 - rangeMin] = 0x7642;
            mapDataStd [rangeNo] [0xC0F9 - rangeMin] = 0x764C;
            mapDataStd [rangeNo] [0xC0FA - rangeMin] = 0x76EA;
            mapDataStd [rangeNo] [0xC0FB - rangeMin] = 0x77B3;
            mapDataStd [rangeNo] [0xC0FC - rangeMin] = 0x77AA;
            mapDataStd [rangeNo] [0xC0FD - rangeMin] = 0x77B0;
            mapDataStd [rangeNo] [0xC0FE - rangeMin] = 0x77AC;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xC140 - rangeMin] = 0x77A7;
            mapDataStd [rangeNo] [0xC141 - rangeMin] = 0x77AD;
            mapDataStd [rangeNo] [0xC142 - rangeMin] = 0x77EF;
            mapDataStd [rangeNo] [0xC143 - rangeMin] = 0x78F7;
            mapDataStd [rangeNo] [0xC144 - rangeMin] = 0x78FA;
            mapDataStd [rangeNo] [0xC145 - rangeMin] = 0x78F4;
            mapDataStd [rangeNo] [0xC146 - rangeMin] = 0x78EF;
            mapDataStd [rangeNo] [0xC147 - rangeMin] = 0x7901;
            mapDataStd [rangeNo] [0xC148 - rangeMin] = 0x79A7;
            mapDataStd [rangeNo] [0xC149 - rangeMin] = 0x79AA;
            mapDataStd [rangeNo] [0xC14A - rangeMin] = 0x7A57;
            mapDataStd [rangeNo] [0xC14B - rangeMin] = 0x7ABF;
            mapDataStd [rangeNo] [0xC14C - rangeMin] = 0x7C07;
            mapDataStd [rangeNo] [0xC14D - rangeMin] = 0x7C0D;
            mapDataStd [rangeNo] [0xC14E - rangeMin] = 0x7BFE;
            mapDataStd [rangeNo] [0xC14F - rangeMin] = 0x7BF7;

            mapDataStd [rangeNo] [0xC150 - rangeMin] = 0x7C0C;
            mapDataStd [rangeNo] [0xC151 - rangeMin] = 0x7BE0;
            mapDataStd [rangeNo] [0xC152 - rangeMin] = 0x7CE0;
            mapDataStd [rangeNo] [0xC153 - rangeMin] = 0x7CDC;
            mapDataStd [rangeNo] [0xC154 - rangeMin] = 0x7CDE;
            mapDataStd [rangeNo] [0xC155 - rangeMin] = 0x7CE2;
            mapDataStd [rangeNo] [0xC156 - rangeMin] = 0x7CDF;
            mapDataStd [rangeNo] [0xC157 - rangeMin] = 0x7CD9;
            mapDataStd [rangeNo] [0xC158 - rangeMin] = 0x7CDD;
            mapDataStd [rangeNo] [0xC159 - rangeMin] = 0x7E2E;
            mapDataStd [rangeNo] [0xC15A - rangeMin] = 0x7E3E;
            mapDataStd [rangeNo] [0xC15B - rangeMin] = 0x7E46;
            mapDataStd [rangeNo] [0xC15C - rangeMin] = 0x7E37;
            mapDataStd [rangeNo] [0xC15D - rangeMin] = 0x7E32;
            mapDataStd [rangeNo] [0xC15E - rangeMin] = 0x7E43;
            mapDataStd [rangeNo] [0xC15F - rangeMin] = 0x7E2B;

            mapDataStd [rangeNo] [0xC160 - rangeMin] = 0x7E3D;
            mapDataStd [rangeNo] [0xC161 - rangeMin] = 0x7E31;
            mapDataStd [rangeNo] [0xC162 - rangeMin] = 0x7E45;
            mapDataStd [rangeNo] [0xC163 - rangeMin] = 0x7E41;
            mapDataStd [rangeNo] [0xC164 - rangeMin] = 0x7E34;
            mapDataStd [rangeNo] [0xC165 - rangeMin] = 0x7E39;
            mapDataStd [rangeNo] [0xC166 - rangeMin] = 0x7E48;
            mapDataStd [rangeNo] [0xC167 - rangeMin] = 0x7E35;
            mapDataStd [rangeNo] [0xC168 - rangeMin] = 0x7E3F;
            mapDataStd [rangeNo] [0xC169 - rangeMin] = 0x7E2F;
            mapDataStd [rangeNo] [0xC16A - rangeMin] = 0x7F44;
            mapDataStd [rangeNo] [0xC16B - rangeMin] = 0x7FF3;
            mapDataStd [rangeNo] [0xC16C - rangeMin] = 0x7FFC;
            mapDataStd [rangeNo] [0xC16D - rangeMin] = 0x8071;
            mapDataStd [rangeNo] [0xC16E - rangeMin] = 0x8072;
            mapDataStd [rangeNo] [0xC16F - rangeMin] = 0x8070;

            mapDataStd [rangeNo] [0xC170 - rangeMin] = 0x806F;
            mapDataStd [rangeNo] [0xC171 - rangeMin] = 0x8073;
            mapDataStd [rangeNo] [0xC172 - rangeMin] = 0x81C6;
            mapDataStd [rangeNo] [0xC173 - rangeMin] = 0x81C3;
            mapDataStd [rangeNo] [0xC174 - rangeMin] = 0x81BA;
            mapDataStd [rangeNo] [0xC175 - rangeMin] = 0x81C2;
            mapDataStd [rangeNo] [0xC176 - rangeMin] = 0x81C0;
            mapDataStd [rangeNo] [0xC177 - rangeMin] = 0x81BF;
            mapDataStd [rangeNo] [0xC178 - rangeMin] = 0x81BD;
            mapDataStd [rangeNo] [0xC179 - rangeMin] = 0x81C9;
            mapDataStd [rangeNo] [0xC17A - rangeMin] = 0x81BE;
            mapDataStd [rangeNo] [0xC17B - rangeMin] = 0x81E8;
            mapDataStd [rangeNo] [0xC17C - rangeMin] = 0x8209;
            mapDataStd [rangeNo] [0xC17D - rangeMin] = 0x8271;
            mapDataStd [rangeNo] [0xC17E - rangeMin] = 0x85AA;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xC1A1 - rangeMin] = 0x8584;
            mapDataStd [rangeNo] [0xC1A2 - rangeMin] = 0x857E;
            mapDataStd [rangeNo] [0xC1A3 - rangeMin] = 0x859C;
            mapDataStd [rangeNo] [0xC1A4 - rangeMin] = 0x8591;
            mapDataStd [rangeNo] [0xC1A5 - rangeMin] = 0x8594;
            mapDataStd [rangeNo] [0xC1A6 - rangeMin] = 0x85AF;
            mapDataStd [rangeNo] [0xC1A7 - rangeMin] = 0x859B;
            mapDataStd [rangeNo] [0xC1A8 - rangeMin] = 0x8587;
            mapDataStd [rangeNo] [0xC1A9 - rangeMin] = 0x85A8;
            mapDataStd [rangeNo] [0xC1AA - rangeMin] = 0x858A;
            mapDataStd [rangeNo] [0xC1AB - rangeMin] = 0x8667;
            mapDataStd [rangeNo] [0xC1AC - rangeMin] = 0x87C0;
            mapDataStd [rangeNo] [0xC1AD - rangeMin] = 0x87D1;
            mapDataStd [rangeNo] [0xC1AE - rangeMin] = 0x87B3;
            mapDataStd [rangeNo] [0xC1AF - rangeMin] = 0x87D2;

            mapDataStd [rangeNo] [0xC1B0 - rangeMin] = 0x87C6;
            mapDataStd [rangeNo] [0xC1B1 - rangeMin] = 0x87AB;
            mapDataStd [rangeNo] [0xC1B2 - rangeMin] = 0x87BB;
            mapDataStd [rangeNo] [0xC1B3 - rangeMin] = 0x87BA;
            mapDataStd [rangeNo] [0xC1B4 - rangeMin] = 0x87C8;
            mapDataStd [rangeNo] [0xC1B5 - rangeMin] = 0x87CB;
            mapDataStd [rangeNo] [0xC1B6 - rangeMin] = 0x893B;
            mapDataStd [rangeNo] [0xC1B7 - rangeMin] = 0x8936;
            mapDataStd [rangeNo] [0xC1B8 - rangeMin] = 0x8944;
            mapDataStd [rangeNo] [0xC1B9 - rangeMin] = 0x8938;
            mapDataStd [rangeNo] [0xC1BA - rangeMin] = 0x893D;
            mapDataStd [rangeNo] [0xC1BB - rangeMin] = 0x89AC;
            mapDataStd [rangeNo] [0xC1BC - rangeMin] = 0x8B0E;
            mapDataStd [rangeNo] [0xC1BD - rangeMin] = 0x8B17;
            mapDataStd [rangeNo] [0xC1BE - rangeMin] = 0x8B19;
            mapDataStd [rangeNo] [0xC1BF - rangeMin] = 0x8B1B;

            mapDataStd [rangeNo] [0xC1C0 - rangeMin] = 0x8B0A;
            mapDataStd [rangeNo] [0xC1C1 - rangeMin] = 0x8B20;
            mapDataStd [rangeNo] [0xC1C2 - rangeMin] = 0x8B1D;
            mapDataStd [rangeNo] [0xC1C3 - rangeMin] = 0x8B04;
            mapDataStd [rangeNo] [0xC1C4 - rangeMin] = 0x8B10;
            mapDataStd [rangeNo] [0xC1C5 - rangeMin] = 0x8C41;
            mapDataStd [rangeNo] [0xC1C6 - rangeMin] = 0x8C3F;
            mapDataStd [rangeNo] [0xC1C7 - rangeMin] = 0x8C73;
            mapDataStd [rangeNo] [0xC1C8 - rangeMin] = 0x8CFA;
            mapDataStd [rangeNo] [0xC1C9 - rangeMin] = 0x8CFD;
            mapDataStd [rangeNo] [0xC1CA - rangeMin] = 0x8CFC;
            mapDataStd [rangeNo] [0xC1CB - rangeMin] = 0x8CF8;
            mapDataStd [rangeNo] [0xC1CC - rangeMin] = 0x8CFB;
            mapDataStd [rangeNo] [0xC1CD - rangeMin] = 0x8DA8;
            mapDataStd [rangeNo] [0xC1CE - rangeMin] = 0x8E49;
            mapDataStd [rangeNo] [0xC1CF - rangeMin] = 0x8E4B;

            mapDataStd [rangeNo] [0xC1D0 - rangeMin] = 0x8E48;
            mapDataStd [rangeNo] [0xC1D1 - rangeMin] = 0x8E4A;
            mapDataStd [rangeNo] [0xC1D2 - rangeMin] = 0x8F44;
            mapDataStd [rangeNo] [0xC1D3 - rangeMin] = 0x8F3E;
            mapDataStd [rangeNo] [0xC1D4 - rangeMin] = 0x8F42;
            mapDataStd [rangeNo] [0xC1D5 - rangeMin] = 0x8F45;
            mapDataStd [rangeNo] [0xC1D6 - rangeMin] = 0x8F3F;
            mapDataStd [rangeNo] [0xC1D7 - rangeMin] = 0x907F;
            mapDataStd [rangeNo] [0xC1D8 - rangeMin] = 0x907D;
            mapDataStd [rangeNo] [0xC1D9 - rangeMin] = 0x9084;
            mapDataStd [rangeNo] [0xC1DA - rangeMin] = 0x9081;
            mapDataStd [rangeNo] [0xC1DB - rangeMin] = 0x9082;
            mapDataStd [rangeNo] [0xC1DC - rangeMin] = 0x9080;
            mapDataStd [rangeNo] [0xC1DD - rangeMin] = 0x9139;
            mapDataStd [rangeNo] [0xC1DE - rangeMin] = 0x91A3;
            mapDataStd [rangeNo] [0xC1DF - rangeMin] = 0x919E;

            mapDataStd [rangeNo] [0xC1E0 - rangeMin] = 0x919C;
            mapDataStd [rangeNo] [0xC1E1 - rangeMin] = 0x934D;
            mapDataStd [rangeNo] [0xC1E2 - rangeMin] = 0x9382;
            mapDataStd [rangeNo] [0xC1E3 - rangeMin] = 0x9328;
            mapDataStd [rangeNo] [0xC1E4 - rangeMin] = 0x9375;
            mapDataStd [rangeNo] [0xC1E5 - rangeMin] = 0x934A;
            mapDataStd [rangeNo] [0xC1E6 - rangeMin] = 0x9365;
            mapDataStd [rangeNo] [0xC1E7 - rangeMin] = 0x934B;
            mapDataStd [rangeNo] [0xC1E8 - rangeMin] = 0x9318;
            mapDataStd [rangeNo] [0xC1E9 - rangeMin] = 0x937E;
            mapDataStd [rangeNo] [0xC1EA - rangeMin] = 0x936C;
            mapDataStd [rangeNo] [0xC1EB - rangeMin] = 0x935B;
            mapDataStd [rangeNo] [0xC1EC - rangeMin] = 0x9370;
            mapDataStd [rangeNo] [0xC1ED - rangeMin] = 0x935A;
            mapDataStd [rangeNo] [0xC1EE - rangeMin] = 0x9354;
            mapDataStd [rangeNo] [0xC1EF - rangeMin] = 0x95CA;

            mapDataStd [rangeNo] [0xC1F0 - rangeMin] = 0x95CB;
            mapDataStd [rangeNo] [0xC1F1 - rangeMin] = 0x95CC;
            mapDataStd [rangeNo] [0xC1F2 - rangeMin] = 0x95C8;
            mapDataStd [rangeNo] [0xC1F3 - rangeMin] = 0x95C6;
            mapDataStd [rangeNo] [0xC1F4 - rangeMin] = 0x96B1;
            mapDataStd [rangeNo] [0xC1F5 - rangeMin] = 0x96B8;
            mapDataStd [rangeNo] [0xC1F6 - rangeMin] = 0x96D6;
            mapDataStd [rangeNo] [0xC1F7 - rangeMin] = 0x971C;
            mapDataStd [rangeNo] [0xC1F8 - rangeMin] = 0x971E;
            mapDataStd [rangeNo] [0xC1F9 - rangeMin] = 0x97A0;
            mapDataStd [rangeNo] [0xC1FA - rangeMin] = 0x97D3;
            mapDataStd [rangeNo] [0xC1FB - rangeMin] = 0x9846;
            mapDataStd [rangeNo] [0xC1FC - rangeMin] = 0x98B6;
            mapDataStd [rangeNo] [0xC1FD - rangeMin] = 0x9935;
            mapDataStd [rangeNo] [0xC1FE - rangeMin] = 0x9A01;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xC240 - rangeMin] = 0x99FF;
            mapDataStd [rangeNo] [0xC241 - rangeMin] = 0x9BAE;
            mapDataStd [rangeNo] [0xC242 - rangeMin] = 0x9BAB;
            mapDataStd [rangeNo] [0xC243 - rangeMin] = 0x9BAA;
            mapDataStd [rangeNo] [0xC244 - rangeMin] = 0x9BAD;
            mapDataStd [rangeNo] [0xC245 - rangeMin] = 0x9D3B;
            mapDataStd [rangeNo] [0xC246 - rangeMin] = 0x9D3F;
            mapDataStd [rangeNo] [0xC247 - rangeMin] = 0x9E8B;
            mapDataStd [rangeNo] [0xC248 - rangeMin] = 0x9ECF;
            mapDataStd [rangeNo] [0xC249 - rangeMin] = 0x9EDE;
            mapDataStd [rangeNo] [0xC24A - rangeMin] = 0x9EDC;
            mapDataStd [rangeNo] [0xC24B - rangeMin] = 0x9EDD;
            mapDataStd [rangeNo] [0xC24C - rangeMin] = 0x9EDB;
            mapDataStd [rangeNo] [0xC24D - rangeMin] = 0x9F3E;
            mapDataStd [rangeNo] [0xC24E - rangeMin] = 0x9F4B;
            mapDataStd [rangeNo] [0xC24F - rangeMin] = 0x53E2;

            mapDataStd [rangeNo] [0xC250 - rangeMin] = 0x5695;
            mapDataStd [rangeNo] [0xC251 - rangeMin] = 0x56AE;
            mapDataStd [rangeNo] [0xC252 - rangeMin] = 0x58D9;
            mapDataStd [rangeNo] [0xC253 - rangeMin] = 0x58D8;
            mapDataStd [rangeNo] [0xC254 - rangeMin] = 0x5B38;
            mapDataStd [rangeNo] [0xC255 - rangeMin] = 0x5F5D;
            mapDataStd [rangeNo] [0xC256 - rangeMin] = 0x61E3;
            mapDataStd [rangeNo] [0xC257 - rangeMin] = 0x6233;
            mapDataStd [rangeNo] [0xC258 - rangeMin] = 0x64F4;
            mapDataStd [rangeNo] [0xC259 - rangeMin] = 0x64F2;
            mapDataStd [rangeNo] [0xC25A - rangeMin] = 0x64FE;
            mapDataStd [rangeNo] [0xC25B - rangeMin] = 0x6506;
            mapDataStd [rangeNo] [0xC25C - rangeMin] = 0x64FA;
            mapDataStd [rangeNo] [0xC25D - rangeMin] = 0x64FB;
            mapDataStd [rangeNo] [0xC25E - rangeMin] = 0x64F7;
            mapDataStd [rangeNo] [0xC25F - rangeMin] = 0x65B7;

            mapDataStd [rangeNo] [0xC260 - rangeMin] = 0x66DC;
            mapDataStd [rangeNo] [0xC261 - rangeMin] = 0x6726;
            mapDataStd [rangeNo] [0xC262 - rangeMin] = 0x6AB3;
            mapDataStd [rangeNo] [0xC263 - rangeMin] = 0x6AAC;
            mapDataStd [rangeNo] [0xC264 - rangeMin] = 0x6AC3;
            mapDataStd [rangeNo] [0xC265 - rangeMin] = 0x6ABB;
            mapDataStd [rangeNo] [0xC266 - rangeMin] = 0x6AB8;
            mapDataStd [rangeNo] [0xC267 - rangeMin] = 0x6AC2;
            mapDataStd [rangeNo] [0xC268 - rangeMin] = 0x6AAE;
            mapDataStd [rangeNo] [0xC269 - rangeMin] = 0x6AAF;
            mapDataStd [rangeNo] [0xC26A - rangeMin] = 0x6B5F;
            mapDataStd [rangeNo] [0xC26B - rangeMin] = 0x6B78;
            mapDataStd [rangeNo] [0xC26C - rangeMin] = 0x6BAF;
            mapDataStd [rangeNo] [0xC26D - rangeMin] = 0x7009;
            mapDataStd [rangeNo] [0xC26E - rangeMin] = 0x700B;
            mapDataStd [rangeNo] [0xC26F - rangeMin] = 0x6FFE;

            mapDataStd [rangeNo] [0xC270 - rangeMin] = 0x7006;
            mapDataStd [rangeNo] [0xC271 - rangeMin] = 0x6FFA;
            mapDataStd [rangeNo] [0xC272 - rangeMin] = 0x7011;
            mapDataStd [rangeNo] [0xC273 - rangeMin] = 0x700F;
            mapDataStd [rangeNo] [0xC274 - rangeMin] = 0x71FB;
            mapDataStd [rangeNo] [0xC275 - rangeMin] = 0x71FC;
            mapDataStd [rangeNo] [0xC276 - rangeMin] = 0x71FE;
            mapDataStd [rangeNo] [0xC277 - rangeMin] = 0x71F8;
            mapDataStd [rangeNo] [0xC278 - rangeMin] = 0x7377;
            mapDataStd [rangeNo] [0xC279 - rangeMin] = 0x7375;
            mapDataStd [rangeNo] [0xC27A - rangeMin] = 0x74A7;
            mapDataStd [rangeNo] [0xC27B - rangeMin] = 0x74BF;
            mapDataStd [rangeNo] [0xC27C - rangeMin] = 0x7515;
            mapDataStd [rangeNo] [0xC27D - rangeMin] = 0x7656;
            mapDataStd [rangeNo] [0xC27E - rangeMin] = 0x7658;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xC2A1 - rangeMin] = 0x7652;
            mapDataStd [rangeNo] [0xC2A2 - rangeMin] = 0x77BD;
            mapDataStd [rangeNo] [0xC2A3 - rangeMin] = 0x77BF;
            mapDataStd [rangeNo] [0xC2A4 - rangeMin] = 0x77BB;
            mapDataStd [rangeNo] [0xC2A5 - rangeMin] = 0x77BC;
            mapDataStd [rangeNo] [0xC2A6 - rangeMin] = 0x790E;
            mapDataStd [rangeNo] [0xC2A7 - rangeMin] = 0x79AE;
            mapDataStd [rangeNo] [0xC2A8 - rangeMin] = 0x7A61;
            mapDataStd [rangeNo] [0xC2A9 - rangeMin] = 0x7A62;
            mapDataStd [rangeNo] [0xC2AA - rangeMin] = 0x7A60;
            mapDataStd [rangeNo] [0xC2AB - rangeMin] = 0x7AC4;
            mapDataStd [rangeNo] [0xC2AC - rangeMin] = 0x7AC5;
            mapDataStd [rangeNo] [0xC2AD - rangeMin] = 0x7C2B;
            mapDataStd [rangeNo] [0xC2AE - rangeMin] = 0x7C27;
            mapDataStd [rangeNo] [0xC2AF - rangeMin] = 0x7C2A;

            mapDataStd [rangeNo] [0xC2B0 - rangeMin] = 0x7C1E;
            mapDataStd [rangeNo] [0xC2B1 - rangeMin] = 0x7C23;
            mapDataStd [rangeNo] [0xC2B2 - rangeMin] = 0x7C21;
            mapDataStd [rangeNo] [0xC2B3 - rangeMin] = 0x7CE7;
            mapDataStd [rangeNo] [0xC2B4 - rangeMin] = 0x7E54;
            mapDataStd [rangeNo] [0xC2B5 - rangeMin] = 0x7E55;
            mapDataStd [rangeNo] [0xC2B6 - rangeMin] = 0x7E5E;
            mapDataStd [rangeNo] [0xC2B7 - rangeMin] = 0x7E5A;
            mapDataStd [rangeNo] [0xC2B8 - rangeMin] = 0x7E61;
            mapDataStd [rangeNo] [0xC2B9 - rangeMin] = 0x7E52;
            mapDataStd [rangeNo] [0xC2BA - rangeMin] = 0x7E59;
            mapDataStd [rangeNo] [0xC2BB - rangeMin] = 0x7F48;
            mapDataStd [rangeNo] [0xC2BC - rangeMin] = 0x7FF9;
            mapDataStd [rangeNo] [0xC2BD - rangeMin] = 0x7FFB;
            mapDataStd [rangeNo] [0xC2BE - rangeMin] = 0x8077;
            mapDataStd [rangeNo] [0xC2BF - rangeMin] = 0x8076;

            mapDataStd [rangeNo] [0xC2C0 - rangeMin] = 0x81CD;
            mapDataStd [rangeNo] [0xC2C1 - rangeMin] = 0x81CF;
            mapDataStd [rangeNo] [0xC2C2 - rangeMin] = 0x820A;
            mapDataStd [rangeNo] [0xC2C3 - rangeMin] = 0x85CF;
            mapDataStd [rangeNo] [0xC2C4 - rangeMin] = 0x85A9;
            mapDataStd [rangeNo] [0xC2C5 - rangeMin] = 0x85CD;
            mapDataStd [rangeNo] [0xC2C6 - rangeMin] = 0x85D0;
            mapDataStd [rangeNo] [0xC2C7 - rangeMin] = 0x85C9;
            mapDataStd [rangeNo] [0xC2C8 - rangeMin] = 0x85B0;
            mapDataStd [rangeNo] [0xC2C9 - rangeMin] = 0x85BA;
            mapDataStd [rangeNo] [0xC2CA - rangeMin] = 0x85B9;
            mapDataStd [rangeNo] [0xC2CB - rangeMin] = 0x85A6;
            mapDataStd [rangeNo] [0xC2CC - rangeMin] = 0x87EF;
            mapDataStd [rangeNo] [0xC2CD - rangeMin] = 0x87EC;
            mapDataStd [rangeNo] [0xC2CE - rangeMin] = 0x87F2;
            mapDataStd [rangeNo] [0xC2CF - rangeMin] = 0x87E0;

            mapDataStd [rangeNo] [0xC2D0 - rangeMin] = 0x8986;
            mapDataStd [rangeNo] [0xC2D1 - rangeMin] = 0x89B2;
            mapDataStd [rangeNo] [0xC2D2 - rangeMin] = 0x89F4;
            mapDataStd [rangeNo] [0xC2D3 - rangeMin] = 0x8B28;
            mapDataStd [rangeNo] [0xC2D4 - rangeMin] = 0x8B39;
            mapDataStd [rangeNo] [0xC2D5 - rangeMin] = 0x8B2C;
            mapDataStd [rangeNo] [0xC2D6 - rangeMin] = 0x8B2B;
            mapDataStd [rangeNo] [0xC2D7 - rangeMin] = 0x8C50;
            mapDataStd [rangeNo] [0xC2D8 - rangeMin] = 0x8D05;
            mapDataStd [rangeNo] [0xC2D9 - rangeMin] = 0x8E59;
            mapDataStd [rangeNo] [0xC2DA - rangeMin] = 0x8E63;
            mapDataStd [rangeNo] [0xC2DB - rangeMin] = 0x8E66;
            mapDataStd [rangeNo] [0xC2DC - rangeMin] = 0x8E64;
            mapDataStd [rangeNo] [0xC2DD - rangeMin] = 0x8E5F;
            mapDataStd [rangeNo] [0xC2DE - rangeMin] = 0x8E55;
            mapDataStd [rangeNo] [0xC2DF - rangeMin] = 0x8EC0;

            mapDataStd [rangeNo] [0xC2E0 - rangeMin] = 0x8F49;
            mapDataStd [rangeNo] [0xC2E1 - rangeMin] = 0x8F4D;
            mapDataStd [rangeNo] [0xC2E2 - rangeMin] = 0x9087;
            mapDataStd [rangeNo] [0xC2E3 - rangeMin] = 0x9083;
            mapDataStd [rangeNo] [0xC2E4 - rangeMin] = 0x9088;
            mapDataStd [rangeNo] [0xC2E5 - rangeMin] = 0x91AB;
            mapDataStd [rangeNo] [0xC2E6 - rangeMin] = 0x91AC;
            mapDataStd [rangeNo] [0xC2E7 - rangeMin] = 0x91D0;
            mapDataStd [rangeNo] [0xC2E8 - rangeMin] = 0x9394;
            mapDataStd [rangeNo] [0xC2E9 - rangeMin] = 0x938A;
            mapDataStd [rangeNo] [0xC2EA - rangeMin] = 0x9396;
            mapDataStd [rangeNo] [0xC2EB - rangeMin] = 0x93A2;
            mapDataStd [rangeNo] [0xC2EC - rangeMin] = 0x93B3;
            mapDataStd [rangeNo] [0xC2ED - rangeMin] = 0x93AE;
            mapDataStd [rangeNo] [0xC2EE - rangeMin] = 0x93AC;
            mapDataStd [rangeNo] [0xC2EF - rangeMin] = 0x93B0;

            mapDataStd [rangeNo] [0xC2F0 - rangeMin] = 0x9398;
            mapDataStd [rangeNo] [0xC2F1 - rangeMin] = 0x939A;
            mapDataStd [rangeNo] [0xC2F2 - rangeMin] = 0x9397;
            mapDataStd [rangeNo] [0xC2F3 - rangeMin] = 0x95D4;
            mapDataStd [rangeNo] [0xC2F4 - rangeMin] = 0x95D6;
            mapDataStd [rangeNo] [0xC2F5 - rangeMin] = 0x95D0;
            mapDataStd [rangeNo] [0xC2F6 - rangeMin] = 0x95D5;
            mapDataStd [rangeNo] [0xC2F7 - rangeMin] = 0x96E2;
            mapDataStd [rangeNo] [0xC2F8 - rangeMin] = 0x96DC;
            mapDataStd [rangeNo] [0xC2F9 - rangeMin] = 0x96D9;
            mapDataStd [rangeNo] [0xC2FA - rangeMin] = 0x96DB;
            mapDataStd [rangeNo] [0xC2FB - rangeMin] = 0x96DE;
            mapDataStd [rangeNo] [0xC2FC - rangeMin] = 0x9724;
            mapDataStd [rangeNo] [0xC2FD - rangeMin] = 0x97A3;
            mapDataStd [rangeNo] [0xC2FE - rangeMin] = 0x97A6;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xC340 - rangeMin] = 0x97AD;
            mapDataStd [rangeNo] [0xC341 - rangeMin] = 0x97F9;
            mapDataStd [rangeNo] [0xC342 - rangeMin] = 0x984D;
            mapDataStd [rangeNo] [0xC343 - rangeMin] = 0x984F;
            mapDataStd [rangeNo] [0xC344 - rangeMin] = 0x984C;
            mapDataStd [rangeNo] [0xC345 - rangeMin] = 0x984E;
            mapDataStd [rangeNo] [0xC346 - rangeMin] = 0x9853;
            mapDataStd [rangeNo] [0xC347 - rangeMin] = 0x98BA;
            mapDataStd [rangeNo] [0xC348 - rangeMin] = 0x993E;
            mapDataStd [rangeNo] [0xC349 - rangeMin] = 0x993F;
            mapDataStd [rangeNo] [0xC34A - rangeMin] = 0x993D;
            mapDataStd [rangeNo] [0xC34B - rangeMin] = 0x992E;
            mapDataStd [rangeNo] [0xC34C - rangeMin] = 0x99A5;
            mapDataStd [rangeNo] [0xC34D - rangeMin] = 0x9A0E;
            mapDataStd [rangeNo] [0xC34E - rangeMin] = 0x9AC1;
            mapDataStd [rangeNo] [0xC34F - rangeMin] = 0x9B03;

            mapDataStd [rangeNo] [0xC350 - rangeMin] = 0x9B06;
            mapDataStd [rangeNo] [0xC351 - rangeMin] = 0x9B4F;
            mapDataStd [rangeNo] [0xC352 - rangeMin] = 0x9B4E;
            mapDataStd [rangeNo] [0xC353 - rangeMin] = 0x9B4D;
            mapDataStd [rangeNo] [0xC354 - rangeMin] = 0x9BCA;
            mapDataStd [rangeNo] [0xC355 - rangeMin] = 0x9BC9;
            mapDataStd [rangeNo] [0xC356 - rangeMin] = 0x9BFD;
            mapDataStd [rangeNo] [0xC357 - rangeMin] = 0x9BC8;
            mapDataStd [rangeNo] [0xC358 - rangeMin] = 0x9BC0;
            mapDataStd [rangeNo] [0xC359 - rangeMin] = 0x9D51;
            mapDataStd [rangeNo] [0xC35A - rangeMin] = 0x9D5D;
            mapDataStd [rangeNo] [0xC35B - rangeMin] = 0x9D60;
            mapDataStd [rangeNo] [0xC35C - rangeMin] = 0x9EE0;
            mapDataStd [rangeNo] [0xC35D - rangeMin] = 0x9F15;
            mapDataStd [rangeNo] [0xC35E - rangeMin] = 0x9F2C;
            mapDataStd [rangeNo] [0xC35F - rangeMin] = 0x5133;

            mapDataStd [rangeNo] [0xC360 - rangeMin] = 0x56A5;
            mapDataStd [rangeNo] [0xC361 - rangeMin] = 0x58DE;
            mapDataStd [rangeNo] [0xC362 - rangeMin] = 0x58DF;
            mapDataStd [rangeNo] [0xC363 - rangeMin] = 0x58E2;
            mapDataStd [rangeNo] [0xC364 - rangeMin] = 0x5BF5;
            mapDataStd [rangeNo] [0xC365 - rangeMin] = 0x9F90;
            mapDataStd [rangeNo] [0xC366 - rangeMin] = 0x5EEC;
            mapDataStd [rangeNo] [0xC367 - rangeMin] = 0x61F2;
            mapDataStd [rangeNo] [0xC368 - rangeMin] = 0x61F7;
            mapDataStd [rangeNo] [0xC369 - rangeMin] = 0x61F6;
            mapDataStd [rangeNo] [0xC36A - rangeMin] = 0x61F5;
            mapDataStd [rangeNo] [0xC36B - rangeMin] = 0x6500;
            mapDataStd [rangeNo] [0xC36C - rangeMin] = 0x650F;
            mapDataStd [rangeNo] [0xC36D - rangeMin] = 0x66E0;
            mapDataStd [rangeNo] [0xC36E - rangeMin] = 0x66DD;
            mapDataStd [rangeNo] [0xC36F - rangeMin] = 0x6AE5;

            mapDataStd [rangeNo] [0xC370 - rangeMin] = 0x6ADD;
            mapDataStd [rangeNo] [0xC371 - rangeMin] = 0x6ADA;
            mapDataStd [rangeNo] [0xC372 - rangeMin] = 0x6AD3;
            mapDataStd [rangeNo] [0xC373 - rangeMin] = 0x701B;
            mapDataStd [rangeNo] [0xC374 - rangeMin] = 0x701F;
            mapDataStd [rangeNo] [0xC375 - rangeMin] = 0x7028;
            mapDataStd [rangeNo] [0xC376 - rangeMin] = 0x701A;
            mapDataStd [rangeNo] [0xC377 - rangeMin] = 0x701D;
            mapDataStd [rangeNo] [0xC378 - rangeMin] = 0x7015;
            mapDataStd [rangeNo] [0xC379 - rangeMin] = 0x7018;
            mapDataStd [rangeNo] [0xC37A - rangeMin] = 0x7206;
            mapDataStd [rangeNo] [0xC37B - rangeMin] = 0x720D;
            mapDataStd [rangeNo] [0xC37C - rangeMin] = 0x7258;
            mapDataStd [rangeNo] [0xC37D - rangeMin] = 0x72A2;
            mapDataStd [rangeNo] [0xC37E - rangeMin] = 0x7378;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xC3A1 - rangeMin] = 0x737A;
            mapDataStd [rangeNo] [0xC3A2 - rangeMin] = 0x74BD;
            mapDataStd [rangeNo] [0xC3A3 - rangeMin] = 0x74CA;
            mapDataStd [rangeNo] [0xC3A4 - rangeMin] = 0x74E3;
            mapDataStd [rangeNo] [0xC3A5 - rangeMin] = 0x7587;
            mapDataStd [rangeNo] [0xC3A6 - rangeMin] = 0x7586;
            mapDataStd [rangeNo] [0xC3A7 - rangeMin] = 0x765F;
            mapDataStd [rangeNo] [0xC3A8 - rangeMin] = 0x7661;
            mapDataStd [rangeNo] [0xC3A9 - rangeMin] = 0x77C7;
            mapDataStd [rangeNo] [0xC3AA - rangeMin] = 0x7919;
            mapDataStd [rangeNo] [0xC3AB - rangeMin] = 0x79B1;
            mapDataStd [rangeNo] [0xC3AC - rangeMin] = 0x7A6B;
            mapDataStd [rangeNo] [0xC3AD - rangeMin] = 0x7A69;
            mapDataStd [rangeNo] [0xC3AE - rangeMin] = 0x7C3E;
            mapDataStd [rangeNo] [0xC3AF - rangeMin] = 0x7C3F;

            mapDataStd [rangeNo] [0xC3B0 - rangeMin] = 0x7C38;
            mapDataStd [rangeNo] [0xC3B1 - rangeMin] = 0x7C3D;
            mapDataStd [rangeNo] [0xC3B2 - rangeMin] = 0x7C37;
            mapDataStd [rangeNo] [0xC3B3 - rangeMin] = 0x7C40;
            mapDataStd [rangeNo] [0xC3B4 - rangeMin] = 0x7E6B;
            mapDataStd [rangeNo] [0xC3B5 - rangeMin] = 0x7E6D;
            mapDataStd [rangeNo] [0xC3B6 - rangeMin] = 0x7E79;
            mapDataStd [rangeNo] [0xC3B7 - rangeMin] = 0x7E69;
            mapDataStd [rangeNo] [0xC3B8 - rangeMin] = 0x7E6A;
            mapDataStd [rangeNo] [0xC3B9 - rangeMin] = 0x7F85;
            mapDataStd [rangeNo] [0xC3BA - rangeMin] = 0x7E73;
            mapDataStd [rangeNo] [0xC3BB - rangeMin] = 0x7FB6;
            mapDataStd [rangeNo] [0xC3BC - rangeMin] = 0x7FB9;
            mapDataStd [rangeNo] [0xC3BD - rangeMin] = 0x7FB8;
            mapDataStd [rangeNo] [0xC3BE - rangeMin] = 0x81D8;
            mapDataStd [rangeNo] [0xC3BF - rangeMin] = 0x85E9;

            mapDataStd [rangeNo] [0xC3C0 - rangeMin] = 0x85DD;
            mapDataStd [rangeNo] [0xC3C1 - rangeMin] = 0x85EA;
            mapDataStd [rangeNo] [0xC3C2 - rangeMin] = 0x85D5;
            mapDataStd [rangeNo] [0xC3C3 - rangeMin] = 0x85E4;
            mapDataStd [rangeNo] [0xC3C4 - rangeMin] = 0x85E5;
            mapDataStd [rangeNo] [0xC3C5 - rangeMin] = 0x85F7;
            mapDataStd [rangeNo] [0xC3C6 - rangeMin] = 0x87FB;
            mapDataStd [rangeNo] [0xC3C7 - rangeMin] = 0x8805;
            mapDataStd [rangeNo] [0xC3C8 - rangeMin] = 0x880D;
            mapDataStd [rangeNo] [0xC3C9 - rangeMin] = 0x87F9;
            mapDataStd [rangeNo] [0xC3CA - rangeMin] = 0x87FE;
            mapDataStd [rangeNo] [0xC3CB - rangeMin] = 0x8960;
            mapDataStd [rangeNo] [0xC3CC - rangeMin] = 0x895F;
            mapDataStd [rangeNo] [0xC3CD - rangeMin] = 0x8956;
            mapDataStd [rangeNo] [0xC3CE - rangeMin] = 0x895E;
            mapDataStd [rangeNo] [0xC3CF - rangeMin] = 0x8B41;

            mapDataStd [rangeNo] [0xC3D0 - rangeMin] = 0x8B5C;
            mapDataStd [rangeNo] [0xC3D1 - rangeMin] = 0x8B58;
            mapDataStd [rangeNo] [0xC3D2 - rangeMin] = 0x8B49;
            mapDataStd [rangeNo] [0xC3D3 - rangeMin] = 0x8B5A;
            mapDataStd [rangeNo] [0xC3D4 - rangeMin] = 0x8B4E;
            mapDataStd [rangeNo] [0xC3D5 - rangeMin] = 0x8B4F;
            mapDataStd [rangeNo] [0xC3D6 - rangeMin] = 0x8B46;
            mapDataStd [rangeNo] [0xC3D7 - rangeMin] = 0x8B59;
            mapDataStd [rangeNo] [0xC3D8 - rangeMin] = 0x8D08;
            mapDataStd [rangeNo] [0xC3D9 - rangeMin] = 0x8D0A;
            mapDataStd [rangeNo] [0xC3DA - rangeMin] = 0x8E7C;
            mapDataStd [rangeNo] [0xC3DB - rangeMin] = 0x8E72;
            mapDataStd [rangeNo] [0xC3DC - rangeMin] = 0x8E87;
            mapDataStd [rangeNo] [0xC3DD - rangeMin] = 0x8E76;
            mapDataStd [rangeNo] [0xC3DE - rangeMin] = 0x8E6C;
            mapDataStd [rangeNo] [0xC3DF - rangeMin] = 0x8E7A;

            mapDataStd [rangeNo] [0xC3E0 - rangeMin] = 0x8E74;
            mapDataStd [rangeNo] [0xC3E1 - rangeMin] = 0x8F54;
            mapDataStd [rangeNo] [0xC3E2 - rangeMin] = 0x8F4E;
            mapDataStd [rangeNo] [0xC3E3 - rangeMin] = 0x8FAD;
            mapDataStd [rangeNo] [0xC3E4 - rangeMin] = 0x908A;
            mapDataStd [rangeNo] [0xC3E5 - rangeMin] = 0x908B;
            mapDataStd [rangeNo] [0xC3E6 - rangeMin] = 0x91B1;
            mapDataStd [rangeNo] [0xC3E7 - rangeMin] = 0x91AE;
            mapDataStd [rangeNo] [0xC3E8 - rangeMin] = 0x93E1;
            mapDataStd [rangeNo] [0xC3E9 - rangeMin] = 0x93D1;
            mapDataStd [rangeNo] [0xC3EA - rangeMin] = 0x93DF;
            mapDataStd [rangeNo] [0xC3EB - rangeMin] = 0x93C3;
            mapDataStd [rangeNo] [0xC3EC - rangeMin] = 0x93C8;
            mapDataStd [rangeNo] [0xC3ED - rangeMin] = 0x93DC;
            mapDataStd [rangeNo] [0xC3EE - rangeMin] = 0x93DD;
            mapDataStd [rangeNo] [0xC3EF - rangeMin] = 0x93D6;

            mapDataStd [rangeNo] [0xC3F0 - rangeMin] = 0x93E2;
            mapDataStd [rangeNo] [0xC3F1 - rangeMin] = 0x93CD;
            mapDataStd [rangeNo] [0xC3F2 - rangeMin] = 0x93D8;
            mapDataStd [rangeNo] [0xC3F3 - rangeMin] = 0x93E4;
            mapDataStd [rangeNo] [0xC3F4 - rangeMin] = 0x93D7;
            mapDataStd [rangeNo] [0xC3F5 - rangeMin] = 0x93E8;
            mapDataStd [rangeNo] [0xC3F6 - rangeMin] = 0x95DC;
            mapDataStd [rangeNo] [0xC3F7 - rangeMin] = 0x96B4;
            mapDataStd [rangeNo] [0xC3F8 - rangeMin] = 0x96E3;
            mapDataStd [rangeNo] [0xC3F9 - rangeMin] = 0x972A;
            mapDataStd [rangeNo] [0xC3FA - rangeMin] = 0x9727;
            mapDataStd [rangeNo] [0xC3FB - rangeMin] = 0x9761;
            mapDataStd [rangeNo] [0xC3FC - rangeMin] = 0x97DC;
            mapDataStd [rangeNo] [0xC3FD - rangeMin] = 0x97FB;
            mapDataStd [rangeNo] [0xC3FE - rangeMin] = 0x985E;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xC440 - rangeMin] = 0x9858;
            mapDataStd [rangeNo] [0xC441 - rangeMin] = 0x985B;
            mapDataStd [rangeNo] [0xC442 - rangeMin] = 0x98BC;
            mapDataStd [rangeNo] [0xC443 - rangeMin] = 0x9945;
            mapDataStd [rangeNo] [0xC444 - rangeMin] = 0x9949;
            mapDataStd [rangeNo] [0xC445 - rangeMin] = 0x9A16;
            mapDataStd [rangeNo] [0xC446 - rangeMin] = 0x9A19;
            mapDataStd [rangeNo] [0xC447 - rangeMin] = 0x9B0D;
            mapDataStd [rangeNo] [0xC448 - rangeMin] = 0x9BE8;
            mapDataStd [rangeNo] [0xC449 - rangeMin] = 0x9BE7;
            mapDataStd [rangeNo] [0xC44A - rangeMin] = 0x9BD6;
            mapDataStd [rangeNo] [0xC44B - rangeMin] = 0x9BDB;
            mapDataStd [rangeNo] [0xC44C - rangeMin] = 0x9D89;
            mapDataStd [rangeNo] [0xC44D - rangeMin] = 0x9D61;
            mapDataStd [rangeNo] [0xC44E - rangeMin] = 0x9D72;
            mapDataStd [rangeNo] [0xC44F - rangeMin] = 0x9D6A;

            mapDataStd [rangeNo] [0xC450 - rangeMin] = 0x9D6C;
            mapDataStd [rangeNo] [0xC451 - rangeMin] = 0x9E92;
            mapDataStd [rangeNo] [0xC452 - rangeMin] = 0x9E97;
            mapDataStd [rangeNo] [0xC453 - rangeMin] = 0x9E93;
            mapDataStd [rangeNo] [0xC454 - rangeMin] = 0x9EB4;
            mapDataStd [rangeNo] [0xC455 - rangeMin] = 0x52F8;
            mapDataStd [rangeNo] [0xC456 - rangeMin] = 0x56A8;
            mapDataStd [rangeNo] [0xC457 - rangeMin] = 0x56B7;
            mapDataStd [rangeNo] [0xC458 - rangeMin] = 0x56B6;
            mapDataStd [rangeNo] [0xC459 - rangeMin] = 0x56B4;
            mapDataStd [rangeNo] [0xC45A - rangeMin] = 0x56BC;
            mapDataStd [rangeNo] [0xC45B - rangeMin] = 0x58E4;
            mapDataStd [rangeNo] [0xC45C - rangeMin] = 0x5B40;
            mapDataStd [rangeNo] [0xC45D - rangeMin] = 0x5B43;
            mapDataStd [rangeNo] [0xC45E - rangeMin] = 0x5B7D;
            mapDataStd [rangeNo] [0xC45F - rangeMin] = 0x5BF6;

            mapDataStd [rangeNo] [0xC460 - rangeMin] = 0x5DC9;
            mapDataStd [rangeNo] [0xC461 - rangeMin] = 0x61F8;
            mapDataStd [rangeNo] [0xC462 - rangeMin] = 0x61FA;
            mapDataStd [rangeNo] [0xC463 - rangeMin] = 0x6518;
            mapDataStd [rangeNo] [0xC464 - rangeMin] = 0x6514;
            mapDataStd [rangeNo] [0xC465 - rangeMin] = 0x6519;
            mapDataStd [rangeNo] [0xC466 - rangeMin] = 0x66E6;
            mapDataStd [rangeNo] [0xC467 - rangeMin] = 0x6727;
            mapDataStd [rangeNo] [0xC468 - rangeMin] = 0x6AEC;
            mapDataStd [rangeNo] [0xC469 - rangeMin] = 0x703E;
            mapDataStd [rangeNo] [0xC46A - rangeMin] = 0x7030;
            mapDataStd [rangeNo] [0xC46B - rangeMin] = 0x7032;
            mapDataStd [rangeNo] [0xC46C - rangeMin] = 0x7210;
            mapDataStd [rangeNo] [0xC46D - rangeMin] = 0x737B;
            mapDataStd [rangeNo] [0xC46E - rangeMin] = 0x74CF;
            mapDataStd [rangeNo] [0xC46F - rangeMin] = 0x7662;

            mapDataStd [rangeNo] [0xC470 - rangeMin] = 0x7665;
            mapDataStd [rangeNo] [0xC471 - rangeMin] = 0x7926;
            mapDataStd [rangeNo] [0xC472 - rangeMin] = 0x792A;
            mapDataStd [rangeNo] [0xC473 - rangeMin] = 0x792C;
            mapDataStd [rangeNo] [0xC474 - rangeMin] = 0x792B;
            mapDataStd [rangeNo] [0xC475 - rangeMin] = 0x7AC7;
            mapDataStd [rangeNo] [0xC476 - rangeMin] = 0x7AF6;
            mapDataStd [rangeNo] [0xC477 - rangeMin] = 0x7C4C;
            mapDataStd [rangeNo] [0xC478 - rangeMin] = 0x7C43;
            mapDataStd [rangeNo] [0xC479 - rangeMin] = 0x7C4D;
            mapDataStd [rangeNo] [0xC47A - rangeMin] = 0x7CEF;
            mapDataStd [rangeNo] [0xC47B - rangeMin] = 0x7CF0;
            mapDataStd [rangeNo] [0xC47C - rangeMin] = 0x8FAE;
            mapDataStd [rangeNo] [0xC47D - rangeMin] = 0x7E7D;
            mapDataStd [rangeNo] [0xC47E - rangeMin] = 0x7E7C;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xC4A1 - rangeMin] = 0x7E82;
            mapDataStd [rangeNo] [0xC4A2 - rangeMin] = 0x7F4C;
            mapDataStd [rangeNo] [0xC4A3 - rangeMin] = 0x8000;
            mapDataStd [rangeNo] [0xC4A4 - rangeMin] = 0x81DA;
            mapDataStd [rangeNo] [0xC4A5 - rangeMin] = 0x8266;
            mapDataStd [rangeNo] [0xC4A6 - rangeMin] = 0x85FB;
            mapDataStd [rangeNo] [0xC4A7 - rangeMin] = 0x85F9;
            mapDataStd [rangeNo] [0xC4A8 - rangeMin] = 0x8611;
            mapDataStd [rangeNo] [0xC4A9 - rangeMin] = 0x85FA;
            mapDataStd [rangeNo] [0xC4AA - rangeMin] = 0x8606;
            mapDataStd [rangeNo] [0xC4AB - rangeMin] = 0x860B;
            mapDataStd [rangeNo] [0xC4AC - rangeMin] = 0x8607;
            mapDataStd [rangeNo] [0xC4AD - rangeMin] = 0x860A;
            mapDataStd [rangeNo] [0xC4AE - rangeMin] = 0x8814;
            mapDataStd [rangeNo] [0xC4AF - rangeMin] = 0x8815;

            mapDataStd [rangeNo] [0xC4B0 - rangeMin] = 0x8964;
            mapDataStd [rangeNo] [0xC4B1 - rangeMin] = 0x89BA;
            mapDataStd [rangeNo] [0xC4B2 - rangeMin] = 0x89F8;
            mapDataStd [rangeNo] [0xC4B3 - rangeMin] = 0x8B70;
            mapDataStd [rangeNo] [0xC4B4 - rangeMin] = 0x8B6C;
            mapDataStd [rangeNo] [0xC4B5 - rangeMin] = 0x8B66;
            mapDataStd [rangeNo] [0xC4B6 - rangeMin] = 0x8B6F;
            mapDataStd [rangeNo] [0xC4B7 - rangeMin] = 0x8B5F;
            mapDataStd [rangeNo] [0xC4B8 - rangeMin] = 0x8B6B;
            mapDataStd [rangeNo] [0xC4B9 - rangeMin] = 0x8D0F;
            mapDataStd [rangeNo] [0xC4BA - rangeMin] = 0x8D0D;
            mapDataStd [rangeNo] [0xC4BB - rangeMin] = 0x8E89;
            mapDataStd [rangeNo] [0xC4BC - rangeMin] = 0x8E81;
            mapDataStd [rangeNo] [0xC4BD - rangeMin] = 0x8E85;
            mapDataStd [rangeNo] [0xC4BE - rangeMin] = 0x8E82;
            mapDataStd [rangeNo] [0xC4BF - rangeMin] = 0x91B4;

            mapDataStd [rangeNo] [0xC4C0 - rangeMin] = 0x91CB;
            mapDataStd [rangeNo] [0xC4C1 - rangeMin] = 0x9418;
            mapDataStd [rangeNo] [0xC4C2 - rangeMin] = 0x9403;
            mapDataStd [rangeNo] [0xC4C3 - rangeMin] = 0x93FD;
            mapDataStd [rangeNo] [0xC4C4 - rangeMin] = 0x95E1;
            mapDataStd [rangeNo] [0xC4C5 - rangeMin] = 0x9730;
            mapDataStd [rangeNo] [0xC4C6 - rangeMin] = 0x98C4;
            mapDataStd [rangeNo] [0xC4C7 - rangeMin] = 0x9952;
            mapDataStd [rangeNo] [0xC4C8 - rangeMin] = 0x9951;
            mapDataStd [rangeNo] [0xC4C9 - rangeMin] = 0x99A8;
            mapDataStd [rangeNo] [0xC4CA - rangeMin] = 0x9A2B;
            mapDataStd [rangeNo] [0xC4CB - rangeMin] = 0x9A30;
            mapDataStd [rangeNo] [0xC4CC - rangeMin] = 0x9A37;
            mapDataStd [rangeNo] [0xC4CD - rangeMin] = 0x9A35;
            mapDataStd [rangeNo] [0xC4CE - rangeMin] = 0x9C13;
            mapDataStd [rangeNo] [0xC4CF - rangeMin] = 0x9C0D;

            mapDataStd [rangeNo] [0xC4D0 - rangeMin] = 0x9E79;
            mapDataStd [rangeNo] [0xC4D1 - rangeMin] = 0x9EB5;
            mapDataStd [rangeNo] [0xC4D2 - rangeMin] = 0x9EE8;
            mapDataStd [rangeNo] [0xC4D3 - rangeMin] = 0x9F2F;
            mapDataStd [rangeNo] [0xC4D4 - rangeMin] = 0x9F5F;
            mapDataStd [rangeNo] [0xC4D5 - rangeMin] = 0x9F63;
            mapDataStd [rangeNo] [0xC4D6 - rangeMin] = 0x9F61;
            mapDataStd [rangeNo] [0xC4D7 - rangeMin] = 0x5137;
            mapDataStd [rangeNo] [0xC4D8 - rangeMin] = 0x5138;
            mapDataStd [rangeNo] [0xC4D9 - rangeMin] = 0x56C1;
            mapDataStd [rangeNo] [0xC4DA - rangeMin] = 0x56C0;
            mapDataStd [rangeNo] [0xC4DB - rangeMin] = 0x56C2;
            mapDataStd [rangeNo] [0xC4DC - rangeMin] = 0x5914;
            mapDataStd [rangeNo] [0xC4DD - rangeMin] = 0x5C6C;
            mapDataStd [rangeNo] [0xC4DE - rangeMin] = 0x5DCD;
            mapDataStd [rangeNo] [0xC4DF - rangeMin] = 0x61FC;

            mapDataStd [rangeNo] [0xC4E0 - rangeMin] = 0x61FE;
            mapDataStd [rangeNo] [0xC4E1 - rangeMin] = 0x651D;
            mapDataStd [rangeNo] [0xC4E2 - rangeMin] = 0x651C;
            mapDataStd [rangeNo] [0xC4E3 - rangeMin] = 0x6595;
            mapDataStd [rangeNo] [0xC4E4 - rangeMin] = 0x66E9;
            mapDataStd [rangeNo] [0xC4E5 - rangeMin] = 0x6AFB;
            mapDataStd [rangeNo] [0xC4E6 - rangeMin] = 0x6B04;
            mapDataStd [rangeNo] [0xC4E7 - rangeMin] = 0x6AFA;
            mapDataStd [rangeNo] [0xC4E8 - rangeMin] = 0x6BB2;
            mapDataStd [rangeNo] [0xC4E9 - rangeMin] = 0x704C;
            mapDataStd [rangeNo] [0xC4EA - rangeMin] = 0x721B;
            mapDataStd [rangeNo] [0xC4EB - rangeMin] = 0x72A7;
            mapDataStd [rangeNo] [0xC4EC - rangeMin] = 0x74D6;
            mapDataStd [rangeNo] [0xC4ED - rangeMin] = 0x74D4;
            mapDataStd [rangeNo] [0xC4EE - rangeMin] = 0x7669;
            mapDataStd [rangeNo] [0xC4EF - rangeMin] = 0x77D3;

            mapDataStd [rangeNo] [0xC4F0 - rangeMin] = 0x7C50;
            mapDataStd [rangeNo] [0xC4F1 - rangeMin] = 0x7E8F;
            mapDataStd [rangeNo] [0xC4F2 - rangeMin] = 0x7E8C;
            mapDataStd [rangeNo] [0xC4F3 - rangeMin] = 0x7FBC;
            mapDataStd [rangeNo] [0xC4F4 - rangeMin] = 0x8617;
            mapDataStd [rangeNo] [0xC4F5 - rangeMin] = 0x862D;
            mapDataStd [rangeNo] [0xC4F6 - rangeMin] = 0x861A;
            mapDataStd [rangeNo] [0xC4F7 - rangeMin] = 0x8823;
            mapDataStd [rangeNo] [0xC4F8 - rangeMin] = 0x8822;
            mapDataStd [rangeNo] [0xC4F9 - rangeMin] = 0x8821;
            mapDataStd [rangeNo] [0xC4FA - rangeMin] = 0x881F;
            mapDataStd [rangeNo] [0xC4FB - rangeMin] = 0x896A;
            mapDataStd [rangeNo] [0xC4FC - rangeMin] = 0x896C;
            mapDataStd [rangeNo] [0xC4FD - rangeMin] = 0x89BD;
            mapDataStd [rangeNo] [0xC4FE - rangeMin] = 0x8B74;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xC540 - rangeMin] = 0x8B77;
            mapDataStd [rangeNo] [0xC541 - rangeMin] = 0x8B7D;
            mapDataStd [rangeNo] [0xC542 - rangeMin] = 0x8D13;
            mapDataStd [rangeNo] [0xC543 - rangeMin] = 0x8E8A;
            mapDataStd [rangeNo] [0xC544 - rangeMin] = 0x8E8D;
            mapDataStd [rangeNo] [0xC545 - rangeMin] = 0x8E8B;
            mapDataStd [rangeNo] [0xC546 - rangeMin] = 0x8F5F;
            mapDataStd [rangeNo] [0xC547 - rangeMin] = 0x8FAF;
            mapDataStd [rangeNo] [0xC548 - rangeMin] = 0x91BA;
            mapDataStd [rangeNo] [0xC549 - rangeMin] = 0x942E;
            mapDataStd [rangeNo] [0xC54A - rangeMin] = 0x9433;
            mapDataStd [rangeNo] [0xC54B - rangeMin] = 0x9435;
            mapDataStd [rangeNo] [0xC54C - rangeMin] = 0x943A;
            mapDataStd [rangeNo] [0xC54D - rangeMin] = 0x9438;
            mapDataStd [rangeNo] [0xC54E - rangeMin] = 0x9432;
            mapDataStd [rangeNo] [0xC54F - rangeMin] = 0x942B;

            mapDataStd [rangeNo] [0xC550 - rangeMin] = 0x95E2;
            mapDataStd [rangeNo] [0xC551 - rangeMin] = 0x9738;
            mapDataStd [rangeNo] [0xC552 - rangeMin] = 0x9739;
            mapDataStd [rangeNo] [0xC553 - rangeMin] = 0x9732;
            mapDataStd [rangeNo] [0xC554 - rangeMin] = 0x97FF;
            mapDataStd [rangeNo] [0xC555 - rangeMin] = 0x9867;
            mapDataStd [rangeNo] [0xC556 - rangeMin] = 0x9865;
            mapDataStd [rangeNo] [0xC557 - rangeMin] = 0x9957;
            mapDataStd [rangeNo] [0xC558 - rangeMin] = 0x9A45;
            mapDataStd [rangeNo] [0xC559 - rangeMin] = 0x9A43;
            mapDataStd [rangeNo] [0xC55A - rangeMin] = 0x9A40;
            mapDataStd [rangeNo] [0xC55B - rangeMin] = 0x9A3E;
            mapDataStd [rangeNo] [0xC55C - rangeMin] = 0x9ACF;
            mapDataStd [rangeNo] [0xC55D - rangeMin] = 0x9B54;
            mapDataStd [rangeNo] [0xC55E - rangeMin] = 0x9B51;
            mapDataStd [rangeNo] [0xC55F - rangeMin] = 0x9C2D;

            mapDataStd [rangeNo] [0xC560 - rangeMin] = 0x9C25;
            mapDataStd [rangeNo] [0xC561 - rangeMin] = 0x9DAF;
            mapDataStd [rangeNo] [0xC562 - rangeMin] = 0x9DB4;
            mapDataStd [rangeNo] [0xC563 - rangeMin] = 0x9DC2;
            mapDataStd [rangeNo] [0xC564 - rangeMin] = 0x9DB8;
            mapDataStd [rangeNo] [0xC565 - rangeMin] = 0x9E9D;
            mapDataStd [rangeNo] [0xC566 - rangeMin] = 0x9EEF;
            mapDataStd [rangeNo] [0xC567 - rangeMin] = 0x9F19;
            mapDataStd [rangeNo] [0xC568 - rangeMin] = 0x9F5C;
            mapDataStd [rangeNo] [0xC569 - rangeMin] = 0x9F66;
            mapDataStd [rangeNo] [0xC56A - rangeMin] = 0x9F67;
            mapDataStd [rangeNo] [0xC56B - rangeMin] = 0x513C;
            mapDataStd [rangeNo] [0xC56C - rangeMin] = 0x513B;
            mapDataStd [rangeNo] [0xC56D - rangeMin] = 0x56C8;
            mapDataStd [rangeNo] [0xC56E - rangeMin] = 0x56CA;
            mapDataStd [rangeNo] [0xC56F - rangeMin] = 0x56C9;

            mapDataStd [rangeNo] [0xC570 - rangeMin] = 0x5B7F;
            mapDataStd [rangeNo] [0xC571 - rangeMin] = 0x5DD4;
            mapDataStd [rangeNo] [0xC572 - rangeMin] = 0x5DD2;
            mapDataStd [rangeNo] [0xC573 - rangeMin] = 0x5F4E;
            mapDataStd [rangeNo] [0xC574 - rangeMin] = 0x61FF;
            mapDataStd [rangeNo] [0xC575 - rangeMin] = 0x6524;
            mapDataStd [rangeNo] [0xC576 - rangeMin] = 0x6B0A;
            mapDataStd [rangeNo] [0xC577 - rangeMin] = 0x6B61;
            mapDataStd [rangeNo] [0xC578 - rangeMin] = 0x7051;
            mapDataStd [rangeNo] [0xC579 - rangeMin] = 0x7058;
            mapDataStd [rangeNo] [0xC57A - rangeMin] = 0x7380;
            mapDataStd [rangeNo] [0xC57B - rangeMin] = 0x74E4;
            mapDataStd [rangeNo] [0xC57C - rangeMin] = 0x758A;
            mapDataStd [rangeNo] [0xC57D - rangeMin] = 0x766E;
            mapDataStd [rangeNo] [0xC57E - rangeMin] = 0x766C;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xC5A1 - rangeMin] = 0x79B3;
            mapDataStd [rangeNo] [0xC5A2 - rangeMin] = 0x7C60;
            mapDataStd [rangeNo] [0xC5A3 - rangeMin] = 0x7C5F;
            mapDataStd [rangeNo] [0xC5A4 - rangeMin] = 0x807E;
            mapDataStd [rangeNo] [0xC5A5 - rangeMin] = 0x807D;
            mapDataStd [rangeNo] [0xC5A6 - rangeMin] = 0x81DF;
            mapDataStd [rangeNo] [0xC5A7 - rangeMin] = 0x8972;
            mapDataStd [rangeNo] [0xC5A8 - rangeMin] = 0x896F;
            mapDataStd [rangeNo] [0xC5A9 - rangeMin] = 0x89FC;
            mapDataStd [rangeNo] [0xC5AA - rangeMin] = 0x8B80;
            mapDataStd [rangeNo] [0xC5AB - rangeMin] = 0x8D16;
            mapDataStd [rangeNo] [0xC5AC - rangeMin] = 0x8D17;
            mapDataStd [rangeNo] [0xC5AD - rangeMin] = 0x8E91;
            mapDataStd [rangeNo] [0xC5AE - rangeMin] = 0x8E93;
            mapDataStd [rangeNo] [0xC5AF - rangeMin] = 0x8F61;

            mapDataStd [rangeNo] [0xC5B0 - rangeMin] = 0x9148;
            mapDataStd [rangeNo] [0xC5B1 - rangeMin] = 0x9444;
            mapDataStd [rangeNo] [0xC5B2 - rangeMin] = 0x9451;
            mapDataStd [rangeNo] [0xC5B3 - rangeMin] = 0x9452;
            mapDataStd [rangeNo] [0xC5B4 - rangeMin] = 0x973D;
            mapDataStd [rangeNo] [0xC5B5 - rangeMin] = 0x973E;
            mapDataStd [rangeNo] [0xC5B6 - rangeMin] = 0x97C3;
            mapDataStd [rangeNo] [0xC5B7 - rangeMin] = 0x97C1;
            mapDataStd [rangeNo] [0xC5B8 - rangeMin] = 0x986B;
            mapDataStd [rangeNo] [0xC5B9 - rangeMin] = 0x9955;
            mapDataStd [rangeNo] [0xC5BA - rangeMin] = 0x9A55;
            mapDataStd [rangeNo] [0xC5BB - rangeMin] = 0x9A4D;
            mapDataStd [rangeNo] [0xC5BC - rangeMin] = 0x9AD2;
            mapDataStd [rangeNo] [0xC5BD - rangeMin] = 0x9B1A;
            mapDataStd [rangeNo] [0xC5BE - rangeMin] = 0x9C49;
            mapDataStd [rangeNo] [0xC5BF - rangeMin] = 0x9C31;

            mapDataStd [rangeNo] [0xC5C0 - rangeMin] = 0x9C3E;
            mapDataStd [rangeNo] [0xC5C1 - rangeMin] = 0x9C3B;
            mapDataStd [rangeNo] [0xC5C2 - rangeMin] = 0x9DD3;
            mapDataStd [rangeNo] [0xC5C3 - rangeMin] = 0x9DD7;
            mapDataStd [rangeNo] [0xC5C4 - rangeMin] = 0x9F34;
            mapDataStd [rangeNo] [0xC5C5 - rangeMin] = 0x9F6C;
            mapDataStd [rangeNo] [0xC5C6 - rangeMin] = 0x9F6A;
            mapDataStd [rangeNo] [0xC5C7 - rangeMin] = 0x9F94;
            mapDataStd [rangeNo] [0xC5C8 - rangeMin] = 0x56CC;
            mapDataStd [rangeNo] [0xC5C9 - rangeMin] = 0x5DD6;
            mapDataStd [rangeNo] [0xC5CA - rangeMin] = 0x6200;
            mapDataStd [rangeNo] [0xC5CB - rangeMin] = 0x6523;
            mapDataStd [rangeNo] [0xC5CC - rangeMin] = 0x652B;
            mapDataStd [rangeNo] [0xC5CD - rangeMin] = 0x652A;
            mapDataStd [rangeNo] [0xC5CE - rangeMin] = 0x66EC;
            mapDataStd [rangeNo] [0xC5CF - rangeMin] = 0x6B10;

            mapDataStd [rangeNo] [0xC5D0 - rangeMin] = 0x74DA;
            mapDataStd [rangeNo] [0xC5D1 - rangeMin] = 0x7ACA;
            mapDataStd [rangeNo] [0xC5D2 - rangeMin] = 0x7C64;
            mapDataStd [rangeNo] [0xC5D3 - rangeMin] = 0x7C63;
            mapDataStd [rangeNo] [0xC5D4 - rangeMin] = 0x7C65;
            mapDataStd [rangeNo] [0xC5D5 - rangeMin] = 0x7E93;
            mapDataStd [rangeNo] [0xC5D6 - rangeMin] = 0x7E96;
            mapDataStd [rangeNo] [0xC5D7 - rangeMin] = 0x7E94;
            mapDataStd [rangeNo] [0xC5D8 - rangeMin] = 0x81E2;
            mapDataStd [rangeNo] [0xC5D9 - rangeMin] = 0x8638;
            mapDataStd [rangeNo] [0xC5DA - rangeMin] = 0x863F;
            mapDataStd [rangeNo] [0xC5DB - rangeMin] = 0x8831;
            mapDataStd [rangeNo] [0xC5DC - rangeMin] = 0x8B8A;
            mapDataStd [rangeNo] [0xC5DD - rangeMin] = 0x9090;
            mapDataStd [rangeNo] [0xC5DE - rangeMin] = 0x908F;
            mapDataStd [rangeNo] [0xC5DF - rangeMin] = 0x9463;

            mapDataStd [rangeNo] [0xC5E0 - rangeMin] = 0x9460;
            mapDataStd [rangeNo] [0xC5E1 - rangeMin] = 0x9464;
            mapDataStd [rangeNo] [0xC5E2 - rangeMin] = 0x9768;
            mapDataStd [rangeNo] [0xC5E3 - rangeMin] = 0x986F;
            mapDataStd [rangeNo] [0xC5E4 - rangeMin] = 0x995C;
            mapDataStd [rangeNo] [0xC5E5 - rangeMin] = 0x9A5A;
            mapDataStd [rangeNo] [0xC5E6 - rangeMin] = 0x9A5B;
            mapDataStd [rangeNo] [0xC5E7 - rangeMin] = 0x9A57;
            mapDataStd [rangeNo] [0xC5E8 - rangeMin] = 0x9AD3;
            mapDataStd [rangeNo] [0xC5E9 - rangeMin] = 0x9AD4;
            mapDataStd [rangeNo] [0xC5EA - rangeMin] = 0x9AD1;
            mapDataStd [rangeNo] [0xC5EB - rangeMin] = 0x9C54;
            mapDataStd [rangeNo] [0xC5EC - rangeMin] = 0x9C57;
            mapDataStd [rangeNo] [0xC5ED - rangeMin] = 0x9C56;
            mapDataStd [rangeNo] [0xC5EE - rangeMin] = 0x9DE5;
            mapDataStd [rangeNo] [0xC5EF - rangeMin] = 0x9E9F;

            mapDataStd [rangeNo] [0xC5F0 - rangeMin] = 0x9EF4;
            mapDataStd [rangeNo] [0xC5F1 - rangeMin] = 0x56D1;
            mapDataStd [rangeNo] [0xC5F2 - rangeMin] = 0x58E9;
            mapDataStd [rangeNo] [0xC5F3 - rangeMin] = 0x652C;
            mapDataStd [rangeNo] [0xC5F4 - rangeMin] = 0x705E;
            mapDataStd [rangeNo] [0xC5F5 - rangeMin] = 0x7671;
            mapDataStd [rangeNo] [0xC5F6 - rangeMin] = 0x7672;
            mapDataStd [rangeNo] [0xC5F7 - rangeMin] = 0x77D7;
            mapDataStd [rangeNo] [0xC5F8 - rangeMin] = 0x7F50;
            mapDataStd [rangeNo] [0xC5F9 - rangeMin] = 0x7F88;
            mapDataStd [rangeNo] [0xC5FA - rangeMin] = 0x8836;
            mapDataStd [rangeNo] [0xC5FB - rangeMin] = 0x8839;
            mapDataStd [rangeNo] [0xC5FC - rangeMin] = 0x8862;
            mapDataStd [rangeNo] [0xC5FD - rangeMin] = 0x8B93;
            mapDataStd [rangeNo] [0xC5FE - rangeMin] = 0x8B92;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xC640 - rangeMin] = 0x8B96;
            mapDataStd [rangeNo] [0xC641 - rangeMin] = 0x8277;
            mapDataStd [rangeNo] [0xC642 - rangeMin] = 0x8D1B;
            mapDataStd [rangeNo] [0xC643 - rangeMin] = 0x91C0;
            mapDataStd [rangeNo] [0xC644 - rangeMin] = 0x946A;
            mapDataStd [rangeNo] [0xC645 - rangeMin] = 0x9742;
            mapDataStd [rangeNo] [0xC646 - rangeMin] = 0x9748;
            mapDataStd [rangeNo] [0xC647 - rangeMin] = 0x9744;
            mapDataStd [rangeNo] [0xC648 - rangeMin] = 0x97C6;
            mapDataStd [rangeNo] [0xC649 - rangeMin] = 0x9870;
            mapDataStd [rangeNo] [0xC64A - rangeMin] = 0x9A5F;
            mapDataStd [rangeNo] [0xC64B - rangeMin] = 0x9B22;
            mapDataStd [rangeNo] [0xC64C - rangeMin] = 0x9B58;
            mapDataStd [rangeNo] [0xC64D - rangeMin] = 0x9C5F;
            mapDataStd [rangeNo] [0xC64E - rangeMin] = 0x9DF9;
            mapDataStd [rangeNo] [0xC64F - rangeMin] = 0x9DFA;

            mapDataStd [rangeNo] [0xC650 - rangeMin] = 0x9E7C;
            mapDataStd [rangeNo] [0xC651 - rangeMin] = 0x9E7D;
            mapDataStd [rangeNo] [0xC652 - rangeMin] = 0x9F07;
            mapDataStd [rangeNo] [0xC653 - rangeMin] = 0x9F77;
            mapDataStd [rangeNo] [0xC654 - rangeMin] = 0x9F72;
            mapDataStd [rangeNo] [0xC655 - rangeMin] = 0x5EF3;
            mapDataStd [rangeNo] [0xC656 - rangeMin] = 0x6B16;
            mapDataStd [rangeNo] [0xC657 - rangeMin] = 0x7063;
            mapDataStd [rangeNo] [0xC658 - rangeMin] = 0x7C6C;
            mapDataStd [rangeNo] [0xC659 - rangeMin] = 0x7C6E;
            mapDataStd [rangeNo] [0xC65A - rangeMin] = 0x883B;
            mapDataStd [rangeNo] [0xC65B - rangeMin] = 0x89C0;
            mapDataStd [rangeNo] [0xC65C - rangeMin] = 0x8EA1;
            mapDataStd [rangeNo] [0xC65D - rangeMin] = 0x91C1;
            mapDataStd [rangeNo] [0xC65E - rangeMin] = 0x9472;
            mapDataStd [rangeNo] [0xC65F - rangeMin] = 0x9470;

            mapDataStd [rangeNo] [0xC660 - rangeMin] = 0x9871;
            mapDataStd [rangeNo] [0xC661 - rangeMin] = 0x995E;
            mapDataStd [rangeNo] [0xC662 - rangeMin] = 0x9AD6;
            mapDataStd [rangeNo] [0xC663 - rangeMin] = 0x9B23;
            mapDataStd [rangeNo] [0xC664 - rangeMin] = 0x9ECC;
            mapDataStd [rangeNo] [0xC665 - rangeMin] = 0x7064;
            mapDataStd [rangeNo] [0xC666 - rangeMin] = 0x77DA;
            mapDataStd [rangeNo] [0xC667 - rangeMin] = 0x8B9A;
            mapDataStd [rangeNo] [0xC668 - rangeMin] = 0x9477;
            mapDataStd [rangeNo] [0xC669 - rangeMin] = 0x97C9;
            mapDataStd [rangeNo] [0xC66A - rangeMin] = 0x9A62;
            mapDataStd [rangeNo] [0xC66B - rangeMin] = 0x9A65;
            mapDataStd [rangeNo] [0xC66C - rangeMin] = 0x7E9C;
            mapDataStd [rangeNo] [0xC66D - rangeMin] = 0x8B9C;
            mapDataStd [rangeNo] [0xC66E - rangeMin] = 0x8EAA;
            mapDataStd [rangeNo] [0xC66F - rangeMin] = 0x91C5;

            mapDataStd [rangeNo] [0xC670 - rangeMin] = 0x947D;
            mapDataStd [rangeNo] [0xC671 - rangeMin] = 0x947E;
            mapDataStd [rangeNo] [0xC672 - rangeMin] = 0x947C;
            mapDataStd [rangeNo] [0xC673 - rangeMin] = 0x9C77;
            mapDataStd [rangeNo] [0xC674 - rangeMin] = 0x9C78;
            mapDataStd [rangeNo] [0xC675 - rangeMin] = 0x9EF7;
            mapDataStd [rangeNo] [0xC676 - rangeMin] = 0x8C54;
            mapDataStd [rangeNo] [0xC677 - rangeMin] = 0x947F;
            mapDataStd [rangeNo] [0xC678 - rangeMin] = 0x9E1A;
            mapDataStd [rangeNo] [0xC679 - rangeMin] = 0x7228;
            mapDataStd [rangeNo] [0xC67A - rangeMin] = 0x9A6A;
            mapDataStd [rangeNo] [0xC67B - rangeMin] = 0x9B31;
            mapDataStd [rangeNo] [0xC67C - rangeMin] = 0x9E1B;
            mapDataStd [rangeNo] [0xC67D - rangeMin] = 0x9E1E;
            mapDataStd [rangeNo] [0xC67E - rangeMin] = 0x7C72;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xC6A1 - rangeMin] = 0x30FE;
            mapDataStd [rangeNo] [0xC6A2 - rangeMin] = 0x309D;
            mapDataStd [rangeNo] [0xC6A3 - rangeMin] = 0x309E;
            mapDataStd [rangeNo] [0xC6A4 - rangeMin] = 0x3005;
            mapDataStd [rangeNo] [0xC6A5 - rangeMin] = 0x3041;
            mapDataStd [rangeNo] [0xC6A6 - rangeMin] = 0x3042;
            mapDataStd [rangeNo] [0xC6A7 - rangeMin] = 0x3043;
            mapDataStd [rangeNo] [0xC6A8 - rangeMin] = 0x3044;
            mapDataStd [rangeNo] [0xC6A9 - rangeMin] = 0x3045;
            mapDataStd [rangeNo] [0xC6AA - rangeMin] = 0x3046;
            mapDataStd [rangeNo] [0xC6AB - rangeMin] = 0x3047;
            mapDataStd [rangeNo] [0xC6AC - rangeMin] = 0x3048;
            mapDataStd [rangeNo] [0xC6AD - rangeMin] = 0x3049;
            mapDataStd [rangeNo] [0xC6AE - rangeMin] = 0x304A;
            mapDataStd [rangeNo] [0xC6AF - rangeMin] = 0x304B;

            mapDataStd [rangeNo] [0xC6B0 - rangeMin] = 0x304C;
            mapDataStd [rangeNo] [0xC6B1 - rangeMin] = 0x304D;
            mapDataStd [rangeNo] [0xC6B2 - rangeMin] = 0x304E;
            mapDataStd [rangeNo] [0xC6B3 - rangeMin] = 0x304F;
            mapDataStd [rangeNo] [0xC6B4 - rangeMin] = 0x3050;
            mapDataStd [rangeNo] [0xC6B5 - rangeMin] = 0x3051;
            mapDataStd [rangeNo] [0xC6B6 - rangeMin] = 0x3052;
            mapDataStd [rangeNo] [0xC6B7 - rangeMin] = 0x3053;
            mapDataStd [rangeNo] [0xC6B8 - rangeMin] = 0x3054;
            mapDataStd [rangeNo] [0xC6B9 - rangeMin] = 0x3055;
            mapDataStd [rangeNo] [0xC6BA - rangeMin] = 0x3056;
            mapDataStd [rangeNo] [0xC6BB - rangeMin] = 0x3057;
            mapDataStd [rangeNo] [0xC6BC - rangeMin] = 0x3058;
            mapDataStd [rangeNo] [0xC6BD - rangeMin] = 0x3059;
            mapDataStd [rangeNo] [0xC6BE - rangeMin] = 0x305A;
            mapDataStd [rangeNo] [0xC6BF - rangeMin] = 0x305B;

            mapDataStd [rangeNo] [0xC6C0 - rangeMin] = 0x305C;
            mapDataStd [rangeNo] [0xC6C1 - rangeMin] = 0x305D;
            mapDataStd [rangeNo] [0xC6C2 - rangeMin] = 0x305E;
            mapDataStd [rangeNo] [0xC6C3 - rangeMin] = 0x305F;
            mapDataStd [rangeNo] [0xC6C4 - rangeMin] = 0x3060;
            mapDataStd [rangeNo] [0xC6C5 - rangeMin] = 0x3061;
            mapDataStd [rangeNo] [0xC6C6 - rangeMin] = 0x3062;
            mapDataStd [rangeNo] [0xC6C7 - rangeMin] = 0x3063;
            mapDataStd [rangeNo] [0xC6C8 - rangeMin] = 0x3064;
            mapDataStd [rangeNo] [0xC6C9 - rangeMin] = 0x3065;
            mapDataStd [rangeNo] [0xC6CA - rangeMin] = 0x3066;
            mapDataStd [rangeNo] [0xC6CB - rangeMin] = 0x3067;
            mapDataStd [rangeNo] [0xC6CC - rangeMin] = 0x3068;
            mapDataStd [rangeNo] [0xC6CD - rangeMin] = 0x3069;
            mapDataStd [rangeNo] [0xC6CE - rangeMin] = 0x306A;
            mapDataStd [rangeNo] [0xC6CF - rangeMin] = 0x306B;

            mapDataStd [rangeNo] [0xC6D0 - rangeMin] = 0x306C;
            mapDataStd [rangeNo] [0xC6D1 - rangeMin] = 0x306D;
            mapDataStd [rangeNo] [0xC6D2 - rangeMin] = 0x306E;
            mapDataStd [rangeNo] [0xC6D3 - rangeMin] = 0x306F;
            mapDataStd [rangeNo] [0xC6D4 - rangeMin] = 0x3070;
            mapDataStd [rangeNo] [0xC6D5 - rangeMin] = 0x3071;
            mapDataStd [rangeNo] [0xC6D6 - rangeMin] = 0x3072;
            mapDataStd [rangeNo] [0xC6D7 - rangeMin] = 0x3073;
            mapDataStd [rangeNo] [0xC6D8 - rangeMin] = 0x3074;
            mapDataStd [rangeNo] [0xC6D9 - rangeMin] = 0x3075;
            mapDataStd [rangeNo] [0xC6DA - rangeMin] = 0x3076;
            mapDataStd [rangeNo] [0xC6DB - rangeMin] = 0x3077;
            mapDataStd [rangeNo] [0xC6DC - rangeMin] = 0x3078;
            mapDataStd [rangeNo] [0xC6DD - rangeMin] = 0x3079;
            mapDataStd [rangeNo] [0xC6DE - rangeMin] = 0x307A;
            mapDataStd [rangeNo] [0xC6DF - rangeMin] = 0x307B;

            mapDataStd [rangeNo] [0xC6E0 - rangeMin] = 0x307C;
            mapDataStd [rangeNo] [0xC6E1 - rangeMin] = 0x307D;
            mapDataStd [rangeNo] [0xC6E2 - rangeMin] = 0x307E;
            mapDataStd [rangeNo] [0xC6E3 - rangeMin] = 0x307F;
            mapDataStd [rangeNo] [0xC6E4 - rangeMin] = 0x3080;
            mapDataStd [rangeNo] [0xC6E5 - rangeMin] = 0x3081;
            mapDataStd [rangeNo] [0xC6E6 - rangeMin] = 0x3082;
            mapDataStd [rangeNo] [0xC6E7 - rangeMin] = 0x3083;
            mapDataStd [rangeNo] [0xC6E8 - rangeMin] = 0x3084;
            mapDataStd [rangeNo] [0xC6E9 - rangeMin] = 0x3085;
            mapDataStd [rangeNo] [0xC6EA - rangeMin] = 0x3086;
            mapDataStd [rangeNo] [0xC6EB - rangeMin] = 0x3087;
            mapDataStd [rangeNo] [0xC6EC - rangeMin] = 0x3088;
            mapDataStd [rangeNo] [0xC6ED - rangeMin] = 0x3089;
            mapDataStd [rangeNo] [0xC6EE - rangeMin] = 0x308A;
            mapDataStd [rangeNo] [0xC6EF - rangeMin] = 0x308B;

            mapDataStd [rangeNo] [0xC6F0 - rangeMin] = 0x308C;
            mapDataStd [rangeNo] [0xC6F1 - rangeMin] = 0x308D;
            mapDataStd [rangeNo] [0xC6F2 - rangeMin] = 0x308E;
            mapDataStd [rangeNo] [0xC6F3 - rangeMin] = 0x308F;
            mapDataStd [rangeNo] [0xC6F4 - rangeMin] = 0x3090;
            mapDataStd [rangeNo] [0xC6F5 - rangeMin] = 0x3091;
            mapDataStd [rangeNo] [0xC6F6 - rangeMin] = 0x3092;
            mapDataStd [rangeNo] [0xC6F7 - rangeMin] = 0x3093;
            mapDataStd [rangeNo] [0xC6F8 - rangeMin] = 0x30A1;
            mapDataStd [rangeNo] [0xC6F9 - rangeMin] = 0x30A2;
            mapDataStd [rangeNo] [0xC6FA - rangeMin] = 0x30A3;
            mapDataStd [rangeNo] [0xC6FB - rangeMin] = 0x30A4;
            mapDataStd [rangeNo] [0xC6FC - rangeMin] = 0x30A5;
            mapDataStd [rangeNo] [0xC6FD - rangeMin] = 0x30A6;
            mapDataStd [rangeNo] [0xC6FE - rangeMin] = 0x30A7;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xC740 - rangeMin] = 0x30A8;
            mapDataStd [rangeNo] [0xC741 - rangeMin] = 0x30A9;
            mapDataStd [rangeNo] [0xC742 - rangeMin] = 0x30AA;
            mapDataStd [rangeNo] [0xC743 - rangeMin] = 0x30AB;
            mapDataStd [rangeNo] [0xC744 - rangeMin] = 0x30AC;
            mapDataStd [rangeNo] [0xC745 - rangeMin] = 0x30AD;
            mapDataStd [rangeNo] [0xC746 - rangeMin] = 0x30AE;
            mapDataStd [rangeNo] [0xC747 - rangeMin] = 0x30AF;
            mapDataStd [rangeNo] [0xC748 - rangeMin] = 0x30B0;
            mapDataStd [rangeNo] [0xC749 - rangeMin] = 0x30B1;
            mapDataStd [rangeNo] [0xC74A - rangeMin] = 0x30B2;
            mapDataStd [rangeNo] [0xC74B - rangeMin] = 0x30B3;
            mapDataStd [rangeNo] [0xC74C - rangeMin] = 0x30B4;
            mapDataStd [rangeNo] [0xC74D - rangeMin] = 0x30B5;
            mapDataStd [rangeNo] [0xC74E - rangeMin] = 0x30B6;
            mapDataStd [rangeNo] [0xC74F - rangeMin] = 0x30B7;

            mapDataStd [rangeNo] [0xC750 - rangeMin] = 0x30B8;
            mapDataStd [rangeNo] [0xC751 - rangeMin] = 0x30B9;
            mapDataStd [rangeNo] [0xC752 - rangeMin] = 0x30BA;
            mapDataStd [rangeNo] [0xC753 - rangeMin] = 0x30BB;
            mapDataStd [rangeNo] [0xC754 - rangeMin] = 0x30BC;
            mapDataStd [rangeNo] [0xC755 - rangeMin] = 0x30BD;
            mapDataStd [rangeNo] [0xC756 - rangeMin] = 0x30BE;
            mapDataStd [rangeNo] [0xC757 - rangeMin] = 0x30BF;
            mapDataStd [rangeNo] [0xC758 - rangeMin] = 0x30C0;
            mapDataStd [rangeNo] [0xC759 - rangeMin] = 0x30C1;
            mapDataStd [rangeNo] [0xC75A - rangeMin] = 0x30C2;
            mapDataStd [rangeNo] [0xC75B - rangeMin] = 0x30C3;
            mapDataStd [rangeNo] [0xC75C - rangeMin] = 0x30C4;
            mapDataStd [rangeNo] [0xC75D - rangeMin] = 0x30C5;
            mapDataStd [rangeNo] [0xC75E - rangeMin] = 0x30C6;
            mapDataStd [rangeNo] [0xC75F - rangeMin] = 0x30C7;

            mapDataStd [rangeNo] [0xC760 - rangeMin] = 0x30C8;
            mapDataStd [rangeNo] [0xC761 - rangeMin] = 0x30C9;
            mapDataStd [rangeNo] [0xC762 - rangeMin] = 0x30CA;
            mapDataStd [rangeNo] [0xC763 - rangeMin] = 0x30CB;
            mapDataStd [rangeNo] [0xC764 - rangeMin] = 0x30CC;
            mapDataStd [rangeNo] [0xC765 - rangeMin] = 0x30CD;
            mapDataStd [rangeNo] [0xC766 - rangeMin] = 0x30CE;
            mapDataStd [rangeNo] [0xC767 - rangeMin] = 0x30CF;
            mapDataStd [rangeNo] [0xC768 - rangeMin] = 0x30D0;
            mapDataStd [rangeNo] [0xC769 - rangeMin] = 0x30D1;
            mapDataStd [rangeNo] [0xC76A - rangeMin] = 0x30D2;
            mapDataStd [rangeNo] [0xC76B - rangeMin] = 0x30D3;
            mapDataStd [rangeNo] [0xC76C - rangeMin] = 0x30D4;
            mapDataStd [rangeNo] [0xC76D - rangeMin] = 0x30D5;
            mapDataStd [rangeNo] [0xC76E - rangeMin] = 0x30D6;
            mapDataStd [rangeNo] [0xC76F - rangeMin] = 0x30D7;

            mapDataStd [rangeNo] [0xC770 - rangeMin] = 0x30D8;
            mapDataStd [rangeNo] [0xC771 - rangeMin] = 0x30D9;
            mapDataStd [rangeNo] [0xC772 - rangeMin] = 0x30DA;
            mapDataStd [rangeNo] [0xC773 - rangeMin] = 0x30DB;
            mapDataStd [rangeNo] [0xC774 - rangeMin] = 0x30DC;
            mapDataStd [rangeNo] [0xC775 - rangeMin] = 0x30DD;
            mapDataStd [rangeNo] [0xC776 - rangeMin] = 0x30DE;
            mapDataStd [rangeNo] [0xC777 - rangeMin] = 0x30DF;
            mapDataStd [rangeNo] [0xC778 - rangeMin] = 0x30E0;
            mapDataStd [rangeNo] [0xC779 - rangeMin] = 0x30E1;
            mapDataStd [rangeNo] [0xC77A - rangeMin] = 0x30E2;
            mapDataStd [rangeNo] [0xC77B - rangeMin] = 0x30E3;
            mapDataStd [rangeNo] [0xC77C - rangeMin] = 0x30E4;
            mapDataStd [rangeNo] [0xC77D - rangeMin] = 0x30E5;
            mapDataStd [rangeNo] [0xC77E - rangeMin] = 0x30E6;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xC7A1 - rangeMin] = 0x30E7;
            mapDataStd [rangeNo] [0xC7A2 - rangeMin] = 0x30E8;
            mapDataStd [rangeNo] [0xC7A3 - rangeMin] = 0x30E9;
            mapDataStd [rangeNo] [0xC7A4 - rangeMin] = 0x30EA;
            mapDataStd [rangeNo] [0xC7A5 - rangeMin] = 0x30EB;
            mapDataStd [rangeNo] [0xC7A6 - rangeMin] = 0x30EC;
            mapDataStd [rangeNo] [0xC7A7 - rangeMin] = 0x30ED;
            mapDataStd [rangeNo] [0xC7A8 - rangeMin] = 0x30EE;
            mapDataStd [rangeNo] [0xC7A9 - rangeMin] = 0x30EF;
            mapDataStd [rangeNo] [0xC7AA - rangeMin] = 0x30F0;
            mapDataStd [rangeNo] [0xC7AB - rangeMin] = 0x30F1;
            mapDataStd [rangeNo] [0xC7AC - rangeMin] = 0x30F2;
            mapDataStd [rangeNo] [0xC7AD - rangeMin] = 0x30F3;
            mapDataStd [rangeNo] [0xC7AE - rangeMin] = 0x30F4;
            mapDataStd [rangeNo] [0xC7AF - rangeMin] = 0x30F5;

            mapDataStd [rangeNo] [0xC7B0 - rangeMin] = 0x30F6;
            mapDataStd [rangeNo] [0xC7B1 - rangeMin] = 0x0414;
            mapDataStd [rangeNo] [0xC7B2 - rangeMin] = 0x0415;
            mapDataStd [rangeNo] [0xC7B3 - rangeMin] = 0x0401;
            mapDataStd [rangeNo] [0xC7B4 - rangeMin] = 0x0416;
            mapDataStd [rangeNo] [0xC7B5 - rangeMin] = 0x0417;
            mapDataStd [rangeNo] [0xC7B6 - rangeMin] = 0x0418;
            mapDataStd [rangeNo] [0xC7B7 - rangeMin] = 0x0419;
            mapDataStd [rangeNo] [0xC7B8 - rangeMin] = 0x041A;
            mapDataStd [rangeNo] [0xC7B9 - rangeMin] = 0x041B;
            mapDataStd [rangeNo] [0xC7BA - rangeMin] = 0x041C;
            mapDataStd [rangeNo] [0xC7BB - rangeMin] = 0x0423;
            mapDataStd [rangeNo] [0xC7BC - rangeMin] = 0x0424;
            mapDataStd [rangeNo] [0xC7BD - rangeMin] = 0x0425;
            mapDataStd [rangeNo] [0xC7BE - rangeMin] = 0x0426;
            mapDataStd [rangeNo] [0xC7BF - rangeMin] = 0x0427;

            mapDataStd [rangeNo] [0xC7C0 - rangeMin] = 0x0428;
            mapDataStd [rangeNo] [0xC7C1 - rangeMin] = 0x0429;
            mapDataStd [rangeNo] [0xC7C2 - rangeMin] = 0x042A;
            mapDataStd [rangeNo] [0xC7C3 - rangeMin] = 0x042B;
            mapDataStd [rangeNo] [0xC7C4 - rangeMin] = 0x042C;
            mapDataStd [rangeNo] [0xC7C5 - rangeMin] = 0x042D;
            mapDataStd [rangeNo] [0xC7C6 - rangeMin] = 0x042E;
            mapDataStd [rangeNo] [0xC7C7 - rangeMin] = 0x042F;
            mapDataStd [rangeNo] [0xC7C8 - rangeMin] = 0x0430;
            mapDataStd [rangeNo] [0xC7C9 - rangeMin] = 0x0431;
            mapDataStd [rangeNo] [0xC7CA - rangeMin] = 0x0432;
            mapDataStd [rangeNo] [0xC7CB - rangeMin] = 0x0433;
            mapDataStd [rangeNo] [0xC7CC - rangeMin] = 0x0434;
            mapDataStd [rangeNo] [0xC7CD - rangeMin] = 0x0435;
            mapDataStd [rangeNo] [0xC7CE - rangeMin] = 0x0451;
            mapDataStd [rangeNo] [0xC7CF - rangeMin] = 0x0436;

            mapDataStd [rangeNo] [0xC7D0 - rangeMin] = 0x0437;
            mapDataStd [rangeNo] [0xC7D1 - rangeMin] = 0x0438;
            mapDataStd [rangeNo] [0xC7D2 - rangeMin] = 0x0439;
            mapDataStd [rangeNo] [0xC7D3 - rangeMin] = 0x043A;
            mapDataStd [rangeNo] [0xC7D4 - rangeMin] = 0x043B;
            mapDataStd [rangeNo] [0xC7D5 - rangeMin] = 0x043C;
            mapDataStd [rangeNo] [0xC7D6 - rangeMin] = 0x043D;
            mapDataStd [rangeNo] [0xC7D7 - rangeMin] = 0x043E;
            mapDataStd [rangeNo] [0xC7D8 - rangeMin] = 0x043F;
            mapDataStd [rangeNo] [0xC7D9 - rangeMin] = 0x0440;
            mapDataStd [rangeNo] [0xC7DA - rangeMin] = 0x0441;
            mapDataStd [rangeNo] [0xC7DB - rangeMin] = 0x0442;
            mapDataStd [rangeNo] [0xC7DC - rangeMin] = 0x0443;
            mapDataStd [rangeNo] [0xC7DD - rangeMin] = 0x0444;
            mapDataStd [rangeNo] [0xC7DE - rangeMin] = 0x0445;
            mapDataStd [rangeNo] [0xC7DF - rangeMin] = 0x0446;

            mapDataStd [rangeNo] [0xC7E0 - rangeMin] = 0x0447;
            mapDataStd [rangeNo] [0xC7E1 - rangeMin] = 0x0448;
            mapDataStd [rangeNo] [0xC7E2 - rangeMin] = 0x0449;
            mapDataStd [rangeNo] [0xC7E3 - rangeMin] = 0x044A;
            mapDataStd [rangeNo] [0xC7E4 - rangeMin] = 0x044B;
            mapDataStd [rangeNo] [0xC7E5 - rangeMin] = 0x044C;
            mapDataStd [rangeNo] [0xC7E6 - rangeMin] = 0x044D;
            mapDataStd [rangeNo] [0xC7E7 - rangeMin] = 0x044E;
            mapDataStd [rangeNo] [0xC7E8 - rangeMin] = 0x044F;
            mapDataStd [rangeNo] [0xC7E9 - rangeMin] = 0x2460;
            mapDataStd [rangeNo] [0xC7EA - rangeMin] = 0x2461;
            mapDataStd [rangeNo] [0xC7EB - rangeMin] = 0x2462;
            mapDataStd [rangeNo] [0xC7EC - rangeMin] = 0x2463;
            mapDataStd [rangeNo] [0xC7ED - rangeMin] = 0x2464;
            mapDataStd [rangeNo] [0xC7EE - rangeMin] = 0x2465;
            mapDataStd [rangeNo] [0xC7EF - rangeMin] = 0x2466;

            mapDataStd [rangeNo] [0xC7F0 - rangeMin] = 0x2467;
            mapDataStd [rangeNo] [0xC7F1 - rangeMin] = 0x2468;
            mapDataStd [rangeNo] [0xC7F2 - rangeMin] = 0x2469;
            mapDataStd [rangeNo] [0xC7F3 - rangeMin] = 0x2474;
            mapDataStd [rangeNo] [0xC7F4 - rangeMin] = 0x2475;
            mapDataStd [rangeNo] [0xC7F5 - rangeMin] = 0x2476;
            mapDataStd [rangeNo] [0xC7F6 - rangeMin] = 0x2477;
            mapDataStd [rangeNo] [0xC7F7 - rangeMin] = 0x2478;
            mapDataStd [rangeNo] [0xC7F8 - rangeMin] = 0x2479;
            mapDataStd [rangeNo] [0xC7F9 - rangeMin] = 0x247A;
            mapDataStd [rangeNo] [0xC7FA - rangeMin] = 0x247B;
            mapDataStd [rangeNo] [0xC7FB - rangeMin] = 0x247C;
            mapDataStd [rangeNo] [0xC7FC - rangeMin] = 0x247D;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xC940 - rangeMin] = 0x4E42;
            mapDataStd [rangeNo] [0xC941 - rangeMin] = 0x4E5C;
            mapDataStd [rangeNo] [0xC942 - rangeMin] = 0x51F5;
            mapDataStd [rangeNo] [0xC943 - rangeMin] = 0x531A;
            mapDataStd [rangeNo] [0xC944 - rangeMin] = 0x5382;
            mapDataStd [rangeNo] [0xC945 - rangeMin] = 0x4E07;
            mapDataStd [rangeNo] [0xC946 - rangeMin] = 0x4E0C;
            mapDataStd [rangeNo] [0xC947 - rangeMin] = 0x4E47;
            mapDataStd [rangeNo] [0xC948 - rangeMin] = 0x4E8D;
            mapDataStd [rangeNo] [0xC949 - rangeMin] = 0x56D7;
            mapDataStd [rangeNo] [0xC94A - rangeMin] = 0xFA0C;
            mapDataStd [rangeNo] [0xC94B - rangeMin] = 0x5C6E;
            mapDataStd [rangeNo] [0xC94C - rangeMin] = 0x5F73;
            mapDataStd [rangeNo] [0xC94D - rangeMin] = 0x4E0F;
            mapDataStd [rangeNo] [0xC94E - rangeMin] = 0x5187;
            mapDataStd [rangeNo] [0xC94F - rangeMin] = 0x4E0E;
            mapDataStd [rangeNo] [0xC950 - rangeMin] = 0x4E2E;
            mapDataStd [rangeNo] [0xC951 - rangeMin] = 0x4E93;
            mapDataStd [rangeNo] [0xC952 - rangeMin] = 0x4EC2;
            mapDataStd [rangeNo] [0xC953 - rangeMin] = 0x4EC9;
            mapDataStd [rangeNo] [0xC954 - rangeMin] = 0x4EC8;
            mapDataStd [rangeNo] [0xC955 - rangeMin] = 0x5198;
            mapDataStd [rangeNo] [0xC956 - rangeMin] = 0x52FC;
            mapDataStd [rangeNo] [0xC957 - rangeMin] = 0x536C;
            mapDataStd [rangeNo] [0xC958 - rangeMin] = 0x53B9;
            mapDataStd [rangeNo] [0xC959 - rangeMin] = 0x5720;
            mapDataStd [rangeNo] [0xC95A - rangeMin] = 0x5903;
            mapDataStd [rangeNo] [0xC95B - rangeMin] = 0x592C;
            mapDataStd [rangeNo] [0xC95C - rangeMin] = 0x5C10;
            mapDataStd [rangeNo] [0xC95D - rangeMin] = 0x5DFF;
            mapDataStd [rangeNo] [0xC95E - rangeMin] = 0x65E1;
            mapDataStd [rangeNo] [0xC95F - rangeMin] = 0x6BB3;

            mapDataStd [rangeNo] [0xC960 - rangeMin] = 0x6BCC;
            mapDataStd [rangeNo] [0xC961 - rangeMin] = 0x6C14;
            mapDataStd [rangeNo] [0xC962 - rangeMin] = 0x723F;
            mapDataStd [rangeNo] [0xC963 - rangeMin] = 0x4E31;
            mapDataStd [rangeNo] [0xC964 - rangeMin] = 0x4E3C;
            mapDataStd [rangeNo] [0xC965 - rangeMin] = 0x4EE8;
            mapDataStd [rangeNo] [0xC966 - rangeMin] = 0x4EDC;
            mapDataStd [rangeNo] [0xC967 - rangeMin] = 0x4EE9;
            mapDataStd [rangeNo] [0xC968 - rangeMin] = 0x4EE1;
            mapDataStd [rangeNo] [0xC969 - rangeMin] = 0x4EDD;
            mapDataStd [rangeNo] [0xC96A - rangeMin] = 0x4EDA;
            mapDataStd [rangeNo] [0xC96B - rangeMin] = 0x520C;
            mapDataStd [rangeNo] [0xC96C - rangeMin] = 0x531C;
            mapDataStd [rangeNo] [0xC96D - rangeMin] = 0x534C;
            mapDataStd [rangeNo] [0xC96E - rangeMin] = 0x5722;
            mapDataStd [rangeNo] [0xC96F - rangeMin] = 0x5723;

            mapDataStd [rangeNo] [0xC970 - rangeMin] = 0x5917;
            mapDataStd [rangeNo] [0xC971 - rangeMin] = 0x592F;
            mapDataStd [rangeNo] [0xC972 - rangeMin] = 0x5B81;
            mapDataStd [rangeNo] [0xC973 - rangeMin] = 0x5B84;
            mapDataStd [rangeNo] [0xC974 - rangeMin] = 0x5C12;
            mapDataStd [rangeNo] [0xC975 - rangeMin] = 0x5C3B;
            mapDataStd [rangeNo] [0xC976 - rangeMin] = 0x5C74;
            mapDataStd [rangeNo] [0xC977 - rangeMin] = 0x5C73;
            mapDataStd [rangeNo] [0xC978 - rangeMin] = 0x5E04;
            mapDataStd [rangeNo] [0xC979 - rangeMin] = 0x5E80;
            mapDataStd [rangeNo] [0xC97A - rangeMin] = 0x5E82;
            mapDataStd [rangeNo] [0xC97B - rangeMin] = 0x5FC9;
            mapDataStd [rangeNo] [0xC97C - rangeMin] = 0x6209;
            mapDataStd [rangeNo] [0xC97D - rangeMin] = 0x6250;
            mapDataStd [rangeNo] [0xC97E - rangeMin] = 0x6C15;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xC9A1 - rangeMin] = 0x6C36;
            mapDataStd [rangeNo] [0xC9A2 - rangeMin] = 0x6C43;
            mapDataStd [rangeNo] [0xC9A3 - rangeMin] = 0x6C3F;
            mapDataStd [rangeNo] [0xC9A4 - rangeMin] = 0x6C3B;
            mapDataStd [rangeNo] [0xC9A5 - rangeMin] = 0x72AE;
            mapDataStd [rangeNo] [0xC9A6 - rangeMin] = 0x72B0;
            mapDataStd [rangeNo] [0xC9A7 - rangeMin] = 0x738A;
            mapDataStd [rangeNo] [0xC9A8 - rangeMin] = 0x79B8;
            mapDataStd [rangeNo] [0xC9A9 - rangeMin] = 0x808A;
            mapDataStd [rangeNo] [0xC9AA - rangeMin] = 0x961E;
            mapDataStd [rangeNo] [0xC9AB - rangeMin] = 0x4F0E;
            mapDataStd [rangeNo] [0xC9AC - rangeMin] = 0x4F18;
            mapDataStd [rangeNo] [0xC9AD - rangeMin] = 0x4F2C;
            mapDataStd [rangeNo] [0xC9AE - rangeMin] = 0x4EF5;
            mapDataStd [rangeNo] [0xC9AF - rangeMin] = 0x4F14;

            mapDataStd [rangeNo] [0xC9B0 - rangeMin] = 0x4EF1;
            mapDataStd [rangeNo] [0xC9B1 - rangeMin] = 0x4F00;
            mapDataStd [rangeNo] [0xC9B2 - rangeMin] = 0x4EF7;
            mapDataStd [rangeNo] [0xC9B3 - rangeMin] = 0x4F08;
            mapDataStd [rangeNo] [0xC9B4 - rangeMin] = 0x4F1D;
            mapDataStd [rangeNo] [0xC9B5 - rangeMin] = 0x4F02;
            mapDataStd [rangeNo] [0xC9B6 - rangeMin] = 0x4F05;
            mapDataStd [rangeNo] [0xC9B7 - rangeMin] = 0x4F22;
            mapDataStd [rangeNo] [0xC9B8 - rangeMin] = 0x4F13;
            mapDataStd [rangeNo] [0xC9B9 - rangeMin] = 0x4F04;
            mapDataStd [rangeNo] [0xC9BA - rangeMin] = 0x4EF4;
            mapDataStd [rangeNo] [0xC9BB - rangeMin] = 0x4F12;
            mapDataStd [rangeNo] [0xC9BC - rangeMin] = 0x51B1;
            mapDataStd [rangeNo] [0xC9BD - rangeMin] = 0x5213;
            mapDataStd [rangeNo] [0xC9BE - rangeMin] = 0x5209;
            mapDataStd [rangeNo] [0xC9BF - rangeMin] = 0x5210;

            mapDataStd [rangeNo] [0xC9C0 - rangeMin] = 0x52A6;
            mapDataStd [rangeNo] [0xC9C1 - rangeMin] = 0x5322;
            mapDataStd [rangeNo] [0xC9C2 - rangeMin] = 0x531F;
            mapDataStd [rangeNo] [0xC9C3 - rangeMin] = 0x534D;
            mapDataStd [rangeNo] [0xC9C4 - rangeMin] = 0x538A;
            mapDataStd [rangeNo] [0xC9C5 - rangeMin] = 0x5407;
            mapDataStd [rangeNo] [0xC9C6 - rangeMin] = 0x56E1;
            mapDataStd [rangeNo] [0xC9C7 - rangeMin] = 0x56DF;
            mapDataStd [rangeNo] [0xC9C8 - rangeMin] = 0x572E;
            mapDataStd [rangeNo] [0xC9C9 - rangeMin] = 0x572A;
            mapDataStd [rangeNo] [0xC9CA - rangeMin] = 0x5734;
            mapDataStd [rangeNo] [0xC9CB - rangeMin] = 0x593C;
            mapDataStd [rangeNo] [0xC9CC - rangeMin] = 0x5980;
            mapDataStd [rangeNo] [0xC9CD - rangeMin] = 0x597C;
            mapDataStd [rangeNo] [0xC9CE - rangeMin] = 0x5985;
            mapDataStd [rangeNo] [0xC9CF - rangeMin] = 0x597B;

            mapDataStd [rangeNo] [0xC9D0 - rangeMin] = 0x597E;
            mapDataStd [rangeNo] [0xC9D1 - rangeMin] = 0x5977;
            mapDataStd [rangeNo] [0xC9D2 - rangeMin] = 0x597F;
            mapDataStd [rangeNo] [0xC9D3 - rangeMin] = 0x5B56;
            mapDataStd [rangeNo] [0xC9D4 - rangeMin] = 0x5C15;
            mapDataStd [rangeNo] [0xC9D5 - rangeMin] = 0x5C25;
            mapDataStd [rangeNo] [0xC9D6 - rangeMin] = 0x5C7C;
            mapDataStd [rangeNo] [0xC9D7 - rangeMin] = 0x5C7A;
            mapDataStd [rangeNo] [0xC9D8 - rangeMin] = 0x5C7B;
            mapDataStd [rangeNo] [0xC9D9 - rangeMin] = 0x5C7E;
            mapDataStd [rangeNo] [0xC9DA - rangeMin] = 0x5DDF;
            mapDataStd [rangeNo] [0xC9DB - rangeMin] = 0x5E75;
            mapDataStd [rangeNo] [0xC9DC - rangeMin] = 0x5E84;
            mapDataStd [rangeNo] [0xC9DD - rangeMin] = 0x5F02;
            mapDataStd [rangeNo] [0xC9DE - rangeMin] = 0x5F1A;
            mapDataStd [rangeNo] [0xC9DF - rangeMin] = 0x5F74;

            mapDataStd [rangeNo] [0xC9E0 - rangeMin] = 0x5FD5;
            mapDataStd [rangeNo] [0xC9E1 - rangeMin] = 0x5FD4;
            mapDataStd [rangeNo] [0xC9E2 - rangeMin] = 0x5FCF;
            mapDataStd [rangeNo] [0xC9E3 - rangeMin] = 0x625C;
            mapDataStd [rangeNo] [0xC9E4 - rangeMin] = 0x625E;
            mapDataStd [rangeNo] [0xC9E5 - rangeMin] = 0x6264;
            mapDataStd [rangeNo] [0xC9E6 - rangeMin] = 0x6261;
            mapDataStd [rangeNo] [0xC9E7 - rangeMin] = 0x6266;
            mapDataStd [rangeNo] [0xC9E8 - rangeMin] = 0x6262;
            mapDataStd [rangeNo] [0xC9E9 - rangeMin] = 0x6259;
            mapDataStd [rangeNo] [0xC9EA - rangeMin] = 0x6260;
            mapDataStd [rangeNo] [0xC9EB - rangeMin] = 0x625A;
            mapDataStd [rangeNo] [0xC9EC - rangeMin] = 0x6265;
            mapDataStd [rangeNo] [0xC9ED - rangeMin] = 0x65EF;
            mapDataStd [rangeNo] [0xC9EE - rangeMin] = 0x65EE;
            mapDataStd [rangeNo] [0xC9EF - rangeMin] = 0x673E;

            mapDataStd [rangeNo] [0xC9F0 - rangeMin] = 0x6739;
            mapDataStd [rangeNo] [0xC9F1 - rangeMin] = 0x6738;
            mapDataStd [rangeNo] [0xC9F2 - rangeMin] = 0x673B;
            mapDataStd [rangeNo] [0xC9F3 - rangeMin] = 0x673A;
            mapDataStd [rangeNo] [0xC9F4 - rangeMin] = 0x673F;
            mapDataStd [rangeNo] [0xC9F5 - rangeMin] = 0x673C;
            mapDataStd [rangeNo] [0xC9F6 - rangeMin] = 0x6733;
            mapDataStd [rangeNo] [0xC9F7 - rangeMin] = 0x6C18;
            mapDataStd [rangeNo] [0xC9F8 - rangeMin] = 0x6C46;
            mapDataStd [rangeNo] [0xC9F9 - rangeMin] = 0x6C52;
            mapDataStd [rangeNo] [0xC9FA - rangeMin] = 0x6C5C;
            mapDataStd [rangeNo] [0xC9FB - rangeMin] = 0x6C4F;
            mapDataStd [rangeNo] [0xC9FC - rangeMin] = 0x6C4A;
            mapDataStd [rangeNo] [0xC9FD - rangeMin] = 0x6C54;
            mapDataStd [rangeNo] [0xC9FE - rangeMin] = 0x6C4B;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xCA40 - rangeMin] = 0x6C4C;
            mapDataStd [rangeNo] [0xCA41 - rangeMin] = 0x7071;
            mapDataStd [rangeNo] [0xCA42 - rangeMin] = 0x725E;
            mapDataStd [rangeNo] [0xCA43 - rangeMin] = 0x72B4;
            mapDataStd [rangeNo] [0xCA44 - rangeMin] = 0x72B5;
            mapDataStd [rangeNo] [0xCA45 - rangeMin] = 0x738E;
            mapDataStd [rangeNo] [0xCA46 - rangeMin] = 0x752A;
            mapDataStd [rangeNo] [0xCA47 - rangeMin] = 0x767F;
            mapDataStd [rangeNo] [0xCA48 - rangeMin] = 0x7A75;
            mapDataStd [rangeNo] [0xCA49 - rangeMin] = 0x7F51;
            mapDataStd [rangeNo] [0xCA4A - rangeMin] = 0x8278;
            mapDataStd [rangeNo] [0xCA4B - rangeMin] = 0x827C;
            mapDataStd [rangeNo] [0xCA4C - rangeMin] = 0x8280;
            mapDataStd [rangeNo] [0xCA4D - rangeMin] = 0x827D;
            mapDataStd [rangeNo] [0xCA4E - rangeMin] = 0x827F;
            mapDataStd [rangeNo] [0xCA4F - rangeMin] = 0x864D;

            mapDataStd [rangeNo] [0xCA50 - rangeMin] = 0x897E;
            mapDataStd [rangeNo] [0xCA51 - rangeMin] = 0x9099;
            mapDataStd [rangeNo] [0xCA52 - rangeMin] = 0x9097;
            mapDataStd [rangeNo] [0xCA53 - rangeMin] = 0x9098;
            mapDataStd [rangeNo] [0xCA54 - rangeMin] = 0x909B;
            mapDataStd [rangeNo] [0xCA55 - rangeMin] = 0x9094;
            mapDataStd [rangeNo] [0xCA56 - rangeMin] = 0x9622;
            mapDataStd [rangeNo] [0xCA57 - rangeMin] = 0x9624;
            mapDataStd [rangeNo] [0xCA58 - rangeMin] = 0x9620;
            mapDataStd [rangeNo] [0xCA59 - rangeMin] = 0x9623;
            mapDataStd [rangeNo] [0xCA5A - rangeMin] = 0x4F56;
            mapDataStd [rangeNo] [0xCA5B - rangeMin] = 0x4F3B;
            mapDataStd [rangeNo] [0xCA5C - rangeMin] = 0x4F62;
            mapDataStd [rangeNo] [0xCA5D - rangeMin] = 0x4F49;
            mapDataStd [rangeNo] [0xCA5E - rangeMin] = 0x4F53;
            mapDataStd [rangeNo] [0xCA5F - rangeMin] = 0x4F64;

            mapDataStd [rangeNo] [0xCA60 - rangeMin] = 0x4F3E;
            mapDataStd [rangeNo] [0xCA61 - rangeMin] = 0x4F67;
            mapDataStd [rangeNo] [0xCA62 - rangeMin] = 0x4F52;
            mapDataStd [rangeNo] [0xCA63 - rangeMin] = 0x4F5F;
            mapDataStd [rangeNo] [0xCA64 - rangeMin] = 0x4F41;
            mapDataStd [rangeNo] [0xCA65 - rangeMin] = 0x4F58;
            mapDataStd [rangeNo] [0xCA66 - rangeMin] = 0x4F2D;
            mapDataStd [rangeNo] [0xCA67 - rangeMin] = 0x4F33;
            mapDataStd [rangeNo] [0xCA68 - rangeMin] = 0x4F3F;
            mapDataStd [rangeNo] [0xCA69 - rangeMin] = 0x4F61;
            mapDataStd [rangeNo] [0xCA6A - rangeMin] = 0x518F;
            mapDataStd [rangeNo] [0xCA6B - rangeMin] = 0x51B9;
            mapDataStd [rangeNo] [0xCA6C - rangeMin] = 0x521C;
            mapDataStd [rangeNo] [0xCA6D - rangeMin] = 0x521E;
            mapDataStd [rangeNo] [0xCA6E - rangeMin] = 0x5221;
            mapDataStd [rangeNo] [0xCA6F - rangeMin] = 0x52AD;

            mapDataStd [rangeNo] [0xCA70 - rangeMin] = 0x52AE;
            mapDataStd [rangeNo] [0xCA71 - rangeMin] = 0x5309;
            mapDataStd [rangeNo] [0xCA72 - rangeMin] = 0x5363;
            mapDataStd [rangeNo] [0xCA73 - rangeMin] = 0x5372;
            mapDataStd [rangeNo] [0xCA74 - rangeMin] = 0x538E;
            mapDataStd [rangeNo] [0xCA75 - rangeMin] = 0x538F;
            mapDataStd [rangeNo] [0xCA76 - rangeMin] = 0x5430;
            mapDataStd [rangeNo] [0xCA77 - rangeMin] = 0x5437;
            mapDataStd [rangeNo] [0xCA78 - rangeMin] = 0x542A;
            mapDataStd [rangeNo] [0xCA79 - rangeMin] = 0x5454;
            mapDataStd [rangeNo] [0xCA7A - rangeMin] = 0x5445;
            mapDataStd [rangeNo] [0xCA7B - rangeMin] = 0x5419;
            mapDataStd [rangeNo] [0xCA7C - rangeMin] = 0x541C;
            mapDataStd [rangeNo] [0xCA7D - rangeMin] = 0x5425;
            mapDataStd [rangeNo] [0xCA7E - rangeMin] = 0x5418;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xCAA1 - rangeMin] = 0x543D;
            mapDataStd [rangeNo] [0xCAA2 - rangeMin] = 0x544F;
            mapDataStd [rangeNo] [0xCAA3 - rangeMin] = 0x5441;
            mapDataStd [rangeNo] [0xCAA4 - rangeMin] = 0x5428;
            mapDataStd [rangeNo] [0xCAA5 - rangeMin] = 0x5424;
            mapDataStd [rangeNo] [0xCAA6 - rangeMin] = 0x5447;
            mapDataStd [rangeNo] [0xCAA7 - rangeMin] = 0x56EE;
            mapDataStd [rangeNo] [0xCAA8 - rangeMin] = 0x56E7;
            mapDataStd [rangeNo] [0xCAA9 - rangeMin] = 0x56E5;
            mapDataStd [rangeNo] [0xCAAA - rangeMin] = 0x5741;
            mapDataStd [rangeNo] [0xCAAB - rangeMin] = 0x5745;
            mapDataStd [rangeNo] [0xCAAC - rangeMin] = 0x574C;
            mapDataStd [rangeNo] [0xCAAD - rangeMin] = 0x5749;
            mapDataStd [rangeNo] [0xCAAE - rangeMin] = 0x574B;
            mapDataStd [rangeNo] [0xCAAF - rangeMin] = 0x5752;

            mapDataStd [rangeNo] [0xCAB0 - rangeMin] = 0x5906;
            mapDataStd [rangeNo] [0xCAB1 - rangeMin] = 0x5940;
            mapDataStd [rangeNo] [0xCAB2 - rangeMin] = 0x59A6;
            mapDataStd [rangeNo] [0xCAB3 - rangeMin] = 0x5998;
            mapDataStd [rangeNo] [0xCAB4 - rangeMin] = 0x59A0;
            mapDataStd [rangeNo] [0xCAB5 - rangeMin] = 0x5997;
            mapDataStd [rangeNo] [0xCAB6 - rangeMin] = 0x598E;
            mapDataStd [rangeNo] [0xCAB7 - rangeMin] = 0x59A2;
            mapDataStd [rangeNo] [0xCAB8 - rangeMin] = 0x5990;
            mapDataStd [rangeNo] [0xCAB9 - rangeMin] = 0x598F;
            mapDataStd [rangeNo] [0xCABA - rangeMin] = 0x59A7;
            mapDataStd [rangeNo] [0xCABB - rangeMin] = 0x59A1;
            mapDataStd [rangeNo] [0xCABC - rangeMin] = 0x5B8E;
            mapDataStd [rangeNo] [0xCABD - rangeMin] = 0x5B92;
            mapDataStd [rangeNo] [0xCABE - rangeMin] = 0x5C28;
            mapDataStd [rangeNo] [0xCABF - rangeMin] = 0x5C2A;

            mapDataStd [rangeNo] [0xCAC0 - rangeMin] = 0x5C8D;
            mapDataStd [rangeNo] [0xCAC1 - rangeMin] = 0x5C8F;
            mapDataStd [rangeNo] [0xCAC2 - rangeMin] = 0x5C88;
            mapDataStd [rangeNo] [0xCAC3 - rangeMin] = 0x5C8B;
            mapDataStd [rangeNo] [0xCAC4 - rangeMin] = 0x5C89;
            mapDataStd [rangeNo] [0xCAC5 - rangeMin] = 0x5C92;
            mapDataStd [rangeNo] [0xCAC6 - rangeMin] = 0x5C8A;
            mapDataStd [rangeNo] [0xCAC7 - rangeMin] = 0x5C86;
            mapDataStd [rangeNo] [0xCAC8 - rangeMin] = 0x5C93;
            mapDataStd [rangeNo] [0xCAC9 - rangeMin] = 0x5C95;
            mapDataStd [rangeNo] [0xCACA - rangeMin] = 0x5DE0;
            mapDataStd [rangeNo] [0xCACB - rangeMin] = 0x5E0A;
            mapDataStd [rangeNo] [0xCACC - rangeMin] = 0x5E0E;
            mapDataStd [rangeNo] [0xCACD - rangeMin] = 0x5E8B;
            mapDataStd [rangeNo] [0xCACE - rangeMin] = 0x5E89;
            mapDataStd [rangeNo] [0xCACF - rangeMin] = 0x5E8C;

            mapDataStd [rangeNo] [0xCAD0 - rangeMin] = 0x5E88;
            mapDataStd [rangeNo] [0xCAD1 - rangeMin] = 0x5E8D;
            mapDataStd [rangeNo] [0xCAD2 - rangeMin] = 0x5F05;
            mapDataStd [rangeNo] [0xCAD3 - rangeMin] = 0x5F1D;
            mapDataStd [rangeNo] [0xCAD4 - rangeMin] = 0x5F78;
            mapDataStd [rangeNo] [0xCAD5 - rangeMin] = 0x5F76;
            mapDataStd [rangeNo] [0xCAD6 - rangeMin] = 0x5FD2;
            mapDataStd [rangeNo] [0xCAD7 - rangeMin] = 0x5FD1;
            mapDataStd [rangeNo] [0xCAD8 - rangeMin] = 0x5FD0;
            mapDataStd [rangeNo] [0xCAD9 - rangeMin] = 0x5FED;
            mapDataStd [rangeNo] [0xCADA - rangeMin] = 0x5FE8;
            mapDataStd [rangeNo] [0xCADB - rangeMin] = 0x5FEE;
            mapDataStd [rangeNo] [0xCADC - rangeMin] = 0x5FF3;
            mapDataStd [rangeNo] [0xCADD - rangeMin] = 0x5FE1;
            mapDataStd [rangeNo] [0xCADE - rangeMin] = 0x5FE4;
            mapDataStd [rangeNo] [0xCADF - rangeMin] = 0x5FE3;

            mapDataStd [rangeNo] [0xCAE0 - rangeMin] = 0x5FFA;
            mapDataStd [rangeNo] [0xCAE1 - rangeMin] = 0x5FEF;
            mapDataStd [rangeNo] [0xCAE2 - rangeMin] = 0x5FF7;
            mapDataStd [rangeNo] [0xCAE3 - rangeMin] = 0x5FFB;
            mapDataStd [rangeNo] [0xCAE4 - rangeMin] = 0x6000;
            mapDataStd [rangeNo] [0xCAE5 - rangeMin] = 0x5FF4;
            mapDataStd [rangeNo] [0xCAE6 - rangeMin] = 0x623A;
            mapDataStd [rangeNo] [0xCAE7 - rangeMin] = 0x6283;
            mapDataStd [rangeNo] [0xCAE8 - rangeMin] = 0x628C;
            mapDataStd [rangeNo] [0xCAE9 - rangeMin] = 0x628E;
            mapDataStd [rangeNo] [0xCAEA - rangeMin] = 0x628F;
            mapDataStd [rangeNo] [0xCAEB - rangeMin] = 0x6294;
            mapDataStd [rangeNo] [0xCAEC - rangeMin] = 0x6287;
            mapDataStd [rangeNo] [0xCAED - rangeMin] = 0x6271;
            mapDataStd [rangeNo] [0xCAEE - rangeMin] = 0x627B;
            mapDataStd [rangeNo] [0xCAEF - rangeMin] = 0x627A;

            mapDataStd [rangeNo] [0xCAF0 - rangeMin] = 0x6270;
            mapDataStd [rangeNo] [0xCAF1 - rangeMin] = 0x6281;
            mapDataStd [rangeNo] [0xCAF2 - rangeMin] = 0x6288;
            mapDataStd [rangeNo] [0xCAF3 - rangeMin] = 0x6277;
            mapDataStd [rangeNo] [0xCAF4 - rangeMin] = 0x627D;
            mapDataStd [rangeNo] [0xCAF5 - rangeMin] = 0x6272;
            mapDataStd [rangeNo] [0xCAF6 - rangeMin] = 0x6274;
            mapDataStd [rangeNo] [0xCAF7 - rangeMin] = 0x6537;
            mapDataStd [rangeNo] [0xCAF8 - rangeMin] = 0x65F0;
            mapDataStd [rangeNo] [0xCAF9 - rangeMin] = 0x65F4;
            mapDataStd [rangeNo] [0xCAFA - rangeMin] = 0x65F3;
            mapDataStd [rangeNo] [0xCAFB - rangeMin] = 0x65F2;
            mapDataStd [rangeNo] [0xCAFC - rangeMin] = 0x65F5;
            mapDataStd [rangeNo] [0xCAFD - rangeMin] = 0x6745;
            mapDataStd [rangeNo] [0xCAFE - rangeMin] = 0x6747;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xCB40 - rangeMin] = 0x6759;
            mapDataStd [rangeNo] [0xCB41 - rangeMin] = 0x6755;
            mapDataStd [rangeNo] [0xCB42 - rangeMin] = 0x674C;
            mapDataStd [rangeNo] [0xCB43 - rangeMin] = 0x6748;
            mapDataStd [rangeNo] [0xCB44 - rangeMin] = 0x675D;
            mapDataStd [rangeNo] [0xCB45 - rangeMin] = 0x674D;
            mapDataStd [rangeNo] [0xCB46 - rangeMin] = 0x675A;
            mapDataStd [rangeNo] [0xCB47 - rangeMin] = 0x674B;
            mapDataStd [rangeNo] [0xCB48 - rangeMin] = 0x6BD0;
            mapDataStd [rangeNo] [0xCB49 - rangeMin] = 0x6C19;
            mapDataStd [rangeNo] [0xCB4A - rangeMin] = 0x6C1A;
            mapDataStd [rangeNo] [0xCB4B - rangeMin] = 0x6C78;
            mapDataStd [rangeNo] [0xCB4C - rangeMin] = 0x6C67;
            mapDataStd [rangeNo] [0xCB4D - rangeMin] = 0x6C6B;
            mapDataStd [rangeNo] [0xCB4E - rangeMin] = 0x6C84;
            mapDataStd [rangeNo] [0xCB4F - rangeMin] = 0x6C8B;

            mapDataStd [rangeNo] [0xCB50 - rangeMin] = 0x6C8F;
            mapDataStd [rangeNo] [0xCB51 - rangeMin] = 0x6C71;
            mapDataStd [rangeNo] [0xCB52 - rangeMin] = 0x6C6F;
            mapDataStd [rangeNo] [0xCB53 - rangeMin] = 0x6C69;
            mapDataStd [rangeNo] [0xCB54 - rangeMin] = 0x6C9A;
            mapDataStd [rangeNo] [0xCB55 - rangeMin] = 0x6C6D;
            mapDataStd [rangeNo] [0xCB56 - rangeMin] = 0x6C87;
            mapDataStd [rangeNo] [0xCB57 - rangeMin] = 0x6C95;
            mapDataStd [rangeNo] [0xCB58 - rangeMin] = 0x6C9C;
            mapDataStd [rangeNo] [0xCB59 - rangeMin] = 0x6C66;
            mapDataStd [rangeNo] [0xCB5A - rangeMin] = 0x6C73;
            mapDataStd [rangeNo] [0xCB5B - rangeMin] = 0x6C65;
            mapDataStd [rangeNo] [0xCB5C - rangeMin] = 0x6C7B;
            mapDataStd [rangeNo] [0xCB5D - rangeMin] = 0x6C8E;
            mapDataStd [rangeNo] [0xCB5E - rangeMin] = 0x7074;
            mapDataStd [rangeNo] [0xCB5F - rangeMin] = 0x707A;

            mapDataStd [rangeNo] [0xCB60 - rangeMin] = 0x7263;
            mapDataStd [rangeNo] [0xCB61 - rangeMin] = 0x72BF;
            mapDataStd [rangeNo] [0xCB62 - rangeMin] = 0x72BD;
            mapDataStd [rangeNo] [0xCB63 - rangeMin] = 0x72C3;
            mapDataStd [rangeNo] [0xCB64 - rangeMin] = 0x72C6;
            mapDataStd [rangeNo] [0xCB65 - rangeMin] = 0x72C1;
            mapDataStd [rangeNo] [0xCB66 - rangeMin] = 0x72BA;
            mapDataStd [rangeNo] [0xCB67 - rangeMin] = 0x72C5;
            mapDataStd [rangeNo] [0xCB68 - rangeMin] = 0x7395;
            mapDataStd [rangeNo] [0xCB69 - rangeMin] = 0x7397;
            mapDataStd [rangeNo] [0xCB6A - rangeMin] = 0x7393;
            mapDataStd [rangeNo] [0xCB6B - rangeMin] = 0x7394;
            mapDataStd [rangeNo] [0xCB6C - rangeMin] = 0x7392;
            mapDataStd [rangeNo] [0xCB6D - rangeMin] = 0x753A;
            mapDataStd [rangeNo] [0xCB6E - rangeMin] = 0x7539;
            mapDataStd [rangeNo] [0xCB6F - rangeMin] = 0x7594;

            mapDataStd [rangeNo] [0xCB70 - rangeMin] = 0x7595;
            mapDataStd [rangeNo] [0xCB71 - rangeMin] = 0x7681;
            mapDataStd [rangeNo] [0xCB72 - rangeMin] = 0x793D;
            mapDataStd [rangeNo] [0xCB73 - rangeMin] = 0x8034;
            mapDataStd [rangeNo] [0xCB74 - rangeMin] = 0x8095;
            mapDataStd [rangeNo] [0xCB75 - rangeMin] = 0x8099;
            mapDataStd [rangeNo] [0xCB76 - rangeMin] = 0x8090;
            mapDataStd [rangeNo] [0xCB77 - rangeMin] = 0x8092;
            mapDataStd [rangeNo] [0xCB78 - rangeMin] = 0x809C;
            mapDataStd [rangeNo] [0xCB79 - rangeMin] = 0x8290;
            mapDataStd [rangeNo] [0xCB7A - rangeMin] = 0x828F;
            mapDataStd [rangeNo] [0xCB7B - rangeMin] = 0x8285;
            mapDataStd [rangeNo] [0xCB7C - rangeMin] = 0x828E;
            mapDataStd [rangeNo] [0xCB7D - rangeMin] = 0x8291;
            mapDataStd [rangeNo] [0xCB7E - rangeMin] = 0x8293;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xCBA1 - rangeMin] = 0x828A;
            mapDataStd [rangeNo] [0xCBA2 - rangeMin] = 0x8283;
            mapDataStd [rangeNo] [0xCBA3 - rangeMin] = 0x8284;
            mapDataStd [rangeNo] [0xCBA4 - rangeMin] = 0x8C78;
            mapDataStd [rangeNo] [0xCBA5 - rangeMin] = 0x8FC9;
            mapDataStd [rangeNo] [0xCBA6 - rangeMin] = 0x8FBF;
            mapDataStd [rangeNo] [0xCBA7 - rangeMin] = 0x909F;
            mapDataStd [rangeNo] [0xCBA8 - rangeMin] = 0x90A1;
            mapDataStd [rangeNo] [0xCBA9 - rangeMin] = 0x90A5;
            mapDataStd [rangeNo] [0xCBAA - rangeMin] = 0x909E;
            mapDataStd [rangeNo] [0xCBAB - rangeMin] = 0x90A7;
            mapDataStd [rangeNo] [0xCBAC - rangeMin] = 0x90A0;
            mapDataStd [rangeNo] [0xCBAD - rangeMin] = 0x9630;
            mapDataStd [rangeNo] [0xCBAE - rangeMin] = 0x9628;
            mapDataStd [rangeNo] [0xCBAF - rangeMin] = 0x962F;

            mapDataStd [rangeNo] [0xCBB0 - rangeMin] = 0x962D;
            mapDataStd [rangeNo] [0xCBB1 - rangeMin] = 0x4E33;
            mapDataStd [rangeNo] [0xCBB2 - rangeMin] = 0x4F98;
            mapDataStd [rangeNo] [0xCBB3 - rangeMin] = 0x4F7C;
            mapDataStd [rangeNo] [0xCBB4 - rangeMin] = 0x4F85;
            mapDataStd [rangeNo] [0xCBB5 - rangeMin] = 0x4F7D;
            mapDataStd [rangeNo] [0xCBB6 - rangeMin] = 0x4F80;
            mapDataStd [rangeNo] [0xCBB7 - rangeMin] = 0x4F87;
            mapDataStd [rangeNo] [0xCBB8 - rangeMin] = 0x4F76;
            mapDataStd [rangeNo] [0xCBB9 - rangeMin] = 0x4F74;
            mapDataStd [rangeNo] [0xCBBA - rangeMin] = 0x4F89;
            mapDataStd [rangeNo] [0xCBBB - rangeMin] = 0x4F84;
            mapDataStd [rangeNo] [0xCBBC - rangeMin] = 0x4F77;
            mapDataStd [rangeNo] [0xCBBD - rangeMin] = 0x4F4C;
            mapDataStd [rangeNo] [0xCBBE - rangeMin] = 0x4F97;
            mapDataStd [rangeNo] [0xCBBF - rangeMin] = 0x4F6A;

            mapDataStd [rangeNo] [0xCBC0 - rangeMin] = 0x4F9A;
            mapDataStd [rangeNo] [0xCBC1 - rangeMin] = 0x4F79;
            mapDataStd [rangeNo] [0xCBC2 - rangeMin] = 0x4F81;
            mapDataStd [rangeNo] [0xCBC3 - rangeMin] = 0x4F78;
            mapDataStd [rangeNo] [0xCBC4 - rangeMin] = 0x4F90;
            mapDataStd [rangeNo] [0xCBC5 - rangeMin] = 0x4F9C;
            mapDataStd [rangeNo] [0xCBC6 - rangeMin] = 0x4F94;
            mapDataStd [rangeNo] [0xCBC7 - rangeMin] = 0x4F9E;
            mapDataStd [rangeNo] [0xCBC8 - rangeMin] = 0x4F92;
            mapDataStd [rangeNo] [0xCBC9 - rangeMin] = 0x4F82;
            mapDataStd [rangeNo] [0xCBCA - rangeMin] = 0x4F95;
            mapDataStd [rangeNo] [0xCBCB - rangeMin] = 0x4F6B;
            mapDataStd [rangeNo] [0xCBCC - rangeMin] = 0x4F6E;
            mapDataStd [rangeNo] [0xCBCD - rangeMin] = 0x519E;
            mapDataStd [rangeNo] [0xCBCE - rangeMin] = 0x51BC;
            mapDataStd [rangeNo] [0xCBCF - rangeMin] = 0x51BE;

            mapDataStd [rangeNo] [0xCBD0 - rangeMin] = 0x5235;
            mapDataStd [rangeNo] [0xCBD1 - rangeMin] = 0x5232;
            mapDataStd [rangeNo] [0xCBD2 - rangeMin] = 0x5233;
            mapDataStd [rangeNo] [0xCBD3 - rangeMin] = 0x5246;
            mapDataStd [rangeNo] [0xCBD4 - rangeMin] = 0x5231;
            mapDataStd [rangeNo] [0xCBD5 - rangeMin] = 0x52BC;
            mapDataStd [rangeNo] [0xCBD6 - rangeMin] = 0x530A;
            mapDataStd [rangeNo] [0xCBD7 - rangeMin] = 0x530B;
            mapDataStd [rangeNo] [0xCBD8 - rangeMin] = 0x533C;
            mapDataStd [rangeNo] [0xCBD9 - rangeMin] = 0x5392;
            mapDataStd [rangeNo] [0xCBDA - rangeMin] = 0x5394;
            mapDataStd [rangeNo] [0xCBDB - rangeMin] = 0x5487;
            mapDataStd [rangeNo] [0xCBDC - rangeMin] = 0x547F;
            mapDataStd [rangeNo] [0xCBDD - rangeMin] = 0x5481;
            mapDataStd [rangeNo] [0xCBDE - rangeMin] = 0x5491;
            mapDataStd [rangeNo] [0xCBDF - rangeMin] = 0x5482;

            mapDataStd [rangeNo] [0xCBE0 - rangeMin] = 0x5488;
            mapDataStd [rangeNo] [0xCBE1 - rangeMin] = 0x546B;
            mapDataStd [rangeNo] [0xCBE2 - rangeMin] = 0x547A;
            mapDataStd [rangeNo] [0xCBE3 - rangeMin] = 0x547E;
            mapDataStd [rangeNo] [0xCBE4 - rangeMin] = 0x5465;
            mapDataStd [rangeNo] [0xCBE5 - rangeMin] = 0x546C;
            mapDataStd [rangeNo] [0xCBE6 - rangeMin] = 0x5474;
            mapDataStd [rangeNo] [0xCBE7 - rangeMin] = 0x5466;
            mapDataStd [rangeNo] [0xCBE8 - rangeMin] = 0x548D;
            mapDataStd [rangeNo] [0xCBE9 - rangeMin] = 0x546F;
            mapDataStd [rangeNo] [0xCBEA - rangeMin] = 0x5461;
            mapDataStd [rangeNo] [0xCBEB - rangeMin] = 0x5460;
            mapDataStd [rangeNo] [0xCBEC - rangeMin] = 0x5498;
            mapDataStd [rangeNo] [0xCBED - rangeMin] = 0x5463;
            mapDataStd [rangeNo] [0xCBEE - rangeMin] = 0x5467;
            mapDataStd [rangeNo] [0xCBEF - rangeMin] = 0x5464;

            mapDataStd [rangeNo] [0xCBF0 - rangeMin] = 0x56F7;
            mapDataStd [rangeNo] [0xCBF1 - rangeMin] = 0x56F9;
            mapDataStd [rangeNo] [0xCBF2 - rangeMin] = 0x576F;
            mapDataStd [rangeNo] [0xCBF3 - rangeMin] = 0x5772;
            mapDataStd [rangeNo] [0xCBF4 - rangeMin] = 0x576D;
            mapDataStd [rangeNo] [0xCBF5 - rangeMin] = 0x576B;
            mapDataStd [rangeNo] [0xCBF6 - rangeMin] = 0x5771;
            mapDataStd [rangeNo] [0xCBF7 - rangeMin] = 0x5770;
            mapDataStd [rangeNo] [0xCBF8 - rangeMin] = 0x5776;
            mapDataStd [rangeNo] [0xCBF9 - rangeMin] = 0x5780;
            mapDataStd [rangeNo] [0xCBFA - rangeMin] = 0x5775;
            mapDataStd [rangeNo] [0xCBFB - rangeMin] = 0x577B;
            mapDataStd [rangeNo] [0xCBFC - rangeMin] = 0x5773;
            mapDataStd [rangeNo] [0xCBFD - rangeMin] = 0x5774;
            mapDataStd [rangeNo] [0xCBFE - rangeMin] = 0x5762;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xCC40 - rangeMin] = 0x5768;
            mapDataStd [rangeNo] [0xCC41 - rangeMin] = 0x577D;
            mapDataStd [rangeNo] [0xCC42 - rangeMin] = 0x590C;
            mapDataStd [rangeNo] [0xCC43 - rangeMin] = 0x5945;
            mapDataStd [rangeNo] [0xCC44 - rangeMin] = 0x59B5;
            mapDataStd [rangeNo] [0xCC45 - rangeMin] = 0x59BA;
            mapDataStd [rangeNo] [0xCC46 - rangeMin] = 0x59CF;
            mapDataStd [rangeNo] [0xCC47 - rangeMin] = 0x59CE;
            mapDataStd [rangeNo] [0xCC48 - rangeMin] = 0x59B2;
            mapDataStd [rangeNo] [0xCC49 - rangeMin] = 0x59CC;
            mapDataStd [rangeNo] [0xCC4A - rangeMin] = 0x59C1;
            mapDataStd [rangeNo] [0xCC4B - rangeMin] = 0x59B6;
            mapDataStd [rangeNo] [0xCC4C - rangeMin] = 0x59BC;
            mapDataStd [rangeNo] [0xCC4D - rangeMin] = 0x59C3;
            mapDataStd [rangeNo] [0xCC4E - rangeMin] = 0x59D6;
            mapDataStd [rangeNo] [0xCC4F - rangeMin] = 0x59B1;

            mapDataStd [rangeNo] [0xCC50 - rangeMin] = 0x59BD;
            mapDataStd [rangeNo] [0xCC51 - rangeMin] = 0x59C0;
            mapDataStd [rangeNo] [0xCC52 - rangeMin] = 0x59C8;
            mapDataStd [rangeNo] [0xCC53 - rangeMin] = 0x59B4;
            mapDataStd [rangeNo] [0xCC54 - rangeMin] = 0x59C7;
            mapDataStd [rangeNo] [0xCC55 - rangeMin] = 0x5B62;
            mapDataStd [rangeNo] [0xCC56 - rangeMin] = 0x5B65;
            mapDataStd [rangeNo] [0xCC57 - rangeMin] = 0x5B93;
            mapDataStd [rangeNo] [0xCC58 - rangeMin] = 0x5B95;
            mapDataStd [rangeNo] [0xCC59 - rangeMin] = 0x5C44;
            mapDataStd [rangeNo] [0xCC5A - rangeMin] = 0x5C47;
            mapDataStd [rangeNo] [0xCC5B - rangeMin] = 0x5CAE;
            mapDataStd [rangeNo] [0xCC5C - rangeMin] = 0x5CA4;
            mapDataStd [rangeNo] [0xCC5D - rangeMin] = 0x5CA0;
            mapDataStd [rangeNo] [0xCC5E - rangeMin] = 0x5CB5;
            mapDataStd [rangeNo] [0xCC5F - rangeMin] = 0x5CAF;

            mapDataStd [rangeNo] [0xCC60 - rangeMin] = 0x5CA8;
            mapDataStd [rangeNo] [0xCC61 - rangeMin] = 0x5CAC;
            mapDataStd [rangeNo] [0xCC62 - rangeMin] = 0x5C9F;
            mapDataStd [rangeNo] [0xCC63 - rangeMin] = 0x5CA3;
            mapDataStd [rangeNo] [0xCC64 - rangeMin] = 0x5CAD;
            mapDataStd [rangeNo] [0xCC65 - rangeMin] = 0x5CA2;
            mapDataStd [rangeNo] [0xCC66 - rangeMin] = 0x5CAA;
            mapDataStd [rangeNo] [0xCC67 - rangeMin] = 0x5CA7;
            mapDataStd [rangeNo] [0xCC68 - rangeMin] = 0x5C9D;
            mapDataStd [rangeNo] [0xCC69 - rangeMin] = 0x5CA5;
            mapDataStd [rangeNo] [0xCC6A - rangeMin] = 0x5CB6;
            mapDataStd [rangeNo] [0xCC6B - rangeMin] = 0x5CB0;
            mapDataStd [rangeNo] [0xCC6C - rangeMin] = 0x5CA6;
            mapDataStd [rangeNo] [0xCC6D - rangeMin] = 0x5E17;
            mapDataStd [rangeNo] [0xCC6E - rangeMin] = 0x5E14;
            mapDataStd [rangeNo] [0xCC6F - rangeMin] = 0x5E19;

            mapDataStd [rangeNo] [0xCC70 - rangeMin] = 0x5F28;
            mapDataStd [rangeNo] [0xCC71 - rangeMin] = 0x5F22;
            mapDataStd [rangeNo] [0xCC72 - rangeMin] = 0x5F23;
            mapDataStd [rangeNo] [0xCC73 - rangeMin] = 0x5F24;
            mapDataStd [rangeNo] [0xCC74 - rangeMin] = 0x5F54;
            mapDataStd [rangeNo] [0xCC75 - rangeMin] = 0x5F82;
            mapDataStd [rangeNo] [0xCC76 - rangeMin] = 0x5F7E;
            mapDataStd [rangeNo] [0xCC77 - rangeMin] = 0x5F7D;
            mapDataStd [rangeNo] [0xCC78 - rangeMin] = 0x5FDE;
            mapDataStd [rangeNo] [0xCC79 - rangeMin] = 0x5FE5;
            mapDataStd [rangeNo] [0xCC7A - rangeMin] = 0x602D;
            mapDataStd [rangeNo] [0xCC7B - rangeMin] = 0x6026;
            mapDataStd [rangeNo] [0xCC7C - rangeMin] = 0x6019;
            mapDataStd [rangeNo] [0xCC7D - rangeMin] = 0x6032;
            mapDataStd [rangeNo] [0xCC7E - rangeMin] = 0x600B;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xCCA1 - rangeMin] = 0x6034;
            mapDataStd [rangeNo] [0xCCA2 - rangeMin] = 0x600A;
            mapDataStd [rangeNo] [0xCCA3 - rangeMin] = 0x6017;
            mapDataStd [rangeNo] [0xCCA4 - rangeMin] = 0x6033;
            mapDataStd [rangeNo] [0xCCA5 - rangeMin] = 0x601A;
            mapDataStd [rangeNo] [0xCCA6 - rangeMin] = 0x601E;
            mapDataStd [rangeNo] [0xCCA7 - rangeMin] = 0x602C;
            mapDataStd [rangeNo] [0xCCA8 - rangeMin] = 0x6022;
            mapDataStd [rangeNo] [0xCCA9 - rangeMin] = 0x600D;
            mapDataStd [rangeNo] [0xCCAA - rangeMin] = 0x6010;
            mapDataStd [rangeNo] [0xCCAB - rangeMin] = 0x602E;
            mapDataStd [rangeNo] [0xCCAC - rangeMin] = 0x6013;
            mapDataStd [rangeNo] [0xCCAD - rangeMin] = 0x6011;
            mapDataStd [rangeNo] [0xCCAE - rangeMin] = 0x600C;
            mapDataStd [rangeNo] [0xCCAF - rangeMin] = 0x6009;

            mapDataStd [rangeNo] [0xCCB0 - rangeMin] = 0x601C;
            mapDataStd [rangeNo] [0xCCB1 - rangeMin] = 0x6214;
            mapDataStd [rangeNo] [0xCCB2 - rangeMin] = 0x623D;
            mapDataStd [rangeNo] [0xCCB3 - rangeMin] = 0x62AD;
            mapDataStd [rangeNo] [0xCCB4 - rangeMin] = 0x62B4;
            mapDataStd [rangeNo] [0xCCB5 - rangeMin] = 0x62D1;
            mapDataStd [rangeNo] [0xCCB6 - rangeMin] = 0x62BE;
            mapDataStd [rangeNo] [0xCCB7 - rangeMin] = 0x62AA;
            mapDataStd [rangeNo] [0xCCB8 - rangeMin] = 0x62B6;
            mapDataStd [rangeNo] [0xCCB9 - rangeMin] = 0x62CA;
            mapDataStd [rangeNo] [0xCCBA - rangeMin] = 0x62AE;
            mapDataStd [rangeNo] [0xCCBB - rangeMin] = 0x62B3;
            mapDataStd [rangeNo] [0xCCBC - rangeMin] = 0x62AF;
            mapDataStd [rangeNo] [0xCCBD - rangeMin] = 0x62BB;
            mapDataStd [rangeNo] [0xCCBE - rangeMin] = 0x62A9;
            mapDataStd [rangeNo] [0xCCBF - rangeMin] = 0x62B0;

            mapDataStd [rangeNo] [0xCCC0 - rangeMin] = 0x62B8;
            mapDataStd [rangeNo] [0xCCC1 - rangeMin] = 0x653D;
            mapDataStd [rangeNo] [0xCCC2 - rangeMin] = 0x65A8;
            mapDataStd [rangeNo] [0xCCC3 - rangeMin] = 0x65BB;
            mapDataStd [rangeNo] [0xCCC4 - rangeMin] = 0x6609;
            mapDataStd [rangeNo] [0xCCC5 - rangeMin] = 0x65FC;
            mapDataStd [rangeNo] [0xCCC6 - rangeMin] = 0x6604;
            mapDataStd [rangeNo] [0xCCC7 - rangeMin] = 0x6612;
            mapDataStd [rangeNo] [0xCCC8 - rangeMin] = 0x6608;
            mapDataStd [rangeNo] [0xCCC9 - rangeMin] = 0x65FB;
            mapDataStd [rangeNo] [0xCCCA - rangeMin] = 0x6603;
            mapDataStd [rangeNo] [0xCCCB - rangeMin] = 0x660B;
            mapDataStd [rangeNo] [0xCCCC - rangeMin] = 0x660D;
            mapDataStd [rangeNo] [0xCCCD - rangeMin] = 0x6605;
            mapDataStd [rangeNo] [0xCCCE - rangeMin] = 0x65FD;
            mapDataStd [rangeNo] [0xCCCF - rangeMin] = 0x6611;

            mapDataStd [rangeNo] [0xCCD0 - rangeMin] = 0x6610;
            mapDataStd [rangeNo] [0xCCD1 - rangeMin] = 0x66F6;
            mapDataStd [rangeNo] [0xCCD2 - rangeMin] = 0x670A;
            mapDataStd [rangeNo] [0xCCD3 - rangeMin] = 0x6785;
            mapDataStd [rangeNo] [0xCCD4 - rangeMin] = 0x676C;
            mapDataStd [rangeNo] [0xCCD5 - rangeMin] = 0x678E;
            mapDataStd [rangeNo] [0xCCD6 - rangeMin] = 0x6792;
            mapDataStd [rangeNo] [0xCCD7 - rangeMin] = 0x6776;
            mapDataStd [rangeNo] [0xCCD8 - rangeMin] = 0x677B;
            mapDataStd [rangeNo] [0xCCD9 - rangeMin] = 0x6798;
            mapDataStd [rangeNo] [0xCCDA - rangeMin] = 0x6786;
            mapDataStd [rangeNo] [0xCCDB - rangeMin] = 0x6784;
            mapDataStd [rangeNo] [0xCCDC - rangeMin] = 0x6774;
            mapDataStd [rangeNo] [0xCCDD - rangeMin] = 0x678D;
            mapDataStd [rangeNo] [0xCCDE - rangeMin] = 0x678C;
            mapDataStd [rangeNo] [0xCCDF - rangeMin] = 0x677A;

            mapDataStd [rangeNo] [0xCCE0 - rangeMin] = 0x679F;
            mapDataStd [rangeNo] [0xCCE1 - rangeMin] = 0x6791;
            mapDataStd [rangeNo] [0xCCE2 - rangeMin] = 0x6799;
            mapDataStd [rangeNo] [0xCCE3 - rangeMin] = 0x6783;
            mapDataStd [rangeNo] [0xCCE4 - rangeMin] = 0x677D;
            mapDataStd [rangeNo] [0xCCE5 - rangeMin] = 0x6781;
            mapDataStd [rangeNo] [0xCCE6 - rangeMin] = 0x6778;
            mapDataStd [rangeNo] [0xCCE7 - rangeMin] = 0x6779;
            mapDataStd [rangeNo] [0xCCE8 - rangeMin] = 0x6794;
            mapDataStd [rangeNo] [0xCCE9 - rangeMin] = 0x6B25;
            mapDataStd [rangeNo] [0xCCEA - rangeMin] = 0x6B80;
            mapDataStd [rangeNo] [0xCCEB - rangeMin] = 0x6B7E;
            mapDataStd [rangeNo] [0xCCEC - rangeMin] = 0x6BDE;
            mapDataStd [rangeNo] [0xCCED - rangeMin] = 0x6C1D;
            mapDataStd [rangeNo] [0xCCEE - rangeMin] = 0x6C93;
            mapDataStd [rangeNo] [0xCCEF - rangeMin] = 0x6CEC;

            mapDataStd [rangeNo] [0xCCF0 - rangeMin] = 0x6CEB;
            mapDataStd [rangeNo] [0xCCF1 - rangeMin] = 0x6CEE;
            mapDataStd [rangeNo] [0xCCF2 - rangeMin] = 0x6CD9;
            mapDataStd [rangeNo] [0xCCF3 - rangeMin] = 0x6CB6;
            mapDataStd [rangeNo] [0xCCF4 - rangeMin] = 0x6CD4;
            mapDataStd [rangeNo] [0xCCF5 - rangeMin] = 0x6CAD;
            mapDataStd [rangeNo] [0xCCF6 - rangeMin] = 0x6CE7;
            mapDataStd [rangeNo] [0xCCF7 - rangeMin] = 0x6CB7;
            mapDataStd [rangeNo] [0xCCF8 - rangeMin] = 0x6CD0;
            mapDataStd [rangeNo] [0xCCF9 - rangeMin] = 0x6CC2;
            mapDataStd [rangeNo] [0xCCFA - rangeMin] = 0x6CBA;
            mapDataStd [rangeNo] [0xCCFB - rangeMin] = 0x6CC3;
            mapDataStd [rangeNo] [0xCCFC - rangeMin] = 0x6CC6;
            mapDataStd [rangeNo] [0xCCFD - rangeMin] = 0x6CED;
            mapDataStd [rangeNo] [0xCCFE - rangeMin] = 0x6CF2;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xCD40 - rangeMin] = 0x6CD2;
            mapDataStd [rangeNo] [0xCD41 - rangeMin] = 0x6CDD;
            mapDataStd [rangeNo] [0xCD42 - rangeMin] = 0x6CB4;
            mapDataStd [rangeNo] [0xCD43 - rangeMin] = 0x6C8A;
            mapDataStd [rangeNo] [0xCD44 - rangeMin] = 0x6C9D;
            mapDataStd [rangeNo] [0xCD45 - rangeMin] = 0x6C80;
            mapDataStd [rangeNo] [0xCD46 - rangeMin] = 0x6CDE;
            mapDataStd [rangeNo] [0xCD47 - rangeMin] = 0x6CC0;
            mapDataStd [rangeNo] [0xCD48 - rangeMin] = 0x6D30;
            mapDataStd [rangeNo] [0xCD49 - rangeMin] = 0x6CCD;
            mapDataStd [rangeNo] [0xCD4A - rangeMin] = 0x6CC7;
            mapDataStd [rangeNo] [0xCD4B - rangeMin] = 0x6CB0;
            mapDataStd [rangeNo] [0xCD4C - rangeMin] = 0x6CF9;
            mapDataStd [rangeNo] [0xCD4D - rangeMin] = 0x6CCF;
            mapDataStd [rangeNo] [0xCD4E - rangeMin] = 0x6CE9;
            mapDataStd [rangeNo] [0xCD4F - rangeMin] = 0x6CD1;

            mapDataStd [rangeNo] [0xCD50 - rangeMin] = 0x7094;
            mapDataStd [rangeNo] [0xCD51 - rangeMin] = 0x7098;
            mapDataStd [rangeNo] [0xCD52 - rangeMin] = 0x7085;
            mapDataStd [rangeNo] [0xCD53 - rangeMin] = 0x7093;
            mapDataStd [rangeNo] [0xCD54 - rangeMin] = 0x7086;
            mapDataStd [rangeNo] [0xCD55 - rangeMin] = 0x7084;
            mapDataStd [rangeNo] [0xCD56 - rangeMin] = 0x7091;
            mapDataStd [rangeNo] [0xCD57 - rangeMin] = 0x7096;
            mapDataStd [rangeNo] [0xCD58 - rangeMin] = 0x7082;
            mapDataStd [rangeNo] [0xCD59 - rangeMin] = 0x709A;
            mapDataStd [rangeNo] [0xCD5A - rangeMin] = 0x7083;
            mapDataStd [rangeNo] [0xCD5B - rangeMin] = 0x726A;
            mapDataStd [rangeNo] [0xCD5C - rangeMin] = 0x72D6;
            mapDataStd [rangeNo] [0xCD5D - rangeMin] = 0x72CB;
            mapDataStd [rangeNo] [0xCD5E - rangeMin] = 0x72D8;
            mapDataStd [rangeNo] [0xCD5F - rangeMin] = 0x72C9;

            mapDataStd [rangeNo] [0xCD60 - rangeMin] = 0x72DC;
            mapDataStd [rangeNo] [0xCD61 - rangeMin] = 0x72D2;
            mapDataStd [rangeNo] [0xCD62 - rangeMin] = 0x72D4;
            mapDataStd [rangeNo] [0xCD63 - rangeMin] = 0x72DA;
            mapDataStd [rangeNo] [0xCD64 - rangeMin] = 0x72CC;
            mapDataStd [rangeNo] [0xCD65 - rangeMin] = 0x72D1;
            mapDataStd [rangeNo] [0xCD66 - rangeMin] = 0x73A4;
            mapDataStd [rangeNo] [0xCD67 - rangeMin] = 0x73A1;
            mapDataStd [rangeNo] [0xCD68 - rangeMin] = 0x73AD;
            mapDataStd [rangeNo] [0xCD69 - rangeMin] = 0x73A6;
            mapDataStd [rangeNo] [0xCD6A - rangeMin] = 0x73A2;
            mapDataStd [rangeNo] [0xCD6B - rangeMin] = 0x73A0;
            mapDataStd [rangeNo] [0xCD6C - rangeMin] = 0x73AC;
            mapDataStd [rangeNo] [0xCD6D - rangeMin] = 0x739D;
            mapDataStd [rangeNo] [0xCD6E - rangeMin] = 0x74DD;
            mapDataStd [rangeNo] [0xCD6F - rangeMin] = 0x74E8;

            mapDataStd [rangeNo] [0xCD70 - rangeMin] = 0x753F;
            mapDataStd [rangeNo] [0xCD71 - rangeMin] = 0x7540;
            mapDataStd [rangeNo] [0xCD72 - rangeMin] = 0x753E;
            mapDataStd [rangeNo] [0xCD73 - rangeMin] = 0x758C;
            mapDataStd [rangeNo] [0xCD74 - rangeMin] = 0x7598;
            mapDataStd [rangeNo] [0xCD75 - rangeMin] = 0x76AF;
            mapDataStd [rangeNo] [0xCD76 - rangeMin] = 0x76F3;
            mapDataStd [rangeNo] [0xCD77 - rangeMin] = 0x76F1;
            mapDataStd [rangeNo] [0xCD78 - rangeMin] = 0x76F0;
            mapDataStd [rangeNo] [0xCD79 - rangeMin] = 0x76F5;
            mapDataStd [rangeNo] [0xCD7A - rangeMin] = 0x77F8;
            mapDataStd [rangeNo] [0xCD7B - rangeMin] = 0x77FC;
            mapDataStd [rangeNo] [0xCD7C - rangeMin] = 0x77F9;
            mapDataStd [rangeNo] [0xCD7D - rangeMin] = 0x77FB;
            mapDataStd [rangeNo] [0xCD7E - rangeMin] = 0x77FA;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xCDA1 - rangeMin] = 0x77F7;
            mapDataStd [rangeNo] [0xCDA2 - rangeMin] = 0x7942;
            mapDataStd [rangeNo] [0xCDA3 - rangeMin] = 0x793F;
            mapDataStd [rangeNo] [0xCDA4 - rangeMin] = 0x79C5;
            mapDataStd [rangeNo] [0xCDA5 - rangeMin] = 0x7A78;
            mapDataStd [rangeNo] [0xCDA6 - rangeMin] = 0x7A7B;
            mapDataStd [rangeNo] [0xCDA7 - rangeMin] = 0x7AFB;
            mapDataStd [rangeNo] [0xCDA8 - rangeMin] = 0x7C75;
            mapDataStd [rangeNo] [0xCDA9 - rangeMin] = 0x7CFD;
            mapDataStd [rangeNo] [0xCDAA - rangeMin] = 0x8035;
            mapDataStd [rangeNo] [0xCDAB - rangeMin] = 0x808F;
            mapDataStd [rangeNo] [0xCDAC - rangeMin] = 0x80AE;
            mapDataStd [rangeNo] [0xCDAD - rangeMin] = 0x80A3;
            mapDataStd [rangeNo] [0xCDAE - rangeMin] = 0x80B8;
            mapDataStd [rangeNo] [0xCDAF - rangeMin] = 0x80B5;

            mapDataStd [rangeNo] [0xCDB0 - rangeMin] = 0x80AD;
            mapDataStd [rangeNo] [0xCDB1 - rangeMin] = 0x8220;
            mapDataStd [rangeNo] [0xCDB2 - rangeMin] = 0x82A0;
            mapDataStd [rangeNo] [0xCDB3 - rangeMin] = 0x82C0;
            mapDataStd [rangeNo] [0xCDB4 - rangeMin] = 0x82AB;
            mapDataStd [rangeNo] [0xCDB5 - rangeMin] = 0x829A;
            mapDataStd [rangeNo] [0xCDB6 - rangeMin] = 0x8298;
            mapDataStd [rangeNo] [0xCDB7 - rangeMin] = 0x829B;
            mapDataStd [rangeNo] [0xCDB8 - rangeMin] = 0x82B5;
            mapDataStd [rangeNo] [0xCDB9 - rangeMin] = 0x82A7;
            mapDataStd [rangeNo] [0xCDBA - rangeMin] = 0x82AE;
            mapDataStd [rangeNo] [0xCDBB - rangeMin] = 0x82BC;
            mapDataStd [rangeNo] [0xCDBC - rangeMin] = 0x829E;
            mapDataStd [rangeNo] [0xCDBD - rangeMin] = 0x82BA;
            mapDataStd [rangeNo] [0xCDBE - rangeMin] = 0x82B4;
            mapDataStd [rangeNo] [0xCDBF - rangeMin] = 0x82A8;

            mapDataStd [rangeNo] [0xCDC0 - rangeMin] = 0x82A1;
            mapDataStd [rangeNo] [0xCDC1 - rangeMin] = 0x82A9;
            mapDataStd [rangeNo] [0xCDC2 - rangeMin] = 0x82C2;
            mapDataStd [rangeNo] [0xCDC3 - rangeMin] = 0x82A4;
            mapDataStd [rangeNo] [0xCDC4 - rangeMin] = 0x82C3;
            mapDataStd [rangeNo] [0xCDC5 - rangeMin] = 0x82B6;
            mapDataStd [rangeNo] [0xCDC6 - rangeMin] = 0x82A2;
            mapDataStd [rangeNo] [0xCDC7 - rangeMin] = 0x8670;
            mapDataStd [rangeNo] [0xCDC8 - rangeMin] = 0x866F;
            mapDataStd [rangeNo] [0xCDC9 - rangeMin] = 0x866D;
            mapDataStd [rangeNo] [0xCDCA - rangeMin] = 0x866E;
            mapDataStd [rangeNo] [0xCDCB - rangeMin] = 0x8C56;
            mapDataStd [rangeNo] [0xCDCC - rangeMin] = 0x8FD2;
            mapDataStd [rangeNo] [0xCDCD - rangeMin] = 0x8FCB;
            mapDataStd [rangeNo] [0xCDCE - rangeMin] = 0x8FD3;
            mapDataStd [rangeNo] [0xCDCF - rangeMin] = 0x8FCD;

            mapDataStd [rangeNo] [0xCDD0 - rangeMin] = 0x8FD6;
            mapDataStd [rangeNo] [0xCDD1 - rangeMin] = 0x8FD5;
            mapDataStd [rangeNo] [0xCDD2 - rangeMin] = 0x8FD7;
            mapDataStd [rangeNo] [0xCDD3 - rangeMin] = 0x90B2;
            mapDataStd [rangeNo] [0xCDD4 - rangeMin] = 0x90B4;
            mapDataStd [rangeNo] [0xCDD5 - rangeMin] = 0x90AF;
            mapDataStd [rangeNo] [0xCDD6 - rangeMin] = 0x90B3;
            mapDataStd [rangeNo] [0xCDD7 - rangeMin] = 0x90B0;
            mapDataStd [rangeNo] [0xCDD8 - rangeMin] = 0x9639;
            mapDataStd [rangeNo] [0xCDD9 - rangeMin] = 0x963D;
            mapDataStd [rangeNo] [0xCDDA - rangeMin] = 0x963C;
            mapDataStd [rangeNo] [0xCDDB - rangeMin] = 0x963A;
            mapDataStd [rangeNo] [0xCDDC - rangeMin] = 0x9643;
            mapDataStd [rangeNo] [0xCDDD - rangeMin] = 0x4FCD;
            mapDataStd [rangeNo] [0xCDDE - rangeMin] = 0x4FC5;
            mapDataStd [rangeNo] [0xCDDF - rangeMin] = 0x4FD3;

            mapDataStd [rangeNo] [0xCDE0 - rangeMin] = 0x4FB2;
            mapDataStd [rangeNo] [0xCDE1 - rangeMin] = 0x4FC9;
            mapDataStd [rangeNo] [0xCDE2 - rangeMin] = 0x4FCB;
            mapDataStd [rangeNo] [0xCDE3 - rangeMin] = 0x4FC1;
            mapDataStd [rangeNo] [0xCDE4 - rangeMin] = 0x4FD4;
            mapDataStd [rangeNo] [0xCDE5 - rangeMin] = 0x4FDC;
            mapDataStd [rangeNo] [0xCDE6 - rangeMin] = 0x4FD9;
            mapDataStd [rangeNo] [0xCDE7 - rangeMin] = 0x4FBB;
            mapDataStd [rangeNo] [0xCDE8 - rangeMin] = 0x4FB3;
            mapDataStd [rangeNo] [0xCDE9 - rangeMin] = 0x4FDB;
            mapDataStd [rangeNo] [0xCDEA - rangeMin] = 0x4FC7;
            mapDataStd [rangeNo] [0xCDEB - rangeMin] = 0x4FD6;
            mapDataStd [rangeNo] [0xCDEC - rangeMin] = 0x4FBA;
            mapDataStd [rangeNo] [0xCDED - rangeMin] = 0x4FC0;
            mapDataStd [rangeNo] [0xCDEE - rangeMin] = 0x4FB9;
            mapDataStd [rangeNo] [0xCDEF - rangeMin] = 0x4FEC;

            mapDataStd [rangeNo] [0xCDF0 - rangeMin] = 0x5244;
            mapDataStd [rangeNo] [0xCDF1 - rangeMin] = 0x5249;
            mapDataStd [rangeNo] [0xCDF2 - rangeMin] = 0x52C0;
            mapDataStd [rangeNo] [0xCDF3 - rangeMin] = 0x52C2;
            mapDataStd [rangeNo] [0xCDF4 - rangeMin] = 0x533D;
            mapDataStd [rangeNo] [0xCDF5 - rangeMin] = 0x537C;
            mapDataStd [rangeNo] [0xCDF6 - rangeMin] = 0x5397;
            mapDataStd [rangeNo] [0xCDF7 - rangeMin] = 0x5396;
            mapDataStd [rangeNo] [0xCDF8 - rangeMin] = 0x5399;
            mapDataStd [rangeNo] [0xCDF9 - rangeMin] = 0x5398;
            mapDataStd [rangeNo] [0xCDFA - rangeMin] = 0x54BA;
            mapDataStd [rangeNo] [0xCDFB - rangeMin] = 0x54A1;
            mapDataStd [rangeNo] [0xCDFC - rangeMin] = 0x54AD;
            mapDataStd [rangeNo] [0xCDFD - rangeMin] = 0x54A5;
            mapDataStd [rangeNo] [0xCDFE - rangeMin] = 0x54CF;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xCE40 - rangeMin] = 0x54C3;
            mapDataStd [rangeNo] [0xCE41 - rangeMin] = 0x830D;
            mapDataStd [rangeNo] [0xCE42 - rangeMin] = 0x54B7;
            mapDataStd [rangeNo] [0xCE43 - rangeMin] = 0x54AE;
            mapDataStd [rangeNo] [0xCE44 - rangeMin] = 0x54D6;
            mapDataStd [rangeNo] [0xCE45 - rangeMin] = 0x54B6;
            mapDataStd [rangeNo] [0xCE46 - rangeMin] = 0x54C5;
            mapDataStd [rangeNo] [0xCE47 - rangeMin] = 0x54C6;
            mapDataStd [rangeNo] [0xCE48 - rangeMin] = 0x54A0;
            mapDataStd [rangeNo] [0xCE49 - rangeMin] = 0x5470;
            mapDataStd [rangeNo] [0xCE4A - rangeMin] = 0x54BC;
            mapDataStd [rangeNo] [0xCE4B - rangeMin] = 0x54A2;
            mapDataStd [rangeNo] [0xCE4C - rangeMin] = 0x54BE;
            mapDataStd [rangeNo] [0xCE4D - rangeMin] = 0x5472;
            mapDataStd [rangeNo] [0xCE4E - rangeMin] = 0x54DE;
            mapDataStd [rangeNo] [0xCE4F - rangeMin] = 0x54B0;

            mapDataStd [rangeNo] [0xCE50 - rangeMin] = 0x57B5;
            mapDataStd [rangeNo] [0xCE51 - rangeMin] = 0x579E;
            mapDataStd [rangeNo] [0xCE52 - rangeMin] = 0x579F;
            mapDataStd [rangeNo] [0xCE53 - rangeMin] = 0x57A4;
            mapDataStd [rangeNo] [0xCE54 - rangeMin] = 0x578C;
            mapDataStd [rangeNo] [0xCE55 - rangeMin] = 0x5797;
            mapDataStd [rangeNo] [0xCE56 - rangeMin] = 0x579D;
            mapDataStd [rangeNo] [0xCE57 - rangeMin] = 0x579B;
            mapDataStd [rangeNo] [0xCE58 - rangeMin] = 0x5794;
            mapDataStd [rangeNo] [0xCE59 - rangeMin] = 0x5798;
            mapDataStd [rangeNo] [0xCE5A - rangeMin] = 0x578F;
            mapDataStd [rangeNo] [0xCE5B - rangeMin] = 0x5799;
            mapDataStd [rangeNo] [0xCE5C - rangeMin] = 0x57A5;
            mapDataStd [rangeNo] [0xCE5D - rangeMin] = 0x579A;
            mapDataStd [rangeNo] [0xCE5E - rangeMin] = 0x5795;
            mapDataStd [rangeNo] [0xCE5F - rangeMin] = 0x58F4;

            mapDataStd [rangeNo] [0xCE60 - rangeMin] = 0x590D;
            mapDataStd [rangeNo] [0xCE61 - rangeMin] = 0x5953;
            mapDataStd [rangeNo] [0xCE62 - rangeMin] = 0x59E1;
            mapDataStd [rangeNo] [0xCE63 - rangeMin] = 0x59DE;
            mapDataStd [rangeNo] [0xCE64 - rangeMin] = 0x59EE;
            mapDataStd [rangeNo] [0xCE65 - rangeMin] = 0x5A00;
            mapDataStd [rangeNo] [0xCE66 - rangeMin] = 0x59F1;
            mapDataStd [rangeNo] [0xCE67 - rangeMin] = 0x59DD;
            mapDataStd [rangeNo] [0xCE68 - rangeMin] = 0x59FA;
            mapDataStd [rangeNo] [0xCE69 - rangeMin] = 0x59FD;
            mapDataStd [rangeNo] [0xCE6A - rangeMin] = 0x59FC;
            mapDataStd [rangeNo] [0xCE6B - rangeMin] = 0x59F6;
            mapDataStd [rangeNo] [0xCE6C - rangeMin] = 0x59E4;
            mapDataStd [rangeNo] [0xCE6D - rangeMin] = 0x59F2;
            mapDataStd [rangeNo] [0xCE6E - rangeMin] = 0x59F7;
            mapDataStd [rangeNo] [0xCE6F - rangeMin] = 0x59DB;

            mapDataStd [rangeNo] [0xCE70 - rangeMin] = 0x59E9;
            mapDataStd [rangeNo] [0xCE71 - rangeMin] = 0x59F3;
            mapDataStd [rangeNo] [0xCE72 - rangeMin] = 0x59F5;
            mapDataStd [rangeNo] [0xCE73 - rangeMin] = 0x59E0;
            mapDataStd [rangeNo] [0xCE74 - rangeMin] = 0x59FE;
            mapDataStd [rangeNo] [0xCE75 - rangeMin] = 0x59F4;
            mapDataStd [rangeNo] [0xCE76 - rangeMin] = 0x59ED;
            mapDataStd [rangeNo] [0xCE77 - rangeMin] = 0x5BA8;
            mapDataStd [rangeNo] [0xCE78 - rangeMin] = 0x5C4C;
            mapDataStd [rangeNo] [0xCE79 - rangeMin] = 0x5CD0;
            mapDataStd [rangeNo] [0xCE7A - rangeMin] = 0x5CD8;
            mapDataStd [rangeNo] [0xCE7B - rangeMin] = 0x5CCC;
            mapDataStd [rangeNo] [0xCE7C - rangeMin] = 0x5CD7;
            mapDataStd [rangeNo] [0xCE7D - rangeMin] = 0x5CCB;
            mapDataStd [rangeNo] [0xCE7E - rangeMin] = 0x5CDB;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xCEA1 - rangeMin] = 0x5CDE;
            mapDataStd [rangeNo] [0xCEA2 - rangeMin] = 0x5CDA;
            mapDataStd [rangeNo] [0xCEA3 - rangeMin] = 0x5CC9;
            mapDataStd [rangeNo] [0xCEA4 - rangeMin] = 0x5CC7;
            mapDataStd [rangeNo] [0xCEA5 - rangeMin] = 0x5CCA;
            mapDataStd [rangeNo] [0xCEA6 - rangeMin] = 0x5CD6;
            mapDataStd [rangeNo] [0xCEA7 - rangeMin] = 0x5CD3;
            mapDataStd [rangeNo] [0xCEA8 - rangeMin] = 0x5CD4;
            mapDataStd [rangeNo] [0xCEA9 - rangeMin] = 0x5CCF;
            mapDataStd [rangeNo] [0xCEAA - rangeMin] = 0x5CC8;
            mapDataStd [rangeNo] [0xCEAB - rangeMin] = 0x5CC6;
            mapDataStd [rangeNo] [0xCEAC - rangeMin] = 0x5CCE;
            mapDataStd [rangeNo] [0xCEAD - rangeMin] = 0x5CDF;
            mapDataStd [rangeNo] [0xCEAE - rangeMin] = 0x5CF8;
            mapDataStd [rangeNo] [0xCEAF - rangeMin] = 0x5DF9;

            mapDataStd [rangeNo] [0xCEB0 - rangeMin] = 0x5E21;
            mapDataStd [rangeNo] [0xCEB1 - rangeMin] = 0x5E22;
            mapDataStd [rangeNo] [0xCEB2 - rangeMin] = 0x5E23;
            mapDataStd [rangeNo] [0xCEB3 - rangeMin] = 0x5E20;
            mapDataStd [rangeNo] [0xCEB4 - rangeMin] = 0x5E24;
            mapDataStd [rangeNo] [0xCEB5 - rangeMin] = 0x5EB0;
            mapDataStd [rangeNo] [0xCEB6 - rangeMin] = 0x5EA4;
            mapDataStd [rangeNo] [0xCEB7 - rangeMin] = 0x5EA2;
            mapDataStd [rangeNo] [0xCEB8 - rangeMin] = 0x5E9B;
            mapDataStd [rangeNo] [0xCEB9 - rangeMin] = 0x5EA3;
            mapDataStd [rangeNo] [0xCEBA - rangeMin] = 0x5EA5;
            mapDataStd [rangeNo] [0xCEBB - rangeMin] = 0x5F07;
            mapDataStd [rangeNo] [0xCEBC - rangeMin] = 0x5F2E;
            mapDataStd [rangeNo] [0xCEBD - rangeMin] = 0x5F56;
            mapDataStd [rangeNo] [0xCEBE - rangeMin] = 0x5F86;
            mapDataStd [rangeNo] [0xCEBF - rangeMin] = 0x6037;

            mapDataStd [rangeNo] [0xCEC0 - rangeMin] = 0x6039;
            mapDataStd [rangeNo] [0xCEC1 - rangeMin] = 0x6054;
            mapDataStd [rangeNo] [0xCEC2 - rangeMin] = 0x6072;
            mapDataStd [rangeNo] [0xCEC3 - rangeMin] = 0x605E;
            mapDataStd [rangeNo] [0xCEC4 - rangeMin] = 0x6045;
            mapDataStd [rangeNo] [0xCEC5 - rangeMin] = 0x6053;
            mapDataStd [rangeNo] [0xCEC6 - rangeMin] = 0x6047;
            mapDataStd [rangeNo] [0xCEC7 - rangeMin] = 0x6049;
            mapDataStd [rangeNo] [0xCEC8 - rangeMin] = 0x605B;
            mapDataStd [rangeNo] [0xCEC9 - rangeMin] = 0x604C;
            mapDataStd [rangeNo] [0xCECA - rangeMin] = 0x6040;
            mapDataStd [rangeNo] [0xCECB - rangeMin] = 0x6042;
            mapDataStd [rangeNo] [0xCECC - rangeMin] = 0x605F;
            mapDataStd [rangeNo] [0xCECD - rangeMin] = 0x6024;
            mapDataStd [rangeNo] [0xCECE - rangeMin] = 0x6044;
            mapDataStd [rangeNo] [0xCECF - rangeMin] = 0x6058;

            mapDataStd [rangeNo] [0xCED0 - rangeMin] = 0x6066;
            mapDataStd [rangeNo] [0xCED1 - rangeMin] = 0x606E;
            mapDataStd [rangeNo] [0xCED2 - rangeMin] = 0x6242;
            mapDataStd [rangeNo] [0xCED3 - rangeMin] = 0x6243;
            mapDataStd [rangeNo] [0xCED4 - rangeMin] = 0x62CF;
            mapDataStd [rangeNo] [0xCED5 - rangeMin] = 0x630D;
            mapDataStd [rangeNo] [0xCED6 - rangeMin] = 0x630B;
            mapDataStd [rangeNo] [0xCED7 - rangeMin] = 0x62F5;
            mapDataStd [rangeNo] [0xCED8 - rangeMin] = 0x630E;
            mapDataStd [rangeNo] [0xCED9 - rangeMin] = 0x6303;
            mapDataStd [rangeNo] [0xCEDA - rangeMin] = 0x62EB;
            mapDataStd [rangeNo] [0xCEDB - rangeMin] = 0x62F9;
            mapDataStd [rangeNo] [0xCEDC - rangeMin] = 0x630F;
            mapDataStd [rangeNo] [0xCEDD - rangeMin] = 0x630C;
            mapDataStd [rangeNo] [0xCEDE - rangeMin] = 0x62F8;
            mapDataStd [rangeNo] [0xCEDF - rangeMin] = 0x62F6;

            mapDataStd [rangeNo] [0xCEE0 - rangeMin] = 0x6300;
            mapDataStd [rangeNo] [0xCEE1 - rangeMin] = 0x6313;
            mapDataStd [rangeNo] [0xCEE2 - rangeMin] = 0x6314;
            mapDataStd [rangeNo] [0xCEE3 - rangeMin] = 0x62FA;
            mapDataStd [rangeNo] [0xCEE4 - rangeMin] = 0x6315;
            mapDataStd [rangeNo] [0xCEE5 - rangeMin] = 0x62FB;
            mapDataStd [rangeNo] [0xCEE6 - rangeMin] = 0x62F0;
            mapDataStd [rangeNo] [0xCEE7 - rangeMin] = 0x6541;
            mapDataStd [rangeNo] [0xCEE8 - rangeMin] = 0x6543;
            mapDataStd [rangeNo] [0xCEE9 - rangeMin] = 0x65AA;
            mapDataStd [rangeNo] [0xCEEA - rangeMin] = 0x65BF;
            mapDataStd [rangeNo] [0xCEEB - rangeMin] = 0x6636;
            mapDataStd [rangeNo] [0xCEEC - rangeMin] = 0x6621;
            mapDataStd [rangeNo] [0xCEED - rangeMin] = 0x6632;
            mapDataStd [rangeNo] [0xCEEE - rangeMin] = 0x6635;
            mapDataStd [rangeNo] [0xCEEF - rangeMin] = 0x661C;

            mapDataStd [rangeNo] [0xCEF0 - rangeMin] = 0x6626;
            mapDataStd [rangeNo] [0xCEF1 - rangeMin] = 0x6622;
            mapDataStd [rangeNo] [0xCEF2 - rangeMin] = 0x6633;
            mapDataStd [rangeNo] [0xCEF3 - rangeMin] = 0x662B;
            mapDataStd [rangeNo] [0xCEF4 - rangeMin] = 0x663A;
            mapDataStd [rangeNo] [0xCEF5 - rangeMin] = 0x661D;
            mapDataStd [rangeNo] [0xCEF6 - rangeMin] = 0x6634;
            mapDataStd [rangeNo] [0xCEF7 - rangeMin] = 0x6639;
            mapDataStd [rangeNo] [0xCEF8 - rangeMin] = 0x662E;
            mapDataStd [rangeNo] [0xCEF9 - rangeMin] = 0x670F;
            mapDataStd [rangeNo] [0xCEFA - rangeMin] = 0x6710;
            mapDataStd [rangeNo] [0xCEFB - rangeMin] = 0x67C1;
            mapDataStd [rangeNo] [0xCEFC - rangeMin] = 0x67F2;
            mapDataStd [rangeNo] [0xCEFD - rangeMin] = 0x67C8;
            mapDataStd [rangeNo] [0xCEFE - rangeMin] = 0x67BA;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xCF40 - rangeMin] = 0x67DC;
            mapDataStd [rangeNo] [0xCF41 - rangeMin] = 0x67BB;
            mapDataStd [rangeNo] [0xCF42 - rangeMin] = 0x67F8;
            mapDataStd [rangeNo] [0xCF43 - rangeMin] = 0x67D8;
            mapDataStd [rangeNo] [0xCF44 - rangeMin] = 0x67C0;
            mapDataStd [rangeNo] [0xCF45 - rangeMin] = 0x67B7;
            mapDataStd [rangeNo] [0xCF46 - rangeMin] = 0x67C5;
            mapDataStd [rangeNo] [0xCF47 - rangeMin] = 0x67EB;
            mapDataStd [rangeNo] [0xCF48 - rangeMin] = 0x67E4;
            mapDataStd [rangeNo] [0xCF49 - rangeMin] = 0x67DF;
            mapDataStd [rangeNo] [0xCF4A - rangeMin] = 0x67B5;
            mapDataStd [rangeNo] [0xCF4B - rangeMin] = 0x67CD;
            mapDataStd [rangeNo] [0xCF4C - rangeMin] = 0x67B3;
            mapDataStd [rangeNo] [0xCF4D - rangeMin] = 0x67F7;
            mapDataStd [rangeNo] [0xCF4E - rangeMin] = 0x67F6;
            mapDataStd [rangeNo] [0xCF4F - rangeMin] = 0x67EE;

            mapDataStd [rangeNo] [0xCF50 - rangeMin] = 0x67E3;
            mapDataStd [rangeNo] [0xCF51 - rangeMin] = 0x67C2;
            mapDataStd [rangeNo] [0xCF52 - rangeMin] = 0x67B9;
            mapDataStd [rangeNo] [0xCF53 - rangeMin] = 0x67CE;
            mapDataStd [rangeNo] [0xCF54 - rangeMin] = 0x67E7;
            mapDataStd [rangeNo] [0xCF55 - rangeMin] = 0x67F0;
            mapDataStd [rangeNo] [0xCF56 - rangeMin] = 0x67B2;
            mapDataStd [rangeNo] [0xCF57 - rangeMin] = 0x67FC;
            mapDataStd [rangeNo] [0xCF58 - rangeMin] = 0x67C6;
            mapDataStd [rangeNo] [0xCF59 - rangeMin] = 0x67ED;
            mapDataStd [rangeNo] [0xCF5A - rangeMin] = 0x67CC;
            mapDataStd [rangeNo] [0xCF5B - rangeMin] = 0x67AE;
            mapDataStd [rangeNo] [0xCF5C - rangeMin] = 0x67E6;
            mapDataStd [rangeNo] [0xCF5D - rangeMin] = 0x67DB;
            mapDataStd [rangeNo] [0xCF5E - rangeMin] = 0x67FA;
            mapDataStd [rangeNo] [0xCF5F - rangeMin] = 0x67C9;

            mapDataStd [rangeNo] [0xCF60 - rangeMin] = 0x67CA;
            mapDataStd [rangeNo] [0xCF61 - rangeMin] = 0x67C3;
            mapDataStd [rangeNo] [0xCF62 - rangeMin] = 0x67EA;
            mapDataStd [rangeNo] [0xCF63 - rangeMin] = 0x67CB;
            mapDataStd [rangeNo] [0xCF64 - rangeMin] = 0x6B28;
            mapDataStd [rangeNo] [0xCF65 - rangeMin] = 0x6B82;
            mapDataStd [rangeNo] [0xCF66 - rangeMin] = 0x6B84;
            mapDataStd [rangeNo] [0xCF67 - rangeMin] = 0x6BB6;
            mapDataStd [rangeNo] [0xCF68 - rangeMin] = 0x6BD6;
            mapDataStd [rangeNo] [0xCF69 - rangeMin] = 0x6BD8;
            mapDataStd [rangeNo] [0xCF6A - rangeMin] = 0x6BE0;
            mapDataStd [rangeNo] [0xCF6B - rangeMin] = 0x6C20;
            mapDataStd [rangeNo] [0xCF6C - rangeMin] = 0x6C21;
            mapDataStd [rangeNo] [0xCF6D - rangeMin] = 0x6D28;
            mapDataStd [rangeNo] [0xCF6E - rangeMin] = 0x6D34;
            mapDataStd [rangeNo] [0xCF6F - rangeMin] = 0x6D2D;

            mapDataStd [rangeNo] [0xCF70 - rangeMin] = 0x6D1F;
            mapDataStd [rangeNo] [0xCF71 - rangeMin] = 0x6D3C;
            mapDataStd [rangeNo] [0xCF72 - rangeMin] = 0x6D3F;
            mapDataStd [rangeNo] [0xCF73 - rangeMin] = 0x6D12;
            mapDataStd [rangeNo] [0xCF74 - rangeMin] = 0x6D0A;
            mapDataStd [rangeNo] [0xCF75 - rangeMin] = 0x6CDA;
            mapDataStd [rangeNo] [0xCF76 - rangeMin] = 0x6D33;
            mapDataStd [rangeNo] [0xCF77 - rangeMin] = 0x6D04;
            mapDataStd [rangeNo] [0xCF78 - rangeMin] = 0x6D19;
            mapDataStd [rangeNo] [0xCF79 - rangeMin] = 0x6D3A;
            mapDataStd [rangeNo] [0xCF7A - rangeMin] = 0x6D1A;
            mapDataStd [rangeNo] [0xCF7B - rangeMin] = 0x6D11;
            mapDataStd [rangeNo] [0xCF7C - rangeMin] = 0x6D00;
            mapDataStd [rangeNo] [0xCF7D - rangeMin] = 0x6D1D;
            mapDataStd [rangeNo] [0xCF7E - rangeMin] = 0x6D42;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xCFA1 - rangeMin] = 0x6D01;
            mapDataStd [rangeNo] [0xCFA2 - rangeMin] = 0x6D18;
            mapDataStd [rangeNo] [0xCFA3 - rangeMin] = 0x6D37;
            mapDataStd [rangeNo] [0xCFA4 - rangeMin] = 0x6D03;
            mapDataStd [rangeNo] [0xCFA5 - rangeMin] = 0x6D0F;
            mapDataStd [rangeNo] [0xCFA6 - rangeMin] = 0x6D40;
            mapDataStd [rangeNo] [0xCFA7 - rangeMin] = 0x6D07;
            mapDataStd [rangeNo] [0xCFA8 - rangeMin] = 0x6D20;
            mapDataStd [rangeNo] [0xCFA9 - rangeMin] = 0x6D2C;
            mapDataStd [rangeNo] [0xCFAA - rangeMin] = 0x6D08;
            mapDataStd [rangeNo] [0xCFAB - rangeMin] = 0x6D22;
            mapDataStd [rangeNo] [0xCFAC - rangeMin] = 0x6D09;
            mapDataStd [rangeNo] [0xCFAD - rangeMin] = 0x6D10;
            mapDataStd [rangeNo] [0xCFAE - rangeMin] = 0x70B7;
            mapDataStd [rangeNo] [0xCFAF - rangeMin] = 0x709F;

            mapDataStd [rangeNo] [0xCFB0 - rangeMin] = 0x70BE;
            mapDataStd [rangeNo] [0xCFB1 - rangeMin] = 0x70B1;
            mapDataStd [rangeNo] [0xCFB2 - rangeMin] = 0x70B0;
            mapDataStd [rangeNo] [0xCFB3 - rangeMin] = 0x70A1;
            mapDataStd [rangeNo] [0xCFB4 - rangeMin] = 0x70B4;
            mapDataStd [rangeNo] [0xCFB5 - rangeMin] = 0x70B5;
            mapDataStd [rangeNo] [0xCFB6 - rangeMin] = 0x70A9;
            mapDataStd [rangeNo] [0xCFB7 - rangeMin] = 0x7241;
            mapDataStd [rangeNo] [0xCFB8 - rangeMin] = 0x7249;
            mapDataStd [rangeNo] [0xCFB9 - rangeMin] = 0x724A;
            mapDataStd [rangeNo] [0xCFBA - rangeMin] = 0x726C;
            mapDataStd [rangeNo] [0xCFBB - rangeMin] = 0x7270;
            mapDataStd [rangeNo] [0xCFBC - rangeMin] = 0x7273;
            mapDataStd [rangeNo] [0xCFBD - rangeMin] = 0x726E;
            mapDataStd [rangeNo] [0xCFBE - rangeMin] = 0x72CA;
            mapDataStd [rangeNo] [0xCFBF - rangeMin] = 0x72E4;

            mapDataStd [rangeNo] [0xCFC0 - rangeMin] = 0x72E8;
            mapDataStd [rangeNo] [0xCFC1 - rangeMin] = 0x72EB;
            mapDataStd [rangeNo] [0xCFC2 - rangeMin] = 0x72DF;
            mapDataStd [rangeNo] [0xCFC3 - rangeMin] = 0x72EA;
            mapDataStd [rangeNo] [0xCFC4 - rangeMin] = 0x72E6;
            mapDataStd [rangeNo] [0xCFC5 - rangeMin] = 0x72E3;
            mapDataStd [rangeNo] [0xCFC6 - rangeMin] = 0x7385;
            mapDataStd [rangeNo] [0xCFC7 - rangeMin] = 0x73CC;
            mapDataStd [rangeNo] [0xCFC8 - rangeMin] = 0x73C2;
            mapDataStd [rangeNo] [0xCFC9 - rangeMin] = 0x73C8;
            mapDataStd [rangeNo] [0xCFCA - rangeMin] = 0x73C5;
            mapDataStd [rangeNo] [0xCFCB - rangeMin] = 0x73B9;
            mapDataStd [rangeNo] [0xCFCC - rangeMin] = 0x73B6;
            mapDataStd [rangeNo] [0xCFCD - rangeMin] = 0x73B5;
            mapDataStd [rangeNo] [0xCFCE - rangeMin] = 0x73B4;
            mapDataStd [rangeNo] [0xCFCF - rangeMin] = 0x73EB;

            mapDataStd [rangeNo] [0xCFD0 - rangeMin] = 0x73BF;
            mapDataStd [rangeNo] [0xCFD1 - rangeMin] = 0x73C7;
            mapDataStd [rangeNo] [0xCFD2 - rangeMin] = 0x73BE;
            mapDataStd [rangeNo] [0xCFD3 - rangeMin] = 0x73C3;
            mapDataStd [rangeNo] [0xCFD4 - rangeMin] = 0x73C6;
            mapDataStd [rangeNo] [0xCFD5 - rangeMin] = 0x73B8;
            mapDataStd [rangeNo] [0xCFD6 - rangeMin] = 0x73CB;
            mapDataStd [rangeNo] [0xCFD7 - rangeMin] = 0x74EC;
            mapDataStd [rangeNo] [0xCFD8 - rangeMin] = 0x74EE;
            mapDataStd [rangeNo] [0xCFD9 - rangeMin] = 0x752E;
            mapDataStd [rangeNo] [0xCFDA - rangeMin] = 0x7547;
            mapDataStd [rangeNo] [0xCFDB - rangeMin] = 0x7548;
            mapDataStd [rangeNo] [0xCFDC - rangeMin] = 0x75A7;
            mapDataStd [rangeNo] [0xCFDD - rangeMin] = 0x75AA;
            mapDataStd [rangeNo] [0xCFDE - rangeMin] = 0x7679;
            mapDataStd [rangeNo] [0xCFDF - rangeMin] = 0x76C4;

            mapDataStd [rangeNo] [0xCFE0 - rangeMin] = 0x7708;
            mapDataStd [rangeNo] [0xCFE1 - rangeMin] = 0x7703;
            mapDataStd [rangeNo] [0xCFE2 - rangeMin] = 0x7704;
            mapDataStd [rangeNo] [0xCFE3 - rangeMin] = 0x7705;
            mapDataStd [rangeNo] [0xCFE4 - rangeMin] = 0x770A;
            mapDataStd [rangeNo] [0xCFE5 - rangeMin] = 0x76F7;
            mapDataStd [rangeNo] [0xCFE6 - rangeMin] = 0x76FB;
            mapDataStd [rangeNo] [0xCFE7 - rangeMin] = 0x76FA;
            mapDataStd [rangeNo] [0xCFE8 - rangeMin] = 0x77E7;
            mapDataStd [rangeNo] [0xCFE9 - rangeMin] = 0x77E8;
            mapDataStd [rangeNo] [0xCFEA - rangeMin] = 0x7806;
            mapDataStd [rangeNo] [0xCFEB - rangeMin] = 0x7811;
            mapDataStd [rangeNo] [0xCFEC - rangeMin] = 0x7812;
            mapDataStd [rangeNo] [0xCFED - rangeMin] = 0x7805;
            mapDataStd [rangeNo] [0xCFEE - rangeMin] = 0x7810;
            mapDataStd [rangeNo] [0xCFEF - rangeMin] = 0x780F;

            mapDataStd [rangeNo] [0xCFF0 - rangeMin] = 0x780E;
            mapDataStd [rangeNo] [0xCFF1 - rangeMin] = 0x7809;
            mapDataStd [rangeNo] [0xCFF2 - rangeMin] = 0x7803;
            mapDataStd [rangeNo] [0xCFF3 - rangeMin] = 0x7813;
            mapDataStd [rangeNo] [0xCFF4 - rangeMin] = 0x794A;
            mapDataStd [rangeNo] [0xCFF5 - rangeMin] = 0x794C;
            mapDataStd [rangeNo] [0xCFF6 - rangeMin] = 0x794B;
            mapDataStd [rangeNo] [0xCFF7 - rangeMin] = 0x7945;
            mapDataStd [rangeNo] [0xCFF8 - rangeMin] = 0x7944;
            mapDataStd [rangeNo] [0xCFF9 - rangeMin] = 0x79D5;
            mapDataStd [rangeNo] [0xCFFA - rangeMin] = 0x79CD;
            mapDataStd [rangeNo] [0xCFFB - rangeMin] = 0x79CF;
            mapDataStd [rangeNo] [0xCFFC - rangeMin] = 0x79D6;
            mapDataStd [rangeNo] [0xCFFD - rangeMin] = 0x79CE;
            mapDataStd [rangeNo] [0xCFFE - rangeMin] = 0x7A80;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xD040 - rangeMin] = 0x7A7E;
            mapDataStd [rangeNo] [0xD041 - rangeMin] = 0x7AD1;
            mapDataStd [rangeNo] [0xD042 - rangeMin] = 0x7B00;
            mapDataStd [rangeNo] [0xD043 - rangeMin] = 0x7B01;
            mapDataStd [rangeNo] [0xD044 - rangeMin] = 0x7C7A;
            mapDataStd [rangeNo] [0xD045 - rangeMin] = 0x7C78;
            mapDataStd [rangeNo] [0xD046 - rangeMin] = 0x7C79;
            mapDataStd [rangeNo] [0xD047 - rangeMin] = 0x7C7F;
            mapDataStd [rangeNo] [0xD048 - rangeMin] = 0x7C80;
            mapDataStd [rangeNo] [0xD049 - rangeMin] = 0x7C81;
            mapDataStd [rangeNo] [0xD04A - rangeMin] = 0x7D03;
            mapDataStd [rangeNo] [0xD04B - rangeMin] = 0x7D08;
            mapDataStd [rangeNo] [0xD04C - rangeMin] = 0x7D01;
            mapDataStd [rangeNo] [0xD04D - rangeMin] = 0x7F58;
            mapDataStd [rangeNo] [0xD04E - rangeMin] = 0x7F91;
            mapDataStd [rangeNo] [0xD04F - rangeMin] = 0x7F8D;

            mapDataStd [rangeNo] [0xD050 - rangeMin] = 0x7FBE;
            mapDataStd [rangeNo] [0xD051 - rangeMin] = 0x8007;
            mapDataStd [rangeNo] [0xD052 - rangeMin] = 0x800E;
            mapDataStd [rangeNo] [0xD053 - rangeMin] = 0x800F;
            mapDataStd [rangeNo] [0xD054 - rangeMin] = 0x8014;
            mapDataStd [rangeNo] [0xD055 - rangeMin] = 0x8037;
            mapDataStd [rangeNo] [0xD056 - rangeMin] = 0x80D8;
            mapDataStd [rangeNo] [0xD057 - rangeMin] = 0x80C7;
            mapDataStd [rangeNo] [0xD058 - rangeMin] = 0x80E0;
            mapDataStd [rangeNo] [0xD059 - rangeMin] = 0x80D1;
            mapDataStd [rangeNo] [0xD05A - rangeMin] = 0x80C8;
            mapDataStd [rangeNo] [0xD05B - rangeMin] = 0x80C2;
            mapDataStd [rangeNo] [0xD05C - rangeMin] = 0x80D0;
            mapDataStd [rangeNo] [0xD05D - rangeMin] = 0x80C5;
            mapDataStd [rangeNo] [0xD05E - rangeMin] = 0x80E3;
            mapDataStd [rangeNo] [0xD05F - rangeMin] = 0x80D9;

            mapDataStd [rangeNo] [0xD060 - rangeMin] = 0x80DC;
            mapDataStd [rangeNo] [0xD061 - rangeMin] = 0x80CA;
            mapDataStd [rangeNo] [0xD062 - rangeMin] = 0x80D5;
            mapDataStd [rangeNo] [0xD063 - rangeMin] = 0x80C9;
            mapDataStd [rangeNo] [0xD064 - rangeMin] = 0x80CF;
            mapDataStd [rangeNo] [0xD065 - rangeMin] = 0x80D7;
            mapDataStd [rangeNo] [0xD066 - rangeMin] = 0x80E6;
            mapDataStd [rangeNo] [0xD067 - rangeMin] = 0x80CD;
            mapDataStd [rangeNo] [0xD068 - rangeMin] = 0x81FF;
            mapDataStd [rangeNo] [0xD069 - rangeMin] = 0x8221;
            mapDataStd [rangeNo] [0xD06A - rangeMin] = 0x8294;
            mapDataStd [rangeNo] [0xD06B - rangeMin] = 0x82D9;
            mapDataStd [rangeNo] [0xD06C - rangeMin] = 0x82FE;
            mapDataStd [rangeNo] [0xD06D - rangeMin] = 0x82F9;
            mapDataStd [rangeNo] [0xD06E - rangeMin] = 0x8307;
            mapDataStd [rangeNo] [0xD06F - rangeMin] = 0x82E8;

            mapDataStd [rangeNo] [0xD070 - rangeMin] = 0x8300;
            mapDataStd [rangeNo] [0xD071 - rangeMin] = 0x82D5;
            mapDataStd [rangeNo] [0xD072 - rangeMin] = 0x833A;
            mapDataStd [rangeNo] [0xD073 - rangeMin] = 0x82EB;
            mapDataStd [rangeNo] [0xD074 - rangeMin] = 0x82D6;
            mapDataStd [rangeNo] [0xD075 - rangeMin] = 0x82F4;
            mapDataStd [rangeNo] [0xD076 - rangeMin] = 0x82EC;
            mapDataStd [rangeNo] [0xD077 - rangeMin] = 0x82E1;
            mapDataStd [rangeNo] [0xD078 - rangeMin] = 0x82F2;
            mapDataStd [rangeNo] [0xD079 - rangeMin] = 0x82F5;
            mapDataStd [rangeNo] [0xD07A - rangeMin] = 0x830C;
            mapDataStd [rangeNo] [0xD07B - rangeMin] = 0x82FB;
            mapDataStd [rangeNo] [0xD07C - rangeMin] = 0x82F6;
            mapDataStd [rangeNo] [0xD07D - rangeMin] = 0x82F0;
            mapDataStd [rangeNo] [0xD07E - rangeMin] = 0x82EA;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xD0A1 - rangeMin] = 0x82E4;
            mapDataStd [rangeNo] [0xD0A2 - rangeMin] = 0x82E0;
            mapDataStd [rangeNo] [0xD0A3 - rangeMin] = 0x82FA;
            mapDataStd [rangeNo] [0xD0A4 - rangeMin] = 0x82F3;
            mapDataStd [rangeNo] [0xD0A5 - rangeMin] = 0x82ED;
            mapDataStd [rangeNo] [0xD0A6 - rangeMin] = 0x8677;
            mapDataStd [rangeNo] [0xD0A7 - rangeMin] = 0x8674;
            mapDataStd [rangeNo] [0xD0A8 - rangeMin] = 0x867C;
            mapDataStd [rangeNo] [0xD0A9 - rangeMin] = 0x8673;
            mapDataStd [rangeNo] [0xD0AA - rangeMin] = 0x8841;
            mapDataStd [rangeNo] [0xD0AB - rangeMin] = 0x884E;
            mapDataStd [rangeNo] [0xD0AC - rangeMin] = 0x8867;
            mapDataStd [rangeNo] [0xD0AD - rangeMin] = 0x886A;
            mapDataStd [rangeNo] [0xD0AE - rangeMin] = 0x8869;
            mapDataStd [rangeNo] [0xD0AF - rangeMin] = 0x89D3;

            mapDataStd [rangeNo] [0xD0B0 - rangeMin] = 0x8A04;
            mapDataStd [rangeNo] [0xD0B1 - rangeMin] = 0x8A07;
            mapDataStd [rangeNo] [0xD0B2 - rangeMin] = 0x8D72;
            mapDataStd [rangeNo] [0xD0B3 - rangeMin] = 0x8FE3;
            mapDataStd [rangeNo] [0xD0B4 - rangeMin] = 0x8FE1;
            mapDataStd [rangeNo] [0xD0B5 - rangeMin] = 0x8FEE;
            mapDataStd [rangeNo] [0xD0B6 - rangeMin] = 0x8FE0;
            mapDataStd [rangeNo] [0xD0B7 - rangeMin] = 0x90F1;
            mapDataStd [rangeNo] [0xD0B8 - rangeMin] = 0x90BD;
            mapDataStd [rangeNo] [0xD0B9 - rangeMin] = 0x90BF;
            mapDataStd [rangeNo] [0xD0BA - rangeMin] = 0x90D5;
            mapDataStd [rangeNo] [0xD0BB - rangeMin] = 0x90C5;
            mapDataStd [rangeNo] [0xD0BC - rangeMin] = 0x90BE;
            mapDataStd [rangeNo] [0xD0BD - rangeMin] = 0x90C7;
            mapDataStd [rangeNo] [0xD0BE - rangeMin] = 0x90CB;
            mapDataStd [rangeNo] [0xD0BF - rangeMin] = 0x90C8;

            mapDataStd [rangeNo] [0xD0C0 - rangeMin] = 0x91D4;
            mapDataStd [rangeNo] [0xD0C1 - rangeMin] = 0x91D3;
            mapDataStd [rangeNo] [0xD0C2 - rangeMin] = 0x9654;
            mapDataStd [rangeNo] [0xD0C3 - rangeMin] = 0x964F;
            mapDataStd [rangeNo] [0xD0C4 - rangeMin] = 0x9651;
            mapDataStd [rangeNo] [0xD0C5 - rangeMin] = 0x9653;
            mapDataStd [rangeNo] [0xD0C6 - rangeMin] = 0x964A;
            mapDataStd [rangeNo] [0xD0C7 - rangeMin] = 0x964E;
            mapDataStd [rangeNo] [0xD0C8 - rangeMin] = 0x501E;
            mapDataStd [rangeNo] [0xD0C9 - rangeMin] = 0x5005;
            mapDataStd [rangeNo] [0xD0CA - rangeMin] = 0x5007;
            mapDataStd [rangeNo] [0xD0CB - rangeMin] = 0x5013;
            mapDataStd [rangeNo] [0xD0CC - rangeMin] = 0x5022;
            mapDataStd [rangeNo] [0xD0CD - rangeMin] = 0x5030;
            mapDataStd [rangeNo] [0xD0CE - rangeMin] = 0x501B;
            mapDataStd [rangeNo] [0xD0CF - rangeMin] = 0x4FF5;

            mapDataStd [rangeNo] [0xD0D0 - rangeMin] = 0x4FF4;
            mapDataStd [rangeNo] [0xD0D1 - rangeMin] = 0x5033;
            mapDataStd [rangeNo] [0xD0D2 - rangeMin] = 0x5037;
            mapDataStd [rangeNo] [0xD0D3 - rangeMin] = 0x502C;
            mapDataStd [rangeNo] [0xD0D4 - rangeMin] = 0x4FF6;
            mapDataStd [rangeNo] [0xD0D5 - rangeMin] = 0x4FF7;
            mapDataStd [rangeNo] [0xD0D6 - rangeMin] = 0x5017;
            mapDataStd [rangeNo] [0xD0D7 - rangeMin] = 0x501C;
            mapDataStd [rangeNo] [0xD0D8 - rangeMin] = 0x5020;
            mapDataStd [rangeNo] [0xD0D9 - rangeMin] = 0x5027;
            mapDataStd [rangeNo] [0xD0DA - rangeMin] = 0x5035;
            mapDataStd [rangeNo] [0xD0DB - rangeMin] = 0x502F;
            mapDataStd [rangeNo] [0xD0DC - rangeMin] = 0x5031;
            mapDataStd [rangeNo] [0xD0DD - rangeMin] = 0x500E;
            mapDataStd [rangeNo] [0xD0DE - rangeMin] = 0x515A;
            mapDataStd [rangeNo] [0xD0DF - rangeMin] = 0x5194;

            mapDataStd [rangeNo] [0xD0E0 - rangeMin] = 0x5193;
            mapDataStd [rangeNo] [0xD0E1 - rangeMin] = 0x51CA;
            mapDataStd [rangeNo] [0xD0E2 - rangeMin] = 0x51C4;
            mapDataStd [rangeNo] [0xD0E3 - rangeMin] = 0x51C5;
            mapDataStd [rangeNo] [0xD0E4 - rangeMin] = 0x51C8;
            mapDataStd [rangeNo] [0xD0E5 - rangeMin] = 0x51CE;
            mapDataStd [rangeNo] [0xD0E6 - rangeMin] = 0x5261;
            mapDataStd [rangeNo] [0xD0E7 - rangeMin] = 0x525A;
            mapDataStd [rangeNo] [0xD0E8 - rangeMin] = 0x5252;
            mapDataStd [rangeNo] [0xD0E9 - rangeMin] = 0x525E;
            mapDataStd [rangeNo] [0xD0EA - rangeMin] = 0x525F;
            mapDataStd [rangeNo] [0xD0EB - rangeMin] = 0x5255;
            mapDataStd [rangeNo] [0xD0EC - rangeMin] = 0x5262;
            mapDataStd [rangeNo] [0xD0ED - rangeMin] = 0x52CD;
            mapDataStd [rangeNo] [0xD0EE - rangeMin] = 0x530E;
            mapDataStd [rangeNo] [0xD0EF - rangeMin] = 0x539E;

            mapDataStd [rangeNo] [0xD0F0 - rangeMin] = 0x5526;
            mapDataStd [rangeNo] [0xD0F1 - rangeMin] = 0x54E2;
            mapDataStd [rangeNo] [0xD0F2 - rangeMin] = 0x5517;
            mapDataStd [rangeNo] [0xD0F3 - rangeMin] = 0x5512;
            mapDataStd [rangeNo] [0xD0F4 - rangeMin] = 0x54E7;
            mapDataStd [rangeNo] [0xD0F5 - rangeMin] = 0x54F3;
            mapDataStd [rangeNo] [0xD0F6 - rangeMin] = 0x54E4;
            mapDataStd [rangeNo] [0xD0F7 - rangeMin] = 0x551A;
            mapDataStd [rangeNo] [0xD0F8 - rangeMin] = 0x54FF;
            mapDataStd [rangeNo] [0xD0F9 - rangeMin] = 0x5504;
            mapDataStd [rangeNo] [0xD0FA - rangeMin] = 0x5508;
            mapDataStd [rangeNo] [0xD0FB - rangeMin] = 0x54EB;
            mapDataStd [rangeNo] [0xD0FC - rangeMin] = 0x5511;
            mapDataStd [rangeNo] [0xD0FD - rangeMin] = 0x5505;
            mapDataStd [rangeNo] [0xD0FE - rangeMin] = 0x54F1;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xD140 - rangeMin] = 0x550A;
            mapDataStd [rangeNo] [0xD141 - rangeMin] = 0x54FB;
            mapDataStd [rangeNo] [0xD142 - rangeMin] = 0x54F7;
            mapDataStd [rangeNo] [0xD143 - rangeMin] = 0x54F8;
            mapDataStd [rangeNo] [0xD144 - rangeMin] = 0x54E0;
            mapDataStd [rangeNo] [0xD145 - rangeMin] = 0x550E;
            mapDataStd [rangeNo] [0xD146 - rangeMin] = 0x5503;
            mapDataStd [rangeNo] [0xD147 - rangeMin] = 0x550B;
            mapDataStd [rangeNo] [0xD148 - rangeMin] = 0x5701;
            mapDataStd [rangeNo] [0xD149 - rangeMin] = 0x5702;
            mapDataStd [rangeNo] [0xD14A - rangeMin] = 0x57CC;
            mapDataStd [rangeNo] [0xD14B - rangeMin] = 0x5832;
            mapDataStd [rangeNo] [0xD14C - rangeMin] = 0x57D5;
            mapDataStd [rangeNo] [0xD14D - rangeMin] = 0x57D2;
            mapDataStd [rangeNo] [0xD14E - rangeMin] = 0x57BA;
            mapDataStd [rangeNo] [0xD14F - rangeMin] = 0x57C6;

            mapDataStd [rangeNo] [0xD150 - rangeMin] = 0x57BD;
            mapDataStd [rangeNo] [0xD151 - rangeMin] = 0x57BC;
            mapDataStd [rangeNo] [0xD152 - rangeMin] = 0x57B8;
            mapDataStd [rangeNo] [0xD153 - rangeMin] = 0x57B6;
            mapDataStd [rangeNo] [0xD154 - rangeMin] = 0x57BF;
            mapDataStd [rangeNo] [0xD155 - rangeMin] = 0x57C7;
            mapDataStd [rangeNo] [0xD156 - rangeMin] = 0x57D0;
            mapDataStd [rangeNo] [0xD157 - rangeMin] = 0x57B9;
            mapDataStd [rangeNo] [0xD158 - rangeMin] = 0x57C1;
            mapDataStd [rangeNo] [0xD159 - rangeMin] = 0x590E;
            mapDataStd [rangeNo] [0xD15A - rangeMin] = 0x594A;
            mapDataStd [rangeNo] [0xD15B - rangeMin] = 0x5A19;
            mapDataStd [rangeNo] [0xD15C - rangeMin] = 0x5A16;
            mapDataStd [rangeNo] [0xD15D - rangeMin] = 0x5A2D;
            mapDataStd [rangeNo] [0xD15E - rangeMin] = 0x5A2E;
            mapDataStd [rangeNo] [0xD15F - rangeMin] = 0x5A15;

            mapDataStd [rangeNo] [0xD160 - rangeMin] = 0x5A0F;
            mapDataStd [rangeNo] [0xD161 - rangeMin] = 0x5A17;
            mapDataStd [rangeNo] [0xD162 - rangeMin] = 0x5A0A;
            mapDataStd [rangeNo] [0xD163 - rangeMin] = 0x5A1E;
            mapDataStd [rangeNo] [0xD164 - rangeMin] = 0x5A33;
            mapDataStd [rangeNo] [0xD165 - rangeMin] = 0x5B6C;
            mapDataStd [rangeNo] [0xD166 - rangeMin] = 0x5BA7;
            mapDataStd [rangeNo] [0xD167 - rangeMin] = 0x5BAD;
            mapDataStd [rangeNo] [0xD168 - rangeMin] = 0x5BAC;
            mapDataStd [rangeNo] [0xD169 - rangeMin] = 0x5C03;
            mapDataStd [rangeNo] [0xD16A - rangeMin] = 0x5C56;
            mapDataStd [rangeNo] [0xD16B - rangeMin] = 0x5C54;
            mapDataStd [rangeNo] [0xD16C - rangeMin] = 0x5CEC;
            mapDataStd [rangeNo] [0xD16D - rangeMin] = 0x5CFF;
            mapDataStd [rangeNo] [0xD16E - rangeMin] = 0x5CEE;
            mapDataStd [rangeNo] [0xD16F - rangeMin] = 0x5CF1;

            mapDataStd [rangeNo] [0xD170 - rangeMin] = 0x5CF7;
            mapDataStd [rangeNo] [0xD171 - rangeMin] = 0x5D00;
            mapDataStd [rangeNo] [0xD172 - rangeMin] = 0x5CF9;
            mapDataStd [rangeNo] [0xD173 - rangeMin] = 0x5E29;
            mapDataStd [rangeNo] [0xD174 - rangeMin] = 0x5E28;
            mapDataStd [rangeNo] [0xD175 - rangeMin] = 0x5EA8;
            mapDataStd [rangeNo] [0xD176 - rangeMin] = 0x5EAE;
            mapDataStd [rangeNo] [0xD177 - rangeMin] = 0x5EAA;
            mapDataStd [rangeNo] [0xD178 - rangeMin] = 0x5EAC;
            mapDataStd [rangeNo] [0xD179 - rangeMin] = 0x5F33;
            mapDataStd [rangeNo] [0xD17A - rangeMin] = 0x5F30;
            mapDataStd [rangeNo] [0xD17B - rangeMin] = 0x5F67;
            mapDataStd [rangeNo] [0xD17C - rangeMin] = 0x605D;
            mapDataStd [rangeNo] [0xD17D - rangeMin] = 0x605A;
            mapDataStd [rangeNo] [0xD17E - rangeMin] = 0x6067;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xD1A1 - rangeMin] = 0x6041;
            mapDataStd [rangeNo] [0xD1A2 - rangeMin] = 0x60A2;
            mapDataStd [rangeNo] [0xD1A3 - rangeMin] = 0x6088;
            mapDataStd [rangeNo] [0xD1A4 - rangeMin] = 0x6080;
            mapDataStd [rangeNo] [0xD1A5 - rangeMin] = 0x6092;
            mapDataStd [rangeNo] [0xD1A6 - rangeMin] = 0x6081;
            mapDataStd [rangeNo] [0xD1A7 - rangeMin] = 0x609D;
            mapDataStd [rangeNo] [0xD1A8 - rangeMin] = 0x6083;
            mapDataStd [rangeNo] [0xD1A9 - rangeMin] = 0x6095;
            mapDataStd [rangeNo] [0xD1AA - rangeMin] = 0x609B;
            mapDataStd [rangeNo] [0xD1AB - rangeMin] = 0x6097;
            mapDataStd [rangeNo] [0xD1AC - rangeMin] = 0x6087;
            mapDataStd [rangeNo] [0xD1AD - rangeMin] = 0x609C;
            mapDataStd [rangeNo] [0xD1AE - rangeMin] = 0x608E;
            mapDataStd [rangeNo] [0xD1AF - rangeMin] = 0x6219;

            mapDataStd [rangeNo] [0xD1B0 - rangeMin] = 0x6246;
            mapDataStd [rangeNo] [0xD1B1 - rangeMin] = 0x62F2;
            mapDataStd [rangeNo] [0xD1B2 - rangeMin] = 0x6310;
            mapDataStd [rangeNo] [0xD1B3 - rangeMin] = 0x6356;
            mapDataStd [rangeNo] [0xD1B4 - rangeMin] = 0x632C;
            mapDataStd [rangeNo] [0xD1B5 - rangeMin] = 0x6344;
            mapDataStd [rangeNo] [0xD1B6 - rangeMin] = 0x6345;
            mapDataStd [rangeNo] [0xD1B7 - rangeMin] = 0x6336;
            mapDataStd [rangeNo] [0xD1B8 - rangeMin] = 0x6343;
            mapDataStd [rangeNo] [0xD1B9 - rangeMin] = 0x63E4;
            mapDataStd [rangeNo] [0xD1BA - rangeMin] = 0x6339;
            mapDataStd [rangeNo] [0xD1BB - rangeMin] = 0x634B;
            mapDataStd [rangeNo] [0xD1BC - rangeMin] = 0x634A;
            mapDataStd [rangeNo] [0xD1BD - rangeMin] = 0x633C;
            mapDataStd [rangeNo] [0xD1BE - rangeMin] = 0x6329;
            mapDataStd [rangeNo] [0xD1BF - rangeMin] = 0x6341;

            mapDataStd [rangeNo] [0xD1C0 - rangeMin] = 0x6334;
            mapDataStd [rangeNo] [0xD1C1 - rangeMin] = 0x6358;
            mapDataStd [rangeNo] [0xD1C2 - rangeMin] = 0x6354;
            mapDataStd [rangeNo] [0xD1C3 - rangeMin] = 0x6359;
            mapDataStd [rangeNo] [0xD1C4 - rangeMin] = 0x632D;
            mapDataStd [rangeNo] [0xD1C5 - rangeMin] = 0x6347;
            mapDataStd [rangeNo] [0xD1C6 - rangeMin] = 0x6333;
            mapDataStd [rangeNo] [0xD1C7 - rangeMin] = 0x635A;
            mapDataStd [rangeNo] [0xD1C8 - rangeMin] = 0x6351;
            mapDataStd [rangeNo] [0xD1C9 - rangeMin] = 0x6338;
            mapDataStd [rangeNo] [0xD1CA - rangeMin] = 0x6357;
            mapDataStd [rangeNo] [0xD1CB - rangeMin] = 0x6340;
            mapDataStd [rangeNo] [0xD1CC - rangeMin] = 0x6348;
            mapDataStd [rangeNo] [0xD1CD - rangeMin] = 0x654A;
            mapDataStd [rangeNo] [0xD1CE - rangeMin] = 0x6546;
            mapDataStd [rangeNo] [0xD1CF - rangeMin] = 0x65C6;

            mapDataStd [rangeNo] [0xD1D0 - rangeMin] = 0x65C3;
            mapDataStd [rangeNo] [0xD1D1 - rangeMin] = 0x65C4;
            mapDataStd [rangeNo] [0xD1D2 - rangeMin] = 0x65C2;
            mapDataStd [rangeNo] [0xD1D3 - rangeMin] = 0x664A;
            mapDataStd [rangeNo] [0xD1D4 - rangeMin] = 0x665F;
            mapDataStd [rangeNo] [0xD1D5 - rangeMin] = 0x6647;
            mapDataStd [rangeNo] [0xD1D6 - rangeMin] = 0x6651;
            mapDataStd [rangeNo] [0xD1D7 - rangeMin] = 0x6712;
            mapDataStd [rangeNo] [0xD1D8 - rangeMin] = 0x6713;
            mapDataStd [rangeNo] [0xD1D9 - rangeMin] = 0x681F;
            mapDataStd [rangeNo] [0xD1DA - rangeMin] = 0x681A;
            mapDataStd [rangeNo] [0xD1DB - rangeMin] = 0x6849;
            mapDataStd [rangeNo] [0xD1DC - rangeMin] = 0x6832;
            mapDataStd [rangeNo] [0xD1DD - rangeMin] = 0x6833;
            mapDataStd [rangeNo] [0xD1DE - rangeMin] = 0x683B;
            mapDataStd [rangeNo] [0xD1DF - rangeMin] = 0x684B;

            mapDataStd [rangeNo] [0xD1E0 - rangeMin] = 0x684F;
            mapDataStd [rangeNo] [0xD1E1 - rangeMin] = 0x6816;
            mapDataStd [rangeNo] [0xD1E2 - rangeMin] = 0x6831;
            mapDataStd [rangeNo] [0xD1E3 - rangeMin] = 0x681C;
            mapDataStd [rangeNo] [0xD1E4 - rangeMin] = 0x6835;
            mapDataStd [rangeNo] [0xD1E5 - rangeMin] = 0x682B;
            mapDataStd [rangeNo] [0xD1E6 - rangeMin] = 0x682D;
            mapDataStd [rangeNo] [0xD1E7 - rangeMin] = 0x682F;
            mapDataStd [rangeNo] [0xD1E8 - rangeMin] = 0x684E;
            mapDataStd [rangeNo] [0xD1E9 - rangeMin] = 0x6844;
            mapDataStd [rangeNo] [0xD1EA - rangeMin] = 0x6834;
            mapDataStd [rangeNo] [0xD1EB - rangeMin] = 0x681D;
            mapDataStd [rangeNo] [0xD1EC - rangeMin] = 0x6812;
            mapDataStd [rangeNo] [0xD1ED - rangeMin] = 0x6814;
            mapDataStd [rangeNo] [0xD1EE - rangeMin] = 0x6826;
            mapDataStd [rangeNo] [0xD1EF - rangeMin] = 0x6828;

            mapDataStd [rangeNo] [0xD1F0 - rangeMin] = 0x682E;
            mapDataStd [rangeNo] [0xD1F1 - rangeMin] = 0x684D;
            mapDataStd [rangeNo] [0xD1F2 - rangeMin] = 0x683A;
            mapDataStd [rangeNo] [0xD1F3 - rangeMin] = 0x6825;
            mapDataStd [rangeNo] [0xD1F4 - rangeMin] = 0x6820;
            mapDataStd [rangeNo] [0xD1F5 - rangeMin] = 0x6B2C;
            mapDataStd [rangeNo] [0xD1F6 - rangeMin] = 0x6B2F;
            mapDataStd [rangeNo] [0xD1F7 - rangeMin] = 0x6B2D;
            mapDataStd [rangeNo] [0xD1F8 - rangeMin] = 0x6B31;
            mapDataStd [rangeNo] [0xD1F9 - rangeMin] = 0x6B34;
            mapDataStd [rangeNo] [0xD1FA - rangeMin] = 0x6B6D;
            mapDataStd [rangeNo] [0xD1FB - rangeMin] = 0x8082;
            mapDataStd [rangeNo] [0xD1FC - rangeMin] = 0x6B88;
            mapDataStd [rangeNo] [0xD1FD - rangeMin] = 0x6BE6;
            mapDataStd [rangeNo] [0xD1FE - rangeMin] = 0x6BE4;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xD240 - rangeMin] = 0x6BE8;
            mapDataStd [rangeNo] [0xD241 - rangeMin] = 0x6BE3;
            mapDataStd [rangeNo] [0xD242 - rangeMin] = 0x6BE2;
            mapDataStd [rangeNo] [0xD243 - rangeMin] = 0x6BE7;
            mapDataStd [rangeNo] [0xD244 - rangeMin] = 0x6C25;
            mapDataStd [rangeNo] [0xD245 - rangeMin] = 0x6D7A;
            mapDataStd [rangeNo] [0xD246 - rangeMin] = 0x6D63;
            mapDataStd [rangeNo] [0xD247 - rangeMin] = 0x6D64;
            mapDataStd [rangeNo] [0xD248 - rangeMin] = 0x6D76;
            mapDataStd [rangeNo] [0xD249 - rangeMin] = 0x6D0D;
            mapDataStd [rangeNo] [0xD24A - rangeMin] = 0x6D61;
            mapDataStd [rangeNo] [0xD24B - rangeMin] = 0x6D92;
            mapDataStd [rangeNo] [0xD24C - rangeMin] = 0x6D58;
            mapDataStd [rangeNo] [0xD24D - rangeMin] = 0x6D62;
            mapDataStd [rangeNo] [0xD24E - rangeMin] = 0x6D6D;
            mapDataStd [rangeNo] [0xD24F - rangeMin] = 0x6D6F;

            mapDataStd [rangeNo] [0xD250 - rangeMin] = 0x6D91;
            mapDataStd [rangeNo] [0xD251 - rangeMin] = 0x6D8D;
            mapDataStd [rangeNo] [0xD252 - rangeMin] = 0x6DEF;
            mapDataStd [rangeNo] [0xD253 - rangeMin] = 0x6D7F;
            mapDataStd [rangeNo] [0xD254 - rangeMin] = 0x6D86;
            mapDataStd [rangeNo] [0xD255 - rangeMin] = 0x6D5E;
            mapDataStd [rangeNo] [0xD256 - rangeMin] = 0x6D67;
            mapDataStd [rangeNo] [0xD257 - rangeMin] = 0x6D60;
            mapDataStd [rangeNo] [0xD258 - rangeMin] = 0x6D97;
            mapDataStd [rangeNo] [0xD259 - rangeMin] = 0x6D70;
            mapDataStd [rangeNo] [0xD25A - rangeMin] = 0x6D7C;
            mapDataStd [rangeNo] [0xD25B - rangeMin] = 0x6D5F;
            mapDataStd [rangeNo] [0xD25C - rangeMin] = 0x6D82;
            mapDataStd [rangeNo] [0xD25D - rangeMin] = 0x6D98;
            mapDataStd [rangeNo] [0xD25E - rangeMin] = 0x6D2F;
            mapDataStd [rangeNo] [0xD25F - rangeMin] = 0x6D68;

            mapDataStd [rangeNo] [0xD260 - rangeMin] = 0x6D8B;
            mapDataStd [rangeNo] [0xD261 - rangeMin] = 0x6D7E;
            mapDataStd [rangeNo] [0xD262 - rangeMin] = 0x6D80;
            mapDataStd [rangeNo] [0xD263 - rangeMin] = 0x6D84;
            mapDataStd [rangeNo] [0xD264 - rangeMin] = 0x6D16;
            mapDataStd [rangeNo] [0xD265 - rangeMin] = 0x6D83;
            mapDataStd [rangeNo] [0xD266 - rangeMin] = 0x6D7B;
            mapDataStd [rangeNo] [0xD267 - rangeMin] = 0x6D7D;
            mapDataStd [rangeNo] [0xD268 - rangeMin] = 0x6D75;
            mapDataStd [rangeNo] [0xD269 - rangeMin] = 0x6D90;
            mapDataStd [rangeNo] [0xD26A - rangeMin] = 0x70DC;
            mapDataStd [rangeNo] [0xD26B - rangeMin] = 0x70D3;
            mapDataStd [rangeNo] [0xD26C - rangeMin] = 0x70D1;
            mapDataStd [rangeNo] [0xD26D - rangeMin] = 0x70DD;
            mapDataStd [rangeNo] [0xD26E - rangeMin] = 0x70CB;
            mapDataStd [rangeNo] [0xD26F - rangeMin] = 0x7F39;

            mapDataStd [rangeNo] [0xD270 - rangeMin] = 0x70E2;
            mapDataStd [rangeNo] [0xD271 - rangeMin] = 0x70D7;
            mapDataStd [rangeNo] [0xD272 - rangeMin] = 0x70D2;
            mapDataStd [rangeNo] [0xD273 - rangeMin] = 0x70DE;
            mapDataStd [rangeNo] [0xD274 - rangeMin] = 0x70E0;
            mapDataStd [rangeNo] [0xD275 - rangeMin] = 0x70D4;
            mapDataStd [rangeNo] [0xD276 - rangeMin] = 0x70CD;
            mapDataStd [rangeNo] [0xD277 - rangeMin] = 0x70C5;
            mapDataStd [rangeNo] [0xD278 - rangeMin] = 0x70C6;
            mapDataStd [rangeNo] [0xD279 - rangeMin] = 0x70C7;
            mapDataStd [rangeNo] [0xD27A - rangeMin] = 0x70DA;
            mapDataStd [rangeNo] [0xD27B - rangeMin] = 0x70CE;
            mapDataStd [rangeNo] [0xD27C - rangeMin] = 0x70E1;
            mapDataStd [rangeNo] [0xD27D - rangeMin] = 0x7242;
            mapDataStd [rangeNo] [0xD27E - rangeMin] = 0x7278;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xD2A1 - rangeMin] = 0x7277;
            mapDataStd [rangeNo] [0xD2A2 - rangeMin] = 0x7276;
            mapDataStd [rangeNo] [0xD2A3 - rangeMin] = 0x7300;
            mapDataStd [rangeNo] [0xD2A4 - rangeMin] = 0x72FA;
            mapDataStd [rangeNo] [0xD2A5 - rangeMin] = 0x72F4;
            mapDataStd [rangeNo] [0xD2A6 - rangeMin] = 0x72FE;
            mapDataStd [rangeNo] [0xD2A7 - rangeMin] = 0x72F6;
            mapDataStd [rangeNo] [0xD2A8 - rangeMin] = 0x72F3;
            mapDataStd [rangeNo] [0xD2A9 - rangeMin] = 0x72FB;
            mapDataStd [rangeNo] [0xD2AA - rangeMin] = 0x7301;
            mapDataStd [rangeNo] [0xD2AB - rangeMin] = 0x73D3;
            mapDataStd [rangeNo] [0xD2AC - rangeMin] = 0x73D9;
            mapDataStd [rangeNo] [0xD2AD - rangeMin] = 0x73E5;
            mapDataStd [rangeNo] [0xD2AE - rangeMin] = 0x73D6;
            mapDataStd [rangeNo] [0xD2AF - rangeMin] = 0x73BC;

            mapDataStd [rangeNo] [0xD2B0 - rangeMin] = 0x73E7;
            mapDataStd [rangeNo] [0xD2B1 - rangeMin] = 0x73E3;
            mapDataStd [rangeNo] [0xD2B2 - rangeMin] = 0x73E9;
            mapDataStd [rangeNo] [0xD2B3 - rangeMin] = 0x73DC;
            mapDataStd [rangeNo] [0xD2B4 - rangeMin] = 0x73D2;
            mapDataStd [rangeNo] [0xD2B5 - rangeMin] = 0x73DB;
            mapDataStd [rangeNo] [0xD2B6 - rangeMin] = 0x73D4;
            mapDataStd [rangeNo] [0xD2B7 - rangeMin] = 0x73DD;
            mapDataStd [rangeNo] [0xD2B8 - rangeMin] = 0x73DA;
            mapDataStd [rangeNo] [0xD2B9 - rangeMin] = 0x73D7;
            mapDataStd [rangeNo] [0xD2BA - rangeMin] = 0x73D8;
            mapDataStd [rangeNo] [0xD2BB - rangeMin] = 0x73E8;
            mapDataStd [rangeNo] [0xD2BC - rangeMin] = 0x74DE;
            mapDataStd [rangeNo] [0xD2BD - rangeMin] = 0x74DF;
            mapDataStd [rangeNo] [0xD2BE - rangeMin] = 0x74F4;
            mapDataStd [rangeNo] [0xD2BF - rangeMin] = 0x74F5;

            mapDataStd [rangeNo] [0xD2C0 - rangeMin] = 0x7521;
            mapDataStd [rangeNo] [0xD2C1 - rangeMin] = 0x755B;
            mapDataStd [rangeNo] [0xD2C2 - rangeMin] = 0x755F;
            mapDataStd [rangeNo] [0xD2C3 - rangeMin] = 0x75B0;
            mapDataStd [rangeNo] [0xD2C4 - rangeMin] = 0x75C1;
            mapDataStd [rangeNo] [0xD2C5 - rangeMin] = 0x75BB;
            mapDataStd [rangeNo] [0xD2C6 - rangeMin] = 0x75C4;
            mapDataStd [rangeNo] [0xD2C7 - rangeMin] = 0x75C0;
            mapDataStd [rangeNo] [0xD2C8 - rangeMin] = 0x75BF;
            mapDataStd [rangeNo] [0xD2C9 - rangeMin] = 0x75B6;
            mapDataStd [rangeNo] [0xD2CA - rangeMin] = 0x75BA;
            mapDataStd [rangeNo] [0xD2CB - rangeMin] = 0x768A;
            mapDataStd [rangeNo] [0xD2CC - rangeMin] = 0x76C9;
            mapDataStd [rangeNo] [0xD2CD - rangeMin] = 0x771D;
            mapDataStd [rangeNo] [0xD2CE - rangeMin] = 0x771B;
            mapDataStd [rangeNo] [0xD2CF - rangeMin] = 0x7710;

            mapDataStd [rangeNo] [0xD2D0 - rangeMin] = 0x7713;
            mapDataStd [rangeNo] [0xD2D1 - rangeMin] = 0x7712;
            mapDataStd [rangeNo] [0xD2D2 - rangeMin] = 0x7723;
            mapDataStd [rangeNo] [0xD2D3 - rangeMin] = 0x7711;
            mapDataStd [rangeNo] [0xD2D4 - rangeMin] = 0x7715;
            mapDataStd [rangeNo] [0xD2D5 - rangeMin] = 0x7719;
            mapDataStd [rangeNo] [0xD2D6 - rangeMin] = 0x771A;
            mapDataStd [rangeNo] [0xD2D7 - rangeMin] = 0x7722;
            mapDataStd [rangeNo] [0xD2D8 - rangeMin] = 0x7727;
            mapDataStd [rangeNo] [0xD2D9 - rangeMin] = 0x7823;
            mapDataStd [rangeNo] [0xD2DA - rangeMin] = 0x782C;
            mapDataStd [rangeNo] [0xD2DB - rangeMin] = 0x7822;
            mapDataStd [rangeNo] [0xD2DC - rangeMin] = 0x7835;
            mapDataStd [rangeNo] [0xD2DD - rangeMin] = 0x782F;
            mapDataStd [rangeNo] [0xD2DE - rangeMin] = 0x7828;
            mapDataStd [rangeNo] [0xD2DF - rangeMin] = 0x782E;

            mapDataStd [rangeNo] [0xD2E0 - rangeMin] = 0x782B;
            mapDataStd [rangeNo] [0xD2E1 - rangeMin] = 0x7821;
            mapDataStd [rangeNo] [0xD2E2 - rangeMin] = 0x7829;
            mapDataStd [rangeNo] [0xD2E3 - rangeMin] = 0x7833;
            mapDataStd [rangeNo] [0xD2E4 - rangeMin] = 0x782A;
            mapDataStd [rangeNo] [0xD2E5 - rangeMin] = 0x7831;
            mapDataStd [rangeNo] [0xD2E6 - rangeMin] = 0x7954;
            mapDataStd [rangeNo] [0xD2E7 - rangeMin] = 0x795B;
            mapDataStd [rangeNo] [0xD2E8 - rangeMin] = 0x794F;
            mapDataStd [rangeNo] [0xD2E9 - rangeMin] = 0x795C;
            mapDataStd [rangeNo] [0xD2EA - rangeMin] = 0x7953;
            mapDataStd [rangeNo] [0xD2EB - rangeMin] = 0x7952;
            mapDataStd [rangeNo] [0xD2EC - rangeMin] = 0x7951;
            mapDataStd [rangeNo] [0xD2ED - rangeMin] = 0x79EB;
            mapDataStd [rangeNo] [0xD2EE - rangeMin] = 0x79EC;
            mapDataStd [rangeNo] [0xD2EF - rangeMin] = 0x79E0;

            mapDataStd [rangeNo] [0xD2F0 - rangeMin] = 0x79EE;
            mapDataStd [rangeNo] [0xD2F1 - rangeMin] = 0x79ED;
            mapDataStd [rangeNo] [0xD2F2 - rangeMin] = 0x79EA;
            mapDataStd [rangeNo] [0xD2F3 - rangeMin] = 0x79DC;
            mapDataStd [rangeNo] [0xD2F4 - rangeMin] = 0x79DE;
            mapDataStd [rangeNo] [0xD2F5 - rangeMin] = 0x79DD;
            mapDataStd [rangeNo] [0xD2F6 - rangeMin] = 0x7A86;
            mapDataStd [rangeNo] [0xD2F7 - rangeMin] = 0x7A89;
            mapDataStd [rangeNo] [0xD2F8 - rangeMin] = 0x7A85;
            mapDataStd [rangeNo] [0xD2F9 - rangeMin] = 0x7A8B;
            mapDataStd [rangeNo] [0xD2FA - rangeMin] = 0x7A8C;
            mapDataStd [rangeNo] [0xD2FB - rangeMin] = 0x7A8A;
            mapDataStd [rangeNo] [0xD2FC - rangeMin] = 0x7A87;
            mapDataStd [rangeNo] [0xD2FD - rangeMin] = 0x7AD8;
            mapDataStd [rangeNo] [0xD2FE - rangeMin] = 0x7B10;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xD340 - rangeMin] = 0x7B04;
            mapDataStd [rangeNo] [0xD341 - rangeMin] = 0x7B13;
            mapDataStd [rangeNo] [0xD342 - rangeMin] = 0x7B05;
            mapDataStd [rangeNo] [0xD343 - rangeMin] = 0x7B0F;
            mapDataStd [rangeNo] [0xD344 - rangeMin] = 0x7B08;
            mapDataStd [rangeNo] [0xD345 - rangeMin] = 0x7B0A;
            mapDataStd [rangeNo] [0xD346 - rangeMin] = 0x7B0E;
            mapDataStd [rangeNo] [0xD347 - rangeMin] = 0x7B09;
            mapDataStd [rangeNo] [0xD348 - rangeMin] = 0x7B12;
            mapDataStd [rangeNo] [0xD349 - rangeMin] = 0x7C84;
            mapDataStd [rangeNo] [0xD34A - rangeMin] = 0x7C91;
            mapDataStd [rangeNo] [0xD34B - rangeMin] = 0x7C8A;
            mapDataStd [rangeNo] [0xD34C - rangeMin] = 0x7C8C;
            mapDataStd [rangeNo] [0xD34D - rangeMin] = 0x7C88;
            mapDataStd [rangeNo] [0xD34E - rangeMin] = 0x7C8D;
            mapDataStd [rangeNo] [0xD34F - rangeMin] = 0x7C85;

            mapDataStd [rangeNo] [0xD350 - rangeMin] = 0x7D1E;
            mapDataStd [rangeNo] [0xD351 - rangeMin] = 0x7D1D;
            mapDataStd [rangeNo] [0xD352 - rangeMin] = 0x7D11;
            mapDataStd [rangeNo] [0xD353 - rangeMin] = 0x7D0E;
            mapDataStd [rangeNo] [0xD354 - rangeMin] = 0x7D18;
            mapDataStd [rangeNo] [0xD355 - rangeMin] = 0x7D16;
            mapDataStd [rangeNo] [0xD356 - rangeMin] = 0x7D13;
            mapDataStd [rangeNo] [0xD357 - rangeMin] = 0x7D1F;
            mapDataStd [rangeNo] [0xD358 - rangeMin] = 0x7D12;
            mapDataStd [rangeNo] [0xD359 - rangeMin] = 0x7D0F;
            mapDataStd [rangeNo] [0xD35A - rangeMin] = 0x7D0C;
            mapDataStd [rangeNo] [0xD35B - rangeMin] = 0x7F5C;
            mapDataStd [rangeNo] [0xD35C - rangeMin] = 0x7F61;
            mapDataStd [rangeNo] [0xD35D - rangeMin] = 0x7F5E;
            mapDataStd [rangeNo] [0xD35E - rangeMin] = 0x7F60;
            mapDataStd [rangeNo] [0xD35F - rangeMin] = 0x7F5D;

            mapDataStd [rangeNo] [0xD360 - rangeMin] = 0x7F5B;
            mapDataStd [rangeNo] [0xD361 - rangeMin] = 0x7F96;
            mapDataStd [rangeNo] [0xD362 - rangeMin] = 0x7F92;
            mapDataStd [rangeNo] [0xD363 - rangeMin] = 0x7FC3;
            mapDataStd [rangeNo] [0xD364 - rangeMin] = 0x7FC2;
            mapDataStd [rangeNo] [0xD365 - rangeMin] = 0x7FC0;
            mapDataStd [rangeNo] [0xD366 - rangeMin] = 0x8016;
            mapDataStd [rangeNo] [0xD367 - rangeMin] = 0x803E;
            mapDataStd [rangeNo] [0xD368 - rangeMin] = 0x8039;
            mapDataStd [rangeNo] [0xD369 - rangeMin] = 0x80FA;
            mapDataStd [rangeNo] [0xD36A - rangeMin] = 0x80F2;
            mapDataStd [rangeNo] [0xD36B - rangeMin] = 0x80F9;
            mapDataStd [rangeNo] [0xD36C - rangeMin] = 0x80F5;
            mapDataStd [rangeNo] [0xD36D - rangeMin] = 0x8101;
            mapDataStd [rangeNo] [0xD36E - rangeMin] = 0x80FB;
            mapDataStd [rangeNo] [0xD36F - rangeMin] = 0x8100;

            mapDataStd [rangeNo] [0xD370 - rangeMin] = 0x8201;
            mapDataStd [rangeNo] [0xD371 - rangeMin] = 0x822F;
            mapDataStd [rangeNo] [0xD372 - rangeMin] = 0x8225;
            mapDataStd [rangeNo] [0xD373 - rangeMin] = 0x8333;
            mapDataStd [rangeNo] [0xD374 - rangeMin] = 0x832D;
            mapDataStd [rangeNo] [0xD375 - rangeMin] = 0x8344;
            mapDataStd [rangeNo] [0xD376 - rangeMin] = 0x8319;
            mapDataStd [rangeNo] [0xD377 - rangeMin] = 0x8351;
            mapDataStd [rangeNo] [0xD378 - rangeMin] = 0x8325;
            mapDataStd [rangeNo] [0xD379 - rangeMin] = 0x8356;
            mapDataStd [rangeNo] [0xD37A - rangeMin] = 0x833F;
            mapDataStd [rangeNo] [0xD37B - rangeMin] = 0x8341;
            mapDataStd [rangeNo] [0xD37C - rangeMin] = 0x8326;
            mapDataStd [rangeNo] [0xD37D - rangeMin] = 0x831C;
            mapDataStd [rangeNo] [0xD37E - rangeMin] = 0x8322;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xD3A1 - rangeMin] = 0x8342;
            mapDataStd [rangeNo] [0xD3A2 - rangeMin] = 0x834E;
            mapDataStd [rangeNo] [0xD3A3 - rangeMin] = 0x831B;
            mapDataStd [rangeNo] [0xD3A4 - rangeMin] = 0x832A;
            mapDataStd [rangeNo] [0xD3A5 - rangeMin] = 0x8308;
            mapDataStd [rangeNo] [0xD3A6 - rangeMin] = 0x833C;
            mapDataStd [rangeNo] [0xD3A7 - rangeMin] = 0x834D;
            mapDataStd [rangeNo] [0xD3A8 - rangeMin] = 0x8316;
            mapDataStd [rangeNo] [0xD3A9 - rangeMin] = 0x8324;
            mapDataStd [rangeNo] [0xD3AA - rangeMin] = 0x8320;
            mapDataStd [rangeNo] [0xD3AB - rangeMin] = 0x8337;
            mapDataStd [rangeNo] [0xD3AC - rangeMin] = 0x832F;
            mapDataStd [rangeNo] [0xD3AD - rangeMin] = 0x8329;
            mapDataStd [rangeNo] [0xD3AE - rangeMin] = 0x8347;
            mapDataStd [rangeNo] [0xD3AF - rangeMin] = 0x8345;

            mapDataStd [rangeNo] [0xD3B0 - rangeMin] = 0x834C;
            mapDataStd [rangeNo] [0xD3B1 - rangeMin] = 0x8353;
            mapDataStd [rangeNo] [0xD3B2 - rangeMin] = 0x831E;
            mapDataStd [rangeNo] [0xD3B3 - rangeMin] = 0x832C;
            mapDataStd [rangeNo] [0xD3B4 - rangeMin] = 0x834B;
            mapDataStd [rangeNo] [0xD3B5 - rangeMin] = 0x8327;
            mapDataStd [rangeNo] [0xD3B6 - rangeMin] = 0x8348;
            mapDataStd [rangeNo] [0xD3B7 - rangeMin] = 0x8653;
            mapDataStd [rangeNo] [0xD3B8 - rangeMin] = 0x8652;
            mapDataStd [rangeNo] [0xD3B9 - rangeMin] = 0x86A2;
            mapDataStd [rangeNo] [0xD3BA - rangeMin] = 0x86A8;
            mapDataStd [rangeNo] [0xD3BB - rangeMin] = 0x8696;
            mapDataStd [rangeNo] [0xD3BC - rangeMin] = 0x868D;
            mapDataStd [rangeNo] [0xD3BD - rangeMin] = 0x8691;
            mapDataStd [rangeNo] [0xD3BE - rangeMin] = 0x869E;
            mapDataStd [rangeNo] [0xD3BF - rangeMin] = 0x8687;

            mapDataStd [rangeNo] [0xD3C0 - rangeMin] = 0x8697;
            mapDataStd [rangeNo] [0xD3C1 - rangeMin] = 0x8686;
            mapDataStd [rangeNo] [0xD3C2 - rangeMin] = 0x868B;
            mapDataStd [rangeNo] [0xD3C3 - rangeMin] = 0x869A;
            mapDataStd [rangeNo] [0xD3C4 - rangeMin] = 0x8685;
            mapDataStd [rangeNo] [0xD3C5 - rangeMin] = 0x86A5;
            mapDataStd [rangeNo] [0xD3C6 - rangeMin] = 0x8699;
            mapDataStd [rangeNo] [0xD3C7 - rangeMin] = 0x86A1;
            mapDataStd [rangeNo] [0xD3C8 - rangeMin] = 0x86A7;
            mapDataStd [rangeNo] [0xD3C9 - rangeMin] = 0x8695;
            mapDataStd [rangeNo] [0xD3CA - rangeMin] = 0x8698;
            mapDataStd [rangeNo] [0xD3CB - rangeMin] = 0x868E;
            mapDataStd [rangeNo] [0xD3CC - rangeMin] = 0x869D;
            mapDataStd [rangeNo] [0xD3CD - rangeMin] = 0x8690;
            mapDataStd [rangeNo] [0xD3CE - rangeMin] = 0x8694;
            mapDataStd [rangeNo] [0xD3CF - rangeMin] = 0x8843;

            mapDataStd [rangeNo] [0xD3D0 - rangeMin] = 0x8844;
            mapDataStd [rangeNo] [0xD3D1 - rangeMin] = 0x886D;
            mapDataStd [rangeNo] [0xD3D2 - rangeMin] = 0x8875;
            mapDataStd [rangeNo] [0xD3D3 - rangeMin] = 0x8876;
            mapDataStd [rangeNo] [0xD3D4 - rangeMin] = 0x8872;
            mapDataStd [rangeNo] [0xD3D5 - rangeMin] = 0x8880;
            mapDataStd [rangeNo] [0xD3D6 - rangeMin] = 0x8871;
            mapDataStd [rangeNo] [0xD3D7 - rangeMin] = 0x887F;
            mapDataStd [rangeNo] [0xD3D8 - rangeMin] = 0x886F;
            mapDataStd [rangeNo] [0xD3D9 - rangeMin] = 0x8883;
            mapDataStd [rangeNo] [0xD3DA - rangeMin] = 0x887E;
            mapDataStd [rangeNo] [0xD3DB - rangeMin] = 0x8874;
            mapDataStd [rangeNo] [0xD3DC - rangeMin] = 0x887C;
            mapDataStd [rangeNo] [0xD3DD - rangeMin] = 0x8A12;
            mapDataStd [rangeNo] [0xD3DE - rangeMin] = 0x8C47;
            mapDataStd [rangeNo] [0xD3DF - rangeMin] = 0x8C57;

            mapDataStd [rangeNo] [0xD3E0 - rangeMin] = 0x8C7B;
            mapDataStd [rangeNo] [0xD3E1 - rangeMin] = 0x8CA4;
            mapDataStd [rangeNo] [0xD3E2 - rangeMin] = 0x8CA3;
            mapDataStd [rangeNo] [0xD3E3 - rangeMin] = 0x8D76;
            mapDataStd [rangeNo] [0xD3E4 - rangeMin] = 0x8D78;
            mapDataStd [rangeNo] [0xD3E5 - rangeMin] = 0x8DB5;
            mapDataStd [rangeNo] [0xD3E6 - rangeMin] = 0x8DB7;
            mapDataStd [rangeNo] [0xD3E7 - rangeMin] = 0x8DB6;
            mapDataStd [rangeNo] [0xD3E8 - rangeMin] = 0x8ED1;
            mapDataStd [rangeNo] [0xD3E9 - rangeMin] = 0x8ED3;
            mapDataStd [rangeNo] [0xD3EA - rangeMin] = 0x8FFE;
            mapDataStd [rangeNo] [0xD3EB - rangeMin] = 0x8FF5;
            mapDataStd [rangeNo] [0xD3EC - rangeMin] = 0x9002;
            mapDataStd [rangeNo] [0xD3ED - rangeMin] = 0x8FFF;
            mapDataStd [rangeNo] [0xD3EE - rangeMin] = 0x8FFB;
            mapDataStd [rangeNo] [0xD3EF - rangeMin] = 0x9004;

            mapDataStd [rangeNo] [0xD3F0 - rangeMin] = 0x8FFC;
            mapDataStd [rangeNo] [0xD3F1 - rangeMin] = 0x8FF6;
            mapDataStd [rangeNo] [0xD3F2 - rangeMin] = 0x90D6;
            mapDataStd [rangeNo] [0xD3F3 - rangeMin] = 0x90E0;
            mapDataStd [rangeNo] [0xD3F4 - rangeMin] = 0x90D9;
            mapDataStd [rangeNo] [0xD3F5 - rangeMin] = 0x90DA;
            mapDataStd [rangeNo] [0xD3F6 - rangeMin] = 0x90E3;
            mapDataStd [rangeNo] [0xD3F7 - rangeMin] = 0x90DF;
            mapDataStd [rangeNo] [0xD3F8 - rangeMin] = 0x90E5;
            mapDataStd [rangeNo] [0xD3F9 - rangeMin] = 0x90D8;
            mapDataStd [rangeNo] [0xD3FA - rangeMin] = 0x90DB;
            mapDataStd [rangeNo] [0xD3FB - rangeMin] = 0x90D7;
            mapDataStd [rangeNo] [0xD3FC - rangeMin] = 0x90DC;
            mapDataStd [rangeNo] [0xD3FD - rangeMin] = 0x90E4;
            mapDataStd [rangeNo] [0xD3FE - rangeMin] = 0x9150;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xD440 - rangeMin] = 0x914E;
            mapDataStd [rangeNo] [0xD441 - rangeMin] = 0x914F;
            mapDataStd [rangeNo] [0xD442 - rangeMin] = 0x91D5;
            mapDataStd [rangeNo] [0xD443 - rangeMin] = 0x91E2;
            mapDataStd [rangeNo] [0xD444 - rangeMin] = 0x91DA;
            mapDataStd [rangeNo] [0xD445 - rangeMin] = 0x965C;
            mapDataStd [rangeNo] [0xD446 - rangeMin] = 0x965F;
            mapDataStd [rangeNo] [0xD447 - rangeMin] = 0x96BC;
            mapDataStd [rangeNo] [0xD448 - rangeMin] = 0x98E3;
            mapDataStd [rangeNo] [0xD449 - rangeMin] = 0x9ADF;
            mapDataStd [rangeNo] [0xD44A - rangeMin] = 0x9B2F;
            mapDataStd [rangeNo] [0xD44B - rangeMin] = 0x4E7F;
            mapDataStd [rangeNo] [0xD44C - rangeMin] = 0x5070;
            mapDataStd [rangeNo] [0xD44D - rangeMin] = 0x506A;
            mapDataStd [rangeNo] [0xD44E - rangeMin] = 0x5061;
            mapDataStd [rangeNo] [0xD44F - rangeMin] = 0x505E;

            mapDataStd [rangeNo] [0xD450 - rangeMin] = 0x5060;
            mapDataStd [rangeNo] [0xD451 - rangeMin] = 0x5053;
            mapDataStd [rangeNo] [0xD452 - rangeMin] = 0x504B;
            mapDataStd [rangeNo] [0xD453 - rangeMin] = 0x505D;
            mapDataStd [rangeNo] [0xD454 - rangeMin] = 0x5072;
            mapDataStd [rangeNo] [0xD455 - rangeMin] = 0x5048;
            mapDataStd [rangeNo] [0xD456 - rangeMin] = 0x504D;
            mapDataStd [rangeNo] [0xD457 - rangeMin] = 0x5041;
            mapDataStd [rangeNo] [0xD458 - rangeMin] = 0x505B;
            mapDataStd [rangeNo] [0xD459 - rangeMin] = 0x504A;
            mapDataStd [rangeNo] [0xD45A - rangeMin] = 0x5062;
            mapDataStd [rangeNo] [0xD45B - rangeMin] = 0x5015;
            mapDataStd [rangeNo] [0xD45C - rangeMin] = 0x5045;
            mapDataStd [rangeNo] [0xD45D - rangeMin] = 0x505F;
            mapDataStd [rangeNo] [0xD45E - rangeMin] = 0x5069;
            mapDataStd [rangeNo] [0xD45F - rangeMin] = 0x506B;

            mapDataStd [rangeNo] [0xD460 - rangeMin] = 0x5063;
            mapDataStd [rangeNo] [0xD461 - rangeMin] = 0x5064;
            mapDataStd [rangeNo] [0xD462 - rangeMin] = 0x5046;
            mapDataStd [rangeNo] [0xD463 - rangeMin] = 0x5040;
            mapDataStd [rangeNo] [0xD464 - rangeMin] = 0x506E;
            mapDataStd [rangeNo] [0xD465 - rangeMin] = 0x5073;
            mapDataStd [rangeNo] [0xD466 - rangeMin] = 0x5057;
            mapDataStd [rangeNo] [0xD467 - rangeMin] = 0x5051;
            mapDataStd [rangeNo] [0xD468 - rangeMin] = 0x51D0;
            mapDataStd [rangeNo] [0xD469 - rangeMin] = 0x526B;
            mapDataStd [rangeNo] [0xD46A - rangeMin] = 0x526D;
            mapDataStd [rangeNo] [0xD46B - rangeMin] = 0x526C;
            mapDataStd [rangeNo] [0xD46C - rangeMin] = 0x526E;
            mapDataStd [rangeNo] [0xD46D - rangeMin] = 0x52D6;
            mapDataStd [rangeNo] [0xD46E - rangeMin] = 0x52D3;
            mapDataStd [rangeNo] [0xD46F - rangeMin] = 0x532D;

            mapDataStd [rangeNo] [0xD470 - rangeMin] = 0x539C;
            mapDataStd [rangeNo] [0xD471 - rangeMin] = 0x5575;
            mapDataStd [rangeNo] [0xD472 - rangeMin] = 0x5576;
            mapDataStd [rangeNo] [0xD473 - rangeMin] = 0x553C;
            mapDataStd [rangeNo] [0xD474 - rangeMin] = 0x554D;
            mapDataStd [rangeNo] [0xD475 - rangeMin] = 0x5550;
            mapDataStd [rangeNo] [0xD476 - rangeMin] = 0x5534;
            mapDataStd [rangeNo] [0xD477 - rangeMin] = 0x552A;
            mapDataStd [rangeNo] [0xD478 - rangeMin] = 0x5551;
            mapDataStd [rangeNo] [0xD479 - rangeMin] = 0x5562;
            mapDataStd [rangeNo] [0xD47A - rangeMin] = 0x5536;
            mapDataStd [rangeNo] [0xD47B - rangeMin] = 0x5535;
            mapDataStd [rangeNo] [0xD47C - rangeMin] = 0x5530;
            mapDataStd [rangeNo] [0xD47D - rangeMin] = 0x5552;
            mapDataStd [rangeNo] [0xD47E - rangeMin] = 0x5545;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xD4A1 - rangeMin] = 0x550C;
            mapDataStd [rangeNo] [0xD4A2 - rangeMin] = 0x5532;
            mapDataStd [rangeNo] [0xD4A3 - rangeMin] = 0x5565;
            mapDataStd [rangeNo] [0xD4A4 - rangeMin] = 0x554E;
            mapDataStd [rangeNo] [0xD4A5 - rangeMin] = 0x5539;
            mapDataStd [rangeNo] [0xD4A6 - rangeMin] = 0x5548;
            mapDataStd [rangeNo] [0xD4A7 - rangeMin] = 0x552D;
            mapDataStd [rangeNo] [0xD4A8 - rangeMin] = 0x553B;
            mapDataStd [rangeNo] [0xD4A9 - rangeMin] = 0x5540;
            mapDataStd [rangeNo] [0xD4AA - rangeMin] = 0x554B;
            mapDataStd [rangeNo] [0xD4AB - rangeMin] = 0x570A;
            mapDataStd [rangeNo] [0xD4AC - rangeMin] = 0x5707;
            mapDataStd [rangeNo] [0xD4AD - rangeMin] = 0x57FB;
            mapDataStd [rangeNo] [0xD4AE - rangeMin] = 0x5814;
            mapDataStd [rangeNo] [0xD4AF - rangeMin] = 0x57E2;

            mapDataStd [rangeNo] [0xD4B0 - rangeMin] = 0x57F6;
            mapDataStd [rangeNo] [0xD4B1 - rangeMin] = 0x57DC;
            mapDataStd [rangeNo] [0xD4B2 - rangeMin] = 0x57F4;
            mapDataStd [rangeNo] [0xD4B3 - rangeMin] = 0x5800;
            mapDataStd [rangeNo] [0xD4B4 - rangeMin] = 0x57ED;
            mapDataStd [rangeNo] [0xD4B5 - rangeMin] = 0x57FD;
            mapDataStd [rangeNo] [0xD4B6 - rangeMin] = 0x5808;
            mapDataStd [rangeNo] [0xD4B7 - rangeMin] = 0x57F8;
            mapDataStd [rangeNo] [0xD4B8 - rangeMin] = 0x580B;
            mapDataStd [rangeNo] [0xD4B9 - rangeMin] = 0x57F3;
            mapDataStd [rangeNo] [0xD4BA - rangeMin] = 0x57CF;
            mapDataStd [rangeNo] [0xD4BB - rangeMin] = 0x5807;
            mapDataStd [rangeNo] [0xD4BC - rangeMin] = 0x57EE;
            mapDataStd [rangeNo] [0xD4BD - rangeMin] = 0x57E3;
            mapDataStd [rangeNo] [0xD4BE - rangeMin] = 0x57F2;
            mapDataStd [rangeNo] [0xD4BF - rangeMin] = 0x57E5;

            mapDataStd [rangeNo] [0xD4C0 - rangeMin] = 0x57EC;
            mapDataStd [rangeNo] [0xD4C1 - rangeMin] = 0x57E1;
            mapDataStd [rangeNo] [0xD4C2 - rangeMin] = 0x580E;
            mapDataStd [rangeNo] [0xD4C3 - rangeMin] = 0x57FC;
            mapDataStd [rangeNo] [0xD4C4 - rangeMin] = 0x5810;
            mapDataStd [rangeNo] [0xD4C5 - rangeMin] = 0x57E7;
            mapDataStd [rangeNo] [0xD4C6 - rangeMin] = 0x5801;
            mapDataStd [rangeNo] [0xD4C7 - rangeMin] = 0x580C;
            mapDataStd [rangeNo] [0xD4C8 - rangeMin] = 0x57F1;
            mapDataStd [rangeNo] [0xD4C9 - rangeMin] = 0x57E9;
            mapDataStd [rangeNo] [0xD4CA - rangeMin] = 0x57F0;
            mapDataStd [rangeNo] [0xD4CB - rangeMin] = 0x580D;
            mapDataStd [rangeNo] [0xD4CC - rangeMin] = 0x5804;
            mapDataStd [rangeNo] [0xD4CD - rangeMin] = 0x595C;
            mapDataStd [rangeNo] [0xD4CE - rangeMin] = 0x5A60;
            mapDataStd [rangeNo] [0xD4CF - rangeMin] = 0x5A58;

            mapDataStd [rangeNo] [0xD4D0 - rangeMin] = 0x5A55;
            mapDataStd [rangeNo] [0xD4D1 - rangeMin] = 0x5A67;
            mapDataStd [rangeNo] [0xD4D2 - rangeMin] = 0x5A5E;
            mapDataStd [rangeNo] [0xD4D3 - rangeMin] = 0x5A38;
            mapDataStd [rangeNo] [0xD4D4 - rangeMin] = 0x5A35;
            mapDataStd [rangeNo] [0xD4D5 - rangeMin] = 0x5A6D;
            mapDataStd [rangeNo] [0xD4D6 - rangeMin] = 0x5A50;
            mapDataStd [rangeNo] [0xD4D7 - rangeMin] = 0x5A5F;
            mapDataStd [rangeNo] [0xD4D8 - rangeMin] = 0x5A65;
            mapDataStd [rangeNo] [0xD4D9 - rangeMin] = 0x5A6C;
            mapDataStd [rangeNo] [0xD4DA - rangeMin] = 0x5A53;
            mapDataStd [rangeNo] [0xD4DB - rangeMin] = 0x5A64;
            mapDataStd [rangeNo] [0xD4DC - rangeMin] = 0x5A57;
            mapDataStd [rangeNo] [0xD4DD - rangeMin] = 0x5A43;
            mapDataStd [rangeNo] [0xD4DE - rangeMin] = 0x5A5D;
            mapDataStd [rangeNo] [0xD4DF - rangeMin] = 0x5A52;

            mapDataStd [rangeNo] [0xD4E0 - rangeMin] = 0x5A44;
            mapDataStd [rangeNo] [0xD4E1 - rangeMin] = 0x5A5B;
            mapDataStd [rangeNo] [0xD4E2 - rangeMin] = 0x5A48;
            mapDataStd [rangeNo] [0xD4E3 - rangeMin] = 0x5A8E;
            mapDataStd [rangeNo] [0xD4E4 - rangeMin] = 0x5A3E;
            mapDataStd [rangeNo] [0xD4E5 - rangeMin] = 0x5A4D;
            mapDataStd [rangeNo] [0xD4E6 - rangeMin] = 0x5A39;
            mapDataStd [rangeNo] [0xD4E7 - rangeMin] = 0x5A4C;
            mapDataStd [rangeNo] [0xD4E8 - rangeMin] = 0x5A70;
            mapDataStd [rangeNo] [0xD4E9 - rangeMin] = 0x5A69;
            mapDataStd [rangeNo] [0xD4EA - rangeMin] = 0x5A47;
            mapDataStd [rangeNo] [0xD4EB - rangeMin] = 0x5A51;
            mapDataStd [rangeNo] [0xD4EC - rangeMin] = 0x5A56;
            mapDataStd [rangeNo] [0xD4ED - rangeMin] = 0x5A42;
            mapDataStd [rangeNo] [0xD4EE - rangeMin] = 0x5A5C;
            mapDataStd [rangeNo] [0xD4EF - rangeMin] = 0x5B72;

            mapDataStd [rangeNo] [0xD4F0 - rangeMin] = 0x5B6E;
            mapDataStd [rangeNo] [0xD4F1 - rangeMin] = 0x5BC1;
            mapDataStd [rangeNo] [0xD4F2 - rangeMin] = 0x5BC0;
            mapDataStd [rangeNo] [0xD4F3 - rangeMin] = 0x5C59;
            mapDataStd [rangeNo] [0xD4F4 - rangeMin] = 0x5D1E;
            mapDataStd [rangeNo] [0xD4F5 - rangeMin] = 0x5D0B;
            mapDataStd [rangeNo] [0xD4F6 - rangeMin] = 0x5D1D;
            mapDataStd [rangeNo] [0xD4F7 - rangeMin] = 0x5D1A;
            mapDataStd [rangeNo] [0xD4F8 - rangeMin] = 0x5D20;
            mapDataStd [rangeNo] [0xD4F9 - rangeMin] = 0x5D0C;
            mapDataStd [rangeNo] [0xD4FA - rangeMin] = 0x5D28;
            mapDataStd [rangeNo] [0xD4FB - rangeMin] = 0x5D0D;
            mapDataStd [rangeNo] [0xD4FC - rangeMin] = 0x5D26;
            mapDataStd [rangeNo] [0xD4FD - rangeMin] = 0x5D25;
            mapDataStd [rangeNo] [0xD4FE - rangeMin] = 0x5D0F;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xD540 - rangeMin] = 0x5D30;
            mapDataStd [rangeNo] [0xD541 - rangeMin] = 0x5D12;
            mapDataStd [rangeNo] [0xD542 - rangeMin] = 0x5D23;
            mapDataStd [rangeNo] [0xD543 - rangeMin] = 0x5D1F;
            mapDataStd [rangeNo] [0xD544 - rangeMin] = 0x5D2E;
            mapDataStd [rangeNo] [0xD545 - rangeMin] = 0x5E3E;
            mapDataStd [rangeNo] [0xD546 - rangeMin] = 0x5E34;
            mapDataStd [rangeNo] [0xD547 - rangeMin] = 0x5EB1;
            mapDataStd [rangeNo] [0xD548 - rangeMin] = 0x5EB4;
            mapDataStd [rangeNo] [0xD549 - rangeMin] = 0x5EB9;
            mapDataStd [rangeNo] [0xD54A - rangeMin] = 0x5EB2;
            mapDataStd [rangeNo] [0xD54B - rangeMin] = 0x5EB3;
            mapDataStd [rangeNo] [0xD54C - rangeMin] = 0x5F36;
            mapDataStd [rangeNo] [0xD54D - rangeMin] = 0x5F38;
            mapDataStd [rangeNo] [0xD54E - rangeMin] = 0x5F9B;
            mapDataStd [rangeNo] [0xD54F - rangeMin] = 0x5F96;

            mapDataStd [rangeNo] [0xD550 - rangeMin] = 0x5F9F;
            mapDataStd [rangeNo] [0xD551 - rangeMin] = 0x608A;
            mapDataStd [rangeNo] [0xD552 - rangeMin] = 0x6090;
            mapDataStd [rangeNo] [0xD553 - rangeMin] = 0x6086;
            mapDataStd [rangeNo] [0xD554 - rangeMin] = 0x60BE;
            mapDataStd [rangeNo] [0xD555 - rangeMin] = 0x60B0;
            mapDataStd [rangeNo] [0xD556 - rangeMin] = 0x60BA;
            mapDataStd [rangeNo] [0xD557 - rangeMin] = 0x60D3;
            mapDataStd [rangeNo] [0xD558 - rangeMin] = 0x60D4;
            mapDataStd [rangeNo] [0xD559 - rangeMin] = 0x60CF;
            mapDataStd [rangeNo] [0xD55A - rangeMin] = 0x60E4;
            mapDataStd [rangeNo] [0xD55B - rangeMin] = 0x60D9;
            mapDataStd [rangeNo] [0xD55C - rangeMin] = 0x60DD;
            mapDataStd [rangeNo] [0xD55D - rangeMin] = 0x60C8;
            mapDataStd [rangeNo] [0xD55E - rangeMin] = 0x60B1;
            mapDataStd [rangeNo] [0xD55F - rangeMin] = 0x60DB;

            mapDataStd [rangeNo] [0xD560 - rangeMin] = 0x60B7;
            mapDataStd [rangeNo] [0xD561 - rangeMin] = 0x60CA;
            mapDataStd [rangeNo] [0xD562 - rangeMin] = 0x60BF;
            mapDataStd [rangeNo] [0xD563 - rangeMin] = 0x60C3;
            mapDataStd [rangeNo] [0xD564 - rangeMin] = 0x60CD;
            mapDataStd [rangeNo] [0xD565 - rangeMin] = 0x60C0;
            mapDataStd [rangeNo] [0xD566 - rangeMin] = 0x6332;
            mapDataStd [rangeNo] [0xD567 - rangeMin] = 0x6365;
            mapDataStd [rangeNo] [0xD568 - rangeMin] = 0x638A;
            mapDataStd [rangeNo] [0xD569 - rangeMin] = 0x6382;
            mapDataStd [rangeNo] [0xD56A - rangeMin] = 0x637D;
            mapDataStd [rangeNo] [0xD56B - rangeMin] = 0x63BD;
            mapDataStd [rangeNo] [0xD56C - rangeMin] = 0x639E;
            mapDataStd [rangeNo] [0xD56D - rangeMin] = 0x63AD;
            mapDataStd [rangeNo] [0xD56E - rangeMin] = 0x639D;
            mapDataStd [rangeNo] [0xD56F - rangeMin] = 0x6397;

            mapDataStd [rangeNo] [0xD570 - rangeMin] = 0x63AB;
            mapDataStd [rangeNo] [0xD571 - rangeMin] = 0x638E;
            mapDataStd [rangeNo] [0xD572 - rangeMin] = 0x636F;
            mapDataStd [rangeNo] [0xD573 - rangeMin] = 0x6387;
            mapDataStd [rangeNo] [0xD574 - rangeMin] = 0x6390;
            mapDataStd [rangeNo] [0xD575 - rangeMin] = 0x636E;
            mapDataStd [rangeNo] [0xD576 - rangeMin] = 0x63AF;
            mapDataStd [rangeNo] [0xD577 - rangeMin] = 0x6375;
            mapDataStd [rangeNo] [0xD578 - rangeMin] = 0x639C;
            mapDataStd [rangeNo] [0xD579 - rangeMin] = 0x636D;
            mapDataStd [rangeNo] [0xD57A - rangeMin] = 0x63AE;
            mapDataStd [rangeNo] [0xD57B - rangeMin] = 0x637C;
            mapDataStd [rangeNo] [0xD57C - rangeMin] = 0x63A4;
            mapDataStd [rangeNo] [0xD57D - rangeMin] = 0x633B;
            mapDataStd [rangeNo] [0xD57E - rangeMin] = 0x639F;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xD5A1 - rangeMin] = 0x6378;
            mapDataStd [rangeNo] [0xD5A2 - rangeMin] = 0x6385;
            mapDataStd [rangeNo] [0xD5A3 - rangeMin] = 0x6381;
            mapDataStd [rangeNo] [0xD5A4 - rangeMin] = 0x6391;
            mapDataStd [rangeNo] [0xD5A5 - rangeMin] = 0x638D;
            mapDataStd [rangeNo] [0xD5A6 - rangeMin] = 0x6370;
            mapDataStd [rangeNo] [0xD5A7 - rangeMin] = 0x6553;
            mapDataStd [rangeNo] [0xD5A8 - rangeMin] = 0x65CD;
            mapDataStd [rangeNo] [0xD5A9 - rangeMin] = 0x6665;
            mapDataStd [rangeNo] [0xD5AA - rangeMin] = 0x6661;
            mapDataStd [rangeNo] [0xD5AB - rangeMin] = 0x665B;
            mapDataStd [rangeNo] [0xD5AC - rangeMin] = 0x6659;
            mapDataStd [rangeNo] [0xD5AD - rangeMin] = 0x665C;
            mapDataStd [rangeNo] [0xD5AE - rangeMin] = 0x6662;
            mapDataStd [rangeNo] [0xD5AF - rangeMin] = 0x6718;

            mapDataStd [rangeNo] [0xD5B0 - rangeMin] = 0x6879;
            mapDataStd [rangeNo] [0xD5B1 - rangeMin] = 0x6887;
            mapDataStd [rangeNo] [0xD5B2 - rangeMin] = 0x6890;
            mapDataStd [rangeNo] [0xD5B3 - rangeMin] = 0x689C;
            mapDataStd [rangeNo] [0xD5B4 - rangeMin] = 0x686D;
            mapDataStd [rangeNo] [0xD5B5 - rangeMin] = 0x686E;
            mapDataStd [rangeNo] [0xD5B6 - rangeMin] = 0x68AE;
            mapDataStd [rangeNo] [0xD5B7 - rangeMin] = 0x68AB;
            mapDataStd [rangeNo] [0xD5B8 - rangeMin] = 0x6956;
            mapDataStd [rangeNo] [0xD5B9 - rangeMin] = 0x686F;
            mapDataStd [rangeNo] [0xD5BA - rangeMin] = 0x68A3;
            mapDataStd [rangeNo] [0xD5BB - rangeMin] = 0x68AC;
            mapDataStd [rangeNo] [0xD5BC - rangeMin] = 0x68A9;
            mapDataStd [rangeNo] [0xD5BD - rangeMin] = 0x6875;
            mapDataStd [rangeNo] [0xD5BE - rangeMin] = 0x6874;
            mapDataStd [rangeNo] [0xD5BF - rangeMin] = 0x68B2;

            mapDataStd [rangeNo] [0xD5C0 - rangeMin] = 0x688F;
            mapDataStd [rangeNo] [0xD5C1 - rangeMin] = 0x6877;
            mapDataStd [rangeNo] [0xD5C2 - rangeMin] = 0x6892;
            mapDataStd [rangeNo] [0xD5C3 - rangeMin] = 0x687C;
            mapDataStd [rangeNo] [0xD5C4 - rangeMin] = 0x686B;
            mapDataStd [rangeNo] [0xD5C5 - rangeMin] = 0x6872;
            mapDataStd [rangeNo] [0xD5C6 - rangeMin] = 0x68AA;
            mapDataStd [rangeNo] [0xD5C7 - rangeMin] = 0x6880;
            mapDataStd [rangeNo] [0xD5C8 - rangeMin] = 0x6871;
            mapDataStd [rangeNo] [0xD5C9 - rangeMin] = 0x687E;
            mapDataStd [rangeNo] [0xD5CA - rangeMin] = 0x689B;
            mapDataStd [rangeNo] [0xD5CB - rangeMin] = 0x6896;
            mapDataStd [rangeNo] [0xD5CC - rangeMin] = 0x688B;
            mapDataStd [rangeNo] [0xD5CD - rangeMin] = 0x68A0;
            mapDataStd [rangeNo] [0xD5CE - rangeMin] = 0x6889;
            mapDataStd [rangeNo] [0xD5CF - rangeMin] = 0x68A4;

            mapDataStd [rangeNo] [0xD5D0 - rangeMin] = 0x6878;
            mapDataStd [rangeNo] [0xD5D1 - rangeMin] = 0x687B;
            mapDataStd [rangeNo] [0xD5D2 - rangeMin] = 0x6891;
            mapDataStd [rangeNo] [0xD5D3 - rangeMin] = 0x688C;
            mapDataStd [rangeNo] [0xD5D4 - rangeMin] = 0x688A;
            mapDataStd [rangeNo] [0xD5D5 - rangeMin] = 0x687D;
            mapDataStd [rangeNo] [0xD5D6 - rangeMin] = 0x6B36;
            mapDataStd [rangeNo] [0xD5D7 - rangeMin] = 0x6B33;
            mapDataStd [rangeNo] [0xD5D8 - rangeMin] = 0x6B37;
            mapDataStd [rangeNo] [0xD5D9 - rangeMin] = 0x6B38;
            mapDataStd [rangeNo] [0xD5DA - rangeMin] = 0x6B91;
            mapDataStd [rangeNo] [0xD5DB - rangeMin] = 0x6B8F;
            mapDataStd [rangeNo] [0xD5DC - rangeMin] = 0x6B8D;
            mapDataStd [rangeNo] [0xD5DD - rangeMin] = 0x6B8E;
            mapDataStd [rangeNo] [0xD5DE - rangeMin] = 0x6B8C;
            mapDataStd [rangeNo] [0xD5DF - rangeMin] = 0x6C2A;

            mapDataStd [rangeNo] [0xD5E0 - rangeMin] = 0x6DC0;
            mapDataStd [rangeNo] [0xD5E1 - rangeMin] = 0x6DAB;
            mapDataStd [rangeNo] [0xD5E2 - rangeMin] = 0x6DB4;
            mapDataStd [rangeNo] [0xD5E3 - rangeMin] = 0x6DB3;
            mapDataStd [rangeNo] [0xD5E4 - rangeMin] = 0x6E74;
            mapDataStd [rangeNo] [0xD5E5 - rangeMin] = 0x6DAC;
            mapDataStd [rangeNo] [0xD5E6 - rangeMin] = 0x6DE9;
            mapDataStd [rangeNo] [0xD5E7 - rangeMin] = 0x6DE2;
            mapDataStd [rangeNo] [0xD5E8 - rangeMin] = 0x6DB7;
            mapDataStd [rangeNo] [0xD5E9 - rangeMin] = 0x6DF6;
            mapDataStd [rangeNo] [0xD5EA - rangeMin] = 0x6DD4;
            mapDataStd [rangeNo] [0xD5EB - rangeMin] = 0x6E00;
            mapDataStd [rangeNo] [0xD5EC - rangeMin] = 0x6DC8;
            mapDataStd [rangeNo] [0xD5ED - rangeMin] = 0x6DE0;
            mapDataStd [rangeNo] [0xD5EE - rangeMin] = 0x6DDF;
            mapDataStd [rangeNo] [0xD5EF - rangeMin] = 0x6DD6;

            mapDataStd [rangeNo] [0xD5F0 - rangeMin] = 0x6DBE;
            mapDataStd [rangeNo] [0xD5F1 - rangeMin] = 0x6DE5;
            mapDataStd [rangeNo] [0xD5F2 - rangeMin] = 0x6DDC;
            mapDataStd [rangeNo] [0xD5F3 - rangeMin] = 0x6DDD;
            mapDataStd [rangeNo] [0xD5F4 - rangeMin] = 0x6DDB;
            mapDataStd [rangeNo] [0xD5F5 - rangeMin] = 0x6DF4;
            mapDataStd [rangeNo] [0xD5F6 - rangeMin] = 0x6DCA;
            mapDataStd [rangeNo] [0xD5F7 - rangeMin] = 0x6DBD;
            mapDataStd [rangeNo] [0xD5F8 - rangeMin] = 0x6DED;
            mapDataStd [rangeNo] [0xD5F9 - rangeMin] = 0x6DF0;
            mapDataStd [rangeNo] [0xD5FA - rangeMin] = 0x6DBA;
            mapDataStd [rangeNo] [0xD5FB - rangeMin] = 0x6DD5;
            mapDataStd [rangeNo] [0xD5FC - rangeMin] = 0x6DC2;
            mapDataStd [rangeNo] [0xD5FD - rangeMin] = 0x6DCF;
            mapDataStd [rangeNo] [0xD5FE - rangeMin] = 0x6DC9;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xD640 - rangeMin] = 0x6DD0;
            mapDataStd [rangeNo] [0xD641 - rangeMin] = 0x6DF2;
            mapDataStd [rangeNo] [0xD642 - rangeMin] = 0x6DD3;
            mapDataStd [rangeNo] [0xD643 - rangeMin] = 0x6DFD;
            mapDataStd [rangeNo] [0xD644 - rangeMin] = 0x6DD7;
            mapDataStd [rangeNo] [0xD645 - rangeMin] = 0x6DCD;
            mapDataStd [rangeNo] [0xD646 - rangeMin] = 0x6DE3;
            mapDataStd [rangeNo] [0xD647 - rangeMin] = 0x6DBB;
            mapDataStd [rangeNo] [0xD648 - rangeMin] = 0x70FA;
            mapDataStd [rangeNo] [0xD649 - rangeMin] = 0x710D;
            mapDataStd [rangeNo] [0xD64A - rangeMin] = 0x70F7;
            mapDataStd [rangeNo] [0xD64B - rangeMin] = 0x7117;
            mapDataStd [rangeNo] [0xD64C - rangeMin] = 0x70F4;
            mapDataStd [rangeNo] [0xD64D - rangeMin] = 0x710C;
            mapDataStd [rangeNo] [0xD64E - rangeMin] = 0x70F0;
            mapDataStd [rangeNo] [0xD64F - rangeMin] = 0x7104;

            mapDataStd [rangeNo] [0xD650 - rangeMin] = 0x70F3;
            mapDataStd [rangeNo] [0xD651 - rangeMin] = 0x7110;
            mapDataStd [rangeNo] [0xD652 - rangeMin] = 0x70FC;
            mapDataStd [rangeNo] [0xD653 - rangeMin] = 0x70FF;
            mapDataStd [rangeNo] [0xD654 - rangeMin] = 0x7106;
            mapDataStd [rangeNo] [0xD655 - rangeMin] = 0x7113;
            mapDataStd [rangeNo] [0xD656 - rangeMin] = 0x7100;
            mapDataStd [rangeNo] [0xD657 - rangeMin] = 0x70F8;
            mapDataStd [rangeNo] [0xD658 - rangeMin] = 0x70F6;
            mapDataStd [rangeNo] [0xD659 - rangeMin] = 0x710B;
            mapDataStd [rangeNo] [0xD65A - rangeMin] = 0x7102;
            mapDataStd [rangeNo] [0xD65B - rangeMin] = 0x710E;
            mapDataStd [rangeNo] [0xD65C - rangeMin] = 0x727E;
            mapDataStd [rangeNo] [0xD65D - rangeMin] = 0x727B;
            mapDataStd [rangeNo] [0xD65E - rangeMin] = 0x727C;
            mapDataStd [rangeNo] [0xD65F - rangeMin] = 0x727F;

            mapDataStd [rangeNo] [0xD660 - rangeMin] = 0x731D;
            mapDataStd [rangeNo] [0xD661 - rangeMin] = 0x7317;
            mapDataStd [rangeNo] [0xD662 - rangeMin] = 0x7307;
            mapDataStd [rangeNo] [0xD663 - rangeMin] = 0x7311;
            mapDataStd [rangeNo] [0xD664 - rangeMin] = 0x7318;
            mapDataStd [rangeNo] [0xD665 - rangeMin] = 0x730A;
            mapDataStd [rangeNo] [0xD666 - rangeMin] = 0x7308;
            mapDataStd [rangeNo] [0xD667 - rangeMin] = 0x72FF;
            mapDataStd [rangeNo] [0xD668 - rangeMin] = 0x730F;
            mapDataStd [rangeNo] [0xD669 - rangeMin] = 0x731E;
            mapDataStd [rangeNo] [0xD66A - rangeMin] = 0x7388;
            mapDataStd [rangeNo] [0xD66B - rangeMin] = 0x73F6;
            mapDataStd [rangeNo] [0xD66C - rangeMin] = 0x73F8;
            mapDataStd [rangeNo] [0xD66D - rangeMin] = 0x73F5;
            mapDataStd [rangeNo] [0xD66E - rangeMin] = 0x7404;
            mapDataStd [rangeNo] [0xD66F - rangeMin] = 0x7401;

            mapDataStd [rangeNo] [0xD670 - rangeMin] = 0x73FD;
            mapDataStd [rangeNo] [0xD671 - rangeMin] = 0x7407;
            mapDataStd [rangeNo] [0xD672 - rangeMin] = 0x7400;
            mapDataStd [rangeNo] [0xD673 - rangeMin] = 0x73FA;
            mapDataStd [rangeNo] [0xD674 - rangeMin] = 0x73FC;
            mapDataStd [rangeNo] [0xD675 - rangeMin] = 0x73FF;
            mapDataStd [rangeNo] [0xD676 - rangeMin] = 0x740C;
            mapDataStd [rangeNo] [0xD677 - rangeMin] = 0x740B;
            mapDataStd [rangeNo] [0xD678 - rangeMin] = 0x73F4;
            mapDataStd [rangeNo] [0xD679 - rangeMin] = 0x7408;
            mapDataStd [rangeNo] [0xD67A - rangeMin] = 0x7564;
            mapDataStd [rangeNo] [0xD67B - rangeMin] = 0x7563;
            mapDataStd [rangeNo] [0xD67C - rangeMin] = 0x75CE;
            mapDataStd [rangeNo] [0xD67D - rangeMin] = 0x75D2;
            mapDataStd [rangeNo] [0xD67E - rangeMin] = 0x75CF;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xD6A1 - rangeMin] = 0x75CB;
            mapDataStd [rangeNo] [0xD6A2 - rangeMin] = 0x75CC;
            mapDataStd [rangeNo] [0xD6A3 - rangeMin] = 0x75D1;
            mapDataStd [rangeNo] [0xD6A4 - rangeMin] = 0x75D0;
            mapDataStd [rangeNo] [0xD6A5 - rangeMin] = 0x768F;
            mapDataStd [rangeNo] [0xD6A6 - rangeMin] = 0x7689;
            mapDataStd [rangeNo] [0xD6A7 - rangeMin] = 0x76D3;
            mapDataStd [rangeNo] [0xD6A8 - rangeMin] = 0x7739;
            mapDataStd [rangeNo] [0xD6A9 - rangeMin] = 0x772F;
            mapDataStd [rangeNo] [0xD6AA - rangeMin] = 0x772D;
            mapDataStd [rangeNo] [0xD6AB - rangeMin] = 0x7731;
            mapDataStd [rangeNo] [0xD6AC - rangeMin] = 0x7732;
            mapDataStd [rangeNo] [0xD6AD - rangeMin] = 0x7734;
            mapDataStd [rangeNo] [0xD6AE - rangeMin] = 0x7733;
            mapDataStd [rangeNo] [0xD6AF - rangeMin] = 0x773D;

            mapDataStd [rangeNo] [0xD6B0 - rangeMin] = 0x7725;
            mapDataStd [rangeNo] [0xD6B1 - rangeMin] = 0x773B;
            mapDataStd [rangeNo] [0xD6B2 - rangeMin] = 0x7735;
            mapDataStd [rangeNo] [0xD6B3 - rangeMin] = 0x7848;
            mapDataStd [rangeNo] [0xD6B4 - rangeMin] = 0x7852;
            mapDataStd [rangeNo] [0xD6B5 - rangeMin] = 0x7849;
            mapDataStd [rangeNo] [0xD6B6 - rangeMin] = 0x784D;
            mapDataStd [rangeNo] [0xD6B7 - rangeMin] = 0x784A;
            mapDataStd [rangeNo] [0xD6B8 - rangeMin] = 0x784C;
            mapDataStd [rangeNo] [0xD6B9 - rangeMin] = 0x7826;
            mapDataStd [rangeNo] [0xD6BA - rangeMin] = 0x7845;
            mapDataStd [rangeNo] [0xD6BB - rangeMin] = 0x7850;
            mapDataStd [rangeNo] [0xD6BC - rangeMin] = 0x7964;
            mapDataStd [rangeNo] [0xD6BD - rangeMin] = 0x7967;
            mapDataStd [rangeNo] [0xD6BE - rangeMin] = 0x7969;
            mapDataStd [rangeNo] [0xD6BF - rangeMin] = 0x796A;

            mapDataStd [rangeNo] [0xD6C0 - rangeMin] = 0x7963;
            mapDataStd [rangeNo] [0xD6C1 - rangeMin] = 0x796B;
            mapDataStd [rangeNo] [0xD6C2 - rangeMin] = 0x7961;
            mapDataStd [rangeNo] [0xD6C3 - rangeMin] = 0x79BB;
            mapDataStd [rangeNo] [0xD6C4 - rangeMin] = 0x79FA;
            mapDataStd [rangeNo] [0xD6C5 - rangeMin] = 0x79F8;
            mapDataStd [rangeNo] [0xD6C6 - rangeMin] = 0x79F6;
            mapDataStd [rangeNo] [0xD6C7 - rangeMin] = 0x79F7;
            mapDataStd [rangeNo] [0xD6C8 - rangeMin] = 0x7A8F;
            mapDataStd [rangeNo] [0xD6C9 - rangeMin] = 0x7A94;
            mapDataStd [rangeNo] [0xD6CA - rangeMin] = 0x7A90;
            mapDataStd [rangeNo] [0xD6CB - rangeMin] = 0x7B35;
            mapDataStd [rangeNo] [0xD6CC - rangeMin] = 0x7B47;
            mapDataStd [rangeNo] [0xD6CD - rangeMin] = 0x7B34;
            mapDataStd [rangeNo] [0xD6CE - rangeMin] = 0x7B25;
            mapDataStd [rangeNo] [0xD6CF - rangeMin] = 0x7B30;

            mapDataStd [rangeNo] [0xD6D0 - rangeMin] = 0x7B22;
            mapDataStd [rangeNo] [0xD6D1 - rangeMin] = 0x7B24;
            mapDataStd [rangeNo] [0xD6D2 - rangeMin] = 0x7B33;
            mapDataStd [rangeNo] [0xD6D3 - rangeMin] = 0x7B18;
            mapDataStd [rangeNo] [0xD6D4 - rangeMin] = 0x7B2A;
            mapDataStd [rangeNo] [0xD6D5 - rangeMin] = 0x7B1D;
            mapDataStd [rangeNo] [0xD6D6 - rangeMin] = 0x7B31;
            mapDataStd [rangeNo] [0xD6D7 - rangeMin] = 0x7B2B;
            mapDataStd [rangeNo] [0xD6D8 - rangeMin] = 0x7B2D;
            mapDataStd [rangeNo] [0xD6D9 - rangeMin] = 0x7B2F;
            mapDataStd [rangeNo] [0xD6DA - rangeMin] = 0x7B32;
            mapDataStd [rangeNo] [0xD6DB - rangeMin] = 0x7B38;
            mapDataStd [rangeNo] [0xD6DC - rangeMin] = 0x7B1A;
            mapDataStd [rangeNo] [0xD6DD - rangeMin] = 0x7B23;
            mapDataStd [rangeNo] [0xD6DE - rangeMin] = 0x7C94;
            mapDataStd [rangeNo] [0xD6DF - rangeMin] = 0x7C98;

            mapDataStd [rangeNo] [0xD6E0 - rangeMin] = 0x7C96;
            mapDataStd [rangeNo] [0xD6E1 - rangeMin] = 0x7CA3;
            mapDataStd [rangeNo] [0xD6E2 - rangeMin] = 0x7D35;
            mapDataStd [rangeNo] [0xD6E3 - rangeMin] = 0x7D3D;
            mapDataStd [rangeNo] [0xD6E4 - rangeMin] = 0x7D38;
            mapDataStd [rangeNo] [0xD6E5 - rangeMin] = 0x7D36;
            mapDataStd [rangeNo] [0xD6E6 - rangeMin] = 0x7D3A;
            mapDataStd [rangeNo] [0xD6E7 - rangeMin] = 0x7D45;
            mapDataStd [rangeNo] [0xD6E8 - rangeMin] = 0x7D2C;
            mapDataStd [rangeNo] [0xD6E9 - rangeMin] = 0x7D29;
            mapDataStd [rangeNo] [0xD6EA - rangeMin] = 0x7D41;
            mapDataStd [rangeNo] [0xD6EB - rangeMin] = 0x7D47;
            mapDataStd [rangeNo] [0xD6EC - rangeMin] = 0x7D3E;
            mapDataStd [rangeNo] [0xD6ED - rangeMin] = 0x7D3F;
            mapDataStd [rangeNo] [0xD6EE - rangeMin] = 0x7D4A;
            mapDataStd [rangeNo] [0xD6EF - rangeMin] = 0x7D3B;

            mapDataStd [rangeNo] [0xD6F0 - rangeMin] = 0x7D28;
            mapDataStd [rangeNo] [0xD6F1 - rangeMin] = 0x7F63;
            mapDataStd [rangeNo] [0xD6F2 - rangeMin] = 0x7F95;
            mapDataStd [rangeNo] [0xD6F3 - rangeMin] = 0x7F9C;
            mapDataStd [rangeNo] [0xD6F4 - rangeMin] = 0x7F9D;
            mapDataStd [rangeNo] [0xD6F5 - rangeMin] = 0x7F9B;
            mapDataStd [rangeNo] [0xD6F6 - rangeMin] = 0x7FCA;
            mapDataStd [rangeNo] [0xD6F7 - rangeMin] = 0x7FCB;
            mapDataStd [rangeNo] [0xD6F8 - rangeMin] = 0x7FCD;
            mapDataStd [rangeNo] [0xD6F9 - rangeMin] = 0x7FD0;
            mapDataStd [rangeNo] [0xD6FA - rangeMin] = 0x7FD1;
            mapDataStd [rangeNo] [0xD6FB - rangeMin] = 0x7FC7;
            mapDataStd [rangeNo] [0xD6FC - rangeMin] = 0x7FCF;
            mapDataStd [rangeNo] [0xD6FD - rangeMin] = 0x7FC9;
            mapDataStd [rangeNo] [0xD6FE - rangeMin] = 0x801F;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xD740 - rangeMin] = 0x801E;
            mapDataStd [rangeNo] [0xD741 - rangeMin] = 0x801B;
            mapDataStd [rangeNo] [0xD742 - rangeMin] = 0x8047;
            mapDataStd [rangeNo] [0xD743 - rangeMin] = 0x8043;
            mapDataStd [rangeNo] [0xD744 - rangeMin] = 0x8048;
            mapDataStd [rangeNo] [0xD745 - rangeMin] = 0x8118;
            mapDataStd [rangeNo] [0xD746 - rangeMin] = 0x8125;
            mapDataStd [rangeNo] [0xD747 - rangeMin] = 0x8119;
            mapDataStd [rangeNo] [0xD748 - rangeMin] = 0x811B;
            mapDataStd [rangeNo] [0xD749 - rangeMin] = 0x812D;
            mapDataStd [rangeNo] [0xD74A - rangeMin] = 0x811F;
            mapDataStd [rangeNo] [0xD74B - rangeMin] = 0x812C;
            mapDataStd [rangeNo] [0xD74C - rangeMin] = 0x811E;
            mapDataStd [rangeNo] [0xD74D - rangeMin] = 0x8121;
            mapDataStd [rangeNo] [0xD74E - rangeMin] = 0x8115;
            mapDataStd [rangeNo] [0xD74F - rangeMin] = 0x8127;

            mapDataStd [rangeNo] [0xD750 - rangeMin] = 0x811D;
            mapDataStd [rangeNo] [0xD751 - rangeMin] = 0x8122;
            mapDataStd [rangeNo] [0xD752 - rangeMin] = 0x8211;
            mapDataStd [rangeNo] [0xD753 - rangeMin] = 0x8238;
            mapDataStd [rangeNo] [0xD754 - rangeMin] = 0x8233;
            mapDataStd [rangeNo] [0xD755 - rangeMin] = 0x823A;
            mapDataStd [rangeNo] [0xD756 - rangeMin] = 0x8234;
            mapDataStd [rangeNo] [0xD757 - rangeMin] = 0x8232;
            mapDataStd [rangeNo] [0xD758 - rangeMin] = 0x8274;
            mapDataStd [rangeNo] [0xD759 - rangeMin] = 0x8390;
            mapDataStd [rangeNo] [0xD75A - rangeMin] = 0x83A3;
            mapDataStd [rangeNo] [0xD75B - rangeMin] = 0x83A8;
            mapDataStd [rangeNo] [0xD75C - rangeMin] = 0x838D;
            mapDataStd [rangeNo] [0xD75D - rangeMin] = 0x837A;
            mapDataStd [rangeNo] [0xD75E - rangeMin] = 0x8373;
            mapDataStd [rangeNo] [0xD75F - rangeMin] = 0x83A4;

            mapDataStd [rangeNo] [0xD760 - rangeMin] = 0x8374;
            mapDataStd [rangeNo] [0xD761 - rangeMin] = 0x838F;
            mapDataStd [rangeNo] [0xD762 - rangeMin] = 0x8381;
            mapDataStd [rangeNo] [0xD763 - rangeMin] = 0x8395;
            mapDataStd [rangeNo] [0xD764 - rangeMin] = 0x8399;
            mapDataStd [rangeNo] [0xD765 - rangeMin] = 0x8375;
            mapDataStd [rangeNo] [0xD766 - rangeMin] = 0x8394;
            mapDataStd [rangeNo] [0xD767 - rangeMin] = 0x83A9;
            mapDataStd [rangeNo] [0xD768 - rangeMin] = 0x837D;
            mapDataStd [rangeNo] [0xD769 - rangeMin] = 0x8383;
            mapDataStd [rangeNo] [0xD76A - rangeMin] = 0x838C;
            mapDataStd [rangeNo] [0xD76B - rangeMin] = 0x839D;
            mapDataStd [rangeNo] [0xD76C - rangeMin] = 0x839B;
            mapDataStd [rangeNo] [0xD76D - rangeMin] = 0x83AA;
            mapDataStd [rangeNo] [0xD76E - rangeMin] = 0x838B;
            mapDataStd [rangeNo] [0xD76F - rangeMin] = 0x837E;

            mapDataStd [rangeNo] [0xD770 - rangeMin] = 0x83A5;
            mapDataStd [rangeNo] [0xD771 - rangeMin] = 0x83AF;
            mapDataStd [rangeNo] [0xD772 - rangeMin] = 0x8388;
            mapDataStd [rangeNo] [0xD773 - rangeMin] = 0x8397;
            mapDataStd [rangeNo] [0xD774 - rangeMin] = 0x83B0;
            mapDataStd [rangeNo] [0xD775 - rangeMin] = 0x837F;
            mapDataStd [rangeNo] [0xD776 - rangeMin] = 0x83A6;
            mapDataStd [rangeNo] [0xD777 - rangeMin] = 0x8387;
            mapDataStd [rangeNo] [0xD778 - rangeMin] = 0x83AE;
            mapDataStd [rangeNo] [0xD779 - rangeMin] = 0x8376;
            mapDataStd [rangeNo] [0xD77A - rangeMin] = 0x839A;
            mapDataStd [rangeNo] [0xD77B - rangeMin] = 0x8659;
            mapDataStd [rangeNo] [0xD77C - rangeMin] = 0x8656;
            mapDataStd [rangeNo] [0xD77D - rangeMin] = 0x86BF;
            mapDataStd [rangeNo] [0xD77E - rangeMin] = 0x86B7;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xD7A1 - rangeMin] = 0x86C2;
            mapDataStd [rangeNo] [0xD7A2 - rangeMin] = 0x86C1;
            mapDataStd [rangeNo] [0xD7A3 - rangeMin] = 0x86C5;
            mapDataStd [rangeNo] [0xD7A4 - rangeMin] = 0x86BA;
            mapDataStd [rangeNo] [0xD7A5 - rangeMin] = 0x86B0;
            mapDataStd [rangeNo] [0xD7A6 - rangeMin] = 0x86C8;
            mapDataStd [rangeNo] [0xD7A7 - rangeMin] = 0x86B9;
            mapDataStd [rangeNo] [0xD7A8 - rangeMin] = 0x86B3;
            mapDataStd [rangeNo] [0xD7A9 - rangeMin] = 0x86B8;
            mapDataStd [rangeNo] [0xD7AA - rangeMin] = 0x86CC;
            mapDataStd [rangeNo] [0xD7AB - rangeMin] = 0x86B4;
            mapDataStd [rangeNo] [0xD7AC - rangeMin] = 0x86BB;
            mapDataStd [rangeNo] [0xD7AD - rangeMin] = 0x86BC;
            mapDataStd [rangeNo] [0xD7AE - rangeMin] = 0x86C3;
            mapDataStd [rangeNo] [0xD7AF - rangeMin] = 0x86BD;

            mapDataStd [rangeNo] [0xD7B0 - rangeMin] = 0x86BE;
            mapDataStd [rangeNo] [0xD7B1 - rangeMin] = 0x8852;
            mapDataStd [rangeNo] [0xD7B2 - rangeMin] = 0x8889;
            mapDataStd [rangeNo] [0xD7B3 - rangeMin] = 0x8895;
            mapDataStd [rangeNo] [0xD7B4 - rangeMin] = 0x88A8;
            mapDataStd [rangeNo] [0xD7B5 - rangeMin] = 0x88A2;
            mapDataStd [rangeNo] [0xD7B6 - rangeMin] = 0x88AA;
            mapDataStd [rangeNo] [0xD7B7 - rangeMin] = 0x889A;
            mapDataStd [rangeNo] [0xD7B8 - rangeMin] = 0x8891;
            mapDataStd [rangeNo] [0xD7B9 - rangeMin] = 0x88A1;
            mapDataStd [rangeNo] [0xD7BA - rangeMin] = 0x889F;
            mapDataStd [rangeNo] [0xD7BB - rangeMin] = 0x8898;
            mapDataStd [rangeNo] [0xD7BC - rangeMin] = 0x88A7;
            mapDataStd [rangeNo] [0xD7BD - rangeMin] = 0x8899;
            mapDataStd [rangeNo] [0xD7BE - rangeMin] = 0x889B;
            mapDataStd [rangeNo] [0xD7BF - rangeMin] = 0x8897;

            mapDataStd [rangeNo] [0xD7C0 - rangeMin] = 0x88A4;
            mapDataStd [rangeNo] [0xD7C1 - rangeMin] = 0x88AC;
            mapDataStd [rangeNo] [0xD7C2 - rangeMin] = 0x888C;
            mapDataStd [rangeNo] [0xD7C3 - rangeMin] = 0x8893;
            mapDataStd [rangeNo] [0xD7C4 - rangeMin] = 0x888E;
            mapDataStd [rangeNo] [0xD7C5 - rangeMin] = 0x8982;
            mapDataStd [rangeNo] [0xD7C6 - rangeMin] = 0x89D6;
            mapDataStd [rangeNo] [0xD7C7 - rangeMin] = 0x89D9;
            mapDataStd [rangeNo] [0xD7C8 - rangeMin] = 0x89D5;
            mapDataStd [rangeNo] [0xD7C9 - rangeMin] = 0x8A30;
            mapDataStd [rangeNo] [0xD7CA - rangeMin] = 0x8A27;
            mapDataStd [rangeNo] [0xD7CB - rangeMin] = 0x8A2C;
            mapDataStd [rangeNo] [0xD7CC - rangeMin] = 0x8A1E;
            mapDataStd [rangeNo] [0xD7CD - rangeMin] = 0x8C39;
            mapDataStd [rangeNo] [0xD7CE - rangeMin] = 0x8C3B;
            mapDataStd [rangeNo] [0xD7CF - rangeMin] = 0x8C5C;

            mapDataStd [rangeNo] [0xD7D0 - rangeMin] = 0x8C5D;
            mapDataStd [rangeNo] [0xD7D1 - rangeMin] = 0x8C7D;
            mapDataStd [rangeNo] [0xD7D2 - rangeMin] = 0x8CA5;
            mapDataStd [rangeNo] [0xD7D3 - rangeMin] = 0x8D7D;
            mapDataStd [rangeNo] [0xD7D4 - rangeMin] = 0x8D7B;
            mapDataStd [rangeNo] [0xD7D5 - rangeMin] = 0x8D79;
            mapDataStd [rangeNo] [0xD7D6 - rangeMin] = 0x8DBC;
            mapDataStd [rangeNo] [0xD7D7 - rangeMin] = 0x8DC2;
            mapDataStd [rangeNo] [0xD7D8 - rangeMin] = 0x8DB9;
            mapDataStd [rangeNo] [0xD7D9 - rangeMin] = 0x8DBF;
            mapDataStd [rangeNo] [0xD7DA - rangeMin] = 0x8DC1;
            mapDataStd [rangeNo] [0xD7DB - rangeMin] = 0x8ED8;
            mapDataStd [rangeNo] [0xD7DC - rangeMin] = 0x8EDE;
            mapDataStd [rangeNo] [0xD7DD - rangeMin] = 0x8EDD;
            mapDataStd [rangeNo] [0xD7DE - rangeMin] = 0x8EDC;
            mapDataStd [rangeNo] [0xD7DF - rangeMin] = 0x8ED7;

            mapDataStd [rangeNo] [0xD7E0 - rangeMin] = 0x8EE0;
            mapDataStd [rangeNo] [0xD7E1 - rangeMin] = 0x8EE1;
            mapDataStd [rangeNo] [0xD7E2 - rangeMin] = 0x9024;
            mapDataStd [rangeNo] [0xD7E3 - rangeMin] = 0x900B;
            mapDataStd [rangeNo] [0xD7E4 - rangeMin] = 0x9011;
            mapDataStd [rangeNo] [0xD7E5 - rangeMin] = 0x901C;
            mapDataStd [rangeNo] [0xD7E6 - rangeMin] = 0x900C;
            mapDataStd [rangeNo] [0xD7E7 - rangeMin] = 0x9021;
            mapDataStd [rangeNo] [0xD7E8 - rangeMin] = 0x90EF;
            mapDataStd [rangeNo] [0xD7E9 - rangeMin] = 0x90EA;
            mapDataStd [rangeNo] [0xD7EA - rangeMin] = 0x90F0;
            mapDataStd [rangeNo] [0xD7EB - rangeMin] = 0x90F4;
            mapDataStd [rangeNo] [0xD7EC - rangeMin] = 0x90F2;
            mapDataStd [rangeNo] [0xD7ED - rangeMin] = 0x90F3;
            mapDataStd [rangeNo] [0xD7EE - rangeMin] = 0x90D4;
            mapDataStd [rangeNo] [0xD7EF - rangeMin] = 0x90EB;

            mapDataStd [rangeNo] [0xD7F0 - rangeMin] = 0x90EC;
            mapDataStd [rangeNo] [0xD7F1 - rangeMin] = 0x90E9;
            mapDataStd [rangeNo] [0xD7F2 - rangeMin] = 0x9156;
            mapDataStd [rangeNo] [0xD7F3 - rangeMin] = 0x9158;
            mapDataStd [rangeNo] [0xD7F4 - rangeMin] = 0x915A;
            mapDataStd [rangeNo] [0xD7F5 - rangeMin] = 0x9153;
            mapDataStd [rangeNo] [0xD7F6 - rangeMin] = 0x9155;
            mapDataStd [rangeNo] [0xD7F7 - rangeMin] = 0x91EC;
            mapDataStd [rangeNo] [0xD7F8 - rangeMin] = 0x91F4;
            mapDataStd [rangeNo] [0xD7F9 - rangeMin] = 0x91F1;
            mapDataStd [rangeNo] [0xD7FA - rangeMin] = 0x91F3;
            mapDataStd [rangeNo] [0xD7FB - rangeMin] = 0x91F8;
            mapDataStd [rangeNo] [0xD7FC - rangeMin] = 0x91E4;
            mapDataStd [rangeNo] [0xD7FD - rangeMin] = 0x91F9;
            mapDataStd [rangeNo] [0xD7FE - rangeMin] = 0x91EA;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xD840 - rangeMin] = 0x91EB;
            mapDataStd [rangeNo] [0xD841 - rangeMin] = 0x91F7;
            mapDataStd [rangeNo] [0xD842 - rangeMin] = 0x91E8;
            mapDataStd [rangeNo] [0xD843 - rangeMin] = 0x91EE;
            mapDataStd [rangeNo] [0xD844 - rangeMin] = 0x957A;
            mapDataStd [rangeNo] [0xD845 - rangeMin] = 0x9586;
            mapDataStd [rangeNo] [0xD846 - rangeMin] = 0x9588;
            mapDataStd [rangeNo] [0xD847 - rangeMin] = 0x967C;
            mapDataStd [rangeNo] [0xD848 - rangeMin] = 0x966D;
            mapDataStd [rangeNo] [0xD849 - rangeMin] = 0x966B;
            mapDataStd [rangeNo] [0xD84A - rangeMin] = 0x9671;
            mapDataStd [rangeNo] [0xD84B - rangeMin] = 0x966F;
            mapDataStd [rangeNo] [0xD84C - rangeMin] = 0x96BF;
            mapDataStd [rangeNo] [0xD84D - rangeMin] = 0x976A;
            mapDataStd [rangeNo] [0xD84E - rangeMin] = 0x9804;
            mapDataStd [rangeNo] [0xD84F - rangeMin] = 0x98E5;

            mapDataStd [rangeNo] [0xD850 - rangeMin] = 0x9997;
            mapDataStd [rangeNo] [0xD851 - rangeMin] = 0x509B;
            mapDataStd [rangeNo] [0xD852 - rangeMin] = 0x5095;
            mapDataStd [rangeNo] [0xD853 - rangeMin] = 0x5094;
            mapDataStd [rangeNo] [0xD854 - rangeMin] = 0x509E;
            mapDataStd [rangeNo] [0xD855 - rangeMin] = 0x508B;
            mapDataStd [rangeNo] [0xD856 - rangeMin] = 0x50A3;
            mapDataStd [rangeNo] [0xD857 - rangeMin] = 0x5083;
            mapDataStd [rangeNo] [0xD858 - rangeMin] = 0x508C;
            mapDataStd [rangeNo] [0xD859 - rangeMin] = 0x508E;
            mapDataStd [rangeNo] [0xD85A - rangeMin] = 0x509D;
            mapDataStd [rangeNo] [0xD85B - rangeMin] = 0x5068;
            mapDataStd [rangeNo] [0xD85C - rangeMin] = 0x509C;
            mapDataStd [rangeNo] [0xD85D - rangeMin] = 0x5092;
            mapDataStd [rangeNo] [0xD85E - rangeMin] = 0x5082;
            mapDataStd [rangeNo] [0xD85F - rangeMin] = 0x5087;

            mapDataStd [rangeNo] [0xD860 - rangeMin] = 0x515F;
            mapDataStd [rangeNo] [0xD861 - rangeMin] = 0x51D4;
            mapDataStd [rangeNo] [0xD862 - rangeMin] = 0x5312;
            mapDataStd [rangeNo] [0xD863 - rangeMin] = 0x5311;
            mapDataStd [rangeNo] [0xD864 - rangeMin] = 0x53A4;
            mapDataStd [rangeNo] [0xD865 - rangeMin] = 0x53A7;
            mapDataStd [rangeNo] [0xD866 - rangeMin] = 0x5591;
            mapDataStd [rangeNo] [0xD867 - rangeMin] = 0x55A8;
            mapDataStd [rangeNo] [0xD868 - rangeMin] = 0x55A5;
            mapDataStd [rangeNo] [0xD869 - rangeMin] = 0x55AD;
            mapDataStd [rangeNo] [0xD86A - rangeMin] = 0x5577;
            mapDataStd [rangeNo] [0xD86B - rangeMin] = 0x5645;
            mapDataStd [rangeNo] [0xD86C - rangeMin] = 0x55A2;
            mapDataStd [rangeNo] [0xD86D - rangeMin] = 0x5593;
            mapDataStd [rangeNo] [0xD86E - rangeMin] = 0x5588;
            mapDataStd [rangeNo] [0xD86F - rangeMin] = 0x558F;

            mapDataStd [rangeNo] [0xD870 - rangeMin] = 0x55B5;
            mapDataStd [rangeNo] [0xD871 - rangeMin] = 0x5581;
            mapDataStd [rangeNo] [0xD872 - rangeMin] = 0x55A3;
            mapDataStd [rangeNo] [0xD873 - rangeMin] = 0x5592;
            mapDataStd [rangeNo] [0xD874 - rangeMin] = 0x55A4;
            mapDataStd [rangeNo] [0xD875 - rangeMin] = 0x557D;
            mapDataStd [rangeNo] [0xD876 - rangeMin] = 0x558C;
            mapDataStd [rangeNo] [0xD877 - rangeMin] = 0x55A6;
            mapDataStd [rangeNo] [0xD878 - rangeMin] = 0x557F;
            mapDataStd [rangeNo] [0xD879 - rangeMin] = 0x5595;
            mapDataStd [rangeNo] [0xD87A - rangeMin] = 0x55A1;
            mapDataStd [rangeNo] [0xD87B - rangeMin] = 0x558E;
            mapDataStd [rangeNo] [0xD87C - rangeMin] = 0x570C;
            mapDataStd [rangeNo] [0xD87D - rangeMin] = 0x5829;
            mapDataStd [rangeNo] [0xD87E - rangeMin] = 0x5837;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xD8A1 - rangeMin] = 0x5819;
            mapDataStd [rangeNo] [0xD8A2 - rangeMin] = 0x581E;
            mapDataStd [rangeNo] [0xD8A3 - rangeMin] = 0x5827;
            mapDataStd [rangeNo] [0xD8A4 - rangeMin] = 0x5823;
            mapDataStd [rangeNo] [0xD8A5 - rangeMin] = 0x5828;
            mapDataStd [rangeNo] [0xD8A6 - rangeMin] = 0x57F5;
            mapDataStd [rangeNo] [0xD8A7 - rangeMin] = 0x5848;
            mapDataStd [rangeNo] [0xD8A8 - rangeMin] = 0x5825;
            mapDataStd [rangeNo] [0xD8A9 - rangeMin] = 0x581C;
            mapDataStd [rangeNo] [0xD8AA - rangeMin] = 0x581B;
            mapDataStd [rangeNo] [0xD8AB - rangeMin] = 0x5833;
            mapDataStd [rangeNo] [0xD8AC - rangeMin] = 0x583F;
            mapDataStd [rangeNo] [0xD8AD - rangeMin] = 0x5836;
            mapDataStd [rangeNo] [0xD8AE - rangeMin] = 0x582E;
            mapDataStd [rangeNo] [0xD8AF - rangeMin] = 0x5839;

            mapDataStd [rangeNo] [0xD8B0 - rangeMin] = 0x5838;
            mapDataStd [rangeNo] [0xD8B1 - rangeMin] = 0x582D;
            mapDataStd [rangeNo] [0xD8B2 - rangeMin] = 0x582C;
            mapDataStd [rangeNo] [0xD8B3 - rangeMin] = 0x583B;
            mapDataStd [rangeNo] [0xD8B4 - rangeMin] = 0x5961;
            mapDataStd [rangeNo] [0xD8B5 - rangeMin] = 0x5AAF;
            mapDataStd [rangeNo] [0xD8B6 - rangeMin] = 0x5A94;
            mapDataStd [rangeNo] [0xD8B7 - rangeMin] = 0x5A9F;
            mapDataStd [rangeNo] [0xD8B8 - rangeMin] = 0x5A7A;
            mapDataStd [rangeNo] [0xD8B9 - rangeMin] = 0x5AA2;
            mapDataStd [rangeNo] [0xD8BA - rangeMin] = 0x5A9E;
            mapDataStd [rangeNo] [0xD8BB - rangeMin] = 0x5A78;
            mapDataStd [rangeNo] [0xD8BC - rangeMin] = 0x5AA6;
            mapDataStd [rangeNo] [0xD8BD - rangeMin] = 0x5A7C;
            mapDataStd [rangeNo] [0xD8BE - rangeMin] = 0x5AA5;
            mapDataStd [rangeNo] [0xD8BF - rangeMin] = 0x5AAC;

            mapDataStd [rangeNo] [0xD8C0 - rangeMin] = 0x5A95;
            mapDataStd [rangeNo] [0xD8C1 - rangeMin] = 0x5AAE;
            mapDataStd [rangeNo] [0xD8C2 - rangeMin] = 0x5A37;
            mapDataStd [rangeNo] [0xD8C3 - rangeMin] = 0x5A84;
            mapDataStd [rangeNo] [0xD8C4 - rangeMin] = 0x5A8A;
            mapDataStd [rangeNo] [0xD8C5 - rangeMin] = 0x5A97;
            mapDataStd [rangeNo] [0xD8C6 - rangeMin] = 0x5A83;
            mapDataStd [rangeNo] [0xD8C7 - rangeMin] = 0x5A8B;
            mapDataStd [rangeNo] [0xD8C8 - rangeMin] = 0x5AA9;
            mapDataStd [rangeNo] [0xD8C9 - rangeMin] = 0x5A7B;
            mapDataStd [rangeNo] [0xD8CA - rangeMin] = 0x5A7D;
            mapDataStd [rangeNo] [0xD8CB - rangeMin] = 0x5A8C;
            mapDataStd [rangeNo] [0xD8CC - rangeMin] = 0x5A9C;
            mapDataStd [rangeNo] [0xD8CD - rangeMin] = 0x5A8F;
            mapDataStd [rangeNo] [0xD8CE - rangeMin] = 0x5A93;
            mapDataStd [rangeNo] [0xD8CF - rangeMin] = 0x5A9D;

            mapDataStd [rangeNo] [0xD8D0 - rangeMin] = 0x5BEA;
            mapDataStd [rangeNo] [0xD8D1 - rangeMin] = 0x5BCD;
            mapDataStd [rangeNo] [0xD8D2 - rangeMin] = 0x5BCB;
            mapDataStd [rangeNo] [0xD8D3 - rangeMin] = 0x5BD4;
            mapDataStd [rangeNo] [0xD8D4 - rangeMin] = 0x5BD1;
            mapDataStd [rangeNo] [0xD8D5 - rangeMin] = 0x5BCA;
            mapDataStd [rangeNo] [0xD8D6 - rangeMin] = 0x5BCE;
            mapDataStd [rangeNo] [0xD8D7 - rangeMin] = 0x5C0C;
            mapDataStd [rangeNo] [0xD8D8 - rangeMin] = 0x5C30;
            mapDataStd [rangeNo] [0xD8D9 - rangeMin] = 0x5D37;
            mapDataStd [rangeNo] [0xD8DA - rangeMin] = 0x5D43;
            mapDataStd [rangeNo] [0xD8DB - rangeMin] = 0x5D6B;
            mapDataStd [rangeNo] [0xD8DC - rangeMin] = 0x5D41;
            mapDataStd [rangeNo] [0xD8DD - rangeMin] = 0x5D4B;
            mapDataStd [rangeNo] [0xD8DE - rangeMin] = 0x5D3F;
            mapDataStd [rangeNo] [0xD8DF - rangeMin] = 0x5D35;

            mapDataStd [rangeNo] [0xD8E0 - rangeMin] = 0x5D51;
            mapDataStd [rangeNo] [0xD8E1 - rangeMin] = 0x5D4E;
            mapDataStd [rangeNo] [0xD8E2 - rangeMin] = 0x5D55;
            mapDataStd [rangeNo] [0xD8E3 - rangeMin] = 0x5D33;
            mapDataStd [rangeNo] [0xD8E4 - rangeMin] = 0x5D3A;
            mapDataStd [rangeNo] [0xD8E5 - rangeMin] = 0x5D52;
            mapDataStd [rangeNo] [0xD8E6 - rangeMin] = 0x5D3D;
            mapDataStd [rangeNo] [0xD8E7 - rangeMin] = 0x5D31;
            mapDataStd [rangeNo] [0xD8E8 - rangeMin] = 0x5D59;
            mapDataStd [rangeNo] [0xD8E9 - rangeMin] = 0x5D42;
            mapDataStd [rangeNo] [0xD8EA - rangeMin] = 0x5D39;
            mapDataStd [rangeNo] [0xD8EB - rangeMin] = 0x5D49;
            mapDataStd [rangeNo] [0xD8EC - rangeMin] = 0x5D38;
            mapDataStd [rangeNo] [0xD8ED - rangeMin] = 0x5D3C;
            mapDataStd [rangeNo] [0xD8EE - rangeMin] = 0x5D32;
            mapDataStd [rangeNo] [0xD8EF - rangeMin] = 0x5D36;

            mapDataStd [rangeNo] [0xD8F0 - rangeMin] = 0x5D40;
            mapDataStd [rangeNo] [0xD8F1 - rangeMin] = 0x5D45;
            mapDataStd [rangeNo] [0xD8F2 - rangeMin] = 0x5E44;
            mapDataStd [rangeNo] [0xD8F3 - rangeMin] = 0x5E41;
            mapDataStd [rangeNo] [0xD8F4 - rangeMin] = 0x5F58;
            mapDataStd [rangeNo] [0xD8F5 - rangeMin] = 0x5FA6;
            mapDataStd [rangeNo] [0xD8F6 - rangeMin] = 0x5FA5;
            mapDataStd [rangeNo] [0xD8F7 - rangeMin] = 0x5FAB;
            mapDataStd [rangeNo] [0xD8F8 - rangeMin] = 0x60C9;
            mapDataStd [rangeNo] [0xD8F9 - rangeMin] = 0x60B9;
            mapDataStd [rangeNo] [0xD8FA - rangeMin] = 0x60CC;
            mapDataStd [rangeNo] [0xD8FB - rangeMin] = 0x60E2;
            mapDataStd [rangeNo] [0xD8FC - rangeMin] = 0x60CE;
            mapDataStd [rangeNo] [0xD8FD - rangeMin] = 0x60C4;
            mapDataStd [rangeNo] [0xD8FE - rangeMin] = 0x6114;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xD940 - rangeMin] = 0x60F2;
            mapDataStd [rangeNo] [0xD941 - rangeMin] = 0x610A;
            mapDataStd [rangeNo] [0xD942 - rangeMin] = 0x6116;
            mapDataStd [rangeNo] [0xD943 - rangeMin] = 0x6105;
            mapDataStd [rangeNo] [0xD944 - rangeMin] = 0x60F5;
            mapDataStd [rangeNo] [0xD945 - rangeMin] = 0x6113;
            mapDataStd [rangeNo] [0xD946 - rangeMin] = 0x60F8;
            mapDataStd [rangeNo] [0xD947 - rangeMin] = 0x60FC;
            mapDataStd [rangeNo] [0xD948 - rangeMin] = 0x60FE;
            mapDataStd [rangeNo] [0xD949 - rangeMin] = 0x60C1;
            mapDataStd [rangeNo] [0xD94A - rangeMin] = 0x6103;
            mapDataStd [rangeNo] [0xD94B - rangeMin] = 0x6118;
            mapDataStd [rangeNo] [0xD94C - rangeMin] = 0x611D;
            mapDataStd [rangeNo] [0xD94D - rangeMin] = 0x6110;
            mapDataStd [rangeNo] [0xD94E - rangeMin] = 0x60FF;
            mapDataStd [rangeNo] [0xD94F - rangeMin] = 0x6104;

            mapDataStd [rangeNo] [0xD950 - rangeMin] = 0x610B;
            mapDataStd [rangeNo] [0xD951 - rangeMin] = 0x624A;
            mapDataStd [rangeNo] [0xD952 - rangeMin] = 0x6394;
            mapDataStd [rangeNo] [0xD953 - rangeMin] = 0x63B1;
            mapDataStd [rangeNo] [0xD954 - rangeMin] = 0x63B0;
            mapDataStd [rangeNo] [0xD955 - rangeMin] = 0x63CE;
            mapDataStd [rangeNo] [0xD956 - rangeMin] = 0x63E5;
            mapDataStd [rangeNo] [0xD957 - rangeMin] = 0x63E8;
            mapDataStd [rangeNo] [0xD958 - rangeMin] = 0x63EF;
            mapDataStd [rangeNo] [0xD959 - rangeMin] = 0x63C3;
            mapDataStd [rangeNo] [0xD95A - rangeMin] = 0x649D;
            mapDataStd [rangeNo] [0xD95B - rangeMin] = 0x63F3;
            mapDataStd [rangeNo] [0xD95C - rangeMin] = 0x63CA;
            mapDataStd [rangeNo] [0xD95D - rangeMin] = 0x63E0;
            mapDataStd [rangeNo] [0xD95E - rangeMin] = 0x63F6;
            mapDataStd [rangeNo] [0xD95F - rangeMin] = 0x63D5;

            mapDataStd [rangeNo] [0xD960 - rangeMin] = 0x63F2;
            mapDataStd [rangeNo] [0xD961 - rangeMin] = 0x63F5;
            mapDataStd [rangeNo] [0xD962 - rangeMin] = 0x6461;
            mapDataStd [rangeNo] [0xD963 - rangeMin] = 0x63DF;
            mapDataStd [rangeNo] [0xD964 - rangeMin] = 0x63BE;
            mapDataStd [rangeNo] [0xD965 - rangeMin] = 0x63DD;
            mapDataStd [rangeNo] [0xD966 - rangeMin] = 0x63DC;
            mapDataStd [rangeNo] [0xD967 - rangeMin] = 0x63C4;
            mapDataStd [rangeNo] [0xD968 - rangeMin] = 0x63D8;
            mapDataStd [rangeNo] [0xD969 - rangeMin] = 0x63D3;
            mapDataStd [rangeNo] [0xD96A - rangeMin] = 0x63C2;
            mapDataStd [rangeNo] [0xD96B - rangeMin] = 0x63C7;
            mapDataStd [rangeNo] [0xD96C - rangeMin] = 0x63CC;
            mapDataStd [rangeNo] [0xD96D - rangeMin] = 0x63CB;
            mapDataStd [rangeNo] [0xD96E - rangeMin] = 0x63C8;
            mapDataStd [rangeNo] [0xD96F - rangeMin] = 0x63F0;

            mapDataStd [rangeNo] [0xD970 - rangeMin] = 0x63D7;
            mapDataStd [rangeNo] [0xD971 - rangeMin] = 0x63D9;
            mapDataStd [rangeNo] [0xD972 - rangeMin] = 0x6532;
            mapDataStd [rangeNo] [0xD973 - rangeMin] = 0x6567;
            mapDataStd [rangeNo] [0xD974 - rangeMin] = 0x656A;
            mapDataStd [rangeNo] [0xD975 - rangeMin] = 0x6564;
            mapDataStd [rangeNo] [0xD976 - rangeMin] = 0x655C;
            mapDataStd [rangeNo] [0xD977 - rangeMin] = 0x6568;
            mapDataStd [rangeNo] [0xD978 - rangeMin] = 0x6565;
            mapDataStd [rangeNo] [0xD979 - rangeMin] = 0x658C;
            mapDataStd [rangeNo] [0xD97A - rangeMin] = 0x659D;
            mapDataStd [rangeNo] [0xD97B - rangeMin] = 0x659E;
            mapDataStd [rangeNo] [0xD97C - rangeMin] = 0x65AE;
            mapDataStd [rangeNo] [0xD97D - rangeMin] = 0x65D0;
            mapDataStd [rangeNo] [0xD97E - rangeMin] = 0x65D2;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xD9A1 - rangeMin] = 0x667C;
            mapDataStd [rangeNo] [0xD9A2 - rangeMin] = 0x666C;
            mapDataStd [rangeNo] [0xD9A3 - rangeMin] = 0x667B;
            mapDataStd [rangeNo] [0xD9A4 - rangeMin] = 0x6680;
            mapDataStd [rangeNo] [0xD9A5 - rangeMin] = 0x6671;
            mapDataStd [rangeNo] [0xD9A6 - rangeMin] = 0x6679;
            mapDataStd [rangeNo] [0xD9A7 - rangeMin] = 0x666A;
            mapDataStd [rangeNo] [0xD9A8 - rangeMin] = 0x6672;
            mapDataStd [rangeNo] [0xD9A9 - rangeMin] = 0x6701;
            mapDataStd [rangeNo] [0xD9AA - rangeMin] = 0x690C;
            mapDataStd [rangeNo] [0xD9AB - rangeMin] = 0x68D3;
            mapDataStd [rangeNo] [0xD9AC - rangeMin] = 0x6904;
            mapDataStd [rangeNo] [0xD9AD - rangeMin] = 0x68DC;
            mapDataStd [rangeNo] [0xD9AE - rangeMin] = 0x692A;
            mapDataStd [rangeNo] [0xD9AF - rangeMin] = 0x68EC;

            mapDataStd [rangeNo] [0xD9B0 - rangeMin] = 0x68EA;
            mapDataStd [rangeNo] [0xD9B1 - rangeMin] = 0x68F1;
            mapDataStd [rangeNo] [0xD9B2 - rangeMin] = 0x690F;
            mapDataStd [rangeNo] [0xD9B3 - rangeMin] = 0x68D6;
            mapDataStd [rangeNo] [0xD9B4 - rangeMin] = 0x68F7;
            mapDataStd [rangeNo] [0xD9B5 - rangeMin] = 0x68EB;
            mapDataStd [rangeNo] [0xD9B6 - rangeMin] = 0x68E4;
            mapDataStd [rangeNo] [0xD9B7 - rangeMin] = 0x68F6;
            mapDataStd [rangeNo] [0xD9B8 - rangeMin] = 0x6913;
            mapDataStd [rangeNo] [0xD9B9 - rangeMin] = 0x6910;
            mapDataStd [rangeNo] [0xD9BA - rangeMin] = 0x68F3;
            mapDataStd [rangeNo] [0xD9BB - rangeMin] = 0x68E1;
            mapDataStd [rangeNo] [0xD9BC - rangeMin] = 0x6907;
            mapDataStd [rangeNo] [0xD9BD - rangeMin] = 0x68CC;
            mapDataStd [rangeNo] [0xD9BE - rangeMin] = 0x6908;
            mapDataStd [rangeNo] [0xD9BF - rangeMin] = 0x6970;

            mapDataStd [rangeNo] [0xD9C0 - rangeMin] = 0x68B4;
            mapDataStd [rangeNo] [0xD9C1 - rangeMin] = 0x6911;
            mapDataStd [rangeNo] [0xD9C2 - rangeMin] = 0x68EF;
            mapDataStd [rangeNo] [0xD9C3 - rangeMin] = 0x68C6;
            mapDataStd [rangeNo] [0xD9C4 - rangeMin] = 0x6914;
            mapDataStd [rangeNo] [0xD9C5 - rangeMin] = 0x68F8;
            mapDataStd [rangeNo] [0xD9C6 - rangeMin] = 0x68D0;
            mapDataStd [rangeNo] [0xD9C7 - rangeMin] = 0x68FD;
            mapDataStd [rangeNo] [0xD9C8 - rangeMin] = 0x68FC;
            mapDataStd [rangeNo] [0xD9C9 - rangeMin] = 0x68E8;
            mapDataStd [rangeNo] [0xD9CA - rangeMin] = 0x690B;
            mapDataStd [rangeNo] [0xD9CB - rangeMin] = 0x690A;
            mapDataStd [rangeNo] [0xD9CC - rangeMin] = 0x6917;
            mapDataStd [rangeNo] [0xD9CD - rangeMin] = 0x68CE;
            mapDataStd [rangeNo] [0xD9CE - rangeMin] = 0x68C8;
            mapDataStd [rangeNo] [0xD9CF - rangeMin] = 0x68DD;

            mapDataStd [rangeNo] [0xD9D0 - rangeMin] = 0x68DE;
            mapDataStd [rangeNo] [0xD9D1 - rangeMin] = 0x68E6;
            mapDataStd [rangeNo] [0xD9D2 - rangeMin] = 0x68F4;
            mapDataStd [rangeNo] [0xD9D3 - rangeMin] = 0x68D1;
            mapDataStd [rangeNo] [0xD9D4 - rangeMin] = 0x6906;
            mapDataStd [rangeNo] [0xD9D5 - rangeMin] = 0x68D4;
            mapDataStd [rangeNo] [0xD9D6 - rangeMin] = 0x68E9;
            mapDataStd [rangeNo] [0xD9D7 - rangeMin] = 0x6915;
            mapDataStd [rangeNo] [0xD9D8 - rangeMin] = 0x6925;
            mapDataStd [rangeNo] [0xD9D9 - rangeMin] = 0x68C7;
            mapDataStd [rangeNo] [0xD9DA - rangeMin] = 0x6B39;
            mapDataStd [rangeNo] [0xD9DB - rangeMin] = 0x6B3B;
            mapDataStd [rangeNo] [0xD9DC - rangeMin] = 0x6B3F;
            mapDataStd [rangeNo] [0xD9DD - rangeMin] = 0x6B3C;
            mapDataStd [rangeNo] [0xD9DE - rangeMin] = 0x6B94;
            mapDataStd [rangeNo] [0xD9DF - rangeMin] = 0x6B97;

            mapDataStd [rangeNo] [0xD9E0 - rangeMin] = 0x6B99;
            mapDataStd [rangeNo] [0xD9E1 - rangeMin] = 0x6B95;
            mapDataStd [rangeNo] [0xD9E2 - rangeMin] = 0x6BBD;
            mapDataStd [rangeNo] [0xD9E3 - rangeMin] = 0x6BF0;
            mapDataStd [rangeNo] [0xD9E4 - rangeMin] = 0x6BF2;
            mapDataStd [rangeNo] [0xD9E5 - rangeMin] = 0x6BF3;
            mapDataStd [rangeNo] [0xD9E6 - rangeMin] = 0x6C30;
            mapDataStd [rangeNo] [0xD9E7 - rangeMin] = 0x6DFC;
            mapDataStd [rangeNo] [0xD9E8 - rangeMin] = 0x6E46;
            mapDataStd [rangeNo] [0xD9E9 - rangeMin] = 0x6E47;
            mapDataStd [rangeNo] [0xD9EA - rangeMin] = 0x6E1F;
            mapDataStd [rangeNo] [0xD9EB - rangeMin] = 0x6E49;
            mapDataStd [rangeNo] [0xD9EC - rangeMin] = 0x6E88;
            mapDataStd [rangeNo] [0xD9ED - rangeMin] = 0x6E3C;
            mapDataStd [rangeNo] [0xD9EE - rangeMin] = 0x6E3D;
            mapDataStd [rangeNo] [0xD9EF - rangeMin] = 0x6E45;

            mapDataStd [rangeNo] [0xD9F0 - rangeMin] = 0x6E62;
            mapDataStd [rangeNo] [0xD9F1 - rangeMin] = 0x6E2B;
            mapDataStd [rangeNo] [0xD9F2 - rangeMin] = 0x6E3F;
            mapDataStd [rangeNo] [0xD9F3 - rangeMin] = 0x6E41;
            mapDataStd [rangeNo] [0xD9F4 - rangeMin] = 0x6E5D;
            mapDataStd [rangeNo] [0xD9F5 - rangeMin] = 0x6E73;
            mapDataStd [rangeNo] [0xD9F6 - rangeMin] = 0x6E1C;
            mapDataStd [rangeNo] [0xD9F7 - rangeMin] = 0x6E33;
            mapDataStd [rangeNo] [0xD9F8 - rangeMin] = 0x6E4B;
            mapDataStd [rangeNo] [0xD9F9 - rangeMin] = 0x6E40;
            mapDataStd [rangeNo] [0xD9FA - rangeMin] = 0x6E51;
            mapDataStd [rangeNo] [0xD9FB - rangeMin] = 0x6E3B;
            mapDataStd [rangeNo] [0xD9FC - rangeMin] = 0x6E03;
            mapDataStd [rangeNo] [0xD9FD - rangeMin] = 0x6E2E;
            mapDataStd [rangeNo] [0xD9FE - rangeMin] = 0x6E5E;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xDA40 - rangeMin] = 0x6E68;
            mapDataStd [rangeNo] [0xDA41 - rangeMin] = 0x6E5C;
            mapDataStd [rangeNo] [0xDA42 - rangeMin] = 0x6E61;
            mapDataStd [rangeNo] [0xDA43 - rangeMin] = 0x6E31;
            mapDataStd [rangeNo] [0xDA44 - rangeMin] = 0x6E28;
            mapDataStd [rangeNo] [0xDA45 - rangeMin] = 0x6E60;
            mapDataStd [rangeNo] [0xDA46 - rangeMin] = 0x6E71;
            mapDataStd [rangeNo] [0xDA47 - rangeMin] = 0x6E6B;
            mapDataStd [rangeNo] [0xDA48 - rangeMin] = 0x6E39;
            mapDataStd [rangeNo] [0xDA49 - rangeMin] = 0x6E22;
            mapDataStd [rangeNo] [0xDA4A - rangeMin] = 0x6E30;
            mapDataStd [rangeNo] [0xDA4B - rangeMin] = 0x6E53;
            mapDataStd [rangeNo] [0xDA4C - rangeMin] = 0x6E65;
            mapDataStd [rangeNo] [0xDA4D - rangeMin] = 0x6E27;
            mapDataStd [rangeNo] [0xDA4E - rangeMin] = 0x6E78;
            mapDataStd [rangeNo] [0xDA4F - rangeMin] = 0x6E64;

            mapDataStd [rangeNo] [0xDA50 - rangeMin] = 0x6E77;
            mapDataStd [rangeNo] [0xDA51 - rangeMin] = 0x6E55;
            mapDataStd [rangeNo] [0xDA52 - rangeMin] = 0x6E79;
            mapDataStd [rangeNo] [0xDA53 - rangeMin] = 0x6E52;
            mapDataStd [rangeNo] [0xDA54 - rangeMin] = 0x6E66;
            mapDataStd [rangeNo] [0xDA55 - rangeMin] = 0x6E35;
            mapDataStd [rangeNo] [0xDA56 - rangeMin] = 0x6E36;
            mapDataStd [rangeNo] [0xDA57 - rangeMin] = 0x6E5A;
            mapDataStd [rangeNo] [0xDA58 - rangeMin] = 0x7120;
            mapDataStd [rangeNo] [0xDA59 - rangeMin] = 0x711E;
            mapDataStd [rangeNo] [0xDA5A - rangeMin] = 0x712F;
            mapDataStd [rangeNo] [0xDA5B - rangeMin] = 0x70FB;
            mapDataStd [rangeNo] [0xDA5C - rangeMin] = 0x712E;
            mapDataStd [rangeNo] [0xDA5D - rangeMin] = 0x7131;
            mapDataStd [rangeNo] [0xDA5E - rangeMin] = 0x7123;
            mapDataStd [rangeNo] [0xDA5F - rangeMin] = 0x7125;

            mapDataStd [rangeNo] [0xDA60 - rangeMin] = 0x7122;
            mapDataStd [rangeNo] [0xDA61 - rangeMin] = 0x7132;
            mapDataStd [rangeNo] [0xDA62 - rangeMin] = 0x711F;
            mapDataStd [rangeNo] [0xDA63 - rangeMin] = 0x7128;
            mapDataStd [rangeNo] [0xDA64 - rangeMin] = 0x713A;
            mapDataStd [rangeNo] [0xDA65 - rangeMin] = 0x711B;
            mapDataStd [rangeNo] [0xDA66 - rangeMin] = 0x724B;
            mapDataStd [rangeNo] [0xDA67 - rangeMin] = 0x725A;
            mapDataStd [rangeNo] [0xDA68 - rangeMin] = 0x7288;
            mapDataStd [rangeNo] [0xDA69 - rangeMin] = 0x7289;
            mapDataStd [rangeNo] [0xDA6A - rangeMin] = 0x7286;
            mapDataStd [rangeNo] [0xDA6B - rangeMin] = 0x7285;
            mapDataStd [rangeNo] [0xDA6C - rangeMin] = 0x728B;
            mapDataStd [rangeNo] [0xDA6D - rangeMin] = 0x7312;
            mapDataStd [rangeNo] [0xDA6E - rangeMin] = 0x730B;
            mapDataStd [rangeNo] [0xDA6F - rangeMin] = 0x7330;

            mapDataStd [rangeNo] [0xDA70 - rangeMin] = 0x7322;
            mapDataStd [rangeNo] [0xDA71 - rangeMin] = 0x7331;
            mapDataStd [rangeNo] [0xDA72 - rangeMin] = 0x7333;
            mapDataStd [rangeNo] [0xDA73 - rangeMin] = 0x7327;
            mapDataStd [rangeNo] [0xDA74 - rangeMin] = 0x7332;
            mapDataStd [rangeNo] [0xDA75 - rangeMin] = 0x732D;
            mapDataStd [rangeNo] [0xDA76 - rangeMin] = 0x7326;
            mapDataStd [rangeNo] [0xDA77 - rangeMin] = 0x7323;
            mapDataStd [rangeNo] [0xDA78 - rangeMin] = 0x7335;
            mapDataStd [rangeNo] [0xDA79 - rangeMin] = 0x730C;
            mapDataStd [rangeNo] [0xDA7A - rangeMin] = 0x742E;
            mapDataStd [rangeNo] [0xDA7B - rangeMin] = 0x742C;
            mapDataStd [rangeNo] [0xDA7C - rangeMin] = 0x7430;
            mapDataStd [rangeNo] [0xDA7D - rangeMin] = 0x742B;
            mapDataStd [rangeNo] [0xDA7E - rangeMin] = 0x7416;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xDAA1 - rangeMin] = 0x741A;
            mapDataStd [rangeNo] [0xDAA2 - rangeMin] = 0x7421;
            mapDataStd [rangeNo] [0xDAA3 - rangeMin] = 0x742D;
            mapDataStd [rangeNo] [0xDAA4 - rangeMin] = 0x7431;
            mapDataStd [rangeNo] [0xDAA5 - rangeMin] = 0x7424;
            mapDataStd [rangeNo] [0xDAA6 - rangeMin] = 0x7423;
            mapDataStd [rangeNo] [0xDAA7 - rangeMin] = 0x741D;
            mapDataStd [rangeNo] [0xDAA8 - rangeMin] = 0x7429;
            mapDataStd [rangeNo] [0xDAA9 - rangeMin] = 0x7420;
            mapDataStd [rangeNo] [0xDAAA - rangeMin] = 0x7432;
            mapDataStd [rangeNo] [0xDAAB - rangeMin] = 0x74FB;
            mapDataStd [rangeNo] [0xDAAC - rangeMin] = 0x752F;
            mapDataStd [rangeNo] [0xDAAD - rangeMin] = 0x756F;
            mapDataStd [rangeNo] [0xDAAE - rangeMin] = 0x756C;
            mapDataStd [rangeNo] [0xDAAF - rangeMin] = 0x75E7;

            mapDataStd [rangeNo] [0xDAB0 - rangeMin] = 0x75DA;
            mapDataStd [rangeNo] [0xDAB1 - rangeMin] = 0x75E1;
            mapDataStd [rangeNo] [0xDAB2 - rangeMin] = 0x75E6;
            mapDataStd [rangeNo] [0xDAB3 - rangeMin] = 0x75DD;
            mapDataStd [rangeNo] [0xDAB4 - rangeMin] = 0x75DF;
            mapDataStd [rangeNo] [0xDAB5 - rangeMin] = 0x75E4;
            mapDataStd [rangeNo] [0xDAB6 - rangeMin] = 0x75D7;
            mapDataStd [rangeNo] [0xDAB7 - rangeMin] = 0x7695;
            mapDataStd [rangeNo] [0xDAB8 - rangeMin] = 0x7692;
            mapDataStd [rangeNo] [0xDAB9 - rangeMin] = 0x76DA;
            mapDataStd [rangeNo] [0xDABA - rangeMin] = 0x7746;
            mapDataStd [rangeNo] [0xDABB - rangeMin] = 0x7747;
            mapDataStd [rangeNo] [0xDABC - rangeMin] = 0x7744;
            mapDataStd [rangeNo] [0xDABD - rangeMin] = 0x774D;
            mapDataStd [rangeNo] [0xDABE - rangeMin] = 0x7745;
            mapDataStd [rangeNo] [0xDABF - rangeMin] = 0x774A;

            mapDataStd [rangeNo] [0xDAC0 - rangeMin] = 0x774E;
            mapDataStd [rangeNo] [0xDAC1 - rangeMin] = 0x774B;
            mapDataStd [rangeNo] [0xDAC2 - rangeMin] = 0x774C;
            mapDataStd [rangeNo] [0xDAC3 - rangeMin] = 0x77DE;
            mapDataStd [rangeNo] [0xDAC4 - rangeMin] = 0x77EC;
            mapDataStd [rangeNo] [0xDAC5 - rangeMin] = 0x7860;
            mapDataStd [rangeNo] [0xDAC6 - rangeMin] = 0x7864;
            mapDataStd [rangeNo] [0xDAC7 - rangeMin] = 0x7865;
            mapDataStd [rangeNo] [0xDAC8 - rangeMin] = 0x785C;
            mapDataStd [rangeNo] [0xDAC9 - rangeMin] = 0x786D;
            mapDataStd [rangeNo] [0xDACA - rangeMin] = 0x7871;
            mapDataStd [rangeNo] [0xDACB - rangeMin] = 0x786A;
            mapDataStd [rangeNo] [0xDACC - rangeMin] = 0x786E;
            mapDataStd [rangeNo] [0xDACD - rangeMin] = 0x7870;
            mapDataStd [rangeNo] [0xDACE - rangeMin] = 0x7869;
            mapDataStd [rangeNo] [0xDACF - rangeMin] = 0x7868;

            mapDataStd [rangeNo] [0xDAD0 - rangeMin] = 0x785E;
            mapDataStd [rangeNo] [0xDAD1 - rangeMin] = 0x7862;
            mapDataStd [rangeNo] [0xDAD2 - rangeMin] = 0x7974;
            mapDataStd [rangeNo] [0xDAD3 - rangeMin] = 0x7973;
            mapDataStd [rangeNo] [0xDAD4 - rangeMin] = 0x7972;
            mapDataStd [rangeNo] [0xDAD5 - rangeMin] = 0x7970;
            mapDataStd [rangeNo] [0xDAD6 - rangeMin] = 0x7A02;
            mapDataStd [rangeNo] [0xDAD7 - rangeMin] = 0x7A0A;
            mapDataStd [rangeNo] [0xDAD8 - rangeMin] = 0x7A03;
            mapDataStd [rangeNo] [0xDAD9 - rangeMin] = 0x7A0C;
            mapDataStd [rangeNo] [0xDADA - rangeMin] = 0x7A04;
            mapDataStd [rangeNo] [0xDADB - rangeMin] = 0x7A99;
            mapDataStd [rangeNo] [0xDADC - rangeMin] = 0x7AE6;
            mapDataStd [rangeNo] [0xDADD - rangeMin] = 0x7AE4;
            mapDataStd [rangeNo] [0xDADE - rangeMin] = 0x7B4A;
            mapDataStd [rangeNo] [0xDADF - rangeMin] = 0x7B3B;

            mapDataStd [rangeNo] [0xDAE0 - rangeMin] = 0x7B44;
            mapDataStd [rangeNo] [0xDAE1 - rangeMin] = 0x7B48;
            mapDataStd [rangeNo] [0xDAE2 - rangeMin] = 0x7B4C;
            mapDataStd [rangeNo] [0xDAE3 - rangeMin] = 0x7B4E;
            mapDataStd [rangeNo] [0xDAE4 - rangeMin] = 0x7B40;
            mapDataStd [rangeNo] [0xDAE5 - rangeMin] = 0x7B58;
            mapDataStd [rangeNo] [0xDAE6 - rangeMin] = 0x7B45;
            mapDataStd [rangeNo] [0xDAE7 - rangeMin] = 0x7CA2;
            mapDataStd [rangeNo] [0xDAE8 - rangeMin] = 0x7C9E;
            mapDataStd [rangeNo] [0xDAE9 - rangeMin] = 0x7CA8;
            mapDataStd [rangeNo] [0xDAEA - rangeMin] = 0x7CA1;
            mapDataStd [rangeNo] [0xDAEB - rangeMin] = 0x7D58;
            mapDataStd [rangeNo] [0xDAEC - rangeMin] = 0x7D6F;
            mapDataStd [rangeNo] [0xDAED - rangeMin] = 0x7D63;
            mapDataStd [rangeNo] [0xDAEE - rangeMin] = 0x7D53;
            mapDataStd [rangeNo] [0xDAEF - rangeMin] = 0x7D56;

            mapDataStd [rangeNo] [0xDAF0 - rangeMin] = 0x7D67;
            mapDataStd [rangeNo] [0xDAF1 - rangeMin] = 0x7D6A;
            mapDataStd [rangeNo] [0xDAF2 - rangeMin] = 0x7D4F;
            mapDataStd [rangeNo] [0xDAF3 - rangeMin] = 0x7D6D;
            mapDataStd [rangeNo] [0xDAF4 - rangeMin] = 0x7D5C;
            mapDataStd [rangeNo] [0xDAF5 - rangeMin] = 0x7D6B;
            mapDataStd [rangeNo] [0xDAF6 - rangeMin] = 0x7D52;
            mapDataStd [rangeNo] [0xDAF7 - rangeMin] = 0x7D54;
            mapDataStd [rangeNo] [0xDAF8 - rangeMin] = 0x7D69;
            mapDataStd [rangeNo] [0xDAF9 - rangeMin] = 0x7D51;
            mapDataStd [rangeNo] [0xDAFA - rangeMin] = 0x7D5F;
            mapDataStd [rangeNo] [0xDAFB - rangeMin] = 0x7D4E;
            mapDataStd [rangeNo] [0xDAFC - rangeMin] = 0x7F3E;
            mapDataStd [rangeNo] [0xDAFD - rangeMin] = 0x7F3F;
            mapDataStd [rangeNo] [0xDAFE - rangeMin] = 0x7F65;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xDB40 - rangeMin] = 0x7F66;
            mapDataStd [rangeNo] [0xDB41 - rangeMin] = 0x7FA2;
            mapDataStd [rangeNo] [0xDB42 - rangeMin] = 0x7FA0;
            mapDataStd [rangeNo] [0xDB43 - rangeMin] = 0x7FA1;
            mapDataStd [rangeNo] [0xDB44 - rangeMin] = 0x7FD7;
            mapDataStd [rangeNo] [0xDB45 - rangeMin] = 0x8051;
            mapDataStd [rangeNo] [0xDB46 - rangeMin] = 0x804F;
            mapDataStd [rangeNo] [0xDB47 - rangeMin] = 0x8050;
            mapDataStd [rangeNo] [0xDB48 - rangeMin] = 0x80FE;
            mapDataStd [rangeNo] [0xDB49 - rangeMin] = 0x80D4;
            mapDataStd [rangeNo] [0xDB4A - rangeMin] = 0x8143;
            mapDataStd [rangeNo] [0xDB4B - rangeMin] = 0x814A;
            mapDataStd [rangeNo] [0xDB4C - rangeMin] = 0x8152;
            mapDataStd [rangeNo] [0xDB4D - rangeMin] = 0x814F;
            mapDataStd [rangeNo] [0xDB4E - rangeMin] = 0x8147;
            mapDataStd [rangeNo] [0xDB4F - rangeMin] = 0x813D;

            mapDataStd [rangeNo] [0xDB50 - rangeMin] = 0x814D;
            mapDataStd [rangeNo] [0xDB51 - rangeMin] = 0x813A;
            mapDataStd [rangeNo] [0xDB52 - rangeMin] = 0x81E6;
            mapDataStd [rangeNo] [0xDB53 - rangeMin] = 0x81EE;
            mapDataStd [rangeNo] [0xDB54 - rangeMin] = 0x81F7;
            mapDataStd [rangeNo] [0xDB55 - rangeMin] = 0x81F8;
            mapDataStd [rangeNo] [0xDB56 - rangeMin] = 0x81F9;
            mapDataStd [rangeNo] [0xDB57 - rangeMin] = 0x8204;
            mapDataStd [rangeNo] [0xDB58 - rangeMin] = 0x823C;
            mapDataStd [rangeNo] [0xDB59 - rangeMin] = 0x823D;
            mapDataStd [rangeNo] [0xDB5A - rangeMin] = 0x823F;
            mapDataStd [rangeNo] [0xDB5B - rangeMin] = 0x8275;
            mapDataStd [rangeNo] [0xDB5C - rangeMin] = 0x833B;
            mapDataStd [rangeNo] [0xDB5D - rangeMin] = 0x83CF;
            mapDataStd [rangeNo] [0xDB5E - rangeMin] = 0x83F9;
            mapDataStd [rangeNo] [0xDB5F - rangeMin] = 0x8423;

            mapDataStd [rangeNo] [0xDB60 - rangeMin] = 0x83C0;
            mapDataStd [rangeNo] [0xDB61 - rangeMin] = 0x83E8;
            mapDataStd [rangeNo] [0xDB62 - rangeMin] = 0x8412;
            mapDataStd [rangeNo] [0xDB63 - rangeMin] = 0x83E7;
            mapDataStd [rangeNo] [0xDB64 - rangeMin] = 0x83E4;
            mapDataStd [rangeNo] [0xDB65 - rangeMin] = 0x83FC;
            mapDataStd [rangeNo] [0xDB66 - rangeMin] = 0x83F6;
            mapDataStd [rangeNo] [0xDB67 - rangeMin] = 0x8410;
            mapDataStd [rangeNo] [0xDB68 - rangeMin] = 0x83C6;
            mapDataStd [rangeNo] [0xDB69 - rangeMin] = 0x83C8;
            mapDataStd [rangeNo] [0xDB6A - rangeMin] = 0x83EB;
            mapDataStd [rangeNo] [0xDB6B - rangeMin] = 0x83E3;
            mapDataStd [rangeNo] [0xDB6C - rangeMin] = 0x83BF;
            mapDataStd [rangeNo] [0xDB6D - rangeMin] = 0x8401;
            mapDataStd [rangeNo] [0xDB6E - rangeMin] = 0x83DD;
            mapDataStd [rangeNo] [0xDB6F - rangeMin] = 0x83E5;

            mapDataStd [rangeNo] [0xDB70 - rangeMin] = 0x83D8;
            mapDataStd [rangeNo] [0xDB71 - rangeMin] = 0x83FF;
            mapDataStd [rangeNo] [0xDB72 - rangeMin] = 0x83E1;
            mapDataStd [rangeNo] [0xDB73 - rangeMin] = 0x83CB;
            mapDataStd [rangeNo] [0xDB74 - rangeMin] = 0x83CE;
            mapDataStd [rangeNo] [0xDB75 - rangeMin] = 0x83D6;
            mapDataStd [rangeNo] [0xDB76 - rangeMin] = 0x83F5;
            mapDataStd [rangeNo] [0xDB77 - rangeMin] = 0x83C9;
            mapDataStd [rangeNo] [0xDB78 - rangeMin] = 0x8409;
            mapDataStd [rangeNo] [0xDB79 - rangeMin] = 0x840F;
            mapDataStd [rangeNo] [0xDB7A - rangeMin] = 0x83DE;
            mapDataStd [rangeNo] [0xDB7B - rangeMin] = 0x8411;
            mapDataStd [rangeNo] [0xDB7C - rangeMin] = 0x8406;
            mapDataStd [rangeNo] [0xDB7D - rangeMin] = 0x83C2;
            mapDataStd [rangeNo] [0xDB7E - rangeMin] = 0x83F3;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xDBA1 - rangeMin] = 0x83D5;
            mapDataStd [rangeNo] [0xDBA2 - rangeMin] = 0x83FA;
            mapDataStd [rangeNo] [0xDBA3 - rangeMin] = 0x83C7;
            mapDataStd [rangeNo] [0xDBA4 - rangeMin] = 0x83D1;
            mapDataStd [rangeNo] [0xDBA5 - rangeMin] = 0x83EA;
            mapDataStd [rangeNo] [0xDBA6 - rangeMin] = 0x8413;
            mapDataStd [rangeNo] [0xDBA7 - rangeMin] = 0x83C3;
            mapDataStd [rangeNo] [0xDBA8 - rangeMin] = 0x83EC;
            mapDataStd [rangeNo] [0xDBA9 - rangeMin] = 0x83EE;
            mapDataStd [rangeNo] [0xDBAA - rangeMin] = 0x83C4;
            mapDataStd [rangeNo] [0xDBAB - rangeMin] = 0x83FB;
            mapDataStd [rangeNo] [0xDBAC - rangeMin] = 0x83D7;
            mapDataStd [rangeNo] [0xDBAD - rangeMin] = 0x83E2;
            mapDataStd [rangeNo] [0xDBAE - rangeMin] = 0x841B;
            mapDataStd [rangeNo] [0xDBAF - rangeMin] = 0x83DB;

            mapDataStd [rangeNo] [0xDBB0 - rangeMin] = 0x83FE;
            mapDataStd [rangeNo] [0xDBB1 - rangeMin] = 0x86D8;
            mapDataStd [rangeNo] [0xDBB2 - rangeMin] = 0x86E2;
            mapDataStd [rangeNo] [0xDBB3 - rangeMin] = 0x86E6;
            mapDataStd [rangeNo] [0xDBB4 - rangeMin] = 0x86D3;
            mapDataStd [rangeNo] [0xDBB5 - rangeMin] = 0x86E3;
            mapDataStd [rangeNo] [0xDBB6 - rangeMin] = 0x86DA;
            mapDataStd [rangeNo] [0xDBB7 - rangeMin] = 0x86EA;
            mapDataStd [rangeNo] [0xDBB8 - rangeMin] = 0x86DD;
            mapDataStd [rangeNo] [0xDBB9 - rangeMin] = 0x86EB;
            mapDataStd [rangeNo] [0xDBBA - rangeMin] = 0x86DC;
            mapDataStd [rangeNo] [0xDBBB - rangeMin] = 0x86EC;
            mapDataStd [rangeNo] [0xDBBC - rangeMin] = 0x86E9;
            mapDataStd [rangeNo] [0xDBBD - rangeMin] = 0x86D7;
            mapDataStd [rangeNo] [0xDBBE - rangeMin] = 0x86E8;
            mapDataStd [rangeNo] [0xDBBF - rangeMin] = 0x86D1;

            mapDataStd [rangeNo] [0xDBC0 - rangeMin] = 0x8848;
            mapDataStd [rangeNo] [0xDBC1 - rangeMin] = 0x8856;
            mapDataStd [rangeNo] [0xDBC2 - rangeMin] = 0x8855;
            mapDataStd [rangeNo] [0xDBC3 - rangeMin] = 0x88BA;
            mapDataStd [rangeNo] [0xDBC4 - rangeMin] = 0x88D7;
            mapDataStd [rangeNo] [0xDBC5 - rangeMin] = 0x88B9;
            mapDataStd [rangeNo] [0xDBC6 - rangeMin] = 0x88B8;
            mapDataStd [rangeNo] [0xDBC7 - rangeMin] = 0x88C0;
            mapDataStd [rangeNo] [0xDBC8 - rangeMin] = 0x88BE;
            mapDataStd [rangeNo] [0xDBC9 - rangeMin] = 0x88B6;
            mapDataStd [rangeNo] [0xDBCA - rangeMin] = 0x88BC;
            mapDataStd [rangeNo] [0xDBCB - rangeMin] = 0x88B7;
            mapDataStd [rangeNo] [0xDBCC - rangeMin] = 0x88BD;
            mapDataStd [rangeNo] [0xDBCD - rangeMin] = 0x88B2;
            mapDataStd [rangeNo] [0xDBCE - rangeMin] = 0x8901;
            mapDataStd [rangeNo] [0xDBCF - rangeMin] = 0x88C9;

            mapDataStd [rangeNo] [0xDBD0 - rangeMin] = 0x8995;
            mapDataStd [rangeNo] [0xDBD1 - rangeMin] = 0x8998;
            mapDataStd [rangeNo] [0xDBD2 - rangeMin] = 0x8997;
            mapDataStd [rangeNo] [0xDBD3 - rangeMin] = 0x89DD;
            mapDataStd [rangeNo] [0xDBD4 - rangeMin] = 0x89DA;
            mapDataStd [rangeNo] [0xDBD5 - rangeMin] = 0x89DB;
            mapDataStd [rangeNo] [0xDBD6 - rangeMin] = 0x8A4E;
            mapDataStd [rangeNo] [0xDBD7 - rangeMin] = 0x8A4D;
            mapDataStd [rangeNo] [0xDBD8 - rangeMin] = 0x8A39;
            mapDataStd [rangeNo] [0xDBD9 - rangeMin] = 0x8A59;
            mapDataStd [rangeNo] [0xDBDA - rangeMin] = 0x8A40;
            mapDataStd [rangeNo] [0xDBDB - rangeMin] = 0x8A57;
            mapDataStd [rangeNo] [0xDBDC - rangeMin] = 0x8A58;
            mapDataStd [rangeNo] [0xDBDD - rangeMin] = 0x8A44;
            mapDataStd [rangeNo] [0xDBDE - rangeMin] = 0x8A45;
            mapDataStd [rangeNo] [0xDBDF - rangeMin] = 0x8A52;

            mapDataStd [rangeNo] [0xDBE0 - rangeMin] = 0x8A48;
            mapDataStd [rangeNo] [0xDBE1 - rangeMin] = 0x8A51;
            mapDataStd [rangeNo] [0xDBE2 - rangeMin] = 0x8A4A;
            mapDataStd [rangeNo] [0xDBE3 - rangeMin] = 0x8A4C;
            mapDataStd [rangeNo] [0xDBE4 - rangeMin] = 0x8A4F;
            mapDataStd [rangeNo] [0xDBE5 - rangeMin] = 0x8C5F;
            mapDataStd [rangeNo] [0xDBE6 - rangeMin] = 0x8C81;
            mapDataStd [rangeNo] [0xDBE7 - rangeMin] = 0x8C80;
            mapDataStd [rangeNo] [0xDBE8 - rangeMin] = 0x8CBA;
            mapDataStd [rangeNo] [0xDBE9 - rangeMin] = 0x8CBE;
            mapDataStd [rangeNo] [0xDBEA - rangeMin] = 0x8CB0;
            mapDataStd [rangeNo] [0xDBEB - rangeMin] = 0x8CB9;
            mapDataStd [rangeNo] [0xDBEC - rangeMin] = 0x8CB5;
            mapDataStd [rangeNo] [0xDBED - rangeMin] = 0x8D84;
            mapDataStd [rangeNo] [0xDBEE - rangeMin] = 0x8D80;
            mapDataStd [rangeNo] [0xDBEF - rangeMin] = 0x8D89;

            mapDataStd [rangeNo] [0xDBF0 - rangeMin] = 0x8DD8;
            mapDataStd [rangeNo] [0xDBF1 - rangeMin] = 0x8DD3;
            mapDataStd [rangeNo] [0xDBF2 - rangeMin] = 0x8DCD;
            mapDataStd [rangeNo] [0xDBF3 - rangeMin] = 0x8DC7;
            mapDataStd [rangeNo] [0xDBF4 - rangeMin] = 0x8DD6;
            mapDataStd [rangeNo] [0xDBF5 - rangeMin] = 0x8DDC;
            mapDataStd [rangeNo] [0xDBF6 - rangeMin] = 0x8DCF;
            mapDataStd [rangeNo] [0xDBF7 - rangeMin] = 0x8DD5;
            mapDataStd [rangeNo] [0xDBF8 - rangeMin] = 0x8DD9;
            mapDataStd [rangeNo] [0xDBF9 - rangeMin] = 0x8DC8;
            mapDataStd [rangeNo] [0xDBFA - rangeMin] = 0x8DD7;
            mapDataStd [rangeNo] [0xDBFB - rangeMin] = 0x8DC5;
            mapDataStd [rangeNo] [0xDBFC - rangeMin] = 0x8EEF;
            mapDataStd [rangeNo] [0xDBFD - rangeMin] = 0x8EF7;
            mapDataStd [rangeNo] [0xDBFE - rangeMin] = 0x8EFA;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xDC40 - rangeMin] = 0x8EF9;
            mapDataStd [rangeNo] [0xDC41 - rangeMin] = 0x8EE6;
            mapDataStd [rangeNo] [0xDC42 - rangeMin] = 0x8EEE;
            mapDataStd [rangeNo] [0xDC43 - rangeMin] = 0x8EE5;
            mapDataStd [rangeNo] [0xDC44 - rangeMin] = 0x8EF5;
            mapDataStd [rangeNo] [0xDC45 - rangeMin] = 0x8EE7;
            mapDataStd [rangeNo] [0xDC46 - rangeMin] = 0x8EE8;
            mapDataStd [rangeNo] [0xDC47 - rangeMin] = 0x8EF6;
            mapDataStd [rangeNo] [0xDC48 - rangeMin] = 0x8EEB;
            mapDataStd [rangeNo] [0xDC49 - rangeMin] = 0x8EF1;
            mapDataStd [rangeNo] [0xDC4A - rangeMin] = 0x8EEC;
            mapDataStd [rangeNo] [0xDC4B - rangeMin] = 0x8EF4;
            mapDataStd [rangeNo] [0xDC4C - rangeMin] = 0x8EE9;
            mapDataStd [rangeNo] [0xDC4D - rangeMin] = 0x902D;
            mapDataStd [rangeNo] [0xDC4E - rangeMin] = 0x9034;
            mapDataStd [rangeNo] [0xDC4F - rangeMin] = 0x902F;

            mapDataStd [rangeNo] [0xDC50 - rangeMin] = 0x9106;
            mapDataStd [rangeNo] [0xDC51 - rangeMin] = 0x912C;
            mapDataStd [rangeNo] [0xDC52 - rangeMin] = 0x9104;
            mapDataStd [rangeNo] [0xDC53 - rangeMin] = 0x90FF;
            mapDataStd [rangeNo] [0xDC54 - rangeMin] = 0x90FC;
            mapDataStd [rangeNo] [0xDC55 - rangeMin] = 0x9108;
            mapDataStd [rangeNo] [0xDC56 - rangeMin] = 0x90F9;
            mapDataStd [rangeNo] [0xDC57 - rangeMin] = 0x90FB;
            mapDataStd [rangeNo] [0xDC58 - rangeMin] = 0x9101;
            mapDataStd [rangeNo] [0xDC59 - rangeMin] = 0x9100;
            mapDataStd [rangeNo] [0xDC5A - rangeMin] = 0x9107;
            mapDataStd [rangeNo] [0xDC5B - rangeMin] = 0x9105;
            mapDataStd [rangeNo] [0xDC5C - rangeMin] = 0x9103;
            mapDataStd [rangeNo] [0xDC5D - rangeMin] = 0x9161;
            mapDataStd [rangeNo] [0xDC5E - rangeMin] = 0x9164;
            mapDataStd [rangeNo] [0xDC5F - rangeMin] = 0x915F;

            mapDataStd [rangeNo] [0xDC60 - rangeMin] = 0x9162;
            mapDataStd [rangeNo] [0xDC61 - rangeMin] = 0x9160;
            mapDataStd [rangeNo] [0xDC62 - rangeMin] = 0x9201;
            mapDataStd [rangeNo] [0xDC63 - rangeMin] = 0x920A;
            mapDataStd [rangeNo] [0xDC64 - rangeMin] = 0x9225;
            mapDataStd [rangeNo] [0xDC65 - rangeMin] = 0x9203;
            mapDataStd [rangeNo] [0xDC66 - rangeMin] = 0x921A;
            mapDataStd [rangeNo] [0xDC67 - rangeMin] = 0x9226;
            mapDataStd [rangeNo] [0xDC68 - rangeMin] = 0x920F;
            mapDataStd [rangeNo] [0xDC69 - rangeMin] = 0x920C;
            mapDataStd [rangeNo] [0xDC6A - rangeMin] = 0x9200;
            mapDataStd [rangeNo] [0xDC6B - rangeMin] = 0x9212;
            mapDataStd [rangeNo] [0xDC6C - rangeMin] = 0x91FF;
            mapDataStd [rangeNo] [0xDC6D - rangeMin] = 0x91FD;
            mapDataStd [rangeNo] [0xDC6E - rangeMin] = 0x9206;
            mapDataStd [rangeNo] [0xDC6F - rangeMin] = 0x9204;

            mapDataStd [rangeNo] [0xDC70 - rangeMin] = 0x9227;
            mapDataStd [rangeNo] [0xDC71 - rangeMin] = 0x9202;
            mapDataStd [rangeNo] [0xDC72 - rangeMin] = 0x921C;
            mapDataStd [rangeNo] [0xDC73 - rangeMin] = 0x9224;
            mapDataStd [rangeNo] [0xDC74 - rangeMin] = 0x9219;
            mapDataStd [rangeNo] [0xDC75 - rangeMin] = 0x9217;
            mapDataStd [rangeNo] [0xDC76 - rangeMin] = 0x9205;
            mapDataStd [rangeNo] [0xDC77 - rangeMin] = 0x9216;
            mapDataStd [rangeNo] [0xDC78 - rangeMin] = 0x957B;
            mapDataStd [rangeNo] [0xDC79 - rangeMin] = 0x958D;
            mapDataStd [rangeNo] [0xDC7A - rangeMin] = 0x958C;
            mapDataStd [rangeNo] [0xDC7B - rangeMin] = 0x9590;
            mapDataStd [rangeNo] [0xDC7C - rangeMin] = 0x9687;
            mapDataStd [rangeNo] [0xDC7D - rangeMin] = 0x967E;
            mapDataStd [rangeNo] [0xDC7E - rangeMin] = 0x9688;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xDCA1 - rangeMin] = 0x9689;
            mapDataStd [rangeNo] [0xDCA2 - rangeMin] = 0x9683;
            mapDataStd [rangeNo] [0xDCA3 - rangeMin] = 0x9680;
            mapDataStd [rangeNo] [0xDCA4 - rangeMin] = 0x96C2;
            mapDataStd [rangeNo] [0xDCA5 - rangeMin] = 0x96C8;
            mapDataStd [rangeNo] [0xDCA6 - rangeMin] = 0x96C3;
            mapDataStd [rangeNo] [0xDCA7 - rangeMin] = 0x96F1;
            mapDataStd [rangeNo] [0xDCA8 - rangeMin] = 0x96F0;
            mapDataStd [rangeNo] [0xDCA9 - rangeMin] = 0x976C;
            mapDataStd [rangeNo] [0xDCAA - rangeMin] = 0x9770;
            mapDataStd [rangeNo] [0xDCAB - rangeMin] = 0x976E;
            mapDataStd [rangeNo] [0xDCAC - rangeMin] = 0x9807;
            mapDataStd [rangeNo] [0xDCAD - rangeMin] = 0x98A9;
            mapDataStd [rangeNo] [0xDCAE - rangeMin] = 0x98EB;
            mapDataStd [rangeNo] [0xDCAF - rangeMin] = 0x9CE6;

            mapDataStd [rangeNo] [0xDCB0 - rangeMin] = 0x9EF9;
            mapDataStd [rangeNo] [0xDCB1 - rangeMin] = 0x4E83;
            mapDataStd [rangeNo] [0xDCB2 - rangeMin] = 0x4E84;
            mapDataStd [rangeNo] [0xDCB3 - rangeMin] = 0x4EB6;
            mapDataStd [rangeNo] [0xDCB4 - rangeMin] = 0x50BD;
            mapDataStd [rangeNo] [0xDCB5 - rangeMin] = 0x50BF;
            mapDataStd [rangeNo] [0xDCB6 - rangeMin] = 0x50C6;
            mapDataStd [rangeNo] [0xDCB7 - rangeMin] = 0x50AE;
            mapDataStd [rangeNo] [0xDCB8 - rangeMin] = 0x50C4;
            mapDataStd [rangeNo] [0xDCB9 - rangeMin] = 0x50CA;
            mapDataStd [rangeNo] [0xDCBA - rangeMin] = 0x50B4;
            mapDataStd [rangeNo] [0xDCBB - rangeMin] = 0x50C8;
            mapDataStd [rangeNo] [0xDCBC - rangeMin] = 0x50C2;
            mapDataStd [rangeNo] [0xDCBD - rangeMin] = 0x50B0;
            mapDataStd [rangeNo] [0xDCBE - rangeMin] = 0x50C1;
            mapDataStd [rangeNo] [0xDCBF - rangeMin] = 0x50BA;

            mapDataStd [rangeNo] [0xDCC0 - rangeMin] = 0x50B1;
            mapDataStd [rangeNo] [0xDCC1 - rangeMin] = 0x50CB;
            mapDataStd [rangeNo] [0xDCC2 - rangeMin] = 0x50C9;
            mapDataStd [rangeNo] [0xDCC3 - rangeMin] = 0x50B6;
            mapDataStd [rangeNo] [0xDCC4 - rangeMin] = 0x50B8;
            mapDataStd [rangeNo] [0xDCC5 - rangeMin] = 0x51D7;
            mapDataStd [rangeNo] [0xDCC6 - rangeMin] = 0x527A;
            mapDataStd [rangeNo] [0xDCC7 - rangeMin] = 0x5278;
            mapDataStd [rangeNo] [0xDCC8 - rangeMin] = 0x527B;
            mapDataStd [rangeNo] [0xDCC9 - rangeMin] = 0x527C;
            mapDataStd [rangeNo] [0xDCCA - rangeMin] = 0x55C3;
            mapDataStd [rangeNo] [0xDCCB - rangeMin] = 0x55DB;
            mapDataStd [rangeNo] [0xDCCC - rangeMin] = 0x55CC;
            mapDataStd [rangeNo] [0xDCCD - rangeMin] = 0x55D0;
            mapDataStd [rangeNo] [0xDCCE - rangeMin] = 0x55CB;
            mapDataStd [rangeNo] [0xDCCF - rangeMin] = 0x55CA;

            mapDataStd [rangeNo] [0xDCD0 - rangeMin] = 0x55DD;
            mapDataStd [rangeNo] [0xDCD1 - rangeMin] = 0x55C0;
            mapDataStd [rangeNo] [0xDCD2 - rangeMin] = 0x55D4;
            mapDataStd [rangeNo] [0xDCD3 - rangeMin] = 0x55C4;
            mapDataStd [rangeNo] [0xDCD4 - rangeMin] = 0x55E9;
            mapDataStd [rangeNo] [0xDCD5 - rangeMin] = 0x55BF;
            mapDataStd [rangeNo] [0xDCD6 - rangeMin] = 0x55D2;
            mapDataStd [rangeNo] [0xDCD7 - rangeMin] = 0x558D;
            mapDataStd [rangeNo] [0xDCD8 - rangeMin] = 0x55CF;
            mapDataStd [rangeNo] [0xDCD9 - rangeMin] = 0x55D5;
            mapDataStd [rangeNo] [0xDCDA - rangeMin] = 0x55E2;
            mapDataStd [rangeNo] [0xDCDB - rangeMin] = 0x55D6;
            mapDataStd [rangeNo] [0xDCDC - rangeMin] = 0x55C8;
            mapDataStd [rangeNo] [0xDCDD - rangeMin] = 0x55F2;
            mapDataStd [rangeNo] [0xDCDE - rangeMin] = 0x55CD;
            mapDataStd [rangeNo] [0xDCDF - rangeMin] = 0x55D9;

            mapDataStd [rangeNo] [0xDCE0 - rangeMin] = 0x55C2;
            mapDataStd [rangeNo] [0xDCE1 - rangeMin] = 0x5714;
            mapDataStd [rangeNo] [0xDCE2 - rangeMin] = 0x5853;
            mapDataStd [rangeNo] [0xDCE3 - rangeMin] = 0x5868;
            mapDataStd [rangeNo] [0xDCE4 - rangeMin] = 0x5864;
            mapDataStd [rangeNo] [0xDCE5 - rangeMin] = 0x584F;
            mapDataStd [rangeNo] [0xDCE6 - rangeMin] = 0x584D;
            mapDataStd [rangeNo] [0xDCE7 - rangeMin] = 0x5849;
            mapDataStd [rangeNo] [0xDCE8 - rangeMin] = 0x586F;
            mapDataStd [rangeNo] [0xDCE9 - rangeMin] = 0x5855;
            mapDataStd [rangeNo] [0xDCEA - rangeMin] = 0x584E;
            mapDataStd [rangeNo] [0xDCEB - rangeMin] = 0x585D;
            mapDataStd [rangeNo] [0xDCEC - rangeMin] = 0x5859;
            mapDataStd [rangeNo] [0xDCED - rangeMin] = 0x5865;
            mapDataStd [rangeNo] [0xDCEE - rangeMin] = 0x585B;
            mapDataStd [rangeNo] [0xDCEF - rangeMin] = 0x583D;

            mapDataStd [rangeNo] [0xDCF0 - rangeMin] = 0x5863;
            mapDataStd [rangeNo] [0xDCF1 - rangeMin] = 0x5871;
            mapDataStd [rangeNo] [0xDCF2 - rangeMin] = 0x58FC;
            mapDataStd [rangeNo] [0xDCF3 - rangeMin] = 0x5AC7;
            mapDataStd [rangeNo] [0xDCF4 - rangeMin] = 0x5AC4;
            mapDataStd [rangeNo] [0xDCF5 - rangeMin] = 0x5ACB;
            mapDataStd [rangeNo] [0xDCF6 - rangeMin] = 0x5ABA;
            mapDataStd [rangeNo] [0xDCF7 - rangeMin] = 0x5AB8;
            mapDataStd [rangeNo] [0xDCF8 - rangeMin] = 0x5AB1;
            mapDataStd [rangeNo] [0xDCF9 - rangeMin] = 0x5AB5;
            mapDataStd [rangeNo] [0xDCFA - rangeMin] = 0x5AB0;
            mapDataStd [rangeNo] [0xDCFB - rangeMin] = 0x5ABF;
            mapDataStd [rangeNo] [0xDCFC - rangeMin] = 0x5AC8;
            mapDataStd [rangeNo] [0xDCFD - rangeMin] = 0x5ABB;
            mapDataStd [rangeNo] [0xDCFE - rangeMin] = 0x5AC6;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xDD40 - rangeMin] = 0x5AB7;
            mapDataStd [rangeNo] [0xDD41 - rangeMin] = 0x5AC0;
            mapDataStd [rangeNo] [0xDD42 - rangeMin] = 0x5ACA;
            mapDataStd [rangeNo] [0xDD43 - rangeMin] = 0x5AB4;
            mapDataStd [rangeNo] [0xDD44 - rangeMin] = 0x5AB6;
            mapDataStd [rangeNo] [0xDD45 - rangeMin] = 0x5ACD;
            mapDataStd [rangeNo] [0xDD46 - rangeMin] = 0x5AB9;
            mapDataStd [rangeNo] [0xDD47 - rangeMin] = 0x5A90;
            mapDataStd [rangeNo] [0xDD48 - rangeMin] = 0x5BD6;
            mapDataStd [rangeNo] [0xDD49 - rangeMin] = 0x5BD8;
            mapDataStd [rangeNo] [0xDD4A - rangeMin] = 0x5BD9;
            mapDataStd [rangeNo] [0xDD4B - rangeMin] = 0x5C1F;
            mapDataStd [rangeNo] [0xDD4C - rangeMin] = 0x5C33;
            mapDataStd [rangeNo] [0xDD4D - rangeMin] = 0x5D71;
            mapDataStd [rangeNo] [0xDD4E - rangeMin] = 0x5D63;
            mapDataStd [rangeNo] [0xDD4F - rangeMin] = 0x5D4A;

            mapDataStd [rangeNo] [0xDD50 - rangeMin] = 0x5D65;
            mapDataStd [rangeNo] [0xDD51 - rangeMin] = 0x5D72;
            mapDataStd [rangeNo] [0xDD52 - rangeMin] = 0x5D6C;
            mapDataStd [rangeNo] [0xDD53 - rangeMin] = 0x5D5E;
            mapDataStd [rangeNo] [0xDD54 - rangeMin] = 0x5D68;
            mapDataStd [rangeNo] [0xDD55 - rangeMin] = 0x5D67;
            mapDataStd [rangeNo] [0xDD56 - rangeMin] = 0x5D62;
            mapDataStd [rangeNo] [0xDD57 - rangeMin] = 0x5DF0;
            mapDataStd [rangeNo] [0xDD58 - rangeMin] = 0x5E4F;
            mapDataStd [rangeNo] [0xDD59 - rangeMin] = 0x5E4E;
            mapDataStd [rangeNo] [0xDD5A - rangeMin] = 0x5E4A;
            mapDataStd [rangeNo] [0xDD5B - rangeMin] = 0x5E4D;
            mapDataStd [rangeNo] [0xDD5C - rangeMin] = 0x5E4B;
            mapDataStd [rangeNo] [0xDD5D - rangeMin] = 0x5EC5;
            mapDataStd [rangeNo] [0xDD5E - rangeMin] = 0x5ECC;
            mapDataStd [rangeNo] [0xDD5F - rangeMin] = 0x5EC6;

            mapDataStd [rangeNo] [0xDD60 - rangeMin] = 0x5ECB;
            mapDataStd [rangeNo] [0xDD61 - rangeMin] = 0x5EC7;
            mapDataStd [rangeNo] [0xDD62 - rangeMin] = 0x5F40;
            mapDataStd [rangeNo] [0xDD63 - rangeMin] = 0x5FAF;
            mapDataStd [rangeNo] [0xDD64 - rangeMin] = 0x5FAD;
            mapDataStd [rangeNo] [0xDD65 - rangeMin] = 0x60F7;
            mapDataStd [rangeNo] [0xDD66 - rangeMin] = 0x6149;
            mapDataStd [rangeNo] [0xDD67 - rangeMin] = 0x614A;
            mapDataStd [rangeNo] [0xDD68 - rangeMin] = 0x612B;
            mapDataStd [rangeNo] [0xDD69 - rangeMin] = 0x6145;
            mapDataStd [rangeNo] [0xDD6A - rangeMin] = 0x6136;
            mapDataStd [rangeNo] [0xDD6B - rangeMin] = 0x6132;
            mapDataStd [rangeNo] [0xDD6C - rangeMin] = 0x612E;
            mapDataStd [rangeNo] [0xDD6D - rangeMin] = 0x6146;
            mapDataStd [rangeNo] [0xDD6E - rangeMin] = 0x612F;
            mapDataStd [rangeNo] [0xDD6F - rangeMin] = 0x614F;

            mapDataStd [rangeNo] [0xDD70 - rangeMin] = 0x6129;
            mapDataStd [rangeNo] [0xDD71 - rangeMin] = 0x6140;
            mapDataStd [rangeNo] [0xDD72 - rangeMin] = 0x6220;
            mapDataStd [rangeNo] [0xDD73 - rangeMin] = 0x9168;
            mapDataStd [rangeNo] [0xDD74 - rangeMin] = 0x6223;
            mapDataStd [rangeNo] [0xDD75 - rangeMin] = 0x6225;
            mapDataStd [rangeNo] [0xDD76 - rangeMin] = 0x6224;
            mapDataStd [rangeNo] [0xDD77 - rangeMin] = 0x63C5;
            mapDataStd [rangeNo] [0xDD78 - rangeMin] = 0x63F1;
            mapDataStd [rangeNo] [0xDD79 - rangeMin] = 0x63EB;
            mapDataStd [rangeNo] [0xDD7A - rangeMin] = 0x6410;
            mapDataStd [rangeNo] [0xDD7B - rangeMin] = 0x6412;
            mapDataStd [rangeNo] [0xDD7C - rangeMin] = 0x6409;
            mapDataStd [rangeNo] [0xDD7D - rangeMin] = 0x6420;
            mapDataStd [rangeNo] [0xDD7E - rangeMin] = 0x6424;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xDDA1 - rangeMin] = 0x6433;
            mapDataStd [rangeNo] [0xDDA2 - rangeMin] = 0x6443;
            mapDataStd [rangeNo] [0xDDA3 - rangeMin] = 0x641F;
            mapDataStd [rangeNo] [0xDDA4 - rangeMin] = 0x6415;
            mapDataStd [rangeNo] [0xDDA5 - rangeMin] = 0x6418;
            mapDataStd [rangeNo] [0xDDA6 - rangeMin] = 0x6439;
            mapDataStd [rangeNo] [0xDDA7 - rangeMin] = 0x6437;
            mapDataStd [rangeNo] [0xDDA8 - rangeMin] = 0x6422;
            mapDataStd [rangeNo] [0xDDA9 - rangeMin] = 0x6423;
            mapDataStd [rangeNo] [0xDDAA - rangeMin] = 0x640C;
            mapDataStd [rangeNo] [0xDDAB - rangeMin] = 0x6426;
            mapDataStd [rangeNo] [0xDDAC - rangeMin] = 0x6430;
            mapDataStd [rangeNo] [0xDDAD - rangeMin] = 0x6428;
            mapDataStd [rangeNo] [0xDDAE - rangeMin] = 0x6441;
            mapDataStd [rangeNo] [0xDDAF - rangeMin] = 0x6435;

            mapDataStd [rangeNo] [0xDDB0 - rangeMin] = 0x642F;
            mapDataStd [rangeNo] [0xDDB1 - rangeMin] = 0x640A;
            mapDataStd [rangeNo] [0xDDB2 - rangeMin] = 0x641A;
            mapDataStd [rangeNo] [0xDDB3 - rangeMin] = 0x6440;
            mapDataStd [rangeNo] [0xDDB4 - rangeMin] = 0x6425;
            mapDataStd [rangeNo] [0xDDB5 - rangeMin] = 0x6427;
            mapDataStd [rangeNo] [0xDDB6 - rangeMin] = 0x640B;
            mapDataStd [rangeNo] [0xDDB7 - rangeMin] = 0x63E7;
            mapDataStd [rangeNo] [0xDDB8 - rangeMin] = 0x641B;
            mapDataStd [rangeNo] [0xDDB9 - rangeMin] = 0x642E;
            mapDataStd [rangeNo] [0xDDBA - rangeMin] = 0x6421;
            mapDataStd [rangeNo] [0xDDBB - rangeMin] = 0x640E;
            mapDataStd [rangeNo] [0xDDBC - rangeMin] = 0x656F;
            mapDataStd [rangeNo] [0xDDBD - rangeMin] = 0x6592;
            mapDataStd [rangeNo] [0xDDBE - rangeMin] = 0x65D3;
            mapDataStd [rangeNo] [0xDDBF - rangeMin] = 0x6686;

            mapDataStd [rangeNo] [0xDDC0 - rangeMin] = 0x668C;
            mapDataStd [rangeNo] [0xDDC1 - rangeMin] = 0x6695;
            mapDataStd [rangeNo] [0xDDC2 - rangeMin] = 0x6690;
            mapDataStd [rangeNo] [0xDDC3 - rangeMin] = 0x668B;
            mapDataStd [rangeNo] [0xDDC4 - rangeMin] = 0x668A;
            mapDataStd [rangeNo] [0xDDC5 - rangeMin] = 0x6699;
            mapDataStd [rangeNo] [0xDDC6 - rangeMin] = 0x6694;
            mapDataStd [rangeNo] [0xDDC7 - rangeMin] = 0x6678;
            mapDataStd [rangeNo] [0xDDC8 - rangeMin] = 0x6720;
            mapDataStd [rangeNo] [0xDDC9 - rangeMin] = 0x6966;
            mapDataStd [rangeNo] [0xDDCA - rangeMin] = 0x695F;
            mapDataStd [rangeNo] [0xDDCB - rangeMin] = 0x6938;
            mapDataStd [rangeNo] [0xDDCC - rangeMin] = 0x694E;
            mapDataStd [rangeNo] [0xDDCD - rangeMin] = 0x6962;
            mapDataStd [rangeNo] [0xDDCE - rangeMin] = 0x6971;
            mapDataStd [rangeNo] [0xDDCF - rangeMin] = 0x693F;

            mapDataStd [rangeNo] [0xDDD0 - rangeMin] = 0x6945;
            mapDataStd [rangeNo] [0xDDD1 - rangeMin] = 0x696A;
            mapDataStd [rangeNo] [0xDDD2 - rangeMin] = 0x6939;
            mapDataStd [rangeNo] [0xDDD3 - rangeMin] = 0x6942;
            mapDataStd [rangeNo] [0xDDD4 - rangeMin] = 0x6957;
            mapDataStd [rangeNo] [0xDDD5 - rangeMin] = 0x6959;
            mapDataStd [rangeNo] [0xDDD6 - rangeMin] = 0x697A;
            mapDataStd [rangeNo] [0xDDD7 - rangeMin] = 0x6948;
            mapDataStd [rangeNo] [0xDDD8 - rangeMin] = 0x6949;
            mapDataStd [rangeNo] [0xDDD9 - rangeMin] = 0x6935;
            mapDataStd [rangeNo] [0xDDDA - rangeMin] = 0x696C;
            mapDataStd [rangeNo] [0xDDDB - rangeMin] = 0x6933;
            mapDataStd [rangeNo] [0xDDDC - rangeMin] = 0x693D;
            mapDataStd [rangeNo] [0xDDDD - rangeMin] = 0x6965;
            mapDataStd [rangeNo] [0xDDDE - rangeMin] = 0x68F0;
            mapDataStd [rangeNo] [0xDDDF - rangeMin] = 0x6978;

            mapDataStd [rangeNo] [0xDDE0 - rangeMin] = 0x6934;
            mapDataStd [rangeNo] [0xDDE1 - rangeMin] = 0x6969;
            mapDataStd [rangeNo] [0xDDE2 - rangeMin] = 0x6940;
            mapDataStd [rangeNo] [0xDDE3 - rangeMin] = 0x696F;
            mapDataStd [rangeNo] [0xDDE4 - rangeMin] = 0x6944;
            mapDataStd [rangeNo] [0xDDE5 - rangeMin] = 0x6976;
            mapDataStd [rangeNo] [0xDDE6 - rangeMin] = 0x6958;
            mapDataStd [rangeNo] [0xDDE7 - rangeMin] = 0x6941;
            mapDataStd [rangeNo] [0xDDE8 - rangeMin] = 0x6974;
            mapDataStd [rangeNo] [0xDDE9 - rangeMin] = 0x694C;
            mapDataStd [rangeNo] [0xDDEA - rangeMin] = 0x693B;
            mapDataStd [rangeNo] [0xDDEB - rangeMin] = 0x694B;
            mapDataStd [rangeNo] [0xDDEC - rangeMin] = 0x6937;
            mapDataStd [rangeNo] [0xDDED - rangeMin] = 0x695C;
            mapDataStd [rangeNo] [0xDDEE - rangeMin] = 0x694F;
            mapDataStd [rangeNo] [0xDDEF - rangeMin] = 0x6951;

            mapDataStd [rangeNo] [0xDDF0 - rangeMin] = 0x6932;
            mapDataStd [rangeNo] [0xDDF1 - rangeMin] = 0x6952;
            mapDataStd [rangeNo] [0xDDF2 - rangeMin] = 0x692F;
            mapDataStd [rangeNo] [0xDDF3 - rangeMin] = 0x697B;
            mapDataStd [rangeNo] [0xDDF4 - rangeMin] = 0x693C;
            mapDataStd [rangeNo] [0xDDF5 - rangeMin] = 0x6B46;
            mapDataStd [rangeNo] [0xDDF6 - rangeMin] = 0x6B45;
            mapDataStd [rangeNo] [0xDDF7 - rangeMin] = 0x6B43;
            mapDataStd [rangeNo] [0xDDF8 - rangeMin] = 0x6B42;
            mapDataStd [rangeNo] [0xDDF9 - rangeMin] = 0x6B48;
            mapDataStd [rangeNo] [0xDDFA - rangeMin] = 0x6B41;
            mapDataStd [rangeNo] [0xDDFB - rangeMin] = 0x6B9B;
            mapDataStd [rangeNo] [0xDDFC - rangeMin] = 0xFA0D;
            mapDataStd [rangeNo] [0xDDFD - rangeMin] = 0x6BFB;
            mapDataStd [rangeNo] [0xDDFE - rangeMin] = 0x6BFC;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xDE40 - rangeMin] = 0x6BF9;
            mapDataStd [rangeNo] [0xDE41 - rangeMin] = 0x6BF7;
            mapDataStd [rangeNo] [0xDE42 - rangeMin] = 0x6BF8;
            mapDataStd [rangeNo] [0xDE43 - rangeMin] = 0x6E9B;
            mapDataStd [rangeNo] [0xDE44 - rangeMin] = 0x6ED6;
            mapDataStd [rangeNo] [0xDE45 - rangeMin] = 0x6EC8;
            mapDataStd [rangeNo] [0xDE46 - rangeMin] = 0x6E8F;
            mapDataStd [rangeNo] [0xDE47 - rangeMin] = 0x6EC0;
            mapDataStd [rangeNo] [0xDE48 - rangeMin] = 0x6E9F;
            mapDataStd [rangeNo] [0xDE49 - rangeMin] = 0x6E93;
            mapDataStd [rangeNo] [0xDE4A - rangeMin] = 0x6E94;
            mapDataStd [rangeNo] [0xDE4B - rangeMin] = 0x6EA0;
            mapDataStd [rangeNo] [0xDE4C - rangeMin] = 0x6EB1;
            mapDataStd [rangeNo] [0xDE4D - rangeMin] = 0x6EB9;
            mapDataStd [rangeNo] [0xDE4E - rangeMin] = 0x6EC6;
            mapDataStd [rangeNo] [0xDE4F - rangeMin] = 0x6ED2;

            mapDataStd [rangeNo] [0xDE50 - rangeMin] = 0x6EBD;
            mapDataStd [rangeNo] [0xDE51 - rangeMin] = 0x6EC1;
            mapDataStd [rangeNo] [0xDE52 - rangeMin] = 0x6E9E;
            mapDataStd [rangeNo] [0xDE53 - rangeMin] = 0x6EC9;
            mapDataStd [rangeNo] [0xDE54 - rangeMin] = 0x6EB7;
            mapDataStd [rangeNo] [0xDE55 - rangeMin] = 0x6EB0;
            mapDataStd [rangeNo] [0xDE56 - rangeMin] = 0x6ECD;
            mapDataStd [rangeNo] [0xDE57 - rangeMin] = 0x6EA6;
            mapDataStd [rangeNo] [0xDE58 - rangeMin] = 0x6ECF;
            mapDataStd [rangeNo] [0xDE59 - rangeMin] = 0x6EB2;
            mapDataStd [rangeNo] [0xDE5A - rangeMin] = 0x6EBE;
            mapDataStd [rangeNo] [0xDE5B - rangeMin] = 0x6EC3;
            mapDataStd [rangeNo] [0xDE5C - rangeMin] = 0x6EDC;
            mapDataStd [rangeNo] [0xDE5D - rangeMin] = 0x6ED8;
            mapDataStd [rangeNo] [0xDE5E - rangeMin] = 0x6E99;
            mapDataStd [rangeNo] [0xDE5F - rangeMin] = 0x6E92;

            mapDataStd [rangeNo] [0xDE60 - rangeMin] = 0x6E8E;
            mapDataStd [rangeNo] [0xDE61 - rangeMin] = 0x6E8D;
            mapDataStd [rangeNo] [0xDE62 - rangeMin] = 0x6EA4;
            mapDataStd [rangeNo] [0xDE63 - rangeMin] = 0x6EA1;
            mapDataStd [rangeNo] [0xDE64 - rangeMin] = 0x6EBF;
            mapDataStd [rangeNo] [0xDE65 - rangeMin] = 0x6EB3;
            mapDataStd [rangeNo] [0xDE66 - rangeMin] = 0x6ED0;
            mapDataStd [rangeNo] [0xDE67 - rangeMin] = 0x6ECA;
            mapDataStd [rangeNo] [0xDE68 - rangeMin] = 0x6E97;
            mapDataStd [rangeNo] [0xDE69 - rangeMin] = 0x6EAE;
            mapDataStd [rangeNo] [0xDE6A - rangeMin] = 0x6EA3;
            mapDataStd [rangeNo] [0xDE6B - rangeMin] = 0x7147;
            mapDataStd [rangeNo] [0xDE6C - rangeMin] = 0x7154;
            mapDataStd [rangeNo] [0xDE6D - rangeMin] = 0x7152;
            mapDataStd [rangeNo] [0xDE6E - rangeMin] = 0x7163;
            mapDataStd [rangeNo] [0xDE6F - rangeMin] = 0x7160;

            mapDataStd [rangeNo] [0xDE70 - rangeMin] = 0x7141;
            mapDataStd [rangeNo] [0xDE71 - rangeMin] = 0x715D;
            mapDataStd [rangeNo] [0xDE72 - rangeMin] = 0x7162;
            mapDataStd [rangeNo] [0xDE73 - rangeMin] = 0x7172;
            mapDataStd [rangeNo] [0xDE74 - rangeMin] = 0x7178;
            mapDataStd [rangeNo] [0xDE75 - rangeMin] = 0x716A;
            mapDataStd [rangeNo] [0xDE76 - rangeMin] = 0x7161;
            mapDataStd [rangeNo] [0xDE77 - rangeMin] = 0x7142;
            mapDataStd [rangeNo] [0xDE78 - rangeMin] = 0x7158;
            mapDataStd [rangeNo] [0xDE79 - rangeMin] = 0x7143;
            mapDataStd [rangeNo] [0xDE7A - rangeMin] = 0x714B;
            mapDataStd [rangeNo] [0xDE7B - rangeMin] = 0x7170;
            mapDataStd [rangeNo] [0xDE7C - rangeMin] = 0x715F;
            mapDataStd [rangeNo] [0xDE7D - rangeMin] = 0x7150;
            mapDataStd [rangeNo] [0xDE7E - rangeMin] = 0x7153;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xDEA1 - rangeMin] = 0x7144;
            mapDataStd [rangeNo] [0xDEA2 - rangeMin] = 0x714D;
            mapDataStd [rangeNo] [0xDEA3 - rangeMin] = 0x715A;
            mapDataStd [rangeNo] [0xDEA4 - rangeMin] = 0x724F;
            mapDataStd [rangeNo] [0xDEA5 - rangeMin] = 0x728D;
            mapDataStd [rangeNo] [0xDEA6 - rangeMin] = 0x728C;
            mapDataStd [rangeNo] [0xDEA7 - rangeMin] = 0x7291;
            mapDataStd [rangeNo] [0xDEA8 - rangeMin] = 0x7290;
            mapDataStd [rangeNo] [0xDEA9 - rangeMin] = 0x728E;
            mapDataStd [rangeNo] [0xDEAA - rangeMin] = 0x733C;
            mapDataStd [rangeNo] [0xDEAB - rangeMin] = 0x7342;
            mapDataStd [rangeNo] [0xDEAC - rangeMin] = 0x733B;
            mapDataStd [rangeNo] [0xDEAD - rangeMin] = 0x733A;
            mapDataStd [rangeNo] [0xDEAE - rangeMin] = 0x7340;
            mapDataStd [rangeNo] [0xDEAF - rangeMin] = 0x734A;

            mapDataStd [rangeNo] [0xDEB0 - rangeMin] = 0x7349;
            mapDataStd [rangeNo] [0xDEB1 - rangeMin] = 0x7444;
            mapDataStd [rangeNo] [0xDEB2 - rangeMin] = 0x744A;
            mapDataStd [rangeNo] [0xDEB3 - rangeMin] = 0x744B;
            mapDataStd [rangeNo] [0xDEB4 - rangeMin] = 0x7452;
            mapDataStd [rangeNo] [0xDEB5 - rangeMin] = 0x7451;
            mapDataStd [rangeNo] [0xDEB6 - rangeMin] = 0x7457;
            mapDataStd [rangeNo] [0xDEB7 - rangeMin] = 0x7440;
            mapDataStd [rangeNo] [0xDEB8 - rangeMin] = 0x744F;
            mapDataStd [rangeNo] [0xDEB9 - rangeMin] = 0x7450;
            mapDataStd [rangeNo] [0xDEBA - rangeMin] = 0x744E;
            mapDataStd [rangeNo] [0xDEBB - rangeMin] = 0x7442;
            mapDataStd [rangeNo] [0xDEBC - rangeMin] = 0x7446;
            mapDataStd [rangeNo] [0xDEBD - rangeMin] = 0x744D;
            mapDataStd [rangeNo] [0xDEBE - rangeMin] = 0x7454;
            mapDataStd [rangeNo] [0xDEBF - rangeMin] = 0x74E1;

            mapDataStd [rangeNo] [0xDEC0 - rangeMin] = 0x74FF;
            mapDataStd [rangeNo] [0xDEC1 - rangeMin] = 0x74FE;
            mapDataStd [rangeNo] [0xDEC2 - rangeMin] = 0x74FD;
            mapDataStd [rangeNo] [0xDEC3 - rangeMin] = 0x751D;
            mapDataStd [rangeNo] [0xDEC4 - rangeMin] = 0x7579;
            mapDataStd [rangeNo] [0xDEC5 - rangeMin] = 0x7577;
            mapDataStd [rangeNo] [0xDEC6 - rangeMin] = 0x6983;
            mapDataStd [rangeNo] [0xDEC7 - rangeMin] = 0x75EF;
            mapDataStd [rangeNo] [0xDEC8 - rangeMin] = 0x760F;
            mapDataStd [rangeNo] [0xDEC9 - rangeMin] = 0x7603;
            mapDataStd [rangeNo] [0xDECA - rangeMin] = 0x75F7;
            mapDataStd [rangeNo] [0xDECB - rangeMin] = 0x75FE;
            mapDataStd [rangeNo] [0xDECC - rangeMin] = 0x75FC;
            mapDataStd [rangeNo] [0xDECD - rangeMin] = 0x75F9;
            mapDataStd [rangeNo] [0xDECE - rangeMin] = 0x75F8;
            mapDataStd [rangeNo] [0xDECF - rangeMin] = 0x7610;

            mapDataStd [rangeNo] [0xDED0 - rangeMin] = 0x75FB;
            mapDataStd [rangeNo] [0xDED1 - rangeMin] = 0x75F6;
            mapDataStd [rangeNo] [0xDED2 - rangeMin] = 0x75ED;
            mapDataStd [rangeNo] [0xDED3 - rangeMin] = 0x75F5;
            mapDataStd [rangeNo] [0xDED4 - rangeMin] = 0x75FD;
            mapDataStd [rangeNo] [0xDED5 - rangeMin] = 0x7699;
            mapDataStd [rangeNo] [0xDED6 - rangeMin] = 0x76B5;
            mapDataStd [rangeNo] [0xDED7 - rangeMin] = 0x76DD;
            mapDataStd [rangeNo] [0xDED8 - rangeMin] = 0x7755;
            mapDataStd [rangeNo] [0xDED9 - rangeMin] = 0x775F;
            mapDataStd [rangeNo] [0xDEDA - rangeMin] = 0x7760;
            mapDataStd [rangeNo] [0xDEDB - rangeMin] = 0x7752;
            mapDataStd [rangeNo] [0xDEDC - rangeMin] = 0x7756;
            mapDataStd [rangeNo] [0xDEDD - rangeMin] = 0x775A;
            mapDataStd [rangeNo] [0xDEDE - rangeMin] = 0x7769;
            mapDataStd [rangeNo] [0xDEDF - rangeMin] = 0x7767;

            mapDataStd [rangeNo] [0xDEE0 - rangeMin] = 0x7754;
            mapDataStd [rangeNo] [0xDEE1 - rangeMin] = 0x7759;
            mapDataStd [rangeNo] [0xDEE2 - rangeMin] = 0x776D;
            mapDataStd [rangeNo] [0xDEE3 - rangeMin] = 0x77E0;
            mapDataStd [rangeNo] [0xDEE4 - rangeMin] = 0x7887;
            mapDataStd [rangeNo] [0xDEE5 - rangeMin] = 0x789A;
            mapDataStd [rangeNo] [0xDEE6 - rangeMin] = 0x7894;
            mapDataStd [rangeNo] [0xDEE7 - rangeMin] = 0x788F;
            mapDataStd [rangeNo] [0xDEE8 - rangeMin] = 0x7884;
            mapDataStd [rangeNo] [0xDEE9 - rangeMin] = 0x7895;
            mapDataStd [rangeNo] [0xDEEA - rangeMin] = 0x7885;
            mapDataStd [rangeNo] [0xDEEB - rangeMin] = 0x7886;
            mapDataStd [rangeNo] [0xDEEC - rangeMin] = 0x78A1;
            mapDataStd [rangeNo] [0xDEED - rangeMin] = 0x7883;
            mapDataStd [rangeNo] [0xDEEE - rangeMin] = 0x7879;
            mapDataStd [rangeNo] [0xDEEF - rangeMin] = 0x7899;

            mapDataStd [rangeNo] [0xDEF0 - rangeMin] = 0x7880;
            mapDataStd [rangeNo] [0xDEF1 - rangeMin] = 0x7896;
            mapDataStd [rangeNo] [0xDEF2 - rangeMin] = 0x787B;
            mapDataStd [rangeNo] [0xDEF3 - rangeMin] = 0x797C;
            mapDataStd [rangeNo] [0xDEF4 - rangeMin] = 0x7982;
            mapDataStd [rangeNo] [0xDEF5 - rangeMin] = 0x797D;
            mapDataStd [rangeNo] [0xDEF6 - rangeMin] = 0x7979;
            mapDataStd [rangeNo] [0xDEF7 - rangeMin] = 0x7A11;
            mapDataStd [rangeNo] [0xDEF8 - rangeMin] = 0x7A18;
            mapDataStd [rangeNo] [0xDEF9 - rangeMin] = 0x7A19;
            mapDataStd [rangeNo] [0xDEFA - rangeMin] = 0x7A12;
            mapDataStd [rangeNo] [0xDEFB - rangeMin] = 0x7A17;
            mapDataStd [rangeNo] [0xDEFC - rangeMin] = 0x7A15;
            mapDataStd [rangeNo] [0xDEFD - rangeMin] = 0x7A22;
            mapDataStd [rangeNo] [0xDEFE - rangeMin] = 0x7A13;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xDF40 - rangeMin] = 0x7A1B;
            mapDataStd [rangeNo] [0xDF41 - rangeMin] = 0x7A10;
            mapDataStd [rangeNo] [0xDF42 - rangeMin] = 0x7AA3;
            mapDataStd [rangeNo] [0xDF43 - rangeMin] = 0x7AA2;
            mapDataStd [rangeNo] [0xDF44 - rangeMin] = 0x7A9E;
            mapDataStd [rangeNo] [0xDF45 - rangeMin] = 0x7AEB;
            mapDataStd [rangeNo] [0xDF46 - rangeMin] = 0x7B66;
            mapDataStd [rangeNo] [0xDF47 - rangeMin] = 0x7B64;
            mapDataStd [rangeNo] [0xDF48 - rangeMin] = 0x7B6D;
            mapDataStd [rangeNo] [0xDF49 - rangeMin] = 0x7B74;
            mapDataStd [rangeNo] [0xDF4A - rangeMin] = 0x7B69;
            mapDataStd [rangeNo] [0xDF4B - rangeMin] = 0x7B72;
            mapDataStd [rangeNo] [0xDF4C - rangeMin] = 0x7B65;
            mapDataStd [rangeNo] [0xDF4D - rangeMin] = 0x7B73;
            mapDataStd [rangeNo] [0xDF4E - rangeMin] = 0x7B71;
            mapDataStd [rangeNo] [0xDF4F - rangeMin] = 0x7B70;

            mapDataStd [rangeNo] [0xDF50 - rangeMin] = 0x7B61;
            mapDataStd [rangeNo] [0xDF51 - rangeMin] = 0x7B78;
            mapDataStd [rangeNo] [0xDF52 - rangeMin] = 0x7B76;
            mapDataStd [rangeNo] [0xDF53 - rangeMin] = 0x7B63;
            mapDataStd [rangeNo] [0xDF54 - rangeMin] = 0x7CB2;
            mapDataStd [rangeNo] [0xDF55 - rangeMin] = 0x7CB4;
            mapDataStd [rangeNo] [0xDF56 - rangeMin] = 0x7CAF;
            mapDataStd [rangeNo] [0xDF57 - rangeMin] = 0x7D88;
            mapDataStd [rangeNo] [0xDF58 - rangeMin] = 0x7D86;
            mapDataStd [rangeNo] [0xDF59 - rangeMin] = 0x7D80;
            mapDataStd [rangeNo] [0xDF5A - rangeMin] = 0x7D8D;
            mapDataStd [rangeNo] [0xDF5B - rangeMin] = 0x7D7F;
            mapDataStd [rangeNo] [0xDF5C - rangeMin] = 0x7D85;
            mapDataStd [rangeNo] [0xDF5D - rangeMin] = 0x7D7A;
            mapDataStd [rangeNo] [0xDF5E - rangeMin] = 0x7D8E;
            mapDataStd [rangeNo] [0xDF5F - rangeMin] = 0x7D7B;

            mapDataStd [rangeNo] [0xDF60 - rangeMin] = 0x7D83;
            mapDataStd [rangeNo] [0xDF61 - rangeMin] = 0x7D7C;
            mapDataStd [rangeNo] [0xDF62 - rangeMin] = 0x7D8C;
            mapDataStd [rangeNo] [0xDF63 - rangeMin] = 0x7D94;
            mapDataStd [rangeNo] [0xDF64 - rangeMin] = 0x7D84;
            mapDataStd [rangeNo] [0xDF65 - rangeMin] = 0x7D7D;
            mapDataStd [rangeNo] [0xDF66 - rangeMin] = 0x7D92;
            mapDataStd [rangeNo] [0xDF67 - rangeMin] = 0x7F6D;
            mapDataStd [rangeNo] [0xDF68 - rangeMin] = 0x7F6B;
            mapDataStd [rangeNo] [0xDF69 - rangeMin] = 0x7F67;
            mapDataStd [rangeNo] [0xDF6A - rangeMin] = 0x7F68;
            mapDataStd [rangeNo] [0xDF6B - rangeMin] = 0x7F6C;
            mapDataStd [rangeNo] [0xDF6C - rangeMin] = 0x7FA6;
            mapDataStd [rangeNo] [0xDF6D - rangeMin] = 0x7FA5;
            mapDataStd [rangeNo] [0xDF6E - rangeMin] = 0x7FA7;
            mapDataStd [rangeNo] [0xDF6F - rangeMin] = 0x7FDB;

            mapDataStd [rangeNo] [0xDF70 - rangeMin] = 0x7FDC;
            mapDataStd [rangeNo] [0xDF71 - rangeMin] = 0x8021;
            mapDataStd [rangeNo] [0xDF72 - rangeMin] = 0x8164;
            mapDataStd [rangeNo] [0xDF73 - rangeMin] = 0x8160;
            mapDataStd [rangeNo] [0xDF74 - rangeMin] = 0x8177;
            mapDataStd [rangeNo] [0xDF75 - rangeMin] = 0x815C;
            mapDataStd [rangeNo] [0xDF76 - rangeMin] = 0x8169;
            mapDataStd [rangeNo] [0xDF77 - rangeMin] = 0x815B;
            mapDataStd [rangeNo] [0xDF78 - rangeMin] = 0x8162;
            mapDataStd [rangeNo] [0xDF79 - rangeMin] = 0x8172;
            mapDataStd [rangeNo] [0xDF7A - rangeMin] = 0x6721;
            mapDataStd [rangeNo] [0xDF7B - rangeMin] = 0x815E;
            mapDataStd [rangeNo] [0xDF7C - rangeMin] = 0x8176;
            mapDataStd [rangeNo] [0xDF7D - rangeMin] = 0x8167;
            mapDataStd [rangeNo] [0xDF7E - rangeMin] = 0x816F;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xDFA1 - rangeMin] = 0x8144;
            mapDataStd [rangeNo] [0xDFA2 - rangeMin] = 0x8161;
            mapDataStd [rangeNo] [0xDFA3 - rangeMin] = 0x821D;
            mapDataStd [rangeNo] [0xDFA4 - rangeMin] = 0x8249;
            mapDataStd [rangeNo] [0xDFA5 - rangeMin] = 0x8244;
            mapDataStd [rangeNo] [0xDFA6 - rangeMin] = 0x8240;
            mapDataStd [rangeNo] [0xDFA7 - rangeMin] = 0x8242;
            mapDataStd [rangeNo] [0xDFA8 - rangeMin] = 0x8245;
            mapDataStd [rangeNo] [0xDFA9 - rangeMin] = 0x84F1;
            mapDataStd [rangeNo] [0xDFAA - rangeMin] = 0x843F;
            mapDataStd [rangeNo] [0xDFAB - rangeMin] = 0x8456;
            mapDataStd [rangeNo] [0xDFAC - rangeMin] = 0x8476;
            mapDataStd [rangeNo] [0xDFAD - rangeMin] = 0x8479;
            mapDataStd [rangeNo] [0xDFAE - rangeMin] = 0x848F;
            mapDataStd [rangeNo] [0xDFAF - rangeMin] = 0x848D;

            mapDataStd [rangeNo] [0xDFB0 - rangeMin] = 0x8465;
            mapDataStd [rangeNo] [0xDFB1 - rangeMin] = 0x8451;
            mapDataStd [rangeNo] [0xDFB2 - rangeMin] = 0x8440;
            mapDataStd [rangeNo] [0xDFB3 - rangeMin] = 0x8486;
            mapDataStd [rangeNo] [0xDFB4 - rangeMin] = 0x8467;
            mapDataStd [rangeNo] [0xDFB5 - rangeMin] = 0x8430;
            mapDataStd [rangeNo] [0xDFB6 - rangeMin] = 0x844D;
            mapDataStd [rangeNo] [0xDFB7 - rangeMin] = 0x847D;
            mapDataStd [rangeNo] [0xDFB8 - rangeMin] = 0x845A;
            mapDataStd [rangeNo] [0xDFB9 - rangeMin] = 0x8459;
            mapDataStd [rangeNo] [0xDFBA - rangeMin] = 0x8474;
            mapDataStd [rangeNo] [0xDFBB - rangeMin] = 0x8473;
            mapDataStd [rangeNo] [0xDFBC - rangeMin] = 0x845D;
            mapDataStd [rangeNo] [0xDFBD - rangeMin] = 0x8507;
            mapDataStd [rangeNo] [0xDFBE - rangeMin] = 0x845E;
            mapDataStd [rangeNo] [0xDFBF - rangeMin] = 0x8437;

            mapDataStd [rangeNo] [0xDFC0 - rangeMin] = 0x843A;
            mapDataStd [rangeNo] [0xDFC1 - rangeMin] = 0x8434;
            mapDataStd [rangeNo] [0xDFC2 - rangeMin] = 0x847A;
            mapDataStd [rangeNo] [0xDFC3 - rangeMin] = 0x8443;
            mapDataStd [rangeNo] [0xDFC4 - rangeMin] = 0x8478;
            mapDataStd [rangeNo] [0xDFC5 - rangeMin] = 0x8432;
            mapDataStd [rangeNo] [0xDFC6 - rangeMin] = 0x8445;
            mapDataStd [rangeNo] [0xDFC7 - rangeMin] = 0x8429;
            mapDataStd [rangeNo] [0xDFC8 - rangeMin] = 0x83D9;
            mapDataStd [rangeNo] [0xDFC9 - rangeMin] = 0x844B;
            mapDataStd [rangeNo] [0xDFCA - rangeMin] = 0x842F;
            mapDataStd [rangeNo] [0xDFCB - rangeMin] = 0x8442;
            mapDataStd [rangeNo] [0xDFCC - rangeMin] = 0x842D;
            mapDataStd [rangeNo] [0xDFCD - rangeMin] = 0x845F;
            mapDataStd [rangeNo] [0xDFCE - rangeMin] = 0x8470;
            mapDataStd [rangeNo] [0xDFCF - rangeMin] = 0x8439;

            mapDataStd [rangeNo] [0xDFD0 - rangeMin] = 0x844E;
            mapDataStd [rangeNo] [0xDFD1 - rangeMin] = 0x844C;
            mapDataStd [rangeNo] [0xDFD2 - rangeMin] = 0x8452;
            mapDataStd [rangeNo] [0xDFD3 - rangeMin] = 0x846F;
            mapDataStd [rangeNo] [0xDFD4 - rangeMin] = 0x84C5;
            mapDataStd [rangeNo] [0xDFD5 - rangeMin] = 0x848E;
            mapDataStd [rangeNo] [0xDFD6 - rangeMin] = 0x843B;
            mapDataStd [rangeNo] [0xDFD7 - rangeMin] = 0x8447;
            mapDataStd [rangeNo] [0xDFD8 - rangeMin] = 0x8436;
            mapDataStd [rangeNo] [0xDFD9 - rangeMin] = 0x8433;
            mapDataStd [rangeNo] [0xDFDA - rangeMin] = 0x8468;
            mapDataStd [rangeNo] [0xDFDB - rangeMin] = 0x847E;
            mapDataStd [rangeNo] [0xDFDC - rangeMin] = 0x8444;
            mapDataStd [rangeNo] [0xDFDD - rangeMin] = 0x842B;
            mapDataStd [rangeNo] [0xDFDE - rangeMin] = 0x8460;
            mapDataStd [rangeNo] [0xDFDF - rangeMin] = 0x8454;

            mapDataStd [rangeNo] [0xDFE0 - rangeMin] = 0x846E;
            mapDataStd [rangeNo] [0xDFE1 - rangeMin] = 0x8450;
            mapDataStd [rangeNo] [0xDFE2 - rangeMin] = 0x870B;
            mapDataStd [rangeNo] [0xDFE3 - rangeMin] = 0x8704;
            mapDataStd [rangeNo] [0xDFE4 - rangeMin] = 0x86F7;
            mapDataStd [rangeNo] [0xDFE5 - rangeMin] = 0x870C;
            mapDataStd [rangeNo] [0xDFE6 - rangeMin] = 0x86FA;
            mapDataStd [rangeNo] [0xDFE7 - rangeMin] = 0x86D6;
            mapDataStd [rangeNo] [0xDFE8 - rangeMin] = 0x86F5;
            mapDataStd [rangeNo] [0xDFE9 - rangeMin] = 0x874D;
            mapDataStd [rangeNo] [0xDFEA - rangeMin] = 0x86F8;
            mapDataStd [rangeNo] [0xDFEB - rangeMin] = 0x870E;
            mapDataStd [rangeNo] [0xDFEC - rangeMin] = 0x8709;
            mapDataStd [rangeNo] [0xDFED - rangeMin] = 0x8701;
            mapDataStd [rangeNo] [0xDFEE - rangeMin] = 0x86F6;
            mapDataStd [rangeNo] [0xDFEF - rangeMin] = 0x870D;

            mapDataStd [rangeNo] [0xDFF0 - rangeMin] = 0x8705;
            mapDataStd [rangeNo] [0xDFF1 - rangeMin] = 0x88D6;
            mapDataStd [rangeNo] [0xDFF2 - rangeMin] = 0x88CB;
            mapDataStd [rangeNo] [0xDFF3 - rangeMin] = 0x88CD;
            mapDataStd [rangeNo] [0xDFF4 - rangeMin] = 0x88CE;
            mapDataStd [rangeNo] [0xDFF5 - rangeMin] = 0x88DE;
            mapDataStd [rangeNo] [0xDFF6 - rangeMin] = 0x88DB;
            mapDataStd [rangeNo] [0xDFF7 - rangeMin] = 0x88DA;
            mapDataStd [rangeNo] [0xDFF8 - rangeMin] = 0x88CC;
            mapDataStd [rangeNo] [0xDFF9 - rangeMin] = 0x88D0;
            mapDataStd [rangeNo] [0xDFFA - rangeMin] = 0x8985;
            mapDataStd [rangeNo] [0xDFFB - rangeMin] = 0x899B;
            mapDataStd [rangeNo] [0xDFFC - rangeMin] = 0x89DF;
            mapDataStd [rangeNo] [0xDFFD - rangeMin] = 0x89E5;
            mapDataStd [rangeNo] [0xDFFE - rangeMin] = 0x89E4;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xE040 - rangeMin] = 0x89E1;
            mapDataStd [rangeNo] [0xE041 - rangeMin] = 0x89E0;
            mapDataStd [rangeNo] [0xE042 - rangeMin] = 0x89E2;
            mapDataStd [rangeNo] [0xE043 - rangeMin] = 0x89DC;
            mapDataStd [rangeNo] [0xE044 - rangeMin] = 0x89E6;
            mapDataStd [rangeNo] [0xE045 - rangeMin] = 0x8A76;
            mapDataStd [rangeNo] [0xE046 - rangeMin] = 0x8A86;
            mapDataStd [rangeNo] [0xE047 - rangeMin] = 0x8A7F;
            mapDataStd [rangeNo] [0xE048 - rangeMin] = 0x8A61;
            mapDataStd [rangeNo] [0xE049 - rangeMin] = 0x8A3F;
            mapDataStd [rangeNo] [0xE04A - rangeMin] = 0x8A77;
            mapDataStd [rangeNo] [0xE04B - rangeMin] = 0x8A82;
            mapDataStd [rangeNo] [0xE04C - rangeMin] = 0x8A84;
            mapDataStd [rangeNo] [0xE04D - rangeMin] = 0x8A75;
            mapDataStd [rangeNo] [0xE04E - rangeMin] = 0x8A83;
            mapDataStd [rangeNo] [0xE04F - rangeMin] = 0x8A81;

            mapDataStd [rangeNo] [0xE050 - rangeMin] = 0x8A74;
            mapDataStd [rangeNo] [0xE051 - rangeMin] = 0x8A7A;
            mapDataStd [rangeNo] [0xE052 - rangeMin] = 0x8C3C;
            mapDataStd [rangeNo] [0xE053 - rangeMin] = 0x8C4B;
            mapDataStd [rangeNo] [0xE054 - rangeMin] = 0x8C4A;
            mapDataStd [rangeNo] [0xE055 - rangeMin] = 0x8C65;
            mapDataStd [rangeNo] [0xE056 - rangeMin] = 0x8C64;
            mapDataStd [rangeNo] [0xE057 - rangeMin] = 0x8C66;
            mapDataStd [rangeNo] [0xE058 - rangeMin] = 0x8C86;
            mapDataStd [rangeNo] [0xE059 - rangeMin] = 0x8C84;
            mapDataStd [rangeNo] [0xE05A - rangeMin] = 0x8C85;
            mapDataStd [rangeNo] [0xE05B - rangeMin] = 0x8CCC;
            mapDataStd [rangeNo] [0xE05C - rangeMin] = 0x8D68;
            mapDataStd [rangeNo] [0xE05D - rangeMin] = 0x8D69;
            mapDataStd [rangeNo] [0xE05E - rangeMin] = 0x8D91;
            mapDataStd [rangeNo] [0xE05F - rangeMin] = 0x8D8C;

            mapDataStd [rangeNo] [0xE060 - rangeMin] = 0x8D8E;
            mapDataStd [rangeNo] [0xE061 - rangeMin] = 0x8D8F;
            mapDataStd [rangeNo] [0xE062 - rangeMin] = 0x8D8D;
            mapDataStd [rangeNo] [0xE063 - rangeMin] = 0x8D93;
            mapDataStd [rangeNo] [0xE064 - rangeMin] = 0x8D94;
            mapDataStd [rangeNo] [0xE065 - rangeMin] = 0x8D90;
            mapDataStd [rangeNo] [0xE066 - rangeMin] = 0x8D92;
            mapDataStd [rangeNo] [0xE067 - rangeMin] = 0x8DF0;
            mapDataStd [rangeNo] [0xE068 - rangeMin] = 0x8DE0;
            mapDataStd [rangeNo] [0xE069 - rangeMin] = 0x8DEC;
            mapDataStd [rangeNo] [0xE06A - rangeMin] = 0x8DF1;
            mapDataStd [rangeNo] [0xE06B - rangeMin] = 0x8DEE;
            mapDataStd [rangeNo] [0xE06C - rangeMin] = 0x8DD0;
            mapDataStd [rangeNo] [0xE06D - rangeMin] = 0x8DE9;
            mapDataStd [rangeNo] [0xE06E - rangeMin] = 0x8DE3;
            mapDataStd [rangeNo] [0xE06F - rangeMin] = 0x8DE2;

            mapDataStd [rangeNo] [0xE070 - rangeMin] = 0x8DE7;
            mapDataStd [rangeNo] [0xE071 - rangeMin] = 0x8DF2;
            mapDataStd [rangeNo] [0xE072 - rangeMin] = 0x8DEB;
            mapDataStd [rangeNo] [0xE073 - rangeMin] = 0x8DF4;
            mapDataStd [rangeNo] [0xE074 - rangeMin] = 0x8F06;
            mapDataStd [rangeNo] [0xE075 - rangeMin] = 0x8EFF;
            mapDataStd [rangeNo] [0xE076 - rangeMin] = 0x8F01;
            mapDataStd [rangeNo] [0xE077 - rangeMin] = 0x8F00;
            mapDataStd [rangeNo] [0xE078 - rangeMin] = 0x8F05;
            mapDataStd [rangeNo] [0xE079 - rangeMin] = 0x8F07;
            mapDataStd [rangeNo] [0xE07A - rangeMin] = 0x8F08;
            mapDataStd [rangeNo] [0xE07B - rangeMin] = 0x8F02;
            mapDataStd [rangeNo] [0xE07C - rangeMin] = 0x8F0B;
            mapDataStd [rangeNo] [0xE07D - rangeMin] = 0x9052;
            mapDataStd [rangeNo] [0xE07E - rangeMin] = 0x903F;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xE0A1 - rangeMin] = 0x9044;
            mapDataStd [rangeNo] [0xE0A2 - rangeMin] = 0x9049;
            mapDataStd [rangeNo] [0xE0A3 - rangeMin] = 0x903D;
            mapDataStd [rangeNo] [0xE0A4 - rangeMin] = 0x9110;
            mapDataStd [rangeNo] [0xE0A5 - rangeMin] = 0x910D;
            mapDataStd [rangeNo] [0xE0A6 - rangeMin] = 0x910F;
            mapDataStd [rangeNo] [0xE0A7 - rangeMin] = 0x9111;
            mapDataStd [rangeNo] [0xE0A8 - rangeMin] = 0x9116;
            mapDataStd [rangeNo] [0xE0A9 - rangeMin] = 0x9114;
            mapDataStd [rangeNo] [0xE0AA - rangeMin] = 0x910B;
            mapDataStd [rangeNo] [0xE0AB - rangeMin] = 0x910E;
            mapDataStd [rangeNo] [0xE0AC - rangeMin] = 0x916E;
            mapDataStd [rangeNo] [0xE0AD - rangeMin] = 0x916F;
            mapDataStd [rangeNo] [0xE0AE - rangeMin] = 0x9248;
            mapDataStd [rangeNo] [0xE0AF - rangeMin] = 0x9252;

            mapDataStd [rangeNo] [0xE0B0 - rangeMin] = 0x9230;
            mapDataStd [rangeNo] [0xE0B1 - rangeMin] = 0x923A;
            mapDataStd [rangeNo] [0xE0B2 - rangeMin] = 0x9266;
            mapDataStd [rangeNo] [0xE0B3 - rangeMin] = 0x9233;
            mapDataStd [rangeNo] [0xE0B4 - rangeMin] = 0x9265;
            mapDataStd [rangeNo] [0xE0B5 - rangeMin] = 0x925E;
            mapDataStd [rangeNo] [0xE0B6 - rangeMin] = 0x9283;
            mapDataStd [rangeNo] [0xE0B7 - rangeMin] = 0x922E;
            mapDataStd [rangeNo] [0xE0B8 - rangeMin] = 0x924A;
            mapDataStd [rangeNo] [0xE0B9 - rangeMin] = 0x9246;
            mapDataStd [rangeNo] [0xE0BA - rangeMin] = 0x926D;
            mapDataStd [rangeNo] [0xE0BB - rangeMin] = 0x926C;
            mapDataStd [rangeNo] [0xE0BC - rangeMin] = 0x924F;
            mapDataStd [rangeNo] [0xE0BD - rangeMin] = 0x9260;
            mapDataStd [rangeNo] [0xE0BE - rangeMin] = 0x9267;
            mapDataStd [rangeNo] [0xE0BF - rangeMin] = 0x926F;

            mapDataStd [rangeNo] [0xE0C0 - rangeMin] = 0x9236;
            mapDataStd [rangeNo] [0xE0C1 - rangeMin] = 0x9261;
            mapDataStd [rangeNo] [0xE0C2 - rangeMin] = 0x9270;
            mapDataStd [rangeNo] [0xE0C3 - rangeMin] = 0x9231;
            mapDataStd [rangeNo] [0xE0C4 - rangeMin] = 0x9254;
            mapDataStd [rangeNo] [0xE0C5 - rangeMin] = 0x9263;
            mapDataStd [rangeNo] [0xE0C6 - rangeMin] = 0x9250;
            mapDataStd [rangeNo] [0xE0C7 - rangeMin] = 0x9272;
            mapDataStd [rangeNo] [0xE0C8 - rangeMin] = 0x924E;
            mapDataStd [rangeNo] [0xE0C9 - rangeMin] = 0x9253;
            mapDataStd [rangeNo] [0xE0CA - rangeMin] = 0x924C;
            mapDataStd [rangeNo] [0xE0CB - rangeMin] = 0x9256;
            mapDataStd [rangeNo] [0xE0CC - rangeMin] = 0x9232;
            mapDataStd [rangeNo] [0xE0CD - rangeMin] = 0x959F;
            mapDataStd [rangeNo] [0xE0CE - rangeMin] = 0x959C;
            mapDataStd [rangeNo] [0xE0CF - rangeMin] = 0x959E;

            mapDataStd [rangeNo] [0xE0D0 - rangeMin] = 0x959B;
            mapDataStd [rangeNo] [0xE0D1 - rangeMin] = 0x9692;
            mapDataStd [rangeNo] [0xE0D2 - rangeMin] = 0x9693;
            mapDataStd [rangeNo] [0xE0D3 - rangeMin] = 0x9691;
            mapDataStd [rangeNo] [0xE0D4 - rangeMin] = 0x9697;
            mapDataStd [rangeNo] [0xE0D5 - rangeMin] = 0x96CE;
            mapDataStd [rangeNo] [0xE0D6 - rangeMin] = 0x96FA;
            mapDataStd [rangeNo] [0xE0D7 - rangeMin] = 0x96FD;
            mapDataStd [rangeNo] [0xE0D8 - rangeMin] = 0x96F8;
            mapDataStd [rangeNo] [0xE0D9 - rangeMin] = 0x96F5;
            mapDataStd [rangeNo] [0xE0DA - rangeMin] = 0x9773;
            mapDataStd [rangeNo] [0xE0DB - rangeMin] = 0x9777;
            mapDataStd [rangeNo] [0xE0DC - rangeMin] = 0x9778;
            mapDataStd [rangeNo] [0xE0DD - rangeMin] = 0x9772;
            mapDataStd [rangeNo] [0xE0DE - rangeMin] = 0x980F;
            mapDataStd [rangeNo] [0xE0DF - rangeMin] = 0x980D;

            mapDataStd [rangeNo] [0xE0E0 - rangeMin] = 0x980E;
            mapDataStd [rangeNo] [0xE0E1 - rangeMin] = 0x98AC;
            mapDataStd [rangeNo] [0xE0E2 - rangeMin] = 0x98F6;
            mapDataStd [rangeNo] [0xE0E3 - rangeMin] = 0x98F9;
            mapDataStd [rangeNo] [0xE0E4 - rangeMin] = 0x99AF;
            mapDataStd [rangeNo] [0xE0E5 - rangeMin] = 0x99B2;
            mapDataStd [rangeNo] [0xE0E6 - rangeMin] = 0x99B0;
            mapDataStd [rangeNo] [0xE0E7 - rangeMin] = 0x99B5;
            mapDataStd [rangeNo] [0xE0E8 - rangeMin] = 0x9AAD;
            mapDataStd [rangeNo] [0xE0E9 - rangeMin] = 0x9AAB;
            mapDataStd [rangeNo] [0xE0EA - rangeMin] = 0x9B5B;
            mapDataStd [rangeNo] [0xE0EB - rangeMin] = 0x9CEA;
            mapDataStd [rangeNo] [0xE0EC - rangeMin] = 0x9CED;
            mapDataStd [rangeNo] [0xE0ED - rangeMin] = 0x9CE7;
            mapDataStd [rangeNo] [0xE0EE - rangeMin] = 0x9E80;
            mapDataStd [rangeNo] [0xE0EF - rangeMin] = 0x9EFD;

            mapDataStd [rangeNo] [0xE0F0 - rangeMin] = 0x50E6;
            mapDataStd [rangeNo] [0xE0F1 - rangeMin] = 0x50D4;
            mapDataStd [rangeNo] [0xE0F2 - rangeMin] = 0x50D7;
            mapDataStd [rangeNo] [0xE0F3 - rangeMin] = 0x50E8;
            mapDataStd [rangeNo] [0xE0F4 - rangeMin] = 0x50F3;
            mapDataStd [rangeNo] [0xE0F5 - rangeMin] = 0x50DB;
            mapDataStd [rangeNo] [0xE0F6 - rangeMin] = 0x50EA;
            mapDataStd [rangeNo] [0xE0F7 - rangeMin] = 0x50DD;
            mapDataStd [rangeNo] [0xE0F8 - rangeMin] = 0x50E4;
            mapDataStd [rangeNo] [0xE0F9 - rangeMin] = 0x50D3;
            mapDataStd [rangeNo] [0xE0FA - rangeMin] = 0x50EC;
            mapDataStd [rangeNo] [0xE0FB - rangeMin] = 0x50F0;
            mapDataStd [rangeNo] [0xE0FC - rangeMin] = 0x50EF;
            mapDataStd [rangeNo] [0xE0FD - rangeMin] = 0x50E3;
            mapDataStd [rangeNo] [0xE0FE - rangeMin] = 0x50E0;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xE140 - rangeMin] = 0x51D8;
            mapDataStd [rangeNo] [0xE141 - rangeMin] = 0x5280;
            mapDataStd [rangeNo] [0xE142 - rangeMin] = 0x5281;
            mapDataStd [rangeNo] [0xE143 - rangeMin] = 0x52E9;
            mapDataStd [rangeNo] [0xE144 - rangeMin] = 0x52EB;
            mapDataStd [rangeNo] [0xE145 - rangeMin] = 0x5330;
            mapDataStd [rangeNo] [0xE146 - rangeMin] = 0x53AC;
            mapDataStd [rangeNo] [0xE147 - rangeMin] = 0x5627;
            mapDataStd [rangeNo] [0xE148 - rangeMin] = 0x5615;
            mapDataStd [rangeNo] [0xE149 - rangeMin] = 0x560C;
            mapDataStd [rangeNo] [0xE14A - rangeMin] = 0x5612;
            mapDataStd [rangeNo] [0xE14B - rangeMin] = 0x55FC;
            mapDataStd [rangeNo] [0xE14C - rangeMin] = 0x560F;
            mapDataStd [rangeNo] [0xE14D - rangeMin] = 0x561C;
            mapDataStd [rangeNo] [0xE14E - rangeMin] = 0x5601;
            mapDataStd [rangeNo] [0xE14F - rangeMin] = 0x5613;

            mapDataStd [rangeNo] [0xE150 - rangeMin] = 0x5602;
            mapDataStd [rangeNo] [0xE151 - rangeMin] = 0x55FA;
            mapDataStd [rangeNo] [0xE152 - rangeMin] = 0x561D;
            mapDataStd [rangeNo] [0xE153 - rangeMin] = 0x5604;
            mapDataStd [rangeNo] [0xE154 - rangeMin] = 0x55FF;
            mapDataStd [rangeNo] [0xE155 - rangeMin] = 0x55F9;
            mapDataStd [rangeNo] [0xE156 - rangeMin] = 0x5889;
            mapDataStd [rangeNo] [0xE157 - rangeMin] = 0x587C;
            mapDataStd [rangeNo] [0xE158 - rangeMin] = 0x5890;
            mapDataStd [rangeNo] [0xE159 - rangeMin] = 0x5898;
            mapDataStd [rangeNo] [0xE15A - rangeMin] = 0x5886;
            mapDataStd [rangeNo] [0xE15B - rangeMin] = 0x5881;
            mapDataStd [rangeNo] [0xE15C - rangeMin] = 0x587F;
            mapDataStd [rangeNo] [0xE15D - rangeMin] = 0x5874;
            mapDataStd [rangeNo] [0xE15E - rangeMin] = 0x588B;
            mapDataStd [rangeNo] [0xE15F - rangeMin] = 0x587A;

            mapDataStd [rangeNo] [0xE160 - rangeMin] = 0x5887;
            mapDataStd [rangeNo] [0xE161 - rangeMin] = 0x5891;
            mapDataStd [rangeNo] [0xE162 - rangeMin] = 0x588E;
            mapDataStd [rangeNo] [0xE163 - rangeMin] = 0x5876;
            mapDataStd [rangeNo] [0xE164 - rangeMin] = 0x5882;
            mapDataStd [rangeNo] [0xE165 - rangeMin] = 0x5888;
            mapDataStd [rangeNo] [0xE166 - rangeMin] = 0x587B;
            mapDataStd [rangeNo] [0xE167 - rangeMin] = 0x5894;
            mapDataStd [rangeNo] [0xE168 - rangeMin] = 0x588F;
            mapDataStd [rangeNo] [0xE169 - rangeMin] = 0x58FE;
            mapDataStd [rangeNo] [0xE16A - rangeMin] = 0x596B;
            mapDataStd [rangeNo] [0xE16B - rangeMin] = 0x5ADC;
            mapDataStd [rangeNo] [0xE16C - rangeMin] = 0x5AEE;
            mapDataStd [rangeNo] [0xE16D - rangeMin] = 0x5AE5;
            mapDataStd [rangeNo] [0xE16E - rangeMin] = 0x5AD5;
            mapDataStd [rangeNo] [0xE16F - rangeMin] = 0x5AEA;

            mapDataStd [rangeNo] [0xE170 - rangeMin] = 0x5ADA;
            mapDataStd [rangeNo] [0xE171 - rangeMin] = 0x5AED;
            mapDataStd [rangeNo] [0xE172 - rangeMin] = 0x5AEB;
            mapDataStd [rangeNo] [0xE173 - rangeMin] = 0x5AF3;
            mapDataStd [rangeNo] [0xE174 - rangeMin] = 0x5AE2;
            mapDataStd [rangeNo] [0xE175 - rangeMin] = 0x5AE0;
            mapDataStd [rangeNo] [0xE176 - rangeMin] = 0x5ADB;
            mapDataStd [rangeNo] [0xE177 - rangeMin] = 0x5AEC;
            mapDataStd [rangeNo] [0xE178 - rangeMin] = 0x5ADE;
            mapDataStd [rangeNo] [0xE179 - rangeMin] = 0x5ADD;
            mapDataStd [rangeNo] [0xE17A - rangeMin] = 0x5AD9;
            mapDataStd [rangeNo] [0xE17B - rangeMin] = 0x5AE8;
            mapDataStd [rangeNo] [0xE17C - rangeMin] = 0x5ADF;
            mapDataStd [rangeNo] [0xE17D - rangeMin] = 0x5B77;
            mapDataStd [rangeNo] [0xE17E - rangeMin] = 0x5BE0;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xE1A1 - rangeMin] = 0x5BE3;
            mapDataStd [rangeNo] [0xE1A2 - rangeMin] = 0x5C63;
            mapDataStd [rangeNo] [0xE1A3 - rangeMin] = 0x5D82;
            mapDataStd [rangeNo] [0xE1A4 - rangeMin] = 0x5D80;
            mapDataStd [rangeNo] [0xE1A5 - rangeMin] = 0x5D7D;
            mapDataStd [rangeNo] [0xE1A6 - rangeMin] = 0x5D86;
            mapDataStd [rangeNo] [0xE1A7 - rangeMin] = 0x5D7A;
            mapDataStd [rangeNo] [0xE1A8 - rangeMin] = 0x5D81;
            mapDataStd [rangeNo] [0xE1A9 - rangeMin] = 0x5D77;
            mapDataStd [rangeNo] [0xE1AA - rangeMin] = 0x5D8A;
            mapDataStd [rangeNo] [0xE1AB - rangeMin] = 0x5D89;
            mapDataStd [rangeNo] [0xE1AC - rangeMin] = 0x5D88;
            mapDataStd [rangeNo] [0xE1AD - rangeMin] = 0x5D7E;
            mapDataStd [rangeNo] [0xE1AE - rangeMin] = 0x5D7C;
            mapDataStd [rangeNo] [0xE1AF - rangeMin] = 0x5D8D;

            mapDataStd [rangeNo] [0xE1B0 - rangeMin] = 0x5D79;
            mapDataStd [rangeNo] [0xE1B1 - rangeMin] = 0x5D7F;
            mapDataStd [rangeNo] [0xE1B2 - rangeMin] = 0x5E58;
            mapDataStd [rangeNo] [0xE1B3 - rangeMin] = 0x5E59;
            mapDataStd [rangeNo] [0xE1B4 - rangeMin] = 0x5E53;
            mapDataStd [rangeNo] [0xE1B5 - rangeMin] = 0x5ED8;
            mapDataStd [rangeNo] [0xE1B6 - rangeMin] = 0x5ED1;
            mapDataStd [rangeNo] [0xE1B7 - rangeMin] = 0x5ED7;
            mapDataStd [rangeNo] [0xE1B8 - rangeMin] = 0x5ECE;
            mapDataStd [rangeNo] [0xE1B9 - rangeMin] = 0x5EDC;
            mapDataStd [rangeNo] [0xE1BA - rangeMin] = 0x5ED5;
            mapDataStd [rangeNo] [0xE1BB - rangeMin] = 0x5ED9;
            mapDataStd [rangeNo] [0xE1BC - rangeMin] = 0x5ED2;
            mapDataStd [rangeNo] [0xE1BD - rangeMin] = 0x5ED4;
            mapDataStd [rangeNo] [0xE1BE - rangeMin] = 0x5F44;
            mapDataStd [rangeNo] [0xE1BF - rangeMin] = 0x5F43;

            mapDataStd [rangeNo] [0xE1C0 - rangeMin] = 0x5F6F;
            mapDataStd [rangeNo] [0xE1C1 - rangeMin] = 0x5FB6;
            mapDataStd [rangeNo] [0xE1C2 - rangeMin] = 0x612C;
            mapDataStd [rangeNo] [0xE1C3 - rangeMin] = 0x6128;
            mapDataStd [rangeNo] [0xE1C4 - rangeMin] = 0x6141;
            mapDataStd [rangeNo] [0xE1C5 - rangeMin] = 0x615E;
            mapDataStd [rangeNo] [0xE1C6 - rangeMin] = 0x6171;
            mapDataStd [rangeNo] [0xE1C7 - rangeMin] = 0x6173;
            mapDataStd [rangeNo] [0xE1C8 - rangeMin] = 0x6152;
            mapDataStd [rangeNo] [0xE1C9 - rangeMin] = 0x6153;
            mapDataStd [rangeNo] [0xE1CA - rangeMin] = 0x6172;
            mapDataStd [rangeNo] [0xE1CB - rangeMin] = 0x616C;
            mapDataStd [rangeNo] [0xE1CC - rangeMin] = 0x6180;
            mapDataStd [rangeNo] [0xE1CD - rangeMin] = 0x6174;
            mapDataStd [rangeNo] [0xE1CE - rangeMin] = 0x6154;
            mapDataStd [rangeNo] [0xE1CF - rangeMin] = 0x617A;

            mapDataStd [rangeNo] [0xE1D0 - rangeMin] = 0x615B;
            mapDataStd [rangeNo] [0xE1D1 - rangeMin] = 0x6165;
            mapDataStd [rangeNo] [0xE1D2 - rangeMin] = 0x613B;
            mapDataStd [rangeNo] [0xE1D3 - rangeMin] = 0x616A;
            mapDataStd [rangeNo] [0xE1D4 - rangeMin] = 0x6161;
            mapDataStd [rangeNo] [0xE1D5 - rangeMin] = 0x6156;
            mapDataStd [rangeNo] [0xE1D6 - rangeMin] = 0x6229;
            mapDataStd [rangeNo] [0xE1D7 - rangeMin] = 0x6227;
            mapDataStd [rangeNo] [0xE1D8 - rangeMin] = 0x622B;
            mapDataStd [rangeNo] [0xE1D9 - rangeMin] = 0x642B;
            mapDataStd [rangeNo] [0xE1DA - rangeMin] = 0x644D;
            mapDataStd [rangeNo] [0xE1DB - rangeMin] = 0x645B;
            mapDataStd [rangeNo] [0xE1DC - rangeMin] = 0x645D;
            mapDataStd [rangeNo] [0xE1DD - rangeMin] = 0x6474;
            mapDataStd [rangeNo] [0xE1DE - rangeMin] = 0x6476;
            mapDataStd [rangeNo] [0xE1DF - rangeMin] = 0x6472;

            mapDataStd [rangeNo] [0xE1E0 - rangeMin] = 0x6473;
            mapDataStd [rangeNo] [0xE1E1 - rangeMin] = 0x647D;
            mapDataStd [rangeNo] [0xE1E2 - rangeMin] = 0x6475;
            mapDataStd [rangeNo] [0xE1E3 - rangeMin] = 0x6466;
            mapDataStd [rangeNo] [0xE1E4 - rangeMin] = 0x64A6;
            mapDataStd [rangeNo] [0xE1E5 - rangeMin] = 0x644E;
            mapDataStd [rangeNo] [0xE1E6 - rangeMin] = 0x6482;
            mapDataStd [rangeNo] [0xE1E7 - rangeMin] = 0x645E;
            mapDataStd [rangeNo] [0xE1E8 - rangeMin] = 0x645C;
            mapDataStd [rangeNo] [0xE1E9 - rangeMin] = 0x644B;
            mapDataStd [rangeNo] [0xE1EA - rangeMin] = 0x6453;
            mapDataStd [rangeNo] [0xE1EB - rangeMin] = 0x6460;
            mapDataStd [rangeNo] [0xE1EC - rangeMin] = 0x6450;
            mapDataStd [rangeNo] [0xE1ED - rangeMin] = 0x647F;
            mapDataStd [rangeNo] [0xE1EE - rangeMin] = 0x643F;
            mapDataStd [rangeNo] [0xE1EF - rangeMin] = 0x646C;

            mapDataStd [rangeNo] [0xE1F0 - rangeMin] = 0x646B;
            mapDataStd [rangeNo] [0xE1F1 - rangeMin] = 0x6459;
            mapDataStd [rangeNo] [0xE1F2 - rangeMin] = 0x6465;
            mapDataStd [rangeNo] [0xE1F3 - rangeMin] = 0x6477;
            mapDataStd [rangeNo] [0xE1F4 - rangeMin] = 0x6573;
            mapDataStd [rangeNo] [0xE1F5 - rangeMin] = 0x65A0;
            mapDataStd [rangeNo] [0xE1F6 - rangeMin] = 0x66A1;
            mapDataStd [rangeNo] [0xE1F7 - rangeMin] = 0x66A0;
            mapDataStd [rangeNo] [0xE1F8 - rangeMin] = 0x669F;
            mapDataStd [rangeNo] [0xE1F9 - rangeMin] = 0x6705;
            mapDataStd [rangeNo] [0xE1FA - rangeMin] = 0x6704;
            mapDataStd [rangeNo] [0xE1FB - rangeMin] = 0x6722;
            mapDataStd [rangeNo] [0xE1FC - rangeMin] = 0x69B1;
            mapDataStd [rangeNo] [0xE1FD - rangeMin] = 0x69B6;
            mapDataStd [rangeNo] [0xE1FE - rangeMin] = 0x69C9;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xE240 - rangeMin] = 0x69A0;
            mapDataStd [rangeNo] [0xE241 - rangeMin] = 0x69CE;
            mapDataStd [rangeNo] [0xE242 - rangeMin] = 0x6996;
            mapDataStd [rangeNo] [0xE243 - rangeMin] = 0x69B0;
            mapDataStd [rangeNo] [0xE244 - rangeMin] = 0x69AC;
            mapDataStd [rangeNo] [0xE245 - rangeMin] = 0x69BC;
            mapDataStd [rangeNo] [0xE246 - rangeMin] = 0x6991;
            mapDataStd [rangeNo] [0xE247 - rangeMin] = 0x6999;
            mapDataStd [rangeNo] [0xE248 - rangeMin] = 0x698E;
            mapDataStd [rangeNo] [0xE249 - rangeMin] = 0x69A7;
            mapDataStd [rangeNo] [0xE24A - rangeMin] = 0x698D;
            mapDataStd [rangeNo] [0xE24B - rangeMin] = 0x69A9;
            mapDataStd [rangeNo] [0xE24C - rangeMin] = 0x69BE;
            mapDataStd [rangeNo] [0xE24D - rangeMin] = 0x69AF;
            mapDataStd [rangeNo] [0xE24E - rangeMin] = 0x69BF;
            mapDataStd [rangeNo] [0xE24F - rangeMin] = 0x69C4;

            mapDataStd [rangeNo] [0xE250 - rangeMin] = 0x69BD;
            mapDataStd [rangeNo] [0xE251 - rangeMin] = 0x69A4;
            mapDataStd [rangeNo] [0xE252 - rangeMin] = 0x69D4;
            mapDataStd [rangeNo] [0xE253 - rangeMin] = 0x69B9;
            mapDataStd [rangeNo] [0xE254 - rangeMin] = 0x69CA;
            mapDataStd [rangeNo] [0xE255 - rangeMin] = 0x699A;
            mapDataStd [rangeNo] [0xE256 - rangeMin] = 0x69CF;
            mapDataStd [rangeNo] [0xE257 - rangeMin] = 0x69B3;
            mapDataStd [rangeNo] [0xE258 - rangeMin] = 0x6993;
            mapDataStd [rangeNo] [0xE259 - rangeMin] = 0x69AA;
            mapDataStd [rangeNo] [0xE25A - rangeMin] = 0x69A1;
            mapDataStd [rangeNo] [0xE25B - rangeMin] = 0x699E;
            mapDataStd [rangeNo] [0xE25C - rangeMin] = 0x69D9;
            mapDataStd [rangeNo] [0xE25D - rangeMin] = 0x6997;
            mapDataStd [rangeNo] [0xE25E - rangeMin] = 0x6990;
            mapDataStd [rangeNo] [0xE25F - rangeMin] = 0x69C2;

            mapDataStd [rangeNo] [0xE260 - rangeMin] = 0x69B5;
            mapDataStd [rangeNo] [0xE261 - rangeMin] = 0x69A5;
            mapDataStd [rangeNo] [0xE262 - rangeMin] = 0x69C6;
            mapDataStd [rangeNo] [0xE263 - rangeMin] = 0x6B4A;
            mapDataStd [rangeNo] [0xE264 - rangeMin] = 0x6B4D;
            mapDataStd [rangeNo] [0xE265 - rangeMin] = 0x6B4B;
            mapDataStd [rangeNo] [0xE266 - rangeMin] = 0x6B9E;
            mapDataStd [rangeNo] [0xE267 - rangeMin] = 0x6B9F;
            mapDataStd [rangeNo] [0xE268 - rangeMin] = 0x6BA0;
            mapDataStd [rangeNo] [0xE269 - rangeMin] = 0x6BC3;
            mapDataStd [rangeNo] [0xE26A - rangeMin] = 0x6BC4;
            mapDataStd [rangeNo] [0xE26B - rangeMin] = 0x6BFE;
            mapDataStd [rangeNo] [0xE26C - rangeMin] = 0x6ECE;
            mapDataStd [rangeNo] [0xE26D - rangeMin] = 0x6EF5;
            mapDataStd [rangeNo] [0xE26E - rangeMin] = 0x6EF1;
            mapDataStd [rangeNo] [0xE26F - rangeMin] = 0x6F03;

            mapDataStd [rangeNo] [0xE270 - rangeMin] = 0x6F25;
            mapDataStd [rangeNo] [0xE271 - rangeMin] = 0x6EF8;
            mapDataStd [rangeNo] [0xE272 - rangeMin] = 0x6F37;
            mapDataStd [rangeNo] [0xE273 - rangeMin] = 0x6EFB;
            mapDataStd [rangeNo] [0xE274 - rangeMin] = 0x6F2E;
            mapDataStd [rangeNo] [0xE275 - rangeMin] = 0x6F09;
            mapDataStd [rangeNo] [0xE276 - rangeMin] = 0x6F4E;
            mapDataStd [rangeNo] [0xE277 - rangeMin] = 0x6F19;
            mapDataStd [rangeNo] [0xE278 - rangeMin] = 0x6F1A;
            mapDataStd [rangeNo] [0xE279 - rangeMin] = 0x6F27;
            mapDataStd [rangeNo] [0xE27A - rangeMin] = 0x6F18;
            mapDataStd [rangeNo] [0xE27B - rangeMin] = 0x6F3B;
            mapDataStd [rangeNo] [0xE27C - rangeMin] = 0x6F12;
            mapDataStd [rangeNo] [0xE27D - rangeMin] = 0x6EED;
            mapDataStd [rangeNo] [0xE27E - rangeMin] = 0x6F0A;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xE2A1 - rangeMin] = 0x6F36;
            mapDataStd [rangeNo] [0xE2A2 - rangeMin] = 0x6F73;
            mapDataStd [rangeNo] [0xE2A3 - rangeMin] = 0x6EF9;
            mapDataStd [rangeNo] [0xE2A4 - rangeMin] = 0x6EEE;
            mapDataStd [rangeNo] [0xE2A5 - rangeMin] = 0x6F2D;
            mapDataStd [rangeNo] [0xE2A6 - rangeMin] = 0x6F40;
            mapDataStd [rangeNo] [0xE2A7 - rangeMin] = 0x6F30;
            mapDataStd [rangeNo] [0xE2A8 - rangeMin] = 0x6F3C;
            mapDataStd [rangeNo] [0xE2A9 - rangeMin] = 0x6F35;
            mapDataStd [rangeNo] [0xE2AA - rangeMin] = 0x6EEB;
            mapDataStd [rangeNo] [0xE2AB - rangeMin] = 0x6F07;
            mapDataStd [rangeNo] [0xE2AC - rangeMin] = 0x6F0E;
            mapDataStd [rangeNo] [0xE2AD - rangeMin] = 0x6F43;
            mapDataStd [rangeNo] [0xE2AE - rangeMin] = 0x6F05;
            mapDataStd [rangeNo] [0xE2AF - rangeMin] = 0x6EFD;

            mapDataStd [rangeNo] [0xE2B0 - rangeMin] = 0x6EF6;
            mapDataStd [rangeNo] [0xE2B1 - rangeMin] = 0x6F39;
            mapDataStd [rangeNo] [0xE2B2 - rangeMin] = 0x6F1C;
            mapDataStd [rangeNo] [0xE2B3 - rangeMin] = 0x6EFC;
            mapDataStd [rangeNo] [0xE2B4 - rangeMin] = 0x6F3A;
            mapDataStd [rangeNo] [0xE2B5 - rangeMin] = 0x6F1F;
            mapDataStd [rangeNo] [0xE2B6 - rangeMin] = 0x6F0D;
            mapDataStd [rangeNo] [0xE2B7 - rangeMin] = 0x6F1E;
            mapDataStd [rangeNo] [0xE2B8 - rangeMin] = 0x6F08;
            mapDataStd [rangeNo] [0xE2B9 - rangeMin] = 0x6F21;
            mapDataStd [rangeNo] [0xE2BA - rangeMin] = 0x7187;
            mapDataStd [rangeNo] [0xE2BB - rangeMin] = 0x7190;
            mapDataStd [rangeNo] [0xE2BC - rangeMin] = 0x7189;
            mapDataStd [rangeNo] [0xE2BD - rangeMin] = 0x7180;
            mapDataStd [rangeNo] [0xE2BE - rangeMin] = 0x7185;
            mapDataStd [rangeNo] [0xE2BF - rangeMin] = 0x7182;

            mapDataStd [rangeNo] [0xE2C0 - rangeMin] = 0x718F;
            mapDataStd [rangeNo] [0xE2C1 - rangeMin] = 0x717B;
            mapDataStd [rangeNo] [0xE2C2 - rangeMin] = 0x7186;
            mapDataStd [rangeNo] [0xE2C3 - rangeMin] = 0x7181;
            mapDataStd [rangeNo] [0xE2C4 - rangeMin] = 0x7197;
            mapDataStd [rangeNo] [0xE2C5 - rangeMin] = 0x7244;
            mapDataStd [rangeNo] [0xE2C6 - rangeMin] = 0x7253;
            mapDataStd [rangeNo] [0xE2C7 - rangeMin] = 0x7297;
            mapDataStd [rangeNo] [0xE2C8 - rangeMin] = 0x7295;
            mapDataStd [rangeNo] [0xE2C9 - rangeMin] = 0x7293;
            mapDataStd [rangeNo] [0xE2CA - rangeMin] = 0x7343;
            mapDataStd [rangeNo] [0xE2CB - rangeMin] = 0x734D;
            mapDataStd [rangeNo] [0xE2CC - rangeMin] = 0x7351;
            mapDataStd [rangeNo] [0xE2CD - rangeMin] = 0x734C;
            mapDataStd [rangeNo] [0xE2CE - rangeMin] = 0x7462;
            mapDataStd [rangeNo] [0xE2CF - rangeMin] = 0x7473;

            mapDataStd [rangeNo] [0xE2D0 - rangeMin] = 0x7471;
            mapDataStd [rangeNo] [0xE2D1 - rangeMin] = 0x7475;
            mapDataStd [rangeNo] [0xE2D2 - rangeMin] = 0x7472;
            mapDataStd [rangeNo] [0xE2D3 - rangeMin] = 0x7467;
            mapDataStd [rangeNo] [0xE2D4 - rangeMin] = 0x746E;
            mapDataStd [rangeNo] [0xE2D5 - rangeMin] = 0x7500;
            mapDataStd [rangeNo] [0xE2D6 - rangeMin] = 0x7502;
            mapDataStd [rangeNo] [0xE2D7 - rangeMin] = 0x7503;
            mapDataStd [rangeNo] [0xE2D8 - rangeMin] = 0x757D;
            mapDataStd [rangeNo] [0xE2D9 - rangeMin] = 0x7590;
            mapDataStd [rangeNo] [0xE2DA - rangeMin] = 0x7616;
            mapDataStd [rangeNo] [0xE2DB - rangeMin] = 0x7608;
            mapDataStd [rangeNo] [0xE2DC - rangeMin] = 0x760C;
            mapDataStd [rangeNo] [0xE2DD - rangeMin] = 0x7615;
            mapDataStd [rangeNo] [0xE2DE - rangeMin] = 0x7611;
            mapDataStd [rangeNo] [0xE2DF - rangeMin] = 0x760A;

            mapDataStd [rangeNo] [0xE2E0 - rangeMin] = 0x7614;
            mapDataStd [rangeNo] [0xE2E1 - rangeMin] = 0x76B8;
            mapDataStd [rangeNo] [0xE2E2 - rangeMin] = 0x7781;
            mapDataStd [rangeNo] [0xE2E3 - rangeMin] = 0x777C;
            mapDataStd [rangeNo] [0xE2E4 - rangeMin] = 0x7785;
            mapDataStd [rangeNo] [0xE2E5 - rangeMin] = 0x7782;
            mapDataStd [rangeNo] [0xE2E6 - rangeMin] = 0x776E;
            mapDataStd [rangeNo] [0xE2E7 - rangeMin] = 0x7780;
            mapDataStd [rangeNo] [0xE2E8 - rangeMin] = 0x776F;
            mapDataStd [rangeNo] [0xE2E9 - rangeMin] = 0x777E;
            mapDataStd [rangeNo] [0xE2EA - rangeMin] = 0x7783;
            mapDataStd [rangeNo] [0xE2EB - rangeMin] = 0x78B2;
            mapDataStd [rangeNo] [0xE2EC - rangeMin] = 0x78AA;
            mapDataStd [rangeNo] [0xE2ED - rangeMin] = 0x78B4;
            mapDataStd [rangeNo] [0xE2EE - rangeMin] = 0x78AD;
            mapDataStd [rangeNo] [0xE2EF - rangeMin] = 0x78A8;

            mapDataStd [rangeNo] [0xE2F0 - rangeMin] = 0x787E;
            mapDataStd [rangeNo] [0xE2F1 - rangeMin] = 0x78AB;
            mapDataStd [rangeNo] [0xE2F2 - rangeMin] = 0x789E;
            mapDataStd [rangeNo] [0xE2F3 - rangeMin] = 0x78A5;
            mapDataStd [rangeNo] [0xE2F4 - rangeMin] = 0x78A0;
            mapDataStd [rangeNo] [0xE2F5 - rangeMin] = 0x78AC;
            mapDataStd [rangeNo] [0xE2F6 - rangeMin] = 0x78A2;
            mapDataStd [rangeNo] [0xE2F7 - rangeMin] = 0x78A4;
            mapDataStd [rangeNo] [0xE2F8 - rangeMin] = 0x7998;
            mapDataStd [rangeNo] [0xE2F9 - rangeMin] = 0x798A;
            mapDataStd [rangeNo] [0xE2FA - rangeMin] = 0x798B;
            mapDataStd [rangeNo] [0xE2FB - rangeMin] = 0x7996;
            mapDataStd [rangeNo] [0xE2FC - rangeMin] = 0x7995;
            mapDataStd [rangeNo] [0xE2FD - rangeMin] = 0x7994;
            mapDataStd [rangeNo] [0xE2FE - rangeMin] = 0x7993;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xE340 - rangeMin] = 0x7997;
            mapDataStd [rangeNo] [0xE341 - rangeMin] = 0x7988;
            mapDataStd [rangeNo] [0xE342 - rangeMin] = 0x7992;
            mapDataStd [rangeNo] [0xE343 - rangeMin] = 0x7990;
            mapDataStd [rangeNo] [0xE344 - rangeMin] = 0x7A2B;
            mapDataStd [rangeNo] [0xE345 - rangeMin] = 0x7A4A;
            mapDataStd [rangeNo] [0xE346 - rangeMin] = 0x7A30;
            mapDataStd [rangeNo] [0xE347 - rangeMin] = 0x7A2F;
            mapDataStd [rangeNo] [0xE348 - rangeMin] = 0x7A28;
            mapDataStd [rangeNo] [0xE349 - rangeMin] = 0x7A26;
            mapDataStd [rangeNo] [0xE34A - rangeMin] = 0x7AA8;
            mapDataStd [rangeNo] [0xE34B - rangeMin] = 0x7AAB;
            mapDataStd [rangeNo] [0xE34C - rangeMin] = 0x7AAC;
            mapDataStd [rangeNo] [0xE34D - rangeMin] = 0x7AEE;
            mapDataStd [rangeNo] [0xE34E - rangeMin] = 0x7B88;
            mapDataStd [rangeNo] [0xE34F - rangeMin] = 0x7B9C;

            mapDataStd [rangeNo] [0xE350 - rangeMin] = 0x7B8A;
            mapDataStd [rangeNo] [0xE351 - rangeMin] = 0x7B91;
            mapDataStd [rangeNo] [0xE352 - rangeMin] = 0x7B90;
            mapDataStd [rangeNo] [0xE353 - rangeMin] = 0x7B96;
            mapDataStd [rangeNo] [0xE354 - rangeMin] = 0x7B8D;
            mapDataStd [rangeNo] [0xE355 - rangeMin] = 0x7B8C;
            mapDataStd [rangeNo] [0xE356 - rangeMin] = 0x7B9B;
            mapDataStd [rangeNo] [0xE357 - rangeMin] = 0x7B8E;
            mapDataStd [rangeNo] [0xE358 - rangeMin] = 0x7B85;
            mapDataStd [rangeNo] [0xE359 - rangeMin] = 0x7B98;
            mapDataStd [rangeNo] [0xE35A - rangeMin] = 0x5284;
            mapDataStd [rangeNo] [0xE35B - rangeMin] = 0x7B99;
            mapDataStd [rangeNo] [0xE35C - rangeMin] = 0x7BA4;
            mapDataStd [rangeNo] [0xE35D - rangeMin] = 0x7B82;
            mapDataStd [rangeNo] [0xE35E - rangeMin] = 0x7CBB;
            mapDataStd [rangeNo] [0xE35F - rangeMin] = 0x7CBF;

            mapDataStd [rangeNo] [0xE360 - rangeMin] = 0x7CBC;
            mapDataStd [rangeNo] [0xE361 - rangeMin] = 0x7CBA;
            mapDataStd [rangeNo] [0xE362 - rangeMin] = 0x7DA7;
            mapDataStd [rangeNo] [0xE363 - rangeMin] = 0x7DB7;
            mapDataStd [rangeNo] [0xE364 - rangeMin] = 0x7DC2;
            mapDataStd [rangeNo] [0xE365 - rangeMin] = 0x7DA3;
            mapDataStd [rangeNo] [0xE366 - rangeMin] = 0x7DAA;
            mapDataStd [rangeNo] [0xE367 - rangeMin] = 0x7DC1;
            mapDataStd [rangeNo] [0xE368 - rangeMin] = 0x7DC0;
            mapDataStd [rangeNo] [0xE369 - rangeMin] = 0x7DC5;
            mapDataStd [rangeNo] [0xE36A - rangeMin] = 0x7D9D;
            mapDataStd [rangeNo] [0xE36B - rangeMin] = 0x7DCE;
            mapDataStd [rangeNo] [0xE36C - rangeMin] = 0x7DC4;
            mapDataStd [rangeNo] [0xE36D - rangeMin] = 0x7DC6;
            mapDataStd [rangeNo] [0xE36E - rangeMin] = 0x7DCB;
            mapDataStd [rangeNo] [0xE36F - rangeMin] = 0x7DCC;

            mapDataStd [rangeNo] [0xE370 - rangeMin] = 0x7DAF;
            mapDataStd [rangeNo] [0xE371 - rangeMin] = 0x7DB9;
            mapDataStd [rangeNo] [0xE372 - rangeMin] = 0x7D96;
            mapDataStd [rangeNo] [0xE373 - rangeMin] = 0x7DBC;
            mapDataStd [rangeNo] [0xE374 - rangeMin] = 0x7D9F;
            mapDataStd [rangeNo] [0xE375 - rangeMin] = 0x7DA6;
            mapDataStd [rangeNo] [0xE376 - rangeMin] = 0x7DAE;
            mapDataStd [rangeNo] [0xE377 - rangeMin] = 0x7DA9;
            mapDataStd [rangeNo] [0xE378 - rangeMin] = 0x7DA1;
            mapDataStd [rangeNo] [0xE379 - rangeMin] = 0x7DC9;
            mapDataStd [rangeNo] [0xE37A - rangeMin] = 0x7F73;
            mapDataStd [rangeNo] [0xE37B - rangeMin] = 0x7FE2;
            mapDataStd [rangeNo] [0xE37C - rangeMin] = 0x7FE3;
            mapDataStd [rangeNo] [0xE37D - rangeMin] = 0x7FE5;
            mapDataStd [rangeNo] [0xE37E - rangeMin] = 0x7FDE;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xE3A1 - rangeMin] = 0x8024;
            mapDataStd [rangeNo] [0xE3A2 - rangeMin] = 0x805D;
            mapDataStd [rangeNo] [0xE3A3 - rangeMin] = 0x805C;
            mapDataStd [rangeNo] [0xE3A4 - rangeMin] = 0x8189;
            mapDataStd [rangeNo] [0xE3A5 - rangeMin] = 0x8186;
            mapDataStd [rangeNo] [0xE3A6 - rangeMin] = 0x8183;
            mapDataStd [rangeNo] [0xE3A7 - rangeMin] = 0x8187;
            mapDataStd [rangeNo] [0xE3A8 - rangeMin] = 0x818D;
            mapDataStd [rangeNo] [0xE3A9 - rangeMin] = 0x818C;
            mapDataStd [rangeNo] [0xE3AA - rangeMin] = 0x818B;
            mapDataStd [rangeNo] [0xE3AB - rangeMin] = 0x8215;
            mapDataStd [rangeNo] [0xE3AC - rangeMin] = 0x8497;
            mapDataStd [rangeNo] [0xE3AD - rangeMin] = 0x84A4;
            mapDataStd [rangeNo] [0xE3AE - rangeMin] = 0x84A1;
            mapDataStd [rangeNo] [0xE3AF - rangeMin] = 0x849F;

            mapDataStd [rangeNo] [0xE3B0 - rangeMin] = 0x84BA;
            mapDataStd [rangeNo] [0xE3B1 - rangeMin] = 0x84CE;
            mapDataStd [rangeNo] [0xE3B2 - rangeMin] = 0x84C2;
            mapDataStd [rangeNo] [0xE3B3 - rangeMin] = 0x84AC;
            mapDataStd [rangeNo] [0xE3B4 - rangeMin] = 0x84AE;
            mapDataStd [rangeNo] [0xE3B5 - rangeMin] = 0x84AB;
            mapDataStd [rangeNo] [0xE3B6 - rangeMin] = 0x84B9;
            mapDataStd [rangeNo] [0xE3B7 - rangeMin] = 0x84B4;
            mapDataStd [rangeNo] [0xE3B8 - rangeMin] = 0x84C1;
            mapDataStd [rangeNo] [0xE3B9 - rangeMin] = 0x84CD;
            mapDataStd [rangeNo] [0xE3BA - rangeMin] = 0x84AA;
            mapDataStd [rangeNo] [0xE3BB - rangeMin] = 0x849A;
            mapDataStd [rangeNo] [0xE3BC - rangeMin] = 0x84B1;
            mapDataStd [rangeNo] [0xE3BD - rangeMin] = 0x84D0;
            mapDataStd [rangeNo] [0xE3BE - rangeMin] = 0x849D;
            mapDataStd [rangeNo] [0xE3BF - rangeMin] = 0x84A7;

            mapDataStd [rangeNo] [0xE3C0 - rangeMin] = 0x84BB;
            mapDataStd [rangeNo] [0xE3C1 - rangeMin] = 0x84A2;
            mapDataStd [rangeNo] [0xE3C2 - rangeMin] = 0x8494;
            mapDataStd [rangeNo] [0xE3C3 - rangeMin] = 0x84C7;
            mapDataStd [rangeNo] [0xE3C4 - rangeMin] = 0x84CC;
            mapDataStd [rangeNo] [0xE3C5 - rangeMin] = 0x849B;
            mapDataStd [rangeNo] [0xE3C6 - rangeMin] = 0x84A9;
            mapDataStd [rangeNo] [0xE3C7 - rangeMin] = 0x84AF;
            mapDataStd [rangeNo] [0xE3C8 - rangeMin] = 0x84A8;
            mapDataStd [rangeNo] [0xE3C9 - rangeMin] = 0x84D6;
            mapDataStd [rangeNo] [0xE3CA - rangeMin] = 0x8498;
            mapDataStd [rangeNo] [0xE3CB - rangeMin] = 0x84B6;
            mapDataStd [rangeNo] [0xE3CC - rangeMin] = 0x84CF;
            mapDataStd [rangeNo] [0xE3CD - rangeMin] = 0x84A0;
            mapDataStd [rangeNo] [0xE3CE - rangeMin] = 0x84D7;
            mapDataStd [rangeNo] [0xE3CF - rangeMin] = 0x84D4;

            mapDataStd [rangeNo] [0xE3D0 - rangeMin] = 0x84D2;
            mapDataStd [rangeNo] [0xE3D1 - rangeMin] = 0x84DB;
            mapDataStd [rangeNo] [0xE3D2 - rangeMin] = 0x84B0;
            mapDataStd [rangeNo] [0xE3D3 - rangeMin] = 0x8491;
            mapDataStd [rangeNo] [0xE3D4 - rangeMin] = 0x8661;
            mapDataStd [rangeNo] [0xE3D5 - rangeMin] = 0x8733;
            mapDataStd [rangeNo] [0xE3D6 - rangeMin] = 0x8723;
            mapDataStd [rangeNo] [0xE3D7 - rangeMin] = 0x8728;
            mapDataStd [rangeNo] [0xE3D8 - rangeMin] = 0x876B;
            mapDataStd [rangeNo] [0xE3D9 - rangeMin] = 0x8740;
            mapDataStd [rangeNo] [0xE3DA - rangeMin] = 0x872E;
            mapDataStd [rangeNo] [0xE3DB - rangeMin] = 0x871E;
            mapDataStd [rangeNo] [0xE3DC - rangeMin] = 0x8721;
            mapDataStd [rangeNo] [0xE3DD - rangeMin] = 0x8719;
            mapDataStd [rangeNo] [0xE3DE - rangeMin] = 0x871B;
            mapDataStd [rangeNo] [0xE3DF - rangeMin] = 0x8743;

            mapDataStd [rangeNo] [0xE3E0 - rangeMin] = 0x872C;
            mapDataStd [rangeNo] [0xE3E1 - rangeMin] = 0x8741;
            mapDataStd [rangeNo] [0xE3E2 - rangeMin] = 0x873E;
            mapDataStd [rangeNo] [0xE3E3 - rangeMin] = 0x8746;
            mapDataStd [rangeNo] [0xE3E4 - rangeMin] = 0x8720;
            mapDataStd [rangeNo] [0xE3E5 - rangeMin] = 0x8732;
            mapDataStd [rangeNo] [0xE3E6 - rangeMin] = 0x872A;
            mapDataStd [rangeNo] [0xE3E7 - rangeMin] = 0x872D;
            mapDataStd [rangeNo] [0xE3E8 - rangeMin] = 0x873C;
            mapDataStd [rangeNo] [0xE3E9 - rangeMin] = 0x8712;
            mapDataStd [rangeNo] [0xE3EA - rangeMin] = 0x873A;
            mapDataStd [rangeNo] [0xE3EB - rangeMin] = 0x8731;
            mapDataStd [rangeNo] [0xE3EC - rangeMin] = 0x8735;
            mapDataStd [rangeNo] [0xE3ED - rangeMin] = 0x8742;
            mapDataStd [rangeNo] [0xE3EE - rangeMin] = 0x8726;
            mapDataStd [rangeNo] [0xE3EF - rangeMin] = 0x8727;

            mapDataStd [rangeNo] [0xE3F0 - rangeMin] = 0x8738;
            mapDataStd [rangeNo] [0xE3F1 - rangeMin] = 0x8724;
            mapDataStd [rangeNo] [0xE3F2 - rangeMin] = 0x871A;
            mapDataStd [rangeNo] [0xE3F3 - rangeMin] = 0x8730;
            mapDataStd [rangeNo] [0xE3F4 - rangeMin] = 0x8711;
            mapDataStd [rangeNo] [0xE3F5 - rangeMin] = 0x88F7;
            mapDataStd [rangeNo] [0xE3F6 - rangeMin] = 0x88E7;
            mapDataStd [rangeNo] [0xE3F7 - rangeMin] = 0x88F1;
            mapDataStd [rangeNo] [0xE3F8 - rangeMin] = 0x88F2;
            mapDataStd [rangeNo] [0xE3F9 - rangeMin] = 0x88FA;
            mapDataStd [rangeNo] [0xE3FA - rangeMin] = 0x88FE;
            mapDataStd [rangeNo] [0xE3FB - rangeMin] = 0x88EE;
            mapDataStd [rangeNo] [0xE3FC - rangeMin] = 0x88FC;
            mapDataStd [rangeNo] [0xE3FD - rangeMin] = 0x88F6;
            mapDataStd [rangeNo] [0xE3FE - rangeMin] = 0x88FB;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xE440 - rangeMin] = 0x88F0;
            mapDataStd [rangeNo] [0xE441 - rangeMin] = 0x88EC;
            mapDataStd [rangeNo] [0xE442 - rangeMin] = 0x88EB;
            mapDataStd [rangeNo] [0xE443 - rangeMin] = 0x899D;
            mapDataStd [rangeNo] [0xE444 - rangeMin] = 0x89A1;
            mapDataStd [rangeNo] [0xE445 - rangeMin] = 0x899F;
            mapDataStd [rangeNo] [0xE446 - rangeMin] = 0x899E;
            mapDataStd [rangeNo] [0xE447 - rangeMin] = 0x89E9;
            mapDataStd [rangeNo] [0xE448 - rangeMin] = 0x89EB;
            mapDataStd [rangeNo] [0xE449 - rangeMin] = 0x89E8;
            mapDataStd [rangeNo] [0xE44A - rangeMin] = 0x8AAB;
            mapDataStd [rangeNo] [0xE44B - rangeMin] = 0x8A99;
            mapDataStd [rangeNo] [0xE44C - rangeMin] = 0x8A8B;
            mapDataStd [rangeNo] [0xE44D - rangeMin] = 0x8A92;
            mapDataStd [rangeNo] [0xE44E - rangeMin] = 0x8A8F;
            mapDataStd [rangeNo] [0xE44F - rangeMin] = 0x8A96;

            mapDataStd [rangeNo] [0xE450 - rangeMin] = 0x8C3D;
            mapDataStd [rangeNo] [0xE451 - rangeMin] = 0x8C68;
            mapDataStd [rangeNo] [0xE452 - rangeMin] = 0x8C69;
            mapDataStd [rangeNo] [0xE453 - rangeMin] = 0x8CD5;
            mapDataStd [rangeNo] [0xE454 - rangeMin] = 0x8CCF;
            mapDataStd [rangeNo] [0xE455 - rangeMin] = 0x8CD7;
            mapDataStd [rangeNo] [0xE456 - rangeMin] = 0x8D96;
            mapDataStd [rangeNo] [0xE457 - rangeMin] = 0x8E09;
            mapDataStd [rangeNo] [0xE458 - rangeMin] = 0x8E02;
            mapDataStd [rangeNo] [0xE459 - rangeMin] = 0x8DFF;
            mapDataStd [rangeNo] [0xE45A - rangeMin] = 0x8E0D;
            mapDataStd [rangeNo] [0xE45B - rangeMin] = 0x8DFD;
            mapDataStd [rangeNo] [0xE45C - rangeMin] = 0x8E0A;
            mapDataStd [rangeNo] [0xE45D - rangeMin] = 0x8E03;
            mapDataStd [rangeNo] [0xE45E - rangeMin] = 0x8E07;
            mapDataStd [rangeNo] [0xE45F - rangeMin] = 0x8E06;

            mapDataStd [rangeNo] [0xE460 - rangeMin] = 0x8E05;
            mapDataStd [rangeNo] [0xE461 - rangeMin] = 0x8DFE;
            mapDataStd [rangeNo] [0xE462 - rangeMin] = 0x8E00;
            mapDataStd [rangeNo] [0xE463 - rangeMin] = 0x8E04;
            mapDataStd [rangeNo] [0xE464 - rangeMin] = 0x8F10;
            mapDataStd [rangeNo] [0xE465 - rangeMin] = 0x8F11;
            mapDataStd [rangeNo] [0xE466 - rangeMin] = 0x8F0E;
            mapDataStd [rangeNo] [0xE467 - rangeMin] = 0x8F0D;
            mapDataStd [rangeNo] [0xE468 - rangeMin] = 0x9123;
            mapDataStd [rangeNo] [0xE469 - rangeMin] = 0x911C;
            mapDataStd [rangeNo] [0xE46A - rangeMin] = 0x9120;
            mapDataStd [rangeNo] [0xE46B - rangeMin] = 0x9122;
            mapDataStd [rangeNo] [0xE46C - rangeMin] = 0x911F;
            mapDataStd [rangeNo] [0xE46D - rangeMin] = 0x911D;
            mapDataStd [rangeNo] [0xE46E - rangeMin] = 0x911A;
            mapDataStd [rangeNo] [0xE46F - rangeMin] = 0x9124;

            mapDataStd [rangeNo] [0xE470 - rangeMin] = 0x9121;
            mapDataStd [rangeNo] [0xE471 - rangeMin] = 0x911B;
            mapDataStd [rangeNo] [0xE472 - rangeMin] = 0x917A;
            mapDataStd [rangeNo] [0xE473 - rangeMin] = 0x9172;
            mapDataStd [rangeNo] [0xE474 - rangeMin] = 0x9179;
            mapDataStd [rangeNo] [0xE475 - rangeMin] = 0x9173;
            mapDataStd [rangeNo] [0xE476 - rangeMin] = 0x92A5;
            mapDataStd [rangeNo] [0xE477 - rangeMin] = 0x92A4;
            mapDataStd [rangeNo] [0xE478 - rangeMin] = 0x9276;
            mapDataStd [rangeNo] [0xE479 - rangeMin] = 0x929B;
            mapDataStd [rangeNo] [0xE47A - rangeMin] = 0x927A;
            mapDataStd [rangeNo] [0xE47B - rangeMin] = 0x92A0;
            mapDataStd [rangeNo] [0xE47C - rangeMin] = 0x9294;
            mapDataStd [rangeNo] [0xE47D - rangeMin] = 0x92AA;
            mapDataStd [rangeNo] [0xE47E - rangeMin] = 0x928D;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xE4A1 - rangeMin] = 0x92A6;
            mapDataStd [rangeNo] [0xE4A2 - rangeMin] = 0x929A;
            mapDataStd [rangeNo] [0xE4A3 - rangeMin] = 0x92AB;
            mapDataStd [rangeNo] [0xE4A4 - rangeMin] = 0x9279;
            mapDataStd [rangeNo] [0xE4A5 - rangeMin] = 0x9297;
            mapDataStd [rangeNo] [0xE4A6 - rangeMin] = 0x927F;
            mapDataStd [rangeNo] [0xE4A7 - rangeMin] = 0x92A3;
            mapDataStd [rangeNo] [0xE4A8 - rangeMin] = 0x92EE;
            mapDataStd [rangeNo] [0xE4A9 - rangeMin] = 0x928E;
            mapDataStd [rangeNo] [0xE4AA - rangeMin] = 0x9282;
            mapDataStd [rangeNo] [0xE4AB - rangeMin] = 0x9295;
            mapDataStd [rangeNo] [0xE4AC - rangeMin] = 0x92A2;
            mapDataStd [rangeNo] [0xE4AD - rangeMin] = 0x927D;
            mapDataStd [rangeNo] [0xE4AE - rangeMin] = 0x9288;
            mapDataStd [rangeNo] [0xE4AF - rangeMin] = 0x92A1;

            mapDataStd [rangeNo] [0xE4B0 - rangeMin] = 0x928A;
            mapDataStd [rangeNo] [0xE4B1 - rangeMin] = 0x9286;
            mapDataStd [rangeNo] [0xE4B2 - rangeMin] = 0x928C;
            mapDataStd [rangeNo] [0xE4B3 - rangeMin] = 0x9299;
            mapDataStd [rangeNo] [0xE4B4 - rangeMin] = 0x92A7;
            mapDataStd [rangeNo] [0xE4B5 - rangeMin] = 0x927E;
            mapDataStd [rangeNo] [0xE4B6 - rangeMin] = 0x9287;
            mapDataStd [rangeNo] [0xE4B7 - rangeMin] = 0x92A9;
            mapDataStd [rangeNo] [0xE4B8 - rangeMin] = 0x929D;
            mapDataStd [rangeNo] [0xE4B9 - rangeMin] = 0x928B;
            mapDataStd [rangeNo] [0xE4BA - rangeMin] = 0x922D;
            mapDataStd [rangeNo] [0xE4BB - rangeMin] = 0x969E;
            mapDataStd [rangeNo] [0xE4BC - rangeMin] = 0x96A1;
            mapDataStd [rangeNo] [0xE4BD - rangeMin] = 0x96FF;
            mapDataStd [rangeNo] [0xE4BE - rangeMin] = 0x9758;
            mapDataStd [rangeNo] [0xE4BF - rangeMin] = 0x977D;

            mapDataStd [rangeNo] [0xE4C0 - rangeMin] = 0x977A;
            mapDataStd [rangeNo] [0xE4C1 - rangeMin] = 0x977E;
            mapDataStd [rangeNo] [0xE4C2 - rangeMin] = 0x9783;
            mapDataStd [rangeNo] [0xE4C3 - rangeMin] = 0x9780;
            mapDataStd [rangeNo] [0xE4C4 - rangeMin] = 0x9782;
            mapDataStd [rangeNo] [0xE4C5 - rangeMin] = 0x977B;
            mapDataStd [rangeNo] [0xE4C6 - rangeMin] = 0x9784;
            mapDataStd [rangeNo] [0xE4C7 - rangeMin] = 0x9781;
            mapDataStd [rangeNo] [0xE4C8 - rangeMin] = 0x977F;
            mapDataStd [rangeNo] [0xE4C9 - rangeMin] = 0x97CE;
            mapDataStd [rangeNo] [0xE4CA - rangeMin] = 0x97CD;
            mapDataStd [rangeNo] [0xE4CB - rangeMin] = 0x9816;
            mapDataStd [rangeNo] [0xE4CC - rangeMin] = 0x98AD;
            mapDataStd [rangeNo] [0xE4CD - rangeMin] = 0x98AE;
            mapDataStd [rangeNo] [0xE4CE - rangeMin] = 0x9902;
            mapDataStd [rangeNo] [0xE4CF - rangeMin] = 0x9900;

            mapDataStd [rangeNo] [0xE4D0 - rangeMin] = 0x9907;
            mapDataStd [rangeNo] [0xE4D1 - rangeMin] = 0x999D;
            mapDataStd [rangeNo] [0xE4D2 - rangeMin] = 0x999C;
            mapDataStd [rangeNo] [0xE4D3 - rangeMin] = 0x99C3;
            mapDataStd [rangeNo] [0xE4D4 - rangeMin] = 0x99B9;
            mapDataStd [rangeNo] [0xE4D5 - rangeMin] = 0x99BB;
            mapDataStd [rangeNo] [0xE4D6 - rangeMin] = 0x99BA;
            mapDataStd [rangeNo] [0xE4D7 - rangeMin] = 0x99C2;
            mapDataStd [rangeNo] [0xE4D8 - rangeMin] = 0x99BD;
            mapDataStd [rangeNo] [0xE4D9 - rangeMin] = 0x99C7;
            mapDataStd [rangeNo] [0xE4DA - rangeMin] = 0x9AB1;
            mapDataStd [rangeNo] [0xE4DB - rangeMin] = 0x9AE3;
            mapDataStd [rangeNo] [0xE4DC - rangeMin] = 0x9AE7;
            mapDataStd [rangeNo] [0xE4DD - rangeMin] = 0x9B3E;
            mapDataStd [rangeNo] [0xE4DE - rangeMin] = 0x9B3F;
            mapDataStd [rangeNo] [0xE4DF - rangeMin] = 0x9B60;

            mapDataStd [rangeNo] [0xE4E0 - rangeMin] = 0x9B61;
            mapDataStd [rangeNo] [0xE4E1 - rangeMin] = 0x9B5F;
            mapDataStd [rangeNo] [0xE4E2 - rangeMin] = 0x9CF1;
            mapDataStd [rangeNo] [0xE4E3 - rangeMin] = 0x9CF2;
            mapDataStd [rangeNo] [0xE4E4 - rangeMin] = 0x9CF5;
            mapDataStd [rangeNo] [0xE4E5 - rangeMin] = 0x9EA7;
            mapDataStd [rangeNo] [0xE4E6 - rangeMin] = 0x50FF;
            mapDataStd [rangeNo] [0xE4E7 - rangeMin] = 0x5103;
            mapDataStd [rangeNo] [0xE4E8 - rangeMin] = 0x5130;
            mapDataStd [rangeNo] [0xE4E9 - rangeMin] = 0x50F8;
            mapDataStd [rangeNo] [0xE4EA - rangeMin] = 0x5106;
            mapDataStd [rangeNo] [0xE4EB - rangeMin] = 0x5107;
            mapDataStd [rangeNo] [0xE4EC - rangeMin] = 0x50F6;
            mapDataStd [rangeNo] [0xE4ED - rangeMin] = 0x50FE;
            mapDataStd [rangeNo] [0xE4EE - rangeMin] = 0x510B;
            mapDataStd [rangeNo] [0xE4EF - rangeMin] = 0x510C;

            mapDataStd [rangeNo] [0xE4F0 - rangeMin] = 0x50FD;
            mapDataStd [rangeNo] [0xE4F1 - rangeMin] = 0x510A;
            mapDataStd [rangeNo] [0xE4F2 - rangeMin] = 0x528B;
            mapDataStd [rangeNo] [0xE4F3 - rangeMin] = 0x528C;
            mapDataStd [rangeNo] [0xE4F4 - rangeMin] = 0x52F1;
            mapDataStd [rangeNo] [0xE4F5 - rangeMin] = 0x52EF;
            mapDataStd [rangeNo] [0xE4F6 - rangeMin] = 0x5648;
            mapDataStd [rangeNo] [0xE4F7 - rangeMin] = 0x5642;
            mapDataStd [rangeNo] [0xE4F8 - rangeMin] = 0x564C;
            mapDataStd [rangeNo] [0xE4F9 - rangeMin] = 0x5635;
            mapDataStd [rangeNo] [0xE4FA - rangeMin] = 0x5641;
            mapDataStd [rangeNo] [0xE4FB - rangeMin] = 0x564A;
            mapDataStd [rangeNo] [0xE4FC - rangeMin] = 0x5649;
            mapDataStd [rangeNo] [0xE4FD - rangeMin] = 0x5646;
            mapDataStd [rangeNo] [0xE4FE - rangeMin] = 0x5658;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xE540 - rangeMin] = 0x565A;
            mapDataStd [rangeNo] [0xE541 - rangeMin] = 0x5640;
            mapDataStd [rangeNo] [0xE542 - rangeMin] = 0x5633;
            mapDataStd [rangeNo] [0xE543 - rangeMin] = 0x563D;
            mapDataStd [rangeNo] [0xE544 - rangeMin] = 0x562C;
            mapDataStd [rangeNo] [0xE545 - rangeMin] = 0x563E;
            mapDataStd [rangeNo] [0xE546 - rangeMin] = 0x5638;
            mapDataStd [rangeNo] [0xE547 - rangeMin] = 0x562A;
            mapDataStd [rangeNo] [0xE548 - rangeMin] = 0x563A;
            mapDataStd [rangeNo] [0xE549 - rangeMin] = 0x571A;
            mapDataStd [rangeNo] [0xE54A - rangeMin] = 0x58AB;
            mapDataStd [rangeNo] [0xE54B - rangeMin] = 0x589D;
            mapDataStd [rangeNo] [0xE54C - rangeMin] = 0x58B1;
            mapDataStd [rangeNo] [0xE54D - rangeMin] = 0x58A0;
            mapDataStd [rangeNo] [0xE54E - rangeMin] = 0x58A3;
            mapDataStd [rangeNo] [0xE54F - rangeMin] = 0x58AF;

            mapDataStd [rangeNo] [0xE550 - rangeMin] = 0x58AC;
            mapDataStd [rangeNo] [0xE551 - rangeMin] = 0x58A5;
            mapDataStd [rangeNo] [0xE552 - rangeMin] = 0x58A1;
            mapDataStd [rangeNo] [0xE553 - rangeMin] = 0x58FF;
            mapDataStd [rangeNo] [0xE554 - rangeMin] = 0x5AFF;
            mapDataStd [rangeNo] [0xE555 - rangeMin] = 0x5AF4;
            mapDataStd [rangeNo] [0xE556 - rangeMin] = 0x5AFD;
            mapDataStd [rangeNo] [0xE557 - rangeMin] = 0x5AF7;
            mapDataStd [rangeNo] [0xE558 - rangeMin] = 0x5AF6;
            mapDataStd [rangeNo] [0xE559 - rangeMin] = 0x5B03;
            mapDataStd [rangeNo] [0xE55A - rangeMin] = 0x5AF8;
            mapDataStd [rangeNo] [0xE55B - rangeMin] = 0x5B02;
            mapDataStd [rangeNo] [0xE55C - rangeMin] = 0x5AF9;
            mapDataStd [rangeNo] [0xE55D - rangeMin] = 0x5B01;
            mapDataStd [rangeNo] [0xE55E - rangeMin] = 0x5B07;
            mapDataStd [rangeNo] [0xE55F - rangeMin] = 0x5B05;

            mapDataStd [rangeNo] [0xE560 - rangeMin] = 0x5B0F;
            mapDataStd [rangeNo] [0xE561 - rangeMin] = 0x5C67;
            mapDataStd [rangeNo] [0xE562 - rangeMin] = 0x5D99;
            mapDataStd [rangeNo] [0xE563 - rangeMin] = 0x5D97;
            mapDataStd [rangeNo] [0xE564 - rangeMin] = 0x5D9F;
            mapDataStd [rangeNo] [0xE565 - rangeMin] = 0x5D92;
            mapDataStd [rangeNo] [0xE566 - rangeMin] = 0x5DA2;
            mapDataStd [rangeNo] [0xE567 - rangeMin] = 0x5D93;
            mapDataStd [rangeNo] [0xE568 - rangeMin] = 0x5D95;
            mapDataStd [rangeNo] [0xE569 - rangeMin] = 0x5DA0;
            mapDataStd [rangeNo] [0xE56A - rangeMin] = 0x5D9C;
            mapDataStd [rangeNo] [0xE56B - rangeMin] = 0x5DA1;
            mapDataStd [rangeNo] [0xE56C - rangeMin] = 0x5D9A;
            mapDataStd [rangeNo] [0xE56D - rangeMin] = 0x5D9E;
            mapDataStd [rangeNo] [0xE56E - rangeMin] = 0x5E69;
            mapDataStd [rangeNo] [0xE56F - rangeMin] = 0x5E5D;

            mapDataStd [rangeNo] [0xE570 - rangeMin] = 0x5E60;
            mapDataStd [rangeNo] [0xE571 - rangeMin] = 0x5E5C;
            mapDataStd [rangeNo] [0xE572 - rangeMin] = 0x7DF3;
            mapDataStd [rangeNo] [0xE573 - rangeMin] = 0x5EDB;
            mapDataStd [rangeNo] [0xE574 - rangeMin] = 0x5EDE;
            mapDataStd [rangeNo] [0xE575 - rangeMin] = 0x5EE1;
            mapDataStd [rangeNo] [0xE576 - rangeMin] = 0x5F49;
            mapDataStd [rangeNo] [0xE577 - rangeMin] = 0x5FB2;
            mapDataStd [rangeNo] [0xE578 - rangeMin] = 0x618B;
            mapDataStd [rangeNo] [0xE579 - rangeMin] = 0x6183;
            mapDataStd [rangeNo] [0xE57A - rangeMin] = 0x6179;
            mapDataStd [rangeNo] [0xE57B - rangeMin] = 0x61B1;
            mapDataStd [rangeNo] [0xE57C - rangeMin] = 0x61B0;
            mapDataStd [rangeNo] [0xE57D - rangeMin] = 0x61A2;
            mapDataStd [rangeNo] [0xE57E - rangeMin] = 0x6189;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xE5A1 - rangeMin] = 0x619B;
            mapDataStd [rangeNo] [0xE5A2 - rangeMin] = 0x6193;
            mapDataStd [rangeNo] [0xE5A3 - rangeMin] = 0x61AF;
            mapDataStd [rangeNo] [0xE5A4 - rangeMin] = 0x61AD;
            mapDataStd [rangeNo] [0xE5A5 - rangeMin] = 0x619F;
            mapDataStd [rangeNo] [0xE5A6 - rangeMin] = 0x6192;
            mapDataStd [rangeNo] [0xE5A7 - rangeMin] = 0x61AA;
            mapDataStd [rangeNo] [0xE5A8 - rangeMin] = 0x61A1;
            mapDataStd [rangeNo] [0xE5A9 - rangeMin] = 0x618D;
            mapDataStd [rangeNo] [0xE5AA - rangeMin] = 0x6166;
            mapDataStd [rangeNo] [0xE5AB - rangeMin] = 0x61B3;
            mapDataStd [rangeNo] [0xE5AC - rangeMin] = 0x622D;
            mapDataStd [rangeNo] [0xE5AD - rangeMin] = 0x646E;
            mapDataStd [rangeNo] [0xE5AE - rangeMin] = 0x6470;
            mapDataStd [rangeNo] [0xE5AF - rangeMin] = 0x6496;

            mapDataStd [rangeNo] [0xE5B0 - rangeMin] = 0x64A0;
            mapDataStd [rangeNo] [0xE5B1 - rangeMin] = 0x6485;
            mapDataStd [rangeNo] [0xE5B2 - rangeMin] = 0x6497;
            mapDataStd [rangeNo] [0xE5B3 - rangeMin] = 0x649C;
            mapDataStd [rangeNo] [0xE5B4 - rangeMin] = 0x648F;
            mapDataStd [rangeNo] [0xE5B5 - rangeMin] = 0x648B;
            mapDataStd [rangeNo] [0xE5B6 - rangeMin] = 0x648A;
            mapDataStd [rangeNo] [0xE5B7 - rangeMin] = 0x648C;
            mapDataStd [rangeNo] [0xE5B8 - rangeMin] = 0x64A3;
            mapDataStd [rangeNo] [0xE5B9 - rangeMin] = 0x649F;
            mapDataStd [rangeNo] [0xE5BA - rangeMin] = 0x6468;
            mapDataStd [rangeNo] [0xE5BB - rangeMin] = 0x64B1;
            mapDataStd [rangeNo] [0xE5BC - rangeMin] = 0x6498;
            mapDataStd [rangeNo] [0xE5BD - rangeMin] = 0x6576;
            mapDataStd [rangeNo] [0xE5BE - rangeMin] = 0x657A;
            mapDataStd [rangeNo] [0xE5BF - rangeMin] = 0x6579;

            mapDataStd [rangeNo] [0xE5C0 - rangeMin] = 0x657B;
            mapDataStd [rangeNo] [0xE5C1 - rangeMin] = 0x65B2;
            mapDataStd [rangeNo] [0xE5C2 - rangeMin] = 0x65B3;
            mapDataStd [rangeNo] [0xE5C3 - rangeMin] = 0x66B5;
            mapDataStd [rangeNo] [0xE5C4 - rangeMin] = 0x66B0;
            mapDataStd [rangeNo] [0xE5C5 - rangeMin] = 0x66A9;
            mapDataStd [rangeNo] [0xE5C6 - rangeMin] = 0x66B2;
            mapDataStd [rangeNo] [0xE5C7 - rangeMin] = 0x66B7;
            mapDataStd [rangeNo] [0xE5C8 - rangeMin] = 0x66AA;
            mapDataStd [rangeNo] [0xE5C9 - rangeMin] = 0x66AF;
            mapDataStd [rangeNo] [0xE5CA - rangeMin] = 0x6A00;
            mapDataStd [rangeNo] [0xE5CB - rangeMin] = 0x6A06;
            mapDataStd [rangeNo] [0xE5CC - rangeMin] = 0x6A17;
            mapDataStd [rangeNo] [0xE5CD - rangeMin] = 0x69E5;
            mapDataStd [rangeNo] [0xE5CE - rangeMin] = 0x69F8;
            mapDataStd [rangeNo] [0xE5CF - rangeMin] = 0x6A15;

            mapDataStd [rangeNo] [0xE5D0 - rangeMin] = 0x69F1;
            mapDataStd [rangeNo] [0xE5D1 - rangeMin] = 0x69E4;
            mapDataStd [rangeNo] [0xE5D2 - rangeMin] = 0x6A20;
            mapDataStd [rangeNo] [0xE5D3 - rangeMin] = 0x69FF;
            mapDataStd [rangeNo] [0xE5D4 - rangeMin] = 0x69EC;
            mapDataStd [rangeNo] [0xE5D5 - rangeMin] = 0x69E2;
            mapDataStd [rangeNo] [0xE5D6 - rangeMin] = 0x6A1B;
            mapDataStd [rangeNo] [0xE5D7 - rangeMin] = 0x6A1D;
            mapDataStd [rangeNo] [0xE5D8 - rangeMin] = 0x69FE;
            mapDataStd [rangeNo] [0xE5D9 - rangeMin] = 0x6A27;
            mapDataStd [rangeNo] [0xE5DA - rangeMin] = 0x69F2;
            mapDataStd [rangeNo] [0xE5DB - rangeMin] = 0x69EE;
            mapDataStd [rangeNo] [0xE5DC - rangeMin] = 0x6A14;
            mapDataStd [rangeNo] [0xE5DD - rangeMin] = 0x69F7;
            mapDataStd [rangeNo] [0xE5DE - rangeMin] = 0x69E7;
            mapDataStd [rangeNo] [0xE5DF - rangeMin] = 0x6A40;

            mapDataStd [rangeNo] [0xE5E0 - rangeMin] = 0x6A08;
            mapDataStd [rangeNo] [0xE5E1 - rangeMin] = 0x69E6;
            mapDataStd [rangeNo] [0xE5E2 - rangeMin] = 0x69FB;
            mapDataStd [rangeNo] [0xE5E3 - rangeMin] = 0x6A0D;
            mapDataStd [rangeNo] [0xE5E4 - rangeMin] = 0x69FC;
            mapDataStd [rangeNo] [0xE5E5 - rangeMin] = 0x69EB;
            mapDataStd [rangeNo] [0xE5E6 - rangeMin] = 0x6A09;
            mapDataStd [rangeNo] [0xE5E7 - rangeMin] = 0x6A04;
            mapDataStd [rangeNo] [0xE5E8 - rangeMin] = 0x6A18;
            mapDataStd [rangeNo] [0xE5E9 - rangeMin] = 0x6A25;
            mapDataStd [rangeNo] [0xE5EA - rangeMin] = 0x6A0F;
            mapDataStd [rangeNo] [0xE5EB - rangeMin] = 0x69F6;
            mapDataStd [rangeNo] [0xE5EC - rangeMin] = 0x6A26;
            mapDataStd [rangeNo] [0xE5ED - rangeMin] = 0x6A07;
            mapDataStd [rangeNo] [0xE5EE - rangeMin] = 0x69F4;
            mapDataStd [rangeNo] [0xE5EF - rangeMin] = 0x6A16;

            mapDataStd [rangeNo] [0xE5F0 - rangeMin] = 0x6B51;
            mapDataStd [rangeNo] [0xE5F1 - rangeMin] = 0x6BA5;
            mapDataStd [rangeNo] [0xE5F2 - rangeMin] = 0x6BA3;
            mapDataStd [rangeNo] [0xE5F3 - rangeMin] = 0x6BA2;
            mapDataStd [rangeNo] [0xE5F4 - rangeMin] = 0x6BA6;
            mapDataStd [rangeNo] [0xE5F5 - rangeMin] = 0x6C01;
            mapDataStd [rangeNo] [0xE5F6 - rangeMin] = 0x6C00;
            mapDataStd [rangeNo] [0xE5F7 - rangeMin] = 0x6BFF;
            mapDataStd [rangeNo] [0xE5F8 - rangeMin] = 0x6C02;
            mapDataStd [rangeNo] [0xE5F9 - rangeMin] = 0x6F41;
            mapDataStd [rangeNo] [0xE5FA - rangeMin] = 0x6F26;
            mapDataStd [rangeNo] [0xE5FB - rangeMin] = 0x6F7E;
            mapDataStd [rangeNo] [0xE5FC - rangeMin] = 0x6F87;
            mapDataStd [rangeNo] [0xE5FD - rangeMin] = 0x6FC6;
            mapDataStd [rangeNo] [0xE5FE - rangeMin] = 0x6F92;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xE640 - rangeMin] = 0x6F8D;
            mapDataStd [rangeNo] [0xE641 - rangeMin] = 0x6F89;
            mapDataStd [rangeNo] [0xE642 - rangeMin] = 0x6F8C;
            mapDataStd [rangeNo] [0xE643 - rangeMin] = 0x6F62;
            mapDataStd [rangeNo] [0xE644 - rangeMin] = 0x6F4F;
            mapDataStd [rangeNo] [0xE645 - rangeMin] = 0x6F85;
            mapDataStd [rangeNo] [0xE646 - rangeMin] = 0x6F5A;
            mapDataStd [rangeNo] [0xE647 - rangeMin] = 0x6F96;
            mapDataStd [rangeNo] [0xE648 - rangeMin] = 0x6F76;
            mapDataStd [rangeNo] [0xE649 - rangeMin] = 0x6F6C;
            mapDataStd [rangeNo] [0xE64A - rangeMin] = 0x6F82;
            mapDataStd [rangeNo] [0xE64B - rangeMin] = 0x6F55;
            mapDataStd [rangeNo] [0xE64C - rangeMin] = 0x6F72;
            mapDataStd [rangeNo] [0xE64D - rangeMin] = 0x6F52;
            mapDataStd [rangeNo] [0xE64E - rangeMin] = 0x6F50;
            mapDataStd [rangeNo] [0xE64F - rangeMin] = 0x6F57;

            mapDataStd [rangeNo] [0xE650 - rangeMin] = 0x6F94;
            mapDataStd [rangeNo] [0xE651 - rangeMin] = 0x6F93;
            mapDataStd [rangeNo] [0xE652 - rangeMin] = 0x6F5D;
            mapDataStd [rangeNo] [0xE653 - rangeMin] = 0x6F00;
            mapDataStd [rangeNo] [0xE654 - rangeMin] = 0x6F61;
            mapDataStd [rangeNo] [0xE655 - rangeMin] = 0x6F6B;
            mapDataStd [rangeNo] [0xE656 - rangeMin] = 0x6F7D;
            mapDataStd [rangeNo] [0xE657 - rangeMin] = 0x6F67;
            mapDataStd [rangeNo] [0xE658 - rangeMin] = 0x6F90;
            mapDataStd [rangeNo] [0xE659 - rangeMin] = 0x6F53;
            mapDataStd [rangeNo] [0xE65A - rangeMin] = 0x6F8B;
            mapDataStd [rangeNo] [0xE65B - rangeMin] = 0x6F69;
            mapDataStd [rangeNo] [0xE65C - rangeMin] = 0x6F7F;
            mapDataStd [rangeNo] [0xE65D - rangeMin] = 0x6F95;
            mapDataStd [rangeNo] [0xE65E - rangeMin] = 0x6F63;
            mapDataStd [rangeNo] [0xE65F - rangeMin] = 0x6F77;

            mapDataStd [rangeNo] [0xE660 - rangeMin] = 0x6F6A;
            mapDataStd [rangeNo] [0xE661 - rangeMin] = 0x6F7B;
            mapDataStd [rangeNo] [0xE662 - rangeMin] = 0x71B2;
            mapDataStd [rangeNo] [0xE663 - rangeMin] = 0x71AF;
            mapDataStd [rangeNo] [0xE664 - rangeMin] = 0x719B;
            mapDataStd [rangeNo] [0xE665 - rangeMin] = 0x71B0;
            mapDataStd [rangeNo] [0xE666 - rangeMin] = 0x71A0;
            mapDataStd [rangeNo] [0xE667 - rangeMin] = 0x719A;
            mapDataStd [rangeNo] [0xE668 - rangeMin] = 0x71A9;
            mapDataStd [rangeNo] [0xE669 - rangeMin] = 0x71B5;
            mapDataStd [rangeNo] [0xE66A - rangeMin] = 0x719D;
            mapDataStd [rangeNo] [0xE66B - rangeMin] = 0x71A5;
            mapDataStd [rangeNo] [0xE66C - rangeMin] = 0x719E;
            mapDataStd [rangeNo] [0xE66D - rangeMin] = 0x71A4;
            mapDataStd [rangeNo] [0xE66E - rangeMin] = 0x71A1;
            mapDataStd [rangeNo] [0xE66F - rangeMin] = 0x71AA;

            mapDataStd [rangeNo] [0xE670 - rangeMin] = 0x719C;
            mapDataStd [rangeNo] [0xE671 - rangeMin] = 0x71A7;
            mapDataStd [rangeNo] [0xE672 - rangeMin] = 0x71B3;
            mapDataStd [rangeNo] [0xE673 - rangeMin] = 0x7298;
            mapDataStd [rangeNo] [0xE674 - rangeMin] = 0x729A;
            mapDataStd [rangeNo] [0xE675 - rangeMin] = 0x7358;
            mapDataStd [rangeNo] [0xE676 - rangeMin] = 0x7352;
            mapDataStd [rangeNo] [0xE677 - rangeMin] = 0x735E;
            mapDataStd [rangeNo] [0xE678 - rangeMin] = 0x735F;
            mapDataStd [rangeNo] [0xE679 - rangeMin] = 0x7360;
            mapDataStd [rangeNo] [0xE67A - rangeMin] = 0x735D;
            mapDataStd [rangeNo] [0xE67B - rangeMin] = 0x735B;
            mapDataStd [rangeNo] [0xE67C - rangeMin] = 0x7361;
            mapDataStd [rangeNo] [0xE67D - rangeMin] = 0x735A;
            mapDataStd [rangeNo] [0xE67E - rangeMin] = 0x7359;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xE6A1 - rangeMin] = 0x7362;
            mapDataStd [rangeNo] [0xE6A2 - rangeMin] = 0x7487;
            mapDataStd [rangeNo] [0xE6A3 - rangeMin] = 0x7489;
            mapDataStd [rangeNo] [0xE6A4 - rangeMin] = 0x748A;
            mapDataStd [rangeNo] [0xE6A5 - rangeMin] = 0x7486;
            mapDataStd [rangeNo] [0xE6A6 - rangeMin] = 0x7481;
            mapDataStd [rangeNo] [0xE6A7 - rangeMin] = 0x747D;
            mapDataStd [rangeNo] [0xE6A8 - rangeMin] = 0x7485;
            mapDataStd [rangeNo] [0xE6A9 - rangeMin] = 0x7488;
            mapDataStd [rangeNo] [0xE6AA - rangeMin] = 0x747C;
            mapDataStd [rangeNo] [0xE6AB - rangeMin] = 0x7479;
            mapDataStd [rangeNo] [0xE6AC - rangeMin] = 0x7508;
            mapDataStd [rangeNo] [0xE6AD - rangeMin] = 0x7507;
            mapDataStd [rangeNo] [0xE6AE - rangeMin] = 0x757E;
            mapDataStd [rangeNo] [0xE6AF - rangeMin] = 0x7625;

            mapDataStd [rangeNo] [0xE6B0 - rangeMin] = 0x761E;
            mapDataStd [rangeNo] [0xE6B1 - rangeMin] = 0x7619;
            mapDataStd [rangeNo] [0xE6B2 - rangeMin] = 0x761D;
            mapDataStd [rangeNo] [0xE6B3 - rangeMin] = 0x761C;
            mapDataStd [rangeNo] [0xE6B4 - rangeMin] = 0x7623;
            mapDataStd [rangeNo] [0xE6B5 - rangeMin] = 0x761A;
            mapDataStd [rangeNo] [0xE6B6 - rangeMin] = 0x7628;
            mapDataStd [rangeNo] [0xE6B7 - rangeMin] = 0x761B;
            mapDataStd [rangeNo] [0xE6B8 - rangeMin] = 0x769C;
            mapDataStd [rangeNo] [0xE6B9 - rangeMin] = 0x769D;
            mapDataStd [rangeNo] [0xE6BA - rangeMin] = 0x769E;
            mapDataStd [rangeNo] [0xE6BB - rangeMin] = 0x769B;
            mapDataStd [rangeNo] [0xE6BC - rangeMin] = 0x778D;
            mapDataStd [rangeNo] [0xE6BD - rangeMin] = 0x778F;
            mapDataStd [rangeNo] [0xE6BE - rangeMin] = 0x7789;
            mapDataStd [rangeNo] [0xE6BF - rangeMin] = 0x7788;

            mapDataStd [rangeNo] [0xE6C0 - rangeMin] = 0x78CD;
            mapDataStd [rangeNo] [0xE6C1 - rangeMin] = 0x78BB;
            mapDataStd [rangeNo] [0xE6C2 - rangeMin] = 0x78CF;
            mapDataStd [rangeNo] [0xE6C3 - rangeMin] = 0x78CC;
            mapDataStd [rangeNo] [0xE6C4 - rangeMin] = 0x78D1;
            mapDataStd [rangeNo] [0xE6C5 - rangeMin] = 0x78CE;
            mapDataStd [rangeNo] [0xE6C6 - rangeMin] = 0x78D4;
            mapDataStd [rangeNo] [0xE6C7 - rangeMin] = 0x78C8;
            mapDataStd [rangeNo] [0xE6C8 - rangeMin] = 0x78C3;
            mapDataStd [rangeNo] [0xE6C9 - rangeMin] = 0x78C4;
            mapDataStd [rangeNo] [0xE6CA - rangeMin] = 0x78C9;
            mapDataStd [rangeNo] [0xE6CB - rangeMin] = 0x799A;
            mapDataStd [rangeNo] [0xE6CC - rangeMin] = 0x79A1;
            mapDataStd [rangeNo] [0xE6CD - rangeMin] = 0x79A0;
            mapDataStd [rangeNo] [0xE6CE - rangeMin] = 0x799C;
            mapDataStd [rangeNo] [0xE6CF - rangeMin] = 0x79A2;

            mapDataStd [rangeNo] [0xE6D0 - rangeMin] = 0x799B;
            mapDataStd [rangeNo] [0xE6D1 - rangeMin] = 0x6B76;
            mapDataStd [rangeNo] [0xE6D2 - rangeMin] = 0x7A39;
            mapDataStd [rangeNo] [0xE6D3 - rangeMin] = 0x7AB2;
            mapDataStd [rangeNo] [0xE6D4 - rangeMin] = 0x7AB4;
            mapDataStd [rangeNo] [0xE6D5 - rangeMin] = 0x7AB3;
            mapDataStd [rangeNo] [0xE6D6 - rangeMin] = 0x7BB7;
            mapDataStd [rangeNo] [0xE6D7 - rangeMin] = 0x7BCB;
            mapDataStd [rangeNo] [0xE6D8 - rangeMin] = 0x7BBE;
            mapDataStd [rangeNo] [0xE6D9 - rangeMin] = 0x7BAC;
            mapDataStd [rangeNo] [0xE6DA - rangeMin] = 0x7BCE;
            mapDataStd [rangeNo] [0xE6DB - rangeMin] = 0x7BAF;
            mapDataStd [rangeNo] [0xE6DC - rangeMin] = 0x7BB9;
            mapDataStd [rangeNo] [0xE6DD - rangeMin] = 0x7BCA;
            mapDataStd [rangeNo] [0xE6DE - rangeMin] = 0x7BB5;
            mapDataStd [rangeNo] [0xE6DF - rangeMin] = 0x7CC5;

            mapDataStd [rangeNo] [0xE6E0 - rangeMin] = 0x7CC8;
            mapDataStd [rangeNo] [0xE6E1 - rangeMin] = 0x7CCC;
            mapDataStd [rangeNo] [0xE6E2 - rangeMin] = 0x7CCB;
            mapDataStd [rangeNo] [0xE6E3 - rangeMin] = 0x7DF7;
            mapDataStd [rangeNo] [0xE6E4 - rangeMin] = 0x7DDB;
            mapDataStd [rangeNo] [0xE6E5 - rangeMin] = 0x7DEA;
            mapDataStd [rangeNo] [0xE6E6 - rangeMin] = 0x7DE7;
            mapDataStd [rangeNo] [0xE6E7 - rangeMin] = 0x7DD7;
            mapDataStd [rangeNo] [0xE6E8 - rangeMin] = 0x7DE1;
            mapDataStd [rangeNo] [0xE6E9 - rangeMin] = 0x7E03;
            mapDataStd [rangeNo] [0xE6EA - rangeMin] = 0x7DFA;
            mapDataStd [rangeNo] [0xE6EB - rangeMin] = 0x7DE6;
            mapDataStd [rangeNo] [0xE6EC - rangeMin] = 0x7DF6;
            mapDataStd [rangeNo] [0xE6ED - rangeMin] = 0x7DF1;
            mapDataStd [rangeNo] [0xE6EE - rangeMin] = 0x7DF0;
            mapDataStd [rangeNo] [0xE6EF - rangeMin] = 0x7DEE;

            mapDataStd [rangeNo] [0xE6F0 - rangeMin] = 0x7DDF;
            mapDataStd [rangeNo] [0xE6F1 - rangeMin] = 0x7F76;
            mapDataStd [rangeNo] [0xE6F2 - rangeMin] = 0x7FAC;
            mapDataStd [rangeNo] [0xE6F3 - rangeMin] = 0x7FB0;
            mapDataStd [rangeNo] [0xE6F4 - rangeMin] = 0x7FAD;
            mapDataStd [rangeNo] [0xE6F5 - rangeMin] = 0x7FED;
            mapDataStd [rangeNo] [0xE6F6 - rangeMin] = 0x7FEB;
            mapDataStd [rangeNo] [0xE6F7 - rangeMin] = 0x7FEA;
            mapDataStd [rangeNo] [0xE6F8 - rangeMin] = 0x7FEC;
            mapDataStd [rangeNo] [0xE6F9 - rangeMin] = 0x7FE6;
            mapDataStd [rangeNo] [0xE6FA - rangeMin] = 0x7FE8;
            mapDataStd [rangeNo] [0xE6FB - rangeMin] = 0x8064;
            mapDataStd [rangeNo] [0xE6FC - rangeMin] = 0x8067;
            mapDataStd [rangeNo] [0xE6FD - rangeMin] = 0x81A3;
            mapDataStd [rangeNo] [0xE6FE - rangeMin] = 0x819F;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xE740 - rangeMin] = 0x819E;
            mapDataStd [rangeNo] [0xE741 - rangeMin] = 0x8195;
            mapDataStd [rangeNo] [0xE742 - rangeMin] = 0x81A2;
            mapDataStd [rangeNo] [0xE743 - rangeMin] = 0x8199;
            mapDataStd [rangeNo] [0xE744 - rangeMin] = 0x8197;
            mapDataStd [rangeNo] [0xE745 - rangeMin] = 0x8216;
            mapDataStd [rangeNo] [0xE746 - rangeMin] = 0x824F;
            mapDataStd [rangeNo] [0xE747 - rangeMin] = 0x8253;
            mapDataStd [rangeNo] [0xE748 - rangeMin] = 0x8252;
            mapDataStd [rangeNo] [0xE749 - rangeMin] = 0x8250;
            mapDataStd [rangeNo] [0xE74A - rangeMin] = 0x824E;
            mapDataStd [rangeNo] [0xE74B - rangeMin] = 0x8251;
            mapDataStd [rangeNo] [0xE74C - rangeMin] = 0x8524;
            mapDataStd [rangeNo] [0xE74D - rangeMin] = 0x853B;
            mapDataStd [rangeNo] [0xE74E - rangeMin] = 0x850F;
            mapDataStd [rangeNo] [0xE74F - rangeMin] = 0x8500;

            mapDataStd [rangeNo] [0xE750 - rangeMin] = 0x8529;
            mapDataStd [rangeNo] [0xE751 - rangeMin] = 0x850E;
            mapDataStd [rangeNo] [0xE752 - rangeMin] = 0x8509;
            mapDataStd [rangeNo] [0xE753 - rangeMin] = 0x850D;
            mapDataStd [rangeNo] [0xE754 - rangeMin] = 0x851F;
            mapDataStd [rangeNo] [0xE755 - rangeMin] = 0x850A;
            mapDataStd [rangeNo] [0xE756 - rangeMin] = 0x8527;
            mapDataStd [rangeNo] [0xE757 - rangeMin] = 0x851C;
            mapDataStd [rangeNo] [0xE758 - rangeMin] = 0x84FB;
            mapDataStd [rangeNo] [0xE759 - rangeMin] = 0x852B;
            mapDataStd [rangeNo] [0xE75A - rangeMin] = 0x84FA;
            mapDataStd [rangeNo] [0xE75B - rangeMin] = 0x8508;
            mapDataStd [rangeNo] [0xE75C - rangeMin] = 0x850C;
            mapDataStd [rangeNo] [0xE75D - rangeMin] = 0x84F4;
            mapDataStd [rangeNo] [0xE75E - rangeMin] = 0x852A;
            mapDataStd [rangeNo] [0xE75F - rangeMin] = 0x84F2;

            mapDataStd [rangeNo] [0xE760 - rangeMin] = 0x8515;
            mapDataStd [rangeNo] [0xE761 - rangeMin] = 0x84F7;
            mapDataStd [rangeNo] [0xE762 - rangeMin] = 0x84EB;
            mapDataStd [rangeNo] [0xE763 - rangeMin] = 0x84F3;
            mapDataStd [rangeNo] [0xE764 - rangeMin] = 0x84FC;
            mapDataStd [rangeNo] [0xE765 - rangeMin] = 0x8512;
            mapDataStd [rangeNo] [0xE766 - rangeMin] = 0x84EA;
            mapDataStd [rangeNo] [0xE767 - rangeMin] = 0x84E9;
            mapDataStd [rangeNo] [0xE768 - rangeMin] = 0x8516;
            mapDataStd [rangeNo] [0xE769 - rangeMin] = 0x84FE;
            mapDataStd [rangeNo] [0xE76A - rangeMin] = 0x8528;
            mapDataStd [rangeNo] [0xE76B - rangeMin] = 0x851D;
            mapDataStd [rangeNo] [0xE76C - rangeMin] = 0x852E;
            mapDataStd [rangeNo] [0xE76D - rangeMin] = 0x8502;
            mapDataStd [rangeNo] [0xE76E - rangeMin] = 0x84FD;
            mapDataStd [rangeNo] [0xE76F - rangeMin] = 0x851E;

            mapDataStd [rangeNo] [0xE770 - rangeMin] = 0x84F6;
            mapDataStd [rangeNo] [0xE771 - rangeMin] = 0x8531;
            mapDataStd [rangeNo] [0xE772 - rangeMin] = 0x8526;
            mapDataStd [rangeNo] [0xE773 - rangeMin] = 0x84E7;
            mapDataStd [rangeNo] [0xE774 - rangeMin] = 0x84E8;
            mapDataStd [rangeNo] [0xE775 - rangeMin] = 0x84F0;
            mapDataStd [rangeNo] [0xE776 - rangeMin] = 0x84EF;
            mapDataStd [rangeNo] [0xE777 - rangeMin] = 0x84F9;
            mapDataStd [rangeNo] [0xE778 - rangeMin] = 0x8518;
            mapDataStd [rangeNo] [0xE779 - rangeMin] = 0x8520;
            mapDataStd [rangeNo] [0xE77A - rangeMin] = 0x8530;
            mapDataStd [rangeNo] [0xE77B - rangeMin] = 0x850B;
            mapDataStd [rangeNo] [0xE77C - rangeMin] = 0x8519;
            mapDataStd [rangeNo] [0xE77D - rangeMin] = 0x852F;
            mapDataStd [rangeNo] [0xE77E - rangeMin] = 0x8662;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xE7A1 - rangeMin] = 0x8756;
            mapDataStd [rangeNo] [0xE7A2 - rangeMin] = 0x8763;
            mapDataStd [rangeNo] [0xE7A3 - rangeMin] = 0x8764;
            mapDataStd [rangeNo] [0xE7A4 - rangeMin] = 0x8777;
            mapDataStd [rangeNo] [0xE7A5 - rangeMin] = 0x87E1;
            mapDataStd [rangeNo] [0xE7A6 - rangeMin] = 0x8773;
            mapDataStd [rangeNo] [0xE7A7 - rangeMin] = 0x8758;
            mapDataStd [rangeNo] [0xE7A8 - rangeMin] = 0x8754;
            mapDataStd [rangeNo] [0xE7A9 - rangeMin] = 0x875B;
            mapDataStd [rangeNo] [0xE7AA - rangeMin] = 0x8752;
            mapDataStd [rangeNo] [0xE7AB - rangeMin] = 0x8761;
            mapDataStd [rangeNo] [0xE7AC - rangeMin] = 0x875A;
            mapDataStd [rangeNo] [0xE7AD - rangeMin] = 0x8751;
            mapDataStd [rangeNo] [0xE7AE - rangeMin] = 0x875E;
            mapDataStd [rangeNo] [0xE7AF - rangeMin] = 0x876D;

            mapDataStd [rangeNo] [0xE7B0 - rangeMin] = 0x876A;
            mapDataStd [rangeNo] [0xE7B1 - rangeMin] = 0x8750;
            mapDataStd [rangeNo] [0xE7B2 - rangeMin] = 0x874E;
            mapDataStd [rangeNo] [0xE7B3 - rangeMin] = 0x875F;
            mapDataStd [rangeNo] [0xE7B4 - rangeMin] = 0x875D;
            mapDataStd [rangeNo] [0xE7B5 - rangeMin] = 0x876F;
            mapDataStd [rangeNo] [0xE7B6 - rangeMin] = 0x876C;
            mapDataStd [rangeNo] [0xE7B7 - rangeMin] = 0x877A;
            mapDataStd [rangeNo] [0xE7B8 - rangeMin] = 0x876E;
            mapDataStd [rangeNo] [0xE7B9 - rangeMin] = 0x875C;
            mapDataStd [rangeNo] [0xE7BA - rangeMin] = 0x8765;
            mapDataStd [rangeNo] [0xE7BB - rangeMin] = 0x874F;
            mapDataStd [rangeNo] [0xE7BC - rangeMin] = 0x877B;
            mapDataStd [rangeNo] [0xE7BD - rangeMin] = 0x8775;
            mapDataStd [rangeNo] [0xE7BE - rangeMin] = 0x8762;
            mapDataStd [rangeNo] [0xE7BF - rangeMin] = 0x8767;

            mapDataStd [rangeNo] [0xE7C0 - rangeMin] = 0x8769;
            mapDataStd [rangeNo] [0xE7C1 - rangeMin] = 0x885A;
            mapDataStd [rangeNo] [0xE7C2 - rangeMin] = 0x8905;
            mapDataStd [rangeNo] [0xE7C3 - rangeMin] = 0x890C;
            mapDataStd [rangeNo] [0xE7C4 - rangeMin] = 0x8914;
            mapDataStd [rangeNo] [0xE7C5 - rangeMin] = 0x890B;
            mapDataStd [rangeNo] [0xE7C6 - rangeMin] = 0x8917;
            mapDataStd [rangeNo] [0xE7C7 - rangeMin] = 0x8918;
            mapDataStd [rangeNo] [0xE7C8 - rangeMin] = 0x8919;
            mapDataStd [rangeNo] [0xE7C9 - rangeMin] = 0x8906;
            mapDataStd [rangeNo] [0xE7CA - rangeMin] = 0x8916;
            mapDataStd [rangeNo] [0xE7CB - rangeMin] = 0x8911;
            mapDataStd [rangeNo] [0xE7CC - rangeMin] = 0x890E;
            mapDataStd [rangeNo] [0xE7CD - rangeMin] = 0x8909;
            mapDataStd [rangeNo] [0xE7CE - rangeMin] = 0x89A2;
            mapDataStd [rangeNo] [0xE7CF - rangeMin] = 0x89A4;

            mapDataStd [rangeNo] [0xE7D0 - rangeMin] = 0x89A3;
            mapDataStd [rangeNo] [0xE7D1 - rangeMin] = 0x89ED;
            mapDataStd [rangeNo] [0xE7D2 - rangeMin] = 0x89F0;
            mapDataStd [rangeNo] [0xE7D3 - rangeMin] = 0x89EC;
            mapDataStd [rangeNo] [0xE7D4 - rangeMin] = 0x8ACF;
            mapDataStd [rangeNo] [0xE7D5 - rangeMin] = 0x8AC6;
            mapDataStd [rangeNo] [0xE7D6 - rangeMin] = 0x8AB8;
            mapDataStd [rangeNo] [0xE7D7 - rangeMin] = 0x8AD3;
            mapDataStd [rangeNo] [0xE7D8 - rangeMin] = 0x8AD1;
            mapDataStd [rangeNo] [0xE7D9 - rangeMin] = 0x8AD4;
            mapDataStd [rangeNo] [0xE7DA - rangeMin] = 0x8AD5;
            mapDataStd [rangeNo] [0xE7DB - rangeMin] = 0x8ABB;
            mapDataStd [rangeNo] [0xE7DC - rangeMin] = 0x8AD7;
            mapDataStd [rangeNo] [0xE7DD - rangeMin] = 0x8ABE;
            mapDataStd [rangeNo] [0xE7DE - rangeMin] = 0x8AC0;
            mapDataStd [rangeNo] [0xE7DF - rangeMin] = 0x8AC5;

            mapDataStd [rangeNo] [0xE7E0 - rangeMin] = 0x8AD8;
            mapDataStd [rangeNo] [0xE7E1 - rangeMin] = 0x8AC3;
            mapDataStd [rangeNo] [0xE7E2 - rangeMin] = 0x8ABA;
            mapDataStd [rangeNo] [0xE7E3 - rangeMin] = 0x8ABD;
            mapDataStd [rangeNo] [0xE7E4 - rangeMin] = 0x8AD9;
            mapDataStd [rangeNo] [0xE7E5 - rangeMin] = 0x8C3E;
            mapDataStd [rangeNo] [0xE7E6 - rangeMin] = 0x8C4D;
            mapDataStd [rangeNo] [0xE7E7 - rangeMin] = 0x8C8F;
            mapDataStd [rangeNo] [0xE7E8 - rangeMin] = 0x8CE5;
            mapDataStd [rangeNo] [0xE7E9 - rangeMin] = 0x8CDF;
            mapDataStd [rangeNo] [0xE7EA - rangeMin] = 0x8CD9;
            mapDataStd [rangeNo] [0xE7EB - rangeMin] = 0x8CE8;
            mapDataStd [rangeNo] [0xE7EC - rangeMin] = 0x8CDA;
            mapDataStd [rangeNo] [0xE7ED - rangeMin] = 0x8CDD;
            mapDataStd [rangeNo] [0xE7EE - rangeMin] = 0x8CE7;
            mapDataStd [rangeNo] [0xE7EF - rangeMin] = 0x8DA0;

            mapDataStd [rangeNo] [0xE7F0 - rangeMin] = 0x8D9C;
            mapDataStd [rangeNo] [0xE7F1 - rangeMin] = 0x8DA1;
            mapDataStd [rangeNo] [0xE7F2 - rangeMin] = 0x8D9B;
            mapDataStd [rangeNo] [0xE7F3 - rangeMin] = 0x8E20;
            mapDataStd [rangeNo] [0xE7F4 - rangeMin] = 0x8E23;
            mapDataStd [rangeNo] [0xE7F5 - rangeMin] = 0x8E25;
            mapDataStd [rangeNo] [0xE7F6 - rangeMin] = 0x8E24;
            mapDataStd [rangeNo] [0xE7F7 - rangeMin] = 0x8E2E;
            mapDataStd [rangeNo] [0xE7F8 - rangeMin] = 0x8E15;
            mapDataStd [rangeNo] [0xE7F9 - rangeMin] = 0x8E1B;
            mapDataStd [rangeNo] [0xE7FA - rangeMin] = 0x8E16;
            mapDataStd [rangeNo] [0xE7FB - rangeMin] = 0x8E11;
            mapDataStd [rangeNo] [0xE7FC - rangeMin] = 0x8E19;
            mapDataStd [rangeNo] [0xE7FD - rangeMin] = 0x8E26;
            mapDataStd [rangeNo] [0xE7FE - rangeMin] = 0x8E27;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xE840 - rangeMin] = 0x8E14;
            mapDataStd [rangeNo] [0xE841 - rangeMin] = 0x8E12;
            mapDataStd [rangeNo] [0xE842 - rangeMin] = 0x8E18;
            mapDataStd [rangeNo] [0xE843 - rangeMin] = 0x8E13;
            mapDataStd [rangeNo] [0xE844 - rangeMin] = 0x8E1C;
            mapDataStd [rangeNo] [0xE845 - rangeMin] = 0x8E17;
            mapDataStd [rangeNo] [0xE846 - rangeMin] = 0x8E1A;
            mapDataStd [rangeNo] [0xE847 - rangeMin] = 0x8F2C;
            mapDataStd [rangeNo] [0xE848 - rangeMin] = 0x8F24;
            mapDataStd [rangeNo] [0xE849 - rangeMin] = 0x8F18;
            mapDataStd [rangeNo] [0xE84A - rangeMin] = 0x8F1A;
            mapDataStd [rangeNo] [0xE84B - rangeMin] = 0x8F20;
            mapDataStd [rangeNo] [0xE84C - rangeMin] = 0x8F23;
            mapDataStd [rangeNo] [0xE84D - rangeMin] = 0x8F16;
            mapDataStd [rangeNo] [0xE84E - rangeMin] = 0x8F17;
            mapDataStd [rangeNo] [0xE84F - rangeMin] = 0x9073;

            mapDataStd [rangeNo] [0xE850 - rangeMin] = 0x9070;
            mapDataStd [rangeNo] [0xE851 - rangeMin] = 0x906F;
            mapDataStd [rangeNo] [0xE852 - rangeMin] = 0x9067;
            mapDataStd [rangeNo] [0xE853 - rangeMin] = 0x906B;
            mapDataStd [rangeNo] [0xE854 - rangeMin] = 0x912F;
            mapDataStd [rangeNo] [0xE855 - rangeMin] = 0x912B;
            mapDataStd [rangeNo] [0xE856 - rangeMin] = 0x9129;
            mapDataStd [rangeNo] [0xE857 - rangeMin] = 0x912A;
            mapDataStd [rangeNo] [0xE858 - rangeMin] = 0x9132;
            mapDataStd [rangeNo] [0xE859 - rangeMin] = 0x9126;
            mapDataStd [rangeNo] [0xE85A - rangeMin] = 0x912E;
            mapDataStd [rangeNo] [0xE85B - rangeMin] = 0x9185;
            mapDataStd [rangeNo] [0xE85C - rangeMin] = 0x9186;
            mapDataStd [rangeNo] [0xE85D - rangeMin] = 0x918A;
            mapDataStd [rangeNo] [0xE85E - rangeMin] = 0x9181;
            mapDataStd [rangeNo] [0xE85F - rangeMin] = 0x9182;

            mapDataStd [rangeNo] [0xE860 - rangeMin] = 0x9184;
            mapDataStd [rangeNo] [0xE861 - rangeMin] = 0x9180;
            mapDataStd [rangeNo] [0xE862 - rangeMin] = 0x92D0;
            mapDataStd [rangeNo] [0xE863 - rangeMin] = 0x92C3;
            mapDataStd [rangeNo] [0xE864 - rangeMin] = 0x92C4;
            mapDataStd [rangeNo] [0xE865 - rangeMin] = 0x92C0;
            mapDataStd [rangeNo] [0xE866 - rangeMin] = 0x92D9;
            mapDataStd [rangeNo] [0xE867 - rangeMin] = 0x92B6;
            mapDataStd [rangeNo] [0xE868 - rangeMin] = 0x92CF;
            mapDataStd [rangeNo] [0xE869 - rangeMin] = 0x92F1;
            mapDataStd [rangeNo] [0xE86A - rangeMin] = 0x92DF;
            mapDataStd [rangeNo] [0xE86B - rangeMin] = 0x92D8;
            mapDataStd [rangeNo] [0xE86C - rangeMin] = 0x92E9;
            mapDataStd [rangeNo] [0xE86D - rangeMin] = 0x92D7;
            mapDataStd [rangeNo] [0xE86E - rangeMin] = 0x92DD;
            mapDataStd [rangeNo] [0xE86F - rangeMin] = 0x92CC;

            mapDataStd [rangeNo] [0xE870 - rangeMin] = 0x92EF;
            mapDataStd [rangeNo] [0xE871 - rangeMin] = 0x92C2;
            mapDataStd [rangeNo] [0xE872 - rangeMin] = 0x92E8;
            mapDataStd [rangeNo] [0xE873 - rangeMin] = 0x92CA;
            mapDataStd [rangeNo] [0xE874 - rangeMin] = 0x92C8;
            mapDataStd [rangeNo] [0xE875 - rangeMin] = 0x92CE;
            mapDataStd [rangeNo] [0xE876 - rangeMin] = 0x92E6;
            mapDataStd [rangeNo] [0xE877 - rangeMin] = 0x92CD;
            mapDataStd [rangeNo] [0xE878 - rangeMin] = 0x92D5;
            mapDataStd [rangeNo] [0xE879 - rangeMin] = 0x92C9;
            mapDataStd [rangeNo] [0xE87A - rangeMin] = 0x92E0;
            mapDataStd [rangeNo] [0xE87B - rangeMin] = 0x92DE;
            mapDataStd [rangeNo] [0xE87C - rangeMin] = 0x92E7;
            mapDataStd [rangeNo] [0xE87D - rangeMin] = 0x92D1;
            mapDataStd [rangeNo] [0xE87E - rangeMin] = 0x92D3;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xE8A1 - rangeMin] = 0x92B5;
            mapDataStd [rangeNo] [0xE8A2 - rangeMin] = 0x92E1;
            mapDataStd [rangeNo] [0xE8A3 - rangeMin] = 0x92C6;
            mapDataStd [rangeNo] [0xE8A4 - rangeMin] = 0x92B4;
            mapDataStd [rangeNo] [0xE8A5 - rangeMin] = 0x957C;
            mapDataStd [rangeNo] [0xE8A6 - rangeMin] = 0x95AC;
            mapDataStd [rangeNo] [0xE8A7 - rangeMin] = 0x95AB;
            mapDataStd [rangeNo] [0xE8A8 - rangeMin] = 0x95AE;
            mapDataStd [rangeNo] [0xE8A9 - rangeMin] = 0x95B0;
            mapDataStd [rangeNo] [0xE8AA - rangeMin] = 0x96A4;
            mapDataStd [rangeNo] [0xE8AB - rangeMin] = 0x96A2;
            mapDataStd [rangeNo] [0xE8AC - rangeMin] = 0x96D3;
            mapDataStd [rangeNo] [0xE8AD - rangeMin] = 0x9705;
            mapDataStd [rangeNo] [0xE8AE - rangeMin] = 0x9708;
            mapDataStd [rangeNo] [0xE8AF - rangeMin] = 0x9702;

            mapDataStd [rangeNo] [0xE8B0 - rangeMin] = 0x975A;
            mapDataStd [rangeNo] [0xE8B1 - rangeMin] = 0x978A;
            mapDataStd [rangeNo] [0xE8B2 - rangeMin] = 0x978E;
            mapDataStd [rangeNo] [0xE8B3 - rangeMin] = 0x9788;
            mapDataStd [rangeNo] [0xE8B4 - rangeMin] = 0x97D0;
            mapDataStd [rangeNo] [0xE8B5 - rangeMin] = 0x97CF;
            mapDataStd [rangeNo] [0xE8B6 - rangeMin] = 0x981E;
            mapDataStd [rangeNo] [0xE8B7 - rangeMin] = 0x981D;
            mapDataStd [rangeNo] [0xE8B8 - rangeMin] = 0x9826;
            mapDataStd [rangeNo] [0xE8B9 - rangeMin] = 0x9829;
            mapDataStd [rangeNo] [0xE8BA - rangeMin] = 0x9828;
            mapDataStd [rangeNo] [0xE8BB - rangeMin] = 0x9820;
            mapDataStd [rangeNo] [0xE8BC - rangeMin] = 0x981B;
            mapDataStd [rangeNo] [0xE8BD - rangeMin] = 0x9827;
            mapDataStd [rangeNo] [0xE8BE - rangeMin] = 0x98B2;
            mapDataStd [rangeNo] [0xE8BF - rangeMin] = 0x9908;

            mapDataStd [rangeNo] [0xE8C0 - rangeMin] = 0x98FA;
            mapDataStd [rangeNo] [0xE8C1 - rangeMin] = 0x9911;
            mapDataStd [rangeNo] [0xE8C2 - rangeMin] = 0x9914;
            mapDataStd [rangeNo] [0xE8C3 - rangeMin] = 0x9916;
            mapDataStd [rangeNo] [0xE8C4 - rangeMin] = 0x9917;
            mapDataStd [rangeNo] [0xE8C5 - rangeMin] = 0x9915;
            mapDataStd [rangeNo] [0xE8C6 - rangeMin] = 0x99DC;
            mapDataStd [rangeNo] [0xE8C7 - rangeMin] = 0x99CD;
            mapDataStd [rangeNo] [0xE8C8 - rangeMin] = 0x99CF;
            mapDataStd [rangeNo] [0xE8C9 - rangeMin] = 0x99D3;
            mapDataStd [rangeNo] [0xE8CA - rangeMin] = 0x99D4;
            mapDataStd [rangeNo] [0xE8CB - rangeMin] = 0x99CE;
            mapDataStd [rangeNo] [0xE8CC - rangeMin] = 0x99C9;
            mapDataStd [rangeNo] [0xE8CD - rangeMin] = 0x99D6;
            mapDataStd [rangeNo] [0xE8CE - rangeMin] = 0x99D8;
            mapDataStd [rangeNo] [0xE8CF - rangeMin] = 0x99CB;

            mapDataStd [rangeNo] [0xE8D0 - rangeMin] = 0x99D7;
            mapDataStd [rangeNo] [0xE8D1 - rangeMin] = 0x99CC;
            mapDataStd [rangeNo] [0xE8D2 - rangeMin] = 0x9AB3;
            mapDataStd [rangeNo] [0xE8D3 - rangeMin] = 0x9AEC;
            mapDataStd [rangeNo] [0xE8D4 - rangeMin] = 0x9AEB;
            mapDataStd [rangeNo] [0xE8D5 - rangeMin] = 0x9AF3;
            mapDataStd [rangeNo] [0xE8D6 - rangeMin] = 0x9AF2;
            mapDataStd [rangeNo] [0xE8D7 - rangeMin] = 0x9AF1;
            mapDataStd [rangeNo] [0xE8D8 - rangeMin] = 0x9B46;
            mapDataStd [rangeNo] [0xE8D9 - rangeMin] = 0x9B43;
            mapDataStd [rangeNo] [0xE8DA - rangeMin] = 0x9B67;
            mapDataStd [rangeNo] [0xE8DB - rangeMin] = 0x9B74;
            mapDataStd [rangeNo] [0xE8DC - rangeMin] = 0x9B71;
            mapDataStd [rangeNo] [0xE8DD - rangeMin] = 0x9B66;
            mapDataStd [rangeNo] [0xE8DE - rangeMin] = 0x9B76;
            mapDataStd [rangeNo] [0xE8DF - rangeMin] = 0x9B75;

            mapDataStd [rangeNo] [0xE8E0 - rangeMin] = 0x9B70;
            mapDataStd [rangeNo] [0xE8E1 - rangeMin] = 0x9B68;
            mapDataStd [rangeNo] [0xE8E2 - rangeMin] = 0x9B64;
            mapDataStd [rangeNo] [0xE8E3 - rangeMin] = 0x9B6C;
            mapDataStd [rangeNo] [0xE8E4 - rangeMin] = 0x9CFC;
            mapDataStd [rangeNo] [0xE8E5 - rangeMin] = 0x9CFA;
            mapDataStd [rangeNo] [0xE8E6 - rangeMin] = 0x9CFD;
            mapDataStd [rangeNo] [0xE8E7 - rangeMin] = 0x9CFF;
            mapDataStd [rangeNo] [0xE8E8 - rangeMin] = 0x9CF7;
            mapDataStd [rangeNo] [0xE8E9 - rangeMin] = 0x9D07;
            mapDataStd [rangeNo] [0xE8EA - rangeMin] = 0x9D00;
            mapDataStd [rangeNo] [0xE8EB - rangeMin] = 0x9CF9;
            mapDataStd [rangeNo] [0xE8EC - rangeMin] = 0x9CFB;
            mapDataStd [rangeNo] [0xE8ED - rangeMin] = 0x9D08;
            mapDataStd [rangeNo] [0xE8EE - rangeMin] = 0x9D05;
            mapDataStd [rangeNo] [0xE8EF - rangeMin] = 0x9D04;

            mapDataStd [rangeNo] [0xE8F0 - rangeMin] = 0x9E83;
            mapDataStd [rangeNo] [0xE8F1 - rangeMin] = 0x9ED3;
            mapDataStd [rangeNo] [0xE8F2 - rangeMin] = 0x9F0F;
            mapDataStd [rangeNo] [0xE8F3 - rangeMin] = 0x9F10;
            mapDataStd [rangeNo] [0xE8F4 - rangeMin] = 0x511C;
            mapDataStd [rangeNo] [0xE8F5 - rangeMin] = 0x5113;
            mapDataStd [rangeNo] [0xE8F6 - rangeMin] = 0x5117;
            mapDataStd [rangeNo] [0xE8F7 - rangeMin] = 0x511A;
            mapDataStd [rangeNo] [0xE8F8 - rangeMin] = 0x5111;
            mapDataStd [rangeNo] [0xE8F9 - rangeMin] = 0x51DE;
            mapDataStd [rangeNo] [0xE8FA - rangeMin] = 0x5334;
            mapDataStd [rangeNo] [0xE8FB - rangeMin] = 0x53E1;
            mapDataStd [rangeNo] [0xE8FC - rangeMin] = 0x5670;
            mapDataStd [rangeNo] [0xE8FD - rangeMin] = 0x5660;
            mapDataStd [rangeNo] [0xE8FE - rangeMin] = 0x566E;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xE940 - rangeMin] = 0x5673;
            mapDataStd [rangeNo] [0xE941 - rangeMin] = 0x5666;
            mapDataStd [rangeNo] [0xE942 - rangeMin] = 0x5663;
            mapDataStd [rangeNo] [0xE943 - rangeMin] = 0x566D;
            mapDataStd [rangeNo] [0xE944 - rangeMin] = 0x5672;
            mapDataStd [rangeNo] [0xE945 - rangeMin] = 0x565E;
            mapDataStd [rangeNo] [0xE946 - rangeMin] = 0x5677;
            mapDataStd [rangeNo] [0xE947 - rangeMin] = 0x571C;
            mapDataStd [rangeNo] [0xE948 - rangeMin] = 0x571B;
            mapDataStd [rangeNo] [0xE949 - rangeMin] = 0x58C8;
            mapDataStd [rangeNo] [0xE94A - rangeMin] = 0x58BD;
            mapDataStd [rangeNo] [0xE94B - rangeMin] = 0x58C9;
            mapDataStd [rangeNo] [0xE94C - rangeMin] = 0x58BF;
            mapDataStd [rangeNo] [0xE94D - rangeMin] = 0x58BA;
            mapDataStd [rangeNo] [0xE94E - rangeMin] = 0x58C2;
            mapDataStd [rangeNo] [0xE94F - rangeMin] = 0x58BC;

            mapDataStd [rangeNo] [0xE950 - rangeMin] = 0x58C6;
            mapDataStd [rangeNo] [0xE951 - rangeMin] = 0x5B17;
            mapDataStd [rangeNo] [0xE952 - rangeMin] = 0x5B19;
            mapDataStd [rangeNo] [0xE953 - rangeMin] = 0x5B1B;
            mapDataStd [rangeNo] [0xE954 - rangeMin] = 0x5B21;
            mapDataStd [rangeNo] [0xE955 - rangeMin] = 0x5B14;
            mapDataStd [rangeNo] [0xE956 - rangeMin] = 0x5B13;
            mapDataStd [rangeNo] [0xE957 - rangeMin] = 0x5B10;
            mapDataStd [rangeNo] [0xE958 - rangeMin] = 0x5B16;
            mapDataStd [rangeNo] [0xE959 - rangeMin] = 0x5B28;
            mapDataStd [rangeNo] [0xE95A - rangeMin] = 0x5B1A;
            mapDataStd [rangeNo] [0xE95B - rangeMin] = 0x5B20;
            mapDataStd [rangeNo] [0xE95C - rangeMin] = 0x5B1E;
            mapDataStd [rangeNo] [0xE95D - rangeMin] = 0x5BEF;
            mapDataStd [rangeNo] [0xE95E - rangeMin] = 0x5DAC;
            mapDataStd [rangeNo] [0xE95F - rangeMin] = 0x5DB1;

            mapDataStd [rangeNo] [0xE960 - rangeMin] = 0x5DA9;
            mapDataStd [rangeNo] [0xE961 - rangeMin] = 0x5DA7;
            mapDataStd [rangeNo] [0xE962 - rangeMin] = 0x5DB5;
            mapDataStd [rangeNo] [0xE963 - rangeMin] = 0x5DB0;
            mapDataStd [rangeNo] [0xE964 - rangeMin] = 0x5DAE;
            mapDataStd [rangeNo] [0xE965 - rangeMin] = 0x5DAA;
            mapDataStd [rangeNo] [0xE966 - rangeMin] = 0x5DA8;
            mapDataStd [rangeNo] [0xE967 - rangeMin] = 0x5DB2;
            mapDataStd [rangeNo] [0xE968 - rangeMin] = 0x5DAD;
            mapDataStd [rangeNo] [0xE969 - rangeMin] = 0x5DAF;
            mapDataStd [rangeNo] [0xE96A - rangeMin] = 0x5DB4;
            mapDataStd [rangeNo] [0xE96B - rangeMin] = 0x5E67;
            mapDataStd [rangeNo] [0xE96C - rangeMin] = 0x5E68;
            mapDataStd [rangeNo] [0xE96D - rangeMin] = 0x5E66;
            mapDataStd [rangeNo] [0xE96E - rangeMin] = 0x5E6F;
            mapDataStd [rangeNo] [0xE96F - rangeMin] = 0x5EE9;

            mapDataStd [rangeNo] [0xE970 - rangeMin] = 0x5EE7;
            mapDataStd [rangeNo] [0xE971 - rangeMin] = 0x5EE6;
            mapDataStd [rangeNo] [0xE972 - rangeMin] = 0x5EE8;
            mapDataStd [rangeNo] [0xE973 - rangeMin] = 0x5EE5;
            mapDataStd [rangeNo] [0xE974 - rangeMin] = 0x5F4B;
            mapDataStd [rangeNo] [0xE975 - rangeMin] = 0x5FBC;
            mapDataStd [rangeNo] [0xE976 - rangeMin] = 0x619D;
            mapDataStd [rangeNo] [0xE977 - rangeMin] = 0x61A8;
            mapDataStd [rangeNo] [0xE978 - rangeMin] = 0x6196;
            mapDataStd [rangeNo] [0xE979 - rangeMin] = 0x61C5;
            mapDataStd [rangeNo] [0xE97A - rangeMin] = 0x61B4;
            mapDataStd [rangeNo] [0xE97B - rangeMin] = 0x61C6;
            mapDataStd [rangeNo] [0xE97C - rangeMin] = 0x61C1;
            mapDataStd [rangeNo] [0xE97D - rangeMin] = 0x61CC;
            mapDataStd [rangeNo] [0xE97E - rangeMin] = 0x61BA;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xE9A1 - rangeMin] = 0x61BF;
            mapDataStd [rangeNo] [0xE9A2 - rangeMin] = 0x61B8;
            mapDataStd [rangeNo] [0xE9A3 - rangeMin] = 0x618C;
            mapDataStd [rangeNo] [0xE9A4 - rangeMin] = 0x64D7;
            mapDataStd [rangeNo] [0xE9A5 - rangeMin] = 0x64D6;
            mapDataStd [rangeNo] [0xE9A6 - rangeMin] = 0x64D0;
            mapDataStd [rangeNo] [0xE9A7 - rangeMin] = 0x64CF;
            mapDataStd [rangeNo] [0xE9A8 - rangeMin] = 0x64C9;
            mapDataStd [rangeNo] [0xE9A9 - rangeMin] = 0x64BD;
            mapDataStd [rangeNo] [0xE9AA - rangeMin] = 0x6489;
            mapDataStd [rangeNo] [0xE9AB - rangeMin] = 0x64C3;
            mapDataStd [rangeNo] [0xE9AC - rangeMin] = 0x64DB;
            mapDataStd [rangeNo] [0xE9AD - rangeMin] = 0x64F3;
            mapDataStd [rangeNo] [0xE9AE - rangeMin] = 0x64D9;
            mapDataStd [rangeNo] [0xE9AF - rangeMin] = 0x6533;

            mapDataStd [rangeNo] [0xE9B0 - rangeMin] = 0x657F;
            mapDataStd [rangeNo] [0xE9B1 - rangeMin] = 0x657C;
            mapDataStd [rangeNo] [0xE9B2 - rangeMin] = 0x65A2;
            mapDataStd [rangeNo] [0xE9B3 - rangeMin] = 0x66C8;
            mapDataStd [rangeNo] [0xE9B4 - rangeMin] = 0x66BE;
            mapDataStd [rangeNo] [0xE9B5 - rangeMin] = 0x66C0;
            mapDataStd [rangeNo] [0xE9B6 - rangeMin] = 0x66CA;
            mapDataStd [rangeNo] [0xE9B7 - rangeMin] = 0x66CB;
            mapDataStd [rangeNo] [0xE9B8 - rangeMin] = 0x66CF;
            mapDataStd [rangeNo] [0xE9B9 - rangeMin] = 0x66BD;
            mapDataStd [rangeNo] [0xE9BA - rangeMin] = 0x66BB;
            mapDataStd [rangeNo] [0xE9BB - rangeMin] = 0x66BA;
            mapDataStd [rangeNo] [0xE9BC - rangeMin] = 0x66CC;
            mapDataStd [rangeNo] [0xE9BD - rangeMin] = 0x6723;
            mapDataStd [rangeNo] [0xE9BE - rangeMin] = 0x6A34;
            mapDataStd [rangeNo] [0xE9BF - rangeMin] = 0x6A66;

            mapDataStd [rangeNo] [0xE9C0 - rangeMin] = 0x6A49;
            mapDataStd [rangeNo] [0xE9C1 - rangeMin] = 0x6A67;
            mapDataStd [rangeNo] [0xE9C2 - rangeMin] = 0x6A32;
            mapDataStd [rangeNo] [0xE9C3 - rangeMin] = 0x6A68;
            mapDataStd [rangeNo] [0xE9C4 - rangeMin] = 0x6A3E;
            mapDataStd [rangeNo] [0xE9C5 - rangeMin] = 0x6A5D;
            mapDataStd [rangeNo] [0xE9C6 - rangeMin] = 0x6A6D;
            mapDataStd [rangeNo] [0xE9C7 - rangeMin] = 0x6A76;
            mapDataStd [rangeNo] [0xE9C8 - rangeMin] = 0x6A5B;
            mapDataStd [rangeNo] [0xE9C9 - rangeMin] = 0x6A51;
            mapDataStd [rangeNo] [0xE9CA - rangeMin] = 0x6A28;
            mapDataStd [rangeNo] [0xE9CB - rangeMin] = 0x6A5A;
            mapDataStd [rangeNo] [0xE9CC - rangeMin] = 0x6A3B;
            mapDataStd [rangeNo] [0xE9CD - rangeMin] = 0x6A3F;
            mapDataStd [rangeNo] [0xE9CE - rangeMin] = 0x6A41;
            mapDataStd [rangeNo] [0xE9CF - rangeMin] = 0x6A6A;

            mapDataStd [rangeNo] [0xE9D0 - rangeMin] = 0x6A64;
            mapDataStd [rangeNo] [0xE9D1 - rangeMin] = 0x6A50;
            mapDataStd [rangeNo] [0xE9D2 - rangeMin] = 0x6A4F;
            mapDataStd [rangeNo] [0xE9D3 - rangeMin] = 0x6A54;
            mapDataStd [rangeNo] [0xE9D4 - rangeMin] = 0x6A6F;
            mapDataStd [rangeNo] [0xE9D5 - rangeMin] = 0x6A69;
            mapDataStd [rangeNo] [0xE9D6 - rangeMin] = 0x6A60;
            mapDataStd [rangeNo] [0xE9D7 - rangeMin] = 0x6A3C;
            mapDataStd [rangeNo] [0xE9D8 - rangeMin] = 0x6A5E;
            mapDataStd [rangeNo] [0xE9D9 - rangeMin] = 0x6A56;
            mapDataStd [rangeNo] [0xE9DA - rangeMin] = 0x6A55;
            mapDataStd [rangeNo] [0xE9DB - rangeMin] = 0x6A4D;
            mapDataStd [rangeNo] [0xE9DC - rangeMin] = 0x6A4E;
            mapDataStd [rangeNo] [0xE9DD - rangeMin] = 0x6A46;
            mapDataStd [rangeNo] [0xE9DE - rangeMin] = 0x6B55;
            mapDataStd [rangeNo] [0xE9DF - rangeMin] = 0x6B54;

            mapDataStd [rangeNo] [0xE9E0 - rangeMin] = 0x6B56;
            mapDataStd [rangeNo] [0xE9E1 - rangeMin] = 0x6BA7;
            mapDataStd [rangeNo] [0xE9E2 - rangeMin] = 0x6BAA;
            mapDataStd [rangeNo] [0xE9E3 - rangeMin] = 0x6BAB;
            mapDataStd [rangeNo] [0xE9E4 - rangeMin] = 0x6BC8;
            mapDataStd [rangeNo] [0xE9E5 - rangeMin] = 0x6BC7;
            mapDataStd [rangeNo] [0xE9E6 - rangeMin] = 0x6C04;
            mapDataStd [rangeNo] [0xE9E7 - rangeMin] = 0x6C03;
            mapDataStd [rangeNo] [0xE9E8 - rangeMin] = 0x6C06;
            mapDataStd [rangeNo] [0xE9E9 - rangeMin] = 0x6FAD;
            mapDataStd [rangeNo] [0xE9EA - rangeMin] = 0x6FCB;
            mapDataStd [rangeNo] [0xE9EB - rangeMin] = 0x6FA3;
            mapDataStd [rangeNo] [0xE9EC - rangeMin] = 0x6FC7;
            mapDataStd [rangeNo] [0xE9ED - rangeMin] = 0x6FBC;
            mapDataStd [rangeNo] [0xE9EE - rangeMin] = 0x6FCE;
            mapDataStd [rangeNo] [0xE9EF - rangeMin] = 0x6FC8;

            mapDataStd [rangeNo] [0xE9F0 - rangeMin] = 0x6F5E;
            mapDataStd [rangeNo] [0xE9F1 - rangeMin] = 0x6FC4;
            mapDataStd [rangeNo] [0xE9F2 - rangeMin] = 0x6FBD;
            mapDataStd [rangeNo] [0xE9F3 - rangeMin] = 0x6F9E;
            mapDataStd [rangeNo] [0xE9F4 - rangeMin] = 0x6FCA;
            mapDataStd [rangeNo] [0xE9F5 - rangeMin] = 0x6FA8;
            mapDataStd [rangeNo] [0xE9F6 - rangeMin] = 0x7004;
            mapDataStd [rangeNo] [0xE9F7 - rangeMin] = 0x6FA5;
            mapDataStd [rangeNo] [0xE9F8 - rangeMin] = 0x6FAE;
            mapDataStd [rangeNo] [0xE9F9 - rangeMin] = 0x6FBA;
            mapDataStd [rangeNo] [0xE9FA - rangeMin] = 0x6FAC;
            mapDataStd [rangeNo] [0xE9FB - rangeMin] = 0x6FAA;
            mapDataStd [rangeNo] [0xE9FC - rangeMin] = 0x6FCF;
            mapDataStd [rangeNo] [0xE9FD - rangeMin] = 0x6FBF;
            mapDataStd [rangeNo] [0xE9FE - rangeMin] = 0x6FB8;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xEA40 - rangeMin] = 0x6FA2;
            mapDataStd [rangeNo] [0xEA41 - rangeMin] = 0x6FC9;
            mapDataStd [rangeNo] [0xEA42 - rangeMin] = 0x6FAB;
            mapDataStd [rangeNo] [0xEA43 - rangeMin] = 0x6FCD;
            mapDataStd [rangeNo] [0xEA44 - rangeMin] = 0x6FAF;
            mapDataStd [rangeNo] [0xEA45 - rangeMin] = 0x6FB2;
            mapDataStd [rangeNo] [0xEA46 - rangeMin] = 0x6FB0;
            mapDataStd [rangeNo] [0xEA47 - rangeMin] = 0x71C5;
            mapDataStd [rangeNo] [0xEA48 - rangeMin] = 0x71C2;
            mapDataStd [rangeNo] [0xEA49 - rangeMin] = 0x71BF;
            mapDataStd [rangeNo] [0xEA4A - rangeMin] = 0x71B8;
            mapDataStd [rangeNo] [0xEA4B - rangeMin] = 0x71D6;
            mapDataStd [rangeNo] [0xEA4C - rangeMin] = 0x71C0;
            mapDataStd [rangeNo] [0xEA4D - rangeMin] = 0x71C1;
            mapDataStd [rangeNo] [0xEA4E - rangeMin] = 0x71CB;
            mapDataStd [rangeNo] [0xEA4F - rangeMin] = 0x71D4;

            mapDataStd [rangeNo] [0xEA50 - rangeMin] = 0x71CA;
            mapDataStd [rangeNo] [0xEA51 - rangeMin] = 0x71C7;
            mapDataStd [rangeNo] [0xEA52 - rangeMin] = 0x71CF;
            mapDataStd [rangeNo] [0xEA53 - rangeMin] = 0x71BD;
            mapDataStd [rangeNo] [0xEA54 - rangeMin] = 0x71D8;
            mapDataStd [rangeNo] [0xEA55 - rangeMin] = 0x71BC;
            mapDataStd [rangeNo] [0xEA56 - rangeMin] = 0x71C6;
            mapDataStd [rangeNo] [0xEA57 - rangeMin] = 0x71DA;
            mapDataStd [rangeNo] [0xEA58 - rangeMin] = 0x71DB;
            mapDataStd [rangeNo] [0xEA59 - rangeMin] = 0x729D;
            mapDataStd [rangeNo] [0xEA5A - rangeMin] = 0x729E;
            mapDataStd [rangeNo] [0xEA5B - rangeMin] = 0x7369;
            mapDataStd [rangeNo] [0xEA5C - rangeMin] = 0x7366;
            mapDataStd [rangeNo] [0xEA5D - rangeMin] = 0x7367;
            mapDataStd [rangeNo] [0xEA5E - rangeMin] = 0x736C;
            mapDataStd [rangeNo] [0xEA5F - rangeMin] = 0x7365;

            mapDataStd [rangeNo] [0xEA60 - rangeMin] = 0x736B;
            mapDataStd [rangeNo] [0xEA61 - rangeMin] = 0x736A;
            mapDataStd [rangeNo] [0xEA62 - rangeMin] = 0x747F;
            mapDataStd [rangeNo] [0xEA63 - rangeMin] = 0x749A;
            mapDataStd [rangeNo] [0xEA64 - rangeMin] = 0x74A0;
            mapDataStd [rangeNo] [0xEA65 - rangeMin] = 0x7494;
            mapDataStd [rangeNo] [0xEA66 - rangeMin] = 0x7492;
            mapDataStd [rangeNo] [0xEA67 - rangeMin] = 0x7495;
            mapDataStd [rangeNo] [0xEA68 - rangeMin] = 0x74A1;
            mapDataStd [rangeNo] [0xEA69 - rangeMin] = 0x750B;
            mapDataStd [rangeNo] [0xEA6A - rangeMin] = 0x7580;
            mapDataStd [rangeNo] [0xEA6B - rangeMin] = 0x762F;
            mapDataStd [rangeNo] [0xEA6C - rangeMin] = 0x762D;
            mapDataStd [rangeNo] [0xEA6D - rangeMin] = 0x7631;
            mapDataStd [rangeNo] [0xEA6E - rangeMin] = 0x763D;
            mapDataStd [rangeNo] [0xEA6F - rangeMin] = 0x7633;

            mapDataStd [rangeNo] [0xEA70 - rangeMin] = 0x763C;
            mapDataStd [rangeNo] [0xEA71 - rangeMin] = 0x7635;
            mapDataStd [rangeNo] [0xEA72 - rangeMin] = 0x7632;
            mapDataStd [rangeNo] [0xEA73 - rangeMin] = 0x7630;
            mapDataStd [rangeNo] [0xEA74 - rangeMin] = 0x76BB;
            mapDataStd [rangeNo] [0xEA75 - rangeMin] = 0x76E6;
            mapDataStd [rangeNo] [0xEA76 - rangeMin] = 0x779A;
            mapDataStd [rangeNo] [0xEA77 - rangeMin] = 0x779D;
            mapDataStd [rangeNo] [0xEA78 - rangeMin] = 0x77A1;
            mapDataStd [rangeNo] [0xEA79 - rangeMin] = 0x779C;
            mapDataStd [rangeNo] [0xEA7A - rangeMin] = 0x779B;
            mapDataStd [rangeNo] [0xEA7B - rangeMin] = 0x77A2;
            mapDataStd [rangeNo] [0xEA7C - rangeMin] = 0x77A3;
            mapDataStd [rangeNo] [0xEA7D - rangeMin] = 0x7795;
            mapDataStd [rangeNo] [0xEA7E - rangeMin] = 0x7799;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xEAA1 - rangeMin] = 0x7797;
            mapDataStd [rangeNo] [0xEAA2 - rangeMin] = 0x78DD;
            mapDataStd [rangeNo] [0xEAA3 - rangeMin] = 0x78E9;
            mapDataStd [rangeNo] [0xEAA4 - rangeMin] = 0x78E5;
            mapDataStd [rangeNo] [0xEAA5 - rangeMin] = 0x78EA;
            mapDataStd [rangeNo] [0xEAA6 - rangeMin] = 0x78DE;
            mapDataStd [rangeNo] [0xEAA7 - rangeMin] = 0x78E3;
            mapDataStd [rangeNo] [0xEAA8 - rangeMin] = 0x78DB;
            mapDataStd [rangeNo] [0xEAA9 - rangeMin] = 0x78E1;
            mapDataStd [rangeNo] [0xEAAA - rangeMin] = 0x78E2;
            mapDataStd [rangeNo] [0xEAAB - rangeMin] = 0x78ED;
            mapDataStd [rangeNo] [0xEAAC - rangeMin] = 0x78DF;
            mapDataStd [rangeNo] [0xEAAD - rangeMin] = 0x78E0;
            mapDataStd [rangeNo] [0xEAAE - rangeMin] = 0x79A4;
            mapDataStd [rangeNo] [0xEAAF - rangeMin] = 0x7A44;

            mapDataStd [rangeNo] [0xEAB0 - rangeMin] = 0x7A48;
            mapDataStd [rangeNo] [0xEAB1 - rangeMin] = 0x7A47;
            mapDataStd [rangeNo] [0xEAB2 - rangeMin] = 0x7AB6;
            mapDataStd [rangeNo] [0xEAB3 - rangeMin] = 0x7AB8;
            mapDataStd [rangeNo] [0xEAB4 - rangeMin] = 0x7AB5;
            mapDataStd [rangeNo] [0xEAB5 - rangeMin] = 0x7AB1;
            mapDataStd [rangeNo] [0xEAB6 - rangeMin] = 0x7AB7;
            mapDataStd [rangeNo] [0xEAB7 - rangeMin] = 0x7BDE;
            mapDataStd [rangeNo] [0xEAB8 - rangeMin] = 0x7BE3;
            mapDataStd [rangeNo] [0xEAB9 - rangeMin] = 0x7BE7;
            mapDataStd [rangeNo] [0xEABA - rangeMin] = 0x7BDD;
            mapDataStd [rangeNo] [0xEABB - rangeMin] = 0x7BD5;
            mapDataStd [rangeNo] [0xEABC - rangeMin] = 0x7BE5;
            mapDataStd [rangeNo] [0xEABD - rangeMin] = 0x7BDA;
            mapDataStd [rangeNo] [0xEABE - rangeMin] = 0x7BE8;
            mapDataStd [rangeNo] [0xEABF - rangeMin] = 0x7BF9;

            mapDataStd [rangeNo] [0xEAC0 - rangeMin] = 0x7BD4;
            mapDataStd [rangeNo] [0xEAC1 - rangeMin] = 0x7BEA;
            mapDataStd [rangeNo] [0xEAC2 - rangeMin] = 0x7BE2;
            mapDataStd [rangeNo] [0xEAC3 - rangeMin] = 0x7BDC;
            mapDataStd [rangeNo] [0xEAC4 - rangeMin] = 0x7BEB;
            mapDataStd [rangeNo] [0xEAC5 - rangeMin] = 0x7BD8;
            mapDataStd [rangeNo] [0xEAC6 - rangeMin] = 0x7BDF;
            mapDataStd [rangeNo] [0xEAC7 - rangeMin] = 0x7CD2;
            mapDataStd [rangeNo] [0xEAC8 - rangeMin] = 0x7CD4;
            mapDataStd [rangeNo] [0xEAC9 - rangeMin] = 0x7CD7;
            mapDataStd [rangeNo] [0xEACA - rangeMin] = 0x7CD0;
            mapDataStd [rangeNo] [0xEACB - rangeMin] = 0x7CD1;
            mapDataStd [rangeNo] [0xEACC - rangeMin] = 0x7E12;
            mapDataStd [rangeNo] [0xEACD - rangeMin] = 0x7E21;
            mapDataStd [rangeNo] [0xEACE - rangeMin] = 0x7E17;
            mapDataStd [rangeNo] [0xEACF - rangeMin] = 0x7E0C;

            mapDataStd [rangeNo] [0xEAD0 - rangeMin] = 0x7E1F;
            mapDataStd [rangeNo] [0xEAD1 - rangeMin] = 0x7E20;
            mapDataStd [rangeNo] [0xEAD2 - rangeMin] = 0x7E13;
            mapDataStd [rangeNo] [0xEAD3 - rangeMin] = 0x7E0E;
            mapDataStd [rangeNo] [0xEAD4 - rangeMin] = 0x7E1C;
            mapDataStd [rangeNo] [0xEAD5 - rangeMin] = 0x7E15;
            mapDataStd [rangeNo] [0xEAD6 - rangeMin] = 0x7E1A;
            mapDataStd [rangeNo] [0xEAD7 - rangeMin] = 0x7E22;
            mapDataStd [rangeNo] [0xEAD8 - rangeMin] = 0x7E0B;
            mapDataStd [rangeNo] [0xEAD9 - rangeMin] = 0x7E0F;
            mapDataStd [rangeNo] [0xEADA - rangeMin] = 0x7E16;
            mapDataStd [rangeNo] [0xEADB - rangeMin] = 0x7E0D;
            mapDataStd [rangeNo] [0xEADC - rangeMin] = 0x7E14;
            mapDataStd [rangeNo] [0xEADD - rangeMin] = 0x7E25;
            mapDataStd [rangeNo] [0xEADE - rangeMin] = 0x7E24;
            mapDataStd [rangeNo] [0xEADF - rangeMin] = 0x7F43;

            mapDataStd [rangeNo] [0xEAE0 - rangeMin] = 0x7F7B;
            mapDataStd [rangeNo] [0xEAE1 - rangeMin] = 0x7F7C;
            mapDataStd [rangeNo] [0xEAE2 - rangeMin] = 0x7F7A;
            mapDataStd [rangeNo] [0xEAE3 - rangeMin] = 0x7FB1;
            mapDataStd [rangeNo] [0xEAE4 - rangeMin] = 0x7FEF;
            mapDataStd [rangeNo] [0xEAE5 - rangeMin] = 0x802A;
            mapDataStd [rangeNo] [0xEAE6 - rangeMin] = 0x8029;
            mapDataStd [rangeNo] [0xEAE7 - rangeMin] = 0x806C;
            mapDataStd [rangeNo] [0xEAE8 - rangeMin] = 0x81B1;
            mapDataStd [rangeNo] [0xEAE9 - rangeMin] = 0x81A6;
            mapDataStd [rangeNo] [0xEAEA - rangeMin] = 0x81AE;
            mapDataStd [rangeNo] [0xEAEB - rangeMin] = 0x81B9;
            mapDataStd [rangeNo] [0xEAEC - rangeMin] = 0x81B5;
            mapDataStd [rangeNo] [0xEAED - rangeMin] = 0x81AB;
            mapDataStd [rangeNo] [0xEAEE - rangeMin] = 0x81B0;
            mapDataStd [rangeNo] [0xEAEF - rangeMin] = 0x81AC;

            mapDataStd [rangeNo] [0xEAF0 - rangeMin] = 0x81B4;
            mapDataStd [rangeNo] [0xEAF1 - rangeMin] = 0x81B2;
            mapDataStd [rangeNo] [0xEAF2 - rangeMin] = 0x81B7;
            mapDataStd [rangeNo] [0xEAF3 - rangeMin] = 0x81A7;
            mapDataStd [rangeNo] [0xEAF4 - rangeMin] = 0x81F2;
            mapDataStd [rangeNo] [0xEAF5 - rangeMin] = 0x8255;
            mapDataStd [rangeNo] [0xEAF6 - rangeMin] = 0x8256;
            mapDataStd [rangeNo] [0xEAF7 - rangeMin] = 0x8257;
            mapDataStd [rangeNo] [0xEAF8 - rangeMin] = 0x8556;
            mapDataStd [rangeNo] [0xEAF9 - rangeMin] = 0x8545;
            mapDataStd [rangeNo] [0xEAFA - rangeMin] = 0x856B;
            mapDataStd [rangeNo] [0xEAFB - rangeMin] = 0x854D;
            mapDataStd [rangeNo] [0xEAFC - rangeMin] = 0x8553;
            mapDataStd [rangeNo] [0xEAFD - rangeMin] = 0x8561;
            mapDataStd [rangeNo] [0xEAFE - rangeMin] = 0x8558;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xEB40 - rangeMin] = 0x8540;
            mapDataStd [rangeNo] [0xEB41 - rangeMin] = 0x8546;
            mapDataStd [rangeNo] [0xEB42 - rangeMin] = 0x8564;
            mapDataStd [rangeNo] [0xEB43 - rangeMin] = 0x8541;
            mapDataStd [rangeNo] [0xEB44 - rangeMin] = 0x8562;
            mapDataStd [rangeNo] [0xEB45 - rangeMin] = 0x8544;
            mapDataStd [rangeNo] [0xEB46 - rangeMin] = 0x8551;
            mapDataStd [rangeNo] [0xEB47 - rangeMin] = 0x8547;
            mapDataStd [rangeNo] [0xEB48 - rangeMin] = 0x8563;
            mapDataStd [rangeNo] [0xEB49 - rangeMin] = 0x853E;
            mapDataStd [rangeNo] [0xEB4A - rangeMin] = 0x855B;
            mapDataStd [rangeNo] [0xEB4B - rangeMin] = 0x8571;
            mapDataStd [rangeNo] [0xEB4C - rangeMin] = 0x854E;
            mapDataStd [rangeNo] [0xEB4D - rangeMin] = 0x856E;
            mapDataStd [rangeNo] [0xEB4E - rangeMin] = 0x8575;
            mapDataStd [rangeNo] [0xEB4F - rangeMin] = 0x8555;

            mapDataStd [rangeNo] [0xEB50 - rangeMin] = 0x8567;
            mapDataStd [rangeNo] [0xEB51 - rangeMin] = 0x8560;
            mapDataStd [rangeNo] [0xEB52 - rangeMin] = 0x858C;
            mapDataStd [rangeNo] [0xEB53 - rangeMin] = 0x8566;
            mapDataStd [rangeNo] [0xEB54 - rangeMin] = 0x855D;
            mapDataStd [rangeNo] [0xEB55 - rangeMin] = 0x8554;
            mapDataStd [rangeNo] [0xEB56 - rangeMin] = 0x8565;
            mapDataStd [rangeNo] [0xEB57 - rangeMin] = 0x856C;
            mapDataStd [rangeNo] [0xEB58 - rangeMin] = 0x8663;
            mapDataStd [rangeNo] [0xEB59 - rangeMin] = 0x8665;
            mapDataStd [rangeNo] [0xEB5A - rangeMin] = 0x8664;
            mapDataStd [rangeNo] [0xEB5B - rangeMin] = 0x879B;
            mapDataStd [rangeNo] [0xEB5C - rangeMin] = 0x878F;
            mapDataStd [rangeNo] [0xEB5D - rangeMin] = 0x8797;
            mapDataStd [rangeNo] [0xEB5E - rangeMin] = 0x8793;
            mapDataStd [rangeNo] [0xEB5F - rangeMin] = 0x8792;

            mapDataStd [rangeNo] [0xEB60 - rangeMin] = 0x8788;
            mapDataStd [rangeNo] [0xEB61 - rangeMin] = 0x8781;
            mapDataStd [rangeNo] [0xEB62 - rangeMin] = 0x8796;
            mapDataStd [rangeNo] [0xEB63 - rangeMin] = 0x8798;
            mapDataStd [rangeNo] [0xEB64 - rangeMin] = 0x8779;
            mapDataStd [rangeNo] [0xEB65 - rangeMin] = 0x8787;
            mapDataStd [rangeNo] [0xEB66 - rangeMin] = 0x87A3;
            mapDataStd [rangeNo] [0xEB67 - rangeMin] = 0x8785;
            mapDataStd [rangeNo] [0xEB68 - rangeMin] = 0x8790;
            mapDataStd [rangeNo] [0xEB69 - rangeMin] = 0x8791;
            mapDataStd [rangeNo] [0xEB6A - rangeMin] = 0x879D;
            mapDataStd [rangeNo] [0xEB6B - rangeMin] = 0x8784;
            mapDataStd [rangeNo] [0xEB6C - rangeMin] = 0x8794;
            mapDataStd [rangeNo] [0xEB6D - rangeMin] = 0x879C;
            mapDataStd [rangeNo] [0xEB6E - rangeMin] = 0x879A;
            mapDataStd [rangeNo] [0xEB6F - rangeMin] = 0x8789;

            mapDataStd [rangeNo] [0xEB70 - rangeMin] = 0x891E;
            mapDataStd [rangeNo] [0xEB71 - rangeMin] = 0x8926;
            mapDataStd [rangeNo] [0xEB72 - rangeMin] = 0x8930;
            mapDataStd [rangeNo] [0xEB73 - rangeMin] = 0x892D;
            mapDataStd [rangeNo] [0xEB74 - rangeMin] = 0x892E;
            mapDataStd [rangeNo] [0xEB75 - rangeMin] = 0x8927;
            mapDataStd [rangeNo] [0xEB76 - rangeMin] = 0x8931;
            mapDataStd [rangeNo] [0xEB77 - rangeMin] = 0x8922;
            mapDataStd [rangeNo] [0xEB78 - rangeMin] = 0x8929;
            mapDataStd [rangeNo] [0xEB79 - rangeMin] = 0x8923;
            mapDataStd [rangeNo] [0xEB7A - rangeMin] = 0x892F;
            mapDataStd [rangeNo] [0xEB7B - rangeMin] = 0x892C;
            mapDataStd [rangeNo] [0xEB7C - rangeMin] = 0x891F;
            mapDataStd [rangeNo] [0xEB7D - rangeMin] = 0x89F1;
            mapDataStd [rangeNo] [0xEB7E - rangeMin] = 0x8AE0;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xEBA1 - rangeMin] = 0x8AE2;
            mapDataStd [rangeNo] [0xEBA2 - rangeMin] = 0x8AF2;
            mapDataStd [rangeNo] [0xEBA3 - rangeMin] = 0x8AF4;
            mapDataStd [rangeNo] [0xEBA4 - rangeMin] = 0x8AF5;
            mapDataStd [rangeNo] [0xEBA5 - rangeMin] = 0x8ADD;
            mapDataStd [rangeNo] [0xEBA6 - rangeMin] = 0x8B14;
            mapDataStd [rangeNo] [0xEBA7 - rangeMin] = 0x8AE4;
            mapDataStd [rangeNo] [0xEBA8 - rangeMin] = 0x8ADF;
            mapDataStd [rangeNo] [0xEBA9 - rangeMin] = 0x8AF0;
            mapDataStd [rangeNo] [0xEBAA - rangeMin] = 0x8AC8;
            mapDataStd [rangeNo] [0xEBAB - rangeMin] = 0x8ADE;
            mapDataStd [rangeNo] [0xEBAC - rangeMin] = 0x8AE1;
            mapDataStd [rangeNo] [0xEBAD - rangeMin] = 0x8AE8;
            mapDataStd [rangeNo] [0xEBAE - rangeMin] = 0x8AFF;
            mapDataStd [rangeNo] [0xEBAF - rangeMin] = 0x8AEF;

            mapDataStd [rangeNo] [0xEBB0 - rangeMin] = 0x8AFB;
            mapDataStd [rangeNo] [0xEBB1 - rangeMin] = 0x8C91;
            mapDataStd [rangeNo] [0xEBB2 - rangeMin] = 0x8C92;
            mapDataStd [rangeNo] [0xEBB3 - rangeMin] = 0x8C90;
            mapDataStd [rangeNo] [0xEBB4 - rangeMin] = 0x8CF5;
            mapDataStd [rangeNo] [0xEBB5 - rangeMin] = 0x8CEE;
            mapDataStd [rangeNo] [0xEBB6 - rangeMin] = 0x8CF1;
            mapDataStd [rangeNo] [0xEBB7 - rangeMin] = 0x8CF0;
            mapDataStd [rangeNo] [0xEBB8 - rangeMin] = 0x8CF3;
            mapDataStd [rangeNo] [0xEBB9 - rangeMin] = 0x8D6C;
            mapDataStd [rangeNo] [0xEBBA - rangeMin] = 0x8D6E;
            mapDataStd [rangeNo] [0xEBBB - rangeMin] = 0x8DA5;
            mapDataStd [rangeNo] [0xEBBC - rangeMin] = 0x8DA7;
            mapDataStd [rangeNo] [0xEBBD - rangeMin] = 0x8E33;
            mapDataStd [rangeNo] [0xEBBE - rangeMin] = 0x8E3E;
            mapDataStd [rangeNo] [0xEBBF - rangeMin] = 0x8E38;

            mapDataStd [rangeNo] [0xEBC0 - rangeMin] = 0x8E40;
            mapDataStd [rangeNo] [0xEBC1 - rangeMin] = 0x8E45;
            mapDataStd [rangeNo] [0xEBC2 - rangeMin] = 0x8E36;
            mapDataStd [rangeNo] [0xEBC3 - rangeMin] = 0x8E3C;
            mapDataStd [rangeNo] [0xEBC4 - rangeMin] = 0x8E3D;
            mapDataStd [rangeNo] [0xEBC5 - rangeMin] = 0x8E41;
            mapDataStd [rangeNo] [0xEBC6 - rangeMin] = 0x8E30;
            mapDataStd [rangeNo] [0xEBC7 - rangeMin] = 0x8E3F;
            mapDataStd [rangeNo] [0xEBC8 - rangeMin] = 0x8EBD;
            mapDataStd [rangeNo] [0xEBC9 - rangeMin] = 0x8F36;
            mapDataStd [rangeNo] [0xEBCA - rangeMin] = 0x8F2E;
            mapDataStd [rangeNo] [0xEBCB - rangeMin] = 0x8F35;
            mapDataStd [rangeNo] [0xEBCC - rangeMin] = 0x8F32;
            mapDataStd [rangeNo] [0xEBCD - rangeMin] = 0x8F39;
            mapDataStd [rangeNo] [0xEBCE - rangeMin] = 0x8F37;
            mapDataStd [rangeNo] [0xEBCF - rangeMin] = 0x8F34;

            mapDataStd [rangeNo] [0xEBD0 - rangeMin] = 0x9076;
            mapDataStd [rangeNo] [0xEBD1 - rangeMin] = 0x9079;
            mapDataStd [rangeNo] [0xEBD2 - rangeMin] = 0x907B;
            mapDataStd [rangeNo] [0xEBD3 - rangeMin] = 0x9086;
            mapDataStd [rangeNo] [0xEBD4 - rangeMin] = 0x90FA;
            mapDataStd [rangeNo] [0xEBD5 - rangeMin] = 0x9133;
            mapDataStd [rangeNo] [0xEBD6 - rangeMin] = 0x9135;
            mapDataStd [rangeNo] [0xEBD7 - rangeMin] = 0x9136;
            mapDataStd [rangeNo] [0xEBD8 - rangeMin] = 0x9193;
            mapDataStd [rangeNo] [0xEBD9 - rangeMin] = 0x9190;
            mapDataStd [rangeNo] [0xEBDA - rangeMin] = 0x9191;
            mapDataStd [rangeNo] [0xEBDB - rangeMin] = 0x918D;
            mapDataStd [rangeNo] [0xEBDC - rangeMin] = 0x918F;
            mapDataStd [rangeNo] [0xEBDD - rangeMin] = 0x9327;
            mapDataStd [rangeNo] [0xEBDE - rangeMin] = 0x931E;
            mapDataStd [rangeNo] [0xEBDF - rangeMin] = 0x9308;

            mapDataStd [rangeNo] [0xEBE0 - rangeMin] = 0x931F;
            mapDataStd [rangeNo] [0xEBE1 - rangeMin] = 0x9306;
            mapDataStd [rangeNo] [0xEBE2 - rangeMin] = 0x930F;
            mapDataStd [rangeNo] [0xEBE3 - rangeMin] = 0x937A;
            mapDataStd [rangeNo] [0xEBE4 - rangeMin] = 0x9338;
            mapDataStd [rangeNo] [0xEBE5 - rangeMin] = 0x933C;
            mapDataStd [rangeNo] [0xEBE6 - rangeMin] = 0x931B;
            mapDataStd [rangeNo] [0xEBE7 - rangeMin] = 0x9323;
            mapDataStd [rangeNo] [0xEBE8 - rangeMin] = 0x9312;
            mapDataStd [rangeNo] [0xEBE9 - rangeMin] = 0x9301;
            mapDataStd [rangeNo] [0xEBEA - rangeMin] = 0x9346;
            mapDataStd [rangeNo] [0xEBEB - rangeMin] = 0x932D;
            mapDataStd [rangeNo] [0xEBEC - rangeMin] = 0x930E;
            mapDataStd [rangeNo] [0xEBED - rangeMin] = 0x930D;
            mapDataStd [rangeNo] [0xEBEE - rangeMin] = 0x92CB;
            mapDataStd [rangeNo] [0xEBEF - rangeMin] = 0x931D;

            mapDataStd [rangeNo] [0xEBF0 - rangeMin] = 0x92FA;
            mapDataStd [rangeNo] [0xEBF1 - rangeMin] = 0x9325;
            mapDataStd [rangeNo] [0xEBF2 - rangeMin] = 0x9313;
            mapDataStd [rangeNo] [0xEBF3 - rangeMin] = 0x92F9;
            mapDataStd [rangeNo] [0xEBF4 - rangeMin] = 0x92F7;
            mapDataStd [rangeNo] [0xEBF5 - rangeMin] = 0x9334;
            mapDataStd [rangeNo] [0xEBF6 - rangeMin] = 0x9302;
            mapDataStd [rangeNo] [0xEBF7 - rangeMin] = 0x9324;
            mapDataStd [rangeNo] [0xEBF8 - rangeMin] = 0x92FF;
            mapDataStd [rangeNo] [0xEBF9 - rangeMin] = 0x9329;
            mapDataStd [rangeNo] [0xEBFA - rangeMin] = 0x9339;
            mapDataStd [rangeNo] [0xEBFB - rangeMin] = 0x9335;
            mapDataStd [rangeNo] [0xEBFC - rangeMin] = 0x932A;
            mapDataStd [rangeNo] [0xEBFD - rangeMin] = 0x9314;
            mapDataStd [rangeNo] [0xEBFE - rangeMin] = 0x930C;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xEC40 - rangeMin] = 0x930B;
            mapDataStd [rangeNo] [0xEC41 - rangeMin] = 0x92FE;
            mapDataStd [rangeNo] [0xEC42 - rangeMin] = 0x9309;
            mapDataStd [rangeNo] [0xEC43 - rangeMin] = 0x9300;
            mapDataStd [rangeNo] [0xEC44 - rangeMin] = 0x92FB;
            mapDataStd [rangeNo] [0xEC45 - rangeMin] = 0x9316;
            mapDataStd [rangeNo] [0xEC46 - rangeMin] = 0x95BC;
            mapDataStd [rangeNo] [0xEC47 - rangeMin] = 0x95CD;
            mapDataStd [rangeNo] [0xEC48 - rangeMin] = 0x95BE;
            mapDataStd [rangeNo] [0xEC49 - rangeMin] = 0x95B9;
            mapDataStd [rangeNo] [0xEC4A - rangeMin] = 0x95BA;
            mapDataStd [rangeNo] [0xEC4B - rangeMin] = 0x95B6;
            mapDataStd [rangeNo] [0xEC4C - rangeMin] = 0x95BF;
            mapDataStd [rangeNo] [0xEC4D - rangeMin] = 0x95B5;
            mapDataStd [rangeNo] [0xEC4E - rangeMin] = 0x95BD;
            mapDataStd [rangeNo] [0xEC4F - rangeMin] = 0x96A9;

            mapDataStd [rangeNo] [0xEC50 - rangeMin] = 0x96D4;
            mapDataStd [rangeNo] [0xEC51 - rangeMin] = 0x970B;
            mapDataStd [rangeNo] [0xEC52 - rangeMin] = 0x9712;
            mapDataStd [rangeNo] [0xEC53 - rangeMin] = 0x9710;
            mapDataStd [rangeNo] [0xEC54 - rangeMin] = 0x9799;
            mapDataStd [rangeNo] [0xEC55 - rangeMin] = 0x9797;
            mapDataStd [rangeNo] [0xEC56 - rangeMin] = 0x9794;
            mapDataStd [rangeNo] [0xEC57 - rangeMin] = 0x97F0;
            mapDataStd [rangeNo] [0xEC58 - rangeMin] = 0x97F8;
            mapDataStd [rangeNo] [0xEC59 - rangeMin] = 0x9835;
            mapDataStd [rangeNo] [0xEC5A - rangeMin] = 0x982F;
            mapDataStd [rangeNo] [0xEC5B - rangeMin] = 0x9832;
            mapDataStd [rangeNo] [0xEC5C - rangeMin] = 0x9924;
            mapDataStd [rangeNo] [0xEC5D - rangeMin] = 0x991F;
            mapDataStd [rangeNo] [0xEC5E - rangeMin] = 0x9927;
            mapDataStd [rangeNo] [0xEC5F - rangeMin] = 0x9929;

            mapDataStd [rangeNo] [0xEC60 - rangeMin] = 0x999E;
            mapDataStd [rangeNo] [0xEC61 - rangeMin] = 0x99EE;
            mapDataStd [rangeNo] [0xEC62 - rangeMin] = 0x99EC;
            mapDataStd [rangeNo] [0xEC63 - rangeMin] = 0x99E5;
            mapDataStd [rangeNo] [0xEC64 - rangeMin] = 0x99E4;
            mapDataStd [rangeNo] [0xEC65 - rangeMin] = 0x99F0;
            mapDataStd [rangeNo] [0xEC66 - rangeMin] = 0x99E3;
            mapDataStd [rangeNo] [0xEC67 - rangeMin] = 0x99EA;
            mapDataStd [rangeNo] [0xEC68 - rangeMin] = 0x99E9;
            mapDataStd [rangeNo] [0xEC69 - rangeMin] = 0x99E7;
            mapDataStd [rangeNo] [0xEC6A - rangeMin] = 0x9AB9;
            mapDataStd [rangeNo] [0xEC6B - rangeMin] = 0x9ABF;
            mapDataStd [rangeNo] [0xEC6C - rangeMin] = 0x9AB4;
            mapDataStd [rangeNo] [0xEC6D - rangeMin] = 0x9ABB;
            mapDataStd [rangeNo] [0xEC6E - rangeMin] = 0x9AF6;
            mapDataStd [rangeNo] [0xEC6F - rangeMin] = 0x9AFA;

            mapDataStd [rangeNo] [0xEC70 - rangeMin] = 0x9AF9;
            mapDataStd [rangeNo] [0xEC71 - rangeMin] = 0x9AF7;
            mapDataStd [rangeNo] [0xEC72 - rangeMin] = 0x9B33;
            mapDataStd [rangeNo] [0xEC73 - rangeMin] = 0x9B80;
            mapDataStd [rangeNo] [0xEC74 - rangeMin] = 0x9B85;
            mapDataStd [rangeNo] [0xEC75 - rangeMin] = 0x9B87;
            mapDataStd [rangeNo] [0xEC76 - rangeMin] = 0x9B7C;
            mapDataStd [rangeNo] [0xEC77 - rangeMin] = 0x9B7E;
            mapDataStd [rangeNo] [0xEC78 - rangeMin] = 0x9B7B;
            mapDataStd [rangeNo] [0xEC79 - rangeMin] = 0x9B82;
            mapDataStd [rangeNo] [0xEC7A - rangeMin] = 0x9B93;
            mapDataStd [rangeNo] [0xEC7B - rangeMin] = 0x9B92;
            mapDataStd [rangeNo] [0xEC7C - rangeMin] = 0x9B90;
            mapDataStd [rangeNo] [0xEC7D - rangeMin] = 0x9B7A;
            mapDataStd [rangeNo] [0xEC7E - rangeMin] = 0x9B95;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xECA1 - rangeMin] = 0x9B7D;
            mapDataStd [rangeNo] [0xECA2 - rangeMin] = 0x9B88;
            mapDataStd [rangeNo] [0xECA3 - rangeMin] = 0x9D25;
            mapDataStd [rangeNo] [0xECA4 - rangeMin] = 0x9D17;
            mapDataStd [rangeNo] [0xECA5 - rangeMin] = 0x9D20;
            mapDataStd [rangeNo] [0xECA6 - rangeMin] = 0x9D1E;
            mapDataStd [rangeNo] [0xECA7 - rangeMin] = 0x9D14;
            mapDataStd [rangeNo] [0xECA8 - rangeMin] = 0x9D29;
            mapDataStd [rangeNo] [0xECA9 - rangeMin] = 0x9D1D;
            mapDataStd [rangeNo] [0xECAA - rangeMin] = 0x9D18;
            mapDataStd [rangeNo] [0xECAB - rangeMin] = 0x9D22;
            mapDataStd [rangeNo] [0xECAC - rangeMin] = 0x9D10;
            mapDataStd [rangeNo] [0xECAD - rangeMin] = 0x9D19;
            mapDataStd [rangeNo] [0xECAE - rangeMin] = 0x9D1F;
            mapDataStd [rangeNo] [0xECAF - rangeMin] = 0x9E88;

            mapDataStd [rangeNo] [0xECB0 - rangeMin] = 0x9E86;
            mapDataStd [rangeNo] [0xECB1 - rangeMin] = 0x9E87;
            mapDataStd [rangeNo] [0xECB2 - rangeMin] = 0x9EAE;
            mapDataStd [rangeNo] [0xECB3 - rangeMin] = 0x9EAD;
            mapDataStd [rangeNo] [0xECB4 - rangeMin] = 0x9ED5;
            mapDataStd [rangeNo] [0xECB5 - rangeMin] = 0x9ED6;
            mapDataStd [rangeNo] [0xECB6 - rangeMin] = 0x9EFA;
            mapDataStd [rangeNo] [0xECB7 - rangeMin] = 0x9F12;
            mapDataStd [rangeNo] [0xECB8 - rangeMin] = 0x9F3D;
            mapDataStd [rangeNo] [0xECB9 - rangeMin] = 0x5126;
            mapDataStd [rangeNo] [0xECBA - rangeMin] = 0x5125;
            mapDataStd [rangeNo] [0xECBB - rangeMin] = 0x5122;
            mapDataStd [rangeNo] [0xECBC - rangeMin] = 0x5124;
            mapDataStd [rangeNo] [0xECBD - rangeMin] = 0x5120;
            mapDataStd [rangeNo] [0xECBE - rangeMin] = 0x5129;
            mapDataStd [rangeNo] [0xECBF - rangeMin] = 0x52F4;

            mapDataStd [rangeNo] [0xECC0 - rangeMin] = 0x5693;
            mapDataStd [rangeNo] [0xECC1 - rangeMin] = 0x568C;
            mapDataStd [rangeNo] [0xECC2 - rangeMin] = 0x568D;
            mapDataStd [rangeNo] [0xECC3 - rangeMin] = 0x5686;
            mapDataStd [rangeNo] [0xECC4 - rangeMin] = 0x5684;
            mapDataStd [rangeNo] [0xECC5 - rangeMin] = 0x5683;
            mapDataStd [rangeNo] [0xECC6 - rangeMin] = 0x567E;
            mapDataStd [rangeNo] [0xECC7 - rangeMin] = 0x5682;
            mapDataStd [rangeNo] [0xECC8 - rangeMin] = 0x567F;
            mapDataStd [rangeNo] [0xECC9 - rangeMin] = 0x5681;
            mapDataStd [rangeNo] [0xECCA - rangeMin] = 0x58D6;
            mapDataStd [rangeNo] [0xECCB - rangeMin] = 0x58D4;
            mapDataStd [rangeNo] [0xECCC - rangeMin] = 0x58CF;
            mapDataStd [rangeNo] [0xECCD - rangeMin] = 0x58D2;
            mapDataStd [rangeNo] [0xECCE - rangeMin] = 0x5B2D;
            mapDataStd [rangeNo] [0xECCF - rangeMin] = 0x5B25;

            mapDataStd [rangeNo] [0xECD0 - rangeMin] = 0x5B32;
            mapDataStd [rangeNo] [0xECD1 - rangeMin] = 0x5B23;
            mapDataStd [rangeNo] [0xECD2 - rangeMin] = 0x5B2C;
            mapDataStd [rangeNo] [0xECD3 - rangeMin] = 0x5B27;
            mapDataStd [rangeNo] [0xECD4 - rangeMin] = 0x5B26;
            mapDataStd [rangeNo] [0xECD5 - rangeMin] = 0x5B2F;
            mapDataStd [rangeNo] [0xECD6 - rangeMin] = 0x5B2E;
            mapDataStd [rangeNo] [0xECD7 - rangeMin] = 0x5B7B;
            mapDataStd [rangeNo] [0xECD8 - rangeMin] = 0x5BF1;
            mapDataStd [rangeNo] [0xECD9 - rangeMin] = 0x5BF2;
            mapDataStd [rangeNo] [0xECDA - rangeMin] = 0x5DB7;
            mapDataStd [rangeNo] [0xECDB - rangeMin] = 0x5E6C;
            mapDataStd [rangeNo] [0xECDC - rangeMin] = 0x5E6A;
            mapDataStd [rangeNo] [0xECDD - rangeMin] = 0x5FBE;
            mapDataStd [rangeNo] [0xECDE - rangeMin] = 0x5FBB;
            mapDataStd [rangeNo] [0xECDF - rangeMin] = 0x61C3;

            mapDataStd [rangeNo] [0xECE0 - rangeMin] = 0x61B5;
            mapDataStd [rangeNo] [0xECE1 - rangeMin] = 0x61BC;
            mapDataStd [rangeNo] [0xECE2 - rangeMin] = 0x61E7;
            mapDataStd [rangeNo] [0xECE3 - rangeMin] = 0x61E0;
            mapDataStd [rangeNo] [0xECE4 - rangeMin] = 0x61E5;
            mapDataStd [rangeNo] [0xECE5 - rangeMin] = 0x61E4;
            mapDataStd [rangeNo] [0xECE6 - rangeMin] = 0x61E8;
            mapDataStd [rangeNo] [0xECE7 - rangeMin] = 0x61DE;
            mapDataStd [rangeNo] [0xECE8 - rangeMin] = 0x64EF;
            mapDataStd [rangeNo] [0xECE9 - rangeMin] = 0x64E9;
            mapDataStd [rangeNo] [0xECEA - rangeMin] = 0x64E3;
            mapDataStd [rangeNo] [0xECEB - rangeMin] = 0x64EB;
            mapDataStd [rangeNo] [0xECEC - rangeMin] = 0x64E4;
            mapDataStd [rangeNo] [0xECED - rangeMin] = 0x64E8;
            mapDataStd [rangeNo] [0xECEE - rangeMin] = 0x6581;
            mapDataStd [rangeNo] [0xECEF - rangeMin] = 0x6580;

            mapDataStd [rangeNo] [0xECF0 - rangeMin] = 0x65B6;
            mapDataStd [rangeNo] [0xECF1 - rangeMin] = 0x65DA;
            mapDataStd [rangeNo] [0xECF2 - rangeMin] = 0x66D2;
            mapDataStd [rangeNo] [0xECF3 - rangeMin] = 0x6A8D;
            mapDataStd [rangeNo] [0xECF4 - rangeMin] = 0x6A96;
            mapDataStd [rangeNo] [0xECF5 - rangeMin] = 0x6A81;
            mapDataStd [rangeNo] [0xECF6 - rangeMin] = 0x6AA5;
            mapDataStd [rangeNo] [0xECF7 - rangeMin] = 0x6A89;
            mapDataStd [rangeNo] [0xECF8 - rangeMin] = 0x6A9F;
            mapDataStd [rangeNo] [0xECF9 - rangeMin] = 0x6A9B;
            mapDataStd [rangeNo] [0xECFA - rangeMin] = 0x6AA1;
            mapDataStd [rangeNo] [0xECFB - rangeMin] = 0x6A9E;
            mapDataStd [rangeNo] [0xECFC - rangeMin] = 0x6A87;
            mapDataStd [rangeNo] [0xECFD - rangeMin] = 0x6A93;
            mapDataStd [rangeNo] [0xECFE - rangeMin] = 0x6A8E;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xED40 - rangeMin] = 0x6A95;
            mapDataStd [rangeNo] [0xED41 - rangeMin] = 0x6A83;
            mapDataStd [rangeNo] [0xED42 - rangeMin] = 0x6AA8;
            mapDataStd [rangeNo] [0xED43 - rangeMin] = 0x6AA4;
            mapDataStd [rangeNo] [0xED44 - rangeMin] = 0x6A91;
            mapDataStd [rangeNo] [0xED45 - rangeMin] = 0x6A7F;
            mapDataStd [rangeNo] [0xED46 - rangeMin] = 0x6AA6;
            mapDataStd [rangeNo] [0xED47 - rangeMin] = 0x6A9A;
            mapDataStd [rangeNo] [0xED48 - rangeMin] = 0x6A85;
            mapDataStd [rangeNo] [0xED49 - rangeMin] = 0x6A8C;
            mapDataStd [rangeNo] [0xED4A - rangeMin] = 0x6A92;
            mapDataStd [rangeNo] [0xED4B - rangeMin] = 0x6B5B;
            mapDataStd [rangeNo] [0xED4C - rangeMin] = 0x6BAD;
            mapDataStd [rangeNo] [0xED4D - rangeMin] = 0x6C09;
            mapDataStd [rangeNo] [0xED4E - rangeMin] = 0x6FCC;
            mapDataStd [rangeNo] [0xED4F - rangeMin] = 0x6FA9;

            mapDataStd [rangeNo] [0xED50 - rangeMin] = 0x6FF4;
            mapDataStd [rangeNo] [0xED51 - rangeMin] = 0x6FD4;
            mapDataStd [rangeNo] [0xED52 - rangeMin] = 0x6FE3;
            mapDataStd [rangeNo] [0xED53 - rangeMin] = 0x6FDC;
            mapDataStd [rangeNo] [0xED54 - rangeMin] = 0x6FED;
            mapDataStd [rangeNo] [0xED55 - rangeMin] = 0x6FE7;
            mapDataStd [rangeNo] [0xED56 - rangeMin] = 0x6FE6;
            mapDataStd [rangeNo] [0xED57 - rangeMin] = 0x6FDE;
            mapDataStd [rangeNo] [0xED58 - rangeMin] = 0x6FF2;
            mapDataStd [rangeNo] [0xED59 - rangeMin] = 0x6FDD;
            mapDataStd [rangeNo] [0xED5A - rangeMin] = 0x6FE2;
            mapDataStd [rangeNo] [0xED5B - rangeMin] = 0x6FE8;
            mapDataStd [rangeNo] [0xED5C - rangeMin] = 0x71E1;
            mapDataStd [rangeNo] [0xED5D - rangeMin] = 0x71F1;
            mapDataStd [rangeNo] [0xED5E - rangeMin] = 0x71E8;
            mapDataStd [rangeNo] [0xED5F - rangeMin] = 0x71F2;

            mapDataStd [rangeNo] [0xED60 - rangeMin] = 0x71E4;
            mapDataStd [rangeNo] [0xED61 - rangeMin] = 0x71F0;
            mapDataStd [rangeNo] [0xED62 - rangeMin] = 0x71E2;
            mapDataStd [rangeNo] [0xED63 - rangeMin] = 0x7373;
            mapDataStd [rangeNo] [0xED64 - rangeMin] = 0x736E;
            mapDataStd [rangeNo] [0xED65 - rangeMin] = 0x736F;
            mapDataStd [rangeNo] [0xED66 - rangeMin] = 0x7497;
            mapDataStd [rangeNo] [0xED67 - rangeMin] = 0x74B2;
            mapDataStd [rangeNo] [0xED68 - rangeMin] = 0x74AB;
            mapDataStd [rangeNo] [0xED69 - rangeMin] = 0x7490;
            mapDataStd [rangeNo] [0xED6A - rangeMin] = 0x74AA;
            mapDataStd [rangeNo] [0xED6B - rangeMin] = 0x74AD;
            mapDataStd [rangeNo] [0xED6C - rangeMin] = 0x74B1;
            mapDataStd [rangeNo] [0xED6D - rangeMin] = 0x74A5;
            mapDataStd [rangeNo] [0xED6E - rangeMin] = 0x74AF;
            mapDataStd [rangeNo] [0xED6F - rangeMin] = 0x7510;

            mapDataStd [rangeNo] [0xED70 - rangeMin] = 0x7511;
            mapDataStd [rangeNo] [0xED71 - rangeMin] = 0x7512;
            mapDataStd [rangeNo] [0xED72 - rangeMin] = 0x750F;
            mapDataStd [rangeNo] [0xED73 - rangeMin] = 0x7584;
            mapDataStd [rangeNo] [0xED74 - rangeMin] = 0x7643;
            mapDataStd [rangeNo] [0xED75 - rangeMin] = 0x7648;
            mapDataStd [rangeNo] [0xED76 - rangeMin] = 0x7649;
            mapDataStd [rangeNo] [0xED77 - rangeMin] = 0x7647;
            mapDataStd [rangeNo] [0xED78 - rangeMin] = 0x76A4;
            mapDataStd [rangeNo] [0xED79 - rangeMin] = 0x76E9;
            mapDataStd [rangeNo] [0xED7A - rangeMin] = 0x77B5;
            mapDataStd [rangeNo] [0xED7B - rangeMin] = 0x77AB;
            mapDataStd [rangeNo] [0xED7C - rangeMin] = 0x77B2;
            mapDataStd [rangeNo] [0xED7D - rangeMin] = 0x77B7;
            mapDataStd [rangeNo] [0xED7E - rangeMin] = 0x77B6;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xEDA1 - rangeMin] = 0x77B4;
            mapDataStd [rangeNo] [0xEDA2 - rangeMin] = 0x77B1;
            mapDataStd [rangeNo] [0xEDA3 - rangeMin] = 0x77A8;
            mapDataStd [rangeNo] [0xEDA4 - rangeMin] = 0x77F0;
            mapDataStd [rangeNo] [0xEDA5 - rangeMin] = 0x78F3;
            mapDataStd [rangeNo] [0xEDA6 - rangeMin] = 0x78FD;
            mapDataStd [rangeNo] [0xEDA7 - rangeMin] = 0x7902;
            mapDataStd [rangeNo] [0xEDA8 - rangeMin] = 0x78FB;
            mapDataStd [rangeNo] [0xEDA9 - rangeMin] = 0x78FC;
            mapDataStd [rangeNo] [0xEDAA - rangeMin] = 0x78F2;
            mapDataStd [rangeNo] [0xEDAB - rangeMin] = 0x7905;
            mapDataStd [rangeNo] [0xEDAC - rangeMin] = 0x78F9;
            mapDataStd [rangeNo] [0xEDAD - rangeMin] = 0x78FE;
            mapDataStd [rangeNo] [0xEDAE - rangeMin] = 0x7904;
            mapDataStd [rangeNo] [0xEDAF - rangeMin] = 0x79AB;

            mapDataStd [rangeNo] [0xEDB0 - rangeMin] = 0x79A8;
            mapDataStd [rangeNo] [0xEDB1 - rangeMin] = 0x7A5C;
            mapDataStd [rangeNo] [0xEDB2 - rangeMin] = 0x7A5B;
            mapDataStd [rangeNo] [0xEDB3 - rangeMin] = 0x7A56;
            mapDataStd [rangeNo] [0xEDB4 - rangeMin] = 0x7A58;
            mapDataStd [rangeNo] [0xEDB5 - rangeMin] = 0x7A54;
            mapDataStd [rangeNo] [0xEDB6 - rangeMin] = 0x7A5A;
            mapDataStd [rangeNo] [0xEDB7 - rangeMin] = 0x7ABE;
            mapDataStd [rangeNo] [0xEDB8 - rangeMin] = 0x7AC0;
            mapDataStd [rangeNo] [0xEDB9 - rangeMin] = 0x7AC1;
            mapDataStd [rangeNo] [0xEDBA - rangeMin] = 0x7C05;
            mapDataStd [rangeNo] [0xEDBB - rangeMin] = 0x7C0F;
            mapDataStd [rangeNo] [0xEDBC - rangeMin] = 0x7BF2;
            mapDataStd [rangeNo] [0xEDBD - rangeMin] = 0x7C00;
            mapDataStd [rangeNo] [0xEDBE - rangeMin] = 0x7BFF;
            mapDataStd [rangeNo] [0xEDBF - rangeMin] = 0x7BFB;

            mapDataStd [rangeNo] [0xEDC0 - rangeMin] = 0x7C0E;
            mapDataStd [rangeNo] [0xEDC1 - rangeMin] = 0x7BF4;
            mapDataStd [rangeNo] [0xEDC2 - rangeMin] = 0x7C0B;
            mapDataStd [rangeNo] [0xEDC3 - rangeMin] = 0x7BF3;
            mapDataStd [rangeNo] [0xEDC4 - rangeMin] = 0x7C02;
            mapDataStd [rangeNo] [0xEDC5 - rangeMin] = 0x7C09;
            mapDataStd [rangeNo] [0xEDC6 - rangeMin] = 0x7C03;
            mapDataStd [rangeNo] [0xEDC7 - rangeMin] = 0x7C01;
            mapDataStd [rangeNo] [0xEDC8 - rangeMin] = 0x7BF8;
            mapDataStd [rangeNo] [0xEDC9 - rangeMin] = 0x7BFD;
            mapDataStd [rangeNo] [0xEDCA - rangeMin] = 0x7C06;
            mapDataStd [rangeNo] [0xEDCB - rangeMin] = 0x7BF0;
            mapDataStd [rangeNo] [0xEDCC - rangeMin] = 0x7BF1;
            mapDataStd [rangeNo] [0xEDCD - rangeMin] = 0x7C10;
            mapDataStd [rangeNo] [0xEDCE - rangeMin] = 0x7C0A;
            mapDataStd [rangeNo] [0xEDCF - rangeMin] = 0x7CE8;

            mapDataStd [rangeNo] [0xEDD0 - rangeMin] = 0x7E2D;
            mapDataStd [rangeNo] [0xEDD1 - rangeMin] = 0x7E3C;
            mapDataStd [rangeNo] [0xEDD2 - rangeMin] = 0x7E42;
            mapDataStd [rangeNo] [0xEDD3 - rangeMin] = 0x7E33;
            mapDataStd [rangeNo] [0xEDD4 - rangeMin] = 0x9848;
            mapDataStd [rangeNo] [0xEDD5 - rangeMin] = 0x7E38;
            mapDataStd [rangeNo] [0xEDD6 - rangeMin] = 0x7E2A;
            mapDataStd [rangeNo] [0xEDD7 - rangeMin] = 0x7E49;
            mapDataStd [rangeNo] [0xEDD8 - rangeMin] = 0x7E40;
            mapDataStd [rangeNo] [0xEDD9 - rangeMin] = 0x7E47;
            mapDataStd [rangeNo] [0xEDDA - rangeMin] = 0x7E29;
            mapDataStd [rangeNo] [0xEDDB - rangeMin] = 0x7E4C;
            mapDataStd [rangeNo] [0xEDDC - rangeMin] = 0x7E30;
            mapDataStd [rangeNo] [0xEDDD - rangeMin] = 0x7E3B;
            mapDataStd [rangeNo] [0xEDDE - rangeMin] = 0x7E36;
            mapDataStd [rangeNo] [0xEDDF - rangeMin] = 0x7E44;

            mapDataStd [rangeNo] [0xEDE0 - rangeMin] = 0x7E3A;
            mapDataStd [rangeNo] [0xEDE1 - rangeMin] = 0x7F45;
            mapDataStd [rangeNo] [0xEDE2 - rangeMin] = 0x7F7F;
            mapDataStd [rangeNo] [0xEDE3 - rangeMin] = 0x7F7E;
            mapDataStd [rangeNo] [0xEDE4 - rangeMin] = 0x7F7D;
            mapDataStd [rangeNo] [0xEDE5 - rangeMin] = 0x7FF4;
            mapDataStd [rangeNo] [0xEDE6 - rangeMin] = 0x7FF2;
            mapDataStd [rangeNo] [0xEDE7 - rangeMin] = 0x802C;
            mapDataStd [rangeNo] [0xEDE8 - rangeMin] = 0x81BB;
            mapDataStd [rangeNo] [0xEDE9 - rangeMin] = 0x81C4;
            mapDataStd [rangeNo] [0xEDEA - rangeMin] = 0x81CC;
            mapDataStd [rangeNo] [0xEDEB - rangeMin] = 0x81CA;
            mapDataStd [rangeNo] [0xEDEC - rangeMin] = 0x81C5;
            mapDataStd [rangeNo] [0xEDED - rangeMin] = 0x81C7;
            mapDataStd [rangeNo] [0xEDEE - rangeMin] = 0x81BC;
            mapDataStd [rangeNo] [0xEDEF - rangeMin] = 0x81E9;

            mapDataStd [rangeNo] [0xEDF0 - rangeMin] = 0x825B;
            mapDataStd [rangeNo] [0xEDF1 - rangeMin] = 0x825A;
            mapDataStd [rangeNo] [0xEDF2 - rangeMin] = 0x825C;
            mapDataStd [rangeNo] [0xEDF3 - rangeMin] = 0x8583;
            mapDataStd [rangeNo] [0xEDF4 - rangeMin] = 0x8580;
            mapDataStd [rangeNo] [0xEDF5 - rangeMin] = 0x858F;
            mapDataStd [rangeNo] [0xEDF6 - rangeMin] = 0x85A7;
            mapDataStd [rangeNo] [0xEDF7 - rangeMin] = 0x8595;
            mapDataStd [rangeNo] [0xEDF8 - rangeMin] = 0x85A0;
            mapDataStd [rangeNo] [0xEDF9 - rangeMin] = 0x858B;
            mapDataStd [rangeNo] [0xEDFA - rangeMin] = 0x85A3;
            mapDataStd [rangeNo] [0xEDFB - rangeMin] = 0x857B;
            mapDataStd [rangeNo] [0xEDFC - rangeMin] = 0x85A4;
            mapDataStd [rangeNo] [0xEDFD - rangeMin] = 0x859A;
            mapDataStd [rangeNo] [0xEDFE - rangeMin] = 0x859E;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xEE40 - rangeMin] = 0x8577;
            mapDataStd [rangeNo] [0xEE41 - rangeMin] = 0x857C;
            mapDataStd [rangeNo] [0xEE42 - rangeMin] = 0x8589;
            mapDataStd [rangeNo] [0xEE43 - rangeMin] = 0x85A1;
            mapDataStd [rangeNo] [0xEE44 - rangeMin] = 0x857A;
            mapDataStd [rangeNo] [0xEE45 - rangeMin] = 0x8578;
            mapDataStd [rangeNo] [0xEE46 - rangeMin] = 0x8557;
            mapDataStd [rangeNo] [0xEE47 - rangeMin] = 0x858E;
            mapDataStd [rangeNo] [0xEE48 - rangeMin] = 0x8596;
            mapDataStd [rangeNo] [0xEE49 - rangeMin] = 0x8586;
            mapDataStd [rangeNo] [0xEE4A - rangeMin] = 0x858D;
            mapDataStd [rangeNo] [0xEE4B - rangeMin] = 0x8599;
            mapDataStd [rangeNo] [0xEE4C - rangeMin] = 0x859D;
            mapDataStd [rangeNo] [0xEE4D - rangeMin] = 0x8581;
            mapDataStd [rangeNo] [0xEE4E - rangeMin] = 0x85A2;
            mapDataStd [rangeNo] [0xEE4F - rangeMin] = 0x8582;

            mapDataStd [rangeNo] [0xEE50 - rangeMin] = 0x8588;
            mapDataStd [rangeNo] [0xEE51 - rangeMin] = 0x8585;
            mapDataStd [rangeNo] [0xEE52 - rangeMin] = 0x8579;
            mapDataStd [rangeNo] [0xEE53 - rangeMin] = 0x8576;
            mapDataStd [rangeNo] [0xEE54 - rangeMin] = 0x8598;
            mapDataStd [rangeNo] [0xEE55 - rangeMin] = 0x8590;
            mapDataStd [rangeNo] [0xEE56 - rangeMin] = 0x859F;
            mapDataStd [rangeNo] [0xEE57 - rangeMin] = 0x8668;
            mapDataStd [rangeNo] [0xEE58 - rangeMin] = 0x87BE;
            mapDataStd [rangeNo] [0xEE59 - rangeMin] = 0x87AA;
            mapDataStd [rangeNo] [0xEE5A - rangeMin] = 0x87AD;
            mapDataStd [rangeNo] [0xEE5B - rangeMin] = 0x87C5;
            mapDataStd [rangeNo] [0xEE5C - rangeMin] = 0x87B0;
            mapDataStd [rangeNo] [0xEE5D - rangeMin] = 0x87AC;
            mapDataStd [rangeNo] [0xEE5E - rangeMin] = 0x87B9;
            mapDataStd [rangeNo] [0xEE5F - rangeMin] = 0x87B5;

            mapDataStd [rangeNo] [0xEE60 - rangeMin] = 0x87BC;
            mapDataStd [rangeNo] [0xEE61 - rangeMin] = 0x87AE;
            mapDataStd [rangeNo] [0xEE62 - rangeMin] = 0x87C9;
            mapDataStd [rangeNo] [0xEE63 - rangeMin] = 0x87C3;
            mapDataStd [rangeNo] [0xEE64 - rangeMin] = 0x87C2;
            mapDataStd [rangeNo] [0xEE65 - rangeMin] = 0x87CC;
            mapDataStd [rangeNo] [0xEE66 - rangeMin] = 0x87B7;
            mapDataStd [rangeNo] [0xEE67 - rangeMin] = 0x87AF;
            mapDataStd [rangeNo] [0xEE68 - rangeMin] = 0x87C4;
            mapDataStd [rangeNo] [0xEE69 - rangeMin] = 0x87CA;
            mapDataStd [rangeNo] [0xEE6A - rangeMin] = 0x87B4;
            mapDataStd [rangeNo] [0xEE6B - rangeMin] = 0x87B6;
            mapDataStd [rangeNo] [0xEE6C - rangeMin] = 0x87BF;
            mapDataStd [rangeNo] [0xEE6D - rangeMin] = 0x87B8;
            mapDataStd [rangeNo] [0xEE6E - rangeMin] = 0x87BD;
            mapDataStd [rangeNo] [0xEE6F - rangeMin] = 0x87DE;

            mapDataStd [rangeNo] [0xEE70 - rangeMin] = 0x87B2;
            mapDataStd [rangeNo] [0xEE71 - rangeMin] = 0x8935;
            mapDataStd [rangeNo] [0xEE72 - rangeMin] = 0x8933;
            mapDataStd [rangeNo] [0xEE73 - rangeMin] = 0x893C;
            mapDataStd [rangeNo] [0xEE74 - rangeMin] = 0x893E;
            mapDataStd [rangeNo] [0xEE75 - rangeMin] = 0x8941;
            mapDataStd [rangeNo] [0xEE76 - rangeMin] = 0x8952;
            mapDataStd [rangeNo] [0xEE77 - rangeMin] = 0x8937;
            mapDataStd [rangeNo] [0xEE78 - rangeMin] = 0x8942;
            mapDataStd [rangeNo] [0xEE79 - rangeMin] = 0x89AD;
            mapDataStd [rangeNo] [0xEE7A - rangeMin] = 0x89AF;
            mapDataStd [rangeNo] [0xEE7B - rangeMin] = 0x89AE;
            mapDataStd [rangeNo] [0xEE7C - rangeMin] = 0x89F2;
            mapDataStd [rangeNo] [0xEE7D - rangeMin] = 0x89F3;
            mapDataStd [rangeNo] [0xEE7E - rangeMin] = 0x8B1E;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xEEA1 - rangeMin] = 0x8B18;
            mapDataStd [rangeNo] [0xEEA2 - rangeMin] = 0x8B16;
            mapDataStd [rangeNo] [0xEEA3 - rangeMin] = 0x8B11;
            mapDataStd [rangeNo] [0xEEA4 - rangeMin] = 0x8B05;
            mapDataStd [rangeNo] [0xEEA5 - rangeMin] = 0x8B0B;
            mapDataStd [rangeNo] [0xEEA6 - rangeMin] = 0x8B22;
            mapDataStd [rangeNo] [0xEEA7 - rangeMin] = 0x8B0F;
            mapDataStd [rangeNo] [0xEEA8 - rangeMin] = 0x8B12;
            mapDataStd [rangeNo] [0xEEA9 - rangeMin] = 0x8B15;
            mapDataStd [rangeNo] [0xEEAA - rangeMin] = 0x8B07;
            mapDataStd [rangeNo] [0xEEAB - rangeMin] = 0x8B0D;
            mapDataStd [rangeNo] [0xEEAC - rangeMin] = 0x8B08;
            mapDataStd [rangeNo] [0xEEAD - rangeMin] = 0x8B06;
            mapDataStd [rangeNo] [0xEEAE - rangeMin] = 0x8B1C;
            mapDataStd [rangeNo] [0xEEAF - rangeMin] = 0x8B13;

            mapDataStd [rangeNo] [0xEEB0 - rangeMin] = 0x8B1A;
            mapDataStd [rangeNo] [0xEEB1 - rangeMin] = 0x8C4F;
            mapDataStd [rangeNo] [0xEEB2 - rangeMin] = 0x8C70;
            mapDataStd [rangeNo] [0xEEB3 - rangeMin] = 0x8C72;
            mapDataStd [rangeNo] [0xEEB4 - rangeMin] = 0x8C71;
            mapDataStd [rangeNo] [0xEEB5 - rangeMin] = 0x8C6F;
            mapDataStd [rangeNo] [0xEEB6 - rangeMin] = 0x8C95;
            mapDataStd [rangeNo] [0xEEB7 - rangeMin] = 0x8C94;
            mapDataStd [rangeNo] [0xEEB8 - rangeMin] = 0x8CF9;
            mapDataStd [rangeNo] [0xEEB9 - rangeMin] = 0x8D6F;
            mapDataStd [rangeNo] [0xEEBA - rangeMin] = 0x8E4E;
            mapDataStd [rangeNo] [0xEEBB - rangeMin] = 0x8E4D;
            mapDataStd [rangeNo] [0xEEBC - rangeMin] = 0x8E53;
            mapDataStd [rangeNo] [0xEEBD - rangeMin] = 0x8E50;
            mapDataStd [rangeNo] [0xEEBE - rangeMin] = 0x8E4C;
            mapDataStd [rangeNo] [0xEEBF - rangeMin] = 0x8E47;

            mapDataStd [rangeNo] [0xEEC0 - rangeMin] = 0x8F43;
            mapDataStd [rangeNo] [0xEEC1 - rangeMin] = 0x8F40;
            mapDataStd [rangeNo] [0xEEC2 - rangeMin] = 0x9085;
            mapDataStd [rangeNo] [0xEEC3 - rangeMin] = 0x907E;
            mapDataStd [rangeNo] [0xEEC4 - rangeMin] = 0x9138;
            mapDataStd [rangeNo] [0xEEC5 - rangeMin] = 0x919A;
            mapDataStd [rangeNo] [0xEEC6 - rangeMin] = 0x91A2;
            mapDataStd [rangeNo] [0xEEC7 - rangeMin] = 0x919B;
            mapDataStd [rangeNo] [0xEEC8 - rangeMin] = 0x9199;
            mapDataStd [rangeNo] [0xEEC9 - rangeMin] = 0x919F;
            mapDataStd [rangeNo] [0xEECA - rangeMin] = 0x91A1;
            mapDataStd [rangeNo] [0xEECB - rangeMin] = 0x919D;
            mapDataStd [rangeNo] [0xEECC - rangeMin] = 0x91A0;
            mapDataStd [rangeNo] [0xEECD - rangeMin] = 0x93A1;
            mapDataStd [rangeNo] [0xEECE - rangeMin] = 0x9383;
            mapDataStd [rangeNo] [0xEECF - rangeMin] = 0x93AF;

            mapDataStd [rangeNo] [0xEED0 - rangeMin] = 0x9364;
            mapDataStd [rangeNo] [0xEED1 - rangeMin] = 0x9356;
            mapDataStd [rangeNo] [0xEED2 - rangeMin] = 0x9347;
            mapDataStd [rangeNo] [0xEED3 - rangeMin] = 0x937C;
            mapDataStd [rangeNo] [0xEED4 - rangeMin] = 0x9358;
            mapDataStd [rangeNo] [0xEED5 - rangeMin] = 0x935C;
            mapDataStd [rangeNo] [0xEED6 - rangeMin] = 0x9376;
            mapDataStd [rangeNo] [0xEED7 - rangeMin] = 0x9349;
            mapDataStd [rangeNo] [0xEED8 - rangeMin] = 0x9350;
            mapDataStd [rangeNo] [0xEED9 - rangeMin] = 0x9351;
            mapDataStd [rangeNo] [0xEEDA - rangeMin] = 0x9360;
            mapDataStd [rangeNo] [0xEEDB - rangeMin] = 0x936D;
            mapDataStd [rangeNo] [0xEEDC - rangeMin] = 0x938F;
            mapDataStd [rangeNo] [0xEEDD - rangeMin] = 0x934C;
            mapDataStd [rangeNo] [0xEEDE - rangeMin] = 0x936A;
            mapDataStd [rangeNo] [0xEEDF - rangeMin] = 0x9379;

            mapDataStd [rangeNo] [0xEEE0 - rangeMin] = 0x9357;
            mapDataStd [rangeNo] [0xEEE1 - rangeMin] = 0x9355;
            mapDataStd [rangeNo] [0xEEE2 - rangeMin] = 0x9352;
            mapDataStd [rangeNo] [0xEEE3 - rangeMin] = 0x934F;
            mapDataStd [rangeNo] [0xEEE4 - rangeMin] = 0x9371;
            mapDataStd [rangeNo] [0xEEE5 - rangeMin] = 0x9377;
            mapDataStd [rangeNo] [0xEEE6 - rangeMin] = 0x937B;
            mapDataStd [rangeNo] [0xEEE7 - rangeMin] = 0x9361;
            mapDataStd [rangeNo] [0xEEE8 - rangeMin] = 0x935E;
            mapDataStd [rangeNo] [0xEEE9 - rangeMin] = 0x9363;
            mapDataStd [rangeNo] [0xEEEA - rangeMin] = 0x9367;
            mapDataStd [rangeNo] [0xEEEB - rangeMin] = 0x9380;
            mapDataStd [rangeNo] [0xEEEC - rangeMin] = 0x934E;
            mapDataStd [rangeNo] [0xEEED - rangeMin] = 0x9359;
            mapDataStd [rangeNo] [0xEEEE - rangeMin] = 0x95C7;
            mapDataStd [rangeNo] [0xEEEF - rangeMin] = 0x95C0;

            mapDataStd [rangeNo] [0xEEF0 - rangeMin] = 0x95C9;
            mapDataStd [rangeNo] [0xEEF1 - rangeMin] = 0x95C3;
            mapDataStd [rangeNo] [0xEEF2 - rangeMin] = 0x95C5;
            mapDataStd [rangeNo] [0xEEF3 - rangeMin] = 0x95B7;
            mapDataStd [rangeNo] [0xEEF4 - rangeMin] = 0x96AE;
            mapDataStd [rangeNo] [0xEEF5 - rangeMin] = 0x96B0;
            mapDataStd [rangeNo] [0xEEF6 - rangeMin] = 0x96AC;
            mapDataStd [rangeNo] [0xEEF7 - rangeMin] = 0x9720;
            mapDataStd [rangeNo] [0xEEF8 - rangeMin] = 0x971F;
            mapDataStd [rangeNo] [0xEEF9 - rangeMin] = 0x9718;
            mapDataStd [rangeNo] [0xEEFA - rangeMin] = 0x971D;
            mapDataStd [rangeNo] [0xEEFB - rangeMin] = 0x9719;
            mapDataStd [rangeNo] [0xEEFC - rangeMin] = 0x979A;
            mapDataStd [rangeNo] [0xEEFD - rangeMin] = 0x97A1;
            mapDataStd [rangeNo] [0xEEFE - rangeMin] = 0x979C;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xEF40 - rangeMin] = 0x979E;
            mapDataStd [rangeNo] [0xEF41 - rangeMin] = 0x979D;
            mapDataStd [rangeNo] [0xEF42 - rangeMin] = 0x97D5;
            mapDataStd [rangeNo] [0xEF43 - rangeMin] = 0x97D4;
            mapDataStd [rangeNo] [0xEF44 - rangeMin] = 0x97F1;
            mapDataStd [rangeNo] [0xEF45 - rangeMin] = 0x9841;
            mapDataStd [rangeNo] [0xEF46 - rangeMin] = 0x9844;
            mapDataStd [rangeNo] [0xEF47 - rangeMin] = 0x984A;
            mapDataStd [rangeNo] [0xEF48 - rangeMin] = 0x9849;
            mapDataStd [rangeNo] [0xEF49 - rangeMin] = 0x9845;
            mapDataStd [rangeNo] [0xEF4A - rangeMin] = 0x9843;
            mapDataStd [rangeNo] [0xEF4B - rangeMin] = 0x9925;
            mapDataStd [rangeNo] [0xEF4C - rangeMin] = 0x992B;
            mapDataStd [rangeNo] [0xEF4D - rangeMin] = 0x992C;
            mapDataStd [rangeNo] [0xEF4E - rangeMin] = 0x992A;
            mapDataStd [rangeNo] [0xEF4F - rangeMin] = 0x9933;

            mapDataStd [rangeNo] [0xEF50 - rangeMin] = 0x9932;
            mapDataStd [rangeNo] [0xEF51 - rangeMin] = 0x992F;
            mapDataStd [rangeNo] [0xEF52 - rangeMin] = 0x992D;
            mapDataStd [rangeNo] [0xEF53 - rangeMin] = 0x9931;
            mapDataStd [rangeNo] [0xEF54 - rangeMin] = 0x9930;
            mapDataStd [rangeNo] [0xEF55 - rangeMin] = 0x9998;
            mapDataStd [rangeNo] [0xEF56 - rangeMin] = 0x99A3;
            mapDataStd [rangeNo] [0xEF57 - rangeMin] = 0x99A1;
            mapDataStd [rangeNo] [0xEF58 - rangeMin] = 0x9A02;
            mapDataStd [rangeNo] [0xEF59 - rangeMin] = 0x99FA;
            mapDataStd [rangeNo] [0xEF5A - rangeMin] = 0x99F4;
            mapDataStd [rangeNo] [0xEF5B - rangeMin] = 0x99F7;
            mapDataStd [rangeNo] [0xEF5C - rangeMin] = 0x99F9;
            mapDataStd [rangeNo] [0xEF5D - rangeMin] = 0x99F8;
            mapDataStd [rangeNo] [0xEF5E - rangeMin] = 0x99F6;
            mapDataStd [rangeNo] [0xEF5F - rangeMin] = 0x99FB;

            mapDataStd [rangeNo] [0xEF60 - rangeMin] = 0x99FD;
            mapDataStd [rangeNo] [0xEF61 - rangeMin] = 0x99FE;
            mapDataStd [rangeNo] [0xEF62 - rangeMin] = 0x99FC;
            mapDataStd [rangeNo] [0xEF63 - rangeMin] = 0x9A03;
            mapDataStd [rangeNo] [0xEF64 - rangeMin] = 0x9ABE;
            mapDataStd [rangeNo] [0xEF65 - rangeMin] = 0x9AFE;
            mapDataStd [rangeNo] [0xEF66 - rangeMin] = 0x9AFD;
            mapDataStd [rangeNo] [0xEF67 - rangeMin] = 0x9B01;
            mapDataStd [rangeNo] [0xEF68 - rangeMin] = 0x9AFC;
            mapDataStd [rangeNo] [0xEF69 - rangeMin] = 0x9B48;
            mapDataStd [rangeNo] [0xEF6A - rangeMin] = 0x9B9A;
            mapDataStd [rangeNo] [0xEF6B - rangeMin] = 0x9BA8;
            mapDataStd [rangeNo] [0xEF6C - rangeMin] = 0x9B9E;
            mapDataStd [rangeNo] [0xEF6D - rangeMin] = 0x9B9B;
            mapDataStd [rangeNo] [0xEF6E - rangeMin] = 0x9BA6;
            mapDataStd [rangeNo] [0xEF6F - rangeMin] = 0x9BA1;

            mapDataStd [rangeNo] [0xEF70 - rangeMin] = 0x9BA5;
            mapDataStd [rangeNo] [0xEF71 - rangeMin] = 0x9BA4;
            mapDataStd [rangeNo] [0xEF72 - rangeMin] = 0x9B86;
            mapDataStd [rangeNo] [0xEF73 - rangeMin] = 0x9BA2;
            mapDataStd [rangeNo] [0xEF74 - rangeMin] = 0x9BA0;
            mapDataStd [rangeNo] [0xEF75 - rangeMin] = 0x9BAF;
            mapDataStd [rangeNo] [0xEF76 - rangeMin] = 0x9D33;
            mapDataStd [rangeNo] [0xEF77 - rangeMin] = 0x9D41;
            mapDataStd [rangeNo] [0xEF78 - rangeMin] = 0x9D67;
            mapDataStd [rangeNo] [0xEF79 - rangeMin] = 0x9D36;
            mapDataStd [rangeNo] [0xEF7A - rangeMin] = 0x9D2E;
            mapDataStd [rangeNo] [0xEF7B - rangeMin] = 0x9D2F;
            mapDataStd [rangeNo] [0xEF7C - rangeMin] = 0x9D31;
            mapDataStd [rangeNo] [0xEF7D - rangeMin] = 0x9D38;
            mapDataStd [rangeNo] [0xEF7E - rangeMin] = 0x9D30;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xEFA1 - rangeMin] = 0x9D45;
            mapDataStd [rangeNo] [0xEFA2 - rangeMin] = 0x9D42;
            mapDataStd [rangeNo] [0xEFA3 - rangeMin] = 0x9D43;
            mapDataStd [rangeNo] [0xEFA4 - rangeMin] = 0x9D3E;
            mapDataStd [rangeNo] [0xEFA5 - rangeMin] = 0x9D37;
            mapDataStd [rangeNo] [0xEFA6 - rangeMin] = 0x9D40;
            mapDataStd [rangeNo] [0xEFA7 - rangeMin] = 0x9D3D;
            mapDataStd [rangeNo] [0xEFA8 - rangeMin] = 0x7FF5;
            mapDataStd [rangeNo] [0xEFA9 - rangeMin] = 0x9D2D;
            mapDataStd [rangeNo] [0xEFAA - rangeMin] = 0x9E8A;
            mapDataStd [rangeNo] [0xEFAB - rangeMin] = 0x9E89;
            mapDataStd [rangeNo] [0xEFAC - rangeMin] = 0x9E8D;
            mapDataStd [rangeNo] [0xEFAD - rangeMin] = 0x9EB0;
            mapDataStd [rangeNo] [0xEFAE - rangeMin] = 0x9EC8;
            mapDataStd [rangeNo] [0xEFAF - rangeMin] = 0x9EDA;

            mapDataStd [rangeNo] [0xEFB0 - rangeMin] = 0x9EFB;
            mapDataStd [rangeNo] [0xEFB1 - rangeMin] = 0x9EFF;
            mapDataStd [rangeNo] [0xEFB2 - rangeMin] = 0x9F24;
            mapDataStd [rangeNo] [0xEFB3 - rangeMin] = 0x9F23;
            mapDataStd [rangeNo] [0xEFB4 - rangeMin] = 0x9F22;
            mapDataStd [rangeNo] [0xEFB5 - rangeMin] = 0x9F54;
            mapDataStd [rangeNo] [0xEFB6 - rangeMin] = 0x9FA0;
            mapDataStd [rangeNo] [0xEFB7 - rangeMin] = 0x5131;
            mapDataStd [rangeNo] [0xEFB8 - rangeMin] = 0x512D;
            mapDataStd [rangeNo] [0xEFB9 - rangeMin] = 0x512E;
            mapDataStd [rangeNo] [0xEFBA - rangeMin] = 0x5698;
            mapDataStd [rangeNo] [0xEFBB - rangeMin] = 0x569C;
            mapDataStd [rangeNo] [0xEFBC - rangeMin] = 0x5697;
            mapDataStd [rangeNo] [0xEFBD - rangeMin] = 0x569A;
            mapDataStd [rangeNo] [0xEFBE - rangeMin] = 0x569D;
            mapDataStd [rangeNo] [0xEFBF - rangeMin] = 0x5699;

            mapDataStd [rangeNo] [0xEFC0 - rangeMin] = 0x5970;
            mapDataStd [rangeNo] [0xEFC1 - rangeMin] = 0x5B3C;
            mapDataStd [rangeNo] [0xEFC2 - rangeMin] = 0x5C69;
            mapDataStd [rangeNo] [0xEFC3 - rangeMin] = 0x5C6A;
            mapDataStd [rangeNo] [0xEFC4 - rangeMin] = 0x5DC0;
            mapDataStd [rangeNo] [0xEFC5 - rangeMin] = 0x5E6D;
            mapDataStd [rangeNo] [0xEFC6 - rangeMin] = 0x5E6E;
            mapDataStd [rangeNo] [0xEFC7 - rangeMin] = 0x61D8;
            mapDataStd [rangeNo] [0xEFC8 - rangeMin] = 0x61DF;
            mapDataStd [rangeNo] [0xEFC9 - rangeMin] = 0x61ED;
            mapDataStd [rangeNo] [0xEFCA - rangeMin] = 0x61EE;
            mapDataStd [rangeNo] [0xEFCB - rangeMin] = 0x61F1;
            mapDataStd [rangeNo] [0xEFCC - rangeMin] = 0x61EA;
            mapDataStd [rangeNo] [0xEFCD - rangeMin] = 0x61F0;
            mapDataStd [rangeNo] [0xEFCE - rangeMin] = 0x61EB;
            mapDataStd [rangeNo] [0xEFCF - rangeMin] = 0x61D6;

            mapDataStd [rangeNo] [0xEFD0 - rangeMin] = 0x61E9;
            mapDataStd [rangeNo] [0xEFD1 - rangeMin] = 0x64FF;
            mapDataStd [rangeNo] [0xEFD2 - rangeMin] = 0x6504;
            mapDataStd [rangeNo] [0xEFD3 - rangeMin] = 0x64FD;
            mapDataStd [rangeNo] [0xEFD4 - rangeMin] = 0x64F8;
            mapDataStd [rangeNo] [0xEFD5 - rangeMin] = 0x6501;
            mapDataStd [rangeNo] [0xEFD6 - rangeMin] = 0x6503;
            mapDataStd [rangeNo] [0xEFD7 - rangeMin] = 0x64FC;
            mapDataStd [rangeNo] [0xEFD8 - rangeMin] = 0x6594;
            mapDataStd [rangeNo] [0xEFD9 - rangeMin] = 0x65DB;
            mapDataStd [rangeNo] [0xEFDA - rangeMin] = 0x66DA;
            mapDataStd [rangeNo] [0xEFDB - rangeMin] = 0x66DB;
            mapDataStd [rangeNo] [0xEFDC - rangeMin] = 0x66D8;
            mapDataStd [rangeNo] [0xEFDD - rangeMin] = 0x6AC5;
            mapDataStd [rangeNo] [0xEFDE - rangeMin] = 0x6AB9;
            mapDataStd [rangeNo] [0xEFDF - rangeMin] = 0x6ABD;

            mapDataStd [rangeNo] [0xEFE0 - rangeMin] = 0x6AE1;
            mapDataStd [rangeNo] [0xEFE1 - rangeMin] = 0x6AC6;
            mapDataStd [rangeNo] [0xEFE2 - rangeMin] = 0x6ABA;
            mapDataStd [rangeNo] [0xEFE3 - rangeMin] = 0x6AB6;
            mapDataStd [rangeNo] [0xEFE4 - rangeMin] = 0x6AB7;
            mapDataStd [rangeNo] [0xEFE5 - rangeMin] = 0x6AC7;
            mapDataStd [rangeNo] [0xEFE6 - rangeMin] = 0x6AB4;
            mapDataStd [rangeNo] [0xEFE7 - rangeMin] = 0x6AAD;
            mapDataStd [rangeNo] [0xEFE8 - rangeMin] = 0x6B5E;
            mapDataStd [rangeNo] [0xEFE9 - rangeMin] = 0x6BC9;
            mapDataStd [rangeNo] [0xEFEA - rangeMin] = 0x6C0B;
            mapDataStd [rangeNo] [0xEFEB - rangeMin] = 0x7007;
            mapDataStd [rangeNo] [0xEFEC - rangeMin] = 0x700C;
            mapDataStd [rangeNo] [0xEFED - rangeMin] = 0x700D;
            mapDataStd [rangeNo] [0xEFEE - rangeMin] = 0x7001;
            mapDataStd [rangeNo] [0xEFEF - rangeMin] = 0x7005;

            mapDataStd [rangeNo] [0xEFF0 - rangeMin] = 0x7014;
            mapDataStd [rangeNo] [0xEFF1 - rangeMin] = 0x700E;
            mapDataStd [rangeNo] [0xEFF2 - rangeMin] = 0x6FFF;
            mapDataStd [rangeNo] [0xEFF3 - rangeMin] = 0x7000;
            mapDataStd [rangeNo] [0xEFF4 - rangeMin] = 0x6FFB;
            mapDataStd [rangeNo] [0xEFF5 - rangeMin] = 0x7026;
            mapDataStd [rangeNo] [0xEFF6 - rangeMin] = 0x6FFC;
            mapDataStd [rangeNo] [0xEFF7 - rangeMin] = 0x6FF7;
            mapDataStd [rangeNo] [0xEFF8 - rangeMin] = 0x700A;
            mapDataStd [rangeNo] [0xEFF9 - rangeMin] = 0x7201;
            mapDataStd [rangeNo] [0xEFFA - rangeMin] = 0x71FF;
            mapDataStd [rangeNo] [0xEFFB - rangeMin] = 0x71F9;
            mapDataStd [rangeNo] [0xEFFC - rangeMin] = 0x7203;
            mapDataStd [rangeNo] [0xEFFD - rangeMin] = 0x71FD;
            mapDataStd [rangeNo] [0xEFFE - rangeMin] = 0x7376;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xF040 - rangeMin] = 0x74B8;
            mapDataStd [rangeNo] [0xF041 - rangeMin] = 0x74C0;
            mapDataStd [rangeNo] [0xF042 - rangeMin] = 0x74B5;
            mapDataStd [rangeNo] [0xF043 - rangeMin] = 0x74C1;
            mapDataStd [rangeNo] [0xF044 - rangeMin] = 0x74BE;
            mapDataStd [rangeNo] [0xF045 - rangeMin] = 0x74B6;
            mapDataStd [rangeNo] [0xF046 - rangeMin] = 0x74BB;
            mapDataStd [rangeNo] [0xF047 - rangeMin] = 0x74C2;
            mapDataStd [rangeNo] [0xF048 - rangeMin] = 0x7514;
            mapDataStd [rangeNo] [0xF049 - rangeMin] = 0x7513;
            mapDataStd [rangeNo] [0xF04A - rangeMin] = 0x765C;
            mapDataStd [rangeNo] [0xF04B - rangeMin] = 0x7664;
            mapDataStd [rangeNo] [0xF04C - rangeMin] = 0x7659;
            mapDataStd [rangeNo] [0xF04D - rangeMin] = 0x7650;
            mapDataStd [rangeNo] [0xF04E - rangeMin] = 0x7653;
            mapDataStd [rangeNo] [0xF04F - rangeMin] = 0x7657;

            mapDataStd [rangeNo] [0xF050 - rangeMin] = 0x765A;
            mapDataStd [rangeNo] [0xF051 - rangeMin] = 0x76A6;
            mapDataStd [rangeNo] [0xF052 - rangeMin] = 0x76BD;
            mapDataStd [rangeNo] [0xF053 - rangeMin] = 0x76EC;
            mapDataStd [rangeNo] [0xF054 - rangeMin] = 0x77C2;
            mapDataStd [rangeNo] [0xF055 - rangeMin] = 0x77BA;
            mapDataStd [rangeNo] [0xF056 - rangeMin] = 0x78FF;
            mapDataStd [rangeNo] [0xF057 - rangeMin] = 0x790C;
            mapDataStd [rangeNo] [0xF058 - rangeMin] = 0x7913;
            mapDataStd [rangeNo] [0xF059 - rangeMin] = 0x7914;
            mapDataStd [rangeNo] [0xF05A - rangeMin] = 0x7909;
            mapDataStd [rangeNo] [0xF05B - rangeMin] = 0x7910;
            mapDataStd [rangeNo] [0xF05C - rangeMin] = 0x7912;
            mapDataStd [rangeNo] [0xF05D - rangeMin] = 0x7911;
            mapDataStd [rangeNo] [0xF05E - rangeMin] = 0x79AD;
            mapDataStd [rangeNo] [0xF05F - rangeMin] = 0x79AC;

            mapDataStd [rangeNo] [0xF060 - rangeMin] = 0x7A5F;
            mapDataStd [rangeNo] [0xF061 - rangeMin] = 0x7C1C;
            mapDataStd [rangeNo] [0xF062 - rangeMin] = 0x7C29;
            mapDataStd [rangeNo] [0xF063 - rangeMin] = 0x7C19;
            mapDataStd [rangeNo] [0xF064 - rangeMin] = 0x7C20;
            mapDataStd [rangeNo] [0xF065 - rangeMin] = 0x7C1F;
            mapDataStd [rangeNo] [0xF066 - rangeMin] = 0x7C2D;
            mapDataStd [rangeNo] [0xF067 - rangeMin] = 0x7C1D;
            mapDataStd [rangeNo] [0xF068 - rangeMin] = 0x7C26;
            mapDataStd [rangeNo] [0xF069 - rangeMin] = 0x7C28;
            mapDataStd [rangeNo] [0xF06A - rangeMin] = 0x7C22;
            mapDataStd [rangeNo] [0xF06B - rangeMin] = 0x7C25;
            mapDataStd [rangeNo] [0xF06C - rangeMin] = 0x7C30;
            mapDataStd [rangeNo] [0xF06D - rangeMin] = 0x7E5C;
            mapDataStd [rangeNo] [0xF06E - rangeMin] = 0x7E50;
            mapDataStd [rangeNo] [0xF06F - rangeMin] = 0x7E56;

            mapDataStd [rangeNo] [0xF070 - rangeMin] = 0x7E63;
            mapDataStd [rangeNo] [0xF071 - rangeMin] = 0x7E58;
            mapDataStd [rangeNo] [0xF072 - rangeMin] = 0x7E62;
            mapDataStd [rangeNo] [0xF073 - rangeMin] = 0x7E5F;
            mapDataStd [rangeNo] [0xF074 - rangeMin] = 0x7E51;
            mapDataStd [rangeNo] [0xF075 - rangeMin] = 0x7E60;
            mapDataStd [rangeNo] [0xF076 - rangeMin] = 0x7E57;
            mapDataStd [rangeNo] [0xF077 - rangeMin] = 0x7E53;
            mapDataStd [rangeNo] [0xF078 - rangeMin] = 0x7FB5;
            mapDataStd [rangeNo] [0xF079 - rangeMin] = 0x7FB3;
            mapDataStd [rangeNo] [0xF07A - rangeMin] = 0x7FF7;
            mapDataStd [rangeNo] [0xF07B - rangeMin] = 0x7FF8;
            mapDataStd [rangeNo] [0xF07C - rangeMin] = 0x8075;
            mapDataStd [rangeNo] [0xF07D - rangeMin] = 0x81D1;
            mapDataStd [rangeNo] [0xF07E - rangeMin] = 0x81D2;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xF0A1 - rangeMin] = 0x81D0;
            mapDataStd [rangeNo] [0xF0A2 - rangeMin] = 0x825F;
            mapDataStd [rangeNo] [0xF0A3 - rangeMin] = 0x825E;
            mapDataStd [rangeNo] [0xF0A4 - rangeMin] = 0x85B4;
            mapDataStd [rangeNo] [0xF0A5 - rangeMin] = 0x85C6;
            mapDataStd [rangeNo] [0xF0A6 - rangeMin] = 0x85C0;
            mapDataStd [rangeNo] [0xF0A7 - rangeMin] = 0x85C3;
            mapDataStd [rangeNo] [0xF0A8 - rangeMin] = 0x85C2;
            mapDataStd [rangeNo] [0xF0A9 - rangeMin] = 0x85B3;
            mapDataStd [rangeNo] [0xF0AA - rangeMin] = 0x85B5;
            mapDataStd [rangeNo] [0xF0AB - rangeMin] = 0x85BD;
            mapDataStd [rangeNo] [0xF0AC - rangeMin] = 0x85C7;
            mapDataStd [rangeNo] [0xF0AD - rangeMin] = 0x85C4;
            mapDataStd [rangeNo] [0xF0AE - rangeMin] = 0x85BF;
            mapDataStd [rangeNo] [0xF0AF - rangeMin] = 0x85CB;

            mapDataStd [rangeNo] [0xF0B0 - rangeMin] = 0x85CE;
            mapDataStd [rangeNo] [0xF0B1 - rangeMin] = 0x85C8;
            mapDataStd [rangeNo] [0xF0B2 - rangeMin] = 0x85C5;
            mapDataStd [rangeNo] [0xF0B3 - rangeMin] = 0x85B1;
            mapDataStd [rangeNo] [0xF0B4 - rangeMin] = 0x85B6;
            mapDataStd [rangeNo] [0xF0B5 - rangeMin] = 0x85D2;
            mapDataStd [rangeNo] [0xF0B6 - rangeMin] = 0x8624;
            mapDataStd [rangeNo] [0xF0B7 - rangeMin] = 0x85B8;
            mapDataStd [rangeNo] [0xF0B8 - rangeMin] = 0x85B7;
            mapDataStd [rangeNo] [0xF0B9 - rangeMin] = 0x85BE;
            mapDataStd [rangeNo] [0xF0BA - rangeMin] = 0x8669;
            mapDataStd [rangeNo] [0xF0BB - rangeMin] = 0x87E7;
            mapDataStd [rangeNo] [0xF0BC - rangeMin] = 0x87E6;
            mapDataStd [rangeNo] [0xF0BD - rangeMin] = 0x87E2;
            mapDataStd [rangeNo] [0xF0BE - rangeMin] = 0x87DB;
            mapDataStd [rangeNo] [0xF0BF - rangeMin] = 0x87EB;

            mapDataStd [rangeNo] [0xF0C0 - rangeMin] = 0x87EA;
            mapDataStd [rangeNo] [0xF0C1 - rangeMin] = 0x87E5;
            mapDataStd [rangeNo] [0xF0C2 - rangeMin] = 0x87DF;
            mapDataStd [rangeNo] [0xF0C3 - rangeMin] = 0x87F3;
            mapDataStd [rangeNo] [0xF0C4 - rangeMin] = 0x87E4;
            mapDataStd [rangeNo] [0xF0C5 - rangeMin] = 0x87D4;
            mapDataStd [rangeNo] [0xF0C6 - rangeMin] = 0x87DC;
            mapDataStd [rangeNo] [0xF0C7 - rangeMin] = 0x87D3;
            mapDataStd [rangeNo] [0xF0C8 - rangeMin] = 0x87ED;
            mapDataStd [rangeNo] [0xF0C9 - rangeMin] = 0x87D8;
            mapDataStd [rangeNo] [0xF0CA - rangeMin] = 0x87E3;
            mapDataStd [rangeNo] [0xF0CB - rangeMin] = 0x87A4;
            mapDataStd [rangeNo] [0xF0CC - rangeMin] = 0x87D7;
            mapDataStd [rangeNo] [0xF0CD - rangeMin] = 0x87D9;
            mapDataStd [rangeNo] [0xF0CE - rangeMin] = 0x8801;
            mapDataStd [rangeNo] [0xF0CF - rangeMin] = 0x87F4;

            mapDataStd [rangeNo] [0xF0D0 - rangeMin] = 0x87E8;
            mapDataStd [rangeNo] [0xF0D1 - rangeMin] = 0x87DD;
            mapDataStd [rangeNo] [0xF0D2 - rangeMin] = 0x8953;
            mapDataStd [rangeNo] [0xF0D3 - rangeMin] = 0x894B;
            mapDataStd [rangeNo] [0xF0D4 - rangeMin] = 0x894F;
            mapDataStd [rangeNo] [0xF0D5 - rangeMin] = 0x894C;
            mapDataStd [rangeNo] [0xF0D6 - rangeMin] = 0x8946;
            mapDataStd [rangeNo] [0xF0D7 - rangeMin] = 0x8950;
            mapDataStd [rangeNo] [0xF0D8 - rangeMin] = 0x8951;
            mapDataStd [rangeNo] [0xF0D9 - rangeMin] = 0x8949;
            mapDataStd [rangeNo] [0xF0DA - rangeMin] = 0x8B2A;
            mapDataStd [rangeNo] [0xF0DB - rangeMin] = 0x8B27;
            mapDataStd [rangeNo] [0xF0DC - rangeMin] = 0x8B23;
            mapDataStd [rangeNo] [0xF0DD - rangeMin] = 0x8B33;
            mapDataStd [rangeNo] [0xF0DE - rangeMin] = 0x8B30;
            mapDataStd [rangeNo] [0xF0DF - rangeMin] = 0x8B35;

            mapDataStd [rangeNo] [0xF0E0 - rangeMin] = 0x8B47;
            mapDataStd [rangeNo] [0xF0E1 - rangeMin] = 0x8B2F;
            mapDataStd [rangeNo] [0xF0E2 - rangeMin] = 0x8B3C;
            mapDataStd [rangeNo] [0xF0E3 - rangeMin] = 0x8B3E;
            mapDataStd [rangeNo] [0xF0E4 - rangeMin] = 0x8B31;
            mapDataStd [rangeNo] [0xF0E5 - rangeMin] = 0x8B25;
            mapDataStd [rangeNo] [0xF0E6 - rangeMin] = 0x8B37;
            mapDataStd [rangeNo] [0xF0E7 - rangeMin] = 0x8B26;
            mapDataStd [rangeNo] [0xF0E8 - rangeMin] = 0x8B36;
            mapDataStd [rangeNo] [0xF0E9 - rangeMin] = 0x8B2E;
            mapDataStd [rangeNo] [0xF0EA - rangeMin] = 0x8B24;
            mapDataStd [rangeNo] [0xF0EB - rangeMin] = 0x8B3B;
            mapDataStd [rangeNo] [0xF0EC - rangeMin] = 0x8B3D;
            mapDataStd [rangeNo] [0xF0ED - rangeMin] = 0x8B3A;
            mapDataStd [rangeNo] [0xF0EE - rangeMin] = 0x8C42;
            mapDataStd [rangeNo] [0xF0EF - rangeMin] = 0x8C75;

            mapDataStd [rangeNo] [0xF0F0 - rangeMin] = 0x8C99;
            mapDataStd [rangeNo] [0xF0F1 - rangeMin] = 0x8C98;
            mapDataStd [rangeNo] [0xF0F2 - rangeMin] = 0x8C97;
            mapDataStd [rangeNo] [0xF0F3 - rangeMin] = 0x8CFE;
            mapDataStd [rangeNo] [0xF0F4 - rangeMin] = 0x8D04;
            mapDataStd [rangeNo] [0xF0F5 - rangeMin] = 0x8D02;
            mapDataStd [rangeNo] [0xF0F6 - rangeMin] = 0x8D00;
            mapDataStd [rangeNo] [0xF0F7 - rangeMin] = 0x8E5C;
            mapDataStd [rangeNo] [0xF0F8 - rangeMin] = 0x8E62;
            mapDataStd [rangeNo] [0xF0F9 - rangeMin] = 0x8E60;
            mapDataStd [rangeNo] [0xF0FA - rangeMin] = 0x8E57;
            mapDataStd [rangeNo] [0xF0FB - rangeMin] = 0x8E56;
            mapDataStd [rangeNo] [0xF0FC - rangeMin] = 0x8E5E;
            mapDataStd [rangeNo] [0xF0FD - rangeMin] = 0x8E65;
            mapDataStd [rangeNo] [0xF0FE - rangeMin] = 0x8E67;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xF140 - rangeMin] = 0x8E5B;
            mapDataStd [rangeNo] [0xF141 - rangeMin] = 0x8E5A;
            mapDataStd [rangeNo] [0xF142 - rangeMin] = 0x8E61;
            mapDataStd [rangeNo] [0xF143 - rangeMin] = 0x8E5D;
            mapDataStd [rangeNo] [0xF144 - rangeMin] = 0x8E69;
            mapDataStd [rangeNo] [0xF145 - rangeMin] = 0x8E54;
            mapDataStd [rangeNo] [0xF146 - rangeMin] = 0x8F46;
            mapDataStd [rangeNo] [0xF147 - rangeMin] = 0x8F47;
            mapDataStd [rangeNo] [0xF148 - rangeMin] = 0x8F48;
            mapDataStd [rangeNo] [0xF149 - rangeMin] = 0x8F4B;
            mapDataStd [rangeNo] [0xF14A - rangeMin] = 0x9128;
            mapDataStd [rangeNo] [0xF14B - rangeMin] = 0x913A;
            mapDataStd [rangeNo] [0xF14C - rangeMin] = 0x913B;
            mapDataStd [rangeNo] [0xF14D - rangeMin] = 0x913E;
            mapDataStd [rangeNo] [0xF14E - rangeMin] = 0x91A8;
            mapDataStd [rangeNo] [0xF14F - rangeMin] = 0x91A5;

            mapDataStd [rangeNo] [0xF150 - rangeMin] = 0x91A7;
            mapDataStd [rangeNo] [0xF151 - rangeMin] = 0x91AF;
            mapDataStd [rangeNo] [0xF152 - rangeMin] = 0x91AA;
            mapDataStd [rangeNo] [0xF153 - rangeMin] = 0x93B5;
            mapDataStd [rangeNo] [0xF154 - rangeMin] = 0x938C;
            mapDataStd [rangeNo] [0xF155 - rangeMin] = 0x9392;
            mapDataStd [rangeNo] [0xF156 - rangeMin] = 0x93B7;
            mapDataStd [rangeNo] [0xF157 - rangeMin] = 0x939B;
            mapDataStd [rangeNo] [0xF158 - rangeMin] = 0x939D;
            mapDataStd [rangeNo] [0xF159 - rangeMin] = 0x9389;
            mapDataStd [rangeNo] [0xF15A - rangeMin] = 0x93A7;
            mapDataStd [rangeNo] [0xF15B - rangeMin] = 0x938E;
            mapDataStd [rangeNo] [0xF15C - rangeMin] = 0x93AA;
            mapDataStd [rangeNo] [0xF15D - rangeMin] = 0x939E;
            mapDataStd [rangeNo] [0xF15E - rangeMin] = 0x93A6;
            mapDataStd [rangeNo] [0xF15F - rangeMin] = 0x9395;

            mapDataStd [rangeNo] [0xF160 - rangeMin] = 0x9388;
            mapDataStd [rangeNo] [0xF161 - rangeMin] = 0x9399;
            mapDataStd [rangeNo] [0xF162 - rangeMin] = 0x939F;
            mapDataStd [rangeNo] [0xF163 - rangeMin] = 0x938D;
            mapDataStd [rangeNo] [0xF164 - rangeMin] = 0x93B1;
            mapDataStd [rangeNo] [0xF165 - rangeMin] = 0x9391;
            mapDataStd [rangeNo] [0xF166 - rangeMin] = 0x93B2;
            mapDataStd [rangeNo] [0xF167 - rangeMin] = 0x93A4;
            mapDataStd [rangeNo] [0xF168 - rangeMin] = 0x93A8;
            mapDataStd [rangeNo] [0xF169 - rangeMin] = 0x93B4;
            mapDataStd [rangeNo] [0xF16A - rangeMin] = 0x93A3;
            mapDataStd [rangeNo] [0xF16B - rangeMin] = 0x93A5;
            mapDataStd [rangeNo] [0xF16C - rangeMin] = 0x95D2;
            mapDataStd [rangeNo] [0xF16D - rangeMin] = 0x95D3;
            mapDataStd [rangeNo] [0xF16E - rangeMin] = 0x95D1;
            mapDataStd [rangeNo] [0xF16F - rangeMin] = 0x96B3;

            mapDataStd [rangeNo] [0xF170 - rangeMin] = 0x96D7;
            mapDataStd [rangeNo] [0xF171 - rangeMin] = 0x96DA;
            mapDataStd [rangeNo] [0xF172 - rangeMin] = 0x5DC2;
            mapDataStd [rangeNo] [0xF173 - rangeMin] = 0x96DF;
            mapDataStd [rangeNo] [0xF174 - rangeMin] = 0x96D8;
            mapDataStd [rangeNo] [0xF175 - rangeMin] = 0x96DD;
            mapDataStd [rangeNo] [0xF176 - rangeMin] = 0x9723;
            mapDataStd [rangeNo] [0xF177 - rangeMin] = 0x9722;
            mapDataStd [rangeNo] [0xF178 - rangeMin] = 0x9725;
            mapDataStd [rangeNo] [0xF179 - rangeMin] = 0x97AC;
            mapDataStd [rangeNo] [0xF17A - rangeMin] = 0x97AE;
            mapDataStd [rangeNo] [0xF17B - rangeMin] = 0x97A8;
            mapDataStd [rangeNo] [0xF17C - rangeMin] = 0x97AB;
            mapDataStd [rangeNo] [0xF17D - rangeMin] = 0x97A4;
            mapDataStd [rangeNo] [0xF17E - rangeMin] = 0x97AA;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xF1A1 - rangeMin] = 0x97A2;
            mapDataStd [rangeNo] [0xF1A2 - rangeMin] = 0x97A5;
            mapDataStd [rangeNo] [0xF1A3 - rangeMin] = 0x97D7;
            mapDataStd [rangeNo] [0xF1A4 - rangeMin] = 0x97D9;
            mapDataStd [rangeNo] [0xF1A5 - rangeMin] = 0x97D6;
            mapDataStd [rangeNo] [0xF1A6 - rangeMin] = 0x97D8;
            mapDataStd [rangeNo] [0xF1A7 - rangeMin] = 0x97FA;
            mapDataStd [rangeNo] [0xF1A8 - rangeMin] = 0x9850;
            mapDataStd [rangeNo] [0xF1A9 - rangeMin] = 0x9851;
            mapDataStd [rangeNo] [0xF1AA - rangeMin] = 0x9852;
            mapDataStd [rangeNo] [0xF1AB - rangeMin] = 0x98B8;
            mapDataStd [rangeNo] [0xF1AC - rangeMin] = 0x9941;
            mapDataStd [rangeNo] [0xF1AD - rangeMin] = 0x993C;
            mapDataStd [rangeNo] [0xF1AE - rangeMin] = 0x993A;
            mapDataStd [rangeNo] [0xF1AF - rangeMin] = 0x9A0F;

            mapDataStd [rangeNo] [0xF1B0 - rangeMin] = 0x9A0B;
            mapDataStd [rangeNo] [0xF1B1 - rangeMin] = 0x9A09;
            mapDataStd [rangeNo] [0xF1B2 - rangeMin] = 0x9A0D;
            mapDataStd [rangeNo] [0xF1B3 - rangeMin] = 0x9A04;
            mapDataStd [rangeNo] [0xF1B4 - rangeMin] = 0x9A11;
            mapDataStd [rangeNo] [0xF1B5 - rangeMin] = 0x9A0A;
            mapDataStd [rangeNo] [0xF1B6 - rangeMin] = 0x9A05;
            mapDataStd [rangeNo] [0xF1B7 - rangeMin] = 0x9A07;
            mapDataStd [rangeNo] [0xF1B8 - rangeMin] = 0x9A06;
            mapDataStd [rangeNo] [0xF1B9 - rangeMin] = 0x9AC0;
            mapDataStd [rangeNo] [0xF1BA - rangeMin] = 0x9ADC;
            mapDataStd [rangeNo] [0xF1BB - rangeMin] = 0x9B08;
            mapDataStd [rangeNo] [0xF1BC - rangeMin] = 0x9B04;
            mapDataStd [rangeNo] [0xF1BD - rangeMin] = 0x9B05;
            mapDataStd [rangeNo] [0xF1BE - rangeMin] = 0x9B29;
            mapDataStd [rangeNo] [0xF1BF - rangeMin] = 0x9B35;

            mapDataStd [rangeNo] [0xF1C0 - rangeMin] = 0x9B4A;
            mapDataStd [rangeNo] [0xF1C1 - rangeMin] = 0x9B4C;
            mapDataStd [rangeNo] [0xF1C2 - rangeMin] = 0x9B4B;
            mapDataStd [rangeNo] [0xF1C3 - rangeMin] = 0x9BC7;
            mapDataStd [rangeNo] [0xF1C4 - rangeMin] = 0x9BC6;
            mapDataStd [rangeNo] [0xF1C5 - rangeMin] = 0x9BC3;
            mapDataStd [rangeNo] [0xF1C6 - rangeMin] = 0x9BBF;
            mapDataStd [rangeNo] [0xF1C7 - rangeMin] = 0x9BC1;
            mapDataStd [rangeNo] [0xF1C8 - rangeMin] = 0x9BB5;
            mapDataStd [rangeNo] [0xF1C9 - rangeMin] = 0x9BB8;
            mapDataStd [rangeNo] [0xF1CA - rangeMin] = 0x9BD3;
            mapDataStd [rangeNo] [0xF1CB - rangeMin] = 0x9BB6;
            mapDataStd [rangeNo] [0xF1CC - rangeMin] = 0x9BC4;
            mapDataStd [rangeNo] [0xF1CD - rangeMin] = 0x9BB9;
            mapDataStd [rangeNo] [0xF1CE - rangeMin] = 0x9BBD;
            mapDataStd [rangeNo] [0xF1CF - rangeMin] = 0x9D5C;

            mapDataStd [rangeNo] [0xF1D0 - rangeMin] = 0x9D53;
            mapDataStd [rangeNo] [0xF1D1 - rangeMin] = 0x9D4F;
            mapDataStd [rangeNo] [0xF1D2 - rangeMin] = 0x9D4A;
            mapDataStd [rangeNo] [0xF1D3 - rangeMin] = 0x9D5B;
            mapDataStd [rangeNo] [0xF1D4 - rangeMin] = 0x9D4B;
            mapDataStd [rangeNo] [0xF1D5 - rangeMin] = 0x9D59;
            mapDataStd [rangeNo] [0xF1D6 - rangeMin] = 0x9D56;
            mapDataStd [rangeNo] [0xF1D7 - rangeMin] = 0x9D4C;
            mapDataStd [rangeNo] [0xF1D8 - rangeMin] = 0x9D57;
            mapDataStd [rangeNo] [0xF1D9 - rangeMin] = 0x9D52;
            mapDataStd [rangeNo] [0xF1DA - rangeMin] = 0x9D54;
            mapDataStd [rangeNo] [0xF1DB - rangeMin] = 0x9D5F;
            mapDataStd [rangeNo] [0xF1DC - rangeMin] = 0x9D58;
            mapDataStd [rangeNo] [0xF1DD - rangeMin] = 0x9D5A;
            mapDataStd [rangeNo] [0xF1DE - rangeMin] = 0x9E8E;
            mapDataStd [rangeNo] [0xF1DF - rangeMin] = 0x9E8C;

            mapDataStd [rangeNo] [0xF1E0 - rangeMin] = 0x9EDF;
            mapDataStd [rangeNo] [0xF1E1 - rangeMin] = 0x9F01;
            mapDataStd [rangeNo] [0xF1E2 - rangeMin] = 0x9F00;
            mapDataStd [rangeNo] [0xF1E3 - rangeMin] = 0x9F16;
            mapDataStd [rangeNo] [0xF1E4 - rangeMin] = 0x9F25;
            mapDataStd [rangeNo] [0xF1E5 - rangeMin] = 0x9F2B;
            mapDataStd [rangeNo] [0xF1E6 - rangeMin] = 0x9F2A;
            mapDataStd [rangeNo] [0xF1E7 - rangeMin] = 0x9F29;
            mapDataStd [rangeNo] [0xF1E8 - rangeMin] = 0x9F28;
            mapDataStd [rangeNo] [0xF1E9 - rangeMin] = 0x9F4C;
            mapDataStd [rangeNo] [0xF1EA - rangeMin] = 0x9F55;
            mapDataStd [rangeNo] [0xF1EB - rangeMin] = 0x5134;
            mapDataStd [rangeNo] [0xF1EC - rangeMin] = 0x5135;
            mapDataStd [rangeNo] [0xF1ED - rangeMin] = 0x5296;
            mapDataStd [rangeNo] [0xF1EE - rangeMin] = 0x52F7;
            mapDataStd [rangeNo] [0xF1EF - rangeMin] = 0x53B4;

            mapDataStd [rangeNo] [0xF1F0 - rangeMin] = 0x56AB;
            mapDataStd [rangeNo] [0xF1F1 - rangeMin] = 0x56AD;
            mapDataStd [rangeNo] [0xF1F2 - rangeMin] = 0x56A6;
            mapDataStd [rangeNo] [0xF1F3 - rangeMin] = 0x56A7;
            mapDataStd [rangeNo] [0xF1F4 - rangeMin] = 0x56AA;
            mapDataStd [rangeNo] [0xF1F5 - rangeMin] = 0x56AC;
            mapDataStd [rangeNo] [0xF1F6 - rangeMin] = 0x58DA;
            mapDataStd [rangeNo] [0xF1F7 - rangeMin] = 0x58DD;
            mapDataStd [rangeNo] [0xF1F8 - rangeMin] = 0x58DB;
            mapDataStd [rangeNo] [0xF1F9 - rangeMin] = 0x5912;
            mapDataStd [rangeNo] [0xF1FA - rangeMin] = 0x5B3D;
            mapDataStd [rangeNo] [0xF1FB - rangeMin] = 0x5B3E;
            mapDataStd [rangeNo] [0xF1FC - rangeMin] = 0x5B3F;
            mapDataStd [rangeNo] [0xF1FD - rangeMin] = 0x5DC3;
            mapDataStd [rangeNo] [0xF1FE - rangeMin] = 0x5E70;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xF240 - rangeMin] = 0x5FBF;
            mapDataStd [rangeNo] [0xF241 - rangeMin] = 0x61FB;
            mapDataStd [rangeNo] [0xF242 - rangeMin] = 0x6507;
            mapDataStd [rangeNo] [0xF243 - rangeMin] = 0x6510;
            mapDataStd [rangeNo] [0xF244 - rangeMin] = 0x650D;
            mapDataStd [rangeNo] [0xF245 - rangeMin] = 0x6509;
            mapDataStd [rangeNo] [0xF246 - rangeMin] = 0x650C;
            mapDataStd [rangeNo] [0xF247 - rangeMin] = 0x650E;
            mapDataStd [rangeNo] [0xF248 - rangeMin] = 0x6584;
            mapDataStd [rangeNo] [0xF249 - rangeMin] = 0x65DE;
            mapDataStd [rangeNo] [0xF24A - rangeMin] = 0x65DD;
            mapDataStd [rangeNo] [0xF24B - rangeMin] = 0x66DE;
            mapDataStd [rangeNo] [0xF24C - rangeMin] = 0x6AE7;
            mapDataStd [rangeNo] [0xF24D - rangeMin] = 0x6AE0;
            mapDataStd [rangeNo] [0xF24E - rangeMin] = 0x6ACC;
            mapDataStd [rangeNo] [0xF24F - rangeMin] = 0x6AD1;

            mapDataStd [rangeNo] [0xF250 - rangeMin] = 0x6AD9;
            mapDataStd [rangeNo] [0xF251 - rangeMin] = 0x6ACB;
            mapDataStd [rangeNo] [0xF252 - rangeMin] = 0x6ADF;
            mapDataStd [rangeNo] [0xF253 - rangeMin] = 0x6ADC;
            mapDataStd [rangeNo] [0xF254 - rangeMin] = 0x6AD0;
            mapDataStd [rangeNo] [0xF255 - rangeMin] = 0x6AEB;
            mapDataStd [rangeNo] [0xF256 - rangeMin] = 0x6ACF;
            mapDataStd [rangeNo] [0xF257 - rangeMin] = 0x6ACD;
            mapDataStd [rangeNo] [0xF258 - rangeMin] = 0x6ADE;
            mapDataStd [rangeNo] [0xF259 - rangeMin] = 0x6B60;
            mapDataStd [rangeNo] [0xF25A - rangeMin] = 0x6BB0;
            mapDataStd [rangeNo] [0xF25B - rangeMin] = 0x6C0C;
            mapDataStd [rangeNo] [0xF25C - rangeMin] = 0x7019;
            mapDataStd [rangeNo] [0xF25D - rangeMin] = 0x7027;
            mapDataStd [rangeNo] [0xF25E - rangeMin] = 0x7020;
            mapDataStd [rangeNo] [0xF25F - rangeMin] = 0x7016;

            mapDataStd [rangeNo] [0xF260 - rangeMin] = 0x702B;
            mapDataStd [rangeNo] [0xF261 - rangeMin] = 0x7021;
            mapDataStd [rangeNo] [0xF262 - rangeMin] = 0x7022;
            mapDataStd [rangeNo] [0xF263 - rangeMin] = 0x7023;
            mapDataStd [rangeNo] [0xF264 - rangeMin] = 0x7029;
            mapDataStd [rangeNo] [0xF265 - rangeMin] = 0x7017;
            mapDataStd [rangeNo] [0xF266 - rangeMin] = 0x7024;
            mapDataStd [rangeNo] [0xF267 - rangeMin] = 0x701C;
            mapDataStd [rangeNo] [0xF268 - rangeMin] = 0x702A;
            mapDataStd [rangeNo] [0xF269 - rangeMin] = 0x720C;
            mapDataStd [rangeNo] [0xF26A - rangeMin] = 0x720A;
            mapDataStd [rangeNo] [0xF26B - rangeMin] = 0x7207;
            mapDataStd [rangeNo] [0xF26C - rangeMin] = 0x7202;
            mapDataStd [rangeNo] [0xF26D - rangeMin] = 0x7205;
            mapDataStd [rangeNo] [0xF26E - rangeMin] = 0x72A5;
            mapDataStd [rangeNo] [0xF26F - rangeMin] = 0x72A6;

            mapDataStd [rangeNo] [0xF270 - rangeMin] = 0x72A4;
            mapDataStd [rangeNo] [0xF271 - rangeMin] = 0x72A3;
            mapDataStd [rangeNo] [0xF272 - rangeMin] = 0x72A1;
            mapDataStd [rangeNo] [0xF273 - rangeMin] = 0x74CB;
            mapDataStd [rangeNo] [0xF274 - rangeMin] = 0x74C5;
            mapDataStd [rangeNo] [0xF275 - rangeMin] = 0x74B7;
            mapDataStd [rangeNo] [0xF276 - rangeMin] = 0x74C3;
            mapDataStd [rangeNo] [0xF277 - rangeMin] = 0x7516;
            mapDataStd [rangeNo] [0xF278 - rangeMin] = 0x7660;
            mapDataStd [rangeNo] [0xF279 - rangeMin] = 0x77C9;
            mapDataStd [rangeNo] [0xF27A - rangeMin] = 0x77CA;
            mapDataStd [rangeNo] [0xF27B - rangeMin] = 0x77C4;
            mapDataStd [rangeNo] [0xF27C - rangeMin] = 0x77F1;
            mapDataStd [rangeNo] [0xF27D - rangeMin] = 0x791D;
            mapDataStd [rangeNo] [0xF27E - rangeMin] = 0x791B;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xF2A1 - rangeMin] = 0x7921;
            mapDataStd [rangeNo] [0xF2A2 - rangeMin] = 0x791C;
            mapDataStd [rangeNo] [0xF2A3 - rangeMin] = 0x7917;
            mapDataStd [rangeNo] [0xF2A4 - rangeMin] = 0x791E;
            mapDataStd [rangeNo] [0xF2A5 - rangeMin] = 0x79B0;
            mapDataStd [rangeNo] [0xF2A6 - rangeMin] = 0x7A67;
            mapDataStd [rangeNo] [0xF2A7 - rangeMin] = 0x7A68;
            mapDataStd [rangeNo] [0xF2A8 - rangeMin] = 0x7C33;
            mapDataStd [rangeNo] [0xF2A9 - rangeMin] = 0x7C3C;
            mapDataStd [rangeNo] [0xF2AA - rangeMin] = 0x7C39;
            mapDataStd [rangeNo] [0xF2AB - rangeMin] = 0x7C2C;
            mapDataStd [rangeNo] [0xF2AC - rangeMin] = 0x7C3B;
            mapDataStd [rangeNo] [0xF2AD - rangeMin] = 0x7CEC;
            mapDataStd [rangeNo] [0xF2AE - rangeMin] = 0x7CEA;
            mapDataStd [rangeNo] [0xF2AF - rangeMin] = 0x7E76;

            mapDataStd [rangeNo] [0xF2B0 - rangeMin] = 0x7E75;
            mapDataStd [rangeNo] [0xF2B1 - rangeMin] = 0x7E78;
            mapDataStd [rangeNo] [0xF2B2 - rangeMin] = 0x7E70;
            mapDataStd [rangeNo] [0xF2B3 - rangeMin] = 0x7E77;
            mapDataStd [rangeNo] [0xF2B4 - rangeMin] = 0x7E6F;
            mapDataStd [rangeNo] [0xF2B5 - rangeMin] = 0x7E7A;
            mapDataStd [rangeNo] [0xF2B6 - rangeMin] = 0x7E72;
            mapDataStd [rangeNo] [0xF2B7 - rangeMin] = 0x7E74;
            mapDataStd [rangeNo] [0xF2B8 - rangeMin] = 0x7E68;
            mapDataStd [rangeNo] [0xF2B9 - rangeMin] = 0x7F4B;
            mapDataStd [rangeNo] [0xF2BA - rangeMin] = 0x7F4A;
            mapDataStd [rangeNo] [0xF2BB - rangeMin] = 0x7F83;
            mapDataStd [rangeNo] [0xF2BC - rangeMin] = 0x7F86;
            mapDataStd [rangeNo] [0xF2BD - rangeMin] = 0x7FB7;
            mapDataStd [rangeNo] [0xF2BE - rangeMin] = 0x7FFD;
            mapDataStd [rangeNo] [0xF2BF - rangeMin] = 0x7FFE;

            mapDataStd [rangeNo] [0xF2C0 - rangeMin] = 0x8078;
            mapDataStd [rangeNo] [0xF2C1 - rangeMin] = 0x81D7;
            mapDataStd [rangeNo] [0xF2C2 - rangeMin] = 0x81D5;
            mapDataStd [rangeNo] [0xF2C3 - rangeMin] = 0x8264;
            mapDataStd [rangeNo] [0xF2C4 - rangeMin] = 0x8261;
            mapDataStd [rangeNo] [0xF2C5 - rangeMin] = 0x8263;
            mapDataStd [rangeNo] [0xF2C6 - rangeMin] = 0x85EB;
            mapDataStd [rangeNo] [0xF2C7 - rangeMin] = 0x85F1;
            mapDataStd [rangeNo] [0xF2C8 - rangeMin] = 0x85ED;
            mapDataStd [rangeNo] [0xF2C9 - rangeMin] = 0x85D9;
            mapDataStd [rangeNo] [0xF2CA - rangeMin] = 0x85E1;
            mapDataStd [rangeNo] [0xF2CB - rangeMin] = 0x85E8;
            mapDataStd [rangeNo] [0xF2CC - rangeMin] = 0x85DA;
            mapDataStd [rangeNo] [0xF2CD - rangeMin] = 0x85D7;
            mapDataStd [rangeNo] [0xF2CE - rangeMin] = 0x85EC;
            mapDataStd [rangeNo] [0xF2CF - rangeMin] = 0x85F2;

            mapDataStd [rangeNo] [0xF2D0 - rangeMin] = 0x85F8;
            mapDataStd [rangeNo] [0xF2D1 - rangeMin] = 0x85D8;
            mapDataStd [rangeNo] [0xF2D2 - rangeMin] = 0x85DF;
            mapDataStd [rangeNo] [0xF2D3 - rangeMin] = 0x85E3;
            mapDataStd [rangeNo] [0xF2D4 - rangeMin] = 0x85DC;
            mapDataStd [rangeNo] [0xF2D5 - rangeMin] = 0x85D1;
            mapDataStd [rangeNo] [0xF2D6 - rangeMin] = 0x85F0;
            mapDataStd [rangeNo] [0xF2D7 - rangeMin] = 0x85E6;
            mapDataStd [rangeNo] [0xF2D8 - rangeMin] = 0x85EF;
            mapDataStd [rangeNo] [0xF2D9 - rangeMin] = 0x85DE;
            mapDataStd [rangeNo] [0xF2DA - rangeMin] = 0x85E2;
            mapDataStd [rangeNo] [0xF2DB - rangeMin] = 0x8800;
            mapDataStd [rangeNo] [0xF2DC - rangeMin] = 0x87FA;
            mapDataStd [rangeNo] [0xF2DD - rangeMin] = 0x8803;
            mapDataStd [rangeNo] [0xF2DE - rangeMin] = 0x87F6;
            mapDataStd [rangeNo] [0xF2DF - rangeMin] = 0x87F7;

            mapDataStd [rangeNo] [0xF2E0 - rangeMin] = 0x8809;
            mapDataStd [rangeNo] [0xF2E1 - rangeMin] = 0x880C;
            mapDataStd [rangeNo] [0xF2E2 - rangeMin] = 0x880B;
            mapDataStd [rangeNo] [0xF2E3 - rangeMin] = 0x8806;
            mapDataStd [rangeNo] [0xF2E4 - rangeMin] = 0x87FC;
            mapDataStd [rangeNo] [0xF2E5 - rangeMin] = 0x8808;
            mapDataStd [rangeNo] [0xF2E6 - rangeMin] = 0x87FF;
            mapDataStd [rangeNo] [0xF2E7 - rangeMin] = 0x880A;
            mapDataStd [rangeNo] [0xF2E8 - rangeMin] = 0x8802;
            mapDataStd [rangeNo] [0xF2E9 - rangeMin] = 0x8962;
            mapDataStd [rangeNo] [0xF2EA - rangeMin] = 0x895A;
            mapDataStd [rangeNo] [0xF2EB - rangeMin] = 0x895B;
            mapDataStd [rangeNo] [0xF2EC - rangeMin] = 0x8957;
            mapDataStd [rangeNo] [0xF2ED - rangeMin] = 0x8961;
            mapDataStd [rangeNo] [0xF2EE - rangeMin] = 0x895C;
            mapDataStd [rangeNo] [0xF2EF - rangeMin] = 0x8958;

            mapDataStd [rangeNo] [0xF2F0 - rangeMin] = 0x895D;
            mapDataStd [rangeNo] [0xF2F1 - rangeMin] = 0x8959;
            mapDataStd [rangeNo] [0xF2F2 - rangeMin] = 0x8988;
            mapDataStd [rangeNo] [0xF2F3 - rangeMin] = 0x89B7;
            mapDataStd [rangeNo] [0xF2F4 - rangeMin] = 0x89B6;
            mapDataStd [rangeNo] [0xF2F5 - rangeMin] = 0x89F6;
            mapDataStd [rangeNo] [0xF2F6 - rangeMin] = 0x8B50;
            mapDataStd [rangeNo] [0xF2F7 - rangeMin] = 0x8B48;
            mapDataStd [rangeNo] [0xF2F8 - rangeMin] = 0x8B4A;
            mapDataStd [rangeNo] [0xF2F9 - rangeMin] = 0x8B40;
            mapDataStd [rangeNo] [0xF2FA - rangeMin] = 0x8B53;
            mapDataStd [rangeNo] [0xF2FB - rangeMin] = 0x8B56;
            mapDataStd [rangeNo] [0xF2FC - rangeMin] = 0x8B54;
            mapDataStd [rangeNo] [0xF2FD - rangeMin] = 0x8B4B;
            mapDataStd [rangeNo] [0xF2FE - rangeMin] = 0x8B55;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xF340 - rangeMin] = 0x8B51;
            mapDataStd [rangeNo] [0xF341 - rangeMin] = 0x8B42;
            mapDataStd [rangeNo] [0xF342 - rangeMin] = 0x8B52;
            mapDataStd [rangeNo] [0xF343 - rangeMin] = 0x8B57;
            mapDataStd [rangeNo] [0xF344 - rangeMin] = 0x8C43;
            mapDataStd [rangeNo] [0xF345 - rangeMin] = 0x8C77;
            mapDataStd [rangeNo] [0xF346 - rangeMin] = 0x8C76;
            mapDataStd [rangeNo] [0xF347 - rangeMin] = 0x8C9A;
            mapDataStd [rangeNo] [0xF348 - rangeMin] = 0x8D06;
            mapDataStd [rangeNo] [0xF349 - rangeMin] = 0x8D07;
            mapDataStd [rangeNo] [0xF34A - rangeMin] = 0x8D09;
            mapDataStd [rangeNo] [0xF34B - rangeMin] = 0x8DAC;
            mapDataStd [rangeNo] [0xF34C - rangeMin] = 0x8DAA;
            mapDataStd [rangeNo] [0xF34D - rangeMin] = 0x8DAD;
            mapDataStd [rangeNo] [0xF34E - rangeMin] = 0x8DAB;
            mapDataStd [rangeNo] [0xF34F - rangeMin] = 0x8E6D;

            mapDataStd [rangeNo] [0xF350 - rangeMin] = 0x8E78;
            mapDataStd [rangeNo] [0xF351 - rangeMin] = 0x8E73;
            mapDataStd [rangeNo] [0xF352 - rangeMin] = 0x8E6A;
            mapDataStd [rangeNo] [0xF353 - rangeMin] = 0x8E6F;
            mapDataStd [rangeNo] [0xF354 - rangeMin] = 0x8E7B;
            mapDataStd [rangeNo] [0xF355 - rangeMin] = 0x8EC2;
            mapDataStd [rangeNo] [0xF356 - rangeMin] = 0x8F52;
            mapDataStd [rangeNo] [0xF357 - rangeMin] = 0x8F51;
            mapDataStd [rangeNo] [0xF358 - rangeMin] = 0x8F4F;
            mapDataStd [rangeNo] [0xF359 - rangeMin] = 0x8F50;
            mapDataStd [rangeNo] [0xF35A - rangeMin] = 0x8F53;
            mapDataStd [rangeNo] [0xF35B - rangeMin] = 0x8FB4;
            mapDataStd [rangeNo] [0xF35C - rangeMin] = 0x9140;
            mapDataStd [rangeNo] [0xF35D - rangeMin] = 0x913F;
            mapDataStd [rangeNo] [0xF35E - rangeMin] = 0x91B0;
            mapDataStd [rangeNo] [0xF35F - rangeMin] = 0x91AD;

            mapDataStd [rangeNo] [0xF360 - rangeMin] = 0x93DE;
            mapDataStd [rangeNo] [0xF361 - rangeMin] = 0x93C7;
            mapDataStd [rangeNo] [0xF362 - rangeMin] = 0x93CF;
            mapDataStd [rangeNo] [0xF363 - rangeMin] = 0x93C2;
            mapDataStd [rangeNo] [0xF364 - rangeMin] = 0x93DA;
            mapDataStd [rangeNo] [0xF365 - rangeMin] = 0x93D0;
            mapDataStd [rangeNo] [0xF366 - rangeMin] = 0x93F9;
            mapDataStd [rangeNo] [0xF367 - rangeMin] = 0x93EC;
            mapDataStd [rangeNo] [0xF368 - rangeMin] = 0x93CC;
            mapDataStd [rangeNo] [0xF369 - rangeMin] = 0x93D9;
            mapDataStd [rangeNo] [0xF36A - rangeMin] = 0x93A9;
            mapDataStd [rangeNo] [0xF36B - rangeMin] = 0x93E6;
            mapDataStd [rangeNo] [0xF36C - rangeMin] = 0x93CA;
            mapDataStd [rangeNo] [0xF36D - rangeMin] = 0x93D4;
            mapDataStd [rangeNo] [0xF36E - rangeMin] = 0x93EE;
            mapDataStd [rangeNo] [0xF36F - rangeMin] = 0x93E3;

            mapDataStd [rangeNo] [0xF370 - rangeMin] = 0x93D5;
            mapDataStd [rangeNo] [0xF371 - rangeMin] = 0x93C4;
            mapDataStd [rangeNo] [0xF372 - rangeMin] = 0x93CE;
            mapDataStd [rangeNo] [0xF373 - rangeMin] = 0x93C0;
            mapDataStd [rangeNo] [0xF374 - rangeMin] = 0x93D2;
            mapDataStd [rangeNo] [0xF375 - rangeMin] = 0x93E7;
            mapDataStd [rangeNo] [0xF376 - rangeMin] = 0x957D;
            mapDataStd [rangeNo] [0xF377 - rangeMin] = 0x95DA;
            mapDataStd [rangeNo] [0xF378 - rangeMin] = 0x95DB;
            mapDataStd [rangeNo] [0xF379 - rangeMin] = 0x96E1;
            mapDataStd [rangeNo] [0xF37A - rangeMin] = 0x9729;
            mapDataStd [rangeNo] [0xF37B - rangeMin] = 0x972B;
            mapDataStd [rangeNo] [0xF37C - rangeMin] = 0x972C;
            mapDataStd [rangeNo] [0xF37D - rangeMin] = 0x9728;
            mapDataStd [rangeNo] [0xF37E - rangeMin] = 0x9726;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xF3A1 - rangeMin] = 0x97B3;
            mapDataStd [rangeNo] [0xF3A2 - rangeMin] = 0x97B7;
            mapDataStd [rangeNo] [0xF3A3 - rangeMin] = 0x97B6;
            mapDataStd [rangeNo] [0xF3A4 - rangeMin] = 0x97DD;
            mapDataStd [rangeNo] [0xF3A5 - rangeMin] = 0x97DE;
            mapDataStd [rangeNo] [0xF3A6 - rangeMin] = 0x97DF;
            mapDataStd [rangeNo] [0xF3A7 - rangeMin] = 0x985C;
            mapDataStd [rangeNo] [0xF3A8 - rangeMin] = 0x9859;
            mapDataStd [rangeNo] [0xF3A9 - rangeMin] = 0x985D;
            mapDataStd [rangeNo] [0xF3AA - rangeMin] = 0x9857;
            mapDataStd [rangeNo] [0xF3AB - rangeMin] = 0x98BF;
            mapDataStd [rangeNo] [0xF3AC - rangeMin] = 0x98BD;
            mapDataStd [rangeNo] [0xF3AD - rangeMin] = 0x98BB;
            mapDataStd [rangeNo] [0xF3AE - rangeMin] = 0x98BE;
            mapDataStd [rangeNo] [0xF3AF - rangeMin] = 0x9948;

            mapDataStd [rangeNo] [0xF3B0 - rangeMin] = 0x9947;
            mapDataStd [rangeNo] [0xF3B1 - rangeMin] = 0x9943;
            mapDataStd [rangeNo] [0xF3B2 - rangeMin] = 0x99A6;
            mapDataStd [rangeNo] [0xF3B3 - rangeMin] = 0x99A7;
            mapDataStd [rangeNo] [0xF3B4 - rangeMin] = 0x9A1A;
            mapDataStd [rangeNo] [0xF3B5 - rangeMin] = 0x9A15;
            mapDataStd [rangeNo] [0xF3B6 - rangeMin] = 0x9A25;
            mapDataStd [rangeNo] [0xF3B7 - rangeMin] = 0x9A1D;
            mapDataStd [rangeNo] [0xF3B8 - rangeMin] = 0x9A24;
            mapDataStd [rangeNo] [0xF3B9 - rangeMin] = 0x9A1B;
            mapDataStd [rangeNo] [0xF3BA - rangeMin] = 0x9A22;
            mapDataStd [rangeNo] [0xF3BB - rangeMin] = 0x9A20;
            mapDataStd [rangeNo] [0xF3BC - rangeMin] = 0x9A27;
            mapDataStd [rangeNo] [0xF3BD - rangeMin] = 0x9A23;
            mapDataStd [rangeNo] [0xF3BE - rangeMin] = 0x9A1E;
            mapDataStd [rangeNo] [0xF3BF - rangeMin] = 0x9A1C;

            mapDataStd [rangeNo] [0xF3C0 - rangeMin] = 0x9A14;
            mapDataStd [rangeNo] [0xF3C1 - rangeMin] = 0x9AC2;
            mapDataStd [rangeNo] [0xF3C2 - rangeMin] = 0x9B0B;
            mapDataStd [rangeNo] [0xF3C3 - rangeMin] = 0x9B0A;
            mapDataStd [rangeNo] [0xF3C4 - rangeMin] = 0x9B0E;
            mapDataStd [rangeNo] [0xF3C5 - rangeMin] = 0x9B0C;
            mapDataStd [rangeNo] [0xF3C6 - rangeMin] = 0x9B37;
            mapDataStd [rangeNo] [0xF3C7 - rangeMin] = 0x9BEA;
            mapDataStd [rangeNo] [0xF3C8 - rangeMin] = 0x9BEB;
            mapDataStd [rangeNo] [0xF3C9 - rangeMin] = 0x9BE0;
            mapDataStd [rangeNo] [0xF3CA - rangeMin] = 0x9BDE;
            mapDataStd [rangeNo] [0xF3CB - rangeMin] = 0x9BE4;
            mapDataStd [rangeNo] [0xF3CC - rangeMin] = 0x9BE6;
            mapDataStd [rangeNo] [0xF3CD - rangeMin] = 0x9BE2;
            mapDataStd [rangeNo] [0xF3CE - rangeMin] = 0x9BF0;
            mapDataStd [rangeNo] [0xF3CF - rangeMin] = 0x9BD4;

            mapDataStd [rangeNo] [0xF3D0 - rangeMin] = 0x9BD7;
            mapDataStd [rangeNo] [0xF3D1 - rangeMin] = 0x9BEC;
            mapDataStd [rangeNo] [0xF3D2 - rangeMin] = 0x9BDC;
            mapDataStd [rangeNo] [0xF3D3 - rangeMin] = 0x9BD9;
            mapDataStd [rangeNo] [0xF3D4 - rangeMin] = 0x9BE5;
            mapDataStd [rangeNo] [0xF3D5 - rangeMin] = 0x9BD5;
            mapDataStd [rangeNo] [0xF3D6 - rangeMin] = 0x9BE1;
            mapDataStd [rangeNo] [0xF3D7 - rangeMin] = 0x9BDA;
            mapDataStd [rangeNo] [0xF3D8 - rangeMin] = 0x9D77;
            mapDataStd [rangeNo] [0xF3D9 - rangeMin] = 0x9D81;
            mapDataStd [rangeNo] [0xF3DA - rangeMin] = 0x9D8A;
            mapDataStd [rangeNo] [0xF3DB - rangeMin] = 0x9D84;
            mapDataStd [rangeNo] [0xF3DC - rangeMin] = 0x9D88;
            mapDataStd [rangeNo] [0xF3DD - rangeMin] = 0x9D71;
            mapDataStd [rangeNo] [0xF3DE - rangeMin] = 0x9D80;
            mapDataStd [rangeNo] [0xF3DF - rangeMin] = 0x9D78;

            mapDataStd [rangeNo] [0xF3E0 - rangeMin] = 0x9D86;
            mapDataStd [rangeNo] [0xF3E1 - rangeMin] = 0x9D8B;
            mapDataStd [rangeNo] [0xF3E2 - rangeMin] = 0x9D8C;
            mapDataStd [rangeNo] [0xF3E3 - rangeMin] = 0x9D7D;
            mapDataStd [rangeNo] [0xF3E4 - rangeMin] = 0x9D6B;
            mapDataStd [rangeNo] [0xF3E5 - rangeMin] = 0x9D74;
            mapDataStd [rangeNo] [0xF3E6 - rangeMin] = 0x9D75;
            mapDataStd [rangeNo] [0xF3E7 - rangeMin] = 0x9D70;
            mapDataStd [rangeNo] [0xF3E8 - rangeMin] = 0x9D69;
            mapDataStd [rangeNo] [0xF3E9 - rangeMin] = 0x9D85;
            mapDataStd [rangeNo] [0xF3EA - rangeMin] = 0x9D73;
            mapDataStd [rangeNo] [0xF3EB - rangeMin] = 0x9D7B;
            mapDataStd [rangeNo] [0xF3EC - rangeMin] = 0x9D82;
            mapDataStd [rangeNo] [0xF3ED - rangeMin] = 0x9D6F;
            mapDataStd [rangeNo] [0xF3EE - rangeMin] = 0x9D79;
            mapDataStd [rangeNo] [0xF3EF - rangeMin] = 0x9D7F;

            mapDataStd [rangeNo] [0xF3F0 - rangeMin] = 0x9D87;
            mapDataStd [rangeNo] [0xF3F1 - rangeMin] = 0x9D68;
            mapDataStd [rangeNo] [0xF3F2 - rangeMin] = 0x9E94;
            mapDataStd [rangeNo] [0xF3F3 - rangeMin] = 0x9E91;
            mapDataStd [rangeNo] [0xF3F4 - rangeMin] = 0x9EC0;
            mapDataStd [rangeNo] [0xF3F5 - rangeMin] = 0x9EFC;
            mapDataStd [rangeNo] [0xF3F6 - rangeMin] = 0x9F2D;
            mapDataStd [rangeNo] [0xF3F7 - rangeMin] = 0x9F40;
            mapDataStd [rangeNo] [0xF3F8 - rangeMin] = 0x9F41;
            mapDataStd [rangeNo] [0xF3F9 - rangeMin] = 0x9F4D;
            mapDataStd [rangeNo] [0xF3FA - rangeMin] = 0x9F56;
            mapDataStd [rangeNo] [0xF3FB - rangeMin] = 0x9F57;
            mapDataStd [rangeNo] [0xF3FC - rangeMin] = 0x9F58;
            mapDataStd [rangeNo] [0xF3FD - rangeMin] = 0x5337;
            mapDataStd [rangeNo] [0xF3FE - rangeMin] = 0x56B2;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xF440 - rangeMin] = 0x56B5;
            mapDataStd [rangeNo] [0xF441 - rangeMin] = 0x56B3;
            mapDataStd [rangeNo] [0xF442 - rangeMin] = 0x58E3;
            mapDataStd [rangeNo] [0xF443 - rangeMin] = 0x5B45;
            mapDataStd [rangeNo] [0xF444 - rangeMin] = 0x5DC6;
            mapDataStd [rangeNo] [0xF445 - rangeMin] = 0x5DC7;
            mapDataStd [rangeNo] [0xF446 - rangeMin] = 0x5EEE;
            mapDataStd [rangeNo] [0xF447 - rangeMin] = 0x5EEF;
            mapDataStd [rangeNo] [0xF448 - rangeMin] = 0x5FC0;
            mapDataStd [rangeNo] [0xF449 - rangeMin] = 0x5FC1;
            mapDataStd [rangeNo] [0xF44A - rangeMin] = 0x61F9;
            mapDataStd [rangeNo] [0xF44B - rangeMin] = 0x6517;
            mapDataStd [rangeNo] [0xF44C - rangeMin] = 0x6516;
            mapDataStd [rangeNo] [0xF44D - rangeMin] = 0x6515;
            mapDataStd [rangeNo] [0xF44E - rangeMin] = 0x6513;
            mapDataStd [rangeNo] [0xF44F - rangeMin] = 0x65DF;

            mapDataStd [rangeNo] [0xF450 - rangeMin] = 0x66E8;
            mapDataStd [rangeNo] [0xF451 - rangeMin] = 0x66E3;
            mapDataStd [rangeNo] [0xF452 - rangeMin] = 0x66E4;
            mapDataStd [rangeNo] [0xF453 - rangeMin] = 0x6AF3;
            mapDataStd [rangeNo] [0xF454 - rangeMin] = 0x6AF0;
            mapDataStd [rangeNo] [0xF455 - rangeMin] = 0x6AEA;
            mapDataStd [rangeNo] [0xF456 - rangeMin] = 0x6AE8;
            mapDataStd [rangeNo] [0xF457 - rangeMin] = 0x6AF9;
            mapDataStd [rangeNo] [0xF458 - rangeMin] = 0x6AF1;
            mapDataStd [rangeNo] [0xF459 - rangeMin] = 0x6AEE;
            mapDataStd [rangeNo] [0xF45A - rangeMin] = 0x6AEF;
            mapDataStd [rangeNo] [0xF45B - rangeMin] = 0x703C;
            mapDataStd [rangeNo] [0xF45C - rangeMin] = 0x7035;
            mapDataStd [rangeNo] [0xF45D - rangeMin] = 0x702F;
            mapDataStd [rangeNo] [0xF45E - rangeMin] = 0x7037;
            mapDataStd [rangeNo] [0xF45F - rangeMin] = 0x7034;

            mapDataStd [rangeNo] [0xF460 - rangeMin] = 0x7031;
            mapDataStd [rangeNo] [0xF461 - rangeMin] = 0x7042;
            mapDataStd [rangeNo] [0xF462 - rangeMin] = 0x7038;
            mapDataStd [rangeNo] [0xF463 - rangeMin] = 0x703F;
            mapDataStd [rangeNo] [0xF464 - rangeMin] = 0x703A;
            mapDataStd [rangeNo] [0xF465 - rangeMin] = 0x7039;
            mapDataStd [rangeNo] [0xF466 - rangeMin] = 0x7040;
            mapDataStd [rangeNo] [0xF467 - rangeMin] = 0x703B;
            mapDataStd [rangeNo] [0xF468 - rangeMin] = 0x7033;
            mapDataStd [rangeNo] [0xF469 - rangeMin] = 0x7041;
            mapDataStd [rangeNo] [0xF46A - rangeMin] = 0x7213;
            mapDataStd [rangeNo] [0xF46B - rangeMin] = 0x7214;
            mapDataStd [rangeNo] [0xF46C - rangeMin] = 0x72A8;
            mapDataStd [rangeNo] [0xF46D - rangeMin] = 0x737D;
            mapDataStd [rangeNo] [0xF46E - rangeMin] = 0x737C;
            mapDataStd [rangeNo] [0xF46F - rangeMin] = 0x74BA;

            mapDataStd [rangeNo] [0xF470 - rangeMin] = 0x76AB;
            mapDataStd [rangeNo] [0xF471 - rangeMin] = 0x76AA;
            mapDataStd [rangeNo] [0xF472 - rangeMin] = 0x76BE;
            mapDataStd [rangeNo] [0xF473 - rangeMin] = 0x76ED;
            mapDataStd [rangeNo] [0xF474 - rangeMin] = 0x77CC;
            mapDataStd [rangeNo] [0xF475 - rangeMin] = 0x77CE;
            mapDataStd [rangeNo] [0xF476 - rangeMin] = 0x77CF;
            mapDataStd [rangeNo] [0xF477 - rangeMin] = 0x77CD;
            mapDataStd [rangeNo] [0xF478 - rangeMin] = 0x77F2;
            mapDataStd [rangeNo] [0xF479 - rangeMin] = 0x7925;
            mapDataStd [rangeNo] [0xF47A - rangeMin] = 0x7923;
            mapDataStd [rangeNo] [0xF47B - rangeMin] = 0x7927;
            mapDataStd [rangeNo] [0xF47C - rangeMin] = 0x7928;
            mapDataStd [rangeNo] [0xF47D - rangeMin] = 0x7924;
            mapDataStd [rangeNo] [0xF47E - rangeMin] = 0x7929;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xF4A1 - rangeMin] = 0x79B2;
            mapDataStd [rangeNo] [0xF4A2 - rangeMin] = 0x7A6E;
            mapDataStd [rangeNo] [0xF4A3 - rangeMin] = 0x7A6C;
            mapDataStd [rangeNo] [0xF4A4 - rangeMin] = 0x7A6D;
            mapDataStd [rangeNo] [0xF4A5 - rangeMin] = 0x7AF7;
            mapDataStd [rangeNo] [0xF4A6 - rangeMin] = 0x7C49;
            mapDataStd [rangeNo] [0xF4A7 - rangeMin] = 0x7C48;
            mapDataStd [rangeNo] [0xF4A8 - rangeMin] = 0x7C4A;
            mapDataStd [rangeNo] [0xF4A9 - rangeMin] = 0x7C47;
            mapDataStd [rangeNo] [0xF4AA - rangeMin] = 0x7C45;
            mapDataStd [rangeNo] [0xF4AB - rangeMin] = 0x7CEE;
            mapDataStd [rangeNo] [0xF4AC - rangeMin] = 0x7E7B;
            mapDataStd [rangeNo] [0xF4AD - rangeMin] = 0x7E7E;
            mapDataStd [rangeNo] [0xF4AE - rangeMin] = 0x7E81;
            mapDataStd [rangeNo] [0xF4AF - rangeMin] = 0x7E80;

            mapDataStd [rangeNo] [0xF4B0 - rangeMin] = 0x7FBA;
            mapDataStd [rangeNo] [0xF4B1 - rangeMin] = 0x7FFF;
            mapDataStd [rangeNo] [0xF4B2 - rangeMin] = 0x8079;
            mapDataStd [rangeNo] [0xF4B3 - rangeMin] = 0x81DB;
            mapDataStd [rangeNo] [0xF4B4 - rangeMin] = 0x81D9;
            mapDataStd [rangeNo] [0xF4B5 - rangeMin] = 0x820B;
            mapDataStd [rangeNo] [0xF4B6 - rangeMin] = 0x8268;
            mapDataStd [rangeNo] [0xF4B7 - rangeMin] = 0x8269;
            mapDataStd [rangeNo] [0xF4B8 - rangeMin] = 0x8622;
            mapDataStd [rangeNo] [0xF4B9 - rangeMin] = 0x85FF;
            mapDataStd [rangeNo] [0xF4BA - rangeMin] = 0x8601;
            mapDataStd [rangeNo] [0xF4BB - rangeMin] = 0x85FE;
            mapDataStd [rangeNo] [0xF4BC - rangeMin] = 0x861B;
            mapDataStd [rangeNo] [0xF4BD - rangeMin] = 0x8600;
            mapDataStd [rangeNo] [0xF4BE - rangeMin] = 0x85F6;
            mapDataStd [rangeNo] [0xF4BF - rangeMin] = 0x8604;

            mapDataStd [rangeNo] [0xF4C0 - rangeMin] = 0x8609;
            mapDataStd [rangeNo] [0xF4C1 - rangeMin] = 0x8605;
            mapDataStd [rangeNo] [0xF4C2 - rangeMin] = 0x860C;
            mapDataStd [rangeNo] [0xF4C3 - rangeMin] = 0x85FD;
            mapDataStd [rangeNo] [0xF4C4 - rangeMin] = 0x8819;
            mapDataStd [rangeNo] [0xF4C5 - rangeMin] = 0x8810;
            mapDataStd [rangeNo] [0xF4C6 - rangeMin] = 0x8811;
            mapDataStd [rangeNo] [0xF4C7 - rangeMin] = 0x8817;
            mapDataStd [rangeNo] [0xF4C8 - rangeMin] = 0x8813;
            mapDataStd [rangeNo] [0xF4C9 - rangeMin] = 0x8816;
            mapDataStd [rangeNo] [0xF4CA - rangeMin] = 0x8963;
            mapDataStd [rangeNo] [0xF4CB - rangeMin] = 0x8966;
            mapDataStd [rangeNo] [0xF4CC - rangeMin] = 0x89B9;
            mapDataStd [rangeNo] [0xF4CD - rangeMin] = 0x89F7;
            mapDataStd [rangeNo] [0xF4CE - rangeMin] = 0x8B60;
            mapDataStd [rangeNo] [0xF4CF - rangeMin] = 0x8B6A;

            mapDataStd [rangeNo] [0xF4D0 - rangeMin] = 0x8B5D;
            mapDataStd [rangeNo] [0xF4D1 - rangeMin] = 0x8B68;
            mapDataStd [rangeNo] [0xF4D2 - rangeMin] = 0x8B63;
            mapDataStd [rangeNo] [0xF4D3 - rangeMin] = 0x8B65;
            mapDataStd [rangeNo] [0xF4D4 - rangeMin] = 0x8B67;
            mapDataStd [rangeNo] [0xF4D5 - rangeMin] = 0x8B6D;
            mapDataStd [rangeNo] [0xF4D6 - rangeMin] = 0x8DAE;
            mapDataStd [rangeNo] [0xF4D7 - rangeMin] = 0x8E86;
            mapDataStd [rangeNo] [0xF4D8 - rangeMin] = 0x8E88;
            mapDataStd [rangeNo] [0xF4D9 - rangeMin] = 0x8E84;
            mapDataStd [rangeNo] [0xF4DA - rangeMin] = 0x8F59;
            mapDataStd [rangeNo] [0xF4DB - rangeMin] = 0x8F56;
            mapDataStd [rangeNo] [0xF4DC - rangeMin] = 0x8F57;
            mapDataStd [rangeNo] [0xF4DD - rangeMin] = 0x8F55;
            mapDataStd [rangeNo] [0xF4DE - rangeMin] = 0x8F58;
            mapDataStd [rangeNo] [0xF4DF - rangeMin] = 0x8F5A;

            mapDataStd [rangeNo] [0xF4E0 - rangeMin] = 0x908D;
            mapDataStd [rangeNo] [0xF4E1 - rangeMin] = 0x9143;
            mapDataStd [rangeNo] [0xF4E2 - rangeMin] = 0x9141;
            mapDataStd [rangeNo] [0xF4E3 - rangeMin] = 0x91B7;
            mapDataStd [rangeNo] [0xF4E4 - rangeMin] = 0x91B5;
            mapDataStd [rangeNo] [0xF4E5 - rangeMin] = 0x91B2;
            mapDataStd [rangeNo] [0xF4E6 - rangeMin] = 0x91B3;
            mapDataStd [rangeNo] [0xF4E7 - rangeMin] = 0x940B;
            mapDataStd [rangeNo] [0xF4E8 - rangeMin] = 0x9413;
            mapDataStd [rangeNo] [0xF4E9 - rangeMin] = 0x93FB;
            mapDataStd [rangeNo] [0xF4EA - rangeMin] = 0x9420;
            mapDataStd [rangeNo] [0xF4EB - rangeMin] = 0x940F;
            mapDataStd [rangeNo] [0xF4EC - rangeMin] = 0x9414;
            mapDataStd [rangeNo] [0xF4ED - rangeMin] = 0x93FE;
            mapDataStd [rangeNo] [0xF4EE - rangeMin] = 0x9415;
            mapDataStd [rangeNo] [0xF4EF - rangeMin] = 0x9410;

            mapDataStd [rangeNo] [0xF4F0 - rangeMin] = 0x9428;
            mapDataStd [rangeNo] [0xF4F1 - rangeMin] = 0x9419;
            mapDataStd [rangeNo] [0xF4F2 - rangeMin] = 0x940D;
            mapDataStd [rangeNo] [0xF4F3 - rangeMin] = 0x93F5;
            mapDataStd [rangeNo] [0xF4F4 - rangeMin] = 0x9400;
            mapDataStd [rangeNo] [0xF4F5 - rangeMin] = 0x93F7;
            mapDataStd [rangeNo] [0xF4F6 - rangeMin] = 0x9407;
            mapDataStd [rangeNo] [0xF4F7 - rangeMin] = 0x940E;
            mapDataStd [rangeNo] [0xF4F8 - rangeMin] = 0x9416;
            mapDataStd [rangeNo] [0xF4F9 - rangeMin] = 0x9412;
            mapDataStd [rangeNo] [0xF4FA - rangeMin] = 0x93FA;
            mapDataStd [rangeNo] [0xF4FB - rangeMin] = 0x9409;
            mapDataStd [rangeNo] [0xF4FC - rangeMin] = 0x93F8;
            mapDataStd [rangeNo] [0xF4FD - rangeMin] = 0x940A;
            mapDataStd [rangeNo] [0xF4FE - rangeMin] = 0x93FF;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xF540 - rangeMin] = 0x93FC;
            mapDataStd [rangeNo] [0xF541 - rangeMin] = 0x940C;
            mapDataStd [rangeNo] [0xF542 - rangeMin] = 0x93F6;
            mapDataStd [rangeNo] [0xF543 - rangeMin] = 0x9411;
            mapDataStd [rangeNo] [0xF544 - rangeMin] = 0x9406;
            mapDataStd [rangeNo] [0xF545 - rangeMin] = 0x95DE;
            mapDataStd [rangeNo] [0xF546 - rangeMin] = 0x95E0;
            mapDataStd [rangeNo] [0xF547 - rangeMin] = 0x95DF;
            mapDataStd [rangeNo] [0xF548 - rangeMin] = 0x972E;
            mapDataStd [rangeNo] [0xF549 - rangeMin] = 0x972F;
            mapDataStd [rangeNo] [0xF54A - rangeMin] = 0x97B9;
            mapDataStd [rangeNo] [0xF54B - rangeMin] = 0x97BB;
            mapDataStd [rangeNo] [0xF54C - rangeMin] = 0x97FD;
            mapDataStd [rangeNo] [0xF54D - rangeMin] = 0x97FE;
            mapDataStd [rangeNo] [0xF54E - rangeMin] = 0x9860;
            mapDataStd [rangeNo] [0xF54F - rangeMin] = 0x9862;

            mapDataStd [rangeNo] [0xF550 - rangeMin] = 0x9863;
            mapDataStd [rangeNo] [0xF551 - rangeMin] = 0x985F;
            mapDataStd [rangeNo] [0xF552 - rangeMin] = 0x98C1;
            mapDataStd [rangeNo] [0xF553 - rangeMin] = 0x98C2;
            mapDataStd [rangeNo] [0xF554 - rangeMin] = 0x9950;
            mapDataStd [rangeNo] [0xF555 - rangeMin] = 0x994E;
            mapDataStd [rangeNo] [0xF556 - rangeMin] = 0x9959;
            mapDataStd [rangeNo] [0xF557 - rangeMin] = 0x994C;
            mapDataStd [rangeNo] [0xF558 - rangeMin] = 0x994B;
            mapDataStd [rangeNo] [0xF559 - rangeMin] = 0x9953;
            mapDataStd [rangeNo] [0xF55A - rangeMin] = 0x9A32;
            mapDataStd [rangeNo] [0xF55B - rangeMin] = 0x9A34;
            mapDataStd [rangeNo] [0xF55C - rangeMin] = 0x9A31;
            mapDataStd [rangeNo] [0xF55D - rangeMin] = 0x9A2C;
            mapDataStd [rangeNo] [0xF55E - rangeMin] = 0x9A2A;
            mapDataStd [rangeNo] [0xF55F - rangeMin] = 0x9A36;

            mapDataStd [rangeNo] [0xF560 - rangeMin] = 0x9A29;
            mapDataStd [rangeNo] [0xF561 - rangeMin] = 0x9A2E;
            mapDataStd [rangeNo] [0xF562 - rangeMin] = 0x9A38;
            mapDataStd [rangeNo] [0xF563 - rangeMin] = 0x9A2D;
            mapDataStd [rangeNo] [0xF564 - rangeMin] = 0x9AC7;
            mapDataStd [rangeNo] [0xF565 - rangeMin] = 0x9ACA;
            mapDataStd [rangeNo] [0xF566 - rangeMin] = 0x9AC6;
            mapDataStd [rangeNo] [0xF567 - rangeMin] = 0x9B10;
            mapDataStd [rangeNo] [0xF568 - rangeMin] = 0x9B12;
            mapDataStd [rangeNo] [0xF569 - rangeMin] = 0x9B11;
            mapDataStd [rangeNo] [0xF56A - rangeMin] = 0x9C0B;
            mapDataStd [rangeNo] [0xF56B - rangeMin] = 0x9C08;
            mapDataStd [rangeNo] [0xF56C - rangeMin] = 0x9BF7;
            mapDataStd [rangeNo] [0xF56D - rangeMin] = 0x9C05;
            mapDataStd [rangeNo] [0xF56E - rangeMin] = 0x9C12;
            mapDataStd [rangeNo] [0xF56F - rangeMin] = 0x9BF8;

            mapDataStd [rangeNo] [0xF570 - rangeMin] = 0x9C40;
            mapDataStd [rangeNo] [0xF571 - rangeMin] = 0x9C07;
            mapDataStd [rangeNo] [0xF572 - rangeMin] = 0x9C0E;
            mapDataStd [rangeNo] [0xF573 - rangeMin] = 0x9C06;
            mapDataStd [rangeNo] [0xF574 - rangeMin] = 0x9C17;
            mapDataStd [rangeNo] [0xF575 - rangeMin] = 0x9C14;
            mapDataStd [rangeNo] [0xF576 - rangeMin] = 0x9C09;
            mapDataStd [rangeNo] [0xF577 - rangeMin] = 0x9D9F;
            mapDataStd [rangeNo] [0xF578 - rangeMin] = 0x9D99;
            mapDataStd [rangeNo] [0xF579 - rangeMin] = 0x9DA4;
            mapDataStd [rangeNo] [0xF57A - rangeMin] = 0x9D9D;
            mapDataStd [rangeNo] [0xF57B - rangeMin] = 0x9D92;
            mapDataStd [rangeNo] [0xF57C - rangeMin] = 0x9D98;
            mapDataStd [rangeNo] [0xF57D - rangeMin] = 0x9D90;
            mapDataStd [rangeNo] [0xF57E - rangeMin] = 0x9D9B;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xF5A1 - rangeMin] = 0x9DA0;
            mapDataStd [rangeNo] [0xF5A2 - rangeMin] = 0x9D94;
            mapDataStd [rangeNo] [0xF5A3 - rangeMin] = 0x9D9C;
            mapDataStd [rangeNo] [0xF5A4 - rangeMin] = 0x9DAA;
            mapDataStd [rangeNo] [0xF5A5 - rangeMin] = 0x9D97;
            mapDataStd [rangeNo] [0xF5A6 - rangeMin] = 0x9DA1;
            mapDataStd [rangeNo] [0xF5A7 - rangeMin] = 0x9D9A;
            mapDataStd [rangeNo] [0xF5A8 - rangeMin] = 0x9DA2;
            mapDataStd [rangeNo] [0xF5A9 - rangeMin] = 0x9DA8;
            mapDataStd [rangeNo] [0xF5AA - rangeMin] = 0x9D9E;
            mapDataStd [rangeNo] [0xF5AB - rangeMin] = 0x9DA3;
            mapDataStd [rangeNo] [0xF5AC - rangeMin] = 0x9DBF;
            mapDataStd [rangeNo] [0xF5AD - rangeMin] = 0x9DA9;
            mapDataStd [rangeNo] [0xF5AE - rangeMin] = 0x9D96;
            mapDataStd [rangeNo] [0xF5AF - rangeMin] = 0x9DA6;

            mapDataStd [rangeNo] [0xF5B0 - rangeMin] = 0x9DA7;
            mapDataStd [rangeNo] [0xF5B1 - rangeMin] = 0x9E99;
            mapDataStd [rangeNo] [0xF5B2 - rangeMin] = 0x9E9B;
            mapDataStd [rangeNo] [0xF5B3 - rangeMin] = 0x9E9A;
            mapDataStd [rangeNo] [0xF5B4 - rangeMin] = 0x9EE5;
            mapDataStd [rangeNo] [0xF5B5 - rangeMin] = 0x9EE4;
            mapDataStd [rangeNo] [0xF5B6 - rangeMin] = 0x9EE7;
            mapDataStd [rangeNo] [0xF5B7 - rangeMin] = 0x9EE6;
            mapDataStd [rangeNo] [0xF5B8 - rangeMin] = 0x9F30;
            mapDataStd [rangeNo] [0xF5B9 - rangeMin] = 0x9F2E;
            mapDataStd [rangeNo] [0xF5BA - rangeMin] = 0x9F5B;
            mapDataStd [rangeNo] [0xF5BB - rangeMin] = 0x9F60;
            mapDataStd [rangeNo] [0xF5BC - rangeMin] = 0x9F5E;
            mapDataStd [rangeNo] [0xF5BD - rangeMin] = 0x9F5D;
            mapDataStd [rangeNo] [0xF5BE - rangeMin] = 0x9F59;
            mapDataStd [rangeNo] [0xF5BF - rangeMin] = 0x9F91;

            mapDataStd [rangeNo] [0xF5C0 - rangeMin] = 0x513A;
            mapDataStd [rangeNo] [0xF5C1 - rangeMin] = 0x5139;
            mapDataStd [rangeNo] [0xF5C2 - rangeMin] = 0x5298;
            mapDataStd [rangeNo] [0xF5C3 - rangeMin] = 0x5297;
            mapDataStd [rangeNo] [0xF5C4 - rangeMin] = 0x56C3;
            mapDataStd [rangeNo] [0xF5C5 - rangeMin] = 0x56BD;
            mapDataStd [rangeNo] [0xF5C6 - rangeMin] = 0x56BE;
            mapDataStd [rangeNo] [0xF5C7 - rangeMin] = 0x5B48;
            mapDataStd [rangeNo] [0xF5C8 - rangeMin] = 0x5B47;
            mapDataStd [rangeNo] [0xF5C9 - rangeMin] = 0x5DCB;
            mapDataStd [rangeNo] [0xF5CA - rangeMin] = 0x5DCF;
            mapDataStd [rangeNo] [0xF5CB - rangeMin] = 0x5EF1;
            mapDataStd [rangeNo] [0xF5CC - rangeMin] = 0x61FD;
            mapDataStd [rangeNo] [0xF5CD - rangeMin] = 0x651B;
            mapDataStd [rangeNo] [0xF5CE - rangeMin] = 0x6B02;
            mapDataStd [rangeNo] [0xF5CF - rangeMin] = 0x6AFC;

            mapDataStd [rangeNo] [0xF5D0 - rangeMin] = 0x6B03;
            mapDataStd [rangeNo] [0xF5D1 - rangeMin] = 0x6AF8;
            mapDataStd [rangeNo] [0xF5D2 - rangeMin] = 0x6B00;
            mapDataStd [rangeNo] [0xF5D3 - rangeMin] = 0x7043;
            mapDataStd [rangeNo] [0xF5D4 - rangeMin] = 0x7044;
            mapDataStd [rangeNo] [0xF5D5 - rangeMin] = 0x704A;
            mapDataStd [rangeNo] [0xF5D6 - rangeMin] = 0x7048;
            mapDataStd [rangeNo] [0xF5D7 - rangeMin] = 0x7049;
            mapDataStd [rangeNo] [0xF5D8 - rangeMin] = 0x7045;
            mapDataStd [rangeNo] [0xF5D9 - rangeMin] = 0x7046;
            mapDataStd [rangeNo] [0xF5DA - rangeMin] = 0x721D;
            mapDataStd [rangeNo] [0xF5DB - rangeMin] = 0x721A;
            mapDataStd [rangeNo] [0xF5DC - rangeMin] = 0x7219;
            mapDataStd [rangeNo] [0xF5DD - rangeMin] = 0x737E;
            mapDataStd [rangeNo] [0xF5DE - rangeMin] = 0x7517;
            mapDataStd [rangeNo] [0xF5DF - rangeMin] = 0x766A;

            mapDataStd [rangeNo] [0xF5E0 - rangeMin] = 0x77D0;
            mapDataStd [rangeNo] [0xF5E1 - rangeMin] = 0x792D;
            mapDataStd [rangeNo] [0xF5E2 - rangeMin] = 0x7931;
            mapDataStd [rangeNo] [0xF5E3 - rangeMin] = 0x792F;
            mapDataStd [rangeNo] [0xF5E4 - rangeMin] = 0x7C54;
            mapDataStd [rangeNo] [0xF5E5 - rangeMin] = 0x7C53;
            mapDataStd [rangeNo] [0xF5E6 - rangeMin] = 0x7CF2;
            mapDataStd [rangeNo] [0xF5E7 - rangeMin] = 0x7E8A;
            mapDataStd [rangeNo] [0xF5E8 - rangeMin] = 0x7E87;
            mapDataStd [rangeNo] [0xF5E9 - rangeMin] = 0x7E88;
            mapDataStd [rangeNo] [0xF5EA - rangeMin] = 0x7E8B;
            mapDataStd [rangeNo] [0xF5EB - rangeMin] = 0x7E86;
            mapDataStd [rangeNo] [0xF5EC - rangeMin] = 0x7E8D;
            mapDataStd [rangeNo] [0xF5ED - rangeMin] = 0x7F4D;
            mapDataStd [rangeNo] [0xF5EE - rangeMin] = 0x7FBB;
            mapDataStd [rangeNo] [0xF5EF - rangeMin] = 0x8030;

            mapDataStd [rangeNo] [0xF5F0 - rangeMin] = 0x81DD;
            mapDataStd [rangeNo] [0xF5F1 - rangeMin] = 0x8618;
            mapDataStd [rangeNo] [0xF5F2 - rangeMin] = 0x862A;
            mapDataStd [rangeNo] [0xF5F3 - rangeMin] = 0x8626;
            mapDataStd [rangeNo] [0xF5F4 - rangeMin] = 0x861F;
            mapDataStd [rangeNo] [0xF5F5 - rangeMin] = 0x8623;
            mapDataStd [rangeNo] [0xF5F6 - rangeMin] = 0x861C;
            mapDataStd [rangeNo] [0xF5F7 - rangeMin] = 0x8619;
            mapDataStd [rangeNo] [0xF5F8 - rangeMin] = 0x8627;
            mapDataStd [rangeNo] [0xF5F9 - rangeMin] = 0x862E;
            mapDataStd [rangeNo] [0xF5FA - rangeMin] = 0x8621;
            mapDataStd [rangeNo] [0xF5FB - rangeMin] = 0x8620;
            mapDataStd [rangeNo] [0xF5FC - rangeMin] = 0x8629;
            mapDataStd [rangeNo] [0xF5FD - rangeMin] = 0x861E;
            mapDataStd [rangeNo] [0xF5FE - rangeMin] = 0x8625;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xF640 - rangeMin] = 0x8829;
            mapDataStd [rangeNo] [0xF641 - rangeMin] = 0x881D;
            mapDataStd [rangeNo] [0xF642 - rangeMin] = 0x881B;
            mapDataStd [rangeNo] [0xF643 - rangeMin] = 0x8820;
            mapDataStd [rangeNo] [0xF644 - rangeMin] = 0x8824;
            mapDataStd [rangeNo] [0xF645 - rangeMin] = 0x881C;
            mapDataStd [rangeNo] [0xF646 - rangeMin] = 0x882B;
            mapDataStd [rangeNo] [0xF647 - rangeMin] = 0x884A;
            mapDataStd [rangeNo] [0xF648 - rangeMin] = 0x896D;
            mapDataStd [rangeNo] [0xF649 - rangeMin] = 0x8969;
            mapDataStd [rangeNo] [0xF64A - rangeMin] = 0x896E;
            mapDataStd [rangeNo] [0xF64B - rangeMin] = 0x896B;
            mapDataStd [rangeNo] [0xF64C - rangeMin] = 0x89FA;
            mapDataStd [rangeNo] [0xF64D - rangeMin] = 0x8B79;
            mapDataStd [rangeNo] [0xF64E - rangeMin] = 0x8B78;
            mapDataStd [rangeNo] [0xF64F - rangeMin] = 0x8B45;

            mapDataStd [rangeNo] [0xF650 - rangeMin] = 0x8B7A;
            mapDataStd [rangeNo] [0xF651 - rangeMin] = 0x8B7B;
            mapDataStd [rangeNo] [0xF652 - rangeMin] = 0x8D10;
            mapDataStd [rangeNo] [0xF653 - rangeMin] = 0x8D14;
            mapDataStd [rangeNo] [0xF654 - rangeMin] = 0x8DAF;
            mapDataStd [rangeNo] [0xF655 - rangeMin] = 0x8E8E;
            mapDataStd [rangeNo] [0xF656 - rangeMin] = 0x8E8C;
            mapDataStd [rangeNo] [0xF657 - rangeMin] = 0x8F5E;
            mapDataStd [rangeNo] [0xF658 - rangeMin] = 0x8F5B;
            mapDataStd [rangeNo] [0xF659 - rangeMin] = 0x8F5D;
            mapDataStd [rangeNo] [0xF65A - rangeMin] = 0x9146;
            mapDataStd [rangeNo] [0xF65B - rangeMin] = 0x9144;
            mapDataStd [rangeNo] [0xF65C - rangeMin] = 0x9145;
            mapDataStd [rangeNo] [0xF65D - rangeMin] = 0x91B9;
            mapDataStd [rangeNo] [0xF65E - rangeMin] = 0x943F;
            mapDataStd [rangeNo] [0xF65F - rangeMin] = 0x943B;

            mapDataStd [rangeNo] [0xF660 - rangeMin] = 0x9436;
            mapDataStd [rangeNo] [0xF661 - rangeMin] = 0x9429;
            mapDataStd [rangeNo] [0xF662 - rangeMin] = 0x943D;
            mapDataStd [rangeNo] [0xF663 - rangeMin] = 0x943C;
            mapDataStd [rangeNo] [0xF664 - rangeMin] = 0x9430;
            mapDataStd [rangeNo] [0xF665 - rangeMin] = 0x9439;
            mapDataStd [rangeNo] [0xF666 - rangeMin] = 0x942A;
            mapDataStd [rangeNo] [0xF667 - rangeMin] = 0x9437;
            mapDataStd [rangeNo] [0xF668 - rangeMin] = 0x942C;
            mapDataStd [rangeNo] [0xF669 - rangeMin] = 0x9440;
            mapDataStd [rangeNo] [0xF66A - rangeMin] = 0x9431;
            mapDataStd [rangeNo] [0xF66B - rangeMin] = 0x95E5;
            mapDataStd [rangeNo] [0xF66C - rangeMin] = 0x95E4;
            mapDataStd [rangeNo] [0xF66D - rangeMin] = 0x95E3;
            mapDataStd [rangeNo] [0xF66E - rangeMin] = 0x9735;
            mapDataStd [rangeNo] [0xF66F - rangeMin] = 0x973A;

            mapDataStd [rangeNo] [0xF670 - rangeMin] = 0x97BF;
            mapDataStd [rangeNo] [0xF671 - rangeMin] = 0x97E1;
            mapDataStd [rangeNo] [0xF672 - rangeMin] = 0x9864;
            mapDataStd [rangeNo] [0xF673 - rangeMin] = 0x98C9;
            mapDataStd [rangeNo] [0xF674 - rangeMin] = 0x98C6;
            mapDataStd [rangeNo] [0xF675 - rangeMin] = 0x98C0;
            mapDataStd [rangeNo] [0xF676 - rangeMin] = 0x9958;
            mapDataStd [rangeNo] [0xF677 - rangeMin] = 0x9956;
            mapDataStd [rangeNo] [0xF678 - rangeMin] = 0x9A39;
            mapDataStd [rangeNo] [0xF679 - rangeMin] = 0x9A3D;
            mapDataStd [rangeNo] [0xF67A - rangeMin] = 0x9A46;
            mapDataStd [rangeNo] [0xF67B - rangeMin] = 0x9A44;
            mapDataStd [rangeNo] [0xF67C - rangeMin] = 0x9A42;
            mapDataStd [rangeNo] [0xF67D - rangeMin] = 0x9A41;
            mapDataStd [rangeNo] [0xF67E - rangeMin] = 0x9A3A;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xF6A1 - rangeMin] = 0x9A3F;
            mapDataStd [rangeNo] [0xF6A2 - rangeMin] = 0x9ACD;
            mapDataStd [rangeNo] [0xF6A3 - rangeMin] = 0x9B15;
            mapDataStd [rangeNo] [0xF6A4 - rangeMin] = 0x9B17;
            mapDataStd [rangeNo] [0xF6A5 - rangeMin] = 0x9B18;
            mapDataStd [rangeNo] [0xF6A6 - rangeMin] = 0x9B16;
            mapDataStd [rangeNo] [0xF6A7 - rangeMin] = 0x9B3A;
            mapDataStd [rangeNo] [0xF6A8 - rangeMin] = 0x9B52;
            mapDataStd [rangeNo] [0xF6A9 - rangeMin] = 0x9C2B;
            mapDataStd [rangeNo] [0xF6AA - rangeMin] = 0x9C1D;
            mapDataStd [rangeNo] [0xF6AB - rangeMin] = 0x9C1C;
            mapDataStd [rangeNo] [0xF6AC - rangeMin] = 0x9C2C;
            mapDataStd [rangeNo] [0xF6AD - rangeMin] = 0x9C23;
            mapDataStd [rangeNo] [0xF6AE - rangeMin] = 0x9C28;
            mapDataStd [rangeNo] [0xF6AF - rangeMin] = 0x9C29;

            mapDataStd [rangeNo] [0xF6B0 - rangeMin] = 0x9C24;
            mapDataStd [rangeNo] [0xF6B1 - rangeMin] = 0x9C21;
            mapDataStd [rangeNo] [0xF6B2 - rangeMin] = 0x9DB7;
            mapDataStd [rangeNo] [0xF6B3 - rangeMin] = 0x9DB6;
            mapDataStd [rangeNo] [0xF6B4 - rangeMin] = 0x9DBC;
            mapDataStd [rangeNo] [0xF6B5 - rangeMin] = 0x9DC1;
            mapDataStd [rangeNo] [0xF6B6 - rangeMin] = 0x9DC7;
            mapDataStd [rangeNo] [0xF6B7 - rangeMin] = 0x9DCA;
            mapDataStd [rangeNo] [0xF6B8 - rangeMin] = 0x9DCF;
            mapDataStd [rangeNo] [0xF6B9 - rangeMin] = 0x9DBE;
            mapDataStd [rangeNo] [0xF6BA - rangeMin] = 0x9DC5;
            mapDataStd [rangeNo] [0xF6BB - rangeMin] = 0x9DC3;
            mapDataStd [rangeNo] [0xF6BC - rangeMin] = 0x9DBB;
            mapDataStd [rangeNo] [0xF6BD - rangeMin] = 0x9DB5;
            mapDataStd [rangeNo] [0xF6BE - rangeMin] = 0x9DCE;
            mapDataStd [rangeNo] [0xF6BF - rangeMin] = 0x9DB9;

            mapDataStd [rangeNo] [0xF6C0 - rangeMin] = 0x9DBA;
            mapDataStd [rangeNo] [0xF6C1 - rangeMin] = 0x9DAC;
            mapDataStd [rangeNo] [0xF6C2 - rangeMin] = 0x9DC8;
            mapDataStd [rangeNo] [0xF6C3 - rangeMin] = 0x9DB1;
            mapDataStd [rangeNo] [0xF6C4 - rangeMin] = 0x9DAD;
            mapDataStd [rangeNo] [0xF6C5 - rangeMin] = 0x9DCC;
            mapDataStd [rangeNo] [0xF6C6 - rangeMin] = 0x9DB3;
            mapDataStd [rangeNo] [0xF6C7 - rangeMin] = 0x9DCD;
            mapDataStd [rangeNo] [0xF6C8 - rangeMin] = 0x9DB2;
            mapDataStd [rangeNo] [0xF6C9 - rangeMin] = 0x9E7A;
            mapDataStd [rangeNo] [0xF6CA - rangeMin] = 0x9E9C;
            mapDataStd [rangeNo] [0xF6CB - rangeMin] = 0x9EEB;
            mapDataStd [rangeNo] [0xF6CC - rangeMin] = 0x9EEE;
            mapDataStd [rangeNo] [0xF6CD - rangeMin] = 0x9EED;
            mapDataStd [rangeNo] [0xF6CE - rangeMin] = 0x9F1B;
            mapDataStd [rangeNo] [0xF6CF - rangeMin] = 0x9F18;

            mapDataStd [rangeNo] [0xF6D0 - rangeMin] = 0x9F1A;
            mapDataStd [rangeNo] [0xF6D1 - rangeMin] = 0x9F31;
            mapDataStd [rangeNo] [0xF6D2 - rangeMin] = 0x9F4E;
            mapDataStd [rangeNo] [0xF6D3 - rangeMin] = 0x9F65;
            mapDataStd [rangeNo] [0xF6D4 - rangeMin] = 0x9F64;
            mapDataStd [rangeNo] [0xF6D5 - rangeMin] = 0x9F92;
            mapDataStd [rangeNo] [0xF6D6 - rangeMin] = 0x4EB9;
            mapDataStd [rangeNo] [0xF6D7 - rangeMin] = 0x56C6;
            mapDataStd [rangeNo] [0xF6D8 - rangeMin] = 0x56C5;
            mapDataStd [rangeNo] [0xF6D9 - rangeMin] = 0x56CB;
            mapDataStd [rangeNo] [0xF6DA - rangeMin] = 0x5971;
            mapDataStd [rangeNo] [0xF6DB - rangeMin] = 0x5B4B;
            mapDataStd [rangeNo] [0xF6DC - rangeMin] = 0x5B4C;
            mapDataStd [rangeNo] [0xF6DD - rangeMin] = 0x5DD5;
            mapDataStd [rangeNo] [0xF6DE - rangeMin] = 0x5DD1;
            mapDataStd [rangeNo] [0xF6DF - rangeMin] = 0x5EF2;

            mapDataStd [rangeNo] [0xF6E0 - rangeMin] = 0x6521;
            mapDataStd [rangeNo] [0xF6E1 - rangeMin] = 0x6520;
            mapDataStd [rangeNo] [0xF6E2 - rangeMin] = 0x6526;
            mapDataStd [rangeNo] [0xF6E3 - rangeMin] = 0x6522;
            mapDataStd [rangeNo] [0xF6E4 - rangeMin] = 0x6B0B;
            mapDataStd [rangeNo] [0xF6E5 - rangeMin] = 0x6B08;
            mapDataStd [rangeNo] [0xF6E6 - rangeMin] = 0x6B09;
            mapDataStd [rangeNo] [0xF6E7 - rangeMin] = 0x6C0D;
            mapDataStd [rangeNo] [0xF6E8 - rangeMin] = 0x7055;
            mapDataStd [rangeNo] [0xF6E9 - rangeMin] = 0x7056;
            mapDataStd [rangeNo] [0xF6EA - rangeMin] = 0x7057;
            mapDataStd [rangeNo] [0xF6EB - rangeMin] = 0x7052;
            mapDataStd [rangeNo] [0xF6EC - rangeMin] = 0x721E;
            mapDataStd [rangeNo] [0xF6ED - rangeMin] = 0x721F;
            mapDataStd [rangeNo] [0xF6EE - rangeMin] = 0x72A9;
            mapDataStd [rangeNo] [0xF6EF - rangeMin] = 0x737F;

            mapDataStd [rangeNo] [0xF6F0 - rangeMin] = 0x74D8;
            mapDataStd [rangeNo] [0xF6F1 - rangeMin] = 0x74D5;
            mapDataStd [rangeNo] [0xF6F2 - rangeMin] = 0x74D9;
            mapDataStd [rangeNo] [0xF6F3 - rangeMin] = 0x74D7;
            mapDataStd [rangeNo] [0xF6F4 - rangeMin] = 0x766D;
            mapDataStd [rangeNo] [0xF6F5 - rangeMin] = 0x76AD;
            mapDataStd [rangeNo] [0xF6F6 - rangeMin] = 0x7935;
            mapDataStd [rangeNo] [0xF6F7 - rangeMin] = 0x79B4;
            mapDataStd [rangeNo] [0xF6F8 - rangeMin] = 0x7A70;
            mapDataStd [rangeNo] [0xF6F9 - rangeMin] = 0x7A71;
            mapDataStd [rangeNo] [0xF6FA - rangeMin] = 0x7C57;
            mapDataStd [rangeNo] [0xF6FB - rangeMin] = 0x7C5C;
            mapDataStd [rangeNo] [0xF6FC - rangeMin] = 0x7C59;
            mapDataStd [rangeNo] [0xF6FD - rangeMin] = 0x7C5B;
            mapDataStd [rangeNo] [0xF6FE - rangeMin] = 0x7C5A;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xF740 - rangeMin] = 0x7CF4;
            mapDataStd [rangeNo] [0xF741 - rangeMin] = 0x7CF1;
            mapDataStd [rangeNo] [0xF742 - rangeMin] = 0x7E91;
            mapDataStd [rangeNo] [0xF743 - rangeMin] = 0x7F4F;
            mapDataStd [rangeNo] [0xF744 - rangeMin] = 0x7F87;
            mapDataStd [rangeNo] [0xF745 - rangeMin] = 0x81DE;
            mapDataStd [rangeNo] [0xF746 - rangeMin] = 0x826B;
            mapDataStd [rangeNo] [0xF747 - rangeMin] = 0x8634;
            mapDataStd [rangeNo] [0xF748 - rangeMin] = 0x8635;
            mapDataStd [rangeNo] [0xF749 - rangeMin] = 0x8633;
            mapDataStd [rangeNo] [0xF74A - rangeMin] = 0x862C;
            mapDataStd [rangeNo] [0xF74B - rangeMin] = 0x8632;
            mapDataStd [rangeNo] [0xF74C - rangeMin] = 0x8636;
            mapDataStd [rangeNo] [0xF74D - rangeMin] = 0x882C;
            mapDataStd [rangeNo] [0xF74E - rangeMin] = 0x8828;
            mapDataStd [rangeNo] [0xF74F - rangeMin] = 0x8826;

            mapDataStd [rangeNo] [0xF750 - rangeMin] = 0x882A;
            mapDataStd [rangeNo] [0xF751 - rangeMin] = 0x8825;
            mapDataStd [rangeNo] [0xF752 - rangeMin] = 0x8971;
            mapDataStd [rangeNo] [0xF753 - rangeMin] = 0x89BF;
            mapDataStd [rangeNo] [0xF754 - rangeMin] = 0x89BE;
            mapDataStd [rangeNo] [0xF755 - rangeMin] = 0x89FB;
            mapDataStd [rangeNo] [0xF756 - rangeMin] = 0x8B7E;
            mapDataStd [rangeNo] [0xF757 - rangeMin] = 0x8B84;
            mapDataStd [rangeNo] [0xF758 - rangeMin] = 0x8B82;
            mapDataStd [rangeNo] [0xF759 - rangeMin] = 0x8B86;
            mapDataStd [rangeNo] [0xF75A - rangeMin] = 0x8B85;
            mapDataStd [rangeNo] [0xF75B - rangeMin] = 0x8B7F;
            mapDataStd [rangeNo] [0xF75C - rangeMin] = 0x8D15;
            mapDataStd [rangeNo] [0xF75D - rangeMin] = 0x8E95;
            mapDataStd [rangeNo] [0xF75E - rangeMin] = 0x8E94;
            mapDataStd [rangeNo] [0xF75F - rangeMin] = 0x8E9A;

            mapDataStd [rangeNo] [0xF760 - rangeMin] = 0x8E92;
            mapDataStd [rangeNo] [0xF761 - rangeMin] = 0x8E90;
            mapDataStd [rangeNo] [0xF762 - rangeMin] = 0x8E96;
            mapDataStd [rangeNo] [0xF763 - rangeMin] = 0x8E97;
            mapDataStd [rangeNo] [0xF764 - rangeMin] = 0x8F60;
            mapDataStd [rangeNo] [0xF765 - rangeMin] = 0x8F62;
            mapDataStd [rangeNo] [0xF766 - rangeMin] = 0x9147;
            mapDataStd [rangeNo] [0xF767 - rangeMin] = 0x944C;
            mapDataStd [rangeNo] [0xF768 - rangeMin] = 0x9450;
            mapDataStd [rangeNo] [0xF769 - rangeMin] = 0x944A;
            mapDataStd [rangeNo] [0xF76A - rangeMin] = 0x944B;
            mapDataStd [rangeNo] [0xF76B - rangeMin] = 0x944F;
            mapDataStd [rangeNo] [0xF76C - rangeMin] = 0x9447;
            mapDataStd [rangeNo] [0xF76D - rangeMin] = 0x9445;
            mapDataStd [rangeNo] [0xF76E - rangeMin] = 0x9448;
            mapDataStd [rangeNo] [0xF76F - rangeMin] = 0x9449;

            mapDataStd [rangeNo] [0xF770 - rangeMin] = 0x9446;
            mapDataStd [rangeNo] [0xF771 - rangeMin] = 0x973F;
            mapDataStd [rangeNo] [0xF772 - rangeMin] = 0x97E3;
            mapDataStd [rangeNo] [0xF773 - rangeMin] = 0x986A;
            mapDataStd [rangeNo] [0xF774 - rangeMin] = 0x9869;
            mapDataStd [rangeNo] [0xF775 - rangeMin] = 0x98CB;
            mapDataStd [rangeNo] [0xF776 - rangeMin] = 0x9954;
            mapDataStd [rangeNo] [0xF777 - rangeMin] = 0x995B;
            mapDataStd [rangeNo] [0xF778 - rangeMin] = 0x9A4E;
            mapDataStd [rangeNo] [0xF779 - rangeMin] = 0x9A53;
            mapDataStd [rangeNo] [0xF77A - rangeMin] = 0x9A54;
            mapDataStd [rangeNo] [0xF77B - rangeMin] = 0x9A4C;
            mapDataStd [rangeNo] [0xF77C - rangeMin] = 0x9A4F;
            mapDataStd [rangeNo] [0xF77D - rangeMin] = 0x9A48;
            mapDataStd [rangeNo] [0xF77E - rangeMin] = 0x9A4A;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xF7A1 - rangeMin] = 0x9A49;
            mapDataStd [rangeNo] [0xF7A2 - rangeMin] = 0x9A52;
            mapDataStd [rangeNo] [0xF7A3 - rangeMin] = 0x9A50;
            mapDataStd [rangeNo] [0xF7A4 - rangeMin] = 0x9AD0;
            mapDataStd [rangeNo] [0xF7A5 - rangeMin] = 0x9B19;
            mapDataStd [rangeNo] [0xF7A6 - rangeMin] = 0x9B2B;
            mapDataStd [rangeNo] [0xF7A7 - rangeMin] = 0x9B3B;
            mapDataStd [rangeNo] [0xF7A8 - rangeMin] = 0x9B56;
            mapDataStd [rangeNo] [0xF7A9 - rangeMin] = 0x9B55;
            mapDataStd [rangeNo] [0xF7AA - rangeMin] = 0x9C46;
            mapDataStd [rangeNo] [0xF7AB - rangeMin] = 0x9C48;
            mapDataStd [rangeNo] [0xF7AC - rangeMin] = 0x9C3F;
            mapDataStd [rangeNo] [0xF7AD - rangeMin] = 0x9C44;
            mapDataStd [rangeNo] [0xF7AE - rangeMin] = 0x9C39;
            mapDataStd [rangeNo] [0xF7AF - rangeMin] = 0x9C33;

            mapDataStd [rangeNo] [0xF7B0 - rangeMin] = 0x9C41;
            mapDataStd [rangeNo] [0xF7B1 - rangeMin] = 0x9C3C;
            mapDataStd [rangeNo] [0xF7B2 - rangeMin] = 0x9C37;
            mapDataStd [rangeNo] [0xF7B3 - rangeMin] = 0x9C34;
            mapDataStd [rangeNo] [0xF7B4 - rangeMin] = 0x9C32;
            mapDataStd [rangeNo] [0xF7B5 - rangeMin] = 0x9C3D;
            mapDataStd [rangeNo] [0xF7B6 - rangeMin] = 0x9C36;
            mapDataStd [rangeNo] [0xF7B7 - rangeMin] = 0x9DDB;
            mapDataStd [rangeNo] [0xF7B8 - rangeMin] = 0x9DD2;
            mapDataStd [rangeNo] [0xF7B9 - rangeMin] = 0x9DDE;
            mapDataStd [rangeNo] [0xF7BA - rangeMin] = 0x9DDA;
            mapDataStd [rangeNo] [0xF7BB - rangeMin] = 0x9DCB;
            mapDataStd [rangeNo] [0xF7BC - rangeMin] = 0x9DD0;
            mapDataStd [rangeNo] [0xF7BD - rangeMin] = 0x9DDC;
            mapDataStd [rangeNo] [0xF7BE - rangeMin] = 0x9DD1;
            mapDataStd [rangeNo] [0xF7BF - rangeMin] = 0x9DDF;

            mapDataStd [rangeNo] [0xF7C0 - rangeMin] = 0x9DE9;
            mapDataStd [rangeNo] [0xF7C1 - rangeMin] = 0x9DD9;
            mapDataStd [rangeNo] [0xF7C2 - rangeMin] = 0x9DD8;
            mapDataStd [rangeNo] [0xF7C3 - rangeMin] = 0x9DD6;
            mapDataStd [rangeNo] [0xF7C4 - rangeMin] = 0x9DF5;
            mapDataStd [rangeNo] [0xF7C5 - rangeMin] = 0x9DD5;
            mapDataStd [rangeNo] [0xF7C6 - rangeMin] = 0x9DDD;
            mapDataStd [rangeNo] [0xF7C7 - rangeMin] = 0x9EB6;
            mapDataStd [rangeNo] [0xF7C8 - rangeMin] = 0x9EF0;
            mapDataStd [rangeNo] [0xF7C9 - rangeMin] = 0x9F35;
            mapDataStd [rangeNo] [0xF7CA - rangeMin] = 0x9F33;
            mapDataStd [rangeNo] [0xF7CB - rangeMin] = 0x9F32;
            mapDataStd [rangeNo] [0xF7CC - rangeMin] = 0x9F42;
            mapDataStd [rangeNo] [0xF7CD - rangeMin] = 0x9F6B;
            mapDataStd [rangeNo] [0xF7CE - rangeMin] = 0x9F95;
            mapDataStd [rangeNo] [0xF7CF - rangeMin] = 0x9FA2;

            mapDataStd [rangeNo] [0xF7D0 - rangeMin] = 0x513D;
            mapDataStd [rangeNo] [0xF7D1 - rangeMin] = 0x5299;
            mapDataStd [rangeNo] [0xF7D2 - rangeMin] = 0x58E8;
            mapDataStd [rangeNo] [0xF7D3 - rangeMin] = 0x58E7;
            mapDataStd [rangeNo] [0xF7D4 - rangeMin] = 0x5972;
            mapDataStd [rangeNo] [0xF7D5 - rangeMin] = 0x5B4D;
            mapDataStd [rangeNo] [0xF7D6 - rangeMin] = 0x5DD8;
            mapDataStd [rangeNo] [0xF7D7 - rangeMin] = 0x882F;
            mapDataStd [rangeNo] [0xF7D8 - rangeMin] = 0x5F4F;
            mapDataStd [rangeNo] [0xF7D9 - rangeMin] = 0x6201;
            mapDataStd [rangeNo] [0xF7DA - rangeMin] = 0x6203;
            mapDataStd [rangeNo] [0xF7DB - rangeMin] = 0x6204;
            mapDataStd [rangeNo] [0xF7DC - rangeMin] = 0x6529;
            mapDataStd [rangeNo] [0xF7DD - rangeMin] = 0x6525;
            mapDataStd [rangeNo] [0xF7DE - rangeMin] = 0x6596;
            mapDataStd [rangeNo] [0xF7DF - rangeMin] = 0x66EB;

            mapDataStd [rangeNo] [0xF7E0 - rangeMin] = 0x6B11;
            mapDataStd [rangeNo] [0xF7E1 - rangeMin] = 0x6B12;
            mapDataStd [rangeNo] [0xF7E2 - rangeMin] = 0x6B0F;
            mapDataStd [rangeNo] [0xF7E3 - rangeMin] = 0x6BCA;
            mapDataStd [rangeNo] [0xF7E4 - rangeMin] = 0x705B;
            mapDataStd [rangeNo] [0xF7E5 - rangeMin] = 0x705A;
            mapDataStd [rangeNo] [0xF7E6 - rangeMin] = 0x7222;
            mapDataStd [rangeNo] [0xF7E7 - rangeMin] = 0x7382;
            mapDataStd [rangeNo] [0xF7E8 - rangeMin] = 0x7381;
            mapDataStd [rangeNo] [0xF7E9 - rangeMin] = 0x7383;
            mapDataStd [rangeNo] [0xF7EA - rangeMin] = 0x7670;
            mapDataStd [rangeNo] [0xF7EB - rangeMin] = 0x77D4;
            mapDataStd [rangeNo] [0xF7EC - rangeMin] = 0x7C67;
            mapDataStd [rangeNo] [0xF7ED - rangeMin] = 0x7C66;
            mapDataStd [rangeNo] [0xF7EE - rangeMin] = 0x7E95;
            mapDataStd [rangeNo] [0xF7EF - rangeMin] = 0x826C;

            mapDataStd [rangeNo] [0xF7F0 - rangeMin] = 0x863A;
            mapDataStd [rangeNo] [0xF7F1 - rangeMin] = 0x8640;
            mapDataStd [rangeNo] [0xF7F2 - rangeMin] = 0x8639;
            mapDataStd [rangeNo] [0xF7F3 - rangeMin] = 0x863C;
            mapDataStd [rangeNo] [0xF7F4 - rangeMin] = 0x8631;
            mapDataStd [rangeNo] [0xF7F5 - rangeMin] = 0x863B;
            mapDataStd [rangeNo] [0xF7F6 - rangeMin] = 0x863E;
            mapDataStd [rangeNo] [0xF7F7 - rangeMin] = 0x8830;
            mapDataStd [rangeNo] [0xF7F8 - rangeMin] = 0x8832;
            mapDataStd [rangeNo] [0xF7F9 - rangeMin] = 0x882E;
            mapDataStd [rangeNo] [0xF7FA - rangeMin] = 0x8833;
            mapDataStd [rangeNo] [0xF7FB - rangeMin] = 0x8976;
            mapDataStd [rangeNo] [0xF7FC - rangeMin] = 0x8974;
            mapDataStd [rangeNo] [0xF7FD - rangeMin] = 0x8973;
            mapDataStd [rangeNo] [0xF7FE - rangeMin] = 0x89FE;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xF840 - rangeMin] = 0x8B8C;
            mapDataStd [rangeNo] [0xF841 - rangeMin] = 0x8B8E;
            mapDataStd [rangeNo] [0xF842 - rangeMin] = 0x8B8B;
            mapDataStd [rangeNo] [0xF843 - rangeMin] = 0x8B88;
            mapDataStd [rangeNo] [0xF844 - rangeMin] = 0x8C45;
            mapDataStd [rangeNo] [0xF845 - rangeMin] = 0x8D19;
            mapDataStd [rangeNo] [0xF846 - rangeMin] = 0x8E98;
            mapDataStd [rangeNo] [0xF847 - rangeMin] = 0x8F64;
            mapDataStd [rangeNo] [0xF848 - rangeMin] = 0x8F63;
            mapDataStd [rangeNo] [0xF849 - rangeMin] = 0x91BC;
            mapDataStd [rangeNo] [0xF84A - rangeMin] = 0x9462;
            mapDataStd [rangeNo] [0xF84B - rangeMin] = 0x9455;
            mapDataStd [rangeNo] [0xF84C - rangeMin] = 0x945D;
            mapDataStd [rangeNo] [0xF84D - rangeMin] = 0x9457;
            mapDataStd [rangeNo] [0xF84E - rangeMin] = 0x945E;
            mapDataStd [rangeNo] [0xF84F - rangeMin] = 0x97C4;

            mapDataStd [rangeNo] [0xF850 - rangeMin] = 0x97C5;
            mapDataStd [rangeNo] [0xF851 - rangeMin] = 0x9800;
            mapDataStd [rangeNo] [0xF852 - rangeMin] = 0x9A56;
            mapDataStd [rangeNo] [0xF853 - rangeMin] = 0x9A59;
            mapDataStd [rangeNo] [0xF854 - rangeMin] = 0x9B1E;
            mapDataStd [rangeNo] [0xF855 - rangeMin] = 0x9B1F;
            mapDataStd [rangeNo] [0xF856 - rangeMin] = 0x9B20;
            mapDataStd [rangeNo] [0xF857 - rangeMin] = 0x9C52;
            mapDataStd [rangeNo] [0xF858 - rangeMin] = 0x9C58;
            mapDataStd [rangeNo] [0xF859 - rangeMin] = 0x9C50;
            mapDataStd [rangeNo] [0xF85A - rangeMin] = 0x9C4A;
            mapDataStd [rangeNo] [0xF85B - rangeMin] = 0x9C4D;
            mapDataStd [rangeNo] [0xF85C - rangeMin] = 0x9C4B;
            mapDataStd [rangeNo] [0xF85D - rangeMin] = 0x9C55;
            mapDataStd [rangeNo] [0xF85E - rangeMin] = 0x9C59;
            mapDataStd [rangeNo] [0xF85F - rangeMin] = 0x9C4C;

            mapDataStd [rangeNo] [0xF860 - rangeMin] = 0x9C4E;
            mapDataStd [rangeNo] [0xF861 - rangeMin] = 0x9DFB;
            mapDataStd [rangeNo] [0xF862 - rangeMin] = 0x9DF7;
            mapDataStd [rangeNo] [0xF863 - rangeMin] = 0x9DEF;
            mapDataStd [rangeNo] [0xF864 - rangeMin] = 0x9DE3;
            mapDataStd [rangeNo] [0xF865 - rangeMin] = 0x9DEB;
            mapDataStd [rangeNo] [0xF866 - rangeMin] = 0x9DF8;
            mapDataStd [rangeNo] [0xF867 - rangeMin] = 0x9DE4;
            mapDataStd [rangeNo] [0xF868 - rangeMin] = 0x9DF6;
            mapDataStd [rangeNo] [0xF869 - rangeMin] = 0x9DE1;
            mapDataStd [rangeNo] [0xF86A - rangeMin] = 0x9DEE;
            mapDataStd [rangeNo] [0xF86B - rangeMin] = 0x9DE6;
            mapDataStd [rangeNo] [0xF86C - rangeMin] = 0x9DF2;
            mapDataStd [rangeNo] [0xF86D - rangeMin] = 0x9DF0;
            mapDataStd [rangeNo] [0xF86E - rangeMin] = 0x9DE2;
            mapDataStd [rangeNo] [0xF86F - rangeMin] = 0x9DEC;

            mapDataStd [rangeNo] [0xF870 - rangeMin] = 0x9DF4;
            mapDataStd [rangeNo] [0xF871 - rangeMin] = 0x9DF3;
            mapDataStd [rangeNo] [0xF872 - rangeMin] = 0x9DE8;
            mapDataStd [rangeNo] [0xF873 - rangeMin] = 0x9DED;
            mapDataStd [rangeNo] [0xF874 - rangeMin] = 0x9EC2;
            mapDataStd [rangeNo] [0xF875 - rangeMin] = 0x9ED0;
            mapDataStd [rangeNo] [0xF876 - rangeMin] = 0x9EF2;
            mapDataStd [rangeNo] [0xF877 - rangeMin] = 0x9EF3;
            mapDataStd [rangeNo] [0xF878 - rangeMin] = 0x9F06;
            mapDataStd [rangeNo] [0xF879 - rangeMin] = 0x9F1C;
            mapDataStd [rangeNo] [0xF87A - rangeMin] = 0x9F38;
            mapDataStd [rangeNo] [0xF87B - rangeMin] = 0x9F37;
            mapDataStd [rangeNo] [0xF87C - rangeMin] = 0x9F36;
            mapDataStd [rangeNo] [0xF87D - rangeMin] = 0x9F43;
            mapDataStd [rangeNo] [0xF87E - rangeMin] = 0x9F4F;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xF8A1 - rangeMin] = 0x9F71;
            mapDataStd [rangeNo] [0xF8A2 - rangeMin] = 0x9F70;
            mapDataStd [rangeNo] [0xF8A3 - rangeMin] = 0x9F6E;
            mapDataStd [rangeNo] [0xF8A4 - rangeMin] = 0x9F6F;
            mapDataStd [rangeNo] [0xF8A5 - rangeMin] = 0x56D3;
            mapDataStd [rangeNo] [0xF8A6 - rangeMin] = 0x56CD;
            mapDataStd [rangeNo] [0xF8A7 - rangeMin] = 0x5B4E;
            mapDataStd [rangeNo] [0xF8A8 - rangeMin] = 0x5C6D;
            mapDataStd [rangeNo] [0xF8A9 - rangeMin] = 0x652D;
            mapDataStd [rangeNo] [0xF8AA - rangeMin] = 0x66ED;
            mapDataStd [rangeNo] [0xF8AB - rangeMin] = 0x66EE;
            mapDataStd [rangeNo] [0xF8AC - rangeMin] = 0x6B13;
            mapDataStd [rangeNo] [0xF8AD - rangeMin] = 0x705F;
            mapDataStd [rangeNo] [0xF8AE - rangeMin] = 0x7061;
            mapDataStd [rangeNo] [0xF8AF - rangeMin] = 0x705D;

            mapDataStd [rangeNo] [0xF8B0 - rangeMin] = 0x7060;
            mapDataStd [rangeNo] [0xF8B1 - rangeMin] = 0x7223;
            mapDataStd [rangeNo] [0xF8B2 - rangeMin] = 0x74DB;
            mapDataStd [rangeNo] [0xF8B3 - rangeMin] = 0x74E5;
            mapDataStd [rangeNo] [0xF8B4 - rangeMin] = 0x77D5;
            mapDataStd [rangeNo] [0xF8B5 - rangeMin] = 0x7938;
            mapDataStd [rangeNo] [0xF8B6 - rangeMin] = 0x79B7;
            mapDataStd [rangeNo] [0xF8B7 - rangeMin] = 0x79B6;
            mapDataStd [rangeNo] [0xF8B8 - rangeMin] = 0x7C6A;
            mapDataStd [rangeNo] [0xF8B9 - rangeMin] = 0x7E97;
            mapDataStd [rangeNo] [0xF8BA - rangeMin] = 0x7F89;
            mapDataStd [rangeNo] [0xF8BB - rangeMin] = 0x826D;
            mapDataStd [rangeNo] [0xF8BC - rangeMin] = 0x8643;
            mapDataStd [rangeNo] [0xF8BD - rangeMin] = 0x8838;
            mapDataStd [rangeNo] [0xF8BE - rangeMin] = 0x8837;
            mapDataStd [rangeNo] [0xF8BF - rangeMin] = 0x8835;

            mapDataStd [rangeNo] [0xF8C0 - rangeMin] = 0x884B;
            mapDataStd [rangeNo] [0xF8C1 - rangeMin] = 0x8B94;
            mapDataStd [rangeNo] [0xF8C2 - rangeMin] = 0x8B95;
            mapDataStd [rangeNo] [0xF8C3 - rangeMin] = 0x8E9E;
            mapDataStd [rangeNo] [0xF8C4 - rangeMin] = 0x8E9F;
            mapDataStd [rangeNo] [0xF8C5 - rangeMin] = 0x8EA0;
            mapDataStd [rangeNo] [0xF8C6 - rangeMin] = 0x8E9D;
            mapDataStd [rangeNo] [0xF8C7 - rangeMin] = 0x91BE;
            mapDataStd [rangeNo] [0xF8C8 - rangeMin] = 0x91BD;
            mapDataStd [rangeNo] [0xF8C9 - rangeMin] = 0x91C2;
            mapDataStd [rangeNo] [0xF8CA - rangeMin] = 0x946B;
            mapDataStd [rangeNo] [0xF8CB - rangeMin] = 0x9468;
            mapDataStd [rangeNo] [0xF8CC - rangeMin] = 0x9469;
            mapDataStd [rangeNo] [0xF8CD - rangeMin] = 0x96E5;
            mapDataStd [rangeNo] [0xF8CE - rangeMin] = 0x9746;
            mapDataStd [rangeNo] [0xF8CF - rangeMin] = 0x9743;

            mapDataStd [rangeNo] [0xF8D0 - rangeMin] = 0x9747;
            mapDataStd [rangeNo] [0xF8D1 - rangeMin] = 0x97C7;
            mapDataStd [rangeNo] [0xF8D2 - rangeMin] = 0x97E5;
            mapDataStd [rangeNo] [0xF8D3 - rangeMin] = 0x9A5E;
            mapDataStd [rangeNo] [0xF8D4 - rangeMin] = 0x9AD5;
            mapDataStd [rangeNo] [0xF8D5 - rangeMin] = 0x9B59;
            mapDataStd [rangeNo] [0xF8D6 - rangeMin] = 0x9C63;
            mapDataStd [rangeNo] [0xF8D7 - rangeMin] = 0x9C67;
            mapDataStd [rangeNo] [0xF8D8 - rangeMin] = 0x9C66;
            mapDataStd [rangeNo] [0xF8D9 - rangeMin] = 0x9C62;
            mapDataStd [rangeNo] [0xF8DA - rangeMin] = 0x9C5E;
            mapDataStd [rangeNo] [0xF8DB - rangeMin] = 0x9C60;
            mapDataStd [rangeNo] [0xF8DC - rangeMin] = 0x9E02;
            mapDataStd [rangeNo] [0xF8DD - rangeMin] = 0x9DFE;
            mapDataStd [rangeNo] [0xF8DE - rangeMin] = 0x9E07;
            mapDataStd [rangeNo] [0xF8DF - rangeMin] = 0x9E03;

            mapDataStd [rangeNo] [0xF8E0 - rangeMin] = 0x9E06;
            mapDataStd [rangeNo] [0xF8E1 - rangeMin] = 0x9E05;
            mapDataStd [rangeNo] [0xF8E2 - rangeMin] = 0x9E00;
            mapDataStd [rangeNo] [0xF8E3 - rangeMin] = 0x9E01;
            mapDataStd [rangeNo] [0xF8E4 - rangeMin] = 0x9E09;
            mapDataStd [rangeNo] [0xF8E5 - rangeMin] = 0x9DFF;
            mapDataStd [rangeNo] [0xF8E6 - rangeMin] = 0x9DFD;
            mapDataStd [rangeNo] [0xF8E7 - rangeMin] = 0x9E04;
            mapDataStd [rangeNo] [0xF8E8 - rangeMin] = 0x9EA0;
            mapDataStd [rangeNo] [0xF8E9 - rangeMin] = 0x9F1E;
            mapDataStd [rangeNo] [0xF8EA - rangeMin] = 0x9F46;
            mapDataStd [rangeNo] [0xF8EB - rangeMin] = 0x9F74;
            mapDataStd [rangeNo] [0xF8EC - rangeMin] = 0x9F75;
            mapDataStd [rangeNo] [0xF8ED - rangeMin] = 0x9F76;
            mapDataStd [rangeNo] [0xF8EE - rangeMin] = 0x56D4;
            mapDataStd [rangeNo] [0xF8EF - rangeMin] = 0x652E;

            mapDataStd [rangeNo] [0xF8F0 - rangeMin] = 0x65B8;
            mapDataStd [rangeNo] [0xF8F1 - rangeMin] = 0x6B18;
            mapDataStd [rangeNo] [0xF8F2 - rangeMin] = 0x6B19;
            mapDataStd [rangeNo] [0xF8F3 - rangeMin] = 0x6B17;
            mapDataStd [rangeNo] [0xF8F4 - rangeMin] = 0x6B1A;
            mapDataStd [rangeNo] [0xF8F5 - rangeMin] = 0x7062;
            mapDataStd [rangeNo] [0xF8F6 - rangeMin] = 0x7226;
            mapDataStd [rangeNo] [0xF8F7 - rangeMin] = 0x72AA;
            mapDataStd [rangeNo] [0xF8F8 - rangeMin] = 0x77D8;
            mapDataStd [rangeNo] [0xF8F9 - rangeMin] = 0x77D9;
            mapDataStd [rangeNo] [0xF8FA - rangeMin] = 0x7939;
            mapDataStd [rangeNo] [0xF8FB - rangeMin] = 0x7C69;
            mapDataStd [rangeNo] [0xF8FC - rangeMin] = 0x7C6B;
            mapDataStd [rangeNo] [0xF8FD - rangeMin] = 0x7CF6;
            mapDataStd [rangeNo] [0xF8FE - rangeMin] = 0x7E9A;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xF940 - rangeMin] = 0x7E98;
            mapDataStd [rangeNo] [0xF941 - rangeMin] = 0x7E9B;
            mapDataStd [rangeNo] [0xF942 - rangeMin] = 0x7E99;
            mapDataStd [rangeNo] [0xF943 - rangeMin] = 0x81E0;
            mapDataStd [rangeNo] [0xF944 - rangeMin] = 0x81E1;
            mapDataStd [rangeNo] [0xF945 - rangeMin] = 0x8646;
            mapDataStd [rangeNo] [0xF946 - rangeMin] = 0x8647;
            mapDataStd [rangeNo] [0xF947 - rangeMin] = 0x8648;
            mapDataStd [rangeNo] [0xF948 - rangeMin] = 0x8979;
            mapDataStd [rangeNo] [0xF949 - rangeMin] = 0x897A;
            mapDataStd [rangeNo] [0xF94A - rangeMin] = 0x897C;
            mapDataStd [rangeNo] [0xF94B - rangeMin] = 0x897B;
            mapDataStd [rangeNo] [0xF94C - rangeMin] = 0x89FF;
            mapDataStd [rangeNo] [0xF94D - rangeMin] = 0x8B98;
            mapDataStd [rangeNo] [0xF94E - rangeMin] = 0x8B99;
            mapDataStd [rangeNo] [0xF94F - rangeMin] = 0x8EA5;

            mapDataStd [rangeNo] [0xF950 - rangeMin] = 0x8EA4;
            mapDataStd [rangeNo] [0xF951 - rangeMin] = 0x8EA3;
            mapDataStd [rangeNo] [0xF952 - rangeMin] = 0x946E;
            mapDataStd [rangeNo] [0xF953 - rangeMin] = 0x946D;
            mapDataStd [rangeNo] [0xF954 - rangeMin] = 0x946F;
            mapDataStd [rangeNo] [0xF955 - rangeMin] = 0x9471;
            mapDataStd [rangeNo] [0xF956 - rangeMin] = 0x9473;
            mapDataStd [rangeNo] [0xF957 - rangeMin] = 0x9749;
            mapDataStd [rangeNo] [0xF958 - rangeMin] = 0x9872;
            mapDataStd [rangeNo] [0xF959 - rangeMin] = 0x995F;
            mapDataStd [rangeNo] [0xF95A - rangeMin] = 0x9C68;
            mapDataStd [rangeNo] [0xF95B - rangeMin] = 0x9C6E;
            mapDataStd [rangeNo] [0xF95C - rangeMin] = 0x9C6D;
            mapDataStd [rangeNo] [0xF95D - rangeMin] = 0x9E0B;
            mapDataStd [rangeNo] [0xF95E - rangeMin] = 0x9E0D;
            mapDataStd [rangeNo] [0xF95F - rangeMin] = 0x9E10;

            mapDataStd [rangeNo] [0xF960 - rangeMin] = 0x9E0F;
            mapDataStd [rangeNo] [0xF961 - rangeMin] = 0x9E12;
            mapDataStd [rangeNo] [0xF962 - rangeMin] = 0x9E11;
            mapDataStd [rangeNo] [0xF963 - rangeMin] = 0x9EA1;
            mapDataStd [rangeNo] [0xF964 - rangeMin] = 0x9EF5;
            mapDataStd [rangeNo] [0xF965 - rangeMin] = 0x9F09;
            mapDataStd [rangeNo] [0xF966 - rangeMin] = 0x9F47;
            mapDataStd [rangeNo] [0xF967 - rangeMin] = 0x9F78;
            mapDataStd [rangeNo] [0xF968 - rangeMin] = 0x9F7B;
            mapDataStd [rangeNo] [0xF969 - rangeMin] = 0x9F7A;
            mapDataStd [rangeNo] [0xF96A - rangeMin] = 0x9F79;
            mapDataStd [rangeNo] [0xF96B - rangeMin] = 0x571E;
            mapDataStd [rangeNo] [0xF96C - rangeMin] = 0x7066;
            mapDataStd [rangeNo] [0xF96D - rangeMin] = 0x7C6F;
            mapDataStd [rangeNo] [0xF96E - rangeMin] = 0x883C;
            mapDataStd [rangeNo] [0xF96F - rangeMin] = 0x8DB2;

            mapDataStd [rangeNo] [0xF970 - rangeMin] = 0x8EA6;
            mapDataStd [rangeNo] [0xF971 - rangeMin] = 0x91C3;
            mapDataStd [rangeNo] [0xF972 - rangeMin] = 0x9474;
            mapDataStd [rangeNo] [0xF973 - rangeMin] = 0x9478;
            mapDataStd [rangeNo] [0xF974 - rangeMin] = 0x9476;
            mapDataStd [rangeNo] [0xF975 - rangeMin] = 0x9475;
            mapDataStd [rangeNo] [0xF976 - rangeMin] = 0x9A60;
            mapDataStd [rangeNo] [0xF977 - rangeMin] = 0x9C74;
            mapDataStd [rangeNo] [0xF978 - rangeMin] = 0x9C73;
            mapDataStd [rangeNo] [0xF979 - rangeMin] = 0x9C71;
            mapDataStd [rangeNo] [0xF97A - rangeMin] = 0x9C75;
            mapDataStd [rangeNo] [0xF97B - rangeMin] = 0x9E14;
            mapDataStd [rangeNo] [0xF97C - rangeMin] = 0x9E13;
            mapDataStd [rangeNo] [0xF97D - rangeMin] = 0x9EF6;
            mapDataStd [rangeNo] [0xF97E - rangeMin] = 0x9F0A;

            //----------------------------------------------------------------//

            rangeNo++;
            rangeMin = rangeData [rangeNo] [0];
            rangeMax = rangeData [rangeNo] [1];

            for (UInt16 i = rangeMin; i <= rangeMax; i++)
            {
                mapDataStd [rangeNo] [i - rangeMin] = 0xffff;
            }

            mapDataStd [rangeNo] [0xF9A1 - rangeMin] = 0x9FA4;
            mapDataStd [rangeNo] [0xF9A2 - rangeMin] = 0x7068;
            mapDataStd [rangeNo] [0xF9A3 - rangeMin] = 0x7065;
            mapDataStd [rangeNo] [0xF9A4 - rangeMin] = 0x7CF7;
            mapDataStd [rangeNo] [0xF9A5 - rangeMin] = 0x866A;
            mapDataStd [rangeNo] [0xF9A6 - rangeMin] = 0x883E;
            mapDataStd [rangeNo] [0xF9A7 - rangeMin] = 0x883D;
            mapDataStd [rangeNo] [0xF9A8 - rangeMin] = 0x883F;
            mapDataStd [rangeNo] [0xF9A9 - rangeMin] = 0x8B9E;
            mapDataStd [rangeNo] [0xF9AA - rangeMin] = 0x8C9C;
            mapDataStd [rangeNo] [0xF9AB - rangeMin] = 0x8EA9;
            mapDataStd [rangeNo] [0xF9AC - rangeMin] = 0x8EC9;
            mapDataStd [rangeNo] [0xF9AD - rangeMin] = 0x974B;
            mapDataStd [rangeNo] [0xF9AE - rangeMin] = 0x9873;
            mapDataStd [rangeNo] [0xF9AF - rangeMin] = 0x9874;

            mapDataStd [rangeNo] [0xF9B0 - rangeMin] = 0x98CC;
            mapDataStd [rangeNo] [0xF9B1 - rangeMin] = 0x9961;
            mapDataStd [rangeNo] [0xF9B2 - rangeMin] = 0x99AB;
            mapDataStd [rangeNo] [0xF9B3 - rangeMin] = 0x9A64;
            mapDataStd [rangeNo] [0xF9B4 - rangeMin] = 0x9A66;
            mapDataStd [rangeNo] [0xF9B5 - rangeMin] = 0x9A67;
            mapDataStd [rangeNo] [0xF9B6 - rangeMin] = 0x9B24;
            mapDataStd [rangeNo] [0xF9B7 - rangeMin] = 0x9E15;
            mapDataStd [rangeNo] [0xF9B8 - rangeMin] = 0x9E17;
            mapDataStd [rangeNo] [0xF9B9 - rangeMin] = 0x9F48;
            mapDataStd [rangeNo] [0xF9BA - rangeMin] = 0x6207;
            mapDataStd [rangeNo] [0xF9BB - rangeMin] = 0x6B1E;
            mapDataStd [rangeNo] [0xF9BC - rangeMin] = 0x7227;
            mapDataStd [rangeNo] [0xF9BD - rangeMin] = 0x864C;
            mapDataStd [rangeNo] [0xF9BE - rangeMin] = 0x8EA8;
            mapDataStd [rangeNo] [0xF9BF - rangeMin] = 0x9482;

            mapDataStd [rangeNo] [0xF9C0 - rangeMin] = 0x9480;
            mapDataStd [rangeNo] [0xF9C1 - rangeMin] = 0x9481;
            mapDataStd [rangeNo] [0xF9C2 - rangeMin] = 0x9A69;
            mapDataStd [rangeNo] [0xF9C3 - rangeMin] = 0x9A68;
            mapDataStd [rangeNo] [0xF9C4 - rangeMin] = 0x9B2E;
            mapDataStd [rangeNo] [0xF9C5 - rangeMin] = 0x9E19;
            mapDataStd [rangeNo] [0xF9C6 - rangeMin] = 0x7229;
            mapDataStd [rangeNo] [0xF9C7 - rangeMin] = 0x864B;
            mapDataStd [rangeNo] [0xF9C8 - rangeMin] = 0x8B9F;
            mapDataStd [rangeNo] [0xF9C9 - rangeMin] = 0x9483;
            mapDataStd [rangeNo] [0xF9CA - rangeMin] = 0x9C79;
            mapDataStd [rangeNo] [0xF9CB - rangeMin] = 0x9EB7;
            mapDataStd [rangeNo] [0xF9CC - rangeMin] = 0x7675;
            mapDataStd [rangeNo] [0xF9CD - rangeMin] = 0x9A6B;
            mapDataStd [rangeNo] [0xF9CE - rangeMin] = 0x9C7A;
            mapDataStd [rangeNo] [0xF9CF - rangeMin] = 0x9E1D;

            mapDataStd [rangeNo] [0xF9D0 - rangeMin] = 0x7069;
            mapDataStd [rangeNo] [0xF9D1 - rangeMin] = 0x706A;
            mapDataStd [rangeNo] [0xF9D2 - rangeMin] = 0x9EA4;
            mapDataStd [rangeNo] [0xF9D3 - rangeMin] = 0x9F7E;
            mapDataStd [rangeNo] [0xF9D4 - rangeMin] = 0x9F49;
            mapDataStd [rangeNo] [0xF9D5 - rangeMin] = 0x9F98;



            //----------------------------------------------------------------//

       //     rangeNo++;
       //     rangeMin = rangeData [rangeNo] [0];

            //----------------------------------------------------------------//

            _sets.Add (new PCLSymSetMap (mapId,
                                         rangeCt,
                                         rangeData,
                                         mapDataStd,
                                         null));
        }
    }
}