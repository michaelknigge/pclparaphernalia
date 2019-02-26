using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class handles a PCL 'status readback' Entity Type object.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    class PCLEntityType
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private PCLEntityTypes.eType _entityType;
        private String _entityName;
        private String _entityIdPCL;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L E n t i t y T y p e                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PCLEntityType(PCLEntityTypes.eType type,
                             String               id,
                             String               name)
        {
            _entityType   = type;
            _entityIdPCL = id;
            _entityName   = name;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I d P C L                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the identifier value.                                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String getIdPCL()
        {
            return _entityIdPCL;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t N a m e                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the entity type name.                                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String getName()
        {
            return _entityName;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t T y p e                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the entity type.                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PCLEntityTypes.eType getType()
        {
            return _entityType;
        }
    }
}