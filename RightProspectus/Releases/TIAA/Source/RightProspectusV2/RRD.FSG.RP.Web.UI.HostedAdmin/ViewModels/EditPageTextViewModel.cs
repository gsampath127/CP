// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-16-2015

using System;
using System.Collections.Generic;
using System.ComponentModel;

/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// Class EditPageTextViewModel.
    /// </summary>
    public class EditPageTextViewModel
    {
        /// <summary>
        /// Gets or sets the resource keys.
        /// </summary>
        /// <value>The resource keys.</value>
        [DisplayName("Resource Key")]
        public List<DisplayValuePair> ResourceKeys { get; set; }
        /// <summary>
        /// Gets or sets the selected resource key.
        /// </summary>
        /// <value>The selected resource key.</value>
        public string SelectedResourceKey { get; set; }
        /// <summary>
        /// Gets or sets the page descriptions.
        /// </summary>
        /// <value>The page descriptions.</value>
        [DisplayName("Page")]
        public List<DisplayValuePair> PageDescriptions { get; set; }
        /// <summary>
        /// Gets or sets the selected page identifier.
        /// </summary>
        /// <value>The selected page identifier.</value>
        public int SelectedPageID { get; set; }
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        [DisplayName("Text")]
        public string HtmlText { get; set; }
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        [DisplayName("Text")]
        public string PlainText { get; set; }
        /// <summary>
        /// Gets or sets the page text identifier.
        /// </summary>
        /// <value>The page text identifier.</value>
        public int PageTextID { get; set; }
        /// <summary>
        /// Gets or sets the version identifier.
        /// </summary>
        /// <value>The version identifier.</value>
        public int VersionID { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is proofing.
        /// </summary>
        /// <value><c>true</c> if this instance is proofing; otherwise, <c>false</c>.</value>
        public bool IsProofing { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is proofing available for page text identifier.
        /// </summary>
        /// <value><c>true</c> if this instance is proofing available for page text identifier; otherwise, <c>false</c>.</value>
        public bool IsProofingAvailableForPageTextId { get; set; }
        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        /// <value>The modified by.</value>
        [DisplayName("Modified By")]
        public int? ModifiedBy { get; set; }
        /// <summary>
        /// Gets or sets the UTC last modified date.
        /// </summary>
        /// <value>The UTC last modified date.</value>
        [DisplayName("Modified Date")]
        public DateTime? UTCLastModifiedDate { get; set; }
        /// <summary>
        /// Gets or sets the success or failed message.
        /// </summary>
        /// <value>The success or failed message.</value>
        public string SuccessOrFailedMessage { get; set; }

        /// <summary>
        /// Gets or sets the name of the modified by.
        /// </summary>
        /// <value>The name of the modified by.</value>
        public string ModifiedByName { get; set; }
    }
}
