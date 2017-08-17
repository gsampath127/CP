// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-03-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace RRD.FSG.RP.Model.Interfaces
{
    /// <summary>
    /// Base factory interface for entity models.
    /// </summary>
    /// <typeparam name="TModel">Type of entity the factory handles.</typeparam>
    /// <typeparam name="TKey">type of primary key identifier for the entities handled by the factory.</typeparam>
    public interface IFactory<TModel, TKey>
        where TModel : IModel<TKey>, new()
    {
        #region Properties


        /// <summary>
        /// Gets the data access instance used to interface between factory and persisted storage of entity data.
        /// </summary>
        /// <value>The data access.</value>
        IDataAccess DataAccess { get; }

        /// <summary>
        /// Gets the connection string for the factory's underyling data access.
        /// </summary>
        /// <value>The connection string.</value>
        string ConnectionString { get; }

        /// <summary>
        /// Gets the vertical market connection string for the factory's underyling data access.
        /// </summary>
        /// <value>The vertical market connection string.</value>
        string VerticalMarketConnectionString { get; }

        /// <summary>
        /// Gets and Sets the Client Name.
        /// </summary>
        /// <value>The name of the client.</value>
        string ClientName { set; get; }


        #endregion

        #region Methods

        #region Read Methods

        #region CreateEntity

        /// <summary>
        /// Instantiates a new entity and populates members with the data record passed in.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to create.</typeparam>
        /// <param name="entityRecord">Data record used to create the entity.</param>
        /// <returns>A new entity of type <typeparamref name="TEntity" />.</returns>
        TEntity CreateEntity<TEntity>(IDataRecord entityRecord)
            where TEntity : TModel, new();

        /// <summary>
        /// Instantiates a new entity and populates members with the data record passed in.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to create.</typeparam>
        /// <param name="entityRow">Data row used to create the entity.</param>
        /// <returns>A new entity of type <typeparamref name="TEntity" />.</returns>
        TEntity CreateEntity<TEntity>(DataRow entityRow)
            where TEntity : TModel, new();

        #endregion

        #region CreateEntities

        /// <summary>
        /// Creates a collection of entities populated by the data from the table passed in.
        /// </summary>
        /// <param name="entityTable">Table containing the entity data.</param>
        /// <returns>A collection of <see cref="TModel" /> entities.</returns>
        IEnumerable<TModel> CreateEntities(DataTable entityTable);

        /// <summary>
        /// Creates a collection of entities populated by the data from the table passed in.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to populate the collection with.</typeparam>
        /// <param name="entityTable">Table containing the entity data.</param>
        /// <returns>A collection of <see cref="TEntity" /> entities.</returns>
        IEnumerable<TEntity> CreateEntities<TEntity>(DataTable entityTable)
            where TEntity : TModel, new();

        /// <summary>
        /// Creates a collection of entities populated by the data from the table passed in.
        /// </summary>
        /// <param name="entityReader">IDataReader containing the entity data.</param>
        /// <returns>A collection of <see cref="TModel" /> entities.</returns>
        IEnumerable<TModel> CreateEntities(IDataReader entityReader);

        /// <summary>
        /// Creates a collection of entities populated by the data from the table passed in.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to populate the collection with.</typeparam>
        /// <param name="entityReader">IDataReader containing the entity data.</param>
        /// <returns>A collection of <see cref="TEntity" /> entities.</returns>
        IEnumerable<TEntity> CreateEntities<TEntity>(IDataReader entityReader)
            where TEntity : TModel, new();

        #endregion

        #region GetEntityByKey

        /// <summary>
        /// Retrieves entity details from the underlying data access and uses them to populate an entity instance.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to retrieve.</typeparam>
        /// <param name="key">Primary key identifier of the entity to retrieve.</param>
        /// <returns>A new instance of type <typeparamref name="TEntity" /> if found. Null otherwise.</returns>
        TEntity GetEntityByKey<TEntity>(TKey key)
            where TEntity : TModel, new();

        /// <summary>
        /// Retrieves entity details from the underlying data access and uses them to populate an entity instance.
        /// </summary>
        /// <param name="key">Primary key identifier of the entity to retrieve.</param>
        /// <returns>A new instance of type <typeparamref name="TModel" /> if found. Null otherwise.</returns>
        TModel GetEntityByKey(TKey key);

        #endregion

        #region GetAllEntities

        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        IEnumerable<TModel> GetAllEntities();

        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <param name="sort">Sort details used for the query (column and order).</param>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        IEnumerable<TModel> GetAllEntities(ISortDetail<TModel> sort);

        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <param name="startRowIndex">First row of the results to return.</param>
        /// <param name="maximumRows">Total number of rows to return.</param>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        IEnumerable<TModel> GetAllEntities(int startRowIndex, int maximumRows);

        /// <summary>
        /// Gets the parameters from entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>IEnumerable&lt;DbParameter&gt;.</returns>
        IEnumerable<DbParameter> GetParametersFromEntity<TEntity>(TEntity entity)
            where TEntity : TModel;
        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <param name="startRowIndex">First row of the results to return.</param>
        /// <param name="maximumRows">Total number of rows to return.</param>
        /// <param name="sort">Sort details used for the query (column and order).</param>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        IEnumerable<TModel> GetAllEntities(int startRowIndex, int maximumRows, ISortDetail<TModel> sort);

        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to retrieve.</typeparam>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        IEnumerable<TEntity> GetAllEntities<TEntity>()
            where TEntity : TModel, new();

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="sort">The sort.</param>
        /// <returns>IEnumerable&lt;TEntity&gt;.</returns>
        IEnumerable<TEntity> GetAllEntities<TEntity>(ISortDetail<TEntity> sort)
            where TEntity : TModel, new();

        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to retrieve.</typeparam>
        /// <param name="startRowIndex">First row of the results to return.</param>
        /// <param name="maximumRows">Total number of rows to return.</param>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        IEnumerable<TEntity> GetAllEntities<TEntity>(int startRowIndex, int maximumRows)
            where TEntity : TModel, new();

        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to retrieve.</typeparam>
        /// <param name="startRowIndex">First row of the results to return.</param>
        /// <param name="maximumRows">Total number of rows to return.</param>
        /// <param name="sort">Sort details used for the query (column and order).</param>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        IEnumerable<TEntity> GetAllEntities<TEntity>(int startRowIndex, int maximumRows, ISortDetail<TEntity> sort)
            where TEntity : TModel, new();

        #endregion

        #region GetEntitiesBySearch

        /// <summary>
        /// Searches the data store for entities using the details set in the search detail instance.
        /// </summary>
        /// <param name="search">Details of what to search for.</param>
        /// <param name="entitiesToIgnore">Optional parameterized list of entities to ignore while searching.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        IEnumerable<TModel> GetEntitiesBySearch(ISearchDetail<TModel> search, params TKey[] entitiesToIgnore);

        /// <summary>
        /// Searches the data store for entities using the details set in the search detail instance.
        /// </summary>
        /// <param name="search">Details of what to search for.</param>
        /// <param name="sort">Sort details used for the query (column and order).</param>
        /// <param name="entitiesToIgnore">Optional parameterized list of entities to ignore while searching.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        IEnumerable<TModel> GetEntitiesBySearch(ISearchDetail<TModel> search, ISortDetail<TModel> sort, params TKey[] entitiesToIgnore);

        /// <summary>
        /// Searches the data store for entities using the details set in the search detail instance.
        /// </summary>
        /// <param name="startRowIndex">First row of the results to return.</param>
        /// <param name="maximumRows">Total number of rows to return.</param>
        /// <param name="search">Details of what to search for.</param>
        /// <param name="entitiesToIgnore">Optional parameterized list of entities to ignore while searching.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        IEnumerable<TModel> GetEntitiesBySearch(int startRowIndex, int maximumRows, ISearchDetail<TModel> search, params TKey[] entitiesToIgnore);

        /// <summary>
        /// Searches the data store for entities using the details set in the search detail instance.
        /// </summary>
        /// <param name="startRowIndex">First row of the results to return.</param>
        /// <param name="maximumRows">Total number of rows to return.</param>
        /// <param name="search">Details of what to search for.</param>
        /// <param name="sort">Sort details used for the query (column and order).</param>
        /// <param name="entitiesToIgnore">Optional parameterized list of entities to ignore while searching.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        IEnumerable<TModel> GetEntitiesBySearch(int startRowIndex, int maximumRows, ISearchDetail<TModel> search, ISortDetail<TModel> sort, params TKey[] entitiesToIgnore);

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
        IEnumerable<TModel> GetEntitiesBySearch(int startRowIndex, int maximumRows, ISearchDetail<TModel> search, ISortDetail<TModel> sort,out int totalRecordCount, params TKey[] entitiesToIgnore);


        /// <summary>
        /// Searches the data store for entities using the details set in the search detail instance.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to search for.</typeparam>
        /// <param name="search">Details of what to search for.</param>
        /// <param name="entitiesToIgnore">Optional parameterized list of entities to ignore while searching.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(ISearchDetail<TEntity> search, params TKey[] entitiesToIgnore)
            where TEntity : TModel, new();

        /// <summary>
        /// Searches the data store for entities using the details set in the search detail instance.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to search for.</typeparam>
        /// <param name="search">Details of what to search for.</param>
        /// <param name="sort">Sort details used for the query (column and order).</param>
        /// <param name="entitiesToIgnore">Optional parameterized list of entities to ignore while searching.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(ISearchDetail<TEntity> search, ISortDetail<TEntity> sort, params TKey[] entitiesToIgnore)
            where TEntity : TModel, new();

        /// <summary>
        /// Searches the data store for entities using the details set in the search detail instance.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to search for.</typeparam>
        /// <param name="startRowIndex">First row of the results to return.</param>
        /// <param name="maximumRows">Total number of rows to return.</param>
        /// <param name="search">Details of what to search for.</param>
        /// <param name="entitiesToIgnore">Optional parameterized list of entities to ignore while searching.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, params TKey[] entitiesToIgnore)
            where TEntity : TModel, new();

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
        IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, ISortDetail<TEntity> sort, params TKey[] entitiesToIgnore)
            where TEntity : TModel, new();

        /// <summary>
        /// Searches the data store for entities using the details set in the search detail instance.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to search for.</typeparam>
        /// <param name="startRowIndex">First row of the results to return.</param>
        /// <param name="maximumRows">Total number of rows to return.</param>
        /// <param name="search">Details of what to search for.</param>
        /// <param name="sort">Sort details used for the query (column and order).</param>
        /// <param name="totalRecordCount">total Record Count.</param>
        /// <param name="entitiesToIgnore">Optional parameterized list of entities to ignore while searching.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, ISortDetail<TEntity> sort,out int totalRecordCount, params TKey[] entitiesToIgnore)
            where TEntity : TModel, new();

        #endregion

        #endregion

        #region Modify Methods


        #region SaveEntity

        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        void SaveEntity(TModel entity);

        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        /// <param name="modifiedBy">Id of the user performing the update.</param>
        void SaveEntity(TModel entity, int modifiedBy);

        #endregion

        #region DeleteEntity

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        void DeleteEntity(TKey key);

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        /// <param name="deletedBy">The deleted by.</param>
        void DeleteEntity(TKey key,int deletedBy);

        /// <summary>
        /// Deletes the entity associated with the object from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void DeleteEntity(TModel entity);

        /// <summary>
        /// Deletes the entity associated with the object from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        void DeleteEntity(TModel entity, int deletedBy);
#endregion
        #endregion

        #endregion
    }
}
