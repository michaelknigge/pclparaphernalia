using Microsoft.Win32;
using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class manages persistent storage of options for the main form.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class MainFormPersist
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Fields (class variables).                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        const String _mainKey = MainForm._regMainKey;

        const String _subKeyVersionData    = "VersionData";
        const String _subKeyWindowState    = "WindowState";

        const String _nameVersionBuild     = "Build";
        const String _nameVersionMajor     = "Major";
        const String _nameVersionMinor     = "Minor";
        const String _nameVersionRevision  = "Revision";

        const String _nameMainWindowLeft   = "MainWindowLeft";
        const String _nameMainWindowTop    = "MainWindowTop";
        const String _nameMainWindowHeight = "MainWindowHeight";
        const String _nameMainWindowWidth  = "MainWindowWidth";
        const String _nameMainWindowScale  = "MainWindowScale";

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d V e r s i o n D a t a                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored version data; first used after version 2.5.0.0     //
        // Missing items are given default values.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadVersionData (ref Int32 major,
                                            ref Int32 minor,
                                            ref Int32 build,
                                            ref Int32 revision)

        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyVersionData;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                major    = (Int32) subKey.GetValue(_nameVersionMajor,    -1);
                minor    = (Int32) subKey.GetValue(_nameVersionMinor,    -1);
                build    = (Int32) subKey.GetValue(_nameVersionBuild,    -1);
                revision = (Int32) subKey.GetValue(_nameVersionRevision, -1);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d W i n d o w D a t a                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored windows state data.                                //
        // Missing items are given default values.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadWindowData (ref Int32 left,
                                           ref Int32 top,
                                           ref Int32 height,
                                           ref Int32 width,
                                           ref Int32 scale)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyWindowState;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                left   = (Int32)subKey.GetValue(_nameMainWindowLeft,   -1);
                top    = (Int32)subKey.GetValue(_nameMainWindowTop,    -1);
                height = (Int32)subKey.GetValue(_nameMainWindowHeight, -1);
                width  = (Int32)subKey.GetValue(_nameMainWindowWidth,  -1);
                scale  = (Int32)subKey.GetValue(_nameMainWindowScale,  100);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e V e r s i o n D a t a                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current version data.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveVersionData (Int32 major,
                                            Int32 minor,
                                            Int32 build,
                                            Int32 revision)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyVersionData;

            using(RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue(_nameVersionMajor,    major,
                                RegistryValueKind.DWord);
                subKey.SetValue(_nameVersionMinor,    minor,
                                RegistryValueKind.DWord);
                subKey.SetValue(_nameVersionBuild,    build,
                                RegistryValueKind.DWord);
                subKey.SetValue(_nameVersionRevision, revision,
                                RegistryValueKind.DWord);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e W i n d o w D a t a                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current window state.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveWindowData (Int32 mwLeft,
                                          Int32 mwTop,
                                          Int32 mwHeight,
                                          Int32 mwWidth,
                                          Int32 mwScale)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyWindowState;

            using(RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue(_nameMainWindowLeft,   mwLeft,
                                                    RegistryValueKind.DWord);
                subKey.SetValue(_nameMainWindowTop,    mwTop,
                                                    RegistryValueKind.DWord);
                subKey.SetValue(_nameMainWindowHeight, mwHeight,
                                                    RegistryValueKind.DWord);
                subKey.SetValue(_nameMainWindowWidth,  mwWidth,
                                                    RegistryValueKind.DWord);
                subKey.SetValue(_nameMainWindowScale,  mwScale,
                                                    RegistryValueKind.DWord);
            }
        }
    }
}
