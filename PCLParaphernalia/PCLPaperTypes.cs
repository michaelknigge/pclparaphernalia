using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines a set of PCL Paper Type objects.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class PCLPaperTypes
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        // Note that the length of the index array must be the same as that   //
        // of the definition array; the entries must be in the same order.    //
        //                                                                    //
        //--------------------------------------------------------------------//
        
        public enum eEntryType
        {
            Standard,
            NotSet
        }

        public enum eIndex
        {
            NotSet,
            Plain,
            Preprinted,
            Letterhead,
            Transparency,
            Prepunched,
            Labels,
            Bond,
            Recycled,
            Color,
            Rough
        }

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//
       
        private static PCLPaperType[] _paperTypes =
        {
            new PCLPaperType(eEntryType.NotSet,  "<not set>"),
            new PCLPaperType(eEntryType.Standard,"Plain"),
            new PCLPaperType(eEntryType.Standard,"Preprinted"),
            new PCLPaperType(eEntryType.Standard,"Letterhead"),
            new PCLPaperType(eEntryType.Standard,"Transparency"),
            new PCLPaperType(eEntryType.Standard,"Prepunched"),
            new PCLPaperType(eEntryType.Standard,"Labels"),
            new PCLPaperType(eEntryType.Standard,"Bond"),
            new PCLPaperType(eEntryType.Standard,"Recycled"),
            new PCLPaperType(eEntryType.Standard,"Color"),
            new PCLPaperType(eEntryType.Standard,"Rough")
        };

        private static Int32 _paperTypeCount = _paperTypes.GetUpperBound(0) + 1;

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C o u n t                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return count of Paper Type definitions.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 getCount()
        {
            return _paperTypeCount;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t N a m e                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return name associated with specified PaperType index.             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getName(Int32 index)
        {
            return _paperTypes[index].getName();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t T y p e                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return type of entry.                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static eEntryType getType(Int32 index)
        {
            return _paperTypes[index].getType();
        }
    }
}