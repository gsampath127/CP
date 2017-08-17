// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-03-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Model.Keys;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace RRD.FSG.RP.Model.Factories.Client
{
    /// <summary>
    /// Class SiteFeatureFactory.
    /// </summary>
    public class SiteFeatureFactory : AuditedBaseFactory<SiteFeatureObjectModel, SiteFeatureKey>
    {

         #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteFeatureFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public SiteFeatureFactory(IDataAccess dataAccess)
            : base(dataAccess) { }
        #endregion

        #region Constants
        /// <summary>
        /// declartion of all Strings/Procedures used as constants
        /// </summary>
        private const string SPGetAllSiteFeature = "RPV2HostedAdmin_GetAllSiteFeature";
        /// <summary>
        /// The sp save SiteFeature
        /// </summary>
        private const string SPSaveSiteFeature = "RPV2HostedAdmin_SaveSiteFeature";
        /// <summary>
        /// The sp Delete SiteFeature
        /// </summary>
        private const string SPDeleteSiteFeature = "RPV2HostedAdmin_DeleteSiteFeature";

        /// <summary>
        /// SiteId
        /// </summary>
        private const string DBCSiteId = "SiteId";
        /// <summary>
        /// SiteKey
        /// </summary>

        private const string DBCSiteKey = "SiteKey";
        /// <summary>
        /// FeatureMode
        /// </summary>

        private const string DBCFeatureMode = "FeatureMode";
             
        #endregion

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

                entity.SiteId = entityRow.Field<int>(DBCSiteId);
                entity.SiteKey = entityRow.Field<string>(DBCSiteKey);
                entity.FeatureMode = entityRow.Field<int>(DBCFeatureMode);
            }

            return entity;
        }


        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        public override void SaveEntity(SiteFeatureObjectModel entity)
        {


            List<DbParameter> parameters = base.GetParametersFromEntity<SiteFeatureObjectModel>(entity) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCSiteId, DbType.Int32, entity.SiteId));

                parameters.Add(DataAccess.CreateParameter(DBCSiteKey, DbType.String, entity.SiteKey));

                parameters.Add(DataAccess.CreateParameter(DBCFeatureMode, DbType.Int32, entity.FeatureMode));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPSaveSiteFeature, parameters);

            }


        }

        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        /// <param name="modifiedBy">The modified by.</param>
        public override void SaveEntity(SiteFeatureObjectModel entity, int modifiedBy)
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
        public override TEntity GetEntityByKey<TEntity>(SiteFeatureKey key)
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
            return this.GetEntitiesInternal<TEntity>(SPGetAllSiteFeature, startRowIndex, maximumRows, null);

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
            return this.GetEntitiesInternal<TEntity>(SPGetAllSiteFeature, startRowIndex, maximumRows, sort);
        }

        /// <summary>
        /// Searches the data store for entities using the details set in the search detail instance.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <param name="search">Details of what to search for.</param>
        /// <param name="sort">The sort.</param>
        /// <param name="entitiesToIgnore">Optional parameterized list of entities to ignore while searching.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        public override IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, ISortDetail<TEntity> sort, params SiteFeatureKey[] entitiesToIgnore)
        {
            return this.GetEntitiesInternal<TEntity>(SPGetAllSiteFeature, startRowIndex, maximumRows, sort);
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
        public override IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, ISortDetail<TEntity> sort,out int totalRecordCount, params SiteFeatureKey[] entitiesToIgnore)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        public override void DeleteEntity(SiteFeatureKey key)
        {
            DeleteEntity(key, 0);
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(SiteFeatureKey key, int modifiedBy)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(SiteFeatureObjectModel entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        public override void DeleteEntity(SiteFeatureObjectModel entity, int deletedBy)
        {
            List<DbParameter> parameters = base.GetDeleteParameters(deletedBy) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCSiteId, DbType.Int32, entity.SiteId));
                parameters.Add(DataAccess.CreateParameter(DBCSiteKey, DbType.String, entity.SiteKey));
                

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeleteSiteFeature, parameters);
            }
        }
    }
}
