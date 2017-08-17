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

namespace RRD.FSG.RP.Model.Factories.Client
{
    /// <summary>
    /// Class SiteNavigationFactory.
    /// </summary>
    public class SiteNavigationFactory : AuditedBaseFactory<SiteNavigationObjectModel, SiteNavigationKey>
    {
        #region Constants
        /// <summary>
        /// declaration of all Strings/Procedures used as constants
        /// </summary>
        private const string SPGetAllStaticResource = "RPV2HostedAdmin_GetAllSiteNavigation";

        /// <summary>
        /// The sp save StaticResource
        /// </summary>
        private const string SPSaveStaticResource = "RPV2HostedAdmin_SaveSiteNavigation";

        /// <summary>
        /// The sp deletes StaticResource
        /// </summary>
        private const string SPDeleteSiteNavigation = "RPV2HostedAdmin_DeleteSiteNavigation";


        /// <summary>
        /// The DBC site navigation identifier
        /// </summary>
        private const string DBCSiteNavigationId = "SiteNavigationId";

        /// <summary>
        /// The DBC site identifier
        /// </summary>
        private const string DBCSiteId = "SiteId";

        /// <summary>
        /// The DBC navigation key
        /// </summary>
        private const string DBCNavigationKey = "NavigationKey";

        /// <summary>
        /// The DBC page identifier
        /// </summary>
        private const string DBCPageId = "PageId";

        /// <summary>
        /// The DBC language culture
        /// </summary>
        private const string DBCLanguageCulture = "LanguageCulture";

        /// <summary>
        /// The DBC UTC modified date
        /// </summary>
        private const string DBCUtcModifiedDate = "UtcModifiedDate";

        /// <summary>
        /// The DBC modified by
        /// </summary>
        private const string DBCModifiedBy = "ModifiedBy";

        /// <summary>
        /// The DBC current version
        /// </summary>
        private const string DBCCurrentVersion = "CurrentVersion";

        /// <summary>
        /// The DBC version
        /// </summary>
        private const string DBCVersion = "Version";

        /// <summary>
        /// The DBC navigation XML
        /// </summary>
        private const string DBCNavigationXML = "NavigationXML";

        /// <summary>
        /// The DBC is proofing
        /// </summary>
        private const string DBCIsProofing = "IsProofing";

        /// <summary>
        /// The DBC is proofing available for site navigation identifier
        /// </summary>
        private const string DBCIsProofingAvailableForSiteNavigationId = "IsProofingAvailableForSiteNavigationId";

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteAdministrationFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public SiteNavigationFactory(IDataAccess dataAccess)
            : base(dataAccess) { }
        #endregion
        /// <summary>
        /// Retrieves entity details from the underlying data access and uses them to populate an entity instance.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <returns>A new instance of type <typeparamref name="TModel" /> if found. Null otherwise.</returns>
        //public StaticResourceObjectModel GetStaticResourceEntity(int key)
        //{
        //    throw new NotImplementedException();
        //}
        public override IEnumerable<TEntity> GetAllEntities<TEntity>(int startRowIndex, int maximumRows)
        {
            IEnumerable<TEntity> sitenavigationDetails = this.GetEntitiesInternal<TEntity>(SPGetAllStaticResource, startRowIndex, maximumRows, null);

            IEnumerable<TemplatePageObjectModel> templatePageDetails = new TemplatePageFactory(DataAccess).GetAllEntities();

            sitenavigationDetails = sitenavigationDetails.Select(x =>
            {
                x.PageName = (x.PageId == 0 || x.PageId == null) ? "" : templatePageDetails.FirstOrDefault(t => t.PageID == x.PageId).PageName;
                x.PageDescription = (x.PageId == 0 || x.PageId == null) ? "" : templatePageDetails.FirstOrDefault(t => t.PageID == x.PageId).PageDescription;
                return x;
            });
            return sitenavigationDetails;
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
            IEnumerable<TEntity> sitenavigationDetails = this.GetEntitiesInternal<TEntity>(SPGetAllStaticResource, startRowIndex, maximumRows, sort);

            IEnumerable<TemplatePageObjectModel> templatePageDetails = new TemplatePageFactory(DataAccess).GetAllEntities();

            sitenavigationDetails = sitenavigationDetails.Select(x =>
            {
                x.PageName = (x.PageId == 0 || x.PageId == null) ? "" : templatePageDetails.FirstOrDefault(t => t.PageID == x.PageId).PageName;
                x.PageDescription = (x.PageId == 0 || x.PageId == null) ? "" : templatePageDetails.FirstOrDefault(t => t.PageID == x.PageId).PageDescription;
                return x;
            });
            return sitenavigationDetails;
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

                entity.SiteNavigationId = entityRow.Field<int>(DBCSiteNavigationId);
                entity.PageId = entityRow.Field<int?>(DBCPageId);
                entity.NavigationKey = entityRow.Field<string>(DBCNavigationKey);
                entity.SiteId = entityRow.Field<int>(DBCSiteId);                
                entity.NavigationXML = entityRow.Field<string>(DBCNavigationXML);
                entity.IsProofing = entityRow.Field<bool>(DBCIsProofing);
                entity.IsProofingAvailableForSiteNavigationId = entityRow.Field<bool>(DBCIsProofingAvailableForSiteNavigationId);
                entity.Version = entityRow.Field<int>(DBCVersion);
                entity.ModifiedBy = entityRow.Field<int>(DBCModifiedBy);

            }

            return entity;
        }
        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        public override void SaveEntity(SiteNavigationObjectModel entity)
        {


            List<DbParameter> parameters = base.GetParametersFromEntity<SiteNavigationObjectModel>(entity) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCSiteId, DbType.Int32, entity.SiteId));

                parameters.Add(DataAccess.CreateParameter(DBCSiteNavigationId, DbType.Int32, entity.SiteNavigationId));

                parameters.Add(DataAccess.CreateParameter(DBCNavigationKey, DbType.String, entity.NavigationKey));

                parameters.Add(DataAccess.CreateParameter(DBCPageId, DbType.Int32, entity.PageId));                

                parameters.Add(DataAccess.CreateParameter(DBCVersion, DbType.Int32, entity.Version));

                parameters.Add(DataAccess.CreateParameter(DBCNavigationXML, DbType.Xml, entity.NavigationXML));

                parameters.Add(DataAccess.CreateParameter(DBCIsProofing, DbType.Boolean, entity.IsProofing));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPSaveStaticResource, parameters);

            }
        }
        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        /// <param name="modifiedBy">The modified by.</param>
        public override void SaveEntity(SiteNavigationObjectModel entity, int modifiedBy)
        {
            base.SetModifiedBy(entity, modifiedBy);
            this.SaveEntity(entity);
        }
        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        public override void DeleteEntity(SiteNavigationKey key)
        {
            DeleteEntity(key, 0);
        }
        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        /// <param name="deletedBy">The deleted by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(SiteNavigationKey key, int deletedBy)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void DeleteEntity(SiteNavigationObjectModel entity)
        {
            DeleteEntity(entity, 0);
        }
       
        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        public override void DeleteEntity(SiteNavigationObjectModel entity, int deletedBy)
        {
            List<DbParameter> parameters = base.GetDeleteParameters(deletedBy) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCSiteNavigationId, DbType.Int32, entity.SiteNavigationId));
                parameters.Add(DataAccess.CreateParameter(DBCVersion, DbType.Int32, entity.Version));
                parameters.Add(DataAccess.CreateParameter(DBCIsProofing, DbType.Int32, entity.IsProofing));
                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeleteSiteNavigation, parameters);
            }
        }
        /// <summary>
        /// Retrieves entity details from the underlying data access and uses them to populat an entity instance.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="key">Primary key identifier of the entity to retrieve.</param>
        /// <returns>A new instance of type <typeparamref name="TModel" /> if found. Null otherwise.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override TEntity GetEntityByKey<TEntity>(SiteNavigationKey key)
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
        /// <param name="entitiesToIgnore">Optional parameterized list of entities to ignore while searching.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        public override IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, ISortDetail<TEntity> sort, params SiteNavigationKey[] entitiesToIgnore)
        {
            return this.GetEntitiesInternal<TEntity>(SPGetAllStaticResource, startRowIndex, maximumRows, sort);
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
        public override IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, ISortDetail<TEntity> sort,out int totalRecordCount, params SiteNavigationKey[] entitiesToIgnore)
        {
            throw new NotImplementedException();
        }
    }
}
