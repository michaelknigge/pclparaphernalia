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
    /// Class handles the MiscSamples Character Embellishments tab.
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

        private enum eTxtModType : byte
        {
            Chr,
            Pat,
            Rot
        }

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Fields (class variables).                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private eTxtModType _indxTxtModTypePCL;
        private eTxtModType _indxTxtModTypePCLXL;

        private Boolean _flagTxtModFormAsMacroPCL;
        private Boolean _flagTxtModFormAsMacroPCLXL;

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n i t i a l i s e D a t a T x t M o d                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initialise 'Text modification' data.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void initialiseDataTxtMod()
        {
            lbOrientation.Visibility = Visibility.Hidden;
            cbOrientation.Visibility = Visibility.Hidden;

            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                if (_indxTxtModTypePCL == eTxtModType.Chr)
                    rbTxtModTypeChr.IsChecked = true;
                else if (_indxTxtModTypePCL == eTxtModType.Pat)
                    rbTxtModTypePat.IsChecked = true;
                else if (_indxTxtModTypePCL == eTxtModType.Rot)
                    rbTxtModTypeRot.IsChecked = true;
                else
                {
                    _indxTxtModTypePCL = eTxtModType.Chr;

                    rbTxtModTypeChr.IsChecked = true;
                }

                if (_flagTxtModFormAsMacroPCL)
                    chkOptFormAsMacro.IsChecked = true;
                else
                    chkOptFormAsMacro.IsChecked = false;
            }
            else
            {
                if (_indxTxtModTypePCLXL == eTxtModType.Chr)
                    rbTxtModTypeChr.IsChecked = true;
                else if (_indxTxtModTypePCLXL == eTxtModType.Pat)
                    rbTxtModTypePat.IsChecked = true;
                else if (_indxTxtModTypePCLXL == eTxtModType.Rot)
                    rbTxtModTypeRot.IsChecked = true;
                else
                {
                    _indxTxtModTypePCLXL = eTxtModType.Chr;

                    rbTxtModTypeChr.IsChecked = true;
                }

                if (_flagTxtModFormAsMacroPCL)
                    chkOptFormAsMacro.IsChecked = true;
                else
                    chkOptFormAsMacro.IsChecked = false;
            }

            initialiseDescTxtMod();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n i t i a l i s e D e s c T x t M o d                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Initialise 'Text modification' description.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void initialiseDescTxtMod()
        {
            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                if (_indxTxtModTypePCL == eTxtModType.Chr)
                {
                    txtTxtModDesc.Text =
                        "Most font embellishments, apart from the standard" +
                        " style (e.g. italic) and weight (e.g. bold)" +
                        " variations, are provided using HP-GL/2 commands:" +
                        "\r\n\r\n" +
                        "This sample shows:\r\n" +
                        "    X and Y sizing\r\n" +
                        "    character slant - left and right\r\n" +
                        "    spacing: extra space / less space\r\n" +
                        "\r\n" +
                        "Some LaserJet printers support PCL pseudo-bold and" +
                        " pseudo-italic character enhancements." +
                        "\r\n" +
                        "Such enhancements can only be applied to certain" +
                        " fonts, for example the internal MS-Mincho and" +
                        " MS-Gothic fonts, or downloaded PCLETTO fonts which" +
                        " contain a 'Character Enhancement' segment." +
                        "\r\n" +
                        "These pseudo-enhancements are not demonstrated here";
                }
                else if (_indxTxtModTypePCL == eTxtModType.Pat)
                {
                    txtTxtModDesc.Text =
                        "Shows samples of Black, White and Patterned text," +
                        " each printed on Black, White and Patterned" +
                        " backgrounds.";
                }
                else
                {
                    txtTxtModDesc.Text =
                        "Shows samples of Rotated text.\r\n" +
                        "Text rotated by other than 0, 90, 180  or 270" +
                        " degrees in PCL is achieved via use of HP-GL/2" +
                        " commands.";
                }
            }
            else
            {
                if (_indxTxtModTypePCLXL == eTxtModType.Chr)
                {
                    txtTxtModDesc.Text =
                        "PCL XL supports several different character" +
                        " embellishments:\r\n" +
                        " - Angle\r\n" +
                        " - Pseudo-Bold\r\n" +
                        " - Scale (separate X and Y values)\r\n" +
                        " - Shear (separate X and Y values)";
                }
                else if (_indxTxtModTypePCLXL == eTxtModType.Pat)
                {
                    txtTxtModDesc.Text =
                        "Shows samples of Black, White and Patterned text," +
                        " each printed on Black, White and Patterned" +
                        " backgrounds.";
                }
                else
                {
                    txtTxtModDesc.Text =
                        "Shows samples of Rotated text.\r\n" +
                        "Text rotated by other than 0, 90, 180  or 270" +
                        " degrees in PCL XL is achieved via use of the" +
                        " CharAngle attribute of the SetCharangle operator," +
                        " in conjunction with appropriate XSpacingData and" +
                        " YSpacingData attributes of the Text operator.";
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m e t r i c s L o a d D a t a T x t M o d                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Load current metrics from persistent storage.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void metricsLoadDataTxtMod()
        {
            Int32 tmpInt = 0;

            ToolMiscSamplesPersist.loadDataTypeTxtMod (
                "PCL",
                ref tmpInt,
                ref _flagTxtModFormAsMacroPCL);

            if (tmpInt == (Int32)eTxtModType.Pat)
                _indxTxtModTypePCL = eTxtModType.Pat;
            else if (tmpInt == (Int32)eTxtModType.Rot)
                _indxTxtModTypePCL = eTxtModType.Rot;
            else
                _indxTxtModTypePCL = eTxtModType.Chr;

            ToolMiscSamplesPersist.loadDataTypeTxtMod (
                "PCLXL",
                ref tmpInt,
                ref _flagTxtModFormAsMacroPCLXL);

            if (tmpInt == (Int32)eTxtModType.Pat)
                _indxTxtModTypePCLXL = eTxtModType.Pat;
            else if (tmpInt == (Int32)eTxtModType.Rot)
                _indxTxtModTypePCLXL = eTxtModType.Rot;
            else
                _indxTxtModTypePCLXL = eTxtModType.Chr;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m e t r i c s S a v e D a t a T x t M o d                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Save current metrics to persistent storage.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void metricsSaveDataTxtMod()
        {
            ToolMiscSamplesPersist.saveDataTypeTxtMod (
                "PCL",
                (Int32)_indxTxtModTypePCL,
                _flagTxtModFormAsMacroPCL);

            ToolMiscSamplesPersist.saveDataTypeTxtMod (
                "PCLXL",
                (Int32)_indxPatternTypePCLXL,
                _flagTxtModFormAsMacroPCLXL);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b T x t M o d T y p e C h r _ C l i c k                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Radio button selecting 'Character enhancement' text modification.  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbTxtModTypeChr_Click (object sender,
                                            RoutedEventArgs e)
        {
            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
                _indxTxtModTypePCL = eTxtModType.Chr;
            else
                _indxTxtModTypePCLXL = eTxtModType.Chr;

            initialiseDescTxtMod();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b T x t M o d T y p e P a t _ C l i c k                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Radio button selecting 'Text & background' text modification.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbTxtModTypePat_Click (object sender,
                                            RoutedEventArgs e)
        {
            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
                _indxTxtModTypePCL = eTxtModType.Pat;
            else
                _indxTxtModTypePCLXL = eTxtModType.Pat;

            initialiseDescTxtMod();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r b T x t M o d T y p e R o t _ C l i c k                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Radio button selecting 'Rotation' text modification.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void rbTxtModTypeRot_Click (object sender,
                                            RoutedEventArgs e)
        {
            if (_crntPDL == ToolCommonData.ePrintLang.PCL)
                _indxTxtModTypePCL = eTxtModType.Rot;
            else
                _indxTxtModTypePCLXL = eTxtModType.Rot;

            initialiseDescTxtMod();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t F l a g T x t M o d F o r m A s M a c r o                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set or unset 'Render fixed text as overlay' flag.                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void setFlagTxtModFormAsMacro (
            Boolean setFlag,
            ToolCommonData.ePrintLang crntPDL)
        {
            if (crntPDL == ToolCommonData.ePrintLang.PCL)
            {
                if (setFlag)
                    _flagTxtModFormAsMacroPCL = true;
                else
                    _flagTxtModFormAsMacroPCL = false;
            }
            else if (crntPDL == ToolCommonData.ePrintLang.PCLXL)
            {
                if (setFlag)
                    _flagTxtModFormAsMacroPCLXL = true;
                else
                    _flagTxtModFormAsMacroPCLXL = false;
            }
        }
    }
}
