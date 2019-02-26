using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class handles a PCL 'status readback' Location Type object.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    class PCLLocationType
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private PCLLocationTypes.eType _locationType;
        private String _locationName;
        private String _locationIdPCL;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L L o c a t i o n T y p e                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PCLLocationType(PCLLocationTypes.eType type,
                               String                  id,
                               String                  name)
        {
            _locationType   = type;
            _locationIdPCL = id;
            _locationName   = name;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I d P C L                                                   //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the identifier value.                                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String getIdPCL()
        {
            return _locationIdPCL;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t N a m e                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the location type name.                                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String getName()
        {
            return _locationName;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t T y p e                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the entity type.                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PCLLocationTypes.eType getType()
        {
            return _locationType;
        }
    }
}