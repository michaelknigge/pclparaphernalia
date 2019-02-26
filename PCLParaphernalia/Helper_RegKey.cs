using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines a class which allows a registry key to be renamed (this
    /// function is not available within the .Net framework).
    /// 
    /// © Chris Hutchinson 2014
    /// 
    /// </summary>

    static class Helper_RegKey
    {
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c o p y K e y                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Copy specified (sub)key.                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void copyKey(
            RegistryKey parentKey,
            String sourceKeyName,
            String targetKeyName)
        {
            RegistryKey targetKey = parentKey.CreateSubKey(targetKeyName);

            RegistryKey sourceKey = parentKey.OpenSubKey(sourceKeyName);

            copyKeyContent(sourceKey, targetKey);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c o p y K e y C o n t e n t                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Copy contents of specified (sub)key.                               //
        // This is achieved by:                                               //
        //   -  copying all the old top-level values;                         //
        //   -  for each subkey, recursively copying the contents of the old  //
        //      (sub)key to the new one.                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void copyKeyContent(
                RegistryKey sourceKey,
                RegistryKey targetKey)
        {
            foreach (String valueName in sourceKey.GetValueNames())
            {
                object objValue = sourceKey.GetValue(valueName);

                RegistryValueKind valKind = sourceKey.GetValueKind(valueName);
                
                targetKey.SetValue(valueName, objValue, valKind);
            }

            foreach (String sourceSubKeyName in sourceKey.GetSubKeyNames())
            {
                RegistryKey sourceSubKey =
                    sourceKey.OpenSubKey(sourceSubKeyName);
                RegistryKey targetSubKey =
                    targetKey.CreateSubKey(sourceSubKeyName);

                copyKeyContent(sourceSubKey, targetSubKey);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // k e y E x i s t s                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Check whether or not specified (sub)key exists.                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean keyExists (
            RegistryKey parentKeyName,
            String      subKeyName)
        {
            using (RegistryKey subKey = parentKeyName.OpenSubKey(subKeyName))
            {
                if (subKey == null)
                    return false;
                else
                    return true;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // r e n a m e K e y                                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Rename specified (sub)key.                                         //
        // This is achieved by:                                               //
        //   -  copying the old (sub)key to a new one;                        //
        //   -  recursively copying the contents of the old (sub)key to the   //
        //      new one;                                                      //
        //   -  deleting the old (sub)key.                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void renameKey(
            RegistryKey parentKey,
            String oldSubKeyName,
            String newSubKeyName)
        {
            RegistryKey subKey = parentKey.OpenSubKey(oldSubKeyName);

            if (subKey != null)
            {
                copyKey(parentKey, oldSubKeyName, newSubKeyName);

                parentKey.DeleteSubKeyTree(oldSubKeyName);
            }
        }
    }
}
