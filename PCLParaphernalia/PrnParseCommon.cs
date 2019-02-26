using System;
using System.Data;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides print-language-independent routines associated with
    /// 'parsing' of print file.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class PrnParseCommon
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
        
        static Boolean _showMacroData = true;

        static PrnParse.eParseType _parseType;
        
        static Int32 _macroLevel = 0;

        static String _colName_RowType = PrnParseConstants.cRptA_colName_RowType;
        static String _colName_Action = PrnParseConstants.cRptA_colName_Action;
        static String _colName_Offset = PrnParseConstants.cRptA_colName_Offset;
        static String _colName_Type   = PrnParseConstants.cRptA_colName_Type;
        static String _colName_Seq    = PrnParseConstants.cRptA_colName_Seq;
        static String _colName_Desc   = PrnParseConstants.cRptA_colName_Desc;

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // a d d D a t a R o w                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Adds a row, with numeric offset value, to the output table.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void addDataRow (
            PrnParseRowTypes.eType rowType,
            DataTable table,
            PrnParseConstants.eOvlShow  makeOvlShow,
            PrnParseConstants.eOptOffsetFormats indxOffsetFormat,
            Int32     offset,
            Int32     level,
            String    type,
            String    seq,
            String    desc)
        {
            if (_parseType == PrnParse.eParseType.ScanForPDL)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Output inhibited.                                          //
                //                                                            //
                //------------------------------------------------------------//
            }
            else if ((_parseType == PrnParse.eParseType.MakeOverlay)
                                           &&
                     (makeOvlShow ==
                        PrnParseConstants.eOvlShow.None))
            {
                //------------------------------------------------------------//
                //                                                            //
                // Output inhibited.                                          //
                //                                                            //
                //------------------------------------------------------------//
            }
            else if ((!_showMacroData) && (_macroLevel > 0))
            {
                //------------------------------------------------------------//
                //                                                            //
                // Output inhibited (e.g. don't show PCL macro contents); do  //
                // nothing.                                                   //
                //                                                            //
                //------------------------------------------------------------//
            }
            else
            {
                DataRow row;

                String offsetText;

                row = table.NewRow ();

                if (offset < 0)
                {
                    if (offset ==
                        (Int32) PrnParseConstants.eOffsetPosition.StartOfFile)
                        offsetText = "<Start>";
                    else if (offset ==
                        (Int32) PrnParseConstants.eOffsetPosition.EndOfFile)
                        offsetText = "<End>";
                    else
                        offsetText = "";
                }
                else
                {
                    if (indxOffsetFormat ==
                        PrnParseConstants.eOptOffsetFormats.Decimal)
                    {
                        if (level == 0)
                            offsetText = String.Format ("{0:d10}", offset);
                        else
                            offsetText = String.Format ("{0:d2}", level) + ":" +
                                         String.Format ("{0:d10}", offset);
                    }
                    else
                    {
                        if (level == 0)
                            offsetText = String.Format ("{0:x8}", offset);
                        else
                            offsetText = String.Format ("{0:x2}", level) + ":" +
                                         String.Format ("{0:x8}", offset);
                    }
                }

                if ((_parseType == PrnParse.eParseType.MakeOverlay) &&
                    (makeOvlShow != PrnParseConstants.eOvlShow.None))
                {
                    row[_colName_Action] = makeOvlShow.ToString ();
                }

                row[_colName_RowType] = (Int32) rowType;
             // row[_colName_RowType] = rowType.ToString(); // **** DIAG ************
                row[_colName_Offset]  = offsetText;
                row[_colName_Type]    = type;
                row[_colName_Seq]     = seq;
                row[_colName_Desc]    = desc;

                table.Rows.Add (row);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // a d d T e x t R o w                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Adds a row, without numeric offset value, to the output table.     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void addTextRow (
            PrnParseRowTypes.eType rowType,
            DataTable table,
            PrnParseConstants.eOvlShow  makeOvlShow,
            String    offsetText,
            String    type,
            String    seq,
            String    desc)
        {
            if (_parseType == PrnParse.eParseType.ScanForPDL)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Output inhibited.                                          //
                //                                                            //
                //------------------------------------------------------------//
            }
            else if ((_parseType == PrnParse.eParseType.MakeOverlay)
                                           &&
                     (makeOvlShow ==
                        PrnParseConstants.eOvlShow.None))
            {
                //------------------------------------------------------------//
                //                                                            //
                // Output inhibited.                                          //
                //                                                            //
                //------------------------------------------------------------//
            }
            else if ((!_showMacroData) && (_macroLevel > 0))
            {
                //------------------------------------------------------------//
                //                                                            //
                // Output inhibited (e.g. don't show PCL macro contents); do  //
                // nothing.                                                   //
                //                                                            //
                //------------------------------------------------------------//
            }
            else
            {
                DataRow row;

                row = table.NewRow ();

                if (_parseType == PrnParse.eParseType.MakeOverlay)
                {
                    if (makeOvlShow == PrnParseConstants.eOvlShow.Insert)
                        row[_colName_Action] = "Insert";
                    else
                        row[_colName_Action] = "";
                }

                row[_colName_RowType] = (Int32) rowType;
             // row[_colName_RowType] = rowType.ToString(); // **** DIAG ************
                row[_colName_Offset] = offsetText;
                row[_colName_Type]   = type;
                row[_colName_Seq]    = seq;
                row[_colName_Desc]   = desc;

                table.Rows.Add (row);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b y t e A r r a y T o H e x S t r i n g                            //
        //--------------------------------------------------------------------//

        public static String byteArrayToHexString(Byte [] byteArray,
                                                  Int32   startByte,
                                                  Int32   byteCt)
        {
            const Int32 triplet = 3;

            Int32 arrayLen = byteArray.Length;

            Char [] chars = new Char [byteCt * triplet];

            for (Int32 i = 0; i < byteCt; i++)
            {
                Int32 b = byteArray[startByte + i];
                Int32 j = i * triplet;

                chars[j]     = PrnParseConstants.cHexChars[b >> 4];
                chars[j + 1] = PrnParseConstants.cHexChars[b & 0xF];
                chars[j + 2] = (Char) PrnParseConstants.asciiSpace;
            }

            return new String (chars);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b y t e A r r a y P a i r T o H e x S t r i n g                    //
        //--------------------------------------------------------------------//

        public static Boolean byteArrayPairToHexString (Byte[]     byteArray,
                                                        Int32      startByte,
                                                        Int32      byteCt,
                                                        ref String hexData)
        {
            const Int32 quintet = 5;

            Boolean all_ffs = true;

            Int32 pairCt = byteCt / 2;

            Int32 b,
                  c;

            Int32 j,
                  k;

            Char[] chars = new Char[pairCt * quintet];

            for (Int32 i = 0; i < pairCt; i++)
            {
                k = i * 2;

                b = byteArray[startByte + k];
                c = byteArray[startByte + k + 1];

                if ((b != 0xff) || (c != 0xff))
                    all_ffs = false;
                
                j = i * quintet;

                chars[j] = PrnParseConstants.cHexChars[b >> 4];
                chars[j + 1] = PrnParseConstants.cHexChars[b & 0xF];
                chars[j + 2] = PrnParseConstants.cHexChars[c >> 4];
                chars[j + 3] = PrnParseConstants.cHexChars[c & 0xF];
                chars[j + 4] = (Char)PrnParseConstants.asciiSpace;
            }

            hexData = new String (chars);

        //    return new String(chars);
            return all_ffs;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b y t e T o H e x S t r i n g                                      //
        //--------------------------------------------------------------------//

        public static String byteToHexString(Byte byteVal)
        {
            Char[] chars = new Char[2];

            Int32 b = byteVal;

            chars[0] = PrnParseConstants.cHexChars[b >> 4];
            chars[1] = PrnParseConstants.cHexChars[b & 0xF];
            
            return new String (chars);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b y t e T o S t r i n g                                            //
        //--------------------------------------------------------------------//

        public static String byteToString(Byte byteVal)
        {
            return ((Char) byteVal).ToString ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n i t i a l i s e R u n T y p e                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set value indicating current tool ('PrnAnalyse' or 'MakeMacro'.    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void initialiseRunType(PrnParse.eParseType parseType)
        {
            _parseType = parseType;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i s A l p h a b e t i c                                            //
        //--------------------------------------------------------------------//
        
        public static Boolean isAlphabetic(Byte byteVal)
        {
            if (((byteVal >= PrnParseConstants.asciiAlphaLCMin)
                                   &&
                 (byteVal <= PrnParseConstants.asciiAlphaLCMax))
                                   ||
                ((byteVal >= PrnParseConstants.asciiAlphaUCMin)
                                   &&
                 (byteVal <= PrnParseConstants.asciiAlphaUCMax)))
                return true;
            else
                return false;
        }
 
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t D i s p l a y C r i t e r i a                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set markers to indicate whether subsequent AddRow actions will     //
        // display data, or not.                                              //
        // This feature is intended to allow for the display, or otherwise,   //
        // of the contents of PCL macros:                                     //
        //    -  The ShowData variable indicates the state of the             //
        //       PCLShowMacroData option.                                     //
        //    -  Each time that a PCL StartMacro sequence is found, the       //
        //       current level is incremented.                                //
        //    -  Each time that a PCL StopMacro sequence is found, the        //
        //       current level is decremented.                                //
        //    -  Thus we know whether we are within a macro.                  //
        //       Although macro Calls can be nested, macro definitions        //
        //       cannot; but we need to cater for someone trying to do this.  //
        //    -  Exit from the language (via UEL sequence) should reset the   //
        //       level to zero.                                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void setDisplayCriteria(Boolean showMacroData,
                                              Int32 macroLevel)
        {
            _showMacroData = showMacroData;
            _macroLevel = macroLevel;
        }
    }
}