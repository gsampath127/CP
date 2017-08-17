// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-31-2015

using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace RRD.FSG.RP.Model.SearchEntities.Client
{// <summary>
    /// <summary>
    /// Class PageNavigationSearchDetail.
    /// </summary>
   public class PageNavigationSearchDetail 
       : AuditedSearchDetail<PageNavigationObjectModel>, ISearchDetailCopyAs<PageNavigationSearchDetail>
    {
        #region Public Properties
        /// <summary>
        /// PageNavigationId.
        /// </summary>
        /// <value>The page navigation identifier.</value>
        public int? PageNavigationId { get; set; }
        /// <summary>
        /// Determines the type of comparison for the PageNavigationId property.
        /// </summary>
        /// <value>The page navigation identifier compare.</value>
        public ValueCompare PageNavigationIdCompare { get; set; }
        /// <summary>
        /// PageId.
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
        /// NavigationKey.
        /// </summary>
        /// <value>The navigation key.</value>
        public string NavigationKey { get; set; }
        /// <summary>
        /// Determines the type of comparison for the NavigationKey property.
        /// </summary>
        /// <value>The navigation key compare.</value>
        public TextCompare NavigationKeyCompare { get; set; }
        /// <summary>
        /// CurrentVersion.
        /// </summary>
        /// <value>The current version.</value>
        public int? CurrentVersion { get; set; }
        /// <summary>
        /// Determines the type of comparison for the CurrentVersion property.
        /// </summary>
        /// <value>The current version compare.</value>
        public ValueCompare CurrentVersionCompare { get; set; }
        /// <summary>
        /// IsProofing.
        /// </summary>
        /// <value><c>null</c> if [is proofing] contains no value, <c>true</c> if [is proofing]; otherwise, <c>false</c>.</value>
        public bool? IsProofing { get; set; }
        /// <summary>
        /// Determines the type of comparison for the IsProofing property.
        /// </summary>
        /// <value>The is proofing compare.</value>
        public ValueCompare IsProofingCompare { get; set; }


        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public override Func<PageNavigationObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    && this.Match(this.PageNavigationId, entity.PageNavigationId, this.PageNavigationIdCompare)
                    && this.Match(this.PageID, entity.PageId, this.PageIDCompare)
                    && this.Match(this.SiteID, entity.SiteId, this.SiteIDCompare)
                   && this.Match(this.NavigationKey, entity.NavigationKey, this.NavigationKeyCompare)
                  && this.Match(this.CurrentVersion, entity.Version, this.CurrentVersionCompare)
                  && this.Match(this.IsProofing, entity.IsProofing, this.IsProofingCompare);
            }
        }
        #endregion

        #region Public Methods
        // <summary>
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
            where TCopy:PageNavigationSearchDetail, new()
        {
            TCopy copy = base.CopyAs<TCopy>();
            copy.PageNavigationId = this.PageNavigationId;
            copy.PageNavigationIdCompare = this.PageNavigationIdCompare;
            copy.PageID = this.PageID;
            copy.PageIDCompare = this.PageIDCompare;
            copy.SiteID = this.SiteID;
            copy.SiteIDCompare = this.SiteIDCompare;
            copy.NavigationKey = this.NavigationKey;
            copy.NavigationKeyCompare = this.NavigationKeyCompare;
            copy.CurrentVersion = this.CurrentVersion;
            copy.CurrentVersionCompare = this.CurrentVersionCompare;
            return copy;
        }
#endregion
    }
}