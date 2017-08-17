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

namespace RRD.FSG.RP.Model.Factories.Client
{
    /// <summary>
    /// Class SiteFactory.
    /// </summary>
    public class SiteFactory : AuditedBaseFactory<SiteObjectModel, int>
    {
        #region Constants
        /// <summary>
        /// declaration of all Strings/Procedures used as constants
        /// </summary>
        private const string SPGetAllSite = "RPV2HostedAdmin_GetAllSite";

        /// <summary>
        /// The sp save PageText
        /// </summary>
        private const string SPSaveSite = "RPV2HostedAdmin_SaveSite";
        
        /// <summary>
        /// The sp deletes PageText
        /// </summary>
        private const string SPDeleteSite = "RPV2HostedAdmin_DeleteSite";


        /// <summary>
        /// SiteId
        /// </summary>
        private const string DBCSiteId = "SiteId";
        
        /// <summary>
        /// SiteName
        /// </summary>
        private const string DBCName = "Name";
        
        /// <summary>
        /// ResourceKey
        /// </summary>
        private const string DBCTemplateId = "TemplateId";
        
        /// <summary>
        /// Text
        /// </summary>
        private const string DBCDefaultPageId = "DefaultPageId";
        
        /// <summary>
        /// Version
        /// </summary>
        private const string DBCParentSiteId = "ParentSiteId";
        
        /// <summary>
        /// DBCDescription
        /// </summary>
        private const string DBCDescription = "Description";
        
        
        /// <summary>
        /// The DBC client identifier
        /// </summary>
        private const string DBCClientId = "ClientId";
        
       
        /// <summary>
        /// The DBC is default site
        /// </summary>
        private const string DBCIsDefaultSite = "IsDefaultSite";
        
      
        /// <summary>
        /// The DBC template text data
        /// </summary>
        private const string DBCTemplateTextData = "TemplateTextData";
        
        /// <summary>
        /// The DBC template page text data
        /// </summary>
        private const string DBCTemplatePageTextData = "TemplatePageTextData";
       
        /// <summary>
        /// The DBC template navigation data
        /// </summary>
        private const string DBCTemplateNavigationData = "TemplateNavigationData";
      
        /// <summary>
        /// The DBC template page navigation data
        /// </summary>
        private const string DBCTemplatePageNavigationData = "TemplatePageNavigationData";


        #endregion


        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteAdministrationFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public SiteFactory(IDataAccess dataAccess)
            : base(dataAccess) { }
        #endregion


        /// <summary>
        /// Retrieves entity details from the underlying data access and uses them to populat an entity instance.
        /// </summary>
        /// <param name="key">Primary key identifier of the entity to retrieve.</param>
        /// <returns>A new instance of type <typeparamref name="TModel" /> if found. Null otherwise.</returns>
        public override SiteObjectModel GetEntityByKey(int key)
        {
            return GetSiteEntity(key);
        }

        /// <summary>
        /// Retrieves a specific taxonomy level entity using the passed in hierarchical level and identifier.
        /// </summary>
        /// <param name="key">The taxonomy key of the entity to retrieve. Consists of Level and TaxonomyId.</param>
        /// <returns>A <see cref="TaxonomyEntity" /> entity.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public SiteObjectModel GetSiteEntity(int key)
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
            IEnumerable <TEntity> siteDetails  = this.GetEntitiesInternal<TEntity>(SPGetAllSite, startRowIndex, maximumRows, null);

            IEnumerable<TemplatePageObjectModel> templatePageDetails = new TemplatePageFactory(DataAccess).GetAllEntities();
            
            siteDetails = siteDetails.Select(x => {   
                                                x.DefaultPageName = templatePageDetails.FirstOrDefault(t => t.PageID == x.DefaultPageId).PageName;
                                                x.PageDescription = templatePageDetails.FirstOrDefault(t => t.PageID == x.DefaultPageId).PageDescription;
                                                x.TemplateName = templatePageDetails.FirstOrDefault(t => t.PageID == x.DefaultPageId).TemplateName;
                                                return x;
                                            });
            return siteDetails;
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
            IEnumerable<TEntity> siteDetails = this.GetEntitiesInternal<TEntity>(SPGetAllSite, startRowIndex, maximumRows, sort);


            IEnumerable<TemplatePageObjectModel> templatePageDetails = new TemplatePageFactory(DataAccess).GetAllEntities();
            siteDetails = siteDetails.Select(x =>
            {
                x.DefaultPageName = templatePageDetails.FirstOrDefault(t => t.PageID == x.DefaultPageId).PageName;
                x.PageDescription = templatePageDetails.FirstOrDefault(t => t.PageID == x.DefaultPageId).PageDescription;
                x.TemplateName = templatePageDetails.FirstOrDefault(t => t.PageID == x.DefaultPageId).TemplateName;
                return x;
            });
            return siteDetails;
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
                  
                entity.SiteID = entityRow.Field<int>(DBCSiteId);
                entity.Key = entity.SiteID;
                entity.Name = entityRow.Field<string>(DBCName);
                entity.TemplateId = entityRow.Field<int>(DBCTemplateId);
                entity.DefaultPageId = entityRow.Field<int>(DBCDefaultPageId);
                entity.ParentSiteId = entityRow.Field<int?>(DBCParentSiteId);
                entity.Description = entityRow.Field<string>(DBCDescription);
                entity.IsDefaultSite = entityRow.Field<bool>(DBCIsDefaultSite);
            }

            return entity;
        }




        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        public override void SaveEntity(SiteObjectModel entity)
        {
            List<DbParameter> parameters = base.GetParametersFromEntity<SiteObjectModel>(entity) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCSiteId, DbType.Int32, entity.SiteID));
                parameters.Add(DataAccess.CreateParameter(DBCName, DbType.String, entity.Name));
                parameters.Add(DataAccess.CreateParameter(DBCTemplateId, DbType.Int32, entity.TemplateId));
                parameters.Add(DataAccess.CreateParameter(DBCDefaultPageId, DbType.Int32, entity.DefaultPageId));
                parameters.Add(DataAccess.CreateParameter(DBCParentSiteId, DbType.Int32, entity.ParentSiteId));
                parameters.Add(DataAccess.CreateParameter(DBCDescription, DbType.String, entity.Description));
                parameters.Add(DataAccess.CreateParameter(DBCClientId, DbType.Int32, entity.ClientId));
                parameters.Add(DataAccess.CreateParameter(DBCIsDefaultSite, DbType.Boolean, entity.IsDefaultSite));
                parameters.Add(DataAccess.CreateParameter(DBCTemplateTextData, SqlDbType.Structured, entity.TemplateText));
                parameters.Add(DataAccess.CreateParameter(DBCTemplatePageTextData, SqlDbType.Structured, entity.TemplatePageText));
                parameters.Add(DataAccess.CreateParameter(DBCTemplateNavigationData, SqlDbType.Structured, entity.TemplateNavigation));
                parameters.Add(DataAccess.CreateParameter(DBCTemplatePageNavigationData, SqlDbType.Structured, entity.TemplatePageNavigation));
                

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPSaveSite, parameters);
            }
        }

        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        /// <param name="modifiedBy">The modified by.</param>
        public override void SaveEntity(SiteObjectModel entity, int modifiedBy)
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
            DeleteEntity(key,0);
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
                parameters.Add(DataAccess.CreateParameter(DBCSiteId, DbType.Int32, key));
                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeleteSite, parameters);
            }
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void DeleteEntity(SiteObjectModel entity)
        {
            DeleteEntity(entity, 0);
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(SiteObjectModel entity, int deletedBy)
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
