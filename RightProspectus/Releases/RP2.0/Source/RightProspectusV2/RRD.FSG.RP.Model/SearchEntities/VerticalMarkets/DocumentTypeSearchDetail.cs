// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-03-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.VerticalMarket;
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace RRD.FSG.RP.Model.SearchEntities.VerticalMarket
{
    /// <summary>
    /// Defines a search detail entity.
    /// Can be used for building a list of search parameters for the GetEntitiesBySearch method of <see cref="I:IFactory" />.
    /// Can also be used to search a collection of entities directly.
    /// </summary>
    public class DocumentTypeSearchDetail
        : SearchDetail<DocumentTypeObjectModel>, ISearchDetailCopyAs<DocumentTypeSearchDetail>
    {
        #region Public Properties
        /// <summary>
        /// DocumentTypeID.
        /// </summary>
        /// <value>The document type identifier.</value>
        public int? DocumentTypeId { get; set; }
        /// <summary>
        /// Determines the type of comparison for the DocumentTypeId property.
        /// </summary>
        /// <value>The document type identifier compare.</value>
        public ValueCompare DocumentTypeIdCompare { get; set; }
        /// <summary>
        /// SiteName.
        /// </summary>
        /// <value>The name of the document type.</value>
        public string DocumentTypeName { get; set; }
        /// <summary>
        /// Determines the type of comparison for the DocumentTypeName property..
        /// </summary>
        /// <value>The document type name compare.</value>
        public TextCompare DocumentTypeNameCompare { get; set; }

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
        public override Func<DocumentTypeObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    && this.Match(this.DocumentTypeId,entity.DocumentTypeId,this.DocumentTypeIdCompare)
                    && this.Match(this.DocumentTypeName,entity.Name,this.DocumentTypeNameCompare)
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
            where TCopy : DocumentTypeSearchDetail, new()
        {
            TCopy copy = base.CopyAs<TCopy>();
            copy.DocumentTypeId = this.DocumentTypeId;
            copy.DocumentTypeIdCompare = this.DocumentTypeIdCompare;
            copy.DocumentTypeName = this.DocumentTypeName;
            copy.DocumentTypeNameCompare = this.DocumentTypeNameCompare;
            return copy;
        }

        #endregion
    }
}
