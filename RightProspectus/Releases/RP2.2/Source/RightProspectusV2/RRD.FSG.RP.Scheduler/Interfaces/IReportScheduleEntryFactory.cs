// ***********************************************************************
// Assembly         : RRD.FSG.RP.Scheduler
// Author           : 
// Created          : 10-29-2015
//
// Last Modified By : 
// Last Modified On : 11-13-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;

namespace RRD.FSG.RP.Scheduler.Interfaces
{
    /// <summary>
    /// Interface for IReportScheduleEntry Factories (non generic).
    /// </summary>
    public interface IReportScheduleEntryFactory
    {
        /// <summary>
        /// Gets the data access instance used to interface between factory and persisted storage of entity data.
        /// </summary>
        /// <value>The data access.</value>
        IDataAccess DataAccess { get; }

        /// <summary>
        /// Saves the content and content type in the queue record.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="reportScheduleId">The report schedule identifier.</param>
        /// <param name="reportScheduleEntry">The report schedule entry.</param>
        void SaveResults(string connectionString, int reportScheduleId, IReportScheduleEntry reportScheduleEntry);

        /// <summary>
        /// Processes the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="serviceName">Name of the service.</param>
        /// <returns>IReportScheduleEntry.</returns>
        IReportScheduleEntry Process(string connectionString, string serviceName);

        /// <summary>
        /// Resets every QueueEntry that is marked as InProcess
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="reportScheduleId">Report schedule Id.</param>
        void ProcessReset(string connectionString, int reportScheduleId);

        /// <summary>
        /// Reset a specific IQueueEntry that is marked as InProcess. Usually called for a graceful exit.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="reportScheduleEntry">The report schedule entry.</param>
        /// <param name="reduceAttempts">True if we should reduce total attempts (such as for a graceful exit), false otherwise.</param>
        void ProcessResetEntry(string connectionString, IReportScheduleEntry reportScheduleEntry, bool reduceAttempts = true);

        /// <summary>
        /// Updates all the properties on the queue record.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="reportScheduleEntry">The report schedule entry.</param>
        /// <param name="success">if set to <c>true</c> [success].</param>
        void Update(string connectionString, IReportScheduleEntry reportScheduleEntry, bool success);

    }
}
