using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace PCLParaphernalia
{
    /// <summary>
    /// Interaction logic for ToolPrintLang.xaml
    /// 
    /// Class handles the Print Languages tool form.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>
    
    [System.Reflection.ObfuscationAttribute(Feature = "renaming",
                                            ApplyToMembers = true)]

    public partial class ToolPrintLang : Window
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private enum eInfoType : byte
        {
            // must be in same order as _subsetTypes array

            PCL,
            HPGL2,
            PCLXLTags,
            PCLXLEnums,
            PJLCmds,
            PMLTags,
            SymbolSets,
            Fonts,
            PaperSizes,
            PrescribeCmds
        }

        public enum eSymSetMapType : byte
        {
            Std,
            PCL,
            Both,
            Max
        }

        FontFamily _fontFixed = new FontFamily ("Courier New");
        FontFamily _fontProp = new FontFamily ("Arial");

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Fields (class variables).                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static Int32[] _subsetTypes =
        {
            // must be in same order as eInfoType enumeration

            (Int32) ToolCommonData.eToolSubIds.PCL,
            (Int32) ToolCommonData.eToolSubIds.HPGL2,
            (Int32) ToolCommonData.eToolSubIds.PCLXLTags,
            (Int32) ToolCommonData.eToolSubIds.PCLXLEnums,
            (Int32) ToolCommonData.eToolSubIds.PJLCmds,
            (Int32) ToolCommonData.eToolSubIds.PMLTags,
            (Int32) ToolCommonData.eToolSubIds.SymbolSets,
            (Int32) ToolCommonData.eToolSubIds.Fonts,
            (Int32) ToolCommonData.eToolSubIds.PaperSizes,
            (Int32) ToolCommonData.eToolSubIds.PrescribeCmds
        };

        private Int32 _ctItems = 0;
        private Int32 _ctTypes = 0;
        private Int32 _indxType = 0;

        private Boolean _flagPCLSeqControl = false,
                        _flagPCLSeqSimple = false,
                        _flagPCLSeqComplex = false,
                        _flagPCLOptObsolete = false,
                        _flagPCLOptDiscrete = false;

        private Boolean _flagPCLXLTagDataType = false,
                        _flagPCLXLTagAttribute = false,
                        _flagPCLXLTagOperator = false,
                        _flagPCLXLTagAttrDef = false,
                        _flagPCLXLTagEmbedDataLen = false,
                        _flagPCLXLTagWhitespace = false,
                        _flagPCLXLOptReserved = false;

        private Boolean _flagPMLTagDataType = false,
                        _flagPMLTagAction = false,
                        _flagPMLTagOutcome = false;

        private Boolean _flagSymSetList = false;

        private Boolean _flagSymSetMap = false;

        private eSymSetMapType _symSetMapType = eSymSetMapType.Std;

        private Boolean _initialised = false;

        private static String _saveFilename;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // T o o l P r i n t L a n g                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public ToolPrintLang (ref ToolCommonData.ePrintLang crntPDL)
        {
            InitializeComponent();

            initialise();

            crntPDL = ToolCommonData.ePrintLang.Unknown;
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
            eInfoType infoType = (eInfoType)_indxType;

            if (infoType == eInfoType.PCL)
                displayPCLSeqs ();
            else if (infoType == eInfoType.HPGL2)
                displayHPGL2Commands ();
            else if (infoType == eInfoType.PCLXLTags)
                displayPCLXLTags ();
            else if (infoType == eInfoType.PCLXLEnums)
                displayPCLXLEnums ();
            else if (infoType == eInfoType.PJLCmds)
                displayPJLCmds ();
            else if (infoType == eInfoType.PMLTags)
                displayPMLTags ();
            else if (infoType == eInfoType.SymbolSets)
                displaySymbolSetData ();
            else if (infoType == eInfoType.Fonts)
                displayFontData ();
            else if (infoType == eInfoType.PaperSizes)
                displayPaperSizeData ();
            else if (infoType == eInfoType.PrescribeCmds)
                displayPrescribeCmds();

            btnSaveReport.Visibility = Visibility.Visible;
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

            TargetCore.metricsReturnFileRpt (ToolCommonData.eToolIds.PrintLang,
                                             ref rptFileFmt,
                                             ref rptChkMarks,
                                             ref flagOptRptWrap);

            ToolPrintLangReport.generate (_subsetTypes [_indxType],
                                          rptFileFmt,
                                          rptChkMarks,
                                          dgSeq,
                                          ref _saveFilename,
                                          _flagPCLSeqControl,
                                          _flagPCLSeqSimple,
                                          _flagPCLSeqComplex,
                                          _flagPCLOptObsolete,
                                          _flagPCLOptDiscrete,
                                          _flagPCLXLTagDataType,
                                          _flagPCLXLTagAttribute,
                                          _flagPCLXLTagOperator,
                                          _flagPCLXLTagAttrDef,
                                          _flagPCLXLTagEmbedDataLen,
                                          _flagPCLXLTagWhitespace,
                                          _flagPCLXLOptReserved,
                                          _flagPMLTagDataType,
                                          _flagPMLTagAction,
                                          _flagPMLTagOutcome,
                                          _flagSymSetList,
                                          _flagSymSetMap,
                                          flagOptRptWrap,
                                          _symSetMapType);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k F o n t O p t M a p _ C h e c k e d                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the Fonts option 'Show Symbol Set lists' checkbox is   //
        // checked.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkFontOptMap_Checked(object sender,
                                           RoutedEventArgs e)
        {
            _flagSymSetList = true;

            lbFontMapComment1.Visibility = Visibility.Visible;
            lbFontMapComment2.Visibility = Visibility.Visible;
            lbFontMapComment3.Visibility = Visibility.Visible;

            clearDetails();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k F o n t O p t M a p _ U n c h e c k e d                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the Fonts option 'Show Symbol Set lists' checkbox is   //
        // unchecked.                                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkFontOptMap_Unchecked(object sender,
                                             RoutedEventArgs e)
        {
            _flagSymSetList = false;

            lbFontMapComment1.Visibility = Visibility.Hidden;
            lbFontMapComment2.Visibility = Visibility.Hidden;
            lbFontMapComment3.Visibility = Visibility.Hidden;

            clearDetails();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L O p t D i s c r e t e _ C h e c k e d                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCL option 'Discrete' checkbox is checked.         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLOptDiscrete_Checked(object sender,
                                               RoutedEventArgs e)
        {
            _flagPCLOptDiscrete = true;

            clearDetails();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L O p t D i s c r e t e _ U n c h e c k e d              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCL option 'Discrete' checkbox is unchecked.       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLOptDiscrete_Unchecked(object sender,
                                                 RoutedEventArgs e)
        {
            _flagPCLOptDiscrete = false;

            clearDetails();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L O p t O b s o l e t e _ C h e c k e d                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCL option 'Obsolete' checkbox is checked.         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLOptObsolete_Checked(object sender,
                                               RoutedEventArgs e)
        {
            _flagPCLOptObsolete = true;

            clearDetails();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L O p t O b s o l e t e _ U n c h e c k e d              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCL option 'Obsolete' checkbox is unchecked.       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLOptObsolete_Unchecked(object sender,
                                                 RoutedEventArgs e)
        {
            _flagPCLOptObsolete = false;

            clearDetails();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L S e q C o m p l e x _ C h e c k e d                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCL sequence type 'Complex' checkbox is checked.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLSeqComplex_Checked(object sender,
                                              RoutedEventArgs e)
        {
            _flagPCLSeqComplex = true;

            clearDetails();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L S e q C o m p l e x _ U n c h e c k e d                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCL sequence type 'Complex' checkbox is unchecked. //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLSeqComplex_Unchecked(object sender,
                                                RoutedEventArgs e)
        {
            _flagPCLSeqComplex = false;

            clearDetails();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L S e q C o n t r o l _ C h e c k e d                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCL sequence type 'Control' checkbox is checked.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLSeqControl_Checked(object sender,
                                              RoutedEventArgs e)
        {
            _flagPCLSeqControl = true;

            clearDetails();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L S e q C o n t r o l _ U n c h e c k e d                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCL sequence type 'Control' checkbox is unchecked. //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLSeqControl_Unchecked(object sender,
                                                RoutedEventArgs e)
        {
            _flagPCLSeqControl = false;

            clearDetails();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L S e q S i m p l e _ C h e c k e d                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCL sequence type 'Simple' checkbox is checked.    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLSeqSimple_Checked(object sender,
                                             RoutedEventArgs e)
        {
            _flagPCLSeqSimple = true;

            clearDetails();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L S e q S i m p l e _ U n c h e c k e d                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCL sequence type 'Simple' checkbox is unchecked.  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLSeqSimple_Unchecked(object sender,
                                               RoutedEventArgs e)
        {
            _flagPCLSeqSimple = false;

            clearDetails();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L X L O p t R e s e r v e d _ C h e c k e d              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCLXL option 'Reserved' checkbox is checked.       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLXLOptReserved_Checked(object sender,
                                                 RoutedEventArgs e)
        {
            _flagPCLXLOptReserved = true;

            clearDetails();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L X L O p t R e s e r v e d _ U n c h e c k e d          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCLXL option 'Reserved' checkbox is unchecked.     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLXLOptReserved_Unchecked(object sender,
                                                   RoutedEventArgs e)
        {
            _flagPCLXLOptReserved = false;

            clearDetails();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L X L T a g A t t r D e f i n e r_ C h e c k e d         //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCLXL tag type 'Attr. Def.' checkbox is checked.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLXLTagAttrDefiner_Checked(object sender,
                                                    RoutedEventArgs e)
        {
            _flagPCLXLTagAttrDef = true;

            clearDetails();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L X L T a g A t t r D e f i n e r_ U n c h e c k e d     //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCLXL tag type 'Attr. Def.' checkbox is unchecked. //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLXLTagAttrDefiner_Unchecked(object sender,
                                                      RoutedEventArgs e)
        {
            _flagPCLXLTagAttrDef = false;

            clearDetails();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L X L T a g A t t r i b u t e _ C h e c k e d            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCLXL tag type 'Attribute' checkbox is checked.    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLXLTagAttribute_Checked(object sender,
                                                  RoutedEventArgs e)
        {
            _flagPCLXLTagAttribute = true;

            clearDetails();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L X L T a g A t t r i b u t e _ U n c h e c k e d        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCLXL tag type 'Attribute' checkbox is unchecked.  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLXLTagAttribute_Unchecked(object sender,
                                                    RoutedEventArgs e)
        {
            _flagPCLXLTagAttribute = false;

            clearDetails();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L X L T a g D a t a T y p e _ C h e c k e d              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCLXL tag type 'Data Type' checkbox is checked.    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLXLTagDataType_Checked(object sender,
                                                 RoutedEventArgs e)
        {
            _flagPCLXLTagDataType = true;

            clearDetails();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L X L T a g D a t a T y p e _ U n c h e c k e d          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCLXL tag type 'Data Type' checkbox is unchecked.  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLXLTagDataType_Unchecked(object sender,
                                                   RoutedEventArgs e)
        {
            _flagPCLXLTagDataType = false;

            clearDetails();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L X L T a g E m b e d D a t a L e n _ C h e c k e d      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCLXL tag type 'Embed Data' checkbox is checked.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLXLTagEmbedDataLen_Checked(object sender,
                                                     RoutedEventArgs e)
        {
            _flagPCLXLTagEmbedDataLen = true;

            clearDetails();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L X L T a g E m b e d D a t a L e n _ U n c h e c k e d  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCLXL tag type 'Embed Data' checkbox is unchecked. //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLXLTagEmbedDataLen_Unchecked(object sender,
                                                       RoutedEventArgs e)
        {
            _flagPCLXLTagEmbedDataLen = false;

            clearDetails();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L X L T a g O p e r a t o r _ C h e c k e d              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCLXL tag type 'Operator' checkbox is checked.     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLXLTagOperator_Checked(object sender,
                                                 RoutedEventArgs e)
        {
            _flagPCLXLTagOperator = true;

            clearDetails();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L X L T a g O p e r a t o r _ U n c h e c k e d          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCLXL tag type 'Operator' checkbox is unchecked.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLXLTagOperator_Unchecked(object sender,
                                                   RoutedEventArgs e)
        {
            _flagPCLXLTagOperator = false;

            clearDetails();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L X L T a g W h i t e s p a c e _ C h e c k e d          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCLXL tag type 'Whitespace' checkbox is checked.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLXLTagWhitespace_Checked(object sender,
                                                   RoutedEventArgs e)
        {
            _flagPCLXLTagWhitespace = true;

            clearDetails();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L X L T a g W h i t e s p a c e _ U n c h e c k e d      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PCLXL tag type 'Whitespace' checkbox is unchecked. //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLXLTagWhitespace_Unchecked(object sender,
                                                     RoutedEventArgs e)
        {
            _flagPCLXLTagWhitespace = false;

            clearDetails();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P M L T a g A c t i o n _ C h e c k e d                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PML tag type 'Action' checkbox is checked.         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPMLTagAction_Checked(object sender,
                                             RoutedEventArgs e)
        {
            _flagPMLTagAction = true;

            clearDetails ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P ML T a g A c t i o n _ U n c h e c k e d                   //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PML tag type 'Action' checkbox is unchecked.       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPMLTagAction_Unchecked(object sender,
                                               RoutedEventArgs e)
        {
            _flagPMLTagAction = false;

            clearDetails ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P M L T a g D a t a T y p e _ C h e c k e d                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PML tag type 'Data Type' checkbox is checked.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPMLTagDataType_Checked(object sender,
                                               RoutedEventArgs e)
        {
            _flagPMLTagDataType = true;

            clearDetails ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P M L T a g D a t a T y p e _ U n c h e c k e d              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PML tag type 'Data Type' checkbox is unchecked.    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPMLTagDataType_Unchecked(object sender,
                                                 RoutedEventArgs e)
        {
            _flagPMLTagDataType = false;

            clearDetails ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P M L T a g O u t c o m e _ C h e c k e d                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PML tag type 'Outcome' checkbox is checked.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPMLTagOutcome_Checked(object sender,
                                              RoutedEventArgs e)
        {
            _flagPMLTagOutcome = true;

            clearDetails ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P M L T a g O u t c o m e _ U n c h e c k e d                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the PML tag type 'Outcome' checkbox is unchecked.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPMLTagOutcome_Unchecked(object sender,
                                                RoutedEventArgs e)
        {
            _flagPMLTagOutcome = false;

            clearDetails ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k S y m S e t O p t M a p _ C h e c k e d                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the Symbol Sets option 'Show Mappings' checkbox is     //
        // checked.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkSymSetOptMap_Checked (object sender,
                                              RoutedEventArgs e)
        {
            _flagSymSetMap = true;

        //  grpSymSetMapSet.Visibility = Visibility.Visible;
            grpSymSetMapType.Visibility = Visibility.Visible;

            clearDetails ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k S y m S e t O p t M a p _ U n c h e c k e d                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the Symbol Sets option 'Show Mappings' checkbox is     //
        // unchecked.                                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkSymSetOptMap_Unchecked (object sender,
                                                RoutedEventArgs e)
        {
            _flagSymSetMap = false;

       //   grpSymSetMapSet.Visibility = Visibility.Hidden;
            grpSymSetMapType.Visibility = Visibility.Hidden;

            clearDetails ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c l e a r D e t a i l s                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Clear the details area, etc.                                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void clearDetails()
        {
            if (_initialised)
            {
                dgSeq.Items.Clear();

                btnSaveReport.Visibility = Visibility.Hidden;

                txtCount.Text = "0";
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d i s p l a y F o n t D a t a                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Display Font data.                                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void displayFontData()
        {
            setColsFonts ();
            
            dgSeq.Items.Clear ();

            _ctItems = PCLFonts.displayFontList (dgSeq);

            txtCount.Text = _ctItems.ToString ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d i s p l a y H P G L 2 C o m m a n d s                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Display HP-GL/2 commands.                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void displayHPGL2Commands()
        {
            setColsHPGL2 ();
            
            dgSeq.Items.Clear ();

            _ctItems = HPGL2Commands.displaySeqList(dgSeq);

            txtCount.Text = _ctItems.ToString();
         }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d i s p l a y P a p e r S i z e s                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Display paper size data.                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void displayPaperSizeData()
        {
            setColsPaperSizes();

            dgSeq.Items.Clear();

            _ctItems = PCLPaperSizes.displayPaperSizeList(dgSeq);

            txtCount.Text = _ctItems.ToString();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d i s p l a y P C L S e q s                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Display PCL sequences.                                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void displayPCLSeqs()
        {
            if ((! _flagPCLSeqSimple)  &&
                (! _flagPCLSeqComplex) &&
                (! _flagPCLSeqControl))
            {
                MessageBox.Show("At least one sequence type must be selected",
                                "PCL sequence type selection",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
            else
            {
                setColsPCL ();

                dgSeq.Items.Clear ();

                _ctItems = 0;

                if (chkPCLSeqControl.IsChecked == true)
                    _ctItems += PCLControlCodes.displaySeqList(dgSeq);

                if (chkPCLSeqSimple.IsChecked == true)
                    _ctItems +=
                        PCLSimpleSeqs.displaySeqList(dgSeq,
                                                     _flagPCLOptObsolete);

                if (chkPCLSeqComplex.IsChecked == true)
                    _ctItems +=
                        PCLComplexSeqs.displaySeqList(dgSeq,
                                                      _flagPCLOptObsolete,
                                                      _flagPCLOptDiscrete);

                txtCount.Text = _ctItems.ToString();
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d i s p l a y P C L X L E n u m s                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Display PCL XL enumeration details.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void displayPCLXLEnums()
        {
            setColsPCLXLEnums ();

            dgSeq.Items.Clear ();

            _ctItems = PCLXLAttrEnums.displayTags(dgSeq);

            txtCount.Text = _ctItems.ToString();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d i s p l a y P C L X L T a g s                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Display PCL XL tag details.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void displayPCLXLTags()
        {
            if ((! _flagPCLXLTagDataType)      &&
                (! _flagPCLXLTagAttribute)     &&
                (! _flagPCLXLTagOperator)      &&
                (! _flagPCLXLTagWhitespace)    &&
                (! _flagPCLXLTagAttrDef)       &&
                (! _flagPCLXLTagEmbedDataLen))
            {
                MessageBox.Show("At least one tag type must be selected",
                                "PCL XL tag type selection",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
            else
            {
                setColsPCLXLTags ();

                dgSeq.Items.Clear ();

                _ctItems = 0;

                if (_flagPCLXLTagAttrDef)
                    _ctItems +=
                        PCLXLAttrDefiners.displayTags(dgSeq,
                                                      _flagPCLXLOptReserved);

                if (_flagPCLXLTagEmbedDataLen)
                    _ctItems +=
                        PCLXLEmbedDataDefs.displayTags(dgSeq,
                                                       _flagPCLXLOptReserved);

                if (_flagPCLXLTagAttribute)
                    _ctItems +=
                        PCLXLAttributes.displayTags(dgSeq,
                                                    _flagPCLXLOptReserved);

                if (_flagPCLXLTagDataType)
                    _ctItems +=
                        PCLXLDataTypes.displayTags(dgSeq,
                                                   _flagPCLXLOptReserved);

                if (_flagPCLXLTagOperator)
                    _ctItems +=
                        PCLXLOperators.displayTags(dgSeq,
                                                   _flagPCLXLOptReserved);

                if (_flagPCLXLTagWhitespace)
                    _ctItems += PCLXLWhitespaces.displayTags(dgSeq);

                txtCount.Text = _ctItems.ToString();
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d i s p l a y P J L C m d s                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Display PJL command details.                                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void displayPJLCmds()
        {
            setColsPJLCmds ();

            dgSeq.Items.Clear ();

            _ctItems = PJLCommands.displayCmds (dgSeq);

            txtCount.Text = _ctItems.ToString ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d i s p l a y P M L T a g s                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Display PML tag details.                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void displayPMLTags()
        {
            if ((!_flagPMLTagDataType) &&
                (!_flagPMLTagAction) &&
                (!_flagPMLTagOutcome))
            {
                MessageBox.Show ("At least one tag type must be selected",
                                "PML tag type selection",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
            else
            {
                setColsPMLTags ();

                dgSeq.Items.Clear ();

                _ctItems = 0;

                if (_flagPMLTagDataType)
                    _ctItems += PMLDataTypes.displayTags (dgSeq);

                if (_flagPMLTagAction)
                    _ctItems += PMLActions.displayTags (dgSeq);

                if (_flagPMLTagOutcome)
                    _ctItems += PMLOutcomes.displayTags (dgSeq);

                txtCount.Text = _ctItems.ToString ();
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d i s p l a y P r e s c r i b e C m d s                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Display Prescribe command details.                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void displayPrescribeCmds()
        {
            setColsPrescribeCmds();

            dgSeq.Items.Clear();

            _ctItems = PrescribeCommands.displayCmds(dgSeq);

            txtCount.Text = _ctItems.ToString();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d i s p l a y S y m b o l S e t D a t a                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Display Symbol Set data.                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void displaySymbolSetData()
        {
            setColsSymbolSets ();

            dgSeq.Items.Clear ();

            _ctItems = PCLSymbolSets.displaySeqList (dgSeq);

            txtCount.Text = _ctItems.ToString ();
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
            eInfoType infoType;

            _initialised = false;

            //----------------------------------------------------------------//
            //                                                                //
            // Populate form.                                                 //
            //                                                                //
            //----------------------------------------------------------------//

            btnSaveReport.Visibility = Visibility.Hidden;

            txtCount.Text = "0";

            //----------------------------------------------------------------//

            _ctTypes = _subsetTypes.Length;

            //----------------------------------------------------------------//

            resetTarget ();

            //----------------------------------------------------------------//
            //                                                                //
            // Reinstate settings from persistent storage.                    //
            //                                                                //
            //----------------------------------------------------------------//

            metricsLoad();

            infoType = (eInfoType)_indxType;

            if (infoType == eInfoType.HPGL2)
            {
                rbSelTypeHPGL2.IsChecked = true;
                tabInfoType.SelectedItem = tabHPGL2;
                setColsHPGL2();
            }
            else if (infoType == eInfoType.PCL)
            {
                rbSelTypePCL.IsChecked = true;
                tabInfoType.SelectedItem = tabPCL;
                setColsPCL();
            }
            else if (infoType == eInfoType.PCLXLEnums)
            {
                rbSelTypePCLXLEnums.IsChecked = true;
                tabInfoType.SelectedItem = tabPCLXLEnums;
                setColsPCLXLEnums();
            }
            else if (infoType == eInfoType.PCLXLTags)
            {
                rbSelTypePCLXLTags.IsChecked = true;
                tabInfoType.SelectedItem = tabPCLXLTags;
                setColsPCLXLTags();
            }
            else if (infoType == eInfoType.PJLCmds)
            {
                rbSelTypePJLCmds.IsChecked = true;
                tabInfoType.SelectedItem = tabPJLCmds;
                setColsPJLCmds();
            }
            else if (infoType == eInfoType.PMLTags)
            {
                rbSelTypePMLTags.IsChecked = true;
                tabInfoType.SelectedItem = tabPMLTags;
                setColsPMLTags();
            }
            else if (infoType == eInfoType.SymbolSets)
            {
                rbSelTypeSymbolSets.IsChecked = true;
                tabInfoType.SelectedItem = tabSymbolSets;
                setColsSymbolSets();
            }
            else if (infoType == eInfoType.Fonts)
            {
                rbSelTypeFonts.IsChecked = true;
                tabInfoType.SelectedItem = tabFonts;
                setColsFonts();
            }
            else if (infoType == eInfoType.PaperSizes)
            {
                rbSelTypePaperSizes.IsChecked = true;
                tabInfoType.SelectedItem = tabPaperSizes;
                setColsPaperSizes();
            }
            else
            {
                rbSelTypePCL.IsChecked = true;
                tabInfoType.SelectedItem = tabPCL;
                setColsPCL();
            }

            _initialised = true;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m e t r i c s L o a d                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Load metrics from persistent storage.                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void metricsLoad ()
        {
            Int32 tmpInt = 0;

            ToolPrintLangPersist.loadDataCommon (ref _indxType,
                                                 ref _saveFilename);

            ToolPrintLangPersist.loadDataPCL (ref _flagPCLSeqControl,
                                              ref _flagPCLSeqSimple,
                                              ref _flagPCLSeqComplex,
                                              ref _flagPCLOptObsolete,
                                              ref _flagPCLOptDiscrete);

            ToolPrintLangPersist.loadDataPCLXL (ref _flagPCLXLTagDataType,
                                                ref _flagPCLXLTagAttribute,
                                                ref _flagPCLXLTagOperator,
                                                ref _flagPCLXLTagAttrDef,
                                                ref _flagPCLXLTagEmbedDataLen,
                                                ref _flagPCLXLTagWhitespace,
                                                ref _flagPCLXLOptReserved);

            ToolPrintLangPersist.loadDataPML (ref _flagPMLTagDataType,
                                              ref _flagPMLTagAction,
                                              ref _flagPMLTagOutcome);

            ToolPrintLangPersist.loadDataFonts (ref _flagSymSetList);

            ToolPrintLangPersist.loadDataSymSets (ref _flagSymSetMap,
                                                  ref tmpInt);

            //----------------------------------------------------------------//

            if ((_indxType < 0) || (_indxType >= _ctTypes))
                _indxType = (Int32) eInfoType.PCL;

            if ((tmpInt < 0) || (tmpInt >= (Int32) eSymSetMapType.Max))
                _symSetMapType = eSymSetMapType.Both;
            else
                _symSetMapType = (eSymSetMapType) tmpInt;

            //----------------------------------------------------------------//

            chkPCLSeqControl.IsChecked = _flagPCLSeqControl;
            chkPCLSeqSimple.IsChecked = _flagPCLSeqSimple;
            chkPCLSeqComplex.IsChecked = _flagPCLSeqComplex;

            chkPCLOptObsolete.IsChecked = _flagPCLOptObsolete;
            chkPCLOptDiscrete.IsChecked = _flagPCLOptDiscrete;

            //----------------------------------------------------------------//

            chkPCLXLTagDataType.IsChecked = _flagPCLXLTagDataType;
            chkPCLXLTagAttribute.IsChecked = _flagPCLXLTagAttribute;
            chkPCLXLTagOperator.IsChecked = _flagPCLXLTagOperator;
            chkPCLXLTagAttrDefiner.IsChecked = _flagPCLXLTagAttrDef;
            chkPCLXLTagEmbedDataLen.IsChecked = _flagPCLXLTagEmbedDataLen;
            chkPCLXLTagWhitespace.IsChecked = _flagPCLXLTagWhitespace;

            chkPCLXLOptReserved.IsChecked = _flagPCLXLOptReserved;

            //----------------------------------------------------------------//

            chkPMLTagDataType.IsChecked = _flagPMLTagDataType;
            chkPMLTagAction.IsChecked = _flagPMLTagAction;
            chkPMLTagOutcome.IsChecked = _flagPMLTagOutcome;

            //----------------------------------------------------------------//

            chkSymSetOptMap.IsChecked = _flagSymSetMap;

            if (_symSetMapType == eSymSetMapType.Std)
                rbSymSetMapStd.IsChecked = true;
            else if (_symSetMapType == eSymSetMapType.PCL)
                rbSymSetMapPCL.IsChecked = true;
            else
                rbSymSetMapBoth.IsChecked = true;

            if (_flagSymSetMap)
            {
                grpSymSetMapType.Visibility = Visibility.Visible;
            }
            else
            {
                grpSymSetMapType.Visibility = Visibility.Hidden;
            }

            //----------------------------------------------------------------//

            chkFontOptMap.IsChecked = _flagSymSetList;

            if (_flagSymSetList)
            {
                lbFontMapComment1.Visibility = Visibility.Visible;
                lbFontMapComment2.Visibility = Visibility.Visible;
                lbFontMapComment3.Visibility = Visibility.Visible;
            }
            else
            {
                lbFontMapComment1.Visibility = Visibility.Hidden;
                lbFontMapComment2.Visibility = Visibility.Hidden;
                lbFontMapComment3.Visibility = Visibility.Hidden;
            }
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
            ToolPrintLangPersist.saveDataCommon (_indxType,
                                                 _saveFilename);

            ToolPrintLangPersist.saveDataPCL (_flagPCLSeqControl,
                                              _flagPCLSeqSimple,
                                              _flagPCLSeqComplex,
                                              _flagPCLOptObsolete,
                                              _flagPCLOptDiscrete);

            ToolPrintLangPersist.saveDataPCLXL (_flagPCLXLTagDataType,
                                                _flagPCLXLTagAttribute,
                                                _flagPCLXLTagOperator,
                                                _flagPCLXLTagAttrDef,
                                                _flagPCLXLTagEmbedDataLen,
                                                _flagPCLXLTagWhitespace,
                                                _flagPCLXLOptReserved);

            ToolPrintLangPersist.saveDataPML (_flagPMLTagDataType,
                                              _flagPMLTagAction,
                                              _flagPMLTagOutcome);

            ToolPrintLangPersist.saveDataFonts (_flagSymSetList);

            ToolPrintLangPersist.saveDataSymSets (_flagSymSetMap,
                                                  (Int32) _symSetMapType);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b S e l T y p e F o n t s _ C l i c k                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Radio button selecting type Fonts clicked.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbSelTypeFonts_Click(object sender, RoutedEventArgs e)
        {
            _indxType = (Int32)eInfoType.Fonts;

            tabInfoType.SelectedItem = tabFonts;

            clearDetails();

            setColsFonts ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b S e l T y p e H P G L 2 _ C l i c k                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Radio button selecting type HP-GL/2 clicked.                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbSelTypeHPGL2_Click(object sender, RoutedEventArgs e)
        {
            _indxType = (Int32)eInfoType.HPGL2;

            tabInfoType.SelectedItem = tabHPGL2;

            clearDetails();

            setColsHPGL2();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b S e l T y p e P a p e r S i z e s _ C l i c k                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Radio button selecting type Paper Sizes clicked.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbSelTypePaperSizes_Click(object sender,
                                               RoutedEventArgs e)
        {
            _indxType = (Int32)eInfoType.PaperSizes;

            tabInfoType.SelectedItem = tabPaperSizes;

            clearDetails();

            setColsPaperSizes();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b S e l T y p e P C L _ C l i c k                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Radio button selecting type PCL clicked.                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbSelTypePCL_Click(object sender, RoutedEventArgs e)
        {
            _indxType = (Int32)eInfoType.PCL;

            tabInfoType.SelectedItem = tabPCL;

            clearDetails();

            setColsPCL();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b S e l T y p e P C L X L E n u m s _ C l i c k                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Radio button selecting type PCL XL Enums clicked.                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbSelTypePCLXLEnums_Click(object sender, RoutedEventArgs e)
        {
            _indxType = (Int32)eInfoType.PCLXLEnums;

            tabInfoType.SelectedItem = tabPCLXLEnums;

            clearDetails();

            setColsPCLXLEnums();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b S e l T y p e P C L X L T a g s _ C l i c k                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Radio button selecting type PCL XL Tags clicked.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbSelTypePCLXLTags_Click(object sender, RoutedEventArgs e)
        {
            _indxType = (Int32)eInfoType.PCLXLTags;

            tabInfoType.SelectedItem = tabPCLXLTags;

            clearDetails();

            setColsPCLXLTags();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b S e l T y p e P J L C m d s _ C l i c k                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Radio button selecting type PJL Commands clicked.                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbSelTypePJLCmds_Click(object sender, RoutedEventArgs e)
        {
            _indxType = (Int32)eInfoType.PJLCmds;

            tabInfoType.SelectedItem = tabPJLCmds;

            clearDetails ();

            setColsPJLCmds ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b S e l T y p e P M L T a g s _ C l i c k                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Radio button selecting type PML Tags clicked.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbSelTypePMLTags_Click(object sender, RoutedEventArgs e)
        {
            _indxType = (Int32)eInfoType.PMLTags;

            tabInfoType.SelectedItem = tabPMLTags;

            clearDetails ();

            setColsPMLTags ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b S e l T y p e P r e s c r i b e C m d s _ C l i c k            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Radio button selecting type PML Tags clicked.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbSelTypePrescribeCmds_Click (object sender,
                                                   RoutedEventArgs e)
        {
            _indxType = (Int32)eInfoType.PrescribeCmds;

            tabInfoType.SelectedItem = tabPrescribeCmds;

            clearDetails ();

            setColsPrescribeCmds ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b S e l T y p e S y m b o l S e t s _ C l i c k                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Radio button selecting type Symbol Sets clicked.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbSelTypeSymbolSets_Click(object sender, RoutedEventArgs e)
        {
            _indxType = (Int32)eInfoType.SymbolSets;

            tabInfoType.SelectedItem = tabSymbolSets;

            clearDetails ();

            setColsSymbolSets ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b S y m S e t M a p B o t h _ C l i c k                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Radio button selecting Symbol Set mapping 'Both'.                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbSymSetMapBoth_Click (object sender, RoutedEventArgs e)
        {
            _symSetMapType = eSymSetMapType.Both;

            clearDetails ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b S y m S e t M a p P C L _ C l i c k                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Radio button selecting Symbol Set mapping 'PCL'.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbSymSetMapPCL_Click (object sender, RoutedEventArgs e)
        {
            _symSetMapType = eSymSetMapType.PCL;

            clearDetails ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b S y m S e t M a p S t d _ C l i c k                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Radio button selecting Symbol Set mapping 'strict'.                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbSymSetMapStd_Click (object sender, RoutedEventArgs e)
        {
            _symSetMapType = eSymSetMapType.Std;

            clearDetails ();
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
            btnGenerate.Content = "Display requested data";
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t C o l s F o n t s                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Define datagrid columns for type Fonts.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void setColsFonts()
        {
            DataGridTextColumn colTypeface = new DataGridTextColumn ();
            DataGridTextColumn colName = new DataGridTextColumn ();
            DataGridTextColumn colSpacing = new DataGridTextColumn ();
            DataGridCheckBoxColumn colFlagScalable =
                new DataGridCheckBoxColumn ();
            DataGridTextColumn colBound = new DataGridTextColumn ();
            DataGridTextColumn colPitch = new DataGridTextColumn ();
            DataGridTextColumn colHeight = new DataGridTextColumn ();
            DataGridCheckBoxColumn colFlagVar_R =
                new DataGridCheckBoxColumn ();
            DataGridCheckBoxColumn colFlagVar_I =
                new DataGridCheckBoxColumn ();
            DataGridCheckBoxColumn colFlagVar_B =
                new DataGridCheckBoxColumn ();
            DataGridCheckBoxColumn colFlagVar_BI =
                new DataGridCheckBoxColumn ();
            DataGridTextColumn colSymbolSets = null;

            if (_flagSymSetList)
            {
                colSymbolSets = new DataGridTextColumn();
                colSymbolSets.Header = "Supported Symbol Sets?";
            }

            colTypeface.Header = "Typeface";
            colName.Header = "Name";
            colSpacing.Header = "Spacing";
            colFlagScalable.Header = "Scalable?";
            colBound.Header = "Bound?";
            colPitch.Header = "Pitch";
            colHeight.Header = "Height";
            colFlagVar_R.Header = "Regular?";
            colFlagVar_I.Header = "Italic?";
            colFlagVar_B.Header = "Bold?";
            colFlagVar_BI.Header = "Bold Italic?";

            dgSeq.Columns.Clear ();
            dgSeq.Columns.Add (colTypeface);
            dgSeq.Columns.Add (colName);
            dgSeq.Columns.Add (colSpacing);
            dgSeq.Columns.Add (colFlagScalable);
            dgSeq.Columns.Add (colBound);
            dgSeq.Columns.Add (colPitch);
            dgSeq.Columns.Add (colHeight);
            dgSeq.Columns.Add (colFlagVar_R);
            dgSeq.Columns.Add (colFlagVar_I);
            dgSeq.Columns.Add (colFlagVar_B);
            dgSeq.Columns.Add (colFlagVar_BI);

            if (_flagSymSetList)
            {
                dgSeq.Columns.Add(colSymbolSets);
            }

            Binding bindId = new Binding ("Typeface");
            bindId.Mode = BindingMode.OneWay;

            Binding bindName = new Binding ("Name");
            bindName.Mode = BindingMode.OneWay;

            Binding bindSpacing = new Binding ("Spacing");
            bindSpacing.Mode = BindingMode.OneWay;

            Binding bindFlagScalable = new Binding ("Scalable");
            bindFlagScalable.Mode = BindingMode.OneWay;

            Binding bindBound = new Binding ("BoundSymbolSet");
            bindBound.Mode = BindingMode.OneWay;

            Binding bindPitch = new Binding ("Pitch");
            bindPitch.Mode = BindingMode.OneWay;

            Binding bindHeight = new Binding ("Height");
            bindHeight.Mode = BindingMode.OneWay;

            Binding bindFlagVar_R = new Binding ("Var_Regular");
            bindFlagVar_R.Mode = BindingMode.OneWay;

            Binding bindFlagVar_I = new Binding ("Var_Italic");
            bindFlagVar_I.Mode = BindingMode.OneWay;

            Binding bindFlagVar_B = new Binding ("Var_Bold");
            bindFlagVar_B.Mode = BindingMode.OneWay;

            Binding bindFlagVar_BI = new Binding ("Var_BoldItalic");
            bindFlagVar_BI.Mode = BindingMode.OneWay;

            Binding bindSymbolSets = null;

            if (_flagSymSetList)
            {
                bindSymbolSets = new Binding("SymbolSets");
                bindSymbolSets.Mode = BindingMode.OneWay;
            }

            colTypeface.Binding = bindId;
            colName.Binding = bindName;
            colSpacing.Binding = bindSpacing;
            colFlagScalable.Binding = bindFlagScalable;
            colBound.Binding = bindBound;
            colPitch.Binding = bindPitch;
            colHeight.Binding = bindHeight;
            colFlagVar_R.Binding = bindFlagVar_R;
            colFlagVar_I.Binding = bindFlagVar_I;
            colFlagVar_B.Binding = bindFlagVar_B;
            colFlagVar_BI.Binding = bindFlagVar_BI;

            if (_flagSymSetList)
            {
                colSymbolSets.Binding = bindSymbolSets;
            }

            dgSeq.FontFamily = _fontProp;
            colTypeface.FontFamily = _fontFixed;
            colBound.FontFamily = _fontFixed;
            if (_flagSymSetList)
            {
                colSymbolSets.FontFamily = _fontFixed;
            }        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t C o l s H P G L 2                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Define datagrid columns for type HP-GL/2.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void setColsHPGL2()
        {
            DataGridTextColumn colMnemonic    = new DataGridTextColumn();
            DataGridTextColumn colDescription = new DataGridTextColumn();

            colMnemonic.Header = "Mnemonic";
            colDescription.Header = "Description";

            dgSeq.Columns.Clear();
            dgSeq.Columns.Add(colMnemonic);
            dgSeq.Columns.Add(colDescription);

            Binding bindMnemonic = new Binding("Mnemonic");
            bindMnemonic.Mode = BindingMode.OneWay;

            Binding bindDescription = new Binding("Description");
            bindDescription.Mode = BindingMode.OneWay;

            colMnemonic.Binding = bindMnemonic;
            colDescription.Binding = bindDescription;

            dgSeq.FontFamily = _fontProp;
            colMnemonic.FontFamily = _fontFixed;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t C o l s P a p e r S i z e s                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Define datagrid columns for type Paper Sizes.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void setColsPaperSizes()
        {
            DataGridTextColumn colName = new DataGridTextColumn();
            DataGridTextColumn colDesc = new DataGridTextColumn();
            DataGridTextColumn colEdgeShort = new DataGridTextColumn();
            DataGridTextColumn colEdgeLong = new DataGridTextColumn();
            DataGridTextColumn colIdPCL = new DataGridTextColumn();
            DataGridTextColumn colIdNamePCLXL = new DataGridTextColumn();

            colName.Header = "Name";
            colDesc.Header = "Description";
            colEdgeShort.Header = "Short edge";
            colEdgeLong.Header = "Long edge";
            colIdPCL.Header = "PCL Id";
            colIdNamePCLXL.Header = "PCLXL Id/Name";

            dgSeq.Columns.Clear();
            dgSeq.Columns.Add(colName);
            dgSeq.Columns.Add(colDesc);
            dgSeq.Columns.Add(colEdgeShort);
            dgSeq.Columns.Add(colEdgeLong);
            dgSeq.Columns.Add(colIdPCL);
            dgSeq.Columns.Add(colIdNamePCLXL);

            Binding bindName = new Binding("Name");
            bindName.Mode = BindingMode.OneWay;

            Binding bindDesc = new Binding("Desc");
            bindDesc.Mode = BindingMode.OneWay;

            Binding bindEdgeShort = new Binding("EdgeShort");
            bindEdgeShort.Mode = BindingMode.OneWay;

            Binding bindEdgeLong = new Binding("EdgeLong");
            bindEdgeLong.Mode = BindingMode.OneWay;

            Binding bindIdPCL = new Binding("IdPCL");
            bindIdPCL.Mode = BindingMode.OneWay;

            Binding bindIdNamePCLXL = new Binding("IdNamePCLXL");
            bindIdNamePCLXL.Mode = BindingMode.OneWay;

            colName.Binding  = bindName;
            colDesc.Binding  = bindDesc;
            colEdgeShort.Binding = bindEdgeShort;
            colEdgeLong.Binding = bindEdgeLong;
            colIdPCL.Binding = bindIdPCL;
            colIdNamePCLXL.Binding = bindIdNamePCLXL;

            dgSeq.FontFamily = _fontFixed;
            dgSeq.FontSize = 11;
            colName.FontFamily = _fontProp;
            colDesc.FontFamily = _fontProp;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t C o l s P C L                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Define datagrid columns for type PCL.                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void setColsPCL()
        {
            DataGridTextColumn colSequence = new DataGridTextColumn();
            DataGridTextColumn colType = new DataGridTextColumn();
            DataGridTextColumn colDescription = new DataGridTextColumn();
            DataGridCheckBoxColumn colFlagObsolete =
                new DataGridCheckBoxColumn();
            DataGridCheckBoxColumn colFlagValIsLen =
                new DataGridCheckBoxColumn();

            colSequence.Header = "Sequence";
            colType.Header = "Type";
            colFlagObsolete.Header = "Obsolete?";
            colFlagValIsLen.Header = "#=length?";
            colDescription.Header = "Description";

            dgSeq.Columns.Clear();
            dgSeq.Columns.Add(colSequence);
            dgSeq.Columns.Add(colType);
            dgSeq.Columns.Add(colFlagObsolete);
            dgSeq.Columns.Add(colFlagValIsLen);
            dgSeq.Columns.Add(colDescription);

            Binding bindSequence = new Binding("Sequence");
            bindSequence.Mode = BindingMode.OneWay;

            Binding bindType = new Binding("Type");
            bindType.Mode = BindingMode.OneWay;

            Binding bindFlagObsolete = new Binding("FlagObsolete");
            bindFlagObsolete.Mode = BindingMode.OneWay;

            Binding bindFlagValIsLen = new Binding("FlagValIsLen");
            bindFlagValIsLen.Mode = BindingMode.OneWay;

            Binding bindDescription = new Binding("Description");
            bindDescription.Mode = BindingMode.OneWay;

            colSequence.Binding = bindSequence;
            colType.Binding = bindType;
            colFlagObsolete.Binding = bindFlagObsolete;
            colFlagValIsLen.Binding = bindFlagValIsLen;
            colDescription.Binding = bindDescription;

            dgSeq.FontFamily = _fontProp;
            colSequence.FontFamily = _fontFixed;
            colDescription.FontFamily = _fontFixed;
            colDescription.FontSize = 11;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t C o l s P C L X L E n u m s                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Define datagrid columns for type PCL XL Enums.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void setColsPCLXLEnums()
        {
            DataGridTextColumn colTagOper = new DataGridTextColumn();
            DataGridTextColumn colTagAttr = new DataGridTextColumn();
            DataGridTextColumn colValue = new DataGridTextColumn();
            DataGridTextColumn colDescription = new DataGridTextColumn();

            colTagOper.Header = "Operator";
            colTagAttr.Header = "Attribute";
            colValue.Header = "Value";
            colDescription.Header = "Description";

            dgSeq.Columns.Clear();
            dgSeq.Columns.Add(colTagOper);
            dgSeq.Columns.Add(colTagAttr);
            dgSeq.Columns.Add(colValue);
            dgSeq.Columns.Add(colDescription);

            Binding bindTagOper = new Binding("Operator");
            bindTagOper.Mode = BindingMode.OneWay;

            Binding bindTagAttr = new Binding("Attribute");
            bindTagAttr.Mode = BindingMode.OneWay;

            Binding bindValue = new Binding("Value");
            bindValue.Mode = BindingMode.OneWay;

            Binding bindDescription = new Binding("Description");
            bindDescription.Mode = BindingMode.OneWay;

            colTagOper.Binding = bindTagOper;
            colTagAttr.Binding = bindTagAttr;
            colValue.Binding = bindValue;
            colDescription.Binding = bindDescription;

            dgSeq.FontFamily = _fontProp;
            colTagOper.FontFamily = _fontFixed;
            colTagAttr.FontFamily = _fontFixed;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t C o l s P C L X L T a g s                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Define datagrid columns for type PCL XL Tags.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void setColsPCLXLTags()
        {
            DataGridTextColumn colTag = new DataGridTextColumn();
            DataGridTextColumn colType = new DataGridTextColumn();
            DataGridTextColumn colDescription = new DataGridTextColumn();
            DataGridCheckBoxColumn colFlagReserved =
                new DataGridCheckBoxColumn();

            colTag.Header = "Tag value";
            colType.Header = "Type";
            colFlagReserved.Header = "Reserved?";
            colDescription.Header = "Description";

            dgSeq.Columns.Clear();
            dgSeq.Columns.Add(colTag);
            dgSeq.Columns.Add(colType);
            dgSeq.Columns.Add(colFlagReserved);
            dgSeq.Columns.Add(colDescription);

            Binding bindTag = new Binding("Tag");
            bindTag.Mode = BindingMode.OneWay;

            Binding bindType = new Binding("Type");
            bindType.Mode = BindingMode.OneWay;

            Binding bindFlagReserved = new Binding("FlagReserved");
            bindFlagReserved.Mode = BindingMode.OneWay;

            Binding bindDescription = new Binding("Description");
            bindDescription.Mode = BindingMode.OneWay;

            colTag.Binding = bindTag;
            colType.Binding = bindType;
            colFlagReserved.Binding = bindFlagReserved;
            colDescription.Binding = bindDescription;
        /*
            BindingSource BS = new BindingSource();
            DataTable testTable = new DataTable();
            testTable.Columns.Add("Column1", typeof(int));
            testTable.Columns.Add("Column2", typeof(string));
            testTable.Columns.Add("Column3", typeof(string));
            testTable.Rows.Add(1, "Value1", "Test1");
            testTable.Rows.Add(2, "Value2", "Test2");
            testTable.Rows.Add(2, "Value2", "Test1");
            testTable.Rows.Add(3, "Value3", "Test3");
            testTable.Rows.Add(4, "Value4", "Test4");
            testTable.Rows.Add(4, "Value4", "Test3");
            DataView view = testTable.DefaultView;
            view.Sort = "Column2 ASC, Column3 ASC";
         // Sorting Column 2 and column 3
            BS.DataSource = view;
            DataGridViewTextBoxColumn textColumn0 = new DataGridViewTextBoxColumn();
            textColumn0.DataPropertyName = "Column1";
            dataGridView1.Columns.Add(textColumn0);
            textColumn0.SortMode = DataGridViewColumnSortMode.Programmatic;
            textColumn0.HeaderCell.SortGlyphDirection = SortOrder.None;
            DataGridViewTextBoxColumn textColumn1 = new DataGridViewTextBoxColumn();
            textColumn1.DataPropertyName = "Column2";
            dataGridView1.Columns.Add(textColumn1);
            textColumn1.SortMode = DataGridViewColumnSortMode.Programmatic;
            textColumn1.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
            DataGridViewTextBoxColumn textColumn2 = new DataGridViewTextBoxColumn();
            textColumn2.DataPropertyName = "Column3";
            dataGridView1.Columns.Add(textColumn2);
            textColumn2.SortMode = DataGridViewColumnSortMode.Programmatic;
            textColumn2.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
            dataGridView1.DataSource = BS;         
        */

            dgSeq.FontFamily = _fontProp;
            colTag.FontFamily = _fontFixed;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t C o l s P J L C m d s                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Define datagrid columns for type PJL commands.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void setColsPJLCmds()
        {
            DataGridTextColumn colCmd = new DataGridTextColumn ();
            DataGridTextColumn colDescription = new DataGridTextColumn ();

            colCmd.Header = "Command";
            colDescription.Header = "Description";

            dgSeq.Columns.Clear ();
            dgSeq.Columns.Add (colCmd);
            dgSeq.Columns.Add (colDescription);

            Binding bindCmd = new Binding ("Name");
            bindCmd.Mode = BindingMode.OneWay;

            Binding bindDescription = new Binding ("Description");
            bindDescription.Mode = BindingMode.OneWay;

            colCmd.Binding = bindCmd;
            colDescription.Binding = bindDescription;

            dgSeq.FontFamily = _fontProp;
            colCmd.FontFamily = _fontFixed;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t C o l s P M L T a g s                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Define datagrid columns for type PML Tags.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void setColsPMLTags()
        {
            DataGridTextColumn colTag = new DataGridTextColumn ();
            DataGridTextColumn colType = new DataGridTextColumn ();
            DataGridTextColumn colDescription = new DataGridTextColumn ();

            colTag.Header = "Tag value";
            colType.Header = "Type";
            colDescription.Header = "Description";

            dgSeq.Columns.Clear ();
            dgSeq.Columns.Add (colTag);
            dgSeq.Columns.Add (colType);
            dgSeq.Columns.Add (colDescription);

            Binding bindTag = new Binding ("Tag");
            bindTag.Mode = BindingMode.OneWay;

            Binding bindType = new Binding ("Type");
            bindType.Mode = BindingMode.OneWay;

            Binding bindDescription = new Binding ("Description");
            bindDescription.Mode = BindingMode.OneWay;

            colTag.Binding = bindTag;
            colType.Binding = bindType;
            colDescription.Binding = bindDescription;

            dgSeq.FontFamily = _fontProp;
            colTag.FontFamily = _fontFixed;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t C o l s P r e s c r i b e C m d s                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Define datagrid columns for type Prescribe commands.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void setColsPrescribeCmds()
        {
            DataGridTextColumn colCmd = new DataGridTextColumn();
            DataGridTextColumn colDescription = new DataGridTextColumn();

            colCmd.Header = "Command";
            colDescription.Header = "Description";

            dgSeq.Columns.Clear();
            dgSeq.Columns.Add(colCmd);
            dgSeq.Columns.Add(colDescription);

            Binding bindCmd = new Binding("Name");
            bindCmd.Mode = BindingMode.OneWay;

            Binding bindDescription = new Binding("Description");
            bindDescription.Mode = BindingMode.OneWay;

            colCmd.Binding = bindCmd;
            colDescription.Binding = bindDescription;

            dgSeq.FontFamily = _fontProp;
            colCmd.FontFamily = _fontFixed;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t C o l s S y m b o l S e t s                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Define datagrid columns for type Symbol Sets.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void setColsSymbolSets ()
        {
            DataGridTextColumn colGroup = new DataGridTextColumn ();
            DataGridTextColumn colType = new DataGridTextColumn ();
            DataGridTextColumn colId = new DataGridTextColumn ();
            DataGridTextColumn colKind1 = new DataGridTextColumn ();
            DataGridTextColumn colAlias = new DataGridTextColumn ();
            DataGridTextColumn colName = new DataGridTextColumn ();
            DataGridTextColumn colMapStd = null;
            DataGridTextColumn colMapPCL = null;
            DataGridTextColumn colMapDiff = null;
            DataGridCheckBoxColumn colFlagMapStd = null;
            DataGridCheckBoxColumn colFlagMapPCL = null;

            Boolean showMaps = _flagSymSetMap;

            if (showMaps)
            {
                if (_symSetMapType == eSymSetMapType.Both)
                {
                    colMapStd = new DataGridTextColumn ();
                    colMapStd.Header = "Mapping (Strict)";

                    colMapPCL = new DataGridTextColumn ();
                    colMapPCL.Header = "Mapping (LaserJet)";

                    colMapDiff = new DataGridTextColumn ();
                    colMapDiff.Header = "Mapping (difference)";
                }
                else if (_symSetMapType == eSymSetMapType.PCL)
                {
                    colMapPCL = new DataGridTextColumn ();
                    colMapPCL.Header = "Mapping (LaserJet)";
                }
                else
                {
                    colMapStd = new DataGridTextColumn ();
                    colMapStd.Header = "Mapping (Strict)";
                }
            }
            else
            {
                colFlagMapStd = new DataGridCheckBoxColumn ();
                colFlagMapStd.Header = "Map (Strict)?";
                colFlagMapPCL = new DataGridCheckBoxColumn ();
                colFlagMapPCL.Header = "Map (Laserjet)?";
            }

            colGroup.Header = "Group";
            colType.Header = "Type";
            colId.Header = "PCL ID";
            colKind1.Header = "Kind1";
            colAlias.Header = "Alias";
            colName.Header = "Name";

            dgSeq.Columns.Clear ();
            dgSeq.Columns.Add (colGroup);
            dgSeq.Columns.Add (colType);
            dgSeq.Columns.Add (colId);
            dgSeq.Columns.Add (colKind1);
            dgSeq.Columns.Add (colAlias);
            dgSeq.Columns.Add (colName);

            if (showMaps)
            {
                if (_symSetMapType == eSymSetMapType.Both)
                {
                    dgSeq.Columns.Add (colMapStd);
                    dgSeq.Columns.Add (colMapPCL);
                    dgSeq.Columns.Add (colMapDiff);
                }
                else if (_symSetMapType == eSymSetMapType.PCL)
                {
                    dgSeq.Columns.Add (colMapPCL);
                }
                else
                {
                    dgSeq.Columns.Add (colMapStd);
                }
            }
            else
            {
                dgSeq.Columns.Add (colFlagMapStd);
                dgSeq.Columns.Add (colFlagMapPCL);
            }

            Binding bindGroup = new Binding ("Groupname");
            bindGroup.Mode = BindingMode.OneWay;

            Binding bindType = new Binding ("TypeDescShort");
            bindType.Mode = BindingMode.OneWay;

            Binding bindId = new Binding ("Id");
            bindId.Mode = BindingMode.OneWay;

            Binding bindKind1 = new Binding ("Kind1");
            bindKind1.Mode = BindingMode.OneWay;

            Binding bindAlias = new Binding ("Alias");
            bindAlias.Mode = BindingMode.OneWay;

            Binding bindName = new Binding ("Name");
            bindName.Mode = BindingMode.OneWay;

            Binding bindMapStd = null;
            Binding bindMapPCL = null;
            Binding bindMapDiff = null;
            Binding bindFlagMapStd = null;
            Binding bindFlagMapPCL = null;

            if (showMaps)
            {
                if (_symSetMapType == eSymSetMapType.Both)
                {
                    bindMapStd = new Binding ("MappingStd");
                    bindMapPCL = new Binding ("MappingPCL");
                    bindMapDiff = new Binding ("MappingDiff");

                    bindMapStd.Mode = BindingMode.OneWay;
                    bindMapPCL.Mode = BindingMode.OneWay;
                    bindMapDiff.Mode = BindingMode.OneWay;

                    colMapStd.Binding = bindMapStd;
                    colMapPCL.Binding = bindMapPCL;
                    colMapDiff.Binding = bindMapDiff;
                }
                else if (_symSetMapType == eSymSetMapType.PCL)
                {
                    bindMapPCL = new Binding ("MappingPCL");
                    bindMapPCL.Mode = BindingMode.OneWay;
                    colMapPCL.Binding = bindMapPCL;
                }
                else
                {
                    bindMapStd = new Binding ("MappingStd");
                    bindMapStd.Mode = BindingMode.OneWay;
                    colMapStd.Binding = bindMapStd;
                }
            }
            else
            {
                bindFlagMapStd = new Binding ("FlagMapStd");
                bindFlagMapStd.Mode = BindingMode.OneWay;
                colFlagMapStd.Binding = bindFlagMapStd;

                bindFlagMapPCL = new Binding ("FlagMapPCL");
                bindFlagMapPCL.Mode = BindingMode.OneWay;
                colFlagMapPCL.Binding = bindFlagMapPCL;
            }

            colGroup.Binding = bindGroup;
            colType.Binding = bindType;
            colId.Binding = bindId;
            colKind1.Binding = bindKind1;
            colAlias.Binding = bindAlias;
            colName.Binding = bindName;

            dgSeq.FontFamily = _fontProp;
            colGroup.FontFamily = _fontFixed;
            colType.FontFamily = _fontFixed;
            colId.FontFamily = _fontFixed;
            colKind1.FontFamily = _fontFixed;
            colAlias.FontFamily = _fontFixed;

            if (showMaps)
            {
                if (_symSetMapType == eSymSetMapType.Both)
                {
                    colMapStd.FontFamily = _fontFixed;
                    colMapPCL.FontFamily = _fontFixed;
                    colMapDiff.FontFamily = _fontFixed;
                }
                else if (_symSetMapType == eSymSetMapType.PCL)
                {
                    colMapPCL.FontFamily = _fontFixed;
                }
                else 
                {
                    colMapStd.FontFamily = _fontFixed;
                }
            }
        }
    }
}
