// ***********************************************************************
// Assembly         : RRD.FSG.RP.Scheduler
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-09-2015
// ***********************************************************************
using RP.Utilities;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace RRD.FSG.RP.Scheduler.Task
{
    /// <summary>
    /// Class MonthlyReportGenerator.
    /// </summary>
    public class MonthlyReportGenerator
    {

        //TODO: Constructor to initialize ReportFactory instance
        //Set BaseFactory.ClientName to client name in clientobjectmodel, this makes sure appropriate client connection string is fetched

        /// <summary>
        /// The site activity factory
        /// </summary>
        private IFactory<SiteActivityObjectModel, int> siteActivityFactory;
        /// <summary>
        /// The document types
        /// </summary>
        string[] docTypes = { "SP", "P", "S", "AR", "SAR", "XBRL" };

        /// <summary>
        /// Initializes a new instance of the <see cref="MonthlyReportGenerator" /> class.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        public MonthlyReportGenerator(string clientName)
        {
            siteActivityFactory = RPV2Resolver.Resolve<IFactory<SiteActivityObjectModel, int>>("SiteActivity");
            siteActivityFactory.ClientName = clientName;
           
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="MonthlyReportGenerator"/> class.
        /// </summary>
        /// <param name="SiteActivityFactory">The site activity factory.</param>
        public MonthlyReportGenerator(IFactory<SiteActivityObjectModel, int> SiteActivityFactory)
        {
            siteActivityFactory = SiteActivityFactory;
        }
        /// <summary>
        /// Gets the monthly reportby date.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns>IEnumerable&lt;SiteActivityObjectModel&gt;.</returns>
        public IEnumerable<SiteActivityObjectModel> GetMonthlyReportbyDate(DateTime fromDate , DateTime toDate)
        {
            return siteActivityFactory.GetEntitiesBySearch(new SiteActivitySearchDetail() { DateFrom = fromDate, DateTo = toDate });
        }


        /// <summary>
        /// Generates the report.
        /// </summary>
        /// <param name="reportContentObjectModel">The report content object model.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GenerateReport(ReportContentObjectModel reportContentObjectModel)
        {
            var reportData = GetMonthlyReportbyDate(reportContentObjectModel.ReportFromDate,reportContentObjectModel.ReportToDate).ToList();
            DataSet ds = new DataSet();
            ds.Tables.Add(PrepareMonthlyDataset(reportData,reportContentObjectModel.FrequencyType,reportContentObjectModel.ReportFromDate, reportContentObjectModel.ReportToDate));
            CreateExcelFile.CreateExcelDocument(ds, reportContentObjectModel.FileName);
            return File.ReadAllBytes(reportContentObjectModel.FileName);
        }


        /// <summary>
        /// Prepares the summary Monthly dataset.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="frequencyType">Type of the frequency.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns>DataTable.</returns>
        public DataTable PrepareMonthlyDataset(IEnumerable<SiteActivityObjectModel> list,string frequencyType, DateTime from, DateTime to)
        {           
            DataTable table = new DataTable();
            int totalCount = 0;
            table.TableName = "Summary and Activity";
            for (int i = 0; i < docTypes.Length; i++)
            {
                table.Columns.Add("#" + docTypes[i]);
            }
            table.Columns.Add("#TrackID", typeof(string));
            table.Columns.Add("#Referer", typeof(string));
            table.Columns.Add("#Fund", typeof(string));
            table.Columns.Add("#Date/Time", typeof(string));
            table.Columns.Add("#IP Address", typeof(string));
            table.Columns.Add("#Document ", typeof(string));
            table.Columns.Add("#Clicks", typeof(string));
            table.Columns.Add("#Initial Document", typeof(string));
            table.Columns.Add("#Source Site", typeof(string));
            if (list.Count() > 0)
            {
                table.Rows.Add(siteActivityFactory.ClientName + "_" + frequencyType + "_ " + from + "-" + to);
            }
           
            table.Rows.Add("", "");
            for (int i = 0; i < docTypes.Length; i++)
            {
                var objList = list.Where(o => o.DocumentTypeMarketId.Equals(docTypes[i]));
                if (objList.Count() > 0)
                {
                    int count = objList.Count();
                    table.Rows.Add(objList.FirstOrDefault().DocumentType, count.ToString());

                    totalCount = totalCount + count;
                }
            }
            table.Rows.Add("Total number of clicks", totalCount);
            table.Rows.Add("Clicks per initial document", list.Count(sp => sp.InitDoc == true).ToString());
            table.Rows.Add("", "");
            table.Rows.Add("", "");
            Guid batchId = new Guid();
            foreach (var param in list)
            {
                if (batchId != param.RequestBatchId)
                {
                    table.Rows.Add("Track ID:", param.RequestBatchId, "Source Site:", param.SiteName == null ? "N/A" : param.SiteName, "Fund", "Date/Time", "IP Address", "Document", "Clicks", "Initial Document");
                }
                table.Rows.Add("", "", "", "", param.Name, param.RequestUtcDate, param.ClientIPAddress, param.DocumentType, param.Click, param.InitDoc);
                batchId = param.RequestBatchId;
            }
            return table;
        }
      
    }
}
