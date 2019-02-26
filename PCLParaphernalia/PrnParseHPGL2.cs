using System;
using System.Data;
using System.Text;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines functions to parse HP-GL/2 commands.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    class PrnParseHPGL2
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private const Int32 HPGL2MnemonicLen = 2;

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private PrnParseLinkData _linkData;
        
        private PrnParseOptions _options;

        private DataTable _table;

        private Byte[] _buf;

        private Byte _finalByte;

        private Int32 _fileOffset;
        private Int32 _endOffset;

        private PrnParseConstants.eOptOffsetFormats _indxOffsetFormat;

        private PrnParseConstants.eOptCharSetSubActs _indxCharSetSubAct;
        private PrnParseConstants.eOptCharSets _indxCharSetName;
        private Int32 _valCharSetSubCode;


  //      private Int32 _textParsingMethod;
        private Int32 _analysisLevel = 0;    // TEMP?? //
        private Int32 _macroLevel    = 0;    // TEMP?? //

        private Boolean _flagMiscBinData = false;

        private ASCIIEncoding _ascii = new ASCIIEncoding ();

        private Byte _labelTerm;

        private Boolean _labelTrans;
        
        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P r n P a r s e H P G L 2                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PrnParseHPGL2()
        {
            resetHPGL2 ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d i s p l a y H P G L 2 C o m m a n d                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Display details of the current HP-GL/2 command.                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void displayHPGL2Command (ref Int32 bufOffset,
                                         Int32 seqLen,
                                         Int32 prefixLen,
                                         Boolean binarySeq,
                                         Boolean continued,
                                         Boolean continuation,
                                         String desc)
        {
            Int32 len;

            len = seqLen;

            //----------------------------------------------------------------//
            //                                                                //
            // Store last character of HP-GL/2 command.                       //
            // This will be used later to check whether HP-GL/2 sequence is   //
            // terminated by ";" before returning to PCL.                     //
            //                                                                //
            //----------------------------------------------------------------//

            _finalByte = _buf [bufOffset + seqLen - 1];

            if (binarySeq)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Encoded (binary) sequence.                                 //
                //                                                            //
                //------------------------------------------------------------//

                String typeText;

                if (prefixLen != 0)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // This is the first part of the (binary) sequence, with  //
                    // the opening mnemonic.                                  //
                    // Display the mnemonic and the description of the        //
                    // command.                                               //
                    //                                                        //
                    //--------------------------------------------------------//

                    PrnParseCommon.addDataRow (
                        PrnParseRowTypes.eType.HPGL2Command,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _indxOffsetFormat,
                        _fileOffset + bufOffset,
                        _analysisLevel,
                        "HP-GL/2 Command",
                        _ascii.GetString (_buf,
                                          bufOffset,
                                          prefixLen),
                        desc);

                    len = len - prefixLen;
                }

                //------------------------------------------------------------//
                //                                                            //
                // Display details of the binary sequence.                    //
                // Indicate whether or not this is the last part of the       //
                // binary sequence (i.e. whether or not the last character of //
                // the supplied sequence is the terminating ';' character).   //
                //                                                            //
                //------------------------------------------------------------//

                if (_finalByte == PrnParseConstants.asciiSemiColon)
                    typeText = "HP-GL/2 Binary + ;";
                else
                    typeText = "HP-GL/2 Binary";

                PrnParseData.processBinary (
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _buf,
                    _fileOffset,
                    bufOffset + prefixLen,
                    len,
                    typeText,
                    _flagMiscBinData,
                    false,
                    true,
                    _indxOffsetFormat,
                    _analysisLevel);
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // Not encoded (binary) sequence.                             //
                // Display sequence (in slices if necessary).                 //
                //                                                            //
                //------------------------------------------------------------//

                Int32 sliceLen,
                      sliceLenMax,
                      sliceStart,
                      sliceOffset,
                      seqBufOffset,
                      ccAdjust;

                Boolean firstSlice,
                        firstSliceAfterCC,
                        nonGraphics,
                        knownCC;
                ;

                String seq = "",
                       ccDesc = "";

                Byte [] seqBuf = new Byte [PrnParseConstants.cRptA_colMax_Seq];
                Byte seqByte,
                     ccByte;

                firstSlice = true;
                firstSliceAfterCC = false;
                nonGraphics = false;
                sliceOffset = 0;
                seqBufOffset = 0;

                if (continuation)
                {
                    sliceLenMax = PrnParseConstants.cRptA_colMax_Seq - prefixLen;

                    for (Int32 i = 0; i < prefixLen; i++)
                    {
                        seqBuf [seqBufOffset++] = 0x20;
                    }
                }
                else
                {
                    sliceLenMax = PrnParseConstants.cRptA_colMax_Seq;
                }

                sliceStart = bufOffset + sliceOffset;

                while (len > sliceLenMax)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Sequence is too large to fit on one output line.       //
                    //                                                        //
                    //--------------------------------------------------------//

                    sliceLen = sliceLenMax;
                    ccByte = 0x20;
                    ccAdjust = 0;

                    for (Int32 i = 0; i < sliceLenMax; i++)
                    {
                        seqByte = _buf [sliceStart + i];

                        if ((seqByte < 0x20) || (seqByte > 0x7e))
                        {
                            /*
                            seqBuf [seqBufOffset + i] = 0x3f;         // ? //
                            nonGraphics = true;
                            */
                            nonGraphics = true;
                            ccByte = seqByte;
                            ccAdjust = 1;
                            sliceLen = i;
                            i = sliceLenMax;    // terminate loop
                        }
                        else
                        {
                            seqBuf [seqBufOffset + i] = seqByte;
                        }
                    }

                    seq = _ascii.GetString (seqBuf, 0, sliceLen + seqBufOffset);

                    if (firstSlice)
                    {
                        PrnParseCommon.addDataRow (
                            PrnParseRowTypes.eType.HPGL2Command,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            _indxOffsetFormat,
                            _fileOffset + bufOffset + sliceOffset,
                            _analysisLevel,
                            "HP-GL/2 Command",
                            seq.ToString (),
                            "");
                    }
                    else if (firstSliceAfterCC)
                    {
                        PrnParseCommon.addDataRow (
                            PrnParseRowTypes.eType.HPGL2Command,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            _indxOffsetFormat,
                            _fileOffset + bufOffset + sliceOffset,
                            _analysisLevel,
                            "HP-GL/2 Command cont.",
                            seq.ToString (),
                            "");
                    }
                    else
                    {
                        PrnParseCommon.addDataRow (
                            PrnParseRowTypes.eType.HPGL2Command,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            _indxOffsetFormat,
                            _fileOffset + bufOffset + sliceOffset,
                            _analysisLevel,
                            "",
                            seq.ToString (),
                            "");
                    }

                    len = len - sliceLen - ccAdjust;
                    sliceOffset = sliceOffset + sliceLen + ccAdjust;
                    sliceStart += (sliceLen + ccAdjust);
                    sliceLenMax = PrnParseConstants.cRptA_colMax_Seq - prefixLen;
                    seqBufOffset = 0;

                    for (Int32 i = 0; i < prefixLen; i++)
                    {
                        seqBuf [seqBufOffset++] = 0x20;
                    }

                    firstSlice = false;
                    firstSliceAfterCC = false;

                    if (nonGraphics)
                    {
                        knownCC = HPGL2ControlCodes.checkTag (ccByte, ref ccDesc);

                        if (knownCC)
                        {
                            HPGL2ControlCodes.incrementStatsCount (
                                ccByte,
                                _analysisLevel);
                        }

                        PrnParseCommon.addDataRow (
                            PrnParseRowTypes.eType.HPGL2ControlCode,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            _indxOffsetFormat,
                            _fileOffset + bufOffset + (sliceOffset - ccAdjust),
                            _analysisLevel,
                            "HP-GL/2 Control Code",
                            "0x" + ccByte.ToString ("x2"),
                            ccDesc);

                        firstSliceAfterCC = true;
                        nonGraphics = false;
                        ccAdjust = 0;
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Display last (or only) slice of sequence.                  //
                //                                                            //
                //------------------------------------------------------------//

                sliceLen = len;
                ccByte = 0x20;
                ccAdjust = 0;

                while (len > 0)
                {
                    for (Int32 i = 0; i < sliceLen; i++)
                    {
                        seqByte = _buf [sliceStart + i];

                        if ((seqByte < 0x20) || (seqByte > 0x7e))
                        {
                            /*
                            seqBuf [seqBufOffset + i] = 0x3f;         // ? //
                            nonGraphics = true;
                            */
                            nonGraphics = true;
                            ccByte = seqByte;
                            ccAdjust = 1;
                            sliceLen = i;
                            i = sliceLen;    // terminate loop
                        }
                        else
                        {
                            seqBuf [seqBufOffset + i] = seqByte;
                        }
                    }

                    seq = _ascii.GetString (seqBuf, 0, sliceLen + seqBufOffset);

                    len = len - sliceLen - ccAdjust;

                    if ((firstSlice) && (!continuation))
                    {
                        PrnParseCommon.addDataRow (
                            PrnParseRowTypes.eType.HPGL2Command,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            _indxOffsetFormat,
                            _fileOffset + bufOffset + sliceOffset,
                            _analysisLevel,
                            "HP-GL/2 Command",
                            seq,
                            (len == 0 ? desc : ""));
                    }
                    else if (firstSliceAfterCC)
                    {
                        PrnParseCommon.addDataRow (
                            PrnParseRowTypes.eType.HPGL2Command,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            _indxOffsetFormat,
                            _fileOffset + bufOffset + sliceOffset,
                            _analysisLevel,
                            "HP-GL/2 Command cont.",
                            seq,
                            (len == 0 ? desc : ""));
                    }
                    else
                    {
                        PrnParseCommon.addDataRow (
                            PrnParseRowTypes.eType.HPGL2Command,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            _indxOffsetFormat,
                            _fileOffset + bufOffset + sliceOffset,
                            _analysisLevel,
                            "",
                            seq,
                            (len == 0 ? desc : ""));
                    }

                    sliceStart += (sliceLen + ccAdjust);
                    sliceOffset += (sliceLen + ccAdjust);
                    sliceLen = len;
                    seqBufOffset = 0;

                    for (Int32 i = 0; i < prefixLen; i++)
                    {
                        seqBuf [seqBufOffset++] = 0x20;
                    }

                    firstSlice = false;
                    firstSliceAfterCC = false;

                    if (nonGraphics)
                    {
                        knownCC = HPGL2ControlCodes.checkTag (ccByte, ref ccDesc);

                        if (knownCC)
                        {
                            HPGL2ControlCodes.incrementStatsCount (
                                ccByte,
                                _analysisLevel);
                        }

                        PrnParseCommon.addDataRow (
                            PrnParseRowTypes.eType.HPGL2ControlCode,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            _indxOffsetFormat,
                            _fileOffset + bufOffset + (sliceOffset - ccAdjust),
                            _analysisLevel,
                            "HP-GL/2 Control Code",
                            "0x" + ccByte.ToString ("x2"),
                            ccDesc);

                        firstSliceAfterCC = true;
                        nonGraphics = false;
                        ccAdjust = 0;
                    }
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a r s e B u f f e r                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Parse provided buffer, assuming that the current print language is //
        // HP-GL/2.                                                           //
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

      //      langSwitch = false;
            seqInvalid = false;

      //      _textParsingMethod = 0;                             // TEMP

            //----------------------------------------------------------------//
                    
            _indxOffsetFormat = _options.IndxGenOffsetFormat;

            _options.getOptCharSet (ref _indxCharSetName,
                                    ref _indxCharSetSubAct,
                                    ref _valCharSetSubCode);

            _options.getOptHPGL2 (ref _flagMiscBinData);

            _endOffset = _options.ValCurFOffsetEnd;

            //----------------------------------------------------------------//

            if (linkData.isContinuation())
                seqInvalid = parseContinuation (ref bufRem,
                                                ref bufOffset,
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
        //  ref ToolCommonData.ePrintLang    xcrntPDL,
            ref Boolean                         endReached)
        {
            PrnParseConstants.eContType contType;

            contType = PrnParseConstants.eContType.None;

            Int32 prefixLen = 0,
                  contDataLen = 0,
                  downloadRem = 0,
                  termPos;

            Boolean badSeq = false,
                    continuation = false,
                    backTrack = false;

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

            if (contType == PrnParseConstants.eContType.HPGL2Binary)
            {
                //-------------------------------------------------------------------//
                //                                                                   //
                // Continuation of an encoded (binary) sequence.                     //
                // Search for standard ";" terminator.                               //
                //                                                                   //
                //-------------------------------------------------------------------//

                termPos = 0;

                for (Int32 i = bufOffset; i < (bufOffset + bufRem); i++)
                {
                    if (_buf[i] == PrnParseConstants.asciiSemiColon)
                    {
                        termPos = i;
                        i = bufOffset + bufRem; // force end of loop
                    }
                }

                if (termPos == 0)
                {
                    //----------------------------------------------------------------//
                    //                                                                //
                    // Standard ";" terminator not found in remainder of current      //
                    // 'block'.                                                       //
                    // Display details of unfinished (binary) sequence, then          //
                    // carry on so that a further continuation action is invoked.     //
                    //                                                                //
                    //----------------------------------------------------------------//

                    displayHPGL2Command (ref bufOffset,
                                        bufRem,
                                        0,
                                        true,
                                        true,
                                        true,
                                        "");

                    bufRem = 0;
                }
                else
                {
                    //----------------------------------------------------------------//
                    //                                                                //
                    // Standard ";" terminator found.                                 //
                    // Display details of unfinished (binary) sequence, then          //
                    // unset continuation markers to carry on with normal analysis.   //
                    //                                                                //
                    //----------------------------------------------------------------//
                    
                    Int32 seqLen = termPos + 1;

                    displayHPGL2Command (ref bufOffset,
                                        seqLen,
                                        0,
                                        true,
                                        false,
                                        true,
                                        "");

                    bufRem = bufRem - seqLen;
                    bufOffset = bufOffset + seqLen;

                    _linkData.resetContData ();
                }
            }
            else if (contType == PrnParseConstants.eContType.HPGL2Label)
            {
                //-------------------------------------------------------------------//
                //                                                                   //
                // Continuation of a LB (Label) value.                               //
                //                                                                   //
                //-------------------------------------------------------------------//

                Boolean termFound;

                termFound = PrnParseData.processLines (
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _linkData,
                    ToolCommonData.ePrintLang.HPGL2,
                    _buf,
                    _fileOffset,
                    bufRem,
                    ref bufRem,
                    ref bufOffset,
                    ref continuation,
                    true,
                    false,
                    _labelTrans,
                    _labelTerm,
                    "HP-GL/2 Label",
                    0,
                    _indxCharSetSubAct,
                    (Byte) _valCharSetSubCode,
                    _indxCharSetName,
                    _indxOffsetFormat,
                    _analysisLevel);

                if (termFound)
                    _linkData.resetContData ();
            }
            else if ((contType == PrnParseConstants.eContType.HPGL2Long) ||
                     (contType == PrnParseConstants.eContType.HPGL2LongQuote))
            {
                //-------------------------------------------------------------------//
                //                                                                   //
                // Continuation of a non-binary command which is larger overall than //
                // a 'read block'.                                                   //
                // The first part will already have been processed and displayed.    //
                //                                                                   //
                //-------------------------------------------------------------------//

                continuation = true;

                badSeq = processHPGL2Command (ref bufRem,
                                              ref bufOffset,
                                              ref continuation);

                if (badSeq)
                    invalidSeqFound = true;

                if (!continuation)
                    _linkData.resetContData ();
            }
            else
            {
                //-------------------------------------------------------------------//
                //                                                                   //
                // Previous 'block' ended with a partial match of a HPGL2 sequence,  //
                // or Special language-switching sequence, or with too few           //
                // characters to identify the sequence, or with an un-terminated     //
                // command.                                                          //
                // The continuation action has already reset the buffer, so now      //
                // unset the marker.                                                 //
                //                                                                   //
                //-------------------------------------------------------------------//

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
            PrnParseConstants.eContType contType =
                PrnParseConstants.eContType.None;

            Boolean continuation = false;
            Boolean langSwitch = false;
            Boolean badSeq = false;
            Boolean invalidSeqFound = false;
            Boolean dummyBool = false;

            continuation = false;
            _finalByte = PrnParseConstants.asciiSemiColon;

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
                    // Check that last sequence was terminated correctly.     //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (_finalByte != PrnParseConstants.asciiSemiColon)
                    {
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.MsgComment,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "Comment",
                            "",
                            "Previous sequence not " +
                            "terminated by semi-colon");
                    }

                    //--------------------------------------------------------//
                    //                                                        //
                    // Switch to PCL language processing.                     //
                    //                                                        //
                    // Note that, in theory, only a few escape sequences are  //
                    // expected:                                              //
                    //      <Esc>E          Printer Reset                     //
                    //      <Esc>%#A        Switch to PCL                     //
                    //      <Esc>%-12345X   Universal Exit Language           //
                    // but if we find an escape sequence, it's certainly not  //
                    // HP-GL/2, unless part of a binary sequence (e.g. for    //
                    // the PE command - but this is processed elsewhere).     //
                    //                                                        //
                    //--------------------------------------------------------//

                    langSwitch = true;

                    crntPDL = ToolCommonData.ePrintLang.PCL;
                    
                    /*
                    //--------------------------------------------------------//
                    //                                                        //
                    // TODO: Check for special language_switching sequences?  //
                    //                                                        //
                    //--------------------------------------------------------//
                    */
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Sequence does not start with Escape character, so it   //
                    // should be a HP-GL/2 command.                           //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (bufRem < (HPGL2MnemonicLen + 1))
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Insufficient characters remain in buffer to        //
                        // definitely identify the sequence as HP-GL/2.       //
                        // Need minimum of 3 characters (allowing 2 for       //
                        // command and 1 for terminator).                     //
                        // Initiate a continuation action.                    //
                        //                                                    //
                        //----------------------------------------------------//

                        continuation = true;

                        contType = PrnParseConstants.eContType.HPGL2;

                        _linkData.setBacktrack (contType, - bufRem);
                    }
                    else if ((PrnParseCommon.isAlphabetic(_buf[bufOffset]))
                                       &&
                             (PrnParseCommon.isAlphabetic(_buf[bufOffset + 1])))
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Sequence starts with two alphabetic characters, so //
                        // chances are that it is a HP-GL/2 command.          //
                        //                                                    //
                        // Note that command mnemonics are usually            //
                        // upper-case, but can be lower-case.                 //
                        //                                                    //
                        //----------------------------------------------------//

                        badSeq = processHPGL2Command (ref bufRem,
                                                      ref bufOffset,
                                                      ref continuation);

                        if (badSeq)
                            invalidSeqFound = true;
                    }
                    else if (_buf [bufOffset] ==
                        PrnParseConstants.asciiSemiColon)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Standard HP-GL/2 sequence terminator found.        //
                        // This is normally included in the previous sequence //
                        // (if any) but if that sequence was quoted (i.e. CO  //
                        // Comment command) it will not have been.            //
                        //                                                    //
                        //----------------------------------------------------//

                        displayHPGL2Command (ref bufOffset,
                                             1,
                                             0,
                                             false,
                                             false,
                                             false,
                                             "HP-GL/2 terminator");

                        bufOffset++;
                        bufRem--;
                    }
                    else
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Not a HP-GL/2 command.                             //
                        //                                                    //
                        // Check for a control code character first.          //
                        //                                                    //
                        //----------------------------------------------------//

                        Boolean knownWS = false;

                        String desc = "";

                        Byte c1 = _buf [bufOffset];

                        knownWS = HPGL2ControlCodes.checkTag (
                                    c1,
                                    ref desc);

                        if (knownWS)
                        {
                            HPGL2ControlCodes.incrementStatsCount (
                                c1,
                                _analysisLevel);

                            PrnParseCommon.addDataRow (
                                PrnParseRowTypes.eType.HPGL2ControlCode,
                                _table,
                                PrnParseConstants.eOvlShow.None,
                                _indxOffsetFormat,
                                _fileOffset + bufOffset,
                                _analysisLevel,
                                "HP-GL/2 Control Code",
                                "0x" + c1.ToString ("x2"),
                                desc);

                            bufOffset++;
                            bufRem--;
                        }
                        else
                        {
                            //----------------------------------------------------//
                            //                                                    //
                            // Display unexpected/invalid sequence up to the next //
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
                                ToolCommonData.ePrintLang.HPGL2,
                                _buf,
                                _fileOffset,
                                bufRem,
                                ref bufRem,
                                ref bufOffset,
                                ref dummyBool,
                                true,
                                false,
                                false,
                                PrnParseConstants.asciiEsc,
                                "Data",
                                0,
                                _indxCharSetSubAct,
                                (Byte)_valCharSetSubCode,
                                _indxCharSetName,
                                _indxOffsetFormat,
                                _analysisLevel);
                        }
                    }
                }
            }
            
            return invalidSeqFound;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s H P G L 2 C o m m a n d                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Process the buffer starting with the command mnemonic discovered.  //
        //                                                                    //
        // External checks will have ensured that at least 3 characters       //
        // (2 for the command mnemonic, and 1 for the first parameter byte,   //
        // or the terminator character) remain in the buffer, when this       //
        // method is called.                                                  //
        //                                                                    //
        // The exception to this is when processing continuations of 'long'   //
        // commands (which overall exceed the length of the 'read block'),    //
        // where the continuation will hence not start with the command       //
        // menemonic.                                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean processHPGL2Command(ref Int32   bufRem,
                                            ref Int32   bufOffset,
                                            ref Boolean continuation)
        {
            PrnParseConstants.eContType contType =
                PrnParseConstants.eContType.None;

            Byte cmdByteA = 0x20,
                 cmdByteB = 0x20;

            Byte paraByte1 = 0x20,
                 crntByte;

            Int32 seqLen = 0,
                  prefixLen = 0,
                  contDataLen = 0,
                  downloadRem = 0;

            Boolean seqKnown,
                    backTrack = false,
                    inclusiveTerm = false,
                    termFound,
                    firstQuoteFound = false;

            Boolean optReset = false,
                    optBinarySeq = false,
                    optToggleTransparency = false,
                    optSetLabelTerm = false,
                    optUseLabelTerm = false,
                    optUseStdTerm = false,
                    optQuoted = false,
                    optSymbolMode = false;

            Boolean invalidSeqFound;

            String desc = "",
                   command,
                   showChar;

            invalidSeqFound = false;

            //----------------------------------------------------------------//
            //                                                                //
            // Check if this is the continuation of a long (> 'read block')   //
            // command.                                                       //
            //                                                                //
            //----------------------------------------------------------------//

            if (_linkData.isContinuation ())
            {
                contType = PrnParseConstants.eContType.None;

                _linkData.getContData (ref contType,
                                       ref prefixLen,
                                       ref contDataLen,
                                       ref downloadRem,
                                       ref backTrack,
                                       ref cmdByteA,
                                       ref cmdByteB);

                if (contType == PrnParseConstants.eContType.HPGL2Long)
                {
                    paraByte1 = _buf[bufOffset];
                    firstQuoteFound = false;
                }
                else if (contType == 
                    PrnParseConstants.eContType.HPGL2LongQuote)
                {
                    paraByte1 = _buf[bufOffset];
                    firstQuoteFound = true;
                }
                else
                {
                    // TODO - internal error
                    invalidSeqFound = true;
                }
            }
            else
            {
                contType = PrnParseConstants.eContType.None;
                cmdByteA = _buf[bufOffset];
                cmdByteB = _buf[bufOffset + 1];
                paraByte1 = _buf[bufOffset + 2];
                firstQuoteFound = false;
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Determine whether command mnemonic is known or not.            //
            //                                                                //
            //----------------------------------------------------------------//

            command = ((Char) cmdByteA).ToString () +           // need to convert these to UPPER for check
                      ((Char) cmdByteB).ToString ();

            seqKnown = HPGL2Commands.checkCmd ((_analysisLevel + _macroLevel),
                                               command,
                                               ref optReset,
                                               ref optBinarySeq,
                                               ref optToggleTransparency,
                                               ref optSetLabelTerm,
                                               ref optUseLabelTerm,
                                               ref optUseStdTerm,
                                               ref optQuoted,
                                               ref optSymbolMode,
                                               ref desc);

            
            //----------------------------------------------------------------//
            //                                                                //
            // Process command.                                               //
            //                                                                //
            //----------------------------------------------------------------//

            if (seqKnown && optUseLabelTerm)
            {
                //------------------------------------------------------------//
                //                                                            //
                // This command is the LB (Label) command, which defines      //
                // variable (text) data to be output.                         //
                // The variable data is terminated by the current Label       //
                // terminator character (<ETX> by default, but may be changed //
                // by the DT (DefineLabelTerminator) command).                //
                //                                                            //
                // Any continuation action for the 'label' part of the        //
                // command will be handled external to this procedure, hence  //
                // we should never reach this point in a continuation         //
                // situation.                                                 //
                //                                                            //
                //------------------------------------------------------------//

                seqLen = HPGL2MnemonicLen;

                displayHPGL2Command(ref bufOffset,
                                    seqLen,
                                    HPGL2MnemonicLen,
                                    false,
                                    false,
                                    false,
                                    desc);

                bufOffset = bufOffset + seqLen;
                bufRem    = bufRem - seqLen;

                termFound = PrnParseData.processLines (
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _linkData,
                    ToolCommonData.ePrintLang.HPGL2,
                    _buf,
                    _fileOffset,
                    bufRem,
                    ref bufRem,
                    ref bufOffset,
                    ref continuation,
                    true,
                    false,
                    _labelTrans,
                    _labelTerm,
                    "HP-GL/2 Label",
                    0,
                    _indxCharSetSubAct,
                    (Byte) _valCharSetSubCode,
                    _indxCharSetName,
                    _indxOffsetFormat,
                    _analysisLevel);
            }
            else if (seqKnown && optSymbolMode)
            {
                //------------------------------------------------------------//
                //                                                            //
                // This command is the SM (Symbol Mode) command, which        //
                // defines a single symbol (character) to be output at each   //
                // X,Y coordinate point using the PA, PD, PE, PR, and PU      //
                // commands.                                                  //
                //                                                            //
                // The command mnemonic may be followed immediately by the    //
                // standard ";" terminator character, which indicates         //
                // termination of symbol mode; otherwise, the next character  //
                // is assumed to define the desired symbol.                   //
                //                                                            //
                // It is unclear what characters are allowed; the             //
                // specification indicates that valid characters are those    //
                // with codes:                                                //
                //  decimal  33 -  58 (0x21-3a),                              //
                //           60 - 126 (0x3c-7e)                               //
                //          161       (0xa1)                                  //
                //      and 254       (0xfe)                                  //
                // but perhaps it should be:                                  //  
                //          161 - 254 (0xa1-fe)  ???                          //
                //                                                            //
                // We'll just allow any character except ";".                 // 
                //                                                            //
                //------------------------------------------------------------//

                Byte paraByte2 = 0x20;

                termFound = false;
                inclusiveTerm = false;

                if (paraByte1 == PrnParseConstants.asciiSemiColon)
                {
                    seqLen = HPGL2MnemonicLen;

                    termFound = true;
                    inclusiveTerm = true;
                }
                else
                {
                    seqLen = HPGL2MnemonicLen + 1;
                  
                    paraByte2 = _buf [bufOffset + 3];

                    if (paraByte2 == PrnParseConstants.asciiSemiColon)
                    {
                        termFound = true;
                        inclusiveTerm = true;
                    }
                    else if (PrnParseCommon.isAlphabetic (paraByte2))
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Alphabetic character found.                        //
                        // This may be the start of the next HP-GL/2 command. //
                        //                                                    //
                        //----------------------------------------------------//

                        termFound = true;
                    }
                }
/*
                displayHPGL2Command (ref bufOffset,
                                    seqLen,
                                    HPGL2MnemonicLen,
                                    false,
                                    false,
                                    false,
                                    desc);

                bufOffset = bufOffset + seqLen;
                bufRem = bufRem - seqLen;
 */
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // Not LB (Label) command.                                    //
                // Search for command terminator and display command details. //
                //                                                            //
                //------------------------------------------------------------//

                int startOffset;

                termFound       = false;
                inclusiveTerm   = false;

                if (continuation)
                    startOffset = bufOffset;
                else
                    startOffset = bufOffset + HPGL2MnemonicLen;

                for (int i = startOffset;
                     (i < bufOffset + bufRem) && !termFound;
                     i++)
                {
                    crntByte = _buf[i];

                    if (crntByte == PrnParseConstants.asciiEsc)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Unexpected Escape character found.                 //
                        // Do not include the character in the sequence.      //
                        //                                                    //
                        //----------------------------------------------------//

                        termFound = true;
                    }
                    else if (seqKnown && optQuoted)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Command parameter is expected to be delimited by   //
                        // double-quote characters, although the initial      //
                        // quote could be preceded by space character(s).     //
                        // Include the delimiter characters in the display    //
                        // sequence.                                          //
                        //                                                    //
                        //----------------------------------------------------//

                        if (firstQuoteFound)
                        {
                            if (crntByte == PrnParseConstants.asciiQuote)
                            {
                                inclusiveTerm = true;
                                termFound     = true;
                            }
                        }
                        else if (crntByte == PrnParseConstants.asciiQuote)
                        {
                            firstQuoteFound = true;
                        }
                        else if (crntByte != PrnParseConstants.asciiSpace)
                        {
                            invalidSeqFound = true;

                            PrnParseCommon.addTextRow (
                                PrnParseRowTypes.eType.MsgWarning,
                                _table,
                                PrnParseConstants.eOvlShow.None,
                                "",
                                "*** Warning ***",
                                "",
                                "Invalid character prior to " +
                                "opening quote character");
                        }
                    }
                    else if (crntByte == PrnParseConstants.asciiSemiColon)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Standard HP-GL/2 sequence terminator found.        //
                        // Include this character in the output sequence.     //
                        //                                                    //
                        //----------------------------------------------------//

                        inclusiveTerm = true;
                        termFound     = true;
                    }
                    else if (seqKnown && optUseStdTerm)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Command (e.g. PE - PolyLine Encoded) requires      //
                        // termination by the standard HP-GL/2 sequence       //
                        // terminator, so do not search for other terminator  //
                        // characters.                                        //
                        //                                                    //
                        //----------------------------------------------------//
                    }
                    else if (PrnParseCommon.isAlphabetic(crntByte))
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Alphabetic character found.                        //
                        // This may be the start of the next HP-GL/2 command. //
                        //                                                    //
                        //----------------------------------------------------//

                        termFound = true;
                    }

                    if (termFound)
                    {
                        seqLen = i - bufOffset;
                    }
                }
            }

            if (!termFound)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Command terminator not found in buffer.                    //
                // Initiate continuation action.                              //
                //                                                            //
                //------------------------------------------------------------//

                seqLen = bufRem;

                if (seqKnown && optBinarySeq)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Command (e.g. PE) has encoded arguments, and could     //
                    // potentially be thousands of characters in length.      //
                    // Display [length] details of this first portion of the  //
                    // command, and initiate special continuation.            //
                    //                                                        //
                    //--------------------------------------------------------//

                    displayHPGL2Command(ref bufOffset,
                                        seqLen,
                                        HPGL2MnemonicLen,
                                        true,
                                        true,
                                        false,
                                        desc);

                    bufOffset = bufOffset + seqLen;
                    bufRem    = bufRem - seqLen;

                    continuation = true;
                    contType = PrnParseConstants.eContType.HPGL2Binary;

                    _linkData.setContinuation (contType);
                }
                else if (seqKnown && optUseLabelTerm)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Incomplete LB (Label) command.                         //
                    // The command mnemonic (and probably the first part of   //
                    // the label) has already been displayed, but the label   //
                    // terminator has not yet been found.                     //
                    //                                                        //
                    //--------------------------------------------------------//

                    continuation = true;
                    contType = PrnParseConstants.eContType.HPGL2Label;

                    _linkData.setContinuation (contType);
                }
                else if (seqLen == PrnParseConstants.bufSize)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // A command has been encountered, which is longer than a //
                    // standard 'read block'.                                 //
                    // Display this part of the command, and initiate         //
                    // appropriate continuation action.                       //
                    //                                                        //
                    //--------------------------------------------------------//

                    displayHPGL2Command(ref bufOffset,
                                        seqLen,
                                        HPGL2MnemonicLen,
                                        optBinarySeq,
                                        true,
                                        continuation,
                                        desc);

                    bufOffset = bufOffset + seqLen;
                    bufRem    = bufRem - seqLen;

                    continuation = true;

                    if (firstQuoteFound)
                        contType = PrnParseConstants.eContType.HPGL2LongQuote;
                    else
                        contType = PrnParseConstants.eContType.HPGL2Long;

                    _linkData.setContData(contType,
                                          HPGL2MnemonicLen,
                                          0,
                                          0,
                                          false,
                                          cmdByteA,
                                          cmdByteB);
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Standard continuation.                                 //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (_linkData.IsEofSet)
                        termFound = true;
                    else
                    {
                        continuation = true;
                        contType = PrnParseConstants.eContType.HPGL2;

                        _linkData.setBacktrack (contType, -bufRem);
                    }
                }
            }

            if (termFound)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Command terminator found in buffer.                        //
                //                                                            //
                //------------------------------------------------------------//

                if (!optUseLabelTerm)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Display HP-GL/2 command.                               //
                    // (except for the LB command, which has already been     //
                    // dealt with).                                           //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (inclusiveTerm)
                    {
                        seqLen = seqLen + 1;
                    }

                    displayHPGL2Command(ref bufOffset,
                                        seqLen,
                                        HPGL2MnemonicLen,
                                        optBinarySeq,
                                        false,
                                        continuation,
                                        desc);

                    bufOffset = bufOffset + seqLen;
                    bufRem    = bufRem - seqLen;

                    continuation = false;
                }

                //------------------------------------------------------------//
                //                                                            //
                // Implement active 'set' type options.                       //
                //                                                            //
                //------------------------------------------------------------//

                if (seqKnown && optReset)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // This is either the DF (Default Values) command or IN   //
                    // (Initialise) command, which reset certain HP-GL/2      //
                    // settings to default values.                            //
                    //                                                        //
                    //--------------------------------------------------------//

                    resetHPGL2();
                }
                else if (seqKnown && optSetLabelTerm)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // This is the DT (Define Label Terminator) command,      //
                    // which sets the terminator character for the Label      //
                    // command.                                               //
                    // The new value (default = <ETX>) remains in effect      //
                    // until another DT command is executed, or the printer   //
                    // is reset (via certain PCL sequences, or the DF         //
                    // (Default Values) or IN (Initialise) commands).         //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (seqLen == HPGL2MnemonicLen)
                    {
                        _labelTerm = PrnParseConstants.asciiETX;
                    }
                    else if (paraByte1 == PrnParseConstants.asciiSemiColon)
                    {
                        _labelTerm = PrnParseConstants.asciiETX;
                    }
                    else if ((paraByte1 == PrnParseConstants.asciiNUL) ||
                             (paraByte1 == PrnParseConstants.asciiLF)  ||
                             (paraByte1 == PrnParseConstants.asciiEsc))
                    {
                        invalidSeqFound = true;

                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.MsgWarning,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "*** Warning ***",
                            "",
                            "Invalid Label terminator");
                                   
                         _labelTerm = PrnParseConstants.asciiETX;
                    }
                    else
                    {
                        _labelTerm = paraByte1;
                    }

                    showChar = PrnParseData.processByte (
                        _labelTerm,
                        _indxCharSetSubAct,
                        (Byte) _valCharSetSubCode,
                        _indxCharSetName);

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgComment,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Comment",
                        "",
                        "Label terminator is now " + showChar);
                }
                else if (seqKnown && optToggleTransparency)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // This is the TD (TransparentData) command, which        //
                    // toggles the state of data transparency; this affects   //
                    // whether or not the special significance of Escape and  //
                    // other control characters is to be ignored within Label //
                    // sequences.                                             //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (seqLen == HPGL2MnemonicLen)
                    {
                        _labelTrans = false;
                    }
                    else if (paraByte1 == PrnParseConstants.asciiSemiColon)
                    {
                        _labelTrans = false;
                    }
                    else if (paraByte1 == PrnParseConstants.asciiDigit0)
                    {
                        _labelTrans = false;
                    }
                    else if (paraByte1 == PrnParseConstants.asciiDigit1)
                    {
                        _labelTrans = true;
                    }
                    else
                    {
                        invalidSeqFound = true;

                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.MsgWarning,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "*** Warning ***",
                            "",
                            "Invalid Transparency value");

                        _labelTrans = false;
                    }

                    if (_labelTrans)
                        showChar = "Set";
                    else
                        showChar = "Unset";

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgComment,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Comment",
                        "",
                        "Label transparency is now " + showChar);
                }
            }
  
            return invalidSeqFound;
        }
        
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // R e s e t H P G L 2                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Resets HPGL2 state variables.                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void resetHPGL2()
        {
            _labelTerm  = PrnParseConstants.asciiETX;
            _labelTrans = false;
        }
    }
}