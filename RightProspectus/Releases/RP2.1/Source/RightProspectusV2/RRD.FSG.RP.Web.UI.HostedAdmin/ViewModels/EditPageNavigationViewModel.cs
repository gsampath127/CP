// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-27-2015
//
// Last Modified By : 
// Last Modified On : 10-31-2015
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;

/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{

    /// <summary>
    /// Class EditPageNavigationViewModel.
    /// </summary>
    public class EditPageNavigationViewModel
    {

        /// <summary>
        /// Gets or sets the navigation keys.
        /// </summary>
        /// <value>The navigation keys.</value>
        [DisplayName("Navigation Key")]
        public List<DisplayValuePair> NavigationKeys { get; set; }
        /// <summary>
        /// Gets or sets the selected Navigation key.
        /// </summary>
        /// <value>The selected Navigation key.</value>
        public string SelectedNavigationKey { get; set; }
        /// <summary>
        /// Gets or sets the Navigation descriptions.
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
        public string Text { get; set; }
        /// <summary>
        /// Gets or sets the navigation XML.
        /// </summary>
        /// <value>The navigation XML.</value>
        [DisplayName("Navigation XML")]
        public string NavigationXML { get; set; }
        /// <summary>
        /// Gets or sets the page text identifier.
        /// </summary>
        /// <value>The page text identifier.</value>
        public int PageNavigationID { get; set; }
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
        public bool IsProofingAvailableForPageNavigationId { get; set; }
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
        /// Display userName
        /// </summary>
        /// <value>The username.</value>
        public string ModifiedByName { get; set; }
        /// <summary>
        /// Gets or sets the success or failed message.
        /// </summary>
        /// <value>The success or failed message.</value>
        public string SuccessOrFailedMessage { get; set; }
    }
}