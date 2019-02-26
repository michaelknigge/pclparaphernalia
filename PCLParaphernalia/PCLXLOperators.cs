using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides details of PCL XL Operator tags.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class PCLXLOperators
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public enum eEmbedDataType : byte
        {
            None = 0,
            Stream,
            PassThrough,
            FontHeader,
            FontChar,
            DitherMatrix,
            Points,
            Image,
            RasterPattern,
            Scan
        }

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // PCLXL Operator tags.                                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public enum eTag : byte
        {
            ArcPath                     = 0x91,
            BeginChar                   = 0x52,
            BeginFontHeader             = 0x4f,
            BeginImage                  = 0xb0,
            BeginPage                   = 0x43,
            BeginRastPattern            = 0xb3,
            BeginScan                   = 0xb6,
            BeginSession                = 0x41,
            BeginStream                 = 0x5b,
            BezierPath                  = 0x93,
            BezierRelPath               = 0x95,
            Chord                       = 0x96,
            ChordPath                   = 0x97,
            CloseDataSource             = 0x49,
            CloseSubPath                = 0x84,
            Comment                     = 0x47,
            EchoComment                 = 0x4a,
            Ellipse                     = 0x98,
            EllipsePath                 = 0x99,
            EndChar                     = 0x54,
            EndFontHeader               = 0x51,
            EndImage                    = 0xb2,
            EndPage                     = 0x44,
            EndRastPattern              = 0xb5,
            EndScan                     = 0xb8,
            EndSession                  = 0x42,
            EndStream                   = 0x5d,
            ExecStream                  = 0x5e,
            LinePath                    = 0x9b,
            LineRelPath                 = 0x9d,
            NewPath                     = 0x85,
            OpenDataSource              = 0x48,
            PaintPath                   = 0x86,
            PassThrough                 = 0xbf,
            Pie                         = 0x9e,
            PiePath                     = 0x9f,
            PopGS                       = 0x60,
            PushGS                      = 0x61,
            Query                       = 0x4b,
            ReadChar                    = 0x53,
            ReadFontHeader              = 0x50,
            ReadImage                   = 0xb1,
            ReadRastPattern             = 0xb4,
            ReadStream                  = 0x5c,
            Rectangle                   = 0xa0,
            RectanglePath               = 0xa1,
            RemoveFont                  = 0x55,
            RemoveStream                = 0x5f,
            RoundRectangle              = 0xa2,
            RoundRectanglePath          = 0xa3,
            ScanLineRel                 = 0xb9,
            SetAdaptiveHalftoning       = 0x94,
            SetBrushSource              = 0x63,
            SetCharAngle                = 0x64,
            SetCharAttributes           = 0x56,
            SetCharBoldValue            = 0x7d,
            SetCharScale                = 0x65,
            SetCharShear                = 0x66,
            SetCharSubMode              = 0x81,
            SetClipIntersect            = 0x67,
            SetClipMode                 = 0x7f,
            SetClipRectangle            = 0x68,
            SetClipReplace              = 0x62,
            SetClipToPage               = 0x69,
            SetColorSpace               = 0x6a,
            SetColorTrapping            = 0x92,
            SetColorTreatment           = 0x58,
            SetCursor                   = 0x6b,
            SetCursorRel                = 0x6c,
            SetDefaultGS                = 0x57,
            SetFillMode                 = 0x6e,
            SetFont                     = 0x6f,
            SetHalftoneMethod           = 0x6d,
            SetLineCap                  = 0x71,
            SetLineDash                 = 0x70,
            SetLineJoin                 = 0x72,
            SetMiterLimit               = 0x73,
            SetNeutralAxis              = 0x7e,
            SetPageDefaultCTM           = 0x74,
            SetPageOrigin               = 0x75,
            SetPageRotation             = 0x76,
            SetPageScale                = 0x77,
            SetPathToClip               = 0x80,
            SetPatternTxMode            = 0x78,
            SetPenSource                = 0x79,
            SetPenWidth                 = 0x7a,
            SetROP                      = 0x7b,
            SetSourceTxMode             = 0x7c,
            SystemText                  = 0xaa,
            Text                        = 0xa8,
            TextPath                    = 0xa9,
            VendorUnique                = 0x46
        }

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static SortedList<Byte, PCLXLOperator> _tags =
            new SortedList<Byte, PCLXLOperator>();

        private static PCLXLOperator _tagUnknown;

        private static Int32 _tagCount;
        
        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L X L O p e r a t o r s                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        static PCLXLOperators()
        {
            populateTable();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h e c k T a g                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Searches the PCL XL Operator tag table for a matching entry.       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean checkTag (
            Byte tagToCheck,
            ref Boolean flagEndSession,
            ref Boolean flagReserved,
            ref PCLXLOperators.eEmbedDataType embedDataType,
            ref PrnParseConstants.eOvlAct makeOvlAct,
            ref String description)
        {
            Boolean seqKnown;

            PCLXLOperator tag;

            if (_tags.IndexOfKey (tagToCheck) != -1)
            {
                seqKnown = true;
                tag = _tags[tagToCheck];
            }
            else
            {
                seqKnown = false;  
                tag = _tagUnknown; 
            }

            tag.getDetails (ref flagEndSession,
                            ref flagReserved,
                            ref embedDataType,
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

            foreach (KeyValuePair<Byte, PCLXLOperator> kvp in _tags)
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
            row[1] = "_____________________";
            row[2] = "";
            row[3] = "";
            row[4] = "";

            table.Rows.Add (row);

            row = table.NewRow ();

            row[0] = "";
            row[1] = "PCL XL Operator tags:";
            row[2] = "";
            row[3] = "";
            row[4] = "";

            table.Rows.Add (row);

            row = table.NewRow ();

            row[0] = "";
            row[1] = "¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯";
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
        // Display list of Operator tags.                                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 displayTags(DataGrid grid,
                                        Boolean incResTags)
        {
            Int32 count = 0;

            Boolean tagReserved;

            foreach (KeyValuePair<Byte, PCLXLOperator> kvp in _tags)
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
        // Return description for specified Operator tag.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getDesc(Byte tag)
        {
            return _tags[tag].Description;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n c r e m e n t S t a t s C o u n t                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Increment the relevant statistics count for the DataType tag.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void incrementStatsCount (Byte  tagByte,
                                                Int32 level)
        {
            PCLXLOperator tag;

            if (_tags.IndexOfKey (tagByte) != -1)
                tag = _tags[tagByte];
            else
                tag = _tagUnknown;

            tag.incrementStatisticsCount (level);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p o p u l a t e T a b l e                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Populate the table of Operator tags.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void populateTable()
        {
            const Boolean flagNone       = false;
            const Boolean flagReserved   = true;
            const Boolean flagEndSession = true;

            Byte tag;
            
            tag = 0x20;                                              // ?    //
            _tagUnknown = 
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "*** Unknown tag ***");

            tag = (Byte) eTag.BeginSession;                          // 0x41 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.Replace_0x77, 
                                     "BeginSession"));

            tag = (Byte) eTag.EndSession;                            // 0x42 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagEndSession, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.Remove, 
                                     "EndSession"));

            tag = (Byte) eTag.BeginPage;                             // 0x43 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.PageBegin, 
                                     "BeginPage"));

            tag = (Byte) eTag.EndPage;                               // 0x44 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.PageEnd, 
                                     "EndPage"));

            tag = 0x45;                                               // 0x45 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = (Byte) eTag.VendorUnique;                          // 0x46 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "VendorUnique"));

            tag = (Byte) eTag.Comment;                               // 0x47 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "Comment"));

            tag = (Byte) eTag.OpenDataSource;                        // 0x48 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.Remove, 
                                     "OpenDataSource"));

            tag = (Byte) eTag.CloseDataSource;                       // 0x49 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.Remove,
                                     "CloseDataSource"));

            tag = (Byte) eTag.EchoComment;                           // 0x4a //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "EchoComment"));

            tag = (Byte) eTag.Query;                                 // 0x4b //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "Query"));

            tag = 0x4c;                                               // 0x4c //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = 0x4d;                                               // 0x4d //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = 0x4e;                                               // 0x4e //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = (Byte) eTag.BeginFontHeader;                       // 0x4f //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "BeginFontHeader"));

            tag = (Byte) eTag.ReadFontHeader;                        // 0x50 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.FontHeader,
                                     PrnParseConstants.eOvlAct.None, 
                                     "ReadFontHeader"));

            tag = (Byte) eTag.EndFontHeader;                         // 0x51 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "EndFontHeader"));

            tag = (Byte) eTag.BeginChar;                             // 0x52 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "BeginChar"));

            tag = (Byte) eTag.ReadChar;                              // 0x53 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.FontChar,
                                     PrnParseConstants.eOvlAct.None, 
                                     "ReadChar"));

            tag = (Byte) eTag.EndChar;                               // 0x54 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "EndChar"));

            tag = (Byte) eTag.RemoveFont;                            // 0x55 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "RemoveFont"));

            tag = (Byte) eTag.SetCharAttributes;                     // 0x56 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetCharAttributes"));

            tag = (Byte) eTag.SetDefaultGS;                          // 0x57 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetDefaultGS"));

            tag = (Byte) eTag.SetColorTreatment;                     // 0x58 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetColorTreatment"));

            tag = 0x59;                                               // 0x59 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = 0x5a;                                               // 0x5a //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = (Byte) eTag.BeginStream;                           // 0x5b //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.Illegal, 
                                     "BeginStream"));

            tag = (Byte) eTag.ReadStream;                            // 0x5c //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.Stream,
                                     PrnParseConstants.eOvlAct.Illegal, 
                                     "ReadStream"));

            tag = (Byte) eTag.EndStream;                             // 0x5d //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.Illegal, 
                                     "EndStream"));

            tag = (Byte) eTag.ExecStream;                            // 0x5e //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "ExecStream"));

            tag = (Byte) eTag.RemoveStream;                          // 0x5f //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "RemoveStream"));

            tag = (Byte) eTag.PopGS;                                 // 0x60 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "PopGS"));

            tag = (Byte) eTag.PushGS;                                // 0x61 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "PushGS"));

            tag = (Byte) eTag.SetClipReplace;                        // 0x62 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetClipReplace"));

            tag = (Byte) eTag.SetBrushSource;                        // 0x63 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetBrushSource"));

            tag = (Byte) eTag.SetCharAngle;                          // 0x64 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetCharAngle"));

            tag = (Byte) eTag.SetCharScale;                          // 0x65 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetCharScale"));

            tag = (Byte) eTag.SetCharShear;                          // 0x66 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetCharShear"));

            tag = (Byte) eTag.SetClipIntersect;                      // 0x67 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetClipIntersect"));

            tag = (Byte) eTag.SetClipRectangle;                      // 0x68 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetClipRectangle"));

            tag = (Byte) eTag.SetClipToPage;                         // 0x69 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetClipToPage"));

            tag = (Byte) eTag.SetColorSpace;                         // 0x6a //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetColorSpace"));

            tag = (Byte) eTag.SetCursor;                             // 0x6b //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetCursor"));

            tag = (Byte) eTag.SetCursorRel;                          // 0x6c //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetCursorRel"));

            tag = (Byte) eTag.SetHalftoneMethod;                     // 0x6d //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.DitherMatrix,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetHalftoneMethod"));

            tag = (Byte) eTag.SetFillMode;                           // 0x6e //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetFillMode"));

            tag = (Byte) eTag.SetFont;                               // 0x6f //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetFont"));

            tag = (Byte) eTag.SetLineDash;                           // 0x70 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetLineDash"));

            tag = (Byte) eTag.SetLineCap;                            // 0x71 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetLineCap"));

            tag = (Byte) eTag.SetLineJoin;                           // 0x72 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetLineJoin"));

            tag = (Byte) eTag.SetMiterLimit;                         // 0x73 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetMiterLimit"));

            tag = (Byte) eTag.SetPageDefaultCTM;                     // 0x74 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.Remove, 
                                     "SetPageDefaultCTM"));

            tag = (Byte) eTag.SetPageOrigin;                         // 0x75 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetPageOrigin"));

            tag = (Byte) eTag.SetPageRotation;                       // 0x76 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetPageRotation"));

            tag = (Byte) eTag.SetPageScale;                          // 0x77 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetPageScale"));

            tag = (Byte) eTag.SetPatternTxMode;                      // 0x78 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetPatternTxMode"));

            tag = (Byte) eTag.SetPenSource;                          // 0x79 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetPenSource"));

            tag = (Byte) eTag.SetPenWidth;                           // 0x7a //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetPenWidth"));

            tag = (Byte) eTag.SetROP;                                // 0x7b //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetROP"));

            tag = (Byte) eTag.SetSourceTxMode;                       // 0x7c //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetSourceTxMode"));

            tag = (Byte) eTag.SetCharBoldValue;                      // 0x7d //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetCharBoldValue"));

            tag = (Byte) eTag.SetNeutralAxis;                        // 0x7e //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetNeutralAxis"));

            tag = (Byte) eTag.SetClipMode;                           // 0x7f //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetClipMode"));

            tag = (Byte) eTag.SetPathToClip;                         // 0x80 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetPathToClip"));

            tag = (Byte) eTag.SetCharSubMode;                        // 0x81 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetCharSubMode"));

            tag = 0x82;                                               // 0x82 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = 0x83;                                               // 0x83 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = (Byte) eTag.CloseSubPath;                          // 0x84 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "CloseSubPath"));

            tag = (Byte) eTag.NewPath;                               // 0x85 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "NewPath"));

            tag = (Byte) eTag.PaintPath;                             // 0x86 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "PaintPath"));

            tag = 0x87;                                               // 0x87 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = 0x88;                                               // 0x88 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = 0x89;                                               // 0x89 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = 0x8a;                                               // 0x8a //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = 0x8b;                                               // 0x8b //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = 0x8c;                                               // 0x8c //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = 0x8d;                                               // 0x8d //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = 0x8e;                                               // 0x8e //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = 0x8f;                                               // 0x8f //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = 0x90;                                               // 0x90 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = (Byte) eTag.ArcPath;                               // 0x91 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "ArcPath"));

            tag = (Byte) eTag.SetColorTrapping;                      // 0x92 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetColorTrapping"));

            tag = (Byte) eTag.BezierPath;                            // 0x93 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.Points,
                                     PrnParseConstants.eOvlAct.None, 
                                     "BezierPath"));

            tag = (Byte) eTag.SetAdaptiveHalftoning;                 // 0x94 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SetAdaptiveHalftoning"));

            tag = (Byte) eTag.BezierRelPath;                         // 0x95 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.Points,
                                     PrnParseConstants.eOvlAct.None, 
                                     "BezierRelPath"));

            tag = (Byte) eTag.Chord;                                 // 0x96 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "Chord"));

            tag = (Byte) eTag.ChordPath;                             // 0x97 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "ChordPath"));

            tag = (Byte) eTag.Ellipse;                               // 0x98 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "Ellipse"));

            tag = (Byte) eTag.EllipsePath;                           // 0x99 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "EllipsePath"));

            tag = 0x9a;                                               // 0x9a //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = (Byte) eTag.LinePath;                              // 0x9b //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.Points,
                                     PrnParseConstants.eOvlAct.None, 
                                     "LinePath"));

            tag = 0x9c;                                               // 0x9c //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = (Byte) eTag.LineRelPath;                           // 0x9d //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.Points,
                                     PrnParseConstants.eOvlAct.None, 
                                     "LineRelPath"));

            tag = (Byte) eTag.Pie;                                   // 0x9e //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "Pie"));

            tag = (Byte) eTag.PiePath;                               // 0x9f //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "PiePath"));

            tag = (Byte) eTag.Rectangle;                             // 0xa0 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "Rectangle"));

            tag = (Byte) eTag.RectanglePath;                         // 0xa1 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "RectanglePath"));

            tag = (Byte) eTag.RoundRectangle;                        // 0xa2 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "RoundRectangle"));

            tag = (Byte) eTag.RoundRectanglePath;                    // 0xa3 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "RoundRectanglePath"));

            tag = 0xa4;                                               // 0xa4 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = 0xa5;                                               // 0xa5 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = 0xa6;                                               // 0xa6 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = 0xa7;                                               // 0xa7 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = (Byte) eTag.Text;                                  // 0xa8 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "Text"));

            tag = (Byte) eTag.TextPath;                              // 0xa9 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "TextPath"));

            tag = (Byte) eTag.SystemText;                            // 0xaa //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "SystemText"));

            tag = 0xab;                                               // 0xab //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = 0xac;                                               // 0xac //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = 0xad;                                               // 0xad //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = 0xae;                                               // 0xae //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = 0xaf;                                               // 0xaf //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = (Byte) eTag.BeginImage;                            // 0xb0 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "BeginImage"));

            tag = (Byte) eTag.ReadImage;                             // 0xb1 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.Image,
                                     PrnParseConstants.eOvlAct.None, 
                                     "ReadImage"));

            tag = (Byte) eTag.EndImage;                              // 0xb2 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "EndImage"));

            tag = (Byte) eTag.BeginRastPattern;                      // 0xb3 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "BeginRastPattern"));

            tag = (Byte) eTag.ReadRastPattern;                       // 0xb4 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.RasterPattern,
                                     PrnParseConstants.eOvlAct.None, 
                                     "ReadRastPattern"));

            tag = (Byte) eTag.EndRastPattern;                        // 0xb5 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "EndRastPattern"));

            tag = (Byte) eTag.BeginScan;                             // 0xb6 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.Scan,
                                     PrnParseConstants.eOvlAct.None, 
                                     "BeginScan"));

            tag = 0xb7;                                               // 0xb7 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagReserved,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "* Reserved *"));

            tag = (Byte) eTag.EndScan;                               // 0xb8 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "EndScan"));

            tag = (Byte) eTag.ScanLineRel;                           // 0xb9 //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.None,
                                     PrnParseConstants.eOvlAct.None, 
                                     "ScanLineRel"));

            tag = (Byte) eTag.PassThrough;                           // 0xbf //
            _tags.Add(tag,
                new PCLXLOperator(tag, flagNone, flagNone,
                                     eEmbedDataType.PassThrough,
                                     PrnParseConstants.eOvlAct.None, 
                                     "PassThrough"));

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
            PCLXLOperator tag;

            _tagUnknown.resetStatistics ();

            foreach (KeyValuePair<Byte, PCLXLOperator> kvp in _tags)
            {
                tag = kvp.Value;

                tag.resetStatistics ();
            }
        }
    }
}
