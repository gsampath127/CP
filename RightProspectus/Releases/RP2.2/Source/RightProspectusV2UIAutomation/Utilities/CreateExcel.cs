using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class CreateExcel
    {
        public static string ExportExcel(List<TestResult> results, string sourcePath, string filename)
        {
            string finalTestResultPath = string.Empty, finalTestCasePath = string.Empty;
            using (SLDocument sl = new SLDocument())
            {
                try
                {
                    sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Test Results");
                    sl.SetCellValue("A1", "URL");
                    sl.SetCellValue("B1", "Browser");
                    sl.SetCellValue("C1", "Error Info");
                    sl.SetCellValue("D1", "Page");
                    sl.SetCellValue("E1", "Test Case");
                    sl.SetCellValue("F1", "Type");

                    int row = 2, x = 0;
                    foreach (var result in results)
                    {
                        sl.SetCellValue("A" + (row + x).ToString(), result.Url);
                        sl.SetCellValue("B" + (row + x).ToString(), result.Browser);
                        sl.SetCellValue("C" + (row + x).ToString(), result.Description);
                        sl.SetCellValue("D" + (row + x).ToString(), result.Page);
                        sl.SetCellValue("E" + (row + x).ToString(), result.TestCase);
                        sl.SetCellValue("F" + (row + x).ToString(), result.Type.ToString());
                        x++;
                    }
                    sl.AutoFitColumn(1, 2);
                    sl.SetColumnWidth(3, 100);
                    sl.AutoFitColumn(4, 6);

                    finalTestCasePath = Path.Combine(sourcePath, "Results", filename.Substring(0, filename.IndexOf('.')) + "_TestCase_" + DateTime.Now.ToString("MMddyyyyhhmmss") + ".xlsx");
                    finalTestResultPath = Path.Combine(sourcePath, "Results", filename.Substring(0, filename.IndexOf('.')) + "_Results_" + DateTime.Now.ToString("MMddyyyyhhmmss") + ".xlsx");
                    
                    //Save Excel
                    sl.SaveAs(finalTestResultPath);
                    File.Move(Path.Combine(sourcePath, filename), finalTestCasePath);
                }
                catch(Exception ex)
                {
                    Log.WriteLog("Error in ExportExel: " + ex.Message);
                }
                return finalTestResultPath;
            }
        }
    }
}
