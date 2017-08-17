// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-17-2015
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
    /// Class EditPageFeatureViewModel.
    /// </summary>
    public class EditPageFeatureViewModel
    {
        /// <summary>
        /// Gets or sets the page identifier.
        /// </summary>
        /// <value>The page identifier.</value>
        public int PageId { get; set; }
        /// <summary>
        /// Gets or sets the page keys.
        /// </summary>
        /// <value>The page keys.</value>
         [DisplayName("Feature Key")]
        public List<DisplayValuePair> PageKeys { get; set; }
         /// <summary>
         /// Gets or sets the selected page key.
         /// </summary>
         /// <value>The selected page key.</value>
        public string SelectedPageKey { get; set; }
        /// <summary>
        /// Gets or sets the page names.
        /// </summary>
        /// <value>The page names.</value>
        [DisplayName("Page")]
        public List<DisplayValuePair> PageNames { get; set; }
        /// <summary>
        /// Gets or sets the selected page identifier.
        /// </summary>
        /// <value>The selected page identifier.</value>
        public int SelectedPageId { get; set; }
        /// <summary>
        /// Gets or sets the page feature.
        /// </summary>
        /// <value>The page feature.</value>
         [DisplayName("Page Feature")]
        public List<DisplayValuePair> PageFeature { get; set; }
         /// <summary>
         /// Gets or sets the selected page feature.
         /// </summary>
         /// <value>The selected page feature.</value>
        public int SelectedPageFeature { get; set; }

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
        public string ModifiedByName { set; get; }
    }
}