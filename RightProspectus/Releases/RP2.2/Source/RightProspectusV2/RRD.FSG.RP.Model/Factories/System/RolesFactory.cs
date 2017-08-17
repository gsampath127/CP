// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-03-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;


namespace RRD.FSG.RP.Model.Factories.System
{
    /// <summary>
    /// Class RolesFactory.
    /// </summary>
    public class RolesFactory : AuditedBaseFactory<RolesObjectModel, int>
    {
        #region Constants
        /// <summary>
        /// The sp save Roles
        /// </summary>
        private const string SPSaveRoles = "RPV2HostedAdmin_SaveRoles";

        /// <summary>
        /// The sp delete Roles
        /// </summary>
        private const string SPDeleteRoles = "RPV2HostedAdmin_DeleteRoles";

        /// <summary>
        /// The sp get Roless
        /// </summary>
        private const string SPGetAllRoles = "RPV2HostedAdmin_GetAllRoles";


        /// <summary>
        /// The Database Column Roles identifier
        /// </summary>
        private const string DBCRolesId = "RoleId";

        /// <summary>
        /// The Database Column Roles name
        /// </summary>
        private const string DBCRolesName = "Name";

        /// <summary>
        /// The database modified by
        /// </summary>
        private const string DBCmodifiedBy = "modifiedBy";

        /// <summary>
        /// The database utc modified date
        /// </summary>
        private const string DBCutcModifiedDate = "utcModifiedDate";
        #endregion



        /// <summary>
        /// Initializes a new instance of the <see cref="RolesFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public RolesFactory(IDataAccess dataAccess)
            : base(dataAccess)
        {
            this.ConnectionString = DBConnectionString.SystemDBConnectionString();
        }




        /// <summary>
        /// Retrieves entity details from the underlying data access and uses them to populate an entity instance.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to retrieve.</typeparam>
        /// <param name="key">Primary key identifier of the entity to retrieve.</param>
        /// <returns>A new instance of type <typeparamref name="TEntity" /> if found. Null otherwise.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override TEntity GetEntityByKey<TEntity>(int key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves entity details from the underlying data access and uses them to populate an entity instance.
        /// </summary>
        /// <param name="key">Primary key identifier of the entity to retrieve.</param>
        /// <returns>A new instance of type <typeparamref name="TModel" /> if found. Null otherwise.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override RolesObjectModel GetEntityByKey(int key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Instantiates a new entity and populates members with the data record passed in.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to create.</typeparam>
        /// <param name="entityRecord">Data record used to create the entity.</param>
        /// <returns>A new entity of type <typeparamref name="TEntity" />.</returns>
        public override TEntity CreateEntity<TEntity>(IDataRecord entityRecord)
        {
            TEntity entity = base.CreateEntity<TEntity>(entityRecord);
            if (entity != null)
            {
                entity.RoleId = entityRecord.GetInt32(entityRecord.GetOrdinal(DBCRolesId));
                entity.Name = entityRecord.GetString(entityRecord.GetOrdinal(DBCRolesName));
                entity.Key = entity.RoleId;
            }

            return entity;
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

                entity.RoleId = entityRow.Field<int>(DBCRolesId);
                entity.Name = entityRow.Field<string>(DBCRolesName);
                entity.Key = entity.RoleId;
            }

            return entity;
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
            return this.GetEntitiesInternal<TEntity>(SPGetAllRoles, startRowIndex, maximumRows, null);
        }

        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <param name="sort">The sort.</param>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        public override IEnumerable<TEntity> GetAllEntities<TEntity>(int startRowIndex, int maximumRows, ISortDetail<TEntity> sort)
        {
            return this.GetEntitiesInternal<TEntity>(SPGetAllRoles, startRowIndex, maximumRows, sort);
        }



        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        public override void SaveEntity(RolesObjectModel entity)
        {
            List<DbParameter> parameters = base.GetParametersFromEntity<RolesObjectModel>(entity) as List<DbParameter>;
            if (parameters != null)
            {

                parameters.Add(DataAccess.CreateParameter(DBCRolesId, DbType.Int32, entity.RoleId));

                parameters.Add(DataAccess.CreateParameter(DBCRolesName, DbType.String, entity.Name));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPSaveRoles, parameters);
            }
        }

        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        /// <param name="modifiedBy">The modified by.</param>
        public override void SaveEntity(RolesObjectModel entity, int modifiedBy)
        {
            base.SetModifiedBy(entity, modifiedBy);
            this.SaveEntity(entity);
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        public override void DeleteEntity(int key)
        {
            this.DeleteEntity(key, 0);
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
                parameters.Add(DataAccess.CreateParameter(DBCRolesId, DbType.Int32, key));
                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeleteRoles, parameters);
            }
        }


        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(RolesObjectModel entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(RolesObjectModel entity, int deletedBy)
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
