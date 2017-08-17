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

namespace RRD.FSG.RP.Model.Factories.Client
{
    /// <summary>
    /// Class TaxonomyAssociationFactory.
    /// </summary>
    public class TaxonomyAssociationFactory : AuditedBaseFactory<TaxonomyAssociationObjectModel, int>
    {
        #region Constants
        /// <summary>
        /// declartion of all Strings/Procedures used as constants
        /// </summary>
        private const string SPGetAllTaxonomyAssociation = "RPV2HostedAdmin_GetAllTaxonomyAssociation";
        /// <summary>
        /// The sp save TaxonomyAssociation
        /// </summary>
        private const string SPSaveTaxonomyAssociation = "RPV2HostedAdmin_SaveTaxonomyAssociation";
        /// <summary>
        /// The sp deletes TaxonomyAssociation
        /// </summary>
        private const string SPDeleteTaxonomyAssociation = "RPV2HostedAdmin_DeleteTaxonomyAssociation";

        /// <summary>
        /// TaxonomyAssociationId
        /// </summary>
        private const string DBCTaxonomyAssociationId = "TaxonomyAssociationId";
        /// <summary>
        /// Level
        /// </summary>
        private const string DBCLevel = "Level";
        /// <summary>
        /// TaxonomyId
        /// </summary>
        private const string DBCTaxonomyId = "TaxonomyId";
        /// <summary>
        /// SiteId
        /// </summary>
        private const string DBCSiteId = "SiteId";
        /// <summary>
        /// ParentTaxonomyAssociationId
        /// </summary>
        private const string DBCParentTaxonomyAssociationId = "ParentTaxonomyAssociationId";
        /// <summary>
        /// NameOverride
        /// </summary>
        private const string DBCNameOverride = "NameOverride";
        /// <summary>
        /// DescriptionOverride
        /// </summary>
        private const string DBCDescriptionOverride = "DescriptionOverride";
        /// <summary>
        /// CssClass
        /// </summary>
        private const string DBCCssClass = "CssClass";
        /// <summary>
        /// MarketId
        /// </summary>
        private const string DBCMarketId = "MarketId";
        /// <summary>
        /// IsProofing
        /// </summary>
        private const string DBCIsProofing = "IsProofing";
        /// <summary>
        /// Order
        /// </summary>
        private const string DBCOrder = "Order";
        /// <summary>
        /// TabbedPageNameOverride
        /// </summary>
        private const string DBCTabbedPageNameOverride = "TabbedPageNameOverride";


        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonomyAssociationFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public TaxonomyAssociationFactory(IDataAccess dataAccess)
            : base(dataAccess) { }
        #endregion

        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        public override IEnumerable<TEntity> GetAllEntities<TEntity>(int startRowIndex, int maximumRows)
        {
            return this.GetEntitiesInternal<TEntity>(SPGetAllTaxonomyAssociation, startRowIndex, maximumRows, null);
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
            return this.GetEntitiesInternal<TEntity>(SPGetAllTaxonomyAssociation, startRowIndex, maximumRows, sort);
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

                entity.TaxonomyAssociationId = entityRow.Field<int>(DBCTaxonomyAssociationId);
                entity.Key = entity.TaxonomyAssociationId;
                entity.Level = entityRow.Field<int>(DBCLevel);
                entity.TaxonomyId = entityRow.Field<int>(DBCTaxonomyId);
                entity.SiteId = entityRow.Field<int?>(DBCSiteId);
                entity.ParentTaxonomyAssociationId = entityRow.Field<int?>(DBCParentTaxonomyAssociationId);                
                entity.NameOverride = entityRow.Field<string>(DBCNameOverride);
                entity.DescriptionOverride = entityRow.Field<string>(DBCDescriptionOverride);
                entity.CssClass = entityRow.Field<string>(DBCCssClass);
                entity.MarketId = entityRow.Field<string>(DBCMarketId);
                entity.IsProofing = entityRow.Field<bool>(DBCIsProofing);
                entity.Order = entityRow.Field<int?>(DBCOrder);
                entity.TabbedPageNameOverride = entityRow.Field<string>(DBCTabbedPageNameOverride);

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
        public override void SaveEntity(TaxonomyAssociationObjectModel entity, int modifiedBy)
        {
            List<DbParameter> parameters = base.GetParametersFromEntity<TaxonomyAssociationObjectModel>(entity) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCTaxonomyAssociationId, DbType.Int32, entity.TaxonomyAssociationId));
                parameters.Add(DataAccess.CreateParameter(DBCLevel, DbType.Int32, entity.Level));
                parameters.Add(DataAccess.CreateParameter(DBCTaxonomyId, DbType.Int32, entity.TaxonomyId));
                parameters.Add(DataAccess.CreateParameter(DBCSiteId, DbType.Int32, entity.SiteId));
                parameters.Add(DataAccess.CreateParameter(DBCParentTaxonomyAssociationId, DbType.Int32, entity.ParentTaxonomyAssociationId));                
                parameters.Add(DataAccess.CreateParameter(DBCNameOverride, DbType.String, entity.NameOverride));
                parameters.Add(DataAccess.CreateParameter(DBCDescriptionOverride, DbType.String, entity.DescriptionOverride));
                parameters.Add(DataAccess.CreateParameter(DBCCssClass, DbType.String, entity.CssClass));
                parameters.Add(DataAccess.CreateParameter(DBCMarketId, DbType.String, entity.MarketId));
                parameters.Add(DataAccess.CreateParameter(DBCTabbedPageNameOverride, DbType.String, entity.TabbedPageNameOverride));
                DataAccess.ExecuteNonQuery(this.ConnectionString, SPSaveTaxonomyAssociation, parameters);
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
                parameters.Add(DataAccess.CreateParameter(DBCTaxonomyAssociationId, DbType.Int32, key));
                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeleteTaxonomyAssociation, parameters);
            }
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void DeleteEntity(TaxonomyAssociationObjectModel entity)
        {
            DeleteEntity(entity, 0);
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(TaxonomyAssociationObjectModel entity, int deletedBy)
        {
            throw new NotImplementedException();
        }

    }
}
