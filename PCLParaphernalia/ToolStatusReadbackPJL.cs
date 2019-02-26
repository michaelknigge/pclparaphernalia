using System;
using System.IO;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides PJL support for the PrinterInfo tool.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class ToolStatusReadbackPJL
    {
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e R e q u e s t                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate status readback request data.                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void generateRequest (BinaryWriter prnWriter,
                                            PJLCommands.eCmdIndex cmdIndx,
                                            Int32        indexCategory,
                                            Int32        indexVariable,
                                            String       customCat,
                                            String       customVar)
        {
            String seq;

            if (cmdIndx != PJLCommands.eCmdIndex.Unknown)
            {
                PJLCommands.eRequestType reqType;
    
                String cmdName;
    
                reqType = PJLCommands.getType(cmdIndx);
                cmdName = PJLCommands.getName(cmdIndx);

                if (reqType == PJLCommands.eRequestType.Category)
                {
                    if (indexCategory < PJLCategories.getCount())
                    {
                        if (PJLCategories.getType (indexCategory) ==
                            PJLCategories.eCategoryType.Custom)
                        {
                            seq = "\x1b" + "%-12345X" +
                                           "@PJL ECHO PCLParaphernalia" + "\x0d\x0a" +
                                           "@PJL " +
                                           cmdName + " " +
                                           customCat + "\x0d\x0a" +
                                  "\x1b" + "%-12345X";

                        }
                        else
                        {
                            String categoryName;

                            categoryName = PJLCategories.getName (indexCategory);

                            seq = "\x1b" + "%-12345X" +
                                           "@PJL ECHO PCLParaphernalia" + "\x0d\x0a" +
                                           "@PJL " +
                                           cmdName + " " +
                                           categoryName + "\x0d\x0a" +
                                  "\x1b" + "%-12345X";
                        }

                        prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
                    }
                }
                else
                {
                    if (indexVariable < PJLVariables.getCount())
                    {
                        PJLVariables.eVarType varType;

                        varType = PJLVariables.getType(indexVariable);

                        if (varType == PJLVariables.eVarType.Custom)
                        {
                            seq = "\x1b" + "%-12345X" +
                                           "@PJL ECHO PCLParaphernalia" + "\x0d\x0a" +
                                           "@PJL " +
                                           cmdName + " " +
                                           customVar + "\x0d\x0a" +
                                  "\x1b" + "%-12345X"; 
                        }
                        else
                        {
                            String variableName;
                            String personality;

                            variableName = PJLVariables.getName (indexVariable);

                            if (varType == PJLVariables.eVarType.PCL)
                                personality = "LPARM : PCL ";
                            else if (varType == PJLVariables.eVarType.PDF)
                                personality = "LPARM : PDF ";
                            else if (varType == PJLVariables.eVarType.PS)
                                personality = "LPARM : POSTSCRIPT ";
                            else
                                personality = "";

                            seq = "\x1b" + "%-12345X" +
                                           "@PJL ECHO PCLParaphernalia" + "\x0d\x0a" +
                                           "@PJL " +
                                           cmdName + " " +
                                           personality +
                                           variableName + "\x0d\x0a" +
                                  "\x1b" + "%-12345X";
                        }

                        prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
                    }
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e a d R e s p o n s e                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Read response from target.                                         //
        //                                                                    //
        // There should be TWO FormFeeds in the returned data:                //
        //  - first one after return of ECHO data;                            //
        //  - second one after end of response data;                          //
        //                                                                    //
        // With some printers, a minimum of two 'reads' are necessary with    //
        // PJL Status Readback, because:                                      //
        //  - the first read reads the ECHO (identifying) data (terminated    //
        //    with a Form Feed byte).                                         //
        //  - the second read reads the actual response data (also terminated //
        //    with a FormFeed byte).                                          //
        //                                                                    //
        // With other printers, the response data is concatenated with the    //
        // echo data, so a single read may be all that is necessary.          //
        //                                                                    //
        // In each case, multiple reads may be required if the length of the  //
        // returned data exceeds the read 'chunk' size.                       //
        //                                                                    //
        // And some printers may respond with bits of data less than the      //
        // 'chunk' size, if there is a lot to return, but it is still         //
        // necessary to carry on reading until the terminating <FF> byte is   //
        // found.                                                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String readResponse ()
        {
            const Int32 replyBufLen = 32768;

            Byte[] replyData = new Byte[replyBufLen];

            Int32 replyLen = 0;

            Boolean readFF_A = false;            // two <FF>s expected //
            Boolean OK = false;
            Boolean replyComplete = false;

            Int32 offset = 0;
            Int32 endOffset = 0;
            Int32 bufRem = replyBufLen;
            Int32 blockLen = 0;

            while (!replyComplete)
            {
                OK = TargetCore.responseReadBlock(offset,
                                                   bufRem,
                                                   ref replyData,
                                                   ref blockLen);

                endOffset = offset + blockLen;

                if (!OK)
                {
                    replyComplete = true;
                }
                else if (!readFF_A)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Not yet found any <FF> bytes.                          //
                    // Search buffer to see if first, or both first and       //
                    // second <FF> (as applicable) are present.               //
                    //                                                        //
                    // This branch is expected to be entered since we include //
                    // a PJL ECHO commmand in the job header.                 //
                    //                                                        //
                    //--------------------------------------------------------//

                    for (Int32 i = offset; i < endOffset; i++)
                    {
                        if (replyData[i] == 0x0c)
                        {
                            if ((readFF_A) && (replyData[endOffset - 1] == 0x0c))
                                replyComplete = true;
                            else
                                readFF_A = true;
                        }
                    }
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // <FF> at end of ECHO text is either not expected, or    //
                    // has been read in a previous read action.               // 
                    //                                                        //
                    //--------------------------------------------------------//

                    if (replyData[endOffset - 1] == 0x0c)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Terminating <FF> found (as last byte of data       //
                        // returned by current read action).                  // 
                        //                                                    //
                        //----------------------------------------------------//

                        replyComplete = true;
                    }
                }

                offset += blockLen;
                bufRem -= blockLen;

                if (bufRem <= 0)
                    replyComplete = true;
            }

            replyLen = endOffset;

            TargetCore.responseCloseConnection();

            return System.Text.Encoding.ASCII.GetString(replyData,
                                                         0,
                                                         replyLen);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e n d R e q u e s t                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Send generated status readback request data to target.             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void sendRequest()
        {
            TargetCore.requestStreamWrite(true);
        }
    }
}
