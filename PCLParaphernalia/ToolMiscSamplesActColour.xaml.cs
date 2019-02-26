using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// Interaction logic for ToolMiscSamples.xaml
    /// 
    /// Class handles the MiscSamples: Colour tab.
    /// 
    /// © Chris Hutchinson 2014
    /// 
    /// </summary>
    
    [System.Reflection.ObfuscationAttribute(Feature = "renaming",
                                            ApplyToMembers = true)]

    public partial class ToolMiscSamples : Window
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const Int32 _sampleCtPCL_CID    = 4;
        const Int32 _sampleCtPCLXL_RGB  = 4;
        const Int32 _sampleCtPCLXL_Gray = 4;

        const Int32 _planeCtPCL_CID     = 3;
        const Int32 _planeCtPCLXL_RGB   = 3;
        const Int32 _planeCtPCLXL_Gray  = 1;

        const String _txtBoxRootNamePCL_CID    = "txtColourPCL_CID_";
        const String _txtBoxRootNamePCLXL_Gray = "txtColourPCLXL_Gray_";
        const String _txtBoxRootNamePCLXL_RGB  = "txtColourPCLXL_RGB_";

        private enum eColourType : byte
        {
            PCL_Simple,          // PCL
            PCL_Imaging,         // PCL
            PCLXL_Gray,          // PCLXL
            PCLXL_RGB            // PCLXL
        }

        public enum ePCLColourSpace : byte
        {
            RGB,    // 0 - values as per PCL ConfigureImageData definition  
            CMY,    // 1
            SRGB    // 2
        }

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Fields (class variables).                                          //
        //                                                                    //
        //--------------------------------------------------------------------//
        
        private eColourType _indxColourTypePCL;
        private eColourType _indxColourTypePCLXL;

        private Boolean _flagColourFmtHexPCL;
        private Boolean _flagColourFmtHexPCLXL;

        private Int32 [] _samplesPCL_CID;
        private Int32 [] _samplesPCLXL_RGB;
        private Int32 [] _samplesPCLXL_Gray;

        private Boolean _flagColourFormAsMacroPCL;
        private Boolean _flagColourFormAsMacroPCLXL;

        /*
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k C o l o u r O p t F o r m A s M a c r o _ C h e c k e d      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Option 'Render fixed text as overlay' checked.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkColourOptFormAsMacro_Checked (object sender,
                                                      RoutedEventArgs e)
        {
            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
                _flagColourFormAsMacroPCL = true;
            else
                _flagColourFormAsMacroPCLXL = true;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k C o l o u r O p t F o r m A s M a c r o _ U n c h e c k e d  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Option 'Render fixed text as overlay' unchecked.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkColourOptFormAsMacro_Unchecked (object sender,
                                                        RoutedEventArgs e)
        {
            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
                _flagColourFormAsMacroPCL = false;
            else
                _flagColourFormAsMacroPCLXL = false;
        }
        */
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c o l o u r M a p P C L _C I D _D i s p l a y                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Populate the colour mapping array.                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void colourMapPCL_CID_Display (Boolean hexDisplay)
        {
            Int32 x;

            String format;

            if (hexDisplay)
                format = "x2";
            else
                format = "";

            //----------------------------------------------------------------//

            x = _samplesPCL_CID [0];
            txtColourPCL_CID_03.Text = (x & 0xff).ToString (format);
            x = x >> 8;
            txtColourPCL_CID_02.Text = (x & 0xff).ToString (format);
            x = x >> 8;
            txtColourPCL_CID_01.Text = (x & 0xff).ToString (format);
            
            x = _samplesPCL_CID [1];
            txtColourPCL_CID_13.Text = (x & 0xff).ToString (format);
            x = x >> 8;
            txtColourPCL_CID_12.Text = (x & 0xff).ToString (format);
            x = x >> 8;
            txtColourPCL_CID_11.Text = (x & 0xff).ToString (format);
            
            x = _samplesPCL_CID [2];
            txtColourPCL_CID_23.Text = (x & 0xff).ToString (format);
            x = x >> 8;
            txtColourPCL_CID_22.Text = (x & 0xff).ToString (format);
            x = x >> 8;
            txtColourPCL_CID_21.Text = (x & 0xff).ToString (format);
            
            x = _samplesPCL_CID [3];
            txtColourPCL_CID_33.Text = (x & 0xff).ToString (format);
            x = x >> 8;
            txtColourPCL_CID_32.Text = (x & 0xff).ToString (format);
            x = x >> 8;
            txtColourPCL_CID_31.Text = (x & 0xff).ToString (format);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c o l o u r M a p P C L X L _G r a y _ D i s p l a y               //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Populate the PCLXL Gray colour mapping array.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void colourMapPCLXL_Gray_Display (Boolean hexDisplay)
        {
            Int32 x;

            String format;

            if (hexDisplay)
                format = "x2";
            else
                format = "";

            //----------------------------------------------------------------//

            x = _samplesPCLXL_Gray [0];
            txtColourPCLXL_Gray_01.Text = (x & 0xff).ToString (format);
            
            x = _samplesPCLXL_Gray [1];
            txtColourPCLXL_Gray_11.Text = (x & 0xff).ToString (format);
            
            x = _samplesPCLXL_Gray [2];
            txtColourPCLXL_Gray_21.Text = (x & 0xff).ToString (format);
            
            x = _samplesPCLXL_Gray [3];
            txtColourPCLXL_Gray_31.Text = (x & 0xff).ToString (format);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c o l o u r M a p P C L X L _R G B _ D i s p l a y                 //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Populate the PCLXL RGB colour mapping array.                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void colourMapPCLXL_RGB_Display (Boolean hexDisplay)
        {
            Int32 x;

            String format;

            if (hexDisplay)
                format = "x2";
            else
                format = "";

            //----------------------------------------------------------------//

            x = _samplesPCLXL_RGB [0];
            txtColourPCLXL_RGB_03.Text = (x & 0xff).ToString (format);
            x = x >> 8;
            txtColourPCLXL_RGB_02.Text = (x & 0xff).ToString (format);
            x = x >> 8;
            txtColourPCLXL_RGB_01.Text = (x & 0xff).ToString (format);
            
            x = _samplesPCLXL_RGB [1];
            txtColourPCLXL_RGB_13.Text = (x & 0xff).ToString (format);
            x = x >> 8;
            txtColourPCLXL_RGB_12.Text = (x & 0xff).ToString (format);
            x = x >> 8;
            txtColourPCLXL_RGB_11.Text = (x & 0xff).ToString (format);
            
            x = _samplesPCLXL_RGB [2];
            txtColourPCLXL_RGB_23.Text = (x & 0xff).ToString (format);
            x = x >> 8;
            txtColourPCLXL_RGB_22.Text = (x & 0xff).ToString (format);
            x = x >> 8;
            txtColourPCLXL_RGB_21.Text = (x & 0xff).ToString (format);
            
            x = _samplesPCLXL_RGB [3];
            txtColourPCLXL_RGB_33.Text = (x & 0xff).ToString (format);
            x = x >> 8;
            txtColourPCLXL_RGB_32.Text = (x & 0xff).ToString (format);
            x = x >> 8;
            txtColourPCLXL_RGB_31.Text = (x & 0xff).ToString (format);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n i t i a l i s e D a t a C o l o u r                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initialise 'Colour' data.                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void initialiseDataColour()
        {
            lbOrientation.Visibility = Visibility.Hidden;
            cbOrientation.Visibility = Visibility.Hidden;

        //  chkMiscFormAsMacro.Visibility = Visibility.Visible;

            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                tabColourData.SelectedItem = tabColourModePCL;

                if (_indxColourTypePCL == eColourType.PCL_Imaging)
                {
                    rbColourModePCL_Imaging.IsChecked = true;

                    grpColourModePCL_CID.Visibility = Visibility.Visible;
                    grpColourModePCLFormat.Visibility = Visibility.Visible;

                    if (_flagColourFmtHexPCL)
                        rbColourOptPCLFmtHex.IsChecked = true;
                    else
                        rbColourOptPCLFmtDec.IsChecked = true;

                    colourMapPCL_CID_Display (_flagColourFmtHexPCL);
                }
                else
                {
                    _indxColourTypePCL = eColourType.PCL_Simple;

                    rbColourModePCL_Simple.IsChecked = true;

                    grpColourModePCL_CID.Visibility = Visibility.Hidden;
                    grpColourModePCLFormat.Visibility = Visibility.Hidden;
                }

                if (_flagColourFormAsMacroPCL)
                    chkOptFormAsMacro.IsChecked = true;
                else
                    chkOptFormAsMacro.IsChecked = false;
            }
            else
            {
                tabColourData.SelectedItem = tabColourOptPCLXL;

                if (_indxColourTypePCLXL == eColourType.PCLXL_RGB)
                {
                    rbColourModePCLXL_RGB.IsChecked = true;

                    tabColourPCLXLModes.SelectedItem = tabColourModePCLXL_RGB;

                    if (_flagColourFmtHexPCLXL)
                        rbColourOptPCLXLFmtHex.IsChecked = true;
                    else
                        rbColourOptPCLXLFmtDec.IsChecked = true;

                    colourMapPCLXL_RGB_Display (_flagColourFmtHexPCLXL);
                }
                else
                {
                    _indxColourTypePCLXL = eColourType.PCLXL_Gray;

                    rbColourModePCLXL_Gray.IsChecked = true;

                    tabColourPCLXLModes.SelectedItem = tabColourModePCLXL_Gray;

                    if (_flagColourFmtHexPCLXL)
                        rbColourOptPCLXLFmtHex.IsChecked = true;
                    else
                        rbColourOptPCLXLFmtDec.IsChecked = true;

                    colourMapPCLXL_Gray_Display (_flagColourFmtHexPCLXL);
                }

                if (_flagColourFormAsMacroPCLXL)
                    chkOptFormAsMacro.IsChecked = true;
                else
                    chkOptFormAsMacro.IsChecked = false;
            }

            initialiseDescColour();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n i t i a l i s e D e s c C o l o u r                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initialise 'Colour' description.                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void initialiseDescColour()
        {
            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                if (_indxColourTypePCL == eColourType.PCL_Simple)
                {
                    txtColourDesc.Text =
                        "Shows samples of PCL simple colour mode." +
                        "\r\n\r\n" +
                        "Shows the colours associated with the three" +
                        " predefined palettes:" +
                        "\r\n" +
                        "  Monochrome - two colour palette;\r\n" +
                        "  RGB - eight colour palette;\r\n" +
                        "  CMY - eight colour palette";
                }
                else
                {
                    txtColourDesc.Text =
                        "Shows samples of PCL imaging mode." +
                        "\r\n\r\n" +
                        "This sample lets you define a four colour" +
                        " palette, in terms of the intensities of" +
                        " three components, each in the range" +
                        " decimal 0 -> 255" +
                        " (hexadecimal 00 -> ff)" +
                        "\r\n\r\n" +
                        "This mechanism is not supported by all PCL printers.";
                }
            }
            else
            {
                if (_indxColourTypePCLXL == eColourType.PCLXL_Gray)
                {
                    txtColourDesc.Text =
                        "Shows samples of PCL XL colour space Gray.";
                }
                else
                {
                    txtColourDesc.Text =
                        "Shows samples of PCL XL colour space RGB.";
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m e t r i c s L o a d D a t a C o l o u r                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Load current metrics from persistent storage.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void metricsLoadDataColour()
        {
            Int32 tmpInt = 0;

            ToolMiscSamplesPersist.loadDataTypeColour (
                "PCL",
                ref tmpInt,
                ref _flagColourFormAsMacroPCL,
                ref _flagColourFmtHexPCL);

            if (tmpInt == (Int32) eColourType.PCL_Simple)
                _indxColourTypePCL = eColourType.PCL_Simple;
            else
                _indxColourTypePCL = eColourType.PCL_Imaging;

            _samplesPCL_CID = new Int32 [_sampleCtPCL_CID];

            ToolMiscSamplesPersist.loadDataTypeColourSample (
                "PCL",
                "Imaging",
                _sampleCtPCL_CID,
                ref _samplesPCL_CID,
                false);

            //----------------------------------------------------------------//

            ToolMiscSamplesPersist.loadDataTypeColour (
                "PCLXL",
                ref tmpInt,
                ref _flagColourFormAsMacroPCLXL,
                ref _flagColourFmtHexPCLXL);

            if (tmpInt == (Int32) eColourType.PCLXL_Gray)
                _indxColourTypePCLXL = eColourType.PCLXL_Gray;
            else
                _indxColourTypePCLXL = eColourType.PCLXL_RGB;

            _samplesPCLXL_Gray = new Int32 [_sampleCtPCLXL_Gray];

            ToolMiscSamplesPersist.loadDataTypeColourSample (
                "PCLXL",
                "Gray",
                _sampleCtPCLXL_Gray,
                ref _samplesPCLXL_Gray,
                true);

            _samplesPCLXL_RGB = new Int32 [_sampleCtPCLXL_RGB];

            ToolMiscSamplesPersist.loadDataTypeColourSample (
                "PCLXL",
                "RGB",
                _sampleCtPCLXL_RGB,
                ref _samplesPCLXL_RGB,
                false);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m e t r i c s S a v e D a t a C o l o u r                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Save current metrics to persistent storage.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void metricsSaveDataColour()
        {
            ToolMiscSamplesPersist.saveDataTypeColour(
                "PCL",
                (Int32) _indxColourTypePCL,
                _flagColourFormAsMacroPCL,
                _flagColourFmtHexPCL);

            ToolMiscSamplesPersist.saveDataTypeColourSample(
                "PCL",
                "Imaging",
                _sampleCtPCL_CID,
                _samplesPCL_CID);

            ToolMiscSamplesPersist.saveDataTypeColour(
                "PCLXL",
                (Int32) _indxColourTypePCLXL,
                _flagColourFormAsMacroPCLXL,
                _flagColourFmtHexPCLXL);

            ToolMiscSamplesPersist.saveDataTypeColourSample(
                "PCLXL",
                "Gray",
                _sampleCtPCLXL_RGB,
                _samplesPCLXL_Gray);

            ToolMiscSamplesPersist.saveDataTypeColourSample(
                "PCLXL",
                "RGB",
                _sampleCtPCLXL_RGB,
                _samplesPCLXL_RGB);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b C o l o u r M o d e P C L _ I m a g i n g _ C l i c k          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Radio button selecting 'PCL imaging' mode.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbColourModePCL_Imaging_Click (object sender,
                                                    RoutedEventArgs e)
        {
            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                _indxColourTypePCL = eColourType.PCL_Imaging;

                initialiseDescColour ();

                if (_flagColourFmtHexPCL)
                    rbColourOptPCLFmtHex.IsChecked = true;
                else
                    rbColourOptPCLFmtDec.IsChecked = true;
                
                colourMapPCL_CID_Display (_flagColourFmtHexPCL);

                grpColourModePCL_CID.Visibility = Visibility.Visible;
                grpColourModePCLFormat.Visibility = Visibility.Visible;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b C o l o u r M o d e P C L _ S i m p l e _ C l i c k            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Radio button selecting PCL 'Simple colour' mode.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbColourModePCL_Simple_Click (object sender,
                                                   RoutedEventArgs e)
        {
            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                _indxColourTypePCL = eColourType.PCL_Simple;

                initialiseDescColour ();

                grpColourModePCL_CID.Visibility = Visibility.Hidden;
                grpColourModePCLFormat.Visibility = Visibility.Hidden;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b C o l o u r M o d e P C L X L _ G r a y _ C l i c k            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Radio button selecting 'PCL XL Gray' colour space.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbColourModePCLXL_Gray_Click (object sender,
                                                   RoutedEventArgs e)
        {
            if (_crntPDL == ToolCommonData.ePrintLang.PCLXL)
            {
                _indxColourTypePCLXL = eColourType.PCLXL_Gray;

                initialiseDescColour ();

                if (_flagColourFmtHexPCLXL)
                    rbColourOptPCLXLFmtHex.IsChecked = true;
                else
                    rbColourOptPCLXLFmtDec.IsChecked = true;

                colourMapPCLXL_Gray_Display (_flagColourFmtHexPCLXL);

                tabColourPCLXLModes.SelectedItem = tabColourModePCLXL_Gray;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b C o l o u r M o d e P C L X L _R G B _ C l i c k               //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Radio button selecting 'PCL XL RGB' colour space.                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbColourModePCLXL_RGB_Click (object sender,
                                                  RoutedEventArgs e)
        {
            if (_crntPDL == ToolCommonData.ePrintLang.PCLXL)
                _indxColourTypePCLXL = eColourType.PCLXL_RGB;

            initialiseDescColour ();

            if (_flagColourFmtHexPCLXL)
                rbColourOptPCLXLFmtHex.IsChecked = true;
            else
                rbColourOptPCLXLFmtDec.IsChecked = true;

            colourMapPCLXL_RGB_Display (_flagColourFmtHexPCLXL);

            tabColourPCLXLModes.SelectedItem = tabColourModePCLXL_RGB;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b C o l o u r O p t P C L F m t D e c _ C l i c k                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the Map format 'Decimal' radio button is clicked.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbColourOptPCLFmtDec_Click (object sender,
                                                 RoutedEventArgs e)
        {
            _flagColourFmtHexPCL = false;

            colourMapPCL_CID_Display (_flagColourFmtHexPCL);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b C o l o u r O p t P C L F m t H e x _ C l i c k                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the Map format 'Hexadecimal' radio button is clicked.  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbColourOptPCLFmtHex_Click (object sender,
                                                 RoutedEventArgs e)
        {
            _flagColourFmtHexPCL = true;

            colourMapPCL_CID_Display (_flagColourFmtHexPCL);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b C o l o u r O p t P C L X L F m t D e c _ C l i c k            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the Map format 'Decimal' radio button is clicked.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbColourOptPCLXLFmtDec_Click (object sender,
                                                   RoutedEventArgs e)
        {
            _flagColourFmtHexPCLXL = false;

            if (_indxColourTypePCLXL == eColourType.PCLXL_Gray)
                colourMapPCLXL_Gray_Display (_flagColourFmtHexPCLXL);
            else
                colourMapPCLXL_RGB_Display (_flagColourFmtHexPCLXL);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b C o l o u r O p t P C L X L F m t M a p _ C l i c k            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the Map format 'Hexadecimal' radio button is clicked.  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbColourOptPCLXLFmtHex_Click (object sender,
                                                    RoutedEventArgs e)
        {
            _flagColourFmtHexPCLXL = true;

            if (_indxColourTypePCLXL == eColourType.PCLXL_Gray)
                colourMapPCLXL_Gray_Display (_flagColourFmtHexPCLXL);
            else
                colourMapPCLXL_RGB_Display (_flagColourFmtHexPCLXL);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t F l a g C o l o u r F o r m A s M a c r o                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set or unset 'Render fixed text as overlay' flag.                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void setFlagColourFormAsMacro (
            Boolean setFlag,
            ToolCommonData.ePrintLang crntPDL)
        {
            if (crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                if (setFlag)
                    _flagColourFormAsMacroPCL = true;
                else
                    _flagColourFormAsMacroPCL = false;
            }
            else if (crntPDL == ToolCommonData.ePrintLang.PCLXL)
            {
                if (setFlag)
                    _flagColourFormAsMacroPCLXL = true;
                else
                    _flagColourFormAsMacroPCLXL = false;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t C o l o u r C h e c k B o x I d                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // A text mapping text box has got focus; check which sample and      //
        // plane this box is supposed to represent.                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean txtColourCheckBoxId (TextBox source,
                                             Int32 nameRootLen,
                                             Int32 sampleCt,
                                             Int32 planeCt,
                                             ref Int32 sampleNo,
                                             ref Int32 planeNo,
                                             ref Int32 planeSig )
        {
            Boolean flagOK = true;

            UInt16 mapIndx;

            String txtBoxName;

            //----------------------------------------------------------------//
            //                                                                //
            // Work out which of the (mapping) text boxes has just lost       //
            // focus.                                                         //
            // This is done by examining the text box name.                   //
            // The names should be in the format 'zz--zzxy', where            //
            //  'zz--zz' represents the text box root name.                   //
            //  'x' represents the sample number in the range '0' -> '3'.     //
            //  'y' represents the plane  number in the range '1' -> '3'.     //
            //                                                                //
            //----------------------------------------------------------------//

            txtBoxName = source.Name;

            flagOK = UInt16.TryParse (txtBoxName.Substring (nameRootLen, 2),
                                      NumberStyles.Integer,
                                      CultureInfo.InvariantCulture,
                                      out mapIndx);

            if (flagOK)
            {
                sampleNo = mapIndx / 10;
                planeNo  = mapIndx - (sampleNo * 10);
                planeSig = planeCt - planeNo;

                if ((sampleNo < 0) || (sampleNo > (sampleCt - 1)))
                    flagOK = false;
                else if ((planeNo < 1) || (planeNo > planeCt))
                    flagOK = false;
                else if ((planeSig < 0) || (planeSig >= planeCt))
                    flagOK = false;
            }

            if (!flagOK)
            {
                MessageBox.Show ("Unable to detemine which mapping text box" +
                                 " has just lost focus!",
                                 "***** Internal error *****",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Warning);
            }

            return flagOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t C o l o u r P C L _C I D _ G o t F o c u s                   //
        //--------------------------------------------------------------------//
        //                                                                    //
        // A text mapping text box has got focus; select the contents.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtColourPCL_CID_GotFocus (object sender,
                                                RoutedEventArgs e)
        {
            TextBox source = e.Source as TextBox;

            source.SelectAll ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t C o l o u r P C L _C I D _ L o s t F o c u s                 //
        //--------------------------------------------------------------------//
        //                                                                    //
        // A colour mapping text box has lost focus; validate the contents.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtColourPCL_CID_LostFocus (object sender,
                                                 RoutedEventArgs e)
        {
            Int32 nameRootLen = _txtBoxRootNamePCL_CID.Length;

            Int32 planeCt  = _planeCtPCL_CID;
            Int32 sampleCt = _sampleCtPCL_CID;

            Int32 sampleNo = 0,
                  planeNo  = 0,
                  planeSig = 0;

            TextBox source = e.Source as TextBox;

            Boolean flagOK = false;
            
            flagOK = txtColourCheckBoxId (source, nameRootLen, sampleCt, planeCt,
                                          ref sampleNo, ref planeNo,
                                          ref planeSig);

            if (flagOK)
            {
                flagOK = validateMapEntry (source,
                                           sampleNo, planeNo, planeSig,
                                           _flagColourFmtHexPCL,
                                           ref _samplesPCL_CID);

                if (! flagOK)
                {
                    Helper_WPFFocusFix.Focus (source);   // need this to focus
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t C o l o u r P C L X L _ G r a y _ G o t F o c u s            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // A text mapping text box has got focus; select the contents.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtColourPCLXL_Gray_GotFocus (object sender,
                                                   RoutedEventArgs e)
        {
            Int32 nameRootLen = _txtBoxRootNamePCLXL_Gray.Length;

            Int32 planeCt = _planeCtPCLXL_Gray;
            Int32 sampleCt = _sampleCtPCLXL_Gray;

            Int32 sampleNo = 0,
                  planeNo = 0,
                  planeSig = 0;

            TextBox source = e.Source as TextBox;

            Boolean flagOK = false;

            flagOK = txtColourCheckBoxId (source, nameRootLen, sampleCt, planeCt,
                                          ref sampleNo, ref planeNo,
                                          ref planeSig);

            if (flagOK)
            {
                flagOK = validateMapEntry (source,
                                           sampleNo, planeNo, planeSig,
                                           _flagColourFmtHexPCLXL,
                                           ref _samplesPCLXL_Gray);

                if (!flagOK)
                {
                    Helper_WPFFocusFix.Focus (source);   // need this to focus
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t C o l o u r P C L X L _ G r a y _ L o s t F o c u s          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // A colour mapping text box has lost focus; validate the contents.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtColourPCLXL_Gray_LostFocus (object sender,
                                                    RoutedEventArgs e)
        {
            Int32 nameRootLen = _txtBoxRootNamePCLXL_RGB.Length;

            Int32 planeCt = _planeCtPCLXL_RGB;
            Int32 sampleCt = _sampleCtPCLXL_RGB;

            Int32 sampleNo = 0,
                  planeNo = 0,
                  planeSig = 0;

            TextBox source = e.Source as TextBox;

            Boolean flagOK = false;

            flagOK = txtColourCheckBoxId (source, nameRootLen, sampleCt, planeCt,
                                          ref sampleNo, ref planeNo,
                                          ref planeSig);

            if (flagOK)
            {
                flagOK = validateMapEntry (source,
                                           sampleNo, planeNo, planeSig,
                                           _flagColourFmtHexPCLXL,
                                           ref _samplesPCLXL_RGB);

                if (!flagOK)
                {
                    Helper_WPFFocusFix.Focus (source);   // need this to focus
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t C o l o u r P C L X L _ R G B _ G o t F o c u s              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // A text mapping text box has got focus; select the contents.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtColourPCLXL_RGB_GotFocus (object sender,
                                                  RoutedEventArgs e)
        {
            TextBox source = e.Source as TextBox;

            source.SelectAll ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t C o l o u r P C L X L _ R G B _ L o s t F o c u s            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // A colour mapping text box has lost focus; validate the contents.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtColourPCLXL_RGB_LostFocus (object sender,
                                                   RoutedEventArgs e)
        {
            //
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // v a l i d a t e M a p E n t r y                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Validate supplied colour component value.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean validateMapEntry (TextBox source,
                                          Int32   sampleNo,
                                          Int32   planeNo,
                                          Int32   planeSig,
                                          Boolean hexFormat,
                                          ref Int32 [] sampleDef)
        {
            Boolean OK = true;

            Byte value;

            String txtNewVal = source.Text;

            if (hexFormat)
                OK = Byte.TryParse (txtNewVal,
                                    NumberStyles.HexNumber,
                                    CultureInfo.InvariantCulture,
                                    out value);
            else
                OK = Byte.TryParse (txtNewVal, out value);

            if (OK)
            {
                Int32 oldVal = sampleDef[sampleNo];
                Int32 mask = 0xffff00;
                Int32 newComp = value;

                for (Int32 i = 0; i < planeSig; i++)
                {
                    mask = ((mask << 8) & 0xffffff) | 0xff;
                    newComp = newComp << 8;
                }
                
                sampleDef[sampleNo] = (oldVal & mask) | newComp;
            }
            else
            {
                String format,
                       formatDesc,
                       txtOldVal;

                Int32 x;

                if (hexFormat)
                {
                    format = "x2";
                    formatDesc = "hexadecimal";
                }
                else
                {
                    format = "";
                    formatDesc = "decimal";
                }

                x = sampleDef[sampleNo];

                for (Int32 i = 1; i < planeNo; i++)
                {
                    x = x >> 8;
                }

                txtOldVal = (x & 0xff).ToString (format);

                MessageBox.Show ("Sample '" + sampleNo +
                                 " ' plane '" + planeNo + "':\n\n" +
                                 "Value " + formatDesc +
                                 " '" + txtNewVal + "' is invalid.\n\n" +
                                 "Reset to original value " + formatDesc +
                                 " '" + txtOldVal + "'",
                                 "Colour mapping",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);

                source.Text = txtOldVal;
            }

            return OK;
        }
    }
}
