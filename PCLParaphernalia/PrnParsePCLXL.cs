using System;
using System.Data;
using System.IO;
using System.Text;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines functions to parse PCLXL sequences.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    class PrnParsePCLXL
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const Int32 _decodeIndentNone = 0;
        const Int32 _decodeIndentStd  = 4;
        const Int32 _decodeAreaMax    = 48;
        const Int32 _decodeSliceMax   = 4;

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private PrnParseLinkData _linkData;
        private PrnParse _analysisOwner;
        private PrnParseOptions _options;

        private PrnParse.eParseType _parseType;

        private PrnParseConstants.eActPCLXL _attrActType;

        private PrnParseConstants.eOvlShow _operOvlShow;

        private DataTable _table;

        private PrnParseFontHddrPCLXL _parseFontHddrPCLXL;
        private PrnParseFontCharPCLXL _parseFontCharPCLXL;

        private Byte[] _buf;

        private Int32 _fileOffset;
        private Int32 _endOffset;

        private PrnParseConstants.eOptOffsetFormats _indxOffsetFormat;

        private PrnParseConstants.eOptCharSetSubActs _indxCharSetSubAct;
        private PrnParseConstants.eOptCharSets _indxCharSetName;
        private Int32 _valCharSetSubCode;
        
        private PrnParseConstants.ePCLXLBinding _startMode;
        private PrnParseConstants.ePCLXLBinding _bindType;
        
        private PCLXLOperators.eEmbedDataType _crntOperEmbedType;
        private PCLXLOperators.eEmbedDataType _prevOperEmbedType;

        private PCLXLOperators.eEmbedDataType _crntEmbedType;

        private PrnParsePCLXLElementMetrics _displayMetricsCrnt;
        private PrnParsePCLXLElementMetrics _displayMetricsEmbedByte;
        private PrnParsePCLXLElementMetrics _displayMetricsEmbedWord;
        private PrnParsePCLXLElementMetrics _displayMetricsHddr;
        private PrnParsePCLXLElementMetrics _displayMetricsNil;
        private PrnParsePCLXLElementMetrics _displayMetricsString;
        private PrnParsePCLXLElementMetrics _displayMetricsUbyte;
        private PrnParsePCLXLElementMetrics _displayMetricsUint16;

        private Int32 _analysisLevel = 0;  

        private Byte _attrID1,
                     _attrID2,
                     _crntOperID;

        private Int32 _attrIDLen,
                      _attrDataStart,
                      _operDataStart,
                      _operNum,
                      _embedDataLen,
                      _embedDataRem,
                      _attrDataVal;
 
        private Boolean _streamActive,
                        _hddrRead,
                        _attrIDFound,
                        _operIDFound,
                        _attrDataStarted,
                        _operDataStarted,
                        _attrEnumerated,
                        _attrOperEnumeration,
                        _attrUbyteAsAscii,
                        _attrUint16AsUnicode,
                        _attrValueIsEmbedLength,
                        _attrValueIsPCLArray,
                        _rawDataAfterOpTag,
                        _analyseStreams,
                        _analyseFontHddr,
                        _analyseFontChar,
                        _analysePCLFontData,
                        _analysePassThrough,
                        _verboseMode,
                        _showOperPos,
                        _showBinData,
                        _continuation,
                        _breakpoint;

        private ASCIIEncoding _ascii = new ASCIIEncoding ();

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P r n P a r s e P C L X L                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PrnParsePCLXL(PrnParse.eParseType parseType)
        {
            const Boolean flagNone            = false;
            const Boolean flagArrayType       = true;
            const Boolean flagUbyteAsAscii    = true;
       //   const Boolean flagUint16AsUnicode = true;

            const Int32 sizeSingle = 1;
            const Int32 sizeDouble = 2;
            const Int32 sizeQuad   = 4;

            _parseType = parseType;

            _parseFontHddrPCLXL = new PrnParseFontHddrPCLXL ();
            _parseFontCharPCLXL = new PrnParseFontCharPCLXL ();

            _displayMetricsCrnt = new PrnParsePCLXLElementMetrics (
                flagNone,
                flagNone,
                flagNone,
                _decodeIndentStd,
                sizeSingle,
                sizeSingle,
                PCLXLDataTypes.eBaseType.Unknown); 

            _displayMetricsEmbedByte = new PrnParsePCLXLElementMetrics (
                flagNone,
                flagNone,
                flagNone,
                _decodeIndentStd,
                sizeSingle,
                sizeSingle,
                PCLXLDataTypes.eBaseType.Ubyte);

            _displayMetricsEmbedWord = new PrnParsePCLXLElementMetrics (
                flagNone,
                flagNone,
                flagNone,
                _decodeIndentStd,
                sizeSingle,
                sizeQuad,
                PCLXLDataTypes.eBaseType.Sint32);

            _displayMetricsHddr = new PrnParsePCLXLElementMetrics (
                flagUbyteAsAscii,
                flagNone,
                flagArrayType,
                _decodeIndentNone,
                sizeSingle,
                sizeSingle,
                PCLXLDataTypes.eBaseType.Ubyte);

            _displayMetricsNil = new PrnParsePCLXLElementMetrics (
                flagNone,
                flagNone,
                flagNone,
                _decodeIndentStd,
                sizeSingle,
                sizeSingle,
                PCLXLDataTypes.eBaseType.Unknown);

            _displayMetricsString = new PrnParsePCLXLElementMetrics (
                flagUbyteAsAscii,
                flagNone,
                flagArrayType,
                _decodeIndentStd,
                sizeSingle,
                sizeSingle,
                PCLXLDataTypes.eBaseType.Ubyte);

            _displayMetricsUbyte = new PrnParsePCLXLElementMetrics (
                flagNone,
                flagNone,
                flagNone,
                _decodeIndentStd,
                sizeSingle,
                sizeSingle,
                PCLXLDataTypes.eBaseType.Ubyte);

            _displayMetricsUint16 = new PrnParsePCLXLElementMetrics (
                flagNone,
                flagNone,
                flagNone,
                _decodeIndentStd,
                sizeSingle,
                sizeDouble,
                PCLXLDataTypes.eBaseType.Uint16);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i s W h i t e s p a c e T a g                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Check if supplied tag is a Whitespace tag.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean isWhitespaceTag (Byte crntByte)
        {
            return PCLXLWhitespaces.isKnownTag (crntByte);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m a k e O v e r l a y I n s e r t H e a d e r                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Make Overlay insert header action.                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void makeOverlayInsertHeader(BinaryWriter binWriter,
                                            Boolean encapsulate,
                                            String streamName,
                                            DataTable table)
        {
            if (encapsulate)
            {
                PCLXLWriter.streamBegin (binWriter, streamName);

                PrnParseCommon.addDataRow (
                    PrnParseRowTypes.eType.MsgComment,
                    table,
                    PrnParseConstants.eOvlShow.Insert,
                    _indxOffsetFormat,
                    (Int32) PrnParseConstants.eOffsetPosition.StartOfFile,
                    _analysisLevel,
                    "PCLXL structure",
                    "0x" + "c8c1....",
                    "BeginStream structure for stream '" + streamName + "'");
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m a k e O v e r l a y I n s e r t T r a i l e r                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Make Overlay insert trailer action.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void makeOverlayInsertTrailer(BinaryWriter binWriter,
                                             Boolean flagRestoreGS,
                                             Boolean encapsulate,
                                             DataTable table)
        {

            if (flagRestoreGS)
            {
                String descText;

                PCLXLWriter.writeOperator (binWriter,
                                           PCLXLOperators.eTag.PopGS,
                                           encapsulate);

                if (encapsulate)
                    descText = "PopGS (encapsulated within" +
                               " ReadStream structure)";
                else
                    descText = "PopGS";

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLXLOperator,
                     table,
                     PrnParseConstants.eOvlShow.Insert,
                     "",
                     "PCLXL Operator",
                     "0x60",
                     descText);
            }

            if (encapsulate)
            {
                PCLXLWriter.streamEnd (binWriter);

                PrnParseCommon.addDataRow (
                    PrnParseRowTypes.eType.PCLXLOperator,
                    table,
                    PrnParseConstants.eOvlShow.Insert,
                    _indxOffsetFormat,
                    (Int32) PrnParseConstants.eOffsetPosition.EndOfFile,
                    _analysisLevel,
                    "PCLXL Operator",
                    "0x5d",
                    "EndStream");
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a r s e B u f f e r                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Parse provided buffer, assuming that the current print language is //
        // PCL XL.                                                            //
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
            DataTable                           table,
            Boolean                             firstCall)
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
            _rawDataAfterOpTag = false;

            _analysisOwner = _linkData.AnalysisOwner;
            _analysisLevel = _linkData.AnalysisLevel;
            _crntEmbedType = _linkData.PclxlEmbedType;

            seqInvalid = false;

            //----------------------------------------------------------------//

            _indxOffsetFormat = _options.IndxGenOffsetFormat;

            _options.getOptCharSet (ref _indxCharSetName,
                                    ref _indxCharSetSubAct,
                                    ref _valCharSetSubCode);

            _endOffset = _options.ValCurFOffsetEnd;

            _options.getOptPCLXLBasic (ref _analyseFontHddr,
                                       ref _analyseFontChar,
                                       ref _analyseStreams,
                                       ref _analysePassThrough,
                                       ref _analysePCLFontData,
                                       ref _showOperPos,
                                       ref _showBinData,
                                       ref _verboseMode);
 
            _startMode = _options.IndxCurFXLBinding;

            //----------------------------------------------------------------//

            if (firstCall)
            {
                _crntOperEmbedType = PCLXLOperators.eEmbedDataType.None;
                _prevOperEmbedType = PCLXLOperators.eEmbedDataType.None;
                _crntOperID = 0;

                _streamActive = true;
                _hddrRead = false;
                _operNum = 0;

                _linkData.MakeOvlPos =
                    PrnParseConstants.eOvlPos.BeforeFirstPage;

                if ((_startMode ==
                        PrnParseConstants.ePCLXLBinding.BinaryLSFirst)  ||
                    (_startMode ==
                        PrnParseConstants.ePCLXLBinding.BinaryMSFirst))
                {
                    _hddrRead = true;
                    _bindType = _startMode;
                }
            }
            
            //----------------------------------------------------------------//

            if (linkData.isContinuation())
                seqInvalid = parseContinuation (ref bufRem,
                                                ref bufOffset,
                                                ref crntPDL,
                                                ref endReached,
                                                firstCall); 
            else
                seqInvalid = parseSequences (ref bufRem,
                                             ref bufOffset,
                                             ref crntPDL,
                                             ref endReached,
                                             firstCall);

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
            ref Boolean                         endReached,
            Boolean                             firstCall)
        {
            PrnParseConstants.eContType contType;

            contType = PrnParseConstants.eContType.None;

            Int32 prefixLen = 0,
                  contDataLen = 0,
                  binDataLen = 0,
                  downloadRem = 0;

            Boolean hddrOK = false,
                    charOK = false;

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

            if (contType == PrnParseConstants.eContType.Reset)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Continuation action was to take some action (initially to  //
                // support the 'Make Macro' breakpoint on the last (or only)  //
                // element in a PCL combination sequence) and then perform    //
                // the necesaary backtracking, but without any further action.//
                //                                                            //
                //------------------------------------------------------------//

                _linkData.resetContData ();
            }
            else if (contType == PrnParseConstants.eContType.PCLXLEmbed)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended with incomplete PCLXL embedded data //
                // sequence; the whole sequence was not completely contained  //
                // in that block.                                             //
                // Output the remaining 'download' characters (or the whole   //
                // buffer and initiate another continuation) before           //
                // continuing with the analysis.                              //
                //                                                            //
                //------------------------------------------------------------//

                if (_embedDataRem > bufRem)
                {
                    binDataLen = bufRem;
                    _embedDataRem = _embedDataRem - bufRem;
                }
                else
                {
                    binDataLen = _embedDataRem;
                    _embedDataRem = 0;
                }

                //------------------------------------------------------------//
                //                                                            //
                // Some, or all, of the download data is contained within the //
                // current 'block'.                                           //
                // Some types of download may require (optional) further      //
                // processing, by capturing (and then later analysing) the    //
                // embedded data.                                             //
                //                                                            //
                //------------------------------------------------------------//
 
                if (_crntOperEmbedType != PCLXLOperators.eEmbedDataType.None)
                {
                    if (((_crntOperEmbedType ==
                            PCLXLOperators.eEmbedDataType.PassThrough) &&
                         (_analysePassThrough))
                                                 ||
                        ((_crntOperEmbedType ==
                            PCLXLOperators.eEmbedDataType.Stream) &&
                         (_analyseStreams))
                                                 ||
                        ((_crntOperEmbedType ==
                            PCLXLOperators.eEmbedDataType.FontHeader) &&
                         (_analyseFontHddr))
                                                 ||
                        ((_crntOperEmbedType ==
                            PCLXLOperators.eEmbedDataType.FontChar) &&
                         (_analyseFontChar)))
                    {
                        _analysisOwner.embeddedDataStore (_buf,
                                                          bufOffset,
                                                          binDataLen);
                    }
                }

                PrnParseData.processBinary (
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _buf,
                    _fileOffset,
                    bufOffset,
                    binDataLen,
                    "               Data",
                    _showBinData,
                    true,
                    true,
                    _indxOffsetFormat,
                    _analysisLevel);

                //------------------------------------------------------------//
                //                                                            //
                // Adjust continuation data and pointers.                     //
                //                                                            //
                //------------------------------------------------------------//

                if (_embedDataRem == 0)
                {
                    _continuation = false;
                    _linkData.resetContData ();
                }

                bufRem = bufRem - binDataLen;
                bufOffset = bufOffset + binDataLen;
            }
            else if (contType == PrnParseConstants.eContType.PCLXLFontHddr)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended during processing of a font header  //
                // download sequence.                                         //
                //                                                            //
                //------------------------------------------------------------//

                if (firstCall)
                {
                    contType = PrnParseConstants.eContType.None;
                    _linkData.resetContData ();
                }

                hddrOK = _parseFontHddrPCLXL.analyseFontHddr (contDataLen,
                                                              _buf,
                                                              _fileOffset,
                                                              ref bufRem,
                                                              ref bufOffset,
                                                              _linkData,
                                                              _options,
                                                              _table);

                if (!hddrOK)
                    invalidSeqFound = true;
            }
            else if (contType == PrnParseConstants.eContType.PCLXLFontChar)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended during processing of a font         //
                // character download sequence.                               //
                //                                                            //
                //------------------------------------------------------------//

                if (firstCall)
                {
                    contType = PrnParseConstants.eContType.None;
                    _linkData.resetContData ();
                }

                charOK = _parseFontCharPCLXL.analyseFontChar (contDataLen,
                                                              _buf,
                                                              _fileOffset,
                                                              ref bufRem,
                                                              ref bufOffset,
                                                              _linkData,
                                                              _options,
                                                              _table);

                if (!charOK)
                    invalidSeqFound = true;
            }
            else if ((contType == PrnParseConstants.eContType.PCLXL)
                                  ||
                     (contType == PrnParseConstants.eContType.Special)
                                  ||
                     (contType == PrnParseConstants.eContType.Unknown))
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended with a partial match of a PCLXL     //
                // sequence, or with insufficient characters to identify      //
                // the type of sequence.                                      //
                // The continuation action has already reset the buffer, so   //
                // now unset the markers.                                     //
                //                                                            //
                //------------------------------------------------------------//

                _continuation = false;
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
            ref Boolean endReached,
            Boolean firstCall)
        {
            PrnParseConstants.eContType contType =
                PrnParseConstants.eContType.None;
            
            Byte crntByte;

            Boolean langSwitch = false;
            Boolean badSeq = false;
            Boolean invalidSeqFound = false;

            _continuation = false;
            _breakpoint = false;

            while (!_continuation && !_breakpoint && !langSwitch &&
                   !invalidSeqFound && !endReached && (bufRem > 0))
            {
                crntByte = _buf[bufOffset];

                //------------------------------------------------------------//
                //                                                            //
                // Process data until language-switch or end of buffer, or    //
                // specified end-point.                                       //
                //                                                            //
                //------------------------------------------------------------//

                if ((_endOffset != -1) &&
                    ((_fileOffset + bufOffset) > _endOffset))
                {
                    endReached = true;
                }
                else if (crntByte == PrnParseConstants.asciiEsc)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Escape character found.                                //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (_streamActive)
                    {
                        invalidSeqFound = true;

                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.MsgWarning,
                            _table,
                            PrnParseConstants.eOvlShow.Illegal,
                            "",
                            "*** Warning ***",
                            "",
                            "Stream still active at " +
                            "language-switch");
                    }

                    langSwitch = true;
                    crntPDL   = ToolCommonData.ePrintLang.PCL;
                    _linkData.MakeOvlPos = PrnParseConstants.eOvlPos.AfterPages;
                }
                else if (!_hddrRead)
                {
                    if (crntByte == PrnParseConstants.prescribeSCRCDelimiter)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // "!" character found; could be the start of an      //
                        // embedded Prescribe command.                        //
                        // Need at least 3 bytes to check for the Prescribe   //
                        // start sequence (which is !R! by default).          //
                        //                                                    //
                        //----------------------------------------------------//

                        if (bufRem < 3)
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Initiate continuation action.                  //
                            //                                                //
                            //------------------------------------------------//

                            _continuation = true;

                            contType = PrnParseConstants.eContType.PCLXL;

                            _linkData.setBacktrack(contType, -bufRem);
                        }
                        else
                        {
                            if ((_buf[bufOffset + 1]
                                == _linkData.PrescribeSCRC)
                                                  && 
                                (_buf[bufOffset +2] ==
                                PrnParseConstants.prescribeSCRCDelimiter))
                            {
                                langSwitch = true;
                                crntPDL = ToolCommonData.ePrintLang.Prescribe;
                            //  _linkData.MakeOvlPos =
                            //      PrnParseConstants.eOvlPos.AfterPages;
                                _linkData.PrescribeCallerPDL =
                                    ToolCommonData.ePrintLang.PCLXL;
                            }
                        }
                    }

                    if (!_continuation && !langSwitch)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Process stream Header.                             //
                        //                                                    //
                        //----------------------------------------------------//

                        badSeq = processHeader(ref bufRem,
                                                ref bufOffset);

                        if (badSeq)
                            invalidSeqFound = true;
                        else if ((_hddrRead) &&
                                 (_parseType == PrnParse.eParseType.MakeOverlay))
                        {
                            _breakpoint =
                                PrnParseMakeOvl.checkActionPCLXLPushGS(
                                            bufOffset,
                                            _fileOffset,
                                            _linkData,
                                            _table,
                                            _indxOffsetFormat);
                        }
                    }
                }
                else if ((crntByte == PrnParseConstants.pclxlAttrUbyte)
                                      ||
                         (crntByte == PrnParseConstants.pclxlAttrUint16))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Attribute definer.                                     //
                    //                                                        //
                    //--------------------------------------------------------//

                    processAttributeTag(ref bufRem,
                                         ref bufOffset);
                }
                else if ((crntByte >= PrnParseConstants.pclxlDataTypeLow)
                                      &&
                         (crntByte <= PrnParseConstants.pclxlDataTypeHigh))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Data Type tag.                                         //
                    //                                                        //
                    //--------------------------------------------------------//

                    badSeq = processDataTypeTag(ref bufRem,
                                                 ref bufOffset);

                    if (badSeq)
                        invalidSeqFound = true;
                }
                else if (isWhitespaceTag(crntByte))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // WhiteSpace (separator) value.                          //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (_attrDataStarted)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Attribute data has started, but attribute          //
                        // identifier tag has not yet been encountered; skip  //
                        // past the whitespace tag.                           //
                        // The data and anything up to the attribute tag (or  //
                        // other significant tag) will be processed when that //
                        // tag has been found.                                //
                        //                                                    //
                        //----------------------------------------------------//

                        bufOffset = bufOffset + 1;
                        bufRem = bufRem - 1;
                    }
                    else
                    {
                        processWhiteSpaceTag(ref bufRem,
                                              ref bufOffset);
                    }
                }
                else if (_attrDataStarted)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Attribute data has been started, but the expected      //
                    // Attribute tag has not been encountered.                //
                    // Revert to stored start point to display orphan         //
                    // attribute data.                                        //
                    //                                                        //
                    //--------------------------------------------------------//

                    Int32 tempLen;

                    invalidSeqFound = true;

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgWarning,
                        _table,
                        PrnParseConstants.eOvlShow.Illegal,
                        "",
                        "*** Warning ***",
                        "",
                        "The following attribute data " +
                        "appears to be orphaned:");

                    _attrIDFound = true;
                    _attrEnumerated = false;
                    _attrUbyteAsAscii = false;
                    _attrUint16AsUnicode = false;

                    tempLen = bufOffset - _attrDataStart;
                    bufRem = bufRem + tempLen;
                    bufOffset = _attrDataStart;

                    _attrDataStarted = false;
                }
                else if ((crntByte >= PrnParseConstants.pclxlOperatorLow)
                                      &&
                         (crntByte <= PrnParseConstants.pclxlOperatorHigh))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Operator tag.                                          //
                    //                                                        //
                    //--------------------------------------------------------//

                    processOperatorTag(ref bufRem,
                                        ref bufOffset);

                    if (_rawDataAfterOpTag)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // One of the special attributes which defines the    //
                        // length of (binary) data to follow the Operator tag //
                        // was encountered.                                   //
                        //                                                    //
                        //----------------------------------------------------//

                        _rawDataAfterOpTag = false;
                        _embedDataRem = _attrDataVal;

                        if (_embedDataRem < 0)
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Invalid data length.                           //
                            //                                                //
                            //------------------------------------------------//

                            PrnParseCommon.addTextRow (
                                PrnParseRowTypes.eType.MsgWarning,
                                _table,
                                PrnParseConstants.eOvlShow.Illegal,
                                "",
                                "*** Warning ***",
                                "",
                                "Invalid raw data" +
                                " length value");

                            _embedDataLen = 0;
                            _embedDataRem = 0;
                        }
                        else if (_embedDataRem > bufRem)
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Embedded data is not all contained in the      //
                            // current buffer.                                //
                            // Set markers for continuation action.           //
                            //                                                //
                            //------------------------------------------------//

                            _continuation = true;

                            _embedDataRem = _embedDataRem - bufRem;
                            _embedDataLen = bufRem;

                            contType =
                                PrnParseConstants.eContType.PCLXLEmbed;

                            _linkData.setContinuation(contType);
                        }
                        else
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Embedded data is all contained in the          //
                            // current buffer.                                //
                            //                                                //
                            //------------------------------------------------//

                            _embedDataLen = _embedDataRem;
                            _embedDataRem = 0;
                        }

                        //----------------------------------------------------//
                        //                                                    //
                        // Display (first or only part of) embedded data.     //
                        //                                                    //
                        //----------------------------------------------------//

                        PrnParseData.processBinary(
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            _buf,
                            _fileOffset,
                            bufOffset,
                            _embedDataLen,
                            "PCLXL Embedded Data",
                            _showBinData,
                            true,
                            true,
                            _indxOffsetFormat,
                            _analysisLevel);

                        bufOffset = bufOffset + _embedDataLen;
                        bufRem = bufRem - _embedDataLen;
                    }
                }
                else if ((crntByte == PrnParseConstants.pclxlEmbedData)
                                    ||
                         (crntByte == PrnParseConstants.pclxlEmbedDataByte))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Data embedded in stream.                               //
                    //                                                        //
                    //--------------------------------------------------------//

                    badSeq = processEmbeddedDataTag(ref bufRem,
                                                     ref bufOffset);
                    if (badSeq)
                        invalidSeqFound = true;
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Unknown identifier.                                    //
                    //                                                        //
                    //--------------------------------------------------------//

                    invalidSeqFound = true;

                    showElement(bufOffset,
                                 1,
                                 "PCLXL",
                                 "*** Unidentified data ***",
                                 true,
                                 _displayMetricsNil,
                                 PrnParseConstants.eOvlShow.Illegal,
                                 PrnParseRowTypes.eType.MsgWarning);

                    bufOffset = bufOffset + 1;
                    bufRem = bufRem - 1;
                }
            }

            if (langSwitch)
            {
                _operNum      = 0;
                _hddrRead     = false;
                _streamActive = false;
            }
            
            return invalidSeqFound;
        }
        
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s A t t r i b u t e T a g                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Process Attribute definer and tag.                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processAttributeTag (ref Int32 bufRem,
                                          ref Int32 bufOffset)
        {
            PrnParseConstants.eContType contType;

            PrnParseConstants.eOvlAct attrOvlAct =
                PrnParseConstants.eOvlAct.None;

            Byte crntByte;

            Boolean dummyBool = false;

            Int32 attrPos = bufOffset;

            String descPrefix = "  ";

            String desc = "";

            //----------------------------------------------------------------//
            //                                                                //
            // Initialise.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            crntByte = _buf[bufOffset];

            //----------------------------------------------------------------//
            //                                                                //
            // The definer is either:                                         //
            //    attr_ubyte     this indicates that a single-byte attribute  //
            //                   identifier tag follows;                      //
            //    attr_uint16    this indicates that a double-byte attribute  //
            //                   identifier tag follows.                      //
            //                                                                //
            // Note that no double-byte attribute identifiers have yet been   //
            // defined (as at Class/Revision 3.0 of the protocol).            //
            //                                                                //
            //----------------------------------------------------------------//

            if (crntByte == PrnParseConstants.pclxlAttrUbyte)
                _attrIDLen = 1;
            else
                _attrIDLen = 2;

            if (bufRem < (_attrIDLen + 2))
            {
                //------------------------------------------------------------//
                //                                                            //
                // Initiate continuation action, because there is             //
                // insufficient data left in the buffer to determine the next //
                // Attribute tag and the next character (which may be the     //
                // Operator tag (needed to interpret some attribute data      //
                // enumerations), or may be the DataType introduction for the //
                // next attribute).                                           //
                //                                                            //
                // If the Attribute definer was preceded by a DataType        //
                // value/identity sequence (the normal case), take this into  //
                // account in determining the 'backtrack' position ( where to //
                // restart the analysis after further data has been read from //
                // the file).                                                 //
                //                                                            //
                //------------------------------------------------------------//

                Int32 backLen;

                _continuation = true;

                contType = PrnParseConstants.eContType.PCLXL;

                if (_operDataStarted)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // We haven't yet determined the Operator; revert to the  //
                    // beginning of the data for the first attribute.         //
                    //                                                        //
                    //--------------------------------------------------------//

                    _operDataStarted = false;
                    _attrDataStarted = false;

                    backLen = (bufOffset + bufRem) - _operDataStart;
                }
                else if (_attrDataStarted)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Revert to the beginning of the data for the current    //
                    // attribute.                                             //
                    //                                                        //
                    //--------------------------------------------------------//

                    _attrDataStarted = false;

                    backLen = (bufOffset + bufRem) - _attrDataStart;
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Attribute not preceded by DataType value/identity      //
                    // sequence?                                              //
                    // This should only be possible with malformed PCLXL (?)  //
                    //                                                        //
                    //--------------------------------------------------------//

                    backLen = bufRem;
                }

                _linkData.setBacktrack (contType, - backLen);
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // Attribute identifier found.                                //
                //                                                            //
                // Obtain description from Attribute table (or use 'Unknown'  //
                // entry).                                                    //
                //                                                            //
                // Then check whether there is associated Attribute data      //
                // (which may need to know the attribute identity (and, in    //
                // some cases, the operator identity as well) in order for    //
                // enumerated values to be interpreted correctly) to be       //
                // displayed first.                                           //
                //                                                            //
                //------------------------------------------------------------//

                _attrIDFound = true;
                if (_attrIDLen == 1)
                {
                    _attrID1 = _buf[bufOffset + 1];
                    _attrID2 = 0x00;
                }
                else
                {
                    _attrID1 = _buf[bufOffset + 1];
                    _attrID1 = _buf[bufOffset + 2];
                }

                PCLXLAttributes.checkTag (_attrIDLen,
                                          _attrID1,
                                          _attrID2,
                                          ref dummyBool,
                                          ref _attrEnumerated,
                                          ref _attrOperEnumeration,
                                          ref _attrUbyteAsAscii,
                                          ref _attrUint16AsUnicode,
                                          ref _attrValueIsEmbedLength,
                                          ref _attrValueIsPCLArray,
                                          ref _attrActType,
                                          ref attrOvlAct,
                                          ref desc);
                
                if (!_operIDFound)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // We've not yet reached the Operator identifier tag.     //
                    //                                                        //
                    // With some Attributes, enumerated values depend on the  //
                    // Operator identifier as well as the Attribute           //
                    // identifier, so we have to read the Operator tag before //
                    // processing the Attribute list.                         //
                    //                                                        //
                    // Another reason for reading forward to the Operator     //
                    // tag, before processing the Attribute list, is that     //
                    // some Embedded Data 'chunks' (e.g. those associated     //
                    // with the PassThrough operator, or the ReadFontHeader   //
                    // operator) need to be 'stitched together' if they are   //
                    // to be separately analysed; but we only do this if the  //
                    // chunks are for the same Attribute and Operator,        //
                    // otherwise we need to invoke analysis of any such       //
                    // chunks aggregated so far for an Attribute/Operator.    //
                    //                                                        //
                    // If not already done for this operator, store the start //
                    // point of the first attribute, for use on a subsequent  //
                    // pass.                                                  //
                    //                                                        //
                    // Then adjust pointers, so that we carry on processing   //
                    // (without reporting details) until we find the operator //
                    // tag.                                                   //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (!_operDataStarted)
                    {
                        _operDataStarted = true;
                        _operDataStart = _attrDataStart;
                    }

                    _attrDataStarted = false;
                    _attrIDFound = false;

                    bufOffset = bufOffset + (_attrIDLen + 1);
                    bufRem = bufRem - (_attrIDLen + 1);
                }
                else if (_attrDataStarted)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Revert to stored start point to display attribute data //
                    // (now that we know the attribute identity).             //
                    //                                                        //
                    //--------------------------------------------------------//

                    Int32 tempLen;
                    
                    tempLen = bufOffset - _attrDataStart;
                    bufRem = bufRem + tempLen;
                    bufOffset = _attrDataStart;

                    if (_parseType == PrnParse.eParseType.MakeOverlay)
                    {
                        //--------------------------------------------------------//
                        //                                                        //
                        // Set any 'Make Overlay' action required that will       //
                        // affect how the preceding Attribute data will be        //
                        // processed.                                             //
                        //                                                        //
                        //--------------------------------------------------------//

                        PrnParseMakeOvl.checkActionPCLXLAttr (
                            true,
                            attrOvlAct,
                            _operOvlShow,
                            _attrDataStart,
                            attrPos,
                            _fileOffset,
                            _linkData,
                            _table,
                            _indxOffsetFormat);
                    }
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Sufficient information in buffer, so the attribute     //
                    // details can now be analysed and displayed.             //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (_parseType == PrnParse.eParseType.MakeOverlay)
                    {
                        //--------------------------------------------------------//
                        //                                                        //
                        // Set any 'Make Overlay' action required that will       //
                        // affect how the preceding Attribute data will be        //
                        // processed.                                             //
                        //                                                        //
                        //--------------------------------------------------------//

                        _breakpoint = PrnParseMakeOvl.checkActionPCLXLAttr (
                            false,
                            attrOvlAct,
                            _operOvlShow,
                            _attrDataStart,
                            attrPos,
                            _fileOffset,
                            _linkData,
                            _table,
                            _indxOffsetFormat);
                    }

                    PrnParseConstants.eOvlShow makeOvlShow =
                        _linkData.MakeOvlShow;

                    if (_attrValueIsEmbedLength)
                        _rawDataAfterOpTag = true;

                    if (_verboseMode)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Display details of the Attribute Definer tag.      //
                        //                                                    //
                        //----------------------------------------------------//

                        if (crntByte == PrnParseConstants.pclxlAttrUbyte)
                        {
                            showElement (bufOffset,
                                         1,
                                         "PCLXL Attribute Def.",
                                         "  attr_ubyte",
                                         true,
                                         _displayMetricsNil,
                                         makeOvlShow,
                                         PrnParseRowTypes.eType.PCLXLAttribute);
                        }
                        else
                        {
                            showElement (bufOffset,
                                         1,
                                         "PCLXL Attribute Def.",
                                         "  attr_uint16",
                                         true,
                                         _displayMetricsNil,
                                         makeOvlShow,
                                         PrnParseRowTypes.eType.PCLXLAttribute);
                        }

                        //----------------------------------------------------//
                        //                                                    //
                        // Display details of the Attribute (name) identifier.//
                        //                                                    //
                        //----------------------------------------------------//

                        showElement (bufOffset + 1,
                                     _attrIDLen,
                                     "                Name",
                                     descPrefix + desc,
                                     true,
                                     _displayMetricsNil,
                                     makeOvlShow,
                                     PrnParseRowTypes.eType.PCLXLAttribute);
                    }
                    else
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Display details of the Attribute Definer tag and   //
                        // (name) identifier.                                 //
                        //                                                    //
                        //----------------------------------------------------//

                        showElement (bufOffset,
                                     _attrIDLen + 1,
                                     "PCLXL Attribute",
                                     descPrefix + desc,
                                     true,
                                     _displayMetricsNil,
                                     makeOvlShow,
                                     PrnParseRowTypes.eType.PCLXLAttribute);
                    }

                    //--------------------------------------------------------//
                    //                                                        //
                    // Update statistics.                                     //
                    //                                                        //
                    //--------------------------------------------------------//

                    PCLXLAttributes.incrementStatsCount (_attrIDLen,
                                                         _attrID1,
                                                         _attrID2,
                                                         _analysisLevel);

                    //--------------------------------------------------------//
                    //                                                        //
                    // Adjust pointers & flags.                               //
                    //                                                        //
                    //--------------------------------------------------------//

                    _attrIDFound = false;
                    _attrDataStarted = false;

                    bufOffset = bufOffset + (_attrIDLen + 1);
                    bufRem = bufRem - (_attrIDLen + 1);
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s D a t a T y p e T a g                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Data Type tag.                                                     //
        //                                                                    //
        // The value associated with some data types may form an enumerated   //
        // set, but the interpretation of individual values is dependent on   //
        // the associated attribute.                                          //
        //                                                                    //
        // Since the Attribute tag follows the Data Type and value, full      //
        // processing of a Data Type is delayed until the Attribute identity  //
        // is known.                                                          //
        // Hence processing of Attribute data is performed in two passes.     //
        //                                                                    //
        // Where continuation action is found necessary (because the buffer   //
        // has been exhausted before all expected data has been found), the   //
        // action will take into account any stored start point for the       //
        // Attribute data.                                                    //
        //                                                                    //
        // Note that continuation action should not occur on the second pass, //
        // because the appropriate action has already been invoked on the     //
        // first pass.                                                        //
        // (If it does, then either the design is incorrect, or the Attribute //
        // data cannot all be fitted in the buffer at one time).              //
        //                                                                    //
        // At protocol version 3.0, some enumerated values are dependent on   //
        // the associated Operator identifier, as well as the Attribute       //
        // identifier, so the 'two-phase' analysis is extended to three       //
        // phases to cater for this situation.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean processDataTypeTag (ref Int32 bufRem,
                                            ref Int32 bufOffset)
        {
            PrnParseConstants.eContType contType;
            
            PCLXLDataTypes.eBaseType baseType =
                PCLXLDataTypes.eBaseType.Unknown;

            PrnParseConstants.eOvlShow makeOvlShow;
            
            Byte crntByte;

            Int32 groupSize = 1,
                  unitSize = 1,
                  arraySize = 1,
                  seqHddrLen = 0,
                  opSeqLen = 0;

            Boolean seqKnown,
                    arrayType = false,
                    invalidArray,
                    flagReserved = false,
                    invalidSeqFound = false;

            String descPrefix = "    ";
            String desc = "";

            //----------------------------------------------------------------//
            //                                                                //
            // Initialise.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            invalidSeqFound = false;
            invalidArray = false;

            makeOvlShow = _linkData.MakeOvlShow;

            crntByte = _buf[bufOffset];

            if (!_attrDataStarted)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Store pointer to start of Attribute data, so that it can   //
                // be re-processed, in the second pass, when the Attribute    //
                // identity is known.                                         //
                //                                                            //
                //------------------------------------------------------------//

                _attrDataStarted = true;
                _attrDataStart = bufOffset;
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Obtain description from Data Type tag table (or use 'Unknown'  //
            // entry).                                                        //
            //                                                                //
            //----------------------------------------------------------------//

            seqKnown = PCLXLDataTypes.checkTag (crntByte,
                                                ref flagReserved,
                                                ref arrayType,
                                                ref groupSize,
                                                ref unitSize,
                                                ref baseType,
                                                ref desc);

            if (!seqKnown)
            {
                arraySize = 1;
                opSeqLen = 2;

                if (bufRem < opSeqLen)
                    _continuation = true;
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // Entry found in Data Type table.                            //
                //                                                            //
                //------------------------------------------------------------//

                if (!arrayType)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Non-array data type.                                   //
                    //                                                        //
                    //--------------------------------------------------------//

                    arraySize = 1;
                    seqHddrLen = 1;
                    opSeqLen = seqHddrLen + (unitSize * groupSize);

                    if (bufRem < opSeqLen)
                    {
                        _continuation = true;
                    }
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Array data type.                                       //
                    //                                                        //
                    // Next byte is expected to be another Data Type          //
                    // identifier, introducing a value representing the       //
                    // number of elements in the array.                       //
                    // This included identifier is expected be of type ubyte  //
                    // or uint16 only.                                        //
                    //                                                        //
                    // Extract this value (checking at each stage that there  //
                    // is sufficient data in the buffer, and invoking a       //
                    // continuation action if necessary).                     //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (bufRem < 2)
                    {
                        _continuation = true;
                    }
                    else
                    {
                        if (_buf[bufOffset + 1] ==
                            PrnParseConstants.pclxlDataTypeUbyte)
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Count of elements in the array is given by an  //
                            // 8-bit value in the next byte.                  //
                            //                                                //
                            //------------------------------------------------//

                            seqHddrLen = 3;

                            if (bufRem < seqHddrLen)
                                _continuation = true;
                            else
                                arraySize = _buf[bufOffset + 2];
                        }
                        else if (_buf[bufOffset + 1] ==
                            PrnParseConstants.pclxlDataTypeUint16)
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Count of elements in the array is given by a   //
                            // 16-bit value in the next two bytes.            //
                            //                                                //
                            //------------------------------------------------//

                            seqHddrLen = 4;
                        
                            if (bufRem < seqHddrLen)
                            {
                                _continuation = true;
                            }
                            else
                            {
                                if (_bindType ==
                                    PrnParseConstants.ePCLXLBinding.BinaryMSFirst)
                                {
                                    arraySize = (_buf[bufOffset + 2] * 256) +
                                                 _buf[bufOffset + 3];
                                }
                                else
                                {
                                    arraySize = (_buf[bufOffset + 3] * 256) +
                                                 _buf[bufOffset + 2];
                                }
                            }
                        }
                        else
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Invalid tag.                                   //
                            //                                                //
                            //------------------------------------------------//

                            invalidSeqFound = true;
                            invalidArray = true;

                            seqHddrLen = 2;
                            arraySize = 0;

                            if (bufRem < opSeqLen)
                                _continuation = true;
                        }

                        if (arraySize < 0)
                        {
                            PrnParseCommon.addTextRow (
                                PrnParseRowTypes.eType.MsgWarning,
                                _table,
                                PrnParseConstants.eOvlShow.Illegal,
                                "",
                                "*** Warning ***",
                                "",
                                "Invalid array size");

                            arraySize = 0;
                        }

                        opSeqLen = seqHddrLen +
                                   (arraySize * groupSize * unitSize);

                        if (bufRem < opSeqLen)
                            _continuation = true;
                    }
                }            
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Data type characteristics established.                         //
            // If sufficient data has been read, and this is the 'second      //
            // pass' (i.e. the Attribute identity is known) then process the  //
            // data type and value.                                           //
            //                                                                //
            //----------------------------------------------------------------//

            if (_continuation)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Continuation action required.                              //
                // No further processing this time round the loop.            //
                //                                                            //
                //------------------------------------------------------------//
            }
            else if (_attrIDFound)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Attribute identity is known, so this is the second pass,   //
                // where the attribute data is being reprocessed.             //
                // Display details of the attribute data.                     //
                //                                                            //
                //------------------------------------------------------------//

                //------------------------------------------------------------//
                //                                                            //
                // Update statistics.                                         //
                //                                                            //
                //------------------------------------------------------------//

                PCLXLDataTypes.incrementStatsCount (crntByte,
                                                  _analysisLevel);

                //------------------------------------------------------------//
                //                                                            //
                // Set display metrics.                                       //
                //                                                            //
                //------------------------------------------------------------//

                _attrDataStarted = false;

                _displayMetricsCrnt.setData (_attrUbyteAsAscii,
                                             _attrUint16AsUnicode,
                                             arrayType,
                                             _decodeIndentStd,
                                             groupSize,
                                             unitSize,
                                             baseType);

                //------------------------------------------------------------//
                //                                                            //
                // First display details of the Data Type tag.                //
                //                                                            //
                //------------------------------------------------------------//

                if (!arrayType)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Not array type.                                        //
                    // Display details of the Data Type tag.                  //
                    //                                                        //
                    //--------------------------------------------------------//

                    showElement (bufOffset,
                                 1,
                                 "PCLXL Data Type",
                                 descPrefix + desc,
                                 true,
                                 _displayMetricsNil,
                                 makeOvlShow,
                                 PrnParseRowTypes.eType.PCLXLDataType);
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Array type.                                            //
                    // Display details of the Data Type tag and the array     //
                    // length definer.                                        //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (!_verboseMode)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Display Data type tag and length definer together. //
                        //                                                    //
                        //----------------------------------------------------//

                        if (_buf[bufOffset + 1] ==
                            PrnParseConstants.pclxlDataTypeUbyte)
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Length definer is ubyte.                       //
                            //                                                //
                            //------------------------------------------------//

                            showElement (bufOffset,
                                         3,
                                         "PCLXL Data Type",
                                         descPrefix + desc,
                                         true,
                                         _displayMetricsNil,
                                         makeOvlShow,
                                         PrnParseRowTypes.eType.PCLXLDataType);
                        }
                        else if (_buf[bufOffset + 1] ==
                            PrnParseConstants.pclxlDataTypeUint16)
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Length definer is uint16.                      //
                            //                                                //
                            //------------------------------------------------//

                            showElement (bufOffset,
                                         4,
                                         "PCLXL Data Type",
                                         descPrefix + desc,
                                         true,
                                         _displayMetricsNil,
                                         makeOvlShow,
                                         PrnParseRowTypes.eType.PCLXLDataType);
                        }
                        else
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Length definer is invalid.                     //
                            //                                                //
                            //------------------------------------------------//

                            showElement (bufOffset,
                                         2,
                                         "PCLXL Data Type",
                                         descPrefix + desc,
                                         true,
                                         _displayMetricsNil,
                                         PrnParseConstants.eOvlShow.Illegal,
                                         PrnParseRowTypes.eType.PCLXLDataType);

                            PrnParseCommon.addTextRow (
                                PrnParseRowTypes.eType.MsgWarning,
                                _table,
                                PrnParseConstants.eOvlShow.Illegal,
                                "",
                                "*** Warning ***",
                                "",
                                "Invalid array length tag");
                        }
                    }
                    else
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Display Data type tag and length definer           //
                        // separately.                                        //
                        //                                                    //
                        //----------------------------------------------------//

                        showElement (bufOffset,
                                     1,
                                     "PCLXL Data Type",
                                     descPrefix + desc,
                                     true,
                                     _displayMetricsNil,
                                     makeOvlShow,
                                     PrnParseRowTypes.eType.PCLXLDataType);
            
                        if (_buf[bufOffset + 1] ==
                            PrnParseConstants.pclxlDataTypeUbyte)
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Length definer is ubyte.                       //
                            //                                                //
                            //------------------------------------------------//

                            showElement (bufOffset + 1,
                                         1,
                                         "PCLXL Data Type",
                                         "    ubyte",
                                         true,
                                         _displayMetricsNil,
                                         makeOvlShow,
                                         PrnParseRowTypes.eType.PCLXLDataType);

                            showElement (bufOffset + 2,
                                         1,
                                         "           Elements",
                                         "",
                                         false,
                                         _displayMetricsUbyte,
                                         makeOvlShow,
                                         PrnParseRowTypes.eType.PCLXLDataType);
                        }
                        else if (_buf[bufOffset + 1] ==
                            PrnParseConstants.pclxlDataTypeUint16)
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Length definer is uint16.                      //
                            //                                                //
                            //------------------------------------------------//

                            showElement (bufOffset + 1,
                                         1,
                                         "PCLXL Data Type",
                                         "    uint16",
                                         true,
                                         _displayMetricsNil,
                                         makeOvlShow,
                                         PrnParseRowTypes.eType.PCLXLDataType);

                            showElement (bufOffset + 2,
                                         2,
                                         "           Elements",
                                         "",
                                         false,
                                         _displayMetricsUint16,
                                         makeOvlShow,
                                         PrnParseRowTypes.eType.PCLXLDataType);
                        }
                        else
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Length definer is invalid.                     //
                            //                                                //
                            //------------------------------------------------//

                            showElement (bufOffset + 1,
                                         1,
                                         "PCLXL Data Type",
                                         "    invalid",
                                         true,
                                         _displayMetricsNil,
                                         PrnParseConstants.eOvlShow.Illegal,
                                         PrnParseRowTypes.eType.PCLXLDataType);

                            PrnParseCommon.addTextRow (
                                PrnParseRowTypes.eType.MsgWarning,
                                _table,
                                PrnParseConstants.eOvlShow.Illegal,
                                "",
                                "*** Warning ***",
                                "",
                                "Invalid array length tag");
                        }
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Now display details of the Data Type value.                //
                //                                                            //
                //------------------------------------------------------------//

                if (invalidArray)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Array type, but length definer is unrecognised.        //
                    // Hence we can't evaluate the array length.              //
                    //                                                        //
                    //--------------------------------------------------------//
                }
                else if ((_attrEnumerated) &&
                         (groupSize == 1)  &&
                         (!arrayType)      &&
                         ((baseType == PCLXLDataTypes.eBaseType.Ubyte)  ||
                          (baseType == PCLXLDataTypes.eBaseType.Sint16) ||
                          (baseType == PCLXLDataTypes.eBaseType.Sint32) ||
                          (baseType == PCLXLDataTypes.eBaseType.Uint16) ||
                          (baseType == PCLXLDataTypes.eBaseType.Uint32)))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // An integer (but not array) value can be an enumeration //
                    // value (rather than an analogue quantity), depending on //
                    // the associated Attribute identifier.                   //
                    // Search the enumerated value table.                     //
                    //                                                        //
                    //--------------------------------------------------------//

                    Boolean valKnown = false;
                    
                    String enumDesc = "";

                    UInt32 uiVal;

                    Int32 valLen = 0;

                    if (baseType == PCLXLDataTypes.eBaseType.Ubyte)
                    {
                        uiVal = _buf[bufOffset + seqHddrLen];
                        valLen = 1;
                    }
                    else
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Signed or unsigned integer value.                  //
                        //                                                    //
                        // The (2-byte or 4-byte) hexadecimal value is        //
                        // converted, byte by byte, to an unsigned integer    //
                        // (taking into account the current byte-ordering).   //
                        //                                                    //
                        //----------------------------------------------------//
                        
                        UInt32 uiSub;

                        uiVal = 0;

                        if ((baseType == PCLXLDataTypes.eBaseType.Sint16) ||
                            (baseType == PCLXLDataTypes.eBaseType.Uint16))
                            valLen = 2;
                        else
                            valLen = 4;

                        if (_bindType ==
                            PrnParseConstants.ePCLXLBinding.BinaryMSFirst)
                        {
                            for (int j=0; j < valLen; j++)
                            {
                                uiSub = _buf[bufOffset + seqHddrLen + j];
                                uiVal = (uiVal * 256) + uiSub;
                            }
                        }
                        else
                        {
                            for (int j=(valLen - 1); j >= 0; j--)
                            {
                                uiSub = _buf[bufOffset + seqHddrLen + j];
                                uiVal = (uiVal * 256) + uiSub;
                            }
                        }
                    }

                    //--------------------------------------------------------//
                    //                                                        //
                    // Check if the value is in the enumerations table.       //
                    // If the value is known, use the description associated  //
                    // with the value.                                        //
                    // Otherwise, output the value directly.                  //
                    //                                                        //
                    //--------------------------------------------------------//

                    Boolean flagValIsTxt = false;

                    valKnown = PCLXLAttrEnums.checkValue (_analysisLevel,
                                                          _crntOperID,
                                                          _attrIDLen,
                                                          _attrID1,
                                                          _attrID2,
                                                          uiVal,
                                                          _attrOperEnumeration,
                                                          ref flagValIsTxt,
                                                          ref enumDesc);

                    if (valKnown)
                    {
                        showElement (bufOffset + seqHddrLen,
                                     valLen,
                                     "           Value",
                                     descPrefix + enumDesc,
                                     true,
                                     _displayMetricsNil,
                                     makeOvlShow,
                                     PrnParseRowTypes.eType.PCLXLDataValue);
                    }
                    else
                    {
                        Int32 decodeIndent = _displayMetricsCrnt.DecodeIndent;
                        String text;

                        showElement (bufOffset + seqHddrLen,
                                     valLen,
                                     "           Value",
                                     "",
                                     false,
                                     _displayMetricsCrnt,
                                     makeOvlShow,
                                     PrnParseRowTypes.eType.PCLXLDataValue);

                        if (decodeIndent != 0)
                            text = new String (' ', decodeIndent) +
                                   "Enumerated value not recognised";
                        else
                            text = "Enumerated value not recognised";

                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.MsgWarning,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "*** Warning ***",
                            "",
                            text);
                    }
                }
                else if (baseType == PCLXLDataTypes.eBaseType.Ubyte)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Byte: unitSize will always be 1.                       //
                    //                                                        //
                    // Note that only the unsigned byte type has been defined.//
                    //                                                        //
                    //--------------------------------------------------------//

                    if (groupSize == 1)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Not grouped (i.e. not ubyte_xy , ubyte_box, etc.). //
                        //                                                    //
                        //----------------------------------------------------//

                        if (!arrayType)
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Single-byte value.                             //
                            // Output value directly.                         //
                            //                                                //
                            //------------------------------------------------//

                            showElement (bufOffset + seqHddrLen,
                                         1,
                                         "           Value",
                                         "",
                                         false,
                                         _displayMetricsCrnt,
                                         makeOvlShow,
                                         PrnParseRowTypes.eType.PCLXLDataValue);
                        }
                        else
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Array of single-byte values.                   //
                            //                                                //
                            //------------------------------------------------//

                            if (!_attrValueIsPCLArray)
                            {
                                showElement (bufOffset + seqHddrLen,
                                             arraySize * groupSize,
                                             "           Value",
                                             "",
                                             false,
                                             _displayMetricsCrnt,
                                             makeOvlShow,
                                             PrnParseRowTypes.eType.PCLXLDataValue);
                            }
                            else
                            {
                                //--------------------------------------------//
                                //                                            //
                                // ubyte_array which is to be interpreted as  //
                                // a PCL string.                              //
                                //                                            //
                                //--------------------------------------------//

                                Int32 tempOffset,
                                      tempLen;

                                String tempText;

                                tempOffset = bufOffset + seqHddrLen;
                                tempLen = arraySize * groupSize;

                                tempText = "of size " + tempLen + " bytes";

                                if (!_analysePCLFontData)
                                {
                                    showElement (tempOffset,
                                                 tempLen,
                                                 "           Value",
                                                 "",
                                                 false,
                                                 _displayMetricsCrnt,
                                                 makeOvlShow,
                                                 PrnParseRowTypes.eType.PCLXLDataValue);
                                }
                                else
                                {
                                    Boolean badSeq;

                                    PrnParseData.processBinary (
                                        _table,
                                        PrnParseConstants.eOvlShow.None,
                                        _buf,
                                        _fileOffset,
                                        tempOffset,
                                        tempLen,
                                        "           Value",
                                        _showBinData,
                                        true,
                                        true,
                                        _indxOffsetFormat,
                                        _analysisLevel);

                                    PrnParseCommon.addTextRow (
                                        PrnParseRowTypes.eType.MsgComment,
                                        _table,
                                        PrnParseConstants.eOvlShow.None,
                                        "",
                                        ">>>>>>>>>>>>>>>>>>>>",
                                        "",
                                        ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>" +
                                        ">>>>>>>>>>>>>>>>>>>>");

                                    PrnParseCommon.addTextRow (
                                        PrnParseRowTypes.eType.MsgComment,
                                        _table,
                                        PrnParseConstants.eOvlShow.None,
                                        "",
                                        "Comment",
                                        "",
                                        "Start analysis of " +
                                        "embedded PCL string");

                                    PrnParseCommon.addTextRow (
                                        PrnParseRowTypes.eType.MsgComment,
                                        _table,
                                        PrnParseConstants.eOvlShow.None,
                                        "",
                                        "Comment",
                                        "",
                                        tempText);                                  

                                    badSeq = _analysisOwner.embeddedPCLAnalyse (
                                        _buf,
                                        ref _fileOffset,
                                        ref tempLen,
                                        ref tempOffset,
                                        _linkData,
                                        _options,
                                        _table);

                                    if (badSeq)
                                        invalidSeqFound = true;

                                    PrnParseCommon.addTextRow (
                                        PrnParseRowTypes.eType.MsgComment,
                                        _table,
                                        PrnParseConstants.eOvlShow.None,
                                        "",
                                        "Comment",
                                        "",
                                        "End analysis of " +
                                        "embedded PCL string");

                                    PrnParseCommon.addTextRow (
                                        PrnParseRowTypes.eType.MsgComment,
                                        _table,
                                        PrnParseConstants.eOvlShow.None,
                                        "",
                                        "Comment",
                                        "",
                                        tempText);

                                    PrnParseCommon.addTextRow (
                                        PrnParseRowTypes.eType.MsgComment,
                                        _table,
                                        PrnParseConstants.eOvlShow.None,
                                        "",
                                        "<<<<<<<<<<<<<<<<<<<<",
                                        "",
                                        "<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<" +
                                        "<<<<<<<<<<<<<<<<<<<<");

                                }
                            }
                        }
                    }
                    else
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Data is for a grouped item (e.g. ubyte_xy,         //
                        // ubyte_box).                                        //
                        //                                                    //
                        // Note that the current (version 3.0) protocol does  //
                        // NOT define any arrays of grouped values (e.g. an   //
                        // array of ubyte_box elements).                      //
                        //                                                    //
                        //----------------------------------------------------//

                        showElement (bufOffset + seqHddrLen,
                                     arraySize * groupSize,
                                     "           Value",
                                     "",
                                     false,
                                     _displayMetricsCrnt,
                                     makeOvlShow,
                                     PrnParseRowTypes.eType.PCLXLDataValue);
                    }
                }
                else if ((baseType == PCLXLDataTypes.eBaseType.Uint16)
                                     &&
                         (groupSize == 1)
                                     &&
                         (arrayType && _attrUint16AsUnicode))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Array of uint16 values is to be treated as an array of //
                    // Unicode characters.                                    //
                    //                                                        //
                    //--------------------------------------------------------//

                    showElement (bufOffset + seqHddrLen,
                                 arraySize * unitSize,
                                 "           Value (U+)",
                                 "",
                                 false,
                                 _displayMetricsCrnt,
                                 makeOvlShow,
                                 PrnParseRowTypes.eType.PCLXLDataValue);
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Data is one (or an array of (single or grouped)):      //
                    //    integer (signed or unsigned)                        //
                    //    real    (always signed)                             //
                    //    unknown                                             //
                    //                                                        //
                    //--------------------------------------------------------//

                    showElement (bufOffset + seqHddrLen,
                                 arraySize * groupSize * unitSize,
                                 "           Value",
                                 "",
                                 false,
                                 _displayMetricsCrnt,
                                 makeOvlShow,
                                 PrnParseRowTypes.eType.PCLXLDataValue);
                }
            }

            //----------------------------------------------------------------//
            //                                                                //
            // If sufficient data has been read, adjust pointers to reference //
            // the next tag (which should be an Attribute tag).               //
            //                                                                //
            //----------------------------------------------------------------//

            if (!_continuation)
            {
                bufOffset = bufOffset + opSeqLen;
                bufRem = bufRem - opSeqLen;

                if ((bufRem == 0) && (!_attrIDFound))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // No data remains in buffer, but attribute identifier    //
                    // has not yet been found. Continuation action required.  //
                    //                                                        //
                    //--------------------------------------------------------//

                    _continuation = true;
                }
            }
            if (_continuation)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Reset pointer back to start of attribute data and invoke   //
                // continuation action.                                       //
                //                                                            //
                //------------------------------------------------------------//

                Int32 bufLen = bufOffset + bufRem;
                Int32 backLen;

                contType = PrnParseConstants.eContType.PCLXL;

                if (_operDataStarted)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // We haven't yet determined the Operator; revert to the  //
                    // beginning of the data for the first attribute.         //
                    //                                                        //
                    //--------------------------------------------------------//

                    _operDataStarted = false;
                    _attrDataStarted = false;

                    backLen = bufLen - _operDataStart;
                }
                else if (_attrDataStarted)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // We've already determined the Operator in a previous    //
                    // pass and are re-processing the attribute list from the //
                    // beginning; revert to the beginning of the data for the //
                    // current attribute.                                     //
                    //                                                        //
                    //--------------------------------------------------------//

                    _attrDataStarted = false;

                    backLen = bufLen - _attrDataStart;
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Attribute not preceded by DataType value/identity      //
                    // sequence?                                              //
                    // This should only be possible with malformed PCLXL (?)  //
                    //                                                        //
                    //--------------------------------------------------------//

                    backLen = bufRem;
                }

                _linkData.setBacktrack (contType, - backLen);

            }

            return invalidSeqFound;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s E m b e d d e d D a t a T a g                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Process Embedded Data Tag.                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean processEmbeddedDataTag (ref Int32 bufRem,
                                                ref Int32 bufOffset)
        {
            PrnParseConstants.eContType contType;

            PrnParseConstants.eOvlShow makeOvlShow;

            Byte crntByte;

            Int32 dataLenSize;

            Boolean invalidSeqFound;

            //----------------------------------------------------------------//
            //                                                                //
            // Initialise.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            invalidSeqFound = false;

            makeOvlShow = _linkData.MakeOvlShow;

            crntByte = _buf[bufOffset];

            if (crntByte == PrnParseConstants.pclxlEmbedDataByte)
                dataLenSize = 1;
            else
                dataLenSize = 4;

            if (bufRem < (dataLenSize + 1))
            {
                //------------------------------------------------------------//
                //                                                            //
                // Initiate continuation action.                              //
                //                                                            //
                //------------------------------------------------------------//

                _continuation = true;

                contType = PrnParseConstants.eContType.PCLXL;

                _linkData.setBacktrack (contType, - bufRem);
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // Interpret length field.                                    //
                //                                                            //
                //------------------------------------------------------------//

                if (dataLenSize == 1)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Single-byte length field.                              //
                    //                                                        //
                    //--------------------------------------------------------//

                    _embedDataRem = _buf[bufOffset + 1];
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Quadruple-byte length field.                           //
                    // Interpret according to stream binding.                 //
                    //                                                        //
                    //--------------------------------------------------------//

                    Int32 x;

                    _embedDataRem = 0;

                    if (_bindType ==
                        PrnParseConstants.ePCLXLBinding.BinaryMSFirst)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Binary value with Most Significant byte first.     //
                        //                                                    //
                        //----------------------------------------------------//

                        for (int i=0; i<4; i++)
                        {
                            x = _buf[bufOffset + 1 + i];
                            _embedDataRem = (_embedDataRem * 256) + x;
                        }
                    }
                    else if (_bindType ==
                        PrnParseConstants.ePCLXLBinding.BinaryLSFirst)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Binary value with Least Significant byte first.    //
                        //                                                    //
                        //----------------------------------------------------//

                        for (int i = 3; i >= 0; i--)
                        {
                            x = _buf[bufOffset + 1 + i];
                            _embedDataRem = (_embedDataRem * 256) + x;
                        }
                    }
                    else
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // ASCII binding.                                     //
                        // Not supported in this code (not sure of format,    //
                        // hence not sure how to interpret).                  //
                        // Should not get to here, since ASCII binding should //
                        // be rejected at header analysis stage.              //
                        //                                                    //
                        //----------------------------------------------------//

                        invalidSeqFound = true;

                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.MsgWarning,
                            _table,
                            PrnParseConstants.eOvlShow.Illegal,
                            "",
                            "*** Warning ***",
                            "",
                            "ASCII Binding not supported");

                        _embedDataRem = _buf[bufOffset + 1];
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Display details of embedded data introductory fields.      //
                //                                                            //
                //------------------------------------------------------------//

                if (dataLenSize == 1)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Embedded data (length value is single-byte).           //
                    //                                                        //
                    //--------------------------------------------------------//

                    showElement (bufOffset,
                                 1,
                                 "PCLXL Data Type",
                                 "    embedded_data_byte",
                                 true,
                                 _displayMetricsNil,
                                 makeOvlShow,
                                 PrnParseRowTypes.eType.PCLXLDataType);

                    showElement (bufOffset + 1,
                                 1,
                                 "PCLXL Embedded Len.",
                                 "",
                                 false,
                                 _displayMetricsEmbedByte,
                                 makeOvlShow,
                                 PrnParseRowTypes.eType.PCLXLDataValue);
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Embedded data (length value is quadruple-byte).        //
                    //                                                        //
                    //--------------------------------------------------------//

                    showElement (bufOffset,
                                 1,
                                 "PCLXL Data Type",
                                 "    embedded_data",
                                 true,
                                 _displayMetricsNil,
                                 makeOvlShow,
                                 PrnParseRowTypes.eType.PCLXLDataType);

                    showElement (bufOffset + 1,
                                 4,
                                 "PCLXL Embedded Len.",
                                 "",
                                 false,
                                 _displayMetricsEmbedWord,
                                 makeOvlShow,
                                 PrnParseRowTypes.eType.PCLXLDataValue);
                }

                //------------------------------------------------------------//
                //                                                            //
                // Adjust pointers to start of embedded data value.           //
                // Then check whether the embedded data is all contained in   //
                // the current buffer.                                        //
                //                                                            //
                //------------------------------------------------------------//

                bufOffset = bufOffset + (dataLenSize + 1);
                bufRem = bufRem - (dataLenSize + 1);

                if (_embedDataRem < 0)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Invalid data length.                                   //
                    //                                                        //
                    //--------------------------------------------------------//

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgWarning,
                        _table,
                        PrnParseConstants.eOvlShow.Illegal,
                        "",
                        "*** Warning ***",
                        "",
                        "Invalid data length value");

                    _embedDataLen = 0;
                    _embedDataRem = 0;
                }
                else if (_embedDataRem > bufRem)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Embedded data is not all contained in the current      //
                    // buffer.                                                //
                    // Set markers for continuation action.                   //
                    //                                                        //
                    //--------------------------------------------------------//

                    _continuation = true;

                    _embedDataRem = _embedDataRem - bufRem;
                    _embedDataLen = bufRem;

                    contType = PrnParseConstants.eContType.PCLXLEmbed;

                    _linkData.setContinuation (contType);
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Embedded data is all contained in the current buffer.  //
                    //                                                        //
                    //--------------------------------------------------------//

                    _embedDataLen = _embedDataRem;
                    _embedDataRem = 0;
                }

                //------------------------------------------------------------//
                //                                                            //
                // First check whether we need to store this embedded data    //
                // for later (embedded) analysis.                             //
                // Then display (first or only part of) embedded data as a    //
                // binary array.                                              //
                //                                                            //
                //------------------------------------------------------------//

                if (_crntOperEmbedType != PCLXLOperators.eEmbedDataType.None)
                {
                    if (((_crntOperEmbedType ==
                            PCLXLOperators.eEmbedDataType.PassThrough) &&
                         (_analysePassThrough))
                                                 ||
                        ((_crntOperEmbedType ==
                            PCLXLOperators.eEmbedDataType.Stream) &&
                         (_analyseStreams))
                                                 ||
                        ((_crntOperEmbedType ==
                            PCLXLOperators.eEmbedDataType.FontHeader) &&
                         (_analyseFontHddr))
                                                 ||
                        ((_crntOperEmbedType ==
                            PCLXLOperators.eEmbedDataType.FontChar) &&
                         (_analyseFontChar)))
                    {
                        _analysisOwner.embeddedDataStore (_buf,
                                                          bufOffset,
                                                          _embedDataLen);
                    }
                }

                PrnParseData.processBinary (
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _buf,
                    _fileOffset,
                    bufOffset,
                    _embedDataLen,
                    "               Data",
                    _showBinData,
                    true,
                    true,
                    _indxOffsetFormat,
                    _analysisLevel);

                bufOffset = bufOffset + _embedDataLen;
                bufRem = bufRem - _embedDataLen;
            }

            return invalidSeqFound;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s H e a d e r                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Format of the stream header is:                                    //
        //                                                                    //
        // byte 0         Binding Format Identifier                           //
        //                ' (0x27) = ASCII (doubt this is allowed)            //
        //                ( (0x28) = binary; high byte first                  //
        //                ) (0x29) = binary; low byte first                   //
        //      1         Reserved: should be space (0x20)                    //
        //      2   -> n  Stream Descriptor String                            //
        //      n+1 ....  Start of PCLXL stream body                          //
        //                                                                    //
        // The Stream Descriptor String is an ASCII string, terminated by a   //
        // LineFeed (0x0a) character. The string should contain at least      //
        // three fields                                                       //
        // (separated by semi-colon (0x3b) characters); only the first three  //
        // fields are mandatory:                                              //
        //                                                                    //
        // field  1        Stream Class Name        (HP-PCL XL)               //
        // field  2        Protocol Class Number    (latest = 3)              //
        // field  3        Protocol Class Revision  (         0)              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean processHeader (ref Int32 bufRem,
                                       ref Int32 bufOffset)
        {
            Boolean invalidSeqFound = false;

            PrnParseConstants.eContType contType;

            PrnParseConstants.eOvlShow makeOvlShow;

            Byte crntByte;

            Int32 hddrLen = 0,
                  termPos = 0;

            for (Int32 i = 0; i < bufRem; i++)
            {
                crntByte = _buf[bufOffset + i];

                if (crntByte == PrnParseConstants.asciiLF)
                {
                    termPos = i;
                    i = bufRem; // force end of loop
                }
            }

            if (termPos == 0)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Line feed terminator character not found.                  //
                // Initiate continuation action.                              //
                //                                                            //
                //------------------------------------------------------------//

                if (bufRem > 200)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Terminator not found within expected maximum length.   //
                    //                                                        //
                    //--------------------------------------------------------//

                    invalidSeqFound = true;

                    PrnParseCommon.addDataRow (
                        PrnParseRowTypes.eType.MsgWarning,
                         _table,
                         PrnParseConstants.eOvlShow.Illegal,
                         _indxOffsetFormat,
                         _fileOffset + bufOffset,
                         _analysisLevel,
                         "*** Warning ***",
                         "",
                         "Invalid data length value");

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgWarning,
                         _table,
                         PrnParseConstants.eOvlShow.Illegal,
                         "",
                         "*** Warning ***",
                         "",
                         "Header terminator not found within 200 bytes");

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgWarning,
                         _table,
                         PrnParseConstants.eOvlShow.Illegal,
                         "",
                         "*** Warning ***",
                         "",
                         "Assume fragment with no header");

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgWarning,
                         _table,
                         PrnParseConstants.eOvlShow.Illegal,
                         "",
                         "*** Warning ***",
                         "",
                         "Assume binding is binary (low-byte first)");

                    _bindType = PrnParseConstants.ePCLXLBinding.BinaryLSFirst;

                    _hddrRead = true;
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Initiate continuation action.                          //
                    //                                                        //
                    //--------------------------------------------------------//

                    _continuation = true;

                    contType = PrnParseConstants.eContType.PCLXL;

                    _linkData.setBacktrack (contType, - bufRem);
                }
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // Linefeed terminator character found.                       //
                //                                                            //
                //------------------------------------------------------------//

                _hddrRead = true;
                hddrLen = termPos + 1;  // include <LF> byte

                if (hddrLen < 9)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Header too small.                                      //
                    // Must consist of at least 9 bytes:                      //
                    //    Binding Format Identifier;                          //
                    //    Reserved byte;                                      //
                    //    Descriptor String, as minimum three fields(each     //
                    //    minimum two bytes including terminators);           //
                    //    Linefeed terminator character.                      //
                    //                                                        //
                    //--------------------------------------------------------//

                    invalidSeqFound = true;

                    PrnParseCommon.addDataRow (
                        PrnParseRowTypes.eType.MsgWarning,
                         _table,
                         PrnParseConstants.eOvlShow.Illegal,
                         _indxOffsetFormat,
                         _fileOffset + bufOffset,
                         _analysisLevel,
                         "*** Warning ***",
                         "",
                         "Header too small");
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Display Stream Header details.                         //
                    //    -  Binding Format Identifier                        //
                    //    -  Reserved byte                                    //
                    //    -  Stream Descriptor String                         //
                    //                                                        //
                    //--------------------------------------------------------//

                    String bindText;

                    if (_buf[bufOffset] == 0x27)
                    {
                        _bindType =
                            PrnParseConstants.ePCLXLBinding.ASCII;
                        bindText = "' = ASCII (not supported by analyser)";

                        invalidSeqFound = true;
                    }
                    else if (_buf[bufOffset] == 0x28)
                    {
                        _bindType =
                            PrnParseConstants.ePCLXLBinding.BinaryMSFirst;
                        bindText = "( = binary: most significant byte first";

                        if (_parseType == PrnParse.eParseType.MakeOverlay)
                        {
                            invalidSeqFound = true;
                        }
                    }
                    else if (_buf[bufOffset] == 0x29)
                    {
                        _bindType =
                            PrnParseConstants.ePCLXLBinding.BinaryLSFirst;
                        bindText = ") = binary: least significant byte first";
                    }
                    else
                    {
                        _bindType =
                            PrnParseConstants.ePCLXLBinding.Unknown;
                        bindText = "*** unknown ***";

                        invalidSeqFound = true;
                    }

                    if (!_verboseMode && !invalidSeqFound)
                    {
                        showElement (bufOffset,
                                     hddrLen,
                                     "PCLXL Stream Header",
                                     "",
                                     false,
                                     _displayMetricsHddr,
                                     PrnParseConstants.eOvlShow.None,
                                     PrnParseRowTypes.eType.PCLXLStreamHddr);
                    }
                    else
                    {
                        if (invalidSeqFound)
                            makeOvlShow = PrnParseConstants.eOvlShow.Illegal;
                        else
                            makeOvlShow = PrnParseConstants.eOvlShow.None;

                        PrnParseCommon.addDataRow (
                            PrnParseRowTypes.eType.PCLXLStreamHddr,
                             _table,
                             PrnParseConstants.eOvlShow.None,
                             _indxOffsetFormat,
                             _fileOffset + bufOffset,
                             _analysisLevel,
                             "PCLXL Stream Header",
                             "",
                             "");

                        showElement (bufOffset,
                                     1,
                                     "      Binding",
                                     bindText,
                                     true,
                                     _displayMetricsNil,
                                     makeOvlShow,
                                     PrnParseRowTypes.eType.PCLXLStreamHddr);

                        showElement (bufOffset + 1,
                                     1,
                                     "      Reserved",
                                     "",
                                     true,
                                     _displayMetricsNil,
                                     PrnParseConstants.eOvlShow.None,
                                     PrnParseRowTypes.eType.PCLXLStreamHddr);

                        showElement (bufOffset + 2,
                                     hddrLen - 2,
                                     "      Descriptor",
                                     "",
                                     false,
                                     _displayMetricsHddr,
                                     PrnParseConstants.eOvlShow.None,
                                     PrnParseRowTypes.eType.PCLXLStreamHddr);
                    }

                    if ((_bindType == PrnParseConstants.ePCLXLBinding.Unknown)
                                              ||
                        (_bindType == PrnParseConstants.ePCLXLBinding.ASCII))
                    {
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.MsgComment,
                             _table,
                             PrnParseConstants.eOvlShow.Illegal,
                             "",
                             "Comment",
                             "",
                             "Assume binding is binary (low-byte first)");

                        _bindType =
                            PrnParseConstants.ePCLXLBinding.BinaryLSFirst;
                    }
                }

                bufOffset = bufOffset + hddrLen;
                bufRem = bufRem - hddrLen;
            }

            if (invalidSeqFound)
                _linkData.setContinuation (PrnParseConstants.eContType.Abort);

            return invalidSeqFound;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s O p e r a t o r T a g                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Process Operator tag.                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processOperatorTag (ref Int32 bufRem,
                                         ref Int32 bufOffset)
        {
            Boolean seqKnown,
                    dummyBool = false,
                    endSession = false;

            Int32 operPos = bufOffset;

            String desc = "";

            PrnParseConstants.eOvlAct operOvlAct =
                PrnParseConstants.eOvlAct.None;

            //----------------------------------------------------------------//
            //                                                                //
            // Initialise.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            _operIDFound = true;
            _crntOperID = _buf[bufOffset];

            _prevOperEmbedType = _crntOperEmbedType;

            //----------------------------------------------------------------//
            //                                                                //
            // Obtain description and flags from Operator tag table (or use   //
            // 'Unknown' entry).                                              //
            //                                                                //
            //----------------------------------------------------------------//

            seqKnown = PCLXLOperators.checkTag (_crntOperID,
                                                ref endSession,
                                                ref dummyBool,
                                                ref _crntOperEmbedType,
                                                ref operOvlAct,
                                                ref desc);

            if (endSession)
                _streamActive = false;

            if (_prevOperEmbedType != PCLXLOperators.eEmbedDataType.None)
            {
                //------------------------------------------------------------//
                //                                                            //
                // The previous operator was of a type (e.g. PCLPassThrough,  //
                // ReadFontHeader, ReadChar) which is followed by embedded    //
                // data.                                                      //
                // Process that embedded data.                                //
                //                                                            //
                //------------------------------------------------------------//

                processStoredEmbeddedData (false);
            }

            if (_operDataStarted)
            {
                //------------------------------------------------------------//
                //                                                            //
                // This operator has a (preceding) Attribute List, and this   //
                // is the first time we've encountered the Operator related   //
                // to that list.                                              //
                //                                                            //
                // Revert to the stored start point to display the attribute  //
                // list (now that we know the operator identity).             //
                //                                                            //
                // This is relevant to the cases where an attribute           //
                // enumeration is dependent on the operator identifier as     //
                // well as the attribute identifier (although we process all  //
                // lists/operators in this way now anyway, for other reasons).//
                //                                                            //
                //------------------------------------------------------------//

                int tempLen;

                _operDataStarted = false;
                _attrDataStarted = false;
                _attrIDFound = false;

                tempLen = bufOffset - _operDataStart;
                bufRem = bufRem + tempLen;
                bufOffset = _operDataStart;

                if (_parseType == PrnParse.eParseType.MakeOverlay)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Set any 'Make Overlay' action required that will       //
                    // affect how the preceding Attribute List will be        //
                    // processed.                                             //
                    //                                                        //
                    //--------------------------------------------------------//

                    PrnParseMakeOvl.checkActionPCLXLOper (
                        true,
                        true,
                        operOvlAct,
                        _operDataStart,
                        operPos,
                        _fileOffset,
                        _linkData,
                        _table,
                        _indxOffsetFormat);

                    _operOvlShow = _linkData.MakeOvlShow;
                }
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // Either we've displayed the attribute list preceding this   //
                // operator, or the operator (e.g. EndChar) has no attribute  //
                // list.                                                      //
                //                                                            //
                //------------------------------------------------------------//

                if (_parseType == PrnParse.eParseType.MakeOverlay)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Check if 'Make Overlay' action required.               //
                    //                                                        //
                    //--------------------------------------------------------//

                    Boolean operHasAttrList;

                    if (bufOffset == _attrDataStart)
                        operHasAttrList = false;
                    else
                        operHasAttrList = true;

                    _breakpoint = PrnParseMakeOvl.checkActionPCLXLOper (
                                    false,
                                    operHasAttrList, 
                                    operOvlAct,
                                    _operDataStart,
                                    operPos,
                                    _fileOffset,
                                    _linkData,
                                    _table,
                                    _indxOffsetFormat);

                    _operOvlShow = _linkData.MakeOvlShow;
                }

                //------------------------------------------------------------//
                //                                                            //
                // Display details of the Operator tag.                       //
                //                                                            //
                //------------------------------------------------------------//

                if ((_parseType == PrnParse.eParseType.MakeOverlay) &&
                    (operOvlAct == PrnParseConstants.eOvlAct.Replace_0x77))
                {
                    String descText;

                    showElement (bufOffset,
                                 1,
                                 "PCLXL Operator",
                                 desc,
                                 true,
                                 _displayMetricsNil,
                                 PrnParseConstants.eOvlShow.Remove,
                                 PrnParseRowTypes.eType.PCLXLOperator);

                    if (_linkData.MakeOvlEncapsulate)
                        descText = "SetPageScale (encapsulated within" +
                                   " ReadStream structure)";
                    else
                        descText = "SetPageScale";
 
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLXLOperator,
                         _table,
                         PrnParseConstants.eOvlShow.Insert,
                         "",
                         "PCLXL Operator",
                         "0x77",
                         descText);
                }
                else
                {
                    showElement (bufOffset,
                                 1,
                                 "PCLXL Operator",
                                 desc,
                                 true,
                                 _displayMetricsNil,
                                 _operOvlShow,
                                 PrnParseRowTypes.eType.PCLXLOperator);
                }

                //------------------------------------------------------------//
                //                                                            //
                // Display Operator tag position (if option set).             //
                //                                                            //
                //------------------------------------------------------------//

                _operNum = _operNum + 1;

                if (_showOperPos)
                {
                    String text;

                    if (_analysisLevel == 0)
                    {
                        text = _operNum.ToString ();
                    }
                    else
                    {
                        text = _operNum.ToString () +
                               " (within embedded " +
                               _crntEmbedType.ToString () + ")";
                          //   " at level " +_analysisLevel.ToString ();
                    }

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgComment,
                         _table,
                         PrnParseConstants.eOvlShow.None,
                         "",
                         "               No.",
                         "",
                         text);
                }

                //------------------------------------------------------------//
                //                                                            //
                // Update statistics.                                         //
                //                                                            //
                //------------------------------------------------------------//

                PCLXLOperators.incrementStatsCount (_crntOperID,
                                                    _analysisLevel);

                //------------------------------------------------------------//
                //                                                            //
                // Adjust pointers & flags.                                   //
                //                                                            //
                //------------------------------------------------------------//

                _operIDFound = false;
                _operDataStarted = false;
                _attrDataStarted = false;
                _attrDataStart = bufOffset + 1;
                _operDataStart = bufOffset + 1;

                bufOffset = bufOffset + 1;
                bufRem = bufRem - 1;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // P r o c e s s S t o r e d E m b e d d e d D a t a                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The previous operator was of a type (e.g. PCLPassThrough,          //
        // ReadFontHeader, ReadChar) which is followed by embedded data.      //
        //                                                                    //
        // Determine if the embedded data collected so far:                   //
        //  - Should be processed immediately, now that we've encountered     //
        //    another operator tag (usually preceded by an attribute list),   //
        //    regardless of whether the new operator tag is the same as the   //
        //    previous one or not.                                            //
        //    This applies, for example, to the ReadChar operator.            //
        //  or:                                                               //
        //  - Should be accumulated, until an operator of a different type is //
        //    encountered, before being analysed, as several successive       //
        //    operators of the same type may be needed to describe the data.  //
        //    This applies, for example, to the PCLPassThrough and            //
        //    ReadFontHeader operators.                                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void processStoredEmbeddedData(Boolean endReached)
        {
            if (endReached)
            {
                _prevOperEmbedType = _crntOperEmbedType;
                _crntOperEmbedType = PCLXLOperators.eEmbedDataType.None;
            }

            if (_prevOperEmbedType == _crntOperEmbedType)
            {
                if ((_prevOperEmbedType ==
                    PCLXLOperators.eEmbedDataType.FontChar) &&
                    (_analyseFontChar))
                {
                    _analysisOwner.embeddedPCLXLAnalyse (
                        ToolCommonData.ePrintLang.PCLXL,
                        PCLXLOperators.eEmbedDataType.FontChar);
                }
            }
            else
            {
                if ((_prevOperEmbedType ==
                    PCLXLOperators.eEmbedDataType.PassThrough) &&
                    (_analysePassThrough))
                {
                    _analysisOwner.embeddedPCLXLAnalyse (
                        ToolCommonData.ePrintLang.PCL,
                        PCLXLOperators.eEmbedDataType.PassThrough);
                }
                else if ((_prevOperEmbedType ==
                            PCLXLOperators.eEmbedDataType.Stream) &&
                    (_analyseStreams))
                {
                    _analysisOwner.embeddedPCLXLAnalyse (
                        ToolCommonData.ePrintLang.PCLXL,
                        PCLXLOperators.eEmbedDataType.Stream);
                }
                else if ((_prevOperEmbedType ==
                            PCLXLOperators.eEmbedDataType.FontHeader) &&
                    (_analyseFontHddr))
                {
                    _analysisOwner.embeddedPCLXLAnalyse (
                        ToolCommonData.ePrintLang.PCLXL,
                        PCLXLOperators.eEmbedDataType.FontHeader);
                }
                else if ((_prevOperEmbedType ==
                    PCLXLOperators.eEmbedDataType.FontChar) &&
                    (_analyseFontChar))
                {
                    _analysisOwner.embeddedPCLXLAnalyse (
                        ToolCommonData.ePrintLang.PCLXL,
                        PCLXLOperators.eEmbedDataType.FontChar);
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s W h i t e S p a c e T a g                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Process ...                                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processWhiteSpaceTag (ref Int32 bufRem,
                                           ref Int32 bufOffset)
        {
            Byte crntByte;

            String desc = "",
                   mnemonic = "";

            Boolean seqKnown;

            //----------------------------------------------------------------//
            //                                                                //
            // Initialise.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            crntByte = _buf[bufOffset];

            //----------------------------------------------------------------//
            //                                                                //
            // Obtain description from WhiteSpace tag table.                  //
            //                                                                //
            //----------------------------------------------------------------//

            seqKnown = PCLXLWhitespaces.checkTag (crntByte,
                                                  ref mnemonic, 
                                                  ref desc);
            if (seqKnown)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Update statistics.                                         //
                //                                                            //
                //------------------------------------------------------------//

                PCLXLWhitespaces.incrementStatsCount (crntByte,
                                                      _analysisLevel);

                //------------------------------------------------------------//
                //                                                            //
                // Display details of the WhiteSpace tag.                     //
                //                                                            //
                //------------------------------------------------------------//

                showElement (bufOffset,
                             1,
                             "PCLXL Whitespace",
                             mnemonic + ": " + desc,
                             true,
                             _displayMetricsNil,
                             _linkData.MakeOvlShow,
                             PrnParseRowTypes.eType.PCLXLWhiteSpace);

                //------------------------------------------------------------//
                //                                                            //
                // Adjust pointers & flags.                                   //
                //                                                            //
                //------------------------------------------------------------//

                bufOffset = bufOffset + 1;
                bufRem = bufRem - 1;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s h o w E l e m e n t                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Display details of current element                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void showElement (Int32 bufOffset,
                                  Int32 dataLen,
                                  String typeText,
                                  String descText,
                                  Boolean useDesc,
                                  PrnParsePCLXLElementMetrics metrics,
                                  PrnParseConstants.eOvlShow makeOvlShow,
                                  PrnParseRowTypes.eType rowType)
        {
            PCLXLDataTypes.eBaseType baseType =
                PCLXLDataTypes.eBaseType.Unknown;

            Int32 sliceLen,
                  chunkIpLen,
                  chunkOpLen,
                  chunkOffset,
                  sliceOffset,
                  groupSize = 0,
                  unitSize = 0,
                  decodeIndent = 0,
                  decodeMax = 0,
                  ipPtr;

            Boolean firstLine = false,
                    firstSlice = false,
                    lastSlice = false,
                    chunkComplete = false,
                    deferItem = false,
                    arrayType = false,
                    treatUbyteAsAscii = false,
                    treatUint16AsUnicode = false,
                    stringAscii = false,
                    stringUnicode = false,
                    seqError = false;

            String seq = "",
                   decode = "";

            StringBuilder chunkOp = new StringBuilder ();

            //----------------------------------------------------------------//
            //                                                                //
            // Initialise.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            firstLine = true;
            firstSlice = true;
            lastSlice = false;
            chunkComplete = false;
            stringAscii = false;
            stringUnicode = false;
            seqError = false;

            sliceOffset = bufOffset;

            if (useDesc)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Interpretation text supplied.                              //
                //                                                            //
                //------------------------------------------------------------//

                groupSize = 1;
                unitSize = 1;
                sliceLen = _decodeSliceMax;
                arrayType = false;
                baseType = PCLXLDataTypes.eBaseType.Unknown;
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // Interpretation text not supplied.                          //
                // Use metrics data to determine how to process sequence.     //
                //                                                            //
                //------------------------------------------------------------//

                metrics.getData (ref treatUbyteAsAscii,
                                 ref treatUint16AsUnicode,
                                 ref arrayType,
                                 ref decodeIndent,
                                 ref groupSize,
                                 ref unitSize,
                                 ref baseType);

                sliceLen = unitSize;
                decodeMax = _decodeAreaMax - decodeIndent;

                if ((baseType == PCLXLDataTypes.eBaseType.Ubyte) &&
                    (treatUbyteAsAscii))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // ubyte (single or array) to be treated as ASCII         //
                    // character(s).                                          //
                    //                                                        //
                    //--------------------------------------------------------//

                    stringAscii = true;
                    sliceLen = _decodeSliceMax;
                }
                else if ((baseType == PCLXLDataTypes.eBaseType.Uint16) &&
                    (treatUint16AsUnicode))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // uint16 (single or array) to be treated as Unicode      //
                    // UCS-2 character(s).                                    //
                    //                                                        //
                    //--------------------------------------------------------//

                    stringUnicode = true;
                    sliceLen = _decodeSliceMax;
                }
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Loop round, interpreting the data slice by slice.              //
            //                                                                //
            // The input data is examined in 'slices' (of a size suitable for //
            // details to be shown in the Seq column of the display window),  //
            // and is output in 'chunks' (of a size suitable for details to   //
            // be shown in the Desc/Interpretation column).                   //
            //                                                                //
            //----------------------------------------------------------------//

            ipPtr = 0;
            chunkIpLen = 0;
            chunkOffset = sliceOffset;
            chunkOpLen = 0;

            while (ipPtr < dataLen)
            {
                decode = "";
                deferItem = false;

                //------------------------------------------------------------//
                //                                                            //
                // Loop step 1:                                               //
                // Select next input slice.                                   //
                //                                                            //
                //------------------------------------------------------------//

                if ((ipPtr + sliceLen) >= dataLen)
                {
                    sliceLen = dataLen - ipPtr;
                    lastSlice = true;
                }

                //------------------------------------------------------------//
                //                                                            //
                // Loop step 2:                                               //
                // Process slice according to data type.                      //
                //                                                            //
                //------------------------------------------------------------//

                if (useDesc)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Interpretation text has been supplied.                 //
                    // Output this with the last slice of the sequence.       //
                    //                                                        //
                    //--------------------------------------------------------//

                    chunkIpLen = chunkIpLen + sliceLen;

                    if (lastSlice)
                        decode = descText;
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Process slice according to data type, and return       //
                    // decode.                                                //
                    //                                                        //
                    //--------------------------------------------------------//

                    decode = showElementDecodeData (ref chunkOp,
                                                    ref chunkIpLen,
                                                    ref chunkOpLen,
                                                    ref chunkComplete,
                                                    ref deferItem,
                                                    ref seqError,
                                                    sliceOffset,
                                                    sliceLen,
                                                    decodeIndent,
                                                    chunkOffset,
                                                    firstSlice,
                                                    lastSlice,
                                                    arrayType,
                                                    stringAscii,
                                                    stringUnicode,
                                                    baseType);
                }

                //------------------------------------------------------------//
                //                                                            //
                // Loop step 3:                                               //
                // Convert slice of supplied sequence data to hexadecimal     //
                // notation in the sequence buffer.                           //
                // Do this only when the current chunk is complete, unless    //
                // verbose mode is set, or an error has been detected.        //
                //                                                            //
                //------------------------------------------------------------//

                seq = showElementSeqData (sliceLen,
                                          sliceOffset,
                                          chunkIpLen,
                                          chunkOffset,
                                          lastSlice,
                                          chunkComplete,
                                          seqError);

                //------------------------------------------------------------//
                //                                                            //
                // Loop step 4:                                               //
                // Output details of current slice (if verbose mode or error  //
                //                                  found)                    //
                //                   current chunk (otherwise)                //
                //                                                            //
                //------------------------------------------------------------//

                if (lastSlice)
                {
                    chunkComplete = true;
                }

                if (_verboseMode || seqError || chunkComplete)
                {
                    if (firstLine)
                    {
                        PrnParseCommon.addDataRow (
                            rowType,
                            _table,
                            makeOvlShow,
                            _indxOffsetFormat,
                            _fileOffset + chunkOffset,
                            _analysisLevel,
                            typeText,
                            seq,
                            decode.ToString ());
                    }
                    else
                    {
                        PrnParseCommon.addDataRow (
                            rowType,
                            _table,
                            makeOvlShow,
                            _indxOffsetFormat,
                            _fileOffset + chunkOffset,
                            _analysisLevel,
                            "",
                            seq,
                            decode.ToString ());
                    }

                    firstLine = false;

                    if (chunkComplete)
                    {
                        chunkComplete = false;
                        chunkOffset = chunkOffset + chunkIpLen;
                        chunkIpLen = 0;
                        chunkOpLen = 0;
                        chunkOp.Clear ();
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Loop step 5:                                               //
                // Adjust pointers to next (if any) slice.                    //
                //                                                            //
                //------------------------------------------------------------//

                firstSlice = false;

                if (!deferItem)
                {
                    ipPtr = ipPtr + sliceLen;
                    sliceOffset = sliceOffset + sliceLen;
                }
            }  // end of While loop
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s h o w E l e m e n t D e c o d e D a t a                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Process slice according to data type, and return decode.           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private String showElementDecodeData (
            ref StringBuilder chunkOp,
            ref Int32 chunkIpLen,
            ref Int32 chunkOpLen,
            ref Boolean chunkComplete,
            ref Boolean deferItem,
            ref Boolean seqError,
            Int32 sliceOffset,
            Int32 sliceLen,
            Int32 decodeIndent,
            Int32 chunkOffset,
            Boolean firstSlice,
            Boolean lastSlice,
            Boolean arrayType,
            Boolean stringAscii,
            Boolean stringUnicode,
            PCLXLDataTypes.eBaseType baseType)
        {
            StringBuilder decode  = new StringBuilder ();

            Int32 decodeMax = _decodeAreaMax - decodeIndent;
            
            Int32 chunkOpRem,
                  itemLen = 0;

            if (stringUnicode)
            {
                //------------------------------------------------------------//
                //                                                            //
                // (Array of) uint16 value(s) is to be treated as (an array   //
                // of) Unicode character(s).                                  //
                // Interpret the value(s) and output the resultant            //
                // character(s) within quotes.                                //
                // Take account of byte ordering before translating.          //
                // Unicode values of U+00FF or less translate directly to     //
                // their ISO-8859-1 equivalent.                               //
                //                                                            //
                //------------------------------------------------------------//

                String showChar;
                
                Int32 k;

                for (Int32 j = 0; j < sliceLen; j = j + 2)
                {
                    k = chunkOffset + chunkIpLen + j;

                    showChar = PrnParseData.processBytePair (
                        _buf[k],
                        _buf[k + 1],
                        (_bindType ==
                            PrnParseConstants.ePCLXLBinding.BinaryMSFirst),
                        _indxCharSetSubAct,
                        (Byte) _valCharSetSubCode,
                        _indxCharSetName);

                    chunkOp.Append(showChar);
                }

                chunkIpLen = chunkIpLen + sliceLen;
                chunkOpLen = chunkOp.Length;

                if (_verboseMode || lastSlice ||
                    ((chunkOpLen + 2) >= decodeMax))
                {
                    chunkComplete = true;

                    decode.Clear();

                    if (decodeIndent != 0)
                    {
                        String indent = new String (' ', decodeIndent);

                        decode.Append(indent);
                    }

                    decode.Append('"' + chunkOp.ToString() + '"');
                }
            }
            else if (stringAscii)
            {
                //------------------------------------------------------------//
                //                                                            //
                // (Array of) ubyte value(s) is to be treated as (an array    //
                // of) ASCII character(s).                                    //
                // Interpret the value(s) and output the resultant            //
                // character(s) within quotes.                                //
                //                                                            //
                //------------------------------------------------------------//

                String showChar;

                Int32 k;

                for (Int32 j = 0; j < sliceLen; j++)
                {
                    k = chunkOffset + chunkIpLen + j;
                    
                    showChar = PrnParseData.processByte (
                        _buf[k],
                        _indxCharSetSubAct,
                        (Byte) _valCharSetSubCode,
                        _indxCharSetName);

                    chunkOp.Append(showChar);
                }

                chunkIpLen = chunkIpLen + sliceLen;
                chunkOpLen = chunkOp.Length;

                if (_verboseMode || lastSlice ||
                    ((chunkOpLen + 2) >= decodeMax))
                {
                    chunkComplete = true;

                    decode.Clear ();

                    if (decodeIndent != 0)
                    {
                        String indent = new String (' ', decodeIndent);

                        decode.Append (indent);
                    }

                    decode.Append ('"' + chunkOp.ToString () + '"');
                }
            }
            else if (baseType == PCLXLDataTypes.eBaseType.Ubyte)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Array, group or single ubyte value(s) to be treated as     //
                // numeric value(s).                                          //
                // For arrays or grouped items, separate items with spaces.   //
                //                                                            //
                //------------------------------------------------------------//

                if (arrayType)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Display arrays within () brackets.                     //
                    // Second and subsequent lines of array are indented to   //
                    // to align with first line.                              //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (firstSlice)
                    {
                        chunkOp.Clear ();
                        chunkOp.Append("(");
                        chunkOpLen = 1;
                    }
                    else if (chunkOpLen == 0)
                    {
                        chunkOp.Clear ();
                        chunkOp.Append(" ");
                        chunkOpLen = 1;
                    }
                }

                chunkOpRem = decodeMax - chunkOpLen;

                if (chunkOpRem <= 0)
                {
                    deferItem = true;
                }
                else
                {
                    if (chunkOpLen != 0)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Insert separation character between elements of    //
                        // array or group.                                    //
                        //                                                    //
                        //----------------------------------------------------//

                        chunkOp.Append(" ");
                        chunkOpLen = chunkOpLen + 1;
                        chunkOpRem = chunkOpRem - 1;
                    }

                    if (lastSlice && arrayType)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Reserve space for terminating " )" characters.     //
                        //                                                    //
                        //----------------------------------------------------//

                        chunkOpRem = chunkOpRem - 2;
                    }

                    if (chunkOpRem <= 0)
                    {
                        deferItem = true;
                    }
                    else
                    {
                        Byte b = _buf[sliceOffset];

                        String tempStr = b.ToString ();
                        
                        itemLen = tempStr.Length;

                        if (itemLen > chunkOpRem)
                            deferItem = true;
                        else
                            chunkOp.Append(tempStr);
                    }
                }

                if (deferItem)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Insufficient space in current line.                    //
                    // Output accumulated details so far.                     //
                    // 'Defer' flag will cause current item to be             //
                    // re-processed next time round the loop.                 //
                    //                                                        //
                    //--------------------------------------------------------//

                    decode.Append ("    " + chunkOp);
                    chunkComplete = true;
                }
                else
                {
                    chunkIpLen = chunkIpLen + sliceLen;
                    chunkOpLen = chunkOpLen + itemLen;
                    
                    if (lastSlice && arrayType)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Add in terminating " )" characters.                //
                        //                                                    //
                        //----------------------------------------------------//

                        chunkOp.Append(" )");
                        chunkOpLen = chunkOpLen + 2;
                    }
                    
                    if (_verboseMode || lastSlice)
                    {
                        //-------------------------------------------------------------//
                        //                                                             //
                        // Copy output line from work buffer to target.                //
                        //                                                             //
                        //-------------------------------------------------------------//

                        decode.Clear();

                        if (decodeIndent != 0)
                        {
                            String indent = new String (' ', decodeIndent);

                            decode.Append (indent);
                        }

                        decode.Append (chunkOp);

                        chunkComplete = true;
                    }
                }
            }
            else if ((baseType == PCLXLDataTypes.eBaseType.Uint16)
                                ||
                     (baseType == PCLXLDataTypes.eBaseType.Uint32)
                                ||
                     (baseType == PCLXLDataTypes.eBaseType.Sint16)
                                ||
                     (baseType == PCLXLDataTypes.eBaseType.Sint32)
                                ||
                     (baseType == PCLXLDataTypes.eBaseType.Real32))
            {
                //------------------------------------------------------------//
                //                                                            //
                // Integer or floating-point value.                           //
                // Take account of byte ordering before translating.          //
                //                                                            //
                //------------------------------------------------------------//

                if (arrayType)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Display arrays within () brackets.                     //
                    // Second and subsequent lines of array are indented to   //
                    // to align with first line.                              //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (firstSlice)
                    {
                        chunkOp.Clear ();
                        chunkOp.Append ("(");
                        chunkOpLen = 1;
                    }
                    else if (chunkOpLen == 0)
                    {
                        chunkOp.Clear ();
                        chunkOp.Append (" ");
                        chunkOpLen = 1;
                    }
                }

                chunkOpRem = decodeMax - chunkOpLen;

                if (chunkOpRem <= 0)
                {
                    deferItem = true;
                }
                else
                {
                    if (chunkOpLen != 0)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Insert separation character between elements of    //
                        // array or group.                                    //
                        //                                                    //
                        //----------------------------------------------------//

                        chunkOp.Append(" ");
                        chunkOpLen = chunkOpLen + 1;
                        chunkOpRem = chunkOpRem - 1;
                    }

                    if (lastSlice && arrayType)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Reserve space for terminating " )" characters.     //
                        //                                                    //
                        //----------------------------------------------------//

                        chunkOpRem = chunkOpRem - 2;
                    }

                    if (chunkOpRem <= 0)
                    {
                        deferItem = true;
                    }
                    else if (baseType == PCLXLDataTypes.eBaseType.Real32)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Real (IEEE 32-bit single-precision floating-point) //
                        // value.                                             //
                        //                                                    //
                        // Decode the value as follows:                       //
                        //                                                    //
                        //  - The 4-byte value is converted, byte by byte, to //
                        //    an unsigned integer (taking into account the    //
                        //    byte-ordering specified by the PCL XL binding). //
                        //                                                    //
                        //  - The resultant integer is then converted back to //
                        //    a byte array (using the host byte ordering).    //
                        //                                                    //
                        //  - This new byte array is then converted to a      //
                        //    floating point value.                           //
                        //                                                    //
                        //  - This value is then converted to its string      //
                        //    representation.                                 //
                        //                                                    //
                        //----------------------------------------------------//
                         
                        UInt32 uiSub,
                               uiTot;

                        Byte [] byteArray;

                        Single f;

                        String tempStr;

                        uiTot = 0;

                        if (_bindType ==
                            PrnParseConstants.ePCLXLBinding.BinaryMSFirst)
                        {
                            for (Int32 j=0; j < sliceLen; j++)
                            {
                                uiSub = _buf[sliceOffset + j];
                                uiTot = (uiTot * 256) + uiSub;
                            }
                        }
                        else
                        {
                            for (Int32 j=(sliceLen-1); j >= 0; j--)
                            {
                                uiSub = _buf[sliceOffset + j];
                                uiTot = (uiTot * 256) + uiSub;
                            }
                        }
                        
                        byteArray = BitConverter.GetBytes(uiTot);

                        f = BitConverter.ToSingle(byteArray, 0);

                        tempStr = f.ToString("F6");

                        itemLen = tempStr.Length;

                        if (itemLen > chunkOpRem)
                            deferItem = true;
                        else
                            chunkOp.Append(tempStr);
                    }
                    else if ((baseType == PCLXLDataTypes.eBaseType.Uint16) ||
                             (baseType == PCLXLDataTypes.eBaseType.Uint32))
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Unsigned integer value.                            //
                        //                                                    //
                        // Decode the value as follows:                       //
                        //                                                    //
                        //  - The (2-byte or 4-byte) value is converted, byte //
                        //    by byte, to an unsigned integer (taking into    //
                        //    account the byte-ordering specified by the      //
                        //    PCL XL binding).                                //
                        //                                                    //
                        //  - This integer value is then converted to its     //
                        //    string representation.                          //
                        //                                                    //
                        //----------------------------------------------------//

                        UInt32 uiSub,
                               uiTot;

                        String tempStr;

                        uiTot = 0;

                        if (_bindType ==
                            PrnParseConstants.ePCLXLBinding.BinaryMSFirst)
                        {
                            for (Int32 j = 0; j < sliceLen; j++)
                            {
                                uiSub = _buf[sliceOffset + j];
                                uiTot = (uiTot * 256) + uiSub;
                            }
                        }
                        else
                        {
                            for (Int32 j = (sliceLen - 1); j >= 0; j--)
                            {
                                uiSub = _buf[sliceOffset + j];
                                uiTot = (uiTot * 256) + uiSub;
                            }
                        }

                        tempStr = uiTot.ToString ();

                        itemLen = tempStr.Length;

                        if (itemLen > chunkOpRem)
                            deferItem = true;
                        else
                            chunkOp.Append (tempStr);

                        if (_attrValueIsEmbedLength)
                        {
                            //------------------------------------------------//
                            //                                                //
                            // One of the special attributes which defines    //
                            // the length of (binary) data to follow the      //
                            // Operator tag.                                  //
                            // Used with host-based streams.                  //
                            //                                                //
                            //------------------------------------------------//

                            _attrDataVal = (Int32) uiTot;
                        }
                    }
                    else
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Signed integer value.                              //
                        //                                                    //
                        // Decode the value as follows:                       //
                        //                                                    //
                        //  - The (2-byte or 4-byte) value is converted, byte //
                        //    by byte, to a signed integer (taking into       //
                        //    account the byte-ordering specified by the      //
                        //    PCL XL binding).                                //
                        //                                                    //
                        //  - This integer value is then converted to its     //
                        //    string representation.                          //
                        //                                                    //
                        //----------------------------------------------------//

                        Int32 iSub,
                              iTot;

                        Boolean msByte;
                        
                        String tempStr;

                        iTot   = 0;
                        msByte = true;

                        if (_bindType ==
                            PrnParseConstants.ePCLXLBinding.BinaryMSFirst)
                        {
                            for (Int32 j = 0; j < sliceLen; j++)
                            {
                                iSub = _buf[sliceOffset + j];

                                if (msByte && (iSub > 0x80))
                                    iTot = iSub - 256;
                                else
                                    iTot = (iTot * 256) + iSub;

                                msByte = false;
                            }
                        }
                        else
                        {
                            for (Int32 j = (sliceLen - 1); j >= 0; j--)
                            {
                                iSub = _buf[sliceOffset + j];

                                if (msByte && (iSub > 0x80))
                                    iTot = iSub - 256;
                                else
                                    iTot = (iTot * 256) + iSub;

                                msByte = false;
                            }
                        }

                        tempStr = iTot.ToString();
                        itemLen = tempStr.Length;

                        if (itemLen > chunkOpRem)
                            deferItem = true;
                        else
                            chunkOp.Append (tempStr);

                        if (_attrValueIsEmbedLength)
                        {
                            //------------------------------------------------//
                            //                                                //
                            // One of the special attributes which defines    //
                            // the length of (binary) data to follow the      //
                            // Operator tag.                                  //
                            // Used with host-based streams.                  //
                            //                                                //
                            //------------------------------------------------//

                            _attrDataVal = (Int32) iTot;
                        }
                    }
                }

                if (deferItem)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Insufficient space in current line.                    //
                    // Output accumulated details so far.                     //
                    // 'Defer' flag will cause current item to be             //
                    // re-processed next time round the loop.                 //
                    //                                                        //
                    //--------------------------------------------------------//

                    decode.Clear ();

                    if (decodeIndent != 0)
                    {
                        String indent = new String (' ', decodeIndent);

                        decode.Append (indent);
                    }

                    decode.Append (chunkOp);

                    chunkComplete = true;
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Interpretation of current item has already been added  //
                    // to output work area.                                   //
                    //                                                        //
                    //--------------------------------------------------------//

                    chunkIpLen = chunkIpLen + sliceLen;
                    chunkOpLen = chunkOpLen + itemLen;

                    if (lastSlice && arrayType)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Add in terminating " )" characters.                //
                        //                                                    //
                        //----------------------------------------------------//

                        chunkOp.Append(" )");
                        chunkOpLen = chunkOpLen + 2;
                    }

                    if (_verboseMode || lastSlice)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Copy output line from work buffer to target.       //
                        //                                                    //
                        //----------------------------------------------------//

                        decode.Clear ();

                        if (decodeIndent != 0)
                        {
                            String indent = new String (' ', decodeIndent);

                            decode.Append (indent);
                        }

                        decode.Append (chunkOp);

                        chunkComplete = true;
                    }
                }
            }
            else
            {
                seqError = true;

                decode.Clear ();
                decode.Append("*** unknown type ***");

                chunkComplete = true;
            }

            return decode.ToString();
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

        private String showElementSeqData (Int32 sliceLen,
                                           Int32 sliceOffset,
                                           Int32 chunkIpLen,
                                           Int32 chunkOffset,
                                           Boolean lastSlice,
                                           Boolean chunkComplete,
                                           Boolean seqError)
        {
            StringBuilder seq = new StringBuilder();

            Byte crntByte;

            Int32 hexPtr,
                  hexStart = 0,
                  hexEnd = 0,
                  sub;

            Boolean useEllipsis,
                    displaySlice;

            Char [] hexBuf = new Char [(_decodeSliceMax * 2) + 1];

            useEllipsis  = false;
            displaySlice = false;
            seq.Clear();

            if (_verboseMode || seqError)
            {
                //-------------------------------------------------------------------//
                //                                                                   //
                // Convert current slice.                                            //
                //                                                                   //
                //-------------------------------------------------------------------//

                displaySlice = true;
                hexStart = sliceOffset;
                hexEnd   = sliceOffset + sliceLen;
            }
            else if ((!_verboseMode) && (chunkComplete || lastSlice))
            {
                //-------------------------------------------------------------------//
                //                                                                   //
                // Convert first few characters of current chunk.                    //
                //                                                                   //
                //-------------------------------------------------------------------//

                displaySlice = true;

                if (chunkIpLen > _decodeSliceMax)
                {
                    hexStart = chunkOffset;
                    hexEnd = chunkOffset + _decodeSliceMax - 1;
                    useEllipsis = true;
                }
                else
                {
                    hexStart = chunkOffset;
                    hexEnd   = chunkOffset + chunkIpLen;
                }
            }

            if (displaySlice)
            {
                hexPtr = 0;

                for (Int32 j=hexStart; j<hexEnd; j++)
                {
                    sub = (_buf[j]);
                    sub = sub >> 4;
                    crntByte = PrnParseConstants.cHexBytes[sub];
                    hexBuf[hexPtr++] = (Char)crntByte;

                    sub = (_buf[j] & 0x0f);
                    crntByte = PrnParseConstants.cHexBytes[sub];
                //    hexBuf[hexPtr++] = crntByte;
                    hexBuf[hexPtr++] = (Char)crntByte;
                }

         //       hexBuf[hexPtr] = 0x00;
         //       hexBuf2[hexPtr] = (Char) 0x00;

                seq.Append("0x");
                seq.Append(hexBuf, 0, hexPtr);

                if (useEllipsis)
                    seq.Append("..");
            }
            
            return seq.ToString();
        }
    }
}