// ***********************************************************************
// Assembly         : RRD.FSG.RP.Scheduler
// Author           : 
// Created          : 10-29-2015
//
// Last Modified By : 
// Last Modified On : 10-31-2015
// ***********************************************************************

namespace RRD.FSG.RP.Scheduler.Interfaces
{
    /// <summary>
    /// Interface for IReportScheduleEntry Factories
    /// </summary>
    /// <typeparam name="TScheduleEntry">Type of IReportScheduleEntry the factory creates.</typeparam>
    public interface IReportScheduleEntryFactory<TScheduleEntry>
        : IReportScheduleEntryFactory
        where TScheduleEntry : class, IReportScheduleEntry
    {
        /// <summary>
        /// Processes the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="serviceName">Name of the service.</param>
        /// <returns>TScheduleEntry.</returns>
        new TScheduleEntry Process(string connectionString, string serviceName);

        /// <summary>
        /// Updates all the properties on the queue record.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="queueEntry">The queue entry.</param>
        /// <param name="success">if set to <c>true</c> [success].</param>
        void Update(string connectionString, TScheduleEntry queueEntry, bool success);

        /// <summary>
        /// Reset a specific TQueueEntry that is marked as InProcess. Usually called for a graceful exit.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="queueEntry">Entry to reset.</param>
        /// <param name="reduceAttempts">True if we should reduce total attempts (such as for a graceful exit), false otherwise.</param>
        void ProcessResetEntry(string connectionString, TScheduleEntry queueEntry, bool reduceAttempts = true);
    }
}
