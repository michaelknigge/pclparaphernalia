using Microsoft.Win32;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Xml;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides the core Report functions.
    /// 
    /// © Chris Hutchinson 2017
    /// 
    /// </summary>

    static class ReportCore
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const Int32 _lcSep = PrnParseConstants.cColSeparatorLen;

        public static String _chkMarkBoxSymFalse = '\u2610'.ToString ();
        public static String _chkMarkBoxSymTrue  = '\u2611'.ToString ();

        public static String _chkMarkTxtSymFalse = '\x2d'.ToString ();
        public static String _chkMarkTxtSymTrue  = '\x2b'.ToString ();

        public static String _chkMarkTextFalse = "false";
        public static String _chkMarkTextTrue  = "true ";

        public enum eRptFileFmt : Byte
        {
            text,
            html,
            xml,
            NA
        }

        public enum eRptChkMarks : Byte
        {
            text,
            txtsym,
            boxsym,
            NA
        }

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Fields (class variables).                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d o c C l o s e                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Close report stream.                                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void docClose (eRptFileFmt rptFileFmt,
                                     Object stream,
                                     Object writer)
        {
            if (rptFileFmt == eRptFileFmt.html)
            {
                HtmlTextWriter htmlWriter = (HtmlTextWriter) writer;
                htmlWriter.Close ();
                writer = null;

                StreamWriter htmlStream = (StreamWriter) stream;

                htmlStream.Close ();
                stream = null;
            }
            else if (rptFileFmt == eRptFileFmt.xml)
            {
                XmlWriter xmlWriter = (XmlWriter)writer;
                xmlWriter.Close ();
                writer = null;

                StreamWriter xmlStream = (StreamWriter)stream;

                xmlStream.Close ();
                stream = null;
            }
            else
            {
                StreamWriter txtWriter = (StreamWriter)writer;

                txtWriter.Close ();
                writer = null;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d o c F i n a l i s e                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write closing elements to report document.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void docFinalise (eRptFileFmt rptFileFmt,
                                        Object writer)
        {
            if (rptFileFmt == eRptFileFmt.html)
                docFinaliseHtml ((HtmlTextWriter)writer);
            else if (rptFileFmt == eRptFileFmt.xml)
                docFinaliseXml ((XmlWriter)writer);
            else
                docFinaliseText ((StreamWriter)writer);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d o c F i n a l i s e H t m l                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write closing elements to report document (html format).           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void docFinaliseHtml (HtmlTextWriter htmlWriter)
        {
            htmlWriter.RenderBeginTag ("p");
            htmlWriter.Write ("*** End of Report ***");
            htmlWriter.RenderEndTag ();    // </p>

            htmlWriter.RenderEndTag ();    // </body>

            htmlWriter.RenderEndTag ();    // </html>
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d o c F i n a l i s e T e x t                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write closing elements to report document (text format).           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void docFinaliseText (StreamWriter txtWriter)
        {
            txtWriter.WriteLine ("");
            txtWriter.WriteLine ("*** End of Report ***");
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d o c F i n a l i s e X m l                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write closing elements to report document (xml format).            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void docFinaliseXml (XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement ("trailer");

            xmlWriter.WriteStartElement ("text");
            xmlWriter.WriteString ("*** End of Report ***");
            xmlWriter.WriteEndElement ();   // </text>

            xmlWriter.WriteEndElement ();   // </trailer>

            xmlWriter.WriteEndDocument ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d o c I n i t i a l i s e                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write opening elements to report document.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void docInitialise (eRptFileFmt rptFileFmt,
                                          Object       writer,
                                          Boolean      useTables,
                                          Boolean      useLines,
                                          Int32        ctRowClrStyles,
                                          String []    rowClasses,
                                          String []    rowClrBack,
                                          String []    rowClrFore)
        {
            if (rptFileFmt == eRptFileFmt.html)
                docInitialiseHtml ((HtmlTextWriter)writer,
                                   ctRowClrStyles,
                                   rowClasses,
                                   rowClrBack,
                                   rowClrFore);
            else if (rptFileFmt == eRptFileFmt.xml)
                docInitialiseXml ((XmlWriter)writer,
                                   useTables,
                                   useLines,
                                   ctRowClrStyles,
                                   rowClasses,
                                   rowClrBack,
                                   rowClrFore);
            else
                docInitialiseText ((StreamWriter)writer);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d o c I n i t i a l i s e H t m l                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write opening elements to report document (html format).           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void docInitialiseHtml (HtmlTextWriter htmlWriter,
                                              Int32 ctRowClrStyles,
                                              String[] rowClasses,
                                              String[] rowClrBack,
                                              String[] rowClrFore)
        {
            //----------------------------------------------------------------//
            //                                                                //
            // Write out the html header tags.                                //
            //                                                                //
            //----------------------------------------------------------------//

            htmlWriter.WriteLine ("<!doctype html>");

            htmlWriter.RenderBeginTag ("html");

            htmlWriter.RenderBeginTag ("head");

            htmlWriter.AddAttribute ("charset", "utf-8");
            htmlWriter.RenderBeginTag ("meta");
            htmlWriter.RenderEndTag ();
            htmlWriter.WriteLine ("");

            //----------------------------------------------------------------//
            //                                                                //
            // Write the standard style definitions, and (if required) the    //
            // optional row colour coding style definitions.                  //
            //                                                                //
            //----------------------------------------------------------------//

            htmlWriter.RenderBeginTag ("style");

            docInitStyles (eRptFileFmt.html, htmlWriter, ctRowClrStyles,
                           rowClasses, rowClrBack, rowClrFore);

            //----------------------------------------------------------------//
            //                                                                //
            // Write out the html header end tags and start body element.     //
            //                                                                //
            //----------------------------------------------------------------//

            htmlWriter.RenderEndTag ();    // </style>

            htmlWriter.RenderEndTag ();    // </head>
            htmlWriter.WriteLine ("");

            htmlWriter.RenderBeginTag ("body");
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d o c I n i t i a l i s e T e x t                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write opening elements to report document (text format).           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void docInitialiseText (StreamWriter txtWriter)
        {
            // ******* nothing to do *******
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d o c I n i t i a l i s e X m l                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write opening elements to report document (xml format).            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void docInitialiseXml (XmlWriter xmlWriter,
                                             Boolean useTables,
                                             Boolean useLines,
                                             Int32 ctRowClrStyles,
                                             String[] rowClasses,
                                             String[] rowClrBack,
                                             String[] rowClrFore)
        {
            xmlWriter.WriteStartDocument ();

            //----------------------------------------------------------------//
            //                                                                //
            // Write the Processing Instruction node.                         //
            //                                                                //
            //----------------------------------------------------------------//

            xmlWriter.WriteProcessingInstruction (
                "xml-stylesheet",
                "type=\"text/xsl\" href=\"#stylesheet\"");

            //----------------------------------------------------------------//
            //                                                                //
            // Write the DocumentType node.                                   //
            //                                                                //
            //----------------------------------------------------------------//

            xmlWriter.WriteDocType ("report", null, null,
                                    "<!ATTLIST xsl:stylesheet id ID #REQUIRED>");

            //----------------------------------------------------------------//
            //                                                                //
            // Write the root element.                                        //
            //                                                                //
            //----------------------------------------------------------------//

            xmlWriter.WriteStartElement ("report");

            xmlWriter.WriteComment ("Start XSL");

            //----------------------------------------------------------------//
            //                                                                //
            // Start stylesheet definition.                                   //
            //                                                                //
            //----------------------------------------------------------------//

            xmlWriter.WriteStartElement ("xsl", "stylesheet",
                                         "http://www.w3.org/1999/XSL/Transform");
            xmlWriter.WriteAttributeString ("id", "stylesheet");
            xmlWriter.WriteAttributeString ("version", "1.0");

            xmlWriter.WriteStartElement ("xsl", "output", null);
            xmlWriter.WriteAttributeString ("indent", "yes");
            xmlWriter.WriteAttributeString ("method", "html");
            xmlWriter.WriteEndElement ();

            xmlWriter.WriteStartElement ("xsl", "template", null);
            xmlWriter.WriteAttributeString ("match", "xsl:stylesheet");
            xmlWriter.WriteEndElement ();

            //----------------------------------------------------------------//
            //                                                                //
            // Template: styles                                               //
            //                                                                //
            //----------------------------------------------------------------//

            xmlWriter.WriteComment ("template: styles");

            xmlWriter.WriteStartElement ("xsl", "template", null);
            xmlWriter.WriteAttributeString ("match", "/report");

            xmlWriter.WriteStartElement ("html");
            xmlWriter.WriteStartElement ("head");
            xmlWriter.WriteStartElement ("style");
     //     xmlWriter.WriteStartElement ("xsl", "comment", null);

            docInitStyles (eRptFileFmt.xml, xmlWriter, ctRowClrStyles,
                           rowClasses, rowClrBack, rowClrFore);

     //     xmlWriter.WriteEndElement ();               // </xsl:comment>
            xmlWriter.WriteEndElement ();               // </style
            xmlWriter.WriteEndElement ();               // </head>

            xmlWriter.WriteStartElement ("body");
            xmlWriter.WriteStartElement ("xsl", "apply-templates", null);
            xmlWriter.WriteEndElement ();               // 

            xmlWriter.WriteEndElement ();               // </body
            xmlWriter.WriteEndElement ();               // </html>
            xmlWriter.WriteEndElement ();               // </xsl:template>

            //----------------------------------------------------------------//
            //                                                                //
            // Template: report header                                        //
            //                                                                //
            //----------------------------------------------------------------//

            xmlWriter.WriteComment ("template: header");

            xmlWriter.WriteStartElement ("xsl", "template", null);
            xmlWriter.WriteAttributeString ("match", "/report/header");

            xmlWriter.WriteStartElement ("p");
            xmlWriter.WriteAttributeString ("class", "title");

            xmlWriter.WriteStartElement ("xsl", "value-of", null);
            xmlWriter.WriteAttributeString ("select", "title");
            xmlWriter.WriteEndElement ();               // </xsl:value-of>
            xmlWriter.WriteEndElement ();               // </p>

            xmlWriter.WriteEndElement ();               // </xsl:template>

            if (useLines)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Template: report lineblock                                 //
                //                                                            //
                //------------------------------------------------------------//

                xmlWriter.WriteStartElement ("xsl", "template", null);
                xmlWriter.WriteAttributeString ("match", "/report/lineblock");

                xmlWriter.WriteStartElement ("xsl", "for-each", null);
                xmlWriter.WriteAttributeString ("select", "item/value");

                xmlWriter.WriteStartElement ("p");

                xmlWriter.WriteStartElement ("xsl", "value-of", null);
                xmlWriter.WriteAttributeString ("select", ".");

                xmlWriter.WriteEndElement ();       // </xsl:value-of>
                xmlWriter.WriteEndElement ();       // </p>
                xmlWriter.WriteEndElement ();       // </xsl:for-each>

                xmlWriter.WriteEndElement ();               // </xsl:template>
            }

            if (useTables)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Template: report tabledata                                 //
                //                                                            //
                //------------------------------------------------------------//

                xmlWriter.WriteComment ("template: tabledata");

                xmlWriter.WriteStartElement ("xsl", "template", null);
                xmlWriter.WriteAttributeString ("match", "/report/tabledata");

                xmlWriter.WriteStartElement ("table");

                //------------------------------------------------------------//

                xmlWriter.WriteComment ("template: tabledata hddr");

                xmlWriter.WriteStartElement ("tr");

                xmlWriter.WriteStartElement ("xsl", "for-each", null);
                xmlWriter.WriteAttributeString ("select", "hddr/col");
                xmlWriter.WriteStartElement ("th");
                xmlWriter.WriteStartElement ("xsl", "attribute", null);
                xmlWriter.WriteAttributeString ("name", "class");
                xmlWriter.WriteStartElement ("xsl", "value-of", null);
                xmlWriter.WriteAttributeString ("select", "../@hddrstyle");
                xmlWriter.WriteEndElement ();       // </xsl:value-of>
                xmlWriter.WriteEndElement ();       // </xsl:attribute>

                xmlWriter.WriteStartElement ("xsl", "value-of", null);
                xmlWriter.WriteAttributeString ("select", ".");

                xmlWriter.WriteEndElement ();       // </xsl:value-of>
                xmlWriter.WriteEndElement ();       // </th>
                xmlWriter.WriteEndElement ();       // </xsl:for-each>

                xmlWriter.WriteEndElement ();       // </tr>

                //------------------------------------------------------------//

                xmlWriter.WriteComment ("template: tabledata item");

                xmlWriter.WriteStartElement ("xsl", "for-each", null);
                xmlWriter.WriteAttributeString ("select", "item");

                xmlWriter.WriteStartElement ("tr");
                xmlWriter.WriteStartElement ("xsl", "attribute", null);
                xmlWriter.WriteAttributeString ("name", "class");
                xmlWriter.WriteStartElement ("xsl", "value-of", null);
                xmlWriter.WriteAttributeString ("select", "@rowType");
                xmlWriter.WriteEndElement ();       // </xsl:value-of>
                xmlWriter.WriteEndElement ();       // </xsl:attribute>

                xmlWriter.WriteStartElement ("xsl", "for-each", null);
                xmlWriter.WriteAttributeString ("select", "*");
                xmlWriter.WriteStartElement ("td");

                xmlWriter.WriteStartElement ("xsl", "attribute", null);
                xmlWriter.WriteAttributeString ("name", "class");
                xmlWriter.WriteStartElement ("xsl", "value-of", null);
                xmlWriter.WriteAttributeString (
                    "select", "concat(../@padType, ' ', @txtfmt)");
                xmlWriter.WriteEndElement ();       // </xsl:value-of>
                xmlWriter.WriteEndElement ();       // </xsl:attribute>

                xmlWriter.WriteStartElement ("xsl", "attribute", null);
                xmlWriter.WriteAttributeString ("name", "colspan");
                xmlWriter.WriteStartElement ("xsl", "value-of", null);
                xmlWriter.WriteAttributeString ("select", "@colspan");
                xmlWriter.WriteEndElement ();       // </xsl:value-of>
                xmlWriter.WriteEndElement ();       // </xsl:attribute>

                xmlWriter.WriteStartElement ("xsl", "value-of", null);
                xmlWriter.WriteAttributeString ("select", ".");

                xmlWriter.WriteEndElement ();       // </xsl:value-of>
                xmlWriter.WriteEndElement ();       // </td>
                xmlWriter.WriteEndElement ();       // </xsl:for-each>

                xmlWriter.WriteEndElement ();       // </tr>
                xmlWriter.WriteEndElement ();       // </xsl:for-each>

                //------------------------------------------------------------//

                xmlWriter.WriteEndElement ();       // </table>

                xmlWriter.WriteEndElement ();       // </xsl:template>

                //------------------------------------------------------------//
                //                                                            //
                // Template: report tablepair                                 //
                //                                                            //
                //------------------------------------------------------------//

                xmlWriter.WriteComment ("template: tablepair");

                xmlWriter.WriteStartElement ("xsl", "template", null);
                xmlWriter.WriteAttributeString ("match", "/report/tablepair");

                xmlWriter.WriteStartElement ("table");

                //------------------------------------------------------------//

                xmlWriter.WriteStartElement ("xsl", "for-each", null);
                xmlWriter.WriteAttributeString ("select", "item");

                xmlWriter.WriteStartElement ("tr");

                xmlWriter.WriteStartElement ("xsl", "for-each", null);
                xmlWriter.WriteAttributeString ("select", "*");
                xmlWriter.WriteStartElement ("td");

                xmlWriter.WriteStartElement ("xsl", "attribute", null);
                xmlWriter.WriteAttributeString ("name", "class");
                xmlWriter.WriteStartElement ("xsl", "value-of", null);
                xmlWriter.WriteAttributeString ("select", "../@padType");
                xmlWriter.WriteEndElement ();       // </xsl:value-of>
                xmlWriter.WriteEndElement ();       // </xsl:attribute>

                xmlWriter.WriteStartElement ("xsl", "value-of", null);
                xmlWriter.WriteAttributeString ("select", ".");

                xmlWriter.WriteEndElement ();       // </xsl:value-of>
                xmlWriter.WriteEndElement ();       // </td>
                xmlWriter.WriteEndElement ();       // </xsl:for-each>

                xmlWriter.WriteEndElement ();       // </tr>
                xmlWriter.WriteEndElement ();       // </xsl:for-each>

                //------------------------------------------------------------//

                xmlWriter.WriteEndElement ();       // </table>

                xmlWriter.WriteEndElement ();       // </xsl:template>
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Template: report trailer                                       //
            //                                                                //
            //----------------------------------------------------------------//

            xmlWriter.WriteComment ("template: trailer");

            xmlWriter.WriteStartElement ("xsl", "template", null);
            xmlWriter.WriteAttributeString ("match", "/report/trailer");

            xmlWriter.WriteStartElement ("p");

            xmlWriter.WriteStartElement ("xsl", "value-of", null);
            xmlWriter.WriteAttributeString ("select", "text");
            xmlWriter.WriteEndElement ();               // </xsl:value-of>
            xmlWriter.WriteEndElement ();               // </p>
            xmlWriter.WriteEndElement ();               // </xsl:template>

            //----------------------------------------------------------------//
            //                                                                //
            // end of stylesheet definition                                   //
            //                                                                //
            //----------------------------------------------------------------//

            xmlWriter.WriteEndElement ();               // </xsl:stylesheet>

            //----------------------------------------------------------------//
            //                                                                //
            // Start XML                                                      //
            //                                                                //
            //----------------------------------------------------------------//

            xmlWriter.WriteComment ("Start XML");
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d o c I n i t S t y l e s                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write standard style definitions and (if required) the optional    //
        // row colour coding style definitions to report document.            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void docInitStyles (eRptFileFmt rptFileFmt,
                                          Object       writer,
                                          Int32        ctRowClrStyles,
                                          String[]     rowClasses,
                                          String[]     rowClrBack,
                                          String[]     rowClrFore)
        {
            String[] stdStyles =
            {
                "p",
                "{",
                "\t" + "font-family: Courier; ",
                "\t" + "font - size: 100 %; ",
                "\t" + "margin-top: 0em;",
                "\t" + "margin-bottom: 0em;",
                "\t" + "white-space: pre;",
                "}",

                "p.title",
                "{",
                "\t" + "font-weight: bold;",
                "}",

                "table",
                "{",
                "\t" + "font-family: Courier;",
                "\t" + "font-size: 100 %;",
                "\t" + "margin-left: 0em;",
                "\t" + "margin-top: 1em;",
                "\t" + "margin-bottom: 1em;",
                "\t" + "white-space: pre;",
                "}",

                "th",
                "{",
                "\t" + "font-style: italic;",
                "\t" + "font-weight: normal;",
                "\t" + "text-align: left;",
                "\t" + "text-decoration: underline;",
                "\t" + "padding-top: 0em;",
                "\t" + "padding-bottom: 0em;",
                "\t" + "vertical-align: top;",
                "}",

                "th.plain",
                "{",
                "\t" + "font-style: normal;",
                "\t" + "font-weight: normal;",
                "\t" + "text-align: left;",
                "\t" + "text-decoration: none;",
                "\t" + "padding-top: 0em;",
                "\t" + "padding-bottom: 0em;",
                "\t" + "vertical-align: top;",
                "}",

                "td",
                "{",
                "\t" + "padding-right: 1em;",
                "\t" + "padding-top: 0em;",
                "\t" + "padding-bottom: 0em;",
                "\t" + "vertical-align: top;",
                "}",

                ".fmtAdorn",
                "{",
                "\t" + "font-style: italic;",
                "\t" + "font-weight: normal;",
                "\t" + "text-align: left;",
                "\t" + "text-decoration: underline;",
                "}",

                ".fmtPlain",
                "{",
                "\t" + "font-style: normal;",
                "\t" + "font-weight: normal;",
                "\t" + "text-align: left;",
                "\t" + "text-decoration: none;",
                "}",

                ".padAnte",
                "{",
                "\t" + "padding-top: 1em;",
                "\t" + "padding-bottom: 0em;",
                "\t" + "vertical-align: top;",
                "}",

                ".padAntePost",
                "{",
                "\t" + "padding-top: 1em;",
                "\t" + "padding-bottom: 1em;",
                "\t" + "vertical-align: top;",
                "}",

                ".padPost",
                "{",
                "\t" + "padding-top: 0em;",
                "\t" + "padding-bottom: 1em;",
                "\t" + "vertical-align: top;",
                "}"
            };

            if (rptFileFmt == eRptFileFmt.html)
                docInitStylesHtml (
                    (HtmlTextWriter)writer, stdStyles, ctRowClrStyles,
                    rowClasses, rowClrBack, rowClrFore);
            else if (rptFileFmt == eRptFileFmt.xml)
                docInitStylesXml (
                    (XmlWriter)writer, stdStyles, ctRowClrStyles,
                    rowClasses, rowClrBack, rowClrFore);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d o c I n i t S t y l e s H t m l                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write standard style definitions and (if required) the optional    //
        // row colour coding style definitions to report document (html       //
        // format).                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void docInitStylesHtml (HtmlTextWriter htmlWriter,
                                              String[]       stdStyles,
                                              Int32          ctRowClrStyles,
                                              String[]       rowClasses,
                                              String[]       rowClrBack,
                                              String[]       rowClrFore)
        {
            //----------------------------------------------------------------//
            //                                                                //
            // Standard style definitions.                                    //
            //                                                                //
            //----------------------------------------------------------------//

            Int32 ctLines = stdStyles.Length;

            for (Int32 i = 0; i < ctLines; i++)
            {
                htmlWriter.WriteLine (stdStyles[i]);
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Optional row colour coding style definitions.                  //
            //                                                                //
            //----------------------------------------------------------------//

            if (ctRowClrStyles > 0)
            {
                htmlWriter.WriteLine ("");

                for (Int32 i = 0; i < ctRowClrStyles; i++)
                {
                    htmlWriter.Write ("tr." +
                                      rowClasses[i] + " {");
                    htmlWriter.Write (" background-color: " +
                                      rowClrBack[i] + ";");
                    htmlWriter.Write (" color: " +
                                      rowClrFore[i] + ";");

                    if (i == ctRowClrStyles)
                        htmlWriter.Write (" }");
                    else
                        htmlWriter.WriteLine (" }");
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d o c I n i t S t y l e s X m l                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write standard style definitions and (if required) the optional    //
        // row colour coding style definitions to report document (xml        //
        // format).                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void docInitStylesXml (XmlWriter xmlWriter,
                                             String[]  stdStyles,
                                             Int32     ctRowClrStyles,
                                             String[]  rowClasses,
                                             String[]  rowClrBack,
                                             String[]  rowClrFore)
        {
            //----------------------------------------------------------------//
            //                                                                //
            // Standard style definitions.                                    //
            //                                                                //
            //----------------------------------------------------------------//

            Int32 ctLines = stdStyles.Length;

            xmlWriter.WriteString ("\r\n");

            for (Int32 i = 0; i < ctLines; i++)
            {
                xmlWriter.WriteString ("\t\t\t" + stdStyles[i] + "\r\n");
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Optional row colour coding style definitions.                  //
            //                                                                //
            //----------------------------------------------------------------//

            if (ctRowClrStyles > 0)
            {
                for (Int32 i = 0; i < ctRowClrStyles; i++)
                {
                    xmlWriter.WriteString ("\t\t\t" + "tr." +
                                           rowClasses[i] + " {");
                    xmlWriter.WriteString (" background-color: " +
                                           rowClrBack[i] + ";");
                    xmlWriter.WriteString (" color: " +
                                           rowClrFore[i] + ";");
                    xmlWriter.WriteString (" }\r\n");
                }
            }

            xmlWriter.WriteString ("\t\t");
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d o c O p e n                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Open report stream.                                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean docOpen (eRptFileFmt rptFileFmt,
                                       ref String saveFilename,
                                       ref Object stream,
                                       ref Object writer)
        {
            SaveFileDialog saveDialog = ToolCommonFunctions.createSaveFileDialog(saveFilename);

            if (rptFileFmt == eRptFileFmt.html)
            {
                saveDialog.Filter = "Html Files | *.html";
                saveDialog.DefaultExt = "html";
            }
            else if (rptFileFmt == eRptFileFmt.xml)
            {
                saveDialog.Filter = "Xml Files | *.xml";
                saveDialog.DefaultExt = "xml";
            }
            else
            {
                saveDialog.Filter = "Text Files | *.txt";
                saveDialog.DefaultExt = "txt";
            }

            Nullable<Boolean> dialogResult = saveDialog.ShowDialog ();
            Boolean fileOpen = false;

            if (dialogResult == true)
            {
                saveFilename = saveDialog.FileName;

                if (rptFileFmt == eRptFileFmt.html)
                {
                    stream = new StreamWriter (saveFilename);

                    if (stream != null)
                    {
                        writer = new HtmlTextWriter ((StreamWriter)stream);
                        fileOpen = true;
                    }
                }
                else if (rptFileFmt == eRptFileFmt.xml)
                {
                    stream = new StreamWriter (saveFilename);

                    if (stream != null)
                    {
                        XmlWriterSettings settings = new XmlWriterSettings ();
                        settings.Encoding = Encoding.UTF8;
                        settings.Indent = true;

                        writer = XmlWriter.Create ((StreamWriter) stream, settings);
                        fileOpen = true;
                    }
                }
                else
                {
                    stream = null;
                    writer = new StreamWriter (saveFilename);

                    if (writer != null)
                    {
                        fileOpen = true;
                    }
                }
            }

            return fileOpen;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // h d d r C l o s e                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Close header section.                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void hddrClose (Object writer,
                                      eRptFileFmt rptFileFmt)
        {
            if (rptFileFmt == eRptFileFmt.html)
                hddrCloseHtml ((HtmlTextWriter)writer);
            else if (rptFileFmt == eRptFileFmt.xml)
                hddrCloseXml ((XmlWriter)writer);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // h d d r C l o s e H t m l                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write close tag in html format.                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void hddrCloseHtml (HtmlTextWriter htmlWriter)
        {
            htmlWriter.RenderEndTag ();     // </table>
        }
  
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // h d d r C l o s e X m l                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write close tag in xml format.                                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void hddrCloseXml (XmlWriter xmlWriter)
        {
            xmlWriter.WriteEndElement ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // h d d r T i t l e                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write report header title line.                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void hddrTitle (Object writer,
                                      eRptFileFmt rptFileFmt,
                                      Boolean subHddr,
                                      String txtVal)
        {
            if (rptFileFmt == eRptFileFmt.html)
                hddrTitleHtml ((HtmlTextWriter)writer, txtVal);
            else if (rptFileFmt == eRptFileFmt.xml)
                hddrTitleXml ((XmlWriter)writer, txtVal);
            else
                hddrTitleText ((StreamWriter)writer, subHddr, txtVal);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // h d d r T i t l e H t m l                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write report header title line in html format.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void hddrTitleHtml (HtmlTextWriter htmlWriter,
                                           String txtVal)
        {
            htmlWriter.AddAttribute ("class", "title");
            htmlWriter.RenderBeginTag ("p");
            htmlWriter.WriteEncodedText (txtVal);
            htmlWriter.RenderEndTag ();
            htmlWriter.WriteLine ("");
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // h d d r T i t l e T e x t                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write report header title line in text format.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void hddrTitleText (StreamWriter txtWriter,
                                           Boolean subHddr,
                                           String txtVal)
        {
            if (subHddr)
                txtWriter.WriteLine ("");

            txtWriter.WriteLine (txtVal);

            if (subHddr)
            {
                Int32 len = txtVal.Length;

                txtWriter.WriteLine ("-".PadRight (len, '-'));
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // h d d r T i t l e X m l                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write report header title line in xml format.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void hddrTitleXml (XmlWriter xmlWriter,
                                          String txtVal)
        {
            xmlWriter.WriteStartElement ("header");

            xmlWriter.WriteStartElement ("title");

            xmlWriter.WriteString (txtVal);

            xmlWriter.WriteEndElement ();           // </title>

            xmlWriter.WriteEndElement ();           // </header>
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l i n e B l o c k C l o s e                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write line block close tag.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void lineBlockClose (Object writer,
                                          eRptFileFmt rptFileFmt)
        {
            if (rptFileFmt == eRptFileFmt.html)
                lineBlockCloseHtml ((HtmlTextWriter)writer);
            else if (rptFileFmt == eRptFileFmt.xml)
                lineBlockCloseXml ((XmlWriter)writer);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l i n e B l o c k C l o s e H t m l                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write close tag in html format.                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void lineBlockCloseHtml (HtmlTextWriter htmlWriter)
        {
            htmlWriter.RenderEndTag ();
            htmlWriter.WriteLine ("");
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l i n e B l o c k C l o s e X m l                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write close tag in xml format.                                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void lineBlockCloseXml (XmlWriter xmlWriter)
        {
            xmlWriter.WriteEndElement ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l i n e B l o c k O p e n                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write line block open tag.                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void lineBlockOpen (Object writer,
                                          eRptFileFmt rptFileFmt)
        {
            String tag = "lineblock"; 

            if (rptFileFmt == eRptFileFmt.html)
                lineBlockOpenHtml ((HtmlTextWriter)writer, tag);
            else if (rptFileFmt == eRptFileFmt.xml)
                lineBlockOpenXml ((XmlWriter)writer, tag);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l i n e B l o c k O p e n H t m l                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write line block  open tag in html format.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void lineBlockOpenHtml (HtmlTextWriter htmlWriter,
                                               String tag)
        {
            htmlWriter.RenderBeginTag (tag);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l i n e B l o c k O p e n X m l                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write line block open tag in xml format.                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void lineBlockOpenXml (XmlWriter xmlWriter,
                                              String txtName)
        {
            xmlWriter.WriteStartElement (txtName);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l i n e I t e m                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write item containing just a value component.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void lineItem (Object writer,
                                     eRptFileFmt rptFileFmt,
                                     String txtVal,
                                     Int32 sizeVal,
                                     Boolean firstItem)
        {
            if (rptFileFmt == eRptFileFmt.html)
                lineItemHtml ((HtmlTextWriter)writer,
                              txtVal, sizeVal, firstItem);
            else if (rptFileFmt == eRptFileFmt.xml)
                lineItemXml ((XmlWriter)writer, txtVal);
            else
                lineItemText ((StreamWriter)writer, txtVal,
                              sizeVal, firstItem);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l i n e I t e m H t m l                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write line item in html format.                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void lineItemHtml (HtmlTextWriter htmlWriter,
                                          String txtVal,
                                          Int32 maxSizeVal,
                                          Boolean firstItem)
        {
            Int32 valPos,
                  valLen;

            valLen = txtVal.Length;
            valPos = 0;

            if (firstItem)
            {
                htmlWriter.RenderBeginTag ("p");
                htmlWriter.Write ("&nbsp;");
                htmlWriter.RenderEndTag ();
                htmlWriter.WriteLine ("");
            }

            while (valPos + maxSizeVal < valLen)
            {
                htmlWriter.RenderBeginTag ("p");
                htmlWriter.WriteEncodedText (
                        txtVal.Substring (valPos, maxSizeVal));
                htmlWriter.RenderEndTag ();
                htmlWriter.WriteLine ("");

                valPos = valPos + maxSizeVal;
            }

            if (valPos <= valLen)
            {
                htmlWriter.RenderBeginTag ("p");
                htmlWriter.WriteEncodedText (
                        txtVal.Substring (valPos, valLen - valPos));
                htmlWriter.RenderEndTag ();
                htmlWriter.WriteLine ("");
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l i n e I t e m T e x t                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write line item in text format.                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void lineItemText (StreamWriter txtWriter,
                                          String txtVal,
                                          Int32 maxSizeVal,
                                          Boolean blankBefore)
        {
            Int32 valPos,
                  valLen;

            if (blankBefore)
                txtWriter.WriteLine ();

            valLen = txtVal.Length;
            valPos = 0;

            while (valPos + maxSizeVal < valLen)
            {
                txtWriter.WriteLine (
                        txtVal.Substring (valPos, maxSizeVal));

                valPos = valPos + maxSizeVal;
            }

            if (valPos <= valLen)
            {
                txtWriter.WriteLine (
                        txtVal.Substring (valPos, valLen - valPos));
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l i n e I t e m X m l                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write line item in xml format.                                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void lineItemXml (XmlWriter xmlWriter,
                                         String txtVal)
        {
            xmlWriter.WriteStartElement ("item");

            try
            {
                String validXml = XmlConvert.VerifyXmlChars (txtVal);

                xmlWriter.WriteStartElement ("value");

                xmlWriter.WriteString (validXml);
            }
            catch
            {
                Byte[] bytes = System.Text.Encoding.UTF8.GetBytes (txtVal);

                String base64 = Convert.ToBase64String (bytes);

                xmlWriter.WriteStartElement ("valuebase64");

                xmlWriter.WriteString (base64);
            }

            xmlWriter.WriteEndElement ();

            xmlWriter.WriteEndElement ();   // </item>
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a b l e C l o s e                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Close report table.                                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void tableClose (Object writer,
                                       eRptFileFmt rptFileFmt)
        {
            if (rptFileFmt == eRptFileFmt.html)
                tableCloseHtml ((HtmlTextWriter)writer);
            else if (rptFileFmt == eRptFileFmt.xml)
                tableCloseXml ((XmlWriter)writer);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a b l e C l o s e H t m l                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write close tag in html format.                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void tableCloseHtml (HtmlTextWriter htmlWriter)
        {
            htmlWriter.RenderEndTag ();
            htmlWriter.WriteLine ("");
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a b l e C l o s e X m l                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write close tag in xml format.                                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void tableCloseXml (XmlWriter xmlWriter)
        {
            xmlWriter.WriteEndElement ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a b l e H d d r D a t a                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Open report data table and write header row.                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void tableHddrData (Object writer,
                                          eRptFileFmt rptFileFmt,
                                          Boolean plain,
                                          Int32 colCt,
                                          String[] colHddrs,
                                          Int32[] colSizes)
        {
            if (rptFileFmt == eRptFileFmt.html)
                tableHddrDataHtml ((HtmlTextWriter)writer, plain,
                                   colCt, colHddrs);
            else if (rptFileFmt == eRptFileFmt.xml)
                tableHddrDataXml ((XmlWriter)writer, plain, colCt, colHddrs);
            else
                tableHddrDataText ((StreamWriter)writer, colCt, colHddrs,
                                   colSizes);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a b l e H d d r D a t a H t m l                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Open report data table and write header row in html format.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void tableHddrDataHtml (HtmlTextWriter htmlWriter,
                                               Boolean plain,
                                               Int32 colCt,
                                               String[] colHddrs)
        {
            Int32 lastCol = colCt - 1;

            htmlWriter.WriteLine ("");
            htmlWriter.RenderBeginTag ("table");

            if (colCt > 0)
            {
                htmlWriter.RenderBeginTag ("tr");

                for (Int32 i = 0; i < colCt; i++)
                {
                    if (plain)
                        htmlWriter.AddAttribute ("class", "plain");

                    htmlWriter.RenderBeginTag ("th");


                    htmlWriter.WriteEncodedText (colHddrs[i].ToString ());
                    htmlWriter.RenderEndTag ();    // </td>

                    if (i != lastCol)
                        htmlWriter.WriteLine ("");
                }

                htmlWriter.RenderEndTag ();    // </tr>
                htmlWriter.WriteLine ("");
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a b l e H d d r D a t a T e x t                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Open report data table and write header row in text format.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void tableHddrDataText (StreamWriter txtWriter,
                                               Int32 colCt,
                                               String[] colHddrs,
                                               Int32[] colSizes)
        {
            if (colCt > 0)
            {
                Int32 lastCol = colCt - 1;

                String colSep = " ".PadRight (_lcSep, ' ');

                StringBuilder line = new StringBuilder ();

                txtWriter.WriteLine ("");

                for (Int32 i = 0; i < colCt; i++)
                {
                    line.Append (colHddrs[i].ToString ().PadRight (colSizes[i],
                                                                   ' '));

                    if (i != lastCol)
                        line.Append (colSep);
                }

                txtWriter.WriteLine (line);

                line.Clear ();

                for (Int32 i = 0; i < colCt; i++)
                {
                    line.Append ("-".PadRight (colSizes[i], '-'));

                    if (i != lastCol)
                        line.Append (colSep);
                }

                txtWriter.WriteLine (line);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a b l e H d d r D a t a X m l                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Open report data table and write header row in xml format.         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void tableHddrDataXml (XmlWriter xmlWriter,
                                              Boolean plain,
                                              Int32 colCt,
                                              String[] colHddrs)
        {
            xmlWriter.WriteStartElement ("tabledata");

            if (colCt > 0)
            {
                if (plain)
                {
                    xmlWriter.WriteStartElement ("hddr");
                    xmlWriter.WriteAttributeString ("hddrstyle", "plain");
                }
                else
                {
                    xmlWriter.WriteStartElement ("hddr");
                }

                for (Int32 i = 0; i < colCt; i++)
                {
                    xmlWriter.WriteStartElement ("col");
                    xmlWriter.WriteString (colHddrs[i]);
                    xmlWriter.WriteEndElement ();
                }

                xmlWriter.WriteEndElement ();
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a b l e H d d r P a i r                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Open report pair table.                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void tableHddrPair (Object writer,
                                          eRptFileFmt rptFileFmt)
        {
            if (rptFileFmt == eRptFileFmt.html)
                tableHddrPairHtml ((HtmlTextWriter)writer);
            else if (rptFileFmt == eRptFileFmt.xml)
                tableHddrPairXml ((XmlWriter)writer);
            else
                tableHddrPairText ((StreamWriter)writer);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a b l e H d d r P a i r H t m l                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Open report pair table and write header row in html format.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void tableHddrPairHtml (HtmlTextWriter htmlWriter)
        {
            htmlWriter.WriteLine ("");
            htmlWriter.RenderBeginTag ("table");
            htmlWriter.WriteLine ("");
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a b l e H d d r P a i r T e x t                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Open report data table and write header row in text format.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void tableHddrPairText (StreamWriter txtWriter)
        {
            txtWriter.WriteLine ("");
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a b l e H d d r P a i r X m l                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Open report pair table and write header row in xml format.         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void tableHddrPairXml (XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement ("tablepair");
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a b l e M u l t i R o w T e x t                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write report table row from data supplied as an array of string    //
        // arrays.                                                            //
        // Only required if output format is text?                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void tableMultiRowText (
            Object writer,
            eRptFileFmt rptFileFmt,
            Int32 colCt,
            String[][] arrData,
            Int32[] colSizes,
            Boolean blankBefore,
            Boolean blankAfter,
            Boolean blankAfterMultiRow)
        {
            if (rptFileFmt == eRptFileFmt.text)
            {
                tableMultiRowTextText (
                    (StreamWriter)writer, colCt, arrData, colSizes,
                    blankBefore, blankAfter, blankAfterMultiRow);

            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a b l e M u l t i R o w T e x t T e x t                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write report table row in text format.                             //
        // Data is supplied as an array of string arrays.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void tableMultiRowTextText (
            StreamWriter txtWriter,
            Int32 colCt,
            String[][] arrData,
            Int32[] colSizes,
            Boolean blankBefore,
            Boolean blankAfter,
            Boolean blankAfterMultiRow)
        {
            Int32 lastCol = colCt - 1;

            Int32 maxRows = 0;

            String space = " ";

            String colSep = " ".PadRight (_lcSep, ' ');

            StringBuilder line = new StringBuilder ();

            for (Int32 i = 0; i < colCt; i++)
            {
                Int32 x = arrData[i].Length;

                if (x > maxRows)
                    maxRows = x;
            }

            if (blankBefore)
            {
                txtWriter.WriteLine ("");
            }

            for (Int32 i = 0; i < maxRows; i++)
            {
                for (Int32 j = 0; j < colCt; j++)
                {
                    if (arrData[j].Length <= i)
                        line.Append (space.PadRight (colSizes[j], ' '));
                    else
                        line.Append ((arrData[j][i]).PadRight (colSizes[j], ' '));

                    if (j != lastCol)
                        line.Append (colSep);
                }

                txtWriter.WriteLine (line);
                line.Clear ();
            }

            if ((blankAfter) || ((blankAfterMultiRow) && (maxRows > 1)))
            {
                txtWriter.WriteLine ("");
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a b l e R o w D a t a                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write report table row from data supplied as a data row.           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void tableRowData (Object writer,
                                         eRptFileFmt rptFileFmt,
                                         eRptChkMarks rptChkMarks,
                                         Int32 colCt,
                                         String rowType,
                                         DataRow row,
                                         String[] colNames,
                                         Int32[] colSizes)
        {
            if (rptFileFmt == eRptFileFmt.html)
                tableRowDataHtml ((HtmlTextWriter)writer, rptChkMarks,
                                  colCt, rowType, row, colNames);
            else if (rptFileFmt == eRptFileFmt.xml)
                tableRowDataXml ((XmlWriter)writer, rptChkMarks,
                                 colCt, rowType, row, colNames);
            else
                tableRowDataText ((StreamWriter)writer, rptChkMarks,
                                  colCt, rowType, row, colNames, colSizes);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a b l e R o w D a t a H t m l                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write report table row in html format.                             //
        // Data is supplied as a data row.                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void tableRowDataHtml (HtmlTextWriter htmlWriter,
                                              eRptChkMarks rptChkMarks,
                                              Int32 colCt,
                                              String rowType,
                                              DataRow row,
                                              String[] colNames)
        {
            Int32 lastCol = colCt - 1;

            if (rowType != null)
                htmlWriter.AddAttribute ("class", rowType);

            htmlWriter.RenderBeginTag ("tr");

            for (Int32 i = 0; i < colCt; i++)
            {
                htmlWriter.RenderBeginTag ("td");

                if (row[colNames[i]] is Boolean)
                {
                    if ((Boolean)row[colNames[i]] == true)
                    {
                        if (rptChkMarks == eRptChkMarks.boxsym)
                            htmlWriter.Write (_chkMarkBoxSymTrue);
                        else if (rptChkMarks == eRptChkMarks.txtsym)
                            htmlWriter.Write (_chkMarkTxtSymTrue);
                        else
                            htmlWriter.Write (_chkMarkTextTrue);
                    }
                    else
                    {
                        if (rptChkMarks == eRptChkMarks.boxsym)
                            htmlWriter.Write (_chkMarkBoxSymFalse);
                        else if (rptChkMarks == eRptChkMarks.txtsym)
                            htmlWriter.Write (_chkMarkTxtSymFalse);
                        else
                            htmlWriter.Write (_chkMarkTextFalse);
                    }
                }
                else if ((i == 0) && (row[colNames[0]].ToString () == ""))
                {
                    htmlWriter.Write ("&nbsp;");
                }
                else
                {
                    htmlWriter.WriteEncodedText (row[colNames[i]].ToString ());
                }

                htmlWriter.RenderEndTag ();    // </td>

                if (i != lastCol)
                    htmlWriter.WriteLine ("");
            }

            htmlWriter.RenderEndTag ();    // </tr>
            htmlWriter.WriteLine ("");
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a b l e R o w D a t a T e x t                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write report table row in text format.                             //
        // Data is supplied as a data row.                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void tableRowDataText (StreamWriter txtWriter,
                                              eRptChkMarks rptChkMarks,
                                              Int32 colCt,
                                              String rowType,
                                              DataRow row,
                                              String[] colNames,
                                              Int32[] colSizes)
        {
            Int32 lastCol = colCt - 1;

            String colSep = " ".PadRight (_lcSep, ' ');

            StringBuilder line = new StringBuilder (); 

            for (Int32 i = 0; i < colCt; i++)
            {
                String itemData;

                if (row[colNames[i]] is Boolean)
                {
                    if ((Boolean)row[colNames[i]] == true)
                    {
                        if (rptChkMarks == eRptChkMarks.boxsym)
                            itemData = _chkMarkBoxSymTrue;
                        else if (rptChkMarks == eRptChkMarks.txtsym)
                            itemData = _chkMarkTxtSymTrue;
                        else
                            itemData = _chkMarkTextTrue;
                    }
                    else
                    {
                        if (rptChkMarks == eRptChkMarks.boxsym)
                            itemData = _chkMarkBoxSymFalse;
                        else if (rptChkMarks == eRptChkMarks.txtsym)
                            itemData = _chkMarkTxtSymFalse;
                        else
                            itemData = _chkMarkTextFalse;
                    }
                }
                else
                {
                    itemData = row[colNames[i]].ToString ();
                }

                line.Append (itemData.PadRight (colSizes[i], ' '));

                if (i != lastCol)
                    line.Append (colSep);
            }

            txtWriter.WriteLine (line);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a b l e R o w D a t a X m l                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write report table row in xml format.                              //
        // Data is supplied as a data row.                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void tableRowDataXml (XmlWriter xmlWriter,
                                              eRptChkMarks rptChkMarks,
                                             Int32 colCt,
                                             String rowType,
                                             DataRow row,
                                             String[] colNames)
        {
            xmlWriter.WriteStartElement ("item");

            if (rowType != null)
            {
                xmlWriter.WriteAttributeString ("rowType", rowType);
            }

            for (Int32 i = 0; i < colCt; i++)
            {
                xmlWriter.WriteStartElement (colNames[i].ToLower ());

                if (row[colNames[i]] is Boolean)
                {
                    if ((Boolean)row[colNames[i]] == true)
                    {
                        if (rptChkMarks == eRptChkMarks.boxsym)
                            xmlWriter.WriteString (_chkMarkBoxSymTrue);
                        else if (rptChkMarks == eRptChkMarks.txtsym)
                            xmlWriter.WriteString (_chkMarkTxtSymTrue);
                        else
                            xmlWriter.WriteString (_chkMarkTextTrue);
                    }
                    else
                    {
                        if (rptChkMarks == eRptChkMarks.boxsym)
                            xmlWriter.WriteString (_chkMarkBoxSymFalse);
                        else if (rptChkMarks == eRptChkMarks.txtsym)
                            xmlWriter.WriteString (_chkMarkTxtSymFalse);
                        else
                            xmlWriter.WriteString (_chkMarkTextFalse);
                    }
                }
                else
                {
                    if ((i == 0) && (row[colNames[0]].ToString () == ""))
                        xmlWriter.WriteCharEntity ((Char)0xa0);
                    else
                        xmlWriter.WriteString (row[colNames[i]].ToString ());
                }

                xmlWriter.WriteEndElement ();
            }

            xmlWriter.WriteEndElement ();   // </item>
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a b l e R o w P a i r                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write table row containing name and value components.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void tableRowPair (Object writer,
                                         eRptFileFmt rptFileFmt,
                                         String txtName,
                                         String txtVal,
                                         Int32 colSpanName,
                                         Int32 colSpanVal,
                                         Int32 sizeName,
                                         Int32 sizeVal,
                                         Boolean blankBefore,
                                         Boolean blankAfter,
                                         Boolean nameAsHddr)
        {
            if (rptFileFmt == eRptFileFmt.html)
                tableRowPairHtml ((HtmlTextWriter)writer,
                                  txtName, txtVal,
                                  colSpanName, colSpanVal,
                                  blankBefore, blankAfter, nameAsHddr);
            else if (rptFileFmt == eRptFileFmt.xml)
                tableRowPairXml ((XmlWriter)writer, txtName, txtVal,
                                 colSpanName, colSpanVal,
                                 blankBefore, blankAfter, nameAsHddr);
            else
                tableRowPairText ((StreamWriter)writer, txtName, txtVal,
                                  sizeName, sizeVal, blankBefore);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a b l e R o w P a i r H t m l                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write table row containing name and value components in html       //
        // format.                                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void tableRowPairHtml (HtmlTextWriter htmlWriter,
                                              String txtName,
                                              String txtVal,
                                              Int32 colSpanName,
                                              Int32 colSpanVal,
                                              Boolean blankBefore,
                                              Boolean blankAfter,
                                              Boolean nameAsHddr)
        {
            String padClass = "";

            htmlWriter.RenderBeginTag ("tr");

            //----------------------------------------------------------------//

            if ((blankBefore) && (blankAfter))
                padClass = "padAntePost";
            else if (blankBefore)
                padClass = "padAnte";
            else if (blankAfter)
                padClass = "padPost";

            //----------------------------------------------------------------//

            if (padClass != "")
                if (nameAsHddr)
                    htmlWriter.AddAttribute ("class",
                                             padClass + " " + "fmtAdorn");
                else
                    htmlWriter.AddAttribute ("class", padClass);
            else if (nameAsHddr)
                    htmlWriter.AddAttribute ("class", "fmtAdorn");

            if (colSpanName != -1)
                htmlWriter.AddAttribute ("colspan", colSpanName.ToString ());

            htmlWriter.RenderBeginTag ("td");

            htmlWriter.WriteEncodedText (txtName);
            htmlWriter.RenderEndTag ();    // </td>

            htmlWriter.WriteLine ("");

            //----------------------------------------------------------------//

            if (padClass != "")
                htmlWriter.AddAttribute ("class", padClass);

            if (colSpanName != -1)
                htmlWriter.AddAttribute ("colspan", colSpanVal.ToString ());

            htmlWriter.RenderBeginTag ("td");

            htmlWriter.WriteEncodedText (txtVal);
            htmlWriter.RenderEndTag ();    // </td>

            //----------------------------------------------------------------//

            htmlWriter.RenderEndTag ();    // </tr>

            htmlWriter.WriteLine ("");
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a b l e R o w P a i r T e x t                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write table row containing name and value components in text       //
        // format.                                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void tableRowPairText (StreamWriter txtWriter,
                                              String txtName,
                                              String txtVal,
                                              Int32 maxSizeName,
                                              Int32 maxSizeVal,
                                              Boolean blankBefore)
        {
            Int32 valPos,
                  valLen;

            Boolean firstLine = true;

            if (blankBefore)
                txtWriter.WriteLine ();

            valLen = txtVal.Length;
            valPos = 0;

            while (valPos + maxSizeVal < valLen)
            {
                String prefix;

                if (firstLine)
                    prefix = (txtName + ":").PadRight (maxSizeName, ' ');
                else
                    prefix = " ".PadRight (maxSizeName, ' ');

                txtWriter.WriteLine (
                        prefix +
                        txtVal.Substring (valPos, maxSizeVal));

                valPos = valPos + maxSizeVal;
                firstLine = false;
            }

            if (valPos <= valLen)
            {
                String prefix;

                if (firstLine)
                    prefix = (txtName + ":").PadRight (maxSizeName, ' ');
                else
                    prefix = " ".PadRight (maxSizeName, ' ');

                txtWriter.WriteLine (
                        prefix +
                        txtVal.Substring (valPos, valLen - valPos));
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a b l e R o w P a i r X m l                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write table row containing name and value components in xml        //
        // format.                                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void tableRowPairXml (XmlWriter xmlWriter,
                                             String txtName,
                                             String txtVal,
                                             Int32 colSpanName,
                                             Int32 colSpanVal,
                                             Boolean blankBefore,
                                             Boolean blankAfter,
                                             Boolean nameAsHddr)
        {
            String padClass = "";

            //----------------------------------------------------------------//

            if ((blankBefore) && (blankAfter))
                padClass = "padAntePost";
            else if (blankBefore)
                padClass = "padAnte";
            else if (blankAfter)
                padClass = "padPost";

            //----------------------------------------------------------------//

            xmlWriter.WriteStartElement ("item");

            if (padClass != "")
                xmlWriter.WriteAttributeString ("padType", padClass);

            xmlWriter.WriteStartElement ("name");
            if (colSpanName != -1)
                xmlWriter.WriteAttributeString ("colspan",
                                                colSpanName.ToString ());
            if (nameAsHddr)
                xmlWriter.WriteAttributeString ("txtfmt",
                                                "fmtAdorn");

            xmlWriter.WriteString (txtName);
            xmlWriter.WriteEndElement ();

            xmlWriter.WriteStartElement ("value");
            if (colSpanName != -1)
                xmlWriter.WriteAttributeString ("colspan",
                                                colSpanVal.ToString ());
            xmlWriter.WriteString (txtVal);
            xmlWriter.WriteEndElement ();

            xmlWriter.WriteEndElement ();   // </item>
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a b l e R o w T e x t                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write report table row from data supplied as a string array.       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void tableRowText (Object writer,
                                         eRptFileFmt rptFileFmt,
                                         Int32 colCt,
                                         String[] data,
                                         String[] colNames,
                                         Int32[] colSizes)
        {
            if (rptFileFmt == eRptFileFmt.html)
                tableRowTextHtml ((HtmlTextWriter)writer, colCt, data);
            else if (rptFileFmt == eRptFileFmt.xml)
                tableRowTextXml ((XmlWriter)writer, colCt,
                              data, colNames);
            else
                tableRowTextText ((StreamWriter)writer, colCt, data, colSizes);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a b l e R o w T e x t H t m l                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write report table row in html format.                             //
        // Data is supplied as a string array.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void tableRowTextHtml (HtmlTextWriter htmlWriter,
                                              Int32 colCt,
                                              String[] data)
        {
            Int32 lastCol = colCt - 1;

            htmlWriter.RenderBeginTag ("tr");

            for (Int32 i = 0; i < colCt; i++)
            {
                htmlWriter.RenderBeginTag ("td");
                if ((i == 0) && (data[0] == ""))
                    htmlWriter.Write ("&nbsp;");
                else
                    htmlWriter.WriteEncodedText (data[i]);
                htmlWriter.RenderEndTag ();    // </td>

                if (i != lastCol)
                    htmlWriter.WriteLine ("");
            }

            htmlWriter.RenderEndTag ();    // </tr>
            htmlWriter.WriteLine ("");
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a b l e R o w T e x t T e x t                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write report table row in text format.                             //
        // Data is supplied as a string array.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void tableRowTextText (StreamWriter txtWriter,
                                              Int32 colCt,
                                              String[] data,
                                              Int32[] colSizes)
        {
            Int32 lastCol = colCt - 1;

            String colSep = " ".PadRight (_lcSep, ' ');

            StringBuilder line = new StringBuilder ();

            for (Int32 i = 0; i < colCt; i++)
            {
                line.Append ((data[i]).PadRight (colSizes[i], ' '));

                if (i != lastCol)
                    line.Append (colSep);
            }

            txtWriter.WriteLine (line);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a b l e R o w T e x t X m l                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write report table row in xml format.                              //
        // Data is supplied as a string array.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void tableRowTextXml (XmlWriter xmlWriter,
                                             Int32 colCt,
                                             String[] data,
                                             String[] colNames)
        {
            xmlWriter.WriteStartElement ("item");

            for (Int32 i = 0; i < colCt; i++)
            {
                xmlWriter.WriteStartElement (colNames[i].ToLower ());
                if ((i == 0) && (data[0] == ""))
                    xmlWriter.WriteCharEntity ((Char)0xa0);
                else
                    xmlWriter.WriteString (data[i]);
                xmlWriter.WriteEndElement ();
            }

            xmlWriter.WriteEndElement ();   // </tableitem>
        }
    }
}