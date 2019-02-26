using System;
using System.Data;
using System.Text;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines functions to parse the binary data associated with various
    /// PCL escape sequences.
    /// 
    /// © Chris Hutchinson 2013
    /// 
    /// </summary>

    class PrnParsePCLBinary
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

        PrnParseFontHddrPCL _parseFontHddrPCL;

        private ASCIIEncoding _ascii = new ASCIIEncoding();

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P r n P a r s e P C L B i n a r y                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PrnParsePCLBinary(PrnParseFontHddrPCL parseFontHddrPCL)
        {
            _parseFontHddrPCL = parseFontHddrPCL;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e A l p h a N u m e r i c I D                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the associated data part of the AlphaNumeric ID command //
        // Format is <esc>&n#W[op-byte][string-data]                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean decodeAlphaNumericID(
            Int32 binDataLen,
            Int32 fileOffset,
            Byte[] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            PrnParseLinkData linkData,
            PrnParseOptions options,
            DataTable table)
        {
            const Int32 lenMin = 1;
            const Int32 lenMax = 512;   // must be less than buffer size 

            PrnParseConstants.eContType contType;

            Byte opCode;

            Int32 binDataRem;

            Boolean dataOK,
                    dummyBool = false;

            //----------------------------------------------------------------//
            //                                                                //
            // Initialise.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            dataOK = true;

            if ((binDataLen < lenMin) || (binDataLen > lenMax))
            {
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.MsgWarning,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "*** Warning ***",
                    "",
                    "AlphaNumeric Id Data length < " + lenMin +
                    " or > " + lenMax);

                dataOK = false;
                contType = PrnParseConstants.eContType.PCLDownload;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataLen,
                                      true,
                                      0x00,
                                      0x00);
            }
            else if (binDataLen > bufRem)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Not all of binary data is in current buffer.               //
                // Initiate continuation action.                              //
                //                                                            //
                //------------------------------------------------------------//

                contType = PrnParseConstants.eContType.PCLAlphaNumericID;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataLen,
                                      true,
                                      0x00,
                                      0x00);
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // interpret data.                                            //
                //                                                            //
                //------------------------------------------------------------//

                String codeVal,
                       codeDesc;

                Int32 analysisLevel;

                Boolean showBinData;

                PrnParseConstants.eOptOffsetFormats indxOffsetFormat;

                PrnParseConstants.eOptCharSetSubActs indxCharSetSubAct =
                    PrnParseConstants.eOptCharSetSubActs.Hex;
                PrnParseConstants.eOptCharSets indxCharSetName =
                    PrnParseConstants.eOptCharSets.ASCII;
                Int32 valCharSetSubCode = 0x20;

                analysisLevel = linkData.AnalysisLevel;

                indxOffsetFormat = options.IndxGenOffsetFormat;
                showBinData = options.FlagPCLMiscBinData;

                analysisLevel = linkData.AnalysisLevel;

                indxOffsetFormat = options.IndxGenOffsetFormat;
                showBinData = options.FlagPCLMiscBinData;

                options.getOptCharSet(ref indxCharSetName,
                                       ref indxCharSetSubAct,
                                       ref valCharSetSubCode);

                PrnParseCommon.addDataRow (
                    PrnParseRowTypes.eType.DataBinary,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    indxOffsetFormat,
                    fileOffset + bufOffset,
                    analysisLevel,
                    "PCL Binary",
                    "[ " + binDataLen + " bytes ]",
                    "Alphanumeric ID data");

                if (showBinData)
                {
                    PrnParseData.processBinary(
                        table,
                        PrnParseConstants.eOvlShow.None,
                        buf,
                        fileOffset,
                        bufOffset,
                        binDataLen,
                        "",
                        showBinData,
                        false,
                        true,
                        indxOffsetFormat,
                        analysisLevel);
                }

                //------------------------------------------------------------//
                //                                                            //
                // Operation code byte.                                       //
                //                                                            //
                //------------------------------------------------------------//

                opCode = buf[bufOffset];

                switch (opCode)
                {
                    case 0:
                        codeVal = "0 (0x00 = <NUL>): ";
                        codeDesc = "Set current Font ID to String ID";
                        break;

                    case 1:
                        codeVal = "1 (0x01 = <SOH>): ";
                        codeDesc = "Associate current Font ID with String ID";
                        break;

                    case 2:
                        codeVal = "2 (0x02 = <STX>): ";
                        codeDesc = "Select font with String ID as primary";
                        break;

                    case 3:
                        codeVal = "3 (0x03 = <ETX>): ";
                        codeDesc = "Select font with String ID as secondary";
                        break;

                    case 4:
                        codeVal = "4 (0x04 = <EOT>): ";
                        codeDesc = "Set current Macro ID to String ID";
                        break;

                    case 5:
                        codeVal = "5 (0x05 = <ENQ>): ";
                        codeDesc = "Associate current Macro ID with String ID";
                        break;

                    case 20:
                        codeVal = "20 (0x20 = <DC4>): ";
                        codeDesc = "Delete font association named by current Font ID";
                        break;

                    case 21:
                        codeVal = "21 (0x21 = <NAK>): ";
                        codeDesc = "Delete macro association named by current Font ID";
                        break;

                    case 100:
                        codeVal = "100 (0x64 = 'd'): ";
                        codeDesc = "Media select";
                        break;

                    default:
                        codeVal = opCode.ToString() + " (0x" +
                                   opCode.ToString("x2") + "): ";
                        codeDesc = "unknown";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Operation Code",
                    "",
                    codeVal + codeDesc);

                //------------------------------------------------------------//
                //                                                            //
                // String identifier (not for all operation codes).           //
                //                                                            //
                //------------------------------------------------------------//

                binDataRem = binDataLen - 1;
                bufRem = bufRem - 1;
                bufOffset = bufOffset + 1;

                if (binDataRem > 0)
                {
                    PrnParseData.processLines(
                    table,
                    PrnParseConstants.eOvlShow.None,
                    linkData,
                    ToolCommonData.ePrintLang.Unknown,
                    buf,
                    fileOffset,
                    binDataRem,
                    ref bufRem,
                    ref bufOffset,
                    ref dummyBool,
                    false,
                    false,
                    false,
                    PrnParseConstants.asciiEsc,
                    "String ID",
                    0,
                    indxCharSetSubAct,
                    (Byte)valCharSetSubCode,
                    indxCharSetName,
                    indxOffsetFormat,
                    analysisLevel);
                }
            }

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e C o l o u r L o o k u p                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the associated data part of the Colour Lookup Tables    //
        // command.                                                           //
        // Format is <esc>*l#W[binary]                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean decodeColourLookup(
            Int32 binDataLen,
            Int32 fileOffset,
            Byte[] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            PrnParseLinkData linkData,
            PrnParseOptions options,
            DataTable table)
        {
            const Int32 lenStd = 770;   // must be less than buffer size 

            PrnParseConstants.eContType contType;

            Int32 binDataRem;

            Boolean dataOK;

            //----------------------------------------------------------------//
            //                                                                //
            // Initialise.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            dataOK = true;

            binDataRem = binDataLen;

            if (binDataLen != lenStd)
            {
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.MsgWarning,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "*** Warning ***",
                    "",
                    "Colour Lookup Tables length != " + lenStd);

                dataOK = false;
                contType = PrnParseConstants.eContType.PCLDownload;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataRem,
                                      true,
                                      0x00,
                                      0x00);
            }
            else if (binDataRem > bufRem)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Not all of binary data is in current buffer.               //
                // Initiate continuation action.                              //
                //                                                            //
                //------------------------------------------------------------//

                contType = PrnParseConstants.eContType.PCLColourLookup;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataRem,
                                      true,
                                      0x00,
                                      0x00);
            }
            else 
            {
                //------------------------------------------------------------//
                //                                                            //
                // The format:                                                //
                //  byte    0       colour space                              //
                //  byte    1       reserved                                  //
                //  bytes   2 - 257 colour component 1 data (256 bytes)       //
                //  bytes 258 - 513 colour component 2 data (256 bytes)       //
                //  bytes 514 - 769 colour component 3 data (256 bytes)       //
                //                                                            //
                //------------------------------------------------------------//

                const Int32 compCt  = 3;
                const Int32 mapSize = 256;  // multiple of lineLen //
                const Int32 lineLen = 16;
                
                Int32 offset,
                      lineBegin,
                      lineEnd;

                Byte byteVal,
                     colourSpace;

                String codeDesc,
                       textDesc;

                Int32 analysisLevel;

                Boolean showBinData;

                PrnParseConstants.eOptOffsetFormats indxOffsetFormat;

                analysisLevel = linkData.AnalysisLevel;
                showBinData = options.FlagPCLMiscBinData;

                indxOffsetFormat = options.IndxGenOffsetFormat;

                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    PrnParseRowTypes.eType.DataBinary,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    indxOffsetFormat,
                    fileOffset + bufOffset,
                    analysisLevel,
                    "PCL Binary",
                    "[ " + lenStd + " bytes ]",
                    "Colour Lookup Tables");

                if (showBinData)
                {
                    PrnParseData.processBinary (
                        table,
                        PrnParseConstants.eOvlShow.None,
                        buf,
                        fileOffset,
                        bufOffset,
                        lenStd,
                        "",
                        showBinData,
                        false,
                        true,
                        indxOffsetFormat,
                        analysisLevel);
                }

                //------------------------------------------------------------//

                offset = bufOffset;

                colourSpace = buf [offset];

                switch (colourSpace)
                {
                    case 0:
                        codeDesc = "0: Device RGB";
                        break;

                    case 1:
                        codeDesc = "1: Device CMY";
                        break;

                    case 2:
                        codeDesc = "2: Colorimetric RGB Spaces";
                        break;

                    case 3:
                        codeDesc = "3: CIE L*a*b*";
                        break;

                    case 4:
                        codeDesc = "4: Luminance-Chrominance Spaces";
                        break;

                    default:
                        codeDesc = colourSpace.ToString () + ": unknown";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Colour Space",
                    "",
                    codeDesc);

                //------------------------------------------------------------//

                offset++;

                byteVal = buf [offset];

                switch (byteVal)
                {
                    case 0:
                        codeDesc = "0";
                        break;

                    default:
                        codeDesc = byteVal.ToString () + ": should be zero";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Reserved Byte",
                    "",
                    codeDesc);

                //------------------------------------------------------------//

                offset++;

                for (Int32 i = 0; i < compCt; i++)
                {
                    lineBegin = 0;

                    for (Int32 j = 0; j < mapSize; j += lineLen)
                    {
                        lineEnd = lineBegin + (lineLen - 1);

                        textDesc = "0x" + lineBegin.ToString ("x2") +
                                   "->" + lineEnd.ToString ("x2");

                        codeDesc = PrnParseCommon.byteArrayToHexString (
                                                    buf,
                                                    offset + j,
                                                    lineLen);

                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLDecode,
                            table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "Component " + i.ToString (),
                            textDesc,
                            "{ " + codeDesc + "]");

                        lineBegin += lineLen;
                    }

                    offset += mapSize;
                }

                //------------------------------------------------------------//

                bufRem = bufRem - lenStd;
                bufOffset = bufOffset + lenStd;
            }

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e C o n f i g u r a t i o n I O                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the associated data part of the Configuration (I/O)     //
        // command.                                                           //
        // Format is <esc>&b#W[key]<space>[value]                             //
        // Both the key and the value can (in theory) be up to 32765 bytes in //
        // length, which would make it difficult to process because of our    //
        // 'read-block' limit (2048), so we only handle the case where the    //
        // total data is less than a block.                                   //
        // In practice, the known keys are all less than 7 bytes in length,   //
        // and the values are probably unlikely to exceed a hundred or so     //
        // bytes in length.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean decodeConfigurationIO(
            Int32 binDataLen,
            Int32 fileOffset,
            Byte[] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            PrnParseLinkData linkData,
            PrnParseOptions options,
            DataTable table)
        {
            const Int32 lenMin = 5;
            const Int32 lenMax = 2047;   // must be less than buffer size 

            PrnParseConstants.eContType contType;

            Int32 binDataRem;

            Boolean dataOK;

            //----------------------------------------------------------------//
            //                                                                //
            // Initialise.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            dataOK = true;

            binDataRem = binDataLen;

            if ((binDataLen < lenMin) || (binDataLen > lenMax))
            {
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.MsgWarning,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "*** Warning ***",
                    "",
                    "Configuration (I/O) length < " + lenMin +
                    " or > " + lenMax);

                dataOK = false;
                contType = PrnParseConstants.eContType.PCLDownload;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataRem,
                                      true,
                                      0x00,
                                      0x00);
            }
            else if (binDataRem > bufRem)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Not all of binary data is in current buffer.               //
                // Initiate continuation action.                              //
                //                                                            //
                //------------------------------------------------------------//

                contType = PrnParseConstants.eContType.PCLConfigurationIO;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataRem,
                                      true,
                                      0x00,
                                      0x00);
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // Find the key first, by looking for the space separator.    //
                //                                                            //
                //------------------------------------------------------------//

                Int32 lenKey = -1,
                      lenVal = -1,
                      dataStart;
                
                Boolean dummyBool = false;

                Int32 analysisLevel;

                Boolean showBinData;

                PrnParseConstants.eOptOffsetFormats indxOffsetFormat;

                PrnParseConstants.eOptCharSetSubActs indxCharSetSubAct =
                    PrnParseConstants.eOptCharSetSubActs.Hex;
                PrnParseConstants.eOptCharSets indxCharSetName =
                    PrnParseConstants.eOptCharSets.ASCII;
                Int32 valCharSetSubCode = 0x20;

                analysisLevel = linkData.AnalysisLevel;

                indxOffsetFormat = options.IndxGenOffsetFormat;
                showBinData = options.FlagPCLMiscBinData;

                analysisLevel = linkData.AnalysisLevel;

                indxOffsetFormat = options.IndxGenOffsetFormat;
                showBinData = options.FlagPCLMiscBinData;

                options.getOptCharSet(ref indxCharSetName,
                                       ref indxCharSetSubAct,
                                       ref valCharSetSubCode);

                PrnParseCommon.addDataRow (
                    PrnParseRowTypes.eType.DataBinary,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    indxOffsetFormat,
                    fileOffset + bufOffset,
                    analysisLevel,
                    "PCL Binary",
                    "[ " + binDataLen + " bytes ]",
                    "Configuration (I/O) data");

                if (showBinData)
                {
                    PrnParseData.processBinary(
                        table,
                        PrnParseConstants.eOvlShow.None,
                        buf,
                        fileOffset,
                        bufOffset,
                        binDataLen,
                        "",
                        showBinData,
                        false,
                        true,
                        indxOffsetFormat,
                        analysisLevel);
                }

                //------------------------------------------------------------//

                dataStart = bufOffset;

                for (Int32 i = 0; i < binDataRem; i++)
                {
                    if (buf[bufOffset + i] == 0x20)
                    {
                        lenKey = i;
                        i = binDataRem;
                    }
                }

                if (lenKey > 0)
                {
                    lenVal = binDataRem - lenKey - 1;
                }
                else
                {
                    lenKey = binDataRem;
                    lenVal = 0;
                }

                PrnParseData.processLines(
                    table,
                    PrnParseConstants.eOvlShow.None,
                    linkData,
                    ToolCommonData.ePrintLang.Unknown,
                    buf,
                    fileOffset,
                    lenKey,
                    ref bufRem,
                    ref bufOffset,
                    ref dummyBool,
                    false,
                    false,
                    true,
                    PrnParseConstants.asciiEsc,
                    "Key",
                    0,
                    indxCharSetSubAct,
                    (Byte)valCharSetSubCode,
                    indxCharSetName,
                    indxOffsetFormat,
                    analysisLevel);

                if (lenVal > 0)
                {
                    bufOffset++;
                    bufRem--;

                    PrnParseData.processLines(
                        table,
                        PrnParseConstants.eOvlShow.None,
                        linkData,
                        ToolCommonData.ePrintLang.Unknown,
                        buf,
                        fileOffset,
                        lenVal,
                        ref bufRem,
                        ref bufOffset,
                        ref dummyBool,
                        false,
                        false,
                        false,
                        PrnParseConstants.asciiEsc,
                        "Value",
                        0,
                        indxCharSetSubAct,
                        (Byte)valCharSetSubCode,
                        indxCharSetName,
                        indxOffsetFormat,
                        analysisLevel);
                }

                if (options.FlagPMLWithinPCL)
                {
                    if ((binDataLen > 4) &&
                        ((buf[dataStart] == 0x50) &&           // P
                         (buf[dataStart + 1] == 0x4d) &&       // M
                         (buf[dataStart + 2] == 0x4c) &&       // L
                         (buf[dataStart + 3] == 0x20)))        // space
                    {
                        PrnParsePML parsePML = new PrnParsePML();

                        dataOK = parsePML.processPMLEmbedded(buf,
                                                             fileOffset,
                                                             binDataLen,
                                                             dataStart,
                                                             linkData,
                                                             options,
                                                             table);
                    }
                }
            }

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e C o n f i g u r e I m a g e D a t a                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the associated data part of the Configure Image Data    //
        // command.                                                           //
        // Format is <esc>*v#W[binary-data]                                   //
        // The common 'short form' uses 6 bytes of binary data; there are     //
        // various long forms.                                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean decodeConfigureImageData(
            Int32 binDataLen,
            Int32 fileOffset,
            Byte[] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            PrnParseLinkData linkData,
            PrnParseOptions options,
            DataTable table)
        {
            const Int32 lenMin = 6;
            const Int32 lenMax = 122;   // must be less than buffer size 

            PrnParseConstants.eContType contType;

            Int32 binDataRem;

            Boolean dataOK;

            //----------------------------------------------------------------//
            //                                                                //
            // Initialise.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            dataOK = true;

            binDataRem = binDataLen;

            if ((binDataLen < lenMin) || (binDataLen > lenMax))
            {
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.MsgWarning,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "*** Warning ***",
                    "",
                    "Configure Image Data length < " + lenMin +
                    " or > " + lenMax);

                dataOK = false;
                contType = PrnParseConstants.eContType.PCLDownload;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataRem,
                                      true,
                                      0x00,
                                      0x00);
            }
            else if (binDataRem > bufRem)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Not all of binary data is in current buffer.               //
                // Initiate continuation action.                              //
                //                                                            //
                //------------------------------------------------------------//

                contType = PrnParseConstants.eContType.PCLConfigureImageData;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataRem,
                                      true,
                                      0x00,
                                      0x00);
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // The first six bytes are common to both 'short' and 'long'  //
                // forms of the data.                                         //
                //                                                            //
                //------------------------------------------------------------//

                Int32 offset;

                Byte colourSpace;
                Byte pixelEncodingMode;
                Byte bitsPerIndex;
                Byte bitsPerPrimary1;
                Byte bitsPerPrimary2;
                Byte bitsPerPrimary3;

                String codeDesc;

                Int32 analysisLevel;

                Boolean showBinData;

                PrnParseConstants.eOptOffsetFormats indxOffsetFormat;

                analysisLevel = linkData.AnalysisLevel;
                showBinData = options.FlagPCLMiscBinData;

                indxOffsetFormat = options.IndxGenOffsetFormat;

                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    PrnParseRowTypes.eType.DataBinary,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    indxOffsetFormat,
                    fileOffset + bufOffset,
                    analysisLevel,
                    "PCL Binary",
                    "[ " + lenMin + " bytes ]",
                    "Configure Image Data header");

                if (showBinData)
                {
                    PrnParseData.processBinary(
                        table,
                        PrnParseConstants.eOvlShow.None,
                        buf,
                        fileOffset,
                        bufOffset,
                        lenMin,
                        "",
                        showBinData,
                        false,
                        true,
                        indxOffsetFormat,
                        analysisLevel);
                }

                //------------------------------------------------------------//

                offset = bufOffset;

                colourSpace = buf[offset];

                switch (colourSpace)
                {
                    case 0:
                        codeDesc = "0: Device RGB";
                        break;

                    case 1:
                        codeDesc = "1: Device CMY";
                        break;

                    case 2:
                        codeDesc = "2: Colorimetric RGB Spaces";
                        break;

                    case 3:
                        codeDesc = "3: CIE L*a*b*";
                        break;

                    case 4:
                        codeDesc = "4: Luminance-Chrominance Spaces";
                        break;

                    default:
                        codeDesc = colourSpace.ToString() + ": unknown";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Color Space",
                    "",
                    codeDesc);

                //------------------------------------------------------------//

                offset++;

                pixelEncodingMode = buf[offset];

                switch (pixelEncodingMode)
                {
                    case 0:
                        codeDesc = "0: Indexed by Plane";
                        break;

                    case 1:
                        codeDesc = "1: Indexed by Pixel";
                        break;

                    case 2:
                        codeDesc = "2: Direct by Plane";
                        break;

                    case 3:
                        codeDesc = "3: Direct by Pixel";
                        break;

                    default:
                        codeDesc = pixelEncodingMode.ToString() + ": unknown";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Pixel Encoding",
                    "",
                    codeDesc);

                //------------------------------------------------------------//

                offset++;

                bitsPerIndex = buf[offset];

                codeDesc = bitsPerIndex.ToString() +
                           ": palette size = " +
                           (1 << bitsPerIndex).ToString();

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Bits/Index",
                    "",
                    codeDesc);

                //------------------------------------------------------------//

                offset++;

                bitsPerPrimary1 = buf[offset];

                codeDesc = bitsPerPrimary1.ToString();

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Bits/Primary 1",
                    "",
                    codeDesc);

                //------------------------------------------------------------//

                offset++;

                bitsPerPrimary2 = buf[offset];

                codeDesc = bitsPerPrimary2.ToString();

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Bits/Primary 2",
                    "",
                    codeDesc);

                //------------------------------------------------------------//

                offset++;

                bitsPerPrimary3 = buf[offset];

                codeDesc = bitsPerPrimary3.ToString();

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Bits/Primary 3",
                    "",
                    codeDesc);

                //------------------------------------------------------------//

                bufRem = bufRem - lenMin;
                bufOffset = bufOffset + lenMin;
                binDataRem = binDataRem - lenMin;

                //------------------------------------------------------------//
                //                                                            //
                // Any remaining bytes are from the 'long' form of the data.  //
                // TODO: interpret these as either pairs of sint16 objects    //
                //                              or pairs of real32 objects    //
                //                                                            //
                //------------------------------------------------------------//

                if (binDataRem > 0)
                {
                    PrnParseCommon.addDataRow (
                        PrnParseRowTypes.eType.DataBinary,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        indxOffsetFormat,
                        fileOffset + bufOffset,
                        analysisLevel,
                        "PCL Binary",
                        "[ " + binDataRem + " bytes ]",
                        "Configure Image Data parameters");

                    if (showBinData)
                    {
                        PrnParseData.processBinary(
                            table,
                            PrnParseConstants.eOvlShow.None,
                            buf,
                            fileOffset,
                            bufOffset,
                            binDataRem,
                            "",
                            showBinData,
                            false,
                            true,
                            indxOffsetFormat,
                            analysisLevel);
                    }

                    switch (colourSpace)
                    {
                        case 0:
                            dataOK = decodeConfigureImageData_0_and_1 (
                                        binDataRem,
                                        buf,
                                        ref bufRem,
                                        ref bufOffset,  
                                        table);
                            break;

                        case 1:
                            dataOK = decodeConfigureImageData_0_and_1 (
                                        binDataRem,
                                        buf,
                                        ref bufRem,
                                        ref bufOffset,
                                        table);
                            break;

                        case 2:
                            dataOK = decodeConfigureImageData_2 (
                                        binDataRem,
                                        buf,
                                        ref bufRem,
                                        ref bufOffset,
                                        table);
                            break;

                        case 3:
                            dataOK = decodeConfigureImageData_3 (
                                        binDataRem,
                                        buf,
                                        ref bufRem,
                                        ref bufOffset,
                                        table);
                            break;

                        case 4:
                            dataOK = decodeConfigureImageData_4 (
                                        binDataRem,
                                        buf,
                                        ref bufRem,
                                        ref bufOffset,
                                        table);
                            break;

                        default:
                            bufRem = bufRem - binDataRem;
                            bufOffset = bufOffset + binDataRem;
                            break;
                    }
                }
            }

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e C o n f i g u r e I m a g e D a t a _ 0 _ a n d _ 1    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the associated data part of Configure Image Data        //
        // formats 0 and 1.                                                   //
        // Data length = 18 (including 6 byte header)                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean decodeConfigureImageData_0_and_1 (
            Int32 binDataLen,
            Byte [] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            DataTable table)
        {
            const Int32 fixedLen = 18 - 6;

            Boolean dataOK = true;

            //----------------------------------------------------------------//

            Int32 offset;

            String textDesc;

            Int16 sint16Val;

            //----------------------------------------------------------------//

            offset = bufOffset;

            if (binDataLen == fixedLen)
            {
                textDesc = "White reference";

                for (Int32 i = 0; i < 2; i++)
                {
                    for (Int32 j = 0; j < 3; j++)
                    {
                        sint16Val = (Int16)((buf [offset] * 256) + buf [offset + 1]);

                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLDecode,
                            table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            textDesc,
                            "Primary " + (j + 1),
                            sint16Val.ToString ());

                        offset += 2;
                    }

                    textDesc = "Black reference";
                }
            }

            //----------------------------------------------------------------//

            bufRem = bufRem - binDataLen;
            bufOffset = bufOffset + binDataLen;

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e C o n f i g u r e I m a g e D a t a _ 2                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the associated data part of Configure Image Data        //
        // format 2.                                                          //
        // Data length = 86  (including 6 byte header)                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean decodeConfigureImageData_2 (
            Int32 binDataLen,
            Byte [] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            DataTable table)
        {
            const Int32 fixedLen = 86 - 6;

            Boolean dataOK = true;

            //----------------------------------------------------------------//

            Int32 offset;

            String codeDesc,
                   textDesc;

            //----------------------------------------------------------------//

            offset = bufOffset;

            if (binDataLen == fixedLen)
            {
                for (Int32 i = 0; i < 4; i++)
                {
                    if (i == 0)
                        textDesc = "Red";
                    else if (i == 1)
                        textDesc = "Green";
                    else if (i == 2)
                        textDesc = "Blue";
                    else
                        textDesc = "White-point";

                    codeDesc = processReal32 (buf, offset);

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        textDesc,
                        "x Chromaticity",
                        codeDesc);

                    offset += 4;

                    codeDesc = processReal32 (buf, offset);

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        textDesc,
                        "y Chromaticity",
                        codeDesc);

                    offset += 4;
                }

                for (Int32 i = 0; i < 3; i++)
                {
                    if (i == 0)
                        textDesc = "Red";
                    else if (i == 1)
                        textDesc = "Green";
                    else
                        textDesc = "Blue";

                    codeDesc = processReal32 (buf, offset);

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        textDesc,
                        "Gamma",
                        codeDesc);

                    offset += 4;

                    codeDesc = processReal32 (buf, offset);

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        textDesc,
                        "Gain",
                        codeDesc);

                    offset += 4;
                }

                for (Int32 i = 0; i < 3; i++)
                {
                    if (i == 0)
                        textDesc = "Red";
                    else if (i == 1)
                        textDesc = "Green";
                    else
                        textDesc = "Blue";

                    codeDesc = processReal32 (buf, offset);

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        textDesc,
                        "Minimum",
                        codeDesc);

                    offset += 4;

                    codeDesc = processReal32 (buf, offset);

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        textDesc,
                        "Maximum",
                        codeDesc);

                    offset += 4;
                }
            }

            //----------------------------------------------------------------//

            bufRem = bufRem - binDataLen;
            bufOffset = bufOffset + binDataLen;

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e C o n f i g u r e I m a g e D a t a _ 3                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the associated data part of Configure Image Data        //
        // format 3.                                                          //
        // Data length = 30  (including 6 byte header)                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean decodeConfigureImageData_3 (
            Int32 binDataLen,
            Byte [] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            DataTable table)
        {
            const Int32 fixedLen = 30 - 6;

            Boolean dataOK = true;

            //----------------------------------------------------------------//

            Int32 offset;

            String codeDesc,
                   textDesc;

            //----------------------------------------------------------------//

            offset = bufOffset;

            if (binDataLen == fixedLen)
            {
                for (Int32 i = 0; i < 3; i++)
                {
                    if (i == 0)
                        textDesc = "L*";
                    else if (i == 1)
                        textDesc = "a*";
                    else
                        textDesc = "b*";

                    codeDesc = processReal32(buf, offset);

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        textDesc,
                        "Minimum",
                        codeDesc);

                    offset += 4;

                    codeDesc = processReal32(buf, offset);

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        textDesc,
                        "Maximum",
                        codeDesc);

                    offset += 4;
                }
            }

            //----------------------------------------------------------------//

            bufRem = bufRem - binDataLen;
            bufOffset = bufOffset + binDataLen;

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e C o n f i g u r e I m a g e D a t a _ 4                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the associated data part of Configure Image Data        //
        // format 4.                                                          //
        // Data length = 122  (including 6 byte header)                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean decodeConfigureImageData_4 (
            Int32 binDataLen,
            Byte [] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            DataTable table)
        {
            const Int32 fixedLen = 122 - 6;

            Boolean dataOK = true;

            //----------------------------------------------------------------//

            Int32 offset;

            String codeDesc,
                   textDesc;

            //----------------------------------------------------------------//

            offset = bufOffset;

            if (binDataLen == fixedLen)
            {
                for (Int32 i = 0; i < 3; i++)
                {
                    textDesc = "Encoding: Primary " + (i + 1);

                    codeDesc = processReal32 (buf, offset);

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        textDesc,
                        "Red",
                        codeDesc);

                    offset += 4;

                    codeDesc = processReal32 (buf, offset);

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        textDesc,
                        "Green",
                        codeDesc);

                    offset += 4;

                    codeDesc = processReal32 (buf, offset);

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        textDesc,
                        "Blue",
                        codeDesc);

                    offset += 4;
                }

                for (Int32 i = 0; i < 3; i++)
                {
                    textDesc = "Primary " + (i + 1);

                    codeDesc = processReal32 (buf, offset);

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        textDesc,
                        "Minimum",
                        codeDesc);

                    offset += 4;

                    codeDesc = processReal32 (buf, offset);

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        textDesc,
                        "Maximum",
                        codeDesc);

                    offset += 4;
                }

                for (Int32 i = 0; i < 4; i++)
                {
                    if (i == 0)
                        textDesc = "Red";
                    else if (i == 1)
                        textDesc = "Green";
                    else if (i == 2)
                        textDesc = "Blue";
                    else
                        textDesc = "White-point";

                    codeDesc = processReal32 (buf, offset);

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        textDesc,
                        "x Chromaticity",
                        codeDesc);

                    offset += 4;

                    codeDesc = processReal32 (buf, offset);

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        textDesc,
                        "y Chromaticity",
                        codeDesc);

                    offset += 4;
                }

                for (Int32 i = 0; i < 3; i++)
                {
                    if (i == 0)
                        textDesc = "Red";
                    else if (i == 1)
                        textDesc = "Green";
                    else
                        textDesc = "Blue";

                    codeDesc = processReal32 (buf, offset);

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        textDesc,
                        "Gamma",
                        codeDesc);

                    offset += 4;

                    codeDesc = processReal32 (buf, offset);

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        textDesc,
                        "Gain",
                        codeDesc);

                    offset += 4;
                }
            }

            //----------------------------------------------------------------//

            bufRem = bufRem - binDataLen;
            bufOffset = bufOffset + binDataLen;

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e C o n f i g u r e R a s t e r D a t a                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the associated data part of the Configure Raster Data   //
        // sequence.                                                          //
        // Format is <esc>*g#W[binary-data]                                   //
        // There are various formats (not all of them known here).            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean decodeConfigureRasterData(
            Int32 binDataLen,
            Int32 fileOffset,
            Byte[] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            PrnParseLinkData linkData,
            PrnParseOptions options,
            DataTable table)
        {
            const Int32 lenMin = 6;
            const Int32 lenMax = 2047;   // must be less than buffer size 

            PrnParseConstants.eContType contType;

            Int32 binDataRem;

            Boolean dataOK;

            //----------------------------------------------------------------//
            //                                                                //
            // Initialise.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            dataOK = true;

            binDataRem = binDataLen;

            if ((binDataLen < lenMin) || (binDataLen > lenMax))
            {
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.MsgWarning,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "*** Warning ***",
                    "",
                    "Configure Raster Data length < " + lenMin +
                    " or > " + lenMax);

                dataOK = false;
                contType = PrnParseConstants.eContType.PCLDownload;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataRem,
                                      true,
                                      0x00,
                                      0x00);
            }
            else if (binDataRem > bufRem)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Not all of binary data is in current buffer.               //
                // Initiate continuation action.                              //
                //                                                            //
                //------------------------------------------------------------//

                contType = PrnParseConstants.eContType.PCLConfigureRasterData;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataRem,
                                      true,
                                      0x00,
                                      0x00);
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // The first byte is common to all formats.                   //
                //                                                            //
                //------------------------------------------------------------//

                Byte format;

                format = buf[bufOffset];

                switch (format)
                {
                    case 1:
                        dataOK = decodeConfigureRasterData_1 (
                                    binDataLen,
                                    fileOffset,
                                    buf,
                                    ref bufRem,
                                    ref bufOffset,
                                    linkData,
                                    options,
                                    table);
                        break;

                    case 2:
                        dataOK = decodeConfigureRasterData_2 (
                                    binDataLen,
                                    fileOffset,
                                    buf,
                                    ref bufRem,
                                    ref bufOffset,
                                    linkData,
                                    options,
                                    table);
                        break;

                    case 3:
                        dataOK = decodeConfigureRasterData_3 (
                                    binDataLen,
                                    fileOffset,
                                    buf,
                                    ref bufRem,
                                    ref bufOffset,
                                    linkData,
                                    options,
                                    table);
                        break;

                    case 4:
                        dataOK = decodeConfigureRasterData_4 (
                                    binDataLen,
                                    fileOffset,
                                    buf,
                                    ref bufRem,
                                    ref bufOffset,
                                    linkData,
                                    options,
                                    table);
                        break;

                    case 5:
                        dataOK = decodeConfigureRasterData_5 (
                                    binDataLen,
                                    fileOffset,
                                    buf,
                                    ref bufRem,
                                    ref bufOffset,
                                    linkData,
                                    options,
                                    table);
                        break;

                    case 6:
                        dataOK = decodeConfigureRasterData_6 (
                                    binDataLen,
                                    fileOffset,
                                    buf,
                                    ref bufRem,
                                    ref bufOffset,
                                    linkData,
                                    options,
                                    table);
                        break;

                    case 7:
                        dataOK = decodeConfigureRasterData_7 (
                                    binDataLen,
                                    fileOffset,
                                    buf,
                                    ref bufRem,
                                    ref bufOffset,
                                    linkData,
                                    options,
                                    table);
                        break;

                    default:
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.MsgWarning,
                            table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "*** Warning ***",
                            "",
                            "Configure Raster Data format " + format +
                            " not known");

                        dataOK = false;
                        contType = PrnParseConstants.eContType.PCLDownload;

                        linkData.setContData(contType,
                                              0,
                                              -bufRem,
                                              binDataLen,
                                              true,
                                              0x00,
                                              0x00);
                        break;
                }
            }

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e C o n f i g u r e R a s t e r D a t a _ 1              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the associated data part of Configure Raster Data       //
        // format 1.                                                          //
        // Data length = 2 + (6 * component count)                            //
        // Used by DeskJet 520, DeskJet 560.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean decodeConfigureRasterData_1(
            Int32 binDataLen,
            Int32 fileOffset,
            Byte[] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            PrnParseLinkData linkData,
            PrnParseOptions options,
            DataTable table)
        {
            const String formatName = "1: Complex Direct Planar (obsolete)";
            const String itemName   = "Component";

            const Int32 fixedLen = 2;
            const Int32 itemLen  = 6;

            PrnParseConstants.eContType contType;

            Boolean dataOK = true;
            
            //----------------------------------------------------------------//
            //                                                                //
            // The first byte is common to all formats.                       //
            //                                                                //
            //----------------------------------------------------------------//

            Int32 offset;
            Int32 calcLen;

            Byte format;
            Byte itemCt;

            String codeDesc;

            Int32 analysisLevel;

            Boolean showBinData;

            PrnParseConstants.eOptOffsetFormats indxOffsetFormat;

            analysisLevel = linkData.AnalysisLevel;
            showBinData = options.FlagPCLMiscBinData;

            indxOffsetFormat = options.IndxGenOffsetFormat;

            //----------------------------------------------------------------//

            PrnParseCommon.addDataRow (
                PrnParseRowTypes.eType.DataBinary,
                table,
                PrnParseConstants.eOvlShow.None,
                indxOffsetFormat,
                fileOffset + bufOffset,
                analysisLevel,
                "PCL Binary",
                "[ " + binDataLen + " bytes ]",
                "Configure Raster Data header");

            if (showBinData)
            {
                PrnParseData.processBinary(
                    table,
                    PrnParseConstants.eOvlShow.None,
                    buf,
                    fileOffset,
                    bufOffset,
                    binDataLen,
                    "",
                    showBinData,
                    false,
                    true,
                    indxOffsetFormat,
                    analysisLevel);
            }

            //----------------------------------------------------------------//

            offset = bufOffset;

            format = buf[offset];
            
            PrnParseCommon.addTextRow (
                PrnParseRowTypes.eType.PCLDecode,
                table,
                PrnParseConstants.eOvlShow.None,
                "",
                "Format",
                "",
                formatName);

            //----------------------------------------------------------------//

            itemCt = buf[offset + 1];

            calcLen = fixedLen + (itemLen * itemCt);

            if (calcLen != binDataLen)
            {
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.MsgError,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "*** Error ***",
                    "",
                    "actual length " + binDataLen +
                    " != calculated length " + calcLen);

                dataOK = false;
                contType = PrnParseConstants.eContType.PCLDownload;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataLen,
                                      true,
                                      0x00,
                                      0x00);
            }
            else
            {
                UInt16 uint16Val;

                offset++;

                switch (itemCt)
                {
                    case 1:
                        codeDesc = "1: Monochrome (K)";
                        break;

                    case 3:
                        codeDesc = "3: Colour (CMY)";
                        break;

                    case 4:
                        codeDesc = "4: Colour (KCMY)";
                        break;

                    default:
                        codeDesc = itemCt.ToString() + ": unknown";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    itemName,
                    "Count",
                    codeDesc);

                //------------------------------------------------------------//
                //                                                            //
                // Decode the elements in each 'component'.                   //
                //                                                            //
                //------------------------------------------------------------//

                offset = bufOffset + fixedLen;

                for (Int32 i = 1; i < (itemCt + 1); i++)
                {
                    uint16Val = (UInt16)((buf[offset] * 256) + buf[offset + 1]);

                    codeDesc = uint16Val.ToString() + " pixels-per-inch";

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        itemName + " " + i,
                        "Res. Horizontal",
                        codeDesc);

                    //--------------------------------------------------------//

                    offset += 2;

                    uint16Val = (UInt16)((buf[offset] * 256) + buf[offset + 1]);

                    codeDesc = uint16Val.ToString() + " pixels-per-inch";


                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        itemName + " " + i,
                        "Res. Vertical",
                        codeDesc);

                    //--------------------------------------------------------//

                    offset += 2;

                    uint16Val = (UInt16)((buf[offset] * 256) + buf[offset + 1]);

                    codeDesc = uint16Val.ToString();

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        itemName + " " + i,
                        "Levels",
                        codeDesc);

                    //--------------------------------------------------------//

                    offset += 2;
                }

                //------------------------------------------------------------//

                bufRem = bufRem - binDataLen;
                bufOffset = bufOffset + binDataLen;
            }

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e C o n f i g u r e R a s t e r D a t a _ 2              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the associated data part of Configure Raster Data       //
        // format 2.                                                          //
        // Data length = 2 + (6 * component count)                            //
        // Used by DeskJet 540, DeskJet 660, DeskJet 850, OfficeJet V40.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean decodeConfigureRasterData_2(
            Int32 binDataLen,
            Int32 fileOffset,
            Byte[] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            PrnParseLinkData linkData,
            PrnParseOptions options,
            DataTable table)
        {
            const String formatName = "2: Complex Direct Planar";
            const String itemName = "Component";

            const Int32 fixedLen = 2;
            const Int32 itemLen = 6;

            PrnParseConstants.eContType contType;

            Boolean dataOK = true;

            //----------------------------------------------------------------//
            //                                                                //
            // The first byte is common to all formats.                       //
            //                                                                //
            //----------------------------------------------------------------//

            Int32 offset;
            Int32 calcLen;

            Byte format;
            Byte itemCt;

            String codeDesc;

            Int32 analysisLevel;

            Boolean showBinData;

            PrnParseConstants.eOptOffsetFormats indxOffsetFormat;

            analysisLevel = linkData.AnalysisLevel;
            showBinData = options.FlagPCLMiscBinData;

            indxOffsetFormat = options.IndxGenOffsetFormat;

            //----------------------------------------------------------------//

            PrnParseCommon.addDataRow (
                PrnParseRowTypes.eType.DataBinary,
                table,
                PrnParseConstants.eOvlShow.None,
                indxOffsetFormat,
                fileOffset + bufOffset,
                analysisLevel,
                "PCL Binary",
                "[ " + binDataLen + " bytes ]",
                "Configure Raster Data header");

            if (showBinData)
            {
                PrnParseData.processBinary(
                    table,
                    PrnParseConstants.eOvlShow.None,
                    buf,
                    fileOffset,
                    bufOffset,
                    binDataLen,
                    "",
                    showBinData,
                    false,
                    true,
                    indxOffsetFormat,
                    analysisLevel);
            }

            //----------------------------------------------------------------//

            offset = bufOffset;

            format = buf[offset];

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Format",
                    "",
                    formatName);

            //----------------------------------------------------------------//

            itemCt = buf[offset + 1];

            calcLen = fixedLen + (itemLen * itemCt);

            if (calcLen != binDataLen)
            {
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.MsgError,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "*** Error ***",
                    "",
                    "actual length " + binDataLen +
                    " != calculated length " + calcLen);

                dataOK = false;
                contType = PrnParseConstants.eContType.PCLDownload;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataLen,
                                      true,
                                      0x00,
                                      0x00);
            }
            else
            {
                UInt16 uint16Val;

                offset++;

                switch (itemCt)
                {
                    case 1:
                        codeDesc = "1: Monochrome (K)";
                        break;

                    case 3:
                        codeDesc = "3: Colour (CMY)";
                        break;

                    case 4:
                        codeDesc = "4: Colour (CMYK)";
                        break;

                    default:
                        codeDesc = itemCt.ToString() + ": unknown";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    itemName,
                    "Count",
                    codeDesc);

                //------------------------------------------------------------//
                //                                                            //
                // Decode the elements in each 'component'.                   //
                //                                                            //
                //------------------------------------------------------------//

                offset = bufOffset + fixedLen;

                for (Int32 i = 1; i < (itemCt + 1); i++)
                {
                    uint16Val = (UInt16)((buf[offset] * 256) + buf[offset + 1]);

                    codeDesc = uint16Val.ToString() + " pixels-per-inch";

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        itemName + " " + i,
                        "Res. Horizontal",
                        codeDesc);

                    //--------------------------------------------------------//

                    offset += 2;

                    uint16Val = (UInt16)((buf[offset] * 256) + buf[offset + 1]);

                    codeDesc = uint16Val.ToString() + " pixels-per-inch";

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        itemName + " " + i,
                        "Res. Vertical",
                        codeDesc);

                    //--------------------------------------------------------//

                    offset += 2;

                    uint16Val = (UInt16)((buf[offset] * 256) + buf[offset + 1]);

                    codeDesc = uint16Val.ToString();

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        itemName + " " + i,
                        "Levels",
                        codeDesc);

                    //--------------------------------------------------------//

                    offset += 2;
                }

                //------------------------------------------------------------//

                bufRem = bufRem - binDataLen;
                bufOffset = bufOffset + binDataLen;
            }

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e C o n f i g u r e R a s t e r D a t a _ 3              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the associated data part of Configure Raster Data       //
        // format 2.                                                          //
        // Data length = 8                                                    //
        // Used by DeskJet 850c.                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean decodeConfigureRasterData_3(
            Int32 binDataLen,
            Int32 fileOffset,
            Byte[] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            PrnParseLinkData linkData,
            PrnParseOptions options,
            DataTable table)
        {
            const String formatName = "3: RGB Direct by Pixel";

            const Int32 fixedLen = 8;

            PrnParseConstants.eContType contType;

            Boolean dataOK = true;

            //----------------------------------------------------------------//
            //                                                                //
            // The first byte is common to all formats.                       //
            //                                                                //
            //----------------------------------------------------------------//

            Int32 offset;
            Int32 calcLen;

            Byte format;

            String codeDesc;

            Int32 analysisLevel;

            Boolean showBinData;

            PrnParseConstants.eOptOffsetFormats indxOffsetFormat;

            analysisLevel = linkData.AnalysisLevel;
            showBinData = options.FlagPCLMiscBinData;

            indxOffsetFormat = options.IndxGenOffsetFormat;

            //----------------------------------------------------------------//

            PrnParseCommon.addDataRow (
                PrnParseRowTypes.eType.DataBinary,
                table,
                PrnParseConstants.eOvlShow.None,
                indxOffsetFormat,
                fileOffset + bufOffset,
                analysisLevel,
                "PCL Binary",
                "[ " + binDataLen + " bytes ]",
                "Configure Raster Data header");

            if (showBinData)
            {
                PrnParseData.processBinary(
                    table,
                    PrnParseConstants.eOvlShow.None,
                    buf,
                    fileOffset,
                    bufOffset,
                    binDataLen,
                    "",
                    showBinData,
                    false,
                    true,
                    indxOffsetFormat,
                    analysisLevel);
            }

            //----------------------------------------------------------------//

            offset = bufOffset;

            format = buf[offset];

            PrnParseCommon.addTextRow (
                PrnParseRowTypes.eType.PCLDecode,
                table,
                PrnParseConstants.eOvlShow.None,
                "",
                "Format",
                "",
                formatName);

            //----------------------------------------------------------------//

            calcLen = fixedLen;

            if (calcLen != binDataLen)
            {
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.MsgError,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "*** Error ***",
                    "",
                    "actual length " + binDataLen +
                    " != calculated length " + calcLen);

                dataOK = false;
                contType = PrnParseConstants.eContType.PCLDownload;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataLen,
                                      true,
                                      0x00,
                                      0x00);
            }
            else
            {
                UInt16 uint16Val;
                Byte byteVal;

                offset++;

                byteVal = buf[offset];

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Bit count:",
                    "Red",
                    byteVal.ToString());

                //------------------------------------------------------------//

                offset++;

                byteVal = buf[offset];

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Bit count:",
                    "Green",
                    byteVal.ToString());

                //------------------------------------------------------------//

                offset++;

                byteVal = buf[offset];

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Bit count:",
                    "Blue",
                    byteVal.ToString());

                //------------------------------------------------------------//

                offset++;

                uint16Val = (UInt16)((buf[offset] * 256) + buf[offset + 1]);

                codeDesc = uint16Val.ToString() + " pixels-per-inch";

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Resolution",
                    "Horizontal",
                    codeDesc);

                //------------------------------------------------------------//

                offset += 2;

                uint16Val = (UInt16)((buf[offset] * 256) + buf[offset + 1]);

                codeDesc = uint16Val.ToString() + " pixels-per-inch";

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Resolution",
                    "Vertical",
                    codeDesc);

                //------------------------------------------------------------//

                bufRem = bufRem - binDataLen;
                bufOffset = bufOffset + binDataLen;
            }

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e C o n f i g u r e R a s t e r D a t a _ 4              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the associated data part of Configure Raster Data       //
        // format 2.                                                          //
        // Data length = 8                                                    //
        // Used by DeskJet 850c.                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean decodeConfigureRasterData_4(
            Int32 binDataLen,
            Int32 fileOffset,
            Byte[] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            PrnParseLinkData linkData,
            PrnParseOptions options,
            DataTable table)
        {
            const String formatName = "4: Indexed by Pixel";

            const Int32 fixedLen = 6;

            PrnParseConstants.eContType contType;

            Boolean dataOK = true;

            //------------------------------------------------------------//
            //                                                            //
            // The first byte is common to all formats.                   //
            //                                                            //
            //------------------------------------------------------------//

            Int32 offset;
            Int32 calcLen;

            Byte format;

            String codeDesc;

            Int32 analysisLevel;

            Boolean showBinData;

            PrnParseConstants.eOptOffsetFormats indxOffsetFormat;

            analysisLevel = linkData.AnalysisLevel;
            showBinData = options.FlagPCLMiscBinData;

            indxOffsetFormat = options.IndxGenOffsetFormat;

            //----------------------------------------------------------------//

            PrnParseCommon.addDataRow (
                PrnParseRowTypes.eType.DataBinary,
                table,
                PrnParseConstants.eOvlShow.None,
                indxOffsetFormat,
                fileOffset + bufOffset,
                analysisLevel,
                "PCL Binary",
                "[ " + binDataLen + " bytes ]",
                "Configure Raster Data header");

            if (showBinData)
            {
                PrnParseData.processBinary(
                    table,
                    PrnParseConstants.eOvlShow.None,
                    buf,
                    fileOffset,
                    bufOffset,
                    binDataLen,
                    "",
                    showBinData,
                    false,
                    true,
                    indxOffsetFormat,
                    analysisLevel);
            }

            //----------------------------------------------------------------//

            offset = bufOffset;

            format = buf[offset];

            PrnParseCommon.addTextRow (
                PrnParseRowTypes.eType.PCLDecode,
                table,
                PrnParseConstants.eOvlShow.None,
                "",
                "Format",
                "",
                formatName);

            //----------------------------------------------------------------//

            calcLen = fixedLen;

            if (calcLen != binDataLen)
            {
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.MsgError,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "*** Error ***",
                    "",
                    "actual length " + binDataLen +
                    " != calculated length " + calcLen);

                dataOK = false;
                contType = PrnParseConstants.eContType.PCLDownload;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataLen,
                                      true,
                                      0x00,
                                      0x00);
            }
            else
            {
                UInt16 uint16Val;
                Byte byteVal;

                offset++;

                byteVal = buf[offset];

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Index bits",
                    "",
                    byteVal.ToString());

                //------------------------------------------------------------//

                offset++;

                uint16Val = (UInt16)((buf[offset] * 256) + buf[offset + 1]);

                codeDesc = uint16Val.ToString() + " pixels-per-inch";

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Resolution",
                    "Horizontal",
                    codeDesc);

                //------------------------------------------------------------//

                offset += 2;

                uint16Val = (UInt16)((buf[offset] * 256) + buf[offset + 1]);

                codeDesc = uint16Val.ToString() + " pixels-per-inch";

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Resolution",
                    "Vertical",
                    codeDesc);

                //------------------------------------------------------------//

                bufRem = bufRem - binDataLen;
                bufOffset = bufOffset + binDataLen;
            }

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e C o n f i g u r e R a s t e r D a t a _ 5              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the associated data part of Configure Raster Data       //
        // format 2.                                                          //
        // Data length = 8                                                    //
        // Used by DeskJet 850c, DeskJet 860c.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean decodeConfigureRasterData_5(
            Int32 binDataLen,
            Int32 fileOffset,
            Byte[] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            PrnParseLinkData linkData,
            PrnParseOptions options,
            DataTable table)
        {
            const String formatName = "5: Mixed Monochrome & Indexed by Pixel";

            const Int32 fixedLen = 11;

            PrnParseConstants.eContType contType;

            Boolean dataOK = true;

            //----------------------------------------------------------------//
            //                                                                //
            // The first byte is common to all formats.                       //
            //                                                                //
            //----------------------------------------------------------------//

            Int32 offset;
            Int32 calcLen;

            Byte format;

            String codeDesc;

            Int32 analysisLevel;

            Boolean showBinData;

            PrnParseConstants.eOptOffsetFormats indxOffsetFormat;

            analysisLevel = linkData.AnalysisLevel;
            showBinData = options.FlagPCLMiscBinData;

            indxOffsetFormat = options.IndxGenOffsetFormat;

            //----------------------------------------------------------------//

            PrnParseCommon.addDataRow (
                PrnParseRowTypes.eType.DataBinary,
                table,
                PrnParseConstants.eOvlShow.None,
                indxOffsetFormat,
                fileOffset + bufOffset,
                analysisLevel,
                "PCL Binary",
                "[ " + binDataLen + " bytes ]",
                "Configure Raster Data header");

            if (showBinData)
            {
                PrnParseData.processBinary(
                    table,
                    PrnParseConstants.eOvlShow.None,
                    buf,
                    fileOffset,
                    bufOffset,
                    binDataLen,
                    "",
                    showBinData,
                    false,
                    true,
                    indxOffsetFormat,
                    analysisLevel);
            }

            //----------------------------------------------------------------//

            offset = bufOffset;

            format = buf[offset];

            PrnParseCommon.addTextRow (
                PrnParseRowTypes.eType.PCLDecode,
                table,
                PrnParseConstants.eOvlShow.None,
                "",
                "Format",
                "",
                formatName);

            //----------------------------------------------------------------//

            calcLen = fixedLen;

            if (calcLen != binDataLen)
            {
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.MsgError,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "*** Error ***",
                    "",
                    "actual length " + binDataLen +
                    " != calculated length " + calcLen);

                dataOK = false;
                contType = PrnParseConstants.eContType.PCLDownload;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataLen,
                                      true,
                                      0x00,
                                      0x00);
            }
            else
            {
                UInt16 uint16Val;
                Byte byteVal;

                offset++;

                byteVal = buf[offset];

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Index bits",
                    "",
                    byteVal.ToString());

                //------------------------------------------------------------//

                offset++;

                uint16Val = (UInt16)((buf[offset] * 256) + buf[offset + 1]);

                codeDesc = uint16Val.ToString() + " pixels-per-inch";

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Indexed Resolution",
                    "Horizontal",
                    codeDesc);

                //------------------------------------------------------------//

                offset += 2;

                uint16Val = (UInt16)((buf[offset] * 256) + buf[offset + 1]);

                codeDesc = uint16Val.ToString() + " pixels-per-inch";

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Indexed Resolution",
                    "Vertical",
                    codeDesc);

                //------------------------------------------------------------//

                offset +=2;

                uint16Val = (UInt16)((buf[offset] * 256) + buf[offset + 1]);

                codeDesc = uint16Val.ToString() + " pixels-per-inch";

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Monochrome Resolution",
                    "Horizontal",
                    codeDesc);

                //------------------------------------------------------------//

                offset += 2;

                uint16Val = (UInt16)((buf[offset] * 256) + buf[offset + 1]);

                codeDesc = uint16Val.ToString() + " pixels-per-inch";

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Monochrome Resolution",
                    "Vertical",
                    codeDesc);

                //------------------------------------------------------------//

                offset += 2;

                byteVal = buf[offset];

                switch (byteVal)
                {
                    case 1:
                        codeDesc = "1: Indexed then Monochrome (top)";
                        break;

                    case 2:
                        codeDesc = "2: Monochrome then indexed (top)";
                        break;

                    case 3:
                        codeDesc = "3: Monochrome (top) then Indexed";
                        break;

                    case 4:
                        codeDesc = "4: indexed (top) then Monochrome";
                        break;

                    default:
                        codeDesc = byteVal.ToString() + ": unknown";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Data Organisation",
                    "",
                    codeDesc);

                //------------------------------------------------------------//

                bufRem = bufRem - binDataLen;
                bufOffset = bufOffset + binDataLen;
            }

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e C o n f i g u r e R a s t e r D a t a _ 6              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the associated data part of Configure Raster Data       //
        // format 6.                                                          //
        // Data length = 4 + (8 * component count)                            //
        // Used by OfficeJet 6700, OfficeJet 7400.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean decodeConfigureRasterData_6(
            Int32 binDataLen,
            Int32 fileOffset,
            Byte[] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            PrnParseLinkData linkData,
            PrnParseOptions options,
            DataTable table)
        {
            const String formatName = "6: identity name not known";
            const String itemName = "Component";

            const Int32 fixedLen = 4;
            const Int32 itemLen = 8;

            PrnParseConstants.eContType contType;

            Boolean dataOK = true;

            //----------------------------------------------------------------//
            //                                                                //
            // The first byte is common to all formats.                       //
            //                                                                //
            //----------------------------------------------------------------//

            Int32 offset;
            Int32 calcLen;

            Byte format;
            UInt16 itemCt;

            String codeDesc;

            Int32 analysisLevel;

            Boolean showBinData;

            PrnParseConstants.eOptOffsetFormats indxOffsetFormat;

            analysisLevel = linkData.AnalysisLevel;
            showBinData = options.FlagPCLMiscBinData;

            indxOffsetFormat = options.IndxGenOffsetFormat;

            //----------------------------------------------------------------//

            PrnParseCommon.addDataRow (
                PrnParseRowTypes.eType.DataBinary,
                table,
                PrnParseConstants.eOvlShow.None,
                indxOffsetFormat,
                fileOffset + bufOffset,
                analysisLevel,
                "PCL Binary",
                "[ " + binDataLen + " bytes ]",
                "Configure Raster Data header");

            if (showBinData)
            {
                PrnParseData.processBinary(
                    table,
                    PrnParseConstants.eOvlShow.None,
                    buf,
                    fileOffset,
                    bufOffset,
                    binDataLen,
                    "",
                    showBinData,
                    false,
                    true,
                    indxOffsetFormat,
                    analysisLevel);
            }

            //----------------------------------------------------------------//

            offset = bufOffset;

            format = buf[offset];

            PrnParseCommon.addTextRow (
                PrnParseRowTypes.eType.PCLDecode,
                table,
                PrnParseConstants.eOvlShow.None,
                "",
                "Format",
                "",
                formatName);

            //----------------------------------------------------------------//

            itemCt = (UInt16)((buf[offset + 2] * 256) + buf[offset + 3]);

            calcLen = fixedLen + (itemLen * itemCt);

            if (calcLen != binDataLen)
            {
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.MsgError,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "*** Error ***",
                    "",
                    "actual length " + binDataLen +
                    " != calculated length " + calcLen);

                dataOK = false;
                contType = PrnParseConstants.eContType.PCLDownload;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataLen,
                                      true,
                                      0x00,
                                      0x00);
            }
            else
            {
                UInt16 uint16Val;
                Byte byteVal;
 
                offset++;

                byteVal = buf[offset];

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Pen mode (?)",
                    "",
                    byteVal.ToString());

                //------------------------------------------------------------//

                offset++;

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    itemName,
                    "Count",
                    itemCt.ToString());

                //------------------------------------------------------------//
                //                                                            //
                // Decode the elements in each 'component'.                   //
                // Note that we don't know what some of these elements are    //
                // for this format!                                           // 
                //                                                            //
                //------------------------------------------------------------//

                offset = bufOffset + fixedLen;

                for (Int32 i = 1; i < (itemCt + 1); i++)
                {
                    uint16Val = (UInt16)((buf[offset] * 256) + buf[offset + 1]);

                    codeDesc = uint16Val.ToString() + " pixels-per-inch";

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        itemName + " " + i,
                        "Res. Horizontal",
                        codeDesc);

                    //--------------------------------------------------------//

                    offset += 2;

                    uint16Val = (UInt16)((buf[offset] * 256) + buf[offset + 1]);

                    codeDesc = uint16Val.ToString() + " pixels-per-inch";

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        itemName + " " + i,
                        "Res. Vertical",
                        codeDesc);

                    //--------------------------------------------------------//

                    offset += 2;

                    byteVal = buf[offset];

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        itemName + " " + i,
                        "Compression Mode",
                        byteVal.ToString());

                    //--------------------------------------------------------//

                    offset++;

                    byteVal = buf[offset];

                    switch (byteVal)
                    {
                        case 0:
                            codeDesc = "0: Portrait";
                            break;

                        case 1:
                            codeDesc = "1: Lansdscape";
                            break;

                        case 2:
                            codeDesc = "2: Reverse Portrait";
                            break;

                        case 3:
                            codeDesc = "3: Reverse Landscape";
                            break;

                        default:
                            codeDesc = byteVal.ToString() + ": unknown";
                            break;
                    }

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        itemName + " " + i,
                        "Orientation",
                        codeDesc);

                    //--------------------------------------------------------//

                    offset++;

                    byteVal = buf[offset];

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        itemName + " " + i,
                        "Bits per Pixel",
                        byteVal.ToString());

                    //--------------------------------------------------------//

                    offset++;

                    byteVal = buf[offset];

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        itemName + " " + i,
                        "Planes per Pixel",
                        byteVal.ToString());

                    //--------------------------------------------------------//

                    offset++;
                }

                //------------------------------------------------------------//

                bufRem = bufRem - binDataLen;
                bufOffset = bufOffset + binDataLen;
            }

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e C o n f i g u r e R a s t e r D a t a _ 7              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the associated data part of Configure Raster Data       //
        // format 7.                                                          //
        // Data length = 4 + (8 * component count)                            //
        // Used by large format DesignJet devices?                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean decodeConfigureRasterData_7(
            Int32 binDataLen,
            Int32 fileOffset,
            Byte[] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            PrnParseLinkData linkData,
            PrnParseOptions options,
            DataTable table)
        {
            const String formatName =
                "7: Complex Direct - Major-Specification Channel-ID";
            const String itemName = "Pen";

            const Int32 fixedLen = 4;
            const Int32 itemLen = 8;

            PrnParseConstants.eContType contType;

            Boolean dataOK = true;

            //----------------------------------------------------------------//
            //                                                                //
            // The first byte is common to all formats.                       //
            //                                                                //
            //----------------------------------------------------------------//

            Int32 offset;
            Int32 calcLen;

            Byte format;
            Byte itemCt;

            String codeDesc;

            Int32 analysisLevel;

            Boolean showBinData;

            PrnParseConstants.eOptOffsetFormats indxOffsetFormat;

            analysisLevel = linkData.AnalysisLevel;
            showBinData = options.FlagPCLMiscBinData;

            indxOffsetFormat = options.IndxGenOffsetFormat;

            //----------------------------------------------------------------//

            PrnParseCommon.addDataRow (
                PrnParseRowTypes.eType.DataBinary,
                table,
                PrnParseConstants.eOvlShow.None,
                indxOffsetFormat,
                fileOffset + bufOffset,
                analysisLevel,
                "PCL Binary",
                "[ " + binDataLen + " bytes ]",
                "Configure Raster Data header");

            if (showBinData)
            {
                PrnParseData.processBinary(
                    table,
                    PrnParseConstants.eOvlShow.None,
                    buf,
                    fileOffset,
                    bufOffset,
                    binDataLen,
                    "",
                    showBinData,
                    false,
                    true,
                    indxOffsetFormat,
                    analysisLevel);
            }

            //----------------------------------------------------------------//

            offset = bufOffset;

            format = buf[offset];

            PrnParseCommon.addTextRow (
                PrnParseRowTypes.eType.PCLDecode,
                table,
                PrnParseConstants.eOvlShow.None,
                "",
                "Format",
                "",
                formatName);

            //----------------------------------------------------------------//

            itemCt = buf[offset + 1];

            calcLen = fixedLen + (itemLen * itemCt);

            if (calcLen != binDataLen)
            {
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.MsgError,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "*** Error ***",
                    "",
                    "actual length " + binDataLen +
                    " != calculated length " + calcLen);

                dataOK = false;
                contType = PrnParseConstants.eContType.PCLDownload;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataLen,
                                      true,
                                      0x00,
                                      0x00);

            }
            else
            {
                UInt16 uint16Val;
                Byte byteVal;

                offset++;

                byteVal = buf[offset];

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    itemName,
                    "Count",
                    itemCt.ToString());

                //------------------------------------------------------------//

                offset++;

                byteVal = buf[offset];

                switch (byteVal)
                {
                    case 0:
                        codeDesc = "0: By Pixel";
                        break;

                    case 1:
                        codeDesc = "1: By Plane";
                        break;

                    default:
                        codeDesc = byteVal.ToString() + ": unknown";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Pens Major",
                    "",
                    codeDesc);

                //------------------------------------------------------------//

                offset++;

                byteVal = buf[offset];

                switch (byteVal)
                {
                    case 0:
                        codeDesc = "0";
                        break;

                    default:
                        codeDesc = byteVal.ToString() +
                                   ": should be zero";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Reserved Byte",
                    "",
                    codeDesc);

                //------------------------------------------------------------//
                //                                                            //
                // Decode the elements for each 'pen'.                        //
                //                                                            //
                //------------------------------------------------------------//

                offset = bufOffset + fixedLen;

                for (Int32 i = 1; i < (itemCt + 1); i++)
                {
                    uint16Val = (UInt16)((buf[offset] * 256) + buf[offset + 1]);

                    codeDesc = uint16Val.ToString() + " pixels-per-inch";

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        itemName + " " + i,
                        "Res. Horizontal",
                        codeDesc);

                    //--------------------------------------------------------//

                    offset += 2;

                    uint16Val = (UInt16)((buf[offset] * 256) + buf[offset + 1]);

                    codeDesc = uint16Val.ToString() + " pixels-per-inch";

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        itemName + " " + i,
                        "Res. Vertical",
                        codeDesc);

                    //--------------------------------------------------------//

                    offset += 2;

                    uint16Val = (UInt16)((buf[offset] * 256) + buf[offset + 1]);

                    codeDesc = uint16Val.ToString() + " (-> bits per pixel = " +
                               Math.Ceiling((Math.Log((Double)uint16Val, 2))).ToString() +
                               ")";

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        itemName + " " + i,
                        "Intensity Levels",
                        codeDesc);

                    //------------------------------------------------------------//

                    offset +=2;

                    byteVal = buf[offset];

                    switch (byteVal)
                    {
                        case 0:
                            codeDesc = "0: By Pixel";
                            break;

                        case 1:
                            codeDesc = "1: By Plane";
                            break;

                        default:
                            codeDesc = byteVal.ToString() + ": unknown";
                            break;
                    }

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        itemName + " " + i,
                        "Planes Major",
                        codeDesc);

                    //--------------------------------------------------------//

                    offset++;

                    byteVal = buf[offset];

                    switch (byteVal)
                    {
                        case 0:
                            codeDesc = "0: K (black)";
                            break;

                        case 1:
                            codeDesc = "1: C (cyan)";
                            break;

                        case 2:
                            codeDesc = "2: M (magenta)";
                            break;

                        case 3:
                            codeDesc = "3: Y (yellow)";
                            break;

                        case 4:
                            codeDesc = "4: R (red)";
                            break;

                        case 5:
                            codeDesc = "5: G (green)";
                            break;

                        case 6:
                            codeDesc = "6: B (blue)";
                            break;

                        case 7:
                            codeDesc = "7: W (white)";
                            break;

                        case 10:
                            codeDesc = "10: O (orange)";
                            break;

                        case 51:
                            codeDesc = "51: k (grey)";
                            break;

                        case 52:
                            codeDesc = "52: c (light cyan)";
                            break;

                        case 53:
                            codeDesc = "53: m (light magenta)";
                            break;

                        default:
                            codeDesc = byteVal.ToString() + ": unknown";
                            break;
                    }

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        itemName + " " + i,
                        "Channel ID",
                        codeDesc);

                    //--------------------------------------------------------//

                    offset++;
                }

                //------------------------------------------------------------//

                bufRem = bufRem - binDataLen;
                bufOffset = bufOffset + binDataLen;
            }

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e D e f i n e L o g i c a l P a g e                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the associated data part of the Define Logical Page     //
        // sequence.                                                          //
        // Format is <esc>&a#W[data]                                          //
        // There are two forms (short and long).                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean decodeDefineLogicalPage(
            Int32 binDataLen,
            Int32 fileOffset,
            Byte[] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            PrnParseLinkData linkData,
            PrnParseOptions options,
            DataTable table)
        {
            const Int32 lenShort = 4;
            const Int32 lenLong = 10;   // must be less than buffer size 

            PrnParseConstants.eContType contType;

            Boolean dataOK;

            //----------------------------------------------------------------//
            //                                                                //
            // Initialise.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            dataOK = true;

            if ((binDataLen != lenShort) && (binDataLen != lenLong))
            {
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.MsgWarning,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "*** Warning ***",
                    "",
                    "Define Logical Page data length not " + lenShort +
                    " or " + lenLong);

                dataOK = false;
                contType = PrnParseConstants.eContType.PCLDownload;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataLen,
                                      true,
                                      0x00,
                                      0x00);
            }
            else if (binDataLen > bufRem)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Not all of binary data is in current buffer.               //
                // Initiate continuation action.                              //
                //                                                            //
                //------------------------------------------------------------//

                contType = PrnParseConstants.eContType.PCLLogicalPageData;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataLen,
                                      true,
                                      0x00,
                                      0x00);
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // interpret data.                                            //
                //                                                            //
                //------------------------------------------------------------//

                Int32 offset;

                Int16 pageOffset;

                UInt16 pageSize;

                Byte pageOrientation,
                     reservedByte;

                String codeDesc;

                Int32 analysisLevel;

                Boolean showBinData;

                PrnParseConstants.eOptOffsetFormats indxOffsetFormat;

                analysisLevel = linkData.AnalysisLevel;
                showBinData = options.FlagPCLMiscBinData;

                indxOffsetFormat = options.IndxGenOffsetFormat;

                PrnParseCommon.addDataRow (
                    PrnParseRowTypes.eType.DataBinary,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    indxOffsetFormat,
                    fileOffset + bufOffset,
                    analysisLevel,
                    "PCL Binary",
                    "[ " + binDataLen + " bytes ]",
                    "Define Logical Page data");

                if (showBinData)
                {
                    PrnParseData.processBinary(
                        table,
                        PrnParseConstants.eOvlShow.None,
                        buf,
                        fileOffset,
                        bufOffset,
                        binDataLen,
                        "",
                        showBinData,
                        false,
                        true,
                        indxOffsetFormat,
                        analysisLevel);
                }

                //------------------------------------------------------------//

                offset = bufOffset;

                pageOffset = (Int16)((buf[offset] * 256) + buf[offset + 1]);

                codeDesc = pageOffset.ToString() + " decipoints";

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Left Offset",
                    "",
                    codeDesc);

                //------------------------------------------------------------//

                offset += 2;

                pageOffset = (Int16)((buf[offset] * 256) + buf[offset + 1]);

                codeDesc = pageOffset.ToString() + " decipoints";

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Top Offset",
                    "",
                    codeDesc);

                //------------------------------------------------------------//

                if (binDataLen == lenLong)
                {
                    offset += 2;

                    pageOrientation = buf[offset];

                    switch (pageOrientation)
                    {
                        case 0:
                            codeDesc = "0: Portrait";
                            break;

                        case 1:
                            codeDesc = "1: Lansdscape";
                            break;

                        case 2:
                            codeDesc = "2: Reverse Portrait";
                            break;

                        case 3:
                            codeDesc = "3: Reverse Landscape";
                            break;

                        default:
                            codeDesc = pageOrientation.ToString() + ": unknown";
                            break;
                    }

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Orientation",
                        "",
                        codeDesc);

                    //--------------------------------------------------------//

                    offset++;

                    reservedByte = buf[offset];

                    switch (reservedByte)
                    {
                        case 0:
                            codeDesc = "0";
                            break;

                        default:
                            codeDesc = reservedByte.ToString() +
                                       ": should be zero";
                            break;
                    }

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Reserved Byte",
                        "",
                        codeDesc);

                    //--------------------------------------------------------//

                    offset++;

                    pageSize = (UInt16)((buf[offset] * 256) +
                                         buf[offset + 1]);

                    codeDesc = pageSize.ToString() + " decipoints";

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Page Width",
                        "",
                        codeDesc);

                    //--------------------------------------------------------//

                    offset += 2;

                    pageSize = (UInt16)((buf[offset] * 256) +
                                         buf[offset + 1]);

                    codeDesc = pageSize.ToString() + " decipoints";

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Page Height",
                        "",
                        codeDesc);
                }

                //------------------------------------------------------------//

                bufRem = bufRem - binDataLen;
                bufOffset = bufOffset + binDataLen;
            }

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e D e f i n e S y m b o l S e t                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the associated data part of the Define Symbol Set       //
        // command.                                                           //
        // Format is <esc>(f#W[binary-data]                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean decodeDefineSymbolSet(
            Int32 binDataLen,
            Int32 fileOffset,
            Byte[] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            PrnParseLinkData linkData,
            PrnParseOptions options,
            DataTable table)
        {
            const Int32 lenMin = 18;
            const Int32 lenMax = (65536 * 2) + lenMin;
            const Int32 kind1Max = 32762;

            PrnParseConstants.eContType contType;

            Int32 binDataRem;

            Boolean dataOK;

            //----------------------------------------------------------------//
            //                                                                //
            // Initialise.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            dataOK = true;

            binDataRem = binDataLen;

            if ((binDataLen < lenMin) || (binDataLen > lenMax))
            {
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.MsgWarning,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "*** Warning ***",
                    "",
                    "Define Symbol Set length < " + lenMin +
                    " or > " + lenMax);

                dataOK = false;
                contType = PrnParseConstants.eContType.PCLDownload;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataRem,
                                      true,
                                      0x00,
                                      0x00);
            }
            else if (lenMin > bufRem)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Not all of binary header data is in current buffer.        //
                // Initiate continuation action.                              //
                //                                                            //
                //------------------------------------------------------------//

                contType = PrnParseConstants.eContType.PCLDefineSymbolSet;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataRem,
                                      true,
                                      0x00,
                                      0x00);
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // The first eighteen bytes are the header.                   //
                //                                                            //
                //------------------------------------------------------------//

                const Byte format_MSL = 1;
                const Byte format_Unicode = 3;

                Int32 offset;

                Int32 mapLen,
                      mapLenCalc,
                      codeCt;

                Byte byteVal;
                Byte format;
                UInt16 uint16Val;
             
                UInt16 firstCode,
                       lastCode;

                String codeDesc;

                Int32 analysisLevel;

                Boolean showBinData;

                PrnParseConstants.eOptOffsetFormats indxOffsetFormat;

                analysisLevel = linkData.AnalysisLevel;
                showBinData = options.FlagPCLMiscBinData;

                indxOffsetFormat = options.IndxGenOffsetFormat;

                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    PrnParseRowTypes.eType.DataBinary,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    indxOffsetFormat,
                    fileOffset + bufOffset,
                    analysisLevel,
                    "PCL Binary",
                    "[ " + lenMin + " bytes ]",
                    "Define Symbol Set header");

                if (showBinData)
                {
                    PrnParseData.processBinary(
                        table,
                        PrnParseConstants.eOvlShow.None,
                        buf,
                        fileOffset,
                        bufOffset,
                        lenMin,
                        "",
                        showBinData,
                        false,
                        true,
                        indxOffsetFormat,
                        analysisLevel);
                }

                //------------------------------------------------------------//

                offset = bufOffset;

                uint16Val = (UInt16)((buf[offset] * 256) + buf[offset + 1]);

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Header size",
                    "",
                    uint16Val.ToString() + " bytes");

                mapLen = binDataLen - uint16Val;

                if (uint16Val != lenMin)
                {
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgWarning,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "*** Warning ***",
                        "",
                        "Define Symbol Set header size != " + lenMin);

                    dataOK = false;
                }

                //------------------------------------------------------------//

                offset += 2;

                uint16Val = (UInt16)((buf[offset] * 256) + buf[offset + 1]);

                codeDesc = uint16Val.ToString() +
                            " (0x" + uint16Val.ToString ("x") + ")" +
                           " : ID = " +
                           PCLSymbolSets.translateKind1ToId(uint16Val);

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Symbol Set ID Code",
                    "",
                    codeDesc);

                if (uint16Val > kind1Max)
                {
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgWarning,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "*** Warning ***",
                        "",
                        "ID Code > maximum value of " +
                        kind1Max + " (ID = " +
                        PCLSymbolSets.translateKind1ToId (kind1Max) + ")");

                    dataOK = false;
                }

                //------------------------------------------------------------//

                offset += 2;

                format = buf[offset];

                switch (format)
                {
                    case format_MSL:
                        codeDesc = "1: MSL (Intellifont)";
                        break;

                    case format_Unicode:
                        codeDesc = "3: Unicode (TrueType)";
                        break;

                    default:
                        codeDesc = format.ToString() + ": unknown";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Format",
                    "",
                    codeDesc);

                //------------------------------------------------------------//

                offset++;

                byteVal = buf[offset];

                switch (byteVal)
                {
                    case 0:
                        codeDesc = PCLSymSetTypes.getDescStd (
                                       (Int32) PCLSymSetTypes.eIndex.Bound_7bit);
                        break;

                    case 1:
                        codeDesc = PCLSymSetTypes.getDescStd (
                                       (Int32) PCLSymSetTypes.eIndex.Bound_8bit);
                        break;

                    case 2:
                        codeDesc = PCLSymSetTypes.getDescStd (
                                       (Int32) PCLSymSetTypes.eIndex.Bound_PC8);
                        break;

                    case 3:
                        codeDesc = PCLSymSetTypes.getDescStd (
                                       (Int32) PCLSymSetTypes.eIndex.Bound_16bit);
                        break;

                    default:
                        codeDesc = byteVal + ": Unknown";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Symbol Set Type",
                    "",
                    codeDesc);

                //------------------------------------------------------------//

                offset++;

                firstCode = (UInt16)((buf[offset] * 256) + buf[offset + 1]);

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "First Code",
                    "",
                    firstCode.ToString () +
                    " (0x" + firstCode.ToString ("x") + ")");

                //------------------------------------------------------------//

                offset += 2;

                lastCode = (UInt16)((buf[offset] * 256) + buf[offset + 1]);

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Last Code",
                    "",
                    lastCode.ToString () +
                    " (0x" + lastCode.ToString ("x") + ")");

                codeCt     = lastCode - firstCode + 1;
                mapLenCalc = codeCt * 2;

                //------------------------------------------------------------//

                offset += 2;

                _parseFontHddrPCL.ParseSegs.decodeCharCompReq (
                    false,
                    (format == format_MSL),
                    true,
                    fileOffset,
                    buf,
                    offset,
                    linkData,
                    options,
                    table);

                //------------------------------------------------------------//

                bufRem = bufRem - lenMin;
                bufOffset = bufOffset + lenMin;
                binDataRem = binDataRem - lenMin;

                //------------------------------------------------------------//
                //                                                            //
                // The remaining bytes are the mappings.                      //
                //                                                            //
                //------------------------------------------------------------//

                if (binDataRem > 0)
                {
                    offset = bufOffset;

                    if (mapLen != mapLenCalc)
                    {
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.MsgWarning,
                            table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "*** Warning ***",
                            "",
                            "Symbol Map size of " + mapLen +
                            " inconsistent with");

                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.MsgWarning,
                            table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "*** Warning ***",
                            "",
                            "calculated size " + mapLenCalc +
                            " for " + codeCt + " code-points");

                        dataOK = false;
                        contType = PrnParseConstants.eContType.PCLDownload;

                        linkData.setContData(contType,
                                              0,
                                              -bufRem,
                                              binDataRem,
                                              true,
                                              0x00,
                                              0x00);
                    }
                    else
                    {
                        linkData.EntryNo = firstCode;

                        decodeDefineSymbolSetMap (binDataRem,
                                                  fileOffset,
                                                  buf,
                                                  ref bufRem,
                                                  ref bufOffset,
                                                  linkData,
                                                  options,
                                                  table);
                    }
                }
            }

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e D e f i n e S y m b o l S e t M a p                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the 'mapping array' part of the associated data part of //
        // the Define Symbol Set command.                                     //
        //                                                                    //
        // When this function is called, we know that the map size is as      //
        // calculated, and it must therefore be an even number of bytes,      //
        // since each entry is 2 bytes long.                                  //
        // We still have to cater for a split entry, since the buffer may end //
        // with half an entry.                                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean decodeDefineSymbolSetMap(
            Int32 binDataLen,
            Int32 fileOffset,
            Byte[] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            PrnParseLinkData linkData,
            PrnParseOptions options,
            DataTable table)
        {
            const Int32 lineMax = 16;

            PrnParseConstants.eContType contType;

            Boolean dataOK = true;

            Int32 lineLen,
                  offset;

            Int32 mapBegin,
                  mapEnd,
                  mapSize;

            Int32 binDataRem;

            String codeDesc = "",
                   textDesc;

            Int32 codeStart,
                  codeCount;

            Int32 analysisLevel;

            Boolean showBinData,
                    all_ffs;

            PrnParseConstants.eOptOffsetFormats indxOffsetFormat;

            analysisLevel = linkData.AnalysisLevel;
            codeStart     = linkData.EntryNo;
            showBinData = options.FlagPCLMiscBinData;

            indxOffsetFormat = options.IndxGenOffsetFormat;

            offset = bufOffset;

            //----------------------------------------------------------------//
            
            binDataRem = binDataLen;

            if (binDataRem > bufRem)
                mapSize = (bufRem / lineMax) * lineMax;
            else
                mapSize = binDataRem;

            codeCount = mapSize >> 1;    // make sure we process byte pairs
            mapSize   = codeCount << 1;

            PrnParseCommon.addDataRow (
                PrnParseRowTypes.eType.DataBinary,
                table,
                PrnParseConstants.eOvlShow.None,
                indxOffsetFormat,
                fileOffset + bufOffset,
                analysisLevel,
                "PCL Binary",
                "[ " + mapSize + " bytes ]",
                "Define Symbol Set mapping (undefined rows omitted)");

            if (showBinData)
            {
                PrnParseData.processBinary(
                    table,
                    PrnParseConstants.eOvlShow.None,
                    buf,
                    fileOffset,
                    bufOffset,
                    mapSize,
                    "",
                    showBinData,
                    false,
                    true,
                    indxOffsetFormat,
                    analysisLevel);
            }

            //------------------------------------------------------------//

            for (Int32 i = 0; i < mapSize; i += lineMax)
            {
                if ((mapSize - i) < lineMax)
                    lineLen = mapSize - i;
                else
                    lineLen = lineMax;

                mapBegin = codeStart + (i / 2);
                mapEnd = mapBegin + (lineLen / 2) - 1;

                textDesc = "0x" + mapBegin.ToString ("x4") +
                           "->" + mapEnd.ToString ("x4");

                all_ffs = PrnParseCommon.byteArrayPairToHexString (
                              buf,
                              offset + i,
                              lineLen,
                              ref codeDesc);

                if (!all_ffs)
                {
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Mapping",
                        textDesc,
                        "[ " + codeDesc + "]");
                }
            }

            codeStart += codeCount;

            //----------------------------------------------------//

            bufRem = bufRem - mapSize;
            bufOffset = bufOffset + mapSize;
            binDataRem = binDataRem - mapSize;

            if (binDataRem > 0)
            {
                contType =
                    PrnParseConstants.eContType.PCLDefineSymbolSetMap;

                linkData.EntryNo = codeStart;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataRem,
                                      true,
                                      0x00,
                                      0x00);
            }
            else
            {
                linkData.resetContData ();
            }


            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e D i t h e r M a t r i x                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the associated data part of the Download Dither Matrix  //
        // command.                                                           //
        // Format is <esc>*m#W[binary]                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean decodeDitherMatrix(
            Int32 binDataLen,
            Int32 fileOffset,
            Byte[] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            PrnParseLinkData linkData,
            PrnParseOptions options,
            DataTable table)
        {
            const Int32 lenMin = 2;
            const Int32 lenMax = 32767;

            PrnParseConstants.eContType contType;

            Int32 binDataRem;

            Boolean dataOK;

            //----------------------------------------------------------------//
            //                                                                //
            // Initialise.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            dataOK = true;

            binDataRem = binDataLen;

            if ((binDataLen < lenMin) || (binDataLen > lenMax))
            {
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.MsgWarning,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "*** Warning ***",
                    "",
                    "Download Dither Matrix length < " + lenMin +
                    " or > " + lenMax);

                dataOK = false;
                contType = PrnParseConstants.eContType.PCLDownload;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataRem,
                                      true,
                                      0x00,
                                      0x00);
            }
            else if (lenMin > bufRem)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Not all of binary header data is in current buffer.        //
                // Initiate continuation action.                              //
                //                                                            //
                //------------------------------------------------------------//

                contType = PrnParseConstants.eContType.PCLDitherMatrix;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataRem,
                                      true,
                                      0x00,
                                      0x00);
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // The first two bytes define the format and number of planes.//
                //                                                            //
                //------------------------------------------------------------//

                Int32 offset;

                Int32 planeCt;

                Byte byteVal;

                String codeDesc;

                Int32 analysisLevel;

                Boolean showBinData;

                PrnParseConstants.eOptOffsetFormats indxOffsetFormat;

                analysisLevel = linkData.AnalysisLevel;
                showBinData = options.FlagPCLMiscBinData;

                indxOffsetFormat = options.IndxGenOffsetFormat;

                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    PrnParseRowTypes.eType.DataBinary,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    indxOffsetFormat,
                    fileOffset + bufOffset,
                    analysisLevel,
                    "PCL Binary",
                    "[ " + lenMin + " bytes ]",
                    "Download Dither Matrix header");

                if (showBinData)
                {
                    PrnParseData.processBinary(
                        table,
                        PrnParseConstants.eOvlShow.None,
                        buf,
                        fileOffset,
                        bufOffset,
                        lenMin,
                        "",
                        showBinData,
                        false,
                        true,
                        indxOffsetFormat,
                        analysisLevel);
                }

                //------------------------------------------------------------//

                offset = bufOffset;

                byteVal = buf[offset];

                switch (byteVal)
                {
                    case 0:
                        codeDesc = "0";
                        break;

                    default:
                        codeDesc = byteVal + ": Unknown";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Format",
                    "",
                    codeDesc);

                //------------------------------------------------------------//

                offset++;

                planeCt = buf[offset];

                switch (planeCt)
                {
                    case 1:
                        codeDesc = "1: one matrix for all primaries";
                        break;

                    case 3:
                        codeDesc = "3: one matrix for each primary";
                        break;

                    default:
                        codeDesc = planeCt + ": Unknown";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Number of Planes",
                    "",
                    codeDesc);

                linkData.EntryCt = planeCt;
                linkData.EntryNo = 1;

                //------------------------------------------------------------//

                bufRem = bufRem - lenMin;
                bufOffset = bufOffset + lenMin;
                binDataRem = binDataRem - lenMin;

                //------------------------------------------------------------//
                //                                                            //
                // The remaining bytes are the cell and matrix definitions    //
                // for each plane.                                            //
                //                                                            //
                //------------------------------------------------------------//

                if (binDataRem > 0)
                {
                    decodeDitherMatrixPlane (binDataRem,
                                             fileOffset,
                                             buf,
                                             ref bufRem,
                                             ref bufOffset,
                                             linkData,
                                             options,
                                             table);
                }
            }

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e D i t h e r M a t r i x P l a n e                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the data for one plane of the Download Dither Matrix    //
        // command.                                                           //
        // The data for each plane is of the format:                          //
        //  bytes 0-1   matrix height (h pixels) as unsigned 16-bit integer   //
        //  bytes 2-3   matrix width  (w pixels) as unsigned 16-bit integer   //
        //  bytes 4...  matrix definition; size h*w bytes                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean decodeDitherMatrixPlane(
            Int32 binDataLen,
            Int32 fileOffset,
            Byte[] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            PrnParseLinkData linkData,
            PrnParseOptions options,
            DataTable table)
        {
            const Int32 lenMin = 4;

            PrnParseConstants.eContType contType;

            Int32 binDataRem;

            Boolean dataOK;

            //----------------------------------------------------------------//
            //                                                                //
            // Initialise.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            dataOK = true;

            binDataRem = binDataLen;

            if (lenMin > bufRem)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Not all of binary header data is in current buffer.        //
                // Initiate continuation action.                              //
                //                                                            //
                //------------------------------------------------------------//

                contType = PrnParseConstants.eContType.PCLDitherMatrixPlane;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataRem,
                                      true,
                                      0x00,
                                      0x00);
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // The first four bytes define the height and width of the    //
                // matrix.                                                    //
                //                                                            //
                //------------------------------------------------------------//

                Int32 offset;

                Int32 planeNo,
                      planeLen;

                UInt16 height,
                       width;

                Int32 analysisLevel;

                Boolean showBinData;

                PrnParseConstants.eOptOffsetFormats indxOffsetFormat;

                analysisLevel = linkData.AnalysisLevel;
                showBinData = options.FlagPCLMiscBinData;

                indxOffsetFormat = options.IndxGenOffsetFormat;

                planeNo = linkData.EntryNo;

                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    PrnParseRowTypes.eType.DataBinary,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    indxOffsetFormat,
                    fileOffset + bufOffset,
                    analysisLevel,
                    "PCL Binary",
                    "[ " + lenMin + " bytes ]",
                    "Download Dither Matrix plane " + planeNo + " header");

                if (showBinData)
                {
                    PrnParseData.processBinary(
                        table,
                        PrnParseConstants.eOvlShow.None,
                        buf,
                        fileOffset,
                        bufOffset,
                        lenMin,
                        "",
                        showBinData,
                        false,
                        true,
                        indxOffsetFormat,
                        analysisLevel);
                }

                //------------------------------------------------------------//

                offset = bufOffset;

                height = (UInt16)((buf [offset] * 256) + buf [offset + 1]); 

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Matrix Height",
                    "Plane " + planeNo,
                    height + " pixels");

                //------------------------------------------------------------//

                offset += 2;

                width = (UInt16)((buf [offset] * 256) + buf [offset + 1]); 

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Matrix Width",
                    "Plane " + planeNo,
                    width + " pixels");

                //------------------------------------------------------------//

                bufRem = bufRem - lenMin;
                bufOffset = bufOffset + lenMin;
                binDataRem = binDataRem - lenMin;

                //------------------------------------------------------------//
                //                                                            //
                // The remaining bytes are the matrix threshold definitions   //
                // for the plane.                                             //
                //                                                            //
                //------------------------------------------------------------//

                planeLen = height * width;

                if (planeLen > binDataRem)
                {
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgWarning,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "*** Warning ***",
                        "",
                        "Download Dither Matrix " + planeNo + " length " +
                        planeLen + " > remainder size " + binDataRem);

                    dataOK = false;
                    contType = PrnParseConstants.eContType.PCLDownload;

                    linkData.setContData (contType,
                                          0,
                                          -bufRem,
                                          binDataRem,
                                          true,
                                          0x00,
                                          0x00);
                }
                else if (binDataRem > 0)
                {
                    linkData.EntrySz1 = height;
                    linkData.EntrySz2 = width;
                    linkData.EntryRem = height * width;

                    decodeDitherMatrixPlaneData (binDataRem,
                                                 fileOffset,
                                                 buf,
                                                 ref bufRem,
                                                 ref bufOffset,
                                                 linkData,
                                                 options,
                                                 table);
                }
            }

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e D i t h e r M a t r i x P l a n e D a t a              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the Dither Matrix data for the current plane.           //
        //                                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean decodeDitherMatrixPlaneData (
            Int32 binDataLen,
            Int32 fileOffset,
            Byte[] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            PrnParseLinkData linkData,
            PrnParseOptions options,
            DataTable table)
        {
            PrnParseConstants.eContType contType;

            Boolean dataOK = true;

            Int32 dataSize,
                  planeNo,
                  offset,
                  matrixRem,
                  matrixHeight,
                  matrixWidth,
                  rows;

            Int32 binDataRem;

            Int32 analysisLevel;

            Boolean showBinData;

            PrnParseConstants.eOptOffsetFormats indxOffsetFormat;

            PrnParseConstants.eOptCharSetSubActs indxCharSetSubAct =
                PrnParseConstants.eOptCharSetSubActs.Hex;
            PrnParseConstants.eOptCharSets indxCharSetName =
                PrnParseConstants.eOptCharSets.ASCII;
            Int32 valCharSetSubCode = 0x20;

            analysisLevel = linkData.AnalysisLevel;

            indxOffsetFormat = options.IndxGenOffsetFormat;
            showBinData = options.FlagPCLMiscBinData;

            analysisLevel = linkData.AnalysisLevel;

            indxOffsetFormat = options.IndxGenOffsetFormat;
            showBinData = options.FlagPCLMiscBinData;

            options.getOptCharSet(ref indxCharSetName,
                                   ref indxCharSetSubAct,
                                   ref valCharSetSubCode);

            planeNo = linkData.EntryNo;

            matrixHeight = linkData.EntrySz1;
            matrixWidth  = linkData.EntrySz2;

            matrixRem = linkData.EntryRem;

            offset = bufOffset;

            //----------------------------------------------------------------//

            binDataRem = binDataLen;

            if (matrixRem > bufRem)
                dataSize = (bufRem / matrixWidth) * matrixWidth;
            else
                dataSize = matrixRem;

            rows = dataSize / matrixWidth;

            PrnParseCommon.addDataRow (
                PrnParseRowTypes.eType.DataBinary,
                table,
                PrnParseConstants.eOvlShow.None,
                indxOffsetFormat,
                fileOffset + bufOffset,
                analysisLevel,
                "PCL Binary",
                "[ " + dataSize + " bytes ]",
                "Download Dither Matrix plane " + planeNo + " data");

            if (showBinData)
            {
                PrnParseData.processBinary(
                    table,
                    PrnParseConstants.eOvlShow.None,
                    buf,
                    fileOffset,
                    bufOffset,
                    dataSize,
                    "",
                    showBinData,
                    false,
                    true,
                    indxOffsetFormat,
                    analysisLevel);
            }

            //------------------------------------------------------------//

            for (Int32 i = 0; i < rows; i++)
            {
                PrnParseData.processBinary (
                    table,
                    PrnParseConstants.eOvlShow.None,
                    buf,
                    fileOffset,
                    bufOffset,
                    matrixWidth,
                    "Matrix row",
                    true,
                    false,
                    false,
                    indxOffsetFormat,
                    analysisLevel);
            }

            //----------------------------------------------------//

            bufRem = bufRem - dataSize;
            bufOffset = bufOffset + dataSize;
            binDataRem = binDataRem - dataSize;

            matrixRem -= dataSize;
            
            if (matrixRem > 0)
            {
                linkData.EntryRem = matrixRem;

                contType =
                    PrnParseConstants.eContType.PCLDitherMatrixPlaneData; 

                linkData.setContData (contType,
                                      0,
                                      -bufRem,
                                      binDataRem,
                                      true,
                                      0x00,
                                      0x00);
            }
            else if (binDataRem > 0)
            {
                linkData.EntryNo++;

                contType =
                    PrnParseConstants.eContType.PCLDitherMatrixPlane;

                linkData.setContData (contType,
                                      0,
                                      -bufRem,
                                      binDataRem,
                                      true,
                                      0x00,
                                      0x00);
            }
            else
            {
                linkData.resetContData ();
            }

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e D r i v e r C o n f i g u r a t i o n                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the associated data part of the Driver Configuration    //
        // command.                                                           //
        // Format is <esc>*o#W[binary-data]                                   //
        // Only have descriptins for early Colour LaserJet formats, not for   //
        // inkjet formats.                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean decodeDriverConfiguration (
            Int32 binDataLen,
            Int32 fileOffset,
            Byte [] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            PrnParseLinkData linkData,
            PrnParseOptions options,
            DataTable table)
        {
            const Int32 lenMin = 2;
            const Int32 lenMax = 100;   // arbitrary; less than buffer size 

            PrnParseConstants.eContType contType;

            Int32 binDataRem;

            Boolean dataOK;

            //----------------------------------------------------------------//
            //                                                                //
            // Initialise.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            dataOK = true;

            binDataRem = binDataLen;

            if ((binDataLen < lenMin) || (binDataLen > lenMax))
            {
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.MsgWarning,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "*** Warning ***",
                    "",
                    "Driver Configuration length < " + lenMin +
                    " or > " + lenMax);

                dataOK = false;
                contType = PrnParseConstants.eContType.PCLDownload;

                linkData.setContData (contType,
                                      0,
                                      -bufRem,
                                      binDataRem,
                                      true,
                                      0x00,
                                      0x00);
            }
            else if (binDataRem > bufRem)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Not all of binary data is in current buffer.               //
                // Initiate continuation action.                              //
                //                                                            //
                //------------------------------------------------------------//

                contType = PrnParseConstants.eContType.PCLDriverConfiguration;

                linkData.setContData (contType,
                                      0,
                                      -bufRem,
                                      binDataRem,
                                      true,
                                      0x00,
                                      0x00);
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // The first six bytes are common to both 'short' and 'long'  //
                // forms of the data.                                         //
                //                                                            //
                //------------------------------------------------------------//

                Int32 offset,
                      argLen;

                Byte deviceId;
                Byte functionIndex;
                Byte colourTreatment;

                String codeDesc;

                Int32 analysisLevel;

                Boolean showBinData;

                PrnParseConstants.eOptOffsetFormats indxOffsetFormat;

                analysisLevel = linkData.AnalysisLevel;
                showBinData = options.FlagPCLMiscBinData;

                indxOffsetFormat = options.IndxGenOffsetFormat;

                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    PrnParseRowTypes.eType.DataBinary,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    indxOffsetFormat,
                    fileOffset + bufOffset,
                    analysisLevel,
                    "PCL Binary",
                    "[ " + lenMin + " bytes ]",
                    "Driver Configuration header");

                if (showBinData)
                {
                    PrnParseData.processBinary (
                        table,
                        PrnParseConstants.eOvlShow.None,
                        buf,
                        fileOffset,
                        bufOffset,
                        lenMin,
                        "",
                        showBinData,
                        false,
                        true,
                        indxOffsetFormat,
                        analysisLevel);
                }

                //------------------------------------------------------------//

                offset = bufOffset;

                deviceId = buf [offset];

                switch (deviceId)
                {
                    case 6:
                        codeDesc = "6: Colour LaserJet";
                        break;

                    case 8:
                        codeDesc = "8: Colour LaserJet 4500";
                        break;

                    default:
                        codeDesc = deviceId.ToString () + ": unknown";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Device Id",
                    "",
                    codeDesc);

                //------------------------------------------------------------//

                offset++;

                functionIndex = buf [offset];

                switch (functionIndex)
                {
                    case 4:
                        codeDesc = "4: Select Colour Treatment";
                        break;

                    default:
                        codeDesc = functionIndex.ToString () + ": unknown";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Function Index",
                    "",
                    codeDesc);

                //------------------------------------------------------------//
                //                                                            //
                // Any remaining bytes are 'arguments'.                       //
                //                                                            //
                //------------------------------------------------------------//

                bufRem = bufRem - lenMin;
                bufOffset = bufOffset + lenMin;

                argLen = binDataRem - lenMin;

                if (argLen > 0)
                {
                    PrnParseCommon.addDataRow (
                        PrnParseRowTypes.eType.DataBinary,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        indxOffsetFormat,
                        fileOffset + bufOffset,
                        analysisLevel,
                        "PCL Binary",
                        "[ " + argLen + " bytes ]",
                        "Driver Configuration arguments");

                    if (showBinData)
                    {
                        PrnParseData.processBinary (
                            table,
                            PrnParseConstants.eOvlShow.None,
                            buf,
                            fileOffset,
                            bufOffset,
                            argLen,
                            "",
                            showBinData,
                            false,
                            true,
                            indxOffsetFormat,
                            analysisLevel);
                    }

                    if ((argLen == 1) && (functionIndex == 4))
                    {
                        offset++;
        
                        colourTreatment = buf [offset];

                        switch (colourTreatment)
                        {
                            case 3:
                                codeDesc = "3: Vivid Graphics";
                                break;

                            case 6:
                                codeDesc = "6: Screen Match";
                                break;

                            default:
                                codeDesc = colourTreatment.ToString () + ": unknown";
                                break;
                        }

                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLDecode,
                            table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "Colour Treatment",
                            "",
                            codeDesc);
                    }
                }

                //------------------------------------------------------------//

                bufRem = bufRem - argLen;
                bufOffset = bufOffset + argLen;
            }

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e E m b e d d e d P M L                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the associated data part of the sequences which may (or //
        // not) be PML sequences (introduced with "PML ").                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean decodeEmbeddedPML(Int32 binDataLen,
                                         Int32 fileOffset,
                                         Byte[] buf,
                                         ref Int32 bufRem,
                                         ref Int32 bufOffset,
                                         PrnParseLinkData linkData,
                                         PrnParseOptions options,
                                         DataTable table)
        {
            const Int32 lenMin = 6;
            const Int32 lenMax = 2047;   // must be less than buffer size 

            PrnParseConstants.eContType contType;

            Boolean dataOK = true;

            Int32 binDataRem;

            //----------------------------------------------------------------//
            //                                                                //
            // Initialise.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            binDataRem = binDataLen;

            if ((binDataLen < lenMin) || (binDataLen > lenMax))
            {
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.MsgWarning,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "*** Warning ***",
                    "",
                    "Embedded PML data length < " + lenMin +
                    " or > " + lenMax);

                dataOK = false;
                contType = PrnParseConstants.eContType.PCLDownload;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataRem,
                                      true,
                                      0x00,
                                      0x00);
            }
            else if (binDataRem > bufRem)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Not all of string identifier is in current buffer.         //
                // Initiate continuation action.                              //
                //                                                            //
                //------------------------------------------------------------//

                contType = PrnParseConstants.eContType.PCLEmbeddedPML;

                linkData.setContData(
                    contType,
                    0,
                    -bufRem,
                    binDataRem,
                    true,
                    0x20,
                    0x20);
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // All of embedded data is in current buffer.                 //
                // Display details.                                           //
                //                                                            //
                //------------------------------------------------------------//

                Int32 analysisLevel;

                Boolean showBinData;

                PrnParseConstants.eOptOffsetFormats indxOffsetFormat;

                analysisLevel = linkData.AnalysisLevel;
                showBinData = options.FlagPCLMiscBinData;

                indxOffsetFormat = options.IndxGenOffsetFormat;

                PrnParseData.processBinary(
                    table,
                    PrnParseConstants.eOvlShow.None,
                    buf,
                    fileOffset,
                    bufOffset,
                    binDataRem,
                    "PCL Binary",
                    showBinData,
                    false,
                    true,
                    indxOffsetFormat,
                    analysisLevel);

                if ((binDataLen > 4) &&
                    ((buf[bufOffset] == 0x50) &&           // P
                     (buf[bufOffset + 1] == 0x4d) &&       // M
                     (buf[bufOffset + 2] == 0x4c) &&       // L
                     (buf[bufOffset + 3] == 0x20)))        // space
                {
                    PrnParsePML parsePML = new PrnParsePML();

                    dataOK = parsePML.processPMLEmbedded(buf,
                                                         fileOffset,
                                                         binDataLen,
                                                         bufOffset,
                                                         linkData,
                                                         options,
                                                         table);
                }

                bufRem = bufRem - binDataLen;
                bufOffset = bufOffset + binDataLen;
            }

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e E s c E n c T e x t                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the associated data part of the Escapement Encapsulated //
        // Text command.                                                      //
        // Format is <esc>&p#W[binary]                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean decodeEscEncText(
            Int32 binDataLen,
            Int32 fileOffset,
            Byte[] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            PrnParseLinkData linkData,
            PrnParseOptions options,
            DataTable table)
        {
            const Int32 lenMin = 1;
            const Int32 lenMax = 32767;

            PrnParseConstants.eContType contType;

            Int32 binDataRem;

            Boolean dataOK;

            //----------------------------------------------------------------//
            //                                                                //
            // Initialise.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            dataOK = true;

            binDataRem = binDataLen;

            if ((binDataLen < lenMin) || (binDataLen > lenMax))
            {
                PrnParseCommon.addTextRow 
                    (PrnParseRowTypes.eType.MsgWarning,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "*** Warning ***",
                    "",
                    "Escapement Encapsulated Text length < " + lenMin +
                    " or > " + lenMax);

                dataOK = false;
                contType = PrnParseConstants.eContType.PCLDownload;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataRem,
                                      true,
                                      0x00,
                                      0x00);
            }
            else if (lenMin > bufRem)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Not all of binary header data is in current buffer.        //
                // Initiate continuation action.                              //
                //                                                            //
                //------------------------------------------------------------//

                contType = PrnParseConstants.eContType.PCLDefineSymbolSet;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataRem,
                                      true,
                                      0x00,
                                      0x00);
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // The first byte is the format - only one value is defined.  //
                //                                                            //
                //------------------------------------------------------------//

                Int32 offset;

                Int32 tripletCt;

                Byte byteVal;

                String codeDesc;

                Int32 analysisLevel;

                Boolean showBinData;

                PrnParseConstants.eOptOffsetFormats indxOffsetFormat;

                analysisLevel = linkData.AnalysisLevel;
                showBinData = options.FlagPCLMiscBinData;

                indxOffsetFormat = options.IndxGenOffsetFormat;

                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    PrnParseRowTypes.eType.DataBinary,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    indxOffsetFormat,
                    fileOffset + bufOffset,
                    analysisLevel,
                    "PCL Binary",
                    "[ " + lenMin + " bytes ]",
                    "Escapement Encapsulated Text header");

                if (showBinData)
                {
                    PrnParseData.processBinary(
                        table,
                        PrnParseConstants.eOvlShow.None,
                        buf,
                        fileOffset,
                        bufOffset,
                        lenMin,
                        "",
                        showBinData,
                        false,
                        true,
                        indxOffsetFormat,
                        analysisLevel);
                }

                //------------------------------------------------------------//

                offset = bufOffset;

                byteVal = buf[offset];

                switch (byteVal)
                {
                    case 0:
                        codeDesc = "0: UBYTE";
                        break;

                    default:
                        codeDesc = byteVal + ": Unknown";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Format",
                    "",
                    codeDesc);

                //------------------------------------------------------------//

                bufRem = bufRem - 1;
                bufOffset = bufOffset + 1;
                binDataRem = binDataRem - 1;

                //------------------------------------------------------------//
                //                                                            //
                // The remaining bytes are the character & escapement         //
                // triplets.                                                  //
                //                                                            //
                //------------------------------------------------------------//

                if (binDataRem > 0)
                {
                    offset = bufOffset;

                    tripletCt = binDataRem / 3;

                    if (binDataRem - (tripletCt * 3) != 0)
                    {
                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.MsgWarning,
                            table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            "*** Warning ***",
                            "",
                            "Encapsulated data size " + binDataRem +
                            " is not a multiple of 3");

                        dataOK = false;
                        contType = PrnParseConstants.eContType.PCLDownload;

                        linkData.setContData(contType,
                                              0,
                                              -bufRem,
                                              binDataRem,
                                              true,
                                              0x00,
                                              0x00);
                    }
                    else
                    {
                        decodeEscEncTextData (binDataRem,
                                              fileOffset,
                                              buf,
                                              ref bufRem,
                                              ref bufOffset,
                                              linkData,
                                              options,
                                              table);
                    }
                }
            }

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e E s c E n c T e x t D a t a                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the triplets of data describing the escapement          //
        // encapsulated text.                                                 //
        //                                                                    //
        // Each triplet is of the format:                                     //
        //  byte  0     text character (range 0x00-0xff)                      //
        //  bytes 1-2   escapement value as signed 16-bit integer             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean decodeEscEncTextData(
            Int32 binDataLen,
            Int32 fileOffset,
            Byte[] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            PrnParseLinkData linkData,
            PrnParseOptions options,
            DataTable table)
        {
            PrnParseConstants.eContType contType;

            Boolean dataOK = true;

            Int32 dataSize,
                  offset;

            Int32 binDataRem;

            String codeDesc;

            Int32 tripletCt;

            Int32 analysisLevel;

            Boolean showBinData;

            PrnParseConstants.eOptOffsetFormats indxOffsetFormat;

            PrnParseConstants.eOptCharSetSubActs indxCharSetSubAct =
                PrnParseConstants.eOptCharSetSubActs.Hex;
            PrnParseConstants.eOptCharSets indxCharSetName =
                PrnParseConstants.eOptCharSets.ASCII;
            Int32 valCharSetSubCode = 0x20;

            analysisLevel = linkData.AnalysisLevel;

            indxOffsetFormat = options.IndxGenOffsetFormat;
            showBinData = options.FlagPCLMiscBinData;

            analysisLevel = linkData.AnalysisLevel;

            indxOffsetFormat = options.IndxGenOffsetFormat;
            showBinData = options.FlagPCLMiscBinData;

            options.getOptCharSet(ref indxCharSetName,
                                   ref indxCharSetSubAct,
                                   ref valCharSetSubCode);

            offset = bufOffset;

            //----------------------------------------------------------------//

            binDataRem = binDataLen;

            if (binDataRem > bufRem)
                dataSize = (bufRem / 3) * 3;
            else
                dataSize = binDataRem;

            tripletCt = dataSize / 3;

            PrnParseCommon.addDataRow (
                PrnParseRowTypes.eType.DataBinary,
                table,
                PrnParseConstants.eOvlShow.None,
                indxOffsetFormat,
                fileOffset + bufOffset,
                analysisLevel,
                "PCL Binary",
                "[ " + dataSize + " bytes ]",
                "Escapement Encapsulated Text data");

            if (showBinData)
            {
                PrnParseData.processBinary(
                    table,
                    PrnParseConstants.eOvlShow.None,
                    buf,
                    fileOffset,
                    bufOffset,
                    dataSize,
                    "",
                    showBinData,
                    false,
                    true,
                    indxOffsetFormat,
                    analysisLevel);
            }

            //------------------------------------------------------------//

            for (Int32 i = 0; i < tripletCt; i++)
            {
                offset = bufOffset + (i * 3);

                codeDesc = PrnParseData.processByte(buf[offset],
                                                    indxCharSetSubAct,
                                                    (Byte)valCharSetSubCode,
                                                    indxCharSetName) +
                           " --> " +
                           processSint16(buf, offset + 1) +
                           " PCL units";

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Char. & advance",
                    "",
                    codeDesc);
            }

            //----------------------------------------------------//

            bufRem = bufRem - dataSize;
            bufOffset = bufOffset + dataSize;
            binDataRem = binDataRem - dataSize;

            if (binDataRem > 0)
            {
                contType =
                    PrnParseConstants.eContType.PCLEscEncTextData;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataRem,
                                      true,
                                      0x00,
                                      0x00);
            }

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e P a l e t t e C o n f i g u r a t i o n                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the associated data part of the Process Configuration   //
        // sequence.                                                          //
        // Format is <esc>*d#W[binary-data]                                   //
        // Data length = 9 + (3 * entry count)                                //
        // Used by DeskJet 660c; DeskJet 850c                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean decodePaletteConfiguration(
            Int32 binDataLen,
            Int32 fileOffset,
            Byte[] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            PrnParseLinkData linkData,
            PrnParseOptions options,
            DataTable table)
        {
            const String itemName = "Palette Entry";

            const Int32 fixedLen = 9;
            const Int32 itemLen = 3;

            Int32 lenMin = fixedLen + itemLen;
            Int32 lenMax = fixedLen + (itemLen * 256);   // must be less than buffer size 

            PrnParseConstants.eContType contType;

            Boolean dataOK = true;

            //----------------------------------------------------------------//
            //                                                                //
            // Initialise.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            dataOK = true;

            if ((binDataLen < lenMin) || (binDataLen > lenMax))
            {
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.MsgWarning,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "*** Warning ***",
                    "",
                    "Palette Configuration length < " + lenMin +
                    " or > " + lenMax);

                dataOK = false;
                contType = PrnParseConstants.eContType.PCLDownload;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataLen,
                                      true,
                                      0x00,
                                      0x00);
            }
            else if (binDataLen > bufRem)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Not all of binary data is in current buffer.               //
                // Initiate continuation action.                              //
                //                                                            //
                //------------------------------------------------------------//

                contType = PrnParseConstants.eContType.PCLPaletteConfiguration;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataLen,
                                      true,
                                      0x00,
                                      0x00);
            }
            else
            {
                Int32 offset;
                Int32 calcLen;

                Byte byteVal;

                Int32 itemCt;

                String codeDesc;

                Int32 analysisLevel;

                Boolean showBinData;

                PrnParseConstants.eOptOffsetFormats indxOffsetFormat;

                analysisLevel = linkData.AnalysisLevel;
                showBinData = options.FlagPCLMiscBinData;

                indxOffsetFormat = options.IndxGenOffsetFormat;

                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    PrnParseRowTypes.eType.DataBinary,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    indxOffsetFormat,
                    fileOffset + bufOffset,
                    analysisLevel,
                    "PCL Binary",
                    "[ " + binDataLen + " bytes ]",
                    "Palette Configuration header");

                if (showBinData)
                {
                    PrnParseData.processBinary(
                        table,
                        PrnParseConstants.eOvlShow.None,
                        buf,
                        fileOffset,
                        bufOffset,
                        binDataLen,
                        "",
                        showBinData,
                        false,
                        true,
                        indxOffsetFormat,
                        analysisLevel);
                }

                //------------------------------------------------------------//

                offset = bufOffset;

                byteVal = buf[offset];

                switch (byteVal)
                {
                    case 1:
                        codeDesc = "1: Device Dependent RGB";
                        break;

                    default:
                        codeDesc = byteVal.ToString() + ": unknown";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Colour Space",
                    "",
                    codeDesc);

                //------------------------------------------------------------//

                offset++;

                byteVal = buf[offset];

                switch (byteVal)
                {
                    case 1:
                        codeDesc = "1: Unsigned 8-bit Integer Data";
                        break;

                    default:
                        codeDesc = byteVal.ToString() + ": unknown";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Format",
                    "",
                    codeDesc);

                //------------------------------------------------------------//

                offset++;

                byteVal = buf[offset];

                itemCt = byteVal + 1;

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Last Palette Entry",
                    "",
                    byteVal.ToString());

                //------------------------------------------------------------//

                offset++;

                byteVal = buf[offset];

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Minimum Range",
                    "Red",
                    byteVal.ToString());

                //------------------------------------------------------------//

                offset++;

                byteVal = buf[offset];

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Maximum Range",
                    "Red",
                    byteVal.ToString());

                //------------------------------------------------------------//

                offset++;

                byteVal = buf[offset];

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Minimum Range",
                    "Green",
                    byteVal.ToString());

                //------------------------------------------------------------//

                offset++;

                byteVal = buf[offset];

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Maximum Range",
                    "Green",
                    byteVal.ToString());

                //-----------------------------------------------------------//

                offset++;

                byteVal = buf[offset];

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Minimum Range",
                    "Blue",
                    byteVal.ToString());

                //------------------------------------------------------------//

                offset++;

                byteVal = buf[offset];

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Maximum Range",
                    "Blue",
                    byteVal.ToString());

                //------------------------------------------------------------//

                calcLen = fixedLen + (itemLen * itemCt);

                if (calcLen != binDataLen)
                {
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgError,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "*** Error ***",
                        "",
                        "actual length " + binDataLen +
                        " != calculated length " + calcLen);

                    dataOK = false;
                    contType = PrnParseConstants.eContType.PCLDownload;

                    linkData.setContData(contType,
                                          0,
                                          -bufRem,
                                          binDataLen,
                                          true,
                                          0x00,
                                          0x00);

                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Decode the elements for each palette entry.            //
                    //                                                        //
                    //--------------------------------------------------------//

                    offset = bufOffset + fixedLen;

                    for (Int32 i = 0; i < itemCt; i++)
                    {
                        byteVal = buf[offset];

                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLDecode,
                            table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            itemName + " " + i,
                            "Red",
                            byteVal.ToString());

                        //----------------------------------------------------//

                        offset++;
                        
                        byteVal = buf[offset];

                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLDecode,
                            table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            itemName + " " + i,
                            "Green",
                            byteVal.ToString());

                        //----------------------------------------------------//
                        
                        offset++;

                        byteVal = buf[offset];

                        PrnParseCommon.addTextRow (
                            PrnParseRowTypes.eType.PCLDecode,
                            table,
                            PrnParseConstants.eOvlShow.None,
                            "",
                            itemName + " " + i,
                            "Blue",
                            byteVal.ToString());

                        //----------------------------------------------------//
                        
                        offset++;
                    }
                }

                //------------------------------------------------------------//

                bufRem = bufRem - binDataLen;
                bufOffset = bufOffset + binDataLen;
            }

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e U s e r D e f i n e d P a t t e r n                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the associated data part of the Configure Image Data    //
        // command.                                                           //
        // Format is <esc>*c#W[binary-data]                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean decodeUserDefinedPattern(
            Int32 binDataLen,
            Int32 fileOffset,
            Byte[] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            PrnParseLinkData linkData,
            PrnParseOptions options,
            DataTable table)
        {
            Int32 lenMin = 6;
            const Int32 lenMax = 32767;

            PrnParseConstants.eContType contType;

            Int32 binDataRem;

            Boolean dataOK;

            //----------------------------------------------------------------//
            //                                                                //
            // Initialise.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            if (buf[bufOffset] == 20)
                lenMin = 12;
            else
                lenMin = 8;

            dataOK = true;

            binDataRem = binDataLen;

            if ((binDataLen < lenMin) || (binDataLen > lenMax))
            {
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.MsgWarning,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "*** Warning ***",
                    "",
                    "User Defined Pattern length < " + lenMin +
                    " or > " + lenMax);

                dataOK = false;
                contType = PrnParseConstants.eContType.PCLDownload;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataRem,
                                      true,
                                      0x00,
                                      0x00);
            }
            else if (lenMin > bufRem)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Not all of binary header data is in current buffer.        //
                // Initiate continuation action.                              //
                //                                                            //
                //------------------------------------------------------------//

                contType = PrnParseConstants.eContType.PCLUserDefPatternHddr;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataRem,
                                      true,
                                      0x00,
                                      0x00);
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // The first eight bytes are common to both formats.          //
                //                                                            //
                //------------------------------------------------------------//

                Int32 offset;

                Int32 rowBytes = 1,
                      patternBytes;

                Byte byteVal,
                     format,
                     pixelEncoding;

                UInt16 uint16Val,
                       patternHeight,
                       patternWidth;

                Boolean badPattern = false;

                String codeDesc;

                Int32 analysisLevel;

                Boolean showBinData;

                PrnParseConstants.eOptOffsetFormats indxOffsetFormat;

                analysisLevel = linkData.AnalysisLevel;
                showBinData = options.FlagPCLMiscBinData;

                indxOffsetFormat = options.IndxGenOffsetFormat;

                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    PrnParseRowTypes.eType.DataBinary,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    indxOffsetFormat,
                    fileOffset + bufOffset,
                    analysisLevel,
                    "PCL Binary",
                    "[ " + lenMin + " bytes ]",
                    "User Defined Pattern header");

                if (showBinData)
                {
                    PrnParseData.processBinary(
                        table,
                        PrnParseConstants.eOvlShow.None,
                        buf,
                        fileOffset,
                        bufOffset,
                        lenMin,
                        "",
                        showBinData,
                        false,
                        true,
                        indxOffsetFormat,
                        analysisLevel);
                }

                //------------------------------------------------------------//

                offset = bufOffset;

                format = buf[offset];

                switch (format)
                {
                    case 0:
                        codeDesc = "0: Fixed (300 ppi) resolution monochrome";
                        break;

                    case 1:
                        codeDesc = "1: Fixed (300 ppi) resolution via palette";
                        break;

                    case 20:
                        codeDesc = "20: Resolution-specified monochrome";
                        break;
                    default:

                        codeDesc = format.ToString() + ": unknown";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Format",
                    "",
                    codeDesc);

                //------------------------------------------------------------//

                offset++;

                byteVal = buf[offset];

                switch (byteVal)
                {
                    case 0:
                        codeDesc = "0";
                        break;

                    default:
                         codeDesc = byteVal.ToString() + ": should be zero";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Continuation marker",
                    "",
                    codeDesc);

                //------------------------------------------------------------//

                offset++;

                pixelEncoding = buf[offset];

                switch (pixelEncoding)
                {
                    case 1:
                        codeDesc = "1 bit-per-pixel";
                        break;

                    case 8:
                        codeDesc = "8 bits-per-pixel";
                        break;

                    default:
                        codeDesc = pixelEncoding.ToString() + ": unknown";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Pixel Encoding",
                    "",
                    codeDesc);

                if (((format == 0) || (format == 20)) &&
                    (pixelEncoding != 1))
                {
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgWarning,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "*** Warning ***",
                        "",
                        "Pixel encoding value " + pixelEncoding +
                        " incompatible with format " + format);
                }

                //------------------------------------------------------------//

                offset++;

                byteVal = buf[offset];

                switch (byteVal)
                {
                    case 0:
                        codeDesc = "0";
                        break;

                    default:
                        codeDesc = byteVal.ToString() + ": should be zero";
                        break;
                }

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Reserved byte",
                    "",
                    codeDesc);

                //------------------------------------------------------------//

                offset++;

                patternHeight = (UInt16)((buf[offset] * 256) + buf[offset + 1]);

                codeDesc = patternHeight.ToString() + " pixels";

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Height",
                    "",
                    codeDesc);

                //------------------------------------------------------------//

                offset +=2;

                patternWidth = (UInt16)((buf[offset] * 256) + buf[offset + 1]);

                codeDesc = patternWidth.ToString() + " pixels";

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Width",
                    "",
                    codeDesc);

                //------------------------------------------------------------//

                if (format == 20)
                {
                    offset += 2;

                    uint16Val = (UInt16)((buf[offset] * 256) + buf[offset + 1]);

                    codeDesc = uint16Val.ToString() + " pixels-per-inch";

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "X resolution",
                        "",
                        codeDesc);

                    //------------------------------------------------------------//
                   
                    offset += 2;

                    uint16Val = (UInt16)((buf[offset] * 256) + buf[offset + 1]);

                    codeDesc = uint16Val.ToString() + " pixels-per-inch";

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.PCLDecode,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "Y resolution",
                        "",
                        codeDesc);

                }

                //------------------------------------------------------------//

                bufRem = bufRem - lenMin;
                bufOffset = bufOffset + lenMin;
                binDataRem = binDataRem - lenMin;

                //------------------------------------------------------------//
                //                                                            //
                // The remaining bytes are the pattern definition itself.     //
                //                                                            //
                //------------------------------------------------------------//

                if (pixelEncoding == 1)
                {
                    rowBytes = patternWidth / 8;

                    if ((patternWidth - (rowBytes * 8)) != 0)
                        rowBytes++;
                }
                else if (pixelEncoding == 8)
                {
                    rowBytes = patternWidth;
                }
                else
                {
                    badPattern = true;
                }

                if (!badPattern)
                {
                    patternBytes = rowBytes * patternHeight;

                    if (patternBytes != binDataRem)
                    {
                        badPattern = true;
                    }
                }

                if (badPattern)
                {
                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgWarning,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "*** Warning ***",
                        "",
                        "Pattern data size of " + binDataRem +
                        " bytes inconsistent with");

                    PrnParseCommon.addTextRow (
                        PrnParseRowTypes.eType.MsgWarning,
                        table,
                        PrnParseConstants.eOvlShow.None,
                        "",
                        "*** Warning ***",
                        "",
                        "height = " + patternHeight +
                        " and width = " + patternWidth +
                        " with pixel encoding = " + pixelEncoding);

                    dataOK = false;
                    contType = PrnParseConstants.eContType.PCLDownload;

                    linkData.setContData (contType,
                                          0,
                                          -bufRem,
                                          binDataRem,
                                          true,
                                          0x00,
                                          0x00);
                }
                else
                {
                //  linkData.EntryCt = patternHeight;
                    linkData.EntrySz1 = rowBytes;
                    linkData.EntryNo = 0;

                    decodeUserDefinedPatternData (binDataRem,
                                                  fileOffset,
                                                  buf,
                                                  ref bufRem,
                                                  ref bufOffset,
                                                  linkData,
                                                  options,
                                                  table);
                }
            }

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e U s e r D e f i n e d P a t t e r n D a t a            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Splits the pattern data into the data for each pattern row.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean decodeUserDefinedPatternData (
            Int32 binDataLen,
            Int32 fileOffset,
            Byte [] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            PrnParseLinkData linkData,
            PrnParseOptions options,
            DataTable table)
        {
            PrnParseConstants.eContType contType;

            Boolean dataOK = true;

            Int32 offset;

            Int32 chunkSize,
                  rowCt,
                  rowNo,
                  rowBytes;

            Int32 binDataRem;

            Int32 codeStart;

            Int32 analysisLevel;

            Boolean showBinData;

            PrnParseConstants.eOptOffsetFormats indxOffsetFormat;

            analysisLevel = linkData.AnalysisLevel;
            codeStart = linkData.EntryNo;
            showBinData = options.FlagPCLMiscBinData;

            rowBytes = linkData.EntrySz1;
       //   rowCt    = linkData.EntryCt;
            rowNo    = linkData.EntryNo;
            
            indxOffsetFormat = options.IndxGenOffsetFormat;

            offset = bufOffset;

            //----------------------------------------------------------------//

            binDataRem = binDataLen;

            if (binDataRem > bufRem)
                rowCt = bufRem / rowBytes;
            else
                rowCt = binDataRem / rowBytes;

            for (Int32 i = 0; i < rowCt; i++)
            {
                offset = bufOffset + (i * rowBytes);

                PrnParseCommon.addDataRow (
                    PrnParseRowTypes.eType.DataBinary,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    indxOffsetFormat,
                    fileOffset + offset,
                    analysisLevel,
                    "PCL Binary",
                    "[ " + rowBytes + " bytes ]",
                    "User Defined Pattern image: row " + rowNo);

                if (showBinData)
                {
                    PrnParseData.processBinary (
                        table,
                        PrnParseConstants.eOvlShow.None,
                        buf,
                        fileOffset,
                        offset,
                        rowBytes,
                        "",
                        showBinData,
                        false,
                        true,
                        indxOffsetFormat,
                        analysisLevel);
                }

                rowNo++;
            }

            //----------------------------------------------------//

            chunkSize = rowCt * rowBytes;
            bufRem = bufRem - chunkSize;
            bufOffset = bufOffset + chunkSize;
            binDataRem = binDataRem - chunkSize;

            if (binDataRem > 0)
            {
                contType =
                    PrnParseConstants.eContType.PCLUserDefPatternData;

                linkData.EntryNo = rowNo;

                linkData.setContData (contType,
                                      0,
                                      -bufRem,
                                      binDataRem,
                                      true,
                                      0x00,
                                      0x00);
            }
            else
            {
                linkData.resetContData ();
            }

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e c o d e V i e w I l l u m i n a n t                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Interprets the associated data part of the Viewing Illuminant      //
        // command.                                                           //
        // Format is <esc>*i#W[binary]                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean decodeViewIlluminant(
            Int32 binDataLen,
            Int32 fileOffset,
            Byte[] buf,
            ref Int32 bufRem,
            ref Int32 bufOffset,
            PrnParseLinkData linkData,
            PrnParseOptions options,
            DataTable table)
        {
            const Int32 lenStd = 8;

            PrnParseConstants.eContType contType;

            Int32 binDataRem;

            Boolean dataOK;

            //----------------------------------------------------------------//
            //                                                                //
            // Initialise.                                                    //
            //                                                                //
            //----------------------------------------------------------------//

            dataOK = true;

            binDataRem = binDataLen;

            if (binDataLen != lenStd)
            {
                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.MsgWarning,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "*** Warning ***",
                    "",
                    "Viewing Illuminant length != " + lenStd);

                dataOK = false;
                contType = PrnParseConstants.eContType.PCLDownload;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataRem,
                                      true,
                                      0x00,
                                      0x00);
            }
            else if (binDataRem > bufRem)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Not all of binary data is in current buffer.               //
                // Initiate continuation action.                              //
                //                                                            //
                //------------------------------------------------------------//

                contType = PrnParseConstants.eContType.PCLViewIlluminant;

                linkData.setContData(contType,
                                      0,
                                      -bufRem,
                                      binDataRem,
                                      true,
                                      0x00,
                                      0x00);
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // The data consists of a pair of floating point values,      //
                // defining the x- and y-chromaticity white points.           //
                //                                                            //
                //------------------------------------------------------------//

                Int32 offset;

                Int32 analysisLevel;

                Boolean showBinData;

                PrnParseConstants.eOptOffsetFormats indxOffsetFormat;

                analysisLevel = linkData.AnalysisLevel;
                showBinData = options.FlagPCLMiscBinData;

                indxOffsetFormat = options.IndxGenOffsetFormat;

                //------------------------------------------------------------//

                PrnParseCommon.addDataRow (
                    PrnParseRowTypes.eType.DataBinary,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    indxOffsetFormat,
                    fileOffset + bufOffset,
                    analysisLevel,
                    "PCL Binary",
                    "[ " + lenStd + " bytes ]",
                    "Viewing Illuminant data");

                if (showBinData)
                {
                    PrnParseData.processBinary(
                        table,
                        PrnParseConstants.eOvlShow.None,
                        buf,
                        fileOffset,
                        bufOffset,
                        lenStd,
                        "",
                        showBinData,
                        false,
                        true,
                        indxOffsetFormat,
                        analysisLevel);
                }

                //------------------------------------------------------------//

                offset = bufOffset;

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "X Chromaticity",
                    "White point",
                    processReal32 (buf, offset));

                //------------------------------------------------------------//

                offset+= 4;

                PrnParseCommon.addTextRow (
                    PrnParseRowTypes.eType.PCLDecode,
                    table,
                    PrnParseConstants.eOvlShow.None,
                    "",
                    "Y Chromaticity",
                    "White point",
                    processReal32 (buf, offset));

                //------------------------------------------------------------//

                bufRem    = bufRem - binDataRem;
                bufOffset = bufOffset + binDataRem;
            }

            return dataOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s S i n t 1 6                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Process 2 bytes as a Sint16 signed 16-bit integer value.           //
        //                                                                    //
        // Signed integer value.                                              //
        //                                                                    //
        // Decode the value as follows:                                       //
        //                                                                    //
        //  - The value is converted, byte by byte, to a signed integer       //
        //    (using Big-Endian byte-ordering).                               //
        //                                                                    //
        //  - This integer value is then converted to its string              //
        //    representation.                                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private String processSint16(Byte [] buf,
                                     Int32 bufOffset)
        {
            const Int32 sliceLen = 2;

            Int32 iSub,
                  iTot;

            Boolean msByte;

            String tempStr;

            iTot = 0;
            msByte = true;

            for (Int32 j = 0; j < sliceLen; j++)
            {
                iSub = buf[bufOffset + j];

                if (msByte && (iSub > 0x80))
                    iTot = iSub - 256;
                else
                    iTot = (iTot * 256) + iSub;

                msByte = false;
            }

            tempStr = iTot.ToString();

            return tempStr;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r o c e s s R e a l 3 2                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Process 4 bytes as a Real32 floating-point value.                  //
        //                                                                    //
        // Real (IEEE 32-bit single-precision floating-point) value.          //
        //                                                                    //
        // Decode the value as follows:                                       //
        //                                                                    //
        //  - The 4-byte value is converted, byte by byte, to an unsigned     //
        //    integer.                                                        //
        //                                                                    //
        //  - The resultant integer is then converted back to a byte array    //
        //    (using the host byte ordering).                                 //
        //                                                                    //
        //  - This new byte array is then converted to a floating-point       //
        //    value.                                                          //
        //                                                                    //
        //  - This value is then converted to its string representation.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private String processReal32(Byte[] buf,
                                     Int32 bufOffset)
        {
            const Int32 sliceLen = 4;

            UInt32 uiSub,
                   uiTot;

            Byte[] byteArray;

            Single f;

            String tempStr;

            uiTot = 0;

            for (Int32 j = 0; j < sliceLen; j++)
            {
                uiSub = buf[bufOffset + j];
                uiTot = (uiTot * 256) + uiSub;
            }

            byteArray = BitConverter.GetBytes(uiTot);

            f = BitConverter.ToSingle(byteArray, 0);

            tempStr = f.ToString("F6");

            return tempStr;
        }
    }
}