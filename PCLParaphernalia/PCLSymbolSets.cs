using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines a set of PCL Symbol Set objects.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class PCLSymbolSets
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        private const Int32 _hex0080 = 128;
        private const Int32 _hex00a0 = 160;
        private const Int32 _hex00c0 = 192;
        private const Int32 _hex00e0 = 224;
        private const Int32 _hex0100 = 256;

        [System.Reflection.ObfuscationAttribute(Exclude = true)]

        public enum eSymSetGroup
        {
            Preset,     // Standard HP-defined symbol sets
            Custom,     // Symbol set specified explicitly via identifier 
            NonStd,     // To allow mapStd of sets with no HP identifier
            Symbol,     // To allow definition of TTF 'symbol' mapStd table
            Unbound,    // Unbound
            Unicode,    // Unicode (symbol set 18N)
            UserSet     // Defined via user-defined symbol set definition file
        };

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static List<PCLSymbolSet> _sets =
            new List<PCLSymbolSet> ();

        private static Int32 _indxSymbol;
        private static Int32 _indxUnbound;
        private static Int32 _indxUnicode;
        private static Int32 _indxUserSet;

        private static Int32 _setsCountCustom;
        private static Int32 _setsCountPreset;
        private static Int32 _setsCountUnicode;
        private static Int32 _setsCountUserSet;

        private static Int32 _setsCountMapped;
        private static Int32 _setsCountStd;
        private static Int32 _setsCountTotal;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L S y m b o l S e t s                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        static PCLSymbolSets()
        {
            populateSymbolSetTable();
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

            foreach (PCLSymbolSet v in _sets)
            {
                if ((v.Group == eSymSetGroup.Preset)  ||
                    (v.Group == eSymSetGroup.Unicode) ||
                    (v.Group == eSymSetGroup.Unbound))
                {
                    count++;
                    grid.Items.Add (v);
                }
                else if (v.Group == eSymSetGroup.NonStd)
                {
                    count++;
                    grid.Items.Add (v);
                }
            }

            return count;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C o u n t                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return count of Symbol Set definitions.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 getCount()
        {
            return _setsCountTotal;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C o u n t M a p p e d                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return count of Symbol Sets which have a defined mapping.          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 getCountMapped ()
        {
            return _setsCountMapped;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C o u n t S t d                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return count of 'standard' Symbol Sets.                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 getCountStd ()
        {
            return _setsCountStd;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C o u n t U s e r S e t                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return count of 'user-defined' Symbol Sets entries.                //
        // Should always be one! ***** TODO - check this  *****               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 getCountUserSet ()
        {
            return _setsCountUserSet;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t G r o u p                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return symbol set group.                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static eSymSetGroup getGroup (Int32 selection)
        {
            return _sets [selection].Group;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I d                                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return PCL ID associated with specified Symbol Set index.          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getId(Int32 selection)
        {
            return _sets[selection].Id;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I d A l p h a                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the alphabetic part of the PCL identifier string.           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Byte getIdAlpha(Int32 selection)
        {
            return _sets[selection].IdAlpha;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I d N u m                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the numneric part of the PCL identifier string.             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt16 getIdNum(Int32 selection)
        {
            return _sets[selection].IdNum;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I n d e x F o r I d                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return index of Symbol Set which matches supplied 'Kind1' value.   //
        // Ignore the User defined entry.                                     // 
        // TODO: must be a more efficient method (use Map?).                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 getIndexForId(UInt16 kind1)
        {
            for (int i = 0; i < _setsCountTotal; i++)
            {
                if ((getKind1 (i) == kind1) &&
                    (getGroup (i) != eSymSetGroup.UserSet))
                    return i;
            }

            return -1;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t N a m e F o r I d                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return name of Symbol Set which matches supplied 'Kind1' value.    //
        // Ignore the User defined entry.                                     // 
        // TODO: must be a more efficient method (use Map?).                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void getNameForId(UInt16     kind1,
                                        ref String name)
        {
            Int32 index = -1;

            index = getIndexForId (kind1);

            if (index == -1)
                name =  "unknown";
            else
                name = getName(index);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I n d i c e s M a p p e d                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return indices of Symbol Sets which have a defined mapping.        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void getIndicesMapped (Int32 firstIndex,
                                             ref Int32 [] subset)
        {
            Int32 index = firstIndex;

            for (Int32 i = 0; i < _setsCountTotal; i++)
            {
                if (_sets [i].FlagMapped)
                {
                    subset [index] = i;

                    index++;
                }
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t K i n d 1                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return 'Kind1' symbol set number associated with specified Symbol  //
        // Set index.                                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt16 getKind1(Int32 selection)
        {
            return _sets[selection].Kind1;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t M a p A r r a y M a x                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Returns the maximum codepoint defined in the mapping array(s) for  //
        // this symbol set.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt16 getMapArrayMax (Int32   selection)
        {
            return _sets[selection].MapArrayMax;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t M a p A r r a y                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return array defining mapping of the character set to Unicode for  //
        // the specified Symbol Set index.                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt16 [] getMapArray (Int32   selection,
                                             Boolean flagMapPCL)
        {
            if (_sets[selection].Group == eSymSetGroup.UserSet)
                return _sets[selection].MapArrayUserSet;
            else if (flagMapPCL)
                return _sets[selection].MapArrayPCL;
            else
                return _sets[selection].MapArrayStd;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t M a p A r r a y S y m b o l                                  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return array defining mapStd of the TTF 'Symbol' encoding set to  //
        // Unicode.                                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt16 [] getMapArraySymbol()
        {
            return _sets[_indxSymbol].MapArrayStd;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t M a p A r r a y U s e r S e t                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return array defining mapStd of the User-defined symbol set.      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt16 [] getMapArrayUserSet ()
        {
            return _sets [_indxUserSet].MapArrayUserSet;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t N a m e                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return name associated with specified Symbol Set index.            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getName(Int32 selection)
        {
            return _sets[selection].Name;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t P a r s e M e t h o d                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return default text parsing method index associated with specified //
        // Symbol Set index.                                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static PCLTextParsingMethods.eIndex getParsingMethod(
            Int32 selection)
        {
            return _sets[selection].ParsingMethod;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t S y m s e t D a t a                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return identifying (reference) name and Kind1 value of the         //
        // selected symbol set value.                                         //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean getSymsetData(Int32 selection,
                                            ref UInt16 kind1,
                                            ref UInt16 idNum,
                                            ref String name)
        {
            Boolean symsetPresent;

            symsetPresent = _sets[selection].getSymsetData (ref kind1,
                                                            ref idNum,
                                                            ref name);

            return symsetPresent;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t S y m s e t D a t a F o r I d A l p h a                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return identifying (reference) name and Kind1 value of the         //
        // selected symbol set if the 'Id Alpha' value matches the specified  //
        // value.                                                             //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean getSymsetDataForIdAlpha(Int32 selection,
                                                      Byte idAlpha,
                                                      ref UInt16 kind1,
                                                      ref UInt16 idNum,  
                                                      ref String name)
        {
            Boolean symsetPresent;

            symsetPresent = _sets[selection].getSymsetDataForIdAlpha (idAlpha,
                                                                      ref kind1,
                                                                      ref idNum,
                                                                      ref name);

            return symsetPresent;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t T y p e                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return symbol set type.                                            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static PCLSymSetTypes.eIndex getType (Int32 selection)
        {
            return _sets [selection].Type;
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // I n d e x S y m b o l                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set or return index of the 'symbol' Symbol Set.                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 IndexSymbol
        {
            get { return _indxSymbol; }
        //    set { _indxSymbol = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // I n d e x U n i c o d e                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set or return index of the 'Unicode' Symbol Set.                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 IndexUnicode
        {
            get { return _indxUnicode; }
        //    set { _indxUnicode = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // I n d e x U s e r S e t                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set or return index of the 'user-defined' Symbol Set.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 IndexUserSet
        {
            get { return _indxUserSet; }
        //    set { _indxUserSet = value; }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // I n d e x U n b o u n d                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return index of the 'unbound' Symbol Set.                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 IndexUnbound
        {
            get { return _indxUnbound; }
        //    set { _indxUnbound = value; }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // n u l l M a p P C L                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return boolean value indicating whether or not the LaserJet (PCL)  //
        // mapping associated with the specified Symbol Set index is null.    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean nullMapPCL (Int32 selection)
        {
            return _sets[selection].NullMapPCL;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // n u l l M a p S t d                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return boolean value indicating whether or not the Standard        //
        // (Strict) mapping associated with the specified Symbol Set index is //
        // null.                                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean nullMapStd (Int32 selection)
        {
            return _sets[selection].NullMapStd;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p o p u l a t e S y m b o l S e t T a b l e                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Populate the table of Symbol Sets.                                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void populateSymbolSetTable()
        {
            eSymSetGroup group;

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Unbound,
                                         PCLSymSetTypes.eIndex.Unbound_Unicode,
                                         PCLTextParsingMethods.eIndex.m83_UTF8,
                                         56,                         //    1X //
                                         "",  
                                         "<unbound>",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _indxUnbound = _sets.Count - 1;

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Symbol,
                                         PCLSymSetTypes.eIndex.Bound_PC8,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         0,                          //    ?? // 
                                         "",
                                         "<symbol>",
                                         false,                // special map //
                                         PCLSymSetMaps.eSymSetMapId.mapSymbol));

            _indxSymbol = _sets.Count - 1;

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Custom,
                                         PCLSymSetTypes.eIndex.Unknown,
                                         PCLTextParsingMethods.eIndex.not_specified,
                                         0,                          //    ?? //
                                         "",
                                         "<specify identifier>",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_PC8,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         278,                        //    8V //
                                         "ARABIC8",
                                         "Arabic-8",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_8V));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_7bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         57,                         //    1Y //
                                         "",
                                         "Barcode: 2 of 5 Industrial",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_7bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         153,                        //    4Y //
                                         "",
                                         "Barcode: 2 of 5 Interleaved",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_7bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         89,                         //    2Y //
                                         "",
                                         "Barcode: 2 of 5 Matrix",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_7bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         25,                         //    0Y //
                                         "",
                                         "Barcode: 3 of 9",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_7bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         185,                        //    5Y //
                                         "",
                                         "Barcode: CODABAR",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_7bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         249,                        //    7Y //
                                         "",
                                         "Barcode: Code 11",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_7bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         217,                        //    6Y //
                                         "",
                                         "Barcode: MSI/Plessey",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_7bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         281,                        //    8Y //
                                         "",
                                         "Barcode: UPC/EAN",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_7bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         505,                        //   15Y //
                                         "",
                                         "Barcode: USPS ZIP",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_16bit,    // 1 or 2-byte //
                                         PCLTextParsingMethods.eIndex.m38_1_or_2_byte_Asian8bit,
                                         596,                        //   18T //
                                         "",
                                         "Big5 Traditional Chinese",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.NonStd,
                                         PCLSymSetTypes.eIndex.Bound_16bit,    // 1 or 2-byte //
                                         PCLTextParsingMethods.eIndex.m38_1_or_2_byte_Asian8bit,
                                         32596,                      // 1018T //
                                         "",
                                         "Big5 Traditional Chinese",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_x1018T));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_16bit,    // 1 or 2-byte //
                                         PCLTextParsingMethods.eIndex.m38_1_or_2_byte_Asian8bit,
                                         564,                        //   17T //
                                         "",
                                         "Chinese CNS 11643-86",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_16bit,    // 1 or 2-byte //
                                         PCLTextParsingMethods.eIndex.m38_1_or_2_byte_Asian8bit,
                                         660,                        //   20T //
                                         "",
                                         "Chinese TCA encoding",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_8bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         50,                         //    1R //
                                         "",
                                         "Cyrillic",
                                         false,
                                        PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_8bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         234,                        //    7J //
                                         "DESKTOP",
                                         "DeskTop",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_7J));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_8bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         263,                        //    8G //
                                         "GREEK8",
                                         "Greek-8",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_8G));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_7bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         8,                          //    0H //
                                         "HEBREW7",
                                         "Hebrew-7",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_0H));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_8bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         264,                        //    8H //
                                         "HEBREW8",
                                         "Hebrew-8",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_8H));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_7bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         37,                         //    1E //
                                         "ISO4",
                                         "ISO 4: United Kingdom",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_1E));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_7bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         21,                         //    0U //
                                         "ISO6",
                                         "ISO 6: ASCII",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_0U));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_7bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         19,                         //    0S //
                                         "ISO11",
                                         "ISO 11: Swedish",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_0S));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_7bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         9,                          //    0I //
                                         "ISO15",
                                         "ISO 15: Italian",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_0I));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_7bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         83,                         //    2S //
                                         "ISO17",
                                         "ISO 17: Spanish",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_2S));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_7bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         39,                         //    1G //
                                         "ISO21",
                                         "ISO 21: German",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_1G));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_7bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         4,                          //    0D //
                                         "ISO60",
                                         "ISO 60: Danish/Norwegian",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_0D));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_7bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         38,                         //    1F //
                                         "ISO69",
                                         "ISO 69: French",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_1F));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_8bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         14,                         //    0N //
                                         "ISOL1",
                                         "ISO 8859-1 Latin 1",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_0N));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_8bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         78,                         //    2N //
                                         "ISOL2",
                                         "ISO 8859-2 Latin 2",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_2N));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_8bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         110,                        //    3N //
                                         "",
                                         "ISO 8859-3 Latin 3",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_3N));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_8bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         142,                        //    4N //
                                         "",
                                         "ISO 8859-4 Latin 4",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_4N));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_8bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         334,                        //   10N //
                                         "ISOCYR",
                                         "ISO 8859-5 Latin/Cyrillic",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_10N));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_8bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         366,                        //   11N //
                                         "",
                                         "ISO 8859-6 Latin/Arabic",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_11N));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_8bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         398,                        //   12N //
                                         "ISOGRK",
                                         "ISO 8859-7 Latin/Greek",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_12N));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_8bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         232,                        //    7H //
                                         "ISOHEB",
                                         "ISO 8859-8 Latin/Hebrew",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_7H));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_8bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         174,                        //    5N //
                                         "ISOL5",
                                         "ISO 8859-9 Latin 5",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_5N));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_8bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         206,                        //    6N //
                                         "ISOL6",
                                         "ISO 8859-10 Latin 6",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_6N));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.NonStd,
                                         PCLSymSetTypes.eIndex.Bound_8bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         32052,                      // 1001T //
                                         "",
                                         "ISO 8859-11 Latin/Thai",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_x1001T));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_8bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         302,                        //    9N //
                                         "ISOL9",
                                         "ISO 8859-15 Latin 9",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_9N));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_16bit,    // 1 or 2-byte //
                                         PCLTextParsingMethods.eIndex.m21_1_or_2_byte_Asian7bit,
                                         555,                        //   17K //
                                         "",
                                         "Japanese JIS",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_16bit,    // 1 or 2-byte //
                                         PCLTextParsingMethods.eIndex.m31_1_or_2_byte_ShiftJIS,
                                         619,                        //   19K //
                                         "WIN31J",
                                         "Japanese Shift-JIS (CP 932)",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.NonStd,
                                         PCLSymSetTypes.eIndex.Bound_16bit,    // 1 or 2-byte //
                                         PCLTextParsingMethods.eIndex.m31_1_or_2_byte_ShiftJIS,
                                         32619,                      // 1019K //
                                         "",
                                         "Japanese Shift-JIS",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_x1019K));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_16bit,    // 1 or 2-byte //
                                         PCLTextParsingMethods.eIndex.m38_1_or_2_byte_Asian8bit,
                                         616,                        //   19H //
                                         "",
                                         "Korean KSC5601-87",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_16bit,    // 1 or 2-byte //
                                         PCLTextParsingMethods.eIndex.m38_1_or_2_byte_Asian8bit,
                                         584,                        //   18H //
                                         "",
                                         "Korean KSC5601-93",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_16bit,    // 1 or 2-byte //
                                         PCLTextParsingMethods.eIndex.m38_1_or_2_byte_Asian8bit,
                                         712,                        //   22H //
                                         "",
                                         "Korean KSX1001",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_16bit,    // 1 or 2-byte //
                                         PCLTextParsingMethods.eIndex.m38_1_or_2_byte_Asian8bit,
                                         680,                        //   21H //
                                         "",
                                         "Korean Unified Hangeul",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_7bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         53,                         //    1U //
                                         "LEGAL",
                                         "Legal",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_1U));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_8bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         269,                        //    8M //
                                         "",
                                         "Math-8",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_8M));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_PC8,        // no C0 //
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         394,                        //   12J //
                                         "MCTEXT",
                                         "MC Text",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_12J));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_7bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         473,                        //   14Y //
                                         "",
                                         "MICR (CMC-7)",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_7bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         335,                        //   10O //
                                         "",
                                         "MICR (E13B)",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_PC8,        // no C0 //
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         202,                        //    6J //
                                         "",
                                         "Microsoft Publishing",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_6J));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_7bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         15,                         //    0O //
                                         "",
                                         "OCR-A",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_7bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         47,                         //    1O //
                                         "",
                                         "OCR-B",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_PC8,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         341,                        //   10U //
                                         "PC8",
                                         "PC-8 CP 437",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_10U));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_PC8,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         373,                        //   11U //
                                         "PC8DN",
                                         "PC-8 Danish/Norwegian",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_11U));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_PC8,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         391,                        //   12G //
                                         "PC8GRK",
                                         "PC-8 Latin/Greek",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_12G));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_PC8,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         308,                        //    9T //
                                         "PC8TK",
                                         "PC-8 Turkish",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_9T));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_PC8,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         853,                        //   26U //
                                         "PC775",
                                         "PC-775 Baltic",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_26U));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_PC8,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         405,                        //   12U //
                                         "PC850",
                                         "PC-850 Multilingual",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_12U));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_PC8,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         327,                        //   10G //
                                         "PC851GRK",
                                         "PC-851 Latin/Greek",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_10G));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_PC8,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         565,                        //   17U //
                                         "PC852",
                                         "PC-852 Latin 2",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_17U));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_PC8,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         437,                        //   13U //
                                         "PC858",
                                         "PC-858 Multilingual + Euro",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_13U));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_PC8,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         488,                        //   15H //
                                         "PC862HEB",
                                         "PC-862 Latin/Hebrew",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_15H));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_PC8,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         342,                        //   10V //
                                         "PC864ARA",
                                         "PC-864 Latin/Arabic",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_10V));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_PC8,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         114,                        //    3R //
                                         "PC866CYR",
                                         "PC-866 Cyrillic",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_3R));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_PC8,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         466,                        //   14R //
                                         "PC866UKR",
                                         "PC-866 Ukrainian",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_14R));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_PC8,        // no C0 //
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         298,                        //    9J //
                                         "PC1004",
                                         "PC-1004 Windows Extended",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_9J));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_7bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         501,                        //   15U //
                                         "",
                                         "Pi Font",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_15U));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_8bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         173,                        //    5M //
                                         "",
                                         "PS Math",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_5M));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_8bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         330,                        //   10J //
                                         "PSTEXT",
                                         "PS Text",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_10J));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_8bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                          277,                        //    8U //
                                         "ROMAN8",
                                         "Roman-8",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_8U));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_8bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         149,                        //    4U //
                                         "ROMAN9",
                                         "Roman-9",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_4U));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_16bit,    // 1 or 2-byte //
                                         PCLTextParsingMethods.eIndex.m38_1_or_2_byte_Asian8bit,
                                         579,                        //   18C //
                                         "",
                                         "Simplified Chinese GB2312-80",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.NonStd,
                                         PCLSymSetTypes.eIndex.Bound_16bit,    // 1 or 2-byte //
                                         PCLTextParsingMethods.eIndex.m38_1_or_2_byte_Asian8bit,
                                         32579,                      // 1018C //
                                         "",
                                         "Simplified Chinese GB2312-80",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_x1018C));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_16bit,    // 1 or 2-byte //
                                         PCLTextParsingMethods.eIndex.m38_1_or_2_byte_Asian8bit,
                                         643,                        //   20C //
                                         "",
                                         "Simplified Chinese GBK",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.NonStd,
                                         PCLSymSetTypes.eIndex.Bound_16bit,    // 1 or 2-byte //
                                         PCLTextParsingMethods.eIndex.m38_1_or_2_byte_Asian8bit,
                                         32643,                      // 1020C //
                                         "",
                                         "Simplified Chinese GBK (CP 936)",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_x1020C));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_8bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         621,                        //   19M //
                                         "",
                                         "Symbol",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                          PCLSymSetTypes.eIndex.Bound_8bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         52,                         //    1T //
                                         "",
                                         "TIS 620-2533 (Thai)",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_1T));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_8bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         276,                        //    8T //
                                         "",
                                         "Turkish-8",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Unicode,
                                         PCLSymSetTypes.eIndex.Bound_16bit,
                                         PCLTextParsingMethods.eIndex.m83_UTF8,
                                         590,                        //   18N //
                                         "",
                                         "Unicode",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _indxUnicode = _sets.Count - 1;

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_8bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         426,                        //   13J //
                                         "",
                                         "Ventura International",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_8bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         205,                        //    6M //
                                         "",
                                         "Ventura Math",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_8bit,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         458,                        //   14J //
                                         "",
                                         "Ventura US",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_PC8,        // no C0 //
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         309,                        //    9U //
                                         "WIN30",
                                         "Windows 3.0 Latin 1 (obsolete)",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_9U));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_PC8,        // no C0 //
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         620,                        //   19L //
                                         "WINBALT",
                                         "Windows Baltic (CP 1257)",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_19L));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_PC8,        // no C0 //
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         629,                        //   19U //
                                         "WINL1",
                                         "Windows Latin 1 (CP 1252)",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_19U));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_PC8,        // no C0 //
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         293,                        //    9E //
                                         "WINL2",
                                         "Windows Latin 2 (CP 1250)",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_9E));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_PC8,        // no C0 //
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         180,                        //    5T //
                                         "WINL5",
                                         "Windows Latin 5 (CP 1254)",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_5T));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_PC8,        // no C0 //
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         310,                        //    9V //
                                         "HPWARA",
                                         "Windows Latin/Arabic",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_9V));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_PC8,        // no C0 //
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         306,                        //    9R //
                                         "WINCYR",
                                         "Windows Latin/Cyrillic (CP 1251)",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_9R));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_PC8,        // no C0 //
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         295,                        //    9G //
                                         "WINGRK",
                                         "Windows Latin/Greek (CP 1253)",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_9G));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.NonStd,
                                         PCLSymSetTypes.eIndex.Bound_PC8,        // no C0 //
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         28852,                      //  901T //
                                         "",
                                         "Windows Latin/Thai (CP 874)",
                                         true,
                                         PCLSymSetMaps.eSymSetMapId.map_x901T));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_PC8,        // no C0 //
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         18540,                      //  579L //
                                         "",
                                         "Wingdings",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.Preset,
                                         PCLSymSetTypes.eIndex.Bound_PC8,        // no C0 //
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         460,                        //   14L //
                                         "",
                                         "ZapfDingBats",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapNull));

            _sets.Add (new PCLSymbolSet (eSymSetGroup.UserSet,
                                         PCLSymSetTypes.eIndex.Bound_PC8,
                                         PCLTextParsingMethods.eIndex.m0_1_byte_default,
                                         65530,                      //    ?? //
                                         "",
                                         "<user-defined via file>",
                                         false,
                                         PCLSymSetMaps.eSymSetMapId.mapUserSet));

            _indxUserSet = _sets.Count - 1;

            _setsCountTotal   = _sets.Count;

            _setsCountCustom  = 0;
            _setsCountPreset  = 0;
            _setsCountUnicode = 0;
            _setsCountUserSet = 0;
            _setsCountMapped  = 0;

            for (Int32 i = 0; i < _setsCountTotal; i++)
            {
                group = _sets [i].Group;

                if (group == eSymSetGroup.Custom)
                    _setsCountCustom++;
                else if (group == eSymSetGroup.Preset)
                    _setsCountPreset++;
                else if (group == eSymSetGroup.Unicode)
                    _setsCountUnicode++;
                else if (group == eSymSetGroup.UserSet)
                    _setsCountUserSet++;

                if (_sets [i].FlagMapped)
                    _setsCountMapped++;
            }

            _setsCountStd = _setsCountCustom +
                            _setsCountPreset +
                            _setsCountUnicode;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i s M a p p e d                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return indication of whether or not a mapStd is defined for the   //
        // symbol set.                                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean isMapped(Int32 selection)
        {
            if (_sets[selection].FlagMapped)
                return true;
            else
                return false;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t D a t a U s e r S e t                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set data (set number and array) defining mapping of the            //
        // User-defined symbol set.                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void setDataUserSet (UInt16      symSetNo,
                                           PCLSymSetTypes.eIndex symSetType,
                                           UInt16      [] mapArray)
        {
            _sets [_indxUserSet].Kind1           = symSetNo;
            _sets [_indxUserSet].Id              = translateKind1ToId (symSetNo);
            _sets [_indxUserSet].Type            = symSetType;
            _sets [_indxUserSet].MapArrayUserSet = mapArray;

            if (symSetType == PCLSymSetTypes.eIndex.Bound_16bit)
                _sets[_indxUserSet].ParsingMethod =
                    PCLTextParsingMethods.eIndex.m83_UTF8;
            else
                _sets[_indxUserSet].ParsingMethod =
                    PCLTextParsingMethods.eIndex.not_specified;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t D a t a U s e r S e t D e f a u l t                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set default data (set number and array) defining mapping of the    //
        // User-defined symbol set.                                           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void setDataUserSetDefault (UInt16 symSetNo)
        {
            Int32 index;

            UInt16 [] mapArray;
        
            index = getIndexForId(symSetNo);

            if (index == -1)
                mapArray = PCLSymSetMaps.getMapArrayUserSet ();
            else
                mapArray = getMapArray (index, false);

            _sets [_indxUserSet].Kind1           = symSetNo;
            _sets [_indxUserSet].Id              = translateKind1ToId (symSetNo);
            _sets [_indxUserSet].Type = PCLSymSetTypes.eIndex.Bound_PC8;
            _sets [_indxUserSet].MapArrayUserSet = mapArray;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t r a n s l a t e I d T o K i n d 1                        ( I )   //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the 'Kind1' integer equivalent of the identifier value; if  //
        // the identifier is invalid, return a value of 0.                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt16 translateIdToKind1(String symbolSetId)
        {
            bool dataValid = true;

            UInt16 kind1Num = 0,
                   kind1Alpha = 0;

            Int32 len;

            //----------------------------------------------------------------//
            //                                                                //
            // Check that field length is within limits.                      //
            //                                                                //
            //----------------------------------------------------------------//

            len = symbolSetId.Length;

            if ((len < 2) || (len > 5))
                dataValid = false;

            //----------------------------------------------------------------//
            //                                                                //
            // Check that all characters, except for the last, are digits,    //
            // and that the value is within the prescribed limits.            //
            //                                                                //
            //----------------------------------------------------------------//

            if (dataValid)
            {
                kind1Num = Convert.ToUInt16(symbolSetId.Substring(0, len - 1));

                if ((kind1Num < 0) || (kind1Num > 2047))
                    dataValid = false;
            }

            //----------------------------------------------------------------//
            //                                                                //
            // Check that last character is upper-case alphabetic, and if so, //
            // convert to the ordinal (decimal) equivalent of the ANSI        //
            // character code.                                                //
            //                                                                //
            //----------------------------------------------------------------//

            if (dataValid)
            {
                Char termChar = symbolSetId[len - 1];

                if ((termChar >= 0x41) && (termChar <= 0x5a))
                    kind1Alpha = termChar;
                else
                    dataValid = false;
            }

            //----------------------------------------------------------------//
            //                                                                //
            // If valid, calculate and return 'Kind1' value; otherwise return //
            // a zero value.                                                  //
            //                                                                //
            //----------------------------------------------------------------//

            if (dataValid)
                return (UInt16)((kind1Num * 32) + (kind1Alpha - 64));
            else
                return 65535;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t r a n s l a t e I d T o K i n d 1                       ( II )   //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the 'Kind1' integer equivalent of the identifier value; if  //
        // the identifier is invalid, return a value of 0.                    //
        //                                                                    //
        // The format of the ID value is:                                     //
        //        nnnnA                                                       //
        // where  nnnn  is a decimal value (with 0 <= nnnn <= 2047)           //
        //              values    0 - 1023 are defined by HP                  //
        //                     1024 - 2047 are available for independant font //
        //                                 vendors                            //
        //        A     is an uppercase alphabetic character.                 //
        //                                                                    //
        // The Kind1 value is calculated as:                                  //
        //    Kind1 = (ID_numeric_part * 32) + (ID_alpha_ordinal - 64)        //
        //                                                                    //
        // The Kind1 value must be in the range 0 - 65535, although, given    //
        // the above restraints:                                              //
        //                                                                    //
        // - the minimum value is 1     (equivalent to ID = 0A)               // 
        // - the maximum value is 65530 (equivalent to ID = 2047Z)            //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt16 translateIdToKind1(UInt16 idNum,
                                                Byte   idAlpha)
        {
            UInt16 badValue = 0;

            if ((idNum < 1) || (idNum > 2047))
                return badValue;
            else if ((idAlpha < 0x40) || (idAlpha > 0x5a))
                return badValue;
            else
                return (UInt16)((idNum * 32) + (idAlpha - 64));
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t r a n s l a t e K i n d 1 T o I d                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the HP string identifier equivalent of the supplied 'Kind1' //
        // numeric identifier.                                                //
        //                                                                    //
        // The format of the ID value is:                                     //
        //        nnnnA                                                       //
        // where  nnnn  is a decimal value (with 0 <= nnnn <= 2047)           //
        //              values    0 - 1023 are defined by HP                  //
        //                     1024 - 2047 are available for independant font //
        //                                 vendors                            //
        //        A     is an uppercase alphabetic character.                 //
        //                                                                    //
        // The Kind1 value is calculated as:                                  //
        //    Kind1 = (ID_numeric_part * 32) + (ID_alpha_ordinal - 64)        //
        //                                                                    //
        // The Kind1 value must be in the range 0 - 65535, although the       //
        // maximum value obtainable, with the above constraints on the ID, is //
        // 65530 (equivalent to ID = 2047Z).                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String translateKind1ToId(UInt16 kind1)
        {
            UInt16 kind1Num = 0,
                   kind1Alpha = 0;

            if ((kind1 < 1) || (kind1 > 65530))
            {
                return "";
            }
            else
            {
                kind1Num = (UInt16)(kind1 / 32);

                kind1Alpha = (UInt16)((kind1 - (kind1Num * 32)) + 64);

                return kind1Num.ToString() + Convert.ToChar(kind1Alpha);
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // t r a n s l a t e K i n d 1 T o I d                       ( I I )  //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the HP string identifier equivalent of the supplied 'Kind1' //
        // numeric identifier.                                                //
        //                                                                    //
        // The format of the ID value is:                                     //
        //        nnnnA                                                       //
        // where  nnnn  is a decimal value (with 0 <= nnnn <= 2047)           //
        //              values    0 - 1023 are defined by HP                  //
        //                     1024 - 2047 are available for independant font //
        //                                 vendors                            //
        //        A     is an uppercase alphabetic character.                 //
        //                                                                    //
        // The Kind1 value is calculated as:                                  //
        //    Kind1 = (ID_numeric_part * 32) + (ID_alpha_ordinal - 64)        //
        //                                                                    //
        // The Kind1 value must be in the range 0 - 65535, although the       //
        // maximum value obtainable, with the above constraints on the ID, is //
        // 65530 (equivalent to ID = 2047Z).                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void translateKind1ToId(UInt16     kind1,
                                              ref String idNum,
                                              ref String idAlpha)
        {
            UInt16 kind1Num = 0,
                   kind1Alpha = 0;

            if ((kind1 < 1) || (kind1 > 65530))
            {
                idNum   = "";
                idAlpha = "";

                return;
            }
            else
            {
                kind1Num   = (UInt16) (kind1 / 32);
                kind1Alpha = (UInt16) ((kind1 - (kind1Num * 32)) + 64);

                idNum   = kind1Num.ToString ();
                idAlpha = Convert.ToChar (kind1Alpha).ToString();

                return;
            }
        }
    }
}