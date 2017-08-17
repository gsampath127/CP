using BCS.Core.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BCS.ObjectModel.Factories
{
    public class RPSecurityTypeDashboardFactory
    {
        private readonly string DB1029ConnectionString;
        private readonly string HostedAdminConnectionString;
        private readonly IDataAccess dataAccess;

        private const string SPBCSGetCompnayDetails = "BCS_GetCompanyDetails";
        private const string SPBCSGetSecuritTypeDetails = "BCS_GetSecuritTypeDetails";
        private const string SPGetSummarizedSecurityFeed = "BCS_GetSummarizedSecurityFeed";
        private const string SPUpdateSecurityTypesInProsTicker = "BCS_UpdateSecurityTypesInProsTicker";
        private const string spGetMissingReports = "BCS_GetMissingReports";
        private const string spGetEdgarOnlineFeedFileHistory = "BCS_GetEdgarOnlineFeedFileHistory";
        private const string SPGetEdgarOnlineData = "BCS_GetEdgarOnlineData";

        private const string DBCTaxonomyMarketIDs = "TaxonomyMarketIDs";
        private const string DBCCompanyID = "CompanyID";
        private const string DBCSecurityTypeID = "SecurityTypeID";
        private const string DBCStartIndex = "startIndex";
        private const string DBCEndIndex = "endIndex";
        private const string DBCSortColumn = "sortColumn";
        private const string DBCSortDirection = "sortDirection";
        private const string DBCCUSIP = "CUSIP";
        private const string DBCSecurityTypeFeedSourceName = "SecurityTypeFeedSourceName";
        private const string DBCReportType = "ReportType";
        private const string DBCCount = "count";
        private const string DBCSelectedDate = "SelectedDate";
        private const string DBCCUSIPs= "CUSIPs";
        private const string DBCCIKs = "CIKs";
        private const string DBCSeries = "Series";
        private const string DBCClass = "Class";
        private const string DBCTicker = "Ticker";


        public RPSecurityTypeDashboardFactory()
        {
            this.DB1029ConnectionString = DBConnectionString.DB1029ConnectionString();
            this.HostedAdminConnectionString = DBConnectionString.HostedAdminConnectionString();
            this.dataAccess = new DataAccess();
        }

        #region Daily Update Report
        #endregion

        #region Missing Reports

        public int UpdateSecurityTypesInProsTicker(string CUSIP, int securityTypeID, string securityTypeFeedSourceName)
        {
            try
            {
                this.dataAccess.ExecuteNonQuery
                     (
                          this.DB1029ConnectionString,
                          SPUpdateSecurityTypesInProsTicker,
                          this.dataAccess.CreateParameter(DBCCUSIP, SqlDbType.NVarChar, CUSIP),
                          this.dataAccess.CreateParameter(DBCSecurityTypeID, SqlDbType.Int, securityTypeID),
                          this.dataAccess.CreateParameter(DBCSecurityTypeFeedSourceName, SqlDbType.NVarChar, securityTypeFeedSourceName)
                      );
                return 1;

            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public List<BCSMissingReports> GetMissingReports(string reportType, int startindex, int endindex, string sortcolumn, string sortdirection, out int virtualCount)
        {
            List<BCSMissingReports> bcsMissingReportData = new List<BCSMissingReports>();
            int count = 0;
            using (IDataReader reader = this.dataAccess.ExecuteReader
             (
                  this.DB1029ConnectionString,
                  spGetMissingReports,
                  this.dataAccess.CreateParameter(DBCReportType, SqlDbType.NVarChar, reportType),
                  this.dataAccess.CreateParameter(DBCStartIndex, SqlDbType.Int, startindex),
                  this.dataAccess.CreateParameter(DBCEndIndex, SqlDbType.Int, endindex),
                  this.dataAccess.CreateParameter(DBCSortColumn, SqlDbType.NVarChar, sortcolumn),
                  this.dataAccess.CreateParameter(DBCSortDirection, SqlDbType.NVarChar, sortdirection),
                  this.dataAccess.CreateParameter(DBCCount, SqlDbType.Int, count, ParameterDirection.Output)
              ))
            {

                BCSMissingReports bcsMissingReport;
                while (reader.Read())
                {
                    bcsMissingReport = new BCSMissingReports()
                    {
                        CUSIP = reader["CUSIP"].ToString(),
                        CompanyName = reader["CompanyName"].ToString(),
                        FundName = reader["FundName"].ToString(),
                        CIK = reader["CIK"].ToString(),
                        SeriesID = reader["SeriesID"].ToString(),
                        ClassContractID = reader["ClassContractID"].ToString(),
                        Ticker = reader["TickerSymbol"].ToString()
                    };
                    bcsMissingReportData.Add(bcsMissingReport);
                }
                virtualCount = 0;
                if (reader.NextResult())
                {
                    if (reader.Read())
                    {
                        virtualCount = int.Parse(reader["virtualcount"].ToString());
                    }
                }
            }
            return bcsMissingReportData;
        }

        #endregion

        #region Security Types

        public DataSet GetCompanyandSecurityType()
        {
            return this.dataAccess.ExecuteDataSet(this.DB1029ConnectionString, SPBCSGetCompnayDetails);

        }
        public DataTable GetSummarizedData()
        {
            DataTable data = new DataTable();
            data = this.dataAccess.ExecuteDataTable(this.DB1029ConnectionString, SPGetSummarizedSecurityFeed);
            DataRow row ;
            row = data.NewRow();
            row["LoadType"] = "Total";
            row["ETF"] = data.Compute("Sum(ETF)","");
            row["ETN"] = data.Compute("Sum(ETN)", "");
            row["MF"] = data.Compute("Sum(MF)", "");
            row["NA"] = data.Compute("Sum(NA)", "");
            row["UIT"] = data.Compute("Sum(UIT)", "");
            data.Rows.Add(row);
            return data;
            
        }

        public List<BCSecurityType> GetSecurityTypeDetails(string CUSIP, int? CompanyID, int? SecuritTypeID, string SortColumn, string SortDirection, int StartIndex, int EndIndex, out int VirtualCount)
        {
            List<BCSecurityType> lstSecurityTypeDetails = new List<BCSecurityType>();
            DataTable marketIds = new DataTable();
            VirtualCount = 0;
            marketIds.Columns.Add("marketId", typeof(string));
            marketIds.Columns.Add("level", typeof(int));

            if (!string.IsNullOrWhiteSpace(CUSIP))
            {
                CUSIP = CUSIP.Replace("\r", "").Replace("\t", "");
                string[] cusipDeliminator = new string[] { "\n" };
                string[] lstCUSIP = CUSIP.Split(cusipDeliminator, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in lstCUSIP)
                {
                    marketIds.Rows.Add(item.Trim(), 0);
                }
            }
            using (IDataReader reader = this.dataAccess.ExecuteReader
              (
                   this.DB1029ConnectionString,
                   SPBCSGetSecuritTypeDetails,
                   this.dataAccess.CreateParameter(DBCTaxonomyMarketIDs, SqlDbType.Structured, marketIds),
                   this.dataAccess.CreateParameter(DBCCompanyID, SqlDbType.Int, CompanyID),
                   this.dataAccess.CreateParameter(DBCSecurityTypeID, SqlDbType.Int, SecuritTypeID),
                   this.dataAccess.CreateParameter(DBCSortDirection, SqlDbType.NVarChar, SortDirection),
                   this.dataAccess.CreateParameter(DBCSortColumn, SqlDbType.NVarChar, SortColumn),
                   this.dataAccess.CreateParameter(DBCStartIndex, SqlDbType.Int, StartIndex),
                   this.dataAccess.CreateParameter(DBCEndIndex, SqlDbType.Int, EndIndex)
               ))
            {
                while (reader.Read())
                {
                    BCSecurityType obj = new BCSecurityType();
                    obj.CUSIP = reader["CUSIP"].ToString();
                    obj.CompanyCIK = reader["CompanyCIK"].ToString();
                    obj.CompanyName = reader["CompanyName"].ToString();
                    obj.FundName = reader["FundName"].ToString();
                    obj.SecurityType = reader["SecurityType"].ToString();
                    obj.ShareClass = reader["ShareClass"].ToString();
                    obj.Ticker = reader["Ticker"].ToString();
                    obj.Loadtype = reader["Loadtype"].ToString();
                    lstSecurityTypeDetails.Add(obj);
                }

                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        VirtualCount = Convert.ToInt32(reader["virtualCount"].ToString());
                    }
                }
            }

            return lstSecurityTypeDetails;

        }

        #endregion

        #region EdgarOnlineFeed

        public DataTable GetEdgarOnlineFeedHistory(DateTime selectedDate)
        {
            return this.dataAccess.ExecuteDataTable(this.DB1029ConnectionString, spGetEdgarOnlineFeedFileHistory,this.dataAccess.CreateParameter(DBCSelectedDate,SqlDbType.DateTime,selectedDate));

        }
        #endregion

        #region Edgar Online Data
        public List<BCSEdgarOnlineData> GetBCSEdgarOnlineData(string CUSIP, string CIK, string Series, string Class, string Ticker, int StartIndex, int EndIndex,out int virtualCount)
        {
            virtualCount = 0;
            #region Filters
            DataTable cusips = new DataTable();
            cusips.Columns.Add("marketId", typeof(string));
            cusips.Columns.Add("level", typeof(int));
            DataTable cik = new DataTable();
            cik.Columns.Add("marketId", typeof(string));
            cik.Columns.Add("level", typeof(int));
            DataTable series = new DataTable();
            series.Columns.Add("marketId", typeof(string));
            series.Columns.Add("level", typeof(int));
            DataTable classes = new DataTable();
            classes.Columns.Add("marketId", typeof(string));
            classes.Columns.Add("level", typeof(int));
            DataTable ticker = new DataTable();
            ticker.Columns.Add("marketId", typeof(string));
            ticker.Columns.Add("level", typeof(int));
            string[] Deliminator = new string[] { "\n" };


            if (!string.IsNullOrWhiteSpace(CUSIP))
            {
                CUSIP = CUSIP.Replace("\r", "").Replace("\t", "");
                string[] lstCUSIP = CUSIP.Split(Deliminator, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in lstCUSIP)
                {
                    cusips.Rows.Add(item.Trim(), 0);
                }
            }
            if (!string.IsNullOrWhiteSpace(CIK))
            {
                CIK = CIK.Replace("\r", "").Replace("\t", "");
                string[] lstCIK = CIK.Split(Deliminator, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in lstCIK)
                {
                    cik.Rows.Add(item.Trim(), 0);
                }
            }
            if (!string.IsNullOrWhiteSpace(Series))
            {
                Series = Series.Replace("\r", "").Replace("\t", "");
               string[] lstSeries = Series.Split(Deliminator, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in lstSeries)
                {
                    series.Rows.Add(item.Trim(), 0);
                }
            }
            if (!string.IsNullOrWhiteSpace(Class))
            {
                Class = Class.Replace("\r", "").Replace("\t", "");
                string[] lstClass = Class.Split(Deliminator, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in lstClass)
                {
                    classes.Rows.Add(item.Trim(), 0);
                }
            }
            if (!string.IsNullOrWhiteSpace(Ticker))
            {
                Ticker = Ticker.Replace("\r", "").Replace("\t", "");
                string[] lstTicker = Ticker.Split(Deliminator, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in lstTicker)
                {
                    ticker.Rows.Add(item.Trim(), 0);
                }
            }
            #endregion

            List<BCSEdgarOnlineData> lstBCSEdgarOnlineData = new List<BCSEdgarOnlineData>();
            using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.DB1029ConnectionString,
                    SPGetEdgarOnlineData,
                    this.dataAccess.CreateParameter(DBCCUSIPs, SqlDbType.Structured, cusips),
                    this.dataAccess.CreateParameter(DBCCIKs, SqlDbType.Structured, cik),
                    this.dataAccess.CreateParameter(DBCSeries, SqlDbType.Structured, series),
                    this.dataAccess.CreateParameter(DBCClass, SqlDbType.Structured, classes),
                    this.dataAccess.CreateParameter(DBCTicker, SqlDbType.Structured, ticker),
                    this.dataAccess.CreateParameter(DBCStartIndex, SqlDbType.Int, StartIndex),
                    this.dataAccess.CreateParameter(DBCEndIndex, SqlDbType.Int, EndIndex)
                ))
            {
                while (reader.Read())
                {
                    BCSEdgarOnlineData data = new BCSEdgarOnlineData();
                    data.ECUSIP = reader["ECUSIP"].ToString();
                    data.ECompanyName = reader["ECompanyName"].ToString();
                    data.ESeriesID = reader["ESeriesID"].ToString();
                    data.EClassContractID = reader["EClassContractID"].ToString();
                    data.ECIK = reader["ECIK"].ToString();
                    data.ETicker = reader["Eticker"].ToString();
                    data.EFundName = reader["EFundName"].ToString();
                    lstBCSEdgarOnlineData.Add(data);
                    virtualCount = virtualCount>0 ?virtualCount: Convert.ToInt32(reader["virtualCount"].ToString());
                }

              
        }

            return lstBCSEdgarOnlineData;

        }
        #endregion
    }
}
