// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-03-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Model.SearchEntities.System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace RRD.FSG.RP.Model.Factories.Client
{
    /// <summary>
    /// Class SiteActivityFactory.
    /// </summary>
    public class SiteActivityFactory: BaseFactory<SiteActivityObjectModel, int>
    {
        #region Constants
        /// <summary>
        /// The sp get all SiteActivity
        /// </summary>
        private const string SPGetAllSiteActivity = "RPV2HostedAdmin_GetAllSiteActivity";

        /// <summary>
        /// The sp get all SiteActivity
        /// </summary>
        private const string SPGetAllSiteActivityForReport = "RPV2HostedAdmin_GetSiteActivityReport";

        /// <summary>
        /// The sp Save SiteActivity
        /// </summary>
        private const string SPSaveSiteActivity = "RPV2HostedSites_SaveSiteActivity";

        /// <summary>
        /// SiteActivityId
        /// </summary>
        private const string DBCSiteActivityId = "SiteActivityId";

        /// <summary>
        /// SiteName
        /// </summary>
        private const string DBCSiteName = "SiteName";

        /// <summary>
        /// ClientIPAddress
        /// </summary>
        private const string DBCClientIPAddress = "ClientIPAddress";

        /// <summary>
        /// UserAgentString
        /// </summary>
        private const string DBCUserAgentString = "UserAgentString";

        /// <summary>
        /// HTTPMethod
        /// </summary>
        private const string DBCHTTPMethod = "HTTPMethod";

        /// <summary>
        /// RequestUri
        /// </summary>
        private const string DBCRequestUriString = "RequestUriString";

        /// <summary>
        /// ParsedRequestUriString
        /// </summary>
        private const string DBCParsedRequestUriString = "ParsedRequestUriString";

        /// <summary>
        /// ServerName
        /// </summary>
        private const string DBCServerName = "ServerName";

        /// <summary>
        /// ReferrerUri
        /// </summary>
        private const string DBCReferrerUriString = "ReferrerUriString";

        /// <summary>
        /// UserId
        /// </summary>
        private const string DBCUserId = "UserId";

        /// <summary>
        /// PageId
        /// </summary>
        private const string DBCPageId = "PageId";

        /// <summary>
        /// Level
        /// </summary>
        private const string DBCLevel = "Level";
        /// <summary>
        /// DocumentTypeExternalId
        /// </summary>
        private const string DBCDocumentTypeExternalId = "DocumentTypeExternalId";

        /// <summary>
        /// TaxonomyExternalId
        /// </summary>
        private const string DBCTaxonomyExternalId = "TaxonomyExternalId";

        /// <summary>
        /// TaxonomyAssociationGroupId
        /// </summary>
        private const string DBCTaxonomyAssociationGroupId = "TaxonomyAssociationGroupId";

        /// <summary>
        /// TaxonomyAssociationId
        /// </summary>
        private const string DBCTaxonomyAssociationId = "TaxonomyAssociationId";

        /// <summary>
        /// DocumentTypeId
        /// </summary>
        private const string DBCDocumentTypeId = "DocumentTypeId";

        /// <summary>
        /// ClientDocumentGroupId
        /// </summary>
        private const string DBCClientDocumentGroupId = "ClientDocumentGroupId";

        /// <summary>
        /// ClientDocumentId
        /// </summary>
        private const string DBCClientDocumentId = "ClientDocumentId";

        /// <summary>
        /// RequestUniqueIdentifier
        /// </summary>
        private const string DBCRequestBatchId = "RequestBatchId";

        /// <summary>
        /// InitDoc
        /// </summary>
        private const string DBCInitDoc = "InitDoc";

        /// <summary>
        /// XBRLDocumentName
        /// </summary>
        private const string DBCXBRLDocumentName = "XBRLDocumentName";

        /// <summary>
        /// XBRLItemType
        /// </summary>
        private const string DBCXBRLItemType = "XBRLItemType";

        /// <summary>
        /// XBRLItemType
        /// </summary>
        private const string DBCBadRequestIssue = "BadRequestIssue";

        /// <summary>
        /// XBRLItemType
        /// </summary>
        private const string DBCBadRequestParameterName = "BadRequestParameterName";

        /// <summary>
        /// XBRLItemType
        /// </summary>
        private const string DBCBadRequestParameterValue = "BadRequestParameterValue";



        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public SiteActivityFactory(IDataAccess dataAccess)
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
                entity.SiteActivityId = entityRow.Field<int>(DBCSiteActivityId);
                entity.ClientIPAddress = entityRow.Field<string>(DBCClientIPAddress);
                entity.UserAgentString = entityRow.Field<string>(DBCUserAgentString);
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
            return this.GetEntitiesInternal<TEntity>(SPGetAllSiteActivity, startRowIndex, maximumRows,null);
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
            return this.GetEntitiesInternal<TEntity>(SPGetAllSiteActivity, startRowIndex, maximumRows,sort);
        }

        /// <summary>
        /// Gets all site activity for reportby date.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns>IEnumerable&lt;SiteActivityObjectModel&gt;.</returns>
        public IEnumerable<SiteActivityObjectModel> GetAllSiteAcitivityForReportbyDate(DateTime from, DateTime to)
        {
            DataTable results = DataAccess.ExecuteDataTable(DBConnectionString.HostedConnectionString(ClientName, DataAccess),
                        SPGetAllSiteActivityForReport,
                         new SqlParameter() { ParameterName = "FromDate", Value = from.ToString("yyyy-MM-dd HH:mm:ss"), SqlDbType = SqlDbType.DateTime },
                         new SqlParameter() { ParameterName = "ToDate", Value = to.ToString("yyyy-MM-dd HH:mm:ss"), SqlDbType = SqlDbType.DateTime }
                        );
            List<SiteActivityObjectModel> list = new List<SiteActivityObjectModel>();
            foreach (DataRow datarow in results.Rows)
            {
                SiteActivityObjectModel model = new SiteActivityObjectModel();
                model.ReferrerUriString = Convert.ToString(datarow["UriString"]);
                model.RequestBatchId =new Guid(Convert.ToString(datarow["RequestBatchId"]));
                model.SiteName = Convert.ToString(datarow["SiteName"]);
                model.Click = Convert.ToInt32(datarow["Click"]);
                model.SiteId = Convert.ToInt32(datarow["SiteId"]);
                model.TaxonomyAssociationId = Convert.ToInt32(datarow["TaxonomyAssociationId"]);
                model.Name = Convert.ToString(datarow["NameOverride"]);
                model.RequestUtcDate = Convert.ToDateTime(datarow["RequestUtcDate"]);
                model.ClientIPAddress = Convert.ToString(datarow["ClientIPAddress"]);
                model.TaxonomyAssociationGroupId = datarow.Field<int?>("TaxonomyAssociationGroupId");
                model.DocumentTypeId = datarow.Field<int?>("DocumentTypeId");
                model.DocumentType = Convert.ToString(datarow["HeaderText"]);           
                model.DocumentTypeMarketId = Convert.ToString(datarow["MarketId"]);
                model.InitDoc = Convert.ToBoolean(datarow["InitDoc"]);
                list.Add(model);
            }
            return list;
        }
        
        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        public override void SaveEntity(SiteActivityObjectModel entity)
        {
            List<DbParameter> parameters = base.GetParametersFromEntity<SiteActivityObjectModel>(entity) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCSiteName, DbType.String, entity.SiteName));
                parameters.Add(DataAccess.CreateParameter(DBCClientIPAddress, DbType.String, entity.ClientIPAddress));
                parameters.Add(DataAccess.CreateParameter(DBCUserAgentString, DbType.String, entity.UserAgentString));
                parameters.Add(DataAccess.CreateParameter(DBCHTTPMethod, DbType.String, entity.HTTPMethod));
                parameters.Add(DataAccess.CreateParameter(DBCRequestUriString, DbType.String, entity.RequestUriString));
                parameters.Add(DataAccess.CreateParameter(DBCParsedRequestUriString, DbType.String, entity.ParsedRequestUriString));
                parameters.Add(DataAccess.CreateParameter(DBCServerName, DbType.String, entity.ServerName));
                parameters.Add(DataAccess.CreateParameter(DBCReferrerUriString, DbType.String, entity.ReferrerUriString));
                parameters.Add(DataAccess.CreateParameter(DBCInitDoc, DbType.Boolean, entity.InitDoc));
                parameters.Add(DataAccess.CreateParameter(DBCRequestBatchId, DbType.Guid, entity.RequestBatchId));

                parameters.Add(DataAccess.CreateParameter(DBCUserId, DbType.Int32, entity.UserId));

                parameters.Add(DataAccess.CreateParameter(DBCPageId, DbType.Int32, entity.PageId));

                parameters.Add(DataAccess.CreateParameter(DBCLevel, DbType.Int32, entity.Level));

                parameters.Add(DataAccess.CreateParameter(DBCDocumentTypeExternalId, DbType.String, entity.DocumentTypeExternalID));

                parameters.Add(DataAccess.CreateParameter(DBCTaxonomyExternalId, DbType.String, entity.TaxonomyExternalId));

                parameters.Add(DataAccess.CreateParameter(DBCTaxonomyAssociationGroupId, DbType.Int32, entity.TaxonomyAssociationGroupId));

                parameters.Add(DataAccess.CreateParameter(DBCTaxonomyAssociationId, DbType.Int32, entity.TaxonomyAssociationId));

                parameters.Add(DataAccess.CreateParameter(DBCDocumentTypeId, DbType.Int32, entity.DocumentTypeId));

                parameters.Add(DataAccess.CreateParameter(DBCClientDocumentGroupId, DbType.Int32, entity.ClientDocumentGroupId));

                parameters.Add(DataAccess.CreateParameter(DBCClientDocumentId, DbType.Int32, entity.ClientDocumentId));

                parameters.Add(DataAccess.CreateParameter(DBCXBRLDocumentName, DbType.String, entity.XBRLDocumentName));

                parameters.Add(DataAccess.CreateParameter(DBCXBRLItemType, DbType.Int32, entity.XBRLItemType));

                parameters.Add(DataAccess.CreateParameter(DBCBadRequestIssue, DbType.Int32, entity.BadRequestIssue));

                parameters.Add(DataAccess.CreateParameter(DBCBadRequestParameterName, DbType.String, entity.BadRequestParameterName));

                parameters.Add(DataAccess.CreateParameter(DBCBadRequestParameterValue, DbType.String, entity.BadRequestParameterValue));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPSaveSiteActivity, parameters);
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
        public override void SaveEntity(SiteActivityObjectModel entity, int modifiedBy)
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
        public override void DeleteEntity(SiteActivityObjectModel entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(SiteActivityObjectModel entity, int deletedBy)
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
        /// <param name="entitiesToIgnore">Optional parameterized list of entities to ignore while searching.</param>
        /// <returns>A collection of entities matching the search details.</returns>
        public override IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, ISortDetail<TEntity> sort, params int[] entitiesToIgnore)
        {
            SiteActivitySearchDetail searchObj = new SiteActivitySearchDetail();
            searchObj = (SiteActivitySearchDetail)search;
            return (IEnumerable<TEntity>)GetAllSiteAcitivityForReportbyDate(searchObj.DateFrom, searchObj.DateTo);            
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
