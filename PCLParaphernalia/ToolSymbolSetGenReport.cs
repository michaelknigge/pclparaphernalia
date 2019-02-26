using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides the Symbol Set Generator 'save report' function.
    /// 
    /// © Chris Hutchinson 2013
    /// 
    /// </summary>

    static class ToolSymbolSetGenReport
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

        private const Int32 cCodePointUnused = 65535;
        private const Int32 cCodePointC1Min = 0x80;
        private const Int32 cCodePointC1Max = 0x9f;

        const Int32 lm0 = 21;
        const Int32 lm1 = 57;

        const Int32 lcDec  = 5;
        const Int32 lcHex  = 4;
        const Int32 lrDec  = 5;
        const Int32 lrHex  = 4;

        const Int32 lSep   = 1;

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
                                     String symSetFilename,
                                     UInt16 symSetNo,
                                     UInt16 [] symSetMap,
                                     UInt16 codeMin,
                                     UInt16 codeMax,
                                     UInt16 codeCt,
                                     UInt64 charCollReq,
                                     Boolean flagIgnoreC0,
                                     Boolean flagIgnoreC1,
                                     Boolean flagMapHex,
                                     PCLSymSetTypes.eIndex symSetType)
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

            saveFilename = symSetFilename + "_report." + fileExt;

            OK = ReportCore.docOpen (rptFileFmt,
                                     ref saveFilename,
                                     ref stream,
                                     ref writer);

            if (OK)
            {
                ReportCore.docInitialise (rptFileFmt, writer, true, false,
                                          0, null,
                                          null, null);

                reportHddr (rptFileFmt, writer, symSetFilename);

                reportBodyMain (rptFileFmt, writer, symSetNo,
                                codeMin, codeMax, codeCt, charCollReq,
                                flagIgnoreC0, flagIgnoreC1, flagMapHex,
                                symSetType);

                reportBodyMap (rptFileFmt, writer, symSetMap, 
                               codeMin, codeMax,
                               flagIgnoreC0, flagIgnoreC1, flagMapHex);

                ReportCore.docFinalise (rptFileFmt, writer);

                ReportCore.docClose (rptFileFmt, stream, writer);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e p o r t B o d y M a i n                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write report header.                                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void reportBodyMain (
            ReportCore.eRptFileFmt rptFileFmt,
            Object writer,
        //  String symSetFilename,
            UInt16 symSetNo,
            UInt16 codeMin,
            UInt16 codeMax,
            UInt16 codeCt,
            UInt64 charCollReq,
            Boolean flagIgnoreC0,
            Boolean flagIgnoreC1,
            Boolean flagMapHex,
            PCLSymSetTypes.eIndex symSetType)
        {
            const Int32 maxLineLen = 80;        // ***************** constant elsewhere ???????????????

            //----------------------------------------------------------------//
            //                                                                //
            // Write out the title.                                           //
            //                                                                //
            //----------------------------------------------------------------//

            ReportCore.hddrTitle (writer, rptFileFmt, true,
                                  "Symbol set details:");

            //----------------------------------------------------------------//
            //                                                                //
            // Write out the symbol set basic details.                        //
            //                                                                //
            //----------------------------------------------------------------//

            ReportCore.tableHddrPair (writer, rptFileFmt);

            ReportCore.tableRowPair (writer, rptFileFmt,
                                 "SymSetNo", symSetNo.ToString (),
                                 _colSpanNone, _colSpanNone,
                                 _maxSizeNameTag, maxLineLen,
                                 _flagNone, _flagNone, _flagNone);

            ReportCore.tableRowPair (writer, rptFileFmt,
                                 "SymSetId",
                                 PCLSymbolSets.translateKind1ToId (symSetNo).ToString (),
                                 _colSpanNone, _colSpanNone,
                                 _maxSizeNameTag, maxLineLen,
                                 _flagNone, _flagNone, _flagNone);

            ReportCore.tableRowPair (writer, rptFileFmt,
                                 "IgnoreC0Codes",
                                 (flagIgnoreC0 ? "true" : "false"),
                                 _colSpanNone, _colSpanNone,
                                 _maxSizeNameTag, maxLineLen,
                                 _flagNone, _flagNone, _flagNone);

            ReportCore.tableRowPair (writer, rptFileFmt,
                                 "IgnoreC1Codes",
                                 (flagIgnoreC1 ? "true" : "false"),
                                 _colSpanNone, _colSpanNone,
                                 _maxSizeNameTag, maxLineLen,
                                 _flagNone, _flagNone, _flagNone);

            ReportCore.tableRowPair (writer, rptFileFmt,
                                 "FirstCode",
                                 (flagMapHex ? "0x" + codeMin.ToString ("x4")
                                             : codeMin.ToString ()),
                                 _colSpanNone, _colSpanNone,
                                 _maxSizeNameTag, maxLineLen,
                                 _flagNone, _flagNone, _flagNone);

            ReportCore.tableRowPair (writer, rptFileFmt,
                                 "Lastcode",
                                 (flagMapHex ? "0x" + codeMax.ToString ("x4")
                                             : codeMax.ToString ()),
                                 _colSpanNone, _colSpanNone,
                                 _maxSizeNameTag, maxLineLen,
                                 _flagNone, _flagNone, _flagNone);

            ReportCore.tableRowPair (writer, rptFileFmt,
                                 "CharCount",
                                 (flagMapHex ? "0x" + codeCt.ToString ("x4")
                                             : codeCt.ToString ()),
                                 _colSpanNone, _colSpanNone,
                                 _maxSizeNameTag, maxLineLen,
                                 _flagNone, _flagNone, _flagNone);

            ReportCore.tableRowPair (writer, rptFileFmt,
                                 "CharReqBits",
                                 "0x" + charCollReq.ToString ("x16"),
                                 _colSpanNone, _colSpanNone,
                                 _maxSizeNameTag, maxLineLen,
                                 _flagNone, _flagNone, _flagNone);

            ReportCore.tableClose (writer, rptFileFmt);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e p o r t B o d y M a p                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write details of mapping to report file.                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void reportBodyMap (
            ReportCore.eRptFileFmt rptFileFmt,
            Object writer,
            UInt16[] symSetMap,
            UInt16 codeMin,
            UInt16 codeMax,
            Boolean flagIgnoreC0,
            Boolean flagIgnoreC1,
            Boolean flagMapHex)
        {
            const Int32 maxLineLen = 80;        // ***************** constant elsewhere ???????????????

            const Int32 lcDec = 5;
            const Int32 lcHex = 4;
            const Int32 lrDec = 5;
            const Int32 lrHex = 4;

            const Int32 colCt = 17;

            Int32 lcCol,
                  lrHddr;

            String fmtHddr,
                   fmtVal;

            Int32 mapIndx,
                  rowIndx;

            String[] colHddrs = new String [colCt];
            String[] colNames = new String [colCt];
            Int32[] colSizes = new Int32 [colCt];

            Int32 ctItems;

            ctItems = symSetMap.Length;

            //----------------------------------------------------------------//
            //                                                                //
            // Write out the header.                                          //
            //                                                                //
            //----------------------------------------------------------------//

            ReportCore.hddrTitle (writer, rptFileFmt, true,
                                  "Mapping detail:");

            ReportCore.tableHddrPair (writer, rptFileFmt);

            ReportCore.tableRowPair (writer, rptFileFmt,
                                 "Format",
                                 (flagMapHex ? "hexadecimal" : "decimal"),
                                 _colSpanNone, _colSpanNone,
                                 _maxSizeNameTag, maxLineLen,
                                 _flagNone, _flagNone, _flagNone);

            ReportCore.tableClose (writer, rptFileFmt);

            //----------------------------------------------------------------//
            //                                                                //
            // Open the table and write the column header text.               //
            //                                                                //
            //----------------------------------------------------------------//

            if (flagMapHex)
            {
                lcCol = lcHex;
                lrHddr = lrHex;

                fmtHddr = "x4";
                fmtVal = "x4";

                colSizes[0] = lrHex;
                colNames[0] = "row";
                colHddrs[0] = "";

                for (Int32 i = 1; i < colCt; i++)
                {
                    colSizes[i] = lcHex;
                    colNames[i] = "col" + (i - 1).ToString ("D2");
                    colHddrs[i] = "_" + (i - 1).ToString ("x");
                }
            }
            else
            { 
                lcCol = lcDec;
                lrHddr = lrDec;

                fmtHddr = "";
                fmtVal = "";

                colSizes[0] = lrDec;
                colNames[0] = "row";
                colHddrs[0] = "";

                for (Int32 i = 1; i < colCt; i++)
                {
                    colSizes[i] = lcDec;
                    colNames[i] = "col" + (i - 1).ToString ("D2");
                    colHddrs[i] = "+" + (i - 1).ToString ("d");
                }
            }

            ReportCore.tableHddrData (writer, rptFileFmt, true,
                                      colCt, colHddrs, colSizes);

            //----------------------------------------------------------------//
            //                                                                //
            // Write the data rows.                                           //
            //                                                                //
            //----------------------------------------------------------------//

            Int32 colCtData = colCt - 1;

            mapIndx = 0;
            rowIndx = codeMin / colCtData;

            for (Int32 i = rowIndx; mapIndx < codeMax; i++)
            {
                String[] rowData = new String [colCt];

                rowIndx = (i * colCtData);

                if (flagMapHex)
                {
                    rowData[0] = (rowIndx.ToString (fmtHddr).
                                    Substring (0, 3) + "_").
                                    PadLeft (lrHddr, ' ');
                }
                else
                {
                    rowData[0] = rowIndx.ToString (fmtHddr).
                                    PadLeft (lrHddr, ' ');
                }

                for (Int32 j = 0; j < colCtData; j++)
                {
                    String val;

                    mapIndx = rowIndx + j;

                    if ((mapIndx < codeMin) || (mapIndx > codeMax))
                    {
                        val = " ".PadLeft (lcCol, ' ');
                    }
                    else if ((flagIgnoreC1) &&
                             ((mapIndx >= cCodePointC1Min) &&
                              (mapIndx <= cCodePointC1Max)))
                    {
                        val = cCodePointUnused.
                                ToString (fmtVal).PadLeft (lcCol, ' ');
                    }
                    else
                    {
                        val = symSetMap[mapIndx].
                                ToString (fmtVal).PadLeft (lcCol, ' ');
                    }

                    rowData[j + 1] = val;
                }

                ReportCore.tableRowText (writer, rptFileFmt, colCt,
                                     rowData, colNames, colSizes);
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
                                        String symSetFilename)
        {
            Int32 maxLineLen = 80;

            String title = "*** Symbol Set Generator ***";

            //----------------------------------------------------------------//
            //                                                                //
            // Write out the title.                                           //
            //                                                                //
            //----------------------------------------------------------------//

            ReportCore.hddrTitle (writer, rptFileFmt, false, title);

            //----------------------------------------------------------------//
            //                                                                //
            // Open the table and Write Write out the date, time and input    //
            // file identity.                                                 //
            //                                                                //
            //----------------------------------------------------------------//

            ReportCore.tableHddrPair (writer, rptFileFmt);

            ReportCore.tableRowPair (writer, rptFileFmt,
                                 "Date_time", DateTime.Now.ToString (),
                                 _colSpanNone, _colSpanNone,
                                 _maxSizeNameTag, maxLineLen,
                                 _flagNone, _flagNone, _flagNone);

            ReportCore.tableRowPair (writer, rptFileFmt,
                                 "Symbol set file", symSetFilename,
                                 _colSpanNone, _colSpanNone,
                                 _maxSizeNameTag, maxLineLen,
                                 _flagNone, _flagNone, _flagNone);

            ReportCore.tableClose (writer, rptFileFmt);
        }
    }
}
