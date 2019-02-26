using System;
using System.IO;
using System.Text;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides PCL support for the Unicode Characters action
    /// of the MiscSamples tool.
    /// 
    /// © Chris Hutchinson 2014
    /// 
    /// </summary>

    static class ToolMiscSamplesActUnicodePCL
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const Int32 _macroId           = 1;
        const UInt16 _unitsPerInch     = PCLWriter.sessionUPI;

        const Int16 _pageOriginX = (_unitsPerInch * 1);
        const Int16 _pageOriginY = (_unitsPerInch * 1);
        const Int16 _incInch = (_unitsPerInch * 1);
        const Int16 _lineInc = (_unitsPerInch * 5) / 6;

        const Int16 _posXDesc = _pageOriginX;
        const Int16 _posXData = _pageOriginX + (2 * _incInch);

        const Int16 _posYHddr = _pageOriginY;
        const Int16 _posYDesc = _pageOriginY + (2 * _incInch);
        const Int16 _posYData = _pageOriginY + (2 * _incInch);

        const Int16 _shade_1 = 40;
        const Int16 _shade_2 = 20;

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Static variables.                                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        static Int32 _indxFontArial = PCLFonts.getIndexForName("Arial");
        static Int32 _indxFontCourier = PCLFonts.getIndexForName("Courier");

        static Int32 _logPageWidth;
        static Int32 _logPageHeight;
        static Int32 _paperWidth;
        static Int32 _paperHeight;

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e J o b                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate test data.                                                //
        //                                                                    //
        // Some sequences are built up as (Unicode) strings, then converted   //
        // to byte arrays before writing out - this works OK because all the  //
        // characters we're using are within the ASCII range (0x00-0x7f) and  //
        // are hence represented using a single byte in the UTF-8 encoding.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void generateJob(BinaryWriter      prnWriter,
                                       Int32             indxPaperSize,
                                       Int32             indxPaperType,
                                       Int32             indxOrientation,
                                       Boolean           formAsMacro,
                                       UInt32            codePoint,
                                       Int32             indxFont,
                                       PCLFonts.eVariant fontVar)
        {
            PCLOrientations.eAspect aspect;

            UInt16 logXOffset;

            //----------------------------------------------------------------//

            aspect = PCLOrientations.getAspect(indxOrientation);

            logXOffset = PCLPaperSizes.getLogicalOffset(indxPaperSize,
                                                        _unitsPerInch, aspect);

            _logPageWidth = PCLPaperSizes.getLogPageWidth(indxPaperSize,
                                                           _unitsPerInch,
                                                           aspect);

            _logPageHeight = PCLPaperSizes.getLogPageLength(indxPaperSize,
                                                          _unitsPerInch,
                                                          aspect);

            _paperWidth = PCLPaperSizes.getPaperWidth(indxPaperSize,
                                                       _unitsPerInch,
                                                       aspect);

            _paperHeight = PCLPaperSizes.getPaperLength(indxPaperSize,
                                                         _unitsPerInch,
                                                         aspect);

            //----------------------------------------------------------------//

            generateJobHeader(prnWriter,
                              indxPaperSize,
                              indxPaperType,
                              indxOrientation,
                              formAsMacro,
                              logXOffset);

            generatePage(prnWriter,
                         indxPaperSize,
                         indxPaperType,
                         indxOrientation,
                         formAsMacro,
                         logXOffset,
                         codePoint,
                         indxFont,
                         fontVar);

            generateJobTrailer(prnWriter, formAsMacro);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e J o b H e a d e r                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write stream initialisation sequences to output file.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void generateJobHeader(BinaryWriter prnWriter,
                                              Int32 indxPaperSize,
                                              Int32 indxPaperType,
                                              Int32 indxOrientation,
                                              Boolean formAsMacro,
                                              UInt16 logXOffset)
        {
            PCLWriter.stdJobHeader(prnWriter, "");

            if (formAsMacro)
                generateOverlay(prnWriter, true, logXOffset,
                                indxPaperSize, indxOrientation);

            PCLWriter.pageHeader(prnWriter,
                                 indxPaperSize,
                                 indxPaperType,
                                 indxOrientation,
                                 PCLPlexModes.eSimplex);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e J o b T r a i l e r                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write tray map termination sequences to output file.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void generateJobTrailer(BinaryWriter prnWriter,
                                               Boolean formAsMacro)
        {
            PCLWriter.stdJobTrailer(prnWriter, formAsMacro, _macroId);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e O v e r l a y                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write background data sequences to output file.                    //
        // Optionally top and tail these with macro (user-defined stream)     //
        // definition sequences.                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void generateOverlay(BinaryWriter prnWriter,
                                            Boolean formAsMacro,
                                            UInt16 logXOffset,
                                            Int32 indxPaperSize,
                                            Int32 indxOrientation)
        {
            Int16 posX,
                  posY;

            Int16 ptSize;

            Int16 boxX,
                  boxY,
                  boxHeight,
                  boxWidth;

            Byte stroke = 1;

            //----------------------------------------------------------------//
            //                                                                //
            // Header                                                         //
            //                                                                //
            //----------------------------------------------------------------//

            if (formAsMacro)
                PCLWriter.macroControl(prnWriter, _macroId,
                                  PCLWriter.eMacroControl.StartDef);

            //----------------------------------------------------------------//
            //                                                                //
            // Box.                                                           //
            //                                                                //
            //----------------------------------------------------------------//

            PCLWriter.patternSet(prnWriter,
                                  PCLWriter.ePatternType.Shading,
                                  60);

            boxX = (Int16)((_unitsPerInch / 2) - logXOffset);
            boxY = (Int16)(_unitsPerInch / 2);

            boxWidth = (Int16)(_paperWidth - _unitsPerInch);
            boxHeight = (Int16)(_paperHeight - _unitsPerInch);

            PCLWriter.rectangleOutline(prnWriter, boxX, boxY,
                                        boxHeight, boxWidth, stroke,
                                        false, false);

            //----------------------------------------------------------------//
            //                                                                //
            // Text.                                                          //
            //                                                                //
            //----------------------------------------------------------------//

            PCLWriter.patternSet(prnWriter,
                                  PCLWriter.ePatternType.SolidBlack,
                                  0);

            ptSize = 15;

            PCLWriter.font(prnWriter, true, "19U",
                           PCLFonts.getPCLFontSelect(_indxFontCourier,
                                                      PCLFonts.eVariant.Bold,
                                                      ptSize, 0));

            posX = (Int16)(_posXDesc - logXOffset);
            posY = _posYHddr;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      "PCL using Unicode characters:");

            ptSize = 12;

            PCLWriter.font(prnWriter, true, "19U",
                           PCLFonts.getPCLFontSelect(_indxFontCourier,
                                                      PCLFonts.eVariant.Regular,
                                                      ptSize, 0));

            posY = _posYDesc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      "Unicode code-point:");

            posY += _lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      "UTF-8 encoding:");

            posY += _lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      "Font:");

            posY += _lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      "Font glyph:");

            //----------------------------------------------------------------//
            //                                                                //
            // Overlay end.                                                   //
            //                                                                //
            //----------------------------------------------------------------//

            PCLWriter.patternSet(prnWriter,
                                  PCLWriter.ePatternType.SolidBlack,
                                  0);

            if (formAsMacro)
                PCLWriter.macroControl(prnWriter, 0,
                                       PCLWriter.eMacroControl.StopDef);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e P a g e                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write individual test data page sequences to output file.          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void generatePage(BinaryWriter      prnWriter,
                                         Int32             indxPaperSize,
                                         Int32             indxPaperType,
                                         Int32             indxOrientation,
                                         Boolean           formAsMacro,
                                         UInt16            logXOffset,
                                         UInt32            codePoint,
                                         Int32             indxFont,
                                         PCLFonts.eVariant fontVar)
        {
            Int16 posX,
                  posY;

            Int16 ptSize;

            Byte[] utf8Seq = new Byte[4];
            Int32 utf8Len = 0;

            String utf8HexVal = "";

            //----------------------------------------------------------------//

            if (formAsMacro)
                PCLWriter.macroControl(prnWriter, _macroId,
                                       PCLWriter.eMacroControl.Call);
            else
                generateOverlay(prnWriter, false, logXOffset,
                                indxPaperSize, indxOrientation);

            //----------------------------------------------------------------//
            //                                                                //
            // Code-point data.                                               //
            //                                                                //
            //----------------------------------------------------------------//

            ptSize = 18;

            PCLWriter.font(prnWriter, true, "19U",
                           PCLFonts.getPCLFontSelect(_indxFontArial,
                                                      PCLFonts.eVariant.Regular,
                                                      ptSize, 0));

            posX = (Int16)(_posXData - logXOffset);
            posY = _posYData;

            if (codePoint < 0x010000)
                PCLWriter.text(prnWriter, posX, posY, 0, "U+" +
                               codePoint.ToString("x4"));
            else
                PCLWriter.text(prnWriter, posX, posY, 0, "U+" +
                               codePoint.ToString("x6"));

            PrnParseDataUTF8.convertUTF32ToUTF8Bytes (codePoint,
                                                      ref utf8Len,
                                                      ref utf8Seq);

            PrnParseDataUTF8.convertUTF32ToUTF8HexString (codePoint,
                                                          true,
                                                          ref utf8HexVal);

            posY += _lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0, utf8HexVal);

            //----------------------------------------------------------------//
            //                                                                //
            // Font data.                                                     //
            //                                                                //
            //----------------------------------------------------------------//

            posY += _lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                           PCLFonts.getName (indxFont) +
                           " " +
                           Enum.GetName(typeof(PCLFonts.eVariant), fontVar));

            posY += _lineInc;

            ptSize = 36;

            PCLWriter.font(prnWriter, true, "18N",
                           PCLFonts.getPCLFontSelect(indxFont,
                                                      fontVar,
                                                      ptSize, 0));

            PCLWriter.textParsingMethod (
                prnWriter,
                PCLTextParsingMethods.eIndex.m83_UTF8);

            PCLWriter.cursorPosition(prnWriter, posX, posY);

            prnWriter.Write(utf8Seq, 0, utf8Len);

            PCLWriter.formFeed(prnWriter);
        }
    }
}
