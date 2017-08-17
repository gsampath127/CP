// ***********************************************************************
// Assembly         : RRD.FSG.RP.Scheduler
// Author           : 
// Created          : 10-29-2015
//
// Last Modified By : 
// Last Modified On : 11-12-2015
// ***********************************************************************
using RRD.FSG.RP.Scheduler.Interfaces;
using System;

namespace RRD.FSG.RP.Scheduler
{
    /// <summary>
    /// Class ReportScheduleEntry.
    /// </summary>
    public class ReportScheduleEntry : IReportScheduleEntry
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportScheduleEntry" /> class.
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
        public ReportScheduleEntry(int reportScheduleId, string reportName, int clientId, string clientName, int status, int frequencyType, int frequencyInterval, DateTime utcNextScheduledRunDate, string serviceName, int? executionCount, string email,
                                     string ftpServerIP, string ftpFilePath, string ftpUserName, string ftpPassword, bool isSFTP, DateTime utcDataStartDate, DateTime utcNextDataEndDate, string errorEmailSub, string errorEmailTemplate, string errorEmail,
                                    DateTime? utcLastDataEndDate)
        {
            ReportScheduleId = reportScheduleId;
            ReportName = reportName;
            ClientId = clientId;
            ClientName = clientName;
            Status = status;
            FrequencyType = frequencyType;
            FrequencyInterval = frequencyInterval;
            UtcNextScheduledRunDate = utcNextScheduledRunDate;
            serviceName = ServiceName;
            ExecutionCount = executionCount;
            Email = email;
            FTPServerIP = ftpServerIP;
            FTPFilePath = ftpFilePath;
            FTPUsername = ftpUserName;
            FTPPassword = ftpPassword;
            IsSFTP = isSFTP;
            UtcDataStartDate = utcDataStartDate;
            UtcNextDataEndDate = utcNextDataEndDate;
            ErrorEmailSub = errorEmailSub;
            ErrorEmailTemplate = errorEmailTemplate;
            ErrorEmail = errorEmail;
            UtcLastDataEndDate = utcLastDataEndDate;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the report schedule identifier.
        /// </summary>
        /// <value>The report schedule identifier.</value>
        public int ReportScheduleId { get; set; }

        /// <summary>
        /// Gets or sets the report name
        /// </summary>
        /// <value>The name of the report.</value>
        public string ReportName { get; set; }

        /// <summary>
        /// Gets or sets the Id of the client.
        /// Optional
        /// </summary>
        /// <value>The client identifier.</value>
        public int ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client name
        /// </summary>
        /// <value>The name of the client.</value>
        public string ClientName { get; set; }

        /// <summary>
        /// Status of report schedule entries
        /// </summary>
        /// <value>The status.</value>
        public int Status { get; set; }

        /// <summary>
        /// Frequency Type
        /// </summary>
        /// <value>The type of the frequency.</value>
        public int FrequencyType { get; set; }
        /// <summary>
        /// Gets or sets the frequency interval.
        /// </summary>
        /// <value>The frequency interval.</value>
        public int FrequencyInterval { get; set; }

        /// <summary>
        /// Gets or sets the next scheduled run date
        /// </summary>
        /// <value>The UTC next scheduled run date.</value>
        public DateTime UtcNextScheduledRunDate { get; set; }

        /// <summary>
        /// This will be a combination of the name of service + machine name to provide a unique fingerprint of which service is processing a job.
        /// </summary>
        /// <value>The name of the service.</value>
        public string ServiceName { get; set; }

        /// <summary>
        /// Total execution count for the current report run attempt.
        /// </summary>
        /// <value>The execution count.</value>
        public int? ExecutionCount { get; set; }

        /// <summary>
        /// Gets or sets the email address
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the error email address
        /// </summary>
        /// <value>The  error email.</value>
        public string ErrorEmail { get; set; }

        /// <summary>
        /// Gets or sets the FTPServerIP address
        /// </summary>
        /// <value>The FTP server ip.</value>
        public string FTPServerIP { get; set; }

        /// <summary>
        /// Gets or sets the FTPFilePath address
        /// </summary>
        /// <value>The FTP file path.</value>
        public string FTPFilePath { get; set; }

        /// <summary>
        /// Gets or sets the FTPUsername address
        /// </summary>
        /// <value>The FTP username.</value>
        public string FTPUsername { get; set; }

        /// <summary>
        /// Gets or sets the FTPPassword address
        /// </summary>
        /// <value>The FTP password.</value>
        public string FTPPassword { get; set; }

        /// <summary>
        /// ISSFTP
        /// </summary>
        public bool IsSFTP { get; set; }

        /// <summary>
        /// Gets or sets the data start date
        /// </summary>
        /// <value>The UTC data start date.</value>
        public DateTime UtcDataStartDate { get; set; }

        /// <summary>
        /// Gets or sets the next data end date
        /// </summary>
        /// <value>The UTC next data end date.</value>
        public DateTime UtcNextDataEndDate { get; set; }

        /// <summary>
        /// Gets or sets the ErrorEmailSub
        /// </summary>
        /// <value>The ErrorEmailSub.</value>
        public string ErrorEmailSub { get; set; }
        /// <summary>
        /// Gets or sets the ErrorEmailTemplate
        /// </summary>
        /// <value>The ErrorEmailTemplate.</value>
        public string ErrorEmailTemplate { get; set; }

        /// <summary>
        /// Gets or sets the UtcLastDataEndDate
        /// </summary>
        /// <value>The UtcLastDataEndDate</value>
        public DateTime? UtcLastDataEndDate { get; set; }

        #endregion
    }
}
