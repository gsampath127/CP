//-----------------------------------------------------------------------
// <copyright file="WorkerBase.cs" company="R. R. Donnelley &amp; Sons Company">
//     Copyright (c) R. R. Donnelley &amp; Sons Company. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace RRD.FSG.RP.Services.ScheduleTasks
{
    using RRD.FSG.RP.Scheduler.Interfaces;
    using RRD.FSG.RP.Services.ScheduleTasks.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Threading;
    

    /// <summary>
    /// Abstract class that defines the common methods used by all of the worker services, 
    /// including multi-threading
    /// </summary>
    public abstract class WorkerBase
        : IWorker
    {
        #region Private Readonly Fields

        /// <summary>
        /// Field to hold the instance of the core business logic class for IWorker.
        /// </summary>
        private readonly WorkerCore workerCore;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:WorkerBase"/> class.
        /// </summary>
        /// <param name="parentService">The parent worker service.</param>
        /// <param name="workerId">Numerical Id of the worker.</param>
        /// <param name="workerName">Unique name of the worker.</param>
        /// <param name="linkedToken">The linked token used to monitor for cancellation requests.</param>
        /// <param name="userRequestToken">The user request token used to validate against the linked token.</param>
        protected WorkerBase(IWorkerService parentService, int workerId, string workerName, CancellationToken linkedToken, CancellationToken userRequestToken)
        {
            this.workerCore = new WorkerCore(this, parentService, workerId, workerName, linkedToken, userRequestToken);
        }

        #endregion

        #region Public Virtual Properties

        /// <summary>
        /// Gets the worker service associated with this worker.
        /// </summary>
        public virtual IWorkerService WorkerService
        {
            get { return this.Core.WorkerService; }
        }

        /// <summary>
        /// Gets the output file path for the worker.
        /// </summary>
        public virtual string OutputFilePath
        {
            get { return WorkerCore.OutputPath; }
        }

        /// <summary>
        /// Gets the numerical ID of the worker. Should be unique to the server instance it belongs to.
        /// </summary>
        public virtual int WorkerId
        {
            get { return this.Core.WorkerId; }
        }

        /// <summary>
        /// Gets the name of the worker. Should be unique.
        /// </summary>
        public virtual string WorkerName
        {
            get { return this.Core.WorkerName; }
        }

        /// <summary>
        /// Gets the linked token that the worker process must monitor for cancellation requests.
        /// </summary>
        public virtual CancellationToken LinkedToken
        {
            get { return this.workerCore.LinkedToken; }
        }

        /// <summary>
        /// Gets the user requested cancellation token that the worker process validates against while monitoring the linked token.
        /// </summary>
        public virtual CancellationToken UserRequestToken
        {
            get { return this.workerCore.UserRequestToken; }
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets the worker Core business logic instance for this IWorker.
        /// </summary>
        internal WorkerCore Core
        {
            get { return this.workerCore; }
        }

        #endregion

        #region Protected Virtual Properties

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        /// <value>The name of the service.</value>
        protected virtual string ServiceName
        {
            get { return this.Core.ServiceName; }
        }

        #endregion


        #region Public Virtual Methods

        /// <summary>
        /// overloaded method needed for WaitCallback delegate used by ThreadPool for multi-threading
        /// Has watchdog code to time out the ProcessQueue if it runs too long
        /// </summary>
        /// <param name="reportSchedule">The queue processor</param>
        /// <param name="reportScheduleEntry">An itme from the queue</param>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Needs to log all exceptions and continue.")]
        public virtual void ProcessQueue(IReportSchedule reportSchedule, IReportScheduleEntry reportScheduleEntry)
        {
            this.Core.ProcessQueue(reportSchedule, reportScheduleEntry);
        }

        /// <summary>
        /// Saves the results back into the queue.
        /// By default, this does not update all the fields on the queueEntry record in the database.
        /// That can be achieved by overriding this method in your derived worker class and calling
        /// an overload on the Queue object.
        /// </summary>
        /// <param name="reportSchedule">The queue.</param>
        /// <param name="reportScheduleEntry">The queue entry.</param>
        /// <param name="message">The message.</param>
        /// <param name="success">If the results message status was successful.</param>
        public virtual void SaveResults(IReportSchedule reportSchedule, IReportScheduleEntry reportScheduleEntry)
        {
            WorkerCore.SaveResults(reportSchedule, reportScheduleEntry);
        }

        #endregion

        #region Public Abstract Methods

        /// <summary>
        /// Service specific method for processing a request.
        /// </summary>
        public abstract void ProcessRequest();

        #endregion

    }
}