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
    public class ClientDocumentTypeSearchDetail : AuditedSearchDetail<ClientDocumentTypeObjectModel>, ISearchDetailCopyAs<ClientDocumentTypeSearchDetail>
    {
        #region Public Properties
        /// <summary>
        /// DocumentTypeID.
        /// </summary>
        /// <value>The client document type identifier.</value>
        public int? ClientDocumentTypeId { get; set; }
        /// <summary>
        /// Determines the type of comparison for the DocumentTypeId property.
        /// </summary>
        /// <value>The client document type identifier compare.</value>
        public ValueCompare ClientDocumentTypeIdCompare { get; set; }


        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public override Func<ClientDocumentTypeObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    && this.Match(this.ClientDocumentTypeId, entity.ClientDocumentTypeId, this.ClientDocumentTypeIdCompare)
                    && this.Match(this.Name, entity.Name, this.NameCompare)
                    && this.Match(this.Description, entity.Description, this.DescriptionCompare);
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
            where TCopy : ClientDocumentTypeSearchDetail, new()
        {
            TCopy copy = base.CopyAs<TCopy>();
            copy.ClientDocumentTypeId = this.ClientDocumentTypeId;
            copy.ClientDocumentTypeIdCompare = this.ClientDocumentTypeIdCompare;
            copy.Name = this.Name;
            copy.NameCompare = this.NameCompare;
            copy.Description = this.Description;
            copy.DescriptionCompare = this.DescriptionCompare;
            return copy;
        }

        #endregion
    }
}
