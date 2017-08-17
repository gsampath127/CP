// ***********************************************************************
// Assembly         : RRD.FSG.RP.Scheduler
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-13-2015
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
    /// Class ReportsTask.
    /// </summary>
    public class ActivityReportTask: BaseTask
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityReportTask"/> class.
        /// </summary>
        public ActivityReportTask()
            :base()
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
                //TODO: Based on the report type call the Generate report method of appropriate report class
                ReportContentObjectModel reportContentObjectModel = new ReportContentObjectModel();
                reportContentObjectModel.ReportToDate = entry.UtcNextDataEndDate;
                reportContentObjectModel.Name = entry.ReportName;
                reportContentObjectModel.ReportScheduleId = entry.ReportScheduleId;

                this.GenerateReportByFrequencyType(entry.FrequencyType,entry.FrequencyInterval ,entry.ClientName, reportContentObjectModel, entry.UtcDataStartDate);


                ReportContent reportContent = new ReportContent(entry.ClientName);
                reportContent.SaveReportContent(reportContentObjectModel);
                ReportTransfer reportTransfer = new ReportTransfer();
                reportTransfer.Process(reportContentObjectModel, entry);
            }            
            catch (Exception exception)
            {
                try
                {
                    SendErrorEmail(entry);
                    // any errors rendering the publication should be flagged and sent back as failed
                    entry.Status = (int)ScheduleStatus.Failure;
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
        public void GenerateReportByFrequencyType(int frequencyType, int frequencyInterval,string clientName, ReportContentObjectModel reportContentObjectModel, DateTime? dataStartDate)
        {
            string directoryPath = ConfigurationManager.AppSettings.Get("OutputPath");
            string folderName = Path.Combine(directoryPath, clientName + "-" + Enum.GetName(typeof(FrequencyType), frequencyType));
            if (!Directory.Exists(folderName))
            {
                System.IO.Directory.CreateDirectory(folderName);
            }


            reportContentObjectModel.FileName = folderName;
            var reportDataEndDate = reportContentObjectModel.ReportToDate;
            MonthlyReportGenerator monthlyReportGenerator = new MonthlyReportGenerator(clientName);
             switch ((FrequencyType)frequencyType)
            {
                case FrequencyType.RunOnce:
                    reportContentObjectModel.ReportFromDate = dataStartDate.Value;
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("Report {0}.xlsx", reportDataEndDate.ToString("MM-dd-yyyy")));
                    reportContentObjectModel.FrequencyType = "Report";
                    reportContentObjectModel.Data = monthlyReportGenerator.GenerateReport(reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    reportContentObjectModel.EmailBody = clientName + "Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                    reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                    break;
                case FrequencyType.Daily:
                    reportContentObjectModel.ReportFromDate = reportDataEndDate.AddDays(-frequencyInterval);
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("Report {0}.xlsx", reportDataEndDate.ToString("MM-dd-yyyy")));
                    reportContentObjectModel.FrequencyType = "Report";
                    reportContentObjectModel.Data = monthlyReportGenerator.GenerateReport(reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    reportContentObjectModel.EmailBody = clientName + "Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                    reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                    break;
                case FrequencyType.Weekly:
                    reportContentObjectModel.ReportFromDate = reportDataEndDate.AddDays(-7);
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("Weekly_Report {0}.xlsx", reportDataEndDate.ToString("MM-dd-yyyy")));
                    WeeklyReportGenerator weeklyReportGenerator = new WeeklyReportGenerator(clientName);
                    reportContentObjectModel.Data = weeklyReportGenerator.GenerateReport(reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    reportContentObjectModel.EmailBody = clientName + " Weekly Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                    reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                    break;
                case FrequencyType.Monthly:
                    reportContentObjectModel.ReportFromDate = reportDataEndDate.AddMonths(-1);
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("Monthly_Report {0}.xlsx", reportDataEndDate.ToString("MM-dd-yyyy")));
                    reportContentObjectModel.FrequencyType = Enum.GetName(typeof(FrequencyType), frequencyType);
                    reportContentObjectModel.Data = monthlyReportGenerator.GenerateReport(reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    reportContentObjectModel.EmailBody = clientName + " Monthly Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                    reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                    break;
                case FrequencyType.Quarterly:
                    reportContentObjectModel.ReportFromDate = reportDataEndDate.AddMonths(-3);
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("Quarterly_Report {0}.xlsx", reportDataEndDate.ToString("MM-dd-yyyy")));
                    QuarterlyReportGenerator quarterlyReportGenerator = new QuarterlyReportGenerator(clientName);
                    reportContentObjectModel.Data = quarterlyReportGenerator.GenerateReport(reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    reportContentObjectModel.EmailBody = clientName+" Quarterly Report: "+reportContentObjectModel.ReportFromDate.ToShortDateString()+"- "+reportContentObjectModel.ReportToDate.ToShortDateString();
                    reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                    break;
                 case FrequencyType.BiAnnually:
                    reportContentObjectModel.ReportFromDate = reportDataEndDate.AddMonths(-6);
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("Semi-Annual_Report {0}.xlsx", reportDataEndDate.ToString("MM-dd-yyyy")));
                    reportContentObjectModel.FrequencyType = Enum.GetName(typeof(FrequencyType), frequencyType);
                    reportContentObjectModel.Data = monthlyReportGenerator.GenerateReport(reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    reportContentObjectModel.EmailBody = clientName + " Semi-Annual Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                    reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                    break;
                 case FrequencyType.Annually:
                    reportContentObjectModel.ReportFromDate = reportDataEndDate.AddYears(-1);
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("Annual_Report {0}.xlsx", reportDataEndDate.ToString("MM-dd-yyyy")));
                    reportContentObjectModel.FrequencyType = Enum.GetName(typeof(FrequencyType), frequencyType);
                    reportContentObjectModel.Data = monthlyReportGenerator.GenerateReport(reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    reportContentObjectModel.EmailBody = clientName + " Annual Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                    reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                    break;
                 case FrequencyType.Hourly:
                    reportContentObjectModel.ReportFromDate = reportDataEndDate.AddHours(-frequencyInterval);
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("Hourly_Report {0}.xlsx", reportDataEndDate.ToString("MM-dd-yyyy")));
                    reportContentObjectModel.FrequencyType = Enum.GetName(typeof(FrequencyType), frequencyType);
                    reportContentObjectModel.Data = monthlyReportGenerator.GenerateReport(reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    reportContentObjectModel.EmailBody = clientName + " Hourly Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                    reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                    break;
              
              
                default:
                break;
            }
             
        }
    }
}
