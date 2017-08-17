using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Factories
{
   public class DocumentSubstitutionFactory: AuditedBaseFactory<DocumentSubstitutionObjectModel, int>   
    {
        #region Constants

          
          private const string DBCId = "Id";
          private const string DBCDocumentType = "DocType";
          private const string DBCSubstituteDocumentType = "SubstituteDocType";
          private const string DBCNDocumentType = "NDocType";
          private const string DBCDocumentSubstitutionId = "DocumentSubstitutionId";
          private const string SPGetAllDocumentSubstitution = "RPV2HostedAdmin_GetAllDocumentSubstitution";
          private const string SPSaveDocumentSubstitution = "RPV2HostedAdmin_SaveDocumentSubstitution";
          public const string SPDeleteDocumentSubstitution = "RPV2HostedAdmin_DeleteDocumentSubstitution";

       #endregion

         #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserVersionFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
          public DocumentSubstitutionFactory(IDataAccess dataAccess)
            : base(dataAccess) { }
        #endregion

        public override TEntity GetEntityByKey<TEntity>(int key)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<TEntity> GetAllEntities<TEntity>(int startRowIndex, int maximumRows)
        {
            return this.GetEntitiesInternal<TEntity>(SPGetAllDocumentSubstitution, startRowIndex, maximumRows, null);
        }

        public override IEnumerable<TEntity> GetAllEntities<TEntity>(int startRowIndex, int maximumRows, Interfaces.ISortDetail<TEntity> sort)
        {
            return this.GetEntitiesInternal<TEntity>(SPGetAllDocumentSubstitution, startRowIndex, maximumRows, null);
        }

        public override IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, Interfaces.ISearchDetail<TEntity> search, Interfaces.ISortDetail<TEntity> sort, out int totalRecordCount, params int[] entitiesToIgnore)
        {
            throw new NotImplementedException();
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
                entity.Id = entityRow.Field<int>(DBCId);
                entity.Key = entity.Id;
                entity.DocumentType = entityRow.Field<string>(DBCDocumentType);
                entity.SubstituteDocumentType = entityRow.Field<string>(DBCSubstituteDocumentType);
                entity.NDocumentType = entityRow.Field<string>(DBCNDocumentType);
               
            }

            return entity;
        }
        public override void SaveEntity(DocumentSubstitutionObjectModel entity, int modifiedBy)
        {
            base.SetModifiedBy(entity, modifiedBy);
            this.SaveEntity(entity);
        }

        public override void SaveEntity(DocumentSubstitutionObjectModel entity)
        {

            List<DbParameter> parameters = base.GetParametersFromEntity<DocumentSubstitutionObjectModel>(entity) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCId, DbType.Int32, entity.Id));

                parameters.Add(DataAccess.CreateParameter(DBCDocumentType, DbType.String, entity.DocumentType));

                parameters.Add(DataAccess.CreateParameter(DBCSubstituteDocumentType, DbType.String, entity.SubstituteDocumentType ));

                parameters.Add(DataAccess.CreateParameter(DBCNDocumentType, DbType.String, entity.NDocumentType));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPSaveDocumentSubstitution, parameters);

            }

        }

        public override void DeleteEntity(int key)
        {
            DeleteEntity(key, 0);
        }

        public override void DeleteEntity(int key, int deletedBy)
        {
            List<DbParameter> parameters = base.GetDeleteParameters(deletedBy) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCDocumentSubstitutionId, DbType.Int32, key));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeleteDocumentSubstitution, parameters);
            }
        }

        public override void DeleteEntity(DocumentSubstitutionObjectModel entity)
        {
            throw new NotImplementedException();
        }

        public override void DeleteEntity(DocumentSubstitutionObjectModel entity, int deletedBy)
        {
            List<DbParameter> parameters = base.GetDeleteParameters(deletedBy) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCDocumentSubstitutionId, DbType.Int32, entity.Id));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeleteDocumentSubstitution, parameters);
            }
        }
    }
}
