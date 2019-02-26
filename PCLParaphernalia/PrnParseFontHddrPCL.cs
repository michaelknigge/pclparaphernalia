using System;
using System.Data;
using System.Text;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class handles PCL downloadable soft font headers.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    class PrnParseFontHddrPCL
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private enum ePCLFontFormat : byte
        {
            Bitmap             = 0,
            IntellifontBound   = 10,
            IntellifontUnbound = 11,
            TrueType           = 15,
            Universal          = 16,
            BitmapResSpec      = 20
        }

        private enum eStage
        {
            Start = 0,
            ShowDesc,
            ShowData,
            ShowCopyright,
            ShowChecksum,
            EndOK,
            BadSeqA,
            BadSeqB
        }

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private PrnParseLinkData _linkData;
        private PrnParseOptions _options;

        private PrnParseFontSegs _parseSegs;

        private DataTable _table;

        private eStage _nextStage;
        private ePCLFontFormat _hddrFormat;

        private Byte[] _buf;

        private Int32 _fileOffset;
        private Int32 _analysisLevel;

        private Boolean _validHddr;
        private Boolean _firstSeg;

        private Boolean _bitmapFont;
        private Boolean _intelliFont;
        private Boolean _truetypeFont;
        private Boolean _boundFont;

        private Int32 _pclDotResX;
        private Int32 _pclDotResY;
        private Int32 _fontType;
        private Int32 _fontScaleFactor;

        private Int32 _hddrLen;
        private Int32 _hddrRem;
        private Int32 _hddrPos;
        private Int32 _hddrChksLen;
        private Int32 _hddrChksPos;
        private Int32 _hddrChksVal;
        private Int32 _hddrCpyrLen;
        private Int32 _hddrCpyrPos;
        private Int32 _hddrCpyrRem;
        private Int32 _hddrDataLen;
        private Int32 _hddrDataPos;
        private Int32 _hddrDataRem;
        private Int32 _hddrDescLen;
        private Int32 _hddrResvLen;
        private Int32 _hddrResvPos;

        private Boolean _showBinData;

        private PrnParseConstants.eOptOffsetFormats _indxOffsetFormat;
        
        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P r n P a r s e F o n t H d d r P C L                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PrnParseFontHddrPCL()
        {
            _parseSegs = new PrnParseFontSegs();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // a n a l y s e F o n t H d d r                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Provide an interpretation of the contents of a PCL soft font       //
        // header.                                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean analyseFontHddr(Int32            hddrLen,
                                       Int32            fileOffset,
                                       Byte []          buf,
                                       ref Int32        bufRem,
                                       ref Int32        bufOffset,
                                       PrnParseLinkData linkData,
                                       PrnParseOptions  options,
                                       DataTable        table)
        {
            const Int32 minHddrDescLen = 64;

            Int32 binDataLen;
            Boolean largeSegs;
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

            _linkData = linkData;
            _options = options;
            
            contType = _linkData.getContType();
            _analysisLevel = _linkData.AnalysisLevel;

            _indxOffsetFormat = _options.IndxGenOffsetFormat;
            _showBinData = _options.FlagPCLMiscBinData;

            //----------------------------------------------------------------//

            if (contType == PrnParseConstants.eContType.None)
            {
                _nextStage = eStage.Start;
                _validHddr = true;
                _firstSeg = true;

                _hddrLen = hddrLen;
                _hddrRem = hddrLen;
                _hddrPos = fileOffset + bufOffset;

                _hddrChksVal = 0;
            }
            else
            {
                contType = PrnParseConstants.eContType.None;
                linkData.resetContData();
            }

            //----------------------------------------------------------------//

            if (_nextStage == eStage.Start)
            {
                if (bufRem < 2)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // First two bytes (descriptor length) are not in buffer. //
                    // Initiate continuation.                                 //
                    //                                                        //
                    //--------------------------------------------------------//

                    contType = PrnParseConstants.eContType.PCLFontHddr;

                    linkData.setBacktrack (contType, - bufRem);
                }
                else
                {
                    _hddrDescLen = (buf[bufOffset] * 256) +
                                    buf[bufOffset + 1];

                    if (_hddrDescLen > _hddrLen)
                    {
                        _validHddr = false;
                        _nextStage = eStage.BadSeqA;

                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.MsgWarning,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "*** Warning ***",
                            "",
                            "Descriptor (size " + _hddrDescLen +
                            ") larger than header (size " + _hddrLen + ")");
                    }
                    else if (_hddrDescLen < minHddrDescLen)
                    {
                        _validHddr = false;
                        _nextStage = eStage.BadSeqA;

                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.MsgWarning,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "*** Warning ***",
                            "",
                            "Descriptor (size " + _hddrDescLen +
                            ") less than minimum (" + minHddrDescLen + ")");
                    }
                    else if (_hddrDescLen > bufRem)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Descriptor is not entirely within buffer.          //
                        // Initiate continuation.                             //
                        //                                                    //
                        //----------------------------------------------------//

                        contType = PrnParseConstants.eContType.PCLFontHddr;

                        linkData.setBacktrack (contType, - bufRem);
                    }
                    else
                    {
                        _nextStage = eStage.ShowDesc;
                    }
                }
            }

            if (_nextStage == eStage.ShowDesc)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Process font descriptor.                                   //
                //                                                            //
                //------------------------------------------------------------//

                processDescriptor(ref bufRem,
                                  ref bufOffset);

                bufRem    = bufRem    - _hddrDescLen;
                _hddrRem  = _hddrRem  - _hddrDescLen;
                bufOffset = bufOffset + _hddrDescLen;

                if (_validHddr)
                    _nextStage = eStage.ShowData;
                else
                    _nextStage = eStage.BadSeqA;
            }

            if (_nextStage == eStage.ShowData)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Output details of variable data:                           //
                //                                                            //
                // Format 10 - Intellifont Bound Scalable font.               //
                // Format 11 - Intellifont Unbound Scalable font.             //
                //    Global Intellifont Data                                 //
                //                                                            //
                // Format 15 - TrueType Scalable font.                        //
                // Format 16 - Universal (TrueType Scalable or Bitmap) font.  //
                //    Segmented Data                                          //
                //                                                            //
                //------------------------------------------------------------//

                if ((_hddrFormat == ePCLFontFormat.IntellifontBound)
                                         ||
                    (_hddrFormat == ePCLFontFormat.IntellifontUnbound))
                {
                    processGlobalData(ref bufRem,
                                      ref bufOffset);


                }
                else if ((_hddrFormat == ePCLFontFormat.TrueType)
                                        ||
                         (_hddrFormat == ePCLFontFormat.Universal))
                {
                    if (_hddrFormat == ePCLFontFormat.Universal)
                        largeSegs = true;
                    else
                        largeSegs = false;

                    continuation = _parseSegs.processSegData (_buf,
                                                              _fileOffset,
                                                              true,
                                                              _firstSeg,
                                                              largeSegs,
                                                              ref bufRem,
                                                              ref bufOffset,
                                                              ref _hddrDataRem,
                                                              ref _hddrRem,
                                                              ref _hddrChksVal,
                                                              ref validSegs,
                                                              _linkData,
                                                              _options,
                                                              _table);

                    _firstSeg = false;
                                        
                    if (!continuation)
                        if (validSegs)
                            _nextStage = eStage.ShowCopyright;
                        else
                            _nextStage = eStage.BadSeqA;
                }
                else
                {
                    _nextStage = eStage.ShowCopyright;
                }
            }

            if (_nextStage == eStage.ShowCopyright)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Output details of copyright data.                          //
                //                                                            //
                //------------------------------------------------------------//

                if (_hddrCpyrLen == 0)
                {
                    _nextStage = eStage.ShowChecksum;
                }
                else
                {
                    processCopyrightData(ref bufRem,
                                         ref bufOffset);
                }
            }

            if (_nextStage == eStage.ShowChecksum)
            {
                processChecksum (ref bufRem,
                                 ref bufOffset);
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

                    contType = PrnParseConstants.eContType.PCLFontHddr;

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
                        "PCL Binary",
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
        //                                                    P r o p e r t y //
        // P a r s e S e g s                                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PrnParseFontSegs ParseSegs
        {
            get { return _parseSegs; }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s C h e c k s u m                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Output details of Reserved byte and Checksum byte.                 //
        // The remainder length should be either 2 (if both are present) or   //
        // zero otherwise.                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processChecksum (ref Int32 bufRem,
                                      ref Int32 bufOffset)
        {
            PrnParseConstants.eContType contType;

            if (_hddrChksLen == 0)
            {
                if (_hddrRem != 0)
                {
                    _validHddr = false;
                    _nextStage = eStage.BadSeqA;

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgWarning,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "*** Warning ***",
                        "",
                        "Header is  internally inconsistent");
                }
            }
            else
            {
                if (_hddrRem != (_hddrResvLen + _hddrChksLen))
                {
                    _validHddr = false;
                    _nextStage = eStage.BadSeqA;

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgWarning,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "*** Warning ***",
                        "",
                        "Header is  internally inconsistent");
                }
                else
                {
                    if (_hddrRem > bufRem)
                    {
                        //------------------------------------------------//
                        //                                                //
                        // Remainder of sequence is not in buffer.        //
                        // Initiate continuation.                         //
                        //                                                //
                        //------------------------------------------------//

                        contType = PrnParseConstants.eContType.PCLFontHddr;

                        _linkData.setBacktrack (contType, -_hddrRem);
                    }
                    else
                    {
                        Byte crntByte;

                        contType = PrnParseConstants.eContType.None;
                        _linkData.resetContData ();

                        //------------------------------------------------//
                        //                                                //
                        // Display Reserved byte (should always be        //
                        // (binary) zero).                                //
                        //                                                //
                        //------------------------------------------------//

                        crntByte = _buf[bufOffset];

                        PrnParseCommon.addDataRow (
                            PrnParseRowTypes.eType.PCLFontHddr,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            _indxOffsetFormat,
                            _fileOffset + bufOffset,
                            _analysisLevel,
                            "Reserved byte",
                            "[ 1 byte ]",
                            "0x" +
                            PrnParseCommon.byteToHexString (crntByte));

                        _hddrChksVal += crntByte;

                        //------------------------------------------------//
                        //                                                //
                        // Display Checksum byte.                         //
                        // Verify that it matches the calculated value.   //
                        //                                                //
                        //------------------------------------------------//

                        crntByte = _buf[bufOffset + 1];

                        PrnParseCommon.addDataRow (
                            PrnParseRowTypes.eType.PCLFontHddr,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            _indxOffsetFormat,
                            _fileOffset + bufOffset + 1,
                            _analysisLevel,
                            "Checksum",
                            "[ 1 byte ]",
                            "0x" +
                            PrnParseCommon.byteToHexString (crntByte));

                        _hddrChksVal = (256 - (_hddrChksVal % 256)) % 256;

                        if (_hddrChksVal != crntByte)
                        {
                            crntByte = (Byte) _hddrChksVal;

                            PrnParseCommon.addTextRow (
                                PrnParseRowTypes.eType.MsgWarning,
                                _table,
                                PrnParseConstants.eOvlShow.None,
                                "",
                                "*** Warning ***",
                                "",
                                "Calculated checksum is 0x" +
                                PrnParseCommon.byteToHexString (crntByte));

                            _validHddr = false;
                        }

                        //------------------------------------------------//
                        //                                                //
                        // Adjust pointers.                               //
                        //                                                //
                        //------------------------------------------------//

                        bufRem = bufRem - _hddrRem;
                        bufOffset = bufOffset + _hddrRem;

                        _hddrRem = 0;
                        _nextStage = eStage.EndOK;
                    }
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s C o p y r i g h t D a t a                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Process Copyright Data.                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processCopyrightData(ref Int32 bufRem,
                                          ref Int32 bufOffset)
        {
            const Int32 sliceMax = 50;

            ASCIIEncoding ascii = new ASCIIEncoding ();
    
            PrnParseConstants.eContType contType;

            Int32 sliceLen;

            String typeText;

            Int32 remLen, 
                  dataLen,
                  offset;

            Boolean firstLine;

            if (_hddrCpyrRem > bufRem)
            {
                //------------------------------------------------------------//
                //                                                            //
                // All of copyright data is not in buffer.                    //
                // Initiate continuation.                                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = PrnParseConstants.eContType.PCLFontHddr;

                dataLen      = bufRem;
                _hddrCpyrRem = _hddrCpyrRem - bufRem;
                _hddrRem     = _hddrRem - bufRem;

                _linkData.setContinuation (contType);
            }
            else
            {
                contType = PrnParseConstants.eContType.None;
      
                _linkData.resetContData();

                dataLen      = _hddrCpyrRem;
                _hddrCpyrRem = 0;
                _hddrRem     = _hddrRem - dataLen;
                _nextStage   = eStage.ShowChecksum;
            }

            firstLine = true;
            remLen       = dataLen;
            offset    = bufOffset;

            if (_showBinData)
            {
                PrnParseData.processBinary (
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _buf,
                    _fileOffset,
                    bufOffset,
                    dataLen,
                    "Font Copyright",
                    _showBinData,
                    false,
                    true,
                    _indxOffsetFormat,
                    _analysisLevel);
            }

            for (Int32 i = 0; i < dataLen; i++)
            {
                _hddrChksVal += _buf[bufOffset + i];
            }

            while (remLen > 0)
            {
                if (remLen > sliceMax)
                    sliceLen = sliceMax;
                else
                    sliceLen = remLen;

                if (firstLine)
                    typeText = "Font Copyright";
                else
                    typeText = "";
            
                PrnParseCommon.addDataRow (
                    PrnParseRowTypes.eType.PCLFontHddr,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    _fileOffset + offset,
                    _analysisLevel,
                    typeText,
                    "",
                    ascii.GetString (_buf, offset, sliceLen));

                remLen -= sliceLen;
                offset += sliceLen;
                firstLine = false;
            }

            bufRem    = bufRem    - dataLen;
            bufOffset = bufOffset + dataLen;
        }
        
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s D e s c r i p t o r                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Provide an interpretation of the contents of the Font Descriptor   //
        // part of the header.                                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processDescriptor(ref Int32 bufLen,
                                       ref Int32 bufOffset)
        {
            ASCIIEncoding ascii = new ASCIIEncoding ();

            Char c;
            
            Int32 ix1,
                  ix2,
                  ix3;

            Int32 indxSymSet;

            Single fx1;

            Boolean pitchSet,
                    valOK;

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
                "PCL Binary",
                "[ " + _hddrDescLen.ToString() + " bytes ]",
                "Font header descriptor");
            
            if (_showBinData)
            {
                PrnParseData.processBinary(
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _buf,
                    _fileOffset,
                    bufOffset,
                    _hddrDescLen,
                    "",
                    _showBinData,
                    false,
                    true,
                    _indxOffsetFormat,
                    _analysisLevel);
            }

            if (_hddrDescLen > 64)
            {
                for (Int32 i = 64; i < _hddrDescLen; i++)
                {
                    _hddrChksVal += _buf[bufOffset + i];
                }
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Determine the Header Format, and from this determine the       //
            // position and size of parts of the header following the         //
            // Descriptor.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            _hddrDataLen = 0;
            _hddrCpyrLen = 0;
            _hddrResvLen = 0;
            _hddrChksLen = 0;

            _hddrDataPos = 0;
            _hddrCpyrPos = 0;
            _hddrResvPos = 0;
            _hddrChksPos = 0;

            _hddrDataRem = 0;
            _hddrCpyrRem = 0;

            _hddrFormat  = (ePCLFontFormat) _buf[bufOffset + 2];
            
            if ((_hddrFormat == ePCLFontFormat.Bitmap)
                               ||
                (_hddrFormat == ePCLFontFormat.BitmapResSpec))
            {
                //------------------------------------------------------------//
                //                                                            //
                // Format 0 - Bitmap font.                                    //
                // Format 20 - Resolution-Specified Bitmap font.              //
                //                                                            //
                //------------------------------------------------------------//

                _hddrCpyrLen = _hddrLen - _hddrDescLen;

                if (_hddrCpyrLen != 0)
                {
                    _hddrCpyrPos = _hddrPos + _hddrDescLen;
                    _hddrCpyrRem = _hddrCpyrLen;
                }
            }
            else if ((_hddrFormat == ePCLFontFormat.IntellifontBound)
                                   ||
                     (_hddrFormat == ePCLFontFormat.IntellifontUnbound))
            {
                //------------------------------------------------------------//
                //                                                            //
                // Format 10 - Intellifont Bound Scalable font.               //
                // Format 11 - Intellifont Unbound Scalable font.             //
                //                                                            //
                //------------------------------------------------------------//
                
                _hddrResvLen = 1;
                _hddrResvPos = _hddrPos + _hddrLen - 2;

                _hddrChksLen = 1;
                _hddrChksPos = _hddrPos + _hddrLen - 1;

                ix1 = bufOffset + _hddrDescLen;

                _hddrDataLen = (_buf[ix1 - 2] * 256) + _buf[ix1 - 1];

                if ((_hddrDataLen < 1) || (_hddrDataLen > _hddrLen))
                {
                    _validHddr = false;

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgWarning,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "*** Warning ***",
                        "",
                        "Intellifont Global Data length (" + _hddrDataLen +
                        ") is negative or is longer than the header!");
                }
                else
                {
                    _hddrDataRem = _hddrDataLen;
                    _hddrDataPos = _hddrPos + _hddrDescLen;

                    _hddrCpyrLen = _hddrLen - (_hddrDescLen + _hddrDataLen + 2);

                    if (_hddrCpyrLen != 0)
                    {
                        _hddrCpyrPos = _hddrDataPos + _hddrDataLen;
                        _hddrCpyrRem = _hddrCpyrLen;
                    }

                    if ((_hddrDescLen + _hddrDataLen + _hddrCpyrLen + 2) !=
                        _hddrLen)
                    {
                        _validHddr = false;

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgWarning,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "*** Warning ***",
                        "",
                        "Header is internally inconsistent");
                    }
                }
            }
            else if ((_hddrFormat == ePCLFontFormat.TrueType)
                                ||
                     (_hddrFormat == ePCLFontFormat.Universal))
            {
                //------------------------------------------------------------//
                //                                                            //
                // Format 15 - TrueType Scalable font.                        //
                // Format 16 - Universal (TrueType Scalable or Bitmap) font.  //
                //                                                            //
                //------------------------------------------------------------//

                _hddrResvLen = 1;
                _hddrResvPos = _hddrPos + _hddrLen - 2;

                _hddrChksLen = 1;
                _hddrChksPos = _hddrPos + _hddrLen - 1;

                _hddrDataLen = _hddrLen - (_hddrDescLen + 2);
                _hddrDataRem = _hddrDataLen;
                _hddrDataPos = _hddrPos + _hddrDescLen;
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
                _bitmapFont       = false;
                _intelliFont      = false;
                _truetypeFont     = false;

                _pclDotResX = PrnParseConstants.pclDotResDefault;
                _pclDotResY = PrnParseConstants.pclDotResDefault;

                //------------------------------------------------------------//
                //                                                            //
                // Font Descriptor size.                                      //
                //                                                            //
                //------------------------------------------------------------//

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLFontHddr,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Descriptor Size:",
                    "",
                    _hddrDescLen.ToString());

                //------------------------------------------------------------//
                //                                                            //
                // Header format (byte 2).                                    //
                //                                                            //
                //------------------------------------------------------------//

                switch (_hddrFormat)
                {
                    case ePCLFontFormat.Bitmap:
                        itemDesc = "0: PCL Bitmap";
                        _bitmapFont = true;
                        break;

                    case ePCLFontFormat.IntellifontBound:
                        itemDesc = "10: Intellifont Scalable (Bound)";
                        _intelliFont = true;
                        break;

                    case ePCLFontFormat.IntellifontUnbound:
                        itemDesc = "11: Intellifont Scalable (Unbound)";
                        _intelliFont = true;
                        break;

                    case ePCLFontFormat.TrueType:
                        itemDesc = "15: Truetype Scalable";
                        _truetypeFont = true;
                        break;

                    case ePCLFontFormat.Universal:
                        itemDesc = "16: Universal";
                        break;

                    case ePCLFontFormat.BitmapResSpec:
                        itemDesc = "20: Resolution-Specified Bitmap";
                        _bitmapFont = true;
                        break;

                    default:
                        itemDesc = _hddrFormat + ": Unknown";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLFontHddr,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Header Format:",
                    "",
                    itemDesc);

                //------------------------------------------------------------//
                //                                                            //
                // X-resolution:                                              //
                // Bitmap      fonts: type 0:  not present                    //
                //                    type 16: within BR Data Segment         //
                //                    type 20: dots per inch    (bytes 64-65) //
                // Intellifont fonts: type 10: design units/Em  (bytes 66-67) //
                // Intellifont fonts: type 11: design units/Em  (bytes 66-67) //
                // Truetype    fonts: type 15: not present                    //
                //                    type 16: not present                    //
                //                                                            //
                //------------------------------------------------------------//

                if (((_hddrFormat == ePCLFontFormat.IntellifontBound)
                                  ||
                     (_hddrFormat == ePCLFontFormat.IntellifontUnbound))
                                  &&
                    (_hddrDescLen > 67))
                {
                    ix1 = (_buf[bufOffset + 66] * 256) + _buf[bufOffset + 67];

                    _pclDotResX = ix1;

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "X-resolution:",
                        "",
                        ix1 + " design units per inch");
                }
                else if ((_hddrFormat == ePCLFontFormat.BitmapResSpec)
                                            &&
                         (_hddrDescLen > 65))
                {
                    ix1 = (_buf[bufOffset + 64] * 256) + _buf[bufOffset + 65];

                    _pclDotResX = ix1;

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "X-resolution:",
                        "",
                        ix1 + " dots per inch");
                }

                //------------------------------------------------------------//
                //                                                            //
                // Y-resolution:                                              //
                // Bitmap      fonts: type 0:  not present                    //
                //                    type 16: within BR Data Segment         //
                //                    type 20: dots per inch    (bytes 66-67) //
                // Intellifont fonts: type 10: design units/Em  (bytes 68-69) //
                // Intellifont fonts: type 11: design units/Em  (bytes 68-69) //
                // Truetype    fonts: type 15: not present                    //
                //                    type 16: not present                    //
                //                                                            //
                //------------------------------------------------------------//

                if (((_hddrFormat == ePCLFontFormat.IntellifontBound)
                                          ||
                     (_hddrFormat == ePCLFontFormat.IntellifontUnbound))
                                          &&
                    (_hddrDescLen > 69))
                {
                    ix1 = (_buf[bufOffset + 68] * 256) + _buf[bufOffset + 69];

                    _pclDotResY = ix1;

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Y-resolution:",
                        "",
                        ix1 + " design units per inch");
                }
                else if ((_hddrFormat == ePCLFontFormat.BitmapResSpec)
                                            &&
                         (_hddrDescLen > 67))
                {
                    ix1 = (_buf[bufOffset + 66] * 256) + _buf[bufOffset + 67];

                    _pclDotResY = ix1;

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Y-resolution:",
                        "",
                        ix1 + " dots per inch");
                }

                //------------------------------------------------------------//
                //                                                            //
                // Font type (byte 3).                                        //
                //                                                            //
                //------------------------------------------------------------//

                _fontType  = _buf[bufOffset + 3];
                _boundFont = true;

                switch (_fontType)
                {
                    case 0:
                        itemDesc = PCLSymSetTypes.getDescStd (
                                       (Int32) PCLSymSetTypes.eIndex.Bound_7bit);
                        break;

                    case 1:
                        itemDesc = PCLSymSetTypes.getDescStd (
                                       (Int32) PCLSymSetTypes.eIndex.Bound_8bit);
                        break;

                    case 2:
                        itemDesc = PCLSymSetTypes.getDescStd (
                                       (Int32) PCLSymSetTypes.eIndex.Bound_PC8);
                        break;

                    case 3:
                        itemDesc = PCLSymSetTypes.getDescStd (
                                       (Int32) PCLSymSetTypes.eIndex.Bound_16bit);
                        break;

                    case 10:
                        itemDesc = PCLSymSetTypes.getDescStd (
                                       (Int32) PCLSymSetTypes.eIndex.Unbound_MSL);
                        _boundFont = false;
                        break;

                    case 11:
                        itemDesc = PCLSymSetTypes.getDescStd (
                                       (Int32) PCLSymSetTypes.eIndex.Unbound_Unicode);
                        _boundFont = false;
                        break;

                    default:
                        itemDesc = _fontType + ": Unknown";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLFontHddr,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Font Type:",
                    "",
                    itemDesc);

                //------------------------------------------------------------//
                //                                                            //
                // First Code (bytes 36-37).                                  //
                // Last Code  (bytes 38-39).                                  //
                //                                                            //
                // For bound fonts, provides the lowest and highest character //
                // codes expected.                                            //
                // For unbound fonts, First Code should be zero, and Last     //
                // Code provides the maximum number of characters expected.   //
                //                                                            //
                //------------------------------------------------------------//

                ix1 = (_buf[bufOffset + 36] * 256) + _buf[bufOffset + 37];
                ix2 = (_buf[bufOffset + 38] * 256) + _buf[bufOffset + 39];
                {
                    if (_boundFont)
                    {
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLFontHddr,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "First Code:",
                            "",
                            ix1.ToString () +
                            " (0x" + ix1.ToString ("x") + ")");

                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLFontHddr,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "Last Code:",
                            "",
                            ix2.ToString () +
                            " (0x" + ix2.ToString ("x") + ")");
                    }
                    else
                    {
                        if (ix1 != 0)
                        {
                            PrnParseCommon.addTextRow (
                                PrnParseRowTypes.eType.MsgComment,
                                _table,
                                PrnParseConstants.eOvlShow.None,
                                "",
                                "Comment",
                                "",
                                "First Code (" + ix1.ToString() +
                                ") should be zero!");
                        }

                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLFontHddr,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "Character Count:",
                            "",
                            ix2 + " (maximum)");
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Symbol set (bytes 14-15).                                  //
                //                                                            //
                // Two part: 11-bit number (binary)                           //
                //            5-bit letter-code: add 64 to this to obtain the //
                //                  (ASCII) character-code of the letter      //
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

                ix1 = (_buf[bufOffset + 14] * 256) + _buf[bufOffset + 15];

                ix2 = ix1 >> 5;
                ix3 = (ix1 & 0x1f) + 64;
                c   = (Char) ix3;

                indxSymSet =
                    PCLSymbolSets.getIndexForId ((UInt16) ix1);

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLFontHddr,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Symbol Set:",
                    "Kind1 value:",
                    ix1.ToString () + " (0x" + ix1.ToString ("x2") + ")");

                if ((indxSymSet) == -1)
                {
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
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
                        PrnParseRowTypes.eType.PCLFontHddr,
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
                // Spacing type (byte 13).                                    //
                //                                                            //
                //------------------------------------------------------------//

                ix1 = _buf[bufOffset + 13];

                switch (ix1)
                {
                    case 0:
                        itemDesc = "0: Fixed-pitch";
                        break;

                    case 1:
                        itemDesc = "1: Proportional";
                        break;

                    default:
                        itemDesc = ix1 + ": Unknown";
                        break;
                }
                
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLFontHddr,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Spacing:",
                    "",
                    itemDesc);

                //------------------------------------------------------------//
                //                                                            //
                // Scaling technology (byte 70).                              //
                // Variety            (byte 71).                              //
                // Only for Header formats 15 and 16.                         //
                //                                                            //
                //------------------------------------------------------------//

                if (((_hddrFormat == ePCLFontFormat.TrueType)
                                       ||
                     (_hddrFormat == ePCLFontFormat.Universal))
                                       &&
                    (_hddrDescLen > 71))
                {
                    ix1 = _buf[bufOffset + 70]; 
                    ix2 = _buf[bufOffset + 71];

                    switch (ix1)
                    {
                        case 0:
                            itemDesc = "0: Intellifont";
                            _intelliFont = true;
                            break;

                        case 1:
                            itemDesc = "1: TrueType";
                            _truetypeFont = true;
                            break;

                        case 254:
                            itemDesc = "254: Bitmap";
                            _bitmapFont = true;
                            break;

                        default:
                            itemDesc = ix1 + ": Unknown";
                            break;
                    }
                    
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Scaling:",
                        "Technology:",
                        itemDesc);

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "",
                        "Variety:",
                        ix2.ToString());
                }

                //------------------------------------------------------------//
                //                                                            //
                // Scale factor (bytes 64-65).                                //
                // Bitmap      fonts: not present.                            //
                // Intellifont fonts: design units (0.01mm) per Em.           //
                // Truetype    fonts: design units per Em.                    //
                //                                                            //
                //------------------------------------------------------------//

                _fontScaleFactor = 1;
    
                if ((!_bitmapFont) && (_hddrDescLen > 65))
                {
                    ix1 = (_buf[bufOffset + 64] * 256) + _buf[bufOffset + 65];

                    _fontScaleFactor = ix1;
                    
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Scale Factor:",
                        "",
                        _fontScaleFactor + " design units per Em");
                }

                //------------------------------------------------------------//
                //                                                            //
                // Pitch (bytes 16-17) and Pitch Extended (byte 40)           //
                // Bitmap   fonts: PITCH value is radix (quarter dot) units   //
                //                 PITCH EXTENDED value is 1/1024 dot units   //
                //                 Calculate as (rounded) characters-per-inch //
                // Scalable fonts: PITCH value is master design space width   //
                //                 PITCH EXTENDED value is zero               //
                //                                                            //
                //------------------------------------------------------------//

                ix1 = (_buf[bufOffset + 16] * 256) + _buf[bufOffset + 17];
                ix2 = _buf[bufOffset + 40];

                pitchSet = true;

                if ((ix1 == 0) && (ix2 == 0))
                {
                    pitchSet = false;
                }

                if (_bitmapFont)
                {
                    ix3 = (ix1 * 256) + ix2;
                    fx1 = ((Single) (1024 * _pclDotResX)) / ix3;
                                  
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Pitch:",
                        "",
                        fx1.ToString("F3") + " characters per inch");

                    if (!pitchSet)
                    {
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.MsgComment,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "Comment",
                            "",
                            "Pitch value is zero");
                    }
                }
                else
                {
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Master Design:",
                        "Space Width:",
                        ix1 + " design units");

                    if (ix1 == 0)
                    {
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.MsgComment,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "Comment",
                            "",
                            "Pitch (space width) value is zero");
                    }

                    if (ix2 != 0)
                    {
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.MsgComment,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "Comment",
                            "",
                            "Scalable font: Pitch Extended value is non-zero");
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Height (bytes 18-19) and Height Extended (byte 41)         //
                // Bitmap      fonts: HEIGHT value is quarter dot units       //
                //                    HEIGHT EXTENDED value is 1/1024 dot     //
                //                    units                                   //
                //                    Calculate as (rounded) PCL points       //
                // Intellifont fonts: HEIGHT value is master design height    //
                //                    HEIGHT EXTENDED value is zero           //
                // Truetype    fonts: both values should be zero              //
                //                                                            //
                //------------------------------------------------------------//

                ix1 = (_buf[bufOffset + 18] * 256) + _buf[bufOffset + 19];
                ix2 = _buf[bufOffset + 41];

                if ((ix1 == 0) && (ix2 == 0))
                {
                    // do nothing
                }
                else if (_bitmapFont)
                {
                    fx1 = ((Single)(72 * ((ix1 * 256) + ix2))) /
                                   (1024 * _pclDotResY);

                    
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Height:",
                        "",
                        fx1.ToString("F2") + " PCL (1/72 inch) points");
                }
                else if (_intelliFont)
                {
                    fx1 = (Single) ix1 / 8;

                    if (pitchSet)
                    {
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLFontHddr,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "Height:",
                            "",
                            fx1.ToString("F2") + " true (typesetter) points");
                    }
                    else
                    {
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLFontHddr,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "Master Design:",
                            "Height:",
                            "",
                            fx1.ToString("F2") + " true (typesetter) points");
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Style (bytes 4 (MSB) & 23 (LSB) considered as one unit).   //
                //                                                            //
                // Components of concatenated Most & Least Significant Bytes) //
                // (bit numbers are zero-indexed from (left) Most Sig.):      //
                //                                                            //
                //    bits  0  -  5   Reserved                                //
                //          6  - 10   Structure  (e.g. Solid)                 //
                //         11  - 13   Width      (e.g. Condensed)             //
                //         14  - 15   Posture    (e.g. Italic)                //
                //                                                            //
                //------------------------------------------------------------//

                ix1 = (_buf[bufOffset + 4] * 256) + _buf[bufOffset + 23];
                                    
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLFontHddr,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Style:",
                    "Value:",
                    ix1.ToString());

                ix2 = (ix1 >> 5) & 0x1f;

                switch (ix2)
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
                    PrnParseRowTypes.eType.PCLFontHddr,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "",
                    "Structure:",
                    itemDesc);

                ix2 = (ix1 >> 2) & 0x07;

                switch (ix2)
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
                    PrnParseRowTypes.eType.PCLFontHddr,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "",
                    "Width:",
                    itemDesc);

                ix2 = ix1 & 0x03;

                switch (ix2)
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
                    PrnParseRowTypes.eType.PCLFontHddr,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "",
                    "Posture:",
                    itemDesc);

                //------------------------------------------------------------//
                //                                                            //
                // Stroke Weight (byte 24).                                   //
                //                                                            //
                // Item is signed byte:                                       //
                //                                                            //
                // e.g.  -7 = Ultra Thin                                      //
                //        0 = Medium                                          //
                //        7 = Ultra Black                                     //
                //                                                            //
                //------------------------------------------------------------//

                ix1 = _buf[bufOffset + 24];

                if (ix1 > 127)
                    ix1 = ix1 - 256;

                switch (ix1)
                {
                    case -7:
                        itemDesc = "-7: Ultra Thin";
                        break;

                    case -6:
                        itemDesc = "-6: Extra Thin";
                        break;

                    case -5:
                        itemDesc = "-5: Thin";
                        break;

                    case -4:
                        itemDesc = "-4: Extra Light";
                        break;

                    case -3:
                        itemDesc = "-3: Light";
                        break;

                    case -2:
                        itemDesc = "-2: Demi Light";
                        break;

                    case -1:
                        itemDesc = "-1: Semi Light";
                        break;

                    case 0:
                        itemDesc = "0: Medium (Book or Text)";
                        break;

                    case 1:
                        itemDesc = "1: Semi Bold";
                        break;

                    case 2:
                        itemDesc = "2: Demi Bold";
                        break;

                    case 3:
                        itemDesc = "3: Bold";
                        break;

                    case 4:
                        itemDesc = "4: Extra Bold";
                        break;

                    case 5:
                        itemDesc = "5: Black";
                        break;

                    case 6:
                        itemDesc = "6: Extra Black";
                        break;

                    case 7:
                        itemDesc = "7: Ultra Black";
                        break;

                    default:
                        itemDesc = ix1 + ": Unknown";
                        break;
                }
                                    
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLFontHddr,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Stroke Weight:",
                    "",
                    itemDesc);

                //------------------------------------------------------------//
                //                                                            //
                // Typeface (bytes 26 (MSB) & 25 (LSB) considered as one).    //
                //                                                            //
                // Components of concatenated Most & Least Significant Bytes) //
                // (bit numbers are zero-indexed from (left) MS):             //
                //                                                            //
                //    bits  0  -  3   Vendor code                             //
                //          4  - 15   Typeface Family code                    //
                //                                                            //
                //------------------------------------------------------------//

                ix1 = (_buf[bufOffset + 26] * 256) + _buf[bufOffset + 25];

                ix2 = ix1 & 0x0fff;
                                                    
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLFontHddr,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Typeface:",
                    "Family Code:",
                    ix2.ToString());

                ix2 = (ix1 >> 12) & 0x0f;

                if (ix2 != 0)
                {
                    switch (ix2)
                    {
                        case 0:
                            itemDesc = "0: Reserved";
                            break;

                        case 1:
                            itemDesc = "1: Agfa Corporation";
                            break;

                        case 2:
                            itemDesc = "2: Bitstream Inc.";
                            break;

                        case 3:
                            itemDesc = "3: Linotype Company";
                            break;

                        case 4:
                            itemDesc = "4: Monotype Corporation";
                            break;

                        case 5:
                            itemDesc = "5: Adobe Systems";
                            break;

                        case 6:
                            itemDesc = "6: Bigelow & Holmes";
                            break;

                        default:
                            itemDesc = ix2 + ": Unknown (Reserved)";
                            break;
                    }
                                                        
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "",
                        "Vendor Code:",
                        itemDesc);
                                                        
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "",
                        "Identifier:",
                        ix1.ToString());
                }

                //------------------------------------------------------------//
                //                                                            //
                // Font Number (bytes 44-47).                                 //
                //                                                            //
                // Components of structure are:                               //
                // (bit numbers are zero-indexed from (left) MS):             //
                //                                                            //
                //    bits  0         Format (Native or Converted)            //
                //          1  -  7   Vendor Name Initial (ASCII code)        //
                //          8  - 63   Vendor Font Number                      //
                //                                                            //
                //------------------------------------------------------------//

                ix1 = _buf[bufOffset + 44];
                ix2 = ix1 >> 7;
                ix3 = ix1 & 0x7f;

                ix1 = (_buf[bufOffset + 45] * 256 * 256) +
                      (_buf[bufOffset + 46] * 256) +
                      _buf[bufOffset + 47];

                if ((ix1 == 0) && (ix2 == 0) && (ix3 == 0))
                {
                    // do nothing
                }
                else
                {
                    switch (ix2)
                    {
                        case 0:
                            itemDesc = "0: Native";
                            break;

                        case 1:
                            itemDesc = "1: Converted";
                            break;

                        default:
                            itemDesc = ix2 + ": Impossible?";
                            break;
                    }
                                                        
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Font Number:",
                        "Format:",
                        itemDesc);

                    switch (ix3)
                    {
                        case 65:
                            itemDesc = "A: Adobe Systems Inc.";
                            break;

                        case 66:
                            itemDesc = "B: Bitstream Inc.";
                            break;

                        case 67:
                            itemDesc = "C: Agfa Division, Miles Inc.";
                            break;

                        case 72:
                            itemDesc = "H: Bigelow & Holmes";
                            break;

                        case 76:
                            itemDesc = "L: Linotype Company";
                            break;

                        case 77:
                            itemDesc = "M: Monotype Corporation plc";
                            break;

                        default:
                            itemDesc = ix3 + ": Unknown";
                            break;
                    }
                                                        
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "",
                        "Vendor Code:",
                        itemDesc);
                                    
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "",
                        "Vendor Font No.:",
                        ix1.ToString());
                }

                //------------------------------------------------------------//
                //                                                            //
                // Font Name (bytes 48-63).                                   //
                //                                                            //
                // Held as null-terminated ASCII string.                      //
                //                                                            //
                //------------------------------------------------------------//

                itemDesc = ascii.GetString (_buf, bufOffset + 48, 16);
                                                    
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLFontHddr,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Font Name:",
                    "",
                    itemDesc);

                //------------------------------------------------------------//
                //                                                            //
                // Orientation (byte 12).                                     //
                //                                                            //
                //    0 = Portrait                                            //
                //    1 = Landscape                                           //
                //    2 = Reverse Portrait                                    //
                //    3 = Reverse Landscape                                   //
                //                                                            //
                //------------------------------------------------------------//

                ix1 = _buf[bufOffset + 12];

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
                    PrnParseRowTypes.eType.PCLFontHddr,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Orientation:",
                    "",
                    itemDesc);

                //------------------------------------------------------------//
                //                                                            //
                // Serif Style (byte 27).                                     //
                //                                                            //
                // Components of structure are:                               //
                // (bit numbers are zero-indexed from (left) MS):             //
                //                                                            //
                //    bits  0  -  1   Serif type                              //
                //          2  -  7   Serif style                             //
                //                                                            //
                //------------------------------------------------------------//

                ix1 = _buf[bufOffset + 27];
                ix2 = ix1 & 0x3f;
                ix3 = ix1 >> 6;

                if (ix3 != 0)
                {
                    switch (ix3)
                    {
                        case 0:
                            itemDesc = "0: Reserved";
                            break;

                        case 1:
                            itemDesc = "1: Sans Serif / Monoline";
                            break;

                        case 2:
                            itemDesc = "2: Serif / Contrasting";
                            break;

                        case 3:
                            itemDesc = "3: Reserved";
                            break;

                        default:
                            itemDesc = ix3 + ": Impossible?";
                            break;
                    }
                                                                        
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Serif Type:",
                        "",
                        itemDesc);
                }

                switch (ix2)
                {
                    case 0:
                        itemDesc = "0: Sans Serif Square";
                        break;

                    case 1:
                        itemDesc = "1: Sans Serif Round";
                        break;

                    case 2:
                        itemDesc = "2: Serif Line";
                        break;

                    case 3:
                        itemDesc = "3: Serif Triangle";
                        break;

                    case 4:
                        itemDesc = "4: Serif Swath";
                        break;

                    case 5:
                        itemDesc = "5: Serif Block";
                        break;

                    case 6:
                        itemDesc = "6: Serif Bracket";
                        break;

                    case 7:
                        itemDesc = "7: Rounded Bracket";
                        break;

                    case 8:
                        itemDesc = "8: Flair Serif, Modified Sans";
                        break;

                    case 9:
                        itemDesc = "9: Script Non-connecting";
                        break;

                    case 10:
                        itemDesc = "10: Script Joining";
                        break;

                    case 11:
                        itemDesc = "11: Script Calligraphic";
                        break;

                    case 12:
                        itemDesc = "12: Script Broken Letter";
                        break;

                    default:
                        itemDesc = ix2 + ": Unknown";
                        break;
                }

                if (ix3 == 0)
                {
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Serif Style:",
                        "",
                        itemDesc);
                }
                else
                {
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "      Style:",
                        "",
                        itemDesc);
                }

                //------------------------------------------------------------//
                //                                                            //
                // Width Type (byte 22).                                      //
                //                                                            //
                // Signed 1-byte field contains the PCL appearance-width      //
                // value.                                                     //
                // Note that these values are not directly related to the     //
                // Width component of the Style value.                        //
                //                                                            //
                //------------------------------------------------------------//

                ix1 = _buf[bufOffset + 22];

                if (ix1 > 127)
                    ix1 = ix1 - 256;

                switch (ix1)
                {
                    case -5:
                        itemDesc = "-5: Ultra Compressed";
                        break;

                    case -4:
                        itemDesc = "-4: Extra Compressed";
                        break;

                    case -3:
                        itemDesc = "-3: Compressed (Extra Condensed)";
                        break;

                    case -2:
                        itemDesc = "2: Condensed";
                        break;

                    case -1:
                        itemDesc = "1: Unknown (Reserved)";
                        break;

                    case 0:
                        itemDesc = "0: Normal";
                        break;

                    case 1:
                        itemDesc = "1: Unknown (Reserved)";
                        break;

                    case 2:
                        itemDesc = "2: Expanded";
                        break;

                    case 3:
                        itemDesc = "3: Extra Expanded";
                        break;

                    case 4:
                        itemDesc = "4: Unknown (Reserved)";
                        break;

                    case 5:
                        itemDesc = "5: Unknown (Reserved)";
                        break;

                    default:
                        itemDesc = ix1 + ": Unknown";
                        break;
                }
                                                                    
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLFontHddr,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Width Type:",
                    "",
                    itemDesc);

                //------------------------------------------------------------//
                //                                                            //
                // Quality (byte 28).                                         //
                //                                                            //
                //------------------------------------------------------------//

                ix1 = _buf[bufOffset + 28];

                switch (ix1)
                {
                    case 0:
                        itemDesc = "0: Data Processing (Draft)";
                        break;

                    case 1:
                        itemDesc = "1: Near Letter Quality";
                        break;

                    case 2:
                        itemDesc = "2: Letter Quality";
                        break;

                    default:
                        itemDesc = ix1 + ": Unknown";
                        break;
                }
                                                                    
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLFontHddr,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Quality:",
                    "",
                    itemDesc);

                //------------------------------------------------------------//
                //                                                            //
                // Placement (byte 29).                                       //
                //                                                            //
                //------------------------------------------------------------//

                ix1 = _buf[bufOffset + 29];

                switch (ix1)
                {
                    case -1:
                        itemDesc = "-1: Inferior";
                        break;

                    case 0:
                        itemDesc = "0: Normal";
                        break;

                    case 1:
                        itemDesc = "1: Superior";
                        break;

                    default:
                        itemDesc = ix1 + ": Unknown";
                        break;
                }
                                                                    
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLFontHddr,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Placement:",
                    "",
                    itemDesc);

                //------------------------------------------------------------//
                //                                                            //
                // Cell Width (bytes 8 & 9).                                  //
                //                                                            //
                // For bitmap fonts, units are PCL system coordinate dots     //
                // (independent of orientation).                              //
                // For scalable fonts, units are font design units.           //
                //                                                            //
                //------------------------------------------------------------//

                ix1 = (_buf[bufOffset + 8] * 256) + _buf[bufOffset + 9];

                if (_bitmapFont)
                {
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Cell Width:",
                        "",
                        ix1 + " dots");
                }
                else
                {
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Cell Width:",
                        "",
                        ix1 + " design units");
                }

                //------------------------------------------------------------//
                //                                                            //
                // Text Width (bytes 34 & 35).                                //
                //                                                            //
                // Specifies the average character spacing for lower-case     //
                // characters.                                                //
                //                                                            //
                //------------------------------------------------------------//

                ix1 = (_buf[bufOffset + 34] * 256) + _buf[bufOffset + 35];

                if (ix1 != 0)
                {
                    if (_bitmapFont)
                    {
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLFontHddr,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "Text Width:",
                            "",
                            ix1 + " quarter dots");

                        fx1 = ((Single) (4 * _pclDotResX)) / ix1;

                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLFontHddr,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "     ---->",
                            "Average Spacing:",
                            fx1.ToString("F1") + " characters per inch");
                    }
                    else
                    {
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLFontHddr,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "Text Width:",
                            "",
                            ix1 + " design units");
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Cell Height (bytes 10 & 11).                               //
                //                                                            //
                // For bitmap fonts, units are PCL system coordinate dots     //
                // (independent of orientation).                              //
                // For scalable fonts, units are font design units.           //
                //                                                            //
                //------------------------------------------------------------//

                ix1 = (_buf[bufOffset + 10] * 256) + _buf[bufOffset + 11];

                if (_bitmapFont)
                {
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Cell Height:",
                        "",
                        ix1 + " dots");
                }
                else
                {
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Cell Height:",
                        "",
                        ix1 + " design units");
                }

                //------------------------------------------------------------//
                //                                                            //
                // Text Height (bytes 32 & 33).                               //
                //                                                            //
                // Specifies optimum inter-line spacing for the font.         //
                //                                                            //
                //------------------------------------------------------------//

                ix1 = (_buf[bufOffset + 32] * 256) + _buf[bufOffset + 33];

                if (ix1 != 0)
                {
                    if (_bitmapFont)
                    {
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLFontHddr,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "Text Height:",
                            "",
                            ix1 + " quarter dots");

                        fx1 = ((Single) (4 * _pclDotResY)) / ix1;

                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLFontHddr,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "     ---->",
                            "Optimum Spacing:",
                            fx1.ToString("F1") + " lines per inch");
                    }
                    else
                    {
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLFontHddr,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "Text Height:",
                            "",
                            ix1 + " design units");
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // x-Height (bytes 20 & 21).                                  //
                //                                                            //
                // Height of lower-case "x" characters.                       //
                // This provides a measure of the 'central' part of           //
                // characters (i.e. not including ascenders or descenders).   //
                //                                                            //
                // Units are radix (quarter) dots for bitmap fonts.           //
                //           design units             scalable fonts.         //
                //                                                            //
                //------------------------------------------------------------//

                ix1 = (_buf[bufOffset + 20] * 256) + _buf[bufOffset + 21];

                if (ix1 != 0)
                {
                    if (_bitmapFont)
                    {
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLFontHddr,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "x-Height:",
                            "",
                            ix1 + " quarter dots");
                    }
                    else
                    {                                                    
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLFontHddr,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "x-Height:",
                            "",
                            ix1 + " design units");
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Cap Height (bytes 42 & 43).                                //
                //                                                            //
                // Height of optical line describing top of (unaccented)      //
                // upper-case characters (usually 'H').                       //
                //                                                            //
                // For bitmap fonts:                                          //
                //    Required units are 'percent of Em of the font', but the //
                //    actual value is held as the product of this percentage  //
                //    (where 1.00=100%) and the maximum unsigned short        //
                //    integer value (65535).                                  //
                //    A value of zero implies 70.87% (which corresponds to a  //
                //    stored value of 46445).                                 //
                //                                                            //
                // For scalable fonts:                                        //
                //    Units are design units.                                 //
                //                                                            //
                //------------------------------------------------------------//

                ix1 = (_buf[bufOffset + 42] * 256) + _buf[bufOffset + 43];

                if (_bitmapFont)
                {
                    if (ix1 == 0)
                    {                                                    
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLFontHddr,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "Cap Height:",
                            "",
                            "Default (70.87% of Em)");
                    }
                    else
                    {
                        fx1 = ((Single) (ix1 * 100)) / 65535;
                                                    
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLFontHddr,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "Cap Height:",
                            "",
                            fx1.ToString("F2") + "% of Em");
                    }
                }
                else if (ix1 != 0)
                {                                                    
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Cap Height:",
                        "",
                        ix1 + " design units");
                }

                //------------------------------------------------------------//
                //                                                            //
                // Baseline Position (bytes 6 & 7).                           //
                //                                                            //
                // Position of bottom of character (ignoring descenders)      //
                // below top of cell (i.e. number of rows in cell above the   //
                // baseline row).                                             //
                //                                                            //
                // Valid range: 0 --> (cell_height - 1)                       //
                //                                                            //
                // Units are PCL system coordinate dots (i.e. independent of  //
                // orientation).                                              //
                //                                                            //
                //------------------------------------------------------------//

                ix1 = (_buf[bufOffset + 6] * 256) + _buf[bufOffset + 7];

                if (_bitmapFont)
                {                                                    
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Baseline:",
                        "",
                        ix1 + " rows below top of cell");
                }
                else if (_intelliFont)
                {
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Baseline:",
                        "",
                        ix1 + " design units (Y co-ord)");
                }
                else if ((_truetypeFont) && (ix1 != 0))
                {
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Baseline:",
                        "",
                        ix1 + ": should be zero!");
                }

                //------------------------------------------------------------//
                //                                                            //
                // Underline Thickness (byte 31).                             //
                //                                                            //
                // Valid range: 0 --> (cell_height - 1)                       //
                // (but expected values are 3 for 300dpi fonts, 6 for 600dpi).//
                //                                                            //
                // Underline Position (byte 30).                              //
                //                                                            //
                // Distance of top row of underline from the Baseline         //
                // Position (number of blank rows between baseline and top    //
                // row of underline).                                         //
                //                                                            //
                //    0    =  on baseline                                     //
                //    +ve  =  above baseline                                  //
                //    -ve  =  below baseline                                  //
                //                                                            //
                // These items are used with Bitmap fonts only; for scalable  //
                // fonts, the values should both be zero.                     //
                // Units are PCL system coordinate dots (i.e. independent of  //
                // orientation).                                              //
                //                                                            //
                //------------------------------------------------------------//

                ix1 = _buf[bufOffset + 31];
                ix2 = _buf[bufOffset + 30];

                if (ix1 != 0)
                {
                    if (_bitmapFont)
                    {                                                    
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLFontHddr,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "Underline:",
                            "Thickness:",
                            ix1 + " dots");
                    }
                    else
                    {
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLFontHddr,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "Underline:",
                            "Thickness:",
                            ix1 + ": should be zero!");
                    }
                }

                if (ix2 != 0)
                {
                    String text1,
                           text3;

                    if (_bitmapFont)
                    {
                        if (ix2 > 127)
                        {
                            ix2 = ix2 - 256;
                        }

                        if (ix1 == 0)
                            text1 = "Underline:";
                        else
                            text1 = "";

                        if (ix1 == 0)
                            text3 = "On baseline";
                        else if (ix2 < 0)
                        {
                            ix2 = - ix2;
                            text3 = ix2 + " dots below baseline";
                        }
                        else
                        {
                            text3 = ix2 + " dots above baseline";
                        }
                                                    
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLFontHddr,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            text1,
                            "Position:",
                            text3);
                    }
                    else
                    {
                        if (ix1 == 0)
                        {
                            PrnParseCommon.addTextRow (
                                PrnParseRowTypes.eType.PCLFontHddr,
                                _table,
                                PrnParseConstants.eOvlShow.None,
                                "",
                                "Underline:",
                                "Position:",
                                ix2 + ": should be zero!");
                        }
                        else
                        {
                            PrnParseCommon.addTextRow (
                                PrnParseRowTypes.eType.PCLFontHddr,
                                _table,
                                PrnParseConstants.eOvlShow.None,
                                "",
                                "",
                                "Position:",
                                ix2 + ": should be zero!");
                        }
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Master Underline Thickness                                 //
                //                                                            //
                //    Defines thickness of the floating underline in design   //
                //    units.                                                  //
                //                                                            //
                //    Bitmap      fonts: not present                          //
                //    Intellifont fonts: bytes 72 & 73                        //
                //    Truetype    fonts: bytes 68 & 69                        //
                //                                                            //
                // Master Underline Position                                  //
                //                                                            //
                //    Defines position of the top of the floating underline   //
                //    with respect to the baseline, in design units.          //
                //                                                            //
                //    Bitmap      fonts: not present                          //
                //    Intellifont fonts: bytes 70 & 71                        //
                //    Truetype    fonts: bytes 66 & 67                        //
                //                                                            //
                //------------------------------------------------------------//

                valOK = false;

                if ((_intelliFont) && (_hddrDescLen > 73))
                {
                    valOK = true;

                    ix1 = (_buf[bufOffset + 72] * 256) + _buf[bufOffset + 73];
                    ix2 = (_buf[bufOffset + 70] * 256) + _buf[bufOffset + 71];
                }
                else if ((_truetypeFont) && (_hddrDescLen > 69))
                {
                    valOK = true;

                    ix1 = (_buf[bufOffset + 68] * 256) + _buf[bufOffset + 69];
                    ix2 = (_buf[bufOffset + 66] * 256) + _buf[bufOffset + 67];
                }

                if ((valOK) && (ix1 != 0))
                {
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Underline:",
                        "Thickness:",
                        ix1 + " design units");

                    if (ix2 > 32767)
                        ix2 = ix2 - 65536;

                    if (ix2 == 0)
                    {
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLFontHddr,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "",
                            "Position:",
                            "On baseline");
                    }
                    else if (ix2 < 0)
                    {
                        ix2 = - ix2;

                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLFontHddr,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "",
                            "Position:",
                            ix2 + " design units below baseline");
                    }
                    else
                    {
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLFontHddr,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "",
                            "Position:",
                            ix2 + " design units above baseline");
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // OR Threshold (byte 74).                                    //
                //                                                            //
                // Present for Intellifont fonts only (formats 10 & 11).      //
                //                                                            //
                // Defines the pixel-size (in design units) above which the   //
                // missing pixel recovery process is switched on in           //
                // Intellifont scaling and rasterisation.                     //
                //                                                            //
                //------------------------------------------------------------//

                if ((_intelliFont) && (_hddrDescLen > 74))
                {
                    ix1 = _buf[bufOffset + 74];

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "OR Threshold:",
                        "",
                        ix1 + " design units");
                }

                //------------------------------------------------------------//
                //                                                            //
                // Global Italic Angle (byte 76).                             //
                //                                                            //
                // Present for Intellifont fonts only (formats 10 & 11).      //
                //                                                            //
                // Defines the tangent of the Italic angle, relative to the   //
                // vertical, multiplied by 32768 (2**15).                     //
                //                                                            //
                //------------------------------------------------------------//

                if ((_intelliFont) && (_hddrDescLen > 76))
                {
                    ix1 = _buf[bufOffset + 76];

                    fx1 = ((Single) ix1) / 32768;

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontHddr,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Italic Angle:",
                        "Tangent:",
                        fx1.ToString("F5"));
                }

                //------------------------------------------------------------//
                //                                                            //
                // Character Complement (bytes 78 - 85).                      //
                //                                                            //
                // Present for unbound Intellifont fonts only (format 11).    //
                //                                                            //
                // 8-byte bit-significant value.                              //
                //                                                            //
                // [TODO  In a future version, interpret this as MSL or       //
                // Unicode sets]                                              //
                //                                                            //
                //------------------------------------------------------------//

                if ((_hddrFormat == ePCLFontFormat.IntellifontUnbound)
                                        &&
                    (_hddrDescLen > 85))
                {
                    _parseSegs.decodeCharCompReq(
                        true,
                        true,
                        true,
                        _fileOffset,
                        _buf,
                        bufOffset + 78,
                        _linkData,
                        _options,
                        _table);
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s G l o b a l D a t a                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Process Intellifont Global Data.                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processGlobalData(ref Int32 bufRem,
                                       ref Int32 bufOffset)
        {
            PrnParseConstants.eContType contType;

            Int32 binDataLen;

            if (_hddrDataRem > bufRem)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Remainder of sequence is not in buffer.                    //
                // Initiate continuation.                                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = PrnParseConstants.eContType.PCLFontHddr;

                binDataLen   = bufRem;
                _hddrDataRem = _hddrDataRem - bufRem;
                _hddrRem     = _hddrRem - bufRem;

                _linkData.setContinuation (contType);
            }
            else
            {
                contType = PrnParseConstants.eContType.None;
      
                _linkData.resetContData();

                binDataLen   = _hddrDataRem;
                _hddrDataRem = 0;
                _hddrRem     = _hddrRem - binDataLen;
                _nextStage   = eStage.ShowCopyright;
            }

            if ((binDataLen) != 0)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Some, or all, of the download data is contained within the //
                // current 'block'.                                           //
                //                                                            //
                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    PrnParseRowTypes.eType.DataBinary,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    _fileOffset + bufOffset,
                    _analysisLevel,
                    "PCL Binary",
                    "[ " + binDataLen + " bytes ]",
                    "Font header Intellifont data");

                if (_showBinData)
                {
                    PrnParseData.processBinary (
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _buf,
                        _fileOffset,
                        bufOffset,
                        binDataLen,
                        "",
                        _showBinData,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }

                for (Int32 i = 0; i < binDataLen; i++)
                {
                    _hddrChksVal += _buf[bufOffset + i];
                }

                bufRem    = bufRem - binDataLen;
                bufOffset = bufOffset + binDataLen;
            }
        }
    }
}