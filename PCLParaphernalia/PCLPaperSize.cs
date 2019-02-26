using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class handles a PCL Paper Size object.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    // [System.Reflection.ObfuscationAttribute(Feature = "properties renaming")]
    [System.Reflection.ObfuscationAttribute(
        Feature = "renaming",
        ApplyToMembers = true)]

    class PCLPaperSize
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        const Double _unitsToInches = (1.00 / PCLPaperSizes._paperSizeUPI);
        const Double _unitsToMilliMetres = (25.4 / PCLPaperSizes._paperSizeUPI);

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private PCLPaperSizes.eIndex _paperSizeIndex;

        private String _paperSizeName;
        private String _paperSizeDesc;
        private Byte   _paperSizeIdPCL;
        private Byte   _paperSizeIdPCLXL;
        private String _paperSizeNamePCLXL;

        private Boolean _paperSizeIsMetric;
        private Boolean _paperSizeIsRare;
        
        private UInt16 _sizeUnitsPerInch;
        private UInt32 _sizeShortEdge;
        private UInt32 _sizeLongEdge;
        private UInt16 _marginsLogicalPort;
        private UInt16 _marginsLogicalLand;
        private UInt16 _marginsUnprintable;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L P a p e r S i z e                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PCLPaperSize(PCLPaperSizes.eIndex sizeIndex,
                            String  name,
                            String  desc,
                            Byte    idPCL,
                            Byte    idPCLXL,
                            String  namePCLXL,
                            Boolean isMetricSize,
                            Boolean isRareSize,
                            UInt16  sizeUnitsPerInch,
                            UInt32  sizeShortEdge,
                            UInt32  sizeLongEdge,
                            UInt16  marginsLogicalPort,
                            UInt16  marginsLogicalLand,
                            UInt16  marginsUnprintable)
        {
            _paperSizeIndex     = sizeIndex;
            _paperSizeName      = name;
            _paperSizeDesc      = desc;
            _paperSizeIdPCL     = idPCL;
            _paperSizeIdPCLXL   = idPCLXL;
            _paperSizeNamePCLXL = namePCLXL;
            _paperSizeIsMetric  = isMetricSize;
            _paperSizeIsRare    = isRareSize;

            _sizeUnitsPerInch   = sizeUnitsPerInch;
            _sizeShortEdge      = sizeShortEdge;
            _sizeLongEdge       = sizeLongEdge;
            _marginsLogicalPort = marginsLogicalPort;
            _marginsLogicalLand = marginsLogicalLand;
            _marginsUnprintable = marginsUnprintable;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c u s t o m D a t a C o p y                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Copy the 'name' and size data fields to the 'custom' entry.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public void customDataCopy(PCLPaperSize customEntry)
        {
            customEntry.customDataPaste (_paperSizeName,
                                         _paperSizeIsMetric,
                                         _paperSizeIsRare,
                                         _sizeUnitsPerInch,
                                         _sizeShortEdge,
                                         _sizeLongEdge,
                                         _marginsLogicalPort,
                                         _marginsLogicalLand,
                                         _marginsUnprintable);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c u s t o m D a t a P a s t e                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Copy the various size data fields to the 'custom' entry.           //
        // Also copy the 'name' field from the 'donor' entry to the 'desc'    //
        // fiedl of the 'custom' entry.                                       //
        // This should only ever be called when the current Paper Size object //
        // instance is the "Custom" one.                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private void customDataPaste (String  donorName,
                                      Boolean isMetricSize,
                                      Boolean isRareSize,
                                      UInt16  sizeUnitsPerInch,
                                      UInt32  sizeShortEdge,
                                      UInt32  sizeLongEdge,
                                      UInt16  marginsLogicalPort,
                                      UInt16  marginsLogicalLand,
                                      UInt16  marginsUnprintable)
        {
            _paperSizeDesc = donorName;

            _paperSizeIsMetric  = isMetricSize;
            _paperSizeIsRare    = isRareSize;

            _sizeUnitsPerInch   = sizeUnitsPerInch;
            _sizeShortEdge      = sizeShortEdge;
            _sizeLongEdge       = sizeLongEdge;
            _marginsLogicalPort = marginsLogicalPort;
            _marginsLogicalLand = marginsLogicalLand;
            _marginsUnprintable = marginsUnprintable;
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // C u s t o m D e s c                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Sets the 'desc' value for the special 'custom' paper size          //
        // instance; this is used to temporarily hold extra data.             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String CustomDesc
        {
            set { _paperSizeDesc = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // C u s t o m L o n g E d g e                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Sets or returns the 'long edge' dimension for the special          //
        // 'custom' paper size instance.                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public UInt32 CustomLongEdge
        {
            get { return _sizeLongEdge; }
            set { _sizeLongEdge = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // C u s t o m S h o r t E d g e                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Sets or returns the 'short edge' dimension for the special         //
        // 'custom' paper size instance.                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public UInt32 CustomShortEdge
        {
            get { return _sizeShortEdge; }
            set { _sizeShortEdge = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // D e s c                                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the paper size description.                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String Desc
        {
            get { return _paperSizeDesc; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // E d g e L o n g                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the long edge dimension for the paper size.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String EdgeLong
        {
            get
            {
                String size;

                if ((_paperSizeIndex == PCLPaperSizes.eIndex.Custom) ||
                    (_paperSizeIndex == PCLPaperSizes.eIndex.Default) ||
                    (_paperSizeIndex == PCLPaperSizes.eIndex.Card_Custom) ||
                    (_paperSizeIndex == PCLPaperSizes.eIndex.Env_Custom))
                {
                    size = "?";
                }
                else
                {

                    if (_paperSizeIsMetric)
                    {
                        size = (Math.Round((_sizeLongEdge *
                                            _unitsToMilliMetres), 3)).ToString("F0") +
                                            " mm";
                    }
                    else
                    {
                        size = (Math.Round((_sizeLongEdge *
                                            _unitsToInches), 3)).ToString("F3") +
                                            "\"";
                    }
                }

                return size;
            }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // E d g e S h o r t                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the short edge dimension for the paper size.                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String EdgeShort
        {
            get
            {
                String size;

                if ((_paperSizeIndex == PCLPaperSizes.eIndex.Custom) ||
                    (_paperSizeIndex == PCLPaperSizes.eIndex.Default) ||
                    (_paperSizeIndex == PCLPaperSizes.eIndex.Card_Custom) ||
                    (_paperSizeIndex == PCLPaperSizes.eIndex.Env_Custom))
                {
                    size = "?";
                }
                else
                {

                    if (_paperSizeIsMetric)
                    {
                        size = (Math.Round((_sizeShortEdge *
                                            _unitsToMilliMetres), 3)).ToString("F0") +
                                            " mm";
                    }
                    else
                    {
                        size = (Math.Round((_sizeShortEdge *
                                            _unitsToInches), 3)).ToString("F3") +
                                            "\"";
                    }
                }

                return size;
            }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g C u s t o m S i z e                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the flag indicating whether or not the paper size is the    //
        // special "Custom" value.                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagCustomSize
        {
            get
            {
                if (_paperSizeIndex == PCLPaperSizes.eIndex.Custom)
                    return true;
                else
                    return false;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t D e s c                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the paper size description.                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String getDesc()
        {
            return _paperSizeDesc;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I d P C L                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the PCL identifier value.                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Byte getIdPCL()
        {
            return _paperSizeIdPCL;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I d P C L X L                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the PCL XL identifier value.                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Byte getIdPCLXL()
        {
            return _paperSizeIdPCLXL;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t L o g i c a l O f f s e t                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the logical page offset of the paper for a given aspect.    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public UInt16 getLogicalOffset(UInt16                  sessionUPI,
                                       PCLOrientations.eAspect aspect)
        {
            if (aspect == PCLOrientations.eAspect.Portrait)
                return (UInt16)((_marginsLogicalPort * sessionUPI) /
                                _sizeUnitsPerInch);
            else
                return (UInt16)((_marginsLogicalLand * sessionUPI) /
                                _sizeUnitsPerInch);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t L o g P a g e L e n g t h                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the length of the PCL logical page for a given aspect.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public UInt16 getLogPageLength (UInt16 sessionUPI,
                                        PCLOrientations.eAspect aspect)
        {
            if (aspect == PCLOrientations.eAspect.Portrait)
            {
                return (UInt16)((_sizeLongEdge *
                                 sessionUPI) /
                                _sizeUnitsPerInch);
            }
            else
            {
                return (UInt16)((_sizeShortEdge *
                                 sessionUPI) /
                                _sizeUnitsPerInch);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t L o g P a g e W i d t h                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the width of the PCL logical page for a given aspect.       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public UInt16 getLogPageWidth (UInt16 sessionUPI,
                                       PCLOrientations.eAspect aspect)
        {
            if (aspect == PCLOrientations.eAspect.Portrait)
            {
                return (UInt16)(((_sizeShortEdge - (_marginsLogicalPort * 2)) *
                                 sessionUPI) /
                                _sizeUnitsPerInch);
            }
            else
            {
                return (UInt16)(((_sizeLongEdge - (_marginsLogicalPort * 2)) *
                                 sessionUPI) /
                                _sizeUnitsPerInch);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t M a r g i n s L o g i c a l L a n d                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the size of the PCL Landscape logical margins.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public UInt16 getMarginsLogicalLand(UInt16 sessionUPI)
        {
            return (UInt16)((_marginsLogicalLand * sessionUPI) /
                            _sizeUnitsPerInch);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t M a r g i n s L o g i c a l P o r t                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the size of the PCL Portrait logical margins.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public UInt16 getMarginsLogicalPort(UInt16 sessionUPI)
        {
            return (UInt16)((_marginsLogicalPort * sessionUPI) /
                            _sizeUnitsPerInch);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t M a r g i n s U n p r i n t a b l e                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the size of the unprintable margins; these are the same for //
        // both standard orientations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public UInt16 getMarginsUnprintable(UInt16 sessionUPI)
        {
            return (UInt16)((_marginsUnprintable * sessionUPI) /
                            _sizeUnitsPerInch);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t N a m e                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the paper size name.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String getName()
        {
            return _paperSizeName;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t N a m e P C L X L                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the PCL XL string name.                                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String getNamePCLXL()
        {
            return _paperSizeNamePCLXL;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t P a p e r L e n g t h                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the length of the paper for a given aspect.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public UInt16 getPaperLength(UInt16                  sessionUPI,
                                     PCLOrientations.eAspect aspect)
        {
            if (aspect == PCLOrientations.eAspect.Portrait)
                return (UInt16)((_sizeLongEdge * sessionUPI) /
                                _sizeUnitsPerInch);
            else
                return (UInt16)((_sizeShortEdge * sessionUPI) /
                                _sizeUnitsPerInch);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t P a p e r W i d t h                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the width of the paper for a given aspect.                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public UInt16 getPaperWidth(UInt16                  sessionUPI,
                                    PCLOrientations.eAspect aspect)
        {
            if (aspect == PCLOrientations.eAspect.Portrait)
                return (UInt16)((_sizeShortEdge * sessionUPI) /
                                _sizeUnitsPerInch);
            else
                return (UInt16)((_sizeLongEdge * sessionUPI) /
                                _sizeUnitsPerInch);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t S i z e L o n g E d g e                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the size of the long edge of the paper.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public UInt16 getSizeLongEdge(UInt16 sessionUPI)
        {
            return (UInt16)((_sizeLongEdge * sessionUPI) / _sizeUnitsPerInch);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t S i z e S h o r t E d g e                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the size of the short edge of the paper.                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public UInt16 getSizeShortEdge(UInt16 sessionUPI)
        {
            return (UInt16)((_sizeShortEdge * sessionUPI) / _sizeUnitsPerInch);
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // I d P C L                                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the PCL identifier (if known) for the paper size.           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String IdPCL
        {
            get
            {
                if (_paperSizeIdPCL == 0xff)
                    return "?";
                else
                    return _paperSizeIdPCL.ToString();
            }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // I d P C L X L                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the PCL XL enumeration (if known) for the paper size.       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String IdPCLXL
        {
            get
            {
                if (_paperSizeIdPCLXL == 0xff)
                    return "?";
                else
                    return _paperSizeIdPCLXL.ToString();
            }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // I d N a m e P C L X L                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the PCL XL enumeration and string name (if known) for the   //
        // paper size.                                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String IdNamePCLXL
        {
            get
            {
                String id,
                       name;
                 
                if (_paperSizeIdPCLXL == 0xff)
                    id = "?";
                else
                    id = _paperSizeIdPCLXL.ToString();

                if (_paperSizeNamePCLXL == "")
                    name = "?";
                else
                    name = _paperSizeNamePCLXL.ToString();

                return id + " / " + name;

            }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // I s M e t r i c S i z e                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Sets or returns the 'is metric size' attribute.                    //
        // The 'set' option would only apply to the special 'Custom' paper    //
        // size.                                                              // 
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean IsMetricSize
        {
            get { return _paperSizeIsMetric; }
            set { _paperSizeIsMetric = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // I s R a r e S i z e                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Returns the 'is rare (or obsolete) size' attribute.                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean IsRareSize
        {
            get { return _paperSizeIsRare; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // N a m e                                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the paper size reference name.                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String Name
        {
            get { return _paperSizeName; }
        }
    }
}