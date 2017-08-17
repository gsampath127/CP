using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Scheduler;
using RRD.FSG.RP.Scheduler.Interfaces;
using RRD.FSG.RP.Services.ScheduleTasks.Interfaces;
using RRD.FSG.RP.Utilities;
using System.ServiceProcess;
using System.Threading;

namespace RRD.FSG.RP.Services.ScheduleTasks
{
    /// <summary>
    /// Partial class that runs a service which schedules the tasks 
    /// </summary>
    public partial class ScheduleTasksService : ServiceBase, IWorkerService
    {
        #region Private Read Only Fields

        /// <summary>
        /// Core business logic for the service.
        /// </summary>
        private readonly WorkerServiceCore workerServiceCore;

        #endregion

        #region Public Virtual Properties

        /// <summary>
        /// Gets the name of the service.
        /// Used for the ServiceNameAndServer propert as well as determining the MSMQ Queue name (if used).
        /// </summary>
        public virtual string WorkerServiceName
        {
            get { return this.workerServiceCore.WorkerServiceName; }
        }

        /// <summary>
        /// Gets the Service Name an Server as a single identifier.
        /// </summary>
        public virtual string ServiceNameAndServer
        {
            get { return this.workerServiceCore.ServiceNameAndServer; }
        }

        /// <summary>
        /// Gets a value indicating whether or not to grab service name from MMC. Gets around a bug in .NET Service implementation.
        /// </summary>
        public virtual bool SetServerNameAndServerFromServiceQuery
        {
            get { return true; }
        }

        /// <summary>
        /// Gets  a cancellation token that indicates whether the service has been stopped.
        /// </summary>
        public virtual CancellationToken ServiceStopToken
        {
            get { return this.workerServiceCore.ServiceStopToken; }
        }

        #endregion

        #region Public Virtual Methods

        /// <summary>
        /// Retrieves a new Queue instance specifically for this service.
        /// The base class method returns the most basic implementation of IQueue, Queue.
        /// If another type of IQueue is desired, please override this method to do so.
        /// </summary>
        /// <returns>Queue instance.</returns>
        public virtual IReportSchedule NewReportSchedule()
        {
            DataAccess dataAccess = new DataAccess();
            return new ReportSchedule(WorkerServiceConfiguration.WorkerServiceDBConnectionString, this.ServiceNameAndServer, WorkerServiceConfiguration.ErrorQueueName, dataAccess);
        }

        /// <summary>
        /// Loads the Worker.
        /// </summary>
        /// <param name="workerId">Unique number to represent the worker.</param>
        /// <param name="workerName">Unique name to represent the worker.</param>
        /// <param name="linkedToken">The linked token used to monitor for cancellation requests.</param>
        /// <param name="userRequestToken">The user request token used to validate against the linked token.</param>
        /// <returns>The application specific Worker.</returns>
        public virtual IWorker NewWorker(int workerId, string workerName, CancellationToken linkedToken, CancellationToken userRequestToken)
        {
            return new ReportWorker(this, workerId, workerName, linkedToken, userRequestToken);
        }

        #endregion
                
        /// <summary>
        /// Constructor that intializes the component
        /// </summary>
        public ScheduleTasksService()
        {
            this.workerServiceCore = new WorkerServiceCore(this);
            InitializeComponent();
        }

        #region Protected Method Overrides

        /// <summary>
        /// Event handler for handling starting the service.
        /// </summary>
        /// <param name="args">The arguments used for starting the service.</param>
        protected override void OnStart(string[] args)
        {
            this.Process();
        }

        /// <summary>
        /// Event handler for handling stopping the service.
        /// </summary>
        protected override void OnStop()
        {
            this.workerServiceCore.ServiceStop();
            this.ExitCode = 0;
        }

        #endregion

        /// <summary>
        /// Schedules the process
        /// </summary>
        public void Process()
        {
            RPV2Resolver.LoadConfiguration();
            this.workerServiceCore.ServiceStart();
        }
    }
}
