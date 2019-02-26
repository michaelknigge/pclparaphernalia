using Microsoft.Win32;
using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Printing;
using System.Printing.IndexedProperties;
using System.Windows;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// Interaction logic for ToolXXXDiags.xaml
    /// 
    /// Class handles the temporary XXX Diags tool form.
    /// 
    /// © Chris Hutchinson 2013
    /// 
    /// </summary>
    
    [System.Reflection.ObfuscationAttribute(Feature = "renaming",
                                            ApplyToMembers = true)]

    public partial class ToolXXXDiags : Window
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//
        /*
        private enum ePDL : byte
        {
            PCL,
            PCLXL,
            Max
        }

        private enum eTrayOpt : byte
        {
            None,
            Auto,
            Selected,
            Max
        }

        private static Int32[] _subsetOrientations = 
        {
            (Int32) PCLOrientations.eIndex.Portrait,
            (Int32) PCLOrientations.eIndex.Landscape,
            (Int32) PCLOrientations.eIndex.ReversePortrait,
            (Int32) PCLOrientations.eIndex.ReverseLandscape
        };

        private static Int32[] _subsetPaperSizes =
        {
            (Int32) PCLPaperSizes.eIndex.A4,
            (Int32) PCLPaperSizes.eIndex.A3,
            (Int32) PCLPaperSizes.eIndex.A5,
            (Int32) PCLPaperSizes.eIndex.A6,
            (Int32) PCLPaperSizes.eIndex.Letter,
            (Int32) PCLPaperSizes.eIndex.Legal,
            (Int32) PCLPaperSizes.eIndex.Executive
        };

        private static Int32[] _subsetPaperTypes =
        {
            (Int32) PCLPaperTypes.eIndex.NotSet,
            (Int32) PCLPaperTypes.eIndex.Plain,
            (Int32) PCLPaperTypes.eIndex.Preprinted,
            (Int32) PCLPaperTypes.eIndex.Letterhead,
            (Int32) PCLPaperTypes.eIndex.Transparency,
            (Int32) PCLPaperTypes.eIndex.Prepunched,
            (Int32) PCLPaperTypes.eIndex.Labels,
            (Int32) PCLPaperTypes.eIndex.Bond,
            (Int32) PCLPaperTypes.eIndex.Recycled,
            (Int32) PCLPaperTypes.eIndex.Color,
            (Int32) PCLPaperTypes.eIndex.Rough
        };
        */
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//
        /*
        private ePDL _indxPDL;

        private eTrayOpt _trayOptPCL;
        private eTrayOpt _trayOptPCLXL;

        private Int32 _ctOrientations;
        private Int32 _ctPaperSizes;
        private Int32 _ctPaperTypes;

        private Int32 _maxTrayIdCrnt;
        private Int32 _maxTrayIdPCL;
        private Int32 _maxTrayIdPCLXL;

        private Int32 _defTrayIdCrnt;
        private Int32 _defTrayIdPCL;
        private Int32 _defTrayIdPCLXL;

        private Int32 _topTrayIdPCL;
        private Int32 _topTrayIdPCLXL;

        private Int32 _autoTrayIdCrnt;
        private Int32 _autoTrayIdPCL;
        private Int32 _autoTrayIdPCLXL;

        private Int32 _indxOrientationPCL;
        private Int32 _indxOrientationPCLXL;
        private Int32 _indxPaperSizePCL;
        private Int32 _indxPaperSizePCLXL;
        private Int32 _indxPaperTypePCL;
        private Int32 _indxPaperTypePCLXL;

        private Int32 _indxPaperTypeCrnt;

        private Int32 _trayIdCount;

        private Boolean _formAsMacroPCL;
        private Boolean _formAsMacroPCLXL;

        private Boolean[] _idSetPCL;
        private Boolean[] _idSetPCLXL;
        */
        private Boolean _initialised;
        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // T o o l X X X D i a g s                                            //
        //                                                                    //
        //--------------------------------------------------------------------//
     
        public ToolXXXDiags(ref ToolCommonData.ePrintLang crntPDL)
        {
            InitializeComponent();

            initialise();

            crntPDL = ToolCommonData.ePrintLang.Unknown;
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
        // Initialisation.                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void initialise()
        {
            _initialised = false;

            //----------------------------------------------------------------//
            //                                                                //
            // Populate form.                                                 //
            //                                                                //
            //----------------------------------------------------------------//

            //----------------------------------------------------------------//

            resetTarget ();

            //----------------------------------------------------------------//
            //                                                                //
            // Reinstate settings from persistent storage.                    //
            //                                                                //
            //----------------------------------------------------------------//

            metricsLoad();

            //----------------------------------------------------------------//
            //                                                                //
            // XXX Diags                                                      //
            //                                                                //
            //----------------------------------------------------------------//

            _initialised = true;

            if (_initialised)
                txtDiags.Clear ();

            prtdata_01 ();

            prtdata_02 ();
         
        }
        /*
        private void rawPrinterTest_00 ()
        {
            // Allow the user to select a file.
            OpenFileDialog openFileDialog = new OpenFileDialog();

            Nullable<Boolean> dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == true)
            {
                // Allow the user to select a printer.
                PrintDialog pd  = new PrintDialog();

                pd.PrinterSettings = new PrinterSettings();
                
                if( DialogResult.OK == pd.ShowDialog(this) )
                {
                    // Print the file to the printer.
                    RawPrinterHelper.SendFileToPrinter(pd.PrinterSettings.PrinterName,
                        openFileDialog.FileName);
                }
            }
        }
        */

        public static void rawPrinterTest_01 ()
        {
            // Specify that the list will contain only the print queues that are installed as local and are shared
            EnumeratedPrintQueueTypes[] enumerationFlags = {EnumeratedPrintQueueTypes.Local};

            LocalPrintServer printServer = new LocalPrintServer();

            //Use the enumerationFlags to filter out unwanted print queues
            PrintQueueCollection printQueuesOnLocalServer = printServer.GetPrintQueues(enumerationFlags);
            
            MessageBox.Show("These are your shared, local print queues:\n\n",
                            "Windows printer selection",
                            MessageBoxButton.OK,
                            MessageBoxImage.Exclamation);

            foreach (PrintQueue printer in printQueuesOnLocalServer)
            {
                MessageBox.Show("\tThe shared printer " + printer.Name + " is located at " + printer.Location + "\n",
                            "Windows printer selection",
                            MessageBoxButton.OK,
                            MessageBoxImage.Exclamation);
            }
        }
 
        /*-
        private void printerTrays ()
        {
            foreach (string p in PrinterSettings.InstalledPrinters)
            {
                PrinterSettings settings = new PrinterSettings { PrinterName = p };

                //  Console.WriteLine ();
                //  Console.WriteLine (settings.PrinterName);

                foreach (PaperSource paperSource in settings.PaperSources)
                {
                //  Console.WriteLine ("    PaperSource: Name = {0}, Kind = {1}, RawKind = {2}",
                //                    paperSource.SourceName, paperSource.Kind, paperSource.RawKind);

                    txtPaperSource.Text = paperSource.SourceName;
                    txtKind.Text = paperSource.Kind.ToString ();
                    txtRawKind.Text = paperSource.RawKind.ToString ();

                }
            }
        }
        */

        private void prtdata_01 ()
        {
            // Specify that the list will contain only the print queues that are installed as local and are shared
            EnumeratedPrintQueueTypes [] enumerationFlags = {EnumeratedPrintQueueTypes.Local,
                                                EnumeratedPrintQueueTypes.Shared};

            LocalPrintServer printServer = new LocalPrintServer ();

            //Use the enumerationFlags to filter out unwanted print queues
            PrintQueueCollection printQueuesOnLocalServer = printServer.GetPrintQueues (enumerationFlags);
            
            txtDiags.Text += "These are your shared, local print queues:\n\n";

            foreach (PrintQueue printer in printQueuesOnLocalServer)
            {
                txtDiags.Text += "\tThe shared printer " + printer.Name + " is located at " + printer.Location + "\n";
            }

            txtDiags.Text += "\n\n\n";
        }

        private void prtdata_02 ()
        {
            // Enumerate the properties, and their types, of a queue without using Reflection
            LocalPrintServer localPrintServer = new LocalPrintServer ();
            PrintQueue defaultPrintQueue = LocalPrintServer.GetDefaultPrintQueue ();

            PrintPropertyDictionary printQueueProperties = defaultPrintQueue.PropertiesCollection;
            
            txtDiags.Text += "These are the properties, and their types, of " +
                             defaultPrintQueue.Name +
                             " " +
                             defaultPrintQueue.GetType ().ToString () + "\n\n";

            foreach (DictionaryEntry entry in printQueueProperties)
            {
                PrintProperty property = (PrintProperty)entry.Value;

                if (property.Value != null)
                {
                     txtDiags.Text += property.Name +
                                      "\t" + property.Value.GetType ().ToString () +
                                      "\t" + property.Value.ToString () +
                                      "\n";
                }
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
        }
    }
}
