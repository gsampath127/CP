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

/// <summary>
/// The Client namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Factories.Client
{
    /// <summary>
    /// Class FootnoteFactory.
    /// </summary>
    public class FootnoteFactory : AuditedBaseFactory<FootnoteObjectModel, int>
    {
        #region Constants
        /// <summary>
        /// declaration of all Strings/Procedures used as constants
        /// </summary>
        private const string SPGetAllFootnote = "RPV2HostedAdmin_GetAllFootnote";
        /// <summary>
        /// The sp save footnote
        /// </summary>
        private const string SPSaveFootnote = "RPV2HostedAdmin_SaveFootnote";
        /// <summary>
        /// The sp deletes footnote
        /// </summary>
        private const string SPDeleteFootnote = "RPV2HostedAdmin_DeleteFootnote";


        /// <summary>
        /// FootnoteId
        /// </summary>
        private const string DBCFootnoteId = "FootnoteId";
        /// <summary>
        /// TaxonomyAssociationId
        /// </summary>
        private const string DBCTaxonomyAssociationId = "TaxonomyAssociationId";

        /// <summary>
        /// TaxonomyAssociationGroupId
        /// </summary>
        private const string DBCTaxonomyAssociationGroupId = "TaxonomyAssociationGroupId";

        /// <summary>
        /// LanguageCulture
        /// </summary>
        private const string DBCLanguageCulture = "LanguageCulture";

        /// <summary>
        /// Text
        /// </summary>
        private const string DBCText = "Text";

        /// <summary>
        /// Order
        /// </summary>
        private const string DBCOrder = "Order";
       


        #endregion


        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="FootnoteAdministrationFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public FootnoteFactory(IDataAccess dataAccess)
            : base(dataAccess) { }
        #endregion


        /// <summary>
        /// Retrieves entity details from the underlying data access and uses them to populate an entity instance.
        /// </summary>
        /// <param name="key">Primary key identifier of the entity to retrieve.</param>
        /// <returns>A new instance of type <typeparamref name="TModel" /> if found. Null otherwise.</returns>
        public override FootnoteObjectModel GetEntityByKey(int key)
        {
            return GetFootnoteEntity(key);
        }

        /// <summary>
        /// Retrieves a specific taxonomy level entity using the passed in hierarchical level and identifier.
        /// </summary>
        /// <param name="key">The taxonomy key of the entity to retrieve. Consists of Level and TaxonomyId.</param>
        /// <returns>A <see cref="TaxonomyEntity" /> entity.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public FootnoteObjectModel GetFootnoteEntity(int key)
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
            return this.GetEntitiesInternal<TEntity>(SPGetAllFootnote, startRowIndex, maximumRows, null);
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
            return this.GetEntitiesInternal<TEntity>(SPGetAllFootnote, startRowIndex, maximumRows, sort);
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

                entity.FootnoteId = entityRow.Field<int>(DBCFootnoteId);
                entity.Key = entity.FootnoteId;
                entity.TaxonomyAssociationId = entityRow.Field<int?>(DBCTaxonomyAssociationId);
                entity.TaxonomyAssociationGroupId = entityRow.Field<int?>(DBCTaxonomyAssociationGroupId);
                entity.LanguageCulture = entityRow.Field<string>(DBCLanguageCulture);
                entity.Text = entityRow.Field<string>(DBCText);
                entity.Order = entityRow.Field<int?>(DBCOrder);
               
            }

            return entity;
        }




        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        public override void SaveEntity(FootnoteObjectModel entity)
        {
            List<DbParameter> parameters = base.GetParametersFromEntity<FootnoteObjectModel>(entity) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCFootnoteId, DbType.Int32, entity.FootnoteId));
                parameters.Add(DataAccess.CreateParameter(DBCTaxonomyAssociationId, DbType.Int32, entity.TaxonomyAssociationId));
                parameters.Add(DataAccess.CreateParameter(DBCTaxonomyAssociationGroupId, DbType.Int32, entity.TaxonomyAssociationGroupId));
                parameters.Add(DataAccess.CreateParameter(DBCLanguageCulture, DbType.String, entity.LanguageCulture));
                parameters.Add(DataAccess.CreateParameter(DBCText, DbType.String, entity.Text));
                parameters.Add(DataAccess.CreateParameter(DBCOrder, DbType.Int32, entity.Order));


                DataAccess.ExecuteNonQuery(this.ConnectionString, SPSaveFootnote, parameters);
            }
        }

        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        /// <param name="modifiedBy">The modified by.</param>
        public override void SaveEntity(FootnoteObjectModel entity, int modifiedBy)
        {
            base.SetModifiedBy(entity, modifiedBy);
            this.SaveEntity(entity);
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
                parameters.Add(DataAccess.CreateParameter(DBCFootnoteId, DbType.Int32, key));
                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeleteFootnote, parameters);
            }
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void DeleteEntity(FootnoteObjectModel entity)
        {
            DeleteEntity(entity, 0);
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(FootnoteObjectModel entity, int deletedBy)
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
