using System;
using System.IO;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides PCL XL support for the FormSample tool.
    /// 
    /// © Chris Hutchinson 2012
    /// 
    /// </summary>

    static class ToolFormSamplePCLX
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const Int32 _symSet_19U = 629;

        public enum eStreamMethod : byte
        {
            ExecuteBegin,
            ExecuteEnd,
            Max
        }

        private static String[] streamMethodNames =
        { 
          "Execute stream (@ start of page)",
          "Execute stream (@ end of page)",
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
            Boolean      flagStreamRemove,
            Boolean      flagMainForm,
            Boolean      flagRearForm,
            Boolean      flagRearBPlate,
            Boolean      flagGSPushPop,
            Boolean      flagPrintDescText,
            String       formFileMain,
            String       formFileRear,
            eStreamMethod indxMethod,
            String       formNameMain,
            String       formNameRear)
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
                              flagStreamRemove,
                              flagMainForm,
                              flagRearForm,
                              formFileMain,
                              formFileRear,
                              indxMethod,
                              formNameMain,
                              formNameRear);

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
                             flagRearBPlate,
                             flagGSPushPop,
                             flagPrintDescText,
                             formFileMain,
                             formFileRear,
                             indxMethod,
                             formNameMain,
                             formNameRear);

            generateJobTrailer (prnWriter,
                                flagStreamRemove,
                                flagMainForm,
                                flagRearForm,
                                formNameMain,
                                formNameRear);
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
            Boolean      flagStreamRemove,
            Boolean      flagMainForm,
            Boolean      flagRearForm,
            String       formFileMain,
            String       formFileRear,
            eStreamMethod indxMethod,
            String       formNameMain,
            String       formNameRear)
        {
            PCLXLWriter.stdJobHeader (prnWriter, "");

            if (flagMainForm)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Main (or only) form in use.                                //
                // Download contents of specified file.                       //
                //                                                            //
                //------------------------------------------------------------//

                PCLXLDownloadStream.streamFileEmbed(prnWriter,
                                                    formFileMain,
                                                    formNameMain,
                                                    flagMainEncapsulated);
            }

            if (!flagSimplexJob)
            {
                if (flagRearForm)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Rear form in use.                                      //
                    // Download contents of specified file.                   //
                    //                                                        //
                    //--------------------------------------------------------//

                    PCLXLDownloadStream.streamFileEmbed(prnWriter,
                                                        formFileRear,
                                                        formNameRear,
                                                        flagRearEncapsulated);
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
                                               Boolean      flagStreamRemove,
                                               Boolean      flagMainForm,
                                               Boolean      flagRearForm,
                                               String       formNameMain,
                                               String       formNameRear)
        {
            if (flagStreamRemove)
            {
                if (flagMainForm)
                    PCLXLWriter.streamRemove (prnWriter,
                                             formNameMain);

                if (flagRearForm)
                    PCLXLWriter.streamRemove (prnWriter,
                                             formNameRear);
            }

            PCLXLWriter.stdJobTrailer (prnWriter, false, "");
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
                                         Boolean      flagRearBPlate,
                                         Boolean      flagGSPushPop,
                                         Boolean      flagPrintDescText,
                                         String       formFileMain,
                                         String       formFileRear,
                                         eStreamMethod indxMethod,
                                         String       formNameMain,
                                         String       formNameRear)
        {
            const Int32 lenBuf  = 1024;
            const Int16 incPosY = 150;

            Byte[] buffer = new Byte[lenBuf];

            Boolean altOrient;
            Boolean pageUsesForm;
            Boolean firstPage;

            Int16 posX,
                  posY;

            String formName;
            Int32 indxOrient;

            Int32 indBuf = 0;
            Int32 crntPtSize;

            altOrient = (indxOrientation != indxOrientRear);
            firstPage = (pageNo == 1);

            if (flagFrontFace)
            {
                indxOrient   = indxOrientation;
                pageUsesForm = flagMainForm;
                formName     = formNameMain;
            }
            else
            {
                indxOrient = indxOrientRear;

                if (flagRearForm)
                {
                    pageUsesForm = flagRearForm;
                    formName     = formNameRear;
                }
                else
                {
                    pageUsesForm = flagMainForm;
                    formName     = formNameMain;
                }
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Write 'BeginPage' operator and (if requested for begin page)   //
            // the required stream 'execute' operator.                        //
            //                                                                //
            //----------------------------------------------------------------//

            PCLXLWriter.pageBegin (prnWriter,
                                   indxPaperSize,
                                   indxPaperType,
                                   -1,
                                   indxOrient,
                                   indxPlexMode,
                                   firstPage,
                                   flagFrontFace);

            if (pageUsesForm)
            {
                if (indxMethod == eStreamMethod.ExecuteBegin)
                {
                    if (flagGSPushPop)
                    {
                        PCLXLWriter.addOperator (
                           ref buffer,
                           ref indBuf,
                           PCLXLOperators.eTag.PushGS);

                        prnWriter.Write (buffer, 0, indBuf);
                        indBuf = 0;
                    }

                    PCLXLWriter.streamExec (prnWriter, false, formName);

                    if (flagGSPushPop)
                    {
                        PCLXLWriter.addOperator (
                            ref buffer,
                            ref indBuf,
                            PCLXLOperators.eTag.PopGS);

                        prnWriter.Write (buffer, 0, indBuf);
                        indBuf = 0;
                    }
                }
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Write descriptive text.                                        //
            //                                                                //
            //----------------------------------------------------------------//

            if (flagPrintDescText)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Headers.                                                   //
                //                                                            //
                //------------------------------------------------------------//

                crntPtSize = 10;

                PCLXLWriter.addAttrUbyte (ref buffer,
                                         ref indBuf,
                                         PCLXLAttributes.eTag.ColorSpace,
                                         (Byte) PCLXLAttrEnums.eVal.eGray);

                PCLXLWriter.addOperator (ref buffer,
                                        ref indBuf,
                                        PCLXLOperators.eTag.SetColorSpace);

                PCLXLWriter.addAttrUbyte (ref buffer,
                                         ref indBuf,
                                         PCLXLAttributes.eTag.GrayLevel,
                                         0);

                PCLXLWriter.addOperator (ref buffer,
                                        ref indBuf,
                                        PCLXLOperators.eTag.SetBrushSource);

                prnWriter.Write (buffer, 0, indBuf);
                indBuf = 0;

                PCLXLWriter.font (prnWriter, false, crntPtSize,
                                 _symSet_19U, "Courier         ");

                posX = 600;
                posY = 1350;

                PCLXLWriter.text(prnWriter, false, false, 
                                 PCLXLWriter.advances_Courier,
                                  crntPtSize, posX, posY, "Page:");

                if (firstPage)
                {
                    posY += incPosY;

                    PCLXLWriter.text(prnWriter, false, false,
                                      PCLXLWriter.advances_Courier, crntPtSize,
                                      posX, posY, "PaperSize:");

                    posY += incPosY;

                    PCLXLWriter.text(prnWriter, false, false,
                                      PCLXLWriter.advances_Courier, crntPtSize,
                                      posX, posY, "PaperType:");

                    posY += incPosY;

                    PCLXLWriter.text(prnWriter, false, false,
                                      PCLXLWriter.advances_Courier, crntPtSize,
                                      posX, posY, "Plex Mode:");

                    posY += incPosY;

                    PCLXLWriter.text(prnWriter, false, false,
                                      PCLXLWriter.advances_Courier, crntPtSize,
                                      posX, posY, "Method:");

                    posY += incPosY;

                    PCLXLWriter.text(prnWriter, false, false,
                                      PCLXLWriter.advances_Courier, crntPtSize,
                                      posX, posY, "Orientation:");

                    posY += incPosY;

                    PCLXLWriter.text(prnWriter, false, false,
                                      PCLXLWriter.advances_Courier, crntPtSize,
                                      posX, posY, "Rear Orientation:");

                    posY += incPosY;

                    PCLXLWriter.text(prnWriter, false, false,
                                      PCLXLWriter.advances_Courier, crntPtSize,
                                      posX, posY, "Main Form:");

                    posY += incPosY;

                    if (flagRearForm)
                        if (flagRearBPlate)
                            PCLXLWriter.text(prnWriter, false, false,
                                              PCLXLWriter.advances_Courier,
                                              crntPtSize, posX, posY,
                                              "Rear Boilerplate:");
                        else
                            PCLXLWriter.text(prnWriter, false, false,
                                              PCLXLWriter.advances_Courier,
                                              crntPtSize, posX, posY,
                                              "Rear Form:");
                }

                //------------------------------------------------------------//
                //                                                            //
                // Write variable data.                                       //
                //                                                            //
                //------------------------------------------------------------//

                crntPtSize = 10;

                PCLXLWriter.addAttrUbyte (ref buffer,
                                         ref indBuf,
                                         PCLXLAttributes.eTag.GrayLevel,
                                         0);

                PCLXLWriter.addOperator (ref buffer,
                                        ref indBuf,
                                        PCLXLOperators.eTag.SetBrushSource);

                prnWriter.Write (buffer, 0, indBuf);
                indBuf = 0;

                PCLXLWriter.font (prnWriter, false, crntPtSize,
                                 _symSet_19U, "Courier       Bd");

                posX = 1800;
                posY = 1350;

                PCLXLWriter.text(prnWriter, false, false,
                                 PCLXLWriter.advances_Courier, crntPtSize,
                                 posX, posY,
                                 pageNo + " of " + pageCount);

                if (firstPage)
                {
                    String textOrientRear;

                    posY += incPosY;

                    PCLXLWriter.text(prnWriter, false, false,
                                      PCLXLWriter.advances_Courier, crntPtSize,
                                      posX, posY,
                                      PCLPaperSizes.getName (indxPaperSize));

                    posY += incPosY;

                    PCLXLWriter.text(prnWriter, false, false,
                                      PCLXLWriter.advances_Courier, crntPtSize,
                                      posX, posY,
                                      PCLPaperTypes.getName (indxPaperType));

                    posY += incPosY;

                    PCLXLWriter.text(prnWriter, false, false,
                                      PCLXLWriter.advances_Courier, crntPtSize,
                                      posX, posY,
                                      PCLPlexModes.getName (indxPlexMode));

                    posY += incPosY;

                    PCLXLWriter.text(prnWriter, false, false,
                                      PCLXLWriter.advances_Courier, crntPtSize,
                                      posX, posY,
                                      streamMethodNames[(Int32) indxMethod]);

                    posY += incPosY;

                    PCLXLWriter.text(prnWriter, false, false,
                                      PCLXLWriter.advances_Courier, crntPtSize,
                                      posX, posY,
                                      PCLOrientations.getName (indxOrientation));

                    if (flagSimplexJob)
                        textOrientRear = "<not applicable>";
                    else
                        textOrientRear = PCLOrientations.getName (indxOrientRear);

                    posY += incPosY;

                    PCLXLWriter.text(prnWriter, false, false,
                                      PCLXLWriter.advances_Courier, crntPtSize,
                                      posX, posY,
                                      textOrientRear);

                    posY += incPosY;

                    if (flagMainForm)
                    {
                        const Int32 maxLen = 51;
                        const Int32 halfLen = (maxLen - 5) / 2;

                        Int32 len = formFileMain.Length;

                        if (len < maxLen)
                            PCLXLWriter.text(prnWriter, false, false,
                                              PCLXLWriter.advances_Courier,
                                              crntPtSize, posX, posY, formFileMain);
                        else
                            PCLXLWriter.text(prnWriter, false, false,
                                              PCLXLWriter.advances_Courier,
                                              crntPtSize, posX, posY,
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
                            PCLXLWriter.text(prnWriter, false, false,
                                              PCLXLWriter.advances_Courier,
                                              crntPtSize, posX, posY, formFileRear);
                        else
                            PCLXLWriter.text(prnWriter, false, false,
                                              PCLXLWriter.advances_Courier,
                                              crntPtSize, posX, posY,
                                              formFileRear.Substring (0, halfLen) +
                                              " ... " +
                                              formFileRear.Substring (len - halfLen,
                                                                      halfLen));
                    }
                }
            }

            //----------------------------------------------------------------//
            //                                                                //
            // If requested for end page, write the required stream 'execute' //
            // operator.                                                      //
            //                                                                //
            //----------------------------------------------------------------//

            if (pageUsesForm)
            {
                if (indxMethod == eStreamMethod.ExecuteEnd)
                {
                    if (flagGSPushPop)
                    {
                        PCLXLWriter.addOperator (
                           ref buffer,
                           ref indBuf,
                           PCLXLOperators.eTag.PushGS);

                        prnWriter.Write (buffer, 0, indBuf);
                        indBuf = 0;
                    }

                    PCLXLWriter.streamExec (prnWriter, false, formName);

                    if (flagGSPushPop)
                    {
                        PCLXLWriter.addOperator (
                            ref buffer,
                            ref indBuf,
                            PCLXLOperators.eTag.PopGS);

                        prnWriter.Write (buffer, 0, indBuf);
                        indBuf = 0;
                    }
                }
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Write EndPage' operator and associated attribute list.         //
            //                                                                //
            //----------------------------------------------------------------//

            PCLXLWriter.pageEnd (prnWriter, 1);

            //------------------------------------------------------------//
            //                                                            //
            // Generate rear boilerplate side if necessary.               //
            //                                                            //
            //------------------------------------------------------------//

            if ((flagRearForm) && (flagRearBPlate))
            {
                PCLXLWriter.pageBegin (prnWriter,
                                       indxPaperSize,
                                       indxPaperType,
                                       -1,
                                       indxOrientRear,
                                       indxPlexMode,
                                       firstPage,
                                       false);

                PCLXLWriter.streamExec (prnWriter, false, formNameRear);

                PCLXLWriter.pageEnd (prnWriter, 1);
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
                                            Boolean      flagRearBPlate,
                                            Boolean      flagGSPushPop,
                                            Boolean      flagPrintDescText,
                                            String       formFileMain,
                                            String       formFileRear,
                                            eStreamMethod indxMethod,
                                            String       formNameMain,
                                            String       formNameRear)
        {
            Boolean flagFrontFace;

            flagFrontFace = true;

            for (Int32 pageNo = 1; pageNo <= pageCount; pageNo++)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Generate test pages.                                       //
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
                              flagRearBPlate,
                              flagGSPushPop,
                              flagPrintDescText,
                              formFileMain,
                              formFileRear,
                              indxMethod,
                              formNameMain,
                              formNameRear);

                //------------------------------------------------------------//
                //                                                            //
                // Toggle front/rear face indicator.                          //
                //                                                            //
                //------------------------------------------------------------//

                if (!flagSimplexJob)
                    if (!flagRearForm)
                        flagFrontFace = !flagFrontFace;
                    else if (!flagRearBPlate)
                        flagFrontFace = !flagFrontFace;
            }
        }
    }
}
