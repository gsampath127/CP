// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-03-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;


namespace RRD.FSG.RP.Model.Factories.System
{
    /// <summary>
    /// Class ClientFactory.
    /// </summary>
    public class ClientFactory: AuditedBaseFactory<ClientObjectModel,int>
    {
        #region Constants
        /// <summary>
        /// The sp save client
        /// </summary>
        private const string SPSaveClient = "RPV2HostedAdmin_SaveClient";
        /// <summary>
        /// The sp delete client
        /// </summary>
        private const string SPDeleteClient = "RPV2HostedAdmin_DeleteClient";
        /// <summary>
        /// The sp get clients
        /// </summary>
        private const string SPGetClients = "RPV2HostedAdmin_GetClients";
        /// <summary>
        /// The sp get clients
        /// </summary>
        private const string SPGetAllClients = "RPV2HostedAdmin_GetAllClients";

        /// <summary>
        /// The sp get client by identifier
        /// </summary>
        private const string SPGetClientById = "RPV2HostedAdmin_GetClientById";
        /// <summary>
        /// The Database Column client identifier
        /// </summary>
        private const string DBCClientId = "clientId";
        /// <summary>
        /// The Database Column client name
        /// </summary>
        private const string DBCClientName = "clientName";
        /// <summary>
        /// The Database Column client name
        /// </summary>
        private const string DBCClientDescription = "clientDescription";
        /// <summary>
        /// The Database Column connection string
        /// </summary>
        private const string DBCClientConnectionStringName = "ClientConnectionStringName";
        /// <summary>
        /// The Database Column database name
        /// </summary>
        private const string DBCClientDatabaseName = "ClientDatabaseName";
        /// <summary>
        /// The Database Column database name
        /// </summary>
        private const string DBCVerticalMarketConnectionStringName = "VerticalMarketConnectionStringName";
        /// <summary>
        /// The Database Column database name
        /// </summary>
        private const string DBCVerticalMarketDatabaseName = "VerticalMarketDatabaseName";
        /// <summary>
        /// The Database Column vertical market identifier
        /// </summary>
        private const string DBCVerticalMarketId = "verticalMarketId";
        /// <summary>
        /// The Database Column vertical market identifier
        /// </summary>
        private const string DBCVerticalMarketName = "verticalMarketName";
        /// <summary>
        /// The database modified by
        /// </summary>
        private const string DBCmodifiedBy = "modifiedBy";
        /// <summary>
        /// The database utc modified date
        /// </summary>
        private const string DBCutcModifiedDate = "utcModifiedDate";
        /// <summary>
        /// The page size for procedure
        /// </summary>
        private const string DBCPageSize = "pageSize";
        /// <summary>
        /// The  page index for procedure
        /// </summary>
        private const string DBCPageIndex = "pageIndex";
        /// <summary>
        /// The sort direction for procedure
        /// </summary>
        private const string DBCSortDirection = "sortDirection";
        /// <summary>
        /// The  sort column for procedure
        /// </summary>
        private const string DBCSortColumn = "sortColumn";
        /// <summary>
        /// The database column description
        /// </summary>
        private const string DBCDescription = "Description";
        /// <summary>
        /// The database column ClientDnsId
        /// </summary>
        private const string DBCClientDnsId = "ClientDnsId";
        /// <summary>
        /// The database column UserId
        /// </summary>
        private const string DBCUserId = "UserId";
        /// <summary>
        /// The database column Dns
        /// </summary>
        private const string DBCClientDnsSiteId = "ClientDnsSiteId";
        /// <summary>
        /// The database column Dns
        /// </summary>
        private const string DBCDns = "Dns";
        /// <summary>
        /// DBCLastModified
        /// </summary>
        private const string DBCLastModified = "UtcLastModified";
        /// <summary>
        /// The ClientUsers
        /// </summary>
        private const string DBCClientUsers = "ClientUsers";
        #endregion



        /// <summary>
        /// Initializes a new instance of the <see cref="ClientFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public ClientFactory(IDataAccess dataAccess)
            : base(dataAccess) {
                this.ConnectionString = DBConnectionString.SystemDBConnectionString();
        }




        /// <summary>
        /// Retrieves entity details from the underlying data access and uses them to populate an entity instance.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to retrieve.</typeparam>
        /// <param name="key">Primary key identifier of the entity to retrieve.</param>
        /// <returns>A new instance of type <typeparamref name="TEntity" /> if found. Null otherwise.</returns>
        public override TEntity GetEntityByKey<TEntity>(int key)
        {
            TEntity clientDataModel = null;
            var results = DataAccess.ExecuteDataTable(this.ConnectionString, SPGetClientById,
                            DataAccess.CreateParameter(DBCClientId, DbType.Int32, key)
               );

            if (results.Rows.Count > 0)
            {
                clientDataModel = CreateEntity<TEntity>(results.Rows[0]);
            }


            return clientDataModel;
        }

        /// <summary>
        /// Retrieves entity details from the underlying data access and uses them to populat an entity instance.
        /// </summary>
        /// <param name="key">Primary key identifier of the entity to retrieve.</param>
        /// <returns>A new instance of type <typeparamref name="TModel" /> if found. Null otherwise.</returns>
        public override ClientObjectModel GetEntityByKey(int key)
        {
            ClientObjectModel clientObjectModel = null;
            var results = DataAccess.ExecuteDataTable(this.ConnectionString, SPGetClientById,
                            DataAccess.CreateParameter(DBCClientId, DbType.Int32, key)
               );

            if (results.Rows.Count > 0)
            {
                clientObjectModel = CreateEntity<ClientObjectModel>(results.Rows[0]);
            }


            return clientObjectModel;
        }

        /// <summary>
        /// Instantiates a new entity and populates members with the data record passed in.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to create.</typeparam>
        /// <param name="entityRecord">Data record used to create the entity.</param>
        /// <returns>A new entity of type <typeparamref name="TEntity" />.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override TEntity CreateEntity<TEntity>(IDataRecord entityRecord)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Instantiates a new entity and populates members with the data record passed in.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to create.</typeparam>
        /// <param name="entityRow">Data row used to create the entity.</param>
        /// <returns>A new entity of type <typeparamref name="TEntity" />.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override TEntity CreateEntity<TEntity>(DataRow entityRow)
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
        public override IEnumerable<TEntity> GetAllEntities<TEntity>(int startRowIndex, int maximumRows, ISortDetail<TEntity> sort)
        {
            DataTable userDatatable = this.DataAccess.ExecuteDataTable(this.ConnectionString, SPGetAllClients);

            List<TEntity> clientObjectModels = new List<TEntity>();
            int CurrentClientID = -1;

            TEntity clientObjectModel = null;

            foreach (DataRow row in userDatatable.Rows)
            {
                if (CurrentClientID != row.Field<int>(DBCClientId))
                {
                    if (clientObjectModel != null)
                    {
                        clientObjectModels.Add(clientObjectModel);
                    }

                    clientObjectModel = new TEntity();

                    clientObjectModel.ClientID = row.Field<int>(DBCClientId);

                    clientObjectModel.Key = clientObjectModel.ClientID;

                    clientObjectModel.ClientName = row.Field<string>(DBCClientName);

                    CurrentClientID = clientObjectModel.ClientID;

                    clientObjectModel.VerticalMarketName = row.Field<string>(DBCVerticalMarketName);

                    clientObjectModel.ClientDescription = row.Field<string>(DBCClientDescription);

                    clientObjectModel.ClientConnectionStringName = row.Field<string>(DBCClientConnectionStringName);

                    clientObjectModel.ClientDatabaseName = row.Field<string>(DBCClientDatabaseName);

                    clientObjectModel.VerticalMarketDatabaseName = row.Field<string>(DBCVerticalMarketDatabaseName);

                    clientObjectModel.VerticalMarketConnectionStringName = row.Field<string>(DBCVerticalMarketConnectionStringName);

                    clientObjectModel.VerticalMarketId = row.Field<int>(DBCVerticalMarketId);

                    clientObjectModel.ModifiedBy = row.Field<int>(DBCmodifiedBy);

                    clientObjectModel.LastModified = row.Field<DateTime>(DBCLastModified);

                    clientObjectModel.ClientDnsList = new List<ClientDNSObjectModel>();

                    clientObjectModel.Users = new List<int>();

                }
                if (row.Field<int?>(DBCClientDnsId) != null)
                {
                    if (clientObjectModel.ClientDnsList.Where(x => x.ClientDnsId == row.Field<int?>(DBCClientDnsId)).Count() <= 0)
                    {
                        clientObjectModel.ClientDnsList.Add(new ClientDNSObjectModel()
                        {
                            ClientDnsId = row.Field<int>(DBCClientDnsId),
                            Dns = row.Field<string>(DBCDns),
                            ClientDnsSiteId = row.Field<int>(DBCClientDnsSiteId)
                        });
                    }
                }

                if (row.Field<int?>(DBCUserId) != null)
                {
                    if (!clientObjectModel.Users.Contains(row.Field<int>(DBCUserId)))
                    {
                        clientObjectModel.Users.Add(row.Field<int>(DBCUserId));
                    }
                }

            }
            if (clientObjectModel != null)
            {
                clientObjectModels.Add(clientObjectModel);
            }

            return clientObjectModels;
        }


        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        public override void SaveEntity(ClientObjectModel entity)
        {
            var clientUsers = new DataTable();
            clientUsers.Columns.Add("ClientId", typeof(int));
            clientUsers.Columns.Add("UserId", typeof(int));


            if (entity.Users != null)
            {
                entity.Users.ForEach(item =>
                {
                    clientUsers.Rows.Add(entity.ClientID, item);
                });
            }

            List<DbParameter> parameters = base.GetParametersFromEntity<ClientObjectModel>(entity) as List<DbParameter>;
            if (parameters != null)
            {

                parameters.Add(DataAccess.CreateParameter(DBCClientId, DbType.Int32, entity.ClientID));

                parameters.Add(DataAccess.CreateParameter(DBCClientName, DbType.String, entity.ClientName));

                parameters.Add(DataAccess.CreateParameter(DBCClientConnectionStringName, DbType.String, entity.ClientConnectionStringName));

                parameters.Add(DataAccess.CreateParameter(DBCClientDatabaseName, DbType.String, entity.ClientDatabaseName));

                parameters.Add(DataAccess.CreateParameter(DBCVerticalMarketId, DbType.Int32, entity.VerticalMarketId));

                parameters.Add(DataAccess.CreateParameter(DBCDescription, DbType.String, entity.ClientDescription));

                parameters.Add(DataAccess.CreateParameter(DBCClientUsers, SqlDbType.Structured, clientUsers));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPSaveClient, parameters);
            }
        }

        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        /// <param name="modifiedBy">The modified by.</param>
        public override void SaveEntity(ClientObjectModel entity, int modifiedBy)
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
            this.DeleteEntity(key, 0);
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
                parameters.Add(DataAccess.CreateParameter(DBCClientId, DbType.Int32, key));
                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeleteClient, parameters);
            }
        }


        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(ClientObjectModel entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(ClientObjectModel entity, int deletedBy)
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
