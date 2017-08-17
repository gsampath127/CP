
/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// Class SiteTextViewModel.
    /// </summary>
    public class SiteTextViewModel
    {
        /// <summary>
        /// SiteTextID
        /// </summary>
        /// <value>The site text identifier.</value>
        public int SiteTextID { get; set; }
        /// <summary>
        /// SiteID
        /// </summary>
        /// <value>The site identifier.</value>
        public int SiteID { get; set; }
        /// <summary>
        /// ResouceKey
        /// </summary>
        /// <value>The resource key.</value>
        public string ResourceKey { get; set; }
        /// <summary>
        /// Text for the Resource key
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }
        /// <summary>
        /// Version
        /// </summary>
        /// <value>The version.</value>
        public string Version { get; set; }
        /// <summary>
        /// Version identifier
        /// </summary>
        /// <value>The version identifier.</value>
        public int VersionID { get; set; }
        /// <summary>
        /// IsProofing Version
        /// </summary>
        /// <value><c>true</c> if this instance is proofing; otherwise, <c>false</c>.</value>
        public bool IsProofing { get; set; }
        /// <summary>
        /// Is ProofingVersion Available for SitetextID
        /// </summary>
        /// <value><c>true</c> if this instance is proofing available for site text identifier; otherwise, <c>false</c>.</value>
        public bool IsProofingAvailableForSiteTextId { get; set; }
    }
}