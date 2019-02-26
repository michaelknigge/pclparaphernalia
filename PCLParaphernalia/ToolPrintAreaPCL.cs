using System;
using System.IO;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides PCL support for the PrintArea tool.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class ToolPrintAreaPCL
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const String _hexChars          = "0123456789ABCDEF";

        const Int32 _macroId            = 1;
        const UInt16 _sessionUPI        = PCLWriter.sessionUPI;

        const Int16 _boxOuterEdge       = (_sessionUPI * 1);
        const Int16 _rulerOriginX       = (_sessionUPI * 1);
        const Int16 _rulerOriginY       = (_sessionUPI * 1);
        const Int16 _rulerCell          = (_sessionUPI * 1);
        const Int16 _rulerDiv           = (_rulerCell / 5);

        const Int16 _posXHddr           = _rulerOriginX + (2 * _rulerDiv);
        const Int16 _posXDesc           = _rulerOriginX + (1 * _rulerDiv);
        const Int16 _posYHddr           = _rulerOriginY - (2 * _rulerDiv);
        const Int16 _posYText           = _rulerOriginY + (2 * _rulerDiv);
        const Int16 _posYDesc           = _rulerOriginY + (6 * _rulerDiv);

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
                                       Int32        indxPlexMode,
                                       String       pjlCommand,
                                       Boolean      formAsMacro)
        {
            PCLOrientations.eAspect aspect;

            UInt16 paperWidth,
                   paperLength,
                   logXOffset;

            UInt16 A4Length,
                   A4Width;

            Single scaleText,
                   scaleTextLength,
                   scaleTextWidth;

            Boolean customPaperSize;

            //----------------------------------------------------------------//

            aspect = PCLOrientations.getAspect(indxOrientation);

            A4Length = PCLPaperSizes.getPaperLength(
                            (Byte)PCLPaperSizes.eIndex.ISO_A4,
                            _sessionUPI,
                            aspect);

            A4Width = PCLPaperSizes.getPaperWidth(
                            (Byte)PCLPaperSizes.eIndex.ISO_A4,
                            _sessionUPI,
                            aspect);

            if (PCLPaperSizes.isCustomSize(indxPaperSize))
                customPaperSize = true;
    //      else if (PCLPaperSizes.getIdPCL(indxPaperSize) == 0xff)
    //          customPaperSize = true;
            else
                customPaperSize = false;

            paperLength = PCLPaperSizes.getPaperLength(indxPaperSize,
                                                       _sessionUPI, aspect);

            paperWidth = PCLPaperSizes.getPaperWidth(indxPaperSize,
                                                     _sessionUPI, aspect);

            logXOffset = PCLPaperSizes.getLogicalOffset(indxPaperSize,
                                                        _sessionUPI, aspect);

            scaleTextLength = (Single)paperLength / A4Length;
            scaleTextWidth  = (Single)paperWidth  / A4Width;

            if (scaleTextLength < scaleTextWidth)
                scaleText = scaleTextLength;
            else
                scaleText = scaleTextWidth;

            //----------------------------------------------------------------//

            generateJobHeader(prnWriter,
                              indxPaperSize,
                              indxPaperType,
                              indxOrientation,
                              indxPlexMode,
                              pjlCommand,
                              formAsMacro,
                              customPaperSize,
                              paperWidth,
                              paperLength,
                              logXOffset,
                              scaleText);

            generatePage(prnWriter,
                         indxPaperSize,
                         indxPaperType,
                         indxOrientation,
                         indxPlexMode,
                         pjlCommand,
                         formAsMacro,
                         customPaperSize,
                         false,
                         paperWidth,
                         paperLength,
                         logXOffset,
                         scaleText);

            if (PCLPlexModes.getPlexType(indxPlexMode) !=
                PCLPlexModes.ePlexType.Simplex)
            {
                generatePage(prnWriter,
                             indxPaperSize,
                             indxPaperType,
                             indxOrientation,
                             indxPlexMode,
                             pjlCommand,
                             formAsMacro,
                             customPaperSize,
                             true,
                             paperWidth,
                             paperLength,
                             logXOffset,
                             scaleText);
            }

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
                                              Int32        indxPlexMode,
                                              String       pjlCommand,
                                              Boolean      formAsMacro,
                                              Boolean      customPaperSize,
                                              UInt16       paperWidth,
                                              UInt16       paperLength,
                                              UInt16       logXOffset,
                                              Single       scaleText)
        {
            PCLWriter.stdJobHeader(prnWriter, pjlCommand);

            if (formAsMacro)
                generateOverlay(prnWriter, true,
                                paperWidth, paperLength, logXOffset, scaleText);

            if (customPaperSize)
                PCLWriter.pageHeaderCustom (prnWriter,
                                            indxPaperType,
                                            indxOrientation,
                                            indxPlexMode,
                                            paperWidth,
                                            paperLength);
            else
                PCLWriter.pageHeader (prnWriter,
                                      indxPaperSize,
                                      indxPaperType,
                                      indxOrientation,
                                      indxPlexMode);
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
                                            UInt16       logXOffset,
                                            Single       scaleText)
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

            rulerCellsX = (Int16)((paperWidth  / _sessionUPI) - 1);
            rulerCellsY = (Int16)((paperLength / _sessionUPI) - 1);
            rulerWidth  = (Int16)(rulerCellsX * _sessionUPI);
            rulerHeight = (Int16)(rulerCellsY * _sessionUPI);

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

            posX = (Int16)(_rulerOriginX - logXOffset);
            posY = _rulerOriginY;

            PCLWriter.lineHorizontal(prnWriter, posX, posY, rulerWidth, stroke);

            posX += _rulerCell;

            for (Int32 i = 0; i < rulerCellsX; i++)
            {
                PCLWriter.lineVertical(prnWriter, posX, posY, _rulerDiv, stroke);
                
                posX += _rulerCell;
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Vertical ruler.                                                //
            //                                                                //
            //----------------------------------------------------------------//
            
            posX = (Int16)(_rulerOriginX - logXOffset);
            posY = _rulerOriginY;

            PCLWriter.lineVertical(prnWriter, posX, posY, rulerHeight, stroke);
                
            posY += _rulerCell;

            for (Int32 i = 0; i < rulerCellsY; i++)
            {
                PCLWriter.lineHorizontal(prnWriter, posX, posY,
                                         _rulerDiv, stroke);

                posY += _rulerCell;
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Left logical page margin - vertical line.                      //
            //                                                                //
            //----------------------------------------------------------------//

            PCLWriter.lineVertical(prnWriter, 0, _rulerOriginY,
                                   rulerHeight, stroke);

            //----------------------------------------------------------------//
            //                                                                //
            // Sample marker box.                                             //
            //                                                                //
            //----------------------------------------------------------------//

            lineInc = (Int16)((_sessionUPI * scaleText) / 8);

            posX = (Int16)((_rulerCell * 5.5 * scaleText) - logXOffset);
            posY = (Int16)(_posYDesc - lineInc);

            generateSquare(prnWriter, posX, posY, true);

            //----------------------------------------------------------------//
            //                                                                //
            // Text.                                                          //
            //                                                                //
            //----------------------------------------------------------------//

            ptSize = (Int16)(15 * scaleText);

            PCLWriter.font(prnWriter, true, "19U",
                      "s1p" + ptSize + "v0s0b16602T");

            posX = (Int16)(_posXHddr - logXOffset);
            posY = _posYHddr;

            PCLWriter.text(prnWriter, posX, posY, 0, 
                      "PCL print area sample");

            ptSize = (Int16)(10 * scaleText);

            PCLWriter.font(prnWriter, true, "19U",
                      "s1p" + ptSize + "v0s0b16602T");

            posX = (Int16)(_posXDesc - logXOffset);
            posY = _posYDesc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      "Paper size:");

            posY += lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      "Paper type:");

            posY += lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0, 
                      "Orientation:");

            posY += lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      "Plex mode:");

            posY += lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0, 
                      "Paper width:");
            
            posY += lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0, 
                      "Paper length:");
            
            posY += lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0, 
                      "PJL option:");

            //----------------------------------------------------------------//

            posY = (Int16)(_posYDesc + (_rulerCell * scaleText));
           
            PCLWriter.text(prnWriter, posX, posY, 0, 
                      "Black squares of side 3 units, each containing a" +
                      " central white square of side one");

            posY += lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0, 
                      "unit, and some directional markers, as per the" +
                      " half-size sample above,");

            posY += lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0, 
                      "demonstrate how objects are clipped by the" +
                      " boundaries of the printable area.");

            posY += lineInc;
            posY += lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0, 
                      "The four corner squares are (theoretically) positioned" +
                      " in the corners of the");
            
            posY += lineInc;
            
            PCLWriter.text(prnWriter, posX, posY, 0, 
                      "physical sheet, except that the left edges of the top" +
                      " and bottom left-hand squares");
            
            posY += lineInc;
            
            PCLWriter.text(prnWriter, posX, posY, 0, 
                      "are constrained to be positioned at the left margin" +
                      " of the PCL logical page,");
            
            posY += lineInc;
            
            PCLWriter.text(prnWriter, posX, posY, 0, 
                      "which is inset from the sheet edge, and marked here" + 
                      " with a vertical line.");

            posY += lineInc;
            posY += lineInc;
            
            PCLWriter.text(prnWriter, posX, posY, 0, 
                      "The middle left-hand square is positioned relative" +
                      " to the bottom and right logical");
            
            posY += lineInc;
            
            PCLWriter.text(prnWriter, posX, posY, 0, 
                      "page margins, and rotated 180 degrees.");

            posY += lineInc;
            posY += lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      "Fixed pitch (10 cpi) text characters are also clipped" +
                      " by the boundaries of the");

            posY += lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      "printable area; one set is shown relative to the" + 
                      " left logical page margin, and");
            
            posY += lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      "another set (rotated 180 degrees) is shown" + 
                      " relative to the right margin.");

            posY += lineInc;
            posY += lineInc;
            
            PCLWriter.text(prnWriter, posX, posY, 0, 
                      "PJL options may move the logical page and/or" +
                      " unprintable area margins relative");

            posY += lineInc;
            
            PCLWriter.text(prnWriter, posX, posY, 0, 
                      "to the physical sheet.");

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
                                         Int32        indxPaperSize,
                                         Int32        indxPaperType,
                                         Int32        indxOrientation,
                                         Int32        indxPlexMode,
                                         String       pjlCommand,
                                         Boolean      formAsMacro,
                                         Boolean      customPaperSize,
                                         Boolean      rearFace,
                                         UInt16       paperWidth,
                                         UInt16       paperLength,
                                         UInt16       logXOffset,
                                         Single       scaleText)
        {
            const String digitsTextA = "         1         2" +
                                       "         3         4" +
                                       "         5         6" +
                                       "         7         8" +
                                       "         9        10" +
                                       "        11        12" +
                                       "        13        14" +
                                       "        15        16" +
                                       "        17        18";

            const String digitsTextB = "12345678901234567890" +
                                       "12345678901234567890" +
                                       "12345678901234567890" +
                                       "12345678901234567890" +
                                       "12345678901234567890" +
                                       "12345678901234567890" +
                                       "12345678901234567890" +
                                       "12345678901234567890" +
                                       "12345678901234567890";

            const Double unitsToInches      = (1.00 / _sessionUPI);
            const Double unitsToMilliMetres = (25.4 / _sessionUPI);

            const Int16 bottomMargin = (Int16)(0.5 * _sessionUPI); 

            Int16 squareRightX,
                  squareBottomY;

            Int16 posX,
                  posY;

            Int16 lineInc,
                  ptSize;

            Int32 ctA;
            
            //----------------------------------------------------------------//
            
            if (formAsMacro)
                PCLWriter.macroControl(prnWriter, _macroId, PCLWriter.eMacroControl.Call);
            else
                generateOverlay(prnWriter, false,
                                paperWidth, paperLength, logXOffset, scaleText);

            //----------------------------------------------------------------//
            //                                                                //
            // Corner squares.                                                //
            //                                                                //
            //----------------------------------------------------------------//

            squareRightX  = (Int16)(paperWidth  - _boxOuterEdge - logXOffset);
            squareBottomY = (Int16)(paperLength - _boxOuterEdge);

            // Top-left.                                                      //

            posX = 0;
            posY = 0;

            generateSquare (prnWriter, posX, posY, false);

            // Top-right.                                                     //

            posX = squareRightX;
            posY = 0;

            generateSquare (prnWriter, posX, posY, false);

            // Bottom-left.                                                   //

            posX = 0;
            posY = squareBottomY;

            generateSquare (prnWriter, posX, posY, false);

            // Bottom-right.                                                  //

            posX = squareRightX;
            posY = squareBottomY;

            generateSquare (prnWriter, posX, posY, false);

            //----------------------------------------------------------------//
            //                                                                //
            // Paper description data.                                        //
            //                                                                //
            //----------------------------------------------------------------//

            ptSize = (Int16)(10 * scaleText);
            lineInc = (Int16)((_sessionUPI * scaleText) / 8);

            PCLWriter.font(prnWriter, true, "19U",
                      "s0p" + (120 / ptSize) + "h0s3b4099T");

            posX = (Int16)((_posXDesc + (_rulerCell * scaleText)) - logXOffset);
            posY = _posYDesc;

            if (customPaperSize)
                PCLWriter.text(prnWriter, posX, posY, 0,
                          PCLPaperSizes.getNameAndDesc(indxPaperSize));
            else
                PCLWriter.text(prnWriter, posX, posY, 0,
                          PCLPaperSizes.getName(indxPaperSize));

            posY += lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      PCLPaperTypes.getName(indxPaperType));

            posY += lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      PCLOrientations.getName(indxOrientation));

            posY += lineInc;

            if (rearFace)
                PCLWriter.text(prnWriter, posX, posY, 0,
                          PCLPlexModes.getName(indxPlexMode) +
                            ": rear face");
            else
                PCLWriter.text(prnWriter, posX, posY, 0,
                          PCLPlexModes.getName(indxPlexMode));

            posY += lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      (Math.Round((paperWidth *
                                   unitsToMilliMetres), 2)).ToString("F1") +
                      " mm = " +
                      (Math.Round((paperWidth *
                                   unitsToInches), 3)).ToString("F3") +
                      "\"");

            posY += lineInc;

            PCLWriter.text(prnWriter, posX, posY, 0,
                      (Math.Round((paperLength *
                                   unitsToMilliMetres), 2)).ToString("F1") +
                      " mm = " +
                      (Math.Round((paperLength *
                                   unitsToInches), 3)).ToString("F3") +
                      "\"");

            posY += lineInc;

            if (pjlCommand == "")
                PCLWriter.text(prnWriter, posX, posY, 0, "<none>");
            else
                PCLWriter.text(prnWriter, posX, posY, 0, pjlCommand);

            //----------------------------------------------------------------//
            //                                                                //
            // Fixed-pitch 10cpi text - not rotated.                          //
            //                                                                //
            //----------------------------------------------------------------//

            PCLWriter.font(prnWriter, true, "19U", "s0p10h0s0b4099T");

            posY = _posYText;

            ctA = (paperWidth * 10) / _sessionUPI;
            
            PCLWriter.text(prnWriter, 0, posY, 0, digitsTextA.Substring(0, ctA));
           
            posY += _rulerDiv;
            
            PCLWriter.text(prnWriter, 0, posY, 0, digitsTextB.Substring(0, ctA));

            //----------------------------------------------------------------//
            //                                                                //
            // Rotate print direction by 180-degrees.                         //
            //                                                                //
            //----------------------------------------------------------------//

            PCLWriter.printDirection(prnWriter, 180);

            //----------------------------------------------------------------//
            //                                                                //
            // Fixed-pitch 10cpi text - 180-degree rotated.                   //
            //                                                                //
            //----------------------------------------------------------------//

            posY = (Int16)(paperLength - _posYText - (2 * bottomMargin));

            ctA = (paperWidth * 10) / _sessionUPI;

            PCLWriter.text(prnWriter, 0, posY, 0, digitsTextA.Substring(0, ctA));

            posY += _rulerDiv;

            PCLWriter.text(prnWriter, 0, posY, 0, digitsTextB.Substring(0, ctA));

            //----------------------------------------------------------------//
            //                                                                //
            // Left box: rotated (180-degree) orientation.                    //
            //                                                                //
            //----------------------------------------------------------------//

            posX = squareRightX;
            posY = (Int16)(((paperLength - _boxOuterEdge) / 2) - bottomMargin);

            generateSquare (prnWriter, posX, posY, false);

            //----------------------------------------------------------------//
            //                                                                //
            // Reset print direction to 0-degrees.                            //
            //                                                                //
            //----------------------------------------------------------------//

            PCLWriter.printDirection(prnWriter, 0);

            //----------------------------------------------------------------//

            PCLWriter.formFeed(prnWriter);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e S q u a r e                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate box-in-box square.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void generateSquare(BinaryWriter prnWriter,
                                           Int16        startX,
                                           Int16        startY,
                                           Boolean      halfSize)
        {
            Int32 scaler;

            Int16 boxOuterEdge,
                  boxInnerEdge,
                  boxInnerOffset,
                  boxMarkerEdge,
                  boxMarkerOffset;

            //----------------------------------------------------------------//
            //                                                                //
            // Set dimensions for full size or half-size image.               //
            //                                                                //
            //----------------------------------------------------------------//

            if (halfSize)
                scaler = 2;
            else
                scaler = 1;

            boxOuterEdge    = (Int16)(_boxOuterEdge / scaler);
            boxInnerEdge    = (Int16)((_boxOuterEdge / 3) / scaler);
            boxInnerOffset  = (Int16)((_boxOuterEdge / 3) / scaler);
            boxMarkerEdge   = (Int16)((_boxOuterEdge / 15) / scaler);
            boxMarkerOffset = (Int16)(((_boxOuterEdge -
                                        boxMarkerEdge) / 2) / scaler);

            //----------------------------------------------------------------//
            //                                                                //
            // Outer square (black).                                          //
            //                                                                //
            //----------------------------------------------------------------//

            Int16 posX = startX;
            Int16 posY = startY;

            PCLWriter.rectangleSolid(prnWriter, posX, posY,
                                     boxOuterEdge, boxOuterEdge,
                                     false, false, false);

            //----------------------------------------------------------------//
            //                                                                //
            // Inner square (white).                                          //
            //                                                                //
            //----------------------------------------------------------------//

            posX += boxInnerOffset;
            posY += boxInnerOffset;

            PCLWriter.rectangleSolid(prnWriter, posX, posY,
                                     boxInnerEdge, boxInnerEdge,
                                     true, false, false);

            //----------------------------------------------------------------//
            //                                                                //
            // Top marker rectangle (white).                                  //
            //                                                                //
            //----------------------------------------------------------------//

            posX = (Int16)(startX + boxMarkerOffset);
            posY = startY;

            PCLWriter.rectangleSolid(prnWriter, posX, posY,
                                     boxInnerOffset,
                                     boxMarkerEdge,
                                     true, false, false);

            //----------------------------------------------------------------//
            //                                                                //
            // Left marker rectangle (white).                                 //
            //                                                                //
            //----------------------------------------------------------------//

            posX = startX;
            posY = (Int16)(startY + boxMarkerOffset);

            PCLWriter.rectangleSolid(prnWriter, posX, posY,
                                     boxMarkerEdge,
                                     boxInnerOffset,
                                     true, false, false);
        }
    }
}
