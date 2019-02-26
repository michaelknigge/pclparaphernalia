using System;
using System.IO;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides primitive and macro operations for PCL XL print language.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class PCLXLWriter
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private const Int32 cSizeAttrReal32 = 7;
        private const Int32 cSizeAttrReal32XY = 11;

        private const Int32 cSizeAttrSint16 = 5;
        private const Int32 cSizeAttrSint16ArrayBase = 6;
        private const Int32 cSizeAttrSint16ArrayUnit = 2;
        private const Int32 cSizeAttrSint16Box = 11;
        private const Int32 cSizeAttrSint16XY = 7;

        private const Int32 cSizeAttrUbyte = 4;
        private const Int32 cSizeAttrUbyteArrayBase = 6;
        private const Int32 cSizeAttrUbyteArrayUnit = 1;

        private const Int32 cSizeAttrUint16 = 5;
        private const Int32 cSizeAttrUint16ArrayBase = 6;
        private const Int32 cSizeAttrUint16ArrayUnit = 2;
        private const Int32 cSizeAttrUint16Box = 11;
        private const Int32 cSizeAttrUint16XY = 7;

        private const Int32 cSizeAttrUint32 = 7;

        private const Int32 cSizeEmbedDataByte = 2;
        private const Int32 cSizeEmbedDataInt32 = 5;

        private const Int32 cSizeOperator = 1;

        public static readonly Byte[] rgbBlack    = { 0, 0, 0 }; // Black
        public static readonly Byte[] monoPalette = { 255, 0 };  // white, black

        public const UInt16 _sessionUPI      = 600;
        public const UInt16 _pointsPerInch   = 72;
        

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//


        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Font advance metrics.                                              //
        //                                                                    //
        // Values:                                                            //
        //  - are for the ASCII range (0x20-0x7e subset) only;                //
        //  - are for point size 360; assume linear scaling for other values; //
        //                                                                    //
        //  - are for units-per-inch of 600.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private const UInt16 cAdvanceTablePPI = 360;

        public static readonly Int16[] advances_ArialRegular =
        {
               833,  833, 1064, 1668, 1668, 2667, 2000,  572,     // 0x20 --> //
               999,  999, 1167, 1751,  833,  999,  833,  833,
              1668, 1668, 1668, 1668, 1668, 1668, 1668, 1668,     // 0x30 --> //
              1668, 1668,  833,  833, 1751, 1751, 1751, 1668,
              3045, 2000, 2000, 2166, 2166, 2000, 1832, 2333,     // 0x40 --> //
              2166,  833, 1500, 2000, 1668, 2499, 2166, 2333,
              2000, 2333, 2166, 2000, 1832, 2166, 2000, 2831,     // 0x50 --> //
              2000, 2000, 1832,  833,  833,  833, 1407, 1668,
               999, 1668, 1668, 1500, 1668, 1668,  833, 1668,     // 0x60 --> //
              1668,  666,  666, 1500,  666, 2499, 1668, 1668,
              1668, 1668,  999, 1500,  833, 1668, 1500, 2166,     // 0x70 --> //
              1500, 1500, 1500, 1001,  779, 1001, 1751
        };

        public static readonly Int16[] advances_ArialBold =
        {
               833,  999, 1422, 1668, 1668, 2667, 2166,  713,     // 0x20 --> //
               999,  999, 1167, 1751,  833,  999,  833,  833,
              1668, 1668, 1668, 1668, 1668, 1668, 1668, 1668,     // 0x30 --> //
              1668, 1668,  999,  999, 1751, 1751, 1751, 1832,
              2925, 2166, 2166, 2166, 2166, 2000, 1832, 2333,     // 0x40 --> //
              2166,  833, 1668, 2166, 1832, 2499, 2166, 2333,
              2000, 2333, 2166, 2000, 1832, 2166, 2000, 2831,     // 0x50 --> //
              2000, 2000, 1832,  999,  833,  999, 1751, 1668,
               999, 1668, 1832, 1668, 1832, 1668,  999, 1832,     // 0x60 --> //
              1832,  833,  833, 1668,  833, 2667, 1832, 1832,
              1832, 1832, 1167, 1668,  999, 1832, 1668, 2333,     // 0x70 --> //
              1668, 1668, 1500, 1167,  839, 1167, 1751
        };

        public static readonly Int16[] advances_Courier =
        {
              1800, 1800, 1800, 1800, 1800, 1800, 1800, 1800,     // 0x20 --> //
              1800, 1800, 1800, 1800, 1800, 1800, 1800, 1800,
              1800, 1800, 1800, 1800, 1800, 1800, 1800, 1800,     // 0x30 --> //
              1800, 1800, 1800, 1800, 1800, 1800, 1800, 1800,
              1800, 1800, 1800, 1800, 1800, 1800, 1800, 1800,     // 0x40 --> //
              1800, 1800, 1800, 1800, 1800, 1800, 1800, 1800,
              1800, 1800, 1800, 1800, 1800, 1800, 1800, 1800,     // 0x50 --> //
              1800, 1800, 1800, 1800, 1800, 1800, 1800, 1800,
              1800, 1800, 1800, 1800, 1800, 1800, 1800, 1800,     // 0x60 --> //
              1800, 1800, 1800, 1800, 1800, 1800, 1800, 1800,
              1800, 1800, 1800, 1800, 1800, 1800, 1800, 1800,     // 0x70 --> //
              1800, 1800, 1800, 1800, 1800, 1800, 1800
        };

        //--------------------------------------------------------------------//
        //                                                                    //
        // Notes:                                                             //
        //                                                                    //
        // 1    As C# does not support the concept of unions, many of the     //
        //      following methods use the BitConverter class.                 //
        //                                                                    //
        //      Note that the conversion operation assumes the Endian-ness of //
        //      the architecture on which it is running.                      //
        //      On Intel, this is Little-Endian, which is what is             //
        //      (fortunately) the setting we specify in the PCL XL stream     //
        //      header in the PCL XL class.                                   //
        //                                                                    //
        // 2    None of the methods do any bound checking on the buffer being //
        //      updated.                                                      //
        //      This is partially excused by the fact that we are writing     //
        //      very specific data (not user-supplied) so we (should) know at //
        //      design time how big a buffer we need.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // a d d A t t r R e a l 3 2                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Append a specified PCLXL Attribute sequence, with a 'real32'       //
        // data type, to the target buffer.                                   //
        // The data is supplied as a 'float' value.                           //
        // Generated sequence size is 7 bytes.                                // 
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void addAttrReal32(ref Byte[] buffer,
                                         ref Int32  bufIndex,
                                         PCLXLAttributes.eTag attributeTag,
                                         Single     valReal32)
        {
            Byte[] tempArray;

            buffer[bufIndex++] = (Byte)PCLXLDataTypes.eTag.Real32;

            tempArray = BitConverter.GetBytes(valReal32);

            buffer[bufIndex++] = tempArray[0];
            buffer[bufIndex++] = tempArray[1];
            buffer[bufIndex++] = tempArray[2];
            buffer[bufIndex++] = tempArray[3];

            buffer[bufIndex++] = (Byte)PCLXLAttrDefiners.eTag.Ubyte;
            buffer[bufIndex++] = (Byte)attributeTag;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // a d d A t t r R e a l 3 2 X Y                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Append a specified PCLXL Attribute sequence, with a 'real32_xy'    //
        // data type, to the target buffer.                                   //
        // The data is supplied as a pair of 'float' values.                  //
        // Generated sequence size is 11 bytes.                               // 
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void addAttrReal32XY(ref Byte[] buffer,
                                           ref Int32  bufIndex,
                                           PCLXLAttributes.eTag attributeTag,
                                           Single     valReal32_x,
                                           Single     valReal32_y)
        {
            Byte[] tempArray;

            buffer[bufIndex++] = (Byte)PCLXLDataTypes.eTag.Real32XY;

            tempArray = BitConverter.GetBytes(valReal32_x);

            buffer[bufIndex++] = tempArray[0];
            buffer[bufIndex++] = tempArray[1];
            buffer[bufIndex++] = tempArray[2];
            buffer[bufIndex++] = tempArray[3];

            tempArray = BitConverter.GetBytes(valReal32_y);

            buffer[bufIndex++] = tempArray[0];
            buffer[bufIndex++] = tempArray[1];
            buffer[bufIndex++] = tempArray[2];
            buffer[bufIndex++] = tempArray[3];

            buffer[bufIndex++] = (Byte)PCLXLAttrDefiners.eTag.Ubyte;
            buffer[bufIndex++] = (Byte)attributeTag;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // a d d A t t r S i n t 1 6                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Append a specified PCLXL Attribute sequence, with a 'sint16'       //
        // data type, to the target buffer.                                   //
        // The data is supplied as a 'short' value.                           //
        // Generated sequence size is 5 bytes.                                // 
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void addAttrSint16(ref Byte[] buffer,
                                         ref Int32  bufIndex,
                                         PCLXLAttributes.eTag attributeTag,
                                         Int16      valSint16)
        {
            Byte[] tempArray;

            buffer[bufIndex++] = (Byte)PCLXLDataTypes.eTag.Sint16;

            tempArray = BitConverter.GetBytes(valSint16);

            buffer[bufIndex++] = tempArray[0];
            buffer[bufIndex++] = tempArray[1];

            buffer[bufIndex++] = (Byte)PCLXLAttrDefiners.eTag.Ubyte;
            buffer[bufIndex++] = (Byte)attributeTag;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // a d d A t t r S i n t 1 6 A r r a y                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Append a specified PCLXL Attribute sequence, with a 'sint16_array' //
        // data type, to the target buffer.                                   //
        // The data is supplied as an array of 'short' values.                //
        // Generated sequence size is 6 + (2 * arraySize) bytes.              // 
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void addAttrSint16Array(ref Byte[] buffer,
                                              ref Int32  bufIndex,
                                              PCLXLAttributes.eTag attributeTag,
                                              Int16      arraySize, 
                                              Int16[]    arraySint16)
        {
            Byte[] tempArray;

            buffer[bufIndex++] = (Byte)PCLXLDataTypes.eTag.Sint16Array;
            buffer[bufIndex++] = (Byte)PCLXLDataTypes.eTag.Uint16;

            tempArray = BitConverter.GetBytes(arraySize);

            buffer[bufIndex++] = tempArray[0];
            buffer[bufIndex++] = tempArray[1];

            for (Int32 i = 0; i < arraySize; i++)
            {
                tempArray = BitConverter.GetBytes(arraySint16[i]);
                buffer[bufIndex++] = tempArray[0];
                buffer[bufIndex++] = tempArray[1];
            }
        
            buffer[bufIndex++] = (Byte)PCLXLAttrDefiners.eTag.Ubyte;
            buffer[bufIndex++] = (Byte)attributeTag;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // a d d A t t r S i n t 1 6 B o x                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Append a specified PCLXL Attribute sequence, with a 'sint16_box'   //
        // data type, to the target buffer.                                   //
        // The data is supplied as a quartet of 'short' values.               //
        // Generated sequence size is 11 bytes.                               // 
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void addAttrSint16Box(ref Byte[] buffer,
                                            ref Int32  bufIndex,
                                            PCLXLAttributes.eTag attributeTag,
                                            Int16      valSint16_x1,
                                            Int16      valSint16_y1,
                                            Int16      valSint16_x2,
                                            Int16      valSint16_y2)
        {
            Byte[] tempArray;

            buffer[bufIndex++] = (Byte)PCLXLDataTypes.eTag.Sint16Box;

            tempArray = BitConverter.GetBytes(valSint16_x1);

            buffer[bufIndex++] = tempArray[0];
            buffer[bufIndex++] = tempArray[1];

            tempArray = BitConverter.GetBytes(valSint16_y1);

            buffer[bufIndex++] = tempArray[0];
            buffer[bufIndex++] = tempArray[1];

            tempArray = BitConverter.GetBytes(valSint16_x2);

            buffer[bufIndex++] = tempArray[0];
            buffer[bufIndex++] = tempArray[1];

            tempArray = BitConverter.GetBytes(valSint16_y2);

            buffer[bufIndex++] = tempArray[0];
            buffer[bufIndex++] = tempArray[1];

            buffer[bufIndex++] = (Byte)PCLXLAttrDefiners.eTag.Ubyte;
            buffer[bufIndex++] = (Byte)attributeTag;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // a d d A t t r S i n t 1 6 X Y                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Append a specified PCLXL Attribute sequence, with a 'sint16_xy'    //
        // data type, to the target buffer.                                   //
        // The data is supplied as a pair of 'short' values.                  //
        // Generated sequence size is 7 bytes.                                // 
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void addAttrSint16XY(ref Byte[] buffer,
                                           ref Int32  bufIndex,
                                           PCLXLAttributes.eTag attributeTag,
                                           Int16      valSint16_x,
                                           Int16      valSint16_y)
        {
            Byte[] tempArray;

            buffer[bufIndex++] = (Byte)PCLXLDataTypes.eTag.Sint16XY;

            tempArray = BitConverter.GetBytes(valSint16_x);

            buffer[bufIndex++] = tempArray[0];
            buffer[bufIndex++] = tempArray[1];

            tempArray = BitConverter.GetBytes(valSint16_y);

            buffer[bufIndex++] = tempArray[0];
            buffer[bufIndex++] = tempArray[1];

            buffer[bufIndex++] = (Byte)PCLXLAttrDefiners.eTag.Ubyte;
            buffer[bufIndex++] = (Byte)attributeTag;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // a d d A t t r U b y t e                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Append a specified PCLXL Attribute sequence, with a 'ubyte'        //
        // data type, to the target buffer.                                   //
        // The data is supplied as a 'byte' value.                            //
        // Generated sequence size is 4 bytes.                                // 
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void addAttrUbyte(ref Byte[] buffer,
                                        ref Int32  bufIndex,
                                        PCLXLAttributes.eTag attributeTag,
                                        Byte       valUbyte)
        {
            buffer[bufIndex++] = (Byte)PCLXLDataTypes.eTag.Ubyte;

            buffer[bufIndex++] = valUbyte;

            buffer[bufIndex++] = (Byte)PCLXLAttrDefiners.eTag.Ubyte;
            buffer[bufIndex++] = (Byte)attributeTag;
        }

        
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // a d d A t t r U b y t e A r r a y                          (1)     //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Append a specified PCLXL Attribute sequence, with a 'ubyte_array'  //
        // data type, to the target buffer.                                   //
        // The data is supplied as an array of 'short' values.                //
        //                                                                    //
        // The assumption is made that all the values are in the range:       //
        //  0 <= x <= 255                                                     //
        // i.e. we only use the least significant byte of each value.         //
        // Generated sequence size is 6 + arraySize bytes.                    // 
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void addAttrUbyteArray(ref Byte[] buffer,
                                             ref Int32  bufIndex,
                                             PCLXLAttributes.eTag attributeTag,
                                             Int16      arraySize,
                                             Int16[]    arraySint16)
        {
            Byte[] tempArray;

            buffer[bufIndex++] = (Byte)PCLXLDataTypes.eTag.UbyteArray;
            buffer[bufIndex++] = (Byte)PCLXLDataTypes.eTag.Uint16;

            tempArray = BitConverter.GetBytes(arraySize);

            buffer[bufIndex++] = tempArray[0];
            buffer[bufIndex++] = tempArray[1];

            for (int i = 0; i < arraySize; i++)
            {
                tempArray = BitConverter.GetBytes(arraySint16[i]);
                buffer[bufIndex++] = tempArray[0];
            }
        
            buffer[bufIndex++] = (Byte)PCLXLAttrDefiners.eTag.Ubyte;
            buffer[bufIndex++] = (Byte)attributeTag;
        }
        
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // a d d A t t r U b y t e A r r a y                          (2)     //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Append a specified PCLXL Attribute sequence, with a 'ubyte_array'  //
        // data type, to the target buffer.                                   //
        // The data is supplied as an array of 'byte' values.                 //
        // Generated sequence size is 6 + arraySize bytes.                    // 
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void addAttrUbyteArray(ref Byte[] buffer,
                                             ref Int32  bufIndex,
                                             PCLXLAttributes.eTag attributeTag,
                                             Int16      arraySize,
                                             Byte[]     arrayUbyte)
        {
            Byte[] tempArray;

            buffer[bufIndex++] = (Byte)PCLXLDataTypes.eTag.UbyteArray;
            buffer[bufIndex++] = (Byte)PCLXLDataTypes.eTag.Uint16;

            tempArray = BitConverter.GetBytes(arraySize);

            buffer[bufIndex++] = tempArray[0];
            buffer[bufIndex++] = tempArray[1];

            for (Int32 i = 0; i < arraySize; i++)
            {
                buffer[bufIndex++] = arrayUbyte[i];
            }
        
            buffer[bufIndex++] = (Byte)PCLXLAttrDefiners.eTag.Ubyte;
            buffer[bufIndex++] = (Byte)attributeTag;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // a d d A t t r U b y t e A r r a y                          (3)     //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Append a specified PCLXL Attribute sequence, with a 'ubyte_array'  //
        // data type, to the target buffer.                                   //
        // The data is supplied as a string of (UTF-8) characters.            //
        //                                                                    //
        // The assumption is made that all the characters are in the ASCII    //
        // range (0x00 -> 0x7f) and are hence each represented using one      //
        // byte (i.e. no multi-byte characters are present).                  //
        // Generated sequence size is 6 + string-length bytes.                // 
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void addAttrUbyteArray(ref Byte[] buffer,
                                             ref Int32  bufIndex,
                                             PCLXLAttributes.eTag attributeTag,
                                             String     valString)
        {
            Byte[] tempArray;
            Int16 arraySize;

            buffer[bufIndex++] = (Byte)PCLXLDataTypes.eTag.UbyteArray;
            buffer[bufIndex++] = (Byte)PCLXLDataTypes.eTag.Uint16;

            arraySize = (Int16) valString.Length;
            tempArray = BitConverter.GetBytes(arraySize);

            buffer[bufIndex++] = tempArray[0];
            buffer[bufIndex++] = tempArray[1];

            for (Int32 i = 0; i < arraySize; i++)
            {
                buffer[bufIndex++] = (Byte) valString[i];
            }

            buffer[bufIndex++] = (Byte)PCLXLAttrDefiners.eTag.Ubyte;
            buffer[bufIndex++] = (Byte)attributeTag;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // a d d A t t r U i n t 1 6                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Append a specified PCLXL Attribute sequence, with a 'uint16'       //
        // data type, to the target buffer.                                   //
        // The data is supplied as a 'ushort' value.                          //
        // Generated sequence size is 5 bytes.                                // 
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void addAttrUint16(ref Byte[] buffer,
                                         ref Int32  bufIndex, 
                                         PCLXLAttributes.eTag attributeTag,
                                         UInt16     valUint16)
        {
            Byte[] tempArray;

            buffer[bufIndex++] = (Byte) PCLXLDataTypes.eTag.Uint16;
            
            tempArray            = BitConverter.GetBytes(valUint16);

            buffer[bufIndex++] = tempArray[0];
            buffer[bufIndex++] = tempArray[1];

            buffer[bufIndex++] = (Byte) PCLXLAttrDefiners.eTag.Ubyte;
            buffer[bufIndex++] = (Byte) attributeTag;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // a d d A t t r U i n t 1 6 A r r a y                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Append a specified PCLXL Attribute sequence, with a 'uint16_array' //
        // data type, to the target buffer.                                   //
        // The data is supplied as an array of 'ushort' values.               //
        // Generated sequence size is 6 + (2 * arraySize) bytes.              // 
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void addAttrUint16Array(ref Byte[] buffer,
                                              ref Int32  bufIndex,
                                              PCLXLAttributes.eTag attributeTag,
                                              Int16      arraySize,
                                              UInt16[]   arrayUint16)
        {
            Byte[] tempArray;

            buffer[bufIndex++] = (Byte)PCLXLDataTypes.eTag.Uint16Array;
            buffer[bufIndex++] = (Byte)PCLXLDataTypes.eTag.Uint16;

            tempArray = BitConverter.GetBytes(arraySize);

            buffer[bufIndex++] = tempArray[0];
            buffer[bufIndex++] = tempArray[1];

            for (Int32 i = 0; i < arraySize; i++)
            {
                tempArray = BitConverter.GetBytes(arrayUint16[i]);
                buffer[bufIndex++] = tempArray[0];
                buffer[bufIndex++] = tempArray[1];
            }

            buffer[bufIndex++] = (Byte)PCLXLAttrDefiners.eTag.Ubyte;
            buffer[bufIndex++] = (Byte)attributeTag;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // a d d A t t r U i n t 1 6 B o x                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Append a specified PCLXL Attribute sequence, with a 'uint16_box'   //
        // data type, to the target buffer.                                   //
        // The data is supplied as a quartet of 'ushort' values.              //
        // Generated sequence size is 11 bytes.                               // 
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void addAttrUint16Box(ref Byte[] buffer,
                                            ref Int32  bufIndex,
                                            PCLXLAttributes.eTag attributeTag,
                                            UInt16     valUint16_x1,
                                            UInt16     valUint16_y1,
                                            UInt16     valUint16_x2,
                                            UInt16     valUint16_y2)
        {
            Byte[] tempArray;

            buffer[bufIndex++] = (Byte)PCLXLDataTypes.eTag.Uint16Box;

            tempArray = BitConverter.GetBytes(valUint16_x1);

            buffer[bufIndex++] = tempArray[0];
            buffer[bufIndex++] = tempArray[1];

            tempArray = BitConverter.GetBytes(valUint16_y1);

            buffer[bufIndex++] = tempArray[0];
            buffer[bufIndex++] = tempArray[1];

            tempArray = BitConverter.GetBytes(valUint16_x2);

            buffer[bufIndex++] = tempArray[0];
            buffer[bufIndex++] = tempArray[1];

            tempArray = BitConverter.GetBytes(valUint16_y2);

            buffer[bufIndex++] = tempArray[0];
            buffer[bufIndex++] = tempArray[1];

            buffer[bufIndex++] = (Byte)PCLXLAttrDefiners.eTag.Ubyte;
            buffer[bufIndex++] = (Byte)attributeTag;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // a d d A t t r U i n t 1 6 X Y                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Append a specified PCLXL Attribute sequence, with a 'uint16_xy'    //
        // data type, to the target buffer.                                   //
        // The data is supplied as a pair of 'ushort' values.                 //
        // Generated sequence size is 7 bytes.                                // 
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void addAttrUint16XY(ref Byte[] buffer,
                                           ref Int32  bufIndex,
                                           PCLXLAttributes.eTag attributeTag,
                                           UInt16     valUint16_x,
                                           UInt16     valUint16_y)
        {
            Byte[] tempArray;

            buffer[bufIndex++] = (Byte)PCLXLDataTypes.eTag.Uint16XY;

            tempArray = BitConverter.GetBytes(valUint16_x);

            buffer[bufIndex++] = tempArray[0];
            buffer[bufIndex++] = tempArray[1];

            tempArray = BitConverter.GetBytes(valUint16_y);

            buffer[bufIndex++] = tempArray[0];
            buffer[bufIndex++] = tempArray[1];

            buffer[bufIndex++] = (Byte)PCLXLAttrDefiners.eTag.Ubyte;
            buffer[bufIndex++] = (Byte)attributeTag;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // a d d A t t r U i n t 3 2                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Append a specified PCLXL Attribute sequence, with a 'uint32'       //
        // data type, to the target buffer.                                   //
        // The data is supplied as a 'uint' value.                            //
        // Generated sequence size is 7 bytes.                                // 
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void addAttrUint32(ref Byte[] buffer,
                                         ref Int32  bufIndex,
                                         PCLXLAttributes.eTag attributeTag,
                                         UInt32     valUint32)
        {
            Byte[] tempArray;

            buffer[bufIndex++] = (Byte)PCLXLDataTypes.eTag.Uint32;

            tempArray = BitConverter.GetBytes(valUint32);

            buffer[bufIndex++] = tempArray[0];
            buffer[bufIndex++] = tempArray[1];
            buffer[bufIndex++] = tempArray[2];
            buffer[bufIndex++] = tempArray[3];

            buffer[bufIndex++] = (Byte)PCLXLAttrDefiners.eTag.Ubyte;
            buffer[bufIndex++] = (Byte)attributeTag;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // a d d E m b e d D a t a I n t r o                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return PCLXL Embedded Data introduction sequence.                  //
        // Generated sequence size is 2 bytes if dataLen value < 256          // 
        //                         or 5 bytes otherwise.                      // 
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void addEmbedDataIntro(ref Byte[] buffer,
                                             ref Int32  bufIndex,
                                             Int32      dataLen)
        {
            Byte[] tempArray;

            tempArray = BitConverter.GetBytes(dataLen);

            if (dataLen < 256)
            {
                buffer[bufIndex++] = (Byte) PCLXLEmbedDataDefs.eTag.Byte;
                buffer[bufIndex++] = tempArray[0];
            }
            else
            {
                buffer[bufIndex++] = (Byte) PCLXLEmbedDataDefs.eTag.Int;
                buffer[bufIndex++] = tempArray[0];
                buffer[bufIndex++] = tempArray[1];
                buffer[bufIndex++] = tempArray[2];
                buffer[bufIndex++] = tempArray[3];
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // a d d O p e r a t o r                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Append a specified PCLXL Operator.                                 //
        // Generated sequence size is 1 byte.                                 // 
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void addOperator(ref Byte[] buffer,
                                       ref Int32  bufIndex,
                                       PCLXLOperators.eTag operatorTag)
        {
            buffer[bufIndex++] = (Byte)operatorTag;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h a r A n g l e                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate SetCharAngle operator and associated Attribute List.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void charAngle (BinaryWriter prnWriter,
                                      Boolean      embeddedStream,
                                      Int16        charAngle)
        {
            Int32 lenBuf;

            Int32 indBuf;

            indBuf = 0;

            lenBuf = cSizeAttrSint16 +
                     cSizeOperator;

            Byte [] buffer = new Byte [lenBuf];

            //----------------------------------------------------------------//

            addAttrSint16 (ref buffer,
                           ref indBuf,
                           PCLXLAttributes.eTag.CharAngle,
                           charAngle);

            addOperator (ref buffer,
                         ref indBuf,
                         PCLXLOperators.eTag.SetCharAngle);

            writeStreamBlock (prnWriter, embeddedStream,
                              buffer, ref indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h a r B o l d                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate SetCharBoldValue operator and associated Attribute List.  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void charBold (BinaryWriter prnWriter,
                                     Boolean      embeddedStream,
                                     Single       charBoldValue)
        {
            Int32 lenBuf;

            Int32 indBuf;

            indBuf = 0;

            lenBuf = cSizeAttrReal32 +
                     cSizeOperator;

            Byte [] buffer = new Byte [lenBuf];

            //----------------------------------------------------------------//

            addAttrReal32 (ref buffer,
                           ref indBuf,
                           PCLXLAttributes.eTag.CharBoldValue,
                           charBoldValue);

            addOperator (ref buffer,
                         ref indBuf,
                         PCLXLOperators.eTag.SetCharBoldValue);

            writeStreamBlock (prnWriter, embeddedStream,
                              buffer, ref indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h a r S c a l e                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate SetCharScale operator and associated Attribute List.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void charScale (BinaryWriter prnWriter,
                                      Boolean      embeddedStream,
                                      Single       charScaleX,
                                      Single       charScaleY)
        {
            Int32 lenBuf;

            Int32 indBuf;

            indBuf = 0;

            lenBuf = cSizeAttrReal32XY +
                     cSizeOperator;

            Byte [] buffer = new Byte [lenBuf];

            //----------------------------------------------------------------//

            addAttrReal32XY (ref buffer,
                             ref indBuf,
                             PCLXLAttributes.eTag.CharScale,
                             charScaleX,
                             charScaleY);

            addOperator (ref buffer,
                         ref indBuf,
                         PCLXLOperators.eTag.SetCharScale);

            writeStreamBlock (prnWriter, embeddedStream,
                              buffer, ref indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h a r S h e a r                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate SetCharShear operator and associated Attribute List.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void charShear (BinaryWriter prnWriter,
                                      Boolean      embeddedStream,
                                      Single       charShearX,
                                      Single       charShearY)
        {
            Int32 lenBuf;

            Int32 indBuf;

            indBuf = 0;

            lenBuf = cSizeAttrReal32XY +
                     cSizeOperator;

            Byte [] buffer = new Byte [lenBuf];

            //----------------------------------------------------------------//

            addAttrReal32XY (ref buffer,
                             ref indBuf,
                             PCLXLAttributes.eTag.CharShear,
                             charShearX,
                             charShearY);

            addOperator (ref buffer,
                         ref indBuf,
                         PCLXLOperators.eTag.SetCharShear);

            writeStreamBlock (prnWriter, embeddedStream,
                              buffer, ref indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // e m b e d D a t a I n t r o                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate embedded data introduction.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void embedDataIntro (BinaryWriter prnWriter,
                                           Boolean embeddedStream,
                                           UInt16 hddrLen)
        {
            Int32 lenBuf;

            Int32 indBuf;

            indBuf = 0;

            if (hddrLen > 256) lenBuf = 5; else lenBuf = 2;

            lenBuf = cSizeAttrUint16 + cSizeOperator;

            Byte[] buffer = new Byte[lenBuf];

            //----------------------------------------------------------------//

            addEmbedDataIntro (ref buffer,
                               ref indBuf,
                               hddrLen);

            writeStreamBlock (prnWriter, embeddedStream,
                              buffer, ref indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // f o n t                                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate font selection operators and associated Attribute Lists   //
        // for specified font name, size and symbol set.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void font(BinaryWriter prnWriter,
                                Boolean      embeddedStream,
                                Single       pointSize,
                                UInt16       symbolSet,
                                String       fontName)
        {
            const Int32 lenHPFontName = 16;
            const Int32 lenBuf = 256;

            Byte[] buffer = new Byte[lenBuf];

            Int32 indBuf,
                  len;

            Single charSize;

            indBuf = 0;

            //----------------------------------------------------------------//

            charSize = (pointSize * _sessionUPI) / _pointsPerInch;

            len = fontName.Length;

            if (len < lenHPFontName)
            {
                String nameFiller = new String('\x20', lenHPFontName - len);

                addAttrUbyteArray(ref buffer,
                                  ref indBuf,
                                  PCLXLAttributes.eTag.FontName,
                                  fontName + nameFiller);
            }
            else
            {
                addAttrUbyteArray(ref buffer,
                                  ref indBuf,
                                  PCLXLAttributes.eTag.FontName,
                                  fontName);
            }

            addAttrUint16(ref buffer,
                          ref indBuf,
                          PCLXLAttributes.eTag.SymbolSet,
                          symbolSet);

            addAttrReal32(ref buffer,
                          ref indBuf,
                          PCLXLAttributes.eTag.CharSize,
                          charSize);

            addOperator(ref buffer,
                        ref indBuf,
                        PCLXLOperators.eTag.SetFont);

            writeStreamBlock(prnWriter, embeddedStream,
                             buffer, ref indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // f o n t C h a r B e g i n                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate BeginChar operator and associated Attribute List.         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void fontCharBegin (BinaryWriter prnWriter,
                                          Boolean embeddedStream,
                                          Int16 fontNameLen,
                                          Byte[] fontName)
        {
            Int32 lenBuf;

            Int32 indBuf;

            indBuf = 0;

            lenBuf = cSizeAttrUbyteArrayBase +
                     (cSizeAttrUbyteArrayUnit * fontNameLen) +
                     cSizeOperator;

            Byte[] buffer = new Byte[lenBuf];

            //----------------------------------------------------------------//

            addAttrUbyteArray (ref buffer,
                               ref indBuf,
                               PCLXLAttributes.eTag.FontName,
                               fontNameLen,
                               fontName);

            addOperator (ref buffer,
                         ref indBuf,
                         PCLXLOperators.eTag.BeginChar);


            writeStreamBlock (prnWriter, embeddedStream,
                              buffer, ref indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // f o n t C h a r E n d                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate EndChar operator.                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void fontCharEnd (BinaryWriter prnWriter,
                                        Boolean embeddedStream)
        {
            Int32 lenBuf;

            Int32 indBuf;

            indBuf = 0;

            lenBuf = cSizeOperator;

            Byte[] buffer = new Byte[lenBuf];

            //----------------------------------------------------------------//

            addOperator (ref buffer,
                         ref indBuf,
                         PCLXLOperators.eTag.EndChar);

            writeStreamBlock (prnWriter, embeddedStream,
                              buffer, ref indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // f o n t C h a r R e a d                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate ReadChar operator and associated Attribute List.          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void fontCharRead (BinaryWriter prnWriter,
                                         Boolean embeddedStream,
                                         UInt16 charCode,
                                         UInt16 charDataSize)
        {
            Int32 lenBuf;

            Int32 indBuf;

            indBuf = 0;

            lenBuf = cSizeAttrUint16 + cSizeAttrUint16 + cSizeOperator;
            Byte[] buffer = new Byte[lenBuf];

            //----------------------------------------------------------------//

            addAttrUint16 (ref buffer,
                           ref indBuf,
                           PCLXLAttributes.eTag.CharCode,
                           charCode);

            addAttrUint16 (ref buffer,
                           ref indBuf,
                           PCLXLAttributes.eTag.CharDataSize,
                           charDataSize);

            addOperator (ref buffer,
                         ref indBuf,
                         PCLXLOperators.eTag.ReadChar);

            writeStreamBlock (prnWriter, embeddedStream,
                              buffer, ref indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // f o n t H d d r B e g i n                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate BeginFontHeader operator and associated Attribute List.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void fontHddrBegin (BinaryWriter prnWriter,
                                          Boolean embeddedStream,
                                          Int16 fontNameLen,
                                          Byte [] fontName,
                                          Byte fontFormat)
        {
            Int32 lenBuf;

            Int32 indBuf;

            indBuf = 0;

            lenBuf = cSizeAttrUbyteArrayBase +
                     (cSizeAttrUbyteArrayUnit * fontNameLen) +
                     cSizeAttrUbyte +
                     cSizeOperator;

            Byte[] buffer = new Byte[lenBuf];

            //----------------------------------------------------------------//

            addAttrUbyteArray (ref buffer,
                               ref indBuf,
                               PCLXLAttributes.eTag.FontName,
                               fontNameLen,
                               fontName);

            addAttrUbyte (ref buffer,
                          ref indBuf,
                          PCLXLAttributes.eTag.FontFormat,
                          fontFormat);

            addOperator (ref buffer,
                         ref indBuf,
                         PCLXLOperators.eTag.BeginFontHeader);

            writeStreamBlock (prnWriter, embeddedStream,
                              buffer, ref indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // f o n t H d d r E n d                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate EndFontHeader operator.                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void fontHddrEnd (BinaryWriter prnWriter,
                                        Boolean embeddedStream)
        {
            Int32 lenBuf;

            Int32 indBuf;

            indBuf = 0;

            lenBuf = cSizeOperator;

            Byte[] buffer = new Byte[lenBuf];

            //----------------------------------------------------------------//

            addOperator (ref buffer,
                         ref indBuf,
                         PCLXLOperators.eTag.EndFontHeader);

            writeStreamBlock (prnWriter, embeddedStream,
                              buffer, ref indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // f o n t H d d r R e a d                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate ReadFontHeader operator and associated Attribute List.    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void fontHddrRead (BinaryWriter prnWriter,
                                         Boolean embeddedStream,
                                         UInt16 hddrLen)
        {
            Int32 lenBuf;

            Int32 indBuf;

            indBuf = 0;

            lenBuf = cSizeAttrUint16 + cSizeOperator;

            Byte[] buffer = new Byte[lenBuf];

            //----------------------------------------------------------------//

            addAttrUint16 (ref buffer,
                           ref indBuf,
                           PCLXLAttributes.eTag.FontHeaderLength,
                           hddrLen);

            addOperator (ref buffer,
                         ref indBuf,
                         PCLXLOperators.eTag.ReadFontHeader);

            writeStreamBlock (prnWriter, embeddedStream,
                              buffer, ref indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // f o n t R e m o v e                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate font remove operators and associated Attribute List for   //
        // for specified font name.                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void fontRemove(BinaryWriter prnWriter,
                                      Boolean embeddedStream,
                                      String fontName)
        {
            const Int32 lenHPFontName = 16;
            const Int32 lenBuf = 256;

            Byte[] buffer = new Byte[lenBuf];

            Int32 indBuf,
                  len;

            indBuf = 0;

            //----------------------------------------------------------------//

            len = fontName.Length;

            if (len < lenHPFontName)
            {
                String nameFiller = new String('\x20', lenHPFontName - len);

                addAttrUbyteArray(ref buffer,
                                  ref indBuf,
                                  PCLXLAttributes.eTag.FontName,
                                  fontName + nameFiller);
            }
            else
            {
                addAttrUbyteArray(ref buffer,
                                  ref indBuf,
                                  PCLXLAttributes.eTag.FontName,
                                  fontName);
            }

            addOperator(ref buffer,
                        ref indBuf,
                        PCLXLOperators.eTag.RemoveFont);

            writeStreamBlock(prnWriter, embeddedStream,
                             buffer, ref indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C h a r S i z e                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return 'session units' equivalent of specified pointsize.          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Single getCharSize(Single pointSize)
        {
            return (pointSize * _sessionUPI) / _pointsPerInch;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i m a g e B e g i n                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate BeginImage operator and associated attribute list to      //
        // define the metrics of a raster image.                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void imageBegin (BinaryWriter prnWriter,
                                       Boolean embeddedStream,
                                       UInt16 srcWidth,
                                       UInt16 srcHeight,
                                       UInt16 destWidth,
                                       UInt16 destHeight,
                                       PCLXLAttrEnums.eVal colorMapping,
                                       PCLXLAttrEnums.eVal colorDepth)
        {
            const Int32 lenBuf = 64;

            Byte[] buffer = new Byte[lenBuf];

            Int32 indBuf;

            indBuf = 0;

            PCLXLWriter.addAttrUbyte(ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.ColorMapping,
                                     (Byte)colorMapping);

            PCLXLWriter.addAttrUbyte(ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.ColorDepth,
                                     (Byte)colorDepth);

            PCLXLWriter.addAttrUint16(ref buffer,
                                      ref indBuf,
                                      PCLXLAttributes.eTag.SourceWidth,
                                      srcWidth);

            PCLXLWriter.addAttrUint16(ref buffer,
                                      ref indBuf,
                                      PCLXLAttributes.eTag.SourceHeight,
                                      srcHeight);

            PCLXLWriter.addAttrUint16XY(ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.DestinationSize,
                                        destWidth, destHeight);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.BeginImage);

            PCLXLWriter.writeStreamBlock(prnWriter, embeddedStream,
                                         buffer, ref indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i m a g e E n d                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate EndImage operator.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void imageEnd (BinaryWriter prnWriter,
                                     Boolean embeddedStream)
        {
            const Int32 lenBuf = 4;

            Byte[] buffer = new Byte[lenBuf];

            Int32 indBuf;

            indBuf = 0;

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.EndImage);

            PCLXLWriter.writeStreamBlock(prnWriter, embeddedStream,
                                         buffer, ref indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i m a g e R e a d                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate ReadImage operator and associated attribute list to       //
        // define the contents of (a block) of a raster image.                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void imageRead (BinaryWriter        prnWriter,
                                      Boolean             embeddedStream,
                                      UInt16              startLine,
                                      UInt16              blockHeight,
                                      PCLXLAttrEnums.eVal compressMode,
                                      Byte []             data)
        {
            const Int32 lenBuf = 64;

            Byte[] buffer = new Byte[lenBuf];

            Int32 indBuf,
                  dataLen;

            indBuf = 0;
            dataLen = data.Length;

            PCLXLWriter.addAttrUint16 (ref buffer,
                                       ref indBuf,
                                       PCLXLAttributes.eTag.StartLine,
                                       startLine);

            PCLXLWriter.addAttrUint16 (ref buffer,
                                       ref indBuf,
                                       PCLXLAttributes.eTag.BlockHeight,
                                       blockHeight);

            PCLXLWriter.addAttrUbyte (ref buffer,
                                      ref indBuf,
                                      PCLXLAttributes.eTag.CompressMode,
                                      (Byte) compressMode);

            PCLXLWriter.addAttrUbyte (ref buffer,
                                      ref indBuf,
                                      PCLXLAttributes.eTag.PadBytesMultiple,
                                      1);

            PCLXLWriter.addAttrUint32 (ref buffer,
                                       ref indBuf,
                                       PCLXLAttributes.eTag.BlockByteLength,
                                       (UInt16) dataLen);

            PCLXLWriter.addOperator (ref buffer,
                                     ref indBuf,
                                     PCLXLOperators.eTag.ReadImage);

            //----------------------------------------------------------------//

            PCLXLWriter.addEmbedDataIntro(ref buffer,
                                          ref indBuf,
                                          dataLen);

            PCLXLWriter.writeStreamBlock(prnWriter, embeddedStream,
                                         buffer, ref indBuf);

            PCLXLWriter.writeStreamBlock(prnWriter, embeddedStream,
                                          data, ref dataLen); 
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a g e B e g i n                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate BeginPage operator and associated attribute list.         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void pageBegin(BinaryWriter prnWriter,
                                     Int32        indxPaperSize,
                                     Int32        indxPaperType,
                                     Int32        indxPaperTray,
                                     Int32        indxOrientation,
                                     Int32        indxPlexMode,
                                     Boolean      flagFirstPage,
                                     Boolean      flagFrontFace)
        {
            const Int32 lenBuf = 1024;

            Byte[] buffer = new Byte[lenBuf];

            Int32 indBuf;

            Boolean flagSimplexJob = PCLPlexModes.isSimplex (indxPlexMode);

            indBuf = 0;

            if (indxOrientation < PCLOrientations.getCount ())
            {
                PCLXLWriter.addAttrUbyte (
                    ref buffer,
                    ref indBuf,
                    PCLXLAttributes.eTag.Orientation,
                    PCLOrientations.getIdPCLXL (indxOrientation));
            }

            if (indxPaperSize < PCLPaperSizes.getCount ())
            {
                PCLXLWriter.addAttrUbyte (
                    ref buffer,
                    ref indBuf,
                    PCLXLAttributes.eTag.MediaSize,
                    PCLPaperSizes.getIdPCLXL (indxPaperSize));
            }

            if (indxPaperTray >= 0)                 // -ve value indicates <not set>
            {
                if (indxPaperTray < 256)
                {
                    PCLXLWriter.addAttrUbyte (ref buffer,
                                       ref indBuf,
                                       PCLXLAttributes.eTag.MediaSource,
                                       (Byte)indxPaperTray);
                }
                else
                {
                    PCLXLWriter.addAttrUint16 (ref buffer,
                                       ref indBuf,
                                       PCLXLAttributes.eTag.MediaSource,
                                       (UInt16)indxPaperTray);

                }
            }

            if (flagFirstPage)
            {
                if ((indxPaperType < PCLPaperTypes.getCount ()) &&
                    (PCLPaperTypes.getType (indxPaperType) !=
                        PCLPaperTypes.eEntryType.NotSet))
                {
                    PCLXLWriter.addAttrUbyteArray (
                        ref buffer,
                        ref indBuf,
                        PCLXLAttributes.eTag.MediaType,
                        PCLPaperTypes.getName (indxPaperType));
                }

                if (flagSimplexJob)
                {
                    PCLXLWriter.addAttrUbyte(
                        ref buffer,
                        ref indBuf,
                        PCLXLAttributes.eTag.SimplexPageMode,
                        (Byte)PCLXLAttrEnums.eVal.eSimplexFrontSide);
                }
            }

            if (!flagSimplexJob)
            {
                Boolean flagLandscape =
                    PCLOrientations.isLandscape(indxOrientation);

                Byte binding = PCLPlexModes.getIdPCLXL(indxPlexMode,
                                                       flagLandscape);

                PCLXLWriter.addAttrUbyte(
                    ref buffer,
                    ref indBuf,
                    PCLXLAttributes.eTag.DuplexPageMode,
                    binding);

                if (flagFrontFace)
                {
                    PCLXLWriter.addAttrUbyte(
                        ref buffer,
                        ref indBuf,
                        PCLXLAttributes.eTag.DuplexPageSide,
                        (Byte)PCLXLAttrEnums.eVal.eFrontMediaSide);
                }
                else
                {
                    PCLXLWriter.addAttrUbyte(
                        ref buffer,
                        ref indBuf,
                        PCLXLAttributes.eTag.DuplexPageSide,
                        (Byte)PCLXLAttrEnums.eVal.eBackMediaSide);
                }
            }

            PCLXLWriter.addOperator (ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.BeginPage);

            PCLXLWriter.addAttrUint16XY (ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.PageOrigin,
                                        0, 0);

            PCLXLWriter.addOperator (ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.SetPageOrigin);

            prnWriter.Write (buffer, 0, indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a g e E n d                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate EndPage operator and associated attribute list.           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void pageEnd(BinaryWriter prnWriter,
                                   UInt16       pageCopies)
        {
            const Int32 lenBuf = 64;

            Byte[] buffer = new Byte[lenBuf];

            Int32 indBuf;

            indBuf = 0;

            addAttrUint16 (ref buffer,
                           ref indBuf,
                           PCLXLAttributes.eTag.PageCopies,
                           pageCopies);

            addOperator (ref buffer,
                         ref indBuf,
                         PCLXLOperators.eTag.EndPage);

            prnWriter.Write (buffer, 0, indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a t t e r n B e g i n                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate BeginRastPattern operator and associated attribute list.  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void patternBegin (BinaryWriter prnWriter,
                                         Boolean embeddedStream,
                                         Int16 patternID,
                                         UInt16 patWidth,
                                         UInt16 patHeight,
                                         UInt16 destWidth,
                                         UInt16 destHeight,
                                         PCLXLAttrEnums.eVal colorMapping,
                                         PCLXLAttrEnums.eVal colorDepth,
                                         PCLXLAttrEnums.eVal persistence,
                                         PCLXLAttrEnums.eVal compressMode)
        {
            const Int32 lenBuf = 64;

            Byte [] buffer = new Byte [lenBuf];

            Int32 indBuf;

            indBuf = 0;

            PCLXLWriter.addAttrUbyte (ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.ColorMapping,
                                     (Byte) colorMapping);

            PCLXLWriter.addAttrUbyte (ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.ColorDepth,
                                     (Byte) colorDepth);

            PCLXLWriter.addAttrUint16 (ref buffer,
                                      ref indBuf,
                                      PCLXLAttributes.eTag.SourceWidth,
                                      patWidth);

            PCLXLWriter.addAttrUint16 (ref buffer,
                                      ref indBuf,
                                      PCLXLAttributes.eTag.SourceHeight,
                                      patHeight);

            PCLXLWriter.addAttrUint16XY (ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.DestinationSize,
                                        destWidth, destHeight);

            PCLXLWriter.addAttrSint16 (ref buffer,
                                      ref indBuf,
                                      PCLXLAttributes.eTag.PatternDefineID,
                                      patternID);

            PCLXLWriter.addAttrUbyte (ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.PatternPersistence,
                                     (Byte) persistence);

            PCLXLWriter.addOperator (ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.BeginRastPattern);

            PCLXLWriter.writeStreamBlock (prnWriter, embeddedStream,
                                         buffer, ref indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a t t e r n D e f i n e                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate BeginRastPattern, ReadRastPattern and EndRastPattern      //
        // operators and associated attribute lists to sequences to define a  //
        // raster pattern.                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void patternDefine(BinaryWriter prnWriter,
                                         Boolean embeddedStream,
                                         Int16 patternID,
                                         UInt16 patWidth,
                                         UInt16 patHeight,
                                         UInt16 destWidth,
                                         UInt16 destHeight,
                                         PCLXLAttrEnums.eVal colorMapping,
                                         PCLXLAttrEnums.eVal colorDepth,
                                         PCLXLAttrEnums.eVal persistence,
                                         PCLXLAttrEnums.eVal compressMode,
                                         Byte[] pattern)
        {
            const Int32 lenBuf = 64;

            Byte[] buffer = new Byte[lenBuf];

            Int32 indBuf,
                  patLen;

            indBuf = 0;
            patLen = pattern.Length;

            PCLXLWriter.addAttrUbyte(ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.ColorMapping,
                                     (Byte) colorMapping);

            PCLXLWriter.addAttrUbyte(ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.ColorDepth,
                                     (Byte) colorDepth);

            PCLXLWriter.addAttrUint16(ref buffer,
                                      ref indBuf,
                                      PCLXLAttributes.eTag.SourceWidth,
                                      patWidth);

            PCLXLWriter.addAttrUint16(ref buffer,
                                      ref indBuf,
                                      PCLXLAttributes.eTag.SourceHeight,
                                      patHeight);

            PCLXLWriter.addAttrUint16XY(ref buffer,
                                        ref indBuf,
                                        PCLXLAttributes.eTag.DestinationSize,
                                        destWidth, destHeight);

            PCLXLWriter.addAttrSint16(ref buffer,
                                      ref indBuf,
                                      PCLXLAttributes.eTag.PatternDefineID,
                                      patternID);

            PCLXLWriter.addAttrUbyte(ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.PatternPersistence,
                                     (Byte) persistence);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.BeginRastPattern);
            
            //----------------------------------------------------------------//

            PCLXLWriter.addAttrUint16(ref buffer,
                                      ref indBuf,
                                      PCLXLAttributes.eTag.StartLine,
                                      0);

            PCLXLWriter.addAttrUint16(ref buffer,
                                      ref indBuf,
                                      PCLXLAttributes.eTag.BlockHeight,
                                      patHeight);

            PCLXLWriter.addAttrUbyte(ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.CompressMode,
                                     (Byte) compressMode);

            PCLXLWriter.addAttrUbyte(ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.PadBytesMultiple,
                                     1);

            PCLXLWriter.addAttrUint32 (ref buffer,
                                       ref indBuf,
                                       PCLXLAttributes.eTag.BlockByteLength,
                                       (UInt16) patLen);

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.ReadRastPattern);

            //----------------------------------------------------------------//

            PCLXLWriter.addEmbedDataIntro(ref buffer,
                                          ref indBuf,
                                          patLen);

            PCLXLWriter.writeStreamBlock(prnWriter, embeddedStream,
                                         buffer, ref indBuf);

            PCLXLWriter.writeStreamBlock (prnWriter, embeddedStream,
                                          pattern, ref patLen); 

            //----------------------------------------------------------------//

            PCLXLWriter.addOperator(ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.EndRastPattern);

            PCLXLWriter.writeStreamBlock(prnWriter, embeddedStream,
                                         buffer, ref indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a t t e r n E n d                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate EndRastPattern operator and associated attribute list.    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void patternEnd (BinaryWriter prnWriter,
                                         Boolean    embeddedStream)
        {
            const Int32 lenBuf = 16;

            Byte [] buffer = new Byte [lenBuf];

            Int32 indBuf;

            indBuf = 0;

            //----------------------------------------------------------------//

            PCLXLWriter.addOperator (ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.EndRastPattern);

            PCLXLWriter.writeStreamBlock (prnWriter, embeddedStream,
                                         buffer, ref indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p a t t e r n R e a d                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate ReadRastPattern operator and associated attribute list.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void patternRead (BinaryWriter         prnWriter,
                                         Boolean             embeddedStream,
                                         UInt16              startLine,
                                         UInt16              blockHeight,
                                         PCLXLAttrEnums.eVal compressMode,
                                         Byte []             pattern)
        {
            const Int32 lenBuf = 64;

            Byte [] buffer = new Byte [lenBuf];

            Int32 indBuf,
                  patLen;

            indBuf = 0;
            patLen = pattern.Length;

            //----------------------------------------------------------------//

            PCLXLWriter.addAttrUint16 (ref buffer,
                                      ref indBuf,
                                      PCLXLAttributes.eTag.StartLine,
                                      startLine);

            PCLXLWriter.addAttrUint16 (ref buffer,
                                      ref indBuf,
                                      PCLXLAttributes.eTag.BlockHeight,
                                      blockHeight);

            PCLXLWriter.addAttrUbyte (ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.CompressMode,
                                     (Byte) compressMode);

            PCLXLWriter.addAttrUbyte (ref buffer,
                                     ref indBuf,
                                     PCLXLAttributes.eTag.PadBytesMultiple,
                                     1);

            PCLXLWriter.addAttrUint32 (ref buffer,
                                       ref indBuf,
                                       PCLXLAttributes.eTag.BlockByteLength,
                                       (UInt16) patLen);

            PCLXLWriter.addOperator (ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.ReadRastPattern);

            //----------------------------------------------------------------//

            PCLXLWriter.addEmbedDataIntro (ref buffer,
                                          ref indBuf,
                                          patLen);

            PCLXLWriter.writeStreamBlock (prnWriter, embeddedStream,
                                         buffer, ref indBuf);

            PCLXLWriter.writeStreamBlock (prnWriter, embeddedStream,
                                          pattern, ref patLen);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e c t a n g l e                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate operators and attribute lists for a rectangle.            //
        // The outline (stroke) and fill properties of the rectangle are      //
        // defined externally (via Pen and Brush definitions).                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void rectangle (BinaryWriter prnWriter,
                                      Boolean embeddedStream,
                                      UInt16 coordX,
                                      UInt16 coordY,
                                      UInt16 height,
                                      UInt16 width)
        {
            Int32 lenBuf;

            Int32 indBuf;

            indBuf = 0;

            lenBuf = cSizeAttrUint16Box +
                     cSizeOperator;

            Byte[] buffer = new Byte[lenBuf];

            //----------------------------------------------------------------//

            addAttrUint16Box (ref buffer,
                              ref indBuf,
                              PCLXLAttributes.eTag.BoundingBox,
                              coordX,
                              coordY,
                              (UInt16) (coordX + width),
                              (UInt16) (coordY + height));

            addOperator (ref buffer,
                         ref indBuf,
                         PCLXLOperators.eTag.Rectangle);

            writeStreamBlock (prnWriter, embeddedStream,
                              buffer, ref indBuf);
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
            const Int32 lenStd = 32;

            Byte[] bufStd = new Byte[lenStd];

            String seq;

            Int32 indStd;

            seq = "\x1b" + "%-12345X";          // Universal Exit Language

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);

            if (pjlCommand != "")
            {
                seq = pjlCommand + "\x0d" + "\x0a";

                prnWriter.Write(seq.ToCharArray(), 0, seq.Length);
            }

            seq = "@PJL Enter Language = PCLXL" + "\x0d" + "\x0a" +
                  ") HP-PCL XL;2;0;" + "PCL Paraphernalia" + "\x0a";

            prnWriter.Write(seq.ToCharArray(), 0, seq.Length);

            indStd = 0;

            addAttrUbyte(ref bufStd,
                         ref indStd,
                         PCLXLAttributes.eTag.Measure,
                         (Byte)PCLXLAttrEnums.eVal.eInch);

            addAttrUint16XY(ref bufStd,
                            ref indStd,
                            PCLXLAttributes.eTag.UnitsPerMeasure,
                            _sessionUPI,
                            _sessionUPI);

            addAttrUbyte(ref bufStd,
                         ref indStd,
                         PCLXLAttributes.eTag.ErrorReport,
                         (Byte)PCLXLAttrEnums.eVal.eErrorPage);

            addOperator(ref bufStd,
                        ref indStd,
                        PCLXLOperators.eTag.BeginSession);

            addAttrUbyte(ref bufStd,
                         ref indStd,
                         PCLXLAttributes.eTag.SourceType,
                         (Byte)PCLXLAttrEnums.eVal.eDefaultDataSource);

            addAttrUbyte(ref bufStd,
                         ref indStd,
                         PCLXLAttributes.eTag.DataOrg,
                         (Byte)PCLXLAttrEnums.eVal.eBinaryLowByteFirst);
            
            addOperator(ref bufStd,
                        ref indStd,
                        PCLXLOperators.eTag.OpenDataSource);

            prnWriter.Write(bufStd, 0, indStd);
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
                                         Boolean      embeddedStream,
                                         String       streamName)
        {
            const Int32 lenStd = 64;

            Byte[] bufStd = new Byte[lenStd];

            Int32 indStd = 0;

            if (embeddedStream)
            {
                addAttrUbyteArray(ref bufStd,
                                  ref indStd,
                                  PCLXLAttributes.eTag.StreamName,
                                  streamName);
            
                addOperator(ref bufStd,
                            ref indStd,
                            PCLXLOperators.eTag.RemoveStream);
            }

            addOperator(ref bufStd,
                        ref indStd,
                        PCLXLOperators.eTag.CloseDataSource);
            
            addOperator(ref bufStd,
                        ref indStd,
                        PCLXLOperators.eTag.EndSession);

            // seq: Universal Exit Language
            bufStd[indStd++] = 0x1b;          // {esc}
            bufStd[indStd++] = 0x25;          // %
            bufStd[indStd++] = 0x2d;          // -
            bufStd[indStd++] = 0x31;          // 1
            bufStd[indStd++] = 0x32;          // 2
            bufStd[indStd++] = 0x33;          // 3
            bufStd[indStd++] = 0x34;          // 4
            bufStd[indStd++] = 0x35;          // 5
            bufStd[indStd++] = 0x58;          // X

            prnWriter.Write(bufStd, 0, indStd);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s t r e a m B e g i n                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write BeginStream operator and associated attribute   //
        // list.                                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void streamBegin(BinaryWriter prnWriter,
                                       String       streamName)
        {
            const Int32 lenBuf = 256;

            Byte[] buffer = new Byte[lenBuf];

            Int32 indBuf;

            indBuf = 0;

            addAttrUbyteArray (ref buffer,
                               ref indBuf,
                               PCLXLAttributes.eTag.StreamName,
                               streamName);

            addOperator (ref buffer,
                         ref indBuf,
                         PCLXLOperators.eTag.BeginStream);

            prnWriter.Write (buffer, 0, indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s t r e a m E n d                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write EndStream operator.                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void streamEnd(BinaryWriter prnWriter)
        {
            const Int32 lenBuf = 64;

            Byte[] buffer = new Byte[lenBuf];

            Int32 indBuf;

            indBuf = 0;

            addOperator (ref buffer,
                         ref indBuf,
                         PCLXLOperators.eTag.EndStream);

            prnWriter.Write (buffer, 0, indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s t r e a m E x e c                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write ExecStream operator and associated attribute    //
        // list.                                                              //
        //                                                                    //
        // Note that the existing application version does not use nested     //
        // streams; this is because:                                          //
        //  - PCL XL Class/Revision 2.0 does not support nested streams;      //
        //  - Even with a C/R 2.1 stream sent to a local LJ M475dn, nested    //
        //    streams produce a PCL XL error, so I've no way of testing.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void streamExec(BinaryWriter prnWriter,
                                      Boolean      embeddedStream,
                                      String       streamName)
        {
            const Int32 lenBuf = 64;

            Byte[] buffer = new Byte[lenBuf];

            Int32 indBuf;

            indBuf = 0;

            PCLXLWriter.addAttrUbyteArray (ref buffer,
                                          ref indBuf,
                                          PCLXLAttributes.eTag.StreamName,
                                          streamName);

            PCLXLWriter.addOperator (ref buffer,
                                    ref indBuf,
                                    PCLXLOperators.eTag.ExecStream);

            writeStreamBlock (prnWriter, embeddedStream,
                              buffer, ref indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s t r e a m H e a d e r                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and write standard user-defined stream header sequences.  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void streamHeader(BinaryWriter prnWriter,
                                        Boolean      embeddedStream,
                                        String       streamName)
        {
            const Int32 lenBuf = 64;

            Byte[] buffer = new Byte[lenBuf];

            Int32 indBuf;

            indBuf = 0;

            if (embeddedStream)
            {
                addAttrUbyteArray (ref buffer,
                                   ref indBuf,
                                   PCLXLAttributes.eTag.StreamName,
                                   streamName);

                addOperator (ref buffer,
                             ref indBuf,
                             PCLXLOperators.eTag.BeginStream);

                prnWriter.Write (buffer, 0, indBuf);
                indBuf = 0;
            }

            // stream header
            buffer[indBuf++] = 0x29;    // ) = binding format (binary L-E)
            buffer[indBuf++] = 0x20;    //   = reserved byte 
            buffer[indBuf++] = 0x48;    // H = stream class name
            buffer[indBuf++] = 0x50;    // P
            buffer[indBuf++] = 0x2d;    // -
            buffer[indBuf++] = 0x50;    // P
            buffer[indBuf++] = 0x43;    // C
            buffer[indBuf++] = 0x4c;    // L
            buffer[indBuf++] = 0x20;    //  
            buffer[indBuf++] = 0x58;    // X
            buffer[indBuf++] = 0x4c;    // L
            buffer[indBuf++] = 0x3b;    // ;
            buffer[indBuf++] = 0x32;    // 2 = protocol class number
            buffer[indBuf++] = 0x3b;    // ;
            buffer[indBuf++] = 0x30;    // 0 = protocol class revision
            buffer[indBuf++] = 0x3b;    // ; 
            buffer[indBuf++] = 0x63;    // c = comment
            buffer[indBuf++] = 0x6f;    // o
            buffer[indBuf++] = 0x6d;    // m
            buffer[indBuf++] = 0x6d;    // m
            buffer[indBuf++] = 0x65;    // e
            buffer[indBuf++] = 0x6e;    // n
            buffer[indBuf++] = 0x74;    // t
            buffer[indBuf++] = 0x0a;    //   = terminator

            addAttrUint16XY(ref buffer,
                            ref indBuf,
                            PCLXLAttributes.eTag.UnitsPerMeasure,
                            _sessionUPI,
                            _sessionUPI);

            addAttrUbyte(ref buffer,
                         ref indBuf,
                         PCLXLAttributes.eTag.Measure,
                         (Byte)PCLXLAttrEnums.eVal.eInch);

            addOperator(ref buffer,
                        ref indBuf,
                        PCLXLOperators.eTag.SetPageScale);

            writeStreamBlock(prnWriter, embeddedStream,
                             buffer, ref indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s t r e a m R e m o v e                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate user-defined stream remove operator and associated        //
        // Attribute List for for specified stream name.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void streamRemove(BinaryWriter prnWriter,
                                        String       streamName)
        {
            const Int32 lenBuf = 256;

            Byte[] buffer = new Byte[lenBuf];

            Int32 indBuf;

            indBuf = 0;

            //----------------------------------------------------------------//

            addAttrUbyteArray (ref buffer,
                               ref indBuf,
                               PCLXLAttributes.eTag.StreamName,
                               streamName);

            addOperator (ref buffer,
                         ref indBuf,
                         PCLXLOperators.eTag.RemoveStream);

            writeStreamBlock (prnWriter, false,
                              buffer, ref indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t e x t                                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate 'Text' operator and associated Attribute List for         //
        // specified text string, position and size.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void text(BinaryWriter prnWriter,
                                Boolean      embeddedStream,
                                Boolean      relativePoint,
                                Int16[]      advanceTable,
                                Single       pointSize,
                                Int16        coordX,
                                Int16        coordY,
                                String       text)
        {
            const Int32 lenBuf = 512; // need to cater for text up to 180 bytes?

            Byte[] buffer = new Byte[lenBuf];

            Int32 indBuf;

            Int16 textLen;

            Single scaleFactor,
                   advance;

            indBuf = 0;

            scaleFactor = pointSize / cAdvanceTablePPI;

            //----------------------------------------------------------------//

            textLen = (Int16)text.Length;

            Int16[] tmpAdvance = new Int16[textLen];

            for (Int32 i = 0; i < textLen; i++)
            {
                advance = advanceTable[((Byte)text[i] - 0x20)];
                tmpAdvance[i] = (Int16)(advance * scaleFactor);
            }

            addAttrSint16XY(ref buffer,
                            ref indBuf,
                            PCLXLAttributes.eTag.Point,
                            coordX, coordY);

            if (relativePoint)
                addOperator(ref buffer,
                            ref indBuf,
                            PCLXLOperators.eTag.SetCursorRel);
            else
                addOperator(ref buffer,
                            ref indBuf,
                            PCLXLOperators.eTag.SetCursor);

            addAttrUbyteArray(ref buffer,
                              ref indBuf,
                              PCLXLAttributes.eTag.TextData,
                              text);

            addAttrUbyteArray(ref buffer,
                              ref indBuf,
                              PCLXLAttributes.eTag.XSpacingData,
                              textLen,
                              tmpAdvance);

            addOperator(ref buffer,
                        ref indBuf,
                        PCLXLOperators.eTag.Text);

            //----------------------------------------------------------------//

            writeStreamBlock(prnWriter, embeddedStream,
                             buffer, ref indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t e x t A d v a n c e                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return advance associated with supplied text string and size.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int16 textAdvance (Int16[] advanceTable,
                                         Single  pointSize,
                                         String  text)
        {
            Int16 textLen;

            Single scaleFactor,
                   advance;

            Int16 totalAdvance = 0;

            scaleFactor = pointSize / cAdvanceTablePPI;

            //----------------------------------------------------------------//

            textLen = (Int16)text.Length;

            for (Int32 i = 0; i < textLen; i++)
            {
                advance = advanceTable[((Byte)text[i] - 0x20)];
                totalAdvance += (Int16)(advance * scaleFactor);
            }

            return totalAdvance;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t e x t C h a r                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate 'Text' operator and associated Attribute List for         //
        // specified single character, position and size.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void textChar (BinaryWriter prnWriter,
                                     Boolean embeddedStream,
                                     Int16 advance,
                                     Single pointSize,
                                     Int16 coordX,
                                     Int16 coordY,
                                     UInt16 codePoint)
        {
            const Int32 lenBuf = 512; // need to cater for text up to 180 bytes?

            Byte[] buffer = new Byte[lenBuf];

            Int16[] advanceArray = { 0x00 };
            UInt16[] codePointArray = { 0x00 };

            Int32 indBuf;

            Single scaleFactor;

            indBuf = 0;

            scaleFactor = pointSize / cAdvanceTablePPI;

            //----------------------------------------------------------------//

            addAttrSint16XY (ref buffer,
                            ref indBuf,
                            PCLXLAttributes.eTag.Point,
                            coordX, coordY);

            addOperator (ref buffer,
                        ref indBuf,
                        PCLXLOperators.eTag.SetCursor);

            codePointArray[0] = codePoint;
            advanceArray[0]   = advance;

            addAttrUint16Array (ref buffer,
                              ref indBuf,
                              PCLXLAttributes.eTag.TextData,
                              1,
                              codePointArray);

            addAttrUbyteArray (ref buffer,
                              ref indBuf,
                              PCLXLAttributes.eTag.XSpacingData,
                              1,
                              advanceArray);

            addOperator (ref buffer,
                        ref indBuf,
                        PCLXLOperators.eTag.Text);

            //----------------------------------------------------------------//

            writeStreamBlock (prnWriter, embeddedStream,
                             buffer, ref indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t e x t A n g l e d                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate 'Text' operator and associated Attribute List for         //
        // specified text string, position, size and angle.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void textAngled (BinaryWriter prnWriter,
                                       Boolean      embeddedStream,
                                       Int16[]      advanceTable,
                                       Single       pointSize,
                                       Int16        coordX,
                                       Int16        coordY,
                                       Int16        angle,
                                       String       text)
        {
            const Int32 lenBuf = 512; // need to cater for text up to 180 bytes?

            Byte[] buffer = new Byte[lenBuf];

            Int32 indBuf;

            Int16 textLen;

            Single scaleFactor,
                   advance;

            Double radians,
                   tangent;

            indBuf = 0;

            scaleFactor = pointSize / cAdvanceTablePPI;

            // Calculate the tangent of specified angle (given in degrees).
            // If angle is positive, make tangent negative to cause Y decrement
            // If angle is negative, make tangent positive to cause Y increment

            radians = angle * (Math.PI / 180);
            tangent = - Math.Tan(radians);

            //----------------------------------------------------------------//

            textLen = (Int16)text.Length;

            Int16[] tmpAdvanceX = new Int16[textLen];
            Int16[] tmpAdvanceY = new Int16[textLen];

            for (Int32 i = 0; i < textLen; i++)
            {
                advance = advanceTable[((Byte)text[i] - 0x20)];
                tmpAdvanceX[i] = (Int16)(advance * scaleFactor);
                tmpAdvanceY[i] = (Int16)(advance * scaleFactor * tangent);
            }

            addAttrSint16XY(ref buffer,
                            ref indBuf,
                            PCLXLAttributes.eTag.Point,
                            coordX, coordY);

            addOperator(ref buffer,
                        ref indBuf,
                        PCLXLOperators.eTag.SetCursor);

            addAttrUbyteArray(ref buffer,
                              ref indBuf,
                              PCLXLAttributes.eTag.TextData,
                              text);

            addAttrUbyteArray(ref buffer,
                              ref indBuf,
                              PCLXLAttributes.eTag.XSpacingData,
                              textLen,
                              tmpAdvanceX);

            addAttrSint16Array(ref buffer,
                               ref indBuf,
                               PCLXLAttributes.eTag.YSpacingData,
                               textLen,
                               tmpAdvanceY);

            addOperator(ref buffer,
                        ref indBuf,
                        PCLXLOperators.eTag.Text);

            //----------------------------------------------------------------//

            writeStreamBlock(prnWriter, embeddedStream,
                             buffer, ref indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e O p e r a t o r                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write specified operator.                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void writeOperator(BinaryWriter prnWriter,
                                         PCLXLOperators.eTag opTag,
                                         Boolean embeddedStream)
        {
            const Int32 lenBuf = 64;

            Byte[] buffer = new Byte[lenBuf];

            Int32 indBuf;

            indBuf = 0;

            addOperator (ref buffer,
                         ref indBuf,
                         opTag);

            writeStreamBlock (prnWriter, embeddedStream, buffer, ref indBuf);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // w r i t e S t r e a m B l o c k                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write out contents of supplied buffer(s), pre-pending this with a  //
        // ReadStream Operator and its associated Attribute List if writing   //
        // an embedded stream.                                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void writeStreamBlock(BinaryWriter prnWriter,
                                            Boolean embeddedStream,
                                            Byte[] buffer,
                                            ref Int32 indBuf)
        {
            if (embeddedStream)
            {
                Int32 lenTemp;
                Int32 indTemp;

                indTemp = 0;

                lenTemp = cSizeAttrUint32 + cSizeOperator;

                if (indBuf > 256)
                    lenTemp += cSizeEmbedDataInt32;
                else
                    lenTemp += cSizeEmbedDataByte;

                Byte[] bufTemp = new Byte[lenTemp];
                
                addAttrUint32 (ref bufTemp,
                              ref indTemp,
                              PCLXLAttributes.eTag.StreamDataLength,
                              (UInt32) indBuf);

                addOperator (ref bufTemp,
                            ref indTemp,
                            PCLXLOperators.eTag.ReadStream);

                addEmbedDataIntro (ref bufTemp,
                                  ref indTemp,
                                  indBuf);

                prnWriter.Write (bufTemp, 0, indTemp);
            }

            //----------------------------------------------------------------//

            prnWriter.Write (buffer, 0, indBuf);
            indBuf = 0;
        }
    }
}
