using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides details of PCL XL Attribute Enumeration values.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class PCLXLAttrEnums
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // PCLXL Attribute enumerations.                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public enum eVal : byte
        {
            e1Bit                     = 0x00,  // Attr 0x33, 0x62 //
            e4Bit                     = 0x01,  // Attr 0x33, 0x62 //
            e8Bit                     = 0x02,  // Attr 0x33, 0x62 //
            eA3Paper                  = 0x05,  // Attr 0x25 //
            eA4Paper                  = 0x02,  // Attr 0x25 //
            eA5Paper                  = 0x10,  // Attr 0x25 //
            eA6Paper                  = 0x11,  // Attr 0x25 //
            eAdd0Degrees              = 0x00,  // Attr 0x75 //
            eAdd180Degrees            = 0x01,  // Attr 0x75 //
            eAutoSelect               = 0x01,  // Attr 0x26 //
            eB5Envelope               = 0x0c,  // Attr 0x25 //
            eB5Paper                  = 0x0d,  // Attr 0x25 //
            eBackChAndErrPage         = 0x03,  // Attr 0x8f //
            eBackChannel              = 0x01,  // Attr 0x8f //
            eBackMediaSide            = 0x01,  // Attr 0x36 //
            eBevelJoin                = 0x02,  // Attr 0x48 //
            eBiLevel                  = 0x00,  // Attr 0x03 //
            eBinaryHighByteFirst      = 0x00,  // Attr 0x82 //
            eBinaryLowByteFirst       = 0x01,  // Attr 0x82 //
            eButtCap                  = 0x00,  // Attr 0x47 //
            eC5Envelope               = 0x08,  // Attr 0x25 //
            eClockWise                = 0x00,  // Attr 0x41 //
            eCOM10Envelope            = 0x06,  // Attr 0x25 //
            eCommandsDiag             = 0x02,  // Attr 0xa0 //
            eCounterClockWise         = 0x01,  // Attr 0x41 //
            eDefaultDataSource        = 0x00,  // Attr 0x88 //
            eDefaultDestination       = 0x00,  // Attr 0x24 //
            eDefaultOrientation       = 0x04,  // Attr 0x28 //
            eDefaultPapersize         = 0x60,  // Attr 0x25 //
            eDefaultSource            = 0x00,  // Attr 0x26 //
            eDeltaRowCompression      = 0x03,  // Attr 0x65 //
            eDeviceBest               = 0x00,  // Attr 0x21 //
            eDirectPixel              = 0x00,  // Attr 0x64 //
            eDirectPlane              = 0x02,  // Attr 0x64 //
            eDisable                  = 0x00,  // Attr 0x1d, 0x1e, 0x1f, 0x20 //
            eDLEnvelope               = 0x09,  // Attr 0x25 //
            eDontCare                 = 0x04,  // Attr 0x77 //
            eDuplexHorizontalBinding  = 0x00,  // Attr 0x35 //
            eDuplexVerticalBinding    = 0x01,  // Attr 0x35 //
            eEnable                   = 0x01,  // Attr 0x1d, 0x1e, 0x1f, 0x20 //
            eEnvelopeTray             = 0x06,  // Attr 0x26 //
            eErrorPage                = 0x02,  // Attr 0x8f //
            eEvenOdd                  = 0x01,  // Attr 0x46, 0x54 //
            eExecPaper                = 0x03,  // Attr 0x25 //
            eExterior                 = 0x01,  // Attr 0x53 //
            eFaceDownBin              = 0x01,  // Attr 0x24 //
            eFaceUpBin                = 0x02,  // Attr 0x24 //
            eFilterDiag               = 0x01,  // Attr 0xa0 //
            eFrontMediaSide           = 0x00,  // Attr 0x36 //
            eFXCompression            = 0x06,  // Attr 0x65 //
            eGray                     = 0x01,  // Attr 0x03 //
            eGraySub                  = 0x07,  // Attr 0x03 //
            eHighLPI                  = 0x00,  // Attr 0x1d, 0x1e, 0x1f, 0x20 //
            eHorizontal               = 0x00,  // Attr 0xad //
            eInch                     = 0x00,  // Attr 0x86 //
            eIndexedPixel             = 0x01,  // Attr 0x64 //
            eInterior                 = 0x00,  // Attr 0x53 //
            eJB4Paper                 = 0x0a,  // Attr 0x25 //
            eJB5Paper                 = 0x0b,  // Attr 0x25 //
            eJB6Paper                 = 0x12,  // Attr 0x25 //
            eJDoublePostcard          = 0x0f,  // Attr 0x25 //
            eJIS16KPaper              = 0x14,  // Attr 0x25 //
            eJIS8KPaper               = 0x13,  // Attr 0x25 //
            eJISExecPaper             = 0x15,  // Attr 0x25 //
            eJobOffsetBin             = 0x03,  // Attr 0x24 //
            eJPEGCompression          = 0x02,  // Attr 0x65 //
            eJPostcard                = 0x0e,  // Attr 0x25 //
            eLandscapeOrientation     = 0x01,  // Attr 0x28 //
            eLedgerPaper              = 0x04,  // Attr 0x25 //
            eLegalPaper               = 0x01,  // Attr 0x25 //
            eLetterPaper              = 0x00,  // Attr 0x25 //
            eLight                    = 0x03,  // Attr 0x1d, 0x1e, 0x1f, 0x20 //
            eLowerCassette            = 0x05,  // Attr 0x26 //
            eLowLPI                   = 0x02,  // Attr 0x1d, 0x1e, 0x1f, 0x20 //
            eManualFeed               = 0x02,  // Attr 0x26 //
            eMax                      = 0x01,  // Attr 0x1d, 0x1e, 0x1f, 0x20 //
            eMediumLPI                = 0x01,  // Attr 0x1d, 0x1e, 0x1f, 0x20 //
            eMillimeter               = 0x01,  // Attr 0x86 //
            eMiterJoin                = 0x00,  // Attr 0x48 //
            eMonarchEnvelope          = 0x07,  // Attr 0x25 //
            eMultiPurposeTray         = 0x03,  // Attr 0x26 //
            eNoCompression            = 0x00,  // Attr 0x65 //
            eNoJoin                   = 0x03,  // Attr 0x48 //
            eNonZeroWinding           = 0x00,  // Attr 0x46, 0x54 //
            eNoReporting              = 0x00,  // Attr 0x8f //
            eNormal                   = 0x02,  // Attr 0x1d, 0x1e, 0x1f, 0x20 //
            eNoSubstitution           = 0x00,  // Attr 0xac //
            eNoTreatment              = 0x00,  // Attr 0x78 //
            eNWBackChAndErrPage       = 0x06,  // Attr 0x8f //
            eNWBackChannel            = 0x04,  // Attr 0x8f //
            eNWErrorPage              = 0x05,  // Attr 0x8f //
            eOpaque                   = 0x00,  // Attr 0x2d //
            ePageDiag                 = 0x04,  // Attr 0xa0 //
            ePagePattern              = 0x01,  // Attr 0x68 //
            ePersonalityDiag          = 0x03,  // Attr 0xa0 //
            ePortraitOrientation      = 0x00,  // Attr 0x28 //
            eProcessBlack             = 0x01,  // Attr 0x1d, 0x1e, 0x1f, 0x20 //
            eReverseLandscape         = 0x03,  // Attr 0x28 //
            eReversePortrait          = 0x02,  // Attr 0x28 //
            eRGB                      = 0x02,  // Attr 0x03 //
            eRLECompression           = 0x01,  // Attr 0x65 //
            eRoundCap                 = 0x01,  // Attr 0x47 //
            eRoundJoin                = 0x01,  // Attr 0x48 //
            eSByte                    = 0x01,  // Attr 0x22, 0x50 //
            eScreenMatch              = 0x01,  // Attr 0x78 //
            eSessionPattern           = 0x02,  // Attr 0x68 //
            eSimplexFrontSide         = 0x00,  // Attr 0x34 //
            eSint16                   = 0x03,  // Attr 0x22, 0x50 //
            eSquareCap                = 0x02,  // Attr 0x47 //
            eSRGB                     = 0x06,  // Attr 0x03 //
            eTempPattern              = 0x00,  // Attr 0x68 //
            eTenthsOfAMillimeter      = 0x02,  // Attr 0x86 //
            eTonerBlack               = 0x00,  // Attr 0x1d, 0x1e, 0x1f, 0x20 //
            eThirdCassette            = 0x07,  // Attr 0x26 //
            eTransparent              = 0x01,  // Attr 0x2d //
            eTriangleCap              = 0x03,  // Attr 0x47 //
            eUByte                    = 0x00,  // Attr 0x22, 0x50 //
            eUint16                   = 0x02,  // Attr 0x22, 0x50 //
            eUpperCassette            = 0x04,  // Attr 0x26 //
            eVertical                 = 0x01,  // Attr 0xad //
            eVerticalRotated          = 0x02,  // Attr 0xad //
            eVerticalSubstitution     = 0x01,  // Attr 0xac //
            eVivid                    = 0x02   // Attr 0x78 //
        }

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static SortedList<String, PCLXLAttrEnum> _tags =
            new SortedList<String, PCLXLAttrEnum>();

        private static PCLXLAttrEnum _tagUnknown; 

        private static Int32 _tagCount;
        
        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L X L A t t r E n u m s                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        static PCLXLAttrEnums()
        {
            populateTable();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h e c k V a l u e                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Searches the PCL XL Attribute Enumerations table for a matching    //
        // entry.                                                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean checkValue (Int32  level,
                                          Byte  crntOperTag,
                                          Int32 attrTagLen,
                                          Byte  attrTagA,
                                          Byte  attrTagB,
                                          UInt32 enumVal,
                                          Boolean operEnumeration,
                                          ref Boolean flagValIsTxt,
                                          ref String desc)
        {
            Boolean seqKnown = false;

            PCLXLAttrEnum tag;
            
            String key;

            Byte operTag;

            if (operEnumeration)
                operTag = crntOperTag;
            else
                operTag = 0x00;

            key = operTag.ToString ("X2") +
                  attrTagLen.ToString ("X2") +
                  attrTagA.ToString ("X2") +
                  attrTagB.ToString ("X2") +
                  ":" +
                  enumVal.ToString("X8");

            if (_tags.IndexOfKey (key) != -1)
            {
                seqKnown = true;
                tag = _tags[key];
            }
            else
            {
                seqKnown = false;
                tag = _tagUnknown;
            }

            tag.getDetails (ref flagValIsTxt,
                            ref desc);

            tag.incrementStatisticsCount (level);

            return seqKnown;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d i s p l a y S t a t s C o u n t s                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Add counts of referenced sequences to nominated data table.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void displayStatsCounts (DataTable table,
                                               Boolean incUsedSeqsOnly,
                                               Boolean excUnusedResTags)
        {
            Int32 count = 0;

            Boolean displaySeq,
                    hddrWritten;

            DataRow row;

            hddrWritten = false;

            //----------------------------------------------------------------//

            displaySeq = true;

            count = _tagUnknown.StatsCtTotal;

            if (count == 0)
                displaySeq = false;

            if (displaySeq)
            {
                if (!hddrWritten)
                {
                    displayStatsCountsHddr (table);
                    hddrWritten = true;
                }

                row = table.NewRow ();

                row[0] = _tagUnknown.ValueWithOpAndAttr;
                row[1] = _tagUnknown.Description;
                row[2] = _tagUnknown.StatsCtParent;
                row[3] = _tagUnknown.StatsCtChild;
                row[4] = _tagUnknown.StatsCtTotal;

                table.Rows.Add (row);
            }

            //----------------------------------------------------------------//

            foreach (KeyValuePair<String, PCLXLAttrEnum> kvp in _tags)
            {
                displaySeq = true;

                count = kvp.Value.StatsCtTotal;

                if (count == 0)
                {
                    if (incUsedSeqsOnly)
                        displaySeq = false;
                }


                if (displaySeq)
                {
                    if (!hddrWritten)
                    {
                        displayStatsCountsHddr (table);
                        hddrWritten = true;
                    }

                    row = table.NewRow ();

                    row[0] = kvp.Value.ValueWithOpAndAttr;
                    row[1] = kvp.Value.Description;
                    row[2] = kvp.Value.StatsCtParent;
                    row[3] = kvp.Value.StatsCtChild;
                    row[4] = kvp.Value.StatsCtTotal;

                    table.Rows.Add (row);
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d i s p l a y S t a t s C o u n t s H d d r                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Add statistics header lines to nominated data table.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void displayStatsCountsHddr(DataTable table)
        {
            DataRow row;

            //----------------------------------------------------------------//

            row = table.NewRow ();

            row[0] = "";
            row[1] = "______________________________";
            row[2] = "";
            row[3] = "";
            row[4] = "";

            table.Rows.Add (row);

            row = table.NewRow ();

            row[0] = "Oper Attr   Value";
            row[1] = "PCL XL Attribute enumerations:";
            row[2] = "";
            row[3] = "";
            row[4] = "";

            table.Rows.Add (row);

            row = table.NewRow ();

            row[0] = "";
            row[1] = "¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯";
            row[2] = "";
            row[3] = "";
            row[4] = "";

            table.Rows.Add (row);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d i s p l a y T a g s                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Display list of Attribute Enumerations.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 displayTags(DataGrid grid)
        {
            Int32 count = 0;

            foreach (KeyValuePair<String, PCLXLAttrEnum> kvp in _tags)
            {
                count++;
                grid.Items.Add(kvp.Value);
            }

            return count;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p o p u l a t e T a b l e                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Populate the table of Attribute Enumeration values.                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void populateTable()
        {
            const Boolean flagNone     = false;
            const Boolean flagValIsTxt = true;

            Byte operTag;
            Byte attrTagA;
            Byte attrTagB = 0x00;

            Int32 attrLen1 = 1;
        //  Int32 attrLen2 = 2; // no 2-byte attribute tages defined yet
            Int32 attrLen;
            Int32 enumVal;

            String root;
            
            attrLen = attrLen1;

            _tagUnknown =
                new PCLXLAttrEnum (0x00, 0x00, 0x00, 1, 0,
                                  flagNone,
                                  "*** Unknown enum ***");

            operTag = 0x00;                                           // ---- //

            attrTagA = (Byte) PCLXLAttributes.eTag.PaletteDepth;      // 0x02 // 
            root = operTag.ToString("X2") +
                   attrLen.ToString("X2") +
                   attrTagA.ToString("X2") +
                   attrTagB.ToString("X2");

            enumVal = (Int32) eVal.e1Bit;                 // ---- 0x02     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "e1Bit"));

            enumVal = (Int32) eVal.e4Bit;                 // ---- 0x02     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "e4Bit"));

            enumVal = (Int32) eVal.e8Bit;                 // ---- 0x02     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "e8Bit"));

            attrTagA = (Byte) PCLXLAttributes.eTag.ColorSpace;        // 0x03 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eBiLevel;              // ---- 0x03     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eBiLevel"));

            enumVal = (Int32) eVal.eGray;                 // ---- 0x03     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eGray"));

            enumVal = (Int32) eVal.eRGB;                  // ---- 0x03     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eRGB"));

            enumVal = (Int32) eVal.eSRGB;                 // ---- 0x03     6 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eSRGB"));

            enumVal = (Int32) eVal.eGraySub;              // ---- 0x03     7 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eGraySub"));

            attrTagA = (Byte) PCLXLAttributes.eTag.DeviceMatrix;      // 0x21 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eDeviceBest;           // ---- 0x21     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eDeviceBest"));

            attrTagA = (Byte) PCLXLAttributes.eTag.DitherMatrixDataType; // 0x22 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eUByte;                // ---- 0x22     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eUByte"));

            enumVal = (Int32) eVal.eSByte;                // ---- 0x22     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eSByte"));

            enumVal = (Int32) eVal.eUint16;               // ---- 0x22     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eUint16"));

            enumVal = (Int32) eVal.eSint16;               // ---- 0x22     3 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eSint16"));

            attrTagA = (Byte) PCLXLAttributes.eTag.MediaDestination;  // 0x24 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eDefaultDestination;   // ---- 0x24     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eDefaultDestination"));

            enumVal = (Int32) eVal.eFaceDownBin;          // ---- 0x24     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eFaceDownBin"));

            enumVal = (Int32) eVal.eFaceUpBin;            // ---- 0x24     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eFaceUpBin"));

            enumVal = (Int32) eVal.eJobOffsetBin;         // ---- 0x24     3 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eJobOffsetBin"));

            enumVal = 5;                                  // ---- 0x24     5 //
            _tags.Add (root + ":" + enumVal.ToString ("X8"),
                new PCLXLAttrEnum (operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eExternalBin_01"));

            enumVal = 6;                                  // ---- 0x24     6 //
            _tags.Add (root + ":" + enumVal.ToString ("X8"),
                new PCLXLAttrEnum (operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eExternalBin_02"));

            enumVal = 7;                                  // ---- 0x24     7 //
            _tags.Add (root + ":" + enumVal.ToString ("X8"),
                new PCLXLAttrEnum (operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eExternalBin_03"));

            enumVal = 8;                                  // ---- 0x24     8 //
            _tags.Add (root + ":" + enumVal.ToString ("X8"),
                new PCLXLAttrEnum (operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eExternalBin_04"));

            enumVal = 9;                                  // ---- 0x24     9 //
            _tags.Add (root + ":" + enumVal.ToString ("X8"),
                new PCLXLAttrEnum (operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eExternalBin_05"));

            enumVal = 10;                                 // ---- 0x24    10 //
            _tags.Add (root + ":" + enumVal.ToString ("X8"),
                new PCLXLAttrEnum (operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eExternalBin_06"));

            enumVal = 11;                                 // ---- 0x24    11 //
            _tags.Add (root + ":" + enumVal.ToString ("X8"),
                new PCLXLAttrEnum (operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eExternalBin_07"));

            enumVal = 12;                                 // ---- 0x24    12 //
            _tags.Add (root + ":" + enumVal.ToString ("X8"),
                new PCLXLAttrEnum (operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eExternalBin_08"));

            enumVal = 13;                                 // ---- 0x24    13 //
            _tags.Add (root + ":" + enumVal.ToString ("X8"),
                new PCLXLAttrEnum (operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eExternalBin_09"));

            enumVal = 14;                                 // ---- 0x24    14 //
            _tags.Add (root + ":" + enumVal.ToString ("X8"),
                new PCLXLAttrEnum (operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eExternalBin_10"));

            attrTagA = (Byte) PCLXLAttributes.eTag.MediaSize;         // 0x25 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eLetterPaper;          // ---- 0x25     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eLetterPaper"));

            enumVal = (Int32) eVal.eLegalPaper;           // ---- 0x25     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eLegalPaper"));

            enumVal = (Int32) eVal.eA4Paper;              // ---- 0x25     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eA4Paper"));

            enumVal = (Int32) eVal.eExecPaper;            // ---- 0x25     3 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eExecPaper"));

            enumVal = (Int32) eVal.eLedgerPaper;          // ---- 0x25     4 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eLedgerPaper"));

            enumVal = (Int32) eVal.eA3Paper;              // ---- 0x25     5 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eA3Paper"));

            enumVal = (Int32) eVal.eCOM10Envelope;        // ---- 0x25     6 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eCOM10Envelope"));

            enumVal = (Int32) eVal.eMonarchEnvelope;      // ---- 0x25     7 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eMonarchEnvelope"));

            enumVal = (Int32) eVal.eC5Envelope;           // ---- 0x25     8 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eC5Envelope"));

            enumVal = (Int32) eVal.eDLEnvelope;           // ---- 0x25     9 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eDLEnvelope"));

            enumVal = (Int32) eVal.eJB4Paper;             // ---- 0x25    10 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eJB4Paper"));

            enumVal = (Int32) eVal.eJB5Paper;             // ---- 0x25    11 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eJB5Paper"));

            enumVal = (Int32) eVal.eB5Envelope;           // ---- 0x25    12 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eB5Envelope"));

            enumVal = (Int32) eVal.eB5Paper;              // ---- 0x25    13 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eB5Paper"));

            enumVal = (Int32) eVal.eJPostcard;            // ---- 0x25    14 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eJPostcard"));

            enumVal = (Int32) eVal.eJDoublePostcard;      // ---- 0x25    15 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eJDoublePostcard"));

            enumVal = (Int32) eVal.eA5Paper;              // ---- 0x25    16 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eA5Paper"));

            enumVal = (Int32) eVal.eA6Paper;              // ---- 0x25    17 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eA6Paper"));

            enumVal = (Int32) eVal.eJB6Paper;             // ---- 0x25    18 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eJB6Paper"));

            enumVal = (Int32) eVal.eJIS8KPaper;           // ---- 0x25    19 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eJIS8KPaper"));

            enumVal = (Int32) eVal.eJIS16KPaper;          // ---- 0x25    20 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eJIS16KPaper"));

            enumVal = (Int32) eVal.eJISExecPaper;         // ---- 0x25    21 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eJISExecPaper"));

            enumVal = (Int32) eVal.eDefaultPapersize;     // ---- 0x25    96 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eDefaultPapersize"));

            attrTagA = (Byte) PCLXLAttributes.eTag.MediaSource;       // 0x26 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eDefaultSource;        // ---- 0x26     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eDefaultSource"));

            enumVal = (Int32) eVal.eAutoSelect;           // ---- 0x26     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eAutoSelect"));

            enumVal = (Int32) eVal.eManualFeed;           // ---- 0x26     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eManualFeed"));

            enumVal = (Int32) eVal.eMultiPurposeTray;     // ---- 0x26     3 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eMultiPurposeTray"));

            enumVal = (Int32) eVal.eUpperCassette;        // ---- 0x26     4 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eUpperCassette"));

            enumVal = (Int32) eVal.eLowerCassette;        // ---- 0x26     5 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eLowerCassette"));

            enumVal = (Int32) eVal.eEnvelopeTray;         // ---- 0x26     6 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eEnvelopeTray"));

            enumVal = (Int32) eVal.eThirdCassette;        // ---- 0x26     7 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eThirdCassette"));

            enumVal = 8;                                  // ---- 0x26     8 //
            _tags.Add (root + ":" + enumVal.ToString ("X8"),
                new PCLXLAttrEnum (operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eExternalTray_01"));

            enumVal = 9;                                  // ---- 0x26     9 //
            _tags.Add (root + ":" + enumVal.ToString ("X8"),
                new PCLXLAttrEnum (operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eExternalTray_02"));

            enumVal = 10;                                 // ---- 0x26    10 //
            _tags.Add (root + ":" + enumVal.ToString ("X8"),
                new PCLXLAttrEnum (operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eExternalTray_03"));

            enumVal = 11;                                 // ---- 0x26    11 //
            _tags.Add (root + ":" + enumVal.ToString ("X8"),
                new PCLXLAttrEnum (operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eExternalTray_04"));

            enumVal = 12;                                 // ---- 0x26    12 //
            _tags.Add (root + ":" + enumVal.ToString ("X8"),
                new PCLXLAttrEnum (operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eExternalTray_05"));

            enumVal = 13;                                 // ---- 0x26    13 //
            _tags.Add (root + ":" + enumVal.ToString ("X8"),
                new PCLXLAttrEnum (operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eExternalTray_06"));

            enumVal = 14;                                 // ---- 0x26    14 //
            _tags.Add (root + ":" + enumVal.ToString ("X8"),
                new PCLXLAttrEnum (operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eExternalTray_07"));

            enumVal = 15;                                 // ---- 0x26    15 //
            _tags.Add (root + ":" + enumVal.ToString ("X8"),
                new PCLXLAttrEnum (operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eExternalTray_08"));

            enumVal = 16;                                 // ---- 0x26    16 //
            _tags.Add (root + ":" + enumVal.ToString ("X8"),
                new PCLXLAttrEnum (operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eExternalTray_09"));

            enumVal = 17;                                 // ---- 0x26    17 //
            _tags.Add (root + ":" + enumVal.ToString ("X8"),
                new PCLXLAttrEnum (operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eExternalTray_10"));

            attrTagA = (Byte) PCLXLAttributes.eTag.Orientation;       // 0x28 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.ePortraitOrientation;  // ---- 0x28     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "ePortraitOrientation"));

            enumVal = (Int32) eVal.eLandscapeOrientation; // ---- 0x28     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eLandscapeOrientation"));

            enumVal = (Int32) eVal.eReversePortrait;      // ---- 0x28     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eReversePortrait"));

            enumVal = (Int32) eVal.eReverseLandscape;     // ---- 0x28     3 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eReverseLandscape"));

            enumVal = (Int32) eVal.eDefaultOrientation;   // ---- 0x28     4 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eDefaultOrientation"));

            attrTagA = (Byte) PCLXLAttributes.eTag.ROP3;              // 0x2c // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = 0;                                   // ---- 0x2c     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_0"));

            enumVal = 1;                                   // ---- 0x2c     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSoon"));

            enumVal = 2;                                   // ---- 0x2c     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSona"));

            enumVal = 3;                                   // ---- 0x2c     3 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSon"));

            enumVal = 4;                                   // ---- 0x2c     4 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPona"));

            enumVal = 5;                                   // ---- 0x2c     5 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPon"));

            enumVal = 6;                                   // ---- 0x2c     6 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSxnon"));

            enumVal = 7;                                   // ---- 0x2c     7 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSaon"));

            enumVal = 8;                                   // ---- 0x2c     8 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDTnaa"));

            enumVal = 9;                                   // ---- 0x2c     9 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSxon"));

            enumVal = 10;                                  // ---- 0x2c    10 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPna"));

            enumVal = 11;                                  // ---- 0x2c    11 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSDnaon"));

            enumVal = 12;                                  // ---- 0x2c    12 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPna"));

            enumVal = 13;                                  // ---- 0x2c    13 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSnaon"));

            enumVal = 14;                                  // ---- 0x2c    14 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSonon"));

            enumVal = 15;                                  // ---- 0x2c    15 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_Pn"));

            enumVal = 16;                                  // ---- 0x2c    16 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSona"));

            enumVal = 17;                                  // ---- 0x2c    17 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSon"));

            enumVal = 18;                                  // ---- 0x2c    18 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPxnon"));

            enumVal = 19;                                  // ---- 0x2c    19 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPaon"));

            enumVal = 20;                                  // ---- 0x2c    20 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSxnon"));

            enumVal = 21;                                  // ---- 0x2c    21 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSaon"));

            enumVal = 22;                                  // ---- 0x2c    22 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSDPSanaxx"));

            enumVal = 23;                                  // ---- 0x2c    23 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPxDSxaxn"));

            enumVal = 24;                                  // ---- 0x2c    24 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPxPDxa"));

            enumVal = 25;                                  // ---- 0x2c    25 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPSanaxn"));

            enumVal = 26;                                  // ---- 0x2c    26 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSPaox"));

            enumVal = 27;                                  // ---- 0x2c    27 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPSxaxn"));

            enumVal = 28;                                  // ---- 0x2c    28 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSDPaox"));

            enumVal = 29;                                  // ---- 0x2c    29 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSPDxaxn"));

            enumVal = 30;                                  // ---- 0x2c    30 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSox"));

            enumVal = 31;                                  // ---- 0x2c    31 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSoan"));

            enumVal = 32;                                  // ---- 0x2c    32 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSnaa"));

            enumVal = 33;                                  // ---- 0x2c    33 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPxon"));

            enumVal = 34;                                  // ---- 0x2c    34 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSna"));

            enumVal = 35;                                  // ---- 0x2c    35 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPDnaon"));

            enumVal = 36;                                  // ---- 0x2c    36 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPxDSxa"));

            enumVal = 37;                                  // ---- 0x2c    37 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSPanaxn"));

            enumVal = 38;                                  // ---- 0x2c    38 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPSaox"));

            enumVal = 39;                                  // ---- 0x2c    39 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPSxnox"));

            enumVal = 40;                                  // ---- 0x2c    40 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSxa"));

            enumVal = 41;                                  // ---- 0x2c    41 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSDPSaoxxn"));

            enumVal = 42;                                  // ---- 0x2c    42 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSana"));

            enumVal = 43;                                  // ---- 0x2c    43 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SSPxPDxaxn"));

            enumVal = 44;                                  // ---- 0x2c    44 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPDSoax"));

            enumVal = 45;                                  // ---- 0x2c    45 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSDnox"));

            enumVal = 46;                                  // ---- 0x2c    46 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSDPxox"));

            enumVal = 47;                                  // ---- 0x2c    47 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSDnoan"));

            enumVal = 48;                                  // ---- 0x2c    48 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSna"));

            enumVal = 49;                                  // ---- 0x2c    49 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPnaon"));

            enumVal = 50;                                  // ---- 0x2c    50 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPSoox"));

            enumVal = 51;                                  // ---- 0x2c    51 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_Sn"));

            enumVal = 52;                                  // ---- 0x2c    52 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPDSaox"));

            enumVal = 53;                                  // ---- 0x2c    53 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPDSxnox"));

            enumVal = 54;                                  // ---- 0x2c    54 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPox"));

            enumVal = 55;                                  // ---- 0x2c    55 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPoan"));

            enumVal = 56;                                  // ---- 0x2c    56 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSDPoax"));

            enumVal = 57;                                  // ---- 0x2c    57 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPDnox"));

            enumVal = 58;                                  // ---- 0x2c    58 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPDSxox"));

            enumVal = 59;                                  // ---- 0x2c    59 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPDnoan"));

            enumVal = 60;                                  // ---- 0x2c    60 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSx"));

            enumVal = 61;                                  // ---- 0x2c    61 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPDSonox"));

            enumVal = 62;                                  // ---- 0x2c    62 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPDSnaox"));

            enumVal = 63;                                  // ---- 0x2c    63 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSan"));

            enumVal = 64;                                  // ---- 0x2c    64 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSDnaa"));

            enumVal = 65;                                  // ---- 0x2c    65 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSxon"));


            enumVal = 66;                                  // ---- 0x2c    66 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDxPDxa"));

            enumVal = 67;                                  // ---- 0x2c    67 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPDSanaxn"));

            enumVal = 68;                                  // ---- 0x2c    68 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDna"));

            enumVal = 69;                                  // ---- 0x2c    69 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSnaon"));

            enumVal = 70;                                  // ---- 0x2c    70 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSPDaox"));

            enumVal = 71;                                  // ---- 0x2c    71 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSDPxaxn"));

            enumVal = 72;                                  // ---- 0x2c    72 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPxa"));

            enumVal = 73;                                  // ---- 0x2c    73 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSPDaoxxn"));

            enumVal = 74;                                  // ---- 0x2c    74 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSDoax"));

            enumVal = 75;                                  // ---- 0x2c    75 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSnox"));

            enumVal = 76;                                  // ---- 0x2c    76 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPana"));

            enumVal = 77;                                  // ---- 0x2c    77 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SSPxDSxoxn"));

            enumVal = 78;                                  // ---- 0x2c    78 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSPxox"));

            enumVal = 79;                                  // ---- 0x2c    79 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSnoan"));

            enumVal = 80;                                  // ---- 0x2c    80 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDna"));

            enumVal = 81;                                  // ---- 0x2c    81 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSPnaon"));

            enumVal = 82;                                  // ---- 0x2c    82 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSDaox"));


            enumVal = 83;                                  // ---- 0x2c    83 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPDSxaxn"));

            enumVal = 84;                                  // ---- 0x2c    84 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSonon"));

            enumVal = 85;                                  // ---- 0x2c    85 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_Dn"));

            enumVal = 86;                                  // ---- 0x2c    86 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSox"));

            enumVal = 87;                                  // ---- 0x2c    87 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSoan"));

            enumVal = 88;                                  // ---- 0x2c    88 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSPoax"));

            enumVal = 89;                                  // ---- 0x2c    89 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSnox"));

            enumVal = 90;                                  // ---- 0x2c    90 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPx"));

            enumVal = 91;                                  // ---- 0x2c    91 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSDonox"));

            enumVal = 92;                                  // ---- 0x2c    92 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSDxox"));

            enumVal = 93;                                  // ---- 0x2c    93 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSnoan"));

            enumVal = 94;                                  // ---- 0x2c    94 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSDnaox"));

            enumVal = 95;                                  // ---- 0x2c    95 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPan"));

            enumVal = 96;                                  // ---- 0x2c    96 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSxa"));

            enumVal = 97;                                  // ---- 0x2c    97 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSPDSaoxxn"));

            enumVal = 98;                                  // ---- 0x2c    98 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSPDoax"));

            enumVal = 99;                                  // ---- 0x2c    99 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPnox"));

            enumVal = 100;                                 // ---- 0x2c   100 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPSoax"));

            enumVal = 101;                                 // ---- 0x2c   101 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSPnox"));

            enumVal = 102;                                 // ---- 0x2c   102 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSx"));

            enumVal = 103;                                 // ---- 0x2c   103 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPSonox"));

            enumVal = 104;                                 // ---- 0x2c   104 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSPDSonoxxxn"));

            enumVal = 105;                                 // ---- 0x2c   105 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSxxn"));

            enumVal = 106;                                 // ---- 0x2c   106 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSax"));

            enumVal = 107;                                 // ---- 0x2c   107 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSDPSoaxn"));

            enumVal = 108;                                 // ---- 0x2c   108 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPax"));

            enumVal = 109;                                 // ---- 0x2c   109 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSPDoaxxn"));

            enumVal = 110;                                 // ---- 0x2c   110 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPSnoax"));

            enumVal = 111;                                 // ---- 0x2c   111 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSxnan"));

            enumVal = 112;                                 // ---- 0x2c   112 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSana"));

            enumVal = 113;                                 // ---- 0x2c   113 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SSDxPDxaxn"));

            enumVal = 114;                                 // ---- 0x2c   114 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPSxox"));

            enumVal = 115;                                 // ---- 0x2c   115 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPnoan"));

            enumVal = 116;                                 // ---- 0x2c   116 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSPDxox"));

            enumVal = 117;                                 // ---- 0x2c   117 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSPnoan"));

            enumVal = 118;                                 // ---- 0x2c   118 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPSnaox"));

            enumVal = 119;                                 // ---- 0x2c   119 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSan"));

            enumVal = 120;                                 // ---- 0x2c   120 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSax"));

            enumVal = 121;                                 // ---- 0x2c   121 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSPDSoaxxn"));

            enumVal = 122;                                 // ---- 0x2c   122 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSDnoax"));

            enumVal = 123;                                 // ---- 0x2c   123 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPxnan"));

            enumVal = 124;                                 // ---- 0x2c   124 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPDSnoax"));

            enumVal = 125;                                 // ---- 0x2c   125 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSxnan"));

            enumVal = 126;                                 // ---- 0x2c   126 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPxDSxo"));

            enumVal = 127;                                 // ---- 0x2c   127 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSaan"));

            enumVal = 128;                                 // ---- 0x2c   128 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSaa"));

            enumVal = 129;                                 // ---- 0x2c   129 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPxDSxon"));

            enumVal = 130;                                 // ---- 0x2c   130 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSxna"));

            enumVal = 131;                                 // ---- 0x2c   131 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPDSnoaxn"));

            enumVal = 132;                                 // ---- 0x2c   132 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPxna"));

            enumVal = 133;                                 // ---- 0x2c   133 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSPnoaxn"));

            enumVal = 134;                                 // ---- 0x2c   134 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSPDSoaxx"));

            enumVal = 135;                                 // ---- 0x2c   135 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSaxn"));

            enumVal = 136;                                 // ---- 0x2c   136 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSa"));

            enumVal = 137;                                 // ---- 0x2c   137 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPSnaoxn"));

            enumVal = 138;                                 // ---- 0x2c   138 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSPnoa"));

            enumVal = 139;                                 // ---- 0x2c   139 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSPDxoxn"));

            enumVal = 140;                                 // ---- 0x2c   140 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPnoa"));

            enumVal = 141;                                 // ---- 0x2c   141 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPSxoxn"));

            enumVal = 142;                                 // ---- 0x2c   142 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SSDxPDxax"));

            enumVal = 143;                                 // ---- 0x2c   143 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSanan"));

            enumVal = 144;                                 // ---- 0x2c   144 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSxna"));

            enumVal = 145;                                 // ---- 0x2c   145 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPSnoaxn"));

            enumVal = 146;                                 // ---- 0x2c   146 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSDPoaxx"));

            enumVal = 147;                                 // ---- 0x2c   147 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPDaxn"));

            enumVal = 148;                                 // ---- 0x2c   148 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSDPSoaxx"));

            enumVal = 149;                                 // ---- 0x2c   149 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSaxn"));

            enumVal = 150;                                 // ---- 0x2c   150 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSxx"));

            enumVal = 151;                                 // ---- 0x2c   151 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSDPSonoxx"));

            enumVal = 152;                                 // ---- 0x2c   152 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPSonoxn"));

            enumVal = 153;                                 // ---- 0x2c   153 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSxn"));

            enumVal = 154;                                 // ---- 0x2c   154 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSnax"));

            enumVal = 155;                                 // ---- 0x2c   155 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPSoaxn"));

            enumVal = 156;                                 // ---- 0x2c   156 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPDnax"));

            enumVal = 157;                                 // ---- 0x2c   157 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSPDoaxn"));

            enumVal = 158;                                 // ---- 0x2c   158 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSPDSaoxx"));

            enumVal = 159;                                 // ---- 0x2c   159 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSxan"));

            enumVal = 160;                                 // ---- 0x2c   160 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPa"));

            enumVal = 161;                                 // ---- 0x2c   161 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSPnaoxn"));

            enumVal = 162;                                 // ---- 0x2c   162 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSnoa"));

            enumVal = 163;                                 // ---- 0x2c   163 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSDxoxn"));

            enumVal = 164;                                 // ---- 0x2c   164 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSPonoxn"));

            enumVal = 165;                                 // ---- 0x2c   165 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDxn"));

            enumVal = 166;                                 // ---- 0x2c   166 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSPnax"));

            enumVal = 167;                                 // ---- 0x2c   167 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSPoaxn"));

            enumVal = 168;                                 // ---- 0x2c   168 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSoa"));

            enumVal = 169;                                 // ---- 0x2c   169 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSoxn"));

            enumVal = 170;                                 // ---- 0x2c   170 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_D"));

            enumVal = 171;                                 // ---- 0x2c   171 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSono"));

            enumVal = 172;                                 // ---- 0x2c   172 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPDSxax"));

            enumVal = 173;                                 // ---- 0x2c   173 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSDaoxn"));

            enumVal = 174;                                 // ---- 0x2c   174 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSPnao"));

            enumVal = 175;                                 // ---- 0x2c   175 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPno"));

            enumVal = 176;                                 // ---- 0x2c   176 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSnoa"));

            enumVal = 177;                                 // ---- 0x2c   177 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSPxoxn"));

            enumVal = 178;                                 // ---- 0x2c   178 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SSPxDSxox"));

            enumVal = 179;                                 // ---- 0x2c   179 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPanan"));

            enumVal = 180;                                 // ---- 0x2c   180 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSDnax"));

            enumVal = 181;                                 // ---- 0x2c   181 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSDoaxn"));

            enumVal = 182;                                 // ---- 0x2c   182 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSDPaoxx"));

            enumVal = 183;                                 // ---- 0x2c   183 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPxan"));

            enumVal = 184;                                 // ---- 0x2c   184 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSDPxax"));

            enumVal = 185;                                 // ---- 0x2c   185 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSPDaoxn"));

            enumVal = 186;                                 // ---- 0x2c   186 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSnao"));

            enumVal = 187;                                 // ---- 0x2c   187 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSno"));

            enumVal = 188;                                 // ---- 0x2c   188 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPDSanax"));

            enumVal = 189;                                 // ---- 0x2c   189 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDxPDxan"));

            enumVal = 190;                                 // ---- 0x2c   190 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSxo"));

            enumVal = 191;                                 // ---- 0x2c   191 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSano"));

            enumVal = 192;                                 // ---- 0x2c   192 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSa"));

            enumVal = 193;                                 // ---- 0x2c   193 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPDSnaoxn"));

            enumVal = 194;                                 // ---- 0x2c   194 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPDSonoxn"));

            enumVal = 195;                                 // ---- 0x2c   195 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSxn"));

            enumVal = 196;                                 // ---- 0x2c   196 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPDnoa"));

            enumVal = 197;                                 // ---- 0x2c   197 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPDSxoxn"));

            enumVal = 198;                                 // ---- 0x2c   198 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPnax"));

            enumVal = 199;                                 // ---- 0x2c   199 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSDPoaxn"));

            enumVal = 200;                                 // ---- 0x2c   200 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPoa"));

            enumVal = 201;                                 // ---- 0x2c   201 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPDoxn"));

            enumVal = 202;                                 // ---- 0x2c   202 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSDxax"));

            enumVal = 203;                                 // ---- 0x2c   203 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPDSaoxn"));

            enumVal = 204;                                 // ---- 0x2c   204 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_S"));

            enumVal = 205;                                 // ---- 0x2c   205 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPono"));

            enumVal = 206;                                 // ---- 0x2c   206 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPnao"));

            enumVal = 207;                                 // ---- 0x2c   207 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPno"));

            enumVal = 208;                                 // ---- 0x2c   208 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSDnoa"));

            enumVal = 209;                                 // ---- 0x2c   209 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSDPxoxn"));

            enumVal = 210;                                 // ---- 0x2c   210 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSnax"));

            enumVal = 211;                                 // ---- 0x2c   211 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPDSoaxn"));

            enumVal = 212;                                 // ---- 0x2c   212 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SSPxPDxax"));

            enumVal = 213;                                 // ---- 0x2c   213 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSanan"));

            enumVal = 214;                                 // ---- 0x2c   214 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSDPSaoxx"));

            enumVal = 215;                                 // ---- 0x2c   215 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSxan"));

            enumVal = 216;                                 // ---- 0x2c   216 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSPxax"));

            enumVal = 217;                                 // ---- 0x2c   217 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPSaoxn"));

            enumVal = 218;                                 // ---- 0x2c   218 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSDanax"));

            enumVal = 219;                                 // ---- 0x2c   219 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPxDSxan"));

            enumVal = 220;                                 // ---- 0x2c   220 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPDnao"));

            enumVal = 221;                                 // ---- 0x2c   221 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDno"));

            enumVal = 222;                                 // ---- 0x2c   222 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPxo"));

            enumVal = 223;                                 // ---- 0x2c   223 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPano"));

            enumVal = 224;                                 // ---- 0x2c   224 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSoa"));

            enumVal = 225;                                 // ---- 0x2c   225 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSoxn"));

            enumVal = 226;                                 // ---- 0x2c   226 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSPDxax"));

            enumVal = 227;                                 // ---- 0x2c   227 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSDPaoxn"));

            enumVal = 228;                                 // ---- 0x2c   228 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPSxax"));

            enumVal = 229;                                 // ---- 0x2c   229 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSPaoxn"));

            enumVal = 230;                                 // ---- 0x2c   230 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPSanax"));

            enumVal = 231;                                 // ---- 0x2c   231 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SPxPDxan"));

            enumVal = 232;                                 // ---- 0x2c   232 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SSPxDSxax"));

            enumVal = 233;                                 // ---- 0x2c   233 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSPDSanaxxn"));

            enumVal = 234;                                 // ---- 0x2c   234 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSao"));

            enumVal = 235;                                 // ---- 0x2c   235 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSxno"));

            enumVal = 236;                                 // ---- 0x2c   236 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPao"));

            enumVal = 237;                                 // ---- 0x2c   237 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPxno"));

            enumVal = 238;                                 // ---- 0x2c   238 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DSo"));

            enumVal = 239;                                 // ---- 0x2c   239 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_SDPnoo"));

            enumVal = 240;                                 // ---- 0x2c   240 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_P"));

            enumVal = 241;                                 // ---- 0x2c   241 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSono"));

            enumVal = 242;                                 // ---- 0x2c   242 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSnao"));

            enumVal = 243;                                 // ---- 0x2c   243 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSno"));

            enumVal = 244;                                 // ---- 0x2c   244 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSDnao"));

            enumVal = 245;                                 // ---- 0x2c   245 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDno"));

            enumVal = 246;                                 // ---- 0x2c   246 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSxo"));

            enumVal = 247;                                 // ---- 0x2c   247 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSano"));

            enumVal = 248;                                 // ---- 0x2c   248 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSao"));

            enumVal = 249;                                 // ---- 0x2c   249 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PDSxno"));

            enumVal = 250;                                 // ---- 0x2c   250 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPo"));

            enumVal = 251;                                 // ---- 0x2c   251 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSnoo"));

            enumVal = 252;                                 // ---- 0x2c   252 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSo"));

            enumVal = 253;                                 // ---- 0x2c   253 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_PSDnoo"));

            enumVal = 254;                                 // ---- 0x2c   254 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_DPSoo"));

            enumVal = 255;                                 // ---- 0x2c   255 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eROP_1"));

            attrTagA = (Byte) PCLXLAttributes.eTag.TxMode;            // 0x2d // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eOpaque;               // ---- 0x2d     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eOpaque"));

            enumVal = (Int32) eVal.eTransparent;          // ---- 0x2d     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eTransparent"));

            attrTagA = (Byte) PCLXLAttributes.eTag.CustomMediaSizeUnits; // 0x30 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eInch;                 // ---- 0x30     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eInch"));

            enumVal = (Int32) eVal.eMillimeter;           // ---- 0x30     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eMillimeter"));

            enumVal = (Int32) eVal.eTenthsOfAMillimeter;  // ---- 0x30     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eTenthsOfAMillimeter"));

            attrTagA = (Byte) PCLXLAttributes.eTag.DitherMatrixDepth; // 0x33 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.e1Bit;                 // ---- 0x33     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "e1Bit"));

            enumVal = (Int32) eVal.e4Bit;                 // ---- 0x33     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "e4Bit"));

            enumVal = (Int32) eVal.e8Bit;                 // ---- 0x33     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "e8Bit"));

            attrTagA = (Byte) PCLXLAttributes.eTag.SimplexPageMode;   // 0x34 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eSimplexFrontSide;     // ---- 0x34     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eSimplexFrontSide"));

            attrTagA = (Byte) PCLXLAttributes.eTag.DuplexPageMode;    // 0x35 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eDuplexHorizontalBinding;// ---- 0x35     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eDuplexHorizontalBinding"));

            enumVal = (Int32) eVal.eDuplexVerticalBinding; // ---- 0x35     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eDuplexVerticalBinding"));

            attrTagA = (Byte) PCLXLAttributes.eTag.DuplexPageSide;    // 0x36 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eFrontMediaSide;       // ---- 0x36     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eFrontMediaSide"));

            enumVal = (Int32) eVal.eBackMediaSide;        // ---- 0x36     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eBackMediaSide"));

            attrTagA = (Byte) PCLXLAttributes.eTag.ArcDirection;      // 0x41 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eClockWise;            // ---- 0x41     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eClockWise"));

            enumVal = (Int32) eVal.eCounterClockWise;     // ---- 0x41     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eCounterClockWise"));

            attrTagA = (Byte) PCLXLAttributes.eTag.FillMode;          // 0x46 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eNonZeroWinding;       // ---- 0x46     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eNonZeroWinding"));

            enumVal = (Int32) eVal.eEvenOdd;              // ---- 0x46     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eEvenOdd"));

            attrTagA = (Byte) PCLXLAttributes.eTag.LineCapStyle;      // 0x47 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eButtCap;              // ---- 0x47     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eButtCap"));

            enumVal = (Int32) eVal.eRoundCap;             // ---- 0x47     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eRoundCap"));

            enumVal = (Int32) eVal.eSquareCap;            // ---- 0x47     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eSquareCap"));

            enumVal = (Int32) eVal.eTriangleCap;          // ---- 0x47     3 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eTriangleCap"));

            attrTagA = (Byte) PCLXLAttributes.eTag.LineJoinStyle;     // 0x48 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eMiterJoin;            // ---- 0x48     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eMiterJoin"));

            enumVal = (Int32) eVal.eRoundJoin;            // ---- 0x48     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eRoundJoin"));

            enumVal = (Int32) eVal.eBevelJoin;            // ---- 0x48     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eBevelJoin"));

            enumVal = (Int32) eVal.eNoJoin;               // ---- 0x48     3 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eNoJoin"));

            attrTagA = (Byte) PCLXLAttributes.eTag.PointType;         // 0x50 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eUByte;                // ---- 0x50     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eUByte"));

            enumVal = (Int32) eVal.eSByte;                // ---- 0x50     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eSByte"));

            enumVal = (Int32) eVal.eUint16;               // ---- 0x50     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eUint16"));

            enumVal = (Int32) eVal.eSint16;               // ---- 0x50     3 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eSint16"));

            attrTagA = (Byte) PCLXLAttributes.eTag.ClipRegion;        // 0x53 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eInterior;             // ---- 0x53     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eInterior"));

            enumVal = (Int32) eVal.eExterior;             // ---- 0x53     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eExterior"));

            attrTagA = (Byte) PCLXLAttributes.eTag.ClipMode;          // 0x54 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eNonZeroWinding;       // ---- 0x54     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eNonZeroWinding"));

            enumVal = (Int32) eVal.eEvenOdd;              // ---- 0x54     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eEvenOdd"));

            attrTagA = (Byte) PCLXLAttributes.eTag.ColorDepth;        // 0x62 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.e1Bit;                 // ---- 0x62     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "e1Bit"));

            enumVal = (Int32) eVal.e4Bit;                 // ---- 0x62     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "e4Bit"));

            enumVal = (Int32) eVal.e8Bit;                 // ---- 0x62     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "e8Bit"));

            attrTagA = (Byte) PCLXLAttributes.eTag.ColorMapping;      // 0x64 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eDirectPixel;          // ---- 0x64     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eDirectPixel"));

            enumVal = (Int32) eVal.eIndexedPixel;         // ---- 0x64     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eIndexedPixel"));

            enumVal = (Int32) eVal.eDirectPlane;          // ---- 0x64     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eDirectPlane"));

            attrTagA = (Byte) PCLXLAttributes.eTag.CompressMode;      // 0x65 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eNoCompression;        // ---- 0x65     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eNoCompression"));

            enumVal = (Int32) eVal.eRLECompression;       // ---- 0x65     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eRLECompression"));

            enumVal = (Int32) eVal.eJPEGCompression;      // ---- 0x65     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eJPEGCompression"));

            enumVal = (Int32) eVal.eDeltaRowCompression;  // ---- 0x65     3 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eDeltaRowCompression"));

            enumVal = (Int32) eVal.eFXCompression;        // ---- 0x65     6 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eFXCompression"));

            attrTagA = (Byte) PCLXLAttributes.eTag.PatternPersistence;// 0x68 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eTempPattern;          // ---- 0x68     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eTempPattern"));

            enumVal = (Int32) eVal.ePagePattern;          // ---- 0x68     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "ePagePattern"));

            enumVal = (Int32) eVal.eSessionPattern;       // ---- 0x68     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eSessionPattern"));

            attrTagA = (Byte) PCLXLAttributes.eTag.TumbleMode;        // 0x75 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eAdd0Degrees;          // ---- 0x75     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eAdd0Degrees"));

            enumVal = (Int32) eVal.eAdd180Degrees;        // ---- 0x75     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eAdd180Degrees"));

            attrTagA = (Byte) PCLXLAttributes.eTag.ContentOrientation;// 0x76 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.ePortraitOrientation;  // ---- 0x76     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "ePortraitOrientation"));

            enumVal = (Int32) eVal.eLandscapeOrientation; // ---- 0x76     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eLandscapeOrientation"));

            enumVal = (Int32) eVal.eReversePortrait;      // ---- 0x76     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eReversePortrait"));

            enumVal = (Int32) eVal.eReverseLandscape;     // ---- 0x76     3 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eReverseLandscape"));

            enumVal = (Int32) eVal.eDefaultOrientation;   // ---- 0x76     4 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eDefaultOrientation"));

            attrTagA = (Byte) PCLXLAttributes.eTag.FeedOrientation;   // 0x77 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.ePortraitOrientation;  // ---- 0x77     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "ePortraitOrientation"));

            enumVal = (Int32) eVal.eLandscapeOrientation; // ---- 0x77     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eLandscapeOrientation"));

            enumVal = (Int32) eVal.eReversePortrait;      // ---- 0x77     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eReversePortrait"));

            enumVal = (Int32) eVal.eReverseLandscape;     // ---- 0x77     3 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eReverseLandscape"));

            enumVal = (Int32) eVal.eDontCare;             // ---- 0x77     4 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eDontCare"));

            attrTagA = (Byte) PCLXLAttributes.eTag.ColorTreatment;    // 0x78 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eNoTreatment;          // ---- 0x78     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eNoTreatment"));

            enumVal = (Int32) eVal.eScreenMatch;          // ---- 0x78     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eScreenMatch"));

            enumVal = (Int32) eVal.eVivid;                // ---- 0x78     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eVivid"));

            attrTagA = (Byte) PCLXLAttributes.eTag.DataOrg;           // 0x82 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eBinaryHighByteFirst;  // ---- 0x82     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eBinaryHighByteFirst"));

            enumVal = (Int32) eVal.eBinaryLowByteFirst;   // ---- 0x82     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eBinaryLowByteFirst"));

            attrTagA = (Byte) PCLXLAttributes.eTag.Measure;           // 0x86 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eInch;                 // ---- 0x86     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eInch"));

            enumVal = (Int32) eVal.eMillimeter;           // ---- 0x86     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eMillimeter"));

            enumVal = (Int32) eVal.eTenthsOfAMillimeter;  // ---- 0x86     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eTenthsOfAMillimeter"));

            attrTagA = (Byte) PCLXLAttributes.eTag.SourceType;        // 0x88 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eDefaultDataSource;    // ---- 0x88     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eDefaultDataSource"));

            attrTagA = (Byte) PCLXLAttributes.eTag.ErrorReport;       // 0x8f // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eNoReporting;          // ---- 0x8f     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eNoReporting"));

            enumVal = (Int32) eVal.eBackChannel;          // ---- 0x8f     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eBackChannel"));

            enumVal = (Int32) eVal.eErrorPage;            // ---- 0x8f     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eErrorPage"));

            enumVal = (Int32) eVal.eBackChAndErrPage;     // ---- 0x8f     3 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eBackChAndErrPage"));

            enumVal = (Int32) eVal.eNWBackChannel;        // ---- 0x8f     4 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eNWBackChannel"));

            enumVal = (Int32) eVal.eNWErrorPage;          // ---- 0x8f     5 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eNWErrorPage"));

            enumVal = (Int32) eVal.eNWBackChAndErrPage;   // ---- 0x8f     6 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eNWBackChAndErrPage"));

            attrTagA = (Byte) PCLXLAttributes.eTag.VUExtension;       // 0x91 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = 0x68701001;                          // ---- 0x91 hp1001//
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagValIsTxt,
                                  "HP_SetColorTreatment"));


            enumVal = 0x68701002;                          // ---- 0x91 hp1002//
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagValIsTxt,
                                  "HP_SetHalftoneMethod"));


            enumVal = 0x68701003;                          // ---- 0x91 hp1003//
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagValIsTxt,
                                  "HP_DownloadColorTable"));


            enumVal = 0x68702002;                          // ---- 0x91 hp2002//
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagValIsTxt,
                                  "HP_SelectMediaFinish"));


            enumVal = 0x68702004;                          // ---- 0x91 hp2004//
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagValIsTxt,
                                  "HP_SetInternalLUT"));


            enumVal = 0x68702101;                          // ---- 0x91 hp2101//
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagValIsTxt,
                                  "HP_JetReady1p0"));


            enumVal = 0x68704000;                          // ---- 0x91 hp4000//
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagValIsTxt,
                                  "JR3BeginImage"));


            enumVal = 0x68704001;                          // ---- 0x91 hp4001//
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagValIsTxt,
                                  "JR3ReadImage"));


            enumVal = 0x68704002;                          // ---- 0x91 hp4002//
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagValIsTxt,
                                  "JR3EndImage"));


            enumVal = 0x68704003;                          // ---- 0x91 hp4003//
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagValIsTxt,
                                  "JR3ExecStream"));

            attrTagA = (Byte) PCLXLAttributes.eTag.EnableDiagnostics; // 0xa0 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eFilterDiag;           // ---- 0xa0     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eFilterDiag"));

            enumVal = (Int32) eVal.eCommandsDiag;         // ---- 0xa0     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eCommandsDiag"));

            enumVal = (Int32) eVal.ePersonalityDiag;      // ---- 0xa0     3 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "ePersonalityDiag"));

            enumVal = (Int32) eVal.ePageDiag;             // ---- 0xa0     4 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "ePageDiag"));


            attrTagA = (Byte) PCLXLAttributes.eTag.SymbolSet;        // 0xaa // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            populateTableAddSymsets (operTag, attrTagA, attrTagB,
                                     attrLen, root);

            attrTagA = (Byte) PCLXLAttributes.eTag.CharSubModeArray;  // 0xac // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eNoSubstitution;       // ---- 0xac     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eNoSubstitution"));

            enumVal = (Int32) eVal.eVerticalSubstitution; // ---- 0xac     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eVerticalSubstitution"));

            attrTagA = (Byte) PCLXLAttributes.eTag.WritingMode;       // 0xad // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eHorizontal;           // ---- 0xad     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eHorizontal"));

            enumVal = (Int32) eVal.eVertical;             // ---- 0xad     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eVertical"));

            enumVal = (Int32) eVal.eVerticalRotated;      // ---- 0xad     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eVerticalRotated"));

            operTag = (Byte) PCLXLOperators.eTag.SetColorTreatment;  // 0x58 //

            attrTagA = (Byte) PCLXLAttributes.eTag.AllObjectTypes;    // 0x1d // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eNoTreatment;          // 0x58 0x1d     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eNoTreatment"));

            enumVal = (Int32) eVal.eScreenMatch;          // 0x58 0x1d     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eScreenMatch"));

            enumVal = (Int32) eVal.eVivid;                // 0x58 0x1d     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eVivid"));

            attrTagA = (Byte) PCLXLAttributes.eTag.TextObjects;       // 0x1e // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eNoTreatment;          // 0x58 0x1e     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eNoTreatment"));

            enumVal = (Int32) eVal.eScreenMatch;          // 0x58 0x1e     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eScreenMatch"));

            enumVal = (Int32) eVal.eVivid;                // 0x58 0x1e     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eVivid"));

            attrTagA = (Byte) PCLXLAttributes.eTag.VectorObjects;     // 0x1f // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eNoTreatment;          // 0x58 0x1f     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eNoTreatment"));

            enumVal = (Int32) eVal.eScreenMatch;          // 0x58 0x1f     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eScreenMatch"));

            enumVal = (Int32) eVal.eVivid;                // 0x58 0x1f     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eVivid"));

            attrTagA = (Byte) PCLXLAttributes.eTag.RasterObjects;     // 0x20 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eNoTreatment;          // 0x58 0x20     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eNoTreatment"));

            enumVal = (Int32) eVal.eScreenMatch;          // 0x58 0x20     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eScreenMatch"));

            enumVal = (Int32) eVal.eVivid;                // 0x58 0x20     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eVivid"));

            operTag = (Byte) PCLXLOperators.eTag.SetHalftoneMethod;  // 0x6d //

            attrTagA = (Byte) PCLXLAttributes.eTag.AllObjectTypes;    // 0x1d // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eHighLPI;              // 0x6d 0x1d     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eHighLPI"));

            enumVal = (Int32) eVal.eMediumLPI;            // 0x6d 0x1d     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eMediumLPI"));

            enumVal = (Int32) eVal.eLowLPI;               // 0x6d 0x1d     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eLowLPI"));

            attrTagA = (Byte) PCLXLAttributes.eTag.TextObjects;       // 0x1e // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eHighLPI;              // 0x6d 0x1e     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eHighLPI"));

            enumVal = (Int32) eVal.eMediumLPI;            // 0x6d 0x1e     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eMediumLPI"));

            enumVal = (Int32) eVal.eLowLPI;               // 0x6d 0x1e     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eLowLPI"));

            attrTagA = (Byte) PCLXLAttributes.eTag.VectorObjects;     // 0x1f // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eHighLPI;              // 0x6d 0x1f     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eHighLPI"));

            enumVal = (Int32) eVal.eMediumLPI;            // 0x6d 0x1f     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eMediumLPI"));

            enumVal = (Int32) eVal.eLowLPI;               // 0x6d 0x1f     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eLowLPI"));

            attrTagA = (Byte) PCLXLAttributes.eTag.RasterObjects;     // 0x20 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eHighLPI;              // 0x6d 0x20     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eHighLPI"));

            enumVal = (Int32) eVal.eMediumLPI;            // 0x6d 0x20     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eMediumLPI"));

            enumVal = (Int32) eVal.eLowLPI;               // 0x6d 0x20     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eLowLPI"));

            operTag = (Byte) PCLXLOperators.eTag.SetNeutralAxis;     // 0x7e //

            attrTagA = (Byte) PCLXLAttributes.eTag.AllObjectTypes;    // 0x1d // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eTonerBlack;           // 0x7e 0x1d     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eTonerBlack"));

            enumVal = (Int32) eVal.eProcessBlack;         // 0x7e 0x1d     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eProcessBlack"));

            attrTagA = (Byte) PCLXLAttributes.eTag.TextObjects;       // 0x1e // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eTonerBlack;           // 0x7e 0x1e     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eTonerBlack"));

            enumVal = (Int32) eVal.eProcessBlack;         // 0x7e 0x1e     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eProcessBlack"));

            attrTagA = (Byte) PCLXLAttributes.eTag.VectorObjects;     // 0x1f // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eTonerBlack;           // 0x7e 0x1f     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eTonerBlack"));

            enumVal = (Int32) eVal.eProcessBlack;         // 0x7e 0x1f     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eProcessBlack"));

            attrTagA = (Byte) PCLXLAttributes.eTag.RasterObjects;     // 0x20 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eTonerBlack;           // 0x7e 0x20     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eTonerBlack"));

            enumVal = (Int32) eVal.eProcessBlack;         // 0x7e 0x20     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eProcessBlack"));

            operTag = (Byte) PCLXLOperators.eTag.SetColorTrapping;   // 0x92 //

            attrTagA = (Byte) PCLXLAttributes.eTag.AllObjectTypes;    // 0x1d // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eDisable;              // 0x92 0x1d     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eDisable"));

            enumVal = (Int32) eVal.eMax;                  // 0x92 0x1d     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eMax"));

            enumVal = (Int32) eVal.eNormal;               // 0x92 0x1d     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eNormal"));

            enumVal = (Int32) eVal.eLight;                // 0x92 0x1d     3 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eLight"));

            attrTagA = (Byte) PCLXLAttributes.eTag.TextObjects;       // 0x1e // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eDisable;              // 0x92 0x1e     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eDisable"));

            enumVal = (Int32) eVal.eMax;                  // 0x92 0x1e     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eMax"));

            enumVal = (Int32) eVal.eNormal;               // 0x92 0x1e     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eNormal"));

            enumVal = (Int32) eVal.eLight;                // 0x92 0x1e     3 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eLight"));

            attrTagA = (Byte) PCLXLAttributes.eTag.VectorObjects;     // 0x1f // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eDisable;              // 0x92 0x1f     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eDisable"));

            enumVal = (Int32) eVal.eMax;                  // 0x92 0x1f     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eMax"));

            enumVal = (Int32) eVal.eNormal;               // 0x92 0x1f     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eNormal"));

            enumVal = (Int32) eVal.eLight;                // 0x92 0x1f     3 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eLight"));


            attrTagA = (Byte) PCLXLAttributes.eTag.RasterObjects;     // 0x20 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eDisable;              // 0x92 0x20     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eDisable"));

            enumVal = (Int32) eVal.eMax;                  // 0x92 0x20     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eMax"));

            enumVal = (Int32) eVal.eNormal;               // 0x92 0x20     2 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eNormal"));

            enumVal = (Int32) eVal.eLight;                // 0x92 0x20     3 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eLight"));

            operTag = (Byte) PCLXLOperators.eTag.SetAdaptiveHalftoning; // 0x94 //

            attrTagA = (Byte) PCLXLAttributes.eTag.AllObjectTypes;    // 0x1d // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eDisable;              // 0x94 0x1d     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eDisable"));

            enumVal = (Int32) eVal.eEnable;               // 0x94 0x1d     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eEnable"));

            attrTagA = (Byte) PCLXLAttributes.eTag.TextObjects;       // 0x1e // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eDisable;              // 0x94 0x1e     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eDisable"));

            enumVal = (Int32) eVal.eEnable;               // 0x94 0x1e     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eEnable"));

            attrTagA = (Byte) PCLXLAttributes.eTag.VectorObjects;     // 0x1f // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eDisable;              // 0x94 0x1f     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eDisable"));

            enumVal = (Int32) eVal.eEnable;               // 0x94 0x1f     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eEnable"));

            attrTagA = (Byte) PCLXLAttributes.eTag.RasterObjects;     // 0x20 // 
            root = operTag.ToString ("X2") +
                   attrLen.ToString ("X2") +
                   attrTagA.ToString ("X2") +
                   attrTagB.ToString ("X2");

            enumVal = (Int32) eVal.eDisable;              // 0x94 0x20     0 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eDisable"));

            enumVal = (Int32) eVal.eEnable;               // 0x94 0x20     1 //
            _tags.Add(root + ":" + enumVal.ToString("X8"),
                new PCLXLAttrEnum(operTag, attrTagA, attrTagB, attrLen, enumVal,
                                  flagNone,
                                  "eEnable"));

            _tagCount = _tags.Count;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        //  p o p u l a t e T a b l e A d d S y m s e t s                     //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Add known symbol sets to the table of enumerations.                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void populateTableAddSymsets(Byte operTag,
                                                   Byte attrTagA,
                                                   Byte attrTagB,
                                                   Int32 attrLen,
                                                   String root)
        {
            const Boolean flagNone = false;

            Int32 ctSymsets;

            ctSymsets = PCLSymbolSets.getCount ();

            if (ctSymsets > 0)
            {
                Int32 enumVal;

                UInt16 kind1 = 0;
                UInt16 idNum = 0;

                String name = "";
                String id = "";

                Boolean presetType = false;

                for (Int32 i = 0; i < ctSymsets; i++)
                {
                    presetType =
                        PCLSymbolSets.getSymsetData (i,
                                                     ref kind1,
                                                     ref idNum,
                                                     ref name);

                    if (presetType)
                    {
                        id = PCLSymbolSets.translateKind1ToId (kind1);

                        enumVal = (Int32) kind1;           // ---- 0xaa     n //
                        _tags.Add (root + ":" + enumVal.ToString ("X8"),
                            new PCLXLAttrEnum (operTag, attrTagA, attrTagB,
                                               attrLen, enumVal,
                                               flagNone,
                                               kind1 +
                                               " (" + id + " = " + name + ")"));

                    }
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        //  r e s e t S t a t s C o u n t s                                   //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Reset counts of referenced tags.                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void resetStatsCounts()
        {
            PCLXLAttrEnum tag;

            _tagUnknown.resetStatistics ();

            foreach (KeyValuePair<String, PCLXLAttrEnum> kvp in _tags)
            {
                tag = kvp.Value;

                tag.resetStatistics ();
            }
        }
    }
}
