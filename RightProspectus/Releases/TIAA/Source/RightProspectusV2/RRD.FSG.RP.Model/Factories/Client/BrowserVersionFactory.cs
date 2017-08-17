using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Factories.Client
{
    public class BrowserVersionFactory : AuditedBaseFactory<BrowserVersionObjectModel, int>
    {
        #region Constants
        /// <summary>
        /// DBCBrowserVersionId
        /// </summary>
        private const string DBCBrowserVersionId = "BrowserVersionId";

        private const string DBCId = "Id";

        private const string DBCSelectedId = "SelectedId";

        private const string DBCName = "Name";

        private const string DBCVersion = "Version";

        private const string DBCDownloadUrl = "DownloadURL";

        public const string SPGetAllBrowserVersion ="RPV2HostedAdmin_GetAllBrowserVersion";

        private const string SPSaveBrowserVersion = "RPV2HostedAdmin_SaveBrowserVersion";

        public const string SPDeleteBrowserVersion = "RPV2HostedAdmin_DeleteBrowserVersion";

        #endregion

         #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserVersionFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public BrowserVersionFactory(IDataAccess dataAccess)
            : base(dataAccess) { }
        #endregion

        public override TEntity GetEntityByKey<TEntity>(int key)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<TEntity> GetAllEntities<TEntity>(int startRowIndex, int maximumRows)
        {
            return this.GetEntitiesInternal<TEntity>(SPGetAllBrowserVersion, startRowIndex, maximumRows, null);
        }

        public override IEnumerable<TEntity> GetAllEntities<TEntity>(int startRowIndex, int maximumRows, Interfaces.ISortDetail<TEntity> sort)
        {
            return this.GetEntitiesInternal<TEntity>(SPGetAllBrowserVersion, startRowIndex, maximumRows, null);
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
                entity.Name = entityRow.Field<string>(DBCName);
                entity.MinimumVersion = entityRow.Field<int>(DBCVersion);
                entity.DownloadUrl = entityRow.Field<string>(DBCDownloadUrl);
               
            }

            return entity;
        }


        public override void SaveEntity(BrowserVersionObjectModel entity, int modifiedBy)
        {
            base.SetModifiedBy(entity, modifiedBy);
            this.SaveEntity(entity);
        }

        public override void SaveEntity(BrowserVersionObjectModel entity)
        {

            List<DbParameter> parameters = base.GetParametersFromEntity<BrowserVersionObjectModel>(entity) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCSelectedId, DbType.Int32, entity.SelectedId));

                parameters.Add(DataAccess.CreateParameter(DBCName, DbType.String, entity.Name));

                parameters.Add(DataAccess.CreateParameter(DBCVersion, DbType.Int32, entity.MinimumVersion));

                parameters.Add(DataAccess.CreateParameter(DBCDownloadUrl, DbType.String, entity.DownloadUrl));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPSaveBrowserVersion, parameters);

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
                parameters.Add(DataAccess.CreateParameter(DBCBrowserVersionId, DbType.Int32, key));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeleteBrowserVersion, parameters);
            }
        }

        public override void DeleteEntity(BrowserVersionObjectModel entity)
        {
            throw new NotImplementedException();
        }

        public override void DeleteEntity(BrowserVersionObjectModel entity, int deletedBy)
        {
            List<DbParameter> parameters = base.GetDeleteParameters(deletedBy) as List<DbParameter>;

            if (parameters != null)
            {
                parameters.Add(DataAccess.CreateParameter(DBCBrowserVersionId, DbType.Int32, entity.Id));

                DataAccess.ExecuteNonQuery(this.ConnectionString, SPDeleteBrowserVersion, parameters);
            }
        }
    }
}
