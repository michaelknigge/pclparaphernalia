using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines a set of PML 'action type' objects.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class PMLDataTypes
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public enum eTag : sbyte
        {
            Unknown = -1,
            StringHddr = -2,
            PMLIntro = -3,

            ObjectID = 0,
            Enumeration,
            Sint,
            Real,
            String,
            Binary,
            ErrorCode,
            NullValue,
            Collection
        }

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static SortedList<Byte, PMLDataType> _tags =
            new SortedList<Byte, PMLDataType>();

        private static PMLDataType _unknownTag;

        private static Int32 _tagCount;
        
        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P M L D a t a T y p e s                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        static PMLDataTypes ()
        {
            populateTable();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h e c k T a g                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Searches the tag table for a matching entry.                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean checkTag(
            Byte       tagToCheck,
            ref String description)
        {
            Boolean seqKnown;

            PMLDataType tag;

            if (_tags.IndexOfKey (tagToCheck) != -1)
            {
                seqKnown = true;
                tag = _tags[tagToCheck];
            }
            else
            {
                seqKnown = false;
                tag = _unknownTag;
            }

            description = tag.getDesc ();

            tag.incrementStatisticsCount (1);   // Statistical data

            return seqKnown;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d i s p l a y S t a t s C o u n t s                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Add counts of referenced sequences to nominated data table.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void displayStatsCounts(DataTable table,
                                               Boolean incUsedSeqsOnly)
        {
            Int32 count = 0;

            Boolean displaySeq,
                    hddrWritten;

            DataRow row;

            hddrWritten = false;

            //----------------------------------------------------------------//

            foreach (KeyValuePair<Byte, PMLDataType> kvp in _tags)
            {
                displaySeq = true;

                count = kvp.Value.StatsCtTotal;

                if (count == 0)
                {
                    if (incUsedSeqsOnly)
                        displaySeq = false;
                }

                if (displaySeq)
                {
                    if (!hddrWritten)
                    {
                        displayStatsCountsHddr (table);
                        hddrWritten = true;
                    }

                    row = table.NewRow ();

                    row[0] = kvp.Value.Tag;
                    row[1] = kvp.Value.Description;
                    row[2] = kvp.Value.StatsCtParent;
                    row[3] = kvp.Value.StatsCtChild;
                    row[4] = kvp.Value.StatsCtTotal;

                    table.Rows.Add (row);
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d i s p l a y S t a t s C o u n t s H d d r                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Add statistics header lines to nominated data table.               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void displayStatsCountsHddr(DataTable table)
        {
            DataRow row;

            //----------------------------------------------------------------//

            row = table.NewRow ();

            row[0] = "";
            row[1] = "______________";
            row[2] = "";
            row[3] = "";
            row[4] = "";

            table.Rows.Add (row);

            row = table.NewRow ();

            row[0] = "";
            row[1] = "PML DataTypes:";
            row[2] = "";
            row[3] = "";
            row[4] = "";

            table.Rows.Add (row);

            row = table.NewRow ();

            row[0] = "";
            row[1] = "¯¯¯¯¯¯¯¯¯¯¯¯¯¯";
            row[2] = "";
            row[3] = "";
            row[4] = "";

            table.Rows.Add (row);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d i s p l a y T a g s                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Display list of tags.                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 displayTags(DataGrid grid)
        {
            Int32 count = 0;

            foreach (KeyValuePair<Byte, PMLDataType> kvp in _tags)
            {
                count++;
                grid.Items.Add (kvp.Value);
            }

            return count;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C o u n t                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return count of definitions.                                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 getCount()
        {
            return _tagCount;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t D e s c                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return description associated with specified tag.                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getDesc(Byte selection)
        {
            return _tags[selection].getDesc();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p o p u l a t e T a b l e                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Populate the table of Operator tags.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void populateTable()
        {
            Byte tag;

            tag = 0x20;                                              // ?    //
            _unknownTag =
                new PMLDataType (tag, 
                                   "*** Unknown tag ***");

            tag = (Byte) eTag.ObjectID;                              // 0x00 //
            _tags.Add (tag,
                new PMLDataType (tag,
                                   "Object Identifier"));

            tag = (Byte) eTag.Enumeration;                           // 0x01 //
            _tags.Add (tag,
                new PMLDataType (tag,
                                   "Enumeration"));

            tag = (Byte) eTag.Sint;                                  // 0x02 //
            _tags.Add (tag,
                new PMLDataType (tag,
                                   "Signed Integer"));

            tag = (Byte) eTag.Real;                                  // 0x03 //
            _tags.Add (tag,
                new PMLDataType (tag,
                                   "Real"));

            tag = (Byte) eTag.String;                                // 0x04 //
            _tags.Add (tag,
                new PMLDataType (tag,
                                   "String"));

            tag = (Byte) eTag.Binary;                                // 0x05 //
            _tags.Add (tag,
                new PMLDataType (tag,
                                   "Binary"));

            tag = (Byte) eTag.ErrorCode;                             // 0x06 //
            _tags.Add (tag,
                new PMLDataType (tag,
                                   "Error Code"));

            tag = (Byte) eTag.NullValue;                             // 0x07 //
            _tags.Add (tag,
                new PMLDataType (tag,
                                   "Null Value"));

            tag = (Byte) eTag.Collection;                            // 0x08 //
            _tags.Add (tag,
                new PMLDataType (tag,
                                   "Collection"));

            _tagCount = _tags.Count;
        }
    }
}