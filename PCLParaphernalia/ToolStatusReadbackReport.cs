using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides the Status Readback 'save report' function.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class ToolStatusReadbackReport
    {
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

        public static void generate (ReportCore.eRptFileFmt rptFileFmt,
                                     TextBox txtReply,
                                     ref String saveFilename)
        {
            Object stream = null;
            Object writer = null;

            Boolean OK = false;

            String saveFolder = null,
                   fileExt;

            ToolCommonFunctions.getFolderName (saveFilename,
                                               ref saveFolder);

            if (rptFileFmt == ReportCore.eRptFileFmt.html)
                fileExt = "html";
            else if (rptFileFmt == ReportCore.eRptFileFmt.xml)
                fileExt = "xml";
            else
                fileExt = "txt";

            saveFilename = saveFolder + "\\SR_Resp." + fileExt;

            OK = ReportCore.docOpen (rptFileFmt,
                                     ref saveFilename,
                                     ref stream,
                                     ref writer);
            if (OK)
            {
                ReportCore.docInitialise (rptFileFmt, writer, false, true,
                                          0, null,
                                          null, null);

                ReportCore.hddrTitle (writer, rptFileFmt, false,
                                      "*** Status Readback response data ***");

                reportBody (rptFileFmt, writer, txtReply);

                ReportCore.docFinalise (rptFileFmt, writer);

                ReportCore.docClose (rptFileFmt, stream, writer);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e p o r t B o d y                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write details of response to report file.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void reportBody(
            ReportCore.eRptFileFmt rptFileFmt,
            Object writer,
            TextBox txtReply)
        {
            const Int32 maxLineLen = 127;

            Int32 ct;

            ReportCore.lineBlockOpen (writer, rptFileFmt);

            ct = txtReply.LineCount;

            for (Int32 i = 0; i < ct; i++)
            {
                String line = txtReply.GetLineText(i);

                String removedCC = line.Replace ("\r\n", "")    // not "<CR><LF>")
                                       .Replace ("\n",   "")    // not "<LF>")
                                       .Replace ("\r",   "")    // not "<CR>")
                                       .Replace ("\f", "<FF>")
                                       .Replace ("\x1b", "<Esc>");

                ReportCore.lineItem (writer, rptFileFmt, removedCC, maxLineLen,
                                     false);
            }

            ReportCore.lineBlockClose (writer, rptFileFmt);
        }
    }
}
