using System;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;

namespace PCLParaphernalia
{
    /// <summary>
    /// Interaction logic for MainForm.xaml
    /// 
    /// This is the main form of the PCLParaphernalia application.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    [System.Reflection.ObfuscationAttribute(Feature = "renaming",
                                            ApplyToMembers = true)]

    public partial class MainForm : Window
    {
        public const String _regMainKey = "Software\\PCLParaphernalia";

        public Boolean _runXXXDiags = false;  // ****  design time toggle ****//

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Fields (class variables).                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private ToolFontSample          _subFormToolFontSample          = null;
        private ToolFormSample          _subFormToolFormSample          = null;
        private ToolImageBitmap         _subFormToolImageBitmap         = null;
        private ToolMakeOverlay         _subFormToolMakeOverlay         = null;
        private ToolMiscSamples         _subFormToolMiscSamples         = null;
  //    private ToolPatternGenerate     _subFormToolPatternGenerate     = null;
        private ToolPrintArea           _subFormToolPrintArea           = null;
        private ToolPrintLang           _subFormToolPrintLang           = null;
        private ToolPrnAnalyse          _subFormToolPrnAnalyse          = null;
        private ToolPrnPrint            _subFormToolPrnPrint            = null;
        private ToolSoftFontGenerate    _subFormToolSoftFontGenerate    = null;
        private ToolStatusReadback      _subFormToolStatusReadback      = null;
        private ToolSymbolSetGenerate   _subFormToolSymbolSetGenerate   = null;
        private ToolTrayMap             _subFormToolTrayMap             = null;
        private ToolXXXDiags            _subFormToolXXXDiags            = null;

        private ToolCommonData.eToolIds _crntToolId =
            ToolCommonData.eToolIds.Min;

        private ToolCommonData.ePrintLang _crntPDL =
            ToolCommonData.ePrintLang.Unknown;

        private ToolCommonData.eToolSubIds _crntSubId =
            ToolCommonData.eToolSubIds.None;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // M a i n f o r m                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public MainForm(String filename)
        {
            InitializeComponent();

            Int32 mwLeft = -1,
                  mwTop = -1,
                  mwHeight = -1,
                  mwWidth = -1,
                  mwScale = 100;

            Int32 versionMajorOld = -1;
            Int32 versionMinorOld = -1;
            Int32 versionBuildOld = -1;
            Int32 versionRevisionOld = -1;

            Int32 versionMajorCrnt = -1;
            Int32 versionMinorCrnt = -1;
            Int32 versionBuildCrnt = -1;
            Int32 versionRevisionCrnt = -1;

            Double windowScale = 1.0;

            //----------------------------------------------------------------//
            //                                                                //
            // Load window state values from registry.                        //
            //                                                                //
            //----------------------------------------------------------------//

            MainFormPersist.loadWindowData (ref mwLeft,
                                           ref mwTop,
                                           ref mwHeight,
                                           ref mwWidth,
                                           ref mwScale);

            if ((mwLeft == -1) || (mwTop == -1) ||
                (mwHeight == -1) || (mwWidth == -1))
            {
                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                this.Width = 801;
                this.Height = 842;
            }
            else
            {
                this.WindowStartupLocation = WindowStartupLocation.Manual;

                this.Left = mwLeft;
                this.Top = mwTop;
                this.Height = mwHeight;
                this.Width = mwWidth;
            }

            if ((mwScale < 25) || (mwScale > 1000))
            {
                mwScale = 100;
            }

            windowScale = ((Double)mwScale / 100);

            MainFormData.WindowScale = windowScale;

            zoomSlider.Value = windowScale;

            //----------------------------------------------------------------//
            //                                                                //
            // Check for version-specific updates.                            //
            //                                                                //
            //----------------------------------------------------------------//

            Assembly assembly = Assembly.GetExecutingAssembly();

            AssemblyName assemblyName = assembly.GetName();

            versionMajorCrnt    = (Int32)assemblyName.Version.Major;
            versionMinorCrnt    = (Int32)assemblyName.Version.Minor;
            versionBuildCrnt    = (Int32)assemblyName.Version.Build;
            versionRevisionCrnt = (Int32)assemblyName.Version.Revision;

            MainFormData.setVersionData (true, versionMajorCrnt,
                                               versionMinorCrnt,
                                               versionBuildCrnt,
                                               versionRevisionCrnt);

            MainFormPersist.loadVersionData (ref versionMajorOld,
                                             ref versionMinorOld,
                                             ref versionBuildOld,
                                             ref versionRevisionOld);

            MainFormData.setVersionData (false, versionMajorOld,
                                                versionMinorOld,
                                                versionBuildOld,
                                                versionRevisionOld);

            if ((versionMajorCrnt != versionMajorOld) ||
                (versionMinorCrnt != versionMinorOld) ||
                (versionBuildCrnt != versionBuildOld) ||
                (versionRevisionCrnt != versionRevisionOld))
            {
                MainFormData.VersionChange = true;
            }
            else
            {
                MainFormData.VersionChange = false;
            }

            if (versionMajorOld == -1)
            {
                //----------------------------------------------------------------//
                //                                                                //
                // First run of post 2.5.0.0 version.                             //
                // Invoke default working folder dialogue.                        //
                //                                                                //
                //----------------------------------------------------------------//
        
                WorkFolder workFolder = new WorkFolder ();

                Nullable<Boolean> dialogResult = workFolder.ShowDialog();
            }

            MainFormPersist.saveVersionData (versionMajorCrnt,
                                             versionMinorCrnt,
                                             versionBuildCrnt,
                                             versionRevisionCrnt);

            ToolCommonData.loadWorkFoldername();

            //----------------------------------------------------------------//
            //                                                                //
            // Load Target state values from registry.                        //
            //                                                                //
            //----------------------------------------------------------------//

            TargetCore.initialiseSettings();

            if (TargetCore.getType() == TargetCore.eTarget.File)
            {
                menuItemTargetFile.IsChecked = true;
                menuItemTargetNetPrinter.IsChecked = false;
                menuItemTargetWinPrinter.IsChecked = false;
            }
            else if (TargetCore.getType() == TargetCore.eTarget.NetPrinter)
            {
                menuItemTargetFile.IsChecked = false;
                menuItemTargetNetPrinter.IsChecked = true;
                menuItemTargetWinPrinter.IsChecked = false;
            }
            else if (TargetCore.getType () == TargetCore.eTarget.WinPrinter)
            {
                menuItemTargetFile.IsChecked = false;
                menuItemTargetNetPrinter.IsChecked = false;
                menuItemTargetWinPrinter.IsChecked = true;
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Load tool.                                                     //
            // If a command-line parameter is present, load the               //
            // 'PRN File Analyse' tool, and pass the parameter which          //
            // identifies the file to be analysed.                            //
            // Otherwise, load the tool in use when the application was last  //
            // closed.                                                        // 
            //                                                                //
            //----------------------------------------------------------------//

            ToolCommonData.eToolIds startToolId;

            _crntToolId = ToolCommonData.eToolIds.Min;

            if (_runXXXDiags)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Load 'XXX Diags' tool.                                     //
                //                                                            // 
                // ***** for design time use only *****                       //
                //                                                            //
                //------------------------------------------------------------//
                
                toolXXXDiags_Selected (this, null);
            }
            else if (filename != "")
            {
                //------------------------------------------------------------//
                //                                                            //
                // Load 'PRN File Analyse' tool and pass in file name.        //
                //                                                            //
                //------------------------------------------------------------//
                
                startToolId = ToolCommonData.eToolIds.PrnAnalyse;

                toolPrnAnalyse_Selected(this, null);

                if (filename != "")
                    _subFormToolPrnAnalyse.prnFileProcess(filename);
            }
            else
            {
                //------------------------------------------------------------//
                //                                                            //
                // Load Tool state values from registry.                      //
                //                                                            //
                //------------------------------------------------------------//

                Int32 crntToolIndex = 0;

                ToolCommonPersist.loadData(ref crntToolIndex);

                if ((crntToolIndex > (Int32)ToolCommonData.eToolIds.Min) &&
                    (crntToolIndex < (Int32)ToolCommonData.eToolIds.Max))
                    startToolId = (ToolCommonData.eToolIds)crntToolIndex;
                else
                    startToolId = ToolCommonData.eToolIds.PrintLang;

                if (startToolId ==
                    ToolCommonData.eToolIds.FontSample)
                    toolFontSample_Selected(this, null);
                else if (startToolId ==
                    ToolCommonData.eToolIds.FormSample)
                    toolFormSample_Selected(this, null);
                else if (startToolId ==
                    ToolCommonData.eToolIds.ImageBitmap)
                    toolImageBitmap_Selected(this, null);
                else if (startToolId ==
                    ToolCommonData.eToolIds.MakeOverlay)
                    toolMakeOverlay_Selected(this, null);
                else if (startToolId ==
                    ToolCommonData.eToolIds.MiscSamples)
                    toolMiscSamples_Selected(this, null);
        //      else if (startToolId ==
        //          ToolCommonData.eToolIds.PatternGenerate)
        //          toolPatternGenerate_Selected(this, null);
                else if (startToolId ==
                    ToolCommonData.eToolIds.PrintArea)
                    toolPrintArea_Selected(this, null);
                else if (startToolId ==
                    ToolCommonData.eToolIds.PrintLang)
                    toolPrintLang_Selected(this, null);
                else if (startToolId ==
                    ToolCommonData.eToolIds.PrnAnalyse)
                    toolPrnAnalyse_Selected(this, null);
                else if (startToolId ==
                    ToolCommonData.eToolIds.PrnPrint)
                    toolPrnPrint_Selected(this, null);
                else if (startToolId ==
                    ToolCommonData.eToolIds.SoftFontGenerate)
                    toolSoftFontGenerate_Selected(this, null);
                else if (startToolId ==
                    ToolCommonData.eToolIds.StatusReadback)
                    toolStatusReadback_Selected(this, null);
                else if (startToolId ==
                    ToolCommonData.eToolIds.SymbolSetGenerate)
                    toolSymbolSetGenerate_Selected(this, null);
                else if (startToolId ==
                    ToolCommonData.eToolIds.TrayMap)
                    toolTrayMap_Selected(this, null);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c r n t T o o l R e s e t P D L                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve the current PDL selected within the current tool.         //
        // This is so that if TargetFile is configured, any new value is      //
        // stored in the appropriate PDL-specific registry key.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void crntToolResetPDL()
        {
            if (_crntToolId ==
                ToolCommonData.eToolIds.FontSample)
                _subFormToolFontSample.giveCrntPDL(ref _crntPDL);
            else if (_crntToolId ==
                ToolCommonData.eToolIds.FormSample)
                _subFormToolFormSample.giveCrntPDL(ref _crntPDL);
            else if (_crntToolId ==
                ToolCommonData.eToolIds.ImageBitmap)
                _subFormToolImageBitmap.giveCrntPDL(ref _crntPDL);
            else if (_crntToolId ==
                ToolCommonData.eToolIds.MakeOverlay)
                _subFormToolMakeOverlay.giveCrntPDL(ref _crntPDL);
            else if (_crntToolId ==
                ToolCommonData.eToolIds.MiscSamples)
                _subFormToolMiscSamples.giveCrntPDL(ref _crntPDL);
     //     else if (_crntToolId ==
     //         ToolCommonData.eToolIds.PatternGenerate)
     //         _subFormToolPatternGenerate.giveCrntPDL(ref _crntPDL);
            else if (_crntToolId ==
                ToolCommonData.eToolIds.PrintArea)
                _subFormToolPrintArea.giveCrntPDL(ref _crntPDL);
            else if (_crntToolId ==
                ToolCommonData.eToolIds.PrintLang)
                _subFormToolPrintLang.giveCrntPDL(ref _crntPDL);
            else if (_crntToolId ==
                ToolCommonData.eToolIds.PrnAnalyse)
                _subFormToolPrnAnalyse.giveCrntPDL(ref _crntPDL);
            else if (_crntToolId ==
                ToolCommonData.eToolIds.PrnPrint)
                _subFormToolPrnPrint.giveCrntPDL(ref _crntPDL);
            else if (_crntToolId ==
                ToolCommonData.eToolIds.SoftFontGenerate)
                _subFormToolSoftFontGenerate.giveCrntPDL(ref _crntPDL);
            else if (_crntToolId ==
                ToolCommonData.eToolIds.StatusReadback)
                _subFormToolStatusReadback.giveCrntPDL(ref _crntPDL);
            else if (_crntToolId ==
                ToolCommonData.eToolIds.SymbolSetGenerate)
                _subFormToolSymbolSetGenerate.giveCrntPDL(ref _crntPDL);
            else if (_crntToolId ==
                ToolCommonData.eToolIds.TrayMap)
                _subFormToolTrayMap.giveCrntPDL(ref _crntPDL);
            else if (_crntToolId ==
                ToolCommonData.eToolIds.XXXDiags)
                _subFormToolXXXDiags.giveCrntPDL(ref _crntPDL);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c r n t T o o l R e s e t S u b I d                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve the current sub-identifier (if any) selected within the   //
        // current tool.                                                      //
        // This is so that if TargetFile is configured, any new value is      //
        // stored in the appropriate PDL-specific registry key.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void crntToolResetSubId()
        {
            _crntSubId = ToolCommonData.eToolSubIds.None;

            if (_crntToolId ==
                ToolCommonData.eToolIds.MiscSamples)
                _subFormToolMiscSamples.giveCrntType(ref _crntSubId);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c r n t T o o l R e s e t T a r g e t                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Reset 'current tool' button details (where necessary) after Target //
        // changed.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void crntToolResetTarget()
        {
            if (_crntToolId ==
                ToolCommonData.eToolIds.FontSample)
                _subFormToolFontSample.resetTarget();
            else if (_crntToolId ==
                ToolCommonData.eToolIds.FormSample)
                _subFormToolFormSample.resetTarget();
            else if (_crntToolId ==
                ToolCommonData.eToolIds.ImageBitmap)
                _subFormToolImageBitmap.resetTarget();
            else if (_crntToolId ==
                ToolCommonData.eToolIds.MakeOverlay)
                _subFormToolMakeOverlay.resetTarget();
            else if (_crntToolId ==
                ToolCommonData.eToolIds.MiscSamples)
                _subFormToolMiscSamples.resetTarget();
    //      else if (_crntToolId ==
    //          ToolCommonData.eToolIds.PatternGenerate)
    //          _subFormToolPatternGenerate.resetTarget();
            else if (_crntToolId ==
                ToolCommonData.eToolIds.PrintArea)
                _subFormToolPrintArea.resetTarget();
            else if (_crntToolId ==
                ToolCommonData.eToolIds.PrintLang)
                _subFormToolPrintLang.resetTarget();
            else if (_crntToolId ==
                ToolCommonData.eToolIds.PrnAnalyse)
                _subFormToolPrnAnalyse.resetTarget();
            else if (_crntToolId ==
                ToolCommonData.eToolIds.PrnPrint)
                _subFormToolPrnPrint.resetTarget();
            else if (_crntToolId ==
                ToolCommonData.eToolIds.SoftFontGenerate)
                _subFormToolSoftFontGenerate.resetTarget();
            else if (_crntToolId ==
                ToolCommonData.eToolIds.StatusReadback)
                _subFormToolStatusReadback.resetTarget();
            else if (_crntToolId ==
                ToolCommonData.eToolIds.SymbolSetGenerate)
                _subFormToolSymbolSetGenerate.resetTarget();
            else if (_crntToolId ==
                ToolCommonData.eToolIds.TrayMap)
                _subFormToolTrayMap.resetTarget();
            else if (_crntToolId ==
                ToolCommonData.eToolIds.XXXDiags)
                _subFormToolXXXDiags.resetTarget();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c r n t T o o l S a v e M e t r i c s                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Save metrics for last active subform.                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void crntToolSaveMetrics()
        {
            if (_crntToolId != ToolCommonData.eToolIds.Min)
                ToolCommonPersist.saveData((Int32)_crntToolId);

            if (_crntToolId ==
                ToolCommonData.eToolIds.FontSample)
                _subFormToolFontSample.metricsSave();
            else if (_crntToolId ==
                ToolCommonData.eToolIds.FormSample)
                _subFormToolFormSample.metricsSave();
            else if (_crntToolId ==
                ToolCommonData.eToolIds.ImageBitmap)
                _subFormToolImageBitmap.metricsSave();
            else if (_crntToolId ==
                ToolCommonData.eToolIds.MakeOverlay)
                _subFormToolMakeOverlay.metricsSave();
            else if (_crntToolId ==
                ToolCommonData.eToolIds.MiscSamples)
                _subFormToolMiscSamples.metricsSave();
     //     else if (_crntToolId ==
     //         ToolCommonData.eToolIds.PatternGenerate)
     //         _subFormToolPatternGenerate.metricsSave();
            else if (_crntToolId ==
                ToolCommonData.eToolIds.PrintArea)
                _subFormToolPrintArea.metricsSave();
            else if (_crntToolId ==
                ToolCommonData.eToolIds.PrintLang)
                _subFormToolPrintLang.metricsSave();
            else if (_crntToolId ==
                ToolCommonData.eToolIds.PrnAnalyse)
                _subFormToolPrnAnalyse.metricsSave();
            else if (_crntToolId ==
                ToolCommonData.eToolIds.PrnPrint)
                _subFormToolPrnPrint.metricsSave();
            else if (_crntToolId ==
                ToolCommonData.eToolIds.SoftFontGenerate)
                _subFormToolSoftFontGenerate.metricsSave();
            else if (_crntToolId ==
                ToolCommonData.eToolIds.StatusReadback)
                _subFormToolStatusReadback.metricsSave();
            else if (_crntToolId ==
                ToolCommonData.eToolIds.SymbolSetGenerate)
                _subFormToolSymbolSetGenerate.metricsSave();
            else if (_crntToolId ==
                ToolCommonData.eToolIds.TrayMap)
                _subFormToolTrayMap.metricsSave();
            else if (_crntToolId ==
                ToolCommonData.eToolIds.XXXDiags)
                _subFormToolXXXDiags.metricsSave();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c r n t T o o l U n c h e c k A l l                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called whenever current tool is selected/changed.                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void crntToolUncheckAll()
        {
            menuItemToolFontSample.IsChecked = false;
            menuItemToolFormSample.IsChecked = false;
            menuItemToolImageBitmap.IsChecked = false;
            menuItemToolMakeOverlay.IsChecked = false;
            menuItemToolMiscSamples.IsChecked = false;
        //  menuItemToolPatternGenerate.IsChecked = false;
            menuItemToolPrintArea.IsChecked = false;
            menuItemToolPrintLang.IsChecked = false;
            menuItemToolPrnAnalyse.IsChecked = false;
            menuItemToolPrnPrint.IsChecked = false;
            menuItemToolSoftFontGenerate.IsChecked = false;
            menuItemToolStatusReadback.IsChecked = false;
            menuItemToolSymbolSetGenerate.IsChecked = false;
            menuItemToolTrayMap.IsChecked = false;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // f i l e E x i t _ C l i c k                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Application shutdown.                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void fileExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // h e l p A b o u t _ C l i c k                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Help | About' menu item is selected.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void helpAbout_Click(object sender, RoutedEventArgs e)
        {
            String deploymentVersion = "";
            String assemblyVersion = "";
            String crntVersion = "";

            if (ApplicationDeployment.IsNetworkDeployed)
                deploymentVersion =
                    ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            else
                deploymentVersion = "Stand-alone";

            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyName assemblyName = assembly.GetName();
            assemblyVersion = assemblyName.Version.ToString();

            if (deploymentVersion == assemblyVersion)
                crntVersion = "Version " + deploymentVersion;
            else
                crntVersion = "Deployment Version\t= " +
                              deploymentVersion + "\r\n" +
                              "Assembly Version\t= " +
                              assemblyVersion;

            MessageBox.Show("PCL Paraphernalia\r\n\r\n" +
                             crntVersion + "\r\n\r\n" +
                             "To report errors, send details to:\r\n\r\n" +
                             "support@pclparaphernalia.eu\r\n\r\n" +
                             "Web site: " +
                             "https://www.pclparaphernalia.eu",
                             "Help About",
                              MessageBoxButton.OK,
                              MessageBoxImage.Information);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // h e l p C o n t e n t s _ C l i c k                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Help | Contents' menu item is selected.           //
        // Note that WPF does not have the Help class as per WinForms.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void helpContents_Click(object sender, RoutedEventArgs e)
        {
            String appStartPath = Path.GetDirectoryName(
                Process.GetCurrentProcess().MainModule.FileName);

            String helpFile = appStartPath + @"\PCLParaphernalia.chm";

            if (File.Exists(helpFile))
            {
                Process.Start(helpFile);
            }
            else
            {
                MessageBox.Show("Help file '" + helpFile +
                                "' does not exist.",
                                "Help file selection",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a r g e t F i l e S e l e c t _ C l i c k                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Target | File | Select' item is selected.         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void targetFileSelect_Click(object sender, RoutedEventArgs e)
        {
            menuItemTargetFile.IsChecked    = true;
            menuItemTargetNetPrinter.IsChecked = false;
            menuItemTargetWinPrinter.IsChecked = false;

            TargetCore.metricsSaveType(TargetCore.eTarget.File);

            crntToolResetTarget ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a r g e t F i l e C o n f i g u r e _ C l i c k                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Target | File | Configure' item is selected.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void targetFileConfigure_Click(object sender,
                                               RoutedEventArgs e)
        {
            crntToolResetPDL();
            
            crntToolResetSubId();

            TargetFile targetFile = new TargetFile (_crntToolId, _crntSubId,
                                                    _crntPDL);

            Nullable<Boolean> dialogResult = targetFile.ShowDialog ();

            if (dialogResult == true)
            {
                menuItemTargetFile.IsChecked = true;
                menuItemTargetNetPrinter.IsChecked = false;
                menuItemTargetWinPrinter.IsChecked = false;

                TargetCore.metricsSaveType (TargetCore.eTarget.File);

                crntToolResetTarget ();
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a r g e t N e t P r i n t e r S e l e c t _ C l i c k            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Target | Network Printer | Select' item is        //
        // selected.                                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void targetNetPrinterSelect_Click (object sender,
                                                   RoutedEventArgs e)
        {
            menuItemTargetFile.IsChecked    = false;
            menuItemTargetNetPrinter.IsChecked = true;
            menuItemTargetWinPrinter.IsChecked = false;

            TargetCore.metricsSaveType(TargetCore.eTarget.NetPrinter);

            crntToolResetTarget ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a r g e t N e t P r i n t e r C o n f i g u r e _ C l i c k      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Target | Network Printer | Configure' item is     //
        // selected.                                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void targetNetPrinterConfigure_Click (object sender,
                                                      RoutedEventArgs e)
        {
            TargetNetPrintConfig targetNetPrintConfig =
                new TargetNetPrintConfig ();

            Nullable<Boolean> dialogResult = targetNetPrintConfig.ShowDialog ();

            if (dialogResult == true)
            {
                menuItemTargetFile.IsChecked = false;
                menuItemTargetNetPrinter.IsChecked = true;
                menuItemTargetWinPrinter.IsChecked = false;

                TargetCore.metricsSaveType (TargetCore.eTarget.NetPrinter);

                crntToolResetTarget ();
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a r g e t R p t F i l e C o n f i g u r e _ C l i c k            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Target | Report File | Configure' item is         //
        // selected.                                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void targetRptFileConfigure_Click (object sender,
                                                   RoutedEventArgs e)
        {
            crntToolResetPDL ();

            crntToolResetSubId ();

            TargetRptFile targetRptFile = new TargetRptFile (_crntToolId,
                                                             _crntSubId,
                                                             _crntPDL);

            Nullable<Boolean> dialogResult = targetRptFile.ShowDialog ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a r g e t W i n P r i n t e r S e l e c t _ C l i c k            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Target | Windows Printer | Select' item is        //
        // selected.                                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void targetWinPrinterSelect_Click (object sender,
                                                   RoutedEventArgs e)
        {
            menuItemTargetFile.IsChecked = false;
            menuItemTargetNetPrinter.IsChecked = false;
            menuItemTargetWinPrinter.IsChecked = true;

            TargetCore.metricsSaveType (TargetCore.eTarget.WinPrinter);

            crntToolResetTarget ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a r g e t W i n P r i n t e r C o n f i g u r e _ C l i c k      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Target | Printer | Configure' item is selected.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void targetWinPrinterConfigure_Click (object sender,
                                                      RoutedEventArgs e)
        {
            TargetWinPrintConfig targetWinPrintConfig =
                new TargetWinPrintConfig ();

            Nullable<Boolean> dialogResult = targetWinPrintConfig.ShowDialog ();

            if (dialogResult == true)
            {
                menuItemTargetFile.IsChecked = false;
                menuItemTargetNetPrinter.IsChecked = false;
                menuItemTargetWinPrinter.IsChecked = true;

                TargetCore.metricsSaveType (TargetCore.eTarget.WinPrinter);

                crntToolResetTarget ();
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t o o l F o n t S a m p l e _ S e l e c t e d                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Font Sample' item is selected.                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void toolFontSample_Selected(object sender, RoutedEventArgs e)
        {
            crntToolSaveMetrics ();
            crntToolUncheckAll ();

            menuItemToolFontSample.IsChecked = true;

            _crntToolId = ToolCommonData.eToolIds.FontSample;

            _subFormToolFontSample = new ToolFontSample (ref _crntPDL);

            TargetCore.metricsLoadFileCapt (_crntToolId, _crntSubId, _crntPDL);

            object content = _subFormToolFontSample.Content;

            _subFormToolFontSample.Content = null;
            _subFormToolFontSample.Close ();

            grid1.Children.Clear ();
            grid1.Children.Add (content as UIElement);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t o o l F o r m S a m p l e _ S e l e c t e d                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Form Sample' item is selected.                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void toolFormSample_Selected(object sender, RoutedEventArgs e)
        {
            crntToolSaveMetrics ();
            crntToolUncheckAll ();

            menuItemToolFormSample.IsChecked = true;

            _crntToolId = ToolCommonData.eToolIds.FormSample;

            _subFormToolFormSample = new ToolFormSample (ref _crntPDL);

            TargetCore.metricsLoadFileCapt (_crntToolId, _crntSubId, _crntPDL);

            object content = _subFormToolFormSample.Content;

            _subFormToolFormSample.Content = null;
            _subFormToolFormSample.Close ();

            grid1.Children.Clear ();
            grid1.Children.Add (content as UIElement);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t o o l I m a g e B i t m a p _ S e l e c t e d                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Image Bitmap' item is selected.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void toolImageBitmap_Selected(object sender,
                                              RoutedEventArgs e)
        {
            crntToolSaveMetrics ();
            crntToolUncheckAll ();

            menuItemToolImageBitmap.IsChecked = true;

            _crntToolId = ToolCommonData.eToolIds.ImageBitmap;

            _subFormToolImageBitmap = new ToolImageBitmap (ref _crntPDL);

            TargetCore.metricsLoadFileCapt (_crntToolId, _crntSubId, _crntPDL);

            object content = _subFormToolImageBitmap.Content;

            _subFormToolImageBitmap.Content = null;
            _subFormToolImageBitmap.Close();

            grid1.Children.Clear();
            grid1.Children.Add(content as UIElement);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t o o l M a k e O v e r l a y _ S e l e c t e d                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Make Overlay' item is selected.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void toolMakeOverlay_Selected(object sender,
                                              RoutedEventArgs e)
        {
            crntToolSaveMetrics ();
            crntToolUncheckAll ();

            menuItemToolMakeOverlay.IsChecked = true;

            _crntToolId = ToolCommonData.eToolIds.MakeOverlay;

            _subFormToolMakeOverlay = new ToolMakeOverlay (ref _crntPDL);

            TargetCore.metricsLoadFileCapt (_crntToolId, _crntSubId, _crntPDL);

            object content = _subFormToolMakeOverlay.Content;

            _subFormToolMakeOverlay.Content = null;
            _subFormToolMakeOverlay.Close ();

            grid1.Children.Clear ();
            grid1.Children.Add (content as UIElement);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t o o l M i s c S a m p l e s _ S e l e c t e d                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Misc Samples' item is selected.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void toolMiscSamples_Selected (object sender,
                                              RoutedEventArgs e)
        {
            crntToolSaveMetrics ();
            crntToolUncheckAll ();

            menuItemToolMiscSamples.IsChecked = true;

            _crntToolId = ToolCommonData.eToolIds.MiscSamples;

            _subFormToolMiscSamples = new ToolMiscSamples(ref _crntPDL,
                                                          ref _crntSubId);

            TargetCore.metricsLoadFileCapt (_crntToolId, _crntSubId, _crntPDL);

            object content = _subFormToolMiscSamples.Content;

            _subFormToolMiscSamples.Content = null;
            _subFormToolMiscSamples.Close ();

            grid1.Children.Clear ();
            grid1.Children.Add (content as UIElement);
        }
        /*
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t o o l P a t t e r n G e n e r a t e _ S e l e c t e d            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Pattern Generate' item is selected.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void toolPatternGenerate_Selected (
            object sender,
            RoutedEventArgs e)
        {
            crntToolSaveMetrics();
            crntToolUncheckAll();

            menuItemToolPatternGenerate.IsChecked = true;

            _crntToolId = ToolCommonData.eToolIds.PatternGenerate;

            _subFormToolPatternGenerate = new ToolPatternGenerate(ref _crntPDL);

            TargetCore.metricsLoadFile(_crntToolId, _crntSubId, _crntPDL);

            object content = _subFormToolPatternGenerate.Content;

            _subFormToolPatternGenerate.Content = null;
            _subFormToolPatternGenerate.Close();

            grid1.Children.Clear();
            grid1.Children.Add(content as UIElement);
        }
        */
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t o o l P r i n t A r e a _ S e l e c t e d                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Print Area' item is selected.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void toolPrintArea_Selected(object sender, RoutedEventArgs e)
        {
            crntToolSaveMetrics ();
            crntToolUncheckAll ();

            menuItemToolPrintArea.IsChecked = true;

            _crntToolId = ToolCommonData.eToolIds.PrintArea;

            _subFormToolPrintArea = new ToolPrintArea (ref _crntPDL);

            TargetCore.metricsLoadFileCapt (_crntToolId, _crntSubId, _crntPDL);

            object content = _subFormToolPrintArea.Content;

            _subFormToolPrintArea.Content = null;
            _subFormToolPrintArea.Close();

            grid1.Children.Clear();
            grid1.Children.Add(content as UIElement);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t o o l P r i n t L a n g _ S e l e c t e d                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Print Languages' item is selected.                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void toolPrintLang_Selected(object sender,
                                            RoutedEventArgs e)
        {
            crntToolSaveMetrics ();
            crntToolUncheckAll ();

            menuItemToolPrintLang.IsChecked = true;

            _crntToolId = ToolCommonData.eToolIds.PrintLang;

            _subFormToolPrintLang = new ToolPrintLang (ref _crntPDL);

            TargetCore.metricsLoadFileCapt (_crntToolId, _crntSubId, _crntPDL);

            object content = _subFormToolPrintLang.Content;

            _subFormToolPrintLang.Content = null;
            _subFormToolPrintLang.Close ();

            grid1.Children.Clear ();
            grid1.Children.Add (content as UIElement);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t o o l P r n A n a l y s e _ S e l e c t e d                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Prn Analyse' item is selected.                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void toolPrnAnalyse_Selected(object sender,
                                             RoutedEventArgs e)
        {
            crntToolSaveMetrics ();
            crntToolUncheckAll ();

            menuItemToolPrnAnalyse.IsChecked = true;

            _crntToolId = ToolCommonData.eToolIds.PrnAnalyse;

            _subFormToolPrnAnalyse = new ToolPrnAnalyse (ref _crntPDL);

            TargetCore.metricsLoadFileCapt (_crntToolId, _crntSubId, _crntPDL);

            object content = _subFormToolPrnAnalyse.Content;

            _subFormToolPrnAnalyse.Content = null;
            _subFormToolPrnAnalyse.Close ();

            grid1.Children.Clear ();
            grid1.Children.Add (content as UIElement);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t o o l P r n P r i n t _ S e l e c t e d                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Prn Print' item is selected.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void toolPrnPrint_Selected(object sender, RoutedEventArgs e)
        {
            crntToolSaveMetrics ();
            crntToolUncheckAll ();

            menuItemToolPrnPrint.IsChecked = true;

            _crntToolId = ToolCommonData.eToolIds.PrnPrint;

            _subFormToolPrnPrint = new ToolPrnPrint (ref _crntPDL);

            TargetCore.metricsLoadFileCapt (_crntToolId, _crntSubId, _crntPDL);

            object content = _subFormToolPrnPrint.Content;

            _subFormToolPrnPrint.Content = null;
            _subFormToolPrnPrint.Close ();

            grid1.Children.Clear ();
            grid1.Children.Add (content as UIElement);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t o o l S o f t F o n t G e n e r a t e _ S e l e c t e d          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Soft Font Generate' item is selected.             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void toolSoftFontGenerate_Selected(object sender,
                                                   RoutedEventArgs e)
        {
            crntToolSaveMetrics ();
            crntToolUncheckAll ();

            menuItemToolSoftFontGenerate.IsChecked = true;

            _crntToolId = ToolCommonData.eToolIds.SoftFontGenerate;

            _subFormToolSoftFontGenerate =
                new ToolSoftFontGenerate (ref _crntPDL);

            TargetCore.metricsLoadFileCapt (_crntToolId, _crntSubId, _crntPDL);

            object content = _subFormToolSoftFontGenerate.Content;

            _subFormToolSoftFontGenerate.Content = null;
            _subFormToolSoftFontGenerate.Close ();

            grid1.Children.Clear ();
            grid1.Children.Add (content as UIElement);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t o o l S t a t u s R e a d b a c k _ S e l e c t e d              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'StatusReadback' item is selected.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void toolStatusReadback_Selected(object sender,
                                                 RoutedEventArgs e)
        {
            crntToolSaveMetrics ();
            crntToolUncheckAll ();

            menuItemToolStatusReadback.IsChecked = true;

            _crntToolId = ToolCommonData.eToolIds.StatusReadback;

            _subFormToolStatusReadback = new ToolStatusReadback (ref _crntPDL);

            TargetCore.metricsLoadFileCapt (_crntToolId, _crntSubId, _crntPDL);

            object content = _subFormToolStatusReadback.Content;

            _subFormToolStatusReadback.Content = null;
            _subFormToolStatusReadback.Close();
            
            grid1.Children.Clear();
            grid1.Children.Add(content as UIElement);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t o o l S y m b o l S e t G e n e r a t e _ S e l e c t e d        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Symbol Set Generate' item is selected.            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void toolSymbolSetGenerate_Selected (object sender,
                                                     RoutedEventArgs e)
        {
            crntToolSaveMetrics ();
            crntToolUncheckAll ();

            menuItemToolSymbolSetGenerate.IsChecked = true;

            _crntToolId = ToolCommonData.eToolIds.SymbolSetGenerate;

            _subFormToolSymbolSetGenerate =
                new ToolSymbolSetGenerate (ref _crntPDL);

            TargetCore.metricsLoadFileCapt (_crntToolId, _crntSubId, _crntPDL);

            object content = _subFormToolSymbolSetGenerate.Content;

            _subFormToolSymbolSetGenerate.Content = null;
            _subFormToolSymbolSetGenerate.Close ();

            grid1.Children.Clear ();
            grid1.Children.Add (content as UIElement);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t o o l T r a y M a p _ S e l e c t e d                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Tray Map' item is selected.                       //
        //                                                                    //
        //--------------------------------------------------------------------//
        
        private void toolTrayMap_Selected(object sender, RoutedEventArgs e)
        {
            crntToolSaveMetrics ();
            crntToolUncheckAll ();

            menuItemToolTrayMap.IsChecked = true;

            _crntToolId = ToolCommonData.eToolIds.TrayMap;

            _subFormToolTrayMap = new ToolTrayMap (ref _crntPDL);

            TargetCore.metricsLoadFileCapt (_crntToolId, _crntSubId, _crntPDL);

            object content = _subFormToolTrayMap.Content;  
            
            _subFormToolTrayMap.Content = null;    
            _subFormToolTrayMap.Close();
            
            grid1.Children.Clear();
            grid1.Children.Add(content as UIElement);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t o o l X X X D i a g s _ S e l e c t e d                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'XXX Diags' item is selected.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void toolXXXDiags_Selected (object sender, RoutedEventArgs e)
        {
            crntToolSaveMetrics ();
            crntToolUncheckAll ();

            menuItemToolTrayMap.IsChecked = true;

            _crntToolId = ToolCommonData.eToolIds.XXXDiags;

            _subFormToolXXXDiags = new ToolXXXDiags (ref _crntPDL);

            TargetCore.metricsLoadFileCapt (_crntToolId, _crntSubId, _crntPDL);

            object content = _subFormToolXXXDiags.Content;

            _subFormToolXXXDiags.Content = null;
            _subFormToolXXXDiags.Close ();

            grid1.Children.Clear ();
            grid1.Children.Add (content as UIElement);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // W i n d o w _ C l o s i n g                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store target and window metrics.                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void Window_Closing(object sender,
                                    System.ComponentModel.CancelEventArgs e)
        {
            //----------------------------------------------------------------//
            //                                                                //
            // Save data from last active subform.                            //
            //                                                                //
            //----------------------------------------------------------------//

            crntToolSaveMetrics ();

            //----------------------------------------------------------------//
            //                                                                //
            // Store current window metrics.                                  //
            //                                                                //
            //----------------------------------------------------------------//

            MainFormPersist.saveWindowData(
                (Int32)this.Left,
                (Int32)this.Top,
                (Int32)this.Height,
                (Int32)this.Width,
                (Int32) (MainFormData.WindowScale * 100));
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // z o o m S l i d e r _ V a l u e C h a n g e d                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'zoomSlider' object is changed.                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void zoomSlider_ValueChanged(
            object sender,
            RoutedPropertyChangedEventArgs<double> e)
        {
            MainFormData.WindowScale = zoomSlider.Value;
        }

        /*
        private void Form1_Load(object sender, EventArgs e)
        {
            // set F1 help topic for this form
            helpProvider1.HelpNamespace = Application.StartupPath + @"\" + sHTMLHelpFileName;
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, @"/Garden/garden.htm");
            helpProvider1.SetHelpNavigator(this.btnStart, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this.btnStart, @"/Garden/flowers.htm");
            helpProvider1.SetHelpNavigator(this.btnExit, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this.btnExit, @"/Garden/tree.htm");
            helpProvider1.SetHelpNavigator(this.chkMain, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this.chkMain, @"/HTMLHelp_Examples/jump_to_anchor.htm#AnchorSample");
        }
        */ 
    }
}
