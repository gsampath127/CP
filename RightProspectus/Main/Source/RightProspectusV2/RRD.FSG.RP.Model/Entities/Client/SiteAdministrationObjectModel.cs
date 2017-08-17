// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By :
// Last Modified On : 11-18-2015
// ***********************************************************************
/// <summary>
/// The Client namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.Client
{
    /// <summary>
    /// Class SiteAdministrationObjectSearchModel.
    /// </summary>
    public class SiteAdministrationObjectSearchModel : SearchBaseModel
    {
        #region SearchProperties

        /// <summary>
        /// Gets or sets the client name for making connection with database
        /// </summary>
        /// <value>The name of the client.</value>
        public string ClientName { get; set; }
        /// <summary>
        /// Gets or sets the site identifier.
        /// </summary>
        /// <value>The site identifier.</value>
        public int? SiteID { get; set; }
        /// <summary>
        /// Gets or sets the name of the site.
        /// </summary>
        /// <value>The name of the site.</value>
        public string SiteName { get; set; }
        /// <summary>
        /// Gets or sets the name of the template.
        /// </summary>
        /// <value>The name of the Template.</value>
        public string TemplateName { get; set; }
        /// <summary>
        /// Gets or sets the name of the defaultPage.
        /// </summary>
        /// <value>The name of the default page.</value>
        public string DefaultPageName { get; set; }
        /// <summary>
        /// Gets or sets the parentSiteId.
        /// </summary>
        /// <value>The parentSiteId.</value>
        public int? ParentSiteID { get; set; }
        /// <summary>
        /// Gets or sets the name of the description.
        /// </summary>
        /// <value>The site description.</value>
        public string Description { get; set; }
        
        #endregion
    }

    /// <summary>
    /// Class SiteAdministrationObjectModel.
    /// </summary>
    public class SiteAdministrationObjectModel : AuditedBaseModel<int>
    {
        #region EntityProperties
        /// <summary>
        /// Gets or sets the site identifier.
        /// </summary>
        /// <value>The site identifier.</value>
        public int SiteID { get; set; }
        /// <summary>
        /// Gets or sets the name of the site.
        /// </summary>
        /// <value>The name of the site.</value>
        public string SiteName { get; set; }
        /// <summary>
        /// Gets or sets the TemplateID.
        /// </summary>
        /// <value>The  TemplateId.</value>
        public int TemplateID { get; set; }
        /// <summary>
        /// Gets or sets the name of the DefaultPageID.
        /// </summary>
        /// <value>The DefaultPageID.</value>
        public int DefaultPageID { get; set; }
        /// <summary>
        /// Gets or sets the name of the description.
        /// </summary>
        /// <value>The site description.</value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the parentSiteId.
        /// </summary>
        /// <value>The parentSiteId.</value>
        public int  ParentSiteID{ get; set; }
        #endregion
    }
}
