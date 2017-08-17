// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-03-2015
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Utilities;
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
    /// Class ClientDocumentTypeFactory.
    /// </summary>
    public class ClientDocumentTypeFactory : AuditedBaseFactory<ClientDocumentTypeObjectModel, int>
    {
        #region Constants
        /// <summary>
        /// The sp get all  ClientDocumentType
        /// </summary>
        private const string SPGetAllClientDocumentType = "RPV2HostedAdmin_GetAllClientDocumentType";

        /// <summary>
        /// The sp to delete ClientDocumentType
        /// </summary>
        private const string SPDeleteClientDocumentType = "RPV2HostedAdmin_DeleteClientDocumentType";

        /// <summary>
        /// The sp to Save ClientDocumentType
        /// </summary>
        private const string SPSaveClientDocumentType = "RPV2HostedAdmin_SaveClientDocumentType";

        /// <summary>
        /// ClientDocumentTypeId
        /// </summary>
        private const string DBCClientDocumentTypeId = "ClientDocumentTypeId";

        /// <summary>
        /// ClientDocumentTypeName
        /// </summary>
        private const string DBCClientDocumentTypeName = "Name";

        /// <summary>
        /// ClientDocumentTypeDescription
        /// </summary>
        private const string DBCClientDocumentTypeDescription = "Description";
       
        /// <summary>
        /// HostedDocumentsDisplayCount
        /// </summary>
        private const string DBCClientDocumentTypeHostedDocumentsDisplayCount = "HostedDocumentsDisplayCount";

        /// <summary>
        /// FTPName
        /// </summary>
        private const string DBCFTPName = "FTPName";

        /// <summary>
        /// FTPUsername
        /// </summary>
        private const string DBCFTPUsername = "FTPUsername";

        /// <summary>
        /// FTPPassword
        /// </summary>
        private const string DBCFTPPassword = "FTPPassword";
      
        /// <summary>
        /// IsSFTP
        /// </summary>
        private const string DBCISSFTP = "IsSFTP";
        
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public ClientDocumentTypeFactory(IDataAccess dataAccess)
            : base(dataAccess) {                
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

                entity.ClientDocumentTypeId = entityRow.Field<int>(DBCClientDocumentTypeId);
                entity.Key = entityRow.Field<int>(DBCClientDocumentTypeId);
                entity.Name = entityRow.Field<string>(DBCClientDocumentTypeName);
                entity.Description = entityRow.Field<string>(DBCClientDocumentTypeDescription);
                entity.HostedDocumentsDisplayCount = entityRow.Field<int>(DBCClientDocumentTypeHostedDocumentsDisplayCount);
                entity.FTPUsername = entityRow.Field<string>(DBCFTPUsername);
                entity.FTPPassword = entityRow.Field<string>(DBCFTPPassword);
                entity.FTPName = entityRow.Field<string>(DBCFTPName);
                entity.IsSFTP = entityRow.Field<bool>(DBCISSFTP);
              

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
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        public override IEnumerable<TEntity> GetAllEntities<TEntity>(int startRowIndex, int maximumRows)
        {
            return this.GetEntitiesInternal<TEntity>(SPGetAllClientDocumentType, startRowIndex, maximumRows,null);
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
          
            return this.GetEntitiesInternal<TEntity>(SPGetAllClientDocumentType, startRowIndex, maximumRows,sort);
            
        }


        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        public override void SaveEntity(ClientDocumentTypeObjectModel entity)
        {

            List<DbParameter> parameters = base.GetParametersFromEntity<ClientDocumentTypeObjectModel>(entity) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCClientDocumentTypeId, DbType.Int32, entity.ClientDocumentTypeId));

                parameters.Add(DataAccess.CreateParameter(DBCClientDocumentTypeName, DbType.String, entity.Name));

                parameters.Add(DataAccess.CreateParameter(DBCClientDocumentTypeDescription, DbType.String, entity.Description));
                parameters.Add(DataAccess.CreateParameter(DBCClientDocumentTypeHostedDocumentsDisplayCount, DbType.Int16, entity.HostedDocumentsDisplayCount));
                parameters.Add(DataAccess.CreateParameter(DBCFTPName, DbType.String, entity.FTPName));
                parameters.Add(DataAccess.CreateParameter(DBCFTPUsername, DbType.String, entity.FTPUsername));
                parameters.Add(DataAccess.CreateParameter(DBCFTPPassword, DbType.String, string.IsNullOrWhiteSpace(entity.FTPPassword) ? null : EmailHelper.EncodePassword(entity.FTPPassword)));
                parameters.Add(DataAccess.CreateParameter(DBCISSFTP, DbType.String, entity.IsSFTP));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPSaveClientDocumentType, parameters);

            }
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        public override void DeleteEntity(int key)
        {
            this.DeleteEntity(key, 0);

        }

        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        /// <param name="modifiedBy">The modified by.</param>
        public override void SaveEntity(ClientDocumentTypeObjectModel entity, int modifiedBy)
        {
            base.SetModifiedBy(entity, modifiedBy);
            this.SaveEntity(entity);
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
                parameters.Add(DataAccess.CreateParameter(DBCClientDocumentTypeId, DbType.Int32, key));
                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeleteClientDocumentType, parameters);
            }
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="System.NotImplementedException"></exception>
           public override void DeleteEntity(ClientDocumentTypeObjectModel entity)
        {
            throw new NotImplementedException();
        }

           /// <summary>
           /// Deletes the entity associated with the key from the data store.
           /// </summary>
           /// <param name="entity">The entity.</param>
           /// <param name="deletedBy">The deleted by.</param>
           /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(ClientDocumentTypeObjectModel entity, int deletedBy)
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

    }
}
