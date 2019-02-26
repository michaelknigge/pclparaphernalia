using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class handles 'link' data for 'parsing' of print file.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    class PrnParseLinkData
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

        private PrnParseConstants.eContType _contType;
        private PrnParseConstants.eOvlAct _makeOvlAct;
        private PrnParseConstants.eOvlPos _makeOvlPos;
        private PrnParseConstants.eOvlShow _makeOvlShow;

        private PrnParse _analysisOwner;

        private Int32 _analysisLevel;
        private Int32 _macroLevel;

        private Int64 _fileSize;
        private Int64 _makeOvlOffset;
        private Int64 _makeOvlSkipBegin;
        private Int64 _makeOvlSkipEnd;

        private Int64 _pclComboStart;

        private Int32 _makeOvlMacroId;

        private String _makeOvlStreamName;

        private PCLXLOperators.eEmbedDataType _pclxlEmbedType; 

        private Int32 _prefixLen;
        private Int32 _dataLen;
        private Int32 _downloadRem;

        private Int32 _entryCt;
        private Int32 _entryNo;
        private Int32 _entryRem;
        private Int32 _entrySz1;
        private Int32 _entrySz2;
   
        private Boolean _pclComboSeq;
        private Boolean _pclComboFirst;
        private Boolean _pclComboLast;
        private Boolean _pclComboModified;

        private Boolean _makeOvlPageMark;
        private Boolean _makeOvlXL;
        private Boolean _makeOvlEncapsulate;
        private Boolean _makeOvlRestoreStateXL;

        private Boolean _backTrack;
        private Boolean _eof;
        
        private Byte _prefixA;
        private Byte _prefixB;

        private Byte _prescribeSCRC;
        private Boolean _prescribeIntroRead;
        private ToolCommonData.ePrintLang _prescribeCallerPDL;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P r n P a r s e L i n k D a t a                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PrnParseLinkData(
            PrnParse analysisOwner,
            Int32    analysisLevel,
            Int32    macroLevel,
            PCLXLOperators.eEmbedDataType pclxlEmbedType)
        {
            _analysisOwner         = analysisOwner;
            _analysisLevel         = analysisLevel;
            _macroLevel            = macroLevel;
            _pclxlEmbedType        = pclxlEmbedType;

            _contType              = PrnParseConstants.eContType.None;
            _prefixLen             = 0;
            _dataLen               = 0;
            _downloadRem           = 0;

            _entryCt               = 0;
            _entryNo               = 0;
            _entryRem              = 0;
            _entrySz1              = 0;
            _entrySz2              = 0;
            
            _backTrack             = false;
            _prefixA               = 0x00;
            _prefixB               = 0x00;
            
            _eof                   = false;

            _fileSize              = 0;
            
            _makeOvlOffset         = 0;
            _makeOvlSkipBegin      = -1;
            _makeOvlSkipEnd        = -1;
            _makeOvlAct            = PrnParseConstants.eOvlAct.None;
            _makeOvlPos            = PrnParseConstants.eOvlPos.BeforeFirstPage;
            _makeOvlShow           = PrnParseConstants.eOvlShow.None;
            _makeOvlMacroId        = -1;
            _makeOvlStreamName     = "";
            _makeOvlPageMark       = false;
            _makeOvlXL             = false;
            _makeOvlEncapsulate    = false;

            _pclComboStart         = -1;
            _pclComboSeq           = false;
            _pclComboFirst         = false;
            _pclComboLast          = false;
            _pclComboModified      = false;

            _prescribeSCRC = PrnParseConstants.prescribeSCRCDefault;
            _prescribeIntroRead    = false;
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // A n a l y s i s L e v e l                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int32 AnalysisLevel
        {
            get { return _analysisLevel; }
            set { _analysisLevel = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // A n a l y s i s O w n e r                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PrnParse AnalysisOwner
        {
            get { return _analysisOwner; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // B a c k T r a c k                                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean BackTrack
        {
            get { return _backTrack; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // D a t a L e n                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return or set continuation data length.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int32 DataLen
        {
            get { return _dataLen; }
            set { _dataLen = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // E n t r y C t                                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int32 EntryCt
        {
            get { return _entryCt; }
            set { _entryCt = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // E n t r y N o                                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int32 EntryNo
        {
            get { return _entryNo; }
            set { _entryNo = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // E n t r y S z 1                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int32 EntrySz1
        {
            get { return _entrySz1; }
            set { _entrySz1 = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // E n t r y R e m                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int32 EntryRem
        {
            get { return _entryRem; }
            set { _entryRem = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // E n t r y S z 2                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int32 EntrySz2
        {
            get { return _entrySz2; }
            set { _entrySz2 = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F i l e S i z e                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int64 FileSize
        {
            get { return _fileSize; }
            set { _fileSize = value; }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C o n t D a t a                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return continuation data flags and values.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void getContData(ref PrnParseConstants.eContType contType,
                                ref Int32      prefixLen,
                                ref Int32      dataLen,
                                ref Int32      downloadRem,
                                ref Boolean    backTrack,
                                ref Byte       prefixA,
                                ref Byte       prefixB)
        {
            contType          = _contType;
            prefixLen         = _prefixLen;
            dataLen           = _dataLen;
            downloadRem       = _downloadRem;
            backTrack         = _backTrack;
            prefixA           = _prefixA;
            prefixB           = _prefixB;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C o n t T y p e                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return stored continuation type identifier.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PrnParseConstants.eContType getContType()
        {
            return _contType;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t P C L C o m b o D a t a                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return 'PCL combination sequence' data flags and values.           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void getPCLComboData(ref Boolean pclComboSeq,
                                    ref Boolean pclComboFirst,
                                    ref Boolean pclComboLast,
                                    ref Boolean pclComboModified,
                                    ref Int64   pclComboStart)
        {
            pclComboSeq      = _pclComboSeq;
            pclComboFirst    = _pclComboFirst;
            pclComboLast     = _pclComboLast;
            pclComboModified = _pclComboModified;
            pclComboStart    = _pclComboStart;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t P r e f i x D a t a                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return prefix information.                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void getPrefixData(ref Int32 prefixLen,
                                  ref Byte prefixA,
                                  ref Byte prefixB)
        {
            prefixLen = _prefixLen;
            prefixA   = _prefixA;
            prefixB   = _prefixB;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i s C o n t i n u a t i o n                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return value indicating continuation state.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean isContinuation()
        {
            if (_contType == PrnParseConstants.eContType.None)
                return false;
            else
                return true;
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // I s E o f S e t                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean IsEofSet
        {
            get { return _eof; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // M a c r o L e v e l                                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int32 MacroLevel
        {
            get { return _macroLevel; }
            set { _macroLevel = value; }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // m a c r o L e v e l A d j u s t                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Increment or decrement Macro Level.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void macroLevelAdjust(Boolean increment)
        {
            if (increment)
                _macroLevel++;
            else
                _macroLevel--;
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // M a k e O v l A c t                                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PrnParseConstants.eOvlAct MakeOvlAct
        {
            get { return _makeOvlAct; }
            set { _makeOvlAct = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // M a k e O v l E n c a p s u l a t e                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean MakeOvlEncapsulate
        {
            get { return _makeOvlEncapsulate; }
            set { _makeOvlEncapsulate = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // M a k e O v l M a c r o I d                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int32 MakeOvlMacroId
        {
            get { return _makeOvlMacroId; }
            set { _makeOvlMacroId = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // M a k e O v l O f f s e t                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int64 MakeOvlOffset
        {
            get { return _makeOvlOffset; }
            set { _makeOvlOffset = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // M a k e O v l P a g e M a r k                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean MakeOvlPageMark
        {
            get { return _makeOvlPageMark; }
            set { _makeOvlPageMark = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // M a k e O v l P o s                                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PrnParseConstants.eOvlPos MakeOvlPos
        {
            get { return _makeOvlPos; }
            set { _makeOvlPos = value; }
        }
 
        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // M a k e O v l R e s t o r e S t a t e X L                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean MakeOvlRestoreStateXL
        {
            get { return _makeOvlRestoreStateXL; }
            set { _makeOvlRestoreStateXL = value; }
        }
 
        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // M a k e O v l S h o w                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PrnParseConstants.eOvlShow MakeOvlShow
        {
            get { return _makeOvlShow; }
            set { _makeOvlShow = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // M a k e O v l S k i p B e g i n                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int64 MakeOvlSkipBegin
        {
            get { return _makeOvlSkipBegin; }
            set { _makeOvlSkipBegin = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // M a k e O v l S k i p E n d                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Int64 MakeOvlSkipEnd
        {
            get { return _makeOvlSkipEnd; }
            set { _makeOvlSkipEnd = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // M a k e O v l S t r e a m N a m e                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String MakeOvlStreamName
        {
            get { return _makeOvlStreamName; }
            set { _makeOvlStreamName = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // M a k e O v l X L                                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean MakeOvlXL
        {
            get { return _makeOvlXL; }
            set { _makeOvlXL = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // P c l C o m b o F i r s t                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean PclComboFirst
        {
            get { return _pclComboFirst; }
            set { _pclComboFirst = value; }
        }

        //--------------------------------------------------------------------//
        //                  `                                  P r o p e r t y //
        // P c l C o m b o L a s t                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean PclComboLast
        {
            get { return _pclComboLast; }
            set { _pclComboLast = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // P c l C o m b o M o d i f i e d                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean PclComboModified
        {
            get { return _pclComboModified; }
            set { _pclComboModified = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // P c l C o m b o S e q                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean PclComboSeq
        {
            get { return _pclComboSeq; }
            set { _pclComboSeq = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // P c l x l E m b e d T y p e                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PCLXLOperators.eEmbedDataType PclxlEmbedType
        {
            get { return _pclxlEmbedType; }
            set { _pclxlEmbedType = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // P r e s c r i b e C a l l e r P D L                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public ToolCommonData.ePrintLang PrescribeCallerPDL
        {
            get { return _prescribeCallerPDL; }
            set { _prescribeCallerPDL = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // P r e s c r i b e I n t r o R e a d                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean PrescribeIntroRead
        {
            get { return _prescribeIntroRead; }
            set { _prescribeIntroRead = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // P r e s c r i b e S C R C                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Byte PrescribeSCRC
        {
            get { return _prescribeSCRC; }
            set { _prescribeSCRC = value; }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e s e t C o n t D a t a                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Reset 'link' continuation data flags and values.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void resetContData()
        {
            _contType            = PrnParseConstants.eContType.None;
            _prefixLen           = 0;
            _dataLen             = 0;
            _downloadRem         = 0;
            _backTrack           = false;
            _prefixA             = 0x00;
            _prefixB             = 0x00;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e s e t P C L C o m b o D a t a                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Reset 'PCL combination sequence' data flags and values.            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void resetPCLComboData()
        {
            _pclComboSeq         = false;
            _pclComboFirst       = false;
            _pclComboLast        = false;
            _pclComboModified    = false;
            _pclComboStart       = -1;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t B a c k t r a c k                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set (backtracking) continuation data flags and values.             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void setBacktrack (PrnParseConstants.eContType contType,
                                  Int32 dataLen)
        {
            _contType          = contType;
            _prefixLen         = 0;
            _dataLen           = dataLen;
            _downloadRem       = 0;
            _backTrack         = true;
            _prefixA           = 0x20;
            _prefixB           = 0x20;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t C o n t D a t a                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set continuation data flags and values.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void setContData(PrnParseConstants.eContType contType,
                                Int32      prefixLen,
                                Int32      dataLen,
                                Int32      downloadRem,
                                Boolean    backTrack,
                                Byte       prefixA,
                                Byte       prefixB)
        {
            _contType          = contType;
            _prefixLen         = prefixLen;
            _dataLen           = dataLen;
            _downloadRem       = downloadRem;
            _backTrack         = backTrack;
            _prefixA           = prefixA;
            _prefixB           = prefixB;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t C o n t i n u a t i o n                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set (non-backtracking) continuation data flags and values.         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void setContinuation (PrnParseConstants.eContType contType)
        {
            _contType          = contType;
            _prefixLen         = 0;
            _dataLen           = 0;
            _downloadRem       = 0;
            _backTrack         = false;
            _prefixA           = 0x20;
            _prefixB           = 0x20;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t E o f                                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Sets the end-of-file flag in the 'link' data.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void setEof(Boolean eofSet)
        {
            _eof = eofSet;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t P C L C o m b o D a t a                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set 'PCL combination sequence' data flags and values.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void setPCLComboData(Boolean pclComboSeq,
                                    Boolean pclComboFirst,
                                    Boolean pclComboLast,
                                    Boolean pclComboModified,
                                    Int64   pclComboStart)
        {
            _pclComboSeq      = pclComboSeq;
            _pclComboFirst    = pclComboFirst;
            _pclComboLast     = pclComboLast;
            _pclComboModified = pclComboModified;
            _pclComboStart    = pclComboStart;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t P r e f i x D a t a                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set prefix information.                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void setPrefixData(Int32 prefixLen,
                                  Byte  prefixA,
                                  Byte  prefixB)
        {
            _prefixLen = prefixLen;
            _prefixA = prefixA;
            _prefixB = prefixB;
        }
    }
}