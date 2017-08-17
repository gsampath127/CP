// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model.Cache
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-03-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web.Caching;

namespace RRD.FSG.RP.Model.Cache
{
    /// <summary>
    /// Base factory cache that interfaces the factory class with sql dependency caching schema.
    /// </summary>
    /// <typeparam name="TFactory">Type of factory to interface with.</typeparam>
    /// <typeparam name="TModel">Type of entity the factory creates.</typeparam>
    /// <typeparam name="TKey">Primary key data type of the entity.</typeparam>
    /// <remarks>IFactoryCache implements IFactory.
    /// This means any factory cache solution can be used in place of the original factory class via dependency injection or other means.
    /// Any non cacheable factory methods such as CreateEntity will pass to the underyling factory for processing.</remarks>
    public abstract class BaseFactoryCache<TFactory, TModel, TKey>
        : IFactoryCache<TFactory, TModel, TKey>
        where TFactory : IFactory<TModel, TKey>
        where TModel : IModel<TKey>, new()
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFactoryCache" /> class.
        /// </summary>
        /// <param name="factory">Factory instance for the caching class.</param>
        public BaseFactoryCache(TFactory factory)
        {
            this.Factory = factory;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a reference for the underlying factory.
        /// </summary>
        /// <value>The factory.</value>
        public virtual TFactory Factory { get; internal set; }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>The mode.</value>
        public FactoryCacheMode Mode { get; set; }

        /// <summary>
        /// Gets the connection string for the factory's underlying data access.
        /// </summary>
        /// <value>The connection string.</value>
        public string ConnectionString
        {
            get
            {
                return this.Factory.ConnectionString;
            }
        }

        /// <summary>
        /// Gets the vertical market connection string for the factory's underlying data access.
        /// </summary>
        /// <value>The vertical market connection string.</value>
        public string VerticalMarketConnectionString
        {
            get
            {
                return this.Factory.VerticalMarketConnectionString;
            }
        }


        /// <summary>
        /// Gets and Sets the Client Name.
        /// </summary>
        /// <value>The name of the client.</value>
        public string ClientName
        {
            get
            {
                return this.Factory.ClientName;
            }
            set
            {
                this.Factory.ClientName = value;
            }
        }

        /// <summary>
        /// Gets the data access instance used to interface between factory and persisted storage of entity data.
        /// </summary>
        /// <value>The data access.</value>
        public IDataAccess DataAccess
        {
            get { return this.Factory.DataAccess; }
        }


        /// <summary>
        /// Defines the collection cache key to be used for the dictionary stored in cache that holds all the retrieved items.
        /// </summary>
        /// <value>The collection cache key.</value>
        public abstract string CollectionCacheKey { get; }

        /// <summary>
        /// Defines the stored procedure (parameterless) that is used to build the query notification used by sql cache dependency feature of HttpCache object.
        /// </summary>
        /// <value>The collection dependency check stored procedure.</value>
        public abstract string CollectionDependencyCheckStoredProcedure { get; }

        /// <summary>
        /// Defines the default sliding expiration to be used for the entities returned by the underlying factory.
        /// </summary>
        /// <value>The sliding expiration.</value>
        public abstract TimeSpan SlidingExpiration { get; }

        /// <summary>
        /// Defines the cache priority of the collection. The default is Normal.
        /// </summary>
        /// <value>The priority.</value>
        public virtual CacheItemPriority Priority { get { return CacheItemPriority.Normal; } }

        /// <summary>
        /// Gets a read only wrapper for the cached dictionary. Wraps a call to the <see cref="DependencyHelper" /> class to retrieve the dictionary from cache.
        /// </summary>
        /// <value>The collection cache.</value>
        public virtual IDictionary<TKey, TModel> CollectionCache
        {
            get { return new ReadOnlyDictionary<TKey, TModel>(this.CollectionCacheInternal); }
        }

        #endregion



        #region Protected Internal Properties

        /// <summary>
        /// Internal property for the cached dictionary. Wraps a call to the <see cref="DependencyHelper" /> class to retrieve the dictionary from cache.
        /// </summary>
        /// <value>The collection cache internal.</value>
        /// <exception cref="System.InvalidOperationException"></exception>
        protected internal virtual ConcurrentDictionary<TKey, TModel> CollectionCacheInternal
        {
            get
            {
                bool returnedFromCache = false;
                CachedCollection collection = DependencyHelper.GetCachedDataWithSqlDependency<CachedCollection>(
                    this.CollectionCacheKey,
                    this.ConnectionString,
                    this.CollectionDependencyCheckStoredProcedure,
                    this.SlidingExpiration,
                    this.Priority,
                     () => new CachedCollection { Mode = this.Mode, Dictionary = new ConcurrentDictionary<TKey, TModel>() },
                    out returnedFromCache);

                // First verify the cached collection is using the same caching mode.
                if (returnedFromCache
                    && collection.Mode != this.Mode)
                {
                    throw new InvalidOperationException(string.Format("This factory is configured for caching mode {0} but there is already a cache collection using mode {1}", this.Mode, collection.Mode));
                }

                return collection.Dictionary;
            }
        }

        #endregion

        #region Public Methods

        #region Factory Wrapper Methods

        #region Read Methods

        #region CreateEntity

        /// <summary>
        /// Wrapper for the underyling Factory.CreateEntity method.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to create.</typeparam>
        /// <param name="entityRecord">Data record used to create the entity.</param>
        /// <returns>A new entity of type <typeparamref name="TEntity" />.</returns>
        public TEntity CreateEntity<TEntity>(IDataRecord entityRecord)
            where TEntity : TModel, new()
        {
            return this.Factory.CreateEntity<TEntity>(entityRecord);
        }

        /// <summary>
        /// Wrapper for the underlying Factory.CreateEntity method.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to create.</typeparam>
        /// <param name="entityRow">Data row used to create the entity.</param>
        /// <returns>A new entity of type <typeparamref name="TEntity" />.</returns>
        public TEntity CreateEntity<TEntity>(DataRow entityRow)
            where TEntity : TModel, new()
        {
            return this.Factory.CreateEntity<TEntity>(entityRow);
        }

        #endregion

        #region CreateEntities

        /// <summary>
        /// Creates a collection of entities populated by the data from the table passed in.
        /// </summary>
        /// <param name="entityTable">Table containing the entity data.</param>
        /// <returns>A collection of <see cref="TModel" /> entities.</returns>
        public IEnumerable<TModel> CreateEntities(DataTable entityTable)
        {
            return this.Factory.CreateEntities(entityTable);
        }

        /// <summary>
        /// Creates a collection of entities populated by the data from the table passed in.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to populate the collection with.</typeparam>
        /// <param name="entityTable">Table containing the entity data.</param>
        /// <returns>A collection of <see cref="TEntity" /> entities.</returns>
        public IEnumerable<TEntity> CreateEntities<TEntity>(DataTable entityTable)
            where TEntity : TModel, new()
        {
            return this.Factory.CreateEntities<TEntity>(entityTable);
        }

        /// <summary>
        /// Creates a collection of entities populated by the data from the table passed in.
        /// </summary>
        /// <param name="entityReader">IDataReader containing the entity data.</param>
        /// <returns>A collection of <see cref="TModel" /> entities.</returns>
        public IEnumerable<TModel> CreateEntities(IDataReader entityReader)
        {
            return this.Factory.CreateEntities(entityReader);
        }

        /// <summary>
        /// Creates a collection of entities populated by the data from the table passed in.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to populate the collection with.</typeparam>
        /// <param name="entityReader">IDataReader containing the entity data.</param>
        /// <returns>A collection of <see cref="TEntity" /> entities.</returns>
        public IEnumerable<TEntity> CreateEntities<TEntity>(IDataReader entityReader)
            where TEntity : TModel, new()
        {
            return this.Factory.CreateEntities<TEntity>(entityReader);
        }

        #endregion

        #endregion

        #region Modify Methods

        /// <summary>
        /// Wrapper for the underlying Factory.SaveEntity method.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        public void SaveEntity(TModel entity)
        {
            Factory.SaveEntity(entity);
        }

        /// <summary>
        /// Wrapper for the underlying Factory.SaveEntity method.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        /// <param name="modifiedBy">The modified by.</param>
        public void SaveEntity(TModel entity, int modifiedBy)
        {
            Factory.SaveEntity(entity, modifiedBy);
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        public void DeleteEntity(TKey key)
        {
            Factory.DeleteEntity(key);
        }


        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        /// <param name="modifiedBy">The modified by.</param>
        public void DeleteEntity(TKey key, int modifiedBy)
        {
            Factory.DeleteEntity(key, modifiedBy);
        }
        /// <summary>
        /// Deletes the entity associated with the entity from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void DeleteEntity(TModel entity)
        {
            Factory.DeleteEntity(entity);
        }
        /// <summary>
        /// Deletes the entity associated with the entity from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        public void DeleteEntity(TModel entity, int deletedBy)
        {
            Factory.DeleteEntity(entity, deletedBy);
        }

        #endregion

        #endregion

        #region Cached Factory Methods

        #region GetEntityByKey

        /// <summary>
        /// Retrieves entity details from the underlying data access and uses them to populate an entity instance.
        /// </summary>
        /// <param name="key">Primary key identifier of the entity to retrieve.</param>
        /// <returns>A new instance of type <typeparamref name="TModel" /> if found. Null otherwise.</returns>
        public TModel GetEntityByKey(TKey key)
        {
            return this.GetEntityByKeyInternal<TModel>(key, this.Factory.GetEntityByKey);
        }

        /// <summary>
        /// Attempts to retrieve an entity from the collection cache by the Key. If unavailable, attempts to retrieve the item from the underyling factory, storing a reference in the colleciton.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to retrieve.</typeparam>
        /// <param name="key">Primary key identifier of the entity to retrieve.</param>
        /// <returns>A new instance of type <typeparamref name="TEntity" /> if found. Null otherwise.</returns>
        public TEntity GetEntityByKey<TEntity>(TKey key)
            where TEntity : TModel, new()
        {
            return this.GetEntityByKeyInternal<TEntity>(key, this.Factory.GetEntityByKey<TEntity>);
        }

        #endregion

        #region GetAllEntities

        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <returns>A collection of all the entities from the data store.</returns>
        public IEnumerable<TModel> GetAllEntities()
        {
            return this.GetAllEntities(null);
        }

        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <param name="sort">Sort details used for the query (column and order).</param>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        public IEnumerable<TModel> GetAllEntities(ISortDetail<TModel> sort)
        {
            return this.GetAllEntities(0, 0, sort);
        }

        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <param name="startRowIndex">First row of the results to return.</param>
        /// <param name="maximumRows">Total number of rows to return.</param>
        /// <returns>A collection of all the entities from the data store.</returns>
        public IEnumerable<TModel> GetAllEntities(int startRowIndex, int maximumRows)
        {
            return this.GetAllEntities(startRowIndex, maximumRows, null);
        }

        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <param name="startRowIndex">First row of the results to return.</param>
        /// <param name="maximumRows">Total number of rows to return.</param>
        /// <param name="sort">Sort details used for the query (column and order).</param>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        public IEnumerable<TModel> GetAllEntities(int startRowIndex, int maximumRows, ISortDetail<TModel> sort)
        {
            return this.GetAllEntities<TModel>(startRowIndex, maximumRows, sort);
        }

        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to retrieve.</typeparam>
        /// <returns>A collection of all the entities from the data store.</returns>
        public IEnumerable<TEntity> GetAllEntities<TEntity>()
            where TEntity : TModel, new()
        {
            return this.GetAllEntities<TEntity>(null);
        }

        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to retrieve.</typeparam>
        /// <param name="sort">Sort details used for the query (column and order).</param>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        public IEnumerable<TEntity> GetAllEntities<TEntity>(ISortDetail<TEntity> sort)
            where TEntity : TModel, new()
        {
            return this.GetAllEntities<TEntity>(0, 0, null);
        }

        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to retrieve.</typeparam>
        /// <param name="startRowIndex">First row of the results to return.</param>
        /// <param name="maximumRows">Total number of rows to return.</param>
        /// <returns>A collection of all the entities from the data store.</returns>
        public IEnumerable<TEntity> GetAllEntities<TEntity>(int startRowIndex, int maximumRows)
            where TEntity : TModel, new()
        {
            return this.GetAllEntities<TEntity>(startRowIndex, maximumRows, null);
        }

        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to retrieve.</typeparam>
        /// <param name="startRowIndex">First row of the results to return.</param>
        /// <param name="maximumRows">Total number of rows to return.</param>
        /// <param name="sort">Sort details used for the query (column and order).</param>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        public IEnumerable<TEntity> GetAllEntities<TEntity>(int startRowIndex, int maximumRows, ISortDetail<TEntity> sort)
            where TEntity : TModel, new()
        {
            return this.GetAllEntitiesInternal<TEntity>(startRowIndex, maximumRows, sort, this.Factory.GetAllEntities<TEntity>);
        }

        #endregion

        #region GetEntitiesBySearch

        /// <summary>
        /// Searches the data store for entities using the details set in the search detail instance.
        /// </summary>
        /// <param name="search">Details of what to search for.</param>
        /// <param name="entitiesToIgnore">Optional parameterized list of entities to ignore while searching.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        public IEnumerable<TModel> GetEntitiesBySearch(ISearchDetail<TModel> search, params TKey[] entitiesToIgnore)
        {
            return this.GetEntitiesBySearch(search, null, entitiesToIgnore);
        }

        /// <summary>
        /// Searches the data store for entities using the details set in the search detail instance.
        /// </summary>
        /// <param name="search">Details of what to search for.</param>
        /// <param name="sort">Sort details used for the query (column and order).</param>
        /// <param name="entitiesToIgnore">Optional parameterized list of entities to ignore while searching.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        public IEnumerable<TModel> GetEntitiesBySearch(ISearchDetail<TModel> search, ISortDetail<TModel> sort, params TKey[] entitiesToIgnore)
        {
            return this.GetEntitiesBySearch(0, 0, search, sort, entitiesToIgnore);
        }

        /// <summary>
        /// Searches the data store for entities using the details set in the search detail instance.
        /// </summary>
        /// <param name="startRowIndex">First row of the results to return.</param>
        /// <param name="maximumRows">Total number of rows to return.</param>
        /// <param name="search">Details of what to search for.</param>
        /// <param name="entitiesToIgnore">Optional parameterized list of entities to ignore while searching.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        public IEnumerable<TModel> GetEntitiesBySearch(int startRowIndex, int maximumRows, ISearchDetail<TModel> search, params TKey[] entitiesToIgnore)
        {
            return this.GetEntitiesBySearch(startRowIndex, maximumRows, search, null, entitiesToIgnore);
        }

        /// <summary>
        /// Searches the data store for entities using the details set in the search detail instance.
        /// </summary>
        /// <param name="startRowIndex">First row of the results to return.</param>
        /// <param name="maximumRows">Total number of rows to return.</param>
        /// <param name="search">Details of what to search for.</param>
        /// <param name="sort">Sort details used for the query (column and order).</param>
        /// <param name="entitiesToIgnore">Optional parameterized list of entities to ignore while searching.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        public IEnumerable<TModel> GetEntitiesBySearch(int startRowIndex, int maximumRows, ISearchDetail<TModel> search, ISortDetail<TModel> sort, params TKey[] entitiesToIgnore)
        {
            return this.GetEntitiesBySearchInternal<TModel>(startRowIndex, maximumRows, search, sort, entitiesToIgnore, this.Factory.GetEntitiesBySearch);
        }

        /// <summary>
        /// Searches the data store for entities using the details set in the search detail instance.
        /// </summary>
        /// <param name="startRowIndex">First row of the results to return.</param>
        /// <param name="maximumRows">Total number of rows to return.</param>
        /// <param name="search">Details of what to search for.</param>
        /// <param name="sort">Sort details used for the query (column and order).</param>
        /// <param name="totalRecordCount">The total record count.</param>
        /// <param name="entitiesToIgnore">Optional parameterized list of entities to ignore while searching.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<TModel> GetEntitiesBySearch(int startRowIndex, int maximumRows, ISearchDetail<TModel> search, ISortDetail<TModel> sort,out int totalRecordCount, params TKey[] entitiesToIgnore)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Searches the data store for entities using the details set in the search detail instance.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to search for.</typeparam>
        /// <param name="search">Details of what to search for.</param>
        /// <param name="entitiesToIgnore">Optional parameterized list of entities to ignore while searching.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        public IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(ISearchDetail<TEntity> search, params TKey[] entitiesToIgnore)
            where TEntity : TModel, new()
        {
            return this.GetEntitiesBySearch<TEntity>(search, null, entitiesToIgnore);
        }

        /// <summary>
        /// Searches the data store for entities using the details set in the search detail instance.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to search for.</typeparam>
        /// <param name="search">Details of what to search for.</param>
        /// <param name="sort">Sort details used for the query (column and order).</param>
        /// <param name="entitiesToIgnore">Optional parameterized list of entities to ignore while searching.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        public IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(ISearchDetail<TEntity> search, ISortDetail<TEntity> sort, params TKey[] entitiesToIgnore) where TEntity : TModel, new()
        {
            return this.GetEntitiesBySearch<TEntity>(0, 0, search, sort, entitiesToIgnore);
        }

        /// <summary>
        /// Searches the data store for entities using the details set in the search detail instance.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to search for.</typeparam>
        /// <param name="startRowIndex">First row of the results to return.</param>
        /// <param name="maximumRows">Total number of rows to return.</param>
        /// <param name="search">Details of what to search for.</param>
        /// <param name="entitiesToIgnore">Optional parameterized list of entities to ignore while searching.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        public IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, params TKey[] entitiesToIgnore)
            where TEntity : TModel, new()
        {
            return this.GetEntitiesBySearch<TEntity>(startRowIndex, maximumRows, search, null, entitiesToIgnore);
        }

        /// <summary>
        /// Searches the data store for entities using the details set in the search detail instance.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to search for.</typeparam>
        /// <param name="startRowIndex">First row of the results to return.</param>
        /// <param name="maximumRows">Total number of rows to return.</param>
        /// <param name="search">Details of what to search for.</param>
        /// <param name="sort">Sort details used for the query (column and order).</param>
        /// <param name="entitiesToIgnore">Optional parameterized list of entities to ignore while searching.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        public IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, ISortDetail<TEntity> sort, params TKey[] entitiesToIgnore)
            where TEntity : TModel, new()
        {
            return this.GetEntitiesBySearchInternal<TEntity>(startRowIndex, maximumRows, search, sort, entitiesToIgnore, this.Factory.GetEntitiesBySearch<TEntity>);
        }

        /// <summary>
        /// Searches the data store for entities using the details set in the search detail instance.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to search for.</typeparam>
        /// <param name="startRowIndex">First row of the results to return.</param>
        /// <param name="maximumRows">Total number of rows to return.</param>
        /// <param name="search">Details of what to search for.</param>
        /// <param name="sort">Sort details used for the query (column and order).</param>
        /// <param name="totalRecordCount">total Record Count</param>
        /// <param name="entitiesToIgnore">Optional parameterized list of entities to ignore while searching.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, ISortDetail<TEntity> sort,out int totalRecordCount, params TKey[] entitiesToIgnore)
            where TEntity : TModel, new()
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region Additional Caching Methods

        #region CacheEntities

        /// <summary>
        /// Allows a series of entities retrieved from an external data source to be cached. Only allowed when the cache mode is set to FactoryCacheMode.All.
        /// Good when loading groups of disparate entity data with a single fetch from the source.
        /// </summary>
        /// <param name="entityTable">DataTable containing the entity records to be cached.</param>
        /// <returns>Total count of entities added (excluding duplicates ignored).</returns>
        /// <remarks>Duplicate entries are ignored. An item is considered duplicate if it shares the same key as a cached entity.</remarks>
        public int CacheEntities(DataTable entityTable)
        {
            return this.CacheEntities<TModel>(entityTable);
        }

        /// <summary>
        /// Allows a series of entities retrieved from an external data source to be cached. Only allowed when the cache mode is set to FactoryCacheMode.All.
        /// Good when loading groups of disparate entity data with a single fetch from the source.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to cache. Good when there is specialization of various types a single factory can support.</typeparam>
        /// <param name="entityTable">DataTable containing the entity records to be cached.</param>
        /// <returns>Total count of entities added (excluding duplicates ignored).</returns>
        /// <remarks>Duplicate entries are ignored. An item is considered duplicate if it shares the same key as a cached entity.</remarks>
        public int CacheEntities<TEntity>(DataTable entityTable)
            where TEntity : TModel, new()
        {
            this.VerifyMode(FactoryCacheMode.All);
            return this.CacheEntities<TEntity>(this.Factory.CreateEntities<TEntity>(entityTable));
        }

        /// <summary>
        /// Allows a series of entities retrieved from an external data source to be cached. Only allowed when the cache mode is set to FactoryCacheMode.All.
        /// Good when loading groups of disparate entity data with a single fetch from the source.
        /// </summary>
        /// <param name="entityReader">IDataReader containing the entity records to be cached.</param>
        /// <returns>Total count of entities added (excluding duplicates ignored).</returns>
        /// <remarks>Duplicate entries are ignored. An item is considered duplicate if it shares the same key as a cached entity.</remarks>
        public int CacheEntities(IDataReader entityReader)
        {
            return this.CacheEntities<TModel>(entityReader);
        }

        /// <summary>
        /// Allows a series of entities retrieved from an external data source to be cached. Only allowed when the cache mode is set to FactoryCacheMode.All.
        /// Good when loading groups of disparate entity data with a single fetch from the source.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to cache. Good when there is specialization of various types a single factory can support.</typeparam>
        /// <param name="entityReader">IDataReader containing the entity records to be cached.</param>
        /// <returns>Total count of entities added (excluding duplicates ignored).</returns>
        /// <remarks>Duplicate entries are ignored. An item is considered duplicate if it shares the same key as a cached entity.</remarks>
        public int CacheEntities<TEntity>(IDataReader entityReader)
            where TEntity : TModel, new()
        {
            this.VerifyMode(FactoryCacheMode.All);
            return this.CacheEntities<TEntity>(this.Factory.CreateEntities<TEntity>(entityReader));
        }

        /// <summary>
        /// Allows a series of entities retrieved from an external data source to be cached. Only allowed when the cache mode is set to FactoryCacheMode.All.
        /// Good when loading groups of disparate entity data with a single fetch from the source.
        /// </summary>
        /// <param name="entities">Collection containing the entity records to be cached.</param>
        /// <returns>Total count of entities added (excluding duplicates ignored).</returns>
        /// <remarks>Duplicate entries are ignored. An item is considered duplicate if it shares the same key as a cached entity.</remarks>
        public int CacheEntities(IEnumerable<TModel> entities)
        {
            return this.CacheEntities<TModel>(entities);
        }

        /// <summary>
        /// Allows a series of entities retrieved from an external data source to be cached. Only allowed when the cache mode is set to FactoryCacheMode.All.
        /// Good when loading groups of disparate entity data with a single fetch from the source.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to cache. Good when there is specialization of various types a single factory can support.</typeparam>
        /// <param name="entities">Collection containing the entity records to be cached.</param>
        /// <returns>Total count of entities added (excluding duplicates ignored).</returns>
        /// <remarks>Duplicate entries are ignored. An item is considered duplicate if it shares the same key as a cached entity.</remarks>
        public int CacheEntities<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : TModel, new()
        {
            this.VerifyMode(FactoryCacheMode.All);
            ConcurrentDictionary<TKey, TModel> dictionary = this.CollectionCacheInternal;
            int count = 0;
            foreach (TEntity entity in entities)
            {
                if (!dictionary.ContainsKey(entity.Key))
                {
                    dictionary.AddOrUpdate(entity.Key, entity, (key, cachedEntity) => entity);
                    count++;
                }
            }

            return count;
        }

        #endregion

        #endregion

        #endregion

        #region Protected Internal Methods

        /// <summary>
        /// Searches the data store for entities using the details set in the search detail instance.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to search for.</typeparam>
        /// <param name="startRowIndex">First row of the results to return.</param>
        /// <param name="maximumRows">Total number of rows to return.</param>
        /// <param name="search">Details of what to search for.</param>
        /// <param name="sort">Sort details used for the query (column and order).</param>
        /// <param name="entitiesToIgnore">List of entities to ignore while searching.</param>
        /// <param name="searchFunction">Delegate function to execute when retrieving entities from the underlying Factory.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        protected internal IEnumerable<TEntity> GetEntitiesBySearchInternal<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, ISortDetail<TEntity> sort, TKey[] entitiesToIgnore, Func<ISearchDetail<TEntity>, TKey[], IEnumerable<TEntity>> searchFunction)
        where TEntity : TModel, new()
        {
            ConcurrentDictionary<TKey, TModel> dictionary = this.CollectionCacheInternal;
            if (dictionary.Count == 0
                && this.Mode == FactoryCacheMode.All)
            {
                this.GetAllEntities();
            }

            IEnumerable<TEntity> cachedFound = from entity in search.Search(dictionary.Values.OfType<TEntity>())
                                               where !entitiesToIgnore.Contains(entity.Key)
                                               select entity;

            if (this.Mode == FactoryCacheMode.Granular)
            {
                IEnumerable<TKey> foundIds = (from entity in cachedFound
                                              select entity.Key).Union(entitiesToIgnore);
                IEnumerable<TEntity> newFound = from entity in searchFunction(search, foundIds.ToArray())
                                                where !entitiesToIgnore.Contains(entity.Key)
                                                select entity;
                foreach (TEntity entity in newFound)
                {
                    if (!dictionary.ContainsKey(entity.Key))
                    {
                        dictionary.AddOrUpdate(entity.Key, entity, (key, cachedEntity) => entity);
                    }
                }

                return this.SortSkipTake(cachedFound.Concat(newFound), sort, startRowIndex, maximumRows);
            }

            return this.SortSkipTake(cachedFound, sort, startRowIndex, maximumRows);
        }

        /// <summary>
        /// Gets the entity by key internal.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="getEntityFunction">The get entity function.</param>
        /// <returns>TEntity.</returns>
        protected internal TEntity GetEntityByKeyInternal<TEntity>(TKey key, Func<TKey, TEntity> getEntityFunction)
            where TEntity : TModel, new()
        {
            TEntity entity = default(TEntity);

            // We get a reference to the current collection cache and use that for the entire operation.
            // This ensures that we are using the same instance in the scenario tha the cache is invalided mid operation and CollecitonCache changes to a new reference.
            ConcurrentDictionary<TKey, TModel> dictionary = this.CollectionCacheInternal;

            // If cache mode is All and the cache is empty, fetch all entities into cache.
            if (this.Mode == FactoryCacheMode.All
                && dictionary.Count == 0)
            {
                this.GetAllEntities();
            }

            // Attempt to locate the entity from the cached dictionary.
            if (dictionary.ContainsKey(key))
            {
                entity = (TEntity)dictionary[key];
            }

            // If cache mode is Granular and the entity is not found, attempt to retrieve and add the entity to cache.
            if (object.Equals(entity, default(TModel))
                && this.Mode == FactoryCacheMode.Granular)
            {
                entity = getEntityFunction(key);
                if (!object.Equals(entity, default(TModel)))
                {
                    if (!dictionary.ContainsKey(key))
                    {
                        dictionary.AddOrUpdate(key, entity, (cachedKey, cachedEntity) => entity);
                    }
                }
            }

            return entity;
        }

        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to retrieve.</typeparam>
        /// <param name="startRowIndex">First row of the results to return.</param>
        /// <param name="maximumRows">Total number of rows to return.</param>
        /// <param name="sort">Sort details used for the query (column and order).</param>
        /// <param name="getEntitiesFunction">Delegate function to execute when retrieving entities from the underlying Factory.</param>
        /// <returns>A collection of all the entities from the data store.</returns>
        protected internal IEnumerable<TEntity> GetAllEntitiesInternal<TEntity>(int startRowIndex, int maximumRows, ISortDetail<TEntity> sort, Func<IEnumerable<TEntity>> getEntitiesFunction)
            where TEntity : TModel, new()
        {
            this.VerifyMode(FactoryCacheMode.All);
            ConcurrentDictionary<TKey, TModel> dictionary = this.CollectionCacheInternal;

            if (dictionary.Values.OfType<TEntity>().Count() == 0)
            {
                foreach (TEntity entity in getEntitiesFunction())
                {
                    dictionary.AddOrUpdate(entity.Key, entity, (key, cachedEntity) => entity);
                }
            }

            return this.SortSkipTake(dictionary.Values.OfType<TEntity>(), sort, startRowIndex, maximumRows);
        }

        /// <summary>
        /// Gets the parameters from entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>IEnumerable&lt;DbParameter&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<DbParameter> GetParametersFromEntity<TEntity>(TEntity entity) where TEntity : TModel
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Verifies the cache mode passed in matches the factory. Throws an error upon mismatch.
        /// </summary>
        /// <param name="allowedMode">Cache mode to compare.</param>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="InvalidOperationException">Thrown if there is a mismatch.</exception>
        private void VerifyMode(FactoryCacheMode allowedMode)
        {
            if (this.Mode != allowedMode)
            {
                throw new InvalidOperationException(string.Format("This operation is not allowed for the current cache mode of {0}. Cache mode expected is {1}.", this.Mode, allowedMode));
            }
        }

        /// <summary>
        /// Skips and takes a number of items for paging purposes.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity in the collection.</typeparam>
        /// <param name="entities">Source entities to skip and take from.</param>
        /// <param name="sort">The sort.</param>
        /// <param name="startRowIndex">First row of the results to return.</param>
        /// <param name="maximumRows">Total number of rows to return.</param>
        /// <returns>IEnumerable&lt;TEntity&gt;.</returns>
        private IEnumerable<TEntity> SortSkipTake<TEntity>(IEnumerable<TEntity> entities, ISortDetail<TEntity> sort, int startRowIndex, int maximumRows)
            where TEntity : TModel, new()
        {
            IEnumerable<TEntity> sorted = sort == null
                ? entities
                : sort.Sort(entities);
            int count = sorted.Count();

            if (startRowIndex < 0)
            {
                startRowIndex = 0;
            }

            if (maximumRows <= 0)
            {
                maximumRows = 0;
            }

            if (startRowIndex > 0)
            {
                if (maximumRows > 0 && maximumRows < count - startRowIndex)
                {
                    return sorted.Skip(startRowIndex).Take(maximumRows);
                }
                else
                {
                    return sorted.Skip(startRowIndex);
                }
            }
            else if (maximumRows > 0 && maximumRows < count)
            {
                return sorted.Take(maximumRows);
            }

            return sorted;
        }

        #endregion

        #region Classes

        /// <summary>
        /// Internal class to hold both the concurrent dictionary as well as cache mode used at the time of creation.
        /// </summary>
        /// <remarks>The FactoryCacheMode is used to help verify any single application domain is using only one cache mode for a specific factory cache implementation.</remarks>
        internal class CachedCollection
        {
            /// <summary>
            /// Gets or sets the concurrent dictionary holding the cached entities.
            /// </summary>
            /// <value>The dictionary.</value>
            public ConcurrentDictionary<TKey, TModel> Dictionary { get; set; }

            /// <summary>
            /// Gets or sets the cache mode of the Factory that created this collection.
            /// </summary>
            /// <value>The mode.</value>
            public FactoryCacheMode Mode { get; set; }
        }

        #endregion
    }
}
