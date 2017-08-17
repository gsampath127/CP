// ***********************************************************************
// Assembly         : RRD.FSG.RP.Scheduler
// Author           : 
// Created          : 11-09-2015
//
// Last Modified By : 
// Last Modified On : 11-12-2015
// ***********************************************************************
using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Enumerations;
using RRD.FSG.RP.Scheduler.Interfaces;
using RRD.FSG.RP.Scheduler.Task.Reports;
using RRD.FSG.RP.Utilities;
using System;
using System.Configuration;
using System.IO;

namespace RRD.FSG.RP.Scheduler.Task
{
    /// <summary>
    /// Class ErrorReportsTask.
    /// </summary>
    public class ErrorReportsTask : BaseTask
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorReportsTask"/> class.
        /// </summary>
        public ErrorReportsTask()
            :base()
        {

        }

        /// <summary>
        /// Overriden process method, that generates the report and does the distribution
        /// </summary>
        /// <param name="entry">Object of type IReportScheduleEntry</param>
        public override void Process(IReportScheduleEntry entry)
        {
            try
            {
            ReportContentObjectModel reportContentObjectModel = new ReportContentObjectModel();
            reportContentObjectModel.ReportToDate = entry.UtcNextDataEndDate;
            reportContentObjectModel.Name = entry.ReportName;
            reportContentObjectModel.ReportScheduleId = entry.ReportScheduleId;

            this.GenerateReportByFrequencyType(entry.FrequencyType, entry.FrequencyInterval, entry.ClientName, reportContentObjectModel, entry.UtcDataStartDate);


            ReportContent reportContent = new ReportContent(entry.ClientName);
            reportContent.SaveReportContent(reportContentObjectModel);
            ReportTransfer reportTransfer = new ReportTransfer();
            reportTransfer.Process(reportContentObjectModel, entry);
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
        /// <param name="frequencyType">Type of the frequency.</param>
        /// <param name="frequencyInterval">Interval of the frequency.</param>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="reportContentObjectModel">The report content object model.</param>
        public void GenerateReportByFrequencyType(int frequencyType, int frequencyInterval, string clientName, ReportContentObjectModel reportContentObjectModel, DateTime? dataStartDate)
        {

            string directoryPath = ConfigurationManager.AppSettings.Get("OutputPath");
            string folderName = Path.Combine(directoryPath, clientName + "-" + Enum.GetName(typeof(FrequencyType), frequencyType));
            if (!Directory.Exists(folderName))
            {
                System.IO.Directory.CreateDirectory(folderName);
            }


            reportContentObjectModel.FileName = folderName;
            var reportDataEndDate = reportContentObjectModel.ReportToDate;
            MonthlyErrorReportGenerator monthlyErrorReportGenerator = new MonthlyErrorReportGenerator(clientName);

            switch ((FrequencyType)frequencyType)
            {
                case FrequencyType.RunOnce:
                    reportContentObjectModel.ReportFromDate = dataStartDate.Value;
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("Error_Report {0}.xlsx", reportDataEndDate.ToString("MM-dd-yyyy")));
                    reportContentObjectModel.FrequencyType = "Report";
                    reportContentObjectModel.Data = monthlyErrorReportGenerator.GenerateReport(reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    reportContentObjectModel.EmailBody = clientName + " Error Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                    reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                    break;
                case FrequencyType.Daily:
                    reportContentObjectModel.ReportFromDate = reportDataEndDate.AddDays(-frequencyInterval);
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("Error_Report {0}.xlsx", reportDataEndDate.ToString("MM-dd-yyyy")));
                    reportContentObjectModel.FrequencyType = "Report";
                    reportContentObjectModel.Data = monthlyErrorReportGenerator.GenerateReport(reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    reportContentObjectModel.EmailBody = clientName + " Error Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                    reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                    break;
                case FrequencyType.Weekly:
                    reportContentObjectModel.ReportFromDate = reportDataEndDate.AddDays(-7);
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("WeeklyError_Report {0}.xlsx", reportDataEndDate.ToString("MM-dd-yyyy")));
                    WeeklyErrorReportGenerator weeklyErrorReportGenerator = new WeeklyErrorReportGenerator(clientName);
                    reportContentObjectModel.Data = weeklyErrorReportGenerator.GenerateReport(reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    reportContentObjectModel.EmailBody = clientName + " Weekly Error Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                    reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                    break;
                case FrequencyType.Monthly:
                    reportContentObjectModel.ReportFromDate = reportDataEndDate.AddMonths(-1);
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("MonthlyError_Report {0}.xlsx", reportDataEndDate.ToString("MM-dd-yyyy")));
                    reportContentObjectModel.FrequencyType = Enum.GetName(typeof(FrequencyType), frequencyType);
                    reportContentObjectModel.Data = monthlyErrorReportGenerator.GenerateReport(reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    reportContentObjectModel.EmailBody = clientName + " Monthly Error Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                    reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                    break;
                case FrequencyType.Quarterly:
                    reportContentObjectModel.ReportFromDate = reportDataEndDate.AddMonths(-3);
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("QuarterlyError_Report {0}.xlsx", reportDataEndDate.ToString("MM-dd-yyyy")));
                    reportContentObjectModel.FrequencyType = Enum.GetName(typeof(FrequencyType), frequencyType);
                    reportContentObjectModel.Data = monthlyErrorReportGenerator.GenerateReport(reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    reportContentObjectModel.EmailBody = clientName + " Quarterly Error Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                    reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                    break;
                case FrequencyType.BiAnnually:
                    reportContentObjectModel.ReportFromDate = reportDataEndDate.AddMonths(-6);
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("Semi-AnnualError_Report {0}.xlsx", reportDataEndDate.ToString("MM-dd-yyyy")));
                    reportContentObjectModel.FrequencyType = Enum.GetName(typeof(FrequencyType), frequencyType);
                    reportContentObjectModel.Data = monthlyErrorReportGenerator.GenerateReport(reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    reportContentObjectModel.EmailBody = clientName + " Semi-Annual Error Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                    reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                    break;
                   
                case FrequencyType.Annually:
                    reportContentObjectModel.ReportFromDate = reportDataEndDate.AddYears(-1);
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("AnnualError_Report {0}.xlsx", reportDataEndDate.ToString("MM-dd-yyyy")));
                    reportContentObjectModel.FrequencyType = Enum.GetName(typeof(FrequencyType), frequencyType);
                    reportContentObjectModel.Data = monthlyErrorReportGenerator.GenerateReport(reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    reportContentObjectModel.EmailBody = clientName + " Annual Error Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                    reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                    break;
                case FrequencyType.Hourly:
                    reportContentObjectModel.ReportFromDate = reportDataEndDate.AddHours(-frequencyInterval);
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("HourlyError_Report {0}.xlsx", reportDataEndDate.ToString("MM-dd-yyyy")));
                    reportContentObjectModel.FrequencyType = Enum.GetName(typeof(FrequencyType), frequencyType);
                    reportContentObjectModel.Data = monthlyErrorReportGenerator.GenerateReport(reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    reportContentObjectModel.EmailBody = clientName + " Hourly Error Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                    reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                    break;
              
               
             
                default:
                    break;
            }
        }
    }
}
