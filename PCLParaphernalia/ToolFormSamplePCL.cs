using System;
using System.IO;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides PCL support for the FormSample tool.
    /// 
    /// © Chris Hutchinson 2012
    /// 
    /// </summary>

    static class ToolFormSamplePCL
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const Int32 _logPageOffset = 142;

        public enum eMacroMethod : byte
        {
            CallBegin,
            CallEnd,
            ExecuteBegin,
            ExecuteEnd,
            Overlay,
            Max
        }

        private static String[] macroMethodNames =
        { 
          "Call macro (@ start of page)",
          "Call macro (@ end of page)",
          "Execute macro (@ start of page)",
          "Execute macro (@ end of page)",
          "Overlay macro (automatic @ end of page)",
          "Max - invalid"
        };

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

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

        public static void generateJob(
            BinaryWriter prnWriter,
            Int32        indxPaperSize,
            Int32        indxPaperType,
            Int32        indxOrientation,
            Int32        indxOrientRear,
            Int32        indxPlexMode,
            Int32        testPageCount,
            Boolean      flagMainEncapsulated,
            Boolean      flagRearEncapsulated,
            Boolean      flagMacroRemove,
            Boolean      flagMainForm,
            Boolean      flagRearForm,
            Boolean      flagMainOnPrnDisk,
            Boolean      flagRearOnPrnDisk,
            Boolean      flagRearBPlate,
            Boolean      flagPrintDescText,
            String       formFileMain,
            String       formFileRear,
            eMacroMethod indxMethod,
            Int32        macroIdMain,
            Int32        macroIdRear)
        {
            Boolean flagSimplexJob = PCLPlexModes.isSimplex (indxPlexMode);

            generateJobHeader (prnWriter,
                              indxPaperSize,
                              indxPaperType,
                              indxOrientation,
                              indxPlexMode,
                              flagSimplexJob,
                              flagMainEncapsulated,
                              flagRearEncapsulated,
                              flagMacroRemove,
                              flagMainForm,
                              flagRearForm,
                              flagMainOnPrnDisk,
                              flagRearOnPrnDisk,
                              formFileMain,
                              formFileRear,
                              indxMethod,
                              macroIdMain,
                              macroIdRear);

            generatePageSet (prnWriter,
                             testPageCount,
                             indxPaperSize,
                             indxPaperType,
                             indxOrientation,
                             indxOrientRear,
                             indxPlexMode,
                             flagSimplexJob, 
                             flagMainForm,
                             flagRearForm,
                             flagMainOnPrnDisk,
                             flagRearOnPrnDisk,
                             flagRearBPlate,
                             flagPrintDescText,
                             formFileMain,
                             formFileRear,
                             indxMethod,
                             macroIdMain,
                             macroIdRear);
           
            generateJobTrailer (prnWriter, 
                                flagMacroRemove,
                                flagMainForm,
                                flagRearForm,
                                macroIdMain,
                                macroIdRear);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e J o b H e a d e r                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write stream initialisation sequences to output file.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void generateJobHeader(
            BinaryWriter prnWriter,
            Int32        indxPaperSize,
            Int32        indxPaperType,
            Int32        indxOrientation,
            Int32        indxPlexMode,
            Boolean      flagSimplexJob,
            Boolean      flagMainEncapsulated,
            Boolean      flagRearEncapsulated,
            Boolean      flagMacroRemove,
            Boolean      flagMainForm,
            Boolean      flagRearForm,
            Boolean      flagMainOnPrnDisk,
            Boolean      flagRearOnPrnDisk,
            String       formFileMain,
            String       formFileRear,
            eMacroMethod indxMethod,
            Int32        macroIdMain,
            Int32        macroIdRear)
        {
            PCLWriter.stdJobHeader (prnWriter, "");

            if (flagMainForm)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Main (or only) form in use.                                //
                //                                                            //
                //------------------------------------------------------------//

                if (flagMainOnPrnDisk)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Associate macro identifier with specified file held on //
                    // printer hard disk.                                     //
                    //                                                        //
                    // Make macro 'permanent' if remove flag not specified.   //
                    // Note that this doesn't appear to work with identifiers //
                    // associated with printer disk files, but we'll leave it //
                    // in anyway, in case it works on some devices.           //
                    //                                                        //
                    //--------------------------------------------------------//

                    PCLWriter.macroFileIdAssociate(prnWriter,
                                                    (UInt16) macroIdMain,
                                                    formFileMain);

                    if (!flagMacroRemove)
                        PCLWriter.macroControl(
                            prnWriter,
                            (Int16)macroIdMain,
                            PCLWriter.eMacroControl.MakePermanent);
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Download contents of specified file.                   //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (!flagMainEncapsulated)
                        PCLWriter.macroControl(prnWriter,
                                                (Int16)macroIdMain,
                                                PCLWriter.eMacroControl.StartDef);

                    PCLDownloadMacro.macroFileCopy(prnWriter, formFileMain);

                    if (!flagMainEncapsulated)
                        PCLWriter.macroControl(prnWriter,
                                                (Int16)macroIdMain,
                                                PCLWriter.eMacroControl.StopDef);

                    if (!flagMacroRemove)
                        PCLWriter.macroControl(
                            prnWriter,
                            (Int16)macroIdMain,
                            PCLWriter.eMacroControl.MakePermanent);
                }
            }

            if (! flagSimplexJob)
            {
                if (flagRearForm)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Rear form in use.                                      //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (flagRearOnPrnDisk)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Associate macro identifier with specified file     //
                        // held on printer hard disk.                         //
                        //                                                    //
                        // Make macro 'permanent' if remove flag not          //
                        // specified.                                         //
                        // Note that this doesn't appear to work with         //
                        // identifiers associated with printer disk files,    //
                        // but we'll leave it in anyway, in case it works on  //
                        // some devices.                                      //
                        //                                                    //
                        //----------------------------------------------------//

                        PCLWriter.macroFileIdAssociate(prnWriter,
                                                        (UInt16)macroIdRear,
                                                        formFileRear);

                        if (!flagMacroRemove)
                            PCLWriter.macroControl(
                                prnWriter,
                                (Int16)macroIdRear,
                                PCLWriter.eMacroControl.MakePermanent);
                    }
                    else
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Download contents of specified file.               //
                        //                                                    //
                        //----------------------------------------------------//

                        if (!flagRearEncapsulated)
                            PCLWriter.macroControl(
                                prnWriter,
                                (Int16)macroIdRear,
                                PCLWriter.eMacroControl.StartDef);

                        PCLDownloadMacro.macroFileCopy(prnWriter, formFileRear);

                        if (!flagRearEncapsulated)
                            PCLWriter.macroControl(
                                prnWriter,
                                (Int16)macroIdRear,
                                PCLWriter.eMacroControl.StopDef);

                        if (!flagMacroRemove)
                            PCLWriter.macroControl(
                                prnWriter,
                                (Int16)macroIdRear,
                                PCLWriter.eMacroControl.MakePermanent);
                    }
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e J o b T r a i l e r                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write termination sequences to output file.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void generateJobTrailer(BinaryWriter prnWriter,
                                               Boolean      flagMacroRemove,
                                               Boolean      flagMainForm,
                                               Boolean      flagRearForm,
                                               Int32        macroIdMain,
                                               Int32        macroIdRear)
        {
            if (flagMacroRemove)
            {
                if (flagMainForm)
                    PCLWriter.macroControl (
                        prnWriter,
                        (Int16) macroIdMain,
                        PCLWriter.eMacroControl.Delete);

                if (flagRearForm)
                    PCLWriter.macroControl (
                        prnWriter,
                        (Int16) macroIdRear,
                        PCLWriter.eMacroControl.Delete);
            }

            PCLWriter.stdJobTrailer (prnWriter, false, -1);
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
                                         Int32        indxOrientation,
                                         Int32        indxOrientRear,
                                         Int32        indxPlexMode,
                                         Boolean      flagFrontFace,           
                                         Boolean      flagSimplexJob,
                                         Boolean      flagMainForm,
                                         Boolean      flagRearForm,
                                         Boolean      flagMainOnPrnDisk,
                                         Boolean      flagRearOnPrnDisk,
                                         Boolean      flagRearBPlate,
                                         Boolean      flagPrintDescText,
                                         String       formFileMain,
                                         String       formFileRear,
                                         eMacroMethod indxMethod,
                                         Int32        macroIdMain,
                                         Int32        macroIdRear)
        {
            const Int16 incPosY = 150;

            Boolean altOrient;
            Boolean pageUsesForm;
            Boolean firstPage;

            Int16 posX,
                  posY;

            Int32 macroId;
            Int32 indxOrient;

            altOrient = (indxOrientation != indxOrientRear);
            firstPage = (pageNo == 1);

            if (flagFrontFace)
            {
                indxOrient   = indxOrientation;
                pageUsesForm = flagMainForm;
                macroId      = macroIdMain;
            }
            else
            {
                indxOrient = indxOrientRear;

                if (flagRearForm)
                {
                    pageUsesForm = flagRearForm;
                    macroId      = macroIdRear;
                }
                else
                {
                    pageUsesForm = flagMainForm;
                    macroId      = macroIdMain;
                }
            }

            if (firstPage)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Generate first (or only) page header.                      //
                //                                                            //
                //------------------------------------------------------------//

                PCLWriter.pageHeader (prnWriter,
                                      indxPaperSize,
                                      indxPaperType,
                                      indxOrientation,
                                      indxPlexMode);

                if (indxMethod == eMacroMethod.Overlay)
                {
                    PCLWriter.macroControl (prnWriter,
                                            (Int16) macroIdMain,
                                            PCLWriter.eMacroControl.Overlay);
                }
            }
            else
            {
                //----------------------------------------------------------------//
                //                                                                //
                // Not first page:                                                //
                // - for simplex jobs:                                            //
                //      - write 'form feed' sequence.                             //
                // - for duplex jobs:                                             // 
                //      - write 'page side' sequence.                             //
                //      - if rear face, and alternate orientations specified,     //
                //        write 'set orientation' sequence.                       //
                //                                                                //
                //----------------------------------------------------------------//

                if (flagSimplexJob)
                {
                    PCLWriter.formFeed (prnWriter);
                }
                else
                {
                    PCLWriter.pageFace (prnWriter, flagFrontFace);

                    if (altOrient)
                    {
                        PCLWriter.pageOrientation (
                            prnWriter,
                            PCLOrientations.getIdPCL (indxOrient).ToString ());
                    }
                }
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Write any required 'begin page' macro 'call' or 'execute'      //
            // sequence.                                                      //
            //                                                                //
            //----------------------------------------------------------------//

            if (pageUsesForm)
            {
                if (indxMethod == eMacroMethod.CallBegin)
                {
                    PCLWriter.macroControl (prnWriter,
                                            (Int16) macroId,
                                            PCLWriter.eMacroControl.Call);
                }
                else if (indxMethod == eMacroMethod.ExecuteBegin)
                {
                    PCLWriter.macroControl (prnWriter,
                                            (Int16) macroId,
                                            PCLWriter.eMacroControl.Execute);
                }
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Write descriptive text headers.                                //
            //                                                                //
            //----------------------------------------------------------------//

            if (flagPrintDescText)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Write headers.                                             //
                //                                                            //
                //------------------------------------------------------------//
                
                PCLWriter.font (prnWriter, true, "19U", "s0p12h0s0b4099T");

                posX = 600 - _logPageOffset;
                posY = 1350;

                PCLWriter.text (prnWriter, posX, posY, 0, "Page:");

                if (firstPage)
                {
                    posY += incPosY;

                    PCLWriter.text (prnWriter, posX, posY, 0, "Paper size:");

                    posY += incPosY;

                    PCLWriter.text (prnWriter, posX, posY, 0, "Paper type:");

                    posY += incPosY;

                    PCLWriter.text (prnWriter, posX, posY, 0, "Plex mode:");

                    posY += incPosY;

                    PCLWriter.text (prnWriter, posX, posY, 0, "Method:");

                    posY += incPosY;

                    PCLWriter.text (prnWriter, posX, posY, 0, "Orientation:");

                    posY += incPosY;

                    PCLWriter.text (prnWriter, posX, posY, 0, "Rear orientation:");

                    posY += incPosY;

                    if (flagMainOnPrnDisk)
                        PCLWriter.text (prnWriter, posX, posY, 0,
                                        "Main form printer file:");
                    else
                        PCLWriter.text (prnWriter, posX, posY, 0,
                                        "Main form download file:");

                    posY += incPosY;

                    if (flagRearOnPrnDisk)
                        PCLWriter.text (prnWriter, posX, posY, 0,
                                        "Rear form printer file:");
                    else
                        PCLWriter.text (prnWriter, posX, posY, 0,
                                        "Rear form download file:");

                    posY += incPosY;

                    if ((flagRearForm) && (flagRearBPlate))
                            PCLWriter.text (prnWriter, posX, posY, 0,
                                "Rear Form is boilerplate");
                }

                //------------------------------------------------------------//
                //                                                            //
                // Write variable data.                                       //
                //                                                            //
                //------------------------------------------------------------//

                PCLWriter.font (prnWriter, true, "19U", "s0p12h0s3b4099T");

                posX = 1920 - _logPageOffset;
                posY = 1350;

                PCLWriter.text (prnWriter, posX, posY, 0,
                                pageNo.ToString () + " of " +
                                pageCount.ToString ());

                if (firstPage)
                {
                    String textOrientRear;

                    posY += incPosY;

                    PCLWriter.text (prnWriter, posX, posY, 0,
                                    PCLPaperSizes.getName (indxPaperSize));

                    posY += incPosY;

                    PCLWriter.text (prnWriter, posX, posY, 0,
                                    PCLPaperTypes.getName (indxPaperType));

                    posY += incPosY;

                    PCLWriter.text (prnWriter, posX, posY, 0,
                                    PCLPlexModes.getName (indxPlexMode));

                    posY += incPosY;

                    PCLWriter.text (prnWriter, posX, posY, 0,
                                    macroMethodNames[(Int32) indxMethod]);

                    posY += incPosY;

                    PCLWriter.text (prnWriter, posX, posY, 0,
                                    PCLOrientations.getName (indxOrientation));

                    if (flagSimplexJob)
                        textOrientRear = "<not applicable>";
                    else if (altOrient)
                        textOrientRear = PCLOrientations.getName (indxOrientRear);
                    else
                        textOrientRear = "<not set>";

                    posY += incPosY;

                    PCLWriter.text (prnWriter, posX, posY, 0,
                                    textOrientRear);

                    posY += incPosY;

                    if (flagMainForm)
                    {
                        const Int32 maxLen = 51;
                        const Int32 halfLen = (maxLen - 5) / 2;

                        Int32 len = formFileMain.Length;

                        if (len < maxLen)
                            PCLWriter.text (prnWriter, posX, posY, 0, formFileMain);
                        else
                            PCLWriter.text (prnWriter, posX, posY, 0,
                                           formFileMain.Substring (0, halfLen) +
                                           " ... " +
                                           formFileMain.Substring (len - halfLen,
                                                                  halfLen));
                    }

                    posY += incPosY;

                    if (flagRearForm)
                    {
                        const Int32 maxLen = 51;
                        const Int32 halfLen = (maxLen - 5) / 2;

                        Int32 len = formFileRear.Length;

                        if (len < maxLen)
                            PCLWriter.text (prnWriter, posX, posY, 0, formFileRear);
                        else
                            PCLWriter.text (prnWriter, posX, posY, 0,
                                           formFileRear.Substring (0, halfLen) +
                                           " ... " +
                                           formFileRear.Substring (len - halfLen,
                                                                  halfLen));
                    }
                }
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Write any required 'end of page' macro 'call' or 'execute'     //
            // sequences.                                                     //
            //                                                                //
            //----------------------------------------------------------------//

            if (pageUsesForm)
            {
                if (indxMethod == eMacroMethod.CallEnd)
                {
                    PCLWriter.macroControl (prnWriter,
                                            (Int16) macroId,
                                            PCLWriter.eMacroControl.Call);
                }
                else if (indxMethod == eMacroMethod.ExecuteEnd)
                {
                    PCLWriter.macroControl (prnWriter,
                                            (Int16) macroId,
                                            PCLWriter.eMacroControl.Execute);
                }
            }

            //------------------------------------------------------------//
            //                                                            //
            // Generate rear boilerplate side if necessary.               //
            //                                                            //
            //------------------------------------------------------------//

            if ((flagRearForm) && (flagRearBPlate))
            {
                PCLWriter.pageFace (prnWriter, false);

                if (altOrient)
                {
                    PCLWriter.pageOrientation (
                        prnWriter,
                        PCLOrientations.getIdPCL (indxOrientRear).ToString ());
                }

                if ((indxMethod == eMacroMethod.CallBegin) ||
                    (indxMethod == eMacroMethod.CallEnd))
                {
                    PCLWriter.macroControl (prnWriter,
                                            (Int16) macroIdRear,
                                            PCLWriter.eMacroControl.Call);
                }
                else if ((indxMethod == eMacroMethod.ExecuteBegin) ||
                         (indxMethod == eMacroMethod.ExecuteEnd))
                {
                    PCLWriter.macroControl (prnWriter,
                                            (Int16) macroIdRear,
                                            PCLWriter.eMacroControl.Execute);
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e P a g e S e t                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write set of test data pages to output file.                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void generatePageSet(BinaryWriter prnWriter,
                                            Int32        pageCount,
                                            Int32        indxPaperSize,
                                            Int32        indxPaperType,
                                            Int32        indxOrientation,
                                            Int32        indxOrientRear,
                                            Int32        indxPlexMode,
                                            Boolean      flagSimplexJob,
                                            Boolean      flagMainForm,
                                            Boolean      flagRearForm,
                                            Boolean      flagMainOnPrnDisk,
                                            Boolean      flagRearOnPrnDisk,
                                            Boolean      flagRearBPlate,
                                            Boolean      flagPrintDescText,
                                            String       formFileMain,
                                            String       formFileRear,
                                            eMacroMethod indxMethod,
                                            Int32        macroIdMain,
                                            Int32        macroIdRear)
        {
            Boolean flagFrontFace;

            flagFrontFace = true;

            for (Int32 pageNo = 1; pageNo <= pageCount; pageNo++)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Generate test page.                                        //
                //                                                            //
                //------------------------------------------------------------//

                generatePage (prnWriter,
                              pageNo,
                              pageCount,
                              indxPaperSize,
                              indxPaperType,
                              indxOrientation,
                              indxOrientRear,
                              indxPlexMode,
                              flagFrontFace,
                              flagSimplexJob,
                              flagMainForm,
                              flagRearForm,
                              flagMainOnPrnDisk,
                              flagRearOnPrnDisk,
                              flagRearBPlate,
                              flagPrintDescText,
                              formFileMain,
                              formFileRear,
                              indxMethod,
                              macroIdMain,
                              macroIdRear);

                //------------------------------------------------------------//
                //                                                            //
                // Toggle front/rear face indicator.                          //
                //                                                            //
                //------------------------------------------------------------//

                if ((! flagSimplexJob) && (! flagRearBPlate))
                    flagFrontFace = ! flagFrontFace;
            }

            //----------------------------------------------------------------//
            //                                                                //
            // If the macro method is 'Overlay', it seems that a terminating  //
            // FormFeed character is required to trigger the (end of page)    //
            // overlay on the last page - without it, only the variable data  //
            // is printed on that page!                                       //
            //                                                                //
            //----------------------------------------------------------------//

            if (indxMethod == eMacroMethod.Overlay)
                PCLWriter.formFeed (prnWriter);
        }
    }
}
