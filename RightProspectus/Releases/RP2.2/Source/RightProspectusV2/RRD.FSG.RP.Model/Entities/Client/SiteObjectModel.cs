// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-13-2015
// ***********************************************************************

using System;
using System.Data;

/// <summary>
/// The Client namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.Client
{
    /// <summary>
    /// Class SiteObjectModel.
    /// </summary>
    public class SiteObjectModel : AuditedBaseModel<int>, IComparable<SiteObjectModel>
    {
        /// <summary>
        /// SiteID
        /// </summary>
        /// <value>The site identifier.</value>
        public int SiteID { get; set; }
        /// <summary>
        /// ClientId
        /// </summary>
        /// <value>The client identifier.</value>
        public int ClientId { get; set; }
        /// <summary>
        /// TemplateId
        /// </summary>
        /// <value>The template identifier.</value>
        public int TemplateId { get; set; }
        /// <summary>
        /// DefaultPageId
        /// </summary>
        /// <value>The default page identifier.</value>
        public int DefaultPageId { get; set; }
        /// <summary>
        /// ParentSiteId
        /// </summary>
        /// <value>The parent site identifier.</value>
        public int? ParentSiteId { get; set; }
        /// <summary>
        /// IsDefaultSite
        /// </summary>
        /// <value><c>true</c> if this instance is default site; otherwise, <c>false</c>.</value>
        public bool IsDefaultSite { get; set; }
        /// <summary>
        /// TemplateName
        /// </summary>
        /// <value>The name of the template.</value>
        public string TemplateName { get; set; }
        /// <summary>
        /// DefaultPageName
        /// </summary>
        /// <value>The default name of the page.</value>
        public string DefaultPageName { get; set; }
        /// <summary>
        /// PageDescription
        /// </summary>
        /// <value>The page description.</value>
        public string PageDescription { get; set; }
        /// <summary>
        /// TemplateText
        /// </summary>
        /// <value>The template text.</value>
        public DataTable TemplateText { get; set; }
        /// <summary>
        /// TemplatePageText
        /// </summary>
        /// <value>The template page text.</value>
        public DataTable TemplatePageText { get; set; }
        /// <summary>
        /// TemplateNavigation
        /// </summary>
        /// <value>The template navigation.</value>
        public DataTable TemplateNavigation { get; set; }
        /// <summary>
        /// TemplatePageNavigation
        /// </summary>
        /// <value>The template page navigation.</value>
        public DataTable TemplatePageNavigation { get; set; }
        /// <summary>
        /// Compares the two Site entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(SiteObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }

    }
}
