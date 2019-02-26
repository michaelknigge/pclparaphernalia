using System;
using System.IO;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides PCL support for the Cross-hatch element of the
    /// Patterns action of the MiscSamples tool.
    /// 
    /// © Chris Hutchinson 2014
    /// 
    /// </summary>

    static class ToolMiscSamplesActPatternXHatchPCL
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const Int32 _macroId           = 1;
        const UInt16 _unitsPerInch     = PCLWriter.sessionUPI;
        const UInt16 _plotUnitsPerInch = PCLWriter.plotterUnitsPerInchHPGL2;

        const Int16 _pageOriginX = (_unitsPerInch * 1);
        const Int16 _pageOriginY = (_unitsPerInch * 1);
        const Int16 _incInch     = (_unitsPerInch * 1);
        const Int16 _lineInc     = (_unitsPerInch * 5) / 6;

        const Int16 _posXDesc  = _pageOriginX;
        const Int16 _posXData1 = _pageOriginX + ((7 * _incInch) / 3);
        const Int16 _posXData2 = _posXData1 + ((3 * _incInch / 2));
        const Int16 _posXData3 = _posXData2 + ((3 * _incInch / 2));

        const Int16 _posYHddr  = _pageOriginY;
        const Int16 _posYDesc1 = _pageOriginY + (2 * _incInch);
        const Int16 _posYDesc2 = _pageOriginY + ((3 * _incInch / 2));
        const Int16 _posYData  = _pageOriginY + (2 * _incInch);

        const Int16 _patternBase_300 = 300;
        const Int16 _patternBase_600 = 600;

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

        static Int32 _patternsCt = 0;
        static UInt16[] _patternIds;
        static UInt16[] _patternHeights;
        static UInt16[] _patternWidths;

        static String [] _patternDescs;

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

            getPatternData ();

            generateJobHeader(prnWriter,
                              indxPaperSize,
                              indxPaperType,
                              indxOrientation,
                              formAsMacro,
                              logXOffset);

            patternDefineDpi300(prnWriter, _patternBase_300);

            patternDefineDpi600(prnWriter, _patternBase_600);

            generatePage(prnWriter,
                         indxPaperSize,
                         indxPaperType,
                         indxOrientation,
                         formAsMacro,
                         logXOffset);

            patternDeleteSet(prnWriter, _patternBase_300);

            patternDeleteSet(prnWriter, _patternBase_600);

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
                      "PCL cross-hatch patterns:");

            //----------------------------------------------------------------//

            ptSize = 12;

            PCLWriter.font(prnWriter, true, "19U",
                           PCLFonts.getPCLFontSelect(_indxFontCourier,
                                                      PCLFonts.eVariant.Regular,
                                                      ptSize, 0));

            //----------------------------------------------------------------//

            posY = _posYDesc1;

            for (Int32 i = 0; i < _patternsCt; i++)
            {
                PCLWriter.text(prnWriter, posX, posY, 0,
                               "#" + _patternIds[i].ToString() + ": ");

                posY += _lineInc;
            }

            //----------------------------------------------------------------//

            ptSize = 10;

            PCLWriter.font(prnWriter, true, "19U",
                           PCLFonts.getPCLFontSelect(_indxFontCourier,
                                                      PCLFonts.eVariant.Regular,
                                                      ptSize, 0));

            //----------------------------------------------------------------//

            posY = _posYDesc1 + (_lineInc / 4);

            for (Int32 i = 0; i < _patternsCt; i++)
            {
                PCLWriter.text(prnWriter, posX, posY, 0,
                               _patternDescs[i] + ":");

                posY += _lineInc;
            }

            //----------------------------------------------------------------//

            ptSize = 8;

            PCLWriter.font(prnWriter, true, "19U",
                           PCLFonts.getPCLFontSelect(_indxFontCourier,
                                                      PCLFonts.eVariant.Regular,
                                                      ptSize, 0));

            //----------------------------------------------------------------//

            posY = _posYDesc2;
            posX = (Int16)(_posXData1 - logXOffset);

            PCLWriter.text(prnWriter, posX, posY, 0,
                      "Predefined");

            posX = (Int16)(_posXData2 - logXOffset);

            PCLWriter.text(prnWriter, posX, posY, 0,
                      "User-defined 300 dpi");

            posX = (Int16)(_posXData3 - logXOffset);

            PCLWriter.text(prnWriter, posX, posY, 0,
                      "User-defined 600 dpi");

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
            rectWidth = _lineInc;

            //----------------------------------------------------------------//
            //                                                                //
            // Pre-defined cross-hatch pattenrs.                              //
            //                                                                //
            //----------------------------------------------------------------//

            posX = (Int16)(_posXData1 - logXOffset);
            posY = _posYData;

            rectX = posX;
            rectY = posY;

            for (Int32 i = 0; i < _patternsCt; i++)
            {
                PCLWriter.rectangleXHatch(prnWriter, rectX, rectY,
                                          rectHeight, rectWidth,
                                          (Int16)_patternIds[i]);

                rectY += _lineInc;
            }

            //----------------------------------------------------------------//
            //                                                                //
            // User-defined 300 dpi cross-hatch patterns.                     //
            //                                                                //
            //----------------------------------------------------------------//

            posX = (Int16)(_posXData2 - logXOffset);
            posY = _posYData;

            rectX = posX;
            rectY = posY;

            for (Int32 i = 0; i < _patternsCt; i++)
            {
                PCLWriter.rectangleUserFill (
                    prnWriter, rectX, rectY,
                    rectHeight, rectWidth,
                    (Int16) (_patternBase_300 + _patternIds[i]),
                    false, false);

                rectY += _lineInc;
            }

            //----------------------------------------------------------------//
            //                                                                //
            // User-defined 600 dpi cross-hatch patterns.                     //
            //                                                                //
            //----------------------------------------------------------------//

            posX = (Int16)(_posXData3 - logXOffset);
            posY = _posYData;

            rectX = posX;
            rectY = posY;

            for (Int32 i = 0; i < _patternsCt; i++)
            {
                PCLWriter.rectangleUserFill(
                    prnWriter, rectX, rectY,
                    rectHeight, rectWidth,
                    (Int16)(_patternBase_600 + _patternIds[i]),
                    false, false);

                rectY += _lineInc;
            }

            //----------------------------------------------------------------//

            PCLWriter.formFeed(prnWriter);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t P a t t e r n D a t a                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve information about the available cross-hatch patterns.     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void getPatternData ()
        {
            _patternsCt = PCLPatternDefs.getCount(
                PCLPatternDefs.eType.CrossHatch);

            _patternIds     = new UInt16[_patternsCt]; 
            _patternHeights = new UInt16[_patternsCt]; 
            _patternWidths  = new UInt16[_patternsCt]; 
            _patternDescs   = new String[_patternsCt]; 

            for (Int32 i= 0; i < _patternsCt; i++)
            {
                _patternIds[i] = PCLPatternDefs.getId(
                    PCLPatternDefs.eType.CrossHatch, i);
                _patternHeights[i] = PCLPatternDefs.getHeight(
                    PCLPatternDefs.eType.CrossHatch, i);
                _patternWidths[i] = PCLPatternDefs.getWidth(
                    PCLPatternDefs.eType.CrossHatch, i);
                _patternDescs[i] = PCLPatternDefs.getDesc(
                    PCLPatternDefs.eType.CrossHatch, i);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a t t e r n D e f i n e D p i 3 0 0                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Define default user-defined patterns to match the pre-defined      //
        // patterns.                                                          //
        // The format 0 pattern header does not define a resolution, so (we   //
        // assume) that the pattern will use the default 300 dots-per-inch.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void patternDefineDpi300(BinaryWriter prnWriter,
                                                 Int32 baseID)
        {
            Byte[] hddrFmt_0 = { 0x00, 0x00, 0x01, 0x00,
                                 0x00, 0x10, 0x00, 0x10 };

            for (Int32 i = 0; i < _patternsCt; i++)
            {
                hddrFmt_0[4] = (Byte)((_patternHeights[i] & 0xff00) >> 8);
                hddrFmt_0[5] = (Byte)(_patternHeights[i] & 0x00ff);

                hddrFmt_0[6] = (Byte)((_patternWidths[i] & 0xff00) >> 8);
                hddrFmt_0[7] = (Byte)(_patternWidths[i] & 0x00ff);

                PCLWriter.patternDefine(
                    prnWriter, (Int16)(baseID + _patternIds[i]),
                    hddrFmt_0,
                    PCLPatternDefs.getBytes(
                        PCLPatternDefs.eType.CrossHatch, i));
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a t t e r n D e f i n e D p i 6 0 0                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Define 600 dots-per-inch user-defined patterns to match the        //
        // pre-defined patterns.                                              //
        // The format 20 pattern header defines X & Y resolutions, which we   //
        // are setting to 600 dots-per-inch.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void patternDefineDpi600(BinaryWriter prnWriter,
                                                Int32 baseID)
        {
            const UInt16 dpi = 600;

            Byte[] hddrFmt_20 = { 0x14, 0x00, 0x01, 0x00,
                                  0x00, 0x10, 0x00, 0x10,
                                  0x02, 0x58, 0x02, 0x58 };

            for (Int32 i = 0; i < _patternsCt; i++)
            {
                hddrFmt_20[4] = (Byte)((_patternHeights[i] & 0xff00) >> 8);
                hddrFmt_20[5] = (Byte)(_patternHeights[i] & 0x00ff);

                hddrFmt_20[6] = (Byte)((_patternWidths[i] & 0xff00) >> 8);
                hddrFmt_20[7] = (Byte)(_patternWidths[i] & 0x00ff);

                hddrFmt_20[8] = (Byte)((dpi & 0xff00) >> 8);
                hddrFmt_20[9] = (Byte)(dpi & 0x00ff);

                hddrFmt_20[10] = (Byte)((dpi & 0xff00) >> 8);
                hddrFmt_20[11] = (Byte)(dpi & 0x00ff);

                PCLWriter.patternDefine(
                    prnWriter, (Int16)(baseID + _patternIds[i]),
                    hddrFmt_20,
                    PCLPatternDefs.getBytes(
                        PCLPatternDefs.eType.CrossHatch, i));
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a t t e r n D e l e t e S e t                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Delete user-defined patterns.                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void patternDeleteSet(BinaryWriter prnWriter,
                                             Int32        baseID)
        {
            for (Int32 i = 0; i < _patternsCt; i++)
            {
                PCLWriter.patternDelete (
                    prnWriter, (Int16)(baseID + _patternIds[i]));
            }
        }
    }
}
