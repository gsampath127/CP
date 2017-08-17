// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
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
    /// Class HostedPageText.
    /// </summary>
    public class HostedPageText
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
