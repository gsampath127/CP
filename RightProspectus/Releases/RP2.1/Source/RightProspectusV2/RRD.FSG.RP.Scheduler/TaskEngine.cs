// ***********************************************************************
// Assembly         : RRD.FSG.RP.Scheduler
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-13-2015
// ***********************************************************************
using RRD.FSG.RP.Scheduler.Interfaces;
using RRD.FSG.RP.Scheduler.Task;

namespace RRD.FSG.RP.Scheduler
{
    /// <summary>
    /// Class TaskEngine.
    /// </summary>
    public class TaskEngine
    {
        /// <summary>
        /// Processes the specified report schedule entry.
        /// </summary>
        /// <param name="reportScheduleEntry">The report schedule entry.</param>
        public void Process(IReportScheduleEntry reportScheduleEntry)
        {
            var task = RetrieveTaskObject(reportScheduleEntry.ReportName);
            task.Process(reportScheduleEntry);
        }

        /// <summary>
        /// Retrieves the task object.
        /// </summary>
        /// <param name="reportName">Name of the report.</param>
        /// <returns>BaseTask.</returns>
        private BaseTask RetrieveTaskObject(string reportName)
        {
            switch (reportName.ToLower())
            {
                case "activityreport":
                    return new ReportsTask();
                case "errorreport":
                    return new ErrorReportsTask();
                case "printrequestreport":
                    return new PrintRequestsTask();
                case "billingreport":
                    return new BillingReportTask();
                case "documentupdatereport":
                    return new DocumentUpdateReportTask();
                default:
                    return new ReportsTask();
            }
        }
    }
}
