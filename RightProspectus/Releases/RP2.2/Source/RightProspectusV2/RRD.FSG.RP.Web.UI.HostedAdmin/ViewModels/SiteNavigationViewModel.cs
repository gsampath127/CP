// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-31-2015

using System.ComponentModel;

/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// Class SiteNavigationViewModel.
    /// </summary>
    public class SiteNavigationViewModel
    {
        /// <summary>
        /// SiteNavigationId
        /// </summary>
        /// <value>The site navigation identifier.</value>
        public int SiteNavigationId { get; set; }
        /// <summary>
        /// Page Name
        /// </summary>
        /// <value>The name of the page.</value>
        public string PageName { get; set; }
        /// <summary>
        /// PageDescription
        /// </summary>
        /// <value>The page description.</value>
        public string PageDescription { get; set; }
        //public string PageDescription { get; set; }
        /// <summary>
        /// NavigationKey
        /// </summary>
        /// <value>The navigation key.</value>
        [DisplayName("Navigation Key")]
        public string NavigationKey { get; set; }
        /// <summary>
        /// UTCModifiedDate
        /// </summary>
        /// <value>The UTC modified date.</value>
        public string UtcModifiedDate { get; set; }
        /// <summary>
        /// ModifiedBy
        /// </summary>
        /// <value>The modified by.</value>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// IsProofing Version or Production Version
        /// </summary>
        /// <value>The version.</value>
        [DisplayName("Version")]
        public string Version { get; set; }
        /// <summary>
        /// VersionID from Page Text Version table
        /// </summary>
        /// <value>The version identifier.</value>
        public int VersionID { get; set; }
        /// <summary>
        /// Is ProofingVersion Available for PagetextID
        /// </summary>
        /// <value><c>true</c> if this instance is proofing available for page text identifier; otherwise, <c>false</c>.</value>
        public bool IsProofingAvailableForPageTextId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is proofing.
        /// </summary>
        /// <value><c>true</c> if this instance is proofing; otherwise, <c>false</c>.</value>
        public bool IsProofing { get; set; }
    }
}

