// ***********************************************************************
// Assembly         : RRD.FSG.RP.Scheduler
// Author           : 
// Created          : 10-29-2015
//
// Last Modified By : 
// Last Modified On : 11-09-2015
// ***********************************************************************

namespace RRD.FSG.RP.Scheduler.Interfaces
{
    /// <summary>
    /// Interface IReportSchedule
    /// </summary>
    public interface IReportSchedule
    {
        /// <summary>
        /// Processes the report schedule and returns an entry.
        /// </summary>
        /// <returns>IReportScheduleEntry representing the item to be processed.</returns>
        IReportScheduleEntry Process();

        /// <summary>
        /// Resets all items owned by this worker service in the queue.
        /// </summary>
        void ProcessReset();

        /// <summary>
        /// Resets a specific IReportScheduleEntry that is marked as InProcess.
        /// </summary>
        /// <param name="reportScheduleEntry">Entry to reset.</param>
        /// <remarks>Due to the asynchronous nature of queue processing it is possible that a cancellation request might result
        /// in multiple calls to this method for the same Entry. It is important that any implementation can handle multiple
        /// calls without negative consequence.. Essentially if the item is already reset, do nothing.</remarks>
        void ProcessResetEntry(IReportScheduleEntry reportScheduleEntry);

        /// <summary>
        /// Saves the ReportScheduleEntry in Report schedule table for the given reportScheduleId
        /// </summary>
        /// <param name="reportScheduleId">The report schedule identifier.</param>
        /// <param name="reportScheduleEntry">The report schedule entry.</param>
        void SaveResults(int reportScheduleId, IReportScheduleEntry reportScheduleEntry);
    }
}
