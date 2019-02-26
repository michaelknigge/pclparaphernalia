using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class handles a PJL 'status readback' Variable object.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    class PJLVariable
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private PJLVariables.eVarType _varType;
        private String _varName;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P J L V a r i a b l e                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PJLVariable(PJLVariables.eVarType type,
                           String                name)
        {
            _varType = type;
            _varName = name;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t N a m e                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the variable name.                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String getName()
        {
            return _varName;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t T y p e                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the variable type.                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PJLVariables.eVarType getType()
        {
            return _varType;
        }
    }
}