using RP.Utilities;
using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.VerticalMarket;
using RRD.FSG.RP.Model.Enumerations;
using RRD.FSG.RP.Model.Factories.VerticalMarket;
using RRD.FSG.RP.Scheduler.Interfaces;
using RRD.FSG.RP.Scheduler.Task.DocumentUpdateReport;
using RRD.FSG.RP.Scheduler.Task.Reports;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Scheduler.Task
{
    public class DocumentUpdateReportTask : BaseTask
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorReportsTask"/> class.
        /// </summary>
        public DocumentUpdateReportTask()
            : base()
        {

        }

        /// <summary>
        /// Overriden process method, that generates the report and does the distribution
        /// </summary>
        /// <param name="entry"></param>
        public override void Process(IReportScheduleEntry entry)
        {
            try
            {
                ReportContentObjectModel reportContentObjectModel = new ReportContentObjectModel();
                reportContentObjectModel.ReportToDate = entry.UtcNextScheduledRunDate;
                reportContentObjectModel.Name = entry.ReportName;
                reportContentObjectModel.ReportScheduleId = entry.ReportScheduleId;
                reportContentObjectModel.IsSFTP = entry.IsSFTP;

                this.GenerateReportByFrequencyType(entry.FrequencyType, entry.FrequencyInterval, entry.ClientName, reportContentObjectModel);

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
        /// GenerateReportByFrequencyType
        /// </summary>
        /// <param name="frequencyType"></param>
        /// <param name="frequencyInterval"></param>
        /// <param name="clientName"></param>
        /// <param name="reportContentObjectModel"></param>
        public void GenerateReportByFrequencyType(int frequencyType, int frequencyInterval, string clientName, ReportContentObjectModel reportContentObjectModel)
        {
            string directoryPath = ConfigurationManager.AppSettings["OutputPath"].ToString();
            string folderName = Path.Combine(directoryPath, clientName + "-" + Enum.GetName(typeof(FrequencyType), frequencyType));
            if (!Directory.Exists(folderName))
            {
                System.IO.Directory.CreateDirectory(folderName);
            }

            reportContentObjectModel.FileName = folderName;
            reportContentObjectModel.FileDir = folderName + "\\";
            var reportRunDate = reportContentObjectModel.ReportToDate;
            DocumentUpdateReportGenerator docUpdateReportGenerator = new DocumentUpdateReportGenerator(clientName);

            switch ((FrequencyType)frequencyType)
            {
                case FrequencyType.RunOnce:
                    reportContentObjectModel.ReportFromDate = reportRunDate.AddMonths(-1);                    
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format(clientName + "RightProStatus{0}.csv", reportRunDate.ToLocalTime().AddHours(1).ToString("yyyyMMddHHmm")));
                    reportContentObjectModel.FrequencyType = "Report";
                    reportContentObjectModel.Data = docUpdateReportGenerator.GenerateReport(clientName, reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    break;
                case FrequencyType.EveryXDays:
                    reportContentObjectModel.ReportFromDate = reportRunDate.AddDays(-frequencyInterval);
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format(clientName + "RightProStatus{0}.csv", reportRunDate.ToLocalTime().AddHours(1).ToString("yyyyMMddHHmm")));
                    reportContentObjectModel.FrequencyType = "Report";
                    reportContentObjectModel.Data = docUpdateReportGenerator.GenerateReport(clientName, reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    break;
                case FrequencyType.Weekly:
                    reportContentObjectModel.ReportFromDate = reportRunDate.AddDays(-7);
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format(clientName + "RightProStatus{0}.csv", reportRunDate.ToLocalTime().AddHours(1).ToString("yyyyMMddHHmm")));
                    reportContentObjectModel.Data = docUpdateReportGenerator.GenerateReport(clientName, reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    break;
                case FrequencyType.Monthly:
                    reportContentObjectModel.ReportFromDate = reportRunDate.AddMonths(-1);
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format(clientName + "RightProStatus{0}.csv", reportRunDate.ToLocalTime().AddHours(1).ToString("yyyyMMddHHmm")));
                    reportContentObjectModel.FrequencyType = Enum.GetName(typeof(FrequencyType), frequencyType);
                    reportContentObjectModel.Data = docUpdateReportGenerator.GenerateReport(clientName, reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    break;
                case FrequencyType.Quarterly:
                    reportContentObjectModel.ReportFromDate = reportRunDate.AddMonths(-3);
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format(clientName + "RightProStatus{0}.csv", reportRunDate.ToLocalTime().AddHours(1).ToString("yyyyMMddHHmm")));
                    reportContentObjectModel.FrequencyType = Enum.GetName(typeof(FrequencyType), frequencyType);
                    reportContentObjectModel.Data = docUpdateReportGenerator.GenerateReport(clientName, reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    break;
                case FrequencyType.BiAnnually:
                    reportContentObjectModel.ReportFromDate = reportRunDate.AddMonths(-6);
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format(clientName + "RightProStatus{0}.csv", reportRunDate.ToLocalTime().AddHours(1).ToString("yyyyMMddHHmm")));
                    reportContentObjectModel.FrequencyType = Enum.GetName(typeof(FrequencyType), frequencyType);
                    reportContentObjectModel.Data = docUpdateReportGenerator.GenerateReport(clientName, reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    break;

                case FrequencyType.Annually:
                    reportContentObjectModel.ReportFromDate = reportRunDate.AddYears(-1);
                    reportContentObjectModel.FileName = Path.Combine(folderName, string.Format(clientName + "RightProStatus{0}.csv", reportRunDate.ToLocalTime().AddHours(1).ToString("yyyyMMddHHmm")));
                    reportContentObjectModel.FrequencyType = Enum.GetName(typeof(FrequencyType), frequencyType);
                    reportContentObjectModel.Data = docUpdateReportGenerator.GenerateReport(clientName, reportContentObjectModel);
                    reportContentObjectModel.FileName = Path.GetFileName(reportContentObjectModel.FileName);
                    break;

                default:
                    break;
            }

            reportContentObjectModel.EmailSubject = reportContentObjectModel.FileName;
        }

    }
}
