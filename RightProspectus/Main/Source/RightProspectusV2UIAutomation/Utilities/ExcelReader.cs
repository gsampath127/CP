using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Utilities
{
    public static class ExcelReader
    {
        //Read Excel data to generic list - overloaded version 1.
        public static IList<T> GetDataToList<T>(string filePath, Func<IList<string>, IList<string>, T> imports, string sheetName = null)
        {
            if (imports != null)
                return GetDataToList<T>(filePath, sheetName, imports);
            else
                return null;
        }

        public async static Task<IList<T>> GetDataToListAsync<T>(string filePath, Func<IList<string>, IList<string>, T> imports)
        {
            return await GetDataToListAsync<T>(filePath, "", imports);
        }

        //Read Excel data to generic list - overloaded version 2.
        public static IList<T> GetDataToList<T>(string filePath, string sheetName, Func<IList<string>, IList<string>, T> imports)
        {
            List<T> resultList = new List<T>();
            // Stream stream = File.Open(filePath, FileMode.Open);

            // Open the spreadsheet document for read-only access.
            //using (SpreadsheetDocument document = SpreadsheetDocument.Open(@"D:\Test\Test.xlsx", false))
            //using (SpreadsheetDocument document = SpreadsheetDocument.Open(ConfigurationManager.AppSettings["readFilePath"].ToString(), false))
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(filePath, false))
            {
                WorkbookPart wbPart = document.WorkbookPart;
                Sheet sheet = null;
                WorksheetPart wsPart = null;

                // Find the worksheet.                
                if (sheetName == "")
                {
                    sheet = wbPart.Workbook.Descendants<Sheet>().FirstOrDefault();
                }
                else
                {
                    sheet = wbPart.Workbook.Descendants<Sheet>().Where(s => s.Name == sheetName).FirstOrDefault();
                }
                if (sheet != null)
                {
                    // Retrieve a reference to the worksheet part.
                    wsPart = (WorksheetPart)(wbPart.GetPartById(sheet.Id));
                }
                if (wsPart == null)
                {
                    throw new Exception("No worksheet.");
                }
                else
                {
                    //List to hold custom column names for mapping data to columns (index-free).
                    var columnNames = new List<string>();

                    //List to hold column address letters for handling empty cell issue (handle empty cell issue).
                    var columnLetters = new List<string>();

                    //Iterate cells of custom header row.
                    foreach (Cell cell in wsPart.Worksheet.Descendants<Row>().ElementAt(0))
                    {
                        //Get custom column names.
                        //Remove spaces, symbols (except underscore), and make lower cases and for all values in columnNames list.                    
                        columnNames.Add(Regex.Replace(GetCellValue(document, cell), @"[^A-Za-z0-9_]", "").ToLower());

                        //Get built-in column names by extracting letters from cell references.
                        columnLetters.Add(GetColumnAddress(cell.CellReference));
                    }

                    //Used for sheet row data to be added through delegation.                
                    var rowData = new List<string>();

                    //Do data in rows
                    string cellLetter = string.Empty;

                    foreach (var row in GetUsedRows(document, wsPart))
                    {
                        rowData.Clear();

                        //Iterate through prepared enumerable.
                        //Parallel.ForEach(GetCellsForRow(row, columnLetters), row =>
                        //{
                        //    rowData.Add(GetCellValue(document, cell));
                        //});
                        foreach (var cell in GetCellsForRow(row, columnLetters))
                        {
                            rowData.Add(GetCellValue(document, cell));
                        }

                        //Calls the delegated function to add it to the collection.
                        resultList.Add(imports(rowData, columnNames));
                    }
                }
            }
            return resultList;
        }


        public async static Task<IList<T>> GetDataToListAsync<T>(string filePath, string sheetName, Func<IList<string>, IList<string>, T> imports)
        {
            List<T> resultList = new List<T>();

            // Open the spreadsheet document for read-only access.
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(filePath, false))
            {
                WorkbookPart wbPart = document.WorkbookPart;
                Sheet sheet = null;
                WorksheetPart wsPart = null;

                // Find the worksheet.                
                if (sheetName == "")
                {
                    sheet = wbPart.Workbook.Descendants<Sheet>().FirstOrDefault();
                }
                else
                {
                    sheet = wbPart.Workbook.Descendants<Sheet>().Where(s => s.Name == sheetName).FirstOrDefault();
                }
                if (sheet != null)
                {
                    // Retrieve a reference to the worksheet part.
                    wsPart = (WorksheetPart)(wbPart.GetPartById(sheet.Id));
                }
                if (wsPart == null)
                {
                    throw new Exception("No worksheet.");
                }
                else
                {
                    //List to hold custom column names for mapping data to columns (index-free).
                    var columnNames = new List<string>();

                    //List to hold column address letters for handling empty cell issue (handle empty cell issue).
                    var columnLetters = new List<string>();

                    //Iterate cells of custom header row.
                    foreach (Cell cell in wsPart.Worksheet.Descendants<Row>().ElementAt(0))
                    {
                        //Get custom column names.
                        //Remove spaces, symbols (except underscore), and make lower cases and for all values in columnNames list.                    
                        columnNames.Add(Regex.Replace(GetCellValue(document, cell), @"[^A-Za-z0-9_]", "").ToLower());

                        //Get built-in column names by extracting letters from cell references.
                        columnLetters.Add(GetColumnAddress(cell.CellReference));
                    }

                    //Used for sheet row data to be added through delegation.                
                    var rowData = new List<string>();

                    //Do data in rows
                    string cellLetter = string.Empty;

                    return await GetCellValuetoCollectionAsync<T>(imports, document, wsPart, columnNames, columnLetters, rowData);

                }
            }
        }

        private async static Task<IList<T>> GetCellValuetoCollectionAsync<T>(Func<IList<string>, IList<string>, T> imports, SpreadsheetDocument document, WorksheetPart wsPart, List<string> columnNames, List<string> columnLetters, List<string> rowData)
        {
            List<T> resultList = new List<T>();

            foreach (var row in GetUsedRows(document, wsPart))
            {
                rowData.Clear();

                foreach (var cell in GetCellsForRow(row, columnLetters))
                {
                    rowData.Add(GetCellValue(document, cell));
                }

                //Calls the delegated function to add it to the collection.
                resultList.Add(imports(rowData, columnNames));
            }
            return resultList;
        }

        private static string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            if (cell == null) return null;
            string value = cell.InnerText;

            //Process values particularly for those data types.
            if (cell.DataType != null)
            {
                switch (cell.DataType.Value)
                {
                    //Obtain values from shared string table.
                    case CellValues.SharedString:
                        var sstPart = document.WorkbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                        value = sstPart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
                        break;

                    //Optional boolean conversion.
                    case CellValues.Boolean:
                        //  var booleanToBit = ConfigurationManager.AppSettings["BooleanToBit"];
                        var booleanToBit = "N";
                        if (booleanToBit != "Y")
                        {
                            value = value == "0" ? "FALSE" : "TRUE";
                        }
                        break;
                    case CellValues.Error:
                        {
                            value = string.Empty;
                            break;
                        }
                    default:
                        //DateTime oaDate = new DateTime(2009, 12, 11);
                        //double oaValue = oaDate.ToOADate();
                        //Console.WriteLine("Serial value of date 2009-12-11: " + oaValue);

                        value = DateTime.FromOADate(Convert.ToDouble(value)).ToShortDateString();
                        //Console.WriteLine("DateTime of serial date value (40158): " + oaDate);
                        break;
                }
            }
            return value;
        }

        private static IEnumerable<Row> GetUsedRows(SpreadsheetDocument document, WorksheetPart wsPart)
        {
            bool hasValue;
            //Iterate all rows except the first one.
            foreach (var row in wsPart.Worksheet.Descendants<Row>().Skip(1))
            {
                hasValue = false;
                foreach (var cell in row.Descendants<Cell>())
                {
                    //Find at least one cell with value for a row
                    if (!string.IsNullOrEmpty(GetCellValue(document, cell)))
                    {
                        hasValue = true;
                        break;
                    }
                }
                if (hasValue)
                {
                    //Return the row and keep iteration state.
                    yield return row;
                }
            }
        }

        private static IEnumerable<Cell> GetCellsForRow(Row row, List<string> columnLetters)
        {
            int workIdx = 0;
            foreach (var cell in row.Descendants<Cell>())
            {
                //Get letter part of cell address.
                var cellLetter = GetColumnAddress(cell.CellReference);

                //Get column index of the matched cell.  
                int currentActualIdx = columnLetters.IndexOf(cellLetter);

                //Add empty cell if work index smaller than actual index.
                for (; workIdx < currentActualIdx; workIdx++)
                {
                    var emptyCell = new Cell() { DataType = null, CellValue = new CellValue(string.Empty) };
                    yield return emptyCell;
                }

                //Return cell with data from Excel row.
                yield return cell;
                workIdx++;

                //Check if it's ending cell but there still is any unmatched columnLetters item.   
                if (cell == row.LastChild)
                {
                    //Append empty cells to enumerable. 
                    for (; workIdx < columnLetters.Count(); workIdx++)
                    {
                        var emptyCell = new Cell() { DataType = null, CellValue = new CellValue(string.Empty) };
                        yield return emptyCell;
                    }
                }
            }
        }

        private static string GetColumnAddress(string cellReference)
        {
            //Create a regular expression to get column address letters.
            Regex regex = new Regex("[A-Za-z]+");
            Match match = regex.Match(cellReference);
            return match.Value;
        }

        public static string CheckPath(string filePath)
        {
            var checkedPath = filePath;

            if (!(checkedPath.StartsWith(@"\\") && checkedPath.Contains(@":\")))
            {
                //Search app or processing folder 
                checkedPath = GetSearchedPath(filePath);
                // checkedPath = GetSearchedPath("~/Files" + filePath);
                if (string.IsNullOrEmpty(checkedPath))
                {
                    //Search dev project bin folder 
                    checkedPath = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.LastIndexOf(@"\bin\") + 5) + filePath;
                    // checkedPath = "~/Files" + filePath;
                    checkedPath = GetSearchedPath(checkedPath);
                    if (string.IsNullOrEmpty(checkedPath))
                    {
                        //Search project folder
                        checkedPath = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf(@"\bin\") + 1) + filePath;
                        checkedPath = GetSearchedPath(checkedPath);
                    }
                }
            }
            else
            {
                //Search absolute path
                checkedPath = GetSearchedPath(checkedPath);
            }

            if (!string.IsNullOrEmpty(checkedPath))
            {
                return checkedPath;
            }
            else
            {
                throw new FileNotFoundException("Source file not found.");
            }
        }

        private static string GetSearchedPath(string searchPath)
        {
            var pos = searchPath.LastIndexOf(@"\") + 1;
            var pod = searchPath.LastIndexOf(@".");
            var srcfolder = searchPath.Substring(0, pos);
            var filePureName = searchPath.Substring(pos, pod - pos);
            var fileDotExt = searchPath.Substring(pod);

            //Get latest updated source file with searched partial name in the folder
            DirectoryInfo dir = new DirectoryInfo(srcfolder);
            if (!dir.Exists) return null;
            var srcFirstFile = dir.GetFiles().Where(fi => fi.Name.ToLower().Contains(filePureName.ToLower())
                      && fi.Name.ToLower().Contains(fileDotExt)).OrderByDescending(fi => fi.LastWriteTime).FirstOrDefault();
            if (srcFirstFile != null && srcFirstFile.Exists)
            {
                return srcFirstFile.FullName;
            }
            return null;
        }

        //List extension for case-insensitive match
        public static int IndexFor(this IList<string> list, string name)
        {
            int idx = list.IndexOf(name.ToLower());
            if (idx < 0)
            {
                throw new Exception(string.Format("Missing required column mapped to: {0}.", name));
            }
            return idx;
        }

        #region String Converters
        public static int ToInt32(this string source)
        {
            int outNum;
            return int.TryParse(source, out outNum) ? outNum : 0;
        }
        public static int? ToInt32Nullable(this string source)
        {
            int outNum;
            return int.TryParse(source, out outNum) ? outNum : (int?)null;
        }
        public static decimal ToDecimal(this string source)
        {
            decimal outNum;
            return decimal.TryParse(source, out outNum) ? outNum : 0;
        }

        public static decimal? ToDecimalNullable(this string source)
        {
            decimal outNum;
            return decimal.TryParse(source, out outNum) ? outNum : (decimal?)null;
        }

        public static double ToDouble(this string source)
        {
            double outNum;
            return double.TryParse(source, out outNum) ? outNum : 0;
        }

        public static double? ToDoubleNullable(this string source)
        {
            double outNum;
            return double.TryParse(source, out outNum) ? outNum : (double?)null;
        }

        public static DateTime ToDateTime(this string source)
        {
            DateTime outDt;
            if (DateTime.TryParse(source, out outDt))
            {
                return outDt;
            }
            else
            {
                //Check OLE Automation date time
                if (IsNumeric(source))
                {
                    return DateTime.FromOADate(source.ToDouble());
                }
                return DateTime.Now;
            }
        }

        public static DateTime? ToDateTimeNullable(this string source)
        {
            DateTime outDt;
            if (DateTime.TryParse(source, out outDt))
            {
                return outDt;
            }
            else
            {
                //Check and handle OLE Automation date time
                if (IsNumeric(source))
                {
                    return DateTime.FromOADate(source.ToDouble());
                }
                return (DateTime?)null;
            }
        }

        public static bool ToBoolean(this string source)
        {
            if (!string.IsNullOrEmpty(source))
                if (source.ToLower() == "true" || source == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            else
            {
                return false;
            }
        }
        public static bool? ToBooleanNullable(this string source)
        {
            if (!string.IsNullOrEmpty(source))
                if (source.ToLower() == "true" || source == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            else
            {
                return (bool?)null;
            }
        }



        public static Guid ToGuid(this string source)
        {
            Guid outGuid;
            return Guid.TryParse(source, out outGuid) ? outGuid : Guid.Empty;
        }

        public static Guid? ToGuidNullable(this string source)
        {
            Guid outGuid;
            return Guid.TryParse(source, out outGuid) ? outGuid : (Guid?)null;
        }
        #endregion

        #region Util
        private static readonly Regex _isNumericRegex = new Regex("^(" +
            /*Hex*/ @"0x[0-9a-f]+" + "|" +
            /*Bin*/ @"0b[01]+" + "|" +
            /*Oct*/ @"0[0-7]*" + "|" +
            /*Dec*/ @"((?!0)|[-+]|(?=0+\.))(\d*\.)?\d+(e\d+)?" +
            ")$");
        static bool IsNumeric(string value)
        {
            return _isNumericRegex.IsMatch(value);
        }
        #endregion

        //public static int IndexFor(this IList<string> list, string name)
        //{
        //    int idx = list.IndexOf(name.ToLower());
        //    if (idx < 0)
        //    {
        //        throw new Exception(string.Format("Missing required column mapped to: {0}.", name));
        //    }
        //    return idx;
        //}
    }
}
