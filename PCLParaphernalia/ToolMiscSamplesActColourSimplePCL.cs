using System;
using System.IO;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides PCL support for the PCL Simple element of the
    /// Colours action of the MiscSamples tool.
    /// 
    /// © Chris Hutchinson 2014
    /// 
    /// </summary>

    static class ToolMiscSamplesActColourSimplePCL
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
        const Int16 _incInch     = (_unitsPerInch * 1);
        const Int16 _lineInc     = (_unitsPerInch * 5) / 6;
        const Int16 _colInc      = (_unitsPerInch * 3) / 2;

        const Int16 _posXDesc  = _pageOriginX;
        const Int16 _posXDesc1 = _posXDesc + ((_incInch * 15) / 4);
        const Int16 _posXDesc2 = _posXDesc + ((_incInch * 5) / 2);
        const Int16 _posXDesc3 = _posXDesc;
        const Int16 _posXDesc4 = _posXDesc;

        const Int16 _posYHddr  = _pageOriginY;
        const Int16 _posYDesc1 = _pageOriginY + (_incInch);
        const Int16 _posYDesc2 = _pageOriginY + ((_incInch * 5) / 4);
        const Int16 _posYDesc3 = _pageOriginY + ((_incInch * 7) / 4);
        const Int16 _posYDesc4 = _pageOriginY + (_incInch * 2);

        const Int16 _posXData  = _posXDesc + ((_incInch * 5) / 2);
        const Int16 _posYData  = _pageOriginY + ((_incInch * 7) / 4);

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
                  boxWidth;

            Int16 rectX,
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
                      "PCL simple colour mode:");

            //----------------------------------------------------------------//

            ptSize = 12;

            PCLWriter.font(prnWriter, true, "19U",
                           PCLFonts.getPCLFontSelect(_indxFontCourier,
                                                      PCLFonts.eVariant.Regular,
                                                      ptSize, 0));

            //----------------------------------------------------------------//

            posX = (Int16) (_posXDesc1 - logXOffset);
            posY = _posYDesc1;

            PCLWriter.text(prnWriter, posX, posY, 0,
                           "Palette");

            //----------------------------------------------------------------//

            posX = (Int16) (_posXDesc2 - logXOffset);
            posY = _posYDesc2;

            posX = (Int16) (_posXDesc2 - logXOffset);

            PCLWriter.text(prnWriter, posX, posY, 0,
                           "Mono");

            posX += _colInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                           "RGB");

            posX += _colInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                           "CMY");

            //----------------------------------------------------------------//

            posX = (Int16) (_posXDesc3 - logXOffset);
            posY = _posYDesc3;

            PCLWriter.text(prnWriter, posX, posY, 0,
                           "index");

            //----------------------------------------------------------------//

            posX = (Int16)(_posXDesc4 - logXOffset);
            posY = _posYDesc4;

            PCLWriter.text(prnWriter, posX, posY, 0, "0");

            posY += _lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0, "1");

            posY += _lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0, "2");

            posY += _lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0, "3");

            posY += _lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0, "4");

            posY += _lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0, "5");

            posY += _lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0, "6");

            posY += _lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0, "7");

            //----------------------------------------------------------------//
            //                                                                //
            // Background shade.                                              //
            //                                                                //
            //----------------------------------------------------------------//

            rectX = (Int16) (_posXDesc2 - (_incInch / 4) - logXOffset);
            rectY = _posYDesc2 + (_incInch / 4);
            rectWidth = (_incInch * 17) / 4;
            rectHeight = _incInch * 7;

            PCLWriter.rectangleShaded (prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, 5,
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
            Int16 posX,
                  posY,
                  rectX,
                  rectY,
                  rectHeight,
                  rectWidth;

            //----------------------------------------------------------------//

            if (formAsMacro)
                PCLWriter.macroControl(prnWriter, _macroId,
                                       PCLWriter.eMacroControl.Call);
            else
                generateOverlay(prnWriter, false, logXOffset,
                                indxPaperSize, indxOrientation);

            rectHeight = (Int16)(_lineInc / 2);
            rectWidth  = _lineInc;

            //----------------------------------------------------------------//
            //                                                                //
            // Set pattern transparency to Opaque so that white samples show  //
            // on the shaded background.                                      //
            //                                                                //
            //----------------------------------------------------------------//

            PCLWriter.patternTransparency (prnWriter,
                                           true);

            //----------------------------------------------------------------//
            //                                                                //
            // Monochrome palette.                                            //
            //                                                                //
            //----------------------------------------------------------------//

            PCLWriter.paletteSimple (prnWriter,
                                     PCLWriter.eSimplePalette.K);

            posX = (Int16)(_posXData - logXOffset);
            posY = _posYData;

            rectX = posX;
            rectY = posY;

            PCLWriter.setForegroundColour (prnWriter, 0);

            PCLWriter.rectangleSolid (prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, false,
                                      false, false);

            rectY += _lineInc;

            PCLWriter.setForegroundColour (prnWriter, 1);

            PCLWriter.rectangleSolid (prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, false,
                                      false, false);

            //----------------------------------------------------------------//
            //                                                                //
            // RGB palette.                                                   //
            //                                                                //
            //----------------------------------------------------------------//

            PCLWriter.paletteSimple (prnWriter,
                                     PCLWriter.eSimplePalette.RGB);

            posX += _colInc;

            rectX = posX;
            rectY = posY;

            PCLWriter.setForegroundColour (prnWriter, 0);

            PCLWriter.rectangleSolid (prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, false,
                                      false, false);

            rectY += _lineInc;

            PCLWriter.setForegroundColour (prnWriter, 1);

            PCLWriter.rectangleSolid (prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, false,
                                      false, false);

            rectY += _lineInc;

            PCLWriter.setForegroundColour (prnWriter, 2);

            PCLWriter.rectangleSolid (prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, false,
                                      false, false);

            rectY += _lineInc;

            PCLWriter.setForegroundColour (prnWriter, 3);

            PCLWriter.rectangleSolid (prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, false,
                                      false, false);

            rectY += _lineInc;

            PCLWriter.setForegroundColour (prnWriter, 4);

            PCLWriter.rectangleSolid (prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, false,
                                      false, false);

            rectY += _lineInc;

            PCLWriter.setForegroundColour (prnWriter, 5);

            PCLWriter.rectangleSolid (prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, false,
                                      false, false);

            rectY += _lineInc;

            PCLWriter.setForegroundColour (prnWriter, 6);

            PCLWriter.rectangleSolid (prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, false,
                                      false, false);

            rectY += _lineInc;

            PCLWriter.setForegroundColour (prnWriter, 7);

            PCLWriter.rectangleSolid (prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, false,
                                      false, false);

            //----------------------------------------------------------------//
            //                                                                //
            // CMY palette.                                                   //
            //                                                                //
            //----------------------------------------------------------------//

            PCLWriter.paletteSimple (prnWriter,
                                     PCLWriter.eSimplePalette.CMY);

            posX += _colInc;
            
            rectX = posX;
            rectY = posY;

            PCLWriter.setForegroundColour (prnWriter, 0);

            PCLWriter.rectangleSolid (prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, true,
                                      false, false);

            rectY += _lineInc;

            PCLWriter.setForegroundColour (prnWriter, 1);

            PCLWriter.rectangleSolid (prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, false,
                                      false, false);

            rectY += _lineInc;

            PCLWriter.setForegroundColour (prnWriter, 2);

            PCLWriter.rectangleSolid (prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, false,
                                      false, false);

            rectY += _lineInc;

            PCLWriter.setForegroundColour (prnWriter, 3);

            PCLWriter.rectangleSolid (prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, false,
                                      false, false);

            rectY += _lineInc;

            PCLWriter.setForegroundColour (prnWriter, 4);

            PCLWriter.rectangleSolid (prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, false,
                                      false, false);

            rectY += _lineInc;

            PCLWriter.setForegroundColour (prnWriter, 5);

            PCLWriter.rectangleSolid (prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, false,
                                      false, false);

            rectY += _lineInc;

            PCLWriter.setForegroundColour (prnWriter, 6);

            PCLWriter.rectangleSolid (prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, false,
                                      false, false);

            rectY += _lineInc;

            PCLWriter.setForegroundColour (prnWriter, 7);

            PCLWriter.rectangleSolid (prnWriter, rectX, rectY,
                                      rectHeight, rectWidth, false,
                                      false, false);

            //----------------------------------------------------------------//

            PCLWriter.formFeed(prnWriter);
        }
    }
}
