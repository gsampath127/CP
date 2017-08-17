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
using System;
using System.Configuration;
using System.IO;

namespace RRD.FSG.RP.Scheduler.Task
{
    /// <summary>
    /// Class ReportsTask.
    /// </summary>
    public class ReportsTask: BaseTask
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportsTask"/> class.
        /// </summary>
        public ReportsTask()
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
                reportContentObjectModel.ReportToDate = entry.UtcNextScheduledRunDate;
                reportContentObjectModel.Name = entry.ReportName;
                reportContentObjectModel.ReportScheduleId = entry.ReportScheduleId;

                this.GenerateReportByFrequencyType(entry.FrequencyType,entry.FrequencyInterval ,entry.ClientName, reportContentObjectModel);


                ReportContent reportContent = new ReportContent(entry.ClientName);
                reportContent.SaveReportContent(reportContentObjectModel);
                ReportTransfer reportTransfer = new ReportTransfer();
                reportTransfer.Process(reportContentObjectModel, entry);
            }
            catch (IOException ioException)
            {
                ExceptionHandler.HandleWorkerServiceProcessException(ioException);
                throw;
            }
            catch (OperationCanceledException exception)
            {
                ExceptionHandler.HandleWorkerServiceProcessException(exception);
                throw;
            }
            catch (Exception exception)
            {
                try
                {
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

        /// <summary>
        /// Generates the type of the report by frequency.
        /// </summary>
        /// <param name="frequencyType">Type of the frequency.</param>
        /// <param name="frequencyInterval">Interval of the frequency.</param>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="reportContentObjectModel">The report content object model.</param>
        public void GenerateReportByFrequencyType(int frequencyType, int frequencyInterval,string clientName, ReportContentObjectModel reportContentObjectModel)
        {
            string directoryPath = ConfigurationManager.AppSettings.Get("OutputPath");
            string folderName = Path.Combine(directoryPath, clientName + "-" + Enum.GetName(typeof(FrequencyType), frequencyType));
            if (!Directory.Exists(folderName))
            {
                System.IO.Directory.CreateDirectory(folderName);
            }


            reportContentObjectModel.FileName = folderName;
            var reportRunDate = reportContentObjectModel.ReportToDate;
            MonthlyReportGenerator monthlyReportGenerator = new MonthlyReportGenerator(clientName);
             switch ((FrequencyType)frequencyType)
            {
                case FrequencyType.RunOnce:
                    reportContentObjectModel.ReportFromDate = reportRunDate.AddMonths(-1);
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("Report {0}.xlsx", reportRunDate.ToString("MM-dd-yyyy")));
                    reportContentObjectModel.FrequencyType = "Report";
                    reportContentObjectModel.Data = monthlyReportGenerator.GenerateReport(reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    reportContentObjectModel.EmailBody = clientName + "Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                    reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                    break;
                case FrequencyType.EveryXDays:
                    reportContentObjectModel.ReportFromDate = reportRunDate.AddDays(-frequencyInterval);
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("Report {0}.xlsx", reportRunDate.ToString("MM-dd-yyyy")));
                    reportContentObjectModel.FrequencyType = "Report";
                    reportContentObjectModel.Data = monthlyReportGenerator.GenerateReport(reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    reportContentObjectModel.EmailBody = clientName + "Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                    reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                    break;
                case FrequencyType.Weekly:
                    reportContentObjectModel.ReportFromDate = reportRunDate.AddDays(-7);
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("Weekly_Report {0}.xlsx", reportRunDate.ToString("MM-dd-yyyy")));
                    WeeklyReportGenerator weeklyReportGenerator = new WeeklyReportGenerator(clientName);
                    reportContentObjectModel.Data = weeklyReportGenerator.GenerateReport(reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    reportContentObjectModel.EmailBody = clientName + " Weekly Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                    reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                    break;
                case FrequencyType.Monthly:
                    reportContentObjectModel.ReportFromDate = reportRunDate.AddMonths(-1);
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("Monthly_Report {0}.xlsx", reportRunDate.ToString("MM-dd-yyyy")));
                    reportContentObjectModel.FrequencyType = Enum.GetName(typeof(FrequencyType), frequencyType);
                    reportContentObjectModel.Data = monthlyReportGenerator.GenerateReport(reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    reportContentObjectModel.EmailBody = clientName + " Monthly Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                    reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                    break;
                case FrequencyType.Quarterly:
                    reportContentObjectModel.ReportFromDate = reportRunDate.AddMonths(-3);
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("Quarterly_Report {0}.xlsx", reportRunDate.ToString("MM-dd-yyyy")));
                    QuarterlyReportGenerator quarterlyReportGenerator = new QuarterlyReportGenerator(clientName);
                    reportContentObjectModel.Data = quarterlyReportGenerator.GenerateReport(reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    reportContentObjectModel.EmailBody = clientName+" Quarterly Report: "+reportContentObjectModel.ReportFromDate.ToShortDateString()+"- "+reportContentObjectModel.ReportToDate.ToShortDateString();
                    reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                    break;
                 case FrequencyType.BiAnnually:   
                    reportContentObjectModel.ReportFromDate = reportRunDate.AddMonths(-6);
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("BiAnnual_Report {0}.xlsx", reportRunDate.ToString("MM-dd-yyyy")));
                    reportContentObjectModel.FrequencyType = Enum.GetName(typeof(FrequencyType), frequencyType);
                    reportContentObjectModel.Data = monthlyReportGenerator.GenerateReport(reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    reportContentObjectModel.EmailBody = clientName + " BiAnnual Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                    reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                    break;
                 case FrequencyType.Annually:
                    reportContentObjectModel.ReportFromDate = reportRunDate.AddYears(-1);
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("Annual_Report {0}.xlsx", reportRunDate.ToString("MM-dd-yyyy")));
                    reportContentObjectModel.FrequencyType = Enum.GetName(typeof(FrequencyType), frequencyType);
                    reportContentObjectModel.Data = monthlyReportGenerator.GenerateReport(reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    reportContentObjectModel.EmailBody = clientName + " Annual Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                    reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                    break;
              
                default:
                break;
            }
             
        }
    }
}
