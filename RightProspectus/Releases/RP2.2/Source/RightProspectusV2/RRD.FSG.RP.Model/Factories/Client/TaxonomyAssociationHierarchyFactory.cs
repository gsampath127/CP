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
    /// Class TaxonomyAssociationHierarchyFactory.
    /// </summary>
    public class TaxonomyAssociationHierarchyFactory : AuditedBaseFactory<TaxonomyAssociationHierarchyObjectModel, TaxonomyAssociationHierarchyKey>
    {
        #region Constants
        /// <summary>
        /// declaration of all Strings/Procedures used as constants
        /// </summary>
        private const string SPGetAllTaxonomyAssociationHierarchy = "RPV2HostedAdmin_GetAllTaxonomyAssociationHierarchy";
        /// <summary>
        /// The sp save TaxonomyAssociationHierarchy
        /// </summary>
        private const string SPSaveTaxonomyAssociationHierarchy = "RPV2HostedAdmin_SaveTaxonomyAssociationHierarchy";
        /// <summary>
        /// The sp deletes TaxonomyAssociationHierarchy
        /// </summary>
        private const string SPDeleteTaxonomyAssociationHierarchy = "RPV2HostedAdmin_DeleteTaxonomyAssociationHierarchy";
        /// <summary>
        /// ParentTaxonomyAssociationId
        /// </summary>
        private const string DBCParentTaxonomyAssociationId = "ParentTaxonomyAssociationId";
        /// <summary>
        /// ChildTaxonomyAssociationId
        /// </summary>
        private const string DBCChildTaxonomyAssociationId = "ChildTaxonomyAssociationId";
        /// <summary>
        /// RelationshipType
        /// </summary>
        private const string DBCRelationshipType = "RelationshipType";
        /// <summary>
        /// Order
        /// </summary>
        private const string DBCOrder = "Order";


        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonomyAssociationHierarchyFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public TaxonomyAssociationHierarchyFactory(IDataAccess dataAccess)
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
            return this.GetEntitiesInternal<TEntity>(SPGetAllTaxonomyAssociationHierarchy, startRowIndex, maximumRows, null);
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
            return this.GetEntitiesInternal<TEntity>(SPGetAllTaxonomyAssociationHierarchy, startRowIndex, maximumRows, sort);
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

                entity.ParentTaxonomyAssociationId = entityRow.Field<int>(DBCParentTaxonomyAssociationId);
                entity.ChildTaxonomyAssociationId = entityRow.Field<int>(DBCChildTaxonomyAssociationId);
                entity.RelationshipType = entityRow.Field<int>(DBCRelationshipType);
                entity.Order = entityRow.Field<int?>(DBCOrder);

            }

            return entity;
        }


        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        public override void SaveEntity(TaxonomyAssociationHierarchyObjectModel entity)
        {          
            List<DbParameter> parameters = base.GetParametersFromEntity<TaxonomyAssociationHierarchyObjectModel>(entity) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCParentTaxonomyAssociationId, DbType.Int32, entity.ParentTaxonomyAssociationId));
                parameters.Add(DataAccess.CreateParameter(DBCChildTaxonomyAssociationId, DbType.Int32, entity.ChildTaxonomyAssociationId));
                parameters.Add(DataAccess.CreateParameter(DBCRelationshipType, DbType.Int32, entity.RelationshipType));
                parameters.Add(DataAccess.CreateParameter(DBCOrder, DbType.Int32, entity.Order));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPSaveTaxonomyAssociationHierarchy, parameters);
            }
        }

        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        /// <param name="modifiedBy">The modified by.</param>
        public override void SaveEntity(TaxonomyAssociationHierarchyObjectModel entity, int modifiedBy)
        {
            base.SetModifiedBy(entity, modifiedBy);
            this.SaveEntity(entity);            
        }



        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void DeleteEntity(TaxonomyAssociationHierarchyObjectModel entity)
        {
            DeleteEntity(entity, 0);
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        public override void DeleteEntity(TaxonomyAssociationHierarchyObjectModel entity, int deletedBy)
        {
            List<DbParameter> parameters = base.GetDeleteParameters(deletedBy) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCParentTaxonomyAssociationId, DbType.Int32, entity.ParentTaxonomyAssociationId));
                parameters.Add(DataAccess.CreateParameter(DBCChildTaxonomyAssociationId, DbType.Int32, entity.ChildTaxonomyAssociationId));
                parameters.Add(DataAccess.CreateParameter(DBCRelationshipType, DbType.Int32, entity.RelationshipType));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeleteTaxonomyAssociationHierarchy, parameters);
            }
        }


        /// <summary>
        /// Retrieves entity details from the underlying data access and uses them to populate an entity instance.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="key">Primary key identifier of the entity to retrieve.</param>
        /// <returns>A new instance of type <typeparamref name="TModel" /> if found. Null otherwise.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override TEntity GetEntityByKey<TEntity>(TaxonomyAssociationHierarchyKey key)
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
        public override IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, ISortDetail<TEntity> sort, out int totalRecordCount, params TaxonomyAssociationHierarchyKey[] entitiesToIgnore)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        public override void DeleteEntity(TaxonomyAssociationHierarchyKey key)
        {
            DeleteEntity(key, 0);
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        /// <param name="deletedBy">The deleted by.</param>
        public override void DeleteEntity(TaxonomyAssociationHierarchyKey key, int deletedBy)
        {
            List<DbParameter> parameters = base.GetDeleteParameters(deletedBy) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCParentTaxonomyAssociationId, DbType.Int32, key.ParentTaxonomyAssociationId));
                parameters.Add(DataAccess.CreateParameter(DBCChildTaxonomyAssociationId, DbType.Int32, key.ChildTaxonomyAssociationId));
                parameters.Add(DataAccess.CreateParameter(DBCRelationshipType, DbType.Int32, key.RelationshipType));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeleteTaxonomyAssociationHierarchy, parameters);
            }
        }
    }
}
