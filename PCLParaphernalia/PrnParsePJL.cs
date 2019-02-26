using System;
using System.Data;
using System.Text;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines functions to parse PJL commands.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    class PrnParsePJL
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private const Int32 _lenPJLIntro   = 4;       // @PJL //
        private const Int32 _maxPJLCmdLen  = 256;
        private const Int32 _maxPJLLineLen = 50;

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

        private Boolean _showPML;

        private PrnParseConstants.eOptCharSetSubActs _indxCharSetSubAct;
        private PrnParseConstants.eOptCharSets _indxCharSetName;
        private Int32 _valCharSetSubCode;

        private ASCIIEncoding _ascii = new ASCIIEncoding ();

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P r n P a r s e P J L                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PrnParsePJL()
        {
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // f i n d P J L T e r m i n a t o r                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Search for PJL terminator character.                               //
        // This should be a LineFeed (<LF>, 0x0a) character, but may be an    //
        // Escape character (<Esc>, 0x1b) signalling return to PCL.           //
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

        private Boolean findPJLTerminator (
            Int32       bufRem,
            Int32       bufOffset,
            ref Int32   commandLen,
            ref Boolean continuation)
        {
            PrnParseConstants.eContType contType =
                PrnParseConstants.eContType.None;

            Byte crntByte;

            Int32 cmdLen,
                  rem,
                  offset;

            Boolean foundTerm,
                    foundLF;

            continuation = false;
            foundTerm    = false;
            foundLF      = false;

            rem    = bufRem    - _lenPJLIntro;
            offset = bufOffset + _lenPJLIntro;
            cmdLen = _lenPJLIntro;

            //----------------------------------------------------------------//
            //                                                                //
            // Search for termination character.                              //
            //                                                                //
            //----------------------------------------------------------------//

            while ((!foundTerm) && (rem > 0) && (cmdLen < _maxPJLCmdLen))
            {
                crntByte = _buf[offset];

                if (crntByte == PrnParseConstants.asciiLF)
                {
                    foundLF   = true;
                    foundTerm = true;
                    offset++;
                    cmdLen++;
                    rem--;
                }
                else if (crntByte == PrnParseConstants.asciiEsc)
                {
                    foundTerm = true;
                }
                else
                {
                    offset++;
                    cmdLen++;
                    rem--;
                }
            }

            if ((!foundTerm) && (cmdLen != _maxPJLCmdLen))
            {
                //------------------------------------------------------------//
                //                                                            //
                // Termination character not found before buffer exhausted,   //
                // or maximum command length exceeded.                        //
                // Initiate (backtracking) continuation action.               //
                //                                                            //
                //------------------------------------------------------------//

                continuation = true;

                contType = PrnParseConstants.eContType.PJL;

                _linkData.setBacktrack (contType, -bufRem);

                commandLen = 0;
            }
            else
            {
                commandLen = cmdLen;
            }

            return foundLF;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a r s e B u f f e r                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Parse provided buffer, assuming that the current print language is //
        // PJL.                                                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean parseBuffer(
            Byte []                             buf,
            ref Int32                           fileOffset,
            ref Int32                           bufRem,
            ref Int32                           bufOffset,
            ref ToolCommonData.ePrintLang    crntPDL,
            ref Boolean                         endReached,
            PrnParseLinkData                    linkData,
            PrnParseOptions                     options,
            DataTable                           table)
        {
            Boolean seqInvalid;

            //----------------------------------------------------------------//
            //                                                                //
            // Initialise.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            _buf        = buf;
            _linkData   = linkData;
            _options    = options;
            _table      = table;
            _fileOffset = fileOffset;

            _analysisLevel = _linkData.AnalysisLevel;

            seqInvalid = false;

            //----------------------------------------------------------------//

            _indxOffsetFormat = _options.IndxGenOffsetFormat;

            _options.getOptCharSet (ref _indxCharSetName,
                                    ref _indxCharSetSubAct,
                                    ref _valCharSetSubCode);

            _endOffset = _options.ValCurFOffsetEnd;

            _showPML = _options.FlagPMLWithinPJL;   
            
            //----------------------------------------------------------------//

            if (linkData.isContinuation())
                seqInvalid = parseContinuation (ref bufRem,
                                                ref bufOffset,
                                                ref crntPDL,
                                                ref endReached); 
            else
                seqInvalid = parseSequences (ref bufRem,
                                             ref bufOffset,
                                             ref crntPDL,
                                             ref endReached);  

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
            ref Int32                           bufRem,
            ref Int32                           bufOffset,
            ref ToolCommonData.ePrintLang    crntPDL,
            ref Boolean                         endReached)
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
            
            _linkData.getContData (ref contType,
                                   ref prefixLen,
                                   ref contDataLen,
                                   ref downloadRem,
                                   ref backTrack,
                                   ref prefixA,
                                   ref prefixB);

            if ((contType == PrnParseConstants.eContType.PJL)
                             ||
                (contType == PrnParseConstants.eContType.Special)
                             ||
                (contType == PrnParseConstants.eContType.Unknown)
                             ||
                (contType == PrnParseConstants.eContType.Reset))
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended with a partial match of a PJL       //
                // sequence, or with insufficient characters to identify      //
                // the type of sequence.                                      //
                // The continuation action has already reset the buffer, so   //
                // now unset the markers.                                     //
                //                                                            //
                //------------------------------------------------------------//

                _linkData.resetContData ();
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

            PrnParseConstants.eContType contType =
                PrnParseConstants.eContType.None;

            Boolean continuation = false;
            Boolean langSwitch = false;
            Boolean badSeq = false;
            Boolean invalidSeqFound = false;
            Boolean dummyBool = false;

            continuation = false;
            startPos = _fileOffset + bufOffset;

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
                    // PJL.                                                   //
                    //                                                        //
                    //--------------------------------------------------------//

                    langSwitch = true;

                    crntPDL = ToolCommonData.ePrintLang.PCL;
                }
                else if (_buf[bufOffset] != PrnParseConstants.asciiAtSign)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Next character is NOT an @ symbol, so it can't be a    //
                    // PJL command.                                           //
                    // Switch to PCL language processing.                     //
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
                    // it should be a PJL command.                            //
                    // PJL commands should start with @PJL and end with a     //
                    // LineFeed (0x0a) character.                             //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (bufRem < 5)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Insufficient characters remain in buffer to        //
                        // identify the sequence as PJL, so initiate a        //
                        // continuation action.                               //
                        //                                                    //
                        //----------------------------------------------------//

                        continuation = true;

                        contType = PrnParseConstants.eContType.PJL;

                        _linkData.setBacktrack (contType, - bufRem);
                    }
                    else if (_ascii.GetString (_buf, bufOffset, _lenPJLIntro)
                             != "@PJL")
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Not a PJL sequence.                                //
                        // Display the unexpected sequence up to the next     //
                        // Escape character.                                  //
                        //                                                    //
                        //----------------------------------------------------//

                        invalidSeqFound = true;

                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.MsgWarning,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "*** Warning ***",
                            "",
                            "Unexpected sequence found");

                        PrnParseData.processLines (
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            _linkData,
                            ToolCommonData.ePrintLang.PJL,
                            _buf,
                            _fileOffset,
                            bufRem,
                            ref bufRem,
                            ref bufOffset,
                            ref dummyBool,
                            true,
                            true,
                            false,
                            PrnParseConstants.asciiEsc,
                            "Data",
                            0,
                            _indxCharSetSubAct,
                            (Byte) _valCharSetSubCode,
                            _indxCharSetName,
                            _indxOffsetFormat,
                            _analysisLevel);
                    }
                    else
                    {
                        //-------------------------------------------------------------//
                        //                                                             //
                        // PJL sequence detected.                                      //
                        //                                                             //
                        //-------------------------------------------------------------//

                        badSeq = processPJLCommand (ref bufRem,
                                                    ref bufOffset,
                                                    ref continuation,
                                                    ref langSwitch,
                                                    ref crntPDL);

                        if (badSeq)
                            invalidSeqFound = true;
                    }
                }
            }

            _linkData.MakeOvlAct    = PrnParseConstants.eOvlAct.Remove;
            _linkData.MakeOvlSkipBegin = startPos;
            _linkData.MakeOvlSkipEnd   = _fileOffset + bufOffset;

            return invalidSeqFound;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s P J L C o m m a n d                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Process current PJL command in buffer.                             //
        // Command format is one of:                                          //
        //                                                                    //
        //    @PJL [<CR>]<LF>                                                 //
        //                                                                    //
        //    Can be used to separate real commands, and add clarity to long  //
        //    sets of commands.                                               //
        //                                                                    //
        //    @PJL command [<words>] [<CR>}<LF>                               //
        //                                                                    //
        //    For the COMMENT and ECHO commands only.                         //
        //    <words>               Any string of printable characters        //
        //                          (range 0x21-0xff) or whitespace           //
        //                          (space (0x20) or horizontal tab (0x09)),  //
        //                          starting with a printable character.      //
        //                          The string may be enclosed in quote       //
        //                          characters (0x22), in which case it       //
        //                          cannot include a quote character.         //
        //                                                                    //
        //    @PJL command [modifier : value] [option [= value]] [<CR>}<LF>   //
        //                                                                    //
        //    command               One of the defined set of command names.  //
        //    modifier:value        is present for some commands to indicate  //
        //                          particular personality, or port, etc.     //
        //    option                Present for most commands.                //
        //    value                 Associated with 'option' name or (if      //
        //                          that is not present, the command).        //
        //                          There may be more than one option=value   //
        //                          pair.                                     // 
        //                                                                    //
        // Whitespace (space or horizontal tab) characters MUST be present    //
        // between the @PJL introducer and the 'command', and between the     //
        // command and modifier names, and between the modifier value and the //
        // option name.                                                       //
        //                                                                    //
        // Interrupt process if an <Esc> character is encountered.            //
        // Long lines are split into shorter slices.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean processPJLCommand(
            ref Int32 bufRem,
            ref Int32 bufOffset,
            ref Boolean continuation,
            ref Boolean langSwitch,
            ref ToolCommonData.ePrintLang crntPDL)
        {
            PrnParseConstants.eContType contType =
                PrnParseConstants.eContType.None;

            Byte crntByte;

            Char crntChar,
                 normChar;

            Int32 len,
                  cmdLen,
                  cmdRem,
                  langLen,
                  offset,
                  lineStart,
                  seqLen = 0;

            Int32 quoteStart = 0,
                  quoteEnd = 0;

            Boolean invalidSeqFound,
                    endLoop,
                    foundTerm,
                    firstLine;

            Boolean foundStartQuote;
            Boolean seqKnown = false;
            Boolean noWhitespace = false;

            String lang,
                   commandName,
                   commandParams,
                   line,
                   showChar,
                   desc = "";

            StringBuilder seq = new StringBuilder ();

            StringBuilder cmd = new StringBuilder ();

            StringBuilder cmdPart1 = new StringBuilder ();
            StringBuilder cmdPart2 = new StringBuilder ();

            cmdPart1.Append ("@PJL");

            invalidSeqFound = false;
            foundStartQuote = false;
            foundTerm = false;
            firstLine = true;
            langSwitch = false;

            lineStart = bufOffset;
            foundTerm = false;

            len = bufRem;
            offset = bufOffset;

            continuation = false;
            foundTerm = false;

            cmdRem = bufRem - _lenPJLIntro;
            offset = bufOffset + _lenPJLIntro;
            cmdLen = _lenPJLIntro;

            //----------------------------------------------------------------//
            //                                                                //
            // Search for termination character.                              //
            // This should be a LineFeed (<LF>, 0x0a) character, but may be   //
            // an Escape character (<Esc>, 0x1b) signalling return to PCL.    //
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

            while ((!foundTerm) && (cmdRem > 0) && (cmdLen < _maxPJLCmdLen))
            {
                crntByte = _buf[offset];

                if (crntByte == PrnParseConstants.asciiLF)
                {
                    foundTerm = true;
                    offset++;
                    cmdLen++;
                    cmdRem--;
                }
                else if (crntByte == PrnParseConstants.asciiEsc)
                {
                    foundTerm = true;
                }
                else
                {
                    offset++;
                    cmdLen++;
                    cmdRem--;
                }
            }

            if ((!foundTerm) && (cmdLen != _maxPJLCmdLen))              // ***** Should this be < rather than != ? ***** //
            {
                //------------------------------------------------------------//
                //                                                            //
                // Termination character not found before buffer exhausted,   //
                // or maximum command length exceeded.                        //
                // Initiate (backtracking) continuation action.               //
                //                                                            //
                //------------------------------------------------------------//

                continuation = true;

                contType = PrnParseConstants.eContType.PJL;

                _linkData.setBacktrack (contType, -bufRem);

            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // Process command.                                           //
                // At this point, we have in the buffer one of:               //
                //  - characters terminated by <LF> (counted in length).      //
                //  - characters terminated by <Esc> (not counted in length). //
                //  - characters not terminated, but maxmimum length.         //
                //                                                            //
                //------------------------------------------------------------//

                cmdRem = cmdLen    - _lenPJLIntro;
                offset = bufOffset + _lenPJLIntro;

                //------------------------------------------------------------//
                //                                                            //
                // Stage 1: look for & skip past whitespace.                  //
                //                                                            //
                //------------------------------------------------------------//

                endLoop = false;

                while ((! endLoop) && (cmdRem > 0))
                {
                    crntByte = _buf[offset];

                    if ((crntByte == PrnParseConstants.asciiSpace)
                                         ||
                        (crntByte == PrnParseConstants.asciiHT))
                    {
                        showChar = PrnParseData.processByte (
                                        crntByte,
                                        _indxCharSetSubAct,
                                        (Byte) _valCharSetSubCode,
                                        _indxCharSetName);

                        cmdPart1.Append (showChar);

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

                    if ((crntByte == PrnParseConstants.asciiSpace)
                                         ||
                        (crntByte == PrnParseConstants.asciiHT))
                    {
                        // nextstage = Whitespace;
                        endLoop = true;
                    }
                    else if ((cmdRem == 2) &&
                             (crntByte == PrnParseConstants.asciiCR))

                    {
                        // nextstage = Terminator;
                        endLoop = true;
                    }
                    else if ((cmdRem == 1) &&
                             (crntByte == PrnParseConstants.asciiLF))

                    {
                        // nextstage = Terminator;
                        endLoop = true;
                    }
                    else if (crntByte == PrnParseConstants.asciiEquals)

                    {
                        // nextstage = Part2;
                        endLoop = true;
                        noWhitespace = true;
                    }
                    else
                    {
                        crntChar = (Char) crntByte;
                        normChar = Char.ToUpper (crntChar);
                        cmd.Append (normChar);

                        showChar = PrnParseData.processByte (
                                        crntByte,
                                        _indxCharSetSubAct,
                                        (Byte) _valCharSetSubCode,
                                        _indxCharSetName);

                        cmdPart1.Append (showChar);

                        offset++;
                        cmdRem--;
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Check whether command name known.                          //
                //                                                            //
                //------------------------------------------------------------//

                commandName = cmd.ToString ();

                if (commandName == "")
                    seqKnown = PJLCommands.checkCmd (PJLCommands.nullCmdKey,
                                                     ref desc,
                                                     _analysisLevel);
                else
                    seqKnown = PJLCommands.checkCmd (cmd.ToString (),
                                                     ref desc,
                                                     _analysisLevel);

                //------------------------------------------------------------//
                //                                                            //
                // Stage 3: look for command remainder.                       //
                //          TODO : split this up into component parts?        //
                //                                                            //
                //------------------------------------------------------------//

                endLoop = false;

                while ((!endLoop) && (cmdRem > 0))
                {
                    crntByte = _buf[offset];

                    if (crntByte == PrnParseConstants.asciiQuote)
                    {
                        if (!foundStartQuote)
                        {
                            foundStartQuote = true;
                            quoteStart = offset;
                        }
                        else
                        {
                            quoteEnd = offset;
                        }
                    }

                    crntChar = (Char) crntByte;
                    normChar = Char.ToUpper (crntChar);

                    showChar = PrnParseData.processByte (
                                    crntByte,
                                    _indxCharSetSubAct,
                                    (Byte) _valCharSetSubCode,
                                    _indxCharSetName);

                    cmdPart2.Append (showChar);

                    if ((crntByte == PrnParseConstants.asciiSpace)
                                         ||
                        (crntByte == PrnParseConstants.asciiHT))
                    {
                    }
                    else if ((crntByte == PrnParseConstants.asciiDEL)
                                         ||
                             (crntByte < PrnParseConstants.asciiSpace))
                    {
                        seq.Append ((Char) PrnParseConstants.asciiPeriod);
                    }
                    else
                    {
                        seq.Append (normChar);
                    }

                    offset++;
                    cmdRem--;
                }

                //--------------------------------------------------------//
                //                                                        //
                // Stage 4: Output details of sequence.                   //
                //                                                        //
                //--------------------------------------------------------//

                commandParams = cmdPart2.ToString ();

                len = commandParams.Length;
                lineStart = 0;

                while ((firstLine) || (len > 0))
                {
                    if (len > _maxPJLLineLen)
                    {
                        line = commandParams.Substring(lineStart,
                                                       _maxPJLLineLen);
                        len -= _maxPJLLineLen;
                        lineStart += _maxPJLLineLen;
                    }
                    else
                    {
                        line = commandParams.Substring(lineStart,
                                                       len);
                        len  = 0;
                    }

                    if (firstLine)
                    {
                        firstLine = false;

                        if (!seqKnown)
                        {
                            PrnParseCommon.addTextRow (
                                PrnParseRowTypes.eType.MsgWarning,
                                _table,
                                PrnParseConstants.eOvlShow.None,
                                "",
                                "*** Warning ***",
                                "",
                                "Following PJL commmand name not recognised:");
                        }

                        if (noWhitespace)
                        {
                            PrnParseCommon.addTextRow (
                                PrnParseRowTypes.eType.MsgWarning,
                                _table,
                                PrnParseConstants.eOvlShow.None,
                                "",
                                "*** Warning ***",
                                "",
                                "Following PJL command name not terminated" +
                                " by space or tab character:");
                        }

                        PrnParseCommon.addDataRow (
                            PrnParseRowTypes.eType.PJLCommand,
                            _table,
                            PrnParseConstants.eOvlShow.Remove,
                            _indxOffsetFormat,
                            _fileOffset + bufOffset,
                            _analysisLevel,
                            "PJL Command",
                            cmdPart1.ToString (),
                            line);
                    }
                    else
                    {
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PJLCommand,
                            _table,
                            PrnParseConstants.eOvlShow.Remove,
                            "",
                            "",
                            "",
                            line);
                    }
                }

                //--------------------------------------------------------//
                //                                                        //
                // Stage 5: Do any special processing.                    //
                //                                                        //
                //--------------------------------------------------------//

                commandParams = seq.ToString ();

                if ((commandName.Length == 5)
                            &&
                    (commandName.Substring (0, 5) == "ENTER")
                            && 
                    (commandParams.Length > 9)
                            &&
                    (commandParams.Substring (0, 9) == "LANGUAGE="))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Enter Language command encountered.                    //
                    //                                                        //
                    //--------------------------------------------------------//

                    langSwitch = true;

                    seqLen = seq.Length;

                    lang = commandParams.Substring (9, seqLen - 9);

                    langLen = lang.Length;

                    if ((langLen >= 5) &&
                        (lang.Substring (0, 5) == "PCLXL"))
                        crntPDL = ToolCommonData.ePrintLang.PCLXL;
                    else if ((langLen >= 7) &&
                             (lang.Substring (0, 7) == "PCL3GUI"))
                        crntPDL = ToolCommonData.ePrintLang.PCL3GUI;
                    else if ((langLen >= 3) &&
                             (lang.Substring (0, 3) == "PCL"))
                        crntPDL = ToolCommonData.ePrintLang.PCL;
                    else if ((langLen >= 10) &&
                             (lang.Substring (0, 10) == "POSTSCRIPT"))
                        crntPDL = ToolCommonData.ePrintLang.PostScript;
                    else if ((langLen >= 4) &&
                             (lang.Substring (0, 4) == "HPGL"))
                        crntPDL = ToolCommonData.ePrintLang.HPGL2;
                    else if ((langLen >= 5) &&
                             (lang.Substring (0, 5) == "XL2HB"))
                        crntPDL = ToolCommonData.ePrintLang.XL2HB;
                    else
                        crntPDL = ToolCommonData.ePrintLang.Unknown;
                }
                else if ((_showPML) &&
                         (((commandName.Length == 5)
                            &&
                           (commandName.Substring (0, 5) == "DMCMD"))
                            ||  
                          ((commandName.Length == 6)
                            &&
                           (commandName.Substring (0, 6) == "DMINFO"))))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // PML Device Management                                  //
                    //                                                        //
                    //--------------------------------------------------------//

                    if ((commandParams.Length > 9)
                            &&
                        (commandParams.Substring (0, 9) == "ASCIIHEX="))
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // PML sequence; encoded as ASCII HEX.                //
                        // Expected to be enclosed in " quotes and followed   //
                        // by <CR><LF> or <LF> PJL terminator characters.     //
                        // Note that the normalised sequence will have "."    //
                        // characters in place of the <CR> and <LF> control   //
                        // codes.                                             //
                        //                                                    //
                        //----------------------------------------------------//

                        PrnParsePML parsePML = new PrnParsePML ();

                        seqLen = quoteEnd - quoteStart - 1;

                        if (seqLen > 0)
                            invalidSeqFound =
                                parsePML.processPMLASCIIHex (_buf,
                                                            _fileOffset,
                                                            seqLen,
                                                            quoteStart + 1,
                                                            _linkData,
                                                            _options,
                                                            _table);
                    }
                }

                bufOffset = offset;
                bufRem   -= cmdLen;
            }

            return invalidSeqFound;
        }
    }
}