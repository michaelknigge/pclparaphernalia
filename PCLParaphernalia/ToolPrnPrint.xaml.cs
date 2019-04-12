using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// Interaction logic for ToolPrnPrint.xaml
    /// 
    /// Class handles the Prn Print tool form.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>
    
    [System.Reflection.ObfuscationAttribute(Feature = "renaming",
                                            ApplyToMembers = true)]

    public partial class ToolPrnPrint : Window
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private String _prnFilename;

   //   private Boolean _initialised;

        private static Stream _ipStream = null;
        private static BinaryReader _binReader = null;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // T o o l P R N P r i n t                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public ToolPrnPrint (ref ToolCommonData.ePrintLang crntPDL)
        {
            InitializeComponent();

            initialise();

            crntPDL = ToolCommonData.ePrintLang.Unknown;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b t n F i l e n a m e B r o w s e _ C l i c k                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Browse' button is clicked.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void btnFilenameBrowse_Click(object sender, RoutedEventArgs e)
        {
            Boolean selected;

            String filename = _prnFilename;

            selected = selectPrnFile (ref filename);

            if (selected)
            {
                _prnFilename = filename;
                txtFilename.Text = _prnFilename;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b t n G e n e r a t e _ C l i c k                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Generate' button is clicked.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            BinaryWriter binWriter = null;

            TargetCore.requestStreamOpen (
                ref binWriter,
                ToolCommonData.eToolIds.PrnPrint,
                ToolCommonData.eToolSubIds.None,
                ToolCommonData.ePrintLang.Unknown);

            copyPrnFile (_prnFilename, binWriter);

            TargetCore.requestStreamWrite (false);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c o p y P r n F i l e                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Copy print file contents to output stream.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean copyPrnFile(String       prnFilename,
                                          BinaryWriter prnWriter)
        {
            Boolean OK = true;

            Boolean fileOpen = false;

            fileOpen = prnOpen(prnFilename);

            if (!fileOpen)
            {
                OK = false;
            }
            else
            {
                const Int32 bufSize = 2048;
                Int32 readSize;

                Boolean endLoop;

                Byte[] buf = new Byte[bufSize];

                endLoop = false;

                while (!endLoop)
                {
                    readSize = _binReader.Read(buf, 0, bufSize);

                    if (readSize == 0)
                        endLoop = true;
                    else
                        prnWriter.Write(buf, 0, readSize);
                }

                prnClose();
            }

            return OK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g i v e C r n t P D L                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void giveCrntPDL(ref ToolCommonData.ePrintLang crntPDL)
        {
            crntPDL = ToolCommonData.ePrintLang.Unknown;
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
        //  _initialised = false;

            //----------------------------------------------------------------//
            //                                                                //
            // Populate form.                                                 //
            //                                                                //
            //----------------------------------------------------------------//

            resetTarget ();

            //----------------------------------------------------------------//
            //                                                                //
            // Reinstate settings from persistent storage.                    //
            //                                                                //
            //----------------------------------------------------------------//

            metricsLoad();

            txtFilename.Text = _prnFilename;

        //  _initialised = true;
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
            ToolPrnPrintPersist.loadDataGeneral (ref _prnFilename);
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
            ToolPrnPrintPersist.saveDataGeneral (_prnFilename);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r n C l o s e                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Close stream and file.                                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void prnClose()
        {
            _binReader.Close();
            _ipStream.Close();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r n O p e n                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Open read stream for specified print file.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean prnOpen(String filename)
        {
            Boolean open = false;

            if ((filename == null) || (filename == ""))
            {
                MessageBox.Show("Print file name is null.",
                                "Print file selection",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);

                return false;
            }
            else if (!File.Exists(filename))
            {
                MessageBox.Show("Print file '" + filename +
                                "' does not exist.",
                                "Print file selection",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);

                return false;
            }
            else
            {
                _ipStream = File.Open(filename,
                                      FileMode.Open,
                                      FileAccess.Read,
                                      FileShare.None);

                if (_ipStream != null)
                {
                    open = true;

                    _binReader = new BinaryReader(_ipStream);
                }
            }

            return open;
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
                btnGenerate.Content = "Copy PRN file contents to file";
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

                btnGenerate.Content = "Send PRN file contents to " +
                                      "\r\n" +
                                      netPrnAddress + " : " +
                                      netPrnPort.ToString ();
            }
            else if (targetType == TargetCore.eTarget.WinPrinter)
            {
                String winPrintername = "";

                TargetCore.metricsLoadWinPrinter (ref winPrintername);

                btnGenerate.Content = "Send PRN file contents to printer " +
                                      "\r\n" +
                                      winPrintername;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e l e c t P r n F i l e                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initiate 'open file' dialogue.                                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean selectPrnFile (ref String prnFilename)
        {
            OpenFileDialog openDialog = ToolCommonFunctions.createOpenFileDialog(prnFilename);

            openDialog.Filter = "Print Files|" +
                                "*.prn; *.pcl; *.dia;" +
                                "*.PRN; *.PCL; *.DIA" +
                                "|All files|" +
                                "*.*";

            Nullable<Boolean> dialogResult = openDialog.ShowDialog();

            if (dialogResult == true)
                prnFilename = openDialog.FileName;

            return dialogResult == true;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t F i l e n a m e _ L o s t F o c u s                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Image filename text has lost focus.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtFilename_LostFocus(object sender,
                                           RoutedEventArgs e)
        {
            _prnFilename = txtFilename.Text;
        }
    }
}
