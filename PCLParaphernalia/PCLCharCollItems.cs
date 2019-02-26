using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines the initial / current settings of PCL Character
    /// Complement/Requirement Collection bits, as used in (unbound) Font
    /// headers and Symbol Set definitions.
    /// 
    /// A font and a symbol set are compatible only if the result of ANDing the
    /// (64-bit) Character Complement field of the font definition with the
    /// (64-bit) Character Requirements field of the symbol set definition is
    /// 64 bits (8 bytes) of zero.
    /// 
    /// In the descriptions below:
    ///     bit 0   is the least significant bit of the eighth byte
    ///     bit 63  is the most  significant bit of the first  byte 
    /// 
    /// © Chris Hutchinson 2013
    /// 
    /// </summary>

    class PCLCharCollItems
    {
        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d C o m p L i s t U n i c o d e                              //
        //                                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Character Complement bits for TrueType (Unicode-indexed) font.     //
        //                                                                    //
        // Bits 2,1,0:  Symbol index identifier: 1 1 0 = Unicode              //
        // Bits 63->3:  Collection compatibilty bits                          //
        //              unset: font is compatible with associated collection  //
        //              set:   font is not compatible with collection         //
        //                                                                    //
        // Because this is the Complement of the corresponding symbol set     //
        // Requirements field, the IsChecked field is set if the bit is NOT   //
        // set.                                                               //
        //                                                                    //
        //--------------------------------------------------------------------//

        public ObservableCollection<PCLCharCollItem> loadCompListUnicode ()
        {
            ObservableCollection<PCLCharCollItem> objColEmp =
                new ObservableCollection<PCLCharCollItem> ();

            PCLCharCollections.eBitType bitType;
            Int32 collsCt;

            collsCt = PCLCharCollections.getCollsCount ();

            Int32 bitNo;
            String desc;

            Boolean itemEnabled = true;
            Boolean itemChecked = false;

            for (Int32 i = 0; i < collsCt; i++)
            {
                bitNo = PCLCharCollections.getBitNo (i);
                bitType = PCLCharCollections.getBitType (i);
                desc = PCLCharCollections.getDescUnicode (i);

                if (bitType == PCLCharCollections.eBitType.Index_0)
                {
                    itemEnabled = false;
                    itemChecked = true;
                }
                else if (bitType == PCLCharCollections.eBitType.Index_1)
                {
                    itemEnabled = false;
                    itemChecked = false;
                }
                else if (bitType == PCLCharCollections.eBitType.Index_2)
                {
                    itemEnabled = false;
                    itemChecked = false;
                }
                else
                {
                    itemEnabled = true;
                    itemChecked = false;
                }

                objColEmp.Add (new PCLCharCollItem (bitNo,
                                                    bitType,
                                                    desc,
                                                    itemEnabled,
                                                    itemChecked));
            }

            return objColEmp;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d R e q L i s t M S L                                        //
        //                                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Character Requirements bits for Intellifont (MSL-indexed) symbol   //
        // set.                                                               //
        //                                                                    //
        // Bits 2,1,0:  Symbol index identifier: 0 0 0 = MSL                  //
        // Bits 63->3:  Collection compatibilty bits                          //
        //              unset: associated collection is not required          //
        //              set:   associated collection is required              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public ObservableCollection<PCLCharCollItem> loadReqListMSL ()
        {
            ObservableCollection<PCLCharCollItem> objColEmp =
                new ObservableCollection<PCLCharCollItem>();

            PCLCharCollections.eBitType bitType;
            Int32 collsCt;

            collsCt = PCLCharCollections.getCollsCount ();

            Int32 bitNo;
            String desc;

            Boolean itemEnabled = true;
            Boolean itemChecked = false;

            for (Int32 i = 0; i < collsCt; i++)
            {
                bitNo   = PCLCharCollections.getBitNo (i);
                bitType = PCLCharCollections.getBitType (i);
                desc    = PCLCharCollections.getDescMSL (i);

                if (bitType == PCLCharCollections.eBitType.Collection)
                {
                    itemEnabled = true;
                    itemChecked = false;
                }
                else
                {
                    itemEnabled = false;
                    itemChecked = false;
                }

                objColEmp.Add (new PCLCharCollItem (bitNo,
                                                    bitType,
                                                    desc,
                                                    itemEnabled,
                                                    itemChecked));
            }
            
            return objColEmp;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // l o a d R e q L i s t U n i c o d e                                //
        //                                                                    //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Character Requirements bits for TrueType (Unicode-indexed) symbol  //
        // set.                                                               //
        //                                                                    //
        // Bits 2,1,0:  Symbol index identifier: 0 0 1 = Unicode              //
        // Bits 63->3:  Collection compatibilty bits                          //
        //              unset: associated collection is not required          //
        //              set:   associated collection is required              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public ObservableCollection<PCLCharCollItem> loadReqListUnicode()
        {
            ObservableCollection<PCLCharCollItem> objColEmp =
                new ObservableCollection<PCLCharCollItem>();

            PCLCharCollections.eBitType bitType;
            Int32 collsCt;

            collsCt = PCLCharCollections.getCollsCount ();

            Int32 bitNo;
            String desc;

            Boolean itemEnabled = true;
            Boolean itemChecked = false;

            for (Int32 i = 0; i < collsCt; i++)
            {
                bitNo   = PCLCharCollections.getBitNo (i);
                bitType = PCLCharCollections.getBitType (i);
                desc    = PCLCharCollections.getDescUnicode (i);

                if (bitType == PCLCharCollections.eBitType.Collection)
                {
                    itemEnabled = true;
                    itemChecked = false;
                }
                else if (bitType == PCLCharCollections.eBitType.Index_0)
                {
                    itemEnabled = false;
                    itemChecked = true;
                }
                else
                {
                    itemEnabled = false;
                    itemChecked = false;
                }

                objColEmp.Add (new PCLCharCollItem (bitNo,
                                                    bitType,
                                                    desc,
                                                    itemEnabled,
                                                    itemChecked));
            }
            
            return objColEmp;
        }
    }
}
