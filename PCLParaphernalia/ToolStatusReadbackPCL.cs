using System;
using System.IO;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides PCL support for the PrinterInfo tool.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class ToolStatusReadbackPCL
    {
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e R e q u e s t                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate status readback request data.                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void generateRequest(BinaryWriter prnWriter,
                                           Int32 indexEntity,
                                           Int32 indexLocType)
        {
            String seq;

            PCLEntityTypes.eType entityType;

            String entityIdPCL;
            String locTypeIdPCL;

            if (indexEntity < PCLEntityTypes.getCount())
            {
                entityType = PCLEntityTypes.getType(indexEntity);
                entityIdPCL = PCLEntityTypes.getIdPCL(indexEntity);
                locTypeIdPCL = PCLLocationTypes.getIdPCL(indexLocType);

                if (entityType == PCLEntityTypes.eType.Memory)
                {
                    seq = "\x1b" + "*s" +
                                   entityIdPCL +
                                   "M";         // entity = memory
                }
                else
                {
                    seq = "\x1b" + "*s" +
                                   locTypeIdPCL +
                                   "t" +       // loc. type 
                                   "0u" +       // loc. unit = all
                                   entityIdPCL +
                                   "I";         // entity
                }

                prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e a d R e s p o n s e                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Read response from target.                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String readResponse ()
        {
            const Int32 replyBufLen = 32768;

            Byte[] replyData = new Byte[replyBufLen];

            Int32 replyLen = 0;

        //  Boolean readFF_A = true;            // only one <FF> expected //
            Boolean OK = false;
            Boolean replyComplete = false;

            Int32 offset = 0;
            Int32 endOffset = 0;
            Int32 bufRem = replyBufLen;
            Int32 blockLen = 0;

            while (!replyComplete)
            {
                OK = TargetCore.responseReadBlock (offset,
                                                   bufRem,
                                                   ref replyData,
                                                   ref blockLen);

                endOffset = offset + blockLen;

                if (!OK)
                {
                    replyComplete = true;
                }
                /*else if (!readFF_A)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Not yet found any <FF> bytes.                          //
                    // Search buffer to see if first, or both first and       //
                    // second <FF> (as applicable) are present.               //
                    //                                                        //
                    // This branch will never be entered unless we include a  //
                    // PJL ECHO commmand in the job header; included in case  //
                    // we ever do this.                                       //
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
                }*/
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

            return System.Text.Encoding.ASCII.GetString (replyData,
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

        public static void sendRequest ()
        {
            TargetCore.requestStreamWrite(true);
        }
    }
}
