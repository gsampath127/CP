// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************

using System.ComponentModel;

/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// Class PageTextViewModel.
    /// </summary>
    public class PageTextViewModel
    {
        /// <summary>
        /// PageTextID
        /// </summary>
        /// <value>The page text identifier.</value>
        public int PageTextID { get; set; }
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
        /// <summary>
        /// ResouceKey
        /// </summary>
        /// <value>The resource key.</value>
        [DisplayName("Resource Key")]
        public string ResourceKey { get; set; }
        /// <summary>
        /// Text for the Resource key
        /// </summary>
        /// <value>The text.</value>
        [DisplayName("Text")]
        public string Text { get; set; }
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