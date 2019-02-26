using System;
using System.IO;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides support for the PCLXL Gray element of the
    /// Colour action of the MiscSamples tool.
    /// 
    /// © Chris Hutchinson 2014
    /// 
    /// </summary>

    static class ToolMiscSamplesActColourGrayPCLXL
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const String _formName          = "MiscSamplesForm";

        const Int32 _symSet_19U         = 629;
        const UInt16 _unitsPerInch      = PCLXLWriter._sessionUPI;
        const Int16 _patternId_1        = 601;

        const Int16 _pageOriginX = (_unitsPerInch * 1);
        const Int16 _pageOriginY = (_unitsPerInch * 1);
        const Int16 _incInch = (_unitsPerInch * 1);
        const Int16 _lineInc = (_unitsPerInch * 5) / 6;
        const Int16 _colInc = (_unitsPerInch * 3) / 2;

        const Int16 _posXDesc = _pageOriginX;
        const Int16 _posXDesc1 = _posXDesc + ((_incInch * 15) / 4);
        const Int16 _posXDesc2 = _posXDesc + ((_incInch * 5) / 2);
        const Int16 _posXDesc3 = _posXDesc;
        const Int16 _posXDesc4 = _posXDesc;

        const Int16 _posYHddr = _pageOriginY;
        const Int16 _posYDesc1 = _pageOriginY + (_incInch);
        const Int16 _posYDesc2 = _pageOriginY + ((_incInch * 5) / 4);
        const Int16 _posYDesc3 = _pageOriginY + ((_incInch * 7) / 4);
        const Int16 _posYDesc4 = _pageOriginY + (_incInch * 2);

        const Int16 _posXData = _posXDesc + ((_incInch * 5) / 2);
        const Int16 _posYData = _pageOriginY + ((_incInch * 7) / 4);

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Static variables.                                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        static Int16 _fontIndexArial   = PCLFonts.getIndexForName("Arial");
        static Int16 _fontIndexCourier = PCLFonts.getIndexForName("Courier");

        static String _fontNameArial =
            PCLFonts.getPCLXLName(_fontIndexArial,
                                  PCLFonts.eVariant.Regular);
        static String _fontNameCourier =
            PCLFonts.getPCLXLName(_fontIndexCourier,
                                  PCLFonts.eVariant.Regular);
        static String _fontNameCourierBold =
            PCLFonts.getPCLXLName(_fontIndexCourier,
                                  PCLFonts.eVariant.Bold);

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

        public static void generateJob (BinaryWriter prnWriter,
                                        Int32        indxPaperSize,
                                        Int32        indxPaperType,
                                        Int32        indxOrientation,
                                        Int32[]      sampleDef,
                                        Boolean      formAsMacro)
        {
            generateJobHeader(prnWriter);

            if (formAsMacro)
                generateOverlay (prnWriter, true,
                                 indxPaperSize, indxOrientation);

            generatePage (prnWriter,
                         indxPaperSize,
                         indxPaperType,
                         indxOrientation,
                         sampleDef,
                         formAsMacro);
 
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

        private static void generateJobHeader(BinaryWriter prnWriter)
        {
            PCLXLWriter.stdJobHeader(prnWriter, "");
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
                                            Int32        indxPaperSize,
                                            Int32        indxOrientation)
        {
            const Int32 lenBuf = 1024;

            Byte[] buffer = new Byte[lenBuf];

            Int16 ptSize;

            Int32 indBuf;

            Int16 posX,
                  posY;

            UInt16 boxX1,
                   boxX2,
                   boxY1,
                   boxY2;

            Int16 rectX,
                  rectY,
                  rectHeight,
                  rectWidth;

            Byte stroke = 1;

            //----------------------------------------------------------------//

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

            PCLXLWriter.addOperator(ref buffer,
                              ref indBuf,
                              PCLXLOperators.eTag.PushGS);

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
                               stroke);

            PCLXLWriter.addOperator(ref buffer,
                              ref indBuf,
                              PCLXLOperators.eTag.SetPenWidth);

            PCLXLWriter.writeStreamBlock(prnWriter, formAsMacro,
                                   buffer, ref indBuf);

            //----------------------------------------------------------------//
            //                                                                //
            // Box.                                                           //
            //                                                                //
            //----------------------------------------------------------------//

            boxX1 = _unitsPerInch / 2;  // half-inch left margin
            boxY1 = _unitsPerInch / 2;  // half-inch top-margin

            boxX2 = (UInt16)(PCLPaperSizes.getPaperWidth(
                                    indxPaperSize, _unitsPerInch,
                                    PCLOrientations.eAspect.Portrait) -
                              boxX1);

            boxY2 = (UInt16)(PCLPaperSizes.getPaperLength(
                                    indxPaperSize, _unitsPerInch,
                                    PCLOrientations.eAspect.Portrait) -
                              boxY1);

            PCLXLWriter.addAttrUbyte(ref buffer,
                               ref indBuf,
                               PCLXLAttributes.eTag.TxMode,
                               (Byte)PCLXLAttrEnums.eVal.eTransparent);

            PCLXLWriter.addOperator(ref buffer,
                              ref indBuf,
                              PCLXLOperators.eTag.SetPatternTxMode);

            PCLXLWriter.addAttrUbyte(ref buffer,
                               ref indBuf,
                               PCLXLAttributes.eTag.TxMode,
                               (Byte)PCLXLAttrEnums.eVal.eTransparent);

            PCLXLWriter.addOperator(ref buffer,
                              ref indBuf,
                              PCLXLOperators.eTag.SetSourceTxMode);

            PCLXLWriter.addAttrUbyte(ref buffer,
                               ref indBuf,
                               PCLXLAttributes.eTag.GrayLevel,
                               100);

            PCLXLWriter.addOperator(ref buffer,
                              ref indBuf,
                              PCLXLOperators.eTag.SetPenSource);

            PCLXLWriter.addAttrUbyte(ref buffer,
                               ref indBuf,
                               PCLXLAttributes.eTag.PenWidth,
                               5);

            PCLXLWriter.addOperator(ref buffer,
                              ref indBuf,
                              PCLXLOperators.eTag.SetPenWidth);

            PCLXLWriter.addAttrUbyte(ref buffer,
                               ref indBuf,
                               PCLXLAttributes.eTag.NullBrush,
                               0);

            PCLXLWriter.addOperator(ref buffer,
                              ref indBuf,
                              PCLXLOperators.eTag.SetBrushSource);

            PCLXLWriter.addAttrUint16XY(ref buffer,
                                  ref indBuf,
                                  PCLXLAttributes.eTag.EllipseDimension,
                                  100, 100);

            PCLXLWriter.addAttrUint16Box(ref buffer,
                                   ref indBuf,
                                   PCLXLAttributes.eTag.BoundingBox,
                                   boxX1, boxY1, boxX2, boxY2);

            PCLXLWriter.addOperator(ref buffer,
                              ref indBuf,
                              PCLXLOperators.eTag.RoundRectangle);

            PCLXLWriter.writeStreamBlock(prnWriter, formAsMacro,
                                   buffer, ref indBuf);

            //----------------------------------------------------------------//
            //                                                                //
            // Text.                                                          //
            //                                                                //
            //----------------------------------------------------------------//

            PCLXLWriter.addAttrUbyte(ref buffer,
                               ref indBuf,
                               PCLXLAttributes.eTag.GrayLevel,
                               100);

            PCLXLWriter.addOperator(ref buffer,
                              ref indBuf,
                              PCLXLOperators.eTag.SetBrushSource);

            PCLXLWriter.addAttrUbyte(ref buffer,
                               ref indBuf,
                               PCLXLAttributes.eTag.NullPen,
                               0);

            PCLXLWriter.addOperator(ref buffer,
                              ref indBuf,
                              PCLXLOperators.eTag.SetPenSource);

            PCLXLWriter.writeStreamBlock(prnWriter, formAsMacro,
                                   buffer, ref indBuf);

            ptSize = 15;

            PCLXLWriter.font(prnWriter, formAsMacro, ptSize,
                             _symSet_19U, _fontNameCourierBold);

            posX = _posXDesc;
            posY = _posYHddr;

            PCLXLWriter.text(prnWriter, formAsMacro, false,
                       PCLXLWriter.advances_Courier, ptSize,
                       posX, posY,
                       "PCL XL Gray colour mode:");

            //----------------------------------------------------------------//

            ptSize = 12;

            PCLXLWriter.font(prnWriter, formAsMacro, ptSize,
                             _symSet_19U, _fontNameCourier);

            posY += _incInch / 2;

            PCLXLWriter.text(prnWriter, formAsMacro, false,
                       PCLXLWriter.advances_Courier, ptSize,
                       posX, posY,
                       "Sample 4-shade palette:");

            //----------------------------------------------------------------//

            posX = _posXDesc2;
            posY = _posYDesc2;

            PCLXLWriter.text(prnWriter, formAsMacro, false,
                       PCLXLWriter.advances_Courier, ptSize,
                       posX, posY,
                       "Gray");

            //----------------------------------------------------------------//

            posX = _posXDesc3;
            posY = _posYDesc3;

            PCLXLWriter.text(prnWriter, formAsMacro, false,
                       PCLXLWriter.advances_Courier, ptSize,
                       posX, posY,
                       "index");

            posX += _incInch;

            PCLXLWriter.text(prnWriter, formAsMacro, false,
                       PCLXLWriter.advances_Courier, ptSize,
                       posX, posY,
                           "value");

            //----------------------------------------------------------------//

            posX = _posXDesc4;
            posY = _posYDesc4;

            PCLXLWriter.text(prnWriter, formAsMacro, false,
                       PCLXLWriter.advances_Courier, ptSize,
                       posX, posY,
                       "0");

            posY += _lineInc;

            PCLXLWriter.text(prnWriter, formAsMacro, false,
                       PCLXLWriter.advances_Courier, ptSize,
                       posX, posY,
                       "1");

            posY += _lineInc;

            PCLXLWriter.text(prnWriter, formAsMacro, false,
                       PCLXLWriter.advances_Courier, ptSize,
                       posX, posY,
                       "2");

            posY += _lineInc;

            PCLXLWriter.text(prnWriter, formAsMacro, false,
                       PCLXLWriter.advances_Courier, ptSize,
                       posX, posY,
                       "3");

            //----------------------------------------------------------------//
            //                                                                //
            // Background shade.                                              //
            //                                                                //
            //----------------------------------------------------------------//

            rectX = _posXDesc2 - (_incInch / 4);
            rectY = _posYDesc2 + (_incInch / 4);
            rectWidth = (_incInch * 13) / 10;
            rectHeight = (_incInch * 7) / 2;

            PCLXLWriter.addAttrUbyte (ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.ColorSpace,
                                     (Byte) PCLXLAttrEnums.eVal.eGray);

            PCLXLWriter.addAttrUbyte (ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.PaletteDepth,
                                     (Byte) PCLXLAttrEnums.eVal.e8Bit);

            PCLXLWriter.addAttrUbyteArray (ref buffer,
                                          ref indBuf,
                                          PCLXLAttributes.eTag.PaletteData,
                                          2,
                                          PCLXLWriter.monoPalette);

            PCLXLWriter.addOperator (ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.SetColorSpace);

            PCLXLWriter.writeStreamBlock(prnWriter, formAsMacro,
                                   buffer, ref indBuf);

            patternDefineDpi600 (prnWriter, _patternId_1, formAsMacro);

            PCLXLWriter.addAttrUbyte (ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.NullPen,
                                     0);

            PCLXLWriter.addOperator (ref buffer,
                              ref indBuf,
                              PCLXLOperators.eTag.SetPenSource);

            PCLXLWriter.addAttrUbyte (ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.TxMode,
                                     (Byte) PCLXLAttrEnums.eVal.eTransparent);

            PCLXLWriter.addOperator (ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.SetPatternTxMode);

            PCLXLWriter.addAttrSint16 (ref buffer,
                                      ref indBuf,
                                      PCLXLAttributes.eTag.PatternSelectID,
                                      601);

            PCLXLWriter.addAttrSint16XY (ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.PatternOrigin,
                                        0, 0);

            PCLXLWriter.addOperator (ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.SetBrushSource);

            PCLXLWriter.addAttrUint16Box (ref buffer,
                                         ref indBuf,
                                         PCLXLAttributes.eTag.BoundingBox,
                                         (UInt16) rectX,
                                         (UInt16) rectY,
                                         (UInt16) (rectX + rectWidth),
                                         (UInt16) (rectY + rectHeight));

            PCLXLWriter.addOperator (ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.Rectangle);

            //----------------------------------------------------------------//
            //                                                                //
            // Overlay end.                                                   //
            //                                                                //
            //----------------------------------------------------------------//

            PCLXLWriter.addOperator(ref buffer,
                              ref indBuf,
                              PCLXLOperators.eTag.PopGS);

            PCLXLWriter.writeStreamBlock(prnWriter, formAsMacro,
                                   buffer, ref indBuf);

            if (formAsMacro)
            {
                PCLXLWriter.addOperator(ref buffer,
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
                                         Int32[]      sampleDef,
                                         Boolean      formAsMacro)
        {
            const Int32 sizeStd = 1024;

            Byte[] bufStd = new Byte[sizeStd];

            Int16 posX,
                  posY,
                  rectX,
                  rectY,
                  rectHeight,
                  rectWidth;
            
            Int32 indStd;

            Int16 ptSize;

      //    Int32 temp;

            Byte shade_0,
                 shade_1,
                 shade_2,
                 shade_3;

            indStd = 0;

            //----------------------------------------------------------------//

            if (indxOrientation < PCLOrientations.getCount())
            {
                PCLXLWriter.addAttrUbyte(ref bufStd,
                                   ref indStd,
                                   PCLXLAttributes.eTag.Orientation,
                                   PCLOrientations.getIdPCLXL(indxOrientation));
            }

            if (indxPaperSize < PCLPaperSizes.getCount())
            {
                PCLXLWriter.addAttrUbyte(ref bufStd,
                                   ref indStd,
                                   PCLXLAttributes.eTag.MediaSize,
                                   PCLPaperSizes.getIdPCLXL(indxPaperSize));
            }

            if ((indxPaperType < PCLPaperTypes.getCount()) &&
                (PCLPaperTypes.getType(indxPaperType) !=
                    PCLPaperTypes.eEntryType.NotSet))
            {
                PCLXLWriter.addAttrUbyteArray(ref bufStd,
                                        ref indStd,
                                        PCLXLAttributes.eTag.MediaType,
                                        PCLPaperTypes.getName(indxPaperType));
            }

            PCLXLWriter.addAttrUbyte(ref bufStd,
                               ref indStd,
                               PCLXLAttributes.eTag.SimplexPageMode,
                               (Byte)PCLXLAttrEnums.eVal.eSimplexFrontSide);

            PCLXLWriter.addOperator(ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.BeginPage);

            PCLXLWriter.addAttrUint16XY(ref bufStd,
                                  ref indStd,
                                  PCLXLAttributes.eTag.PageOrigin,
                                  0, 0);

            PCLXLWriter.addOperator(ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.SetPageOrigin);

            PCLXLWriter.addAttrUbyte(ref bufStd,
                               ref indStd,
                               PCLXLAttributes.eTag.ColorSpace,
                               (Byte)PCLXLAttrEnums.eVal.eGray);

            PCLXLWriter.addOperator(ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.SetColorSpace);

            prnWriter.Write(bufStd, 0, indStd);
            indStd = 0;
            
            //----------------------------------------------------------------//

            if (formAsMacro)
            {
                PCLXLWriter.addAttrUbyteArray(ref bufStd,
                                        ref indStd,
                                        PCLXLAttributes.eTag.StreamName,
                                        _formName);

                PCLXLWriter.addOperator(ref bufStd,
                                  ref indStd,
                                  PCLXLOperators.eTag.ExecStream);

                prnWriter.Write(bufStd, 0, indStd);
                indStd = 0;
            }
            else
            {
                generateOverlay(prnWriter, false,
                                indxPaperSize, indxOrientation);
            }

            //----------------------------------------------------------------//

            rectHeight = (Int16) (_lineInc / 2);
            rectWidth = _lineInc;

            //----------------------------------------------------------------//
            //                                                                //
            // Colour definitions.                                            //
            //                                                                //
            //----------------------------------------------------------------//

            shade_0 = (Byte) (sampleDef[0] & 0xff);
            shade_1 = (Byte) (sampleDef[1] & 0xff);
            shade_2 = (Byte) (sampleDef[2] & 0xff);
            shade_3 = (Byte) (sampleDef[3] & 0xff);

            //----------------------------------------------------------------//
            //                                                                //
            // Write details.                                                 //
            //                                                                //
            //----------------------------------------------------------------//

            ptSize = 12;

            PCLXLWriter.font (prnWriter, false, ptSize,
                             _symSet_19U, _fontNameCourier);

            posX = _posXDesc;
            posY = _posYHddr;

            //----------------------------------------------------------------//

            posX = _posXDesc4;
            posY = _posYDesc4;

            posX += _incInch;

            PCLXLWriter.text(prnWriter, false, false,
                       PCLXLWriter.advances_Courier, ptSize,
                       posX, posY,
                           "0x" +
                           shade_0.ToString ("x2"));

            posY += _lineInc;

            PCLXLWriter.text(prnWriter, false, false,
                       PCLXLWriter.advances_Courier, ptSize,
                       posX, posY,
                           "0x" +
                           shade_1.ToString ("x2"));

            posY += _lineInc;

            PCLXLWriter.text(prnWriter, false, false,
                       PCLXLWriter.advances_Courier, ptSize,
                       posX, posY,
                           "0x" +
                           shade_2.ToString ("x2"));

            posY += _lineInc;

            PCLXLWriter.text(prnWriter, false, false,
                       PCLXLWriter.advances_Courier, ptSize,
                       posX, posY,
                           "0x" +
                           shade_3.ToString ("x2"));

            //----------------------------------------------------------------//
            //                                                                //
            // Gray colour space.                                             //
            //                                                                //
            //----------------------------------------------------------------//

            posX = _posXData;
            posY = _posYData;

            rectX = posX;
            rectY = posY;

            PCLXLWriter.addAttrUbyte (ref bufStd,
                               ref indStd,
                               PCLXLAttributes.eTag.ColorSpace,
                               (Byte) PCLXLAttrEnums.eVal.eGray);

            PCLXLWriter.addOperator (ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.SetColorSpace);

            PCLXLWriter.addAttrUbyte (ref bufStd,
                                     ref indStd,
                                     PCLXLAttributes.eTag.NullPen,
                                     0);

            PCLXLWriter.addOperator (ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.SetPenSource);

            PCLXLWriter.addAttrUbyte (ref bufStd,
                               ref indStd,
                               PCLXLAttributes.eTag.GrayLevel,
                               shade_0);

            PCLXLWriter.addOperator (ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.SetBrushSource);

            PCLXLWriter.addAttrUint16Box (ref bufStd,
                                         ref indStd,
                                         PCLXLAttributes.eTag.BoundingBox,
                                         (UInt16) rectX,
                                         (UInt16) rectY,
                                         (UInt16) (rectX + rectWidth),
                                         (UInt16) (rectY + rectHeight));

            PCLXLWriter.addOperator (ref bufStd,
                                    ref indStd,
                                    PCLXLOperators.eTag.Rectangle);

            prnWriter.Write (bufStd, 0, indStd);
            indStd = 0;

            //----------------------------------------------------------------//

            rectY += _lineInc;


            PCLXLWriter.addAttrUbyte (ref bufStd,
                               ref indStd,
                               PCLXLAttributes.eTag.GrayLevel,
                               shade_1);

            PCLXLWriter.addOperator (ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.SetBrushSource);

            PCLXLWriter.addAttrUint16Box (ref bufStd,
                                         ref indStd,
                                         PCLXLAttributes.eTag.BoundingBox,
                                         (UInt16) rectX,
                                         (UInt16) rectY,
                                         (UInt16) (rectX + rectWidth),
                                         (UInt16) (rectY + rectHeight));

            PCLXLWriter.addOperator (ref bufStd,
                                    ref indStd,
                                    PCLXLOperators.eTag.Rectangle);

            prnWriter.Write (bufStd, 0, indStd);
            indStd = 0;

            //----------------------------------------------------------------//

            rectY += _lineInc;


            PCLXLWriter.addAttrUbyte (ref bufStd,
                               ref indStd,
                               PCLXLAttributes.eTag.GrayLevel,
                               shade_2);

            PCLXLWriter.addOperator (ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.SetBrushSource);

            PCLXLWriter.addAttrUint16Box (ref bufStd,
                                         ref indStd,
                                         PCLXLAttributes.eTag.BoundingBox,
                                         (UInt16) rectX,
                                         (UInt16) rectY,
                                         (UInt16) (rectX + rectWidth),
                                         (UInt16) (rectY + rectHeight));

            PCLXLWriter.addOperator (ref bufStd,
                                    ref indStd,
                                    PCLXLOperators.eTag.Rectangle);

            prnWriter.Write (bufStd, 0, indStd);
            indStd = 0;

            //----------------------------------------------------------------//

            rectY += _lineInc;


            PCLXLWriter.addAttrUbyte (ref bufStd,
                               ref indStd,
                               PCLXLAttributes.eTag.GrayLevel,
                               shade_3);

            PCLXLWriter.addOperator (ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.SetBrushSource);

            PCLXLWriter.addAttrUint16Box (ref bufStd,
                                         ref indStd,
                                         PCLXLAttributes.eTag.BoundingBox,
                                         (UInt16) rectX,
                                         (UInt16) rectY,
                                         (UInt16) (rectX + rectWidth),
                                         (UInt16) (rectY + rectHeight));

            PCLXLWriter.addOperator (ref bufStd,
                                    ref indStd,
                                    PCLXLOperators.eTag.Rectangle);

            prnWriter.Write (bufStd, 0, indStd);
            indStd = 0;

            //----------------------------------------------------------------//

            PCLXLWriter.addAttrUint16(ref bufStd,
                                ref indStd,
                                PCLXLAttributes.eTag.PageCopies,
                                1);

            PCLXLWriter.addOperator(ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.EndPage);

            prnWriter.Write(bufStd, 0, indStd);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a t t e r n D e f i n e D p i 6 0 0                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Define 600 dots-per-inch user-defined pattern.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void patternDefineDpi600 (BinaryWriter prnWriter,
                                                 Int16        patternId,
                                                 Boolean      embedded)
        {
            const UInt16 patWidth  = 16;
            const UInt16 patHeight = 16;

            const UInt16 destWidth =
                (UInt16) ((patWidth  * _unitsPerInch) / 600);
            const UInt16 destHeight =
                (UInt16) ((patHeight * _unitsPerInch) / 600);

            Byte[] pattern = { 0x00, 0x00, 0x60, 0x60,
                               0x60, 0x60, 0x00, 0x00,
                               0x00, 0x00, 0x06, 0x06,
                               0x06, 0x06, 0x00, 0x00,
                               0x00, 0x00, 0x60, 0x60,
                               0x60, 0x60, 0x00, 0x00,
                               0x00, 0x00, 0x06, 0x06,
                               0x06, 0x06, 0x00, 0x00 };

            PCLXLWriter.patternDefine (prnWriter,
                                       embedded,
                                       patternId,
                                       patWidth,
                                       patHeight,
                                       destWidth,
                                       destHeight,
                                       PCLXLAttrEnums.eVal.eIndexedPixel,
                                       PCLXLAttrEnums.eVal.e1Bit,
                                       PCLXLAttrEnums.eVal.eTempPattern,
                                       PCLXLAttrEnums.eVal.eNoCompression,
                                       pattern);
        }
    }
}
