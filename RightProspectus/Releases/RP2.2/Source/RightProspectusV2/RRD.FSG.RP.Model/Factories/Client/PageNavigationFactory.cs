// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-13-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.Interfaces;
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
    /// Class PageNavigationFactory.
    /// </summary>
    public class PageNavigationFactory : AuditedBaseFactory<PageNavigationObjectModel, PageNavigationKey> 
    {
        #region Constants
        /// <summary>
        /// declaration of all Strings/Procedures used as constants
        /// </summary>
        private const string SPGetAllPageNavigation = "RPV2HostedAdmin_GetAllPageNavigation";
        /// <summary>
        /// The sp save PageNavigation
        /// </summary>
        private const string SPSavePageNavigation = "RPV2HostedAdmin_SavePageNavigation";
        /// <summary>
        /// The sp Delete PageNavigation
        /// </summary>
        private const string SPDeletePageNavigation = "RPV2HostedAdmin_DeletePageNavigation";

        /// <summary>
        /// PageNavigationId
        /// </summary>
        private const string DBCPageNavigationId = "PageNavigationId";
        /// <summary>
        /// PageID
        /// </summary>
        private const string DBCPageID = "PageId";
        /// <summary>
        /// SITEID
        /// </summary>
        private const string DBCSITEID = "SiteId";
        /// <summary>
        /// NavigationKey
        /// </summary>
        private const string DBCNavigationKey = "NavigationKey";

        /// <summary>
        /// The DBC navigation XML
        /// </summary>
        private const string DBCNavigationXML = "NavigationXML";
        /// <summary>
        /// CurrentVersion
        /// </summary>
        private const string DBCVersion = "Version";
        /// <summary>
        /// ModifiedBy
        /// </summary>
        private const string DBCModifiedBy = "ModifiedBy";
        /// <summary>
        /// IsProofing
        /// </summary>
        private const string DBCIsProofing = "IsProofing";
        /// <summary>
        /// IsProofingAvailableForPageTextID
        /// </summary>
        private const string DBCIsProofingAvailableForPageNavigationID = "IsProofingAvailableForPageNavigationID";
        /// <summary>
        /// LanguageCulture
        /// </summary>
        private const string DBCLanguageCulture = "LanguageCulture";

        #endregion
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditedBaseFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public PageNavigationFactory(IDataAccess dataAccess)
            :base(dataAccess){ }

        /// <summary>
        /// Retrieves entity details from the underlying data access and uses them to populate an entity instance.
        /// </summary>
        /// <param name="key">Primary key identifier of the entity to retrieve.</param>
        /// <returns>A new instance of type <typeparamref name="TModel" /> if found. Null otherwise.</returns>
        public override PageNavigationObjectModel GetEntityByKey(PageNavigationKey key)
        {
            return GetPageNavigationEntity(key);
            
        }
        /// <summary>
        /// Gets the page navigation entity.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>PageNavigationObjectModel.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public PageNavigationObjectModel GetPageNavigationEntity(PageNavigationKey key)
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
        public override IEnumerable<TEntity> GetAllEntities<TEntity>(int startRowIndex, int maximumRows, Interfaces.ISortDetail<TEntity> sort)
        {
            IEnumerable<TEntity> pageNavigationDetails = this.GetEntitiesInternal<TEntity>(SPGetAllPageNavigation, startRowIndex, maximumRows, null);

            IEnumerable<TemplatePageObjectModel> templatePageDetails = new TemplatePageFactory(DataAccess).GetAllEntities();

            pageNavigationDetails = pageNavigationDetails.Select(x =>
            {
                x.PageName = x.PageId == 0 ? "" : templatePageDetails.FirstOrDefault(t => t.PageID == x.PageId).PageName;
                x.PageDescription = x.PageId == 0 ? "" : templatePageDetails.FirstOrDefault(t => t.PageID == x.PageId).PageDescription;
                return x;
            });
            return pageNavigationDetails;
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
        public override IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, ISortDetail<TEntity> sort, params PageNavigationKey[] entitiesToIgnore)
        {
            IEnumerable<TEntity> pageNavigationDetails = this.GetEntitiesInternal<TEntity>(SPGetAllPageNavigation, startRowIndex, maximumRows, sort);

            IEnumerable<TemplatePageObjectModel> templatePageDetails = new TemplatePageFactory(DataAccess).GetAllEntities();

            pageNavigationDetails = pageNavigationDetails.Select(x =>
            {
                x.PageName = x.PageId == 0 ? "" : templatePageDetails.FirstOrDefault(t => t.PageID == x.PageId).PageName;
                x.PageDescription = x.PageId == 0 ? "" : templatePageDetails.FirstOrDefault(t => t.PageID == x.PageId).PageDescription;
                return x;
            });
            return pageNavigationDetails;
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
        public override IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, ISortDetail<TEntity> sort, out int totalRecordCount, params PageNavigationKey[] entitiesToIgnore)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Instantiates a new entity and populates members with the data record passed in.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to create.</typeparam>
        /// <param name="entityRow">The entity row.</param>
        /// <returns>A new entity of type <typeparamref name="TEntity" />.</returns>
        public override TEntity CreateEntity<TEntity>(DataRow entityRow)
        {
            TEntity entity = base.CreateEntity<TEntity>(entityRow);
            if(entity!=null)
            {
               
                entity.PageNavigationId = entityRow.Field<int>(DBCPageNavigationId);
                entity.PageId = entityRow.Field<int>(DBCPageID);
                entity.SiteId = entityRow.Field<int>(DBCSITEID);
                entity.NavigationKey = entityRow.Field<string>(DBCNavigationKey);
                entity.ModifiedBy = entityRow.Field<int>(DBCModifiedBy);
                entity.Version = entityRow.Field<int>(DBCVersion);
                entity.NavigationXML = entityRow.Field<string>(DBCNavigationXML);
                entity.IsProofing = entityRow.Field<bool>(DBCIsProofing);
                entity.IsProofingAvailableForPageNavigationID = entityRow.Field<bool>(DBCIsProofingAvailableForPageNavigationID);
            }
            return entity;
        }


        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        public override void SaveEntity(PageNavigationObjectModel entity)
        {
            List<DbParameter> parameters = base.GetParametersFromEntity<PageNavigationObjectModel>(entity) as List<DbParameter>;
           
            if(parameters!=null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCPageNavigationId, DbType.Int32, entity.PageNavigationId));
                parameters.Add(DataAccess.CreateParameter(DBCPageID,DbType.Int32,entity.PageId));
                parameters.Add(DataAccess.CreateParameter(DBCSITEID,DbType.Int32,entity.SiteId));
                parameters.Add(DataAccess.CreateParameter(DBCNavigationKey, DbType.String, entity.NavigationKey));
                parameters.Add(DataAccess.CreateParameter(DBCNavigationXML, DbType.Xml, entity.NavigationXML));
                parameters.Add(DataAccess.CreateParameter(DBCVersion, DbType.Int32, entity.Version));                
                parameters.Add(DataAccess.CreateParameter(DBCIsProofing, DbType.Boolean, entity.IsProofing));                
                DataAccess.ExecuteNonQuery(this.ConnectionString, SPSavePageNavigation, parameters);


            }
        }

        /// <summary>
        /// Saves the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="modifiedby">The modifiedby.</param>
        public override void SaveEntity(PageNavigationObjectModel entity,int modifiedby)
        {
            base.SetModifiedBy(entity,modifiedby);
            this.SaveEntity(entity);
        }

        /// <summary>
        /// Retrieves entity details from the underlying data access and uses them to populate an entity instance.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="key">Primary key identifier of the entity to retrieve.</param>
        /// <returns>A new instance of type <typeparamref name="TModel" /> if found. Null otherwise.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override TEntity GetEntityByKey<TEntity>(PageNavigationKey key)
        {
            throw new NotImplementedException();
        }





        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        public override void DeleteEntity(PageNavigationKey key)
        {
            DeleteEntity(key, 0);
        }
        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        /// <param name="deletedBy">The deleted by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(PageNavigationKey key, int deletedBy)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void DeleteEntity(PageNavigationObjectModel entity)
        {
            DeleteEntity(entity, 0);
        }
        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        public override void DeleteEntity(PageNavigationObjectModel entity, int deletedBy)
        {
            List<DbParameter> parameters = base.GetDeleteParameters(deletedBy) as List<DbParameter>;

            if(parameters!=null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCPageNavigationId, DbType.Int32, entity.PageNavigationId));
                parameters.Add(DataAccess.CreateParameter(DBCVersion, DbType.Int32, entity.Version));
                parameters.Add(DataAccess.CreateParameter(DBCIsProofing, DbType.Boolean, entity.IsProofing));             
                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeletePageNavigation, parameters);
            }
        }
    }
}
