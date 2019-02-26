using System;
using System.IO;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// Interaction logic for ToolMiscSamples.xaml
    /// 
    /// Class handles the MiscSamples: Patterns tab.
    /// 
    /// © Chris Hutchinson 2015
    /// 
    /// </summary>
    
    [System.Reflection.ObfuscationAttribute(Feature = "renaming",
                                            ApplyToMembers = true)]

    public partial class ToolMiscSamples : Window
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private enum ePatternType : byte
        {
            Shading,
            XHatch
        }

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Fields (class variables).                                          //
        //                                                                    //
        //--------------------------------------------------------------------//
        
        private ePatternType _indxPatternTypePCL;
        private ePatternType _indxPatternTypePCLXL;

        private Boolean _flagPatternFormAsMacroPCL;
        private Boolean _flagPatternFormAsMacroPCLXL;

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n i t i a l i s e D a t a P a t t e r n                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initialise 'Patterns' data.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void initialiseDataPattern()
        {
            lbOrientation.Visibility = Visibility.Hidden;
            cbOrientation.Visibility = Visibility.Hidden;

            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                if (_indxPatternTypePCL == ePatternType.Shading)
                    rbPatternTypeShading.IsChecked = true;
                else if (_indxPatternTypePCL == ePatternType.XHatch)
                    rbPatternTypeXHatch.IsChecked = true;
                else
                {
                    _indxPatternTypePCL = ePatternType.Shading;

                    rbPatternTypeShading.IsChecked = true;
                }

                if (_flagPatternFormAsMacroPCL)
                    chkOptFormAsMacro.IsChecked = true;
                else
                    chkOptFormAsMacro.IsChecked = false;
            }
            else
            {
                if (_indxPatternTypePCLXL == ePatternType.Shading)
                    rbPatternTypeShading.IsChecked = true;
                else if (_indxPatternTypePCLXL == ePatternType.XHatch)
                    rbPatternTypeXHatch.IsChecked = true;
                else
                {
                    _indxPatternTypePCLXL = ePatternType.Shading;

                    rbPatternTypeShading.IsChecked = true;
                }

                if (_flagPatternFormAsMacroPCLXL)
                    chkOptFormAsMacro.IsChecked = true;
                else
                    chkOptFormAsMacro.IsChecked = false;
            }

            initialiseDescPattern();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n i t i a l i s e D e s c P a t t e r n                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initialise 'Patterns' description.                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void initialiseDescPattern()
        {
            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                if (_indxPatternTypePCL == ePatternType.Shading)
                {
                    txtPatternDesc.Text =
                        "Shows samples of built-in and user-defined" +
                        " Shading patterns.";
                }
                else
                {
                    txtPatternDesc.Text =
                        "Shows samples of built-in and user-defined" +
                        " Cross-Hatch patterns.";
                }
            }
            else
            {
                if (_indxPatternTypePCLXL == ePatternType.Shading)
                {
                    txtPatternDesc.Text =
                        "Shows samples of user-defined Shading patterns.";
                }
                else
                {
                    txtPatternDesc.Text =
                        "Shows samples of user-defined Cross-hatch patterns.";
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m e t r i c s L o a d D a t a P a t t e r n                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Load current metrics from persistent storage.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void metricsLoadDataPattern()
        {
            Int32 tmpInt = 0;

            ToolMiscSamplesPersist.loadDataTypePattern (
                "PCL",
                ref tmpInt,
                ref _flagPatternFormAsMacroPCL);

            if (tmpInt == (Int32) ePatternType.XHatch)
                _indxPatternTypePCL = ePatternType.XHatch;
            else
                _indxPatternTypePCL = ePatternType.Shading;

            ToolMiscSamplesPersist.loadDataTypePattern (
                "PCLXL",
                ref tmpInt,
                ref _flagPatternFormAsMacroPCLXL);

            if (tmpInt == (Int32) ePatternType.XHatch)
                _indxPatternTypePCLXL = ePatternType.XHatch;
            else
                _indxPatternTypePCLXL = ePatternType.Shading;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m e t r i c s S a v e D a t a P a t t e r n                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Save current metrics to persistent storage.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void metricsSaveDataPattern()
        {
            ToolMiscSamplesPersist.saveDataTypePattern (
                "PCL",
                (Int32) _indxPatternTypePCL,
                _flagPatternFormAsMacroPCL);

            ToolMiscSamplesPersist.saveDataTypePattern (
                "PCLXL",
                (Int32) _indxPatternTypePCLXL,
                _flagPatternFormAsMacroPCLXL);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b P a t t e r n T y p e S h a d i n g _ C l i c k                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Radio button selecting 'Shading' patterns.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbPatternTypeShading_Click (object sender,
                                                 RoutedEventArgs e)
        {
            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
                _indxPatternTypePCL = ePatternType.Shading;
            else
                _indxPatternTypePCLXL = ePatternType.Shading;

            initialiseDescPattern();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b P a t t e r n T y p e X H a t c h _ C l i c k                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Radio button selecting 'Cross-hatch' patterns.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbPatternTypeXHatch_Click (object sender,
                                                RoutedEventArgs e)
        {
            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
                _indxPatternTypePCL = ePatternType.XHatch;
            else
                _indxPatternTypePCLXL = ePatternType.XHatch;

            initialiseDescPattern();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t F l a g P a t t e r n F o r m A s M a c r o                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set or unset 'Render fixed text as overlay' flag.                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void setFlagPatternFormAsMacro (
            Boolean setFlag,
            ToolCommonData.ePrintLang crntPDL)
        {
            if (crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                if (setFlag)
                    _flagPatternFormAsMacroPCL = true;
                else
                    _flagPatternFormAsMacroPCL = false;
            }
            else if (crntPDL == ToolCommonData.ePrintLang.PCLXL)
            {
                if (setFlag)
                    _flagPatternFormAsMacroPCLXL = true;
                else
                    _flagPatternFormAsMacroPCLXL = false;
            }
        }
    }
}
