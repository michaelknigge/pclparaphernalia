using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class handles a PCL Pattern Def object.
    /// 
    /// © Chris Hutchinson 2016
    /// 
    /// </summary>

    class PCLPatternDef
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private String _desc;
		private PCLPatternDefs.eType _type;

        private UInt16   _id;
        private UInt16   _idSec;
        private UInt16   _height;
        private UInt16   _width;

        private Byte [] _pattern;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L P a t t e r n D e f                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public PCLPatternDef (String    desc,
		                      PCLPatternDefs.eType type,
		                      UInt16    id,
		                      UInt16    idSec,
		                      UInt16    height,
		                      UInt16    width,
                              Byte []   pattern)
        {
            _desc           = desc;
			_type           = type;
            _id             = id;
            _idSec          = idSec;
            _height         = height;
            _width          = width;
			_pattern        = pattern;
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // D e s c                                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the pattern decription.                                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public String Desc
        {
            get { return _desc; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // H e i g h t                                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the Height of the pattern definition.                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public UInt16 Height
        {
            get { return _height; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // I d                                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the PCL identifier value for the pattern.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public UInt16 Id
        {
            get { return _id; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // I d S e c                                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the secondary 'identifier' value for the pattern.           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public UInt16 IdSec
        {
            get { return _idSec; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // P a t t e r n                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the bytes which define the pattern.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public Byte [] Pattern
        {
            get { return _pattern; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // W i d t h                                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the Width of the pattern definition.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public UInt16 Width
        {
            get { return _width; }
        }
    }
}