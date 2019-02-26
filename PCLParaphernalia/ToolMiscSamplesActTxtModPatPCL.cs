using System;
using System.IO;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides PCL support for the Text and Background element
    /// of the Text Modification action of the MiscSamples tool.
    /// 
    /// © Chris Hutchinson 2014
    /// 
    /// </summary>

    static class ToolMiscSamplesActTxtModPatPCL
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const Int32 _macroId           = 1;
        const UInt16 _unitsPerInch     = PCLWriter.sessionUPI;

        const Int16 _pageOriginX       = (_unitsPerInch * 1);
        const Int16 _pageOriginY       = (_unitsPerInch * 1);
        const Int16 _incInch           = (_unitsPerInch * 1);
        const Int16 _lineInc           = (_unitsPerInch * 5) / 6;

        const Int16 _posXDesc  = _pageOriginX;
        const Int16 _posXData  = _pageOriginX + (2 * _incInch);

        const Int16 _posYHddr  = _pageOriginY;
        const Int16 _posYDesc  = _pageOriginY + (2 * _incInch);
        const Int16 _posYData  = _pageOriginY + (2 * _incInch);

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

        public static void generateJob(BinaryWriter prnWriter,
                                       Int32 indxPaperSize,
                                       Int32 indxPaperType,
                                       Int32 indxOrientation,
                                       Boolean formAsMacro)
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
                         logXOffset);

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
                  boxWidth,
                  rectX,
                  rectY,
                  rectHeight,
                  rectWidth;

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
                      "PCL text & background:");

            ptSize = 12;

            PCLWriter.font(prnWriter, true, "19U",
                           PCLFonts.getPCLFontSelect(_indxFontCourier,
                                                      PCLFonts.eVariant.Regular,
                                                      ptSize, 0));

            posY = _posYDesc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      "Black:");

            posY += _lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      "Shade " + _shade_1 + "%:");

            posY += _lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      "Shade " + _shade_2 + "%:");

            posY += _lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      "White:");

            //----------------------------------------------------------------//
            //                                                                //
            // Background shading.                                            //
            //                                                                //
            //----------------------------------------------------------------//

            posX = (Int16)(_posXData - logXOffset);
            posY = _posYData - (_lineInc / 2);

            rectX = posX;
            rectY = posY;

            rectHeight = (Int16)((_lineInc * 3) / 5);
            rectWidth  = (Int16)((_unitsPerInch * 9) / 10);

            PCLWriter.patternTransparency(prnWriter, false);

            PCLWriter.patternSet(prnWriter,
                                  PCLWriter.ePatternType.SolidBlack,
                                  0);

            PCLWriter.rectangleSolid(prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, false,
                                      false, false);

            rectX += rectWidth;

            PCLWriter.rectangleShaded(prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, _shade_1,
                                      false, false);

            rectX += rectWidth;

            PCLWriter.rectangleShaded(prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, _shade_2,
                                      false, false);

            rectX = posX;
            rectY += _lineInc;

            PCLWriter.rectangleSolid(prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, false,
                                      false, false);

            rectX += rectWidth;

            PCLWriter.rectangleShaded(prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, _shade_1,
                                      false, false);

            rectX += rectWidth;

            PCLWriter.rectangleShaded(prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, _shade_2,
                                      false, false);

            rectX = posX;
            rectY += _lineInc;

            PCLWriter.rectangleSolid(prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, false,
                                      false, false);

            rectX += rectWidth;

            PCLWriter.rectangleShaded(prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, _shade_1,
                                      false, false);

            rectX += rectWidth;

            PCLWriter.rectangleShaded(prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, _shade_2,
                                      false, false);

            rectX = posX;
            rectY += _lineInc;

            PCLWriter.rectangleSolid(prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, false,
                                      false, false);

            rectX += rectWidth;

            PCLWriter.rectangleShaded(prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, _shade_1,
                                      false, false);

            rectX += rectWidth;

            PCLWriter.rectangleShaded(prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, _shade_2,
                                      false, false);

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

        private static void generatePage(BinaryWriter prnWriter,
                                         Int32 indxPaperSize,
                                         Int32 indxPaperType,
                                         Int32 indxOrientation,
                                         Boolean formAsMacro,
                                         UInt16 logXOffset)
        {
            String sampleText  = "000000000000000";

            Int16 posX,
                  posY;

            Int16 ptSize;

            //----------------------------------------------------------------//

            if (formAsMacro)
                PCLWriter.macroControl(prnWriter, _macroId,
                                       PCLWriter.eMacroControl.Call);
            else
                generateOverlay(prnWriter, false, logXOffset,
                                indxPaperSize, indxOrientation);

            //----------------------------------------------------------------//
            //                                                                //
            // Text.                                                          //
            //                                                                //
            //----------------------------------------------------------------//

            ptSize = 36;

            PCLWriter.font(prnWriter, true, "19U",
                           PCLFonts.getPCLFontSelect(_indxFontArial,
                                                      PCLFonts.eVariant.Regular,
                                                      ptSize, 0));

            //----------------------------------------------------------------//
            // Black                                                          //
            //----------------------------------------------------------------//

            posX = (Int16)(_posXData - logXOffset);
            posY = _posYData;

            PCLWriter.patternTransparency(prnWriter, false);

            PCLWriter.patternSet(prnWriter,
                                 PCLWriter.ePatternType.SolidBlack,
                                 0);

            PCLWriter.text(prnWriter, posX, posY, 0, sampleText);

            //----------------------------------------------------------------//
            // Shade 1                                                        //
            //----------------------------------------------------------------//

            posY += _lineInc;

            PCLWriter.patternTransparency(prnWriter, true);

            PCLWriter.patternSet(prnWriter,
                                 PCLWriter.ePatternType.Shading,
                                 _shade_1);

            PCLWriter.text(prnWriter, posX, posY, 0, sampleText);

            //----------------------------------------------------------------//
            // Shade 2                                                        //
            //----------------------------------------------------------------//

            posY += _lineInc;

            PCLWriter.patternTransparency(prnWriter, true);

            PCLWriter.patternSet(prnWriter,
                                 PCLWriter.ePatternType.Shading,
                                 _shade_2);

            PCLWriter.text(prnWriter, posX, posY, 0, sampleText);

            //----------------------------------------------------------------//
            // White                                                          //
            //----------------------------------------------------------------//

            posY += _lineInc;

            PCLWriter.patternTransparency(prnWriter, true);

            PCLWriter.patternSet(prnWriter,
                                 PCLWriter.ePatternType.SolidWhite,
                                 0);

            PCLWriter.text(prnWriter, posX, posY, 0, sampleText);

            //----------------------------------------------------------------//

            PCLWriter.formFeed(prnWriter);
        }
    }
}
