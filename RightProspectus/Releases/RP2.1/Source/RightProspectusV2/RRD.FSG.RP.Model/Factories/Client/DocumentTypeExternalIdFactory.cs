// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-03-2015
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
    /// Class DocumentTypeExternalIdFactory.
    /// </summary>
    public class DocumentTypeExternalIdFactory : AuditedBaseFactory<DocumentTypeExternalIdObjectModel, DocumentTypeExternalIdKey>
    {
        #region Constants
        /// <summary>
        /// declaration of all Strings/Procedures used as constants
        /// </summary>
        private const string SPGetAllDocumentTypeExternalId = "RPV2HostedAdmin_GetAllDocumentTypeExternalId";
        /// <summary>
        /// The sp save DocumentTypeExternalId
        /// </summary>
        private const string SPSaveDocumentTypeExternalId = "RPV2HostedAdmin_SaveDocumentTypeExternalId";
        /// <summary>
        /// The sp deletes DocumentTypeExternalId
        /// </summary>
        private const string SPDeleteDocumentTypeExternalId = "RPV2HostedAdmin_DeleteDocumentTypeExternalId";

        /// <summary>
        /// The sp get search combo document type
        /// </summary>
        private const string SPGetSearchComboDocType = "RPV2HostedAdmin_GetSearchComboDocType";



        /// <summary>
        /// DocumentTypeId
        /// </summary>
        private const string DBCDocumentTypeId = "DocumentTypeId";
        /// <summary>
        /// DocumentTypeName
        /// </summary>
        private const string DBCDocumentTypeName = "DocumentTypeName";
        /// <summary>
        /// ExternalId
        /// </summary>
        private const string DBCExternalId = "ExternalId";
        /// <summary>
        /// IsPrimary
        /// </summary>
        private const string DBCIsPrimary = "IsPrimary";

        /// <summary>
        /// Modified Date
        /// </summary>
        private const string DBCModifiedDate = "UtcLastModified";
        /// <summary>
        /// CModified By
        /// </summary>
        private const string DBCModifiedBy = "ModifiedBy";

        #endregion


        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteAdministrationFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public DocumentTypeExternalIdFactory(IDataAccess dataAccess)
            : base(dataAccess) { }
        #endregion


        /// <summary>
        /// Retrieves entity details from the underlying data access and uses them to populate an entity instance.
        /// </summary>
        /// <param name="key">Primary key identifier of the entity to retrieve.</param>
        /// <returns>A new instance of type <typeparamref name="TModel" /> if found. Null otherwise.</returns>
        public override DocumentTypeExternalIdObjectModel GetEntityByKey(DocumentTypeExternalIdKey key)
        {
            return GetDocumentTypeExternalIdEntity(key);
        }

        /// <summary>
        /// Retrieves a specific taxonomy level entity using the passed in hierarchical level and identifier.
        /// </summary>
        /// <param name="key">The taxonomy key of the entity to retrieve. Consists of Level and TaxonomyId.</param>
        /// <returns>A <see cref="TaxonomyEntity" /> entity.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public DocumentTypeExternalIdObjectModel GetDocumentTypeExternalIdEntity(DocumentTypeExternalIdKey key)
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
            return this.GetEntitiesInternal<TEntity>(SPGetAllDocumentTypeExternalId, startRowIndex, maximumRows, null);
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
            return this.GetEntitiesInternal<TEntity>(SPGetAllDocumentTypeExternalId, startRowIndex, maximumRows, sort);
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

                entity.DocumentTypeId = entityRow.Field<int>(DBCDocumentTypeId);
                entity.ExternalId = entityRow.Field<string>(DBCExternalId);
                entity.ModifiedDate = entityRow.Field<DateTime>(DBCModifiedDate);
                entity.ModifiedBy = entityRow.Field<int>(DBCModifiedBy);
                entity.IsPrimary = entityRow.Field<bool>(DBCIsPrimary);
            }

            return entity;
        }




        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        public override void SaveEntity(DocumentTypeExternalIdObjectModel entity)
        {
            

            List<DbParameter> parameters = base.GetParametersFromEntity<DocumentTypeExternalIdObjectModel>(entity) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCDocumentTypeId, DbType.Int32, entity.DocumentTypeId));

                parameters.Add(DataAccess.CreateParameter(DBCExternalId, DbType.String, entity.ExternalId));

                parameters.Add(DataAccess.CreateParameter(DBCIsPrimary, DbType.Boolean, entity.IsPrimary));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPSaveDocumentTypeExternalId, parameters);                               

            }
                       

        }

        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        /// <param name="modifiedBy">The modified by.</param>
        public override void SaveEntity(DocumentTypeExternalIdObjectModel entity, int modifiedBy)
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
        public override TEntity GetEntityByKey<TEntity>(DocumentTypeExternalIdKey key)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        public override void DeleteEntity(DocumentTypeExternalIdKey key)
        {
            DeleteEntity(key, 0);
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        /// <param name="deletedBy">The deleted by.</param>
        public override void DeleteEntity(DocumentTypeExternalIdKey key, int deletedBy)
        {
            List<DbParameter> parameters = base.GetDeleteParameters(deletedBy) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCDocumentTypeId, DbType.Int32, key.DocumentTypeId));
                parameters.Add(DataAccess.CreateParameter(DBCExternalId, DbType.String, key.ExternalId));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeleteDocumentTypeExternalId, parameters);
            }
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void DeleteEntity(DocumentTypeExternalIdObjectModel entity)
        {
            DeleteEntity(entity, 0);
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        public override void DeleteEntity(DocumentTypeExternalIdObjectModel entity, int deletedBy)
        {
            List<DbParameter> parameters = base.GetDeleteParameters(deletedBy) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCDocumentTypeId, DbType.Int32, entity.DocumentTypeId));

                parameters.Add(DataAccess.CreateParameter(DBCExternalId, DbType.String, entity.ExternalId));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeleteDocumentTypeExternalId, parameters);
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
        public override IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, ISortDetail<TEntity> sort, out int totalRecordCount, params DocumentTypeExternalIdKey[] entitiesToIgnore)
        {
            throw new NotImplementedException();
        }

    }
}
