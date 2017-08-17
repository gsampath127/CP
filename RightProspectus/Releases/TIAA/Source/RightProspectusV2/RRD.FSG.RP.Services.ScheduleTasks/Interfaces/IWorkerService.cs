using RRD.FSG.RP.Scheduler.Interfaces;
using System.Threading;

namespace RRD.FSG.RP.Services.ScheduleTasks.Interfaces
{
    /// <summary>
    /// Interface for worker service classes.
    /// </summary>
    public interface IWorkerService
    {
        #region Properties

        /// <summary>
        /// Gets the name of the service.
        /// Used for the ServiceNameAndServer propert as well as determining the MSMQ Queue name (if used).
        /// </summary>
        string WorkerServiceName { get; }

        /// <summary>
        /// Gets the identifier for this service. Used for load balanced service identification.
        /// </summary>
        string ServiceNameAndServer { get; }

        /// <summary>
        /// Gets a value indicating whether or not to grab Service name from MMC. This gets around a known .NET bug with Service implementation.
        /// Set "true" for actual windows services, false for other implementations such as unit tests.
        /// </summary>
        bool SetServerNameAndServerFromServiceQuery { get; }

        /// <summary>
        /// Gets a cancellation token that indicates whether the service has been stopped.
        /// </summary>
        CancellationToken ServiceStopToken { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a worker base instance that implements IWorkerBase.
        /// </summary>
        /// <param name="workerId">Unique number to represent the worker.</param>
        /// <param name="workerName">Unique name to represent the worker.</param>
        /// <param name="linkedToken">The linked token used to monitor for cancellation requests.</param>
        /// <param name="userRequestToken">The user request token used to validate against the linked token.</param>
        /// <returns>A worker instance that implements IWorkerBase.</returns>
        IWorker NewWorker(int workerId, string workerName, CancellationToken linkedToken, CancellationToken userRequestToken);

        /// <summary>
        /// Wrapper for ServiceBase.RequesteAdditionalTime. Used by the internal implementation of service stop requests.
        /// </summary>
        /// <param name="milliseconds">number of milliseconds to request.</param>
        void RequestAdditionalTime(int milliseconds);

        /// <summary>
        /// Retrieves the appropriate implementation of IQueue for the IWorkerService.
        /// </summary>
        /// <returns>IQueue instance.</returns>
        IReportSchedule NewReportSchedule();

        #endregion
    }
}
