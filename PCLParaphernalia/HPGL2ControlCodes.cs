using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class provides details of HP-GL/2 control code characters.
    /// 
    /// © Chris Hutchinson 2013
    /// 
    /// </summary>

    static class HPGL2ControlCodes
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static SortedList<Byte, HPGL2ControlCode> _tags =
            new SortedList<Byte, HPGL2ControlCode>();

        private static HPGL2ControlCode _tagUnknown;

        private static Int32 _tagCount;
        
        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // H P G L 2 C o n t r o l C o d e s                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        static HPGL2ControlCodes()
        {
            populateTable();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h e c k T a g                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Searches the HP-GL/2 Control Code tag table for a matching entry.  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean checkTag (Byte tagToCheck,
                                        ref String description)
        {
            Boolean seqKnown;

            HPGL2ControlCode tag;

            if (_tags.IndexOfKey (tagToCheck) != -1)
            {
                seqKnown = true;
                tag = _tags[tagToCheck];
            }
            else
            {
                seqKnown = false;
                tag = _tagUnknown; 
            }

            description = tag.Description;

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

            displaySeq = true;

            count = _tagUnknown.StatsCtTotal;

            if (count == 0)
                displaySeq = false;

            if (displaySeq)
            {
                if (!hddrWritten)
                {
                    displayStatsCountsHddr (table);
                    hddrWritten = true;
                }

                row = table.NewRow ();

                row[0] = _tagUnknown.Tag;
                row[1] = _tagUnknown.Description;
                row[2] = _tagUnknown.StatsCtParent;
                row[3] = _tagUnknown.StatsCtChild;
                row[4] = _tagUnknown.StatsCtTotal;

                table.Rows.Add (row);
            }

            //----------------------------------------------------------------//

            foreach (KeyValuePair<Byte, HPGL2ControlCode> kvp in _tags)
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

                    row[0] = kvp.Value.Sequence;
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
            row[1] = "______________________";
            row[2] = "";
            row[3] = "";
            row[4] = "";

            table.Rows.Add (row);

            row = table.NewRow ();

            row[0] = "";
            row[1] = "HP-GL/2 control codes:";
            row[2] = "";
            row[3] = "";
            row[4] = "";

            table.Rows.Add (row);

            row = table.NewRow ();

            row[0] = "";
            row [1] = "¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯";
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
        // Display list of Whitespace tags.                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 displayTags(DataGrid grid)
        {
            Int32 count = 0;

            foreach (KeyValuePair<Byte, HPGL2ControlCode> kvp in _tags)
            {
                    count++;
                    grid.Items.Add(kvp.Value);
            }

            return count;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i n c r e m e n t S t a t s C o u n t                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Increment the relevant statistics count for the DataType tag.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void incrementStatsCount (Byte tagByte,
                                                Int32 level)
        {
            HPGL2ControlCode tag;

            if (_tags.IndexOfKey (tagByte) != -1)
                tag = _tags[tagByte];
            else
                tag = _tagUnknown;

            tag.incrementStatisticsCount (level);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i s K n o w n T a g                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Searches the HP-GL/2 Control Code tag table for a matching entry.  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean isKnownTag(Byte tagToCheck)
        {
            if (_tags.IndexOfKey (tagToCheck) != -1)
                return true;
            else
                return false;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p o p u l a t e T a b l e                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Populate the table of Control Code tags.                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void populateTable()
        {
            Byte tag;

            tag = 0x20;                                              // ?    //
            _tagUnknown =
                new HPGL2ControlCode (tag,
                                      true,
                                      "", 
                                      "*** Unknown tag ***");

            tag = 0x00;                                               // 0x00 //
            _tags.Add (tag,
                new HPGL2ControlCode (tag,
                                      true,
                                      "<NUL>",
                                      "Null"));

            tag = 0x03;                                               // 0x03 //
            _tags.Add (tag,
                new HPGL2ControlCode (tag,
                                      true,   // except when Label terminator //
                                      "<ETX>",
                                      "End of Text"));

            tag = 0x07;                                               // 0x07 //
            _tags.Add (tag,
                new HPGL2ControlCode (tag,
                                      true,
                                      "<BEL>",
                                      "Bell"));

            tag = 0x08;                                               // 0x08 //
            _tags.Add (tag,
                new HPGL2ControlCode (tag,
                                      false,
                                      "<BS>",
                                      "Backspace"));

            tag = 0x09;                                               // 0x09 //
            _tags.Add(tag,
                new HPGL2ControlCode (tag,
                                      false,
                                      "<HT>",
                                      "Horizontal Tab"));

            tag = 0x0a;                                               // 0x0a //
            _tags.Add(tag,
                new HPGL2ControlCode (tag,
                                      false,
                                      "<LF>",
                                      "Line Feed"));

            tag = 0x0b;                                               // 0x0b //
            _tags.Add(tag,
                new HPGL2ControlCode (tag,
                                      true,
                                      "<VT>",
                                      "Vertical Tab"));

            tag = 0x0c;                                               // 0x0c //
            _tags.Add(tag,
                new HPGL2ControlCode (tag,
                                      true,
                                      "<FF>",
                                      "Form Feed"));

            tag = 0x0d;                                               // 0x0d //
            _tags.Add(tag,
                new HPGL2ControlCode (tag,
                                      true,
                                      "<CR>",
                                      "Carriage Return"));

            tag = 0x0e;                                               // 0x0e //
            _tags.Add (tag,
                new HPGL2ControlCode (tag,
                                      false,
                                      "<SO>",
                                      "Shift Out"));

            tag = 0x0f;                                               // 0x0f //
            _tags.Add (tag,
                new HPGL2ControlCode (tag,
                                      false,
                                      "<SI>",
                                      "Shift In"));

            // Escape is a control code in the sense that it is the           //
            // introductory character for simple and complex PCL escape       //
            // sequences.                                                     //
            // Don't include it here, as (with the current mechanism) it will //
            // always register a zero hit count in the statistics.            //
            /*
            tag = 0x1b;                                               // 0x1b //
            _tags.Add (tag,
                new HPGL2ControlCode (tag,
                                      false,
                                      "<ESC>",
                                      "Escape"));
            */
            tag = 0x20;                                               // 0x20 //
            _tags.Add(tag,
                new HPGL2ControlCode (tag,
                                      true,
                                      "<SP>",
                                      "Space"));

            _tagCount = _tags.Count;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        //  r e s e t S t a t s C o u n t s                                   //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Reset counts of referenced tags.                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void resetStatsCounts()
        {
            HPGL2ControlCode tag;

            _tagUnknown.resetStatistics ();

            foreach (KeyValuePair<Byte, HPGL2ControlCode> kvp in _tags)
            {
                tag = kvp.Value;

                tag.resetStatistics ();
            }
        }
    }
}
