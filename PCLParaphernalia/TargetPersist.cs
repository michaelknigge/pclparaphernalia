using Microsoft.Win32;
using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class manages persistent storage of Target options.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class TargetPersist
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Fields (class variables).                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        const String _mainKey             = MainForm._regMainKey;
  
        const String _subKeyTarget            = "Target";
        const String _subKeyTargetFile        = "File";
        const String _subKeyTargetPrinter     = "Printer";
        const String _subKeyTargetNetPrinter  = "NetPrinter";
        const String _subKeyTargetWinPrinter  = "WinPrinter";
        const String _subKeyWorkFolder        = "WorkFolder";

        const String _nameIndxTargetType  = "IndxTargetType";
        const String _nameFilename        = "Filename";
        const String _nameFoldername      = "Foldername";
        const String _namePrintername     = "Printername";
        const String _nameIPAddress       = "IPAddress";
        const String _namePort            = "Port";
        const String _nameTimeoutSend     = "TimeoutMsecsSend";
        const String _nameTimeoutReceive  = "TimeoutMsecsReceive";

        const String _defaultFilename     = "ItemNoLongerUsed";
        const String _defaultPrintername  = "<None>";
        const String _defaultIPAddress    = "192.168.0.98";

        const Int32 _indexZero            = 0;
        const Int32 _defaultNetPort       = 9100;

        const Int32 _defaultNetTimeoutSend    = 15000;
        const Int32 _defaultNetTimeoutReceive = 10000;

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a C o m m o n                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored common Target data.                                //
        // Missing items are given default values.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataCommon (ref Int32  indxTargetType)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyTarget;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                indxTargetType = (Int32)subKey.GetValue(_nameIndxTargetType,
                                                        _indexZero);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a N e t P r i n t e r                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored Target network printer data.                       //
        // Missing items are given default values.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataNetPrinter (ref String ipAddress,
                                               ref Int32  port,
                                               ref Int32  timeoutSend,
                                               ref Int32  timeoutReceive)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key = _subKeyTarget + "\\" + _subKeyTargetNetPrinter;

            //----------------------------------------------------------------//

            using (RegistryKey subKey = keyMain.CreateSubKey (_subKeyTarget))
            {
                if (Helper_RegKey.keyExists (subKey, _subKeyTargetPrinter))
                    // update from v2_5_0_0
                    Helper_RegKey.renameKey (subKey,
                                           _subKeyTargetPrinter,
                                           _subKeyTargetNetPrinter);
            }

            //----------------------------------------------------------------//

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                ipAddress = (String)subKey.GetValue (_nameIPAddress,
                                                     _defaultIPAddress);

                port = (Int32)subKey.GetValue (_namePort,
                                               _defaultNetPort);

                timeoutSend    = (Int32)subKey.GetValue (_nameTimeoutSend,
                                               _defaultNetTimeoutSend);

                timeoutReceive = (Int32)subKey.GetValue (_nameTimeoutReceive,
                                               _defaultNetTimeoutReceive);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a W i n P r i n t e r                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored Target windows printer data.                       //
        // Missing items are given default values.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataWinPrinter (ref String printerName)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key = _subKeyTarget + "\\" + _subKeyTargetWinPrinter;

            //----------------------------------------------------------------//

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                printerName = (String) subKey.GetValue (_namePrintername,
                                                        _defaultPrintername);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a W o r k F ol d e r                                 //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored default working folder data.                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadDataWorkFolder(ref String foldername)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyTarget + "\\" + _subKeyWorkFolder;

            String defWorkFolder = Environment.GetEnvironmentVariable("TMP");

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                foldername = (String)subKey.GetValue(_nameFoldername,
                                                     defWorkFolder);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a C o m m o n                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current common Target data.                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataCommon(Int32 indxTargetType)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key = _subKeyTarget;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                subKey.SetValue (_nameIndxTargetType,
                                 indxTargetType,
                                 RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a N e t P r i n t e r                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current Target Network Printer data.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataNetPrinter (Int32  indxTargetType,
                                               String ipAddress,
                                               Int32  port,
                                               Int32  timeoutSend,
                                               Int32  timeoutReceive)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key = _subKeyTarget;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                subKey.SetValue (_nameIndxTargetType,
                                 indxTargetType,
                                 RegistryValueKind.DWord);
            }

            key = _subKeyTarget + "\\" + _subKeyTargetNetPrinter;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                subKey.SetValue (_nameIPAddress,
                                 ipAddress,
                                 RegistryValueKind.String);

                subKey.SetValue (_namePort,
                                 port,
                                 RegistryValueKind.DWord);

                subKey.SetValue (_nameTimeoutSend,
                                 timeoutSend,
                                 RegistryValueKind.DWord);

                subKey.SetValue (_nameTimeoutReceive,
                                 timeoutReceive,
                                 RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a W i n P r i n t e r                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current Target Windows Printer data.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataWinPrinter (Int32 indxTargetType,
                                               String printerName)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key = _subKeyTarget;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                subKey.SetValue (_nameIndxTargetType,
                                 indxTargetType,
                                 RegistryValueKind.DWord);
            }

            key = _subKeyTarget + "\\" + _subKeyTargetWinPrinter;

            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                subKey.SetValue (_namePrintername,
                                 printerName,
                                 RegistryValueKind.String);
            }
        }
 
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a W o r k F o l d e r                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current default working folder data.                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveDataWorkFolder (String saveFoldername)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey (_mainKey);

            String key = _subKeyTarget + "\\" + _subKeyWorkFolder;


            using (RegistryKey subKey = keyMain.CreateSubKey (key))
            {
                subKey.SetValue (_nameFoldername,
                                saveFoldername,
                                RegistryValueKind.String);
            }
        }
    }
}
