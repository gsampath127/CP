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
        public BillingReportModel GetBillingReport(DateTime fromDate, DateTime toDate,string clientName)
        {
            return hostedClientPagescenariosFactory.GetTaxonomyAssociationforBillingreport(clientName, fromDate, toDate);
        }
        /// <summary>
        /// Method that calls excel generator
        /// </summary>
        /// <param name="reportContentObjectModel"></param>
        /// <param name="clientName"></param>
        /// <returns></returns>
        public byte[] GenerateReport(ReportContentObjectModel reportContentObjectModel,string clientName)
        {
            BillingReportModel billingReportData = GetBillingReport(reportContentObjectModel.ReportFromDate, reportContentObjectModel.ReportToDate,clientName);
            DataTable finalReport=PrepareBillingreportData(billingReportData, reportContentObjectModel.FrequencyType, reportContentObjectModel.ReportFromDate, reportContentObjectModel.ReportToDate,clientName);
            //BillingReportModel billingReportData = GetBillingReport(DateTime.Now.AddDays(-3), DateTime.Now, clientName);
            //DataTable finalReport=PrepareBillingreportData(billingReportData, "4", DateTime.Now, DateTime.Now,clientName);
            CreateExcelFile.CreateExcelDocument(finalReport, reportContentObjectModel.FileName,true);
            return File.ReadAllBytes(reportContentObjectModel.FileName);
        }

       /// <summary>
       /// Method for organizing data 
       /// </summary>
       /// <param name="dataSet"></param>
       /// <param name="frequencyType"></param>
       /// <param name="from"></param>
       /// <param name="to"></param>
       /// <param name="clientName"></param>
       /// <returns></returns>
       public DataTable PrepareBillingreportData(BillingReportModel billingReport,string frequencyType, DateTime from, DateTime to,string clientName)
        {
            DataTable table = new DataTable();
            table.Columns.Add("#c1", typeof(string));
            table.Columns.Add("#c2", typeof(string));
            table.Columns.Add("#c3", typeof(string));
            table.Columns.Add("#c4", typeof(string));
            table.Columns.Add("#c5", typeof(string));
         
            table.Rows.Add("AsOf", from);
            table.Rows.Add("ClientName",clientName);
            table.Rows.Add("Total Number of Active Funds", billingReport.ActiveFundCount);
            table.Rows.Add("Total Number of Summary Prospectus", billingReport.SummaryProspectusCount);
            table.Rows.Add("Total Number of Funds Removed", billingReport.RemovedFundCount);
            table.Rows.Add("Total Number of New Funds", billingReport.NewFundCount);
            table.Rows.Add("", "");

            table.Rows.Add("SeriesId", "CUSIPs", "List of Funds/Series", "Summary Prospectus", "Group/SiteName");
            foreach (BillingReportActiveFundDetails obj in billingReport.LstBillingReportActiveFundDetails)
            {
                table.Rows.Add(obj.SeriesID, obj.MarketID, obj.FundName, obj.IsSummaryProspectus, obj.SiteName);
            }
            table.Rows.Add("", "");
            table.Rows.Add("", "");
            table.Rows.Add("Removed CUSIPs", "Date");
            foreach (BillingReportRemovedFundDetails obj in billingReport.LstBillingReportRemovedFundDetails)
            {
                table.Rows.Add(obj.MarketID,obj.removedDate);
            }

            return table;
        }

    }
}
