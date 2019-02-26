using System;
using System.Windows;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// Interaction logic for ToolMiscSamples.xaml
    /// 
    /// Class handles the MiscSamples: Define Logical Page tool form.
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

        const Int32 _defLogPageOffLeftDPt = 170;
        const Int32 _defLogPageOffTopDPt  = 0;

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Fields (class variables).                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Int32 _logPageOffLeftDPt;
        private Int32 _logPageOffTopDPt;

        private Int32 _logPageWidthDPt;
        private Int32 _logPageHeightDPt;

        private Boolean _flagLogPageOptStdPagePCL;

        private Boolean _flagLogPageFormAsMacroPCL;

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b t n G e n e r a t e _ C l i c k                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Reset logical page' button is clicked.            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void btnLogPageReset_Click(object sender, EventArgs e)
        {
            setPaperMetricsLogPage();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k L o g P a g e O p t S t d P a g e _ C h e c k e d            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Option 'Add standard page' checked.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkLogPageOptStdPage_Checked(object sender,
                                                RoutedEventArgs e)
        {
            _flagLogPageOptStdPagePCL = true;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k L o g P a g e O p t S t d P a g e _ U n c h e c k e d        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Option 'Add standard page' unchecked.                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkLogPageOptStdPage_Unchecked(object sender,
                                                RoutedEventArgs e)
        {
            _flagLogPageOptStdPagePCL = false;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n i t i a l i s e D a t a L o g P a g e                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initialise 'Define logical page' data.                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void initialiseDataLogPage ()
        {
            lbOrientation.Visibility = Visibility.Visible;
            cbOrientation.Visibility = Visibility.Visible;

            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                txtLogPageDesc.Text =
                    "The standard PCL logical page is smaller than the" +
                    " physical page of the selected page size.\r\n" +
                    "Some LaserJet printers support a 'Define Logical Page'" +
                    " escape sequence via which the size and position of the" +
                    " logical page can be specified (although this doesn't" +
                    " mean that marks can be made within the unprintable" +
                    " regions of the physical page).";

                setPaperMetricsLogPage();

                grpLogPagePhysical.Visibility = Visibility.Visible;
                grpLogPageLogical.Visibility = Visibility.Visible;
                grpLogPageOptions.Visibility = Visibility.Visible;

                if (_flagLogPageFormAsMacroPCL)
                    chkOptFormAsMacro.IsChecked = true;
                else
                    chkOptFormAsMacro.IsChecked = false;
            }
            else
            {
                txtLogPageDesc.Text =
                    "PCL XL can address all points on the physical page" +
                    " (although there are still unprintable regions all" +
                    " around the page), so there is no need for the" +
                    " equivalent of the PCL logical page.";

                grpLogPagePhysical.Visibility = Visibility.Hidden;
                grpLogPageLogical.Visibility = Visibility.Hidden;
                grpLogPageOptions.Visibility = Visibility.Hidden;

                btnGenerate.IsEnabled = false;

                chkOptFormAsMacro.IsChecked = false;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m e t r i c s L o a d D a t a L o g P a g e                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Load current metrics from persistent storage.                      //
        // Only relevant to PCL.                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void metricsLoadDataLogPage()
        {
            ToolMiscSamplesPersist.loadDataTypeLogPage (
                "PCL",
                ref _logPageOffLeftDPt,
                ref _logPageOffTopDPt,
                ref _logPageHeightDPt,
                ref _logPageWidthDPt,
                ref _flagLogPageFormAsMacroPCL,
                ref _flagLogPageOptStdPagePCL);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m e t r i c s S a v e D a t a L o g P a g e                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Save current metrics to persistent storage.                        //
        // Only relevant to PCL.                                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void metricsSaveDataLogPage()
        {
            ToolMiscSamplesPersist.saveDataTypeLogPage("PCL",
                                             _logPageOffLeftDPt,
                                             _logPageOffTopDPt,
                                             _logPageHeightDPt,
                                             _logPageWidthDPt,
                                             _flagLogPageFormAsMacroPCL,
                                             _flagLogPageOptStdPagePCL);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t F l a g L o g P a g e F o r m A s M a c r o                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set or unset 'Render fixed text as overlay' flag.                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void setFlagLogPageFormAsMacro (
            Boolean setFlag,
            ToolCommonData.ePrintLang crntPDL)
        {
            if (crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                if (setFlag)
                    _flagLogPageFormAsMacroPCL = true;
                else
                    _flagLogPageFormAsMacroPCL = false;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t P a p e r M e t r i c s L o g P a g e                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set the contents of the Paper metrics fields.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void setPaperMetricsLogPage()
        {
            //----------------------------------------------------------------//
            //                                                                //
            // Display the physical paper data.                               //
            //                                                                //
            //----------------------------------------------------------------//

            txtLogPagePaperWidthDPt.Text =
                (_paperWidthPhysical * _unitsToDPts).ToString("F0");
            txtLogPagePaperHeightDPt.Text =
                (_paperLengthPhysical * _unitsToDPts).ToString("F0");

            txtLogPagePaperWidthMet.Text
                = (_paperWidthPhysical * _unitsToMilliMetres).ToString("F1");
            txtLogPagePaperHeightMet.Text
                = (_paperLengthPhysical * _unitsToMilliMetres).ToString("F1");

            txtLogPagePaperWidthImp.Text
                = (_paperWidthPhysical * _unitsToInches).ToString("F2");
            txtLogPagePaperHeightImp.Text
                = (_paperLengthPhysical * _unitsToInches).ToString("F2");

            txtLogPageUnprintableDPt.Text =
                (_paperMarginsUnprintable * _unitsToDPts).ToString("F0");
            txtLogPageUnprintableMet.Text =
                (_paperMarginsUnprintable * _unitsToMilliMetres).ToString("F1");
            txtLogPageUnprintableImp.Text =
                (_paperMarginsUnprintable * _unitsToInches).ToString("F2");

            //----------------------------------------------------------------//
            //                                                                //
            // Display the (default) logical page offset values appropriate   //
            // to the selected paper size.                                    //
            //                                                                //
            //----------------------------------------------------------------//

            txtLogPageOffLeftDPt.Text =
                (_paperMarginsLogicalLeft * _unitsToDPts).ToString("F0");

            txtLogPageOffTopDPt.Text =
                (_paperMarginsLogicalTop * _unitsToDPts).ToString("F0");

            txtLogPageOffLeftMet.Text =
                (_paperMarginsLogicalLeft * _unitsToMilliMetres).ToString("F1");

            txtLogPageOffTopMet.Text =
                (_paperMarginsLogicalTop * _unitsToMilliMetres).ToString("F1");

            txtLogPageOffLeftImp.Text =
                (_paperMarginsLogicalLeft * _unitsToInches).ToString("F2");

            txtLogPageOffTopImp.Text =
                (_paperMarginsLogicalTop * _unitsToInches).ToString("F2");

            //----------------------------------------------------------------//
            //                                                                //
            // Display the (default) logical page size values appropriate     //
            // to the selected paper size.                                    //
            //                                                                //
            //----------------------------------------------------------------//

            txtLogPageHeightDPt.Text =
                (_paperLengthLogical * _unitsToDPts).ToString("F0");

            txtLogPageWidthDPt.Text =
                (_paperWidthLogical * _unitsToDPts).ToString("F0");

            txtLogPageHeightMet.Text =
                (_paperLengthLogical * _unitsToMilliMetres).ToString("F1");

            txtLogPageWidthMet.Text =
                (_paperWidthLogical * _unitsToMilliMetres).ToString("F1");

            txtLogPageHeightImp.Text =
                (_paperLengthLogical * _unitsToInches).ToString("F2");

            txtLogPageWidthImp.Text =
                (_paperWidthLogical * _unitsToInches).ToString("F2");
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t L o g P a g e O f f L e f t D P t _ L o s t F o c u s        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Logical Offset Left DeciPoints item has lost focus.                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtLogPageOffLeftDPt_LostFocus(object sender,
                                             RoutedEventArgs e)
        {
            if (validateLogPageOffLeftDPt(true))
            {
                txtLogPageOffLeftMet.Text =
                    (_logPageOffLeftDPt * _dPtsToMilliMetres).ToString("F1");

                txtLogPageOffLeftImp.Text =
                    (_logPageOffLeftDPt * _dPtsToInches).ToString("F2");
            }
            else
            {
                txtLogPageOffLeftMet.Text = "";
                txtLogPageOffLeftImp.Text = "";
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t L o g P a g e O f f L e f t D P t _ T e x t C h a n g e d    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Logical Offset Left DeciPoints item has changed.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtLogPageOffLeftDPt_TextChanged(object sender,
                                               TextChangedEventArgs e)
        {
            if (validateLogPageOffLeftDPt(false))
            {
                txtLogPageOffLeftMet.Text =
                    (_logPageOffLeftDPt * _dPtsToMilliMetres).ToString("F1");

                txtLogPageOffLeftImp.Text =
                    (_logPageOffLeftDPt * _dPtsToInches).ToString("F2");
            }
            else
            {
                txtLogPageOffLeftMet.Text = "";
                txtLogPageOffLeftImp.Text = "";
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t L o g P a g e O f f T o p D P t _ L o s t F o c u s          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Logical Offset Top DeciPoints item has lost focus.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtLogPageOffTopDPt_LostFocus(object sender,
                                             RoutedEventArgs e)
        {
            if (validateLogPageOffTopDPt(true))
            {
                txtLogPageOffTopMet.Text =
                    (_logPageOffTopDPt * _dPtsToMilliMetres).ToString("F1");

                txtLogPageOffTopImp.Text =
                    (_logPageOffTopDPt * _dPtsToInches).ToString("F2");
            }
            else
            {
                txtLogPageOffTopMet.Text = "";
                txtLogPageOffTopImp.Text = "";
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t L o g P a g e O f f T o p D P t _ T e x t C h a n g e d      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Logical Offset Top DeciPoints item has changed.                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtLogPageOffTopDPt_TextChanged(object sender,
                                               TextChangedEventArgs e)
        {
            if (validateLogPageOffTopDPt(false))
            {
                txtLogPageOffTopMet.Text =
                    (_logPageOffTopDPt * _dPtsToMilliMetres).ToString("F1");

                txtLogPageOffTopImp.Text =
                    (_logPageOffTopDPt * _dPtsToInches).ToString("F2");
            }
            else
            {
                txtLogPageOffTopMet.Text = "";
                txtLogPageOffTopImp.Text = "";
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t L o g P a g e H e i g h t D P t _ L o s t F o c u s          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Logical Page Height DeciPoints item has lost focus.                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtLogPageHeightDPt_LostFocus(object sender,
                                             RoutedEventArgs e)
        {
            if (validateLogPageHeightDPt(true))
            {
                txtLogPageHeightMet.Text =
                    (_logPageHeightDPt * _dPtsToMilliMetres).ToString("F1");

                txtLogPageHeightImp.Text =
                    (_logPageHeightDPt * _dPtsToInches).ToString("F2");
            }
            else
            {
                txtLogPageHeightMet.Text = "";
                txtLogPageHeightImp.Text = "";
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t L o g P a g e H e i g h t D P t _ T e x t C h a n g e d      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Logical Page Height DeciPoints item has changed.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtLogPageHeightDPt_TextChanged(object sender,
                                               TextChangedEventArgs e)
        {
            if (validateLogPageHeightDPt(false))
            {
                txtLogPageHeightMet.Text =
                    (_logPageHeightDPt * _dPtsToMilliMetres).ToString("F1");

                txtLogPageHeightImp.Text =
                    (_logPageHeightDPt * _dPtsToInches).ToString("F2");
            }
            else
            {
                txtLogPageHeightMet.Text = "";
                txtLogPageHeightImp.Text = "";
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t L o g P a g e W i d t h D P t _ L o s t F o c u s            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Logical Page Width DeciPoints item has lost focus.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtLogPageWidthDPt_LostFocus(object sender,
                                             RoutedEventArgs e)
        {
            if (validateLogPageWidthDPt(true))
            {
                txtLogPageWidthMet.Text =
                    (_logPageWidthDPt * _dPtsToMilliMetres).ToString("F1");

                txtLogPageWidthImp.Text =
                    (_logPageWidthDPt * _dPtsToInches).ToString("F2");
            }
            else
            {
                txtLogPageWidthMet.Text = "";
                txtLogPageWidthImp.Text = "";
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t L o g P a g e W i d t h D P t _ T e x t C h a n g e d        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Logical Page Width DeciPoints item has changed.                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtLogPageWidthDPt_TextChanged(object sender,
                                               TextChangedEventArgs e)
        {
            if (validateLogPageWidthDPt(false))
            {
                txtLogPageWidthMet.Text =
                    (_logPageWidthDPt * _dPtsToMilliMetres).ToString("F1");

                txtLogPageWidthImp.Text =
                    (_logPageWidthDPt * _dPtsToInches).ToString("F2");
            }
            else
            {
                txtLogPageWidthMet.Text = "";
                txtLogPageWidthImp.Text = "";
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // v a l i d a t e L o g P a g e O f f L e f t D P t                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Validate logical offset left decipoints value.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean validateLogPageOffLeftDPt(Boolean lostFocusEvent)
        {
            const Int32 minVal = -32767;
            const Int32 maxVal = 32767;
            const Int32 defVal = _defLogPageOffLeftDPt;

            Int32 value = 0;

            Boolean OK = true;

            String crntText = txtLogPageOffLeftDPt.Text;

            if (crntText == "")
            {
                value = 0;
            }
            else
            {
                OK = Int32.TryParse(crntText, out value);

                if ((value < minVal) || (value > maxVal))
                    OK = false;
            }

            if (OK)
            {
                _logPageOffLeftDPt = value;
            }
            else
            {
                if (lostFocusEvent)
                {
                    String newText = defVal.ToString("F2");

                    MessageBox.Show("Left offset value '" + crntText +
                                    "' is invalid.\n\n" +
                                    "Value will be reset to default '" +
                                    newText + "'",
                                    "Logical page attribute invalid",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);

                    _logPageOffLeftDPt = defVal;

                    txtLogPageOffLeftDPt.Text = newText;
                }
                else
                {
                    MessageBox.Show("Left offset value '" + crntText +
                                    "' is invalid.\n\n" +
                                    "Valid range is :\n\t" +
                                    minVal + " <= value <= " + maxVal,
                                    "Logical Page attribute invalid",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);

                    txtLogPageOffLeftDPt.Focus();
                    txtLogPageOffLeftDPt.SelectAll();
                }
            }

            return OK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // v a l i d a t e L o g P a g e O f f T o p D P t                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Validate logical offset top decipoints value.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean validateLogPageOffTopDPt(Boolean lostFocusEvent)
        {
            const Int32 minVal = -32767;
            const Int32 maxVal = 32767;
            const Int32 defVal = _defLogPageOffTopDPt;

            Int32 value = 0;

            Boolean OK = true;

            String crntText = txtLogPageOffTopDPt.Text;

            if (crntText == "")
            {
                value = 0;
            }
            else
            {
                OK = Int32.TryParse(crntText, out value);

                if ((value < minVal) || (value > maxVal))
                    OK = false;
            }

            if (OK)
            {
                _logPageOffTopDPt = value;
            }
            else
            {
                if (lostFocusEvent)
                {
                    String newText = defVal.ToString("F2");

                    MessageBox.Show("Top offset value '" + crntText +
                                    "' is invalid.\n\n" +
                                    "Value will be reset to default '" +
                                    newText + "'",
                                    "Logical page attribute invalid",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);

                    _logPageOffTopDPt = defVal;

                    txtLogPageOffTopDPt.Text = newText;
                }
                else
                {
                    MessageBox.Show("Top offset value '" + crntText +
                                    "' is invalid.\n\n" +
                                    "Valid range is :\n\t" +
                                    minVal + " <= value <= " + maxVal,
                                    "Logical Page attribute invalid",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);

                    txtLogPageOffTopDPt.Focus();
                    txtLogPageOffTopDPt.SelectAll();
                }
            }

            return OK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // v a l i d a t e L o g P a g e H e i g h t D P t                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Validate logical page height value.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean validateLogPageHeightDPt(Boolean lostFocusEvent)
        {
            const Int32 minVal = 1;
            const Int32 maxVal = 65535;
            Int32 defVal = (Int32) (_paperLengthLogical * _unitsToDPts);

            Int32 value = 0;

            Boolean OK = true;

            String crntText = txtLogPageHeightDPt.Text;

            if (crntText == "")
            {
                value = 0;
            }
            else
            {
                OK = Int32.TryParse(crntText, out value);

                if ((value < minVal) || (value > maxVal))
                    OK = false;
            }

            if (OK)
            {
                _logPageHeightDPt = value;
            }
            else
            {
                if (lostFocusEvent)
                {
                    String newText = defVal.ToString("F2");

                    MessageBox.Show("Logical page height value '" + crntText +
                                    "' is invalid.\n\n" +
                                    "Value will be reset to default '" +
                                    newText + "'",
                                    "Logical page attribute invalid",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);

                    _logPageHeightDPt = defVal;

                    txtLogPageHeightDPt.Text = newText;
                }
                else
                {
                    MessageBox.Show("Logical page height value '" + crntText +
                                    "' is invalid.\n\n" +
                                    "Valid range is :\n\t" +
                                    minVal + " <= value <= " + maxVal,
                                    "Logical Page attribute invalid",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);

                    txtLogPageHeightDPt.Focus();
                    txtLogPageHeightDPt.SelectAll();
                }
            }

            return OK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // v a l i d a t e L o g P a g e W i d t h D P t                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Validate logical page height value.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean validateLogPageWidthDPt(Boolean lostFocusEvent)
        {
            const Int32 minVal = 1;
            const Int32 maxVal = 65535;
            Int32 defVal = (Int32) (_paperWidthLogical * _unitsToDPts);

            Int32 value = 0;

            Boolean OK = true;

            String crntText = txtLogPageWidthDPt.Text;

            if (crntText == "")
            {
                value = 0;
            }
            else
            {
                OK = Int32.TryParse(crntText, out value);

                if ((value < minVal) || (value > maxVal))
                    OK = false;
            }

            if (OK)
            {
                _logPageWidthDPt = value;
            }
            else
            {
                if (lostFocusEvent)
                {
                    String newText = defVal.ToString("F2");

                    MessageBox.Show("Logical page width value '" + crntText +
                                    "' is invalid.\n\n" +
                                    "Value will be reset to default '" +
                                    newText + "'",
                                    "Logical page attribute invalid",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);

                    _logPageWidthDPt = defVal;

                    txtLogPageWidthDPt.Text = newText;
                }
                else
                {
                    MessageBox.Show("Logical page width value '" + crntText +
                                    "' is invalid.\n\n" +
                                    "Valid range is :\n\t" +
                                    minVal + " <= value <= " + maxVal,
                                    "Logical Page attribute invalid",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);

                    txtLogPageWidthDPt.Focus();
                    txtLogPageWidthDPt.SelectAll();
                }
            }

            return OK;
        }
    }
}
