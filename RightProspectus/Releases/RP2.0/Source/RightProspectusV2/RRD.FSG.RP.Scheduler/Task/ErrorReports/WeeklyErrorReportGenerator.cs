// ***********************************************************************
// Assembly         : RRD.FSG.RP.Scheduler
// Author           : 
// Created          : 11-09-2015
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

namespace RRD.FSG.RP.Scheduler.Task.Reports
{
    /// <summary>
    /// Class WeeklyErrorReportGenerator.
    /// </summary>
    public class WeeklyErrorReportGenerator
    {

        /// <summary>
        /// The Bad request factory
        /// </summary>
        private IFactory<SiteActivityObjectModel, int> badRequestFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonthlyErrorReportGenerator" /> class.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        public WeeklyErrorReportGenerator(string clientName)
        {
            badRequestFactory = RPV2Resolver.Resolve<IFactory<SiteActivityObjectModel, int>>("BadRequest");
            badRequestFactory.ClientName = clientName;
           
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MonthlyErrorReportGenerator" /> class.
        /// </summary>
        /// <param name="BadRequestFactory">The bad request factory.</param>
        /// <param name="clientName">Name of the client.</param>
        public WeeklyErrorReportGenerator(IFactory<SiteActivityObjectModel, int> BadRequestFactory,string clientName)
        {
            badRequestFactory = BadRequestFactory;
            badRequestFactory.ClientName = clientName;
    
        }
        /// <summary>
        /// Gets the weekly report by date.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns>IEnumerable&lt;SiteActivityObjectModel&gt;.</returns>
        public IEnumerable<SiteActivityObjectModel> GetWeeklyErrorReportbyDate(DateTime fromDate, DateTime toDate)
        {
            return badRequestFactory.GetEntitiesBySearch(new SiteActivitySearchDetail() { DateFrom = fromDate, DateTo = toDate });
        }

        /// <summary>
        /// Generates the report.
        /// </summary>
        /// <param name="reportContentObjectModel">The report content object model.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GenerateReport(ReportContentObjectModel reportContentObjectModel)
        {
            var reportData = GetWeeklyErrorReportbyDate(reportContentObjectModel.ReportFromDate, reportContentObjectModel.ReportToDate).ToList();
            DataSet ds = new DataSet();
            ds.Tables.Add(PrepareWeeklyErrorDataset(reportData, reportContentObjectModel.ReportFromDate, reportContentObjectModel.ReportToDate));
            CreateExcelFile.CreateExcelDocument(ds, reportContentObjectModel.FileName);
            return File.ReadAllBytes(reportContentObjectModel.FileName);
        }

        /// <summary>
        /// Prepares the weekly error dataset.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns>DataTable.</returns>
        public DataTable PrepareWeeklyErrorDataset(IEnumerable<SiteActivityObjectModel> list, DateTime from, DateTime to)
        {
            DataTable table = new DataTable();
         
            table.TableName = "Error Report";

            table.Columns.Add("#Reference ID", typeof(string));
            table.Columns.Add("#Message", typeof(string));
            table.Columns.Add("#Error Item", typeof(string));
            table.Columns.Add("#Date/Time", typeof(string));
            table.Columns.Add("#IP Address", typeof(string));
            table.Columns.Add("#UserAgent ", typeof(string));
            table.Columns.Add("#Referer", typeof(string));
            if (list.Count() > 0)
            {
                table.Rows.Add(badRequestFactory.ClientName + "_Weekly_ " + from + "-" + to);
            }
            table.Rows.Add("", "");
      
            table.Rows.Add("Reference ID:", "Message", "Error Item", "Date/Time", "IP Address", "UserAgent", "Referer");
            
            foreach (var param in list)
            {
               
                table.Rows.Add( param.SiteActivityId, param.BadRequestIssueDescription,param.BadRequestParameterValue, param.RequestUtcDate, param.ClientIPAddress, param.UserAgentString,param.ReferrerUriString);
                
            }
            return table;
        }

    }
}
