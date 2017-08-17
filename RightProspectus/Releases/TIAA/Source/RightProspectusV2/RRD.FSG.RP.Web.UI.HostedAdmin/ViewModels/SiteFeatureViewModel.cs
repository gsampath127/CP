
/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// Class SiteFeatureViewModel.
    /// </summary>
    public class SiteFeatureViewModel
    {
        /// <summary>
        /// Gets or sets the SiteId.
        /// </summary>
        /// <value>The SiteId .</value>
        public int SiteId { get; set; }

        /// <summary>
        /// Gets or sets the SiteKey.
        /// </summary>
        /// <value>The SiteKey .</value>
        public string SiteKey { get; set; }

        /// <summary>
        /// Gets or sets the FeatureMode.
        /// </summary>
        /// <value>The FeatureMode .</value>
        public int FeatureMode { get; set; }

        /// <summary>
        /// Gets or sets the feature modes.
        /// </summary>
        /// <value>The feature modes.</value>
        public string FeatureModes { get; set; }
    }
}