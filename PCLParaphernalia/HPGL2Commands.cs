using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines the set of HP-GL/2 Commands.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class HPGL2Commands
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static SortedList<String, HPGL2Command> _cmds =
            new SortedList<String, HPGL2Command>();

        private static HPGL2Command _cmdUnknown;

        private static Int32 _cmdsCount;
        
        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // H P G L 2 C o m m a n d s                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        static HPGL2Commands()
        {
            populateTable();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // C h e c k C m d                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Searches the HP-GL/2 sequence table for an entry identified by the //
        // two-character alphabetic command mnemonic.                         //
        //                                                                    //
        // If found, the description and option flags of the sequence are     //
        // returned, otherwise details of the 'unknown' entry are returned.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean checkCmd(Int32       macroLevel,
                                       String      mnemonic,
                                       ref Boolean optResetHPGL2,
                                       ref Boolean optBinaryData,
                                       ref Boolean optFlipTransp,
                                       ref Boolean optSetLblTerm,
                                       ref Boolean optUseLblTerm,
                                       ref Boolean optUseStdTerm,
                                       ref Boolean optQuotedData,
                                       ref Boolean optSymbolMode,
                                       ref String  description)
        {
            Boolean cmdKnown;

            HPGL2Command cmd;

            if (_cmds.IndexOfKey (mnemonic) != -1)
            {
                cmdKnown = true;
                cmd = _cmds[mnemonic];
            }
            else
            {
                cmdKnown = false;
                cmd = _cmdUnknown;
            }

            optResetHPGL2 = cmd.FlagResetHPGL2;
            optBinaryData = cmd.FlagBinaryData;
            optFlipTransp = cmd.FlagFlipTransp;
            optSetLblTerm = cmd.FlagSetLblTerm;
            optUseLblTerm = cmd.FlagUseLblTerm;
            optUseStdTerm = cmd.FlagUseStdTerm;
            optQuotedData = cmd.FlagQuotedData;
            optSymbolMode = cmd.FlagSymbolMode;

            description = cmd.Description;

            cmd.incrementStatisticsCount(macroLevel);

            return cmdKnown;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d i s p l a y S e q L i s t                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Display list of commands in nominated data grid.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 displaySeqList(DataGrid grid)
        {
            Int32 count = 0;

            foreach (KeyValuePair<String, HPGL2Command> kvp in _cmds)
            {
                count++;
                grid.Items.Add(kvp.Value);
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

            displaySeq = true;

            count = _cmdUnknown.StatsCtTotal;

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

                row[0] = _cmdUnknown.Mnemonic;
                row[1] = _cmdUnknown.Description;
                row[2] = _cmdUnknown.StatsCtParent;
                row[3] = _cmdUnknown.StatsCtChild;
                row[4] = _cmdUnknown.StatsCtTotal;

                table.Rows.Add (row);
            }

            //----------------------------------------------------------------//

            foreach (KeyValuePair<String, HPGL2Command> kvp in _cmds)
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

                    row[0] = kvp.Value.Mnemonic;
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
            row[1] = "_________________";
            row[2] = "";
            row[3] = "";
            row[4] = "";

            table.Rows.Add (row);

            row = table.NewRow ();

            row[0] = "";
            row[1] = "HP-GL/2 commands:";
            row[2] = "";
            row[3] = "";
            row[4] = "";

            table.Rows.Add (row);

            row = table.NewRow ();

            row[0] = "";
            row[1] = "¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯";
            row[2] = "";
            row[3] = "";
            row[4] = "";

            table.Rows.Add (row);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t S e q C o u n t                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return count of sequences.                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 getSeqCount()
        {
            return _cmdsCount;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p o p u l a t e T a b l e                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Populate the table of HP-GL/2 commands.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void populateTable()
        {
            const Boolean flagNone       = false;
            const Boolean flagResetHPGL2 = true;
            const Boolean flagBinaryData = true;
            const Boolean flagFlipTransp = true;
            const Boolean flagSetLblTerm = true;
            const Boolean flagUseLblTerm = true;
            const Boolean flagUseStdTerm = true;
            const Boolean flagQuotedData = true;
            const Boolean flagSymbolMode = true;

            String command;

            command = "??";                                             // ?? //
            _cmdUnknown =
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "*** Unknown command ***");

            command = "AA";                                             // AA //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Arc Absolute"));
            command = "AC";                                             // AC //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Anchor Corner"));
            command = "AD";                                             // AD //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Alternate Font Definition"));
            command = "AR";                                             // AR //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Arc Relative"));
            command = "AT";                                             // AT //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Absolute Arc Three Point"));
            command = "BP";                                             // BP //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Begin Plot"));
            command = "BR";                                             // BR //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Bezier Relative"));
            command = "BZ";                                             // BZ //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Bezier Absolute"));
            command = "CF";                                             // CF //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Character Fill Mode"));
            command = "CI";                                             // CI //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Circle"));
            command = "CO";                                             // CO //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagQuotedData, flagNone,
                                 "Comment"));
            command = "CP";                                             // CP //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Character Plot"));
            command = "CR";                                             // CR //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Colour Range"));
            command = "CT";                                             // CT //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Chord Tolerance Mode"));
            command = "DC";                                             // DC //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Digitize Clear"));
            command = "DF";                                             // DF //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagResetHPGL2, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Default Values"));
            command = "DI";                                             // DI //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Absolute Direction"));
            command = "DL";                                             // DL //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Download Character"));
            command = "DP";                                             // DP //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Digitize Point"));
            command = "DR";                                             // DR //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Relative Direction"));
            command = "DT";                                             // DT //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagSetLblTerm,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Define Label Terminator"));
            command = "DV";                                             // DV //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Define Variable Text Path"));
            command = "EA";                                             // EA //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Edge Rectangle Absolute"));
            command = "EC";                                             // EC //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Enable Cutter"));
            command = "EP";                                             // EP //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Edge Polygon"));
            command = "ER";                                             // ER //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Edge Rectangle Relative"));
            command = "ES";                                             // ES //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Extra Space"));
            command = "EW";                                             // EW //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Edge Wedge"));
            command = "FI";                                             // FI //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Select Primary Font"));
            command = "FN";                                             // FN //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Select Secondary Font"));
            command = "FP";                                             // FP //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Fill Polygon"));
            command = "FR";                                             // FR //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Frame Advance"));
            command = "FT";                                             // FT //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Fill Type"));
            command = "IN";                                             // IN //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagResetHPGL2, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Initialise"));
            command = "IP";                                             // IP //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Input P1 and P2"));
            command = "IR";                                             // IR //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Input Relative P1 and P2"));
            command = "IW";                                             // IW //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Input Window"));
            command = "LA";                                             // LA //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Line Attributes"));
            command = "LB";                                             // LB //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagUseLblTerm, flagNone,
                                 flagNone, flagNone,
                                 "Label"));
            command = "LM";                                             // LM //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Label Mode"));
            command = "LO";                                             // LO //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Label Origin"));
            command = "LT";                                             // LT //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Line Type"));
            command = "MC";                                             // MC //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Merge Control"));
            command = "MG";                                             // MG //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Message"));
            command = "MT";                                             // MT //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Media Type"));
            command = "NP";                                             // NP //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Number Of Pens"));
            command = "NR";                                             // NR //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Not Ready"));
            command = "OD";                                             // OD //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Output Digitized Point & Pen Status"));
            command = "OE";                                             // OE //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Output Error"));
            command = "OH";                                             // OH //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Output Hardclip Limits"));
            command = "OI";                                             // OI //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Output Identification"));
            command = "OP";                                             // OP //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Output P1 and P2"));
            command = "OS";                                             // OS //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Output Status"));
            command = "PA";                                             // PA //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Plot Absolute"));
            command = "PC";                                             // PC //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Pen Colour Assignment"));
            command = "PD";                                             // PD //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Pen Down"));
            command = "PE";                                             // PE //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagBinaryData,
                                 flagNone, flagNone,
                                 flagNone, flagUseStdTerm,
                                 flagNone, flagNone,
                                 "Polyline Encoded"));
            command = "PG";                                             // PG //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Advance Page"));
            command = "PM";                                             // PM //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Polygon Mode"));
            command = "PP";                                             // PP //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Pixel Placement"));
            command = "PR";                                             // PR //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Plot Relative"));
            command = "PS";                                             // PS //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Plot Size"));
            command = "PU";                                             // PU //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Pen Up"));
            command = "PW";                                             // PW //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Pen Width"));
            command = "QL";                                             // QL //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Quality Level"));
            command = "RA";                                             // RA //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Fill Rectangle Absolute"));
            command = "RF";                                             // RF //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Raster Fill Definition"));
            command = "RO";                                             // RO //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Rotate Coordinate System"));
            command = "RP";                                             // RP //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Replot"));
            command = "RQ";                                             // RQ //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Quick Fill Rectangle Relative"));
            command = "RR";                                             // RR //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Fill Rectangle Relative"));
            command = "RT";                                             // RT //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Relative Arc Three Point"));
            command = "SA";                                             // SA //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Select Alternate Font"));
            command = "SB";                                             // SB //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Scalable or Bitmap Fonts"));
            command = "SC";                                             // SC //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Scale"));
            command = "SD";                                             // SD //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Standard Font Definition"));
            command = "SI";                                             // SI //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Absolute Character Size"));
            command = "SL";                                             // SL //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Character Slant"));
            command = "SM";                                             // SM //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagSymbolMode, 
                                 "Symbol Mode"));
            command = "SP";                                             // SP //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Select Pen"));
            command = "SR";                                             // SR //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Relative Character Size"));
            command = "SS";                                             // SS //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Select Standard Font"));
            command = "ST";                                             // ST //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Sort"));
            command = "SV";                                             // SV //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Screened Vectors"));
            command = "TD";                                             // TD //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagFlipTransp, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Transparent Data"));
            command = "TR";                                             // TR //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Transparency Mode"));
            command = "UL";                                             // UL //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "User-defined Line Type"));
            command = "VS";                                             // VS //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Velocity Select"));
            command = "WG";                                             // WG //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Fill Wedge"));
            command = "WU";                                             // WU //
            _cmds.Add(command,
                new HPGL2Command(command,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 flagNone, flagNone,
                                 "Pen Width Unit Selection"));


            _cmdsCount = _cmds.Count;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        //  r e s e t S t a t s C o u n t s                                   //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Reset counts of referenced commnads.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void resetStatsCounts()
        {
            HPGL2Command cmd;

            _cmdUnknown.resetStatistics ();

            foreach (KeyValuePair<String, HPGL2Command> kvp in _cmds)
            {
                cmd = kvp.Value;

                cmd.resetStatistics ();
            }
        }
    }
}
