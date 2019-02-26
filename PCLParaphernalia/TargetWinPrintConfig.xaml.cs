using System;
using System.Drawing.Printing;
using System.Windows;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// Interaction logic for TargetWinPrintConfig.xaml
    /// 
    /// Class handles the Target (printer) definition form.
    /// 
    /// © Chris Hutchinson 2014
    /// 
    /// </summary>
    
    [System.Reflection.ObfuscationAttribute(Feature = "renaming",
                                            ApplyToMembers = true)]

    public partial class TargetWinPrintConfig : Window
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Fields (class variables).                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean _initialised;

        private String _printerName;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // T a r g e t W i n P r i n t e r                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public TargetWinPrintConfig()
        {
            _initialised = false;

            InitializeComponent();

            initialise();

            _initialised = true;
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
        // c b P r i n t e r s _ S e l e c t i o n C h a n g e d              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Printers item has changed.                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbPrinters_SelectionChanged (object sender,
                                                  System.EventArgs e)
        {
            if (_initialised)
            {
                if (cbPrinters.SelectedIndex != -1)
                {
                    _printerName = cbPrinters.SelectedItem.ToString ();
                }
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

        private void initialise()
        {
            Int32 indxPrinter = 0;
            Int32 ctPrinters  = 0;

            String printerName;

            //----------------------------------------------------------------//
            //                                                                //
            // Populate form.                                                 //
            //                                                                //
            //----------------------------------------------------------------//

            TargetCore.metricsLoadWinPrinter (ref _printerName);

            cbPrinters.Items.Clear ();

            ctPrinters = PrinterSettings.InstalledPrinters.Count;

            for (Int32 i = 0; i < ctPrinters; i++)
            {
                printerName = PrinterSettings.InstalledPrinters[i];

                cbPrinters.Items.Add (printerName);

                if (printerName == _printerName)
                    indxPrinter = i;
            }

            //----------------------------------------------------------------//

            if ((indxPrinter < 0) || (indxPrinter >= ctPrinters))
                indxPrinter = 0;

            cbPrinters.SelectedIndex = indxPrinter;
            _printerName = cbPrinters.Text; 

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

            this.Height = 240 * windowScale;
            this.Width = 440 * windowScale;

            // Double h = resizeable.Height;
            // Double w = resizeable.Width;

            // this.Height = h;
            // this.Width = w;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m e t r i c s S a v e                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Save the current settings.                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void metricsSave()
        {
            TargetCore.metricsSaveWinPrinter (_printerName);
        }
    }
}
