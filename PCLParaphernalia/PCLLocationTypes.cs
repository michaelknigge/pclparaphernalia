using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines a set of PCL 'status readback' Location Type objects.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class PCLLocationTypes
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public enum eType : byte
        {
            Current,
            All,
            Internal,
            Downloaded,
            Cartridge,
            ROMDevice
        }

        private static PCLLocationType[] _locationTypes = 
        {
            new PCLLocationType(eType.All,            "2",
                                "All locations"),
            new PCLLocationType(eType.Current,        "1",
                                "Currently selected"),
            new PCLLocationType(eType.Internal,       "3",
                                "Internal"),
            new PCLLocationType(eType.Downloaded,     "4",
                                "Downloaded entities"),
            new PCLLocationType(eType.Cartridge,      "5",
                                "Cartridge"),
            new PCLLocationType(eType.ROMDevice,      "7",
                                "SIMMs/DIMMs")
        };
        
        private static Int32 _locationTypeCount =
            _locationTypes.GetUpperBound(0) + 1;
        
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C o u n t                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return count of Entity Type definitions.                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 getCount()
        {
            return _locationTypeCount;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I d P C L                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return PCL ID associated with specified Entity Type index.         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getIdPCL(Int32 selection)
        {
            return _locationTypes[selection].getIdPCL();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t N a m e                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return name associated with specified Entity Type index.           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getName(Int32 selection)
        {
            return _locationTypes[selection].getName();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t T y p e                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return type of Entity.                                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static eType getType(Int32 selection)
        {
            return _locationTypes[selection].getType();
        }
    }
}