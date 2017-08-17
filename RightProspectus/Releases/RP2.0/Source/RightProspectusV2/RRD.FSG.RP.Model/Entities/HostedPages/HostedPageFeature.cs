// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-17-2015
// ***********************************************************************


/// <summary>
/// The HostedPages namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.HostedPages
{
    /// <summary>
    /// Class HostedPageFeature.
    /// </summary>
    public class HostedPageFeature
    {
        /// <summary>
        /// Gets or sets the site identifier.
        /// </summary>
        /// <value>The site identifier.</value>
        public int SiteID { get; set; }

        /// <summary>
        /// Gets or sets the page identifier.
        /// </summary>
        /// <value>The page identifier.</value>
        public int PageID { get; set; }

        /// <summary>
        /// Gets or sets the feature key.
        /// </summary>
        /// <value>The feature key.</value>
        public string FeatureKey { get; set; }

        /// <summary>
        /// Gets or sets the feature mode.
        /// </summary>
        /// <value>The feature mode.</value>
        public int FeatureMode { get; set; }
    }
}
