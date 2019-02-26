using System;
using System.Reflection;
using System.Windows.Media;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class manages options for the AnalysePRN tool.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    public class PrnParseOptions
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private String _prnFilename = ""; // TEMPORARY ????????????????????????????????

        private PropertyInfo[] _stdClrsPropertyInfo;

        private PrnParseConstants.eOptCharSets _indxCharSetName;
        private PrnParseConstants.eOptCharSetSubActs _indxCharSetSubAct;
        private Int32 _valCharSetSubCode;

        private PrnParseConstants.eOptOffsetFormats _indxGenOffsetFormat;
        
        private PrnParseConstants.eOptOffsetFormats _indxCurFOffsetFormat;

        private PrnParseConstants.eOptStatsLevel _indxStatsLevel;
        
        private ToolCommonData.ePrintLang _indxCurFInitLang;

        private PrnParseConstants.ePCLXLBinding _indxCurFXLBinding;

        private Int32 _ctClrMapStdClrs;

        private Int32[] _indxClrMapBack;
        private Int32[] _indxClrMapFore;

        private Int32 _valCurFOffsetStart;
        private Int32 _valCurFOffsetEnd;
        private Int32 _valCurFOffsetMax;

        private Int32 _valPCLFontDrawHeight;
        private Int32 _valPCLFontDrawWidth;

        private Int32 _valPCLXLFontDrawHeight;
        private Int32 _valPCLXLFontDrawWidth;

        private Boolean _flagClrMapUseClr;

        private Boolean _flagGenMiscAutoAnalyse;
        private Boolean _flagGenDiagFileAccess;

        private Boolean _flagHPGL2MiscBinData;

        private Boolean _flagPCLFontHddr;
        private Boolean _flagPCLFontChar;
        private Boolean _flagPCLFontDraw;
        private Boolean _flagPCLMacroDisplay;
        private Boolean _flagPCLMiscStyleData;
        private Boolean _flagPCLMiscBinData;

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

        private Boolean _flagPCLXLFontHddr;
        private Boolean _flagPCLXLFontChar;
        private Boolean _flagPCLXLFontDraw;
        private Boolean _flagPCLXLEncPCLFontSelect;
        private Boolean _flagPCLXLEncPCLPassThrough;
        private Boolean _flagPCLXLEncUserStream;
        private Boolean _flagPCLXLMiscBinData;
        private Boolean _flagPCLXLMiscOperPos;
        private Boolean _flagPCLXLMiscVerbose;

        private Boolean _flagPMLMiscVerbose;
        private Boolean _flagPMLWithinPCL;
        private Boolean _flagPMLWithinPJL;

        private Boolean _flagStatsExcUnusedPCLObs;
        private Boolean _flagStatsExcUnusedPCLXLRes;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P r n P a r s e O p t i o n s                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PrnParseOptions()
        {
            Int32 ctRowTypes = PrnParseRowTypes.getCount ();

            _indxClrMapBack = new Int32[ctRowTypes];
            _indxClrMapFore = new Int32[ctRowTypes];

            metricsLoad ();
        }

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P r n P a r s e O p t i o n s                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PrnParseOptions(PrnParseOptions parent)
        {
            Int32 ctRowTypes = PrnParseRowTypes.getCount ();

            _indxClrMapBack = new Int32[ctRowTypes];
            _indxClrMapFore = new Int32[ctRowTypes];

            parent.getOptCharSet (ref _indxCharSetName,
                                  ref _indxCharSetSubAct,
                                  ref _valCharSetSubCode);

            parent.getOptClrMap (ref _flagClrMapUseClr,
                                 ref _indxClrMapBack,
                                 ref _indxClrMapFore);

            parent.getOptClrMapStdClrs (ref _ctClrMapStdClrs,
                                        ref _stdClrsPropertyInfo);
            
            parent.getOptCurF (ref _indxCurFInitLang,
                               ref _indxCurFXLBinding,
                               ref _indxCurFOffsetFormat,
                               ref _valCurFOffsetStart,
                               ref _valCurFOffsetEnd,
                               ref _valCurFOffsetMax);
            
            parent.getOptGeneral (ref _indxGenOffsetFormat,
                                  ref _flagGenMiscAutoAnalyse,
                                  ref _flagGenDiagFileAccess);

            parent.getOptHPGL2 (ref _flagHPGL2MiscBinData);

            parent.getOptPCL (ref _flagPCLFontHddr,
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

            parent.getOptPCLXL (ref _flagPCLXLFontHddr,
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

            parent.getOptPML (ref _flagPMLWithinPCL,
                              ref _flagPMLWithinPJL,
                              ref _flagPMLMiscVerbose);
            
            parent.getOptStats (ref _indxStatsLevel,
                                ref _flagStatsExcUnusedPCLObs,
                                ref _flagStatsExcUnusedPCLXLRes);
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g C l r M a p U s e C l r                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagClrMapUseClr
        {
            get { return _flagClrMapUseClr; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g G e n D i a g F i l e A c c e s s                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagGenDiagFileAccess
        {
            get { return _flagGenDiagFileAccess; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g G e n M i s c A u t o A n a l y s e                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagGenMiscAutoAnalyse
        {
            get { return _flagGenMiscAutoAnalyse; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g P C L M i s c B i n D a t a                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagPCLMiscBinData
        {
            get { return _flagPCLMiscBinData; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g P C L X L M i s c B i n D a t a                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagPCLXLMiscBinData
        {
            get { return _flagPCLXLMiscBinData; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g P M L W i t h i n P C L                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagPMLWithinPCL
        {
            get { return _flagPMLWithinPCL; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g P M L W i t h i n P J L                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagPMLWithinPJL
        {
            get { return _flagPMLWithinPJL; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g P M L V e r b o s e                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagPMLVerbose
        {
            get { return _flagPMLMiscVerbose; }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t O p t C h a r S e t                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return current 'Character Set' options.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void getOptCharSet(
            ref PrnParseConstants.eOptCharSets indxName,
            ref PrnParseConstants.eOptCharSetSubActs indxSubAct,
            ref Int32 valSubCode)
        {
            indxName       = _indxCharSetName;
            indxSubAct     = _indxCharSetSubAct;
            valSubCode     = _valCharSetSubCode;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t O p t C l r M a p                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return current 'Colour Map' options.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void getOptClrMap (ref Boolean flagClrMapUseClr,
                                  ref Int32[] indxClrMapBack,
                                  ref Int32[] indxClrMapFore)
        {
            Int32 ctRowTypes = PrnParseRowTypes.getCount ();

            flagClrMapUseClr = _flagClrMapUseClr;

            for (Int32 i = 0; i < ctRowTypes; i++)
            {
                indxClrMapBack[i] = _indxClrMapBack[i];
                indxClrMapFore[i] = _indxClrMapFore[i];
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t O p t C l r M a p                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return current 'Colour Map' options.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void getOptClrMapStdClrs (
            ref Int32           ctClrMapStdClrs,
            ref PropertyInfo [] stdClrsPropertyInfo)
        {
            ctClrMapStdClrs     = _ctClrMapStdClrs;
            stdClrsPropertyInfo = _stdClrsPropertyInfo;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t O p t C u r F                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return current 'Current File' options.                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void getOptCurF(
            ref ToolCommonData.ePrintLang indxInitLang,
            ref PrnParseConstants.ePCLXLBinding indxXLBinding,
            ref PrnParseConstants.eOptOffsetFormats indxOffsetFormat,
            ref Int32 valOffsetStart,
            ref Int32 valOffsetEnd,
            ref Int32 valOffsetMax)
        {
            indxInitLang     = _indxCurFInitLang;
            indxXLBinding    = _indxCurFXLBinding;
            indxOffsetFormat = _indxCurFOffsetFormat;
            valOffsetStart   = _valCurFOffsetStart;
            valOffsetEnd     = _valCurFOffsetEnd;
            valOffsetMax     = _valCurFOffsetMax;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t O p t C u r F B a s i c                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return basic 'Current File' options.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void getOptCurFBasic(
            ref ToolCommonData.ePrintLang indxInitLang,
            ref PrnParseConstants.ePCLXLBinding indxXLBinding,
            ref Int32 valOffsetStart,
            ref Int32 valOffsetEnd)
        {
            indxInitLang   = _indxCurFInitLang;
            indxXLBinding  = _indxCurFXLBinding;
            valOffsetStart = _valCurFOffsetStart;
            valOffsetEnd   = _valCurFOffsetEnd;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t O p t C u r F O f f s e t s                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return 'Current File' offset options.                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void getOptCurFOffsets(
            ref Int32 valOffsetStart,
            ref Int32 valOffsetEnd)
        {
            valOffsetStart = _valCurFOffsetStart;
            valOffsetEnd = _valCurFOffsetEnd;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t O p t G e n e r a l                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return current 'General' options.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void getOptGeneral(
            ref PrnParseConstants.eOptOffsetFormats indxOffsetFormat,
            ref Boolean flagAutoAnalyse,
            ref Boolean flagDiagFileAccess)
        {
            indxOffsetFormat  = _indxGenOffsetFormat;
            flagAutoAnalyse   = _flagGenMiscAutoAnalyse;
            flagDiagFileAccess = _flagGenDiagFileAccess;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t O p t H P G L 2                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return current 'HP-GL/2' options.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void getOptHPGL2(ref Boolean flagMiscBinData)
        {
            flagMiscBinData = _flagHPGL2MiscBinData;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t O p t P C L                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return current 'PCL' options.                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void getOptPCL(ref Boolean flagFontHddr,
                              ref Boolean flagFontChar,
                              ref Boolean flagFontDraw,
                              ref Int32   valFontDrawHeight,
                              ref Int32   valFontDrawWidth,
                              ref Boolean flagMacroDisplay,
                              ref Boolean flagMiscStyleData,
                              ref Boolean flagMiscBinData,
                              ref Boolean flagTransAlphaNumId,
                              ref Boolean flagTransColourLookup,
                              ref Boolean flagTransConfIO,
                              ref Boolean flagTransConfImageData,
                              ref Boolean flagTransConfRasterData,
                              ref Boolean flagTransDefLogPage,
                              ref Boolean flagTransDefSymSet,
                              ref Boolean flagTransDitherMatrix,
                              ref Boolean flagTransDriverConf,
                              ref Boolean flagTransEscEncText,
                              ref Boolean flagTransPaletteConf,
                              ref Boolean flagTransUserPattern,
                              ref Boolean flagTransViewIlluminant)
        {
            flagFontHddr        = _flagPCLFontHddr;
            flagFontChar        = _flagPCLFontChar;
            flagFontDraw        = _flagPCLFontDraw;

            valFontDrawHeight   = _valPCLFontDrawHeight;
            valFontDrawWidth    = _valPCLFontDrawWidth;

            flagMacroDisplay    = _flagPCLMacroDisplay;
            
            flagMiscStyleData   = _flagPCLMiscStyleData;
            flagMiscBinData     = _flagPCLMiscBinData;

            flagTransAlphaNumId     = _flagPCLTransAlphaNumId;
            flagTransColourLookup   = _flagPCLTransColourLookup;
            flagTransConfIO         = _flagPCLTransConfIO;
            flagTransConfImageData  = _flagPCLTransConfImageData;
            flagTransConfRasterData = _flagPCLTransConfRasterData;
            flagTransDefLogPage     = _flagPCLTransDefLogPage;
            flagTransDefSymSet      = _flagPCLTransDefSymSet;
            flagTransDitherMatrix   = _flagPCLTransDitherMatrix;
            flagTransDriverConf     = _flagPCLTransDriverConf;
            flagTransEscEncText     = _flagPCLTransEscEncText;
            flagTransPaletteConf    = _flagPCLTransPaletteConf;
            flagTransUserPattern    = _flagPCLTransUserPattern;
            flagTransViewIlluminant = _flagPCLTransViewIlluminant;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t O p t P C L B a s i c                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return current 'PCL' basic options.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void getOptPCLBasic(ref Boolean flagFontHddr,
                                   ref Boolean flagFontChar,
                                   ref Boolean flagMacroDisplay,
                                   ref Boolean flagMiscStyleData,
                                   ref Boolean flagMiscBinData,
                                   ref Boolean flagTransAlphaNumId,
                                   ref Boolean flagTransColourLookup,
                                   ref Boolean flagTransConfIO,
                                   ref Boolean flagTransConfImageData,
                                   ref Boolean flagTransConfRasterData,
                                   ref Boolean flagTransDefLogPage,
                                   ref Boolean flagTransDefSymSet,
                                   ref Boolean flagTransDitherMatrix,
                                   ref Boolean flagTransDriverConf,
                                   ref Boolean flagTransEscEncText,
                                   ref Boolean flagTransPaletteConf,
                                   ref Boolean flagTransUserPattern,
                                   ref Boolean flagTransViewIlluminant)
        {
            flagFontHddr = _flagPCLFontHddr;
            flagFontChar = _flagPCLFontChar;

            flagMacroDisplay    = _flagPCLMacroDisplay;

            flagMiscStyleData   = _flagPCLMiscStyleData;
            flagMiscBinData     = _flagPCLMiscBinData;

            flagTransAlphaNumId     = _flagPCLTransAlphaNumId;
            flagTransColourLookup   = _flagPCLTransColourLookup;
            flagTransConfIO         = _flagPCLTransConfIO;
            flagTransConfImageData  = _flagPCLTransConfImageData;
            flagTransConfRasterData = _flagPCLTransConfRasterData;
            flagTransDefLogPage     = _flagPCLTransDefLogPage;
            flagTransDefSymSet      = _flagPCLTransDefSymSet;
            flagTransDitherMatrix   = _flagPCLTransDitherMatrix;
            flagTransDriverConf     = _flagPCLTransDriverConf;
            flagTransEscEncText     = _flagPCLTransEscEncText;
            flagTransPaletteConf    = _flagPCLTransPaletteConf;
            flagTransUserPattern    = _flagPCLTransUserPattern;
            flagTransViewIlluminant = _flagPCLTransViewIlluminant;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t O p t P C L D r a w                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return current 'PCL' font character draw options.                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void getOptPCLDraw(ref Boolean flagFontDraw,
                                  ref Int32 valFontDrawHeight,
                                  ref Int32 valFontDrawWidth)
        {
            flagFontDraw = _flagPCLFontDraw;

            valFontDrawHeight = _valPCLFontDrawHeight;
            valFontDrawWidth = _valPCLFontDrawWidth;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t O p t P C L X L                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return current 'PCL XL' options.                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void getOptPCLXL(ref Boolean flagFontHddr,
                                ref Boolean flagFontChar,
                                ref Boolean flagFontDraw,
                                ref Int32   valFontDrawHeight,
                                ref Int32   valFontDrawWidth,
                                ref Boolean flagEncUserStream,
                                ref Boolean flagEncPCLPassThrough,
                                ref Boolean flagEncPCLFontSelect,
                                ref Boolean flagMiscOperPos,
                                ref Boolean flagMiscBinData,
                                ref Boolean flagMiscVerbose)
        {
            flagFontHddr      = _flagPCLXLFontHddr;
            flagFontChar      = _flagPCLXLFontChar;
            flagFontDraw  = _flagPCLXLFontDraw;

            valFontDrawHeight = _valPCLXLFontDrawHeight;
            valFontDrawWidth  = _valPCLXLFontDrawWidth;

            flagEncUserStream = _flagPCLXLEncUserStream;
            flagEncPCLPassThrough = _flagPCLXLEncPCLPassThrough;
            flagEncPCLFontSelect  = _flagPCLXLEncPCLFontSelect;

            flagMiscOperPos       = _flagPCLXLMiscOperPos;
            flagMiscBinData       = _flagPCLXLMiscBinData;
            flagMiscVerbose       = _flagPCLXLMiscVerbose;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t O p t P C L X L B a s i c                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return current 'PCL XL' basic options.                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void getOptPCLXLBasic(ref Boolean flagFontHddr,
                                     ref Boolean flagFontChar,
                                     ref Boolean flagEncUserStream,
                                     ref Boolean flagEncPCLPassThrough,
                                     ref Boolean flagEncPCLFontSelect,
                                     ref Boolean flagMiscOperPos,
                                     ref Boolean flagMiscBinData,
                                     ref Boolean flagMiscVerbose)
        {
            flagFontHddr = _flagPCLXLFontHddr;
            flagFontChar = _flagPCLXLFontChar;

            flagEncUserStream = _flagPCLXLEncUserStream;
            flagEncPCLPassThrough = _flagPCLXLEncPCLPassThrough;
            flagEncPCLFontSelect = _flagPCLXLEncPCLFontSelect;

            flagMiscOperPos = _flagPCLXLMiscOperPos;
            flagMiscBinData = _flagPCLXLMiscBinData;
            flagMiscVerbose = _flagPCLXLMiscVerbose;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t O p t P C L X L D r a w                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return current 'PCL XL' font character draw options.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void getOptPCLXLDraw (ref Boolean flagFontDraw,
                                     ref Int32 valFontDrawHeight,
                                     ref Int32 valFontDrawWidth)
        {
            flagFontDraw = _flagPCLXLFontDraw;

            valFontDrawHeight = _valPCLXLFontDrawHeight;
            valFontDrawWidth = _valPCLXLFontDrawWidth;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t O p t P M L                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return current 'PML' options.                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void getOptPML(ref Boolean flagWithinPCL,
                              ref Boolean flagWithinPJL,
                              ref Boolean flagMiscVerbose)
        {
            flagWithinPCL   = _flagPMLWithinPCL;
            flagWithinPJL   = _flagPMLWithinPJL;
            flagMiscVerbose = _flagPMLMiscVerbose;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t O p t S t a t s                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return current 'Statistics' options.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void getOptStats(
            ref PrnParseConstants.eOptStatsLevel indxLevel,
            ref Boolean                          flagExcUnusedPCLObs,
            ref Boolean                          flagExcUnusedPCLXLRes)
        {
            indxLevel = _indxStatsLevel;
            flagExcUnusedPCLObs   = _flagStatsExcUnusedPCLObs;
            flagExcUnusedPCLXLRes = _flagStatsExcUnusedPCLXLRes;
        }


        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // I n d x G e n O f f s e t F o r m a t                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PrnParseConstants.eOptOffsetFormats IndxGenOffsetFormat
        {
            get { return _indxGenOffsetFormat; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // I n d x C u r F X L B i n d i n g                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PrnParseConstants.ePCLXLBinding IndxCurFXLBinding
        {
            get { return _indxCurFXLBinding; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // I n d x S t a t s L e v e l                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PrnParseConstants.eOptStatsLevel IndxStatsLevel
        {
            get { return _indxStatsLevel; }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m e t r i c s L o a d                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Load metrics from persistent storage.                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void metricsLoad()
        {
            ToolPrnAnalysePersist.loadData (ref _prnFilename);

            metricsLoadCharSet ();
            metricsLoadClrMap ();
            metricsLoadGen ();
            metricsLoadHPGL2 ();
            metricsLoadPCL ();
            metricsLoadPCLXL ();
            metricsLoadPML ();
            metricsLoadStats ();

            metricsLoadCurF ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m e t r i c s L o a d C h a r S e t                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Load 'Character Set' option metrics from persistent storage.       //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void metricsLoadCharSet()
        {
            Int32 i1 = 0,
                  i2 = 0,
                  i3 = 0,
                  max;

            //----------------------------------------------------------------//

            ToolPrnAnalysePersist.loadOptCharSet(ref i1,
                                                 ref i2,
                                                 ref i3);

            max = (Int32) PrnParseConstants.eOptCharSets.Max;

            if ((i1 < 0) || (i1 >= max))
                i1 = (Int32) PrnParseConstants.eOptCharSets.ISO_8859_1;

            _indxCharSetName = (PrnParseConstants.eOptCharSets) i1;

            max = (Int32) PrnParseConstants.eOptCharSetSubActs.Max;

            if ((i2 < 0) || (i2 >= max))
                i2 = (Int32) PrnParseConstants.eOptCharSetSubActs.Hex;

            _indxCharSetSubAct = (PrnParseConstants.eOptCharSetSubActs) i2;

            if ((i3  < PrnParseConstants.asciiSpace)    ||
                (i3 == PrnParseConstants.asciiDEL)      ||
                (i3  > PrnParseConstants.asciiMax8bit))
                i3 = PrnParseConstants.asciiSubDefault;

            _valCharSetSubCode = i3;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m e t r i c s L o a d C l r M a p                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Load 'colour map' option metrics from persistent storage.          //
        //                                                                    //
        // Set the arrays to the application default values first, so that    //
        // these values (which are passed by reference to the subsequent      //
        // 'load from registry' function) are used as defaults if the         //
        // registry items do not yet exist (i.e. on first run of the version  //
        // which introduced the colour coding feature).                       // 
        //                                                                    //
        //--------------------------------------------------------------------//

        private void metricsLoadClrMap()
        {
            PrnParseRowTypes.setDefaultClrs (ref _indxClrMapBack,
                                             ref _indxClrMapFore);

            ToolPrnAnalysePersist.loadOptClrMap (ref _flagClrMapUseClr);

            ToolPrnAnalysePersist.loadOptClrMapCrnt (ref _indxClrMapBack,
                                                     ref _indxClrMapFore);

            //----------------------------------------------------------------//
            //                                                                //
            // Get the properties of the standard 'Colors' class object.      //
            //                                                                //
            // We should be provided with a list of the 140 standard colours  //
            // (as defined in .net, Unix X11, etc.) plus the 'transparent'    //
            // colour.                                                        //
            // This list is unlikely to change, so we can store the indices   //
            // of the selected colours and be relatively confident that these //
            // will always refer to the same actual colours.                  //
            //                                                                //
            //----------------------------------------------------------------//

            _stdClrsPropertyInfo = typeof (Colors).GetProperties ();

            _ctClrMapStdClrs = _stdClrsPropertyInfo.Length;   // length = 141 //
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m e t r i c s L o a d C u r F                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Load 'Current file' option metrics (default values).               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void metricsLoadCurF()
        {
            resetOptCurF (-1);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m e t r i c s L o a d G e n                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Load 'General' option metrics from persistent storage.             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void metricsLoadGen()
        {
            Int32 i1 = 0,
                  max;

            //----------------------------------------------------------------//

            ToolPrnAnalysePersist.loadOptGeneral (ref i1,
                                                  ref _flagGenMiscAutoAnalyse,
                                                  ref _flagGenDiagFileAccess);

            max = (Int32) PrnParseConstants.eOptOffsetFormats.Max;

            if ((i1 < 0) || (i1 >= max))
                i1 = (Int32) PrnParseConstants.eOptOffsetFormats.Decimal;

            _indxGenOffsetFormat = (PrnParseConstants.eOptOffsetFormats) i1;

        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m e t r i c s L o a d H P G L 2                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Load 'HP-GL/2' option metrics from persistent storage.             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void metricsLoadHPGL2()
        {
            ToolPrnAnalysePersist.loadOptHPGL2 (ref _flagHPGL2MiscBinData);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m e t r i c s L o a d P C L                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Load metrics from persistent storage.                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void metricsLoadPCL()
        {
            ToolPrnAnalysePersist.loadOptPCL (ref _flagPCLFontHddr,
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
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m e t r i c s L o a d P C L X L                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Load 'PCL XL' option metrics from persistent storage.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void metricsLoadPCLXL()
        {
            ToolPrnAnalysePersist.loadOptPCLXL (ref _flagPCLXLFontHddr,
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
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m e t r i c s L o a d P M L                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Load 'PML' option metrics from persistent storage.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void metricsLoadPML()
        {
            ToolPrnAnalysePersist.loadOptPML (ref _flagPMLWithinPCL,
                                              ref _flagPMLWithinPJL,
                                              ref _flagPMLMiscVerbose);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m e t r i c s L o a d S t a t s                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Load 'General' option metrics from persistent storage.             //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void metricsLoadStats ()
        {
            Int32 i1 = 0,
                  max;

            //----------------------------------------------------------------//

            ToolPrnAnalysePersist.loadOptStats (
                ref i1,
                ref _flagStatsExcUnusedPCLObs,
                ref _flagStatsExcUnusedPCLXLRes);

            max = (Int32) PrnParseConstants.eOptStatsLevel.Max;

            if ((i1 < 0) || (i1 >= max))
                i1 = (Int32) PrnParseConstants.eOptStatsLevel.ReferencedOnly;

            _indxStatsLevel = (PrnParseConstants.eOptStatsLevel) i1;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m e t r i c s S a v e                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Save metrics to persistent storage.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void metricsSave()
        {
            ToolPrnAnalysePersist.saveOptCharSet (
                (Int32) _indxCharSetName,
                (Int32) _indxCharSetSubAct,
                        _valCharSetSubCode);

            //----------------------------------------------------------------//

            ToolPrnAnalysePersist.saveOptClrMap (_flagClrMapUseClr);

            ToolPrnAnalysePersist.saveOptClrMapCrnt (_indxClrMapBack,
                                                     _indxClrMapFore);

            //----------------------------------------------------------------//

            ToolPrnAnalysePersist.saveOptGeneral (
                (Int32) _indxGenOffsetFormat,
                        _flagGenMiscAutoAnalyse,
                        _flagGenDiagFileAccess);

            //----------------------------------------------------------------//

            ToolPrnAnalysePersist.saveOptHPGL2 (_flagHPGL2MiscBinData);

            //----------------------------------------------------------------//

            ToolPrnAnalysePersist.saveOptPCL (_flagPCLFontHddr,
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

            //----------------------------------------------------------------//

            ToolPrnAnalysePersist.saveOptPCLXL (_flagPCLXLFontHddr,
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

            //----------------------------------------------------------------//

            ToolPrnAnalysePersist.saveOptPML (_flagPMLWithinPCL,
                                              _flagPMLWithinPJL,
                                              _flagPMLMiscVerbose);

            //----------------------------------------------------------------//

            ToolPrnAnalysePersist.saveOptStats ((Int32) _indxStatsLevel,
                                                _flagStatsExcUnusedPCLObs,
                                                _flagStatsExcUnusedPCLXLRes);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e s e t O p t C u r F                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Reset 'Current File' options to defaults.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void resetOptCurF(Int64 fileSize)
        {
            _indxCurFInitLang = ToolCommonData.ePrintLang.PCL;
            _indxCurFXLBinding = PrnParseConstants.ePCLXLBinding.Unknown;
            _indxCurFOffsetFormat = _indxGenOffsetFormat;

            _valCurFOffsetMax = (Int32) fileSize;

            _valCurFOffsetStart = 0;
            _valCurFOffsetEnd   = -1;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t O p t C h a r S e t                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set current 'Character Set' options.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void setOptCharSet(
            PrnParseConstants.eOptCharSets indxName,
            PrnParseConstants.eOptCharSetSubActs indxSubAct,
            Int32 valSubCode)
        {
            _indxCharSetName       = indxName;
            _indxCharSetSubAct     = indxSubAct;
            _valCharSetSubCode = valSubCode;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t O p t C l r M a p                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return current 'Colour Map' options.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void setOptClrMap (Boolean flagClrMapUseClr,
                                  Int32[] indxClrMapBack,
                                  Int32[] indxClrMapFore)
        {
            _flagClrMapUseClr = flagClrMapUseClr;
            _indxClrMapBack   = indxClrMapBack;
            _indxClrMapFore   = indxClrMapFore;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t O p t C u r F                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set current 'Current File' options.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void setOptCurF(
            ToolCommonData.ePrintLang indxInitLang,
            PrnParseConstants.ePCLXLBinding indxXLBinding,
            PrnParseConstants.eOptOffsetFormats indxOffsetFormat,
            Int32 valOffsetStart,
            Int32 valOffsetEnd)
        {
            _indxCurFInitLang     = indxInitLang;
            _indxCurFXLBinding    = indxXLBinding;
            _indxCurFOffsetFormat = indxOffsetFormat;
            _valCurFOffsetStart   = valOffsetStart;
            _valCurFOffsetEnd     = valOffsetEnd;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t O p t G e n e r a l                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set current 'General' options.                                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void setOptGeneral(
            PrnParseConstants.eOptOffsetFormats indxOffsetFormat,
            Boolean flagMiscAutoAnalyse,
            Boolean flagDiagFileAccess)
        {
            _indxGenOffsetFormat = indxOffsetFormat;
            _flagGenMiscAutoAnalyse  = flagMiscAutoAnalyse;
            _flagGenDiagFileAccess  = flagDiagFileAccess;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t O p t H P G L 2                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set current 'HP-GL/2' options.                                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void setOptHPGL2(Boolean flagMiscBinData)
        {
            _flagHPGL2MiscBinData = flagMiscBinData;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t O p t P C L                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set current 'PCL' options.                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void setOptPCL(Boolean flagFontHddr,
                              Boolean flagFontChar,
                              Boolean flagFontDraw,
                              Int32   valFontDrawHeight,
                              Int32   valFontDrawWidth,
                              Boolean flagMacroDisplay,
                              Boolean flagMiscStyleData,
                              Boolean flagMiscBinData,
                              Boolean flagTransAlphaNumId,
                              Boolean flagTransColourLookup,
                              Boolean flagTransConfIO,
                              Boolean flagTransConfImageData,
                              Boolean flagTransConfRasterData,
                              Boolean flagTransDefLogPage,
                              Boolean flagTransDefSymSet,
                              Boolean flagTransDitherMatrix,
                              Boolean flagTransDriverConf,
                              Boolean flagTransEscEncText,
                              Boolean flagTransPaletteConf,
                              Boolean flagTransUserPattern,
                              Boolean flagTransViewIlluminant)
        {
            _flagPCLFontHddr        = flagFontHddr;
            _flagPCLFontChar        = flagFontChar;
            _flagPCLFontDraw        = flagFontDraw;

            _valPCLFontDrawHeight   = valFontDrawHeight;
            _valPCLFontDrawWidth    = valFontDrawWidth;

            _flagPCLMacroDisplay    = flagMacroDisplay;

            _flagPCLMiscStyleData   = flagMiscStyleData;
            _flagPCLMiscBinData     = flagMiscBinData;

            _flagPCLTransAlphaNumId     = flagTransAlphaNumId;
            _flagPCLTransColourLookup   = flagTransColourLookup;
            _flagPCLTransConfIO         = flagTransConfIO;
            _flagPCLTransConfImageData  = flagTransConfImageData;
            _flagPCLTransConfRasterData = flagTransConfRasterData;
            _flagPCLTransDefLogPage     = flagTransDefLogPage;
            _flagPCLTransDefSymSet      = flagTransDefSymSet;
            _flagPCLTransDitherMatrix   = flagTransDitherMatrix;
            _flagPCLTransDriverConf     = flagTransDriverConf;
            _flagPCLTransEscEncText     = flagTransEscEncText;
            _flagPCLTransPaletteConf    = flagTransPaletteConf;
            _flagPCLTransUserPattern    = flagTransUserPattern;
            _flagPCLTransViewIlluminant = flagTransViewIlluminant;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t O p t P C L X L                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set current 'PCL XL' options.                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void setOptPCLXL(Boolean flagFontHddr,
                                Boolean flagFontChar,
                                Boolean flagFontDraw,
                                Int32   valFontDrawHeight,
                                Int32   valFontDrawWidth,
                                Boolean flagEncUserStream,
                                Boolean flagEncPCLPassThrough,
                                Boolean flagEncPCLFontSelect,
                                Boolean flagMiscOperPos,
                                Boolean flagMiscBinData,
                                Boolean flagMiscVerbose)
        {
            _flagPCLXLFontHddr      = flagFontHddr;
            _flagPCLXLFontChar      = flagFontChar;
            _flagPCLXLFontDraw  = flagFontDraw;

            _valPCLXLFontDrawHeight = valFontDrawHeight;
            _valPCLXLFontDrawWidth  = valFontDrawWidth;

            _flagPCLXLEncUserStream     = flagEncUserStream;
            _flagPCLXLEncPCLPassThrough = flagEncPCLPassThrough;
            _flagPCLXLEncPCLFontSelect  = flagEncPCLFontSelect;

            _flagPCLXLMiscOperPos       = flagMiscOperPos;
            _flagPCLXLMiscBinData       = flagMiscBinData;
            _flagPCLXLMiscVerbose       = flagMiscVerbose;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t O p t P M L                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set current 'PML' options.                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void setOptPML(Boolean flagWithinPCL,
                              Boolean flagWithinPJL,
                              Boolean flagMiscVerbose)
        {
            _flagPMLWithinPCL   = flagWithinPCL;
            _flagPMLWithinPJL   = flagWithinPJL;
            _flagPMLMiscVerbose = flagMiscVerbose;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t O p t S t a t s                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set current 'Statistics' options.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void setOptStats (
            PrnParseConstants.eOptStatsLevel indxLevel,
            Boolean flagExcUnusedPCLObs,
            Boolean flagExcUnusedPCLXLRes)
        {
            _indxStatsLevel = indxLevel;
            _flagStatsExcUnusedPCLObs   = flagExcUnusedPCLObs;
            _flagStatsExcUnusedPCLXLRes = flagExcUnusedPCLXLRes;
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // V a l C u r F O f f s e t E n d                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int32 ValCurFOffsetEnd
        {
            get { return _valCurFOffsetEnd; }
        }
    }
}
