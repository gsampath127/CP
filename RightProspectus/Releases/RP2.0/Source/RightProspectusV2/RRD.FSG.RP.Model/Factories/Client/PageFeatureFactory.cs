// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-13-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Model.Keys;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

/// <summary>
/// The Client namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Factories.Client
{
    /// <summary>
    /// Class PageFeatureFactory.
    /// </summary>
    public class PageFeatureFactory : AuditedBaseFactory<PageFeatureObjectModel, PageFeatureKey>
    {

        #region Constants
        /// <summary>
        /// declaration of all Strings/Procedures used as constants
        /// </summary>
        private const string SPDeletePageFeature = "RPV2HostedAdmin_DeletePageFeature";
        /// <summary>
        /// The sp get all PageFeature
        /// </summary>
        private const string SPGetAllPageFeature = "RPV2HostedAdmin_GetAllPageFeature";
        /// <summary>
        /// The sp save PageFeature
        /// </summary>
        private const string SPSavePageFeature = "RPV2HostedAdmin_SavePageFeature";



        /// <summary>
        /// SiteId
        /// </summary>
        private const string DBCSiteId = "SiteId";
        /// <summary>
        /// PageId
        /// </summary>
        private const string DBCPageId = "PageId";
        /// <summary>
        /// Key
        /// </summary>
        private const string DBCKey = "PageKey";
        /// <summary>
        /// FeatureMode
        /// </summary>
        private const string DBCFeatureMode = "FeatureMode";
        /// <summary>
        /// UtcModifiedDate
        /// </summary>
        private const string DBCUtcModifiedDate = "UtcModifiedDate";
        /// <summary>
        /// ModifiedBy
        /// </summary>
        private const string DBCModifiedBy = "ModifiedBy";

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="PageFeatureFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public PageFeatureFactory(IDataAccess dataAccess)
            : base(dataAccess) { }
        #endregion


        /// <summary>
        /// Instantiates a new entity and populates members with the data record passed in.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to create.</typeparam>
        /// <param name="entityRow">The entity row.</param>
        /// <returns>A new entity of type <typeparamref name="TEntity" />.</returns>
        public override TEntity CreateEntity<TEntity>(DataRow entityRow)
        {
            TEntity entity = base.CreateEntity<TEntity>(entityRow);
            if (entity != null)
            {

                entity.SiteId = entityRow.Field<int>(DBCSiteId);
                entity.PageId = entityRow.Field<int>(DBCPageId);
                entity.PageKey = entityRow.Field<string>(DBCKey);
                entity.FeatureMode = entityRow.Field<int>(DBCFeatureMode);
            }

            return entity;
        }


        /// <summary>
        /// Retrieves entity details from the underlying data access and uses them to populat an entity instance.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="key">Primary key identifier of the entity to retrieve.</param>
        /// <returns>A new instance of type <typeparamref name="TModel" /> if found. Null otherwise.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override TEntity GetEntityByKey<TEntity>(PageFeatureKey key)
        {
            throw new NotImplementedException();
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
            IEnumerable<TEntity> pageFeatureDetails = this.GetEntitiesInternal<TEntity>(SPGetAllPageFeature, startRowIndex, maximumRows, null);

            IEnumerable<TemplatePageObjectModel> templatePageDetails = new TemplatePageFactory(DataAccess).GetAllEntities();

            pageFeatureDetails = pageFeatureDetails.Select(x =>
            {
                x.PageName = templatePageDetails.FirstOrDefault(t => t.PageID == x.PageId).PageName;
                x.PageDescription = templatePageDetails.FirstOrDefault(t => t.PageID == x.PageId).PageDescription;
                return x;
            });
            return pageFeatureDetails;
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
        public override IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, ISortDetail<TEntity> sort, params PageFeatureKey[] entitiesToIgnore)
        {
            IEnumerable<TEntity> pageFeatureDetails = this.GetEntitiesInternal<TEntity>(SPGetAllPageFeature, startRowIndex, maximumRows, sort);

            IEnumerable<TemplatePageObjectModel> templatePageDetails = new TemplatePageFactory(DataAccess).GetAllEntities();

            pageFeatureDetails = pageFeatureDetails.Select(x =>
            {
                x.PageName = templatePageDetails.FirstOrDefault(t => t.PageID == x.PageId).PageName;
                x.PageDescription = templatePageDetails.FirstOrDefault(t => t.PageID == x.PageId).PageDescription;
                return x;
            });
            return pageFeatureDetails;
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
        public override IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, ISortDetail<TEntity> sort, out int totalRecordCount, params PageFeatureKey[] entitiesToIgnore)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        public override void SaveEntity(PageFeatureObjectModel entity)
        {
            List<DbParameter> parameters = base.GetParametersFromEntity<PageFeatureObjectModel>(entity) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCSiteId, DbType.Int32, entity.SiteId));
                parameters.Add(DataAccess.CreateParameter(DBCPageId, DbType.Int32, entity.PageId));
                parameters.Add(DataAccess.CreateParameter(DBCKey, DbType.String, entity.PageKey));
                parameters.Add(DataAccess.CreateParameter(DBCFeatureMode, DbType.Int32, entity.FeatureMode));
                DataAccess.ExecuteNonQuery(this.ConnectionString, SPSavePageFeature, parameters);
            }
        }

        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        /// <param name="modifiedBy">The modified by.</param>
        public override void SaveEntity(PageFeatureObjectModel entity, int modifiedBy)
        {
            base.SetModifiedBy(entity, modifiedBy);
            this.SaveEntity(entity);
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        public override void DeleteEntity(PageFeatureKey key)
        {
            DeleteEntity(key, 0);
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(PageFeatureKey key, int modifiedBy)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void DeleteEntity(PageFeatureObjectModel entity)
        {
            DeleteEntity(entity, 0);    
        }

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="modifiedBy">The modified by.</param>
        public override void DeleteEntity(PageFeatureObjectModel entity, int modifiedBy)
        {
            List<DbParameter> parameters = base.GetDeleteParameters(modifiedBy) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCPageId, DbType.Int32, entity.PageId));
                parameters.Add(DataAccess.CreateParameter(DBCKey, DbType.String, entity.PageKey));
                parameters.Add(DataAccess.CreateParameter(DBCSiteId, DbType.Int32, entity.SiteId));
                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeletePageFeature, parameters);
            }
        }
    }
}
