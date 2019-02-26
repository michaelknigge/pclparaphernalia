using System;
using System.Data;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class handles PCL XL downloadable soft font characters.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    class PrnParseFontCharPCLXL
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private enum eStage
        {
            Start = 0,
            CheckDataHddr,
            ShowDataHddr,
            ShowDataBody,
            ShowDataRem,
            EndOK,
            BadSeqA,
            BadSeqB
        }

        private enum ePCLXLCharFormat
        {
            Bitmap = 0,
            TrueType = 1
        }

        private enum ePCLXLCharClass
        {
            Bitmap = 0,
            TTFClass0 = 0,
            TTFClass1 = 1,
            TTFClass2 = 2
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
        private ePCLXLCharFormat _charFormat;
        private ePCLXLCharClass _charClass;

        private Byte[] _buf;

        private Int32 _fileOffset;
        private Int32 _analysisLevel;

        private Int32 _charLen;
        private Int32 _charRem;
        private Int32 _charPos;
        private Int32 _charHddrLen;
        private Int32 _charDataLen;
        private Int32 _charHeight;
        private Int32 _charWidth;

        private Int32 _charDataSize;

        private Int32 _drawCharMaxHeight;
        private Int32 _drawCharMaxWidth;

        private Boolean _showBinData;
        private Boolean _validChar;

        private Boolean _drawCharShape;

        private Boolean _bitmapFont;
        private Boolean _truetypeFont;

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
                                       Byte[] buf,
                                       Int32 fileOffset,
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
            _showBinData = _options.FlagPCLXLMiscBinData;

            _options.getOptPCLXLDraw (ref _drawCharShape,
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
                if (bufRem < (_blockHddrLen))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // First two bytes (Format & Class) are not in buffer.    //
                    // Initiate continuation.                                 //
                    //                                                        //
                    //--------------------------------------------------------//

                    contType = PrnParseConstants.eContType.PCLXLFontChar;

                    _linkData.setBacktrack (contType, - bufRem);
                }
                else
                {
                    processBlockHeader (ref bufRem,
                                        ref bufOffset);
                }
            }

            if (_nextStage == eStage.CheckDataHddr)
            {
                if (_charHddrLen > bufRem)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Data Header is not entirely within buffer.             //
                    // Initiate (back-tracking) continuation.                 //
                    //                                                        //
                    //--------------------------------------------------------//

                    contType = PrnParseConstants.eContType.PCLXLFontChar;

                    _linkData.setBacktrack (contType, - bufRem);
                }
                else
                {
                    _nextStage = eStage.ShowDataHddr;
                }
            }

            if (_nextStage == eStage.ShowDataHddr)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Process character data header.                             //
                //                                                            //
                //------------------------------------------------------------//

                processDataHeader (ref bufRem,
                                   ref bufOffset);
            }

            if ((_nextStage == eStage.ShowDataBody) && (_charRem != 0))
            {
                //------------------------------------------------------------//
                //                                                            //
                // Output details of (first part of) variable data.           //
                //                                                            //
                //------------------------------------------------------------//

                if (_charFormat == ePCLXLCharFormat.Bitmap)
                {
                    processRasterDataBody (ref bufRem,
                                           ref bufOffset);
                }
                else if (_charFormat == ePCLXLCharFormat.TrueType)
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
                // Output details of (remainder of) variable data.            //
                //                                                            //
                //------------------------------------------------------------//

                if (_charFormat == ePCLXLCharFormat.Bitmap)
                {
                    processRasterDataRem (ref bufRem,
                                          ref bufOffset);
                }
                else if (_charFormat == ePCLXLCharFormat.TrueType)
                {
                    processTrueTypeDataBody (ref bufRem,
                                             ref bufOffset);
                }
                else
                {
                    _nextStage = eStage.BadSeqA;
                }
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
                    "Processing of header abandoned");
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

                    contType = PrnParseConstants.eContType.PCLXLFontChar;

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
                        "PCLXL Binary",
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
        //         1   Class                                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processBlockHeader (ref Int32 bufRem,
                                         ref Int32 bufOffset)
        {
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
                "[ " + _blockHddrLen + " bytes ]",
                "Block header");

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

            _charFormat = (ePCLXLCharFormat) _buf[bufOffset];
            _charClass  = (ePCLXLCharClass) _buf[bufOffset + 1];
            _validChar  = true;

            //----------------------------------------------------------------//
            //                                                                //
            // Format (byte 0).                                               //
            //                                                                //
            //----------------------------------------------------------------//

            switch (_charFormat)
            {
                case ePCLXLCharFormat.Bitmap:
                    itemDesc = "0: Bitmap";
                    _bitmapFont = true;
                    _truetypeFont = false;

                    break;

                case ePCLXLCharFormat.TrueType:
                    itemDesc = "1: Truetype";
                    _bitmapFont = false;
                    _truetypeFont = true;

                    break;

                default:
                    itemDesc = _charFormat.ToString () + ": Unknown";
                    _bitmapFont = false;
                    _truetypeFont = false;
                    _validChar = false;
                    break;
            }

            PrnParseCommon.addTextRow (
                PrnParseRowTypes.eType.PCLXLFontChar,
                _table,
                PrnParseConstants.eOvlShow.None,
                "",
                "Format:",
                "",
                itemDesc);

            //----------------------------------------------------------------//
            //                                                                //
            // Class (byte 1).                                                //
            //                                                                //
            //----------------------------------------------------------------//

            itemDesc = _charClass.ToString () + ": Unknown";

            if (_bitmapFont)
            {
                if (_charClass == ePCLXLCharClass.Bitmap)
                {
                    itemDesc = "0: Bitmap";
                    _charHddrLen = 8;
                }
                else
                    _validChar = false;
            }
            else if (_truetypeFont)
            {
                if (_charClass == ePCLXLCharClass.TTFClass0)
                {
                    itemDesc = "0: Dense";
                    _charHddrLen = 4;
                }
                else if (_charClass == ePCLXLCharClass.TTFClass1)
                {
                    itemDesc = "1: Sparse";
                    _charHddrLen = 8;
                }
                else if (_charClass == ePCLXLCharClass.TTFClass2)
                {
                    itemDesc = "2: Sparse - vertical rotated";
                    _charHddrLen = 10;
                }
                else
                    _validChar = false;
            }

            PrnParseCommon.addTextRow (
                PrnParseRowTypes.eType.PCLXLFontChar,
                _table,
                PrnParseConstants.eOvlShow.None,
                "",
                "Class:",
                "",
                itemDesc);

            //----------------------------------------------------------------//
            //                                                                //
            // Adjust pointers & check validity.                              //
            //                                                                //
            //----------------------------------------------------------------//

            bufRem    = bufRem    - _blockHddrLen;
            bufOffset = bufOffset + _blockHddrLen;
            _charRem  = _charRem  - _blockHddrLen;

            if (_validChar)
            {
                _nextStage = eStage.CheckDataHddr;
            }
            else
            {
                _nextStage = eStage.BadSeqA;

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.MsgWarning,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "",
                    "",
                    "Format and/or Class invalid");
            }
        }
        
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s D a t a H e a d e r                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Provide an interpretation of the contents of the Character Data    //
        // header bytes.                                                      //
        // The content of the header depends on the Format and Class          //
        // attributes (found in the block header).                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processDataHeader (ref Int32 bufRem,
                                        ref Int32 bufOffset)
        {
            UInt16 ui16a;
            
            UInt32 ui32a;

            Int16 si16a;

            //----------------------------------------------------------------//
            //                                                                //
            // Show size (calculated when block header processed) and         //
            // (optionally) the binary header data.                           //
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
                "[ " + _charHddrLen + " bytes ]",
                "Character data header");

            if (_showBinData)
            {
                PrnParseData.processBinary (
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    _buf,
                    _fileOffset,
                    bufOffset,
                    _charHddrLen,
                    "",
                    true,
                    false,
                    true,
                    _indxOffsetFormat,
                    _analysisLevel);
            }

            if (_charFormat == ePCLXLCharFormat.Bitmap)
            {
                UInt16 bytesPerRow;

                //------------------------------------------------------------//
                //                                                            //
                // Bitmap font.                                               //
                //------------------------------------------------------------//
                //                                                            //
                // Left offset (bytes 0 & 1).                                 //
                //                                                            //
                // Distance from the 'reference point' to the left side of    //
                // the character.                                             //
                //                                                            //
                //------------------------------------------------------------//

                si16a = (Int16) ((_buf[bufOffset] * 256) +
                                  _buf[bufOffset + 1]);

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLXLFontChar,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Left Offset:",
                    "",
                    si16a.ToString() + " dots");

                //------------------------------------------------------------//
                //                                                            //
                // Top offset (bytes 2 & 3).                                  //
                //                                                            //
                // Distance from the 'reference point' to the top of the      //
                // character.                                                 //
                //                                                            //
                //------------------------------------------------------------//

                si16a = (Int16) ((_buf[bufOffset + 2] * 256) +
                                  _buf[bufOffset + 3]);

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLXLFontChar,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Top Offset:",
                    "",
                    si16a.ToString () + " dots");

                //------------------------------------------------------------//
                //                                                            //
                // Character Width (bytes 4 & 5).                             //
                //                                                            //
                //------------------------------------------------------------//

                _charWidth = (UInt16) ((_buf[bufOffset + 4] * 256) +
                                   _buf[bufOffset + 5]);

                bytesPerRow = (UInt16) ((_charWidth / 8));

                if ((_charWidth % 8) != 0)
                    bytesPerRow++;

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLXLFontChar,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Character Width:",
                    "",
                    _charWidth + " dots (requires " +
                    bytesPerRow + " padded bytes per row)");

                //------------------------------------------------------------//
                //                                                            //
                // Character Height (bytes 6 & 7).                            //
                //                                                            //
                //------------------------------------------------------------//

                _charHeight = (UInt16) ((_buf[bufOffset + 6] * 256) +
                                         _buf[bufOffset + 7]);

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLXLFontChar,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Character Height:",
                    "",
                    _charHeight.ToString () + " dots");

                //------------------------------------------------------------//
                //                                                            //
                // Estimated character data size:                             //
                //    ((paddedCharWidth) / 8) * charHeight) bytes.            //
                //                                                            //
                //------------------------------------------------------------//
                
                ui32a = (UInt32) (bytesPerRow * _charHeight);

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLXLFontChar,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Raster data size:",
                    "",
                    ui32a + " bytes (assuming " +
                    _charHeight + " rows of " +
                    bytesPerRow + " bytes)");

                if (ui32a != (_charLen - _blockHddrLen - _charHddrLen))
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
                        "data header = "  + _charHddrLen  + " bytes");
                }
            }
            else if (_truetypeFont)
            {
                //------------------------------------------------------------//
                // TrueType font.                                             //
                //------------------------------------------------------------//
                //                                                            //
                // Character Data Size (bytes 0 & 1).                         //
                // This gives the size of the remainder of the header         //
                // (including these two bytes) plus the glyph data size.      //
                // It should hence be equal to the total CharacterLength      //
                // minus the block header length (the size of the Format and  //
                // Class bytes).                                              //
                //                                                            //
                //------------------------------------------------------------//

                _charDataSize = (Int16) ((_buf[bufOffset] * 256) +
                                          _buf[bufOffset + 1]);

                ui16a = (UInt16) (_charDataSize - _charHddrLen);

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLXLFontChar,
                    _table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Character Data Size:",
                    "",
                    _charDataSize.ToString() + " bytes " +
                    " (header = " + _charHddrLen + 
                    "; glyph data = "  + ui16a + ")");

                if ((_charDataSize + _blockHddrLen) != _charLen)
                {
                    _validChar = false;

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgWarning,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "*** Warning ***",
                        "",
                        "Character Data Size (" + _charDataSize + " bytes)" +
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
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Left Side Bearing.                                     //
                    // For class 0 - not present.                             //
                    //           1 - bytes 2 & 3.                             //
                    //           2 - bytes 2 & 3.                             //
                    //                                                        //
                    //--------------------------------------------------------//

                    if ((_charClass == ePCLXLCharClass.TTFClass1) ||
                        (_charClass == ePCLXLCharClass.TTFClass2))
                    {
                        si16a = (Int16) ((_buf[bufOffset + 2] * 256) +
                                          _buf[bufOffset + 3]);

                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLXLFontChar,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "Left Side Bearing:",
                            "",
                            si16a.ToString () + " font units");
                    }

                    //--------------------------------------------------------//
                    //                                                        //
                    // Advance Width.                                         //
                    // For class 0 - not present.                             //
                    //           1 - bytes 4 & 5.                             //
                    //           2 - bytes 4 & 5.                             //
                    //                                                        //
                    //--------------------------------------------------------//

                    if ((_charClass == ePCLXLCharClass.TTFClass1) ||
                        (_charClass == ePCLXLCharClass.TTFClass2))
                    {
                        si16a = (Int16) ((_buf[bufOffset + 4] * 256) +
                                          _buf[bufOffset + 5]);
        
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLXLFontChar,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "Advance Width:",
                            "",
                            si16a.ToString () + " font units");
                    }

                    //--------------------------------------------------------//
                    //                                                        //
                    // Top Side Bearing.                                      //
                    // For class 0 - not present.                             //
                    //           1 - not present.                             //
                    //           2 - bytes 6 & 7.                             //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (_charClass == ePCLXLCharClass.TTFClass2)
                    {
                        si16a = (Int16) ((_buf[bufOffset + 6] * 256) +
                                          _buf[bufOffset + 7]);

                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLXLFontChar,
                            _table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "Top Side Bearing:",
                            "",
                            si16a.ToString () + " font units");
                    }

                    //--------------------------------------------------------//
                    //                                                        //
                    // TrueType Glyph ID.                                     //
                    // For class 0 - bytes  2 &  3.                           //
                    //           1 - bytes  6 &  7.                           //
                    //           2 - bytes  8 &  9.                           //
                    //                                                        //
                    //--------------------------------------------------------//

                    ui16a = 0;

                    if (_charClass == ePCLXLCharClass.TTFClass0)
                    {
                        ui16a = (UInt16) ((_buf[bufOffset + 2] * 256) +
                                           _buf[bufOffset + 3]);
                    }
                    else if (_charClass == ePCLXLCharClass.TTFClass1)
                    {
                        ui16a = (UInt16) ((_buf[bufOffset + 6] * 256) +
                                           _buf[bufOffset + 7]);
                    }
                    else if (_charClass == ePCLXLCharClass.TTFClass2)
                    {
                        ui16a = (UInt16) ((_buf[bufOffset + 8] * 256) +
                                           _buf[bufOffset + 9]);
                    }

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLXLFontChar,
                        _table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "TrueType Glyph ID:",
                        "",
                        ui16a.ToString ());
                }
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Adjust pointers.                                               //
            //                                                                //
            //----------------------------------------------------------------//

            bufRem       = bufRem    - _charHddrLen;
            bufOffset    = bufOffset + _charHddrLen;
            _charRem     = _charRem  - _charHddrLen;
            _charDataLen = _charRem;

            if (_validChar)
                _nextStage = eStage.ShowDataBody;
            else
                _nextStage = eStage.BadSeqA;
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

                    contType   = PrnParseConstants.eContType.PCLXLFontChar;

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
                    "PCLXL Binary",
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
                    rasterDraw(bufOffset, binDataLen, shapeTooLarge);
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

                contType = PrnParseConstants.eContType.PCLXLFontChar;

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
                    "PCLXL Binary",
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
        // Process TrueType character data.                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void processTrueTypeDataBody (ref Int32 bufRem,
                                              ref Int32 bufOffset)
        {
            PrnParseConstants.eContType contType;
            
            Int32 binDataLen;

            if (_charRem > bufRem)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Remainder of sequence is not in buffer.                    //
                // Initiate continuation.                                     //
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
                    "PCLXL Binary",
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

                bufRem    = bufRem - binDataLen;
                bufOffset = bufOffset + binDataLen;
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
            else if (_charClass == ePCLXLCharClass.Bitmap)
            {
                //----------------------------------------------------//
                //                                                    //
                // Display shape of Bitmap character.                 //
                // Note that we don't expect that this function will  //
                // have been invoked for any other character class!   //
                //                                                    //
                //----------------------------------------------------//

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
                    //------------------------------------------------//
                    //                                                //
                    // Calculate slice length.                        //
                    //                                                //
                    //------------------------------------------------//

                    if ((i + bytesPerRow) > dataLen)
                        sliceLen = dataLen - i;
                    else
                        sliceLen = bytesPerRow;

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
                            PrnParseRowTypes.eType.PCLXLFontChar,
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
                            PrnParseRowTypes.eType.PCLXLFontChar,
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
        }
    }
}