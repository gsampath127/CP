﻿// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By :
// Last Modified On : 11-18-2015
// ***********************************************************************
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
    /// Class ClientDocumentFactory.
    /// </summary>
    public class ClientDocumentFactory : AuditedBaseFactory<ClientDocumentObjectModel, int>
    {
        #region Constants
        /// <summary>
        /// declaration of all Strings/Procedures used as constants
        /// </summary>
        private const string SPGetAllClientDocument = "RPV2HostedAdmin_GetAllClientDocument";
        /// <summary>
        /// The sp save ClientDocument
        /// </summary>
        private const string SPSaveClientDocument = "RPV2HostedAdmin_SaveClientDocument";
        /// <summary>
        /// The sp deletes ClientDocument
        /// </summary>
        private const string SPDeleteClientDocument = "RPV2HostedAdmin_DeleteClientDocument";

        /// <summary>
        /// ClientDocumentId
        /// </summary>
        private const string DBCClientDocumentId = "ClientDocumentId";

        /// <summary>
        /// DocumentTypeId
        /// </summary>
        private const string DBCClientDocumentTypeId = "ClientDocumentTypeId";

        /// <summary>
        /// FileName
        /// </summary>
        private const string DBCFileName = "FileName";

        /// <summary>
        /// MimeType
        /// </summary>
        private const string DBCMimeType = "MimeType";

        /// <summary>
        /// IsPrivate
        /// </summary>
        private const string DBCIsPrivate = "IsPrivate";

        /// <summary>
        /// ContentUri
        /// </summary>
        private const string DBCContentUri = "ContentUri";

        /// <summary>
        /// Name
        /// </summary>
        private const string DBCName = "Name";

        /// <summary>
        /// Description
        /// </summary>
        private const string DBCDescription = "Description";

        /// <summary>
        /// Description
        /// </summary>
        private const string DBCClientDocumentTypeName = "ClientDocumentTypeName";

        /// <summary>
        /// FileData
        /// </summary>
        private const string DBCFileData = "FileData";



        #endregion
        
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientDocumentFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public ClientDocumentFactory(IDataAccess dataAccess)
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
            return this.GetEntitiesInternal<TEntity>(SPGetAllClientDocument, startRowIndex, maximumRows, null);
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
            return this.GetEntitiesInternal<TEntity>(SPGetAllClientDocument, startRowIndex, maximumRows, sort);
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

                entity.ClientDocumentId = entityRow.Field<int>(DBCClientDocumentId);
                entity.Key = entity.ClientDocumentId;
                entity.Name = entityRow.Field<string>(DBCName);
                entity.Description = entityRow.Field<string>(DBCDescription);
                entity.IsPrivate = entityRow.Field<bool>(DBCIsPrivate);
                entity.FileName = entityRow.Field<string>(DBCFileName);
                entity.MimeType = entityRow.Field<string>(DBCMimeType);
                entity.ClientDocumentTypeName = entityRow.Field<string>(DBCClientDocumentTypeName);
                entity.ClientDocumentTypeId = entityRow.Field<int>(DBCClientDocumentTypeId);
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
        public override void SaveEntity(ClientDocumentObjectModel entity)
        {
            List<DbParameter> parameters = base.GetParametersFromEntity<ClientDocumentObjectModel>(entity) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCClientDocumentId, DbType.Int32, entity.ClientDocumentId));
                parameters.Add(DataAccess.CreateParameter(DBCName, DbType.String, entity.Name));
                parameters.Add(DataAccess.CreateParameter(DBCDescription, DbType.String, entity.Description));
                parameters.Add(DataAccess.CreateParameter(DBCIsPrivate, DbType.Int32, entity.IsPrivate));
                parameters.Add(DataAccess.CreateParameter(DBCContentUri, DbType.String, entity.ContentUri));
                parameters.Add(DataAccess.CreateParameter(DBCClientDocumentTypeId, DbType.Int32, entity.ClientDocumentTypeId));
                parameters.Add(DataAccess.CreateParameter(DBCMimeType, DbType.String, entity.MimeType));
                parameters.Add(DataAccess.CreateParameter(DBCFileName, DbType.String, entity.FileName));
                parameters.Add(DataAccess.CreateParameter(DBCFileData, SqlDbType.VarBinary, entity.FileData));
                DataAccess.ExecuteNonQuery(this.ConnectionString, SPSaveClientDocument, parameters);
            }
        }

        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        /// <param name="modifiedBy">The modified by.</param>
        public override void SaveEntity(ClientDocumentObjectModel entity, int modifiedBy)
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
                parameters.Add(DataAccess.CreateParameter(DBCClientDocumentId, DbType.Int32, key));
                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeleteClientDocument, parameters);
            }
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void DeleteEntity(ClientDocumentObjectModel entity)
        {
            DeleteEntity(entity, 0);
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(ClientDocumentObjectModel entity, int deletedBy)
        {
            throw new NotImplementedException();
        }
        
    }
}