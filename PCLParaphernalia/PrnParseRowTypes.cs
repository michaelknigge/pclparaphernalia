using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines a set of PCL 'status readback' Entity Type objects.
    /// 
    /// © Chris Hutchinson 2017
    /// 
    /// </summary>

    static class PrnParseRowTypes
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public enum eType
        {
            MsgError = 0,
            MsgWarning,
            MsgComment,
            MsgDiag,
            DataBinary,
            DataText,
            PCLSeqComplex,
            PCLSeqSimple,
            PCLControlCode,
            PCLDecode,
            PCLFontHddr,
            PCLFontChar,
            HPGL2Command,
            HPGL2ControlCode,
            PCLXLStreamHddr,
            PCLXLWhiteSpace,
            PCLXLOperator,
            PCLXLAttribute,
            PCLXLDataType,
            PCLXLDataValue,
            PCLXLFontHddr,
            PCLXLFontChar,
            PJLCommand,
            PMLSeq,
            PrescribeCommand
        }

        //--------------------------------------------------------------------//
        //                                                                    //
        // Must keep this in the same order as the eType enumeration.         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static PrnParseRowType[] _rowTypes = 
        {
            new PrnParseRowType(eType.MsgError,
                                "Error message"),
            new PrnParseRowType(eType.MsgWarning,
                     			"Warning message"),
            new PrnParseRowType(eType.MsgComment,
                                "Comment"),
            new PrnParseRowType(eType.MsgDiag,
                                "Diagnostic"),
            new PrnParseRowType(eType.DataBinary,
                                "Binary data"),
            new PrnParseRowType(eType.DataText,
                                "Text data"),
            new PrnParseRowType(eType.PCLSeqComplex,
                                "PCL complex sequence"),
            new PrnParseRowType(eType.PCLSeqSimple,
                                "PCL simple sequence"),
            new PrnParseRowType(eType.PCLControlCode,
                                "PCL control code"),
            new PrnParseRowType(eType.PCLDecode,
                                "PCL binary data decode"),
            new PrnParseRowType(eType.PCLFontHddr,
                                "PCL soft font header"),
            new PrnParseRowType(eType.PCLFontChar,
                                "PCL soft font character"),
            new PrnParseRowType(eType.HPGL2Command,
                                "HP-GL/2 command"),
            new PrnParseRowType(eType.HPGL2ControlCode,
                                "HP-GL/2 control code"),
            new PrnParseRowType(eType.PCLXLStreamHddr,
                                "PCL XL stream header"),
            new PrnParseRowType(eType.PCLXLWhiteSpace,
                                "PCL XL whitespace"),
            new PrnParseRowType(eType.PCLXLOperator,
                                "PCL XL operator"),
            new PrnParseRowType(eType.PCLXLAttribute,
                                "PCL XL attribute"),
            new PrnParseRowType(eType.PCLXLDataType,
                                "PCL XL data type"),
            new PrnParseRowType(eType.PCLXLDataValue,
                                "PCL XL data value"),
            new PrnParseRowType(eType.PCLXLFontHddr,
                                "PCL XL soft font header"),
            new PrnParseRowType(eType.PCLXLFontChar,
                                "PCL XL soft font character"),
            new PrnParseRowType(eType.PJLCommand,
                                "PJL command"),
            new PrnParseRowType(eType.PMLSeq,
                                "PML sequence"),
            new PrnParseRowType(eType.PrescribeCommand,
                                "Prescribe command")
       };
        
        private static Int32 _rowTypeCount = _rowTypes.GetUpperBound(0) + 1;
        
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C o u n t                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return count of Entity Type definitions.                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 getCount()
        {
            return _rowTypeCount;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t D e s c                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return description associated with specified row type index.       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getDesc (Int32 selection)
        {
            return _rowTypes[selection].getDesc();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t T y p e                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return type of type.                                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static eType getType(Int32 selection)
        {
            return _rowTypes[selection].getType();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t D e f a u l t C l r s                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Sets default background/foreground colours for each row type.      //
        //                                                                    //
        // Use the colours defined by the standard 'Colors' class object.     //
        // This list should consist of 140 standard colours (as defined in    //
        // .net, Unix X11, etc.) plus the 'transparent' colour.               //
        // This list is unlikely to change, so we can store the indices of    //
        // the selected colours and be relatively confident that these will   //
        // always refer to the same actual colours.                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void setDefaultClrs (
            ref Int32 [] indxClrMapBack,
            ref Int32 [] indxClrMapFore)
        {
            PrnParseRowTypes.eType rowType;

            for (Int32 i = 0; i < _rowTypeCount; i++)
            {
                rowType = (PrnParseRowTypes.eType)i;

                switch (rowType)
                {
                    case PrnParseRowTypes.eType.MsgError:

                        indxClrMapBack[i] = (Int32)PrnParseConstants.eStdClrs.Crimson;
                        indxClrMapFore[i] = (Int32)PrnParseConstants.eStdClrs.Yellow;

                        break;

                    case PrnParseRowTypes.eType.MsgWarning:

                        indxClrMapBack[i] = (Int32)PrnParseConstants.eStdClrs.Red;
                        indxClrMapFore[i] = (Int32)PrnParseConstants.eStdClrs.White;

                        break;

                    case PrnParseRowTypes.eType.MsgComment:

                        indxClrMapBack[i] = (Int32)PrnParseConstants.eStdClrs.Bisque;
                        indxClrMapFore[i] = (Int32)PrnParseConstants.eStdClrs.Blue;

                        break;

                     case PrnParseRowTypes.eType.MsgDiag:

                        indxClrMapBack[i] = (Int32)PrnParseConstants.eStdClrs.Fuchsia;
                        indxClrMapFore[i] = (Int32)PrnParseConstants.eStdClrs.Yellow;

                        break;

                    case PrnParseRowTypes.eType.DataBinary:

                        indxClrMapBack[i] = (Int32)PrnParseConstants.eStdClrs.Ivory;
                        indxClrMapFore[i] = (Int32)PrnParseConstants.eStdClrs.SlateGray;

                        break;

                    case PrnParseRowTypes.eType.DataText:

                        indxClrMapBack[i] = (Int32)PrnParseConstants.eStdClrs.Azure;
                        indxClrMapFore[i] = (Int32)PrnParseConstants.eStdClrs.Black;

                        break;

                    case PrnParseRowTypes.eType.PCLSeqComplex:

                        indxClrMapBack[i] = (Int32)PrnParseConstants.eStdClrs.LightYellow;
                        indxClrMapFore[i] = (Int32)PrnParseConstants.eStdClrs.DarkBlue;

                        break;

                    case PrnParseRowTypes.eType.PCLSeqSimple:

                        indxClrMapBack[i] = (Int32)PrnParseConstants.eStdClrs.LightYellow;
                        indxClrMapFore[i] = (Int32)PrnParseConstants.eStdClrs.Blue;

                        break;

                    case PrnParseRowTypes.eType.PCLControlCode:

                        indxClrMapBack[i] = (Int32)PrnParseConstants.eStdClrs.Honeydew;
                        indxClrMapFore[i] = (Int32)PrnParseConstants.eStdClrs.Black;

                        break;

                    case PrnParseRowTypes.eType.PCLDecode:

                        indxClrMapBack[i] = (Int32)PrnParseConstants.eStdClrs.White;
                        indxClrMapFore[i] = (Int32)PrnParseConstants.eStdClrs.Black;

                        break;

                    case PrnParseRowTypes.eType.PCLFontHddr:

                        indxClrMapBack[i] = (Int32)PrnParseConstants.eStdClrs.LightCyan;
                        indxClrMapFore[i] = (Int32)PrnParseConstants.eStdClrs.BlueViolet;

                        break;

                    case PrnParseRowTypes.eType.PCLFontChar:

                        indxClrMapBack[i] = (Int32)PrnParseConstants.eStdClrs.LightCyan;
                        indxClrMapFore[i] = (Int32)PrnParseConstants.eStdClrs.DarkMagenta;

                        break;

                    case PrnParseRowTypes.eType.HPGL2Command:

                        indxClrMapBack[i] = (Int32)PrnParseConstants.eStdClrs.LemonChiffon;
                        indxClrMapFore[i] = (Int32)PrnParseConstants.eStdClrs.Teal;

                        break;

                    case PrnParseRowTypes.eType.HPGL2ControlCode:

                        indxClrMapBack[i] = (Int32)PrnParseConstants.eStdClrs.PaleGoldenrod;
                        indxClrMapFore[i] = (Int32)PrnParseConstants.eStdClrs.Teal;

                        break;

                    case PrnParseRowTypes.eType.PCLXLStreamHddr:

                        indxClrMapBack[i] = (Int32)PrnParseConstants.eStdClrs.LightGreen;
                        indxClrMapFore[i] = (Int32)PrnParseConstants.eStdClrs.Blue;

                        break;

                    case PrnParseRowTypes.eType.PCLXLWhiteSpace:

                        indxClrMapBack[i] = (Int32)PrnParseConstants.eStdClrs.SeaShell;
                        indxClrMapFore[i] = (Int32)PrnParseConstants.eStdClrs.Black;

                        break;

                    case PrnParseRowTypes.eType.PCLXLOperator:

                        indxClrMapBack[i] = (Int32)PrnParseConstants.eStdClrs.LightCyan;
                        indxClrMapFore[i] = (Int32)PrnParseConstants.eStdClrs.Red;

                        break;

                    case PrnParseRowTypes.eType.PCLXLAttribute:

                        indxClrMapBack[i] = (Int32)PrnParseConstants.eStdClrs.MistyRose;
                        indxClrMapFore[i] = (Int32)PrnParseConstants.eStdClrs.Red;

                        break;

                    case PrnParseRowTypes.eType.PCLXLDataType:

                        indxClrMapBack[i] = (Int32)PrnParseConstants.eStdClrs.FloralWhite;
                        indxClrMapFore[i] = (Int32)PrnParseConstants.eStdClrs.Teal;

                        break;

                    case PrnParseRowTypes.eType.PCLXLDataValue:

                        indxClrMapBack[i] = (Int32)PrnParseConstants.eStdClrs.FloralWhite;
                        indxClrMapFore[i] = (Int32)PrnParseConstants.eStdClrs.Blue;

                        break;

                    case PrnParseRowTypes.eType.PCLXLFontHddr:

                        indxClrMapBack[i] = (Int32)PrnParseConstants.eStdClrs.LightCyan;
                        indxClrMapFore[i] = (Int32)PrnParseConstants.eStdClrs.Blue;

                        break;

                    case PrnParseRowTypes.eType.PCLXLFontChar:

                        indxClrMapBack[i] = (Int32)PrnParseConstants.eStdClrs.LightCyan;
                        indxClrMapFore[i] = (Int32)PrnParseConstants.eStdClrs.DarkMagenta;

                        break;

                   case PrnParseRowTypes.eType.PJLCommand:

                        indxClrMapBack[i] = (Int32)PrnParseConstants.eStdClrs.Khaki;
                        indxClrMapFore[i] = (Int32)PrnParseConstants.eStdClrs.Black;

                        break;

                    case PrnParseRowTypes.eType.PMLSeq:

                        indxClrMapBack[i] = (Int32)PrnParseConstants.eStdClrs.Lavender;
                        indxClrMapFore[i] = (Int32)PrnParseConstants.eStdClrs.Blue;

                        break;

                    case PrnParseRowTypes.eType.PrescribeCommand:

                        indxClrMapBack[i] = (Int32)PrnParseConstants.eStdClrs.PeachPuff;
                        indxClrMapFore[i] = (Int32)PrnParseConstants.eStdClrs.Teal;

                        break;

                    default:

                        indxClrMapBack[i] = (Int32)PrnParseConstants.eStdClrs.White;
                        indxClrMapFore[i] = (Int32)PrnParseConstants.eStdClrs.Black;

                        break;
                }
            }
        }
    }
}