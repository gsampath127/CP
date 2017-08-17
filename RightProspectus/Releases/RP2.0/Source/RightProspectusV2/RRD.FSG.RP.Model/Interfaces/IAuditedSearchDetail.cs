// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace RRD.FSG.RP.Model.Interfaces
{
    /// <summary>
    /// Interface IAuditedSearchDetail
    /// </summary>
    public interface IAuditedSearchDetail
        : ISearchDetail
    {
        #region Search Properties

        /// <summary>
        /// Gets the last modified date of the entities being searched. Must be UTC.
        /// </summary>
        /// <value>The modified by.</value>
        int? ModifiedBy { get; set; }

        /// <summary>
        /// Determines the type of comparison for the ModifiedBy property.
        /// </summary>
        /// <value>The modified by compare.</value>
        ValueCompare ModifiedByCompare { get; set; }

        /// <summary>
        /// Gets the identifier of the user who last modified the entities being searched.
        /// </summary>
        /// <value>The last modified.</value>
        DateTime? LastModified { get; set; }

        /// <summary>
        /// Determines the type of comparison for the LastModified property.
        /// </summary>
        /// <value>The last modified compare.</value>
        ValueCompare LastModifiedCompare { get; set; }

        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        new Func<IAuditedModel, bool> SearchPredicate { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Searches a collection of entities using the search parameters set in this search detail instance.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to search.</typeparam>
        /// <param name="source">Source collection to search.</param>
        /// <returns>A collection of entities that match the search criteria.</returns>
        new IEnumerable<TEntity> Search<TEntity>(IEnumerable<TEntity> source)
            where TEntity : IAuditedModel;

        #endregion
    }
}
