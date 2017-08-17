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
    /// Class HostedSiteText.
    /// </summary>
    public class HostedSiteText
    {
        /// <summary>
        /// Gets or sets the site identifier.
        /// </summary>
        /// <value>The site identifier.</value>
        public int SiteID { get; set; }

        /// <summary>
        /// Gets or sets the resource key.
        /// </summary>
        /// <value>The resource key.</value>
        public string ResourceKey { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is current production version.
        /// </summary>
        /// <value><c>true</c> if this instance is current production version; otherwise, <c>false</c>.</value>
        public bool IsCurrentProductionVersion { get; set; }

    }

}
