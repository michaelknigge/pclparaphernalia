using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class TEMPORARY test pad.
    /// 
    /// © Chris Hutchinson 2010
    /// 
    /// </summary>

    static class TEMP_scratchPad
    {
        private static void bits_01()
        {
            //----------------------------------------------------------------//
            // Unicode encoding                                               //
            //----------------------------------------------------------------//

            Byte[] bytes;

            // Unicode characters.
            Char[] chars = new Char[] {'\u0023', // #
                                       '\u0025', // %
                                       '\u03a0', // Pi
                                       '\u03a3'  // Sigma
                                      };

            UnicodeEncoding Unicode = new UnicodeEncoding ();

            int byteCount = Unicode.GetByteCount (chars, 1, 2);
            bytes = new Byte[byteCount];
            int bytesEncodedCount = Unicode.GetBytes (chars, 1, 2, bytes, 0);

            // At this point:
            //  byteCount           = 4
            //  bytesEncodedCount   = 4
            //  bytes[]             = { '\x25', '\x00', '\xa0', '\x03' }
        }

        //--------------------------------------------------------------------//

        private static void bits_02()
        {
            //----------------------------------------------------------------//
            // UTF8 encoding                                                  //
            //----------------------------------------------------------------//

            Byte[] bytes;

            // Unicode characters.
            Char[] chars = new Char[] {'\u0023', // #
                                       '\u0025', // %
                                       '\u03a0', // Pi
                                       '\u03a3'  // Sigma
                                      };

            UTF8Encoding utf8 = new UTF8Encoding ();

            int byteCount = utf8.GetByteCount (chars, 1, 2);
            bytes = new Byte[byteCount];
            int bytesEncodedCount = utf8.GetBytes (chars, 1, 2, bytes, 0);

            // At this point:
            //  byteCount           = 3
            //  bytesEncodedCount   = 3
            //  bytes[]             = { '\x23', '\xce', '\xa0' }

            byteCount = utf8.GetByteCount (chars, 0, 2);
            bytes = new Byte[byteCount];
            bytesEncodedCount = utf8.GetBytes (chars, 0, 2, bytes, 0);

            // At this point:
            //  byteCount           = 2
            //  bytesEncodedCount   = 2
            //  bytes[]             = { '\x23', '\x25' }
        }

        //--------------------------------------------------------------------//

        private static void bits_03(Int32 coordX,
                                    Int32 coordY,
                                    String text)
        {
            //----------------------------------------------------------------//
            // Data bytes                                                     //
            //----------------------------------------------------------------//

            Int32 indxStd;

            Int16 textLen;

            String temp;

            Byte escape = 0x1b;

            Byte[] bufStd = new Byte[64];

            indxStd = 0;

            //----------------------------------------------------------------//

            bufStd[indxStd++] = escape;
            bufStd[indxStd++] = (Byte) '*';
            bufStd[indxStd++] = (Byte) 'p';

            temp = coordX.ToString ();
            textLen = (Int16) temp.Length;

            for (Int32 i = 0; i < textLen; i++)
            {
                bufStd[indxStd++] = (Byte) temp[i];
            }

            bufStd[indxStd++] = (Byte) 'x';

            temp = coordY.ToString ();
            textLen = (Int16) temp.Length;

            for (Int32 i = 0; i < textLen; i++)
            {
                bufStd[indxStd++] = (Byte) temp[i];
            }

            bufStd[indxStd++] = (Byte) 'Y';

            textLen = (Int16) text.Length;

            for (Int32 i = 0; i < textLen; i++)
            {
                bufStd[indxStd++] = (Byte) text[i];
            }

            //----------------------------------------------------------------//

            //  prnWriter.Write(bufStd, 0, indxStd);
            indxStd = 0;
        }

        //--------------------------------------------------------------------//

        private static void bits_04()
        {
            //----------------------------------------------------------------//
            // Enumerate the current set of system fonts,
            // and fill the combo box with the names of the fonts.
            //----------------------------------------------------------------//

            /*
            foreach (FontFamily fontFamily in Fonts.SystemFontFamilies)
            {
                // FontFamily.Source contains the font family name.
                comboBoxFonts.Items.Add(fontFamily.Source);
            }

            comboBoxFonts.SelectedIndex = 0;
            */
        }

        //--------------------------------------------------------------------//

        private static void bits_05()
        {
            /*
            //----------------------------------------------------------------//
            //                                                                //
            // Left-hand boxes.                                               //
            // Since the left logical page margin is in-board of the physical //
            // edge (and the unprintable region), we do this by making use of //
            // 180-degree text rotation.                                      //
            // Before doing this, the bottom margin must be set to zero (same //
            // as the top margin); this is done indirectly by setting VMI     //
            // (line spacing) to 1/48 inch, then setting text length to the   //
            // number of such lines possible in the physical page length.     //
            //                                                                //
            //----------------------------------------------------------------//

            PCL.setVMI(prnWriter, 1);

            PCL.setTextLength(prnWriter,
                               (Int16)((pageLength * 48) / _unitsPerInch));
            
            PCL.printDirection(prnWriter, 180);

            //----------------------------------------------------------------//

            posX = rightX;
            posY = 0;

            PCL.rectangleSolid(prnWriter, posX, posY,
                                boxOuterEdge, boxOuterEdge, false);

            posX += boxInnerOffset;
            posY += boxInnerOffset;

            PCL.rectangleSolid(prnWriter, posX, posY,
                                boxInnerEdge, boxInnerEdge, true);

            //----------------------------------------------------------------//

            posX = rightX;
            posY = bottomY;

            PCL.rectangleSolid(prnWriter, posX, posY,
                                boxOuterEdge, boxOuterEdge, false);

            posX += boxInnerOffset;
            posY += boxInnerOffset;

            PCL.rectangleSolid(prnWriter, posX, posY,
                                boxInnerEdge, boxInnerEdge, true);

            */

            /*
            //--------------------------------------------------------------------//
            //                                                        M e t h o d //
            // s e t C o l s A n a l y s i s                                      //
            //--------------------------------------------------------------------//
            //                                                                    //
            // Define datagrid columns for analysis, and bind to dataset.         //
            //                                                                    //
            //--------------------------------------------------------------------//

            private void setColsAnalysis()
            {
                DataGridTextColumn colOffset = new DataGridTextColumn ();
                DataGridTextColumn colSeq    = new DataGridTextColumn ();
                DataGridTextColumn colType   = new DataGridTextColumn ();
                DataGridTextColumn colDesc   = new DataGridTextColumn ();

                colOffset.Header = "Offset";
                colType.Header   = "Type";
                colSeq.Header    = "Sequence";
                colDesc.Header   = "Description";

                dgAnalysis.Columns.Clear ();
                dgAnalysis.Columns.Add (colOffset);
                dgAnalysis.Columns.Add (colType);
                dgAnalysis.Columns.Add (colSeq);
                dgAnalysis.Columns.Add (colDesc);

            //  Binding bindOffset = new Binding ("colOffset");
            //  bindOffset.Mode = BindingMode.OneWay;

            //  Binding bindSeq = new Binding ("colSeq");
            //  bindSeq.Mode = BindingMode.OneWay;

            //  Binding bindType = new Binding ("colType");
            //  bindType.Mode = BindingMode.OneWay;

            //  Binding bindDesc = new Binding ("colDesc");
            //  bindDesc.Mode = BindingMode.OneWay;

            //  colOffset.Binding = bindOffset;
            //  colSeq.Binding = bindSeq;
            //  colType.Binding = bindType;
            //  colDesc.Binding = bindDesc;
            }
            */







            /*
            //--------------------------------------------------------------------//
            // EXAMPLE

            // Put the next line into the Declarations section.
            private System.Data.DataSet dataSet;

            private void MakeDataTables()
            {
                // Run all of the functions. 
                MakeParentTable ();
                MakeChildTable ();
                MakeDataRelation ();
                BindToDataGrid ();
            }

            private void MakeParentTable()
            {
                // Create a new DataTable.
                System.Data.DataTable table = new DataTable ("ParentTable");
                // Declare variables for DataColumn and DataRow objects.
                DataColumn column;
                DataRow row;

                // Create new DataColumn, set DataType, 
                // ColumnName and add to DataTable.    
                column = new DataColumn ();
                column.DataType = System.Type.GetType ("System.Int32");
                column.ColumnName = "id";
                column.ReadOnly = true;
                column.Unique = true;
                // Add the Column to the DataColumnCollection.
                table.Columns.Add (column);

                // Create second column.
                column = new DataColumn ();
                column.DataType = System.Type.GetType ("System.String");
                column.ColumnName = "ParentItem";
                column.AutoIncrement = false;
                column.Caption = "ParentItem";
                column.ReadOnly = false;
                column.Unique = false;
                // Add the column to the table.
                table.Columns.Add (column);

                // Make the ID column the primary key column.
                DataColumn[] PrimaryKeyColumns = new DataColumn[1];
                PrimaryKeyColumns[0] = table.Columns["id"];
                table.PrimaryKey = PrimaryKeyColumns;

                // Instantiate the DataSet variable.
                dataSet = new DataSet ();
                // Add the new DataTable to the DataSet.
                dataSet.Tables.Add (table);

                // Create three new DataRow objects and add 
                // them to the DataTable
                for (int i = 0; i <= 2; i++)
                {
                    row = table.NewRow ();
                    row["id"] = i;
                    row["ParentItem"] = "ParentItem " + i;
                    table.Rows.Add (row);
                }
            }

            private void MakeChildTable()
            {
                // Create a new DataTable.
                DataTable table = new DataTable ("childTable");
                DataColumn column;
                DataRow row;

                // Create first column and add to the DataTable.
                column = new DataColumn ();
                column.DataType = System.Type.GetType ("System.Int32");
                column.ColumnName = "ChildID";
                column.AutoIncrement = true;
                column.Caption = "ID";
                column.ReadOnly = true;
                column.Unique = true;

                // Add the column to the DataColumnCollection.
                table.Columns.Add (column);

                // Create second column.
                column = new DataColumn ();
                column.DataType = System.Type.GetType ("System.String");
                column.ColumnName = "ChildItem";
                column.AutoIncrement = false;
                column.Caption = "ChildItem";
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add (column);

                // Create third column.
                column = new DataColumn ();
                column.DataType = System.Type.GetType ("System.Int32");
                column.ColumnName = "ParentID";
                column.AutoIncrement = false;
                column.Caption = "ParentID";
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add (column);

                dataSet.Tables.Add (table);

                // Create three sets of DataRow objects, 
                // five rows each, and add to DataTable.
                for (int i = 0; i <= 4; i++)
                {
                    row = table.NewRow ();
                    row["childID"] = i;
                    row["ChildItem"] = "Item " + i;
                    row["ParentID"] = 0;
                    table.Rows.Add (row);
                }
                for (int i = 0; i <= 4; i++)
                {
                    row = table.NewRow ();
                    row["childID"] = i + 5;
                    row["ChildItem"] = "Item " + i;
                    row["ParentID"] = 1;
                    table.Rows.Add (row);
                }
                for (int i = 0; i <= 4; i++)
                {
                    row = table.NewRow ();
                    row["childID"] = i + 10;
                    row["ChildItem"] = "Item " + i;
                    row["ParentID"] = 2;
                    table.Rows.Add (row);
                }
            }

            private void MakeDataRelation()
            {
                // DataRelation requires two DataColumn 
                // (parent and child) and a name.
                DataColumn parentColumn =
                    dataSet.Tables["ParentTable"].Columns["id"];
                DataColumn childColumn =
                    dataSet.Tables["ChildTable"].Columns["ParentID"];
                DataRelation relation = new
                    DataRelation ("parent2Child", parentColumn, childColumn);
                dataSet.Tables["ChildTable"].ParentRelations.Add (relation);
            }

            private void BindToDataGrid()
            {
                // Instruct the DataGrid to bind to the DataSet, with the 
                // ParentTable as the topmost DataTable.
            //    dataGrid1.SetDataBinding (dataSet, "ParentTable");// SetDataBinding not available with WPF DataGrid
                dgAnalysis.DataContext = dataSet.Tables[0];  
            }
            */


        }
    }
}