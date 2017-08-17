using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Factories.Client
{
    public class VerticalDataImportFactory : HostedPageBaseFactory, IVerticalDataImportFactory
    {
        #region Constants
        /// <summary>
        /// SPVerticalImport_SaveDocumentTypeAssociation
        /// </summary>
        private const string SPGetTaxonomyAssociationUsingSiteId = "RPV2HostedAdmin_GetTaxonomyAssociationUsingSiteId";
        /// <summary>
        /// SPVerticalImport_SaveDocumentTypeAssociation
        /// </summary>
        private const string SPVerticalImport_SaveDocumentTypeAssociation = "RPV2HostedAdmin_VerticalImport_SaveDocumentTypeAssociation";
        /// <summary>
        /// SPVerticalImport_SaveDocumentTypeAssociation_ExcelImport
        /// </summary>
        private const string SPVerticalImport_SaveDocumentTypeAssociation_ExcelImport = "RPV2HostedAdmin_VerticalImport_SaveDocumentTypeAssociation_ExcelImport";
        /// <summary>
        /// SPVerticalImport_VerifyTaxonomyAssociasion
        /// </summary>
        private const string SPVerticalImport_VerifyTaxonomyAssociation = "RPV2HostedAdmin_VerticalImport_VerifyTaxonomyAssociation";
        ///<summary>
        ///SPVerticalImport_GetGroups
        ///<summary>
        private const string SPVerticalImport_GetTaxonomyAssociationGroupUsingSiteId = "RPV2HostedAdmin_VerticalImport_GetTaxonomyAssociationGroupUsingSiteId";
        ///<summary>
        ///SPVerticalImport_SaveGroupExcellImport
        ///<summary>
        private const string SPVerticalImport_SaveGroupExcellImport = "RPV2HostedAdmin_VerticalImport_SaveTaxonomyAssociationGroup_ExcelImport";
        ///<summary>
        ///SPVerticalImport_GetGroupFunds
        ///<summary>        
        private const string SPVerticalImport_GetGroupFunds = "RPV2HostedAdmin_VerticalImport_GetAllTaxonomyAssociationGroupTaxonomyAssociation";
        /// <summary>
        /// DBCSiteId
        /// </summary>
        private const string DBCSiteId = "SiteId";
        /// <summary>
        /// DBCIsProofing
        /// </summary>
        private const string DBCIsProofing = "IsProofing";
        /// <summary>
        /// DBCAdded
        /// </summary>
        /// 
        private const string DBCAdded = "Added";
        /// <summary>
        /// DBCUpdated
        /// </summary>        
        private const string DBCUpdated = "Updated";
        /// <summary>
        /// DBCDeleted
        /// </summary>
        private const string DBCDeleted = "Deleted";
        /// <summary>
        /// DBCModifiedBy
        /// </summary>
        private const string DBCModifiedBy = "ModifiedBy";

        /// <summary>
        /// SPVerticalImport_SaveDocumentTypeAssociation
        /// </summary>
        private const string SPVerticalImport_SaveTaxonomyAssociation = "RPV2HostedAdmin_VerticalImport_SaveTaxonomyAssociation";
        /// <summary>
        /// SPVerticalImport_SaveDocumentTypeAssociation
        /// </summary>
        private const string SPVerticalImport_SaveTaxonomyAssociation_ExcelImport = "RPV2HostedAdmin_VerticalImport_SaveTaxonomyAssociation_ExcelImport";
        /// <summary>
        /// SPVerticalImport_SaveDocumentTypeAssociationHierarchy
        /// </summary>
        private const string SPVerticalImport_SaveTaxonomyAssociationHierarchy = "RPV2HostedAdmin_VerticalImport_SaveTaxonomyAssociationHierarchy";
        /// <summary>
        /// SPVerticalImport_SaveDocumentTypeAssociationHierarchy
        /// </summary>
        private const string SPVerticalImport_SaveTaxonomyAssociationHierarchy_ExcelImport = "RPV2HostedAdmin_VerticalImport_SaveTaxonomyAssociationHierarchy_ExcelImport";
        /// <summary>
        /// SPVerticalImport_ApproveProofing
        /// </summary>
        private const string SPVerticalImport_ApproveProofing = "RPV2HostedAdmin_VerticalImportGetAllApproveProofing";
        /// <summary>
        /// SPVerticalImport_GetAllTaxonomyAssociationHierachyProduct
        /// </summary>
        private const string SPVerticalImport_GetAllTaxonomyAssociationHierachyProduct = "RPV2HostedAdmin_VerticalImport_GetAllTaxonomyAssociationHierachy";
        /// <summary>
        /// SPVerticalImport_SaveFootnote
        /// </summary>
        private const string SPVerticalImport_SaveFootnote = "RPV2HostedAdmin_VerticalImport_SaveFootnote";
        /// <summary>
        /// SPVerticalImport_SaveFootnote_ExcelImport
        /// </summary>
        private const string SPVerticalImport_SaveTaxonomyGroupFundMapping = "RPV2HostedAdmin_VerticalImport_SaveTaxonomyAssociationGroupFunds";
        /// <summary>
        /// SPVerticalImport_SaveFootnote_ExcelImport
        /// </summary>
        private const string SPVerticalImport_SaveFootnote_ExcelImport = "RPV2HostedAdmin_VerticalImport_SaveFootnote_ExcelImport";
        ///<summary>
        ///SPVerticalImport_SaveTaxonomyAssociationGroupFunds_ExcelImport
        /// </summary>
        private const string SPVerticalImport_SaveTaxonomyAssociationGroupFunds_ExcelImport = "RPV2HostedAdmin_VerticalImport_SaveTaxonomyAssociationGroupFunds_ExcelImport";

        /// <summary>
        /// The DBC TaxonomyIdMarketId Report data type
        /// </summary>
        private const string DBCClientMarketIds = "ClientMarketIds";
        /// <summary>
        /// The sp CustomizeFundOrder
        /// </summary>
        private const string SPCustomizeFundOrder = "RPV2HostedAdmin_SiteFeature_CustomizeTaxonomyOrder";
        /// <summary>
        /// SPVerticalImport_SaveGroup
        /// </summary>
        private const string SPVerticalImport_SaveTaxonomyAssociationGroup = "RPV2HostedAdmin_VerticalImport_SaveTaxonomyAssociationGroup";

        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="VerticalDataImportFactory" /> class.
        /// </summary>
        /// <param name="paramDataAccess">The parameter data access.</param>
        public VerticalDataImportFactory(IDataAccess paramDataAccess)
            : base(paramDataAccess) { }

        public string ClientName { get; set; }
        public int UserId { get; set; }

        public string SaveDocumentTypeAssociation(List<DocumentTypeAssociationObjectModel> added,
                     List<DocumentTypeAssociationObjectModel> updated, List<DocumentTypeAssociationObjectModel> deleted)
        {
            string status = string.Empty;

            var results = this.DataAccess.ExecuteScalar(DBConnectionString.HostedConnectionString(ClientName, DataAccess)
                , SPVerticalImport_SaveDocumentTypeAssociation,
                DataAccess.CreateParameter(DBCAdded, SqlDbType.Structured, GetDocumentTypeAssociationDataTable(added)),
                DataAccess.CreateParameter(DBCUpdated, SqlDbType.Structured, GetDocumentTypeAssociationDataTable(updated)),
                DataAccess.CreateParameter(DBCDeleted, SqlDbType.Structured, GetDocumentTypeAssociationDataTable(deleted))
                );

            if (results != null)
            {
                status = results.ToString();
            }
            return status;
        }

        public string SaveDocumentTypeAssociationExcelImport(int? siteId, List<DocumentTypeAssociationObjectModel> importData)
        {
            string status = string.Empty;

            //var results = this.DataAccess.ExecuteScalar(DBConnectionString.HostedConnectionString(ClientName, DataAccess),
            //    SPVerticalImport_SaveDocumentTypeAssociation_ExcelImport,
            //    DataAccess.CreateParameter(DBCSiteId, SqlDbType.Int, siteId),
            //    DataAccess.CreateParameter(DBCAdded, SqlDbType.Structured, GetDocumentTypeAssociationDataTable(importData))
            //    );

            //if (results != null)
            //{
            //    status = results.ToString();
            //}

            //Commented above code and added below as part of load testing
            using (SqlConnection sqlConn = new SqlConnection(DBConnectionString.HostedConnectionString(ClientName, DataAccess)))
            {
                sqlConn.Open();
                using (SqlCommand command = new SqlCommand(SPVerticalImport_SaveDocumentTypeAssociation_ExcelImport, sqlConn))
                {
                    command.CommandTimeout = ConfigValues.VerticalDataImport_ExcelImport_CommandTimeout;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SiteId", siteId == null ? (object)DBNull.Value : siteId));
                    command.Parameters.Add(new SqlParameter("@Added", GetDocumentTypeAssociationDataTable(importData)));

                    var results = command.ExecuteScalar();
                    if (results != null)
                    {
                        status = results.ToString();
                    }
                }
                sqlConn.Close();
            }

            return status;
        }


        public string SaveFootNotes(List<FootnoteObjectModel> added,
                    List<FootnoteObjectModel> updated, List<FootnoteObjectModel> deleted)
        {
            string status = string.Empty;

            var results = this.DataAccess.ExecuteScalar(DBConnectionString.HostedConnectionString(ClientName, DataAccess)
                , SPVerticalImport_SaveFootnote,
                DataAccess.CreateParameter(DBCAdded, SqlDbType.Structured, GetFootnoteDataTable(added)),
                DataAccess.CreateParameter(DBCUpdated, SqlDbType.Structured, GetFootnoteDataTable(updated)),
                DataAccess.CreateParameter(DBCDeleted, SqlDbType.Structured, GetFootnoteDataTable(deleted))
                );

            if (results != null)
            {
                status = results.ToString();
            }
            return status;
        }
        public string SaveFootnoteExcelImport(List<FootnoteObjectModel> importData)
        {
            string status = string.Empty;

            //var results = this.DataAccess.ExecuteScalar(DBConnectionString.HostedConnectionString(ClientName, DataAccess),
            //    SPVerticalImport_SaveFootnote_ExcelImport,
            //    DataAccess.CreateParameter(DBCAdded, SqlDbType.Structured, GetFootnoteDataTable(importData))
            //    );

            //if (results != null)
            //{
            //    status = results.ToString();
            //}

            //Commented above code and added below as part of load testing
            using (SqlConnection sqlConn = new SqlConnection(DBConnectionString.HostedConnectionString(ClientName, DataAccess)))
            {
                sqlConn.Open();
                using (SqlCommand command = new SqlCommand(SPVerticalImport_SaveFootnote_ExcelImport, sqlConn))
                {
                    command.CommandTimeout = ConfigValues.VerticalDataImport_ExcelImport_CommandTimeout;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Added", GetFootnoteDataTable(importData)));

                    var results = command.ExecuteScalar();
                    if (results != null)
                    {
                        status = results.ToString();
                    }
                }
                sqlConn.Close();
            }
            return status;

                       
        }



        private DataTable GetDocumentTypeAssociationDataTable(List<DocumentTypeAssociationObjectModel> documentTypeAssociationData)
        {
            DataTable documentTypeAssociationDataTable = new DataTable();
            documentTypeAssociationDataTable.Columns.Add("DocumentTypeAssociationId", typeof(int));
            documentTypeAssociationDataTable.Columns.Add("DocumentTypeId", typeof(int));
            documentTypeAssociationDataTable.Columns.Add("SiteId", typeof(int));
            documentTypeAssociationDataTable.Columns.Add("TaxonomyAssociationId", typeof(int));
            documentTypeAssociationDataTable.Columns.Add("Order", typeof(int));
            documentTypeAssociationDataTable.Columns.Add("HeaderText", typeof(string));
            documentTypeAssociationDataTable.Columns.Add("LinkText", typeof(string));
            documentTypeAssociationDataTable.Columns.Add("DescriptionOverride", typeof(string));
            documentTypeAssociationDataTable.Columns.Add("CssClass", typeof(string));
            documentTypeAssociationDataTable.Columns.Add("MarketId", typeof(string));
            documentTypeAssociationDataTable.Columns.Add("UtcModifiedDate", typeof(DateTime));
            documentTypeAssociationDataTable.Columns.Add("ModifiedBy", typeof(int));

            if (documentTypeAssociationData != null && documentTypeAssociationData.Count > 0)
            {
                documentTypeAssociationData.ForEach(p =>
                {
                    documentTypeAssociationDataTable.Rows.Add(
                        p.DocumentTypeAssociationId,
                        p.DocumentTypeId,
                        p.SiteId,
                        p.TaxonomyAssociationId,
                        p.Order,
                        p.HeaderText,
                        p.LinkText,
                        p.DescriptionOverride,
                        p.CssClass,
                        p.MarketId,
                        p.LastModified,
                        this.UserId
                    );
                });
            }

            return documentTypeAssociationDataTable;
        }

        private DataTable GetTaxonomyAssociationDataTable(List<TaxonomyAssociationObjectModel> taxonomyAssociationData)
        {
            DataTable taxonomyAssociationDataTable = new DataTable();
            taxonomyAssociationDataTable.Columns.Add("TaxonomyAssociationId", typeof(int));
            taxonomyAssociationDataTable.Columns.Add("Level", typeof(int));
            taxonomyAssociationDataTable.Columns.Add("TaxonomyId", typeof(int));
            taxonomyAssociationDataTable.Columns.Add("SiteId", typeof(int));
            taxonomyAssociationDataTable.Columns.Add("ParentTaxonomyAssociationId", typeof(int));
            taxonomyAssociationDataTable.Columns.Add("NameOverride", typeof(string));
            taxonomyAssociationDataTable.Columns.Add("TabbedPageNameOverride", typeof(string));
            taxonomyAssociationDataTable.Columns.Add("DescriptionOverride", typeof(string));
            taxonomyAssociationDataTable.Columns.Add("CssClass", typeof(string));
            taxonomyAssociationDataTable.Columns.Add("MarketId", typeof(string));
            taxonomyAssociationDataTable.Columns.Add("UtcModifiedDate", typeof(DateTime));
            taxonomyAssociationDataTable.Columns.Add("ModifiedBy", typeof(int));
            taxonomyAssociationDataTable.Columns.Add("Order", typeof(int));

            if (taxonomyAssociationData != null && taxonomyAssociationData.Count > 0)
            {
                taxonomyAssociationData.ForEach(p =>
                {
                    taxonomyAssociationDataTable.Rows.Add(
                        p.TaxonomyAssociationId,
                        p.Level,
                        p.TaxonomyId,
                        p.SiteId,
                        p.ParentTaxonomyAssociationId,
                        p.NameOverride,
                        p.TabbedPageNameOverride,
                        p.DescriptionOverride,
                        p.CssClass,
                        p.MarketId,
                        p.LastModified,
                        this.UserId,
                        p.Order
                    );
                });
            }

            return taxonomyAssociationDataTable;
        }

        private DataTable GetaxonomyAssociationHierarchyDataTable(List<TaxonomyAssociationProductObjectModel> taxonomyAssociationHierarchyData)
        {
            DataTable taxonomyAssociationHierarchyDataTable = new DataTable();
            taxonomyAssociationHierarchyDataTable.Columns.Add("ParentTaxonomyAssociationId", typeof(int));
            taxonomyAssociationHierarchyDataTable.Columns.Add("ChildTaxonomyAssociationId", typeof(int));
            taxonomyAssociationHierarchyDataTable.Columns.Add("RelationshipType", typeof(int));
            taxonomyAssociationHierarchyDataTable.Columns.Add("Order", typeof(int));
            taxonomyAssociationHierarchyDataTable.Columns.Add("UtcModifiedDate", typeof(DateTime));
            taxonomyAssociationHierarchyDataTable.Columns.Add("ModifiedBy", typeof(int));

            if (taxonomyAssociationHierarchyData != null && taxonomyAssociationHierarchyData.Count > 0)
            {
                taxonomyAssociationHierarchyData.ForEach(p =>
                {
                    taxonomyAssociationHierarchyDataTable.Rows.Add(
                        p.ParentTaxonomyAssociationId,
                        p.ChildTaxonomyAssociationId,
                        1,
                        p.Order,
                        p.UtcModifiedDate,
                        this.UserId
                    );
                });
            }

            return taxonomyAssociationHierarchyDataTable;
        }

        private DataTable GetFootnoteDataTable(List<FootnoteObjectModel> footNoteData)
        {
            DataTable footNoteDataTable = new DataTable();
            footNoteDataTable.Columns.Add("FootnoteId", typeof(int));
            footNoteDataTable.Columns.Add("TaxonomyAssociationId", typeof(int));
            footNoteDataTable.Columns.Add("TaxonomyAssociationGroupId", typeof(int));
            footNoteDataTable.Columns.Add("LanguageCulture", typeof(string));
            footNoteDataTable.Columns.Add("Text", typeof(string));
            footNoteDataTable.Columns.Add("Order", typeof(int));
            footNoteDataTable.Columns.Add("UtcModifiedDate", typeof(DateTime));
            footNoteDataTable.Columns.Add("ModifiedBy", typeof(int));

            if (footNoteData != null && footNoteData.Count > 0)
            {
                footNoteData.ForEach(p =>
                {
                    footNoteDataTable.Rows.Add(
                        p.FootnoteId,
                        p.TaxonomyAssociationId,
                        p.TaxonomyAssociationGroupId,
                        p.LanguageCulture,
                        p.Text,
                        p.Order,
                        p.LastModified,
                        this.UserId
                    );
                });
            }

            return footNoteDataTable;
        }

        public string SaveTaxonomyAssociation(List<TaxonomyAssociationObjectModel> added,
                     List<TaxonomyAssociationObjectModel> updated, List<TaxonomyAssociationObjectModel> deleted)
        {
            added = VerifyTaxonomywithVerticalData(added);
            string status = null;

            var results = this.DataAccess.ExecuteScalar(
                DBConnectionString.HostedConnectionString(ClientName, DataAccess),
                SPVerticalImport_SaveTaxonomyAssociation,
                DataAccess.CreateParameter(DBCAdded, SqlDbType.Structured, GetTaxonomyAssociationDataTable(added)),
                DataAccess.CreateParameter(DBCUpdated, SqlDbType.Structured, GetTaxonomyAssociationDataTable(updated)),
                DataAccess.CreateParameter(DBCDeleted, SqlDbType.Structured, GetTaxonomyAssociationDataTable(deleted))
            );

            if (results != null)
            {
                status = results.ToString();
            }
            return status;
        }

        public string ApproveProofing(int siteId)
        {
            string status = null;
            //var results = this.DataAccess.ExecuteScalar(
            //                    DBConnectionString.HostedConnectionString(ClientName, DataAccess),
            //                    SPVerticalImport_ApproveProofing,
            //                    DataAccess.CreateParameter(DBCSiteId, SqlDbType.Int, siteId)
            //               );
            //if (results != null)
            //{
            //    status = results.ToString();
            //}
                    
            using (SqlConnection sqlConn = new SqlConnection(DBConnectionString.HostedConnectionString(ClientName, DataAccess)))
            {
                sqlConn.Open();
                using (SqlCommand command = new SqlCommand(SPVerticalImport_ApproveProofing, sqlConn))
                {
                    command.CommandTimeout = ConfigValues.VerticalDataImport_ApproveProofing_CommandTimeout;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SiteId", siteId == null ? (object)DBNull.Value : siteId));

                    var results = command.ExecuteScalar();
                    if (results != null)
                    {
                        status = results.ToString();
                    }
                }
                sqlConn.Close();
            }

            return status;
        }

        public List<TaxonomyAssociationObjectModel> GetTaxonomyAssociationUsingSiteId(string clientName, int siteId, bool isProofing)
        {
            List<TaxonomyAssociationObjectModel> data = new List<TaxonomyAssociationObjectModel>();

            DataTable TaxonomyAssociationDataTable = DataAccess.ExecuteDataTable(DBConnectionString.HostedConnectionString(clientName, DataAccess),
                                                   SPGetTaxonomyAssociationUsingSiteId,
                                                   this.DataAccess.CreateParameter(DBCSiteId, SqlDbType.Int, siteId),
                                                   this.DataAccess.CreateParameter(DBCIsProofing, SqlDbType.Bit, isProofing)
                                                   );

            foreach (DataRow entityRow in TaxonomyAssociationDataTable.Rows)
            {
                TaxonomyAssociationObjectModel entity = new TaxonomyAssociationObjectModel();
                entity.TaxonomyAssociationId = entityRow.Field<int>("TaxonomyAssociationId");
                entity.Level = entityRow.Field<int>("Level");
                entity.TaxonomyId = entityRow.Field<int>("TaxonomyId");
                entity.SiteId = entityRow.Field<int?>("SiteId");
                entity.ParentTaxonomyAssociationId = entityRow.Field<int?>("ParentTaxonomyAssociationId");
                entity.NameOverride = entityRow.Field<string>("NameOverride");
                entity.DescriptionOverride = entityRow.Field<string>("DescriptionOverride");
                entity.CssClass = entityRow.Field<string>("CssClass");
                entity.MarketId = entityRow.Field<string>("MarketId");
                entity.IsProofing = entityRow.Field<bool>("IsProofing");
                entity.Order = entityRow.Field<int?>("Order");

                data.Add(entity);
            }

            return data;
        }

        public List<TaxonomyAssociationProductObjectModel> GetTaxonomyAssociationProduct(int siteId)
        {

            DataTable Data = this.DataAccess.ExecuteDataTable(DBConnectionString.HostedConnectionString(ClientName, DataAccess),
            SPVerticalImport_GetAllTaxonomyAssociationHierachyProduct,
            DataAccess.CreateParameter(DBCSiteId, SqlDbType.Int, siteId)
            );
            List<TaxonomyAssociationProductObjectModel> lstEntities = new List<TaxonomyAssociationProductObjectModel>();


            foreach (DataRow row in Data.Rows)
            {
                TaxonomyAssociationProductObjectModel entity = new TaxonomyAssociationProductObjectModel();
                entity.ParentTaxonomyAssociationId = row.Field<int>("ParentTaxonomyAssociationId");
                entity.ChildTaxonomyAssociationId = row.Field<int?>("ChildTaxonomyAssociationId");
                entity.ChildTaxonomyId = row.Field<int?>("ChildTaxonomyId");
                entity.ChildMarketId = row.Field<string>("ChildMarketId");
                entity.ChildNameOverride = row.Field<string>("ChildNameOverride");
                entity.ParentTaxonmyId = row.Field<int?>("ParentTaxonomyId");
                entity.ParentMarketId = row.Field<string>("ParentMarketId");
                entity.ParentNameOverride = row.Field<string>("ParentNameOverride");
                entity.RelationshipType = row.Field<int?>("RelationshipType");
                entity.Order = row.Field<int?>("Order");
                entity.UtcModifiedDate = row.Field<DateTime?>("UtcModifiedDate");
                entity.ModifiedBy = row.Field<int?>("ModifiedBy");
                lstEntities.Add(entity);
            }

            return lstEntities;
        }

        public List<TaxonomyAssociationObjectModel> SaveTaxonomyAssocaitionExcelImport(int? siteId, List<TaxonomyAssociationObjectModel> importData, out string status)
        {
            status = string.Empty;
            DataTable clientMarketIds = new DataTable();
            clientMarketIds.Columns.Add("MarketId", typeof(string));
            clientMarketIds.Columns.Add("Level", typeof(int));

            importData.ForEach(p =>
            {
                clientMarketIds.Rows.Add(p.MarketId, p.Level);
            });

            DataTable validData = this.DataAccess.ExecuteDataTable(
                    DBConnectionString.VerticalDBConnectionString(ClientName, DataAccess),
                    SPVerticalImport_VerifyTaxonomyAssociation,
                    DataAccess.CreateParameter(DBCClientMarketIds, SqlDbType.Structured, clientMarketIds)
                );

            foreach (DataRow entityRow in validData.Rows)
            {
                List<TaxonomyAssociationObjectModel> lstTAData = importData.FindAll(p => p.MarketId == entityRow.Field<string>("MarketId"));
                foreach (TaxonomyAssociationObjectModel taData in lstTAData)
                {
                    taData.IsObjectinVerticalMarket = true;
                    taData.TaxonomyId = entityRow.Field<int>("TaxonomyId");
                }
            }

            //var results = this.DataAccess.ExecuteScalar(
            //    DBConnectionString.HostedConnectionString(ClientName, DataAccess),
            //    SPVerticalImport_SaveTaxonomyAssociation_ExcelImport,
            //    DataAccess.CreateParameter(DBCSiteId, SqlDbType.Int, siteId),
            //    DataAccess.CreateParameter(DBCAdded, SqlDbType.Structured, GetTaxonomyAssociationDataTable(importData.FindAll(a => a.IsObjectinVerticalMarket)))
            //);

            //if (results != null)
            //{
            //    status = results.ToString();
            //}

            //Commented above code and added below as part of load testing
            using (SqlConnection sqlConn = new SqlConnection(DBConnectionString.HostedConnectionString(ClientName, DataAccess)))
            {
                sqlConn.Open();
                using (SqlCommand command = new SqlCommand(SPVerticalImport_SaveTaxonomyAssociation_ExcelImport, sqlConn))
                {
                    command.CommandTimeout = ConfigValues.VerticalDataImport_ExcelImport_CommandTimeout;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SiteId", siteId == null ? (object)DBNull.Value : siteId));
                    command.Parameters.Add(new SqlParameter("@Added", GetTaxonomyAssociationDataTable(importData.FindAll(a => a.IsObjectinVerticalMarket))));

                    var results = command.ExecuteScalar();
                    if (results != null)
                    {
                        status = results.ToString();
                    }
                }
                sqlConn.Close();
            }

            List<TaxonomyAssociationObjectModel> invalidData = importData.FindAll(a => !a.IsObjectinVerticalMarket);

            return invalidData;

        }
        public string SaveTaxonomyHierarchy(List<TaxonomyAssociationProductObjectModel> added,
                    List<TaxonomyAssociationProductObjectModel> updated, List<TaxonomyAssociationProductObjectModel> deleted)
        {
            string status = null;

            var results = this.DataAccess.ExecuteScalar(DBConnectionString.HostedConnectionString(ClientName, DataAccess)
                , SPVerticalImport_SaveTaxonomyAssociationHierarchy,
                DataAccess.CreateParameter(DBCAdded, SqlDbType.Structured, GetaxonomyAssociationHierarchyDataTable(added)),
                DataAccess.CreateParameter(DBCUpdated, SqlDbType.Structured, GetaxonomyAssociationHierarchyDataTable(updated)),
                DataAccess.CreateParameter(DBCDeleted, SqlDbType.Structured, GetaxonomyAssociationHierarchyDataTable(deleted))
                );
            if (results != null)
            {
                status = results.ToString();
            }
            return status;
        }

        public string SaveTaxonomyHierarchyExcelImport(List<TaxonomyAssociationProductObjectModel> importData)
        {

            string status = null;

            //var results = this.DataAccess.ExecuteScalar(
            //    DBConnectionString.HostedConnectionString(ClientName, DataAccess),
            //    SPVerticalImport_SaveTaxonomyAssociationHierarchy_ExcelImport,
            //    DataAccess.CreateParameter(DBCAdded, SqlDbType.Structured, GetaxonomyAssociationHierarchyDataTable(importData))
            //);

            //if (results != null)
            //{
            //    status = results.ToString();
            //}

            //Commented above code and added below as part of load testing
            using (SqlConnection sqlConn = new SqlConnection(DBConnectionString.HostedConnectionString(ClientName, DataAccess)))
            {
                sqlConn.Open();
                using (SqlCommand command = new SqlCommand(SPVerticalImport_SaveTaxonomyAssociationHierarchy_ExcelImport, sqlConn))
                {
                    command.CommandTimeout = ConfigValues.VerticalDataImport_ExcelImport_CommandTimeout;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Added", GetaxonomyAssociationHierarchyDataTable(importData)));

                    var results = command.ExecuteScalar();
                    if (results != null)
                    {
                        status = results.ToString();
                    }
                }
                sqlConn.Close();
            }


            return status;
        }

        public void CustomizeFundOrder(int siteid)
        {
            this.DataAccess.ExecuteNonQuery(
               DBConnectionString.HostedConnectionString(ClientName, DataAccess),
               SPCustomizeFundOrder,
               DataAccess.CreateParameter(DBCSiteId, SqlDbType.Int, siteid),
               DataAccess.CreateParameter(DBCModifiedBy, SqlDbType.Int, this.UserId)
           );
        }
        public List<TaxonomyGroupObjectModel> GetTaxonomyAssociationGroups(int? siteId)
        {


            DataTable Data = this.DataAccess.ExecuteDataTable(DBConnectionString.HostedConnectionString(ClientName, DataAccess),
               SPVerticalImport_GetTaxonomyAssociationGroupUsingSiteId,
               DataAccess.CreateParameter(DBCSiteId, SqlDbType.Int, siteId)
               );
            List<TaxonomyGroupObjectModel> lstEntities = new List<TaxonomyGroupObjectModel>();


            foreach (DataRow row in Data.Rows)
            {
                TaxonomyGroupObjectModel entity = new TaxonomyGroupObjectModel();
                entity.Name = row.Field<string>("Name");
                entity.Description = row.Field<string>("Description");
                entity.CssClass = row.Field<string>("CssClass");
                entity.SiteId = row.Field<int?>("SiteId");
                entity.TaxonomyAssociationGroupId = row.Field<int?>("TaxonomyAssociationGroupId");
                entity.ParentTaxonomyAssociationGroupId = row.Field<int?>("ParentTaxonomyAssociationGroupId");
                entity.ParentTaxonomyAssociationId = row.Field<int?>("ParentTaxonomyAssociationId");
                entity.UtcModifiedDate = row.Field<DateTime?>("UtcModifiedDate");
                entity.ModifiedBy = row.Field<int?>("ModifiedBy");
                entity.Level = row.Field<int>("Level");
                entity.Order = row.Field<int?>("Order");

                lstEntities.Add(entity);
            }

            return lstEntities;


        }

        public string SaveTaxonomyAssociationGroup(List<TaxonomyGroupObjectModel> added,
                   List<TaxonomyGroupObjectModel> updated, List<TaxonomyGroupObjectModel> deleted)
        {
            string status = string.Empty;

            var results = this.DataAccess.ExecuteScalar(DBConnectionString.HostedConnectionString(ClientName, DataAccess)
                , SPVerticalImport_SaveTaxonomyAssociationGroup,
                DataAccess.CreateParameter(DBCAdded, SqlDbType.Structured, GetTaxonomyAssociationGrouoDataTable(added)),
                DataAccess.CreateParameter(DBCUpdated, SqlDbType.Structured, GetTaxonomyAssociationGrouoDataTable(updated)),
                DataAccess.CreateParameter(DBCDeleted, SqlDbType.Structured, GetTaxonomyAssociationGrouoDataTable(deleted))
                );

            if (results != null)
            {
                status = results.ToString();
            }
            return status;




        }
        public string SaveTaxonomyGroupExcelImport(List<TaxonomyGroupObjectModel> importData)
        {

            string status = null;

            //var results = this.DataAccess.ExecuteScalar(
            //    DBConnectionString.HostedConnectionString(ClientName, DataAccess),
            //    SPVerticalImport_SaveGroupExcellImport,
            //    DataAccess.CreateParameter(DBCAdded, SqlDbType.Structured, GetTaxonomyAssociationGrouoDataTable(importData))
            //);

            //if (results != null)
            //{
            //    status = results.ToString();
            //}

            //Commented above code and added below as part of load testing
            using (SqlConnection sqlConn = new SqlConnection(DBConnectionString.HostedConnectionString(ClientName, DataAccess)))
            {
                sqlConn.Open();
                using (SqlCommand command = new SqlCommand(SPVerticalImport_SaveGroupExcellImport, sqlConn))
                {
                    command.CommandTimeout = ConfigValues.VerticalDataImport_ExcelImport_CommandTimeout;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Added", GetTaxonomyAssociationGrouoDataTable(importData)));

                    var results = command.ExecuteScalar();
                    if (results != null)
                    {
                        status = results.ToString();
                    }
                }
                sqlConn.Close();
            }

            return status;
        }

        private DataTable GetTaxonomyAssociationGrouoDataTable(List<TaxonomyGroupObjectModel> taxonomyGroupData)
        {
            DataTable taxonomyAssociationGroupDataTable = new DataTable();
            taxonomyAssociationGroupDataTable.Columns.Add("Name", typeof(string));
            taxonomyAssociationGroupDataTable.Columns.Add("Description", typeof(string));
            taxonomyAssociationGroupDataTable.Columns.Add("CssClass", typeof(string));
            taxonomyAssociationGroupDataTable.Columns.Add("TaxonomyAssociationGroupId", typeof(int));
            taxonomyAssociationGroupDataTable.Columns.Add("SiteId", typeof(int));
            taxonomyAssociationGroupDataTable.Columns.Add("ParentTaxonomyAssociationId", typeof(int));
            taxonomyAssociationGroupDataTable.Columns.Add("ParentTaxonomyAssociationGroupId", typeof(int));
            taxonomyAssociationGroupDataTable.Columns.Add("UtcModifiedDate", typeof(DateTime));
            taxonomyAssociationGroupDataTable.Columns.Add("ModifiedBy", typeof(int));
            taxonomyAssociationGroupDataTable.Columns.Add("Order", typeof(int));

            if (taxonomyGroupData != null && taxonomyGroupData.Count > 0)
            {
                taxonomyGroupData.ForEach(p =>
                {
                    taxonomyAssociationGroupDataTable.Rows.Add(
                        p.Name,
                        p.Description,
                        p.CssClass,
                        p.TaxonomyAssociationGroupId,
                        p.SiteId,
                        p.ParentTaxonomyAssociationId,
                        p.ParentTaxonomyAssociationGroupId,
                        p.UtcModifiedDate,
                        p.ModifiedBy,
                        p.Order
                    );
                });
            }

            return taxonomyAssociationGroupDataTable;
        }
        public List<TaxonomyAssociationGroupTaxonomyAssociationObjectModel> GetTaxonomyAssociationGroupFunds(int? siteId)
        {

            DataTable Data = this.DataAccess.ExecuteDataTable(DBConnectionString.HostedConnectionString(ClientName, DataAccess),
            SPVerticalImport_GetGroupFunds,
            DataAccess.CreateParameter(DBCSiteId, SqlDbType.Int, siteId)
            );
            List<TaxonomyAssociationGroupTaxonomyAssociationObjectModel> lstEntities = new List<TaxonomyAssociationGroupTaxonomyAssociationObjectModel>();


            foreach (DataRow row in Data.Rows)
            {
                TaxonomyAssociationGroupTaxonomyAssociationObjectModel entity = new TaxonomyAssociationGroupTaxonomyAssociationObjectModel();
                entity.TaxonomyAssociationGroupId = row.Field<int>("TaxonomyAssociationGroupId");
                entity.TaxonomyAssociationId = row.Field<int>("TaxonomyAssociationId");
                entity.MarketId = row.Field<string>("MarketId");
                entity.Order = row.Field<int?>("Order");
                entity.NameOverride = row.Field<string>("NameOverride");
                entity.Name = row.Field<string>("Name");
                lstEntities.Add(entity);
            }
            return lstEntities;
        }

        public string SaveTaxonomyGroupFundMapping(List<TaxonomyAssociationGroupTaxonomyAssociationObjectModel> added,
            List<TaxonomyAssociationGroupTaxonomyAssociationObjectModel> updated,
            List<TaxonomyAssociationGroupTaxonomyAssociationObjectModel> deleted)
        {
            string status = string.Empty;

            var results = this.DataAccess.ExecuteScalar(DBConnectionString.HostedConnectionString(ClientName, DataAccess)
                 , SPVerticalImport_SaveTaxonomyGroupFundMapping,
                 DataAccess.CreateParameter(DBCAdded, SqlDbType.Structured, GetTaxonomyAssociationGroupFundDataTable(added)),
                 DataAccess.CreateParameter(DBCUpdated, SqlDbType.Structured, GetTaxonomyAssociationGroupFundDataTable(updated)),
                 DataAccess.CreateParameter(DBCDeleted, SqlDbType.Structured, GetTaxonomyAssociationGroupFundDataTable(deleted))
                 );

            if (results != null)
            {
                status = results.ToString();
            }
            return status;
        }
        private DataTable GetTaxonomyAssociationGroupFundDataTable(List<TaxonomyAssociationGroupTaxonomyAssociationObjectModel> TaxonomyGroupFundData)
        {
            DataTable taxonomyAssociationGroupFundDataTable = new DataTable();
            taxonomyAssociationGroupFundDataTable.Columns.Add("TaxonomyAssociationGroupId", typeof(int));
            taxonomyAssociationGroupFundDataTable.Columns.Add("TaxonomyAssociationId", typeof(int));

            taxonomyAssociationGroupFundDataTable.Columns.Add("Order", typeof(int));
            taxonomyAssociationGroupFundDataTable.Columns.Add("UtcModifiedDate", typeof(DateTime));
            taxonomyAssociationGroupFundDataTable.Columns.Add("ModifiedBy", typeof(int));

            if (TaxonomyGroupFundData != null && TaxonomyGroupFundData.Count > 0)
            {
                TaxonomyGroupFundData.ForEach(p =>
                {
                    taxonomyAssociationGroupFundDataTable.Rows.Add(

                        p.TaxonomyAssociationGroupId,
                        p.TaxonomyAssociationId,
                        p.Order,
                        null,
                        this.UserId
                    );
                });
            }

            return taxonomyAssociationGroupFundDataTable;
        }
        public string SaveTaxonomyGroupFundsExcelImport(List<TaxonomyAssociationGroupTaxonomyAssociationObjectModel> importData)
        {

            string status = null;

            //var results = this.DataAccess.ExecuteScalar(
            //    DBConnectionString.HostedConnectionString(ClientName, DataAccess),
            //    SPVerticalImport_SaveTaxonomyAssociationGroupFunds_ExcelImport,
            //    DataAccess.CreateParameter(DBCAdded, SqlDbType.Structured, GetTaxonomyAssociationGroupFundDataTable(importData))

            //);

            //if (results != null)
            //{
            //    status = results.ToString();
            //}

            //Commented above code and added below as part of load testing
            using (SqlConnection sqlConn = new SqlConnection(DBConnectionString.HostedConnectionString(ClientName, DataAccess)))
            {
                sqlConn.Open();
                using (SqlCommand command = new SqlCommand(SPVerticalImport_SaveTaxonomyAssociationGroupFunds_ExcelImport, sqlConn))
                {
                    command.CommandTimeout = ConfigValues.VerticalDataImport_ExcelImport_CommandTimeout;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Added", GetTaxonomyAssociationGroupFundDataTable(importData)));

                    var results = command.ExecuteScalar();
                    if (results != null)
                    {
                        status = results.ToString();
                    }
                }
                sqlConn.Close();
            }

            return status;
        }

        public List<TaxonomyAssociationObjectModel> VerifyTaxonomywithVerticalData(List<TaxonomyAssociationObjectModel> taxonomyData)
        {
            if (taxonomyData != null && taxonomyData.Count > 0)
            {
                DataTable clientMarketIds = new DataTable();
                clientMarketIds.Columns.Add("MarketId", typeof(string));
                clientMarketIds.Columns.Add("Level", typeof(int));

                taxonomyData.ForEach(p =>
                {
                    clientMarketIds.Rows.Add(p.MarketId, p.Level);
                });

                DataTable validData = this.DataAccess.ExecuteDataTable(
                    DBConnectionString.VerticalDBConnectionString(ClientName, DataAccess),
                    SPVerticalImport_VerifyTaxonomyAssociation,
                    DataAccess.CreateParameter(DBCClientMarketIds, SqlDbType.Structured, clientMarketIds)
                );

                foreach (DataRow entityRow in validData.Rows)
                {
                    List<TaxonomyAssociationObjectModel> lstTAData = taxonomyData.FindAll(p => p.MarketId == entityRow.Field<string>("MarketId"));
                    foreach (TaxonomyAssociationObjectModel taData in lstTAData)
                    {
                        taData.IsObjectinVerticalMarket = true;
                        taData.TaxonomyId = entityRow.Field<int>("TaxonomyId");
                        taData.RPFundName = entityRow.Field<string>("ProsName");


                    }
                }

                taxonomyData.RemoveAll(a => !a.IsObjectinVerticalMarket);
            }
            return taxonomyData;

        }
    }
}
