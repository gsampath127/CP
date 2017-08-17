// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-05-2015
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
    /// Class EditTaxonomyLevelExternalIdViewModel.
    /// </summary>
    public class EditTaxonomyLevelExternalIdViewModel
    {
        /// <summary>
        /// Gets or sets the selected level identifier.
        /// </summary>
        /// <value>
        /// The selected level identifier.
        /// </value>
        public int SelectedLevelId { get; set; }
        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public List<DisplayValuePair> Level { get; set; }
        /// <summary>
        /// Gets or sets the selected taxonomy identifier.
        /// </summary>
        /// <value>
        /// The selected taxonomy identifier.
        /// </value>
        public int SelectedTaxonomyId { get; set; }
        /// <summary>
        /// Gets or sets the taxonomy identifier.
        /// </summary>
        /// <value>
        /// The taxonomy identifier.
        /// </value>
        public List<DisplayValuePair> TaxonomyId { get; set; }
        /// <summary>
        /// Gets or sets the external identifier.
        /// </summary>
        /// <value>
        /// The external identifier.
        /// </value>
        [DisplayName("External Id")]
        public string ExternalId { get; set; }
        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        /// <value>
        /// The modified by.
        /// </value>
        [DisplayName("Modified By")]
        public int? ModifiedBy { get; set; }
        /// <summary>
        /// Gets or sets the UTC last modified date.
        /// </summary>
        /// <value>
        /// The UTC last modified date.
        /// </value>
        [DisplayName("Modified Date")]
        public DateTime? UTCLastModifiedDate { get; set; }
        /// <summary>
        /// Gets or sets the IsPrimary.
        /// </summary>
        /// <value>The IsPrimary.</value>
        [DisplayName("Is Primary")]
        public bool IsPrimary { get; set; }
        /// <summary>
        /// Gets or sets the ModifiedByName.
        /// </summary>
        /// <value>The ModifiedByName.</value>
        public string ModifiedByName { get; set; }
        /// <summary>
        /// Gets or sets the success or failed message.
        /// </summary>
        /// <value>
        /// The success or failed message.
        /// </value>
        public string SuccessOrFailedMessage { get; set; }
    }
}