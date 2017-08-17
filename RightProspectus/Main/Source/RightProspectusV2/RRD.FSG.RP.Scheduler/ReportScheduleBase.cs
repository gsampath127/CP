// ***********************************************************************
// Assembly         : RRD.FSG.RP.Scheduler
// Author           : 
// Created          : 10-29-2015
//
// Last Modified By : 
// Last Modified On : 11-13-2015
// ***********************************************************************
using RRD.FSG.RP.Scheduler.Interfaces;

namespace RRD.FSG.RP.Scheduler
{
    /// <summary>
    /// Class ReportScheduleBase.
    /// </summary>
    /// <typeparam name="TScheduleEntry">The type of the t schedule entry.</typeparam>
    public abstract class ReportScheduleBase<TScheduleEntry> 
        : IReportSchedule<TScheduleEntry>
        where TScheduleEntry : class, IReportScheduleEntry
    {
        #region Public Properties

        /// <summary>
        /// Gets the report scheduler identifier.
        /// </summary>
        /// <value>The report scheduler identifier.</value>
        public int ReportSchedulerId { get; private set; }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        /// <value>The name of the service.</value>
        protected string ServiceName { get; private set; }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        protected string ConnectionString { get; private set; }

        /// <summary>
        /// Gets the name of the error report schedule.
        /// </summary>
        /// <value>The name of the error report schedule.</value>
        protected string ErrorReportScheduleName { get; private set; }

        /// <summary>
        /// Gets or sets the factory.
        /// </summary>
        /// <value>The factory.</value>
        protected IReportScheduleEntryFactory<TScheduleEntry> Factory { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueOf(TQueueEntry)" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="errorReportScheduleName">Name of the error report schedule.</param>
        protected ReportScheduleBase(string connectionString, string serviceName, string errorReportScheduleName)
        {
            this.ConnectionString = connectionString;
            this.ServiceName = serviceName;
            this.ErrorReportScheduleName = errorReportScheduleName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Queue" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="serviceName">Name of the service.</param>
        public ReportScheduleBase(string connectionString, string serviceName)
            : this(connectionString, serviceName, string.Empty)
        {
        }

        #endregion

        #region Public Virtual Methods

        /// <summary>
        /// Updates all properties on the queue record.
        /// </summary>
        /// <param name="scheduleEntry">The schedule entry.</param>
        /// <param name="success">if set to <c>true</c> [success].</param>
        public virtual void Update(TScheduleEntry scheduleEntry, bool success)
        {
            Factory.Update(ConnectionString, scheduleEntry, success);
        }

        /// <summary>
        /// Processes the queue and returns an entry.
        /// </summary>
        /// <returns>TQueueEntry representing the item to be processed.</returns>
        public virtual TScheduleEntry Process()
        {
            return Factory.Process(ConnectionString, ServiceName);
        }

        /// <summary>
        /// Saves the content and content type fields on the queueEntry with the given context id.
        /// </summary>
        /// <param name="reportScheduleId">The report schedule identifier.</param>
        /// <param name="reportScheduleEntry">The report schedule entry.</param>
        public virtual void SaveResults(int reportScheduleId, IReportScheduleEntry reportScheduleEntry)
        {
            Factory.SaveResults(ConnectionString, reportScheduleId, reportScheduleEntry);
        }

        /// <summary>
        /// Processes the reset.
        /// </summary>
        public virtual void ProcessReset()
        {
            Factory.ProcessReset(ConnectionString, ReportSchedulerId);
        }

        /// <summary>
        /// Resets a specific IQueueEntry that is marked as InProcess.
        /// </summary>
        /// <param name="scheduleEntry">The schedule entry.</param>
        /// <remarks>Due to the asynchronous nature of queue processing it is possible that a cancellation request might result
        /// in multiple calls to this method for the same Entry. It is important that any implementation can handle multiple
        /// calls without negative consequence.. Essentially if the item is already reset, do nothing.</remarks>
        public virtual void ProcessResetEntry(TScheduleEntry scheduleEntry)
        {
            Factory.ProcessResetEntry(this.ConnectionString, scheduleEntry);
        }

        #endregion

        #region IQueue Members

        /// <summary>
        /// Resets a specific IQueueEntry that is marked as InProcess.
        /// </summary>
        /// <param name="reportScheduleEntry">Entry to reset.</param>
        /// <remarks>Due to the asynchronous nature of queue processing it is possible that a cancellation request might result
        /// in multiple calls to this method for the same Entry. It is important that any implementation can handle multiple
        /// calls without negative consequence.. Essentially if the item is already reset, do nothing.</remarks>
        void IReportSchedule.ProcessResetEntry(IReportScheduleEntry reportScheduleEntry)
        {
            this.ProcessResetEntry(reportScheduleEntry as TScheduleEntry);
        }

        /// <summary>
        /// Processes the queue and returns an entry.
        /// </summary>
        /// <returns>IQueueEntry representing the item to be processed.</returns>
        IReportScheduleEntry IReportSchedule.Process()
        {
            return this.Process();
        }

        #endregion
    }
}
