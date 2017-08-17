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


namespace RRD.FSG.RP.Model.Factories.System
{
    /// <summary>
    /// Class UserFactory.
    /// </summary>
    public class UserFactory : AuditedBaseFactory<UserObjectModel,int>
    {
        #region  Constants 
        /// <summary>
        /// The sp save user
        /// </summary>
        private const string SPSaveUser = "RPV2HostedAdmin_SaveUser";

        /// <summary>
        /// The sp delete user
        /// </summary>
        private const string SPDeleteUser = "RPV2HostedAdmin_DeleteUser";


        /// <summary>
        /// The sp get all users
        /// </summary>
        private const string SPGetAllUsers = "RPV2HostedAdmin_GetAllUsers";

        /// <summary>
        /// The sp get user by identifier
        /// </summary>
        private const string SPGetUserById = "RPV2HostedAdmin_GetUserById";


        /// <summary>
        /// The DBC user identifier
        /// </summary>
        private const string DBCUserId = "userId";

        /// <summary>
        /// The dbce mail
        /// </summary>
        private const string DBCEMail = "eMail";

        /// <summary>
        /// The database cf FirstName
        /// </summary>
        private const string DBCFirstName = "FirstName";

        /// <summary>
        /// The database cl LastName
        /// </summary>
        private const string DBCLastName = "LastName";

        /// <summary>
        /// The DBC user name
        /// </summary>
        private const string DBCUserName = "userName";

        /// <summary>
        /// The DBC SecurityStamp
        /// </summary>
        private const string DBCSecurityStamp = "SecurityStamp";

        /// <summary>
        /// The DBC user name
        /// </summary>
        private const string DBCPhoneNumber = "PhoneNumber";

        /// <summary>
        /// The DBC SecurityStamp
        /// </summary>
        private const string DBCPhoneNumberConfirmed = "PhoneNumberConfirmed";

        /// <summary>
        /// The database modified by
        /// </summary>
        private const string DBCmodifiedBy = "modifiedBy";

        /// <summary>
        /// The database modified by
        /// </summary>
        private const string DBCLastModified = "LastModified";

        /// <summary>
        /// The database  modified date
        /// </summary>
        private const string DBCutcModifiedDate = "utcModifiedDate";

        /// <summary>
        /// The DBC email confirmed
        /// </summary>
        private const string DBCEmailConfirmed = "EmailConfirmed";

        /// <summary>
        /// The DBC TwoFactorEnabled
        /// </summary>
        private const string DBCTwoFactorEnabled = "TwoFactorEnabled";

        /// <summary>
        /// The DBC password hash
        /// </summary>
        private const string DBCPasswordHash = "PasswordHash";

        /// <summary>
        /// The RoleId
        /// </summary>
        private const string DBCRoleId = "RoleId";

        /// <summary>
        /// The RoleName
        /// </summary>
        private const string DBCRoleName = "Name";

        /// <summary>
        /// The UserRoles
        /// </summary>
        private const string DBCUserRoles = "UserRoles";

        /// <summary>
        /// The ClientId
        /// </summary>
        private const string DBCClientId = "ClientId";

        /// <summary>
        /// The ClientUsers
        /// </summary>
        private const string DBCClientUsers = "ClientUsers";

        
            
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public UserFactory(IDataAccess dataAccess)
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
            TEntity userDataModel = null;
            var results = DataAccess.ExecuteDataTable(this.ConnectionString, SPGetUserById,
                            DataAccess.CreateParameter(DBCUserId, DbType.Int32, key)
               );

            if (results.Rows.Count > 0)
            {
                userDataModel = CreateEntity<TEntity>(results.Rows[0]);
            }


            return userDataModel;
        }

        /// <summary>
        /// Retrieves entity details from the underlying data access and uses them to populat an entity instance.
        /// </summary>
        /// <param name="key">Primary key identifier of the entity to retrieve.</param>
        /// <returns>A new instance of type <typeparamref name="TModel" /> if found. Null otherwise.</returns>
        public override UserObjectModel GetEntityByKey(int key)
        {
            UserObjectModel userDataModel = null;
            var results = DataAccess.ExecuteDataTable(DBConnectionString.SystemDBConnectionString(), SPGetUserById,
                            DataAccess.CreateParameter(DBCUserId, DbType.Int32, key)
               );

            if (results.Rows.Count > 0)
            {
                userDataModel = CreateEntity<UserObjectModel>(results.Rows[0]);
            }


            return userDataModel;
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
                entity.UserId = entityRecord.GetInt32(entityRecord.GetOrdinal(DBCUserId));
                entity.Email = entityRecord.GetString(entityRecord.GetOrdinal(DBCEMail));
                entity.EmailConfirmed = entityRecord.GetBoolean(entityRecord.GetOrdinal(DBCEmailConfirmed));
                entity.PasswordHash = entityRecord.GetString(entityRecord.GetOrdinal(DBCPasswordHash));
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
                entity.UserId = entityRow.Field<int>(DBCUserId);
                entity.Email = entityRow.Field<string>(DBCEMail);
                entity.EmailConfirmed = entityRow.Field<bool>(DBCEmailConfirmed);
                entity.PasswordHash = entityRow.Field<string>(DBCPasswordHash);
            }

            return entity;
        }


        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <typeparam name="UserObjectModel">The type of the user object model.</typeparam>
        /// <returns>IEnumerable&lt;UserObjectModel&gt;.</returns>
        public override IEnumerable<UserObjectModel> GetAllEntities<UserObjectModel>()
        {
            DataTable userDatatable = this.DataAccess.ExecuteDataTable(this.ConnectionString, SPGetAllUsers);

            List<UserObjectModel> userObjectModels = new List<UserObjectModel>();
            int CurrentUserID = -1;

            UserObjectModel userObjectModel = null;

            foreach (DataRow row in userDatatable.Rows)
            {
                if (CurrentUserID != row.Field<int>(DBCUserId))
                {
                    if (userObjectModel != null)
                    {
                        userObjectModels.Add(userObjectModel);
                    }

                    userObjectModel = new UserObjectModel();

                    userObjectModel.UserId = row.Field<int>(DBCUserId);

                    userObjectModel.Key = userObjectModel.UserId;

                    CurrentUserID = userObjectModel.UserId;

                    userObjectModel.Email = row.Field<string>(DBCEMail);

                    userObjectModel.EmailConfirmed = row.Field<bool>(DBCEmailConfirmed);

                    userObjectModel.PasswordHash = row.Field<string>(DBCPasswordHash);

                    userObjectModel.SecurityStamp = row.Field<string>(DBCSecurityStamp);

                    userObjectModel.PhoneNumber = row.Field<string>(DBCPhoneNumber);

                    userObjectModel.PhoneNumberConfirmed = row.Field<bool>(DBCPhoneNumberConfirmed);

                    userObjectModel.UserName = row.Field<string>(DBCUserName);

                    userObjectModel.FirstName = row.Field<string>(DBCFirstName);

                    userObjectModel.LastName = row.Field<string>(DBCLastName);

                    userObjectModel.ModifiedBy = row.Field<int>(DBCmodifiedBy);

                    userObjectModel.LastModified = row.Field<DateTime>(DBCLastModified);

                    userObjectModel.Clients = new List<int>();
                    userObjectModel.Roles = new List<RolesObjectModel>();
                }
                if (row.Field<int?>(DBCClientId) != null)
                {
                    if (!userObjectModel.Clients.Contains(row.Field<int>(DBCClientId)))
                    {
                        userObjectModel.Clients.Add(row.Field<int>(DBCClientId));
                    }
                }
                if (row.Field<int?>(DBCRoleId) != null)
                {
                    if (!userObjectModel.Roles.Exists(p=>p.RoleId == row.Field<int>(DBCRoleId)))
                    {
                        userObjectModel.Roles.Add(new RolesObjectModel { RoleId = row.Field<int>(DBCRoleId), RoleName = row.Field<string>(DBCRoleName) });
                    }
                }

            }
            if (userObjectModel != null)
            {
                userObjectModels.Add(userObjectModel);
            }

            return userObjectModels;

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
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <param name="sort">The sort.</param>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override IEnumerable<TEntity> GetAllEntities<TEntity>(int startRowIndex, int maximumRows, ISortDetail<TEntity> sort)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="sort">The sort.</param>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override IEnumerable<TEntity> GetAllEntities<TEntity>(ISortDetail<TEntity> sort)
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
        public override void SaveEntity(UserObjectModel entity, int modifiedBy)
        {
            base.SetModifiedBy(entity, modifiedBy);
            this.SaveEntity(entity);
        }



        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        public override void SaveEntity(UserObjectModel entity)
        {

            var userRoles = new DataTable();
            userRoles.Columns.Add("UserId", typeof(int));
            userRoles.Columns.Add("RoleID", typeof(int));

            if(entity.Roles != null)
            {
                entity.Roles.ForEach(item => {
                    userRoles.Rows.Add(entity.UserId, item.RoleId);
                });
            }

            var clientUsers = new DataTable();
            clientUsers.Columns.Add("ClientId", typeof(int));
            clientUsers.Columns.Add("UserId", typeof(int));

            if (entity.Clients != null)
            {
                entity.Clients.ForEach(item =>
                {
                    clientUsers.Rows.Add(item,entity.UserId);
                });
            }


            List<DbParameter> parameters = base.GetParametersFromEntity<UserObjectModel>(entity) as List<DbParameter>;

            if (parameters != null)
            {

                parameters.Add(DataAccess.CreateParameter(DBCUserId, DbType.Int32, entity.UserId));

                parameters.Add(DataAccess.CreateParameter(DBCEMail, DbType.String, entity.Email));

                parameters.Add(DataAccess.CreateParameter(DBCEmailConfirmed, DbType.Boolean, entity.EmailConfirmed));

                parameters.Add(DataAccess.CreateParameter(DBCPasswordHash, DbType.String, entity.PasswordHash));

                parameters.Add(DataAccess.CreateParameter(DBCSecurityStamp, DbType.String, entity.SecurityStamp));

                parameters.Add(DataAccess.CreateParameter(DBCPhoneNumber, DbType.String, entity.PhoneNumber));

                parameters.Add(DataAccess.CreateParameter(DBCPhoneNumberConfirmed, DbType.Boolean, entity.PhoneNumberConfirmed));

                parameters.Add(DataAccess.CreateParameter(DBCTwoFactorEnabled, DbType.Boolean, entity.TwoFactorEnabled));

                parameters.Add(DataAccess.CreateParameter(DBCUserName, DbType.String, entity.UserName));

                parameters.Add(DataAccess.CreateParameter(DBCFirstName, DbType.String, entity.FirstName));

                parameters.Add(DataAccess.CreateParameter(DBCLastName, DbType.String, entity.LastName));

                parameters.Add(DataAccess.CreateParameter(DBCUserRoles, SqlDbType.Structured , userRoles));

                parameters.Add(DataAccess.CreateParameter(DBCClientUsers, SqlDbType.Structured , clientUsers));

                

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPSaveUser, parameters);
            }
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
                parameters.Add(DataAccess.CreateParameter(DBCUserId, DbType.Int32, key));
                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeleteUser, parameters);
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
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(UserObjectModel entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(UserObjectModel entity, int deletedBy)
        {
            throw new NotImplementedException();
        }



    }
}
