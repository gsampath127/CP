// ***********************************************************************
// Assembly         : RRD.FSG.RP.Scheduler
// Author           : 
// Created          : 10-27-2015
//
// Last Modified By : 
// Last Modified On : 11-12-2015
// ***********************************************************************
using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Enumerations;
using RRD.FSG.RP.Scheduler.Interfaces;
using RRD.FSG.RP.Scheduler.Task.PrintRequest;
using RRD.FSG.RP.Utilities;
using System;
using System.IO;

namespace RRD.FSG.RP.Scheduler.Task
{
    /// <summary>
    /// Class PrintRequestsTask.
    /// </summary>
    public class PrintRequestsTask : BaseTask
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrintRequestsTask"/> class.
        /// </summary>
        public PrintRequestsTask()
            : base()
        {

        }

        /// <summary>
        /// Process the task.
        /// </summary>
        /// <param name="entry">The entry.</param>
        public override void Process(IReportScheduleEntry entry)
        {
            try
            {
                ReportContentObjectModel reportContentObjectModel = new ReportContentObjectModel();
                reportContentObjectModel.ReportToDate = entry.UtcNextDataEndDate;
                reportContentObjectModel.Name = entry.ReportName;
                reportContentObjectModel.ReportScheduleId = entry.ReportScheduleId;

                this.GenerateReportByFrequencyType(reportContentObjectModel, entry);
                
                if (!string.IsNullOrWhiteSpace(reportContentObjectModel.FileName))
                {
                    ReportTransfer reportTransfer = new ReportTransfer();
                    reportTransfer.Process(reportContentObjectModel, entry);
                }
            }            
            catch (Exception exception)
            {
                try
                {
                    // any errors rendering the publication should be flagged and sent back as failed
                    entry.Status = (int)ScheduleStatus.Failure;
                    SendErrorEmail(entry);
                    ExceptionHandler.HandleWorkerServiceProcessException(exception);
                }
                catch
                {
                    // if the error can't be logged, then don't crash the service
                }
            }

        }

        private void SendErrorEmail(IReportScheduleEntry entry)
        {
            if (!(string.IsNullOrWhiteSpace(entry.ErrorEmailSub) || string.IsNullOrWhiteSpace(entry.ErrorEmailSub)))
            {
                string errorEmailTemplate = "";
                if (!string.IsNullOrWhiteSpace(entry.ErrorEmailTemplate))
                {
                    errorEmailTemplate = entry.ErrorEmailTemplate.Replace("<<clientname>>", entry.ClientName).Replace("<<errordate>>", DateTime.Now.AddHours(1).ToString());
                }

                string errorEmailSub = entry.ErrorEmailSub.Replace("<<clientname>>", entry.ClientName);
                EmailHelper.SendEmail(ConfigValues.ReportContentEmailFrom.ToString(), entry.ErrorEmail, errorEmailSub, errorEmailTemplate, "", "", null, "Support", null);
            }
        }

        /// <summary>
        /// Generates the type of the report by frequency.
        /// </summary>
        /// <param name="reportContentObjectModel">The report content object model.</param>
        /// <param name="entry">The entry.</param>
        public void GenerateReportByFrequencyType(ReportContentObjectModel reportContentObjectModel, IReportScheduleEntry entry)
        {
            //var reportRunDate = reportContentObjectModel.ReportToDate;
            switch ((FrequencyType)entry.FrequencyType)
            {
                case FrequencyType.RunOnce:
                    reportContentObjectModel.ReportFromDate = entry.UtcDataStartDate;
                    break;
                case FrequencyType.Daily:
                    reportContentObjectModel.ReportFromDate = reportContentObjectModel.ReportToDate.AddDays(-entry.FrequencyInterval);
                    break;
                case FrequencyType.Weekly:
                    reportContentObjectModel.ReportFromDate = reportContentObjectModel.ReportToDate.AddDays(-7);
                    break;
                case FrequencyType.Monthly:
                    reportContentObjectModel.ReportFromDate = reportContentObjectModel.ReportToDate.AddMonths(-1);
                    break;
                case FrequencyType.Quarterly:
                    reportContentObjectModel.ReportFromDate = reportContentObjectModel.ReportToDate.AddMonths(-3);
                    break;
                case FrequencyType.BiAnnually:
                    reportContentObjectModel.ReportFromDate = reportContentObjectModel.ReportToDate.AddMonths(-6);
                    break;
                case FrequencyType.Annually:
                    reportContentObjectModel.ReportFromDate = reportContentObjectModel.ReportToDate.AddYears(-1);
                    break;
                case FrequencyType.Hourly:
                    reportContentObjectModel.ReportFromDate = reportContentObjectModel.ReportToDate.AddHours(-entry.FrequencyInterval);
                    break;                
            }

            PrintRequestGenerator printRequestGenerator = new PrintRequestGenerator();
            printRequestGenerator.GeneratePrintRequestXML(entry.ClientName, reportContentObjectModel);
        }
    }
}
