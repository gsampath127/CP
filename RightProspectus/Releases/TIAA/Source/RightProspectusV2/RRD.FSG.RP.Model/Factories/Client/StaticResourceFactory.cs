// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-03-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace RRD.FSG.RP.Model.Factories.Client
{
    /// <summary>
    /// Class StaticResourceFactory.
    /// </summary>
    public class StaticResourceFactory : AuditedBaseFactory<StaticResourceObjectModel, int>
    {
        #region Constants
        /// <summary>
        /// declaration of all Strings/Procedures used as constants
        /// </summary>
        private const string SPGetAllStaticResource = "RPV2HostedAdmin_GetAllStaticResource";

        /// <summary>
        /// The sp save StaticResource
        /// </summary>
        private const string SPSaveStaticResource = "RPV2HostedAdmin_SaveStaticResource";

        /// <summary>
        /// The sp deletes StaticResource
        /// </summary>
        private const string SPDeleteStaticResource = "RPV2HostedAdmin_DeleteStaticResource";

        /// <summary>
        /// The DBC static resource identifier
        /// </summary>
        private const string DBCStaticResourceId = "staticResourceId";

        /// <summary>
        /// The DBC file name
        /// </summary>
        private const string DBCFileName = "fileName";

        /// <summary>
        /// The DBC size
        /// </summary>
        private const string DBCSize = "size";

        /// <summary>
        /// The DBC MIME type
        /// </summary>
        private const string DBCMimeType = "mimeType";

        /// <summary>
        /// The DBC data
        /// </summary>
        private const string DBCData = "data";

        /// <summary>
        /// The DBC UTC modified date
        /// </summary>
        private const string DBCUtcModifiedDate = "utcModifiedDate";

        /// <summary>
        /// The DBC modified by
        /// </summary>
        private const string DBCModifiedBy = "modifiedBy";


        #endregion


        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteAdministrationFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public StaticResourceFactory(IDataAccess dataAccess)
            : base(dataAccess) { }
        #endregion


        /// <summary>
        /// Retrieves entity details from the underlying data access and uses them to populate an entity instance.
        /// </summary>
        /// <param name="key">Primary key identifier of the entity to retrieve.</param>
        /// <returns>A new instance of type <typeparamref name="TModel" /> if found. Null otherwise.</returns>
        public override StaticResourceObjectModel GetEntityByKey(int key)
        {
            return GetStaticResourceEntity(key);
        }

        /// <summary>
        /// Retrieves a specific taxonomy level entity using the passed in hierarchical level and identifier.
        /// </summary>
        /// <param name="key">The taxonomy key of the entity to retrieve. Consists of Level and TaxonomyId.</param>
        /// <returns>A <see cref="TaxonomyEntity" /> entity.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public StaticResourceObjectModel GetStaticResourceEntity(int key)
        {
            throw new NotImplementedException();

        }


        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        public override IEnumerable<TEntity> GetAllEntities<TEntity>(int startRowIndex, int maximumRows)
        {
            return this.GetEntitiesInternal<TEntity>(SPGetAllStaticResource, startRowIndex, maximumRows, null);
        }

        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <param name="sort">The sort.</param>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        public override IEnumerable<TEntity> GetAllEntities<TEntity>(int startRowIndex, int maximumRows, Interfaces.ISortDetail<TEntity> sort)
        {
            return this.GetEntitiesInternal<TEntity>(SPGetAllStaticResource, startRowIndex, maximumRows, sort);
        }

        /// <summary>
        /// Instantiates a new entity and populates members with the data record passed in.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to create.</typeparam>
        /// <param name="entityRow">Data row used to create the entity.</param>
        /// <returns>A new entity of type <typeparamref name="TEntity" />.</returns>
        public override TEntity CreateEntity<TEntity>(DataRow entityRow)
        {
            TEntity entity = base.CreateEntity<TEntity>(entityRow);
            if (entity != null)
            {

                entity.StaticResourceId = entityRow.Field<int>(DBCStaticResourceId);
                entity.Key = entity.StaticResourceId;
                entity.FileName = entityRow.Field<string>(DBCFileName);
                entity.Size = entityRow.Field<int>(DBCSize);
                entity.MimeType = entityRow.Field<string>(DBCMimeType);
                entity.Data = entityRow.Field<byte[]>(DBCData);
            }

            return entity;
        }




        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        public override void SaveEntity(StaticResourceObjectModel entity)
        {


            List<DbParameter> parameters = base.GetParametersFromEntity<StaticResourceObjectModel>(entity) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCStaticResourceId, DbType.Int32, entity.StaticResourceId));

                parameters.Add(DataAccess.CreateParameter(DBCFileName, DbType.String, entity.FileName));

                parameters.Add(DataAccess.CreateParameter(DBCSize, DbType.Int32, entity.Size));

                parameters.Add(DataAccess.CreateParameter(DBCMimeType, DbType.String, entity.MimeType));

                parameters.Add(DataAccess.CreateParameter(DBCData, SqlDbType.VarBinary, entity.Data));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPSaveStaticResource, parameters);

            }


        }

        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        /// <param name="modifiedBy">The modified by.</param>
        public override void SaveEntity(StaticResourceObjectModel entity, int modifiedBy)
        {
            base.SetModifiedBy(entity, modifiedBy);
            this.SaveEntity(entity);
        }



        /// <summary>
        /// Retrieves entity details from the underlying data access and uses them to populate an entity instance.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="key">Primary key identifier of the entity to retrieve.</param>
        /// <returns>A new instance of type <typeparamref name="TModel" /> if found. Null otherwise.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override TEntity GetEntityByKey<TEntity>(int key)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        public override void DeleteEntity(int key)
        {
            DeleteEntity(key, 0);
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        /// <param name="deletedBy">The deleted by.</param>
        public override void DeleteEntity(int key, int deletedBy)
        {
            List<DbParameter> parameters = base.GetDeleteParameters(deletedBy) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCStaticResourceId, DbType.Int32, key));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeleteStaticResource, parameters);
            }
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(StaticResourceObjectModel entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(StaticResourceObjectModel entity, int deletedBy)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// Searches the data store for entities using the details set in the search detail instance.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <param name="search">Details of what to search for.</param>
        /// <param name="sort">The sort.</param>
        /// <param name="totalRecordCount">The total record count.</param>
        /// <param name="entitiesToIgnore">Optional parameterized list of entities to ignore while searching.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, ISortDetail<TEntity> sort, out int totalRecordCount, params int[] entitiesToIgnore)
        {
            throw new NotImplementedException();
        }
    }
}
