using System;
using System.Data;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class handles PCL XL downloadable soft font headers.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    class PrnParseFontHddrPCLXL
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private enum ePCLXLCharFormat : byte
        {
            Bitmap             = 0,
            TrueType           = 1
        }

        private enum ePCLXLFontFormat : byte
        {
            Download           = 0
        }

        private enum ePCLXLFontTechnology : byte
        {
            TrueType           = 1,
            Bitmap             = 254
        }

        private enum eStage
        {
            Start = 0,
            ShowHddr,
            ShowData,
            EndOK,
            BadSeqA,
            BadSeqB
        }

        const Int32 _cHddrDescLen = 8; // Format 0 header
        const Int32 _cSegHddrLen  = 6; // type (2) + size (4)

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private PrnParseOptions _options;

        private PrnParseFontSegs _parseSegs;

        private DataTable _table;

        private eStage _nextStage;
        private ePCLXLFontFormat _hddrFormat;

        private Byte[] _buf;

        private Int32 _fileOffset;
        private Int32 _analysisLevel;

        private Boolean _validHddr;
        private Boolean _firstSeg;

        private Int32 _hddrLen;
        private Int32 _hddrRem;
        private Int32 _hddrPos;
        private Int32 _hddrDataLen;
        private Int32 _hddrDataRem;

        private Boolean _showBinData;

        private PrnParseConstants.eOptOffsetFormats _indxOffsetFormat;
        
        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P r n P a r s e F o n t H d d r P C L X L                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PrnParseFontHddrPCLXL()
        {
            _parseSegs = new PrnParseFontSegs();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // a n a l y s e F o n t H d d r                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Provide an interpretation of the contents of a PCL XL soft font    //
        // header.                                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean analyseFontHddr(Int32            hddrLen,
                                       Byte []          buf,
                                       Int32            fileOffset,
                                       ref Int32        bufRem,
                                       ref Int32        bufOffset,
                                       PrnParseLinkData linkData,
                                       PrnParseOptions  options,
                                       DataTable        table)
        {
            Int32 binDataLen;
            Boolean validSegs = false;
            
            PrnParseConstants.eContType contType;

            Boolean continuation = false;

            //----------------------------------------------------------------//
            //                                                                //
            // Initialise.                                                    //
            //                                                                //
            //----------------------------------------------------------------//
            
            _table = table;
            _buf = buf;
            _fileOffset = fileOffset;

            _analysisLevel = linkData.AnalysisLevel;

            _options = options;
            
            contType = linkData.getContType();

            _indxOffsetFormat = _options.IndxGenOffsetFormat;

            _showBinData = _options.FlagPCLXLMiscBinData;

            //----------------------------------------------------------------//

            if (contType == PrnParseConstants.eContType.None)
            {
                _nextStage = eStage.Start;
                _validHddr = true;
                _firstSeg = true;

                _hddrLen = hddrLen;
                _hddrRem = hddrLen;
                _hddrPos = fileOffset + bufOffset;
            }
            else
            {
                contType = PrnParseConstants.eContType.None;
                linkData.resetContData();
            }

            //----------------------------------------------------------------//

            if (_nextStage == eStage.Start)
            {
                if (bufRem < _cHddrDescLen)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Font header descriptor is not all in buffer.           //
                    // Initiate continuation.                                 //
                    //                                                        //
                    //--------------------------------------------------------//

                    contType = PrnParseConstants.eContType.PCLXLFontHddr;

                    linkData.setBacktrack (contType, - bufRem);
                }
                else
                {
                        _nextStage = eStage.ShowHddr;
                }
            }

            if (_nextStage == eStage.ShowHddr)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Process font header.                                       //
                //                                                            //
                //------------------------------------------------------------//

                processFontHeader (ref bufRem,
                                   ref bufOffset);

                bufRem    = bufRem    - _cHddrDescLen;
                _hddrRem  = _hddrRem  - _cHddrDescLen;
                bufOffset = bufOffset + _cHddrDescLen;

                if (_validHddr)
                    _nextStage = eStage.ShowData;
                else
                    _nextStage = eStage.BadSeqA;
            }

            if (_nextStage == eStage.ShowData)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Output details of segmented data:                          //
                //                                                            //
                //------------------------------------------------------------//
                
                Int32 dummy = 0;

                continuation = _parseSegs.processSegData (_buf,
                                                          _fileOffset,
                                                          false,
                                                          _firstSeg,
                                                          true,
                                                          ref bufRem,
                                                          ref bufOffset,
                                                          ref _hddrDataRem,
                                                          ref _hddrRem,
                                                          ref dummy,
                                                          ref validSegs,
                                                          linkData,
                                                          _options,
                                                          _table);

                _firstSeg = false;
            }

            if (_nextStage == eStage.EndOK)
            {
                //------------------------------------------------------------//
                //                                                            //
                // End of processing of valid header.                         //
                //                                                            //
                //------------------------------------------------------------//

                return _validHddr;
            }

            if (_nextStage == eStage.BadSeqA)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Inconsistency found.                                       //
                //                                                            //
                //------------------------------------------------------------//

                _nextStage = eStage.BadSeqB;

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.MsgError,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "",
                    "",
                    "Processing of header abandoned!");
            }

            if ((_nextStage == eStage.BadSeqB) && (_hddrRem != 0))
            {
                //------------------------------------------------------------//
                //                                                            //
                // Header does not appear to be valid.                        //
                // Treat remainder of header as a binary sequence without     //
                // interpretation.                                            //
                // Check if remainder of download sequence is within the      //
                // buffer.                                                    //
                //                                                            //
                //------------------------------------------------------------//

                if (_hddrRem > bufRem)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Remainder of sequence is not in buffer.                //
                    // Initiate continuation.                                 //
                    //                                                        //
                    //--------------------------------------------------------//

                    contType = PrnParseConstants.eContType.PCLXLFontHddr;

                    binDataLen = bufRem;
                    _hddrRem   = _hddrRem - bufRem;

                    linkData.setContinuation (contType);
                }
                else
                {
                    contType = PrnParseConstants.eContType.None;
                    linkData.resetContData();

                    binDataLen = _hddrRem;
                    _hddrRem = 0;
                }

                if ((binDataLen) != 0)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Some, or all, of the download data is contained within //
                    // the current 'block'.                                   //
                    //                                                        //
                    //--------------------------------------------------------//

                    PrnParseData.processBinary (
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        buf,
                        fileOffset,
                        bufOffset,
                        binDataLen,
                        "PCLXL Binary",
                        _showBinData,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);

                    bufRem    = bufRem - binDataLen;
                    bufOffset = bufOffset + binDataLen;
                }
            }

            return _validHddr;
        }
        
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s F o n t H e a d e r                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Provide an interpretation of the contents of the initial part of   //
        // the font header.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processFontHeader(ref Int32 bufLen,
                                       ref Int32 bufOffset)
        {
            Char c;
            
            Int32 ix1,
                  ix2,
                  ix3;

            Int32 indxSymSet;

            String itemDesc;

            //----------------------------------------------------------------//
            //                                                                //
            // Show size and (optionally) data.                               //
            //                                                                //
            //----------------------------------------------------------------//

            PrnParseCommon.addDataRow (
                PrnParseRowTypes.eType.DataBinary,
                _table,
                PrnParseConstants.eOvlShow.None,
                _indxOffsetFormat,
                _fileOffset + bufOffset,
                _analysisLevel,
                "PCLXL Binary",
                "[ " + _cHddrDescLen.ToString() + " bytes ]",
                "Font header");
            
            if (_showBinData)
            {
                PrnParseData.processBinary(
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _buf,
                    _fileOffset,
                    bufOffset,
                    _cHddrDescLen,
                    "",
                    _showBinData,
                    false,
                    true,
                    _indxOffsetFormat,
                    _analysisLevel);
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Determine the Header Format, and from this determine the       //
            // position and size of parts of the header following the         //
            // Descriptor.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            _hddrFormat  = (ePCLXLFontFormat) _buf[bufOffset];
            
            if (_hddrFormat == ePCLXLFontFormat.Download)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Format 0 - Only format supported (as at v3.0).             //
                //                                                            //
                //------------------------------------------------------------//

                _hddrDataLen = _hddrLen - _cHddrDescLen;
                _hddrDataRem = _hddrDataLen;
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // Unknown format.                                            //
                //                                                            //
                //------------------------------------------------------------//

                _validHddr = false;

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.MsgWarning,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "*** Warning ***",
                    "",
                    "Header format (" + _hddrFormat + ") is not recognised");
            }
            
            if (!_validHddr)
            {
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.MsgWarning,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "*** Warning ***",
                    "",
                    "Processing of Font Header abandoned");
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // Header format (byte 0).                                    //
                //                                                            //
                //------------------------------------------------------------//

                switch (_hddrFormat)
                {
                    case ePCLXLFontFormat.Download:
                        itemDesc = "0: PCLXL Download";
                        break;

                    default:
                        itemDesc = _hddrFormat + ": Unknown";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLXLFontHddr,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Header Format:",
                    "",
                    itemDesc);

                //------------------------------------------------------------//
                //                                                            //
                // Orientation (byte 1).                                      //
                //                                                            //
                //    0 = Portrait                                            //
                //    1 = Landscape                                           //
                //    2 = Reverse Portrait                                    //
                //    3 = Reverse Landscape                                   //
                //                                                            //
                //------------------------------------------------------------//

                ix1  = _buf[bufOffset + 1];

                switch (ix1)
                {
                    case 0:
                        itemDesc = "0: Portrait";
                        break;

                    case 1:
                        itemDesc = "1: Landscape";
                        break;

                    case 2:
                        itemDesc = "2: Reverse Portrait";
                        break;

                    case 3:
                        itemDesc = "3: Reverse Landscape";
                        break;

                    default:
                        itemDesc = ix1 + ": Unknown";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLXLFontHddr,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Orientation:",
                    "",
                    itemDesc);

                //------------------------------------------------------------//
                //                                                            //
                // Mapping (bytes 2-3).                                       //
                //                                                            //
                // Two part: 11-bit number (binary).                          //
                //            5-bit letter-code: add 64 to this to obtain the //
                //                  (ASCII) character-code of the letter.     //
                //                                                            //
                // e.g. value of  0x000E --> 0N                               //
                //                0x0115 --> 8U                               //
                //                0x0155 --> 10U                              //
                //                0x01F1 --> 15Q                              //
                //                                                            //
                // Symbol sets where the letter part equates to 'Q' are       //
                // reserved for 'Specials'.                                   //
                //                                                            //
                //------------------------------------------------------------//

                ix1 = (_buf[bufOffset + 2] * 256) + _buf[bufOffset + 3];

                ix2 = ix1 >> 5;
                ix3 = (ix1 & 0x1f) + 64;
                c = (Char) ix3;

                indxSymSet =
                    PCLSymbolSets.getIndexForId ((UInt16)ix1);
 
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLXLFontHddr,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Mapping:",
                    "Kind1 value:",
                    ix1.ToString () + " (0x" + ix1.ToString ("x2") + ")");

                if ((indxSymSet) == -1)
                {
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLXLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "     ---->",
                        "Identifier:",
                        ix2.ToString () + c);
                }
                else
                {
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLXLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "     ---->",
                        "Identifier:",
                        ix2.ToString () + c +
                        " (" + PCLSymbolSets.getName (indxSymSet) + ")");
                }

                //------------------------------------------------------------//
                //                                                            //
                // Scaling Technology (byte 4).                               //
                //         Variety    (byte 5).    Expected to be zero.       //
                //                                                            //
                //------------------------------------------------------------//

                ePCLXLFontTechnology scaling =
                    (ePCLXLFontTechnology) _buf[bufOffset + 4];
                ix2 = _buf[bufOffset + 5];

                switch (scaling)
                {
                    case ePCLXLFontTechnology.TrueType:
                        itemDesc = "1: TrueType";
                        break;

                    case ePCLXLFontTechnology.Bitmap:
                        itemDesc = "254: Bitmap";
                        break;

                    default:
                        itemDesc = scaling.ToString() + ": Unknown";
                        break;
                }
 
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLXLFontHddr,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Scaling:",
                    "Technology:",
                    itemDesc);

 
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLXLFontHddr,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "",
                    "Variety:",
                    ix2.ToString());

                //----------------------------------------------------------------------//
                //                                                                      //
                // Number of Characters (bytes 6-7).                                    //
                //                                                                      //
                //----------------------------------------------------------------------//

                ix1 = (_buf[bufOffset + 6] * 256) + _buf[bufOffset + 7];

 
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLXLFontHddr,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Character Count:",
                    "",
                    ix1.ToString());
            }
        }
    }
}