using System;
using System.Data;
using System.Reflection;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides the Prn Analyse 'save report' function.
    /// 
    /// © Chris Hutchinson 2010-2017
    /// 
    /// </summary>

    static class ToolPrnAnalyseReport
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const Int32 _maxSizeNameTag = 15;
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

        public static void generate(
            ToolPrnAnalyse.eInfoType indxInfoType,
			ReportCore.eRptFileFmt   rptFileFmt,
            DataTable                table,
            String                   prnFilename,
            Int64                    fileSize,
            Boolean                  flagOffsetHex,
            PrnParseOptions          options)
        {
            Object stream = null;
            Object writer = null;

            Boolean OK = false;

            Int32 reportSize;

            String fileExt;
            String saveFilename = null;

            if (rptFileFmt == ReportCore.eRptFileFmt.html)
                fileExt = "html";
            else if (rptFileFmt == ReportCore.eRptFileFmt.xml)
                fileExt = "xml";
            else
                fileExt = "txt";

            if (indxInfoType == ToolPrnAnalyse.eInfoType.Analysis)
            {
                saveFilename = prnFilename + "_analysis." + fileExt;

                OK = ReportCore.docOpen (rptFileFmt,
                                         ref saveFilename,
                                         ref stream,
                                         ref writer);

                if (OK)
                {
                    Int32 ctClrMapRowTypes = PrnParseRowTypes.getCount ();

                    Boolean useClr = options.FlagClrMapUseClr;

                    reportSize = table.Rows.Count;

                    if (useClr)
                    {
                        String[] rowClasses = new String[ctClrMapRowTypes];
                        String[] rowClrBack = new String[ctClrMapRowTypes];
                        String[] rowClrFore = new String[ctClrMapRowTypes];

                        getRowColourStyleData (options,
                                               ref rowClasses,
                                               ref rowClrBack,
                                               ref rowClrFore);

                        ReportCore.docInitialise (rptFileFmt, writer, true, false,
                                                  ctClrMapRowTypes, rowClasses,
                                                  rowClrBack, rowClrFore);
                    }
                    else
                    {
                        ReportCore.docInitialise (rptFileFmt, writer, true, false,
                                                  0, null,
                                                  null, null);
                    }

                    reportHeader (indxInfoType, rptFileFmt, writer,
                                  prnFilename, fileSize, reportSize);

                    reportBodyAnalysis (rptFileFmt, writer,
                                        table, flagOffsetHex);

                    ReportCore.docFinalise (rptFileFmt, writer);

                    ReportCore.docClose (rptFileFmt, stream, writer);
                }
            }
            else if (indxInfoType == ToolPrnAnalyse.eInfoType.Content)
            {
                saveFilename = prnFilename + "_content." + fileExt;

                OK = ReportCore.docOpen (rptFileFmt,
                                         ref saveFilename,
                                         ref stream,
                                         ref writer);
                if (OK)
                {
                    reportSize = table.Rows.Count;

                    ReportCore.docInitialise (rptFileFmt, writer, true, false,
                                              0, null,
                                              null, null);

                    reportHeader (indxInfoType, rptFileFmt, writer,
                                  prnFilename, fileSize, reportSize);

                    reportBodyContent (rptFileFmt, writer,
                                       table, flagOffsetHex);

                    ReportCore.docFinalise (rptFileFmt, writer);

                    ReportCore.docClose (rptFileFmt, stream, writer);
                }
            }
            else if (indxInfoType == ToolPrnAnalyse.eInfoType.Statistics)
            {
                saveFilename = prnFilename + "_statistics." + fileExt;

                OK = ReportCore.docOpen (rptFileFmt,
                                         ref saveFilename,
                                         ref stream,
                                         ref writer);
                if (OK)
                {
                    reportSize = table.Rows.Count;

                    ReportCore.docInitialise (rptFileFmt, writer, true, false,
                                              0, null,
                                              null, null);

                    reportHeader (indxInfoType, rptFileFmt, writer,
                                  prnFilename, fileSize, reportSize);

                    reportBodyStatistics (rptFileFmt, writer,
                                          table);

                    ReportCore.docFinalise (rptFileFmt, writer);

                    ReportCore.docClose (rptFileFmt, stream, writer);
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t R o w C o l o u r S t y l e D a t a                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Get references to colour style data for colour coded analysis.     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void getRowColourStyleData (
            PrnParseOptions options,
            ref String[] classes,
            ref String[] clrBack,
            ref String[] clrFore)
        {
            Int32 indxClrBack;
            Int32 indxClrFore;

            PropertyInfo[] stdClrsPropertyInfo = null;

            Boolean flagClrMapUseClr = false;

            PropertyInfo pInfoBack,
                         pInfoFore;

            Int32 ctClrMapRowTypes = PrnParseRowTypes.getCount ();
            Int32 ctClrMapStdClrs = 0;

            Int32[] indxClrMapBack = new Int32[ctClrMapRowTypes];
            Int32[] indxClrMapFore = new Int32[ctClrMapRowTypes];

            options.getOptClrMap (ref flagClrMapUseClr,
                                  ref indxClrMapBack,
                                  ref indxClrMapFore);

            options.getOptClrMapStdClrs (ref ctClrMapStdClrs,
                                         ref stdClrsPropertyInfo);

            //----------------------------------------------------------------//

            for (Int32 i = 0; i < ctClrMapRowTypes; i++)
            {
                String rowType =
                    Enum.GetName (typeof (PrnParseRowTypes.eType), i);

                indxClrBack = indxClrMapBack[i];
                indxClrFore = indxClrMapFore[i];

                pInfoBack = stdClrsPropertyInfo[indxClrBack] as PropertyInfo;
                pInfoFore = stdClrsPropertyInfo[indxClrFore] as PropertyInfo;

                classes[i] = rowType;
                clrBack[i] = pInfoBack.Name;
                clrFore[i] = pInfoFore.Name;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e p o r t B o d y A n a l y s i s                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write details of Analysis to report file.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void reportBodyAnalysis (
            ReportCore.eRptFileFmt rptFileFmt,
            Object                 writer,
            DataTable              table,
            Boolean                flagOffsetHex)
        {
            const Int32 colCt = 4;

            const String c0Name = PrnParseConstants.cRptA_colName_Offset;
            const String c1Name = PrnParseConstants.cRptA_colName_Type;
            const String c2Name = PrnParseConstants.cRptA_colName_Seq;
            const String c3Name = PrnParseConstants.cRptA_colName_Desc;

            const Int32 lc0 = PrnParseConstants.cRptA_colMax_Offset;
            const Int32 lc1 = PrnParseConstants.cRptA_colMax_Type;
            const Int32 lc2 = PrnParseConstants.cRptA_colMax_Seq;
            const Int32 lc3 = PrnParseConstants.cRptA_colMax_Desc;

            const String rtName = PrnParseConstants.cRptA_colName_RowType;

            String c0Hddr;

            String[] colHddrs;
            String[] colNames;
            Int32[] colSizes;

            Int32 ctItems;

            ctItems = table.Rows.Count;

            if (flagOffsetHex)
                c0Hddr = c0Name + ": hex";
            else
                c0Hddr = c0Name + ": dec";

            colHddrs = new String[colCt] { c0Hddr, c1Name, c2Name, c3Name };
            colNames = new String[colCt] { c0Name, c1Name, c2Name, c3Name };
            colSizes = new Int32[colCt] { lc0, lc1, lc2, lc3 };

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

                Int32 indxRowType = (Int32)row[rtName];

                String rowType = Enum.GetName
                                    (typeof (PrnParseRowTypes.eType),
                                     indxRowType);

                ReportCore.tableRowData (
                    writer, rptFileFmt,
                    ReportCore.eRptChkMarks.text,   // not used by this tool //
                    colCt, rowType,
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
        // r e p o r t B o d y C o n t e n t                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write details of Content to report file.                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void reportBodyContent (
            ReportCore.eRptFileFmt rptFileFmt,
            Object                 writer,
            DataTable              table,
            Boolean                flagOffsetHex)
        {
            const Int32 colCt = 3;

            const String c0Name = PrnParseConstants.cRptC_colName_Offset;
            const String c1Name = PrnParseConstants.cRptC_colName_Hex;
            const String c2Name = PrnParseConstants.cRptC_colName_Text;

            const Int32 lc0 = PrnParseConstants.cRptC_colMax_Offset;
            const Int32 lc1 = PrnParseConstants.cRptC_colMax_Hex;
            const Int32 lc2 = PrnParseConstants.cRptC_colMax_Text;

            String c0Hddr;

            String[] colHddrs;
            String[] colNames;
            Int32[] colSizes;

            Int32 ctItems;

            ctItems = table.Rows.Count;

            if (flagOffsetHex)
                c0Hddr = c0Name + ": hex";
            else
                c0Hddr = c0Name + ": dec";

            colHddrs = new String[colCt] { c0Hddr, c1Name, c2Name };
            colNames = new String[colCt] { c0Name, c1Name, c2Name };
            colSizes = new Int32[colCt] { lc0, lc1, lc2 };

            ctItems = table.Rows.Count;

            if (flagOffsetHex)
                c0Hddr = c0Name + ": hex";
            else
                c0Hddr = c0Name + ": dec";

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

                ReportCore.tableRowData (
                    writer, rptFileFmt,
                    ReportCore.eRptChkMarks.text,   // not used by this tool //
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
        // r e p o r t B o d y S t a t i s t i c s                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write details of Statistics to report file.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void reportBodyStatistics (
            ReportCore.eRptFileFmt rptFileFmt,
            Object                 writer,
            DataTable              table)
        {
            const Int32 colCt = 5;

            const String c0Name = PrnParseConstants.cRptS_colName_Seq;
            const String c1Name = PrnParseConstants.cRptS_colName_Desc;
            const String c2Name = PrnParseConstants.cRptS_colName_CtP;
            const String c3Name = PrnParseConstants.cRptS_colName_CtE;
            const String c4Name = PrnParseConstants.cRptS_colName_CtT;

            const Int32 lc0 = PrnParseConstants.cRptS_colMax_Seq;
            const Int32 lc1 = PrnParseConstants.cRptS_colMax_Desc;
            const Int32 lc2 = PrnParseConstants.cRptS_colMax_CtP;
            const Int32 lc3 = PrnParseConstants.cRptS_colMax_CtE;
            const Int32 lc4 = PrnParseConstants.cRptS_colMax_CtT;

            String[] colNames;
            Int32[] colSizes;

            Int32 ctItems;

            ctItems = table.Rows.Count;

            colNames = new String[colCt] { c0Name, c1Name, c2Name, c3Name, c4Name };
            colSizes = new Int32[colCt] { lc0, lc1, lc2, lc3, lc4 };

            //----------------------------------------------------------------//
            //                                                                //
            // Open the table and Write the column header text.               //
            //                                                                //
            //----------------------------------------------------------------//

            ReportCore.tableHddrData (writer, rptFileFmt, false,
                                  colCt, colNames, colSizes);

            //----------------------------------------------------------------//
            //                                                                //
            // Write the data rows.                                           //
            //                                                                //
            //----------------------------------------------------------------//

            for (Int32 i = 0; i < ctItems; i++)
            {
                DataRow row = table.Rows[i];

                ReportCore.tableRowData (
                    writer, rptFileFmt,
                    ReportCore.eRptChkMarks.text,   // not used by this tool //
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
        // r e p o r t H e a d e r                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write report header.                                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void reportHeader (
            ToolPrnAnalyse.eInfoType indxInfoType,
            ReportCore.eRptFileFmt   rptFileFmt,
            Object                   writer,
            String                   prnFilename,
            Int64                    fileSize,
            Int32                    reportSize)
        {
            Int32 maxLineLen = 0;

            String title = "";

            if (indxInfoType == ToolPrnAnalyse.eInfoType.Analysis)
            {
                title = "*** Prn Analysis ***";

                maxLineLen = PrnParseConstants.cRptA_colMax_Offset +
                             PrnParseConstants.cRptA_colMax_Type   +
                             PrnParseConstants.cRptA_colMax_Seq    +
                             PrnParseConstants.cRptA_colMax_Desc   +
                             (PrnParseConstants.cColSeparatorLen * 3) - 12;
            }
            else if (indxInfoType == ToolPrnAnalyse.eInfoType.Content)
            {
                title = "*** Prn Content ***";

                maxLineLen = PrnParseConstants.cRptC_colMax_Offset +
                             PrnParseConstants.cRptC_colMax_Hex    +
                             PrnParseConstants.cRptC_colMax_Text   +
                             (PrnParseConstants.cColSeparatorLen * 2) - 12;
            }
            else if (indxInfoType == ToolPrnAnalyse.eInfoType.Statistics)
            {
                title = "*** Prn Analysis Statistics ***";

                maxLineLen = PrnParseConstants.cRptS_colMax_Seq  +
                             PrnParseConstants.cRptS_colMax_Desc +
                             PrnParseConstants.cRptS_colMax_CtP  +
                             PrnParseConstants.cRptS_colMax_CtE  +
                             PrnParseConstants.cRptS_colMax_CtT  +
                             (PrnParseConstants.cColSeparatorLen * 4) - 12;
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Write out the title.                                           //
            //                                                                //
            //----------------------------------------------------------------//

            ReportCore.hddrTitle (writer, rptFileFmt, false, title);

            //----------------------------------------------------------------//
            //                                                                //
            // Open the table and Write Write out the date, time, input file  //
            // identity and size, and count of report rows.                   //
            //                                                                //
            //----------------------------------------------------------------//

            ReportCore.tableHddrPair (writer, rptFileFmt);

            ReportCore.tableRowPair (writer, rptFileFmt,
                                 "Date_time", DateTime.Now.ToString(),
                                 _colSpanNone, _colSpanNone,
                                 _maxSizeNameTag, maxLineLen,
                                 _flagNone, _flagNone, _flagNone);
            
            ReportCore.tableRowPair (writer, rptFileFmt,
                                 "Filename", prnFilename,
                                 _colSpanNone, _colSpanNone,
                                 _maxSizeNameTag, maxLineLen,
                                 _flagNone, _flagNone, _flagNone);

            ReportCore.tableRowPair (writer, rptFileFmt,
                                 "Filesize", fileSize.ToString () + " bytes",
                                 _colSpanNone, _colSpanNone,
                                 _maxSizeNameTag, maxLineLen,
                                 _flagNone, _flagNone, _flagNone);

            ReportCore.tableRowPair (writer, rptFileFmt,
                                 "Report_size", reportSize +
                                    " rows (excluding header and trailer lines)",
                                 _colSpanNone, _colSpanNone,
                                 _maxSizeNameTag, maxLineLen,
                                 _flagNone, _flagNone, _flagNone);

            ReportCore.tableClose (writer, rptFileFmt);
        }
    }
}
