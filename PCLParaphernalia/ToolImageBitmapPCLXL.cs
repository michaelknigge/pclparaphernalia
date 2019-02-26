using System;
using System.IO;
using System.Windows;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides PCL XL support for the ImageBitmap tool.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class ToolImageBitmapPCLXL
    {
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e I m a g e                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate print equivalent of bitmap image.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void generateImage(BinaryWriter prnWriter,
                                          Single       destPosX,
                                          Single       destPosY,
                                          Int32        destScalePercentX,
                                          Int32        destScalePercentY)
        {
            const Int32 sizeStd = 1024;

            Byte[] bufStd = new Byte[sizeStd];

            Int32 srcWidth = 0,
                  srcHeight = 0,
                  srcResX = 0,
                  srcResY = 0;

            UInt32 srcCompression = 0,
                   srcPaletteEntries = 0;

            UInt16 srcBitsPerPixel = 0;

            Boolean srcBlackWhite = false;

            ToolImageBitmapCore.getBmpInfo(ref srcWidth,
                                           ref srcHeight,
                                           ref srcBitsPerPixel,
                                           ref srcCompression,
                                           ref srcResX,
                                           ref srcResY,
                                           ref srcPaletteEntries,
                                           ref srcBlackWhite);

            if (srcCompression != 0)
            {
                MessageBox.Show("Bitmaps: compressed formats not supported",
                                "Bitmap file",
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);
                return;
            }
            else if ((srcBitsPerPixel != 1) &&
                     (srcBitsPerPixel != 4) &&
                     (srcBitsPerPixel != 24))
            {
                MessageBox.Show("Bitmaps: only 1-, 4- and 24-bit supported",
                                "Bitmap file",
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);
                return;
            }
            else if (srcHeight < 0)
            {
                MessageBox.Show("Bitmaps: top-down DIBs not supported",
                                "Bitmap file",
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);
                return;
            }

            generateImageHeader(prnWriter,
                                srcBitsPerPixel,
                                srcWidth,
                                srcHeight,
                                srcResX,
                                srcResY,
                                destPosX,
                                destPosY,
                                destScalePercentX,
                                destScalePercentY,
                                srcPaletteEntries,
                                srcBlackWhite);

            generateImageData(prnWriter,
                              srcBitsPerPixel,
                              srcWidth,
                              srcHeight);

            generateImageTrailer(prnWriter);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e I m a g e D a t a                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate ReadImage operator(s) and associated embedded data.       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void generateImageData(BinaryWriter prnWriter,
                                              UInt16       srcBitsPerPixel,
                                              Int32        srcWidth,
                                              Int32        srcHeight)
        {
            const Int32 maxImageBlock = 2048;
            const Int32 sizeStd = 64;

            Byte[] bufStd = new Byte[sizeStd];

            Int32 indStd = 0;

            Boolean firstBlock = true,
                    indexed = true;

            Int32 bytesPerRow,
                  padBytes;

            Int32 imageCrntLine,
                  imageHeight,
                  imageRowMult,
                  imageBlockHeight,
                  imageBlockSize;

            //------------------------------------------------------------//

            if (srcBitsPerPixel == 1)
            {
                indexed = true;
                bytesPerRow = srcWidth / 8;
                if ((srcWidth % 8) != 0)
                    bytesPerRow++;
            }
            else if (srcBitsPerPixel == 4)
            {
                indexed = true;
                bytesPerRow = srcWidth / 2;
                if ((srcWidth % 2) != 0)
                    bytesPerRow++;
            }
            else // if (srcBitsPerPixel == 24)
            {
                indexed = false;
                bytesPerRow = srcWidth * 3;
            }

            padBytes = bytesPerRow % 4;

            if (padBytes != 0)
            {
                padBytes = 4 - padBytes;
                bytesPerRow += padBytes;
            }

            imageCrntLine = 0;
            imageHeight = srcHeight;
            imageRowMult = (Int32)Math.Floor((Double)maxImageBlock /
                                             (Double)bytesPerRow);

            if (imageRowMult == 0)
                imageRowMult = 1;

            Byte[] bufSub = new Byte[bytesPerRow];

            for (Int32 i = 0; i < imageHeight; i += imageRowMult)
            {
                if ((imageCrntLine + imageRowMult) >= imageHeight)
                    imageBlockHeight = imageHeight - imageCrntLine;
                else
                    imageBlockHeight = imageRowMult;

                imageBlockSize = imageBlockHeight * bytesPerRow;

                PCLXLWriter.addAttrUint16(ref bufStd,
                                    ref indStd,
                                    PCLXLAttributes.eTag.StartLine,
                                    (UInt16)imageCrntLine);

                PCLXLWriter.addAttrUint16(ref bufStd,
                                    ref indStd,
                                    PCLXLAttributes.eTag.BlockHeight,
                                    (UInt16)imageBlockHeight);

                PCLXLWriter.addAttrUbyte(ref bufStd,
                                   ref indStd,
                                   PCLXLAttributes.eTag.CompressMode,
                                   (Byte)PCLXLAttrEnums.eVal.eNoCompression);

                PCLXLWriter.addOperator(ref bufStd,
                                  ref indStd,
                                  PCLXLOperators.eTag.ReadImage);

                PCLXLWriter.addEmbedDataIntro(ref bufStd,
                                        ref indStd,
                                        imageBlockSize);

                prnWriter.Write(bufStd, 0, indStd);
                indStd = 0;

                for (Int32 j = 0; j < imageRowMult; j++)
                {
                    if ((i + j) >= imageHeight)
                        j = imageRowMult;
                    else
                    {
                        ToolImageBitmapCore.getNextImageBlock(ref bufSub,
                                                              bytesPerRow,
                                                              firstBlock);

                        //     if (srcBitsPerPixel == 24)
                        if (! indexed)
                        {
                            // change BGR components to RGB //

                            Byte temp;
                            Int32 endLine = bytesPerRow - 2;

                            for (Int32 k = 0; k <= endLine; k += 3)
                            {
                                if (bufSub[k] != bufSub[k + 2])
                                {
                                    temp = bufSub[k];
                                    bufSub[k] = bufSub[k + 2];
                                    bufSub[k + 2] = temp;
                                }
                            }
                        }

                        firstBlock = false;

                        prnWriter.Write(bufSub, 0, bytesPerRow);
                    }
                }

                imageCrntLine += imageBlockHeight;
            }
        }
        
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e I m a g e H e a d e r                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write image initialisation sequences to output file.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void generateImageHeader(BinaryWriter prnWriter,
                                                UInt16       srcBitsPerPixel,
                                                Int32        srcWidth,
                                                Int32        srcHeight,
                                                Int32        srcResX,
                                                Int32        srcResY,
                                                Single       destPosX,
                                                Single       destPosY,
                                                Int32        destScalePercentX,
                                                Int32        destScalePercentY,
                                                UInt32       srcPaletteEntries,
                                                Boolean      srcBlackWhite)
        {
            const Int32 sizeStd = 256;

            Byte[] bufStd = new Byte[sizeStd];

            Int32 indStd = 0;

            Int32 destWidth = 0,
                  destHeight = 0;
            
            UInt32 paletteEntries = 0,
                   paletteSize = 0;

            Byte colourDepth = 0,
                 colourMapping = 0,
                 colourSpace = 0;
       
            //----------------------------------------------------------------//
            //                                                                //
            // Calculate destination size.                                    //
            //                                                                //
            //----------------------------------------------------------------//

            if (srcResX == 0)
                srcResX = 96; // DefaultSourceBitmapResolution;
            else
                srcResX = (Int32)(srcResX / 39.37);

            if (srcResY == 0)
                srcResY = 96; // DefaultSourceBitmapResolution;
            else
                srcResY = (Int32)(srcResY / 39.37);

            destWidth = ((srcWidth * PCLXLWriter._sessionUPI) / srcResX) *
                          (destScalePercentX / 100);
            destHeight = ((srcHeight * PCLXLWriter._sessionUPI) / srcResY) *
                          (destScalePercentY / 100);

            //----------------------------------------------------------------//
            //                                                                //
            // Set position.                                                  //
            //                                                                //
            //----------------------------------------------------------------//

            PCLXLWriter.addAttrSint16XY(ref bufStd,
                                  ref indStd,
                                  PCLXLAttributes.eTag.Point,
                                  (Int16)(destPosX * PCLXLWriter._sessionUPI),
                                  (Int16)(destPosY * PCLXLWriter._sessionUPI));

            PCLXLWriter.addOperator(ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.SetCursor);

            //----------------------------------------------------------------//
            //                                                                //
            // Set colour space.                                              //
            //                                                                //
            // Note that we only support the following bitmap types:          //
            //                                                                //
            //   -  1-bit black and white:                                    //
            //      Colour space: Gray (1 plane)                              //
            //      Encoding:     indirect-pixel                              //
            //      Palette:      elements = 2 (= 2^1)                        //
            //                    planes   = 1                                //
            //                    length   = 2 (= 2 * 1) bytes.               //
            //      Image data:   Each image pixel is defined by 1 bit        //
            //                    which is used an an index into the          //
            //                    2-element palette.                          // 
            //                                                                //
            //   -  1-bit colour                                              //
            //      Colour space: RGB (3 plane)                               //
            //      Encoding:     indirect-pixel                              //
            //      Palette:      elements = 2 (= 2^1)                        //
            //                    planes   = 3                                //
            //                    length   = 6 (= 2 * 3) bytes.               //
            //      Image data:   Each image pixel is defined by 1 bit        //
            //                    which is used an an index into the          //
            //                    2-element palette.                          // 
            //                                                                //
            //   -  4-bit:                                                    //
            //      Colour space: RGB (3-plane)                               //
            //      Encoding:     indirect-pixel                              //
            //      Palette:      elements = 16 (= 2^4)                       //
            //                    planes   = 3                                //
            //                    length   = 48 (= 16 * 3) bytes.             //
            //      Image data:   Each group of 4 bits defines an image       //
            //                    pixel by use as an index into the           //
            //                    16-element palette.                         // 
            //                                                                //
            //   -  24-bit:                                                   //
            //      Colour space: RGB (3-plane)                               //
            //      Encoding:     direct-pixel                                //
            //      Palette:      none                                        //
            //      Image data:   Each group of 24 bits defines an image      //
            //                    pixel as three 8-bit values, directly       //
            //                    specifying the RGB values.                  //
            //                                                                //
            //----------------------------------------------------------------//

            if (srcBlackWhite)
            {
                colourSpace    = (Byte)PCLXLAttrEnums.eVal.eGray;
                colourDepth    = (Byte)PCLXLAttrEnums.eVal.e1Bit;
                colourMapping  = (Byte)PCLXLAttrEnums.eVal.eIndexedPixel;
                paletteEntries = 2;
                paletteSize    = 2;
            }
            else if (srcBitsPerPixel == 1)
            {
                colourSpace    = (Byte)PCLXLAttrEnums.eVal.eRGB;
                colourDepth    = (Byte)PCLXLAttrEnums.eVal.e1Bit;
                colourMapping  = (Byte)PCLXLAttrEnums.eVal.eIndexedPixel;
                paletteEntries = 0x00000001 << 1;
                paletteSize    = 3 * paletteEntries;    // one per plane
            }
            else if (srcBitsPerPixel == 4)
            {
                colourSpace    = (Byte)PCLXLAttrEnums.eVal.eRGB;
                colourDepth    = (Byte)PCLXLAttrEnums.eVal.e4Bit;
                colourMapping  = (Byte)PCLXLAttrEnums.eVal.eIndexedPixel;
                paletteEntries = 0x00000001 << 4;
                paletteSize    = 3 * paletteEntries;    // one per plane
            }
            else if (srcBitsPerPixel == 24)
            {
                colourSpace    = (Byte)PCLXLAttrEnums.eVal.eRGB;
                colourDepth    = (Byte)PCLXLAttrEnums.eVal.e8Bit;
                colourMapping  = (Byte)PCLXLAttrEnums.eVal.eDirectPixel;
                paletteEntries = 0;
                paletteSize    = 0;
            }

            PCLXLWriter.addOperator(ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.PushGS);

            PCLXLWriter.addAttrUbyte(ref bufStd,
                               ref indStd,
                               PCLXLAttributes.eTag.ColorSpace,
                               colourSpace);

            if (paletteEntries != 0)
            {
                PCLXLWriter.addAttrUbyte(ref bufStd,
                                   ref indStd,
                                   PCLXLAttributes.eTag.PaletteDepth,
                                   (byte)PCLXLAttrEnums.eVal.e8Bit);

                if (srcBlackWhite)
                {
                    byte[] tempUByteArray = new byte[2];

                    tempUByteArray[0] = 0;
                    tempUByteArray[1] = 255;

                    PCLXLWriter.addAttrUbyteArray(ref bufStd,
                                            ref indStd,
                                            PCLXLAttributes.eTag.PaletteData,
                                            2,
                                            tempUByteArray);
                }
                else
                {
                    Int32 offset;

                    Byte red = 0x00,
                         green = 0x00,
                         blue = 0x00;

                    Byte[] tempUByteArray = new Byte[paletteSize];

                    for (Int32 i = 0; i < srcPaletteEntries; i++)
                    {
                        offset = i * 3;

                        ToolImageBitmapCore.getBmpPaletteEntry(i,
                                                               ref red,
                                                               ref green,
                                                               ref blue);

                        tempUByteArray[offset] = red;
                        tempUByteArray[offset + 1] = green;
                        tempUByteArray[offset + 2] = blue;
                    }

                    PCLXLWriter.addAttrUbyteArray(ref bufStd,
                                            ref indStd,
                                            PCLXLAttributes.eTag.PaletteData,
                                            (Int16)paletteSize,
                                            tempUByteArray);
                }
            }

            PCLXLWriter.addOperator(ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.SetColorSpace);

            //------------------------------------------------------------//
            //                                                            //
            // Generate BeginImage operator.                              //
            //                                                            //
            //------------------------------------------------------------//

            PCLXLWriter.addAttrUbyte(ref bufStd,
                               ref indStd,
                               PCLXLAttributes.eTag.ColorMapping,
                               colourMapping);

            PCLXLWriter.addAttrUbyte(ref bufStd,
                               ref indStd,
                               PCLXLAttributes.eTag.ColorDepth,
                               colourDepth);

            PCLXLWriter.addAttrUint16(ref bufStd,
                                ref indStd,
                                PCLXLAttributes.eTag.SourceWidth,
                                (UInt16)srcWidth);

            PCLXLWriter.addAttrUint16(ref bufStd,
                                ref indStd,
                                PCLXLAttributes.eTag.SourceHeight,
                                (UInt16)srcHeight);

            PCLXLWriter.addAttrUint16XY(ref bufStd,
                                  ref indStd,
                                  PCLXLAttributes.eTag.DestinationSize,
                                  (UInt16)destWidth,
                                  (UInt16)destHeight);

            PCLXLWriter.addOperator(ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.BeginImage);

            prnWriter.Write(bufStd, 0, indStd);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e I m a g e T r a i l e r                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write image termination sequences to output file.                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void generateImageTrailer(BinaryWriter prnWriter)
        {
            const Int32 sizeStd = 16;

            Byte[] bufStd = new Byte[sizeStd];

            Int32 indStd;

            indStd = 0;
            
            PCLXLWriter.addOperator(ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.EndImage);
            
            PCLXLWriter.addOperator(ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.PopGS);

            prnWriter.Write(bufStd, 0, indStd);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e J o b                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate test data.                                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void generateJob(BinaryWriter prnWriter,
                                       Int32        paperSize,
                                       Int32        paperType,
                                       Int32        orientation,
                                       Single       destPosX,
                                       Single       destPosY,
                                       Int32        destScalePercentX,
                                       Int32        destScalePercentY)
        {
            generateJobHeader(prnWriter,
                              paperSize,
                              paperType,
                              orientation);

            generateImage(prnWriter,
                          destPosX,
                          destPosY,
                          destScalePercentX,
                          destScalePercentY);

            generateJobTrailer(prnWriter);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e J o b H e a d e r                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write stream initialisation sequences to output file.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void generateJobHeader(BinaryWriter prnWriter,
                                              Int32        paperSize,
                                              Int32        paperType,
                                              Int32        orientation)
        {
            const Int32 sizeStd = 1024;

            Byte[] bufStd = new Byte[sizeStd];

            Int32 indStd;

            PCLXLWriter.stdJobHeader(prnWriter, "");
 
            indStd = 0;

            if (orientation < PCLOrientations.getCount())
            {
                PCLXLWriter.addAttrUbyte(ref bufStd,
                                   ref indStd,
                                   PCLXLAttributes.eTag.Orientation,
                                   PCLOrientations.getIdPCLXL(orientation));
            }

            if (paperSize < PCLPaperSizes.getCount())
            {
                PCLXLWriter.addAttrUbyte(ref bufStd,
                                   ref indStd,
                                   PCLXLAttributes.eTag.MediaSize,
                                   PCLPaperSizes.getIdPCLXL(paperSize));
            }

            if ((paperType < PCLPaperTypes.getCount()) &&
                (PCLPaperTypes.getType(paperType) !=
                    PCLPaperTypes.eEntryType.NotSet))
            {
                PCLXLWriter.addAttrUbyteArray(ref bufStd,
                                        ref indStd,
                                        PCLXLAttributes.eTag.MediaType,
                                        PCLPaperTypes.getName(paperType));
            }

            PCLXLWriter.addAttrUbyte(ref bufStd,
                               ref indStd,
                               PCLXLAttributes.eTag.SimplexPageMode,
                               (Byte)PCLXLAttrEnums.eVal.eSimplexFrontSide);

            PCLXLWriter.addOperator(ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.BeginPage);

            PCLXLWriter.addAttrUint16XY(ref bufStd,
                                  ref indStd,
                                  PCLXLAttributes.eTag.PageOrigin,
                                  0, 0);

            PCLXLWriter.addOperator(ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.SetPageOrigin);

            PCLXLWriter.addAttrUbyteArray(ref bufStd,
                                    ref indStd,
                                    PCLXLAttributes.eTag.RGBColor,
                                    3, PCLXLWriter.rgbBlack);

            PCLXLWriter.addOperator(ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.SetBrushSource);

            PCLXLWriter.addAttrUbyteArray(ref bufStd,
                                    ref indStd,
                                    PCLXLAttributes.eTag.RGBColor,
                                    3, PCLXLWriter.rgbBlack);

            PCLXLWriter.addOperator(ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.SetPenSource);

            prnWriter.Write(bufStd, 0, indStd);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e n e r a t e J o b T r a i l e r                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Write termination sequences to output file.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void generateJobTrailer(BinaryWriter prnWriter)
        {
            const Int32 sizeStd = 32;

            Byte[] bufStd = new Byte[sizeStd];

            Int32 indStd;

            indStd = 0;

            PCLXLWriter.addAttrUint16(ref bufStd,
                                ref indStd,
                                PCLXLAttributes.eTag.PageCopies,
                                1);

            PCLXLWriter.addOperator(ref bufStd,
                              ref indStd,
                              PCLXLOperators.eTag.EndPage);

            prnWriter.Write(bufStd, 0, indStd);

            PCLXLWriter.stdJobTrailer(prnWriter, false, "");
        }
    }
}
