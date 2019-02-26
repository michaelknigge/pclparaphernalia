using System;
using System.Text;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides print-language-independent routines associated with
    /// 'parsing' of print file UTF-8 data strings.
    /// 
    /// © Chris Hutchinson 2012
    /// 
    /// </summary>

    static class PrnParseDataUTF8
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public enum eUTF8Result
        {
            success                 = 0,
            incompleteSeq           = 1,
            invalidLength           = 2,
            invalidLeadByteMark     = 3,
            invalidTrailByteMark    = 4,
            illegalSixByteSeq       = 5,
            illegalFiveByteSeq      = 6,
            overlongTwoByteSeq      = 7,
            overlongThreeByteSeq    = 8,
            overlongFourByteSeq     = 9,
            overvalueFourByteSeq    = 10,
            surrogateCodepointUTF8  = 11,
            surrogateCodepointUTF32 = 21,
            exceedsLegalMaximum     = 22
        };

        const Byte cTrailByteMask = 0xBF;
        const Byte cTrailByteMark = 0x80;

        const Int32 cMaxBMP = 0x0000FFFF,
                    cMaxUCS2 = 0x0000FFFF,
                    cMaxUCS4 = 0x7FFFFFFF,
                    cMaxUTF16 = 0x0010FFFF,
                    cMaxUTF32 = 0x0010FFFF,
                    cSurrogateHiLo = 0x0000D800,
                    cSurrogateHiHi = 0x0000DBFF,
                    cSurrogateLoLo = 0x0000DC00,
                    cSurrogateLoHi = 0x0000DFFF,
                    cUTF32HalfBase = 0x00010000,
                    cUTF32HalfMask = 0x000003FF,
                    cReplacementChar = 0x0000FFFD;

        //--------------------------------------------------------------------//
        //                                                                    //
        // Values subtracted from a buffer value during UTF-8 to UTF-16 or    //
        // UTF-32 conversion.                                                 //
        // This table contains as many values as there are UTF-8 sequence     //
        // lengths.                                                           //
        //                                                                    //
        // Each value (apart from the first (zero) value) is derived as       //
        // follows:                                                           //
        //                                                                    //
        //  (firstByteMask * (0x40 ^ N)) + (0x80 * (0x40 ^ (N-1)))            //
        //                               + (0x80 * (0x40 ^ (N-2)))            //
        //                                       . . .                        //
        //                               + (0x80 * (0x40 ^ (N-N)))            //
        // where N = number of trailing bytes.                                //
        //                                                                    //
        // The value associated with 5 trailing bytes is actually             //
        // 0x3F82082080, but the leading 6 significant bits (0x3F), which     //
        // started as the 'first byte mask' of 0xFC (and which will therefore //
        // be discarded), overflows an Int32 value.                           //
        // It is, in any event, irrelevant, since such sequences are no       //
        // longer legal UTF-8 anyway.                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

   
        static Int64 [] cOffsetsUTF8 =
        {
            0x0000000000,   // 0 trailing bytes //
            0x0000003080,   // 1 trailing byte  //
            0x00000E2080,   // 2 trailing bytes //
            0x0003C82080,   // 3 trailing bytes //
            0x00FA082080,   // 4 trailing bytes //
            0x3F82082080    // 5 trailing bytes //
        };

        //--------------------------------------------------------------------//
        //                                                                    //
        // This table shows the expected number of trailing bytes expected    //
        // for a given UTF-8 lead byte.                                       //
        // As per above, sequences with 4 or 5 trailing bytes are no longer   //
        // legal UTF-8 sequences, although they were in the original          //
        // definition.                                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Byte[] cTrailByteCountsUTF8 =
        {
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,    // lead byte 0x00 --> 0x0f //
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,    //           0x10 --> 0x1f //
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,    //           0x20 --> 0x2f //
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,    //           0x30 --> 0x3f //
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,    //           0x40 --> 0x4f //
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,    //           0x50 --> 0x5f //
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,    //           0x60 --> 0x6f //
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,    //           0x70 --> 0x7f //
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,    //           0x80 --> 0x8f //
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,    //           0x90 --> 0x9f //
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,    //           0xa0 --> 0xaf //
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,    //           0xb0 --> 0xbf //
            1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,    //           0xc0 --> 0xcf //
            1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,    //           0xd0 --> 0xdf //
            2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,    //           0xe0 --> 0xef //
            3,3,3,3,3,3,3,3,4,4,4,4,5,5,0,0     //           0xf0 --> 0xff //
        };

        //--------------------------------------------------------------------//
        //                                                                    //
        // UTF-8 is a variable-length, byte-order independant, encoding of    //
        // the Universal Character Set (UCS).                                 //
        //                                                                    //
        // Codepoints from U+00 to U+7F (i.e. the US-ASCII subset) are stored //
        // as is, in a single byte; all other Unicode characters are          //
        // represented by a sequence of bytes, each byte having (at least)    //
        // the top-bit set.                                                   //
        // Where the top bit is set (=1) in the lead byte, the number of 1    //
        // bits before the first 0 bit indicates the number of bytes in the   //
        // sequence.                                                          //
        //                                                                    //
        //    U+00000000 - U+0000007F: 0xxxxxxx                               //
        //    U+00000080 - U+000007FF: 110xxxxx 10xxxxxx                      //
        //    U+00000800 - U+0000FFFF: 1110xxxx 10xxxxxx 10xxxxxx             //
        //    U+00010000 - U+001FFFFF: 11110xxx 10xxxxxx 10xxxxxx 10xxxxxx    //
        //                                                                    //
        // Note that sequences of 5 or 6 bytes were valid in the original     //
        // definition, but are no longer legal UTF-8 sequences.               //
        // This is because the maximum legal Unicode code-point is (and will  //
        // now always be) limited to U+001FFFFF.                              //
        //                                                                    //
        // For completeness, the sequences with 4 or 5 trailing bytes are:    //
        //                                                                    //
        //    U+00200000 - U+03FFFFFF: 111110xx 10xxxxxx 10xxxxxx 10xxxxxx    //
        //                                      10xxxxxx                      //
        //    U+04000000 - U+7FFFFFFF: 1111110x 10xxxxxx 10xxxxxx 10xxxxxx    //
        //                                      10xxxxxx 10xxxxxx             //
        //                                                                    //
        // When converting to UTF-8 (from UTF-16 or UTF-32), a mask is OR-ed  //
        // into the first byte.                                               //
        // The mask (as per above) depends on how many trailing bytes there   //
        // are.                                                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        static Byte[] cLeadByteMarksUTF8 =
        {
            0x00,           // 0 trailing bytes //
            0xC0,           // 1 trailing byte  //
            0xE0,           // 2 trailing bytes //
            0xF0,           // 3 trailing bytes //
            0xF8,           // 4 trailing bytes //
            0xFC            // 5 trailing bytes //
        };
       
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

   //     static PrnParse.eParseType _parseType;

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h e c k U T F S e q                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Verifies that the indicated byte array contains a well-formed      //
        // UTF-8 character.                                                   //
        //                                                                    //
        // Legal UTF-8 sequences can be between 1 and 4 bytes in length.      //
        // Prior to RFC 3629 (November 2003), which restricts (for ever)      //
        // Unicode to values in the range U+00000000 - U+0010FFFF, UTF-8      //
        // sequences could be up to 6 bytes in length.                        //
        //                                                                    //
        // RFC 3629 provides a definition of UTF-8 in ABNF (Augmented         //
        // Backus-Naur Form), as follows:                                     //
        //                                                                    //
        //    A UTF-8 string is a sequence of octets representing a sequence  //
        //    of UCS characters.  An octet sequence is valid UTF-8 only if it //
        //    matches the following syntax, which is derived from the rules   //
        //    for encoding UTF-8 and is expressed in the ABNF of [RFC2234].   //
        //                                                                    //
        //    UTF8-octets = *( UTF8-char )                                    //
        //                                                                    //
        //    UTF8-char   = UTF8-1 /                                          //
        //                  UTF8-2 /                                          //
        //                  UTF8-3 /                                          //
        //                  UTF8-4                                            //
        //                                                                    //
        //    UTF8-1      = %x00-7F                                           //
        //                                                                    //
        //    UTF8-2      = %xC2-DF   UTF8-tail                               //
        //                                                                    //
        //    UTF8-3      = %xE0      %xA0-BF      UTF8-tail   /              //
        //                  %xE1-EC             2( UTF8-tail ) /              //
        //                  %xED      %x80-9F      UTF8-tail   /              //
        //                  %xEE-EF             2( UTF8-tail )                //
        //                                                                    //
        //    UTF8-4      = %xF0      %x90-BF   2( UTF8-tail ) /              //
        //                  %xF1-F3             3( UTF8-tail ) /              //
        //                  %xF4      %x80-8F   2( UTF8-tail )                //
        //                                                                    //
        //    UTF8-tail   = %x80-BF                                           //
        //                                                                    //
        // From this we can determine:                                        //
        //                                                                    //
        // Valid sequences:                                                   //
        //                                                                    //
        //    Lead   Trail1 Trail2 Trail3 Trail4 Trail5 Codepoint range       //
        //    -----  -----  -----  -----  -----  -----  --------   --------   //
        //    00-7f                                        0000  -    007F    //
        //    c2-df  80-bf                                 0080  -    07FF    //
        //    e0     a0-bf  80-bf                          0800  -    0FFF    //
        //    e1-eC  80-bf  80-bf                          1000  -    CFFF    //
        //    ed     80-9f  80-bf                          D000  -    D7FF    //
        //    ee-ef  80-bf  80-bf                          E000  -    FFFF    //
        //    f0     90-bf  80-bf  80-bf                  10000  -   3FFFF    //
        //    f1-f3  80-bf  80-bf  80-bf                  40000  -   FFFFF    //
        //    f4     80-8f  80-bf  80-bf                 100000  -  10FFFF    //
        //                                                                    //
        // Invalid sequences:                                                 //
        //                                                                    //
        //    Lead   Trail1 Trail2 Trail3 Trail4 Trail5 Reason                //
        //    -----  -----  -----  -----  -----  -----  ----------------------//
        //  ! 80-bf                                     Trail byte range      //
        //  ! c0     80-bf                              Overlong  (0000-003F) //
        //  ! c1     80-bf                              Overlong  (0000-007F) //
        //  ! e0     80-9f  80-bf                       Overlong  (0000-07FF) //
        //  ! ed     a0-ff  80-bf                       Surrogate (D800-DFFF) //
        //  ! f0     80-8f  80-bf  80-bf                Overlong  (0000-FFFF) //
        //  ! f4     90-bf  80-bf  80-bf                 110000  -  13FFFF    //
        //  ! f5-f7  80-bf  80-bf  80-bf                 140000  -  1FFFFF    //
        //  ! f8-fb  80-bf  80-bf  80-bf  80-bf          200000  - 3FFFFFF    //
        //  ! fc-fd  80-bf  80-bf  80-bf  80-bf  80-bf  4000000  - 7FFFFFFF   //
        //  ! fe-ff                                     Not defined           //
        //                                                                    //
        //  ! indicates invalid UTF-8                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static eUTF8Result checkUTF8Seq(Byte[] seq,
                                                Int32 seqLen,
                                                ref Int32 codepointUCS)
        {
            Int32 ptrRev = seqLen,
                  ptrFwd = 0;

            Int64 codepoint = 0;

            Byte leadByte,
                 trailByte;

            leadByte = seq[0];

            switch (seqLen)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Check bytes (from last to first).                          //
                // All trailing bytes must be in the format 10xxxxxx          //
                // (i.e. have the first two bits as 10).                      //
                // Hence these bytes must be in the range 0x80 - 0xbf.        //
                //                                                            //
                //------------------------------------------------------------//

                case 6:
                    trailByte = seq[--ptrRev];

                    if ((trailByte < 0x80) || (trailByte > 0xBF))
                        return eUTF8Result.invalidTrailByteMark;

                    codepoint +=  seq[ptrFwd++];
                    codepoint <<= 6;

                    goto case 5;   // as C# does not support fall-through //

                case 5:
                    trailByte = seq[--ptrRev];

                    if ((trailByte < 0x80) || (trailByte > 0xBF))
                        return eUTF8Result.invalidTrailByteMark;

                    codepoint +=  seq[ptrFwd++];
                    codepoint <<= 6;

                    goto case 4;   // as C# does not support fall-through //

                case 4:
                    trailByte = seq[--ptrRev];

                    if ((trailByte < 0x80) || (trailByte > 0xBF))
                        return eUTF8Result.invalidTrailByteMark;

                    codepoint +=  seq[ptrFwd++];
                    codepoint <<= 6;
                    
                    goto case 3;   // as C# does not support fall-through //

                case 3:
                    trailByte = seq[--ptrRev];

                    if ((trailByte < 0x80) || (trailByte > 0xBF))
                        return eUTF8Result.invalidTrailByteMark;

                    codepoint +=  seq[ptrFwd++];
                    codepoint <<= 6;
                    
                    goto case 2;   // as C# does not support fall-through //

                case 2:
                    trailByte = seq[--ptrRev];

                    if ((trailByte < 0x80) || (trailByte > 0xBF))
                        return eUTF8Result.invalidTrailByteMark;

                //  leadByte = seq[--ptrRev];
                    
                    switch (leadByte)
                    {
                        case 0xE0:
                            //------------------------------------------------//
                            //                                                //
                            // Lead byte 0xe0 indicates a 3-byte sequence.    //
                            // If the next byte is < 0xa0, this indicates an  //
                            // overlong sequence, representing a codepoint of //
                            // U+07FF or less, which should have been encoded //
                            // as a 1-byte or 2-byte sequence.                //
                            //                                                //
                            //------------------------------------------------//

                            if (trailByte < 0xA0)
                                return eUTF8Result.overlongThreeByteSeq;
                            break;

                        case 0xED:
                            //------------------------------------------------//
                            //                                                //
                            // Lead byte 0xed indicates a 3-byte sequence.    //
                            // If the next byte is > 0x9f, this represents a  //
                            // codepoint in the range U+D8000 - U+DFFF, which //
                            // is reserved for the construction of UTF-16     //
                            // 'surrogate' pairs.                             //
                            //                                                //
                            //------------------------------------------------//

                            if (trailByte > 0x9F)
                                return eUTF8Result.surrogateCodepointUTF8;
                            break;

                        case 0xF0:
                            //------------------------------------------------//
                            //                                                //
                            // Lead byte 0xf0 indicates a 4-byte sequence.    //
                            // If the next byte is < 0x90, this indicates an  //
                            // overlong sequence, representing a codepoint of //
                            // U+FFFF or less, which should have been encoded //
                            // as a 1-byte, 2-byte or 3-byte sequence.        //
                            //                                                //
                            //------------------------------------------------//

                            if (trailByte < 0x90)
                                return eUTF8Result.overlongFourByteSeq;
                            break;

                        case 0xF4:
                            //------------------------------------------------//
                            //                                                //
                            // Lead byte 0xf4 indicates a 4-byte sequence.    //
                            // If the next byte is > 0x8f, this represents a  //
                            // codepoint of U+110000 or above, which is       //
                            // outside of the allowed Unicode range.          //
                            //                                                //
                            //------------------------------------------------//

                            if (trailByte > 0x8F)
                                return eUTF8Result.overvalueFourByteSeq;
                            break;
                    }

                    codepoint +=  seq[ptrFwd++];
                    codepoint <<= 6;

                    goto case 1;   // as C# does not support fall-through //

                case 1:
                    //--------------------------------------------------------//
                    //                                                        //
                    // Lead byte in multi-byte  sequence must be >= 0xc2.     //
                    // Only byte in single-byte sequence must be <= 0x7f.     //
                    //                                                        //
                    //--------------------------------------------------------//

                 // leadByte = seq[--ptrRev];

                    if (leadByte >= 0x80 && leadByte < 0xC0)
                        return eUTF8Result.invalidLeadByteMark;
                    else if (leadByte >= 0xC0 && leadByte < 0xC2)
                        return eUTF8Result.overlongTwoByteSeq;

                    codepoint +=  seq[ptrFwd++];

                    break;

                default:
                    //--------------------------------------------------------//
                    //                                                        //
                    // Length of supplied sequence is not between 1 and 6     //
                    // bytes.                                                 //
                    //                                                        //
                    //--------------------------------------------------------//

                    return eUTF8Result.invalidLength;
            }

            // leadByte = seq[--ptrRev];

            if (leadByte > 0xF4)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Sequences where the lead byte is >= 0xF5 are invalid.      //
                //                                                            //
                //------------------------------------------------------------//

                if (leadByte > 0xFD)
                    return eUTF8Result.invalidLeadByteMark;
                else if (leadByte > 0xFB)
                    return eUTF8Result.illegalSixByteSeq;
                else if (leadByte > 0xF7)
                    return eUTF8Result.illegalFiveByteSeq;
                else
                    return eUTF8Result.overvalueFourByteSeq;
            }

            codepoint -= cOffsetsUTF8[seqLen - 1];

            codepointUCS = (Int32) codepoint;
            
            return eUTF8Result.success;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c o n v e r t U T F 3 2 T o U T F 8 B y t e s                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Convert the supplied UTF-32 value into the equivalent UTF-8        //
        // byte sequence.                                                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static eUTF8Result convertUTF32ToUTF8Bytes(
            UInt32 utf32Value,
            ref Int32 utf8SeqLen,
            ref Byte [] utf8Seq)
        {
            eUTF8Result result;
            Int32 seqPos;

            UInt32 codepoint;

            result = eUTF8Result.success;

            codepoint = utf32Value;

            if ((codepoint >= cSurrogateHiLo) &&
                (codepoint <= cSurrogateLoHi))
            {
                //------------------------------------------------------------//
                //                                                            //
                // UTF-16 surrogate values are illegal in UTF-32              //
                //                                                            //
                //------------------------------------------------------------//

                return eUTF8Result.surrogateCodepointUTF32;
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Calculate how many bytes the result will require.              //
            // Translate any codepoint greater than the legal UTF-32 maximum  //
            // value into the replacement character.                          //
            //                                                                //
            //----------------------------------------------------------------//

            if (codepoint < 0x80)
            {
                utf8SeqLen = 1;
            }
            else if (codepoint < 0x800)
            {
                utf8SeqLen = 2;
            }
            else if (codepoint < 0x10000)
            {
                utf8SeqLen = 3;
            }
            else if (codepoint <= cMaxUTF32)
            {
                utf8SeqLen = 4;
            }
            else
            {
                utf8SeqLen = 3;

                codepoint = cReplacementChar;
                result = eUTF8Result.exceedsLegalMaximum;
            }

            seqPos = utf8SeqLen - 1;

            switch (utf8SeqLen)
            {
                // note: everything falls through hence use of GOTO (!!) as     //
                //       C# does not support fall through.                      // 

                case 4:
                    utf8Seq[seqPos--] =
                        (Byte) ((codepoint | cTrailByteMark) & cTrailByteMask);
                    codepoint >>= 6;
                    goto case 3; 

                case 3:
                    utf8Seq[seqPos--] =
                        (Byte) ((codepoint | cTrailByteMark) & cTrailByteMask);
                    codepoint >>= 6;
                    goto case 2; 

                case 2:
                    utf8Seq[seqPos--] =
                        (Byte) ((codepoint | cTrailByteMark) & cTrailByteMask);
                    codepoint >>= 6;
                    goto case 1; 

                case 1:
                    utf8Seq[seqPos--] =
                        (Byte) (codepoint | cLeadByteMarksUTF8[utf8SeqLen - 1]);
                    break;
            }

            return result;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c o n v e r t U T F 3 2 T o U T F 8 H e x S t r i n g              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Convert the supplied UTF-32 value into the equivalent UTF-8        //
        // hexadecimal string sequence.                                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static eUTF8Result convertUTF32ToUTF8HexString(
            UInt32 utf32Value,
            Boolean flag_0x_Prefix,
            ref String utf8Hex)
        {
            eUTF8Result result;
            Int32 seqPos;
            Int32 utf8SeqLen = 0;
            
            Byte[] utf8Seq = new Byte[4];

            UInt32 codepoint;

            StringBuilder utf8HexVal = new StringBuilder();

            result = eUTF8Result.success;

            codepoint = utf32Value;

            if ((codepoint >= cSurrogateHiLo) &&
                (codepoint <= cSurrogateLoHi))
            {
                //------------------------------------------------------------//
                //                                                            //
                // UTF-16 surrogate values are illegal in UTF-32              //
                //                                                            //
                //------------------------------------------------------------//

                return eUTF8Result.surrogateCodepointUTF32;
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Calculate how many bytes the result will require.              //
            // Translate any codepoint greater than the legal UTF-32 maximum  //
            // value into the replacement character.                          //
            //                                                                //
            //----------------------------------------------------------------//

            if (codepoint < 0x80)
            {
                utf8SeqLen = 1;
            }
            else if (codepoint < 0x800)
            {
                utf8SeqLen = 2;
            }
            else if (codepoint < 0x10000)
            {
                utf8SeqLen = 3;
            }
            else if (codepoint <= cMaxUTF32)
            {
                utf8SeqLen = 4;
            }
            else
            {
                utf8SeqLen = 3;

                codepoint = cReplacementChar;
                result = eUTF8Result.exceedsLegalMaximum;
            }

            seqPos = utf8SeqLen - 1;

            switch (utf8SeqLen)
            {
                // note: everything falls through hence use of GOTO (!!) as     //
                //       C# does not support fall through.                      // 

                case 4:
                    utf8Seq[seqPos--] =
                        (Byte)((codepoint | cTrailByteMark) & cTrailByteMask);
                    codepoint >>= 6;
                    goto case 3;

                case 3:
                    utf8Seq[seqPos--] =
                        (Byte)((codepoint | cTrailByteMark) & cTrailByteMask);
                    codepoint >>= 6;
                    goto case 2;

                case 2:
                    utf8Seq[seqPos--] =
                        (Byte)((codepoint | cTrailByteMark) & cTrailByteMask);
                    codepoint >>= 6;
                    goto case 1;

                case 1:
                    utf8Seq[seqPos--] =
                        (Byte)(codepoint | cLeadByteMarksUTF8[utf8SeqLen - 1]);
                    break;
            }

            utf8HexVal.Clear();

            if (flag_0x_Prefix)
                utf8HexVal.Append("0x");

            for (Int32 i = 0; i < utf8SeqLen; i++)
            {
                utf8HexVal.Append(utf8Seq[i].ToString("x2"));
            }

            utf8Hex = utf8HexVal.ToString ();

            return result;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c o n v e r t U T F 8 T o U T F 3 2                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Convert the supplied UTF-8 sequence into the equivalent UTF-32     //
        // value.                                                             //
        // No validation of the UTF-8 sequence is performed - it is assumed   //
        // that this already been checked.                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void convertUTF8ToUTF32(
            Byte[] utf8Seq,
            Int32 utf8SeqLen,
            ref UInt32 utf32Value)
        {
            Int64 codepoint = 0;
            Int32 seqPos = 0; 

            switch (utf8SeqLen)
            {
                case 6:
                    codepoint += utf8Seq[seqPos++];
                    codepoint <<= 6;
                    goto case 5;

                case 5:
                    codepoint += utf8Seq[seqPos++];
                    codepoint <<= 6;
                    goto case 4;

                case 4:
                    codepoint += utf8Seq[seqPos++];
                    codepoint <<= 6;
                    goto case 3;

                case 3:
                    codepoint += utf8Seq[seqPos++];
                    codepoint <<= 6;
                    goto case 2;

                case 2:
                    codepoint += utf8Seq[seqPos++];
                    codepoint <<= 6;
                    goto case 1;

                case 1:
                    codepoint += utf8Seq[seqPos];
                    break;
            }

            codepoint -= cOffsetsUTF8[utf8SeqLen - 1];

            utf32Value = (UInt32) codepoint;
        }
    }
}