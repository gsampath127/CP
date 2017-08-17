// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015
// ***********************************************************************
using System.Collections.Generic;

/// <summary>
/// The HostedPages namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.HostedPages
{
    /// <summary>
    /// Class ClientSiteData.
    /// </summary>
    public class ClientSiteData
    {
        /// <summary>
        /// Gets or sets the site texts.
        /// </summary>
        /// <value>The site texts.</value>
        public List<HostedSiteText> SiteTexts { get; set; }

        /// <summary>
        /// Gets or sets the page texts.
        /// </summary>
        /// <value>The page texts.</value>
        public List<HostedPageText> PageTexts { get; set; }

        /// <summary>
        /// Gets or sets the sites.
        /// </summary>
        /// <value>The sites.</value>
        public List<HostedSite> Sites { get; set; }

        /// <summary>
        /// Gets or sets the static resources.
        /// </summary>
        /// <value>The static resources.</value>
        public List<HostedStaticResource> StaticResources { get; set; }

        /// <summary>
        /// Gets or sets the site navigations.
        /// </summary>
        /// <value>The site navigations.</value>
        public List<HostedSiteNavigation> SiteNavigations { get; set; }

        /// <summary>
        /// Gets or sets the page navigations.
        /// </summary>
        /// <value>The page navigations.</value>
        public List<HostedPageNavigation> PageNavigations { get; set; }

        /// <summary>
        /// Gets or sets the site features.
        /// </summary>
        /// <value>The site features.</value>
        public List<HostedSiteFeature> SiteFeatures { get; set; }

        /// <summary>
        /// Gets or sets the page features.
        /// </summary>
        /// <value>The page features.</value>
        public List<HostedPageFeature> PageFeatures { get; set; }
        
    }
}
