// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-12-2015

using System;
using System.Collections.Generic;
using System.ComponentModel;

/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// Class EditReportScheduleViewModel.
    /// </summary>
    public class EditReportScheduleViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The ReportScheduleId.</value>
        public int ReportScheduleId { get; set; }
        /// <summary>
        /// Gets or sets the selected level identifier.
        /// </summary>
        /// <value>The selected level identifier.</value>
        public int SelectedReportId { get; set; }
        /// <summary>
        /// Gets or sets the ReportName.
        /// </summary>
        /// <value>The ReportName.</value>
        public List<DisplayValuePair> ReportName { get; set; }
        /// <summary>
        /// Gets or sets the selected level identifier.
        /// </summary>
        /// <value>The selected level identifier.</value>
        public int SelectedFrequencyType { get; set; }
        /// <summary>
        /// Gets or sets the FrequencyType.
        /// </summary>
        /// <value>The FrequencyType.</value>
        public List<DisplayValuePair> FrequencyType { get; set; }
        /// <summary>
        /// Gets or sets the FrequencyInterval.
        /// </summary>
        /// <value>The FrequencyInterval.</value>
        public int FrequencyInterval { get; set; }
        /// <summary>
        /// Gets or sets the UTC last modified date.
        /// </summary>
        /// <value>The UTC last modified date.</value>
        [DisplayName("First Scheduled Run Date")]
        public string UtcFirstScheduledRunDate { get; set; }
        /// <summary>
        /// Gets or sets the UTC last modified date.
        /// </summary>
        /// <value>The UTC last modified date.</value>
        [DisplayName("Last Scheduled Run Date")]
        public string UtcLastScheduledRunDate { get; set; }
        /// <summary>
        /// Gets or sets the UTC last modified date.
        /// </summary>
        /// <value>The UTC last modified date.</value>
        [DisplayName("Next Scheduled Run Date")]
        public string UtcNextScheduledRunDate { get; set; }
        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        /// <value>The modified by.</value>
        [DisplayName("Modified By")]
        public int? ModifiedBy { get; set; }
        /// <summary>
        /// Gets or sets the name of the modified by.
        /// </summary>
        /// <value>The name of the modified by.</value>
        [DisplayName("Modified By")]
        public string ModifiedByName { get; set; }
        /// <summary>
        /// Gets or sets the UTC last modified date.
        /// </summary>
        /// <value>The UTC last modified date.</value>
        [DisplayName("Modified Date")]
        public DateTime? UTCLastModifiedDate { get; set; }
        /// <summary>
        /// Gets or sets the FrequencyDescription.
        /// </summary>
        /// <value>The FrequencyDescription</value>
        public string FrequencyDescription { get; set; }
        /// <summary>
        /// IsEnabled
        /// </summary>
        /// <value><c>true</c> if this instance is enabled; otherwise, <c>false</c>.</value>
        public bool IsEnabled { get; set; }
        /// <summary>
        /// Gets or sets the success or failed message.
        /// </summary>
        /// <value>The success or failed message.</value>
        public string SuccessOrFailedMessage { get; set; }
        /// <summary>
        /// Gets or sets the Transfer Type.
        /// </summary>
        /// <value>The Transfer Type.</value>
        [DisplayName("Transfer Type")]
        public  List<DisplayValuePair>  TransferType { get; set; }
        /// <summary>
        /// Gets or sets the Email.
        /// </summary>
        /// <value>The Email.</value>
        [DisplayName("Email")]
        public string  Email { get; set; }

        /// <summary>
        /// Gets or sets the FTPServerIP.
        /// </summary>
        /// <value>The FTPServerIP.</value>
        [DisplayName("SFTPServerIP")]
        public string  FTPServerIP { get; set; }
        /// <summary>
        /// Gets or sets the FTPFilePath.
        /// </summary>
        /// <value>The FTPFilePath.</value>
        [DisplayName("SFTPFilePath")]
        public string  FTPFilePath { get; set; }

        /// <summary>
        /// Gets or sets the FTPUsername.
        /// </summary>
        /// <value>The FTPUsername.</value>
        [DisplayName("SFTPUsername")]
        public string  FTPUsername { get; set; }
        /// <summary>
        /// Gets or sets the FTPPassword.
        /// </summary>
        /// <value>The FTPPassword.</value>
        [DisplayName("SFTPPassword")]
        public string  FTPPassword { get; set; }

        /// <summary>
        /// Gets or sets the type of the selected transfer.
        /// </summary>
        /// <value>The type of the selected transfer.</value>
        public int SelectedTransferType { get; set; }
    }
}