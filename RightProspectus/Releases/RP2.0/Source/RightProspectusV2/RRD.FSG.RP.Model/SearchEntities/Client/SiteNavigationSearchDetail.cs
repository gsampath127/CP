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
{
    /// <summary>
    /// Defines a search detail entity.
    /// Can be used for building a list of search parameters for the GetEntitiesBySearch method of <see cref="I:IFactory" />.
    /// Can also be used to search a collection of entities directly.
    /// </summary>
    public class SiteNavigationSearchDetail
        : AuditedSearchDetail<SiteNavigationObjectModel>, ISearchDetailCopyAs<SiteNavigationSearchDetail>
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the StaticResource identifier.
        /// </summary>
        /// <value>The StaticResourceId identifier.</value>
        public int? SiteNavigationId { get; set; }
        /// <summary>
        /// Determines the type of comparison for the StaticResourceId property.
        /// </summary>
        /// <value>The site navigation identifier compare.</value>
        public ValueCompare SiteNavigationIdCompare { get; set; }

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
        /// Gets or sets the site identifier.
        /// </summary>
        /// <value>The site identifier.</value>
        public int? SiteId { get; set; }
        /// <summary>
        /// Determines the type of comparison for the StaticResourceId property.
        /// </summary>
        /// <value>The site identifier compare.</value>
        public ValueCompare SiteIdCompare { get; set; }
        /// <summary>
        /// Gets or sets the FileName identifier.
        /// </summary>
        /// <value>The FileName.</value>
        public string NavigationKey { get; set; }
        /// <summary>
        /// Determines the type of comparison for the FileName property..
        /// </summary>
        /// <value>The navigation key compare.</value>
        public TextCompare NavigationKeyCompare { get; set; }
        /// <summary>
        /// Gets or sets the Size identifier.
        /// </summary>
        /// <value>The Size.</value>
        public int? PageId { get; set; }
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
        /// Determines the type of comparison for the Size property.
        /// </summary>
        /// <value>The page identifier compare.</value>
        public ValueCompare PageIdCompare { get; set; }
        /// <summary>
        /// Gets or sets the MimeType identifier.
        /// </summary>
        /// <value>The MimeType.</value>
        public string LanguageCulture { get; set; }
        /// <summary>
        /// Determines the type of comparison for the MimeType property..
        /// </summary>
        /// <value>The language culture compare.</value>
        public TextCompare LanguageCultureCompare { get; set; }

        /// <summary>
        /// Gets the last modified date of the entities being searched. Must be UTC.
        /// </summary>
        /// <value>The modified by.</value>
        public int? ModifiedBy { get; set; }
        /// <summary>
        /// Determines the type of comparison for the Size property.
        /// </summary>
        /// <value><c>null</c> if [is proofing] contains no value, <c>true</c> if [is proofing]; otherwise, <c>false</c>.</value>
        public bool? IsProofing { get; set; }
        /// <summary>
        /// Determines the type of comparison for the NavigationKey property..
        /// </summary>
        /// <value>The is proofing compare.</value>
        public ValueCompare IsProofingCompare { get; set; }

        /// <summary>
        /// Determines the type of comparison for the ModifiedBy property.
        /// </summary>
        /// <value>The modified by compare.</value>
        public ValueCompare ModifiedByCompare { get; set; }


        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public override Func<SiteNavigationObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    && this.Match(this.SiteNavigationId, entity.SiteNavigationId, this.SiteNavigationIdCompare)
                    && this.Match(this.TemplateId, entity.TemplateId, this.TemplateIdCompare)
                    && this.Match(this.NavigationKey, entity.NavigationKey, this.NavigationKeyCompare)
                    && this.Match(this.PageId, entity.PageId, this.PageIdCompare)
                    && this.Match(this.SiteId, entity.SiteId, this.SiteIdCompare)
                    && this.Match(this.Version, entity.Version, this.VersionCompare)
                    && this.Match(this.IsProofing, entity.IsProofing, this.IsProofingCompare);

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
            where TCopy : SiteNavigationSearchDetail, new()
        {
            TCopy copy = base.CopyAs<TCopy>();
            copy.SiteNavigationId = this.SiteNavigationId;
            copy.TemplateId = this.TemplateId;
            copy.TemplateIdCompare = this.TemplateIdCompare;
            copy.SiteNavigationIdCompare = this.SiteNavigationIdCompare;
            copy.NavigationKey = this.NavigationKey;
            copy.NavigationKeyCompare = this.NavigationKeyCompare;
            copy.PageId = this.PageId;
            copy.PageIdCompare = this.PageIdCompare;
            copy.ModifiedBy = this.ModifiedBy;
            copy.ModifiedByCompare = this.ModifiedByCompare;
            copy.Version = this.Version;
            copy.VersionCompare = this.VersionCompare;
            copy.IsProofing = this.IsProofing;
            copy.IsProofingCompare = this.IsProofingCompare;

            return copy;
        }

        #endregion
    }
}
