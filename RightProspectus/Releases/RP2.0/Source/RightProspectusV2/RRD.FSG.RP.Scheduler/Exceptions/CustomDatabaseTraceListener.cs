using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.Instrumentation;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using System.Diagnostics;
using System.Data;
using System.Data.Common;

namespace RRD.FSG.RP.Scheduler
{
    class CustomDatabaseTraceListener : CustomTraceListener
    {
        #region Variables

        Microsoft.Practices.EnterpriseLibrary.Data.Database database;

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public override void Write(string message)
        {
            // Do nothing
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public override void WriteLine(string message)
        {
            Write(message);
        }

        /// <summary>
        /// Executes the WriteLog stored procedure
        /// </summary>
        /// <param name="logEntry">The LogEntry to store in the database.</param>
        /// <param name="db">An instance of the database class to use for storing the LogEntry</param>
        /// <param name="transaction">The transaction that wraps around the execution calls for storing the LogEntry</param>
        /// <returns>An integer for the LogEntry Id</returns>
        private void ExecuteWriteLogStoredProcedure(LogEntry logEntry, Microsoft.Practices.EnterpriseLibrary.Data.Database db)
        {
            string storedProcedure = this.Attributes["writeLogStoredProcName"].ToString(); 

            using (DbCommand cmd = db.GetStoredProcCommand(storedProcedure))
            {
                db.AddInParameter(cmd, "@ErrorCode", DbType.Int32, 1000);
                db.AddInParameter(cmd, "@Priority", DbType.Int32, logEntry.Priority);
                db.AddInParameter(cmd, "@Severity", DbType.String, logEntry.LoggedSeverity);
                db.AddInParameter(cmd, "@Title", DbType.String, logEntry.Title);
                db.AddInParameter(cmd, "@MachineName", DbType.String, logEntry.MachineName);
                db.AddInParameter(cmd, "@AppDomainName", DbType.String, logEntry.AppDomainName);
                db.AddInParameter(cmd, "@ProcessID", DbType.String, logEntry.ProcessId);
                db.AddInParameter(cmd, "@ProcessName", DbType.String, logEntry.ProcessName);
                db.AddInParameter(cmd, "@ThreadName", DbType.String, logEntry.ManagedThreadName);
                db.AddInParameter(cmd, "@Win32ThreadId", DbType.String, logEntry.Win32ThreadId);
                db.AddInParameter(cmd, "@EventId", DbType.Int32, logEntry.EventId);
                db.AddInParameter(cmd, "@SiteActivityId", DbType.Int32, DBNull.Value);
                db.AddInParameter(cmd, "@Message", DbType.String, logEntry.Message);
                db.AddInParameter(cmd, "@FormattedMessage", DbType.String, logEntry.ErrorMessages);
                db.AddInParameter(cmd, "@URL", DbType.String, DBNull.Value);
                db.AddInParameter(cmd, "@AbsoluteURL", DbType.String, DBNull.Value);
                

                db.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// Delivers the trace data to the underlying database.
        /// </summary>
        /// <param name="eventCache">The context information provided by <see cref="System.Diagnostics"/>.</param>
        /// <param name="source">The name of the trace source that delivered the trace data.</param>
        /// <param name="eventType">The type of event.</param>
        /// <param name="id">The id of the event.</param>
        /// <param name="data">The data to trace.</param>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"E:\TEST\Log.txt", true))
            {
                file.WriteLine("CustomDatabaseTraceListner : ");
            }

            LogEntry logEntry = data as LogEntry;
            string message = data as string;
            if (logEntry != null)
            {
                ExecuteStoredProcedure(logEntry);
            }
            else if (message != null)
            {
                Write(message);
            }
            else
            {
                base.TraceData(eventCache, source, eventType, id, data);
            }
        }

        /// <summary>
        /// Executes the stored procedures
        /// </summary>
        /// <param name="logEntry">The LogEntry to store in the database</param>
        private void ExecuteStoredProcedure(LogEntry logEntry)
        {
            if (database == null)
            {
                database = DatabaseFactory.CreateDatabase(this.Attributes["databaseInstanceName"].ToString());
            }

            ExecuteWriteLogStoredProcedure(logEntry, database);
        }

        #endregion
    }
}
