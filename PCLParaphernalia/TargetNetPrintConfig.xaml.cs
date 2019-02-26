using System;
using System.Windows;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// Interaction logic for TargetNetPrinter.xaml
    /// 
    /// Class handles the Target (printer) definition form.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>
    
    [System.Reflection.ObfuscationAttribute(Feature = "renaming",
                                            ApplyToMembers = true)]

    public partial class TargetNetPrintConfig : Window
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Fields (class variables).                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Int32 _printerPort;
        private Int32 _timeoutReceive;
        private Int32 _timeoutSend;

        private String _printerAddress;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // T a r g e t N e t P r i n t e r                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public TargetNetPrintConfig()
        {
            InitializeComponent();

            initialise();
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
        // i n i t i a l i s e                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initialise 'target' data.                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void initialise()
        {
            TargetCore.metricsLoadNetPrinter (ref _printerAddress,
                                              ref _printerPort,
                                              ref _timeoutSend,
                                              ref _timeoutReceive);

            txtPrinterAddress.Text = _printerAddress;
            txtPrinterPort.Text    = _printerPort.ToString();

            txtTimeoutSend.Text    = _timeoutSend.ToString();
            txtTimeoutReceive.Text = _timeoutReceive.ToString();

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

            this.Height = 280 * windowScale;
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
            TargetCore.metricsSaveNetPrinter (_printerAddress,
                                              _printerPort,
                                              _timeoutSend,
                                              _timeoutReceive);
        }

    
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t P r i n t e r A d d r e s s _ T e x t C h a n g e d          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the target 'Printer Address' text is changed.          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtPrinterAddress_TextChanged(object sender,
                                                   TextChangedEventArgs e)
        {
            _printerAddress = txtPrinterAddress.Text;
        }
    
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t P r i n t e r P o r t _ T e x t C h a n g e d                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the target 'Printer Port' text is changed.             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtPrinterPort_TextChanged(object sender,
                                                TextChangedEventArgs e)
        {
            _printerPort = Int32.Parse(txtPrinterPort.Text);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t T i m e o u t R e c e i v e _ T e x t C h a n g e d          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the timeout 'Receive' text is changed.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtTimeoutReceive_TextChanged (object sender,
                                                    TextChangedEventArgs e)
        {
            _timeoutReceive = Int32.Parse(txtTimeoutReceive.Text);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t T i m e o u t S e n d _ T e x t C h a n g e d                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the timeout 'Send' text is changed.                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtTimeoutSend_TextChanged (object sender,
                                                 TextChangedEventArgs e)
        {
            _timeoutSend = Int32.Parse(txtTimeoutSend.Text);
        }
    }
}
