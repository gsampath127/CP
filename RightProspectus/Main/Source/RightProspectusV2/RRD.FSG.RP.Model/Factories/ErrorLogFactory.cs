// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-03-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Model.SearchEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;


namespace RRD.FSG.RP.Model.Factories
{
    /// <summary>
    /// Class ErrorLogFactory.
    /// </summary>
    public class ErrorLogFactory: BaseFactory<ErrorLogObjectModel, int>
    {
        #region Constants
        /// <summary>
        /// The sp get all ErrorLog
        /// </summary>
        private const string SPGetAllErrorLog = "RPV2Hosted_GetAllErrorLog";
        /// <summary>
        /// The sp get all ErrorLog
        /// </summary>
        private const string SPGetErrorLog = "RPV2HostedAdmin_GetErrorLog";
        /// <summary>
        /// The sp save ErrorLog
        /// </summary>
        private const string SPSaveErrorLog = "RPV2Hosted_SaveErrorLog";

        /// <summary>
        /// ErrorLogId
        /// </summary>
        private const string DBCErrorLogId = "ErrorLogId";
        /// <summary>
        /// ErrorLogName
        /// </summary>
        private const string DBCErrorCode = "ErrorCode";
        /// <summary>
        /// ErrorUtcDate
        /// </summary>
        private const string DBCErrorUtcDate = "ErrorUtcDate";
        /// <summary>
        /// FromErrorDate
        /// </summary>
        private const string DBCFromErrorDate = "FromErrorDate";
        /// <summary>
        /// ToErrorDate
        /// </summary>
        private const string DBCToErrorDate = "ToErrorDate";
        /// <summary>
        /// Priority
        /// </summary>
        private const string DBCPriority = "Priority";
        /// <summary>
        /// Severity
        /// </summary>
        private const string DBCSeverity = "Severity";
        /// <summary>
        /// Title
        /// </summary>
        private const string DBCTitle = "Title";
        /// <summary>
        /// MachineName
        /// </summary>
        private const string DBCMachineName = "MachineName";
        /// <summary>
        /// AppDomainName
        /// </summary>
        private const string DBCAppDomainName = "AppDomainName";
        /// <summary>
        /// ProcessID
        /// </summary>
        private const string DBCProcessID = "ProcessID";
        /// <summary>
        /// ProcessName
        /// </summary>
        private const string DBCProcessName = "ProcessName";
        /// <summary>
        /// ThreadName
        /// </summary>
        private const string DBCThreadName = "ThreadName";
        /// <summary>
        /// Win32ThreadId
        /// </summary>
        private const string DBCWin32ThreadId = "Win32ThreadId";
        /// <summary>
        /// EventId
        /// </summary>
        private const string DBCEventId = "EventId";
        /// <summary>
        /// SiteActivityId
        /// </summary>
        private const string DBCSiteActivityId = "SiteActivityId";
        /// <summary>
        /// The DBC message
        /// </summary>
        private const string DBCMessage = "Message";
        /// <summary>
        /// FormattedMessage
        /// </summary>
        private const string DBCFormattedMessage = "FormattedMessage";
        /// <summary>
        /// URL
        /// </summary>
        private const string DBCURL = "URL";
        /// <summary>
        /// AbsoluteURL
        /// </summary>
        private const string DBCAbsoluteURL = "AbsoluteURL";



        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public ErrorLogFactory(IDataAccess dataAccess)
            : base(dataAccess) {
                this.ConnectionString = DBConnectionString.SystemDBConnectionString(); 
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

                entity.ErrorLogId = entityRow.Field<int>(DBCErrorLogId);
                entity.ErrorCode = entityRow.Field<int>(DBCErrorCode);
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
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        public override IEnumerable<TEntity> GetAllEntities<TEntity>(int startRowIndex, int maximumRows)
        {
            return this.GetEntitiesInternal<TEntity>(SPGetAllErrorLog, startRowIndex, maximumRows,null);
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
            return this.GetEntitiesInternal<TEntity>(SPGetAllErrorLog, startRowIndex, maximumRows,sort);
        }



        /// <summary>
        /// Gets the entities by search.
        /// </summary>
        /// <param name="searchParams">The search parameters.</param>
        /// <returns>List&lt;ErrorLogObjectModel&gt;.</returns>
        public List<ErrorLogObjectModel> GetEntitiesBySearch(ErrorLogSearchDetail searchParams)
        {
            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(DataAccess.CreateParameter(DBCErrorCode, DbType.String, searchParams.ErrorCode));
            parameters.Add(DataAccess.CreateParameter(DBCFromErrorDate, DbType.String, searchParams.FromErrorDate));
            parameters.Add(DataAccess.CreateParameter(DBCToErrorDate, DbType.String, searchParams.ToErrorDate));
            parameters.Add(DataAccess.CreateParameter(DBCTitle, DbType.String, searchParams.Title));
            parameters.Add(DataAccess.CreateParameter(DBCProcessName, DbType.String, searchParams.ProcessName));
            parameters.Add(DataAccess.CreateParameter(DBCEventId, DbType.String, searchParams.EventId));

            DataTable resultTable = DataAccess.ExecuteDataTable(this.ConnectionString, SPGetErrorLog, parameters.ToArray());

            List<ErrorLogObjectModel> errorLogs = new List<ErrorLogObjectModel>();

            if (resultTable != null && resultTable.Rows.Count > 0)
                foreach (DataRow row in resultTable.Rows)
                {
                    errorLogs.Add(new ErrorLogObjectModel
                    {
                        ErrorCode = Convert.ToInt32(row[DBCErrorCode]),
                        ErrorUtcDate = Convert.ToDateTime(row[DBCErrorUtcDate]),
                        Priority = Convert.ToInt32(row[DBCPriority]),
                        Severity = row[DBCSeverity].ToString(),
                        Title = row[DBCTitle].ToString(),
                        MachineName = row[DBCMachineName].ToString(),
                        AppDomainName = row[DBCAppDomainName].ToString(),
                        ProcessID = row[DBCProcessID].ToString(),
                        ProcessName = row[DBCProcessName].ToString(),
                        ThreadName = row[DBCThreadName].ToString(),
                        Win32ThreadId = row[DBCWin32ThreadId].ToString(),
                        EventId = Convert.ToInt32(row[DBCEventId]),
                        SiteActivityID = Convert.ToInt32(row[DBCSiteActivityId]),
                        Message = row[DBCMessage].ToString(),
                        FormattedMessage = row[DBCFormattedMessage].ToString(),
                        URL = row[DBCURL].ToString(),
                        AbsoluteURL = row[DBCAbsoluteURL].ToString()
                    });
                }

            return errorLogs;
        }

        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        public override void SaveEntity(ErrorLogObjectModel entity)
        {
            List<DbParameter> parameters = base.GetParametersFromEntity<ErrorLogObjectModel>(entity) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCErrorCode, DbType.String, entity.ErrorCode));

                parameters.Add(DataAccess.CreateParameter(DBCPriority, DbType.Int32, entity.Priority));

                parameters.Add(DataAccess.CreateParameter(DBCSeverity, DbType.String, entity.Severity));

                parameters.Add(DataAccess.CreateParameter(DBCTitle, DbType.String, entity.Title));

                parameters.Add(DataAccess.CreateParameter(DBCMachineName, DbType.String, entity.MachineName));

                parameters.Add(DataAccess.CreateParameter(DBCAppDomainName, DbType.String, entity.AppDomainName));

                parameters.Add(DataAccess.CreateParameter(DBCProcessID, DbType.String, entity.ProcessID));

                parameters.Add(DataAccess.CreateParameter(DBCProcessName, DbType.String, entity.ProcessName));

                parameters.Add(DataAccess.CreateParameter(DBCThreadName, DbType.String, entity.ThreadName));

                parameters.Add(DataAccess.CreateParameter(DBCWin32ThreadId, DbType.String, entity.Win32ThreadId));

                parameters.Add(DataAccess.CreateParameter(DBCEventId, DbType.Int32, entity.EventId));

                parameters.Add(DataAccess.CreateParameter(DBCSiteActivityId, DbType.Int32, entity.SiteActivityID));

                parameters.Add(DataAccess.CreateParameter(DBCMessage, DbType.String, entity.Message));

                parameters.Add(DataAccess.CreateParameter(DBCFormattedMessage, DbType.String, entity.FormattedMessage));

                parameters.Add(DataAccess.CreateParameter(DBCURL, DbType.String, entity.URL));

                parameters.Add(DataAccess.CreateParameter(DBCAbsoluteURL, DbType.String, entity.AbsoluteURL));              

                

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPSaveErrorLog, parameters);

            }

        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(int key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void SaveEntity(ErrorLogObjectModel entity, int modifiedBy)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(int key, int modifiedBy)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(ErrorLogObjectModel entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(ErrorLogObjectModel entity, int deletedBy)
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
        public override IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, ISortDetail<TEntity> sort, out int totalRecordCount, params int[] entitiesToIgnore)
        {
            totalRecordCount = 0;
            List<DbParameter> parameters = new List<DbParameter>();
            ErrorLogSearchDetail searchParams = (ErrorLogSearchDetail)search;
            parameters.Add(DataAccess.CreateParameter(DBCErrorCode, DbType.String, searchParams.ErrorCode));
            parameters.Add(DataAccess.CreateParameter(DBCFromErrorDate, DbType.String, searchParams.FromErrorDate));
            parameters.Add(DataAccess.CreateParameter(DBCToErrorDate, DbType.String, searchParams.ToErrorDate));
            parameters.Add(DataAccess.CreateParameter(DBCTitle, DbType.String, searchParams.Title));
            parameters.Add(DataAccess.CreateParameter(DBCProcessName, DbType.String, searchParams.ProcessName));
            parameters.Add(DataAccess.CreateParameter(DBCEventId, DbType.String, searchParams.EventId));

            DataTable resultTable = DataAccess.ExecuteDataTable(this.ConnectionString, SPGetErrorLog, parameters.ToArray());

            List<ErrorLogObjectModel> errorLogs = new List<ErrorLogObjectModel>();

            if (resultTable != null && resultTable.Rows.Count > 0)
            {
                foreach (DataRow row in resultTable.Rows)
                {
                    errorLogs.Add(new ErrorLogObjectModel
                    {
                        ErrorCode = row[DBCErrorCode] != null ? Convert.ToInt32(row[DBCErrorCode]) : 0,
                        ErrorUtcDate = row[DBCErrorUtcDate] != null ? Convert.ToDateTime(row[DBCErrorUtcDate]) : DateTime.MinValue,
                        Priority = row[DBCPriority] != null ? Convert.ToInt32(row[DBCPriority]) : 0,
                        Severity = row[DBCSeverity] != null ? row[DBCSeverity].ToString() : string.Empty,
                        Title = row[DBCTitle] != null ? row[DBCTitle].ToString() : string.Empty,
                        MachineName = row[DBCMachineName] != null ? row[DBCMachineName].ToString() : string.Empty,
                        AppDomainName = row[DBCAppDomainName] != null ? row[DBCAppDomainName].ToString() : string.Empty,
                        ProcessID = row[DBCProcessID] != null ? row[DBCProcessID].ToString() : string.Empty,
                        ProcessName = row[DBCProcessName] != null ? row[DBCProcessName].ToString() : string.Empty,
                        ThreadName = row[DBCThreadName] != DBNull.Value ? row[DBCThreadName].ToString() : string.Empty,
                        Win32ThreadId = row[DBCWin32ThreadId] != null ? row[DBCWin32ThreadId].ToString() : string.Empty,
                        EventId = row[DBCEventId] != null ? Convert.ToInt32(row[DBCEventId]) : 0,
                        SiteActivityID = row[DBCSiteActivityId] != DBNull.Value ? Convert.ToInt32(row[DBCSiteActivityId]) : 0,
                        Message = row[DBCMessage] != null ? row[DBCMessage].ToString() : string.Empty,
                        FormattedMessage = row[DBCFormattedMessage] != null ? row[DBCFormattedMessage].ToString() : string.Empty,
                        URL = row[DBCURL] != DBNull.Value ? row[DBCURL].ToString() : string.Empty,
                        AbsoluteURL = row[DBCAbsoluteURL] != null ? row[DBCAbsoluteURL].ToString() : string.Empty
                    });
                }
                totalRecordCount = resultTable.Rows.Count;
            }

            return (IEnumerable<TEntity>)errorLogs;
        }

    }
}
