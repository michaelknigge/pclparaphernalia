using Microsoft.Win32;
using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class manages persistent storage of common Tool options.
    /// 
    /// © Chris Hutchinson 2011
    /// 
    /// </summary>

    static class ToolCommonPersist
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Fields (class variables).                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        const String _mainKey             = MainForm._regMainKey;
  
        const String _subKeyTools         = "Tools";

        const String _nameIndxToolType    = "IndxToolType";

        const Int32 _indexZero            = 0;

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d D a t a                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored common Tool data.                                  //
        // Missing items are given default values.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void loadData(ref Int32  indxToolType)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyTools;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                indxToolType = (Int32)subKey.GetValue(_nameIndxToolType,
                                                      _indexZero);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s a v e D a t a                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Store current common Tool data.                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void saveData(Int32  indxToolType)
        {
            RegistryKey keyMain =
                Registry.CurrentUser.CreateSubKey(_mainKey);

            String key = _subKeyTools;

            using (RegistryKey subKey = keyMain.CreateSubKey(key))
            {
                subKey.SetValue(_nameIndxToolType,
                                indxToolType,
                                RegistryValueKind.DWord);
            }
        }
    }
}
