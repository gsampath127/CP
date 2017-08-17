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
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace RRD.FSG.RP.Model.Factories.Client
{
    /// <summary>
    /// Class VerticalXmlImportFactory.
    /// </summary>
    public class VerticalXmlImportFactory : BaseFactory<VerticalXmlImportObjectModel, int>
    {
        #region Constants
        /// <summary>
        /// The sp get all VerticalXmlImport
        /// </summary>
        private const string SPGetAllVerticalXmlImport = "RPV2HostedAdmin_GetAllVerticalXmlImport";
        /// <summary>
        /// The sp get VerticalXmlImport BY ID
        /// </summary>
        private const string SPGetVerticalXmlImportByID = "RPV2HostedAdmin_GetVerticalXmlImportByID";
        /// <summary>
        /// The sp save SaveVerticalXmlImport
        /// </summary>
        private const string SPSaveVerticalXmlImport = "RPV2HostedAdmin_SaveVerticalXmlImport";
        /// <summary>
        /// The sp save SaveVerticalXmlBackupToExportTableAndUpdateStatus
        /// </summary>
        private const string SPSaveVerticalXmlBackupToExportTableAndUpdateStatus = "RPV2HostedAdmin_SaveVerticalXmlBackupToExportTableAndUpdateStatus";
        /// <summary>
        /// SP for DequeueVerticalXmlImport
        /// </summary>
        private const string SPDequeueVerticalXmlImport = "RPV2HostedAdmin_DequeueVerticalXmlImport";
        /// <summary>
        /// VerticalXmlImportId
        /// </summary>
        private const string DBCVerticalXmlImportId = "VerticalXmlImportId";
        /// <summary>
        /// ImportXml
        /// </summary>
        private const string DBCImportXml = "ImportXml";
        /// <summary>
        /// ImportXml
        /// </summary>
        private const string DBCExportXml = "ExportXml";
        /// <summary>
        /// ImportExportXML
        /// </summary>
        private const string DBCImportExportXML = "ImportExportXML";
        /// <summary>
        /// ImportDate
        /// </summary>
        private const string DBCImportDate = "ImportDate";
        /// <summary>
        /// ImportTypes
        /// </summary>
        private const string DBCImportTypes = "ImportTypes";
        /// <summary>
        /// ImportedBy
        /// </summary>
        private const string DBCImportedBy = "ImportedBy";
        /// <summary>
        /// ExportedBy
        /// </summary>
        private const string DBCExportedBy = "ExportedBy";
        /// <summary>
        /// ExportBackupId
        /// </summary>
        private const string DBCExportBackupId = "ExportBackupId";
        /// <summary>
        /// ImportDescription
        /// </summary>
        private const string DBCImportDescription = "ImportDescription";
        /// <summary>
        /// Status
        /// </summary>
        private const string DBCStatus = "Status";

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public VerticalXmlImportFactory(IDataAccess dataAccess)
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

                entity.VerticalXmlImportId = entityRow.Field<int>(DBCVerticalXmlImportId);
                entity.Key = entity.VerticalXmlImportId;
                entity.ImportDate = entityRow.Field<DateTime>(DBCImportDate);
                entity.ImportDescription = entityRow.Field<string>(DBCImportDescription);
                entity.ImportedBy = entityRow.Field<int>(DBCImportedBy);
                entity.ImportTypes = entityRow.Field<int>(DBCImportTypes);
                entity.Status = entityRow.Field<int>(DBCStatus);
                entity.ExportBackupId = entityRow.Field<int?>(DBCExportBackupId);
            }

            return entity;
        }


        /// <summary>
        /// Dequeues the and load import XML.
        /// </summary>
        public void DequeueAndLoadImportXML()
        {
            VerticalXmlImportObjectModel entity = null;
            DataTable verticalXmlImportDatatable = this.DataAccess.ExecuteDataTable(this.ConnectionString, SPDequeueVerticalXmlImport);

            if (verticalXmlImportDatatable.Rows.Count > 0) // We have to create a Export XML
            {

                entity = new VerticalXmlImportObjectModel();



                DataRow entityRow = verticalXmlImportDatatable.Rows[0];

                entity.VerticalXmlImportId = entityRow.Field<int>(DBCVerticalXmlImportId);
                entity.ImportDate = entityRow.Field<DateTime>(DBCImportDate);
                
                entity.ImportXml = entityRow.Field<string>(DBCImportXml);
                entity.ImportedBy = entityRow.Field<int>(DBCImportedBy);
                entity.ImportTypes = entityRow.Field<int>(DBCImportTypes);
                
                entity.Status = entityRow.Field<int>(DBCStatus);

                bool isBackup = false;

                VerticalImportExportGenerationFactory verticalImportExportGenerationFactory = new VerticalImportExportGenerationFactory(this.DataAccess);
                verticalImportExportGenerationFactory.ClientName = this.ClientName;

                try
                {



                    if(entity.Status == 1)
                    {
                        XDocument exportxml = verticalImportExportGenerationFactory.GenerateExportXML();

                        this.DataAccess.ExecuteNonQuery(this.ConnectionString, SPSaveVerticalXmlBackupToExportTableAndUpdateStatus,
                           this.DataAccess.CreateParameter(DBCVerticalXmlImportId, DbType.Int32, entity.VerticalXmlImportId),
                           this.DataAccess.CreateParameter(DBCExportXml, DbType.Xml, exportxml.ToString(SaveOptions.None)),
                           this.DataAccess.CreateParameter(DBCExportedBy, DbType.Int32, entity.ImportedBy)
                           );

                        entity.Status = 3;

                        entity.ImportDate = DateTime.UtcNow;
                        
                    }
                    else
                        if(entity.Status == 5)
                    {
                        isBackup = true;

                        entity.Status = 6;

                        entity.ImportDate = DateTime.UtcNow;
                    }

                    TextReader tr = new StringReader(entityRow.Field<string>(DBCImportExportXML));
                    

                    XDocument xmlToLoad = XDocument.Load(tr);

                  int PassOrFailed =  verticalImportExportGenerationFactory.LoadDataFromImportXML(xmlToLoad, entity.ImportedBy, isBackup, entity.VerticalXmlImportId);

                  if (isBackup && PassOrFailed != 0)
                  {
                      PassOrFailed = verticalImportExportGenerationFactory.LoadDataFromImportXML(xmlToLoad, entity.ImportedBy, false, entity.VerticalXmlImportId);
                  }

                  if (PassOrFailed == 0)
                      entity.Status = -1;
                  else
                      if (PassOrFailed == 2)
                          entity.Status = 7;
                }
                catch(Exception exception) // Catch if there is a Export Error and update the status as Error..
                {
                    verticalImportExportGenerationFactory.LogErrorToDB(true, entity.VerticalXmlImportId,
                                    "Error occurred in Dequeue and LoadXML " + exception.Message,null);
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
            DataTable verticalXmlImportDatatable = this.DataAccess.ExecuteDataTable(this.ConnectionString, SPGetVerticalXmlImportByID,
                           this.DataAccess.CreateParameter(DBCVerticalXmlImportId,DbType.Int32,key));

            if (verticalXmlImportDatatable.Rows.Count > 0)
            {
                entity = new TEntity();

                DataRow entityRow = verticalXmlImportDatatable.Rows[0];

                entity.VerticalXmlImportId = entityRow.Field<int>(DBCVerticalXmlImportId);
                entity.ImportDate = entityRow.Field<DateTime>(DBCImportDate);
                entity.ImportXml = entityRow.Field<string>(DBCImportXml);
                entity.ImportDescription = entityRow.Field<string>(DBCImportDescription);
                entity.ImportedBy = entityRow.Field<int>(DBCImportedBy);
                entity.ImportTypes = entityRow.Field<int>(DBCImportTypes);
                entity.Status = entityRow.Field<int>(DBCStatus);
                entity.ExportBackupId = entityRow.Field<int?>(DBCExportBackupId);
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
            IEnumerable<TEntity> importDetails =  this.GetEntitiesInternal<TEntity>(SPGetAllVerticalXmlImport, startRowIndex, maximumRows, null);

            IEnumerable<UserObjectModel> userDetails = new UserFactory(DataAccess).GetAllEntities<UserObjectModel>();
            importDetails = importDetails.Select(x => 
                                                {
                                                    x.ImportedByName = userDetails.FirstOrDefault(t => t.UserId == x.ImportedBy) != null ? userDetails.FirstOrDefault(t => t.UserId == x.ImportedBy).FirstName + " " + userDetails.FirstOrDefault(t => t.UserId == x.ImportedBy).LastName : string.Empty;
                                                    return x;
                                                });            
            return importDetails;
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
            IEnumerable<TEntity> importDetails = this.GetEntitiesInternal<TEntity>(SPGetAllVerticalXmlImport, startRowIndex, maximumRows, sort);

            IEnumerable<UserObjectModel> userDetails = new UserFactory(DataAccess).GetAllEntities<UserObjectModel>();
            importDetails = importDetails.Select(x =>
            {
                x.ImportedByName = userDetails.FirstOrDefault(t => t.UserId == x.ImportedBy) != null ? userDetails.FirstOrDefault(t => t.UserId == x.ImportedBy).FirstName + " " + userDetails.FirstOrDefault(t => t.UserId == x.ImportedBy).LastName : string.Empty;
                return x;
            });
            return importDetails;
        }


        /// <summary>
        /// Saves any changes to the entity using the underlying data access for the factory.
        /// </summary>
        /// <param name="entity">Entity to be saved.</param>
        public override void SaveEntity(VerticalXmlImportObjectModel entity)
        {
            List<DbParameter> parameters = base.GetParametersFromEntity<VerticalXmlImportObjectModel>(entity) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCVerticalXmlImportId, DbType.Int32, entity.VerticalXmlImportId));
                parameters.Add(DataAccess.CreateParameter(DBCImportTypes, DbType.Int32, entity.ImportTypes));
                parameters.Add(DataAccess.CreateParameter(DBCImportXml, DbType.Xml, entity.ImportXml));
                parameters.Add(DataAccess.CreateParameter(DBCImportedBy, DbType.Int32, entity.ImportedBy));
                parameters.Add(DataAccess.CreateParameter(DBCImportDescription, DbType.String, entity.ImportDescription));
                parameters.Add(DataAccess.CreateParameter(DBCStatus, DbType.Int32, entity.Status));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPSaveVerticalXmlImport, parameters);
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
        public override void SaveEntity(VerticalXmlImportObjectModel entity, int modifiedBy)
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
        public override void DeleteEntity(VerticalXmlImportObjectModel entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity associated with the key from the data store.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="deletedBy">The deleted by.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteEntity(VerticalXmlImportObjectModel entity, int deletedBy)
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
