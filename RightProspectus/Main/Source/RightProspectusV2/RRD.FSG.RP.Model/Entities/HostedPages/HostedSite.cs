// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************
using System;

/// <summary>
/// The HostedPages namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.HostedPages
{
    /// <summary>
    /// Class HostedSite.
    /// </summary>
    public class HostedSite
    {
        /// <summary>
        /// Gets or sets the site identifier.
        /// </summary>
        /// <value>The site identifier.</value>
        public int SiteId { get; set; }

        /// <summary>
        /// Gets or sets the name of the site.
        /// </summary>
        /// <value>The name of the site.</value>
        public string SiteName { get; set; }

        /// <summary>
        /// Gets or sets the template identifier.
        /// </summary>
        /// <value>The template identifier.</value>
        public int TemplateId { get; set; }

        /// <summary>
        /// Gets or sets the default page identifier.
        /// </summary>
        /// <value>The default page identifier.</value>
        public int DefaultPageId { get; set; }

        /// <summary>
        /// Gets or sets the parent site identifier.
        /// </summary>
        /// <value>The parent site identifier.</value>
        public int ParentSiteId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the UTC modified date.
        /// </summary>
        /// <value>The UTC modified date.</value>
        public DateTime UtcModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the modified.
        /// </summary>
        /// <value>The modified.</value>
        public int Modified { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is default site.
        /// </summary>
        /// <value><c>true</c> if this instance is default site; otherwise, <c>false</c>.</value>
        public bool IsDefaultSite { get; set; }
    }
}
