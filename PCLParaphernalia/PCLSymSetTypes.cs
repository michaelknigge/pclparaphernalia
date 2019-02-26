using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines a set of PCL Symbol Set Type objects.
    /// 
    /// © Chris Hutchinson 2015
    /// 
    /// </summary>

    static class PCLSymSetTypes
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        // Note that the length of the index array must be the same as that   //
        // of the definition array; the entries must be in the same order.    //
        //                                                                    //
        //--------------------------------------------------------------------//

		private static Boolean _flagBound   = true;
		private static Boolean _flagUnbound = false;
		
		public enum eIndex
        {
            Unknown            = 0,
            Bound_7bit         = 1,
            Bound_8bit         = 2,
            Bound_PC8          = 3,
            Bound_16bit        = 4,
            Unbound_MSL        = 5,
            Unbound_Unicode    = 6
        };
		
		private enum eIdPCL: byte
        {
            Unknown            = 0,
            Bound_7bit         = 0,
            Bound_8bit         = 1,
            Bound_PC8          = 2,
            Bound_16bit        = 3,
            Unbound_MSL        = 10,
            Unbound_Unicode    = 11
        };

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static List<PCLSymSetType> _sets =
            new List<PCLSymSetType> ();

        private static Int32 _setsCountBound;
        private static Int32 _setsCountTotal;

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // P C L S y m b o l S e t T y p e s                                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        static PCLSymSetTypes()
        {
            populateSymbolSetTypeTable();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C o u n t                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return count of Symbol Set Type definitions.                       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 getCount()
        {
            return _setsCountTotal;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t C o u n t B o u n d                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return count of Symbol Set Types which are 'bound'.                //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 getCountBound ()
        {
            return _setsCountBound;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t D e s c S h o r t                                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return short description associated with the specified Symbol Set  //
        // Type index.                                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getDescShort (Int32 selection)
        {
            return _sets[selection].DescShort;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t D e s c S t d                                                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return standard description associated with the specified Symbol   //
        // Set Type index.                                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getDescStd (Int32 selection)
        {
            return _sets[selection].DescStd;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I d P C L                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the PCL identifier associated with the specified Symbol Set //
		// Type index.                                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Byte getIdPCL (Int32 selection)
        {
            return _sets[selection].IdPCL;
        }


        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I n d e x F o r I d P C L                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the index of the Symbol Set Type associated with the        //
        // specified PCL identifier.                                          //
        //                                                                    //
        // Note that the length of the index array must be the same as that   //
        // of the definition array; the entries must be in the same order,    //
        // and the count of entries in the set must be the same as the count  //
        // of entries in these two arrays.                                    //
        //                                                                    //
        // If no match is found, a default index is returned.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static eIndex getIndexForIdPCL (Byte idPCL)
        {
            String entry;

            entry = Enum.GetName(typeof(eIdPCL), idPCL);

            if (entry == null)
                return eIndex.Bound_PC8;
            else
                return (eIndex) Enum.Parse(typeof(eIndex), entry);
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // i s B o u n d                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return indication of whether or not the symbol set type associated //
		// with the specified index is bound or unbound.                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean isBound (Int32 selection)
        {
            if (_sets[selection].IsBound)
                return true;
            else
                return false;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p o p u l a t e S y m b o l S e t T y p e T a b l e                //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Populate the table of Symbol Set Types.                            //
        //                                                                    //
        // Note that the length of the definition array must be the same as   //
        // that of the index array; the entries must be in the same order.    //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void populateSymbolSetTypeTable()
        {
            _sets.Add (new PCLSymSetType (
							(Byte) eIdPCL.Unknown,
							_flagBound,
							"<unknown>",
                            "<unknown>"));

            _sets.Add (new PCLSymSetType (
							(Byte) eIdPCL.Bound_7bit,
							_flagBound,
							"0: Bound; 7-bit (96 charset: " +
                                "0x20-7f printable)",
                            "0: 7-bit"));

            _sets.Add (new PCLSymSetType (
                            (Byte) eIdPCL.Bound_8bit,
                            _flagBound,
							"1: Bound; 8-bit (192 charset: " + 
                                "0x20-7f, 0xa0-ff printable)",
                            "1: 8 bit"));

            _sets.Add (new PCLSymSetType (
                            (Byte) eIdPCL.Bound_PC8,
                            _flagBound,
							"2: Bound; PC-8 (256 charset: " +
                                "0x01-06, 0x10-1a, 0x1c-ff printable)",
                            "2: 8-bit PC8"));

            _sets.Add (new PCLSymSetType (
                            (Byte) eIdPCL.Bound_16bit,
                            _flagBound,
							"3: Bound; 16-bit (65535 charset: " +
                                "(0x01-06, 0x10-1a, 0x1c-fffd printable)",
                            "3: 16-bit"));

            _sets.Add (new PCLSymSetType (
                            (Byte) eIdPCL.Unbound_MSL,
                            _flagUnbound,
                            "10: Unbound; uses HP MSL numbers",
                            "10: index MSL"));

            _sets.Add (new PCLSymSetType (
                            (Byte) eIdPCL.Unbound_Unicode,
                            _flagUnbound,
                            "11: Unbound; uses Unicode code points",
                             "11: index U+"));

            _setsCountTotal = _sets.Count;
            _setsCountBound = 0;

            for (Int32 i = 0; i < _setsCountTotal; i++)
            {
                if (_sets [i].IsBound == _flagBound)
                    _setsCountBound++;
            }
        }
    }
}