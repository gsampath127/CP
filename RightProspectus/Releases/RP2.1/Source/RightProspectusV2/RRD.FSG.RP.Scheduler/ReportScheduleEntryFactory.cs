// ***********************************************************************
// Assembly         : RRD.FSG.RP.Scheduler
// Author           : 
// Created          : 10-29-2015
//
// Last Modified By : 
// Last Modified On : 11-12-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using System;
using System.Data;

namespace RRD.FSG.RP.Scheduler
{
    /// <summary>
    /// Class ReportScheduleEntryFactory.
    /// </summary>
    public class ReportScheduleEntryFactory
        :ReportScheduleEntryFactoryBase<ReportScheduleEntry>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportScheduleEntryFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public ReportScheduleEntryFactory(IDataAccess dataAccess)
            : base(dataAccess) { }
        
        #endregion

        #region Protected Override Methods


        /// <summary>
        /// Instantiates a ReportScheduleEntry object using the data record.
        /// </summary>
        /// <param name="record">record used to instantiate the object.</param>
        /// <returns>the instantiated ReportScheduleEntry object.</returns>
        protected override ReportScheduleEntry CreateScheduleEntry(IDataRecord record)

        {
            return CreateScheduleEntry((int)record["ReportScheduleId"], record["ReportName"].ToString(), (int)record["ClientId"], record["ClientName"].ToString(), (int)record["Status"], (int)record["FrequencyType"], (int)record["FrequencyInterval"], record.GetDateTime(record.GetOrdinal("UtcNextScheduledRunDate")), record["ServiceName"].ToString(), (int)record["ExecutionCount"], record["Email"].ToString(), record["FTPServerIP"].ToString(), record["FTPFilePath"].ToString(), record["FTPUsername"].ToString(), record["FTPPassword"].ToString(), (bool)record["IsSFTP"]);
        }

        /// <summary>
        /// Instantiates a ReportScheduleEntry object using the data row.
        /// </summary>
        /// <param name="row">row used to instantiate the object.</param>
        /// <returns>the instantiated ReportScheduleEntry object.</returns>
        protected override ReportScheduleEntry CreateScheduleEntry(DataRow row)
        {
            return CreateScheduleEntry((int)row["ReportScheduleId"], row["ReportName"].ToString(), (int)row["ClientId"], row["ClientName"].ToString(), (int)row["Status"], (int)row["FrequencyType"], (int)row["FrequencyInterval"], (DateTime)row["UtcNextScheduledRunDate"], row["ServiceName"].ToString(), (int)row["ExecutionCount"], row["Email"].ToString(), row["FTPServerIP"].ToString(), row["FTPFilePath"].ToString(), row["FTPUsername"].ToString(), row["FTPPassword"].ToString(),(bool)row["IsSFTP"]);
        }

        #endregion

        #region Protected Virtual Methods

        /// <summary>
        /// Instantiates a ReportScheduleEntry object using the passed in data.
        /// </summary>
        /// <param name="reportScheduleId">The report schedule identifier.</param>
        /// <param name="reportName">Name of the report.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="status">The status.</param>
        /// <param name="frequencyType">Type of the frequency.</param>
        /// <param name="frequencyInterval">The frequency interval.</param>
        /// <param name="utcNextScheduledRunDate">The UTC next scheduled run date.</param>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="executionCount">The execution count.</param>
        /// <param name="email">The email.</param>
        /// <param name="ftpServerIP">The FTP server ip.</param>
        /// <param name="ftpFilePath">The FTP file path.</param>
        /// <param name="ftpUserName">Name of the FTP user.</param>
        /// <param name="ftpPassword">The FTP password.</param>
        /// <returns>returns an entry of report schedule</returns>
        protected virtual ReportScheduleEntry CreateScheduleEntry(int reportScheduleId, string reportName, int clientId, string clientName, int status, int frequencyType, int frequencyInterval, DateTime utcNextScheduledRunDate, string serviceName, int? executionCount, string email, string ftpServerIP, string ftpFilePath, string ftpUserName, string ftpPassword, bool IsSFTP)
        {
            return new ReportScheduleEntry(reportScheduleId, reportName, clientId, clientName, status, frequencyType, frequencyInterval, utcNextScheduledRunDate, serviceName, executionCount, email, ftpServerIP, ftpFilePath, ftpUserName, ftpPassword, IsSFTP);
        }

        #endregion
    }
}
