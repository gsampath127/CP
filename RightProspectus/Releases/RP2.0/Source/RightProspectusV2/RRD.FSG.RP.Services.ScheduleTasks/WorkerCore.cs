//-----------------------------------------------------------------------
// <copyright file="WorkerCore.cs" company="R. R. Donnelley &amp; Sons Company">
//     Copyright (c) R. R. Donnelley &amp; Sons Company. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace RRD.FSG.RP.Services.ScheduleTasks
{
    using RRD.FSG.RP.Model.Enumerations;
    using RRD.FSG.RP.Scheduler;
    using RRD.FSG.RP.Scheduler.Interfaces;
    using RRD.FSG.RP.Services.ScheduleTasks.Interfaces;
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Scheduler = RRD.FSG.RP.Scheduler;
    

    /// <summary>
    /// Core business logic for IWorker instances.
    /// </summary>
    internal sealed class WorkerCore
    {
        #region Private Constants

        /// <summary>
        /// Interval in which to wait before checking to see if the thread should be cancelled or aborted.
        /// </summary>
        private const int ThreadJoinIntervalMS = 500;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkerCore"/> class.
        /// </summary>
        /// <param name="parentWorker">IWorker associated with the worker business logic instance.</param>
        /// <param name="parentService">IWorkerService associated with the IWorker instance.</param>
        /// <param name="workerId">Numerical Id of the worker.</param>
        /// <param name="workerName">Unique name of the worker.</param>
        /// <param name="linkedToken">The linked token used to monitor for cancellation requests.</param>
        /// <param name="userRequestToken">The user request token used to validate against the linked token.</param>
        internal WorkerCore(IWorker parentWorker, IWorkerService parentService, int workerId, string workerName, CancellationToken linkedToken, CancellationToken userRequestToken)
        {
            // Set the Worker.
            this.Worker = parentWorker;

            // Set the Worker Service.
            this.WorkerService = parentService;

            // Set the id of the Worker.
            this.WorkerId = workerId;
            this.WorkerName = workerName;

            // Set the user request token.
            this.UserRequestToken = userRequestToken;

            // Create the token source for worker timeouts.
            this.WorkerTimeoutTokenSource = new CancellationTokenSource();

            // Link the worker timeout token source with the token passed in.
            this.LinkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(linkedToken, this.WorkerTimeoutTokenSource.Token);

            // Prep the working directory.
            CreateDirectoryIfItDoesntExist(WorkerServiceConfiguration.OutputFilePath);
        }

        #endregion

        #region Public Static Properties

        /// <summary>
        /// Gets the output file path used by the IWorker instance.
        /// </summary>
        public static string OutputPath
        {
            get { return WorkerServiceConfiguration.OutputFilePath; }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the worker associated with this worker busines logic instance.
        /// </summary>
        public IWorker Worker { get; private set; }

        /// <summary>
        /// Gets the worker service associated with the worker.
        /// </summary>
        public IWorkerService WorkerService { get; private set; }

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        /// <value>The name of the service.</value>
        public string ServiceName
        {
            get { return this.WorkerService == null ? null : this.WorkerService.ServiceNameAndServer; }
        }

        /// <summary>
        /// Gets the numerical ID of the worker. Should be unique to the server instance it belongs to.
        /// </summary>
        public int WorkerId { get; private set; }

        /// <summary>
        /// Gets the name of the worker. Should be unique.
        /// </summary>
        public string WorkerName { get; private set; }

        /// <summary>
        /// Gets the token used for signaling a cancellation request to the worker task. This request could originate from various sources.
        /// </summary>
        public CancellationToken LinkedToken
        {
            get { return this.LinkedTokenSource.Token; }
        }

        /// <summary>
        /// Gets the user requested cancellation token that the worker process validates against while monitoring the linked token.
        /// </summary>
        public CancellationToken UserRequestToken { get; private set; }

        /// <summary>
        /// Gets the token source for timeout cancellations. Allows us to attempt to exit a thread gracefully before aborting.
        /// </summary>
        public CancellationTokenSource WorkerTimeoutTokenSource { get; private set; }

        /// <summary>
        /// Gets the linked token source, joining the passed in token with the local timeout token.
        /// </summary>
        public CancellationTokenSource LinkedTokenSource { get; private set; }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Creates the specified directory if it doesn't exist.
        /// </summary>
        /// <param name="directory">The directory to create.</param>
        public static void CreateDirectoryIfItDoesntExist(string directory)
        {
            try
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
            }
            catch (IOException ex)
            {
                Scheduler.ExceptionHandler.HandleWorkerServiceException(ex);
                throw;
            }
            catch (UnauthorizedAccessException ex)
            {
                Scheduler.ExceptionHandler.HandleWorkerServiceException(ex);
                throw;
            }
            catch (ArgumentException ex)
            {
                Scheduler.ExceptionHandler.HandleWorkerServiceException(ex);
                throw;
            }
            catch (NotSupportedException ex)
            {
                Scheduler.ExceptionHandler.HandleWorkerServiceException(ex);
                throw;
            }
        }

        /// <summary>
        /// Saves the results back into the queue.
        /// By default, this does not update all the fields on the queueEntry record in the database.
        /// That can be achieved by overriding this method in your derived this.Worker class and calling
        /// an overload on the Queue object.
        /// </summary>
        /// <param name="queue">The queue.</param>
        /// <param name="queueEntry">The queue entry.</param>
        /// <param name="message">The message.</param>
        /// <param name="success">If the results message status was successful.</param>
        public static void SaveResults(IReportSchedule reportSchedule, IReportScheduleEntry reportScheduleEntry)
        {
            if (reportSchedule != null && reportScheduleEntry != null)
            {
                reportSchedule.SaveResults(reportScheduleEntry.ReportScheduleId, reportScheduleEntry);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// overloaded method needed for WaitCallback delegate used by ThreadPool for multi-threading
        /// Has watchdog code to time out the ProcessQueue if it runs too long
        /// </summary>
        /// <param name="queue">Queue to use in processing the entry.</param>
        /// <param name="reportScheduleEntry">An item from the queue</param>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is a worker thread. We want to capture and log all errors but not tear down service.")]
        public void ProcessQueue(IReportSchedule reportSchedule, IReportScheduleEntry reportScheduleEntry)
        {
            try
            {
                bool success = true;

                if (reportScheduleEntry != null)
                {
                    try
                    {
                        // Create a new thread to execute the request, so we can abort if the process
                        // exceeds the specified timeout.
                        // We wrap the execution in a delegate so we can catch and ignore
                        // OperationCanceledException exceptions caused by cancellation requests.
                        // This allows the thread to exit gracefully.
                        // https://msdn.microsoft.com/en-us/library/ms228965(v=vs.100).aspx
                        Thread workThread = new Thread(
                            new ThreadStart(
                                () =>
                                {
                                    try
                                    {
                                        // Execute the process
                                        new TaskEngine().Process(reportScheduleEntry);
                                    }
                                    catch (OperationCanceledException exception)
                                    {
                                        // Log the exception then exit gracefully.
                                        try
                                        {
                                            Scheduler.ExceptionHandler.HandleWorkerServiceProcessException(exception);
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }));

                        // Set worker thread to background.
                        workThread.IsBackground = true;

                        // Create stop watch for monitoring time spent processing.
                        Stopwatch workThreadStopwatch = new Stopwatch();

                        // Simultaneously start both the thread and the stop watch.
                        Parallel.Invoke(workThread.Start, workThreadStopwatch.Start);

                        // Wait for configured time, or until thread has completed.
                        while (!workThread.Join(ThreadJoinIntervalMS))
                        {
                            // If time is greater than configured timeout, abort thread.
                            if (workThreadStopwatch.ElapsedMilliseconds >= WorkerServiceConfiguration.WorkerThreadTimeoutMS)
                            {
                                // First signal cancellation.
                                this.WorkerTimeoutTokenSource.Cancel();

                                // Then give the thread a chance to exit gracefully.
                                if (!workThread.Join(WorkerServiceConfiguration.WorkerThreadMaxWaitTime))
                                {
                                    // If the thread did not exit gracefully, abort the thread.
                                    workThread.Abort();
                                }

                                // Throw a worker timeout exception.
                                this.ThrowWorkerTimedOutException();
                            }

                            // If cancellation request from any source but a user request, give the thread
                            // a certain amount of time to exit gracefully, then abort the thread if it does not respond.
                            if (this.LinkedToken.IsCancellationRequested
                                && !this.UserRequestToken.IsCancellationRequested)
                            {
                                // Give the thread a chance to exit gracefully.
                                if (!workThread.Join(WorkerServiceConfiguration.WorkerThreadMaxWaitTime))
                                {
                                    // If not, abort the thread.
                                    workThread.Abort();
                                }
                            }
                        }

                        // Stop the stopwatch.
                        workThreadStopwatch.Stop();
                    }
                    catch (OperationCanceledException exception)
                    {
                        // Log the exception.
                        Scheduler.ExceptionHandler.HandleWorkerServiceProcessException(exception);

                        // Only update status and reset queue entry if there is no cancellation request originating from a user.
                        if (!this.UserRequestToken.IsCancellationRequested)
                        {
                            // Set success flag to false.
                            success = false;

                            // Set status to failed.
                            reportScheduleEntry.Status = (int)ScheduleStatus.Failure;

                            // Reset the entry in the queue.
                            reportSchedule.ProcessResetEntry(reportScheduleEntry);
                        }
                    }
                    catch (WorkerThreadTimeoutException exception)
                    {
                        // Log the exception.
                        Scheduler.ExceptionHandler.HandleWorkerServiceProcessException(exception);

                        // It is possible for a worker thread with a cancellation request
                        // to time out if the worker has not implemented cooperative cancellation.
                        // In this case we simply ignore the error.
                        if (!this.UserRequestToken.IsCancellationRequested)
                        {
                            // set failed status and log error
                            success = false;
                            reportScheduleEntry.Status = (int)ScheduleStatus.Failure;
                        }
                    }
                    catch (Exception exception)
                    {
                        // Log the exception.
                        Scheduler.ExceptionHandler.HandleWorkerServiceProcessException(exception);

                        // set failed status and log error
                        success = false;
                        reportScheduleEntry.Status = (int)ScheduleStatus.Failure;
                    }
                    finally
                    {
                        // Only update request if there is no cancellation request originating from a user.
                        if (!this.UserRequestToken.IsCancellationRequested)
                        {
                            // Save results of processing.
                            reportScheduleEntry.Status = ((reportScheduleEntry.Status != (int)ScheduleStatus.Failure) && (success == true)) ? (int)ScheduleStatus.Success : (int)ScheduleStatus.Failure;
                            this.Worker.SaveResults(reportSchedule, reportScheduleEntry);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // exceptions with dequeuing, should be rare
                try
                {
                    Scheduler.ExceptionHandler.HandleWorkerServiceProcessException(ex);
                }
                catch
                {
                    // if the error can't be logged, then don't crash the service
                }
            }
        }

        /// <summary>
        /// Throws an exception, to be used when the workerthread timeout is reached
        /// </summary>
        /// <exception cref="WorkerThreadTimeoutException">Always thrown.</exception>
        public void ThrowWorkerTimedOutException()
        {
            throw new WorkerThreadTimeoutException();
        }

        #endregion

    }
}
