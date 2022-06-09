using System;
using System.Data;
using System.Text;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines functions to parse PCL escape sequences.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    class PrnParsePCL
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private PrnParseLinkData _linkData;
        
        private PrnParseOptions _options;

        private PrnParse.eParseType _parseType;

        private PrnParseHPGL2 _parseHPGL2;
 
        private PrnParseFontHddrPCL _parseFontHddrPCL;
        private PrnParseFontCharPCL _parseFontCharPCL;
        private PrnParsePCLBinary   _parsePCLBinary;

        private DataTable _table;

        private Byte[] _buf;

        private Int32 _fileOffset;
        private Int32 _endOffset;

        private PrnParseConstants.eOptOffsetFormats _indxOffsetFormat;

        private PrnParseConstants.eOptCharSetSubActs _indxCharSetSubAct;
        private PrnParseConstants.eOptCharSets _indxCharSetName;
        private Int32 _valCharSetSubCode;

        private Int32 _textParsingMethod;
        private Int32 _analysisLevel = 0;
        private Int32 _macroLevel    = 0;

        private Boolean _analyseFontHddr;
        private Boolean _analyseFontChar;
        private Boolean _interpretStyle;

        private Boolean _transAlphaNumId;
        private Boolean _transColourLookup;
        private Boolean _transConfIO;
        private Boolean _transConfImageData;
        private Boolean _transConfRasterData;
        private Boolean _transDefLogPage;
        private Boolean _transDefSymSet;
        private Boolean _transDitherMatrix;
        private Boolean _transDriverConf;
        private Boolean _transEscEncText;
        private Boolean _transPaletteConf;
        private Boolean _transUserPattern;
        private Boolean _transViewIlluminant;

        private Boolean _showBinData;
        private Boolean _showMacroData;

        private Boolean _analysePML;

        private ASCIIEncoding _ascii = new ASCIIEncoding ();
        
        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P r n P a r s e P C L                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PrnParsePCL(PrnParse.eParseType parseType,
                           PrnParseHPGL2       parseHPGL2)
        {
            _parseType  = parseType;
            _parseHPGL2 = parseHPGL2;

            _textParsingMethod =
                (Int32) PCLTextParsingMethods.ePCLVal.m0_1_byte_default;

            _parseFontHddrPCL = new PrnParseFontHddrPCL ();
            _parseFontCharPCL = new PrnParseFontCharPCL ();
            _parsePCLBinary   = new PrnParsePCLBinary(_parseFontHddrPCL);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a r s e B u f f e r                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Parse provided buffer, assuming that the current print language is //
        // PCL.                                                               //
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

            //-------------------------------------------------------------------------//
            //                                                                         //
            // Initialise.                                                             //
            //                                                                         //
            //-------------------------------------------------------------------------//

            _buf        = buf;
            _linkData   = linkData;
            _options    = options;
            _table      = table;
            _fileOffset = fileOffset;

            _analysisLevel = linkData.AnalysisLevel;

            seqInvalid = false;

            //----------------------------------------------------------------//

            _indxOffsetFormat = options.IndxGenOffsetFormat;

            _options.getOptCharSet (ref _indxCharSetName,
                                    ref _indxCharSetSubAct,
                                    ref _valCharSetSubCode);

            _options.getOptPCLBasic (ref _analyseFontHddr,
                                     ref _analyseFontChar,
                                     ref _showMacroData,
                                     ref _interpretStyle,
                                     ref _showBinData,
                                     ref _transAlphaNumId,
                                     ref _transColourLookup,
                                     ref _transConfIO,
                                     ref _transConfImageData,
                                     ref _transConfRasterData,
                                     ref _transDefLogPage,
                                     ref _transDefSymSet,
                                     ref _transDitherMatrix,
                                     ref _transDriverConf,
                                     ref _transEscEncText,
                                     ref _transPaletteConf,
                                     ref _transUserPattern,
                                     ref _transViewIlluminant);

            _endOffset = _options.ValCurFOffsetEnd;

            _analysePML = _options.FlagPMLWithinPCL;

            //----------------------------------------------------------------//

            if (linkData.isContinuation())
                seqInvalid = parseContinuation (ref bufRem,
                                                ref bufOffset,
                                                ref crntPDL,
                                                ref endReached); 
            else
                seqInvalid = parseSequence (ref bufRem,
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

            Int32 prefixLen = 0,
                  contDataLen = 0,
                  downloadRem = 0,
                  binDataLen;

            Boolean hddrOK,
                    charOK,
                    dataOK,
                    badSeq = false,
                    continuation = false,
                    breakpoint = false,
                    backTrack = false,
                    dummyBool = false;

            Boolean invalidSeqFound = false;

            Byte prefixA = 0x00,
                 prefixB = 0x00;
            
            contType = PrnParseConstants.eContType.None;
            
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
                _linkData.resetPCLComboData ();
            }
            else if ((contType == PrnParseConstants.eContType.PCLDownload)
                               ||
                (contType == PrnParseConstants.eContType.PCLDownloadCombo))
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended with an escape sequence with        //
                // following (usually binary) data download which is not      //
                // being specifically decoded.                                //
                // But the whole sequence was not completely contained in     //
                // that block.                                                //
                // Output the remaining 'download' characters (or the whole   //
                // buffer and initiate another continuation) before           //
                // continuing with the analysis.                              //
                //                                                            //
                //------------------------------------------------------------//

                if (downloadRem > bufRem)
                {
                    binDataLen = bufRem;
                    downloadRem = downloadRem - bufRem;
                }
                else
                {
                    binDataLen = downloadRem;
                    downloadRem = 0;
                }

                //------------------------------------------------------------//
                //                                                            //
                // Some, or all, of the download data is contained within the //
                // current 'block'.                                           //
                //                                                            //
                //------------------------------------------------------------//

                PrnParseData.processBinary (
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _buf,
                    _fileOffset,
                    bufOffset,
                    binDataLen,
                    "PCL Binary",
                    _showBinData,
                    false,
                    true,
                    _indxOffsetFormat,
                    _analysisLevel);
                
                //------------------------------------------------------------//
                //                                                            //
                // Adjust continuation data and pointers.                     //
                //                                                            //
                //------------------------------------------------------------//

                if (downloadRem == 0)
                {
                    if (contType == PrnParseConstants.eContType.PCLDownloadCombo)
                    {
                        _linkData.setContData (
                            PrnParseConstants.eContType.PCLComplex,
                            prefixLen,
                            0,
                            0,
                            false,
                            prefixA,
                            prefixB);
                    }
                    else
                    {
                        continuation = false;
                        _linkData.resetContData ();
                    }
                }
                else
                {
                    _linkData.setContData (contType,
                                           prefixLen,
                                           contDataLen,
                                           downloadRem, // this is the value to update
                                           backTrack,
                                           prefixA,
                                           prefixB);
                }

                bufRem = bufRem - binDataLen;
                bufOffset = bufOffset + binDataLen;
            }
            else if (contType == PrnParseConstants.eContType.PCLFontHddr)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended during processing of a font header  //
                // download sequence.                                         //
                //                                                            //
                //------------------------------------------------------------//

                hddrOK = _parseFontHddrPCL.analyseFontHddr (-1,
                                                            _fileOffset,
                                                            _buf,
                                                            ref bufRem,
                                                            ref bufOffset,
                                                            _linkData,
                                                            _options,
                                                            _table);

                if (!hddrOK)
                    invalidSeqFound = true;
            }
            else if (contType == PrnParseConstants.eContType.PCLFontChar)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended during processing of a font         //
                // character download sequence.                               //
                //                                                            //
                //------------------------------------------------------------//

                charOK = _parseFontCharPCL.analyseFontChar (-1,
                                                            _fileOffset,
                                                            _buf,
                                                            ref bufRem,
                                                            ref bufOffset,
                                                            _linkData,
                                                            _options,
                                                            _table);
                
                if (!charOK)
                    invalidSeqFound = true;
            }
            else if (contType == PrnParseConstants.eContType.PCLAlphaNumericID)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended during processing of an             //
                // AlphaNumeric ID sequence.                                  //
                //                                                            //
                //------------------------------------------------------------//

                dataOK = _parsePCLBinary.decodeAlphaNumericID (
                    downloadRem,
                    _fileOffset,
                    _buf,
                    ref bufRem,
                    ref bufOffset,
                    _linkData,
                    _options,
                    _table);

                if (!dataOK)
                    invalidSeqFound = true;

                continuation = false;
                _linkData.resetContData ();
            }
            else if (contType == PrnParseConstants.eContType.PCLColourLookup)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended during processing of a              //
                // Colour Lookup Tables sequence.                             //
                //                                                            //
                //------------------------------------------------------------//

                dataOK = _parsePCLBinary.decodeColourLookup (
                    downloadRem,
                    _fileOffset,
                    _buf,
                    ref bufRem,
                    ref bufOffset,
                    _linkData,
                    _options,
                    _table);

                if (!dataOK)
                    invalidSeqFound = true;

                continuation = false;
                _linkData.resetContData();
            }
            else if (contType == PrnParseConstants.eContType.PCLConfigurationIO)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended during processing of a              //
                // Configuration (I/O) sequence.                              //
                //                                                            //
                //------------------------------------------------------------//

                dataOK = _parsePCLBinary.decodeConfigurationIO(
                    downloadRem,
                    _fileOffset,
                    _buf,
                    ref bufRem,
                    ref bufOffset,
                    _linkData,
                    _options,
                    _table);

                if (!dataOK)
                    invalidSeqFound = true;

                continuation = false;
                _linkData.resetContData();
            }
            else if (contType == PrnParseConstants.eContType.PCLConfigureImageData)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended during processing of a              //
                // Configure Image Data sequence.                             //
                //                                                            //
                //------------------------------------------------------------//

                dataOK = _parsePCLBinary.decodeConfigureImageData (
                    downloadRem,
                    _fileOffset,
                    _buf,
                    ref bufRem,
                    ref bufOffset,
                    _linkData,
                    _options,
                    _table);

                if (!dataOK)
                    invalidSeqFound = true;

                continuation = false;
                _linkData.resetContData ();
            }
            else if (contType == PrnParseConstants.eContType.PCLConfigureRasterData)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended during processing of a              //
                // Configure Raster Data sequence.                            //
                //                                                            //
                //------------------------------------------------------------//

                dataOK = _parsePCLBinary.decodeConfigureRasterData (
                    downloadRem,
                    _fileOffset,
                    _buf,
                    ref bufRem,
                    ref bufOffset,
                    _linkData,
                    _options,
                    _table);

                if (!dataOK)
                    invalidSeqFound = true;

                continuation = false;
                _linkData.resetContData();
            }
            else if (contType == PrnParseConstants.eContType.PCLLogicalPageData)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended during processing of a              //
                // Define Logical Page sequence.                              //
                //                                                            //
                //------------------------------------------------------------//

                dataOK = _parsePCLBinary.decodeDefineLogicalPage (
                    downloadRem,
                    _fileOffset,
                    _buf,
                    ref bufRem,
                    ref bufOffset,
                    _linkData,
                    _options,
                    _table);

                if (!dataOK)
                    invalidSeqFound = true;

                continuation = false;
                _linkData.resetContData();
            }
            else if (contType == PrnParseConstants.eContType.PCLDefineSymbolSet)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended during processing of a              //
                // Define Symbol Set header sequence.                         //
                //                                                            //
                //------------------------------------------------------------//

                dataOK = _parsePCLBinary.decodeDefineSymbolSet (
                    downloadRem,
                    _fileOffset,
                    _buf,
                    ref bufRem,
                    ref bufOffset,
                    _linkData,
                    _options,
                    _table);

                if (!dataOK)
                    invalidSeqFound = true;

                continuation = false;
                _linkData.resetContData();
            }
            else if (contType == PrnParseConstants.eContType.PCLDefineSymbolSetMap)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended during processing of a              //
                // Define Symbol Set mapping data sequence.                   //
                //                                                            //
                //------------------------------------------------------------//

                dataOK = _parsePCLBinary.decodeDefineSymbolSetMap (
                    downloadRem,
                    _fileOffset,
                    _buf,
                    ref bufRem,
                    ref bufOffset,
                    _linkData,
                    _options,
                    _table);

                if (!dataOK)
                    invalidSeqFound = true;
                /*
                if (downloadRem == 0)
                {
                    continuation = false;
                    _linkData.resetContData();
                }
                */
            }
            else if (contType == PrnParseConstants.eContType.PCLDitherMatrix)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended during processing of a Download     //
                // Dither Matrix sequence.                                    //
                //                                                            //
                //------------------------------------------------------------//

                dataOK = _parsePCLBinary.decodeDitherMatrix (
                    downloadRem,
                    _fileOffset,
                    _buf,
                    ref bufRem,
                    ref bufOffset,
                    _linkData,
                    _options,
                    _table);

                if (!dataOK)
                    invalidSeqFound = true;

                if (_linkData.getContType () == PrnParseConstants.eContType.None)
                {
                    continuation = false;
                    _linkData.resetContData ();
                }
            }
            else if (contType == PrnParseConstants.eContType.PCLDitherMatrixPlane)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended during processing of Download       //
                // Dither Matrix plane data header.                           //
                //                                                            //
                //------------------------------------------------------------//

                dataOK = _parsePCLBinary.decodeDitherMatrixPlane (
                    downloadRem,
                    _fileOffset,
                    _buf,
                    ref bufRem,
                    ref bufOffset,
                    _linkData,
                    _options,
                    _table);

                if (!dataOK)
                    invalidSeqFound = true;

                if (_linkData.getContType () == PrnParseConstants.eContType.None)
                {
                    continuation = false;
                    _linkData.resetContData ();
                }
            }
            else if (contType == PrnParseConstants.eContType.PCLDitherMatrixPlaneData)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended during processing of Download       //
                // Dither Matrix plane data.                                  //
                //                                                            //
                //------------------------------------------------------------//

                dataOK = _parsePCLBinary.decodeDitherMatrixPlaneData (
                    downloadRem,
                    _fileOffset,
                    _buf,
                    ref bufRem,
                    ref bufOffset,
                    _linkData,
                    _options,
                    _table);

                if (!dataOK)
                    invalidSeqFound = true;

                if (_linkData.getContType () == PrnParseConstants.eContType.None)
                {
                    continuation = false;
                    _linkData.resetContData ();
                }
            }
            else if (contType == PrnParseConstants.eContType.PCLDriverConfiguration)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended during processing of a              //
                // Driver Configuration sequence.                             //
                //                                                            //
                //------------------------------------------------------------//

                dataOK = _parsePCLBinary.decodeDriverConfiguration (
                    downloadRem,
                    _fileOffset,
                    _buf,
                    ref bufRem,
                    ref bufOffset,
                    _linkData,
                    _options,
                    _table);

                if (!dataOK)
                    invalidSeqFound = true;

                continuation = false;
                _linkData.resetContData ();
            }
            else if (contType == PrnParseConstants.eContType.PCLEscEncText)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended during processing of an             //
                // Escapement Encapsulated Text sequence.                     //
                //                                                            //
                //------------------------------------------------------------//

                dataOK = _parsePCLBinary.decodeEscEncText (
                    downloadRem,
                    _fileOffset,
                    _buf,
                    ref bufRem,
                    ref bufOffset,
                    _linkData,
                    _options,
                    _table);

                if (!dataOK)
                    invalidSeqFound = true;

                continuation = false;
                _linkData.resetContData ();
            }
            else if (contType == PrnParseConstants.eContType.PCLEscEncTextData)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended during processing of an             //
                // Escapement Encapsulated Text data sequence.                //
                //                                                            //
                //------------------------------------------------------------//

                dataOK = _parsePCLBinary.decodeEscEncTextData (
                    downloadRem,
                    _fileOffset,
                    _buf,
                    ref bufRem,
                    ref bufOffset,
                    _linkData,
                    _options,
                    _table);

                if (!dataOK)
                    invalidSeqFound = true;

                continuation = false;
                _linkData.resetContData ();
            }
            else if (contType == PrnParseConstants.eContType.PCLPaletteConfiguration)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended during processing of a              //
                // Palette Configuration sequence.                            //
                //                                                            //
                //------------------------------------------------------------//

                dataOK = _parsePCLBinary.decodePaletteConfiguration (
                    downloadRem,
                    _fileOffset,
                    _buf,
                    ref bufRem,
                    ref bufOffset,
                    _linkData,
                    _options,
                    _table);

                if (!dataOK)
                    invalidSeqFound = true;

                continuation = false;
                _linkData.resetContData ();
            }
            else if (contType ==
                PrnParseConstants.eContType.PCLUserDefPatternHddr)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended during processing of an             //
                // User Defined Pattern sequence header.                      //
                //                                                            //
                //------------------------------------------------------------//

                dataOK = _parsePCLBinary.decodeUserDefinedPattern (
                    downloadRem,
                    _fileOffset,
                    _buf,
                    ref bufRem,
                    ref bufOffset,
                    _linkData,
                    _options,
                    _table);

                if (!dataOK)
                    invalidSeqFound = true;

                continuation = false;
                _linkData.resetContData ();
            }
            else if (contType ==
                PrnParseConstants.eContType.PCLUserDefPatternData)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended during processing of an             //
                // User Defined Pattern sequence pattern data.                //
                //                                                            //
                //------------------------------------------------------------//

                dataOK = _parsePCLBinary.decodeUserDefinedPatternData (
                    downloadRem,
                    _fileOffset,
                    _buf,
                    ref bufRem,
                    ref bufOffset,
                    _linkData,
                    _options,
                    _table);

                if (!dataOK)
                    invalidSeqFound = true;

                if (_linkData.getContType () == PrnParseConstants.eContType.None)
                {
                    continuation = false;
                    _linkData.resetContData ();
                }
            }
            else if (contType == PrnParseConstants.eContType.PCLViewIlluminant)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended during processing of a              //
                // Viewing Illuminant sequence.                               //
                //                                                            //
                //------------------------------------------------------------//

                dataOK = _parsePCLBinary.decodeViewIlluminant (
                    downloadRem,
                    _fileOffset,
                    _buf,
                    ref bufRem,
                    ref bufOffset,
                    _linkData,
                    _options,
                    _table);

                if (!dataOK)
                    invalidSeqFound = true;

                continuation = false;
                _linkData.resetContData ();
            }
            else if (contType == PrnParseConstants.eContType.PCLEmbeddedPML)
            {
                //--------------------------------------------------------//
                //                                                        //
                // Download contains data which may include an embedded   //
                // PML sequence.                                          //
                //                                                        //
                //--------------------------------------------------------//

                dataOK = _parsePCLBinary.decodeEmbeddedPML (downloadRem,
                                            _fileOffset,
                                            _buf,
                                            ref bufRem,
                                            ref bufOffset,
                                            _linkData,
                                            _options,
                                            _table);

                if (!dataOK)
                    invalidSeqFound = true;

                continuation = false;
                _linkData.resetContData ();
            }
            else if (contType == PrnParseConstants.eContType.PCLMultiByteData)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended during processing of a multi-byte   //
                // text parsing method sequence.                              //
                //                                                            //
                //------------------------------------------------------------//

                String typeText;

                if ((_textParsingMethod ==
                    (Int32) PCLTextParsingMethods.ePCLVal.m83_UTF8) ||
                    (_textParsingMethod ==
                    (Int32) PCLTextParsingMethods.ePCLVal.m1008_UTF8_alt))
                    typeText = "UTF-8 data";
                else
                    typeText = "Data";

                PrnParseData.processLines (
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _linkData,
                    ToolCommonData.ePrintLang.PCL,
                    _buf,
                    _fileOffset,
                    bufRem,
                    ref bufRem,
                    ref bufOffset,
                    ref continuation,
                    true,
                    false,
                    false,
                    PrnParseConstants.asciiEsc,
                    typeText,
                    _textParsingMethod,
                    _indxCharSetSubAct,
                    (Byte) _valCharSetSubCode,
                    _indxCharSetName,
                    _indxOffsetFormat,
                    _analysisLevel);

                if (continuation)
                {
                    contType = PrnParseConstants.eContType.PCLMultiByteData;

                    _linkData.setBacktrack (contType, -bufRem);
                }
                else
                {
                    contType = PrnParseConstants.eContType.None;

                    _linkData.resetContData ();
                }
            }
            else if (contType == PrnParseConstants.eContType.PCLEmbeddedData)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended during processing of an embedded    //
                // data sequence (e.g. the data associated with the           //
                // Transparent Print command).                                //
                //                                                            //
                //------------------------------------------------------------//

                PrnParseData.processLines (
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _linkData,
                    ToolCommonData.ePrintLang.Unknown,
                    _buf,
                    _fileOffset,
                    downloadRem,
                    ref bufRem,
                    ref bufOffset,
                    ref dummyBool,
                    true,
                    false,
                    false,
                    PrnParseConstants.asciiEsc,
                    "Embedded data",
                    0,
                    _indxCharSetSubAct,
                    (Byte) _valCharSetSubCode,
                    _indxCharSetName,
                    _indxOffsetFormat,
                    _analysisLevel);

                continuation = false;
                _linkData.resetContData ();
            }
            else if (contType == PrnParseConstants.eContType.PCLComplex)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended during processing of a complex      //
                // (parameterised) escape sequence.                           //
                //                                                            //
                //------------------------------------------------------------//

                badSeq = parseSequenceComplex (ref bufRem,
                                               ref bufOffset,
                                               ref continuation,
                                               ref breakpoint,
                                               ref crntPDL,
                    //   true, // // CheckExtensionTable //
                                               true);

                if (badSeq)
                    invalidSeqFound = true;
            }
            else if (contType == PrnParseConstants.eContType.PCLExtension)
            {
                //-------------------------------------------------------------------//
                //                                                                   //
                // Previous 'block' ended during processing of a complex escape      //
                // sequence which has been identified as a vendor-defined extension  //
                // sequence.                                                         //
                //                                                                   //
                //-------------------------------------------------------------------//

                badSeq = parseSequenceComplex (ref bufRem,
                                               ref bufOffset,
                                               ref continuation,
                                               ref breakpoint,
                                               ref crntPDL,
                    //   true, // // CheckExtensionTable //
                                               false);

                if (badSeq)
                    invalidSeqFound = true;
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // Previous 'block' ended with a partial match of a Special   //
                // Escape sequence, or with insufficient characters to        //
                // identify the type of sequence.                             //
                // The continuation action has already reset the buffer, so   //
                // now unset the markers.                                     //
                //                                                            //
                //------------------------------------------------------------//

                continuation = false;
                _linkData.resetContData ();
            }

            if ((_endOffset != -1) && ((_fileOffset + bufOffset) > _endOffset))
                endReached = true;

            return invalidSeqFound;
        }


        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a r s e S e q u e n c e                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Process sequences until end-point reached.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean parseSequence(
            ref Int32 bufRem,
            ref Int32 bufOffset,
            ref ToolCommonData.ePrintLang crntPDL,
            ref Boolean endReached)
        {
            Boolean breakpoint = false;
            Boolean continuation = false;
            Boolean langSwitch = false;
            Boolean badSeq = false;
            Boolean invalidSeqFound = false;
            Boolean seqKnown = false;
            Boolean dummyBool = false;

            while (!continuation && !breakpoint && !langSwitch &&
                   !endReached   && (bufRem > 0))
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
                else if (_buf[bufOffset] != PrnParseConstants.asciiEsc)
                {
                    if (_buf[bufOffset] ==
                        PrnParseConstants.prescribeSCRCDelimiter)
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

                            continuation = true;

                            _linkData.setBacktrack(
                                PrnParseConstants.eContType.PCLMultiByteData,
                                -bufRem);
                        }
                        else
                        {
                            if ((_buf[bufOffset + 1]
                                == _linkData.PrescribeSCRC)
                                                  &&
                                (_buf[bufOffset + 2] ==
                                PrnParseConstants.prescribeSCRCDelimiter))
                            {
                                langSwitch = true;
                                crntPDL = ToolCommonData.ePrintLang.Prescribe;
                                //  _linkData.MakeOvlPos =
                                //      PrnParseConstants.eOvlPos.AfterPages;
                                _linkData.PrescribeCallerPDL =
                                    ToolCommonData.ePrintLang.PCL;
                            }
                        }
                    }

                    if ((!continuation) && (!langSwitch))
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Data.                                              //
                        // Output <LF> terminated lines until next Escape     //
                        // character found, or end of buffer reached.         //
                        //                                                    //
                        //----------------------------------------------------//

                        String typeText;

                        if ((_textParsingMethod ==
                            (Int32)PCLTextParsingMethods.ePCLVal.m83_UTF8) ||
                            (_textParsingMethod ==
                            (Int32)PCLTextParsingMethods.ePCLVal.m1008_UTF8_alt))
                            typeText = "UTF-8 data";
                        else
                            typeText = "Data";

                        PrnParseData.processLines(
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            _linkData,
                            ToolCommonData.ePrintLang.PCL,
                            _buf,
                            _fileOffset,
                            bufRem,
                            ref bufRem,
                            ref bufOffset,
                            ref continuation,
                            true,
                            false,
                            false,
                            PrnParseConstants.asciiEsc,
                            typeText,
                            _textParsingMethod,
                            _indxCharSetSubAct,
                            (Byte)_valCharSetSubCode,
                            _indxCharSetName,
                            _indxOffsetFormat,
                            _analysisLevel);

                        if (_parseType == PrnParse.eParseType.MakeOverlay &&
                            _linkData.MakeOvlPageMark == true)
                        {
                            PrnParseConstants.eOvlPos makeOvlPos;

                            makeOvlPos = _linkData.MakeOvlPos;

                            if (makeOvlPos == PrnParseConstants.eOvlPos.BeforeFirstPage)
                            {
                                makeOvlPos = PrnParseConstants.eOvlPos.WithinFirstPage;
                            }
                            else if (makeOvlPos != PrnParseConstants.eOvlPos.WithinFirstPage)
                            {
                                makeOvlPos = PrnParseConstants.eOvlPos.WithinOtherPages;
                            }

                            _linkData.MakeOvlPos = makeOvlPos;
                        }

                        if (_linkData.MakeOvlAct !=
                                PrnParseConstants.eOvlAct.None)
                        {
                            breakpoint = true;
                        }
                        else if (continuation)
                        {
                            _linkData.setBacktrack(
                                PrnParseConstants.eContType.PCLMultiByteData,
                                -bufRem);
                        }
                        else
                        {
                            _linkData.resetContData();
                        }
                    }
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Escape character found.                                //
                    //                                                        //
                    //--------------------------------------------------------//

                    _linkData.resetPCLComboData();

                    if ((bufRem >= 2) &&
                             (_buf[bufOffset + 1] >=
                                PrnParseConstants.pclSimpleICharLow)
                                           &&
                             (_buf[bufOffset + 1] <=
                                PrnParseConstants.pclSimpleICharHigh))
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // A Simple (two-character) escape sequence.          //
                        // Or the start of a (vendor-defined) Extension       //
                        // Complex escape sequence which doesn't follow the   //
                        // rules.                                             //
                        // We could check for the latter first; we could do   //
                        // this because:                                      //
                        //  - The only known Extension sequences with the     //
                        //    I_CHAR character in this range are the Data     //
                        //    Products ones, with I_CHAR="|".                 //
                        //  - There are no standard Simple sequences with "|" //
                        //    as the second character (out of permitted       //
                        //    range).                                         //
                        //                                                    //
                        //----------------------------------------------------//

                        if (_buf[bufOffset + 1] == PrnParseConstants.asciiPipe)
                        {
                            //------------------------------------------------//
                            //                                                //
                            // DataProducts (Typhoon series) Extension        //
                            // sequence?                                      //
                            // Ideally, this I_CHAR should be checked against //
                            // a set of values in a table, but so far, we     //
                            // only have this one value.                      //
                            //                                                //
                            //------------------------------------------------//

                            badSeq = parseSequenceComplex(ref bufRem,
                                                           ref bufOffset,
                                                           ref continuation,
                                                           ref breakpoint,
                                                           ref crntPDL,
                                                           //   true, // // CheckExtensionTable //
                                                           false);

                            if (crntPDL != ToolCommonData.ePrintLang.PCL)
                                langSwitch = true;

                            if (badSeq)
                                invalidSeqFound = true;
                        }
                        else
                        {
                            seqKnown = parseSequenceSimple(ref bufRem,
                                                            ref bufOffset,
                                                            ref breakpoint);

                            if (!seqKnown)
                            {
                                //--------------------------------------------//
                                //                                            //
                                // Unrecognised data.                         //
                                // Output <LF> terminated lines until next    //
                                // Escape character found, or end of buffer   //
                                // reached.                                   //
                                //                                            //
                                //--------------------------------------------//

                                PrnParseData.processLines(
                                    _table,
                                    PrnParseConstants.eOvlShow.None,
                                    _linkData,
                                    ToolCommonData.ePrintLang.PCL,
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
                                    (Byte)_valCharSetSubCode,
                                    _indxCharSetName,
                                    _indxOffsetFormat,
                                    _analysisLevel);
                            }
                        }
                    }
                    else if ((bufRem >= 3)     // min = 3 if nil-G with no value
                                           &&
                             (_buf[bufOffset + 1] >=
                                PrnParseConstants.pclComplexICharLow)
                                           &&
                             (_buf[bufOffset + 1] <=
                                PrnParseConstants.pclComplexICharHigh))
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Complex (parameterised) PCL sequence.              //
                        //                                                    //
                        //----------------------------------------------------//

                        badSeq = parseSequenceComplex(ref bufRem,
                                                       ref bufOffset,
                                                       ref continuation,
                                                       ref breakpoint,
                                                       ref crntPDL,
                                                       //   true, // // CheckExtensionTable //
                                                       true);

                        if (crntPDL != ToolCommonData.ePrintLang.PCL)
                            langSwitch = true;

                        if (badSeq)
                            invalidSeqFound = true;
                    }
                    else if (bufRem < 4)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Remaining data in buffer too short to determine    //
                        // type.                                              //
                        // Initiate continuation action.                      //
                        //                                                    //
                        //----------------------------------------------------//

                        continuation = true;

                        _linkData.setBacktrack(
                            PrnParseConstants.eContType.Unknown,
                            -bufRem);
                    }
                    else
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Unrecognised data.                                 //
                        // Output <LF> terminated lines until next Escape     //
                        // character found, or end of buffer reached.         //
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
                            "Unexpected sequence found:");

                        PrnParseData.processLines(
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            _linkData,
                            ToolCommonData.ePrintLang.PCL,
                            _buf,
                            _fileOffset,
                            bufRem,
                            ref bufRem,
                            ref bufOffset,
                            ref continuation,
                            true,
                            true,
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

            return invalidSeqFound;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a r s e S e q u e n c e C o m p l e x                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // This function processes a sequence (already identified by its      //
        // opening characters) as a Complex (Parameterised) Escape Sequence.  //
        //                                                                    //
        // Continuation action is invoked if the sequence is not terminated   //
        // in the current 'block'.                                            //
        //                                                                    //
        // Format of Complex Escape Sequences:                                //
        //                                                                    //
        //      byte     Escape character (x'1B)                              //
        //      byte     Parameterised Indicator character (I_CHAR)           //
        //      byte     Group character                   (G_CHAR)           //
        //      n-byte   Value field                       (V_FIELD)          //
        //      byte     Termination character             (T_CHAR)           //
        //                                                                    //
        // Sequences may be combined, where the I/G_CHAR prefix is the same,  //
        // by omitting this prefix (and the Escape character) from the second //
        // and subsequent sequence. In these cases, the Termination Character //
        // for all but the last in the combination becomes a Parameter        //
        // Character (P_CHAR) which is the lower-case equivalent of the       //
        // upper-case Termination Character (T_CHAR).                         //
        //                                                                    //
        // Some sequences have no Group character (in which case they cannot  //
        // be part of a Combination sequence).                                //
        // Some sequences do not have a value field.                          //
        //                                                                    //
        // Each table entry matches I/G/T_CHAR values against descriptions.   //
        // Options indicate special treatment is required for some sequences. //
        //                                                                    //
        // I_CHAR should be within the (ASCII) range 0x21 - 0x2f.             //
        // However, vendor-dependent extensions may utilise other values      //
        // (e.g. 0x7C by DataProducts devices).                               //
        // We don't currently cater for any of these, but could if they were  //
        // tabulated in an Extension Sequence table.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean parseSequenceComplex(
            ref Int32 bufRem,
            ref Int32 bufOffset,
            ref Boolean continuation,
            ref Boolean breakpoint,
            ref ToolCommonData.ePrintLang crntPDL,
            //  Boolean CheckExtensionTable,
            Boolean CheckStandardTable)
        {
            Boolean invalidSeq = true;

            PrnParseConstants.eContType contType =
                PrnParseConstants.eContType.None;

            PrnParseConstants.eActPCL actType =
                PrnParseConstants.eActPCL.None;

            PrnParseConstants.eOvlAct makeOvlAct =
                PrnParseConstants.eOvlAct.None;

            PrnParseConstants.eOvlShow makeOvlShow =
                PrnParseConstants.eOvlShow.None;

            Byte gChar = 0x20,
                 iChar = 0x20,
                 p_or_TChar,
                 crntByte;

            Int32 vLen = 0,
                  vtLen = 0,
                  processedLen,
                  seqLen,
                  seqOffset,
                  seqPos,
                  seqStart,
                  prefixLen = 0,
                  binDataLen,
                  contDataLen = 0,
                  downloadRem = 0,
                  endPos,
                  vInt,
                  vPosFirst,
                  vPosNext,
                  vPosCrnt;
            //   Int32 vendorCode;

            Int64 comboStart = 0;

            Boolean seqKnown,
                    seqComplete,
                    seqProprietary = false,
                    comboSeq = false,
                    comboFirst = false,
                    comboLast = false,
                    comboModified = false,
                    backTrack = false,
                    p_or_TCharFound = false;

            Boolean optObsolete = false,
                    optResetHPGL2 = false,
                    optNilGChar = false,
                    optNilValue = false,
                    optValueIsLen = false,
                    optDisplayHexVal = false;
            // Boolean optValueAngleQuoted;

            Boolean vCheck,
                    vInvalid,
                    vCharInvalid,
                    vNegative,
                    vFractional,
                    vSignFound,
                    vStarted,
                    vQuotedStart,
                    vQuotedEnd,
                    vNumberStarted;

            String descComplex = "",
                   typeText,
                   vendorName;

            invalidSeq = false;
            seqComplete = false;
            continuation = false;
            vInvalid = false;
            vNegative = false;
            vFractional = false;
            vSignFound = false;
            vStarted = false;
            vQuotedStart = false;
            vQuotedEnd = false;
            vNumberStarted = false;

            vInt = 0;
            binDataLen = 0;

            p_or_TChar = 0x20;

            //----------------------------------------------------------------//
            //                                                                //
            // Determine whether continuation call or not.                    //
            //                                                                //
            //----------------------------------------------------------------//

            if (_linkData.isContinuation ())
            {
                //------------------------------------------------------------//
                //                                                            //
                // Continuation situation.                                    //
                // This will only occur if the sequence is a combination      //
                // sequence, and at least one (value + parameter character)   //
                // pair has already been processed.                           //
                //                                                            //
                //------------------------------------------------------------//

                contType = PrnParseConstants.eContType.None;

                _linkData.getContData (ref contType,
                                       ref prefixLen,
                                       ref contDataLen,
                                       ref downloadRem,
                                       ref backTrack,
                                       ref iChar,
                                       ref gChar);

                _linkData.getPCLComboData (ref comboSeq,
                                           ref comboFirst,
                                           ref comboLast,
                                           ref comboModified,
                                           ref comboStart);

                comboFirst = false;
                comboLast = false;

                seqOffset = 0;
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // Not a continuation situation.                              //
                // Read and store details of I and G characters, and hence    //
                // determine sequence prefix.                                 //
                //                                                            //
                //------------------------------------------------------------//

                iChar = _buf[bufOffset + 1];
                gChar = _buf[bufOffset + 2];

                if ((gChar < PrnParseConstants.pclComplexGCharLow)
                              ||
                    (gChar > PrnParseConstants.pclComplexGCharHigh))
                {
                    gChar = 0x20;
                    prefixLen = 1;
                }
                else
                {
                    prefixLen = 2;
                }

                seqOffset = prefixLen + 1; // references first V_field //
                comboSeq = false;
                comboFirst = true;  // initial state if combo detected //
                comboLast = false; // initial state if combo detected //
                comboModified = false; // initial state if combo detected //
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Process bytes from the buffer until the (upper-case)           //
            // Termination character is found, or the end of buffer is        //
            // reached, or (in a 'Make Overlay' run) a breakpoint is reached. //
            //                                                                //
            //----------------------------------------------------------------//

            vPosFirst = bufOffset + seqOffset;
            vPosNext  = vPosFirst;
            vPosCrnt  = vPosFirst;
            endPos = bufOffset + bufRem;

            for (int i = vPosFirst;
                 (i < endPos) && !continuation && !breakpoint && !seqComplete;
                 i++)
            {
                crntByte = _buf[i];

                //------------------------------------------------------------//
                //                                                            //
                // Check next character.                                      //
                //                                                            //
                //------------------------------------------------------------//

                if (crntByte == PrnParseConstants.asciiEsc)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Invalid - sequence terminated before previous (part)   //
                    // sequence terminated!                                   //
                    //                                                        //
                    //--------------------------------------------------------//

                    String text;

                    seqComplete = true;
                    invalidSeq = true;

                    if (p_or_TCharFound)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Previous part sequence already processed and       //
                        // displayed.                                         //
                        // We don't want to repeat the display.               //
                        //                                                    //
                        //----------------------------------------------------//

                        p_or_TCharFound = false;

                        text = "previous sequence";
                    }
                    else
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Previous part sequence not yet complete.           //
                        // We want to display what we have up to now.         //
                        //                                                    //
                        //----------------------------------------------------//

                        if (vtLen != 0)
                            p_or_TCharFound = true;

                        if (comboSeq)
                            text = "next (part of) sequence";
                        else
                            text = "next sequence";
                    }

                    PrnParseCommon.addDataRow (
                        PrnParseRowTypes.eType.MsgWarning,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _indxOffsetFormat,
                        _fileOffset + i,
                        _analysisLevel,
                        "*** Warning ***",
                        "",
                        "<Esc> found before termination of " + text);

                    p_or_TChar = PrnParseConstants.asciiSUB;    // will not match any table entries //

                    i = i - 1;                                  // point back to byte before <Esc>  //
                }
                else
                {
                    p_or_TCharFound = false;
                }

                if (seqComplete)
                {
                    // already processed above
                }
                else if ((i == vPosNext) &&
                         (crntByte == PrnParseConstants.asciiAngleLeft))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // First character of value field is "<".                 //
                    // This could be the start of a (non-standard)            //
                    // 'angle-quoted' value as used in proprietary Oce        //
                    // VarioPrint sequences, and in the (obsolete) HP 'Large  //
                    // Character Print Data' sequence; note that these do not //
                    // adhere to the standard PCL syntax rules.               //
                    // Search for equivalent ">" character; we expect it to   //
                    // be within the next 60 bytes.                           //
                    //                                                        //
                    //--------------------------------------------------------//

                    vStarted = true;
                    vQuotedStart = true;
                    vQuotedEnd = false;
                }
                else if ((vQuotedStart) && (!vQuotedEnd))
                {
                    if (crntByte == PrnParseConstants.asciiAngleRight)
                    {
                        vQuotedEnd = true;
                    }
                }
                else if ((crntByte >= PrnParseConstants.pclComplexTCharLow)
                                        &&
                         (crntByte <= PrnParseConstants.pclComplexTCharHigh))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Sequence Terminator Character found.                   //
                    //                                                        //
                    //--------------------------------------------------------//

                    seqComplete = true;
                    p_or_TCharFound = true;
                    p_or_TChar = crntByte;
                    comboLast = true;

                    if (comboSeq)
                        comboFirst = false;
                }
                else if ((crntByte >= PrnParseConstants.pclComplexPCharLow)
                                      &&
                         (crntByte <= PrnParseConstants.pclComplexPCharHigh))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Sequence Parameter Character found.                    //
                    //                                                        //
                    //--------------------------------------------------------//

                    p_or_TCharFound = true;
                    p_or_TChar = (Byte) (crntByte - 0x20);
                    comboLast = false;

                    if (comboSeq)
                        comboFirst = false;
                    else
                        comboFirst = true;

                    comboSeq = true;
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Character must be part of the value field.             //
                    // Check that the character is valid, and calculate the   //
                    // aggregate value (at least, of the integer part).       //
                    //                                                        //
                    //--------------------------------------------------------//

                    vStarted = true;
                    vCharInvalid = false;
                    vQuotedStart = false;
                    vQuotedEnd = false;

                    if (vFractional)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Decimal point already found.                       //
                        // Only allow digits.                                 //
                        //                                                    //
                        //----------------------------------------------------//

                        if ((crntByte >= PrnParseConstants.asciiDigit0)
                                   &&
                            (crntByte <= PrnParseConstants.asciiDigit9))
                        {
                            // do nothing //
                        }
                        else
                        {
                            vCharInvalid = true;
                        }
                    }
                    else if (vNumberStarted)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Digit already found, but not fractional.           //
                        // Only allow digits or decimal point.                //
                        //                                                    //
                        //----------------------------------------------------//

                        if (crntByte == PrnParseConstants.asciiPeriod)
                        {
                            vFractional = true;
                        }
                        else if ((crntByte >= PrnParseConstants.asciiDigit0)
                                         &&
                                 (crntByte <= PrnParseConstants.asciiDigit9))
                        {
                            vInt = (vInt * 10) +
                                   (crntByte - PrnParseConstants.asciiDigit0);
                        }
                        else
                        {
                            vCharInvalid = true;
                        }
                    }
                    else if (vSignFound)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Sign already found, but no digit or decimal point. //
                        // Only allow spaces or digits or decimal point.      //
                        //                                                    //
                        //----------------------------------------------------//

                        if (crntByte == PrnParseConstants.asciiSpace)
                        {
                            // do nothing - spaces allowed before/after sign //
                        }
                        else if (crntByte == PrnParseConstants.asciiPeriod)
                        {
                            vFractional = true;
                        }
                        else if ((crntByte >= PrnParseConstants.asciiDigit0)
                                         &&
                                 (crntByte <= PrnParseConstants.asciiDigit9))
                        {
                            vNumberStarted = true;

                            vInt = (crntByte - PrnParseConstants.asciiDigit0);
                        }
                        else
                        {
                            vCharInvalid = true;
                        }
                    }
                    else
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Only spaces (if anything) found so far.            //
                        // Allow spaces or signs or digits or decimal point.  //
                        //                                                    //
                        //----------------------------------------------------//

                        if (crntByte == PrnParseConstants.asciiSpace)
                        {
                            // do nothing //
                        }
                        else if (crntByte == PrnParseConstants.asciiPlus)
                        {
                            vSignFound = true;
                        }
                        else if (crntByte == PrnParseConstants.asciiMinus)
                        {
                            vSignFound = true;
                            vNegative = true;
                        }
                        else if (crntByte == PrnParseConstants.asciiPeriod)
                        {
                            vFractional = true;
                        }
                        else if ((crntByte >= PrnParseConstants.asciiDigit0)
                                        &&
                                 (crntByte <= PrnParseConstants.asciiDigit9))
                        {
                            vNumberStarted = true;
                            vInt = (crntByte - PrnParseConstants.asciiDigit0);
                        }
                        else
                        {
                            vCharInvalid = true;
                        }
                    }

                    if (vCharInvalid)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Invalid value value !                              //
                        //                                                    //
                        //----------------------------------------------------//

                        String seq;
                        String text;

                        if (crntByte < PrnParseConstants.asciiGraphicMin)
                            seq = "0x" + crntByte.ToString("x2");
                        else
                            seq = ((Char)crntByte).ToString();

                        if (comboSeq)
                            text = "next (part of) combination sequence";
                        else
                            text = "next sequence";

                        vInvalid = true;
                        invalidSeq = true;

                        PrnParseCommon.addDataRow (
                            PrnParseRowTypes.eType.MsgWarning,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            _indxOffsetFormat,
                            _fileOffset + i,
                            _analysisLevel,
                            "*** Warning ***",
                            seq,
                            "Invalid value in " + text);
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Check whether Sequence Terminator Character or Parameter   //
                // Character Has been found.                                  //
                //                                                            //
                //------------------------------------------------------------//

                if (p_or_TCharFound)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Sequence Terminator Character or Parameter Character   //
                    // found.                                                 //
                    // Search table for description of (this part of) the     //
                    // sequence.                                              //
                    //                                                        //
                    //--------------------------------------------------------//

                    String val;
                    String seq;

                    seqKnown = false;
                    seqProprietary = false;

                    if (vNegative)
                    {
                        vInt = -vInt;
                    }

                    vLen = i - vPosCrnt;
                    vtLen = vLen + 1;

                    if (vInvalid || vFractional)
                        vCheck = false;
                    else
                        vCheck = true;

                    /*
                    if (CheckExtensionTable)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Check sequence against entries in special          //
                        // Extension Sequence table.                          //
                        //                                                    //
                        //----------------------------------------------------//

                        seqKnown = PCLExtensionSeqs.checkComplexSeq(
                            (_analysisLevel +
                             _macroLevel),
                            iChar,
                            gChar,
                            p_or_TChar,
                            vCheck,
                            vInt,
                            ref optObsolete,
                            ref optResetHPGL2,
                            ref optNilGChar,
                            ref optNilValue,
                            ref optValueIsLen,
                            ref optValueAngleQuoted,
                            ref actType,
                            ref makeOvlAct,
                            ref descComplex,
                            ref vendorCode);

                        seqKnown = false; // TEMPORARY //
                        if (seqKnown)
                            seqProprietary = true;
                    }
                    */

                    if ((!seqProprietary) && (CheckStandardTable))
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Check sequence against entries in standard Complex //
                        // (Parameterised) Sequence table.                    //
                        //                                                    //
                        //----------------------------------------------------//

                        seqKnown = PCLComplexSeqs.checkComplexSeq (
                            (_analysisLevel +
                             _macroLevel),
                            iChar,
                            gChar,
                            p_or_TChar,
                            vCheck,
                            vInt,
                            ref optObsolete,
                            ref optResetHPGL2,
                            ref optNilGChar,
                            ref optNilValue,
                            ref optValueIsLen,
                            ref optDisplayHexVal,
                            ref actType,
                            ref makeOvlAct,
                            ref descComplex);
                    }

                    if (seqKnown)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Check sequence validity against option flags.      //
                        //                                                    //
                        //----------------------------------------------------//

                        if (optNilValue && vStarted)
                        {
                            invalidSeq = true;

                            PrnParseCommon.addTextRow (
                                PrnParseRowTypes.eType.MsgWarning,
                                _table,
                                PrnParseConstants.eOvlShow.None,
                                "",
                                "*** Warning ***",
                                "",
                                "Unexpected value field in next sequence");
                        }

                        if (optNilGChar && (prefixLen != 1))
                        {
                            //------------------------------------------------//
                            //                                                //
                            // This should never occur?                       //
                            // It implies that we have a sequence with a      //
                            // G_CHAR component which has matched an entry in //
                            // the table for a nil-G_CHAR sequence!           //
                            //                                                //
                            //------------------------------------------------//

                            invalidSeq = true;

                            PrnParseCommon.addTextRow (
                                PrnParseRowTypes.eType.MsgError,
                                _table,
                                PrnParseConstants.eOvlShow.None,
                                "",
                                "*** Error ***",
                                "",
                                "Unexpected nil-G sequence match");
                        }
                        else if (!optNilGChar && (prefixLen != 2))
                        {
                            //------------------------------------------------//
                            //                                                //
                            // This should never occur?                       //
                            // It implies that we have a sequence without a   //
                            // G_CHAR component which has matched an entry in //
                            // the table for a standard (with G_CHAR)         //
                            // sequence!                                      //
                            //                                                //
                            //------------------------------------------------//

                            invalidSeq = true;

                            PrnParseCommon.addTextRow (
                                PrnParseRowTypes.eType.MsgError,
                                _table,
                                PrnParseConstants.eOvlShow.None,
                                "",
                                "*** Error ***",
                                "",
                                "Unexpected sequence match");
                        }

                        //----------------------------------------------------//
                        //                                                    //
                        // Action 'active' option flags.                      //
                        //                                                    //
                        //----------------------------------------------------//

                        if (optResetHPGL2)
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Sequence resets HP-GL/2 state variables.       //
                            //                                                //
                            //------------------------------------------------//

                            _parseHPGL2.resetHPGL2 ();
                        }

                        if (actType ==
                            PrnParseConstants.eActPCL.SwitchToHPGL2)
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Switch to HP-GL/2 language processing.         //
                            //                                                //
                            //------------------------------------------------//

                            crntPDL = ToolCommonData.ePrintLang.HPGL2;
                            seqComplete = true;
                        }
                        else if (actType ==
                            PrnParseConstants.eActPCL.SwitchToPJL)
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Switch to PJL language processing.             //
                            //                                                //
                            //------------------------------------------------//

                            crntPDL = ToolCommonData.ePrintLang.PJL;
                            seqComplete = true;
                        }
                        else if (actType ==
                            PrnParseConstants.eActPCL.TextParsing)
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Sequence sets TextParsingMethod.               //
                            //                                                //
                            //------------------------------------------------//

                            _textParsingMethod = vInt;
                        }

                        if (optObsolete)
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Sequence marked as obsolete.                   //
                            //                                                //
                            //------------------------------------------------//

                            PrnParseCommon.addTextRow (
                                PrnParseRowTypes.eType.MsgComment,
                                _table,
                                PrnParseConstants.eOvlShow.None,
                                "",
                                "Comment",
                                "",
                                "The following sequence is considered to be" +
                                " obsolete:");
                        }
                    }

                    //--------------------------------------------------------//
                    //                                                        //
                    // Check for 'macro stop' sequence.                       //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (actType == PrnParseConstants.eActPCL.MacroStop)
                    {
                        if (_macroLevel > 0)
                            _macroLevel--;

                        PrnParseCommon.setDisplayCriteria (_showMacroData,
                                                          _macroLevel);
                    }

                    //--------------------------------------------------------//
                    //                                                        //
                    // Update sequence metrics.                               //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (comboFirst)
                    {
                        seqPos = vPosCrnt - prefixLen;
                        seqLen = vtLen + prefixLen;
                        seqStart = bufOffset;
                        comboStart = _fileOffset + seqStart;

                        if (seqProprietary)
                            typeText = "PCL Extension";
                        else
                            typeText = "PCL Parameterised";
                    }
                    else
                    {
                        seqPos = vPosCrnt;
                        seqLen = vtLen;
                        seqStart = vPosCrnt;

                        typeText = "";
                    }

                    //--------------------------------------------------------//
                    //                                                        //
                    // If a 'Make Overlay' run, check what action (if any) is //
                    // required.                                              //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (_parseType == PrnParse.eParseType.MakeOverlay)
                    {
                        Int32 fragLen;

                        if (comboFirst)
                            fragLen = vtLen + prefixLen + 1;
                        else
                            fragLen = vtLen;

                        _linkData.MakeOvlAct = makeOvlAct;
                        _linkData.PclComboModified = comboModified;

                        _linkData.setPrefixData (prefixLen, iChar, gChar);

                        breakpoint = PrnParseMakeOvl.checkActionPCL (
                                        comboSeq,
                                        seqComplete,
                                        vInt,
                                        seqStart,
                                        fragLen,
                                        _fileOffset,
                                        _linkData,
                                        _table,
                                        _indxOffsetFormat);

                        makeOvlAct = _linkData.MakeOvlAct;
                        makeOvlShow = _linkData.MakeOvlShow;

                        if (makeOvlAct == PrnParseConstants.eOvlAct.Adjust)
                            makeOvlAct = PrnParseConstants.eOvlAct.None;

                        if (breakpoint)
                        {
                            contType = _linkData.getContType ();

                            comboModified = _linkData.PclComboModified;

                            _linkData.setPCLComboData (comboSeq,
                                                       comboFirst,
                                                       comboLast,
                                                       comboModified,
                                                       comboStart);
                        }
                    }

                    //--------------------------------------------------------//
                    //                                                        //
                    // Output details and interpretation of (this part of)    //
                    // the sequence.                                          //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (seqProprietary)
                    {
                        /*
                        switch (vendorCode)
                        {
                            case eVendorDataProducts:
                                vendorName = "Data Products";
                                break;

                            case eVendorOce:
                                vendorName = "Oce";
                                break;

                            default:
                                vendorName = "Unknown";
                        }
                        */
                        vendorName = "Unknown"; // TEMPORARY //

                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.MsgComment,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "Comment",
                            "",
                            "The following sequence is proprietary to " +
                            vendorName + ":");
                    }

                    if (vLen > 0)
                    {
                        if ((optDisplayHexVal) &&
                            (vCheck)) // vCheck ensures we don't do this for invalid or fractional values
                            val = _ascii.GetString (_buf, vPosCrnt, vLen) +
                                " (0x" + vInt.ToString ("x") + ")";
                        else
                            val = _ascii.GetString (_buf, vPosCrnt, vLen);
                    }
                    else
                    {
                        val = "";
                    }

                    if (seqLen > 0)
                        seq = _ascii.GetString (_buf, seqPos, seqLen);
                    else
                        seq = "";

                    if (seqProprietary)
                    {
                        parseSequenceComplexDisplay (_fileOffset + seqStart,
                                                     prefixLen,
                                                     comboFirst,
                                                     makeOvlShow,
                                                     typeText,
                                                     seq,
                                                     descComplex,
                                                     val);
                    }
                    else
                    {
                        if (seqKnown)
                        {
                            if ((makeOvlAct ==
                                    PrnParseConstants.eOvlAct.IdMacro) &&
                                (vInt != _linkData.MakeOvlMacroId))
                            {
                                makeOvlAct = PrnParseConstants.eOvlAct.None;
                            }

                            parseSequenceComplexDisplay (_fileOffset + seqStart,
                                                         prefixLen,
                                                         comboFirst,
                                                         makeOvlShow,
                                                         typeText,
                                                         seq,
                                                         descComplex,
                                                         val);

                            if ((actType == PrnParseConstants.eActPCL.StyleData)
                                  && (_interpretStyle))
                            {
                                processStyleData (vInt);
                            }
                        }
                        else
                        {
                            parseSequenceComplexDisplay (
                                _fileOffset + seqStart,
                                prefixLen,
                                comboFirst,
                                makeOvlShow,
                                typeText,
                                seq,
                                "***** Unknown sequence *****",
                                val);
                        }
                    }

                    //--------------------------------------------------------//
                    //                                                        //
                    // Adjust pointer to start of next (part) sequence.       //
                    // If the sequence is recognised as being a 'download'    //
                    // type of sequence, the pointer will reference the start //
                    // of 'download' characters.                              //
                    // Otherwise, the pointer will reference either the start //
                    // of the next escape sequence (if the T_CHAR has been    //
                    // found), or the start of the 'value' field of the next  //
                    // part of a combination sequence.                        //
                    //                                                        //
                    //--------------------------------------------------------//

                    vPosCrnt = vPosCrnt + vtLen;

                    if (comboSeq)
                        vPosNext = vPosCrnt;

                    if (!seqComplete)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Terminator character has not yet been found, so a  //
                        // combination sequence is being processed.           //
                        //                                                    //
                        //----------------------------------------------------//

                        comboSeq = true;
                    }

                    //--------------------------------------------------------//
                    //                                                        //
                    // Check for 'macro start' sequence.                      //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (actType == PrnParseConstants.eActPCL.MacroStart)
                    {
                        if (!_showMacroData)
                        {
                            PrnParseCommon.addTextRow (
                                PrnParseRowTypes.eType.MsgComment,
                                _table,
                                PrnParseConstants.eOvlShow.None,
                                "",
                                "Comment",
                                "",
                                "Preference options inhibit display of " +
                                "macro contents");
                        }

                        _macroLevel++;

                        PrnParseCommon.setDisplayCriteria (_showMacroData,
                                                          _macroLevel);
                    }

                    //--------------------------------------------------------//
                    //                                                        //
                    // Check for 'download' sequence.                         //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (seqKnown && optValueIsLen)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Download sequence.                                 //
                        // Terminator character is expected to be followed by //
                        // download data bytes (the number of which is        //
                        // defined by the Value field preceding the T_CHAR    //
                        // (Terminator) or P_CHAR (Parameter) character.      //
                        //                                                    //
                        //----------------------------------------------------//

                        if (vInvalid || vFractional || vNegative)
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Invalid bytecount for download sequence.       //
                            // Abort processing of the current sequence.      //
                            //                                                //
                            //------------------------------------------------//

                            invalidSeq = true;

                            PrnParseCommon.addTextRow (
                                PrnParseRowTypes.eType.MsgWarning,
                                _table,
                                PrnParseConstants.eOvlShow.None,
                                "",
                                "*** Warning ***",
                                "",
                                "Invalid bytecount in previous sequence");

                            PrnParseCommon.addTextRow (
                                PrnParseRowTypes.eType.MsgWarning,
                                _table,
                                PrnParseConstants.eOvlShow.None,
                                "",
                                "*** Warning ***",
                                "",
                                "Processing of current sequence abandoned");
                        }
                        else
                        {
                            //------------------------------------------------//
                            //                                                //
                            // Valid download sequence introduction.          //
                            // Adjust external pointers to start of the       //
                            // download data (after the sequence termination  //
                            // character).                                    //
                            // Adjust internal pointers so that the           //
                            // adjustment after the end of the 'for' loop     //
                            // does not 'double count'.                       //
                            //                                                //
                            //------------------------------------------------//

                            seqLen = seqOffset + (vPosCrnt - vPosFirst);
                            bufRem = bufRem - seqLen;
                            bufOffset = bufOffset + seqLen;

                            seqOffset = 0;
                            vPosFirst = vPosCrnt;
                            binDataLen = vInt;

                            //------------------------------------------------//
                            //                                                //
                            // Process download data.                         //
                            //                                                //
                            //------------------------------------------------//

                            if ((_parseType == PrnParse.eParseType.MakeOverlay)
                                                  &&
                                (makeOvlAct ==
                                    PrnParseConstants.eOvlAct.Remove))
                            {
                                _linkData.MakeOvlSkipEnd += vInt;
                                _linkData.DataLen += vInt;
                            }

                            i = i + binDataLen;
                            vPosCrnt = vPosCrnt + binDataLen;

                            continuation = parseSequenceEmbeddedData (
                                                ref bufRem,
                                                ref bufOffset,
                                                ref binDataLen,
                                                actType,
                                                vPosFirst,
                                                endPos,
                                                prefixLen,
                                                iChar,
                                                gChar,
                                                seqComplete,
                                                ref invalidSeq);

                            vPosFirst = vPosCrnt;
                            bufRem = bufRem - binDataLen;
                            bufOffset = bufOffset + binDataLen;
                            /*
                            i = i + binDataLen;
                            vPosCrnt = vPosCrnt + binDataLen;
                            vPosFirst = vPosCrnt;
                            */

                        }  // end of download sequence processing
                    }  // end of 'if download sequence'

                    if (comboSeq)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Reset flags for next part of combination sequence. //
                        //                                                    //
                        //----------------------------------------------------//

                        vInvalid = false;
                        vNegative = false;
                        vFractional = false;
                        vSignFound = false;
                        vStarted = false;
                        vNumberStarted = false;
                        vInt = 0;
                        binDataLen = 0;
                        vLen = 0;
                        vtLen = 0;
                    }
                }  // end of 'if termination character found'
            }  // end of 'for' loop

            //----------------------------------------------------------------//
            //                                                                //
            // Either the complete sequence has been found and processed, or  //
            // the end of buffer has been reached, or a continuation (for a   //
            // download sequence) has already been signalled.                 //
            // Calculate how much has been processed in this iteration, and   //
            // then adjust external buffer pointers and flags.                //
            //                                                                //
            //----------------------------------------------------------------//

            processedLen = seqOffset + (vPosCrnt - vPosFirst);
            bufRem = bufRem - processedLen;
            bufOffset = bufOffset + processedLen;

            if (continuation)
            {
                //------------------------------------------------------------//
                //                                                            //
                // End of buffer reached during processing of a 'download'    //
                // sequence.                                                  //
                // Do nothing - action already invoked.                       //
                //                                                            //
                //------------------------------------------------------------//
            }
            else if (breakpoint)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Breakpoint (indicating sequence removal required) found    //
                // during MakeMacro run.                                      //
                // Do nothing - action already invoked.                       //
                //                                                            //
                //------------------------------------------------------------//
            }
            else if (seqComplete)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Sequence Terminator character found - no continuation      //
                // necessary.                                                 //
                //                                                            //
                //------------------------------------------------------------//

                _linkData.resetContData ();
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // (Upper-case) sequence Terminator character not found       //
                // before end of buffer.                                      //
                // Initiate continuation action.                              //
                //                                                            //
                //------------------------------------------------------------//

                Int32 dataLen;

                if (p_or_TCharFound)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // (Lower-case) Parameter character just encountered;     //
                    // buffer exhausted before any value bytes for the next   //
                    // part of the combination sequence.                      //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (seqProprietary)
                    {
                        contType = PrnParseConstants.eContType.PCLExtension;
                        dataLen = bufRem;
                    }
                    else
                    {
                        contType = PrnParseConstants.eContType.PCLComplex;
                        dataLen = bufRem;
                    }
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // At least one value byte, for the first or next part of //
                    // the complex sequence, has been encountered.            //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (!comboSeq)
                    {
                        contType = PrnParseConstants.eContType.Unknown;
                        dataLen = bufRem + processedLen;
                    }
                    else
                    {
                        contType = PrnParseConstants.eContType.PCLComplex;
                        dataLen = bufRem;
                    }
                }

                _linkData.setContData (contType,
                                       prefixLen,
                                       -dataLen,
                                       0,
                                       true,
                                       iChar,
                                       gChar);

                _linkData.PclComboSeq = comboSeq;

                bufRem = 0;
            }

            return invalidSeq;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a r s e S e q u e n c e C o m p l e x D i s p l a y              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Display details of the supplied sequence, in slices if necessary.  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void parseSequenceComplexDisplay(
            Int32 offset,
            Int32 prefixLen,
            Boolean firstPart,
            PrnParseConstants.eOvlShow makeOvlShow,
            String type,
            String sequence,
            String description,
            String value)
        {
            const String escText = "<Esc>";

            Boolean firstSlice;

            Int32 len,
                  opSeqFixLen,
                  opSeqOffset,
                  sliceLen,
                  sliceMax,
                  sliceOffset,
                  crntOffset;

            String opFixed,
                   typeText,
                   descText,
                   seqSlice;

            //----------------------------------------------------------------//
            //                                                                //
            // Display sequence (in slices if necessary).                     //
            //                                                                //
            //----------------------------------------------------------------//

            len = sequence.Length;
            firstSlice = true;
            sliceOffset = 0;
            opSeqFixLen = escText.Length;
            descText = "";

            if (firstPart)
            {
                opSeqOffset = opSeqFixLen;
                opFixed = escText;
            }
            else
            {
                opSeqOffset = opSeqFixLen + prefixLen;
                opFixed = new String (' ', opSeqOffset);
            }

            sliceMax = PrnParseConstants.cRptA_colMax_Seq - opSeqOffset;

            while (len > 0)
            {
                if (len > sliceMax)
                //--------------------------------------------------------//
                //                                                        //
                // Not last slice.                                        //
                //                                                        //
                //--------------------------------------------------------//
                {
                    sliceLen = sliceMax;
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Last, or only, slice.                                  //
                    //                                                        //
                    //--------------------------------------------------------//

                    Int32 ptr = description.IndexOf ("#");

                    sliceLen = len;

                    if (ptr == -1)
                    {
                        descText = description;
                    }
                    else
                    {
                        if (value.Length == 0)
                        {
                            descText = description.Substring (0, ptr) +
                                      "0" +
                                      description.Substring (ptr + 1);
                        }
                        else
                        {
                            descText = description.Substring (0, ptr) +
                                       value +
                                       description.Substring (ptr + 1);
                        }
                    }
                }

                seqSlice = sequence.Substring (sliceOffset, sliceLen);

                if (firstSlice)
                {
                    crntOffset = offset + sliceOffset;
                    typeText = type;
                }
                else
                {
                    crntOffset = offset + sliceOffset + 1;  // WHY ????????????????//
                    typeText = "";
                }

                PrnParseCommon.addDataRow (
                    PrnParseRowTypes.eType.PCLSeqComplex,
                    _table,
                    makeOvlShow,
                    _indxOffsetFormat,
                    crntOffset,
                    _analysisLevel,
                    typeText,
                    opFixed + seqSlice,
                    descText);

                len = len - sliceLen;
                sliceOffset = sliceOffset + sliceLen;
                opSeqOffset = opSeqFixLen + prefixLen;

                sliceMax = PrnParseConstants.cRptA_colMax_Seq - opSeqOffset;

                opFixed = new String (' ', opSeqOffset);
                firstSlice = false;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a r s e S e q u e n c e E m b e d d e d D a t a                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Process data associated with a PCL parameterised sequence where    //
        // value field indicates the length of the (often binary) data        //
        // associated with, and following, the sequence parameter (or         //
        // terminator) character.                                             // 
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean parseSequenceEmbeddedData(
            ref Int32 bufRem,
            ref Int32 bufOffset,
            ref Int32 binDataLen,
            PrnParseConstants.eActPCL actType,
            Int32 startPos,
            Int32 endPos,
            Int32 prefixLen,
            Byte iChar,
            Byte gChar,
            Boolean seqComplete,
            ref Boolean invalidSeqFound)
        {
            Boolean continuation,
                    hddrOK,
                    charOK,
                    dataOK;

            Boolean dummyBool = false;

            Boolean analyseRun;

            Int32 downloadRem = 0;

            continuation = false;
            invalidSeqFound = false;

            if (binDataLen == 0)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Download sequence with zero length.                        //
                // (this does occur sometimes, especially with raster         //
                // graphics data).                                            //
                //                                                            //
                //------------------------------------------------------------//
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // Download sequence with non-zero length.                    //
                //                                                            //
                //------------------------------------------------------------//

                analyseRun = (_parseType == PrnParse.eParseType.Analyse);

                PrnParseConstants.eOvlShow ovlShow;

                if (_parseType == PrnParse.eParseType.MakeOverlay)
                    ovlShow = _linkData.MakeOvlShow;
                else
                    ovlShow = PrnParseConstants.eOvlShow.None;

                if ((analyseRun) &&
                    (actType == PrnParseConstants.eActPCL.FontHddr) &&
                    (_analyseFontHddr))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Sequence is a PCL font header download, and the option //
                    // to analyse it has been invoked.                        //
                    //                                                        //
                    //--------------------------------------------------------//

                    hddrOK = _parseFontHddrPCL.analyseFontHddr (
                                binDataLen,
                                 _fileOffset,
                                _buf,
                                ref bufRem,
                                ref bufOffset,
                                _linkData,
                                _options,
                                _table);

                    if (!hddrOK)
                        invalidSeqFound = true;

                    binDataLen = 0;

                    continuation = _linkData.isContinuation ();
                }
                else if ((analyseRun) &&
                         (actType == PrnParseConstants.eActPCL.FontChar) &&
                         (_analyseFontChar))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Sequence is a PCL font character download, and the     //
                    // option to analyse it has been invoked.                 //
                    //                                                        //
                    //--------------------------------------------------------//

                    charOK = _parseFontCharPCL.analyseFontChar (binDataLen,
                                                               _fileOffset,
                                                               _buf,
                                                               ref bufRem,
                                                               ref bufOffset,
                                                               _linkData,
                                                               _options,
                                                               _table);

                    if (!charOK)
                        invalidSeqFound = true;

                    binDataLen = 0;

                    continuation = _linkData.isContinuation ();
                }
                else if ((analyseRun) &&
                         (actType == PrnParseConstants.eActPCL.AlphaNumericID) &&
                         (_transAlphaNumId))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Sequence is the AlphaNumericID command:                //
                    //      <esc>&n#W[op-char][string-data]                   //
                    // We must have, at least, the op-code character (as the  //
                    // binary data length is non-zero).                       //
                    //                                                        //
                    //--------------------------------------------------------//

                    dataOK = _parsePCLBinary.decodeAlphaNumericID (
                        binDataLen,
                        _fileOffset,
                        _buf,
                        ref bufRem,
                        ref bufOffset,
                        _linkData,
                        _options,
                        _table);

                    if (!dataOK)
                        invalidSeqFound = true;

                    binDataLen = 0;

                    continuation = _linkData.isContinuation();
                }
                else if ((analyseRun) &&
                         (actType == PrnParseConstants.eActPCL.ColourLookup) &&
                         (_transColourLookup))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Sequence is the Colour Lookup Tables command:          //
                    //      <esc>*l#W[binary]                                 //
                    //                                                        //
                    //--------------------------------------------------------//

                    dataOK = _parsePCLBinary.decodeColourLookup (
                        binDataLen,
                        _fileOffset,
                        _buf,
                        ref bufRem,
                        ref bufOffset,
                        _linkData,
                        _options,
                        _table);

                    if (!dataOK)
                        invalidSeqFound = true;

                    binDataLen = 0;

                    continuation = _linkData.isContinuation();
                }
                else if ((analyseRun) &&
                         (actType == PrnParseConstants.eActPCL.ConfigurationIO) &&
                         (_transConfIO))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Sequence is the Configuration (I/O) command:           //
                    //      <esc>&b#W[binary]                                 //
                    //                                                        //
                    //--------------------------------------------------------//

                    dataOK = _parsePCLBinary.decodeConfigurationIO (
                        binDataLen,
                        _fileOffset,
                        _buf,
                        ref bufRem,
                        ref bufOffset,
                        _linkData,
                        _options,
                        _table);

                    if (!dataOK)
                        invalidSeqFound = true;

                    binDataLen = 0;

                    continuation = _linkData.isContinuation();
                }
                else if ((analyseRun) &&
                         (actType == PrnParseConstants.eActPCL.ConfigureImageData) &&
                         (_transConfImageData))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Sequence is the ConfigureImageData command:            //
                    //      <esc>*v#W[binary-data]                            //
                    // The common 'short form' uses 6 bytes of binary data;   //
                    // there are various long forms.                          //
                    //                                                        //
                    //--------------------------------------------------------//

                    dataOK = _parsePCLBinary.decodeConfigureImageData (
                        binDataLen,
                        _fileOffset,
                        _buf,
                        ref bufRem,
                        ref bufOffset,
                        _linkData,
                        _options,
                        _table);

                    if (!dataOK)
                        invalidSeqFound = true;

                    binDataLen = 0;

                    continuation = _linkData.isContinuation();
                }
                else if ((analyseRun) &&
                         (actType == PrnParseConstants.eActPCL.ConfigureRasterData) &&
                         (_transConfRasterData))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Sequence is the ConfigureRasterData command:           //
                    //      <esc>*g#W[binary-data]                            //
                    // Similar purpose to the Configure Image Data sequence,  //
                    // but used with inkjet devices (PCL3 enhanced languages).//
                    // There are various formats 9not all of which are known  //
                    // here).                                                 //
                    //                                                        //
                    //--------------------------------------------------------//

                    dataOK = _parsePCLBinary.decodeConfigureRasterData (
                        binDataLen,
                        _fileOffset,
                        _buf,
                        ref bufRem,
                        ref bufOffset,
                        _linkData,
                        _options,
                        _table);

                    if (!dataOK)
                        invalidSeqFound = true;

                    binDataLen = 0;

                    continuation = _linkData.isContinuation();
                }
                else if ((analyseRun) &&
                         (actType == PrnParseConstants.eActPCL.LogicalPageData) &&
                         (_transDefLogPage))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Sequence is the Define Logical Page command:           //
                    //      <esc>&a#W[binary-data]                            //
                    // Data length should be 4 or 10 bytes.                   //
                    //                                                        //
                    //--------------------------------------------------------//

                    dataOK = _parsePCLBinary.decodeDefineLogicalPage (
                        binDataLen,
                        _fileOffset,
                        _buf,
                        ref bufRem,
                        ref bufOffset,
                        _linkData,
                        _options,
                        _table);

                    if (!dataOK)
                        invalidSeqFound = true;

                    binDataLen = 0;

                    continuation = _linkData.isContinuation();
                }
                else if ((analyseRun) &&
                         (actType == PrnParseConstants.eActPCL.DefineSymbolSet) &&
                         (_transDefSymSet))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Sequence is the Define Symbol Set command:             //
                    //      <esc>(f#W[binary-data]                            //
                    //                                                        //
                    //--------------------------------------------------------//

                    dataOK = _parsePCLBinary.decodeDefineSymbolSet(
                        binDataLen,
                        _fileOffset,
                        _buf,
                        ref bufRem,
                        ref bufOffset,
                        _linkData,
                        _options,
                        _table);

                    if (!dataOK)
                        invalidSeqFound = true;

                    binDataLen = 0;

                    continuation = _linkData.isContinuation();
                }
                else if ((analyseRun) &&
                         (actType == PrnParseConstants.eActPCL.DitherMatrix) &&
                         (_transDitherMatrix))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Sequence is the Download Dither Matrix command:        //
                    //      <esc>*m#W[binary]                                 //
                    //                                                        //
                    //--------------------------------------------------------//

                    dataOK = _parsePCLBinary.decodeDitherMatrix (
                        binDataLen,
                        _fileOffset,
                        _buf,
                        ref bufRem,
                        ref bufOffset,
                        _linkData,
                        _options,
                        _table);

                    if (!dataOK)
                        invalidSeqFound = true;

                    binDataLen = 0;

                    continuation = _linkData.isContinuation();
                }
                else if ((analyseRun) &&
                         (actType == PrnParseConstants.eActPCL.DriverConfiguration) &&
                         (_transDriverConf))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Sequence is the Driver Configuration command:          //
                    //      <esc>*o#W[binary-data]                            //
                    //                                                        //
                    //--------------------------------------------------------//

                    dataOK = _parsePCLBinary.decodeDriverConfiguration (
                        binDataLen,
                        _fileOffset,
                        _buf,
                        ref bufRem,
                        ref bufOffset,
                        _linkData,
                        _options,
                        _table);

                    if (!dataOK)
                        invalidSeqFound = true;

                    binDataLen = 0;

                    continuation = _linkData.isContinuation ();
                }
                else if ((analyseRun) &&
                         (actType == PrnParseConstants.eActPCL.EscEncText) &&
                         (_transEscEncText))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Sequence is the Escapement Encapsulated Text command:  //
                    //      <esc>&p#W[binary]                                 //
                    //                                                        //
                    //--------------------------------------------------------//

                    dataOK = _parsePCLBinary.decodeEscEncText (
                        binDataLen,
                        _fileOffset,
                        _buf,
                        ref bufRem,
                        ref bufOffset,
                        _linkData,
                        _options,
                        _table);

                    if (!dataOK)
                        invalidSeqFound = true;

                    binDataLen = 0;

                    continuation = _linkData.isContinuation();
                }
                else if ((analyseRun) &&
                         (actType == PrnParseConstants.eActPCL.PaletteConfiguration) &&
                         (_transPaletteConf))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Sequence is the Palette Configuration command:         //
                    //      <esc>&a#W[binary-data]                            //
                    // Data length is variable.                               //
                    //                                                        //
                    //--------------------------------------------------------//

                    dataOK = _parsePCLBinary.decodePaletteConfiguration (
                        binDataLen,
                        _fileOffset,
                        _buf,
                        ref bufRem,
                        ref bufOffset,
                        _linkData,
                        _options,
                        _table);

                    if (!dataOK)
                        invalidSeqFound = true;

                    binDataLen = 0;

                    continuation = _linkData.isContinuation();
                }
                else if ((analyseRun) &&
                         (actType == PrnParseConstants.eActPCL.UserDefinedPattern) &&
                         (_transUserPattern))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Sequence is the User Defined Pattern command:          //
                    //      <esc>*c#W[data]                                   //
                    //                                                        //
                    //--------------------------------------------------------//

                    dataOK = _parsePCLBinary.decodeUserDefinedPattern(
                        binDataLen,
                        _fileOffset,
                        _buf,
                        ref bufRem,
                        ref bufOffset,
                        _linkData,
                        _options,
                        _table);

                    if (!dataOK)
                        invalidSeqFound = true;

                    binDataLen = 0;

                    continuation = _linkData.isContinuation();
                }
                else if ((analyseRun) &&
                         (actType == PrnParseConstants.eActPCL.ViewIlluminant) &&
                         (_transViewIlluminant))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Sequence is the Viewing Illuminant command:            //
                    //      <esc>*i#W[binary]                                 //
                    //                                                        //
                    //--------------------------------------------------------//

                    dataOK = _parsePCLBinary.decodeViewIlluminant (
                        binDataLen,
                        _fileOffset,
                        _buf,
                        ref bufRem,
                        ref bufOffset,
                        _linkData,
                        _options,
                        _table);

                    if (!dataOK)
                        invalidSeqFound = true;

                    binDataLen = 0;

                    continuation = _linkData.isContinuation();
                }
                else if ((analyseRun) &&
                         (actType == PrnParseConstants.eActPCL.ConfigurationIO) &&
                         (_analysePML))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Download contains data which may include an embedded   //
                    // PML sequence.                                          //
                    //                                                        //
                    //--------------------------------------------------------//

                    dataOK = _parsePCLBinary.decodeEmbeddedPML(
                        binDataLen,
                        _fileOffset,
                        _buf,
                        ref bufRem,
                        ref bufOffset,
                        _linkData,
                        _options,
                        _table);

                    if (!dataOK)
                        invalidSeqFound = true;

                    binDataLen = 0;

                    continuation = _linkData.isContinuation();
                }
                else if ((analyseRun) &&
                         (actType == PrnParseConstants.eActPCL.EmbeddedData))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Download contains data which is to be displayed as     //
                    // text, as far as possible.                              //
                    // A typical example of this is the data associated with  //
                    // the Transparent Print command:                         //
                    //      <esc>&p#X[data]                                   //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (binDataLen > bufRem)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Not all of string identifier is in current buffer. //
                        // Initiate continuation action.                      //
                        // We should only do this simple continuation if the  //
                        // length of the data will fit within a 'read block'. //
                        // TODO - *** need to cater for data longer than ***  //
                        //        *** buffer ?? check this out           ***  //
                        //                                                    //
                        //----------------------------------------------------//

                        continuation = true;

                        _linkData.setContData (
                            PrnParseConstants.eContType.PCLEmbeddedData,
                            0,
                            -bufRem,
                            binDataLen,
                            true,
                            0x20,
                            0x20);
                    }
                    else
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // All of string identifier is in current buffer.     //
                        // Display details.                                   //
                        //                                                    //
                        //----------------------------------------------------//

                        PrnParseData.processLines (
                            _table,
                            ovlShow,
                            _linkData,
                            ToolCommonData.ePrintLang.Unknown,
                            _buf,
                            _fileOffset,
                            binDataLen,
                            ref bufRem,
                            ref bufOffset,
                            ref dummyBool,
                            true,
                            false,
                            true,
                            PrnParseConstants.asciiEsc,
                            "Embedded data",
                            0,
                            _indxCharSetSubAct,
                            (Byte) _valCharSetSubCode,
                            _indxCharSetName,
                            _indxOffsetFormat,
                            _analysisLevel);

                        binDataLen = 0;
                    }
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // No special sequence processing invoked, or not an      //
                    // Analyse run.                                           //
                    //                                                        //
                    //--------------------------------------------------------//

                    if ((startPos + binDataLen) > endPos)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // No special sequence processing invoked.            //
                        // Sequence straddles 'block' boundary.               //
                        // Initiate continuation action.                      //
                        //                                                    //
                        //----------------------------------------------------//

                        continuation = true;

                        downloadRem = binDataLen;
                        binDataLen = endPos - startPos;
                        downloadRem = downloadRem - binDataLen;

                        if (!seqComplete)
                        {
                            _linkData.setContData (
                                PrnParseConstants.eContType.PCLDownloadCombo,
                                prefixLen,
                                0,
                                downloadRem,
                                false,
                                iChar,
                                gChar);

                            _linkData.PclComboSeq = true;
                        }
                        else
                        {
                            _linkData.setContData (
                                PrnParseConstants.eContType.PCLDownload,
                                0,
                                0,
                                downloadRem,
                                false,
                                0x20,
                                0x20);

                            _linkData.PclComboSeq = false;
                        }
                    }

                    if (binDataLen == 0)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // None of the download data is contained within the  //
                        // current 'read block'.                              //
                        // The necessary Continuation processing has already  //
                        // been signalled.                                    //
                        //                                                    //
                        //----------------------------------------------------//
                    }
                    else
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Some, or all, of the download data is contained    //
                        // within the current 'read block'.                   //
                        //                                                    //
                        //----------------------------------------------------//

                        PrnParseData.processBinary (
                            _table,
                            ovlShow,
                            _buf,
                            _fileOffset,
                            bufOffset,
                            binDataLen,
                            "PCL Binary",
                            _showBinData,
                            false,
                            true,
                            _indxOffsetFormat,
                            _analysisLevel);
                    }
                }
            }

            return continuation;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a r s e S e q u e n c e S i m p l e                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Format of simple sequences:                                        //
        //                                                                    //
        //      byte     Escape character (x'1B')                             //
        //      byte     Indicator character               (I_CHAR)           //
        //                                                                    //
        // Each table entry matches an I_CHAR value against a description     //
        // Note that any 'switch_language' sequences may be processed as      //
        // Special Escape Sequences (provided the current language allows)    //
        // rather than as Simple Escape Sequences.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean parseSequenceSimple(
            ref Int32 bufRem,
            ref Int32 bufOffset,
            ref Boolean breakpoint)
        {
            Byte iChar;

            bool seqKnown = false;
            bool optObsolete = false;
            bool optResetHPGL2 = false;

            PrnParseConstants.eOvlAct makeOvlAct =
                PrnParseConstants.eOvlAct.None;

            PrnParseConstants.eOvlShow makeOvlShow =
                PrnParseConstants.eOvlShow.None;

            String descSimple = "";

            iChar = _buf[bufOffset + 1];

            seqKnown = PCLSimpleSeqs.checkSimpleSeq (
                (_analysisLevel + _macroLevel),
                iChar,
                ref optObsolete,
                ref optResetHPGL2,
                ref makeOvlAct,
                ref descSimple);

            if (seqKnown)
            {
                if (optResetHPGL2)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Sequence resets HP-GL/2 state variables.               //
                    //                                                        //
                    //--------------------------------------------------------//

                    //        _HPGL2Analysis->ResetHPGL2();
                }

                if (optObsolete)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Sequence marked as obsolete.                           //
                    //                                                        //
                    //--------------------------------------------------------//

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgComment,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Comment",
                        "",
                        "The following sequence is considered to be obsolete:");
                }

                if (_parseType == PrnParse.eParseType.MakeOverlay)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // MakeMacro action indicated.                            //
                    //                                                        //
                    //--------------------------------------------------------//

                    _linkData.MakeOvlAct = makeOvlAct;

                    breakpoint = PrnParseMakeOvl.checkActionPCL (
                                    false,
                                    true,
                                    -1,
                                    bufOffset,
                                    2,
                                    _fileOffset,
                                    _linkData,
                                    _table,
                                    _indxOffsetFormat);

                    makeOvlAct = _linkData.MakeOvlAct;
                    makeOvlShow = _linkData.MakeOvlShow;
                }
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Output details and interpretation of the sequence.             //
            //                                                                //
            //----------------------------------------------------------------//

            PrnParseCommon.addDataRow (
                PrnParseRowTypes.eType.PCLSeqSimple,
                _table,
                makeOvlShow,
                _indxOffsetFormat,
                _fileOffset + bufOffset,
                _analysisLevel,
                "PCL Simple",
                "<Esc>" + (Char) iChar,
                descSimple);

            bufRem = bufRem - 2;
            bufOffset = bufOffset + 2;

            return seqKnown;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s S t y l e D a t a                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Dispays an interpretation of the components of the style value.    //
        //                                                                    //
        // Bit numbers are zero-indexed from (left) Most Significant:         //
        //                                                                    //
        //    bits  0  -  5   Reserved                                        //
        //          6  - 10   Structure  (e.g. Solid)                         //
        //         11  - 13   Width      (e.g. Condensed)                     //
        //         14  - 15   Posture    (e.g. Italic)                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processStyleData(Int32 style)
        {
            Int32 index;

            String itemDesc;

            index = (style >> 5) & 0x1f;

            switch (index)
            {
                case 0:
                    itemDesc = "0: Solid (Normal, Black)";
                    break;

                case 1:
                    itemDesc = "32: Outline (Hollow)";
                    break;

                case 2:
                    itemDesc = "64: Inline (Incised, Engraved)";
                    break;

                case 3:
                    itemDesc = "96: Contour, Edged (Antique, Distressed)";
                    break;

                case 4:
                    itemDesc = "128: Solid with Shadow";
                    break;

                case 5:
                    itemDesc = "160: Outline with Shadow";
                    break;

                case 6:
                    itemDesc = "192: Inline with Shadow";
                    break;

                case 7:
                    itemDesc = "224: Contour, or Edged, with Shadow";
                    break;

                case 8:
                    itemDesc = "256: Pattern Filled";
                    break;

                case 9:
                    itemDesc = "288: Pattern Filled 1";
                    break;

                case 10:
                    itemDesc = "320: Pattern Filled 2";
                    break;

                case 11:
                    itemDesc = "352: Pattern Filled 3";
                    break;

                case 12:
                    itemDesc = "384: Pattern Filled with Shadow";
                    break;

                case 13:
                    itemDesc = "416: Pattern Filled with Shadow 1";
                    break;

                case 14:
                    itemDesc = "448: Pattern Filled with Shadow 2";
                    break;

                case 15:
                    itemDesc = "480: Pattern Filled with Shadow 3";
                    break;

                case 16:
                    itemDesc = "512: Inverse";
                    break;

                case 17:
                    itemDesc = "544: Inverse with Border";
                    break;

                default:
                    itemDesc = ">=576: Unknown (Reserved)";
                    break;
            }

            PrnParseCommon.addTextRow (
                PrnParseRowTypes.eType.PCLDecode,
                _table,
                PrnParseConstants.eOvlShow.None,
                "",
                "     ----> Structure",
                "",
                itemDesc);

            index = (style >> 2) & 0x07;

            switch (index)
            {
                case 0:
                    itemDesc = "0: Normal";
                    break;

                case 1:
                    itemDesc = "4: Condensed";
                    break;

                case 2:
                    itemDesc = "8: Compressed (Extra Condensed)";
                    break;

                case 3:
                    itemDesc = "12: Extra Compressed";
                    break;

                case 4:
                    itemDesc = "16: Ultra Compressed";
                    break;

                case 5:
                    itemDesc = "20: Unknown (Reserved)";
                    break;

                case 6:
                    itemDesc = "24: Expanded (Extended)";
                    break;

                case 7:
                    itemDesc = "28: Extra Expanded (Extra Extended)";
                    break;

                default:
                    itemDesc = ">=32: Impossible?";
                    break;
            }

            PrnParseCommon.addTextRow (
                PrnParseRowTypes.eType.PCLDecode,
                _table,
                PrnParseConstants.eOvlShow.None,
                "",
                "     ----> Width",
                "",
                itemDesc);
            
            index = style & 0x03;

            switch (index)
            {
                case 0:
                    itemDesc = "0: Upright";
                    break;

                case 1:
                    itemDesc = "1: Oblique, Italic";
                    break;

                case 2:
                    itemDesc = "2: Alternate Italic (Backslanted, Cursive, Swash)";
                    break;

                case 3:
                    itemDesc = "3: Unknown (Reserved)";
                    break;

                default:
                    itemDesc = ">=4: Impossible?";
                    break;
            }

            PrnParseCommon.addTextRow (
                PrnParseRowTypes.eType.PCLDecode,
                _table,
                PrnParseConstants.eOvlShow.None,
                "",
                "     ----> Posture",
                "",
                itemDesc);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e s e t P C L                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Resets PCL state variables.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void resetPCL()

        {
           _textParsingMethod =
                (Int32) PCLTextParsingMethods.ePCLVal.m0_1_byte_default;
           _macroLevel        = 0;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t T a b l e                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set Datatable target (used by Make Overlay inserts).               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void setTable(DataTable table)
        {
            _table = table;
        }
    }
}