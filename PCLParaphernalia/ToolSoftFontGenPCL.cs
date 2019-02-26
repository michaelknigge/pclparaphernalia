using System;
using System.Data;
using System.IO;
using System.Windows;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides PCL handling for the Soft Font Generate tool.
    /// 
    /// © Chris Hutchinson 2012
    /// 
    /// </summary>

    class ToolSoftFontGenPCL
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const Int32 cSizeHddrFmt15Max   = 0xffff;
        const Int32 cSizeHddrDesc       = 72;
        const Int32 cSizeHddrTrail      = 2;

        const Int32 cSizeCharHddr       = 4;
        const Int32 cSizeCharGlyphHddr  = 4;
        const Int32 cSizeCharTrail      = 2;

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private ToolSoftFontGenPCLCommon _baseHandler;

        private Stream _opStream = null;
        private BinaryWriter _binWriter = null;

        private ToolSoftFontGenTTF _ttfHandler = null;

        private DataTable _tableLog;

        private Boolean _symbolMapping = false;
        
        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // T o o l S o f t G e n P C L                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public ToolSoftFontGenPCL(DataTable tableLog,
                                   ToolSoftFontGenTTF ttfHandler)
        {
            _baseHandler = new ToolSoftFontGenPCLCommon();
 
            _tableLog = tableLog;

            _ttfHandler = ttfHandler;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e F o n t                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate PCLETTO font.                                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean generateFont (ref String pclFilename,
                                     ref Boolean monoSpaced,
                                     Boolean symbolMapping,
                                     Boolean fmt16,
                                     Boolean segGTLast,
                                     Boolean usePCLT,
                                     Boolean symSetUnbound,
                                     Boolean tabvmtxPresent,
                                     Boolean flagVMetrics,
                                     Byte    symSetType,
                                     Int32   sizeCharSet,
                                     UInt16  symSet,
                                     UInt16  style,
                                     SByte   strokeWeight,
                                     UInt16  typeface,
                                     UInt64  charCollComp,
                                     Byte[]  conversionText)
        {
            Boolean flagOK = true;
            Boolean useVMetrics;

            if (fmt16)
                useVMetrics = flagVMetrics;
            else
                useVMetrics = false;

            _baseHandler.initialise (_ttfHandler);

            _symbolMapping = symbolMapping;

            //----------------------------------------------------------------//
            //                                                                //
            // Open print file and stream.                                    //
            //                                                                //
            //----------------------------------------------------------------//

            try
            {
                _baseHandler.streamOpen (ref pclFilename,
                                         false,
                                         ref _binWriter,
                                         ref _opStream);

            }

            catch (Exception exc)
            {
                flagOK = false;

                MessageBox.Show (exc.ToString (),
                                "Failure to open output font file",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }

            if (flagOK)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Generate font file contents.                               //
                //                                                            //
                //------------------------------------------------------------//
                
                UInt16 numChars = 0,
                       firstCode = 0,
                       lastCode = 0,
                       maxGlyphId = 0,
                       maxComponentDepth = 0,
                       unitsPerEm = 0;

                Boolean glyphZeroExists = false;

                _ttfHandler.glyphReferencedUnmarkAll ();

                _ttfHandler.getBasicMetrics (ref numChars,
                                             ref firstCode,
                                             ref lastCode,
                                             ref maxGlyphId,
                                             ref maxComponentDepth,
                                             ref unitsPerEm,
                                             ref glyphZeroExists);

                try
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Write font header.                                     //
                    //                                                        //
                    //--------------------------------------------------------//

                    writeHddr (ref monoSpaced,
                               fmt16,
                               segGTLast,
                               usePCLT,
                               glyphZeroExists,
                               symSetUnbound,
                               tabvmtxPresent,
                               useVMetrics,
                               symSetType,
                               firstCode,
                               lastCode,
                               numChars,
                               unitsPerEm,
                               symSet,
                               style,
                               strokeWeight,
                               typeface,
                               charCollComp,
                               conversionText);

                    //--------------------------------------------------------//
                    //                                                        //
                    // Write Galley Character and font characters.            //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (glyphZeroExists)
                        writeChar (0xffff, 0, 0, 0, maxGlyphId);

                    writeCharSet (maxGlyphId, sizeCharSet, symSetUnbound);

                    //--------------------------------------------------------//
                    //                                                        //
                    // Close streams and files.                               //
                    //                                                        //
                    //--------------------------------------------------------//

                    _binWriter.Close ();
                    _opStream.Close ();

                    _ttfHandler.fontFileClose ();
                }

                catch (Exception exc)
                {
                    flagOK = false;

                    MessageBox.Show (exc.ToString (),
                                    "Failure to write font file",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                }
            }

            return flagOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l s B y t e                                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return low (least-significant) byte from supplied unsigned 16-bit  //
        // integer.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Byte lsByte(UInt16 value)
        {
            return (Byte) (value & 0x00ff);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l s U I n t 1 6                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return low (least-significant) unsigned 16-bit integer from        //
        // supplied unsigned 32-bit integer.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private UInt16 lsUInt16(UInt32 value)
        {
            return (UInt16) (value & 0x0000ffff);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m s B y t e                                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return high (most-significant) byte from supplied unsigned 16-bit  //
        // integer.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Byte msByte(UInt16 value)
        {
            return (Byte) ((value & 0xff00) >> 8);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m s U I n t 1 6                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return high (most-significant) unsigned 16-bit integer from        //
        // supplied unsigned 32-bit integer.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private UInt16 msUInt16(UInt32 value)
        {
            return (UInt16) ((value & 0xffff0000) >> 16);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e C h a r                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write PCL format character data:                                   //
        //                                                                    //
        //    <esc>*c#E         Character Code:                               //
        //                      #      = decimal character code               //
        //    <esc>(s#W[data]   Character Descriptor / Data                   //
        //                      #      = number of bytes of data              //
        //                      [data] = font character data                  //
        //                                                                    //
        // Note that the function may be called recursively, if a glyph is    //
        // composite (i.e. made up of two or more components).                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void writeChar (UInt16 charCode,
                                UInt16 codepoint,
                                UInt16 glyphId,
                                UInt16 depth,
                                UInt16 maxGlyphId)
        {
            UInt16 glyphWidth = 0,
                   glyphHeight = 0,
                   charBlockSize = 0,
                   charDataSize = 0;

            Int16 glyphLSB = 0,
                  glyphTSB = 0;

            UInt32 glyphOffset = 0,
                   glyphLength = 0;

            Boolean glyphComposite = false;

            Byte checksumMod256;

            Byte[] charHddr      = new Byte[cSizeCharHddr];
            Byte[] charGlyphHddr = new Byte[cSizeCharGlyphHddr];
            Byte[] charTrail     = new Byte[cSizeCharTrail];
            Byte[] glyphData     = null;

            //----------------------------------------------------------------//
            //                                                                //
            // Mark glyph as used.                                            //
            // These markers are checked for composite sub-glyphs.            //
            //                                                                //
            //----------------------------------------------------------------//

            _ttfHandler.glyphReferencedMark (glyphId);

            //----------------------------------------------------------------//
            //                                                                //
            // Get glyph details:                                             //
            //    advance width.                                              //
            //    left-side bearing.                                          //
            //    offset and length of the glyph data in the TTF file.        //
            //                                                                //
            //----------------------------------------------------------------//

            _ttfHandler.getGlyphData (glyphId,
                                      ref glyphWidth,
                                      ref glyphHeight,  // not used here
                                      ref glyphLSB,
                                      ref glyphTSB,     // not used here
                                      ref glyphOffset,
                                      ref glyphLength,
                                      ref glyphComposite);

            //----------------------------------------------------------------//
            //                                                                //
            // Log character details.                                         //
            //                                                                //
            //----------------------------------------------------------------//

            ToolSoftFontGenLog.logCharDetails (_tableLog,
                                               false,
                                               glyphComposite,
                                               charCode,
                                               codepoint,
                                               glyphId,
                                               depth,
                                               glyphWidth,
                                               glyphHeight,
                                               glyphLSB,
                                               glyphTSB,
                                               glyphOffset,
                                               glyphLength);

            //----------------------------------------------------------------//
            //                                                                //
            // Calculate total size of header.                                //
            //                                                                //
            // Write PCL 'Character Code' escape sequence.                    //
            // Write PCL 'Character Definition' escape sequence.              //
            //                                                                //
            //----------------------------------------------------------------//

            charBlockSize = (UInt16) (cSizeCharHddr + cSizeCharGlyphHddr +
                                      glyphLength   + cSizeCharTrail);

            PCLWriter.charDownloadCode (_binWriter, charCode);

            PCLWriter.charDownloadDesc (_binWriter, charBlockSize);

            //----------------------------------------------------------------//
            //                                                                //
            // Write Format 15 header.                                        //
            // This character format is used with both Format 15 and          //
            // Format 16 font headers.                                        //
            //                                                                //
            //----------------------------------------------------------------//

            charHddr[0]  = 15;                  // Format = 15
            charHddr[1]  = 0;                   // Continuation = false
            charHddr[2]  = 2;                   // Descriptor size
            charHddr[3]  = 15;                  // Class = 15

            _baseHandler.writeBuffer (cSizeCharHddr, charHddr);

            //----------------------------------------------------------------//
            //                                                                //
            // Write glyph header.                                            //
            // This counts towards the checksum recorded in the trailer.      //
            //                                                                //
            //----------------------------------------------------------------//

            checksumMod256 = 0;

            charDataSize = (UInt16) (cSizeCharGlyphHddr + glyphLength);

            charGlyphHddr[0]  = msByte (charDataSize);
            charGlyphHddr[1]  = lsByte (charDataSize);
            charGlyphHddr[2]  = msByte (glyphId);
            charGlyphHddr[3]  = lsByte (glyphId);

            _baseHandler.writeCharFragment (cSizeCharGlyphHddr,
                                            charGlyphHddr,
                                            ref checksumMod256);

            //----------------------------------------------------------------//
            //                                                                //
            // Write TrueType glyph data (copied from TrueType font file).    //
            // The data is read into a dynamically allocated buffer because:  //
            //    -  This avoids the complication of having a fixed-length    //
            //       buffer and a loop to read the data in chunks.            //
            //    -  Not having a static buffer allows the function to be     //
            //       called recursively.                                      //
            //                                                                //
            //----------------------------------------------------------------//
            
            if (glyphLength > 0)
            {
                Boolean flagOK = true;

                glyphData = new Byte[glyphLength];
        
                flagOK = _ttfHandler.readByteArray ((Int32) glyphOffset,
                                                    (Int32) glyphLength,
                                                    ref glyphData);
                // TODO: what if flagOK = true (i.e. read fails?

                _baseHandler.writeCharFragment ((Int32) glyphLength,
                                                glyphData,
                                                ref checksumMod256);
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Write trailer (Reserved byte and Checksum byte).               //
            //                                                                //
            //----------------------------------------------------------------//

            checksumMod256 = (Byte) ((256 - checksumMod256) % 256);

            charTrail[0]  = 0;                  // Reserved byte
            charTrail[1]  = checksumMod256;     // Checksum byte

            _baseHandler.writeBuffer (cSizeCharTrail, charTrail);

            //----------------------------------------------------------------//
            //                                                                //
            // Handler composite glyphs.                                      //
            //                                                                //
            //----------------------------------------------------------------//

            if (glyphComposite)
            {
                // if we move this to TTFHandler, do the maxGlyphId check there instead

                Int32 indBuf;

                UInt16 glyphCompFlags,
                       glyphCompId;

                indBuf = 10; // point to first set of component data //

                do
                {
                    glyphCompFlags = (UInt16) ((glyphData[indBuf] << 8) +
                                                glyphData[indBuf + 1]);
                    glyphCompId    = (UInt16) ((glyphData[indBuf + 2] << 8) +
                                                glyphData[indBuf + 3]);

                    if (glyphCompId > maxGlyphId)
                    {
                        // flagOK = false;

                        ToolSoftFontGenLog.logError (
                            _tableLog, MessageBoxImage.Error,
                            "Composite glyph identifier " + glyphCompId +
                            " > maximum of " + maxGlyphId);
                    }
                    else
                    {
                        if (_ttfHandler.glyphReferencedCheck (glyphCompId))
                        {
                            ToolSoftFontGenLog.logCharDetails (
                                _tableLog,
                                true,
                                _ttfHandler.glyphCompositeCheck (glyphCompId),
                                0,
                                0,
                                glyphCompId,
                                depth,
                                0,
                                0,
                                0,
                                0,
                                0,
                                0);
                        }
                        else
                        {
                            // flagOK = 
                            writeChar (0xffff, 0, glyphCompId,
                                       (UInt16) (depth + 1), maxGlyphId);
                         }
                    }

                    // if flagOK
                    {
                        indBuf += 4;

                        if ((glyphCompFlags &
                            ToolSoftFontGenTTF.mask_glyf_compFlag_ARG_1_AND_2_ARE_WORDS) != 0)
                            indBuf += 4;
                        else
                            indBuf += 2;

                        if ((glyphCompFlags &
                            ToolSoftFontGenTTF.mask_glyf_compFlag_WE_HAVE_A_TWO_BY_TWO) != 0)
                            indBuf += 8;
                        else if ((glyphCompFlags &
                            ToolSoftFontGenTTF.mask_glyf_compFlag_WE_HAVE_AN_X_AND_Y_SCALE) != 0)
                            indBuf += 4;
                        else  if ((glyphCompFlags &
                            ToolSoftFontGenTTF.mask_glyf_compFlag_WE_HAVE_A_SCALE) != 0)
                            indBuf += 2;
                    }
                } while ((glyphCompFlags &
                            ToolSoftFontGenTTF.mask_glyf_compFlag_MORE_COMPONENTS) != 0);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e C h a r S e t                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write PCL format characters for the set of characters in the       //
        // selected symbol set.                                               //
        //                                                                    //
        // For each character, write Character Code and Character Definition  //
        // sequences.                                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void writeCharSet (UInt16  maxGlyphId,
                                   Int32   sizeCharSet,
                                   Boolean symSetUnbound)
        {
            Boolean glyphExists = false;

            UInt16 startCode,
                   endCode,
                   glyphId = 0,
                   codepoint = 0;

            //----------------------------------------------------------------//
            //                                                                //
            // Download individual character glyphs.                          //
            //                                                                //
            //----------------------------------------------------------------//

            startCode = 0;
            endCode = (UInt16) (sizeCharSet - 1);

            for (Int32 i = startCode; i <= endCode; i++)
            {
                UInt16 charCode = (UInt16) i;

                glyphExists = _ttfHandler.getCharData (charCode,
                                                       ref codepoint,
                                                       ref glyphId);

                if (glyphExists)
                {
                    writeChar (charCode, codepoint, glyphId, 0, maxGlyphId);
                }
                else if (! symSetUnbound)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Check whether or not all (graphic) characters in the   //
                    // target character set have glyphs which are present.    //
                    // Report any that don't.                                 //
                    //                                                        //
                    // For standard 'Unicode' encoded (usually text) fonts,   //
                    // ignore characters for which the target code-point has  //
                    // been set to 0xffff (indicating character not present)  //
                    // and perhaps those with target code-points in the       //
                    // 'control code' ranges (0x00->0x1f and 0x7f->0x9f).     //
                    //                                                        //
                    // For 'Symbol' encoded fonts, perhaps ignore characters  //
                    // with target code-points in the 'control code' ranges   //
                    // (0xf000->0xf01f and 0xf07f->0xf09f).                   //
                    //                                                        //
                    //--------------------------------------------------------//

                    if (codepoint == 0xffff)
                    {
                        // do nothing - character-code not mapped
                    }
                    /*
                    else if ((!_symbolMapping) &&
                             (codepoint < 0x20) ||
                             ((codepoint >= 0x7f) && (codepoint <= 0x9f)))
                    {
                        // do nothing //
                    }
                    else if ((_symbolMapping) &&
                             (codepoint < 0xf020) ||
                             ((codepoint >= 0xf07f) && (codepoint <= 0xf09f)))
                    {
                        // do nothing //
                    }
                    */
                    else
                    {
                        ToolSoftFontGenLog.logMissingChar (
                            _tableLog,
                            charCode,
                            codepoint);
                    }
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e H d d r                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate font header descriptor, segmented data and checksum byte. //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean writeHddr (ref Boolean monoSpaced,
                                   Boolean fmt16,
                                   Boolean segGTLast,
                                   Boolean usePCLT,
                                   Boolean glyphZeroExists,
                                   Boolean symSetUnbound,
                                   Boolean tabvmtxPresent,
                                   Boolean flagVMetrics,
                                   Byte    symSetType,
                                   UInt16  firstCode,
                                   UInt16  lastCode,
                                   UInt16  numChars,
                                   UInt16  unitsPerEm,
                                   UInt16  symSet,
                                   UInt16  style,
                                   SByte   strokeWeight,
                                   UInt16  typeface,
                                   UInt64  charCollComp,
                                   Byte [] conversionText)
        {
            Boolean flagOK = true;

            UInt16 cellWidth = 0,
                   cellHeight = 0,
                   textWidth = 0,
                   textHeight = 0,
                   pitch = 0,
                   xHeight = 0,
                   capHeight = 0,
                   mUlinePosU = 0,
                   mUlineDep = 0;

            Int16 mUlinePos = 0;

            UInt32 fontNo = 0;

            Int32 sum;
            Int32 convTextLen;
            Int32 hddrLen;

            Byte mod256;
            Byte serifStyle = 0;
            Byte fontFormat;
            Byte fontType;
            Byte fontSpacing;

            SByte widthType = 0;

            Byte [] fontNamePCLT = new Byte [ToolSoftFontGenTTF.cSizeFontname];
            Byte [] panoseData = new Byte [ToolSoftFontGenTTF.cSizePanose];
            Byte [] hddrDesc = new Byte [cSizeHddrDesc];

            //----------------------------------------------------------------//
            //                                                                //
            // Get relevant PCL data from TrueType font.                      //
            //                                                                //
            //----------------------------------------------------------------//

            monoSpaced = false;

            _ttfHandler.getPCLFontHeaderData (usePCLT,
                                              ref monoSpaced,
                                              ref cellWidth,
                                              ref cellHeight,
                                              ref textWidth,
                                              ref textHeight,
                                              ref pitch,
                                              ref xHeight,
                                              ref capHeight,
                                              ref mUlinePos,
                                              ref mUlineDep,
                                              ref fontNo,
                                              ref serifStyle,
                                              ref widthType,
                                              ref fontNamePCLT,
                                              ref panoseData);

            mUlinePosU = (UInt16)mUlinePos;

            //----------------------------------------------------------------//

            if (fmt16)
                fontFormat = 16;            // Format = Universal
            else
                fontFormat = 15;            // Format = TrueType scalable

            if (monoSpaced)
                fontSpacing = 0;            // Spacing = Fixed-pitch
            else
                fontSpacing = 1;            // Spacing = Proportional

            if (symSetUnbound)
            {
                fontType = 11;              // Type = unbound Unicode-indexed
                firstCode = 0;
                lastCode = numChars;
            }
            else
            {
                fontType = symSetType;      // Type = as per target symbol set
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Calculate total size of header.                                //
            // Write PCL 'download header' escape sequence.                   //
            //                                                                //
            //----------------------------------------------------------------//

            convTextLen = conversionText.Length;

            hddrLen = cSizeHddrDesc +
                      _baseHandler.getHddrSegmentsLen (
                            false,
                            fmt16,
                            glyphZeroExists,
                            symSetUnbound,
                            tabvmtxPresent,
                            flagVMetrics,
                            convTextLen) +
                      cSizeHddrTrail;

            if ((hddrLen > cSizeHddrFmt15Max) && (! fmt16))
            {
                flagOK = false;

                MessageBox.Show ("Header length of '" + hddrLen +
                                 "' is incompatible with 'format 15'" +
                                 " font.",
                                 "Soft font header invalid",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
            }
            else
            {

                PCLWriter.fontDownloadHddr (_binWriter, (UInt32)hddrLen);

                //------------------------------------------------------------//
                //                                                            //
                // Write font header descriptor.                              //
                //                                                            //
                //------------------------------------------------------------//

                hddrDesc [0] = msByte (cSizeHddrDesc);
                hddrDesc [1] = lsByte (cSizeHddrDesc);

                hddrDesc [2] = fontFormat;          // Font Format
                hddrDesc [3] = fontType;            // Font Type
                hddrDesc [4] = msByte (style);      // Style MSB
                hddrDesc [5] = 0;                   // Reserved
                hddrDesc [6] = 0;                   // Baseline Position MSB
                hddrDesc [7] = 0;                   // Baseline Position LSB
                hddrDesc [8] = msByte (cellWidth);  // Cell width MSB
                hddrDesc [9] = lsByte (cellWidth);  // Cell Width LSB
                hddrDesc [10] = msByte (cellHeight); // Cell Height MSB
                hddrDesc [11] = lsByte (cellHeight); // Cell Height LSB
                hddrDesc [12] = 0;                   // Orientation
                hddrDesc [13] = fontSpacing;         // Spacing
                hddrDesc [14] = msByte (symSet);     // Symbol Set MSB
                hddrDesc [15] = lsByte (symSet);     // Symbol Set LSB
                hddrDesc [16] = msByte (pitch);      // Pitch MSB
                hddrDesc [17] = lsByte (pitch);      // Pitch LSB
                hddrDesc [18] = 0;                   // Height MSB
                hddrDesc [19] = 0;                   // Height LSB
                hddrDesc [20] = msByte (xHeight);    // xHeight MSB
                hddrDesc [21] = msByte (xHeight);    // xHeight LSB
                hddrDesc [22] = (Byte)widthType;    // Width Type
                hddrDesc [23] = lsByte (style);      // Style LSB
                hddrDesc [24] = (Byte)strokeWeight; // Stroke Weight
                hddrDesc [25] = lsByte (typeface);   // Typeface LSB
                hddrDesc [26] = msByte (typeface);   // Typeface MSB
                hddrDesc [27] = serifStyle;          // Serif Style
                hddrDesc [28] = 2;                   // Quality = Letter
                hddrDesc [29] = 0;                   // Placement
                hddrDesc [30] = 0;                   // Underline Position
                hddrDesc [31] = 0;                   // Underline Thickness
                hddrDesc [32] = msByte (textHeight); // Text Height MSB
                hddrDesc [33] = lsByte (textHeight); // Text Height LSB
                hddrDesc [34] = msByte (textWidth);  // Text Width MSB
                hddrDesc [35] = lsByte (textWidth);  // Text Width LSB
                hddrDesc [36] = msByte (firstCode);  // First Code MSB
                hddrDesc [37] = lsByte (firstCode);  // First Code LSB
                hddrDesc [38] = msByte (lastCode);   // Last Code MSB
                hddrDesc [39] = lsByte (lastCode);   // Last Code LSB
                hddrDesc [40] = 0;                   // Pitch Extended
                hddrDesc [41] = 0;                   // Height Extended
                hddrDesc [42] = msByte (capHeight);  // Cap Height MSB
                hddrDesc [43] = lsByte (capHeight);  // Cap Height LSB
                hddrDesc [44] = msByte (msUInt16 (fontNo));  // Font No. byte 0
                hddrDesc [45] = lsByte (msUInt16 (fontNo));  // Font No. byte 1
                hddrDesc [46] = msByte (lsUInt16 (fontNo));  // Font No. byte 2
                hddrDesc [47] = lsByte (lsUInt16 (fontNo));  // Font No. byte 3
                hddrDesc [48] = fontNamePCLT [0];     // Font Name byte 0
                hddrDesc [49] = fontNamePCLT [1];     // Font Name byte 1
                hddrDesc [50] = fontNamePCLT [2];     // Font Name byte 2
                hddrDesc [51] = fontNamePCLT [3];     // Font Name byte 3
                hddrDesc [52] = fontNamePCLT [4];     // Font Name byte 4
                hddrDesc [53] = fontNamePCLT [5];     // Font Name byte 5
                hddrDesc [54] = fontNamePCLT [6];     // Font Name byte 6
                hddrDesc [55] = fontNamePCLT [7];     // Font Name byte 7
                hddrDesc [56] = fontNamePCLT [8];     // Font Name byte 8
                hddrDesc [57] = fontNamePCLT [9];     // Font Name byte 9
                hddrDesc [58] = fontNamePCLT [10];    // Font Name byte 10
                hddrDesc [59] = fontNamePCLT [11];    // Font Name byte 11
                hddrDesc [60] = fontNamePCLT [12];    // Font Name byte 12
                hddrDesc [61] = fontNamePCLT [13];    // Font Name byte 13
                hddrDesc [62] = fontNamePCLT [14];    // Font Name byte 14
                hddrDesc [63] = fontNamePCLT [15];    // Font Name byte 15
                hddrDesc [64] = msByte (unitsPerEm); // Scale Factor MSB
                hddrDesc [65] = lsByte (unitsPerEm); // Scale Factor LSB
                hddrDesc [66] = msByte (mUlinePosU); // Master U-line Pos. MSB
                hddrDesc [67] = lsByte (mUlinePosU); // Master U-line Pos. LSB
                hddrDesc [68] = msByte (mUlineDep);  // Master U-line Dep. MSB
                hddrDesc [69] = lsByte (mUlineDep);  // Master U-line Dep. LSB
                hddrDesc [70] = 1;                   // Scaling Tech. = TrueType
                hddrDesc [71] = 0;                   // Variety

                _baseHandler.writeBuffer (cSizeHddrDesc, hddrDesc);

                //------------------------------------------------------------//
                //                                                            //
                // Start calculating checksum byte from byte 64 onwards of    //
                // header.                                                    //
                //                                                            //
                //------------------------------------------------------------//

                sum = 0;

                for (Int32 i = 64; i < cSizeHddrDesc; i++)
                {
                    sum += hddrDesc [i];
                }

                mod256 = (Byte)(sum % 256);

                //------------------------------------------------------------//
                //                                                            //
                // Write header segmented data.                               //
                //                                                            //
                //------------------------------------------------------------//

                flagOK = _baseHandler.writeHddrSegments (false,
                                                         fmt16,
                                                         segGTLast,
                                                         glyphZeroExists,
                                                         symSetUnbound,
                                                         tabvmtxPresent,
                                                         flagVMetrics,
                                                         charCollComp,
                                                         conversionText,
                                                         panoseData,
                                                         ref mod256);

                if (flagOK)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Write 'reserved byte' and (calculated) checksum byte.  //
                    //                                                        //
                    //--------------------------------------------------------//

                    mod256 = (Byte)((256 - mod256) % 256);

                    Byte [] trailer = new Byte [cSizeHddrTrail];

                    trailer [0] = 0;
                    trailer [1] = mod256;

                    _baseHandler.writeBuffer (cSizeHddrTrail, trailer);
                }
            }

            return flagOK;
        }
    }
}
