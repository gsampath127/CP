// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015
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
    public class DocumentTypeAssociationSearchDetail
        : AuditedSearchDetail<DocumentTypeAssociationObjectModel>, ISearchDetailCopyAs<DocumentTypeAssociationSearchDetail>
    {
        #region Public Properties
        /// <summary>
        /// DocumentTypeAssociationId.
        /// </summary>
        /// <value>The document type association identifier.</value>
        public int? DocumentTypeAssociationId { get; set; }
        /// <summary>
        /// Determines the type of comparison for the DocumentTypeAssociationId property.
        /// </summary>
        /// <value>The document type association identifier compare.</value>
        public ValueCompare DocumentTypeAssociationIdCompare { get; set; }
        /// <summary>
        /// DocumentTypeId.
        /// </summary>
        /// <value>The document type identifier.</value>
        public int? DocumentTypeId { get; set; }
        /// <summary>
        /// Determines the type of comparison for the DocumentTypeId property.
        /// </summary>
        /// <value>The document type identifier compare.</value>
        public ValueCompare DocumentTypeIdCompare { get; set; }
        /// <summary>
        /// SiteId.
        /// </summary>
        /// <value>The site identifier.</value>
        public int? SiteId { get; set; }
        /// <summary>
        /// Determines the type of comparison for the SiteId property.
        /// </summary>
        /// <value>The site identifier compare.</value>
        public ValueCompare SiteIdCompare { get; set; }
        /// <summary>
        /// TaxonomyAssociationId.
        /// </summary>
        /// <value>The taxonomy association identifier.</value>
        public int? TaxonomyAssociationId { get; set; }
        /// <summary>
        /// Determines the type of comparison for the TaxonomyAssociationId property.
        /// </summary>
        /// <value>The taxonomy association identifier compare.</value>
        public ValueCompare TaxonomyAssociationIdCompare { get; set; }
        /// <summary>
        /// HeaderText.
        /// </summary>
        /// <value>The header text.</value>
        public string HeaderText { get; set; }
        /// <summary>
        /// Determines the type of comparison for the HeaderText property..
        /// </summary>
        /// <value>The header text compare.</value>
        public TextCompare HeaderTextCompare { get; set; }
        /// <summary>
        /// Gets the LinkText.
        /// </summary>
        /// <value>The link text.</value>
        public string LinkText { get; set; }
        /// <summary>
        /// Determines the type of comparison for the LinkText property..
        /// </summary>
        /// <value>The link text compare.</value>
        public TextCompare LinkTextCompare { get; set; }
        /// <summary>
        /// MarketId.
        /// </summary>
        /// <value>The market identifier.</value>
        public string MarketId { get; set; }
        /// <summary>
        /// Determines the type of comparison for the MarketId property..
        /// </summary>
        /// <value>The market identifier compare.</value>
        public TextCompare MarketIdCompare { get; set; }

        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public override Func<DocumentTypeAssociationObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    && this.Match(this.DocumentTypeAssociationId, entity.DocumentTypeAssociationId, this.DocumentTypeAssociationIdCompare)
                    && this.Match(this.DocumentTypeId, entity.DocumentTypeId, this.DocumentTypeIdCompare)
                    && this.Match(this.SiteId, entity.SiteId, this.SiteIdCompare)
                    && this.Match(this.TaxonomyAssociationId, entity.TaxonomyAssociationId, this.TaxonomyAssociationIdCompare)
                    && this.Match(this.HeaderText, entity.HeaderText, this.HeaderTextCompare)
                    && this.Match(this.LinkText, entity.LinkText, this.LinkTextCompare)
                    && this.Match(this.MarketId, entity.MarketId, this.MarketIdCompare);
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
            where TCopy : DocumentTypeAssociationSearchDetail, new()
        {
            TCopy copy = base.CopyAs<TCopy>();
            copy.DocumentTypeAssociationId = this.DocumentTypeAssociationId;
            copy.DocumentTypeAssociationIdCompare = this.DocumentTypeAssociationIdCompare;
            copy.DocumentTypeId = this.DocumentTypeId;
            copy.DocumentTypeIdCompare = this.DocumentTypeIdCompare;
            copy.SiteId = this.SiteId;
            copy.SiteIdCompare = this.SiteIdCompare;
            copy.TaxonomyAssociationId = this.TaxonomyAssociationId;
            copy.TaxonomyAssociationIdCompare = this.TaxonomyAssociationIdCompare;
            copy.HeaderText = this.HeaderText;
            copy.HeaderTextCompare = this.HeaderTextCompare;
            copy.LinkText = this.LinkText;
            copy.LinkTextCompare = this.LinkTextCompare;
            copy.MarketId = this.MarketId;
            copy.MarketIdCompare = this.MarketIdCompare;

            return copy;
        }

        #endregion
    }
}
