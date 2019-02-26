using Microsoft.Win32;
using System;
using System.IO;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// Interaction logic for ToolPrinterInfo.xaml
    /// 
    /// Class handles the PrinterInfo tool form.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>
    
    [System.Reflection.ObfuscationAttribute(Feature = "renaming",
                                            ApplyToMembers = true)]

    public partial class ToolStatusReadback : Window
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private const Int32 _defaultPJLFSCount    = 100;
        private const Int32 _defaultPJLFSEntry    = 1;
        private const Int32 _defaultPJLFSOffset   = 0;
        private const Int32 _defaultPJLFSSize     = 999999;

        private const String _defaultPJLFSPassword = "65535";

        private const String _defaultPJLFSObjPath = "0:\\pcl\\macros\\macro1";
        private const String _defaultPJLFSVolume  = "0:";

        private static PJLCommands.eCmdIndex[] _subsetPJLCommands = 
        {
            PJLCommands.eCmdIndex.DINQUIRE,
            PJLCommands.eCmdIndex.INFO,
            PJLCommands.eCmdIndex.INQUIRE
        };

        private static PJLCommands.eCmdIndex [] _subsetPJLFSCommands = 
        {
            PJLCommands.eCmdIndex.FSAPPEND,
            PJLCommands.eCmdIndex.FSDELETE,
            PJLCommands.eCmdIndex.FSDIRLIST,
            PJLCommands.eCmdIndex.FSDOWNLOAD,
            PJLCommands.eCmdIndex.FSINIT,
            PJLCommands.eCmdIndex.FSMKDIR,
            PJLCommands.eCmdIndex.FSQUERY,
            PJLCommands.eCmdIndex.FSUPLOAD
        };

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private TargetCore.eTarget _targetType;

        private ToolCommonData.ePrintLang _crntPDL;

        PJLCommands.eRequestType _reqTypePJL;
        PJLCommands.eRequestType _reqTypePJLFS;
        PJLCommands.eCmdIndex _cmdIndxPJL;
        PJLCommands.eCmdIndex _cmdIndxPJLFS;

        private Int32 _ctPCLEntityTypes;
        private Int32 _ctPCLLocTypes;
        private Int32 _ctPJLCategories;
        private Int32 _ctPJLCommands;
        private Int32 _ctPJLFSCommands;
        private Int32 _ctPJLVariables;

        private Int32 _indxPCLEntityType;
        private Int32 _indxPCLLocType;
        private Int32 _indxPJLCategory;
        private Int32 _indxPJLCommand;
        private Int32 _indxPJLFSCommand;
        private Int32 _indxPJLVariable;

        private Int32 _valPJLFSOpt1;
        private Int32 _valPJLFSOpt2;

        private static String _reportFilenamePCL;
        private static String _reportFilenamePJL;

        private static String _customCatPJL;
        private static String _customVarPJL;

        private static String _binSrcFilenamePJLFS;
        private static String _binTgtFilenamePJLFS;
        private static String _objPathPJLFS;
        private static String _objVolPJLFS;
        private static String _objDirPJLFS;
        private static String _objFilPJLFS;
        private static String _passwordPJLFS;

        private Boolean _flagPJLFS;
        private Boolean _flagPJLFSSecJob;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // T o o l P r i n t e r I n f o                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public ToolStatusReadback (ref ToolCommonData.ePrintLang crntPDL)
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
        // Called when the 'Generate Test Data' button is clicked.            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                BinaryWriter requestWriter = null;

                //------------------------------------------------------------//
                //                                                            //
                // Generate request data, and write this to a file.           //
                // If Target = File, the file is the nominated file,          //
                // otherwise a temporary file is used.                        //
                //                                                            //
                //------------------------------------------------------------//

                TargetCore.requestStreamOpen (
                    ref requestWriter,
                    ToolCommonData.eToolIds.StatusReadback,
                    ToolCommonData.eToolSubIds.None,
                    _crntPDL);

                if (_crntPDL == ToolCommonData.ePrintLang.PCL)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // PCL Status Readback.                                   //
                    //                                                        //
                    //--------------------------------------------------------//

                    ToolStatusReadbackPCL.generateRequest (requestWriter,
                                                           _indxPCLEntityType,
                                                           _indxPCLLocType);
                }
                else if (!_flagPJLFS)
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // PJL Status Readback.                                   //
                    //                                                        //
                    //--------------------------------------------------------//

                    ToolStatusReadbackPJL.generateRequest (requestWriter,
                                                           _cmdIndxPJL,
                                                           _indxPJLCategory,
                                                           _indxPJLVariable,
                                                           _customCatPJL,
                                                           _customVarPJL);
                }
                else
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // PJL File System commands.                              //
                    //                                                        //
                    //--------------------------------------------------------//

                    String path;

                    if (_reqTypePJLFS == PJLCommands.eRequestType.FSInit)
                        path = _objVolPJLFS;
                    else if ((_reqTypePJLFS == PJLCommands.eRequestType.FSDirList) ||
                             (_reqTypePJLFS == PJLCommands.eRequestType.FSMkDir))
                        path = _objDirPJLFS;
                    else
                        path = _objPathPJLFS;

                    ToolStatusReadbackPJLFS.generateRequest (requestWriter,
                                                             _cmdIndxPJLFS,
                                                             _flagPJLFSSecJob,
                                                             _passwordPJLFS,
                                                             path,
                                                             _binSrcFilenamePJLFS,
                                                             _valPJLFSOpt1,
                                                             _valPJLFSOpt2);
                }

                //------------------------------------------------------------//
                //                                                            //
                // Send generated request data (read from the temporary file) //
                // to the target device, then (in most cases) read the        //
                // response.                                                  //
                //                                                            //
                //------------------------------------------------------------//

                _targetType = TargetCore.getType ();

                if (_targetType == TargetCore.eTarget.File)
                {
                    TargetCore.requestStreamWrite(false);

                    txtReply.Text = "Request sequence has been saved to" +
                                    " file.\n\n" +
                                    "Specified target of 'File' means that" +
                                    " a reply is not meaningful.";
                }
                else if (_targetType == TargetCore.eTarget.NetPrinter)
                {
                    if (_crntPDL == ToolCommonData.ePrintLang.PCL)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // PCL Status Readback.                               //
                        // Response from printer is expected.                 //
                        //                                                    //
                        //----------------------------------------------------//

                        ToolStatusReadbackPCL.sendRequest();

                        txtReply.Text = ToolStatusReadbackPCL.readResponse ();
                    }
                    else if (!_flagPJLFS)
                    {
                        //----------------------------------------------------//
                        //                                                    //
                        // PJL Status Readback.                               //
                        // Response from printer is expected.                 //
                        //                                                    //
                        //----------------------------------------------------//

                        ToolStatusReadbackPJL.sendRequest();

                        txtReply.Text = ToolStatusReadbackPJL.readResponse ();
                    }
                    else
                    {
                        //--------------------------------------------------------//
                        //                                                        //
                        // PJL File System commands.                              //
                        // Response from printer is expected for some of the      //
                        // commands, but not all.                                 //
                        //                                                        //
                        //--------------------------------------------------------//

                        ToolStatusReadbackPJLFS.sendRequest(_cmdIndxPJLFS);

                        txtReply.Text = ToolStatusReadbackPJLFS.readResponse (
                            _cmdIndxPJLFS,
                            _binTgtFilenamePJLFS);
                    }
                }
                else if (_targetType == TargetCore.eTarget.WinPrinter)
                {
                    txtReply.Text = "This application does not support" +
                                    " Status readback via a Windows printer" +
                                    " instance.\n\n" +
                                    "Choose a network printer Target instead.";
                }
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

            btnSaveReport.Visibility = Visibility.Visible;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b t n P J L F S L o c P a t h B r o w s e _ C l i c k              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'binary source/target' browse button is clicked.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void btnPJLFSLocPathBrowse_Click (object sender, EventArgs e)
        {
            Boolean selected,
                    upload;

            String filename;

            if (_reqTypePJLFS == PJLCommands.eRequestType.FSUpload)
            {
                upload = true;
                filename = _binTgtFilenamePJLFS;
            }
            else
            {
                upload = false;
                filename = _binSrcFilenamePJLFS;
            }

            selected = selectLocBinFile (upload, ref filename);

            if (selected)
            {
                txtPJLFSLocPath.Text = filename;

                if (upload)
                {
                    _binTgtFilenamePJLFS = filename;
                }
                else
                {
                    _binSrcFilenamePJLFS = filename;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b t n S a v e R e p o r t _ C l i c k                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Save Report' button is clicked.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void btnSaveReport_Click(object sender, EventArgs e)
        {
            Boolean flagOptRptWrap = false;

            ReportCore.eRptFileFmt rptFileFmt = ReportCore.eRptFileFmt.NA;
            ReportCore.eRptChkMarks rptChkMarks = ReportCore.eRptChkMarks.NA;

            TargetCore.metricsReturnFileRpt (
                ToolCommonData.eToolIds.StatusReadback,
                ref rptFileFmt,
                ref rptChkMarks,
                ref flagOptRptWrap);


            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
                ToolStatusReadbackReport.generate(rptFileFmt,
                                                  txtReply,
                                                  ref _reportFilenamePCL);
            else if (_crntPDL == ToolCommonData.ePrintLang.PJL)
                ToolStatusReadbackReport.generate(rptFileFmt,
                                                  txtReply,
                                                  ref _reportFilenamePJL);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b P C L E n t i t y _ S e l e c t i o n C h a n g e d            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCL 'Entity' selection is changed.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbPCLEntity_SelectionChanged(object sender,
                                                  SelectionChangedEventArgs e)
        {
            _indxPCLEntityType = cbPCLEntityType.SelectedIndex;

            txtReply.Clear();

            btnSaveReport.Visibility = Visibility.Hidden;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b P C L L o c T y p e _ S e l e c t i o n C h a n g e d          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCL 'Entity' selection is changed.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbPCLLocType_SelectionChanged(object sender,
                                                   SelectionChangedEventArgs e)
        {
            _indxPCLLocType = cbPCLLocType.SelectedIndex;

            txtReply.Clear();

            btnSaveReport.Visibility = Visibility.Hidden;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b P J L C a t e g o r y _ S e l e c t i o n C h a n g e d        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PJL 'Category' selection is changed.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbPJLCategory_SelectionChanged(object sender,
                                                    SelectionChangedEventArgs e)
        {
            _indxPJLCategory = cbPJLCategory.SelectedIndex;

            if (PJLCategories.getType (_indxPJLCategory) ==
                PJLCategories.eCategoryType.Custom)
                txtPJLCustomCat.Visibility = Visibility.Visible;
            else
                txtPJLCustomCat.Visibility = Visibility.Hidden;

            txtReply.Clear();

            btnSaveReport.Visibility = Visibility.Hidden;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b P J L C o m m a n d _ S e l e c t i o n C h a n g e d          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PJL 'Command' selection is changed.                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbPJLCommand_SelectionChanged(object sender,
                                                   SelectionChangedEventArgs e)
        {
            _indxPJLCommand = cbPJLCommand.SelectedIndex;

            _cmdIndxPJL = _subsetPJLCommands[_indxPJLCommand];

            _reqTypePJL = PJLCommands.getType(_cmdIndxPJL);

            txtPJLCustomCat.Visibility = Visibility.Hidden;
            txtPJLCustomVar.Visibility = Visibility.Hidden;

            if (_reqTypePJL == PJLCommands.eRequestType.Category)
            {
                lbPJLCategory.Visibility = Visibility.Visible;
                cbPJLCategory.Visibility = Visibility.Visible;
                lbPJLVariable.Visibility = Visibility.Hidden;
                cbPJLVariable.Visibility = Visibility.Hidden;

                if (PJLCategories.getType (_indxPJLCategory) ==
                    PJLCategories.eCategoryType.Custom)
                    txtPJLCustomCat.Visibility = Visibility.Visible;
            }
            else
            {
                lbPJLCategory.Visibility = Visibility.Hidden;
                cbPJLCategory.Visibility = Visibility.Hidden;
                lbPJLVariable.Visibility = Visibility.Visible;
                cbPJLVariable.Visibility = Visibility.Visible;

                if (PJLVariables.getType (_indxPJLVariable) ==
                    PJLVariables.eVarType.Custom)
                    txtPJLCustomVar.Visibility = Visibility.Visible;
            }

            txtReply.Clear();

            btnSaveReport.Visibility = Visibility.Hidden;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b P J L F S C o m m a n d _ S e l e c t i o n C h a n g e d      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PJL 'Command' selection is changed.                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbPJLFSCommand_SelectionChanged (
            object sender,
            SelectionChangedEventArgs e)
        {
            _indxPJLFSCommand = cbPJLFSCommand.SelectedIndex;

            _cmdIndxPJLFS = _subsetPJLFSCommands [_indxPJLFSCommand];

            _reqTypePJLFS = PJLCommands.getType (_cmdIndxPJLFS);

            lbPJLFSLocPath.Visibility = Visibility.Hidden;
            txtPJLFSLocPath.Visibility = Visibility.Hidden;
            btnPJLFSLocPathBrowse.Visibility = Visibility.Hidden;

            lbPJLFSOpt1.Visibility = Visibility.Hidden;
            txtPJLFSOpt1.Visibility = Visibility.Hidden;

            lbPJLFSOpt2.Visibility = Visibility.Hidden;
            txtPJLFSOpt2.Visibility = Visibility.Hidden;

            if (_reqTypePJLFS == PJLCommands.eRequestType.FSInit)
            {
                txtPJLFSPath.Text = _objVolPJLFS;
            }
            else if (_reqTypePJLFS == PJLCommands.eRequestType.FSBinSrc)
            {
                txtPJLFSPath.Text = _objPathPJLFS;

                lbPJLFSLocPath.Content = "Source file:";
                lbPJLFSLocPath.Visibility = Visibility.Visible;

                txtPJLFSLocPath.Text = _binSrcFilenamePJLFS;
                txtPJLFSLocPath.Visibility = Visibility.Visible;

                btnPJLFSLocPathBrowse.Visibility = Visibility.Visible;
            }
            else if (_reqTypePJLFS == PJLCommands.eRequestType.FSUpload)
            {
                txtPJLFSPath.Text = _objPathPJLFS;

                lbPJLFSLocPath.Content = "Target file:";
                lbPJLFSLocPath.Visibility = Visibility.Visible;

                txtPJLFSLocPath.Text = _binTgtFilenamePJLFS;
                txtPJLFSLocPath.Visibility = Visibility.Visible;

                btnPJLFSLocPathBrowse.Visibility = Visibility.Visible;

                lbPJLFSOpt1.Content = "Size:";
                lbPJLFSOpt1.Visibility = Visibility.Visible;
                txtPJLFSOpt1.Visibility = Visibility.Visible;
                
                _valPJLFSOpt1 = _defaultPJLFSSize;
                txtPJLFSOpt1.Text = _valPJLFSOpt1.ToString ();

                lbPJLFSOpt2.Content = "Offset:";
                lbPJLFSOpt2.Visibility = Visibility.Visible;
                txtPJLFSOpt2.Visibility = Visibility.Visible;
                
                _valPJLFSOpt2 = _defaultPJLFSOffset;
                txtPJLFSOpt2.Text = _valPJLFSOpt2.ToString ();
            }
            else if (_reqTypePJLFS == PJLCommands.eRequestType.FSDirList)
            {
                txtPJLFSPath.Text = _objDirPJLFS;

                lbPJLFSOpt1.Content = "Count:";
                lbPJLFSOpt1.Visibility = Visibility.Visible;
                txtPJLFSOpt1.Visibility = Visibility.Visible;

                _valPJLFSOpt1 = _defaultPJLFSCount;
                txtPJLFSOpt1.Text = _valPJLFSOpt1.ToString ();

                lbPJLFSOpt2.Content = "Entry:";
                lbPJLFSOpt2.Visibility = Visibility.Visible;
                txtPJLFSOpt2.Visibility = Visibility.Visible;
                
                _valPJLFSOpt2 = _defaultPJLFSEntry;
                txtPJLFSOpt2.Text = _valPJLFSOpt2.ToString ();
            }
            else if (_reqTypePJLFS == PJLCommands.eRequestType.FSMkDir)
            {
                txtPJLFSPath.Text = _objDirPJLFS;
            }
            else    // reqTypePJLFS == .FSDelete || .FsQuery //
            {
                txtPJLFSPath.Text = _objPathPJLFS;
            }

            txtReply.Clear ();

            btnSaveReport.Visibility = Visibility.Hidden;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b P J L V a r i a b l e _ S e l e c t i o n C h a n g e d        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PJL 'Variable' selection is changed.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbPJLVariable_SelectionChanged(object sender,
                                                    SelectionChangedEventArgs e)
        {
            _indxPJLVariable = cbPJLVariable.SelectedIndex;

            if (PJLVariables.getType (_indxPJLVariable) ==
                PJLVariables.eVarType.Custom)
                txtPJLCustomVar.Visibility = Visibility.Visible;
            else
                txtPJLCustomVar.Visibility = Visibility.Hidden;

            txtReply.Clear();

            btnSaveReport.Visibility = Visibility.Hidden;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P J L F S S e c J o b_ C h e c k e d                         //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PJLFS secure job checkbox is checked.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPJLFSSecJob_Checked (object sender, RoutedEventArgs e)
        {
            _flagPJLFSSecJob = true;

            lbPJLFSPwd.Visibility = Visibility.Visible;
            txtPJLFSPwd.Visibility = Visibility.Visible;

            txtPJLFSPwd.Text = _defaultPJLFSPassword.ToString ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P J L F S S e c J o b_ U n c h e c k e d                     //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PJLFS secure job checkbox is unchecked.            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPJLFSSecJob_Unchecked (object sender, RoutedEventArgs e)
        {
            _flagPJLFSSecJob = false;

            lbPJLFSPwd.Visibility = Visibility.Hidden;
            txtPJLFSPwd.Visibility = Visibility.Hidden;
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
        // Initialisation.                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void initialise()
        {
            PJLVariables.eVarType varType;

            PJLCommands.eCmdIndex indxCmd;

            String personality;

            //----------------------------------------------------------------//
            //                                                                //
            // Populate form.                                                 //
            //                                                                //
            //----------------------------------------------------------------//

            cbPCLEntityType.Items.Clear();
            
            _ctPCLEntityTypes = PCLEntityTypes.getCount();

            for (Int32 i = 0; i < _ctPCLEntityTypes; i++)
            {
                cbPCLEntityType.Items.Add(PCLEntityTypes.getName(i));
            }

            //----------------------------------------------------------------//

            cbPCLLocType.Items.Clear();

            _ctPCLLocTypes = PCLLocationTypes.getCount();

            for (Int32 i = 0; i < _ctPCLLocTypes; i++)
            {
                cbPCLLocType.Items.Add(PCLLocationTypes.getName(i));
            }

            //----------------------------------------------------------------//

            cbPJLCommand.Items.Clear();
            
            _ctPJLCommands = _subsetPJLCommands.Length;

            for (Int32 i = 0; i < _ctPJLCommands; i++)
            {
                indxCmd = (PJLCommands.eCmdIndex) _subsetPJLCommands[i];

                cbPJLCommand.Items.Add (PJLCommands.getName (indxCmd));
            }

            //----------------------------------------------------------------//

            cbPJLFSCommand.Items.Clear();
            
            _ctPJLFSCommands = _subsetPJLFSCommands.Length;

            for (Int32 i = 0; i < _ctPJLFSCommands; i++)
            {
                indxCmd = (PJLCommands.eCmdIndex) _subsetPJLFSCommands[i];

                cbPJLFSCommand.Items.Add (PJLCommands.getName (indxCmd));
            }

            //----------------------------------------------------------------//

            cbPJLCategory.Items.Clear();
            
            _ctPJLCategories = PJLCategories.getCount();

            for (Int32 i = 0; i < _ctPJLCategories; i++)
            {
                cbPJLCategory.Items.Add(PJLCategories.getName(i));
            }

            //----------------------------------------------------------------//

            cbPJLVariable.Items.Clear();
            
            _ctPJLVariables = PJLVariables.getCount();

            for (Int32 i = 0; i < _ctPJLVariables; i++)
            {
                varType = PJLVariables.getType(i);

                if (varType == PJLVariables.eVarType.PCL)
                    personality = "PCL: ";
                else if (varType == PJLVariables.eVarType.PDF)
                    personality = "PDF: ";
                else if (varType == PJLVariables.eVarType.PS)
                    personality = "POSTSCRIPT: ";
                else
                    personality = "";

                cbPJLVariable.Items.Add(personality + PJLVariables.getName(i));
            }

            //----------------------------------------------------------------//

            resetTarget ();

            //----------------------------------------------------------------//
            //                                                                //
            // Reinstate settings from persistent storage.                    //
            //                                                                //
            //----------------------------------------------------------------//

            metricsLoad();

            cbPCLEntityType.SelectedIndex = _indxPCLEntityType;
            cbPCLLocType.SelectedIndex    = _indxPCLLocType;
            cbPJLCategory.SelectedIndex   = _indxPJLCategory;
            cbPJLCommand.SelectedIndex    = _indxPJLCommand;
            cbPJLFSCommand.SelectedIndex  = _indxPJLFSCommand;
            cbPJLVariable.SelectedIndex   = _indxPJLVariable;

            _cmdIndxPJL   = _subsetPJLCommands [_indxPJLCommand];
            _cmdIndxPJLFS = _subsetPJLFSCommands [_indxPJLFSCommand];

            _reqTypePJL   = PJLCommands.getType (_cmdIndxPJL);
            _reqTypePJLFS = PJLCommands.getType (_cmdIndxPJLFS);

            _passwordPJLFS = _defaultPJLFSPassword;

            ToolCommonFunctions.splitPathName (_objPathPJLFS,
                                               ref _objVolPJLFS,
                                               ref _objDirPJLFS,
                                               ref _objFilPJLFS);

            if (_crntPDL == ToolCommonData.ePrintLang.PJL)
            {
                if (_flagPJLFS)
                {
                    rbSelTypePJLFS.IsChecked = true;
                    tabPDLs.SelectedItem = tabPJLFS;

                    if (_reqTypePJLFS == PJLCommands.eRequestType.FSUpload)
                        txtPJLFSLocPath.Text = _binTgtFilenamePJLFS;
                    else
                        txtPJLFSLocPath.Text = _binSrcFilenamePJLFS;

                    if (_reqTypePJLFS == PJLCommands.eRequestType.FSInit)
                        txtPJLFSPath.Text = _objVolPJLFS;
                    else if ((_reqTypePJLFS == PJLCommands.eRequestType.FSDirList) ||
                             (_reqTypePJLFS == PJLCommands.eRequestType.FSMkDir))
                        txtPJLFSPath.Text = _objDirPJLFS;
                    else
                        txtPJLFSPath.Text = _objPathPJLFS;

                    if (_flagPJLFSSecJob)
                    {
                        lbPJLFSPwd.Visibility = Visibility.Visible;
                        txtPJLFSPwd.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        lbPJLFSPwd.Visibility = Visibility.Hidden;
                        txtPJLFSPwd.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    rbSelTypePJL.IsChecked = true;
                    tabPDLs.SelectedItem = tabPJL;

                    txtPJLCustomCat.Visibility = Visibility.Hidden;
                    txtPJLCustomVar.Visibility = Visibility.Hidden;

                    txtPJLCustomCat.Text = _customCatPJL;
                    txtPJLCustomVar.Text = _customVarPJL;

                    if (_reqTypePJL == PJLCommands.eRequestType.Category)
                    {
                        if (PJLCategories.getType (_indxPJLCategory) ==
                            PJLCategories.eCategoryType.Custom)
                            txtPJLCustomCat.Visibility = Visibility.Visible;

                    }
                    else if (_reqTypePJL == PJLCommands.eRequestType.Variable)
                    {
                        if (PJLVariables.getType (_indxPJLVariable) ==
                            PJLVariables.eVarType.Custom)
                            txtPJLCustomVar.Visibility = Visibility.Visible;
                    }
                }
            }
            else
            {
                rbSelTypePCL.IsChecked = true;
                tabPDLs.SelectedItem = tabPCL;
            }

            txtReply.Clear();

            btnSaveReport.Visibility = Visibility.Hidden;
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
            Int32 indxTemp = 0;

            ToolStatusReadbackPersist.loadDataCommon (ref indxTemp);

            ToolStatusReadbackPersist.loadDataPCL (ref _indxPCLEntityType,
                                                   ref _indxPCLLocType,
                                                   ref _reportFilenamePCL);

            ToolStatusReadbackPersist.loadDataPJL (ref _indxPJLCategory,
                                                   ref _indxPJLCommand,
                                                   ref _indxPJLVariable,
                                                   ref _customCatPJL,
                                                   ref _customVarPJL,
                                                   ref _reportFilenamePJL);

            ToolStatusReadbackPersist.loadDataPJLFS (ref _indxPJLFSCommand,
                                                     ref _objPathPJLFS,
                                                     ref _binSrcFilenamePJLFS,
                                                     ref _binTgtFilenamePJLFS,
                                                     ref _flagPJLFS,
                                                     ref _flagPJLFSSecJob);

            //----------------------------------------------------------------//

            if (indxTemp == (Int32)ToolCommonData.ePrintLang.PJL)
                _crntPDL = ToolCommonData.ePrintLang.PJL;
            else
                _crntPDL = ToolCommonData.ePrintLang.PCL;

            //----------------------------------------------------------------//

            if ((_indxPCLEntityType < 0) ||
                (_indxPCLEntityType >= _ctPCLEntityTypes))
                _indxPCLEntityType = 0;

            if ((_indxPCLLocType < 0) ||
                (_indxPCLLocType >= _ctPCLLocTypes))
                _indxPCLLocType = 0;

            //----------------------------------------------------------------//

            if ((_indxPJLCategory < 0) ||
                (_indxPJLCategory >= _ctPJLCategories))
                _indxPJLCategory = 0;

            if ((_indxPJLCommand < 0) ||
                (_indxPJLCommand >= _ctPJLCommands))
                _indxPJLCommand = 0;

            if ((_indxPJLVariable < 0) ||
                (_indxPJLVariable >= _ctPJLVariables))
                _indxPJLVariable = 0;

            if ((_indxPJLFSCommand < 0) ||
                (_indxPJLFSCommand >= _ctPJLFSCommands))
                _indxPJLFSCommand = 0;
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
            ToolStatusReadbackPersist.saveDataCommon ((Int32) _crntPDL);

            ToolStatusReadbackPersist.saveDataPCL(_indxPCLEntityType,
                                               _indxPCLLocType,
                                               _reportFilenamePCL);

            ToolStatusReadbackPersist.saveDataPJL(_indxPJLCategory,
                                               _indxPJLCommand,
                                               _indxPJLVariable,
                                               _customCatPJL,
                                               _customVarPJL,
                                               _reportFilenamePJL);

            ToolStatusReadbackPersist.saveDataPJLFS (_indxPJLFSCommand,
                                                     _objPathPJLFS,
                                                     _binSrcFilenamePJLFS,
                                                     _binTgtFilenamePJLFS,
                                                     _flagPJLFS,
                                                     _flagPJLFSSecJob);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b S e l T y p e P C L _ C l i c k                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Radio button selecting PCL clicked.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbSelTypePCL_Click(object sender, RoutedEventArgs e)
        {
            _crntPDL = ToolCommonData.ePrintLang.PCL;

            tabPDLs.SelectedItem = tabPCL;

            txtReply.Clear();

            btnSaveReport.Visibility = Visibility.Hidden;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b S e l T y p e P J L _ C l i c k                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Radio button selecting PJL clicked.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbSelTypePJL_Click(object sender, RoutedEventArgs e)
        {
            _crntPDL = ToolCommonData.ePrintLang.PJL;
            _flagPJLFS = false;

            tabPDLs.SelectedItem = tabPJL;

            txtReply.Clear();

            btnSaveReport.Visibility = Visibility.Hidden;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b S e l T y p e P J L F S _ C l i c k                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Radio button selecting PJL FS clicked.                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbSelTypePJLFS_Click (object sender, RoutedEventArgs e)
        {
            _crntPDL = ToolCommonData.ePrintLang.PJL;
            _flagPJLFS = true;

            tabPDLs.SelectedItem = tabPJLFS;

            if (_flagPJLFSSecJob)
            {
                lbPJLFSPwd.Visibility = Visibility.Visible;
                txtPJLFSPwd.Visibility = Visibility.Visible;
            }
            else
            {
                lbPJLFSPwd.Visibility = Visibility.Hidden;
                txtPJLFSPwd.Visibility = Visibility.Hidden;
            }

            txtReply.Clear ();

            btnSaveReport.Visibility = Visibility.Hidden;
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
                btnGenerate.Content = "Generate request & save to file";
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

                btnGenerate.Content = "Generate request & read reply from " +
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
        // s e l e c t L o c B i n F i l e                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initiate 'open file' dialogue for binary file, used with:          //
        //  -   FSAPPEND   with binary Source file                            //
        //  -   FSDOWNLOAD             Source                                 //
        //  -   FSUPLOAD               Target                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean selectLocBinFile (Boolean    upload,
                                          ref String locBinFilename)
        {
            Boolean selected;

            if (upload)
                selected = selectLocBinTgtFile(ref locBinFilename);
            else
                selected = selectLocBinSrcFile(ref locBinFilename);

            return selected;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e l e c t L o c B i n S r c F i l e                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initiate 'open file' dialogue for binary Source file, used with:   //
        //  -   FSAPPEND                                                      //
        //  -   FSDOWNLOAD                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean selectLocBinSrcFile (ref String locBinFilename)
        {
            Boolean selected;

            String folderName = null;
            String fileName = null;

            OpenFileDialog openDialog = new OpenFileDialog();

            ToolCommonFunctions.splitPathName(locBinFilename,
                                               ref folderName,
                                               ref fileName);

            openDialog.InitialDirectory = folderName;
            openDialog.FileName = fileName;
            openDialog.CheckFileExists = true;

            openDialog.Filter = "Print Files|" +
                                "*.prn; *.pcl; *.dia;" +
                                "*.PRN; *.PCL; *.DIA" +
                                "|Font files|" +
                                "*.sfp; *.sfs; *.sft; *.sfx; " +
                                "*.SFP; *.SFS; *.SFT, *.SFX" +
                                "|Overlay files|" +
                                "*.ovl; *.ovx;" +
                                "*.OVL; *.OVX" +
                                "|All files|" +
                                "*.*";

            Nullable<Boolean> dialogResult = openDialog.ShowDialog();

            if (dialogResult == true)
            {
                selected = true;
                locBinFilename = openDialog.FileName;
            }
            else
            {
                selected = false;
            }

            return selected;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e l e c t L o c B i n T g t F i l e                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initiate 'open file' dialogue for binary Target file, used with:   //
        //  -   FSUPLOAD                                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean selectLocBinTgtFile(ref String locBinFilename)
        {
            Boolean selected = false;

            String saveFolder = null;
            String saveFile = null;

            SaveFileDialog saveDialog;

            //----------------------------------------------------------------//
            //                                                                //
            // Invoke 'Save As' dialogue.                                     //
            //                                                                //
            //----------------------------------------------------------------//

            ToolCommonFunctions.splitPathName (locBinFilename,
                                               ref saveFolder,
                                               ref saveFile);

            saveDialog = new SaveFileDialog();

            saveDialog.Filter = "Print Files|" +
                                "*.prn; *.pcl; *.dia;" +
                                "*.PRN; *.PCL; *.DIA" +
                                "|Font files|" +
                                "*.sfp; *.sfs; *.sft; *.sfx; " +
                                "*.SFP; *.SFS; *.SFT, *.SFX" +
                                "|Overlay files|" +
                                "*.ovl; *.ovx;" +
                                "*.OVL; *.OVX" +
                                "|All files|" +
                                "*.*";

            saveDialog.DefaultExt = "pcl";
            saveDialog.InitialDirectory = saveFolder;
            saveDialog.OverwritePrompt = true;
            saveDialog.FileName = saveFile;

            Nullable<Boolean> dialogResult = saveDialog.ShowDialog();

            if (dialogResult == true)
            {
                locBinFilename = saveDialog.FileName;

                selected = true;
            }

            return selected;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t P J L C u s t o m C a t _ G o t F o c u s                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Text box for PJL Category custom valuie has focus.                 //
        // Select all text in the box, so that it can be over-written easily, //
        // without inadvertently causing validation failures.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtPJLCustomCat_GotFocus (object sender,
                                           RoutedEventArgs e)
        {
            txtPJLCustomCat.SelectAll ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t P J L C u s t o m C a t _ L o s t F o c u s                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Text box for PJL Category custom valuie has lost focus.            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtPJLCustomCat_LostFocus (object sender,
                                            RoutedEventArgs e)
        {
            _customCatPJL = txtPJLCustomCat.Text;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t P J L C u s t o m V a r _ G o t F o c u s                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Text box for PJL Variable custom valuie has focus.                 //
        // Select all text in the box, so that it can be over-written easily, //
        // without inadvertently causing validation failures.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtPJLCustomVar_GotFocus (object sender,
                                           RoutedEventArgs e)
        {
            txtPJLCustomVar.SelectAll ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t P J L C u s t o m V a r _ L o s t F o c u s                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Text box for PJL Variable custom valuie has lost focus.            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtPJLCustomVar_LostFocus (object sender,
                                            RoutedEventArgs e)
        {
            _customVarPJL = txtPJLCustomVar.Text;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t P J L F S L o c P a t h _ G o t F o c u s                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Text box for PJL FileSystem binary source / target file has focus. //
        // Select all text in the box, so that it can be over-written easily, //
        // without inadvertently causing validation failures.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtPJLFSLocPath_GotFocus (object sender,
                                               RoutedEventArgs e)
        {
            txtPJLFSLocPath.SelectAll ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t P J L F S L o c P a t h _ L o s t F o c u s                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Text box for PJL FileSystem binary source / target  file has lost  //
        // focus.                                                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtPJLFSLocPath_LostFocus(object sender,
                                                RoutedEventArgs e)
        {
            if (_reqTypePJLFS == PJLCommands.eRequestType.FSUpload)
                _binTgtFilenamePJLFS = txtPJLFSLocPath.Text;
            else
                _binSrcFilenamePJLFS = txtPJLFSLocPath.Text;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t P J L F S O p t 1 _ G o t F o c u s                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Text box for PJL FileSystem option 1 has focus.                    //
        // Select all text in the box, so that it can be over-written easily, //
        // without inadvertently causing validation failures.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtPJLFSOpt1_GotFocus (object sender,
                                            RoutedEventArgs e)
        {
            txtPJLFSOpt1.SelectAll ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t P J L F S O p t 1 _ L o s t F o c u s                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Text box for PJL FileSystem option 1 has lost focus.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtPJLFSOpt1_LostFocus (object sender,
                                             RoutedEventArgs e)
        {
            if (! validatePJLFSOpt1 (true, ref _valPJLFSOpt1))
            {
                txtPJLFSOpt1.Focus ();
                txtPJLFSOpt1.SelectAll ();
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t P J L F S O p t 2 _ G o t F o c u s                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Text box for PJL FileSystem option 2 has focus.                    //
        // Select all text in the box, so that it can be over-written easily, //
        // without inadvertently causing validation failures.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtPJLFSOpt2_GotFocus (object sender,
                                            RoutedEventArgs e)
        {
            txtPJLFSOpt2.SelectAll ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t P J L F S O p t 2 _ L o s t F o c u s                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Text box for PJL FileSystem option 2 has lost focus.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtPJLFSOpt2_LostFocus (object sender,
                                             RoutedEventArgs e)
        {
            if (!validatePJLFSOpt2 (true, ref _valPJLFSOpt2))
            {
                txtPJLFSOpt2.Focus ();
                txtPJLFSOpt2.SelectAll ();
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t P J L F S P a t h _ G o t F o c u s                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Text box for PJL FileSystem object name has focus.                 //
        // Select all text in the box, so that it can be over-written easily, //
        // without inadvertently causing validation failures.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtPJLFSPath_GotFocus (object sender,
                                            RoutedEventArgs e)
        {
            txtPJLFSPath.SelectAll ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t P J L F S P a t h _ L o s t F o c u s                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Text box for PJL FileSystem object name has lost focus.            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtPJLFSPath_LostFocus (object sender,
                                             RoutedEventArgs e)
        {
            if (_reqTypePJLFS == PJLCommands.eRequestType.FSInit)
                _objVolPJLFS = txtPJLFSPath.Text;
            else if ((_reqTypePJLFS == PJLCommands.eRequestType.FSDirList) ||
                     (_reqTypePJLFS == PJLCommands.eRequestType.FSMkDir))
                _objDirPJLFS = txtPJLFSPath.Text;
            else
                _objPathPJLFS = txtPJLFSPath.Text;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t P J L F S P w d _ G o t F o c u s                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Text box for PJL FileSystem password has focus.                    //
        // Select all text in the box, so that it can be over-written easily, //
        // without inadvertently causing validation failures.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtPJLFSPwd_GotFocus (object sender,
                                           RoutedEventArgs e)
        {
            txtPJLFSPwd.SelectAll ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t P J L F S P w d _ L o s t F o c u s                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Text box for PJL FileSystem password has lost focus.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtPJLFSPwd_LostFocus (object sender,
                                            RoutedEventArgs e)
        {
            _passwordPJLFS = txtPJLFSPwd.Text;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // v a l i d a t e P J L F S O p t 1                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Validate PJL File System optional paramater 1.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean validatePJLFSOpt1 (Boolean lostFocusEvent,
                                           ref Int32 newValue)
        {
            Int32 value;
            Int32 defVal;

            String boxName;

            Boolean OK = true;

            if (_reqTypePJLFS == PJLCommands.eRequestType.FSDirList)
            {
                boxName = "Count";
                defVal = _defaultPJLFSCount;
            }
            else
            {
                boxName = "Size";
                defVal = _defaultPJLFSSize;
            }

            String crntText = txtPJLFSOpt1.Text;

            OK = Int32.TryParse (crntText, out value);

            if (OK)
                if (value < 0)
                    OK = false;

            if (OK)
            {
                newValue = value;
            }
            else
            {
                if (lostFocusEvent)
                {
                    String newText = defVal.ToString ();

                    MessageBox.Show (boxName + " value is invalid.\n\n" +
                                     "Value will be reset to default '" +
                                     newText + "'",
                                     "Option value invalid",
                                     MessageBoxButton.OK,
                                     MessageBoxImage.Warning);

                    txtPJLFSOpt1.Text = newText;
                    newValue = defVal;

                    OK = true;
                }
                else
                {
                    MessageBox.Show (boxName + " value is invalid.\n\n",
                                     "Option value invalid",
                                     MessageBoxButton.OK,
                                     MessageBoxImage.Warning);

                    txtPJLFSOpt1.Focus ();
                    txtPJLFSOpt1.SelectAll ();
                }
            }

            return OK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // v a l i d a t e P J L F S O p t 2                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Validate PJL File System optional paramater 2.                     //
        //                                                                    //
        //--------------------------------------------------------------------//


        private Boolean validatePJLFSOpt2 (Boolean lostFocusEvent,
                                           ref Int32 newValue)
        {
            Int32 value;
            Int32 defVal;

            String boxName;

            Boolean OK = true;

            if (_reqTypePJLFS == PJLCommands.eRequestType.FSDirList)
            {
                boxName = "Entry";
                defVal = _defaultPJLFSEntry;
            }
            else
            {
                boxName = "Offset";
                defVal = _defaultPJLFSOffset;
            }

            String crntText = txtPJLFSOpt2.Text;

            OK = Int32.TryParse (crntText, out value);

            if (OK)
                if (value < 0)
                    OK = false;

            if (OK)
            {
                newValue = value;
            }
            else
            {
                if (lostFocusEvent)
                {
                    String newText = defVal.ToString ();

                    MessageBox.Show (boxName + " value is invalid.\n\n" +
                                     "Value will be reset to default '" +
                                     newText + "'",
                                     "Option value invalid",
                                     MessageBoxButton.OK,
                                     MessageBoxImage.Warning);

                    txtPJLFSOpt2.Text = newText;
                    newValue = defVal;

                    OK = true;
                }
                else
                {
                    MessageBox.Show (boxName + " value is invalid.\n\n",
                                     "Option value invalid",
                                     MessageBoxButton.OK,
                                     MessageBoxImage.Warning);

                    txtPJLFSOpt2.Focus ();
                    txtPJLFSOpt2.SelectAll ();
                }
            }

            return OK;
        }

    }
}
