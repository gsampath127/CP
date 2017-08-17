// ***********************************************************************
// Assembly         : RRD.FSG.RP.Scheduler
// Author           : 
// Created          : 10-29-2015
//
// Last Modified By : 
// Last Modified On : 11-12-2015
// ***********************************************************************
using System;

namespace RRD.FSG.RP.Scheduler.Interfaces
{
    /// <summary>
    /// Interface defining report schedule entries to be processed.
    /// </summary>
    public interface IReportScheduleEntry
    {
        /// <summary>
        /// Gets or sets the report schedule identifier.
        /// </summary>
        /// <value>The report schedule identifier.</value>
        int ReportScheduleId { get; set; }

        /// <summary>
        /// Gets or sets the name of the report.
        /// </summary>
        /// <value>The name of the report.</value>
        string ReportName { get; set; }

        /// <summary>
        /// Gets or sets the Id of the client.
        /// Optional
        /// </summary>
        /// <value>The client identifier.</value>
        int ClientId { get; set; }

        /// <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        /// <value>The name of the client.</value>
        string ClientName { get; set; }

        /// <summary>
        /// Status of report schedule entries
        /// </summary>
        /// <value>The status.</value>
        int Status { get; set; }

        /// <summary>
        /// Frequency Type
        /// </summary>
        /// <value>The type of the frequency.</value>
        int FrequencyType { get; set; }
        // <summary>
        /// <summary>
        /// Gets or sets the frequency interval.
        /// </summary>
        /// <value>The frequency interval.</value>
        int FrequencyInterval { get; set; }

        /// <summary>
        /// Gets or sets the next scheduled run date
        /// </summary>
        /// <value>The UTC next scheduled run date.</value>
        DateTime UtcNextScheduledRunDate { get; set; }

        /// <summary>
        /// This will be a combination of the name of service + machine name to provide a unique fingerprint of which service is processing a job.
        /// </summary>
        /// <value>The name of the service.</value>
        string ServiceName { get; set; }

        /// <summary>
        /// Total execution count for the current report run attempt.
        /// </summary>
        /// <value>The execution count.</value>
        int? ExecutionCount { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        string Email { get; set; }

        /// <summary>
        /// Gets or sets the FTPServerIP address
        /// </summary>
        /// <value>The FTP server ip.</value>
        string FTPServerIP { get; set; }

        /// <summary>
        /// Gets or sets the FTPFilePath address
        /// </summary>
        /// <value>The FTP file path.</value>
        string FTPFilePath { get; set; }

        /// <summary>
        /// Gets or sets the FTPUsername address
        /// </summary>
        /// <value>The FTP username.</value>
        string FTPUsername { get; set; }

        /// <summary>
        /// Gets or sets the FTPPassword address
        /// </summary>
        /// <value>The FTP password.</value>
        string FTPPassword { get; set; }

        /// <summary>
        /// Gets or sets whether it is SFTP or not
        /// </summary>
        bool IsSFTP { get; set; }

        /// <summary>
        /// Gets or sets the data start date
        /// </summary>
        /// <value>The UTC data start date.</value>
        DateTime UtcDataStartDate { get; set; }

        /// <summary>
        /// Gets or sets the next data end date
        /// </summary>
        /// <value>The UTC next data end date.</value>
        DateTime UtcNextDataEndDate { get; set; }

        /// <summary>
        /// Gets or sets the ErrorEmailSub
        /// </summary>
        /// <value>The ErrorEmailSub.</value>
        string ErrorEmailSub { get; set; }
        /// <summary>
        /// Gets or sets the ErrorEmailTemplate
        /// </summary>
        /// <value>The ErrorEmailTemplate.</value>
        string ErrorEmailTemplate { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        string ErrorEmail { get; set; }

        /// <summary>
        /// Gets or sets the UtcLastDataEndDate
        /// </summary>
        /// <value>The UTC next data end date.</value>
        DateTime? UtcLastDataEndDate { get; set; }
    }
}
