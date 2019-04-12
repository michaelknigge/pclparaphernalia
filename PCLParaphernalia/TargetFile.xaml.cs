using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// Interaction logic for TargetFile.xaml
    /// 
    /// Class handles the Target (file) definition form.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>
    
    [System.Reflection.ObfuscationAttribute(Feature = "renaming",
                                            ApplyToMembers = true)]

    public partial class TargetFile : Window
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Fields (class variables).                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private String _saveFilename;
        private ToolCommonData.eToolIds    _crntToolId;
        private ToolCommonData.eToolSubIds _crntSubId;
        private ToolCommonData.ePrintLang  _crntPDL;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // T a r g e t F i l e                                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public TargetFile (ToolCommonData.eToolIds    crntToolId,
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
        // b t n O p F i l e n a m e B r o w s e _ C l i c k                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the output file 'Browse' button is clicked.            //
        // Invoke 'Save As' dialogue to select target file.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void btnOpFilenameBrowse_Click(object sender, RoutedEventArgs e)
        {
            Boolean selected;

            String filename = _saveFilename;

            selected = selectTargetFile (ref filename);

            if (selected)
            {
                _saveFilename = filename;
                txtOpFilename.Text = _saveFilename;
            }
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
            // Output file data.                                              //
            //                                                                //
            //----------------------------------------------------------------//

            if ((_crntToolId == ToolCommonData.eToolIds.FontSample)
                                          ||
                (_crntToolId == ToolCommonData.eToolIds.FormSample)
                                          ||
                (_crntToolId == ToolCommonData.eToolIds.ImageBitmap)
                                          ||
                (_crntToolId == ToolCommonData.eToolIds.MiscSamples)
                                          ||
                (_crntToolId == ToolCommonData.eToolIds.PrintArea)
                                          ||
                (_crntToolId == ToolCommonData.eToolIds.PrnPrint)
                                          ||
                (_crntToolId == ToolCommonData.eToolIds.StatusReadback)
                                          ||
                (_crntToolId == ToolCommonData.eToolIds.TrayMap))
            {
                grpOpFile.Visibility = Visibility.Visible;
                lbFileNA.Visibility = Visibility.Hidden;
                btnOK.Visibility = Visibility.Visible;

                TargetCore.metricsReturnFileCapt (_crntToolId,
                                                  _crntSubId,
                                                  _crntPDL,
                                                  ref _saveFilename);

                txtOpFilename.Text = _saveFilename;
            }
            else
            {
                grpOpFile.Visibility = Visibility.Hidden;
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

            this.Height = 300 * windowScale;
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
            TargetCore.metricsSaveFileCapt (_crntToolId, _crntSubId, _crntPDL,
                                            _saveFilename);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e l e c t T a r g e t F i l e                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Browse' button is clicked.                        //
        // Invoke 'Save As' dialogue to select target file.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean selectTargetFile (ref String targetFile)
        {
            SaveFileDialog saveDialog = ToolCommonFunctions.createSaveFileDialog(targetFile);

            Nullable<Boolean> dialogResult = saveDialog.ShowDialog ();

            if (dialogResult == true)
                targetFile = saveDialog.FileName;

            return dialogResult == true;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t O p F i l e n a m e _ Lo s t F o c u s                       //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the target output 'Filename' text has lost focus.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtOpFilename_LostFocus (object sender,
                                              RoutedEventArgs e)
        {
            _saveFilename = txtOpFilename.Text;
        }
    }
}
