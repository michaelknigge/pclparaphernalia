using System;
using System.IO;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides PCL XL support for the FontSample tool.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class ToolFontSamplePCLXL
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const String _formName          = "FontSampleForm";
        const String _hexChars          = "0123456789ABCDEF";

        const Int32 _symSet_19U         = 629;
        const Int16 _gridDim            = 16;
        const Int32 _gridDimHalf        = _gridDim / 2;
        const Int16 _gridCols           = _gridDim;
        const Int16 _gridRows           = _gridDim;
        const UInt16 _unitsPerInch      = PCLXLWriter._sessionUPI;

        const Int16 _lineSpacing        = _unitsPerInch / 4;
        const Int16 _cellWidth          = (_unitsPerInch * 1) / 3;
        const Int16 _cellHeight         = (_unitsPerInch * 25) / 60;

        const Int16 _marginX            = (_unitsPerInch * 7) / 6;
        const Int16 _posYDesc           = (_unitsPerInch * 3) / 4;
        const Int16 _posYGrid           = _posYDesc + (_lineSpacing * 4);
        const Int16 _posYSelData        = _posYGrid +
                                          (_cellHeight * (_gridRows + 2)) +
                                          (_lineSpacing * 2);

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
                                       Boolean      formAsMacro,
                                       Boolean      showC0Chars,
                                       Boolean      optGridVertical,
                                       String       fontId,
                                       String       fontDesc,
                                       UInt16       symbolSet,
                                       String       fontName,
                                       String       symbolSetName,
                                       UInt16 []    sampleRangeOffsets,
                                       Double       pointSize,
                                       Boolean      downloadFont,
                                       Boolean      downloadFontRemove,
                                       String       fontFilename,
                                       Boolean      symSetUserSet,
                                       Boolean      showMapCodesUCS2,
                                       Boolean      showMapCodesUTF8,
                                       String       symSetUserFile)
        {
            Int32 pageCt = sampleRangeOffsets.Length;

            UInt16 symSetUserMapMax = 0;

            UInt16[] symSetUserMap = null;

            //----------------------------------------------------------------//

            generateJobHeader(prnWriter);

            if (formAsMacro)
                generateOverlay(prnWriter, true, optGridVertical);

            if (downloadFont)
            {
                PCLXLDownloadFont.fontFileCopy (prnWriter, fontFilename);
            }

            //----------------------------------------------------------------//

            if (symSetUserSet)
            {
                symSetUserMapMax = PCLSymbolSets.getMapArrayMax(
                          PCLSymbolSets.IndexUserSet);

                symSetUserMap = new UInt16[symSetUserMapMax + 1];

                symSetUserMap = PCLSymbolSets.getMapArrayUserSet();
            }

            //----------------------------------------------------------------//

            for (Int32 i = 0; i < pageCt; i++)
            {
                UInt16 rangeOffset = sampleRangeOffsets[i];

                generatePage(prnWriter,
                         indxPaperSize,
                         indxPaperType,
                         indxOrientation,
                         formAsMacro,
                         showC0Chars,
                         optGridVertical,
                         fontId,
                         fontDesc,
                         symbolSet,
                         fontName,
                         symbolSetName,
                         rangeOffset,
                         pointSize,
                         downloadFont,
                         fontFilename,
                         symSetUserSet,
                         showMapCodesUCS2,
                         showMapCodesUTF8,
                         symSetUserFile,
                         symSetUserMapMax,
                         symSetUserMap);
            }

            generateJobTrailer(prnWriter, formAsMacro,
                               downloadFont, downloadFontRemove, fontName);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e J o b H e a d e r                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write stream initialisation sequences to output file.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void generateJobHeader(BinaryWriter prnWriter)
        {
            PCLXLWriter.stdJobHeader(prnWriter, "");
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e J o b T r a i l e r                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write job termination sequences to output file.                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void generateJobTrailer(BinaryWriter prnWriter,
                                               Boolean      formAsMacro,
                                               Boolean      downloadFont,
                                               Boolean      downloadFontRemove,
                                               String       fontName)
        {
            if ((downloadFont) && (downloadFontRemove))
            {
                PCLXLWriter.fontRemove(prnWriter, false, fontName); 
            }

            PCLXLWriter.stdJobTrailer(prnWriter, formAsMacro, _formName);
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
                                            Boolean      optGridVertical)
        {
            const Int16 twoCellWidth    = _cellWidth  * 2;
            const Int16 twoCellHeight   = _cellHeight * 2;

            const Int16 gridWidthInner  = _cellWidth  * _gridCols;
            const Int16 gridHeightInner = _cellHeight * _gridRows;
            const Int16 gridWidthOuter  = _cellWidth  * (_gridCols + 2);
            const Int16 gridHeightOuter = _cellHeight * (_gridRows + 2);

            const Int32 lenBuf     = 1024;
            const Int16 patternID  = 1;
            const UInt16 patWidth  = 24;
            const UInt16 patHeight = 24;

            Byte[] buffer = new Byte[lenBuf];

            Byte[] pattern_1 = { 0x00, 0x0c, 0x00, 0x00, 0x00, 0x00,
                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                 0x00, 0xc0, 0xc0, 0x00, 0x00, 0x00,
                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                 0x0c, 0x00, 0x0c, 0x00, 0x00, 0x00,
                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                 0xc0, 0x00, 0x00, 0x00, 0x00, 0x00,
                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                 0x0c, 0x00, 0x0c, 0x00, 0x00, 0x00,
                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                 0x00, 0xc0, 0xc0, 0x00, 0x00, 0x00,
                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            Int32 indBuf;

            Int16 crntPtSize;

            Int16 posX1,
                  posX2,
                  posY1,
                  posY2;

            indBuf = 0;

            //----------------------------------------------------------------//
            //                                                                //
            // Header                                                         //
            //                                                                //
            //----------------------------------------------------------------//

            if (formAsMacro)
            {
                PCLXLWriter.streamHeader(prnWriter, true, _formName);
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Colour space, pen & brush definitions.                         //
            //                                                                //
            //----------------------------------------------------------------//

            PCLXLWriter.addAttrUbyte(ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.ColorSpace,
                                     (Byte)PCLXLAttrEnums.eVal.eGray);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.SetColorSpace);

            PCLXLWriter.addAttrUbyte(ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.NullBrush,
                                     0);
            
            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.SetBrushSource);

            PCLXLWriter.addAttrUbyte(ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.GrayLevel,
                                     0);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.SetPenSource);

            PCLXLWriter.addAttrUbyte(ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.PenWidth,
                                     8);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.SetPenWidth);

            PCLXLWriter.writeStreamBlock(prnWriter, formAsMacro,
                                         buffer, ref indBuf);

            //----------------------------------------------------------------//
            //                                                                //
            // Block rectangle 1 (includes row headers).                      //
            //                                                                //
            //----------------------------------------------------------------//

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.NewPath);

            PCLXLWriter.addAttrSint16XY(ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.Point,
                                        _marginX, _posYGrid);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.SetCursor);

            PCLXLWriter.addAttrSint16XY(ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.Point,
                                        0, _cellHeight);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.SetCursorRel);

            PCLXLWriter.addAttrSint16XY(ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.EndPoint,
                                        gridWidthOuter, 0);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.LineRelPath);

            PCLXLWriter.addAttrSint16XY(ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.EndPoint,
                                        0, gridHeightInner);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.LineRelPath);

            PCLXLWriter.addAttrSint16XY(ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.EndPoint,
                                        -gridWidthOuter, 0);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.LineRelPath);

            PCLXLWriter.addAttrSint16XY(ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.EndPoint,
                                        0, -gridHeightInner);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.LineRelPath);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.PaintPath);

            PCLXLWriter.writeStreamBlock(prnWriter, formAsMacro,
                                         buffer, ref indBuf);

            //----------------------------------------------------------------//
            //                                                                //
            // Block rectangle 2 (includes column headers).                   //
            //                                                                //
            //----------------------------------------------------------------//

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.NewPath);

            PCLXLWriter.addAttrSint16XY(ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.Point,
                                        _marginX, _posYGrid);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.SetCursor);

            PCLXLWriter.addAttrSint16XY(ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.Point,
                                        _cellWidth, 0);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.SetCursorRel);

            PCLXLWriter.addAttrSint16XY(ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.EndPoint,
                                        gridWidthInner, 0);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.LineRelPath);

            PCLXLWriter.addAttrSint16XY(ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.EndPoint,
                                        0, gridHeightOuter);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.LineRelPath);

            PCLXLWriter.addAttrSint16XY(ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.EndPoint,
                                        -gridWidthInner, 0);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.LineRelPath);

            PCLXLWriter.addAttrSint16XY(ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.EndPoint,
                                        0, -gridHeightOuter);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.LineRelPath);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.PaintPath);

            PCLXLWriter.writeStreamBlock(prnWriter, formAsMacro,
                                         buffer, ref indBuf);

            //----------------------------------------------------------------//
            //                                                                //
            // Top-left and bottom-right corners.                             //
            //                                                                //
            //----------------------------------------------------------------//

            PCLXLWriter.addAttrUbyte(ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.GrayLevel,
                                     224);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.SetBrushSource);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.NewPath);

            PCLXLWriter.addAttrSint16Box(ref buffer,
                                         ref indBuf,
                                         PCLXLAttributes.eTag.BoundingBox,
                                         _marginX,
                                         _posYGrid,
                                         _marginX  + twoCellWidth,
                                         _posYGrid + twoCellHeight);
            
            PCLXLWriter.addAttrSint16XY(ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.StartPoint,
                                        _marginX + _cellWidth,
                                        _posYGrid);
            
            PCLXLWriter.addAttrSint16XY(ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.EndPoint,
                                        _marginX,
                                        _posYGrid + _cellHeight);

            PCLXLWriter.addAttrUbyte(ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.ArcDirection,
                                     (Byte)PCLXLAttrEnums.eVal.eCounterClockWise);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.ArcPath);

            PCLXLWriter.addAttrSint16XY(ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.EndPoint,
                                        _cellWidth, 0);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.LineRelPath);

            PCLXLWriter.addAttrSint16XY(ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.EndPoint,
                                        0, -_cellHeight);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.LineRelPath);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.PaintPath);

            PCLXLWriter.writeStreamBlock(prnWriter, formAsMacro,
                                         buffer, ref indBuf);

            posX1 = _marginX  + gridWidthOuter  - twoCellWidth;
            posY1 = _posYGrid + gridHeightOuter - twoCellHeight;

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.NewPath);

            PCLXLWriter.addAttrSint16Box(ref buffer,
                                         ref indBuf,
                                         PCLXLAttributes.eTag.BoundingBox,
                                         posX1,
                                         posY1,
                                         (Int16)(posX1 + twoCellWidth),
                                         (Int16)(posY1 + twoCellHeight));

            PCLXLWriter.addAttrSint16XY(ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.StartPoint,
                                        (Int16)(posX1 + _cellWidth),
                                        (Int16)(posY1 + twoCellHeight));

            PCLXLWriter.addAttrSint16XY(ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.EndPoint,
                                        (Int16)(posX1 + twoCellWidth),
                                        (Int16)(posY1 + _cellHeight));

            PCLXLWriter.addAttrUbyte(ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.ArcDirection,
                                     (Byte)PCLXLAttrEnums.eVal.eCounterClockWise);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.ArcPath);

            PCLXLWriter.addAttrSint16XY(ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.EndPoint,
                                        -_cellWidth, 0);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.LineRelPath);

            PCLXLWriter.addAttrSint16XY(ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.EndPoint,
                                        0, _cellHeight);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.LineRelPath);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.PaintPath);

            PCLXLWriter.writeStreamBlock(prnWriter, formAsMacro,
                                         buffer, ref indBuf);

            //----------------------------------------------------------------//
            //                                                                //
            // Grid lines - Horizontal.                                       //
            //                                                                //
            //----------------------------------------------------------------//

            PCLXLWriter.addAttrUbyte(ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.NullBrush,
                                     0);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.SetBrushSource);

            PCLXLWriter.addAttrUbyte(ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.PenWidth,
                                     1);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.SetPenWidth);

            //----------------------------------------------------------------//

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.NewPath);

            PCLXLWriter.addAttrSint16XY(ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.Point,
                                        _marginX, _posYGrid);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.SetCursor);

            posX1 = 0;                      // relative movement X
            posY1 = twoCellHeight;          // relative movement Y
            posX2 = gridWidthOuter;         // relative draw X
            posY2 = 0;                      // relative draw Y

            for (Int32 i = 1; i < _gridRows; i++)
            {
                PCLXLWriter.addAttrSint16XY(ref buffer,
                                            ref indBuf,
                                            PCLXLAttributes.eTag.Point,
                                            posX1, posY1);

                PCLXLWriter.addOperator(ref buffer,
                                        ref indBuf,
                                        PCLXLOperators.eTag.SetCursorRel);

                PCLXLWriter.addAttrSint16XY(ref buffer,
                                            ref indBuf,
                                            PCLXLAttributes.eTag.EndPoint,
                                            posX2, posY2);

                PCLXLWriter.addOperator(ref buffer,
                                        ref indBuf,
                                        PCLXLOperators.eTag.LineRelPath);

                if (i == 1)
                { 
                    posX1 = -gridWidthOuter;
                    posY1 = _cellHeight;
                }
            }

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.PaintPath);

            PCLXLWriter.writeStreamBlock(prnWriter, formAsMacro,
                                         buffer, ref indBuf);

            //----------------------------------------------------------------//
            //                                                                //
            // Grid lines - Vertical.                                         //
            //                                                                //
            //----------------------------------------------------------------//

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.NewPath);

            PCLXLWriter.addAttrSint16XY(ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.Point,
                                        _marginX, _posYGrid);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.SetCursor);

            posX1 = twoCellWidth;           // relative movement X
            posY1 = 0;                      // relative movement Y
            posX2 = 0;                      // relative draw X
            posY2 = gridHeightOuter;        // relative draw Y

            for (Int32 i = 1; i < _gridCols; i++)
            {
                PCLXLWriter.addAttrSint16XY(ref buffer,
                                            ref indBuf,
                                            PCLXLAttributes.eTag.Point,
                                            posX1, posY1);

                PCLXLWriter.addOperator(ref buffer,
                                        ref indBuf,
                                        PCLXLOperators.eTag.SetCursorRel);

                PCLXLWriter.addAttrSint16XY(ref buffer,
                                            ref indBuf,
                                            PCLXLAttributes.eTag.EndPoint,
                                            posX2, posY2);

                PCLXLWriter.addOperator(ref buffer,
                                        ref indBuf,
                                        PCLXLOperators.eTag.LineRelPath);

                if (i == 1)
                {
                    posX1 = _cellWidth;
                    posY1 = -gridHeightOuter;
                }
            }

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.PaintPath);

            PCLXLWriter.writeStreamBlock(prnWriter, formAsMacro,
                                         buffer, ref indBuf);

            //----------------------------------------------------------------//
            //                                                                //
            // Shade control code cells.                                      //
            // Position depends on whether the Horizontal or Vertical grid    //
            // option was selected.                                           //
            //                                                                //
            //----------------------------------------------------------------//

            PCLXLWriter.addAttrUbyte(ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.ColorSpace,
                                     (Byte)PCLXLAttrEnums.eVal.eGray);

            PCLXLWriter.addAttrUbyte(ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.PaletteDepth,
                                     (Byte)PCLXLAttrEnums.eVal.e8Bit);

            PCLXLWriter.addAttrUbyteArray(ref buffer,
                                          ref indBuf,
                                          PCLXLAttributes.eTag.PaletteData,
                                          2,
                                          PCLXLWriter.monoPalette);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.SetColorSpace);

            PCLXLWriter.addAttrUbyte(ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.NullPen,
                                     0);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.SetPenSource);

            PCLXLWriter.addAttrUbyte(ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.TxMode,
                                     (Byte)PCLXLAttrEnums.eVal.eTransparent);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.SetPatternTxMode);

            PCLXLWriter.writeStreamBlock(prnWriter, formAsMacro,
                                         buffer, ref indBuf);

            PCLXLWriter.patternDefine (prnWriter,
                                       formAsMacro,
                                       patternID,
                                       patWidth,
                                       patHeight,
                                       patWidth,
                                       patHeight,
                                       PCLXLAttrEnums.eVal.eIndexedPixel,
                                       PCLXLAttrEnums.eVal.e1Bit,
                                       PCLXLAttrEnums.eVal.eTempPattern,
                                       PCLXLAttrEnums.eVal.eNoCompression,
                                       pattern_1);

            PCLXLWriter.addAttrSint16(ref buffer,
                                      ref indBuf,
                                      PCLXLAttributes.eTag.PatternSelectID,
                                      patternID);

            PCLXLWriter.addAttrSint16XY(ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.PatternOrigin,
                                        0, 0);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.SetBrushSource);

            posX1 = _marginX  + _cellWidth;
            posY1 = _posYGrid + _cellHeight;


            if (optGridVertical)
            {
                posX2 = (Int16)(posX1 + twoCellWidth);
                posY2 = (Int16)(posY1 + gridHeightInner);

                PCLXLWriter.addAttrUint16Box(ref buffer,
                                             ref indBuf,
                                             PCLXLAttributes.eTag.BoundingBox,
                                             (UInt16)posX1,
                                             (UInt16)posY1,
                                             (UInt16)posX2,
                                             (UInt16)posY2);

                PCLXLWriter.addOperator(ref buffer,
                                        ref indBuf,
                                        PCLXLOperators.eTag.Rectangle);

                posX1 += (_cellWidth * _gridDimHalf);
                posX2 = (Int16)(posX1 + twoCellWidth);

                PCLXLWriter.addAttrUint16Box(ref buffer,
                                             ref indBuf,
                                             PCLXLAttributes.eTag.BoundingBox,
                                             (UInt16)posX1,
                                             (UInt16)posY1,
                                             (UInt16)posX2,
                                             (UInt16)posY2);

                PCLXLWriter.addOperator(ref buffer,
                                        ref indBuf,
                                        PCLXLOperators.eTag.Rectangle);
            }
            else
            {
                posX2 = (Int16)(posX1 + gridWidthInner);
                posY2 = (Int16)(posY1 + twoCellHeight);

                PCLXLWriter.addAttrUint16Box(ref buffer,
                                             ref indBuf,
                                             PCLXLAttributes.eTag.BoundingBox,
                                             (UInt16)posX1,
                                             (UInt16)posY1,
                                             (UInt16)posX2,
                                             (UInt16)posY2);

                PCLXLWriter.addOperator(ref buffer,
                                        ref indBuf,
                                        PCLXLOperators.eTag.Rectangle);

                posY1 += (_cellHeight * _gridDimHalf);
                posY2 = (Int16)(posY1 + twoCellHeight);

                PCLXLWriter.addAttrUint16Box(ref buffer,
                                             ref indBuf,
                                             PCLXLAttributes.eTag.BoundingBox,
                                             (UInt16)posX1,
                                             (UInt16)posY1,
                                             (UInt16)posX2,
                                             (UInt16)posY2);

                PCLXLWriter.addOperator(ref buffer,
                                        ref indBuf,
                                        PCLXLOperators.eTag.Rectangle);
            }


            PCLXLWriter.writeStreamBlock(prnWriter, formAsMacro,
                                         buffer, ref indBuf);

            //----------------------------------------------------------------//
            //                                                                //
            // Cell column header and trailer labels.                         //
            // Content depends on whether the Horizontal or Vertical grid     //
            // option was selected.                                           //
            //                                                                //
            //----------------------------------------------------------------//


            PCLXLWriter.addAttrUbyte(ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.GrayLevel,
                                     128);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.SetBrushSource);

            PCLXLWriter.writeStreamBlock(prnWriter, formAsMacro,
                                         buffer, ref indBuf);

            //----------------------------------------------------------------//
            
            crntPtSize = 6;

            PCLXLWriter.font(prnWriter, formAsMacro, 6,
                             _symSet_19U, "Courier       Bd");

            posX1 = _marginX  + (_cellWidth / 3);
            posY1 = _posYGrid + _lineSpacing;

            PCLXLWriter.text(prnWriter, formAsMacro, false,
                             PCLXLWriter.advances_Courier, crntPtSize,
                             posX1, posY1, "hex");

            posX1 = _marginX  + gridWidthInner + _cellWidth  + (_cellWidth / 5);
            posY1 = _posYGrid + gridHeightInner + _cellHeight + _lineSpacing;

            PCLXLWriter.text(prnWriter, formAsMacro, false,
                             PCLXLWriter.advances_Courier, crntPtSize,
                             posX1, posY1, "dec");

            //----------------------------------------------------------------//

            crntPtSize = 10;

            PCLXLWriter.font(prnWriter, formAsMacro, crntPtSize,
                             _symSet_19U, "Courier       Bd");

            posX1 = _marginX  + ((_cellWidth * 17) / 20);
            posY1 = _posYGrid + _lineSpacing;

            if (optGridVertical)
            {
                PCLXLWriter.text(prnWriter, formAsMacro, false,
                                 PCLXLWriter.advances_Courier, crntPtSize,
                                 posX1, posY1,
                                 "  0_  1_  2_  3_  4_  5_  6_  7_" +
                                 "  8_  9_  A_  B_  C_  D_  E_  F_");
            }
            else
            {
                PCLXLWriter.text(prnWriter, formAsMacro, false,
                                 PCLXLWriter.advances_Courier, crntPtSize,
                                 posX1, posY1,
                                 "  _0  _1  _2  _3  _4  _5  _6  _7" +
                                 "  _8  _9  _A  _B  _C  _D  _E  _F");
            }

            posY1 = _posYGrid + gridHeightInner + _cellHeight + _lineSpacing;

            if (optGridVertical)
            {
                PCLXLWriter.text(prnWriter, formAsMacro, false,
                                 PCLXLWriter.advances_Courier, crntPtSize,
                                 posX1, posY1,
                                 "   0  16  32  48  64  80  96 112" +
                                 " 128 144 160 176 192 208 224 240");
            }
            else
            {
                PCLXLWriter.text(prnWriter, formAsMacro, false,
                                 PCLXLWriter.advances_Courier, crntPtSize,
                                 posX1, posY1,
                                 "  +0  +1  +2  +3  +4  +5  +6  +7" +
                                 "  +8  +9 +10 +11 +12 +13 +14 +15");
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Cell row labels.                                               //
            // Content depends on whether the Horizontal or Vertical grid     //
            // option was selected.                                           //
            //                                                                //
            //----------------------------------------------------------------//

            posX1 = _marginX  + (_cellWidth / 4);
            posY1 = _posYGrid + _cellHeight + _lineSpacing;

            if (optGridVertical)
            {
                for (Int32 i = 0; i < _gridRows; i++)
                {
                    PCLXLWriter.text(prnWriter, formAsMacro, false,
                                     PCLXLWriter.advances_Courier, crntPtSize,
                                     posX1, posY1,
                                     "_" + _hexChars[i].ToString());

                    posY1 += _cellHeight;
                }
            }
            else
            {
                for (Int32 i = 0; i < _gridRows; i++)
                {
                    PCLXLWriter.text(prnWriter, formAsMacro, false,
                                     PCLXLWriter.advances_Courier, crntPtSize,
                                     posX1, posY1,
                                     _hexChars[i].ToString() + "_");

                    posY1 += _cellHeight;
                }
            }


            posX1 = _marginX  + gridWidthInner + _cellWidth + (_cellWidth / 8);
            posY1 = _posYGrid + _cellHeight + _lineSpacing;

            if (optGridVertical)
            {
                for (Int32 i = 0; i < _gridRows; i++)
                {
                    PCLXLWriter.text(prnWriter, formAsMacro, false,
                                     PCLXLWriter.advances_Courier, crntPtSize,
                                     posX1, posY1,
                                     "+" + i);

                    posY1 += _cellHeight;
                }
            }
            else
            {
                for (Int32 i = 0; i < _gridRows; i++)
                {
                    String tmpStr = (i * _gridDim).ToString();

                    Int32 len = tmpStr.Length;

                    if (len == 2)
                        tmpStr = " " + tmpStr;
                    else if (len == 1)
                        tmpStr = "  " + tmpStr;

                    PCLXLWriter.text(prnWriter, formAsMacro, false,
                                     PCLXLWriter.advances_Courier, crntPtSize,
                                     posX1, posY1,
                                     tmpStr);

                    posY1 += _cellHeight;
                }
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Header text.                                                   //
            //                                                                //
            //----------------------------------------------------------------//

            crntPtSize = 12;

            PCLXLWriter.font(prnWriter, formAsMacro, crntPtSize,
                             _symSet_19U, "Arial         Bd");

            posX1 = _marginX;
            posY1 = _posYDesc;

            PCLXLWriter.text(prnWriter, formAsMacro, false,
                             PCLXLWriter.advances_ArialBold, crntPtSize,
                             posX1, posY1, "PCL XL font:");

            posY1 += _lineSpacing;

            PCLXLWriter.text(prnWriter, formAsMacro, false,
                             PCLXLWriter.advances_ArialBold, crntPtSize,
                             posX1, posY1, "Size:");

            posY1 += _lineSpacing;

            PCLXLWriter.text(prnWriter, formAsMacro, false,
                             PCLXLWriter.advances_ArialBold, crntPtSize,
                             posX1, posY1, "Symbol set:");

            posY1 += _lineSpacing;

            PCLXLWriter.text(prnWriter, formAsMacro, false,
                             PCLXLWriter.advances_ArialBold, crntPtSize,
                             posX1, posY1, "Description:");

            //----------------------------------------------------------------//
            //                                                                //
            // Footer text.                                                   //
            //                                                                //
            //----------------------------------------------------------------//

            posX1 = _marginX;
            posY1 = _posYSelData;

            PCLXLWriter.text(prnWriter, formAsMacro, false,
                             PCLXLWriter.advances_ArialBold, crntPtSize,
                             posX1, posY1, "PCL XL font characteristics:");

            posX1 += _cellWidth;
            posY1 += _cellHeight;

            PCLXLWriter.text(prnWriter, formAsMacro, false,
                             PCLXLWriter.advances_ArialBold, crntPtSize,
                             posX1, posY1, "Font name:");

            posY1 += _lineSpacing;

            PCLXLWriter.text(prnWriter, formAsMacro, false,
                             PCLXLWriter.advances_ArialBold, crntPtSize,
                             posX1, posY1, "Symbol set:");

            posY1 += _lineSpacing;

            PCLXLWriter.text(prnWriter, formAsMacro, false,
                             PCLXLWriter.advances_ArialBold, crntPtSize,
                             posX1, posY1, "Size (@ " + _unitsPerInch + " upi):");

            //----------------------------------------------------------------//

            crntPtSize = 6;

            PCLXLWriter.font(prnWriter, formAsMacro, crntPtSize,
                             _symSet_19U, "Arial         Bd");

            posX1 = _marginX + (_cellWidth * _gridDimHalf);
            posY1 = _posYSelData;

            PCLXLWriter.text(prnWriter, formAsMacro, false,
                             PCLXLWriter.advances_ArialBold, crntPtSize,
                             posX1, posY1,
                             "(the content of the grid is undefined if no" +
                             " font with these characteristics is available)");

            //----------------------------------------------------------------//
            //                                                                //
            // Overlay end.                                                   //
            //                                                                //
            //----------------------------------------------------------------//

            if (formAsMacro)
            {
                PCLXLWriter.addOperator (ref buffer,
                                  ref indBuf,
                                  PCLXLOperators.eTag.EndStream);

                prnWriter.Write(buffer, 0, indBuf);
            }
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
                                         Boolean      formAsMacro,
                                         Boolean      showC0Chars,
                                         Boolean      optGridVertical,
                                         String       fontId,
                                         String       fontDesc,
                                         UInt16       symSetKind1,
                                         String       fontName,
                                         String       symbolSetName,
                                         Int32        sampleRangeOffset,
                                         Double       pointSize,
                                         Boolean      downloadFont,
                                         String       fontFilename,
                                         Boolean      symSetUserSet,
                                         Boolean      showMapCodesUCS2,
                                         Boolean      showMapCodesUTF8,
                                         String       symSetUserFile,
                                         UInt16       symSetUserMapMax,
                                         UInt16[]     symSetUserMap)
        {
            const Int32 indxMajorC0Start = 0;
            const Int32 indxMajorC0End   = 2;
            const Int32 lenBuf = 1024;
            
            Byte[] buffer = new Byte[lenBuf];

            Int32 indBuf;

            Int16 posX,
                  posY,
                  posXStart,
                  posYStart;

            Single crntPtSize;
            Single charSize;
            String displayCharSize;

            String symSetId;

            //----------------------------------------------------------------//
            //                                                                //
            // Page initialisation.                                           //
            //                                                                //
            //----------------------------------------------------------------//

            indBuf = 0;

            symSetId = PCLSymbolSets.translateKind1ToId((UInt16)symSetKind1);
            charSize = PCLXLWriter.getCharSize((Single)pointSize);
            displayCharSize = charSize.ToString("F2");

            if (indxOrientation < PCLOrientations.getCount())
            {
                PCLXLWriter.addAttrUbyte(ref buffer,
                                         ref indBuf,
                                         PCLXLAttributes.eTag.Orientation,
                                         PCLOrientations.getIdPCLXL(indxOrientation));
            }

            if (indxPaperSize < PCLPaperSizes.getCount())
            {
                PCLXLWriter.addAttrUbyte(ref buffer,
                                         ref indBuf,
                                         PCLXLAttributes.eTag.MediaSize,
                                         PCLPaperSizes.getIdPCLXL(indxPaperSize));
            }

            if ((indxPaperType < PCLPaperTypes.getCount()) &&
                (PCLPaperTypes.getType(indxPaperType) !=
                    PCLPaperTypes.eEntryType.NotSet))
            {
                PCLXLWriter.addAttrUbyteArray(ref buffer,
                                              ref indBuf,
                                              PCLXLAttributes.eTag.MediaType,
                                              PCLPaperTypes.getName(indxPaperType));
            }

            PCLXLWriter.addAttrUbyte(ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.SimplexPageMode,
                                     (Byte) PCLXLAttrEnums.eVal.eSimplexFrontSide);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.BeginPage);

            PCLXLWriter.addAttrUint16XY(ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.PageOrigin,
                                        0, 0);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.SetPageOrigin);

            //----------------------------------------------------------------//
            //                                                                //
            // Background image.                                              //
            //                                                                //
            //----------------------------------------------------------------//

            if (formAsMacro)
            {
                PCLXLWriter.addAttrUbyteArray(ref buffer,
                                              ref indBuf,
                                              PCLXLAttributes.eTag.StreamName,
                                              _formName);

                PCLXLWriter.addOperator(ref buffer,
                                        ref indBuf,
                                        PCLXLOperators.eTag.ExecStream);

                prnWriter.Write(buffer, 0, indBuf);
                indBuf = 0;
            }
            else
            {
                prnWriter.Write(buffer, 0, indBuf);
                indBuf = 0;

                generateOverlay(prnWriter, false, optGridVertical);
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Descriptive text.                                              //
            //                                                                //
            //----------------------------------------------------------------//

            crntPtSize = 12;

            PCLXLWriter.addAttrUbyte(ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.GrayLevel,
                                     0);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.SetBrushSource);

            prnWriter.Write(buffer, 0, indBuf);
            indBuf = 0;

            PCLXLWriter.font(prnWriter, false, crntPtSize,
                             _symSet_19U, "Courier       Bd");

            posX = _marginX + ((_cellWidth * 7) / 2);
            posY = _posYDesc;

            if (downloadFont)
            {
                const Int32 maxLen = 51;
                const Int32 halfLen = (maxLen - 5) / 2;

                String tempId;

                Int32 len = fontFilename.Length;

                if (len < maxLen)
                    tempId = fontFilename;
                else
                    tempId = fontFilename.Substring(0, halfLen) + " ... " +
                             fontFilename.Substring(len - halfLen, halfLen);

                PCLXLWriter.text(prnWriter, false, false,
                                 PCLXLWriter.advances_Courier, crntPtSize,
                                 posX, posY, tempId);
            }
            else
            {
                PCLXLWriter.text(prnWriter, false, false,
                                 PCLXLWriter.advances_Courier, crntPtSize,
                                 posX, posY, fontId);
            }

            posY += _lineSpacing;

            PCLXLWriter.text(prnWriter, false, false,
                             PCLXLWriter.advances_Courier, crntPtSize,
                             posX, posY, pointSize.ToString() + " point");

            posY += _lineSpacing;

            if (sampleRangeOffset == 0)
            {
                PCLXLWriter.text(prnWriter, false, false,
                                 PCLXLWriter.advances_Courier, crntPtSize,
                                 posX, posY, symSetId +
                                             " (" + symbolSetName + ")");
            }
            else
            {
                String offsetText;

                offsetText = ": Range offset 0x" +
                             sampleRangeOffset.ToString ("X4");

                PCLXLWriter.text(prnWriter, false, false,
                                 PCLXLWriter.advances_Courier, crntPtSize,
                                 posX, posY, symSetId +
                                             " (" + symbolSetName + ")" +
                                             offsetText);
            }

            crntPtSize = 8;

            PCLXLWriter.font(prnWriter, false, crntPtSize,
                             _symSet_19U, "Courier       Bd");

            posY += _lineSpacing;

            PCLXLWriter.text(prnWriter, false, false,
                             PCLXLWriter.advances_Courier, crntPtSize,
                             posX, posY, fontDesc);

            //----------------------------------------------------------------//

            if (symSetUserSet)
            {
                const Int32 maxLen = 61;
                const Int32 halfLen = (maxLen - 5) / 2;

                Int32 len = symSetUserFile.Length;

                posY += _lineSpacing / 2;

                if (len < maxLen)
                    PCLXLWriter.text(prnWriter, false, false,
                                     PCLXLWriter.advances_Courier, crntPtSize,
                                     posX, posY,
                                     "Symbol set file: " + symSetUserFile);
                else
                    PCLXLWriter.text(prnWriter, false, false,
                                     PCLXLWriter.advances_Courier, crntPtSize,
                                     posX, posY,
                                     "Symbol set file: " +
                                     symSetUserFile.Substring(0, halfLen) +
                                     " ... " +
                                     symSetUserFile.Substring(len - halfLen,
                                                              halfLen));
            }

            //----------------------------------------------------------------//

            crntPtSize = 12;

            PCLXLWriter.font(prnWriter, false, crntPtSize,
                             _symSet_19U, "Courier       Bd");

            posX = _marginX + (_cellWidth * 6);
            posY = _posYSelData + _cellHeight;

            PCLXLWriter.text(prnWriter, false, false,
                             PCLXLWriter.advances_Courier, crntPtSize,
                             posX, posY, fontName);

            posY += _lineSpacing;

            PCLXLWriter.text(prnWriter, false, false,
                             PCLXLWriter.advances_Courier, crntPtSize,
                             posX, posY, symSetKind1.ToString());

            posY += _lineSpacing;

            PCLXLWriter.text(prnWriter, false, false,
                             PCLXLWriter.advances_Courier, crntPtSize,
                             posX, posY, displayCharSize);

            //----------------------------------------------------------------//
            //                                                                //
            // Grid.                                                          //
            //                                                                //
            //----------------------------------------------------------------//

            crntPtSize = (Single)pointSize;

            PCLXLWriter.addAttrUbyte(ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.GrayLevel,
                                     0);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.SetBrushSource);

            prnWriter.Write(buffer, 0, indBuf);
            indBuf = 0;
            
            PCLXLWriter.font(prnWriter, false, crntPtSize,
                             symSetKind1, fontName);
            
            //----------------------------------------------------------------//

            Int32 startIndxMajor;
            Int32 startRow;
            Int32 startCol;
            Int16 rowSize;

            if ((showC0Chars) || (sampleRangeOffset != 0))
                startIndxMajor = indxMajorC0Start;
            else
                startIndxMajor = indxMajorC0End;

            //----------------------------------------------------------------//

            if (optGridVertical)
            {
                startRow = 0;
                startCol = startIndxMajor;
    
                rowSize = (Int16)(_gridDim - startIndxMajor);

                posX = (Int16)(_marginX + (_cellWidth * (startIndxMajor + 1)) +
                                          (_cellWidth / 3));
                posY = (Int16)(_posYGrid + _cellHeight +
                                          (_cellHeight * 2 / 3));
            }
            else
            {
                startRow = startIndxMajor;
                startCol = 0;

                rowSize = _gridDim;

                posX = _marginX + _cellWidth + (_cellWidth / 3);
                posY = (Int16)(_posYGrid + (_cellHeight * (startIndxMajor + 1)) +
                                           (_cellHeight * 2 / 3));
            }

            posXStart = posX;
            posYStart = posY;

            Int16[] tmpAdvance = new Int16[rowSize];

            for (Int32 i = 0; i < rowSize; i++)
            {
                tmpAdvance[i] = _cellWidth;
            }

            for (Int32 row = startRow;
                       row < _gridDim;
                       row++)
            {
                Int32 indxMajor;

                PCLXLWriter.addAttrSint16XY(ref buffer,
                                            ref indBuf,
                                            PCLXLAttributes.eTag.Point,
                                            posX, posY);

                PCLXLWriter.addOperator(ref buffer,
                                        ref indBuf,
                                        PCLXLOperators.eTag.SetCursor);

                indxMajor = startIndxMajor + (row - startRow);

                if ((sampleRangeOffset == 0) && (! symSetUserSet))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // No range offset specified.                             //
                    //                                                        //
                    //--------------------------------------------------------//

                    Byte[] codes8Bit = new Byte[rowSize];

                    if (optGridVertical)
                    {
                        for (Int32 col = 0;
                                   col < rowSize;
                                   col++)
                        {
                            codes8Bit[col] =
                                (Byte)(((startIndxMajor + col) * _gridDim) +
                                       row);
                        }
                    }
                    else
                    { 
                        for (Int32 col = 0;
                                   col < rowSize;
                                   col++)
                        {
                            codes8Bit[col] = (Byte)((indxMajor * _gridDim) +
                                                    col);
                        }
                    }

                    PCLXLWriter.addAttrUbyteArray(
                        ref buffer,
                        ref indBuf,
                        PCLXLAttributes.eTag.TextData,
                        rowSize,
                        codes8Bit);
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Range offset specified (only for symbol set Unicode),  //
                    // or a User-defined PCL Symbol Set definition is in use. //
                    //                                                        //
                    // The Unicode standard is such that the offset could  be //
                    // up to 0x10ff00; but PCL XL only uses ubyte or uint16   //
                    // arrays for text characters, hence we are limited to    //
                    // offsets of 0xff00 or less.                             // 
                    //                                                        //
                    //--------------------------------------------------------//

                    UInt16[] codes16Bit = new UInt16[rowSize];

                    if (optGridVertical)
                    {
                        for (Int32 col = 0;
                                   col < rowSize;
                                   col++)
                        {
                            codes16Bit[col] =
                                (UInt16) (sampleRangeOffset +
                                          ((startIndxMajor + col) * _gridDim) +
                                           row);
                        }
                    }
                    else
                    {
                        for (Int32 col = 0;
                                   col < rowSize;
                                   col++)
                        {
                            codes16Bit[col] =
                                (UInt16)(sampleRangeOffset +
                                          (indxMajor * _gridDim) + col);
                        }
                    }

                    if (symSetUserSet)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // map the local code-points to the target            //
                        // code-points defined by the PCL User-Defined Symbol //
                        // Set file.                                          //
                        //                                                    //
                        //----------------------------------------------------//

                        Int32 indx;

                        for (Int32 i = 0; i < rowSize; i++)
                        {
                            indx = codes16Bit[i];

                            if (indx < symSetUserMapMax)
                            {
                                codes16Bit[i] = symSetUserMap[indx];
                            }
                            else
                            {
                                codes16Bit[i] = 0xffff; // not a character
                            }
                        }
                    }

                    PCLXLWriter.addAttrUint16Array(
                        ref buffer,
                        ref indBuf,
                        PCLXLAttributes.eTag.TextData,
                        rowSize,
                        codes16Bit);
                }

                PCLXLWriter.addAttrUbyteArray(
                    ref buffer,
                    ref indBuf,
                    PCLXLAttributes.eTag.XSpacingData,
                    rowSize,
                    tmpAdvance);

                PCLXLWriter.addOperator(ref buffer,
                                         ref indBuf,
                                         PCLXLOperators.eTag.Text);

                posY += _cellHeight;

                prnWriter.Write(buffer, 0, indBuf);
                indBuf = 0;
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Mapping of target code-points.                                 //
            //                                                                //
            //----------------------------------------------------------------//

            if (showMapCodesUCS2)
            {
                Int16 posXStartCopy = posXStart;
                Int16 posYStartCopy = posYStart;

                //------------------------------------------------------------//
                //                                                            //
                // Unicode target code-point value (print at top of cell).    //
                // Print target Unicode code-points referenced by the         //
                // user-defined symbol set file.                              //
                //                                                            //
                //------------------------------------------------------------//

                crntPtSize = 6;

                posXStart -= ((_cellWidth * 7) / 24);
                posYStart -= (_cellHeight / 2);

                posX = posXStart;
                posY = posYStart;

                PCLXLWriter.font(prnWriter, false, crntPtSize,
                                 _symSet_19U, "Arial           ");

                for (Int32 indxMajor = startIndxMajor;
                           indxMajor < _gridDim;
                           indxMajor++)
                {
                    UInt16 codeVal,
                           mapVal;

                    for (Int32 indxMinor = 0; indxMinor < _gridDim; indxMinor++)
                    {
                        codeVal = (UInt16)(((indxMajor * _gridDim) +
                                             indxMinor) + sampleRangeOffset);

                        if (symSetUserSet)
                        {
                            if (codeVal <= symSetUserMapMax)
                            {
                                mapVal = symSetUserMap[(Int32)codeVal];
                            }
                            else
                            {
                                mapVal = 0xffff;
                            }
                        }
                        else
                        {
                            mapVal = codeVal;
                        }

                        if (mapVal != 0xffff)
                        {
                            PCLXLWriter.text(prnWriter, false, false,
                                             PCLXLWriter.advances_ArialRegular,
                                             crntPtSize,
                                             posX, posY,
                                             "U+" + mapVal.ToString("X4"));
                        }

                        if (optGridVertical)
                            posY += _cellHeight;
                        else
                            posX += _cellWidth;
                    }

                    if (optGridVertical)
                        posXStart += _cellWidth;
                    else
                        posYStart += _cellHeight;

                    posX = posXStart;
                    posY = posYStart;
                }

                posXStart = posXStartCopy;
                posYStart = posYStartCopy;
            }

            if (showMapCodesUTF8)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Equivalent UTF-8 representation of target code-point       //
                // value (print at bottom of cell).                           //
                //                                                            //
                //------------------------------------------------------------//

                crntPtSize = 5;

                posXStart -= ((_cellWidth * 7) / 24);
                posYStart += ((_cellHeight * 3) / 10);

                posX = posXStart;
                posY = posYStart;

                PCLXLWriter.font(prnWriter, false, crntPtSize,
                                 _symSet_19U, "Arial           ");

                for (Int32 indxMajor = startIndxMajor;
                           indxMajor < _gridDim;
                           indxMajor++)
                {
                    UInt16 codeVal,
                           mapVal;

                    for (Int32 indxMinor = 0; indxMinor < _gridDim; indxMinor++)
                    {
                        codeVal = (UInt16)(((indxMajor * _gridDim) +
                                             indxMinor) + sampleRangeOffset);

                        if (symSetUserSet)
                        {
                            if (codeVal <= symSetUserMapMax)
                            {
                                mapVal = symSetUserMap[(Int32)codeVal];
                            }
                            else
                            {
                                mapVal = 0xffff;
                            }
                        }
                        else
                        {
                            mapVal = codeVal;
                        }

                        if (mapVal != 0xffff)
                        {
                            String utf8Hex = null;

                            PrnParseDataUTF8.convertUTF32ToUTF8HexString(
                                mapVal,
                                true,
                                ref utf8Hex);

                            PCLXLWriter.text(prnWriter, false, false,
                                             PCLXLWriter.advances_ArialRegular,
                                             crntPtSize,
                                             posX, posY,
                                             utf8Hex);
                        }

                        if (optGridVertical)
                            posY += _cellHeight;
                        else
                            posX += _cellWidth;
                    }

                    if (optGridVertical)
                        posXStart += _cellWidth;
                    else
                        posYStart += _cellHeight;

                    posX = posXStart;
                    posY = posYStart;
                }
            }

            //----------------------------------------------------------------//

            PCLXLWriter.addAttrUint16(ref buffer,
                                      ref indBuf,
                                      PCLXLAttributes.eTag.PageCopies,
                                      1);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.EndPage);

            prnWriter.Write(buffer, 0, indBuf);
        }
    }
}
