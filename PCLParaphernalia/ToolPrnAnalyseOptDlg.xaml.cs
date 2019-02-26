using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PCLParaphernalia
{
    /// <summary>
    /// Interaction logic for ToolPrnAnalyseOptDlg.xaml
    /// </summary>

    public partial class ToolPrnAnalyseOptDlg : Window
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private const Int32 _ctClrMapThemes = 5;

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private PrnParseOptions _options;

        private PrnParseConstants.eOptCharSets _indxCharSetName;

        private PrnParseConstants.eOptCharSetSubActs _indxCharSetSubAct;

        private PrnParseConstants.eOptOffsetFormats _indxGenOffsetFormat;

        private PrnParseConstants.eOptOffsetFormats _indxCurFOffsetFormat;

        private PrnParseConstants.eOptStatsLevel _indxStatsLevel;

        private ToolCommonData.ePrintLang _indxCurFInitLang;

        private PrnParseConstants.ePCLXLBinding _indxCurFXLBinding;

        private TabItem _crntTab;

        private Int64 _fileSize;

        private PropertyInfo [] _stdClrsPropertyInfo;

        private String [] _clrMapThemeNames;

        private TextBox [] _txtClrMapSamples;
        private Int32 _ctClrMapStdClrs;
        private Int32 _ctClrMapRowTypes;
        private Int32 _crntClrMapTheme;
        private Int32 _crntClrMapRowType;
        private Int32 [] _indxClrMapBack;
        private Int32 [] _indxClrMapFore;

        private Int32 _valCharSetSubCode;

        private Int32 _valCurFOffsetStart;
        private Int32 _valCurFOffsetEnd;
        private Int32 _valCurFOffsetMax;

        private Int32 _valPCLFontDrawHeight;
        private Int32 _valPCLFontDrawWidth;

        private Int32 _valPCLXLFontDrawHeight;
        private Int32 _valPCLXLFontDrawWidth;

        
        private Boolean _initialised;
        private Boolean _inhibitChecks;
        private Boolean _fileOpen;

        private Boolean _flagClrMapUseClr;

        private Boolean _flagGenMiscAutoAnalyse;
        private Boolean _flagGenDiagFileAccess;

        private Boolean _flagHPGL2MiscBinData;

        private Boolean _flagPCLMacroDisplay;
        
        private Boolean _flagPCLMiscBinData;
        private Boolean _flagPCLMiscStyleData;

        private Boolean _flagPCLTransAlphaNumId;
        private Boolean _flagPCLTransColourLookup;
        private Boolean _flagPCLTransConfIO;
        private Boolean _flagPCLTransConfImageData;
        private Boolean _flagPCLTransConfRasterData;
        private Boolean _flagPCLTransDefLogPage;
        private Boolean _flagPCLTransDefSymSet;
        private Boolean _flagPCLTransDitherMatrix;
        private Boolean _flagPCLTransDriverConf;
        private Boolean _flagPCLTransEscEncText;
        private Boolean _flagPCLTransPaletteConf;
        private Boolean _flagPCLTransUserPattern;
        private Boolean _flagPCLTransViewIlluminant;

        private Boolean _flagPCLFontChar;
        private Boolean _flagPCLFontHddr;
        private Boolean _flagPCLFontDraw;

        private Boolean _flagPCLXLEncPCLFontSelect;
        private Boolean _flagPCLXLEncPCLPassThrough;
        private Boolean _flagPCLXLEncUserStream;
        private Boolean _flagPCLXLMiscBinData;
        private Boolean _flagPCLXLMiscOperPos;
        private Boolean _flagPCLXLMiscVerbose;
        private Boolean _flagPCLXLFontChar;
        private Boolean _flagPCLXLFontHddr;
        private Boolean _flagPCLXLFontDraw;

        private Boolean _flagPMLMiscVerbose;
        private Boolean _flagPMLWithinPCL;
        private Boolean _flagPMLWithinPJL;

        private Boolean _flagStatsExcUnusedPCLObs;
        private Boolean _flagStatsExcUnusedPCLXLRes;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // T o o l P r n A n a l y s e O p t D l g                            //
        //                                                                    //
        // This is a modal form, and it is created dynamically when required. //
        // As the form is destroyed after each invocation, values cannot be   //
        // returned via a member function; they must be returned via a        //
        // reference to a class instance supplied to the constructor.         //
        //                                                                    //
        //--------------------------------------------------------------------//
        
        public ToolPrnAnalyseOptDlg(PrnParseOptions options,
                                    Int64           fileSize)
        {
            _inhibitChecks = true;

            InitializeComponent ();

            initialise (options, fileSize);
            
            _inhibitChecks = false;
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
        // b t n C l r M a p S e t A l l R o w T y p e s S a m e _ C l i c k  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'Set all row types to these colours' button is     //
        // clicked.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void btnClrMapSetAllRowTypesSame_Click (
            object sender,
            RoutedEventArgs e)
        {
            Int32 indxClrBack = cbClrMapBack.SelectedIndex;
            Int32 indxClrFore = cbClrMapFore.SelectedIndex;

            PropertyInfo pInfoBack =
                cbClrMapBack.SelectedItem as PropertyInfo;
            PropertyInfo pInfoFore =
                cbClrMapFore.SelectedItem as PropertyInfo;

            Color selClrBack = (Color)pInfoBack.GetValue (null, null);
            Color selClrFore = (Color)pInfoFore.GetValue (null, null);

            SolidColorBrush selBrBack = new SolidColorBrush ();
            SolidColorBrush selBrFore = new SolidColorBrush ();

            selBrBack.Color = selClrBack;
            selBrFore.Color = selClrFore;

            for (Int32 i = 0; i < _ctClrMapRowTypes; i++)
            {
                _txtClrMapSamples[i].Background = selBrBack;
                _txtClrMapSamples[i].Foreground = selBrFore;

                _txtClrMapSamples[i].ToolTip =
                    pInfoBack.Name + " / " + pInfoFore.Name;

                _indxClrMapBack[i] = indxClrBack;
                _indxClrMapFore[i] = indxClrFore;
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Reset 'save/restore' items.                                    //
            //                                                                //
            //----------------------------------------------------------------//

            cbClrMapTheme.SelectedIndex = -1;

            txtClrMapThemeName.Text = "";

            btnClrMapThemeRestore.IsEnabled = false;
            btnClrMapThemeSave.IsEnabled = false;
            txtClrMapThemeName.IsEnabled = false;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b t n C l r M a p T h e m e R e s e t _ C l i c k                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the colour code mapping 'Reset to built-in default'    //
        // button is clicked.                                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void btnClrMapThemeReset_Click (object sender,
                                                RoutedEventArgs e)
        {
            Int32 indxClrBack;
            Int32 indxClrFore;

            PropertyInfo pInfoBack,
                         pInfoFore;

            //----------------------------------------------------------------//
            //                                                                //
            // Set the array indices to the application defaults.             //
            //                                                                //
            //----------------------------------------------------------------//

            PrnParseRowTypes.setDefaultClrs (ref _indxClrMapBack,
                                             ref _indxClrMapFore);

            //----------------------------------------------------------------//
            //                                                                //
            // Reset the background and foreground colours of the sample text //
            // boxes.                                                         //
            //                                                                //
            //----------------------------------------------------------------//

            for (Int32 i = 0; i < _ctClrMapRowTypes; i++)
            {
                Color clrBack = new Color (),
                      clrFore = new Color ();

                SolidColorBrush brushBack = new SolidColorBrush (),
                                brushFore = new SolidColorBrush ();

                indxClrBack = _indxClrMapBack[i];
                indxClrFore = _indxClrMapFore[i];

                pInfoBack = _stdClrsPropertyInfo[indxClrBack] as PropertyInfo;
                pInfoFore = _stdClrsPropertyInfo[indxClrFore] as PropertyInfo;

                clrBack = (Color)pInfoBack.GetValue (null, null);
                clrFore = (Color)pInfoFore.GetValue (null, null);

                brushBack.Color = clrBack;
                brushFore.Color = clrFore;

                _txtClrMapSamples[i].Background = brushBack;
                _txtClrMapSamples[i].Foreground = brushFore;

                _txtClrMapSamples[i].ToolTip = pInfoBack.Name + " / " +
                                               pInfoFore.Name;
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Reset the colours of the colour selection combo boxes to those //
            // associated with the current selected row type.                 //
            //                                                                //
            //----------------------------------------------------------------//

            _crntClrMapRowType = cbClrMapRowType.SelectedIndex;

            indxClrBack = _indxClrMapBack[_crntClrMapRowType];
            indxClrFore = _indxClrMapFore[_crntClrMapRowType];

            cbClrMapBack.SelectedIndex = indxClrBack;
            cbClrMapFore.SelectedIndex = indxClrFore;

            //----------------------------------------------------------------//
            //                                                                //
            // Reset 'save/restore' items.                                    //
            //                                                                //
            //----------------------------------------------------------------//

            cbClrMapTheme.SelectedIndex = -1;

            txtClrMapThemeName.Text = "";

            btnClrMapThemeRestore.IsEnabled = false;
            btnClrMapThemeSave.IsEnabled = false;
            txtClrMapThemeName.IsEnabled = false;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b t n C l r M a p T h e m e R e s t o r e _ C l i c k              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the colour code mapping 'Restore from user theme'      //
        // button is clicked.                                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void btnClrMapThemeRestore_Click (object sender,
                                                  RoutedEventArgs e)
        {
            Int32 indxClrBack;
            Int32 indxClrFore;

            PropertyInfo pInfoBack,
                         pInfoFore;

            //----------------------------------------------------------------//
            //                                                                //
            // Set the array indices to the stored user defaults.             //
            //                                                                //
            //----------------------------------------------------------------//

            ToolPrnAnalysePersist.loadOptClrMapTheme (_crntClrMapTheme,
                                                      ref _indxClrMapBack,
                                                      ref _indxClrMapFore);

            //----------------------------------------------------------------//
            //                                                                //
            // Reset the background and foreground colours of the sample text //
            // boxes.                                                         //
            //                                                                //
            //----------------------------------------------------------------//

            for (Int32 i = 0; i < _ctClrMapRowTypes; i++)
            {
                Color clrBack = new Color (),
                      clrFore = new Color ();

                SolidColorBrush brushBack = new SolidColorBrush (),
                                brushFore = new SolidColorBrush ();

                indxClrBack = _indxClrMapBack[i];
                indxClrFore = _indxClrMapFore[i];

                pInfoBack = _stdClrsPropertyInfo[indxClrBack] as PropertyInfo;
                pInfoFore = _stdClrsPropertyInfo[indxClrFore] as PropertyInfo;

                clrBack = (Color)pInfoBack.GetValue (null, null);
                clrFore = (Color)pInfoFore.GetValue (null, null);

                brushBack.Color = clrBack;
                brushFore.Color = clrFore;

                _txtClrMapSamples[i].Background = brushBack;
                _txtClrMapSamples[i].Foreground = brushFore;

                _txtClrMapSamples[i].ToolTip = pInfoBack.Name + " / " +
                                               pInfoFore.Name;
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Reset the colours of the colour selection combo boxes to those //
            // associated with the current selected row type.                 //
            //                                                                //
            //----------------------------------------------------------------//

            _crntClrMapRowType = cbClrMapRowType.SelectedIndex;

            indxClrBack = _indxClrMapBack[_crntClrMapRowType];
            indxClrFore = _indxClrMapFore[_crntClrMapRowType];
            _inhibitChecks = true;
            cbClrMapBack.SelectedIndex = indxClrBack;
            cbClrMapFore.SelectedIndex = indxClrFore;
            _inhibitChecks = false;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b t n C l r M a p T h e m e S a v e _ C l i c k                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the colour code mapping 'save as user theme' button    //
        // is clicked.                                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void btnClrMapThemeSave_Click (object sender,
                                               RoutedEventArgs e)
        {
            //----------------------------------------------------------------//
            //                                                                //
            // Save the current arry values in the registry.                  //
            //                                                                //
            //----------------------------------------------------------------//

            String name = _clrMapThemeNames[_crntClrMapTheme];

            ToolPrnAnalysePersist.saveOptClrMapTheme (
                _crntClrMapTheme,
                _indxClrMapBack,
                _indxClrMapFore,
                name);
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
            _inhibitChecks = true;

            //----------------------------------------------------------------//
            //                                                                //
            // Store current values.                                          //
            //                                                                //
            //----------------------------------------------------------------//

            _options.setOptCharSet (_indxCharSetName,
                                    _indxCharSetSubAct,
                                    _valCharSetSubCode);

            _options.setOptClrMap (_flagClrMapUseClr,
                                   _indxClrMapBack,
                                   _indxClrMapFore);

            _options.setOptCurF (_indxCurFInitLang,
                                 _indxCurFXLBinding,
                                 _indxCurFOffsetFormat,
                                 _valCurFOffsetStart,
                                 _valCurFOffsetEnd);

            _options.setOptGeneral (_indxGenOffsetFormat,
                                    _flagGenMiscAutoAnalyse,
                                    _flagGenDiagFileAccess);

            _options.setOptHPGL2 (_flagHPGL2MiscBinData);

            _options.setOptPCL (_flagPCLFontHddr,
                                _flagPCLFontChar,
                                _flagPCLFontDraw,
                                _valPCLFontDrawHeight,
                                _valPCLFontDrawWidth,
                                _flagPCLMacroDisplay,
                                _flagPCLMiscStyleData,
                                _flagPCLMiscBinData,
                                _flagPCLTransAlphaNumId,
                                _flagPCLTransColourLookup,
                                _flagPCLTransConfIO,
                                _flagPCLTransConfImageData,
                                _flagPCLTransConfRasterData,
                                _flagPCLTransDefLogPage,
                                _flagPCLTransDefSymSet,
                                _flagPCLTransDitherMatrix,
                                _flagPCLTransDriverConf,
                                _flagPCLTransEscEncText,
                                _flagPCLTransPaletteConf,
                                _flagPCLTransUserPattern,
                                _flagPCLTransViewIlluminant);

            _options.setOptPCLXL (_flagPCLXLFontHddr,
                                  _flagPCLXLFontChar,
                                  _flagPCLXLFontDraw,
                                  _valPCLXLFontDrawHeight,
                                  _valPCLXLFontDrawWidth,
                                  _flagPCLXLEncUserStream,
                                  _flagPCLXLEncPCLPassThrough,
                                  _flagPCLXLEncPCLFontSelect,
                                  _flagPCLXLMiscOperPos,
                                  _flagPCLXLMiscBinData,
                                  _flagPCLXLMiscVerbose);
            
            _options.setOptPML (_flagPMLWithinPCL,
                                _flagPMLWithinPJL,
                                _flagPMLMiscVerbose);

            _options.setOptStats (_indxStatsLevel,
                                  _flagStatsExcUnusedPCLObs,
                                  _flagStatsExcUnusedPCLXLRes);

            //----------------------------------------------------------------//
            //                                                                //
            // Close form.                                                    //
            //                                                                //
            //----------------------------------------------------------------//
            
            this.DialogResult = true;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b C l r M a p B a c k _ S e l e c t i o n C h a n g e d          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Colour mapping background item has changed.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbClrMapBack_SelectionChanged (object sender,
                                                    SelectionChangedEventArgs e)
        {
            if (_initialised && cbClrMapBack.HasItems & !_inhibitChecks)
            {
                PropertyInfo pInfoBack =
                    cbClrMapBack.SelectedItem as PropertyInfo;
                PropertyInfo pInfoFore =
                    cbClrMapFore.SelectedItem as PropertyInfo;

                Color selClr = (Color)pInfoBack.GetValue (null, null);

                SolidColorBrush selBr = new SolidColorBrush ();

                cbClrMapBack.ToolTip = pInfoBack.Name;

                selBr.Color = selClr;

                _txtClrMapSamples[_crntClrMapRowType].Background = selBr;
                _txtClrMapSamples[_crntClrMapRowType].ToolTip =
                    pInfoBack.Name + " / " + pInfoFore.Name;

                _indxClrMapBack [_crntClrMapRowType] = 
                    cbClrMapBack.SelectedIndex;

                //------------------------------------------------------------//
                //                                                            //
                // Reset 'save/restore' items.                                //
                //                                                            //
                //------------------------------------------------------------//

                cbClrMapTheme.SelectedIndex = -1;

                txtClrMapThemeName.Text = "";

                btnClrMapThemeRestore.IsEnabled = false;
                btnClrMapThemeSave.IsEnabled = false;
                txtClrMapThemeName.IsEnabled = false;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b C l r M a p F o r e _ S e l e c t i o n C h a n g e d          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Colour mapping foreground item has changed.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbClrMapFore_SelectionChanged (object sender,
                                                    SelectionChangedEventArgs e)
        {
            if (_initialised && cbClrMapFore.HasItems & !_inhibitChecks)
            {
                PropertyInfo pInfoBack =
                    cbClrMapBack.SelectedItem as PropertyInfo;
                PropertyInfo pInfoFore =
                    cbClrMapFore.SelectedItem as PropertyInfo;

                Color selClr = (Color) pInfoFore.GetValue (null, null);

                SolidColorBrush selBr = new SolidColorBrush ();

                cbClrMapFore.ToolTip = pInfoFore.Name;

                selBr.Color = selClr;

                _txtClrMapSamples[_crntClrMapRowType].Foreground = selBr;
                _txtClrMapSamples[_crntClrMapRowType].ToolTip =
                    pInfoBack.Name + " / " + pInfoFore.Name;

                _indxClrMapFore[_crntClrMapRowType] =
                    cbClrMapFore.SelectedIndex;

                //------------------------------------------------------------//
                //                                                            //
                // Reset 'save/restore' items.                                //
                //                                                            //
                //------------------------------------------------------------//

                cbClrMapTheme.SelectedIndex = -1;

                txtClrMapThemeName.Text = "";

                btnClrMapThemeRestore.IsEnabled = false;
                btnClrMapThemeSave.IsEnabled = false;
                txtClrMapThemeName.IsEnabled = false;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b C l r M a p R o w T y p e _ S e l e c t i o n C h a n g e d    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Colour coding row type item has changed.                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbClrMapRowType_SelectionChanged (object sender,
                                                       SelectionChangedEventArgs e)
        {
            if (_initialised && cbClrMapRowType.HasItems)
            {
                Int32 indxClrBack,
                      indxClrFore;

                _crntClrMapRowType = cbClrMapRowType.SelectedIndex;

                indxClrBack = _indxClrMapBack[_crntClrMapRowType];
                indxClrFore = _indxClrMapFore[_crntClrMapRowType];

                cbClrMapBack.SelectedIndex = indxClrBack;
                cbClrMapFore.SelectedIndex = indxClrFore;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b C l r M a p T h e m e _ S e l e c t i o n C h a n g e d        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Colour coding row type item has changed.                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbClrMapTheme_SelectionChanged (object sender,
                                                     SelectionChangedEventArgs e)
        {
            if (_initialised && cbClrMapTheme.HasItems &!_inhibitChecks)
            {
                String name = "";

                Int32 tempInt = cbClrMapTheme.SelectedIndex;

                if (tempInt >= 0)
                {
                    _crntClrMapTheme = tempInt;

                    ToolPrnAnalysePersist.loadOptClrMapThemeName (
                        _crntClrMapTheme, ref name);

                    txtClrMapThemeName.Text = name;

                    _clrMapThemeNames[_crntClrMapTheme] = name;

                    btnClrMapThemeRestore.IsEnabled = true;
                    btnClrMapThemeSave.IsEnabled = true;
                    txtClrMapThemeName.IsEnabled = true;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k C l r M a p U s e C l r _ C h a n g e                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The Colour coding 'use colour coding' check box has been changed.  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkClrMapUseClr_Change (object sender,
                                             RoutedEventArgs e)
        {
            if (!_inhibitChecks)
            {
                _flagClrMapUseClr = (Boolean)chkClrMapUseClr.IsChecked;

                if (_flagClrMapUseClr)
                {
                    grpClrMapSelect.Visibility = Visibility.Visible;
                    grpClrMapSamples.Visibility = Visibility.Visible;
                    grpClrMapThemes.Visibility = Visibility.Visible;
               }
                else
                {
                    grpClrMapSelect.Visibility = Visibility.Hidden;
                    grpClrMapSamples.Visibility = Visibility.Hidden;
                    grpClrMapThemes.Visibility = Visibility.Hidden;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k G e n D i a g F i l e A c c e s s _ C h a n g e              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The diagnstic 'Log file access data ' check box has been changed.  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkGenDiagFileAccess_Change (object sender,
                                                  RoutedEventArgs e)
        {
            if (! _inhibitChecks)
            {
                _flagGenDiagFileAccess =
                    (Boolean)chkGenDiagFileAccess.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k G e n M i s c A u t o A n a l y s e _ C h a n g e            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The General 'Auto analyse on file open' check box has been changed.//
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkGenMiscAutoAnalyse_Change(object sender,
                                                  RoutedEventArgs e)
        {
            if (! _inhibitChecks)
            {
                _flagGenMiscAutoAnalyse =
                    (Boolean) chkGenMiscAutoAnalyse.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k H P G L 2 M i s c B i n D a t a _ C h a n g e                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The HPGL2 'Show binary data' check box has been changed.           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkHPGL2MiscBinData_Change(object sender,
                                                RoutedEventArgs e)
        {
            if (! _inhibitChecks)
            {
                _flagHPGL2MiscBinData =
                    (Boolean) chkHPGL2MiscBinData.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L F o n t C h a r _ C h a n g e                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL 'Analyse Font Character' check box has been changed.       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLFontChar_Change(object sender,
                                           RoutedEventArgs e)
        {
            if (! _inhibitChecks)
            {
                _flagPCLFontChar = (Boolean) chkPCLFontChar.IsChecked;

                chkPCLFontDraw.IsEnabled = false;
                sldrPCLFontDrawHeight.IsEnabled = false;
                sldrPCLFontDrawWidth.IsEnabled = false;

                if (_flagPCLFontChar)
                {
                    chkPCLFontDraw.IsEnabled = true;

                    if (_flagPCLFontDraw)
                    {
                        sldrPCLFontDrawHeight.IsEnabled = true;
                        sldrPCLFontDrawWidth.IsEnabled = true;
                    }
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L F o n t D r a w _ C h a n g e                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL 'Draw Font Character' check box has been changed.          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLFontDraw_Change(object sender,
                                               RoutedEventArgs e)
        {
            if (! _inhibitChecks)
            {
                _flagPCLFontDraw = (Boolean) chkPCLFontDraw.IsChecked;

                if (_flagPCLFontDraw)
                {
                    sldrPCLFontDrawHeight.IsEnabled = true;
                    sldrPCLFontDrawWidth.IsEnabled = true;
                }
                else
                {
                    sldrPCLFontDrawHeight.IsEnabled = false;
                    sldrPCLFontDrawWidth.IsEnabled = false;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L F o n t H d d r _ C h a n g e                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL 'Analyse Font Header' check box has been changed.          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLFontHddr_Change(object sender,
                                           RoutedEventArgs e)
        {
            if (! _inhibitChecks)
            {
                _flagPCLFontHddr = (Boolean) chkPCLFontHddr.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L M a c r o D i s p l a y _ C h a n g e                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL 'Display macro contents' check box has been changed.       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLMacroDisplay_Change(object sender,
                                               RoutedEventArgs e)
        {
            if (! _inhibitChecks)
            {
                _flagPCLMacroDisplay = (Boolean) chkPCLMacroDisplay.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L M i s c B i n D a t a _ C h a n g e                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL 'Display binary data' check box has been changed.          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLMiscBinData_Change(object sender,
                                              RoutedEventArgs e)
        {
            if (! _inhibitChecks)
            {
                _flagPCLMiscBinData = (Boolean) chkPCLMiscBinData.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L M i s c S t y l e D a t a _ C h a n g e                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL 'Interpret style value' check box has been changed.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLMiscStyleData_Change(object sender,
                                                RoutedEventArgs e)
        {
            if (! _inhibitChecks)
            {
                _flagPCLMiscStyleData = (Boolean) chkPCLMiscStyleData.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L T r a n s A l p h a N u m I d _ C h a n g e            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL interpret 'Alphanumeric ID' check box has been changed.    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLTransAlphaNumId_Change(object sender,
                                                  RoutedEventArgs e)
        {
            if (!_inhibitChecks)
            {
                _flagPCLTransAlphaNumId =
                    (Boolean)chkPCLTransAlphaNumId.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L T r a n s C o l o u r L o o k u p _ C h a n g e        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL interpret 'Colour Lookup Tables' check box has been        //
        // changed.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLTransColourLookup_Change(object sender,
                                                    RoutedEventArgs e)
        {
            if (!_inhibitChecks)
            {
                _flagPCLTransColourLookup =
                    (Boolean)chkPCLTransColourLookup.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L T r a n s C o n f I O _ C h a n g e                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL Interpret 'Configuration (I/O)' check box has been         //
        // changed.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLTransConfIO_Change(object sender,
                                              RoutedEventArgs e)
        {
            if (!_inhibitChecks)
            {
                _flagPCLTransConfIO =
                    (Boolean)chkPCLTransConfIO.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L T r a n s C o n f I m a g e D a t a _ C h a n g e      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL Interpret 'Configure Image Data' check box has been        //
        // changed.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLTransConfImageData_Change(object sender,
                                                     RoutedEventArgs e)
        {
            if (!_inhibitChecks)
            {
                _flagPCLTransConfImageData =
                    (Boolean)chkPCLTransConfImageData.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L T r a n s C o n f R a s t e r D a t a _ C h a n g e    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL Interpret 'Configure Raster Data' check box has been       //
        // changed.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLTransConfRasterData_Change(object sender,
                                                      RoutedEventArgs e)
        {
            if (!_inhibitChecks)
            {
                _flagPCLTransConfRasterData =
                    (Boolean)chkPCLTransConfRasterData.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L T r a n s D e f L o g P a g e _ C h a n g e            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL interpret 'Define Logical Page' check box has been         //
        // changed.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLTransDefLogPage_Change(object sender,
                                                  RoutedEventArgs e)
        {
            if (!_inhibitChecks)
            {
                _flagPCLTransDefLogPage =
                    (Boolean)chkPCLTransDefLogPage.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L T r a n s D e f S y m S e t _ C h a n g e              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL interpret 'Define Symbol Set' check box has been           //
        // changed.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLTransDefSymSet_Change(object sender,
                                                 RoutedEventArgs e)
        {
            if (!_inhibitChecks)
            {
                _flagPCLTransDefSymSet =
                    (Boolean)chkPCLTransDefSymSet.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L T r a n s D i t h e r M a t r i x _ C h a n g e        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL interpret 'Download Dither Matrix' check box has been      //
        // changed.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLTransDitherMatrix_Change(object sender,
                                                    RoutedEventArgs e)
        {
            if (!_inhibitChecks)
            {
                _flagPCLTransDitherMatrix =
                    (Boolean)chkPCLTransDitherMatrix.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L T r a n s D r i v e r C o n f _ C h a n g e            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL interpret 'Driver Configuration' check box has been        //
        // changed.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLTransDriverConf_Change (object sender,
                                                   RoutedEventArgs e)
        {
            if (!_inhibitChecks)
            {
                _flagPCLTransDriverConf =
                    (Boolean)chkPCLTransDriverConf.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L T r a n s E s c E n c T e x t _ C h a n g e            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL interpret 'Escapement Encapsulated Text' check box has     //
        // been changed.                                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLTransEscEncText_Change(object sender,
                                                  RoutedEventArgs e)
        {
            if (!_inhibitChecks)
            {
                _flagPCLTransEscEncText =
                    (Boolean)chkPCLTransEscEncText.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L T r a n s P a l e t t e C o n f _ C h a n g e          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL interpret 'Palette Configuration' check box has been       //
        // changed.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLTransPaletteConf_Change(object sender,
                                                   RoutedEventArgs e)
        {
            if (!_inhibitChecks)
            {
                _flagPCLTransPaletteConf =
                    (Boolean)chkPCLTransPaletteConf.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L T r a n s U s e r P a t t e r n _ C h a n g e          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL interpret 'User-defined Pattern' check box has been        //
        // changed.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLTransUserPattern_Change(object sender,
                                                   RoutedEventArgs e)
        {
            if (!_inhibitChecks)
            {
                _flagPCLTransUserPattern =
                    (Boolean)chkPCLTransUserPattern.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L T r a n s V i e w I l l u m i n a n t _ C h a n g e    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL interpret 'Viewing Illuminant' check box has been          //
        // changed.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLTransViewIlluminant_Change(object sender,
                                                      RoutedEventArgs e)
        {
            if (!_inhibitChecks)
            {
                _flagPCLTransViewIlluminant =
                    (Boolean)chkPCLTransViewIlluminant.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L X L E n c P C L F o n t S e l e c t _ C h a n g e      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL XL 'Analyse PCL Font Select Data' check box has been       //
        // changed.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLXLEncPCLFontSelect_Change(object sender,
                                                     RoutedEventArgs e)
        {
            if (! _inhibitChecks)
            {
                _flagPCLXLEncPCLFontSelect =
                    (Boolean) chkPCLXLEncPCLFontSelect.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L X L E n c P C L P a s s T h r o u g h _ C h a n g e    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL XL 'Analyse PCL PassThrough Data' check box has been       //
        // changed.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLXLEncPCLPassThrough_Change(object sender,
                                                      RoutedEventArgs e)
        {
            if (! _inhibitChecks)
            {
                _flagPCLXLEncPCLPassThrough =
                    (Boolean) chkPCLXLEncPCLPassThrough.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L X L E n c U s e r S t r e a m _ C h a n g e            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL XL 'Analyse User-Defined Streams' check box has been       //
        // changed.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLXLEncUserStream_Change(object sender,
                                                  RoutedEventArgs e)
        {
            if (! _inhibitChecks)
            {
                _flagPCLXLEncUserStream = 
                    (Boolean) chkPCLXLEncUserStream.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L X L F o n t C h a r _ C h a n g e                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL XL 'Analyse Font Character' check box has been changed.    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLXLFontChar_Change(object sender,
                                             RoutedEventArgs e)
        {
            if (!_inhibitChecks)
            {
                _flagPCLXLFontChar = (Boolean) chkPCLXLFontChar.IsChecked;

                chkPCLXLFontDraw.IsEnabled = false;
                sldrPCLXLFontDrawHeight.IsEnabled = false;
                sldrPCLXLFontDrawWidth.IsEnabled = false;

                if (_flagPCLXLFontChar)
                {
                    chkPCLXLFontDraw.IsEnabled = true;

                    if (_flagPCLXLFontDraw)
                    {
                        sldrPCLXLFontDrawHeight.IsEnabled = true;
                        sldrPCLXLFontDrawWidth.IsEnabled = true;
                    }
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L X L F o n t D r a w _ C h a n g e                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL XL 'Draw Font Character' check box has been changed.       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLXLFontDraw_Change(object sender,
                                                 RoutedEventArgs e)
        {
            if (!_inhibitChecks)
            {
                _flagPCLXLFontDraw = (Boolean) chkPCLXLFontDraw.IsChecked;

                if (_flagPCLXLFontDraw)
                {
                    sldrPCLXLFontDrawHeight.IsEnabled = true;
                    sldrPCLXLFontDrawWidth.IsEnabled = true;
                }
                else
                {
                    sldrPCLXLFontDrawHeight.IsEnabled = false;
                    sldrPCLXLFontDrawWidth.IsEnabled = false;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L X L F o n t H d d r _ C h a n g e                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL XL 'Analyse Font Header' check box has been changed.       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLXLFontHddr_Change(object sender,
                                             RoutedEventArgs e)
        {
            if (! _inhibitChecks)
            {
                _flagPCLXLFontHddr =
                    (Boolean) chkPCLXLFontHddr.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L X L M i s c B i n D a t a _ C h a n g e                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL XL 'Show Embedded Data' check box has been changed.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLXLMiscBinData_Change(object sender,
                                                RoutedEventArgs e)
        {
            if (! _inhibitChecks)
            {
                _flagPCLXLMiscBinData = (Boolean) chkPCLXLMiscBinData.IsChecked;
            }
        }
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L M i s c O p e r P o s _ C h a n g e                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL XL 'Show Operator Positions' check box has been changed.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLXLMiscOperPos_Change(object sender,
                                                RoutedEventArgs e)
        {
            if (! _inhibitChecks)
            {
                _flagPCLXLMiscOperPos = (Boolean) chkPCLXLMiscOperPos.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P C L M i s c V e r b o s e _ C h a n g e                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PCL XL 'Verbose' check box has been changed.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPCLXLMiscVerbose_Change(object sender,
                                                RoutedEventArgs e)
        {
            if (! _inhibitChecks)
            {
                _flagPCLXLMiscVerbose = (Boolean) chkPCLXLMiscVerbose.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P M L M i s c V e r b o s e _ C h a n g e                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PML 'Verbose' check box has been changed.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPMLMiscVerbose_Change(object sender,
                                              RoutedEventArgs e)
        {
            if (! _inhibitChecks)
            {
                _flagPMLMiscVerbose = (Boolean) chkPMLMiscVerbose.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P M L W i t h i n P C L _ C h a n g e                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PML 'Show embedded PML in PCL' check box has been changed.     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPMLWithinPCL_Change(object sender,
                                            RoutedEventArgs e)
        {
            if (! _inhibitChecks)
            {
                _flagPMLWithinPCL = (Boolean) chkPMLWithinPCL.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k P M L W i t h i n P J L _ C h a n g e                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The PML 'Show embedded PML in PJL' check box has been changed.     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkPMLWithinPJL_Change(object sender,
                                            RoutedEventArgs e)
        {
            if (! _inhibitChecks)
            {
                _flagPMLWithinPJL = (Boolean) chkPMLWithinPJL.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k S t a t s E x c U n u s e d P C L O b s _ C h a n g e        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The Statistics 'Exclude unreferenced Obsolete PCL sequences' check //
        // box has been changed.                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkStatsExcUnusedPCLObs_Change (object sender,
                                                     RoutedEventArgs e)
        {
            if (!_inhibitChecks)
            {
                _flagStatsExcUnusedPCLObs =
                    (Boolean) chkStatsExcUnusedPCLObs.IsChecked;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k S t a t s E x c U n u s e d P C L X L R e s _ C h a n g e    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The Statistics 'Exclude unreferenced Reserved PCL XL tags' check   //
        // box has been changed.                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkStatsExcUnusedPCLXLRes_Change (object sender,
                                                       RoutedEventArgs e)
        {
            if (!_inhibitChecks)
            {
                _flagStatsExcUnusedPCLXLRes =
                    (Boolean) chkStatsExcUnusedPCLXLRes.IsChecked;
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

        private void initialise(PrnParseOptions options,
                                Int64           fileSize)
        {
            _initialised = false;

            _options = options;
            
            _fileSize = fileSize;

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

            this.Height = 800 * windowScale;
            this.Width  = 450 * windowScale;

            //----------------------------------------------------------------//
            //                                                                //
            // Populate form.                                                 //
            //                                                                //
            //----------------------------------------------------------------//

            prepareTabCharSet (options);
            prepareTabGen (options);
            prepareTabClrMap (options);
            prepareTabHPGL2 (options);
            prepareTabPCL (options);
            prepareTabPCLXL (options);
            prepareTabPML (options);
            prepareTabStats (options);
            
            prepareTabCurF (options, fileSize);

            //----------------------------------------------------------------//
            //                                                                //
            // Select active tab.                                             //
            //                                                                //
            //----------------------------------------------------------------//

            if (_fileOpen)
            {
                tabCurF.Visibility = Visibility.Visible;
                _crntTab = tabCurF;
                tabOptType.SelectedItem = tabCurF;
            }
            else
            {
                tabCurF.Visibility = Visibility.Hidden;
                _crntTab = tabGeneral;
                tabOptType.SelectedItem = tabGeneral;
            }

            _initialised = true;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n i t i a l i s e C l r M a p P i c k e r G r i d                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initialise colour map 'colour picker' grid.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void initialiseClrMapPickerGrid (object sender,
                                                 RoutedEventArgs e)
        {
            const Int32 reqCols = 10;

            Grid grid = sender as Grid;

            Int32 ctItems,
                  ctRows,
                  ctCols;

            ctItems = grid.Children.Count;

            ctRows = ctItems / reqCols;
            if (ctItems - (ctRows * ctRows) != 0)
                ctRows++;

            if (ctItems < reqCols)
                ctCols = ctItems;
            else
                ctCols = reqCols;


            if (grid != null)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Add rows.                                                  //
                //                                                            //
                //------------------------------------------------------------//

                if (grid.RowDefinitions.Count == 0)
                {
                    for (int r = 0; r < ctRows; r++)
                    {
                        grid.RowDefinitions.Add (new RowDefinition ());
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Add columns.                                               //
                //                                                            //
                //------------------------------------------------------------//

                if (grid.ColumnDefinitions.Count == 0)
                {
                    for (int c = 0; c < ctCols; c++)
                    {
                        grid.ColumnDefinitions.Add (new ColumnDefinition ());
                    }
                }

                //------------------------------------------------------------//
                //                                                            //
                // Populate cells.                                            //
                //                                                            //
                //------------------------------------------------------------//

                for (int i = 0; i < grid.Children.Count; i++)
                {
                    Grid.SetColumn (grid.Children[i], i % reqCols);
                    Grid.SetRow (grid.Children[i], i / reqCols);
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r e p a r e T a b C h a r S e t                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Sets fields on the Character Set tab.                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void prepareTabCharSet(PrnParseOptions options)
        {
            Char subChar;
            
            //----------------------------------------------------------------//
            //                                                                //
            // Get current values.                                            //
            //                                                                //
            //----------------------------------------------------------------//

            options.getOptCharSet (ref _indxCharSetName,
                                   ref _indxCharSetSubAct,
                                   ref _valCharSetSubCode);

            //----------------------------------------------------------------//
            //                                                                //
            // Set dialogue display values to current values.                 //
            //                                                                //
            //----------------------------------------------------------------//

            if (_indxCharSetName ==
                PrnParseConstants.eOptCharSets.ASCII)
                rbCharSetNameASCII.IsChecked = true;
            else if (_indxCharSetName ==
                PrnParseConstants.eOptCharSets.ISO_8859_1)
                rbCharSetNameISO88591.IsChecked = true;
            else
                rbCharSetNameWinANSI.IsChecked = true;

            //----------------------------------------------------------------//
            
            sldrCharSetSubCode.IsEnabled = false;
            
            if (_indxCharSetSubAct ==
                PrnParseConstants.eOptCharSetSubActs.Mnemonics)
                rbCharSetSubActMnemonics.IsChecked  = true;
            else if (_indxCharSetSubAct ==
                PrnParseConstants.eOptCharSetSubActs.MnemonicsIncSpace)
                rbCharSetSubActMnemonicsAndSpaces.IsChecked = true;
            else if (_indxCharSetSubAct ==
                PrnParseConstants.eOptCharSetSubActs.Hex)
                rbCharSetSubActHex.IsChecked = true;
            else if (_indxCharSetSubAct ==
                PrnParseConstants.eOptCharSetSubActs.Dots)
                rbCharSetSubActDots.IsChecked = true;
            else if (_indxCharSetSubAct ==
                PrnParseConstants.eOptCharSetSubActs.Spaces)
                rbCharSetSubActSpaces.IsChecked = true;
            else
            {
                rbCharSetSubActSubstitute.IsChecked = true;
                sldrCharSetSubCode.IsEnabled = true;
            }

            //----------------------------------------------------------------//

            subChar = (Char) _valCharSetSubCode;

            sldrCharSetSubCode.Value = _valCharSetSubCode;

            txtCharSetSubCode.Text = _valCharSetSubCode.ToString ();
            txtCharSetSubChar.Text = subChar.ToString();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r e p a r e T a b C l r M a p                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Sets fields on the Colour Coding tab.                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void prepareTabClrMap (PrnParseOptions options)
        {
            const Int32 indxRowTypeDefault = 0;
            const Int32 indxThemeDefault   = 0;

            PropertyInfo pInfoBack,
                         pInfoFore;

            Int32 indxClrBack,
                  indxClrFore;

            //----------------------------------------------------------------//
            //                                                                //
            // Populate the row type selection combo box.                     //
            //                                                                //
            //----------------------------------------------------------------//

            cbClrMapRowType.Items.Clear ();

            _ctClrMapRowTypes = PrnParseRowTypes.getCount ();

            for (Int32 i = 0; i < _ctClrMapRowTypes; i++)
            {
                cbClrMapRowType.Items.Add (PrnParseRowTypes.getDesc (i));
            }

            cbClrMapRowType.SelectedIndex = indxRowTypeDefault;

            //----------------------------------------------------------------//
            //                                                                //
            // Get current sample values.                                     //
            //                                                                //
            //----------------------------------------------------------------//

            _indxClrMapBack = new Int32[_ctClrMapRowTypes];
            _indxClrMapFore = new Int32[_ctClrMapRowTypes];

            options.getOptClrMap (ref _flagClrMapUseClr,
                                  ref _indxClrMapBack,
                                  ref _indxClrMapFore);

            options.getOptClrMapStdClrs (ref _ctClrMapStdClrs,
                                         ref _stdClrsPropertyInfo);

            //----------------------------------------------------------------//
            //                                                                //
            // Populate the colour picker selection combo boxes.              //
            // Set the current items according to which row type is selected. //
            //                                                                //
            //----------------------------------------------------------------//

            cbClrMapBack.ItemsSource = _stdClrsPropertyInfo;
            cbClrMapFore.ItemsSource = _stdClrsPropertyInfo;

            indxClrBack = _indxClrMapBack[indxRowTypeDefault];
            indxClrFore = _indxClrMapFore[indxRowTypeDefault];

            cbClrMapBack.SelectedIndex = indxClrBack;
            cbClrMapFore.SelectedIndex = indxClrFore;

            pInfoBack = cbClrMapBack.SelectedItem as PropertyInfo;
            pInfoFore = cbClrMapFore.SelectedItem as PropertyInfo;

            cbClrMapBack.ToolTip = pInfoBack.Name;
            cbClrMapFore.ToolTip = pInfoFore.Name;

            //----------------------------------------------------------------//
            //                                                                //
            // Populate the  sample grid.                                     //
            //                                                                //
            //----------------------------------------------------------------//

            Int32 ctSampleCols = 2;
            Int32 ctSampleRows = (_ctClrMapRowTypes / ctSampleCols);

            if ((_ctClrMapRowTypes % ctSampleCols) != 0)
                ctSampleRows++;

            Grid grid1 = new Grid ();

            for (Int32 i = 0; i < ctSampleCols; i++)
            {
                grid1.ColumnDefinitions.Add (new ColumnDefinition ());
            }

            for (Int32 i = 0; i < ctSampleRows; i++)
            {
                RowDefinition rowDefX = new RowDefinition ();
                rowDefX.MinHeight = 25;
                grid1.RowDefinitions.Add (rowDefX);
            }

            Thickness thk1 = new Thickness ();
            thk1.Bottom = 2;
            thk1.Top = 2;
            thk1.Left = 2;
            thk1.Right = 2;

            _txtClrMapSamples = new TextBox[_ctClrMapRowTypes];

            for (Int32 i = 0; i < _ctClrMapRowTypes; i++)
            {
                Color clrBack = new Color (),
                      clrFore = new Color ();

                SolidColorBrush brushBack = new SolidColorBrush (),
                                brushFore = new SolidColorBrush ();

                Int32 crntRow = i / ctSampleCols;
                Int32 crntCol = i % ctSampleCols;

                PrnParseRowTypes.eType rowType =
                    (PrnParseRowTypes.eType)i;

                _txtClrMapSamples[i] = new TextBox ();
                _txtClrMapSamples[i].Margin = thk1;
                _txtClrMapSamples[i].Text = PrnParseRowTypes.getDesc (i);

                indxClrBack = _indxClrMapBack[i];
                indxClrFore = _indxClrMapFore[i];

                pInfoBack = _stdClrsPropertyInfo[indxClrBack] as PropertyInfo;
                pInfoFore = _stdClrsPropertyInfo[indxClrFore] as PropertyInfo;

                clrBack = (Color) pInfoBack.GetValue (null, null);
                clrFore = (Color) pInfoFore.GetValue (null, null);

                brushBack.Color = clrBack;
                brushFore.Color = clrFore;

                _txtClrMapSamples[i].Background = brushBack;
                _txtClrMapSamples[i].Foreground = brushFore;

                _txtClrMapSamples[i].ToolTip = pInfoBack.Name + " / " +
                                               pInfoFore.Name;

                Grid.SetRow (_txtClrMapSamples[i], crntRow);
                Grid.SetColumn (_txtClrMapSamples[i], crntCol);

                grid1.Children.Add (_txtClrMapSamples[i]);
            }

            grpClrMapSamples.Content = grid1;

            //----------------------------------------------------------------//
            //                                                                //
            // Populate the user theme combo box.                             //
            //                                                                //
            //----------------------------------------------------------------//

            _clrMapThemeNames = new String[_ctClrMapThemes];

            cbClrMapTheme.Items.Clear ();

            for (Int32 i = 0; i < _ctClrMapThemes; i++)
            {
                String name = "";

                ToolPrnAnalysePersist.loadOptClrMapThemeName (i, ref name);

                _clrMapThemeNames[i] = name;

                cbClrMapTheme.Items.Add (i + ": " + name);
            }

            _crntClrMapTheme = indxThemeDefault;

            cbClrMapTheme.SelectedIndex = -1;

            txtClrMapThemeName.Text = "";

            //------------------------------------------------------------//
            //                                                            //
            // Reset 'save/restore' theme items.                          //
            //                                                            //
            //------------------------------------------------------------//

            cbClrMapTheme.SelectedIndex = -1;

            txtClrMapThemeName.Text = "";

            btnClrMapThemeRestore.IsEnabled = false;
            btnClrMapThemeSave.IsEnabled = false;
            txtClrMapThemeName.IsEnabled = false;

            //----------------------------------------------------------------//
            //                                                                //
            // Set visibility of colour selection items.                      //
            //                                                                //
            //----------------------------------------------------------------//

            if (_flagClrMapUseClr)
            {
                chkClrMapUseClr.IsChecked = true;

                grpClrMapSelect.Visibility = Visibility.Visible;
                grpClrMapSamples.Visibility = Visibility.Visible;
                grpClrMapThemes.Visibility = Visibility.Visible;
            }
            else
            {
                chkClrMapUseClr.IsChecked = false;

                grpClrMapSelect.Visibility = Visibility.Hidden;
                grpClrMapSamples.Visibility = Visibility.Hidden;
                grpClrMapThemes.Visibility = Visibility.Hidden;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r e p a r e T a b C u r F                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Sets fields on the Current File tab.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void prepareTabCurF (PrnParseOptions options,
                                     Int64           fileSize)
        {
            //----------------------------------------------------------------//
            //                                                                //
            // Get current values.                                            //
            //                                                                //
            //----------------------------------------------------------------//

            options.getOptCurF (ref _indxCurFInitLang,
                                ref _indxCurFXLBinding,
                                ref _indxCurFOffsetFormat,
                                ref _valCurFOffsetStart,
                                ref _valCurFOffsetEnd,
                                ref _valCurFOffsetMax);

            //----------------------------------------------------------------//
            //                                                                //
            // Check to see if 'Current File' tab should be visible or not    //
            // If it should, set dialogue display values to current values.   //
            //                                                                //
            //----------------------------------------------------------------//
            
            _fileOpen = false;

            if (fileSize != -1)
            {
                //------------------------------------------------------------//
                //                                                            //
                // Set dialogue display values to current values.             //
                //                                                            //
                //------------------------------------------------------------//
                
                String fmt;

                _fileOpen = true;

                _valCurFOffsetMax = (Int32) fileSize;

                if (_indxCurFInitLang ==
                    ToolCommonData.ePrintLang.PCL)
                    rbCurFInitLangPCL.IsChecked = true;
                else if (_indxCurFInitLang ==
                    ToolCommonData.ePrintLang.PCLXL)
                    rbCurFInitLangPCLXL.IsChecked = true;
                else if (_indxCurFInitLang ==
                    ToolCommonData.ePrintLang.HPGL2)
                    rbCurFInitLangHPGL2.IsChecked = true;
                else if (_indxCurFInitLang ==
                    ToolCommonData.ePrintLang.PJL)
                    rbCurFInitLangPJL.IsChecked = true;
                else
                    rbCurFInitLangPostScript.IsChecked = true;

                //------------------------------------------------------------//

                if (_indxCurFInitLang != ToolCommonData.ePrintLang.PCLXL)
                {
                    grpCurFXLBind.Visibility = Visibility.Hidden;
                }
                else
                {
                    grpCurFXLBind.Visibility = Visibility.Visible;

                    if (_indxCurFXLBinding ==
                        PrnParseConstants.ePCLXLBinding.Unknown)
                        rbCurFXLBindUnknown.IsChecked = true;
                    else if (_indxCurFXLBinding ==
                        PrnParseConstants.ePCLXLBinding.BinaryLSFirst)
                        rbCurFXLBindLS.IsChecked = true;
                    else
                        rbCurFXLBindMS.IsChecked = true;

                }

                //------------------------------------------------------------//

                if (_indxCurFOffsetFormat ==
                        PrnParseConstants.eOptOffsetFormats.Hexadecimal)
                {
                    rbCurFOffsetHex.IsChecked = true;

                    fmt = "X8";
                }
                else
                {
                    rbCurFOffsetDec.IsChecked = true;

                    fmt = "";
                }
                
                txtCurFOffsetStart.Text = _valCurFOffsetStart.ToString(fmt);
                txtCurFOffsetEnd.Text   = _valCurFOffsetEnd.ToString(fmt);
                txtCurFOffsetMax.Text   = _valCurFOffsetMax.ToString (fmt);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r e p a r e T a b G e n                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Sets fields on the General tab.                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void prepareTabGen(PrnParseOptions options)
        {
            //----------------------------------------------------------------//
            //                                                                //
            // Get current values.                                            //
            //                                                                //
            //----------------------------------------------------------------//

            options.getOptGeneral (ref _indxGenOffsetFormat,
                                   ref _flagGenMiscAutoAnalyse,
                                   ref _flagGenDiagFileAccess);

            //----------------------------------------------------------------//
            //                                                                //
            // Set dialogue display values to current values.                 //
            //                                                                //
            //----------------------------------------------------------------//

            if (_indxGenOffsetFormat ==
                PrnParseConstants.eOptOffsetFormats.Hexadecimal)
                rbGenOffsetHex.IsChecked = true;
            else
                rbGenOffsetDec.IsChecked = true; 
 
            //----------------------------------------------------------------//

            chkGenMiscAutoAnalyse.IsChecked = _flagGenMiscAutoAnalyse;
 
            //----------------------------------------------------------------//

            chkGenDiagFileAccess.IsChecked = _flagGenDiagFileAccess;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r e p a r e T a b H P G L 2                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Sets fields on the HP-GL/2 tab.                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void prepareTabHPGL2(PrnParseOptions options)
        {
            //----------------------------------------------------------------//
            //                                                                //
            // Get current values.                                            //
            //                                                                //
            //----------------------------------------------------------------//

            options.getOptHPGL2 (ref _flagHPGL2MiscBinData);

            //----------------------------------------------------------------//
            //                                                                //
            // Set dialogue display values to current values.                 //
            //                                                                //
            //----------------------------------------------------------------//

            chkHPGL2MiscBinData.IsChecked    = _flagHPGL2MiscBinData;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r e p a r e T a b P C L                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Sets fields on the PCL tab.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void prepareTabPCL(PrnParseOptions options)
        {
            //----------------------------------------------------------------//
            //                                                                //
            // Get current values.                                            //
            //                                                                //
            //----------------------------------------------------------------//

            options.getOptPCL (ref _flagPCLFontHddr,
                               ref _flagPCLFontChar,
                               ref _flagPCLFontDraw,
                               ref _valPCLFontDrawHeight,
                               ref _valPCLFontDrawWidth,
                               ref _flagPCLMacroDisplay,
                               ref _flagPCLMiscStyleData,
                               ref _flagPCLMiscBinData,
                               ref _flagPCLTransAlphaNumId,
                               ref _flagPCLTransColourLookup,
                               ref _flagPCLTransConfIO,
                               ref _flagPCLTransConfImageData,
                               ref _flagPCLTransConfRasterData,
                               ref _flagPCLTransDefLogPage,
                               ref _flagPCLTransDefSymSet,
                               ref _flagPCLTransDitherMatrix,
                               ref _flagPCLTransDriverConf,
                               ref _flagPCLTransEscEncText,
                               ref _flagPCLTransPaletteConf,
                               ref _flagPCLTransUserPattern,
                               ref _flagPCLTransViewIlluminant);

            //----------------------------------------------------------------//
            //                                                                //
            // Set dialogue display values to current values.                 //
            //                                                                //
            //----------------------------------------------------------------//

            sldrPCLFontDrawHeight.IsEnabled = false;

            chkPCLFontHddr.IsChecked         = _flagPCLFontHddr;
            chkPCLFontChar.IsChecked         = _flagPCLFontChar;

            chkPCLFontDraw.IsChecked         = _flagPCLFontDraw;

            sldrPCLFontDrawHeight.Value      = _valPCLFontDrawHeight;
            sldrPCLFontDrawWidth.Value       = _valPCLFontDrawWidth;

            txtPCLFontDrawHeight.Text =
                _valPCLFontDrawHeight.ToString ();
            txtPCLFontDrawWidth.Text  =
                _valPCLFontDrawWidth.ToString ();

            chkPCLFontDraw.IsEnabled = false;
            sldrPCLFontDrawHeight.IsEnabled = false;
            sldrPCLFontDrawWidth.IsEnabled = false;

            if (_flagPCLFontChar)
            {
                chkPCLFontDraw.IsEnabled = true;

                if (_flagPCLFontDraw)
                {
                    sldrPCLFontDrawHeight.IsEnabled = true;
                    sldrPCLFontDrawWidth.IsEnabled = true;
                }
            }

            chkPCLMacroDisplay.IsChecked        = _flagPCLMacroDisplay;

            chkPCLMiscStyleData.IsChecked       = _flagPCLMiscStyleData;
            chkPCLMiscBinData.IsChecked         = _flagPCLMiscBinData;
            
            chkPCLTransAlphaNumId.IsChecked     = _flagPCLTransAlphaNumId;
            chkPCLTransColourLookup.IsChecked   = _flagPCLTransColourLookup;
            chkPCLTransConfIO.IsChecked         = _flagPCLTransConfIO;
            chkPCLTransConfImageData.IsChecked  = _flagPCLTransConfImageData;
            chkPCLTransConfRasterData.IsChecked = _flagPCLTransConfRasterData;
            chkPCLTransDefLogPage.IsChecked     = _flagPCLTransDefLogPage;
            chkPCLTransDefSymSet.IsChecked      = _flagPCLTransDefSymSet;
            chkPCLTransDitherMatrix.IsChecked   = _flagPCLTransDitherMatrix;
            chkPCLTransDriverConf.IsChecked     = _flagPCLTransDriverConf;
            chkPCLTransEscEncText.IsChecked     = _flagPCLTransEscEncText;
            chkPCLTransPaletteConf.IsChecked    = _flagPCLTransPaletteConf;
            chkPCLTransUserPattern.IsChecked    = _flagPCLTransUserPattern;
            chkPCLTransViewIlluminant.IsChecked = _flagPCLTransViewIlluminant;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r e p a r e T a b P C L X L                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Sets fields on the PCL XL tab.                                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void prepareTabPCLXL(PrnParseOptions options)
        {
            //----------------------------------------------------------------//
            //                                                                //
            // Get current values.                                            //
            //                                                                //
            //----------------------------------------------------------------//

            options.getOptPCLXL (ref _flagPCLXLFontHddr,
                                 ref _flagPCLXLFontChar,
                                 ref _flagPCLXLFontDraw,
                                 ref _valPCLXLFontDrawHeight,
                                 ref _valPCLXLFontDrawWidth,
                                 ref _flagPCLXLEncUserStream,
                                 ref _flagPCLXLEncPCLPassThrough,
                                 ref _flagPCLXLEncPCLFontSelect,
                                 ref _flagPCLXLMiscOperPos,
                                 ref _flagPCLXLMiscBinData,
                                 ref _flagPCLXLMiscVerbose);

            //----------------------------------------------------------------//
            //                                                                //
            // Set dialogue display values to current values.                 //
            //                                                                //
            //----------------------------------------------------------------//

            chkPCLXLFontHddr.IsChecked          = _flagPCLXLFontHddr;
            chkPCLXLFontChar.IsChecked          = _flagPCLXLFontChar;

            chkPCLXLFontDraw.IsChecked          = _flagPCLXLFontDraw;

            sldrPCLXLFontDrawHeight.Value       = _valPCLXLFontDrawHeight;
            sldrPCLXLFontDrawWidth.Value        = _valPCLXLFontDrawWidth;

            txtPCLXLFontDrawHeight.Text =
                _valPCLXLFontDrawHeight.ToString ();
            txtPCLXLFontDrawWidth.Text =
                _valPCLXLFontDrawWidth.ToString ();

            chkPCLXLFontDraw.IsEnabled = false;
            sldrPCLXLFontDrawHeight.IsEnabled = false;
            sldrPCLXLFontDrawWidth.IsEnabled = false;

            if (_flagPCLXLFontChar)
            {
                chkPCLXLFontDraw.IsEnabled = true;

                if (_flagPCLXLFontDraw)
                {
                    sldrPCLXLFontDrawHeight.IsEnabled = true;
                    sldrPCLXLFontDrawWidth.IsEnabled = true;
                }
            }

            chkPCLXLEncUserStream.IsChecked     = _flagPCLXLEncUserStream;
            chkPCLXLEncPCLPassThrough.IsChecked = _flagPCLXLEncPCLPassThrough;
            chkPCLXLEncPCLFontSelect.IsChecked  = _flagPCLXLEncPCLFontSelect;
            
            chkPCLXLMiscOperPos.IsChecked       = _flagPCLXLMiscOperPos;
            chkPCLXLMiscBinData.IsChecked       = _flagPCLXLMiscBinData;
            chkPCLXLMiscVerbose.IsChecked       = _flagPCLXLMiscVerbose;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r e p a r e T a b P M L                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Sets fields on the PML tab.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void prepareTabPML(PrnParseOptions options)
        {
            //----------------------------------------------------------------//
            //                                                                //
            // Get current values.                                            //
            //                                                                //
            //----------------------------------------------------------------//

            options.getOptPML (ref _flagPMLWithinPCL,
                               ref _flagPMLWithinPJL,
                               ref _flagPMLMiscVerbose);

            //----------------------------------------------------------------//
            //                                                                //
            // Set dialogue display values to current values.                 //
            //                                                                //
            //----------------------------------------------------------------//

            chkPMLWithinPCL.IsChecked    = _flagPMLWithinPCL;
            chkPMLWithinPJL.IsChecked    = _flagPMLWithinPJL;
            chkPMLMiscVerbose.IsChecked  = _flagPMLMiscVerbose;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p r e p a r e T a b S t a t s                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Sets fields on the Statistics tab.                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void prepareTabStats(PrnParseOptions options)
        {
            //----------------------------------------------------------------//
            //                                                                //
            // Get current values.                                            //
            //                                                                //
            //----------------------------------------------------------------//

            options.getOptStats (ref _indxStatsLevel,
                                 ref _flagStatsExcUnusedPCLObs,
                                 ref _flagStatsExcUnusedPCLXLRes);

            //----------------------------------------------------------------//
            //                                                                //
            // Set dialogue display values to current values.                 //
            //                                                                //
            //----------------------------------------------------------------//

            if (_indxStatsLevel ==
                PrnParseConstants.eOptStatsLevel.ReferencedOnly)
            {
                rbStatsLevUsedOnly.IsChecked = true;

                chkStatsExcUnusedPCLObs.IsEnabled = false;
                chkStatsExcUnusedPCLXLRes.IsEnabled = false;
            }
            else 
            {
                rbStatsLevAll.IsChecked = true;

                chkStatsExcUnusedPCLObs.IsEnabled = true;
                chkStatsExcUnusedPCLXLRes.IsEnabled = true;
            }

            if (_flagStatsExcUnusedPCLObs)
                chkStatsExcUnusedPCLObs.IsChecked = true;
            else
                chkStatsExcUnusedPCLObs.IsChecked = false;

            if (_flagStatsExcUnusedPCLXLRes)
                chkStatsExcUnusedPCLXLRes.IsChecked = true;
            else
                chkStatsExcUnusedPCLXLRes.IsChecked = false;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b C h a r s e t N a m e _ C h e c k e d                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Character Set tab:                                                 //
        // One of the 'Character Set' radio buttons has been clicked.         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbCharsetName_Checked(object sender, RoutedEventArgs e)
        {
            if (! _inhibitChecks)
            {
                if (rbCharSetNameASCII.IsChecked == true)
                    _indxCharSetName =
                        PrnParseConstants.eOptCharSets.ASCII;
                else if (rbCharSetNameISO88591.IsChecked == true)
                    _indxCharSetName =
                        PrnParseConstants.eOptCharSets.ISO_8859_1;
                else
                    _indxCharSetName =
                        PrnParseConstants.eOptCharSets.Win_ANSI;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b C h a r s e t S u b A c t _ C h e c k e d                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Character Set tab:                                                 //
        // One of the 'Non-graphic characters' radio buttons has been clicked.//
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbCharsetSubAct_Checked(object sender,
                                             RoutedEventArgs e)
        {
            if (! _inhibitChecks)
            {
                txtCharSetSubCode.IsEnabled = false;

                if (rbCharSetSubActMnemonics.IsChecked == true)
                    _indxCharSetSubAct =
                        PrnParseConstants.eOptCharSetSubActs.Mnemonics;
                else if (rbCharSetSubActMnemonicsAndSpaces.IsChecked == true)
                    _indxCharSetSubAct =
                        PrnParseConstants.eOptCharSetSubActs.MnemonicsIncSpace;
                else if (rbCharSetSubActHex.IsChecked == true)
                    _indxCharSetSubAct =
                        PrnParseConstants.eOptCharSetSubActs.Hex;
                else if (rbCharSetSubActDots.IsChecked == true)
                    _indxCharSetSubAct =
                        PrnParseConstants.eOptCharSetSubActs.Dots;
                else if (rbCharSetSubActSpaces.IsChecked == true)
                    _indxCharSetSubAct =
                        PrnParseConstants.eOptCharSetSubActs.Spaces;
                else
                {
                    _indxCharSetSubAct =
                        PrnParseConstants.eOptCharSetSubActs.Substitute;

                    txtCharSetSubCode.IsEnabled = true;
                }
            }
        }
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b C u r F I n i t L a n g _ C h e c k e d                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Current File tab:                                                  //
        // One of the 'Initial language' radio buttons has been clicked.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbCurFInitLang_Checked(object sender,
                                            RoutedEventArgs e)
        {
            if (! _inhibitChecks)
            {
                grpCurFXLBind.Visibility = Visibility.Hidden;

                if (rbCurFInitLangPCL.IsChecked == true)
                    _indxCurFInitLang =
                        ToolCommonData.ePrintLang.PCL;
                else if (rbCurFInitLangPCLXL.IsChecked == true)
                {
                    _indxCurFInitLang =
                        ToolCommonData.ePrintLang.PCLXL;

                    grpCurFXLBind.Visibility = Visibility.Visible;
                }
                else if (rbCurFInitLangHPGL2.IsChecked == true)
                    _indxCurFInitLang =
                        ToolCommonData.ePrintLang.HPGL2;
                else if (rbCurFInitLangPJL.IsChecked == true)
                    _indxCurFInitLang =
                        ToolCommonData.ePrintLang.PJL;
                else
                    _indxCurFInitLang =
                        ToolCommonData.ePrintLang.PostScript;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b C u r F O f f s e t F o r m a t _ C h e c k e d                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Current File tab:                                                  //
        // One of the 'Offset type Set' radio buttons has been clicked.       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbCurFOffsetFormat_Checked(object sender,
                                                RoutedEventArgs e)
        {
            if (! _inhibitChecks)
            {
                String fmt;

                if (rbCurFOffsetHex.IsChecked == true)
                {
                    _indxCurFOffsetFormat =
                        PrnParseConstants.eOptOffsetFormats.Hexadecimal;
                    fmt = "X8";
                }
                else
                {
                    _indxCurFOffsetFormat =
                        PrnParseConstants.eOptOffsetFormats.Decimal;
                    fmt = "";
                }

                txtCurFOffsetStart.Text = _valCurFOffsetStart.ToString (fmt);
                txtCurFOffsetEnd.Text = _valCurFOffsetEnd.ToString (fmt);
                txtCurFOffsetMax.Text = _valCurFOffsetMax.ToString (fmt);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b C u r F X L B i n d _ C h e c k e d                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Current File tab:                                                  //
        // One of the 'PCL XL stream header' radio buttons has been clicked.  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbCurFXLBind_Checked(object sender,
                                          RoutedEventArgs e)
        {
            if (!_inhibitChecks)
            {
                if (rbCurFXLBindUnknown.IsChecked == true)
                    _indxCurFXLBinding =
                        PrnParseConstants.ePCLXLBinding.Unknown;
                else if (rbCurFXLBindLS.IsChecked == true)
                    _indxCurFXLBinding =
                        PrnParseConstants.ePCLXLBinding.BinaryLSFirst;
                else
                    _indxCurFXLBinding =
                        PrnParseConstants.ePCLXLBinding.BinaryMSFirst;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b G e n O f f s e t _ C h e c k e d                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // General tab:                                                       //
        // One of the 'Offset type Set' radio buttons has been clicked.       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbGenOffsetType_Checked(object sender, RoutedEventArgs e)
        {
            if (!_inhibitChecks)
            {
                if (rbGenOffsetHex.IsChecked == true)
                {
                    _indxGenOffsetFormat =
                        PrnParseConstants.eOptOffsetFormats.Hexadecimal;
                    _indxCurFOffsetFormat =
                        PrnParseConstants.eOptOffsetFormats.Hexadecimal;
                    rbCurFOffsetHex.IsChecked = true;
                }
                else
                {
                    _indxGenOffsetFormat =
                       PrnParseConstants.eOptOffsetFormats.Decimal;
                    _indxCurFOffsetFormat =
                       PrnParseConstants.eOptOffsetFormats.Decimal;
                    rbCurFOffsetDec.IsChecked = true;
                }
            }
        }


        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b S t a t s L e v e l _ C h e c k e d                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Statistics tab:                                                    //
        // One of the 'Statistics Level' radio buttons has been clicked.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbStatsLevel_Checked(object sender, RoutedEventArgs e)
        {
            if (!_inhibitChecks)
            {
                if (rbStatsLevUsedOnly.IsChecked == true)
                {
                    _indxStatsLevel =
                        PrnParseConstants.eOptStatsLevel.ReferencedOnly;

                    chkStatsExcUnusedPCLObs.IsEnabled = false;
                    chkStatsExcUnusedPCLXLRes.IsEnabled = false;
                }
                else
                {
                    _indxStatsLevel =
                        PrnParseConstants.eOptStatsLevel.All;

                    chkStatsExcUnusedPCLObs.IsEnabled = true;
                    chkStatsExcUnusedPCLXLRes.IsEnabled = true;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s l d r C h a r S e t S u b C o d e _ V a l u e C h a n g e d      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Character Set tab:                                                 //
        // The Character Set 'substitute character code' slider has changed.  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void sldrCharSetSubCode_ValueChanged(
            object sender,
            RoutedPropertyChangedEventArgs<double> e)
        {
            if (!_inhibitChecks)
            {
                Char subChar;

                _valCharSetSubCode = (Int32) sldrCharSetSubCode.Value;

                subChar = (Char) _valCharSetSubCode;

                txtCharSetSubCode.Text = _valCharSetSubCode.ToString ();
                txtCharSetSubChar.Text = subChar.ToString ();
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s l d r P C L F o n t D r a w H e i g h t _ V a l u e C h a n g e d//
        //--------------------------------------------------------------------//
        //                                                                    //
        // PCL tab:                                                           //
        // The 'Font Draw Maximum Height' slider has changed.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void sldrPCLFontDrawHeight_ValueChanged(
            object sender,
            RoutedPropertyChangedEventArgs<double> e)
        {
            if (!_inhibitChecks)
            {
                _valPCLFontDrawHeight = (Int32) sldrPCLFontDrawHeight.Value;

                txtPCLFontDrawHeight.Text = _valPCLFontDrawHeight.ToString ();
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s l d r P C L F o n t D r a w W i d t h _ V a l u e C h a n g e d  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // PCL tab:                                                           //
        // The 'Font Draw Maximum Width' slider has changed.                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void sldrPCLFontDrawWidth_ValueChanged(
            object sender,
            RoutedPropertyChangedEventArgs<double> e)
        {
            if (!_inhibitChecks)
            {
                _valPCLFontDrawWidth = (Int32) sldrPCLFontDrawWidth.Value;

                txtPCLFontDrawWidth.Text = _valPCLFontDrawWidth.ToString ();
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s l d r P C L X L F o n t D r a w H e i g h t                      //
        // _ V a l u e C h a n g e d                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // PCL XL tab:                                                        //
        // The 'Font Draw Maximum Height' slider has changed.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void sldrPCLXLFontDrawHeight_ValueChanged(
            object sender,
            RoutedPropertyChangedEventArgs<double> e)
        {
            if (!_inhibitChecks)
            {
                _valPCLXLFontDrawHeight = (Int32) sldrPCLXLFontDrawHeight.Value;

                txtPCLXLFontDrawHeight.Text =
                    _valPCLXLFontDrawHeight.ToString ();
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s l d r P C L X L F o n t D r a w W i d t h                        //
        // _ V a l u e C h a n g e d                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // PCL XL tab:                                                        //
        // The 'Font Draw Maximum Width' slider has changed.                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void sldrPCLXLFontDrawWidth_ValueChanged(
            object sender,
            RoutedPropertyChangedEventArgs<double> e)
        {
            if (!_inhibitChecks)
            {
                _valPCLXLFontDrawWidth = (Int32) sldrPCLXLFontDrawWidth.Value;

                txtPCLXLFontDrawWidth.Text = _valPCLXLFontDrawWidth.ToString ();
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t a b O p t T y p e _ S e l e c t i o n C h a n g e d              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // The selected tab has changed.                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void tabOptType_SelectionChanged(object sender,
                                                 SelectionChangedEventArgs e)
        {
            Boolean OK = true;

            TabItem newTab = (TabItem) tabOptType.SelectedItem;

            if (!_inhibitChecks)
            {

                if (_crntTab == tabGeneral)
                {
                    OK = validateGeneral ();
                }
                else if (_crntTab == tabCharSet)
                {
                    OK = validateCharSet ();
                }

                if (OK)
                    _crntTab = newTab;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t C l r M a p T h e m e N a m e _ L o s t F o c u s            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Colour coding tab:                                                 //
        // The 'theme name' text box has lost focus.                          //
        //                                                                    //
        // Note that we only save (persist the (possibly) new value if the    //
        // 'save as user theme' button is used to save the theme.             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtClrMapThemeName_LostFocus (object sender,
                                                   RoutedEventArgs e)
        {
            _clrMapThemeNames[_crntClrMapTheme] = txtClrMapThemeName.Text;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t C u r F O f f s e t E n d _ L o s t F o c u s                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Current File tab:                                                  //
        // The 'End Offset' text box has lost focus; validate contents.       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtCurFOffsetEnd_LostFocus(object sender,
                                                RoutedEventArgs e)
        {
            validateCurFOffsetEnd ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t x t C u r F O f f s e t S t a r t _ L o s t F o c u s            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Current File tab:                                                  //
        // The 'Start Offset' text box has lost focus; validate contents.     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void txtCurFOffsetStart_LostFocus(object sender, 
                                                  RoutedEventArgs e)
        {
            validateCurFOffsetStart ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // v a l i d a t e C h a r S e t                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Validate 'Character Set' tab.                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean validateCharSet()
        {
            Boolean OK;

            OK = true;  // or validateCharSetSubCode ();
            
            return OK;
        }
        /*
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // v a l i d a t e C h a r S e t S u b C h a r C o d e                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Validate Character Set 'substitute character code' value.          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean validateCharSetSubCode()
        {
            const Byte minVal = PrnParseConstants.asciiSpace;
            const Byte maxVal = PrnParseConstants.asciiMax8bit;
            const Byte excVal = PrnParseConstants.asciiDEL;
        //  const Byte defVal = PrnParseConstants.asciiSubDefault;

            Byte value;

            Boolean OK = true;

            String crntText = txtCharSetSubCode.Text;

            OK = Byte.TryParse (crntText, out value);

            if (OK)
                if ((value < minVal) || (value > maxVal) ||
                    (value == excVal))
                    OK = false;

            if (OK)
            {
                _valCharSetSubCode = value;

                _inhibitChecks = false;
            }
            else
            {
                MessageBox.Show ("Value '" + crntText + "' is invalid.\n\n" +
                                 "Valid range is :\n\t" +
                                 minVal + " <= value <= " + maxVal + "\n" +
                                 "(excluding " + excVal + ")",
                                 "Character set substitute character code",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);

                _inhibitChecks = true;

                tabOptType.SelectedItem = _crntTab;

                txtCharSetSubCode.SelectAll ();
                txtCharSetSubCode.Focus ();
            }

            return OK;
        }
        */

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // v a l i d a t e C u r F O f f s e t E n d                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Validate Current File 'Offset End' value.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean validateCurFOffsetEnd()
        {
            Int32 value;

            Boolean OK = true;

            String crntText = txtCurFOffsetEnd.Text;

            OK = Int32.TryParse (crntText, out value);

            if ((OK) && (value != -1)) 
                if ((value > _fileSize)            ||
                    (value < _valCurFOffsetStart))
                    OK = false;

            if (OK)
            {
                _valCurFOffsetEnd = value;

                _inhibitChecks = false;
            }
            else
            {
                MessageBox.Show ("Value '" + crntText + "' is invalid.\n\n" +
                                 "Valid range is :\n\t" +
                                 0 + " <= value <= " + _fileSize + "\n" +
                                 "(and > Start value " + _valCurFOffsetStart + ")",
                                 "Offset End Point",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);

                _inhibitChecks = true;

                tabOptType.SelectedItem = _crntTab;

                txtCurFOffsetEnd.SelectAll ();
                txtCurFOffsetEnd.Focus ();
            }

            return OK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // v a l i d a t e C u r F O f f s e t S t a r t                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Validate Current File 'Offset Start' value.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean validateCurFOffsetStart()
        {
            Int32 value;

            Boolean OK = true;

            String crntText = txtCurFOffsetStart.Text;

            OK = Int32.TryParse (crntText, out value);

            if (OK)
                if ((value < 0) || (value > _fileSize))
                    OK = false;

            if (OK)
            {
                _valCurFOffsetStart = value;

                _inhibitChecks = false;
            }
            else
            {
                MessageBox.Show ("Value '" + crntText + "' is invalid.\n\n" +
                                 "Valid range is :\n\t" +
                                 0 + " <= value <= " + _fileSize,
                                 "Offset Start Point",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);

                _inhibitChecks = true;

                tabOptType.SelectedItem = _crntTab;

                txtCurFOffsetStart.SelectAll ();
                txtCurFOffsetStart.Focus ();
            }

            return OK;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // v a l i d a t e G e n e r a l                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Validate 'General' tab.                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Boolean validateGeneral()
        {
            Boolean OK;
            
            OK = true;

            return OK;
        }
    }
}
