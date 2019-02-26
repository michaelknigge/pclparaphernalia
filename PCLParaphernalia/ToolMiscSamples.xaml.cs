using System;
using System.IO;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
// using System.Windows.Media;

namespace PCLParaphernalia
{
    /// <summary>
    /// Interaction logic for ToolMiscSamples.xaml
    /// 
    /// Class handles the MiscSamples tool form.
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

        const UInt16 _unitsPerInch = 600;
        const UInt16 _dPtsPerInch = 720;

        const Double _unitsToDPts        = (1.00 * _dPtsPerInch / _unitsPerInch);
        const Double _unitsToInches      = (1.00 / _unitsPerInch);
        const Double _unitsToMilliMetres = (25.4 / _unitsPerInch);

        const Double _dPtsToInches       = (1.00 / 720);
        const Double _dPtsToMilliMetres  = (25.4 / 720);

        private enum eSampleType : byte
        {
            // must be in same order as _subsetTypes array

            Colour,
            LogOper,
            LogPage,     // PCL only
            Pattern,
            TxtMod,
            Unicode
        };

        private String[] sSampleNames = 
        {
            // must be in same order as _subsetTypes array

            "Colour",
            "Logical operations",
            "Logical page definition",     // PCL only
            "Patterns",
            "Text modification",
            "Unicode characters"
        };

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Fields (class variables).                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static Int32 [] _subsetPDLs =
        {
            (Int32) ToolCommonData.ePrintLang.PCL,
            (Int32) ToolCommonData.ePrintLang.PCLXL
        };

        private static Int32[] _subsetSampleTypes =
        {
            // must be in same order as eSampleTypes enumeration

            (Int32) ToolCommonData.eToolSubIds.Colour,
            (Int32) ToolCommonData.eToolSubIds.LogOper,
            (Int32) ToolCommonData.eToolSubIds.LogPage,
            (Int32) ToolCommonData.eToolSubIds.Pattern,
            (Int32) ToolCommonData.eToolSubIds.TxtMod,
            (Int32) ToolCommonData.eToolSubIds.Unicode
        };

        private static Int32[] _subsetOrientations = 
        {
            (Int32) PCLOrientations.eIndex.Portrait,
            (Int32) PCLOrientations.eIndex.Landscape,
            (Int32) PCLOrientations.eIndex.ReversePortrait,
            (Int32) PCLOrientations.eIndex.ReverseLandscape
        };

        private static Int32 [] _subsetPaperSizes =
        {
            (Int32) PCLPaperSizes.eIndex.ISO_A4,
            (Int32) PCLPaperSizes.eIndex.ANSI_A_Letter,
        };

        private static Int32 [] _subsetPaperTypes =
        {
            (Int32) PCLPaperTypes.eIndex.NotSet,
            (Int32) PCLPaperTypes.eIndex.Plain
        };

        private Int32 _indxPDL;
        private Int32 _indxSampleType;

        private ToolCommonData.ePrintLang  _crntPDL;

        private Int32 _ctPDLs;
        private Int32 _ctSampleTypes;
        private Int32 _ctOrientations;
        private Int32 _ctPaperSizes;
        private Int32 _ctPaperTypes;

        private Int32 _indxOrientationPCL;
        private Int32 _indxOrientationPCLXL;
        private Int32 _indxPaperSizePCL;
        private Int32 _indxPaperSizePCLXL;
        private Int32 _indxPaperTypePCL;
        private Int32 _indxPaperTypePCLXL;

        private UInt16 _paperSizeShortEdge;
        private UInt16 _paperSizeLongEdge;
        private UInt16 _paperMarginsLogicalLand;
        private UInt16 _paperMarginsLogicalPort;
        private UInt16 _paperMarginsLogicalLeft;
        private UInt16 _paperMarginsLogicalTop;
        private UInt16 _paperMarginsUnprintable;

        private UInt16 _paperWidthPrintable;
        private UInt16 _paperWidthLogical;
        private UInt16 _paperWidthPhysical;
        private UInt16 _paperLengthPrintable;
        private UInt16 _paperLengthLogical;
        private UInt16 _paperLengthPhysical;

        private Boolean _initialised;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // T o o l M i s c S a m p l e s                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public ToolMiscSamples (ref ToolCommonData.ePrintLang  crntPDL,
                                ref ToolCommonData.eToolSubIds crntType )
        {
            InitializeComponent();

            _initialised = false;

            initialise();

            _initialised = true;

            crntPDL  = _crntPDL;
            crntType =
                (ToolCommonData.eToolSubIds)_subsetSampleTypes[_indxSampleType];
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b t n G e n e r a t e _ C l i c k                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Generate Test Data' button is clicked.            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            //----------------------------------------------------------------//
            //                                                                //
            // Get current test metrics.                                      //
            // Note that the relevant (PDL-specific) stored option values     //
            // SHOULD already be up-to-date, since the fields all have        //
            // associated 'OnChange' actions. ***** Not with WPF ????? *****  //
            // But we'll save them all anyway, to make sure.                  //
            //                                                                //
            //----------------------------------------------------------------//

     //     btnGenerate.BorderBrush = Brushes.Red;

            _indxPDL = cbPDL.SelectedIndex;
            _crntPDL = (ToolCommonData.ePrintLang) _subsetPDLs[_indxPDL];

            pdlOptionsStore();

            //----------------------------------------------------------------//
            //                                                                //
            // Generate test print file.                                      //
            //                                                                //
            //----------------------------------------------------------------//

            try
            {
                BinaryWriter binWriter = null;

                ToolCommonData.eToolSubIds sampleType =
                    (ToolCommonData.eToolSubIds) _subsetSampleTypes[_indxSampleType];

                TargetCore.metricsLoadFileCapt (
                    ToolCommonData.eToolIds.MiscSamples,
                    sampleType,
                    _crntPDL);

                TargetCore.requestStreamOpen (
                    ref binWriter,
                    ToolCommonData.eToolIds.MiscSamples,
                    sampleType,
                    _crntPDL);

                if (_crntPDL == ToolCommonData.ePrintLang.PCL)
                {
                    switch (_indxSampleType)
                    {
                        case (Int32) eSampleType.Colour:

                            if (_indxColourTypePCL == eColourType.PCL_Simple)
                            {
                                ToolMiscSamplesActColourSimplePCL.generateJob (
                                    binWriter,
                                    _subsetPaperSizes[_indxPaperSizePCL],
                                    _subsetPaperTypes[_indxPaperTypePCL],
                                    _subsetOrientations[_indxOrientationPCL],
                                    _flagColourFormAsMacroPCL);
                            }
                            else if (_indxColourTypePCL == eColourType.PCL_Imaging)
                            {
                                ToolMiscSamplesActColourImagingPCL.generateJob (
                                    binWriter,
                                    _subsetPaperSizes[_indxPaperSizePCL],
                                    _subsetPaperTypes[_indxPaperTypePCL],
                                    _subsetOrientations[_indxOrientationPCL],
                                    _samplesPCL_CID,
                                    _flagColourFormAsMacroPCL);
                            }
                            break;

                        case (Int32) eSampleType.LogOper:

                            Int32 indxMode =
                                _subsetLogOperModesPCL[_indxLogOperModePCL];

                            if (PCLPalettes.isMonochrome(indxMode))
                            {
                                ToolMiscSamplesActLogOperPCL.generateJob(
                                    binWriter,
                                    _subsetPaperSizes[_indxPaperSizePCL],
                                    _subsetPaperTypes[_indxPaperTypePCL],
                                    _subsetOrientations[_indxOrientationPCL],
                                    indxMode,
                                    _indxLogOperMonoD1PCL,
                                    _indxLogOperMonoD2PCL,
                                    _indxLogOperMonoS1PCL,
                                    _indxLogOperMonoS2PCL,
                                    _indxLogOperMonoT1PCL,
                                    _indxLogOperMonoT2PCL,
                                    (_indxLogOperROPFromPCL * _logOperROPInc),
                                    ((_indxLogOperROPToPCL + 1) * _logOperROPInc) - 1,
                                    _flagLogOperUseMacrosPCL);
                            }
                            else
                            {
                                ToolMiscSamplesActLogOperPCL.generateJob(
                                    binWriter,
                                    _subsetPaperSizes[_indxPaperSizePCL],
                                    _subsetPaperTypes[_indxPaperTypePCL],
                                    _subsetOrientations[_indxOrientationPCL],
                                    indxMode,
                                    _indxLogOperClrD1PCL,
                                    _indxLogOperClrD2PCL,
                                    _indxLogOperClrS1PCL,
                                    _indxLogOperClrS2PCL,
                                    _indxLogOperClrT1PCL,
                                    _indxLogOperClrT2PCL,
                                    (_indxLogOperROPFromPCL * _logOperROPInc),
                                    ((_indxLogOperROPToPCL + 1) * _logOperROPInc) - 1,
                                    _flagLogOperUseMacrosPCL);
                            }

                            break;

                        case (Int32) eSampleType.LogPage:

                            ToolMiscSamplesActLogPagePCL.generateJob (
                                binWriter,
                                _subsetPaperSizes [_indxPaperSizePCL],
                                _subsetPaperTypes [_indxPaperTypePCL],
                                _subsetOrientations [_indxOrientationPCL],
                                (Int16) _logPageOffLeftDPt,
                                (Int16) _logPageOffTopDPt,
                                (UInt16) _logPageWidthDPt,
                                (UInt16) _logPageHeightDPt,
                                _flagLogPageFormAsMacroPCL,
                                _flagLogPageOptStdPagePCL);
                            break;

                        case (Int32) eSampleType.Pattern:

                            if (_indxPatternTypePCL == ePatternType.Shading)
                            {
                                ToolMiscSamplesActPatternShadePCL.generateJob(
                                    binWriter,
                                    _subsetPaperSizes[_indxPaperSizePCL],
                                    _subsetPaperTypes[_indxPaperTypePCL],
                                    (Int32)PCLOrientations.eIndex.Portrait,
                                    _flagPatternFormAsMacroPCL);
                            }
                            else if (_indxPatternTypePCL == ePatternType.XHatch)
                            {
                                ToolMiscSamplesActPatternXHatchPCL.generateJob(
                                    binWriter,
                                    _subsetPaperSizes[_indxPaperSizePCL],
                                    _subsetPaperTypes[_indxPaperTypePCL],
                                    (Int32)PCLOrientations.eIndex.Portrait,
                                    _flagPatternFormAsMacroPCL);
                            }
                            break;

                        case (Int32) eSampleType.TxtMod:

                            if (_indxTxtModTypePCL == eTxtModType.Chr)
                            {
                                ToolMiscSamplesActTxtModChrPCL.generateJob(
                                    binWriter,
                                    _subsetPaperSizes[_indxPaperSizePCL],
                                    _subsetPaperTypes[_indxPaperTypePCL],
                                    (Int32)PCLOrientations.eIndex.Portrait,
                                    _flagTxtModFormAsMacroPCL);
                            }
                            else if (_indxTxtModTypePCL == eTxtModType.Pat)
                            {
                                ToolMiscSamplesActTxtModPatPCL.generateJob(
                                    binWriter,
                                    _subsetPaperSizes[_indxPaperSizePCL],
                                    _subsetPaperTypes[_indxPaperTypePCL],
                                    (Int32)PCLOrientations.eIndex.Portrait,
                                    _flagTxtModFormAsMacroPCL);
                            }
                            else if (_indxTxtModTypePCL == eTxtModType.Rot)
                            {
                                ToolMiscSamplesActTxtModRotPCL.generateJob(
                                    binWriter,
                                    _subsetPaperSizes[_indxPaperSizePCL],
                                    _subsetPaperTypes[_indxPaperTypePCL],
                                    (Int32)PCLOrientations.eIndex.Portrait,
                                    _flagTxtModFormAsMacroPCL);
                            }

                            break;

                        case (Int32) eSampleType.Unicode:

                            ToolMiscSamplesActUnicodePCL.generateJob (
                                binWriter,
                                _subsetPaperSizes [_indxPaperSizePCL],
                                _subsetPaperTypes [_indxPaperTypePCL],
                                (Int32)PCLOrientations.eIndex.Portrait,
                                _flagUnicodeFormAsMacroPCL,
                                _unicodeUCS2PCL,
                                _subsetUnicodeFonts[_indxUnicodeFontPCL],
                                _unicodeFontVarPCL);
                            break;
                    }
                }
                else   // if (_crntPDL == ToolCommonData.ePrintLang.PCLXL)
                {
                    switch (_indxSampleType)
                    {
                        case (Int32) eSampleType.Colour:

                            if (_indxColourTypePCLXL == eColourType.PCLXL_Gray)
                            {
                                ToolMiscSamplesActColourGrayPCLXL.generateJob (
                                    binWriter,
                                    _subsetPaperSizes[_indxPaperSizePCLXL],
                                    _subsetPaperTypes[_indxPaperTypePCLXL],
                                    _subsetOrientations[_indxOrientationPCLXL],
                                    _samplesPCLXL_Gray,
                                    _flagColourFormAsMacroPCLXL);
                            }
                            else if (_indxColourTypePCLXL == eColourType.PCLXL_RGB)
                            {
                                ToolMiscSamplesActColourRGBPCLXL.generateJob (
                                    binWriter,
                                    _subsetPaperSizes[_indxPaperSizePCLXL],
                                    _subsetPaperTypes[_indxPaperTypePCLXL],
                                    _subsetOrientations[_indxOrientationPCLXL],
                                    _samplesPCLXL_RGB,
                                    _flagColourFormAsMacroPCLXL);
                            }
                            break;

                        case (Int32) eSampleType.LogOper:

                            Int32 indxMode =
                                _subsetLogOperModesPCLXL[_indxLogOperModePCLXL];

                            if (PCLXLPalettes.isMonochrome(indxMode))
                            {
                                ToolMiscSamplesActLogOperPCLXL.generateJob(
                                    binWriter,
                                    _subsetPaperSizes[_indxPaperSizePCLXL],
                                    _subsetPaperTypes[_indxPaperTypePCLXL],
                                    _subsetOrientations[_indxOrientationPCLXL],
                                    indxMode,
                                    _indxLogOperGrayD1PCLXL,
                                    _indxLogOperGrayD2PCLXL,
                                    _indxLogOperGrayS1PCLXL,
                                    _indxLogOperGrayS2PCLXL,
                                    _indxLogOperGrayT1PCLXL,
                                    _indxLogOperGrayT2PCLXL,
                                    (_indxLogOperROPFromPCLXL * _logOperROPInc),
                                    ((_indxLogOperROPToPCLXL + 1) * _logOperROPInc) - 1,
                                    _flagLogOperUseMacrosPCLXL,
                                    _flagLogOperOptSrcTextPatPCLXL);
                            }
                            else
                            {
                                ToolMiscSamplesActLogOperPCLXL.generateJob(
                                    binWriter,
                                    _subsetPaperSizes[_indxPaperSizePCLXL],
                                    _subsetPaperTypes[_indxPaperTypePCLXL],
                                    _subsetOrientations[_indxOrientationPCLXL],
                                    indxMode,
                                    _indxLogOperClrD1PCLXL,
                                    _indxLogOperClrD2PCLXL,
                                    _indxLogOperClrS1PCLXL,
                                    _indxLogOperClrS2PCLXL,
                                    _indxLogOperClrT1PCLXL,
                                    _indxLogOperClrT2PCLXL,
                                    (_indxLogOperROPFromPCLXL * _logOperROPInc),
                                    ((_indxLogOperROPToPCLXL + 1) * _logOperROPInc) - 1,
                                    _flagLogOperUseMacrosPCLXL,
                                    _flagLogOperOptSrcTextPatPCLXL);
                            }
                            
                            break;

                        case (Int32) eSampleType.LogPage:

                            // can't select this option for PCLXL
                            // so should never reach here
                            break;

                        case (Int32) eSampleType.Pattern:

                            if (_indxPatternTypePCLXL == ePatternType.Shading)
                            {
                                ToolMiscSamplesActPatternShadePCLXL.generateJob(
                                    binWriter,
                                    _subsetPaperSizes[_indxPaperSizePCLXL],
                                    _subsetPaperTypes[_indxPaperTypePCLXL],
                                    (Int32)PCLOrientations.eIndex.Portrait,
                                    _flagPatternFormAsMacroPCLXL);
                            }
                            else if (_indxPatternTypePCLXL == ePatternType.XHatch)
                            {
                                ToolMiscSamplesActPatternXHatchPCLXL.generateJob(
                                    binWriter,
                                    _subsetPaperSizes[_indxPaperSizePCLXL],
                                    _subsetPaperTypes[_indxPaperTypePCLXL],
                                    (Int32)PCLOrientations.eIndex.Portrait,
                                    _flagPatternFormAsMacroPCLXL);
                            }
                            break;

                        case (Int32) eSampleType.TxtMod:

                            if (_indxTxtModTypePCLXL == eTxtModType.Chr)
                            {
                                ToolMiscSamplesActTxtModChrPCLXL.generateJob(
                                    binWriter,
                                    _subsetPaperSizes[_indxPaperSizePCLXL],
                                    _subsetPaperTypes[_indxPaperTypePCLXL],
                                    (Int32)PCLOrientations.eIndex.Portrait,
                                    _flagPatternFormAsMacroPCLXL);
                            }
                            else if (_indxTxtModTypePCLXL == eTxtModType.Pat)
                            {
                                ToolMiscSamplesActTxtModPatPCLXL.generateJob(
                                    binWriter,
                                    _subsetPaperSizes[_indxPaperSizePCLXL],
                                    _subsetPaperTypes[_indxPaperTypePCLXL],
                                    (Int32)PCLOrientations.eIndex.Portrait,
                                    _flagTxtModFormAsMacroPCLXL);
                            }
                            else if (_indxTxtModTypePCLXL == eTxtModType.Rot)
                            {
                                ToolMiscSamplesActTxtModRotPCLXL.generateJob(
                                    binWriter,
                                    _subsetPaperSizes[_indxPaperSizePCLXL],
                                    _subsetPaperTypes[_indxPaperTypePCLXL],
                                    (Int32)PCLOrientations.eIndex.Portrait,
                                    _flagTxtModFormAsMacroPCLXL);
                            }

                            break;

                        case (Int32) eSampleType.Unicode:

                            ToolMiscSamplesActUnicodePCLXL.generateJob (
                                binWriter,
                                _subsetPaperSizes [_indxPaperSizePCLXL],
                                _subsetPaperTypes [_indxPaperTypePCLXL],
                                (Int32)PCLOrientations.eIndex.Portrait,
                                _flagUnicodeFormAsMacroPCLXL,
                                _unicodeUCS2PCLXL,
                                _subsetUnicodeFonts[_indxUnicodeFontPCLXL],
                                _unicodeFontVarPCLXL);
                            break;
                    }
                }

                TargetCore.requestStreamWrite(false);
            }

            catch (SocketException sockExc)
            {
                MessageBox.Show(sockExc.ToString(),
                                "Socket exception",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString(),
                                "Unknown exception",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }

    //      btnGenerate.BorderBrush = Brushes.Transparent;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b O r i e n t a t i o n _ S e l e c t i o n C h a n g e d        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Orientation item has changed.                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbOrientation_SelectionChanged(object sender,
                                                    SelectionChangedEventArgs e)
        {
            if (_initialised && cbOrientation.HasItems)
            {
                if (_crntPDL == ToolCommonData.ePrintLang.PCL)
                    _indxOrientationPCL   = cbOrientation.SelectedIndex;
                else
                    _indxOrientationPCLXL = cbOrientation.SelectedIndex;

                setPaperMetrics();
                setPaperMetricsLogPage();
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b P a p e r S i z e _ S e l e c t i o n C h a n g e d            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Paper Size item has changed.                                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbPaperSize_SelectionChanged(object sender,
                                                  SelectionChangedEventArgs e)
        {
            if (_initialised && cbPaperSize.HasItems)
            {
                if (_crntPDL == ToolCommonData.ePrintLang.PCL)
                    _indxPaperSizePCL   = cbPaperSize.SelectedIndex;
                else
                    _indxPaperSizePCLXL = cbPaperSize.SelectedIndex;

                setPaperMetrics();
                setPaperMetricsLogPage();
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b P a p e r T y p e _ S e l e c t i o n C h a n g e d            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Paper Type item has changed.                                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbPaperType_SelectionChanged(object sender,
                                                  SelectionChangedEventArgs e)
        {
            if (_initialised && cbPaperType.HasItems)
            {
                if (_crntPDL == ToolCommonData.ePrintLang.PCL)
                    _indxPaperTypePCL   = cbPaperType.SelectedIndex;
                else
                    _indxPaperTypePCLXL = cbPaperType.SelectedIndex;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b P D L _ S e l e c t i o n C h a n g e d                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Print Language item has changed.                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbPDL_SelectionChanged(object sender,
                                            SelectionChangedEventArgs e)
        {
            if (_initialised)
            {
                pdlOptionsStore();

                _indxPDL = cbPDL.SelectedIndex;
                _crntPDL = (ToolCommonData.ePrintLang) _subsetPDLs[_indxPDL];

                pdlOptionsRestore();

                initialiseData (false);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b S a m p l e T y p e _ S e l e c t i o n C h a n g e d          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Sample type item has changed.                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbSampleType_SelectionChanged (object sender,
                                                    SelectionChangedEventArgs e)
        {
            if (_initialised && cbSampleType.HasItems)
            {
                _indxSampleType = cbSampleType.SelectedIndex;

                initialiseData (true);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k O p t F o r m A s M a c r o _ C h e c k e d                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Option 'Render fixed text as overlay' checked.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkOptFormAsMacro_Checked (object sender,
                                                RoutedEventArgs e)
        {
            switch (_indxSampleType)
            {
                case (Int32) eSampleType.Colour:

                    setFlagColourFormAsMacro (true, _crntPDL);
                    break;

                case (Int32) eSampleType.LogOper:

                    setFlagLogOperFormAsMacro (true, _crntPDL);
                    break;

                case (Int32) eSampleType.LogPage:

                    setFlagLogPageFormAsMacro (true, _crntPDL);
                    break;

                case (Int32) eSampleType.Pattern:

                    setFlagPatternFormAsMacro (true, _crntPDL);
                    break;

                case (Int32) eSampleType.TxtMod:

                    setFlagTxtModFormAsMacro (true, _crntPDL);
                    break;

                case (Int32) eSampleType.Unicode:

                    setFlagUnicodeFormAsMacro (true, _crntPDL);
                    break;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k O p t F o r m A s M a c r o _ U n c h e c k e d              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Option 'Render fixed text as overlay' unchecked.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkOptFormAsMacro_Unchecked (object sender,
                                                  RoutedEventArgs e)
        {
            switch (_indxSampleType)
            {
                case (Int32) eSampleType.Colour:

                    setFlagColourFormAsMacro (false, _crntPDL);
                    break;

                case (Int32) eSampleType.LogOper:

                    setFlagLogOperFormAsMacro (false, _crntPDL);
                    break;

                case (Int32) eSampleType.LogPage:

                    setFlagLogPageFormAsMacro (false, _crntPDL);
                    break;

                case (Int32) eSampleType.Pattern:

                    setFlagPatternFormAsMacro (false, _crntPDL);
                    break;

                case (Int32) eSampleType.TxtMod:

                    setFlagTxtModFormAsMacro (false, _crntPDL);
                    break;

                case (Int32) eSampleType.Unicode:

                    setFlagUnicodeFormAsMacro (false, _crntPDL);
                    break;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g i v e C r n t P D L                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void giveCrntPDL (ref ToolCommonData.ePrintLang crntPDL)
        {
            crntPDL = _crntPDL;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g i v e C r n t T y p e                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void giveCrntType (ref ToolCommonData.eToolSubIds crntType)
        {
            crntType =
                (ToolCommonData.eToolSubIds)_subsetSampleTypes[_indxSampleType];
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n i t i a l i s e                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initialisation.                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void initialise()
        {
            Int32 index;
                
            //----------------------------------------------------------------//
            //                                                                //
            // Populate form.                                                 //
            //                                                                //
            //----------------------------------------------------------------//

            cbPDL.Items.Clear();

            _ctPDLs = _subsetPDLs.Length;

            for (Int32 i = 0; i < _ctPDLs; i++)
            {
            //  index = _subsetPDLs[i];

                cbPDL.Items.Add(Enum.GetName(
                    typeof(ToolCommonData.ePrintLang), i));
            }

            //----------------------------------------------------------------//

            cbOrientation.Items.Clear();

            _ctOrientations = _subsetOrientations.Length;

            for (Int32 i = 0; i < _ctOrientations; i++)
            {
                index = _subsetOrientations[i];

                cbOrientation.Items.Add(PCLOrientations.getName(index));
            }

            //----------------------------------------------------------------//

            cbPaperSize.Items.Clear();

            _ctPaperSizes = _subsetPaperSizes.Length;

            for (Int32 i = 0; i < _ctPaperSizes; i++)
            {
                index = _subsetPaperSizes[i];

                cbPaperSize.Items.Add(PCLPaperSizes.getName(index));
            }

            //----------------------------------------------------------------//

            cbPaperType.Items.Clear();

            _ctPaperTypes = _subsetPaperTypes.Length;

            for (Int32 i = 0; i < _ctPaperTypes; i++)
            {
                index = _subsetPaperTypes[i];

                cbPaperType.Items.Add(PCLPaperTypes.getName(index));
            }

            //----------------------------------------------------------------//

            _ctSampleTypes = _subsetSampleTypes.Length;

            cbSampleType.Items.Clear();

            _ctSampleTypes = _subsetSampleTypes.Length;

            for (Int32 i = 0; i < _ctSampleTypes; i++)
            {
                cbSampleType.Items.Add(sSampleNames [i]);
            }

            //----------------------------------------------------------------//

            resetTarget ();

            //----------------------------------------------------------------//
            //                                                                //
            // Reinstate settings from persistent storage.                    //
            //                                                                //
            //----------------------------------------------------------------//

            metricsLoad();

            pdlOptionsRestore();

            cbPDL.SelectedIndex = (Byte)_indxPDL;

            initialiseData (true);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n i t i a l i s e D a t a                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initialise data for selected sample type.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void initialiseData (Boolean typeChange)
        {
            setPaperMetrics ();

            //----------------------------------------------------------------//

            btnGenerate.IsEnabled = true;

            chkOptFormAsMacro.Visibility = Visibility.Visible;
            chkOptFormAsMacro.Content = "Render fixed text as overlay";

            if (_indxSampleType == (Int32) eSampleType.Colour)
            {
                initialiseDataColour ();

                cbSampleType.SelectedIndex = (Int32) eSampleType.Colour;

                tabSampleType.SelectedItem = tabColour;
            }
            else if (_indxSampleType == (Int32) eSampleType.LogOper)
            {
                if (_crntPDL == ToolCommonData.ePrintLang.PCL)
                    chkOptFormAsMacro.Content = "Use macros";
                else if (_crntPDL == ToolCommonData.ePrintLang.PCLXL)
                    chkOptFormAsMacro.Content = "Use user-defined streams";

                initialiseDataLogOper (typeChange);

                cbSampleType.SelectedIndex = (Int32) eSampleType.LogOper;

                tabSampleType.SelectedItem = tabLogOper;
            }
            else if (_indxSampleType == (Int32) eSampleType.LogPage)
            {
                if (_crntPDL == ToolCommonData.ePrintLang.PCLXL)
                    chkOptFormAsMacro.Visibility = Visibility.Hidden; 
                
                initialiseDataLogPage ();

                cbSampleType.SelectedIndex = (Int32) eSampleType.LogPage;

                tabSampleType.SelectedItem = tabLogPage;
            }
            else if (_indxSampleType == (Int32) eSampleType.Pattern)
            {
                initialiseDataPattern ();

                cbSampleType.SelectedIndex = (Int32) eSampleType.Pattern;

                tabSampleType.SelectedItem = tabPattern;
            }
            else if (_indxSampleType == (Int32) eSampleType.TxtMod)
            {
                initialiseDataTxtMod ();

                cbSampleType.SelectedIndex = (Int32) eSampleType.TxtMod;

                tabSampleType.SelectedItem = tabTxtMod;
            }
            else if (_indxSampleType == (Int32) eSampleType.Unicode)
            {
                initialiseDataUnicode ();

                cbSampleType.SelectedIndex = (Int32) eSampleType.Unicode;

                tabSampleType.SelectedItem = tabUnicode;
            }
            else
            {
                _indxSampleType = (Int32) eSampleType.TxtMod;

                initialiseDataTxtMod ();

                cbSampleType.SelectedIndex = (Int32) eSampleType.TxtMod;

                tabSampleType.SelectedItem = tabTxtMod;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m e t r i c s L o a d                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Load metrics from persistent storage.                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void metricsLoad()
        {
            ToolMiscSamplesPersist.loadDataCommon(ref _indxPDL,
                                                  ref _indxSampleType);

            ToolMiscSamplesPersist.loadDataCommonPDL (
                "PCL",
                ref _indxOrientationPCL,
                ref _indxPaperSizePCL,
                ref _indxPaperTypePCL);

            ToolMiscSamplesPersist.loadDataCommonPDL (
                "PCLXL",
                ref _indxOrientationPCLXL,
                ref _indxPaperSizePCLXL,
                ref _indxPaperTypePCLXL);

            //----------------------------------------------------------------//

            if ((_indxPDL < 0) || (_indxPDL >= _ctPDLs))
                _indxPDL = 0;
            
            _crntPDL = (ToolCommonData.ePrintLang) _subsetPDLs[_indxPDL];

            //----------------------------------------------------------------//

            if ((_indxSampleType < 0) || (_indxSampleType >= _ctSampleTypes))
                _indxSampleType = (Int32) eSampleType.TxtMod;

            //----------------------------------------------------------------//

            metricsLoadDataColour ();
            metricsLoadDataLogOper ();
            metricsLoadDataLogPage ();
            metricsLoadDataPattern();
            metricsLoadDataTxtMod();
            metricsLoadDataUnicode();
            
            //----------------------------------------------------------------//

            if ((_indxOrientationPCL < 0) ||
                (_indxOrientationPCL >= _ctOrientations))
                _indxOrientationPCL = 0;

            if ((_indxPaperSizePCL < 0) ||
                (_indxPaperSizePCL >= _ctPaperSizes))
                _indxPaperSizePCL = 0;

            if ((_indxPaperTypePCL < 0) ||
                (_indxPaperTypePCL >= _ctPaperTypes))
                _indxPaperTypePCL = 0;

            //----------------------------------------------------------------//

            if ((_indxOrientationPCLXL < 0) ||
                (_indxOrientationPCLXL >= _ctOrientations))
                _indxOrientationPCLXL = 0;

            if ((_indxPaperSizePCLXL < 0) ||
                (_indxPaperSizePCLXL >= _ctPaperSizes))
                _indxPaperSizePCLXL = 0;

            if ((_indxPaperTypePCLXL < 0) ||
                (_indxPaperTypePCLXL >= _ctPaperTypes))
                _indxPaperTypePCLXL = 0;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m e t r i c s S a v e                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Save current metrics to persistent storage.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void metricsSave()
        {
            pdlOptionsStore();

            ToolMiscSamplesPersist.saveDataCommon(_indxPDL,
                                                  (Int32)_indxSampleType);

            ToolMiscSamplesPersist.saveDataCommonPDL (
                "PCL",
                _indxOrientationPCL,
                _indxPaperSizePCL,
                _indxPaperTypePCL);

            ToolMiscSamplesPersist.saveDataCommonPDL (
                "PCLXL",
                _indxOrientationPCLXL,
                _indxPaperSizePCLXL,
                _indxPaperTypePCLXL);

            metricsSaveDataColour();
            metricsSaveDataLogOper();
            metricsSaveDataLogPage();
            metricsSaveDataPattern();
            metricsSaveDataTxtMod();
            metricsSaveDataUnicode();
       }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p d l O p t i o n s R e s t o r e                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Restore the test metrics options for the current PDL.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void pdlOptionsRestore()
        {
            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                cbOrientation.SelectedIndex  = _indxOrientationPCL;
                cbPaperSize.SelectedIndex    = _indxPaperSizePCL;
                cbPaperType.SelectedIndex    = _indxPaperTypePCL;
            }
            else
            {
                cbOrientation.SelectedIndex  = _indxOrientationPCLXL;
                cbPaperSize.SelectedIndex    = _indxPaperSizePCLXL;
                cbPaperType.SelectedIndex    = _indxPaperTypePCLXL;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p d l O p t i o n s S t o r e                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store the test metrics options for the current PDL.                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void pdlOptionsStore()
        {
            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                _indxOrientationPCL = cbOrientation.SelectedIndex;
                _indxPaperSizePCL   = cbPaperSize.SelectedIndex;
                _indxPaperTypePCL   = cbPaperType.SelectedIndex;
            }
            else
            {
                _indxOrientationPCLXL = cbOrientation.SelectedIndex;
                _indxPaperSizePCLXL   = cbPaperSize.SelectedIndex;
                _indxPaperTypePCLXL   = cbPaperType.SelectedIndex;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e s e t T a r g e t                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Reset the text on the 'Generate' button.                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void resetTarget()
        {
            TargetCore.eTarget targetType = TargetCore.getType ();

            if (targetType == TargetCore.eTarget.File)
            {
                btnGenerate.Content = "Generate & send test data to file";
            }
            else if (targetType == TargetCore.eTarget.NetPrinter)
            {
                String netPrnAddress = "";
                Int32  netPrnPort = 0;

                Int32 netTimeoutSend = 0;
                Int32 netTimeoutReceive = 0;

                TargetCore.metricsLoadNetPrinter(ref netPrnAddress,
                                                  ref netPrnPort,
                                                  ref netTimeoutSend,
                                                  ref netTimeoutReceive);

                btnGenerate.Content = "Generate & send test data to " +
                                      "\r\n" +
                                      netPrnAddress + " : " +
                                      netPrnPort.ToString ();
            }
            else if (targetType == TargetCore.eTarget.WinPrinter)
            {
                String winPrintername = "";

                TargetCore.metricsLoadWinPrinter (ref winPrintername);

                btnGenerate.Content = "Generate & send test data to printer " +
                                      "\r\n" +
                                      winPrintername;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t P a p e r M e t r i c s                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set the contents of the Paper metrics fields.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void setPaperMetrics()
        {
            PCLOrientations.eAspect aspect;

            Int32 indxOrientation,
                  indxPaperSize,
                  indxPaperType;

            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                indxOrientation = _subsetOrientations[_indxOrientationPCL];
                indxPaperSize = _subsetPaperSizes[_indxPaperSizePCL];
                indxPaperType = _indxPaperTypePCL;
            }
            else
            {
                indxOrientation = _subsetOrientations[_indxOrientationPCLXL];
                indxPaperSize = _subsetPaperSizes[_indxPaperSizePCLXL];
                indxPaperType = _indxPaperTypePCLXL;
            }

            aspect = PCLOrientations.getAspect(indxOrientation);

            _paperSizeLongEdge =
                PCLPaperSizes.getSizeLongEdge(indxPaperSize,
                                              _unitsPerInch);

            _paperSizeShortEdge =
                PCLPaperSizes.getSizeShortEdge(indxPaperSize,
                                               _unitsPerInch);

            _paperMarginsUnprintable =
                PCLPaperSizes.getMarginsUnprintable(indxPaperSize,
                                                    _unitsPerInch);

            _paperMarginsLogicalLand =
                PCLPaperSizes.getMarginsLogicalLand(indxPaperSize,
                                                    _unitsPerInch);

            _paperMarginsLogicalPort =
                PCLPaperSizes.getMarginsLogicalPort(indxPaperSize,
                                                    _unitsPerInch);

            if (aspect == PCLOrientations.eAspect.Portrait)
            {
                _paperMarginsLogicalLeft = _paperMarginsLogicalPort;
                _paperMarginsLogicalTop  = 0;

                _paperWidthPhysical =
                    (UInt16)(_paperSizeShortEdge);

                _paperLengthPhysical =
                    (UInt16)(_paperSizeLongEdge);

                _paperWidthPrintable =
                    (UInt16)(_paperSizeShortEdge -
                             (_paperMarginsUnprintable * 2));

                _paperLengthPrintable =
                    (UInt16)(_paperSizeLongEdge -
                             (_paperMarginsUnprintable * 2));

                _paperWidthLogical =
                    (UInt16)(_paperSizeShortEdge -
                             (_paperMarginsLogicalPort * 2));

                _paperLengthLogical =
                    (UInt16)(_paperSizeLongEdge);
            }
            else
            {
                _paperMarginsLogicalLeft = _paperMarginsLogicalLand;
                _paperMarginsLogicalTop = 0;

                _paperWidthPhysical =
                    (UInt16)(_paperSizeLongEdge);

                _paperLengthPhysical =
                    (UInt16)(_paperSizeShortEdge);

                _paperWidthPrintable =
                    (UInt16)(_paperSizeLongEdge -
                             (_paperMarginsUnprintable * 2));

                _paperLengthPrintable =
                    (UInt16)(_paperSizeShortEdge -
                             (_paperMarginsUnprintable * 2));

                _paperWidthLogical =
                    (UInt16)(_paperSizeLongEdge -
                             (_paperMarginsLogicalLand * 2));

                _paperLengthLogical =
                    (UInt16)(_paperSizeShortEdge);
            }
        }
     }
}
