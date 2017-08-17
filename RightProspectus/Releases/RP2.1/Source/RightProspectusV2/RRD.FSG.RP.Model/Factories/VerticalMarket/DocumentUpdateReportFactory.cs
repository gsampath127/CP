using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.VerticalMarket;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Factories.VerticalMarket
{
    public class DocumentUpdateReportFactory : BaseFactory<DocumentUpdateReportObjectModel, int>
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentUpdateReportFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public DocumentUpdateReportFactory(IDataAccess dataAccess)
            : base(dataAccess) { }
        #endregion

        #region Constants
        private const string SPGetAllTaxonomyAsssociation = "RPV2HostedAdmin_GetAllTaxonomyAssociation";
        private const string SPGetNuveenData = "RPV2HostedAdmin_DataExchangeReport";

        private const string DBCUpdatedDateTime = "DocumentDate";
        private const string DBCCusip = "CUSIP";
        private const string DBCDocType = "DocType";
        private const string DBCIsDocUpdated = "IsDocUpdated";
        private const string DBCTT_TaxonomyAssociation = "@TT_TaxonomyAssociation";

        #endregion

        public override IEnumerable<TEntity> GetAllEntities<TEntity>(int startRowIndex, int maximumRows, Interfaces.ISortDetail<TEntity> sort)
        {
            DataTable taxonomyIDs = new DataTable();
            taxonomyIDs.Columns.Add("marketId", typeof(string));
            taxonomyIDs.Columns.Add("TaxonomyID", typeof(Int32));
            taxonomyIDs.Columns.Add("IsNameOverrideProvided", typeof(int));


            DataTable userDatatable = this.DataAccess.ExecuteDataTable(this.ConnectionString, SPGetAllTaxonomyAsssociation);
            foreach (DataRow row in userDatatable.Rows)
            {
                //if (row["NameOverride"] != null && row["NameOverride"].ToString() != string.Empty)
                //{
                // bool name = Convert.ToBoolean((string.IsNullOrEmpty(row["NameOverride"].ToString())) ? 0 : 1);
                taxonomyIDs.Rows.Add(row["MarketId"].ToString()
                                   , Convert.ToInt32(row["TaxonomyId"].ToString())
                                   , Convert.ToBoolean((string.IsNullOrEmpty(row["NameOverride"].ToString())) ? 0 : 1)
                                   );
                //}
            }

            List<TEntity> entities = new List<TEntity>();

            try
            {
                DataTable dtNuveenData = this.DataAccess.ExecuteDataTable(
                            this.VerticalMarketConnectionString
                          , SPGetNuveenData
                          , this.DataAccess.CreateParameter(DBCTT_TaxonomyAssociation, SqlDbType.Structured, taxonomyIDs)
                          );


                foreach (DataRow row in dtNuveenData.Rows)
                {
                    entities.Add(this.CreateEntity<TEntity>(row));
                }
            }
            catch (Exception e)
            {

            }

            return entities;
        }

        public DataTable GetReportData()
        {
            DataTable taxonomyIDs = new DataTable();
            taxonomyIDs.Columns.Add("TaxonomyID", typeof(Int32));
            taxonomyIDs.Columns.Add("CUSIP", typeof(bool));

            DataTable userDatatable = this.DataAccess.ExecuteDataTable(this.ConnectionString, SPGetAllTaxonomyAsssociation);
            foreach (DataRow row in userDatatable.Rows)
            {
                if (row["NameOverride"] != null && row["NameOverride"].ToString() != string.Empty)
                {
                    taxonomyIDs.Rows.Add(Convert.ToInt32(row["TaxonomyId"].ToString()), row["MarketId"].ToString());
                }
            }

            DataTable dtNuveenData = this.DataAccess.ExecuteDataTable(this.ConnectionString, SPGetNuveenData
                , this.DataAccess.CreateParameter(DBCTT_TaxonomyAssociation, SqlDbType.Structured,
                                                            taxonomyIDs));

            return taxonomyIDs;
        }

        public override TEntity CreateEntity<TEntity>(DataRow entityRow)
        {
            TEntity entity = base.CreateEntity<TEntity>(entityRow);
            if (entity != null)
            {
                entity.DocumentDate = entityRow.Field<DateTime>(DBCUpdatedDateTime);
                entity.CUSIP = entityRow.Field<string>(DBCCusip);
                entity.DocType = entityRow.Field<string>(DBCDocType);
                entity.IsDocUpdated = entityRow.Field<string>(DBCIsDocUpdated);
            }
            return entity;
        }

        public override IEnumerable<TEntity> GetEntitiesBySearch<TEntity>(int startRowIndex, int maximumRows, Interfaces.ISearchDetail<TEntity> search, Interfaces.ISortDetail<TEntity> sort, out int totalRecordCount, params int[] entitiesToIgnore)
        {
            throw new NotImplementedException();
        }

        public override TEntity GetEntityByKey<TEntity>(int key)
        {
            throw new NotImplementedException();
        }

        public override void SaveEntity(DocumentUpdateReportObjectModel entity, int modifiedBy)
        {
            throw new NotImplementedException();
        }

        public override void DeleteEntity(int key)
        {
            throw new NotImplementedException();
        }

        public override void DeleteEntity(int key, int modifiedBy)
        {
            throw new NotImplementedException();
        }

        public override void DeleteEntity(DocumentUpdateReportObjectModel entity)
        {
            throw new NotImplementedException();
        }

        public override void DeleteEntity(DocumentUpdateReportObjectModel entity, int deletedBy)
        {
            throw new NotImplementedException();
        }
    }
}
