using RP.Utilities;
using RRD.FSG.RP.Model.Entities;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.HostedPages;
using RRD.FSG.RP.Model.Factories.HostedPages;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Model.Interfaces.HostedPages;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Scheduler.Task.BillingReports
{
    /// <summary>
    /// Class for monthly billing report
    /// </summary>
    public class MonthlyBillingReportGenerator
    {
        /// <summary>
        /// object for IHostedClientPageScenariosFactory
        /// </summary>
        private IHostedClientPageScenariosFactory hostedClientPagescenariosFactory;
        /// <summary>
        /// Constructor for billing report
        /// </summary>
        /// <param name="clientName"></param>
        public MonthlyBillingReportGenerator(string clientName)
        {
            hostedClientPagescenariosFactory = RPV2Resolver.Resolve<IHostedClientPageScenariosFactory>();
        }
        /// <summary>
        /// Method that fetches data from factory
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="clientName"></param>
        /// <returns></returns>
        public BillingReportModel GetBillingReport(DateTime fromDate, DateTime toDate, string clientName)
        {
            return hostedClientPagescenariosFactory.GetTaxonomyAssociationforBillingreport(clientName, fromDate, toDate);
        }
        /// <summary>
        /// Method that calls excel generator
        /// </summary>
        /// <param name="reportContentObjectModel"></param>
        /// <param name="clientName"></param>
        /// <returns></returns>
        public byte[] GenerateReport(ReportContentObjectModel reportContentObjectModel, string clientName)
        {
            BillingReportModel billingReportData = GetBillingReport(reportContentObjectModel.ReportFromDate, reportContentObjectModel.ReportToDate, clientName);
            DataSet dsReport = new DataSet();
            int sheetnum = 0;
            for (int i = 1; i <= 4; i++)
            {
                sheetnum = i;
                DataTable finalReport = new DataTable();
                finalReport = PrepareBillingreportData(billingReportData, reportContentObjectModel.FrequencyType, reportContentObjectModel.ReportFromDate, reportContentObjectModel.ReportToDate, clientName, sheetnum);
                dsReport.Tables.Add(finalReport);
            }

            CreateExcelFile.CreateExcelDocument(dsReport, reportContentObjectModel.FileName, true);
            return File.ReadAllBytes(reportContentObjectModel.FileName);
        }

        /// <summary>
        /// Method for organizing data 
        /// </summary>
        /// <param name="billingReport"></param>
        /// <param name="frequencyType"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="clientName"></param>
        /// <param name="sheetNumber"></param>
        /// <returns></returns>
        public DataTable PrepareBillingreportData(BillingReportModel billingReport, string frequencyType, DateTime from, DateTime to, string clientName, int sheetNumber)
        {
            from = TimeZoneInfo.ConvertTimeFromUtc(from, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));//Converting to EST. 
            to = TimeZoneInfo.ConvertTimeFromUtc(to, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));//Converting to EST. 

            DataTable table = new DataTable();
            switch (sheetNumber)
            {
                case 1:
                    table.TableName = "Summary";
                    break;
                case 2:
                    table.TableName = "Super Set";
                    break;
                case 3:
                    table.TableName = "Unique Series ID";
                    break;
                case 4:
                    table.TableName = "Unique CUSIP";
                    break;
                default:
                    table.TableName = string.Empty;
                    break;
            }

            table.Columns.Add("#c1", typeof(string));
            table.Columns.Add("#c2", typeof(string));
            table.Columns.Add("#c3", typeof(string));
            table.Columns.Add("#c4", typeof(string));
            table.Columns.Add("#c5", typeof(string));
            if (sheetNumber != 1)
            {              
                table.Columns.Add("#c6", typeof(string));
                table.Columns.Add("#c7", typeof(string));
                table.Columns.Add("#c8", typeof(string));
                table.Columns.Add("#c9", typeof(string));
                table.Columns.Add("#c10", typeof(string));
                table.Columns.Add("#c11", typeof(string));
                table.Columns.Add("#c12", typeof(string));
                table.Columns.Add("#c13", typeof(string));
                table.Columns.Add("#c14", typeof(string));
                table.Columns.Add("#c15", typeof(string));
                table.Columns.Add("#c16", typeof(string));
                table.Columns.Add("#c17", typeof(string));
                table.Columns.Add("#c18", typeof(string));
                table.Columns.Add("#c19", typeof(string));
                table.Columns.Add("#c20", typeof(string));
                table.Columns.Add("#c21", typeof(string));
                table.Columns.Add("#c22", typeof(string));
                table.Columns.Add("#c23", typeof(string));
                table.Columns.Add("#c24", typeof(string));
                table.Columns.Add("#c25", typeof(string));
                table.Columns.Add("#c26", typeof(string));
                table.Columns.Add("#c27", typeof(string));
            }

            if (sheetNumber == 1)
            {

                table.Rows.Add("NOTE: DATA IS CAPTURED AT THE CUSTOMER LEVEL", "", "", "", "");
                table.Rows.Add("", "", "", "", ""); 
                table.Rows.Add("Report Summary","","","","Legend");
                table.Rows.Add("Billing Period", from + " to " + to,"","","NAC - Not Applicable for Customer");
                table.Rows.Add("Client/Customer Name", clientName,"","","Y - Document Available");
                table.Rows.Add("Total Number of CUSIPs", billingReport.ActiveFundCount,"","","N- Document Not Available");
                table.Rows.Add("Total Number of Summary Prospectus", billingReport.SummaryProspectusCount,"","","NA - Not Available");
                table.Rows.Add("Total Number of Funds Removed", billingReport.RemovedFundCount,"","","SP - Summary Prospectus");
                table.Rows.Add("Total Number of Funds Added/new", billingReport.NewFundCount,"","","P   - Statutory Prospectus");
                table.Rows.Add("Total Number of Unique Series Ids", billingReport.UniqueSeriesIdCount,"","","S- Statement Of Additional Information");
                table.Rows.Add("Total Number of Unique CUSIPs", billingReport.UniqueCUSIPcount,"","","AR - Annual Report");
                table.Rows.Add("Total Number of Unique PDF Names/Summary Prospectus", billingReport.UniqueSummaryProspectusPdfNamesCount,"","","SAR - Semi - Annual Report");
                table.Rows.Add("Total Number of Unique PDF Names/Prospectus", billingReport.UniqueProspectusPdfNamesCount, "", "", "PVR - Proxy Voting Record");
                table.Rows.Add("Total Number of Unique PDF Names/SAI", billingReport.UniqueSAIPdfNamesCount, "", "", "FS - Factsheet");
                table.Rows.Add("Total Number of Unique PDF Names/Annual Report", billingReport.UniqueAnnualReportPdfNamesCount);
                table.Rows.Add("Total Number of Unique PDF Names/Semi-Annual Report", billingReport.UniqueSemiAnnualReportPdfNamesCount);
                if (billingReport.IsPVREnabled)
                {
                    table.Rows.Add("Total Number of Unique PDF Names/Proxy Voting Record", billingReport.UniquePVRPdfNamesCount);
                }
                if (billingReport.IsFSEnabled)
                {
                    table.Rows.Add("Total Number of Unique PDF Names/Fact Sheet", billingReport.UniqueFSPdfNamesCount);
                }
                table.Rows.Add("Total Number of Updated PDFs/Summary Prospectus", billingReport.SPDocumentUpdatedCount);
                table.Rows.Add("Total Number of Updated PDFs/Prospectus", billingReport.PDocumentUpdatedCount);
                table.Rows.Add("Total Number of Updated PDFs/SAI", billingReport.SAIDocumentUpdatedCount);
                table.Rows.Add("Total Number of Updated PDFs/Annual Report", billingReport.ARDocumentUpdatedCount);
                table.Rows.Add("Total Number of Updated PDFs/Semi-Annual Report", billingReport.SARDocumentUpdatedCount);
                if (billingReport.IsPVREnabled)
                {
                    table.Rows.Add("Total Number of Updated PDFs/Proxy Voting Record", billingReport.PVRDocumentUpdatedCount);
                }
                if (billingReport.IsFSEnabled)
                {
                    table.Rows.Add("Total Number of Updated PDFs/Fact Sheet", billingReport.FSDocumentUpdatedCount);
                }

            }

            if (sheetNumber == 2)  //"Super Set"
            {
                List<string> lstColumnNames = new List<string>() { "Group/SiteName", "SeriesId", "CUSIPs", "Fundname", "SEC Fund Name (Based on series ID)","SP?","SP Updated?",
                    "P?","P Updated?","SAI?","SAI Updated?","AR?","AR Updated?","SAR?","SAR Updated?",
                    "SP PDF Url","P PDF Url","S PDF Url","AR PDF Url","SAR PDF Url"};

                if (billingReport.IsPVREnabled)
                {
                    lstColumnNames.Add("PVR?");
                    lstColumnNames.Add("PVR Updated?");
                    lstColumnNames.Add("PVR PDF Url");
                }
                if (billingReport.IsFSEnabled)
                {
                    lstColumnNames.Add("FS?");
                    lstColumnNames.Add("FS Updated?");
                    lstColumnNames.Add("FS PDF Url");
                }
                if (billingReport.IsXBRLEnabled)
                {
                    lstColumnNames.Add("XBRL?");
                }

                table.Rows.Add(lstColumnNames.ToArray());
                foreach (BillingReportActiveFundDetails obj in billingReport.LstBillingReportActiveFundDetails)
                {
                    List<string> lstColumnValues = new List<string>() { obj.SiteName, obj.SeriesID, obj.MarketID, obj.FundName, obj.SECFundName, obj.IsSummaryProspectus,obj.SPDocumentUpdatedDate, 
                        obj.IsProspectus,obj.PDocumentUpdatedDate ,obj.IsSAI,obj.SAIDocumentUpdatedDate, obj.IsAnnualReport,obj.ARDocumentUpdatedDate, obj.IsSemiAnnualReport,obj.SARDocumentUpdatedDate,
                          obj.SPPDFName, obj.PPDFName, obj.SAIPDFName, obj.ARPDFName, obj.SARPDFName};

                    if (billingReport.IsPVREnabled)
                    {
                        lstColumnValues.Add(obj.IsPVR);
                        lstColumnValues.Add(obj.PVRDocumentUpdatedDate);
                        lstColumnValues.Add(obj.PVRPDFName);
                    }

                    if (billingReport.IsFSEnabled)
                    {
                        lstColumnValues.Add(obj.IsFactSheet);
                        lstColumnValues.Add(obj.FSDocumentUpdatedDate);
                        lstColumnValues.Add(obj.FactSheetPDFName);
                    }

                    if (billingReport.IsXBRLEnabled)
                    {
                        lstColumnValues.Add(obj.IsXBRL);
                    }

                    table.Rows.Add(lstColumnValues.ToArray());
                }

                table.Rows.Add("", "");
                table.Rows.Add("", "");
                table.Rows.Add("Removed CUSIPs", "Date");
                foreach (BillingReportRemovedFundDetails obj in billingReport.LstBillingReportRemovedFundDetails)
                {
                    table.Rows.Add(obj.MarketID, obj.removedDate);
                }
            }
            else if (sheetNumber == 3)  //"Unique Series ID"
            {
                List<string> lstColumnNames = new List<string>() {"SeriesId", "Fundname", "SEC Fund Name (Based on series ID)", "SP?","SP Updated ?", 
                    "P?","P Updated ?", "SAI?","SAI Updated?", "AR?","AR Updated?", "SAR?","SAR Updated ?",
                    "SP PDF Url", "P PDF Url", "S PDF Url", "AR PDF Url", "SAR PDF Url" };

                if (billingReport.IsPVREnabled)
                {
                    lstColumnNames.Add("PVR?");
                    lstColumnNames.Add("PVR Updated?");
                    lstColumnNames.Add("PVR PDF Url");
                }
                if (billingReport.IsFSEnabled)
                {
                    lstColumnNames.Add("FS?");
                    lstColumnNames.Add("FS Updated?");
                    lstColumnNames.Add("FS PDF Url");
                }
                if (billingReport.IsXBRLEnabled)
                {
                    lstColumnNames.Add("XBRL?");
                }

                table.Rows.Add(lstColumnNames.ToArray());

                foreach (BillingReportActiveFundDetails obj in billingReport.LstBillingReportActiveFundDetails.GroupBy(i => i.SeriesID,
                                                                                           (key, group) => group.First()).ToList()) // Group by SeriesID and take first record
                {
                    List<string> lstColumnValues = new List<string>() { obj.SeriesID,obj.FundName, obj.SECFundName, obj.IsSummaryProspectus,obj.SPDocumentUpdatedDate,
                        obj.IsProspectus,obj.PDocumentUpdatedDate, obj.IsSAI,obj.SAIDocumentUpdatedDate, obj.IsAnnualReport,obj.ARDocumentUpdatedDate, obj.IsSemiAnnualReport,obj.SARDocumentUpdatedDate,
                          obj.SPPDFName, obj.PPDFName, obj.SAIPDFName, obj.ARPDFName, obj.SARPDFName};

                    if (billingReport.IsPVREnabled)
                    {
                        lstColumnValues.Add(obj.IsPVR);
                        lstColumnValues.Add(obj.PVRDocumentUpdatedDate);
                        lstColumnValues.Add(obj.PVRPDFName);
                    }

                    if (billingReport.IsFSEnabled)
                    {
                        lstColumnValues.Add(obj.IsFactSheet);
                        lstColumnValues.Add(obj.FSDocumentUpdatedDate);
                        lstColumnValues.Add(obj.FactSheetPDFName);
                    }

                    if (billingReport.IsXBRLEnabled)
                    {
                        lstColumnValues.Add(obj.IsXBRL);
                    }

                    table.Rows.Add(lstColumnValues.ToArray());
                }
            }
            else if (sheetNumber == 4) //"Unique CUSIP"
            {

                List<string> lstColumnNames = new List<string>() {"SeriesId", "CUSIPs", "Fundname", "SEC Fund Name (Based on series ID)", "SP?","SP Updated?",
                    "P?","P Updated?", "SAI?","SAI Updated?", "AR?","AR Updated?", "SAR?","SAR Updated?", 
                    "SP PDF Url", "P PDF Url", "S PDF Url", "AR PDF Url", "SAR PDF Url" };

                if (billingReport.IsPVREnabled)
                {
                    lstColumnNames.Add("PVR?");
                    lstColumnNames.Add("PVR Updated?");
                    lstColumnNames.Add("PVR PDF Url");
                }
                if (billingReport.IsFSEnabled)
                {
                    lstColumnNames.Add("FS?");
                    lstColumnNames.Add("FS Updated?");
                    lstColumnNames.Add("FS PDF Url");
                }
                if (billingReport.IsXBRLEnabled)
                {
                    lstColumnNames.Add("XBRL?");
                }

                table.Rows.Add(lstColumnNames.ToArray());

                foreach (BillingReportActiveFundDetails obj in billingReport.LstBillingReportActiveFundDetails.GroupBy(i => i.MarketID,
                                                                                           (key, group) => group.First()).ToList()) // Group by MarketID and take first record
                {
                    List<string> lstColumnValues = new List<string>() {obj.SeriesID, obj.MarketID, obj.FundName, obj.SECFundName, obj.IsSummaryProspectus,obj.SPDocumentUpdatedDate,
                        obj.IsProspectus,obj.PDocumentUpdatedDate, obj.IsSAI,obj.SAIDocumentUpdatedDate, obj.IsAnnualReport,obj.ARDocumentUpdatedDate, obj.IsSemiAnnualReport,obj.SARDocumentUpdatedDate,
                        obj.SPPDFName, obj.PPDFName, obj.SAIPDFName, obj.ARPDFName, obj.SARPDFName };

                    if (billingReport.IsPVREnabled)
                    {
                        lstColumnValues.Add(obj.IsPVR);
                        lstColumnValues.Add(obj.PVRDocumentUpdatedDate);
                        lstColumnValues.Add(obj.PVRPDFName);
                    }

                    if (billingReport.IsFSEnabled)
                    {
                        lstColumnValues.Add(obj.IsFactSheet);
                        lstColumnValues.Add(obj.FSDocumentUpdatedDate);
                        lstColumnValues.Add(obj.FactSheetPDFName);
                    }

                    if (billingReport.IsXBRLEnabled)
                    {
                        lstColumnValues.Add(obj.IsXBRL);
                    }

                    table.Rows.Add(lstColumnValues.ToArray());
                }
            }
            return table;
        }

    }
}
