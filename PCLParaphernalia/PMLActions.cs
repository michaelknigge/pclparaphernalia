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

    static class PMLActions
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static SortedList<Byte, PMLAction> _tags =
            new SortedList<Byte, PMLAction>();

        private static PMLAction _unknownTag;

        private static Int32 _tagCount;
        
        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P M L A c t i o n T y p e s                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        static PMLActions ()
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
            Byte tagToCheck,
            ref String description)
        {
            Boolean seqKnown;

            PMLAction tag;

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
        // d i s p l a y T a g s                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Display list of tags.                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 displayTags(DataGrid grid)
        {
            Int32 count = 0;

            foreach (KeyValuePair<Byte, PMLAction> kvp in _tags)
            {
                count++;
                grid.Items.Add (kvp.Value);
            }

            return count;
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

            foreach (KeyValuePair<Byte, PMLAction> kvp in _tags)
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
            row[1] = "____________";
            row[2] = "";
            row[3] = "";
            row[4] = "";

            table.Rows.Add (row);

            row = table.NewRow ();

            row[0] = "";
            row[1] = "PML Actions:";
            row[2] = "";
            row[3] = "";
            row[4] = "";

            table.Rows.Add (row);

            row = table.NewRow ();

            row[0] = "";
            row[1] = "¯¯¯¯¯¯¯¯¯¯¯¯";
            row[2] = "";
            row[3] = "";
            row[4] = "";

            table.Rows.Add (row);
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
                new PMLAction (tag, 
                                   "*** Unknown tag ***");

            tag = 0x00;                                              // 0x00 //
            _tags.Add (tag,
                new PMLAction (tag,
                                   "GetRequest"));

            tag = 0x01;                                              // 0x01 //
            _tags.Add (tag,
                new PMLAction (tag,
                                   "GetNextRequest"));

            tag = 0x02;                                              // 0x02 //
            _tags.Add (tag,
                new PMLAction (tag,
                                   "GetBlockRequest"));

            tag = 0x03;                                              // 0x03 //
            _tags.Add (tag,
                new PMLAction (tag,
                                   "GetNextBlockRequest"));

            tag = 0x04;                                              // 0x04 //
            _tags.Add (tag,
                new PMLAction (tag,
                                   "SetRequest"));

            tag = 0x05;                                              // 0x05 //
            _tags.Add (tag,
                new PMLAction (tag,
                                   "EnableTrapRequest"));

            tag = 0x06;                                              // 0x06 //
            _tags.Add (tag,
                new PMLAction (tag,
                                   "DisableTrapRequest"));

            tag = 0x07;                                              // 0x07 //
            _tags.Add (tag,
                new PMLAction (tag,
                                   "TrapRequest"));

            tag = 0x80;                                              // 0x80 //
            _tags.Add (tag,
                new PMLAction (tag,
                                   "GetReply"));

            tag = 0x81;                                              // 0x81 //
            _tags.Add (tag,
                new PMLAction (tag,
                                   "GetNextReply"));

            tag = 0x82;                                              // 0x82 //
            _tags.Add (tag,
                new PMLAction (tag,
                                   "GetBlockReply"));

            tag = 0x83;                                              // 0x83 //
            _tags.Add (tag,
                new PMLAction (tag,
                                   "GetNextBlockReply"));

            tag = 0x84;                                              // 0x84 //
            _tags.Add (tag,
                new PMLAction (tag,
                                   "SetReply"));

            tag = 0x85;                                              // 0x85 //
            _tags.Add (tag,
                new PMLAction (tag,
                                   "EnableTrapReply"));

            tag = 0x86;                                              // 0x86 //
            _tags.Add (tag,
                new PMLAction (tag,
                                   "DisableTrapReply"));

            tag = 0x87;                                              // 0x87 //
            _tags.Add (tag,
                new PMLAction (tag,
                                   "TrapReply"));

            _tagCount = _tags.Count;
        }
    }
}