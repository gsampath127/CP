using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Enumerations;
using RRD.FSG.RP.Scheduler.Interfaces;
using RRD.FSG.RP.Scheduler.Task.BillingReports;
using RRD.FSG.RP.Scheduler.Task.Reports;
using RRD.FSG.RP.Utilities;
//using RRD.FSG.RP.Scheduler.Task.Reports;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Scheduler.Task
{
    /// <summary>
    /// class BillingReportTask
    /// </summary>
    public class BillingReportTask: BaseTask
    {
        /// <summary>
        /// Default constructor for billing report 
        /// </summary>
        public BillingReportTask()
            :base()
        {

        }
          /// <summary>
          /// Process the task
          /// </summary>
          /// <param name="entry"></param>
          public override void Process(IReportScheduleEntry entry)
          {
              try
              {
                  
                  //TODO: Based on the report type call the Generate report method of appropriate report class
                  ReportContentObjectModel reportContentObjectModel = new ReportContentObjectModel();
                  reportContentObjectModel.ReportToDate =/* DateTime.Now;//*/entry.UtcNextDataEndDate;
                  reportContentObjectModel.Name = /*"billingreport";//*/entry.ReportName;
                  reportContentObjectModel.ReportScheduleId =/* 219; //*/entry.ReportScheduleId;

                  this.GenerateReportByFrequencyType(entry.FrequencyType, entry.FrequencyInterval, entry.ClientName, reportContentObjectModel, entry.UtcDataStartDate);
                  //this.GenerateReportByFrequencyType(4, 1, "URLAndBillingReportTest", reportContentObjectModel);

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
          /// Generates the report for the given frequency type
          /// </summary>
          /// <param name="frequencyType"></param>
          /// <param name="frequencyInterval"></param>
          /// <param name="clientName"></param>
          /// <param name="reportContentObjectModel"></param>
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
              MonthlyBillingReportGenerator billingreport = new MonthlyBillingReportGenerator(clientName);
              switch ((FrequencyType)frequencyType)
              {
                  case FrequencyType.RunOnce:
                      reportContentObjectModel.ReportFromDate = dataStartDate.Value;
                      reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("BillingReport {0}.xlsx", reportDataEndDate.ToString("MM-dd-yyyy")));
                      reportContentObjectModel.FrequencyType = "Report";
                      reportContentObjectModel.Data = billingreport.GenerateReport(reportContentObjectModel,clientName);
                      reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                      reportContentObjectModel.EmailBody = clientName + "Billing Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                      reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                      break;
                  case FrequencyType.Daily:
                      reportContentObjectModel.ReportFromDate = reportDataEndDate.AddDays(-frequencyInterval);
                      reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("BillingReport {0}.xlsx", reportDataEndDate.ToString("MM-dd-yyyy")));
                      reportContentObjectModel.FrequencyType = "Report";
                      reportContentObjectModel.Data = billingreport.GenerateReport(reportContentObjectModel,clientName);
                      reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                      reportContentObjectModel.EmailBody = clientName + "Billing Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                      reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                      break;
                  case FrequencyType.Weekly:
                      reportContentObjectModel.ReportFromDate = reportDataEndDate.AddDays(-7);
                      reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("WeeklyBilling_Report {0}.xlsx", reportDataEndDate.ToString("MM-dd-yyyy")));
                      reportContentObjectModel.Data = billingreport.GenerateReport(reportContentObjectModel,clientName);
                      reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                      reportContentObjectModel.EmailBody = clientName + " Weekly Billing Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                      reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                      break;
                  case FrequencyType.Monthly:
                      reportContentObjectModel.ReportFromDate = reportDataEndDate.AddMonths(-1);
                      reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("MonthlyBilling_Report {0}.xlsx", reportDataEndDate.ToString("MM-dd-yyyy")));
                      reportContentObjectModel.FrequencyType = Enum.GetName(typeof(FrequencyType), frequencyType);
                      reportContentObjectModel.Data = billingreport.GenerateReport(reportContentObjectModel,clientName);
                      reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                      reportContentObjectModel.EmailBody = clientName + " Monthly Billing Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                      reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                      break;
                  case FrequencyType.Quarterly:
                      reportContentObjectModel.ReportFromDate = reportDataEndDate.AddMonths(-3);
                      reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("QuarterlyBilling_Report {0}.xlsx", reportDataEndDate.ToString("MM-dd-yyyy")));
                      reportContentObjectModel.Data = billingreport.GenerateReport(reportContentObjectModel,clientName);
                      reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                      reportContentObjectModel.EmailBody = clientName + " Quarterly Billing Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                      reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                      break;
                  case FrequencyType.BiAnnually:
                      reportContentObjectModel.ReportFromDate = reportDataEndDate.AddMonths(-6);
                      reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("Semi-AnnualBilling_Report {0}.xlsx", reportDataEndDate.ToString("MM-dd-yyyy")));
                      reportContentObjectModel.FrequencyType = Enum.GetName(typeof(FrequencyType), frequencyType);
                      reportContentObjectModel.Data = billingreport.GenerateReport(reportContentObjectModel,clientName);
                      reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                      reportContentObjectModel.EmailBody = clientName + " Semi-Annual Billing  Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                      reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                      break;
                  case FrequencyType.Annually:
                      reportContentObjectModel.ReportFromDate = reportDataEndDate.AddYears(-1);
                      reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("AnnualBilling_Report {0}.xlsx", reportDataEndDate.ToString("MM-dd-yyyy")));
                      reportContentObjectModel.FrequencyType = Enum.GetName(typeof(FrequencyType), frequencyType);
                      reportContentObjectModel.Data = billingreport.GenerateReport(reportContentObjectModel,clientName);
                      reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                      reportContentObjectModel.EmailBody = clientName + " Annual Billing Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                      reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                      break;
                  case FrequencyType.Hourly:
                      reportContentObjectModel.ReportFromDate = reportDataEndDate.AddHours(-frequencyInterval);
                      reportContentObjectModel.FileName = Path.Combine(folderName, string.Format("HourlyBilling_Report {0}.xlsx", reportDataEndDate.ToString("MM-dd-yyyy")));
                      reportContentObjectModel.FrequencyType = Enum.GetName(typeof(FrequencyType), frequencyType);
                      reportContentObjectModel.Data = billingreport.GenerateReport(reportContentObjectModel, clientName);
                      reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                      reportContentObjectModel.EmailBody = clientName + " Hourly Billing Report: " + reportContentObjectModel.ReportFromDate.ToShortDateString() + "- " + reportContentObjectModel.ReportToDate.ToShortDateString();
                      reportContentObjectModel.EmailSubject = reportContentObjectModel.EmailBody;
                      break;

                  default:
                      break;
              }
             
            
              }
          }

    }

