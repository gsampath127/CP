// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-29-2015
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
    /// Class ClientDocumentGroupSearchDetail.
    /// </summary>
    public class ClientDocumentGroupSearchDetail : AuditedSearchDetail<ClientDocumentGroupObjectModel>, ISearchDetailCopyAs<ClientDocumentGroupSearchDetail>
    {
        
        #region Public Properties
        /// <summary>
        /// ClientDocumentGroupId
        /// </summary>
        /// <value>The client document group identifier.</value>
        public int ClientDocumentGroupId { get; set; }

        /// <summary>
        /// Determines the type of comparison for the ClientDocumentGroupId property.
        /// </summary>
        /// <value>The client document group identifier compare.</value>
        public ValueCompare ClientDocumentGroupIdCompare { get; set; }

        /// <summary>
        /// ParentClientDocumentGroupId
        /// </summary>
        /// <value>The parent client document group identifier.</value>
        public int? ParentClientDocumentGroupId { get; set; }

        /// <summary>
        /// Determines the type of comparison for the ParentClientDocumentGroupId property.
        /// </summary>
        /// <value>The parent client document group identifier compare.</value>
        public ValueCompare ParentClientDocumentGroupIdCompare { get; set; }

        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public override Func<ClientDocumentGroupObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    && this.Match(this.ParentClientDocumentGroupId, entity.ParentClientDocumentGroupId, this.ParentClientDocumentGroupIdCompare)
                    && this.Match(this.Name, entity.Name, this.NameCompare);
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
            where TCopy : ClientDocumentGroupSearchDetail, new()
        {
            TCopy copy = base.CopyAs<TCopy>();
            copy.ParentClientDocumentGroupId = this.ParentClientDocumentGroupId;
            copy.ParentClientDocumentGroupIdCompare = this.ParentClientDocumentGroupIdCompare;
            return copy;
        }

        #endregion

        /// <summary>
        /// Instantiates a new entity and copies the properties from this entity to the new one.
        /// </summary>
        /// <typeparam name="TCopy">Type of entity to create.</typeparam>
        /// <returns>A new entity with the search details copied.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        TCopy ISearchDetailCopyAs<ClientDocumentGroupSearchDetail>.CopyAs<TCopy>()
        {
            throw new NotImplementedException();
        }

    }
}
