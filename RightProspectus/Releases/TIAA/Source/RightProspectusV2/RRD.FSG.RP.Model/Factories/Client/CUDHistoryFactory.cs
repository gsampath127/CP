// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-27-2015
//
// Last Modified By : 
// Last Modified On : 11-17-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Model.Keys;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SortDetail.Client;
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
    /// Class CUDHistoryFactory.
    /// </summary>
    public class CUDHistoryFactory : BaseFactory<CUDHistoryObjectModel, CUDHistoryKey>
    {
        #region Constants
        /// <summary>
        /// declaration of all Strings/Procedures used as constants
        /// </summary>
        private const string SPGetAllCUDHistory = "RPV2HostedAdmin_GetAllCUDHistory";
        /// <summary>
        /// The sp getCUDHistory
        /// </summary>
        private const string SPGetCUDHistory = "RPV2HostedAdmin_GetCUDHistory";

        /// <summary>
        /// The sp getCUDHistoryData
        /// </summary>
        private const string SPGetCUDHistoryData = "RPV2HostedAdmin_GetCUDHistoryDataById";

        /// <summary>
        /// CudHistoryId
        /// </summary>
        private const string DBCCUDHistoryId = "CUDHistoryId";
        /// <summary>
        /// Table Name
        /// </summary>
        private const string DBCTableName = "TableName";

        /// <summary>
        /// Date
        /// </summary>
        private const string DBCCUDDate = "UtcCUDDate";

        /// <summary>
        /// Type
        /// </summary>
        private const string DBCCUDType = "CUDType";

        /// <summary>
        /// Type
        /// </summary>
        private const string DBCColumnName = "ColumnName";

        /// <summary>
        /// OldValue
        /// </summary>
        private const string DBCOldValue = "OldValue";

        /// <summary>
        /// NewValue
        /// </summary>
        private const string DBCNewValue = "NewValue";

        /// <summary>
        /// DBCNewValueBinary
        /// </summary>
        private const string DBCNewValueBinary = "NewValueBinary";

        /// <summary>
        /// DBCOldValueBinary
        /// </summary>
        private const string DBCOldValueBinary = "OldValueBinary";

        /// <summary>
        /// DBType
        /// </summary>
        private const string DBCSqlDbType = "SqlDbType";
        /// <summary>
        /// UserId
        /// </summary>
        private const string DBCUserId = "UserId";
        /// <summary>
        /// IsAdmin
        /// </summary>
        private const string DBIsAdmin = "isAdmin";
        /// <summary>
        /// IsTableName
        /// </summary>
        private const string DBIsTableName = "isTableName";
        /// <summary>
        /// IsCUDHistory
        /// </summary>
        private const string DBIsCUDHistory = "isCUDHistory";
        /// <summary>
        /// IsCUDHistoryData
        /// </summary>
        private const string DBIsCUDHistoryData = "isCUDHistoryData";
        /// <summary>
        /// CUDHistory
        /// </summary>
        private const string DBCUDHistory = "CUDHistory";



        #region StoredProcedureParameters
        /// <summary>
        /// CUD from date
        /// </summary>
        private const string DBCCUDFromDate = "UTCFromCUDDate";
        /// <summary>
        /// CUD To Date
        /// </summary>
        private const string DBCCUDToDate = "UTCToCUDDate";
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


        #endregion



        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteAdministrationFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public CUDHistoryFactory(IDataAccess dataAccess)
            : base(dataAccess) { }
        #endregion

        /// <summary>
        /// Retrieves entity details from the underlying data access and uses them to populate an entity instance.
        /// </summary>
        /// <param name="key">Primary key identifier of the entity to retrieve.</param>
        /// <returns>A new instance of type <typeparamref name="TModel" /> if found. Null otherwise.</returns>
        public override CUDHistoryObjectModel GetEntityByKey(CUDHistoryKey key)
        {
            return GetCUDHistoryEntity(key);
        }

        /// <summary>
        /// Retrieves a specific taxonomy level entity using the passed in hierarchical level and identifier.
        /// </summary>
        /// <param name="key">The taxonomy key of the entity to retrieve. Consists of Level and TaxonomyId.</param>
        /// <returns>A <see cref="TaxonomyEntity" /> entity.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public CUDHistoryObjectModel GetCUDHistoryEntity(CUDHistoryKey key)
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
            DataTable result = new DataTable(DBCUDHistory);
            result = DataAccess.ExecuteDataTable(this.ConnectionString, SPGetAllCUDHistory, null);
            result.Columns.Add(DBIsCUDHistory);
            for (int i = 0; i < result.Rows.Count; i++)
            {
                result.Rows[i][2] = DBIsTableName;
            }
            return this.CreateEntities<TEntity>(result);
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
            DataTable result = new DataTable(DBCUDHistory);
            result = DataAccess.ExecuteDataTable(this.ConnectionString, SPGetAllCUDHistory, null);
            result.Columns.Add(DBIsCUDHistory);
            for (int i = 0; i < result.Rows.Count; i++)
            {
                result.Rows[i][2] = DBIsTableName;
            }
            return this.CreateEntities<TEntity>(result);
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

                if (entityRow.Field<string>(DBIsCUDHistory) == DBIsCUDHistory)
                {
                    entity.CUDHistoryId = entityRow.Field<int>(DBCCUDHistoryId);
                    entity.TableName = entityRow.Field<string>(DBCTableName);
                    entity.UtcCUDDate = entityRow.Field<DateTime>(DBCCUDDate);
                    entity.CUDType = entityRow.Field<string>(DBCCUDType);
                    if (entityRow.Table.Columns.Contains(DBCUserId))
                    {
                        if (entityRow[DBCUserId] != null && entityRow[DBCUserId].ToString() != "")
                            entity.UserId = entityRow.Field<int>(DBCUserId);
                    }
                }

                else if (entityRow.Field<string>(DBIsCUDHistory) == DBIsCUDHistoryData)
                {
                    entity.CUDHistoryId = entityRow.Field<int>(DBCCUDHistoryId);
                    entity.ColumnName = entityRow.Field<string>(DBCColumnName);
                    entity.OldValue = entityRow.Field<string>(DBCOldValue);
                    entity.NewValue = entityRow.Field<string>(DBCNewValue);
                    entity.SqlDbType = entityRow.Field<int>(DBCSqlDbType);
                    if (entity.SqlDbType == 165)
                    {
                        byte[] imageByteData = entityRow.Field<byte[]>(DBCNewValueBinary);
                        string imageBase64Data = string.Empty;
                        string imageDataURL = string.Empty;
                        if (imageByteData != null)
                        {
                            imageBase64Data = Convert.ToBase64String(imageByteData);
                            imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
                            entity.NewImageDataURL = imageDataURL;
                        }

                        imageByteData = entityRow.Field<byte[]>(DBCOldValueBinary);
                        if (imageByteData != null)
                        {
                            imageBase64Data = Convert.ToBase64String(imageByteData);
                            imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
                            entity.OldImageDataURL = imageDataURL;
                        }
                        entity.IsBinaryImage = true;
                    }

                }
                else if (entityRow.Field<string>(DBIsCUDHistory) == DBIsTableName)
                {
                    entity.TableName = entityRow.Field<string>(DBCTableName);
                    if (entityRow[DBCUserId] != null && entityRow[DBCUserId].ToString() != "")
                    entity.UserId = entityRow.Field<int>(DBCUserId);
                }

            }
            return entity;
        }


        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void SaveEntity(CUDHistoryObjectModel entity, int modifiedBy)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(CUDHistoryObjectModel entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(CUDHistoryObjectModel entity, int deletedBy)
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
        public override IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, ISortDetail<TEntity> sort, out int totalRecordCount, params CUDHistoryKey[] entitiesToIgnore)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            CUDHistorySearchDetail searchDetails = search as CUDHistorySearchDetail;

            CUDHistorySortDetail sortDetails = sort as CUDHistorySortDetail;
            totalRecordCount = 0;
            DataSet result = new DataSet(DBCUDHistory);
            if (parameters != null)
            {

                if (!searchDetails.IsHistoryData)
                {
                    parameters.Add(DataAccess.CreateParameter(DBCCUDHistoryId, DbType.Int32, searchDetails.CUDHistoryId));
                    parameters.Add(DataAccess.CreateParameter(DBCTableName, DbType.String, searchDetails.TableName));
                    parameters.Add(DataAccess.CreateParameter(DBCCUDType, DbType.String, searchDetails.CUDType));
                    parameters.Add(DataAccess.CreateParameter(DBCCUDFromDate, DbType.DateTime, searchDetails.UtcCUDDateFrom));
                    parameters.Add(DataAccess.CreateParameter(DBCCUDToDate, DbType.DateTime, searchDetails.UtcCUDDateTo));
                    parameters.Add(DataAccess.CreateParameter(DBCPageSize, DbType.Int32, maximumRows));
                    parameters.Add(DataAccess.CreateParameter(DBCPageIndex, DbType.Int32, startRowIndex));
                    parameters.Add(DataAccess.CreateParameter(DBCSortDirection, DbType.String, sortDetails.Order));
                    parameters.Add(DataAccess.CreateParameter(DBCSortColumn, DbType.String, sortDetails.Column));
                    parameters.Add(DataAccess.CreateParameter(DBCUserId, DbType.Int32, searchDetails.UserId));
                    result = DataAccess.ExecuteDataSet(this.ConnectionString, SPGetCUDHistory, parameters.ToArray());
                    result.Tables[0].Columns.Add(DBIsCUDHistory);
                    for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                    {
                        result.Tables[0].Rows[i][6] = DBIsCUDHistory;
                    }
                    totalRecordCount = Convert.ToInt32(result.Tables[1].Rows[0][0]);
                }
                else
                {
                    parameters.Add(DataAccess.CreateParameter(DBCCUDHistoryId, DbType.Int32, searchDetails.CUDHistoryId));
                    parameters.Add(DataAccess.CreateParameter(DBCPageSize, DbType.Int32, maximumRows));
                    parameters.Add(DataAccess.CreateParameter(DBCPageIndex, DbType.Int32, startRowIndex));
                    parameters.Add(DataAccess.CreateParameter(DBCSortDirection, DbType.String, sortDetails.Order));
                    parameters.Add(DataAccess.CreateParameter(DBCSortColumn, DbType.String, sortDetails.Column));
                    result = DataAccess.ExecuteDataSet(this.ConnectionString, SPGetCUDHistoryData, parameters.ToArray());
                    result.Tables[0].Columns.Add(DBIsCUDHistory);
                    for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                    {
                        result.Tables[0].Rows[i][8] = DBIsCUDHistoryData;
                    }
                    totalRecordCount = Convert.ToInt32(result.Tables[1].Rows[0][0]);
                }


            }
            return this.CreateEntities<TEntity>(result.Tables[0]);
            
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(CUDHistoryKey key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(CUDHistoryKey key, int modifiedBy)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves entity details from the underlying data access and uses them to populate an entity instance.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="key">Primary key identifier of the entity to retrieve.</param>
        /// <returns>A new instance of type <typeparamref name="TModel" /> if found. Null otherwise.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override TEntity GetEntityByKey<TEntity>(CUDHistoryKey key)
        {
            throw new NotImplementedException();
        }
    }
}
