using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides details of PCL XL Attribute tags.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class PCLXLAttributes
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // PCLXL Attribute tags.                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public enum eTag : byte
        {
            AllObjectTypes              = 0x1d,
            ArcDirection                = 0x41,
            BitmapCharScaling           = 0xae,
            BlockByteLength             = 0x6f,
            BlockHeight                 = 0x63,
            BoundingBox                 = 0x42,
            CharAngle                   = 0xa1,
            CharBoldValue               = 0xb1,
            CharCode                    = 0xa2,
            CharDataSize                = 0xa3,
            CharScale                   = 0xa4,
            CharShear                   = 0xa5,
            CharSize                    = 0xa6,
            CharSubModeArray            = 0xac,
            ClipMode                    = 0x54,
            ClipRegion                  = 0x53,
            ColorDepth                  = 0x62,
            ColorDepthArray             = 0x61,
            ColorMapping                = 0x64,
            ColorSpace                  = 0x03,
            ColorTreatment              = 0x78,
            CommentData                 = 0x81,
            CompressMode                = 0x65,
            ContentOrientation          = 0x76,
            ControlPoint1               = 0x51,
            ControlPoint2               = 0x52,
            CustomMediaSize             = 0x2f,
            CustomMediaSizeUnits        = 0x30,
            DashOffset                  = 0x43,
            DataOrg                     = 0x82,
            DestinationBox              = 0x66,
            DestinationSize             = 0x67,
            DeviceMatrix                = 0x21,
            DitherMatrixDataType        = 0x22,
            DitherMatrixDepth           = 0x33,
            DitherMatrixSize            = 0x32,
            DitherOrigin                = 0x23,
            DuplexPageMode              = 0x35,
            DuplexPageSide              = 0x36,
            EllipseDimension            = 0x44,
            EnableDiagnostics           = 0xa0,
            EndPoint                    = 0x45,
            ErrorReport                 = 0x8f,
            FeedOrientation             = 0x77,
            FillMode                    = 0x46,
            FontFormat                  = 0xa9,
            FontHeaderLength            = 0xa7,
            FontName                    = 0xa8,
            GrayLevel                   = 0x09,
            LineCapStyle                = 0x47,
            LineDashStyle               = 0x4a,
            LineJoinStyle               = 0x48,
            Measure                     = 0x86,
            MediaDestination            = 0x24,
            MediaSize                   = 0x25,
            MediaSource                 = 0x26,
            MediaType                   = 0x27,
            MiterLength                 = 0x49,
            NewDestinationSize          = 0x0d,
            NullBrush                   = 0x04,
            NullPen                     = 0x05,
            NumberOfPoints              = 0x4d,
            NumberOfScanLines           = 0x73,
            Orientation                 = 0x28,
            PadBytesMultiple            = 0x6e,
            PageAngle                   = 0x29,
            PageCopies                  = 0x31,
            PageOrigin                  = 0x2a,
            PageScale                   = 0x2b,
            PaletteData                 = 0x06,
            PaletteDepth                = 0x02,
            PaletteIndex                = 0x07,
            PatternDefineID             = 0x69,
            PatternOrigin               = 0x0c,
            PatternPersistence          = 0x68,
            PatternSelectID             = 0x08,
            PCLSelectFont               = 0x8d,
            PenWidth                    = 0x4b,
            Point                       = 0x4c,
            PointType                   = 0x50,
            PrimaryArray                = 0x0e,
            PrimaryDepth                = 0x0f,
            PrintableArea               = 0x74,
            QueryKey                    = 0x8a,
            RasterObjects               = 0x20,
            RGBColor                    = 0x0b,
            ROP3                        = 0x2c,
            SimplexPageMode             = 0x34,
            SolidLine                   = 0x4e,
            SourceHeight                = 0x6b,
            SourceType                  = 0x88,
            SourceWidth                 = 0x6c,
            StartLine                   = 0x6d,
            StartPoint                  = 0x4f,
            StreamDataLength            = 0x8c,
            StreamName                  = 0x8b,
            SymbolSet                   = 0xaa,
            TextData                    = 0xab,
            TextObjects                 = 0x1e,
            TumbleMode                  = 0x75,
            TxMode                      = 0x2d,
            UnitsPerMeasure             = 0x89,
            VectorObjects               = 0x1f,
            VUExtension                 = 0x91,
            VUDataLength                = 0x92,
            VUAttr1                     = 0x93,
            VUAttr2                     = 0x94,
            VUAttr3                     = 0x95,
            VUAttr4                     = 0x96,
            VUAttr5                     = 0x97,
            VUAttr6                     = 0x98,
            VUAttr7                     = 0x99,
            VUAttr8                     = 0x9a,
            VUAttr9                     = 0x9b,
            VUAttr10                    = 0x9c,
            VUAttr11                    = 0x9d,
            VUAttr12                    = 0x9e,
            WritingMode                 = 0xad,
            XSpacingData                = 0xaf,
            YSpacingData                = 0xb0
        }

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static SortedList<Int32, PCLXLAttribute> _tags =
            new SortedList<Int32, PCLXLAttribute>();

        private static PCLXLAttribute _tagUnknown;

        private static Int32 _tagCount;
        
        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L X L A t t r i b u t e s                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        static PCLXLAttributes()
        {
            populateTable();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h e c k T a g                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Searches the PCL XL Attribute tag table for a matching entry.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean checkTag (
            Int32       tagLen1,
            Byte        tagA,
            Byte        tagB,
            ref Boolean flagReserved,
            ref Boolean flagAttrEnum,
            ref Boolean flagOperEnum,
            ref Boolean flagUbyteTxt,
            ref Boolean flagUintTxt,
            ref Boolean flagValIsLen,
            ref Boolean flagValIsPCL,
            ref PrnParseConstants.eActPCLXL actionType,
            ref PrnParseConstants.eOvlAct makeOvlAct,
            ref String  description)
        {
            Boolean seqKnown;

            PCLXLAttribute tag;
            
            Int32 key;

            key = key = (((tagLen1 * 256) + tagA) * 256) + tagB;

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
            
            tag.getDetails (ref flagReserved,
                            ref flagAttrEnum,
                            ref flagOperEnum,
                            ref flagUbyteTxt,
                            ref flagUintTxt,
                            ref flagValIsLen,
                            ref flagValIsPCL,
                            ref actionType,
                            ref makeOvlAct,
                            ref description);

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

        public static void displayStatsCounts(DataTable table,
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

                row[0] = _tagUnknown.Tag;
                row[1] = _tagUnknown.Description;
                row[2] = _tagUnknown.StatsCtParent;
                row[3] = _tagUnknown.StatsCtChild;
                row[4] = _tagUnknown.StatsCtTotal;

                table.Rows.Add (row);
            }

            //----------------------------------------------------------------//

            foreach (KeyValuePair<Int32, PCLXLAttribute> kvp in _tags)
            {
                displaySeq = true;

                count = kvp.Value.StatsCtTotal;

                if (count == 0)
                {
                    if (incUsedSeqsOnly)
                        displaySeq = false;
                    else if ((excUnusedResTags) &&
                             (kvp.Value.FlagReserved == true))
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

                    row[0] = kvp.Value.Tag;
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
            row[1] = "______________________";
            row[2] = "";
            row[3] = "";
            row[4] = "";

            table.Rows.Add (row);

            row = table.NewRow ();

            row[0] = "";
            row[1] = "PCL XL Attribute tags:";
            row[2] = "";
            row[3] = "";
            row[4] = "";

            table.Rows.Add (row);

            row = table.NewRow ();

            row[0] = "";
            row[1] = "¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯";
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
        // Display list of Attribute tags.                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 displayTags(DataGrid grid,
                                        Boolean  incResTags)
        {
            Int32 count = 0;

            Boolean tagReserved;

            foreach (KeyValuePair<Int32, PCLXLAttribute> kvp
                in _tags)
            {
                tagReserved = kvp.Value.FlagReserved;

                if ((incResTags == true) ||
                    ((incResTags == false) && (!tagReserved)))
                {
                    count++;
                    grid.Items.Add(kvp.Value);
                }
            }

            return count;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t D e s c                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return description for specified Attribute tag.                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getDesc(Byte tagA,
                                     Byte tagB,
                                     Int32 tagLen)
        {
            Int32 key = (((tagLen * 256) + tagA) * 256) + tagB;

            return _tags[key].Description;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n c r e m e n t S t a t s C o u n t s                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Increment the relevant statistics count for the Attribute tag.     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void incrementStatsCount (Int32 tagLen,
                                                Byte  tagByteA,
                                                Byte  tagByteB,
                                                Int32 level)
        {
            PCLXLAttribute tag;

            Int32 key;

            key = key = (((tagLen * 256) + tagByteA) * 256) + tagByteB;

            if (_tags.IndexOfKey (key) != -1)
                tag = _tags[key];
            else
                tag = _tagUnknown;

            tag.incrementStatisticsCount (level);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p o p u l a t e T a b l e                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Populate the table of Attribute tags.                              //
        //                                                                    //
        // As at Class/Revision 3.0, all tags are 1-byte; no 2-byte tags have //
        // yet been defined.                                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void populateTable()
        {
            const Boolean flagNone     = false;
            const Boolean flagReserved = true;
            const Boolean flagAttrEnum = true;
            const Boolean flagOperEnum = true;
            const Boolean flagUbyteTxt = true;
            const Boolean flagUintTxt  = true;
            const Boolean flagValIsLen = true;
            const Boolean flagValIsPCL = true;

            Int32 tagLen1 = 1;
       //   Int32 tagLen2 = 2; // no 2-byte tags yet defined

            Int32 key;

            Byte tagA;
            Byte tagB = 0x00;

            tagA = 0x20;                                              // ?    //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tagUnknown =
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,  
                                   "*** Unknown tag ***");

            tagA = (Byte) eTag.PaletteDepth;                          // 0x02 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "PaletteDepth"));

            tagA = (Byte) eTag.ColorSpace;                            // 0x03 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "ColorSpace"));

            tagA = (Byte) eTag.NullBrush;                             // 0x04 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "NullBrush"));

            tagA = (Byte) eTag.NullPen;                               // 0x05 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "NullPen"));

            tagA = (Byte) eTag.PaletteData;                           // 0x06 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "PaletteData"));

            tagA = (Byte) eTag.PaletteIndex;                          // 0x07 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "PaletteIndex"));

            tagA = (Byte) eTag.PatternSelectID;                       // 0x08 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "PatternSelectID"));

            tagA = (Byte) eTag.GrayLevel;                             // 0x09 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "GrayLevel"));

            tagA = (Byte) eTag.RGBColor;                              // 0x0b //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "RGBColor"));

            tagA = (Byte) eTag.PatternOrigin;                         // 0x0c //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "PatternOrigin"));

            tagA = (Byte) eTag.NewDestinationSize;                    // 0x0d //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "NewDestinationSize"));

            tagA = (Byte) eTag.PrimaryArray;                          // 0x0e //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "PrimaryArray"));

            tagA = (Byte) eTag.PrimaryDepth;                          // 0x0f //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "PrimaryDepth"));

            tagA = (Byte) eTag.AllObjectTypes;                        // 0x1d //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagOperEnum,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "AllObjectTypes"));

            tagA = (Byte) eTag.TextObjects;                           // 0x1e //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagOperEnum,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "TextObjects"));

            tagA = (Byte) eTag.VectorObjects;                         // 0x1f //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagOperEnum,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "VectorObjects"));

            tagA = (Byte) eTag.RasterObjects;                         // 0x20 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagOperEnum,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "RasterObjects"));

            tagA = (Byte) eTag.DeviceMatrix;                          // 0x21 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "DeviceMatrix"));

            tagA = (Byte) eTag.DitherMatrixDataType;                  // 0x22 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "DitherMatrixDataType"));

            tagA = (Byte) eTag.DitherOrigin;                          // 0x23 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "DitherOrigin"));

            tagA = (Byte) eTag.MediaDestination;                      // 0x24 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "MediaDestination"));

            tagA = (Byte) eTag.MediaSize;                             // 0x25 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagUbyteTxt, flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "MediaSize"));

            tagA = (Byte) eTag.MediaSource;                           // 0x26 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "MediaSource"));

            tagA = (Byte) eTag.MediaType;                             // 0x27 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagUbyteTxt, flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "MediaType"));

            tagA = (Byte) eTag.Orientation;                           // 0x28 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "Orientation"));

            tagA = (Byte) eTag.PageAngle;                             // 0x29 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "PageAngle"));

            tagA = (Byte) eTag.PageOrigin;                            // 0x2a //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "PageOrigin"));

            tagA = (Byte) eTag.PageScale;                             // 0x2b //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "PageScale"));

            tagA = (Byte) eTag.ROP3;                                  // 0x2c //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "ROP3"));

            tagA = (Byte) eTag.TxMode;                                // 0x2d //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "TxMode"));


            tagA = (Byte) eTag.CustomMediaSize;                       // 0x2f //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "CustomMediaSize"));

            tagA = (Byte) eTag.CustomMediaSizeUnits;                  // 0x30 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "CustomMediaSizeUnits"));

            tagA = (Byte) eTag.PageCopies;                            // 0x31 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "PageCopies"));

            tagA = (Byte) eTag.DitherMatrixSize;                      // 0x32 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "DitherMatrixSize"));

            tagA = (Byte) eTag.DitherMatrixDepth;                     // 0x33 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "DitherMatrixDepth"));

            tagA = (Byte) eTag.SimplexPageMode;                       // 0x34 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "SimplexPageMode"));

            tagA = (Byte) eTag.DuplexPageMode;                        // 0x35 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "DuplexPageMode"));

            tagA = (Byte) eTag.DuplexPageSide;                        // 0x36 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "DuplexPageSide"));

            tagA = (Byte) eTag.ArcDirection;                          // 0x41 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "ArcDirection"));

            tagA = (Byte) eTag.BoundingBox;                           // 0x42 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "BoundingBox"));

            tagA = (Byte) eTag.DashOffset;                            // 0x43 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "DashOffset"));

            tagA = (Byte) eTag.EllipseDimension;                      // 0x44 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "EllipseDimension"));

            tagA = (Byte) eTag.EndPoint;                              // 0x45 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "EndPoint"));

            tagA = (Byte) eTag.FillMode;                              // 0x46 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "FillMode"));

            tagA = (Byte) eTag.LineCapStyle;                          // 0x47 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "LineCapStyle"));

            tagA = (Byte) eTag.LineJoinStyle;                         // 0x48 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "LineJoinStyle"));

            tagA = (Byte) eTag.MiterLength;                           // 0x49 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "MiterLength"));

            tagA = (Byte) eTag.LineDashStyle;                         // 0x4a //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "LineDashStyle"));

            tagA = (Byte) eTag.PenWidth;                              // 0x4b //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "PenWidth"));

            tagA = (Byte) eTag.Point;                                 // 0x4c //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "Point"));

            tagA = (Byte) eTag.NumberOfPoints;                        // 0x4d //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "NumberOfPoints"));

            tagA = (Byte) eTag.SolidLine;                             // 0x4e //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "SolidLine"));

            tagA = (Byte) eTag.StartPoint;                            // 0x4f //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "StartPoint"));

            tagA = (Byte) eTag.PointType;                             // 0x50 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "PointType"));

            tagA = (Byte) eTag.ControlPoint1;                         // 0x51 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "ControlPoint1"));

            tagA = (Byte) eTag.ControlPoint2;                         // 0x52 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "ControlPoint2"));

            tagA = (Byte) eTag.ClipRegion;                            // 0x53 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "ClipRegion"));

            tagA = (Byte) eTag.ClipMode;                              // 0x54 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "ClipMode"));

            tagA = (Byte) eTag.ColorDepthArray;                       // 0x61 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "ColorDepthArray"));

            tagA = (Byte) eTag.ColorDepth;                            // 0x62 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "ColorDepth"));

            tagA = (Byte) eTag.BlockHeight;                           // 0x63 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "BlockHeight"));

            tagA = (Byte) eTag.ColorMapping;                          // 0x64 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "ColorMapping"));

            tagA = (Byte) eTag.CompressMode;                          // 0x65 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "CompressMode"));

            tagA = (Byte) eTag.DestinationBox;                        // 0x66 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "DestinationBox"));

            tagA = (Byte) eTag.DestinationSize;                       // 0x67 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "DestinationSize"));

            tagA = (Byte) eTag.PatternPersistence;                    // 0x68 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "PatternPersistence"));

            tagA = (Byte) eTag.PatternDefineID;                       // 0x69 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "PatternDefineID"));

            tagA = (Byte) eTag.SourceHeight;                          // 0x6b //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "SourceHeight"));

            tagA = (Byte) eTag.SourceWidth;                           // 0x6c //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "SourceWidth"));

            tagA = (Byte) eTag.StartLine;                             // 0x6d //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "StartLine"));

            tagA = (Byte) eTag.PadBytesMultiple;                      // 0x6e //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "PadBytesMultiple"));

            tagA = (Byte) eTag.BlockByteLength;                       // 0x6f //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "BlockByteLength"));

            tagA = (Byte) eTag.NumberOfScanLines;                     // 0x73 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "NumberOfScanLines"));

            tagA = (Byte) eTag.PrintableArea;                         // 0x74 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "PrintableArea"));

            tagA = (Byte) eTag.TumbleMode;                            // 0x75 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "TumbleMode"));

            tagA = (Byte) eTag.ContentOrientation;                    // 0x76 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "ContentOrientation"));

            tagA = (Byte) eTag.FeedOrientation;                       // 0x77 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "FeedOrientation"));

            tagA = (Byte) eTag.ColorTreatment;                        // 0x78 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "ColorTreatment"));

            tagA = (Byte) eTag.CommentData;                           // 0x81 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagUbyteTxt, flagUintTxt,  flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "CommentData"));

            tagA = (Byte) eTag.DataOrg;                               // 0x82 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "DataOrg"));

            tagA = (Byte) eTag.Measure;                               // 0x86 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.Measure,
                                   PrnParseConstants.eOvlAct.None,
                                   "Measure"));

            tagA = (Byte) eTag.SourceType;                            // 0x88 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "SourceType"));

            tagA = (Byte) eTag.UnitsPerMeasure;                       // 0x89 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.UnitsPerMeasure,
                                   PrnParseConstants.eOvlAct.None,
                                   "UnitsPerMeasure"));

            tagA = (Byte) eTag.QueryKey;                              // 0x8a //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "QueryKey"));

            tagA = (Byte) eTag.StreamName;                            // 0x8b //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagUbyteTxt, flagUintTxt,  flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "StreamName"));

            tagA = (Byte) eTag.StreamDataLength;                      // 0x8c //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "StreamDataLength"));

            tagA = (Byte) eTag.PCLSelectFont;                         // 0x8d //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagUbyteTxt, flagNone,     flagNone,     flagValIsPCL,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "PCLSelectFont"));

            tagA = (Byte) eTag.ErrorReport;                           // 0x8f //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.Remove,
                                   "ErrorReport"));

            tagA = (Byte) eTag.VUExtension;                           // 0x91 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "VUExtension"));

            tagA = (Byte) eTag.VUDataLength;                          // 0x92 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagValIsLen, flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "VUDataLength"));

            tagA = (Byte) eTag.VUAttr1;                               // 0x93 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "VUAttr1"));

            tagA = (Byte) eTag.VUAttr2;                               // 0x94 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "VUAttr2"));

            tagA = (Byte) eTag.VUAttr3;                               // 0x95 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "VUAttr3"));

            tagA = (Byte) eTag.VUAttr4;                               // 0x96 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "VUAttr4"));

            tagA = (Byte) eTag.VUAttr5;                               // 0x97 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "VUAttr5"));

            tagA = (Byte) eTag.VUAttr6;                               // 0x98 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "VUAttr6"));

            tagA = (Byte) eTag.VUAttr7;                               // 0x99 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "VUAttr7"));

            tagA = (Byte) eTag.VUAttr8;                               // 0x9a //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "VUAttr8"));

            tagA = (Byte) eTag.VUAttr9;                               // 0x9b //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "VUAttr9"));

            tagA = (Byte) eTag.VUAttr10;                              // 0x9c //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "VUAttr10"));

            tagA = (Byte) eTag.VUAttr11;                              // 0x9d //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "VUAttr11"));

            tagA = (Byte) eTag.VUAttr12;                              // 0x9e //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "VUAttr12"));

            tagA = 0x9f;                                               // 0x9f //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagReserved, flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "* Reserved *"));

            tagA = (Byte) eTag.EnableDiagnostics;                     // 0xa0 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "EnableDiagnostics"));

            tagA = (Byte) eTag.CharAngle;                             // 0xa1 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "CharAngle"));

            tagA = (Byte) eTag.CharCode;                              // 0xa2 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "CharCode"));

            tagA = (Byte) eTag.CharDataSize;                          // 0xa3 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "CharDataSize"));

            tagA = (Byte) eTag.CharScale;                             // 0xa4 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "CharScale"));

            tagA = (Byte) eTag.CharShear;                             // 0xa5 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "CharShear"));

            tagA = (Byte) eTag.CharSize;                              // 0xa6 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.CharSize,
                                   PrnParseConstants.eOvlAct.None, 
                                   "CharSize"));

            tagA = (Byte) eTag.FontHeaderLength;                      // 0xa7 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "FontHeaderLength"));

            tagA = (Byte) eTag.FontName;                              // 0xa8 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagUbyteTxt, flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "FontName"));

            tagA = (Byte) eTag.FontFormat;                            // 0xa9 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None,
                                   "FontFormat"));

            tagA = (Byte) eTag.SymbolSet;                             // 0xaa //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "SymbolSet"));

            tagA = (Byte) eTag.TextData;                              // 0xab //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagUbyteTxt, flagUintTxt,  flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "TextData"));

            tagA = (Byte) eTag.CharSubModeArray;                      // 0xac //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "CharSubModeArray"));

            tagA = (Byte) eTag.WritingMode;                           // 0xad //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagAttrEnum, flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "WritingMode"));

            tagA = (Byte) eTag.BitmapCharScaling;                     // 0xae //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "BitmapCharScaling"));

            tagA = (Byte) eTag.XSpacingData;                          // 0xaf //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "XSpacingData"));

            tagA = (Byte) eTag.YSpacingData;                          // 0xb0 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "YSpacingData"));

            tagA = (Byte) eTag.CharBoldValue;                         // 0xb1 //
            key = (((tagLen1 * 256) + tagA) * 256) + tagB;
            _tags.Add (key,
                new PCLXLAttribute(tagLen1, tagA, tagB,
                                   flagNone,     flagNone,     flagNone,
                                   flagNone,     flagNone,     flagNone,     flagNone,
                                   PrnParseConstants.eActPCLXL.None,
                                   PrnParseConstants.eOvlAct.None, 
                                   "CharBoldValue"));


            _tagCount = _tags.Count;
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
            PCLXLAttribute tag;

            _tagUnknown.resetStatistics ();

            foreach (KeyValuePair<Int32, PCLXLAttribute> kvp in _tags)
            {
                tag = kvp.Value;

                tag.resetStatistics ();
            }
        }
    }
}
