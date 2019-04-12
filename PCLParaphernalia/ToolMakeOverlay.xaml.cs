using Microsoft.Win32;
using System;
using System.Data;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PCLParaphernalia
{
    /// <summary>
    /// Interaction logic for ToolMakeOverlay.xaml
    /// 
    /// Class handles the Make Overlay tool form.
    /// 
    /// © Chris Hutchinson 2010-2017
    /// 
    /// </summary>
    
    [System.Reflection.ObfuscationAttribute(Feature = "renaming",
                                            ApplyToMembers = true)]

    public partial class ToolMakeOverlay : Window
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private const UInt16 _defaultPCLMacroId = 1;

        private const String _defaultPCLXLSStreamName = "Stream 001";

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private ToolCommonData.ePrintLang _crntPDL;

        private String _prnFilename;
        private String _ovlFilenamePCL;
        private String _ovlFilenamePCLXL;

        private String _streamNamePCLXL;

        private Int32 _macroIdPCL;

        private PrnParseOptions _options;

        private PropertyInfo[] _stdClrsPropertyInfo;

        private Int32 _ctClrMapStdClrs;

        private Int32[] _indxClrMapBack;
        private Int32[] _indxClrMapFore;

        private Boolean _flagClrMapUseClr;

        private DataTable _tableProgress;

        private Boolean _flagRestoreCursorPCL;
        private Boolean _flagRestoreGSPCLXL;

        private Boolean _flagOvlEncPCL;
        private Boolean _flagOvlEncPCLXL;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // T o o l M a k e O v e r l a y                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public ToolMakeOverlay (ref ToolCommonData.ePrintLang crntPDL)
        {
            InitializeComponent();

            initialise();

            crntPDL = _crntPDL;
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
            PrnParse parseFile = new PrnParse (PrnParse.eParseType.MakeOverlay,
                                               0);

            _tableProgress.Clear ();

            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                parseFile.makeOverlayPCL (_prnFilename,
                                           ref _ovlFilenamePCL,
                                           _options,
                                           _tableProgress,
                                           _flagRestoreCursorPCL,
                                           _flagOvlEncPCL,
                                           _macroIdPCL);

                txtPCLOvlFilename.Text = _ovlFilenamePCL;
            }
            else if (_crntPDL == ToolCommonData.ePrintLang.PCLXL)
            {
                parseFile.makeOverlayPCLXL (_prnFilename,
                                           ref _ovlFilenamePCLXL,
                                           _options,
                                           _tableProgress,
                                           _flagRestoreGSPCLXL,
                                           _flagOvlEncPCLXL,
                                           _streamNamePCLXL);

                txtPCLXLOvlFilename.Text = _ovlFilenamePCLXL;
            }

            grpProgress.Visibility = Visibility.Visible;
            btnSaveReport.Visibility = Visibility.Visible;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b t n P C L O v l F i l e n a m e B r o w s e _ C l i c k          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the target PCL overlay file 'Browse' button is         //
        // clicked.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void btnPCLOvlFilenameBrowse_Click(object sender,
                                                    RoutedEventArgs e)
        {
            Boolean selected;

            String filename = _ovlFilenamePCL;

            selected = selectOvlFilePCL (ref filename);

            if (selected)
            {
                _ovlFilenamePCL = filename;
                txtPCLOvlFilename.Text = _ovlFilenamePCL;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b t n P C L X L O v l F i l e n a m e B r o w s e _ C l i c k      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the target PCLXL overlay file 'Browse' button is       //
        // clicked.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void btnPCLXLOvlFilenameBrowse_Click(object sender,
                                                    RoutedEventArgs e)
        {
            Boolean selected;

            String filename = _ovlFilenamePCLXL;

            selected = selectOvlFilePCLXL (ref filename);

            if (selected)
            {
                _ovlFilenamePCLXL = filename;
                txtPCLXLOvlFilename.Text = _ovlFilenamePCLXL;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b t n P r n F i l e n a m e B r o w s e _ C l i c k                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the source print file 'Browse' button is clicked.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void btnPrnFilenameBrowse_Click(object sender,
                                                RoutedEventArgs e)
        {
            Boolean selected;

            String filename = _prnFilename;

            selected = selectPrnFile (ref filename);

            if (selected)
            {
                _prnFilename = filename;
                txtPrnFilename.Text = _prnFilename;

                grpOverlay.Visibility = Visibility.Hidden;
                grpProgress.Visibility = Visibility.Hidden;

                btnGenerate.Visibility = Visibility.Hidden;
                btnSaveReport.Visibility = Visibility.Hidden;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b t n S a v e R e p o r t _ C l i c k                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Save report' button is clicked.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void btnSaveReport_Click(object sender, RoutedEventArgs e)
        {
            Boolean flagOffsetHex,
                    flagOptRptWrap = false;

            ReportCore.eRptFileFmt rptFileFmt = ReportCore.eRptFileFmt.NA;
            ReportCore.eRptChkMarks rptChkMarks = ReportCore.eRptChkMarks.NA;

            if (_options.IndxGenOffsetFormat ==
                PrnParseConstants.eOptOffsetFormats.Decimal)
                flagOffsetHex = false;
            else
                flagOffsetHex = true;

            TargetCore.metricsReturnFileRpt (ToolCommonData.eToolIds.MakeOverlay,
                                             ref rptFileFmt,
                                             ref rptChkMarks,
                                             ref flagOptRptWrap);

            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
                ToolMakeOverlayReport.generate(rptFileFmt,
                                               _tableProgress,
                                                _prnFilename,
                                                _ovlFilenamePCL,
                                                flagOffsetHex,
                                                _options);
            else
                ToolMakeOverlayReport.generate (rptFileFmt,
                                               _tableProgress,
                                                _prnFilename,
                                                _ovlFilenamePCLXL,
                                                flagOffsetHex,
                                                _options);

            btnSaveReport.Visibility = Visibility.Visible;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b t n S c a n _ C l i c k                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Scan' button is clicked.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void btnScan_Click(object sender, RoutedEventArgs e)
        {
            Boolean validPDL = false;

            Int32 ptr;

            ToolCommonData.ePrintLang scanPDL;

            PrnParse parseFile = new PrnParse (PrnParse.eParseType.ScanForPDL,
                                               0);

            _tableProgress.Clear ();

            scanPDL = ToolCommonData.ePrintLang.Unknown;

            parseFile.makeOverlayScan (_prnFilename,
                                       _options,
                                       ref scanPDL);

            ptr = _prnFilename.LastIndexOf (".");

            if (ptr <= 0)
                ptr = _prnFilename.Length;

            if ((scanPDL == ToolCommonData.ePrintLang.PCL)
                                  ||
                (scanPDL == ToolCommonData.ePrintLang.HPGL2))
            {
                validPDL = true;
                _crntPDL = ToolCommonData.ePrintLang.PCL;

                tabPCL.IsEnabled = true;
                tabPCL.IsSelected = true;

                tabPCLXL.IsEnabled = false;
                
                _ovlFilenamePCL = _prnFilename.Substring (0, ptr) + ".ovl";

                txtPCLOvlFilename.Text = _ovlFilenamePCL;
            }
            else if ((scanPDL == ToolCommonData.ePrintLang.PCLXL)
                                  ||
                (scanPDL == ToolCommonData.ePrintLang.XL2HB))
            {
                validPDL = true;
                _crntPDL = ToolCommonData.ePrintLang.PCLXL;

                tabPCLXL.IsEnabled = true;
                tabPCLXL.IsSelected = true;

                tabPCL.IsEnabled = false;

                _ovlFilenamePCLXL = _prnFilename.Substring (0, ptr) + ".ovx";

                txtPCLXLOvlFilename.Text = _ovlFilenamePCLXL;
            }

            if (validPDL)
            {
                grpOverlay.Visibility = Visibility.Visible;
                btnGenerate.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show ("Source print file appears to be a '" +
                                 scanPDL.ToString () +
                                "' printfile.\n\n" +
                                "This tool does not support the generation of an " +
                                "overlay from such a print file.",
                                "Page Description Language not supported",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L R e s t o r e C u r s o r _ C h e c k e d              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL 'save/restore cursor position' checkbox has been checked.  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLRestoreCursor_Checked(object sender,
                                                  RoutedEventArgs e)
        {
            _flagRestoreCursorPCL = true;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L R e s t o r e C u r s o r _ U n c h e c k e d          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL 'save/restore cursor position' checkbox has been           //
        // unchecked.                                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLRestoreCursor_Unchecked(object sender,
                                                    RoutedEventArgs e)
        {
            _flagRestoreCursorPCL = false;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L X L R e s t o r e G S _ C h e c k e d                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCLXL 'save/restore graphics state' checkbox has been checked. //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLXLRestoreGS_Checked(object sender,
                                              RoutedEventArgs e)
        {
            _flagRestoreGSPCLXL = true;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L X L R e s t o r e G S_ U n c h e c k e d               //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCLXL 'save/restore graphics state' checkbox has been          //
        // unchecked.                                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLXLRestoreGS_Unchecked(object sender,
                                                RoutedEventArgs e)
        {
            _flagRestoreGSPCLXL = false;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d g P r o g r e s s _ A u t o G e n e r a t i n g C o l u m n      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Modifies column captions for 'Analysis' grid.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void dgProgress_AutoGeneratingColumn(
            object sender,
            DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();

            if (headername == PrnParseConstants.cRptA_colName_RowType)
            {
                e.Cancel = true;
            }
            else if (headername == PrnParseConstants.cRptA_colName_Offset)
            {
                if (_options.IndxGenOffsetFormat ==
                    PrnParseConstants.eOptOffsetFormats.Decimal)
                    e.Column.Header = headername + ": dec";
                else
                    e.Column.Header = headername + ": hex";
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d g p r o g r e s s _ L o a d i n g R o w                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Modifies 'Progress' grid row when loading.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void dgProgress_LoadingRow (object sender,
                                            DataGridRowEventArgs e)
        {
            DataRowView rowView = (DataRowView)e.Row.Item;
            DataRow row = rowView.Row;

            Boolean nullRow =
                row.IsNull (PrnParseConstants.cRptA_colName_RowType);

            if (!nullRow)
            {
                if (_flagClrMapUseClr)
                {
                    Int32 indxClrBack,
                          indxClrFore;

                    Int32 rowType;

                    Color clrBack = new Color (),
                          clrFore = new Color ();

                    SolidColorBrush brushBack = new SolidColorBrush (),
                                    brushFore = new SolidColorBrush ();

                    rowType = (Int32)row[PrnParseConstants.cRptA_colName_RowType];

                    indxClrBack = _indxClrMapBack[rowType];
                    indxClrFore = _indxClrMapFore[rowType];

                    clrBack = (Color)(_stdClrsPropertyInfo[indxClrBack]
                        as PropertyInfo).GetValue (null, null);
                    clrFore = (Color)(_stdClrsPropertyInfo[indxClrFore]
                        as PropertyInfo).GetValue (null, null);

                    brushBack.Color = clrBack;
                    brushFore.Color = clrFore;

                    e.Row.Background = brushBack;
                    e.Row.Foreground = brushFore;
                }
                else
                {
                    e.Row.Background = Brushes.White;
                    e.Row.Foreground = Brushes.Black;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g i v e C r n t P D L                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void giveCrntPDL(ref ToolCommonData.ePrintLang crntPDL)
        {
            crntPDL = _crntPDL;
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
            //----------------------------------------------------------------//
            //                                                                //
            // Populate form.                                                 //
            //                                                                //
            //----------------------------------------------------------------//

            btnGenerate.Content = "Generate overlay";

            _options = new PrnParseOptions (); // need a dummy one //

            grpOverlay.Visibility = Visibility.Hidden;
            grpProgress.Visibility = Visibility.Hidden;

            btnGenerate.Visibility = Visibility.Hidden;
            btnSaveReport.Visibility = Visibility.Hidden;

            //----------------------------------------------------------------//
            //                                                                //
            // Reinstate settings from persistent storage.                    //
            //                                                                //
            //----------------------------------------------------------------//

            metricsLoad();

            txtPrnFilename.Text   = _prnFilename;

            txtPCLMacroId.Text = _macroIdPCL.ToString ();
            txtPCLXLStreamName.Text = _streamNamePCLXL;

            rbPCLOvlEnc.IsChecked = _flagOvlEncPCL;  
            rbPCLXLOvlEnc.IsChecked = _flagOvlEncPCLXL;

            chkPCLRestoreCursor.IsChecked = _flagRestoreCursorPCL;  
            chkPCLXLRestoreGS.IsChecked     = _flagRestoreGSPCLXL;

            if (_flagOvlEncPCL)
            {
                lbPCLMacroId.Visibility  = Visibility.Visible;
                txtPCLMacroId.Visibility = Visibility.Visible;
            }
            else
            {
                lbPCLMacroId.Visibility  = Visibility.Hidden;
                txtPCLMacroId.Visibility = Visibility.Hidden;
            }

            if (_flagOvlEncPCLXL)
            {
                lbPCLXLStreamName.Visibility  = Visibility.Visible;
                txtPCLXLStreamName.Visibility = Visibility.Visible;
            }
            else
            {
                lbPCLXLStreamName.Visibility  = Visibility.Hidden;
                txtPCLXLStreamName.Visibility = Visibility.Hidden;
            }

            //----------------------------------------------------------------//

            txtPCLOvlFilename.Text = _ovlFilenamePCL;
            txtPCLXLOvlFilename.Text = _ovlFilenamePCLXL;

            //----------------------------------------------------------------//

            Int32 ctRowTypes = PrnParseRowTypes.getCount ();

            _indxClrMapBack = new Int32[ctRowTypes];
            _indxClrMapFore = new Int32[ctRowTypes];

            _options.getOptClrMap (ref _flagClrMapUseClr,
                                   ref _indxClrMapBack,
                                   ref _indxClrMapFore);

            _options.getOptClrMapStdClrs (ref _ctClrMapStdClrs,
                                          ref _stdClrsPropertyInfo);

            //----------------------------------------------------------------//

            initialiseGridProgress ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n i t i a l i s e G r i d P r o g r e s s                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initialises 'Progress' dataset and associate with grid.            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void initialiseGridProgress()
        {
            _tableProgress = new DataTable ("Progress");

            _tableProgress.Columns.Add (PrnParseConstants.cRptA_colName_RowType,
                                        typeof(Int32));
            _tableProgress.Columns.Add (PrnParseConstants.cRptA_colName_Action,
                                        typeof (String));
            _tableProgress.Columns.Add (PrnParseConstants.cRptA_colName_Offset,
                                        typeof (String));
            _tableProgress.Columns.Add (PrnParseConstants.cRptA_colName_Type,
                                        typeof (String));
            _tableProgress.Columns.Add (PrnParseConstants.cRptA_colName_Seq,
                                        typeof (String));
            _tableProgress.Columns.Add (PrnParseConstants.cRptA_colName_Desc,
                                        typeof (String));

            dgProgress.DataContext = _tableProgress;  // bind to grid
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
            ToolMakeOverlayPersist.loadDataCommon (ref _prnFilename);

            ToolMakeOverlayPersist.loadDataPCL (ref _ovlFilenamePCL,
                                                 ref _flagRestoreCursorPCL, 
                                                 ref _flagOvlEncPCL,
                                                 ref _macroIdPCL);

            ToolMakeOverlayPersist.loadDataPCLXL (ref _ovlFilenamePCLXL,
                                                 ref _flagRestoreGSPCLXL, 
                                                 ref _flagOvlEncPCLXL,
                                                 ref _streamNamePCLXL);
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
            ToolMakeOverlayPersist.saveDataCommon (_prnFilename);

            ToolMakeOverlayPersist.saveDataPCL (_ovlFilenamePCL,
                                                 _flagRestoreCursorPCL,
                                                 _flagOvlEncPCL,
                                                 _macroIdPCL);

            ToolMakeOverlayPersist.saveDataPCLXL (_ovlFilenamePCLXL,
                                                 _flagRestoreGSPCLXL,
                                                 _flagOvlEncPCLXL,
                                                 _streamNamePCLXL);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b P C L O v l E n c _ C l i c k                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCL 'encapsulated' overlay radio button is         //
        // clicked.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbPCLOvlEnc_Click(object sender, RoutedEventArgs e)
        {
            _flagOvlEncPCL = true;

            lbPCLMacroId.Visibility = Visibility.Visible;
            txtPCLMacroId.Visibility = Visibility.Visible;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b P C L O v l R a w _ C l i c k                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCL 'raw' overlay radio button is clicked.         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbPCLOvlRaw_Click(object sender, RoutedEventArgs e)
        {
            _flagOvlEncPCL = false;

            lbPCLMacroId.Visibility = Visibility.Hidden;
            txtPCLMacroId.Visibility = Visibility.Hidden;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b P C L X L O v l E n c _ C l i c k                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCLXL 'encapsulated' overlay radio button is       //
        // clicked.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbPCLXLOvlEnc_Click(object sender, RoutedEventArgs e)
        {
            _flagOvlEncPCLXL = true;

            lbPCLXLStreamName.Visibility = Visibility.Visible;
            txtPCLXLStreamName.Visibility = Visibility.Visible;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b P C L X L O v l R a w _ C l i c k                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCLXL 'raw' overlay radio button is clicked.       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbPCLXLOvlRaw_Click(object sender, RoutedEventArgs e)
        {
            _flagOvlEncPCLXL = false;

            lbPCLXLStreamName.Visibility = Visibility.Hidden;
            txtPCLXLStreamName.Visibility = Visibility.Hidden;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e s e t T a r g e t                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Reset the target type.                                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void resetTarget()
        {
            // Dummy method
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e l e c t O v l F i l e P C L                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initiate 'open file' dialogue for the PCL overlay file.            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean selectOvlFilePCL (ref String selectedName)
        {
            OpenFileDialog openDialog = ToolCommonFunctions.createOpenFileDialog(selectedName);

            openDialog.CheckFileExists = false;
            openDialog.Filter = "Overlay Files|*.ovl; *.OVL" +
                                "|All files|*.*";

            Nullable<Boolean> dialogResult = openDialog.ShowDialog();

            if (dialogResult == true)
                selectedName = openDialog.FileName;

            return dialogResult == true;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e l e c t O v l F i l e P C L X L                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initiate 'open file' dialogue for the PCLXL overlay file.          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean selectOvlFilePCLXL(ref String selectedName)
        {
            OpenFileDialog openDialog = ToolCommonFunctions.createOpenFileDialog(selectedName);

            openDialog.CheckFileExists = false;
            openDialog.Filter = "Overlay Files|*.ovx; *.OVX" +
                                "|All files|*.*";


            Nullable<Boolean> dialogResult = openDialog.ShowDialog();

            if (dialogResult == true)
                selectedName = openDialog.FileName;

            return dialogResult == true;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e l e c t P r n F i l e                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initiate 'open file' dialogue.                                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean selectPrnFile (ref String selectedName)
        {
            OpenFileDialog openDialog = ToolCommonFunctions.createOpenFileDialog(selectedName);

            openDialog.Filter = "Print Files|*.prn; *.PRN" +
                                "|All files|*.*";


            Nullable<Boolean> dialogResult = openDialog.ShowDialog();

            if (dialogResult == true)
                selectedName = openDialog.FileName;

            return dialogResult == true;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t P C L M a c r o I d _ L o s t F o c u s                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // PCL (target) macro identifier has lost focus.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtPCLMacroId_LostFocus(object sender,
                                              RoutedEventArgs e)
        {
            validatePCLMacroId (true, ref _macroIdPCL);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t P C L M a c r o I d _ T e x t C h a n g e d                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // PCL (target) macro identifier text has changed.                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtPCLMacroId_TextChanged(object sender,
                                                TextChangedEventArgs e)
        {
            validatePCLMacroId (false, ref _macroIdPCL);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t P C L O v l F i l e n a m e _ L o s t F o c u s              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // PCL Overlay file 'Filename' text has lost focus.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtPCLOvlFilename_LostFocus(object sender,
                                                  RoutedEventArgs e)
        {
            _ovlFilenamePCL = txtPCLOvlFilename.Text;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t P C L X L O v l F i l e n a m e _ L o s t F o c u s          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // PCLXL Overlay file 'Filename' text has lost focus.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtPCLXLOvlFilename_LostFocus(object sender,
                                                  RoutedEventArgs e)
        {
            _ovlFilenamePCLXL = txtPCLXLOvlFilename.Text;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t P C L X L S t r e a m N a m e _ L o s t F o c u s            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // PCLXL (target) stream name has lost focus.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtPCLXLStreamName_LostFocus(object sender,
                                                 RoutedEventArgs e)
        {
            _streamNamePCLXL = txtPCLXLStreamName.Text; // TODO
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t P C L X L S t r e a m _ T e x t C h a n g e d                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // PCLXL (target) stream name text has changed.                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtPCLXLStreamName_TextChanged(object sender,
                                                   TextChangedEventArgs e)
        {
            _streamNamePCLXL = txtPCLXLStreamName.Text; // TODO
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t P r n F i l e n a m e _ L o s t F o c u s                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Print file 'Filename' text has lost focus.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtPrnFilename_LostFocus(object sender,
                                              RoutedEventArgs e)
        {
            _prnFilename = txtPrnFilename.Text;

            grpOverlay.Visibility = Visibility.Hidden;
            grpProgress.Visibility = Visibility.Hidden;

            btnGenerate.Visibility = Visibility.Hidden;
            btnSaveReport.Visibility = Visibility.Hidden;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // v a l i d a t e P C L M a c r o I d                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Validate PCL MacroId number.                                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean validatePCLMacroId(Boolean lostFocusEvent,
                                            ref Int32 macroId)
        {
            const UInt16 minVal = 0;
            const UInt16 maxVal = 32767;
            const UInt16 defVal = _defaultPCLMacroId;

            UInt16 value;

            Boolean OK = true;

            String crntText = txtPCLMacroId.Text;

            OK = UInt16.TryParse (crntText, out value);

            if (OK)
            {
                if ((value < minVal) || (value > maxVal))
                {
                    OK = false;
                }
            }

            if (OK)
            {
                macroId = value;
            }
            else
            {
                if (lostFocusEvent)
                {
                    String newText = defVal.ToString ();

                    MessageBox.Show ("Macro identifier '" + crntText +
                                    "' is invalid.\n\n" +
                                    "Value will be reset to default '" +
                                    newText + "'",
                                    "PCL macro identifier invalid",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);

                    macroId = defVal;

                    txtPCLMacroId.Text = newText;
                }
                else
                {
                    MessageBox.Show ("Macro identifier '" + crntText +
                                    "' is invalid.\n\n" +
                                    "Valid range is :\n\t" +
                                    minVal + " <= value <= " + maxVal,
                                    "PCL macro identifier invalid",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);

                    txtPCLMacroId.Focus ();
                    txtPCLMacroId.SelectAll ();
                }
            }

            return OK;
        }
    }
}
