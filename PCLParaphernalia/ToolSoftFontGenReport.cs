using System;
using System.Data;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides the Soft Font Generator 'save report' function.
    /// 
    /// © Chris Hutchinson 2012
    /// 
    /// </summary>

    static class ToolSoftFontGenReport
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const Int32 _maxSizeNameTag = 22;
        const Int32 _colSpanNone = -1;

        const Boolean _flagNone = false;
        const Boolean _flagBlankBefore = true;

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate the report.                                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void generate (ReportCore.eRptFileFmt rptFileFmt,
                                     ReportCore.eRptChkMarks rptChkMarks,
                                     DataTable tableDonor,
                                     DataTable tableMapping,
                                     DataTable tableTarget,
                                     DataTable tableChars,
                                     String    fontNameTTF,
                                     String    fontFilenameTTF,
                                     String    fontFilenamePCL )
        {
            Object stream = null;
            Object writer = null;

            Boolean OK = false;

            String fileExt;
            String saveFilename = null;

            if (rptFileFmt == ReportCore.eRptFileFmt.html)
                fileExt = "html";
            else if (rptFileFmt == ReportCore.eRptFileFmt.xml)
                fileExt = "xml";
            else
                fileExt = "txt";

            saveFilename = fontFilenamePCL + "_report." + fileExt;

            OK = ReportCore.docOpen (rptFileFmt,
                                     ref saveFilename,
                                     ref stream,
                                     ref writer);
            if (OK)
            {
                ReportCore.docInitialise (rptFileFmt, writer, true, false,
                                          0, null,
                                          null, null);

                reportHddr (rptFileFmt, writer,
                            fontNameTTF, fontFilenameTTF, fontFilenamePCL);

                reportHddrSub (rptFileFmt, writer, "Donor font details");

                reportBodyStd (rptFileFmt, rptChkMarks, writer, tableDonor);

                reportHddrSub (rptFileFmt, writer, "Mapping details");

                reportBodyStd (rptFileFmt, rptChkMarks, writer, tableMapping);

                reportHddrSub (rptFileFmt, writer, "Target font details");

                reportBodyStd (rptFileFmt, rptChkMarks, writer, tableTarget);

                reportHddrSub (rptFileFmt, writer, "Generated character details");

                reportBodyChars (rptFileFmt, rptChkMarks, writer, tableChars);

                ReportCore.docFinalise (rptFileFmt, writer);

                ReportCore.docClose (rptFileFmt, stream, writer);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e p o r t B o d y C h a r s                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write details of generated characters to report file.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void reportBodyChars (
            ReportCore.eRptFileFmt rptFileFmt,
            ReportCore.eRptChkMarks rptChkMarks,
            Object writer,
            DataTable table )
        {
            const Int32 colCt = 13;

            const String c0Name = "DecCode";
            const String c1Name = "HexCode";
            const String c2Name = "Unicode";
            const String c3Name = "Glyph";
            const String c4Name = "Abs";
            const String c5Name = "Prev";
            const String c6Name = "Comp";
            const String c7Name = "Depth";
            const String c8Name = "Width";
            const String c9Name = "LSB";
            const String c10Name = "Height";
            const String c11Name = "TSB";
            const String c12Name = "Length";

            const String c0Hddr = "DecCode";
            const String c1Hddr = "HexCode";
            const String c2Hddr = "Unicode";
            const String c3Hddr = "Glyph";
            const String c4Hddr = "Abs?";
            const String c5Hddr = "Prev?";
            const String c6Hddr = "Comp?";
            const String c7Hddr = "Depth";
            const String c8Hddr = "Width";
            const String c9Hddr = "LSB";
            const String c10Hddr = "Height";
            const String c11Hddr = "TSB";
            const String c12Hddr = "Length";

            const Int32 lc0 = 7;
            const Int32 lc1 = 7;
            const Int32 lc2 = 7;
            const Int32 lc3 = 5;
            const Int32 lc4 = 5;
            const Int32 lc5 = 5;
            const Int32 lc6 = 5;
            const Int32 lc7 = 5;
            const Int32 lc8 = 5;
            const Int32 lc9 = 6;
            const Int32 lc10 = 6;
            const Int32 lc11 = 6;
            const Int32 lc12 = 6;

            String[] colHddrs;
            String[] colNames;
            Int32[] colSizes;

            Int32 ctItems;

            ctItems = table.Rows.Count;

            colHddrs = new String[colCt] { c0Hddr, c1Hddr, c2Hddr, c3Hddr,
                                           c4Hddr, c5Hddr, c6Hddr, c7Hddr,
                                           c8Hddr, c9Hddr, c10Hddr, c11Hddr,
                                           c12Hddr };
            colNames = new String[colCt] { c0Name, c1Name, c2Name, c3Name,
                                           c4Name, c5Name, c6Name, c7Name,
                                           c8Name, c9Name, c10Name, c11Name,
                                           c12Name };
            colSizes = new Int32[colCt] { lc0, lc1, lc2, lc3,
                                          lc4, lc5, lc6, lc7,
                                          lc8, lc9, lc10, lc11,
                                          lc12};

            //----------------------------------------------------------------//
            //                                                                //
            // Open the table and Write the column header text.               //
            //                                                                //
            //----------------------------------------------------------------//

            ReportCore.tableHddrData (writer, rptFileFmt, false,
                                      colCt, colHddrs, colSizes);

            //----------------------------------------------------------------//
            //                                                                //
            // Write the data rows.                                           //
            //                                                                //
            //----------------------------------------------------------------//

            for (Int32 i = 0; i < ctItems; i++)
            {
                DataRow row = table.Rows[i];

                ReportCore.tableRowData (writer, rptFileFmt, rptChkMarks,
                                         colCt, null,
                                         row, colNames, colSizes);
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Write any required end tags.                                   //
            //                                                                //
            //----------------------------------------------------------------//

            ReportCore.tableClose (writer, rptFileFmt);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e p o r t B o d y S t d                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write details of specified two-column table to report file.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void reportBodyStd (
            ReportCore.eRptFileFmt rptFileFmt,
            ReportCore.eRptChkMarks rptChkMarks,
            Object writer,
            DataTable table)
        {
            const Int32 colCt = 2;

            const String c0Name = "Name";
            const String c1Name = "Value";

            const Int32 lc0 = 21;
            const Int32 lc1 = 57;

            String[] colHddrs;
            String[] colNames;
            Int32[] colSizes;

            Int32 ctItems;

            ctItems = table.Rows.Count;

            colHddrs = new String[colCt] { c0Name, c1Name };
            colNames = new String[colCt] { c0Name, c1Name };
            colSizes = new Int32[colCt] { lc0, lc1 };

            ctItems = table.Rows.Count;

            //----------------------------------------------------------------//
            //                                                                //
            // Open the table and Write the column header text.               //
            //                                                                //
            //----------------------------------------------------------------//

            ReportCore.tableHddrData (writer, rptFileFmt, false,
                                  colCt, colHddrs, colSizes);

            //----------------------------------------------------------------//
            //                                                                //
            // Write the data rows.                                           //
            //                                                                //
            //----------------------------------------------------------------//

            for (Int32 i = 0; i < ctItems; i++)
            {
                DataRow row = table.Rows[i];

                ReportCore.tableRowData (writer, rptFileFmt, rptChkMarks,
                                         colCt, null,
                                         row, colNames, colSizes);
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Write any required end tags.                                   //
            //                                                                //
            //----------------------------------------------------------------//

            ReportCore.tableClose (writer, rptFileFmt);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e p o r t H d d r                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write main report header.                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void reportHddr (ReportCore.eRptFileFmt rptFileFmt,
                                        Object writer,
                                        String fontNameTTF,
                                        String fontFilenameTTF,
                                        String fontFilenamePCL)
        {
            Int32 maxLineLen = 80;

            String title = "*** Soft Font Generator ***";

            //----------------------------------------------------------------//
            //                                                                //
            // Write out the title.                                           //
            //                                                                //
            //----------------------------------------------------------------//

            ReportCore.hddrTitle (writer, rptFileFmt, false, title);

            //----------------------------------------------------------------//
            //                                                                //
            // Write out the date, time, input file identity and size, and    //
            // count of report rows.                                          //
            //                                                                //
            //----------------------------------------------------------------//

            ReportCore.tableHddrPair (writer, rptFileFmt);

            ReportCore.tableRowPair (writer, rptFileFmt,
                                 "Date_time", DateTime.Now.ToString (),
                                 _colSpanNone, _colSpanNone,
                                 _maxSizeNameTag, maxLineLen,
                                 _flagNone, _flagNone, _flagNone);

            ReportCore.tableRowPair (writer, rptFileFmt,
                                 "Target_PCL_font_file", fontFilenamePCL,
                                 _colSpanNone, _colSpanNone,
                                 _maxSizeNameTag, maxLineLen,
                                 _flagNone, _flagNone, _flagNone);

            ReportCore.tableRowPair (writer, rptFileFmt,
                                 "Donor_TTF_name", fontNameTTF,
                                 _colSpanNone, _colSpanNone,
                                 _maxSizeNameTag, maxLineLen,
                                 _flagNone, _flagNone, _flagNone);

            ReportCore.tableRowPair (writer, rptFileFmt,
                                 "Donor_TTF_file", fontFilenameTTF,
                                 _colSpanNone, _colSpanNone,
                                 _maxSizeNameTag, maxLineLen,
                                 _flagNone, _flagNone, _flagNone);

            ReportCore.tableClose (writer, rptFileFmt);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e p o r t H d d r S u b                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write sub report header.                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void reportHddrSub (ReportCore.eRptFileFmt rptFileFmt,
                                           Object writer,
                                           String subHead)
        {
            //----------------------------------------------------------------//
            //                                                                //
            // Write out the sub-header.                                      //
            //                                                                //
            //----------------------------------------------------------------//

            ReportCore.hddrTitle (writer, rptFileFmt, true, subHead);
        }
    }
}
