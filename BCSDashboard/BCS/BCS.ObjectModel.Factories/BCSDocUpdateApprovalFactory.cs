using BCS.Core.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace BCS.ObjectModel.Factories
{
    public class BCSDocUpdateApprovalFactory
    {
        private readonly string DB1029ConnectionString;
        private readonly string HostedAdminConnectionString;
        private readonly IDataAccess dataAccess;

        private const string DBCCUSIP = "CUSIP";
        private const string DBCTaxonomyMarketIDs = "TaxonomyMarketIDs";
        private const string DBCDocumentIDs = "DocIDs";
        private const string DBCAccNumber = "Acc#";
        private const string DBCStartIndex = "StartIndex";
        private const string DBCEndIndex = "EndIndex";
        private const string DBBCSDocUpdateId = "BCSDocUpdateId";
        private const string DBCISPDFRECEIVED = "IsPDFReceived";

        private const string DBCReportType = "ReportType";
        private const string DBCStartDate = "StartDate";
        private const string DBCEndDate = "EndDate";

        private const string DBCSortColumn = "sortColumn";
        private const string DBCSortDirection = "sortDirection";
        private const string DBCCount = "count";

        private const string DBCSelectedDate = "SelectedDate";
        private const string DBCStatus = "Status";

        private const string DBCID = "Id";
        private const string DBCType = "Type";

        private const string DBCFLTDate = "FLTDate";
        private const string DBCompany = "Company";


        private const string SPGetBCSDocUpdateApprovalDuplicateCUSIPData = "BCS_GetBCSDocUpdateApprovalDuplicateCUSIPData";
        private const string SPGetBCSCustomerDocUpdateDuplicateCUSIPData = "BCS_GetBCSCustomerDocUpdateDuplicateCUSIPData";
        private const string SPGetBCSDocUpdateApprovalAllCUSIPData = "BCS_GetBCSDocUpdateApprovalAllCUSIPData";
        private const string SPRemoveDuplicateCUSIPUsingBCSDocUpdateID = "BCS_RemoveDuplicateCUSIPUsingBCSDocUpdateID";
        private const string SPGetBCSCustomerDocUpdatelAllCUSIPData = "BCS_GetBCSCustomerDocUpdateAllCUSIPData";
        private const string SPBCSRemoveCustomerDocUpdateDupicateCUSIPs = "BCS_RemoveCustomerDocUpdateDupicateCUSIPs";

        private const string SPGetBCSLiveUpdateAllianceBernsteinAllCusipData = "BCS_GetBCSLiveUpdateAllianceBernsteinAllCusipData";
        private const string SPGetBCSLiveUpdateTransamericaAllCusipData = "BCS_GetBCSLiveUpdateTransamericaAllCusipData";


        private const string SPGetBCSTRPReportFLTFTPInfoData = "BCS_GetBCSTRPReportFLTFTPInfoData";
        private const string SPGetBCSTRPReportFLTMissingData = "BCS_GetBCSTRPReportFLTMissingData";
        private const string SPGetBCSTRPReportRPMissingCUSIPData = "BCS_GetBCSTRPReportRPMissingCUSIPData";
        private const string SPGetBCSTRPReportBlankFLTCUSIPData = "BCS_GetBCSTRPBlankFLTCUSIPdetail";


        private const string SPBCSGetClientNames = "BCS_GetBCSClients";


        private const string SPGetBCSTransamericaReports = "BCSTransamerica_GetReports";
        private const string SPGetBCSAllianceBernsteinReports = "BCSAllianceBernstein_GetReports";

        private const string SPBCSTransamericaGetSLINKReportDetails = "BCS_TransamericaGetSLINKReportDetails";
        private const string SPBCSAllianceBernsteinGetSLINKReportDetails = "BCS_AllianceBernsteinGetSLINKReportDetails";

        private const string SPGetBCSTRPFLTFileInfo = "BCS_GetBCSTRPFLTFileInfo";
        private const string SPGBCSGetMissingCUSIP = "BCS_GetMissingCUSIP";

        public BCSDocUpdateApprovalFactory()
        {
            this.DB1029ConnectionString = DBConnectionString.DB1029ConnectionString();
            this.HostedAdminConnectionString = DBConnectionString.HostedAdminConnectionString();
            this.dataAccess = new DataAccess();
        }

        public BCSDocUpdateApprovalCUSIPData GetBCSDocUpdateApprovalDuplicateCUSIPData(string CUSIP, string AccNumber, int StartIndex, int EndIndex, string status = null)
        {
            DataTable marketIds = new DataTable();
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

            BCSDocUpdateApprovalCUSIPData bCSDocUpdateApprovalCUSIPData = new BCSDocUpdateApprovalCUSIPData();
            bCSDocUpdateApprovalCUSIPData.DuplicateCUSIPDetails = new List<BCSDocUpdateApprovalCUSIPDetails>();

            using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.DB1029ConnectionString,
                    SPGetBCSDocUpdateApprovalDuplicateCUSIPData,
                    this.dataAccess.CreateParameter(DBCTaxonomyMarketIDs, SqlDbType.Structured, marketIds),
                    this.dataAccess.CreateParameter(DBCAccNumber, SqlDbType.NVarChar, AccNumber),
                    this.dataAccess.CreateParameter(DBCStatus, SqlDbType.NVarChar, status),
                    this.dataAccess.CreateParameter(DBCStartIndex, SqlDbType.Int, StartIndex),
                    this.dataAccess.CreateParameter(DBCEndIndex, SqlDbType.Int, EndIndex)
                ))
            {
                if (reader.Read())
                {
                    bCSDocUpdateApprovalCUSIPData.DuplicateCUSIPDetailsTotalCount = Convert.ToInt32(reader["DuplicateCUSIPDetailsTotalCount"]);
                }

                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        BCSDocUpdateApprovalCUSIPDetails details = new BCSDocUpdateApprovalCUSIPDetails();
                        details.BCSDocUpdateId = Convert.ToInt32(reader["BCSDocUpdateId"]);
                        details.CUSIP = Convert.ToString(reader["CUSIP"]);
                        details.EdgarID = Convert.ToInt32(reader["EdgarID"]);
                        details.Accnumber = Convert.ToString(reader["Acc#"]);
                        details.RRDPDFURL = Convert.ToString(reader["RRDPDFURL"]);
                        details.DocumentType = Convert.ToString(reader["DocumentType"]);
                        details.DocumentDate = Convert.ToString(reader["DocumentDate"]);
                        details.FundName = Convert.ToString(reader["FundName"]);
                        details.Status = Convert.ToString(reader["Status"]);

                        bCSDocUpdateApprovalCUSIPData.DuplicateCUSIPDetails.Add(details);
                    }
                }
            }
            return bCSDocUpdateApprovalCUSIPData;
        }

        public BCSDocUpdateApprovalCUSIPData GetBCSCustomerDocUpdateDuplicateCUSIPData(string CUSIP, string AccNumber, int StartIndex, int EndIndex, string status = null)
        {
            DataTable marketIds = new DataTable();
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

            BCSDocUpdateApprovalCUSIPData bCSCustomerDocUpdateCUSIPData = new BCSDocUpdateApprovalCUSIPData();
            bCSCustomerDocUpdateCUSIPData.DuplicateCUSIPDetails = new List<BCSDocUpdateApprovalCUSIPDetails>();
            string PdfName = string.Empty;
            using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.DB1029ConnectionString,
                    SPGetBCSCustomerDocUpdateDuplicateCUSIPData,
                    this.dataAccess.CreateParameter(DBCTaxonomyMarketIDs, SqlDbType.Structured, marketIds),
                    this.dataAccess.CreateParameter(DBCAccNumber, SqlDbType.NVarChar, AccNumber),
                    this.dataAccess.CreateParameter(DBCStatus, SqlDbType.NVarChar, status),
                    this.dataAccess.CreateParameter(DBCStartIndex, SqlDbType.Int, StartIndex),
                    this.dataAccess.CreateParameter(DBCEndIndex, SqlDbType.Int, EndIndex)
                ))
            {
                while (reader.Read())
                {
                    BCSDocUpdateApprovalCUSIPDetails details = new BCSDocUpdateApprovalCUSIPDetails();
                    details.BCSDocUpdateId = Convert.ToInt32(reader["Id"]);
                    details.CUSIP = Convert.ToString(reader["CUSIP"]);
                    details.EdgarID = Convert.ToInt32(reader["EdgarID"]);
                    details.Accnumber = Convert.ToString(reader["Acc#"]);
                    details.DocumentType = Convert.ToString(reader["DocumentType"]);
                    details.DocumentDate = Convert.ToString(reader["DocumentDate"]);
                    details.FundName = Convert.ToString(reader["FundName"]);
                    details.Status = Convert.ToString(reader["Status"]);
                    details.ReportType = Convert.ToString(reader["ReportType"]);
                    PdfName = "PDF\\" + Convert.ToString(reader["PdfName"]);
                    details.RRDPDFURL = Convert.ToString(reader["RRDPDFURL"]);
                    if (!string.IsNullOrWhiteSpace(details.RRDPDFURL))
                    {
                        details.RRDPDFURL = details.RRDPDFURL.Replace("\\GIM\\Zip", string.Empty).Replace(Path.GetFileName(details.RRDPDFURL), PdfName);
                        details.RRDPDFURL = details.RRDPDFURL.ToLower().Replace(ConfigValues.RPBCSSANPath.ToLower(), ConfigValues.RPDestinationURLReplace).Replace(@"\", "/");
                    }
                    bCSCustomerDocUpdateCUSIPData.DuplicateCUSIPDetails.Add(details);
                }

                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        bCSCustomerDocUpdateCUSIPData.DuplicateCUSIPDetailsTotalCount = Convert.ToInt32(reader["DuplicateCUSIPDetailsTotalCount"]);
                    }
                }

            }
            return bCSCustomerDocUpdateCUSIPData;
        }

        public BCSDocUpdateApprovalCUSIPData GetBCSDocUpdateApprovalAllCUSIPData(string CUSIP, string AccNumber, int StartIndex, int EndIndex, string status, string documentID, List<string> lstInvalidCUSIPs, List<string> lstInvalidDocIds)
        {
            DataTable marketIds = new DataTable();
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
            DataTable documentIds = new DataTable();
            documentIds.Columns.Add("documentId", typeof(string));

            if (!string.IsNullOrWhiteSpace(documentID))
            {
                documentID = documentID.Replace("\r", "").Replace("\t", "");
                string[] cusipDeliminator = new string[] { "\n" };
                string[] lstDocumentID = documentID.Split(cusipDeliminator, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in lstDocumentID)
                {
                    documentIds.Rows.Add(item.Trim());
                }
            }

            BCSDocUpdateApprovalCUSIPData bCSDocUpdateApprovalCUSIPData = new BCSDocUpdateApprovalCUSIPData();
            bCSDocUpdateApprovalCUSIPData.AllCUSIPDetails = new List<BCSDocUpdateApprovalCUSIPDetails>();

            using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.DB1029ConnectionString,
                    SPGetBCSDocUpdateApprovalAllCUSIPData,
                    this.dataAccess.CreateParameter(DBCTaxonomyMarketIDs, SqlDbType.Structured, marketIds),
                    this.dataAccess.CreateParameter(DBCDocumentIDs, SqlDbType.Structured, documentIds),
                    this.dataAccess.CreateParameter(DBCAccNumber, SqlDbType.NVarChar, AccNumber),
                     this.dataAccess.CreateParameter(DBCStatus, SqlDbType.NVarChar, status),
                    this.dataAccess.CreateParameter(DBCStartIndex, SqlDbType.Int, StartIndex),
                    this.dataAccess.CreateParameter(DBCEndIndex, SqlDbType.Int, EndIndex)
                ))
            {

                if (reader.Read())
                {
                    bCSDocUpdateApprovalCUSIPData.AllCUSIPDetailsTotalCount = Convert.ToInt32(reader["AllCUSIPDetailsTotalCount"]);
                }

                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        BCSDocUpdateApprovalCUSIPDetails details = new BCSDocUpdateApprovalCUSIPDetails();
                        details.BCSDocUpdateId = Convert.ToInt32(reader["BCSDocUpdateId"]);
                        details.CUSIP = Convert.ToString(reader["CUSIP"]);
                        details.EdgarID = Convert.ToInt32(reader["EdgarID"]);
                        details.Accnumber = Convert.ToString(reader["Acc#"]);
                        details.RRDPDFURL = Convert.ToString(reader["RRDPDFURL"]);
                        details.DocumentType = Convert.ToString(reader["DocumentType"]);
                        details.DocumentDate = Convert.ToString(reader["DocumentDate"]);
                        details.DocumentID = Convert.ToString(reader["DocumentID"]);
                        details.FundName = Convert.ToString(reader["FundName"]);
                        details.Status = Convert.ToString(reader["Status"]);
                        details.StatusDate = Convert.ToString(reader["StatusDate"]);
                        bCSDocUpdateApprovalCUSIPData.AllCUSIPDetails.Add(details);
                    }
                }

                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        lstInvalidCUSIPs.Add(Convert.ToString(reader["CUSIP"]));
                    }
                }
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        lstInvalidDocIds.Add(Convert.ToString(reader["DocumentID"]));
                    }
                }
            }
            return bCSDocUpdateApprovalCUSIPData;
        }

        public BCSDocUpdateApprovalCUSIPData GetBCSCustomerDocUpdateAllCUSIPData(string CUSIP, string AccNumber, int StartIndex, int EndIndex, string Status = null)
        {
            DataTable marketIds = new DataTable();
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

            BCSDocUpdateApprovalCUSIPData bCSCustomerDocUpdateCUSIPData = new BCSDocUpdateApprovalCUSIPData();
            bCSCustomerDocUpdateCUSIPData.AllCUSIPDetails = new List<BCSDocUpdateApprovalCUSIPDetails>();
            string PdfName = string.Empty;
            using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.DB1029ConnectionString,
                    SPGetBCSCustomerDocUpdatelAllCUSIPData,
                    this.dataAccess.CreateParameter(DBCTaxonomyMarketIDs, SqlDbType.Structured, marketIds),
                    this.dataAccess.CreateParameter(DBCAccNumber, SqlDbType.NVarChar, AccNumber),
                    this.dataAccess.CreateParameter(DBCStatus, SqlDbType.NVarChar, Status),
                    this.dataAccess.CreateParameter(DBCStartIndex, SqlDbType.Int, StartIndex),
                    this.dataAccess.CreateParameter(DBCEndIndex, SqlDbType.Int, EndIndex)
                ))
            {


                while (reader.Read())
                {
                    BCSDocUpdateApprovalCUSIPDetails details = new BCSDocUpdateApprovalCUSIPDetails();
                    details.BCSDocUpdateId = Convert.ToInt32(reader["Id"]);
                    details.CUSIP = Convert.ToString(reader["CUSIP"]);
                    details.EdgarID = Convert.ToInt32(reader["EdgarID"]);
                    details.Accnumber = Convert.ToString(reader["Acc#"]);
                    details.DocumentType = Convert.ToString(reader["DocumentType"]);
                    details.DocumentDate = Convert.ToString(reader["DocumentDate"]);
                    details.FundName = Convert.ToString(reader["FundName"]);
                    details.Status = Convert.ToString(reader["Status"]);
                    details.StatusDate = Convert.ToString(reader["StatusDate"]);
                    details.ReportType = Convert.ToString(reader["ReportType"]);
                    PdfName = "PDF\\" + Convert.ToString(reader["PdfName"]);
                    details.RRDPDFURL = Convert.ToString(reader["RRDPDFURL"]);
                    if (!string.IsNullOrWhiteSpace(details.RRDPDFURL))
                    {
                        details.RRDPDFURL = details.RRDPDFURL.Replace("\\GIM\\Zip", string.Empty).Replace(Path.GetFileName(details.RRDPDFURL), PdfName);
                        details.RRDPDFURL = details.RRDPDFURL.ToLower().Replace(ConfigValues.RPBCSSANPath.ToLower(), ConfigValues.RPDestinationURLReplace).Replace(@"\", "/");
                    }
                    bCSCustomerDocUpdateCUSIPData.AllCUSIPDetails.Add(details);
                }
                if (reader.NextResult())
                {
                    if (reader.Read())
                    {
                        bCSCustomerDocUpdateCUSIPData.AllCUSIPDetailsTotalCount = Convert.ToInt32(reader["AllCUSIPDetailsTotalCount"]);
                    }
                }
            }
            return bCSCustomerDocUpdateCUSIPData;
        }

        public BCSDocUpdateApprovalCUSIPData GetBCSLiveUpdateCustomerDocUpdateAllCUSIPData(string clientName, string CUSIP, string AccNumber, int StartIndex, int EndIndex, string Status, string documentID, List<string> lstInvalidCUSIPs, List<string> lstInvalidDocIds)
        {
            DataTable marketIds = new DataTable();
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
            DataTable documentIds = new DataTable();
            documentIds.Columns.Add("documentId", typeof(string));

            if (!string.IsNullOrWhiteSpace(documentID))
            {
                documentID = documentID.Replace("\r", "").Replace("\t", "");
                string[] cusipDeliminator = new string[] { "\n" };
                string[] lstDocumentID = documentID.Split(cusipDeliminator, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in lstDocumentID)
                {
                    documentIds.Rows.Add(item.Trim());
                }
            }


            BCSDocUpdateApprovalCUSIPData bCSCustomerDocUpdateCUSIPData = new BCSDocUpdateApprovalCUSIPData();
            bCSCustomerDocUpdateCUSIPData.AllCUSIPDetails = new List<BCSDocUpdateApprovalCUSIPDetails>();
            string PdfName = string.Empty;
            string spGetBCSLiveUpdateCustomerAllCusipData = string.Empty;
            switch (clientName)
            {
                case "Transamerica": spGetBCSLiveUpdateCustomerAllCusipData = SPGetBCSLiveUpdateTransamericaAllCusipData; break;
                case "AllianceBernstein": spGetBCSLiveUpdateCustomerAllCusipData = SPGetBCSLiveUpdateAllianceBernsteinAllCusipData; break;
            }
            using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.DB1029ConnectionString,
                    spGetBCSLiveUpdateCustomerAllCusipData,
                    this.dataAccess.CreateParameter(DBCTaxonomyMarketIDs, SqlDbType.Structured, marketIds),
                    this.dataAccess.CreateParameter(DBCDocumentIDs, SqlDbType.Structured, documentIds),
                    this.dataAccess.CreateParameter(DBCAccNumber, SqlDbType.NVarChar, AccNumber),
                    this.dataAccess.CreateParameter(DBCStatus, SqlDbType.NVarChar, Status),
                    this.dataAccess.CreateParameter(DBCStartIndex, SqlDbType.Int, StartIndex),
                    this.dataAccess.CreateParameter(DBCEndIndex, SqlDbType.Int, EndIndex)
                ))
            {


                while (reader.Read())
                {
                    BCSDocUpdateApprovalCUSIPDetails details = new BCSDocUpdateApprovalCUSIPDetails();
                    details.BCSDocUpdateId = Convert.ToInt32(reader["Id"]);
                    details.CUSIP = Convert.ToString(reader["CUSIP"]);
                    details.EdgarID = Convert.ToInt32(reader["EdgarID"]);
                    details.Accnumber = Convert.ToString(reader["Acc#"]);
                    details.DocumentType = Convert.ToString(reader["DocumentType"]);
                    details.DocumentID = Convert.ToString(reader["DocumentID"]);
                    details.DocumentDate = Convert.ToString(reader["DocumentDate"]);
                    details.FundName = Convert.ToString(reader["FundName"]);
                    details.Status = Convert.ToString(reader["Status"]);
                    details.StatusDate = Convert.ToString(reader["StatusDate"]);
                    details.ReportType = Convert.ToString(reader["ReportType"]);
                    PdfName = "PDF\\" + Convert.ToString(reader["PdfName"]);
                    details.RRDPDFURL = Convert.ToString(reader["RRDPDFURL"]);
                    if (!string.IsNullOrWhiteSpace(details.RRDPDFURL))
                    {
                        details.RRDPDFURL = details.RRDPDFURL.Replace("\\GIM\\Zip", string.Empty).Replace(Path.GetFileName(details.RRDPDFURL), PdfName);
                        details.RRDPDFURL = details.RRDPDFURL.ToLower().Replace(ConfigValues.RPBCSSANPath.ToLower(), ConfigValues.RPDestinationURLReplace).Replace(@"\", "/");
                    }

                    bCSCustomerDocUpdateCUSIPData.AllCUSIPDetails.Add(details);
                }
                if (reader.NextResult())
                {
                    if (reader.Read())
                    {
                        bCSCustomerDocUpdateCUSIPData.AllCUSIPDetailsTotalCount = Convert.ToInt32(reader["AllCUSIPDetailsTotalCount"]);
                    }
                }

                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        lstInvalidCUSIPs.Add(Convert.ToString(reader["CUSIP"]));
                    }
                }
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        lstInvalidDocIds.Add(Convert.ToString(reader["DocumentID"]));
                    }
                }
            }
            return bCSCustomerDocUpdateCUSIPData;
        }

        public void RemoveDuplicateCUSIPUsingBCSDocUpdateID(int BCSDocUpdateId)
        {
            this.dataAccess.ExecuteNonQuery
              (
                   this.DB1029ConnectionString,
                   SPRemoveDuplicateCUSIPUsingBCSDocUpdateID,
                   this.dataAccess.CreateParameter(DBBCSDocUpdateId, SqlDbType.Int, BCSDocUpdateId)
              );
        }

        public void UpdateCustomerDocUpdateDuplicateCUSIP(int Id, string ReportType, string Type)
        {
            this.dataAccess.ExecuteNonQuery
            (
                 this.DB1029ConnectionString,
                 SPBCSRemoveCustomerDocUpdateDupicateCUSIPs,
                  this.dataAccess.CreateParameter(DBCType, SqlDbType.VarChar, Type),
                  this.dataAccess.CreateParameter(DBCID, SqlDbType.Int, Id),
                  this.dataAccess.CreateParameter(DBCReportType, SqlDbType.VarChar, ReportType)


            );
        }

        #region BCSTRPReports

        public BCSTRPReportData GetBCSTRPReportFLTFTPInfoData(string CUSIP, bool? isPDFReceived, int StartIndex, int EndIndex)
        {

            BCSTRPReportData bCSTRPReportData = new BCSTRPReportData();

            List<BCSTRPReportFLTFTPInfoData> bCSTRPReportFLTFTPInfoData = new List<BCSTRPReportFLTFTPInfoData>();

            using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.DB1029ConnectionString,
                    SPGetBCSTRPReportFLTFTPInfoData,
                    this.dataAccess.CreateParameter(DBCCUSIP, SqlDbType.NVarChar, CUSIP),
                    this.dataAccess.CreateParameter(DBCISPDFRECEIVED, SqlDbType.Bit, isPDFReceived),
                    this.dataAccess.CreateParameter(DBCStartIndex, SqlDbType.Int, StartIndex),
                    this.dataAccess.CreateParameter(DBCEndIndex, SqlDbType.Int, EndIndex)
                ))
            {

                if (reader.Read())
                {
                    bCSTRPReportData.BCSTRPReportFLTFTPInfoDataVirtualCount = Convert.ToInt32(reader["FLTFTPInfoDataTotalCount"]);
                }

                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        BCSTRPReportFLTFTPInfoData details = new BCSTRPReportFLTFTPInfoData();
                        details.CompanyName = Convert.ToString(reader["CompanyName"]);
                        details.FundName = Convert.ToString(reader["FundName"]);
                        details.FLTCUSIP = Convert.ToString(reader["FLTCUSIP"]);
                        details.RPCUSIP = Convert.ToString(reader["RPCUSIP"]);
                        details.DatePDFReceivedonFTP = Convert.ToString(reader["DatePDFReceivedonFTP"]);
                        bCSTRPReportFLTFTPInfoData.Add(details);
                    }
                }

                bCSTRPReportData.BCSTRPReportFLTFTPInfoData = bCSTRPReportFLTFTPInfoData;

            }
            return bCSTRPReportData;
        }

        public BCSTRPReportData GetBCSTRPReportBlankFLTCUSIPData(int StartIndex, int EndIndex)
        {

            BCSTRPReportData bCSTRPReportData = new BCSTRPReportData();

            List<BCSTRPReportBlankFLTCUSIPData> bCSTRPReportBlankFLTCUSIPData = new List<BCSTRPReportBlankFLTCUSIPData>();

            using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.DB1029ConnectionString,
                    SPGetBCSTRPReportBlankFLTCUSIPData,
                    this.dataAccess.CreateParameter(DBCStartIndex, SqlDbType.Int, StartIndex),
                    this.dataAccess.CreateParameter(DBCEndIndex, SqlDbType.Int, EndIndex)
                ))
            {

                if (reader.Read())
                {
                    bCSTRPReportData.BCSTRPReportBlankFLTCUSIPDataVirtualCount = Convert.ToInt32(reader["TotalBlankFLTCUSIPDetailCount"]);
                }

                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        BCSTRPReportBlankFLTCUSIPData details = new BCSTRPReportBlankFLTCUSIPData();
                        details.FUNDCODE = Convert.ToString(reader["FUNDCODE"]);
                        details.FUNDNAME = Convert.ToString(reader["FUNDNAME"]);
                        details.FUNDTYPE = Convert.ToString(reader["FUNDTYPE"]);
                        details.FUNDTELEACCESSCODE = Convert.ToString(reader["FUNDTELEACCESSCODE"]);
                        details.FUNDCUSIPNUMBER = Convert.ToString(reader["FUNDCUSIPNUMBER"]);
                        details.FUNDCHKHEADINGCODE = Convert.ToString(reader["FUNDCHKHEADINGCODE"]);
                        details.FUNDGROUPNUMBER = Convert.ToString(reader["FUNDGROUPNUMBER"]);
                        details.FUNDPROSPECTUSINSERT = Convert.ToString(reader["FUNDPROSPECTUSINSERT"]);
                        details.FUNDPROSPECTUSINSERT2 = Convert.ToString(reader["FUNDPROSPECTUSINSERT2"]);
                        details.FUNDTICKERSYMBOL = Convert.ToString(reader["FUNDTICKERSYMBOL"]);
                        details.FUNDDocName = Convert.ToString(reader["FUNDDocName"]);
                        details.DateFLTRecordHasChanged = Convert.ToString(reader["DateFLTRecordHasChanged"]);
                        bCSTRPReportBlankFLTCUSIPData.Add(details);
                    }
                }

                bCSTRPReportData.BCSTRPReportBlankFLTCUSIPData = bCSTRPReportBlankFLTCUSIPData;

            }
            return bCSTRPReportData;
        }


        public List<BCSTRPFLTFileInfo> GetTrpFLTFileInfo(DateTime FLTDate)
        {
            List<BCSTRPFLTFileInfo> lstFLTFileInfo = new List<BCSTRPFLTFileInfo>();
            using (IDataReader reader = this.dataAccess.ExecuteReader
                (
                this.DB1029ConnectionString,
                SPGetBCSTRPFLTFileInfo,
                this.dataAccess.CreateParameter(DBCFLTDate, SqlDbType.DateTime, FLTDate) // .ToString("yyyy-MM-dd HH:mm:ss")
                ))
            {
                //if(reader.Read())
                //{
                while (reader.Read())
                {
                    BCSTRPFLTFileInfo objFLTFileInfo = new BCSTRPFLTFileInfo();
                    objFLTFileInfo.FileName = reader["FileName"].ToString();
                    objFLTFileInfo.DateReceived = reader["DateReceived"].ToString();
                    lstFLTFileInfo.Add(objFLTFileInfo);
                }
                //}
            }
            return lstFLTFileInfo;
        }

        public BCSTRPReportData GetBCSTRPReportFLTMissingData(int StartIndex, int EndIndex)
        {

            BCSTRPReportData bCSTRPReportData = new BCSTRPReportData();

            List<BCSTRPReportFLTMissingData> bCSTRPReportFLTMissingData = new List<BCSTRPReportFLTMissingData>();

            using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.HostedAdminConnectionString,
                    SPGetBCSTRPReportFLTMissingData,
                    this.dataAccess.CreateParameter(DBCStartIndex, SqlDbType.Int, StartIndex),
                    this.dataAccess.CreateParameter(DBCEndIndex, SqlDbType.Int, EndIndex)
                ))
            {

                if (reader.Read())
                {
                    bCSTRPReportData.BCSTRPReportFLTMissingDataVirtualCount = Convert.ToInt32(reader["FLTMissingDataTotalCount"]);
                }

                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        BCSTRPReportFLTMissingData details = new BCSTRPReportFLTMissingData();
                        details.CompanyName = Convert.ToString(reader["CompanyName"]);
                        details.FileName = Convert.ToString(reader["FileName"]);
                        details.Path = Convert.ToString(reader["Path"]);
                        if (!string.IsNullOrWhiteSpace(details.Path))
                        {
                            details.Path = details.Path.ToLower().Replace(ConfigValues.BCSTRowePriceFLTFTPArchiveDocumentPath.ToLower(), ConfigValues.BCSTRowePriceFLTFTPArchiveDocumentPathURL).Replace(@"\", @"/"); ;
                        }
                        details.DateReceivedOnFTP = Convert.ToString(reader["DateReceivedOnFTP"]);
                        bCSTRPReportFLTMissingData.Add(details);
                    }
                }

                bCSTRPReportData.BCSTRPReportFLTMissingData = bCSTRPReportFLTMissingData;

            }
            return bCSTRPReportData;
        }


        public BCSTRPReportData GetBCSTRPReportRPMissingCUSIPData(int StartIndex, int EndIndex)
        {

            BCSTRPReportData bCSTRPReportData = new BCSTRPReportData();

            List<BCSTRPReportRPCUSIPMissingData> bCSTRPReportRPMissingCUSIPData = new List<BCSTRPReportRPCUSIPMissingData>();

            using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.DB1029ConnectionString,
                    SPGetBCSTRPReportRPMissingCUSIPData,
                    this.dataAccess.CreateParameter(DBCStartIndex, SqlDbType.Int, StartIndex),
                    this.dataAccess.CreateParameter(DBCEndIndex, SqlDbType.Int, EndIndex)
                ))
            {

                if (reader.Read())
                {
                    bCSTRPReportData.BCSTRPReportRPCUSIPMissingDataVirtualCount = Convert.ToInt32(reader["RPMissingCUSIPDataTotalCount"]);
                }

                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        BCSTRPReportRPCUSIPMissingData details = new BCSTRPReportRPCUSIPMissingData();
                        details.CompanyName = Convert.ToString(reader["CompanyName"]);
                        details.FundName = Convert.ToString(reader["FundName"]);
                        details.FLTCUSIP = Convert.ToString(reader["FLTCUSIP"]);
                        details.LIPPERCUSIP = Convert.ToString(reader["LIPPERCUSIP"]);
                        details.EOnlineCUSIP = Convert.ToString(reader["EOnlineCUSIP"]);
                        details.CalculatedCUSIP = UtilityFactory.GenerateCUSIPWithCheckDigit(details.FLTCUSIP);

                        bCSTRPReportRPMissingCUSIPData.Add(details);
                    }
                }

                bCSTRPReportData.BCSTRPReportRPCUSIPMissingData = bCSTRPReportRPMissingCUSIPData;

            }
            return bCSTRPReportData;
        }
        #endregion

        #region BCS Report DashBorad

        #region BCSReports
        public List<BCSReports> GetBCSReports(string clientName, string reportType, DateTime? startDate, DateTime? endDate, int startindex, int endindex, string sortcolumn, string sortdirection, out int virtualCount)
        {
            List<BCSReports> bcsReportData = new List<BCSReports>();
            string spGetBCSReports = string.Empty;
            int count = 0;
            switch (clientName)
            {
                case "Transamerica": spGetBCSReports = SPGetBCSTransamericaReports; break;
                case "AllianceBernstein": spGetBCSReports = SPGetBCSAllianceBernsteinReports; break;
            }
            using (IDataReader reader = this.dataAccess.ExecuteReader
             (
                  this.DB1029ConnectionString,
                  spGetBCSReports,
                  this.dataAccess.CreateParameter(DBCReportType, SqlDbType.NVarChar, reportType),
                  this.dataAccess.CreateParameter(DBCStartDate, SqlDbType.DateTime, startDate),
                  this.dataAccess.CreateParameter(DBCEndDate, SqlDbType.DateTime, endDate),
                  this.dataAccess.CreateParameter(DBCEndIndex, SqlDbType.Int, endindex),
                  this.dataAccess.CreateParameter(DBCStartIndex, SqlDbType.Int, startindex),
                  this.dataAccess.CreateParameter(DBCSortColumn, SqlDbType.NVarChar, sortcolumn),
                  this.dataAccess.CreateParameter(DBCSortDirection, SqlDbType.NVarChar, sortdirection),
                  this.dataAccess.CreateParameter(DBCCount, SqlDbType.Int, count, ParameterDirection.Output)

              ))
            {

                BCSReports bcsReport;
                while (reader.Read())
                {
                    bcsReport = new BCSReports();
                    if (reportType == "CUSIP Not Present in RP")
                    {

                        bcsReport.CUSIP_WL = reader["CUSIP_WL"].ToString();
                        bcsReport.FundName_WL = reader["FundName_WL"].ToString();
                        bcsReport.Class_WL = reader["Class_WL"].ToString();
                    }
                    else
                    {
                        bcsReport.Class_WL = reader["Class_WL"].ToString();
                        bcsReport.Class_RP = reader["Class_RP"].ToString();
                        bcsReport.CUSIP_WL = reader["CUSIP_WL"].ToString();
                        bcsReport.CUSIP_RP = reader["CUSIP_RP"].ToString();
                        bcsReport.FundName_WL = reader["FundName_WL"].ToString();
                        bcsReport.FundName_RP = reader["FundName_RP"].ToString();
                        bcsReport.Status = reader["Status"].ToString();
                        bcsReport.DateModified = Convert.ToDateTime(reader["DateModified"]);
                    }


                    bcsReportData.Add(bcsReport);
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

            return bcsReportData;
        }

        public List<SLINKReportDetails> GetSLinkReportDetails(string clientName, string selectedStatus, DateTime? selectedDate, string docID, string sortColumn, string sortDirection, int startIndex, int endIndex, out int virtualCount, out object countDetails)
        {
            DataTable DocumentIds = new DataTable();
            DocumentIds.Columns.Add("DocumentId", typeof(string));

            if (!string.IsNullOrWhiteSpace(docID))
            {
                docID = docID.Replace("\r", "").Replace("\t", "");
                string[] docIdDeliminator = new string[] { "\n" };
                string[] lstDocID = docID.Split(docIdDeliminator, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in lstDocID)
                {
                    DocumentIds.Rows.Add(item.Trim());
                }
            }
            
            List<SLINKReportDetails> linkReportData = new List<SLINKReportDetails>();
            string spGetLInkReportDetails = "BCS_" + clientName + "GetSLINKReportDetails";
            if (clientName == "GMS")
            {
                spGetLInkReportDetails = "BCS_GIMGetSLINKReportDetails";
            }

            using (IDataReader reader = this.dataAccess.ExecuteReader
            (
                 this.DB1029ConnectionString,
                 spGetLInkReportDetails,
                 this.dataAccess.CreateParameter(DBCDocumentIDs, SqlDbType.Structured, DocumentIds),
                 this.dataAccess.CreateParameter(DBCSelectedDate, SqlDbType.DateTime, selectedDate),
                 this.dataAccess.CreateParameter(DBCStatus, SqlDbType.NVarChar, selectedStatus),
                 this.dataAccess.CreateParameter(DBCSortColumn, SqlDbType.NVarChar, sortColumn),
                 this.dataAccess.CreateParameter(DBCSortDirection, SqlDbType.NVarChar, sortDirection),
                 this.dataAccess.CreateParameter(DBCStartIndex, SqlDbType.Int, startIndex),
                 this.dataAccess.CreateParameter(DBCEndIndex, SqlDbType.Int, endIndex)
             ))
            {

                SLINKReportDetails linkReport;
                while (reader.Read())
                {
                    linkReport = new SLINKReportDetails();

                    linkReport.SLINKFileName = reader["SLINKFileName"].ToString();
                    linkReport.ZipFilePath = reader["ZipFileName"].ToString();
                    linkReport.ZipFileName = Path.GetFileName(linkReport.ZipFilePath);

                    linkReport.ZipFilePath = linkReport.ZipFilePath.Replace(ConfigValues.RPSourceURLReplace, ConfigValues.RPDestinationSANReplace).Replace(@"\", "/");

                    linkReport.Status = reader["Status"].ToString();
                    linkReport.ReceivedDate = Convert.ToDateTime(reader["ReceivedDate"]);
                    linkReportData.Add(linkReport);
                }
                virtualCount = 0;
                countDetails = null;
                if (reader.NextResult())
                {

                    if (reader.Read())
                    {
                        virtualCount = Convert.ToInt32(reader["virtualcount"]);

                        countDetails = new
                        {
                            TotalCount = Convert.ToInt32(reader["virtualcount"]),
                            ExCount = Convert.ToInt32(reader["ExCount"]),
                            APCount = Convert.ToInt32(reader["APCount"]),
                            OPCount = Convert.ToInt32(reader["OPCount"]),
                            APCCount = Convert.ToInt32(reader["APCCount"]),
                            OPCCount = Convert.ToInt32(reader["OPCCount"])
                        };
                    }
                }
            }

            return linkReportData;

        }

        #endregion

        #endregion

        #region BCSGatewayDocUpdate
        public BCSGatewayDocUpdateData GetDocUpdateHedaerData(string dirPath)
        {
            string[] lines = System.IO.File.ReadAllLines(dirPath);
            string header = lines[0];
            string[] headerData = header.Split('|');

            BCSGatewayDocUpdateData objDocUpdateHeder = new BCSGatewayDocUpdateData()
            {
                HeaderRecordType = headerData[0],
                HeaderDataType = headerData[1],
                HeaderSystem = headerData[2],
                HeaderFileName = headerData[3],
                HeaderDateTime = headerData[4],
                HeaderTotalRecordCount = Convert.ToInt32(headerData[5]),
                HeaderFLRecordCount = Convert.ToInt32(headerData[6]),
                HeaderEXRecordCount = Convert.ToInt32(headerData[7]),
                HeaderAPRecordCount = Convert.ToInt32(headerData[8]),
                HeaderOPRecordCount = Convert.ToInt32(headerData[9]),
                HeaderAPCRecordCount = Convert.ToInt32(headerData[10])
            };
            return objDocUpdateHeder;
        }
        #endregion

        public List<SANFileDetails> GetSANFileDetails(string DocumentPath, DateTime? date, string filter)
        {
            //date = DateTime.Now.AddDays(-1);
            List<SANFileDetails> fileData = new List<SANFileDetails>();

            var files = Directory.EnumerateFiles(DocumentPath, filter, SearchOption.TopDirectoryOnly)
                .Select(fn => new FileInfo(fn));

            var fileInfo = files.Where(fn => fn.LastWriteTime.ToShortDateString() == date.Value.ToShortDateString());

            foreach (var file in fileInfo)
            {
                SANFileDetails fileDetails = new SANFileDetails();
                fileDetails.DirectoryName = file.FullName;
                fileDetails.FileName = file.Name;
                fileDetails.ReceivedTime = file.LastWriteTime;

                fileData.Add(fileDetails);
            }
            return fileData;
        }
        #region missing cusip
        public List<BCSTRPReportRPCUSIPMissingData> GetMissingCUSIP(string company, int startIndex, int endIndex, string sortColumn, string sortDirection, out int virtualCount)
        {
            List<BCSTRPReportRPCUSIPMissingData> bCSRPMissingCUSIPData = new List<BCSTRPReportRPCUSIPMissingData>();

            using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.DB1029ConnectionString,
                    SPGBCSGetMissingCUSIP,
                    this.dataAccess.CreateParameter(DBCompany, SqlDbType.NVarChar, company),
                    this.dataAccess.CreateParameter(DBCStartIndex, SqlDbType.Int, startIndex),
                    this.dataAccess.CreateParameter(DBCEndIndex, SqlDbType.Int, endIndex),
                    
                    this.dataAccess.CreateParameter(DBCSortColumn, SqlDbType.NVarChar, sortColumn),
                    this.dataAccess.CreateParameter(DBCSortDirection, SqlDbType.NVarChar, sortDirection)
                   

                ))
            { 

                while (reader.Read())
                {
                    BCSTRPReportRPCUSIPMissingData details = new BCSTRPReportRPCUSIPMissingData();
                    details.CompanyName = Convert.ToString(reader["CompanyName"]);
                    details.FundName = Convert.ToString(reader["FundName"]);
                    details.FLTCUSIP = Convert.ToString(reader["FLTCUSIP"]);
                    details.LIPPERCUSIP = Convert.ToString(reader["LIPPERCUSIP"]);
                    details.EOnlineCUSIP = Convert.ToString(reader["EOnlineCUSIP"]);
                    details.CalculatedCUSIP = UtilityFactory.GenerateCUSIPWithCheckDigit(details.FLTCUSIP);
                   
                    bCSRPMissingCUSIPData.Add(details);
                }
                virtualCount = 0;
                if(reader.NextResult())
                {
                    if(reader.Read())
                    {
                        virtualCount = Convert.ToInt32(reader["virtualCount"]);
                    }
                }
            }                   
            return bCSRPMissingCUSIPData;
        }
        #endregion
    }
}
