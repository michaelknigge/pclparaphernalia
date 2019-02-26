using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides common (PCL and PCL XL) handling for the Soft Font
    /// Generate tool.
    /// 
    /// © Chris Hutchinson 2012
    /// 
    /// </summary>

    class ToolSoftFontGenPCLCommon
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private const Int32 cDataBufLen        = 2048;
        private const Int32 cSizeHddrFmt15Max  = 0xffff;

        private const Int32 cSizeSegCC         = 8;
        private const Int32 cSizeSegGC         = 6;
        public  const Int32 cSizeSegGTDirEntry = 16;
        public  const Int32 cSizeSegGTDirHddr  = 12;
        private const Int32 cSizeSegPA         = 10;
        private const Int32 cSizeSegVR         = 4;

        private const Int32 cSizeSegHddrFmt15  = 4;
        private const Int32 cSizeSegHddrFmt16  = 6;
        
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private BinaryWriter _binWriter;
        
        private ToolSoftFontGenTTF _ttfHandler = null;
        
        private ASCIIEncoding _ascii = new ASCIIEncoding ();

        private Byte[] _dataBuf = new Byte[cDataBufLen];

        private ToolSoftFontGenTTFTable _metrics_cvt;
        private ToolSoftFontGenTTFTable _metrics_gdir;
        private ToolSoftFontGenTTFTable _metrics_fpgm;
        private ToolSoftFontGenTTFTable _metrics_head;
        private ToolSoftFontGenTTFTable _metrics_hhea;
        private ToolSoftFontGenTTFTable _metrics_hmtx;
        private ToolSoftFontGenTTFTable _metrics_maxp;
        private ToolSoftFontGenTTFTable _metrics_prep;
        private ToolSoftFontGenTTFTable _metrics_vhea;
        private ToolSoftFontGenTTFTable _metrics_vmtx;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // T o o l S o f t G e n P C L C o m m o n                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public ToolSoftFontGenPCLCommon()
        {
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t H d d r S e g m e n t s L e n                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return overall length of font header.                              //
        //                                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int32 getHddrSegmentsLen (Boolean pdlIsPCLXL,
                                         Boolean fmt16,
                                         Boolean glyphZeroExists,
                                         Boolean symSetUnbound,
                                         Boolean tabvmtxPresent,
                                         Boolean flagVMetrics,
                                         Int32 convTextLen)
        {
            Int32 segmentsLen = 0,
                  numGTTables;

            Int32 segHddrSize;

            Int32 segLenCC = 0,
                  segLenCP = 0,
                  segLenGC = 0,
                  segLenGT = 0,
                  segLenPA = 0,
                  segLenVI = 0,
                  segLenVR = 0,
                  segLenNull = 0;

            Int32 sizeGTTables;

            Int32 sizeGTDirectory;

            if ((pdlIsPCLXL) || (fmt16))
                segHddrSize = cSizeSegHddrFmt16;
            else 
                segHddrSize = cSizeSegHddrFmt15;

            //----------------------------------------------------------------//
            //                                                                //
            // Segment CC.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            segLenCC = segHddrSize + cSizeSegCC;

            //----------------------------------------------------------------//
            //                                                                //
            // Segment CP.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            segLenCP = segHddrSize + convTextLen;

            //----------------------------------------------------------------//
            //                                                                //
            // Segment GC.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            segLenGC = segHddrSize + cSizeSegGC;

            //----------------------------------------------------------------//
            //                                                                //
            // Segment GT.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            numGTTables = _ttfHandler.getOutputNumTables (pdlIsPCLXL,
                                                          symSetUnbound,
                                                          flagVMetrics);

            sizeGTTables = (Int32) _ttfHandler.getSegGTTablesSize (
                                        pdlIsPCLXL,
                                        symSetUnbound,
                                        flagVMetrics);

            sizeGTDirectory = numGTTables *cSizeSegGTDirEntry;

            segLenGT = segHddrSize + cSizeSegGTDirHddr +
                       sizeGTDirectory + sizeGTTables;

            //----------------------------------------------------------------//
            //                                                                //
            // Segment null.                                                  //
            //                                                                //
            //----------------------------------------------------------------//

            segLenNull = segHddrSize;

            //----------------------------------------------------------------//
            //                                                                //
            // Segment PA.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            segLenPA = segHddrSize + ToolSoftFontGenTTF.cSizePanose;

            //----------------------------------------------------------------//
            //                                                                //
            // Segment VI.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            segLenVI = segHddrSize + convTextLen;

            //----------------------------------------------------------------//
            //                                                                //
            // Segment VR.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            segLenVR = segHddrSize + cSizeSegVR;

            //----------------------------------------------------------------//
            //                                                                //
            // Return overall segmented data length.                          //
            //                                                                //
            //----------------------------------------------------------------//

            segmentsLen = segLenPA + segLenGT + segLenNull;

            if (glyphZeroExists)
                segmentsLen += segLenGC;

            if (pdlIsPCLXL)
            {
                segmentsLen += segLenVI;
            }
            else
            {
                segmentsLen += segLenCP;

                if (symSetUnbound)
                    segmentsLen += segLenCC;
            }

            if ((tabvmtxPresent) && (flagVMetrics))
                segmentsLen += segLenVR;

            return segmentsLen;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n i t i a l i s e                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initialise.                                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void initialise(ToolSoftFontGenTTF ttfHandler)
        {
            Boolean flagOK = true;

            _ttfHandler = ttfHandler;

            _ttfHandler.getTableMetrics (ref _metrics_cvt,
                                         ref _metrics_gdir,
                                         ref _metrics_fpgm,
                                         ref _metrics_head,
                                         ref _metrics_hhea,
                                         ref _metrics_hmtx,
                                         ref _metrics_maxp,
                                         ref _metrics_prep,
                                         ref _metrics_vhea,
                                         ref _metrics_vmtx);

            flagOK = _ttfHandler.fontFileReOpen ();

            // if (!flagOK) ... failed to re-open ...
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
        // l s U I n t 3 2                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return low (least-significant) unsigned 32-bit integer from        //
        // supplied unsigned 64-bit integer.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private UInt32 lsUInt32 (UInt64 value)
        {
            return (UInt32)(value & 0x0000ffffffff);
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
        // m s U I n t 3 2                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return high (most-significant) unsigned 32-bit integer from        //
        // supplied unsigned 64-bit integer.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private UInt32 msUInt32 (UInt64 value)
        {
            return (UInt32)((value & 0xffffffff00000000) >> 32);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s t r e a m O p e n                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Create output file via 'Save As' dialogue.                         //
        // Then open target stream and binary writer.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void streamOpen(ref String fontFilename,
                               Boolean pdlIsPCLXL,
                               ref BinaryWriter binWriter,
                               ref Stream opStream)
        {
            String folderName = null;
            String fileName = null;

            SaveFileDialog saveDialog = new SaveFileDialog ();

            ToolCommonFunctions.splitPathName (fontFilename,
                                               ref folderName,
                                               ref fileName);

            saveDialog.InitialDirectory = folderName;
            saveDialog.FileName = fileName;
            saveDialog.CheckFileExists = false;
            saveDialog.OverwritePrompt = true;

            if (pdlIsPCLXL)
            {
                saveDialog.Filter = "PCLXLETTO Font Files | *.sfx";
                saveDialog.DefaultExt = "sfx";
            }
            else
            {
                saveDialog.Filter = "PCLETTO Font Files | *.sft";
                saveDialog.DefaultExt = "sft";
            }

            Nullable<Boolean> dialogResult = saveDialog.ShowDialog ();

            if (dialogResult == true)
            {
                fontFilename = saveDialog.FileName;
            }

            opStream = File.Create (fontFilename);

            if (opStream != null)
            {
                _binWriter = new BinaryWriter (opStream);
                binWriter = _binWriter;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e B u f f e r                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write contents of supplied buffer to output font file.             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void writeBuffer (Int32  bufLen,
                                 Byte[] buffer)
        {
            _binWriter.Write (buffer, 0, bufLen);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e C h a r F r a g m e n t                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write font character fragment to output font file.                 //
        // Calculate and return a modulo-256 checksum of the fragment.        //
        // This is only required for PCL fonts.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void writeCharFragment (Int32 fragLen,
                                       Byte[] fragment,
                                       ref Byte sumMod256)
        {
            UInt32 sum = sumMod256;

            for (int i = 0; i < fragLen; i++)
            {
                sum += fragment[i];
            }

            writeBuffer (fragLen, fragment);

            sumMod256 = (Byte) (sum % 256);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e H d d r F r a g m e n t                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write font header fragment to output font file.                    //
        // If this is a PCL XL font (rather than a PCL font), prefix this     //
        // with the appropriate ReadFontHeader operator and Embedded Data     //
        // introduction.                                                      //
        // Calculate and return a modulo-256 checksum of the fragment; note   //
        // that this is only required for PCL fonts, so don't sum the PCL XL  //
        // headers.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void writeHddrFragment(Boolean pdlIsPCLXL,
                                      Int32 fragLen,
                                      Byte [] fragment,
                                      ref Byte sumMod256)
        {
            UInt32 sum = sumMod256;

            if (pdlIsPCLXL)
            {
                PCLXLWriter.fontHddrRead (_binWriter,
                                          false,
                                          (UInt16) fragLen);

                PCLXLWriter.embedDataIntro (_binWriter,
                                            false,
                                            (UInt16) fragLen);
            }

            for (int i = 0; i < fragLen; i++)
            {
                sum += fragment[i];
            }

            writeBuffer (fragLen, fragment);

            sumMod256 = (Byte) (sum % 256);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e H d d r S e g D a t a C C                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write CC (Character Complement) segment data to output file.       //
        //                                                                    //
        // An 8-byte (64-bit) array:                                          //
        //                                                                    //
        // Bits 2,1,0 (least significant bits) must be set to 1,1,0 (0x06) to //
        //            indicate Unicode indexing.                              //
        // Bits 63->3 individually indicate compatibility of the (unbound)    //
        //            font with specific symbol collections:                  //
        //            0 = compatible 1 = not compatible                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean writeHddrSegDataCC (Boolean pdlIsPCLXL,
                                           Boolean fmt16,
                                           UInt64  charCollComp,
                                           ref Byte sumMod256)
        {
            Boolean flagOK = true;

            Byte [] segId = new Byte [2] { (Byte)'C', (Byte)'C' };

            Byte [] segData = new Byte [cSizeSegCC];

            UInt16 valUInt16;
            UInt32 valUInt32;

            valUInt32 = msUInt32 (charCollComp);
            valUInt16 = msUInt16 (valUInt32);
            segData [0] = msByte (valUInt16);
            segData [1] = lsByte (valUInt16);

            valUInt16 = lsUInt16 (valUInt32);
            segData [2] = msByte (valUInt16);
            segData [3] = lsByte (valUInt16);

            valUInt32 = lsUInt32 (charCollComp);
            valUInt16 = msUInt16 (valUInt32);
            segData [4] = msByte (valUInt16);
            segData [5] = lsByte (valUInt16);

            valUInt16 = lsUInt16 (valUInt32);
            segData [6] = msByte (valUInt16);
            segData [7] = lsByte (valUInt16);

            flagOK = writeHddrSegHddr (pdlIsPCLXL,
                                       fmt16,
                                       (UInt32)cSizeSegCC,
                                       segId,
                                       ref sumMod256);

            if (flagOK)
            {
                writeHddrFragment (pdlIsPCLXL,
                                   cSizeSegCC,
                                   segData,
                                   ref sumMod256);
            }

            return flagOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e H d d r S e g D a t a C P                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write CP (Copyright) segment data to output file.                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean writeHddrSegDataCP (Boolean pdlIsPCLXL,
                                           Boolean fmt16,
                                           Byte [] conversionText,
                                           ref Byte sumMod256)
        {
            Boolean flagOK = true;

            Byte [] segId = new Byte [2] { (Byte)'C', (Byte)'P' };

            Int32 convTextLen = conversionText.Length;

            flagOK = writeHddrSegHddr (pdlIsPCLXL,
                                       fmt16,
                                       (UInt32)convTextLen,
                                       segId,
                                       ref sumMod256);

            if (flagOK)
            {
                writeHddrFragment (pdlIsPCLXL,
                                   convTextLen,
                                   conversionText,
                                   ref sumMod256);
            }

            return flagOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e H d d r S e g D a t a G C                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write (optional) GC (Galley Character) segment data to output file.//
        //                                                                    //
        // Output a default one, which defines a default galley, but not any  //
        // individual region galleys.                                         //
        // Note that this method should only be called if glyph Zero exists   //
        // in the donor TrueType font.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean writeHddrSegDataGC (Boolean pdlIsPCLXL,
                                           Boolean fmt16,
                                           ref Byte sumMod256)
        {
            Boolean flagOK = true;

            Byte [] segId = new Byte [2] { (Byte)'G', (Byte)'C' };

            const UInt16 numRegions = 0;

            Byte [] segData = new Byte [cSizeSegGC];

            segData [0] = 0;
            segData [1] = 0;
            segData [2] = 0xff;
            segData [3] = 0xff;
            segData [4] = msByte (msUInt16 (numRegions));
            segData [5] = lsByte (msUInt16 (numRegions));

            flagOK = writeHddrSegHddr (pdlIsPCLXL,
                                       fmt16,
                                       (UInt32)cSizeSegGC,
                                       segId,
                                       ref sumMod256);

            if (flagOK)
            {
                writeHddrFragment (pdlIsPCLXL,
                                   cSizeSegGC,
                                   segData,
                                   ref sumMod256);
            }

            return flagOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e H d d r S e g D a t a G T                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write GT (Global TrueType) segment data to output file.            //
        //                                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean writeHddrSegDataGT (Boolean pdlIsPCLXL,
                                           Boolean fmt16,
                                           Boolean symSetUnbound,
                                           Boolean tabvmtxPresent, 
                                           Boolean flagVMetrics,
                                           ref Byte sumMod256)
        {
            Boolean flagOK = true;

            Byte[] segId = new Byte[2] { (Byte) 'G', (Byte) 'T' };

            UInt32 segLenGT = 0;

            UInt32 tabLen,
                   sizeTables;

            Int32 numTables;
            UInt32 sizeDirectory;

            UInt32 crntOffset = 0;

            //----------------------------------------------------------------//
            //                                                                //
            // Calculate overall segment length and write segment header.     //
            //                                                                //
            //----------------------------------------------------------------//

            numTables = _ttfHandler.getOutputNumTables (pdlIsPCLXL,
                                                        symSetUnbound,
                                                        flagVMetrics);
            
            sizeTables = _ttfHandler.getSegGTTablesSize (pdlIsPCLXL,
                                                         symSetUnbound,
                                                         flagVMetrics);

            sizeDirectory = (UInt32) (numTables * cSizeSegGTDirEntry);

            segLenGT = cSizeSegGTDirHddr + sizeDirectory + sizeTables;

            flagOK = writeHddrSegHddr (pdlIsPCLXL,
                                       fmt16,
                                       (UInt32) segLenGT,
                                       segId,
                                       ref sumMod256);

            if (flagOK)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Write table directory header, and directory entries for    //
                // the tables which will be written (including the zero-size  //
                // 'gdir' table).                                             //
                //                                                            //
                // If the donor font has vhea and vmtx tables, this indicates //
                // that the font supports vertical rotated characters.        //
                // If we supported these tables (valid in GT segment for both //
                // PCL and PCL XL fonts), we'd also need to consider writing  //
                // segments VE, VR and VT (but perhaps only for PCL XL).      //
                // Difficulty would be in working out the contents of those   //
                // segments (especially VE and VT), as there doesn't appear   //
                // to be any definitive guidance for this.                    //
                //                                                            //
                //------------------------------------------------------------//

                writeHddrSegDataGTDirHddr (pdlIsPCLXL,
                                          (UInt16)numTables,
                                          ref sumMod256);

                crntOffset = (UInt32)(cSizeSegGTDirHddr + sizeDirectory);

                //------------------------------------------------------------//
                //                                                            //
                // cvt                                                        //
                //                                                            //
                //------------------------------------------------------------//

                tabLen = _metrics_cvt.TableLength;

                if (tabLen != 0)
                {
                    writeHddrSegDataGTDirEntry (pdlIsPCLXL,
                                                _metrics_cvt.TableTag,
                                                tabLen,
                                                _metrics_cvt.TableChecksum,
                                                crntOffset,
                                                ref sumMod256);

                    crntOffset += _metrics_cvt.TablePadLen;
                }

                //------------------------------------------------------------//
                //                                                            //
                // fpgm                                                       //
                //                                                            //
                //------------------------------------------------------------//

                tabLen = _metrics_fpgm.TableLength;

                if (tabLen != 0)
                {
                    writeHddrSegDataGTDirEntry (pdlIsPCLXL,
                                                _metrics_fpgm.TableTag,
                                                tabLen,
                                                _metrics_fpgm.TableChecksum,
                                                crntOffset,
                                                ref sumMod256);

                    crntOffset += _metrics_fpgm.TablePadLen;
                }

                //------------------------------------------------------------//
                //                                                            //
                // gdir                                                       //
                // empty table                                                //
                //                                                            //
                //------------------------------------------------------------//

                writeHddrSegDataGTDirEntry (pdlIsPCLXL,
                                            _metrics_gdir.TableTag,
                                            0,
                                            0,
                                            0,
                                            ref sumMod256);

                //------------------------------------------------------------//
                //                                                            //
                // head                                                       //
                //                                                            //
                //------------------------------------------------------------//

                writeHddrSegDataGTDirEntry (pdlIsPCLXL,
                                            _metrics_head.TableTag,
                                            _metrics_head.TableLength,
                                            _metrics_head.TableChecksum,
                                            crntOffset,
                                            ref sumMod256);

                crntOffset += _metrics_head.TablePadLen;

                //------------------------------------------------------------//
                //                                                            //
                // hhea                                                       //
                //                                                            //
                //------------------------------------------------------------//

                if ((!pdlIsPCLXL) || (symSetUnbound))
                {
                    writeHddrSegDataGTDirEntry (pdlIsPCLXL,
                                                _metrics_hhea.TableTag,
                                                _metrics_hhea.TableLength,
                                                _metrics_hhea.TableChecksum,
                                                crntOffset,
                                                ref sumMod256);

                    crntOffset += _metrics_hhea.TablePadLen;
                }

                //------------------------------------------------------------//
                //                                                            //
                // hmtx                                                       //
                //                                                            //
                //------------------------------------------------------------//

                if ((!pdlIsPCLXL) || (symSetUnbound))
                {
                    writeHddrSegDataGTDirEntry (pdlIsPCLXL,
                                                _metrics_hmtx.TableTag,
                                                _metrics_hmtx.TableLength,
                                                _metrics_hmtx.TableChecksum,
                                                crntOffset,
                                                ref sumMod256);

                    crntOffset += _metrics_hmtx.TablePadLen;
                }

                //------------------------------------------------------------//
                //                                                            //
                // maxp                                                       //
                //                                                            //
                //------------------------------------------------------------//

                writeHddrSegDataGTDirEntry (pdlIsPCLXL,
                                            _metrics_maxp.TableTag,
                                            _metrics_maxp.TableLength,
                                            _metrics_maxp.TableChecksum,
                                            crntOffset,
                                            ref sumMod256);

                crntOffset += _metrics_maxp.TablePadLen;

                //------------------------------------------------------------//
                //                                                            //
                // prep                                                       //
                //                                                            //
                //------------------------------------------------------------//

                tabLen = _metrics_prep.TableLength;

                if (tabLen != 0)
                {
                    writeHddrSegDataGTDirEntry (pdlIsPCLXL,
                                                _metrics_prep.TableTag,
                                                tabLen,
                                                _metrics_prep.TableChecksum,
                                                crntOffset,
                                                ref sumMod256);

                    crntOffset += _metrics_prep.TablePadLen;
                }

                if ((tabvmtxPresent) && (flagVMetrics))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // vhea                                                   //
                    // Support for vertical rotated characters                //
                    //                                                        //
                    //--------------------------------------------------------//

                    tabLen = _metrics_vhea.TableLength;

                    if (tabLen != 0)
                    {
                        if ((!pdlIsPCLXL) || (symSetUnbound))
                        {
                            writeHddrSegDataGTDirEntry (
                                pdlIsPCLXL,
                                _metrics_vhea.TableTag,
                                tabLen,
                                _metrics_vhea.TableChecksum,
                                crntOffset,
                                ref sumMod256);

                            crntOffset += _metrics_vhea.TablePadLen;
                        }
                    }

                    //--------------------------------------------------------//
                    //                                                        //
                    // vmtx                                                   //
                    // Support for vertical rotated characters                //
                    //                                                        //
                    //--------------------------------------------------------//

                    tabLen = _metrics_vmtx.TableLength;

                    if (tabLen != 0)
                    {
                        if ((!pdlIsPCLXL) || (symSetUnbound))
                        {
                            writeHddrSegDataGTDirEntry (
                                pdlIsPCLXL,
                                _metrics_vmtx.TableTag,
                                tabLen,
                                _metrics_vmtx.TableChecksum,
                                crntOffset,
                                ref sumMod256);

                            crntOffset += _metrics_vmtx.TablePadLen;
                        }
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Write the actual tables; these follow the end of the table //
                // directory.                                                 //
                //                                                            //
                //------------------------------------------------------------//

                //------------------------------------------------------------//
                //                                                            //
                // cvt                                                        //
                //                                                            //
                //------------------------------------------------------------//
                
                tabLen = _metrics_cvt.TableLength;

                if (tabLen != 0)
                {
                    writeHddrSegDataGTTableData (pdlIsPCLXL,
                                                tabLen,
                                                _metrics_cvt.TableOffset,
                                                _metrics_cvt.TablePadBytes,
                                                ref sumMod256);
                }

                //------------------------------------------------------------//
                //                                                            //
                // fpgm                                                       //
                //                                                            //
                //------------------------------------------------------------//

                tabLen = _metrics_fpgm.TableLength;

                if (tabLen != 0)
                {
                    writeHddrSegDataGTTableData (pdlIsPCLXL,
                                                tabLen,
                                                _metrics_fpgm.TableOffset,
                                                _metrics_fpgm.TablePadBytes,
                                                ref sumMod256);
                }

                //------------------------------------------------------------//
                //                                                            //
                // head                                                       //
                //                                                            //
                //------------------------------------------------------------//

                writeHddrSegDataGTTableData (pdlIsPCLXL,
                                            _metrics_head.TableLength,
                                            _metrics_head.TableOffset,
                                            _metrics_head.TablePadBytes,
                                            ref sumMod256);

                //------------------------------------------------------------//
                //                                                            //
                // hhea                                                       //
                //                                                            //
                //------------------------------------------------------------//

                if ((!pdlIsPCLXL) || (symSetUnbound))
                {
                    writeHddrSegDataGTTableData (pdlIsPCLXL,
                                                _metrics_hhea.TableLength,
                                                _metrics_hhea.TableOffset,
                                                _metrics_hhea.TablePadBytes,
                                                ref sumMod256);
                }

                //------------------------------------------------------------//
                //                                                            //
                // hmtx                                                       //
                //                                                            //
                //------------------------------------------------------------//

                if ((!pdlIsPCLXL) || (symSetUnbound))
                {
                    writeHddrSegDataGTTableData (pdlIsPCLXL,
                                                _metrics_hmtx.TableLength,
                                                _metrics_hmtx.TableOffset,
                                                _metrics_hmtx.TablePadBytes,
                                                ref sumMod256);
                }

                //------------------------------------------------------------//
                //                                                            //
                // maxp                                                       //
                //                                                            //
                //------------------------------------------------------------//

                writeHddrSegDataGTTableData (pdlIsPCLXL,
                                            _metrics_maxp.TableLength,
                                            _metrics_maxp.TableOffset,
                                            _metrics_maxp.TablePadBytes,
                                            ref sumMod256);

                //------------------------------------------------------------//
                //                                                            //
                // prep                                                       //
                //                                                            //
                //------------------------------------------------------------//

                tabLen = _metrics_prep.TableLength;

                if (tabLen != 0)
                {
                    writeHddrSegDataGTTableData (pdlIsPCLXL,
                                                tabLen,
                                                _metrics_prep.TableOffset,
                                                _metrics_prep.TablePadBytes,
                                                ref sumMod256);
                }

                if ((tabvmtxPresent) && (flagVMetrics))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // vhea                                                   //
                    // Support for vertical rotated characters                //
                    //                                                        //
                    //--------------------------------------------------------//

                    tabLen = _metrics_vhea.TableLength;

                    if (tabLen != 0)
                    {
                        if ((!pdlIsPCLXL) || (symSetUnbound))
                        {
                            writeHddrSegDataGTTableData (
                                pdlIsPCLXL,
                                tabLen,
                                _metrics_vhea.TableOffset,
                                _metrics_vhea.TablePadBytes,
                                ref sumMod256);
                        }
                    }

                    //--------------------------------------------------------//
                    //                                                        //
                    // vmtx                                                   //
                    // Support for vertical rotated characters                //
                    //                                                        //
                    //--------------------------------------------------------//

                    tabLen = _metrics_vmtx.TableLength;

                    if (tabLen != 0)
                    {
                        if ((!pdlIsPCLXL) || (symSetUnbound))
                        {
                            writeHddrSegDataGTTableData (
                                pdlIsPCLXL,
                                tabLen,
                                _metrics_vmtx.TableOffset,
                                _metrics_vmtx.TablePadBytes,
                                ref sumMod256);
                        }
                    }
                }
            }

            return flagOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e H d d r S e g D a t a G T D i r E n t r y                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write GT segment table directory entry to output file.             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void writeHddrSegDataGTDirEntry(Boolean pdlIsPCLXL,
                                               UInt32  tabTag,
                                               UInt32  tabLen,
                                               UInt32  tabChk,
                                               UInt32 crntOffset,
                                               ref Byte sumMod256)
        {
            Byte[] indxEntry = new Byte[cSizeSegGTDirEntry];

            indxEntry[0] = msByte (msUInt16 (tabTag));
            indxEntry[1] = lsByte (msUInt16 (tabTag));
            indxEntry[2] = msByte (lsUInt16 (tabTag));
            indxEntry[3] = lsByte (lsUInt16 (tabTag));
            indxEntry[4] = msByte (msUInt16 (tabChk));
            indxEntry[5] = lsByte (msUInt16 (tabChk));
            indxEntry[6] = msByte (lsUInt16 (tabChk));
            indxEntry[7] = lsByte (lsUInt16 (tabChk));
            indxEntry[8] = msByte (msUInt16 (crntOffset));
            indxEntry[9] = lsByte (msUInt16 (crntOffset));
            indxEntry[10] = msByte (lsUInt16 (crntOffset));
            indxEntry[11] = lsByte (lsUInt16 (crntOffset));
            indxEntry[12] = msByte (msUInt16 (tabLen));
            indxEntry[13] = lsByte (msUInt16 (tabLen));
            indxEntry[14] = msByte (lsUInt16 (tabLen));
            indxEntry[15] = lsByte (lsUInt16 (tabLen));

            writeHddrFragment (pdlIsPCLXL,
                               cSizeSegGTDirEntry,
                               indxEntry,
                               ref sumMod256);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e H d d r S e g D a t a G T D i r H d d r                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write GT segment table directory header to output file.            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void writeHddrSegDataGTDirHddr(Boolean pdlIsPCLXL,
                                              UInt16 numTables,
                                              ref Byte sumMod256)
        {
            Int16 powerN,
                  twoToPowerN;

            UInt16 entrySelector,
                   rangeShift,
                   searchRange;

            Byte[] indxHddr = new Byte[cSizeSegGTDirHddr];

            powerN = 0;
            twoToPowerN = 1;

            while ((twoToPowerN * 2) <= numTables)
            {
                powerN = (Int16) (powerN + 1);
                twoToPowerN = (Int16) (twoToPowerN * 2);
            }

            entrySelector = (UInt16) powerN;
            searchRange = (UInt16) (twoToPowerN * cSizeSegGTDirEntry);
            rangeShift = (UInt16) ((numTables * cSizeSegGTDirEntry)
                                   - searchRange);


            indxHddr[0] = 0;
            indxHddr[1] = 1;
            indxHddr[2] = 0;
            indxHddr[3] = 0;
            indxHddr[4] = msByte (numTables);
            indxHddr[5] = lsByte (numTables);
            indxHddr[6] = msByte (searchRange);
            indxHddr[7] = lsByte (searchRange);
            indxHddr[8] = msByte (entrySelector);
            indxHddr[9] = lsByte (entrySelector);
            indxHddr[10] = msByte (rangeShift);
            indxHddr[11] = lsByte (rangeShift);

            writeHddrFragment (pdlIsPCLXL,
                               cSizeSegGTDirHddr,
                               indxHddr,
                               ref sumMod256);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e H d d r S e g D a t a G T T a b l e D a t a              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write GT segment table data to output file.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void writeHddrSegDataGTTableData(Boolean pdlIsPCLXL,
                                                UInt32  tabLength,
                                                UInt32  tabOffset,
                                                Int32  padBytes,
                                                ref Byte sumMod256)
        {
            Int32 readLen,
                   readRem,
                   readStart;

            readRem   = (Int32) tabLength;
            readStart = (Int32) tabOffset;

            while (readRem > 0)
            {
                if (readRem > cDataBufLen)
                {
                    readLen = cDataBufLen;
                    readRem = readRem - cDataBufLen;
                }
                else
                {
                    readLen = readRem;
                    readRem = 0;
                }

                _ttfHandler.readByteArray (readStart,
                                           readLen,
                                           ref _dataBuf);

                writeHddrFragment (pdlIsPCLXL,
                                   readLen,
                                   _dataBuf,
                                   ref sumMod256);

                readStart = readStart + cDataBufLen;
            }

            if (padBytes > 0)
            {
                _dataBuf[0] = 0x00;
                _dataBuf[1] = 0x00;
                _dataBuf[2] = 0x00;
                _dataBuf[3] = 0x00;

                writeHddrFragment (pdlIsPCLXL,
                                   padBytes,
                                   _dataBuf,
                                   ref sumMod256);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e H d d r S e g D a t a N u l l                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write Null segment data to output file.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean writeHddrSegDataNull (Boolean pdlIsPCLXL,
                                             Boolean fmt16,
                                             ref Byte sumMod256)
        {
            Boolean flagOK;

            Byte[] segId = new Byte[2] { 0xff, 0xff };

            flagOK = writeHddrSegHddr (pdlIsPCLXL,
                                       fmt16,
                                       0,
                                       segId,
                                       ref sumMod256);

            return flagOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e H d d r S e g D a t a P A                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write PA (Panose) segment data to output file.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean writeHddrSegDataPA (Boolean pdlIsPCLXL,
                                           Boolean fmt16,
                                           Byte [] panoseData,
                                           ref Byte sumMod256)
        {
            Boolean flagOK = true;

            Byte [] segId = new Byte [2] { (Byte)'P', (Byte)'A' };

            Byte [] segData = new Byte [cSizeSegPA];

            segData [0] = panoseData [0];
            segData [1] = panoseData [1];
            segData [2] = panoseData [2];
            segData [3] = panoseData [3];
            segData [4] = panoseData [4];
            segData [5] = panoseData [5];
            segData [6] = panoseData [6];
            segData [7] = panoseData [7];
            segData [8] = panoseData [8];
            segData [9] = panoseData [9];

            flagOK = writeHddrSegHddr (pdlIsPCLXL,
                                       fmt16,
                                       (UInt32)cSizeSegPA,
                                       segId,
                                       ref sumMod256);

            if (flagOK)
            {
                writeHddrFragment (pdlIsPCLXL,
                                   cSizeSegPA,
                                   segData,
                                   ref sumMod256);
            }

            return flagOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e H d d r S e g D a t a V I                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write VI segment data to output file.                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean writeHddrSegDataVI (Boolean pdlIsPCLXL,
                                           Boolean fmt16,
                                           Byte [] conversionText,
                                           ref Byte sumMod256)
        {
            Boolean flagOK = true;

            Byte [] segId = new Byte [2] { (Byte)'V', (Byte)'I' };

            Int32 convTextLen = conversionText.Length;

            flagOK = writeHddrSegHddr (pdlIsPCLXL,
                                       fmt16,
                                       (UInt32)convTextLen,
                                       segId,
                                       ref sumMod256);

            if (flagOK)
            {
                writeHddrFragment (pdlIsPCLXL,
                                   convTextLen,
                                   conversionText,
                                   ref sumMod256);
            }

            return flagOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e H d d r S e g D a t a V R                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write VR segment data to output file.                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean writeHddrSegDataVR (Boolean pdlIsPCLXL,
                                           Boolean fmt16,
                                           ref Byte sumMod256)
        {
            Boolean flagOK = true;

            UInt16 vDescender;

            Byte [] segId = new Byte [2] { (Byte)'V', (Byte)'R' };

            Byte [] segData = new Byte [cSizeSegVR];

            vDescender = (UInt16) _ttfHandler.getOS2sTypoDescender ();

            segData [0] = 0;                    // format MSB
            segData [1] = 0;                    // format LSB
            segData [2] = msByte (vDescender);  // sTypoDescender MSB
            segData [3] = lsByte (vDescender);  // sTypoDescender LSB

            flagOK = writeHddrSegHddr (pdlIsPCLXL,
                                       fmt16,
                                       (UInt32) cSizeSegVR,
                                       segId,
                                       ref sumMod256);

            if (flagOK)
            {
                writeHddrFragment (pdlIsPCLXL,
                                   cSizeSegVR,
                                   segData,
                                   ref sumMod256);
            }

            return flagOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e H d d r S e g H d d r                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write segment header to output file.                               //
        // PCL      Format 16   size fields are 4 bytes;                      //
        // PCL      Format 15   size fields are 2 bytes.                      //
        // PCL XL               size fields are 4 bytes; same as PCL Fmt 16.  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean writeHddrSegHddr (Boolean pdlIsPCLXL,
                                         Boolean fmt16,
                                         UInt32 segDataLen,
                                         Byte [] segId,
                                         ref Byte sumMod256)
        {
            Boolean flagOK = true;

            if ((pdlIsPCLXL) || (fmt16))
            {
                Byte[] segHddrFmt16 = new Byte[cSizeSegHddrFmt16];

                segHddrFmt16[0] = segId[0];
                segHddrFmt16[1] = segId[1];
                segHddrFmt16[2] = msByte (msUInt16 (segDataLen));
                segHddrFmt16[3] = lsByte (msUInt16 (segDataLen));
                segHddrFmt16[4] = msByte (lsUInt16 (segDataLen));
                segHddrFmt16[5] = lsByte (lsUInt16 (segDataLen));

                writeHddrFragment (pdlIsPCLXL,
                                   cSizeSegHddrFmt16,
                                   segHddrFmt16,
                                   ref sumMod256);
            }
            else if (segDataLen > cSizeHddrFmt15Max)
            {
                flagOK = false;

                MessageBox.Show ("Header segment length of '" + segDataLen +
                                 "' is incompatible with 'format 15'" +
                                 " font.",
                                 "Soft font header invalid",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);

            }
            else
            {
                Byte [] segHddrFmt15 = new Byte [cSizeSegHddrFmt15];

                segHddrFmt15 [0] = segId [0];
                segHddrFmt15 [1] = segId [1];
                segHddrFmt15 [2] = msByte (lsUInt16 (segDataLen));
                segHddrFmt15 [3] = lsByte (lsUInt16 (segDataLen));

                writeHddrFragment (pdlIsPCLXL,
                                   cSizeSegHddrFmt15,
                                   segHddrFmt15,
                                   ref sumMod256);
            }

            return flagOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e H d d r S e g m e n t s                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write segmented data to output file.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean writeHddrSegments (Boolean pdlIsPCLXL,
                                          Boolean fmt16,
                                          Boolean segGTLast,
                                          Boolean glyphZeroExists,
                                          Boolean symSetUnbound,
                                          Boolean tabvmtxPresent,
                                          Boolean flagVMetrics,
                                          UInt64  charCollComp,
                                          Byte [] conversionText,
                                          Byte [] panoseData,
                                          ref Byte sumMod256)
        {
            Boolean flagOK = true;

            if (!pdlIsPCLXL)
            {
                if (symSetUnbound)
                {
                    flagOK = writeHddrSegDataCC (pdlIsPCLXL,
                                                 fmt16,
                                                 charCollComp,
                                                 ref sumMod256);
                }

                if (flagOK)

                    flagOK = writeHddrSegDataPA (pdlIsPCLXL, fmt16,
                                                 panoseData,
                                                 ref sumMod256);
            }

            if ((flagOK) && (!segGTLast))
                flagOK = writeHddrSegDataGT (pdlIsPCLXL, fmt16,
                                             symSetUnbound,
                                             tabvmtxPresent,
                                             flagVMetrics,
                                             ref sumMod256);

            if (flagOK)
            {
                if (glyphZeroExists)
                    flagOK = writeHddrSegDataGC (pdlIsPCLXL, fmt16,
                                                 ref sumMod256);

                if (flagOK)
                {
                    if (pdlIsPCLXL)
                        flagOK = writeHddrSegDataVI (pdlIsPCLXL, fmt16,
                                                     conversionText,
                                                     ref sumMod256);
                    else
                        flagOK = writeHddrSegDataCP (pdlIsPCLXL, fmt16,
                                                     conversionText,
                                                     ref sumMod256);
                }
            }

            if (flagOK)
            {
                if ((tabvmtxPresent) && (flagVMetrics))
                {
                    flagOK = writeHddrSegDataVR (pdlIsPCLXL, fmt16,
                                                 ref sumMod256);
                }
            }

            if ((flagOK) && (segGTLast))
                flagOK = writeHddrSegDataGT(pdlIsPCLXL, fmt16,
                                             symSetUnbound,
                                             tabvmtxPresent,
                                             flagVMetrics,
                                             ref sumMod256);

            if (flagOK)
                flagOK = writeHddrSegDataNull (pdlIsPCLXL, fmt16,
                                               ref sumMod256);

            return flagOK;
        }
    }
}
