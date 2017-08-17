// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-16-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Xml.Linq;

namespace RRD.FSG.RP.Model.Factories.Client
{
    /// <summary>
    /// Class VerticalXmlExportFactory.
    /// </summary>
    public class VerticalXmlExportFactory : BaseFactory<VerticalXmlExportObjectModel, int>
    {
        #region Constants
        /// <summary>
        /// The sp get all VerticalXmlExport
        /// </summary>
        private const string SPGetAllVerticalXmlExport = "RPV2HostedAdmin_GetAllVerticalXmlExport";

        /// <summary>
        /// The sp get VerticalXmlExport BY ID
        /// </summary>
        private const string SPGetVerticalXmlExportByID = "RPV2HostedAdmin_GetVerticalXmlExportByID";

        /// <summary>
        /// The sp save SaveVerticalXmlExport
        /// </summary>
        private const string SPSaveVerticalXmlExport = "RPV2HostedAdmin_SaveVerticalXmlExport";

        /// <summary>
        /// The sp dequeue the Export table with status 0
        /// </summary>
        private const string SPDequeueVerticalXmlExport = "RPV2HostedAdmin_DequeueVerticalXmlExport";


        /// <summary>
        /// VerticalXmlExportId
        /// </summary>
        private const string DBCVerticalXmlExportId = "VerticalXmlExportId";

        /// <summary>
        /// ExportXml
        /// </summary>
        private const string DBCExportXml = "ExportXml";

        /// <summary>
        /// ExportDate
        /// </summary>
        private const string DBCExportDate = "ExportDate";

        /// <summary>
        /// ExportTypes
        /// </summary>
        private const string DBCExportTypes = "ExportTypes";

        /// <summary>
        /// ExportedBy
        /// </summary>
        private const string DBCExportedBy = "ExportedBy";

        /// <summary>
        /// ExportDescription
        /// </summary>
        private const string DBCExportDescription = "ExportDescription";

        /// <summary>
        /// Status
        /// </summary>
        private const string DBCStatus = "Status";

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public VerticalXmlExportFactory(IDataAccess dataAccess)
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

                entity.VerticalXmlExportId = entityRow.Field<int>(DBCVerticalXmlExportId);
                entity.Key = entity.VerticalXmlExportId;
                entity.ExportDate = entityRow.Field<DateTime>(DBCExportDate);
                entity.ExportDescription = entityRow.Field<string>(DBCExportDescription);
                entity.ExportedBy = entityRow.Field<int>(DBCExportedBy);
                entity.ExportTypes = entityRow.Field<int>(DBCExportTypes);
                entity.Status = entityRow.Field<int>(DBCStatus);                
            }

            return entity;
        }

        /// <summary>
        /// Dequeues the and save export XML.
        /// </summary>
        public void DequeueAndSaveExportXML()
        {
            VerticalXmlExportObjectModel entity = null;
            DataTable verticalXmlExportDatatable = this.DataAccess.ExecuteDataTable(this.ConnectionString, SPDequeueVerticalXmlExport);

            if (verticalXmlExportDatatable.Rows.Count > 0) // We have to create a Export XML
            {

                entity = new VerticalXmlExportObjectModel();

               

                DataRow entityRow = verticalXmlExportDatatable.Rows[0];

                entity.VerticalXmlExportId = entityRow.Field<int>(DBCVerticalXmlExportId);
                entity.ExportDate = entityRow.Field<DateTime>(DBCExportDate);                
                entity.ExportDescription = entityRow.Field<string>(DBCExportDescription);
                entity.ExportedBy = entityRow.Field<int>(DBCExportedBy);
                entity.ExportTypes = entityRow.Field<int>(DBCExportTypes);
                entity.Status = entityRow.Field<int>(DBCStatus);

                VerticalImportExportGenerationFactory exportgeneration = new VerticalImportExportGenerationFactory(this.DataAccess);

                try
                {

                    

                    exportgeneration.ClientName = this.ClientName;
                    XDocument exportxml = exportgeneration.GenerateExportXML();

                    entity.Status = 2;

                    entity.ExportDate = DateTime.UtcNow;

                    entity.ExportXml = exportxml.ToString(SaveOptions.None);
                }
                catch (Exception exception) // Catch if there is a Export Error and update the status as Error..
                {
                    exportgeneration.LogErrorToDB(true, entity.VerticalXmlExportId,
                                    "Error occurred in Dequeue And Save Export XML " + exception.Message,
                                    null);
                    entity.Status = -1;
                }
                this.SaveEntity(entity);

            }


        }

        /// <summary>
        /// Retrieves entity details from the underlying data access and uses them to populat an entity instance.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="key">Primary key identifier of the entity to retrieve.</param>
        /// <returns>A new instance of type <typeparamref name="TModel" /> if found. Null otherwise.</returns>
        public override TEntity GetEntityByKey<TEntity>(int key)
        {
            TEntity entity = null;
            DataTable verticalXmlExportDatatable = this.DataAccess.ExecuteDataTable(this.ConnectionString, SPGetVerticalXmlExportByID,
                           this.DataAccess.CreateParameter(DBCVerticalXmlExportId,DbType.Int32,key));

            if (verticalXmlExportDatatable.Rows.Count > 0)
            {
                entity = new TEntity();

                DataRow entityRow = verticalXmlExportDatatable.Rows[0];

                entity.VerticalXmlExportId = entityRow.Field<int>(DBCVerticalXmlExportId);
                entity.ExportDate = entityRow.Field<DateTime>(DBCExportDate);
                entity.ExportXml = entityRow.Field<string>(DBCExportXml);
                entity.ExportDescription = entityRow.Field<string>(DBCExportDescription);
                entity.ExportedBy = entityRow.Field<int>(DBCExportedBy);
                entity.ExportTypes = entityRow.Field<int>(DBCExportTypes);
                entity.Status = entityRow.Field<int>(DBCStatus);

            }

            return entity;
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
            IEnumerable<TEntity> exportDetails = this.GetEntitiesInternal<TEntity>(SPGetAllVerticalXmlExport, startRowIndex, maximumRows, null);

            IEnumerable<UserObjectModel> userDetails = new UserFactory(DataAccess).GetAllEntities<UserObjectModel>();
            exportDetails = exportDetails.Select(x =>
            {
                x.ExportedByName = userDetails.FirstOrDefault(t => t.UserId == x.ExportedBy) != null ? userDetails.FirstOrDefault(t => t.UserId == x.ExportedBy).FirstName + " " + userDetails.FirstOrDefault(t => t.UserId == x.ExportedBy).LastName : string.Empty;
                return x;
            });
            return exportDetails;
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
            IEnumerable<TEntity> exportDetails = this.GetEntitiesInternal<TEntity>(SPGetAllVerticalXmlExport, startRowIndex, maximumRows, sort);

            IEnumerable<UserObjectModel> userDetails = new UserFactory(DataAccess).GetAllEntities<UserObjectModel>();
            exportDetails = exportDetails.Select(x =>
            {
                x.ExportedByName = userDetails.FirstOrDefault(t => t.UserId == x.ExportedBy) != null ? userDetails.FirstOrDefault(t => t.UserId == x.ExportedBy).FirstName + " " + userDetails.FirstOrDefault(t => t.UserId == x.ExportedBy).LastName : string.Empty;
                return x;
            });
            return exportDetails;
        }


        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        public override void SaveEntity(VerticalXmlExportObjectModel entity)
        {
            List<DbParameter> parameters = base.GetParametersFromEntity<VerticalXmlExportObjectModel>(entity) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCVerticalXmlExportId, DbType.Int32, entity.VerticalXmlExportId));
                parameters.Add(DataAccess.CreateParameter(DBCExportTypes, DbType.Int32, entity.ExportTypes));
                parameters.Add(DataAccess.CreateParameter(DBCExportXml, DbType.Xml, entity.ExportXml));
                parameters.Add(DataAccess.CreateParameter(DBCExportedBy, DbType.Int32, entity.ExportedBy));
                parameters.Add(DataAccess.CreateParameter(DBCExportDescription, DbType.String, entity.ExportDescription));
                parameters.Add(DataAccess.CreateParameter(DBCStatus, DbType.Int32, entity.Status));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPSaveVerticalXmlExport, parameters);
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
        public override void SaveEntity(VerticalXmlExportObjectModel entity, int modifiedBy)
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
        public override void DeleteEntity(VerticalXmlExportObjectModel entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(VerticalXmlExportObjectModel entity, int deletedBy)
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
