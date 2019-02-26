using System;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// Interaction logic for ToolTrayMap.xaml
    /// 
    /// Class handles the TrayMap tool form.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>
    
    [System.Reflection.ObfuscationAttribute(Feature = "renaming",
                                            ApplyToMembers = true)]

    public partial class ToolTrayMap : Window
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const Int32 _maxSheetNo   = 6;
        const Int32 _maxPaperTray = 299;

        private enum eTrayOpt : byte
        {
            None,
            Auto,
            Selected,
            Max
        }

        private static Int32[] _subsetPDLs =
        {
            (Int32) ToolCommonData.ePrintLang.PCL,
            (Int32) ToolCommonData.ePrintLang.PCLXL,
        };

        private static Int32[] _subsetOrientations = 
        {
            (Int32) PCLOrientations.eIndex.Portrait,
            (Int32) PCLOrientations.eIndex.Landscape,
            (Int32) PCLOrientations.eIndex.ReversePortrait,
            (Int32) PCLOrientations.eIndex.ReverseLandscape
        };

        private static Int32[] _subsetPaperSizes =
        {
            (Int32) PCLPaperSizes.eIndex.ISO_A4,
            (Int32) PCLPaperSizes.eIndex.ISO_A3,
            (Int32) PCLPaperSizes.eIndex.ISO_A5,
            (Int32) PCLPaperSizes.eIndex.ISO_A6,
            (Int32) PCLPaperSizes.eIndex.ANSI_A_Letter,
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

        private static Int32[] _subsetPlexModes =
        {
            (Int32) PCLPlexModes.eIndex.Simplex,
            (Int32) PCLPlexModes.eIndex.DuplexLongEdge,
            (Int32) PCLPlexModes.eIndex.DuplexShortEdge
        };

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private ToolCommonData.ePrintLang _crntPDL;

        private Int32 _srcAutoSelectPCL;
        private Int32 _srcAutoSelectPCLXL;

        private Int32 _ctPDLs;
        private Int32 _ctOrientations;
        private Int32 _ctPaperSizes;
        private Int32 _ctPaperTypes;
        private Int32 _ctPlexModes;

        private Int32 _indxPDL;

        private Int32[] _indxOrientFrontPCL;
        private Int32[] _indxOrientFrontPCLXL;
        private Int32[] _indxOrientRearPCL;
        private Int32[] _indxOrientRearPCLXL;
        private Int32[] _indxPaperSizePCL;
        private Int32[] _indxPaperSizePCLXL;
        private Int32[] _indxPaperTypePCL;
        private Int32[] _indxPaperTypePCLXL;
        private Int32[] _indxPlexModePCL;
        private Int32[] _indxPlexModePCLXL;
        private Int32[] _indxPaperTrayPCL;
        private Int32[] _indxPaperTrayPCLXL;

   //   private Int32 _indxPaperTrayNotUsed;

        private Int32 _sheetCtPCL;
        private Int32 _sheetCtPCLXL;

        private Boolean _formAsMacroPCL;
        private Boolean _formAsMacroPCLXL;

        private Boolean _initialised;
        private Boolean _inhibitTrayIdChange;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // T o o l T r a y M a p                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public ToolTrayMap (ref ToolCommonData.ePrintLang crntPDL)
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
            //----------------------------------------------------------------//
            //                                                                //
            // Get current test metrics.                                      //
            // Note that the relevant (PDL-specific) stored option values     //
            // SHOULD already be up-to-date, since the fields all have        //
            // associated 'OnChange' actions. ***** Not with WPF ????? *****  //
            // But we'll save them all anyway, to make sure.                  //
            //                                                                //
            //----------------------------------------------------------------//

            _indxPDL = cbPDL.SelectedIndex;
            _crntPDL = (ToolCommonData.ePrintLang) _subsetPDLs [_indxPDL];

            pdlOptionsStore();

            //------------------------------------------------------------//
            //                                                            //
            // Generate test print file.                                  //
            //                                                            //
            //------------------------------------------------------------//

            try
            {
                BinaryWriter binWriter = null;

                TargetCore.requestStreamOpen(
                    ref binWriter,
                    ToolCommonData.eToolIds.TrayMap,
                    ToolCommonData.eToolSubIds.None,
                    _crntPDL);

                if (_crntPDL == ToolCommonData.ePrintLang.PCL)
                {
                    Int32[] indxPaperSize   = new Int32[_sheetCtPCL];
                    Int32[] indxPaperType   = new Int32[_sheetCtPCL];
                    Int32[] indxPaperTray   = new Int32[_sheetCtPCL];
                    Int32[] indxPlexMode    = new Int32[_sheetCtPCL];
                    Int32[] indxOrientFront = new Int32[_sheetCtPCL];
                    Int32[] indxOrientRear  = new Int32[_sheetCtPCL];

                    for (Int32 i = 0; i < _sheetCtPCL; i++)
                    {
                        indxPaperSize[i] =
                            _subsetPaperSizes[_indxPaperSizePCL[i]];
                        indxPaperType[i] =
                            _subsetPaperTypes[_indxPaperTypePCL[i]];
                        indxPaperTray[i] = _indxPaperTrayPCL[i] - 1;
                        indxPlexMode[i] =
                            _subsetPlexModes[_indxPlexModePCL[i]];
                        indxOrientFront[i] =
                            _subsetOrientations[_indxOrientFrontPCL[i]];
                        indxOrientRear[i] =
                            _subsetOrientations[_indxOrientRearPCL[i]];
                    }

                    ToolTrayMapPCL.generateJob(
                        binWriter,
                        _sheetCtPCL,
                        indxPaperSize,
                        indxPaperType,
                        indxPaperTray,
                        indxPlexMode,
                        indxOrientFront,
                        indxOrientRear,
                        _formAsMacroPCL);
                }
                else
                {
                    Int32[] indxPaperSize   = new Int32[_sheetCtPCLXL];
                    Int32[] indxPaperType   = new Int32[_sheetCtPCLXL];
                    Int32[] indxPaperTray   = new Int32[_sheetCtPCLXL];
                    Int32[] indxPlexMode    = new Int32[_sheetCtPCLXL];
                    Int32[] indxOrientFront = new Int32[_sheetCtPCLXL];
                    Int32[] indxOrientRear  = new Int32[_sheetCtPCLXL];

                    for (Int32 i = 0; i < _sheetCtPCLXL; i++)
                    {
                        indxPaperSize[i] =
                            _subsetPaperSizes[_indxPaperSizePCLXL[i]];
                        indxPaperType[i] =
                            _subsetPaperTypes[_indxPaperTypePCLXL[i]];
                        indxPaperTray[i] = _indxPaperTrayPCLXL[i] - 1;
                        indxPlexMode[i] =
                            _subsetPlexModes[_indxPlexModePCLXL[i]];
                        indxOrientFront[i] =
                            _subsetOrientations[_indxOrientFrontPCLXL[i]];
                        indxOrientRear[i] =
                            _subsetOrientations[_indxOrientRearPCLXL[i]];
                    }

                    ToolTrayMapPCLXL.generateJob(
                        binWriter,
                        _sheetCtPCLXL,
                        indxPaperSize,
                        indxPaperType,
                        indxPaperTray,
                        indxPlexMode,
                        indxOrientFront,
                        indxOrientRear,
                        _formAsMacroPCLXL);
                }

                TargetCore.requestStreamWrite(false);
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
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // b t n R e s e t S h e e t D a t a _ C l i c k                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Called when the 'reset all sheets the same' button is clicked.     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void btnResetSheetData_Click(object sender, EventArgs e)
        {
            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                for (Int32 i = 1; i < _maxSheetNo; i++)
                {
                    _indxPaperSizePCL[i]    = _indxPaperSizePCL[0];
                    _indxPaperTypePCL[i]    = _indxPaperTypePCL[0];
                    _indxPaperTrayPCL[i]    = _indxPaperTrayPCL[0];
                    _indxPlexModePCL[i]     = _indxPlexModePCL[0];
                    _indxOrientFrontPCL[i]  = _indxOrientFrontPCL[0];
                    _indxOrientRearPCL[i]   = _indxOrientRearPCL[0];
                }
            }
            else
            {
                for (Int32 i = 1; i < _maxSheetNo; i++)
                {
                    _indxPaperSizePCLXL[i]    = _indxPaperSizePCLXL[0];
                    _indxPaperTypePCLXL[i]    = _indxPaperTypePCLXL[0];
                    _indxPaperTrayPCLXL[i]    = _indxPaperTrayPCLXL[0];
                    _indxPlexModePCLXL[i]     = _indxPlexModePCLXL[0];
                    _indxOrientFrontPCLXL[i]  = _indxOrientFrontPCLXL[0];
                    _indxOrientRearPCLXL[i]   = _indxOrientRearPCLXL[0];
                }
            }

            sheetDataRestore();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b O r i e n t F r o n t _ S e l e c t i o n C h a n g e d        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Paper Orientation (front) item has changed.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbOrientFront_SelectionChanged (
            object sender,
            SelectionChangedEventArgs e)
        {
            Boolean flagOK = false;

            UInt16 sheetIndx;

            ComboBox source = e.Source as ComboBox;

            String cbName;

            //----------------------------------------------------------------//
            //                                                                //
            // Work out which combo box has just changed.                     //
            // This is done by examining the combo box name.                  //
            // The names should be in the format 'cbnn_xyz', where 'nn' is    //
            // a decimal value.                                               //
            //                                                                //
            //----------------------------------------------------------------//

            cbName = source.Name; // should be in format cbnn_xyz

            flagOK = UInt16.TryParse(cbName.Substring(2, 2),
                                      NumberStyles.HexNumber,
                                      CultureInfo.InvariantCulture,
                                      out sheetIndx);

            if (flagOK)
            {
                if (sheetIndx > _maxSheetNo)
                    flagOK = false;
            }

            if (!flagOK)
            {
                MessageBox.Show("Unable to detemine which Orientation (F)" +
                                " item has just been changed!",
                                "***** Internal error *****",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
            else
            {
                Int32 srcIndex = source.SelectedIndex;
                Int32 arrayIndex = sheetIndx - 1;

                if (_crntPDL == ToolCommonData.ePrintLang.PCL)
                    _indxOrientFrontPCL[arrayIndex] = srcIndex;
                else
                    _indxOrientFrontPCLXL[arrayIndex] = srcIndex;
            }
        }
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b O r i e n t R e a r _ S e l e c t i o n C h a n g e d          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Paper Orientation (rear) item has changed.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbOrientRear_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
        {
            Boolean flagOK = false;

            UInt16 sheetIndx;

            ComboBox source = e.Source as ComboBox;

            String cbName;

            //----------------------------------------------------------------//
            //                                                                //
            // Work out which combo box has just changed.                     //
            // This is done by examining the combo box name.                  //
            // The names should be in the format 'cbnn_xyz', where 'nn' is    //
            // a decimal value.                                               //
            //                                                                //
            //----------------------------------------------------------------//

            cbName = source.Name; // should be in format cbnn_xyz

            flagOK = UInt16.TryParse(cbName.Substring(2, 2),
                                      NumberStyles.HexNumber,
                                      CultureInfo.InvariantCulture,
                                      out sheetIndx);

            if (flagOK)
            {
                if (sheetIndx > _maxSheetNo)
                    flagOK = false;
            }

            if (!flagOK)
            {
                MessageBox.Show("Unable to detemine which Orientation (R)" +
                                " item has just been changed!",
                                "***** Internal error *****",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
            else
            {
                Int32 srcIndex = source.SelectedIndex;
                Int32 arrayIndex = sheetIndx - 1;

                if (_crntPDL == ToolCommonData.ePrintLang.PCL)
                    _indxOrientRearPCL[arrayIndex] = srcIndex;
                else
                    _indxOrientRearPCLXL[arrayIndex] = srcIndex;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b P a p e r S i z e _ S e l e c t i o n C h a n g e d            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Paper Size item has changed.                                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbPaperSize_SelectionChanged (
            object sender,
            SelectionChangedEventArgs e)
        {
            Boolean flagOK = false;

            UInt16 sheetIndx;

            ComboBox source = e.Source as ComboBox;

            String cbName;

            //----------------------------------------------------------------//
            //                                                                //
            // Work out which combo box has just changed.                     //
            // This is done by examining the combo box name.                  //
            // The names should be in the format 'cbnn_xyz', where 'nn' is    //
            // a decimal value.                                               //
            //                                                                //
            //----------------------------------------------------------------//

            cbName = source.Name; // should be in format cbnn_xyz

            flagOK = UInt16.TryParse(cbName.Substring(2, 2),
                                      NumberStyles.HexNumber,
                                      CultureInfo.InvariantCulture,
                                      out sheetIndx);

            if (flagOK)
            {
                if (sheetIndx > _maxSheetNo)
                    flagOK = false;
            }

            if (!flagOK)
            {
                MessageBox.Show("Unable to detemine which Paper Size" +
                                " item has just been changed!",
                                "***** Internal error *****",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
            else
            {
                Int32 srcIndex = source.SelectedIndex;
                Int32 arrayIndex = sheetIndx - 1;

                if (_crntPDL == ToolCommonData.ePrintLang.PCL)
                    _indxPaperSizePCL[arrayIndex] = srcIndex;
                else
                    _indxPaperSizePCLXL[arrayIndex] = srcIndex;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b P a p e r T r a y _ S e l e c t i o n C h a n g e d            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Paper Tray Identifier item has changed.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbPaperTray_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
        {
            if (! _inhibitTrayIdChange)
            {
                Boolean flagOK = false;

                UInt16 sheetIndx;

                ComboBox source = e.Source as ComboBox;

                String cbName;

                //------------------------------------------------------------//
                //                                                            //
                // Work out which combo box has just changed.                 //
                // This is done by examining the combo box name.              //
                // The names should be in the format 'cbnn_xyz', where 'nn'   //
                // is a decimal value.                                        //
                //                                                            //
                //------------------------------------------------------------//

                cbName = source.Name; // should be in format cbnn_xyz

                flagOK = UInt16.TryParse(cbName.Substring(2, 2),
                                          NumberStyles.HexNumber,
                                          CultureInfo.InvariantCulture,
                                          out sheetIndx);

                if (flagOK)
                {
                    if (sheetIndx > _maxSheetNo)
                        flagOK = false;
                }

                if (!flagOK)
                {
                    MessageBox.Show("Unable to detemine which Tray Identifier" +
                                    " item has just been changed!",
                                    "***** Internal error *****",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);
                }
                else
                {
                    Int32 srcIndex = source.SelectedIndex;
                    Int32 arrayIndex = sheetIndx - 1;

                    if (_crntPDL == ToolCommonData.ePrintLang.PCL)
                        _indxPaperTrayPCL[arrayIndex] = srcIndex;
                    else
                        _indxPaperTrayPCLXL[arrayIndex] = srcIndex;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b P a p e r T y p e _ S e l e c t i o n C h a n g e d            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Paper Type item has changed.                                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbPaperType_SelectionChanged (
            object sender,
            SelectionChangedEventArgs e)
        {
            Boolean flagOK = false;

            UInt16 sheetIndx;

            ComboBox source = e.Source as ComboBox;

            String cbName;

            //----------------------------------------------------------------//
            //                                                                //
            // Work out which combo box has just changed.                     //
            // This is done by examining the combo box name.                  //
            // The names should be in the format 'cbnn_xyz', where 'nn' is    //
            // a decimal value.                                               //
            //                                                                //
            //----------------------------------------------------------------//

            cbName = source.Name; // should be in format cbnn_xyz

            flagOK = UInt16.TryParse(cbName.Substring(2, 2),
                                      NumberStyles.HexNumber,
                                      CultureInfo.InvariantCulture,
                                      out sheetIndx);

            if (flagOK)
            {
                if (sheetIndx > _maxSheetNo)
                    flagOK = false;
            }

            if (!flagOK)
            {
                MessageBox.Show("Unable to detemine which Paper Type" +
                                " item has just been changed!",
                                "***** Internal error *****",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
            else
            {
                Int32 srcIndex = source.SelectedIndex;
                Int32 arrayIndex = sheetIndx - 1;

                if (_crntPDL == ToolCommonData.ePrintLang.PCL)
                    _indxPaperTypePCL[arrayIndex] = srcIndex;
                else
                    _indxPaperTypePCLXL[arrayIndex] = srcIndex;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b P D L _ S e l e c t i o n C h a n g e d                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Print Language item has changed.                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbPDL_SelectionChanged(object sender,
                                            SelectionChangedEventArgs e)
        {
            if (_initialised)
            {
                pdlOptionsStore();

                _indxPDL = cbPDL.SelectedIndex;
                _crntPDL = (ToolCommonData.ePrintLang) _subsetPDLs [_indxPDL];

                pdlOptionsRestore();
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b P l e x M o d e _ S e l e c t i o n C h a n g e d              //
        //--------------------------------------------------------------------//
        //                                                                    //
        //One of the Plex Mode items has changed.                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbPlexMode_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
        {
            Boolean flagOK = false;

            UInt16 sheetIndx;

            ComboBox source = e.Source as ComboBox;

            String cbName;

            //----------------------------------------------------------------//
            //                                                                //
            // Work out which combo box has just changed.                     //
            // This is done by examining the combo box name.                  //
            // The names should be in the format 'cbnn_xyz', where 'nn' is    //
            // a decimal value.                                               //
            //                                                                //
            //----------------------------------------------------------------//

            cbName = source.Name; // should be in format cbnn_xyz

            flagOK = UInt16.TryParse(cbName.Substring(2, 2),
                                      NumberStyles.HexNumber,
                                      CultureInfo.InvariantCulture,
                                      out sheetIndx);

            if (flagOK)
            {
                if (sheetIndx > _maxSheetNo)
                    flagOK = false;
            }

            if (!flagOK)
            {
                MessageBox.Show("Unable to detemine which Plex Mode" +
                                " item has just been changed!",
                                "***** Internal error *****",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
            else
            {
                Int32 srcIndex = source.SelectedIndex;
                Int32 arrayIndex = sheetIndx - 1;

                Boolean simplex =
                    PCLPlexModes.isSimplex(_subsetPlexModes[srcIndex]);

                if (_crntPDL == ToolCommonData.ePrintLang.PCL)
                    _indxPlexModePCL[arrayIndex] = srcIndex;
                else
                    _indxPlexModePCLXL[arrayIndex] = srcIndex;

                if (simplex)
                {
                    if (sheetIndx == 1)
                        cb01_OrientRear.Visibility = Visibility.Hidden;
                    else if (sheetIndx == 2)
                        cb02_OrientRear.Visibility = Visibility.Hidden;
                    else if (sheetIndx == 3)
                        cb03_OrientRear.Visibility = Visibility.Hidden;
                    else if (sheetIndx == 4)
                        cb04_OrientRear.Visibility = Visibility.Hidden;
                    else if (sheetIndx == 5)
                        cb05_OrientRear.Visibility = Visibility.Hidden;
                    else if (sheetIndx == 6)
                        cb06_OrientRear.Visibility = Visibility.Hidden;
                }
                else
                {
                    if (sheetIndx == 1)
                        cb01_OrientRear.Visibility = Visibility.Visible;
                    else if (sheetIndx == 2)
                        cb02_OrientRear.Visibility = Visibility.Visible;
                    else if (sheetIndx == 3)
                        cb03_OrientRear.Visibility = Visibility.Visible;
                    else if (sheetIndx == 4)
                        cb04_OrientRear.Visibility = Visibility.Visible;
                    else if (sheetIndx == 5)
                        cb05_OrientRear.Visibility = Visibility.Visible;
                    else if (sheetIndx == 6)
                        cb06_OrientRear.Visibility = Visibility.Visible;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c b S h e e t C t _ S e l e c t i o n C h a n g e d                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Sheet count item has changed.                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void cbSheetCt_SelectionChanged (
            object sender,
            SelectionChangedEventArgs e)
        {
            if (_initialised && cbSheetCt.HasItems)
            {
                sheetDataResetVisibility();
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k O p t F o r m A s M a c r o _ C h e c k e d                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Option 'Render fixed text as overlay' checked.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkOptFormAsMacro_Checked (object sender,
                                                RoutedEventArgs e)
        {
            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
                _formAsMacroPCL = true;
            else
                _formAsMacroPCLXL = true;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h k O p t F o r m A s M a c r o _ U n c h e c k e d              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Option 'Render fixed text as overlay' unchecked.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void chkOptFormAsMacro_Unchecked (object sender,
                                                  RoutedEventArgs e)
        {
            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
                _formAsMacroPCL = false;
            else
                _formAsMacroPCLXL = false;
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
            Int32 index;

            _initialised = false;

            _srcAutoSelectPCL   = ToolTrayMapPCL.TrayIdAutoSelect;
            _srcAutoSelectPCLXL = ToolTrayMapPCLXL.TrayIdAutoSelect;

            //----------------------------------------------------------------//
            //                                                                //
            // Populate form.                                                 //
            //                                                                //
            //----------------------------------------------------------------//

            cbPDL.Items.Clear();

            _ctPDLs = _subsetPDLs.Length;

            for (Int32 i = 0; i < _ctPDLs; i++)
            {
                index = _subsetPDLs[i];

                cbPDL.Items.Add(Enum.GetName(
                    typeof(ToolCommonData.ePrintLang), i));
            }

            //----------------------------------------------------------------//
            
            cbSheetCt.Items.Clear();

            for (Int32 i = 1; i <= _maxSheetNo; i++)
            {
                cbSheetCt.Items.Add(i);
            }

            cbSheetCt.SelectedIndex = 0;

            //----------------------------------------------------------------//

            cb01_PaperSize.Items.Clear();
            cb02_PaperSize.Items.Clear();
            cb03_PaperSize.Items.Clear();
            cb04_PaperSize.Items.Clear();
            cb05_PaperSize.Items.Clear();
            cb06_PaperSize.Items.Clear();

            _ctPaperSizes = _subsetPaperSizes.Length;

            for (Int32 i = 0; i < _ctPaperSizes; i++)
            {
                index = _subsetPaperSizes[i];

                String name = PCLPaperSizes.getName(index);

                cb01_PaperSize.Items.Add(name);
                cb02_PaperSize.Items.Add(name);
                cb03_PaperSize.Items.Add(name);
                cb04_PaperSize.Items.Add(name);
                cb05_PaperSize.Items.Add(name);
                cb06_PaperSize.Items.Add(name);
            }

            //----------------------------------------------------------------//

            cb01_PaperType.Items.Clear();
            cb02_PaperType.Items.Clear();
            cb03_PaperType.Items.Clear();
            cb04_PaperType.Items.Clear();
            cb05_PaperType.Items.Clear();
            cb06_PaperType.Items.Clear();

            _ctPaperTypes = _subsetPaperTypes.Length;

            for (Int32 i = 0; i < _ctPaperTypes; i++)
            {
                index = _subsetPaperTypes[i];

                String name = PCLPaperTypes.getName(index);

                cb01_PaperType.Items.Add(name);
                cb02_PaperType.Items.Add(name);
                cb03_PaperType.Items.Add(name);
                cb04_PaperType.Items.Add(name);
                cb05_PaperType.Items.Add(name);
                cb06_PaperType.Items.Add(name);
            }

            //----------------------------------------------------------------//

            cb01_PaperTray.Items.Clear();
            cb02_PaperTray.Items.Clear();
            cb03_PaperTray.Items.Clear();
            cb04_PaperTray.Items.Clear();
            cb05_PaperTray.Items.Clear();
            cb06_PaperTray.Items.Clear();

            cb01_PaperTray.Items.Add("<not specified>");
            cb02_PaperTray.Items.Add("<not specified>");
            cb03_PaperTray.Items.Add("<not specified>");
            cb04_PaperTray.Items.Add("<not specified>");
            cb05_PaperTray.Items.Add("<not specified>");
            cb06_PaperTray.Items.Add("<not specified>");

    //      _indxPaperTrayNotUsed = 0;

            for (Int32 i = 0; i <= _maxPaperTray; i++)
            {
                String name;

                if (i == _srcAutoSelectPCL)
                    name = i.ToString() + ": auto-select PCL";
                else if (i == _srcAutoSelectPCLXL)
                    name = i.ToString() + ": auto-select PCLXL";
                else
                    name = i.ToString();

                cb01_PaperTray.Items.Add(name);
                cb02_PaperTray.Items.Add(name);
                cb03_PaperTray.Items.Add(name);
                cb04_PaperTray.Items.Add(name);
                cb05_PaperTray.Items.Add(name);
                cb06_PaperTray.Items.Add(name);
            }

            //----------------------------------------------------------------//

            cb01_PlexMode.Items.Clear();
            cb02_PlexMode.Items.Clear();
            cb03_PlexMode.Items.Clear();
            cb04_PlexMode.Items.Clear();
            cb05_PlexMode.Items.Clear();
            cb06_PlexMode.Items.Clear();

            _ctPlexModes = _subsetPlexModes.Length;

            for (Int32 i = 0; i < _ctPlexModes; i++)
            {
                index = _subsetPlexModes[i];

                String name = PCLPlexModes.getName(index);

                cb01_PlexMode.Items.Add(name);
                cb02_PlexMode.Items.Add(name);
                cb03_PlexMode.Items.Add(name);
                cb04_PlexMode.Items.Add(name);
                cb05_PlexMode.Items.Add(name);
                cb06_PlexMode.Items.Add(name);
            }

            //----------------------------------------------------------------//

            cb01_OrientFront.Items.Clear();
            cb02_OrientFront.Items.Clear();
            cb03_OrientFront.Items.Clear();
            cb04_OrientFront.Items.Clear();
            cb05_OrientFront.Items.Clear();
            cb06_OrientFront.Items.Clear();

            cb01_OrientRear.Items.Clear();
            cb02_OrientRear.Items.Clear();
            cb03_OrientRear.Items.Clear();
            cb04_OrientRear.Items.Clear();
            cb05_OrientRear.Items.Clear();
            cb06_OrientRear.Items.Clear();

            _ctOrientations = _subsetOrientations.Length;

            for (Int32 i = 0; i < _ctOrientations; i++)
            {
                index = _subsetOrientations[i];

                String name = PCLOrientations.getName(index);

                cb01_OrientFront.Items.Add(name);
                cb02_OrientFront.Items.Add(name);
                cb03_OrientFront.Items.Add(name);
                cb04_OrientFront.Items.Add(name);
                cb05_OrientFront.Items.Add(name);
                cb06_OrientFront.Items.Add(name);

                cb01_OrientRear.Items.Add(name);
                cb02_OrientRear.Items.Add(name);
                cb03_OrientRear.Items.Add(name);
                cb04_OrientRear.Items.Add(name);
                cb05_OrientRear.Items.Add(name);
                cb06_OrientRear.Items.Add(name);
            }

            //----------------------------------------------------------------//

            resetTarget();

            //----------------------------------------------------------------//
            //                                                                //
            // Populate PDL-specific items.                                   //
            //                                                                //
            //----------------------------------------------------------------//

            initialisePCL();
            initialisePCLXL();

            //----------------------------------------------------------------//
            //                                                                //
            // Reinstate settings from persistent storage.                    //
            //                                                                //
            //----------------------------------------------------------------//

            metricsLoad();

            pdlOptionsRestore();
            
            cbPDL.SelectedIndex = (Byte)_indxPDL;
            
            _initialised = true;
            
            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
                cbSheetCt.SelectedIndex = _sheetCtPCL - 1;
            else
                cbSheetCt.SelectedIndex = _sheetCtPCLXL - 1;

            sheetDataResetVisibility();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n i t i a l i s e P C L                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initialisation (PCL).                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void initialisePCL()
        {
            _indxPaperSizePCL   = new Int32[_maxSheetNo];
            _indxPaperTypePCL   = new Int32[_maxSheetNo];
            _indxPaperTrayPCL   = new Int32[_maxSheetNo];
            _indxPlexModePCL    = new Int32[_maxSheetNo];
            _indxOrientFrontPCL = new Int32[_maxSheetNo];
            _indxOrientRearPCL  = new Int32[_maxSheetNo];
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n i t i a l i s e P C L X L                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initialisation (PCLXL).                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void initialisePCLXL()
        {
            _indxPaperSizePCLXL   = new Int32[_maxSheetNo];
            _indxPaperTypePCLXL   = new Int32[_maxSheetNo];
            _indxPaperTrayPCLXL   = new Int32[_maxSheetNo];
            _indxPlexModePCLXL    = new Int32[_maxSheetNo];
            _indxOrientFrontPCLXL = new Int32[_maxSheetNo];
            _indxOrientRearPCLXL  = new Int32[_maxSheetNo];
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
            ToolTrayMapPersist.loadDataCommon(ref _indxPDL);

            ToolTrayMapPersist.loadDataPCLOpt (ref _formAsMacroPCL,
                                               ref _sheetCtPCL);

            ToolTrayMapPersist.loadDataPCLXLOpt (ref _formAsMacroPCLXL,
                                                 ref _sheetCtPCLXL);

            //----------------------------------------------------------------//

            if ((_indxPDL < 0) || (_indxPDL >= _ctPDLs))
                _indxPDL = 0;
            
            _crntPDL = (ToolCommonData.ePrintLang) _subsetPDLs [_indxPDL];

            //----------------------------------------------------------------//

            for (Int32 i = 0; i < _maxSheetNo; i++)
            {
                ToolTrayMapPersist.loadDataSheetOpt(
                    "PCL",
                    i+1,
                    ref _indxPaperSizePCL[i],
                    ref _indxPaperTypePCL[i],
                    ref _indxPaperTrayPCL[i],
                    ref _indxPlexModePCL[i],
                    ref _indxOrientFrontPCL[i],
                    ref _indxOrientRearPCL[i]);

                if ((_indxPaperSizePCL[i] < 0) ||
                    (_indxPaperSizePCL[i] >= _ctPaperSizes))
                    _indxPaperSizePCL[i] = 0;

                if ((_indxPaperTypePCL[i] < 0) ||
                    (_indxPaperTypePCL[i] >= _ctPaperTypes))
                    _indxPaperTypePCL[i] = 0;

                if ((_indxPaperTrayPCL[i] < 0) ||
                    (_indxPaperTrayPCL[i] >= (_maxPaperTray+1)))
                    _indxPaperTrayPCL[i] = 0;

                if ((_indxPlexModePCL[i] < 0) ||
                    (_indxPlexModePCL[i] >= _ctPlexModes))
                    _indxPlexModePCL[i] = 0;

                if ((_indxOrientFrontPCL[i] < 0) ||
                    (_indxOrientFrontPCL[i] >= _ctOrientations))
                    _indxOrientFrontPCL[i] = 0;

                if ((_indxOrientRearPCL[i] < 0) ||
                    (_indxOrientRearPCL[i] >= _ctOrientations))
                    _indxOrientRearPCL[i] = 0;
            }
            //----------------------------------------------------------------//

            for (Int32 i = 0; i < _maxSheetNo; i++)
            {
                ToolTrayMapPersist.loadDataSheetOpt(
                    "PCLXL",
                    i+1,
                    ref _indxPaperSizePCLXL[i],
                    ref _indxPaperTypePCLXL[i],
                    ref _indxPaperTrayPCLXL[i],
                    ref _indxPlexModePCLXL[i],
                    ref _indxOrientFrontPCLXL[i],
                    ref _indxOrientRearPCLXL[i]);

                if ((_indxPaperSizePCLXL[i] < 0) ||
                    (_indxPaperSizePCLXL[i] >= _ctPaperSizes))
                    _indxPaperSizePCLXL[i] = 0;

                if ((_indxPaperTypePCLXL[i] < 0) ||
                    (_indxPaperTypePCLXL[i] >= _ctPaperTypes))
                    _indxPaperTypePCLXL[i] = 0;

                if ((_indxPaperTrayPCLXL[i] < 0) ||
                    (_indxPaperTrayPCLXL[i] >= (_maxPaperTray + 1)))
                    _indxPaperTrayPCLXL[i] = 0;

                if ((_indxPlexModePCLXL[i] < 0) ||
                    (_indxPlexModePCLXL[i] >= _ctPlexModes))
                    _indxPlexModePCLXL[i] = 0;

                if ((_indxOrientFrontPCLXL[i] < 0) ||
                    (_indxOrientFrontPCLXL[i] >= _ctOrientations))
                    _indxOrientFrontPCLXL[i] = 0;

                if ((_indxOrientRearPCLXL[i] < 0) ||
                    (_indxOrientRearPCLXL[i] >= _ctOrientations))
                    _indxOrientRearPCLXL[i] = 0;
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
            pdlOptionsStore();

        //  trayIdSetStore();

            ToolTrayMapPersist.saveDataCommon((Int32)_indxPDL);

            ToolTrayMapPersist.saveDataPCLOpt (_formAsMacroPCL,
                                               _sheetCtPCL);

            ToolTrayMapPersist.saveDataPCLXLOpt (_formAsMacroPCLXL,
                                                 _sheetCtPCLXL);


            for (Int32 i = 0; i < _maxSheetNo; i++)
            {
                ToolTrayMapPersist.saveDataSheetOpt (
                    "PCL",
                    i + 1,
                    _indxPaperSizePCL[i],
                    _indxPaperTypePCL[i],
                    _indxPaperTrayPCL[i],
                    _indxPlexModePCL[i],
                    _indxOrientFrontPCL[i],
                    _indxOrientRearPCL[i]);
            }

            for (Int32 i = 0; i < _maxSheetNo; i++)
            {
                ToolTrayMapPersist.saveDataSheetOpt (
                    "PCLXL",
                    i + 1,
                    _indxPaperSizePCLXL[i],
                    _indxPaperTypePCLXL[i],
                    _indxPaperTrayPCLXL[i],
                    _indxPlexModePCLXL[i],
                    _indxOrientFrontPCLXL[i],
                    _indxOrientRearPCLXL[i]);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p d l O p t i o n s R e s t o r e                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Restore the test metrics options for the current PDL.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void pdlOptionsRestore()
        {
            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                cbSheetCt.SelectedIndex = _sheetCtPCL - 1;

                chkOptFormAsMacro.IsChecked  = _formAsMacroPCL;
            }
            else
            {
                cbSheetCt.SelectedIndex = _sheetCtPCLXL - 1;

                chkOptFormAsMacro.IsChecked = _formAsMacroPCLXL;
            }

            sheetDataRestore ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p d l O p t i o n s S t o r e                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store the test metrics options for the current PDL.                //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void pdlOptionsStore()
        {
            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                _sheetCtPCL = (cbSheetCt.SelectedIndex) + 1;

                if (chkOptFormAsMacro.IsChecked == true)
                    _formAsMacroPCL = true;
                else
                    _formAsMacroPCL = false;
            }
            else
            {
                _sheetCtPCLXL = (cbSheetCt.SelectedIndex) + 1;

                if (chkOptFormAsMacro.IsChecked == true)
                    _formAsMacroPCLXL = true;
                else
                    _formAsMacroPCLXL = false;
            }

            sheetDataStore ();
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
                btnGenerate.Content = "Generate & save test data to file";
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

                btnGenerate.Content = "Generate & send test data to " +
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
        // s h e e t D a t a R e s e t V i s i b i l i t y                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Sheet count item has changed; ensure correct sheet data groups are //
        // are visible or hidden, as appropriate.                             //
        // The assumption is that the sheet count value is one greater than   //
        // the combo-box item index.                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void sheetDataResetVisibility ()
        {
            Int32 sheetCt;

            sheetCt = (cbSheetCt.SelectedIndex) + 1;

            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
                _sheetCtPCL = sheetCt;
            else
                _sheetCtPCLXL = sheetCt;

            switch (sheetCt)
            {
                case 1:

                    btnResetSheetData.Visibility = Visibility.Hidden;

                    grp01_Sheet.Visibility = Visibility.Visible;
                    grp02_Sheet.Visibility = Visibility.Hidden;
                    grp03_Sheet.Visibility = Visibility.Hidden;
                    grp04_Sheet.Visibility = Visibility.Hidden;
                    grp05_Sheet.Visibility = Visibility.Hidden;
                    grp06_Sheet.Visibility = Visibility.Hidden;
                    break;

                case 2:

                    btnResetSheetData.Visibility = Visibility.Visible;

                    grp01_Sheet.Visibility = Visibility.Visible;
                    grp02_Sheet.Visibility = Visibility.Visible;
                    grp03_Sheet.Visibility = Visibility.Hidden;
                    grp04_Sheet.Visibility = Visibility.Hidden;
                    grp05_Sheet.Visibility = Visibility.Hidden;
                    grp06_Sheet.Visibility = Visibility.Hidden;
                    break;

                case 3:

                    btnResetSheetData.Visibility = Visibility.Visible;

                    grp01_Sheet.Visibility = Visibility.Visible;
                    grp02_Sheet.Visibility = Visibility.Visible;
                    grp03_Sheet.Visibility = Visibility.Visible;
                    grp04_Sheet.Visibility = Visibility.Hidden;
                    grp05_Sheet.Visibility = Visibility.Hidden;
                    grp06_Sheet.Visibility = Visibility.Hidden;
                    break;

                case 4:

                    btnResetSheetData.Visibility = Visibility.Visible;

                    grp01_Sheet.Visibility = Visibility.Visible;
                    grp02_Sheet.Visibility = Visibility.Visible;
                    grp03_Sheet.Visibility = Visibility.Visible;
                    grp04_Sheet.Visibility = Visibility.Visible;
                    grp05_Sheet.Visibility = Visibility.Hidden;
                    grp06_Sheet.Visibility = Visibility.Hidden;
                    break;

                case 5:

                    btnResetSheetData.Visibility = Visibility.Visible;

                    grp01_Sheet.Visibility = Visibility.Visible;
                    grp02_Sheet.Visibility = Visibility.Visible;
                    grp03_Sheet.Visibility = Visibility.Visible;
                    grp04_Sheet.Visibility = Visibility.Visible;
                    grp05_Sheet.Visibility = Visibility.Visible;
                    grp06_Sheet.Visibility = Visibility.Hidden;
                    break;

                case 6:

                    btnResetSheetData.Visibility = Visibility.Visible;

                    grp01_Sheet.Visibility = Visibility.Visible;
                    grp02_Sheet.Visibility = Visibility.Visible;
                    grp03_Sheet.Visibility = Visibility.Visible;
                    grp04_Sheet.Visibility = Visibility.Visible;
                    grp05_Sheet.Visibility = Visibility.Visible;
                    grp06_Sheet.Visibility = Visibility.Visible;
                    break;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s h e e t D a t a R e s t o r e                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Restore the selected sheet data items to the stored values.        //
        //                                                                    //
        // So that we can show the correct "auto-select" identifier (which is //
        // different for PCL and PCL XL), we need to change the correct item  //
        // values - the assumption is made that the identifier value is one   //
        // greater than the combo-box item index 9oin order to cater for the  //
        // first entry which is for "<not specified>".                        // 
        //                                                                    //
        //--------------------------------------------------------------------//

        private void sheetDataRestore ()
        {
            Int32 itemAutoPCL   = _srcAutoSelectPCL + 1;
            Int32 itemAutoPCLXL = _srcAutoSelectPCLXL + 1;

            String valStdPCL    = _srcAutoSelectPCL.ToString();
            String valStdPCLXL  = _srcAutoSelectPCLXL.ToString();

            String valAutoPCL   = _srcAutoSelectPCL.ToString()   +
                                  ": auto-select";
            String valAutoPCLXL = _srcAutoSelectPCLXL.ToString() +
                                  ": auto-select";

            _inhibitTrayIdChange = true;

            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                cb01_PaperTray.Items[itemAutoPCLXL] = valStdPCLXL;
                cb02_PaperTray.Items[itemAutoPCLXL] = valStdPCLXL;
                cb03_PaperTray.Items[itemAutoPCLXL] = valStdPCLXL;
                cb04_PaperTray.Items[itemAutoPCLXL] = valStdPCLXL;
                cb05_PaperTray.Items[itemAutoPCLXL] = valStdPCLXL;
                cb06_PaperTray.Items[itemAutoPCLXL] = valStdPCLXL;

                cb01_PaperTray.Items[itemAutoPCL] = valAutoPCL;
                cb02_PaperTray.Items[itemAutoPCL] = valAutoPCL;
                cb03_PaperTray.Items[itemAutoPCL] = valAutoPCL;
                cb04_PaperTray.Items[itemAutoPCL] = valAutoPCL;
                cb05_PaperTray.Items[itemAutoPCL] = valAutoPCL;
                cb06_PaperTray.Items[itemAutoPCL] = valAutoPCL;

                cb01_PaperSize.SelectedIndex = _indxPaperSizePCL[0];
                cb02_PaperSize.SelectedIndex = _indxPaperSizePCL[1];
                cb03_PaperSize.SelectedIndex = _indxPaperSizePCL[2];
                cb04_PaperSize.SelectedIndex = _indxPaperSizePCL[3];
                cb05_PaperSize.SelectedIndex = _indxPaperSizePCL[4];
                cb06_PaperSize.SelectedIndex = _indxPaperSizePCL[5];

                cb01_PaperType.SelectedIndex = _indxPaperTypePCL[0];
                cb02_PaperType.SelectedIndex = _indxPaperTypePCL[1];
                cb03_PaperType.SelectedIndex = _indxPaperTypePCL[2];
                cb04_PaperType.SelectedIndex = _indxPaperTypePCL[3];
                cb05_PaperType.SelectedIndex = _indxPaperTypePCL[4];
                cb06_PaperType.SelectedIndex = _indxPaperTypePCL[5];

                cb01_PaperTray.SelectedIndex = _indxPaperTrayPCL[0];
                cb02_PaperTray.SelectedIndex = _indxPaperTrayPCL[1];
                cb03_PaperTray.SelectedIndex = _indxPaperTrayPCL[2];
                cb04_PaperTray.SelectedIndex = _indxPaperTrayPCL[3];
                cb05_PaperTray.SelectedIndex = _indxPaperTrayPCL[4];
                cb06_PaperTray.SelectedIndex = _indxPaperTrayPCL[5];

                cb01_PlexMode.SelectedIndex = _indxPlexModePCL[0];
                cb02_PlexMode.SelectedIndex = _indxPlexModePCL[1];
                cb03_PlexMode.SelectedIndex = _indxPlexModePCL[2];
                cb04_PlexMode.SelectedIndex = _indxPlexModePCL[3];
                cb05_PlexMode.SelectedIndex = _indxPlexModePCL[4];
                cb06_PlexMode.SelectedIndex = _indxPlexModePCL[5];

                cb01_OrientFront.SelectedIndex = _indxOrientFrontPCL[0];
                cb02_OrientFront.SelectedIndex = _indxOrientFrontPCL[1];
                cb03_OrientFront.SelectedIndex = _indxOrientFrontPCL[2];
                cb04_OrientFront.SelectedIndex = _indxOrientFrontPCL[3];
                cb05_OrientFront.SelectedIndex = _indxOrientFrontPCL[4];
                cb06_OrientFront.SelectedIndex = _indxOrientFrontPCL[5];

                cb01_OrientRear.SelectedIndex = _indxOrientRearPCL[0];
                cb02_OrientRear.SelectedIndex = _indxOrientRearPCL[1];
                cb03_OrientRear.SelectedIndex = _indxOrientRearPCL[2];
                cb04_OrientRear.SelectedIndex = _indxOrientRearPCL[3];
                cb05_OrientRear.SelectedIndex = _indxOrientRearPCL[4];
                cb06_OrientRear.SelectedIndex = _indxOrientRearPCL[5];
            }
            else
            {
                cb01_PaperTray.Items[itemAutoPCL] = valStdPCL;
                cb02_PaperTray.Items[itemAutoPCL] = valStdPCL;
                cb03_PaperTray.Items[itemAutoPCL] = valStdPCL;
                cb04_PaperTray.Items[itemAutoPCL] = valStdPCL;
                cb05_PaperTray.Items[itemAutoPCL] = valStdPCL;
                cb06_PaperTray.Items[itemAutoPCL] = valStdPCL;

                cb01_PaperTray.Items[itemAutoPCLXL] = valAutoPCLXL;
                cb02_PaperTray.Items[itemAutoPCLXL] = valAutoPCLXL;
                cb03_PaperTray.Items[itemAutoPCLXL] = valAutoPCLXL;
                cb04_PaperTray.Items[itemAutoPCLXL] = valAutoPCLXL;
                cb05_PaperTray.Items[itemAutoPCLXL] = valAutoPCLXL;
                cb06_PaperTray.Items[itemAutoPCLXL] = valAutoPCLXL;

                cb01_PaperSize.SelectedIndex = _indxPaperSizePCLXL[0];
                cb02_PaperSize.SelectedIndex = _indxPaperSizePCLXL[1];
                cb03_PaperSize.SelectedIndex = _indxPaperSizePCLXL[2];
                cb04_PaperSize.SelectedIndex = _indxPaperSizePCLXL[3];
                cb05_PaperSize.SelectedIndex = _indxPaperSizePCLXL[4];
                cb06_PaperSize.SelectedIndex = _indxPaperSizePCLXL[5];

                cb01_PaperType.SelectedIndex = _indxPaperTypePCLXL[0];
                cb02_PaperType.SelectedIndex = _indxPaperTypePCLXL[1];
                cb03_PaperType.SelectedIndex = _indxPaperTypePCLXL[2];
                cb04_PaperType.SelectedIndex = _indxPaperTypePCLXL[3];
                cb05_PaperType.SelectedIndex = _indxPaperTypePCLXL[4];
                cb06_PaperType.SelectedIndex = _indxPaperTypePCLXL[5];

                cb01_PaperTray.SelectedIndex = _indxPaperTrayPCLXL[0];
                cb02_PaperTray.SelectedIndex = _indxPaperTrayPCLXL[1];
                cb03_PaperTray.SelectedIndex = _indxPaperTrayPCLXL[2];
                cb04_PaperTray.SelectedIndex = _indxPaperTrayPCLXL[3];
                cb05_PaperTray.SelectedIndex = _indxPaperTrayPCLXL[4];
                cb06_PaperTray.SelectedIndex = _indxPaperTrayPCLXL[5];

                cb01_PlexMode.SelectedIndex = _indxPlexModePCLXL[0];
                cb02_PlexMode.SelectedIndex = _indxPlexModePCLXL[1];
                cb03_PlexMode.SelectedIndex = _indxPlexModePCLXL[2];
                cb04_PlexMode.SelectedIndex = _indxPlexModePCLXL[3];
                cb05_PlexMode.SelectedIndex = _indxPlexModePCLXL[4];
                cb06_PlexMode.SelectedIndex = _indxPlexModePCLXL[5];

                cb01_OrientFront.SelectedIndex = _indxOrientFrontPCLXL[0];
                cb02_OrientFront.SelectedIndex = _indxOrientFrontPCLXL[1];
                cb03_OrientFront.SelectedIndex = _indxOrientFrontPCLXL[2];
                cb04_OrientFront.SelectedIndex = _indxOrientFrontPCLXL[3];
                cb05_OrientFront.SelectedIndex = _indxOrientFrontPCLXL[4];
                cb06_OrientFront.SelectedIndex = _indxOrientFrontPCLXL[5];

                cb01_OrientRear.SelectedIndex = _indxOrientRearPCLXL[0];
                cb02_OrientRear.SelectedIndex = _indxOrientRearPCLXL[1];
                cb03_OrientRear.SelectedIndex = _indxOrientRearPCLXL[2];
                cb04_OrientRear.SelectedIndex = _indxOrientRearPCLXL[3];
                cb05_OrientRear.SelectedIndex = _indxOrientRearPCLXL[4];
                cb06_OrientRear.SelectedIndex = _indxOrientRearPCLXL[5];
            }

            _inhibitTrayIdChange = false;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s h e e t D a t a S t o r e                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store the selected sheet data items.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void sheetDataStore()
        {
            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                _indxPaperSizePCL[0] = cb01_PaperSize.SelectedIndex;
                _indxPaperSizePCL[1] = cb02_PaperSize.SelectedIndex;
                _indxPaperSizePCL[2] = cb03_PaperSize.SelectedIndex;
                _indxPaperSizePCL[3] = cb04_PaperSize.SelectedIndex;
                _indxPaperSizePCL[4] = cb05_PaperSize.SelectedIndex;
                _indxPaperSizePCL[5] = cb06_PaperSize.SelectedIndex;

                _indxPaperTypePCL[0] = cb01_PaperType.SelectedIndex;
                _indxPaperTypePCL[1] = cb02_PaperType.SelectedIndex;
                _indxPaperTypePCL[2] = cb03_PaperType.SelectedIndex;
                _indxPaperTypePCL[3] = cb04_PaperType.SelectedIndex;
                _indxPaperTypePCL[4] = cb05_PaperType.SelectedIndex;
                _indxPaperTypePCL[5] = cb06_PaperType.SelectedIndex;

                _indxPaperTrayPCL[0] = cb01_PaperTray.SelectedIndex;
                _indxPaperTrayPCL[1] = cb02_PaperTray.SelectedIndex;
                _indxPaperTrayPCL[2] = cb03_PaperTray.SelectedIndex;
                _indxPaperTrayPCL[3] = cb04_PaperTray.SelectedIndex;
                _indxPaperTrayPCL[4] = cb05_PaperTray.SelectedIndex;
                _indxPaperTrayPCL[5] = cb06_PaperTray.SelectedIndex;

                _indxPlexModePCL[0] = cb01_PlexMode.SelectedIndex;
                _indxPlexModePCL[1] = cb02_PlexMode.SelectedIndex;
                _indxPlexModePCL[2] = cb03_PlexMode.SelectedIndex;
                _indxPlexModePCL[3] = cb04_PlexMode.SelectedIndex;
                _indxPlexModePCL[4] = cb05_PlexMode.SelectedIndex;
                _indxPlexModePCL[5] = cb06_PlexMode.SelectedIndex;

                _indxOrientFrontPCL[0] = cb01_OrientFront.SelectedIndex;
                _indxOrientFrontPCL[1] = cb02_OrientFront.SelectedIndex;
                _indxOrientFrontPCL[2] = cb03_OrientFront.SelectedIndex;
                _indxOrientFrontPCL[3] = cb04_OrientFront.SelectedIndex;
                _indxOrientFrontPCL[4] = cb05_OrientFront.SelectedIndex;
                _indxOrientFrontPCL[5] = cb06_OrientFront.SelectedIndex;

                _indxOrientRearPCL[0] = cb01_OrientRear.SelectedIndex;
                _indxOrientRearPCL[1] = cb02_OrientRear.SelectedIndex;
                _indxOrientRearPCL[2] = cb03_OrientRear.SelectedIndex;
                _indxOrientRearPCL[3] = cb04_OrientRear.SelectedIndex;
                _indxOrientRearPCL[4] = cb05_OrientRear.SelectedIndex;
                _indxOrientRearPCL[5] = cb06_OrientRear.SelectedIndex;
            }
            else
            {
                _indxPaperSizePCLXL[0] = cb01_PaperSize.SelectedIndex;
                _indxPaperSizePCLXL[1] = cb02_PaperSize.SelectedIndex;
                _indxPaperSizePCLXL[2] = cb03_PaperSize.SelectedIndex;
                _indxPaperSizePCLXL[3] = cb04_PaperSize.SelectedIndex;
                _indxPaperSizePCLXL[4] = cb05_PaperSize.SelectedIndex;
                _indxPaperSizePCLXL[5] = cb06_PaperSize.SelectedIndex;

                _indxPaperTypePCLXL[0] = cb01_PaperType.SelectedIndex;
                _indxPaperTypePCLXL[1] = cb02_PaperType.SelectedIndex;
                _indxPaperTypePCLXL[2] = cb03_PaperType.SelectedIndex;
                _indxPaperTypePCLXL[3] = cb04_PaperType.SelectedIndex;
                _indxPaperTypePCLXL[4] = cb05_PaperType.SelectedIndex;
                _indxPaperTypePCLXL[5] = cb06_PaperType.SelectedIndex;

                _indxPaperTrayPCLXL[0] = cb01_PaperTray.SelectedIndex;
                _indxPaperTrayPCLXL[1] = cb02_PaperTray.SelectedIndex;
                _indxPaperTrayPCLXL[2] = cb03_PaperTray.SelectedIndex;
                _indxPaperTrayPCLXL[3] = cb04_PaperTray.SelectedIndex;
                _indxPaperTrayPCLXL[4] = cb05_PaperTray.SelectedIndex;
                _indxPaperTrayPCLXL[5] = cb06_PaperTray.SelectedIndex;

                _indxPlexModePCLXL[0] = cb01_PlexMode.SelectedIndex;
                _indxPlexModePCLXL[1] = cb02_PlexMode.SelectedIndex;
                _indxPlexModePCLXL[2] = cb03_PlexMode.SelectedIndex;
                _indxPlexModePCLXL[3] = cb04_PlexMode.SelectedIndex;
                _indxPlexModePCLXL[4] = cb05_PlexMode.SelectedIndex;
                _indxPlexModePCLXL[5] = cb06_PlexMode.SelectedIndex;

                _indxOrientFrontPCLXL[0] = cb01_OrientFront.SelectedIndex;
                _indxOrientFrontPCLXL[1] = cb02_OrientFront.SelectedIndex;
                _indxOrientFrontPCLXL[2] = cb03_OrientFront.SelectedIndex;
                _indxOrientFrontPCLXL[3] = cb04_OrientFront.SelectedIndex;
                _indxOrientFrontPCLXL[4] = cb05_OrientFront.SelectedIndex;
                _indxOrientFrontPCLXL[5] = cb06_OrientFront.SelectedIndex;

                _indxOrientRearPCLXL[0] = cb01_OrientRear.SelectedIndex;
                _indxOrientRearPCLXL[1] = cb02_OrientRear.SelectedIndex;
                _indxOrientRearPCLXL[2] = cb03_OrientRear.SelectedIndex;
                _indxOrientRearPCLXL[3] = cb04_OrientRear.SelectedIndex;
                _indxOrientRearPCLXL[4] = cb05_OrientRear.SelectedIndex;
                _indxOrientRearPCLXL[5] = cb06_OrientRear.SelectedIndex;
            }
        }
    }
}
