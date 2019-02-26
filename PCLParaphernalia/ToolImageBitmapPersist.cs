using Microsoft.Win32;
using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class manages persistent storage of options for the ImageBitmap tool.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class ToolImageBitmapPersist
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        const String _mainKey                 = MainForm._regMainKey;

        const String _subKeyTools             = "Tools";
        const String _subKeyToolsImageBitmap  = "ImageBitmap";
        const String _subKeyPCL5              = "PCL5";
        const String _subKeyPCL6              = "PCL6";
        const String _subKeyPCL               = "PCL";
        const String _subKeyPCLXL             = "PCLXL";

        const String _nameCaptureFile         = "CaptureFile";
        const String _nameCoordX              = "CoordX";
        const String _nameCoordY              = "CoordY";
        const String _nameFilename            = "Filename";
        const String _nameIndxOrientation     = "IndxOrientation";
        const String _nameIndxPaperSize       = "IndxPaperSize";
        const String _nameIndxPaperType       = "IndxPaperType";
        const String _nameIndxPDL             = "IndxPDL";
        const String _nameIndxRasterRes       = "IndxRasterRes";
        const String _nameScaleX              = "ScaleX";
        const String _nameScaleY              = "ScaleY";

        const Int32 _indexZero                = 0;

        const String _defaultCaptureFile      = "CaptureFile_ImageBitmap.prn";
        const String _defaultCaptureFilePCL   = "CaptureFile_ImageBitmapPCL.prn";
        const String _defaultCaptureFilePCLXL = "CaptureFile_ImageBitmapPCLXL.prn";
        const String _defaultFilename         = "DefaultImageFile.bmp";

        const Int32 _defaultCoord             = 300;
        const Int32 _defaultScale             = 100;

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a C a p t u r e                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored Image Bitmap capture file data.                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataCapture(ToolCommonData.ePrintLang crntPDL,
                                            ref String captureFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String oldKey = _subKeyTools + "\\" + _subKeyToolsImageBitmap;
            String oldFile;

            Boolean update_from_v2_5_0_0 = false;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            using (RegistryKey subKey = keyMain.OpenSubKey(oldKey, true))
            {
                oldFile = (String)subKey.GetValue(_nameCaptureFile);

                if (oldFile != null)
                {
                    update_from_v2_5_0_0 = true;

                    subKey.DeleteValue(_nameCaptureFile);
                }
            }

            if (update_from_v2_5_0_0)
            {
                String keyPCL = _subKeyTools +
                                 "\\" + _subKeyToolsImageBitmap +
                                 "\\" + _subKeyPCL;

                using (RegistryKey subKey = keyMain.CreateSubKey(keyPCL))
                {
                    subKey.SetValue(_nameCaptureFile,
                                     oldFile,
                                     RegistryValueKind.String);
                }

                String keyPCLXL = _subKeyTools +
                                 "\\" + _subKeyToolsImageBitmap +
                                 "\\" + _subKeyPCLXL;

                using (RegistryKey subKey = keyMain.CreateSubKey(keyPCLXL))
                {
                    subKey.SetValue(_nameCaptureFile,
                                     oldFile,
                                     RegistryValueKind.String);
                }
            }

            if (crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                String key = _subKeyTools + "\\" + _subKeyToolsImageBitmap +
                                            "\\" + _subKeyPCL;

                using (RegistryKey subKey = keyMain.CreateSubKey(key))
                {
                    captureFile = (String)subKey.GetValue(
                        _nameCaptureFile,
                        defWorkFolder + "\\" + _defaultCaptureFilePCL);
                }
            }
            else if (crntPDL == ToolCommonData.ePrintLang.PCLXL)
            {
                String key = _subKeyTools + "\\" + _subKeyToolsImageBitmap +
                                            "\\" + _subKeyPCLXL;

                using (RegistryKey subKey = keyMain.CreateSubKey(key))
                {
                    captureFile = (String)subKey.GetValue(
                        _nameCaptureFile,
                        defWorkFolder + "\\" + _defaultCaptureFilePCLXL);
                }
            }
        }
        
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a C o m m o n                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored Bitmap common data.                                //
        // Missing items are given default values.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataCommon(ref Int32  indxPDL,
                                          ref String filename,
                                          ref Int32  destPosX,
                                          ref Int32  destPosY,
                                          ref Int32  destScaleX,
                                          ref Int32  destScaleY,
                                          ref Int32  indxRasterRes)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsImageBitmap;

            String defWorkFolder = ToolCommonData.DefWorkFolder;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                if (Helper_RegKey.keyExists(subKey, _subKeyPCL5))
                    // update from v2_5_0_0
                    Helper_RegKey.renameKey(subKey, _subKeyPCL5, _subKeyPCL);

                if (Helper_RegKey.keyExists(subKey, _subKeyPCL6))
                    // update from v2_5_0_0
                    Helper_RegKey.renameKey(subKey, _subKeyPCL6, _subKeyPCLXL);

                indxPDL = (Int32)subKey.GetValue(_nameIndxPDL,
                                                       _indexZero);

                filename      = (String)subKey.GetValue(_nameFilename,
                                                        defWorkFolder + "\\" +
                                                        _defaultFilename);

                destPosX      = (Int32)subKey.GetValue(_nameCoordX,
                                                       _defaultCoord);

                destPosY      = (Int32)subKey.GetValue(_nameCoordY,
                                                       _defaultCoord);

                destScaleX    = (Int32)subKey.GetValue(_nameScaleX,
                                                       _defaultScale);

                destScaleY    = (Int32)subKey.GetValue(_nameScaleY,
                                                       _defaultScale);

                indxRasterRes = (Int32)subKey.GetValue(_nameIndxRasterRes,
                                                       _indexZero);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a P C L                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored Bitmap PCL or PCLXL data.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataPCL(String    pdlName,
                                       ref Int32 indxOrientation,
                                       ref Int32 indxPaperSize,
                                       ref Int32 indxPaperType)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsImageBitmap +
                                 "\\" + pdlName;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                indxOrientation = (Int32)subKey.GetValue(_nameIndxOrientation,
                                                         _indexZero);
                indxPaperSize   = (Int32)subKey.GetValue(_nameIndxPaperSize,
                                                         _indexZero);
                indxPaperType   = (Int32)subKey.GetValue(_nameIndxPaperType,
                                                         _indexZero);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a C a p t u r e                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current Image Bitmap capture file data.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataCapture(ToolCommonData.ePrintLang crntPDL,
                                            String captureFile)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            if (crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                String key = _subKeyTools + "\\" + _subKeyToolsImageBitmap +
                                            "\\" + _subKeyPCL;

                using (RegistryKey subKey = keyMain.CreateSubKey(key))
                {
                    if (captureFile != null)
                    {
                        subKey.SetValue(_nameCaptureFile,
                                         captureFile,
                                         RegistryValueKind.String);
                    }
                }
            }
            else if (crntPDL == ToolCommonData.ePrintLang.PCLXL)
            {
                String key = _subKeyTools + "\\" + _subKeyToolsImageBitmap +
                                            "\\" + _subKeyPCLXL;

                using (RegistryKey subKey = keyMain.CreateSubKey(key))
                {
                    if (captureFile != null)
                    {
                        subKey.SetValue(_nameCaptureFile,
                                         captureFile,
                                         RegistryValueKind.String);
                    }
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a C o m m o n                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current Bitmap common data.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataCommon(Int32  indxPDL,
                                          String filename,
                                          Int32  destPosX,
                                          Int32  destPosY,
                                          Int32  destScaleX,
                                          Int32  destScaleY,
                                          Int32  indxRasterRes)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyTools + "\\" + _subKeyToolsImageBitmap;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue(_nameIndxPDL,
                                indxPDL,
                                RegistryValueKind.DWord);

                if (filename != null)
                {
                    subKey.SetValue (_nameFilename,
                                    filename,
                                    RegistryValueKind.String);
                }

                subKey.SetValue(_nameCoordX,
                                destPosX,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameCoordY,
                                destPosY,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameScaleX,
                                destScaleX,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameScaleY,
                                destScaleY,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxRasterRes,
                                indxRasterRes,
                                RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a P C L                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current Bitmap PCL or PCLXL data.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataPCL(String pdlName,
                                       Int32  indxOrientation,
                                       Int32  indxPaperSize,
                                       Int32  indxPaperType)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key;

            key = _subKeyTools + "\\" + _subKeyToolsImageBitmap +
                                 "\\" + pdlName;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue(_nameIndxOrientation,
                                indxOrientation,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxPaperSize,
                                indxPaperSize,
                                RegistryValueKind.DWord);

                subKey.SetValue(_nameIndxPaperType,
                                indxPaperType,
                                RegistryValueKind.DWord);
            }
        }
    }
}
