using RRD.FSG.RP.Scheduler.Interfaces;
using System.Threading;

namespace RRD.FSG.RP.Services.ScheduleTasks.Interfaces
{
    /// <summary>
    /// Interface for worker class
    /// </summary>
    public interface IWorker
    {
        #region Properties

        /// <summary>
        /// Gets the IWorkerService associated with this IWorker.
        /// </summary>
        IWorkerService WorkerService { get; }

        /// <summary>
        /// Gets the output file path for the worker.
        /// </summary>
        string OutputFilePath { get; }

        /// <summary>
        /// Gets the name of the worker. Should be unique.
        /// </summary>
        string WorkerName { get; }

        /// <summary>
        /// Gets the numerical ID of the worker. Should be unique to the server instance it belongs to.
        /// </summary>
        int WorkerId { get; }

        /// <summary>
        /// Gets the linked token that the worker process must monitor for cancellation requests.
        /// </summary>
        CancellationToken LinkedToken { get; }

        /// <summary>
        /// Gets the user requested cancellation token that the worker process validates against while monitoring the linked token.
        /// </summary>
        CancellationToken UserRequestToken { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Proceses a queue entry associated with a specific queue.
        /// </summary>
        /// <param name="reportSchedule">The report schedule.</param>
        /// <param name="reportScheduleEntry">The report schedule entry.</param>
        void ProcessQueue(IReportSchedule reportSchedule, IReportScheduleEntry reportScheduleEntry);

        /// <summary>
        /// Service specific method for processing a request.
        /// </summary>
        void ProcessRequest();

        /// <summary>
        /// Saves the results back into the queue.
        /// By default, this does not update all the fields on the queueEntry record in the database.
        /// That can be achieved by overriding this method in your derived worker class and calling
        /// an overload on the Queue object.
        /// </summary>
        /// <param name="reportSchedule">The report schedule.</param>
        /// <param name="reportScheduleEntry">The report schedule entry.</param>
        void SaveResults(IReportSchedule reportSchedule, IReportScheduleEntry reportScheduleEntry);

        #endregion
    }
}
