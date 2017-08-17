// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-03-2015
// ***********************************************************************

using System.ComponentModel;


/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// Class TaxonomyLevelExternalIdViewModel.
    /// </summary>
    public class TaxonomyLevelExternalIdViewModel
    {
        /// <summary>
        /// Gets or sets the level identifier.
        /// </summary>
        /// <value>The level identifier.</value>
        public int LevelId { get; set; }
        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>The level.</value>
        public string Level { get; set; }
        /// <summary>
        /// Gets or sets the taxonomy identifier.
        /// </summary>
        /// <value>The taxonomy identifier.</value>
        public int TaxonomyId { get; set; }
        /// <summary>
        /// Gets or sets the taxonomy.
        /// </summary>
        /// <value>The taxonomy.</value>
        public string Taxonomy { get; set; }
        /// <summary>
        /// Gets or sets the external identifier.
        /// </summary>
        /// <value>The external identifier.</value>
        public string ExternalId { get; set; }
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
        public string UTCLastModifiedDate { get; set; }




        /// <summary>
        /// Gets or sets the Is Primary.
        /// </summary>
        /// <value>The UTC last modified date.</value>
        [DisplayName("Is Primary")]
        public bool IsPrimary { get; set; }
        /// <summary>
        /// Gets or sets the success or failed message.
        /// </summary>
        /// <value>The success or failed message.</value>
        public string SuccessOrFailedMessage { get; set; }
    }
}