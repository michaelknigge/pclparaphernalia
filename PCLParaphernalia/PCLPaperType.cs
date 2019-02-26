using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class handles a PCL Paper Type object.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    class PCLPaperType
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private PCLPaperTypes.eEntryType _entryType;

        private String _paperTypeName;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L P a p e r T y p e                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PCLPaperType(PCLPaperTypes.eEntryType entryType,
                            String                   name)
        {
            _entryType     = entryType;
            _paperTypeName = name;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t N a m e                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the paper type name.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String getName()
        {
            return _paperTypeName;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t T y p e                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the entry type.                                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PCLPaperTypes.eEntryType getType()
        {
            return _entryType;
        }
    }
}