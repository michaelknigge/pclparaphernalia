using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class lists the defined Unicode blocks.
	/// Correct as at Unicode.org "Blocks-10.0.0.txt" dated 2017-04-12  
    /// 
    /// © Chris Hutchinson 2017
    /// 
    /// </summary>

    static class UnicodeBlocks
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Constants and enumerations.                                        //
        //                                                                    //
        //--------------------------------------------------------------------//

        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//
        
        private static Int32 _blocksCount;

        private static SortedList<UInt32, UnicodeBlock> _blocksList =
              new SortedList<UInt32, UnicodeBlock>();

        //--------------------------------------------------------------------//
        //                                              C o n s t r u c t o r //
        // U n i c o d e B l o c k s                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        static UnicodeBlocks ()
        {
            populateTable();
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t B l o c k s C o u n t                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return count of defined blocks.                                    //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Int32 getBlocksCount()
        {
            return _blocksCount;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t R a n g e E n d                                              //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return range end value associated with specified block item.       //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt32 getRangeEnd (UInt32 index)
        {
            return _blocksList [index].RangeEnd;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t R a n g e S t a r t                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return range start value associated with specified block item.     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt32 getRangeStart (UInt32 index)
        {
            return _blocksList [index].RangeStart;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t N a m e                                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the name associated with specified block item.              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getName (UInt32 index)
        {
            return _blocksList [index].Name;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t I n d e x F o r C o d e p o i n t                            //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the list index for the specified codepoint.                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static UInt32 getIndexForCodepoint (UInt32 codepoint)
        {
            Boolean found = false;

            UInt32 key = 0;

            foreach (KeyValuePair<UInt32, UnicodeBlock> kvp in _blocksList)
            {
                key = kvp.Key;

                if (codepoint >= key)
                {
                    if (codepoint <= _blocksList[key].RangeEnd)
                    {
                        found = true;
                        break;
                    }
                }
            }

            if (found)
                return key;
            else
                return 0xffffffff;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t B l o c k N a m e F o r C o d e p o i n t                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return the block name associated with the specified codepoint.     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static String getBlocknameForCodepoint (UInt32 codepoint)
        {
            Boolean found = false;

            UInt32 key = 0xffffffff;

            foreach (KeyValuePair<UInt32, UnicodeBlock> kvp in _blocksList)
            {
                key = kvp.Key;

                if (codepoint >= key)
                {
                    if (codepoint <= _blocksList[key].RangeEnd)
                    {
                        found = true;
                        break;
                    }
                }
            }

            if (found)
                return _blocksList[key].Name;
            else
                return "<not defined>";
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // p o p u l a t e T a b l e                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Populate the table of Unicode blocks.                              //
        // Indexed by range start value.                                      //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static void populateTable()
        {
            _blocksList.Add (                              //   0000     007F //
                0x0000,
                new UnicodeBlock (
                    0x0000, 0x007F,
                    "Basic Latin"));

            _blocksList.Add (                              //   0080     00FF //
                0x0080,
                new UnicodeBlock (
                    0x0080, 0x00FF,
                    "Latin - 1 Supplement"));

            _blocksList.Add (                              //   0100     017F //
                0x0100,
                new UnicodeBlock (
                    0x0100, 0x017F,
                    "Latin Extended - A"));

            _blocksList.Add (                              //   0180     024F //
                0x0180,
                new UnicodeBlock (
                    0x0180, 0x024F,
                    "Latin Extended - B"));

            _blocksList.Add (                              //   0250     02AF //
                0x0250,
                new UnicodeBlock (
                    0x0250, 0x02AF,
                    "IPA Extensions"));

            _blocksList.Add (                              //   02B0     02FF //
                0x02B0,
                new UnicodeBlock (
                    0x02B0, 0x02FF,
                    "Spacing Modifier Letters"));

            _blocksList.Add (                              //   0300     036F //
                0x0300,
                new UnicodeBlock (
                    0x0300, 0x036F,
                    "Combining Diacritical Marks"));

            _blocksList.Add (                              //   0370     03FF //
                0x0370,
                new UnicodeBlock (
                    0x0370, 0x03FF,
                    "Greek and Coptic"));

            _blocksList.Add (                              //   0400     04FF //
                0x0400,
                new UnicodeBlock (
                    0x0400, 0x04FF,
                    "Cyrillic"));

            _blocksList.Add (                              //   0500     052F //
                0x0500,
                new UnicodeBlock (
                    0x0500, 0x052F,
                    "Cyrillic Supplement"));

            _blocksList.Add (                              //   0530     058F //
                0x0530,
                new UnicodeBlock (
                    0x0530, 0x058F,
                    "Armenian"));

            _blocksList.Add (                              //   0590     05FF //
                0x0590,
                new UnicodeBlock (
                    0x0590, 0x05FF,
                    "Hebrew"));

            _blocksList.Add (                              //   0600     06FF //
                0x0600,
                new UnicodeBlock (
                    0x0600, 0x06FF,
                    "Arabic"));

            _blocksList.Add (                              //   0700     074F //
                0x0700,
                new UnicodeBlock (
                    0x0700, 0x074F,
                    "Syriac"));

            _blocksList.Add (                              //   0750     077F //
                0x0750,
                new UnicodeBlock (
                    0x0750, 0x077F,
                    "Arabic Supplement"));

            _blocksList.Add (                              //   0780     07BF //
                0x0780,
                new UnicodeBlock (
                    0x0780, 0x07BF,
                    "Thaana"));

            _blocksList.Add (                              //   07C0     07FF //
                0x07C0,
                new UnicodeBlock (
                    0x07C0, 0x07FF,
                    "NKo"));

            _blocksList.Add (                              //   0800     083F //
                0x0800,
                new UnicodeBlock (
                    0x0800, 0x083F,
                    "Samaritan"));

            _blocksList.Add (                              //   0840     085F //
                0x0840,
                new UnicodeBlock (
                    0x0840, 0x085F,
                    "Mandaic"));

            _blocksList.Add (                              //   0860     086F //
                0x0860,
                new UnicodeBlock (
                    0x0860, 0x086F,
                    "Syriac Supplement"));

            _blocksList.Add (                              //   08A0     08FF //
                0x08A0,
                new UnicodeBlock (
                    0x08A0, 0x08FF,
                    "Arabic Extended - A"));

            _blocksList.Add (                              //   0900     097F //
                0x0900,
                new UnicodeBlock (
                    0x0900, 0x097F,
                    "Devanagari"));

            _blocksList.Add (                              //   0980     09FF //
                0x0980,
                new UnicodeBlock (
                    0x0980, 0x09FF,
                    "Bengali"));

            _blocksList.Add (                              //   0A00     0A7F //
                0x0A00,
                new UnicodeBlock (
                    0x0A00, 0x0A7F,
                    "Gurmukhi"));

            _blocksList.Add (                              //   0A80     0AFF //
                0x0A80,
                new UnicodeBlock (
                    0x0A80, 0x0AFF,
                    "Gujarati"));

            _blocksList.Add (                              //   0B00     0B7F //
                0x0B00,
                new UnicodeBlock (
                    0x0B00, 0x0B7F,
                    "Oriya"));

            _blocksList.Add (                              //   0B80     0BFF //
                0x0B80,
                new UnicodeBlock (
                    0x0B80, 0x0BFF,
                    "Tamil"));

            _blocksList.Add (                              //   0C00     0C7F //
                0x0C00,
                new UnicodeBlock (
                    0x0C00, 0x0C7F,
                    "Telugu"));

            _blocksList.Add (                              //   0C80     0CFF //
                0x0C80,
                new UnicodeBlock (
                    0x0C80, 0x0CFF,
                    "Kannada"));

            _blocksList.Add (                              //   0D00     0D7F //
                0x0D00,
                new UnicodeBlock (
                    0x0D00, 0x0D7F,
                    "Malayalam"));

            _blocksList.Add (                              //   0D80     0DFF //
                0x0D80,
                new UnicodeBlock (
                    0x0D80, 0x0DFF,
                    "Sinhala"));

            _blocksList.Add (                              //   0E00     0E7F //
                0x0E00,
                new UnicodeBlock (
                    0x0E00, 0x0E7F,
                    "Thai"));

            _blocksList.Add (                              //   0E80     0EFF //
                0x0E80,
                new UnicodeBlock (
                    0x0E80, 0x0EFF,
                    "Lao"));

            _blocksList.Add (                              //   0F00     0FFF //
                0x0F00,
                new UnicodeBlock (
                    0x0F00, 0x0FFF,
                    "Tibetan"));

            _blocksList.Add (                              //   1000     109F //
                0x1000,
                new UnicodeBlock (
                    0x1000, 0x109F,
                    "Myanmar"));

            _blocksList.Add (                              //   10A0     10FF //
                0x10A0,
                new UnicodeBlock (
                    0x10A0, 0x10FF,
                    "Georgian"));

            _blocksList.Add (                              //   1100     11FF //
                0x1100,
                new UnicodeBlock (
                    0x1100, 0x11FF,
                    "Hangul Jamo"));

            _blocksList.Add (                              //   1200     137F //
                0x1200,
                new UnicodeBlock (
                    0x1200, 0x137F,
                    "Ethiopic"));

            _blocksList.Add (                              //   1380     139F //
                0x1380,
                new UnicodeBlock (
                    0x1380, 0x139F,
                    "Ethiopic Supplement"));

            _blocksList.Add (                              //   13A0     13FF //
                0x13A0,
                new UnicodeBlock (
                    0x13A0, 0x13FF,
                    "Cherokee"));

            _blocksList.Add (                              //   1400     167F //
                0x1400,
                new UnicodeBlock (
                    0x1400, 0x167F,
                    "Unified Canadian Aboriginal Syllabics"));

            _blocksList.Add (                              //   1680     169F //
                0x1680,
                new UnicodeBlock (
                    0x1680, 0x169F,
                    "Ogham"));

            _blocksList.Add (                              //   16A0     16FF //
                0x16A0,
                new UnicodeBlock (
                    0x16A0, 0x16FF,
                    "Runic"));

            _blocksList.Add (                              //   1700     171F //
                0x1700,
                new UnicodeBlock (
                    0x1700, 0x171F,
                    "Tagalog"));

            _blocksList.Add (                              //   1720     173F //
                0x1720,
                new UnicodeBlock (
                    0x1720, 0x173F,
                    "Hanunoo"));

            _blocksList.Add (                              //   1740     175F //
                0x1740,
                new UnicodeBlock (
                    0x1740, 0x175F,
                    "Buhid"));

            _blocksList.Add (                              //   1760     177F //
                0x1760,
                new UnicodeBlock (
                    0x1760, 0x177F,
                    "Tagbanwa"));

            _blocksList.Add (                              //   1780     17FF //
                0x1780,
                new UnicodeBlock (
                    0x1780, 0x17FF,
                    "Khmer"));

            _blocksList.Add (                              //   1800     18AF //
                0x1800,
                new UnicodeBlock (
                    0x1800, 0x18AF,
                    "Mongolian"));

            _blocksList.Add (                              //   18B0     18FF //
                0x18B0,
                new UnicodeBlock (
                    0x18B0, 0x18FF,
                    "Unified Canadian Aboriginal Syllabics Extended"));

            _blocksList.Add (                              //   1900     194F //
                0x1900,
                new UnicodeBlock (
                    0x1900, 0x194F,
                    "Limbu"));

            _blocksList.Add (                              //   1950     197F //
                0x1950,
                new UnicodeBlock (
                    0x1950, 0x197F,
                    "Tai Le"));

            _blocksList.Add (                              //   1980     19DF //
                0x1980,
                new UnicodeBlock (
                    0x1980, 0x19DF,
                    "New Tai Lue"));

            _blocksList.Add (                              //   19E0     19FF //
                0x19E0,
                new UnicodeBlock (
                    0x19E0, 0x19FF,
                    "Khmer Symbols"));

            _blocksList.Add (                              //   1A00     1A1F //
                0x1A00,
                new UnicodeBlock (
                    0x1A00, 0x1A1F,
                    "Buginese"));

            _blocksList.Add (                              //   1A20     1AAF //
                0x1A20,
                new UnicodeBlock (
                    0x1A20, 0x1AAF,
                    "Tai Tham"));

            _blocksList.Add (                              //   1AB0     1AFF //
                0x1AB0,
                new UnicodeBlock (
                    0x1AB0, 0x1AFF,
                    "Combining Diacritical Marks Extended"));

            _blocksList.Add (                              //   1B00     1B7F //
                0x1B00,
                new UnicodeBlock (
                    0x1B00, 0x1B7F,
                    "Balinese"));

            _blocksList.Add (                              //   1B80     1BBF //
                0x1B80,
                new UnicodeBlock (
                    0x1B80, 0x1BBF,
                    "Sundanese"));

            _blocksList.Add (                              //   1BC0     1BFF //
                0x1BC0,
                new UnicodeBlock (
                    0x1BC0, 0x1BFF,
                    "Batak"));

            _blocksList.Add (                              //   1C00     1C4F //
                0x1C00,
                new UnicodeBlock (
                    0x1C00, 0x1C4F,
                    "Lepcha"));

            _blocksList.Add (                              //   1C50     1C7F //
                0x1C50,
                new UnicodeBlock (
                    0x1C50, 0x1C7F,
                    "Ol Chiki"));

            _blocksList.Add (                              //   1C80     1C8F //
                0x1C80,
                new UnicodeBlock (
                    0x1C80, 0x1C8F,
                    "Cyrillic Extended - C"));

            _blocksList.Add (                              //   1CC0     1CCF //
                0x1CC0,
                new UnicodeBlock (
                    0x1CC0, 0x1CCF,
                    "Sundanese Supplement"));

            _blocksList.Add (                              //   1CD0     1CFF //
                0x1CD0,
                new UnicodeBlock (
                    0x1CD0, 0x1CFF,
                    "Vedic Extensions"));

            _blocksList.Add (                              //   1D00     1D7F //
                0x1D00,
                new UnicodeBlock (
                    0x1D00, 0x1D7F,
                    "Phonetic Extensions"));

            _blocksList.Add (                              //   1D80     1DBF //
                0x1D80,
                new UnicodeBlock (
                    0x1D80, 0x1DBF,
                    "Phonetic Extensions Supplement"));

            _blocksList.Add (                              //   1DC0     1DFF //
                0x1DC0,
                new UnicodeBlock (
                    0x1DC0, 0x1DFF,
                    "Combining Diacritical Marks Supplement"));

            _blocksList.Add (                              //   1E00     1EFF //
                0x1E00,
                new UnicodeBlock (
                    0x1E00, 0x1EFF,
                    "Latin Extended Additional"));

            _blocksList.Add (                              //   1F00     1FFF //
                0x1F00,
                new UnicodeBlock (
                    0x1F00, 0x1FFF,
                    "Greek Extended"));

            _blocksList.Add (                              //   2000     206F //
                0x2000,
                new UnicodeBlock (
                    0x2000, 0x206F,
                    "General Punctuation"));

            _blocksList.Add (                              //   2070     209F //
                0x2070,
                new UnicodeBlock (
                    0x2070, 0x209F,
                    "Superscripts and Subscripts"));

            _blocksList.Add (                              //   20A0     20CF //
                0x20A0,
                new UnicodeBlock (
                    0x20A0, 0x20CF,
                    "Currency Symbols"));

            _blocksList.Add (                              //   20D0     20FF //
                0x20D0,
                new UnicodeBlock (
                    0x20D0, 0x20FF,
                    "Combining Diacritical Marks for Symbols"));

            _blocksList.Add (                              //   2100     214F //
                0x2100,
                new UnicodeBlock (
                    0x2100, 0x214F,
                    "Letterlike Symbols"));

            _blocksList.Add (                              //   2150     218F //
                0x2150,
                new UnicodeBlock (
                    0x2150, 0x218F,
                    "Number Forms"));

            _blocksList.Add (                              //   2190     21FF //
                0x2190,
                new UnicodeBlock (
                    0x2190, 0x21FF,
                    "Arrows"));

            _blocksList.Add (                              //   2200     22FF //
                0x2200,
                new UnicodeBlock (
                    0x2200, 0x22FF,
                    "Mathematical Operators"));

            _blocksList.Add (                              //   2300     23FF //
                0x2300,
                new UnicodeBlock (
                    0x2300, 0x23FF,
                    "Miscellaneous Technical"));

            _blocksList.Add (                              //   2400     243F //
                0x2400,
                new UnicodeBlock (
                    0x2400, 0x243F,
                    "Control Pictures"));

            _blocksList.Add (                              //   2440     245F //
                0x2440,
                new UnicodeBlock (
                    0x2440, 0x245F,
                    "Optical Character Recognition"));

            _blocksList.Add (                              //   2460     24FF //
                0x2460,
                new UnicodeBlock (
                    0x2460, 0x24FF,
                    "Enclosed Alphanumerics"));

            _blocksList.Add (                              //   2500     257F //
                0x2500,
                new UnicodeBlock (
                    0x2500, 0x257F,
                    "Box Drawing"));

            _blocksList.Add (                              //   2580     259F //
                0x2580,
                new UnicodeBlock (
                    0x2580, 0x259F,
                    "Block Elements"));

            _blocksList.Add (                              //   25A0     25FF //
                0x25A0,
                new UnicodeBlock (
                    0x25A0, 0x25FF,
                    "Geometric Shapes"));

            _blocksList.Add (                              //   2600     26FF //
                0x2600,
                new UnicodeBlock (
                    0x2600, 0x26FF,
                    "Miscellaneous Symbols"));

            _blocksList.Add (                              //   2700     27BF //
                0x2700,
                new UnicodeBlock (
                    0x2700, 0x27BF,
                    "Dingbats"));

            _blocksList.Add (                              //   27C0     27EF //
                0x27C0,
                new UnicodeBlock (
                    0x27C0, 0x27EF,
                    "Miscellaneous Mathematical Symbols - A"));

            _blocksList.Add (                              //   27F0     27FF //
                0x27F0,
                new UnicodeBlock (
                    0x27F0, 0x27FF,
                    "Supplemental Arrows - A"));

            _blocksList.Add (                              //   2800     28FF //
                0x2800,
                new UnicodeBlock (
                    0x2800, 0x28FF,
                    "Braille Patterns"));

            _blocksList.Add (                              //   2900     297F //
                0x2900,
                new UnicodeBlock (
                    0x2900, 0x297F,
                    "Supplemental Arrows - B"));

            _blocksList.Add (                              //   2980     29FF //
                0x2980,
                new UnicodeBlock (
                    0x2980, 0x29FF,
                    "Miscellaneous Mathematical Symbols - B"));

            _blocksList.Add (                              //   2A00     2AFF //
                0x2A00,
                new UnicodeBlock (
                    0x2A00, 0x2AFF,
                    "Supplemental Mathematical Operators"));

            _blocksList.Add (                              //   2B00     2BFF //
                0x2B00,
                new UnicodeBlock (
                    0x2B00, 0x2BFF,
                    "Miscellaneous Symbols and Arrows"));

            _blocksList.Add (                              //   2C00     2C5F //
                0x2C00,
                new UnicodeBlock (
                    0x2C00, 0x2C5F,
                    "Glagolitic"));

            _blocksList.Add (                              //   2C60     2C7F //
                0x2C60,
                new UnicodeBlock (
                    0x2C60, 0x2C7F,
                    "Latin Extended - C"));

            _blocksList.Add (                              //   2C80     2CFF //
                0x2C80,
                new UnicodeBlock (
                    0x2C80, 0x2CFF,
                    "Coptic"));

            _blocksList.Add (                              //   2D00     2D2F //
                0x2D00,
                new UnicodeBlock (
                    0x2D00, 0x2D2F,
                    "Georgian Supplement"));

            _blocksList.Add (                              //   2D30     2D7F //
                0x2D30,
                new UnicodeBlock (
                    0x2D30, 0x2D7F,
                    "Tifinagh"));

            _blocksList.Add (                              //   2D80     2DDF //
                0x2D80,
                new UnicodeBlock (
                    0x2D80, 0x2DDF,
                    "Ethiopic Extended"));

            _blocksList.Add (                              //   2DE0     2DFF //
                0x2DE0,
                new UnicodeBlock (
                    0x2DE0, 0x2DFF,
                    "Cyrillic Extended - A"));

            _blocksList.Add (                              //   2E00     2E7F //
                0x2E00,
                new UnicodeBlock (
                    0x2E00, 0x2E7F,
                    "Supplemental Punctuation"));

            _blocksList.Add (                              //   2E80     2EFF //
                0x2E80,
                new UnicodeBlock (
                    0x2E80, 0x2EFF,
                    "CJK Radicals Supplement"));

            _blocksList.Add (                              //   2F00     2FDF //
                0x2F00,
                new UnicodeBlock (
                    0x2F00, 0x2FDF,
                    "Kangxi Radicals"));

            _blocksList.Add (                              //   2FF0     2FFF //
                0x2FF0,
                new UnicodeBlock (
                    0x2FF0, 0x2FFF,
                    "Ideographic Description Characters"));

            _blocksList.Add (                              //   3000     303F //
                0x3000,
                new UnicodeBlock (
                    0x3000, 0x303F,
                    "CJK Symbols and Punctuation"));

            _blocksList.Add (                              //   3040     309F //
                0x3040,
                new UnicodeBlock (
                    0x3040, 0x309F,
                    "Hiragana"));

            _blocksList.Add (                              //   30A0     30FF //
                0x30A0,
                new UnicodeBlock (
                    0x30A0, 0x30FF,
                    "Katakana"));

            _blocksList.Add (                              //   3100     312F //
                0x3100,
                new UnicodeBlock (
                    0x3100, 0x312F,
                    "Bopomofo"));

            _blocksList.Add (                              //   3130     318F //
                0x3130,
                new UnicodeBlock (
                    0x3130, 0x318F,
                    "Hangul Compatibility Jamo"));

            _blocksList.Add (                              //   3190     319F //
                0x3190,
                new UnicodeBlock (
                    0x3190, 0x319F,
                    "Kanbun"));

            _blocksList.Add (                              //   31A0     31BF //
                0x31A0,
                new UnicodeBlock (
                    0x31A0, 0x31BF,
                    "Bopomofo Extended"));

            _blocksList.Add (                              //   31C0     31EF //
                0x31C0,
                new UnicodeBlock (
                    0x31C0, 0x31EF,
                    "CJK Strokes"));

            _blocksList.Add (                              //   31F0     31FF //
                0x31F0,
                new UnicodeBlock (
                    0x31F0, 0x31FF,
                    "Katakana Phonetic Extensions"));

            _blocksList.Add (                              //   3200     32FF //
                0x3200,
                new UnicodeBlock (
                    0x3200, 0x32FF,
                    "Enclosed CJK Letters and Months"));

            _blocksList.Add (                              //   3300     33FF //
                0x3300,
                new UnicodeBlock (
                    0x3300, 0x33FF,
                    "CJK Compatibility"));

            _blocksList.Add (                              //   3400     4DBF //
                0x3400,
                new UnicodeBlock (
                    0x3400, 0x4DBF,
                    "CJK Unified Ideographs Extension A"));

            _blocksList.Add (                              //   4DC0     4DFF //
                0x4DC0,
                new UnicodeBlock (
                    0x4DC0, 0x4DFF,
                    "Yijing Hexagram Symbols"));

            _blocksList.Add (                              //   4E00     9FFF //
                0x4E00,
                new UnicodeBlock (
                    0x4E00, 0x9FFF,
                    "CJK Unified Ideographs"));

            _blocksList.Add (                              //   A000     A48F //
                0xA000,
                new UnicodeBlock (
                    0xA000, 0xA48F,
                    "Yi Syllables"));

            _blocksList.Add (                              //   A490     A4CF //
                0xA490,
                new UnicodeBlock (
                    0xA490, 0xA4CF,
                    "Yi Radicals"));

            _blocksList.Add (                              //   A4D0     A4FF //
                0xA4D0,
                new UnicodeBlock (
                    0xA4D0, 0xA4FF,
                    "Lisu"));

            _blocksList.Add (                              //   A500     A63F //
                0xA500,
                new UnicodeBlock (
                    0xA500, 0xA63F,
                    "Vai"));

            _blocksList.Add (                              //   A640     A69F //
                0xA640,
                new UnicodeBlock (
                    0xA640, 0xA69F,
                    "Cyrillic Extended-B"));

            _blocksList.Add (                              //   A6A0     A6FF //
                0xA6A0,
                new UnicodeBlock (
                    0xA6A0, 0xA6FF,
                    "Bamum"));

            _blocksList.Add (                              //   A700     A71F //
                0xA700,
                new UnicodeBlock (
                    0xA700, 0xA71F,
                    "Modifier Tone Letters"));

            _blocksList.Add (                              //   A720     A7FF //
                0xA720,
                new UnicodeBlock (
                    0xA720, 0xA7FF,
                    "Latin Extended-D"));

            _blocksList.Add (                              //   A800     A82F //
                0xA800,
                new UnicodeBlock (
                    0xA800, 0xA82F,
                    "Syloti Nagri"));

            _blocksList.Add (                              //   A830     A83F //
                0xA830,
                new UnicodeBlock (
                    0xA830, 0xA83F,
                    "Common Indic Number Forms"));

            _blocksList.Add (                              //   A840     A87F //
                0xA840,
                new UnicodeBlock (
                    0xA840, 0xA87F,
                    "Phags-pa"));

            _blocksList.Add (                              //   A880     A8DF //
                0xA880,
                new UnicodeBlock (
                    0xA880, 0xA8DF,
                    "Saurashtra"));

            _blocksList.Add (                              //   A8E0     A8FF //
                0xA8E0,
                new UnicodeBlock (
                    0xA8E0, 0xA8FF,
                    "Devanagari Extended"));

            _blocksList.Add (                              //   A900     A92F //
                0xA900,
                new UnicodeBlock (
                    0xA900, 0xA92F,
                    "Kayah Li"));

            _blocksList.Add (                              //   A930     A95F //
                0xA930,
                new UnicodeBlock (
                    0xA930, 0xA95F,
                    "Rejang"));

            _blocksList.Add (                              //   A960     A97F //
                0xA960,
                new UnicodeBlock (
                    0xA960, 0xA97F,
                    "Hangul Jamo Extended-A"));

            _blocksList.Add (                              //   A980     A9DF //
                0xA980,
                new UnicodeBlock (
                    0xA980, 0xA9DF,
                    "Javanese"));

            _blocksList.Add (                              //   A9E0     A9FF //
                0xA9E0,
                new UnicodeBlock (
                    0xA9E0, 0xA9FF,
                    "Myanmar Extended-B"));

            _blocksList.Add (                              //   AA00     AA5F //
                0xAA00,
                new UnicodeBlock (
                    0xAA00, 0xAA5F,
                    "Cham"));

            _blocksList.Add (                              //   AA60     AA7F //
                0xAA60,
                new UnicodeBlock (
                    0xAA60, 0xAA7F,
                    "Myanmar Extended-A"));

            _blocksList.Add (                              //   AA80     AADF //
                0xAA80,
                new UnicodeBlock (
                    0xAA80, 0xAADF,
                    "Tai Viet"));

            _blocksList.Add (                              //   AAE0     AAFF //
                0xAAE0,
                new UnicodeBlock (
                    0xAAE0, 0xAAFF,
                    "Meetei Mayek Extensions"));

            _blocksList.Add (                              //   AB00     AB2F //
                0xAB00,
                new UnicodeBlock (
                    0xAB00, 0xAB2F,
                    "Ethiopic Extended-A"));

            _blocksList.Add (                              //   AB30     AB6F //
                0xAB30,
                new UnicodeBlock (
                    0xAB30, 0xAB6F,
                    "Latin Extended-E"));

            _blocksList.Add (                              //   AB70     ABBF //
                0xAB70,
                new UnicodeBlock (
                    0xAB70, 0xABBF,
                    "Cherokee Supplement"));

            _blocksList.Add (                              //   ABC0     ABFF //
                0xABC0,
                new UnicodeBlock (
                    0xABC0, 0xABFF,
                    "Meetei Mayek"));

            _blocksList.Add (                              //   AC00     D7AF //
                0xAC00,
                new UnicodeBlock (
                    0xAC00, 0xD7AF,
                    "Hangul Syllables"));

            _blocksList.Add (                              //   D7B0     D7FF //
                0xD7B0,
                new UnicodeBlock (
                    0xD7B0, 0xD7FF,
                    "Hangul Jamo Extended-B"));

            _blocksList.Add (                              //   D800     DB7F //
                0xD800,
                new UnicodeBlock (
                    0xD800, 0xDB7F,
                    "High Surrogates"));

            _blocksList.Add (                              //   DB80     DBFF //
                0xDB80,
                new UnicodeBlock (
                    0xDB80, 0xDBFF,
                    "High Private Use Surrogates"));

            _blocksList.Add (                              //   DC00     DFFF //
                0xDC00,
                new UnicodeBlock (
                    0xDC00, 0xDFFF,
                    "Low Surrogates"));

            _blocksList.Add (                              //   E000     F8FF //
                0xE000,
                new UnicodeBlock (
                    0xE000, 0xF8FF,
                    "Private Use Area"));

            _blocksList.Add (                              //   F900     FAFF //
                0xF900,
                new UnicodeBlock (
                    0xF900, 0xFAFF,
                    "CJK Compatibility Ideographs"));

            _blocksList.Add (                              //   FB00     FB4F //
                0xFB00,
                new UnicodeBlock (
                    0xFB00, 0xFB4F,
                    "Alphabetic Presentation Forms"));

            _blocksList.Add (                              //   FB50     FDFF //
                0xFB50,
                new UnicodeBlock (
                    0xFB50, 0xFDFF,
                    "Arabic Presentation Forms-A"));

            _blocksList.Add (                              //   FE00     FE0F //
                0xFE00,
                new UnicodeBlock (
                    0xFE00, 0xFE0F,
                    "Variation Selectors"));

            _blocksList.Add (                              //   FE10     FE1F //
                0xFE10,
                new UnicodeBlock (
                    0xFE10, 0xFE1F,
                    "Vertical Forms"));

            _blocksList.Add (                              //   FE20     FE2F //
                0xFE20,
                new UnicodeBlock (
                    0xFE20, 0xFE2F,
                    "Combining Half Marks"));

            _blocksList.Add (                              //   FE30     FE4F //
                0xFE30,
                new UnicodeBlock (
                    0xFE30, 0xFE4F,
                    "CJK Compatibility Forms"));

            _blocksList.Add (                              //   FE50     FE6F //
                0xFE50,
                new UnicodeBlock (
                    0xFE50, 0xFE6F,
                    "Small Form Variants"));

            _blocksList.Add (                              //   FE70     FEFF //
                0xFE70,
                new UnicodeBlock (
                    0xFE70, 0xFEFF,
                    "Arabic Presentation Forms-B"));

            _blocksList.Add (                              //   FF00     FFEF //
                0xFF00,
                new UnicodeBlock (
                    0xFF00, 0xFFEF,
                    "Halfwidth and Fullwidth Forms"));

            _blocksList.Add (                              //   FFF0     FFFF //
                0xFFF0,
                new UnicodeBlock (
                    0xFFF0, 0xFFFF,
                    "Specials"));

            _blocksList.Add (                              //  10000    1007F //
                0x10000,
                new UnicodeBlock (
                    0x10000, 0x1007F,
                    "Linear B Syllabary"));

            _blocksList.Add (                              //  10080    100FF //
                0x10080,
                new UnicodeBlock (
                    0x10080, 0x100FF,
                    "Linear B Ideograms"));

            _blocksList.Add (                              //  10100    1013F //
                0x10100,
                new UnicodeBlock (
                    0x10100, 0x1013F,
                    "Aegean Numbers"));

            _blocksList.Add (                              //  10140    1018F //
                0x10140,
                new UnicodeBlock (
                    0x10140, 0x1018F,
                    "Ancient Greek Numbers"));

            _blocksList.Add (                              //  10190    101CF //
                0x10190,
                new UnicodeBlock (
                    0x10190, 0x101CF,
                    "Ancient Symbols"));

            _blocksList.Add (                              //  101D0    101FF //
                0x101D0,
                new UnicodeBlock (
                    0x101D0, 0x101FF,
                    "Phaistos Disc"));

            _blocksList.Add (                              //  10280    1029F //
                0x10280,
                new UnicodeBlock (
                    0x10280, 0x1029F,
                    "Lycian"));

            _blocksList.Add (                              //  102A0    102DF //
                0x102A0,
                new UnicodeBlock (
                    0x102A0, 0x102DF,
                    "Carian"));

            _blocksList.Add (                              //  102E0    102FF //
                0x102E0,
                new UnicodeBlock (
                    0x102E0, 0x102FF,
                    "Coptic Epact Numbers"));

            _blocksList.Add (                              //  10300    1032F //
                0x10300,
                new UnicodeBlock (
                    0x10300, 0x1032F,
                    "Old Italic"));

            _blocksList.Add (                              //  10330    1034F //
                0x10330,
                new UnicodeBlock (
                    0x10330, 0x1034F,
                    "Gothic"));

            _blocksList.Add (                              //  10350    1037F //
                0x10350,
                new UnicodeBlock (
                    0x10350, 0x1037F,
                    "Old Permic"));

            _blocksList.Add (                              //  10380    1039F //
                0x10380,
                new UnicodeBlock (
                    0x10380, 0x1039F,
                    "Ugaritic"));

            _blocksList.Add (                              //  103A0    103DF //
                0x103A0,
                new UnicodeBlock (
                    0x103A0, 0x103DF,
                    "Old Persian"));

            _blocksList.Add (                              //  10400    1044F //
                0x10400,
                new UnicodeBlock (
                    0x10400, 0x1044F,
                    "Deseret"));

            _blocksList.Add (                              //  10450    1047F //
                0x10450,
                new UnicodeBlock (
                    0x10450, 0x1047F,
                    "Shavian"));

            _blocksList.Add (                              //  10480    104AF //
                0x10480,
                new UnicodeBlock (
                    0x10480, 0x104AF,
                    "Osmanya"));

            _blocksList.Add (                              //  104B0    104FF //
                0x104B0,
                new UnicodeBlock (
                    0x104B0, 0x104FF,
                    "Osage"));

            _blocksList.Add (                              //  10500    1052F //
                0x10500,
                new UnicodeBlock (
                    0x10500, 0x1052F,
                    "Elbasan"));

            _blocksList.Add (                              //  10530    1056F //
                0x10530,
                new UnicodeBlock (
                    0x10530, 0x1056F,
                    "Caucasian Albanian"));

            _blocksList.Add (                              //  10600    1077F //
                0x10600,
                new UnicodeBlock (
                    0x10600, 0x1077F,
                    "Linear A"));

            _blocksList.Add (                              //  10800    1083F //
                0x10800,
                new UnicodeBlock (
                    0x10800, 0x1083F,
                    "Cypriot Syllabary"));

            _blocksList.Add (                              //  10840    1085F //
                0x10840,
                new UnicodeBlock (
                    0x10840, 0x1085F,
                    "Imperial Aramaic"));

            _blocksList.Add (                              //  10860    1087F //
                0x10860,
                new UnicodeBlock (
                    0x10860, 0x1087F,
                    "Palmyrene"));

            _blocksList.Add (                              //  10880    108AF //
                0x10880,
                new UnicodeBlock (
                    0x10880, 0x108AF,
                    "Nabataean"));

            _blocksList.Add (                              //  108E0    108FF //
                0x108E0,
                new UnicodeBlock (
                    0x108E0, 0x108FF,
                    "Hatran"));

            _blocksList.Add (                              //  10900    1091F //
                0x10900,
                new UnicodeBlock (
                    0x10900, 0x1091F,
                    "Phoenician"));

            _blocksList.Add (                              //  10920    1093F //
                0x10920,
                new UnicodeBlock (
                    0x10920, 0x1093F,
                    "Lydian"));

            _blocksList.Add (                              //  10980    1099F //
                0x10980,
                new UnicodeBlock (
                    0x10980, 0x1099F,
                    "Meroitic Hieroglyphs"));

            _blocksList.Add (                              //  109A0    109FF //
                0x109A0,
                new UnicodeBlock (
                    0x109A0, 0x109FF,
                    "Meroitic Cursive"));

            _blocksList.Add (                              //  10A00    10A5F //
                0x10A00,
                new UnicodeBlock (
                    0x10A00, 0x10A5F,
                    "Kharoshthi"));

            _blocksList.Add (                              //  10A60    10A7F //
                0x10A60,
                new UnicodeBlock (
                    0x10A60, 0x10A7F,
                    "Old South Arabian"));

            _blocksList.Add (                              //  10A80    10A9F //
                0x10A80,
                new UnicodeBlock (
                    0x10A80, 0x10A9F,
                    "Old North Arabian"));

            _blocksList.Add (                              //  10AC0    10AFF //
                0x10AC0,
                new UnicodeBlock (
                    0x10AC0, 0x10AFF,
                    "Manichaean"));

            _blocksList.Add (                              //  10B00    10B3F //
                0x10B00,
                new UnicodeBlock (
                    0x10B00, 0x10B3F,
                    "Avestan"));

            _blocksList.Add (                              //  10B40    10B5F //
                0x10B40,
                new UnicodeBlock (
                    0x10B40, 0x10B5F,
                    "Inscriptional Parthian"));

            _blocksList.Add (                              //  10B60    10B7F //
                0x10B60,
                new UnicodeBlock (
                    0x10B60, 0x10B7F,
                    "Inscriptional Pahlavi"));

            _blocksList.Add (                              //  10B80    10BAF //
                0x10B80,
                new UnicodeBlock (
                    0x10B80, 0x10BAF,
                    "Psalter Pahlavi"));

            _blocksList.Add (                              //  10C00    10C4F //
                0x10C00,
                new UnicodeBlock (
                    0x10C00, 0x10C4F,
                    "Old Turkic"));

            _blocksList.Add (                              //  10C80    10CFF //
                0x10C80,
                new UnicodeBlock (
                    0x10C80, 0x10CFF,
                    "Old Hungarian"));

            _blocksList.Add (                              //  10E60    10E7F //
                0x10E60,
                new UnicodeBlock (
                    0x10E60, 0x10E7F,
                    "Rumi Numeral Symbols"));

            _blocksList.Add (                              //  11000    1107F //
                0x11000,
                new UnicodeBlock (
                    0x11000, 0x1107F,
                    "Brahmi"));

            _blocksList.Add (                              //  11080    110CF //
                0x11080,
                new UnicodeBlock (
                    0x11080, 0x110CF,
                    "Kaithi"));

            _blocksList.Add (                              //  110D0    110FF //
                0x110D0,
                new UnicodeBlock (
                    0x110D0, 0x110FF,
                    "Sora Sompeng"));

            _blocksList.Add (                              //  11100    1114F //
                0x11100,
                new UnicodeBlock (
                    0x11100, 0x1114F,
                    "Chakma"));

            _blocksList.Add (                              //  11150    1117F //
                0x11150,
                new UnicodeBlock (
                    0x11150, 0x1117F,
                    "Mahajani"));

            _blocksList.Add (                              //  11180    111DF //
                0x11180,
                new UnicodeBlock (
                    0x11180, 0x111DF,
                    "Sharada"));

            _blocksList.Add (                              //  111E0    111FF //
                0x111E0,
                new UnicodeBlock (
                    0x111E0, 0x111FF,
                    "Sinhala Archaic Numbers"));

            _blocksList.Add (                              //  11200    1124F //
                0x11200,
                new UnicodeBlock (
                    0x11200, 0x1124F,
                    "Khojki"));

            _blocksList.Add (                              //  11280    112AF //
                0x11280,
                new UnicodeBlock (
                    0x11280, 0x112AF,
                    "Multani"));

            _blocksList.Add (                              //  112B0    112FF //
                0x112B0,
                new UnicodeBlock (
                    0x112B0, 0x112FF,
                    "Khudawadi"));

            _blocksList.Add (                              //  11300    1137F //
                0x11300,
                new UnicodeBlock (
                    0x11300, 0x1137F,
                    "Grantha"));

            _blocksList.Add (                              //  11400    1147F //
                0x11400,
                new UnicodeBlock (
                    0x11400, 0x1147F,
                    "Newa"));

            _blocksList.Add (                              //  11480    114DF //
                0x11480,
                new UnicodeBlock (
                    0x11480, 0x114DF,
                    "Tirhuta"));

            _blocksList.Add (                              //  11580    115FF //
                0x11580,
                new UnicodeBlock (
                    0x11580, 0x115FF,
                    "Siddham"));

            _blocksList.Add (                              //  11600    1165F //
                0x11600,
                new UnicodeBlock (
                    0x11600, 0x1165F,
                    "Modi"));

            _blocksList.Add (                              //  11660    1167F //
                0x11660,
                new UnicodeBlock (
                    0x11660, 0x1167F,
                    "Mongolian Supplement"));

            _blocksList.Add (                              //  11680    116CF //
                0x11680,
                new UnicodeBlock (
                    0x11680, 0x116CF,
                    "Takri"));

            _blocksList.Add (                              //  11700    1173F //
                0x11700,
                new UnicodeBlock (
                    0x11700, 0x1173F,
                    "Ahom"));

            _blocksList.Add (                              //  118A0    118FF //
                0x118A0,
                new UnicodeBlock (
                    0x118A0, 0x118FF,
                    "Warang Citi"));

            _blocksList.Add (                              //  11A00    11A4F //
                0x11A00,
                new UnicodeBlock (
                    0x11A00, 0x11A4F,
                    "Zanabazar Square"));

            _blocksList.Add (                              //  11A50    11AAF //
                0x11A50,
                new UnicodeBlock (
                    0x11A50, 0x11AAF,
                    "Soyombo"));

            _blocksList.Add (                              //  11AC0    11AFF //
                0x11AC0,
                new UnicodeBlock (
                    0x11AC0, 0x11AFF,
                    "Pau Cin Hau"));

            _blocksList.Add (                              //  11C00    11C6F //
                0x11C00,
                new UnicodeBlock (
                    0x11C00, 0x11C6F,
                    "Bhaiksuki"));

            _blocksList.Add (                              //  11C70    11CBF //
                0x11C70,
                new UnicodeBlock (
                    0x11C70, 0x11CBF,
                    "Marchen"));

            _blocksList.Add (                              //  11D00    11D5F //
                0x11D00,
                new UnicodeBlock (
                    0x11D00, 0x11D5F,
                    "Masaram Gondi"));

            _blocksList.Add (                              //  12000    123FF //
                0x12000,
                new UnicodeBlock (
                    0x12000, 0x123FF,
                    "Cuneiform"));

            _blocksList.Add (                              //  12400    1247F //
                0x12400,
                new UnicodeBlock (
                    0x12400, 0x1247F,
                    "Cuneiform Numbers and Punctuation"));

            _blocksList.Add (                              //  12480    1254F //
                0x12480,
                new UnicodeBlock (
                    0x12480, 0x1254F,
                    "Early Dynastic Cuneiform"));

            _blocksList.Add (                              //  13000    1342F //
                0x13000,
                new UnicodeBlock (
                    0x13000, 0x1342F,
                    "Egyptian Hieroglyphs"));

            _blocksList.Add (                              //  14400    1467F //
                0x14400,
                new UnicodeBlock (
                    0x14400, 0x1467F,
                    "Anatolian Hieroglyphs"));

            _blocksList.Add (                              //  16800    16A3F //
                0x16800,
                new UnicodeBlock (
                    0x16800, 0x16A3F,
                    "Bamum Supplement"));

            _blocksList.Add (                              //  16A40    16A6F //
                0x16A40,
                new UnicodeBlock (
                    0x16A40, 0x16A6F,
                    "Mro"));

            _blocksList.Add (                              //  16AD0    16AFF //
                0x16AD0,
                new UnicodeBlock (
                    0x16AD0, 0x16AFF,
                    "Bassa Vah"));

            _blocksList.Add (                              //  16B00    16B8F //
                0x16B00,
                new UnicodeBlock (
                    0x16B00, 0x16B8F,
                    "Pahawh Hmong"));

            _blocksList.Add (                              //  16F00    16F9F //
                0x16F00,
                new UnicodeBlock (
                    0x16F00, 0x16F9F,
                    "Miao"));

            _blocksList.Add (                              //  16FE0    16FFF //
                0x16FE0,
                new UnicodeBlock (
                    0x16FE0, 0x16FFF,
                    "Ideographic Symbols and Punctuation"));

            _blocksList.Add (                              //  17000    187FF //
                0x17000,
                new UnicodeBlock (
                    0x17000, 0x187FF,
                    "Tangut"));

            _blocksList.Add (                              //  18800    18AFF //
                0x18800,
                new UnicodeBlock (
                    0x18800, 0x18AFF,
                    "Tangut Components"));

            _blocksList.Add (                              //  1B000    1B0FF //
                0x1B000,
                new UnicodeBlock (
                    0x1B000, 0x1B0FF,
                    "Kana Supplement"));

            _blocksList.Add (                              //  1B100    1B12F //
                0x1B100,
                new UnicodeBlock (
                    0x1B100, 0x1B12F,
                    "Kana Extended-A"));

            _blocksList.Add (                              //  1B170    1B2FF //
                0x1B170,
                new UnicodeBlock (
                    0x1B170, 0x1B2FF,
                    "Nushu"));

            _blocksList.Add (                              //  1BC00    1BC9F //
                0x1BC00,
                new UnicodeBlock (
                    0x1BC00, 0x1BC9F,
                    "Duployan"));

            _blocksList.Add (                              //  1BCA0    1BCAF //
                0x1BCA0,
                new UnicodeBlock (
                    0x1BCA0, 0x1BCAF,
                    "Shorthand Format Controls"));

            _blocksList.Add (                              //  1D000    1D0FF //
                0x1D000,
                new UnicodeBlock (
                    0x1D000, 0x1D0FF,
                    "Byzantine Musical Symbols"));

            _blocksList.Add (                              //  1D100    1D1FF //
                0x1D100,
                new UnicodeBlock (
                    0x1D100, 0x1D1FF,
                    "Musical Symbols"));

            _blocksList.Add (                              //  1D200    1D24F //
                0x1D200,
                new UnicodeBlock (
                    0x1D200, 0x1D24F,
                    "Ancient Greek Musical Notation"));

            _blocksList.Add (                              //  1D300    1D35F //
                0x1D300,
                new UnicodeBlock (
                    0x1D300, 0x1D35F,
                    "Tai Xuan Jing Symbols"));

            _blocksList.Add (                              //  1D360    1D37F //
                0x1D360,
                new UnicodeBlock (
                    0x1D360, 0x1D37F,
                    "Counting Rod Numerals"));

            _blocksList.Add (                              //  1D400    1D7FF //
                0x1D400,
                new UnicodeBlock (
                    0x1D400, 0x1D7FF,
                    "Mathematical Alphanumeric Symbols"));

            _blocksList.Add (                              //  1D800    1DAAF //
                0x1D800,
                new UnicodeBlock (
                    0x1D800, 0x1DAAF,
                    "Sutton SignWriting"));

            _blocksList.Add (                              //  1E000    1E02F //
                0x1E000,
                new UnicodeBlock (
                    0x1E000, 0x1E02F,
                    "Glagolitic Supplement"));

            _blocksList.Add (                              //  1E800    1E8DF //
                0x1E800,
                new UnicodeBlock (
                    0x1E800, 0x1E8DF,
                    "Mende Kikakui"));

            _blocksList.Add (                              //  1E900    1E95F //
                0x1E900,
                new UnicodeBlock (
                    0x1E900, 0x1E95F,
                    "Adlam"));

            _blocksList.Add (                              //  1EE00    1EEFF //
                0x1EE00,
                new UnicodeBlock (
                    0x1EE00, 0x1EEFF,
                    "Arabic Mathematical Alphabetic Symbols"));

            _blocksList.Add (                              //  1F000    1F02F //
                0x1F000,
                new UnicodeBlock (
                    0x1F000, 0x1F02F,
                    "Mahjong Tiles"));

            _blocksList.Add (                              //  1F030    1F09F //
                0x1F030,
                new UnicodeBlock (
                    0x1F030, 0x1F09F,
                    "Domino Tiles"));

            _blocksList.Add (                              //  1F0A0    1F0FF //
                0x1F0A0,
                new UnicodeBlock (
                    0x1F0A0, 0x1F0FF,
                    "Playing Cards"));

            _blocksList.Add (                              //  1F100    1F1FF //
                0x1F100,
                new UnicodeBlock (
                    0x1F100, 0x1F1FF,
                    "Enclosed Alphanumeric Supplement"));

            _blocksList.Add (                              //  1F200    1F2FF //
                0x1F200,
                new UnicodeBlock (
                    0x1F200, 0x1F2FF,
                    "Enclosed Ideographic Supplement"));

            _blocksList.Add (                              //  1F300    1F5FF //
                0x1F300,
                new UnicodeBlock (
                    0x1F300, 0x1F5FF,
                    "Miscellaneous Symbols and Pictographs"));

            _blocksList.Add (                              //  1F600    1F64F //
                0x1F600,
                new UnicodeBlock (
                    0x1F600, 0x1F64F,
                    "Emoticons"));

            _blocksList.Add (                              //  1F650    1F67F //
                0x1F650,
                new UnicodeBlock (
                    0x1F650, 0x1F67F,
                    "Ornamental Dingbats"));

            _blocksList.Add (                              //  1F680    1F6FF //
                0x1F680,
                new UnicodeBlock (
                    0x1F680, 0x1F6FF,
                    "Transport and Map Symbols"));

            _blocksList.Add (                              //  1F700    1F77F //
                0x1F700,
                new UnicodeBlock (
                    0x1F700, 0x1F77F,
                    "Alchemical Symbols"));

            _blocksList.Add (                              //  1F780    1F7FF //
                0x1F780,
                new UnicodeBlock (
                    0x1F780, 0x1F7FF,
                    "Geometric Shapes Extended"));

            _blocksList.Add (                              //  1F800    1F8FF //
                0x1F800,
                new UnicodeBlock (
                    0x1F800, 0x1F8FF,
                    "Supplemental Arrows-C"));

            _blocksList.Add (                              //  1F900    1F9FF //
                0x1F900,
                new UnicodeBlock (
                    0x1F900, 0x1F9FF,
                    "Supplemental Symbols and Pictographs"));

            _blocksList.Add (                              //  20000    2A6DF //
                0x20000,
                new UnicodeBlock (
                    0x20000, 0x2A6DF,
                    "CJK Unified Ideographs Extension B"));

            _blocksList.Add (                              //  2A700    2B73F //
                0x2A700,
                new UnicodeBlock (
                    0x2A700, 0x2B73F,
                    "CJK Unified Ideographs Extension C"));

            _blocksList.Add (                              //  2B740    2B81F //
                0x2B740,
                new UnicodeBlock (
                    0x2B740, 0x2B81F,
                    "CJK Unified Ideographs Extension D"));

            _blocksList.Add (                              //  2B820    2CEAF //
                0x2B820,
                new UnicodeBlock (
                    0x2B820, 0x2CEAF,
                    "CJK Unified Ideographs Extension E"));

            _blocksList.Add (                              //  2CEB0    2EBEF //
                0x2CEB0,
                new UnicodeBlock (
                    0x2CEB0, 0x2EBEF,
                    "CJK Unified Ideographs Extension F //"));

            _blocksList.Add (                              //  2F800    2FA1F //
                0x2F800,
                new UnicodeBlock (
                    0x2F800, 0x2FA1F,
                    "CJK Compatibility Ideographs Supplement"));

            _blocksList.Add (                              //  E0000    E007F //
                0xE0000,
                new UnicodeBlock (
                    0xE0000, 0xE007F,
                    "Tags"));

            _blocksList.Add (                              //  E0100    E01EF //
                0xE0100,
                new UnicodeBlock (
                    0xE0100, 0xE01EF,
                    "Variation Selectors Supplement"));

            _blocksList.Add (                              //  F0000    FFFFF //
                0xF0000,
                new UnicodeBlock (
                    0xF0000, 0xFFFFF,
                    "Supplementary Private Use Area-A"));

            _blocksList.Add (                              // 100000   10FFFF //
                0x100000,
                new UnicodeBlock (
                    0x100000, 0x10FFFF,
                    "Supplementary Private Use Area-B"));


            _blocksCount = _blocksList.Count;
        }
    }
}
