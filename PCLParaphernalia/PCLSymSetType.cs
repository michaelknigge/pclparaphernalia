using System;
using System.Text;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class handles a PCL Symbol Set Type.
    /// 
    /// © Chris Hutchinson 2015
    /// 
    /// </summary>

    // [System.Reflection.ObfuscationAttribute(Feature = "properties renaming")]
    [System.Reflection.ObfuscationAttribute (
        Feature = "renaming",
        ApplyToMembers = true)]

    class PCLSymSetType
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

        private String _descStd;
        private String _descShort;

        private Byte   _idPCL;

        private Boolean _flagBound;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L S y m b o l S e t T y p e                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PCLSymSetType (Byte idPCL,
                                 Boolean flagBound,
                                 String descStd,
                                 String descShort)
        {
            _idPCL         = idPCL;
            _flagBound     = flagBound;
            _descStd       = descStd;
            _descShort     = descShort;
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // D e s c S h o r t                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the symbol set type short description.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String DescShort
        {
            get { return _descShort; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // D e s c S t d                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the symbol set type standard description.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String DescStd
        {
            get { return _descStd; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // I d P C L                                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the PCL identifier string.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Byte IdPCL
        {
            get { return _idPCL; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // I s B o u n d                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the flag indicating whether or not the symbol set type is   //
        // bound or unbound.                                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean IsBound
        {
            get { return _flagBound; }
        }
    }
}