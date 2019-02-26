using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides common Tool data.
    /// 
    /// © Chris Hutchinson 2014
    /// 
    /// </summary>

    [System.Reflection.ObfuscationAttribute(Feature = "renaming",
                                            ApplyToMembers = true)]

    public static class ToolCommonData
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Form (and tool) identifiers, etc.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        [System.Reflection.ObfuscationAttribute(Exclude = true)]

        public enum eToolIds : Byte
        {
            Min,
            FontSample,
            FormSample,
            ImageBitmap,
            MakeOverlay,
            MiscSamples,
            PatternGenerate,
            PrintArea,
            PrintLang,
            PrnAnalyse,
            PrnPrint,
            SoftFontGenerate,
            StatusReadback,
            SymbolSetGenerate,
            TrayMap,
            Max,
            XXXDiags
        }

        [System.Reflection.ObfuscationAttribute(Exclude = true)]

        public enum eToolSubIds : Byte
        {
            None,
                            // used by ToolPrintLang:
            PCL,            //
            HPGL2,          //
            PCLXLTags,      //
            PCLXLEnums,     //
            PJLCmds,        //
            PMLTags,        //
            SymbolSets,     //
            Fonts,          //
            PaperSizes,     //
            PrescribeCmds,  //
                            // used by ToolMiscSamples:
            Colour,         //
            LogOper,        //
            LogPage,        //
            Pattern,        //
            TxtMod,         //
            Unicode         //
        }

        public enum ePrintLang
        {
            PCL = 0,
            PCLXL,
            HPGL2,
            PJL,
            PostScript,
            PML,
            XL2HB,
            PCL3GUI,
            Prescribe,
            Unknown
        }

        static String _tmpFolder = Environment.GetEnvironmentVariable ("TMP");

        static String _defWorkFolder = _tmpFolder;

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // D e f W o r k F o l d e r                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Get the default work folder name.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String DefWorkFolder
        {
            get { return _defWorkFolder; }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d W o r k F o l d e r N a m e                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Load default work folder name from registry.                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadWorkFoldername ()
        {
            TargetPersist.loadDataWorkFolder (ref _defWorkFolder);
        }
    }
}
