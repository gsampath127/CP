// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-03-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.VerticalMarket;
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;

namespace RRD.FSG.RP.Model.Factories.VerticalMarket
{
    /// <summary>
    /// Class DocumentTypeFactory.
    /// </summary>
    public class DocumentTypeFactory : BaseFactory<DocumentTypeObjectModel, int>
    {
        #region Constants
        /// <summary>
        /// The sp get all DocumentType
        /// </summary>
        private const string SPGetAllDocumentType = "RPV2HostedAdmin_GetAllDocumentType";

        /// <summary>
        /// DocumentTypeId
        /// </summary>
        private const string DBCDocumentTypeId = "DocumentTypeId";

        /// <summary>
        /// DocumentTypeName
        /// </summary>
        private const string DBCDocumentTypeName = "DocumentTypeName";

        /// <summary>
        /// DocumentTypeName
        /// </summary>
        private const string DBCMarketID = "MarketID";

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public DocumentTypeFactory(IDataAccess dataAccess)
            : base(dataAccess)
        {

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

                entity.DocumentTypeId = entityRow.Field<int>(DBCDocumentTypeId);
                entity.Key = entityRow.Field<int>(DBCDocumentTypeId);
                entity.Name = entityRow.Field<string>(DBCDocumentTypeName);
                entity.MarketId = entityRow.Field<string>(DBCMarketID);
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
        /// Gets all entities.
        /// </summary>
        /// <typeparam name="DocumentTypeObjectModel">The type of the document type object model.</typeparam>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <returns>IEnumerable&lt;DocumentTypeObjectModel&gt;.</returns>
        public override IEnumerable<DocumentTypeObjectModel> GetAllEntities<DocumentTypeObjectModel>(int startRowIndex, int maximumRows)
        {
            // return this.GetEntitiesInternal<TEntity>(SPGetAllDocumentType, startRowIndex, maximumRows,null);

            DataTable DocumentTypesDatatable = this.DataAccess.ExecuteDataTable(this.VerticalMarketConnectionString, SPGetAllDocumentType);

            List<DocumentTypeObjectModel> lstDoctypes = new List<DocumentTypeObjectModel>();
            DocumentTypeObjectModel objDocType = null;
            foreach (DataRow dRow in DocumentTypesDatatable.Rows)
            {
                objDocType = new DocumentTypeObjectModel();
                objDocType.DocumentTypeId = dRow.Field<int>(DBCDocumentTypeId);
                objDocType.Name = dRow.Field<string>(DBCDocumentTypeName);
                objDocType.Key = dRow.Field<int>(DBCDocumentTypeId);
                objDocType.MarketId = dRow.Field<string>(DBCMarketID);
                lstDoctypes.Add(objDocType);
            }

            return lstDoctypes;
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
            return this.GetEntitiesInternal<TEntity>(SPGetAllDocumentType, startRowIndex, maximumRows, sort);
        }



        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void SaveEntity(DocumentTypeObjectModel entity)
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
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void SaveEntity(DocumentTypeObjectModel entity, int modifiedBy)
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
        public override void DeleteEntity(DocumentTypeObjectModel entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(DocumentTypeObjectModel entity, int deletedBy)
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
