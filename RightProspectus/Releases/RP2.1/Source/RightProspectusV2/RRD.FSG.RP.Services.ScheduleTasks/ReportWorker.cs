using RRD.FSG.RP.Services.ScheduleTasks.Interfaces;
using System;
using System.Threading;

namespace RRD.FSG.RP.Services.ScheduleTasks
{
    /// <summary>
    /// RRD.FSG.RP.Services.ScheduleTasks
    /// </summary>
    public class ReportWorker : WorkerBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportWorker"/> class.
        /// </summary>
        /// <param name="parentService">The parent worker service.</param>
        /// <param name="workerId">Numeric Id of the worker.</param>
        /// <param name="workerName">String Id of the worker.</param>
        /// <param name="linkedToken">The linked token used to monitor for cancellation requests.</param>
        /// <param name="userRequestToken">The user request token used to validate against the linked token.</param>
        public ReportWorker(IWorkerService parentService, int workerId, string workerName, CancellationToken linkedToken, CancellationToken userRequestToken)
            : base(parentService, workerId, workerName, linkedToken, userRequestToken)
        {
        }

        #endregion

        /// <summary>
        /// Service specific method for processing a request.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void ProcessRequest()
        {
            throw new NotImplementedException();
        }
    }
}
