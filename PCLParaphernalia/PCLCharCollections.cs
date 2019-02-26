using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines the sets of PCL Character Complement/Requirement
    /// Collection bits, as used in (unbound) Font headers and Symbol Set
    /// definitions.
    /// 
    /// © Chris Hutchinson 2013
    /// 
    /// </summary>

    static class PCLCharCollections
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public const Int32 itemsCt = 64;

        public enum eBitType
        {
            Index_0,
            Index_1,
            Index_2,
            Collection
        }

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//
        
        private static Int32 _collsCount;

        private static SortedList<Int32, PCLCharCollection> _collsList =
              new SortedList<Int32, PCLCharCollection>();

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L C h a r C o l l e c t i o n s                                //
        //                                                                    //
        //--------------------------------------------------------------------//

        static PCLCharCollections()
        {
            populateTable();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C o l l s C o u n t                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return count of collections.                                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 getCollsCount()
        {
            return _collsCount;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t B i t N o                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return bit number associated with specified collection item.       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 getBitNo (Int32 index)
        {
            return _collsList [index].getBitNo ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t B i t T y p e                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return bit type associated with specified collection item.         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static eBitType getBitType (Int32 index)
        {
            return _collsList [index].getBitType ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t D e s c M S L                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return MSL description associated with specified collection item.  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getDescMSL (Int32 index)
        {
            return _collsList [index].getDescMSL ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t D e s c U n i c o d e                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return Unicode description associated with specified collection    //
        // item.                                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getDescUnicode (Int32 index)
        {
            return _collsList [index].getDescUnicode ();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I n d e x F o r K e y                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the list index for the specified (bit number) key.          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 getindexForKey (Int32 bitNo)
        {
            return _collsList.IndexOfKey (bitNo);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p o p u l a t e T a b l e                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Populate the table of simple PCL sequences.                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void populateTable()
        {
            Int32 bitNo;

            bitNo = 0;                                                  //  0 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Index_0,
                    bitNo,
                    "Bit  0: Symbol Index id 2^0",
                    "Bit  0: Symbol Index id 2^0"));

            bitNo = 1;                                                  //  1 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Index_1,
                    bitNo,
                    "Bit  1: Symbol Index id 2^1",
                    "Bit  1: Symbol Index id 2^1"));

            bitNo = 2;                                                  //  2 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Index_2,
                    bitNo,
                    "Bit  2: Symbol Index id 2^2",
                    "Bit  2: Symbol Index id 2^2"));

            bitNo = 3;                                                  //  3 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit  3: Miscellaneous: reserved",
                    "Bit  3: Cyrillic, Arabic, Greek, Hebrew: reserved"));

            bitNo = 4;                                                  //  4 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit  4: Miscellaneous: reserved",
                    "Bit  4: Cyrillic, Arabic, Greek, Hebrew: reserved"));

            bitNo = 5;                                                  //  5 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit  5: Miscellaneous: reserved",
                    "Bit  5: Cyrillic, Arabic, Greek, Hebrew: reserved"));

            bitNo = 6;                                                  //  6 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit  6: Miscellaneous: reserved",
                    "Bit  6: Cyrillic, Arabic, Greek, Hebrew: reserved"));

            bitNo = 7;                                                  //  7 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit  7: Miscellaneous: reserved",
                    "Bit  7: Cyrillic, Arabic, Greek, Hebrew: reserved"));

            bitNo = 8;                                                  //  8 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit  8: Miscellaneous: reserved",
                    "Bit  8: Cyrillic, Arabic, Greek, Hebrew: reserved"));

            bitNo = 9;                                                  //  9 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit  9: Miscellaneous: reserved",
                    "Bit  9: Cyrillic, Arabic, Greek, Hebrew: reserved"));

            bitNo = 10;                                                 // 10 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 10: Miscellaneous: reserved",
                    "Bit 10: Cyrillic, Arabic, Greek, Hebrew: reserved"));

            bitNo = 11;                                                 // 11 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 11: Miscellaneous: reserved",
                    "Bit 11: Cyrillic, Arabic, Greek, Hebrew: reserved"));

            bitNo = 12;                                                 // 12 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 12: Miscellaneous: reserved",
                    "Bit 12: Cyrillic, Arabic, Greek, Hebrew: reserved"));

            bitNo = 13;                                                 // 13 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 13: Miscellaneous: reserved",
                    "Bit 13: Cyrillic, Arabic, Greek, Hebrew: reserved"));

            bitNo = 14;                                                 // 14 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 14: Miscellaneous: reserved",
                    "Bit 14: Cyrillic, Arabic, Greek, Hebrew: reserved"));

            bitNo = 15;                                                 // 15 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 15: Miscellaneous: reserved",
                    "Bit 15: Cyrillic, Arabic, Greek, Hebrew: reserved"));

            bitNo = 16;                                                 // 16 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 16: Miscellaneous: reserved",
                    "Bit 16: Cyrillic, Arabic, Greek, Hebrew: reserved"));

            bitNo = 17;                                                 // 17 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 17: Miscellaneous: reserved",
                    "Bit 17: Cyrillic, Arabic, Greek, Hebrew: reserved"));

            bitNo = 18;                                                 // 18 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 18: Miscellaneous: reserved",
                    "Bit 18: Cyrillic, Arabic, Greek, Hebrew: reserved"));

            bitNo = 19;                                                 // 19 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 19: Miscellaneous: reserved",
                    "Bit 19: Cyrillic, Arabic, Greek, Hebrew: reserved"));

            bitNo = 20;                                                 // 20 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 20: Miscellaneous: reserved",
                    "Bit 20: Cyrillic, Arabic, Greek, Hebrew: reserved"));

            bitNo = 21;                                                 // 21 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 21: Miscellaneous: reserved",
                    "Bit 21: Cyrillic, Arabic, Greek, Hebrew: reserved"));

            bitNo = 22;                                                 // 22 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 22: Miscellaneous: reserved",
                    "Bit 22: Variants: PC Code Pages"));

            bitNo = 23;                                                 // 23 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 23: Miscellaneous: reserved",
                    "Bit 23: Variants: PostScript"));

            bitNo = 24;                                                 // 24 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 24: Miscellaneous: reserved",
                    "Bit 24: Variants: Macintosh"));

            bitNo = 25;                                                 // 25 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 25: Miscellaneous: reserved",
                    "Bit 25: Variants: PCL"));

            bitNo = 26;                                                 // 26 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 26: Miscellaneous: reserved",
                    "Bit 26: Variants: Accents"));

            bitNo = 27;                                                 // 27 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 27: Miscellaneous: reserved",
                    "Bit 27: Variants: Desktop Publishing"));

            bitNo = 28;                                                 // 28 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 28: Miscellaneous: reserved",
                    "Bit 28: Latin 5: Turkish"));

            bitNo = 29;                                                 // 29 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 29: Miscellaneous: reserved",
                    "Bit 29: Latin 2: East European"));

            bitNo = 30;                                                 // 30 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 30: Miscellaneous: reserved",
                    "Bit 30: Latin 1: West European"));

            bitNo = 31;                                                 // 31 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 31: Miscellaneous: reserved",
                    "Bit 31: ASCII"));

            bitNo = 32;                                                 // 32 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 32: Miscellaneous: Dingbats",
                    "Bit 32: Miscellaneous: reserved"));

            bitNo = 33;                                                 // 33 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 33: Miscellaneous: Semi-graphic",
                    "Bit 33: Miscellaneous: reserved"));

            bitNo = 34;                                                 // 34 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 34: Miscellaneous: Math",
                    "Bit 34: Miscellaneous: reserved"));

            bitNo = 35;                                                 // 35 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 35: Miscellaneous: reserved",
                    "Bit 35: Miscellaneous: reserved"));

            bitNo = 36;                                                 // 36 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 36: Miscellaneous: reserved",
                    "Bit 36: Miscellaneous: reserved"));

            bitNo = 37;                                                 // 37 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 37: Miscellaneous: reserved",
                    "Bit 37: Miscellaneous: reserved"));

            bitNo = 38;                                                 // 38 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 38: Miscellaneous: reserved",
                    "Bit 38: Miscellaneous: reserved"));

            bitNo = 39;                                                 // 39 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 39: Miscellaneous: reserved",
                    "Bit 39: Miscellaneous: reserved"));

            bitNo = 40;                                                 // 40 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 40: Miscellaneous: reserved",
                    "Bit 40: Miscellaneous: reserved"));

            bitNo = 41;                                                 // 41 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 41: Miscellaneous: reserved",
                    "Bit 41: Miscellaneous: reserved"));

            bitNo = 42;                                                 // 42 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 42: Miscellaneous: reserved",
                    "Bit 42: Miscellaneous: reserved"));

            bitNo = 43;                                                 // 43 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 43: Miscellaneous: reserved",
                    "Bit 43: Miscellaneous: reserved"));

            bitNo = 44;                                                 // 44 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 44: Miscellaneous: reserved",
                    "Bit 44: Miscellaneous: reserved"));

            bitNo = 45;                                                 // 45 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 45: Miscellaneous: reserved",
                    "Bit 45: Miscellaneous: reserved"));

            bitNo = 46;                                                 // 46 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 46: Miscellaneous: reserved",
                    "Bit 46: Miscellaneous: reserved"));

            bitNo = 47;                                                 // 47 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 47: Miscellaneous: reserved",
                    "Bit 47: Miscellaneous: reserved"));

            bitNo = 48;                                                 // 48 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 48: Hebrew: reserved",
                    "Bit 48: Miscellaneous: reserved"));

            bitNo = 49;                                                 // 49 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 49: Hebrew: reserved",
                    "Bit 49: Miscellaneous: reserved"));

            bitNo = 50;                                                 // 50 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 50: Greek: reserved",
                    "Bit 50: Miscellaneous: reserved"));

            bitNo = 51;                                                 // 51 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 51: Greek: reserved",
                    "Bit 51: Miscellaneous: reserved"));

            bitNo = 52;                                                 // 52 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 52: Arabic: reserved",
                    "Bit 52: Miscellaneous: reserved"));

            bitNo = 53;                                                 // 53 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 53: Arabic: reserved",
                    "Bit 53: Miscellaneous: reserved"));

            bitNo = 54;                                                 // 54 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 54: Arabic: reserved",
                    "Bit 54: Miscellaneous: reserved"));

            bitNo = 55;                                                 // 55 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 55: Cyrillic: reserved",
                    "Bit 55: Miscellaneous: reserved"));

            bitNo = 56;                                                 // 56 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 56: Cyrillic: reserved",
                    "Bit 56: Miscellaneous: reserved"));

            bitNo = 57;                                                 // 57 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 57: Cyrillic: reserved",
                    "Bit 57: Miscellaneous: reserved"));

            bitNo = 58;                                                 // 58 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 58: Latin: reserved",
                    "Bit 58: Miscellaneous: reserved"));

            bitNo = 59;                                                 // 59 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 59: Latin: reserved",
                    "Bit 59: Miscellaneous: reserved"));

            bitNo = 60;                                                 // 60 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 60: Latin: reserved",
                    "Bit 60: Miscellaneous: reserved"));

            bitNo = 61;                                                 // 61 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 61: Latin: Turkish",
                    "Bit 61: Miscellaneous: reserved"));

            bitNo = 62;                                                 // 62 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 62: Latin: East European",
                    "Bit 62: Miscellaneous: reserved"));

            bitNo = 63;                                                 // 63 //
            _collsList.Add (
                bitNo,
                new PCLCharCollection (
                    eBitType.Collection,
                    bitNo,
                    "Bit 63: Latin: Standard",
                    "Bit 63: Miscellaneous: reserved" ));

            _collsCount = _collsList.Count;
        }
    }
}
