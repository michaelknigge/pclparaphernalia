using System;
using System.IO;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides PCL support for the TrayMap tool.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class ToolTrayMapPCL
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const Int32 _macroIdBaseFront    = 1;
        const Int32 _macroIdBaseRear     = 11;
        const Int32 _noForm              = -1;

        const Int32 _trayIdAutoSelectPCL = 7;

        const UInt16 _unitsPerInch = PCLWriter.sessionUPI;

        const Int16 _posXName    = (_unitsPerInch * 1);
        const Int16 _posXValue   = (_unitsPerInch * 7) / 2;
        const Int16 _posXIncSub  = (_unitsPerInch / 3);

        const Int16 _posYHddr    = (_unitsPerInch * 1);
        const Int16 _posYDesc    = (_unitsPerInch * 21) / 10;
        const Int16 _posYIncMain = (_unitsPerInch * 3) / 4;
        const Int16 _posYIncSub  = (_unitsPerInch / 3);

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static Int32 _logPageOffset;

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e J o b                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate test data.                                                //
        //                                                                    //
        // Most sequences are built up as (Unicode) strings, then converted   //
        // to byte arrays before writing out - this works OK because all the  //
        // characters we're using are within the ASCII range (0x00-0x7f) and  //
        // are hence represented using a single byte in the UTF-8 encoding.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void generateJob(BinaryWriter prnWriter,
                                       Int32        pageCount,
                                       Int32[]      indxPaperSize,
                                       Int32[]      indxPaperType,
                                       Int32[]      indxPaperTray,
                                       Int32[]      indxPlexMode,
                                       Int32[]      indxOrientFront,
                                       Int32[]      indxOrientRear,
                                       Boolean      formAsMacro)
        {
            Int32[] indxFormsFront = new Int32[pageCount];
            Int32[] indxFormsRear  = new Int32[pageCount];

            Int16[] macroIdsFront = new Int16[pageCount];
            Int16[] macroIdsRear  = new Int16[pageCount];

            Single[] scaleFactors = new Single[pageCount];

            Int32 formCountFront = 0;
            Int32 formCountRear = 0;

            //----------------------------------------------------------------//
            //                                                                //
            // Set up the scaling data for each sheet, relative to the A4     //
            // paper size dimensions.                                         //
            //                                                                //
            //----------------------------------------------------------------//

            Single A4LengthPort =
                (Single)PCLPaperSizes.getPaperLength(
                            (Int32)PCLPaperSizes.eIndex.ISO_A4,
                            _unitsPerInch,
                            PCLOrientations.eAspect.Portrait);

            for (Int32 i = 0; i < pageCount; i++)
            {
                scaleFactors[i] = (Single)
                (PCLPaperSizes.getPaperLength(
                    indxPaperSize[i],
                    _unitsPerInch,
                    PCLOrientations.eAspect.Portrait) /
                 A4LengthPort);
            }

            //----------------------------------------------------------------//
            //                                                                //
            // We'll also set the logical page offset value to be used on all //
            // pages (and all overlays) from the value for the front of the   //
            // first sheet.                                                   //
            // This may be inaccurate for subsequent sheets (but only if they //
            // use different page sizes and/or orientations), but the error   //
            // will be minimal (at most, about 30 'dots', or 0.05 inch).      //
            //                                                                //
            //----------------------------------------------------------------//

            _logPageOffset = PCLPaperSizes.getLogicalOffset (
                indxPaperSize[0],
                _unitsPerInch,
                PCLOrientations.getAspect(indxOrientFront[0]));

            //----------------------------------------------------------------//
            //                                                                //
            // Generate the print job.                                        //
            //                                                                //
            //----------------------------------------------------------------//

            generateJobHeader(prnWriter);


            if (formAsMacro)
                generateOverlaySet(prnWriter,
                                   pageCount,
                                   indxPaperSize,
                                   indxPlexMode,
                                   scaleFactors,
                                   ref formCountFront,
                                   ref formCountRear,
                                   ref indxFormsFront,
                                   ref indxFormsRear,
                                   ref macroIdsFront,
                                   ref macroIdsRear);

            generatePageSet(prnWriter,
                            pageCount,
                            indxPaperSize,
                            indxPaperType,
                            indxPaperTray,
                            indxPlexMode,
                            indxOrientFront,
                            indxOrientRear,
                            indxFormsFront,
                            indxFormsRear,
                            macroIdsFront,
                            macroIdsRear,
                            scaleFactors,
                            formAsMacro);

            if (formAsMacro)
                generateOverlayDeletes (prnWriter,
                                        formCountFront, formCountRear,
                                        macroIdsFront, macroIdsRear);

            generateJobTrailer(prnWriter);
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
            PCLWriter.stdJobHeader(prnWriter, "");
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e J o b T r a i l e r                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write termination sequences to output file.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void generateJobTrailer(BinaryWriter prnWriter)
        {
            PCLWriter.stdJobTrailer(prnWriter, false, 0);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e O v e r l a y D e l e t e s                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Delete overlays.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void generateOverlayDeletes (BinaryWriter prnWriter,
                                                    Int32 formCountFront,
                                                    Int32 formCountRear,
                                                    Int16[] macroIdsFront,
                                                    Int16[] macroIdsRear)
        {
            for (Int32 i = 0; i < formCountFront; i++)
            {
                PCLWriter.macroControl (prnWriter, macroIdsFront[i],
                                        PCLWriter.eMacroControl.Delete);
            }

            for (Int32 i = 0; i < formCountRear; i++)
            {
                PCLWriter.macroControl (prnWriter, macroIdsRear[i],
                                        PCLWriter.eMacroControl.Delete);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e O v e r l a y F r o n t                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write background data sequences for front overlay to output file.  //
        // Optionally top and tail these with macro definition sequences.     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void generateOverlayFront (BinaryWriter prnWriter,
                                                  Boolean      formAsMacro,
                                                  Int16        macroId,
                                                  Single       scaleFactor)
        {
            Int16 rectHeight = (Int16)(scaleFactor * (_unitsPerInch / 2));
            Int16 rectWidth  = (Int16)(scaleFactor * ((_unitsPerInch * 7) / 2));
            Int16 rectStroke = (Int16)(scaleFactor * (_unitsPerInch / 200));

            Int32 ptSizeHddr = (Int32)(scaleFactor * 24),
                  ptSizeMain = (Int32)(scaleFactor * 18),
                  ptSizeSub  = (Int32)(scaleFactor * 8);

            Int16 posX,
                  posY,
                  posYInc;

            if (formAsMacro)
                PCLWriter.macroControl(prnWriter, macroId,
                                       PCLWriter.eMacroControl.StartDef);

            //----------------------------------------------------------------//

            posYInc = (Int16) (scaleFactor * _posYIncMain);
            posX = (Int16) ((scaleFactor * _posXName) - _logPageOffset);
            posY = (Int16) (scaleFactor * _posYHddr);

            PCLWriter.font (prnWriter, true,
                            "19U", "s1p" + ptSizeHddr + "v0s3b16602T");

            PCLWriter.text(prnWriter, posX, posY, 0, "Tray map test (PCL)");

            //----------------------------------------------------------------//

            posY = (Int16) (scaleFactor * _posYDesc);

            PCLWriter.font(prnWriter, true, "", "s" + ptSizeMain + "V");

            PCLWriter.text(prnWriter, posX, posY, 0, "Page Number:");

            posY += posYInc;
            PCLWriter.text(prnWriter, posX, posY, 0, "Paper Size:");
        
            posY += posYInc;
            PCLWriter.text(prnWriter, posX, posY, 0, "Paper Type:");
        
            posY += posYInc;
            PCLWriter.text(prnWriter, posX, posY, 0, "Plex Mode:");
        
            posY += posYInc;
            PCLWriter.text(prnWriter, posX, posY, 0, "Orientation: ");
        
            posY += posYInc;
            PCLWriter.text(prnWriter, posX, posY, 0, "PCL Tray ID:");
        
            posY += posYInc;
            PCLWriter.text(prnWriter, posX, posY, 0, "Printer Tray:");

            //----------------------------------------------------------------//

            posX = (Int16)((scaleFactor * (_posXValue + _posXIncSub)) -
                             _logPageOffset);
            posY += (Int16)(scaleFactor * _posYIncSub);

            PCLWriter.font(prnWriter, true,
                           "19U", "s1p" + ptSizeSub + "v0s3b16602T");
        
            PCLWriter.text(prnWriter, posX, posY, 0,
                      "record the tray name/number used in this box");

            //----------------------------------------------------------------//

            posX  = (Int16)(((scaleFactor * _posXValue) - _logPageOffset));
            posY -= (Int16)(scaleFactor * (_posXIncSub * 2));

            PCLWriter.rectangleOutline(prnWriter, posX, posY,
                                       rectHeight, rectWidth, rectStroke,
                                       false, false);

            //----------------------------------------------------------------//

            if (formAsMacro)
                PCLWriter.macroControl(prnWriter, macroId,
                                       PCLWriter.eMacroControl.StopDef);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e O v e r l a y R e a r                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write background data sequences fopr rear overlay to output file.  //
        // Optionally top and tail these with macro definition sequences.     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void generateOverlayRear (BinaryWriter prnWriter,
                                                  Boolean     formAsMacro,
                                                  Int16       macroId,
                                                  Single      scaleFactor)
        {
            Int16 posX,
                  posY,
                  posYInc;

            Int32 ptSizeHddr = (Int32)(scaleFactor * 24),
                  ptSizeMain = (Int32)(scaleFactor * 18);

            if (formAsMacro)
                PCLWriter.macroControl(prnWriter, macroId,
                                       PCLWriter.eMacroControl.StartDef);

            //----------------------------------------------------------------//

            posYInc = (Int16)(scaleFactor * _posYIncMain);

            posX = (Int16)((scaleFactor * _posXName) - _logPageOffset);
            posY = (Int16)(scaleFactor *_posYHddr);

            PCLWriter.font(prnWriter, true,
                            "19U", "s1p" + ptSizeHddr + "v0s3b16602T");

            PCLWriter.text(prnWriter, posX, posY, 0, "Tray map test (PCL)");

            //----------------------------------------------------------------//

            posY = (Int16)(scaleFactor * _posYDesc);

            PCLWriter.font(prnWriter, true, "", "s" + ptSizeMain + "V");

            PCLWriter.text(prnWriter, posX, posY, 0, "Page Number:");

            posY += (Int16)(posYInc * 4);

            PCLWriter.text(prnWriter, posX, posY, 0, "Orientation: ");

            //----------------------------------------------------------------//

            if (formAsMacro)
                PCLWriter.macroControl(prnWriter, macroId,
                                       PCLWriter.eMacroControl.StopDef);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e O v e r l a y S e t                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Because each sheet may be a different size, the information to be  //
        // printed may need to be scaled to fit the individual sheets, and    //
        // separate (scaled) macros may also be required.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void generateOverlaySet (BinaryWriter prnWriter,
                                               Int32        pageCount,
                                               Int32[]      indxPaperSize,
                                               Int32[]      indxPlexMode,
                                               Single[]     scaleFactors,
                                               ref Int32    formCountFront,
                                               ref Int32    formCountRear,
                                               ref Int32[]  indxFormsFront,
                                               ref Int32[]  indxFormsRear,
                                               ref Int16[]  macroIdsFront,
                                               ref Int16[]  macroIdsRear)
        {
            const Int32 noForm = -1;

            Int16 crntFormFront,
                  crntFormRear;

            Boolean[] duplexSheet = new Boolean[pageCount];

            //----------------------------------------------------------------//
            //                                                                //
            // Which sheets are duplex?.                                      //
            //                                                                //
            //----------------------------------------------------------------//

            for (Int32 i = 0; i < pageCount; i++)
            {
                if (PCLPlexModes.isSimplex(indxPlexMode[i]))
                    duplexSheet[i] = false;
                else
                    duplexSheet[i] = true;
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Establish the forms required for the front side of the sheets. //
            // A different one is required for each paper size.               //
            //                                                                //
            //----------------------------------------------------------------//
            //----------------------------------------------------------------//
            // First sheet.                                                   //
            //----------------------------------------------------------------//

            crntFormFront = 0;

            macroIdsFront[crntFormFront] =
                (Int16) (_macroIdBaseFront + crntFormFront);

            generateOverlayFront(prnWriter, true,
                                 macroIdsFront[crntFormFront],
                                  scaleFactors[0]);

            indxFormsFront[0] = crntFormFront++;

            //----------------------------------------------------------------//
            // Subsequent sheets.                                             //
            //----------------------------------------------------------------//

            for (Int32 i = 1; i < pageCount; i++)
            {
                Boolean matchFound = false;

                for (Int32 j = 0; j < i; j++)
                {
                    if (indxPaperSize[i] == indxPaperSize[j])
                    {
                        //----------------------------------------------------//
                        // Same paper size as a previous sheet.               //
                        //----------------------------------------------------//

                        matchFound = true;

                        indxFormsFront[i] = indxFormsFront[j];

                        j = i; // force end loop //
                    }
                }

                if (!matchFound)
                {
                    //----------------------------------------------------//
                    // New paper size.                                    //
                    //----------------------------------------------------//

                    macroIdsFront[crntFormFront] =
                        (Int16)(_macroIdBaseFront + crntFormFront);

                    generateOverlayFront (prnWriter, true,
                                          macroIdsFront[crntFormFront],
                                          scaleFactors[i]);

                    indxFormsFront[i] = crntFormFront++;
                }
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Establish the forms required for the rear side of the sheets.  //
            // A different one is required for each paper size.               //
            //                                                                //
            //----------------------------------------------------------------//
            //----------------------------------------------------------------//
            // First sheet.                                                   //
            //----------------------------------------------------------------//

            crntFormRear = 0;

            if (duplexSheet[0])
            {
                macroIdsRear[crntFormRear] =
                    (Int16)(_macroIdBaseRear + crntFormRear);

                generateOverlayRear (prnWriter, true,
                                     macroIdsRear[crntFormRear],
                                     scaleFactors[0]);

                indxFormsRear[0] = crntFormRear++;
            }
            else
            {
                indxFormsRear[0] = noForm;
            }

            //----------------------------------------------------------------//
            // Subsequent sheets.                                             //
            //----------------------------------------------------------------//

            for (Int32 i = 1; i < pageCount; i++)
            {
                if (!duplexSheet[i])
                {
                    indxFormsRear[i] = noForm;
                }
                else
                {
                    Boolean matchFound = false;

                    for (Int32 j = 0; j < i; j++)
                    {
                        if (indxPaperSize[i] == indxPaperSize[j] &&
                            duplexSheet[j])
                        {
                            //------------------------------------------------//
                            // Same paper size as a previous duplex sheet.    //
                            //------------------------------------------------//

                            matchFound = true;

                            indxFormsRear[i] = indxFormsRear[j];

                            j = i; // force end loop //
                        }
                    }

                    //----------------------------------------------------//
                    // New paper size.                                    //
                    //----------------------------------------------------//

                    if (!matchFound)
                    {
                        macroIdsRear[crntFormRear] =
                            (Int16)(_macroIdBaseRear + crntFormRear);

                        generateOverlayRear (prnWriter, true,
                                             macroIdsRear[crntFormRear],
                                             scaleFactors[i]);

                        indxFormsRear[i] = crntFormRear++;
                    }
                }
            }

            formCountFront = crntFormFront;
            formCountRear = crntFormRear;
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
                                         Int32        pageNo,
                                         Int32        pageCount,
                                         Int32        indxPaperSize,
                                         Int32        indxPaperType,
                                         Int32        indxPaperTray,
                                         Int32        indxPlexMode,
                                         Int32        indxOrientFront,
                                         Int32        indxOrientRear,
                                         Int16        macroIdFront,
                                         Int16        macroIdRear,
                                         Single       scaleFactor,
                                         Boolean      formAsMacro)
        {
            Int16 posX,
                  posY,
                  posYInc;

            Int32 pitchMain = (Int32)(6 / scaleFactor);

            Boolean simplex = PCLPlexModes.isSimplex(indxPlexMode);

            PCLWriter.pageHeader(prnWriter,
                                 indxPaperSize,
                                 indxPaperType,
                                 indxOrientFront,
                                 indxPlexMode);

            if (indxPaperTray != -1)
                PCLWriter.paperSource(prnWriter, (Int16) indxPaperTray);

            if (! simplex)
            {
                PCLWriter.pageFace(prnWriter, true);
            }
               
            if (formAsMacro)
                PCLWriter.macroControl(prnWriter, macroIdFront,
                                       PCLWriter.eMacroControl.Call);
            else
                generateOverlayFront (prnWriter, false,
                                      _noForm, scaleFactor);

            //----------------------------------------------------------------//

            posYInc = (Int16)(scaleFactor * _posYIncMain);

            posX = (Int16)((scaleFactor *_posXValue) - _logPageOffset);
            posY = (Int16)((scaleFactor *_posYDesc));

            PCLWriter.font (prnWriter, true,
                            "19U", "s0p" + pitchMain + "h0s3b4099T");

            if (simplex)
            {
                PCLWriter.text(prnWriter, posX, posY, 0, pageNo.ToString() +
                                                    " of " +
                                                    pageCount.ToString());
            }
            else
            {
                PCLWriter.text(prnWriter, posX, posY, 0, pageNo.ToString() +
                                                    " of " +
                                                    pageCount.ToString());
            }

            //----------------------------------------------------------------//

            posY += posYInc;

            if (indxPaperSize >= PCLPaperSizes.getCount())
                PCLWriter.text(prnWriter, posX, posY, 0, "*** unknown ***");
            else
                PCLWriter.text(prnWriter, posX, posY, 0,
                          PCLPaperSizes.getName(indxPaperSize));

            //----------------------------------------------------------------//

            posY += posYInc;

            if (indxPaperType >= PCLPaperTypes.getCount())
                PCLWriter.text(prnWriter, posX, posY, 0, "*** unknown ***");
            else if (PCLPaperTypes.getType(indxPaperType) ==
                    PCLPaperTypes.eEntryType.NotSet)
                PCLWriter.text(prnWriter, posX, posY, 0, "<not set>");
            else
                PCLWriter.text(prnWriter, posX, posY, 0,
                          PCLPaperTypes.getName(indxPaperType));
            
            //----------------------------------------------------------------//

            posY += posYInc;

            if (indxPlexMode >= PCLPlexModes.getCount())
                PCLWriter.text(prnWriter, posX, posY, 0, "*** unknown ***");
            else
                PCLWriter.text(prnWriter, posX, posY, 0,
                          PCLPlexModes.getName(indxPlexMode));

            //----------------------------------------------------------------//

            posY += posYInc;

            if (indxOrientFront >= PCLOrientations.getCount())
                PCLWriter.text(prnWriter, posX, posY, 0, "*** unknown ***");
            else
                PCLWriter.text(prnWriter, posX, posY, 0,
                          PCLOrientations.getName(indxOrientFront));

            //----------------------------------------------------------------//

            posY += posYInc;

            if (indxPaperTray == PCLTrayDatas.getIdNotSetPCL())
                PCLWriter.text(prnWriter, posX, posY, 0, "<not set>");
            else if (indxPaperTray == _trayIdAutoSelectPCL)
                PCLWriter.text(prnWriter, posX, posY, 0,
                               indxPaperTray.ToString() + " (auto-select)");
            else
                PCLWriter.text(prnWriter, posX, posY, 0,
                               indxPaperTray.ToString());

            //----------------------------------------------------------------//
            //                                                                // 
            // Rear face (if not simplex)                                     // 
            //                                                                // 
            //----------------------------------------------------------------//

            if (! simplex)
            {
                if (indxOrientRear != indxOrientFront)
                {
                    PCLWriter.pageOrientation(
                        prnWriter,
                        PCLOrientations.getIdPCL(indxOrientRear).ToString());
                }
                                                         
                PCLWriter.pageFace(prnWriter, false);

                if (formAsMacro)
                    PCLWriter.macroControl(prnWriter, macroIdRear,
                                           PCLWriter.eMacroControl.Call);
                else
                    generateOverlayRear (prnWriter, false,
                                         _noForm, scaleFactor);

                //----------------------------------------------------------------//

                posX = (Int16)((scaleFactor * _posXValue) - _logPageOffset);
                posY = (Int16)(scaleFactor *_posYDesc);

                PCLWriter.font (prnWriter, true,
                                "19U", "s0p" + pitchMain + "h0s3b4099T");

                PCLWriter.text(prnWriter, posX, posY, 0, pageNo.ToString() +
                                                    " (rear) of " +
                                                    pageCount.ToString());

                //----------------------------------------------------------------//

                posY += (Int16) (posYInc * 4);

                if (indxOrientRear >= PCLOrientations.getCount())
                    PCLWriter.text(prnWriter, posX, posY, 0, "*** unknown ***");
                else
                    PCLWriter.text(prnWriter, posX, posY, 0,
                              PCLOrientations.getName(indxOrientRear));
            }

            PCLWriter.formFeed(prnWriter);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e P a g e S e t                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write set of test data pages to output file.                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void generatePageSet (BinaryWriter prnWriter,
                                             Int32        pageCount,
                                             Int32[]      indxPaperSize,
                                             Int32[]      indxPaperType,
                                             Int32[]      indxPaperTray,
                                             Int32[]      indxPlexMode,
                                             Int32[]      indxOrientFront,
                                             Int32[]      indxOrientRear,
                                             Int32[]      indxFormsFront,
                                             Int32[]      indxFormsRear,
                                             Int16[]      macroIdsFront,
                                             Int16[]      macroIdsRear,
                                             Single[]     scaleFactors,
                                             Boolean      formAsMacro)
        {
            for (Int32 i = 0; i < pageCount; i++)
            {
                Int16 macroIdFront;
                Int16 macroIdRear;

                Int32 index;

                if (formAsMacro)
                {
                    index = indxFormsFront[i];

                    macroIdFront = macroIdsFront[index];

                    index = indxFormsRear[i];

                    if (index == _noForm)
                        macroIdRear = _noForm;
                    else
                        macroIdRear = macroIdsRear[index];
                }
                else
                {
                    macroIdFront = _noForm;
                    macroIdRear  = _noForm;
                }

                generatePage(prnWriter,
                              i + 1,
                              pageCount,
                              indxPaperSize[i],
                              indxPaperType[i],
                              indxPaperTray[i],
                              indxPlexMode[i],
                              indxOrientFront[i],
                              indxOrientRear[i],
                              macroIdFront,
                              macroIdRear,
                              scaleFactors[i],
                              formAsMacro);
            }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // T r a y I d A u t o S e l e c t                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the 'auto-select' tray identifier.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 TrayIdAutoSelect
        {
            get
            {
                return _trayIdAutoSelectPCL;
            }
        }
    }
}
