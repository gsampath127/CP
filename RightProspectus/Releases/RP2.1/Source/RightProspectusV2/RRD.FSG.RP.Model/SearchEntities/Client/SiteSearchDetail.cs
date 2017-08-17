// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace RRD.FSG.RP.Model.SearchEntities.Client
{
    /// <summary>
    /// Defines a search detail entity.
    /// Can be used for building a list of search parameters for the GetEntitiesBySearch method of <see cref="I:IFactory" />.
    /// Can also be used to search a collection of entities directly.
    /// </summary>
    public class SiteSearchDetail
        : AuditedSearchDetail<SiteObjectModel>, ISearchDetailCopyAs<SiteSearchDetail>
    {
        #region Public Properties
        /// <summary>
        /// SiteID
        /// </summary>
        /// <value>The site identifier.</value>
        public int? SiteID { get; set; }
        /// <summary>
        /// Determines the type of comparison for the SiteID property.
        /// </summary>
        /// <value>The site identifier compare.</value>
        public ValueCompare SiteIDCompare { get; set; }
        /// <summary>
        /// TemplateId
        /// </summary>
        /// <value>The template identifier.</value>
        public int? TemplateId { get; set; }
        /// <summary>
        /// Determines the type of comparison for the TemplateId property.
        /// </summary>
        /// <value>The template identifier compare.</value>
        public ValueCompare TemplateIdCompare { get; set; }
        /// <summary>
        /// DefaultPageId
        /// </summary>
        /// <value>The default page identifier.</value>
        public int? DefaultPageId { get; set; }
        /// <summary>
        /// Determines the type of comparison for the DefaultPageId property.
        /// </summary>
        /// <value>The default page identifier compare.</value>
        public ValueCompare DefaultPageIdCompare { get; set; }
        /// <summary>
        /// ParentSiteId
        /// </summary>
        /// <value>The parent site identifier.</value>
        public int? ParentSiteId { get; set; }
        /// <summary>
        /// Determines the type of comparison for the ParentSiteId property.
        /// </summary>
        /// <value>The parent site identifier compare.</value>
        public ValueCompare ParentSiteIdCompare { get; set; }

        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public override Func<SiteObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    && this.Match(this.SiteID,entity.SiteID,this.SiteIDCompare)
                    && this.Match(this.TemplateId,entity.TemplateId,this.TemplateIdCompare)
                    && this.Match(this.DefaultPageId,entity.DefaultPageId,this.DefaultPageIdCompare)
                    && this.Match(this.ParentSiteId,entity.ParentSiteId,this.ParentSiteIdCompare );
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns an enumeration of DbParameter entities based on values set in th underlying search detail entity.
        /// </summary>
        /// <param name="dataAccess">Data access instance used to create the parameters.</param>
        /// <returns>A collection of DbParameter entities.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override IEnumerable<DbParameter> GetSearchParameters(IDataAccess dataAccess)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new search detail and copies the parameters from this one to it.
        /// </summary>
        /// <typeparam name="TCopy">Type of search detail to create.</typeparam>
        /// <returns>A new search detail with the same search parameters as this one.</returns>
        public virtual new TCopy CopyAs<TCopy>()
            where TCopy : SiteSearchDetail, new()
        {
            TCopy copy = base.CopyAs<TCopy>();
            copy.SiteID = this.SiteID;
            copy.SiteIDCompare = this.SiteIDCompare;
            copy.TemplateId = this.TemplateId;
            copy.TemplateIdCompare = this.TemplateIdCompare;
            copy.DefaultPageId = this.DefaultPageId;
            copy.DefaultPageIdCompare = this.DefaultPageIdCompare;
            copy.ParentSiteId = this.ParentSiteId;
            copy.ParentSiteIdCompare = this.ParentSiteIdCompare;
            return copy;
        }

        #endregion
    }
}
