using System;
using System.IO;
using System.Text;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides primitive and macro operations for PCL print language.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class PCLWriter
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public const UInt16 sessionUPI               = 600;
        public const UInt16 pointsPerInch            = 72;
        public const UInt16 plotterUnitsPerInchHPGL2 = 1016;

        public enum ePushPop
        {
            Push,
            Pop
        }

        public enum eMacroControl
        {
            StartDef,       // 0
            StopDef,        // 1
            Execute,        // 2
            Call,           // 3
            Overlay,        // 4
            Disable,        // 5
            DeleteAll,      // 6
            DeleteTemp,     // 7
            Delete,         // 8
            MakeTemporary,  // 9
            MakePermanent   // 10
        }

        public enum ePatternType
        {
            SolidBlack,     // 0
            SolidWhite,     // 1
            Shading,        // 2
            CrossHatch,     // 3
            UserDefined     // 4
        }

        public enum eSimplePalette
        {
            K,
            RGB,
            CMY
        }

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        //--------------------------------------------------------------------//
        //                                                                    //
        // In a number of the methods, text is supplied as a (Unicode)        //
        // string, then converted to a character array before writing out.    //
        //                                                                    //
        // This works OK provided that all characters are within the ASCII    //
        // range (0x00-0x7f), and are hence represented using a single byte   //
        // in the UTF-8 encoding.                                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h a r D o w n l o a d C o d e                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate download soft font character code sequence.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void charDownloadCode(BinaryWriter prnWriter,
                                            UInt16 codepoint)
        {
            String seq;

            seq = "\x1b" + "*c" + codepoint + "E";

            prnWriter.Write (seq.ToCharArray (), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h a r D o w n l o a d D e s c                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate download soft font character descriptor/data header       //
        // sequence.                                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void charDownloadDesc(BinaryWriter prnWriter,
                                            UInt16 hddrLen)
        {
            String seq;

            seq = "\x1b" + "(s" + hddrLen + "W";

            prnWriter.Write (seq.ToCharArray (), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c m d H P G L 2                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate HP-GL/2 command with parameters.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void cmdHPGL2 (BinaryWriter prnWriter,
                                     String       mnemonic,
                                     String       parameters,
                                     Boolean      terminate)
        {
            StringBuilder cmd = new StringBuilder ();

            if (parameters == "")
                cmd.Append (mnemonic);
            else
                cmd.Append (mnemonic).Append (parameters);

            if (terminate)
                cmd.Append(";");

            prnWriter.Write(cmd.ToString ().ToCharArray(), 0, cmd.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c o n f i g u r e I m a g e D a t a                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write (short 6-byte form of) Configure Image Data     //
        // sequence.                                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void configureImageData(BinaryWriter prnWriter,
                                              Byte         colourSpace,
                                              Byte         pixelEncodingMode,
                                              Byte         bitsPerIndex,
                                              Byte         bitsPerPrimary_1,
                                              Byte         bitsPerPrimary_2,
                                              Byte         bitsPerPrimary_3)
        {
            Int32 indStd = 0;

            Byte [] seq = new Byte [11];

            seq[indStd++] = 0x1b;
            seq[indStd++] = (Byte)'*';
            seq[indStd++] = (Byte)'v';
            seq[indStd++] = (Byte)'6';
            seq[indStd++] = (Byte)'W';
            seq[indStd++] = colourSpace;        // 0: ColourSpace
            seq[indStd++] = pixelEncodingMode;  // 1: Pixel Encoding Mode
            seq[indStd++] = bitsPerIndex;       // 2: Bits Per Index
            seq[indStd++] = bitsPerPrimary_1;   // 3: Bits Per Component
            seq[indStd++] = bitsPerPrimary_2;   // 4: Bits Per Component
            seq[indStd++] = bitsPerPrimary_3;   // 5: Bits Per Component

            prnWriter.Write(seq, 0, indStd);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c u r s o r P o s i t i o n                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write Cursor Position sequence.                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void cursorPosition(BinaryWriter prnWriter,
                                          Int16        coordX,
                                          Int16        coordY)
        {
            String seq;

            seq = "\x1b" + "*p" +
                           coordX +
                           "x" +                // Position: Horizontal
                           coordY +
                           "Y";                 // Position: Vertical

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c u r s o r P u s h P o p                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write Cursor Push (Store) sequence.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void cursorPushPop(BinaryWriter prnWriter,
                                         ePushPop     pushPop)
        {
            String seq;

            if (pushPop == ePushPop.Push)
                seq = "\x1b" + "&f0S";          // Cursor Position: Push
            else
                seq = "\x1b" + "&f1S";          // Cursor Position: Pop

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c u r s o r R e l a t i v e                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write Cursor (relative) Position sequence.            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void cursorRelative(BinaryWriter prnWriter,
                                          Int16        coordX,
                                          Int16        coordY)
        {
            String seq;

            String relX;
            String relY;

            if (coordX > 0)
                relX = "+" + coordX;
            else
                relX = coordX.ToString();

            if (coordY > 0)
                relY = "+" + coordY;
            else
                relY = coordY.ToString();

            if (coordX == 0)
            {
                seq = "\x1b" + "*p" +
                               relY +
                               "Y";             // Position: Vertical
            }
            else if (coordY == 0)
            {
                seq = "\x1b" + "*p" +
                               relX +
                               "X";             // Position: Horizontal
            }
            else
            {
                seq = "\x1b" + "*p" +
                               relX +
                               "x"  +           // Position: Horizontal
                               relY +
                               "Y";             // Position: Vertical
            }

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d e f L o g P a g e                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate define logical page sequence.                             //
        //                                                                    //
        // Several of the fields in the (binary) data associated with this    //
        // escape sequence are 16-bit signed, or unsigned integer values      //
        // where the values are expected to be in most-significant-byte-first //
        // order, so the bytes have to be reversed from that used in the      //
        // intel least-significant-byte-first order.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void defLogPage (BinaryWriter prnWriter,
                                       Int32        indxOrientation,
                                       Int16        leftOffset,
                                       Int16        topOffset,
                                       UInt16       pageWidth,
                                       UInt16       pageHeight)
        {
            const Int32 defLogPageDataLen = 10;

            String seq;

            Byte[] buffer = new Byte[defLogPageDataLen];

            Byte[] tempArray;

            seq = "\x1b" + "&a" +
                           defLogPageDataLen +
                           "W";                 // Define Logical Page

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);

            //----------------------------------------------------------------//

            tempArray = BitConverter.GetBytes(leftOffset);

            buffer[0] = tempArray[1];
            buffer[1] = tempArray[0];

            tempArray = BitConverter.GetBytes(topOffset);

            buffer[2] = tempArray[1];
            buffer[3] = tempArray[0];

            buffer[4] = PCLOrientations.getIdPCL(indxOrientation);
            buffer[5] = 0x00;

            tempArray = BitConverter.GetBytes(pageWidth);

            buffer[6] = tempArray[1];
            buffer[7] = tempArray[0];

            tempArray = BitConverter.GetBytes(pageHeight);

            buffer[8] = tempArray[1];
            buffer[9] = tempArray[0];

            prnWriter.Write(buffer, 0, defLogPageDataLen);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // f o n t                                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate font selection sequence.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void font(BinaryWriter prnWriter,
                                Boolean      primary,
                                String       symSet,
                                String       fontSel)
        {
            String seq;

            Char priSec;

            if (primary)
                priSec = '(';
            else
                priSec = ')';

            if (symSet != "")
                seq = "\x1b" + priSec + symSet +
                      "\x1b" + priSec + fontSel;
            else
                seq = "\x1b" + priSec + fontSel;

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // f o n t D o w n l o a d H d d r                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate download soft font header sequence.                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void fontDownloadHddr (BinaryWriter prnWriter,
                                             UInt32       hddrLen)
        {
            String seq;

            seq = "\x1b" + ")s" + hddrLen + "W";

            prnWriter.Write (seq.ToCharArray (), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // f o n t D o w n l o a d I D                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate font control ID sequence.                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void fontDownloadID(BinaryWriter prnWriter,
                                          UInt16       downloadID)
        {
            String seq;

            seq = "\x1b" + "*c" + downloadID + "D";

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // f o n t D o w n l o a d R e m o v e                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate font control delete sequence.                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void fontDownloadRemove(BinaryWriter prnWriter,
                                              UInt16       downloadID)
        {
            String seq;

            seq = "\x1b" + "*c" + downloadID + "d2F";

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // f o n t D o w n l o a d S a v e                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate font control save sequence.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void fontDownloadSave(BinaryWriter prnWriter,
                                            Boolean      permanent)
        {
            String seq;

            if (permanent)
                seq = "\x1b" + "*c5F";
            else
                seq = "\x1b" + "*c4F";

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // f o n t F i l e _ I d _ A s s o c i a t e                   I      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate sequences to associate a specified font identifier with   //
        // a specified printer-resident file.                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void fontFileIdAssociate (BinaryWriter prnWriter,
                                                UInt16       fontID,
                                                String       filename)
        {
            String seq;

            Int32 fnLen = filename.Length + 1;

            seq = "\x1b" + "*c" + fontID + "D" +
                  "\x1b" + "&n" + fnLen + "W" + "\x01" + filename;

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // f o n t F i l e _ I d _ A s s o c i a t e                  I I     //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate sequences to associate a specified font identifier with   //
        // a specified printer-resident file via use of a macro.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void fontFileIdAssociate (BinaryWriter prnWriter,
                                                UInt16 fontID,
                                                UInt16 fontMacroId,
                                                String filename)
        {
            String seq;

            Int32 fnLen = filename.Length + 1;

            seq = "\x1b" + "*c" + fontID + "D" +
                  "\x1b" + "&f" + fontMacroId + "Y" +
                  "\x1b" + "&n" + fnLen + "W" + "\x05" + filename +
                  "\x1b" + "&f3X";

            prnWriter.Write (seq.ToCharArray (), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // f o r m F e e d                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate form feed control character.                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void formFeed(BinaryWriter prnWriter)
        {
            Byte[] x = new Byte[1];

            x[0] = 0x0C;

            prnWriter.Write(x, 0, 1);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l i n e H o r i z o n t a l                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate escape sequences to draw a horizontal line.               //
        // The line is defined in terms of position, length and stroke (line  //
        // thickness).                                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void lineHorizontal(BinaryWriter prnWriter,
                                          Int16        coordX,
                                          Int16        coordY,
                                          Int16        length,
                                          Int16        stroke)
        {
            String seq;

            seq = "\x1b" + "*p" +
                           coordX + "x" +       // Position: Horizontal
                           coordY + "Y" +       // Position: Vertical
                  "\x1b" + "*c" +
                           length + "a" +       // Rectangle Size: Horizontal
                           stroke + "b" +       // Rectangle Size: Vertical
                           "0P";                // Fill Rectangle: Solid Area

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l i n e V e r t i c a l                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate escape sequences to draw a vertical line.                 //
        // The line is defined in terms of position, length and stroke (line  //
        // thickness).                                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void lineVertical(BinaryWriter prnWriter,
                                        Int16        coordX,
                                        Int16        coordY,
                                        Int16        length,
                                        Int16        stroke)
        {
            String seq;

            seq = "\x1b" + "*p" +
                           coordX + "x" +       // Position: Horizontal
                           coordY + "Y" +       // Position: Vertical
                  "\x1b" + "*c" +
                           stroke + "a" +       // Rectangle Size: Horizontal
                           length + "b" +       // Rectangle Size: Vertical
                           "0P";                // Fill Rectangle: Solid Area

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m a c r o C o n t r o l                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write macro control sequence.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void macroControl(BinaryWriter  prnWriter,
                                        Int16         macroId,
                                        eMacroControl control)
        {
            String seq;

            if (control == eMacroControl.StartDef)
                seq = "\x1b" + "&f" +
                               macroId + "y" +  // Macro: ID
                               "0X";            // Macro: start definition
            else if (control == eMacroControl.StopDef)
                seq = "\x1b" + "&f" +
                          //   macroId + "y" +  // Macro: ID - don't need this
                               "1X";            // Macro: end definition
            else if (control == eMacroControl.Execute)
                seq = "\x1b" + "&f" +
                               macroId + "y" +  // Macro: ID
                               "2X";            // Macro: Execute
            else if (control == eMacroControl.Call)
                seq = "\x1b" + "&f" +
                               macroId + "y" +  // Macro: ID
                               "3X";            // Macro: call
            else if (control == eMacroControl.Overlay)
                seq = "\x1b" + "&f" +
                               macroId + "y" +  // Macro: ID
                               "4X";            // Macro: enable for overlay
            else if (control == eMacroControl.Delete)
                seq = "\x1b" + "&f" +
                               macroId + "y" +  // Macro: ID
                               "8X";            // Macro: delete
            else if (control == eMacroControl.MakePermanent)
                seq = "\x1b" + "&f" +
                               macroId + "y" +  // Macro: ID
                               "10X";           // Macro: make permanent
            else
                seq = "";

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m a c r o D o w n l o a d I d                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate macro control Id sequence.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void macroDownloadId (BinaryWriter prnWriter,
                                            UInt16       downloadId)
        {
            String seq;

            seq = "\x1b" + "&f" + downloadId + "Y";

            prnWriter.Write (seq.ToCharArray (), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m a c r o F i l e _ I d _ A s s o c i a t e                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate sequences to associate a specified macro identifier with  //
        // a specified printer-resident file.                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void macroFileIdAssociate (BinaryWriter prnWriter,
                                                 UInt16       downloadID,
                                                 String       filename)
        {
            String seq;

            Int32 fnLen = filename.Length + 1;

            seq = "\x1b" + "&f" + downloadID + "Y" +
                  "\x1b" + "&n" + fnLen + "W" + "\x05" + filename;

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m a r g i n L e f t                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write 'left margin' sequence.                         //
        //                                                                    //
        // Width of each column is given by current HMI value.                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void marginLeft (BinaryWriter prnWriter,
                                       Int16        columns)
        {
            String seq;

            seq = "\x1b" + "&a" +               // left margin
                           columns +
                           "L";

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m a r g i n T o p                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write 'top margin' sequence.                          //
        //                                                                    //
        // Height of each line is given by current VMI value.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void marginTop (BinaryWriter prnWriter,
                                      Int16        lines)
        {
            String seq;

            seq = "\x1b" + "&l" +               // top margin
                           lines +
                           "E";

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m o d e H P G L 2                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write Enter HP-GL/2 Mode sequence.                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void modeHPGL2 (BinaryWriter prnWriter,
                                      Boolean      cursorPCL,
                                      Boolean      penPCL)
        {
            String seq;

            Int16 mode;

            if (penPCL)
                mode = 1;
            else
                mode = 0;

            if (cursorPCL)
                mode += 2;

            seq = "\x1b" + "%" +
                           mode +
                           "B";                 // Enter HP-GL/2 Mode

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m o d e P C L                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write Enter PCL Mode sequence.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void modePCL (BinaryWriter prnWriter,
                                    Boolean      cursorPCL)
        {
            String seq;

            Int16 mode;

            if (cursorPCL)
                mode = 0;
            else
                mode = 1;

            seq = "\x1b" + "%" +
                           mode +
                           "A";                 // Enter PCL Mode

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a g e F a c e                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write 'page face' sequence.                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void pageFace(BinaryWriter prnWriter,
                                    Boolean frontFace)
        {
            String seq;
            String faceId;

            if (frontFace)
                faceId = "1";
            else
                faceId = "2";

            seq = "\x1b" + "&a" +               // page face
                           faceId +
                           "G";

            prnWriter.Write (seq.ToCharArray (), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a g e H e a d e r                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write page header sequences.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void pageHeader(BinaryWriter prnWriter,
                                      Int32        indxPaperSize,
                                      Int32        indxPaperType,
                                      Int32        indxOrientation,
                                      Int32        indxPlexMode)
        {
            String seq;

            if (PCLPaperTypes.getType(indxPaperType) !=
                    PCLPaperTypes.eEntryType.NotSet)
            {
                String tmpStr = PCLPaperTypes.getName(indxPaperType);
                Int32 len = tmpStr.Length + 1;

                seq = "\x1b" + "&n" +           // Alphanumeric ID
                               len  +
                               "Wd" +           // ... Paper Type
                               tmpStr;

                prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
            }

            seq = "\x1b" + "&l"   +
                           PCLPaperSizes.getIdPCL(indxPaperSize) +
                           "a"    +             // Paper Size
                           PCLOrientations.getIdPCL(indxOrientation) +
                           "o"    +             // Orientation
                           PCLPlexModes.getIdPCL(indxPlexMode) +
                           "s"    +             // plex mode 
                           "1l"   +             // perforation skip enable
                           "0E"   +             // Top Margin (lines)
                  "\x1b" + "&a0L";              // Left Margin (columns)
   
            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a g e H e a d e r C u s t o m                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write page header sequences for custom size paper.    //
        // The width and length values are supplied as dots (at 600 dpi).     //
        // The PCL sequences use decipoint units, so need to convert first.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void pageHeaderCustom (BinaryWriter prnWriter,
                                             Int32        indxPaperType,
                                             Int32        indxOrientation,
                                             Int32        indxPlexMode,
                                             UInt16       paperWidth,
                                             UInt16       paperLength)
        {
            String seq;

            Double scale;

            Int32 dptWidth,
                  dptLength;

            scale = (pointsPerInch * 10.0) / sessionUPI;

            dptWidth  = (Int32) Math.Round ((scale * paperWidth));
            dptLength = (Int32) Math.Round ((scale * paperLength));

            if (PCLPaperTypes.getType(indxPaperType) !=
                    PCLPaperTypes.eEntryType.NotSet)
            {
                String tmpStr = PCLPaperTypes.getName(indxPaperType);
                Int32 len = tmpStr.Length + 1;

                seq = "\x1b" + "&n" +           // Alphanumeric ID
                               len +
                               "Wd" +           // ... Paper Type
                               tmpStr;

                prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
            }

            seq = "\x1b" + "&f" +
                           dptWidth +
                           "i" +             // Custom paper width
                           dptLength +
                           "J";              // Custom paper length
   
            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);

            seq = "\x1b" + "&l" +
                           "101" +           // custom paper ID   
                           "a" +             // Paper Size
                           PCLOrientations.getIdPCL(indxOrientation) +
                           "o" +             // Orientation
                           PCLPlexModes.getIdPCL(indxPlexMode) +
                           "s" +             // plex mode 
                           "1l" +            // perforation skip enable
                           "0E" +            // Top Margin (lines)
                  "\x1b" + "&a0L";           // Left Margin (columns)

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a g e O r i e n t a t i o n                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write 'page orientation' sequence.                    //
        // A change in orientation resets margins, HMI and VMI and page and   //
        // text lengths to the user-default environment values.               //
        // So we reset the Top and Left margins to zero here; can't reset     //
        // other values as we don't know what to set them to.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void pageOrientation(BinaryWriter prnWriter,
                                           String       orientId)
        {
            String seq;

            seq = "\x1b" + "&l" +               // page orientation
                           orientId +
                           "o"  +
                           "0E" +               // Top Margin (lines)
                  "\x1b" + "&a0L";              // Left Margin (columns)
            ;

            prnWriter.Write (seq.ToCharArray (), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a l e t t e E n t r y                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write Palette index entry in terms of colour          //
        // components.                                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void paletteEntry(BinaryWriter prnWriter,
                                        Int16        index,
                                        Int16        colour1,
                                        Int16        colour2,
                                        Int16        colour3)
        {
            String seq;
            
            seq = "\x1b" + "*v" +              
                           colour1 + "a" +      // Colour Component 1
                           colour2 + "b" +      // Colour Component 2
                           colour3 + "c" +      // Colour Component 3
                           index   + "I";       // Assign Colour Index

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a l e t t e P u s h P o p                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write Palette Stack Push (Store) or Pop (Recall)      //
        // sequence.                                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void palettePushPop(BinaryWriter prnWriter,
                                          ePushPop     pushPop)
        {
            String seq;

            if (pushPop == ePushPop.Push)
                seq = "\x1b" + "*p0P";          // Palette Stack: Push (Store)
            else
                seq = "\x1b" + "*p1P";          // Palette Stack: Pop (Recall)

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a l e t t e S i m p l e                                   I      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write Simple Colour sequence.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void paletteSimple(BinaryWriter   prnWriter,
                                         eSimplePalette palette)
        {
            String seq;
            
            if (palette == eSimplePalette.RGB)
                seq = "\x1b" + "*r3U";          // Simple Colour: RGB palette
            else if (palette == eSimplePalette.CMY)
                seq = "\x1b" + "*r-3U";         // Simple Colour: CMY palette
            else
                seq = "\x1b" + "*r1U";          // Simple Colour: K palette

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a l e t t e S i m p l e                                  I I     //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write Simple Colour sequence.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void paletteSimple (BinaryWriter prnWriter,
                                          Int16        palette)
        {
            String seq;

            seq = "\x1b" + "*r" + palette + "U";

            prnWriter.Write (seq.ToCharArray (), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a p e r S o u r c e                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write media (paper) source sequence.                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void paperSource(BinaryWriter prnWriter,
                                       Int16        trayId)
        {
            String seq;

            seq = "\x1b" + "&l" +               // tray identifier
                           trayId +
                           "H";

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a t t e r n D e f i n e                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate escape sequences to define a user-defined pattern.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void patternDefine(BinaryWriter prnWriter,
                                         Int16        patternID,
                                         Byte []      header,
                                         Byte []      pattern)
        {
            Int32 headerLen,
                  patternLen,
                  dataLen;

            String seq = "";

            headerLen  = header.Length;
            patternLen = pattern.Length;
            dataLen    = headerLen + patternLen;

            seq = "\x1b" + "*c" +
                  patternID + "g" +    // Pattern ID
                  dataLen + "W";

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);

            prnWriter.Write(header, 0, headerLen);

            prnWriter.Write(pattern, 0, patternLen);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a t t e r n D e l e t e                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate escape sequences to delete a user-defined pattern.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void patternDelete(BinaryWriter prnWriter,
                                         Int16 patternID)
        {
            String seq = "";

            seq = "\x1b" + "*c" +
                  patternID + "g" +    // Pattern ID
                  "2Q";                // Pattern Delete

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a t t e r n S e t                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate escape sequences to set the current pattern.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void patternSet (BinaryWriter prnWriter,
                                       ePatternType patternType,
                                       Int16        patternID)
        {
            String seq = "";

            if (patternType == ePatternType.SolidBlack)
            {
                seq = "\x1b" + "*v0T";
            }
            else if (patternType == ePatternType.SolidWhite)
            {
                seq = "\x1b" + "*v1T";
            }
            else if (patternType == ePatternType.Shading)
            {
                seq = "\x1b" + "*c" +
                               patternID + "G" +    // Shading percentage
                      "\x1b" + "*v2T";
            }
            else if (patternType == ePatternType.CrossHatch)
            {
                seq = "\x1b" + "*c" +
                               patternID + "G" +    // Cross hatch ID
                      "\x1b" + "*v3T";
            }
            else if (patternType == ePatternType.UserDefined)
            {
                seq = "\x1b" + "*c" +
                               patternID + "G" +    // Pattern ID
                      "\x1b" + "*v4T";
            }

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a t t e r n T r a n s p a r e n c y                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate escape sequences to set the current pattern transparency. //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void patternTransparency (BinaryWriter prnWriter,
                                                Boolean      opaque)
        {
            String seq = "";

            if (opaque)
            {
                seq = "\x1b" + "*v1O";
            }
            else
            {
                seq = "\x1b" + "*v0O";
            }

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p e r f o r a t i o n S k i p                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write 'perforation skip' sequence.                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void perforationSkip(BinaryWriter prnWriter,
                                           Boolean      enable)
        {
            String seq;

            if (enable)
                seq = "\x1b" + "&l1L";          // perforation skip enable
            else
                seq = "\x1b" + "&l0L";          // perforation skip disable

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p i c t u r e F r a m e                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate escape sequences to define the HP-GL/2 picture frame.     //
        // The frame is defined in terms of position, height, and width.      //
        // The values are provided as _unitsPerInch values, and (where        //
        // necessary) converted to decipoints.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void pictureFrame (BinaryWriter prnWriter,
                                         Int16 coordX,
                                         Int16 coordY,
                                         Int16 height,
                                         Int16 width)
        {
            String seq;

            Int16 dpHeight,
                  dpWidth;

            dpHeight = (Int16) ((height * pointsPerInch * 10) /
                                 sessionUPI);
            dpWidth  = (Int16) ((width  * pointsPerInch * 10) /
                                 sessionUPI);

            seq = "\x1b" + "*p" +
                           coordX + "x" +       // Position: Horizontal
                           coordY + "Y" +       // Position: Vertical
                  "\x1b" + "*c" +
                           dpWidth + "x" +      // Picture Frame: Horizontal
                           dpHeight + "y" +     // Picture Frame: Vertical
                           "0T";                // Set Anchor Point

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r i n t D i r e c t i o n                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write 'print direction' sequence.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void printDirection(BinaryWriter prnWriter,
                                          Int16        ccwAngle)
        {
            String seq;

            seq = "\x1b" + "&a" +               // print direction
                           ccwAngle +
                           "P";

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r a s t e r B e g i n                                       I      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write Raster definition and start sequences where     //
        // scaling is not required.                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void rasterBegin(BinaryWriter prnWriter,
                                       Int32        srcWidth,
                                       Int32        srcHeight,
                                       Int32        compressMode)
        {
            String seq;

            seq = "\x1b" + "*r0f" +             // Raster Presentation: Logical
                           srcWidth + "s" +     // Source Width
                           srcHeight + "t" +    // Source Height
                           "1A" +               // Start Raster Graphics: at X
                  "\x1b" + "*b" + 
                           compressMode + "M";  // Compression mode

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r a s t e r B e g i n                                      I I     //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write Raster definition and start sequences where     //
        // scaling is required.                                               //
        // Note that scaling via the Arbitrary Scaling method is not          //
        // supported on many printers, and these sequences will be ignored on //
        // devices which do not support them:                                 //
        //                                                                    //
        //      <esc>*t#H     Raster Width: Destinatation                     //
        //      <esc>*t#V     Raster Height: Destinatation                    //
        //      <esc>*r2A     Start Raster Graphics: Scale Mode at 0          //
        //      <esc>*r3A     Start Raster Graphics: Scale Mode at CAP        //
        //                                                                    //
        // Printers which DO NOT offer support include LJ1320                 //
        //                                             LJ4200                 //
        // Printers which DO offer support include     CLJ4600                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void rasterBegin(BinaryWriter prnWriter,
                                       Int32        srcWidth,
                                       Int32        srcHeight,
                                       Int32        srcResX,
                                       Int32        srcResY,
                                       Int32        destScalePercentX,
                                       Int32        destScalePercentY,
                                       Int32        compressionMode)
        {
            String seq;
            
            Int32 destWidth  = 0,
                  destHeight = 0;

            seq = "\x1b" + "*r0f" +             // Raster Presentation: Logical
                           srcWidth + "s" +     // Source Width
                           srcHeight + "T";     // Source Height

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);

            if ((destScalePercentX == 100) &&
                (destScalePercentY == 100))
            {
                seq = "\x1b" + "*r1A" +         // Start Raster Graphics: at X
                      "\x1b" + "*b" + 
                      compressionMode + "M";    // Compression mode
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                //                                                            //
                //------------------------------------------------------------//

                if (srcResX == 0)
                    srcResX = 96; // DefaultSourceBitmapResolution;
                else
                    srcResX = (Int32)(srcResX / 39.37);

                if (srcResY == 0)
                    srcResY = 96; // DefaultSourceBitmapResolution;
                else
                    srcResY = (Int32)(srcResY / 39.37);

                destWidth = ((srcWidth * 720) / srcResX) *
                              (destScalePercentX / 100);
                destHeight = ((srcHeight * 720) / srcResY) *
                              (destScalePercentY / 100);

                seq = "\x1b" + "*t" +
                               destWidth  +
                               "h" +            // Raster Width: Destinatation
                               destHeight +
                               "V" +            // Raster Height: Destinatation
                      "\x1b" + "*r3A" +         // Arbitrary scaling
                      "\x1b" + "*b" +
                      compressionMode + "M";    // Compression mode
            }

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r a s t e r C o m p r e s s i o n M o d e                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write Raster compression mode sequence.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void rasterCompressionMode(BinaryWriter prnWriter,
                                                 Int32        compressMode)
        {
            String seq;

            seq = "\x1b" + "*b" + 
                           compressMode + "M";  // Compression mode

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r a s t e r E n d                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write Raster End sequence.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void rasterEnd(BinaryWriter prnWriter)
        {
            String seq;

            seq = "\x1b" + "*rC";               // End Raster Graphics

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r a s t e r R e s o l u t i o n                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write Raster resolution sequence.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void rasterResolution (BinaryWriter prnWriter,
                                             Int32        indxRasterResolution,
                                             Boolean      valueIsIndex)
        {
            String seq;

            UInt16 rasterRes;

            if (valueIsIndex)
            {
                rasterRes =
                    PCLRasterResolutions.getValue (indxRasterResolution);
            }
            else
            {
                rasterRes = (UInt16) indxRasterResolution;
            }

            seq = "\x1b" + "*t" +
                           rasterRes +
                           "R";                 // Raster Resolution

            prnWriter.Write (seq.ToCharArray (), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r a s t e r T r a n s f e r P l a n e                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write raster transfer plane sequence.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void rasterTransferPlane (BinaryWriter prnWriter,
                                                Int32        rowLength,
                                                Byte[]       buffer)
        {
            String seq;

            seq = "\x1b" + "*b" +
                           rowLength +
                           "V";                 // Transfer Raster Data: Plane

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);

            if (rowLength > 0)
                prnWriter.Write(buffer, 0, rowLength);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r a s t e r T r a n s f e r R o w                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write raster transfer row sequence.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void rasterTransferRow (BinaryWriter prnWriter,
                                             Int32 rowLength,
                                             Byte[] buffer)
        {
            String seq;

            seq = "\x1b" + "*b" +
                           rowLength +
                           "W";                 // Transfer Raster Data: Row

            prnWriter.Write (seq.ToCharArray (), 0, seq.Length);

            if (rowLength > 0)
                prnWriter.Write (buffer, 0, rowLength);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e c t a n g l e O u t l i n e                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate escape sequences to draw a rectangle (as a set of four    //
        // filled rectangles).                                                //
        // The rectangle is defined in terms of position, height, width and   //
        // stroke (line thickness).                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void rectangleOutline(BinaryWriter prnWriter,
                                            Int16        coordX,
                                            Int16        coordY,
                                            Int16        height,
                                            Int16        width,
                                            Int16        stroke,
                                            Boolean      floating,
                                            Boolean      relative) 
        {
            String seq;

            String posSeq;

            if (floating)
                posSeq = "";
            else if (relative)
                posSeq = "\x1b" + "*p" +
                           (coordX > 0 ? "+" : "") +
                           coordX + "x" +       // Position: Horizontal
                           (coordY > 0 ? "+" : "") +
                           coordY + "Y";        // Position: Vertical
            else
                posSeq = "\x1b" + "*p" +
                           coordX + "x" +       // Position: Horizontal
                           coordY + "Y";        // Position: Vertical

            //----------------------------------------------------------------//

            seq = posSeq +                      // Position or null
                  "\x1b" + "*c" +
                           width + "a" +        // Rectangle Size: Horizontal
                           stroke + "b" +       // Rectangle Size: Vertical
                           "0P" +               // Fill Rectangle: Solid Area
                  "\x1b" + "*c" +
                           stroke + "a" +       // Rectangle Size: Horizontal
                           height + "b" +       // Rectangle Size: Vertical
                           "0P" +               // Fill Rectangle: Solid Area
                  "\x1b" + "*p+" +
                           height + "Y" +       // Position: Vertical
                  "\x1b" + "*c" +
                           (width + stroke) +
                                    "a" +       // Rectangle Size: Horizontal
                           stroke + "b" +       // Rectangle Size: Vertical
                           "0P" +               // Fill Rectangle: Solid Area
                  "\x1b" + "*p+" +
                           width + "X" +        // Position: Horizontal
                  "\x1b" + "*p-" +
                           height + "Y" +       // Position: Vertical
                  "\x1b" + "*c" +
                           stroke + "a" +       // Rectangle Size: Horizontal
                           height + "b" +       // Rectangle Size: Vertical
                           "0P";                // Fill Rectangle: Solid Area

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e c t a n g l e S h a d e d                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate escape sequences to draw a shaded rectangle.              //
        // The rectangle is defined in terms of position, height, width and   //
        // shade depth.                                                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void rectangleShaded(BinaryWriter prnWriter,
                                           Int16        coordX,
                                           Int16        coordY,
                                           Int16        height,
                                           Int16        width,
                                           Int16        shade,
                                           Boolean      floating,
                                           Boolean      relative)
        {
            String seq;

            String posSeq;

            if (floating)
                posSeq = "";
            else if (relative)
                posSeq = "\x1b" + "*p" +
                           (coordX > 0 ? "+" : "") +
                           coordX + "x" +       // Position: Horizontal
                           (coordY > 0 ? "+" : "") +
                           coordY + "Y";        // Position: Vertical
            else
                posSeq = "\x1b" + "*p" +
                           coordX + "x" +       // Position: Horizontal
                           coordY + "Y";        // Position: Vertical

            //----------------------------------------------------------------//

            seq = posSeq +                      // Position or null
                  "\x1b" + "*c" +
                           shade + "g" +        // Fill Shade
                           width + "a" +        // Rectangle Size: Horizontal
                           height + "b" +       // Rectangle Size: Vertical
                           "2P";                // Fill Rectangle: Shaded area

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e c t a n g l e S o l i d                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate escape sequences to draw a solid rectangle.               //
        // The rectangle is defined in terms of position, height, width and   //
        // shade depth.                                                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void rectangleSolid(BinaryWriter prnWriter,
                                          Int16        coordX,
                                          Int16        coordY,
                                          Int16        height,
                                          Int16        width,
                                          Boolean      white,
                                          Boolean      floating,
                                          Boolean      relative)
        {
            String seq;

            String posSeq;

            if (floating)
                posSeq = "";
            else if (relative)
                posSeq = "\x1b" + "*p" +
                           (coordX > 0 ? "+" : "") +
                           coordX + "x" +       // Position: Horizontal
                           (coordY > 0 ? "+" : "") +
                           coordY + "Y";        // Position: Vertical
            else
                posSeq = "\x1b" + "*p" +
                           coordX + "x" +       // Position: Horizontal
                           coordY + "Y";        // Position: Vertical

            //----------------------------------------------------------------//

            if (white)
            {
                seq = posSeq +                  // Position or null
                      "\x1b" + "*c" +
                               width  + "a" +   // Rectangle Size: Horizontal
                               height + "b" +   // Rectangle Size: Vertical
                               "1P";            // Fill Rectangle: Solid white
            }
            else
            {
                seq = posSeq +                  // Position or null
                      "\x1b" + "*c" +
                               width  + "a" +   // Rectangle Size: Horizontal
                               height + "b" +   // Rectangle Size: Vertical
                               "0P";            // Fill Rectangle: Solid
            }

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e c t a n g l e U s e r F i l l                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate escape sequences to draw a rectangle filled with a        //
        // (previously specified) user-defined pattern.                       //
        // The rectangle is defined in terms of position, height, width and   //
        // pattern identifier.                                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void rectangleUserFill(BinaryWriter prnWriter,
                                             Int16        coordX,
                                             Int16        coordY,
                                             Int16        height,
                                             Int16        width,
                                             Int16        patternID,
                                             Boolean      floating,
                                             Boolean      relative)
        {
            String seq;

            String posSeq;

            if (floating)
                posSeq = "";
            else if (relative)
                posSeq = "\x1b" + "*p" +
                           (coordX > 0 ? "+" : "") +
                           coordX + "x" +       // Position: Horizontal
                           (coordY > 0 ? "+" : "") +
                           coordY + "Y";        // Position: Vertical
            else
                posSeq = "\x1b" + "*p" +
                           coordX + "x" +       // Position: Horizontal
                           coordY + "Y";        // Position: Vertical

            //----------------------------------------------------------------//

            seq = posSeq +                      // Position or null
                  "\x1b" + "*c" +
                           patternID + "g" +    // Pattern ID
                           width + "a" +        // Rectangle Size: Horizontal
                           height + "b" +       // Rectangle Size: Vertical
                           "4P";                // Fill Rectangle: User pattern

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e c t a n g l e X H a t c h                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate escape sequences to draw a rectangle filled with a        //
        // predefined cross-hatch pattern.                                    //
        // The rectangle is defined in terms of position, height, width and   //
        // pattern id.                                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void rectangleXHatch(BinaryWriter prnWriter,
                                           Int16 coordX,
                                           Int16 coordY,
                                           Int16 height,
                                           Int16 width,
                                           Int16 hatch_id)
        {
            String seq;

            seq = "\x1b" + "*p" +
                           coordX + "x" +       // Position: Horizontal
                           coordY + "Y" +       // Position: Vertical
                  "\x1b" + "*c" +
                           hatch_id + "g" +     // Cross-hatch id
                           width + "a" +        // Rectangle Size: Horizontal
                           height + "b" +       // Rectangle Size: Vertical
                           "3P";                // Fill Rectangle: Cross-hatch

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t F o r e g r o u n d C o l o u r                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate sequence to set foreground colour.                        //
        // index is specified using a Byte parameter, so this method is       //
        // limited to use of palettes of 256 or fewer colours.                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void setForegroundColour (BinaryWriter prnWriter,
                                                Byte         colourIndex)
        {
            String seq;

            seq = "\x1b" + "*v" + colourIndex + "S";

            prnWriter.Write (seq.ToCharArray (), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t R O P                                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate sequence to set raster operation.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void setROP (BinaryWriter prnWriter,
                                   Int32        operation)
        {
            String seq;

            seq = "\x1b" + "*l" + operation + "O";

            prnWriter.Write (seq.ToCharArray (), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t T e x t L e n g t h                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write 'set Text Length' sequence.                     //
        // The height of each line is defined by the current VMI.             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void setTextLength(BinaryWriter prnWriter,
                                         Int16        lines)
        {
            String seq;

            seq = "\x1b" + "&l"  +              // tray identifier
                           lines +              // number of lines
                           "F";

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t V M I                                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write 'set Vertical Motion Index' sequence.           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void setVMI(BinaryWriter prnWriter,
                                  Single       increment)
        {
            String seq;

            seq = "\x1b" + "&l"   +             // set VMI
                           increment +          // 1/48 inch increments 
                           "C";

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s o u r c e T r a n s p a r e n c y                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate escape sequences to set the current source transparency.  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void sourceTransparency (BinaryWriter prnWriter,
                                               Boolean      opaque)
        {
            String seq = "";

            if (opaque)
            {
                seq = "\x1b" + "*v1N";
            }
            else
            {
                seq = "\x1b" + "*v0N";
            }

            prnWriter.Write (seq.ToCharArray (), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s t d J o b H e a d e r                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write standard job header sequences.                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void stdJobHeader(BinaryWriter prnWriter,
                                        String       pjlCommand)
        {
            String seq;

            seq = "\x1b" + "%-12345X";          // Universal Exit Language
            
            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);

            if (pjlCommand != "")
            {
                seq = pjlCommand + "\x0d" + "\x0a";

                prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
            }
             
            seq = "@PJL Enter Language = PCL" +
                  "\x0d" + "\x0a"   +
                  "\x1b" + "E" +                // Printer Reset
                  "\x1b" + "&u600D";            // Unit-of-Measure

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s t d J o b T r a i l e r                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write standard job trailer sequences.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void stdJobTrailer(BinaryWriter prnWriter,
                                         Boolean      formAsMacro,
                                         Int16        macroId)
        {
            String seq;

            if (formAsMacro)
            {
                macroControl(prnWriter, macroId, eMacroControl.Delete);
            }
            
            seq = "\x1b" + "E" +                // Printer Reset
                  "\x1b" + "%-12345X";          // Universal Exit Language

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s y m S e t D o w n l o a d C o d e                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate download symbol set code sequence.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void symSetDownloadCode (BinaryWriter prnWriter,
                                               UInt16       symSetNo)
        {
            String seq;

            seq = "\x1b" + "*c" + symSetNo + "R";

            prnWriter.Write (seq.ToCharArray (), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s y m S e t D o w n l o a d D e s c                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate download symbol set descriptor/data sequence.             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void symSetDownloadDesc (BinaryWriter prnWriter,
                                               UInt32       descLen)
        {
            String seq;

            seq = "\x1b" + "(f" + descLen + "W";

            prnWriter.Write (seq.ToCharArray (), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s y m S e t D o w n l o a d R e m o v e                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate symbol set control delete sequence.                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void symSetDownloadRemove (BinaryWriter prnWriter,
                                                 UInt16       symSetNo)
        {
            String seq;

            seq = "\x1b" + "*c" + symSetNo + "r2S";

            prnWriter.Write (seq.ToCharArray (), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s y m S e t D o w n l o a d S a v e                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate symbol set control save sequence.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void symSetDownloadSave (BinaryWriter prnWriter,
                                               Boolean      permanent)
        {
            String seq;

            if (permanent)
                seq = "\x1b" + "*c5S";
            else
                seq = "\x1b" + "*c4S";

            prnWriter.Write (seq.ToCharArray (), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t e x t                                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate escape sequences to write specified text string (using    //
        // current font) at specified position.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void text(BinaryWriter prnWriter,
                                Int16        coordX,
                                Int16        coordY,
                                Int16        spacing,
                                String       text)
        {
            String seq;

            seq = "\x1b" + "*p" +
                           coordX + "x" +       // Position: Horizontal
                           coordY + "Y";        // Position: Vertical

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);

            if (spacing == 0)
            {
                prnWriter.Write(text.ToCharArray(), 0, text.Length);
            }
            else
            {
                Int32 len = text.Length;

                seq = "\x1b" + "&f0S";          // Cursor position: Push

                for (int i = 0; i < len; i++)
                {
                    seq += text[i] +
                           "\x1b" + "&f1S" +    // Cursor position: Pop
                           "\x1b" + "*p+" +
                                    spacing +
                                    "X" +       // Position: Horizontal
                           "\x1b" + "&f0S";     // Cursor position: Push
                }

                prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t e x t R o t a t e d                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate escape sequences to write specified text string (using    //
        // current font) at specified position and (orthogonal) rotation.     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void textRotated (BinaryWriter prnWriter,
                                        Int16        coordX,
                                        Int16        coordY,
                                        Int16        spacing,
                                        Int16        ccwAngle,
                                        Boolean      resetRotation, 
                                        String       text)
        {
            String seq;

            seq = "\x1b" + "*p" +
                           coordX + "x" +       // Position: Horizontal
                           coordY + "Y";        // Position: Vertical

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);

            printDirection(prnWriter, ccwAngle);

            if (spacing == 0)
            {
                prnWriter.Write(text.ToCharArray(), 0, text.Length);
            }
            else
            {
                Int32 len = text.Length;

                seq = "\x1b" + "&f0S";          // Cursor position: Push

                for (int i = 0; i < len; i++)
                {
                    seq += text[i] +
                           "\x1b" + "&f1S" +    // Cursor position: Pop
                           "\x1b" + "*p+" +
                                    spacing +
                                    "X" +       // Position: Horizontal
                           "\x1b" + "&f0S";     // Cursor position: Push
                }

                prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
            }

            if (resetRotation)
                printDirection(prnWriter, 0);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t e x t P a r s i n g M e t h o d                           I      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write 'text parsing method' sequence.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void textParsingMethod (
            BinaryWriter                 prnWriter,
            PCLTextParsingMethods.eIndex eMethod)
        {
            String seq;

            Byte indx = (Byte) eMethod;

            seq = "\x1b" + "&t" +
                  PCLTextParsingMethods.getValue (indx).ToString () +
                  "P";

            prnWriter.Write (seq.ToCharArray (), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t e x t P a r s i n g M e t h o d                          I I     //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write 'text parsing method' sequence.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void textParsingMethod(BinaryWriter prnWriter,
                                             Int32        parseMethod)
        {
            String seq;

            seq = "\x1b" + "&t" + parseMethod.ToString () + "P";

            prnWriter.Write (seq.ToCharArray (), 0, seq.Length);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t r a n s p a r e n t P r i n t                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write 'transparent print' introduction sequence.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void transparentPrint(BinaryWriter prnWriter,
                                            Int16        byteCount)
        {
            String seq;

            seq = "\x1b" + "&p" +               // transparent print
                           byteCount +
                           "X";

            prnWriter.Write (seq.ToCharArray (), 0, seq.Length);
        }
    }
}
