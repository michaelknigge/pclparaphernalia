using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// Interaction logic for ToolSymbolSetGenerate.xaml
    /// 
    /// Class handles the SymbolSetGenerate tool form.
    /// 
    /// © Chris Hutchinson 2013
    /// 
    /// </summary>
    
    [System.Reflection.ObfuscationAttribute(Feature = "renaming",
                                            ApplyToMembers = true)]

    public partial class ToolSymbolSetGenerate : Window
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private const Char   _defaultSymSetIdAlpha    = 'N';
        private const UInt16 _defaultSymSetIdNum      = 0;
        private const UInt16 _defaultSymSetNo         = 14;          //    0N //
        private const UInt16 _symSetNoTargetMax       = 32762;       // 1023Z //


        private const Int32  cSizeCharSet_8bit        = 256;
        private const Int32  cSizeCharSet_16bit       = 65536;
        private const Int32  cCodePointUnused         = 65535;
        private const Int32  cCodePointC0Min          = 0x00;
        private const Int32  cCodePointC0Max          = 0x1f;
        private const Int32  cCodePointC1Min          = 0x80;
        private const Int32  cCodePointC1Max          = 0x9f;

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private UInt16 [] _symSetMapDonor;
        private UInt16 [] _symSetMapUserSet;
        private UInt16 [] _symSetMapTarget;
        private UInt16 [] _symSetMap8bit;
        private UInt16 [] _symSetMap16bit;

        private UInt16 _codeMin = 0;
        private UInt16 _codeMax = 0;
        private UInt16 _codeCt  = 0;
        
        private Int32 _sizeCharSet;

        private UInt16 _offsetMin;
        private UInt16 _offsetMax;

        private Boolean _initialised             = false;
        private Boolean _initialisedOffsets      = false;
        private Boolean _flagDonorSymSetMapPCL   = false;   
        private Boolean _flagDonorSymSetUserSet  = false;
        private Boolean _flagMapHex              = true;   
        private Boolean _flagCharCollReqInhibit  = false;   
        private Boolean _flagCharCollReqSpecific = false;   
        private Boolean _flagIndexUnicode        = true;   
        private Boolean _flagIgnoreC0            = true;   
        private Boolean _flagIgnoreC1            = true;   
        private Boolean _flagMultiByteMap        = false;
        private Boolean _flagMultiByteSet        = false;

        private Boolean _flagSymSetNullMapPCL    = false;
        private Boolean _flagSymSetNullMapStd    = false;

        private ASCIIEncoding _ascii = new ASCIIEncoding ();
        
        private Int32[] _subsetSymSets;

        private Int32 _ctMappedSymSets = 0;
        private Int32 _indxOffsets = 0;

        private Int32 _indxDonorSymSetSubset;

        private UInt64 _targetCharCollReq;
        private UInt64 _targetCharCollReqMSL;
        private UInt64 _targetCharCollReqUnicode;
        private UInt64 _targetCharCollReqAllMSL;
        private UInt64 _targetCharCollReqAllUnicode;

        private UInt16 _targetSymSetNo;
        private UInt16 _donorSymSetNo;
        private UInt16 _donorSymSetNoUserSet;

        private PCLSymbolSets.eSymSetGroup _donorSymSetGroup;

        private PCLSymSetTypes.eIndex _targetSymSetType;

        private String _donorSymSetFile;
        private String _donorSymSetFolder;
        private String _targetSymSetFile;
        private String _targetSymSetFolder;

        private ObservableCollection<PCLCharCollItem> _charCollReqListMSL =
                    new ObservableCollection<PCLCharCollItem> ();
        private ObservableCollection<PCLCharCollItem> _charCollReqListUnicode =
                    new ObservableCollection<PCLCharCollItem> ();

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // T o o l S o f t F o n t G e n e r a t e                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public ToolSymbolSetGenerate (ref ToolCommonData.ePrintLang crntPDL)
        {
            InitializeComponent();

            initialise();

            crntPDL = ToolCommonData.ePrintLang.PCL;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b t n C o d e P t C l e a r _ C l i c k                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'clear' code points button is clicked.             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void btnCodePtClear_Click (object sender, EventArgs e)
        {
            for (Int32 i = 0; i < cSizeCharSet_8bit; i++)
            {
                _symSetMapTarget [_offsetMin + i] = cCodePointUnused;
            }

            mapDisplay (_flagMapHex, _flagIgnoreC0, _flagIgnoreC1,
                        _offsetMin);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b t n C o d e P t C l e a r A l l _ C l i c k                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'clear all' code points button is clicked.         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void btnCodePtClearAll_Click (object sender, EventArgs e)
        { 
            for (Int32 i = 0; i < cSizeCharSet_16bit; i++)
            {
                _symSetMapTarget [i] = cCodePointUnused;
            }

            mapDisplay (_flagMapHex, _flagIgnoreC0, _flagIgnoreC1,
                        _offsetMin);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b t n D e f i n e S y m S e t _ C l i c k                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Define symbol set' button is clicked.             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void btnDefineSymSet_Click (object sender, EventArgs e)
        {
            const UInt16 offsetFactor = 100 * 32;

            String idAlpha = "",
                   idNum = "";

            Int32 targetOffset = ((_symSetNoTargetMax - _donorSymSetNo) / 
                                 (offsetFactor)) * (offsetFactor);

            _targetSymSetNo = (UInt16) (_donorSymSetNo + targetOffset);

            PCLSymbolSets.translateKind1ToId (_targetSymSetNo,
                                              ref idNum,
                                              ref idAlpha);

            txtTargetSymSetNo.Text = _donorSymSetNo.ToString ();

            txtTargetSymSetIdNum.Text = idNum;
            txtTargetSymSetIdAlpha.Text = idAlpha;
            txtTargetSymSetNo.Text = _targetSymSetNo.ToString ();

            //----------------------------------------------------------------//

            setTargetSymSetFilename ();

            if (_flagDonorSymSetUserSet)
            {
                // map already set - see checkDonorSymSetFile method
            }
            else
            {
                Int32 sizeDonorSet;

                sizeDonorSet = PCLSymbolSets.getMapArrayMax (
                    _subsetSymSets [_indxDonorSymSetSubset]) + 1;

                _symSetMapDonor = PCLSymbolSets.getMapArray (
                                _subsetSymSets [_indxDonorSymSetSubset],
                                _flagDonorSymSetMapPCL);

                if (sizeDonorSet > cSizeCharSet_8bit)
                    _flagMultiByteSet = true;
                else
                    _flagMultiByteSet = false;
                
                setMultiByteData (_flagMultiByteSet);

                for (Int32 i = 0; i < sizeDonorSet; i++)
                {
                    _symSetMapTarget [i] = _symSetMapDonor [i];
                }

                for (Int32 i = sizeDonorSet; i < _sizeCharSet; i++)
                {
                    _symSetMapTarget [i] = cCodePointUnused;
                }
            }

            mapDisplay (_flagMapHex, _flagIgnoreC0, _flagIgnoreC1, _offsetMin);

            btnDefineSymSet.IsEnabled = false;
            btnGenerateSymSet.IsEnabled = true;
            btnLogSave.IsEnabled = false;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b t n D o n o r S y m S e t F i l e B r o w s e _ C l i c k        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Activated when the Browse button on the donor User-defined symbol  //
        // set file panel is clicked.                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void btnDonorSymSetFileBrowse_Click (object sender,
                                                     RoutedEventArgs e)
        {
            Boolean selected;

            String filename = _donorSymSetFile;

            selected = selectDonorSymSetFile (ref filename);

            if (selected)
            {
                _donorSymSetFile = filename;
                txtDonorSymSetFile.Text = _donorSymSetFile;

                ToolCommonFunctions.getFolderName (_donorSymSetFile,
                                                   ref _donorSymSetFolder);
                
                donorSymSetChange ();
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b t n G e n e r a t e S y m S e t _ C l i c k                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Generate symbol set' button is clicked.           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void btnGenerateSymSet_Click (object sender, EventArgs e)
        {
            ToolSymbolSetGenPCL PCLHandler = new ToolSymbolSetGenPCL ();

            mapMetrics (_flagIgnoreC0, _flagIgnoreC1, _sizeCharSet,
                        ref _codeMin, ref _codeMax, ref _codeCt,
                        ref _targetSymSetType);

            PCLHandler.generateSymSet (ref _targetSymSetFile,
                                        _flagIgnoreC0,
                                        _flagIgnoreC1,
                                        _targetSymSetNo,
                                        _codeMin,
                                        _codeMax,
                                        _targetCharCollReq,
                                        _symSetMapTarget,
                                        _targetSymSetType);

            txtTargetSymSetFile.Text = _targetSymSetFile;

            ToolCommonFunctions.getFolderName(_targetSymSetFile,
                                               ref _targetSymSetFolder);

            btnDefineSymSet.IsEnabled = false;
            btnGenerateSymSet.IsEnabled = true;
            btnLogSave.IsEnabled = true;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b t n L o g S a v e _ C l i c k                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Save log' button is clicked.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void btnLogSave_Click(object sender, EventArgs e)
        {
            Boolean flagOptRptWrap = false;

            ReportCore.eRptFileFmt rptFileFmt = ReportCore.eRptFileFmt.NA;
            ReportCore.eRptChkMarks rptChkMarks = ReportCore.eRptChkMarks.NA;

            TargetCore.metricsReturnFileRpt (
                ToolCommonData.eToolIds.SymbolSetGenerate,
                ref rptFileFmt,
                ref rptChkMarks,    // not used by this tool //
                ref flagOptRptWrap);

            ToolSymbolSetGenReport.generate (rptFileFmt,
                                             _targetSymSetFile,
                                             _targetSymSetNo,
                                             _symSetMapTarget,
                                             _codeMin,
                                             _codeMax,
                                             _codeCt,
                                             _targetCharCollReq,
                                             _flagIgnoreC0,
                                             _flagIgnoreC1,
                                             _flagMapHex,
                                             _targetSymSetType);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b t n T a r g e t S y m S e t F i l e B r o w s e _ C l i c k      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Activated when the Browse button on the target symbol set file     //
        // panel is clicked.                                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void btnTargetSymSetFileBrowse_Click (object sender,
                                                      RoutedEventArgs e)
        {
            Boolean selected;
 
            String filename = _targetSymSetFile;

            selected = selectTargetSymSetFile (ref filename);

            if (selected)
            {
                _targetSymSetFile = filename;
                txtTargetSymSetFile.Text = _targetSymSetFile;

                ToolCommonFunctions.getFolderName (_targetSymSetFile,
                                                   ref _targetSymSetFolder);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b C h a r C o l l s _ S e l e c t i o n C h a n g e d            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // SelectionChanged event handler for Character Collections           //
        // combination box.                                                   //
        // Set the selected item to null, otherwise if one of the disabled    //
        // checkbox items is clicked, details of this item appear in the      //
        // combination box header.                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        void cbCharColls_SelectionChanged (object sender,
                                           SelectionChangedEventArgs e)
        {
            cbCharColls.SelectedItem = null;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b C h a r C o l l s I t e m _ P r o p e r t y C h a n g e d      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // PropertyChanged event handler for Character Collection combination //
        // box item.                                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        void cbCharCollsItem_PropertyChanged (object sender,
                                              PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsChecked")
            {
                if (! _flagCharCollReqInhibit)
                    setTargetCharCollReqArray (_flagIndexUnicode);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b D o n o r S y m S e t _ S e l e c t i o n C h a n g e d        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Donor Symbol Set item has changed.                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbDonorSymSet_SelectionChanged (
            object sender,
            SelectionChangedEventArgs e)
        {
            if (_initialised && cbDonorSymSet.HasItems)
            {
                setDonorSymSetAttributes ();

                btnDefineSymSet.IsEnabled = true;
                btnGenerateSymSet.IsEnabled = false;
                btnLogSave.IsEnabled = false;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b O f f s e t R a n g e _ S e l e c t i o n C h a n g e d        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Offset Range item has changed.                                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbOffsetRange_SelectionChanged (
            object sender,
            SelectionChangedEventArgs e)
        {
            if (_initialisedOffsets && cbOffsetRange.HasItems)
            {
                _indxOffsets = cbOffsetRange.SelectedIndex;

                setOffsetData ();

                mapDisplay (_flagMapHex, _flagIgnoreC0, _flagIgnoreC1,
                            _offsetMin);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h e c k D o n o r S y m S e t F i l e                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Check the contents of the donor (PCL download) symbol set file.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean checkDonorSymSetFile ()
        {
            Boolean flagOK = true;

            Boolean selected = true;

            if (!File.Exists (_donorSymSetFile))
            {
                String filename = _donorSymSetFile;

                MessageBox.Show (
                    "File " + _donorSymSetFile + " does not exist",
                    "Symbol Set definition file",
                     MessageBoxButton.OK,
                     MessageBoxImage.Information);

                //------------------------------------------------------------//
                //                                                            //
                // Invoke File/Open dialogue to select a Symbol set file.     //
                //                                                            //
                //------------------------------------------------------------//

                selected = selectDonorSymSetFile (ref filename);

                if (selected)
                {
                    _donorSymSetFile = filename;
                    txtDonorSymSetFile.Text = _donorSymSetFile;

                    ToolCommonFunctions.getFolderName (_donorSymSetFile,
                                                       ref _donorSymSetFolder);
                }
                else
                {
                    rbDonorSymSetPreset.IsChecked = true;
                    rbDonorSymSetPreset_Click (this, null);
                }
            }

            //----------------------------------------------------------------//

            if (selected)
            {
                UInt16 firstCode = 0,
                       lastCode = 0;

                PCLSymSetTypes.eIndex symSetType =
                    PCLSymSetTypes.eIndex.Unknown;

                flagOK = PCLDownloadSymSet.checkSymSetFile (
                    _donorSymSetFile,
                    ref _donorSymSetNoUserSet,
                    ref firstCode,
                    ref lastCode,
                    ref symSetType);    // not used here at present

                if (flagOK)
                {
                    Int32 sizeDonorSet;

                    sizeDonorSet = lastCode + 1;

                    if (sizeDonorSet > cSizeCharSet_8bit)
                        _flagMultiByteSet = true;
                    else
                        _flagMultiByteSet = false;

                    setMultiByteData (_flagMultiByteSet);

                    _symSetMapUserSet = PCLSymbolSets.getMapArrayUserSet ();

                    for (Int32 i = 0; i < sizeDonorSet; i++)
                    {
                        _symSetMapTarget [i] = _symSetMapUserSet [i];
                    }

                    for (Int32 i = sizeDonorSet; i < _sizeCharSet; i++)
                    {
                        _symSetMapTarget [i] = cCodePointUnused;
                    }

                    txtDonorSymSetFile.Text = _donorSymSetFile;
                }
                else
                {
                    _donorSymSetNoUserSet = _defaultSymSetNo;

                    PCLSymbolSets.setDataUserSetDefault (_defaultSymSetNo);
                    
                    _symSetMapTarget = PCLSymbolSets.getMapArrayUserSet ();

                    txtDonorSymSetFile.Text = "***** Invalid symbol set file *****";
                }
            }

            return flagOK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k I g n o r e C 0 _ C h e c k e d                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // 'Ignore C0 control code-points' check-box checked.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkIgnoreC0_Checked (object sender, RoutedEventArgs e)
        {
            _flagIgnoreC0 = true;

            if (_initialised)
                mapDisplay (_flagMapHex, _flagIgnoreC0, _flagIgnoreC1,
                            _offsetMin);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k I g n o r e C 0 _ U n c h e c k e d                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // 'Ignore C0 control code-points' check-box unchecked.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkIgnoreC0_Unchecked (object sender, RoutedEventArgs e)
        {
            _flagIgnoreC0 = false;

            if (_initialised)
                mapDisplay (_flagMapHex, _flagIgnoreC0, _flagIgnoreC1,
                            _offsetMin);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k I g n o r e C 1 _ C h e c k e d                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // 'Ignore C1 control code-points' check-box checked.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkIgnoreC1_Checked (object sender, RoutedEventArgs e)
        {
            _flagIgnoreC1 = true;

            if (_initialised)
                mapDisplay (_flagMapHex, _flagIgnoreC0, _flagIgnoreC1,
                            _offsetMin);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k I g n o r e C 1 _ U n c h e c k e d                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // 'Ignore C1 control code-points' check-box unchecked.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkIgnoreC1_Unchecked (object sender, RoutedEventArgs e)
        {
            _flagIgnoreC1 = false;

            if (_initialised)
                mapDisplay (_flagMapHex, _flagIgnoreC0, _flagIgnoreC1,
                            _offsetMin);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d o n o r S y m S e t C h a n g e                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called after any of the 'donor' symbol set details have changed.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void donorSymSetChange ()
        {
            Boolean flagOK = true;

            if (_flagDonorSymSetUserSet)
                flagOK = checkDonorSymSetFile ();

            setDonorSymSetAttributes ();

            grpTargetSymSetDetails.Visibility = Visibility.Hidden;

            if (flagOK)
            {
                btnDefineSymSet.IsEnabled = true;
                btnGenerateSymSet.IsEnabled = false;
                btnLogSave.IsEnabled = false;
            }
            else
            {
                btnDefineSymSet.IsEnabled = false;
                btnGenerateSymSet.IsEnabled = false;
                btnLogSave.IsEnabled = false;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g i v e C r n t P D L                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void giveCrntPDL(ref ToolCommonData.ePrintLang crntPDL)
        {
            crntPDL = ToolCommonData.ePrintLang.PCL;
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

            _sizeCharSet = cSizeCharSet_8bit;

            initialiseSymSetList ();

            btnDefineSymSet.IsEnabled = true;
            btnGenerateSymSet.IsEnabled = false;
            btnLogSave.IsEnabled = false;

            //----------------------------------------------------------------//
            //                                                                //
            // Reinstate settings from persistent storage.                    //
            //                                                                //
            //----------------------------------------------------------------//

            metricsLoad();

     //     mapDisplay (_flagMapHex, _flagIgnoreC0, _flagIgnoreC1);

            cbDonorSymSet.SelectedIndex  = _indxDonorSymSetSubset;

            txtDonorSymSetFile.Text = _donorSymSetFile;

            if (_flagDonorSymSetMapPCL)
                rbDonorSymSetMapPCL.IsChecked = true;
            else
                rbDonorSymSetMapStd.IsChecked = true;

            if (_flagDonorSymSetUserSet)
                rbDonorSymSetUserSet.IsChecked = true;
            else
                rbDonorSymSetPreset.IsChecked = true;

            //----------------------------------------------------------------//

            setOffsetRanges ();

            _symSetMap8bit = new UInt16 [cSizeCharSet_8bit];
            _symSetMapTarget = _symSetMap8bit;

            _flagMultiByteSet = false;
            _flagMultiByteMap = false;

            btnCodePtClearAll.Visibility = Visibility.Hidden;

            setMultiByteData (_flagMultiByteSet);

            donorSymSetChange ();

            //----------------------------------------------------------------//

            initialiseCharCollReqLists ();

            //----------------------------------------------------------------//

            if (_flagMapHex)
                rbMapHex.IsChecked = true;
            else
                rbMapDec.IsChecked = true;

            if (_flagIgnoreC0)
                chkIgnoreC0.IsChecked = true;
            else
                chkIgnoreC0.IsChecked = false;

            if (_flagIgnoreC1)
                chkIgnoreC1.IsChecked = true;
            else
                chkIgnoreC1.IsChecked = false;

            if (_flagIndexUnicode)
                rbIndexUnicode.IsChecked = true;
            else
                rbIndexMSL.IsChecked = true;

            if (_flagCharCollReqSpecific)
            {
                rbCharReqSpecific.IsChecked = true;
                cbCharColls.Visibility = Visibility.Visible;
                tblkCharCollsText.Visibility = Visibility.Visible;

                populateCharCollReq (_flagIndexUnicode);

                if (_flagIndexUnicode)
                    _targetCharCollReq = _targetCharCollReqUnicode;
                else
                    _targetCharCollReq = _targetCharCollReqMSL;
            }
            else
            {
                rbCharReqSpecific.IsChecked = false;
                cbCharColls.Visibility = Visibility.Hidden;
                tblkCharCollsText.Visibility = Visibility.Hidden;

                if (_flagIndexUnicode)
                    _targetCharCollReq = _targetCharCollReqAllUnicode;
                else
                    _targetCharCollReq = _targetCharCollReqAllMSL;
            }

            setTargetCharCollReqValue (_targetCharCollReq);

            _initialised = true;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n i t i a l i s e C h a r C o l l R e q L i s t s                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initialise lists of character collections.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void initialiseCharCollReqLists ()
        {
            Int32 bitNo;

            UInt64 bitVal;
            
            Boolean bitSet;

            PCLCharCollections.eBitType bitType;

            //----------------------------------------------------------------//
            //                                                                //
            // Create lists of items and add to collection objects.           //
            //                                                                //
            //----------------------------------------------------------------//

            PCLCharCollItems items = new PCLCharCollItems ();

            _charCollReqListMSL     = items.loadReqListMSL ();
            _charCollReqListUnicode = items.loadReqListUnicode ();

            //----------------------------------------------------------------//
            //                                                                //
            // Obtain the non-specific ('all') bit arrays.                    //
            //                                                                //
            //----------------------------------------------------------------//

            _targetCharCollReqAllMSL = 0;

            foreach (PCLCharCollItem item in _charCollReqListMSL)
            {
                bitType = item.BitType;

                if (bitType != PCLCharCollections.eBitType.Collection)
                {
                    bitSet = item.IsChecked;
                 
                    if (bitSet)
                    {
                        bitNo = item.BitNo;
                        bitVal = ((UInt64)0x01) << bitNo;

                        _targetCharCollReqAllMSL =
                            (_targetCharCollReqAllMSL | bitVal);
                    }
                }
            }

            _targetCharCollReqAllUnicode = 0;

            foreach (PCLCharCollItem item in _charCollReqListUnicode)
            {
                bitType = item.BitType;

                if (bitType != PCLCharCollections.eBitType.Collection)
                {
                    bitSet = item.IsChecked;

                    if (bitSet)
                    {
                        bitNo = item.BitNo;
                        bitVal = ((UInt64)0x01) << bitNo;

                        _targetCharCollReqAllUnicode =
                            (_targetCharCollReqAllUnicode | bitVal);
                    }
                }
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Create PropertyChanged event handler for each item in the      //
            // collections.                                                   // 
            //                                                                //
            //----------------------------------------------------------------//

            foreach (PCLCharCollItem item in _charCollReqListMSL)
            {
                item.PropertyChanged +=
                    new PropertyChangedEventHandler (
                        cbCharCollsItem_PropertyChanged);
            }

            foreach (PCLCharCollItem item in _charCollReqListUnicode)
            {
                item.PropertyChanged +=
                    new PropertyChangedEventHandler (
                        cbCharCollsItem_PropertyChanged);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n i t i a l i s e S y m S e t L i s t                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Populate list of symbol sets with defined mappings.                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void initialiseSymSetList()
        {
            Int32 index;

            cbDonorSymSet.Items.Clear();

            _ctMappedSymSets = PCLSymbolSets.getCountMapped ();

            _subsetSymSets = new Int32[_ctMappedSymSets];

            PCLSymbolSets.getIndicesMapped (0, ref _subsetSymSets);

            for (Int32 i = 0; i < _ctMappedSymSets; i++)
            {
                index = _subsetSymSets[i];    
                cbDonorSymSet.Items.Add (PCLSymbolSets.getName(index));
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m a p D i s p l a y                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Populate the mapping array.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void mapDisplay (Boolean hexDisplay,
                                 Boolean ignoreC0,
                                 Boolean ignoreC1,
                                 UInt16  offset)
        {
            const Int32 noGlyph = 65535;

            UInt16 codeMin = 0,
                   codeMax = 0,
                   codeCt  = 0;

            String format;

            //----------------------------------------------------------------//

            if (hexDisplay)
            {
                format = "x4";

                lbMap0x_0.Content = "__0";
                lbMap0x_1.Content = "__1";
                lbMap0x_2.Content = "__2";
                lbMap0x_3.Content = "__3";
                lbMap0x_4.Content = "__4";
                lbMap0x_5.Content = "__5";
                lbMap0x_6.Content = "__6";
                lbMap0x_7.Content = "__7";
                lbMap0x_8.Content = "__8";
                lbMap0x_9.Content = "__9";
                lbMap0x_a.Content = "__a";
                lbMap0x_b.Content = "__b";
                lbMap0x_c.Content = "__c";
                lbMap0x_d.Content = "__d";
                lbMap0x_e.Content = "__e";
                lbMap0x_f.Content = "__f";

                lbMap0x0_.Content = "0x0_";
                lbMap0x1_.Content = "0x1_";
                lbMap0x2_.Content = "0x2_";
                lbMap0x3_.Content = "0x3_";
                lbMap0x4_.Content = "0x4_";
                lbMap0x5_.Content = "0x5_";
                lbMap0x6_.Content = "0x6_";
                lbMap0x7_.Content = "0x7_";
                lbMap0x8_.Content = "0x8_";
                lbMap0x9_.Content = "0x9_";
                lbMap0xa_.Content = "0xa_";
                lbMap0xb_.Content = "0xb_";
                lbMap0xc_.Content = "0xc_";
                lbMap0xd_.Content = "0xd_";
                lbMap0xe_.Content = "0xe_";
                lbMap0xf_.Content = "0xf_";
            }
            else
            {
                format = "";

                lbMap0x_0.Content = "+ 0";
                lbMap0x_1.Content = "+ 1";
                lbMap0x_2.Content = "+ 2";
                lbMap0x_3.Content = "+ 3";
                lbMap0x_4.Content = "+ 4";
                lbMap0x_5.Content = "+ 5";
                lbMap0x_6.Content = "+ 6";
                lbMap0x_7.Content = "+ 7";
                lbMap0x_8.Content = "+ 8";
                lbMap0x_9.Content = "+ 9";
                lbMap0x_a.Content = "+10";
                lbMap0x_b.Content = "+11";
                lbMap0x_c.Content = "+12";
                lbMap0x_d.Content = "+13";
                lbMap0x_e.Content = "+14";
                lbMap0x_f.Content = "+15";

                lbMap0x0_.Content = "  0";
                lbMap0x1_.Content = " 16";
                lbMap0x2_.Content = " 32";
                lbMap0x3_.Content = " 48";
                lbMap0x4_.Content = " 64";
                lbMap0x5_.Content = " 80";
                lbMap0x6_.Content = " 96";
                lbMap0x7_.Content = "112";
                lbMap0x8_.Content = "128";
                lbMap0x9_.Content = "144";
                lbMap0xa_.Content = "160";
                lbMap0xb_.Content = "176";
                lbMap0xc_.Content = "192";
                lbMap0xd_.Content = "208";
                lbMap0xe_.Content = "224";
                lbMap0xf_.Content = "240";
            }

            //----------------------------------------------------------------//

            if ((offset == 0) && (ignoreC0))
            {
                txtMap0x00.Text = noGlyph.ToString (format);
                txtMap0x01.Text = noGlyph.ToString (format);
                txtMap0x02.Text = noGlyph.ToString (format);
                txtMap0x03.Text = noGlyph.ToString (format);
                txtMap0x04.Text = noGlyph.ToString (format);
                txtMap0x05.Text = noGlyph.ToString (format);
                txtMap0x06.Text = noGlyph.ToString (format);
                txtMap0x07.Text = noGlyph.ToString (format);
                txtMap0x08.Text = noGlyph.ToString (format);
                txtMap0x09.Text = noGlyph.ToString (format);
                txtMap0x0a.Text = noGlyph.ToString (format);
                txtMap0x0b.Text = noGlyph.ToString (format);
                txtMap0x0c.Text = noGlyph.ToString (format);
                txtMap0x0d.Text = noGlyph.ToString (format);
                txtMap0x0e.Text = noGlyph.ToString (format);
                txtMap0x0f.Text = noGlyph.ToString (format);

                txtMap0x10.Text = noGlyph.ToString (format);
                txtMap0x11.Text = noGlyph.ToString (format);
                txtMap0x12.Text = noGlyph.ToString (format);
                txtMap0x13.Text = noGlyph.ToString (format);
                txtMap0x14.Text = noGlyph.ToString (format);
                txtMap0x15.Text = noGlyph.ToString (format);
                txtMap0x16.Text = noGlyph.ToString (format);
                txtMap0x17.Text = noGlyph.ToString (format);
                txtMap0x18.Text = noGlyph.ToString (format);
                txtMap0x19.Text = noGlyph.ToString (format);
                txtMap0x1a.Text = noGlyph.ToString (format);
                txtMap0x1b.Text = noGlyph.ToString (format);
                txtMap0x1c.Text = noGlyph.ToString (format);
                txtMap0x1d.Text = noGlyph.ToString (format);
                txtMap0x1e.Text = noGlyph.ToString (format);
                txtMap0x1f.Text = noGlyph.ToString (format);

                txtMap0x00.IsEnabled = false;
                txtMap0x01.IsEnabled = false;
                txtMap0x02.IsEnabled = false;
                txtMap0x03.IsEnabled = false;
                txtMap0x04.IsEnabled = false;
                txtMap0x05.IsEnabled = false;
                txtMap0x06.IsEnabled = false;
                txtMap0x07.IsEnabled = false;
                txtMap0x08.IsEnabled = false;
                txtMap0x09.IsEnabled = false;
                txtMap0x0a.IsEnabled = false;
                txtMap0x0b.IsEnabled = false;
                txtMap0x0c.IsEnabled = false;
                txtMap0x0d.IsEnabled = false;
                txtMap0x0e.IsEnabled = false;
                txtMap0x0f.IsEnabled = false;

                txtMap0x10.IsEnabled = false;
                txtMap0x11.IsEnabled = false;
                txtMap0x12.IsEnabled = false;
                txtMap0x13.IsEnabled = false;
                txtMap0x14.IsEnabled = false;
                txtMap0x15.IsEnabled = false;
                txtMap0x16.IsEnabled = false;
                txtMap0x17.IsEnabled = false;
                txtMap0x18.IsEnabled = false;
                txtMap0x19.IsEnabled = false;
                txtMap0x1a.IsEnabled = false;
                txtMap0x1b.IsEnabled = false;
                txtMap0x1c.IsEnabled = false;
                txtMap0x1d.IsEnabled = false;
                txtMap0x1e.IsEnabled = false;
                txtMap0x1f.IsEnabled = false;
            }
            else
            {
                txtMap0x00.Text = _symSetMapTarget [offset + 0x00].ToString (format);
                txtMap0x01.Text = _symSetMapTarget [offset + 0x01].ToString (format);
                txtMap0x02.Text = _symSetMapTarget [offset + 0x02].ToString (format);
                txtMap0x03.Text = _symSetMapTarget [offset + 0x03].ToString (format);
                txtMap0x04.Text = _symSetMapTarget [offset + 0x04].ToString (format);
                txtMap0x05.Text = _symSetMapTarget [offset + 0x05].ToString (format);
                txtMap0x06.Text = _symSetMapTarget [offset + 0x06].ToString (format);
                txtMap0x07.Text = _symSetMapTarget [offset + 0x07].ToString (format);
                txtMap0x08.Text = _symSetMapTarget [offset + 0x08].ToString (format);
                txtMap0x09.Text = _symSetMapTarget [offset + 0x09].ToString (format);
                txtMap0x0a.Text = _symSetMapTarget [offset + 0x0a].ToString (format);
                txtMap0x0b.Text = _symSetMapTarget [offset + 0x0b].ToString (format);
                txtMap0x0c.Text = _symSetMapTarget [offset + 0x0c].ToString (format);
                txtMap0x0d.Text = _symSetMapTarget [offset + 0x0d].ToString (format);
                txtMap0x0e.Text = _symSetMapTarget [offset + 0x0e].ToString (format);
                txtMap0x0f.Text = _symSetMapTarget [offset + 0x0f].ToString (format);

                txtMap0x10.Text = _symSetMapTarget [offset + 0x10].ToString (format);
                txtMap0x11.Text = _symSetMapTarget [offset + 0x11].ToString (format);
                txtMap0x12.Text = _symSetMapTarget [offset + 0x12].ToString (format);
                txtMap0x13.Text = _symSetMapTarget [offset + 0x13].ToString (format);
                txtMap0x14.Text = _symSetMapTarget [offset + 0x14].ToString (format);
                txtMap0x15.Text = _symSetMapTarget [offset + 0x15].ToString (format);
                txtMap0x16.Text = _symSetMapTarget [offset + 0x16].ToString (format);
                txtMap0x17.Text = _symSetMapTarget [offset + 0x17].ToString (format);
                txtMap0x18.Text = _symSetMapTarget [offset + 0x18].ToString (format);
                txtMap0x19.Text = _symSetMapTarget [offset + 0x19].ToString (format);
                txtMap0x1a.Text = _symSetMapTarget [offset + 0x1a].ToString (format);
                txtMap0x1b.Text = _symSetMapTarget [offset + 0x1b].ToString (format);
                txtMap0x1c.Text = _symSetMapTarget [offset + 0x1c].ToString (format);
                txtMap0x1d.Text = _symSetMapTarget [offset + 0x1d].ToString (format);
                txtMap0x1e.Text = _symSetMapTarget [offset + 0x1e].ToString (format);
                txtMap0x1f.Text = _symSetMapTarget [offset + 0x1f].ToString (format);

                txtMap0x00.IsEnabled = true;
                txtMap0x01.IsEnabled = true;
                txtMap0x02.IsEnabled = true;
                txtMap0x03.IsEnabled = true;
                txtMap0x04.IsEnabled = true;
                txtMap0x05.IsEnabled = true;
                txtMap0x06.IsEnabled = true;
                txtMap0x07.IsEnabled = true;
                txtMap0x08.IsEnabled = true;
                txtMap0x09.IsEnabled = true;
                txtMap0x0a.IsEnabled = true;
                txtMap0x0b.IsEnabled = true;
                txtMap0x0c.IsEnabled = true;
                txtMap0x0d.IsEnabled = true;
                txtMap0x0e.IsEnabled = true;
                txtMap0x0f.IsEnabled = true;

                txtMap0x10.IsEnabled = true;
                txtMap0x11.IsEnabled = true;
                txtMap0x12.IsEnabled = true;
                txtMap0x13.IsEnabled = true;
                txtMap0x14.IsEnabled = true;
                txtMap0x15.IsEnabled = true;
                txtMap0x16.IsEnabled = true;
                txtMap0x17.IsEnabled = true;
                txtMap0x18.IsEnabled = true;
                txtMap0x19.IsEnabled = true;
                txtMap0x1a.IsEnabled = true;
                txtMap0x1b.IsEnabled = true;
                txtMap0x1c.IsEnabled = true;
                txtMap0x1d.IsEnabled = true;
                txtMap0x1e.IsEnabled = true;
                txtMap0x1f.IsEnabled = true;
            }

            //----------------------------------------------------------------//

            txtMap0x20.Text = _symSetMapTarget [offset + 0x20].ToString (format);
            txtMap0x21.Text = _symSetMapTarget [offset + 0x21].ToString (format);
            txtMap0x22.Text = _symSetMapTarget [offset + 0x22].ToString (format);
            txtMap0x23.Text = _symSetMapTarget [offset + 0x23].ToString (format);
            txtMap0x24.Text = _symSetMapTarget [offset + 0x24].ToString (format);
            txtMap0x25.Text = _symSetMapTarget [offset + 0x25].ToString (format);
            txtMap0x26.Text = _symSetMapTarget [offset + 0x26].ToString (format);
            txtMap0x27.Text = _symSetMapTarget [offset + 0x27].ToString (format);
            txtMap0x28.Text = _symSetMapTarget [offset + 0x28].ToString (format);
            txtMap0x29.Text = _symSetMapTarget [offset + 0x29].ToString (format);
            txtMap0x2a.Text = _symSetMapTarget [offset + 0x2a].ToString (format);
            txtMap0x2b.Text = _symSetMapTarget [offset + 0x2b].ToString (format);
            txtMap0x2c.Text = _symSetMapTarget [offset + 0x2c].ToString (format);
            txtMap0x2d.Text = _symSetMapTarget [offset + 0x2d].ToString (format);
            txtMap0x2e.Text = _symSetMapTarget [offset + 0x2e].ToString (format);
            txtMap0x2f.Text = _symSetMapTarget [offset + 0x2f].ToString (format);

            txtMap0x30.Text = _symSetMapTarget [offset + 0x30].ToString (format);
            txtMap0x31.Text = _symSetMapTarget [offset + 0x31].ToString (format);
            txtMap0x32.Text = _symSetMapTarget [offset + 0x32].ToString (format);
            txtMap0x33.Text = _symSetMapTarget [offset + 0x33].ToString (format);
            txtMap0x34.Text = _symSetMapTarget [offset + 0x34].ToString (format);
            txtMap0x35.Text = _symSetMapTarget [offset + 0x35].ToString (format);
            txtMap0x36.Text = _symSetMapTarget [offset + 0x36].ToString (format);
            txtMap0x37.Text = _symSetMapTarget [offset + 0x37].ToString (format);
            txtMap0x38.Text = _symSetMapTarget [offset + 0x38].ToString (format);
            txtMap0x39.Text = _symSetMapTarget [offset + 0x39].ToString (format);
            txtMap0x3a.Text = _symSetMapTarget [offset + 0x3a].ToString (format);
            txtMap0x3b.Text = _symSetMapTarget [offset + 0x3b].ToString (format);
            txtMap0x3c.Text = _symSetMapTarget [offset + 0x3c].ToString (format);
            txtMap0x3d.Text = _symSetMapTarget [offset + 0x3d].ToString (format);
            txtMap0x3e.Text = _symSetMapTarget [offset + 0x3e].ToString (format);
            txtMap0x3f.Text = _symSetMapTarget [offset + 0x3f].ToString (format);

            txtMap0x40.Text = _symSetMapTarget [offset + 0x40].ToString (format);
            txtMap0x41.Text = _symSetMapTarget [offset + 0x41].ToString (format);
            txtMap0x42.Text = _symSetMapTarget [offset + 0x42].ToString (format);
            txtMap0x43.Text = _symSetMapTarget [offset + 0x43].ToString (format);
            txtMap0x44.Text = _symSetMapTarget [offset + 0x44].ToString (format);
            txtMap0x45.Text = _symSetMapTarget [offset + 0x45].ToString (format);
            txtMap0x46.Text = _symSetMapTarget [offset + 0x46].ToString (format);
            txtMap0x47.Text = _symSetMapTarget [offset + 0x47].ToString (format);
            txtMap0x48.Text = _symSetMapTarget [offset + 0x48].ToString (format);
            txtMap0x49.Text = _symSetMapTarget [offset + 0x49].ToString (format);
            txtMap0x4a.Text = _symSetMapTarget [offset + 0x4a].ToString (format);
            txtMap0x4b.Text = _symSetMapTarget [offset + 0x4b].ToString (format);
            txtMap0x4c.Text = _symSetMapTarget [offset + 0x4c].ToString (format);
            txtMap0x4d.Text = _symSetMapTarget [offset + 0x4d].ToString (format);
            txtMap0x4e.Text = _symSetMapTarget [offset + 0x4e].ToString (format);
            txtMap0x4f.Text = _symSetMapTarget [offset + 0x4f].ToString (format);

            txtMap0x50.Text = _symSetMapTarget [offset + 0x50].ToString (format);
            txtMap0x51.Text = _symSetMapTarget [offset + 0x51].ToString (format);
            txtMap0x52.Text = _symSetMapTarget [offset + 0x52].ToString (format);
            txtMap0x53.Text = _symSetMapTarget [offset + 0x53].ToString (format);
            txtMap0x54.Text = _symSetMapTarget [offset + 0x54].ToString (format);
            txtMap0x55.Text = _symSetMapTarget [offset + 0x55].ToString (format);
            txtMap0x56.Text = _symSetMapTarget [offset + 0x56].ToString (format);
            txtMap0x57.Text = _symSetMapTarget [offset + 0x57].ToString (format);
            txtMap0x58.Text = _symSetMapTarget [offset + 0x58].ToString (format);
            txtMap0x59.Text = _symSetMapTarget [offset + 0x59].ToString (format);
            txtMap0x5a.Text = _symSetMapTarget [offset + 0x5a].ToString (format);
            txtMap0x5b.Text = _symSetMapTarget [offset + 0x5b].ToString (format);
            txtMap0x5c.Text = _symSetMapTarget [offset + 0x5c].ToString (format);
            txtMap0x5d.Text = _symSetMapTarget [offset + 0x5d].ToString (format);
            txtMap0x5e.Text = _symSetMapTarget [offset + 0x5e].ToString (format);
            txtMap0x5f.Text = _symSetMapTarget [offset + 0x5f].ToString (format);

            txtMap0x60.Text = _symSetMapTarget [offset + 0x60].ToString (format);
            txtMap0x61.Text = _symSetMapTarget [offset + 0x61].ToString (format);
            txtMap0x62.Text = _symSetMapTarget [offset + 0x62].ToString (format);
            txtMap0x63.Text = _symSetMapTarget [offset + 0x63].ToString (format);
            txtMap0x64.Text = _symSetMapTarget [offset + 0x64].ToString (format);
            txtMap0x65.Text = _symSetMapTarget [offset + 0x65].ToString (format);
            txtMap0x66.Text = _symSetMapTarget [offset + 0x66].ToString (format);
            txtMap0x67.Text = _symSetMapTarget [offset + 0x67].ToString (format);
            txtMap0x68.Text = _symSetMapTarget [offset + 0x68].ToString (format);
            txtMap0x69.Text = _symSetMapTarget [offset + 0x69].ToString (format);
            txtMap0x6a.Text = _symSetMapTarget [offset + 0x6a].ToString (format);
            txtMap0x6b.Text = _symSetMapTarget [offset + 0x6b].ToString (format);
            txtMap0x6c.Text = _symSetMapTarget [offset + 0x6c].ToString (format);
            txtMap0x6d.Text = _symSetMapTarget [offset + 0x6d].ToString (format);
            txtMap0x6e.Text = _symSetMapTarget [offset + 0x6e].ToString (format);
            txtMap0x6f.Text = _symSetMapTarget [offset + 0x6f].ToString (format);

            txtMap0x70.Text = _symSetMapTarget [offset + 0x70].ToString (format);
            txtMap0x71.Text = _symSetMapTarget [offset + 0x71].ToString (format);
            txtMap0x72.Text = _symSetMapTarget [offset + 0x72].ToString (format);
            txtMap0x73.Text = _symSetMapTarget [offset + 0x73].ToString (format);
            txtMap0x74.Text = _symSetMapTarget [offset + 0x74].ToString (format);
            txtMap0x75.Text = _symSetMapTarget [offset + 0x75].ToString (format);
            txtMap0x76.Text = _symSetMapTarget [offset + 0x76].ToString (format);
            txtMap0x77.Text = _symSetMapTarget [offset + 0x77].ToString (format);
            txtMap0x78.Text = _symSetMapTarget [offset + 0x78].ToString (format);
            txtMap0x79.Text = _symSetMapTarget [offset + 0x79].ToString (format);
            txtMap0x7a.Text = _symSetMapTarget [offset + 0x7a].ToString (format);
            txtMap0x7b.Text = _symSetMapTarget [offset + 0x7b].ToString (format);
            txtMap0x7c.Text = _symSetMapTarget [offset + 0x7c].ToString (format);
            txtMap0x7d.Text = _symSetMapTarget [offset + 0x7d].ToString (format);
            txtMap0x7e.Text = _symSetMapTarget [offset + 0x7e].ToString (format);
            txtMap0x7f.Text = _symSetMapTarget [offset + 0x7f].ToString (format);

            //----------------------------------------------------------------//

            if ((offset == 0) && (ignoreC1))
            {
                txtMap0x80.Text = noGlyph.ToString (format);
                txtMap0x81.Text = noGlyph.ToString (format);
                txtMap0x82.Text = noGlyph.ToString (format);
                txtMap0x83.Text = noGlyph.ToString (format);
                txtMap0x84.Text = noGlyph.ToString (format);
                txtMap0x85.Text = noGlyph.ToString (format);
                txtMap0x86.Text = noGlyph.ToString (format);
                txtMap0x87.Text = noGlyph.ToString (format);
                txtMap0x88.Text = noGlyph.ToString (format);
                txtMap0x89.Text = noGlyph.ToString (format);
                txtMap0x8a.Text = noGlyph.ToString (format);
                txtMap0x8b.Text = noGlyph.ToString (format);
                txtMap0x8c.Text = noGlyph.ToString (format);
                txtMap0x8d.Text = noGlyph.ToString (format);
                txtMap0x8e.Text = noGlyph.ToString (format);
                txtMap0x8f.Text = noGlyph.ToString (format);

                txtMap0x90.Text = noGlyph.ToString (format);
                txtMap0x91.Text = noGlyph.ToString (format);
                txtMap0x92.Text = noGlyph.ToString (format);
                txtMap0x93.Text = noGlyph.ToString (format);
                txtMap0x94.Text = noGlyph.ToString (format);
                txtMap0x95.Text = noGlyph.ToString (format);
                txtMap0x96.Text = noGlyph.ToString (format);
                txtMap0x97.Text = noGlyph.ToString (format);
                txtMap0x98.Text = noGlyph.ToString (format);
                txtMap0x99.Text = noGlyph.ToString (format);
                txtMap0x9a.Text = noGlyph.ToString (format);
                txtMap0x9b.Text = noGlyph.ToString (format);
                txtMap0x9c.Text = noGlyph.ToString (format);
                txtMap0x9d.Text = noGlyph.ToString (format);
                txtMap0x9e.Text = noGlyph.ToString (format);
                txtMap0x9f.Text = noGlyph.ToString (format);

                txtMap0x80.IsEnabled = false;
                txtMap0x81.IsEnabled = false;
                txtMap0x82.IsEnabled = false;
                txtMap0x83.IsEnabled = false;
                txtMap0x84.IsEnabled = false;
                txtMap0x85.IsEnabled = false;
                txtMap0x86.IsEnabled = false;
                txtMap0x87.IsEnabled = false;
                txtMap0x88.IsEnabled = false;
                txtMap0x89.IsEnabled = false;
                txtMap0x8a.IsEnabled = false;
                txtMap0x8b.IsEnabled = false;
                txtMap0x8c.IsEnabled = false;
                txtMap0x8d.IsEnabled = false;
                txtMap0x8e.IsEnabled = false;
                txtMap0x8f.IsEnabled = false;

                txtMap0x90.IsEnabled = false;
                txtMap0x91.IsEnabled = false;
                txtMap0x92.IsEnabled = false;
                txtMap0x93.IsEnabled = false;
                txtMap0x94.IsEnabled = false;
                txtMap0x95.IsEnabled = false;
                txtMap0x96.IsEnabled = false;
                txtMap0x97.IsEnabled = false;
                txtMap0x98.IsEnabled = false;
                txtMap0x99.IsEnabled = false;
                txtMap0x9a.IsEnabled = false;
                txtMap0x9b.IsEnabled = false;
                txtMap0x9c.IsEnabled = false;
                txtMap0x9d.IsEnabled = false;
                txtMap0x9e.IsEnabled = false;
                txtMap0x9f.IsEnabled = false;
            }
            else
            {
                txtMap0x80.Text = _symSetMapTarget [offset + 0x80].ToString (format);
                txtMap0x81.Text = _symSetMapTarget [offset + 0x81].ToString (format);
                txtMap0x82.Text = _symSetMapTarget [offset + 0x82].ToString (format);
                txtMap0x83.Text = _symSetMapTarget [offset + 0x83].ToString (format);
                txtMap0x84.Text = _symSetMapTarget [offset + 0x84].ToString (format);
                txtMap0x85.Text = _symSetMapTarget [offset + 0x85].ToString (format);
                txtMap0x86.Text = _symSetMapTarget [offset + 0x86].ToString (format);
                txtMap0x87.Text = _symSetMapTarget [offset + 0x87].ToString (format);
                txtMap0x88.Text = _symSetMapTarget [offset + 0x88].ToString (format);
                txtMap0x89.Text = _symSetMapTarget [offset + 0x89].ToString (format);
                txtMap0x8a.Text = _symSetMapTarget [offset + 0x8a].ToString (format);
                txtMap0x8b.Text = _symSetMapTarget [offset + 0x8b].ToString (format);
                txtMap0x8c.Text = _symSetMapTarget [offset + 0x8c].ToString (format);
                txtMap0x8d.Text = _symSetMapTarget [offset + 0x8d].ToString (format);
                txtMap0x8e.Text = _symSetMapTarget [offset + 0x8e].ToString (format);
                txtMap0x8f.Text = _symSetMapTarget [offset + 0x8f].ToString (format);

                txtMap0x90.Text = _symSetMapTarget [offset + 0x90].ToString (format);
                txtMap0x91.Text = _symSetMapTarget [offset + 0x91].ToString (format);
                txtMap0x92.Text = _symSetMapTarget [offset + 0x92].ToString (format);
                txtMap0x93.Text = _symSetMapTarget [offset + 0x93].ToString (format);
                txtMap0x94.Text = _symSetMapTarget [offset + 0x94].ToString (format);
                txtMap0x95.Text = _symSetMapTarget [offset + 0x95].ToString (format);
                txtMap0x96.Text = _symSetMapTarget [offset + 0x96].ToString (format);
                txtMap0x97.Text = _symSetMapTarget [offset + 0x97].ToString (format);
                txtMap0x98.Text = _symSetMapTarget [offset + 0x98].ToString (format);
                txtMap0x99.Text = _symSetMapTarget [offset + 0x99].ToString (format);
                txtMap0x9a.Text = _symSetMapTarget [offset + 0x9a].ToString (format);
                txtMap0x9b.Text = _symSetMapTarget [offset + 0x9b].ToString (format);
                txtMap0x9c.Text = _symSetMapTarget [offset + 0x9c].ToString (format);
                txtMap0x9d.Text = _symSetMapTarget [offset + 0x9d].ToString (format);
                txtMap0x9e.Text = _symSetMapTarget [offset + 0x9e].ToString (format);
                txtMap0x9f.Text = _symSetMapTarget [offset + 0x9f].ToString (format);

                txtMap0x80.IsEnabled = true;
                txtMap0x81.IsEnabled = true;
                txtMap0x82.IsEnabled = true;
                txtMap0x83.IsEnabled = true;
                txtMap0x84.IsEnabled = true;
                txtMap0x85.IsEnabled = true;
                txtMap0x86.IsEnabled = true;
                txtMap0x87.IsEnabled = true;
                txtMap0x88.IsEnabled = true;
                txtMap0x89.IsEnabled = true;
                txtMap0x8a.IsEnabled = true;
                txtMap0x8b.IsEnabled = true;
                txtMap0x8c.IsEnabled = true;
                txtMap0x8d.IsEnabled = true;
                txtMap0x8e.IsEnabled = true;
                txtMap0x8f.IsEnabled = true;

                txtMap0x90.IsEnabled = true;
                txtMap0x91.IsEnabled = true;
                txtMap0x92.IsEnabled = true;
                txtMap0x93.IsEnabled = true;
                txtMap0x94.IsEnabled = true;
                txtMap0x95.IsEnabled = true;
                txtMap0x96.IsEnabled = true;
                txtMap0x97.IsEnabled = true;
                txtMap0x98.IsEnabled = true;
                txtMap0x99.IsEnabled = true;
                txtMap0x9a.IsEnabled = true;
                txtMap0x9b.IsEnabled = true;
                txtMap0x9c.IsEnabled = true;
                txtMap0x9d.IsEnabled = true;
                txtMap0x9e.IsEnabled = true;
                txtMap0x9f.IsEnabled = true;
            }

            //----------------------------------------------------------------//

            txtMap0xa0.Text = _symSetMapTarget [offset + 0xa0].ToString (format);
            txtMap0xa1.Text = _symSetMapTarget [offset + 0xa1].ToString (format);
            txtMap0xa2.Text = _symSetMapTarget [offset + 0xa2].ToString (format);
            txtMap0xa3.Text = _symSetMapTarget [offset + 0xa3].ToString (format);
            txtMap0xa4.Text = _symSetMapTarget [offset + 0xa4].ToString (format);
            txtMap0xa5.Text = _symSetMapTarget [offset + 0xa5].ToString (format);
            txtMap0xa6.Text = _symSetMapTarget [offset + 0xa6].ToString (format);
            txtMap0xa7.Text = _symSetMapTarget [offset + 0xa7].ToString (format);
            txtMap0xa8.Text = _symSetMapTarget [offset + 0xa8].ToString (format);
            txtMap0xa9.Text = _symSetMapTarget [offset + 0xa9].ToString (format);
            txtMap0xaa.Text = _symSetMapTarget [offset + 0xaa].ToString (format);
            txtMap0xab.Text = _symSetMapTarget [offset + 0xab].ToString (format);
            txtMap0xac.Text = _symSetMapTarget [offset + 0xac].ToString (format);
            txtMap0xad.Text = _symSetMapTarget [offset + 0xad].ToString (format);
            txtMap0xae.Text = _symSetMapTarget [offset + 0xae].ToString (format);
            txtMap0xaf.Text = _symSetMapTarget [offset + 0xaf].ToString (format);

            txtMap0xb0.Text = _symSetMapTarget [offset + 0xb0].ToString (format);
            txtMap0xb1.Text = _symSetMapTarget [offset + 0xb1].ToString (format);
            txtMap0xb2.Text = _symSetMapTarget [offset + 0xb2].ToString (format);
            txtMap0xb3.Text = _symSetMapTarget [offset + 0xb3].ToString (format);
            txtMap0xb4.Text = _symSetMapTarget [offset + 0xb4].ToString (format);
            txtMap0xb5.Text = _symSetMapTarget [offset + 0xb5].ToString (format);
            txtMap0xb6.Text = _symSetMapTarget [offset + 0xb6].ToString (format);
            txtMap0xb7.Text = _symSetMapTarget [offset + 0xb7].ToString (format);
            txtMap0xb8.Text = _symSetMapTarget [offset + 0xb8].ToString (format);
            txtMap0xb9.Text = _symSetMapTarget [offset + 0xb9].ToString (format);
            txtMap0xba.Text = _symSetMapTarget [offset + 0xba].ToString (format);
            txtMap0xbb.Text = _symSetMapTarget [offset + 0xbb].ToString (format);
            txtMap0xbc.Text = _symSetMapTarget [offset + 0xbc].ToString (format);
            txtMap0xbd.Text = _symSetMapTarget [offset + 0xbd].ToString (format);
            txtMap0xbe.Text = _symSetMapTarget [offset + 0xbe].ToString (format);
            txtMap0xbf.Text = _symSetMapTarget [offset + 0xbf].ToString (format);

            txtMap0xc0.Text = _symSetMapTarget [offset + 0xc0].ToString (format);
            txtMap0xc1.Text = _symSetMapTarget [offset + 0xc1].ToString (format);
            txtMap0xc2.Text = _symSetMapTarget [offset + 0xc2].ToString (format);
            txtMap0xc3.Text = _symSetMapTarget [offset + 0xc3].ToString (format);
            txtMap0xc4.Text = _symSetMapTarget [offset + 0xc4].ToString (format);
            txtMap0xc5.Text = _symSetMapTarget [offset + 0xc5].ToString (format);
            txtMap0xc6.Text = _symSetMapTarget [offset + 0xc6].ToString (format);
            txtMap0xc7.Text = _symSetMapTarget [offset + 0xc7].ToString (format);
            txtMap0xc8.Text = _symSetMapTarget [offset + 0xc8].ToString (format);
            txtMap0xc9.Text = _symSetMapTarget [offset + 0xc9].ToString (format);
            txtMap0xca.Text = _symSetMapTarget [offset + 0xca].ToString (format);
            txtMap0xcb.Text = _symSetMapTarget [offset + 0xcb].ToString (format);
            txtMap0xcc.Text = _symSetMapTarget [offset + 0xcc].ToString (format);
            txtMap0xcd.Text = _symSetMapTarget [offset + 0xcd].ToString (format);
            txtMap0xce.Text = _symSetMapTarget [offset + 0xce].ToString (format);
            txtMap0xcf.Text = _symSetMapTarget [offset + 0xcf].ToString (format);

            txtMap0xd0.Text = _symSetMapTarget [offset + 0xd0].ToString (format);
            txtMap0xd1.Text = _symSetMapTarget [offset + 0xd1].ToString (format);
            txtMap0xd2.Text = _symSetMapTarget [offset + 0xd2].ToString (format);
            txtMap0xd3.Text = _symSetMapTarget [offset + 0xd3].ToString (format);
            txtMap0xd4.Text = _symSetMapTarget [offset + 0xd4].ToString (format);
            txtMap0xd5.Text = _symSetMapTarget [offset + 0xd5].ToString (format);
            txtMap0xd6.Text = _symSetMapTarget [offset + 0xd6].ToString (format);
            txtMap0xd7.Text = _symSetMapTarget [offset + 0xd7].ToString (format);
            txtMap0xd8.Text = _symSetMapTarget [offset + 0xd8].ToString (format);
            txtMap0xd9.Text = _symSetMapTarget [offset + 0xd9].ToString (format);
            txtMap0xda.Text = _symSetMapTarget [offset + 0xda].ToString (format);
            txtMap0xdb.Text = _symSetMapTarget [offset + 0xdb].ToString (format);
            txtMap0xdc.Text = _symSetMapTarget [offset + 0xdc].ToString (format);
            txtMap0xdd.Text = _symSetMapTarget [offset + 0xdd].ToString (format);
            txtMap0xde.Text = _symSetMapTarget [offset + 0xde].ToString (format);
            txtMap0xdf.Text = _symSetMapTarget [offset + 0xdf].ToString (format);

            txtMap0xe0.Text = _symSetMapTarget [offset + 0xe0].ToString (format);
            txtMap0xe1.Text = _symSetMapTarget [offset + 0xe1].ToString (format);
            txtMap0xe2.Text = _symSetMapTarget [offset + 0xe2].ToString (format);
            txtMap0xe3.Text = _symSetMapTarget [offset + 0xe3].ToString (format);
            txtMap0xe4.Text = _symSetMapTarget [offset + 0xe4].ToString (format);
            txtMap0xe5.Text = _symSetMapTarget [offset + 0xe5].ToString (format);
            txtMap0xe6.Text = _symSetMapTarget [offset + 0xe6].ToString (format);
            txtMap0xe7.Text = _symSetMapTarget [offset + 0xe7].ToString (format);
            txtMap0xe8.Text = _symSetMapTarget [offset + 0xe8].ToString (format);
            txtMap0xe9.Text = _symSetMapTarget [offset + 0xe9].ToString (format);
            txtMap0xea.Text = _symSetMapTarget [offset + 0xea].ToString (format);
            txtMap0xeb.Text = _symSetMapTarget [offset + 0xeb].ToString (format);
            txtMap0xec.Text = _symSetMapTarget [offset + 0xec].ToString (format);
            txtMap0xed.Text = _symSetMapTarget [offset + 0xed].ToString (format);
            txtMap0xee.Text = _symSetMapTarget [offset + 0xee].ToString (format);
            txtMap0xef.Text = _symSetMapTarget [offset + 0xef].ToString (format);

            txtMap0xf0.Text = _symSetMapTarget [offset + 0xf0].ToString (format);
            txtMap0xf1.Text = _symSetMapTarget [offset + 0xf1].ToString (format);
            txtMap0xf2.Text = _symSetMapTarget [offset + 0xf2].ToString (format);
            txtMap0xf3.Text = _symSetMapTarget [offset + 0xf3].ToString (format);
            txtMap0xf4.Text = _symSetMapTarget [offset + 0xf4].ToString (format);
            txtMap0xf5.Text = _symSetMapTarget [offset + 0xf5].ToString (format);
            txtMap0xf6.Text = _symSetMapTarget [offset + 0xf6].ToString (format);
            txtMap0xf7.Text = _symSetMapTarget [offset + 0xf7].ToString (format);
            txtMap0xf8.Text = _symSetMapTarget [offset + 0xf8].ToString (format);
            txtMap0xf9.Text = _symSetMapTarget [offset + 0xf9].ToString (format);
            txtMap0xfa.Text = _symSetMapTarget [offset + 0xfa].ToString (format);
            txtMap0xfb.Text = _symSetMapTarget [offset + 0xfb].ToString (format);
            txtMap0xfc.Text = _symSetMapTarget [offset + 0xfc].ToString (format);
            txtMap0xfd.Text = _symSetMapTarget [offset + 0xfd].ToString (format);
            txtMap0xfe.Text = _symSetMapTarget [offset + 0xfe].ToString (format);
            txtMap0xff.Text = _symSetMapTarget [offset + 0xff].ToString (format);

            //----------------------------------------------------------------//

            mapMetrics (ignoreC0, ignoreC1, _sizeCharSet,
                        ref codeMin, ref codeMax, ref codeCt,
                        ref _targetSymSetType);

            txtCodeMin.Text = codeMin.ToString (format);
            txtCodeMax.Text = codeMax.ToString (format);
            txtCodeCt.Text = codeCt.ToString (format);

            if (_flagMultiByteSet)
            {
                if (hexDisplay)
                    txtMapOffset.Text = "0x" + offset.ToString (format) + "+";
                else
                    txtMapOffset.Text = offset.ToString (format) + "+";

                txtOffsetRangeMin.Text = _offsetMin.ToString (format);
                txtOffsetRangeMax.Text = _offsetMax.ToString (format);
            }

            grpTargetSymSetDetails.Visibility = Visibility.Visible;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m a p M e t r i c s                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Calculate the code-point metrics: first and last codes, and count. //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void mapMetrics (Boolean ignoreC0,
                                 Boolean ignoreC1,
                                 Int32   setSize,
                                 ref UInt16 codeMin,
                                 ref UInt16 codeMax,
                                 ref UInt16 codeCt,
                                 ref PCLSymSetTypes.eIndex symSetType)
        {
            Boolean usesC1Range = false;
            Boolean codePointSig = false;

            //----------------------------------------------------------------//
            //                                                                //
            // Minimum (significant) code-point.                              //
            //                                                                //
            //----------------------------------------------------------------//

            codeMin = 0;

            for (Int32 i = 0; i < setSize; i++)
            {
                if ((i >= cCodePointC0Min) && (i <= cCodePointC0Max))
                {
                    if (ignoreC0)
                        codePointSig = false;
                    else
                        codePointSig = true;
                }
                else if ((i >= cCodePointC1Min) && (i <= cCodePointC1Max))
                {
                    if (ignoreC1)
                        codePointSig = false;
                    else
                        codePointSig = true;
                }
                else
                {
                    codePointSig = true;
                }

                if ((codePointSig) && (_symSetMapTarget [i] != cCodePointUnused))
                {
                    codeMin = (UInt16) i;

                    i = setSize; // end loop
                }
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Maximum (significant) code-point.                              //
            //                                                                //
            //----------------------------------------------------------------//

            codeMax = 0;

            for (Int32 i = (setSize - 1); i >= 0; i--)
            {
                if ((i >= cCodePointC0Min) && (i <= cCodePointC0Max))
                {
                    if (ignoreC0)
                        codePointSig = false;
                    else
                        codePointSig = true;
                }
                else if ((i >= cCodePointC1Min) && (i <= cCodePointC1Max))
                {
                    if (ignoreC1)
                        codePointSig = false;
                    else
                        codePointSig = true;
                }
                else
                {
                    codePointSig = true;
                }

                if ((codePointSig) && (_symSetMapTarget [i] != cCodePointUnused))
                {
                    codeMax = (UInt16) i;

                    i = -1; // end loop
                }
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Count of (significant) code-points.                            //
            //                                                                //
            //----------------------------------------------------------------//

            codeCt = 0;

            for (Int32 i = 0; i < setSize; i++)
            {
                if ((i >= cCodePointC0Min) && (i <= cCodePointC0Max))
                {
                    if (ignoreC0)
                        codePointSig = false;
                    else
                        codePointSig = true;
                }
                else if ((i >= cCodePointC1Min) && (i <= cCodePointC1Max))
                {
                    if (ignoreC1)
                        codePointSig = false;
                    else
                        codePointSig = true;
                }
                else
                {
                    codePointSig = true;
                }

                if ((codePointSig) && (_symSetMapTarget [i] != cCodePointUnused))
                {
                    codeCt++;
                }
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Determine symbol set type.                                     //
            //                                                                //
            //----------------------------------------------------------------//

            usesC1Range = false;

            if (! ignoreC1)
            {
                Int32 checkMin;
                Int32 checkMax;

                if (codeMin < cCodePointC1Min)
                    checkMin = cCodePointC1Min;
                else
                    checkMin = codeMin;

                if (codeMax > cCodePointC1Max)
                    checkMax = cCodePointC1Max;
                else
                    checkMax = codeMax;

                for (Int32 i = checkMin; i < checkMax; i++)
                {
                    if (_symSetMapTarget [i] != cCodePointUnused)
                        usesC1Range = true;
                }
            }

            //----------------------------------------------------------------//

            if (codeMax > 0xff)
                symSetType = PCLSymSetTypes.eIndex.Bound_16bit;
            else if ((codeMin >= 0x20) && (codeMax <= 0x7f))
                symSetType = PCLSymSetTypes.eIndex.Bound_7bit;
            else if ((codeMin < 0x20) || (usesC1Range))
                symSetType = PCLSymSetTypes.eIndex.Bound_PC8;
            else
                symSetType = PCLSymSetTypes.eIndex.Bound_8bit;
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
            ToolSymbolSetGenPersist.loadDataDonor (
                ref _indxDonorSymSetSubset,
                ref _flagDonorSymSetUserSet,
                ref _flagDonorSymSetMapPCL,
                ref _donorSymSetFile);

            ToolCommonFunctions.getFolderName (_donorSymSetFile,
                                               ref _donorSymSetFolder);

            ToolSymbolSetGenPersist.loadDataTarget (
                ref _flagMapHex,
                ref _flagIgnoreC0,
                ref _flagIgnoreC1,
                ref _flagIndexUnicode,
                ref _flagCharCollReqSpecific,
                ref _targetCharCollReqUnicode,
                ref _targetCharCollReqMSL,
                ref _targetSymSetFolder);

            if ((_indxDonorSymSetSubset < 0) ||
                (_indxDonorSymSetSubset >= _ctMappedSymSets))
                _indxDonorSymSetSubset = 0;
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
            ToolSymbolSetGenPersist.saveDataDonor (
                _indxDonorSymSetSubset,
                _flagDonorSymSetUserSet,
                _flagDonorSymSetMapPCL,
                _donorSymSetFile);

            if ((_targetSymSetFile != "") && (_targetSymSetFile != null))
            {
                ToolCommonFunctions.getFolderName (_targetSymSetFile,
                                                   ref _targetSymSetFolder);
            }

            ToolSymbolSetGenPersist.saveDataTarget (
                _flagMapHex,
                _flagIgnoreC0,
                _flagIgnoreC1,
                _flagIndexUnicode,
                _flagCharCollReqSpecific,
                _targetCharCollReqUnicode,
                _targetCharCollReqMSL,
                _targetSymSetFolder);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p o p u l a t e C h a r C o l l R e q                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Populate the individual check boxes in the Character Collection    //
        // Requirements combination box.                                      //
        // The values depend on whether we are using Unicode indexing (the    //
        // default) or MSL indexing (only of use with Intellifont fonts), so  //
        // we associate the relevant collection with the ItemsSource of the   //
        // combobox.                                                          //
        //                                                                    //  
        // We assume that there are 64 items, and that the collection has     //
        // defined them in order, starting with the one for bit 0 (the least  //
        // significant bit of the 64-bit array).                              //
        //                                                                    //
        // Finally, set the appropriate value in the associated text block.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void populateCharCollReq (Boolean flagIndexUnicode)
        {
            UInt64 bitVal;

            Int32 bitNo;

            PCLCharCollections.eBitType bitType;

            _flagCharCollReqInhibit = true;

            if (_flagIndexUnicode)
            {
                cbCharColls.ItemsSource = _charCollReqListUnicode;

                foreach (PCLCharCollItem item in _charCollReqListUnicode)
                {
                    bitNo = item.BitNo;
                    bitVal = ((UInt64)0x01) << bitNo;
                    bitType = item.BitType;

                    if (bitType == PCLCharCollections.eBitType.Collection)
                    {
                        if ((_targetCharCollReqUnicode & bitVal) != 0)
                            item.IsChecked = true;
                        else
                            item.IsChecked = false;
                    }
                    else
                    {
                        if ((_targetCharCollReqAllUnicode & bitVal) != 0)
                            item.IsChecked = true;
                        else
                            item.IsChecked = false;
                    }
                }

                setTargetCharCollReqValue (_targetCharCollReqUnicode);
            }
            else
            {
                cbCharColls.ItemsSource = _charCollReqListMSL;

                foreach (PCLCharCollItem item in _charCollReqListMSL)
                {
                    bitNo = item.BitNo;
                    bitVal = ((UInt64)1) << bitNo;
                    bitType = item.BitType;

                    if (bitType == PCLCharCollections.eBitType.Collection)
                    {
                        if ((_targetCharCollReqMSL & bitVal) != 0)
                            item.IsChecked = true;
                        else
                            item.IsChecked = false;
                    }
                    else
                    {
                        if ((_targetCharCollReqAllMSL & bitVal) != 0)
                            item.IsChecked = true;
                        else
                            item.IsChecked = false;
                    }
                }

                setTargetCharCollReqValue (_targetCharCollReqMSL);
            }

            _flagCharCollReqInhibit = false;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b C h a r R e q A l l _ C l i c k                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the Character Requirements Collection 'All' radio      //
        // button is clicked.                                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbCharReqAll_Click (object sender,
                                         RoutedEventArgs e)
        {
            _flagCharCollReqSpecific = false;

            cbCharColls.Visibility = Visibility.Hidden;
            tblkCharCollsText.Visibility = Visibility.Hidden;

            if (_flagIndexUnicode)
                _targetCharCollReq = _targetCharCollReqAllUnicode;
            else
                _targetCharCollReq = _targetCharCollReqAllMSL;

            setTargetCharCollReqValue (_targetCharCollReq);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b C h a r R e q S p e c i f i c _ C l i c k                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the Character Requirements Collection 'Specific' radio //
        // button is clicked.                                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbCharReqSpecific_Click (object sender,
                                              RoutedEventArgs e)
        {
            _flagCharCollReqSpecific = true;

            populateCharCollReq (_flagIndexUnicode);

            cbCharColls.Visibility = Visibility.Visible;
            tblkCharCollsText.Visibility = Visibility.Visible;

            if (_flagIndexUnicode)
                _targetCharCollReq = _targetCharCollReqUnicode;
            else
                _targetCharCollReq = _targetCharCollReqMSL;

            setTargetCharCollReqValue (_targetCharCollReq);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b D o n o r S y m S e t M a p P C L _ C l i c k                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Radio button selecting Symbol Set mapping 'PCL'.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbDonorSymSetMapPCL_Click (object sender,
                                                RoutedEventArgs e)
        {
            _flagDonorSymSetMapPCL = true;

            donorSymSetChange ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b D o n o r S y m S e t M a p S t d _ C l i c k                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Radio button selecting Symbol Set mapping 'strict'.                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbDonorSymSetMapStd_Click (object sender,
                                                RoutedEventArgs e)
        {
            _flagDonorSymSetMapPCL = false;

            donorSymSetChange ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b D o n o r S y m S e t P r e s e t _ C l i c k                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the donor Symbol Set 'Preset' radio button is clicked. //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbDonorSymSetPreset_Click (object sender,
                                                RoutedEventArgs e)
        {
            _flagDonorSymSetUserSet = false;

            grpDonorSymSet.Visibility = Visibility.Visible;
            cbDonorSymSet.Visibility = Visibility.Visible;

            grpDonorSymSetMapType.Visibility = Visibility.Visible;

            grpDonorSymSetFile.Visibility = Visibility.Hidden;

            btnDefineSymSet.IsEnabled = true;
            btnGenerateSymSet.IsEnabled = false;
            btnLogSave.IsEnabled = false;

            donorSymSetChange ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b D o n o r S y m S e t U s e r S e t _ C l i c k                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the donor Symbol  Set 'User set' radio button is       //
        // clicked.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbDonorSymSetUserSet_Click (object sender,
                                                 RoutedEventArgs e)
        {
            _flagDonorSymSetUserSet = true;

            grpDonorSymSet.Visibility = Visibility.Visible;
            cbDonorSymSet.Visibility = Visibility.Hidden;

            grpDonorSymSetMapType.Visibility = Visibility.Hidden;

            grpDonorSymSetFile.Visibility = Visibility.Visible;

            btnDefineSymSet.IsEnabled = true;
            btnGenerateSymSet.IsEnabled = false;
            btnLogSave.IsEnabled = false;

            donorSymSetChange ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b I n d e x M S L _ C l i c k                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the Symbol Index 'MSL' radio button is clicked.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbIndexMSL_Click (object sender,
                                       RoutedEventArgs e)
        {
            _flagIndexUnicode = false;

            if (_flagCharCollReqSpecific)
            {
                populateCharCollReq (_flagIndexUnicode);

                MessageBox.Show ("Specific 'Character collection" +
                                 " requirements' settings need to be" +
                                 " reviewed as the 'Symbol index' type" +
                                 " has been changed.",
                                 "Symbol index type changed",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Warning);
            }
            else
            {
                _targetCharCollReq = _targetCharCollReqAllMSL;

                setTargetCharCollReqValue (_targetCharCollReq);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b I n d e x U n i c o d e _ C l i c k                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the Symbol Index 'Unicode' radio button is clicked.    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbIndexUnicode_Click (object sender,
                                           RoutedEventArgs e)
        {
            _flagIndexUnicode = true;

            if (_flagCharCollReqSpecific)
            {
                populateCharCollReq (_flagIndexUnicode);

                MessageBox.Show ("Specific 'Character collection" +
                                 " requirements' settings need to be " +
                                 " reviewed as the 'Symbol index' type" +
                                 " has been changed.",
                                 "Symbol index type changed",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Warning);
            }
            else
            {
                _targetCharCollReq = _targetCharCollReqAllUnicode;

                setTargetCharCollReqValue (_targetCharCollReq);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b M a p D e c _ C l i c k                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the Map format 'Decimal' radio button is clicked.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbMapDec_Click (object sender,
                                     RoutedEventArgs e)
        {
            _flagMapHex = false;

            mapDisplay (_flagMapHex, _flagIgnoreC0, _flagIgnoreC1,
                        _offsetMin);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b M a p H e x _ C l i c k                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the Map format 'Hexadecimal' radio button is clicked.  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbMapHex_Click (object sender,
                                     RoutedEventArgs e)
        {
            _flagMapHex = true;

            mapDisplay (_flagMapHex, _flagIgnoreC0, _flagIgnoreC1,
                        _offsetMin);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b T y p e M o n o B y t e _ C l i c k                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the Type '7/8-bit' radio button is clicked.            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbTypeMonoByte_Click (object sender,
                                           RoutedEventArgs e)
        {
            _flagMultiByteSet = false;

            _sizeCharSet = cSizeCharSet_8bit;

            grpCodeOffset.Visibility = Visibility.Hidden;
            txtMapOffset.Visibility = Visibility.Hidden;
            btnCodePtClearAll.Visibility = Visibility.Hidden;

            cbOffsetRange.SelectedIndex = 0;

            setOffsetData ();

            mapDisplay (_flagMapHex, _flagIgnoreC0, _flagIgnoreC1,
                        _offsetMin);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b T y p e M u l t i B y t e _ C l i c k                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the Type '16-bit' radio button is clicked.             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbTypeMultiByte_Click (object sender,
                                            RoutedEventArgs e)
        {
            _flagMultiByteSet = true;

            _sizeCharSet = cSizeCharSet_16bit;

            if (!_flagMultiByteMap)
            {
                _flagMultiByteMap = true;
                _symSetMap16bit = new UInt16 [cSizeCharSet_16bit];
                _symSetMapTarget = _symSetMap16bit;

                for (Int32 i = 0; i < cSizeCharSet_8bit; i++)
                {
                    _symSetMapTarget [i] = _symSetMap8bit [i];
                }

                for (Int32 i = cSizeCharSet_8bit; i < cSizeCharSet_16bit; i++)
                {
                    _symSetMapTarget [i] = cCodePointUnused;
                }
            }

            grpCodeOffset.Visibility = Visibility.Visible;
            txtMapOffset.Visibility = Visibility.Visible;
            btnCodePtClearAll.Visibility = Visibility.Visible;

            cbOffsetRange.SelectedIndex = 0;

            setOffsetData ();

            mapDisplay (_flagMapHex, _flagIgnoreC0, _flagIgnoreC1,
                        _offsetMin);
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
            // dummy method
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e l e c t D o n o r S y m S e t F i l e                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initiate 'open file' dialogue for donor user-defined symbol set    //
        // file.                                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean selectDonorSymSetFile (ref String symSetFilename)
        {
            OpenFileDialog openDialog = ToolCommonFunctions.createOpenFileDialog(symSetFilename);

            openDialog.Filter = "PCL files|*.pcl; *.PCL;" +
                                "|All files|*.*";

            Nullable<Boolean> dialogResult = openDialog.ShowDialog();

            if (dialogResult == true)
                symSetFilename = openDialog.FileName;

            return dialogResult == true;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e l e c t T a r g e t S y m S e t F i l e                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initiate 'open file' dialogue for target user-defined symbol set   //
        // file.                                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean selectTargetSymSetFile (ref String symSetFilename)
        {
            OpenFileDialog openDialog = ToolCommonFunctions.createOpenFileDialog(symSetFilename);

            openDialog.CheckFileExists = false;
            openDialog.Filter = "PCL files|*.pcl; *.PCL;" +
                                "|All files|*.*";

            Nullable<Boolean> dialogResult = openDialog.ShowDialog();

            if (dialogResult == true)
                symSetFilename = openDialog.FileName;

            return dialogResult == true;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t D o n o r S y m S e t A t t r i b u t e s                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set the attributes of the selected donor symbol set.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void setDonorSymSetAttributes ()
        {
            String idNum = "",
                   idAlpha = "";

            grpTargetSymSetDetails.Visibility = Visibility.Hidden;
            
            if (_flagDonorSymSetUserSet)
            {
                //--------------------------------------------------------//
                //                                                        //
                // User set - bound to a user-defined symbol set.         //
                //                                                        //
                // The user-defined symbol set is defined via a PCL      //
                // symbol set definition held within a user-specified     //
                // file.                                                  //
                //                                                        //
                //--------------------------------------------------------//

                grpDonorSymSetMapType.Visibility = Visibility.Hidden;
                grpDonorSymSetFile.Visibility = Visibility.Visible;
                grpDonorSymSet.Visibility = Visibility.Visible;
                cbDonorSymSet.Visibility = Visibility.Hidden;

                _donorSymSetNo = _donorSymSetNoUserSet;

                PCLSymbolSets.translateKind1ToId (_donorSymSetNoUserSet,
                                                  ref idNum,
                                                  ref idAlpha);

                txtDonorSymSetNo.Text = _donorSymSetNoUserSet.ToString ();

                txtDonorSymSetIdNum.Text = idNum;
                txtDonorSymSetIdAlpha.Text = idAlpha;
            }
            else if (_indxDonorSymSetSubset != -1)
            {
                Int32 indxDonorSymSet;

                _indxDonorSymSetSubset = cbDonorSymSet.SelectedIndex;

                indxDonorSymSet = _subsetSymSets [_indxDonorSymSetSubset];

                _donorSymSetGroup = PCLSymbolSets.getGroup (indxDonorSymSet);

                _flagSymSetNullMapPCL = PCLSymbolSets.nullMapPCL(indxDonorSymSet);
                _flagSymSetNullMapStd = PCLSymbolSets.nullMapStd(indxDonorSymSet);

                if ((_donorSymSetGroup == PCLSymbolSets.eSymSetGroup.Preset) ||
                    (_donorSymSetGroup == PCLSymbolSets.eSymSetGroup.NonStd))
                {
                    //--------------------------------------------------------//
                    //                                                        //
                    // Pre-defined symbol set.                                //
                    // Obtain the number and equivalent ID values, and set    //
                    // these values in the number and ID boxes (which should  //
                    // be disabled to prevent user input).                    //
                    //                                                        //
                    //--------------------------------------------------------//

                    grpDonorSymSetMapType.Visibility = Visibility.Visible;
                    grpDonorSymSetFile.Visibility = Visibility.Hidden;
                    grpDonorSymSet.Visibility = Visibility.Visible;
                    cbDonorSymSet.Visibility = Visibility.Visible;

                    txtDonorSymSetNo.IsEnabled = false;
                    txtDonorSymSetIdNum.IsEnabled = false;
                    txtDonorSymSetIdAlpha.IsEnabled = false;

                    _donorSymSetNo = PCLSymbolSets.getKind1 (indxDonorSymSet);

                    txtDonorSymSetNo.Text = _donorSymSetNo.ToString ();

                    PCLSymbolSets.translateKind1ToId (_donorSymSetNo,
                                                      ref idNum,
                                                      ref idAlpha);

                    txtDonorSymSetIdNum.Text = idNum;
                    txtDonorSymSetIdAlpha.Text = idAlpha;

                    //--------------------------------------------------------//

                    if (_flagSymSetNullMapPCL)
                    {
                        rbDonorSymSetMapPCL.IsEnabled = false;
                        rbDonorSymSetMapStd.IsChecked = true;

                        _flagDonorSymSetMapPCL = false;
                    }
                    else if (_flagSymSetNullMapStd)
                    {
                        rbDonorSymSetMapStd.IsEnabled = false;
                        rbDonorSymSetMapPCL.IsChecked = true;

                        _flagDonorSymSetMapPCL = true;
                    }
                    else
                    {
                        rbDonorSymSetMapPCL.IsEnabled = true;
                        rbDonorSymSetMapStd.IsEnabled = true;
                    }
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t M u l t i B y t e D a t a                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Offset Range item has changed; update dependent items.             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void setMultiByteData (Boolean multiByte)
        {
            if (multiByte)
            {
                rbTypeMultiByte.IsChecked = true;

                _sizeCharSet = cSizeCharSet_16bit;

                _flagMultiByteSet = true;

                if (!_flagMultiByteMap)
                {
                    _flagMultiByteMap = true;

                    _symSetMap16bit = new UInt16 [cSizeCharSet_16bit];

                    _symSetMapTarget = _symSetMap16bit;

                    grpCodeOffset.Visibility = Visibility.Visible;
                    txtMapOffset.Visibility = Visibility.Visible;
                }
            }
            else
            {
                rbTypeMonoByte.IsChecked = true;

                _sizeCharSet = cSizeCharSet_8bit;

                _flagMultiByteSet = false;

                grpCodeOffset.Visibility = Visibility.Hidden;
                txtMapOffset.Visibility = Visibility.Hidden;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t O f f s e t D a t a                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Offset Range item has changed; update dependent items.             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void setOffsetData ()
        {
            _offsetMin = (UInt16) (_indxOffsets * 256);
            _offsetMax = (UInt16) (_offsetMin + 0xff);

            if (_flagMapHex)
            {
                txtOffsetRangeMin.Text = _offsetMin.ToString ("x4");
                txtOffsetRangeMax.Text = _offsetMax.ToString ("x4");

                if (_offsetMin != 0)
                    txtMapOffset.Text = _offsetMin.ToString ("x4") + "+";
            }
            else
            {
                txtOffsetRangeMin.Text = _offsetMin.ToString ();
                txtOffsetRangeMax.Text = _offsetMax.ToString ();

                if (_offsetMin != 0)
                    txtMapOffset.Text = _offsetMin.ToString () + "+";
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t O f f s e t R a n g e s                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Populate the range offset drop-down box for multi-byte sets.       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void setOffsetRanges ()
        {
            _initialisedOffsets = false;

            cbOffsetRange.Items.Clear ();

            for (Int32 i = 0; i < 256; i++)
            {
                cbOffsetRange.Items.Add (i.ToString ("x2") + "00");
            }

            cbOffsetRange.SelectedIndex = 0;

            _initialisedOffsets = true;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t T a r g e t C h a r C o l l R e q A r r a y                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Generate and display the text associated with the current set of   //
        // Check boxes selected within the Character Collections combo box.   //
        // Also store the calculated array value.                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void setTargetCharCollReqArray (Boolean flagIndexUnicode)
        {
            UInt64 targetCharCollReq = 0,
                   bitVal;

            Int32 bitNo;

            if (cbCharColls.ItemsSource != null)
            {
                foreach (PCLCharCollItem item in cbCharColls.ItemsSource)
                {
                    bitNo = item.BitNo;

                    if (item.IsChecked)
                    {
                        bitVal = ((UInt64)1) << bitNo;
                        targetCharCollReq = targetCharCollReq | bitVal;
                    }
                }
            }

            setTargetCharCollReqValue (targetCharCollReq);

            //----------------------------------------------------------------//

            if (flagIndexUnicode)
                _targetCharCollReqUnicode = targetCharCollReq;
            else
                _targetCharCollReqMSL     = targetCharCollReq;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t T a r g e t C h a r C o l l R e q V a l u e                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Display the current Character Collection array value.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void setTargetCharCollReqValue (UInt64 arrayVal)
        {
            tblkCharColls.Text = "0x" +  arrayVal.ToString ("x16");
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t T a r g e t S y m S e t A t t r i b u t e s                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set the attributes of the selected target symbol set.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void setTargetSymSetAttributes ()
        {
            String symSetId = txtTargetSymSetIdNum.Text +
                              txtTargetSymSetIdAlpha.Text;

            _targetSymSetNo = PCLSymbolSets.translateIdToKind1 (symSetId);

            txtTargetSymSetNo.Text = _targetSymSetNo.ToString ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t T a r g e t S y m S e t F i l e n a m e                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set the default name of the selected target symbol set file.       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void setTargetSymSetFilename ()
        {
            String mapType;

            if (_flagDonorSymSetMapPCL)
                mapType = "_LJ";
            else
                mapType = "_Std";

            _targetSymSetFile =
                _targetSymSetFolder + "\\" +
                "DefineSymbolSet_" +
                PCLSymbolSets.translateKind1ToId (_targetSymSetNo) +
                mapType + 
                ".pcl";

            txtTargetSymSetFile.Text = _targetSymSetFile;
        }


        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t D o n o r S y m S e t F i l e _ L o s t F o c u s            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Donor user-defined symbol set filename item has lost focus.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtDonorSymSetFile_LostFocus (object sender,
                                                   RoutedEventArgs e)
        {
            _donorSymSetFile = txtDonorSymSetFile.Text;

            donorSymSetChange ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t M a p _ G o t F o c u s                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // A text mapping text box has got focus; select the contents.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtMap_GotFocus (object sender, RoutedEventArgs e)
        {
            TextBox source = e.Source as TextBox;

            source.SelectAll ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t M a p _ L o s t F o c u s                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // A text mapping text box has lost focus; validate the contents.     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtMap_LostFocus (object sender, RoutedEventArgs e)
        {
            Boolean flagOK = false;

            UInt16 mapIndx;

            TextBox source = e.Source as TextBox;

            String txtBoxName;

            //----------------------------------------------------------------//
            //                                                                //
            // Work out which (mapping) text box has just lost focus.         //
            // This is done by examining the text box name.                   //
            // The names should be in the format 'txtMap0xpq', where 'pq' is  //
            // a hexadecimal value (range 00 -> ff).                          //
            //                                                                //
            //----------------------------------------------------------------//

            txtBoxName = source.Name; // should be in format txtMap0xpq

            flagOK = UInt16.TryParse (txtBoxName.Substring (8, 2),
                                      NumberStyles.HexNumber,
                                      CultureInfo.InvariantCulture,
                                      out mapIndx);

            if (flagOK)
            {
                if (mapIndx > _sizeCharSet)
                    flagOK = false;
            }

            if (!flagOK)
            {
                MessageBox.Show ("Unable to detemine which mapping text box" +
                                 " has just lost focus!",
                                "***** Internal error *****",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
            else
            {
                flagOK = validateMapEntry (source, (UInt16) (_offsetMin +  mapIndx));

                if (flagOK)
                {
                    UInt16 codeMin = 0,
                           codeMax = 0,
                           codeCt = 0;

                    String format;

                    if (_flagMapHex)
                        format = "x4";
                    else
                        format = "";

                    mapMetrics (_flagIgnoreC0, _flagIgnoreC1, _sizeCharSet,
                                ref codeMin, ref codeMax, ref codeCt,
                                ref _targetSymSetType);

                    txtCodeMin.Text = codeMin.ToString (format);
                    txtCodeMax.Text = codeMax.ToString (format);
                    txtCodeCt.Text = codeCt.ToString (format);
                }
                else
                {
                    Helper_WPFFocusFix.Focus (source);   // need this to focus
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t T a r g e t S y m S e t F i l e _ L o s t F o c u s          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Target user-defined symbol set filename item has lost focus.       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtTargetSymSetFile_LostFocus (object sender,
                                                    RoutedEventArgs e)
        {
            _targetSymSetFile = txtTargetSymSetFile.Text;

            ToolCommonFunctions.getFolderName (_targetSymSetFile,
                                               ref _targetSymSetFolder);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t T a r g e t S y m S e t I d A l p h a _ G o t F o c u s      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Text box for target symbol set identifier (alphabetic part) has    //
        // focus.                                                             //
        // Select all text in the box, so that it can be over-written easily, //
        // without inadvertently causing validation failures.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtTargetSymSetIdAlpha_GotFocus (object sender,
                                                  RoutedEventArgs e)
        {
            txtTargetSymSetIdAlpha.SelectAll ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t T a r g e t S y m S e t I d A l p h a _ L o s t F o c u s    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Alphabetic part of target symbol set identifier has lost focus.    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtTargetSymSetIdAlpha_LostFocus (object sender,
                                                       RoutedEventArgs e)
        {
            if (validateTargetSymSetIdAlpha (true))
            {
                setTargetSymSetAttributes ();

                setTargetSymSetFilename ();
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t O p S y m S e t I d A l p h a _ T e x t C h a n g e d        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Alphabetic part of target symbol set identifier changed.           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtTargetSymSetIdAlpha_TextChanged (object sender,
                                                         TextChangedEventArgs e)
        {
            if (validateTargetSymSetIdAlpha (false))
            {
                setTargetSymSetAttributes ();

                setTargetSymSetFilename ();
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t T a r g e t S y m S e t I d N u m _ G o t F o c u s          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Text box for target symbol set identifier (numeric part) has       //
        // focus.                                                             //
        // Select all text in the box, so that it can be over-written easily, //
        // without inadvertently causing validation failures.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtTargetSymSetIdNum_GotFocus (object sender,
                                                    RoutedEventArgs e)
        {
            txtTargetSymSetIdNum.SelectAll ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t T a r g e t S y m S e t I d N u m _ L o s t F o c u s        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Alphabetic part of target symbol set identifier has lost focus.    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtTargetSymSetIdNum_LostFocus (object sender,
                                                     RoutedEventArgs e)
        {
            if (validateTargetSymSetIdNum (true))
            {
                setTargetSymSetAttributes ();

                setTargetSymSetFilename ();
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t T a r g e t S y m S e t I d N u m _ T e x t C h a n g e d    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Numeric part of target symbol set identifier changed.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtTargetSymSetIdNum_TextChanged (object sender,
                                                       TextChangedEventArgs e)
        {
            if (validateTargetSymSetIdNum (false))
            {
                setTargetSymSetAttributes ();

                setTargetSymSetFilename ();
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // v a l i d a t e M a p E n t r y                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Validate supplied codepoint value and return value.                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean validateMapEntry (TextBox source,
                                          UInt16  mapIndx)
        {
            Boolean OK = true;

            UInt16 value;
            
            String txtCodepoint = source.Text;

            if (_flagMapHex)
                OK = UInt16.TryParse (txtCodepoint,
                                      NumberStyles.HexNumber,
                                      CultureInfo.InvariantCulture,
                                      out value);
            else
                OK = UInt16.TryParse (txtCodepoint, out value);

            if (OK)
            {
                _symSetMapTarget [mapIndx] = value;
            }
            else
            {
                String format,
                       formatDesc,
                       valText,
                       indxText;

                if (_flagMapHex)
                {
                    format     = "x4";
                    formatDesc = "hexadecimal";
                }
                else
                {
                    format     = "";
                    formatDesc = "decimal";
                }

                valText     = _symSetMapTarget[mapIndx].ToString (format);
                indxText    = mapIndx.ToString (format);

                source.Text = valText;

                MessageBox.Show ("Map point " + formatDesc +
                                 " '" + indxText + "':\n\n" +
                                 "Target code-point value " + formatDesc +
                                 " '" + txtCodepoint + "' is invalid.\n\n" +
                                 "Reset to original value " + formatDesc +
                                 " '" + valText + "'",
                                 "Target code point mapping",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
            }

            return OK;
        }
 
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // v a l i d a t e T a r g e t S y m S e t I d A l p h a              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Validate alphabetic part of target symbol set identifier.          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean validateTargetSymSetIdAlpha (Boolean lostFocusEvent)
        {
            const Char minVal = 'A';
            const Char maxVal = 'Z';
            const Char badVal = 'X';
            const Char defVal = _defaultSymSetIdAlpha;

            Int32 value = 0,
                  len;

            Boolean OK = true;

            String crntText = txtTargetSymSetIdAlpha.Text;

            len = crntText.Length;

            if (len != 1)
            {
                OK = false;
            }
            else
            {
                value = Char.ConvertToUtf32 (crntText, 0);

                if ((value < minVal) || (value > maxVal) || (value == badVal))
                    OK = false;
            }

            if (!OK)
            {
                if (lostFocusEvent)
                {
                    MessageBox.Show ("Alphabetic part '" + crntText +
                                    "' of identifier is invalid.\n\n" +
                                    "Value will be reset to default '" +
                                    defVal + "'",
                                    "Symbol Set identifier invalid",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);

                    txtTargetSymSetIdAlpha.Text = defVal.ToString ();

                    OK = true;
                }
                else
                {
                    MessageBox.Show ("Alphabetic part '" + crntText +
                                    "' of identifier is invalid.\n\n" +
                                    "Valid range is :\n\t" +
                                    minVal + " <= value <= " + maxVal +
                                    " excluding " + badVal,
                                    "Symbol Set identifier invalid",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);

                    txtTargetSymSetIdAlpha.Focus ();
                    txtTargetSymSetIdAlpha.SelectAll ();
                }
            }

            return OK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // v a l i d a t e T a r g e t S y m S e t I d N u m                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Validate numeric part of target symbol set identifier.             //
        //                                                                    //
        // Within a symbol set definition, the numeric part of the identifier //
        // must be within the (inclusive) range 0 -> 1023.                    //
        //                                                                    //
        // Within a bound soft font definition, the Kind1 value of the symbol //
        // set to which the font is bound may equate to having a numeric part //
        // with a value up to 2047; but values of 1024 -> 2047 are not        //
        // allowed within actual symbol set definitions.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean validateTargetSymSetIdNum (Boolean lostFocusEvent)
        {
            const UInt16 minVal = 0;
            const UInt16 maxVal = 1023;
            const UInt16 defVal = _defaultSymSetIdNum;

            UInt16 value;

            Boolean OK = true;

            String crntText = txtTargetSymSetIdNum.Text;

            OK = UInt16.TryParse (crntText, out value);

            if (OK)
                if ((value < minVal) || (value > maxVal))
                    OK = false;

            if (!OK)
            {
                if (lostFocusEvent)
                {
                    String newText = defVal.ToString ();

                    MessageBox.Show ("Numeric part '" + crntText +
                                    "' of identifier is invalid.\n\n" +
                                    "Value will be reset to default '" +
                                    newText + "'",
                                    "Symbol Set identifier invalid",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);

                    txtTargetSymSetIdNum.Text = newText;

                    OK = true;
                }
                else
                {
                    MessageBox.Show ("Numeric part '" + crntText +
                                    "' of identifier is invalid.\n\n" +
                                    "Valid range is :\n\t" +
                                    minVal + " <= value <= " + maxVal,
                                    "Symbol Set identifier invalid",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);

                    txtTargetSymSetIdNum.Focus ();
                    txtTargetSymSetIdNum.SelectAll ();
                }
            }

            return OK;
        }
    }
}
