using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// Interaction logic for TargetRptFile.xaml
    /// 
    /// Class handles the Target (report file) definition form.
    /// 
    /// © Chris Hutchinson 2017
    /// 
    /// </summary>
    
    [System.Reflection.ObfuscationAttribute(Feature = "renaming",
                                            ApplyToMembers = true)]

    public partial class TargetRptFile : Window
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Fields (class variables).                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private ToolCommonData.eToolIds    _crntToolId;
        private ToolCommonData.eToolSubIds _crntSubId;
        private ToolCommonData.ePrintLang  _crntPDL;
        private ReportCore.eRptFileFmt     _rptFileFmt;
        private ReportCore.eRptChkMarks    _rptChkMarks;

        private Boolean _flagOptRptWrap;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // T a r g e t R p t F i l e                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public TargetRptFile (ToolCommonData.eToolIds    crntToolId,
                              ToolCommonData.eToolSubIds crntSubId,
                              ToolCommonData.ePrintLang  crntPDL)
        {
            InitializeComponent();

            _crntToolId = crntToolId;
            _crntSubId  = crntSubId;
            _crntPDL    = crntPDL;

            initialise ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b t n C a n c e l _ C l i c k                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Cancel' button is clicked.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b t n O K _ C l i c k                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'OK' button is clicked.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            metricsSave ();

            this.DialogResult = true;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k R p t O p t W r a p _ C h e c k e d                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the Report file 'wrap' checkbox is checked.            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkRptOptWrap_Checked (object sender,
                                            RoutedEventArgs e)
        {
            _flagOptRptWrap = true;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k R p t O p t W r a p _ U n c h e c k e d                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the Report file 'wrap' checkbox is unchecked.          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkRptOptWrap_Unchecked (object sender,
                                              RoutedEventArgs e)
        {
            _flagOptRptWrap = false;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n i t i a l i s e                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initialise 'target' data.                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void initialise ()
        {
            btnOK.Visibility = Visibility.Hidden;

            //----------------------------------------------------------------//
            //                                                                //
            // Tool and PDL identifiers.                                      //
            //                                                                //
            //----------------------------------------------------------------//

            if (_crntSubId == ToolCommonData.eToolSubIds.None)
            {
                txtCrntTool.Text =
                    Enum.GetName (typeof (ToolCommonData.eToolIds),
                                  _crntToolId);
            }
            else
            {
                txtCrntTool.Text =
                    Enum.GetName (typeof (ToolCommonData.eToolIds),
                                  _crntToolId) +
                    "|" +
                    Enum.GetName (typeof (ToolCommonData.eToolSubIds),
                                  _crntSubId);
            }

            txtCrntPDL.Text  = _crntPDL.ToString ();

            //----------------------------------------------------------------//
            //                                                                //
            // Report file data.                                              //
            //                                                                //
            //----------------------------------------------------------------//

            if ((_crntToolId == ToolCommonData.eToolIds.MakeOverlay)
                                          ||
                (_crntToolId == ToolCommonData.eToolIds.PrintLang)
                                          ||
                (_crntToolId == ToolCommonData.eToolIds.PrnAnalyse)
                                          ||
                (_crntToolId == ToolCommonData.eToolIds.SoftFontGenerate)
                                          ||
                (_crntToolId == ToolCommonData.eToolIds.StatusReadback)
                                          ||
                (_crntToolId == ToolCommonData.eToolIds.SymbolSetGenerate))
            {
                grpRptFile.Visibility = Visibility.Visible;
                lbFileNA.Visibility = Visibility.Hidden;
                btnOK.Visibility = Visibility.Visible;

                TargetCore.metricsReturnFileRpt (_crntToolId,
                                                 ref _rptFileFmt,
                                                 ref _rptChkMarks,
                                                 ref _flagOptRptWrap);

                if (_rptFileFmt == ReportCore.eRptFileFmt.html)
                    rbRptFmtHtml.IsChecked = true;
                else if (_rptFileFmt == ReportCore.eRptFileFmt.xml)
                    rbRptFmtXml.IsChecked = true;
                else
                    rbRptFmtText.IsChecked = true;

                grpRptChkMarks.Visibility = Visibility.Hidden;
                grpRptOpt.Visibility = Visibility.Hidden;

                if (_crntToolId == ToolCommonData.eToolIds.PrintLang)
                {
                    grpRptChkMarks.Visibility = Visibility.Visible;
                    grpRptOpt.Visibility = Visibility.Visible;

                    if (_rptChkMarks >= ReportCore.eRptChkMarks.NA)
                        _rptChkMarks = ReportCore.eRptChkMarks.text;

                    if (_rptChkMarks == ReportCore.eRptChkMarks.boxsym)
                        rbRptChkMarksBoxSym.IsChecked = true;
                    else if(_rptChkMarks == ReportCore.eRptChkMarks.txtsym)
                        rbRptChkMarksTxtSym.IsChecked = true;
                    else
                        rbRptChkMarksText.IsChecked = true;

                    if (_flagOptRptWrap)
                        chkRptOptWrap.IsChecked = true;
                    else
                        chkRptOptWrap.IsChecked = false;
                }
                else if (_crntToolId == ToolCommonData.eToolIds.SoftFontGenerate)
                {
                    grpRptChkMarks.Visibility = Visibility.Visible;

                    if (_rptChkMarks >= ReportCore.eRptChkMarks.NA)
                        _rptChkMarks = ReportCore.eRptChkMarks.text;

                    if (_rptChkMarks == ReportCore.eRptChkMarks.boxsym)
                        rbRptChkMarksBoxSym.IsChecked = true;
                    else if (_rptChkMarks == ReportCore.eRptChkMarks.txtsym)
                        rbRptChkMarksTxtSym.IsChecked = true;
                    else
                        rbRptChkMarksText.IsChecked = true;
                }
            }
            else
            {
                grpRptFile.Visibility = Visibility.Hidden;
                lbFileNA.Visibility = Visibility.Visible;
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Set the (hidden) slider object to the passed-in scale value.   //
            // The slider is used as the source binding for a scale           //
            // transform in the (child) Options dialogue window, so that all  //
            // windows use the same scaling mechanism as the main window.     //
            //                                                                //
            // NOTE: it would be better to bind the transform directly to the //
            //       scale value (set and stored in the Main window), but (so //
            //       far) I've failed to find a way to bind directly to a     //
            //       class object Property value.                             //
            //                                                                //
            //----------------------------------------------------------------//

            Double windowScale = MainFormData.WindowScale;

            zoomSlider.Value = windowScale;

            //----------------------------------------------------------------//
            //                                                                //
            // Setting sizes to the resizeable DockPanel element doesn't work!//
            //                                                                //
            //----------------------------------------------------------------//

            this.Height = 350 * windowScale;
            this.Width = 730 * windowScale;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m e t r i c s S a v e                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Save the current settings.                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void metricsSave ()
        {
            TargetCore.metricsSaveFileRpt (_crntToolId, _rptFileFmt,
                                           _rptChkMarks, _flagOptRptWrap);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b R p t F m t H t m l _ C l i c k                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the report format 'html' radio button is selected.     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbRptFmtHtml_Click (object sender,
                                         RoutedEventArgs e)
        {
            _rptFileFmt = ReportCore.eRptFileFmt.html;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b R p t F m t T e x t _ C l i c k                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the report format 'text' radio button is selected.     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbRptFmtText_Click (object sender,
                                         RoutedEventArgs e)
        {
            _rptFileFmt = ReportCore.eRptFileFmt.text;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b R p t F m t X m l _ C l i c k                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the report format 'xml' radio button is selected.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbRptFmtXml_Click (object sender,
                                        RoutedEventArgs e)
        {
            _rptFileFmt = ReportCore.eRptFileFmt.xml;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b R p t C h k M a r k s B o x S y m _ C l i c k                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the report option 'true/false' as 'ballot box' radio   //
        // button is selected.                                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbRptChkMarksBoxSym_Click (object sender,
                                                RoutedEventArgs e)
        {
            _rptChkMarks = ReportCore.eRptChkMarks.boxsym;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b R p t C h k M a r k s T e x t _ C l i c k                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the report option 'true/false' as 'text' radio         //
        // button is selected.                                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbRptChkMarksText_Click (object sender,
                                              RoutedEventArgs e)
        {
            _rptChkMarks = ReportCore.eRptChkMarks.text;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b R p t C h k M a r k s T x t S y m _ C l i c k                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the report option 'true/false' as 'plus/minus' radio   //
        // button is selected.                                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbRptChkMarksTxtSym_Click (object sender,
                                                RoutedEventArgs e)
        {
            _rptChkMarks = ReportCore.eRptChkMarks.txtsym;
        }
    }
}
