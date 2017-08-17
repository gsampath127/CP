// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : NI317175
// Created          : 10-05-2015
//
// Last Modified By : NI317175
// Last Modified On : 11-17-2015
// ***********************************************************************
/// <summary>
/// The HostedPages namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.HostedPages
{
    /// <summary>
    /// Class HostedSiteNavigation.
    /// </summary>
    public class HostedSiteNavigation
    {
        /// <summary>
        /// Gets or sets the site identifier.
        /// </summary>
        /// <value>The site identifier.</value>
        public int SiteID { get; set; }

        /// <summary>
        /// Gets or sets the navigation key.
        /// </summary>
        /// <value>The navigation key.</value>
        public string NavigationKey { get; set; }

        /// <summary>
        /// Gets or sets the navigation XML.
        /// </summary>
        /// <value>The navigation XML.</value>
        public string NavigationXml { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is current production version.
        /// </summary>
        /// <value><c>true</c> if this instance is current production version; otherwise, <c>false</c>.</value>
        public bool IsCurrentProductionVersion { get; set; }
    }
}
