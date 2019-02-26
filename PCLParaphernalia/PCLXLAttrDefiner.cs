using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines a PCL XL Attribute Definer tag.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>
    
 // [System.Reflection.ObfuscationAttribute(Feature = "properties renaming")]
    [System.Reflection.ObfuscationAttribute (
        Feature = "renaming",
        ApplyToMembers = true)]

    class PCLXLAttrDefiner
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private Byte _tag;

        private String  _description;

        private Boolean _flagReserved;
      
        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L X L A t t r D e f i n e r                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

	    public PCLXLAttrDefiner(Byte    tag,
                                Boolean flagReserved,
                                String  description)
	    {
            _tag          = tag;
            _flagReserved = flagReserved;
            _description = description;
	    }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // D e s c r i p t i o n                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String Description
        {
            get { return _description; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // F l a g R e s e r v e d                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Boolean FlagReserved
        {
            get { return _flagReserved; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // T a g                                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String Tag
        {
            get { return "0x" + _tag.ToString("x2"); }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // T y p e                                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String Type
        {
            get { return "Attribute Definer"; }
        }
    }
}