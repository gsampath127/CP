// ***********************************************************************
// Assembly         : RRD.FSG.RP.Utilities
// Author           : 
// Created          : 10-27-2015
//
// Last Modified By : 
// Last Modified On : 10-31-2015
// ***********************************************************************
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace RP.Utilities
{
    /// <summary>
    /// To Hold the Excel related export methods
    /// </summary>
    public static class CreateExcelFile
    {
        #region HELPER_FUNCTIONS

        /// <summary>
        /// Lists to data table.
        /// </summary>
        /// <typeparam name="T">A generic type</typeparam>
        /// <param name="list">The list.</param>
        /// <returns>DataTable.</returns>
        public static DataTable ListToDataTable<T>(List<T> list)
        {
            DataTable dt = new DataTable();

            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                dt.Columns.Add(new DataColumn(GetColumnDisplayName(info), GetNullableType(info.PropertyType)));
            }
            foreach (T t in list)
            {
                DataRow row = dt.NewRow();
                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    if (!IsNullableType(info.PropertyType))
                        row[GetColumnDisplayName(info)] = info.GetValue(t, null);
                    else
                        row[GetColumnDisplayName(info)] = (info.GetValue(t, null) ?? DBNull.Value);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }
        /// <summary>
        /// Gets the type of the nullable.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>Type.</returns>
        private static Type GetNullableType(Type t)
        {
            Type returnType = t;
            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                returnType = Nullable.GetUnderlyingType(t);
            }
            return returnType;
        }
        /// <summary>
        /// Determines whether [is nullable type] [the specified type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if [is nullable type] [the specified type]; otherwise, <c>false</c>.</returns>
        private static bool IsNullableType(Type type)
        {
            return (type == typeof(string) ||
                    type.IsArray ||
                    (type.IsGenericType &&
                     type.GetGenericTypeDefinition().Equals(typeof(Nullable<>))));
        }

        /// <summary>
        /// Creates the excel document.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="xlsxFilePath">The XLSX file path.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool CreateExcelDocument(DataTable dt, string xlsxFilePath, bool IsBillingReport = false)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            bool result = CreateExcelDocument(ds, xlsxFilePath, IsBillingReport);
            ds.Tables.Remove(dt);
            return result;
        }

        /// <summary>
        /// Gets the display name of the column.
        /// </summary>
        /// <param name="objPropertyInfo">The object property information.</param>
        /// <returns>System.String.</returns>
        public static string GetColumnDisplayName(PropertyInfo objPropertyInfo)
        {
            try
            {
                return objPropertyInfo.GetCustomAttributes(typeof(DisplayNameAttribute), false).Count() > 0 ?
                                               objPropertyInfo.GetCustomAttributes(typeof(DisplayNameAttribute), false).Cast<DisplayNameAttribute>().Single().DisplayName
                                               : objPropertyInfo.Name;
            }
            catch
            {
                return objPropertyInfo.Name;
            }
        }
        #endregion

        //added by harsh
        /// <summary>
        /// Create an Excel file, and write it out to a MemoryStream (rather than directly to a file)
        /// </summary>
        /// <param name="dt">DataTable containing the data to be written to the Excel.</param>
        /// <param name="filename">The filename (without a path) to call the new Excel file.</param>
        /// <param name="Response">HttpResponse of the current page.</param>
        /// <returns>True if it was created succesfully, otherwise false.</returns>
        public static bool CreateExcelDocument(DataTable dt, string filename, System.Web.HttpResponse Response, bool isUrlGeneration = false)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                CreateExcelDocumentAsStream(ds, filename, Response, isUrlGeneration);
                ds.Tables.Remove(dt);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Failed, exception thrown: " + ex.Message);
                return false;
            }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public static bool CreateExcelDocumentAsStream(DataSet ds, string filename, System.Web.HttpResponse Response, bool IsUrlGeneration = false)
        {
            try
            {
                System.IO.MemoryStream stream = new System.IO.MemoryStream();
                using (SpreadsheetDocument document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook, true))
                {
                    WriteExcelFile(ds, document, false, IsUrlGeneration);
                    //changes


                }

                stream.Flush();
                stream.Position = 0;

                Response.ClearContent();
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";

                //  NOTE: If you get an "HttpCacheability does not exist" error on the following line, make sure you have
                //  manually added System.Web to this project's References.

                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                byte[] data1 = new byte[stream.Length];
                stream.Read(data1, 0, data1.Length);
                stream.Close();
                Response.BinaryWrite(data1);
                Response.Flush();
                Response.End();

                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Failed, exception thrown: " + ex.Message);
                return false;
            }
        }


        /// <summary>
        /// Create an Excel file, and write it out to a MemoryStream (rather than directly to a file)
        /// </summary>
        /// <param name="dt">DataTable containing the data to be written to the Excel.</param>
        /// <returns>True if it was created successfully, otherwise false.</returns>
        public static bool CreateExcelDocument(DataTable dt, bool isBillingReport = false)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                CreateExcelDocumentAsStream(ds, isBillingReport);

                ds.Tables.Remove(dt);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Failed, exception thrown: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Creates the excel document.
        /// </summary>
        /// <typeparam name="T">A generic type</typeparam>
        /// <param name="list">The list.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] CreateExcelDocument<T>(List<T> list)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(ListToDataTable(list));
                return CreateExcelDocumentAsStream(ds);

            }
            catch (Exception ex)
            {
                Trace.WriteLine("Failed, exception thrown: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Create an Excel file, and write it out to a MemoryStream (rather than directly to a file)
        /// </summary>
        /// <param name="ds">DataSet containing the data to be written to the Excel.</param>
        /// <returns>Either a MemoryStream, or NULL if something goes wrong.</returns>
        public static byte[] CreateExcelDocumentAsStream(DataSet ds, bool isBillingReport = false)
        {
            try
            {
                System.IO.MemoryStream stream = new System.IO.MemoryStream();
                using (SpreadsheetDocument document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook, true))
                {

                    WriteExcelFile(ds, document, isBillingReport);

                    stream.Flush();
                    stream.Position = 0;


                    byte[] data = new byte[stream.Length];
                    stream.Read(data, 0, data.Length);

                    return data;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Failed, exception thrown: " + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// Creates the data set.
        /// </summary>
        /// <typeparam name="T">A generic Type</typeparam>
        /// <param name="list">The list.</param>
        /// <returns>DataSet.</returns>
        public static DataSet CreateDataSet<T>(List<T> list)
        {
            //list is nothing or has nothing, return nothing (or add exception handling)
            if (list == null || list.Count == 0) { return null; }

            //get the type of the first obj in the list
            var obj = list[0].GetType();

            //now grab all properties
            var properties = obj.GetProperties();

            //make sure the obj has properties, return nothing (or add exception handling)
            if (properties.Length == 0) { return null; }

            //it does so create the dataset and table
            var dataSet = new DataSet();
            var dataTable = new DataTable();

            //now build the columns from the properties
            var columns = new DataColumn[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                columns[i] = new DataColumn(properties[i].Name, properties[i].PropertyType);
            }

            //add columns to table
            dataTable.Columns.AddRange(columns);

            //now add the list values to the table
            foreach (var item in list)
            {
                //create a new row from table
                var dataRow = dataTable.NewRow();

                //now we have to iterate thru each property of the item and retrieve it's value for the corresponding row's cell
                var itemProperties = item.GetType().GetProperties();

                for (int i = 0; i < itemProperties.Length; i++)
                {
                    dataRow[i] = itemProperties[i].GetValue(item, null);
                }

                //now add the populated row to the table
                dataTable.Rows.Add(dataRow);
            }

            //add table to dataset
            dataSet.Tables.Add(dataTable);

            //return dataset
            return dataSet;
        }

        /// <summary>
        /// Create an Excel file, and write it to a file.
        /// </summary>
        /// <param name="ds">DataSet containing the data to be written to the Excel.</param>
        /// <param name="excelFilename">Name of file to be written.</param>
        /// <returns>True if successful, false if something went wrong.</returns>
        public static bool CreateExcelDocument(DataSet ds, string excelFilename, bool isBillingReport = false)
        {
            try
            {
                using (SpreadsheetDocument document = SpreadsheetDocument.Create(excelFilename, SpreadsheetDocumentType.Workbook))
                {

                    WriteExcelFile(ds, document, isBillingReport);
                }
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Failed, exception thrown: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Writes the excel file.
        /// </summary>
        /// <param name="ds">The ds.</param>
        /// <param name="spreadsheet">The spreadsheet.</param>
        private static void WriteExcelFile(DataSet ds, SpreadsheetDocument spreadsheet, bool IsBillingReport = false, bool isUrlGeneration = false)
        {
            //  Create the Excel file contents.  This function is used when creating an Excel file either writing 
            //  to a file, or writing to a MemoryStream.
            spreadsheet.AddWorkbookPart();
            spreadsheet.WorkbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();

            // Following line of code (which prevents crashes in Excel 2010)
            spreadsheet.WorkbookPart.Workbook.Append(new BookViews(new WorkbookView()));

            //  If we don't add a "WorkbookStylesPart", OLEDB will refuse to connect to this .xlsx file !
            WorkbookStylesPart workbookStylesPart = spreadsheet.WorkbookPart.AddNewPart<WorkbookStylesPart>("rIdStyles");

            Stylesheet stylesheet = new Stylesheet();
            if (IsBillingReport)
            {
                workbookStylesPart.Stylesheet = CreateSheet();
            }
            else if (isUrlGeneration)
            {
                workbookStylesPart.Stylesheet = CreateSheet();
            }
            else
            {
                workbookStylesPart.Stylesheet = stylesheet;
            }

            //  Loop through each of the DataTables in our DataSet, and create a new Excel Worksheet for each.
            uint worksheetNumber = 1;
            foreach (DataTable dt in ds.Tables)
            {
                //  For each worksheet you want to create               
                WorksheetPart newWorksheetPart = spreadsheet.WorkbookPart.AddNewPart<WorksheetPart>();
                newWorksheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet();


                // create sheet data
                newWorksheetPart.Worksheet.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.SheetData());

                // save worksheet
                // newWorksheetPart.Worksheet.SheetFormatProperties.BaseColumnWidth = 100;
                WriteDataTableToExcelWorksheet(dt, newWorksheetPart, IsBillingReport, isUrlGeneration);

                newWorksheetPart.Worksheet.Save();

                // create the worksheet to workbook relation
                if (worksheetNumber == 1)
                    spreadsheet.WorkbookPart.Workbook.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Sheets());

                spreadsheet.WorkbookPart.Workbook.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Sheets>().AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Sheet()
                {
                    Id = spreadsheet.WorkbookPart.GetIdOfPart(newWorksheetPart),
                    SheetId = (uint)worksheetNumber,
                    Name = dt.TableName
                });

                worksheetNumber++;
            }
            spreadsheet.WorkbookPart.Workbook.Save();
            workbookStylesPart.Stylesheet.Save();
        }




        /// <summary>
        /// Writes the data table to excel worksheet.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="worksheetPart">The worksheet part.</param>
        private static void WriteDataTableToExcelWorksheet(DataTable dt, WorksheetPart worksheetPart, bool IsBillingReport = false, bool isUrlGeneration = false)
        {
            var worksheet = worksheetPart.Worksheet;
            var sheetData = worksheet.GetFirstChild<SheetData>();

            //change for width adjustment

            if (isUrlGeneration )
            {
                
                Columns columns1 = new Columns();
                Column column1 = new Column() { Min = (UInt32Value)1U, Max = (UInt32Value)1U, Width = 9.7109375D, BestFit = true, CustomWidth = true };
                Column column2 = new Column() { Min = (UInt32Value)2U, Max = (UInt32Value)2U, Width = 68.140625D, BestFit = true, CustomWidth = true };
                Column column3 = new Column() { Min = (UInt32Value)3U, Max = (UInt32Value)3U, Width = 13D, BestFit = true, CustomWidth = true };
                Column column4 = new Column() { Min = (UInt32Value)4U, Max = (UInt32Value)4U, Width = 34D, BestFit = true, CustomWidth = true };
                Column column5 = new Column() { Min = (UInt32Value)5U, Max = (UInt32Value)5U, Width = 106D, BestFit = true, CustomWidth = true };
                Column column6 = new Column() { Min = (UInt32Value)6U, Max = (UInt32Value)6U, Width = 116D, BestFit = true, CustomWidth = true };
                Column column7 = new Column() { Min = (UInt32Value)7U, Max = (UInt32Value)7U, Width = 106D, BestFit = true, CustomWidth = true };
                Column column8 = new Column() { Min = (UInt32Value)8U, Max = (UInt32Value)8U, Width = 116D, BestFit = true, CustomWidth = true };

                columns1.Append(column1);
                columns1.Append(column2);
                columns1.Append(column3);
                columns1.Append(column4);
                columns1.Append(column5);
                columns1.Append(column6);
                columns1.Append(column7);
                columns1.Append(column8);

                worksheet.InsertBefore(columns1, sheetData);
            }
            else if (IsBillingReport)
            {
                Columns columns1 = new Columns();
                Column column1 = new Column() { Min = (UInt32Value)1U, Max = (UInt32Value)1U, Width = 35D, BestFit = true, CustomWidth = true };
                Column column2 = new Column() { Min = (UInt32Value)2U, Max = (UInt32Value)2U, Width = 36D, BestFit = true, CustomWidth = true };
                Column column3 = new Column() { Min = (UInt32Value)3U, Max = (UInt32Value)3U, Width = 50D, BestFit = true, CustomWidth = true };
                Column column4 = new Column() { Min = (UInt32Value)4U, Max = (UInt32Value)4U, Width = 20D, BestFit = true, CustomWidth = true };
                Column column5 = new Column() { Min = (UInt32Value)5U, Max = (UInt32Value)5U, Width = 20D, BestFit = true, CustomWidth = true };

                columns1.Append(column1);
                columns1.Append(column2);
                columns1.Append(column3);
                columns1.Append(column4);
                columns1.Append(column5);
                worksheet.InsertBefore(columns1, sheetData);
   
            
            }
                //chnge
            string cellValue = "";
            //changes



            //  Create a Header Row in our Excel file, containing one header for each Column of data in our DataTable.   
            //  We'll also create an array, showing which type each column of data is (Text or Numeric), so when we come to write the actual
            //  cells of data, we'll know if to write Text values or Numeric cell values.
            int numberOfColumns = dt.Columns.Count;
            bool[] IsNumericColumn = new bool[numberOfColumns];

            string[] excelColumnNames = new string[numberOfColumns];
            for (int n = 0; n < numberOfColumns; n++)
                excelColumnNames[n] = GetExcelColumnName(n);

            //
            //  Create the Header row in our Excel Worksheet
            //
            uint rowIndex = 1;

            var headerRow = new Row { RowIndex = rowIndex };  // add a row at the top of spreadsheet
            sheetData.Append(headerRow);




            for (int colInx = 0; colInx < numberOfColumns; colInx++)
            {
                DataColumn col = dt.Columns[colInx];
                AppendTextCell(excelColumnNames[colInx] + "1", col.ColumnName.StartsWith("#") ? string.Empty : col.ColumnName, headerRow, IsBillingReport, isUrlGeneration);
                IsNumericColumn[colInx] = (col.DataType.FullName == "System.Decimal") || (col.DataType.FullName == "System.Int32");
            }

            //
            //  Now, step through each row of data in our DataTable...
            //
            double cellNumericValue = 0;
            foreach (DataRow dr in dt.Rows)
            {
                // ...create a new row, and append a set of this row's data to it.
                ++rowIndex;
                var newExcelRow = new Row { RowIndex = rowIndex };  // add a row at the top of spreadsheet

                sheetData.Append(newExcelRow);

                for (int colInx = 0; colInx < numberOfColumns; colInx++)
                {
                    cellValue = dr.ItemArray[colInx].ToString();


                    // Create cell with data
                    if (IsNumericColumn[colInx])
                    {
                        //  For numeric cells, make sure our input data IS a number, then write it out to the Excel file.
                        //  If this numeric value is NULL, then don't write anything to the Excel file.
                        cellNumericValue = 0;
                        if (double.TryParse(cellValue, out cellNumericValue))
                        {
                            cellValue = cellNumericValue.ToString();
                            AppendNumericCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow, IsBillingReport, isUrlGeneration);
                        }
                    }
                    else
                    {
                        //  For text cells, just write the input data straight out to the Excel file.
                        AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow, IsBillingReport, isUrlGeneration);
                    }
                }
            }
        }

        /// <summary>
        /// Appends the text cell.
        /// </summary>
        /// <param name="cellReference">The cell reference.</param>
        /// <param name="cellStringValue">The cell string value.</param>
        /// <param name="excelRow">The excel row.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        private static void AppendTextCell(string cellReference, string cellStringValue, Row excelRow, bool IsBillingReport = false, bool isUrlGeneration = false)
        {
            //  Add a new Excel Cell to our Row 
            Cell cell = new Cell() { CellReference = cellReference, DataType = CellValues.String };
            CellValue cellValue = new CellValue();
            cellValue.Text = cellStringValue;
            cell.Append(cellValue);

            if (IsBillingReport)
            {
                if (cellReference == "A2" || cellReference == "A3" || cellReference == "A4" || cellReference == "A5" || cellReference == "A6" || cellReference == "A7")
                {

                    cell.StyleIndex = 1;
                }
                else if (cellReference == "B2" || cellReference == "B3" || cellReference == "B4" || cellReference == "B5" || cellReference == "B6" || cellReference == "B7")
                {
                    cell.StyleIndex = 2;
                }
                else if (cellReference == "A9" || cellReference == "B9" || cellReference == "C9" || cellReference == "D9" || cellReference == "E9")
                {
                    cell.StyleIndex = 3;
                }
                else if (cellValue.Text == "Removed CUSIPs" || cellValue.Text == "Date")
                {
                    cell.StyleIndex = 4;
                }
                else if (!(string.IsNullOrEmpty(cellValue.Text)))
                {
                    cell.StyleIndex = 5;
                }
            }
            if (isUrlGeneration)
            {
                if (cellReference == "A1" || cellReference == "B1" || cellReference == "C1" || cellReference == "D1" || cellReference == "E1" || cellReference == "F1" ||
                    cellReference == "G1" || cellReference == "H1")
                {
                    cell.StyleIndex = 2;
                }
                else if (!(string.IsNullOrEmpty(cellValue.Text)))
                {
                    cell.StyleIndex = 5;
                }
            }
            excelRow.Append(cell);

        }

        /// <summary>
        /// Appends the numeric cell.
        /// </summary>
        /// <param name="cellReference">The cell reference.</param>
        /// <param name="cellStringValue">The cell string value.</param>
        /// <param name="excelRow">The excel row.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        private static void AppendNumericCell(string cellReference, string cellStringValue, Row excelRow, bool IsBillingReport = false, bool isUrlGeneration = false)
        {
            //  Add a new Excel Cell to our Row 
            Cell cell = new Cell() { CellReference = cellReference };
            CellValue cellValue = new CellValue();
            cellValue.Text = cellStringValue;
            cell.Append(cellValue);
            if (IsBillingReport)
            {
                if (cellReference == "A2" || cellReference == "A3" || cellReference == "A4" || cellReference == "A5" || cellReference == "A6" || cellReference == "A7")
                {
                    cell.StyleIndex = 1;
                }
                else if (cellReference == "B2" || cellReference == "B3" || cellReference == "B4" || cellReference == "B5" || cellReference == "B6" || cellReference == "B7")
                {
                    cell.StyleIndex = 2;
                }
                else if (cellReference == "A9" || cellReference == "B9" || cellReference == "C9" || cellReference == "D9" || cellReference == "E9")
                {
                    cell.StyleIndex = 3;
                }
                else if (cellValue.Text == "Removed CUSIPs" || cellValue.Text == "Date")
                {
                    cell.StyleIndex = 4;
                }
                else if (!(string.IsNullOrEmpty(cellValue.Text)))
                {
                    cell.StyleIndex = 5;
                }
            }
            if (isUrlGeneration)
            {
                if (cellReference == "A1" || cellReference == "B1" || cellReference == "C1" || cellReference == "D1" || cellReference == "E1" || cellReference == "F1")
                {
                    cell.StyleIndex = 3;
                }
               
                else if (!(string.IsNullOrEmpty(cellValue.Text)))
                {
                    cell.StyleIndex = 5;
                }
            }
            excelRow.Append(cell);
        }

        /// <summary>
        /// Gets the name of the excel column.
        /// </summary>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns>System.String.</returns>
        private static string GetExcelColumnName(int columnIndex)
        {
            if (columnIndex < 26)
                return ((char)('A' + columnIndex)).ToString();

            char firstChar = (char)('A' + (columnIndex / 26) - 1);
            char secondChar = (char)('A' + (columnIndex % 26));

            return string.Format("{0}{1}", firstChar, secondChar);
        }

        private static Stylesheet CreateSheet()
        {

            return new Stylesheet(

                 new Fonts(
            new Font(                                                               // Index 0 – The default font.
                new FontSize() { Val = 11 },
                new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                new FontName() { Val = "Calibri" }),
            new Font(                                                               // Index 1 – The bold font.
                new Bold(),
                new FontSize() { Val = 11 },
                new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                new FontName() { Val = "Calibri" })

        ),

        new Fills(
            new Fill(                                                           // Index 0 – The default fill.
                new PatternFill() { PatternType = PatternValues.None }),
            new Fill(                                                           // Index 1 – The default fill of gray 125 (required)
                new PatternFill() { PatternType = PatternValues.Gray125 }),
            new Fill(                                                           // Index 2 – The blue fill.
                new PatternFill(
                    new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "CCFFFF" } }
                ) { PatternType = PatternValues.Solid }),
            new Fill(                                                           // Index 3 – The pale yellow fill.
            new PatternFill(
                   new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "FFFFCC" } }
                ) { PatternType = PatternValues.Solid }),
            new Fill(                                                           // Index 4 – The Light pink fill.
            new PatternFill(
                   new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "FFCCFF" } }
                ) { PatternType = PatternValues.Solid })
        ),
        new Borders(
            new Border(                                                         // Index 0 – The default border.
                new LeftBorder(),
                new RightBorder(),
                new TopBorder(),
                new BottomBorder(),
                new DiagonalBorder()),

            new Border(                                                         // Index 1 – Applies a Left, Right, Top, Bottom border to a cell
                new LeftBorder(
                    new Color() { Auto = true }
                ) { Style = BorderStyleValues.Thin },
                new RightBorder(
                    new Color() { Auto = true }
                ) { Style = BorderStyleValues.Thin },
                new TopBorder(
                    new Color() { Auto = true }
                ) { Style = BorderStyleValues.Thin },
                new BottomBorder(
                    new Color() { Auto = true }
                ) { Style = BorderStyleValues.Thin },
                new DiagonalBorder())
        ),

        new CellFormats(
            new CellFormat() { FontId = 0, FillId = 0, BorderId = 0 },                          // Index 0 – The default cell style.  If a cell does not have a style index applied it will use this style combination instead
            new CellFormat() { FontId = 1, FillId = 2, BorderId = 1, ApplyBorder = true, ApplyFont = true },       // Index 1 – Bold Colored top Bordered
            new CellFormat() { FontId = 1, FillId = 0, BorderId = 1, ApplyFont = true },       // Index 2 – Bold Bordered
            new CellFormat() { FontId = 1, FillId = 3, BorderId = 1, ApplyFont = true },       // Index 3 – Bold Colored 2
            new CellFormat() { FontId = 1, FillId = 4, BorderId = 1, ApplyFill = true },       // Index 4 – Bold Colored 3
            new CellFormat() { FontId = 0, FillId = 0, BorderId = 1 }                 //Index 5-Normal text bordered
            )
    );
        }

    }
}
