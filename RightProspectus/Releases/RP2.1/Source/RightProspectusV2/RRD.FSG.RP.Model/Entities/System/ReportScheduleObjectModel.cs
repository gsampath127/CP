// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-12-2015
// ***********************************************************************

using System;

/// <summary>
/// The System namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.System
{
    /// <summary>
    /// Class ReportScheduleObjectModel.
    /// </summary>
    public class ReportScheduleObjectModel : AuditedBaseModel<int>, IComparable<ReportScheduleObjectModel>
    {
        /// <summary>
        /// Primary key identifier of the scheduled report.
        /// </summary>
        /// <value>The report schedule identifier.</value>
        public int ReportScheduleId { get; set; }

        /// <summary>
        /// Identifies the Report this schedule is for.
        /// </summary>
        /// <value>The report identifier.</value>
        public int ReportId { get; set; }

        /// <summary>
        /// Identifies the Client this schedule is for.
        /// </summary>
        /// <value>The client identifier.</value>
        public int ClientId { get; set; }

        /// <summary>
        /// Boolean flag that determines if the report is enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is enabled; otherwise, <c>false</c>.</value>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Defines the type of Name for this report.
        /// </summary>
        /// <value>The name of the report.</value>
        public string ReportName { get; set; }
        /// <summary>
        /// Defines the type of frequency for this report.
        /// </summary>
        /// <value>The type of the frequency.</value>
        public int FrequencyType { get; set; }

        /// <summary>
        /// Defines the interval to be used for determining next run date.
        /// </summary>
        /// <value>The frequency interval.</value>
        public int FrequencyInterval { get; set; }

        /// <summary>
        /// First scheduled date for the report to run.
        /// </summary>
        /// <value>The UTC first scheduled run date.</value>
        public DateTime? UtcFirstScheduledRunDate { get; set; }

        /// <summary>
        /// The date the report was scheduled to run when it was run last. Used to determine next date.
        /// </summary>
        /// <value>The UTC last scheduled run date.</value>
        public DateTime? UtcLastScheduledRunDate { get; set; }

        /// <summary>
        /// The actual date and time the report ran last. May not be the same as the last scheduled report, in the case of a report being missed or skipped due to an error.
        /// </summary>
        /// <value>The UTC last actual run date.</value>
        public DateTime? UtcLastActualRunDate { get; set; }

        /// <summary>
        /// The next run date
        /// </summary>
        /// <value>The UTC next scheduled run date.</value>
        public DateTime? UtcNextScheduledRunDate { get; set; }

        /// <summary>
        /// The schedule pattern in easy to understand terms.
        /// </summary>
        /// <value>The frequency description.</value>
        public string FrequencyDescription { get; set; }
        /// <summary>
        /// Gets or sets the email address
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }
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
        /// Gets or sets whether its FTP or SFTP
        /// </summary>
        public bool IsSFTP { get; set; }

        /// <summary>
        /// Compares the two ReportSchedule entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(ReportScheduleObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }
    }
}
