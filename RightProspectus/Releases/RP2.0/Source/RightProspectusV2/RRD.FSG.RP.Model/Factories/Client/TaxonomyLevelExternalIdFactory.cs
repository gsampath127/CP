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
using RRD.FSG.RP.Model.Entities.VerticalMarket;
using RRD.FSG.RP.Model.Factories.VerticalMarket;
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace RRD.FSG.RP.Model.Factories.Client
{
    /// <summary>
    /// Class TaxonomyLevelExternalIdFactory.
    /// </summary>
    public class TaxonomyLevelExternalIdFactory : AuditedBaseFactory<TaxonomyLevelExternalIdObjectModel, TaxonomyLevelExternalIdKey>
    {
        #region Constants
        /// <summary>
        /// declaration of all Strings/Procedures used as constants
        /// </summary>
        private const string SPGetAllTaxonomyLevelExternalId = "RPV2HostedAdmin_GetAllTaxonomyLevelExternalId";
        /// <summary>
        /// The sp save TaxonomyLevelExternalId
        /// </summary>
        private const string SPSaveTaxonomyLevelExternalId = "RPV2HostedAdmin_SaveTaxonomyLevelExternalId";
        /// <summary>
        /// The sp deletes TaxonomyLevelExternalId
        /// </summary>
        private const string SPDeleteTaxonomyLevelExternalId = "RPV2HostedAdmin_DeleteTaxonomyLevelExternalId";


        /// <summary>
        /// Level
        /// </summary>
        private const string DBCLevel = "Level";
        /// <summary>
        /// TaxonomyId
        /// </summary>
        private const string DBCTaxonomyId = "TaxonomyId";
        /// <summary>
        /// TaxonomyName
        /// </summary>
        private const string DBCTaxonomyName = "TaxonomyName";
        /// <summary>
        /// ExternalId
        /// </summary>
        private const string DBCExternalId = "ExternalId";
        /// <summary>
        /// ExternalId
        /// </summary>
        private const string DBCIsPrimary = "IsPrimary";

        #endregion


        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteAdministrationFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public TaxonomyLevelExternalIdFactory(IDataAccess dataAccess)
            : base(dataAccess) { }
        #endregion




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
            DataTable taxonomyIDs = new DataTable();

            taxonomyIDs.Columns.Add("TaxonomyID", typeof(Int32));
            taxonomyIDs.Columns.Add("Level", typeof(bool));
            taxonomyIDs.Columns.Add("IsNameProvided", typeof(bool));

            List<TEntity> entities = new List<TEntity>();

            DataTable userDatatable = this.DataAccess.ExecuteDataTable(this.ConnectionString, SPGetAllTaxonomyLevelExternalId);

         
            

            foreach (DataRow row in userDatatable.Rows)
            {
                entities.Add(this.CreateEntity<TEntity>(row));
            }

            entities.ForEach(e =>
            {
                if (string.IsNullOrWhiteSpace(e.TaxonomyName))
                {
                    taxonomyIDs.Rows.Add(e.TaxonomyId, e.Level, false);
                }
            }
                        );

            if (taxonomyIDs.Rows.Count > 0)
            {

                TaxonomyFactory taxonomyFactory = new TaxonomyFactory(DataAccess);

                taxonomyFactory.ClientName = this.ClientName;

                IEnumerable<TaxonomyObjectModel> taxonomyObjectModels = taxonomyFactory.GetTaxonomyNameForTaxonomyIDs(taxonomyIDs);

                if (taxonomyObjectModels.Count() > 0)
                {
                    using (var sequenceTaxonomy = taxonomyObjectModels.GetEnumerator())
                    {
                        while (sequenceTaxonomy.MoveNext())
                        {
                            TaxonomyObjectModel taxonomyObjectModel = sequenceTaxonomy.Current;


                            List<TEntity> currentEntity = entities.FindAll(f => f.TaxonomyId == taxonomyObjectModel.TaxonomyId);

                            if (currentEntity != null && currentEntity.Count > 0)
                            {
                                currentEntity.ForEach(p=> p.TaxonomyName = taxonomyObjectModel.TaxonomyName);
                            }
                        }
                    }
                }
            }


            return entities;

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

                entity.TaxonomyId = entityRow.Field<int>(DBCTaxonomyId);
                entity.TaxonomyName = entityRow.Field<string>(DBCTaxonomyName);
                entity.Level = entityRow.Field<int>(DBCLevel);
                entity.ExternalId = entityRow.Field<string>(DBCExternalId);
                entity.IsPrimary = entityRow.Field<bool>(DBCIsPrimary);
            }

            return entity;
        }




        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        public override void SaveEntity(TaxonomyLevelExternalIdObjectModel entity)
        {


            List<DbParameter> parameters = base.GetParametersFromEntity<TaxonomyLevelExternalIdObjectModel>(entity) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCLevel, DbType.Int32, entity.Level));

                parameters.Add(DataAccess.CreateParameter(DBCTaxonomyId, DbType.Int32, entity.TaxonomyId));

                parameters.Add(DataAccess.CreateParameter(DBCExternalId, DbType.String, entity.ExternalId));
                parameters.Add(DataAccess.CreateParameter(DBCIsPrimary,DbType.Boolean,entity.IsPrimary));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPSaveTaxonomyLevelExternalId, parameters);

            }


        }

        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        /// <param name="modifiedBy">The modified by.</param>
        public override void SaveEntity(TaxonomyLevelExternalIdObjectModel entity, int modifiedBy)
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
        public override TEntity GetEntityByKey<TEntity>(TaxonomyLevelExternalIdKey key)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        public override void DeleteEntity(TaxonomyLevelExternalIdKey key)
        {
            DeleteEntity(key, 0);
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        /// <param name="deletedBy">The deleted by.</param>
        public override void DeleteEntity(TaxonomyLevelExternalIdKey key, int deletedBy)
        {
            List<DbParameter> parameters = base.GetDeleteParameters(deletedBy) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCLevel, DbType.Int32, key.Level));

                parameters.Add(DataAccess.CreateParameter(DBCTaxonomyId, DbType.Int32, key.TaxonomyId));

                parameters.Add(DataAccess.CreateParameter(DBCExternalId, DbType.String, key.ExternalId));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeleteTaxonomyLevelExternalId, parameters);
            }
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void DeleteEntity(TaxonomyLevelExternalIdObjectModel entity)
        {
            DeleteEntity(entity, 0);
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        public override void DeleteEntity(TaxonomyLevelExternalIdObjectModel entity, int deletedBy)
        {
            List<DbParameter> parameters = base.GetDeleteParameters(deletedBy) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCLevel, DbType.Int32, entity.Level));

                parameters.Add(DataAccess.CreateParameter(DBCTaxonomyId, DbType.Int32, entity.TaxonomyId));

                parameters.Add(DataAccess.CreateParameter(DBCExternalId, DbType.String, entity.ExternalId));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeleteTaxonomyLevelExternalId, parameters);
            }
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
        public override IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, ISortDetail<TEntity> sort, out int totalRecordCount, params TaxonomyLevelExternalIdKey[] entitiesToIgnore)
        {
            throw new NotImplementedException();
        }
    }
}
