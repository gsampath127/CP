using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public  class FileData
    {
        public string Url { get; set; }
        public string Page { get; set; }

        public static  FileData ReadExcel(IList<string> rowData, IList<string> columnNames)
        {
            FileData file = new FileData();
            {
                file.Url = (rowData[columnNames.IndexOf("url")]).ToString().Trim();
                file.Page = (rowData[columnNames.IndexOf("page")]).ToString().Trim();
            }
            return file;
        }
       
    }

    public static class FileDataCollection
    {
        public static IList<FileData> GetData(string fileName, string sheet)
        {
            return ExcelReader.GetDataToList(fileName, FileData.ReadExcel, sheet);
        }
    }

    public class ConfigData
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public static ConfigData ReadExcel(IList<string> rowData, IList<string> columnNames)
        {
            ConfigData file = new ConfigData();
            {
                file.Key = (rowData[columnNames.IndexOf("key")]).ToString().Trim();
                file.Value = (rowData[columnNames.IndexOf("value")]).ToString().Trim();
            }
            return file;
        }
    }

    public static class ConfigDataCollection
    {
        public static IList<ConfigData> GetData(string fileName, string sheet)
        {
            return ExcelReader.GetDataToList(fileName, ConfigData.ReadExcel, sheet);
        }

        public static string GetValue(IList<ConfigData> configData, string key)
        {
            try
            {
                foreach (var item in configData)
                {
                    if (item.Key == key)
                        return item.Value;
                }
            }catch
            { }
            return string.Empty;
        }
    }
}
