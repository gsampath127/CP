// ***********************************************************************
// Assembly         : RRD.FSG.RP.Scheduler
// Author           : 
// Created          : 10-29-2015
//
// Last Modified By : 
// Last Modified On : 10-29-2015
// ***********************************************************************
namespace RRD.FSG.RP.Scheduler.Interfaces
{
    /// <summary>
    /// Interface IReportSchedule
    /// </summary>
    /// <typeparam name="TScheduleEntry">The type of the t schedule entry.</typeparam>
    public interface IReportSchedule<TScheduleEntry>:
        IReportSchedule
        where TScheduleEntry : IReportScheduleEntry
    {
        /// <summary>
        /// Processes the queue and returns an entry.
        /// </summary>
        /// <returns>TQueueEntry representing the item to be processed.</returns>
        new TScheduleEntry Process();

        /// <summary>
        /// Reset a specific TQueueEntry that is marked as InProcess
        /// </summary>
        /// <param name="scheduleEntry">The schedule entry.</param>
        void ProcessResetEntry(TScheduleEntry scheduleEntry);
    }
}
