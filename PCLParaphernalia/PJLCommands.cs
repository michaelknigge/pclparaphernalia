using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines a set of PJL 'status readback' Command objects.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class PJLCommands
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public enum eRequestType
        {
            None,
            Variable,
            Category,
            FSBinSrc,
            FSDelete,
            FSDirList,
            FSInit,
            FSMkDir,
            FSQuery,
            FSUpload
        }

        public enum eCmdFormat
        {
            None,
            Standard,
            Words
        }

        [System.Reflection.ObfuscationAttribute(Exclude = true)]

        public enum eCmdIndex
        {
            Unknown,
            Null,
            COMMENT,
            DEFAULT,
            DINQUIRE,
            DMCMD,
            DMINFO,
            ECHO,
            ENTER,
            EOJ,
            FSAPPEND,
            FSDELETE,
            FSDIRLIST,
            FSDOWNLOAD,
            FSINIT,
            FSMKDIR,
            FSQUERY,
            FSUPLOAD,
            INFO,
            INITIALIZE,
            INQUIRE,
            JOB,
            OPMSG,
            RDYMSG,
            RESET,
            SET,
            STMSG,
            USAGE,
            USTATUS,
            USTATUSOFF
        }

        public static String nullCmdKey = "<null>";

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static SortedList<String, PJLCommand> _cmds =
            new SortedList<String, PJLCommand> ();

        private static PJLCommand _cmdUnknown;

        private static Int32 _cmdCount;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P J L C o m m a n d s                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        static PJLCommands ()
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

        public static Boolean checkCmd (String     name,
                                        ref String description,
                                        Int32      level)
        {
            Boolean seqKnown = true;

            PJLCommand cmd;

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

            cmd.incrementStatisticsCount (level);

            return seqKnown;
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

            foreach (KeyValuePair<String, PJLCommand> kvp
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

                row[0] = _cmdUnknown.Name;
                row[1] = _cmdUnknown.Description;
                row[2] = _cmdUnknown.StatsCtParent;
                row[3] = _cmdUnknown.StatsCtChild;
                row[4] = _cmdUnknown.StatsCtTotal;

                table.Rows.Add (row);
            }

            //----------------------------------------------------------------//

            foreach (KeyValuePair<String, PJLCommand> kvp
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
            row[1] = "_____________";
            row[2] = "";
            row[3] = "";
            row[4] = "";

            table.Rows.Add (row);

            row = table.NewRow ();

            row[0] = "";
            row[1] = "PJL commands:";
            row[2] = "";
            row[3] = "";
            row[4] = "";

            table.Rows.Add (row);

            row = table.NewRow ();

            row[0] = "";
            row[1] = "¯¯¯¯¯¯¯¯¯¯¯¯¯";
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

        public static String getDesc (eCmdIndex key)
        {
            return _cmds[key.ToString()].Description;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t F o r m a t                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return format of command.                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static eCmdFormat getFormat (eCmdIndex key)
        {
            return _cmds[key.ToString()].Format;
        }

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

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t T y p e                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return type of command.                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static eRequestType getType (eCmdIndex key)
        {
            return _cmds[key.ToString()].Type;
        }

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
            eCmdIndex indx;
            
            indx = eCmdIndex.Unknown;
            _cmdUnknown = 
                new PJLCommand (indx,
                                eCmdFormat.Standard,
                                eRequestType.None,
                                "*** Unknown command ***");

            indx = eCmdIndex.Null;
            _cmds.Add (nullCmdKey,
                new PJLCommand (indx,
                                eCmdFormat.None,
                                eRequestType.None,
                                "Null (no command)"));

            indx = eCmdIndex.COMMENT;
            _cmds.Add (indx.ToString(),
                new PJLCommand (indx,
                                eCmdFormat.Words,
                                eRequestType.None,
                                "Comment"));

            indx = eCmdIndex.DEFAULT;
            _cmds.Add (indx.ToString (),
                new PJLCommand (indx,
                                eCmdFormat.Standard,
                                eRequestType.None,
                                "Set environment variable default"));

            indx = eCmdIndex.DINQUIRE;
            _cmds.Add (indx.ToString (),
                new PJLCommand (indx,
                                eCmdFormat.Standard,
                                eRequestType.Variable,
                                "Request default value of environment variable"));

            indx = eCmdIndex.DMCMD;
            _cmds.Add (indx.ToString (),
                new PJLCommand (indx,
                                eCmdFormat.Standard,
                                eRequestType.None,
                                "Process PML request"));

            indx = eCmdIndex.DMINFO;
            _cmds.Add (indx.ToString (),
                new PJLCommand (indx,
                                eCmdFormat.Standard,
                                eRequestType.None,
                                "Process PML request & read response"));

            indx = eCmdIndex.ECHO;
            _cmds.Add (indx.ToString (),
                new PJLCommand (indx,
                                eCmdFormat.Words,
                                eRequestType.None,
                                "Echo value to host"));

            indx = eCmdIndex.ENTER;
            _cmds.Add (indx.ToString (),
                new PJLCommand (indx,
                                eCmdFormat.Standard,
                                eRequestType.None,
                                "Enter language"));

            indx = eCmdIndex.EOJ;
            _cmds.Add (indx.ToString (),
                new PJLCommand (indx,
                                eCmdFormat.Standard,
                                eRequestType.None,
                                "Job end"));

            indx = eCmdIndex.FSAPPEND;
            _cmds.Add (indx.ToString (),
                new PJLCommand (indx,
                                eCmdFormat.Standard,
                                eRequestType.FSBinSrc,
                                "File System: file append"));

            indx = eCmdIndex.FSDELETE;
            _cmds.Add (indx.ToString (),
                new PJLCommand (indx,
                                eCmdFormat.Standard,
                                eRequestType.FSDelete,
                                "File System: file delete"));

            indx = eCmdIndex.FSDIRLIST;
            _cmds.Add (indx.ToString (),
                new PJLCommand (indx,
                                eCmdFormat.Standard,
                                eRequestType.FSDirList,
                                "File System: return directory list"));

            indx = eCmdIndex.FSDOWNLOAD;
            _cmds.Add (indx.ToString (),
                new PJLCommand (indx,
                                eCmdFormat.Standard,
                                eRequestType.FSBinSrc,
                                "File System: download file to printer"));

            indx = eCmdIndex.FSINIT;
            _cmds.Add (indx.ToString (),
                new PJLCommand (indx,
                                eCmdFormat.Standard,
                                eRequestType.FSInit,
                                "File System: initialise"));

            indx = eCmdIndex.FSMKDIR;
            _cmds.Add (indx.ToString (),
                new PJLCommand (indx,
                                eCmdFormat.Standard,
                                eRequestType.FSMkDir,
                                "File System: create directory"));

            indx = eCmdIndex.FSQUERY;
            _cmds.Add (indx.ToString (),
                new PJLCommand (indx,
                                eCmdFormat.Standard,
                                eRequestType.FSQuery,
                                "File System: query"));

            indx = eCmdIndex.FSUPLOAD;
            _cmds.Add (indx.ToString (),
                new PJLCommand (indx,
                                eCmdFormat.Standard,
                                eRequestType.FSUpload,
                                "File System: upload file to host"));

            indx = eCmdIndex.INFO;
            _cmds.Add (indx.ToString (),
                new PJLCommand (indx,
                                eCmdFormat.Standard,
                                eRequestType.Category,
                                "Request information category details"));

            indx = eCmdIndex.INITIALIZE;
            _cmds.Add (indx.ToString (),
                new PJLCommand (indx,
                                eCmdFormat.Standard,
                                eRequestType.None,
                                "Reset environment variables to factory defaults"));

            indx = eCmdIndex.INQUIRE;
            _cmds.Add (indx.ToString (),
                new PJLCommand (indx,
                                eCmdFormat.Standard,
                                eRequestType.Variable,
                                "Request value of environment variable"));

            indx = eCmdIndex.JOB;
            _cmds.Add (indx.ToString (),
                new PJLCommand (indx,
                                eCmdFormat.Standard,
                                eRequestType.None,
                                "Job start"));

            indx = eCmdIndex.OPMSG;
            _cmds.Add (indx.ToString (),
                new PJLCommand (indx,
                                eCmdFormat.Standard,
                                eRequestType.None,
                                "Display Operator message"));

            indx = eCmdIndex.RDYMSG;
            _cmds.Add (indx.ToString (),
                new PJLCommand (indx,
                                eCmdFormat.Standard,
                                eRequestType.None,
                                "Display Ready message"));

            indx = eCmdIndex.RESET;
            _cmds.Add (indx.ToString (),
                new PJLCommand (indx,
                                eCmdFormat.Standard,
                                eRequestType.None,
                                "Reset environment variables to defaults"));

            indx = eCmdIndex.SET;
            _cmds.Add (indx.ToString (),
                new PJLCommand (indx,
                                eCmdFormat.Standard,
                                eRequestType.None,
                                "Set environment variable"));

            indx = eCmdIndex.STMSG;
            _cmds.Add (indx.ToString (),
                new PJLCommand (indx,
                                eCmdFormat.Standard,
                                eRequestType.None,
                                "Display Status message"));

            indx = eCmdIndex.USAGE;
            _cmds.Add (indx.ToString (),
                new PJLCommand (indx,
                                eCmdFormat.Standard,
                                eRequestType.None,
                                "Usage (proprietary extension)"));

            indx = eCmdIndex.USTATUS;
            _cmds.Add (indx.ToString (),
                new PJLCommand (indx,
                                eCmdFormat.Standard,
                                eRequestType.None,
                                "Allow printer to send unsolicited messages"));

            indx = eCmdIndex.USTATUSOFF;
            _cmds.Add (indx.ToString (),
                new PJLCommand (indx,
                                eCmdFormat.Standard,
                                eRequestType.None,
                                "Stop printer sending unsolicited messages"));

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
            PJLCommand cmd;

            _cmdUnknown.resetStatistics ();

            foreach (KeyValuePair<String, PJLCommand> kvp in _cmds)
            {
                cmd = kvp.Value;

                cmd.resetStatistics ();
            }
        }
    }
}