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
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

/// <summary>
/// The Client namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Factories.Client
{
    /// <summary>
    /// Class DocumentTypeAssociationFactory.
    /// </summary>
    public class DocumentTypeAssociationFactory: AuditedBaseFactory<DocumentTypeAssociationObjectModel, int>
    {
        #region Constants
        /// <summary>
        /// declaration of all Strings/Procedures used as constants
        /// </summary>
        private const string SPGetAllDocumentTypeAssociation = "RPV2HostedAdmin_GetAllDocumentTypeAssociation";
        /// <summary>
        /// The sp save DocumentTypeAssociation
        /// </summary>
        private const string SPSaveDocumentTypeAssociation = "RPV2HostedAdmin_SaveDocumentTypeAssociation";
        /// <summary>
        /// The sp deletes DocumentTypeAssociation
        /// </summary>
        private const string SPDeleteDocumentTypeAssociation = "RPV2HostedAdmin_DeleteDocumentTypeAssociation";

        /// <summary>
        /// DocumentTypeAssociationId
        /// </summary>
        private const string DBCDocumentTypeAssociationId = "DocumentTypeAssociationId";
        /// <summary>
        /// DocumentTypeId
        /// </summary>
        private const string DBCDocumentTypeId = "DocumentTypeId";
        /// <summary>
        /// SiteId
        /// </summary>
        private const string DBCSiteId = "SiteId";
        /// <summary>
        /// TaxonomyAssociationId
        /// </summary>
        private const string DBCTaxonomyAssociationId = "TaxonomyAssociationId";
        /// <summary>
        /// Order
        /// </summary>
        private const string DBCOrder = "Order";
        /// <summary>
        /// HeaderText
        /// </summary>
        private const string DBCHeaderText = "HeaderText";
        /// <summary>
        /// LinkText
        /// </summary>
        private const string DBCLinkText = "LinkText";
        /// <summary>
        /// DescriptionOverride
        /// </summary>
        private const string DBCDescriptionOverride = "DescriptionOverride";
        /// <summary>
        /// CssClass
        /// </summary>
        private const string DBCCssClass = "CssClass";
        /// <summary>
        /// MarketId
        /// </summary>
        private const string DBCMarketId = "MarketId";
        /// <summary>
        /// IsProofing
        /// </summary>
        private const string DBCIsProofing = "IsProofing";
        


        #endregion
        
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentTypeAssociationFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public DocumentTypeAssociationFactory(IDataAccess dataAccess)
            : base(dataAccess) { }
        #endregion

        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        public override IEnumerable<TEntity> GetAllEntities<TEntity>(int startRowIndex, int maximumRows)
        {
            return this.GetEntitiesInternal<TEntity>(SPGetAllDocumentTypeAssociation, startRowIndex, maximumRows, null);
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
            return this.GetEntitiesInternal<TEntity>(SPGetAllDocumentTypeAssociation, startRowIndex, maximumRows, sort);
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

                entity.DocumentTypeAssociationId = entityRow.Field<int>(DBCDocumentTypeAssociationId);
                entity.Key = entity.DocumentTypeAssociationId;
                entity.DocumentTypeId = entityRow.Field<int>(DBCDocumentTypeId);
                entity.SiteId = entityRow.Field<int?>(DBCSiteId);
                entity.TaxonomyAssociationId = entityRow.Field<int?>(DBCTaxonomyAssociationId);
                entity.Order = entityRow.Field<int?>(DBCOrder);
                entity.HeaderText = entityRow.Field<string>(DBCHeaderText);
                entity.LinkText = entityRow.Field<string>(DBCLinkText);
                entity.DescriptionOverride = entityRow.Field<string>(DBCDescriptionOverride);
                entity.CssClass = entityRow.Field<string>(DBCCssClass);
                entity.MarketId = entityRow.Field<string>(DBCMarketId);
                entity.IsProofing = entityRow.Field<bool>(DBCIsProofing);

            }

            return entity;
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

        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        /// <param name="modifiedBy">The modified by.</param>
        public override void SaveEntity(DocumentTypeAssociationObjectModel entity, int modifiedBy)
        {
            List<DbParameter> parameters = base.GetParametersFromEntity<DocumentTypeAssociationObjectModel>(entity) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCDocumentTypeAssociationId, DbType.Int32, entity.DocumentTypeAssociationId));
                parameters.Add(DataAccess.CreateParameter(DBCDocumentTypeId, DbType.Int32, entity.DocumentTypeId));
                parameters.Add(DataAccess.CreateParameter(DBCSiteId, DbType.Int32, entity.SiteId));
                parameters.Add(DataAccess.CreateParameter(DBCTaxonomyAssociationId, DbType.Int32, entity.TaxonomyAssociationId));
                parameters.Add(DataAccess.CreateParameter(DBCOrder, DbType.Int32, entity.Order));
                parameters.Add(DataAccess.CreateParameter(DBCHeaderText, DbType.String, entity.HeaderText));
                parameters.Add(DataAccess.CreateParameter(DBCLinkText, DbType.String, entity.LinkText));
                parameters.Add(DataAccess.CreateParameter(DBCDescriptionOverride, DbType.String, entity.DescriptionOverride));
                parameters.Add(DataAccess.CreateParameter(DBCCssClass, DbType.String, entity.CssClass));
                parameters.Add(DataAccess.CreateParameter(DBCMarketId, DbType.String, entity.MarketId));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPSaveDocumentTypeAssociation, parameters);
            }
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
                parameters.Add(DataAccess.CreateParameter(DBCDocumentTypeAssociationId, DbType.Int32, key));
                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeleteDocumentTypeAssociation, parameters);
            }
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void DeleteEntity(DocumentTypeAssociationObjectModel entity)
        {
            DeleteEntity(entity, 0);
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(DocumentTypeAssociationObjectModel entity, int deletedBy)
        {
            throw new NotImplementedException();
        }
        
    }
}
