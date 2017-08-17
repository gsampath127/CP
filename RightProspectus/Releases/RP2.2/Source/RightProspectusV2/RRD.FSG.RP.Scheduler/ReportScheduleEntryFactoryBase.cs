// ***********************************************************************
// Assembly         : RRD.FSG.RP.Scheduler
// Author           : 
// Created          : 10-29-2015
//
// Last Modified By : 
// Last Modified On : 11-13-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Scheduler.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace RRD.FSG.RP.Scheduler
{
    /// <summary>
    /// Base class for IReportScheduleEntry factories.
    /// </summary>
    /// <typeparam name="TScheduleEntry">The type of the t schedule entry.</typeparam>
    public abstract class ReportScheduleEntryFactoryBase<TScheduleEntry>
        :IReportScheduleEntryFactory<TScheduleEntry>
        where TScheduleEntry : class, IReportScheduleEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public ReportScheduleEntryFactoryBase(IDataAccess dataAccess)
        {
            DataAccess = dataAccess;  
        }

        #region Protected Virtual Properties

        /// <summary>
        /// Gets the stored procedure for saving results.
        /// </summary>
        /// <value>The save results procedure.</value>
        protected virtual string SaveResultsProcedure
        {
            get { return "RPV2HostedAdmin_SaveReportScheduleEntry"; }
        }

        /// <summary>
        /// Gets the stored procedure for en queuing items.
        /// </summary>
        /// <value>The en queue procedure.</value>
        protected virtual string EnQueueProcedure
        {
            get { return "Queue_Enqueue"; }
        }

        /// <summary>
        /// Gets the stored procedure for getting a count of items in queue.
        /// </summary>
        /// <value>The get count procedure.</value>
        protected virtual string GetCountProcedure
        {
            get { return "Queue_GetCount"; }
        }

        /// <summary>
        /// Gets the stored procedure for dequeuing an item.
        /// </summary>
        /// <value>The process procedure.</value>
        protected virtual string ProcessProcedure
        {
            get { return "RPV2HostedAdmin_ProcessReportSchedule"; }
        }

        /// <summary>
        /// Gets the stored procedure for reseting queue items owned by a specific process.
        /// </summary>
        /// <value>The process reset procedure.</value>
        protected virtual string ProcessResetProcedure
        {
            get { return "RPV2HostedAdmin_ProcessResetReportSchedule"; }
        }

        /// <summary>
        /// Gets the stored procedure for resetting queue items owned by a specific process.
        /// </summary>
        /// <value>The process reset entry procedure.</value>
        protected virtual string ProcessResetEntryProcedure
        {
            get { return "Queue_ProcessResetEntry"; }
        }

        /// <summary>
        /// Gets the stored procedure for updating the queue.
        /// </summary>
        /// <value>The update procedure.</value>
        protected virtual string UpdateProcedure
        {
            get { return "Queue_Update"; }
        }

        /// <summary>
        /// Gets the initialize cancel procedure.
        /// </summary>
        /// <value>The initialize cancel procedure.</value>
        protected virtual string InitCancelProcedure
        {
            get { return "Queue_InitiateCancel"; }
        }

        /// <summary>
        /// Gets the initialize batch cancel procedure.
        /// </summary>
        /// <value>The initialize batch cancel procedure.</value>
        protected virtual string InitBatchCancelProcedure
        {
            get { return "Queue_InitiateBatchCancel"; }
        }

        /// <summary>
        /// Gets the complete cancel procedure.
        /// </summary>
        /// <value>The complete cancel procedure.</value>
        protected virtual string CompleteCancelProcedure
        {
            get { return "Queue_CompleteCancel"; }
        }

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the data access instance used to interface between factory and persisted storage of entity data.
        /// </summary>
        /// <value>The data access.</value>
        public IDataAccess DataAccess { get; internal set; }
        #endregion

        #region Public Virtual Methods

        /// <summary>
        /// Saves the content and content type in the queue record.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="reportScheduleId">The report schedule identifier.</param>
        /// <param name="reportScheduleEntry">The report schedule entry.</param>
        public virtual void SaveResults(string connectionString, int reportScheduleId, IReportScheduleEntry reportScheduleEntry)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter("ReportScheduleId", DbType.Int32, reportScheduleId));
                parameters.Add(DataAccess.CreateParameter("Status", DbType.Int32, reportScheduleEntry.Status));
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["maxProcessAttempts"]))
                {
                    parameters.Add(DataAccess.CreateParameter("MaxProcessAttempts", DbType.Int32, int.Parse(ConfigurationManager.AppSettings["maxProcessAttempts"])));
                }
                parameters.Add(DataAccess.CreateParameter("UtcLastScheduledRunDate", DbType.DateTime, reportScheduleEntry.UtcNextScheduledRunDate));
                
            }
            this.DataAccess.ExecuteNonQuery(connectionString, SaveResultsProcedure, parameters);
        }

        /// <summary>
        /// Updates all the properties on the queue record.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="scheduleEntry">The queue entry.</param>
        /// <param name="success">if set to <c>true</c> [success].</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual void Update(string connectionString, TScheduleEntry scheduleEntry, bool success)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Processes the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="serviceName">Name of the service.</param>
        /// <returns>The next item from the queue.</returns>
        /// <exception cref="System.ApplicationException">Unable to Process the ReportScheduleEntry</exception>
        /// <exception cref="ApplicationException">Thrown if processing the entry resulted in an unknown error.</exception>
        public virtual TScheduleEntry Process(string connectionString, string serviceName)
        {
            try
            {
                return GetScheduleEntry(connectionString, ProcessProcedure, serviceName);
            }
            catch (Exception ex)
            {
                if (ex is ApplicationException)
                {
                    throw;
                }

                throw new ApplicationException("Unable to Process the ReportScheduleEntry", ex);
            }
        }

        /// <summary>
        /// Resets every QueueEntry that is marked as InProcess
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="reportSchedulerId">Report scheduler id.</param>
        public virtual void ProcessReset(string connectionString, int reportSchedulerId)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter("ReportScheduleId", DbType.Int32, reportSchedulerId));
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["maxProcessAttempts"]))
                {
                    parameters.Add(DataAccess.CreateParameter("MaxProcessAttempts", DbType.Int32, int.Parse(ConfigurationManager.AppSettings["maxProcessAttempts"])));
                }
            }
            this.DataAccess.ExecuteNonQuery(connectionString, ProcessResetProcedure, parameters);
        }

        /// <summary>
        /// Reset a specific TQueueEntry that is marked as InProcess. Usually called for a graceful exit.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="scheduleEntry">The schedule entry.</param>
        /// <param name="reduceAttempts">True if we should reduce total attempts (such as for a graceful exit), false otherwise.</param>
        public virtual void ProcessResetEntry(string connectionString, TScheduleEntry scheduleEntry, bool reduceAttempts = true)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter("ReportScheduleId", DbType.Int32, scheduleEntry.ReportScheduleId));
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["maxProcessAttempts"]))
                {
                    parameters.Add(DataAccess.CreateParameter("MaxProcessAttempts", DbType.Int32, int.Parse(ConfigurationManager.AppSettings["maxProcessAttempts"])));
                }
            }
            this.DataAccess.ExecuteNonQuery(connectionString, ProcessResetProcedure, parameters);
        }

        #endregion

        #region Protected Abstract Methods

        /// <summary>
        /// Instantiates a TQueueEntry object using the data record.
        /// </summary>
        /// <param name="record">record used to instantiate the object.</param>
        /// <returns>the instantiated TQueueEntry object.</returns>
        protected abstract TScheduleEntry CreateScheduleEntry(IDataRecord record);

        /// <summary>
        /// Instantiates a TQueueEntry object using the data row.
        /// </summary>
        /// <param name="row">row used to instantiate the object.</param>
        /// <returns>the instantiated TQueueEntry object.</returns>
        protected abstract TScheduleEntry CreateScheduleEntry(DataRow row);

        #endregion

        #region Private Methods

        /// <summary>
        /// Used by both Peek and Process operations, they both do the exact same functionality in code the differences lie in the stored procedures
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="spName">The name of the stored procedure to run</param>
        /// <param name="serviceName">Name of the service.</param>
        /// <returns>TScheduleEntry.</returns>
        private TScheduleEntry GetScheduleEntry(string connectionString, string spName, string serviceName)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter("ServiceName", DbType.String, serviceName));
            }

            TScheduleEntry scheduleEntry = null;

            using (IDataReader dr = this.DataAccess.ExecuteReader(connectionString, spName, parameters.ToArray()))
            {
                if (dr.Read())
                {
                    scheduleEntry = CreateScheduleEntry(dr);
                }
            }

            return scheduleEntry;
        }

        #endregion

        #region IQueueEntryFactory Members

        /// <summary>
        /// Updates all the properties on the queue record.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="reportScheduleEntry">The report schedule entry.</param>
        /// <param name="success">if set to <c>true</c> [success].</param>
        void IReportScheduleEntryFactory.Update(string connectionString, IReportScheduleEntry reportScheduleEntry, bool success)
        {
            this.Update(connectionString, reportScheduleEntry as TScheduleEntry, success);
        }

        /// <summary>
        /// Processes the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="serviceName">Name of the service.</param>
        /// <returns>IReportScheduleEntry.</returns>
        IReportScheduleEntry IReportScheduleEntryFactory.Process(string connectionString, string serviceName)
        {
            return this.Process(connectionString, serviceName);
        }

        /// <summary>
        /// Reseta a specific TQueueEntry that is marked as InProcess. Usually called for a graceful exit.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="reportScheduleEntry">The report schedule entry.</param>
        /// <param name="reduceAttempts">True if we should reduce total attempts (such as for a graceful exit), false otherwise.</param>
        void IReportScheduleEntryFactory.ProcessResetEntry(string connectionString, IReportScheduleEntry reportScheduleEntry, bool reduceAttempts = true)
        {
            this.ProcessResetEntry(connectionString, reportScheduleEntry as TScheduleEntry, reduceAttempts);
        }

        #endregion
    }
}
