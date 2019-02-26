using System;
using System.Data;
using System.Text;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class handles segmented data elements of downloadable soft fonts.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    class PrnParseFontSegs
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
        
        private PrnParseConstants.eContType _contType;

        private DataTable _table;

        private Byte[] _buf;

        private Int32 _fileOffset;
        private Int32 _analysisLevel;
        private Int32 _segRem;

        private Boolean _validSegs;
        private Boolean _showBinData;
        private Boolean _PCL;
        private PrnParseOptions _options;

        private PrnParseConstants.eOptOffsetFormats _indxOffsetFormat;
        private PrnParseRowTypes.eType _rowType;

        private ASCIIEncoding _ascii = new ASCIIEncoding ();

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e C h a r C o m p R e q                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Provide an interpretation of the contents of:                      //
        //  -   Character Complement   array (part of font header)            //
        //  -   Character Requirements array (part of Define Symbol Set)      //
        // One is the complement of the other.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean decodeCharCompReq(Boolean complement,
                                         Boolean format_MSL,
                                         Boolean PCL,
                                         Int32 fileOffset,
                                         Byte[] buf,
                                         Int32 bufOffset,
                                         PrnParseLinkData linkData,
                                         PrnParseOptions options,
                                         DataTable table)
        {
            const Int32 arrayBytes  = 8;
            const Int32 bitsPerByte = 8;
            const Int32 arrayBits = arrayBytes * bitsPerByte;
 
            PCLCharCollections.eBitType bitType;
            PrnParseRowTypes.eType rowType;

            Boolean dataOK = true;

            UInt64 charCollArray,
                   charCollVal,
                   charCollIndex,
                   bitVal;

            Int32 offset;

            String codeDesc,
                   textDesc;

            Int32 analysisLevel;
            Int32 listIndex;

            Boolean bitSet,
                    bitSig;

            Boolean showBinData;

            PrnParseConstants.eOptOffsetFormats indxOffsetFormat;

            analysisLevel = linkData.AnalysisLevel;
            showBinData = options.FlagPCLMiscBinData;

            indxOffsetFormat = options.IndxGenOffsetFormat;

            offset = bufOffset;

            if (complement)
                textDesc = "Char. Complement";
            else
                textDesc = "Char. Requirements";

            if (PCL)
                rowType = PrnParseRowTypes.eType.PCLFontHddr;
            else
                rowType = PrnParseRowTypes.eType.PCLXLFontHddr;

            //----------------------------------------------------------------//
            //                                                                //
            // Obtain character collection bit array.                         //
            //                                                                //
            //----------------------------------------------------------------//

            charCollArray = 0;

            for (Int32 i = 0; i < arrayBytes; i++)
            {
                charCollArray = (charCollArray << 8) + buf [offset + i];
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Obtain aggregate values for the symbol index and collection    //
            // bits which are set.                                            //
            //                                                                //
            //----------------------------------------------------------------//

            charCollVal   = 0;
            charCollIndex = 0;

            for (Int32 i = 0; i < arrayBits; i++)
            {
                bitVal = ((UInt64) 0x01) << i;

                if ((charCollArray & bitVal) != 0)
                {
                    // bit is set //

                    listIndex = PCLCharCollections.getindexForKey (i);

                    bitType = PCLCharCollections.getBitType (listIndex);

                    if (bitType == PCLCharCollections.eBitType.Collection)
                        charCollVal += bitVal;
                    else
                        charCollIndex += bitVal;
                }
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Display details of symbol index identifier bits.               //
            //                                                                //
            //----------------------------------------------------------------//

            if (format_MSL)
            {
                if ((complement) && (charCollIndex == 0x07))
                    codeDesc = "'111' = MSL";
                else if ((!complement) && (charCollIndex == 0x00))
                    codeDesc = "'000' = MSL";
                else
                    codeDesc = "'" + charCollIndex.ToString () +
                              "' not MSL value!";
            }
            else
            {
                if ((complement) && (charCollIndex == 0x06))
                    codeDesc = "'110' = Unicode";
                else if ((!complement) && (charCollIndex == 0x01))
                    codeDesc = "'001' = Unicode";
                else
                    codeDesc = "'" + charCollIndex.ToString () +
                               "' not Unicode value!";
            }

            PrnParseCommon.addTextRow (
                rowType,
                table,
                PrnParseConstants.eOvlShow.None,
                "",
                textDesc,
                "Symbol index",
                codeDesc);

            //----------------------------------------------------------------//
            //                                                                //
            // Display details of collection bits.                            //
            //                                                                //
            //----------------------------------------------------------------//
            
            if (charCollVal == 0)
            {
                if (complement)
                    codeDesc = "All bits unset - compatible with any" +
                               " character set";
                else
                    codeDesc = "All bits unset - compatible with any" +
                               " typeface";

                PrnParseCommon.addTextRow (
                    rowType,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    textDesc,
                    "Collection",
                    codeDesc);
            }
            else
            {
                for (Int32 i = 0; i < arrayBits; i++)
                {
                    bitVal = ((UInt64)0x01) << i;

                    if ((charCollVal & bitVal) != 0)
                        bitSet = true;
                    else
                        bitSet = false;

                    if (bitSet && (!complement))
                        bitSig = true;
                    else if ((!bitSet) & (complement))
                        bitSig = true;
                    else
                        bitSig = false;

                    if (bitSig)
                    {
                        listIndex = PCLCharCollections.getindexForKey (i);

                        if (format_MSL)
                            codeDesc =
                                PCLCharCollections.getDescMSL (listIndex);
                        else
                            codeDesc =
                                PCLCharCollections.getDescUnicode (listIndex);

                        PrnParseCommon.addTextRow (
                            rowType,
                            table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            textDesc,
                            "Collection",
                            codeDesc);
                    }
                }
            }

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s S e g D a t a                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Process Segmented Data.                                            //
        //                                                                    //
        // PCL Format 15 (TrueType): the data consists of one or more         //
        // segments, each in TTLLD format:                                    //
        //                                                                    //
        //    bytes 0 -> 1         segment identifier                         //
        //          2 -> 3         segment data length 'n' (may be zero)      //
        //          4 -> 4 + (n-1) segment data                               //
        //                                                                    //
        // PCL Format 16 (Universal): the data consists of one or more        //
        // segments, each in TTLLLLD format:                                  //
        //                                                                    //
        //    bytes 0 -> 1         segment identifier                         //
        //          2 -> 5         segment data length 'n' (may be zero)      //
        //          6 -> 6 + (n-1) segment data                               //
        //                                                                    //
        // PCL XL segments have the same basic structure as PCL Universal     //
        // segments.                                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean processSegData(Byte[] buf,
                                      Int32 fileOffset,
                                      Boolean PCL,
                                      Boolean firstSeg,
                                      Boolean largeSegs,
                                      ref Int32 bufRem,
                                      ref Int32 bufOffset,
                                      ref Int32 hddrDataRem,
                                      ref Int32 hddrRem,
                                      ref Int32 hddrChksVal,
                                      ref Boolean valid,
                                      PrnParseLinkData linkData,
                                      PrnParseOptions options,
                                      DataTable table)
        {
            PrnParseConstants.eContType contType;

            Boolean continuation = false;
            
            Int32 binDataLen;
            Int32 segHddrLen;
            Int32 segSize;
            Int32 segType;

            _buf = buf;
            _fileOffset = fileOffset;
            _linkData = linkData;
            _table = table;
            _PCL = PCL;
            _options = options;

            _analysisLevel = _linkData.AnalysisLevel;

            if (PCL)
            {
                _contType = PrnParseConstants.eContType.PCLFontHddr;
                _rowType = PrnParseRowTypes.eType.PCLFontHddr;
                _showBinData = options.FlagPCLMiscBinData;
            }
            else
            {
                _contType = PrnParseConstants.eContType.PCLXLFontHddr;
                _rowType = PrnParseRowTypes.eType.PCLXLFontHddr;
                _showBinData = options.FlagPCLXLMiscBinData; 
            }

            if (largeSegs)
                segHddrLen = 6;
            else
                segHddrLen = 4;

            _indxOffsetFormat = options.IndxGenOffsetFormat;

            //----------------------------------------------------------------//
            //                                                                //
            // Reset continuation data.                                       //
            //                                                                //
            //----------------------------------------------------------------//

            contType = PrnParseConstants.eContType.None;

            _linkData.resetContData ();

            if (firstSeg)
            {
                //------------------------------------------------------------//
                //                                                            //
                // First segment.                                             //
                //                                                            //
                //------------------------------------------------------------//
                
                String text;

                segSize = 0;
                _segRem = 0;
                _validSegs = true;

                if (PCL)
                    text = "PCL Binary";
                else
                    text = "PCL XL Binary";

                PrnParseCommon.addDataRow (
                    PrnParseRowTypes.eType.DataBinary,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    _fileOffset + bufOffset,
                    _analysisLevel,
                    text,
                    "[ " + hddrDataRem + " bytes ]",
                    "Font header segmented data");
            }

            while ((hddrDataRem != 0) &&
                   (contType == PrnParseConstants.eContType.None) &&
                   (_validSegs))
            {
                if (_segRem == 0)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Either first segment, or previous segment fully        //
                    // processed.                                             //
                    // Output details of segment type and data length.        //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (bufRem < segHddrLen)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Segment Type and Length data is not in buffer.     //
                        // Initiate (back-tracking) continuation.             //
                        //                                                    //
                        //----------------------------------------------------//

                        continuation = true;

                        contType = _contType;

                        _linkData.setBacktrack (contType, - bufRem);
                    }
                    else
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Segment Type and Length data is in buffer.         //
                        //                                                    //
                        //----------------------------------------------------//

                        segType = (_buf[bufOffset] * 256) +
                                   _buf[bufOffset + 1];

                        if (largeSegs)
                        {
                            segSize = (_buf[bufOffset + 2] * 65536 * 256) +
                                      (_buf[bufOffset + 3] * 65536) +
                                      (_buf[bufOffset + 4] * 256) +
                                       _buf[bufOffset + 5];
                        }
                        else
                        {
                            segSize = (_buf[bufOffset + 2] * 256) +
                                       _buf[bufOffset + 3];
                        }

                        switch (segType)
                        {
                            case 0x4150:
                                processSeg_AP (segSize,
                                               segHddrLen,
                                               ref bufRem,
                                               ref bufOffset,
                                               ref hddrDataRem,
                                               ref hddrRem,
                                               ref hddrChksVal);
                                break;

                            case 0x4252:
                                processSeg_BR (segSize,
                                               segHddrLen,
                                               ref bufRem,
                                               ref bufOffset,
                                               ref hddrDataRem,
                                               ref hddrRem,
                                               ref hddrChksVal);
                                break;

                            case 0x4343:
                                processSeg_CC (segSize,
                                               segHddrLen,
                                               ref bufRem,
                                               ref bufOffset,
                                               ref hddrDataRem,
                                               ref hddrRem,
                                               ref hddrChksVal);
                                break;

                            case 0x4345:
                                processSeg_CE (segSize,
                                               segHddrLen,
                                               ref bufRem,
                                               ref bufOffset,
                                               ref hddrDataRem,
                                               ref hddrRem,
                                               ref hddrChksVal);
                                break;

                            case 0x4350:
                                processSeg_CP (segSize,
                                               segHddrLen,
                                               ref bufRem,
                                               ref bufOffset,
                                               ref hddrDataRem,
                                               ref hddrRem,
                                               ref hddrChksVal);
                                break;

                            case 0x4743:
                                processSeg_GC (segSize,
                                               segHddrLen,
                                               ref bufRem,
                                               ref bufOffset,
                                               ref hddrDataRem,
                                               ref hddrRem,
                                               ref hddrChksVal);
                                break;

                            case 0x4749:
                                processSeg_GI (segSize,
                                               segHddrLen,
                                               ref bufRem,
                                               ref bufOffset,
                                               ref hddrDataRem,
                                               ref hddrRem,
                                               ref hddrChksVal);
                                break;

                            case 0x4754:
                                processSeg_GT (segSize,
                                               segHddrLen,
                                               ref bufRem,
                                               ref bufOffset,
                                               ref hddrDataRem,
                                               ref hddrRem,
                                               ref hddrChksVal);
                                break;

                            case 0x4946:
                                processSeg_IF (segSize,
                                               segHddrLen,
                                               ref bufRem,
                                               ref bufOffset,
                                               ref hddrDataRem,
                                               ref hddrRem,
                                               ref hddrChksVal);
                                break;

                            case 0x5041:
                                processSeg_PA (segSize,
                                               segHddrLen,
                                               ref bufRem,
                                               ref bufOffset,
                                               ref hddrDataRem,
                                               ref hddrRem,
                                               ref hddrChksVal);
                                break;

                            case 0x5046:
                                processSeg_PF (segSize,
                                               segHddrLen,
                                               ref bufRem,
                                               ref bufOffset,
                                               ref hddrDataRem,
                                               ref hddrRem,
                                               ref hddrChksVal);
                                break;

                            case 0x5446:
                                processSeg_TF (segSize,
                                               segHddrLen,
                                               ref bufRem,
                                               ref bufOffset,
                                               ref hddrDataRem,
                                               ref hddrRem,
                                               ref hddrChksVal);
                                break;

                            case 0x5645:
                                processSeg_VE (segSize,
                                               segHddrLen,
                                               ref bufRem,
                                               ref bufOffset,
                                               ref hddrDataRem,
                                               ref hddrRem,
                                               ref hddrChksVal);
                                break;

                            case 0x5649:
                                processSeg_VI (segSize,
                                               segHddrLen,
                                               ref bufRem,
                                               ref bufOffset,
                                               ref hddrDataRem,
                                               ref hddrRem,
                                               ref hddrChksVal);
                                break;

                            case 0x5652:
                                processSeg_VR (segSize,
                                               segHddrLen,
                                               ref bufRem,
                                               ref bufOffset,
                                               ref hddrDataRem,
                                               ref hddrRem,
                                               ref hddrChksVal);
                                break;

                            case 0x5654:
                                processSeg_VT (segSize,
                                               segHddrLen,
                                               ref bufRem,
                                               ref bufOffset,
                                               ref hddrDataRem,
                                               ref hddrRem,
                                               ref hddrChksVal);
                                break;

                            case 0x5857:
                                processSeg_XW (segSize,
                                               segHddrLen,
                                               ref bufRem,
                                               ref bufOffset,
                                               ref hddrDataRem,
                                               ref hddrRem,
                                               ref hddrChksVal);
                                break;

                            case 0x00ffff:
                                processSegNull (segSize,
                                                segHddrLen,
                                                ref bufRem,
                                                ref bufOffset,
                                                ref hddrDataRem,
                                                ref hddrRem,
                                                ref hddrChksVal);
                                break;

                            default:
                                processSegUnknown (segType,
                                                   segSize,
                                                   segHddrLen,
                                                   ref bufRem,
                                                   ref bufOffset,
                                                   ref hddrDataRem,
                                                   ref hddrRem,
                                                   ref hddrChksVal);
                                break;
                        }

                        contType = _linkData.getContType ();

                        if (contType != PrnParseConstants.eContType.None)
                            continuation = true;
                    }
                }

                if (_segRem > bufRem)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Remainder of segment is not in buffer.                 //
                    // Initiate (non back-tracking) continuation.             //
                    //                                                        //
                    //--------------------------------------------------------//

                    continuation = true;

                    contType = _contType;

                    binDataLen = bufRem;
                    _segRem = _segRem - bufRem;
                    hddrDataRem = hddrDataRem - bufRem;
                    hddrRem = hddrRem - bufRem;

                    _linkData.setContinuation (contType);
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Remainder of segment is in buffer.                     //
                    //                                                        //
                    //--------------------------------------------------------//

                    binDataLen = _segRem;
                    _segRem = 0;
                    hddrDataRem = hddrDataRem - binDataLen;
                    hddrRem = hddrRem - binDataLen;
                }

                if ((binDataLen) != 0)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Some, or all, of the segmented data is contained       //
                    // within the current 'block'.                            //
                    //                                                        //
                    //--------------------------------------------------------//

                    PrnParseData.processBinary (
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _buf,
                        _fileOffset,
                        bufOffset,
                        binDataLen,
                        "Segment data",
                        _showBinData,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);

                    if (_PCL)
                    {
                        for (Int32 i = 0; i < binDataLen; i++)
                        {
                            hddrChksVal += _buf[bufOffset + i];
                        }
                    }

                    bufRem = bufRem - binDataLen;
                    bufOffset = bufOffset + binDataLen;
                }
            }

            if ((hddrDataRem == 0) && (_validSegs))
            {
                //------------------------------------------------------------//
                //                                                            //
                // All segmented data processed.                              //
                //                                                            //
                //------------------------------------------------------------//

                valid = true;
            }

            return continuation;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s S e g N u l l                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Segment type: Null                                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processSegNull(Int32 segSize,
                                    Int32 segHddrLen,
                                    ref Int32 bufRem,
                                    ref Int32 bufOffset,
                                    ref Int32 hddrDataRem,
                                    ref Int32 hddrRem,
                                    ref Int32 hddrChksVal)
        {
            String segTypeDesc = "Null";

            PrnParseConstants.eContType contType;

            Int32 baseOffset,
                  dataOffset,
                  minSegSize,
                  minSegLen;

            minSegSize = 0;
            minSegLen = segHddrLen + minSegSize;
            baseOffset = bufOffset + _fileOffset;
            dataOffset = bufOffset + segHddrLen;

            if (bufRem < minSegLen)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Minimum required size not in buffer.                       //
                // Initiate (back-tracking) continuation.                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = _contType;

                _linkData.setBacktrack (contType, - bufRem);
            }
            else
            {
                if (_showBinData)
                {
                    PrnParseData.processBinary (
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _buf,
                        _fileOffset,
                        bufOffset,
                        minSegLen,
                        "Segment header",
                        true,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }

                if (_PCL)
                {
                    for (Int32 i = 0; i < minSegLen; i++)
                    {
                        hddrChksVal += _buf[bufOffset + i];
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Interpret segment header bytes:                            //
                // bytes  0 -  1     segment type                             //
                //        2 -  x-1   segment size  (format 15: x=4;           //
                //                                  format 16: x=6)           //
                //        x -  ..    segment data                             //
                //                                                            //
                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset,
                    _analysisLevel,
                    "Segment type:",
                    "",
                    segTypeDesc);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 2,
                    _analysisLevel,
                    "        size:",
                    "",
                    segSize + " (" +
                    (segSize + segHddrLen) +
                    " including type & size fields))");

                baseOffset += segHddrLen;
 
                //------------------------------------------------------------//
                //                                                            //
                // Adjust offsets and remainders.                             //
                //                                                            //
                //------------------------------------------------------------//

                bufOffset = bufOffset + minSegLen;
                bufRem = bufRem - minSegLen;
                hddrDataRem = hddrDataRem - minSegLen;
                hddrRem = hddrRem - minSegLen;

                //------------------------------------------------------------//
                //                                                            //
                // Remaining binary segment data.                             //
                //                                                            //
                //------------------------------------------------------------//

                if ((segSize - minSegLen) > hddrDataRem)
                {
                    reportError (
                        "Segment (size " + segSize + ") larger than",
                        "remainder (" + hddrDataRem + ") of segmented data",
                        "Header is  internally inconsistent");

                    _segRem = hddrDataRem;
                }
                else
                {
                    _segRem = segSize - minSegSize;

                    if (_segRem != 0)
                    {
                        reportError (
                            "Segment remainder " + _segRem + " non-zero",
                            "", "");
                    }
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s S e g U n k n o w n                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Segment type: Unknown                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processSegUnknown(Int32 segType,
                                       Int32 segSize,
                                       Int32 segHddrLen,
                                       ref Int32 bufRem,
                                       ref Int32 bufOffset,
                                       ref Int32 hddrDataRem,
                                       ref Int32 hddrRem,
                                       ref Int32 hddrChksVal)
        {
            String segTypeDesc = "0x" + segType.ToString("X4") + ": Unknown";

            PrnParseConstants.eContType contType;

            Int32 baseOffset,
                  dataOffset,
                  minSegSize,
                  minSegLen;

            minSegSize = 0;
            minSegLen = segHddrLen + minSegSize;
            baseOffset = bufOffset + _fileOffset;
            dataOffset = bufOffset + segHddrLen;

            if (bufRem < minSegLen)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Minimum required size not in buffer.                       //
                // Initiate (back-tracking) continuation.                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = _contType;

                _linkData.setBacktrack (contType, - bufRem);
            }
            else
            {
                if (_showBinData)
                {
                    PrnParseData.processBinary (
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _buf,
                        _fileOffset,
                        bufOffset,
                        minSegLen,
                        "Segment header",
                        true,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }

                if (_PCL)
                {
                    for (Int32 i = 0; i < minSegLen; i++)
                    {
                        hddrChksVal += _buf[bufOffset + i];
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Interpret segment header bytes:                            //
                // bytes  0 -  1     segment type                             //
                //        2 -  x-1   segment size  (format 15: x=4;           //
                //                                  format 16: x=6)           //
                //        x -  ..    segment data                             //
                //                                                            //
                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset,
                    _analysisLevel,
                    "Segment type:",
                    "",
                    segTypeDesc);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 2,
                    _analysisLevel,
                    "        size:",
                    "",
                    segSize + " (" +
                    (segSize + segHddrLen) +
                    " including type & size fields)");

                baseOffset += segHddrLen;

                //------------------------------------------------------------//


                //------------------------------------------------------------//
                //                                                            //
                // Adjust offsets and remainders.                             //
                //                                                            //
                //------------------------------------------------------------//

                bufOffset = bufOffset + minSegLen;
                bufRem = bufRem - minSegLen;
                hddrDataRem = hddrDataRem - minSegLen;
                hddrRem = hddrRem - minSegLen;

                //------------------------------------------------------------//
                //                                                            //
                // Remaining binary segment data.                             //
                //                                                            //
                //------------------------------------------------------------//

                if ((segSize - minSegLen) > hddrDataRem)
                {
                    reportError (
                        "Segment (size " + segSize + ") larger than",
                        "remainder (" + hddrDataRem + ") of segmented data",
                        "Header is  internally inconsistent");

                    _segRem = hddrDataRem;
                }
                else
                {
                    _segRem = segSize - minSegSize;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s S e g _ A P                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Segment type: Application Support                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processSeg_AP(Int32     segSize,
                                   Int32     segHddrLen,
                                   ref Int32 bufRem,
                                   ref Int32 bufOffset,
                                   ref Int32 hddrDataRem,
                                   ref Int32 hddrRem,
                                   ref Int32 hddrChksVal)
        {
            String segTypeDesc = "AP: Application Support";

            PrnParseConstants.eContType contType;

            Int32 baseOffset,
                  dataOffset,
                  minSegSize,
                  minSegLen;

            minSegSize = 0;   // TODO when we discover the segment format     //
            minSegLen = segHddrLen + minSegSize;
            baseOffset = bufOffset + _fileOffset;
            dataOffset = bufOffset + segHddrLen;

            if (bufRem < minSegLen)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Minimum required size not in buffer.                       //
                // Initiate (back-tracking) continuation.                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = _contType;

                _linkData.setBacktrack (contType, - bufRem);
            }
            else
            {
                if (_showBinData)
                {
                    PrnParseData.processBinary (
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _buf,
                        _fileOffset,
                        bufOffset,
                        minSegLen,
                        "Segment header",
                        true,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }

                if (_PCL)
                {
                    for (Int32 i = 0; i < minSegLen; i++)
                    {
                        hddrChksVal += _buf[bufOffset + i];
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Interpret segment header bytes:                            //
                // bytes  0 -  1     segment type                             //
                //        2 -  x-1   segment size  (format 15: x=4;           //
                //                                  format 16: x=6)           //
                //        x -  ..    segment data                             //
                //                                                            //
                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset,
                    _analysisLevel,
                    "Segment type:",
                    "",
                    segTypeDesc);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 2,
                    _analysisLevel,
                    "        size:",
                    "",
                    segSize + " (" +
                    (segSize + segHddrLen) +
                    " including type & size fields)");

                baseOffset += segHddrLen;

                //------------------------------------------------------------//
                //                                                            //
                // TODO if we ever discover the segment format                //
                //                                                            //
                //------------------------------------------------------------//


                //------------------------------------------------------------//
                //                                                            //
                // Adjust offsets and remainders.                             //
                //                                                            //
                //------------------------------------------------------------//

                bufOffset = bufOffset + minSegLen;
                bufRem    = bufRem - minSegLen;
                hddrDataRem = hddrDataRem - minSegLen;
                hddrRem = hddrRem - minSegLen;

                //------------------------------------------------------------//
                //                                                            //
                // Remaining binary segment data.                             //
                //                                                            //
                //------------------------------------------------------------//

                if ((segSize - minSegLen) > hddrDataRem)
                {
                    reportError (
                        "Segment (size " + segSize + ") larger than",
                        "remainder (" + hddrDataRem + ") of segmented data",
                        "Header is  internally inconsistent");

                    _segRem = hddrDataRem;
                }
                else
                {
                    _segRem = segSize - minSegSize;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s S e g _ B R                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Segment type: Bitmap Resolution                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processSeg_BR(Int32 segSize,
                                   Int32 segHddrLen,
                                   ref Int32 bufRem,
                                   ref Int32 bufOffset,
                                   ref Int32 hddrDataRem,
                                   ref Int32 hddrRem,
                                   ref Int32 hddrChksVal)
        {
            String segTypeDesc = "BR: Bitmap Resolution";

            PrnParseConstants.eContType contType;
            
            Int32 baseOffset,
                  dataOffset,
                  minSegSize,
                  minSegLen;

            UInt16 ui16a;

            minSegSize = 4;
            minSegLen  = segHddrLen + minSegSize;
            baseOffset = bufOffset + _fileOffset;
            dataOffset = bufOffset + segHddrLen;

            if (bufRem < minSegLen)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Minimum required size not in buffer.                       //
                // Initiate (back-tracking) continuation.                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = _contType;

                _linkData.setBacktrack (contType, - bufRem);
            }
            else
            {
                if (_showBinData)
                {
                    PrnParseData.processBinary (
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _buf,
                        _fileOffset,
                        bufOffset,
                        minSegLen,
                        "Segment header",
                        true,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }

                if (_PCL)
                {
                    for (Int32 i = 0; i < minSegLen; i++)
                    {
                        hddrChksVal += _buf[bufOffset + i];
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Interpret segment header bytes:                            //
                // bytes  0 -  1     segment type                             //
                //        2 -  x-1   segment size  (format 15: x=4;           //
                //                                  format 16: x=6)           //
                //        x -  ..    segment data                             //
                //                                                            //
                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset,
                    _analysisLevel,
                    "Segment type:",
                    "",
                    segTypeDesc);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 2,
                    _analysisLevel,
                    "        size:",
                    "",
                    segSize + " (" +
                    (segSize + segHddrLen) +
                    " including type & size fields)");

                baseOffset += segHddrLen;

                //------------------------------------------------------------//
                //                                                            //
                // bytes x  -x+1     X Resolution                             //
                //       x+2-x+3     Y Resolution                             //
                //                                                            //
                //------------------------------------------------------------//
            
                ui16a = (UInt16)((_buf[dataOffset    ] * 256) +
                                  _buf[dataOffset + 1]);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset,
                    _analysisLevel,
                    "        data:",
                    "X Resolution:",
                    ui16a + " dots per inch");

                ui16a = (UInt16)((_buf[dataOffset + 2] * 256) +
                                  _buf[dataOffset + 3]);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 2,
                    _analysisLevel,
                    "        data:",
                    "Y Resolution:",
                    ui16a + " dots per inch");

                //------------------------------------------------------------//
                //                                                            //
                // Adjust offsets and remainders.                             //
                //                                                            //
                //------------------------------------------------------------//

                bufOffset    = bufOffset    + minSegLen;
                bufRem       = bufRem       - minSegLen;
                hddrDataRem = hddrDataRem - minSegLen;
                hddrRem     = hddrRem     - minSegLen;

                //------------------------------------------------------------//
                //                                                            //
                // Remaining binary segment data.                             //
                //                                                            //
                //------------------------------------------------------------//

                if ((segSize - minSegLen) > hddrDataRem)
                {
                    reportError (
                        "Segment (size " + segSize + ") larger than",
                        "remainder (" + hddrDataRem + ") of segmented data",
                        "Header is  internally inconsistent");

                    _segRem = hddrDataRem;
                }
                else
                {
                    _segRem = segSize - minSegSize;

                    if (_segRem != 0)
                    {
                        reportError (
                            "Segment remainder " + _segRem + " non-zero",
                            "", "");
                    }
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s S e g _ C C                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Segment type: Character Complement                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processSeg_CC(Int32 segSize,
                                   Int32 segHddrLen,
                                   ref Int32 bufRem,
                                   ref Int32 bufOffset,
                                   ref Int32 hddrDataRem,
                                   ref Int32 hddrRem,
                                   ref Int32 hddrChksVal)
        {
            String segTypeDesc = "CC: Character Complement";

            PrnParseConstants.eContType contType;

            Int32 baseOffset,
                  dataOffset,
                  minSegSize,
                  minSegLen;

            minSegSize = 8;
            minSegLen = segHddrLen + minSegSize;
            baseOffset = bufOffset + _fileOffset;
            dataOffset = bufOffset + segHddrLen;

            if (bufRem < minSegLen)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Minimum required size not in buffer.                       //
                // Initiate (back-tracking) continuation.                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = _contType;

                _linkData.setBacktrack (contType, -bufRem);
            }
            else
            {
                if (_showBinData)
                {
                    PrnParseData.processBinary (
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _buf,
                        _fileOffset,
                        bufOffset,
                        minSegLen,
                        "Segment header",
                        true,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }

                if (_PCL)
                {
                    for (Int32 i = 0; i < minSegLen; i++)
                    {
                        hddrChksVal += _buf[bufOffset + i];
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Interpret segment header bytes:                            //
                // bytes  0 -  1     segment type                             //
                //        2 -  x-1   segment size  (format 15: x=4;           //
                //                                  format 16: x=6)           //
                //        x -  ..    segment data                             //
                //                                                            //
                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset,
                    _analysisLevel,
                    "Segment type:",
                    "",
                    segTypeDesc);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 2,
                    _analysisLevel,
                    "        size:",
                    "",
                    segSize + " (" +
                    (segSize + segHddrLen) +
                    " including type & size fields)");

                baseOffset += segHddrLen;

                //------------------------------------------------------------//
                //                                                            //
                // Segment format is the standard Character Complement data.  //
                //                                                            //
                //------------------------------------------------------------//

                decodeCharCompReq(
                    true,
                    false,
                    _PCL,
                    _fileOffset,
                    _buf,
                    dataOffset,
                    _linkData,
                    _options,
                    _table);

                //------------------------------------------------------------//
                //                                                            //
                // Adjust offsets and remainders.                             //
                //                                                            //
                //------------------------------------------------------------//

                bufOffset = bufOffset + minSegLen;
                bufRem = bufRem - minSegLen;
                hddrDataRem = hddrDataRem - minSegLen;
                hddrRem = hddrRem - minSegLen;

                //------------------------------------------------------------//
                //                                                            //
                // Remaining binary segment data.                             //
                //                                                            //
                //------------------------------------------------------------//

                if ((segSize - minSegLen) > hddrDataRem)
                {
                    reportError (
                        "Segment (size " + segSize + ") larger than",
                        "remainder (" + hddrDataRem + ") of segmented data",
                        "Header is  internally inconsistent");

                    _segRem = hddrDataRem;
                }
                else
                {
                    _segRem = segSize - minSegSize;
                }
            }
        }
            
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s S e g _ C E                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Segment type: Character Enhancement                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processSeg_CE(Int32 segSize,
                                   Int32 segHddrLen,
                                   ref Int32 bufRem,
                                   ref Int32 bufOffset,
                                   ref Int32 hddrDataRem,
                                   ref Int32 hddrRem,
                                   ref Int32 hddrChksVal)
        {
            String segTypeDesc = "CE: Character Enhancement";

            PrnParseConstants.eContType contType;

            Int32 baseOffset,
                  dataOffset,
                  minSegSize,
                  minSegLen;

            minSegSize = 0;   // TODO if we ever discover the segment format  //
            minSegLen = segHddrLen + minSegSize;
            baseOffset = bufOffset + _fileOffset;
            dataOffset = bufOffset + segHddrLen;

            if (bufRem < minSegLen)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Minimum required size not in buffer.                       //
                // Initiate (back-tracking) continuation.                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = _contType;

                _linkData.setBacktrack (contType, -bufRem);
            }
            else
            {
                if (_showBinData)
                {
                    PrnParseData.processBinary (
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _buf,
                        _fileOffset,
                        bufOffset,
                        minSegLen,
                        "Segment header",
                        true,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }

                if (_PCL)
                {
                    for (Int32 i = 0; i < minSegLen; i++)
                    {
                        hddrChksVal += _buf[bufOffset + i];
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Interpret segment header bytes:                            //
                // bytes  0 -  1     segment type                             //
                //        2 -  x-1   segment size  (format 15: x=4;           //
                //                                  format 16: x=6)           //
                //        x -  ..    segment data                             //
                //                                                            //
                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset,
                    _analysisLevel,
                    "Segment type:",
                    "",
                    segTypeDesc);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 2,
                    _analysisLevel,
                    "        size:",
                    "",
                    segSize + " (" +
                    (segSize + segHddrLen) +
                    " including type & size fields)");

                baseOffset += segHddrLen;

                //------------------------------------------------------------//
                //                                                            //
                // TODO if we ever discover the segment format                //
                //                                                            //
                //------------------------------------------------------------//


                //------------------------------------------------------------//
                //                                                            //
                // Adjust offsets and remainders.                             //
                //                                                            //
                //------------------------------------------------------------//

                bufOffset = bufOffset + minSegLen;
                bufRem = bufRem - minSegLen;
                hddrDataRem = hddrDataRem - minSegLen;
                hddrRem = hddrRem - minSegLen;

                //------------------------------------------------------------//
                //                                                            //
                // Remaining binary segment data.                             //
                //                                                            //
                //------------------------------------------------------------//

                if ((segSize - minSegLen) > hddrDataRem)
                {
                    reportError (
                        "Segment (size " + segSize + ") larger than",
                        "remainder (" + hddrDataRem + ") of segmented data",
                        "Header is  internally inconsistent");

                    _segRem = hddrDataRem;
                }
                else
                {
                    _segRem = segSize - minSegSize;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s S e g _ C P                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Segment type: Copyright                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processSeg_CP(Int32 segSize,
                                   Int32 segHddrLen,
                                   ref Int32 bufRem,
                                   ref Int32 bufOffset,
                                   ref Int32 hddrDataRem,
                                   ref Int32 hddrRem,
                                   ref Int32 hddrChksVal)
        {
            String segTypeDesc = "CP: Copyright";

            const Int32 sliceMax = 50;

            PrnParseConstants.eContType contType;

            Boolean firstLine;

            String textA,
                   textB;

            Int32 baseOffset,
                  dataOffset,
                  minSegSize,
                  minSegLen;
            
            Int32 cpyRem,
                  sliceLen,
                  cpyOffset;

            minSegSize = segSize;
            minSegLen = segHddrLen + minSegSize;
            baseOffset = bufOffset + _fileOffset;
            dataOffset = bufOffset + segHddrLen;

            if (bufRem < minSegLen)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Minimum required size not in buffer.                       //
                // Initiate (back-tracking) continuation.                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = _contType;

                _linkData.setBacktrack (contType, -bufRem);
            }
            else
            {
                if (_showBinData)
                {
                    PrnParseData.processBinary (
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _buf,
                        _fileOffset,
                        bufOffset,
                        minSegLen,
                        "Segment header",
                        true,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }

                if (_PCL)
                {
                    for (Int32 i = 0; i < minSegLen; i++)
                    {
                        hddrChksVal += _buf[bufOffset + i];
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Interpret segment header bytes:                            //
                // bytes  0 -  1     segment type                             //
                //        2 -  x-1   segment size  (format 15: x=4;           //
                //                                  format 16: x=6)           //
                //        x -  ..    segment data                             //
                //                                                            //
                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset,
                    _analysisLevel,
                    "Segment type:",
                    "",
                    segTypeDesc);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 2,
                    _analysisLevel,
                    "        size:",
                    "",
                    segSize + " (" +
                    (segSize + segHddrLen) +
                    " including type & size fields)");

                baseOffset += segHddrLen;

                //------------------------------------------------------------//
                //                                                            //
                //        x -  ..    Copyright data; treat as an array of     //
                //                   ASCII characters.                        //
                //                                                            //
                //------------------------------------------------------------//

                firstLine = true;
                cpyRem = segSize;
                cpyOffset = 0;

                while (cpyRem > 0)
                {
                    if (cpyRem > sliceMax)
                        sliceLen = sliceMax;
                    else
                        sliceLen = cpyRem;

                    if (firstLine)
                    {
                        textA = "        data:";
                        textB = "Copyright:";
                    }
                    else
                    {
                        textA = "";
                        textB = "";
                    }

                    PrnParseCommon.addDataRow (
                        _rowType,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _indxOffsetFormat,
                        baseOffset + cpyOffset,
                        _analysisLevel,
                        textA,
                        textB,
                        _ascii.GetString (_buf,
                                          dataOffset + cpyOffset,
                                          sliceLen));

                    cpyRem -= sliceLen;
                    cpyOffset += sliceLen;
                    firstLine = false;
                }

                //------------------------------------------------------------//
                //                                                            //
                // Adjust offsets and remainders.                             //
                //                                                            //
                //------------------------------------------------------------//

                bufOffset = bufOffset + minSegLen;
                bufRem = bufRem - minSegLen;
                hddrDataRem = hddrDataRem - minSegLen;
                hddrRem = hddrRem - minSegLen;

                //------------------------------------------------------------//
                //                                                            //
                // Remaining binary segment data.                             //
                //                                                            //
                //------------------------------------------------------------//

                if ((segSize - minSegLen) > hddrDataRem)
                {
                    reportError (
                        "Segment (size " + segSize + ") larger than",
                        "remainder (" + hddrDataRem + ") of segmented data",
                        "Header is  internally inconsistent");

                    _segRem = hddrDataRem;
                }
                else
                {
                    _segRem = segSize - minSegSize;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s S e g _ G C                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Segment type: Galley Character                                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processSeg_GC(Int32 segSize,
                                   Int32 segHddrLen,
                                   ref Int32 bufRem,
                                   ref Int32 bufOffset,
                                   ref Int32 hddrDataRem,
                                   ref Int32 hddrRem,
                                   ref Int32 hddrChksVal)
        {
            String segTypeDesc = "GC: Galley Character";

            PrnParseConstants.eContType contType;

            Int32 baseOffset,
                  dataOffset,
                  minSegSize,
                  varSegSize,
                  minSegLen;

            UInt16 ui16a,
                   numRegions = 0;

            Boolean numRegionsOK = false;

            minSegSize = 6;
            varSegSize = 0;
            minSegLen = segHddrLen + minSegSize;
            baseOffset = bufOffset + _fileOffset;
            dataOffset = bufOffset + segHddrLen;

            if (bufRem < minSegLen)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Minimum required size not in buffer.                       //
                // Initiate (back-tracking) continuation.                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = _contType;

                _linkData.setBacktrack (contType, - bufRem);
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // Obtain 'Number of Regions' value from header.              //
                //                                                            //
                //------------------------------------------------------------//

                numRegions = (UInt16)((_buf[dataOffset + 4] * 256) +
                                       _buf[dataOffset + 5]);

                varSegSize = 6 * numRegions;

                if ((minSegLen + varSegSize) <= PrnParseConstants.bufSize)
                {
                    minSegSize += varSegSize;
                    minSegLen += varSegSize;
                    numRegionsOK = true;
                }
                else
                {
                    numRegionsOK = false;
                    _validSegs = false;
                }
            }

            if (bufRem < minSegLen)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Minimum required size not in buffer.                       //
                // Initiate (back-tracking) continuation.                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = _contType;

                _linkData.setBacktrack (contType, - bufRem);
            }
            else
            {
                if (_showBinData)
                {
                    PrnParseData.processBinary (
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _buf,
                        _fileOffset,
                        bufOffset,
                        minSegLen,
                        "Segment header",
                        true,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }

                if (_PCL)
                {
                    for (Int32 i = 0; i < minSegLen; i++)
                    {
                        hddrChksVal += _buf[bufOffset + i];
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Interpret segment header bytes:                            //
                // bytes  0 -  1     segment type                             //
                //        2 -  x-1   segment size  (format 15: x=4;           //
                //                                  format 16: x=6)           //
                //        x -  ..    segment data                             //
                //                                                            //
                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset,
                    _analysisLevel,
                    "Segment type:",
                    "",
                    segTypeDesc);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 2,
                    _analysisLevel,
                    "        size:",
                    "",
                    segSize + " (" +
                    (segSize + segHddrLen) +
                    " including type & size fields)");

                baseOffset += segHddrLen;

                //------------------------------------------------------------//
                //                                                            //
                // bytes x  -x+1     Format (should be zero)                  //
                //                                                            //
                //------------------------------------------------------------//

                ui16a = (UInt16)((_buf[dataOffset    ] * 256) +
                                  _buf[dataOffset + 1]);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset,
                    _analysisLevel,
                    "        data:",
                    "Format:",
                    ui16a.ToString());

                //------------------------------------------------------------//
                //                                                            //
                // bytes x+2-x+3     Default Galley Character                 //
                //                                                            //
                //------------------------------------------------------------//

                ui16a = (UInt16)((_buf[dataOffset + 2] * 256) +
                                  _buf[dataOffset + 3]);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 2,
                    _analysisLevel,
                    "        data:",
                    "Default Galley:",
                    "0x" + ui16a.ToString("X4"));

                //------------------------------------------------------------//
                //                                                            //
                // bytes x+4-x+5     Number of Regions                        //
                //                                                            //
                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 4,
                    _analysisLevel,
                    "        data:",
                    "Region Count:",
                    numRegions.ToString());

                if (numRegionsOK)
                {

                    //--------------------------------------------------------//
                    //                                                        //
                    // bytes x+6- ..     Region N definitions                 //
                    //                   numRegions entries, each of format:  //
                    //                   bytes  0 -  1     Region Start Code  //
                    //                   bytes  2 -  3     Region End Code    //
                    //                   bytes  4 -  5     Region Galley Char //
                    //                                                        //
                    //--------------------------------------------------------//

                    for (Int32 i = 0; i < numRegions; i++)
                    {
                        Int32 j = i * 6;

                        ui16a = (UInt16) ((_buf[dataOffset + 6 + j] * 256) +
                                          _buf[dataOffset + 7 + j]);

                        PrnParseCommon.addDataRow (
                            _rowType,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            _indxOffsetFormat,
                            baseOffset + 6 + j,
                            _analysisLevel,
                            "        data:",
                            "Region Start:",
                            "0x" + ui16a.ToString ("X4"));

                        ui16a = (UInt16) ((_buf[dataOffset + 8 + j] * 256) +
                                          _buf[dataOffset + 9 + j]);

                        PrnParseCommon.addDataRow (
                            _rowType,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            _indxOffsetFormat,
                            baseOffset + 8 + j,
                            _analysisLevel,
                            "        data:",
                            "       End:",
                            "0x" + ui16a.ToString ("X4"));

                        ui16a = (UInt16) ((_buf[dataOffset + 10 + j] * 256) +
                                          _buf[dataOffset + 11 + j]);

                        PrnParseCommon.addDataRow (
                            _rowType,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            _indxOffsetFormat,
                            baseOffset + 10 + j,
                            _analysisLevel,
                            "        data:",
                            "       Galley:",
                            "0x" + ui16a.ToString ("X4"));
                    }
                }
                else
                {
                    reportError (
                        "Possibly corrupt: 'Region Count' value " + numRegions,
                        "makes minimum segment header size " +
                            (minSegLen + varSegSize) + " bytes",
                        "This is larger than application buffer size of " +
                            PrnParseConstants.bufSize + " bytes");
                }

                //------------------------------------------------------------//
                //                                                            //
                // Adjust offsets and remainders.                             //
                //                                                            //
                //------------------------------------------------------------//

                bufOffset = bufOffset + minSegLen;
                bufRem = bufRem - minSegLen;
                hddrDataRem = hddrDataRem - minSegLen;
                hddrRem = hddrRem - minSegLen;

                //------------------------------------------------------------//
                //                                                            //
                // Remaining binary segment data.                             //
                //                                                            //
                //------------------------------------------------------------//

                if ((segSize - minSegLen) > hddrDataRem)
                {
                    reportError (
                        "Segment (size " + segSize + ") larger than",
                        "remainder (" + hddrDataRem + ") of segmented data",
                        "Header is  internally inconsistent");

                    _segRem = hddrDataRem;
                }
                else
                {
                    _segRem = segSize - minSegSize;

                    if (_segRem != 0)
                    {
                        reportError (
                            "Segment remainder " + _segRem + " non-zero",
                            "", "");
                    }
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s S e g _ G I                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Segment type: Global Intellifont                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processSeg_GI(Int32 segSize,
                                   Int32 segHddrLen,
                                   ref Int32 bufRem,
                                   ref Int32 bufOffset,
                                   ref Int32 hddrDataRem,
                                   ref Int32 hddrRem,
                                   ref Int32 hddrChksVal)
        {
            String segTypeDesc = "GI: Global Intellifont";

            PrnParseConstants.eContType contType;

            Int32 baseOffset,
                  dataOffset,
                  minSegSize,
                  minSegLen;

            minSegSize = 0;   // TODO if we ever discover the segment format  //
            minSegLen = segHddrLen + minSegSize;
            baseOffset = bufOffset + _fileOffset;
            dataOffset = bufOffset + segHddrLen;

            if (bufRem < minSegLen)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Minimum required size not in buffer.                       //
                // Initiate (back-tracking) continuation.                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = _contType;

                _linkData.setBacktrack (contType, -bufRem);
            }
            else
            {
                if (_showBinData)
                {
                    PrnParseData.processBinary (
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _buf,
                        _fileOffset,
                        bufOffset,
                        minSegLen,
                        "Segment header",
                        true,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }

                if (_PCL)
                {
                    for (Int32 i = 0; i < minSegLen; i++)
                    {
                        hddrChksVal += _buf[bufOffset + i];
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Interpret segment header bytes:                            //
                // bytes  0 -  1     segment type                             //
                //        2 -  x-1   segment size  (format 15: x=4;           //
                //                                  format 16: x=6)           //
                //        x -  ..    segment data                             //
                //                                                            //
                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset,
                    _analysisLevel,
                    "Segment type:",
                    "",
                    segTypeDesc);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 2,
                    _analysisLevel,
                    "        size:",
                    "",
                    segSize + " (" +
                    (segSize + segHddrLen) +
                    " including type & size fields)");

                baseOffset += segHddrLen;

                //------------------------------------------------------------//


                //------------------------------------------------------------//
                //                                                            //
                // Adjust offsets and remainders.                             //
                //                                                            //
                //------------------------------------------------------------//

                bufOffset = bufOffset + minSegLen;
                bufRem = bufRem - minSegLen;
                hddrDataRem = hddrDataRem - minSegLen;
                hddrRem = hddrRem - minSegLen;

                //------------------------------------------------------------//
                //                                                            //
                // Remaining binary segment data.                             //
                //                                                            //
                //------------------------------------------------------------//

                if ((segSize - minSegLen) > hddrDataRem)
                {
                    reportError (
                        "Segment (size " + segSize + ") larger than",
                        "remainder (" + hddrDataRem + ") of segmented data",
                        "Header is  internally inconsistent");

                    _segRem = hddrDataRem;
                }
                else
                {
                    _segRem = segSize - minSegSize;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s S e g _ G T                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Segment type: Global TrueType                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processSeg_GT(Int32 segSize,
                                   Int32 segHddrLen,
                                   ref Int32 bufRem,
                                   ref Int32 bufOffset,
                                   ref Int32 hddrDataRem,
                                   ref Int32 hddrRem,
                                   ref Int32 hddrChksVal)
        {
            String segTypeDesc = "GT: Global TrueType";

            PrnParseConstants.eContType contType;

            Int32 baseOffset,
                  dataOffset,
                  minSegSize,
                  varSegSize,
                  minSegLen;

            Int32 tableOffset,
                  padBytes;

            UInt32 ui32a,
                   ui32b;

            UInt32 offset,
                   size,
                   padSize;

            UInt16 ui16a,
                   numTables = 0;

            Boolean numTablesOK = false;

            minSegSize = 12;
            varSegSize = 0;
            minSegLen = segHddrLen + minSegSize;
            baseOffset = bufOffset + _fileOffset;
            dataOffset = bufOffset + segHddrLen;

            if (bufRem < minSegLen)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Minimum required size not in buffer.                       //
                // Initiate (back-tracking) continuation.                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = _contType;

                _linkData.setBacktrack (contType, - bufRem);
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // Obtain 'Number of Tables' value from SFNT Directory Header //
                //                                                            //
                //------------------------------------------------------------//
                
                numTables = (UInt16)((_buf[dataOffset + 4] * 256) +
                                      _buf[dataOffset + 5]);

                varSegSize = 16 * numTables;

                if ((minSegLen + varSegSize) <= PrnParseConstants.bufSize)
                {
                    minSegSize += varSegSize;
                    minSegLen  += varSegSize;
                    numTablesOK = true;
                }
                else
                {
                    numTablesOK = false;
                    _validSegs  = false;
                }
            }

            if (bufRem < minSegLen)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Minimum required size not in buffer.                       //
                // Initiate (back-tracking) continuation.                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = _contType;

                _linkData.setBacktrack (contType, -bufRem);
            }
            else
            {
                if (_showBinData)
                {
                    PrnParseData.processBinary (
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _buf,
                        _fileOffset,
                        bufOffset,
                        minSegLen,
                        "Segment header",
                        true,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }

                if (_PCL)
                {
                    for (Int32 i = 0; i < minSegLen; i++)
                    {
                        hddrChksVal += _buf[bufOffset + i];
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Interpret segment header bytes:                            //
                // bytes  0 -  1     segment type                             //
                //        2 -  x-1   segment size  (format 15: x=4;           //
                //                                  format 16: x=6)           //
                //        x -  ..    segment data                             //
                //                                                            //
                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset,
                    _analysisLevel,
                    "Segment type:",
                    "",
                    segTypeDesc);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 2,
                    _analysisLevel,
                    "        size:",
                    "",
                    segSize + " (" +
                    (segSize + segHddrLen) +
                    " including type & size fields)");

                baseOffset += segHddrLen;

                //------------------------------------------------------------//
                //                                                            //
                // Interpret SFNT Directory Header.                           //
                // bytes  x -  x+3   SFNT Version                             //
                //                                                            //
                //------------------------------------------------------------//

                tableOffset = baseOffset;

                ui32a = (UInt32) ((_buf[dataOffset] * 65536 * 256) +
                                 (_buf[dataOffset + 1] * 65536) +
                                 (_buf[dataOffset + 2] * 256) +
                                  _buf[dataOffset + 3]);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset,
                    _analysisLevel,
                    "        data:",
                    "SFNT version:",
                    "0x" + ui32a.ToString ("X8"));

                //------------------------------------------------------------//
                //                                                            //
                // bytes x+4-x+5     Number of Tables                         //
                //                                                            //
                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 4,
                    _analysisLevel,
                    "        data:",
                    "Table Count:",
                    numTables.ToString ());

                //------------------------------------------------------------//
                //                                                            //
                // bytes x+6-x+7     Search Range                             //
                //                   This should be:                          //
                //                   (max. power of 2 <= numTables) * 16      //
                //                                                            //
                //------------------------------------------------------------//

                ui16a = (UInt16) ((_buf[dataOffset + 6] * 256) +
                                  _buf[dataOffset + 7]);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 6,
                    _analysisLevel,
                    "        data:",
                    "Search Range:",
                    ui16a.ToString ());

                //------------------------------------------------------------//
                //                                                            //
                // bytes x+8-x+9     Entry Selector                           //
                //                   This should be:                          //
                //                   Log-base-2 (max. power of 2 <= numTables)//
                //                                                            //
                //------------------------------------------------------------//

                ui16a = (UInt16) ((_buf[dataOffset + 8] * 256) +
                                  _buf[dataOffset + 9]);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 8,
                    _analysisLevel,
                    "        data:",
                    "Entry Selector:",
                    ui16a.ToString ());

                //------------------------------------------------------------//
                //                                                            //
                // bytes x+10-x+11   Range Shift                              //
                //                   This should be:                          //
                //                   (numTables * 16) - SearchRange           //
                //                                                            //
                //------------------------------------------------------------//

                ui16a = (UInt16) ((_buf[dataOffset + 10] * 256) +
                                  _buf[dataOffset + 11]);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 10,
                    _analysisLevel,
                    "        data:",
                    "Range Shift:",
                    ui16a.ToString ());

                if (numTablesOK)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // bytes x+12- ..    Table directory entries              //
                    //                   numTables entries, each of format:   //
                    //                   bytes  0 -  3     Tag                //
                    //                   bytes  4 -  7     CheckSum           //
                    //                   bytes  8 - 11     Offset             //
                    //                   bytes 12 - 15     Size               //
                    //                                                        //
                    //--------------------------------------------------------//

                    for (Int32 i = 0; i < numTables; i++)
                    {
                        Int32 j = i * 16;

                        //----------------------------------------------------//
                        //                                                    //
                        // Tag                                                //
                        //                                                    //
                        //----------------------------------------------------//

                        PrnParseCommon.addDataRow (
                            _rowType,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            _indxOffsetFormat,
                            baseOffset + 12 + j,
                            _analysisLevel,
                            "        data:",
                            "Table Tag:",
                            _ascii.GetString (_buf, dataOffset + 12 + j, 4));

                        //----------------------------------------------------//
                        //                                                    //
                        // Checksum                                           //
                        //                                                    //
                        //----------------------------------------------------//

                        ui32a = (UInt32) ((_buf[dataOffset + 16 + j] * 65536 * 256) +
                                         (_buf[dataOffset + 17 + j] * 65536) +
                                         (_buf[dataOffset + 18 + j] * 256) +
                                          _buf[dataOffset + 19 + j]);

                        PrnParseCommon.addDataRow (
                            _rowType,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            _indxOffsetFormat,
                            baseOffset + 16 + j,
                            _analysisLevel,
                            "        data:",
                            "      Checksum:",
                            "0x" + ui32a.ToString ("X8"));

                        //----------------------------------------------------//
                        //                                                    //
                        // Offset                                             //
                        //                                                    //
                        //----------------------------------------------------//

                        offset = (UInt32) ((_buf[dataOffset + 20 + j] * 65536 * 256) +
                                           (_buf[dataOffset + 21 + j] * 65536) +
                                           (_buf[dataOffset + 22 + j] * 256) +
                                            _buf[dataOffset + 23 + j]);

                        ui32b = (UInt32) (tableOffset + offset);

                        if (offset == 0)
                            PrnParseCommon.addDataRow (
                                _rowType,
                                _table,
                                PrnParseConstants.eOvlShow.None,
                                _indxOffsetFormat,
                                baseOffset + 20 + j,
                                _analysisLevel,
                                "        data:",
                                "      Offset:",
                                "0");
                        else
                        {
                            PrnParseCommon.addDataRow (
                                _rowType,
                                _table,
                                PrnParseConstants.eOvlShow.None,
                                _indxOffsetFormat,
                                baseOffset + 20 + j,
                                _analysisLevel,
                                "        data:",
                                "      Offset:",
                                offset.ToString () + " relative (= " +
                                ui32b.ToString () + " absolute)");
                        }

                        //----------------------------------------------------//
                        //                                                    //
                        // Size                                               //
                        // Where the size is not a multiple of 4 bytes, the   //
                        // table should be padded to the next multiple.       //
                        //                                                    //
                        //----------------------------------------------------//

                        size = (UInt32) ((_buf[dataOffset + 24 + j] * 65536 * 256) +
                                         (_buf[dataOffset + 25 + j] * 65536) +
                                         (_buf[dataOffset + 26 + j] * 256) +
                                          _buf[dataOffset + 27 + j]);

                        padBytes = (Int32)(size % 4);

                        if (padBytes == 0)
                        {
                            PrnParseCommon.addDataRow (
                                _rowType,
                                _table,
                                PrnParseConstants.eOvlShow.None,
                                _indxOffsetFormat,
                                baseOffset + 24 + j,
                                _analysisLevel,
                                "        data:",
                                "      Size:",
                                size.ToString ());
                        }
                        else
                        {
                            padBytes = 4 - padBytes;
                            padSize = (UInt32)(size + padBytes);

                            PrnParseCommon.addDataRow (
                                _rowType,
                                _table,
                                PrnParseConstants.eOvlShow.None,
                                _indxOffsetFormat,
                                baseOffset + 24 + j,
                                _analysisLevel,
                                "        data:",
                                "      Size:",
                                size.ToString ()    + " (padded size = " +
                                padSize.ToString () + ")");
                        }

                        if ((offset > segSize) || ((offset + size) > segSize))
                        {
                            reportError (
                                "Offset and/or size incompatible with" +
                                    " segment size",
                                "", "");
                        }
                    }
                }
                else
                {
                    reportError (
                        "Possibly corrupt: 'Table Count' value " + numTables,
                        "makes minimum segment header size " +
                            (minSegLen + varSegSize) + " bytes",
                        "This is larger than application buffer size of " +
                            PrnParseConstants.bufSize + " bytes");
                }

                //------------------------------------------------------------//
                //                                                            //
                // Adjust offsets and remainders.                             //
                //                                                            //
                //------------------------------------------------------------//

                bufOffset = bufOffset + minSegLen;
                bufRem = bufRem - minSegLen;
                hddrDataRem = hddrDataRem - minSegLen;
                hddrRem = hddrRem - minSegLen;

                //------------------------------------------------------------//
                //                                                            //
                // Remaining binary segment data.                             //
                //                                                            //
                //------------------------------------------------------------//

                if ((segSize - minSegLen) > hddrDataRem)
                {
                    reportError (
                        "Segment (size " + segSize + ") larger than",
                        "remainder (" + hddrDataRem + ") of segmented data",
                        "Header is  internally inconsistent");

                    _segRem = hddrDataRem;
                }
                else
                {
                    _segRem = segSize - minSegSize;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s S e g _ I F                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Segment type: Intellifont Face                                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processSeg_IF(Int32 segSize,
                                   Int32 segHddrLen,
                                   ref Int32 bufRem,
                                   ref Int32 bufOffset,
                                   ref Int32 hddrDataRem,
                                   ref Int32 hddrRem,
                                   ref Int32 hddrChksVal)
        {
            String segTypeDesc = "IF: Intellifont Face";

            PrnParseConstants.eContType contType;

            Int32 baseOffset,
                  dataOffset,
                  minSegSize,
                  minSegLen;

            minSegSize = 0;   // TODO if we ever discover the segment format  //
            minSegLen = segHddrLen + minSegSize;
            baseOffset = bufOffset + _fileOffset;
            dataOffset = bufOffset + segHddrLen;

            if (bufRem < minSegLen)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Minimum required size not in buffer.                       //
                // Initiate (back-tracking) continuation.                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = _contType;

                _linkData.setBacktrack (contType, -bufRem);
            }
            else
            {
                if (_showBinData)
                {
                    PrnParseData.processBinary (
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _buf,
                        _fileOffset,
                        bufOffset,
                        minSegLen,
                        "Segment header",
                        true,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }

                if (_PCL)
                {
                    for (Int32 i = 0; i < minSegLen; i++)
                    {
                        hddrChksVal += _buf[bufOffset + i];
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Interpret segment header bytes:                            //
                // bytes  0 -  1     segment type                             //
                //        2 -  x-1   segment size  (format 15: x=4;           //
                //                                  format 16: x=6)           //
                //        x -  ..    segment data                             //
                //                                                            //
                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset,
                    _analysisLevel,
                    "Segment type:",
                    "",
                    segTypeDesc);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 2,
                    _analysisLevel,
                    "        size:",
                    "",
                    segSize + " (" +
                    (segSize + segHddrLen) +
                    " including type & size fields)");

                baseOffset += segHddrLen;

                //------------------------------------------------------------//


                //------------------------------------------------------------//
                //                                                            //
                // Adjust offsets and remainders.                             //
                //                                                            //
                //------------------------------------------------------------//

                bufOffset = bufOffset + minSegLen;
                bufRem = bufRem - minSegLen;
                hddrDataRem = hddrDataRem - minSegLen;
                hddrRem = hddrRem - minSegLen;

                //------------------------------------------------------------//
                //                                                            //
                // Remaining binary segment data.                             //
                //                                                            //
                //------------------------------------------------------------//

                if ((segSize - minSegLen) > hddrDataRem)
                {
                    reportError (
                        "Segment (size " + segSize + ") larger than",
                        "remainder (" + hddrDataRem + ") of segmented data",
                        "Header is  internally inconsistent");

                    _segRem = hddrDataRem;
                }
                else
                {
                    _segRem = segSize - minSegSize;
                }
            }
        }


        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s S e g _ P A                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Segment type: Panose Description                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processSeg_PA(Int32 segSize,
                                   Int32 segHddrLen,
                                   ref Int32 bufRem,
                                   ref Int32 bufOffset,
                                   ref Int32 hddrDataRem,
                                   ref Int32 hddrRem,
                                   ref Int32 hddrChksVal)
        {
            String segTypeDesc = "PA: Panose Description";

            PrnParseConstants.eContType contType;

            Byte b;

            Int32 baseOffset,
                  dataOffset,
                  minSegSize,
                  minSegLen;

            String panoseSet;

            minSegSize = 10;
            minSegLen = segHddrLen + minSegSize;
            baseOffset = bufOffset + _fileOffset;
            dataOffset = bufOffset + segHddrLen;

            if (bufRem < minSegLen)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Minimum required size not in buffer.                       //
                // Initiate (back-tracking) continuation.                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = _contType;

                _linkData.setBacktrack (contType, -bufRem);
            }
            else
            {
                if (_showBinData)
                {
                    PrnParseData.processBinary (
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _buf,
                        _fileOffset,
                        bufOffset,
                        minSegLen,
                        "Segment header",
                        true,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }

                if (_PCL)
                {
                    for (Int32 i = 0; i < minSegLen; i++)
                    {
                        hddrChksVal += _buf[bufOffset + i];
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Interpret segment header bytes:                            //
                // bytes  0 -  1     segment type                             //
                //        2 -  x-1   segment size  (format 15: x=4;           //
                //                                  format 16: x=6)           //
                //        x -  ..    segment data                             //
                //                                                            //
                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset,
                    _analysisLevel,
                    "Segment type:",
                    "",
                    segTypeDesc);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 2,
                    _analysisLevel,
                    "        size:",
                    "",
                    segSize + " (" +
                    (segSize + segHddrLen) +
                    " including type & size fields)");

                baseOffset += segHddrLen;

                //------------------------------------------------------------//
                //                                                            //
                // bytes x  -x+9     Panose classification numbers            //
                //                                                            //
                //------------------------------------------------------------//

                panoseSet = "";

                for (Int32 i = 0; i < 10; i++)
                {
                    b = _buf[dataOffset + i];

                    if (panoseSet.Length != 0)
                        panoseSet += "-";

                    panoseSet += b;
                }

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset,
                    _analysisLevel,
                    "        data:",
                    "Panose Class:",
                    panoseSet);

                //------------------------------------------------------------//
                //                                                            //
                // Adjust offsets and remainders.                             //
                //                                                            //
                //------------------------------------------------------------//

                bufOffset = bufOffset + minSegLen;
                bufRem = bufRem - minSegLen;
                hddrDataRem = hddrDataRem - minSegLen;
                hddrRem = hddrRem - minSegLen;

                //------------------------------------------------------------//
                //                                                            //
                // Remaining binary segment data.                             //
                //                                                            //
                //------------------------------------------------------------//

                if ((segSize - minSegLen) > hddrDataRem)
                {
                    reportError (
                        "Segment (size " + segSize + ") larger than",
                        "remainder (" + hddrDataRem + ") of segmented data",
                        "Header is  internally inconsistent");

                    _segRem = hddrDataRem;
                }
                else
                {
                    _segRem = segSize - minSegSize;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s S e g _ P F                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Segment type: PostScript Font                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processSeg_PF(Int32 segSize,
                                   Int32 segHddrLen,
                                   ref Int32 bufRem,
                                   ref Int32 bufOffset,
                                   ref Int32 hddrDataRem,
                                   ref Int32 hddrRem,
                                   ref Int32 hddrChksVal)
        {
            String segTypeDesc = "PF: PostScript Font";

            PrnParseConstants.eContType contType;

            Int32 baseOffset,
                  dataOffset,
                  minSegSize,
                  minSegLen;

            minSegSize = 0;   // TODO if we ever discover the segment format  //
            minSegLen = segHddrLen + minSegSize;
            baseOffset = bufOffset + _fileOffset;
            dataOffset = bufOffset + segHddrLen;

            if (bufRem < minSegLen)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Minimum required size not in buffer.                       //
                // Initiate (back-tracking) continuation.                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = _contType;

                _linkData.setBacktrack (contType, -bufRem);
            }
            else
            {
                if (_showBinData)
                {
                    PrnParseData.processBinary (
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _buf,
                        _fileOffset,
                        bufOffset,
                        minSegLen,
                        "Segment header",
                        true,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }

                if (_PCL)
                {
                    for (Int32 i = 0; i < minSegLen; i++)
                    {
                        hddrChksVal += _buf[bufOffset + i];
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Interpret segment header bytes:                            //
                // bytes  0 -  1     segment type                             //
                //        2 -  x-1   segment size  (format 15: x=4;           //
                //                                  format 16: x=6)           //
                //        x -  ..    segment data                             //
                //                                                            //
                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset,
                    _analysisLevel,
                    "Segment type:",
                    "",
                    segTypeDesc);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 2,
                    _analysisLevel,
                    "        size:",
                    "",
                    segSize + " (" +
                    (segSize + segHddrLen) +
                    " including type & size fields)");

                baseOffset += segHddrLen;

                //------------------------------------------------------------//


                //------------------------------------------------------------//
                //                                                            //
                // Adjust offsets and remainders.                             //
                //                                                            //
                //------------------------------------------------------------//

                bufOffset = bufOffset + minSegLen;
                bufRem = bufRem - minSegLen;
                hddrDataRem = hddrDataRem - minSegLen;
                hddrRem = hddrRem - minSegLen;

                //------------------------------------------------------------//
                //                                                            //
                // Remaining binary segment data.                             //
                //                                                            //
                //------------------------------------------------------------//

                if ((segSize - minSegLen) > hddrDataRem)
                {
                    reportError (
                        "Segment (size " + segSize + ") larger than",
                        "remainder (" + hddrDataRem + ") of segmented data",
                        "Header is  internally inconsistent");

                    _segRem = hddrDataRem;
                }
                else
                {
                    _segRem = segSize - minSegSize;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s S e g _ T F                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Segment type: Type Face String                                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processSeg_TF(Int32 segSize,
                                   Int32 segHddrLen,
                                   ref Int32 bufRem,
                                   ref Int32 bufOffset,
                                   ref Int32 hddrDataRem,
                                   ref Int32 hddrRem,
                                   ref Int32 hddrChksVal)
        {
            String segTypeDesc = "TF: Type Face String";

            PrnParseConstants.eContType contType;

            Int32 baseOffset,
                  dataOffset,
                  minSegSize,
                  minSegLen;

            minSegSize = 0;   // TODO if we ever discover the segment format  //
            minSegLen = segHddrLen + minSegSize;
            baseOffset = bufOffset + _fileOffset;
            dataOffset = bufOffset + segHddrLen;

            if (bufRem < minSegLen)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Minimum required size not in buffer.                       //
                // Initiate (back-tracking) continuation.                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = _contType;

                _linkData.setBacktrack (contType, -bufRem);
            }
            else
            {
                if (_showBinData)
                {
                    PrnParseData.processBinary (
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _buf,
                        _fileOffset,
                        bufOffset,
                        minSegLen,
                        "Segment header",
                        true,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }

                if (_PCL)
                {
                    for (Int32 i = 0; i < minSegLen; i++)
                    {
                        hddrChksVal += _buf[bufOffset + i];
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Interpret segment header bytes:                            //
                // bytes  0 -  1     segment type                             //
                //        2 -  x-1   segment size  (format 15: x=4;           //
                //                                  format 16: x=6)           //
                //        x -  ..    segment data                             //
                //                                                            //
                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset,
                    _analysisLevel,
                    "Segment type:",
                    "",
                    segTypeDesc);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 2,
                    _analysisLevel,
                    "        size:",
                    "",
                    segSize + " (" +
                    (segSize + segHddrLen) +
                    " including type & size fields)");

                baseOffset += segHddrLen;

                //------------------------------------------------------------//


                //------------------------------------------------------------//
                //                                                            //
                // Adjust offsets and remainders.                             //
                //                                                            //
                //------------------------------------------------------------//

                bufOffset = bufOffset + minSegLen;
                bufRem = bufRem - minSegLen;
                hddrDataRem = hddrDataRem - minSegLen;
                hddrRem = hddrRem - minSegLen;

                //------------------------------------------------------------//
                //                                                            //
                // Remaining binary segment data.                             //
                //                                                            //
                //------------------------------------------------------------//

                if ((segSize - minSegLen) > hddrDataRem)
                {
                    reportError (
                        "Segment (size " + segSize + ") larger than",
                        "remainder (" + hddrDataRem + ") of segmented data",
                        "Header is  internally inconsistent");

                    _segRem = hddrDataRem;
                }
                else
                {
                    _segRem = segSize - minSegSize;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s S e g _ V E                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Segment type: Vertical Exclude                                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processSeg_VE(Int32 segSize,
                                   Int32 segHddrLen,
                                   ref Int32 bufRem,
                                   ref Int32 bufOffset,
                                   ref Int32 hddrDataRem,
                                   ref Int32 hddrRem,
                                   ref Int32 hddrChksVal)
        {
            String segTypeDesc = "VE: Vertical Exclude";

            PrnParseConstants.eContType contType;

            Int32 baseOffset,
                  dataOffset,
                  minSegSize,
                  varSegSize,
                  minSegLen;

            UInt16 ui16a,
                   numRanges = 0;

            Boolean numRangesOK = false;

            minSegSize = 2;
            varSegSize = 0;
            minSegLen = segHddrLen + minSegSize;
            baseOffset = bufOffset + _fileOffset;
            dataOffset = bufOffset + segHddrLen;

            if (bufRem < minSegLen)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Minimum required size not in buffer.                       //
                // Initiate (back-tracking) continuation.                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = _contType;

                _linkData.setBacktrack (contType, - bufRem);
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // Obtain 'Number of Ranges' value from header.               //
                //                                                            //
                //------------------------------------------------------------//

                numRanges = _buf[bufOffset + 7];

                varSegSize = 4 * numRanges;

                if ((minSegLen + varSegSize) <= PrnParseConstants.bufSize)
                {
                    minSegSize += varSegSize;
                    minSegLen += varSegSize;
                    numRangesOK = true;
                }
                else
                {
                    numRangesOK = false;
                    _validSegs = false;
                }

            }

            if (bufRem < minSegLen)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Minimum required size not in buffer.                       //
                // Initiate (back-tracking) continuation.                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = _contType;

                _linkData.setBacktrack (contType, - bufRem);
            }
            else
            {
                if (_showBinData)
                {
                    PrnParseData.processBinary (
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _buf,
                        _fileOffset,
                        bufOffset,
                        minSegLen,
                        "Segment header",
                        true,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }

                if (_PCL)
                {
                    for (Int32 i = 0; i < minSegLen; i++)
                    {
                        hddrChksVal += _buf[bufOffset + i];
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Interpret segment header bytes:                            //
                // bytes  0 -  1     segment type                             //
                //        2 -  x-1   segment size  (format 15: x=4;           //
                //                                  format 16: x=6)           //
                //        x -  ..    segment data                             //
                //                                                            //
                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset,
                    _analysisLevel,
                    "Segment type:",
                    "",
                    segTypeDesc);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 2,
                    _analysisLevel,
                    "        size:",
                    "",
                    segSize + " (" +
                    (segSize + segHddrLen) +
                    " including type & size fields)");

                baseOffset += segHddrLen;

                //------------------------------------------------------------//
                //                                                            //
                // bytes  x          Format (should be zero)                  //
                //                                                            //
                //------------------------------------------------------------//

                ui16a = _buf[bufOffset + 6];

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 6,
                    _analysisLevel,
                    "        data:",
                    "Format:",
                    ui16a.ToString());
                
                //------------------------------------------------------------//
                //                                                            //
                // bytes  x+1        Number of Ranges                         //
                //                                                            //
                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 7,
                    _analysisLevel,
                    "        data:",
                    "Range Count:",
                    numRanges.ToString());

                if (numRangesOK)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // bytes x+2 - ..    Range N definitions                  //
                    //                   numRanges entries, each of format:   //
                    //                   bytes  0 -  1     Range First Code   //
                    //                   bytes  2 -  3     Range Last Code    //
                    //                                                        //
                    //--------------------------------------------------------//

                    for (Int32 i = 0; i < numRanges; i++)
                    {
                        Int32 j = i * 4;

                        ui16a = (UInt16) ((_buf[bufOffset + 8 + j] * 256) +
                                           _buf[bufOffset + 9 + j]);

                        PrnParseCommon.addDataRow (
                            _rowType,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            _indxOffsetFormat,
                            baseOffset + 8 + j,
                            _analysisLevel,
                            "        data:",
                            "Range FirstCode:",
                            "0x" + ui16a.ToString ("X4"));

                        j += 2;

                        ui16a = (UInt16) ((_buf[bufOffset + 8 + j] * 256) +
                                           _buf[bufOffset + 9 + j]);

                        PrnParseCommon.addDataRow (
                            _rowType,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            _indxOffsetFormat,
                            baseOffset + 8 + j,
                            _analysisLevel,
                            "        data:",
                            "      LastCode:",
                            "0x" + ui16a.ToString ("X4"));
                    }
                }
                else
                {
                    reportError (
                        "Possibly corrupt: 'Range Count' value " + numRanges,
                        "makes minimum segment header size " +
                            (minSegLen + varSegSize) + " bytes",
                        "This is larger than application buffer size of " +
                            PrnParseConstants.bufSize + " bytes");
                }

                //------------------------------------------------------------//
                //                                                            //
                // Adjust offsets and remainders.                             //
                //                                                            //
                //------------------------------------------------------------//

                bufOffset = bufOffset + minSegLen;
                bufRem = bufRem - minSegLen;
                hddrDataRem = hddrDataRem - minSegLen;
                hddrRem = hddrRem - minSegLen;

                //------------------------------------------------------------//
                //                                                            //
                // Remaining binary segment data.                             //
                //                                                            //
                //------------------------------------------------------------//

                if ((segSize - minSegLen) > hddrDataRem)
                {
                    reportError (
                        "Segment (size " + segSize + ") larger than",
                        "remainder (" + hddrDataRem + ") of segmented data",
                        "Header is  internally inconsistent");

                    _segRem = hddrDataRem;
                }
                else
                {
                    _segRem = segSize - minSegSize;

                    if (_segRem != 0)
                    {
                        reportError (
                            "Segment remainder " + _segRem + " non-zero",
                            "", "");
                    }
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s S e g _ V I                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Segment type: Vendor Information                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processSeg_VI(Int32 segSize,
                                   Int32 segHddrLen,
                                   ref Int32 bufRem,
                                   ref Int32 bufOffset,
                                   ref Int32 hddrDataRem,
                                   ref Int32 hddrRem,
                                   ref Int32 hddrChksVal)
        {
            String segTypeDesc = "VI: Vendor Information";

            const Int32 sliceMax = 50;

            PrnParseConstants.eContType contType;

            Boolean firstLine;

            String textA,
                   textB;

            Int32 baseOffset,
                  dataOffset,
                  minSegSize,
                  minSegLen;

            Int32 sliceLen,
                  infRem,
                  infOffset;

            minSegSize = segSize;
            minSegLen = segHddrLen + minSegSize;
            baseOffset = bufOffset + _fileOffset;
            dataOffset = bufOffset + segHddrLen;

            if (bufRem < minSegLen)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Minimum required size not in buffer.                       //
                // Initiate (back-tracking) continuation.                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = _contType;

                _linkData.setBacktrack (contType, -bufRem);
            }
            else
            {
                if (_showBinData)
                {
                    PrnParseData.processBinary (
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _buf,
                        _fileOffset,
                        bufOffset,
                        minSegLen,
                        "Segment header",
                        true,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }

                if (_PCL)
                {
                    for (Int32 i = 0; i < minSegLen; i++)
                    {
                        hddrChksVal += _buf[bufOffset + i];
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Interpret segment header bytes:                            //
                // bytes  0 -  1     segment type                             //
                //        2 -  x-1   segment size  (format 15: x=4;           //
                //                                  format 16: x=6)           //
                //        x -  ..    segment data                             //
                //                                                            //
                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset,
                    _analysisLevel,
                    "Segment type:",
                    "",
                    segTypeDesc);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 2,
                    _analysisLevel,
                    "        size:",
                    "",
                    segSize + " (" +
                    (segSize + segHddrLen) +
                    " including type & size fields)");

                baseOffset += segHddrLen;


                //------------------------------------------------------------//
                //                                                            //
                //        x -  ..    Vendor information; treat as an array of //
                //                   ASCII characters.                        //
                //                                                            //
                //------------------------------------------------------------//

                firstLine = true;
                infRem = segSize;
                infOffset = 0;

                while (infRem > 0)
                {
                    if (infRem > sliceMax)
                        sliceLen = sliceMax;
                    else
                        sliceLen = infRem;

                    if (firstLine)
                    {
                        textA = "        data:";
                        textB = "Information:";
                    }
                    else
                    {
                        textA = "";
                        textB = "";
                    }

                    PrnParseCommon.addDataRow (
                        _rowType,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _indxOffsetFormat,
                        baseOffset + infOffset,
                        _analysisLevel,
                        textA,
                        textB,
                        _ascii.GetString (_buf,
                                          dataOffset + infOffset,
                                          sliceLen));

                    infRem -= sliceLen;
                    infOffset += sliceLen;
                    firstLine = false;
                }


                //------------------------------------------------------------//
                //                                                            //
                // Adjust offsets and remainders.                             //
                //                                                            //
                //------------------------------------------------------------//

                bufOffset = bufOffset + minSegLen;
                bufRem = bufRem - minSegLen;
                hddrDataRem = hddrDataRem - minSegLen;
                hddrRem = hddrRem - minSegLen;

                //------------------------------------------------------------//
                //                                                            //
                // Remaining binary segment data.                             //
                //                                                            //
                //------------------------------------------------------------//

                if ((segSize - minSegLen) > hddrDataRem)
                {
                    reportError (
                        "Segment (size " + segSize + ") larger than",
                        "remainder (" + hddrDataRem + ") of segmented data",
                        "Header is  internally inconsistent");

                    _segRem = hddrDataRem;
                }
                else
                {
                    _segRem = segSize - minSegSize;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s S e g _ V R                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Segment type: Vertical Rotation                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processSeg_VR(Int32 segSize,
                                   Int32 segHddrLen,
                                   ref Int32 bufRem,
                                   ref Int32 bufOffset,
                                   ref Int32 hddrDataRem,
                                   ref Int32 hddrRem,
                                   ref Int32 hddrChksVal)
        {
            String segTypeDesc = "VR: Vertical Rotation";

            PrnParseConstants.eContType contType;

            Int32 baseOffset,
                  dataOffset,
                  minSegSize,
                  minSegLen;
   
            Int16 si16a;

            UInt16 ui16a;

            minSegSize = 4;
            minSegLen = segHddrLen + minSegSize;
            baseOffset = bufOffset + _fileOffset;
            dataOffset = bufOffset + segHddrLen;

            if (bufRem < minSegLen)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Minimum required size not in buffer.                       //
                // Initiate (back-tracking) continuation.                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = _contType;

                _linkData.setBacktrack (contType, - bufRem);
            }
            else
            {
                if (_showBinData)
                {
                    PrnParseData.processBinary (
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _buf,
                        _fileOffset,
                        bufOffset,
                        minSegLen,
                        "Segment header",
                        true,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }

                if (_PCL)
                {
                    for (Int32 i = 0; i < minSegLen; i++)
                    {
                        hddrChksVal += _buf[bufOffset + i];
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Interpret segment header bytes:                            //
                // bytes  0 -  1     segment type                             //
                //        2 -  x-1   segment size  (format 15: x=4;           //
                //                                  format 16: x=6)           //
                //        x -  ..    segment data                             //
                //                                                            //
                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset,
                    _analysisLevel,
                    "Segment type:",
                    "",
                    segTypeDesc);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 2,
                    _analysisLevel,
                    "        size:",
                    "",
                    segSize + " (" +
                    (segSize + segHddrLen) +
                    " including type & size fields)");

                baseOffset += segHddrLen;

                //------------------------------------------------------------//
                //                                                            //
                // bytes x  -x+1     Format (should be zero)                  //
                //                                                            //
                //------------------------------------------------------------//

                ui16a = (UInt16)((_buf[dataOffset    ] * 256) +
                                  _buf[dataOffset + 1]);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset,
                    _analysisLevel,
                    "        data:",
                    "Format:",
                    ui16a.ToString());

                //------------------------------------------------------------//
                //                                                            //
                // bytes x+2-x+3     sTypoDescender                           //
                //                                                            //
                //------------------------------------------------------------//

                si16a = (Int16)((_buf[dataOffset + 2] * 256) +
                                 _buf[dataOffset + 3]);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 2,
                    _analysisLevel,
                    "        data:",
                    "sTypoDescender:",
                    si16a.ToString());

                //------------------------------------------------------------//
                //                                                            //
                // Adjust offsets and remainders.                             //
                //                                                            //
                //------------------------------------------------------------//

                bufOffset = bufOffset + minSegLen;
                bufRem = bufRem - minSegLen;
                hddrDataRem = hddrDataRem - minSegLen;
                hddrRem = hddrRem - minSegLen;

                //------------------------------------------------------------//
                //                                                            //
                // Remaining binary segment data.                             //
                //                                                            //
                //------------------------------------------------------------//

                if ((segSize - minSegLen) > hddrDataRem)
                {
                    reportError (
                        "Segment (size " + segSize + ") larger than",
                        "remainder (" + hddrDataRem + ") of segmented data",
                        "Header is  internally inconsistent");

                    _segRem = hddrDataRem;
                }
                else
                {
                    _segRem = segSize - minSegSize;

                    if (_segRem != 0)
                    {
                        reportError (
                            "Segment remainder " + _segRem + " non-zero",
                            "", "");
                    }
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s S e g _ V T                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Segment type: Vertical Transformation                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processSeg_VT(Int32 segSize,
                                   Int32 segHddrLen,
                                   ref Int32 bufRem,
                                   ref Int32 bufOffset,
                                   ref Int32 hddrDataRem,
                                   ref Int32 hddrRem,
                                   ref Int32 hddrChksVal)
        {
            String segTypeDesc = "VT: Vertical Transformation";

            PrnParseConstants.eContType contType;

            Int32 baseOffset,
                  dataOffset,
                  minSegSize,
                  minSegLen;

            Int32 eoTMOffset;

            UInt16 ui16a,
                   numSubs;

            minSegSize = segSize;
            minSegLen = segHddrLen + minSegSize;
            baseOffset = bufOffset + _fileOffset;
            dataOffset = bufOffset + segHddrLen;

            if (bufRem < minSegLen)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Minimum required size not in buffer.                       //
                // Initiate (back-tracking) continuation.                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = _contType;

                _linkData.setBacktrack (contType, - bufRem);
            }
            else
            {
                if (_showBinData)
                {
                    PrnParseData.processBinary (
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _buf,
                        _fileOffset,
                        bufOffset,
                        minSegLen,
                        "Segment header",
                        true,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }

                if (_PCL)
                {
                    for (Int32 i = 0; i < minSegLen; i++)
                    {
                        hddrChksVal += _buf[bufOffset + i];
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Interpret segment header bytes:                            //
                // bytes  0 -  1     segment type                             //
                //        2 -  x-1   segment size  (format 15: x=4;           //
                //                                  format 16: x=6)           //
                //        x -  ..    segment data                             //
                //                                                            //
                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset,
                    _analysisLevel,
                    "Segment type:",
                    "",
                    segTypeDesc);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 2,
                    _analysisLevel,
                    "        size:",
                    "",
                    segSize + " (" +
                    (segSize + segHddrLen) +
                    " including type & size fields)");

                baseOffset += segHddrLen;

                //------------------------------------------------------------//
                //                                                            //
                // Derive 'Number of Substitutes' value from Segment Size.    //
                //                                                            //
                //------------------------------------------------------------//

                numSubs    = (UInt16)((segSize - 4) / 4);

                //------------------------------------------------------------//
                //                                                            //
                // bytes x  - ..     Substitute N definitions                 //
                //                   numSubs entries, each of format:         //
                //                   bytes  0 -  1     Horizontal Glyph ID    //
                //                   bytes  2 -  3     Vertical Sub. Glyph ID //
                //                                                            //
                //------------------------------------------------------------//

                for (Int32 i=0; i<numSubs; i++)
                {
                    Int32 j = i * 4;

                    ui16a = (UInt16)((_buf[dataOffset + j    ] * 256) +
                                      _buf[dataOffset + j + 1]);

                    PrnParseCommon.addDataRow (
                        _rowType,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _indxOffsetFormat,
                        baseOffset + j,
                        _analysisLevel,
                        "        data:",
                        "Glyph ID:",
                        ui16a.ToString());

                    ui16a = (UInt16)((_buf[dataOffset + j + 2] * 256) +
                                      _buf[dataOffset + j + 3]);

                    PrnParseCommon.addDataRow (
                        _rowType,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _indxOffsetFormat,
                        baseOffset + j + 2,
                        _analysisLevel,
                        "        data:",
                        "      Sub. ID:",
                        ui16a.ToString());
                }

                //------------------------------------------------------------//
                //                                                            //
                // bytes z  - z+1    End of Table Marker 1 (should be 0xFFFF) //
                // bytes z+2- z+3    End of Table Marker 2 (should be 0xFFFF) //
                //                                                            //
                //------------------------------------------------------------//

                eoTMOffset = segHddrLen + (numSubs * 4);
                minSegLen  = eoTMOffset + 4;

                ui16a = (UInt16)((_buf[dataOffset + eoTMOffset    ] * 256) +
                                  _buf[dataOffset + eoTMOffset + 1]);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + eoTMOffset,
                    _analysisLevel,
                    "        data:",
                    "EoT Marker 1:",
                    "0x" + ui16a.ToString("X4"));

                ui16a = (UInt16)((_buf[dataOffset + eoTMOffset + 2] * 256) +
                                  _buf[dataOffset + eoTMOffset + 3]);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + eoTMOffset + 2,
                    _analysisLevel,
                    "        data:",
                    "EoT Marker 2:",
                    "0x" + ui16a.ToString("X4"));

                //------------------------------------------------------------//
                //                                                            //
                // Adjust offsets and remainders.                             //
                //                                                            //
                //------------------------------------------------------------//

                bufOffset = bufOffset + minSegLen;
                bufRem = bufRem - minSegLen;
                hddrDataRem = hddrDataRem - minSegLen;
                hddrRem = hddrRem - minSegLen;

                //------------------------------------------------------------//
                //                                                            //
                // Remaining binary segment data.                             //
                //                                                            //
                //------------------------------------------------------------//

                if ((segSize - minSegLen) > hddrDataRem)
                {
                    reportError (
                        "Segment (size " + segSize + ") larger than",
                        "remainder (" + hddrDataRem + ") of segmented data",
                        "Header is  internally inconsistent");

                    _segRem = hddrDataRem;
                }
                else
                {
                    _segRem = segSize - minSegSize;

                    if (_segRem != 0)
                    {
                        reportError (
                            "Segment remainder " + _segRem + " non-zero",
                            "", "");

                    }
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s S e g _ X W                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Segment type: X-Window Font                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processSeg_XW(Int32 segSize,
                                   Int32 segHddrLen,
                                   ref Int32 bufRem,
                                   ref Int32 bufOffset,
                                   ref Int32 hddrDataRem,
                                   ref Int32 hddrRem,
                                   ref Int32 hddrChksVal)
        {
            String segTypeDesc = "XW: X-Window Font";

            PrnParseConstants.eContType contType;

            Int32 baseOffset,
                  dataOffset,
                  minSegSize,
                  minSegLen;

            minSegSize = 0;   // TODO if we ever discover the segment format  //
            minSegLen = segHddrLen + minSegSize;
            baseOffset = bufOffset + _fileOffset;
            dataOffset = bufOffset + segHddrLen;

            if (bufRem < minSegLen)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Minimum required size not in buffer.                       //
                // Initiate (back-tracking) continuation.                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = _contType;

                _linkData.setBacktrack (contType, -bufRem);
            }
            else
            {
                if (_showBinData)
                {
                    PrnParseData.processBinary (
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _buf,
                        _fileOffset,
                        bufOffset,
                        minSegLen,
                        "Segment header",
                        true,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }

                if (_PCL)
                {
                    for (Int32 i = 0; i < minSegLen; i++)
                    {
                        hddrChksVal += _buf[bufOffset + i];
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Interpret segment header bytes:                            //
                // bytes  0 -  1     segment type                             //
                //        2 -  x-1   segment size  (format 15: x=4;           //
                //                                  format 16: x=6)           //
                //        x -  ..    segment data                             //
                //                                                            //
                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset,
                    _analysisLevel,
                    "Segment type:",
                    "",
                    segTypeDesc);

                PrnParseCommon.addDataRow (
                    _rowType,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _indxOffsetFormat,
                    baseOffset + 2,
                    _analysisLevel,
                    "        size:",
                    "",
                    segSize + " (" +
                    (segSize + segHddrLen) +
                    " including type & size fields)");

                baseOffset += segHddrLen;

                //------------------------------------------------------------//


                //------------------------------------------------------------//
                //                                                            //
                // Adjust offsets and remainders.                             //
                //                                                            //
                //------------------------------------------------------------//

                bufOffset = bufOffset + minSegLen;
                bufRem = bufRem - minSegLen;
                hddrDataRem = hddrDataRem - minSegLen;
                hddrRem = hddrRem - minSegLen;

                //------------------------------------------------------------//
                //                                                            //
                // Remaining binary segment data.                             //
                //                                                            //
                //------------------------------------------------------------//

                if ((segSize - minSegLen) > hddrDataRem)
                {
                    reportError (
                        "Segment (size " + segSize + ") larger than",
                        "remainder (" + hddrDataRem + ") of segmented data",
                        "Header is  internally inconsistent");

                    _segRem = hddrDataRem;
                }
                else
                {
                    _segRem = segSize - minSegSize;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e p o r t E r r o r                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Report error.                                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void reportError (String line1,
                                  String line2,
                                  String line3)
        {
            _validSegs = false;

            PrnParseCommon.addTextRow (
                PrnParseRowTypes.eType.MsgWarning,
                _table,
                PrnParseConstants.eOvlShow.None,
                "",
                "*** Warning ***",
                "",
                line1);

            if (line2 != "")
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.MsgWarning,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "",
                    "",
                    line2);

            if (line3 != "")
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.MsgWarning,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "",
                    "",
                    line3);
        }
    }
}