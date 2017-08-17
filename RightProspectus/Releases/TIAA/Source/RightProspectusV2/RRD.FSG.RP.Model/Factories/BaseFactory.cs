// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-03-2015
// ***********************************************************************
using Microsoft.SqlServer.Server;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace RRD.FSG.RP.Model.Factories
{
    /// <summary>
    /// Base factory class for entity models.
    /// </summary>
    /// <summary>
    /// Class BaseFactory.
    /// </summary>
    public abstract class BaseFactory
    {
        #region Constants

        /// <summary>
        /// The start row index parameter name
        /// </summary>
        internal const string StartRowIndexParameterName = "StartRowIndex";

        /// <summary>
        /// The maximum row index parameter name
        /// </summary>
        internal const string MaximumRowIndexParameterName = "MaximumRows";

        /// <summary>
        /// The sort column parameter name
        /// </summary>
        internal const string SortColumnParameterName = "SortColumn";

        /// <summary>
        /// The sort order parameter name
        /// </summary>
        internal const string SortOrderParameterName = "SortOrder";

        #endregion
        /// <summary>
        /// The client name
        /// </summary>
        private string clientName;
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFactory"/> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        /// <param name="connectionString">Connection string used to access the persisted storage.</param>
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFactory"/> class.
        /// </summary>
        /// <param name="dataAccess">The data access.</param>
        public BaseFactory(IDataAccess dataAccess)
        {
            DataAccess = dataAccess;  
        }

        #region Public Properties

        /// <summary>
        /// Gets the data access instance used to interface between factory and persisted storage of entity data.
        /// </summary>
        /// <summary>
        /// Gets the data access.
        /// </summary>
        /// <value>The data access.</value>
        public IDataAccess DataAccess { get; internal set; }

        /// <summary>
        /// Gets the connection string for the factory's underyling data access.
        /// </summary>
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        public string ConnectionString { get; internal set; }

        /// <summary>
        /// Gets the Vertical Market connection string for the factory's underyling data access.
        /// </summary>
        /// <summary>
        /// Gets the vertical market connection string.
        /// </summary>
        /// <value>The vertical market connection string.</value>
        public string VerticalMarketConnectionString { get; internal set; }
        #endregion

        /// <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        /// <value>The name of the client.</value>
        public string ClientName
        {
            get
            {
                return clientName;
            }
            set
            {
                this.clientName = value;
                this.SetConnectionString(DBConnectionString.HostedConnectionString(value, DataAccess));

                this.SetVerticalMarketConnectionString(DBConnectionString.VerticalDBConnectionString(value, DataAccess));
            }
        }

        # region Private methods

        /// <summary>
        /// Sets the connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        private void SetConnectionString(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        /// <summary>
        /// Sets the vertical market connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        private void SetVerticalMarketConnectionString(string connectionString)
        {
            this.VerticalMarketConnectionString = connectionString;
        }
        # endregion

        #region Protected Internal Methods


        /// <summary>
        /// Adds the paging and sort.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <param name="sort">The sort.</param>
        /// <returns>List&lt;DbParameter&gt;.</returns>
        protected internal List<DbParameter> AddPagingAndSort(IEnumerable<DbParameter> parameters, int startRowIndex, int maximumRows, ISortDetail sort)
        {
            List<DbParameter> listParameters = new List<DbParameter>(parameters ?? new DbParameter[0]);
            if (startRowIndex > 0 || maximumRows > 0)
            {
                if (startRowIndex > 0)
                {
                    listParameters.Add(this.DataAccess.CreateParameter(StartRowIndexParameterName, DbType.Int32, startRowIndex));
                }

                if (maximumRows > 0)
                {
                    listParameters.Add(this.DataAccess.CreateParameter(MaximumRowIndexParameterName, DbType.Int32, maximumRows));
                }
            }

            if (sort != null
                && sort.Column != SortColumn.Unspecified)
            {
                listParameters.Add(this.DataAccess.CreateParameter(SortColumnParameterName, DbType.Int32, sort.Column));

                if (sort.Order != SortOrder.Unspecified)
                {
                    listParameters.Add(this.DataAccess.CreateParameter(SortOrderParameterName, DbType.Int32, sort.Order));
                }
            }

            return listParameters;
        }

        #endregion
    }

    /// <summary>
    /// Base typed factory class for entity models.
    /// </summary>
    /// <typeparam name="TModel">Type of entity the factory handles.</typeparam>
    /// <typeparam name="TKey">type of primary key identifier for the entities handled by the factory.</typeparam>
    public abstract class BaseFactory<TModel, TKey>
        : BaseFactory, IFactory<TModel, TKey>
        where TModel : IModel<TKey>, new()
    {
        
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public BaseFactory(IDataAccess dataAccess)
            : base(dataAccess)
        { }

        #endregion



        #region Public Methods

        #region Read Methods

        #region CreateEntity

        /// <summary>
        /// Instantiates a new entity and populates members with the data record passed in.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to create.</typeparam>
        /// <param name="entityRecord">Data record used to create the entity.</param>
        /// <returns>A new entity of type <typeparamref name="TEntity" />.</returns>
        public virtual TEntity CreateEntity<TEntity>(IDataRecord entityRecord)
            where TEntity : TModel, new()
        {
            if (entityRecord == null)
                return default(TEntity);
            TEntity entity = new TEntity();
            entity.Name = entityRecord.GetString(entityRecord.GetOrdinal("Name"));
            entity.Description = entityRecord.GetString(entityRecord.GetOrdinal("Description"));
            return entity;
        }

        /// <summary>
        /// Instantiates a new entity and populates members with the data record passed in.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to create.</typeparam>
        /// <param name="entityRow">Data row used to create the entity.</param>
        /// <returns>A new entity of type <typeparamref name="TEntity" />.</returns>
        public virtual TEntity CreateEntity<TEntity>(DataRow entityRow)
            where TEntity : TModel, new()
        {
            if (entityRow == null)
                return default(TEntity);
            TEntity entity = new TEntity();
            return entity;
        }

        #endregion

        #region GetEntityByKey

        /// <summary>
        /// Retrieves entity details from the underlying data access and uses them to populat an entity instance.
        /// </summary>
        /// <param name="key">Primary key identifier of the entity to retrieve.</param>
        /// <returns>A new instance of type <typeparamref name="TModel" /> if found. Null otherwise.</returns>
        public virtual TModel GetEntityByKey(TKey key)
        {
            return this.GetEntityByKey<TModel>(key);
        }

        /// <summary>
        /// Retrieves entity details from the underlying data access and uses them to populate an entity instance.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to retrieve.</typeparam>
        /// <param name="key">Primary key identifier of the entity to retrieve.</param>
        /// <returns>A new instance of type <typeparamref name="TEntity" /> if found. Null otherwise.</returns>
        public abstract TEntity GetEntityByKey<TEntity>(TKey key)
            where TEntity : TModel, new();

        #endregion

        #region CreateEntities

        /// <summary>
        /// Creates a collection of entities populated by the data from the table passed in.
        /// </summary>
        /// <param name="entityTable">Table containing the entity data.</param>
        /// <returns>A collection of <see cref="TModel" /> entities.</returns>
        /// <remarks>This is an iterator method, meaning the results are returned as the enumeration is iterated.</remarks>
        public virtual IEnumerable<TModel> CreateEntities(DataTable entityTable)
        {
            foreach (DataRow row in entityTable.Rows)
            {
                yield return CreateEntity<TModel>(row);
            }

            yield break;
        }

        /// <summary>
        /// Creates a collection of entities populated by the data from the table passed in.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to populate the collection with.</typeparam>
        /// <param name="entityTable">Table containing the entity data.</param>
        /// <returns>A collection of <see cref="TEntity" /> entities.</returns>
        /// <remarks>This is an iterator method, meaning the results are returned as the enumeration is iterated.</remarks>
        public virtual IEnumerable<TEntity> CreateEntities<TEntity>(DataTable entityTable)
            where TEntity : TModel, new()
        {
            foreach (DataRow row in entityTable.Rows)
            {
                yield return CreateEntity<TEntity>(row);
            }

            yield break;
        }

        /// <summary>
        /// Creates a collection of entities populated by the data from the table passed in.
        /// </summary>
        /// <param name="entityReader">IDataReader containing the entity data.</param>
        /// <returns>A collection of <see cref="TModel" /> entities.</returns>
        public virtual IEnumerable<TModel> CreateEntities(IDataReader entityReader)
        {
            return CreateEntities<TModel>(entityReader);
        }

        /// <summary>
        /// Creates a collection of entities populated by the data from the table passed in.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to populate the collection with.</typeparam>
        /// <param name="entityReader">IDataReader containing the entity data.</param>
        /// <returns>A collection of <see cref="TEntity" /> entities.</returns>
        public virtual IEnumerable<TEntity> CreateEntities<TEntity>(IDataReader entityReader)
            where TEntity : TModel, new()
        {
            List<TEntity> entities = new List<TEntity>();
            while (entityReader.Read())
            {
                entities.Add(CreateEntity<TEntity>(entityReader));
            }

            return entities;
        }

        #endregion

        #region GetAllEntities

        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        public virtual IEnumerable<TModel> GetAllEntities()
        {
            return this.GetAllEntities(0, 0);
        }

        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <param name="sort">Sort details used for the query (column and order).</param>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        public virtual IEnumerable<TModel> GetAllEntities(ISortDetail<TModel> sort)
        {
            return this.GetAllEntities(0, 0, sort);
        }

        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <param name="startRowIndex">First row of the results to return.</param>
        /// <param name="maximumRows">Total number of rows to return.</param>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        public virtual IEnumerable<TModel> GetAllEntities(int startRowIndex, int maximumRows)
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
        public virtual IEnumerable<TModel> GetAllEntities(int startRowIndex, int maximumRows, ISortDetail<TModel> sort)
        {
            return this.GetAllEntities<TModel>(startRowIndex, maximumRows, sort);
        }

        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to retrieve.</typeparam>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        public virtual IEnumerable<TEntity> GetAllEntities<TEntity>()
            where TEntity : TModel, new()
        {
            return this.GetAllEntities<TEntity>(0, 0);
        }

        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to retrieve.</typeparam>
        /// <param name="sort">Sort details used for the query (column and order).</param>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        public virtual IEnumerable<TEntity> GetAllEntities<TEntity>(ISortDetail<TEntity> sort)
            where TEntity : TModel, new()
        {
            return this.GetAllEntities<TEntity>(0, 0, sort);
        }

        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to retrieve.</typeparam>
        /// <param name="startRowIndex">First row of the results to return.</param>
        /// <param name="maximumRows">Total number of rows to return.</param>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        public virtual IEnumerable<TEntity> GetAllEntities<TEntity>(int startRowIndex, int maximumRows)
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
        public abstract IEnumerable<TEntity> GetAllEntities<TEntity>(int startRowIndex, int maximumRows, ISortDetail<TEntity> sort)
            where TEntity : TModel, new();
        #endregion

        #region GetEntitiesBySearch

        /// <summary>
        /// Searches the data store for entities using the details set in the search detail instance.
        /// </summary>
        /// <param name="search">Details of what to search for.</param>
        /// <param name="entitiesToIgnore">Optional parameterized list of entities to ignore while searching.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        public virtual IEnumerable<TModel> GetEntitiesBySearch(ISearchDetail<TModel> search, params TKey[] entitiesToIgnore)
        {
            return this.GetEntitiesBySearch(0, 0, search, null, entitiesToIgnore);
        }

        /// <summary>
        /// Searches the data store for entities using the details set in the search detail instance.
        /// </summary>
        /// <param name="search">Details of what to search for.</param>
        /// <param name="sort">Sort details used for the query (column and order).</param>
        /// <param name="entitiesToIgnore">Optional parameterized list of entities to ignore while searching.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        public virtual IEnumerable<TModel> GetEntitiesBySearch(ISearchDetail<TModel> search, ISortDetail<TModel> sort, params TKey[] entitiesToIgnore)
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
        public virtual IEnumerable<TModel> GetEntitiesBySearch(int startRowIndex, int maximumRows, ISearchDetail<TModel> search, params TKey[] entitiesToIgnore)
        {
            return this.GetEntitiesBySearch<TModel>(startRowIndex, maximumRows, search, entitiesToIgnore);
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
        public virtual IEnumerable<TModel> GetEntitiesBySearch(int startRowIndex, int maximumRows, ISearchDetail<TModel> search, ISortDetail<TModel> sort, params TKey[] entitiesToIgnore)
        {
            return this.GetEntitiesBySearch<TModel>(startRowIndex, maximumRows, search, sort, entitiesToIgnore);
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
        public virtual IEnumerable<TModel> GetEntitiesBySearch(int startRowIndex, int maximumRows, ISearchDetail<TModel> search, ISortDetail<TModel> sort,out int totalRecordCount, params TKey[] entitiesToIgnore)
        {
            return this.GetEntitiesBySearch<TModel>(startRowIndex, maximumRows, search, sort,out totalRecordCount, entitiesToIgnore);
        }

        /// <summary>
        /// Searches the data store for entities using the details set in the search detail instance.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to search for.</typeparam>
        /// <param name="search">Details of what to search for.</param>
        /// <param name="entitiesToIgnore">Optional parameterized list of entities to ignore while searching.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        public virtual IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(ISearchDetail<TEntity> search, params TKey[] entitiesToIgnore)
            where TEntity : TModel, new()
        {
            return this.GetEntitiesBySearch<TEntity>(0, 0, search, null, entitiesToIgnore);
        }

        /// <summary>
        /// Searches the data store for entities using the details set in the search detail instance.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to search for.</typeparam>
        /// <param name="search">Details of what to search for.</param>
        /// <param name="sort">Sort details used for the query (column and order).</param>
        /// <param name="entitiesToIgnore">Optional parameterized list of entities to ignore while searching.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        public virtual IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(ISearchDetail<TEntity> search, ISortDetail<TEntity> sort, params TKey[] entitiesToIgnore)
            where TEntity : TModel, new()
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
        public virtual IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, params TKey[] entitiesToIgnore)
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
        public virtual IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, ISortDetail<TEntity> sort, params TKey[] entitiesToIgnore)
            where TEntity : TModel, new()
        {
            int totalRecordCount=0;
            return this.GetEntitiesBySearch<TEntity>(startRowIndex, maximumRows, search, sort, out totalRecordCount, entitiesToIgnore);
        }


        /// <summary>
        /// Searches the data store for entities using the details set in the search detail instance.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to search for.</typeparam>
        /// <param name="startRowIndex">First row of the results to return.</param>
        /// <param name="maximumRows">Total number of rows to return.</param>
        /// <param name="search">Details of what to search for.</param>
        /// <param name="sort">Sort details used for the query (column and order).</param>
        /// <param name="totalRecordCount">The total record count.</param>
        /// <param name="entitiesToIgnore">Optional parameterized list of entities to ignore while searching.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        public abstract IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, ISortDetail<TEntity> sort,out int totalRecordCount, params TKey[] entitiesToIgnore)
            where TEntity : TModel, new();

        #endregion

        #endregion

        #region Modify Methods

        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        public virtual void SaveEntity(TModel entity)
        {
            this.SaveEntity(entity, 0);
        }

        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        /// <param name="modifiedBy">The modified by.</param>
        public abstract void SaveEntity(TModel entity,int modifiedBy);

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        public abstract void DeleteEntity(TKey key);

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        /// <param name="modifiedBy">The modified by.</param>
        public abstract void DeleteEntity(TKey key,int modifiedBy);

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public abstract void DeleteEntity(TModel entity);

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        public abstract void DeleteEntity(TModel entity, int deletedBy);


        #endregion

        #endregion

        #region Protected Internal Methods

        /// <summary>
        /// Executes a stored procedure with the given parameters using the connection string and data access entity associated with the instance,
        /// and returns an entity representing the first record from the result set.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to return.</typeparam>
        /// <param name="storedProcedure">Stored procecdure to execute.</param>
        /// <param name="parameters">Parameters to pass into the stored procedure.</param>
        /// <returns>An entity representing the the first record of the result set.</returns>
        protected internal TEntity GetEntityInternal<TEntity>(string storedProcedure, params DbParameter[] parameters)
            where TEntity : TModel, new()
        {
            DataTable table = this.DataAccess.ExecuteDataTable(this.ConnectionString, storedProcedure, parameters);
            if (table.Rows.Count > 0)
            {
                return this.CreateEntity<TEntity>(table.Rows[0]);
            }

            return default(TEntity);
        }

        /// <summary>
        /// Executes a stored procedure with the given parameters using the connection string and data access entity associated with the instance,
        /// and returns a collection of entities representing the records from the result set.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to return.</typeparam>
        /// <param name="storedProcedure">Stored procecdure to execute.</param>
        /// <param name="startRowIndex">First row of the results to return.</param>
        /// <param name="maximumRows">Total number of rows to return.</param>
        /// <param name="sort">Sort details used for the query (column and order).</param>
        /// <param name="parameters">Parameters to pass into the stored procedure.</param>
        /// <returns>A collection of entities representing the result set.</returns>
        protected internal IEnumerable<TEntity> GetEntitiesInternal<TEntity>(string storedProcedure, int startRowIndex, int maximumRows, ISortDetail<TEntity> sort, params DbParameter[] parameters)
            where TEntity : TModel, new()
        {
            List<DbParameter> listParameters = this.AddPagingAndSort(parameters, startRowIndex, maximumRows, sort);
            return this.CreateEntities<TEntity>(this.DataAccess.ExecuteDataTable(this.ConnectionString, storedProcedure, listParameters.ToArray()));
        }

        /// <summary>
        /// Gets the entities from multiple tables.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <param name="sort">The sort.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="createRowMethods">The create row methods.</param>
        /// <returns>IEnumerable&lt;TEntity&gt;.</returns>
        /// <exception cref="System.ArgumentException">createRowMethods;Not enough function delegates supplied for the number of tables returned by the stored procedure.</exception>
        protected internal IEnumerable<TEntity> GetEntitiesFromMultipleTables<TEntity>(string storedProcedure, int startRowIndex, int maximumRows, ISortDetail<TEntity> sort, IEnumerable<DbParameter> parameters, params Func<DataRow, TEntity>[] createRowMethods)
            where TEntity : TModel, new()
        {
            List<DbParameter> listParameters = this.AddPagingAndSort(parameters, startRowIndex, maximumRows, sort);

            DataSet dataset = this.DataAccess.ExecuteDataSet(this.ConnectionString, storedProcedure, listParameters.ToArray());
            if (createRowMethods.Length < dataset.Tables.Count)
            {
                throw new ArgumentException("createRowMethods", "Not enough function delegates supplied for the number of tables returned by the stored procedure.");
            }
            
            for (int index = 0; index < dataset.Tables.Count; index++)
            {
                foreach (DataRow row in dataset.Tables[index].Rows)
                {
                    yield return createRowMethods[index](row);
                }
            }

            yield break;
        }

        /// <summary>
        /// Gets the entities by search internal.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="search">The search.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <param name="sort">The sort.</param>
        /// <param name="ignoreTableParameter">The ignore table parameter.</param>
        /// <param name="createIgnoreTableFunction">The create ignore table function.</param>
        /// <param name="entitiesToIgnore">The entities to ignore.</param>
        /// <returns>IEnumerable&lt;TEntity&gt;.</returns>
        protected internal IEnumerable<TEntity> GetEntitiesBySearchInternal<TEntity>(ISearchDetail<TEntity> search, string storedProcedure, int startRowIndex, int maximumRows, ISortDetail<TEntity> sort, string ignoreTableParameter, Func<IEnumerable<TKey>, IEnumerable<SqlDataRecord>> createIgnoreTableFunction, IEnumerable<TKey> entitiesToIgnore)
            where TEntity : TModel, new()
        {
            return this.GetEntitiesBySearchInternal<TEntity, TKey>(search, storedProcedure, startRowIndex, maximumRows, sort, ignoreTableParameter, createIgnoreTableFunction, entitiesToIgnore);
        }

        /// <summary>
        /// Gets the entities by search internal.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <typeparam name="TIgnoreColumnType">The type of the t ignore column type.</typeparam>
        /// <param name="search">The search.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <param name="sort">The sort.</param>
        /// <param name="ignoreTableParameter">The ignore table parameter.</param>
        /// <param name="createIgnoreTableFunction">The create ignore table function.</param>
        /// <param name="entitiesToIgnore">The entities to ignore.</param>
        /// <returns>IEnumerable&lt;TEntity&gt;.</returns>
        protected internal IEnumerable<TEntity> GetEntitiesBySearchInternal<TEntity, TIgnoreColumnType>(ISearchDetail<TEntity> search, string storedProcedure, int startRowIndex, int maximumRows, ISortDetail<TEntity> sort, string ignoreTableParameter, Func<IEnumerable<TIgnoreColumnType>, IEnumerable<SqlDataRecord>> createIgnoreTableFunction, IEnumerable<TIgnoreColumnType> entitiesToIgnore)
            where TEntity : TModel, new()
        {
            List<DbParameter> parameters = search.GetSearchParameters(this.DataAccess) as List<DbParameter>;
            parameters.Add(this.DataAccess.CreateParameter(ignoreTableParameter, SqlDbType.Structured, createIgnoreTableFunction(entitiesToIgnore)));
            return this.GetEntitiesInternal<TEntity>(storedProcedure, startRowIndex, maximumRows, sort, parameters.ToArray());
        }

        #endregion


        /// <summary>
        /// Gets the parameters from entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>IEnumerable&lt;DbParameter&gt;.</returns>
        public virtual IEnumerable<DbParameter> GetParametersFromEntity<TEntity>(TEntity entity) where TEntity : TModel
        {
            return new List<DbParameter>();
        }






    }
}
