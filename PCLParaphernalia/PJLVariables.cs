using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines a set of PJL 'status readback' Variable objects.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class PJLVariables
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public enum eVarType
        {
            Custom,
            General,
            PCL,
            PDF,
            PS
        }

        private static PJLVariable[] _vars = 
        {
            new PJLVariable(eVarType.Custom,
                            "<specify value>"),
            new PJLVariable(eVarType.General,
                            "AUTOCONT"),
            new PJLVariable(eVarType.General,
                            "AUTOSELECT"),
            new PJLVariable(eVarType.General,
                            "BINDING"),
            new PJLVariable(eVarType.General,
                            "BITSPERPIXEL"),
            new PJLVariable(eVarType.General,
                            "CLEARABLEWARNINGS"),
            new PJLVariable(eVarType.General,
                            "CONTEXTSWITCH"),
            new PJLVariable(eVarType.General,
                            "COPIES"),
            new PJLVariable(eVarType.General,
                            "COURIER"),
            new PJLVariable(eVarType.General,
                            "CPLOCK"),
            new PJLVariable(eVarType.General,
                            "DENSITY"),
            new PJLVariable(eVarType.General,
                            "DISKLOCK"),
            new PJLVariable(eVarType.General,
                            "DUPLEX"),
            new PJLVariable(eVarType.General,
                            "ECONOMODE"),
            new PJLVariable(eVarType.General,
                            "EDGETOEDGE"),
            new PJLVariable(eVarType.General,
                            "FIH"),
            new PJLVariable(eVarType.General,
                            "FINISH"),
            new PJLVariable(eVarType.General,
                            "FINISHEROPTION"),
            new PJLVariable(eVarType.General,
                            "FINISHERTYPE"),
            new PJLVariable(eVarType.General,
                            "FORMLINES"),
            new PJLVariable(eVarType.General,
                            "HELDJOBTIMEOUT"),
            new PJLVariable(eVarType.General,
                            "HITRANSFER"),
            new PJLVariable(eVarType.General,
                            "HOLD"),
            new PJLVariable(eVarType.General,
                            "HOLDKEY"),
            new PJLVariable(eVarType.General,
                            "HOLDTYPE"),
            new PJLVariable(eVarType.General,
                            "HOSTCLEANINGPAGE"),
            new PJLVariable(eVarType.General,
                            "IMAGEADAPT"),
            new PJLVariable(eVarType.General,
                            "INTRAY1"),
            new PJLVariable(eVarType.General,
                            "INTRAY2"),
            new PJLVariable(eVarType.General,
                            "INTRAY3"),
            new PJLVariable(eVarType.General,
                            "INTRAY1SIZE"),
            new PJLVariable(eVarType.General,
                            "INTRAY2SIZE"),
            new PJLVariable(eVarType.General,
                            "INTRAY3SIZE"),
            new PJLVariable(eVarType.General,
                            "INTRAY4SIZE"),
            new PJLVariable(eVarType.General,
                            "INTRAY5SIZE"),
            new PJLVariable(eVarType.General,
                            "INTRAY6SIZE"),
            new PJLVariable(eVarType.General,
                            "INTRAY7SIZE"),
            new PJLVariable(eVarType.General,
                            "INTRAY8SIZE"),
            new PJLVariable(eVarType.General,
                            "IOBUFFER"),
            new PJLVariable(eVarType.General,
                            "IOSIZE"),
            new PJLVariable(eVarType.General,
                            "JOBATTR"),
            new PJLVariable(eVarType.General,
                            "JOBID"),
            new PJLVariable(eVarType.General,
                            "JOBIDVALUE"),
            new PJLVariable(eVarType.General,
                            "JOBMFQBEGIN"),
            new PJLVariable(eVarType.General,
                            "JOBMFQEND"),
            new PJLVariable(eVarType.General,
                            "JOBNAME"),
            new PJLVariable(eVarType.General,
                            "JOBOFFSET"),
            new PJLVariable(eVarType.General,
                            "JOBSOURCE"),
            new PJLVariable(eVarType.General,
                            "LANG"),
            new PJLVariable(eVarType.General,
                            "LANGPROMPT"),
            new PJLVariable(eVarType.General,
                            "LOWCARTRIDGE"),
            new PJLVariable(eVarType.General,
                            "LOWSUPPLIES"),
            new PJLVariable(eVarType.General,
                            "LOWTONER"),
            new PJLVariable(eVarType.General,
                            "MAINTINTERVAL"),
            new PJLVariable(eVarType.General,
                            "MANUALDUPLEX"),
            new PJLVariable(eVarType.General,
                            "MANUALFEED"),
            new PJLVariable(eVarType.General,
                            "MEDIASOURCE"),
            new PJLVariable(eVarType.General,
                            "MEDIATYPE"),
            new PJLVariable(eVarType.General,
                            "MFQBEGIN"),
            new PJLVariable(eVarType.General,
                            "MFQEND"),
            new PJLVariable(eVarType.General,
                            "MPTRAY"),
            new PJLVariable(eVarType.General,
                            "ORIENTATION"),
            new PJLVariable(eVarType.General,
                            "OUTBIN"),
            new PJLVariable(eVarType.General,
                            "OUTBINPROCESS"),
            new PJLVariable(eVarType.General,
                            "OUTLINEPOINTSIZE"),
            new PJLVariable(eVarType.General,
                            "OUTTONER"),
            new PJLVariable(eVarType.General,
                            "PAGEPROTECT"),
            new PJLVariable(eVarType.General,
                            "PAGES"),
            new PJLVariable(eVarType.General,
                            "PAPER"),
            new PJLVariable(eVarType.General,
                            "PARALLEL"),
            new PJLVariable(eVarType.General,
                            "PASSWORD"),
            new PJLVariable(eVarType.General,
                            "PERSONALITY"),
            new PJLVariable(eVarType.General,
                            "PLANESINUSE"),
            new PJLVariable(eVarType.General,
                            "POWERSAVE"),
            new PJLVariable(eVarType.General,
                            "POWERSAVEMODE"),
            new PJLVariable(eVarType.General,
                            "POWERSAVETIME"),
            new PJLVariable(eVarType.General,
                            "PR1200SPEED"),
            new PJLVariable(eVarType.General,
                            "PRINTONBACKSIDE"),
            new PJLVariable(eVarType.General,
                            "PRINTQUALITY"),
            new PJLVariable(eVarType.General,
                            "PROCESSINGBOUNDARY"),
            new PJLVariable(eVarType.General,
                            "PROCESSINGOPTION"),
            new PJLVariable(eVarType.General,
                            "PROCESSINGTYPE"),
            new PJLVariable(eVarType.General,
                            "QTY"),
            new PJLVariable(eVarType.General,
                            "RENDERMODE"),
            new PJLVariable(eVarType.General,
                            "REPRINT"),
            new PJLVariable(eVarType.General,
                            "RESOLUTION"),
            new PJLVariable(eVarType.General,
                            "RESOURCESAVE"),
            new PJLVariable(eVarType.General,
                            "RESOURCESAVESIZE"),
            new PJLVariable(eVarType.General,
                            "RET"),
            new PJLVariable(eVarType.General,
                            "SCAN"),
            new PJLVariable(eVarType.General,
                            "STAPLEOPTION"),
            new PJLVariable(eVarType.General,
                            "STRINGCODESET"),
            new PJLVariable(eVarType.General,
                            "TESTPAGE"),
            new PJLVariable(eVarType.General,
                            "TIMEOUT"),
            new PJLVariable(eVarType.General,
                            "TRAY1TEMP"),
            new PJLVariable(eVarType.General,
                            "TRAY2TEMP"),
            new PJLVariable(eVarType.General,
                            "TRAY3TEMP"),
            new PJLVariable(eVarType.General,
                            "USERNAME"),
            new PJLVariable(eVarType.General,
                            "WIDEA4"),
            //----------------------------------------------------------------//
            new PJLVariable(eVarType.PCL,
                            "FONTNUMBER"),
            new PJLVariable(eVarType.PCL,
                            "FONTSOURCE"),
            new PJLVariable(eVarType.PCL,
                            "LINETERMINATION"),
            new PJLVariable(eVarType.PCL,
                            "PITCH"),
            new PJLVariable(eVarType.PCL,
                            "PTSIZE"),
            new PJLVariable(eVarType.PCL,
                            "RESOURCESAVESIZE"),
            new PJLVariable(eVarType.PCL,
                            "SYMSET"),
            //----------------------------------------------------------------//
            new PJLVariable(eVarType.PDF,
                            "OWNERPASSWORD"),
            new PJLVariable(eVarType.PDF,
                            "USERPASSWORD"),
            //----------------------------------------------------------------//
            new PJLVariable(eVarType.PS,
                            "ADOBEMBT"),
            new PJLVariable(eVarType.PS,
                            "JAMRECOVERY"),
            new PJLVariable(eVarType.PS,
                            "PRTPSERRS"),
            new PJLVariable(eVarType.PS,
                            "RESOURCESAVESIZE")
        };
        
        private static Int32 _varCount = _vars.GetUpperBound(0) + 1;

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C o u n t                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return count of Variable definitions.                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 getCount()
        {
            return _varCount;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t N a m e                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return name associated with specified variable.                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getName(Int32 selection)
        {
            return _vars[selection].getName();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t T y p e                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return type of variable.                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static eVarType getType(Int32 selection)
        {
            return _vars[selection].getType();
        }
    }
}