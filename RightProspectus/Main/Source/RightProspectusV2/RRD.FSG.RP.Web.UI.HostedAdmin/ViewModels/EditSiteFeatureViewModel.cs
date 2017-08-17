// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-09-2015

using System;
using System.Collections.Generic;
using System.ComponentModel;

/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// Class EditSiteFeatureViewModel.
    /// </summary>
    public class EditSiteFeatureViewModel
    {
        /// <summary>
        /// Gets or sets the Site Feature keys.
        /// </summary>
        /// <value>The Feature keys.</value>
        [DisplayName("Feature Key")]
        public List<DisplayValuePair> FeatureKeys { get; set; }

        /// <summary>
        /// Gets or sets the selected Feature  key.
        /// </summary>
        /// <value>The selected Feature key.</value>
        public string SelectedFeatureKey { get; set; }

        /// <summary>
        /// Gets or sets the Site Feature keys.
        /// </summary>
        /// <value>The Feature keys.</value>
        [DisplayName("Feature Mode")]
        public string FeatureModes { get; set; }

        /// <summary>
        /// Gets or sets the selected FeatureMode.
        /// </summary>
        /// <value>The selected FeatureMode.</value>
        public string SelectedFeatureMode { get; set; }
        /// <summary>
        /// Gets or Sets the SiteId
        /// </summary>
        /// <value>The SiteId.</value>

        public int SiteId { get; set; }

        /// <summary>
        /// Gets or Sets the FeatureMode
        /// </summary>
        /// <value>The FeatureMode.</value>

        public int? FeatureMode { get; set; }


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
        /// Gets or sets the name of the modified by.
        /// </summary>
        /// <value>The name of the modified by.</value>
        public string ModifiedByName { get; set; }
    }
}