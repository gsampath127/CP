
/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// Class ReportScheduleViewModel.
    /// </summary>
    public class ReportScheduleViewModel
    {
        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>The client identifier.</value>
        public int ClientId { get; set; }
        /// <summary>
        /// Gets or sets the frequency description.
        /// </summary>
        /// <value>The frequency description.</value>
        public string FrequencyDescription { get; set; }
        /// <summary>
        /// Gets or sets the frequency interval.
        /// </summary>
        /// <value>The frequency interval.</value>
        public int FrequencyInterval { get; set; }
        /// <summary>
        /// Gets or sets the type of the frequency.
        /// </summary>
        /// <value>The type of the frequency.</value>
        public string FrequencyType { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is enabled; otherwise, <c>false</c>.</value>
        public string IsEnabled { get; set; }
        /// <summary>
        /// Gets or sets the report identifier.
        /// </summary>
        /// <value>The report identifier.</value>
        public int ReportId { get; set; }
        /// <summary>
        /// Gets or sets the name of the report.
        /// </summary>
        /// <value>The name of the report.</value>
        public string ReportName { get; set; }
        /// <summary>
        /// Gets or sets the report schedule identifier.
        /// </summary>
        /// <value>The report schedule identifier.</value>
        public int ReportScheduleId { get; set; }
        /// <summary>
        /// Gets or sets the UTC first scheduled run date.
        /// </summary>
        /// <value>The UTC first scheduled run date.</value>
        public string UtcFirstScheduledRunDate { get; set; }
        /// <summary>
        /// Gets or sets the UTC last actual run date.
        /// </summary>
        /// <value>The UTC last actual run date.</value>
        public string UtcLastActualRunDate { get; set; }
        /// <summary>
        /// Gets or sets the UTC last scheduled run date.
        /// </summary>
        /// <value>The UTC last scheduled run date.</value>
        public string UtcLastScheduledRunDate { get; set; }
        /// <summary>
        /// Gets or sets the UTC next scheduled run date.
        /// </summary>
        /// <value>The UTC next scheduled run date.</value>
        public string UtcNextScheduledRunDate { get; set; }
        /// <summary>
        /// Gets or sets the success or failed message.
        /// </summary>
        /// <value>The success or failed message.</value>
        public string SuccessOrFailedMessage { get; set; }

    }
}