// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-12-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Model.SortDetail.System;
using RRD.FSG.RP.Utilities;

namespace RRD.FSG.RP.Model.Factories.System
{
    /// <summary>
    /// Class ReportScheduleFactory.
    /// </summary>
    public class ReportScheduleFactory : AuditedBaseFactory<ReportScheduleObjectModel, int>
    {
        #region Constants
        /// <summary>
        /// The sp save client
        /// </summary>
        private const string SPSaveReportSchedule = "RPV2HostedAdmin_SaveReportSchedule";

        /// <summary>
        /// The sp delete client
        /// </summary>
        private const string SPDeleteReportSchedule = "RPV2HostedAdmin_DeleteReportSchedule";

        /// <summary>
        /// The sp get clients
        /// </summary>
        private const string SPGetReportSchedule = "RPV2HostedAdmin_GetReportSchedule";

        /// <summary>
        /// The sp get clients
        /// </summary>
        private const string SPGetAllReportSchedule = "RPV2HostedAdmin_GetAllReportSchedule";
        /// <summary>
        /// The sp get reports
        /// </summary>
        private const string SPGetAllReports = "RPV2HostedAdmin_GetAllReports";
        /// <summary>
        /// The sp get clients
        /// </summary>
        private const string SPGetReportScheduleByClientId = "RPV2HostedAdmin_GetReportScheduleByClientId";

        /// <summary>
        /// The sp get client by identifier
        /// </summary>
        private const string SPGetReportScheduleById = "RPV2HostedAdmin_GetReportScheduleById";

        /// <summary>
        /// The Database Column client identifier
        /// </summary>
        private const string DBCReportScheduleId = "ReportScheduleId";

        /// <summary>
        /// The DBC report name
        /// </summary>
        private const string DBCReportName = "ReportName";

        /// <summary>
        /// The DBC report identifier
        /// </summary>
        private const string DBCReportId = "ReportId";

        /// <summary>
        /// The DBC ISSFTP identifier
        /// </summary>
        private const string DBCIsFTPTransfer = "ISSFTP";

        /// <summary>
        /// The DBC client identifier
        /// </summary>
        private const string DBCClientId = "ClientId";

        /// <summary>
        /// The DBC is enabled
        /// </summary>
        private const string DBCIsEnabled = "IsEnabled";

        /// <summary>
        /// The DBC frequency type
        /// </summary>
        private const string DBCFrequencyType = "FrequencyType";

        /// <summary>
        /// The DBC frequency interval
        /// </summary>
        private const string DBCFrequencyInterval = "FrequencyInterval";

        /// <summary>
        /// The DBC UTC first scheduled run date
        /// </summary>
        private const string DBCUtcFirstScheduledRunDate = "UtcFirstScheduledRunDate";

        /// <summary>
        /// The DBC UTC last scheduled run date
        /// </summary>
        private const string DBCUtcLastScheduledRunDate = "UtcLastScheduledRunDate";

        /// <summary>
        /// The DBC UTC last actual run date
        /// </summary>
        private const string DBCUtcLastActualRunDate = "UtcLastActualRunDate";

        /// <summary>
        /// The DBC UTC next scheduled run date
        /// </summary>
        private const string DBCUtcNextScheduledRunDate = "UtcNextScheduledRunDate";

        /// <summary>
        /// The DBC frequency description
        /// </summary>
        private const string DBCFrequencyDescription = "FrequencyDescription";

        /// <summary>
        /// The DBC email
        /// </summary>
        private const string DBCEmail = "email";

        /// <summary>
        /// The DBCFTP server ip
        /// </summary>
        private const string DBCFTPServerIP = "ftpServerIP";

        /// <summary>
        /// The DBCFT pile path
        /// </summary>
        private const string DBCFTPilePath = "ftpFilePath";

        /// <summary>
        /// The DBCFTP username
        /// </summary>
        private const string DBCFTPUsername = "ftpUsername";

        /// <summary>
        /// The DBCFTP password
        /// </summary>
        private const string DBCFTPPassword = "ftpPassword";
        /// <summary>
        /// Page size
        /// </summary>
        private const string DBCPageSize = "PageSize";
        /// <summary>
        /// Page Index
        /// </summary>
        private const string DBCPageIndex = "PageIndex";
        /// <summary>
        /// Sort Direction
        /// </summary>
        private const string DBCSortDirection = "SortDirection";
        /// <summary>
        /// Sort Column
        /// </summary>
        private const string DBCSortColumn = "SortColumn";

        /// <summary>
        /// The DBC IsSFTP
        /// </summary>
        private const string DBCIsSFTP = "IsSFTP";


        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportScheduleFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public ReportScheduleFactory(IDataAccess dataAccess)
            : base(dataAccess)
        {
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
            TEntity reportScheduleDataModel = null;
            var result = DataAccess.ExecuteDataTable(this.ConnectionString, SPGetReportScheduleById,
                            DataAccess.CreateParameter(DBCReportScheduleId, DbType.Int32, key));
            if (result.Rows.Count > 0)
            {
                result.Columns.Add("Entity");
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    result.Rows[i][22] = "GetEntityByKey";
                }

                reportScheduleDataModel = CreateEntity<TEntity>(result.Rows[0]);
            }

            return reportScheduleDataModel;
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
            DataTable result = DataAccess.ExecuteDataTable(this.ConnectionString, SPGetAllReportSchedule, null);
            result.Columns.Add("Entity");
            for (int i = 0; i < result.Rows.Count; i++)
            {
                result.Rows[i][8] = "GetAllEntities";
            }
            return this.CreateEntities<TEntity>(result);

            //return this.GetEntitiesInternal<TEntity>(SPGetAllReportSchedule, startRowIndex, maximumRows, null);
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
            DataTable result = DataAccess.ExecuteDataTable(this.ConnectionString, SPGetAllReportSchedule, null);
            result.Columns.Add("Entity");
            for (int i = 0; i < result.Rows.Count; i++)
            {
                result.Rows[i][8] = "GetAllEntities";
            }
            return this.CreateEntities<TEntity>(result);
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
            List<DbParameter> parameters = new List<DbParameter>();
            ReportScheduleSearchDetail searchDetails = search as ReportScheduleSearchDetail;
            ReportScheduleSortDetail sortDetails = sort as ReportScheduleSortDetail;

            totalRecordCount = 0;

            if (maximumRows > 0)
            {
                DataSet result = new DataSet();
                if (parameters != null)
                {
                    parameters.Add(DataAccess.CreateParameter(DBCReportName, DbType.String, searchDetails.ReportName));
                    parameters.Add(DataAccess.CreateParameter(DBCFrequencyType, DbType.Int32, searchDetails.FrequencyType));
                    parameters.Add(DataAccess.CreateParameter(DBCFrequencyInterval, DbType.Int32, searchDetails.FrequencyInterval));
                    parameters.Add(DataAccess.CreateParameter(DBCUtcFirstScheduledRunDate, DbType.DateTime, searchDetails.FirstScheduleRunDate));
                    parameters.Add(DataAccess.CreateParameter(DBCUtcLastScheduledRunDate, DbType.DateTime, searchDetails.LastScheduleRunDate));
                    parameters.Add(DataAccess.CreateParameter(DBCUtcNextScheduledRunDate, DbType.DateTime, searchDetails.NextScheduleRunDate));
                    parameters.Add(DataAccess.CreateParameter(DBCIsEnabled, DbType.Boolean, searchDetails.IsEnabled));
                    parameters.Add(DataAccess.CreateParameter(DBCPageSize, DbType.Int32, maximumRows));
                    parameters.Add(DataAccess.CreateParameter(DBCPageIndex, DbType.Int32, startRowIndex));
                    parameters.Add(DataAccess.CreateParameter(DBCSortDirection, DbType.String, sortDetails.Order));
                    parameters.Add(DataAccess.CreateParameter(DBCSortColumn, DbType.String, sortDetails.Column));
                    parameters.Add(DataAccess.CreateParameter(DBCClientId, DbType.Int32, searchDetails.ClientId));

                    result = DataAccess.ExecuteDataSet(this.ConnectionString, SPGetReportScheduleByClientId, parameters.ToArray());
                    result.Tables[0].Columns.Add("Entity");
                    for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                    {
                        result.Tables[0].Rows[i][14] = "GetEntitiesBySearch";
                    }
                    totalRecordCount = Convert.ToInt32(result.Tables[1].Rows[0][0]);
                }
                return this.CreateEntities<TEntity>(result.Tables[0]);
            }
            else if (maximumRows == -1)
            {
                DataTable result = DataAccess.ExecuteDataTable(this.ConnectionString, SPGetAllReports, null);
                result.Columns.Add("Entity");
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    result.Rows[i][5] = "GetAllReports";
                }
                return this.CreateEntities<TEntity>(result);
            }
            else
            {
                parameters.Add(DataAccess.CreateParameter(DBCClientId, DbType.Int32, searchDetails.ClientId));
                DataTable result = DataAccess.ExecuteDataTable(this.ConnectionString, SPGetAllReportSchedule, parameters.ToArray());
                result.Columns.Add("Entity");
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    result.Rows[i][10] = "GetAllEntities";
                }
                return this.CreateEntities<TEntity>(result);
            }
        }

        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        /// <param name="modifiedBy">The modified by.</param>
        public override void SaveEntity(ReportScheduleObjectModel entity, int modifiedBy)
        {
            base.SetModifiedBy(entity, modifiedBy);
            this.SaveEntity(entity);
        }

        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        public override void SaveEntity(ReportScheduleObjectModel entity)
        {

            List<DbParameter> parameters = base.GetParametersFromEntity<ReportScheduleObjectModel>(entity) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCReportScheduleId, DbType.Int32, entity.ReportScheduleId));

                parameters.Add(DataAccess.CreateParameter(DBCReportId, DbType.Int32, entity.ReportId));

                parameters.Add(DataAccess.CreateParameter(DBCClientId, DbType.Int32, entity.ClientId));
                parameters.Add(DataAccess.CreateParameter(DBCIsEnabled, DbType.Boolean, entity.IsEnabled));
                parameters.Add(DataAccess.CreateParameter(DBCFrequencyType, DbType.Int32, entity.FrequencyType));
                parameters.Add(DataAccess.CreateParameter(DBCFrequencyInterval, DbType.Int32, entity.FrequencyInterval));
                parameters.Add(DataAccess.CreateParameter(DBCUtcFirstScheduledRunDate, DbType.DateTime, entity.UtcFirstScheduledRunDate));
                parameters.Add(DataAccess.CreateParameter(DBCUtcLastScheduledRunDate, DbType.DateTime, entity.UtcLastScheduledRunDate));
                parameters.Add(DataAccess.CreateParameter(DBCEmail, DbType.String, entity.Email));
                parameters.Add(DataAccess.CreateParameter(DBCFTPServerIP, DbType.String, entity.FTPServerIP));
                parameters.Add(DataAccess.CreateParameter(DBCFTPilePath, DbType.String, entity.FTPFilePath));
                parameters.Add(DataAccess.CreateParameter(DBCFTPUsername, DbType.String, entity.FTPUsername));
                parameters.Add(DataAccess.CreateParameter(DBCFTPPassword, DbType.String, string.IsNullOrWhiteSpace(entity.FTPPassword) ? null : EmailHelper.EncodePassword(entity.FTPPassword)));
                parameters.Add(DataAccess.CreateParameter(DBCIsSFTP, DbType.Boolean, entity.IsSFTP));
                DataAccess.ExecuteNonQuery(this.ConnectionString, SPSaveReportSchedule, parameters);
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
        /// <param name="key">Identity of the entity to delete.</param>
        /// <param name="modifiedBy">The modified by.</param>
        public override void DeleteEntity(int key, int modifiedBy)
        {

            List<DbParameter> parameters = base.GetDeleteParameters(modifiedBy) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCReportScheduleId, DbType.Int32, key));
                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeleteReportSchedule, parameters);
            }
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(ReportScheduleObjectModel entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(ReportScheduleObjectModel entity, int deletedBy)
        {
            throw new NotImplementedException();
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
                switch (entityRow.Field<string>("Entity"))
                {
                    case "GetAllEntities":
                        entity.ReportName = entityRow.Field<string>(DBCReportName);
                        entity.IsEnabled = entityRow.Field<bool>(DBCIsEnabled);
                        entity.FrequencyType = entityRow.Field<int>(DBCFrequencyType);
                        entity.FrequencyInterval = entityRow.Field<int>(DBCFrequencyInterval);
                        entity.UtcFirstScheduledRunDate = entityRow.Field<DateTime>(DBCUtcFirstScheduledRunDate);
                        entity.UtcLastActualRunDate = entityRow.Field<DateTime?>(DBCUtcLastActualRunDate);
                        entity.UtcNextScheduledRunDate = entityRow.Field<DateTime?>(DBCUtcNextScheduledRunDate);
                        break;
                    case "GetEntitiesBySearch":
                        entity.ReportScheduleId = entityRow.Field<int>(DBCReportScheduleId);
                        entity.Key = entityRow.Field<int>(DBCReportScheduleId);
                        entity.ReportId = entityRow.Field<int>(DBCReportId);
                        entity.ReportName = entityRow.Field<string>(DBCReportName);
                        entity.ClientId = entityRow.Field<int>(DBCClientId);
                        entity.IsEnabled = entityRow.Field<bool>(DBCIsEnabled);
                        entity.FrequencyType = entityRow.Field<int>(DBCFrequencyType);
                        entity.FrequencyInterval = entityRow.Field<int>(DBCFrequencyInterval);
                        entity.UtcFirstScheduledRunDate = entityRow.Field<DateTime>(DBCUtcFirstScheduledRunDate);
                        entity.UtcNextScheduledRunDate = entityRow.Field<DateTime?>(DBCUtcNextScheduledRunDate);
                        entity.UtcLastActualRunDate = entityRow.Field<DateTime?>(DBCUtcLastActualRunDate);
                        break;
                    case "GetEntityByKey":
                        entity.ReportScheduleId = entityRow.Field<int>(DBCReportScheduleId);
                        entity.Key = entityRow.Field<int>(DBCReportScheduleId);
                        entity.ReportId = entityRow.Field<int>(DBCReportId);
                        entity.ReportName = entityRow.Field<string>(DBCReportName);
                        entity.ClientId = entityRow.Field<int>(DBCClientId);
                        entity.IsEnabled = entityRow.Field<bool>(DBCIsEnabled);
                        entity.FrequencyType = entityRow.Field<int>(DBCFrequencyType);
                        entity.FrequencyInterval = entityRow.Field<int>(DBCFrequencyInterval);
                        entity.UtcFirstScheduledRunDate = entityRow.Field<DateTime>(DBCUtcFirstScheduledRunDate);
                        entity.UtcLastActualRunDate = entityRow.Field<DateTime?>(DBCUtcLastActualRunDate);
                        entity.UtcNextScheduledRunDate = entityRow.Field<DateTime?>(DBCUtcNextScheduledRunDate);
                        entity.FrequencyDescription = entityRow.Field<string>(DBCFrequencyDescription);
                        entity.Email = entityRow.Field<string>(DBCEmail);
                        entity.FTPServerIP = entityRow.Field<string>(DBCFTPServerIP);
                        entity.FTPFilePath = entityRow.Field<string>(DBCFTPilePath);
                        entity.FTPUsername = entityRow.Field<string>(DBCFTPUsername);
                        entity.FTPPassword = entityRow.Field<string>(DBCFTPPassword);
                        entity.IsSFTP = entityRow.Field<bool>(DBCIsFTPTransfer);
                        break;
                    case "GetAllReports":
                        entity.ReportId = entityRow.Field<int>(DBCReportId);
                        entity.ReportName = entityRow.Field<string>(DBCReportName);
                        break;
                }

            }

            return entity;
        }

    }
}
