using System;
using System.IO;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides PCL support for the Logical Operations action
    /// of the MiscSamples tool.
    /// 
    /// © Chris Hutchinson 2014
    /// 
    /// </summary>

    static class ToolMiscSamplesActLogOperPCL
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const UInt16 _unitsPerInch      = PCLWriter.sessionUPI;

        const Int16 _rasterRes          = 600;
        const Byte  _defaultROP         = 252;

        const Int32 _macroIdDestBox         = 101;
        const Int32 _macroIdDestBoxRow      = 111;
        const Int32 _macroIdDestBoxRowHddr  = 112;
        const Int32 _macroIdDestBoxPage     = 121;
        const Int32 _macroIdSrcBoxRasterPos = 201;
        const Int32 _macroIdSrcBoxRasterNeg = 202;
        const Int32 _macroIdSrcBoxRasters   = 211;
        const Int32 _macroIdSrcBoxText      = 212;
        const Int32 _macroIdSrcBox          = 221; 
        const Int32 _macroIdSrcBoxRow       = 231;
 
        const Int32 _patternId              = 101;

        const Int16 _incInch     = (_unitsPerInch * 1);
        const Int16 _pageOriginX = (_incInch * 1);
        const Int16 _pageOriginY = (_incInch * 1) / 2;
        const Int16 _rowInc      = (_incInch * 5) / 4;
        const Int16 _colInc      = (_incInch * 5) / 4;
        const Int16 _lineInc     = (_incInch / 6);

        const Int16 _posXPage_1_Hddr  = _pageOriginX;
        const Int16 _posYPage_1_Hddr  = _pageOriginY + (_incInch * 1) / 2;
        const Int16 _posYPage_1_Data1 = _pageOriginY + (_incInch * 9) / 4;
        const Int16 _posYPage_1_Data2 = _pageOriginY + (_incInch * 13) / 2;

        const Int16 _posXPage_n_Hddr  = _pageOriginX;
        const Int16 _posYPage_n_Hddr  = _pageOriginY;

        const Int16 _posXPage_n_Data = _pageOriginX;
        const Int16 _posYPage_n_Data = _pageOriginY + (_incInch / 3);

        const Int16 _destBoxSide = _incInch;
        
        const Int16 _sourceImagePixelsWidth  = 192;
        const Int16 _sourceImagePixelsHeight = 192;

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        static Int32 _indxFontArial   = PCLFonts.getIndexForName ("Arial");
        static Int32 _indxFontCourier = PCLFonts.getIndexForName ("Courier");

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
                                       Int32        indxPalette,
                                       Int32        indxClrD1,
                                       Int32        indxClrD2,
                                       Int32        indxClrS1,
                                       Int32        indxClrS2,
                                       Int32        indxClrT1,
                                       Int32        indxClrT2,
                                       Int32        minROP,
                                       Int32        maxROP,
                                       Boolean      flagUseMacros)
        {
            const PCLOrientations.eAspect aspectPort
                    = PCLOrientations.eAspect.Portrait;

            PCLOrientations.eAspect aspect;

            UInt16 paperWidth,
                   paperLength,
                   paperLengthPort,
                   logXOffset;

            Boolean flagOptColour;

            Byte idClrD1 = 0,
                 idClrD2 = 0,
                 idClrS1 = 0,
                 idClrS2 = 0,
                 idClrT1 = 0,
                 idClrT2 = 0,
                 idClrBlack = 0,
                 idClrWhite = 0;

            String nameClrD1,
                   nameClrD2,
                   nameClrS1,
                   nameClrS2,
                   nameClrT1,
                   nameClrT2;

            //----------------------------------------------------------------//

            aspect = PCLOrientations.getAspect (indxOrientation);

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

            idClrBlack = PCLPalettes.getColourId(
                            indxPalette,
                            PCLPalettes.getClrItemBlack(indxPalette));
            idClrWhite = PCLPalettes.getColourId(
                            indxPalette,
                            PCLPalettes.getClrItemWhite(indxPalette));

            idClrD1 = PCLPalettes.getColourId(indxPalette, indxClrD1);
            idClrS1 = PCLPalettes.getColourId(indxPalette, indxClrS1);
            idClrT1 = PCLPalettes.getColourId(indxPalette, indxClrT1);

            idClrD2 = PCLPalettes.getColourId(indxPalette, indxClrD2);
            idClrS2 = PCLPalettes.getColourId(indxPalette, indxClrS2);
            idClrT2 = PCLPalettes.getColourId(indxPalette, indxClrT2);

            nameClrD1 = PCLPalettes.getColourName(indxPalette, indxClrD1);
            nameClrD2 = PCLPalettes.getColourName(indxPalette, indxClrD2);
            nameClrS1 = PCLPalettes.getColourName(indxPalette, indxClrS1);
            nameClrS2 = PCLPalettes.getColourName(indxPalette, indxClrS2);
            nameClrT1 = PCLPalettes.getColourName(indxPalette, indxClrT1);
            nameClrT2 = PCLPalettes.getColourName(indxPalette, indxClrT2);

            if (PCLPalettes.isMonochrome (indxPalette))
                flagOptColour = false;
            else
                flagOptColour = true;

            //----------------------------------------------------------------//

            generateJobHeader(prnWriter,
                              indxPaperSize,
                              indxPaperType,
                              indxOrientation,
                              logXOffset);

            //----------------------------------------------------------------//

            PCLWriter.paletteSimple (prnWriter,
                                     PCLPalettes.getPaletteId (indxPalette));

            PCLWriter.rasterResolution (prnWriter, _rasterRes, false);

            if (flagOptColour)
            {
                if (idClrT1 == idClrBlack)                                   // ***** DO WE NEED TO DISTINGUISH THIS *****
                    writePattern (prnWriter, _patternId, idClrT1, idClrT2,
                                  false);
                else
                    writePattern (prnWriter, _patternId, idClrT1, idClrT2,
                                  true);
            }
            else
            {
                writePattern (prnWriter, _patternId, idClrT1, idClrT2,
                              false);
            }

            if (flagUseMacros)
            {
                writeDestBox (prnWriter, idClrD1, idClrD2, idClrBlack,
                              flagOptColour, true);

                writeDestBoxRow (prnWriter, idClrD1, idClrD2, idClrBlack,
                                 flagOptColour, true);

                writeDestBoxRowHddr (prnWriter, true);

                writeDestBoxPage (prnWriter, logXOffset, idClrD1, idClrD2,
                                  idClrBlack, flagOptColour, true);

                writeSrcBoxRaster (prnWriter, idClrS1, idClrS2,
                                   false, flagOptColour, true);
                writeSrcBoxRaster (prnWriter, idClrS1, idClrS2,
                                   true,  flagOptColour, true);

                writeSrcBoxText (prnWriter, idClrS1, idClrS2, idClrBlack,
                                 flagOptColour, true);

                writeSrcBox (prnWriter, idClrS1, idClrS2, idClrBlack,
                             flagOptColour, true);

                writeSrcBoxRow (prnWriter, idClrS1, idClrS2, idClrBlack,
                                flagOptColour, true);
            }

            generatePageSet (prnWriter,
                             logXOffset,
                             indxPalette,
                             idClrD1,
                             idClrD2,
                             idClrS1,
                             idClrS2,
                             idClrBlack,
                             idClrWhite,
                             nameClrD1,
                             nameClrD2,
                             nameClrS1,
                             nameClrS2,
                             nameClrT1,
                             nameClrT2,
                             minROP,
                             maxROP,
                             flagOptColour,
                             flagUseMacros);

            generateJobTrailer(prnWriter, flagUseMacros);
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
                                              UInt16       logXOffset)
        {
            PCLWriter.stdJobHeader(prnWriter, "");

            PCLWriter.pageHeader(prnWriter,
                                 indxPaperSize,
                                 indxPaperType,
                                 indxOrientation,
                                 (Int32) PCLPlexModes.eIndex.DuplexLongEdge);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e J o b T r a i l e r                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write tray map termination sequences to output file.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void generateJobTrailer (BinaryWriter prnWriter,
                                                Boolean      flagUseMacros)
        {
            PCLWriter.patternDelete (prnWriter, _patternId);

            if (flagUseMacros)
            {
                PCLWriter.macroControl (prnWriter, _macroIdSrcBoxRow,
                                        PCLWriter.eMacroControl.Delete);

                PCLWriter.macroControl (prnWriter, _macroIdSrcBox,
                                        PCLWriter.eMacroControl.Delete);

                PCLWriter.macroControl (prnWriter, _macroIdSrcBoxText,
                                        PCLWriter.eMacroControl.Delete);

                PCLWriter.macroControl (prnWriter, _macroIdSrcBoxRasterPos,
                                        PCLWriter.eMacroControl.Delete);

                PCLWriter.macroControl (prnWriter, _macroIdSrcBoxRasterNeg,
                                        PCLWriter.eMacroControl.Delete);

                PCLWriter.macroControl (prnWriter, _macroIdDestBoxPage,
                                        PCLWriter.eMacroControl.Delete);

                PCLWriter.macroControl (prnWriter, _macroIdDestBoxRowHddr,
                                        PCLWriter.eMacroControl.Delete);

                PCLWriter.macroControl (prnWriter, _macroIdDestBoxRow,
                                        PCLWriter.eMacroControl.Delete);

                PCLWriter.macroControl (prnWriter, _macroIdDestBox,
                                        PCLWriter.eMacroControl.Delete);
            }

            PCLWriter.stdJobTrailer (prnWriter, false, 0);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e P a g e _ 1                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write introductory page sequences to output file.                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void generatePage_1 (BinaryWriter prnWriter,
                                            UInt16       logXOffset,
                                            Int32        indxPalette,
                                            Byte         minROP,
                                            Byte         maxROP,
                                            Byte         idClrD1,
                                            Byte         idClrD2,
                                            Byte         idClrS1,
                                            Byte         idClrS2,
                                            Byte         idClrBlack,
                                            Byte         idClrWhite,
                                            String       nameClrD1,
                                            String       nameClrD2,
                                            String       nameClrS1,
                                            String       nameClrS2,
                                            String       nameClrT1,
                                            String       nameClrT2,
                                            Boolean      flagOptColour,
                                            Boolean      flagUseMacros)
        {
            Int16 posX,
                  posY;

            Int16 ptSize,
                  srcOffsetX,
                  srcOffsetY;

            String nameClrSpace;

            //----------------------------------------------------------------//

            srcOffsetX = (Int16) (((_destBoxSide / 2) -
                                    _sourceImagePixelsWidth) / 2);
            srcOffsetY = (Int16) ((_destBoxSide -
                                   _sourceImagePixelsHeight) / 2); 

            //----------------------------------------------------------------//
            //                                                                //
            // Heading and introductory texts.                                //
            //                                                                //
            //----------------------------------------------------------------//

            nameClrSpace = PCLPalettes.getPaletteName(indxPalette);

            if (flagOptColour)
                PCLWriter.setForegroundColour (prnWriter, idClrBlack);
            
            ptSize = 15;

            PCLWriter.font (prnWriter, true, "19U",
                           PCLFonts.getPCLFontSelect (_indxFontArial,
                                                      PCLFonts.eVariant.Bold,
                                                      ptSize, 0));

            posX = (Int16) (_posXPage_1_Hddr - logXOffset);
            posY = _posYPage_1_Hddr;

            PCLWriter.text (prnWriter, posX, posY, 0,
                      "PCL Logical Operations samples:");

            ptSize = 12;

            posY += (_lineInc * 3);

            PCLWriter.font (prnWriter, true, "19U",
                           PCLFonts.getPCLFontSelect (_indxFontArial,
                                                      PCLFonts.eVariant.Regular,
                                                      ptSize, 0));

            PCLWriter.text (prnWriter, posX, posY, 0,
                      "Palette = " + nameClrSpace);

            posY += (_lineInc * 2);

            PCLWriter.text (prnWriter, posX, posY, 0,
                      "Shows how a Source image, in conjunction with a" +
                      " Texture (a Pattern and colour");

            posY += _lineInc;

            PCLWriter.text (prnWriter, posX, posY, 0,
                      "combination) interacts with a Destination image" +
                      " (i.e. what is already on the page),");

            posY += _lineInc;

            PCLWriter.text (prnWriter, posX, posY, 0,
                      "and the effect of the different Logical Operation" +
                      " (ROP) values, together with Source");
            
             posY += _lineInc;

            PCLWriter.text (prnWriter, posX, posY, 0,
                      "and Texture (pattern) transparency settings.");

            //----------------------------------------------------------------//

            ptSize = 12;

            PCLWriter.font (prnWriter, true, "19U",
                           PCLFonts.getPCLFontSelect (_indxFontCourier,
                                                      PCLFonts.eVariant.Regular,
                                                      ptSize, 0));
            posY = _posYPage_1_Data1;

            PCLWriter.text (prnWriter, posX, posY, 0,
                      "(D)estination:");

            posY += _rowInc;

            PCLWriter.text (prnWriter, posX, posY, 0,
                      "(S)ource:");

            posY += _rowInc;

            PCLWriter.text (prnWriter, posX, posY, 0,
                      "(T)exture (pattern):");

            //----------------------------------------------------------------//
            //                                                                //
            // Destination image.                                             //
            //                                                                //
            //----------------------------------------------------------------//

            posX += (_colInc * 2);
            posY = _posYPage_1_Data1;

            PCLWriter.cursorPosition (prnWriter,
                                      posX,
                                      posY);
            if (flagUseMacros)
                PCLWriter.macroControl (prnWriter, _macroIdDestBox,
                                        PCLWriter.eMacroControl.Call);
            else
                writeDestBox (prnWriter, idClrD1, idClrD2, idClrBlack,
                              flagOptColour, false);

            if (flagOptColour)
                PCLWriter.setForegroundColour (prnWriter, idClrBlack);

            //----------------------------------------------------------------//
            //                                                                //
            // Source image.                                                  //
            //                                                                //
            //----------------------------------------------------------------//

            posY += _rowInc;

            posX += srcOffsetX;
            posY += srcOffsetY;

            PCLWriter.cursorPosition (prnWriter,
                                      posX,
                                      posY);

            ptSize = 28;

            PCLWriter.font (prnWriter, true, "19U",
                           PCLFonts.getPCLFontSelect (_indxFontArial,
                                                      PCLFonts.eVariant.Regular,
                                                      ptSize, 0));

            if (flagUseMacros)
            {
                PCLWriter.macroControl (prnWriter, _macroIdSrcBox,
                                        PCLWriter.eMacroControl.Call);
            }
            else
            {
                writeSrcBox (prnWriter, idClrS1, idClrS2, idClrBlack,
                             flagOptColour, false);
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Texture (pattern).                                             //
            //                                                                //
            //----------------------------------------------------------------//

            posY += _rowInc;

            posX -= srcOffsetX;
            posY -= srcOffsetY;

            PCLWriter.rectangleUserFill (prnWriter,
                                         posX,
                                         posY,
                                         _destBoxSide,
                                         _destBoxSide,
                                         _patternId,
                                         false,
                                         false);

            //----------------------------------------------------------------//
            //                                                                //
            // Image explanatory texts.                                       //
            //                                                                //
            //----------------------------------------------------------------//

            posX = (Int16) (_posXPage_1_Hddr - logXOffset);
            posX += (_rowInc * 3);

            posY = _posYPage_1_Data1;

            ptSize = 8;

            PCLWriter.font (prnWriter, true, "19U",
                           PCLFonts.getPCLFontSelect (_indxFontCourier,
                                                      PCLFonts.eVariant.Regular,
                                                      ptSize, 0));

            PCLWriter.text (prnWriter, posX, posY, 0,
                      "colours = " + nameClrD1 + " / " + nameClrD2);

            //----------------------------------------------------------------//
            
            posY = _posYPage_1_Data1;
            posY += _rowInc;
            
            PCLWriter.text (prnWriter, posX, posY, 0,
                      "colours = " + nameClrS1 + " / " + nameClrS2);

            posY += _lineInc;

            PCLWriter.text (prnWriter, posX, posY, 0,
                      "includes:");

            posY += _lineInc;

            PCLWriter.text (prnWriter, posX, posY, 0,
                      " - small square raster image");

            posY += _lineInc;

            PCLWriter.text (prnWriter, posX, posY, 0,
                      " - inverse copy of raster image");

            posY += _lineInc;

            PCLWriter.text (prnWriter, posX, posY, 0,
                      " - text (the letter 'O' in each colour)");

            //----------------------------------------------------------------//

            posY = _posYPage_1_Data1;
            posY += (_rowInc * 2);

            PCLWriter.text (prnWriter, posX, posY, 0,
                      "colours = " + nameClrT1 + " / " + nameClrT2);

            //----------------------------------------------------------------//
            //                                                                //
            // Sample with default ROP.                                       //
            //                                                                //
            //----------------------------------------------------------------//

            ptSize = 12;

            posX = (Int16) (_posXPage_1_Hddr - logXOffset);
            posY = _posYPage_1_Data2;

            PCLWriter.font (prnWriter, true, "19U",
                           PCLFonts.getPCLFontSelect (_indxFontCourier,
                                                      PCLFonts.eVariant.Bold,
                                                      ptSize, 0));

            PCLWriter.text (prnWriter, posX, posY, 0,
                      "Sample (using default ROP):");

            posY += _rowInc / 2;

            PCLWriter.cursorPosition (prnWriter,
                                      posX,
                                      posY);

            if (flagUseMacros)
                PCLWriter.macroControl (prnWriter, _macroIdDestBoxRowHddr,
                                        PCLWriter.eMacroControl.Call);
            else
                writeDestBoxRowHddr (prnWriter, false);

            posY += _incInch / 3;

            ptSize = 10;

            PCLWriter.font (prnWriter, true, "19U",
                           PCLFonts.getPCLFontSelect (_indxFontCourier,
                                                      PCLFonts.eVariant.Regular,
                                                      ptSize, 0));

            PCLWriter.text (prnWriter, posX, posY, 0,
                            PCLLogicalOperations.getDescShort (_defaultROP));


            posY += _lineInc;

            PCLWriter.text (prnWriter, posX, posY, 0,
                            PCLLogicalOperations.actInfix (_defaultROP));

            posY -= _lineInc;
            posX += _colInc;

            PCLWriter.cursorPosition (prnWriter,
                                      posX,
                                      posY);

            if (flagUseMacros)
                PCLWriter.macroControl (prnWriter, _macroIdDestBoxRow,
                                        PCLWriter.eMacroControl.Call);
            else
                writeDestBoxRow (prnWriter, idClrD1, idClrD2, idClrBlack,
                                 flagOptColour, false);

            //----------------------------------------------------------------//

            ptSize = 28;

            PCLWriter.font (prnWriter, true, "19U",
                           PCLFonts.getPCLFontSelect (_indxFontArial,
                                                      PCLFonts.eVariant.Regular,
                                                      ptSize, 0));

            //----------------------------------------------------------------//

            posX += srcOffsetX;
            posY += srcOffsetY;

            PCLWriter.patternSet (prnWriter,
                                  PCLWriter.ePatternType.UserDefined,
                                  _patternId);

            PCLWriter.setROP (prnWriter, _defaultROP);

            PCLWriter.cursorPosition (prnWriter,
                                      posX,
                                      posY);

            if (flagUseMacros)
            {
                PCLWriter.macroControl (prnWriter, _macroIdSrcBoxRow,
                                        PCLWriter.eMacroControl.Call);
            }
            else
                writeSrcBoxRow (prnWriter, idClrS1, idClrS2, idClrBlack,
                                flagOptColour, false);

            //----------------------------------------------------------------//
            //                                                                //
            // Explanatory text for following pages.                          //
            //                                                                //
            //----------------------------------------------------------------//

            if (flagOptColour)
                PCLWriter.setForegroundColour (prnWriter, idClrBlack);

            PCLWriter.patternSet (prnWriter,
                                  PCLWriter.ePatternType.SolidBlack,
                                  -1);

            ptSize = 12;

            PCLWriter.font (prnWriter, true, "19U",
                           PCLFonts.getPCLFontSelect (_indxFontArial,
                                                      PCLFonts.eVariant.Bold,
                                                      ptSize, 0));
            posX -= _colInc;
            posY += _rowInc;

            PCLWriter.text (prnWriter, posX, posY, 0,
                      "The following pages show the effects of the various" + 
                      " Logical Operation (ROP)");
 
            posY += _lineInc;

            PCLWriter.text (prnWriter, posX, posY, 0,
                      "values (in the range " +
                      PCLLogicalOperations.getDescShort (minROP) +
                      " - " +
                      PCLLogicalOperations.getDescShort (maxROP) +
                      "), when combined with");

            posY += _lineInc;

            PCLWriter.text (prnWriter, posX, posY, 0,
                      "different Source and Texture (pattern) transparency" +
                      " settings:");
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e P a g e _ n                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write individual test data page sequences to output file.          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void generatePage_n (BinaryWriter prnWriter,
                                            UInt16       logXOffset,
                                            Byte         startROP,
                                            Byte         idClrD1,
                                            Byte         idClrD2,
                                            Byte         idClrS1,
                                            Byte         idClrS2,
                                            Byte         idClrBlack,
                                            Byte         idClrWhite,
                                            Boolean      flagOptColour,
                                            Boolean      flagUseMacros)
        {
            Int16 posX,
                  posY;

            Int16 ptSize,
                  srcOffsetX,
                  srcOffsetY;

            //----------------------------------------------------------------//

            srcOffsetX = (Int16) (((_destBoxSide / 2) -
                                    _sourceImagePixelsWidth) / 2);
            srcOffsetY = (Int16) ((_destBoxSide -
                                   _sourceImagePixelsHeight) / 2); 

            //----------------------------------------------------------------//

            PCLWriter.patternSet (prnWriter,
                                  PCLWriter.ePatternType.SolidBlack,
                                  -1);

            PCLWriter.setROP (prnWriter, (Byte) (_defaultROP));

            ptSize = 10;

            PCLWriter.font (prnWriter, true, "19U",
                           PCLFonts.getPCLFontSelect (_indxFontCourier,
                                                      PCLFonts.eVariant.Regular,
                                                      ptSize, 0));

            posX = (Int16) (_posXPage_n_Data - logXOffset);
            posY = _posYPage_n_Data;

            for (Int32 i = 0; i < 8; i++)
            {
                PCLWriter.text (prnWriter, posX, posY, 0,
                                PCLLogicalOperations.getDescShort (startROP + i));

                posY += _lineInc;

                PCLWriter.text (prnWriter, posX, posY, 0,
                                PCLLogicalOperations.actInfix (startROP + i));

                posY -= _lineInc;

                posY += _rowInc;
            }


            //----------------------------------------------------------------//

            if (flagUseMacros)
                PCLWriter.macroControl (prnWriter, _macroIdDestBoxPage,
                                        PCLWriter.eMacroControl.Call);
            else
                writeDestBoxPage (prnWriter, logXOffset, idClrD1, idClrD2,
                                  idClrBlack, flagOptColour, false);

            //----------------------------------------------------------------//

            if (flagOptColour)
                PCLWriter.setForegroundColour (prnWriter, idClrBlack);

            //----------------------------------------------------------------//

            ptSize = 28;

            PCLWriter.font (prnWriter, true, "19U",
                           PCLFonts.getPCLFontSelect (_indxFontArial,
                                                      PCLFonts.eVariant.Regular,
                                                      ptSize, 0));

            PCLWriter.patternSet (prnWriter,
                                  PCLWriter.ePatternType.UserDefined,
                                  _patternId);

            //----------------------------------------------------------------//
            
            posX = (Int16) (_posXPage_n_Data + _colInc - logXOffset);
            posY = _posYPage_n_Data;

            posX += srcOffsetX;
            posY += srcOffsetY;

            PCLWriter.cursorPosition (prnWriter,
                                      posX,
                                      posY);

            //----------------------------------------------------------------//

            for (Int32 i = 0; i < 8; i++)
            {
                PCLWriter.setROP (prnWriter, (Byte) (startROP + i));

                if (flagUseMacros)
                {
                    PCLWriter.macroControl (prnWriter, _macroIdSrcBoxRow,
                                            PCLWriter.eMacroControl.Call);
                }
                else
                    writeSrcBoxRow (prnWriter, idClrS1, idClrS2, idClrBlack,
                                    flagOptColour, false);

                posY += +_rowInc;

                PCLWriter.cursorPosition (prnWriter,
                                          posX,
                                          posY);
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
                                             UInt16       logXOffset,
                                             Int32        indxPalette,
                                             Byte         idClrD1,
                                             Byte         idClrD2,
                                             Byte         idClrS1,
                                             Byte         idClrS2,
                                             Byte         idClrBlack,
                                             Byte         idClrWhite,
                                             String       nameClrD1,
                                             String       nameClrD2,
                                             String       nameClrS1,
                                             String       nameClrS2,
                                             String       nameClrT1,
                                             String       nameClrT2,
                                             Int32        minROP,
                                             Int32        maxROP,  
                                             Boolean      flagOptColour,
                                             Boolean      flagUseMacros)
        {
            generatePage_1 (prnWriter,
                            logXOffset,
                            indxPalette,
                            (Byte) minROP,
                            (Byte) maxROP,
                            idClrD1,
                            idClrD2,
                            idClrS1,
                            idClrS2,
                            idClrBlack,
                            idClrWhite,
                            nameClrD1,
                            nameClrD2,
                            nameClrS1,
                            nameClrS2,
                            nameClrT1,
                            nameClrT2,
                            flagOptColour,
                            flagUseMacros);

            PCLWriter.formFeed (prnWriter);

            for (Int32 i = minROP; i < maxROP; i += 8)
            {
                generatePage_n (prnWriter,
                                logXOffset,
                                (Byte) i,
                                idClrD1,
                                idClrD2,
                                idClrS1,
                                idClrS2,
                                idClrBlack,
                                idClrWhite,
                                flagOptColour,
                                flagUseMacros);

                PCLWriter.formFeed (prnWriter);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e D e s t B o x                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write sequences (either directly, or as a macro definition) for    //
        // the 'destination' box.                                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void writeDestBox (BinaryWriter prnWriter,
                                          Byte         idClrD1,
                                          Byte         idClrD2,
                                          Byte         idClrBlack,
                                          Boolean      flagOptColour,
                                          Boolean      flagUseMacros)
        {
            const Int16 macroId = _macroIdDestBox;
            const Int16 halfBox = _destBoxSide / 2;

            if (flagUseMacros)
            {
                PCLWriter.macroControl (prnWriter, macroId,
                                        PCLWriter.eMacroControl.StartDef);
            }

            PCLWriter.setForegroundColour (prnWriter, idClrD1);

            PCLWriter.patternSet (prnWriter,
                                  PCLWriter.ePatternType.SolidBlack,
                                  -1);

            PCLWriter.cursorPushPop (prnWriter, PCLWriter.ePushPop.Push);

            PCLWriter.rectangleSolid (prnWriter,
                                      0,
                                      0,
                                      halfBox,
                                      halfBox,
                                      false,
                                      true,
                                      false);

            PCLWriter.cursorRelative (prnWriter,
                                      halfBox,
                                      halfBox);

            PCLWriter.rectangleSolid (prnWriter,
                                      0,
                                      0,
                                      halfBox,
                                      halfBox,
                                      false,
                                      true,
                                      false);

            if (flagOptColour)
            {
                PCLWriter.setForegroundColour (prnWriter, idClrD2);

                PCLWriter.cursorRelative (prnWriter,
                                          -halfBox,
                                          0);

                PCLWriter.rectangleSolid (prnWriter,
                                          0,
                                          0,
                                          halfBox,
                                          halfBox,
                                          false,
                                          true,
                                          false);

                PCLWriter.cursorRelative (prnWriter,
                                          halfBox,
                                          -halfBox);

                PCLWriter.rectangleSolid (prnWriter,
                                          0,
                                          0,
                                          halfBox,
                                          halfBox,
                                          false,
                                          true,
                                          false);
            }
            
            PCLWriter.setForegroundColour (prnWriter, idClrBlack);

            PCLWriter.cursorPushPop (prnWriter, PCLWriter.ePushPop.Pop);

            if (flagUseMacros)
            {
                PCLWriter.macroControl (prnWriter, macroId,
                                    PCLWriter.eMacroControl.StopDef);

                PCLWriter.macroControl (prnWriter, macroId,
                                        PCLWriter.eMacroControl.MakePermanent);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e D e s t B o x P a g e                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write sequences (either directly, or as a macro definition) for    //
        // page of 8 rows of the 4 'destination' boxes, plus column headings. //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void writeDestBoxPage (BinaryWriter prnWriter,
                                              UInt16       logXOffset,
                                              Byte         idClrD1,
                                              Byte         idClrD2,
                                              Byte         idClrBlack,
                                              Boolean      flagOptColour,
                                              Boolean      flagUseMacros)
        {
            const Int16 macroId = _macroIdDestBoxPage;

            Int16 posX,
                  posY;

            if (flagUseMacros)
            {
                PCLWriter.macroControl (prnWriter, macroId,
                                        PCLWriter.eMacroControl.StartDef);
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Headers.                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            posX = (Int16) (_posXPage_n_Hddr - logXOffset);
            posY = _posYPage_n_Hddr;

            PCLWriter.cursorPosition (prnWriter,
                                      posX,
                                      posY);

            if (flagUseMacros)
                PCLWriter.macroControl (prnWriter, _macroIdDestBoxRowHddr,
                                        PCLWriter.eMacroControl.Call);
            else
                writeDestBoxRowHddr (prnWriter, false);

            //----------------------------------------------------------------//
            //                                                                //
            // Rows of destination boxes.                                     //
            //                                                                //
            //----------------------------------------------------------------//

            posX = (Int16) (_posXPage_n_Data + _colInc - logXOffset);
            posY = _posYPage_n_Data;

            PCLWriter.cursorPosition (prnWriter,
                                      posX,
                                      posY);

            for (Int32 i = 0; i < 8; i++)
            {
                if (flagUseMacros)
                    PCLWriter.macroControl (prnWriter, _macroIdDestBoxRow,
                                            PCLWriter.eMacroControl.Call);
                else
                    writeDestBoxRow (prnWriter, idClrD1, idClrD2, idClrBlack,
                                     flagOptColour, false);

                PCLWriter.cursorRelative (prnWriter,
                                          0,
                                          _rowInc);
            }

            if (flagUseMacros)
            {
                PCLWriter.macroControl (prnWriter, macroId,
                                    PCLWriter.eMacroControl.StopDef);

                PCLWriter.macroControl (prnWriter, macroId,
                                        PCLWriter.eMacroControl.MakePermanent);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e D e s t B o x R o w                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write sequences (either directly, or as a macro definition) for    //
        // row of 4 'destination' boxes.                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void writeDestBoxRow (BinaryWriter prnWriter,
                                             Byte         idClrD1,
                                             Byte         idClrD2,
                                             Byte         idClrBlack,
                                             Boolean      flagOptColour,
                                             Boolean      flagUseMacros)
        {
            const Int16 macroId = _macroIdDestBoxRow;

            if (flagUseMacros)
            {
                PCLWriter.macroControl (prnWriter, macroId,
                                    PCLWriter.eMacroControl.StartDef);
            }

            PCLWriter.cursorPushPop (prnWriter, PCLWriter.ePushPop.Push);

            if (flagUseMacros)
                PCLWriter.macroControl (prnWriter, _macroIdDestBox,     // box 1
                                        PCLWriter.eMacroControl.Call);
            else
                writeDestBox (prnWriter, idClrD1, idClrD2, idClrBlack,
                              flagOptColour, false);

            PCLWriter.cursorRelative (prnWriter,
                                      _colInc,
                                      0);

            if (flagUseMacros)
                PCLWriter.macroControl (prnWriter, _macroIdDestBox,     // box 2
                                        PCLWriter.eMacroControl.Call);
            else
                writeDestBox (prnWriter, idClrD1, idClrD2, idClrBlack,
                              flagOptColour, false);

            PCLWriter.cursorRelative (prnWriter,
                                      _colInc,
                                      0);

            if (flagUseMacros)
                PCLWriter.macroControl (prnWriter, _macroIdDestBox,     // box 3
                                        PCLWriter.eMacroControl.Call);
            else
                writeDestBox (prnWriter, idClrD1, idClrD2, idClrBlack,
                              flagOptColour, false);

            PCLWriter.cursorRelative (prnWriter,
                                      _colInc,
                                      0);

            if (flagUseMacros)
                PCLWriter.macroControl (prnWriter, _macroIdDestBox,     // box 4
                                        PCLWriter.eMacroControl.Call);
            else
                writeDestBox (prnWriter, idClrD1, idClrD2, idClrBlack,
                              flagOptColour, false);

            PCLWriter.cursorPushPop (prnWriter, PCLWriter.ePushPop.Pop);

            if (flagUseMacros)
            {
                PCLWriter.macroControl (prnWriter, macroId,
                                        PCLWriter.eMacroControl.StopDef);

                PCLWriter.macroControl (prnWriter, macroId,
                                        PCLWriter.eMacroControl.MakePermanent);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e D e s t B o x R o w H d d r                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write sequences (either directly, or as a macro definition) for    //
        // column headers for samples.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void writeDestBoxRowHddr (BinaryWriter prnWriter,
                                                 Boolean      flagUseMacros)
        {
            const Int16 macroId = _macroIdDestBoxRowHddr;

            Int16 ptSize;

            if (flagUseMacros)
            {
                PCLWriter.macroControl (prnWriter, macroId,
                                        PCLWriter.eMacroControl.StartDef);
            }

            ptSize = 10;

            PCLWriter.font (prnWriter, true, "19U",
                           PCLFonts.getPCLFontSelect (_indxFontArial,
                                                      PCLFonts.eVariant.Regular,
                                                      ptSize, 0));

            //----------------------------------------------------------------//

            PCLWriter.cursorPushPop (prnWriter, PCLWriter.ePushPop.Push);

            prnWriter.Write ("ROP".ToCharArray ());

            PCLWriter.cursorPushPop (prnWriter, PCLWriter.ePushPop.Pop);

            PCLWriter.cursorRelative (prnWriter, _colInc, 0);

            PCLWriter.cursorPushPop (prnWriter, PCLWriter.ePushPop.Push);

            prnWriter.Write ("Source = transparent".ToCharArray ());

            PCLWriter.cursorPushPop (prnWriter, PCLWriter.ePushPop.Pop);
            PCLWriter.cursorPushPop (prnWriter, PCLWriter.ePushPop.Push);

            PCLWriter.cursorRelative (prnWriter, (_colInc * 2), 0);

            prnWriter.Write ("Source = opaque".ToCharArray ());

            PCLWriter.cursorPushPop (prnWriter, PCLWriter.ePushPop.Pop);

            PCLWriter.cursorRelative (prnWriter, 0, (_incInch / 6));

            ptSize = 8;

            PCLWriter.font (prnWriter, true, "19U",
                           PCLFonts.getPCLFontSelect (_indxFontArial,
                                                      PCLFonts.eVariant.Regular,
                                                      ptSize, 0));

            for (Int32 i = 0; i < 2; i++)
            {
                PCLWriter.cursorPushPop (prnWriter, PCLWriter.ePushPop.Push);

                prnWriter.Write ("Pattern=transparent".ToCharArray ());

                PCLWriter.cursorPushPop (prnWriter, PCLWriter.ePushPop.Pop);

                PCLWriter.cursorRelative (prnWriter, _colInc, 0);

                PCLWriter.cursorPushPop (prnWriter, PCLWriter.ePushPop.Push);

                prnWriter.Write ("Pattern=opaque".ToCharArray ());

                PCLWriter.cursorPushPop (prnWriter, PCLWriter.ePushPop.Pop);

                PCLWriter.cursorRelative (prnWriter, _colInc, 0);
            }

            if (flagUseMacros)
            {
                PCLWriter.macroControl (prnWriter, macroId,
                                        PCLWriter.eMacroControl.StopDef);

                PCLWriter.macroControl (prnWriter, macroId,
                                        PCLWriter.eMacroControl.MakePermanent);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e P a t t e r n                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Define user-defined sample pattern.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void writePattern (BinaryWriter prnWriter,
                                          Int16 patternId,
                                          Byte idClrT1,
                                          Byte idClrT2,
                                          Boolean flagOptColour)
        {
            const UInt16 patWidth = 16; // multiple of 8
            const UInt16 patHeight = 16; // multiple of 8

            Byte patWidthMS = (Byte) ((patWidth >> 8) & 0xff);
            Byte patWidthLS = (Byte) (patWidth & 0xff);
            Byte patHeightMS = (Byte) ((patHeight >> 8) & 0xff);
            Byte patHeightLS = (Byte) (patHeight & 0xff);

            Byte [] patternBase = { 0xC0, 0x01,      // row 00
                                    0xE0, 0x00,      //     01
                                    0x70, 0x00,      //     02
                                    0x38, 0x00,      //     03
                                    0x1C, 0x00,      //     04
                                    0x0E, 0x00,      //     05
                                    0x07, 0x00,      //     06
                                    0x03, 0x80,      //     07
                                    0x01, 0xC0,      //     08
                                    0x00, 0xE0,      //     09
                                    0x00, 0x70,      //     10
                                    0x00, 0x38,      //     11
                                    0x00, 0x1C,      //     12
                                    0x00, 0x0E,      //     13
                                    0x00, 0x07,      //     14
                                    0x80, 0x03 };    //     15

            if (flagOptColour)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Set up format 01 colour pattern, using 8 bits-per-pixel    //
                // elements to index into the current palette (which, using   //
                // simple colour mode, is limited to 8 entries).              //
                //                                                            //
                // Note that there is not a resolution-specified equivalent   //
                // of this format, so the pattern will have the (default)     //
                // resolution of 300 dpi.                                     //
                //                                                            //
                //------------------------------------------------------------//

                const Byte format = 1;
                const Byte bitsPerPixel = 8;

                Int32 patSize = (patWidth * patHeight * bitsPerPixel) / 8;

                Int32 rowBytes = patWidth / 8;

                Int32 rowVal;

                UInt32 mask;

                Int32 indexIp,
                      indexOp;

                Byte [] hddrFmt_01 = { format,
                                      0x00,
                                      bitsPerPixel,
                                      0x00,
                                      patHeightMS,
                                      patHeightLS,
                                      patWidthMS,
                                      patWidthLS };

                Byte [] pattern = new Byte [patSize];

                for (Int32 i = 0; i < patHeight; i++)
                {
                    mask = 0x01 << (patWidth - 1);

                    indexIp = i * rowBytes;
                    indexOp = i * patWidth;

                    rowVal = 0;

                    for (Int32 k = 0; k < rowBytes; k++)
                    {
                        rowVal = (rowVal * 256) +
                                 patternBase [indexIp + k];
                    }

                    for (Int32 j = 0; j < patWidth; j++)
                    {
                        if ((rowVal & mask) != 0)
                            pattern [indexOp + j] = idClrT1;
                        else
                            pattern [indexOp + j] = idClrT2;

                        mask = mask >> 1;
                    }
                }

                PCLWriter.patternDefine (prnWriter, patternId,
                                         hddrFmt_01, pattern);
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // Set up format 20 (resolution-specified) monochrome         //
                // pattern.                                                   //
                // But use 300 dpi resolution, to match the colour one (which //
                // always uses the default resolution, similar to format 0).  //
                //                                                            //
                //------------------------------------------------------------//

                const Int16 rasterRes = 300;

                const Byte format = 20;
                const Byte bitsPerPixel = 1;

                Byte rasterResMS = (Byte) ((rasterRes >> 8) & 0xff);
                Byte rasterResLS = (Byte) (rasterRes & 0xff);

                Byte [] hddrFmt_20 = { format,
                                      0x00,
                                      bitsPerPixel,
                                      0x00,
                                      patHeightMS,
                                      patHeightLS,
                                      patWidthMS,
                                      patWidthLS,
                                      rasterResMS,
                                      rasterResLS,
                                      rasterResMS,
                                      rasterResLS };

                PCLWriter.patternDefine (prnWriter, patternId,
                                         hddrFmt_20, patternBase);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e S r c B o x                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write sequences (either directly, or as a macro definition) for    //
        // source image.                                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void writeSrcBox (BinaryWriter prnWriter,
                                         Byte         idClrS1,
                                         Byte         idClrS2,
                                         Byte         idClrBlack, 
                                         Boolean      flagOptColour,
                                         Boolean      flagUseMacros)
        {
            const Int16 macroId = _macroIdSrcBox;

            if (flagUseMacros)
            {
                PCLWriter.macroControl (prnWriter, macroId,
                                        PCLWriter.eMacroControl.StartDef);
            }

            if (flagUseMacros)
            {
                PCLWriter.macroControl (prnWriter, _macroIdSrcBoxText,
                                        PCLWriter.eMacroControl.Call);

                PCLWriter.cursorPushPop (prnWriter, PCLWriter.ePushPop.Push);

                PCLWriter.macroControl (prnWriter, _macroIdSrcBoxRasterPos,
                                        PCLWriter.eMacroControl.Call);

                PCLWriter.cursorPushPop (prnWriter, PCLWriter.ePushPop.Pop);
                PCLWriter.cursorPushPop (prnWriter, PCLWriter.ePushPop.Push);

                PCLWriter.cursorRelative (prnWriter, _destBoxSide / 2, 0);

                PCLWriter.macroControl (prnWriter, _macroIdSrcBoxRasterNeg,
                                        PCLWriter.eMacroControl.Call);

                PCLWriter.cursorPushPop (prnWriter, PCLWriter.ePushPop.Pop);
            }
            else
            {
                writeSrcBoxText (prnWriter, idClrS1, idClrS2, idClrBlack,
                                 flagOptColour, false);

                PCLWriter.cursorPushPop (prnWriter, PCLWriter.ePushPop.Push);

                writeSrcBoxRaster (prnWriter, idClrS1, idClrS2,
                                   false, flagOptColour, false);

                PCLWriter.cursorPushPop (prnWriter, PCLWriter.ePushPop.Pop);
                PCLWriter.cursorPushPop (prnWriter, PCLWriter.ePushPop.Push);

                PCLWriter.cursorRelative (prnWriter, _destBoxSide / 2, 0);

                writeSrcBoxRaster (prnWriter, idClrS1, idClrS2,
                                   true, flagOptColour, false);

                PCLWriter.cursorPushPop (prnWriter, PCLWriter.ePushPop.Pop);
            }

            if (flagUseMacros)
            {
                PCLWriter.macroControl (prnWriter, macroId,
                                        PCLWriter.eMacroControl.StopDef);

                PCLWriter.macroControl (prnWriter, macroId,
                                        PCLWriter.eMacroControl.MakePermanent);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e S r c B o x R a s t e r                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write sequences (either directly, or as a macro definition) for    //
        // 'source' raster image.                                             //
        //                                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void writeSrcBoxRaster (BinaryWriter prnWriter,
                                               Byte         idClrS1,
                                               Byte         idClrS2,
                                               Boolean      inverse,
                                               Boolean      flagOptColour,
                                               Boolean      flagUseMacros)
        {
            const Int16 macroIdPos = _macroIdSrcBoxRasterPos;
            const Int16 macroIdNeg = _macroIdSrcBoxRasterNeg;

            const Int16 blockCt = 7;    // A + B + C + D + C + B + A

            const Int16 compressModeRLE      = 1;
            const Int16 compressModeDeltaRow = 3;
            const Int16 compressModeAdaptive = 5;

            const Int16 rowCtA = 16;
            const Int16 rowCtB = 16;
            const Int16 rowCtC = 32;
            const Int16 rowCtD = 64;

            Byte[] maskRowAPos = { 0x03, 0xff,
                                   0x03, 0x00,
                                   0x07, 0xff,
                                   0x03, 0x00,
                                   0x03, 0xff };
            Byte[] maskRowANeg = { 0x03, 0x00,
                                   0x03, 0xff,
                                   0x07, 0x00,
                                   0x03, 0xff,
                                   0x03, 0x00 };

            Byte[] maskRowBPos = { 0x01, 0xff,
                                   0x13, 0x00,
                                   0x01, 0xff };
            Byte[] maskRowBNeg = { 0x01, 0x00,
                                   0x13, 0xff,
                                   0x01, 0x00 };
 
            Byte[] maskRowCPos = { 0x03, 0x00,
                                   0x03, 0xff,
                                   0x07, 0x00,
                                   0x03, 0xff,
                                   0x03, 0x00 };
            Byte[] maskRowCNeg = { 0x03, 0xff,
                                   0x03, 0x00,
                                   0x07, 0xff,
                                   0x03, 0x00,
                                   0x03, 0xff };

            Byte[] maskRowDPos = { 0x01, 0xff,
                                   0x05, 0x00,
                                   0x07, 0xff,
                                   0x05, 0x00,
                                   0x01, 0xff };
            Byte[] maskRowDNeg = { 0x01, 0x00,
                                   0x05, 0xff,
                                   0x07, 0x00,
                                   0x05, 0xff,
                                   0x01, 0x00 };

            Byte[] maskRowCrnt;

            Int32 maskLen;

            Int16 rowCtCrnt;
            Int16 macroId;

            Int32 blockSize,
                  rowSize;

            //----------------------------------------------------------------//

            if (inverse)
                macroId = macroIdNeg;
            else
                macroId = macroIdPos;

            if (flagUseMacros)
            {
                PCLWriter.macroControl (prnWriter, macroId,
                                        PCLWriter.eMacroControl.StartDef);
            }

            if (flagOptColour)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Colour mode is Simple Colour mode; this uses a predefined  //
                // palette consisting of eight colours.                       //
                //                                                            //   
                // When using this mode, the pixel encoding mode is always    //
                // Indexed Planar.                                            //
                // A palette of eight values implies three planes (since      //
                // 8 = 2 to power 3).                                         //
                //                                                            //
                // This image uses a mix of compression modes:                //
                //      1 - Run-Length encoded                                //
                //      3 - delta row; the latter is used to repeat rows.     //
                //                                                            //
                //------------------------------------------------------------//
                
                const Int32 planeCt = 3;

                Int32 indxClr1,
                      indxClr2;

                PCLWriter.rasterBegin (prnWriter,
                                       _sourceImagePixelsWidth,
                                       _sourceImagePixelsHeight,
                                       compressModeRLE);

                //------------------------------------------------------------//

                for (Int32 blockNo = 0; blockNo < blockCt; blockNo++)
                {
                    if ((blockNo == 0) || (blockNo == 6))
                    {
                        rowCtCrnt = rowCtA;

                        if (inverse)
                            maskRowCrnt = maskRowANeg;
                        else
                            maskRowCrnt = maskRowAPos;
                    }
                    else if ((blockNo == 1) || (blockNo == 5))
                    {
                        rowCtCrnt = rowCtB;

                        if (inverse)
                            maskRowCrnt = maskRowBNeg;
                        else
                            maskRowCrnt = maskRowBPos;
                    }
                    else if ((blockNo == 2) || (blockNo == 4))
                    {
                        rowCtCrnt = rowCtC;

                        if (inverse)
                            maskRowCrnt = maskRowCNeg;
                        else
                            maskRowCrnt = maskRowCPos;
                    }
                    else
                    {
                        rowCtCrnt = rowCtD;

                        if (inverse)
                            maskRowCrnt = maskRowDNeg;
                        else
                            maskRowCrnt = maskRowDPos;
                    }

                    indxClr1 = idClrS1;
                    indxClr2 = idClrS2;

                    maskLen = maskRowCrnt.Length;

                    Byte [] opRow = new Byte [maskLen];

                    for (Int32 plane = 0; plane < planeCt; plane++)
                    {
                        for (Int32 j = 0; j < maskLen; j++)
                        {
                            if ((j & 1) == 0)
                            {
                                // odd bytes are RLE repeat count bytes
                                opRow[j] = maskRowCrnt[j];
                            }
                            else
                            {
                                // even bytes are the RLE bit pattern bytes
                                Int32 opByte = 0;

                                Int32 ipByte = maskRowCrnt[j];

                                for (Int32 k = 0; k < 8; k++)
                                {
                                    if (k != 0)
                                    {
                                        ipByte = ipByte << 1;
                                        opByte = opByte << 1;
                                    }

                                    if ((ipByte & 0x80) != 0)
                                    {
                                        if ((indxClr1 & 0x01) != 0)
                                            opByte += 1;
                                    }
                                    else
                                    {
                                        if ((indxClr2 & 0x01) != 0)
                                            opByte += 1;
                                    }
                                }

                                opRow[j] = (Byte) opByte;
                            }
                        }

                        if (plane < (planeCt -1))
                            PCLWriter.rasterTransferPlane (prnWriter,
                                                           maskLen,
                                                           opRow);
                        else
                            PCLWriter.rasterTransferRow (prnWriter,
                                                         maskLen,
                                                         opRow);

                        indxClr1 = indxClr1 >> 1;   // next plane of colour
                        indxClr2 = indxClr2 >> 1;   // next plane of colour
                    }

                    PCLWriter.rasterCompressionMode (prnWriter,
                                                     compressModeDeltaRow);

                    for (Int32 j = 1; j < rowCtCrnt; j++)
                    {
                        PCLWriter.rasterTransferRow (prnWriter, 0, null);
                    }

                    if (blockNo < 6)
                    {
                        PCLWriter.rasterCompressionMode (prnWriter,
                                                         compressModeRLE);
                    }
                }

                //------------------------------------------------------------//

                PCLWriter.rasterEnd (prnWriter);
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // Monochrome mode is a simple colour mode which uses a       //
                // predefined palette consisting of two entries (black and    //
                // white).                                                    //
                //                                                            //
                // This image uses compression mode 5 ("adaptive              //
                // compression").                                             //
                // Elements use TLLD format:                                  //
                //  Type 01 - data uses Run-length encoding method            //
                //       05 - repeat count; data is null                      //
                //                                                            //
                // This compression mode cannot be used with 'planar'         //
                // pixel-encoding modes - note that Simple Colour mode sets   //
                // 'indexed planar'.                                          //
                //                                                            //
                // Monochrome mode is actually a simple colour mode, and DOES //
                // use indexed planar pixel encoding - but as there is only   //
                // one plane, it works OK.                                    //
                //                                                            //
                //------------------------------------------------------------//

                const Int32 sizeTLL = 3;

                PCLWriter.rasterBegin (prnWriter,
                                       _sourceImagePixelsWidth,
                                       _sourceImagePixelsHeight,
                                       compressModeAdaptive);

                for (Int32 blockNo = 0; blockNo < blockCt; blockNo++)
                {
                    Byte[] block;

                    Int32 offset;

                    if ((blockNo == 0) || (blockNo == 6))
                    {
                        rowCtCrnt = rowCtA;

                        if (inverse)
                            maskRowCrnt = maskRowANeg;
                        else
                            maskRowCrnt = maskRowAPos;
                    }
                    else if ((blockNo == 1) || (blockNo == 5))
                    {
                        rowCtCrnt = rowCtB;

                        if (inverse)
                            maskRowCrnt = maskRowBNeg;
                        else
                            maskRowCrnt = maskRowBPos;
                    }
                    else if ((blockNo == 2) || (blockNo == 4))
                    {
                        rowCtCrnt = rowCtC;

                        if (inverse)
                            maskRowCrnt = maskRowCNeg;
                        else
                            maskRowCrnt = maskRowCPos;
                    }
                    else
                    {
                        rowCtCrnt = rowCtD;

                        if (inverse)
                            maskRowCrnt = maskRowDNeg;
                        else
                            maskRowCrnt = maskRowDPos;
                    }

                    rowSize = maskRowCrnt.Length;

                    blockSize = sizeTLL + rowSize + sizeTLL;

                    block = new Byte[blockSize];

                    block[0] = 0x01;        // type 01 - RLE
                    block[1] = 0x00;        // length - assume < 256;
                    block[2] = (Byte) rowSize;

                    for (Int32 byteNo = 0; byteNo < rowSize; byteNo++)
                    {
                        block[3 + byteNo] = maskRowCrnt[byteNo];
                    }

                    offset = rowSize + 3;

                    block[offset]     = 0x05;   // type 05 - repeat
                    block[offset + 1] = 0x00;   // repeat count - assume < 256
                    block[offset + 2] = (Byte) (rowCtCrnt - 1);

                    PCLWriter.rasterTransferRow(prnWriter,
                                                 blockSize,
                                                 block); 
                }

                PCLWriter.rasterEnd (prnWriter);
            }

            if (flagUseMacros)
            {
                PCLWriter.macroControl (prnWriter, macroId,
                                        PCLWriter.eMacroControl.StopDef);

                PCLWriter.macroControl (prnWriter, macroId,
                                        PCLWriter.eMacroControl.MakePermanent);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e S r c B o x R o w                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write sequences (either directly, or as a macro definition) for    //
        // source image.                                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void writeSrcBoxRow (BinaryWriter prnWriter,
                                            Byte         idClrS1,
                                            Byte         idClrS2,
                                            Byte         idClrBlack,
                                            Boolean      flagOptColour,
                                            Boolean      flagUseMacros)
        {
            const Int16 macroId = _macroIdSrcBoxRow;

            if (flagUseMacros)
            {
                PCLWriter.macroControl (prnWriter, macroId,
                                        PCLWriter.eMacroControl.StartDef);
            }

            PCLWriter.sourceTransparency  (prnWriter, false);
            PCLWriter.patternTransparency (prnWriter, false);

            if (flagUseMacros)
                PCLWriter.macroControl (prnWriter, _macroIdSrcBox,
                                        PCLWriter.eMacroControl.Call);
            else
                writeSrcBox (prnWriter, idClrS1, idClrS2, idClrBlack,
                             flagOptColour, false);

            //----------------------------------------------------------------//

            PCLWriter.cursorRelative (prnWriter,
                                      _colInc,
                                      0);

            PCLWriter.sourceTransparency  (prnWriter, false);
            PCLWriter.patternTransparency (prnWriter, true);

            if (flagUseMacros)
               PCLWriter.macroControl (prnWriter, _macroIdSrcBox,
                                        PCLWriter.eMacroControl.Call);
            else
                writeSrcBox (prnWriter, idClrS1, idClrS2, idClrBlack,
                             flagOptColour, false);

            //----------------------------------------------------------------//

            PCLWriter.cursorRelative (prnWriter,
                                      _colInc,
                                      0);

            PCLWriter.sourceTransparency  (prnWriter, true);
            PCLWriter.patternTransparency (prnWriter, false);

            if (flagUseMacros)
                PCLWriter.macroControl (prnWriter, _macroIdSrcBox,
                                        PCLWriter.eMacroControl.Call);
            else
                writeSrcBox (prnWriter, idClrS1, idClrS2, idClrBlack,
                             flagOptColour, false);

            //----------------------------------------------------------------//

            PCLWriter.cursorRelative (prnWriter,
                                      _colInc,
                                      0);

            PCLWriter.sourceTransparency  (prnWriter, true);
            PCLWriter.patternTransparency (prnWriter, true);

            if (flagUseMacros)
                PCLWriter.macroControl (prnWriter, _macroIdSrcBox,
                                        PCLWriter.eMacroControl.Call);
            else
                writeSrcBox (prnWriter, idClrS1, idClrS2, idClrBlack,
                             flagOptColour, false);

            //----------------------------------------------------------------//

            if (flagUseMacros)
            {
                PCLWriter.macroControl (prnWriter, macroId,
                                        PCLWriter.eMacroControl.StopDef);

                PCLWriter.macroControl (prnWriter, macroId,
                                        PCLWriter.eMacroControl.MakePermanent);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e S r c B o x T e x t                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write sequences (either directly, or as a macro definition) for    //
        // sample, consisting of multiple copies of 'source' text characters. //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void writeSrcBoxText (BinaryWriter prnWriter,
                                             Byte         idClrS1,
                                             Byte         idClrS2,
                                             Byte         idClrBlack,
                                             Boolean      flagOptColour,
                                             Boolean      flagUseMacros)
        {
            const Int16 macroId = _macroIdSrcBoxText;

            if (flagUseMacros)
            {
                PCLWriter.macroControl (prnWriter, macroId,
                                        PCLWriter.eMacroControl.StartDef);
            }

            PCLWriter.cursorPushPop (prnWriter, PCLWriter.ePushPop.Push);

            PCLWriter.cursorRelative (prnWriter, _destBoxSide / 4, 0);

            PCLWriter.cursorPushPop (prnWriter, PCLWriter.ePushPop.Push);

            PCLWriter.setForegroundColour (prnWriter, idClrS1);

            prnWriter.Write ("O".ToCharArray ());

            PCLWriter.cursorPushPop (prnWriter, PCLWriter.ePushPop.Pop);

            PCLWriter.cursorRelative (prnWriter, 0, (_destBoxSide * 5) / 8);

            PCLWriter.setForegroundColour (prnWriter, idClrS2);

            prnWriter.Write ("O".ToCharArray ());

            PCLWriter.setForegroundColour (prnWriter, idClrBlack);

            PCLWriter.cursorPushPop (prnWriter, PCLWriter.ePushPop.Pop);

            if (flagUseMacros)
            {
                PCLWriter.macroControl (prnWriter, macroId,
                                        PCLWriter.eMacroControl.StopDef);

                PCLWriter.macroControl (prnWriter, macroId,
                                        PCLWriter.eMacroControl.MakePermanent);
            }
        }
    }
}
