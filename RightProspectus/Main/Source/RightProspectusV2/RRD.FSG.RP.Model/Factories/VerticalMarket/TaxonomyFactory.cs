// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
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
using System.Linq;

namespace RRD.FSG.RP.Model.Factories.VerticalMarket
{
    /// <summary>
    /// Class TaxonomyFactory.
    /// </summary>
    public class TaxonomyFactory: BaseFactory<TaxonomyObjectModel, TaxonomyKey>
    {
        #region Constants
        /// <summary>
        /// The sp get all Taxonomy
        /// </summary>
        private const string SPGetAllTaxonomy = "RPV2HostedAdmin_GetAllTaxonomy";

        /// <summary>
        /// The sp get TaxonomyNameForTaxonomyIDs
        /// </summary>
        private const string SPGetTaxonomyNameForTaxonomyIDs = "RPV2HostedAdmin_GetTaxonomyNameForTaxonomyIDs";

        /// <summary>
        /// TaxonomyId
        /// </summary>
        private const string DBCTaxonomyId = "TaxonomyId";

        /// <summary>
        /// Level
        /// </summary>
        private const string DBCLevel = "Level";

        /// <summary>
        /// TaxonomyName
        /// </summary>
        private const string DBCTaxonomyName = "TaxonomyName";

        /// <summary>
        /// TaxonomyIds
        /// </summary>
        private const string DBCTaxonomyIds = "TaxonomyIds";

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public TaxonomyFactory(IDataAccess dataAccess)
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

                entity.TaxonomyId = entityRow.Field<int>(DBCTaxonomyId);
                entity.Level = entityRow.Field<int>(DBCLevel);
                entity.TaxonomyName = entityRow.Field<string>(DBCTaxonomyName);
            }

            return entity;
        }



        /// <summary>
        /// Gets the taxonomy name for taxonomy i ds.
        /// </summary>
        /// <param name="taxonomyIDs">The taxonomy i ds.</param>
        /// <returns>IEnumerable&lt;TaxonomyObjectModel&gt;.</returns>
        public IEnumerable<TaxonomyObjectModel> GetTaxonomyNameForTaxonomyIDs(DataTable taxonomyIDs)
        {            


            DataTable taxononyDatatable = this.DataAccess.ExecuteDataTable(this.VerticalMarketConnectionString, 
                                        SPGetTaxonomyNameForTaxonomyIDs,
                                        DataAccess.CreateParameter(DBCTaxonomyIds, SqlDbType.Structured, taxonomyIDs));

            List<TaxonomyObjectModel> taxonomyObjectModels = new List<TaxonomyObjectModel>();
            

            foreach (DataRow row in taxononyDatatable.Rows)
            {
                taxonomyObjectModels.Add(new TaxonomyObjectModel()
                {

                    TaxonomyId = row.Field<int>(DBCTaxonomyId),
                   
                    Level = row.Field<int>(DBCLevel),

                    TaxonomyName = row.Field<string>(DBCTaxonomyName)
                });
            }

            return taxonomyObjectModels;

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
            DataTable taxonomyIDs = new DataTable();

            taxonomyIDs.Columns.Add("TaxonomyID", typeof(Int32));
            taxonomyIDs.Columns.Add("Level", typeof(bool));
            taxonomyIDs.Columns.Add("IsNameProvided", typeof(bool));

            List<TEntity> entities = new List<TEntity>();

            DataTable userDatatable = this.DataAccess.ExecuteDataTable(this.ConnectionString, SPGetAllTaxonomy);



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


                IEnumerable<TaxonomyObjectModel> taxonomyObjectModels = this.GetTaxonomyNameForTaxonomyIDs(taxonomyIDs);

                if (taxonomyObjectModels.Count() > 0)
                {
                    using (var sequenceTaxonomy = taxonomyObjectModels.GetEnumerator())
                    {
                        while (sequenceTaxonomy.MoveNext())
                        {
                            TaxonomyObjectModel taxonomyObjectModel = sequenceTaxonomy.Current;


                            TEntity currentEntity = entities.Find(f => f.TaxonomyId == taxonomyObjectModel.TaxonomyId);

                            if (currentEntity != null)
                            {
                                currentEntity.TaxonomyName = taxonomyObjectModel.TaxonomyName;
                            }
                        }
                    }
                }
            }
            entities.RemoveAll(e => string.IsNullOrWhiteSpace(e.TaxonomyName));
            return entities;
        }

        /// <summary>
        /// Retrieves all entities from the underlying data access and uses them to populate a collection of entities.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <param name="sort">The sort.</param>
        /// <returns>A collection of entities representing all the entities from the data store.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override IEnumerable<TEntity> GetAllEntities<TEntity>(int startRowIndex, int maximumRows, ISortDetail<TEntity> sort)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void SaveEntity(TaxonomyObjectModel entity)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void SaveEntity(TaxonomyObjectModel entity, int modifiedBy)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(TaxonomyObjectModel entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(TaxonomyObjectModel entity, int deletedBy)
        {
            throw new NotImplementedException();
        }






        /// <summary>
        /// Retrieves entity details from the underlying data access and uses them to populat an entity instance.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="key">Primary key identifier of the entity to retrieve.</param>
        /// <returns>A new instance of type <typeparamref name="TModel" /> if found. Null otherwise.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override TEntity GetEntityByKey<TEntity>(TaxonomyKey key)
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
        public override IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, ISearchDetail<TEntity> search, ISortDetail<TEntity> sort,out int totalRecordCount, params TaxonomyKey[] entitiesToIgnore)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(TaxonomyKey key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="key">Identity of the entity to delete.</param>
        /// <param name="modifiedBy">The modified by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(TaxonomyKey key, int modifiedBy)
        {
            throw new NotImplementedException();
        }
    }
}
