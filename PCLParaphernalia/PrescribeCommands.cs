using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines a set of Kyocera Prescibe command objects.
    /// 
    /// © Chris Hutchinson 2017
    /// 
    /// </summary>

    static class PrescribeCommands
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static SortedList<String, PrescribeCommand> _cmds =
            new SortedList<String, PrescribeCommand> ();

        private static PrescribeCommand _cmdUnknown;
        private static PrescribeCommand _cmdIntro;

        private static Int32 _cmdCount;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P r e s c r i b e C o m m a n d s                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        static PrescribeCommands ()
        {
            populateTable();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h e c k C m d                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Searches the command name table for a matching entry.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean checkCmd (String      name,
                                        ref String  description,
                                        ref Boolean flagCmdExit,
                                        ref Boolean flagCmdSetCRC,
                                        Int32       level)
        {
            Boolean seqKnown = true;

            PrescribeCommand cmd;

            if (_cmds.IndexOfKey (name) != -1)
            {
                seqKnown = true;
                cmd = _cmds[name];
            }
            else
            {
                seqKnown = false;
                cmd = _cmdUnknown;
            }

            description = cmd.Description;
            flagCmdExit = cmd.IsCmdExit;
            flagCmdSetCRC = cmd.IsCmdSetCRC;

            cmd.incrementStatisticsCount (level);

            return seqKnown;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h e c k C m d I n t r o                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Searches the command name table for a matching entry.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void checkCmdIntro (ref String  description,
                                          Int32       level)
        {
            description = _cmdIntro.Description;

            _cmdIntro.incrementStatisticsCount (level);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // d i s p l a y C m d s                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Display list of commands.                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 displayCmds (DataGrid grid)
        {
            Int32 count = 0;

            foreach (KeyValuePair<String, PrescribeCommand> kvp
                in _cmds)
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
        // Add counts of referenced commands to nominated data table.         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void displayStatsCounts (DataTable table,
                                               Boolean incUsedSeqsOnly)
        {
            Int32 count = 0;

            Boolean displaySeq,
                    hddrWritten;

            DataRow row;

            hddrWritten = false;

            displaySeq = true;

            //----------------------------------------------------------------//

            count = _cmdIntro.StatsCtTotal;

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

                row[0] = _cmdIntro.Name;
                row[1] = _cmdIntro.Description;
                row[2] = _cmdIntro.StatsCtParent;
                row[3] = _cmdIntro.StatsCtChild;
                row[4] = _cmdIntro.StatsCtTotal;

                table.Rows.Add (row);
            }

            //----------------------------------------------------------------//

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

                row[0] = _cmdUnknown.Name;
                row[1] = _cmdUnknown.Description;
                row[2] = _cmdUnknown.StatsCtParent;
                row[3] = _cmdUnknown.StatsCtChild;
                row[4] = _cmdUnknown.StatsCtTotal;

                table.Rows.Add (row);
            }

            //----------------------------------------------------------------//

            foreach (KeyValuePair<String, PrescribeCommand> kvp
                in _cmds)
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

                    row[0] = kvp.Value.Name;
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
            row[1] = "___________________";
            row[2] = "";
            row[3] = "";
            row[4] = "";

            table.Rows.Add (row);

            row = table.NewRow ();

            row[0] = "";
            row[1] = "Prescribe commands:";
            row[2] = "";
            row[3] = "";
            row[4] = "";

            table.Rows.Add (row);

            row = table.NewRow ();

            row[0] = "";
            row[1] = "¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯";
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
        // Return count of Command definitions.                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 getCount()
        {
            return _cmdCount;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t D e s c                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return description associated with specified command.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getDesc (String name)
        {
            return _cmds[name].Description;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t D e s c C m d I n t r o                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return description associated with the Prescribe start sequence.   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getDescCmdIntro ()
        {
            return _cmdIntro.Description;
        }
        /*
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t N a m e                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return name associated with specified command.                     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getName (eCmdIndex key)
        {
            return _cmds[key.ToString()].Name;
        }
        */
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p o p u l a t e T a b l e                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Populate table of commands.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void populateTable ()
        {
            String command;

            command = "?";
            _cmdUnknown =
                new PrescribeCommand(
                    command,
                    "*** Unknown command ***",
                    false, false, false);

            command = "!R!";
            _cmdIntro =
                new PrescribeCommand(
                        command,
                        "PRESCRIBE start sequence",
                        true, false, false);

            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "PRESCRIBE start sequence",
                          true, false, false));

            command = "ACLI";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Add CoLor by Index",
                          false, false, false));

            command = "ALTB";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "ALlocate TaBle",
                          false, false, false));
            /*
            command = "ALTB A";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "[ALlocate TaBle] Assign user - defined character table"));

            command = "ALTB C";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "[ALlocate TaBle] Convert character code"));

            command = "ALTB D";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "[ALlocate TaBle] Delete user - defined character table"));

            command = "ALTB E";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "[ALlocate TaBle] End defining combination characters"));

            command = "ALTB G";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "[ALlocate TaBle] Generate user - defined table"));

            command = "ALTB R";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "[ALlocate TaBle] Release user - defined character table"));

            command = "ALTB S";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "[ALlocate TaBle] Start to define the combination character"));

            command = "ALTB T";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "[ALlocate TaBle] define combined character by Table"));
            */

            command = "ALTF";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "change to ALTernate Font",
                          false, false, false));

            command = "AMCR";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "call Automatic MaCRo",
                          false, false, false));

            command = "APSG";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Assign Paper Source Group",
                          false, false, false));

            command = "ARC";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "draw filled -in ARC",
                          false, false, false));

            command = "ASFN";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "ASsign external characters for FoNt",
                          false, false, false));

            command = "ASTK";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Assign STacKer trays",
                          false, false, false));

            command = "BARC";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "draw BARCode",
                          false, false, false));

            command = "BKLT";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "print in BooKLeT binding",
                          false, false, false));

            command = "BLK";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "draw filled-in BLocK",
                          false, false, false));

            command = "BOX";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "draw BOX",
                          false, false, false));

            command = "CALL";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "CALL macro",
                          false, false, false));

            command = "CCPY";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Carbon CoPY",
                          false, false, false));

            command = "CDSK";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Check hard DiSK",
                          false, false, false));

            command = "CID";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Configure color - Image Data",
                          false, false, false));

            command = "CIR";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "draw CIRcle",
                          false, false, false));

            command = "CLIP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "CLIP current path",
                          false, false, false));

            command = "CLPR";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "CLiP Rectangular area",
                          false, false, false));

            command = "CLSP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "CLoSe Path",
                          false, false, false));

            command = "CMNT";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "CoMmeNT",
                          false, false, false));

            command = "CMOD";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Color MODe",
                          false, false, false));

            command = "COPY";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "set number of COPIES",
                          false, false, false));

            command = "CPAL";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Control PALette",
                          false, false, false));

            command = "CPTH";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Character PaTH",
                          false, false, false));

            command = "CSET";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Change symbol SET by symbol - set ID",
                          false, false, false));

            command = "CSTK";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "select Collator STacKer",
                          false, false, false));

            command = "CTXT";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "print Centered TeXT",
                          false, false, false));

            command = "DAF";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Delete All Fonts",
                          false, false, false));

            command = "DAM";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Delete All Macros",
                          false, false, false));

            command = "DAP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Draw to Absolute Position",
                          false, false, false));

            command = "DELF";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "DELete Font",
                          false, false, false));

            command = "DELM";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "DELete Macro",
                          false, false, false));

            command = "DPAT";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "select Dashed PATtern",
                          false, false, false));

            command = "DRP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Draw to Relative Position",
                          false, false, false));

            command = "DRPA";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Draw to Relative Position specified by Angle",
                          false, false, false));

            command = "DUPX";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "select / deselect DUPleX mode",
                          false, false, false));

            command = "DXPG";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "select DupleX PaGe side",
                          false, false, false));

            command = "DZP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Draw to Zero - relative Position",
                          false, false, false));

            command = "EMCR";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Enable MaCRo depending on paper source",
                          false, false, false));

            command = "ENDB";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "END a two - dimensional Barcode string",
                          false, false, false));

            command = "ENDC";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "END carbon Copy",
                          false, false, false));

            command = "ENDD";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "END Dump",
                          false, false, false));

            command = "ENDM";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "END Macro",
                          false, false, false));

            command = "ENDR";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "END Raster data",
                          false, false, false));

            command = "EPL";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "select EcoPrint Level",
                          false, false, false));

            command = "EXIT";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "EXIT from PRESCRIBE mode",
                          false, true, false));

            command = "FDIR";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "MP tray Feed DIRection",
                          false, false, false));

            command = "FILL";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "FILL closed path",
                          false, false, false));

            command = "FLAT";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "set FLATness",
                          false, false, false));

            command = "FLST";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "print Font LiST",
                          false, false, false));

            command = "FOLD";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "FOLD printed pages",
                          false, false, false));

            command = "FONT";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "change current FONT",
                          false, false, false));

            command = "FPAT";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "generate Fill PATtern",
                          false, false, false));

            command = "FRPO";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Firmware RePrOgram",
                          false, false, false));

            /*
            command = "FRPO INIT";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "FRPO - INITialize"));
            */

            command = "FSET";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "change current Font SETting by characteristic",
                          false, false, false));

            command = "FTMD";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "bitmap FonT MoDe",
                          false, false, false));

            command = "GPAT";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "set Gray PATtern",
                          false, false, false));

            command = "GRAY";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "represent GRAY",
                          false, false, false));

            command = "GRRD";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "GRaphic data ReaD",
                          false, false, false));

            command = "HUE";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "adjust HUE",
                          false, false, false));

            command = "INTL";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "InterNaTionaL characters",
                          false, false, false));

            command = "JOBD";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "JOB Deletion",
                          false, false, false));

            command = "JOBL";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "print JOB List",
                          false, false, false));

            command = "JOBO";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "JOB Output",
                          false, false, false));

            command = "JOBP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "JOB, print with Print options",
                          false, false, false));

            command = "JOBS";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "JOB Start",
                          false, false, false));

            command = "JOBT";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "JOB Terminate",
                          false, false, false));

            command = "JOG";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "JOG output stacks for separation",
                          false, false, false));

            command = "LAPI";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "LoaD API Program",
                          false, false, false));

            command = "LDFC";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "LoaD Font Character",
                          false, false, false));

            command = "LDFN";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "LoaD FoNt",
                          false, false, false));

            /*
            command = "LDFN C";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "generate bitmap character for LoaDing FoNt"));

            command = "LDFN F";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "create header for LoaDing FoNt"));

            command = "LDFN S";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "LoaD truetype FoNt"));
            */
            command = "LGHT";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "adjust LiGHTness",
                          false, false, false));

            command = "MAP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Move to Absolute Position",
                          false, false, false));

            command = "MCLR";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Match CoLoR",
                          false, false, false));

            command = "MCRO";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "define MaCRO",
                          false, false, false));

            command = "MDAT";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "set MeDia type Attribute",
                          false, false, false));

            command = "MID";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Multi - tray ID",
                          false, false, false));

            command = "MPSS";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "e - MPS Storage",
                          false, false, false));

            command = "MRP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Move to Relative Position",
                          false, false, false));

            command = "MRPA";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Move to Relative Position specified by Angle",
                          false, false, false));

            command = "MSTK";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "select Mailbox STacKer",
                          false, false, false));

            command = "MTYP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "select Media TYPe",
                          false, false, false));

            command = "MZP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Move to Zero - relative Position",
                          false, false, false));

            command = "NEWP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "start   NEW Path",
                          false, false, false));

            command = "OTRY";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "select paper Output TRaY",
                          false, false, false));

            command = "PAGE";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "start   new PAGE",
                          false, false, false));

            command = "PANT";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "create PANTone color palette",
                          false, false, false));

            command = "PARC";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "in Path, draw ARC",
                          false, false, false));

            command = "PAT";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "select fill PATtern",
                          false, false, false));

            command = "PCRP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "in Path, Curve to Relative Position",
                          false, false, false));

            command = "PCZP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "in Path, Curve to Zero-relative Position",
                          false, false, false));

            command = "PDIR";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "set Print DIRection",
                          false, false, false));

            command = "PDRP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "in Path, Draw to Relative Position",
                          false, false, false));

            command = "PDZP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "in Path, Draw to Zero-relative Position",
                          false, false, false));

            command = "PELP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "in Path, draw ELlipse",
                          false, false, false));

            command = "PIE";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "draw PIE chart",
                          false, false, false));

            command = "PMRA";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "in Path, Move to Relative position specified by Angle",
                          false, false, false));

            command = "PMRP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "in Path, Move to Relative Position",
                          false, false, false));

            command = "PMZP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "in Path, Move to Zero-relative Position",
                          false, false, false));

            command = "PNCH";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "PuNCH",
                          false, false, false));

            command = "PRBX";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "in Path, draw Round BoX",
                          false, false, false));

            command = "PRRC";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "in Path, at Relative position, draw aRC",
                          false, false, false));

            command = "PSRC";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "select Paper SouRCe",
                          false, false, false));

            command = "PXPL";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "PiXel PLacement",
                          false, false, false));

            command = "RCLT";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Rotated CoLlaTion",
                          false, false, false));

            command = "RDMP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Received-data DuMP",
                          false, false, false));

            command = "RES";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "RESet",
                          false, false, false));

            command = "RESL";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "select RESoLution",
                          false, false, false));

            command = "RGBL";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "control RGB Level",
                          false, false, false));

            command = "RGST";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "offset ReGiSTration",
                          false, false, false));

            command = "RPCS";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Return to Previous Code Set",
                          false, false, false));

            command = "RPF";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Return to Previous Font",
                          false, false, false));

            command = "RPG";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Return to Previous Graphics state",
                          false, false, false));

            command = "RPP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Return to Previous Position",
                          false, false, false));

            command = "RPPL";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Return to Previous PaLette",
                          false, false, false));

            command = "RPU";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Return to Previous Unit",
                          false, false, false));

            command = "RTTX";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "RoTate TeXt",
                          false, false, false));

            command = "RTXT";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "print Right-aligned TeXT",
                          false, false, false));

            command = "RVCD";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "ReceiVe Compressed raster Data",
                          false, false, false));

            command = "RVCL";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "ReceiVe CoLor raster data",
                          false, false, false));

            command = "RVRD";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "ReceiVe Raster Data",
                          false, false, false));

            command = "RWER";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Read/Write External Resource",
                          false, false, false));
            /*
            command = "RWER D";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "[Read/Write External Resource] Delete data on external media"));

            command = "RWER F";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "[Read/Write External Resource] Format external media"));

            command = "RWER I";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "[Read/Write External Resource] automatically print partition Information"));

            command = "RWER L";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "[Read/Write External Resource] print partition List"));

            command = "RWER R";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "[Read/Write External Resource] Read data from external media"));

            command = "RWER S";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "[Read/Write External Resource] Store TrueType font"));

            command = "RWER T";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "[Read/Write External Resource] set Terminate string"));

            command = "RWER W";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "[Read/Write External Resource] Write data to external media"));

            */

            command = "RWRF";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Read/Write Resource File",
                          false, false, false));

            /*
            command = "RWRF D";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "[Read/Write Resource File] Delete data on external device"));

            command = "RWRF F";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "[Read/Write Resource File] Format external device"));
            command = "RWRF L";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "[Read/Write Resource File] print resource file List"));

            command = "RWRF P";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "[Read/Write Resource File] set hidden file"));

            command = "RWRF R";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "[Read/Write Resource File] Read"));

            command = "RWRF T";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "[Read/Write Resource File] set Terminate string"));

            command = "RWRF W";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "[Read/Write Resource File] Write data to external device"));

             */
             
             command = "SATU";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "adjust SATUration level",
                          false, false, false));

            command = "SBM";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set Bottom Margin",
                          false, false, false));

            command = "SCAP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set line CAP",
                          false, false, false));

            command = "SCCS";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Save Current Code Set",
                          false, false, false));

            command = "SCF";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Save Current Font",
                          false, false, false));

            command = "SCG";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Save Current Graphics state",
                          false, false, false));

            command = "SCOL";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Select COLor",
                          false, false, false));

            command = "SCP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Save Current Position",
                          false, false, false));

            command = "SCPI";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set Characters Per Inch",
                          false, false, false));

            command = "SCPL";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Save Current PaLette",
                          false, false, false));

            command = "SCRC";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set Command Recognition Character",
                          false, false, true));

            command = "SCS";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set Character Spacing",
                          false, false, false));

            command = "SCSZ";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set Custom paper SiZe",
                          false, false, false));

            command = "SCU";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Save Current Unit",
                          false, false, false));

            command = "SDP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Store Dash Pattern",
                          false, false, false));

            command = "SEM";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set Emulation Mode",
                          false, false, false));

            command = "SETF";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "SET alternate Font",
                          false, false, false));

            command = "SFA";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set bitmap Font Attributes",
                          false, false, false));

            command = "SFNT";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Select current FoNT by typeface",
                          false, false, false));

            command = "SGPC";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set kcGl Pen Color",
                          false, false, false));

            command = "SHMI";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set HMI",
                          false, false, false));

            command = "SIMG";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set IMaGe model",
                          false, false, false));

            command = "SIMP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "create SIMPle color palette",
                          false, false, false));

            command = "SIR";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set Image Refinement level",
                          false, false, false));

            command = "SLJN";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set Line JoiN",
                          false, false, false));

            command = "SLM";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set Left Margin",
                          false, false, false));

            command = "SLPI";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set Lines Per Inch",
                          false, false, false));

            command = "SLPP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set Lines Per Page",
                          false, false, false));

            command = "SLS";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set Line Spacing",
                          false, false, false));

            command = "SMLT";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set Miter LimiT",
                          false, false, false));

            command = "SMNT";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set MoNiTor simulation",
                          false, false, false));

            command = "SPAL";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Select PALette",
                          false, false, false));

            command = "SPD";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set Pen Diameter",
                          false, false, false));

            command = "SPL";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set Page Length",
                          false, false, false));

            command = "SPO";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set Page Orientation",
                          false, false, false));

            command = "SPSZ";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set Paper SiZe",
                          false, false, false));

            command = "SPW";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set Page Width",
                          false, false, false));

            command = "SRM";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set Right Margin",
                          false, false, false));

            command = "SRO";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set Raster Options",
                          false, false, false));

            command = "SROP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set Raster Operation",
                          false, false, false));

            command = "SSTK";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "select Sorter STacKer",
                          false, false, false));

            command = "STAK";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "select paper STAcKer",
                          false, false, false));

            command = "STAT";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "STATus",
                          false, false, false));

            command = "STM";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set Top Margin",
                          false, false, false));

            command = "STPC";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "set STaPle Counter",
                          false, false, false));

            command = "STPL";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "STaPLe",
                          false, false, false));

            command = "STR";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "SeT dot Resolution",
                          false, false, false));

            command = "STRK";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "STRoKe current path",
                          false, false, false));

            command = "SULP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Set UnderLine Parameters",
                          false, false, false));

            command = "TATR";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "apply Tray Attributes",
                          false, false, false));

            command = "TEXT";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "print TEXT",
                          false, false, false));

            command = "TPRS";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Text PaRSing",
                          false, false, false));

            command = "TRSM";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "TRansparency Separate Mode",
                          false, false, false));

            command = "UNIT";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "set UNIT of measurement",
                          false, false, false));

            command = "UOM";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Unit Of Measurement per dots",
                          false, false, false));

            command = "VMAL";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Virtual Mailbox Alias",
                          false, false, false));

            command = "VMOB";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "Virtual Mailbox Output Bin",
                          false, false, false));

            command = "VMPW";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "set Virtual Mailbox PassWord",
                          false, false, false));

            command = "WIDE";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "set WIDE A4 mode",
                          false, false, false));

            command = "WRED";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "WRite EnD",
                          false, false, false));

            command = "XBAR";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "print two dimensional barcode",
                          false, false, false));

            command = "XBCP";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "select barcode options",
                          false, false, false));

            /*
            command = "XBCP 0";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "select barcode type/reset all other XBCP parameters"));

            command = "XBCP 1";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "specify narrowest element width"));

            command = "XBCP 2";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "specify error correction level by percentage"));

            command = "XBCP 3";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "set error correction level"));

            command = "XBCP 4";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "set data code word rows"));

            command = "XBCP 5";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "set data code word columns"));

            command = "XBCP 6";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "determine aspect ratio of vertical height and horizontal width"));

            command = "XBCP 7";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "determine bar height"));

            command = "XBCP 8";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "automatically set rows and columns"));

            command = "XBCP 9";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "enable truncation"));

            command = "XBCP 10";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "enable file name"));

            command = "XBCP 11";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "enable block count"));

            command = "XBCP 12";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "enable time stamp"));

            command = "XBCP 13";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "enable sender ID"));

            command = "XBCP 14";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "enable addressee ID"));

            command = "XBCP 15";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "enable file size"));

            command = "XBCP 16";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "enable checksum"));

            command = "XBCP 17";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "allow control of file ID"));

            command = "XBCP 18";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "enable Macro PDF417 symbol mode"));

            command = "XBCP 19";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "position symbols at the specified locations"));

            
            */

            command = "XBUF";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "define a BUFfer name",
                          false, false, false));

            command = "XPAT";
            _cmds.Add(command,
                      new PrescribeCommand(
                          command,
                          "generate eXpanded fill PATtern",
                          false, false, false));

            _cmdCount = _cmds.Count;
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
            PrescribeCommand cmd;

            _cmdUnknown.resetStatistics ();
            _cmdIntro.resetStatistics ();

            foreach (KeyValuePair<String, PrescribeCommand> kvp in _cmds)
            {
                cmd = kvp.Value;

                cmd.resetStatistics ();
            }
        }
    }
}