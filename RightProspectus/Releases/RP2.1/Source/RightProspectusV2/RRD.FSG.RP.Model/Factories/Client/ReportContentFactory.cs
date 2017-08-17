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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Factories.Client
{
    /// <summary>
    /// Class ReportContentFactory.
    /// </summary>
    public class ReportContentFactory : AuditedBaseFactory<ReportContentObjectModel, int>
    {
        #region Constants

        /// <summary>
        /// The sp get ReportContent
        /// </summary>
        private const string SPGetAllReportContent = "RPV2HostedAdmin_GetAllReportContent";
        /// <summary>
        /// The sp save ReportContent
        /// </summary>
        private const string SPSaveReportContent = "RPV2HostedAdmin_SaveReportContent";
        /// <summary>
        /// The sp deletes ReportContent
        /// </summary>
        private const string SPDeleteReportContent = "RPV2HostedAdmin_DeleteReportContent";

        /// <summary>
        /// ReportContenttId
        /// </summary>
        private const string DBCReportContenttId = "ReportContenttId";
        /// <summary>
        /// FileName
        /// </summary>
        private const string DBCFileName = "FileName";
        /// <summary>
        /// ReportScheduleId
        /// </summary>
        private const string DBCReportScheduleId = "ReportScheduleId";
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
        /// ReportRunDate
        /// </summary>
        private const string DBCReportRunDate = "ReportRunDate";
        /// <summary>
        /// FileData
        /// </summary>
        private const string DBCData = "Data";

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the ReportContentFactory class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public ReportContentFactory(IDataAccess dataAccess)
            : base(dataAccess) { }
        #endregion

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
            return this.GetEntitiesInternal<TEntity>(SPGetAllReportContent, startRowIndex, maximumRows, null);
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
            return this.GetEntitiesInternal<TEntity>(SPGetAllReportContent, startRowIndex, maximumRows, sort);
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
        public override void SaveEntity(ReportContentObjectModel entity, int modifiedBy)
        {
            base.SetModifiedBy(entity, modifiedBy);
            this.SaveEntity(entity);
        }

        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        public override void SaveEntity(ReportContentObjectModel entity)
        {
            List<DbParameter> parameters = base.GetParametersFromEntity<ReportContentObjectModel>(entity) as List<DbParameter>;

            if (parameters != null)
            {

                parameters.Add(DataAccess.CreateParameter(DBCFileName, DbType.String, entity.FileName));

                parameters.Add(DataAccess.CreateParameter(DBCReportScheduleId, DbType.Int32, entity.ReportScheduleId));

                parameters.Add(DataAccess.CreateParameter(DBCMimeType, DbType.String, entity.MimeType));

                parameters.Add(DataAccess.CreateParameter(DBCIsPrivate, DbType.Int32, entity.IsPrivate));

                parameters.Add(DataAccess.CreateParameter(DBCContentUri, DbType.String, entity.ContentUri));

                parameters.Add(DataAccess.CreateParameter(DBCName, DbType.String, entity.Name));

                parameters.Add(DataAccess.CreateParameter(DBCDescription, DbType.String, entity.Description));

                parameters.Add(DataAccess.CreateParameter(DBCReportRunDate, DbType.DateTime, entity.ReportRunDate));

                parameters.Add(DataAccess.CreateParameter(DBCData, SqlDbType.VarBinary, entity.Data));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPSaveReportContent, parameters);

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
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        /// <param name="modifiedBy">The modified by.</param>
        public override void DeleteEntity(int key, int modifiedBy)
        {
            List<DbParameter> parameters = base.GetDeleteParameters(modifiedBy) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCReportContenttId, DbType.Int32, key));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeleteReportContent, parameters);
            }
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(ReportContentObjectModel entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        public override void DeleteEntity(ReportContentObjectModel entity, int deletedBy)
        {
                //parameters.Add(DataAccess.CreateParameter(DBCReportContenttId, DbType.Int32, entity.ReportContenttId));

            List<DbParameter> parameters = base.GetDeleteParameters(deletedBy) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCReportContenttId, DbType.Int32, entity.ReportContentId));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeleteReportContent, parameters);
            }
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
                entity.ReportContentId = entityRow.Field<int>(DBCReportContenttId);
                entity.Key = entity.ReportContentId;
                entity.FileName = entityRow.Field<string>(DBCFileName);
                entity.ReportScheduleId = entityRow.Field<int>(DBCReportScheduleId);
                entity.MimeType = entityRow.Field<string>(DBCMimeType);
                entity.IsPrivate = entityRow.Field<int>(DBCIsPrivate);
                entity.ContentUri = entityRow.Field<string>(DBCContentUri);
                entity.Name = entityRow.Field<string>(DBCName);
                entity.Description = entityRow.Field<string>(DBCDescription);
                entity.ReportRunDate = entityRow.Field<DateTime>(DBCReportRunDate);
            }
            return entity;
        }
    }
}
