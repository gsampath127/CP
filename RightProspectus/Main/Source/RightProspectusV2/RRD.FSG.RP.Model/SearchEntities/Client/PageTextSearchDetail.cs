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
    public class PageTextSearchDetail
        : AuditedSearchDetail<PageTextObjectModel>, ISearchDetailCopyAs<PageTextSearchDetail>
    {
        #region Public Properties
        /// <summary>
        /// PageTextID.
        /// </summary>
        /// <value>The page text identifier.</value>
        public int? PageTextID { get; set; }
        /// <summary>
        /// Determines the type of comparison for the PageTextID property.
        /// </summary>
        /// <value>The page text identifier compare.</value>
        public ValueCompare PageTextIDCompare { get; set; }
        /// <summary>
        /// Version.
        /// </summary>
        /// <value>The version.</value>
        public int? Version { get; set; }
        /// <summary>
        /// Determines the type of comparison for the Version property.
        /// </summary>
        /// <value>The version compare.</value>
        public ValueCompare VersionCompare { get; set; }
        /// <summary>
        /// PageID.
        /// </summary>
        /// <value>The page identifier.</value>
        public int? PageID { get; set; }
        /// <summary>
        /// Determines the type of comparison for the PageID property.
        /// </summary>
        /// <value>The page identifier compare.</value>
        public ValueCompare PageIDCompare { get; set; }
        /// <summary>
        /// SiteID.
        /// </summary>
        /// <value>The site identifier.</value>
        public int? SiteID { get; set; }
        /// <summary>
        /// Determines the type of comparison for the SiteID property.
        /// </summary>
        /// <value>The site identifier compare.</value>
        public ValueCompare SiteIDCompare { get; set; }
        /// <summary>
        /// SiteName.
        /// </summary>
        /// <value>The name of the site.</value>
        public string SiteName { get; set; }
        /// <summary>
        /// Determines the type of comparison for the SiteName property..
        /// </summary>
        /// <value>The site name compare.</value>
        public TextCompare SiteNameCompare { get; set; }
        /// <summary>
        /// Gets the ResourceKey.
        /// </summary>
        /// <value>The resource key.</value>
        public string ResourceKey { get; set; }
        /// <summary>
        /// Determines the type of comparison for the ResourceKey property..
        /// </summary>
        /// <value>The resource key compare.</value>
        public TextCompare ResourceKeyCompare { get; set; }
        /// <summary>
        /// IsProofing.
        /// </summary>
        /// <value><c>null</c> if [is proofing] contains no value, <c>true</c> if [is proofing]; otherwise, <c>false</c>.</value>
        public bool? IsProofing { get; set; }
        /// <summary>
        /// Determines the type of comparison for the ResourceKey property..
        /// </summary>
        /// <value>The is proofing compare.</value>
        public ValueCompare IsProofingCompare { get; set; }


        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public override Func<PageTextObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    &&  this.Match(this.PageTextID,entity.PageTextID,this.PageTextIDCompare)
                    && this.Match(this.Version,entity.Version,this.VersionCompare)
                    && this.Match(this.PageID,entity.PageID,this.PageIDCompare)
                    && this.Match(this.SiteID,entity.SiteID,this.SiteIDCompare)
                    && this.Match(this.SiteName, entity.SiteName,this.SiteNameCompare)
                    && this.Match(this.ResourceKey,entity.ResourceKey,this.ResourceKeyCompare)
                    && this.Match(this.IsProofing,entity.IsProofing,this.IsProofingCompare);
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
            where TCopy : PageTextSearchDetail, new()
        {
            TCopy copy = base.CopyAs<TCopy>();
            copy.PageTextID = this.PageTextID;
            copy.PageTextIDCompare = this.PageTextIDCompare;
            copy.Version = this.Version;
            copy.VersionCompare = this.VersionCompare;
            copy.SiteID = this.SiteID;
            copy.SiteIDCompare = this.SiteIDCompare;
            copy.Version = this.Version;
            copy.VersionCompare = this.VersionCompare;
            copy.SiteName = this.SiteName;
            copy.SiteNameCompare = this.SiteNameCompare;
            copy.ResourceKey = this.ResourceKey;
            copy.ResourceKeyCompare = this.ResourceKeyCompare;
            return copy;
        }

        #endregion
    }
}
