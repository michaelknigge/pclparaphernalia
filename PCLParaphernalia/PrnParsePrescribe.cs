using System;
using System.Data;
using System.Text;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides routines associated with 'parsing' of
	/// Kyocera Prescribe commands.
    /// 
    /// © Chris Hutchinson 2017
    /// 
    /// </summary>

    class PrnParsePrescribe
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private const Int32 _maxCmdLen = 256;

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private PrnParseLinkData _linkData;

        private PrnParseOptions _options;

        private DataTable _table;

        private Byte[] _buf;

        private Int32 _analysisLevel;

        private Int32 _fileOffset;
        private Int32 _endOffset;

        private PrnParseConstants.eOptOffsetFormats _indxOffsetFormat;

        private PrnParseConstants.eOptCharSetSubActs _indxCharSetSubAct;
        private PrnParseConstants.eOptCharSets _indxCharSetName;
        private Int32 _valCharSetSubCode;

        private ASCIIEncoding _ascii = new ASCIIEncoding();

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P r n P a r s e P r e s c r i b e                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PrnParsePrescribe()
        {
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // f i n d C o m m a n d T e r m i n a t o r                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Search for Prescribe command terminator character.                 //
        // This is a semi-colon (0x3b) character.                             //
        // But we may encounter an Escape character (<Esc>, 0x1b) signalling  //
        // return to PCL.                                                     //
        //                                                                    //
        // This is to make sure that the termination character is in the      //
        // buffer before processing the command, so that we don't have to     //
        // cater for doing a 'continuation' read part way through processing  //
        // the command.                                                       //
        //                                                                    //
        // Initiate continuation action if terminator is not found in buffer, //
        // subject to a maximum command length (to prevent recursive          //
        // continuation actions).                                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean findCommandTerminator(
            Int32 bufRem,
            Int32 bufOffset,
            ref Int32 commandLen,
            ref Boolean continuation)
        {
            PrnParseConstants.eContType contType =
                PrnParseConstants.eContType.None;

            Byte crntByte;

            Int32 cmdLen,
                  rem,
                  offset;

            Boolean foundEnd,
                    foundTerm;

            continuation = false;
            foundEnd = false;
            foundTerm = false;

            rem = bufRem;
            offset = bufOffset;
            cmdLen = 0;

            //----------------------------------------------------------------//
            //                                                                //
            // Search for termination character.                              //
            //                                                                //
            //----------------------------------------------------------------//

            while ((!foundEnd) && (rem > 0) && (cmdLen < _maxCmdLen))
            {
                crntByte = _buf[offset];

                if (crntByte == PrnParseConstants.asciiSemiColon)
                {
                    foundTerm = true;
                    foundEnd = true;
                    offset++;
                    cmdLen++;
                    rem--;
                }
                else if (crntByte == PrnParseConstants.asciiEsc)
                {
                    foundEnd = true;
                }
                else
                {
                    offset++;
                    cmdLen++;
                    rem--;
                }
            }

            if ((!foundEnd) && (cmdLen < _maxCmdLen))
            {
                //------------------------------------------------------------//
                //                                                            //
                // Termination character not found before buffer exhausted,   //
                // or maximum command length exceeded.                        //
                // Initiate (backtracking) continuation action.               //
                //                                                            //
                //------------------------------------------------------------//

                continuation = true;

                contType = PrnParseConstants.eContType.Prescribe;

                _linkData.setBacktrack(contType, -bufRem);

                commandLen = 0;
            }
            else
            {
                commandLen = cmdLen;
            }

            return foundTerm;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a r s e B u f f e r                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Parse provided buffer, assuming that the current print language is //
        // Prescribe.                                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean parseBuffer(
            Byte[] buf,
            ref Int32 fileOffset,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            ref ToolCommonData.ePrintLang crntPDL,
            ref Boolean endReached,
            PrnParseLinkData linkData,
            PrnParseOptions options,
            DataTable table)
        {
            Boolean seqInvalid;

            //----------------------------------------------------------------//
            //                                                                //
            // Initialise.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            _buf = buf;
            _linkData = linkData;
            _options = options;
            _table = table;
            _fileOffset = fileOffset;

            _analysisLevel = _linkData.AnalysisLevel;

            seqInvalid = false;

            //----------------------------------------------------------------//

            _indxOffsetFormat = _options.IndxGenOffsetFormat;

            _options.getOptCharSet(ref _indxCharSetName,
                                    ref _indxCharSetSubAct,
                                    ref _valCharSetSubCode);

            _endOffset = _options.ValCurFOffsetEnd;

            //----------------------------------------------------------------//

            if (linkData.isContinuation())
                seqInvalid = parseContinuation(ref bufRem,
                                                ref bufOffset,
                                                ref crntPDL,
                                                ref endReached);
            else
                seqInvalid = parseSequences(ref bufRem,
                                             ref bufOffset,
                                             ref crntPDL,
                                             ref endReached);

            if (endReached)
            {
                crntPDL = _linkData.PrescribeCallerPDL;
            }

            return seqInvalid;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a r s e C o n t i n u a t i o n                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Handle continuation situation signalled on last pass.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean parseContinuation(
            ref Int32 bufRem,
            ref Int32 bufOffset,
            ref ToolCommonData.ePrintLang crntPDL,
            ref Boolean endReached)
        {
            PrnParseConstants.eContType contType;

            contType = PrnParseConstants.eContType.None;

            Int32 prefixLen = 0,
                  contDataLen = 0,
                  downloadRem = 0;

            Boolean backTrack = false;

            Boolean invalidSeqFound = false;

            Byte prefixA = 0x00,
                 prefixB = 0x00;

            _linkData.getContData(ref contType,
                                   ref prefixLen,
                                   ref contDataLen,
                                   ref downloadRem,
                                   ref backTrack,
                                   ref prefixA,
                                   ref prefixB);

            if ((contType == PrnParseConstants.eContType.Prescribe)
                             ||
                (contType == PrnParseConstants.eContType.Reset))
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended with a partial match of a Prescribe //
                // command, or with insufficient characters to identify       //
                // the type of sequence.                                      //
                // The continuation action has already reset the buffer, so   //
                // now unset the markers.                                     //
                //                                                            //
                //------------------------------------------------------------//

                _linkData.resetContData();
            }

            if ((_endOffset != -1) && ((_fileOffset + bufOffset) > _endOffset))
                endReached = true;

            return invalidSeqFound;
        }


        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a r s e S e q u e n c e s                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Process sequences until end-point reached.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean parseSequences(
            ref Int32 bufRem,
            ref Int32 bufOffset,
            ref ToolCommonData.ePrintLang crntPDL,
            ref Boolean endReached)
        {
            Int64 startPos;

            Boolean continuation = false;
            Boolean langSwitch = false;
            Boolean badSeq = false;
            Boolean invalidSeqFound = false;

            continuation = false;
            startPos = _fileOffset + bufOffset;

            if (!_linkData.PrescribeIntroRead)
            {
                if ((_buf[bufOffset] ==
                        PrnParseConstants.prescribeSCRCDelimiter)
                                      && 
                    (_buf[bufOffset + 1] == _linkData.PrescribeSCRC)
                                      &&
                    (_buf[bufOffset + 2] ==
                        PrnParseConstants.prescribeSCRCDelimiter))
                {
                    String seq = _ascii.GetString(_buf, bufOffset, 3);
                    //  String desc = PrescribeCommands.getDescCmdIntro();
                    String desc = "";

                    PrescribeCommands.checkCmdIntro (ref desc,
                                                     _analysisLevel);

                    PrnParseCommon.addDataRow (
                        PrnParseRowTypes.eType.PrescribeCommand,
                        _table,
                        PrnParseConstants.eOvlShow.Remove,
                        _indxOffsetFormat,
                        _fileOffset + bufOffset,
                        _analysisLevel,
                        "Prescribe",
                        seq,
                        desc);

                    bufOffset += 3;
                    bufRem    -= 3;

                    _linkData.PrescribeIntroRead = true;
                }
                else
                {
                    // internal error ??
                }
            }

            while (!continuation && !langSwitch &&
                   !endReached && (bufRem > 0))
            {
                //------------------------------------------------------------//
                //                                                            //
                // Process data until language-switch or end of buffer, or    //
                // specified end point.                                       //
                //                                                            //
                //------------------------------------------------------------//

                if ((_endOffset != -1) &&
                    ((_fileOffset + bufOffset) > _endOffset))
                {
                    endReached = true;
                }
                else if (_buf[bufOffset] == PrnParseConstants.asciiEsc)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Escape character found.                                //
                    // Switch to PCL language processing.                     //
                    //                                                        //
                    // Note that, in theory, only a few escape sequences are  //
                    // expected:                                              //
                    //      <Esc>E          Printer Reset                     //
                    //      <Esc>%-12345X   Universal Exit Language           //
                    // but if we find an escape sequence, it's certainly not  //
                    // Prescribe.                                             //
                    //                                                        //
                    //--------------------------------------------------------//

                    langSwitch = true;

                    crntPDL = ToolCommonData.ePrintLang.PCL;
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Sequence does not start with an Escape character, so   //
                    // it should be a Prescribe command.                      //
                    //                                                        //
                    //--------------------------------------------------------//

                    badSeq = processCommand(ref bufRem,
                                             ref bufOffset,
                                             ref continuation,
                                             ref langSwitch,
                                             ref crntPDL);

                    if (badSeq)
                        invalidSeqFound = true;
                }
            }

            _linkData.MakeOvlAct = PrnParseConstants.eOvlAct.Remove;
            _linkData.MakeOvlSkipBegin = startPos;
            _linkData.MakeOvlSkipEnd = _fileOffset + bufOffset;

            return invalidSeqFound;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s C o m m a n d                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Process current Prescribe command in buffer.                       //
        //                                                                    //
        // Interrupt process if an <Esc> character is encountered.            //
        // Long commands are split into shorter slices.                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean processCommand(
            ref Int32 bufRem,
            ref Int32 bufOffset,
            ref Boolean continuation,
            ref Boolean langSwitch,
            ref ToolCommonData.ePrintLang crntPDL)
        {
            PrnParseConstants.eContType contType =
                PrnParseConstants.eContType.None;

            Byte crntByte,
                 cmdParaByte1 = 0x3f;

            Char crntChar,
                 normChar;

            Int32 len,
                  cmdLen,
                  cmdRem,
                  cmdStart,
                  offset,
                  lineStart;

            Int32 quoteStart = 0,
                  quoteEnd = 0;

            Boolean invalidSeqFound,
                    cmdParaByte1Found,
                    endLoop,
                    foundTerm;

        //  Boolean flagWithinQuote;
            Boolean flagWithinQuoteDouble;
            Boolean flagWithinQuoteSingle;
            Boolean cmdKnown = false;
            Boolean flagCmdExit  = false;
            Boolean flagCmdSetCRC  = false;

            String command,
                   commandName,
                   commandDesc = "";

            StringBuilder cmd = new StringBuilder();

            invalidSeqFound = false;
            foundTerm = false;
            langSwitch = false;

            lineStart = bufOffset;
            foundTerm = false;

            len = bufRem;
            offset = bufOffset;

            continuation = false;
            foundTerm = false;

            cmdRem   = bufRem;
            cmdStart = offset;
            cmdLen   = 0;

            //----------------------------------------------------------------//
            //                                                                //
            // Search for termination character.                              //
            // This should be a Semi-colon (0x3b) character.                  //
            // But we may encounter an Escape character (<Esc>, 0x1b)         //
            // signalling return to PCL?                                      //
            //                                                                //
            // This is to make sure that the termination character is in the  //
            // buffer before processing the command, so that we don't have to //
            // cater for doing a 'continuation' read part way through         //
            // processing the command.                                        //
            //                                                                //
            // Initiate continuation action if terminator is not found in     //
            // buffer, subject to a maximum command length (to prevent        //
            // recursive continuation actions).                               //
            //                                                                //
            //----------------------------------------------------------------//

        //  flagWithinQuote = false;
            flagWithinQuoteDouble = false;
            flagWithinQuoteSingle = false;

            while ((!foundTerm) && (cmdRem > 0) && (cmdLen < _maxCmdLen))
            {
                crntByte = _buf[offset];

                if (crntByte == PrnParseConstants.asciiEsc)
                {
                    foundTerm = true;
                }
                else
                {
                    if (flagWithinQuoteDouble)
                    {
                        if (crntByte == PrnParseConstants.asciiQuote)
                        {
                            flagWithinQuoteDouble = false;
                        //  flagWithinQuote = false;
                            quoteEnd = offset;
                        }
                    }
                    else if (flagWithinQuoteSingle)
                    {
                        if (crntByte == PrnParseConstants.asciiApostrophe)
                        {
                            flagWithinQuoteSingle = false;
                        //  flagWithinQuote = false;
                            quoteEnd = offset;
                        }
                    }
                    else if (crntByte == PrnParseConstants.asciiQuote)
                    {
                        flagWithinQuoteDouble = true;
                    //  flagWithinQuote = true;
                        quoteStart = offset;
                    }
                    else if (crntByte == PrnParseConstants.asciiApostrophe)
                    {
                        flagWithinQuoteSingle = true;
                    //  flagWithinQuote = true;
                        quoteStart = offset;
                    }
                    else if (crntByte == PrnParseConstants.asciiSemiColon)
                    {
                        foundTerm = true;
                    }

                    offset++;
                    cmdLen++;
                    cmdRem--;
                }
            }

            if ((!foundTerm) && (cmdLen < _maxCmdLen))
            {
                //------------------------------------------------------------//
                //                                                            //
                // Termination character not found before buffer exhausted,   //
                // or maximum command length exceeded.                        //
                // Initiate (backtracking) continuation action.               //
                //                                                            //
                //------------------------------------------------------------//

                continuation = true;

                contType = PrnParseConstants.eContType.Prescribe;

                _linkData.setBacktrack(contType, -bufRem);

            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // Process command.                                           //
                // At this point, we have in the buffer one of:               //
                //  - characters terminated by <semi-colon> (counted in       //
                //    length).                                                //
                //  - characters terminated by <Esc> (not counted in length). //
                //  - characters not terminated, but maxmimum length.         //
                //                                                            //
                //------------------------------------------------------------//

                cmdRem = cmdLen;
                offset = bufOffset;

                //------------------------------------------------------------//
                //                                                            //
                // Stage 1: look for & skip past whitespace.                  //
                //                                                            //
                //------------------------------------------------------------//

                endLoop = false;

                while ((!endLoop) && (cmdRem > 0))
                {
                    crntByte = _buf[offset];

                    if ((crntByte == PrnParseConstants.asciiSpace)
                                         ||
                        (crntByte == PrnParseConstants.asciiHT))
                    {
                        offset++;
                        cmdRem--;
                    }
                    else if ((crntByte == PrnParseConstants.asciiCR)
                                         ||
                        (crntByte == PrnParseConstants.asciiLF))
                    {
                        offset++;
                        cmdRem--;
                    }
                    else
                    {
                        endLoop = true;
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Stage 2: look for command name.                            //
                //                                                            //
                //------------------------------------------------------------//

                endLoop = false;

                while ((!endLoop) && (cmdRem > 0))
                {
                    crntByte = _buf[offset];

                    //--------------------------------------------------------//
                    //                                                        //
                    // Check for special characters first.                    //
                    //                                                        //
                    //--------------------------------------------------------//

                    if ((cmdRem == 1) &&
                             (crntByte == PrnParseConstants.asciiSemiColon))

                    {
                        // nextstage = Parameters or Terminator;
                        endLoop = true;
                    }
                    else if (((crntByte >= PrnParseConstants.asciiAlphaUCMin)
                                                &&
                              (crntByte <= PrnParseConstants.asciiAlphaUCMax))
                                                ||
                             ((crntByte >= PrnParseConstants.asciiAlphaLCMin)
                                                &&
                              (crntByte <= PrnParseConstants.asciiAlphaLCMax)))
                    {
                        crntChar = (Char)crntByte;
                        normChar = Char.ToUpper(crntChar);
                        cmd.Append(normChar);

                        offset++;
                        cmdRem--;
                    }
                    else
                    {
                        // nextstage = Parameters or Terminator;
                        endLoop = true;
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Check whether command name known.                          //
                //                                                            //
                //------------------------------------------------------------//

                commandName = cmd.ToString();

                cmdKnown = PrescribeCommands.checkCmd( cmd.ToString(),
                                                       ref commandDesc,
                                                       ref flagCmdExit,
                                                       ref flagCmdSetCRC,
                                                       _analysisLevel);

                //------------------------------------------------------------//
                //                                                            //
                // Stage 3: look for command remainder parameters, or the     //
                // terminator character.                                      //
                //                                                            //
                //------------------------------------------------------------//

                endLoop = false;
                cmdParaByte1Found = false;

                while ((!endLoop) && (cmdRem > 0))
                {
                    crntByte = _buf[offset];

                    if (!cmdParaByte1Found)
                    {
                        if ((crntByte != PrnParseConstants.asciiSpace)
                                             &&
                            (crntByte != PrnParseConstants.asciiHT))
                        {
                            cmdParaByte1 = crntByte;
                            cmdParaByte1Found = true;
                        }
                    }

                    offset++;
                    cmdRem--;
                }

                //------------------------------------------------------------//
                //                                                            //
                // Stage 4: Output details of command.                        //
                // Display sequence (in slices if necessary).                 //
                //                                                            //
                //------------------------------------------------------------//

                command = Encoding.ASCII.GetString(_buf, cmdStart, cmdLen);

                const Int32 indent = 2;

                lineStart = 0;
                len = cmdLen;       // or length of string? //

                Int32 sliceLen,
                      sliceLenMax,
                      sliceStart,
                      sliceOffset,
                      ccAdjust;

                Boolean firstSlice;

                String seq = "";

                Byte[] seqBuf = new Byte[PrnParseConstants.cRptA_colMax_Seq];

                firstSlice = true;
                sliceOffset = 0;

                if (firstSlice)
                    sliceLenMax = PrnParseConstants.cRptA_colMax_Seq;
                else
                    sliceLenMax = PrnParseConstants.cRptA_colMax_Seq - indent;

                sliceStart = bufOffset + sliceOffset;

                while (len > sliceLenMax)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Sequence is too large to fit on one output line.       //
                    //                                                        //
                    //--------------------------------------------------------//

                    sliceLen = sliceLenMax;
                    ccAdjust = 0;

                    if (firstSlice)
                    {
                        seq = command.Substring(sliceOffset, sliceLen);

                        PrnParseCommon.addDataRow (
                            PrnParseRowTypes.eType.PrescribeCommand,
                            _table,
                            PrnParseConstants.eOvlShow.Remove,
                            _indxOffsetFormat,
                            _fileOffset + bufOffset + sliceOffset,
                            _analysisLevel,
                            "Prescribe command",
                            seq.ToString(),
                            "");
                    }
                    else
                    {
                        seq = "  " + // indent number of spaces
                              command.Substring(sliceOffset, sliceLen);

                        PrnParseCommon.addDataRow (
                            PrnParseRowTypes.eType.PrescribeCommand,
                            _table,
                            PrnParseConstants.eOvlShow.Remove,
                            _indxOffsetFormat,
                            _fileOffset + bufOffset + sliceOffset,
                            _analysisLevel,
                            "",
                            seq.ToString(),
                            "");
                    }

                    len = len - sliceLen - ccAdjust;
                    sliceOffset = sliceOffset + sliceLen + ccAdjust;
                    sliceStart += (sliceLen + ccAdjust);
                    sliceLenMax = PrnParseConstants.cRptA_colMax_Seq - indent;

                    firstSlice = false;
                }

                //------------------------------------------------------------//
                //                                                            //
                // Display last (or only) slice of sequence.                  //
                //                                                            //
                //------------------------------------------------------------//

                sliceLen = len;
                ccAdjust = 0;

                if (len > 0)
                {
                    if (firstSlice)
                    {
                        seq = command.Substring(sliceOffset, sliceLen);

                        PrnParseCommon.addDataRow (
                            PrnParseRowTypes.eType.PrescribeCommand,
                            _table,
                            PrnParseConstants.eOvlShow.Remove,
                            _indxOffsetFormat,
                            _fileOffset + bufOffset + sliceOffset,
                            _analysisLevel,
                            "Prescribe Command",
                            seq,
                            commandDesc);
                    }
                    else
                    {
                        seq = "  " + // indent number of spaces
                              command.Substring(sliceOffset, sliceLen);

                        PrnParseCommon.addDataRow (
                            PrnParseRowTypes.eType.PrescribeCommand,
                            _table,
                            PrnParseConstants.eOvlShow.Remove,
                            _indxOffsetFormat,
                            _fileOffset + bufOffset + sliceOffset,
                            _analysisLevel,
                            "",
                            seq,
                            commandDesc);
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Stage 5: Do any special processing.                        //
                //                                                            //
                //------------------------------------------------------------//

                bufOffset = offset;
                bufRem -= cmdLen;

                if (flagCmdExit)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Exit command found and processed.                      //
                    //                                                        //
                    //--------------------------------------------------------//

                    langSwitch = true;

                    crntPDL = _linkData.PrescribeCallerPDL;
                    _linkData.PrescribeIntroRead = false;
                }
                else if (flagCmdSetCRC)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Set Command Recognition Character command found and    //
                    // processed.                                             //
                    //                                                        //
                    //--------------------------------------------------------//

                    _linkData.PrescribeSCRC = cmdParaByte1;

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgComment,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Comment",
                        "",
                        "Set Prescribe CRC = " + (Char) cmdParaByte1);
                }
            }

            return invalidSeqFound;
        }
    }
}