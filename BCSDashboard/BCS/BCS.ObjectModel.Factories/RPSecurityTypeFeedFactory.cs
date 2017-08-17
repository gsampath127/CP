using BCS.Core.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.IO.Compression;
using System.Data.SqlClient;
using ICSharpCode.SharpZipLib.Zip;

namespace BCS.ObjectModel.Factories
{
    public class RPSecurityTypeFeedFactory
    {
        private readonly string DB1029ConnectionString;
        private readonly string ReadOnlyDB1029ConnectionString;
        private readonly IDataAccess dataAccess;
        private int ErrorSleepTime = ConfigValues.ErrorSleepTime;

        private const string SPInsertEdgarOnlineFeedFileHistory = "EdgarOnline_InsertEdgarOnlineFeedFileHistory";
        private const string SPGetUnprocessedEdgarOnlineFeedFile = "EdgarOnline_GetUnprocessedEdgarOnlineFeedFile";
        private const string SPDeleteAllRecordsEdgarOnlineFeed_FTP = "EdgarOnline_DeleteAllRecordsEdgarOnlineFeed_FTP";
        private const string SPSetEdgarOnlineFeedFileProcessed = "EdgarOnline_SetEdgarOnlineFeedFileProcessed";
        private const string SPUpdateEdgarOnlineFeed = "EdgarOnline_UpdateEdgarOnlineFeed";
        private const string SPGetDailyUpdateReport = "BCS_GetDailyUpdateReport";
        private const string DBCFileName = "FileName";
        private const string DBCSelectedDate = "SelectedDate";
        private const string DBCSortDirection = "sortDirection";
        private const string DBCSortColumn = "sortColumn";
        private const string DBCStartIndex = "startIndex";
        private const string DBCEndIndex = "endIndex";
        public RPSecurityTypeFeedFactory()
        {
            this.DB1029ConnectionString = DBConnectionString.DB1029ConnectionString();
            this.ReadOnlyDB1029ConnectionString = DBConnectionString.ReadOnlyDB1029Connection();
            this.dataAccess = new DataAccess();
        }

        public void PickUpStoreAndDeleteEdgarOnlineFeedFile()
        {
            List<string> EdgarOnlineFeedFileNames = EdgarOnlineFeedFileDownload();

            if (EdgarOnlineFeedFileNames.Count > 0)
            {
                string fileNames = string.Empty;

                foreach (string name in EdgarOnlineFeedFileNames)
                {
                    fileNames = fileNames + "<br />" + name;
                }

                string emailBody = "<html><body>";
                emailBody = emailBody + @"<br /><font size=""5"" color=""#666666"" face=""Arial, Helvetica, sans-serif"">" + "EdgarOnlineFeed File FTP Alert</font><br /><br />";
                emailBody = emailBody + "List of Document(s) received on " + DateTime.Now.ToString() + ":<hr />" + fileNames + "<hr/>";
                emailBody = emailBody + "</body></html>";

                UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.ConfirmationEmailListTo, null, null, "EdgarOnlineFeed File Alert", emailBody, "Support", null);
            }
        }

        private List<string> EdgarOnlineFeedFileDownload()
        {
            List<string> edgarOnlineFeedFileList = UtilityFactory.GetFileList(ConfigValues.EdgarOnlineFeedFTPPath,
                                                                    ConfigValues.EdgarOnlineFeedFTPUsername, ConfigValues.EdgarOnlineFeedFTPPassword);

            if (edgarOnlineFeedFileList != null)
            {
                foreach (string fileName in edgarOnlineFeedFileList.FindAll(p=> p.Contains(".zip")))
                {
                    Uri uri = new Uri("ftp://" + ConfigValues.EdgarOnlineFeedFTPPath + "/" + fileName);

                    try
                    {
                        //1. Download File from FTP
                        UtilityFactory.DownloadFileFromFTP(uri, ConfigValues.EdgarOnlineFeedArchiveDropPath + fileName, ConfigValues.EdgarOnlineFeedFTPUsername, ConfigValues.EdgarOnlineFeedFTPPassword, true);

                        //2. Delete File from FTP
                        UtilityFactory.DeleteFTPFileRRDInt(uri, ConfigValues.EdgarOnlineFeedFTPUsername, ConfigValues.EdgarOnlineFeedFTPPassword);

                        //3. Insert EdgarOnlineFeed File History
                        InsertEdgarOnlineFeedFileHistory(fileName);

                    }
                    catch (Exception expt)
                    {
                        string ErrorEmailBody = "Error in RP.SecurityTypeFeedSourceService in function EdgarOnlineFeedFileDownload -  : " + expt.ToString();
                        UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.BCSExceptionEmailListTo, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);

                        System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
                    }

                }
            }

            return edgarOnlineFeedFileList != null ? edgarOnlineFeedFileList.FindAll(p => p.Contains(".zip")) : edgarOnlineFeedFileList;
        }

        private void InsertEdgarOnlineFeedFileHistory(string filename)
        {
            this.dataAccess.ExecuteNonQuery
                     (
                          this.DB1029ConnectionString,
                          SPInsertEdgarOnlineFeedFileHistory,
                          this.dataAccess.CreateParameter(DBCFileName, SqlDbType.NVarChar, filename)
                      );
        }

        public void ProcessEdgarOnlineFeedFile()
        {
            using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.DB1029ConnectionString,
                    SPGetUnprocessedEdgarOnlineFeedFile
                ))
            {

                while (reader.Read())
                {
                    string strFileName = reader["FileName"].ToString();

                    EdgarOnlineFeedBulkInsert(strFileName);

                    SetEdgarOnlineFeedFileProcessed(strFileName);

                    UpdateEdgarOnlineFeed();
                }
            }
        }

        private void EdgarOnlineFeedBulkInsert(string fileName)
        {
            try
            {
                string zipPath = ConfigValues.EdgarOnlineFeedArchiveDropPath + fileName;
                string extractPath = ConfigValues.EdgarOnlineFeedArchiveDropExractFilePath;

                string FeedFilePath = UtilityFactory.ExtractZipFile(zipPath, "", extractPath).SingleOrDefault(p => p.EndsWith(".txt"));

                DataTable Data = new DataTable();
                Data = CreateDataTableFromWatchListFile(FeedFilePath);

                if (Data.Rows.Count > 0)
                {
                    using (SqlConnection cn = new SqlConnection(this.DB1029ConnectionString))
                    {
                        cn.Open();
                        using (SqlBulkCopy copy = new SqlBulkCopy(cn))
                        {
                            copy.DestinationTableName = "EdgarOnlineFeed_FTP";

                            DeleteOld_EdgarOnlineFeed_FTPData();
                            copy.WriteToServer(Data);
                        }
                        cn.Close();
                    }

                    //Delete extracted file after processing
                    File.Delete(FeedFilePath);
                }

            }
            catch (Exception expt)
            {
                string ErrorEmailBody = "Error in RP.SecurityTypeFeedSourceService in function EdgarOnlineFeedBulkInsert -  FileName: " + fileName + " -  Exception message : " + expt.ToString();
                UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.BCSExceptionEmailListTo, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);

                System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
            }
        }

        private void DeleteOld_EdgarOnlineFeed_FTPData()
        {
            try
            {
                this.dataAccess.ExecuteNonQuery
                     (
                          this.DB1029ConnectionString,
                          SPDeleteAllRecordsEdgarOnlineFeed_FTP
                      );

            }
            catch (Exception expt)
            {
                string ErrorEmailBody = "Error in RP.SecurityTypeFeedSourceService in function DeleteOld_EdgarOnlineFeed_FTPData -  : " + expt.ToString();
                UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.BCSExceptionEmailListTo, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);

                System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
            }
        }

        private static DataTable CreateDataTableFromWatchListFile(string strFileName)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ECUSIP", typeof(string));
            dt.Columns.Add("EFundName", typeof(string));
            dt.Columns.Add("Eticker", typeof(string));
            dt.Columns.Add("ECompanyName", typeof(string));
            dt.Columns.Add("ECIK", typeof(string));
            dt.Columns.Add("ESeriesID", typeof(string));
            dt.Columns.Add("EClassContractID", typeof(string));
            dt.Columns.Add("Euniverseabbrev", typeof(string));

            try
            {

                StreamReader sr = new StreamReader(strFileName);
                string input;
                string strErrorMessage = string.Empty;
                string strCUSIPErrors = String.Empty;
                bool isFormatError = false;

                input = sr.ReadLine();
                string previousECUSIP = "";
                while ((input = sr.ReadLine()) != null)
                {
                    string[] s = input.Split(new char[] { '|' });
                    if (s.Length == 17)
                    {
                        string ECUSIP = s[0].ToString();

                        if (previousECUSIP != ECUSIP)
                        {
                            previousECUSIP = ECUSIP;

                            DataRow dr = dt.NewRow();
                            dr["ECUSIP"] = ECUSIP;
                            dr["EFundName"] = s[1].ToString();
                            dr["Eticker"] = s[9].ToString();
                            dr["ECompanyName"] = s[10].ToString();
                            dr["ECIK"] = s[11].ToString();
                            dr["ESeriesID"] = s[12].ToString();
                            dr["EClassContractID"] = s[13].ToString();
                            dr["Euniverseabbrev"] = s[14].ToString();
                            dt.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        isFormatError = true;
                        strErrorMessage = strErrorMessage + "<br/>" + input;
                        break;
                    }
                }
                sr.Close();

                if (isFormatError)
                {
                    string emailString = "<html><head><title></title></head><body>";
                    emailString = emailString + @"<br /><font size=""5"" color=""#666666"" face=""Arial, Helvetica, sans-serif"">" + "Edgar Online file -  " + strFileName + " Rejected</font><br /><br />";
                    emailString = emailString + "Format error in file :<hr />" + Path.GetFileName(strFileName) + "<br>" + strErrorMessage + "<hr/>";
                    emailString = emailString + "</body></html>";

                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.BCSExceptionEmailListTo, null, null, ConfigValues.BCSExceptionEmailSub, emailString, "Support", null);

                    dt = new DataTable();
                }
            }
            catch (Exception expt)
            {
                string ErrorEmailBody = "Error in RP.SecurityTypeFeedSourceService in function CreateDataTableFromWatchListFile -  : " + expt.ToString();
                UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.BCSExceptionEmailListTo, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);

                System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
            }
            return dt;
        }

        private void SetEdgarOnlineFeedFileProcessed(string strFileName)
        {
            try
            {
                this.dataAccess.ExecuteNonQuery
                     (
                          this.DB1029ConnectionString,
                          SPSetEdgarOnlineFeedFileProcessed,
                          this.dataAccess.CreateParameter(DBCFileName, SqlDbType.NVarChar, strFileName)
                      );


                //Move file to Processed folder

                string zipPath = ConfigValues.EdgarOnlineFeedArchiveDropPath + strFileName;
                if (File.Exists(zipPath))
                {
                    File.Copy(zipPath, ConfigValues.EdgarOnlineFeedArchiveProcessedFilePath + strFileName, true);
                    File.Delete(zipPath);
                }

            }
            catch (Exception expt)
            {
                string ErrorEmailBody = "Error in RP.SecurityTypeFeedSourceService in function SetEdgarOnlineFeedFileProcessed -  : " + expt.ToString();
                UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.BCSExceptionEmailListTo, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);

                System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
            }
        }

        private void UpdateEdgarOnlineFeed()
        {
            try
            {
                this.dataAccess.ExecuteNonQuery
                     (
                          this.DB1029ConnectionString,
                          SPUpdateEdgarOnlineFeed
                      );

            }
            catch (Exception expt)
            {
                string ErrorEmailBody = "Error in RP.SecurityTypeFeedSourceService in function UpdateEdgarOnlineFeed -  : " + expt.ToString();
                UtilityFactory.SendEmail(ConfigValues.EmailFrom, ConfigValues.BCSExceptionEmailListTo, null, null, ConfigValues.BCSExceptionEmailSub, ErrorEmailBody, "Support", null);

                System.Threading.Thread.Sleep(ConfigValues.ErrorSleepTime);
            }
        }





        #region Daily Update Report

        public List<BCSDailyUpdateReport> GetDailyUpdateReport(DateTime selectedDate, string SortColumn, string SortDirection, int StartIndex, int EndIndex, out int VirtualCount)
        {
            List<BCSDailyUpdateReport> reportData = new List<BCSDailyUpdateReport>();
            VirtualCount = 0;
            using (IDataReader reader = this.dataAccess.ExecuteReader
                  (
                       this.DB1029ConnectionString,
                       SPGetDailyUpdateReport,
                       this.dataAccess.CreateParameter(DBCSelectedDate, SqlDbType.DateTime, selectedDate),
                       this.dataAccess.CreateParameter(DBCSortDirection, SqlDbType.NVarChar, SortDirection),
                       this.dataAccess.CreateParameter(DBCSortColumn, SqlDbType.NVarChar, SortColumn),
                       this.dataAccess.CreateParameter(DBCStartIndex, SqlDbType.Int, StartIndex),
                       this.dataAccess.CreateParameter(DBCEndIndex, SqlDbType.Int, EndIndex)
                   ))
            {
                while (reader.Read())
                {

                    BCSDailyUpdateReport objBCSDailyUpdateReport = new BCSDailyUpdateReport();
                    objBCSDailyUpdateReport.CUSIP = reader["CUSIP"].ToString();
                    objBCSDailyUpdateReport.CompanyCIK = reader["CompanyCIK"].ToString();
                    objBCSDailyUpdateReport.CompanyName = reader["CompanyName"].ToString();
                    objBCSDailyUpdateReport.FundName = reader["FundName"].ToString();
                    objBCSDailyUpdateReport.SecurityType = reader["SecurityType"].ToString();
                    objBCSDailyUpdateReport.Class = reader["Class"].ToString();
                    objBCSDailyUpdateReport.Ticker = reader["Ticker"].ToString();
                    objBCSDailyUpdateReport.SeriesID = reader["SeriesID"].ToString();
                    objBCSDailyUpdateReport.OldSecurityType = reader["OldSecurityType"].ToString();
                    reportData.Add(objBCSDailyUpdateReport);

                }
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        VirtualCount = Convert.ToInt32(reader["virtualCount"].ToString());
                    }
                }
            }
            return reportData;
        }

        #endregion

        #region Missing Reports
        #endregion

        #region Security Types
        #endregion


    }
}
