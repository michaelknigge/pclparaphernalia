using System;
using System.IO;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// Interaction logic for ToolMiscSamples.xaml
    /// 
    /// Class handles the MiscSamples: Logical operations tab.
    /// 
    /// © Chris Hutchinson 2015
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

        private static Int32 _logOperROPMin = 0;
        private static Int32 _logOperROPMax = 255;
        private static Int32 _logOperROPInc = 8;

        private static Int16[] _subsetLogOperModesPCL = 
        {
            (Int32) PCLPalettes.eIndex.PCLMonochrome,
            (Int32) PCLPalettes.eIndex.PCLSimpleColourCMY,
            (Int32) PCLPalettes.eIndex.PCLSimpleColourRGB
        };

        private static Int16[] _subsetLogOperModesPCLXL = 
        {
            (Int32) PCLXLPalettes.eIndex.PCLXLGray,
            (Int32) PCLXLPalettes.eIndex.PCLXLRGB
        };

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Fields (class variables).                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean _flagLogOperUseMacrosPCL;
        private Boolean _flagLogOperUseMacrosPCLXL;
        private Boolean _flagLogOperOptSrcTextPatPCLXL;
 
        private Int32 _indxLogOperModePCL;
        private Int32 _indxLogOperModePCLXL;
 
        private Int32 _indxLogOperROPFromPCL;
        private Int32 _indxLogOperROPFromPCLXL;
        private Int32 _indxLogOperROPToPCL;
        private Int32 _indxLogOperROPToPCLXL;

        private Int32 _indxLogOperClrD1PCL;
        private Int32 _indxLogOperClrD2PCL;
        private Int32 _indxLogOperClrS1PCL;
        private Int32 _indxLogOperClrS2PCL;
        private Int32 _indxLogOperClrT1PCL;
        private Int32 _indxLogOperClrT2PCL;

        private Int32 _indxLogOperClrD1PCLXL;
        private Int32 _indxLogOperClrD2PCLXL;
        private Int32 _indxLogOperClrS1PCLXL;
        private Int32 _indxLogOperClrS2PCLXL;
        private Int32 _indxLogOperClrT1PCLXL;
        private Int32 _indxLogOperClrT2PCLXL;

        private Int32 _indxLogOperGrayD1PCLXL;
        private Int32 _indxLogOperGrayD2PCLXL;
        private Int32 _indxLogOperGrayS1PCLXL;
        private Int32 _indxLogOperGrayS2PCLXL;
        private Int32 _indxLogOperGrayT1PCLXL;
        private Int32 _indxLogOperGrayT2PCLXL;

        private Int32 _indxLogOperMonoD1PCL;
        private Int32 _indxLogOperMonoD2PCL;
        private Int32 _indxLogOperMonoS1PCL;
        private Int32 _indxLogOperMonoS2PCL;
        private Int32 _indxLogOperMonoT1PCL;
        private Int32 _indxLogOperMonoT2PCL;

        private Int32 _ctLogOperModesPCL;
        private Int32 _ctLogOperModesPCLXL;

        private Int16 _maxLogOperClrItemsPCL;
        private Int16 _maxLogOperClrItemsPCLXL;

        private Int16[] _ctLogOperClrsPCL;
        private Int16[] _ctLogOperClrsPCLXL;
 
        private String[,] _clrsLogOperPCL;
        private String[,] _clrsLogOperPCLXL;

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b L o g O p e r C l r D 1 _ S e l e c t i o n C h a n g e d      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Colour index item for sample Destination has changed.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbLogOperClrD1_SelectionChanged (
            object sender,
            SelectionChangedEventArgs e)
        {
            if (_initialised)
            {
                if ((_crntPDL == ToolCommonData.ePrintLang.PCL) &&
                     cbLogOperClrD1PCL.HasItems)
                {
                    _indxLogOperClrD1PCL =
                        (Byte) cbLogOperClrD1PCL.SelectedIndex;
                }
                else if ((_crntPDL == ToolCommonData.ePrintLang.PCLXL) &&
                     cbLogOperClrD1PCLXL.HasItems)
                {
                    _indxLogOperClrD1PCLXL =
                        (Byte) cbLogOperClrD1PCLXL.SelectedIndex;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b L o g O p e r C l r D 2 _ S e l e c t i o n C h a n g e d      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Colour index item for sample Destination colour 2 has changed.     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbLogOperClrD2_SelectionChanged (
            object sender,
            SelectionChangedEventArgs e)
        {
            if (_initialised)
            {
                if ((_crntPDL == ToolCommonData.ePrintLang.PCL) &&
                     cbLogOperClrD2PCL.HasItems)
                {
                    _indxLogOperClrD2PCL =
                        (Byte) cbLogOperClrD2PCL.SelectedIndex;
                }
                else if ((_crntPDL == ToolCommonData.ePrintLang.PCLXL) &&
                     cbLogOperClrD2PCLXL.HasItems)
                {
                    _indxLogOperClrD2PCLXL =
                        (Byte) cbLogOperClrD2PCLXL.SelectedIndex;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b L o g O p e r C l r S 1 _ S e l e c t i o n C h a n g e d      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Colour index item for sample Source image has changed.             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbLogOperClrS1_SelectionChanged (
            object sender,
            SelectionChangedEventArgs e)
        {
            if (_initialised)
            {
                if ((_crntPDL == ToolCommonData.ePrintLang.PCL) &&
                     cbLogOperClrS1PCL.HasItems)
                {
                    _indxLogOperClrS1PCL =
                        (Byte) cbLogOperClrS1PCL.SelectedIndex;
                }
                else if ((_crntPDL == ToolCommonData.ePrintLang.PCLXL) &&
                     cbLogOperClrS1PCLXL.HasItems)
                {
                    _indxLogOperClrS1PCLXL =
                        (Byte) cbLogOperClrS1PCLXL.SelectedIndex;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b L o g O p e r C l r S 2 _ S e l e c t i o n C h a n g e d      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Colour index item for sample Source image colour 2 has changed.    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbLogOperClrS2_SelectionChanged (
            object sender,
            SelectionChangedEventArgs e)
        {
            if (_initialised)
            {
                if ((_crntPDL == ToolCommonData.ePrintLang.PCL) &&
                     cbLogOperClrS2PCL.HasItems)
                {
                    _indxLogOperClrS2PCL =
                        (Byte) cbLogOperClrS2PCL.SelectedIndex;
                }
                else if ((_crntPDL == ToolCommonData.ePrintLang.PCLXL) &&
                     cbLogOperClrS2PCLXL.HasItems)
                {
                    _indxLogOperClrS2PCLXL =
                        (Byte) cbLogOperClrS2PCLXL.SelectedIndex;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b L o g O p e r C l r T 1 _ S e l e c t i o n C h a n g e d      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Colour index item for sample Texture (pattern) has changed.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbLogOperClrT1_SelectionChanged (
            object sender,
            SelectionChangedEventArgs e)
        {
            if (_initialised)
            {
                if ((_crntPDL == ToolCommonData.ePrintLang.PCL) &&
                     cbLogOperClrT1PCL.HasItems)
                {
                    _indxLogOperClrT1PCL =
                        (Byte) cbLogOperClrT1PCL.SelectedIndex;
                }
                else if ((_crntPDL == ToolCommonData.ePrintLang.PCLXL) &&
                     cbLogOperClrT1PCLXL.HasItems)
                {
                    _indxLogOperClrT1PCLXL =
                        (Byte) cbLogOperClrT1PCLXL.SelectedIndex;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b L o g O p e r C l r T 2 _ S e l e c t i o n C h a n g e d      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Colour index item for sample Texture (pattern) colour 2 has        //
        // changed.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbLogOperClrT2_SelectionChanged (
            object sender,
            SelectionChangedEventArgs e)
        {
            if (_initialised)
            {
                if ((_crntPDL == ToolCommonData.ePrintLang.PCL) &&
                     cbLogOperClrT2PCL.HasItems)
                {
                    _indxLogOperClrT2PCL =
                        (Byte) cbLogOperClrT2PCL.SelectedIndex;
                }
                else if ((_crntPDL == ToolCommonData.ePrintLang.PCLXL) &&
                     cbLogOperClrT2PCLXL.HasItems)
                {
                    _indxLogOperClrT2PCLXL =
                        (Byte) cbLogOperClrT2PCLXL.SelectedIndex;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b L o g O p e r G r a y D 1 _ S e l e c t i o n C h a n g e d    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Gray index item for sample Destination has changed.                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbLogOperGrayD1_SelectionChanged (
            object sender,
            SelectionChangedEventArgs e)
        {
            if (_initialised)
            {
                if ((_crntPDL == ToolCommonData.ePrintLang.PCL) &&
                     cbLogOperMonoD1PCL.HasItems)
                {
                    _indxLogOperMonoD1PCL =
                        (Byte)cbLogOperMonoD1PCL.SelectedIndex;
                }
                else if ((_crntPDL == ToolCommonData.ePrintLang.PCLXL) &&
                     cbLogOperGrayD1PCLXL.HasItems)
                {
                    _indxLogOperGrayD1PCLXL =
                        (Byte) cbLogOperGrayD1PCLXL.SelectedIndex;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b L o g O p e r G r a y D 2 _ S e l e c t i o n C h a n g e d    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Gray index item for sample Destination shade 2 has changed.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbLogOperGrayD2_SelectionChanged (
            object sender,
            SelectionChangedEventArgs e)
        {
            if (_initialised)
            {
                if ((_crntPDL == ToolCommonData.ePrintLang.PCL) &&
                    cbLogOperMonoD2PCL.HasItems)
                {
                    _indxLogOperMonoD2PCL =
                        (Byte)cbLogOperMonoD2PCL.SelectedIndex;
                }
                else if ((_crntPDL == ToolCommonData.ePrintLang.PCLXL) &&
                     cbLogOperGrayD2PCLXL.HasItems)
                {
                    _indxLogOperGrayD2PCLXL =
                        (Byte) cbLogOperGrayD2PCLXL.SelectedIndex;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b L o g O p e r G r a y S 1 _ S e l e c t i o n C h a n g e d    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Gray index item for sample Source image has changed.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbLogOperGrayS1_SelectionChanged (
            object sender,
            SelectionChangedEventArgs e)
        {
            if (_initialised)
            {
                if ((_crntPDL == ToolCommonData.ePrintLang.PCL) &&
                    cbLogOperMonoS1PCL.HasItems)
                {
                    _indxLogOperMonoS1PCL =
                        (Byte)cbLogOperMonoS1PCL.SelectedIndex;
                }
                else if ((_crntPDL == ToolCommonData.ePrintLang.PCLXL) &&
                     cbLogOperGrayS1PCLXL.HasItems)
                {
                    _indxLogOperGrayS1PCLXL =
                        (Byte) cbLogOperGrayS1PCLXL.SelectedIndex;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b L o g O p e r G r a y S 2 _ S e l e c t i o n C h a n g e d    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Gray index item for sample Source image shade 2 has changed.       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbLogOperGrayS2_SelectionChanged (
            object sender,
            SelectionChangedEventArgs e)
        {
            if (_initialised)
            {
                if ((_crntPDL == ToolCommonData.ePrintLang.PCL) &&
                    cbLogOperMonoS2PCL.HasItems)
                {
                    _indxLogOperMonoS2PCL =
                        (Byte)cbLogOperMonoS2PCL.SelectedIndex;
                }
                else if ((_crntPDL == ToolCommonData.ePrintLang.PCLXL) &&
                     cbLogOperGrayS2PCLXL.HasItems)
                {
                    _indxLogOperGrayS2PCLXL =
                        (Byte) cbLogOperGrayS2PCLXL.SelectedIndex;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b L o g O p e r G r a y T 1 _ S e l e c t i o n C h a n g e d    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Gray index item for sample Texture (pattern) has changed.          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbLogOperGrayT1_SelectionChanged (
            object sender,
            SelectionChangedEventArgs e)
        {
            if (_initialised)
            {
                if ((_crntPDL == ToolCommonData.ePrintLang.PCL) &&
                    cbLogOperMonoT1PCL.HasItems)
                {
                    _indxLogOperMonoT1PCL =
                        (Byte)cbLogOperMonoT1PCL.SelectedIndex;
                }
                else if ((_crntPDL == ToolCommonData.ePrintLang.PCLXL) &&
                     cbLogOperGrayT1PCLXL.HasItems)
                {
                    _indxLogOperGrayT1PCLXL =
                        (Byte) cbLogOperGrayT1PCLXL.SelectedIndex;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b L o g O p e r G r a y T 2 _ S e l e c t i o n C h a n g e d    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Gray index item for sample Texture (pattern) shade 2 has changed.  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbLogOperGrayT2_SelectionChanged (
            object sender,
            SelectionChangedEventArgs e)
        {
            if (_initialised)
            {
                if ((_crntPDL == ToolCommonData.ePrintLang.PCL) &&
                    cbLogOperMonoT2PCL.HasItems)
                {
                    _indxLogOperMonoT2PCL =
                        (Byte)cbLogOperMonoT2PCL.SelectedIndex;
                }
                else if ((_crntPDL == ToolCommonData.ePrintLang.PCLXL) &&
                     cbLogOperGrayT2PCLXL.HasItems)
                {
                    _indxLogOperGrayT2PCLXL =
                        (Byte) cbLogOperGrayT2PCLXL.SelectedIndex;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b L o g O p e r M o d e _ S e l e c t i o n C h a n g e d        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Index item for colour mode has changed.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbLogOperMode_SelectionChanged (
            object sender,
            SelectionChangedEventArgs e)
        {
            if (_initialised)
            {
                if ((_crntPDL == ToolCommonData.ePrintLang.PCL) &&
                    cbLogOperModePCL.HasItems)
                {
                    _indxLogOperModePCL = (Byte) cbLogOperModePCL.SelectedIndex;

                    if (PCLPalettes.isMonochrome (_indxLogOperModePCL))
                    {
                        tabLogOperClrMonoPCL.IsSelected = true;

                        initialiseDataLogOperMonoPCL ();
                    }
                    else
                    {
                        tabLogOperClrSimplePCL.IsSelected = true;

                        initialiseDataLogOperClrsPCL ();
                    }
                }
                else if ((_crntPDL == ToolCommonData.ePrintLang.PCLXL) &&
                    cbLogOperModePCLXL.HasItems)
                {
                    _indxLogOperModePCLXL = (Byte) cbLogOperModePCLXL.SelectedIndex;

                    if (PCLXLPalettes.isMonochrome (_indxLogOperModePCLXL))
                    {
                        tabLogOperClrGrayPCLXL.IsSelected = true;

                        initialiseDataLogOperGrayPCLXL ();
                    }
                    else
                    {
                        tabLogOperClrRGBPCLXL.IsSelected = true;

                        initialiseDataLogOperClrsPCLXL ();
                    }
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b L o g O p e r R O P F r o m _ S e l e c t i o n C h a n g e d  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Index item for ROP range start has changed.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbLogOperROPFrom_SelectionChanged (
            object sender,
            SelectionChangedEventArgs e)
        {
            if (_initialised)
            {
                if ((_crntPDL == ToolCommonData.ePrintLang.PCL) &&
                    cbLogOperROPFromPCL.HasItems)
                {
                    Int32 temp = cbLogOperROPFromPCL.SelectedIndex;

                    if (temp <= _indxLogOperROPToPCL)
                    {
                        _indxLogOperROPFromPCL = temp;
                    }
                    else
                    {
                        Int32 toVal   = (_logOperROPInc *
                                        (_indxLogOperROPToPCL + 1)) - 1,
                              fromVal = (_logOperROPInc * temp),
                              maxVal  = (_logOperROPInc *
                                        _indxLogOperROPToPCL);
 
                        MessageBox.Show ("'From' value " + fromVal +
                                         " > 'To' value " + toVal + "\r\n" +
                                         "Reset to maximum consistent value " +
                                         maxVal,
                                        "ROP range",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);

                        cbLogOperROPFromPCL.SelectedIndex =
                            _indxLogOperROPToPCL;
                    }
                }
                else if ((_crntPDL == ToolCommonData.ePrintLang.PCLXL) &&
                    cbLogOperROPFromPCLXL.HasItems)
                {
                    Int32 temp = cbLogOperROPFromPCLXL.SelectedIndex;

                    if (temp <= _indxLogOperROPToPCLXL)
                    {
                        _indxLogOperROPFromPCLXL = temp;
                    }
                    else
                    {
                        Int32 toVal = (_logOperROPInc *
                                        (_indxLogOperROPToPCLXL + 1)) - 1,
                              fromVal = (_logOperROPInc * temp),
                              maxVal  = (_logOperROPInc *
                                        _indxLogOperROPToPCLXL);

                        MessageBox.Show ("'From' value " + fromVal +
                                         " > 'To' value " + toVal + "\r\n" +
                                         "Reset to maximum consistent value " +
                                         maxVal,
                                        "ROP range",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);

                        cbLogOperROPFromPCL.SelectedIndex =
                            _indxLogOperROPToPCL;
                    }
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b L o g O p e r R O P T o _ S e l e c t i o n C h a n g e d      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Index item for ROP range end has changed.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbLogOperROPTo_SelectionChanged (
            object sender,
            SelectionChangedEventArgs e)
        {
            if (_initialised)
            {
                if ((_crntPDL == ToolCommonData.ePrintLang.PCL) &&
                    cbLogOperROPToPCL.HasItems)
                {
                    Int32 temp = cbLogOperROPToPCL.SelectedIndex;

                    if (temp >= _indxLogOperROPFromPCL)
                    {
                        _indxLogOperROPToPCL = temp;
                    }
                    else
                    {
                        Int32 toVal   = (_logOperROPInc * (temp + 1)) - 1 ,
                              fromVal = (_logOperROPInc *
                                         _indxLogOperROPFromPCL),
                              minVal  = fromVal + (_logOperROPInc - 1);

                        MessageBox.Show ("'To' value " + toVal +
                                         " < 'From' value " + fromVal + "\r\n" +
                                         "Reset to minimum consistent value " +
                                         minVal,
                                        "ROP range",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);

                        cbLogOperROPToPCL.SelectedIndex =
                            _indxLogOperROPFromPCL;
                    }
                }
                else if ((_crntPDL == ToolCommonData.ePrintLang.PCLXL) &&
                    cbLogOperROPToPCLXL.HasItems)
                {
                    Int32 temp = cbLogOperROPToPCLXL.SelectedIndex;

                    if (temp >= _indxLogOperROPFromPCLXL)
                    {
                        _indxLogOperROPToPCLXL = temp;
                    }
                    else
                    {
                        Int32 toVal   = (_logOperROPInc * (temp + 1)) - 1 ,
                              fromVal = (_logOperROPInc *
                                         _indxLogOperROPFromPCLXL),
                              minVal  = fromVal + (_logOperROPInc - 1);

                        MessageBox.Show ("'To' value " + toVal +
                                         " < 'From' value " + fromVal + "\r\n" +
                                         "Reset to minimum consistent value " +
                                         minVal,
                                        "ROP range",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);

                        cbLogOperROPToPCLXL.SelectedIndex =
                            _indxLogOperROPFromPCLXL;
                    }
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n i t i a l i s e D a t a L o g O p e r                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initialise 'Logical operations' data.                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void initialiseDataLogOper(Boolean typeChange)
        {
            Int16 index;

            if (typeChange)
            {
                Int16 tempCt;
                Int16 tempInc;

                //------------------------------------------------------------//
                //                                                            //
                // Populate form.                                             //
                //                                                            //
                //------------------------------------------------------------//

                lbOrientation.Visibility = Visibility.Hidden;
                cbOrientation.Visibility = Visibility.Hidden;

                _maxLogOperClrItemsPCL   = 0;
                _maxLogOperClrItemsPCLXL = 0;

                //------------------------------------------------------------//
                //                                                            //
                // PCL.                                                       //
                //                                                            //
                //------------------------------------------------------------//

                cbLogOperModePCL.Items.Clear ();

                _ctLogOperModesPCL = _subsetLogOperModesPCL.Length;
                tempCt = 0;

                for (Int32 i = 0; i < _ctLogOperModesPCL; i++)
                {
                    index = _subsetLogOperModesPCL[i];

                    cbLogOperModePCL.Items.Add (PCLPalettes.getPaletteName (index));

                    tempCt = PCLPalettes.getCtClrItems (index);

                    if (tempCt > _maxLogOperClrItemsPCL)
                        _maxLogOperClrItemsPCL = tempCt;
                }

                if ((_indxLogOperModePCL < 0) ||
                    (_indxLogOperModePCL >= _ctLogOperModesPCL))
                    _indxLogOperModePCL = 0;

                //------------------------------------------------------------//

                _clrsLogOperPCL = new String [_ctLogOperModesPCL,
                                              _maxLogOperClrItemsPCL];

                _ctLogOperClrsPCL = new Int16 [_ctLogOperModesPCL];

                for (Int32 i = 0; i < _ctLogOperModesPCL; i++)
                {
                    index = _subsetLogOperModesPCL[i];

                    tempCt = PCLPalettes.getCtClrItems (index);
                    _ctLogOperClrsPCL[i] = tempCt;

                    for (Int16 j = 0; j < tempCt; j++)
                    {
                        _clrsLogOperPCL[i, j] =
                            PCLPalettes.getColourName (index, j);
                    }
                }

                cbLogOperModePCL.SelectedIndex   = _indxLogOperModePCL;

                //------------------------------------------------------------//

                cbLogOperROPFromPCL.Items.Clear ();
                cbLogOperROPToPCL.Items.Clear ();

                tempCt = (Int16) ((_logOperROPMax - _logOperROPMin) /
                                  _logOperROPInc);
                tempInc = (Int16) (_logOperROPInc - 1);

                for (Int32 i = _logOperROPMin; i < _logOperROPMax;
                           i += _logOperROPInc)
                {
                    cbLogOperROPFromPCL.Items.Add (i.ToString ());
                    cbLogOperROPToPCL.Items.Add ((i + tempInc).ToString ());
                }

                if ((_indxLogOperROPFromPCL < _logOperROPMin) ||
                    (_indxLogOperROPFromPCL > tempCt))
                    _indxLogOperROPFromPCL = 0;

                cbLogOperROPFromPCL.SelectedIndex = _indxLogOperROPFromPCL;

                if ((_indxLogOperROPToPCL < _indxLogOperROPFromPCL) ||
                    (_indxLogOperROPToPCL > tempCt))
                    _indxLogOperROPToPCL = tempCt;

                cbLogOperROPToPCL.SelectedIndex = _indxLogOperROPToPCL;

                //------------------------------------------------------------//
                //                                                            //
                // PCL XL.                                                    //
                //                                                            //
                //------------------------------------------------------------//

                cbLogOperModePCLXL.Items.Clear ();

                _ctLogOperModesPCLXL = _subsetLogOperModesPCLXL.Length;
                tempCt = 0;

                for (Int32 i = 0; i < _ctLogOperModesPCLXL; i++)
                {
                    index = _subsetLogOperModesPCLXL[i];

                    cbLogOperModePCLXL.Items.Add (
                        PCLXLPalettes.getPaletteName (index));

                    tempCt = PCLXLPalettes.getCtClrItems (index);

                    if (tempCt > _maxLogOperClrItemsPCLXL)
                        _maxLogOperClrItemsPCLXL = tempCt;
                }

                if ((_indxLogOperModePCLXL < 0) ||
                    (_indxLogOperModePCLXL >= _ctLogOperModesPCLXL))
                    _indxLogOperModePCLXL = 0;

                //------------------------------------------------------------//

                _clrsLogOperPCLXL = new String [_ctLogOperModesPCLXL,
                                                _maxLogOperClrItemsPCLXL];

                _ctLogOperClrsPCLXL = new Int16 [_ctLogOperModesPCLXL];

                for (Int32 i = 0; i < _ctLogOperModesPCLXL; i++)
                {
                    index = _subsetLogOperModesPCLXL[i];

                    tempCt = PCLXLPalettes.getCtClrItems (index);
                    _ctLogOperClrsPCLXL [i] = tempCt;

                    for (Int16 j = 0; j < tempCt; j++)
                    {
                        _clrsLogOperPCLXL[i, j] =
                            PCLXLPalettes.getColourName (index, j);
                    }
                }

                cbLogOperModePCLXL.SelectedIndex = _indxLogOperModePCLXL;

                //------------------------------------------------------------//

                cbLogOperROPFromPCLXL.Items.Clear ();
                cbLogOperROPToPCLXL.Items.Clear ();

                tempCt = (Int16) ((_logOperROPMax - _logOperROPMin) /
                                   _logOperROPInc);
                tempInc = (Int16) (_logOperROPInc - 1);

                for (Int32 i = _logOperROPMin; i < _logOperROPMax;
                           i += _logOperROPInc)
                {
                    cbLogOperROPFromPCLXL.Items.Add (i.ToString ());
                    cbLogOperROPToPCLXL.Items.Add ((i + tempInc).ToString ());
                }

                if ((_indxLogOperROPFromPCLXL < _logOperROPMin) ||
                    (_indxLogOperROPFromPCLXL > tempCt))
                    _indxLogOperROPFromPCLXL = 0;

                cbLogOperROPFromPCLXL.SelectedIndex = _indxLogOperROPFromPCLXL;

                if ((_indxLogOperROPToPCLXL < _indxLogOperROPFromPCLXL) ||
                    (_indxLogOperROPToPCLXL > tempCt))
                    _indxLogOperROPToPCLXL = tempCt;

                cbLogOperROPToPCLXL.SelectedIndex = _indxLogOperROPToPCLXL;

                if (_flagLogOperOptSrcTextPatPCLXL)
                    rbLogOperOptSrcTextPatPCLXL.IsChecked = true;
                else
                    rbLogOperOptSrcTextClrPCLXL.IsChecked = true;
            }

            //----------------------------------------------------------------//
            
            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                tabLogOperDataPCL.IsSelected = true;

                if (PCLPalettes.isMonochrome (_indxLogOperModePCL))
                {
                    tabLogOperClrMonoPCL.IsSelected = true;
            
                    initialiseDataLogOperMonoPCL ();
                }
                else
                {
                    tabLogOperClrSimplePCL.IsSelected = true;
            
                    initialiseDataLogOperClrsPCL ();
                }

                if (_flagLogOperUseMacrosPCL)
                    chkOptFormAsMacro.IsChecked = true;
                else
                    chkOptFormAsMacro.IsChecked = false;
            }
            else
            {
                tabLogOperDataPCLXL.IsSelected = true;

                if (PCLXLPalettes.isMonochrome (_indxLogOperModePCLXL))
                {
                    tabLogOperClrGrayPCLXL.IsSelected = true;
            
                    initialiseDataLogOperGrayPCLXL ();
                }
                else
                {
                    tabLogOperClrRGBPCLXL.IsSelected = true;
            
                    initialiseDataLogOperClrsPCLXL ();
                }

                if (_flagLogOperUseMacrosPCLXL)
                    chkOptFormAsMacro.IsChecked = true;
                else
                    chkOptFormAsMacro.IsChecked = false;

                if (_flagLogOperOptSrcTextPatPCLXL)
                    rbLogOperOptSrcTextPatPCLXL.IsChecked = true;
                else
                    rbLogOperOptSrcTextClrPCLXL.IsChecked = false;
            }

            //----------------------------------------------------------------//

            initialiseDescLogOper ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n i t i a l i s e D a t a L o g O p e r C l r s P C L            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initialise 'Logical operations' PCL colour data.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void initialiseDataLogOperClrsPCL ()
        {
            Int16 tempCt;

            //------------------------------------------------------------//
            //                                                            //
            // Colour 1                                                   //
            //                                                            //
            //------------------------------------------------------------//

            cbLogOperClrD1PCL.Items.Clear ();
            cbLogOperClrS1PCL.Items.Clear ();
            cbLogOperClrT1PCL.Items.Clear ();

            tempCt = _ctLogOperClrsPCL[_indxLogOperModePCL];

            for (Int32 i = 0; i < tempCt; i++)
            {
                cbLogOperClrD1PCL.Items.Add (
                    _clrsLogOperPCL[_indxLogOperModePCL, i]);

                cbLogOperClrS1PCL.Items.Add (
                    _clrsLogOperPCL[_indxLogOperModePCL, i]);

                cbLogOperClrT1PCL.Items.Add (
                    _clrsLogOperPCL[_indxLogOperModePCL, i]);
            }

            //------------------------------------------------------------//

            if ((_indxLogOperClrD1PCL < 0) ||
                (_indxLogOperClrD1PCL > tempCt))
                _indxLogOperClrD1PCL = 0;

            if ((_indxLogOperClrS1PCL < 0) ||
                (_indxLogOperClrS1PCL > tempCt))
                _indxLogOperClrS1PCL = 0;

            if ((_indxLogOperClrT1PCL < 0) ||
                (_indxLogOperClrT1PCL > tempCt))
                _indxLogOperClrT1PCL = 0;

            cbLogOperClrD1PCL.SelectedIndex = _indxLogOperClrD1PCL;
            cbLogOperClrS1PCL.SelectedIndex = _indxLogOperClrS1PCL;
            cbLogOperClrT1PCL.SelectedIndex = _indxLogOperClrT1PCL;

            //------------------------------------------------------------//
            //                                                            //
            // Colour 2                                                   //
            //                                                            //
            //------------------------------------------------------------//

            cbLogOperClrD2PCL.Items.Clear ();
            cbLogOperClrS2PCL.Items.Clear ();
            cbLogOperClrT2PCL.Items.Clear ();

            tempCt = _ctLogOperClrsPCL[_indxLogOperModePCL];

            for (Int32 i = 0; i < tempCt; i++)
            {
                cbLogOperClrD2PCL.Items.Add (
                    _clrsLogOperPCL[_indxLogOperModePCL, i]);

                cbLogOperClrS2PCL.Items.Add (
                    _clrsLogOperPCL[_indxLogOperModePCL, i]);

                cbLogOperClrT2PCL.Items.Add (
                    _clrsLogOperPCL[_indxLogOperModePCL, i]);
            }

            //------------------------------------------------------------//

            if ((_indxLogOperClrD2PCL < 0) ||
                (_indxLogOperClrD2PCL > tempCt))
                _indxLogOperClrD2PCL = 0;

            if ((_indxLogOperClrS2PCL < 0) ||
                (_indxLogOperClrS2PCL > tempCt))
                _indxLogOperClrS2PCL = 0;

            if ((_indxLogOperClrT2PCL < 0) ||
                (_indxLogOperClrT2PCL > tempCt))
                _indxLogOperClrT2PCL = 0;

            cbLogOperClrD2PCL.SelectedIndex = _indxLogOperClrD2PCL;
            cbLogOperClrS2PCL.SelectedIndex = _indxLogOperClrS2PCL;
            cbLogOperClrT2PCL.SelectedIndex = _indxLogOperClrT2PCL;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n i t i a l i s e D a t a L o g O p e r C l r s P C L X L        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initialise 'Logical operations' PCLXL colour data.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void initialiseDataLogOperClrsPCLXL ()
        {
            Int16 tempCt;

            //------------------------------------------------------------//
            //                                                            //
            // Colour 1                                                   //
            //                                                            //
            //------------------------------------------------------------//

            cbLogOperClrD1PCLXL.Items.Clear ();
            cbLogOperClrS1PCLXL.Items.Clear ();
            cbLogOperClrT1PCLXL.Items.Clear ();

            tempCt = _ctLogOperClrsPCLXL[_indxLogOperModePCLXL];

            for (Int32 i = 0; i < tempCt; i++)
            {
                cbLogOperClrD1PCLXL.Items.Add (
                    _clrsLogOperPCLXL[_indxLogOperModePCLXL, i]);

                cbLogOperClrS1PCLXL.Items.Add (
                    _clrsLogOperPCLXL[_indxLogOperModePCLXL, i]);

                cbLogOperClrT1PCLXL.Items.Add (
                    _clrsLogOperPCLXL[_indxLogOperModePCLXL, i]);
            }

            //------------------------------------------------------------//

            if ((_indxLogOperClrD1PCLXL < 0) ||
                (_indxLogOperClrD1PCLXL > tempCt))
                _indxLogOperClrD1PCLXL = 0;

            if ((_indxLogOperClrS1PCLXL < 0) ||
                (_indxLogOperClrS1PCLXL > tempCt))
                _indxLogOperClrS1PCLXL = 0;

            if ((_indxLogOperClrT1PCLXL < 0) ||
                (_indxLogOperClrT1PCLXL > tempCt))
                _indxLogOperClrT1PCLXL = 0;

            cbLogOperClrD1PCLXL.SelectedIndex = _indxLogOperClrD1PCLXL;
            cbLogOperClrS1PCLXL.SelectedIndex = _indxLogOperClrS1PCLXL;
            cbLogOperClrT1PCLXL.SelectedIndex = _indxLogOperClrT1PCLXL;

            //------------------------------------------------------------//
            //                                                            //
            // Colour 2                                                   //
            //                                                            //
            //------------------------------------------------------------//

            cbLogOperClrD2PCLXL.Items.Clear ();
            cbLogOperClrS2PCLXL.Items.Clear ();
            cbLogOperClrT2PCLXL.Items.Clear ();

            tempCt = _ctLogOperClrsPCLXL[_indxLogOperModePCLXL];

            for (Int32 i = 0; i < tempCt; i++)
            {
                cbLogOperClrD2PCLXL.Items.Add (
                    _clrsLogOperPCLXL[_indxLogOperModePCLXL, i]);

                cbLogOperClrS2PCLXL.Items.Add (
                    _clrsLogOperPCLXL[_indxLogOperModePCLXL, i]);

                cbLogOperClrT2PCLXL.Items.Add (
                    _clrsLogOperPCLXL[_indxLogOperModePCLXL, i]);
            }

            //------------------------------------------------------------//

            if ((_indxLogOperClrD2PCLXL < 0) ||
                (_indxLogOperClrD2PCLXL > tempCt))
                _indxLogOperClrD2PCLXL = 0;

            if ((_indxLogOperClrS2PCLXL < 0) ||
                (_indxLogOperClrS2PCLXL > tempCt))
                _indxLogOperClrS2PCLXL = 0;

            if ((_indxLogOperClrT2PCLXL < 0) ||
                (_indxLogOperClrT2PCLXL > tempCt))
                _indxLogOperClrT2PCLXL = 0;

            cbLogOperClrD2PCLXL.SelectedIndex = _indxLogOperClrD2PCLXL;
            cbLogOperClrS2PCLXL.SelectedIndex = _indxLogOperClrS2PCLXL;
            cbLogOperClrT2PCLXL.SelectedIndex = _indxLogOperClrT2PCLXL;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n i t i a l i s e D a t a L o g O p e r G r a y P C L X L        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initialise 'Logical operations' PCLXL gray level data.             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void initialiseDataLogOperGrayPCLXL ()
        {
            const Byte minLevel = 0;
            const Byte maxLevel = 255;

            const Int32 grayPalette =
                (Int32) PCLXLPalettes.eIndex.PCLXLGray;

            //------------------------------------------------------------//
            //                                                            //
            // Shade 1                                                    //
            //                                                            //
            //------------------------------------------------------------//

            cbLogOperGrayD1PCLXL.Items.Clear ();
            cbLogOperGrayS1PCLXL.Items.Clear ();
            cbLogOperGrayT1PCLXL.Items.Clear ();

            cbLogOperGrayD2PCLXL.Items.Clear ();
            cbLogOperGrayS2PCLXL.Items.Clear ();
            cbLogOperGrayT2PCLXL.Items.Clear ();

            for (Int32 i = minLevel; i <= maxLevel; i++)
            {
                String name = PCLXLPalettes.getGrayLevel (grayPalette,
                                                          (Byte) i);

                cbLogOperGrayD1PCLXL.Items.Add(name);
                cbLogOperGrayS1PCLXL.Items.Add(name);
                cbLogOperGrayT1PCLXL.Items.Add(name);

                cbLogOperGrayD2PCLXL.Items.Add(name);
                cbLogOperGrayS2PCLXL.Items.Add(name);
                cbLogOperGrayT2PCLXL.Items.Add(name);
            }

            //------------------------------------------------------------//
            //                                                            //
            // Shade 1                                                    //
            //                                                            //
            //------------------------------------------------------------//

            if ((_indxLogOperGrayD1PCLXL < minLevel) ||
                (_indxLogOperGrayD1PCLXL > maxLevel))
                _indxLogOperGrayD1PCLXL = minLevel;

            if ((_indxLogOperGrayS1PCLXL < minLevel) ||
                (_indxLogOperGrayS1PCLXL > maxLevel))
                _indxLogOperGrayS1PCLXL = minLevel;

            if ((_indxLogOperGrayT1PCLXL < minLevel) ||
                (_indxLogOperGrayT1PCLXL > maxLevel))
                _indxLogOperGrayT1PCLXL = minLevel;

            cbLogOperGrayD1PCLXL.SelectedIndex = _indxLogOperGrayD1PCLXL;
            cbLogOperGrayS1PCLXL.SelectedIndex = _indxLogOperGrayS1PCLXL;
            cbLogOperGrayT1PCLXL.SelectedIndex = _indxLogOperGrayT1PCLXL;

            //------------------------------------------------------------//
            //                                                            //
            // Shade 2                                                    //
            //                                                            //
            //------------------------------------------------------------//

            if ((_indxLogOperGrayD2PCLXL < minLevel) ||
                (_indxLogOperGrayD2PCLXL > maxLevel))
                _indxLogOperGrayD2PCLXL = 0;

            if ((_indxLogOperGrayS2PCLXL < minLevel) ||
                (_indxLogOperGrayS2PCLXL > maxLevel))
                _indxLogOperGrayS2PCLXL = 0;

            if ((_indxLogOperGrayT2PCLXL < minLevel) ||
                (_indxLogOperGrayT2PCLXL > maxLevel))
                _indxLogOperGrayT2PCLXL = 0;

            cbLogOperGrayD2PCLXL.SelectedIndex = _indxLogOperGrayD2PCLXL;
            cbLogOperGrayS2PCLXL.SelectedIndex = _indxLogOperGrayS2PCLXL;
            cbLogOperGrayT2PCLXL.SelectedIndex = _indxLogOperGrayT2PCLXL;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n i t i a l i s e D a t a L o g O p e r M o n o P C L            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initialise 'Logical operations' PCL monochrome data.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void initialiseDataLogOperMonoPCL ()
        {
            Int16 tempCt;

            //------------------------------------------------------------//
            //                                                            //
            // Colour 1                                                   //
            //                                                            //
            //------------------------------------------------------------//

            cbLogOperMonoD1PCL.Items.Clear ();
            cbLogOperMonoS1PCL.Items.Clear ();
            cbLogOperMonoT1PCL.Items.Clear ();

            tempCt = _ctLogOperClrsPCL[_indxLogOperModePCL];

            for (Int32 i = 0; i < tempCt; i++)
            {
                cbLogOperMonoD1PCL.Items.Add (
                    _clrsLogOperPCL[_indxLogOperModePCL, i]);

                cbLogOperMonoS1PCL.Items.Add (
                    _clrsLogOperPCL[_indxLogOperModePCL, i]);

                cbLogOperMonoT1PCL.Items.Add (
                    _clrsLogOperPCL[_indxLogOperModePCL, i]);
            }

            //------------------------------------------------------------//

            if ((_indxLogOperMonoD1PCL < 0) ||
                (_indxLogOperMonoD1PCL > tempCt))
                _indxLogOperMonoD1PCL = 0;

            if ((_indxLogOperMonoS1PCL < 0) ||
                (_indxLogOperMonoS1PCL > tempCt))
                _indxLogOperMonoS1PCL = 0;

            if ((_indxLogOperMonoT1PCL < 0) ||
                (_indxLogOperMonoT1PCL > tempCt))
                _indxLogOperMonoT1PCL = 0;

            cbLogOperMonoD1PCL.SelectedIndex = _indxLogOperMonoD1PCL;
            cbLogOperMonoS1PCL.SelectedIndex = _indxLogOperMonoS1PCL;
            cbLogOperMonoT1PCL.SelectedIndex = _indxLogOperMonoT1PCL;

            //------------------------------------------------------------//
            //                                                            //
            // Colour 2                                                   //
            //                                                            //
            //------------------------------------------------------------//

            cbLogOperMonoD2PCL.Items.Clear ();
            cbLogOperMonoS2PCL.Items.Clear ();
            cbLogOperMonoT2PCL.Items.Clear ();

            tempCt = _ctLogOperClrsPCL[_indxLogOperModePCL];

            for (Int32 i = 0; i < tempCt; i++)
            {
                cbLogOperMonoD2PCL.Items.Add (
                    _clrsLogOperPCL[_indxLogOperModePCL, i]);

                cbLogOperMonoS2PCL.Items.Add (
                    _clrsLogOperPCL[_indxLogOperModePCL, i]);

                cbLogOperMonoT2PCL.Items.Add (
                    _clrsLogOperPCL[_indxLogOperModePCL, i]);
            }

            //------------------------------------------------------------//

            if ((_indxLogOperMonoD2PCL < 0) ||
                (_indxLogOperMonoD2PCL > tempCt))
                _indxLogOperMonoD2PCL = 0;

            if ((_indxLogOperMonoS2PCL < 0) ||
                (_indxLogOperMonoS2PCL > tempCt))
                _indxLogOperMonoS2PCL = 0;

            if ((_indxLogOperMonoS2PCL < 0) ||
                (_indxLogOperMonoS2PCL > tempCt))
                _indxLogOperMonoT2PCL = 0;

            cbLogOperMonoD2PCL.SelectedIndex = _indxLogOperMonoD2PCL;
            cbLogOperMonoS2PCL.SelectedIndex = _indxLogOperMonoS2PCL;
            cbLogOperMonoT2PCL.SelectedIndex = _indxLogOperMonoT2PCL;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n i t i a l i s e D e s c L o g O p e r                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initialise 'Logical operations' description.                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void initialiseDescLogOper()
        {
            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                txtLogOperDesc.Text =
                    "Shows effect of the 256 different raster (logical)" +
                    " operations (ROPs), in conjunction with the possible" +
                    " combinations of Source and Pattern transparency.";
            }
            else
            {
                txtLogOperDesc.Text =
                    "Shows effect of the 256 different raster (logical)" +
                    " operations (ROPs), in conjunction with the possible" +
                    " combinations of Source and Pattern transparency.";
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m e t r i c s L o a d D a t a L o g O p e r                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Load current metrics from persistent storage.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void metricsLoadDataLogOper()
        {
            Boolean dummyBool = false;

            ToolMiscSamplesPersist.loadDataTypeLogOper (
                "PCL",
                ref _indxLogOperModePCL,
                ref _indxLogOperROPFromPCL,
                ref _indxLogOperROPToPCL,
                ref _indxLogOperClrD1PCL,
                ref _indxLogOperClrD2PCL,
                ref _indxLogOperClrS1PCL,
                ref _indxLogOperClrS2PCL,
                ref _indxLogOperClrT1PCL,
                ref _indxLogOperClrT2PCL,
                ref _indxLogOperMonoD1PCL,
                ref _indxLogOperMonoD2PCL,
                ref _indxLogOperMonoS1PCL,
                ref _indxLogOperMonoS2PCL,
                ref _indxLogOperMonoT1PCL,
                ref _indxLogOperMonoT2PCL,
                ref _flagLogOperUseMacrosPCL,
                ref dummyBool);

            ToolMiscSamplesPersist.loadDataTypeLogOper (
                "PCLXL",
                ref _indxLogOperModePCLXL,
                ref _indxLogOperROPFromPCLXL,
                ref _indxLogOperROPToPCLXL,
                ref _indxLogOperClrD1PCLXL,
                ref _indxLogOperClrD2PCLXL,
                ref _indxLogOperClrS1PCLXL,
                ref _indxLogOperClrS2PCLXL,
                ref _indxLogOperClrT1PCLXL,
                ref _indxLogOperClrT2PCLXL,
                ref _indxLogOperGrayD1PCLXL,
                ref _indxLogOperGrayD2PCLXL,
                ref _indxLogOperGrayS1PCLXL,
                ref _indxLogOperGrayS2PCLXL,
                ref _indxLogOperGrayT1PCLXL,
                ref _indxLogOperGrayT2PCLXL,
                ref _flagLogOperUseMacrosPCLXL,
                ref _flagLogOperOptSrcTextPatPCLXL);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m e t r i c s S a v e D a t a L o g O p e r                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Save current metrics to persistent storage.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void metricsSaveDataLogOper()
        {
            Boolean dummyBool = false;

            ToolMiscSamplesPersist.saveDataTypeLogOper (
                "PCL",
                _indxLogOperModePCL,
                _indxLogOperROPFromPCL,
                _indxLogOperROPToPCL,
                _indxLogOperClrD1PCL,
                _indxLogOperClrD2PCL,
                _indxLogOperClrS1PCL,
                _indxLogOperClrS2PCL,
                _indxLogOperClrT1PCL,
                _indxLogOperClrT2PCL,
                _indxLogOperMonoD1PCL,
                _indxLogOperMonoD2PCL,
                _indxLogOperMonoS1PCL,
                _indxLogOperMonoS2PCL,
                _indxLogOperMonoT1PCL,
                _indxLogOperMonoT2PCL,
                _flagLogOperUseMacrosPCL,
                dummyBool);

            ToolMiscSamplesPersist.saveDataTypeLogOper (
                "PCLXL",
                _indxLogOperModePCLXL,
                _indxLogOperROPFromPCLXL,
                _indxLogOperROPToPCLXL,
                _indxLogOperClrD1PCLXL,
                _indxLogOperClrD2PCLXL,
                _indxLogOperClrS1PCLXL,
                _indxLogOperClrS2PCLXL,
                _indxLogOperClrT1PCLXL,
                _indxLogOperClrT2PCLXL,
                _indxLogOperGrayD1PCLXL,
                _indxLogOperGrayD2PCLXL,
                _indxLogOperGrayS1PCLXL,
                _indxLogOperGrayS2PCLXL,
                _indxLogOperGrayT1PCLXL,
                _indxLogOperGrayT2PCLXL,
                _flagLogOperUseMacrosPCLXL,
                _flagLogOperOptSrcTextPatPCLXL);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b L o g O p e r O p t S r c T e x t C l r _ C l i c k            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the source text usage 'source colours/shades' radio    //
        // button is clicked.                                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbLogOperOptSrcTextClr_Click (object sender,
                                                   RoutedEventArgs e)
        {
            _flagLogOperOptSrcTextPatPCLXL = false;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b L o g O p e r O p t S r c T e x t P a t _ C l i c k            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the source text usage 'pattern' radio button is        //
        // clicked.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbLogOperOptSrcTextPat_Click (object sender,
                                                   RoutedEventArgs e)
        {
            _flagLogOperOptSrcTextPatPCLXL = true;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t F l a g L o g O p e r F o r m A s M a c r o                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set or unset 'use macros' flag.                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void setFlagLogOperFormAsMacro (
            Boolean setFlag,
            ToolCommonData.ePrintLang crntPDL)
        {
            if (crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                if (setFlag)
                    _flagLogOperUseMacrosPCL = true;
                else
                    _flagLogOperUseMacrosPCL = false;
            }
            else if (crntPDL == ToolCommonData.ePrintLang.PCLXL)
            {
                if (setFlag)
                    _flagLogOperUseMacrosPCLXL = true;
                else
                    _flagLogOperUseMacrosPCLXL = false;
            }
        }
    }
}
