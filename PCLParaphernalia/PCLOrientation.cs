using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class handles a PCL Orientation object.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    class PCLOrientation
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private PCLOrientations.eAspect _orientationAspect;
        private String _orientationName;
        private Byte   _orientationIdPCL;
        private Byte   _orientationIdPCLXL;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L O r i e n t a t i o n                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PCLOrientation(PCLOrientations.eAspect aspect,
                              String                  name,
                              Byte                    idPCL,
                              Byte                    idPCLXL)
        {
            _orientationAspect  = aspect;
            _orientationName    = name;
            _orientationIdPCL  = idPCL;
            _orientationIdPCLXL  = idPCLXL;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t A s p e c t                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the orientation aspect.                                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PCLOrientations.eAspect getAspect()
        {
            return _orientationAspect;
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
            return _orientationIdPCL;
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
            return _orientationIdPCLXL;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t N a m e                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the orientation name.                                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String getName()
        {
            return _orientationName;
        }
    }
}