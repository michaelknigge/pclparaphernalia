using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class handles a PCL Plex Mode object.
    /// 
    /// © Chris Hutchinson 2012
    /// 
    /// </summary>

    class PCLPlexMode
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private PCLPlexModes.ePlexType _plexType;

        private String _plexModeName;
        private Byte   _plexModeIdPCL;
        private Byte   _plexModeIdPCLXLLand;
        private Byte   _plexModeIdPCLXLPort;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L P l e x M o d e                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PCLPlexMode (PCLPlexModes.ePlexType plexType,
                            String                 name,
                            Byte                   idPCL,
                            Byte                   idPCLXLLand,
                            Byte                   idPCLXLPort)
        {
            _plexType           = plexType;
            _plexModeName       = name;
            _plexModeIdPCL      = idPCL;
            _plexModeIdPCLXLLand = idPCLXLLand;
            _plexModeIdPCLXLPort = idPCLXLPort;
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
            return _plexModeIdPCL;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I d P C L X L                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the PCL XL (duplex) binding enumeration value for the       //
        // specified page orientation.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Byte getIdPCLXL(Boolean landscape)
        {
            if (landscape)
                return _plexModeIdPCLXLLand;
            else
                return _plexModeIdPCLXLPort;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t N a m e                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the plex mode name.                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String getName()
        {
            return _plexModeName;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t P l e x T y p e                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the plex type.                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PCLPlexModes.ePlexType getPlexType()
        {
            return _plexType;
        }
    }
}