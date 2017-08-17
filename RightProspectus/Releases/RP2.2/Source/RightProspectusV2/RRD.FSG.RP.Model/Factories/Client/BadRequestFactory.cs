// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-29-2015
//
// Last Modified By : 
// Last Modified On : 11-03-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Enumerations;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Model.SearchEntities.System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RRD.FSG.RP.Model.Factories.Client
{
    /// <summary>
    /// Class BadRequestFactory.
    /// </summary>
    public class BadRequestFactory : BaseFactory<SiteActivityObjectModel, int>
    {


        #region Constants
        /// <summary>
        /// The sp get all
        /// </summary>
        private const string SPGetAllBadRequestReports = "RPV2HostedAdmin_GetErrorActivityReport";
        /// <summary>
        /// SiteActivityId
        /// </summary>
        private const string DBCSiteActivityId = "SiteActivityId";
        /// <summary>
        /// RequestIssue
        /// </summary>
        private const string DBCRequestIssue = "BadRequestIssue";
        /// <summary>
        /// RequestParameterName
        /// </summary>
        private const string DBCParameterName = "BadRequestParameterName";
        /// <summary>
        /// RequestParameterValue
        /// </summary>
        private const string DBCParameterValue = "BadRequestParameterValue";
        /// <summary>
        /// ClientIPAddress
        /// </summary>
        private const string DBCClientIPAddress = "ClientIPAddress";
        /// <summary>
        /// UserAgentString
        /// </summary>
        private const string DBCUserAgentString = "UserAgentString";

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public BadRequestFactory(IDataAccess dataAccess)
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
                entity.BadRequestIssue = entityRow.Field<int>(DBCRequestIssue);
                entity.BadRequestParameterName = entityRow.Field<string>(DBCParameterName);
                entity.BadRequestParameterValue = entityRow.Field<string>(DBCParameterValue);
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
            return this.GetEntitiesInternal<TEntity>(SPGetAllBadRequestReports, startRowIndex, maximumRows, null);
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
            return this.GetEntitiesInternal<TEntity>(SPGetAllBadRequestReports, startRowIndex, maximumRows, sort);
        }

        /// <summary>
        /// Gets all bad requests for reportby date.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns>IEnumerable&lt;SiteActivityObjectModel&gt;.</returns>
        public IEnumerable<SiteActivityObjectModel> GetAllBadRequestsForReportbyDate(DateTime from, DateTime to)
        {
            DataTable results = DataAccess.ExecuteDataTable(DBConnectionString.HostedConnectionString(ClientName, DataAccess),
                        SPGetAllBadRequestReports,

                          new SqlParameter() { ParameterName = "FromDate", Value = from.ToString("yyyy-MM-dd HH:mm:ss"), SqlDbType = SqlDbType.DateTime },
                         new SqlParameter() { ParameterName = "ToDate", Value = to.ToString("yyyy-MM-dd HH:mm:ss"), SqlDbType = SqlDbType.DateTime }
                        );
            List<SiteActivityObjectModel> list = new List<SiteActivityObjectModel>();
            foreach (DataRow datarow in results.Rows)
            {
                SiteActivityObjectModel model = new SiteActivityObjectModel();
                model.SiteActivityId = Convert.ToInt32(datarow["SiteActivityId"]);
                model.SiteName = Convert.ToString(datarow["SiteName"]);
                model.BadRequestIssue = Convert.ToInt32(datarow["BadRequestIssue"]);
                model.BadRequestIssueDescription = Enum.GetName(typeof(BadRequestType), model.BadRequestIssue);
                model.BadRequestParameterName = Convert.ToString(datarow["BadRequestParameterName"]);
                model.BadRequestParameterValue = Convert.ToString(datarow["BadRequestParameterValue"]);
                model.ReferrerUriString = Convert.ToString(datarow["UriString"]);
                model.RequestUtcDate = Convert.ToDateTime(datarow["RequestUtcDate"]);
                model.ClientIPAddress = Convert.ToString(datarow["ClientIPAddress"]);
                model.UserAgentString = Convert.ToString(datarow["UserAgentString"]);

                list.Add(model);
            }
            return list;
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
            return (IEnumerable<TEntity>)GetAllBadRequestsForReportbyDate(searchObj.DateFrom, searchObj.DateTo);
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
        /// <exception cref="System.NotImplementedException"></exception>
        public override void SaveEntity(SiteActivityObjectModel entity, int modifiedBy)
        {
            throw new NotImplementedException();
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
    }
}
