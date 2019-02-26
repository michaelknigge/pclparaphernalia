using System;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides the PDLData 'save report' function.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class ToolPrintLangReport
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
        const Boolean _flagBlankAfter = true;
        const Boolean _flagNameAsHddr = true;

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
        // Generate the PDL Data report.                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void generate(
            Int32                        indxInfoType,
            ReportCore.eRptFileFmt       rptFileFmt,
            ReportCore.eRptChkMarks      rptChkMarks,
            DataGrid                     dgSeq,
            ref String                   saveFilename,
            Boolean                      flagPCLSeqControl,
            Boolean                      flagPCLSeqSimple,
            Boolean                      flagPCLSeqComplex,
            Boolean                      flagPCLOptObsolete,
            Boolean                      flagPCLOptDiscrete,
            Boolean                      flagPCLXLTagDataType,
            Boolean                      flagPCLXLTagAttribute,
            Boolean                      flagPCLXLTagOperator,
            Boolean                      flagPCLXLTagAttrDef,
            Boolean                      flagPCLXLTagEmbedDataLen,
            Boolean                      flagPCLXLTagWhitespace,
            Boolean                      flagPCLXLOptReserved,
            Boolean                      flagPMLTagDataType,
            Boolean                      flagPMLTagAction,
            Boolean                      flagPMLTagOutcome,
            Boolean                      flagSymSetList,
            Boolean                      flagSymSetMap,
            Boolean                      flagOptRptWrap,
            ToolPrintLang.eSymSetMapType symSetMapType)
        {
            Object stream = null;
            Object writer = null;

            Boolean OK;

            String saveFolder = null;
            String fileExt;

            ToolCommonData.eToolSubIds infoType =
                (ToolCommonData.eToolSubIds)indxInfoType;

            ToolCommonFunctions.getFolderName (saveFilename,
                                               ref saveFolder);

            if (rptFileFmt == ReportCore.eRptFileFmt.html)
                fileExt = "html";
            else if (rptFileFmt == ReportCore.eRptFileFmt.xml)
                fileExt = "xml";
            else
                fileExt = "txt";

            saveFilename = saveFolder +
                           "\\PDLData_" + infoType.ToString () +
                           "." + fileExt;

            OK = ReportCore.docOpen (rptFileFmt,
                                     ref saveFilename,
                                     ref stream,
                                     ref writer);

            if (OK)
            {
                ReportCore.docInitialise (rptFileFmt, writer, true, false,
                                          0, null,
                                          null, null);
                reportHeader (infoType,
                              rptFileFmt,
                              writer,
                              dgSeq,
                              flagPCLSeqControl,
                              flagPCLSeqSimple,
                              flagPCLSeqComplex,
                              flagPCLOptObsolete,
                              flagPCLOptDiscrete,
                              flagPCLXLTagDataType,
                              flagPCLXLTagAttribute,
                              flagPCLXLTagOperator,
                              flagPCLXLTagAttrDef,
                              flagPCLXLTagEmbedDataLen,
                              flagPCLXLTagWhitespace,
                              flagPCLXLOptReserved,
                              flagPMLTagDataType,
                              flagPMLTagAction,
                              flagPMLTagOutcome,
                              flagSymSetList,
                              flagSymSetMap);

                if (infoType == ToolCommonData.eToolSubIds.PCL)
                    reportBodyPCLSeqs(rptFileFmt, rptChkMarks,
                                      writer, dgSeq);
                else if (infoType == ToolCommonData.eToolSubIds.HPGL2)
                    reportBodyHPGL2Commands (rptFileFmt, writer, dgSeq);
                else if (infoType == ToolCommonData.eToolSubIds.PCLXLTags)
                    reportBodyPCLXLTags (rptFileFmt, rptChkMarks,
                                        writer, dgSeq);
                else if (infoType == ToolCommonData.eToolSubIds.PCLXLEnums)
                    reportBodyPCLXLEnums (rptFileFmt, writer, dgSeq);
                else if (infoType == ToolCommonData.eToolSubIds.PJLCmds)
                    reportBodyPJLCommands (rptFileFmt, writer, dgSeq);
                else if (infoType == ToolCommonData.eToolSubIds.PMLTags)
                    reportBodyPMLTags (rptFileFmt, writer, dgSeq);
                else if (infoType == ToolCommonData.eToolSubIds.SymbolSets)
                    reportBodySymbolSets (rptFileFmt, rptChkMarks,
                                          writer, dgSeq,
                                          flagSymSetMap, flagOptRptWrap,
                                          symSetMapType);
                else if (infoType == ToolCommonData.eToolSubIds.Fonts)
                    reportBodyFonts (rptFileFmt, rptChkMarks,
                                     writer, dgSeq,
                                     flagSymSetList, flagOptRptWrap);
                else if (infoType == ToolCommonData.eToolSubIds.PaperSizes)
                    reportBodyPaperSizes (rptFileFmt, writer, dgSeq);
                else if (infoType == ToolCommonData.eToolSubIds.PrescribeCmds)
                    reportBodyPrescribeCommands (rptFileFmt, writer, dgSeq);

                ReportCore.docFinalise (rptFileFmt, writer);

                ReportCore.docClose (rptFileFmt, stream, writer);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e p o r t B o d y F o n t s                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write details of displayed Fonts to report file.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void reportBodyFonts (
            ReportCore.eRptFileFmt rptFileFmt,
            ReportCore.eRptChkMarks rptChkMarks,
            Object writer,
            DataGrid dgSeq,
            Boolean  flagSymSetList,
            Boolean  flagOptRptWrap)
        {
            const Int32 colCtStd = 11;
            const Int32 colCtExt = 12;

            const String c0Name = "Typeface";
            const String c1Name = "Name";
            const String c2Name = "Spacing";
            const String c3Name = "Scalable";
            const String c4Name = "BoundSymbolSet";
            const String c5Name = "Pitch";
            const String c6Name = "Height";
            const String c7Name = "Var_Regular";
            const String c8Name = "Var_Italic";
            const String c9Name = "Var_Bold";
            const String c10Name= "Var_BoldItalic";
            const String c11Name= "SymbolSets";

            const String c0Hddr = "Typeface";
            const String c1Hddr = "Name";
            const String c2Hddr = "Spacing";
            const String c3Hddr = "Scalable?";
            const String c4Hddr = "Bound?";
            const String c5Hddr = "Pitch";
            const String c6Hddr = "Height";
            const String c7Hddr = "Regular";
            const String c8Hddr = "Italic ";
            const String c9Hddr = " Bold  ";
            const String c10Hddr= "Bold-It";
            const String c11Hddr= "Supported Symbol Sets?";

            const Int32 lcSep = 2;

            const Int32 lc0 = 8;
            const Int32 lc1 = 22;
            const Int32 lc2 = 12;
            const Int32 lc3 = 9;
            const Int32 lc4 = 6;
            const Int32 lc5 = 6;
            const Int32 lc6 = 6;
            const Int32 lc7 = 7;
            const Int32 lc8 = 7;
            const Int32 lc9 = 7;
            const Int32 lc10 = 7;
            const Int32 lc11 = 25;

            String[] colHddrs;
            String[] colNames;
            Int32[] colSizes;

            Int32 ctItems,
                  colCt;

            Int32 colSpanName = -1,
                  colSpanVal  = -1;

            PCLFont pclFont;

            String chkTrue,
                   chkTrue3,
                   chkTrue7,
                   chkTrue8,
                   chkTrue9,
                   chkTrue10;

            String chkFalse,
                   chkFalse3,
                   chkFalse7,
                   chkFalse8,
                   chkFalse9,
                   chkFalse10;

            if (rptChkMarks == ReportCore.eRptChkMarks.boxsym)
            {
                chkTrue = ReportCore._chkMarkBoxSymTrue;
                chkFalse = ReportCore._chkMarkBoxSymFalse;
            }
            else if (rptChkMarks == ReportCore.eRptChkMarks.txtsym)
            {
                chkTrue = ReportCore._chkMarkTxtSymTrue;
                chkFalse = ReportCore._chkMarkTxtSymFalse;
            }
            else // if (rptChkMarks == ReportCore.eRptChkMarks.text)
            {
                chkTrue = ReportCore._chkMarkTextTrue;
                chkFalse = ReportCore._chkMarkTextFalse;
            }

            chkTrue3 = (chkTrue.PadLeft ((lc3 / 2) + 1, ' '));
            chkTrue7 = (chkTrue.PadLeft ((lc7 / 2) + 1, ' '));
            chkTrue8 = (chkTrue.PadLeft ((lc8 / 2) + 1, ' '));
            chkTrue9 = (chkTrue.PadLeft ((lc9 / 2) + 1, ' '));
            chkTrue10 = (chkTrue.PadLeft ((lc10 / 2) + 1, ' '));

            chkFalse3 = (chkFalse.PadLeft ((lc3 / 2) + 1, ' '));
            chkFalse7 = (chkFalse.PadLeft ((lc7 / 2) + 1, ' '));
            chkFalse8 = (chkFalse.PadLeft ((lc8 / 2) + 1, ' '));
            chkFalse9 = (chkFalse.PadLeft ((lc9 / 2) + 1, ' '));
            chkFalse10 = (chkFalse.PadLeft ((lc10 / 2) + 1, ' '));

            ctItems = dgSeq.Items.Count;

            if ((flagSymSetList) && (! flagOptRptWrap))
            {
                colCt = colCtExt;

                colHddrs = new String[colCtExt] { c0Hddr, c1Hddr, c2Hddr,
                                                  c3Hddr, c4Hddr, c5Hddr,
                                                  c6Hddr, c7Hddr, c8Hddr,
                                                  c9Hddr, c10Hddr, c11Hddr};
                colNames = new String[colCtExt] { c0Name, c1Name, c2Name,
                                                  c3Name, c4Name, c5Name,
                                                  c6Name, c7Name, c8Name,
                                                  c9Name, c10Name, c11Name};
                colSizes = new Int32[colCtExt] { lc0, lc1, lc2,
                                                 lc3, lc4, lc5,
                                                 lc6, lc7, lc8,
                                                 lc9, lc10, lc11};
            }
            else
            {
                colCt = colCtStd;

                colSpanName = 2;
                colSpanVal = colCt - colSpanName;

                colHddrs = new String[colCtStd] { c0Hddr, c1Hddr, c2Hddr,
                                                  c3Hddr, c4Hddr, c5Hddr,
                                                  c6Hddr, c7Hddr, c8Hddr,
                                                  c9Hddr, c10Hddr};
                colNames = new String[colCtStd] { c0Name, c1Name, c2Name,
                                                  c3Name, c4Name, c5Name,
                                                  c6Name, c7Name, c8Name,
                                                  c9Name, c10Name};
                colSizes = new Int32[colCtStd] { lc0, lc1, lc2,
                                                 lc3, lc4, lc5,
                                                 lc6, lc7, lc8,
                                                 lc9, lc10};
            }

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
                String[] data = new string[colCt];

                pclFont = (PCLFont)dgSeq.Items[i];

                data[0] = pclFont.Typeface.ToString ();
                data[1] = pclFont.Name;
                data[2] = pclFont.Spacing;
                data[3] = (pclFont.Scalable) ? chkTrue3 : chkFalse3;
                data[4] = pclFont.BoundSymbolSet;
                data[5] = pclFont.Pitch;
                data[6] = pclFont.Height;
                data[7] = (pclFont.Var_Regular) ? chkTrue7 : chkFalse7;
                data[8] = (pclFont.Var_Italic) ? chkTrue8 : chkFalse8;
                data[9] = (pclFont.Var_Bold) ? chkTrue9 : chkFalse9;
                data[10] = (pclFont.Var_BoldItalic) ? chkTrue10 : chkFalse10;

                if (!flagSymSetList)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Mapping not to be shown.                               //
                    //                                                        //
                    //--------------------------------------------------------//

                    ReportCore.tableRowText (writer, rptFileFmt, colCt, data,
                                             colNames, colSizes);
                }
                else if (!flagOptRptWrap)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Mapping to be shown without wrapping.                  //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (rptFileFmt != ReportCore.eRptFileFmt.text)
                    {
                        //--------------------------------------------------------//
                        //                                                        //
                        // No wrap mapping for html or xml report format.         //
                        //                                                        //
                        //--------------------------------------------------------//

                        data[11] = pclFont.SymbolSets;

                        ReportCore.tableRowText (writer, rptFileFmt, colCt, data,
                                                 colNames, colSizes);
                    }
                    else
                    {
                        //--------------------------------------------------------//
                        //                                                        //
                        // No wrap mapping for text report format.                //
                        //                                                        //
                        //--------------------------------------------------------//

                        String[][] arrData = new String[colCt][];

                        arrData[0] = new String[1];
                        arrData[1] = new String[1];
                        arrData[2] = new String[1];
                        arrData[3] = new String[1];
                        arrData[4] = new String[1];
                        arrData[5] = new String[1];
                        arrData[6] = new String[1];
                        arrData[7] = new String[1];
                        arrData[8] = new String[1];
                        arrData[9] = new String[1];
                        arrData[10] = new String[1];

                        arrData[0][0] = data[0];
                        arrData[1][0] = data[1];
                        arrData[2][0] = data[2];
                        arrData[3][0] = data[3];
                        arrData[4][0] = data[4];
                        arrData[5][0] = data[5];
                        arrData[6][0] = data[6];
                        arrData[7][0] = data[7];
                        arrData[8][0] = data[8];
                        arrData[9][0] = data[9];
                        arrData[10][0] = data[10];

                        arrData[11] = pclFont.SymbolSetRows;

                        ReportCore.tableMultiRowText (writer, rptFileFmt, colCt,
                                                      arrData, colSizes,
                                                      false, false, false);
                    }
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Mapping to be shown with wrapping.                     //
                    //                                                        //
                    //--------------------------------------------------------//

                    Int32 maxLineLen = 120;

                    ReportCore.tableRowText (writer, rptFileFmt, colCt, data,
                                             colNames, colSizes);

                    if (pclFont.SymbolSetCt > 0)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Mapping data available for this symbol set.        //
                        //                                                    //
                        //----------------------------------------------------//

                        if (rptFileFmt != ReportCore.eRptFileFmt.text)
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Wrapped mapping for html or xml report format. //
                            //                                                //
                            //------------------------------------------------//

                            ReportCore.tableRowPair (
                                writer, rptFileFmt,
                                c11Hddr, pclFont.SymbolSets,
                                colSpanName, colSpanVal,
                                _maxSizeNameTag, maxLineLen,
                                _flagBlankBefore, _flagBlankAfter, _flagNameAsHddr);
                        }
                        else
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Wrapped mapping for text report format.        //
                            //                                                //
                            //------------------------------------------------//

                            const Int32 colCtPair = 2;

                            Int32 tmpInt;

                            Int32[] colSizesPair = new Int32[colCtPair];

                            String[][] arrData = new String[colCtPair][];

                            tmpInt = 0;
                            for (Int32 col = 0; col < colSpanName; col++)
                            {
                                if (col != 0)
                                    tmpInt += lcSep;

                                tmpInt += colSizes[col];
                            }
                            colSizesPair[0] = tmpInt;

                            tmpInt = 0;
                            for (Int32 col = 0; col < colSpanVal; col++)
                            {
                                if (col != 0)
                                    tmpInt += lcSep;

                                tmpInt += colSizes[col + colSpanName];
                            }
                            colSizesPair[1] = tmpInt;

                            arrData[0] = new String[1];

                            arrData[0][0] = data[0];
                            arrData[0][0] = c11Hddr;
                            arrData[1] = pclFont.SymbolSetRows;

                            ReportCore.tableMultiRowText (
                                writer, rptFileFmt, colCtPair,
                                arrData, colSizesPair, true, true, true);
                        }
                    }
                }
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
        // r e p o r t B o d y H P G L 2 C o m m a n d s                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write details of displayed HP-GL/2 commands to report file.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void reportBodyHPGL2Commands(
            ReportCore.eRptFileFmt rptFileFmt,
            Object writer,
            DataGrid dgSeq)
        {
            const Int32 colCt = 2;

            const String c0Name = "Mnemonic";
            const String c1Name = "Description";

            const String c0Hddr = "Mnemonic";
            const String c1Hddr = "Description";

            const Int32 lc0 = 8;
            const Int32 lc1 = 35;

            String[] colHddrs;
            String[] colNames;
            Int32[] colSizes;

            Int32 ctItems;

            HPGL2Command hpgl2Command;

            ctItems = dgSeq.Items.Count;

            colHddrs = new String[colCt] { c0Hddr, c1Hddr };
            colNames = new String[colCt] { c0Name, c1Name };
            colSizes = new Int32[colCt] { lc0, lc1 };

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
                String[] data = new string[colCt];

                hpgl2Command = (HPGL2Command)dgSeq.Items[i];

                data[0] = hpgl2Command.Mnemonic;
                data[1] = hpgl2Command.Description;

                ReportCore.tableRowText (writer, rptFileFmt, colCt, data,
                                      colNames, colSizes);
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
        // r e p o r t B o d y P a p e r S i z e s                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write details of displayed paper size details to report file.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void reportBodyPaperSizes(
            ReportCore.eRptFileFmt rptFileFmt,
            Object writer,
            DataGrid dgSeq)
        {
            const Int32 colCt = 6;

            const String c0Name = "Name";
            const String c1Name = "Desc";
            const String c2Name = "EdgeShort";
            const String c3Name = "EdgeLong";
            const String c4Name = "IdPCL";
            const String c5Name = "IdNamePCLXL";

            const String c0Hddr = "Name";
            const String c1Hddr = "Description";
            const String c2Hddr = "Short edge";
            const String c3Hddr = "Long edge";
            const String c4Hddr = "PCL Id";
            const String c5Hddr = "PCL XL Id/Name";

            const Int32 lc0 = 25;
            const Int32 lc1 = 45;
            const Int32 lc2 = 10;
            const Int32 lc3 = 10;
            const Int32 lc4 = 10;
            const Int32 lc5 = 15;

            String[] colHddrs;
            String[] colNames;
            Int32[] colSizes;

            Int32 ctItems;

            PCLPaperSize paperSize;

            ctItems = dgSeq.Items.Count;

            colHddrs = new String[colCt] { c0Hddr, c1Hddr, c2Hddr, c3Hddr,
                                           c4Hddr, c5Hddr };
            colNames = new String[colCt] { c0Name, c1Name, c2Name, c3Name,
                                           c4Name, c5Name };
            colSizes = new Int32[colCt] { lc0, lc1, lc2, lc3, lc4, lc5 };

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
                String[] data = new string[colCt];

                paperSize = (PCLPaperSize)dgSeq.Items[i];

                data[0] = paperSize.Name;
                data[1] = paperSize.Desc;
                data[2] = paperSize.EdgeShort;
                data[3] = paperSize.EdgeLong;
                data[4] = paperSize.IdPCL;
                data[5] = paperSize.IdNamePCLXL;

                ReportCore.tableRowText (writer, rptFileFmt, colCt, data,
                                      colNames, colSizes);
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
        // r e p o r t B o d y P C L S e q s                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write details of displayed PCL sequences to report file.           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void reportBodyPCLSeqs (
            ReportCore.eRptFileFmt rptFileFmt,
            ReportCore.eRptChkMarks rptChkMarks,
            Object writer,
            DataGrid dgSeq)
        {
            const Int32 colCt = 5;

            const String c0Name = "Sequence";
            const String c1Name = "Type";
            const String c2Name = "Obsolete";
            const String c3Name = "ValIsLen";
            const String c4Name = "Description";

            const String c2Hddr = "Obsolete?";
            const String c3Hddr = "#=length?";

            const Int32 lc0 = 20;
            const Int32 lc1 = 7;
            const Int32 lc2 = 9;
            const Int32 lc3 = 9;
            const Int32 lc4 = 35;

            String[] colHddrs;
            String[] colNames;
            Int32[] colSizes;

            Int32 ctItems;

            String typeName;

            PCLControlCode pclControlCode;

            PCLSimpleSeq pclSimpleSeq;

            PCLComplexSeq pclComplexSeq;

            String chkTrue,
                   chkTrue2,
                   chkTrue3;

            String chkFalse,
                   chkFalse2,
                   chkFalse3;

            if (rptChkMarks == ReportCore.eRptChkMarks.boxsym)
            {
                chkTrue = ReportCore._chkMarkBoxSymTrue;
                chkFalse = ReportCore._chkMarkBoxSymFalse;
            }
            else if (rptChkMarks == ReportCore.eRptChkMarks.txtsym)
            {
                chkTrue = ReportCore._chkMarkTxtSymTrue;
                chkFalse = ReportCore._chkMarkTxtSymFalse;
            }
            else // if (rptChkMarks == ReportCore.eRptChkMarks.text)
            {
                chkTrue = ReportCore._chkMarkTextTrue;
                chkFalse = ReportCore._chkMarkTextFalse;
            }

            chkTrue2 = (chkTrue.PadLeft ((lc2 / 2) + 1, ' '));
            chkTrue3 = (chkTrue.PadLeft ((lc3 / 2) + 1, ' '));

            chkFalse2 = (chkFalse.PadLeft ((lc2 / 2) + 1, ' '));
            chkFalse3 = (chkFalse.PadLeft ((lc3 / 2) + 1, ' '));

            ctItems = dgSeq.Items.Count;

            colHddrs = new String[colCt] { c0Name, c1Name, c2Hddr, c3Hddr, c4Name };
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
                String[] data = new string[colCt];

                typeName = dgSeq.Items[i].GetType ().Name;

                if (typeName == "PCLControlCode")
                {
                    pclControlCode = (PCLControlCode)dgSeq.Items[i];

                    data[0] = pclControlCode.Sequence;
                    data[1] = pclControlCode.Type;
                    data[2] = (pclControlCode.FlagObsolete) ?
                                chkTrue2 : chkFalse2;
                    data[3] = (pclControlCode.FlagValIsLen) ?
                                chkTrue3 : chkFalse3;
                    data[4] = pclControlCode.Description;
                }
                else if (typeName == "PCLSimpleSeq")
                {
                    pclSimpleSeq = (PCLSimpleSeq)dgSeq.Items[i];

                    data[0] = pclSimpleSeq.Sequence;
                    data[1] = pclSimpleSeq.Type;
                    data[2] = (pclSimpleSeq.FlagObsolete) ?
                                chkTrue2 : chkFalse2;
                    data[3] = (pclSimpleSeq.FlagValIsLen) ?
                                chkTrue3 : chkFalse3;
                    data[4] = pclSimpleSeq.Description;
                }
                else if (typeName == "PCLComplexSeq")
                {
                    pclComplexSeq = (PCLComplexSeq)dgSeq.Items[i];

                    data[0] = pclComplexSeq.Sequence;
                    data[1] = pclComplexSeq.Type;
                    data[2] = (pclComplexSeq.FlagObsolete) ?
                                chkTrue2 : chkFalse2;
                    data[3] = (pclComplexSeq.FlagValIsLen) ?
                                chkTrue3 : chkFalse3;
                    data[4] = pclComplexSeq.Description;
                }

                ReportCore.tableRowText (writer,rptFileFmt, colCt, data, 
                                      colNames, colSizes);
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
        // r e p o r t B o d y P C L X L E n u m s                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write details of displayed PCL XL enumerations to report file.     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void reportBodyPCLXLEnums (
            ReportCore.eRptFileFmt rptFileFmt,
            Object writer,
            DataGrid dgSeq)
        {
            const Int32 colCt = 4;

            const String c0Name = "Operator";
            const String c1Name = "Attribute";
            const String c2Name = "Value";
            const String c3Name = "Description";

            const String c0Hddr = "Operator";
            const String c1Hddr = "Attribute";
            const String c2Hddr = "Value";
            const String c3Hddr = "Description";

            const Int32 lc0 = 21;
            const Int32 lc1 = 20;
            const Int32 lc2 = 10;
            const Int32 lc3 = 35;

            String[] colHddrs;
            String[] colNames;
            Int32[] colSizes;

            Int32 ctItems;

            PCLXLAttrEnum xlAttrEnum;

            ctItems = dgSeq.Items.Count;

            colHddrs = new String[colCt] { c0Hddr, c1Hddr, c2Hddr, c3Hddr };
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
                String[] data = new string[colCt];

                xlAttrEnum = (PCLXLAttrEnum)dgSeq.Items[i];

                data[0] = xlAttrEnum.Operator;
                data[1] = xlAttrEnum.Attribute;
                data[2] = xlAttrEnum.Value;
                data[3] = xlAttrEnum.Description;

                ReportCore.tableRowText (writer, rptFileFmt, colCt, data,
                                      colNames, colSizes);
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
        // r e p o r t B o d y P C L X L T a g s                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write details of displayed PCL XL tags to report file.             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void reportBodyPCLXLTags (
            ReportCore.eRptFileFmt rptFileFmt,
            ReportCore.eRptChkMarks rptChkMarks,
            Object writer,
            DataGrid dgSeq)
        {
            const Int32 colCt = 4;

            const String c0Name = "Operator";
            const String c1Name = "Attribute";
            const String c2Name = "Value";
            const String c3Name = "Description";

            const String c0Hddr = "Tag";
            const String c1Hddr = "Type";
            const String c2Hddr = "Reserved";
            const String c3Hddr = "Description";

            const Int32 lc0 = 4;
            const Int32 lc1 = 18;
            const Int32 lc2 = 5;
            const Int32 lc3 = 35;

            String[] colHddrs;
            String[] colNames;
            Int32[] colSizes;

            Int32 ctItems;

            String typeName;

            PCLXLAttrDefiner xlAttrDef;
            PCLXLAttribute    xlAttribute;
            PCLXLDataType     xlDataType;
            PCLXLEmbedDataDef xlEmbedDef;
            PCLXLOperator     xlOperator;
            PCLXLWhitespace   xlWhitespace;

            String chkTrue,
                   chkTrue2;

            String chkFalse,
                   chkFalse2;

            if (rptChkMarks == ReportCore.eRptChkMarks.boxsym)
            {
                chkTrue = ReportCore._chkMarkBoxSymTrue;
                chkFalse = ReportCore._chkMarkBoxSymFalse;
            }
            else if (rptChkMarks == ReportCore.eRptChkMarks.txtsym)
            {
                chkTrue = ReportCore._chkMarkTxtSymTrue;
                chkFalse = ReportCore._chkMarkTxtSymFalse;
            }
            else // if (rptChkMarks == ReportCore.eRptChkMarks.text)
            {
                chkTrue = ReportCore._chkMarkTextTrue;
                chkFalse = ReportCore._chkMarkTextFalse;
            }

            chkTrue2 = (chkTrue.PadLeft ((lc2 / 2) + 1, ' '));

            chkFalse2 = (chkFalse.PadLeft ((lc2 / 2) + 1, ' '));

            ctItems = dgSeq.Items.Count;

            colHddrs = new String[colCt] { c0Hddr, c1Hddr, c2Hddr, c3Hddr };
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
                String[] data = new string[colCt];

                typeName = dgSeq.Items[i].GetType ().Name;

                if (typeName == "PCLXLAttrDefiner")
                {
                    xlAttrDef = (PCLXLAttrDefiner)dgSeq.Items[i];

                    data[0] = xlAttrDef.Tag;
                    data[1] = xlAttrDef.Type;
                    data[2] = (xlAttrDef.FlagReserved) ?
                                chkTrue2 : chkFalse2;
                    data[3] = xlAttrDef.Description;
                }
                else if (typeName == "PCLXLAttribute")
                {
                    xlAttribute = (PCLXLAttribute)dgSeq.Items[i];

                    data[0] = xlAttribute.Tag;
                    data[1] = xlAttribute.Type;
                    data[2] = (xlAttribute.FlagReserved) ?
                                chkTrue2 : chkFalse2;
                    data[3] = xlAttribute.Description;
                }
                else if (typeName == "PCLXLDataType")
                {
                    xlDataType = (PCLXLDataType)dgSeq.Items[i];

                    data[0] = xlDataType.Tag;
                    data[1] = xlDataType.Type;
                    data[2] = (xlDataType.FlagReserved) ?
                                chkTrue2 : chkFalse2;
                    data[3] = xlDataType.Description;
                }
                else if (typeName == "PCLXLEmbedDataDef")
                {
                    xlEmbedDef = (PCLXLEmbedDataDef)dgSeq.Items[i];

                    data[0] = xlEmbedDef.Tag;
                    data[1] = xlEmbedDef.Type;
                    data[2] = (xlEmbedDef.FlagReserved) ?
                                chkTrue2 : chkFalse2;
                    data[3] = xlEmbedDef.Description;
                }
                else if (typeName == "PCLXLOperator")
                {
                    xlOperator = (PCLXLOperator)dgSeq.Items[i];

                    data[0] = xlOperator.Tag;
                    data[1] = xlOperator.Type;
                    data[2] = (xlOperator.FlagReserved) ?
                                chkTrue2 : chkFalse2;
                    data[3] = xlOperator.Description;
                }
                else if (typeName == "PCLXLWhitespace")
                {
                    xlWhitespace = (PCLXLWhitespace)dgSeq.Items[i];

                    data[0] = xlWhitespace.Tag;
                    data[1] = xlWhitespace.Type;
                    data[2] = (xlWhitespace.FlagReserved) ?
                                chkTrue2 : chkFalse2;
                    data[3] = xlWhitespace.Description;
                }

                ReportCore.tableRowText (writer, rptFileFmt, colCt, data,
                                      colNames, colSizes);
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
        // r e p o r t B o d y P J L C o m m a n d s                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write details of displayed PJL commands to report file.            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void reportBodyPJLCommands (
            ReportCore.eRptFileFmt rptFileFmt,
            Object writer,
            DataGrid dgSeq)
        {
            const Int32 colCt = 2;

            const String c0Name = "Name";
            const String c1Name = "Description";

            const String c0Hddr = "Command";
            const String c1Hddr = "Description";

            const Int32 lc0 = 10;
            const Int32 lc1 = 35;

            String[] colHddrs;
            String[] colNames;
            Int32[] colSizes;

            Int32 ctItems;

            PJLCommand pjlCommand;

            ctItems = dgSeq.Items.Count;

            colHddrs = new String[colCt] { c0Hddr, c1Hddr };
            colNames = new String[colCt] { c0Name, c1Name };
            colSizes = new Int32[colCt] { lc0, lc1 };

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
                String[] data = new string[colCt];

                pjlCommand = (PJLCommand)dgSeq.Items[i];

                data[0] = pjlCommand.Name;
                data[1] = pjlCommand.Description;

                ReportCore.tableRowText (writer, rptFileFmt, colCt, data,
                                      colNames, colSizes);
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
        // r e p o r t B o d y P M L T a g s                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write details of displayed PML tags to report file.                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void reportBodyPMLTags (
            ReportCore.eRptFileFmt rptFileFmt,
            Object writer,
            DataGrid dgSeq)
        {
            const Int32 colCt = 3;

            const String c0Name = "Tag";
            const String c1Name = "Type";
            const String c2Name = "Description";

            const String c0Hddr = "Tag";
            const String c1Hddr = "Type";
            const String c2Hddr = "Description";

            const Int32 lc0 = 4;
            const Int32 lc1 = 10;
            const Int32 lc2 = 35;

            String[] colHddrs;
            String[] colNames;
            Int32[] colSizes;

            Int32 ctItems;
            String typeName;

            PMLDataType pmlDataType;
            PMLAction pmlAction;
            PMLOutcome pmlOutcome;

            ctItems = dgSeq.Items.Count;

            colHddrs = new String[colCt] { c0Hddr, c1Hddr, c2Hddr };
            colNames = new String[colCt] { c0Name, c1Name, c2Name };
            colSizes = new Int32[colCt] { lc0, lc1, lc2 };

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
                String[] data = new string[colCt];

                typeName = dgSeq.Items[i].GetType ().Name;

                if (typeName == "PMLDataType")
                {
                    pmlDataType = (PMLDataType)dgSeq.Items[i];

                    data[0] = pmlDataType.Tag;
                    data[1] = pmlDataType.Type;
                    data[2] = pmlDataType.Description;
                }
                else if (typeName == "PMLAction")
                {
                    pmlAction = (PMLAction)dgSeq.Items[i];

                    data[0] = pmlAction.Tag;
                    data[1] = pmlAction.Type;
                    data[2] = pmlAction.Description;
                }
                else if (typeName == "PMLOutcome")
                {
                    pmlOutcome = (PMLOutcome)dgSeq.Items[i];

                    data[0] = pmlOutcome.Tag;
                    data[1] = pmlOutcome.Type;
                    data[2] = pmlOutcome.Description;
                }

                ReportCore.tableRowText (writer, rptFileFmt, colCt, data,
                                      colNames, colSizes);
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
        // r e p o r t B o d y P r e s c r i b e C o m m a n d s              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write details of displayed Prescribe commands to report file.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void reportBodyPrescribeCommands (
            ReportCore.eRptFileFmt rptFileFmt,
            Object writer,
            DataGrid dgSeq)
        {
            const Int32 colCt = 2;

            const String c0Name = "Name";
            const String c1Name = "Description";

            const String c0Hddr = "Command";
            const String c1Hddr = "Description";

            const Int32 lc0 = 7;
            const Int32 lc1 = 35;

            String[] colHddrs;
            String[] colNames;
            Int32[] colSizes;

            Int32 ctItems;

            PrescribeCommand prescribeCommand;

            ctItems = dgSeq.Items.Count;

            colHddrs = new String[colCt] { c0Hddr, c1Hddr };
            colNames = new String[colCt] { c0Name, c1Name };
            colSizes = new Int32[colCt] { lc0, lc1 };

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
                String[] data = new string[colCt];

                prescribeCommand = (PrescribeCommand)dgSeq.Items[i];

                data[0] = prescribeCommand.Name;
                data[1] = prescribeCommand.Description;

                ReportCore.tableRowText (writer, rptFileFmt, colCt, data,
                                      colNames, colSizes);
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
        // r e p o r t B o d y S y m b o l S e t s                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write details of displayed Symbol Sets to report file.             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void reportBodySymbolSets (
            ReportCore.eRptFileFmt rptFileFmt,
            ReportCore.eRptChkMarks rptChkMarks,
            Object writer,
            DataGrid dgSeq,
            Boolean                      flagSymSetMap,
            Boolean                      flagOptRptWrap,
            ToolPrintLang.eSymSetMapType symSetMapType)
        {
            const Int32 colCtNoMap   = 8;
            const Int32 colCtMapWrap = 6;
            const Int32 colCtMapOne  = 7;
            const Int32 colCtMapBoth = 9;

            const String c0Name = "Groupname";
            const String c1Name = "TypeDescShort";
            const String c2Name = "Id";
            const String c3Name = "Kind1";
            const String c4Name = "Alias";
            const String c5Name = "Name";
            const String c6aName = "FlagMapStd";
            const String c6bName = "MappingStd";
            const String c7aName = "FlagMapPCL";
            const String c7bName = "MappingPCL";
            const String c8Name = "MappingDiff";

            const String c0Hddr = "Group";
            const String c1Hddr = "Type";
            const String c2Hddr = "Id";
            const String c3Hddr = "Kind1";
            const String c4Hddr = "Alias";
            const String c5Hddr = "Name";
            const String c6aHddr = "Map (Strict)?";
            const String c6bHddr = "Mapping (Strict)";
            const String c7aHddr = "Map (LaserJet)?";
            const String c7bHddr = "Mapping (LaserJet)";
            const String c8Hddr = "Mapping (differences)";

            const Int32 lc0 = 7;
            const Int32 lc1 = 13;
            const Int32 lc2 = 5;
            const Int32 lc3 = 5;
            const Int32 lc4 = 8;
            const Int32 lc5 = 50;
            const Int32 lc6a = 15;
            const Int32 lc6b = 89;
            const Int32 lc7a = 15;
            const Int32 lc7b = 89;
            const Int32 lc8 = 89;

            String[] colHddrs;
            String[] colNames;
            Int32[] colSizes;

            Int32 ctItems,
                  colCt;

            Int32 colSpanName = -1,
                  colSpanVal  = -1;

            PCLSymbolSet symbolSet;

            String chkTrue,
                   chkTrue6a,
                   chkTrue7a;

            String chkFalse,
                   chkFalse6a,
                   chkFalse7a;

            if (rptChkMarks == ReportCore.eRptChkMarks.boxsym)
            {
                chkTrue = ReportCore._chkMarkBoxSymTrue;
                chkFalse = ReportCore._chkMarkBoxSymFalse;
            }
            else if (rptChkMarks == ReportCore.eRptChkMarks.txtsym)
            {
                chkTrue = ReportCore._chkMarkTxtSymTrue;
                chkFalse = ReportCore._chkMarkTxtSymFalse;
            }
            else // if (rptChkMarks == ReportCore.eRptChkMarks.text)
            {
                chkTrue = ReportCore._chkMarkTextTrue;
                chkFalse = ReportCore._chkMarkTextFalse;
            }

            chkTrue6a = (chkTrue.PadLeft ((lc6a / 2) + 1, ' '));
            chkTrue7a = (chkTrue.PadLeft ((lc7a / 2) + 1, ' '));

            chkFalse6a = (chkFalse.PadLeft ((lc6a / 2) + 1, ' '));
            chkFalse7a = (chkFalse.PadLeft ((lc7a / 2) + 1, ' '));

            ctItems = dgSeq.Items.Count;

            if (! flagSymSetMap)
            {
                colCt = colCtNoMap;

                colHddrs = new String[colCtNoMap] { c0Hddr, c1Hddr, c2Hddr,
                                                    c3Hddr, c4Hddr, c5Hddr,
                                                    c6aHddr, c7aHddr };
                colNames = new String[colCtNoMap] { c0Name, c1Name, c2Name,
                                                    c3Name, c4Name, c5Name,
                                                    c6aName, c7aName };
                colSizes = new Int32[colCtNoMap] { lc0, lc1, lc2,
                                                   lc3, lc4, lc5,
                                                   lc6a, lc7a };
            }
            else
            {
                if (flagOptRptWrap)
                {
                    colCt = colCtMapWrap;

                    colSpanName = 2;
                    colSpanVal  = colCt - colSpanName;

                    colHddrs = new String[colCtMapWrap] { c0Hddr, c1Hddr, c2Hddr,
                                                          c3Hddr, c4Hddr, c5Hddr };
                    colNames = new String[colCtMapWrap] { c0Name, c1Name, c2Name,
                                                          c3Name, c4Name, c5Name };
                    colSizes = new Int32[colCtMapWrap] { lc0, lc1, lc2,
                                                         lc3, lc4, lc5 };
                }
                else if (symSetMapType == ToolPrintLang.eSymSetMapType.Std)
                {
                    colCt = colCtMapOne;

                    colHddrs = new String[colCtMapOne] { c0Hddr, c1Hddr, c2Hddr,
                                                         c3Hddr, c4Hddr, c5Hddr,
                                                         c6bHddr };
                    colNames = new String[colCtMapOne] { c0Name, c1Name, c2Name,
                                                         c3Name, c4Name, c5Name,
                                                         c6bName };
                    colSizes = new Int32[colCtMapOne] { lc0, lc1, lc2,
                                                        lc3, lc4, lc5,
                                                        lc6b };
                }
                else if (symSetMapType == ToolPrintLang.eSymSetMapType.PCL)
                {
                    colCt = colCtMapOne;

                    colHddrs = new String[colCtMapOne] { c0Hddr, c1Hddr, c2Hddr,
                                                         c3Hddr, c4Hddr, c5Hddr,
                                                         c7bHddr };
                    colNames = new String[colCtMapOne] { c0Name, c1Name, c2Name,
                                                         c3Name, c4Name, c5Name,
                                                         c7bName };
                    colSizes = new Int32[colCtMapOne] { lc0, lc1, lc2,
                                                        lc3, lc4, lc5,
                                                        lc7b };
                }
                else //if (symSetMapType == ToolPrintLang.eSymSetMapType.Both)
                {
                    colCt = colCtMapBoth;

                    colHddrs = new String[colCtMapBoth] { c0Hddr, c1Hddr, c2Hddr,
                                                          c3Hddr, c4Hddr, c5Hddr,
                                                          c6bHddr, c7bHddr, c8Hddr };
                    colNames = new String[colCtMapBoth] { c0Name, c1Name, c2Name,
                                                          c3Name, c4Name, c5Name,
                                                          c6bName, c7bName, c8Name };
                    colSizes = new Int32[colCtMapBoth] { lc0, lc1, lc2,
                                                         lc3, lc4, lc5,
                                                         lc6b, lc7b, lc8 };
                }
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Open the main table and Write the column header text.          //
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
                String[] data = new string[colCt];

                Boolean mapStd,
                        mapPCL;

                symbolSet = (PCLSymbolSet)dgSeq.Items[i];

                mapStd = symbolSet.FlagMapStd;
                mapPCL = symbolSet.FlagMapPCL;

                data[0] = symbolSet.Groupname;
                data[1] = symbolSet.TypeDescShort;
                data[2] = symbolSet.Id;
                data[3] = symbolSet.Kind1.ToString ();
                data[4] = symbolSet.Alias;
                data[5] = symbolSet.Name;

                if (!flagSymSetMap)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Mapping not to be shown.                               //
                    //                                                        //
                    //--------------------------------------------------------//

                    data[6] = (mapStd) ? chkTrue6a : chkFalse6a;
                    data[7] = (mapPCL) ? chkTrue7a : chkFalse7a;

                    ReportCore.tableRowText (writer, rptFileFmt, colCt, data,
                                             colNames, colSizes);
                }
                else if (flagOptRptWrap)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Mapping to be shown with wrapping.                     //
                    //                                                        //
                    //--------------------------------------------------------//

                    Int32 maxLineLen = 120;

                    ReportCore.tableRowText (writer, rptFileFmt, colCt, data,
                                             colNames, colSizes);

                    if ((mapStd) || (mapPCL))
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Mapping data available for this symbol set.        //
                        //                                                    //
                        //----------------------------------------------------//

                        if (rptFileFmt != ReportCore.eRptFileFmt.text)
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Wrapped mapping for html or xml report format. //
                            //                                                //
                            //------------------------------------------------//

                            if (symSetMapType == ToolPrintLang.eSymSetMapType.Std)
                            {
                                if (mapStd)
                                    ReportCore.tableRowPair (
                                        writer, rptFileFmt, c6bHddr,
                                        symbolSet.MappingStd,
                                        colSpanName, colSpanVal,
                                        _maxSizeNameTag, maxLineLen,
                                        _flagBlankBefore, _flagBlankAfter, _flagNameAsHddr);
                                else
                                    ReportCore.tableRowPair (
                                        writer, rptFileFmt, c6bHddr,
                                        "Not defined - see LaserJet mapping definition",
                                        colSpanName, colSpanVal,
                                        _maxSizeNameTag, maxLineLen,
                                        _flagBlankBefore, _flagBlankAfter, _flagNameAsHddr);
                            }
                            else if (symSetMapType == ToolPrintLang.eSymSetMapType.PCL)
                            {
                                if (mapPCL)
                                    ReportCore.tableRowPair (
                                        writer, rptFileFmt, c7bHddr,
                                        symbolSet.MappingPCL,
                                        colSpanName, colSpanVal,
                                        _maxSizeNameTag, maxLineLen,
                                        _flagBlankBefore, _flagBlankAfter, _flagNameAsHddr);
                                else
                                    ReportCore.tableRowPair (
                                        writer, rptFileFmt, c7bHddr,
                                        "Not defined - see Standard (Strict) mapping definition",
                                        colSpanName, colSpanVal,
                                        _maxSizeNameTag, maxLineLen,
                                        _flagBlankBefore, _flagBlankAfter, _flagNameAsHddr);
                            }
                            else // if (symSetMapType == ToolPrintLang.eSymSetMapType.Both)
                            {
                                if (mapStd)
                                    ReportCore.tableRowPair (
                                        writer, rptFileFmt, c6bHddr,
                                        symbolSet.MappingStd,
                                        colSpanName, colSpanVal,
                                        _maxSizeNameTag, maxLineLen,
                                        _flagBlankBefore, _flagNone, _flagNameAsHddr);
                                else
                                    ReportCore.tableRowPair (
                                        writer, rptFileFmt, c6bHddr,
                                        "Not defined - see LaserJet mapping definition",
                                        colSpanName, colSpanVal,
                                        _maxSizeNameTag, maxLineLen,
                                        _flagBlankBefore, _flagNone, _flagNameAsHddr);

                                if (mapPCL)
                                    ReportCore.tableRowPair (
                                        writer, rptFileFmt, c7bHddr,
                                        symbolSet.MappingPCL,
                                        colSpanName, colSpanVal,
                                        _maxSizeNameTag, maxLineLen,
                                        _flagBlankBefore, _flagNone, _flagNameAsHddr);
                                else
                                    ReportCore.tableRowPair (
                                        writer, rptFileFmt, c7bHddr,
                                        "Not defined - see Standard (Strict) mapping definition",
                                        colSpanName, colSpanVal,
                                        _maxSizeNameTag, maxLineLen,
                                        _flagBlankBefore, _flagNone, _flagNameAsHddr);

                                if ((mapStd) && (mapPCL))
                                    ReportCore.tableRowPair (
                                        writer, rptFileFmt, c8Hddr,
                                        symbolSet.MappingPCL,
                                        colSpanName, colSpanVal,
                                        _maxSizeNameTag, maxLineLen,
                                        _flagBlankBefore, _flagBlankAfter, _flagNameAsHddr);
                                else
                                    ReportCore.tableRowPair (
                                        writer, rptFileFmt, c8Hddr,
                                        "Not applicable (only one set defined)",
                                        colSpanName, colSpanVal,
                                        _maxSizeNameTag, maxLineLen,
                                        _flagBlankBefore, _flagBlankAfter, _flagNameAsHddr);
                            }
                        }
                        else
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Wrapped mapping for text report format.        //
                            //                                                //
                            //------------------------------------------------//

                            const Int32 colCtPair = 2;
                            Int32[] colSizesPair = new Int32[colCtPair] {
                                                        22, 100 };          // *************** do this another way ????? **************

                            String[][] arrData = new String[colCtPair][];

                            arrData[0] = new String[1];

                            arrData[0][0] = data[0];

                            if (symSetMapType == ToolPrintLang.eSymSetMapType.Std)
                            {
                                arrData[0][0] = c6bHddr;
                                arrData[1] = symbolSet.MapRowsStd;

                                ReportCore.tableMultiRowText (
                                    writer, rptFileFmt, colCtPair,
                                    arrData, colSizesPair, true, true, true);
                            }
                            else if (symSetMapType == ToolPrintLang.eSymSetMapType.PCL)
                            {
                                arrData[0][0] = c7bHddr;
                                arrData[1] = symbolSet.MapRowsPCL;

                                ReportCore.tableMultiRowText (
                                    writer, rptFileFmt, colCtPair,
                                    arrData, colSizesPair, true, true, true);
                            }
                            else // if (symSetMapType == ToolPrintLang.eSymSetMapType.Both)
                            {
                                arrData[0][0] = c6bHddr;
                                arrData[1] = symbolSet.MapRowsStd;

                                ReportCore.tableMultiRowText (
                                    writer, rptFileFmt, colCtPair,
                                    arrData, colSizesPair, true, false, false);

                                arrData[0][0] = c7bHddr;
                                arrData[1] = symbolSet.MapRowsPCLDiff;

                                ReportCore.tableMultiRowText (
                                    writer, rptFileFmt, colCtPair,
                                    arrData, colSizesPair, true, false, false);

                                arrData[0][0] = c8Hddr;
                                arrData[1] = symbolSet.MapRowsDiff;

                                ReportCore.tableMultiRowText (
                                    writer, rptFileFmt, colCtPair,
                                    arrData, colSizesPair, true, true, true);
                            }
                        }
                    }
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Mapping to be shown without wrapping.                  //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (rptFileFmt != ReportCore.eRptFileFmt.text)
                    {
                        //--------------------------------------------------------//
                        //                                                        //
                        // No wrap mapping for html or xml report format.         //
                        //                                                        //
                        //--------------------------------------------------------//

                        if (symSetMapType == ToolPrintLang.eSymSetMapType.Std)
                        {
                            data[6] = symbolSet.MappingStd;
                        }
                        else if (symSetMapType == ToolPrintLang.eSymSetMapType.PCL)
                        {
                            data[6] = symbolSet.MappingPCL;
                        }
                        else // if (symSetMapType == ToolPrintLang.eSymSetMapType.Both)
                        {
                            data[6] = symbolSet.MappingStd;
                            data[7] = symbolSet.MappingPCLDiff;
                            data[8] = symbolSet.MappingDiff;
                        }

                        ReportCore.tableRowText (writer, rptFileFmt, colCt, data,
                                                 colNames, colSizes);
                    }
                    else
                    {
                        //--------------------------------------------------------//
                        //                                                        //
                        // No wrap mapping for text report format.                //
                        //                                                        //
                        //--------------------------------------------------------//

                        String[][] arrData = new String[colCt][];

                        arrData[0] = new String[1];
                        arrData[1] = new String[1];
                        arrData[2] = new String[1];
                        arrData[3] = new String[1];
                        arrData[4] = new String[1];
                        arrData[5] = new String[1];

                        arrData[0][0] = data[0];
                        arrData[1][0] = data[1];
                        arrData[2][0] = data[2];
                        arrData[3][0] = data[3];
                        arrData[4][0] = data[4];
                        arrData[5][0] = data[5];

                        if (symSetMapType == ToolPrintLang.eSymSetMapType.Std)
                        {
                            arrData[6] = symbolSet.MapRowsStd;
                        }
                        else if (symSetMapType == ToolPrintLang.eSymSetMapType.PCL)
                        {
                            arrData[6] = symbolSet.MapRowsPCL;
                        }
                        else // if (symSetMapType == ToolPrintLang.eSymSetMapType.Both)
                        {
                            arrData[6] = symbolSet.MapRowsStd;
                            arrData[7] = symbolSet.MapRowsPCLDiff;
                            arrData[8] = symbolSet.MapRowsDiff;
                        }

                        ReportCore.tableMultiRowText (writer, rptFileFmt, colCt,
                                                      arrData, colSizes,
                                                 //   false, false, true);
                                                      false, false, false);
                    }
                }
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

        private static void reportHeader(ToolCommonData.eToolSubIds infoType,
                                         ReportCore.eRptFileFmt rptFileFmt,
                                         Object   writer,
                                         DataGrid dgSeq,
                                         Boolean  flagPCLSeqControl,
                                         Boolean  flagPCLSeqSimple,
                                         Boolean  flagPCLSeqComplex,
                                         Boolean  flagPCLOptObsolete,
                                         Boolean  flagPCLOptDiscrete,
                                         Boolean  flagPCLXLTagDataType,
                                         Boolean  flagPCLXLTagAttribute,
                                         Boolean  flagPCLXLTagOperator,
                                         Boolean  flagPCLXLTagAttrDef,
                                         Boolean  flagPCLXLTagEmbedDataLen,
                                         Boolean  flagPCLXLTagWhitespace,
                                         Boolean  flagPCLXLOptReserved,
                                         Boolean  flagPMLTagDataType,
                                         Boolean  flagPMLTagAction,
                                         Boolean  flagPMLTagOutcome,
                                         Boolean  flagSymSetList,
                                         Boolean  flagSymSetMap)
        {
            Int32 maxLineLen = 80;          // ********************** set this from column sizes ????????????????

            Int32 ctCols;

            String sort = "";
            String colHddr = "";
            String colSort = "";

            Boolean selHddrStarted = false;

            ctCols = dgSeq.Columns.Count;

            for (Int32 i = 0; i < ctCols; i++)
            {
                colSort = dgSeq.ColumnFromDisplayIndex(i).SortDirection.ToString();

                if (colSort != "")
                {
                    colHddr = dgSeq.ColumnFromDisplayIndex(i).Header.ToString();

                    if (sort != "")
                        sort += "; ";

                    sort += colHddr + "(" + colSort + ")";
                }
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Write out the title and selection criteria details.            //
            //                                                                //
            //----------------------------------------------------------------//

            if (sort != "")
            {
                if (!selHddrStarted)
                {
                    ReportCore.tableHddrPair (writer, rptFileFmt);

                    selHddrStarted = true;
                }

                ReportCore.tableRowPair (writer, rptFileFmt,
                        "Sort", sort,
                       _colSpanNone, _colSpanNone,
                        _maxSizeNameTag, maxLineLen,
                       _flagNone, _flagNone, _flagNone);
            }

            if (infoType == ToolCommonData.eToolSubIds.PCL)
            {
                ReportCore.hddrTitle (writer, rptFileFmt, false,
                                      "PCL sequence list:");

                if (flagPCLSeqControl)
                {
                    if (! selHddrStarted)
                    {
                        ReportCore.tableHddrPair (writer, rptFileFmt);

                        selHddrStarted = true;
                    }

                    ReportCore.tableRowPair (writer, rptFileFmt,
                                         "Include", "Control Codes",
                                         _colSpanNone, _colSpanNone,
                                         _maxSizeNameTag, maxLineLen,
                                         _flagNone, _flagNone, _flagNone);
                }

                if (flagPCLSeqSimple)
                {
                    if (!selHddrStarted)
                    {
                        ReportCore.tableHddrPair (writer, rptFileFmt);

                        selHddrStarted = true;
                    }

                    ReportCore.tableRowPair (writer, rptFileFmt,
                                         "Include", "Simple escape sequences",
                                         _colSpanNone, _colSpanNone,
                                         _maxSizeNameTag, maxLineLen,
                                         _flagNone, _flagNone, _flagNone);
                }

                if (flagPCLSeqComplex)
                {
                    if (!selHddrStarted)
                    {
                        ReportCore.tableHddrPair (writer, rptFileFmt);

                        selHddrStarted = true;
                    }

                    ReportCore.tableRowPair (writer, rptFileFmt,
                                         "Include",
                                         "Complex (parameterised) escape sequences",
                                         _colSpanNone, _colSpanNone,
                                         _maxSizeNameTag, maxLineLen,
                                         _flagNone, _flagNone, _flagNone);
                }

                if (!selHddrStarted)
                {
                    ReportCore.tableHddrPair (writer, rptFileFmt);

                    selHddrStarted = true;
                }

                ReportCore.tableRowPair (writer, rptFileFmt,
                                     "Select",
                                     ((flagPCLOptDiscrete == true) ?
                                        "Show" : "Do not show") +
                                     " discrete values for enumerated sequences",
                                     _colSpanNone, _colSpanNone,
                                     _maxSizeNameTag, maxLineLen,
                                     _flagNone, _flagNone, _flagNone);

                ReportCore.tableRowPair (writer, rptFileFmt,
                                     "Select",
                                     ((flagPCLOptObsolete == true) ?
                                        "Show" : "Do not show") +
                                     " obsolete sequences",
                                     _colSpanNone, _colSpanNone,
                                     _maxSizeNameTag, maxLineLen,
                                     _flagNone, _flagNone, _flagNone);
            }
            else if (infoType == ToolCommonData.eToolSubIds.HPGL2)
            {
                ReportCore.hddrTitle (writer, rptFileFmt, false,
                                      "HP-GL/2 command list:");
            }
            else if (infoType == ToolCommonData.eToolSubIds.PCLXLTags)
            {
                ReportCore.hddrTitle (writer, rptFileFmt, false,
                                      "PCL XL tag list:");

                if (flagPCLXLTagDataType)
                {
                    if (!selHddrStarted)
                    {
                        ReportCore.tableHddrPair (writer, rptFileFmt);

                        selHddrStarted = true;
                    }

                    ReportCore.tableRowPair (writer, rptFileFmt,
                       "Include",
                       "Data Type tags",
                       _colSpanNone, _colSpanNone,
                       _maxSizeNameTag, maxLineLen,
                       _flagNone, _flagNone, _flagNone);
                }

                if (flagPCLXLTagAttribute)
                {
                    if (!selHddrStarted)
                    {
                        ReportCore.tableHddrPair (writer, rptFileFmt);

                        selHddrStarted = true;
                    }

                    ReportCore.tableRowPair (writer, rptFileFmt,
                       "Include",
                       "Attribute tags",
                       _colSpanNone, _colSpanNone,
                       _maxSizeNameTag, maxLineLen,
                       _flagNone, _flagNone, _flagNone);
                }

                if (flagPCLXLTagOperator)
                {
                    if (!selHddrStarted)
                    {
                        ReportCore.tableHddrPair (writer, rptFileFmt);

                        selHddrStarted = true;
                    }

                    ReportCore.tableRowPair (writer, rptFileFmt,
                       "Include",
                       "Operator tags",
                       _colSpanNone, _colSpanNone,
                       _maxSizeNameTag, maxLineLen,
                       _flagNone, _flagNone, _flagNone);
                }

                if (flagPCLXLTagAttrDef)
                {
                    if (!selHddrStarted)
                    {
                        ReportCore.tableHddrPair (writer, rptFileFmt);

                        selHddrStarted = true;
                    }

                    ReportCore.tableRowPair (writer, rptFileFmt,
                       "Include",
                       "Attribute Definer tags",
                       _colSpanNone, _colSpanNone,
                       _maxSizeNameTag, maxLineLen,
                       _flagNone, _flagNone, _flagNone);
                }

                if (flagPCLXLTagOperator)
                {
                    if (!selHddrStarted)
                    {
                        ReportCore.tableHddrPair (writer, rptFileFmt);

                        selHddrStarted = true;
                    }

                    ReportCore.tableRowPair (writer, rptFileFmt,
                       "Include",
                       "Embedded Data Length tags",
                       _colSpanNone, _colSpanNone,
                       _maxSizeNameTag, maxLineLen,
                       _flagNone, _flagNone, _flagNone);
                }

                if (flagPCLXLTagWhitespace)
                {
                    if (!selHddrStarted)
                    {
                        ReportCore.tableHddrPair (writer, rptFileFmt);

                        selHddrStarted = true;
                    }

                    ReportCore.tableRowPair (writer, rptFileFmt,
                       "Include",
                       "Whitespace tags",
                       _colSpanNone, _colSpanNone,
                       _maxSizeNameTag, maxLineLen,
                       _flagNone, _flagNone, _flagNone);
                }

                if (!selHddrStarted)
                {
                    ReportCore.tableHddrPair (writer, rptFileFmt);

                    selHddrStarted = true;
                }

                ReportCore.tableRowPair (writer, rptFileFmt,
                                     "Select",
                                     ((flagPCLXLOptReserved == true) ?
                                        "Show" : "Do not show") +
                                     " reserved values",
                                     _colSpanNone, _colSpanNone,
                                     _maxSizeNameTag, maxLineLen,
                                     _flagNone, _flagNone, _flagNone);
            }
            else if (infoType == ToolCommonData.eToolSubIds.PCLXLEnums)
            {
                ReportCore.hddrTitle (writer, rptFileFmt, false,
                                      "PCL XL enumeration list:");
            }
            else if (infoType == ToolCommonData.eToolSubIds.PJLCmds)
            {
                ReportCore.hddrTitle (writer, rptFileFmt, false,
                                      "PJL command list:");
            }
            else if (infoType == ToolCommonData.eToolSubIds.PMLTags)
            {
                ReportCore.hddrTitle (writer, rptFileFmt, false,
                                      "PML tag list:");

                if (flagPMLTagDataType)
                {
                    if (!selHddrStarted)
                    {
                        ReportCore.tableHddrPair (writer, rptFileFmt);

                        selHddrStarted = true;
                    }

                    ReportCore.tableRowPair (writer, rptFileFmt,
                       "Include",
                       "Data Type tags",
                       _colSpanNone, _colSpanNone,
                       _maxSizeNameTag, maxLineLen,
                       _flagNone, _flagNone, _flagNone);
                }

                if (flagPMLTagAction)
                {
                    if (!selHddrStarted)
                    {
                        ReportCore.tableHddrPair (writer, rptFileFmt);

                        selHddrStarted = true;
                    }

                    ReportCore.tableRowPair (writer, rptFileFmt,
                       "Include",
                       "Action tags",
                       _colSpanNone, _colSpanNone,
                       _maxSizeNameTag, maxLineLen,
                       _flagNone, _flagNone, _flagNone);
                }

                if (flagPMLTagOutcome)
                {
                    if (!selHddrStarted)
                    {
                        ReportCore.tableHddrPair (writer, rptFileFmt);

                        selHddrStarted = true;
                    }

                    ReportCore.tableRowPair (writer, rptFileFmt,
                       "Include",
                       "Outcome tags",
                       _colSpanNone, _colSpanNone,
                       _maxSizeNameTag, maxLineLen,
                       _flagNone, _flagNone, _flagNone);
                }
            }
            else if (infoType == ToolCommonData.eToolSubIds.SymbolSets)
            {
                ReportCore.hddrTitle (writer, rptFileFmt, false,
                                      "Symbol Set list:");

                if (flagSymSetMap)
                {
                    if (!selHddrStarted)
                    {
                        ReportCore.tableHddrPair (writer, rptFileFmt);

                        selHddrStarted = true;
                    }

                    ReportCore.tableRowPair (writer, rptFileFmt,
                       "Include",
                       "Character mapping",
                       _colSpanNone, _colSpanNone,
                       _maxSizeNameTag, maxLineLen,
                       _flagNone, _flagNone, _flagNone);
                }
            }
            else if (infoType == ToolCommonData.eToolSubIds.Fonts)
            {
                ReportCore.hddrTitle (writer, rptFileFmt, false,
                                      "Font list:");

                if (flagSymSetList)
                {
                    if (!selHddrStarted)
                    {
                        ReportCore.tableHddrPair (writer, rptFileFmt);

                        selHddrStarted = true;
                    }

                    ReportCore.tableRowPair (writer, rptFileFmt,
                       "Include",
                       "Supported Symbol Sets",
                       _colSpanNone, _colSpanNone,
                       _maxSizeNameTag, maxLineLen,
                       _flagNone, _flagNone, _flagNone);
                }
            }
            else if (infoType == ToolCommonData.eToolSubIds.PaperSizes)
            {
                ReportCore.hddrTitle (writer, rptFileFmt, false,
                                      "Paper size list:");

            }
            else if (infoType == ToolCommonData.eToolSubIds.PrescribeCmds)
            {
                ReportCore.hddrTitle (writer, rptFileFmt, false,
                                      "Prescribe command list:");
            }

            if (selHddrStarted)
            {
                ReportCore.tableClose (writer, rptFileFmt);
            }
        }
    }
}
