using System;
using System.IO;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides support for the Logical Operations action
    /// of the MiscSamples tool.
    /// 
    /// © Chris Hutchinson 2014
    /// 
    /// </summary>

    static class ToolMiscSamplesActLogOperPCLXL
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const Int32 _symSet_19U             = 629;
        const UInt16 _unitsPerInch          = PCLXLWriter._sessionUPI;

        const Int16 _rasterRes              = 600;
        const Int32 _compCtRGB              = 3;

        const String _streamIdDestBoxPage   = "LogOperDestBoxPage";
        const String _streamIdSrcBoxRow     = "LogOperSrcBoxRow";

        const Int32 _patternId              = 101;

        const Int16 _incInch                = (_unitsPerInch * 1);
        const Int16 _pageOriginX            = (_incInch * 1);
        const Int16 _pageOriginY            = (_incInch * 1) / 2;
        const Int16 _rowInc                 = (_incInch * 5) / 4;
        const Int16 _colInc                 = (_incInch * 5) / 4;
        const Int16 _lineInc                = (_incInch / 6);

        const Int16 _posXPage_1_Hddr        = _pageOriginX;
        const Int16 _posYPage_1_Hddr        = _pageOriginY + (_incInch * 1) / 2;
        const Int16 _posYPage_1_Data1       = _pageOriginY + (_incInch * 9) / 4;
        const Int16 _posYPage_1_Data2       = _pageOriginY + (_incInch * 13) / 2;

        const Int16 _posXPage_n_Hddr        = _pageOriginX;
        const Int16 _posYPage_n_Hddr        = _pageOriginY;

        const Int16 _posXPage_n_Data        = _pageOriginX;
        const Int16 _posYPage_n_Data        = _pageOriginY + (_incInch / 3);

        const UInt16 _destBoxSide           = (UInt16) _incInch;

        const Int16 _sourceImagePixelsWidth = 192;
        const Int16 _sourceImagePixelsHeight= 192;

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        static Int16 _indxFontArial   = PCLFonts.getIndexForFontArial ();
        static Int16 _indxFontCourier = PCLFonts.getIndexForFontCourier ();

        static String _nameFontArialBold =
            PCLFonts.getPCLXLName (_indxFontArial,
                                   PCLFonts.eVariant.Bold);
        static String _nameFontArialRegular =
            PCLFonts.getPCLXLName (_indxFontArial,
                                   PCLFonts.eVariant.Regular);
        static String _nameFontCourierBold =
            PCLFonts.getPCLXLName (_indxFontCourier,
                                   PCLFonts.eVariant.Bold);
        static String _nameFontCourierRegular =
            PCLFonts.getPCLXLName (_indxFontCourier,
                                   PCLFonts.eVariant.Regular);

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e f i n e R G B P a l e t t e                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Define RGB palette array.                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void defineRGBPalette (Int32 indxPalette,
                                              Int32 paletteDepth,
                                              ref Byte [] paletteData)
        {
            Int32 indx,
                  rgbVal,
                  offset,
                  temp;

            for (Int32 i = 0; i < paletteDepth; i++)
            {
                indx = PCLXLPalettes.getColourId (indxPalette, i);
                rgbVal = PCLXLPalettes.getColourRGB (indxPalette, i);

                offset = _compCtRGB * indx;

                temp = rgbVal;

                paletteData [offset + 2] = (Byte) (temp & 0xff);
                temp = temp >> 8;
                paletteData [offset + 1] = (Byte) (temp & 0xff);
                temp = temp >> 8;
                paletteData [offset]     = (Byte) (temp & 0xff);
            }
        }

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
        // Note also that (unlike the PCL equivalent) only the lower-level    //
        // objects are written as user-defined streams; this is because:      //
        //  - PCL XL Class/Revision 2.0 does not support nested streams;      //
        //  - Even with a C/R 2.1 stream sent to my local LJ M475dn, nested   //
        //    streams produce a PCL XL error, so I've no way of testing such  //
        //    streams.                                                        //
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
                                       Boolean      flagUseMacros,
                                       Boolean      flagSrcTextPat)
        {
            Boolean flagOptColour;

            Int32 rgbClrD1 = 0,
                  rgbClrD2 = 0,
                  rgbClrS1 = 0,
                  rgbClrS2 = 0,
                  rgbClrT1 = 0,
                  rgbClrT2 = 0,
                  rgbClrBlack = 0,
                  rgbClrWhite = 0;

            Byte idClrS1,
                 idClrS2,
                 idClrT1,
                 idClrT2;

            String nameClrD1,
                   nameClrD2,
                   nameClrS1,
                   nameClrS2,
                   nameClrT1,
                   nameClrT2;

            //----------------------------------------------------------------//

            Int16 indxFontArial = PCLFonts.getIndexForFontArial ();

            PCLFonts.getPCLXLName (indxFontArial,
                                   PCLFonts.eVariant.Bold);

            //----------------------------------------------------------------//

            rgbClrBlack = PCLXLPalettes.getColourRGB(
                             indxPalette,
                             PCLXLPalettes.getClrItemBlack(indxPalette));
            rgbClrWhite = PCLXLPalettes.getColourRGB(
                             indxPalette,
                             PCLXLPalettes.getClrItemWhite(indxPalette));

            if (PCLXLPalettes.isMonochrome (indxPalette))
            {
                flagOptColour = false;

                rgbClrD1 = indxClrD1;
                rgbClrD2 = indxClrD2;
                rgbClrS1 = indxClrS1;
                rgbClrS2 = indxClrS2;
                rgbClrT1 = indxClrT1;
                rgbClrT2 = indxClrT2;

                idClrS1 = (Byte) indxClrS1;
                idClrS2 = (Byte) indxClrS2;
                idClrT1 = (Byte) indxClrT1;
                idClrT2 = (Byte) indxClrT2;

                nameClrD1 = PCLXLPalettes.getGrayLevel(indxPalette,
                                                       (Byte) indxClrD1);
                nameClrD2 = PCLXLPalettes.getGrayLevel(indxPalette,
                                                       (Byte) indxClrD2);
                nameClrS1 = PCLXLPalettes.getGrayLevel(indxPalette,
                                                       (Byte) indxClrS1);
                nameClrS2 = PCLXLPalettes.getGrayLevel(indxPalette,
                                                       (Byte) indxClrS2);
                nameClrT1 = PCLXLPalettes.getGrayLevel(indxPalette,
                                                       (Byte) indxClrT1);
                nameClrT2 = PCLXLPalettes.getGrayLevel(indxPalette,
                                                       (Byte) indxClrT2);
            }
            else
            {
                flagOptColour = true;

                rgbClrD1 = PCLXLPalettes.getColourRGB (indxPalette, indxClrD1);
                rgbClrD2 = PCLXLPalettes.getColourRGB (indxPalette, indxClrD2);
                rgbClrS1 = PCLXLPalettes.getColourRGB (indxPalette, indxClrS1);
                rgbClrS2 = PCLXLPalettes.getColourRGB (indxPalette, indxClrS2);
                rgbClrT1 = PCLXLPalettes.getColourRGB (indxPalette, indxClrT1);
                rgbClrT2 = PCLXLPalettes.getColourRGB (indxPalette, indxClrT2);

                idClrS1 = PCLXLPalettes.getColourId (indxPalette, indxClrS1);
                idClrS2 = PCLXLPalettes.getColourId (indxPalette, indxClrS2);
                idClrT1 = PCLXLPalettes.getColourId (indxPalette, indxClrT1);
                idClrT2 = PCLXLPalettes.getColourId (indxPalette, indxClrT2);

                nameClrD1 = PCLXLPalettes.getColourName (indxPalette, indxClrD1);
                nameClrD2 = PCLXLPalettes.getColourName (indxPalette, indxClrD2);
                nameClrS1 = PCLXLPalettes.getColourName (indxPalette, indxClrS1);
                nameClrS2 = PCLXLPalettes.getColourName (indxPalette, indxClrS2);
                nameClrT1 = PCLXLPalettes.getColourName (indxPalette, indxClrT1);
                nameClrT2 = PCLXLPalettes.getColourName (indxPalette, indxClrT2);
            }

            //----------------------------------------------------------------//

            generateJobHeader (prnWriter, "");

            //----------------------------------------------------------------//

            if (flagUseMacros)
            {
                writeDestBoxPage (prnWriter, rgbClrD1, rgbClrD2, rgbClrBlack,
                                  flagOptColour, true);

                writeSrcBoxRow (prnWriter, rgbClrS1, rgbClrS2, rgbClrBlack,
                                idClrS1, idClrS2,
                                flagOptColour, true, flagSrcTextPat);
            }
            
            generatePageSet (prnWriter,
                             indxPaperSize,
                             indxPaperType,
                             indxOrientation,
                             indxPalette,
                             rgbClrD1,
                             rgbClrD2,
                             rgbClrS1,
                             rgbClrS2,
                             rgbClrT1,
                             rgbClrT2,
                             rgbClrBlack,
                             rgbClrWhite,
                             idClrS1,
                             idClrS2,
                             idClrT1,
                             idClrT2,
                             nameClrD1,
                             nameClrD2,
                             nameClrS1,
                             nameClrS2,
                             nameClrT1,
                             nameClrT2,
                             minROP,
                             maxROP,
                             flagOptColour,
                             flagUseMacros,
                             flagSrcTextPat);

            if (flagUseMacros)
            {
                // delete user-defined streams

                PCLXLWriter.streamRemove (prnWriter, _streamIdDestBoxPage);
                PCLXLWriter.streamRemove (prnWriter, _streamIdSrcBoxRow);
            }

            generateJobTrailer (prnWriter, flagUseMacros);
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
                                              String       pjlCommand)
        {
            PCLXLWriter.stdJobHeader(prnWriter, pjlCommand);
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
            PCLXLWriter.stdJobTrailer(prnWriter, false, null);
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
                                            Int32 indxPaperSize,
                                            Int32 indxPaperType,
                                            Int32 indxOrientation,
                                            Int32 indxPalette,
                                            Byte defaultROP,
                                            Byte minROP,
                                            Byte maxROP,
                                            Int32 rgbClrD1,
                                            Int32 rgbClrD2,
                                            Int32 indxClrS1,
                                            Int32 indxClrS2,
                                            Int32 rgbClrT1,
                                            Int32 rgbClrT2,
                                            Int32 rgbClrBlack,
                                            Int32 rgbClrWhite,
                                            Byte  idClrS1,
                                            Byte  idClrS2,
                                            Byte  idClrT1,
                                            Byte  idClrT2,
                                            String nameClrD1,
                                            String nameClrD2,
                                            String nameClrS1,
                                            String nameClrS2,
                                            String nameClrT1,
                                            String nameClrT2,
                                            Byte [] paletteRGB, 
                                            Boolean flagOptColour,
                                            Boolean flagUseMacros,
                                            Boolean flagSrcTextPat)
        {
            const Int32 sizeStd = 1024;
            const Int32 sizeRGB = 3;

            Byte[] bufStd = new Byte[sizeStd];

            Byte[] rgbBlack = { 0, 0, 0 };
            Byte greyLevelBlack = 0;

            Int16 posX,
                  posY;

            Int32 indStd;

            Int16 ptSize;

            Int16 srcOffsetX,
                  srcOffsetY;

            String nameClrSpace,
                   nameShade;

            //----------------------------------------------------------------//

            srcOffsetX = (Int16) (((_destBoxSide / 2) -
                                    _sourceImagePixelsWidth) / 2);
            srcOffsetY = (Int16) ((_destBoxSide -
                                   _sourceImagePixelsHeight) / 2);

            indStd = 0;

            //----------------------------------------------------------------//
            //                                                                //
            // Page initialisation.                                           //
            //                                                                //
            //----------------------------------------------------------------//

            if (indxOrientation < PCLOrientations.getCount ())
            {
                PCLXLWriter.addAttrUbyte (ref bufStd,
                                   ref indStd,
                                   PCLXLAttributes.eTag.Orientation,
                                   PCLOrientations.getIdPCLXL (indxOrientation));
            }

            if (indxPaperSize < PCLPaperSizes.getCount ())
            {
                PCLXLWriter.addAttrUbyte (ref bufStd,
                                   ref indStd,
                                   PCLXLAttributes.eTag.MediaSize,
                                   PCLPaperSizes.getIdPCLXL (indxPaperSize));
            }

            if ((indxPaperType < PCLPaperTypes.getCount ()) &&
                (PCLPaperTypes.getType (indxPaperType) !=
                    PCLPaperTypes.eEntryType.NotSet))
            {
                PCLXLWriter.addAttrUbyteArray (ref bufStd,
                                        ref indStd,
                                        PCLXLAttributes.eTag.MediaType,
                                        PCLPaperTypes.getName (indxPaperType));
            }

            PCLXLWriter.addAttrUbyte (ref bufStd,
                               ref indStd,
                               PCLXLAttributes.eTag.DuplexPageMode,
                               (Byte) PCLXLAttrEnums.eVal.eDuplexVerticalBinding);

            PCLXLWriter.addOperator (ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.BeginPage);

            PCLXLWriter.addAttrUint16XY (ref bufStd,
                                  ref indStd,
                                  PCLXLAttributes.eTag.PageOrigin,
                                  0, 0);

            PCLXLWriter.addOperator (ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.SetPageOrigin);

            prnWriter.Write (bufStd, 0, indStd);
            indStd = 0;

            //----------------------------------------------------------------//
            //                                                                //
            // Colour space.                                                  //
            //                                                                //
            //----------------------------------------------------------------//

            nameClrSpace = PCLXLPalettes.getPaletteName(indxPalette);

            if (flagOptColour)
                nameShade = "colour";
            else
                nameShade = "gray level";

            if (flagOptColour)
            {
                PCLXLWriter.addAttrUbyte (ref bufStd,
                                          ref indStd,
                                          PCLXLAttributes.eTag.ColorSpace,
                                          (Byte) PCLXLAttrEnums.eVal.eRGB);

                PCLXLWriter.addAttrUbyte (ref bufStd,
                                          ref indStd,
                                          PCLXLAttributes.eTag.PaletteDepth,
                                          (Byte) PCLXLAttrEnums.eVal.e8Bit);

                PCLXLWriter.addAttrUbyteArray (ref bufStd,
                                               ref indStd,
                                               PCLXLAttributes.eTag.PaletteData,
                                               (Int16) paletteRGB.Length,
                                               paletteRGB);

                PCLXLWriter.addOperator (ref bufStd,
                                         ref indStd,
                                         PCLXLOperators.eTag.SetColorSpace);

                prnWriter.Write (bufStd, 0, indStd);
                indStd = 0;
            }
            else
            {
                PCLXLWriter.addAttrUbyte (ref bufStd,
                                   ref indStd,
                                   PCLXLAttributes.eTag.ColorSpace,
                                   (Byte) PCLXLAttrEnums.eVal.eGray);

                PCLXLWriter.addOperator (ref bufStd,
                                  ref indStd,
                                  PCLXLOperators.eTag.SetColorSpace);

                prnWriter.Write (bufStd, 0, indStd);
                indStd = 0;
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Define pattern.                                                //
            //                                                                //
            //----------------------------------------------------------------//

            if (flagOptColour)
            {
                writePattern (prnWriter, _patternId, idClrT1, idClrT2,
                              true);
            }
            else
            {
                writePattern (prnWriter, _patternId, idClrT1, idClrT2,
                              false);
            }

            prnWriter.Write (bufStd, 0, indStd);
            indStd = 0;

            //----------------------------------------------------------------//
            //                                                                //
            // Heading and introductory texts.                                //
            //                                                                //
            //----------------------------------------------------------------//

            ptSize = 15;

            posX = _posXPage_1_Hddr;
            posY = _posYPage_1_Hddr;

            if (flagOptColour)
            {
                PCLXLWriter.addAttrUbyteArray (ref bufStd,
                                   ref indStd,
                                   PCLXLAttributes.eTag.RGBColor,
                                   sizeRGB,
                                   rgbBlack);
            }
            else
            {
                PCLXLWriter.addAttrUbyte (ref bufStd,
                                   ref indStd,
                                   PCLXLAttributes.eTag.GrayLevel,
                                   greyLevelBlack);
            }

            PCLXLWriter.addOperator (ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.SetBrushSource);

            PCLXLWriter.font (prnWriter, false, ptSize, _symSet_19U, 
                              _nameFontArialBold);

            PCLXLWriter.text(prnWriter, false, false,
                       PCLXLWriter.advances_ArialBold, ptSize,
                       posX, posY,
                      "PCL XL Logical Operations samples:");

            ptSize = 12;

            posY += (_lineInc * 3);

            PCLXLWriter.font (prnWriter, false, ptSize, _symSet_19U,
                              _nameFontArialRegular);

            PCLXLWriter.text(prnWriter, false, false,
                       PCLXLWriter.advances_ArialRegular, ptSize,
                       posX, posY,
                      "Colour space = " + nameClrSpace);

            posY += (_lineInc * 2);

            PCLXLWriter.text(prnWriter, false, false,
                       PCLXLWriter.advances_ArialRegular, ptSize,
                       posX, posY,
                      "Shows how a Source image, in conjunction with a" +
                      " Texture (a Pattern and " + nameShade);

            posY += _lineInc;

            PCLXLWriter.text(prnWriter, false, false,
                       PCLXLWriter.advances_ArialRegular, ptSize,
                       posX, posY,
                      "combination) interacts with a Destination image" +
                      " (i.e. what is already on the page),");

            posY += _lineInc;

            PCLXLWriter.text(prnWriter, false, false,
                       PCLXLWriter.advances_ArialRegular, ptSize,
                       posX, posY,
                      "and the effect of the different Logical Operation" +
                      " (ROP) values, together with Source");

            posY += _lineInc;

            PCLXLWriter.text(prnWriter, false, false,
                       PCLXLWriter.advances_ArialRegular, ptSize,
                       posX, posY,
                      "and Texture (pattern) transparency settings.");

            prnWriter.Write (bufStd, 0, indStd);
            indStd = 0;

            //----------------------------------------------------------------//

            ptSize = 12;

            posY = _posYPage_1_Data1;

            PCLXLWriter.font (prnWriter, false, ptSize, _symSet_19U,
                              _nameFontCourierRegular);

            PCLXLWriter.text(prnWriter, false, false,
                       PCLXLWriter.advances_Courier, ptSize,
                       posX, posY,
                      "(D)estination:");

            posY += _rowInc;

            PCLXLWriter.text(prnWriter, false, false,
                       PCLXLWriter.advances_Courier, ptSize,
                       posX, posY,
                      "(S)ource:");

            posY += _rowInc;

            PCLXLWriter.text(prnWriter, false, false,
                       PCLXLWriter.advances_Courier, ptSize,
                       posX, posY,
                      "(T)exture (pattern):");

            //----------------------------------------------------------------//
            //                                                                //
            // Destination image.                                             //
            //                                                                //
            //----------------------------------------------------------------//

            posX += (_colInc * 2);
            posY = _posYPage_1_Data1;

            PCLXLWriter.addOperator(ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.PushGS);
            
            PCLXLWriter.addAttrSint16XY(ref bufStd,
                                  ref indStd,
                                  PCLXLAttributes.eTag.PageOrigin,
                                  posX, posY);

            PCLXLWriter.addOperator(ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.SetPageOrigin);

            prnWriter.Write (bufStd, 0, indStd);
            indStd = 0;

            writeDestBox (prnWriter, rgbClrD1, rgbClrD2, rgbClrBlack,
                          flagOptColour, false);
            
            PCLXLWriter.addOperator(ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.PopGS);

            //----------------------------------------------------------------//
            //                                                                //
            // Source image.                                                  //
            //                                                                //
            //----------------------------------------------------------------//

            posY += _rowInc;

            posX += srcOffsetX;
            posY += srcOffsetY;

            PCLXLWriter.addAttrSint16XY (ref bufStd,
                                  ref indStd,
                                  PCLXLAttributes.eTag.Point,
                                  posX, posY);

            PCLXLWriter.addOperator (ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.SetCursor);

            prnWriter.Write (bufStd, 0, indStd);
            indStd = 0;

            ptSize = 28;

            PCLXLWriter.font (prnWriter, false, ptSize, _symSet_19U,
                              _nameFontArialRegular);

            writeSrcBox (prnWriter, indxClrS1, indxClrS2, rgbClrBlack,
                         idClrS1, idClrS2,
                         flagOptColour, false, flagSrcTextPat);

            //----------------------------------------------------------------//
            //                                                                //
            // Texture (pattern).                                             //
            //                                                                //
            //----------------------------------------------------------------//

            PCLXLWriter.addOperator(ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.PushGS);

            /*
            if (flagOptColour)
            {
                PCLXLWriter.addAttrUbyte (ref bufStd,
                                         ref indStd,
                                         PCLXLAttributes.eTag.ColorSpace,
                                         (Byte) PCLXLAttrEnums.eVal.eRGB);
            }
            else
            {
                PCLXLWriter.addAttrUbyte (ref bufStd,
                                         ref indStd,
                                         PCLXLAttributes.eTag.ColorSpace,
                                         (Byte) PCLXLAttrEnums.eVal.eGray);

                PCLXLWriter.addAttrUbyte (ref bufStd,
                                         ref indStd,
                                         PCLXLAttributes.eTag.PaletteDepth,
                                         (Byte) PCLXLAttrEnums.eVal.e8Bit);

                PCLXLWriter.addAttrUbyteArray (ref bufStd,
                                              ref indStd,
                                              PCLXLAttributes.eTag.PaletteData,
                                              2,
                                              PCLXLWriter.monoPalette);
            }
            PCLXLWriter.addOperator (ref bufStd,
                                    ref indStd,
                                    PCLXLOperators.eTag.SetColorSpace);
            */

            PCLXLWriter.addAttrUbyte (ref bufStd,
                                     ref indStd,
                                     PCLXLAttributes.eTag.NullPen,
                                     0);

            PCLXLWriter.addOperator (ref bufStd,
                                    ref indStd,
                                    PCLXLOperators.eTag.SetPenSource);

            PCLXLWriter.addAttrSint16 (ref bufStd,
                                   ref indStd,
                                   PCLXLAttributes.eTag.PatternSelectID,
                                   _patternId);

            PCLXLWriter.addOperator (ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.SetBrushSource);

            prnWriter.Write (bufStd, 0, indStd);
            indStd = 0;

            posY += _rowInc;

            posX -= srcOffsetX;
            posY -= srcOffsetY;

            PCLXLWriter.rectangle (prnWriter,
                                   false,
                                   (UInt16) posX,
                                   (UInt16) posY,
                                   _destBoxSide,
                                   _destBoxSide);

            PCLXLWriter.addOperator(ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.PopGS);

            prnWriter.Write (bufStd, 0, indStd);
            indStd = 0;

            //----------------------------------------------------------------//
            //                                                                //
            // Image explanatory texts.                                       //
            //                                                                //
            //----------------------------------------------------------------//

            posX = _posXPage_1_Hddr;
            posX += (_rowInc * 3);

            posY = _posYPage_1_Data1;

            ptSize = 8;

            PCLXLWriter.font (prnWriter, false, ptSize, _symSet_19U,
                              _nameFontCourierRegular);

            PCLXLWriter.text(prnWriter, false, false,
                       PCLXLWriter.advances_Courier, ptSize,
                       posX, posY,
                       nameShade + "s = " + nameClrD1 + " / " + nameClrD2);

            //----------------------------------------------------------------//

            posY = _posYPage_1_Data1;
            posY += _rowInc;

            PCLXLWriter.text(prnWriter, false, false,
                       PCLXLWriter.advances_Courier, ptSize,
                       posX, posY,
                       nameShade + "s = " + nameClrS1 + " / " + nameClrS2);

            posY += _lineInc;

            PCLXLWriter.text(prnWriter, false, false,
                       PCLXLWriter.advances_Courier, ptSize,
                       posX, posY,
                      "includes:");

            posY += _lineInc;

            PCLXLWriter.text(prnWriter, false, false,
                       PCLXLWriter.advances_Courier, ptSize,
                       posX, posY,
                      " - small square raster image");

            posY += _lineInc;

            PCLXLWriter.text(prnWriter, false, false,
                       PCLXLWriter.advances_Courier, ptSize,
                       posX, posY,
                      " - inverse copy of raster image");

            posY += _lineInc;

            if (flagSrcTextPat)
            {
                PCLXLWriter.text (prnWriter, false, false,
                           PCLXLWriter.advances_Courier, ptSize,
                           posX, posY,
                          " - text (the letter 'O' using the defined pattern");
            }
            else
            {
                PCLXLWriter.text (prnWriter, false, false,
                           PCLXLWriter.advances_Courier, ptSize,
                           posX, posY,
                          " - text (the letter 'O' in each " + nameShade + ")");
            }

            //----------------------------------------------------------------//

            posY = _posYPage_1_Data1;
            posY += (_rowInc * 2);

            PCLXLWriter.text(prnWriter, false, false,
                       PCLXLWriter.advances_Courier, ptSize,
                       posX, posY,
                       nameShade + "s = " + nameClrT1 + " / " + nameClrT2);

            //----------------------------------------------------------------//
            //                                                                //
            // Sample with default ROP.                                       //
            //                                                                //
            //----------------------------------------------------------------//

            ptSize = 12;

            posX = _posXPage_1_Hddr;
            posY = _posYPage_1_Data2;

            PCLXLWriter.font (prnWriter, false, ptSize, _symSet_19U,
                              _nameFontCourierBold);

            PCLXLWriter.text(prnWriter, false, false,
                       PCLXLWriter.advances_Courier, ptSize,
                       posX, posY,
                      "Sample (using default ROP):");

            posY += _rowInc / 2;
            posY += _incInch / 3;

            ptSize = 10;

            PCLXLWriter.font (prnWriter, false, ptSize, _symSet_19U,
                              _nameFontCourierRegular);

            PCLXLWriter.text(prnWriter, false, false,
                       PCLXLWriter.advances_Courier, ptSize,
                       posX, posY,
                       PCLLogicalOperations.getDescShort (defaultROP));

            //----------------------------------------------------------------//
            
            posY -= _incInch / 3;

            PCLXLWriter.addOperator (ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.PushGS);

            PCLXLWriter.addAttrSint16XY (ref bufStd,
                                  ref indStd,
                                  PCLXLAttributes.eTag.PageOrigin,
                                  posX, posY);

            PCLXLWriter.addOperator (ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.SetPageOrigin);

            prnWriter.Write (bufStd, 0, indStd);
            indStd = 0;

            writeDestBoxRowHddr (prnWriter, false);

            PCLXLWriter.addOperator (ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.PopGS);

            //----------------------------------------------------------------//
            
            posX += _colInc;
            posY += _incInch / 3;

            PCLXLWriter.addOperator (ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.PushGS);

            PCLXLWriter.addAttrSint16XY (ref bufStd,
                                  ref indStd,
                                  PCLXLAttributes.eTag.PageOrigin,
                                  posX, posY);

            PCLXLWriter.addOperator (ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.SetPageOrigin);

            prnWriter.Write (bufStd, 0, indStd);
            indStd = 0;

            writeDestBoxRow (prnWriter, rgbClrD1, rgbClrD2, rgbClrBlack,
                             flagOptColour, false);

            PCLXLWriter.addOperator (ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.PopGS);

            prnWriter.Write (bufStd, 0, indStd);
            indStd = 0;

            //----------------------------------------------------------------//

            ptSize = 28;

            PCLXLWriter.font (prnWriter, false, ptSize, _symSet_19U,
                              _nameFontArialRegular);

            //----------------------------------------------------------------//

            posX += srcOffsetX;
            posY += srcOffsetY;

            PCLXLWriter.addAttrSint16 (ref bufStd, ref indStd,
                                       PCLXLAttributes.eTag.PatternSelectID,
                                       _patternId);

            PCLXLWriter.addOperator (ref bufStd, ref indStd,
                                     PCLXLOperators.eTag.SetBrushSource);

            prnWriter.Write (bufStd, 0, indStd);
            indStd = 0;

            //----------------------------------------------------------------//

            PCLXLWriter.addAttrSint16XY (ref bufStd, ref indStd,
                                         PCLXLAttributes.eTag.Point,
                                         posX, posY);

            PCLXLWriter.addOperator (ref bufStd, ref indStd,
                                     PCLXLOperators.eTag.SetCursor);

            PCLXLWriter.addAttrUbyte (ref bufStd, ref indStd,
                                      PCLXLAttributes.eTag.ROP3,
                                      defaultROP);

            PCLXLWriter.addOperator (ref bufStd, ref indStd,
                                     PCLXLOperators.eTag.SetROP);

            prnWriter.Write (bufStd, 0, indStd);
            indStd = 0;

            if (flagUseMacros)
                PCLXLWriter.streamExec (prnWriter, false, _streamIdSrcBoxRow);
            else
                writeSrcBoxRow (prnWriter, indxClrS1, indxClrS2, rgbClrBlack,
                                idClrS1, idClrS2,
                                flagOptColour, false, flagSrcTextPat);

            //----------------------------------------------------------------//
            //                                                                //
            // Explanatory text for following pages.                          //
            //                                                                //
            //----------------------------------------------------------------//

            if (flagOptColour)
            {
                PCLXLWriter.addAttrUbyteArray (ref bufStd,
                                   ref indStd,
                                   PCLXLAttributes.eTag.RGBColor,
                                   sizeRGB,
                                   rgbBlack);
            }
            else
            {
                PCLXLWriter.addAttrUbyte (ref bufStd,
                                   ref indStd,
                                   PCLXLAttributes.eTag.GrayLevel,
                                   greyLevelBlack);
            }

            PCLXLWriter.addOperator (ref bufStd,
                                    ref indStd,
                                    PCLXLOperators.eTag.SetBrushSource);

            prnWriter.Write (bufStd, 0, indStd);
            indStd = 0;

            //----------------------------------------------------------------//

            ptSize = 12;

            posX -= _colInc;
            posY += _rowInc;

            PCLXLWriter.font (prnWriter, false, ptSize, _symSet_19U,
                              _nameFontArialBold);

            PCLXLWriter.text(prnWriter, false, false,
                       PCLXLWriter.advances_ArialBold, ptSize,
                       posX, posY,
                      "The following pages show the effects of the various" +
                      " Logical Operation (ROP)");

            posY += _lineInc;

            PCLXLWriter.text(prnWriter, false, false,
                       PCLXLWriter.advances_ArialBold, ptSize,
                       posX, posY,
                      "values (in the range " +
                      PCLLogicalOperations.getDescShort (minROP) +
                      " - " +
                      PCLLogicalOperations.getDescShort (maxROP) +
                      "), when combined with");

            posY += _lineInc;

            PCLXLWriter.text(prnWriter, false, false,
                       PCLXLWriter.advances_ArialBold, ptSize,
                       posX, posY,
                      "different Source and Texture (pattern) transparency" +
                      " settings:");

            //----------------------------------------------------------------//

            PCLXLWriter.addAttrUint16 (ref bufStd,
                                ref indStd,
                                PCLXLAttributes.eTag.PageCopies,
                                1);

            PCLXLWriter.addOperator (ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.EndPage);

            prnWriter.Write (bufStd, 0, indStd);
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
                                            Int32 indxPaperSize,
                                            Int32 indxOrientation,
                                            Byte startROP,
                                            Int32 rgbClrD1,
                                            Int32 rgbClrD2,
                                            Int32 rgbClrS1,
                                            Int32 rgbClrS2,
                                            Int32 rgbClrBlack,
                                            Int32 rgbClrWhite,
                                            Byte idClrS1,
                                            Byte idClrS2,
                                            Byte idClrT1,
                                            Byte idClrT2,
                                            Byte [] paletteRGB,
                                            Boolean flagOptColour,
                                            Boolean flagUseMacros,
                                            Boolean flagSrcTextPat)
        {
            const Int32 sizeStd = 1024;
            const Int32 sizeRGB = 3;

            Byte[] bufStd = new Byte[sizeStd];

            Byte [] rgbBlack = {0, 0, 0};
            Byte greyLevelBlack = 0;

            Int16 posX,
                  posY;

            Int32 indStd;

            Int16 ptSize;

            Int16 srcOffsetX,
                  srcOffsetY;

            //----------------------------------------------------------------//

            srcOffsetX = (Int16) (((_destBoxSide / 2) -
                                    _sourceImagePixelsWidth) / 2);
            srcOffsetY = (Int16) ((_destBoxSide -
                                   _sourceImagePixelsHeight) / 2);

            indStd = 0;

            //----------------------------------------------------------------//

            if (indxOrientation < PCLOrientations.getCount ())
            {
                PCLXLWriter.addAttrUbyte (ref bufStd,
                                   ref indStd,
                                   PCLXLAttributes.eTag.Orientation,
                                   PCLOrientations.getIdPCLXL (indxOrientation));
            }

            if (indxPaperSize < PCLPaperSizes.getCount ())
            {
                PCLXLWriter.addAttrUbyte (ref bufStd,
                                   ref indStd,
                                   PCLXLAttributes.eTag.MediaSize,
                                   PCLPaperSizes.getIdPCLXL (indxPaperSize));
            }

            PCLXLWriter.addOperator (ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.BeginPage);

            PCLXLWriter.addAttrUint16XY (ref bufStd,
                                  ref indStd,
                                  PCLXLAttributes.eTag.PageOrigin,
                                  0, 0);

            PCLXLWriter.addOperator (ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.SetPageOrigin);

            if (flagOptColour)
            {
                PCLXLWriter.addAttrUbyte (ref bufStd,
                                          ref indStd,
                                          PCLXLAttributes.eTag.ColorSpace,
                                          (Byte) PCLXLAttrEnums.eVal.eRGB);

                PCLXLWriter.addAttrUbyte (ref bufStd,
                                          ref indStd,
                                          PCLXLAttributes.eTag.PaletteDepth,
                                          (Byte) PCLXLAttrEnums.eVal.e8Bit);

                PCLXLWriter.addAttrUbyteArray (ref bufStd,
                                               ref indStd,
                                               PCLXLAttributes.eTag.PaletteData,
                                               (Int16) paletteRGB.Length,
                                               paletteRGB);

                PCLXLWriter.addOperator (ref bufStd,
                                         ref indStd,
                                         PCLXLOperators.eTag.SetColorSpace);

                prnWriter.Write (bufStd, 0, indStd);
                indStd = 0;
            }
            else
            {
                PCLXLWriter.addAttrUbyte (ref bufStd,
                                   ref indStd,
                                   PCLXLAttributes.eTag.ColorSpace,
                                   (Byte) PCLXLAttrEnums.eVal.eGray);

                PCLXLWriter.addOperator (ref bufStd,
                                  ref indStd,
                                  PCLXLOperators.eTag.SetColorSpace);

                prnWriter.Write (bufStd, 0, indStd);
                indStd = 0;
            }

            //----------------------------------------------------------------//

            if (flagOptColour)
            {
                PCLXLWriter.addAttrUbyteArray (ref bufStd,
                                   ref indStd,
                                   PCLXLAttributes.eTag.RGBColor,
                                   sizeRGB,
                                   rgbBlack);
            }
            else
            {
                PCLXLWriter.addAttrUbyte (ref bufStd,
                                   ref indStd,
                                   PCLXLAttributes.eTag.GrayLevel,
                                   greyLevelBlack);
            }

            PCLXLWriter.addOperator (ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.SetBrushSource);

            prnWriter.Write (bufStd, 0, indStd);
            indStd = 0;

            ptSize = 10;

            PCLXLWriter.font (prnWriter, false, ptSize, _symSet_19U,
                              _nameFontCourierRegular);

            //----------------------------------------------------------------//

            posX = _posXPage_n_Data;
            posY = _posYPage_n_Data;

            for (Int32 i = 0; i < 8; i++)
            {
                PCLXLWriter.text(prnWriter, false, false,
                       PCLXLWriter.advances_Courier, ptSize,
                       posX, posY,
                       PCLLogicalOperations.getDescShort (startROP + i));

                posY += _rowInc;
            }

            //----------------------------------------------------------------//

            if (flagUseMacros)
                PCLXLWriter.streamExec (prnWriter, false, _streamIdDestBoxPage);
            else
                writeDestBoxPage (prnWriter, rgbClrD1, rgbClrD2, rgbClrBlack,
                                  flagOptColour, false);

            //----------------------------------------------------------------//

            ptSize = 28;

            PCLXLWriter.font (prnWriter, false, ptSize, _symSet_19U,
                              _nameFontArialRegular);

            posX = (Int16) (_posXPage_n_Data + _colInc);
            posY = _posYPage_n_Data;

            posX += srcOffsetX;
            posY += srcOffsetY;

            PCLXLWriter.addAttrSint16 (ref bufStd, ref indStd,
                                       PCLXLAttributes.eTag.PatternSelectID,
                                       _patternId);

            PCLXLWriter.addOperator (ref bufStd, ref indStd,
                                     PCLXLOperators.eTag.SetBrushSource);

            PCLXLWriter.addAttrSint16XY (ref bufStd,
                                         ref indStd,
                                         PCLXLAttributes.eTag.Point,
                                         posX, posY);

            PCLXLWriter.addOperator (ref bufStd,
                                     ref indStd,
                                     PCLXLOperators.eTag.SetCursor);

            prnWriter.Write (bufStd, 0, indStd);
            indStd = 0;

            for (Int32 i = 0; i < 8; i++)
            {
                PCLXLWriter.addAttrUbyte (ref bufStd,
                                          ref indStd,
                                          PCLXLAttributes.eTag.ROP3,
                                          (Byte) (startROP + i));

                PCLXLWriter.addOperator (ref bufStd,
                                         ref indStd,
                                         PCLXLOperators.eTag.SetROP);

                prnWriter.Write (bufStd, 0, indStd);
                indStd = 0;

                if (flagUseMacros)
                    PCLXLWriter.streamExec (prnWriter, false,
                                            _streamIdSrcBoxRow);
                else
                    writeSrcBoxRow (prnWriter, rgbClrS1, rgbClrS2,
                                    rgbClrBlack, idClrS1, idClrS2,
                                    flagOptColour, false, flagSrcTextPat);

                posY += +_rowInc;

                PCLXLWriter.addAttrSint16XY (ref bufStd,
                                             ref indStd,
                                             PCLXLAttributes.eTag.Point,
                                             posX, posY);

                PCLXLWriter.addOperator (ref bufStd,
                                         ref indStd,
                                         PCLXLOperators.eTag.SetCursor);

                prnWriter.Write (bufStd, 0, indStd);
                indStd = 0;
            }

            //----------------------------------------------------------------//

            PCLXLWriter.addAttrUint16 (ref bufStd,
                                ref indStd,
                                PCLXLAttributes.eTag.PageCopies,
                                1);

            PCLXLWriter.addOperator (ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.EndPage);

            prnWriter.Write (bufStd, 0, indStd);
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
                                             Int32 indxPaperSize,
                                             Int32 indxPaperType,
                                             Int32 indxOrientation,
                                             Int32 indxPalette,
                                             Int32 rgbClrD1,
                                             Int32 rgbClrD2,
                                             Int32 rgbClrS1,
                                             Int32 rgbClrS2,
                                             Int32 rgbClrT1,
                                             Int32 rgbClrT2,
                                             Int32 rgbClrBlack,
                                             Int32 rgbClrWhite,
                                             Byte  idClrS1,
                                             Byte  idClrS2,
                                             Byte  idClrT1,
                                             Byte  idClrT2,
                                             String nameClrD1,
                                             String nameClrD2,
                                             String nameClrS1,
                                             String nameClrS2,
                                             String nameClrT1,
                                             String nameClrT2,
                                             Int32 minROP,
                                             Int32 maxROP,
                                             Boolean flagOptColour,
                                             Boolean flagUseMacros,
                                             Boolean flagSrcTextPat)
        {
            const Byte defaultROP = 252;

            Int32 paletteDepth = PCLXLPalettes.getCtClrItems (indxPalette);

            Int32 paletteSize = _compCtRGB * paletteDepth;

            Byte [] paletteRGB = new Byte [paletteSize];

            if (flagOptColour)
                defineRGBPalette (indxPalette, paletteDepth, ref paletteRGB);

            generatePage_1 (prnWriter,
                            indxPaperSize,
                            indxPaperType,
                            indxOrientation,
                            indxPalette,
                            defaultROP,
                            (Byte) minROP,
                            (Byte) maxROP,
                            rgbClrD1,
                            rgbClrD2,
                            rgbClrS1,
                            rgbClrS2,
                            rgbClrT1,
                            rgbClrT2,
                            rgbClrBlack,
                            rgbClrWhite,
                            idClrS1,
                            idClrS2,
                            idClrT1,
                            idClrT2,
                            nameClrD1,
                            nameClrD2,
                            nameClrS1,
                            nameClrS2,
                            nameClrT1,
                            nameClrT2,
                            paletteRGB,
                            flagOptColour,
                            flagUseMacros,
                            flagSrcTextPat);

            for (Int32 i = minROP; i < maxROP; i += 8)
            {
                generatePage_n (prnWriter,
                                indxPaperSize,
                                indxOrientation,
                                (Byte) i,
                                rgbClrD1,
                                rgbClrD2,
                                rgbClrS1,
                                rgbClrS2,
                                rgbClrBlack,
                                rgbClrWhite,
                                idClrS1,
                                idClrS2,
                                idClrT1,
                                idClrT2,
                                paletteRGB,
                                flagOptColour,
                                flagUseMacros,
                                flagSrcTextPat);
            }
        }
 
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e D e s t B o x                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write sequences (either directly, or as a part of a stream         //
        // definition) for the 'destination' box.                             //
        //                                                                    //
        // Note that the position of the box is set relative to the current   //
        // origin (set before calling this function), because:                //
        //  - PCL XL rectangle definitions always use absolute coordinates;   //
        //    these values are stored when defining the stream;               //
        //  - Parameters cannot be passed when executing the stored stream.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void writeDestBox (BinaryWriter prnWriter,
                                          Int32        rgbClrD1,
                                          Int32        rgbClrD2,
                                          Int32        rgbClrBlack,
                                          Boolean      flagOptColour,
                                          Boolean      flagUseMacros)
        {
            const UInt16 halfBox = _destBoxSide / 2;
            const Int32 sizeRGB = 3;
            const Int32 lenBuf = 1024;

            Byte[] buffer = new Byte[lenBuf];

            Int32 indBuf;

            UInt16 posX = 0;
            UInt16 posY = 0;

            indBuf = 0;

            //----------------------------------------------------------------//
            //                                                                //
            // Colour 1.                                                      //
            //                                                                //
            //----------------------------------------------------------------//

            if (flagOptColour)
            {
                Byte[] rgb = { 0, 0, 0 };

                rgb[0] = (Byte) ((rgbClrD1 & 0xff0000) >> 16);
                rgb[1] = (Byte) ((rgbClrD1 & 0x00ff00) >> 8);
                rgb[2] = (Byte)  (rgbClrD1 & 0x0000ff);

                PCLXLWriter.addAttrUbyteArray (ref buffer,
                                   ref indBuf,
                                   PCLXLAttributes.eTag.RGBColor,
                                   sizeRGB,
                                   rgb);
            }
            else
            {
                Byte grayLevel = (Byte)  (rgbClrD1 & 0x0000ff);

                PCLXLWriter.addAttrUbyte (ref buffer,
                                   ref indBuf,
                                   PCLXLAttributes.eTag.GrayLevel,
                                   grayLevel);
            }

            PCLXLWriter.addOperator (ref buffer,
                              ref indBuf,
                              PCLXLOperators.eTag.SetBrushSource);

            PCLXLWriter.addAttrUbyte (ref buffer,
                               ref indBuf,
                               PCLXLAttributes.eTag.NullPen,
                               0);

            PCLXLWriter.addOperator (ref buffer,
                              ref indBuf,
                              PCLXLOperators.eTag.SetPenSource);

            PCLXLWriter.writeStreamBlock (prnWriter, flagUseMacros,
                                   buffer, ref indBuf);

            posX = 0; // relative to current origin
            posY = 0; // relative to current origin

            PCLXLWriter.rectangle (prnWriter, flagUseMacros, posX, posY,
                                   halfBox, halfBox);

            posX += halfBox;
            posY += halfBox;

            PCLXLWriter.rectangle (prnWriter, flagUseMacros, posX, posY,
                                   halfBox, halfBox);

            //----------------------------------------------------------------//
            //                                                                //
            // Colour 2.                                                      //
            //                                                                //
            //----------------------------------------------------------------//

            if (flagOptColour)
            {
                Byte[] rgb = { 0, 0, 0 };

                rgb[0] = (Byte) ((rgbClrD2 & 0xff0000) >> 16);
                rgb[1] = (Byte) ((rgbClrD2 & 0x00ff00) >> 8);
                rgb[2] = (Byte)  (rgbClrD2 & 0x0000ff);

                PCLXLWriter.addAttrUbyteArray (ref buffer,
                                   ref indBuf,
                                   PCLXLAttributes.eTag.RGBColor,
                                   sizeRGB,
                                   rgb);
            }
            else
            {
                Byte grayLevel = (Byte)  (rgbClrD2 & 0x0000ff);

                PCLXLWriter.addAttrUbyte (ref buffer,
                                   ref indBuf,
                                   PCLXLAttributes.eTag.GrayLevel,
                                   grayLevel);
            }

            PCLXLWriter.addOperator (ref buffer,
                              ref indBuf,
                              PCLXLOperators.eTag.SetBrushSource);

            PCLXLWriter.writeStreamBlock (prnWriter, flagUseMacros,
                                   buffer, ref indBuf);

            posX = 0;       // relative to current origin
            posY = halfBox; // relative to current origin

            PCLXLWriter.rectangle (prnWriter, flagUseMacros, posX, posY,
                                   halfBox, halfBox);

            posX += halfBox;
            posY -= halfBox;

            PCLXLWriter.rectangle (prnWriter, flagUseMacros, posX, posY,
                                   halfBox, halfBox);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e D e s t B o x P a g e                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write sequences for 8 rows of the 4 'destination' boxes, plus      //
        // column headings.                                                   //
        //                                                                    //
        // Note that the position of the rows are set relative to the current //
        // origin (set before calling this function), because:                //
        //  - PCL XL rectangle definitions always use absolute coordinates;   //
        //    these values are stored when the lower-level stream is defined; //
        //  - Parameters cannot be passed when executing the stored stream.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void writeDestBoxPage (BinaryWriter prnWriter,
                                              Int32        rgbClrD1,
                                              Int32        rgbClrD2,
                                              Int32        rgbClrBlack,
                                              Boolean      flagOptColour,
                                              Boolean      flagUseMacros)
        {
            const String streamId = _streamIdDestBoxPage;
            const Int32 lenBuf = 64;

            Byte[] buffer = new Byte[lenBuf];

            Int32 indBuf;

            Int16 posX = 0;
            Int16 posY = 0;

            indBuf = 0;

            //----------------------------------------------------------------//
            //                                                                //
            // Stream header                                                  //
            //                                                                //
            //----------------------------------------------------------------//

            if (flagUseMacros)
            {
                PCLXLWriter.streamHeader (prnWriter, true, streamId);
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Column headings.                                               //
            //                                                                //
            //----------------------------------------------------------------//

            posX = _posXPage_n_Hddr;
            posY = _posYPage_n_Hddr;

            PCLXLWriter.addOperator(ref buffer, ref indBuf,
                                    PCLXLOperators.eTag.PushGS);

            PCLXLWriter.addAttrSint16XY(ref buffer, ref indBuf,
                                        PCLXLAttributes.eTag.PageOrigin,
                                        posX, posY);

            PCLXLWriter.addOperator(ref buffer, ref indBuf,
                                    PCLXLOperators.eTag.SetPageOrigin);

            PCLXLWriter.writeStreamBlock (prnWriter, flagUseMacros,
                                          buffer, ref indBuf);

            writeDestBoxRowHddr(prnWriter, flagUseMacros);

            //----------------------------------------------------------------//
            //                                                                //
            // Boxes.                                                         //
            //                                                                //
            //----------------------------------------------------------------//

            PCLXLWriter.addAttrSint16XY (ref buffer, ref indBuf,
                                         PCLXLAttributes.eTag.PageOrigin,
                                         _colInc,
                                         _posYPage_n_Data - _posYPage_n_Hddr);

            PCLXLWriter.addOperator (ref buffer, ref indBuf,
                                     PCLXLOperators.eTag.SetPageOrigin);


            PCLXLWriter.writeStreamBlock (prnWriter, flagUseMacros,
                                          buffer, ref indBuf);

            for (Int32 i = 0; i < 8; i++)
            {
                PCLXLWriter.addOperator (ref buffer, ref indBuf,
                                         PCLXLOperators.eTag.PushGS);

                PCLXLWriter.writeStreamBlock (prnWriter, flagUseMacros,
                                       buffer, ref indBuf);

                writeDestBoxRow (prnWriter, rgbClrD1, rgbClrD2, rgbClrBlack,
                                 flagOptColour, flagUseMacros);

                PCLXLWriter.addOperator (ref buffer, ref indBuf,
                                         PCLXLOperators.eTag.PopGS);
                
                PCLXLWriter.addAttrSint16XY(ref buffer, ref indBuf,
                                            PCLXLAttributes.eTag.PageOrigin,
                                            0, _rowInc);

                PCLXLWriter.addOperator (ref buffer, ref indBuf,
                                         PCLXLOperators.eTag.SetPageOrigin);


                PCLXLWriter.writeStreamBlock (prnWriter, flagUseMacros,
                                              buffer, ref indBuf);
            }

            PCLXLWriter.addOperator (ref buffer, ref indBuf,
                                     PCLXLOperators.eTag.PopGS);


            PCLXLWriter.writeStreamBlock (prnWriter, flagUseMacros,
                                          buffer, ref indBuf);

            //----------------------------------------------------------------//
            //                                                                //
            // Stream end.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            if (flagUseMacros)
            {
                PCLXLWriter.streamEnd (prnWriter);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e D e s t B o x R o w                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write sequences for row of 4 'destination' boxes.                  //
        //                                                                    //
        // Note that the position of the row is set relative to the current   //
        // origin (set before calling this function), because:                //
        //  - PCL XL rectangle definitions always use absolute coordinates;   //
        //    these values are stored when the lower-level stream is defined; //
        //  - Parameters cannot be passed when executing the stored stream.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void writeDestBoxRow (BinaryWriter prnWriter,
                                             Int32        rgbClrD1,
                                             Int32        rgbClrD2,
                                             Int32        rgbClrBlack,
                                             Boolean      flagOptColour,
                                             Boolean      flagUseMacros)
        {
            const Int32 lenBuf = 64;

            Byte[] buffer = new Byte[lenBuf];

            Int32 indBuf;

            Int16 posX = 0;
            Int16 posY = 0;

            indBuf = 0;

            //----------------------------------------------------------------//
            //                                                                //
            // Boxes.                                                         //
            //                                                                //
            //----------------------------------------------------------------//

            posX = _colInc;
            posY = 0;

            for (Int32 i = 0; i < 4; i++)
            {
                writeDestBox (prnWriter, rgbClrD1, rgbClrD2, rgbClrBlack,
                              flagOptColour, flagUseMacros);

                PCLXLWriter.addAttrSint16XY (ref buffer, ref indBuf,
                                             PCLXLAttributes.eTag.PageOrigin,
                                             posX, posY);

                PCLXLWriter.addOperator (ref buffer, ref indBuf,
                                         PCLXLOperators.eTag.SetPageOrigin);

                PCLXLWriter.writeStreamBlock (prnWriter, flagUseMacros,
                                              buffer, ref indBuf);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e D e s t B o x R o w H d d r                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write sequences (either directly, or as a part of a stream         //
        // definition) for column headers for samples.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void writeDestBoxRowHddr (BinaryWriter prnWriter,
                                                 Boolean      flagUseMacros)
        {
            Int16 ptSize;

            Int16 posX = 0;
            Int16 posY = 0;

            //----------------------------------------------------------------//
            //                                                                //
            // Main headings.                                                 //
            //                                                                //
            //----------------------------------------------------------------//

            ptSize = 10;

            PCLXLWriter.font (prnWriter, flagUseMacros, ptSize, _symSet_19U,
                              _nameFontArialRegular);

            //----------------------------------------------------------------//

            posX = 0;   // relative to current origin
            posY = 0;   // relative to current origin

            PCLXLWriter.text(prnWriter, flagUseMacros, false,
                       PCLXLWriter.advances_ArialRegular, ptSize,
                       posX, posY,
                       "ROP");

            posX += _colInc;

            PCLXLWriter.text(prnWriter, flagUseMacros, false,
                       PCLXLWriter.advances_ArialRegular, ptSize,
                       posX, posY,
                       "Source = transparent");

            posX += _colInc * 2;

            PCLXLWriter.text(prnWriter, flagUseMacros, false,
                       PCLXLWriter.advances_ArialRegular, ptSize,
                       posX, posY,
                       "Source = opaque");

            //----------------------------------------------------------------//
            //                                                                //
            // Sub headings.                                                  //
            //                                                                //
            //----------------------------------------------------------------//

            ptSize = 8;

            PCLXLWriter.font (prnWriter, flagUseMacros, ptSize, _symSet_19U,
                              _nameFontArialRegular);

            //----------------------------------------------------------------//

            posX = 0;              // relative to current origin
            posY = _incInch / 6;   // relative to current origin

            for (Int32 i = 0; i < 2; i++)
            {
                posX += _colInc;

                PCLXLWriter.text(prnWriter, flagUseMacros, false,
                           PCLXLWriter.advances_ArialRegular, ptSize,
                           posX, posY,
                           "Pattern=transparent");

                posX += _colInc;

                PCLXLWriter.text(prnWriter, flagUseMacros, false,
                           PCLXLWriter.advances_ArialRegular, ptSize,
                           posX, posY,
                           "Pattern=opaque");
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
                                          Int16        patternId,
                                          Byte         idClrT1,
                                          Byte         idClrT2,
                                          Boolean      flagOptColour)
        {
            const Int32 bitsPerByte = 8;

            const UInt16 patWidth  = 16; // multiple of 8
            const UInt16 patHeight = 16; // multiple of 8
            const Int32  rowBytes  = patWidth / 8;
            const Int32  rowCt     = patHeight;

            const UInt16 destWidth =
                (UInt16) ((patWidth  * _unitsPerInch) / 300);
            const UInt16 destHeight =
                (UInt16) ((patHeight * _unitsPerInch) / 300);

            Byte [] mask = { 0xC0, 0x01,      // row 00
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

            UInt16 startLine = 0;
            UInt16 blockHeight;

            Int32 blockSize = 0;
            Int32 bitsPerPixel;

            //----------------------------------------------------------------//
            //                                                                //
            // Image definition                                               //
            //                                                                //
            // The image is described by e1Bit values in the mask.            //
            // These are converted to:                                        //
            //  - e4Bit indices (into 16-colour palette) for eRGB.            //
            //  - e8Bit direct intensity values for          eGray.           //
            //                                                                //
            //----------------------------------------------------------------//

            if (flagOptColour)
            {
                bitsPerPixel = 4;   // e4bit palette indices

                PCLXLWriter.patternBegin (prnWriter,
                                          false,
                                          _patternId,
                                          patWidth,
                                          patHeight,
                                          destWidth,
                                          destHeight,
                                          PCLXLAttrEnums.eVal.eIndexedPixel,
                                          PCLXLAttrEnums.eVal.e4Bit,
                                          PCLXLAttrEnums.eVal.eSessionPattern,
                                          PCLXLAttrEnums.eVal.eNoCompression);
            }
            else
            {
                bitsPerPixel = 8;   // e8bit direct values

                PCLXLWriter.patternBegin (prnWriter,
                                          false,
                                          _patternId,
                                          patWidth,
                                          patHeight,
                                          destWidth,
                                          destHeight,
                                          PCLXLAttrEnums.eVal.eDirectPixel,
                                          PCLXLAttrEnums.eVal.e8Bit,
                                          PCLXLAttrEnums.eVal.eSessionPattern,
                                          PCLXLAttrEnums.eVal.eNoCompression);
            }

            //------------------------------------------------------------//

            startLine = 0;

            Byte [] block;

            blockHeight = (UInt16) rowCt;
            blockSize = rowBytes * bitsPerPixel * rowCt;
            block = new Byte [blockSize];

            for (Int32 row = 0; row < rowCt; row++)
            {
                Byte maskByte;
                Byte dataByte;

                Int32 rowStart   = row * rowBytes;
                Int32 blockStart = rowStart * bitsPerPixel;
                Int32 offsetA,
                      offsetB;

                for (Int32 maskByteNo = 0;
                           maskByteNo < rowBytes;
                           maskByteNo++)
                {
                    //----------------------------------------------------//
                    //                                                    //
                    // Convert mask bits to:                              //
                    //  - eRGB  pairs of e4Bit colour indices             //
                    //  - eGray single   e8Bit grey levels                //
                    //                                                    //
                    //----------------------------------------------------//

                    maskByte = mask [rowStart + maskByteNo];
                    offsetA  = maskByteNo * bitsPerPixel;

                    for (Int32 i = 0; i < bitsPerByte; i++)
                    {
                        if ((maskByte & 0x80) == 0)
                            dataByte = idClrT2;
                        else
                            dataByte = idClrT1;

                        if (bitsPerPixel == 4)
                        {
                            i++;
                            offsetB = (i / 2);

                            maskByte = (Byte) (maskByte << 1);
                            dataByte = (Byte) (dataByte << bitsPerPixel);

                            if ((maskByte & 0x80) == 0)
                                dataByte += idClrT2;
                            else
                                dataByte += idClrT1;
                        }
                        else
                        {
                            offsetB = i;
                        }

                        maskByte = (Byte) (maskByte << 1);

                        block [blockStart + offsetA + offsetB] = dataByte;
                    }
                }
            }

            PCLXLWriter.patternRead (prnWriter,
                                     false,
                                     startLine,
                                     blockHeight,
                                     PCLXLAttrEnums.eVal.eNoCompression,
                                     block);

            //----------------------------------------------------------------//

            PCLXLWriter.patternEnd (prnWriter, false);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e S r c B o x                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write sequences (either directly, or as a part of a stream         //
        // definition) for 'source' image.                                    //
        //                                                                    //
        // Note that (unlike the PCL equivalent) this AND the lower-level     //
        // methdods are written as ONE (rather than separate) streams; this   //
        // is because:                                                        //
        //  - PCL XL Class/Revision 2.0 does not support nested streams;      //
        //  - Even with a C/R 2.1 stream sent to a local LJ M475dn, nested    //
        //    streams produce a PCL XL error, so I've no way of testing.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void writeSrcBox (BinaryWriter prnWriter,
                                         Int32        rgbClrS1,
                                         Int32        rgbClrS2,
                                         Int32        rgbClrBlack,
                                         Byte         idClrS1,
                                         Byte         idClrS2,
                                         Boolean      flagOptColour,
                                         Boolean      flagUseMacros,
                                         Boolean      flagSrcTextPat)
        {
            writeSrcBoxText (prnWriter, rgbClrS1, rgbClrS2, rgbClrBlack,
                             flagOptColour, flagUseMacros, flagSrcTextPat);

            writeSrcBoxRasters (prnWriter, idClrS1, idClrS2,
                                flagOptColour, flagUseMacros);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e S r c B o x R a s t e r                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write sequences (either directly, or as a part of a stream         //
        // definition) for 'source' raster image.                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void writeSrcBoxRaster (BinaryWriter prnWriter,
                                               Byte         idClrS1,
                                               Byte         idClrS2,
                                               Boolean      inverse,
                                               Boolean      flagOptColour,
                                               Boolean      flagUseMacros)
        {
            const Int32 bitsPerByte = 8;

            const Int16 blockCt = 7;    // A + B + C + D + C + B + A
            const Int16 rowCtA  = 2;
            const Int16 rowCtB  = 2;
            const Int16 rowCtC  = 4;
            const Int16 rowCtD  = 8;

            const UInt16 rowCt  = rowCtA + rowCtB + rowCtC + rowCtD +
                                  rowCtC + rowCtB + rowCtA;
            const UInt16 colCt  = 24;

            const UInt16 destWidth  = (UInt16) ((colCt * 8 * _unitsPerInch) / 600);
            const UInt16 destHeight = (UInt16) ((rowCt * 8 * _unitsPerInch) / 600);

            UInt16 startLine = 0;
            UInt16 blockHeight;

            Int32 blockSize = 0;
            Int32 bitsPerPixel;

            Int16 rowCtCrnt;
            Int32 maskLen;

            Byte [] maskRowAPos = { 0xf0, 0xff, 0x0f };
            Byte [] maskRowANeg = { 0x0f, 0x00, 0xf0 };

            Byte [] maskRowBPos = { 0xc0, 0x00, 0x03 };
            Byte [] maskRowBNeg = { 0x3f, 0xff, 0xfc };

            Byte [] maskRowCPos = { 0x0f, 0x00, 0xf0 };
            Byte [] maskRowCNeg = { 0xf0, 0xff, 0x0f };

            Byte [] maskRowDPos = { 0xc0, 0xff, 0x03 };
            Byte [] maskRowDNeg = { 0x3f, 0x00, 0xfc };

            Byte [] maskRowCrnt;

            //----------------------------------------------------------------//
            //                                                                //
            // Image definition                                               //
            //                                                                //
            // The image is described by e1Bit values in the mask.            //
            // These are converted to:                                        //
            //  - e4Bit indices (into 16-colour palette) for eRGB.            //
            //  - e8Bit direct intensity values for          eGray.           //
            //                                                                //
            //----------------------------------------------------------------//

            if (flagOptColour)
            {
                bitsPerPixel = 4;   // e4bit palette indices

                PCLXLWriter.imageBegin (prnWriter,
                                       flagUseMacros,
                                       colCt,
                                       rowCt,
                                       destWidth,
                                       destHeight,
                                       PCLXLAttrEnums.eVal.eIndexedPixel,
                                       PCLXLAttrEnums.eVal.e4Bit);
            }
            else
            {
                bitsPerPixel = 8;   // e8bit direct values

                PCLXLWriter.imageBegin (prnWriter,
                                       flagUseMacros,
                                       colCt,
                                       rowCt,
                                       destWidth,
                                       destHeight,
                                       PCLXLAttrEnums.eVal.eDirectPixel,
                                       PCLXLAttrEnums.eVal.e8Bit);
            }

            //------------------------------------------------------------//

            startLine = 0;

            for (Int32 blockNo = 0; blockNo < blockCt; blockNo++)
            {
                Byte [] block;

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

                maskLen = maskRowCrnt.Length;
                blockHeight = (UInt16) rowCtCrnt;


                blockSize = maskLen * bitsPerPixel * blockHeight;
                block = new Byte [blockSize];

                for (Int32 row = 0; row < rowCtCrnt; row++)
                {
                    Byte maskByte;
                    Byte dataByte;

                    Int32 rowStart = row * maskLen * bitsPerPixel;
                    Int32 offsetA,
                          offsetB;

                    for (Int32 maskByteNo = 0;
                               maskByteNo < maskLen;
                               maskByteNo++)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Convert mask bits to:                              //
                        //  - eRGB  pairs of e4Bit colour indices             //
                        //  - eGray single   e8Bit grey levels                //
                        //                                                    //
                        //----------------------------------------------------//

                        maskByte = maskRowCrnt [maskByteNo];
                        offsetA = maskByteNo * bitsPerPixel;

                        for (Int32 i = 0; i < bitsPerByte; i++)
                        {
                            if ((maskByte & 0x80) == 0)
                                dataByte = idClrS2;
                            else
                                dataByte = idClrS1;

                            if (bitsPerPixel == 4)
                            {
                                i++;
                                offsetB = (i / 2);

                                maskByte = (Byte) (maskByte << 1);
                                dataByte = (Byte) (dataByte << bitsPerPixel);

                                if ((maskByte & 0x80) == 0)
                                    dataByte += idClrS2;
                                else
                                    dataByte += idClrS1;
                            }
                            else
                            {
                                offsetB = i;
                            }

                            maskByte = (Byte) (maskByte << 1);

                            block [rowStart + offsetA + offsetB] = dataByte;
                        }
                    }
                }

                PCLXLWriter.imageRead (prnWriter,
                                       flagUseMacros,
                                       startLine,
                                       blockHeight,
                                       PCLXLAttrEnums.eVal.eNoCompression,
                                       block);

                startLine = (UInt16) (startLine + rowCtCrnt);
            }

            //----------------------------------------------------------------//

            PCLXLWriter.imageEnd (prnWriter, flagUseMacros);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e S r c B o x R a s t e r s                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write sequences (either directly, or as a part of a stream         //
        // definition) for 'source' raster images.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void writeSrcBoxRasters (BinaryWriter prnWriter,
                                                Byte         idClrS1,
                                                Byte         idClrS2,
                                                Boolean      flagOptColour,
                                                Boolean      flagUseMacros)
        {
            const Int32 lenBuf = 64;

            Byte [] buffer = new Byte [lenBuf];

            Int32 indBuf;

            indBuf = 0;

            PCLXLWriter.addOperator (ref buffer,
                                     ref indBuf,
                                     PCLXLOperators.eTag.PushGS);

            PCLXLWriter.writeStreamBlock (prnWriter, flagUseMacros,
                                          buffer, ref indBuf);

            writeSrcBoxRaster (prnWriter, idClrS1, idClrS2,
                               false, flagOptColour, flagUseMacros);

            PCLXLWriter.addAttrSint16XY (ref buffer,
                                         ref indBuf,
                                         PCLXLAttributes.eTag.Point,
                                         _destBoxSide / 2, 0);

            PCLXLWriter.addOperator (ref buffer,
                                     ref indBuf,
                                     PCLXLOperators.eTag.SetCursorRel);

            PCLXLWriter.writeStreamBlock (prnWriter, flagUseMacros,
                                          buffer, ref indBuf);

            writeSrcBoxRaster (prnWriter, idClrS1, idClrS2,
                               true, flagOptColour, flagUseMacros);

            //----------------------------------------------------------------//

            PCLXLWriter.addOperator (ref buffer,
                                     ref indBuf,
                                     PCLXLOperators.eTag.PopGS);

            PCLXLWriter.writeStreamBlock (prnWriter, flagUseMacros,
                                          buffer, ref indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e S r c B o x R o w                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write sequences (either directly, or as a stream definition) for   //
        // 'source' image.                                                    //
        //                                                                    //
        // Note that (unlike the PCL equivalent) this AND the lower-level     //
        // methdods are written as ONE (rather than separate) streams; this   //
        // is because:                                                        //
        //  - PCL XL Class/Revision 2.0 does not support nested streams;      //
        //  - Even with a C/R 2.1 stream sent to a local LJ M475dn, nested    //
        //    streams produce a PCL XL error, so I've no way of testing.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void writeSrcBoxRow (BinaryWriter prnWriter,
                                            Int32        rgbClrS1,
                                            Int32        rgbClrS2,
                                            Int32        rgbClrBlack,
                                            Byte         idClrS1,
                                            Byte         idClrS2,
                                            Boolean      flagOptColour,
                                            Boolean      flagUseMacros,
                                            Boolean      flagSrcTextPat)
        {
            const String streamId = _streamIdSrcBoxRow;
            const Int32 lenBuf = 64;

            Byte [] buffer = new Byte [lenBuf];

            Int32 indBuf;

            indBuf = 0;

            //----------------------------------------------------------------//
            //                                                                //
            // Stream header                                                  //
            //                                                                //
            //----------------------------------------------------------------//

            if (flagUseMacros)
            {
                PCLXLWriter.streamHeader (prnWriter, true, streamId);
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Body                                                           //
            //                                                                //
            //----------------------------------------------------------------//

            PCLXLWriter.addAttrUbyte (ref buffer, ref indBuf,
                                      PCLXLAttributes.eTag.TxMode,
                                      (Byte) PCLXLAttrEnums.eVal.eTransparent);

            PCLXLWriter.addOperator (ref buffer, ref indBuf,
                                     PCLXLOperators.eTag.SetSourceTxMode);

            PCLXLWriter.addAttrUbyte (ref buffer, ref indBuf,
                                      PCLXLAttributes.eTag.TxMode,
                                      (Byte) PCLXLAttrEnums.eVal.eTransparent);

            PCLXLWriter.addOperator (ref buffer, ref indBuf,
                                     PCLXLOperators.eTag.SetPatternTxMode);

            PCLXLWriter.writeStreamBlock (prnWriter, flagUseMacros,
                                          buffer, ref indBuf);

            writeSrcBox (prnWriter, rgbClrS1, rgbClrS2, rgbClrBlack,
                         idClrS1, idClrS2,
                         flagOptColour, flagUseMacros, flagSrcTextPat);

            //----------------------------------------------------------------//

            PCLXLWriter.addAttrSint16XY (ref buffer, ref indBuf,
                                         PCLXLAttributes.eTag.Point,
                                         _colInc, 0);

            PCLXLWriter.addOperator (ref buffer, ref indBuf,
                                     PCLXLOperators.eTag.SetCursorRel);

            PCLXLWriter.addAttrUbyte (ref buffer, ref indBuf,
                                      PCLXLAttributes.eTag.TxMode,
                                      (Byte) PCLXLAttrEnums.eVal.eOpaque);

            PCLXLWriter.addOperator (ref buffer, ref indBuf,
                                     PCLXLOperators.eTag.SetPatternTxMode);

            PCLXLWriter.writeStreamBlock (prnWriter, flagUseMacros,
                                          buffer, ref indBuf);

            writeSrcBox (prnWriter, rgbClrS1, rgbClrS2, rgbClrBlack,
                         idClrS1, idClrS2,
                         flagOptColour, flagUseMacros, flagSrcTextPat);

            //----------------------------------------------------------------//

            PCLXLWriter.addAttrSint16XY (ref buffer, ref indBuf,
                                         PCLXLAttributes.eTag.Point,
                                         _colInc, 0);

            PCLXLWriter.addOperator (ref buffer, ref indBuf,
                                     PCLXLOperators.eTag.SetCursorRel);

            PCLXLWriter.addAttrUbyte (ref buffer, ref indBuf,
                                      PCLXLAttributes.eTag.TxMode,
                                      (Byte) PCLXLAttrEnums.eVal.eOpaque);

            PCLXLWriter.addOperator (ref buffer, ref indBuf,
                                     PCLXLOperators.eTag.SetSourceTxMode);

            PCLXLWriter.addAttrUbyte (ref buffer, ref indBuf,
                                      PCLXLAttributes.eTag.TxMode,
                                      (Byte) PCLXLAttrEnums.eVal.eTransparent);

            PCLXLWriter.addOperator (ref buffer, ref indBuf,
                                     PCLXLOperators.eTag.SetPatternTxMode);

            PCLXLWriter.writeStreamBlock (prnWriter, flagUseMacros,
                                          buffer, ref indBuf);

            writeSrcBox (prnWriter, rgbClrS1, rgbClrS2, rgbClrBlack,
                         idClrS1, idClrS2,
                         flagOptColour, flagUseMacros, flagSrcTextPat);

            //----------------------------------------------------------------//

            PCLXLWriter.addAttrSint16XY (ref buffer, ref indBuf,
                                         PCLXLAttributes.eTag.Point,
                                         _colInc, 0);

            PCLXLWriter.addOperator (ref buffer, ref indBuf,
                                     PCLXLOperators.eTag.SetCursorRel);

            PCLXLWriter.addAttrUbyte (ref buffer, ref indBuf,
                                      PCLXLAttributes.eTag.TxMode,
                                      (Byte) PCLXLAttrEnums.eVal.eOpaque);

            PCLXLWriter.addOperator (ref buffer, ref indBuf,
                                     PCLXLOperators.eTag.SetPatternTxMode);

            PCLXLWriter.writeStreamBlock (prnWriter, flagUseMacros,
                                          buffer, ref indBuf);

            writeSrcBox (prnWriter, rgbClrS1, rgbClrS2, rgbClrBlack,
                         idClrS1, idClrS2,
                         flagOptColour, flagUseMacros, flagSrcTextPat);

            //----------------------------------------------------------------//
            //                                                                //
            // Stream trailer.                                                //
            //                                                                //
            //----------------------------------------------------------------//

            if (flagUseMacros)
            {
                PCLXLWriter.streamEnd (prnWriter);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e S r c B o x T e x t                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write sequences (either directly, or as a part of a stream         //
        // definition) for 'source' text characters.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void writeSrcBoxText (BinaryWriter prnWriter,
                                             Int32        rgbClrS1,
                                             Int32        rgbClrS2,
                                             Int32        rgbClrBlack,
                                             Boolean      flagOptColour,
                                             Boolean      flagUseMacros,
                                             Boolean      flagSrcTextPat)
        {
            const Int32 sizeRGB = 3;
            const Int32 lenBuf = 64;

            Byte [] buffer = new Byte [lenBuf];

            Int32 indBuf;
            Int16 ptSize;

            Int16 posX = 0;

            indBuf = 0;

            PCLXLWriter.addOperator (ref buffer,
                                     ref indBuf,
                                     PCLXLOperators.eTag.PushGS);

            //----------------------------------------------------------------//

            if (flagSrcTextPat)
            {
                PCLXLWriter.addAttrSint16 (ref buffer, ref indBuf,
                                           PCLXLAttributes.eTag.PatternSelectID,
                                           _patternId);
            }
            else if (flagOptColour)
            {
                Byte [] rgb = { 0, 0, 0 };

                rgb [0] = (Byte) ((rgbClrS1 & 0xff0000) >> 16);
                rgb [1] = (Byte) ((rgbClrS1 & 0x00ff00) >> 8);
                rgb [2] = (Byte) (rgbClrS1 & 0x0000ff);

                PCLXLWriter.addAttrUbyteArray (ref buffer,
                                               ref indBuf,
                                               PCLXLAttributes.eTag.RGBColor,
                                               sizeRGB,
                                               rgb);
            }
            else
            {
                Byte grayLevel = (Byte) (rgbClrS1 & 0x0000ff);

                PCLXLWriter.addAttrUbyte (ref buffer,
                                          ref indBuf,
                                          PCLXLAttributes.eTag.GrayLevel,
                                          grayLevel);
            }

            PCLXLWriter.addOperator (ref buffer,
                                     ref indBuf,
                                     PCLXLOperators.eTag.SetBrushSource);

            PCLXLWriter.addAttrUbyte (ref buffer,
                                      ref indBuf,
                                      PCLXLAttributes.eTag.NullPen,
                                      0);

            PCLXLWriter.addOperator (ref buffer,
                                     ref indBuf,
                                     PCLXLOperators.eTag.SetPenSource);

            PCLXLWriter.writeStreamBlock (prnWriter, flagUseMacros,
                                          buffer, ref indBuf);

            //----------------------------------------------------------------//

            ptSize = 28;

            PCLXLWriter.text (prnWriter, flagUseMacros, true,
                              PCLXLWriter.advances_ArialRegular, ptSize,
                              (_destBoxSide / 4), 0,
                              "O");

            //----------------------------------------------------------------//

            if (flagSrcTextPat)
            {
                PCLXLWriter.addAttrSint16 (ref buffer, ref indBuf,
                                           PCLXLAttributes.eTag.PatternSelectID,
                                           _patternId);
            }
            else if (flagOptColour)
            {
                Byte [] rgb = { 0, 0, 0 };

                rgb [0] = (Byte) ((rgbClrS2 & 0xff0000) >> 16);
                rgb [1] = (Byte) ((rgbClrS2 & 0x00ff00) >> 8);
                rgb [2] = (Byte) (rgbClrS2 & 0x0000ff);

                PCLXLWriter.addAttrUbyteArray (ref buffer,
                                               ref indBuf,
                                               PCLXLAttributes.eTag.RGBColor,
                                               sizeRGB,
                                               rgb);
            }
            else
            {
                Byte grayLevel = (Byte) (rgbClrS2 & 0x0000ff);

                PCLXLWriter.addAttrUbyte (ref buffer,
                                          ref indBuf,
                                          PCLXLAttributes.eTag.GrayLevel,
                                          grayLevel);
            }

            PCLXLWriter.addOperator (ref buffer,
                                     ref indBuf,
                                     PCLXLOperators.eTag.SetBrushSource);

            PCLXLWriter.writeStreamBlock (prnWriter, flagUseMacros,
                                          buffer, ref indBuf);

            //----------------------------------------------------------------//

            posX = PCLXLWriter.textAdvance (PCLXLWriter.advances_ArialRegular,
                                            ptSize,
                                            "O");

            PCLXLWriter.text (prnWriter, flagUseMacros, true,
                              PCLXLWriter.advances_ArialRegular, ptSize,
                              (Int16) (-posX), (_destBoxSide * 5) / 8,
                              "O");

            //----------------------------------------------------------------//

            PCLXLWriter.addOperator (ref buffer,
                                     ref indBuf,
                                     PCLXLOperators.eTag.PopGS);

            PCLXLWriter.writeStreamBlock (prnWriter, flagUseMacros,
                                          buffer, ref indBuf);
        }
    }
}
