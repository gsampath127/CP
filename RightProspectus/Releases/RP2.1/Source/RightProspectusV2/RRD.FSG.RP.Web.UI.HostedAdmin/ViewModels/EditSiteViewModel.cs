// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-31-2015

using System;
using System.Collections.Generic;
using System.ComponentModel;

/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// Class EditSiteViewModel.
    /// </summary>
    public class EditSiteViewModel
    {
        /// <summary>
        /// Gets or sets the site identifier.
        /// </summary>
        /// <value>The site identifier.</value>
        public int SiteID { get; set; }
        /// <summary>
        /// Gets or sets the name of the site.
        /// </summary>
        /// <value>The name of the site.</value>
        [DisplayName("Site Name")]
        public string SiteName { get; set; }
        /// <summary>
        /// Gets or sets the template names.
        /// </summary>
        /// <value>The template names.</value>
        [DisplayName("Template Name")]
        public List<DisplayValuePair> TemplateNames { get; set; }
        /// <summary>
        /// Gets or sets the selected template identifier.
        /// </summary>
        /// <value>The selected template identifier.</value>
        public int SelectedTemplateID { get; set; }
        /// <summary>
        /// Gets or sets the page descriptions.
        /// </summary>
        /// <value>The page descriptions.</value>
        [DisplayName("Default Page")]
        public List<DisplayValuePair> PageDescriptions { get; set; }
        /// <summary>
        /// Gets or sets the selected default page name identifier.
        /// </summary>
        /// <value>The selected default page name identifier.</value>
        public int SelectedDefaultPageNameID { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [DisplayName("Description")]
        public string Description { get; set; }
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
        [DisplayName("UTC Last Modified Date")]
        public DateTime? UTCLastModifiedDate { get; set; }
        /// <summary>
        /// Gets or sets the success or failed message.
        /// </summary>
        /// <value>The success or failed message.</value>
        public string SuccessOrFailedMessage { get; set; }
        /// <summary>
        /// Gets or sets the base URL.
        /// </summary>
        /// <value>The base URL.</value>
        public string BaseURL { get; set; }
        /// <summary>
        /// Gets or sets the IsDefaultSite.
        /// </summary>
        /// <value>IsDefaultSite.</value>
        [DisplayName("Default Site?")]
        public bool IsDefaultSite { get; set; }

        /// <summary>
        /// Gets or sets the IsDefaultSite.
        /// </summary>
        /// <value>IsDefaultSite.</value>
        public bool DisableDefaultSiteCheckbox { get; set; }
    }
}