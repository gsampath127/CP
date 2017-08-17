using RRD.DSA.Core.DAL;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Interfaces
{
    #region ISearchDetail<TModel>

    /// <summary>
    /// Defines a search detail entity.
    /// Can be used for building a list of search parameters for the GetEntitiesBySearch method of <see cref="I:IFactory" />.
    /// Can also be used to search a collection of entities directly.
    /// </summary>
    /// <typeparam name="TModel">The type of the t model.</typeparam>
    public interface ISearchDetail<TModel>
        : ISearchDetail
        where TModel : IModel
    {
        #region Properties

        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        new Func<TModel, bool> SearchPredicate { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Searches a collection of entities using the search parameters set in this search detail instance.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to search.</typeparam>
        /// <param name="source">Source collection to search.</param>
        /// <returns>A collection of entities that match the search criteria.</returns>
        new IEnumerable<TEntity> Search<TEntity>(IEnumerable<TEntity> source)
            where TEntity : TModel;

        #endregion
    }

    #endregion

    #region ISearchDetail

    /// <summary>
    /// Interface ISearchDetail
    /// </summary>
    public interface ISearchDetail
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the entity or entities being searched for.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }

        /// <summary>
        /// Determines the type of comparison for the Name property.
        /// </summary>
        /// <value>The name compare.</value>
        TextCompare NameCompare { get; set; }

        /// <summary>
        /// Gets or sets the description of the entity or entities being searched for..
        /// </summary>
        /// <value>The description.</value>
        string Description { get; set; }

        /// <summary>
        /// Determines the type of comparison for the Description property.
        /// </summary>
        /// <value>The description compare.</value>
        TextCompare DescriptionCompare { get; set; }

        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        Func<IModel, bool> SearchPredicate { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Returns an enumeration of DbParameter entities based on values set in th underlying search detail entity.
        /// </summary>
        /// <param name="dataAccess">Data access instance used to create the parameters.</param>
        /// <returns>A collection of DbParameter entities.</returns>
        IEnumerable<DbParameter> GetSearchParameters(IDataAccess dataAccess);

        /// <summary>
        /// Searches a collection of entities using the search parameters set in this search detail instance.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to search.</typeparam>
        /// <param name="source">Source collection to search.</param>
        /// <returns>A collection of entities that match the search criteria.</returns>
        IEnumerable<TEntity> Search<TEntity>(IEnumerable<TEntity> source)
            where TEntity : IModel;

        #endregion
    }

#endregion
}
