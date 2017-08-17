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
    /// Class PageTextFactory.
    /// </summary>
    public class PageTextFactory : AuditedBaseFactory<PageTextObjectModel, PageTextKey>
    {
        #region Constants
        /// <summary>
        /// declaration of all Strings/Procedures used as constants
        /// </summary>
        private const string SPGetAllPageText = "RPV2HostedAdmin_GetAllPageText";
        /// <summary>
        /// The sp save PageText
        /// </summary>
        private const string SPSavePageText = "RPV2HostedAdmin_SavePageText";
        /// <summary>
        /// The sp deletes PageText
        /// </summary>
        private const string SPDeletePageText = "RPV2HostedAdmin_DeletePageText";



        /// <summary>
        /// PageTextId
        /// </summary>
        private const string DBCPageTextId = "PageTextId";
        /// <summary>
        /// Version
        /// </summary>
        private const string DBCVersion = "Version";
        /// <summary>
        /// PageID
        /// </summary>
        private const string DBCPageID = "PageID";
        /// <summary>
        /// TemplateId
        /// </summary>
        private const string DBCTemplateId = "TemplateId";
        /// <summary>
        /// SITEID
        /// </summary>
        private const string DBCSITEID = "SiteId";
        /// <summary>
        /// SiteName
        /// </summary>
        private const string DBCSiteName = "SiteName";
        /// <summary>
        /// ResourceKey
        /// </summary>
        private const string DBCResourceKey = "ResourceKey";
        /// <summary>
        /// Text
        /// </summary>
        private const string DBCText = "Text";
        /// <summary>
        /// IsProofing
        /// </summary>
        private const string DBCIsProofing = "IsProofing";
        /// <summary>
        /// IsProofingAvailableForPageTextID
        /// </summary>
        private const string DBCIsProofingAvailableForPageTextID = "IsProofingAvailableForPageTextID";
        /// <summary>
        /// LanguageCulture
        /// </summary>
        private const string DBCLanguageCulture = "LanguageCulture";

        #endregion


        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="PageTextFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public PageTextFactory(IDataAccess dataAccess)
            : base(dataAccess) { }
        #endregion


        /// <summary>
        /// Retrieves entity details from the underlying data access and uses them to populate an entity instance.
        /// </summary>
        /// <param name="key">Primary key identifier of the entity to retrieve.</param>
        /// <returns>A new instance of type <typeparamref name="TModel" /> if found. Null otherwise.</returns>
        public override PageTextObjectModel GetEntityByKey(PageTextKey key)
        {
            return GetPageTextEntity(key);
        }

        /// <summary>
        /// Retrieves a specific taxonomy level entity using the passed in hierarchical level and identifier.
        /// </summary>
        /// <param name="key">The taxonomy key of the entity to retrieve. Consists of Level and TaxonomyId.</param>
        /// <returns>A <see cref="TaxonomyEntity" /> entity.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public PageTextObjectModel GetPageTextEntity(PageTextKey key)
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
            IEnumerable<TEntity> pageTextDetails = this.GetEntitiesInternal<TEntity>(SPGetAllPageText, startRowIndex, maximumRows,null);

            IEnumerable<TemplatePageObjectModel> templatePageDetails = new TemplatePageFactory(DataAccess).GetAllEntities();

            pageTextDetails = pageTextDetails.Select(x =>
            {
                x.PageName = templatePageDetails.FirstOrDefault(t => t.PageID == x.PageID).PageName;
                x.PageDescription = templatePageDetails.FirstOrDefault(t => t.PageID == x.PageID).PageDescription;                
                return x;
            });
            return pageTextDetails;
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
            IEnumerable<TEntity> pageTextDetails = this.GetEntitiesInternal<TEntity>(SPGetAllPageText, startRowIndex, maximumRows, sort);

            IEnumerable<TemplatePageObjectModel> templatePageDetails = new TemplatePageFactory(DataAccess).GetAllEntities();

            pageTextDetails = pageTextDetails.Select(x =>
            {
                x.PageName = templatePageDetails.FirstOrDefault(t => t.PageID == x.PageID).PageName;
                x.PageDescription = templatePageDetails.FirstOrDefault(t => t.PageID == x.PageID).PageDescription;
                return x;
            });
            return pageTextDetails;
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

                entity.PageTextID = entityRow.Field<int>(DBCPageTextId);
                entity.Version = entityRow.Field<int>(DBCVersion);
                entity.PageID = entityRow.Field<int>(DBCPageID);
                entity.TemplateID = entityRow.Field<int>(DBCTemplateId);
                entity.SiteID = entityRow.Field<int>(DBCSITEID);
                entity.SiteName = entityRow.Field<string>(DBCSiteName);
                entity.ResourceKey = entityRow.Field<string>(DBCResourceKey);
                entity.Text = entityRow.Field<string>(DBCText);
                entity.IsProofing = entityRow.Field<bool>(DBCIsProofing);
                entity.IsProofingAvailableForPageTextId = entityRow.Field<bool>(DBCIsProofingAvailableForPageTextID);
            }

            return entity;
        }




        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        public override void SaveEntity(PageTextObjectModel entity)
        {
            List<DbParameter> parameters = base.GetParametersFromEntity<PageTextObjectModel>(entity) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCPageTextId, DbType.Int32, entity.PageTextID));
                parameters.Add(DataAccess.CreateParameter(DBCVersion, DbType.Int32, entity.Version));
                parameters.Add(DataAccess.CreateParameter(DBCPageID, DbType.Int32, entity.PageID));
                parameters.Add(DataAccess.CreateParameter(DBCSITEID, DbType.Int32, entity.SiteID));
                parameters.Add(DataAccess.CreateParameter(DBCResourceKey, DbType.String, entity.ResourceKey));
                parameters.Add(DataAccess.CreateParameter(DBCText, DbType.String, entity.Text));
                parameters.Add(DataAccess.CreateParameter(DBCIsProofing, DbType.Boolean, entity.IsProofing));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPSavePageText, parameters);
            }
           
        }

        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        /// <param name="modifiedBy">The modified by.</param>
        public override void SaveEntity(PageTextObjectModel entity, int modifiedBy)
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
        public override TEntity GetEntityByKey<TEntity>(PageTextKey key)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        public override void DeleteEntity(PageTextKey key)
        {
            DeleteEntity(key, 0);
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        /// <param name="deletedBy">The deleted by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(PageTextKey key, int deletedBy)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void DeleteEntity(PageTextObjectModel entity)
        {
            DeleteEntity(entity, 0);
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        public override void DeleteEntity(PageTextObjectModel entity, int deletedBy)
        {
            List<DbParameter> parameters = base.GetDeleteParameters(deletedBy) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCPageTextId, DbType.Int32, entity.PageTextID));
                parameters.Add(DataAccess.CreateParameter(DBCVersion, DbType.Int32, entity.Version));
                parameters.Add(DataAccess.CreateParameter(DBCIsProofing, DbType.Boolean, entity.IsProofing));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeletePageText, parameters);
            }
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
        public override IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, ISortDetail<TEntity> sort, out int totalRecordCount, params PageTextKey[] entitiesToIgnore)
        {
            throw new NotImplementedException();
        }
    }
}
