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
    /// Class QuarterlyReportGenerator.
    /// </summary>
    public class QuarterlyReportGenerator
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
        /// Initializes a new instance of the <see cref="QuarterlyReportGenerator" /> class.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        public QuarterlyReportGenerator(string clientName)
        {
            siteActivityFactory = RPV2Resolver.Resolve<IFactory<SiteActivityObjectModel, int>>("SiteActivity");
            siteActivityFactory.ClientName = clientName;

        }

        /// <summary>
        /// Parameterized Constructor for Testing
        /// </summary>
        /// <param name="SiteActivityFactory">The site activity factory.</param>
        public QuarterlyReportGenerator(IFactory<SiteActivityObjectModel, int> SiteActivityFactory)
        {
            siteActivityFactory = SiteActivityFactory;
        
        }

        /// <summary>
        /// Gets the Quarterly report by date.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns>IEnumerable&lt;SiteActivityObjectModel&gt;.</returns>
        public IEnumerable<SiteActivityObjectModel> GetQuarterlyReportbyDate(DateTime fromDate , DateTime toDate)
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
            var reportData = GetQuarterlyReportbyDate(reportContentObjectModel.ReportFromDate,reportContentObjectModel.ReportToDate).ToList();
            DataSet ds = new DataSet();
            ds.Tables.Add(PrepareSummaryQuarterlyDataset(reportData, reportContentObjectModel.ReportFromDate, reportContentObjectModel.ReportToDate));
            ds.Tables.Add(PrepareQuarterlyDataset(reportData));
            CreateExcelFile.CreateExcelDocument(ds, reportContentObjectModel.FileName);
            return File.ReadAllBytes(reportContentObjectModel.FileName);
        }


        /// <summary>
        /// Prepares the summary weekly dataset.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns>DataTable.</returns>
        public DataTable PrepareSummaryQuarterlyDataset(IEnumerable<SiteActivityObjectModel> list, DateTime from, DateTime to)
        {
            DataTable table = new DataTable();
            int totalCount = 0;
            table.TableName = "Document Activity";
            for (int i = 0; i < docTypes.Length; i++)
            {
                table.Columns.Add("#" + docTypes[i]);
            } 
            if (list.Count() > 0)
            {
                table.Rows.Add(siteActivityFactory.ClientName + "_Quarterly_Report_Q" + (int)Math.Ceiling((double)to.AddMonths(-3).Month / (double)3));
            }
            
            table.Rows.Add("", "");
            for (int i = 0; i < docTypes.Length; i++)
            {

                var objList = list.Where(o => o.DocumentTypeMarketId.Equals(docTypes[i]));
                if (objList.Count() > 0)
                {
                    table.Rows.Add(objList.FirstOrDefault().DocumentType, objList.Count().ToString());

                    totalCount = totalCount + objList.Count();
                }
            }
            table.Rows.Add("Total number of clicks", totalCount);
            table.Rows.Add("Clicks per initial document", list.Count(sp => sp.InitDoc == true).ToString());
            table.Rows.Add("", "");
            table.Rows.Add("", "");
            return table;
        }

        /// <summary>
        /// Prepares the Quarterly dataset.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>DataTable.</returns>
        public DataTable PrepareQuarterlyDataset(IEnumerable<SiteActivityObjectModel> list)
        {
            DataTable table = new DataTable();
            table.TableName = "Details";
            table.Columns.Add("TrackID", typeof(string));
            table.Columns.Add("Referer", typeof(string));
            table.Columns.Add("Fund", typeof(string));
            table.Columns.Add("Date/Time", typeof(string));
            table.Columns.Add("IP Address", typeof(string));
            table.Columns.Add("Document", typeof(string));
            table.Columns.Add("Clicks", typeof(string));
            table.Columns.Add("Init Doc", typeof(string));
            table.Columns.Add("Source Site", typeof(string));
            foreach (var param in list)
            {
                table.Rows.Add(param.RequestBatchId, param.ReferrerUriString, param.Name, param.RequestUtcDate, param.ClientIPAddress, param.DocumentType, param.Click, param.InitDoc, param.SiteName);
            }
            return table;
        }
    }
   
}
