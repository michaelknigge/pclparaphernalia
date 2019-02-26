using System;
using System.Data;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class handles PCL downloadable soft font characters.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    class PrnParseFontCharPCL
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private enum eStage
        {
            Start = 0,
            CheckDesc,
            ShowDesc,
            ShowData,
            ShowDataHddr,
            ShowDataBody,
            ShowDataRem,
            ShowChecksum,
            EndOK,
            BadSeqA,
            BadSeqB
        }

        private enum ePCLCharFormat
        {
            Raster = 4,
            Intellifont = 10,
            TrueType = 15
        }

        private enum ePCLCharClass
        {
            Bitmap = 1,
            BitmapCompressed = 2,
            Intellifont = 3,
            IntellifontCompound = 4,
            TrueType = 15,
            Unknown
        }

        private const Int32 _blockHddrLen = 2;

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private PrnParseLinkData _linkData;
        private PrnParseOptions _options;

        private DataTable _table;

        private eStage _nextStage;
        private ePCLCharFormat _charFormat;
        private ePCLCharClass _charClass;

        private Byte[] _buf;
        
        private Byte _charDescLen;

        private Int32 _fileOffset;
        private Int32 _analysisLevel;

        private Int32 _charLen;
        private Int32 _charRem;
        private Int32 _charPos;
        private Int32 _charDataLen;
        private Int32 _charDataRem;
        private Int32 _charDataBlockRem;
        private Int32 _charHeight;
        private Int32 _charWidth;

        private Int32 _charChksLen;
//        private Int32 _charChksPos;
        private Int32 _charChksVal;

        private Int32 _charResvLen;
//        private Int32 _charResvPos;

        private Int32 _drawCharMaxHeight;
        private Int32 _drawCharMaxWidth;

        private Boolean _showBinData;
        private Boolean _validChar;
        private Boolean _contChar;
  //      private Boolean _contCharExpected;

        private Boolean _drawCharShape;

  //      private Boolean _bitmapFont;
  //      private Boolean _intelliFont;
  //      private Boolean _truetypeFont;
  //      private Boolean _bitmapCompressed;

        private PrnParseConstants.eOptOffsetFormats _indxOffsetFormat;

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // a n a l y s e F o n t C h a r                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Provide an interpretation of the contents of a PCL soft font       //
        // character description/data block.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean analyseFontChar(Int32 charLen,
                                       Int32 fileOffset,
                                       Byte[] buf,
                                       ref Int32 bufRem,
                                       ref Int32 bufOffset,
                                       PrnParseLinkData linkData,
                                       PrnParseOptions options,
                                       DataTable table)
        {
            Int32 binDataLen;
            
            PrnParseConstants.eContType contType;

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

            _options.getOptPCLDraw (ref _drawCharShape,
                                    ref _drawCharMaxHeight,
                                    ref _drawCharMaxWidth);

            if (contType == PrnParseConstants.eContType.None)
            {
                _nextStage = eStage.Start;
                _validChar = true;

                _charLen = charLen;
                _charRem = charLen;
                _charPos = fileOffset + bufOffset;

                _charHeight = -1;
                _charWidth  = -1;
            }
            else
            {
                contType = PrnParseConstants.eContType.None;
                _linkData.resetContData();
            }

            if (_nextStage == eStage.Start)
            {
                if (bufRem > _blockHddrLen)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Block header (Format Identifier & Continuation Marker) //
                    // and at least one more byte (Descriptor Size, unless    //
                    // continuation block) are in buffer.                     //
                    // Process block header and determine next stage (either  //
                    // CheckDesc, or ShowData (if Continuation block).        //
                    //                                                        //
                    //--------------------------------------------------------//

                    processBlockHeader (ref bufRem,
                                        ref bufOffset);

                    if (_contChar)
                    {
                        _nextStage = eStage.ShowData;
                        _charDataBlockRem = _charDataRem;
                    }
                    else
                    {
                        _nextStage = eStage.CheckDesc;
                        _charChksVal = 0;
                    }
                }
                else
                {
                    contType = PrnParseConstants.eContType.PCLFontChar;

                    _linkData.setBacktrack (contType, -bufRem);
                }
            }

            if (_nextStage == eStage.CheckDesc)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Check if character data descriptor is in buffer.           //
                //                                                            //
                //------------------------------------------------------------//

                _charDescLen = buf[bufOffset];

                if ((_charDescLen + _blockHddrLen) > _charLen)
                {
                    _validChar = false;
                    _nextStage = eStage.BadSeqA;

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgWarning,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "*** Warning ***",
                        "",
                        "Descriptor size (" + _charDescLen + " bytes)" +
                        " inconsistent with");

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgWarning,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "",
                        "",
                        "download size = " + _charLen +
                        " and block header = " + _blockHddrLen + " bytes");
                }
                else if ((_charDescLen) > bufRem)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Header/Descriptor is not entirely within buffer.       //
                    // Initiate (back-tracking) continuation.                 //
                    //                                                        //
                    //--------------------------------------------------------//

                    contType = PrnParseConstants.eContType.PCLFontChar;

                    _linkData.setBacktrack (contType, - bufRem);
                }
                else
                {
                    _nextStage = eStage.ShowDesc;
                }
            }

            if (_nextStage == eStage.ShowDesc)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Process character descriptor.                              //
                //                                                            //
                //------------------------------------------------------------//

                processDescriptor(ref bufRem,
                                  ref bufOffset);

                bufRem       = bufRem    - _charDescLen;
                _charRem     = _charRem  - _charDescLen;
                bufOffset    = bufOffset + _charDescLen;

                if (_validChar)
                    _nextStage = eStage.ShowData;
                else
                    _nextStage = eStage.BadSeqA;
            }

            if (_nextStage == eStage.ShowData)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Process character data.                                    //
                //                                                            //
                //------------------------------------------------------------//

                if (_charFormat == ePCLCharFormat.Raster)
                {
                    _charDataLen = _charRem;
                    _charDataRem = _charDataLen;

                    if (_contChar)
                        _nextStage = eStage.ShowDataRem;
                    else
                        _nextStage = eStage.ShowDataBody;
                }
                else
                {
                    if (_contChar)
                        _nextStage = eStage.ShowDataBody;
                    else
                        _nextStage = eStage.ShowDataHddr;
                }
            }

            if ((_nextStage == eStage.ShowDataHddr) && (_charRem != 0))
            {
                //------------------------------------------------------------//
                //                                                            //
                // Output details of (scalable font) character data header.   //
                //                                                            //
                //------------------------------------------------------------//

                if (_charFormat == ePCLCharFormat.Intellifont)
                {
                    processIntellifontDataHddr (ref bufRem,
                                                ref bufOffset);
                }
                else if (_charFormat == ePCLCharFormat.TrueType)
                {
                    processTrueTypeDataHddr (ref bufRem,
                                             ref bufOffset);
                }
                else
                {
                    _nextStage = eStage.BadSeqA;
                }
            }

            if ((_nextStage == eStage.ShowDataBody) && (_charRem != 0))
            {
                //------------------------------------------------------------//
                //                                                            //
                // Output details of character data.                          //
                //                                                            //
                //------------------------------------------------------------//

                if (_charFormat == ePCLCharFormat.Raster)
                {
                    processRasterDataBody (ref bufRem,
                                           ref bufOffset);
                }
                else if (_charFormat == ePCLCharFormat.Intellifont)
                {
                    processIntellifontDataBody (ref bufRem,
                                                ref bufOffset);
                }
                else if (_charFormat == ePCLCharFormat.TrueType)
                {
                    processTrueTypeDataBody (ref bufRem,
                                             ref bufOffset);
                }
                else
                {
                    _nextStage = eStage.BadSeqA;
                }
            }

            if ((_nextStage == eStage.ShowDataRem) && (_charRem != 0))
            {
                //------------------------------------------------------------//
                //                                                            //
                // Output details of remainder of (Raster) character data.    //
                //                                                            //
                //------------------------------------------------------------//

                if (_charFormat == ePCLCharFormat.Raster)
                {
                    processRasterDataRem (ref bufRem,
                                          ref bufOffset);
                }
                else
                {
                    _nextStage = eStage.BadSeqA;
                }
            }

            if ((_nextStage == eStage.ShowChecksum) && (_charRem != 0))
            {
                //------------------------------------------------------------//
                //                                                            //
                // Output details of Reserved byte and Checksum fields.       //
                // Not present for Raster (bitmap) format.                    //
                //                                                            //
                //------------------------------------------------------------//

                processChecksum(ref bufRem,
                                ref bufOffset);

            }

            if (_nextStage == eStage.EndOK)
            {
                //------------------------------------------------------------//
                //                                                            //
                // End of processing of valid header.                         //
                //                                                            //
                //------------------------------------------------------------//

                return _validChar;
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
                    "Processing of character abandoned");
            }

            if ((_nextStage == eStage.BadSeqB) && (_charRem != 0))
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

                if (_charRem > bufRem)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Remainder of sequence is not in buffer.                //
                    // Initiate continuation.                                 //
                    //                                                        //
                    //--------------------------------------------------------//

                    contType = PrnParseConstants.eContType.PCLFontChar;

                    binDataLen = bufRem;
                    _charRem   = _charRem - bufRem;

                    _linkData.setContinuation (contType);
                }
                else
                {
                    contType = PrnParseConstants.eContType.None;
                    _linkData.resetContData();

                    binDataLen = _charRem;
                    _charRem = 0;
                }

                if ((binDataLen) != 0)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Some, or all, of the download data is contained within //
                    // the current 'block'.                                   //
                    //                                                        //
                    //--------------------------------------------------------//

                    PrnParseCommon.addDataRow (
                        PrnParseRowTypes.eType.DataBinary,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _indxOffsetFormat,
                        fileOffset + bufOffset,
                        _analysisLevel,
                        "PCL Binary",
                        "[ " + binDataLen + " bytes ]",
                        "");

                    bufRem    = bufRem - binDataLen;
                    bufOffset = bufOffset + binDataLen;
                }
            }

            return _validChar;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s B l o c k H e a d e r                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Provide an interpretation of the contents of the Block header.     //
        //                                                                    //
        //    byte 0   Format                                                 //
        //         1   Continuation flag                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processBlockHeader (ref Int32 bufRem,
                                         ref Int32 bufOffset)
        {
            String itemDesc;

            PrnParseCommon.addDataRow (
                PrnParseRowTypes.eType.DataBinary,
                _table,
                PrnParseConstants.eOvlShow.None,
                _indxOffsetFormat,
                _fileOffset + bufOffset,
                _analysisLevel,
                "PCL Binary",
                "[ " + _blockHddrLen + " bytes ]",
                "Character data block header");

            if (_showBinData)
            {
                PrnParseData.processBinary (
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _buf,
                    _fileOffset,
                    bufOffset,
                    _blockHddrLen,
                    "",
                    true,
                    false,
                    true,
                    _indxOffsetFormat,
                    _analysisLevel);
            }

            //--------------------------------------------------------//
            //                                                        //
            // Format (byte 0):                                       //
            //                                                        //
            //--------------------------------------------------------//

            _charFormat = (ePCLCharFormat) _buf[bufOffset];

            switch (_charFormat)
            {
                case ePCLCharFormat.Raster:
                    itemDesc = "4: Raster";
                    break;

                case ePCLCharFormat.Intellifont:
                    itemDesc = "10: Intellifont Scalable";
                    break;

                case ePCLCharFormat.TrueType:
                    itemDesc = "15: Truetype Scalable";
                    break;

                default:
                    itemDesc = _charFormat.ToString () + ": Unknown";
                    break;
            }

            PrnParseCommon.addTextRow (
                PrnParseRowTypes.eType.PCLFontChar,
                _table,
                PrnParseConstants.eOvlShow.None,
                "",
                "Header Format:",
                "",
                itemDesc);

            //--------------------------------------------------------//
            //                                                        //
            // Continuation flag (byte 1).                            //
            //                                                        //
            //--------------------------------------------------------//

            if (_buf[bufOffset + 1] == 0)
            {
                _contChar = false;
                _charClass = ePCLCharClass.Unknown; // Set in desc.
            }
            else
            {
                _contChar = true;

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLFontChar,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "",
                    "",
                    "Continuation block");
            }

            //--------------------------------------------------------//
            //                                                        //
            // Adjust pointers.                                       //
            //                                                        //
            //--------------------------------------------------------//

            bufRem    = bufRem    - _blockHddrLen;
            _charRem  = _charRem  - _blockHddrLen;
            bufOffset = bufOffset + _blockHddrLen;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s C h e c k s u m                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Output details of Reserved byte and Checksum byte.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processChecksum (ref Int32 bufRem,
                                      ref Int32 bufOffset)
        {
            PrnParseConstants.eContType contType;

            _charChksLen = 1; // TEMP
            _charResvLen = 1; // TEMP

            if (_charChksLen == 0)
            {
                if (_charRem != 0)
                {
                    _validChar = false;
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
                if (_charRem != (_charResvLen + _charChksLen))
                {
                    _validChar = false;
                    _nextStage = eStage.BadSeqA;

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgWarning,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "*** Warning ***",
                        "",
                        "Either Character Data Size is incorrect or");

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgWarning,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "",
                        "",
                        "Reserved byte and/or Checksum byte are missing");
                }
                else
                {
                    if (_charRem > bufRem)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // Remainder of sequence is not in buffer.            //
                        // Initiate (back-tracking) continuation.             //
                        //                                                    //
                        //----------------------------------------------------//

                        contType = PrnParseConstants.eContType.PCLFontChar;

                        _linkData.setBacktrack (contType, - bufRem);
                    }
                    else
                    {
                        Byte crntByte;

                        contType = PrnParseConstants.eContType.None;
                        _linkData.resetContData();

                        //----------------------------------------------------//
                        //                                                    //
                        // Display Reserved byte (should always be            //
                        // (binary) zero).                                    //
                        //                                                    //
                        //----------------------------------------------------//

                        crntByte = _buf[bufOffset];

                        PrnParseCommon.addDataRow (
                            PrnParseRowTypes.eType.PCLFontChar,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            _indxOffsetFormat,
                            _fileOffset + bufOffset,
                            _analysisLevel,
                            "Reserved byte",
                            "[ 1 byte ]",
                            "0x" +
                            PrnParseCommon.byteToHexString(crntByte));

                        _charChksVal += crntByte;

                        //----------------------------------------------------//
                        //                                                    //
                        // Display Checksum byte.                             //
                        // Verify that it matches the calculated value.       //
                        //                                                    //
                        //----------------------------------------------------//

                        crntByte = _buf[bufOffset + 1];

                        PrnParseCommon.addDataRow (
                            PrnParseRowTypes.eType.PCLFontChar,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            _indxOffsetFormat,
                            _fileOffset + bufOffset + 1,
                            _analysisLevel,
                            "Checksum",
                            "[ 1 byte ]",
                            "0x" +
                            PrnParseCommon.byteToHexString (crntByte));

                        _charChksVal = (256 - (_charChksVal % 256)) % 256;
                            
                        if (_charChksVal != crntByte)
                        {
                            crntByte = (Byte) _charChksVal;

                            PrnParseCommon.addTextRow (
                                PrnParseRowTypes.eType.MsgWarning,
                                _table,
                                PrnParseConstants.eOvlShow.None,
                                "",
                                "*** Warning ***",
                                "",
                                "Calculated checksum is 0x" +
                                PrnParseCommon.byteToHexString (crntByte));
                                
                            _validChar = false;
                        }
                            
                        //----------------------------------------------------//
                        //                                                    //
                        // Adjust pointers.                                   //
                        //                                                    //
                        //----------------------------------------------------//

                        bufRem    = bufRem    - _charRem;
                        bufOffset = bufOffset + _charRem;

                        _charRem = 0;
                        _nextStage = eStage.EndOK;
                    }
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s D e s c r i p t o r                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Provide an interpretation of the contents of the Character         //
        // Descriptor part of the header.                                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processDescriptor(ref Int32 bufRem,
                                       ref Int32 bufOffset)
        {
            UInt32 ui32a;
            UInt16 ui16a;

            Int16 si16a;

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
                "[ " + _charDescLen + " bytes ]",
                "Character descriptor");

            if (_showBinData)
            {
                PrnParseData.processBinary (
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _buf,
                    _fileOffset,
                    bufOffset,
                    _charDescLen,
                    "",
                    true,
                    false,
                    true,
                    _indxOffsetFormat,
                    _analysisLevel);
            }

            // _bitmapFont       = false;
            // _intelliFont      = false;
            // _truetypeFont     = false;

            //----------------------------------------------------------------//
            //                                                                //
            // Character Descriptor size.                                     //
            //                                                                //
            //----------------------------------------------------------------//

            PrnParseCommon.addTextRow (
                PrnParseRowTypes.eType.PCLFontChar,
                _table,
                PrnParseConstants.eOvlShow.None,
                "",
                "Descriptor Size:",
                "",
                _charDescLen.ToString());

            //----------------------------------------------------------------//
            //                                                                //
            // Class (byte 1).                                                //
            //                                                                //
            //----------------------------------------------------------------//

            _charClass = (ePCLCharClass) _buf[bufOffset + 1];

            switch (_charClass)
            {
                case ePCLCharClass.Bitmap:
                    itemDesc = "1: Bitmap";
                    //_bitmapFont = true;
                    break;

                case ePCLCharClass.BitmapCompressed:
                    itemDesc = "2: Compressed Bitmap";
                    //_bitmapFont       = true;
                    //_bitmapCompressed = true;
                    break;

                case ePCLCharClass.Intellifont:
                    itemDesc = "3: Intellifont Scalable: Contour";
                    //_intelliFont = true;
                    break;

                case ePCLCharClass.IntellifontCompound:
                    itemDesc = "4: Intellifont Scalable: Compound Contour";
                    //_intelliFont = true;
                    break;

                case ePCLCharClass.TrueType:
                    itemDesc = "15: Truetype Scalable";
                    //_truetypeFont = true;
                    break;

                default:
                    itemDesc = _charClass + ": Unknown";
                    break;
            }

            PrnParseCommon.addTextRow (
                PrnParseRowTypes.eType.PCLFontChar,
                _table,
                PrnParseConstants.eOvlShow.None,
                "",
                "Class:",
                "",
                itemDesc);

            if (_charFormat == ePCLCharFormat.Raster)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Format 4 - Raster (bitmap).                                //
                //                                                            //
                //------------------------------------------------------------//

                UInt16 bytesPerRow;

                //------------------------------------------------------------//
                //                                                            //
                // Orientation (byte 2).                                      //
                //                                                            //
                //    0 = Portrait                                            //
                //    1 = Landscape                                           //
                //    2 = Reverse Portrait                                    //
                //    3 = Reverse Landscape                                   //
                //                                                            //
                //------------------------------------------------------------//

                ui16a = _buf[bufOffset + 2];

                switch (ui16a)
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
                        itemDesc = ui16a + ": Unknown";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLFontChar,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Orientation:",
                    "",
                    itemDesc);

                //------------------------------------------------------------//
                //                                                            //
                // Left offset (bytes 4 & 5).                                 //
                //                                                            //
                // Distance from the 'reference point' to the left side of    //
                // the character.                                             //
                //                                                            //
                //------------------------------------------------------------//

                si16a = (Int16) ((_buf[bufOffset + 4] * 256) +
                                  _buf[bufOffset + 5]);

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLFontChar,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Left Offset:",
                    "",
                    si16a + " dots");

                //------------------------------------------------------------//
                //                                                            //
                // Top offset (bytes 6 & 7).                                  //
                //                                                            //
                // Distance from the 'reference point' to the top of the      //
                // character.                                                 //
                //                                                            //
                //------------------------------------------------------------//

                si16a = (Int16) ((_buf[bufOffset + 6] * 256) +
                                  _buf[bufOffset + 7]);

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLFontChar,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Top Offset:",
                    "",
                    si16a + " dots");

                //------------------------------------------------------------//
                //                                                            //
                // Character Width (bytes 8 & 9).                             //
                //                                                            //
                //------------------------------------------------------------//

                _charWidth = (UInt16) ((_buf[bufOffset + 8] * 256) +
                                        _buf[bufOffset + 9]);

                bytesPerRow = (UInt16) ((_charWidth / 8));

                if ((_charWidth % 8) != 0)
                    bytesPerRow++;

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLFontChar,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Character Width:",
                    "",
                    _charWidth + " dots (requires " +
                    bytesPerRow + " padded bytes per row)");

                //------------------------------------------------------------//
                //                                                            //
                // Character Height (bytes 10 & 11).                          //
                //                                                            //
                //------------------------------------------------------------//

                _charHeight = (UInt16) ((_buf[bufOffset + 10] * 256) +
                                         _buf[bufOffset + 11]);

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLFontChar,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Character Height:",
                    "",
                    _charHeight + " dots");

                //------------------------------------------------------------//
                //                                                            //
                // Delta-X (bytes 12 & 13).                                   //
                //                                                            //
                // Number of radix (quarter) dots to advance cursor after     //
                // printing character; only relevant to proportionally-spaced //
                // fonts.                                                     //
                //                                                            //
                //------------------------------------------------------------//

                si16a = (Int16) ((_buf[bufOffset + 12] * 256) +
                                  _buf[bufOffset + 13]);

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLFontChar,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Delta-X:",
                    "",
                    si16a + " quarter-dots");

                //------------------------------------------------------------//
                //                                                            //
                // Estimated character data size:                             //
                //    (bytesPerRow * charHeight) bytes.                       //
                //                                                            //
                //------------------------------------------------------------//

                if (_charClass == ePCLCharClass.Bitmap)
                {
                    ui32a = (UInt32) (bytesPerRow * _charHeight);

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontChar,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Raster data size:",
                        "",
                        ui32a + " bytes (assuming " +
                        _charHeight + " rows of " +
                        bytesPerRow + " bytes)");

                    if (ui32a != (_charLen - _blockHddrLen - _charDescLen))
                    {
                        _validChar = false;

                        _nextStage = eStage.BadSeqA;

                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.MsgWarning,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "*** Warning ***",
                            "",
                            "Estimated data size (" + ui32a + " bytes)" +
                            " inconsistent with");

                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.MsgWarning,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "",
                            "",
                            "download size = " + _charLen +
                            ", block header = " + _blockHddrLen + " and ");

                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.MsgWarning,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "",
                            "",
                            "descriptor = " + _charDescLen + " bytes");
                    }
                }
            }
            else if (_charFormat == ePCLCharFormat.Intellifont)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Expected Descriptor size is only two bytes (the Format &   //
                // Class bytes).                                              //
                // Report any extra bytes as unexpected.                      //
                //                                                            //
                //------------------------------------------------------------//

                Int32 charDescExtra = _charDescLen - 2;

                if (charDescExtra > 0)
                {
                    _validChar = false;
                    _nextStage = eStage.BadSeqA;

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgWarning,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "***Warning***",
                        "",
                        "Descriptor size (" + _charDescLen + " bytes)" +
                        " larger than expected (2 bytes)");

                    PrnParseData.processBinary (
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _buf,
                        _fileOffset,
                        bufOffset + 2,
                        charDescExtra,
                        "Additional data:",
                        _showBinData,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }
            }
            else if (_charFormat == ePCLCharFormat.TrueType)
            {
                Int32 charDescExtra = _charDescLen - 2;

                if (charDescExtra > 0)
                {
                    PrnParseData.processBinary (
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _buf,
                        _fileOffset,
                        bufOffset + 2,
                        charDescExtra,
                        "Additional data:",
                        _showBinData,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s I n t e l l i f o n t D a t a B o d y                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Process Intellifont character data.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processIntellifontDataBody (ref Int32 bufRem,
                                                 ref Int32 bufOffset)
        {
            PrnParseConstants.eContType contType;
            
            Int32 binDataLen;

            if (_charDataBlockRem > bufRem)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Remainder of sequence is not in buffer.                    //
                // Initiate continuation.                                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = PrnParseConstants.eContType.PCLFontChar;

                binDataLen        = bufRem;
                _charRem          = _charRem - bufRem;
                _charDataRem      = _charDataRem - bufRem;
                _charDataBlockRem = _charDataBlockRem - bufRem;

                _linkData.setContinuation (contType);
            }
            else
            {
                contType = PrnParseConstants.eContType.None;
      
                _linkData.resetContData();

                binDataLen = _charDataBlockRem;
                _charRem = _charRem - _charDataBlockRem;
                _charDataRem = _charDataRem - _charDataBlockRem;
                _charDataBlockRem = 0;

                _nextStage = eStage.ShowChecksum; 
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
                    "Intellifont character data");

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
                        true,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }

                for (Int32 i = 0; i < binDataLen; i++)
                {
                    _charChksVal += _buf[bufOffset + i];
                }

                bufRem    = bufRem - binDataLen;
                bufOffset = bufOffset + binDataLen;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s I n t e l l i f o n t D a t a H d d r                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Process Intellifont character data header.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processIntellifontDataHddr(ref Int32 bufRem,
                                                ref Int32 bufOffset)
        {
            PrnParseConstants.eContType contType;

            Int32 hddrLen;

            UInt16 contourDataSize;

            Int16 metricDataOffset,
                  charDataOffset,
                  contourTreeOffset,
                  xyDataOffset,
                  compEscapement;

            Byte compCount;

            if (_charClass == ePCLCharClass.Intellifont)
                hddrLen = 10;
            else
                hddrLen = 4;

            if (bufRem < hddrLen)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Header is not in buffer.                                   //
                // Initiate (back-tracking) continuation.                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = PrnParseConstants.eContType.PCLFontChar;

                _linkData.setBacktrack (contType, -bufRem);
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // Show size and (optionally) data.                           //
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
                    "[ " + hddrLen + " bytes ]",
                    "Intellifont character data header");

                if (_showBinData)
                {
                    PrnParseData.processBinary (
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _buf,
                        _fileOffset,
                        bufOffset,
                        hddrLen,
                        "",
                        true,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }

                for (Int32 i = 0; i < hddrLen; i++)
                {
                    _charChksVal += _buf[bufOffset + i];
                }

                if (_charClass == ePCLCharClass.Intellifont)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Contour Data Size.                                     //
                    //                                                        //
                    //--------------------------------------------------------//

                    contourDataSize = (UInt16) ((_buf[bufOffset] * 256) +
                                                 _buf[bufOffset + 1]);
                    _charDataLen = (UInt16) (contourDataSize - hddrLen);

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontChar,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Contour Data Size:",
                        "",
                        contourDataSize + " bytes" +
                        "(header = " + hddrLen +
                        "; glyph data = " + _charDataLen + ")");

                    //--------------------------------------------------------//
                    //                                                        //
                    // Metric Data Offset.                                    //
                    //                                                        //
                    //--------------------------------------------------------//

                    metricDataOffset = (Int16) ((_buf[bufOffset + 2] * 256) +
                                                 _buf[bufOffset + 3]);

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontChar,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Metric Data Offset:",
                        "",
                        metricDataOffset + " bytes");

                    //--------------------------------------------------------//
                    //                                                        //
                    // Character Intellifont Data Offset.                     //
                    //                                                        //
                    //--------------------------------------------------------//

                    charDataOffset = (Int16) ((_buf[bufOffset + 4] * 256) +
                                               _buf[bufOffset + 5]);

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontChar,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Character Data Offset:",
                        "",
                        charDataOffset + " bytes");

                    //--------------------------------------------------------//
                    //                                                        //
                    // Contour Tree Offset.                                   //
                    //                                                        //
                    //--------------------------------------------------------//

                    contourTreeOffset = (Int16) ((_buf[bufOffset + 6] * 256) +
                                                  _buf[bufOffset + 7]);

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontChar,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Contour Tree Offset:",
                        "",
                        contourTreeOffset + " bytes");

                    //--------------------------------------------------------//
                    //                                                        //
                    // XY Data Offset.                                        //
                    //                                                        //
                    //--------------------------------------------------------//

                    xyDataOffset = (Int16) ((_buf[bufOffset + 8] * 256) +
                                             _buf[bufOffset + 9]);

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontChar,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "XY Data Offset:",
                        "",
                        xyDataOffset + " bytes");
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Compound Character Escapement.                         //
                    //                                                        //
                    //--------------------------------------------------------//

                    compEscapement = (Int16) ((_buf[bufOffset] * 256) +
                                               _buf[bufOffset + 1]);

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontChar,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Compound Escapement:",
                        "",
                        compEscapement + " design units");

                    //--------------------------------------------------------//
                    //                                                        //
                    // Number of Components.                                  //
                    //                                                        //
                    //--------------------------------------------------------//

                    compCount = _buf[bufOffset + 2];

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLFontChar,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Number of Components:",
                        "",
                        compCount.ToString ());
                }

                //------------------------------------------------------------//
                //                                                            //
                // Adjust pointers to reference start of variable data.       //
                //                                                            //
                //------------------------------------------------------------//

                contType = PrnParseConstants.eContType.None;
                _linkData.resetContData ();

                _charRem = _charRem - hddrLen;
                bufRem = bufRem - hddrLen;
                bufOffset = bufOffset + hddrLen;
                _charDataRem = _charDataLen;
                _charDataBlockRem = _charDataLen;

                if ((_charDataLen + 2) > _charRem)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Character data must be spread over two or more blocks. //
                    //                                                        //
                    //--------------------------------------------------------//

                    _charDataBlockRem = _charRem;

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgComment,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Comment",
                        "",
                        "Continuation block expected");
                }

                _nextStage = eStage.ShowDataBody;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s R a s t e r D a t a B o d y                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Process Raster character data.                                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processRasterDataBody (ref Int32 bufRem,
                                            ref Int32 bufOffset)
        {
            PrnParseConstants.eContType contType;

            Int32 binDataLen;

            Boolean shapeTooLarge = false;

            if (_drawCharShape)
            {
                if ((_charHeight > _drawCharMaxHeight)
                                ||
                    (_charWidth > _drawCharMaxWidth)
                                ||
                    (_charDataLen > PrnParseConstants.bufSize))
                {
                    shapeTooLarge = true;
                }
                else
                {
                   shapeTooLarge = false;
                }
            }

            if (_charRem > bufRem)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Remainder of sequence is not in buffer.                    //
                // Initiate continuation (back-tracking if drawing character  //
                // shape).                                                    //
                //                                                            //
                //------------------------------------------------------------//

                if ((_drawCharShape) && (! shapeTooLarge))
                {
                    contType = PrnParseConstants.eContType.PCLFontChar;

                    _linkData.setBacktrack (contType, - bufRem);

                    bufOffset  = bufOffset + bufRem;
                    bufRem     = 0;
                    binDataLen = 0;
                }
                else
                {
                    _nextStage = eStage.ShowDataRem;

                    contType   = PrnParseConstants.eContType.PCLFontChar;

                    binDataLen = bufRem;
                    _charRem   = _charRem - bufRem;

                    _linkData.setContinuation (contType);
                }
            }
            else
            {
                contType = PrnParseConstants.eContType.None;
                _linkData.resetContData();

                binDataLen = _charRem;
                _charRem = 0;

                _nextStage = eStage.EndOK;
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
                    "Raster character data");

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
                        true,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }

                if (_drawCharShape)
                {
                    rasterDraw (bufOffset, binDataLen, shapeTooLarge);
                }

                //------------------------------------------------------------//
                //                                                            //
                // Adjust pointers.                                           //
                //                                                            //
                //------------------------------------------------------------//

                bufRem    = bufRem - binDataLen;
                bufOffset = bufOffset + binDataLen;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s R a s t e r D a t a R e m                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Process remainder of Raster character data.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processRasterDataRem(ref Int32 bufRem,
                                          ref Int32 bufOffset)
        {
            PrnParseConstants.eContType contType;

            Int32 binDataLen;

            if (_charRem > bufRem)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Remainder of sequence is not in buffer.                    //
                // Initiate (non back-tracking) continuation.                 //
                //                                                            //
                //------------------------------------------------------------//

                contType = PrnParseConstants.eContType.PCLFontChar;

                binDataLen = bufRem;
                _charRem   = _charRem - bufRem;

                _linkData.setContinuation (contType);
            }
            else
            {
                contType = PrnParseConstants.eContType.None;
                _linkData.resetContData();

                binDataLen = _charRem;
                _charRem = 0;

                _nextStage = eStage.EndOK;
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
                    "Raster character data");

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
                        true,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }

                //------------------------------------------------------------//
                //                                                            //
                // Adjust pointers.                                           //
                //                                                            //
                //------------------------------------------------------------//

                bufRem    = bufRem - binDataLen;
                bufOffset = bufOffset + binDataLen;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s T r u e T y p e D a t a B o d y                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Process TrueType glyph data.                                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processTrueTypeDataBody (ref Int32 bufRem,
                                              ref Int32 bufOffset)
        {
            PrnParseConstants.eContType contType;
            
            Int32 binDataLen;

            if (_charDataBlockRem > bufRem)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Remainder of sequence is not in buffer.                    //
                // Initiate continuation.                                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = PrnParseConstants.eContType.PCLFontChar;

                binDataLen        = bufRem;
                _charRem          = _charRem - bufRem;
                _charDataRem      = _charDataRem - bufRem;
                _charDataBlockRem = _charDataBlockRem - bufRem;

                _linkData.setContinuation (contType);
            }
            else
            {
                contType = PrnParseConstants.eContType.None;
                _linkData.resetContData();

                binDataLen        = _charDataBlockRem;
                _charRem          = _charRem - _charDataBlockRem;
                _charDataRem      = _charDataRem - _charDataBlockRem;
                _charDataBlockRem = 0;

                _nextStage = eStage.ShowChecksum;
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
                    "TrueType glyph data");

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
                        true,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }

                for (Int32 i = 0; i < binDataLen; i++)
                {
                    _charChksVal += _buf[bufOffset + i];
                }

                bufRem    = bufRem - binDataLen;
                bufOffset = bufOffset + binDataLen;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s T r u e T y p e D a t a H d d r                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Process TrueType character data header.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processTrueTypeDataHddr(ref Int32 bufRem,
                                             ref Int32 bufOffset)
        {
            PrnParseConstants.eContType contType;

            Int32 hddrLen;

            UInt16 charDataSize;

            Int16 glyphID;

            hddrLen = 4;

            if (bufRem < hddrLen)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Header is not in buffer.                                   //
                // Initiate (back-tracking) continuation.                     //
                //                                                            //
                //------------------------------------------------------------//

                contType = PrnParseConstants.eContType.PCLFontChar;

                _linkData.setBacktrack (contType, -bufRem);
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // Show size and (optionally) data.                           //
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
                    "[ " + hddrLen + " bytes ]",
                    "TrueType character data header");

                if (_showBinData)
                {
                    PrnParseData.processBinary (
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        _buf,
                        _fileOffset,
                        bufOffset,
                        hddrLen,
                        "",
                        true,
                        false,
                        true,
                        _indxOffsetFormat,
                        _analysisLevel);
                }

                for (Int32 i = 0; i < hddrLen; i++)
                {
                    _charChksVal += _buf[bufOffset + i];
                }

                //------------------------------------------------------------//
                //                                                            //
                // Character Data Size.                                       //
                //                                                            //
                //------------------------------------------------------------//

                charDataSize = (UInt16) ((_buf[bufOffset] * 256) +
                                          _buf[bufOffset + 1]);
                _charDataLen = (UInt16) (charDataSize - hddrLen);

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLFontChar,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Character Data Size:",
                    "",
                    charDataSize + " bytes" +
                    " (header = " + hddrLen +
                    "; glyph data = " + _charDataLen + ")");

                //------------------------------------------------------------//
                //                                                            //
                // Glyph ID.                                                  //
                //                                                            //
                //------------------------------------------------------------//

                glyphID = (Int16) ((_buf[bufOffset + 2] * 256) +
                                    _buf[bufOffset + 3]);

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLFontChar,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Glyph ID:",
                    "",
                    glyphID.ToString ());

                //------------------------------------------------------------//
                //                                                            //
                // Adjust pointers to reference start of variable data.       //
                //                                                            //
                //------------------------------------------------------------//

                contType = PrnParseConstants.eContType.None;
                _linkData.resetContData ();

                _charRem = _charRem - hddrLen;
                bufRem = bufRem - hddrLen;
                bufOffset = bufOffset + hddrLen;

                _charDataRem      = _charDataLen;
                _charDataBlockRem = _charDataLen;
                
                if ((_charDataLen + 2) > _charRem)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Character data must be spread over two or more blocks. //
                    //                                                        //
                    //--------------------------------------------------------//

                    _charDataBlockRem = _charRem;

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgComment,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Comment",
                        "",
                        "Continuation block expected");
                }

                _nextStage = eStage.ShowDataBody;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r a s t e r D r a w                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Draw raster character.                                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rasterDraw (Int32   bufOffset,
                                 Int32   dataLen,
                                 Boolean shapeTooLarge)
        {
            if (shapeTooLarge)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Shape exceeds size constraints.                            //
                //                                                            //
                //------------------------------------------------------------//

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.MsgComment,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Comment",
                    "Shape",
                    "***** Too large to display *****");

                if (_charHeight > _drawCharMaxHeight)
                {
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgComment,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Comment",
                        "Shape",
                        "Height (" + _charHeight +
                        ") > " + _drawCharMaxHeight +
                        " dots");
                }

                if (_charWidth > _drawCharMaxWidth)
                {
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgComment,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Comment",
                        "Shape",
                        "Width (" + _charWidth +
                        ") > " + _drawCharMaxWidth +
                        " dots");
                }

                if (_charDataLen > PrnParseConstants.bufSize)
                {
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgComment,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Comment",
                        "",
                        "Data   (" + _charDataLen +
                        ") > " + PrnParseConstants.bufSize +
                        " bytes");
                }
            }
            else if (_charClass == ePCLCharClass.Bitmap)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Display shape of bitmap character.                         //
                //                                                            //
                //------------------------------------------------------------//

                Boolean firstLine;

                Int32 bytesPerRow,
                      sliceLen,
                      crntOffset,
                      sub;

                String rowImage;

                bytesPerRow = (_charWidth / 8);

                if (_charWidth - (bytesPerRow * 8) != 0)
                    bytesPerRow++;

                firstLine = true;
                crntOffset = bufOffset;

                for (Int32 i = 0; i < dataLen; i += bytesPerRow)
                {
                    if ((i + bytesPerRow) > dataLen)
                    {
                        //--------------------------------------------//
                        //                                            //
                        // Last slice of data is less than full.      //
                        //                                            //
                        //--------------------------------------------//

                        sliceLen = dataLen - i;
                    }
                    else
                    {
                        sliceLen = bytesPerRow;
                    }

                    //------------------------------------------------//
                    //                                                //
                    // Extract required details from current slice.   //
                    //                                                //
                    //------------------------------------------------//

                    rowImage = "";

                    for (Int32 j = crntOffset;
                         j < (crntOffset + sliceLen);
                         j++)
                    {
                        sub = (_buf[j]);

                        for (Int32 k = 0; k < 8; k++)
                        {
                            if ((sub & 0x80) != 0)
                                rowImage += "@";
                            else
                                rowImage += " ";

                            sub = sub << 1;
                        }
                    }

                    //------------------------------------------------//
                    //                                                //
                    // Add row (line) to grid.                        //
                    //                                                //
                    //------------------------------------------------//

                    if (firstLine)
                    {
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLFontChar,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "",
                            "Shape",
                            rowImage);
                    }
                    else
                    {
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLFontChar,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "",
                            "",
                            rowImage);
                    }

                    firstLine = false;
                    crntOffset = crntOffset + sliceLen;
                }
            }
            else if (_charClass == ePCLCharClass.BitmapCompressed)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Display shape of compressed bitmap character.              //
                //                                                            //
                //------------------------------------------------------------//

                Boolean firstLine,
                        blackDot;

                Int32 pos,
                      crntOffset,
                      colCt,
                      dotCt,
                      rptCt;

                String rowImage;

                firstLine = true;
                crntOffset = bufOffset;
                pos = 0;

                while (pos < dataLen)
                {
                    colCt = 0;
                    rowImage = "";

                    rptCt = _buf[crntOffset + pos];
                    pos++;

                    blackDot = false;

                    while ((pos < dataLen) && (colCt < _charWidth))
                    {
                        dotCt = _buf[crntOffset + pos];
                        colCt += dotCt;

                        for (Int32 i = 0; i < dotCt; i++)
                        {
                            if (blackDot)
                                rowImage += "@";
                            else
                                rowImage += " ";
                        }

                        blackDot = !blackDot;
                        pos++;
                    }

                    for (Int32 j = 0; j <= rptCt; j++)
                    {
                        if (firstLine)
                        {
                            PrnParseCommon.addTextRow (
                                PrnParseRowTypes.eType.PCLFontChar,
                                _table,
                                PrnParseConstants.eOvlShow.None,
                                "",
                                "",
                                "Shape",
                                rowImage);
                        }
                        else
                        {
                            PrnParseCommon.addTextRow (
                                PrnParseRowTypes.eType.PCLFontChar,
                                _table,
                                PrnParseConstants.eOvlShow.None,
                                "",
                                "",
                                "",
                                rowImage);
                        }

                        firstLine = false;
                    }
                }
            }
        }
    }
}