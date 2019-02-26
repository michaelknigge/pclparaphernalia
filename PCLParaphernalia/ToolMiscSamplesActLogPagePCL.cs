using System;
using System.IO;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides PCL support for the Define Logical Page action
    /// of the MiscSamples tool.
    /// 
    /// © Chris Hutchinson 2014
    /// 
    /// </summary>

    static class ToolMiscSamplesActLogPagePCL
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const String _hexChars          = "0123456789ABCDEF";

        const Int32 _macroId            = 1;
        const UInt16 _unitsPerInch      = PCLWriter.sessionUPI;

        const Int16 _rulerDivPerCell = 10;
        const Int16 _rulerVOriginX = (_unitsPerInch * 6);
        const Int16 _rulerHOriginY = (_unitsPerInch * 5);
        const Int16 _rulerCell     = (_unitsPerInch * 1);
        const Int16 _rulerDiv      = (_rulerCell / _rulerDivPerCell);

        const Int16 _posOrigin = _rulerCell;
        const Int16 _posXDesc  = _posOrigin + (4 * _rulerDiv);
        const Int16 _posYHddr  = _posOrigin - (4 * _rulerDiv);
        const Int16 _posYDesc  = _posOrigin + (4 * _rulerDiv);

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
                                       Int32        indxPaperSize,
                                       Int32        indxPaperType,
                                       Int32        indxOrientation,
                                       Int16        logLeftOffset,
                                       Int16        logTopOffset,
                                       UInt16       logPageWidth,
                                       UInt16       logPageHeight,
                                       Boolean      formAsMacro,
                                       Boolean      incStdPage)
        {
            const PCLOrientations.eAspect aspectPort
                    = PCLOrientations.eAspect.Portrait;

            PCLOrientations.eAspect aspect;

            UInt16 paperWidth,
                   paperLength,
                   paperLengthPort,
                   logXOffset;

            //----------------------------------------------------------------//

            aspect = PCLOrientations.getAspect(indxOrientation);

            paperLength = PCLPaperSizes.getPaperLength(indxPaperSize,
                                                       _unitsPerInch, aspect);

            paperWidth = PCLPaperSizes.getPaperWidth(indxPaperSize,
                                                     _unitsPerInch, aspect);

            logXOffset = PCLPaperSizes.getLogicalOffset(indxPaperSize,
                                                        _unitsPerInch, aspect);

            paperLengthPort = PCLPaperSizes.getPaperLength(indxPaperSize,
                                                           _unitsPerInch,
                                                           aspectPort);

            //----------------------------------------------------------------//

            aspect = PCLOrientations.getAspect(indxOrientation);

            logXOffset = PCLPaperSizes.getLogicalOffset(indxPaperSize,
                                                        _unitsPerInch, aspect);

            //----------------------------------------------------------------//

            generateJobHeader(prnWriter,
                              indxPaperSize,
                              indxPaperType,
                              indxOrientation,
                              formAsMacro,
                              paperWidth,
                              paperLength,
                              logXOffset);

            generatePageSet (prnWriter,
                             indxPaperSize,
                             indxPaperType,
                             indxOrientation,
                             formAsMacro,
                             incStdPage,
                             paperWidth,
                             paperLength,
                             logXOffset,
                             logLeftOffset,
                             logTopOffset,
                             logPageWidth,
                             logPageHeight);

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
                                              Int32        indxPaperSize,
                                              Int32        indxPaperType,
                                              Int32        indxOrientation,
                                              Boolean      formAsMacro,
                                              UInt16       paperWidth,
                                              UInt16       paperLength,
                                              UInt16       logXOffset)
        {
            PCLWriter.stdJobHeader(prnWriter, "");

            if (formAsMacro)
                generateOverlay(prnWriter, true,
                                paperWidth, paperLength, logXOffset);

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
                                               Boolean      formAsMacro)
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
                                            Boolean      formAsMacro,
                                            UInt16       paperWidth,
                                            UInt16       paperLength,
                                            UInt16       logXOffset)
        {
            Int16 rulerWidth;
            Int16 rulerHeight;

            Int16 rulerCellsX;
            Int16 rulerCellsY;

            Int16 posX,
                  posY;

            Int16 lineInc,
                  ptSize;

            Int16 stroke = 1;

            //----------------------------------------------------------------//

            rulerCellsX = (Int16)((paperWidth / _unitsPerInch) + 1);
            rulerCellsY = (Int16)((paperLength / _unitsPerInch) + 1);
            rulerWidth  = (Int16)(rulerCellsX * _unitsPerInch);
            rulerHeight = (Int16)(rulerCellsY * _unitsPerInch);

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
            // Horizontal ruler.                                              //
            //                                                                //
            //----------------------------------------------------------------//

            posX = 0;
            posY = _rulerHOriginY;

            PCLWriter.lineHorizontal(prnWriter, posX, posY, rulerWidth, stroke);

            posY -= (_rulerDiv / 2);

            for (Int32 i = 0; i < rulerCellsX; i++)
            {
                PCLWriter.lineVertical(prnWriter, posX, posY,
                                       _rulerDiv * 2, stroke);

                posX += _rulerDiv;

                for (Int32 j = 1; j < _rulerDivPerCell; j++)
                {
                    PCLWriter.lineVertical(prnWriter, posX, posY,
                                           _rulerDiv, stroke);

                    posX += _rulerDiv;
                }
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Vertical ruler.                                                //
            //                                                                //
            //----------------------------------------------------------------//

            posX = _rulerVOriginX;
            posY = 0;

            PCLWriter.lineVertical(prnWriter, posX, posY, rulerHeight, stroke);

            posX -= (_rulerDiv / 2);

            for (Int32 i = 0; i < rulerCellsY; i++)
            {
                PCLWriter.lineHorizontal(prnWriter, posX, posY,
                                         _rulerDiv * 2, stroke);

                posY += _rulerDiv;

                for (Int32 j = 1; j < _rulerDivPerCell; j++)
                {
                    PCLWriter.lineHorizontal(prnWriter, posX, posY,
                                             _rulerDiv, stroke);

                    posY += _rulerDiv;
                }
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Left logical page margin - vertical line.                      //
            //                                                                //
            //----------------------------------------------------------------//

            PCLWriter.lineVertical(prnWriter, 0, 0,
                                   rulerHeight, stroke);

            //----------------------------------------------------------------//
            //                                                                //
            // Text.                                                          //
            //                                                                //
            //----------------------------------------------------------------//

            ptSize = 10;
            lineInc = (_rulerDiv * 2);

            PCLWriter.font(prnWriter, true, "19U",
                      "s1p" + ptSize + "v0s0b16602T");

            posX = (Int16)(_posXDesc - logXOffset);
            posY = _posYDesc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      "Paper size:");

            posY += lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      "Orientation:");

            posY += lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      "Paper width:");

            posY += lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      "Paper length:");

            posY += lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      "Logical page left offset:");

            posY += lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      "Logical page top offset:");

            posY += lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      "Logical page width:");

            posY += lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      "Logical page height:");

            //----------------------------------------------------------------//
            //                                                                //
            // Overlay end.                                                   //
            //                                                                //
            //----------------------------------------------------------------//

            if (formAsMacro)
                PCLWriter.macroControl(prnWriter, 0, PCLWriter.eMacroControl.StopDef);
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
                                         Boolean stdPage,
                                         UInt16 paperWidth,
                                         UInt16 paperLength,
                                         UInt16 logXOffset,
                                         Int16 logLeftOffset,
                                         Int16 logTopOffset,
                                         UInt16 logPageWidth,
                                         UInt16 logPageHeight)
        {
            const UInt16 dcptsPerInch = 720;

            const Double unitsToInches = (1.00 / _unitsPerInch);
            const Double unitsToMilliMetres = (25.4 / _unitsPerInch);

            const Double dcptsToInches      = (1.00 / dcptsPerInch);
            const Double dcptsToMilliMetres = (25.4 / dcptsPerInch);

            Int16 posX,
                  posY;

            Int16 lineInc,
                  ptSize;

            //----------------------------------------------------------------//

            if (formAsMacro)
                PCLWriter.macroControl(prnWriter, _macroId, 
                                       PCLWriter.eMacroControl.Call);
            else
                generateOverlay(prnWriter, false,
                                paperWidth, paperLength, logXOffset);

            //----------------------------------------------------------------//
            //                                                                //
            // Header.                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            ptSize = 15;

            PCLWriter.font(prnWriter, true, "19U",
                      "s1p" + ptSize + "v0s0b16602T");

            posX = (Int16)(_posXDesc - logXOffset);
            posY = _posYHddr;

            if (stdPage)
            {
                PCLWriter.text(prnWriter, posX, posY, 0,
                          "PCL Standard Logical Page sample");
            }
            else
            {
                PCLWriter.text(prnWriter, posX, posY, 0,
                          "PCL Define Logical Page sample");
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Paper description data.                                        //
            //                                                                //
            //----------------------------------------------------------------//

            ptSize = 10;
            lineInc = _rulerDiv * 2;

            PCLWriter.font(prnWriter, true, "19U",
                      "s0p" + (120 / ptSize) + "h0s3b4099T");

            posX = (Int16)((_posXDesc + (_rulerCell * 2)) - logXOffset);
            posY = _posYDesc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      PCLPaperSizes.getName(indxPaperSize));

            posY += lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      PCLOrientations.getName(indxOrientation));

            posY += lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      (paperWidth * unitsToMilliMetres).ToString("F0") +
                      "mm = " +
                      (paperWidth * unitsToInches).ToString("F2") +
                      "\"");

            posY += lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      (paperLength * unitsToMilliMetres).ToString("F0") +
                      "mm = " +
                      (paperLength * unitsToInches).ToString("F2") +
                      "\"");

            if (stdPage)
            {
                posY += lineInc;

                PCLWriter.text(prnWriter, posX, posY, 0,
                          "standard");

                posY += lineInc;

                PCLWriter.text(prnWriter, posX, posY, 0,
                          "standard");

                posY += lineInc;

                PCLWriter.text(prnWriter, posX, posY, 0,
                          "standard");

                posY += lineInc;

                PCLWriter.text(prnWriter, posX, posY, 0,
                          "standard");

                PCLWriter.formFeed(prnWriter);
            }
            else
            {
                posY += lineInc;

                PCLWriter.text(prnWriter, posX, posY, 0,
                          logLeftOffset.ToString("F0") +
                          " decipoints = " +
                          (logLeftOffset * dcptsToMilliMetres).ToString("F0") +
                          "mm = " +
                          (logLeftOffset * dcptsToInches).ToString("F2") +
                          "\"");

                posY += lineInc;

                PCLWriter.text(prnWriter, posX, posY, 0,
                          logTopOffset.ToString("F0") +
                          " decipoints = " +
                          (logTopOffset * dcptsToMilliMetres).ToString("F0") +
                          "mm = " +
                          (logTopOffset * dcptsToInches).ToString("F2") +
                          "\"");

                posY += lineInc;

                PCLWriter.text(prnWriter, posX, posY, 0,
                          logPageWidth.ToString("F0") +
                          " decipoints = " +
                          (logPageWidth * dcptsToMilliMetres).ToString("F0") +
                          "mm = " +
                          (logPageWidth * dcptsToInches).ToString("F2") +
                          "\"");

                posY += lineInc;

                PCLWriter.text(prnWriter, posX, posY, 0,
                          logPageHeight.ToString("F0") +
                          " decipoints = " +
                          (logPageHeight * dcptsToMilliMetres).ToString("F0") +
                          "mm = " +
                          (logPageHeight * dcptsToInches).ToString("F2") +
                          "\"");
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e P a g e S e t                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write test data page(s) to output file.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void generatePageSet (BinaryWriter prnWriter,
                                             Int32        indxPaperSize,
                                             Int32        indxPaperType,
                                             Int32        indxOrientation,
                                             Boolean      formAsMacro,
                                             Boolean      incStdPage,
                                             UInt16       paperWidth,
                                             UInt16       paperLength,
                                             UInt16       logXOffset,
                                             Int16        logLeftOffset,
                                             Int16        logTopOffset,
                                             UInt16       logPageWidth,
                                             UInt16       logPageHeight)
        {
            if (incStdPage)
            {
                generatePage (prnWriter,
                              indxPaperSize,
                              indxPaperType,
                              indxOrientation,
                              formAsMacro,
                              true,
                              paperWidth,
                              paperLength,
                              logXOffset,
                              logLeftOffset,
                              logTopOffset,
                              logPageWidth,
                              logPageHeight);
            }

            //----------------------------------------------------------------//

            PCLWriter.defLogPage(prnWriter,
                                  indxOrientation,
                                  logLeftOffset,
                                  logTopOffset,
                                  logPageWidth,
                                  logPageHeight);

            PCLWriter.marginLeft(prnWriter, 0);

            PCLWriter.marginTop(prnWriter, 0);

            //----------------------------------------------------------------//

            generatePage(prnWriter,
                  indxPaperSize,
                  indxPaperType,
                  indxOrientation,
                  formAsMacro,
                  false,
                  paperWidth,
                  paperLength,
                  logXOffset,
                  logLeftOffset,
                  logTopOffset,
                  logPageWidth,
                  logPageHeight);
        }
    }
}
