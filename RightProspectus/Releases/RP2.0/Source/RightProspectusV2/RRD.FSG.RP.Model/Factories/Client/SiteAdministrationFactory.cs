using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace RRD.FSG.RP.Model.Factories.Client
{
    /// <summary>
    /// Factory class for site administration
    /// </summary>
    public class SiteAdministrationFactory : AuditedBaseFactory<SiteAdministrationObjectModel, int>
    {
        #region Constants
        /// <summary>
        /// declaration of all Strings/Procedures used as constants
        /// </summary>
        private const string SPRPV2HostedAdminGetSites = "RPV2HostedAdmin_GetSites";
        /// <summary>
        /// The DBC site identifier
        /// </summary>
        private const string DBCSiteId = "siteId";
        /// <summary>
        /// The DBC site name
        /// </summary>
        private const string DBCSiteName = "name";
        /// <summary>
        /// The DBC template identifier
        /// </summary>
        private const string DBCTemplateId = "templateId";
        /// <summary>
        /// The DBC default page identifier
        /// </summary>
        private const string DBCDefaultPageId = "defaultPageId";
        /// <summary>
        /// The DBC parent site identifier
        /// </summary>
        private const string DBCParentSiteId = "parentSiteId";
        /// <summary>
        /// The DBC description
        /// </summary>
        private const string DBCDescription = "description";
        /// <summary>
        /// The DBC UTC modified date
        /// </summary>
        private const string DBCUtcModifiedDate = "utcModifiedDate";
        /// <summary>
        /// The DBC modified by
        /// </summary>
        private const string DBCModifiedBy = "modifiedBy ";
        /// <summary>
        /// The DBC page index
        /// </summary>
        private const string DBCPageIndex = "pageIndex";
        /// <summary>
        /// The DBC page size
        /// </summary>
        private const string DBCPageSize = "pageSize";
        /// <summary>
        /// The DBC sort direction
        /// </summary>
        private const string DBCSortDirection = "sortDirection ";
        /// <summary>
        /// The DBC sort column
        /// </summary>
        private const string DBCSortColumn = "sortColumn";
        /// <summary>
        /// The DBC count
        /// </summary>
        private const string DBCCount = "count ";
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteAdministrationFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public SiteAdministrationFactory(IDataAccess dataAccess)
            : base(dataAccess) { }
        #endregion


        /// <summary>
        /// returns the entities that matches the searchEntity.
        /// </summary>
        /// <typeparam name="T">Type of entity to create.</typeparam>
        /// <param name="searchEntity">Data record used to search the entity.</param>
        /// <returns>A new entity.</returns>
        public IEnumerable<SiteAdministrationObjectModel> GetAllEntityByEntitySearch<T>(T searchEntity)
        {
            SiteAdministrationObjectSearchModel T1 = searchEntity as SiteAdministrationObjectSearchModel;

            List<SiteAdministrationObjectModel> siteDataModels = new List<SiteAdministrationObjectModel>();



            var results = DataAccess.ExecuteDataTable(DBConnectionString.HostedConnectionString(T1.ClientName, DataAccess), SPRPV2HostedAdminGetSites,
                          DataAccess.CreateParameter(DBCSiteId, DbType.Int32, T1.SiteID),
                          DataAccess.CreateParameter(DBCSiteName, DbType.String, T1.SiteName),
                          DataAccess.CreateParameter(DBCTemplateId, DbType.String, T1.TemplateName),
                          DataAccess.CreateParameter(DBCDefaultPageId, DbType.String, T1.DefaultPageName),
                          DataAccess.CreateParameter(DBCParentSiteId, DbType.Int32, T1.ParentSiteID == null ? null : T1.ParentSiteID),
                          DataAccess.CreateParameter(DBCDescription, DbType.String, string.IsNullOrEmpty(T1.Description) ? null : T1.Description),
                          DataAccess.CreateParameter(DBCUtcModifiedDate, DbType.Date, null),
                          DataAccess.CreateParameter(DBCModifiedBy, DbType.String, null),
                          DataAccess.CreateParameter(DBCPageSize, DbType.Int32, T1.PageSize),
                          DataAccess.CreateParameter(DBCPageIndex, DbType.Int32, T1.PageIndex),
                          DataAccess.CreateParameter(DBCSortDirection, DbType.String, T1.SortDirection),
                          DataAccess.CreateParameter(DBCSortColumn, DbType.String, T1.SortColumn)
                          );


            foreach (DataRow datarow in results.Rows)
            {

                siteDataModels.Add(CreateEntity<SiteAdministrationObjectModel>(datarow));
            }

            return siteDataModels;

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
                entity.SiteID = entityRecord.GetInt32(entityRecord.GetOrdinal(DBCSiteId));
                entity.SiteName = entityRecord.GetString(entityRecord.GetOrdinal(DBCSiteName));
                entity.TemplateID = entityRecord.GetInt32(entityRecord.GetOrdinal(DBCTemplateId));
                entity.DefaultPageID = entityRecord.GetInt32(entityRecord.GetOrdinal(DBCDefaultPageId));
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
                entity.SiteID = entityRow.Field<int>("SiteID");
                entity.SiteName = entityRow.Field<string>("SiteName");
                entity.TemplateID = entityRow.Field<int>("TemplateName");
                entity.DefaultPageID = entityRow.Field<int>("DefaultPageName");

            }

            return entity;
        }

        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void SaveEntity(SiteAdministrationObjectModel entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves entity details from the underlying data access and uses them to populat an entity instance.
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
        /// Retrieves entity details from the underlying data access and uses them to populat an entity instance.
        /// </summary>
        /// <param name="key">Primary key identifier of the entity to retrieve.</param>
        /// <returns>A new instance of type <typeparamref name="TModel" /> if found. Null otherwise.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override SiteAdministrationObjectModel GetEntityByKey(int key)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override IEnumerable<TEntity> GetAllEntities<TEntity>()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(int key)
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
        /// <exception cref="System.NotImplementedException"></exception>
        public override IEnumerable<TEntity> GetAllEntities<TEntity>(int startRowIndex, int maximumRows)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void SaveEntity(SiteAdministrationObjectModel entity, int modifiedBy)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(int key, int modifiedBy)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(SiteAdministrationObjectModel entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(SiteAdministrationObjectModel entity, int deletedBy)
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
        /// <exception cref="System.NotImplementedException"></exception>
        public override IEnumerable<TEntity> GetAllEntities<TEntity>(int startRowIndex, int maximumRows, Interfaces.ISortDetail<TEntity> sort)
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
        public override IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, ISortDetail<TEntity> sort,out int totalRecordCount, params int[] entitiesToIgnore)
        {
            throw new NotImplementedException();
        }
    }
}
