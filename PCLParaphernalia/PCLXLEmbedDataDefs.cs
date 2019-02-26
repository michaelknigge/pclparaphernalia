using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides details of PCL XL Embedded Data Definer tags.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class PCLXLEmbedDataDefs
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // PCLXL Embedded Data Definer tags.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public enum eTag : byte
        {
            Int    = 0xfa,
            Byte   = 0xfb
        }

        //--------------------------------------------------------------------//
        //                                                                    //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static SortedList<Byte, PCLXLEmbedDataDef> _tags =
            new SortedList<Byte, PCLXLEmbedDataDef>();

        private static Int32 _tagCount;
        
        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L X L E m b e d D a t a D e f s                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        static PCLXLEmbedDataDefs()
        {
            populateTable();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d i s p l a y T a g s                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Display list of Embedded Data Definer tags.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 displayTags(DataGrid grid,
                                        Boolean  incResTags)
        {
            Int32 count = 0;

            Boolean tagReserved;

            foreach (KeyValuePair<Byte, PCLXLEmbedDataDef> kvp in _tags)
            {
                tagReserved = kvp.Value.FlagReserved;

                if ((incResTags == true) ||
                    ((incResTags == false) && (!tagReserved)))
                {
                    count++;
                    grid.Items.Add(kvp.Value);
                }
            }

            return count;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p o p u l a t e T a b l e                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Populate the table of Embedded Data Definer tags.                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void populateTable()
        {
            const Boolean flagNone     = false;

            Byte tag;

            tag = (Byte) eTag.Int;                                   // 0xfa //
            _tags.Add(tag,
                new PCLXLEmbedDataDef(tag,
                                         flagNone,
                                         "data length integer"));

            tag = (Byte) eTag.Byte;                                  // 0xfb //
            _tags.Add(tag,
                new PCLXLEmbedDataDef(tag,
                                         flagNone,
                                         "data length byte"));

            _tagCount = _tags.Count;
        }
    }
}
