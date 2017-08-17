// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model.Cache
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Caching;

namespace RRD.FSG.RP.Model.Cache.Interfaces
{
    /// <summary>
    /// Base factory cache interface that interfaces the factory class with sql dependency caching schema.
    /// </summary>
    /// <typeparam name="TFactory">Type of factory to interface with.</typeparam>
    /// <typeparam name="TModel">Type of entity the factory creates.</typeparam>
    /// <typeparam name="TKey">Primary key data type of the entity.</typeparam>
    /// <remarks>IFactoryCache implements IFactory.
    /// This means any factory cache solution can be used in place of the original factory class via dependency injection or other means.
    /// Any non cacheable factory methods such as CreateEntity will pass to the underlying factory for processing.</remarks>
    public interface IFactoryCache<TFactory, TModel, TKey>
        : IFactory<TModel, TKey>
        where TFactory : IFactory<TModel, TKey>
        where TModel : IModel<TKey>, new()
    {
        #region Properties
        /// <summary>
        /// Gets a reference for the underlying factory.
        /// </summary>
        /// <value>The factory.</value>
        TFactory Factory { get; }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>The mode.</value>
        FactoryCacheMode Mode { get; set; }

        /// <summary>
        /// Defines the collection cache key to be used for the dictionary stored in cache that holds all the retrieved items.
        /// </summary>
        /// <value>The collection cache key.</value>
        string CollectionCacheKey { get; }

        /// <summary>
        /// Defines the stored procedure (paramaterless) that is used to build the query notification used by sql cache dependency feature of HttpCache object.
        /// </summary>
        /// <value>The collection dependency check stored procedure.</value>
        string CollectionDependencyCheckStoredProcedure { get; }

        /// <summary>
        /// Property for the cached dictionary. Wraps a call to the <see cref="DependencyHelper" /> class to retrieve the dictionary from cache.
        /// </summary>
        /// <value>The collection cache.</value>
        IDictionary<TKey, TModel> CollectionCache { get; }

        /// <summary>
        /// Defines the default sliding expiration to be used for the entities returned by the underlying factory.
        /// </summary>
        /// <value>The sliding expiration.</value>
        TimeSpan SlidingExpiration { get; }

        /// <summary>
        /// Defines the cache priority of the collection. The default is Normal.
        /// </summary>
        /// <value>The priority.</value>
        CacheItemPriority Priority { get; }

        #endregion

        #region Methods

        #region CacheEntities

        /// <summary>
        /// Allows a series of entities retrieved from an external data source to be cached. Only allowed when the cache mode is set to FactoryCacheMode.All.
        /// Good when loading groups of disparate entity data with a single fetch from the source.
        /// </summary>
        /// <param name="entityTable">DataTable containing the entity records to be cached.</param>
        /// <returns>Total count of entities added (excluding duplicates ignored).</returns>
        /// <remarks>Duplicate entries are ignored. An item is considered duplicate if it shares the same key as a cached entity.</remarks>
        int CacheEntities(DataTable entityTable);

        /// <summary>
        /// Allows a series of entities retrieved from an external data source to be cached. Only allowed when the cache mode is set to FactoryCacheMode.All.
        /// Good when loading groups of disparate entity data with a single fetch from the source.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to cache. Good when there is specialization of various types a single factory can support.</typeparam>
        /// <param name="entityTable">DataTable containing the entity records to be cached.</param>
        /// <returns>Total count of entities added (excluding duplicates ignored).</returns>
        /// <remarks>Duplicate entries are ignored. An item is considered duplicate if it shares the same key as a cached entity.</remarks>
        int CacheEntities<TEntity>(DataTable entityTable)
            where TEntity : TModel, new();

        /// <summary>
        /// Allows a series of entities retrieved from an external data source to be cached. Only allowed when the cache mode is set to FactoryCacheMode.All.
        /// Good when loading groups of disparate entity data with a single fetch from the source.
        /// </summary>
        /// <param name="entityReader">IDataReader containing the entity records to be cached.</param>
        /// <returns>Total count of entities added (excluding duplicates ignored).</returns>
        /// <remarks>Duplicate entries are ignored. An item is considered duplicate if it shares the same key as a cached entity.</remarks>
        int CacheEntities(IDataReader entityReader);

        /// <summary>
        /// Allows a series of entities retrieved from an external data source to be cached. Only allowed when the cache mode is set to FactoryCacheMode.All.
        /// Good when loading groups of disparate entity data with a single fetch from the source.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to cache. Good when there is specialization of various types a single factory can support.</typeparam>
        /// <param name="entityReader">IDataReader containing the entity records to be cached.</param>
        /// <returns>Total count of entities added (excluding duplicates ignored).</returns>
        /// <remarks>Duplicate entries are ignored. An item is considered duplicate if it shares the same key as a cached entity.</remarks>
        int CacheEntities<TEntity>(IDataReader entityReader)
            where TEntity : TModel, new();

        /// <summary>
        /// Allows a series of entities retrieved from an external data source to be cached. Only allowed when the cache mode is set to FactoryCacheMode.All.
        /// Good when loading groups of disparate entity data with a single fetch from the source.
        /// </summary>
        /// <param name="entities">Collection containing the entity records to be cached.</param>
        /// <returns>Total count of entities added (excluding duplicates ignored).</returns>
        /// <remarks>Duplicate entries are ignored. An item is considered duplicate if it shares the same key as a cached entity.</remarks>
        int CacheEntities(IEnumerable<TModel> entities);

        /// <summary>
        /// Allows a series of entities retrieved from an external data source to be cached. Only allowed when the cache mode is set to FactoryCacheMode.All.
        /// Good when loading groups of disparate entity data with a single fetch from the source.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to cache. Good when there is specialization of various types a single factory can support.</typeparam>
        /// <param name="entities">Collection containing the entity records to be cached.</param>
        /// <returns>Total count of entities added (excluding duplicates ignored).</returns>
        /// <remarks>Duplicate entries are ignored. An item is considered duplicate if it shares the same key as a cached entity.</remarks>
        int CacheEntities<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : TModel, new();

        #endregion

        #endregion
    }
}
