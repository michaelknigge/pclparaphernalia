using System;
using System.Data;
using System.Text;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides print-language-independent routines associated with
    /// 'parsing' of print file data strings.
    /// 
    /// © Chris Hutchinson 2012
    /// 
    /// </summary>

    static class PrnParseData
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const Int32 _decodeSliceMax = 4;
        
        public enum eCharType
        {
            C0Controls = 0,
            Space,
            Graphic,
            DEL,
            C1Controls,
            Extended,
            TwoByte
        }
      
        const String cCcName_0x07        = "<BEL>",
                     cCcName_0x0a        = "<LF>",
                     cCcName_0x0c        = "<FF>",
                     cCcName_0x0d        = "<CR>",
                     cCcName_0x1b        = "<Esc>",
                     cCcName_0x7f        = "<DEL>",
                     cCcName_Extended    = "<ext>",
                     cCcName_Unicode     = "<U+x>";

        const Int32 cC0NameLen           = 5;
        const Int32 cC1NameLen           = 6;

        const String cC0Names_List =     "<NUL><SOH><STX><ETX>" +
                                         "<EOT><ENQ><ACK><BEL>" +
                                         "<BS> <HT> <LF> <VT> " +
                                         "<FF> <CR> <SO> <SI> " +
                                         "<DLE><DC1><DC2><DC3>" +
                                         "<DC4><NAK><SYN><ETB>" +
                                         "<CAN><EM> <SUB><Esc>" +
                                         "<FS> <GS> <RS> <US> " +
                                         "<SP> ";

        const String cC1Names_List =     "<PAD> <HOP> <BPH> <NBH> " +
                                         "<IND> <NEL> <SSA> <ESA> " +
                                         "<HTS> <HTJ> <VTS> <PLD> " +
                                         "<PLU> <RI>  <SS2> <SS3> " +
                                         "<DCS> <PU1> <PU2> <STS> " +
                                         "<CCH> <MW>  <SPA> <EPA> " +
                                         "<SOS> <SGCI><SC1> <CSI> " +
                                         "<ST>  <OSC> <PM>  <APC> ";

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        static PrnParse.eParseType _parseType;

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n i t i a l i s e R u n T y p e                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set value indicating current tool ('PrnAnalyse' or 'MakeMacro').   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void initialiseRunType(PrnParse.eParseType parseType)
        {
            _parseType = parseType;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s B i n a r y                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Displays details of a sequence of binary data (i.e. a sequence     //
        // which normally consists of unprintable characters).                //
        // A flag indicates whether the hexadecimal data should be displayed, //
        // or just a count of the data characters.                            //
        //                                                                    //
        //---------------------------------------------- ---------------------//

        public static void processBinary(
            DataTable table,
            PrnParseConstants.eOvlShow makeOvlShow,
            Byte[] buf,
            Int32 fileOffset,
            Int32 seqOffset,
            Int32 seqLen,
            String typeText,
            Boolean showAsHex,
            Boolean indent,
            Boolean showOffset,
            PrnParseConstants.eOptOffsetFormats indxOffsetFormat,
            Int32 level)
        {
            const Int32 iBytesPerLine = 16;

            if (!showAsHex)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Just indicate number of bytes.                             //
                //                                                            //
                //------------------------------------------------------------//

                if (showOffset)
                {
                    PrnParseCommon.addDataRow (
                        PrnParseRowTypes.eType.DataBinary,
                        table,
                        makeOvlShow,
                        indxOffsetFormat,
                        fileOffset + seqOffset,
                        level,
                        typeText,
                        "[ " + seqLen + " bytes ]",
                        "");
                }
                else
                {
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.DataBinary,
                        table,
                        makeOvlShow,
                        "",
                        typeText,
                        "[ " + seqLen + " bytes ]",
                        "");
                }
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // Display hex characters.                                    //
                // Split the sequence into 'slices'.                          //
                // Each 'slice' will provide one line of the output display.  //
                // Note that the last 'slice' may be less than a full line.   //
                //                                                            //
                //------------------------------------------------------------//

                Int32 sliceLen,
                      crntOffset; ;

                Boolean firstLine;

                String preamble;

                if (indent)
                    preamble = "    [ ";
                else
                    preamble = "[ ";

                firstLine = true;
                crntOffset = seqOffset;

                for (int i = 0; i < seqLen; i += iBytesPerLine)
                {
                    if ((i + iBytesPerLine) > seqLen)
                        sliceLen = seqLen - i;
                    else
                        sliceLen = iBytesPerLine;

                    if (firstLine)
                    {
                        if (showOffset)
                        {
                            PrnParseCommon.addDataRow (
                                PrnParseRowTypes.eType.DataBinary,
                                table,
                                makeOvlShow,
                                indxOffsetFormat,
                                fileOffset + crntOffset,
                                level,
                                typeText,
                                "[ " + seqLen + " bytes ]",
                                preamble +
                                PrnParseCommon.byteArrayToHexString (buf,
                                                                     crntOffset,
                                                                     sliceLen) +
                                "]");
                        }
                        else
                        {
                            PrnParseCommon.addTextRow (
                                PrnParseRowTypes.eType.DataBinary,
                                table,
                                makeOvlShow,
                                "",
                                typeText,
                                "[ " + seqLen + " bytes ]",
                                preamble +
                                PrnParseCommon.byteArrayToHexString (buf,
                                                                     crntOffset,
                                                                     sliceLen) +
                                "]");
                        }
                    }
                    else
                    {
                        if (showOffset)
                        {
                            PrnParseCommon.addDataRow (
                                PrnParseRowTypes.eType.DataBinary,
                                table,
                                makeOvlShow,
                                indxOffsetFormat,
                                fileOffset + crntOffset,
                                level,
                                "",
                                "",
                                preamble +
                                PrnParseCommon.byteArrayToHexString (buf,
                                                                     crntOffset,
                                                                     sliceLen) +
                                "]");
                        }
                        else
                        {
                            PrnParseCommon.addTextRow (
                                PrnParseRowTypes.eType.DataBinary,
                                table,
                                makeOvlShow,
                                "",
                                "",
                                "",
                                preamble +
                                PrnParseCommon.byteArrayToHexString (buf,
                                                                     crntOffset,
                                                                     sliceLen) +
                                "]");
                        }
                    }
                    
                    firstLine = false;
                    crntOffset = crntOffset + sliceLen;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s B y t e                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Examine supplied byte and return a string containing either the    //
        // character represented by the byte, or (for non-graphic characters) //
        // a representation of the character.                                 //
        // The representation used depends on the current option settings.    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String processByte(
            Byte dataByte,
            PrnParseConstants.eOptCharSetSubActs showCCAction,
            Byte showSubCode,
            PrnParseConstants.eOptCharSets showCharSet)
        {
            Int32 charVal = dataByte;

            return processValue (charVal,
                                 showCCAction,
                                 showSubCode,
                                 showCharSet);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s B y t e P a i r                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Examine supplied double-byte character and return a String         //
        // containing:                                                        //
        //    -  If the character is outside the Basic Multilingual Plane     //
        //       (i.e. the value is greater than 0x00ff), or is considered to //
        //       be a non-graphic character, a representation of the          //
        //       character (the representation used depends on the current    //
        //       option settings).                                            //
        //    -  Otherwise, the (single-byte) Windows ANSI character          //
        //       corresponding to the supplied 0x00xy value).                 //
        //                                                                    //
        // The character is supplied as two separate single-byte characters;  //
        // which of these is interpreted as the most significant byte depends //
        // on the indicated byte order.                                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String processBytePair(
            Byte byteA,
            Byte byteB,
            Boolean highByteFirst,
            PrnParseConstants.eOptCharSetSubActs showCCAction,
            Byte showSubCode,
            PrnParseConstants.eOptCharSets showCharSet)
        {
            Byte highByte,
                 lowByte;

            Int32 charVal;

            if (highByteFirst)
            {
                highByte = byteA;
                lowByte  = byteB;
            }
            else
            {
                highByte = byteB;
                lowByte  = byteA;
            }

            charVal = (highByte * 256) + lowByte;

            return processValue (charVal,
                                 showCCAction,
                                 showSubCode,
                                 showCharSet);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s L i n e s                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Output buffer contents as <LF>-terminated lines, until next <Esc>  //
        // character, or end of buffer (subject to any maximum length         //
        // restriction).                                                      //
        // Long lines are split into shorter slices.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean processLines(
            DataTable table,
            PrnParseConstants.eOvlShow makeOvlShow,
            PrnParseLinkData linkData,
            ToolCommonData.ePrintLang crntPDL,
            Byte[] buf,
            Int32 fileOffset,
            Int32 maxLen,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            ref Boolean continuation,
            Boolean showOffset,
            Boolean ignoreFirst,
            Boolean ignoreFEs,
            Byte termChar,
            String typeText,
            Int32 textMode,
            PrnParseConstants.eOptCharSetSubActs showCCAction,
            Byte showSubCode,
            PrnParseConstants.eOptCharSets showCharSet,
            PrnParseConstants.eOptOffsetFormats indxOffsetFormat,
            Int32 level)
        {
            Boolean foundTerm = true;

            Byte c1,
                 c2;

            String showChar = "";
            String line;

            Int32 len,
                  protectedLen,
                  offset,
                  lineStart,
                  lineLen;

            Boolean foundEsc,
                    foundLF,
                    firstChar,
                    ignoreFE,
                    multiByteChar,
                    utf8Char,
                    multiByteData,
                    contLine;

            Boolean knownCC = false;
            Boolean optCCLineTerm = false;
            Boolean pageMarked = false;

            PrnParseConstants.eOvlAct makeOvlAct;
            PrnParseConstants.eOvlShow crntOvlShow;

            String descCC = "";
            String mnemonicCC = "";

            //----------------------------------------------------------------//
            //                                                                //
            // Initialise.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            makeOvlAct = PrnParseConstants.eOvlAct.None;
            crntOvlShow = makeOvlShow;

            foundEsc = false;
            foundLF = false;
            foundTerm = false;
            firstChar = true;
            continuation = false;
            contLine = false;
            multiByteData = false;

            line = "";

            offset = bufOffset;
            lineStart = offset;
            lineLen = 0;

            protectedLen = bufRem - maxLen;

            if (protectedLen > 0)
            {
                len = maxLen;
            }
            else
            {
                len = bufRem;
                protectedLen = 0;
            }

            while (!foundEsc && !foundTerm && !continuation && len > 0)
            {
                if (firstChar && ignoreFirst)
                    ignoreFE = true;
                else
                    ignoreFE = false;

                c1 = buf[offset];

                //------------------------------------------------------------//
                //                                                            //
                // Check the text parsing method to see if we have a 1-byte   //
                // or 2-byte (or more) character.                             //
                // Method  0   onebyte_default                                //
                //             always 1-byte                                  //
                //         1   onebyte_alt                                    //
                //             always 1-byte                                  //
                //         2   twobyte                                        //
                //             always 2-byte - not sure if this is supported  //
                //             even if it is, how can we distinguish between  //
                //             two bytes values starting with 0x1B, and real  //
                //             (presumably single-byte mode) escape sequences?//
                //        21   multibyte_Asian7bit                            //
                //             2-byte if first byte is in range 0x21->0xFF    //
                //        31   multibyte_ShiftJIS                             //
                //             2-byte if first byte is in range 0x81->0x9F    //
                //                    if first byte is in range 0xE0->0xFC    //
                //        38   multibyte_Asian8bit                            //
                //             2-byte if first byte is in range 0x80->0xFF    //
                //        83   multibyte_UTF8                                 //
                //             multi-byte if first byte is > 0x7F             //
                //      1008   multibyte_UTF8-alt                             //
                //             multi-byte if first byte is > 0x7F             //
                //                                                            //
                //------------------------------------------------------------//

                multiByteChar = false;
                utf8Char      = false;

                if (textMode ==
                    (Int32) PCLTextParsingMethods.ePCLVal.m21_1_or_2_byte_Asian7bit)
                {
                    if (c1 >= 0x21)
                        multiByteChar = true;
                }
                else if (textMode ==
                    (Int32) PCLTextParsingMethods.ePCLVal.m2_2_byte)
                {
                    if (c1 != 0x1b)
                        multiByteChar = true;
                }
                else if (textMode ==
                    (Int32) PCLTextParsingMethods.ePCLVal.m31_1_or_2_byte_ShiftJIS)
                {
                    if ((c1 >= 0x81) && (c1 <= 0x9f))
                        multiByteChar = true;
                    else if ((c1 >= 0xe0) && (c1 <= 0xfc))
                        multiByteChar = true;
                }
                else if (textMode ==
                    (Int32) PCLTextParsingMethods.ePCLVal.m38_1_or_2_byte_Asian8bit)
                {
                    if (c1 >= 0x80)
                        multiByteChar = true;
                }
                else if (textMode ==
                    (Int32) PCLTextParsingMethods.ePCLVal.m83_UTF8)
                {
                    if (c1 >= 0x80)
                        utf8Char = true;
                }
                else if (textMode ==
                    (Int32) PCLTextParsingMethods.ePCLVal.m1008_UTF8_alt)
                {
                    if (c1 >= 0x80)
                        utf8Char = true;
                }

                if ((multiByteChar) || (utf8Char))
                    multiByteData = true;

                //------------------------------------------------------------//
                //                                                            //
                // Now check the character, or characters, depending on the   //
                // text parsing method.                                       //
                //                                                            //
                //------------------------------------------------------------//

                if (multiByteChar)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Potential 1 or 2 byte character.                       //
                    //                                                        //
                    //--------------------------------------------------------//

                    pageMarked = true;

                    if (len == 1)
                    {
                        continuation = true;
                    }
                    else
                    {
                        lineLen += 2;
                        offset++;
                        len--;

                        c2 = buf[offset];

                        showChar = processBytePair (c1, c2, true,
                                                    showCCAction,
                                                    showSubCode,
                                                    showCharSet);

                        line = line + showChar;
                    }
                }
                else if (utf8Char)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // UTF-8 character.                                       //
                    //                                                        //
                    //--------------------------------------------------------//

                    Int32 seqLen = PrnParseDataUTF8.cTrailByteCountsUTF8[c1] + 1;

                    pageMarked = true;

                    if (seqLen > len)
                    {
                        continuation = true;
                    }
                    else
                    {
                        Int32 codepoint = 0;

                        PrnParseDataUTF8.eUTF8Result result;

                        Byte [] seq = new Byte [seqLen];

                        for (Int32 i = 0; i < seqLen; i++)
                        {
                            seq[i] = buf[offset + i];
                        }
                        
                        result = PrnParseDataUTF8.checkUTF8Seq(seq,
                                                               seqLen,
                                                               ref codepoint);
                        
                        if (result == PrnParseDataUTF8.eUTF8Result.success)
                            showChar = "[U+" + codepoint.ToString ("x4") + "]";
                        else
                            showChar = "[invalid UTF-8]";
                        
                        line = line + showChar;

                        lineLen += seqLen;
                        offset+= (seqLen - 1);
                        len-= (seqLen - 1);
                    }
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Single byte (not multi-byte) character.                //
                    // Check for special characters first.                    //
                    //                                                        //
                    //--------------------------------------------------------//

                    if ((c1 == termChar) && !ignoreFE)
                    {
                        foundTerm = true;
                    }

                    if (!ignoreFEs)
                    {
                        if ((c1 == PrnParseConstants.asciiEsc) && !ignoreFE)
                        {
                            foundEsc = true;
                        }
                        else if (crntPDL == ToolCommonData.ePrintLang.PCL)
                        {
                            knownCC = PCLControlCodes.checkControlCode (
                                level,
                                c1,
                                ref optCCLineTerm,
                                ref mnemonicCC,
                                ref makeOvlAct,
                                ref descCC);

                            if (makeOvlAct == PrnParseConstants.eOvlAct.PageMark)
                                pageMarked = true;
                        }
                        else if (crntPDL == ToolCommonData.ePrintLang.HPGL2)
                        {
                            knownCC = PCLControlCodes.checkControlCode (
                                level,
                                c1,
                                ref optCCLineTerm,
                                ref mnemonicCC,
                                ref makeOvlAct,
                                ref descCC);

                            if (makeOvlAct == PrnParseConstants.eOvlAct.PageMark)
                                pageMarked = true;

                            if (knownCC)
                                foundTerm = true;
                        }

                        if (c1 == PrnParseConstants.asciiLF)
                            foundLF = true;
                    }

                    //--------------------------------------------------------//
                    //                                                        //
                    // Now add character, or (for control codes) the desired  //
                    // representation of the character (unless <FF> or <Esc>  //
                    // character just found) to the output line.              //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (!knownCC && !foundEsc && !foundLF)
                    {
                        pageMarked = true;

                        showChar = processByte (c1,
                                                showCCAction,
                                                showSubCode,
                                                showCharSet);

                        line = line + showChar;
                        lineLen++;
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Have we found anything special?                            //
                //                                                            //
                //------------------------------------------------------------//

                if (continuation)
                {
                    linkData.setContinuation (
                        PrnParseConstants.eContType.PCLMultiByteData);
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Have we found anything special?                        //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (foundLF || knownCC || foundEsc)
                    {
                        if (line != "")
                        {
                            //------------------------------------------------//
                            //                                                //
                            // <LF>, <FF> or <Esc> character found.           //
                            // Output line up to (and for <LF>, including)    //
                            // this terminator.                               //
                            //                                                //
                            //------------------------------------------------//

                            if (!contLine)
                            {
                                String seqBytes;

                                if (multiByteData)
                                { 
                                    seqBytes = showElementSeqData(buf,
                                                                  lineStart,
                                                                  lineLen);
                                }
                                else
                                {
                                    seqBytes = "";
                                }

                                if (showOffset)
                                    PrnParseCommon.addDataRow (
                                        PrnParseRowTypes.eType.DataText,
                                        table,
                                        PrnParseConstants.eOvlShow.None,
                                        indxOffsetFormat,
                                        fileOffset + lineStart,
                                        level,
                                        typeText,
                                        seqBytes,
                                        line);
                                else
                                    PrnParseCommon.addTextRow (
                                        PrnParseRowTypes.eType.DataText,
                                        table,
                                        PrnParseConstants.eOvlShow.None,
                                        "",
                                        typeText,
                                        "",
                                        line);
                            }
                            else
                            {
                                if (showOffset)
                                    PrnParseCommon.addDataRow (
                                        PrnParseRowTypes.eType.DataText,
                                        table,
                                        PrnParseConstants.eOvlShow.None,
                                        indxOffsetFormat,
                                        fileOffset + lineStart,
                                        level,
                                        "",
                                        "",
                                        line);
                                else
                                    PrnParseCommon.addTextRow (
                                        PrnParseRowTypes.eType.DataText,
                                        table,
                                        PrnParseConstants.eOvlShow.None,
                                        "",
                                        "",
                                        "",
                                        line);
                            }

                            contLine = false;
                        }

                        if (knownCC)
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Control code character found.                  //
                            // Output this as a separate item.                //
                            //                                                //
                            //------------------------------------------------//

                            if ((_parseType == PrnParse.eParseType.MakeOverlay)
                                                    &&
                                (makeOvlAct !=
                                    PrnParseConstants.eOvlAct.None))
                            {
                                linkData.MakeOvlAct = makeOvlAct;

                                foundTerm = PrnParseMakeOvl.checkActionPCL (
                                    false,
                                    true,
                                    -1,
                                    offset,
                                    1,
                                    fileOffset,
                                    linkData,
                                    table,
                                    indxOffsetFormat);

                                makeOvlAct = linkData.MakeOvlAct;

                                if (makeOvlAct == PrnParseConstants.eOvlAct.Adjust)
                                    makeOvlShow = PrnParseConstants.eOvlShow.None;

                                linkData.MakeOvlAct = makeOvlAct;
                            }

                            knownCC = false;
                            foundLF = false;

                            if (showOffset)
                                PrnParseCommon.addDataRow (
                                    PrnParseRowTypes.eType.PCLControlCode,
                                    table,
                                    makeOvlShow,
                                    indxOffsetFormat,
                                    fileOffset + offset,
                                    level,
                                    "PCL Control Code",
                                    "0x" + c1.ToString ("x2"),
                                    mnemonicCC + ": " + descCC);
                            else
                                PrnParseCommon.addTextRow (
                                    PrnParseRowTypes.eType.PCLControlCode,
                                    table,
                                    makeOvlShow,
                                    "",
                                    "PCL Control Code",
                                    "0x" + c1.ToString ("x2"),
                                    mnemonicCC + ": " + descCC);

                            contLine = false;
                        }

                        line = "";
                        lineStart = offset + 1;
                    }
                    else if (line.Length >= PrnParseConstants.cRptA_colMax_Desc)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Current line has reached maximum item size.        //
                        // Output this slice, and start new one.              //
                        //                                                    //
                        //----------------------------------------------------//

                        if (!contLine)
                        {
                            String seqBytes;

                            if (multiByteData)
                            {
                                seqBytes = showElementSeqData(buf,
                                                              lineStart,
                                                              lineLen);
                            }
                            else
                            {
                                seqBytes = "";
                            }

                            if (showOffset)
                                PrnParseCommon.addDataRow (
                                    PrnParseRowTypes.eType.DataText,
                                    table,
                                    makeOvlShow,
                                    indxOffsetFormat,
                                    fileOffset + lineStart,
                                    level,
                                    typeText,
                                    seqBytes,
                                    line);
                            else
                                PrnParseCommon.addTextRow (
                                    PrnParseRowTypes.eType.DataText,
                                    table,
                                    PrnParseConstants.eOvlShow.None,
                                    "",
                                    typeText,
                                    "",
                                    line);

                            contLine = true;
                        }
                        else
                        {
                            if (showOffset)
                                PrnParseCommon.addDataRow (
                                    PrnParseRowTypes.eType.DataText,
                                    table,
                                    makeOvlShow,
                                    indxOffsetFormat,
                                    fileOffset + lineStart,
                                    level,
                                    "",
                                    "",
                                    line);
                            else
                                PrnParseCommon.addTextRow (
                                    PrnParseRowTypes.eType.DataText,
                                    table,
                                    makeOvlShow,
                                    "",
                                    "",
                                    "",
                                    line);
                        }

                        line = "";
                        lineStart = offset + 1;
                        lineLen = 0;
                    }


                    if (!foundEsc)
                    {
                        offset++;
                        len--;
                    }

                    firstChar = false;
                }
            }

            if (line != "")
            {
                //------------------------------------------------------------//
                //                                                            //
                // Output any remaining slice (buffer exhausted).             //
                //                                                            //
                //------------------------------------------------------------//

                if (!contLine)
                {
                    if (showOffset)
                        PrnParseCommon.addDataRow (
                            PrnParseRowTypes.eType.DataText,
                            table,
                            makeOvlShow,
                            indxOffsetFormat,
                            fileOffset + lineStart,
                            level,
                            typeText,
                            "",
                            line);
                    else
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.DataText,
                            table,
                            makeOvlShow,
                            "",
                            typeText,
                            "",
                            line);
                }
                else
                {
                    if (showOffset)
                        PrnParseCommon.addDataRow (
                            PrnParseRowTypes.eType.DataText,
                            table,
                            makeOvlShow,
                            indxOffsetFormat,
                            fileOffset + lineStart,
                            level,
                            "",
                            "",
                            line);
                    else
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.DataText,
                            table,
                            makeOvlShow,
                            "",
                            "",
                            "",
                            line);
                }
            }

            linkData.MakeOvlPageMark = pageMarked;

            bufOffset = offset;
            bufRem = len + protectedLen;

            return foundTerm;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s V a l u e                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Examine supplied character value and return a String containing:   //
        //    -  If the character is outside the Basic Multilingual Plane     //
        //       (i.e. the value is greater than 0x00ff), or is considered to //
        //       be a non-graphic character, a representation of the          //
        //       character (the representation used depends on the current    //
        //       option settings).                                            //
        //    -  Otherwise, the (single-byte) Windows ANSI character          //
        //       corresponding to the supplied 0x00xy value).                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static String processValue(
            Int32 charVal,
            PrnParseConstants.eOptCharSetSubActs showCCAction,
            Byte showSubCode,
            PrnParseConstants.eOptCharSets showCharSet)
        {
            String outStr = "";

            Byte highByte,
                 lowByte;

            eCharType charType;

            String ccName;

            highByte = (Byte) ((charVal >> 8) & 0xff);
            lowByte  = (Byte) (charVal & 0xff);

            //----------------------------------------------------------------//
            //                                                                //
            // Determine character type, according to character set           //
            // preference.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            if (charVal > 0xff)
                charType = eCharType.TwoByte;
            else if (charVal == PrnParseConstants.asciiSpace)
                charType = eCharType.Space;
            else if (charVal == PrnParseConstants.asciiDEL)
                charType = eCharType.DEL;
            else if (charVal < 0x21)
                charType = eCharType.C0Controls;
            else if ((showCharSet == PrnParseConstants.eOptCharSets.ASCII)
                                    &&
                        (charVal >= 0x80))
                charType = eCharType.Extended;
            else if ((showCharSet == PrnParseConstants.eOptCharSets.ISO_8859_1)
                                    &&
                        (charVal >= 0xa0))
                charType = eCharType.Graphic;
            else if ((showCharSet == PrnParseConstants.eOptCharSets.ISO_8859_1)
                                       &&
                        (charVal >= 0x80))
                charType = eCharType.C1Controls;
            else
                charType = eCharType.Graphic;

            //----------------------------------------------------------------//
            //                                                                //
            // Interpret.                                                     //
            //                                                                //
            //----------------------------------------------------------------//

            switch (showCCAction)
            {
                case PrnParseConstants.eOptCharSetSubActs.Mnemonics:

                    //--------------------------------------------------------//
                    //                                                        //
                    // Show non-graphic characters as mnemonics (e.g. <NUL>,  //
                    // <LF>).                                                 //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (charType == eCharType.TwoByte)
                    {
                        outStr = cCcName_Unicode;
                    }
                    else if (charType == eCharType.DEL)
                    {
                        outStr = cCcName_0x7f;
                    }
                    else if (charType == eCharType.C0Controls)
                    {
                        ccName = cC0Names_List.Substring (
                            (charVal * cC0NameLen),
                            cC0NameLen);

                        if (ccName.Substring (cC0NameLen - 1, 1) == " ")
                            outStr = ccName.Substring (0, cC0NameLen - 1);
                        else
                            outStr = ccName;
                    }
                    else if (charType == eCharType.Extended)
                    {
                        outStr = cCcName_Extended;
                    }
                    else if (charType == eCharType.C1Controls)
                    {
                        ccName = cC1Names_List.Substring (
                            ((charVal - 128) * cC1NameLen),
                            cC1NameLen);

                        if (ccName.Substring (cC1NameLen - 2, 2) == "  ")
                            outStr = ccName.Substring (0, cC1NameLen - 2);
                        else if (ccName.Substring (cC1NameLen - 1, 1) == " ")
                            outStr = ccName.Substring (0, cC1NameLen - 1);
                        else
                            outStr = ccName;
                    }
                    else
                    {
                        outStr = PrnParseCommon.byteToString (lowByte);
                    }

                    break;

                case PrnParseConstants.eOptCharSetSubActs.MnemonicsIncSpace:

                    //--------------------------------------------------------//
                    //                                                        //
                    // Show non-graphic characters as mnemonics (e.g. <NUL>,  //
                    // <LF>), and also show space using a mnemonic (<SP>).    //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (charType == eCharType.TwoByte)
                    {
                        outStr = cCcName_Unicode;
                    }
                    else if (charType == eCharType.DEL)
                    {
                        outStr = cCcName_0x7f;
                    }
                    else if ((charType == eCharType.C0Controls)
                                            ||
                            (charType == eCharType.Space))
                    {
                        ccName = cC0Names_List.Substring (
                                    (charVal * cC0NameLen),
                                    cC0NameLen);

                        if (ccName.Substring (cC0NameLen - 1, 1) == " ")
                            outStr = ccName.Substring (0, cC0NameLen - 1);
                        else
                            outStr = ccName;
                    }
                    else if (charType == eCharType.Extended)
                    {
                        outStr = cCcName_Extended;
                    }
                    else if (charType == eCharType.C1Controls)
                    {
                        ccName = cC1Names_List.Substring (
                            ((charVal - 128) * cC1NameLen),
                            cC1NameLen);

                        if (ccName.Substring (cC1NameLen - 2, 2) == "  ")
                            outStr = ccName.Substring (0, cC1NameLen - 2);
                        else if (ccName.Substring (cC1NameLen - 1, 1) == " ")
                            outStr = ccName.Substring (0, cC1NameLen - 1);
                        else
                            outStr = ccName;
                    }
                    else
                    {
                        outStr = PrnParseCommon.byteToString (lowByte);
                    }

                    break;

                case PrnParseConstants.eOptCharSetSubActs.Hex:

                    //--------------------------------------------------------//
                    //                                                        //
                    // Show non-graphic characters as hexadecimal characters. //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (charType == eCharType.TwoByte)
                        outStr = "[" +
                                 PrnParseCommon.byteToHexString (highByte) +
                                 PrnParseCommon.byteToHexString (lowByte) +
                                 "]";
                    else if ((charType == eCharType.Space)
                                        ||
                            (charType == eCharType.Graphic))
                        outStr = PrnParseCommon.byteToString (lowByte);
                    else
                        outStr = "[" +
                                 PrnParseCommon.byteToHexString (lowByte) +
                                 "]";

                    break;

                case PrnParseConstants.eOptCharSetSubActs.Dots:

                    //--------------------------------------------------------//
                    //                                                        //
                    // Show non-graphic characters as dots (".").             //
                    //                                                        //
                    //--------------------------------------------------------//

                    if ((charType == eCharType.Space) ||
                        (charType == eCharType.Graphic))
                        outStr = PrnParseCommon.byteToString (lowByte);
                    else
                        outStr = ".";

                    break;

                case PrnParseConstants.eOptCharSetSubActs.Spaces:

                    //--------------------------------------------------------//
                    //                                                        //
                    // Show non-graphic characters as spaces.                 //
                    //                                                        //
                    //--------------------------------------------------------//

                    if ((charType == eCharType.Space) ||
                        (charType == eCharType.Graphic))
                        outStr = PrnParseCommon.byteToString (lowByte);
                    else
                        outStr = " ";

                    break;

                case PrnParseConstants.eOptCharSetSubActs.Substitute:

                    //--------------------------------------------------------//
                    //                                                        //
                    // Show non-graphic characters using the specified        //
                    // substitute character.                                  //
                    //                                                        //
                    //--------------------------------------------------------//

                    if ((charType == eCharType.Space) ||
                        (charType == eCharType.Graphic))
                        outStr = PrnParseCommon.byteToString (lowByte);
                    else
                        outStr = PrnParseCommon.byteToString (showSubCode);

                    break;
            }

            return outStr;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s h o w E l e m e n t S e q D a t a                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Convert slice of supplied sequence data to hexadecimal notation in //
        // the sequence buffer.                                               //
        // Do this only when the current chunk is complete, unless verbose    //
        // mode is set, or an error has been detected.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static String showElementSeqData( Byte[] buf,
                                           Int32  sliceOffset,
                                           Int32  sliceLen)
        {
            StringBuilder seq = new StringBuilder();

            Byte crntByte;

            Int32 hexPtr,
                  hexStart = 0,
                  hexEnd = 0,
                  sub;

            Boolean useEllipsis;

            Char[] hexBuf = new Char[(_decodeSliceMax * 2) + 1];

            useEllipsis = false;

            //-----------------------------------------------------------------//
            //                                                                 //
            // Convert first few characters of slice.                          //
            //                                                                 //
            //-----------------------------------------------------------------//

            if (sliceLen > _decodeSliceMax)
            {
                hexStart = sliceOffset;
                hexEnd = sliceOffset + _decodeSliceMax - 1;
                useEllipsis = true;
            }
            else
            {
                hexStart = sliceOffset;
                hexEnd = sliceOffset + sliceLen;
            }

            hexPtr = 0;

            for (Int32 j = hexStart; j < hexEnd; j++)
            {
                sub = (buf[j]);
                sub = sub >> 4;
                crntByte = PrnParseConstants.cHexBytes[sub];
                hexBuf[hexPtr++] = (Char)crntByte;

                sub = (buf[j] & 0x0f);
                crntByte = PrnParseConstants.cHexBytes[sub];
                hexBuf[hexPtr++] = (Char)crntByte;
            }

            seq.Clear();
            seq.Append("0x");
            seq.Append(hexBuf, 0, hexPtr);

            if (useEllipsis)
                seq.Append("..");

            return seq.ToString();
        }
    }
}