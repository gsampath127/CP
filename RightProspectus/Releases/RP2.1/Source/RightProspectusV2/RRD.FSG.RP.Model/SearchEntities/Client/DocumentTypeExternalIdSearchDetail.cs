// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-16-2015
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
    public class DocumentTypeExternalIdSearchDetail
        : AuditedSearchDetail<DocumentTypeExternalIdObjectModel>, ISearchDetailCopyAs<DocumentTypeExternalIdSearchDetail>
    {
        #region Public Properties
        /// <summary>
        /// DocumentTypeID.
        /// </summary>
        /// <value>The document type identifier.</value>
        public int? DocumentTypeID { get; set; }
        /// <summary>
        /// Determines the type of comparison for the DocumentTypeID property.
        /// </summary>
        /// <value>The document type identifier compare.</value>
        public ValueCompare DocumentTypeIDCompare { get; set; }
        /// <summary>
        /// DocumentTypeName.
        /// </summary>
        /// <value>The name of the document type.</value>
        public string DocumentTypeName { get; set; }
        /// <summary>
        /// Determines the type of comparison for the DocumentTypeName property..
        /// </summary>
        /// <value>The document type name compare.</value>
        public TextCompare DocumentTypeNameCompare { get; set; }
        /// <summary>
        /// ExternalId.
        /// </summary>
        /// <value>The external identifier.</value>
        public string ExternalId { get; set; }
        /// <summary>
        /// Determines the type of comparison for the ExternalId property.
        /// </summary>
        /// <value>The external identifier compare.</value>
        public TextCompare ExternalIdCompare { get; set; }



        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public override Func<DocumentTypeExternalIdObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    && this.Match(this.DocumentTypeID,entity.DocumentTypeId,this.DocumentTypeIDCompare)
                    && this.Match(this.DocumentTypeName,entity.DocumentTypeName,this.DocumentTypeNameCompare)
                    && this.Match(this.ExternalId,entity.ExternalId,this.ExternalIdCompare);
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
            where TCopy : DocumentTypeExternalIdSearchDetail, new()
        {
            TCopy copy = base.CopyAs<TCopy>();
            copy.DocumentTypeID = this.DocumentTypeID;
            copy.DocumentTypeIDCompare = this.DocumentTypeIDCompare;
            copy.DocumentTypeName = this.DocumentTypeName;
            copy.DocumentTypeNameCompare = this.DocumentTypeNameCompare;
            copy.ExternalId = this.ExternalId;
            copy.ExternalIdCompare = this.ExternalIdCompare;
            return copy;
        }

        #endregion
    }
}
