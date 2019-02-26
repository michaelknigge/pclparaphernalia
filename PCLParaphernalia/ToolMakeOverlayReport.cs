using System;
using System.Data;
using System.Reflection;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides the Make Overlay 'save report' function.
    /// 
    /// © Chris Hutchinson 2012
    /// 
    /// </summary>

    static class ToolMakeOverlayReport
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
            ReportCore.eRptFileFmt   rptFileFmt,
            DataTable                table,
            String                   prnFilename,
            String                   ovlFilename,
            Boolean                  flagOffsetHex,
            PrnParseOptions          options)
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

            saveFilename = ovlFilename + "_report." + fileExt;

            OK = ReportCore.docOpen (rptFileFmt,
                                     ref saveFilename,
                                     ref stream,
                                     ref writer);

            if (OK)
            {
                Int32 ctClrMapRowTypes = PrnParseRowTypes.getCount ();

                Boolean useClr = options.FlagClrMapUseClr;

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

                reportHeader (rptFileFmt, writer,
                              prnFilename, ovlFilename);

                reportBody (rptFileFmt, writer,
                            table, flagOffsetHex);

                ReportCore.docFinalise (rptFileFmt, writer);

                ReportCore.docClose (rptFileFmt, stream, writer);
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
        // r e p o r t B o d y                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write details of process to report file.                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void reportBody (ReportCore.eRptFileFmt rptFileFmt,
                                        Object writer,
                                        DataTable table,
                                        Boolean flagOffsetHex)
        {
            const Int32 colCt = 5;

            const String c0Name = PrnParseConstants.cRptA_colName_Action;
            const String c1Name = PrnParseConstants.cRptA_colName_Offset;
            const String c2Name = PrnParseConstants.cRptA_colName_Type;
            const String c3Name = PrnParseConstants.cRptA_colName_Seq;
            const String c4Name = PrnParseConstants.cRptA_colName_Desc;

            const Int32 lc0 = PrnParseConstants.cRptA_colMax_Action;
            const Int32 lc1 = PrnParseConstants.cRptA_colMax_Offset;
            const Int32 lc2 = PrnParseConstants.cRptA_colMax_Type;
            const Int32 lc3 = PrnParseConstants.cRptA_colMax_Seq;
            const Int32 lc4 = PrnParseConstants.cRptA_colMax_Desc;

            const String rtName = PrnParseConstants.cRptA_colName_RowType;

            String c1Hddr;

            String[] colHddrs;
            String[] colNames;
            Int32[] colSizes;

            Int32 ctItems;

            ctItems = table.Rows.Count;

            if (flagOffsetHex)
                c1Hddr = c1Name + ": hex";
            else
                c1Hddr = c1Name + ": dec";

            colHddrs = new String[colCt] { c0Name, c1Hddr, c2Name, c3Name, c4Name };
            colNames = new String[colCt] { c0Name, c1Name, c2Name, c3Name, c4Name };
            colSizes = new Int32[colCt] { lc0, lc1, lc2, lc3, lc4 };

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
        // r e p o r t H e a d e r                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write report header.                                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void reportHeader (
            ReportCore.eRptFileFmt rptFileFmt,
            Object                 writer,
            String                 prnFilename,
            String                 ovlFilename)
        {
            Int32 maxLineLen = 0;

            String title = "";

            title = "*** Make Overlay report ***:";

            maxLineLen = PrnParseConstants.cRptA_colMax_Action +
                         PrnParseConstants.cRptA_colMax_Offset +
                         PrnParseConstants.cRptA_colMax_Type +
                         PrnParseConstants.cRptA_colMax_Seq +
                         PrnParseConstants.cRptA_colMax_Desc +
                         (PrnParseConstants.cColSeparatorLen * 4) - 15;

            //----------------------------------------------------------------//
            //                                                                //
            // Write out the title.                                           //
            //                                                                //
            //----------------------------------------------------------------//

            ReportCore.hddrTitle (writer, rptFileFmt, false, title);

            //----------------------------------------------------------------//
            //                                                                //
            // Write out the date, time, input file identity, and             //
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
                                 "Print_file", prnFilename,
                                 _colSpanNone, _colSpanNone,
                                 _maxSizeNameTag, maxLineLen,
                                 _flagNone, _flagNone, _flagNone);

            ReportCore.tableRowPair (writer, rptFileFmt,
                                 "Overlay_file", ovlFilename,
                                 _colSpanNone, _colSpanNone,
                                 _maxSizeNameTag, maxLineLen,
                                 _flagNone, _flagNone, _flagNone);

            ReportCore.tableClose (writer, rptFileFmt);
        }
    }
}