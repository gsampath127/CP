using RRD.DSA.Core;
using Scheduler = RRD.FSG.RP.Scheduler;
using RRD.FSG.RP.Scheduler.Interfaces;
using RRD.FSG.RP.Services.ScheduleTasks.Interfaces;
using RRD.FSG.RP.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace RRD.FSG.RP.Services.ScheduleTasks
{
    /// <summary>
    /// Core business logic for the IWorkerService implementations, specifically for WorkerServiceBase.
    /// </summary>
    internal sealed class WorkerServiceCore : IDisposable
    {
        #region Read Only Fields

        /// <summary>
        /// High precision timer used to poll/process a queue.
        /// </summary>
        private readonly HighPrecisionTimer workerServiceTimer = new HighPrecisionTimer();

        /// <summary>
        /// Cleanup process Timer
        /// </summary>
        private readonly System.Timers.Timer cleanupTimer = new System.Timers.Timer();

        /// <summary>
        /// List of active tasks. Used for joining back to main thread when service is stopping.
        /// </summary>
        private readonly List<WorkerTask> workerTasks = new List<WorkerTask>();

        /// <summary>
        /// Worker service implementation utilizing this instance of the business logic class.
        /// </summary>
        private readonly IWorkerService workerService;

        /// <summary>
        /// Cancellation token source used by various threads to signal stopping of service.
        /// </summary>
        private readonly CancellationTokenSource serviceStopTokenSource = new CancellationTokenSource();

        #endregion

        #region Fields

        /// <summary>
        /// Thread for the service worker, set when executed in order to provide a handle for joining when the service is stopped.
        /// </summary>
        private WeakReference serviceWorkerTask = null;

        /// <summary>
        /// Thread for the cleanup tasks, set when executed in order to provide a handle for joining when the service is stopped.
        /// </summary>
        private WeakReference cleanupTask = null;

        /// <summary>
        /// Flag for whether or not the instance is disposed.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Holds total amount of workers spawned.
        /// </summary>
        private int totalWorkersSpawned = 0;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:WorkerServiceCore"/> class.
        /// </summary>
        /// <param name="service">Worker service implementation utilizing the core business logic.</param>
        internal WorkerServiceCore(IWorkerService service)
        {
            this.workerService = service;
            try
            {
                // Attach event handler for task scheduler as "Catch all" for faulted tasks.
                TaskScheduler.UnobservedTaskException += new EventHandler<UnobservedTaskExceptionEventArgs>(this.TaskSchedulerUnobservedTaskException);

                // Set the cancellation token used to broker stop requests to Tasks.
                this.ServiceStopToken = this.serviceStopTokenSource.Token;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleWorkerServiceException(ex);
                throw;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the name of the service.
        /// Used for the ServiceNameAndServer propert as well as determining the MSMQ Queue name (if used).
        /// </summary>
        public string WorkerServiceName { get; private set; }

        /// <summary>
        /// Gets the Service Name and Machine Name as a single unique identifier.
        /// </summary>
        public string ServiceNameAndServer
        {
            get { return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", Environment.MachineName, this.workerService.WorkerServiceName); }
        }

        /// <summary>
        /// Gets a cancellation token that indicates whether the service has been stopped.
        /// </summary>
        public CancellationToken ServiceStopToken { get; private set; }

        /// <summary>
        /// Gets the ReportSchedule associated with this service.
        /// </summary>
        public IReportSchedule ReportSchedule { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the service is running.
        /// </summary>
        public bool IsRunning { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes and starts timers for the service.
        /// </summary>
        public void ServiceStart()
        {
            // Delay startup if configured to do so in order to allow debugging.
            AddDelayForDebug();

            // Start cleanup timer. Runs based on the cleanup timer interval.
            if (!this.cleanupTimer.Enabled)
            {
                this.cleanupTimer.Interval = WorkerServiceConfiguration.CleanupTimerInterval;
                this.cleanupTimer.Elapsed += new ElapsedEventHandler(this.CleanupTimerElapsed);
                this.cleanupTimer.Start();
            }

            // If configured to do so, get the name of the service.
            if (this.workerService.SetServerNameAndServerFromServiceQuery)
            {
                this.WorkerServiceName = GetMMCServiceName();
            }

            // Ensure the queue.
            if (!this.EnsureReportSchedule())
            {
                // If queue cannot be ensured, throw an exception.
                throw new InvalidOperationException("Cannot ensure queue, so service cannot start.");
            }

            // Start worker service timer to instantiate worker tasks as needed.
            // Initial event occurs 500ms after on start.
            // Subsequent events use the value specified in the configuration (or 5 seconds if not set or invalid).
            if (!this.workerServiceTimer.Running)
            {
                this.workerServiceTimer.TimerEvent += new TimerEvent(this.WorkerServiceTimerEvent);
                this.workerServiceTimer.Start(WorkerServiceConfiguration.InitialWorkerServiceTimerInterval);
            }

            // Set running flag to true.
            this.IsRunning = true;

            //while (true)
            //{
                
            //}
        }

        /// <summary>
        /// Stops all timers and joins all threads.
        /// </summary>
        public void ServiceStop()
        {
            // Request cancellation to all running threads via the service stop token source.
            this.serviceStopTokenSource.Cancel();

            // Stop timers.
            this.workerServiceTimer.Stop();
            this.cleanupTimer.Stop();

            // Request additional time for joining to worker tasks.
            this.workerService.RequestAdditionalTime(WorkerServiceConfiguration.WorkerThreadMaxWaitTime);

            // Wait for all worker tasks to complete.
            try
            {
                // Lock the worker tasks collection. This blocks any modifications to collection during the wait.
                lock (this.workerTasks)
                {
                    Task.WaitAll((from workerTask in this.workerTasks
                                  where workerTask.TaskReference.IsAlive
                                  select (Task)workerTask.TaskReference.Target).ToArray());
                }
            }
            catch (AggregateException ex)
            {
                HandleTaskWaitAggregateException(ex);
            }

            // Join tasks launched by timers (if running).
            this.WaitForTask(this.serviceWorkerTask);
            this.WaitForTask(this.cleanupTask);

            // Set running flag to false.
            this.IsRunning = false;
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Disposes of managed resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Private Static Methods

        /// <summary>
        /// Handle the aggregate exception thrown after waiting for a task to cancel.
        /// There should only be OperationCanceledException instances. Anything else will be logged and rethrown.
        /// </summary>
        /// <param name="aggregateException">Exception to flatten and check.</param>
        /// <exception cref="ArgumentExcception">Thrown if one or more exceptions in the original list were not of type OperationCanceledException.</exception>
        private static void HandleTaskWaitAggregateException(AggregateException aggregateException)
        {
            List<Exception> nonCancelledExceptions = new List<Exception>();

            // Flatten the exception
            AggregateException flattened = aggregateException.Flatten();
            foreach (Exception exception in flattened.InnerExceptions)
            {
                // Grab all exceptions except operation canceled exceptions.
                if (!(exception is OperationCanceledException))
                {
                    // Add to list of non cancelled exceptions
                    nonCancelledExceptions.Add(exception);

                    // Log the exception
                    ExceptionHandler.HandleWorkerServiceProcessException(exception);
                }
            }

            // If any exist, rethrow as a new aggregate.
            if (nonCancelledExceptions.Count > 0)
            {
                throw new AggregateException(nonCancelledExceptions);
            }
        }

        /// <summary>
        /// Delays the startup of the service if configured to do so in order for developers to be able to attach a debugger to the service.
        /// </summary>
        private static void AddDelayForDebug()
        {
            if (WorkerServiceConfiguration.AddDelayForDebug)
            {
                Thread.Sleep(60000);
            }
        }

        /// <summary>
        /// Gets the name of the current service from the MMC.
        /// Calling System.ServiceProcess.ServiceBase::ServiceName always returns
        /// an empty string,
        /// see https://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=387024
        /// So we have to do some more work to find out our service name, this only works if 
        /// the process contains a single service, if there are more then one services hosted
        /// in the process you will have to do something else
        /// </summary>
        /// <returns>The name of the current service in the MMC.</returns>
        /// <exception cref="Exception">Thrown if any error encountered while trying to retrieve the service name.</exception>
        private static string GetMMCServiceName()
        {
            string serviceName = string.Empty;
            int retryCount = 0;
            int processId = Process.GetCurrentProcess().Id;

            while (string.IsNullOrEmpty(serviceName))
            {
                try
                {
                    using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM Win32_Service where ProcessId  = {0}", processId)))
                    {
                        foreach (ManagementObject queryObj in searcher.Get())
                        {
                            serviceName = queryObj["Name"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    retryCount++;
                    if (retryCount < WorkerServiceConfiguration.ServiceNameMaxRetries)
                    {
                        Thread.Sleep(WorkerServiceConfiguration.ServiceNameRetryInterval);
                    }
                    else
                    {
                        throw new Exception("Unable to retrieve the name of the service from the MMC.", ex);
                    }
                }
            }

            return serviceName;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Elapsed event for cleanup timer.
        /// </summary>
        /// <param name="sender">Timer that is firing the event.</param>
        /// <param name="e">Event arguments for the timer.</param>
        private void CleanupTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // Only run if service is not stopped or stopping.
            if (!this.ServiceStopToken.IsCancellationRequested && this.cleanupTimer.Enabled)
            {
                // First stop the timer to prevent an overlap of events.
                this.cleanupTimer.Stop();

                // Then delete the output files, setting a reference to the Task.
                this.cleanupTask = new WeakReference(Task.Factory.StartNew(this.DeleteOldOutputFiles, this.ServiceStopToken)
                    .ContinueWith(Scheduler.ExceptionHandler.FaultedWorkerServiceTaskHandler, this.ServiceStopToken, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Default));
            }
        }

        /// <summary>
        /// Event handler for observing the unhandled exception.
        /// </summary>
        /// <param name="sender"> task that raises the exception</param>
        /// <param name="e">Event arguments that holds the aggregate exception</param>
        private void TaskSchedulerUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            if (e != null)
            {
                if (e.Exception != null)
                {
                    // Flatten exceptions to one encompassing list and then log each exception.
                    foreach (Exception exception in e.Exception.Flatten().InnerExceptions)
                    {
                        ExceptionHandler.HandleWorkerServiceProcessException(exception);
                    }

                    // Finally set all exceptions as observed so application teardown does not occur.
                    e.SetObserved();
                }
            }
        }

        #endregion EventHandlers

        #region Private Methods

        /// <summary>
        /// Disposes of managed resources if disposing is true.
        /// </summary>
        /// <param name="disposing">determines whether to dispose of managed resources.</param>
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // First, stop the service if it is still running.
                    if (this.IsRunning)
                    {
                        this.ServiceStop();
                    }

                    // Dispose of the worker service timer.
                    this.workerServiceTimer.Dispose();

                    // Dispose of the cleanup timer.
                    this.cleanupTimer.Dispose();

                    // Dispose of the service stop token source. This will cause a chain of events that disposes
                    // all linked token sources, but NOT the user cancellation token source that is part of the link.
                    this.serviceStopTokenSource.Dispose();

                    // Dispose of any worker tasks left.
                    lock (this.workerTasks)
                    {
                        foreach (WorkerTask workerTask in this.workerTasks)
                        {
                            // Dispose of the workerTask.
                            workerTask.Dispose();
                        }
                    }
                }

                this.disposed = true;
            }
        }

        #region WaitForTask Overloads

        /// <summary>
        /// Attempts to join a Task back to the main thread, requesting additional time for any pending operation to complete the join.
        /// Not meant to be used on worker tasks.
        /// </summary>
        /// <param name="taskReference">Task to join.</param>
        private void WaitForTask(WeakReference taskReference)
        {
            // Only attempt if thread reference is valid.
            if (taskReference != null && taskReference.IsAlive)
            {
                this.WaitForTask(taskReference.Target as Task);
            }
        }

        /// <summary>
        /// Attempts to join a Task back to the main thread, requesting additional time for any pending operation to complete the join.
        /// Not meant to be used on worker tasks.
        /// </summary>
        /// <param name="task">Task to join.</param>
        private void WaitForTask(Task task)
        {
            if (task != null && !task.IsCompleted)
            {
                this.workerService.RequestAdditionalTime(WorkerServiceConfiguration.ThreadJoinWaitIntervalMS);
                try
                {
                    task.Wait(WorkerServiceConfiguration.ThreadJoinWaitIntervalMS);
                }
                catch (OperationCanceledException ex)
                {
                    // If the operation canceled exception was was caused by another cancellation request, log and rethrow.
                    if (ex.CancellationToken != this.ServiceStopToken)
                    {
                        ExceptionHandler.HandleWorkerServiceProcessException(ex);
                        throw;
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// This is the primary callback for the service.
        /// It's main purpose is to replace Tasks that have completed due to either no work or faults.
        /// </summary>
        /// <param name="time">Time in ms showing discrepancy between intended time of execution and actual time of execution. Required for the precision timer event handler, however we don't use it.</param>
        private void WorkerServiceTimerEvent(long time)
        {
            // Only perform action if timer thread is still running.
            if (this.workerServiceTimer.Running)
            {
                // Then stop the timer so there is no overlapping.
                this.workerServiceTimer.Stop();

                // Then queue the worker tasks, setting a reference to the task.
                this.serviceWorkerTask = new WeakReference(Task.Factory.StartNew(this.ReportScheduleWorkerTasks, this.ServiceStopToken)
                    .ContinueWith(Scheduler.ExceptionHandler.FaultedWorkerServiceTaskHandler, this.ServiceStopToken, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Default));
            }
        }

        /// <summary>
        /// This method instantiates enough worker tasks to fill the max number of threads set in the configuration.
        /// </summary>
        private void ReportScheduleWorkerTasks()
        {
            try
            {
                // Acquire a lock for modifying tasks collection.
                lock (this.workerTasks)
                {
                    // Build an array of items that should be removed from the task list (no valid running Task instance).
                    WorkerTask[] tasksToRemove = this.workerTasks.Where(workerTask =>
                    {
                        Task task = null;
                        if (workerTask.TaskReference.IsAlive)
                        {
                            task = workerTask.TaskReference.Target as Task;
                        }

                        return task == null || task.IsCompleted;
                    }).ToArray();

                    // Remove and dispose of each item in the list
                    foreach (WorkerTask workerTask in tasksToRemove)
                    {
                        this.workerTasks.Remove(workerTask);
                        workerTask.Dispose();
                    }

                    // Declare reference for queue entry to be used within loop below.
                    IReportScheduleEntry entry = null;

                    // As long as there is work to do, start enough tasks to fill up our max thread value.
                    // This replaces threads that have exited due to either no work being available or to faulting.
                    for (int workerCount = this.workerTasks.Count; !this.ServiceStopToken.IsCancellationRequested && workerCount < WorkerServiceConfiguration.MaxWorkerThreads; workerCount++)
                    {
                        // First lets see if there is an entry. No point in launching a Task if there is no work.
                        entry = this.GetScheduleEntry(this.ServiceStopToken);
                        if (entry == null)
                        {
                            break;
                        }

                        // If we have work to be done, launch a Task and do it!
                        this.workerTasks.Add(this.ReportScheduleWorkerTask(entry));
                    }
                }

                // Throw Operation Cancelled if necessary.
                this.ServiceStopToken.ThrowIfCancellationRequested();
            }
            finally
            {
                // Finally start the timer back up.
                if (!this.ServiceStopToken.IsCancellationRequested)
                {
                    this.workerServiceTimer.Start(WorkerServiceConfiguration.WorkerServiceTimerInterval);
                }
            }
        }

        /// <summary>
        /// Verifies service queue has been initialized.
        /// </summary>
        /// <returns>True is successful, false if not.</returns>
        private bool EnsureReportSchedule()
        {
            // Only instantiate the queue if not already done.
            if (this.ReportSchedule == null)
            {
                // Get the queue from the worker service.
                this.ReportSchedule = this.workerService.NewReportSchedule();

                // Reset any jobs this service is already processing.
                this.ReportSchedule.ProcessReset();
            }

            // Return true if queue instantiation is successful, false otherwise.
            return this.ReportSchedule != null;
        }

        /// <summary>
        /// Checks to see if the service should still be processing entries.
        /// If so, returns the next report schedule from the database.
        /// If not, returns null.
        /// </summary>
        /// <param name="token">Cancellation token to check before returning an item.</param>
        /// <returns>null if service should not be processing, false otherwise.</returns>
        private IReportScheduleEntry GetScheduleEntry(CancellationToken token)
        {
            // Always return null if cancellation requested.
            if (token.IsCancellationRequested)
            {
                return null;
            }

            // Return next item from ReportSchedule.
            return this.ReportSchedule.Process();
        }

        /// <summary>
        /// Spins up and returns a new worker task.
        /// </summary>
        /// <param name="entry">Entry to be passed to new Task as initial work.</param>
        /// <returns>The running task.</returns>
        private WorkerTask ReportScheduleWorkerTask(IReportScheduleEntry entry)
        {
            // Start the new task.
            return new WorkerTask(this, entry);
        }

        /// <summary>
        /// Delete the old output files
        /// </summary>
        private void DeleteOldOutputFiles()
        {
            try
            {
                if (Directory.Exists(WorkerServiceConfiguration.OutputFilePath))
                {
                    // Only delete files if service is not stopped or stopping.
                    if (!this.ServiceStopToken.IsCancellationRequested)
                    {
                        DateTime olderThanDate = DateTime.Now.AddDays(0 - WorkerServiceConfiguration.DaysOld);
                        this.DeleteOldFiles(WorkerServiceConfiguration.OutputFilePath, olderThanDate);
                    }
                }

                // Throw Operation Cancelled if necessary.
                this.ServiceStopToken.ThrowIfCancellationRequested();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleWorkerServiceException(ex);
            }
            finally
            {
                // Finally restart timer
                if (!this.ServiceStopToken.IsCancellationRequested)
                {
                    this.cleanupTimer.Start();
                }
            }
        }

        /// <summary>
        /// Deletes old temporary files.
        /// </summary>
        /// <param name="directory">The directory containing the files.</param>
        /// <param name="olderThanDate">The date to use as the cutoff for deleting files.</param>
        private void DeleteOldFiles(string directory, DateTime olderThanDate)
        {
            // Recursively call on all subdirectories to walk the entire hierarchy.
            foreach (string subdirectory in Directory.GetDirectories(directory))
            {
                if (!this.ServiceStopToken.IsCancellationRequested)
                {
                    this.DeleteOldFiles(subdirectory, olderThanDate);
                }
            }

            // Delete all files in current directory that match criteria.
            foreach (string filePath in Directory.GetFiles(directory))
            {
                if (!this.ServiceStopToken.IsCancellationRequested)
                {
                    // delete files older than a day old
                    if (File.GetLastWriteTime(filePath) < olderThanDate)
                    {
                        File.Delete(filePath);
                    }
                }
            }
        }

        #endregion

        #region Internal Classes

        /// <summary>
        /// Provides the interface between the Task, IWorker instance, and report schedule entry.
        /// Contains the actual worker task action method.
        /// </summary>
        private sealed class WorkerTask
            : IDisposable
        {
            #region Private Read Only Fields

            /// <summary>
            /// Gets or sets a reference to the parent core instance.
            /// </summary>
            private readonly WorkerServiceCore Core = null;

            #endregion

            #region Constructors

            /// <summary>
            /// Initializes a new instance of the <see cref="WorkerTask"/> class.
            /// </summary>
            /// <param name="core">worker service core instance that spawned the task.</param>
            /// <param name="entry">first entry the task will process.</param>
            public WorkerTask(WorkerServiceCore core, IReportScheduleEntry reportScheduleEntry)
            {
                // Set a reference to the core instance.
                this.Core = core;

                // Set a reference to the report schedule entry.
                this.ReportScheduleEnrty = reportScheduleEntry;

                // Create the user cancellation token source. This allows just this task to be cancelled.
                this.UserRequestTokenSource = new CancellationTokenSource();

                // Create the linked cancellation token source.
                // This allows the Task to take cancellations from both the user cancellation token and the main stop token.
                this.ServiceStopAndUserRequestLinkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(this.Core.ServiceStopToken, this.UserRequestTokenSource.Token);

                // Start a new Task instance and continuation tasks for error handling and recovery.
                this.StartTask();
            }

            #endregion

            #region Properties

            /// <summary>
            /// Gets the Entry associated with the thread.
            /// </summary>
            public IReportScheduleEntry ReportScheduleEnrty { get; private set; }

            /// <summary>
            /// Gets the User Requested Cancellation Token Source, used for signaling cancellations from users.
            /// </summary>
            public CancellationTokenSource UserRequestTokenSource { get; private set; }

            /// <summary>
            /// Gets the reference to the Task instance that does the work.
            /// </summary>
            public WeakReference TaskReference { get; private set; }

            /// <summary>
            /// Gets the linked token source that is passed to the Task.
            /// This allows the task to receive signalling from both sources.
            /// </summary>
            public CancellationTokenSource ServiceStopAndUserRequestLinkedTokenSource { get; private set; }

            

            #endregion

            #region #IDisposable Members

            /// <summary>
            /// Allows disposing of the cancellation token source objects.
            /// </summary>
            public void Dispose()
            {
                // Only dispose of the non linked token. The linked token gets disposed of from the ServiceStopToken disposal.
                this.UserRequestTokenSource.Dispose();
            }

            #endregion

            #region Private Methods

            /// <summary>
            /// Instantiates a new Task instance and sets continuation tasks for error handling and recovery.
            /// </summary>
            private void StartTask()
            {
                // Instantiate and start the task.
                Task task = Task.Factory.StartNew(this.WorkerTaskAction, this.ServiceStopAndUserRequestLinkedTokenSource.Token);

                // Set task fault handler to log exceptions.
                task.ContinueWith(Scheduler.ExceptionHandler.FaultedWorkserServiceProcessTaskHandler, this.ServiceStopAndUserRequestLinkedTokenSource.Token, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Default);

                // Set additional task fault handler to replace faulted task with a new one.
                // NOTE: This last step might not be necessary with change in architecture.
                task.ContinueWith(this.ReScheduleTaskAfterFault, this.ServiceStopAndUserRequestLinkedTokenSource.Token, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Default);

                // Set a weak reference to the task for waiting during service stop requests.
                // We don't hold a strong reference to the task, as it is generally ill advised
                // to handle disposing of parallel task objects even though they implement the IDisposable.
                // Per MS, better to let either TaskScheduler or Garbage Collection handle this when they go out of scope.
                this.TaskReference = new WeakReference(task);
            }

            /// <summary>
            /// The  worker task: this pulls items out of the queue and processes them.
            /// </summary>
            private void WorkerTaskAction()
            {
                // Set a reference to the thread. Only used when forcing a cancellation, such as a hung thread.
                //this.ThreadReference = new WeakReference(Thread.CurrentThread);

                // main IWorker instance for this worker task.
                IWorker worker = null;

                // Get worker id and increment count.
                int workerId = Interlocked.Increment(ref this.Core.totalWorkersSpawned);

                // Generate unique worker name.
                string workerName = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", this.Core.workerService.ServiceNameAndServer, workerId);

                // Intialize the local worker instance.
                worker = this.Core.workerService.NewWorker(workerId, workerName, this.ServiceStopAndUserRequestLinkedTokenSource.Token, this.UserRequestTokenSource.Token);

                // Process entries as long as the service is not stopping and the report schedule returns entries.
                for (; this.ReportScheduleEnrty != null; this.ReportScheduleEnrty = this.Core.GetScheduleEntry(this.ServiceStopAndUserRequestLinkedTokenSource.Token))
                {
                    worker.ProcessQueue(this.Core.ReportSchedule, this.ReportScheduleEnrty);
                }

                // Throw OperationCanceledException if cancellation requested.
                this.ServiceStopAndUserRequestLinkedTokenSource.Token.ThrowIfCancellationRequested();
            }

            /// <summary>
            /// Removes references to the faulted task and requeues a task to replace it.
            /// </summary>
            /// <param name="faultedTask">A reference to the faulted task.</param>
            private void ReScheduleTaskAfterFault(Task faultedTask)
            {
                // Only check and requeue if no cancellation is requested.
                if (!this.ServiceStopAndUserRequestLinkedTokenSource.IsCancellationRequested)
                {
                    // Check to see if there is any work to be done.
                    IReportScheduleEntry entry = this.Core.GetScheduleEntry(this.Core.ServiceStopToken);

                    // If work, set it and start a Task.
                    if (entry != null)
                    {
                        this.ReportScheduleEnrty = entry;
                        this.StartTask();
                    }
                }
            }

            #endregion
        }

        #endregion
    }
}
