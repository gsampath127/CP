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
    /// Class ClientDocumentGroupFactory.
    /// </summary>
    public class ClientDocumentGroupFactory : AuditedBaseFactory<ClientDocumentGroupObjectModel, int>
    {

        #region Constants
        /// <summary>
        /// declaration of all Strings/Procedures used as constants
        /// </summary>
        private const string SPGetAllClientDocumentGroup = "RPV2HostedAdmin_GetAllClientDocumentGroup";
        /// <summary>
        /// The sp save ClientDocumentGroup
        /// </summary>
        private const string SPSaveClientDocumentGroup = "RPV2HostedAdmin_SaveClientDocumentGroup";
        /// <summary>
        /// The sp deletes ClientDocumentGroup
        /// </summary>
        private const string SPDeleteClientDocumentGroup = "RPV2HostedAdmin_DeleteClientDocumentGroup";

        /// <summary>
        /// ClientDocumentGroupId
        /// </summary>
        private const string DBCClientDocumentGroupId = "ClientDocumentGroupId";

        /// <summary>
        /// ClientDocumentId
        /// </summary>
        private const string DBCClientDocumentId = "ClientDocumentId";

        /// <summary>
        /// ParentClientDocumentGroupId
        /// </summary>
        private const string DBCParentClientDocumentGroupId = "ParentClientDocumentGroupId";

        /// <summary>
        /// CssClass
        /// </summary>
        private const string DBCCssClass = "CssClass";

        /// <summary>
        /// Name
        /// </summary>
        private const string DBCName = "Name";

        /// <summary>
        /// Description
        /// </summary>
        private const string DBCDescription = "Description";

        /// <summary>
        /// The database modified by
        /// </summary>
        private const string DBCModifiedBy = "ModifiedBy";

        /// <summary>
        /// The database modified by
        /// </summary>
        private const string DBCLastModified = "LastModified";

        /// <summary>
        /// The database cuts modified date
        /// </summary>
        private const string DBCUtcModifiedDate = "UtcLastModified";

        /// <summary>
        /// FileName
        /// </summary>
        private const string DBCFileName = "FileName";

        /// <summary>
        /// MimeType
        /// </summary>
        private const string DBCMimeType = "MimeType";

        /// <summary>
        /// The UserRoles
        /// </summary>
        private const string DBCClientDocumentGroupClientDocument = "ClientDocumentGroupClientDocument";

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientDocumentGroupFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public ClientDocumentGroupFactory(IDataAccess dataAccess)
            : base(dataAccess) { }
        #endregion

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
                entity.ClientDocumentGroupId = entityRow.Field<int>(DBCClientDocumentGroupId);
                entity.Key = entity.ClientDocumentGroupId;
                entity.Name = entityRow.Field<string>(DBCName);
                entity.Description = entityRow.Field<string>(DBCDescription);
                entity.ParentClientDocumentGroupId = entityRow.Field<int>(DBCParentClientDocumentGroupId);
                entity.CssClass = entityRow.Field<string>(DBCCssClass);
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
        /// <param name="sort">The sort.</param>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        public override IEnumerable<TEntity> GetAllEntities<TEntity>(int startRowIndex, int maximumRows, Interfaces.ISortDetail<TEntity> sort)
        {
            return this.GetEntitiesInternal<TEntity>(SPGetAllClientDocumentGroup, startRowIndex, maximumRows, sort);
        }

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <typeparam name="ClientDocumentGroupObjectModel">The type of the client document group object model.</typeparam>
        /// <returns>IEnumerable&lt;ClientDocumentGroupObjectModel&gt;.</returns>
        public override IEnumerable<ClientDocumentGroupObjectModel> GetAllEntities<ClientDocumentGroupObjectModel>()
        {
            DataTable clientDocumentGroupDatatable = this.DataAccess.ExecuteDataTable(this.ConnectionString, SPGetAllClientDocumentGroup);

            List<ClientDocumentGroupObjectModel> clientDocumentGroupObjectModels = new List<ClientDocumentGroupObjectModel>();

            int CurrentClientDocumentGroupId = -1;

            ClientDocumentGroupObjectModel clientDocumentGroupObjectModel = null;

            foreach (DataRow row in clientDocumentGroupDatatable.Rows)
            {
                if (CurrentClientDocumentGroupId != row.Field<int>(DBCClientDocumentGroupId))
                {
                    if (clientDocumentGroupObjectModel != null)
                    {
                        clientDocumentGroupObjectModels.Add(clientDocumentGroupObjectModel);
                    }

                    clientDocumentGroupObjectModel = new ClientDocumentGroupObjectModel();

                    clientDocumentGroupObjectModel.ClientDocumentGroupId = row.Field<int>(DBCClientDocumentGroupId);

                    clientDocumentGroupObjectModel.Key = clientDocumentGroupObjectModel.ClientDocumentGroupId;

                    CurrentClientDocumentGroupId = clientDocumentGroupObjectModel.ClientDocumentGroupId;

                    clientDocumentGroupObjectModel.ClientDocumentGroupId = row.Field<int>(DBCClientDocumentGroupId);

                    clientDocumentGroupObjectModel.Name = row.Field<string>(DBCName);

                    clientDocumentGroupObjectModel.Description = row.Field<string>(DBCDescription);

                    clientDocumentGroupObjectModel.CssClass = row.Field<string>(DBCCssClass);

                    clientDocumentGroupObjectModel.ParentClientDocumentGroupId = row.Field<int?>(DBCParentClientDocumentGroupId);

                    clientDocumentGroupObjectModel.ModifiedBy = row.Field<int>(DBCModifiedBy);

                    clientDocumentGroupObjectModel.LastModified = row.Field<DateTime>(DBCUtcModifiedDate);

                    clientDocumentGroupObjectModel.ClientDocuments = new List<ClientDocumentObjectModel>();

                }
                if (row.Field<int?>(DBCClientDocumentId) != null)
                {
                    if (!clientDocumentGroupObjectModel.ClientDocuments.Exists(p => p.ClientDocumentId == row.Field<int>(DBCClientDocumentId)))
                    {
                        clientDocumentGroupObjectModel.ClientDocuments.Add(new ClientDocumentObjectModel { ClientDocumentId = row.Field<int>(DBCClientDocumentId), FileName = row.Field<string>(DBCFileName), MimeType = row.Field<string>(DBCMimeType) });
                    }
                }
            }

            if (clientDocumentGroupObjectModel != null)
            {
                clientDocumentGroupObjectModels.Add(clientDocumentGroupObjectModel);
            }

            return clientDocumentGroupObjectModels;
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
        public override void SaveEntity(ClientDocumentGroupObjectModel entity, int modifiedBy)
        {
            base.SetModifiedBy(entity, modifiedBy);
            this.SaveEntity(entity);
        }

        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        public override void SaveEntity(ClientDocumentGroupObjectModel entity)
        {
            var clientDocumentGroupClientDocuments = new DataTable();
            clientDocumentGroupClientDocuments.Columns.Add("ClientDocumentGroupId", typeof(int));
            clientDocumentGroupClientDocuments.Columns.Add("ClientDocumentId", typeof(int));
            clientDocumentGroupClientDocuments.Columns.Add("Order", typeof(int));

            if (entity.ClientDocuments != null)
            {
                entity.ClientDocuments.ForEach(item =>
                {
                    clientDocumentGroupClientDocuments.Rows.Add(entity.ClientDocumentGroupId, item.ClientDocumentId, item.Order);
                });
            }

            List<DbParameter> parameters = base.GetParametersFromEntity<ClientDocumentGroupObjectModel>(entity) as List<DbParameter>;

            if (parameters != null)
            {

                parameters.Add(DataAccess.CreateParameter(DBCClientDocumentGroupId, DbType.Int32, entity.ClientDocumentGroupId));

                parameters.Add(DataAccess.CreateParameter(DBCName, DbType.String, entity.Name));

                parameters.Add(DataAccess.CreateParameter(DBCDescription, DbType.String, entity.Description));

                parameters.Add(DataAccess.CreateParameter(DBCParentClientDocumentGroupId, DbType.Int32, entity.ParentClientDocumentGroupId));

                parameters.Add(DataAccess.CreateParameter(DBCCssClass, DbType.String, entity.CssClass));

                parameters.Add(DataAccess.CreateParameter(DBCClientDocumentGroupClientDocument, SqlDbType.Structured, clientDocumentGroupClientDocuments));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPSaveClientDocumentGroup, parameters);
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
                parameters.Add(DataAccess.CreateParameter(DBCClientDocumentGroupId, DbType.Int32, key));
                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeleteClientDocumentGroup, parameters);
            }
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void DeleteEntity(ClientDocumentGroupObjectModel entity)
        {
            DeleteEntity(entity, 0);
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(ClientDocumentGroupObjectModel entity, int deletedBy)
        {
            throw new NotImplementedException();
        }
    }
}
