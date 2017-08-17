// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-27-2015
//
// Last Modified By : 
// Last Modified On : 11-12-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.HostedPages;
using RRD.FSG.RP.Model.Interfaces.HostedPages;
using RRD.FSG.RP.Model.Interfaces.System;
using RRD.FSG.RP.Model.Interfaces.VerticalMarket;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace RRD.FSG.RP.Model.Factories.HostedPages
{
    /// <summary>
    /// Class HostedClientRequestMaterialFactory.
    /// </summary>
    public class HostedClientRequestMaterialFactory : HostedPageBaseFactory, IHostedClientRequestMaterialFactory
    {
        /// <summary>
        /// The i hosted vertical page scenarios
        /// </summary>
        private IHostedVerticalPageScenariosFactory iHostedVerticalPageScenarios;
        /// <summary>
        /// The i system common factory
        /// </summary>
        private ISystemCommonFactory iSystemCommonFactory;

        /// <summary>
        /// The sp save request material email history
        /// </summary>
        private const string SPSaveRequestMaterialEmailHistory = "RPV2HostedSites_SaveRequestMaterialEmailHistory";
        /// <summary>
        /// The sp save request material print history
        /// </summary>
        private const string SPSaveRequestMaterialPrintHistory = "RPV2HostedSites_SaveRequestMaterialPrintHistory";
        /// <summary>
        /// The sp update request material email click date
        /// </summary>
        private const string SPUpdateRequestMaterialEmailClickDate = "RPV2HostedSites_UpdateRequestMaterialEmailClickDate";
        /// <summary>
        /// The sp get request material print request data
        /// </summary>
        private const string SPGetRequestMaterialPrintRequestData = "RPV2HostedSites_GetRequestMaterialPrintRequestData";

        /// <summary>
        /// The DBC recip email
        /// </summary>
        private const string DBCRecipEmail = "RecipEmail";

        /// <summary>
        /// The DBC unique identifier
        /// </summary>
        private const string DBCUniqueID = "UniqueID";

        /// <summary>
        /// The DBC request batch identifier
        /// </summary>
        private const string DBCRequestBatchId = "RequestBatchId";

        /// <summary>
        /// The DBC site name
        /// </summary>
        private const string DBCSiteName = "SiteName";

        /// <summary>
        /// The DBCF click date
        /// </summary>
        private const string DBCFClickDate = "FClickDate";

        /// <summary>
        /// The DBC user agent
        /// </summary>
        private const string DBCUserAgent = "UserAgent";

        /// <summary>
        /// The dbcip address
        /// </summary>
        private const string DBCIPAddress = "IPAddress";

        /// <summary>
        /// The DBC referer
        /// </summary>
        private const string DBCReferer = "Referer";

        /// <summary>
        /// The DBC request URI string
        /// </summary>
        private const string DBCRequestUriString = "RequestUriString";

        /// <summary>
        /// The DBC sent
        /// </summary>
        private const string DBCSent = "Sent";

        /// <summary>
        /// The DBC email pros details
        /// </summary>
        private const string DBCEmailProsDetails = "RequestMaterialEmailProsDetail";

        /// <summary>
        /// The DBC print pros details
        /// </summary>
        private const string DBCPrintProsDetails = "RequestMaterialPrintProsDetail";

        /// <summary>
        /// The DBC client company name
        /// </summary>
        private const string DBCClientCompanyName = "ClientCompanyName";

        /// <summary>
        /// The DBC client first name
        /// </summary>
        private const string DBCClientFirstName = "ClientFirstName";

        /// <summary>
        /// The DBC client middle name
        /// </summary>
        private const string DBCClientMiddleName = "ClientMiddleName";

        /// <summary>
        /// The DBC client last name
        /// </summary>
        private const string DBCClientLastName = "ClientLastName";

        /// <summary>
        /// The DBC client full name
        /// </summary>
        private const string DBCClientFullName = "ClientFullName";

        /// <summary>
        /// The DBC address1
        /// </summary>
        private const string DBCAddress1 = "Address1";

        /// <summary>
        /// The DBC address2
        /// </summary>
        private const string DBCAddress2 = "Address2";

        /// <summary>
        /// The DBC city
        /// </summary>
        private const string DBCCity = "City";

        /// <summary>
        /// The DBC state or province
        /// </summary>
        private const string DBCStateOrProvince = "StateOrProvince";

        /// <summary>
        /// The DBC postal code
        /// </summary>
        private const string DBCPostalCode = "PostalCode";

        /// <summary>
        /// The DBC unique identifier
        /// </summary>
        private const string DBCUniqueId = "UniqueId";

        /// <summary>
        /// The DBC document type identifier
        /// </summary>
        private const string DBCDocumentTypeId = "DocumentTypeId";

        /// <summary>
        /// The DBC taxonomy association identifier
        /// </summary>
        private const string DBCTaxonomyAssociationId = "TaxonomyAssociationId";

        /// <summary>
        /// The DBC report from date
        /// </summary>
        private const string DBCReportFromDate = "ReportFromDate";

        /// <summary>
        /// The DBC report to date
        /// </summary>
        private const string DBCReportToDate = "ReportToDate";


        /// <summary>
        /// Initializes a new instance of the <see cref="HostedClientRequestMaterialFactory"/> class.
        /// </summary>
        /// <param name="paramDataAccess">The parameter data access.</param>
        /// <param name="paramHostedVerticalPageScenarios">The parameter hosted vertical page scenarios.</param>
        /// <param name="paramSystemCommonFactory">The parameter system common factory.</param>
        public HostedClientRequestMaterialFactory(IDataAccess paramDataAccess,
                    IHostedVerticalPageScenariosFactory paramHostedVerticalPageScenarios,
            ISystemCommonFactory paramSystemCommonFactory)
            : base(paramDataAccess)
        {
            iHostedVerticalPageScenarios = paramHostedVerticalPageScenarios;
            iSystemCommonFactory = paramSystemCommonFactory;
        }

        /// <summary>
        /// Saves the email details.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="site">The site.</param>
        /// <param name="emailHistoryObjectModel">The email history object model.</param>
        public void SaveEmailDetails(string clientName, string site, RequestMaterialEmailHistory emailHistoryObjectModel)
        {
            var emailProsDetail = new DataTable();            
            emailProsDetail.Columns.Add("TaxonomyAssociationId", typeof(int));
            emailProsDetail.Columns.Add("DocumentTypeId", typeof(int));
            emailProsDetail.Columns.Add("Quantity", typeof(int));

            TaxonomyAssociationData taxanomyData = emailHistoryObjectModel.TaxonomyAssociationData;

            foreach (var selectedFund in taxanomyData.DocumentTypes)
            {
                emailProsDetail.Rows.Add(taxanomyData.TaxonomyAssociationID, selectedFund.DocumentTypeId, 1);

            }

            DataAccess.ExecuteNonQuery(
               DBConnectionString.HostedConnectionString(clientName, DataAccess), SPSaveRequestMaterialEmailHistory,

               DataAccess.CreateParameter(DBCSiteName, DbType.String, site),
               DataAccess.CreateParameter(DBCRecipEmail, DbType.String, emailHistoryObjectModel.RecipEmail),
               DataAccess.CreateParameter(DBCUniqueID, DbType.Guid, emailHistoryObjectModel.UniqueID),
               DataAccess.CreateParameter(DBCRequestBatchId, DbType.Guid, emailHistoryObjectModel.RequestBatchId),
               DataAccess.CreateParameter(DBCRequestUriString, DbType.String, emailHistoryObjectModel.RequestUriString),
               DataAccess.CreateParameter(DBCUserAgent, DbType.String, emailHistoryObjectModel.UserAgent),
               DataAccess.CreateParameter(DBCIPAddress, DbType.String, emailHistoryObjectModel.IPAddress),
               DataAccess.CreateParameter(DBCReferer, DbType.String, emailHistoryObjectModel.Referer),
               DataAccess.CreateParameter(DBCSent, DbType.Boolean, emailHistoryObjectModel.Sent),
               DataAccess.CreateParameter(DBCEmailProsDetails, SqlDbType.Structured, emailProsDetail)

                );

        }

        /// <summary>
        /// Saves the print details.
        /// </summary>
        /// <param name="clientName">Name of the clientSavePrintDetails(.</param>
        /// <param name="site">The site.</param>
        /// <param name="emailPrintObjectModel">The email print object model.</param>
        public void SavePrintDetails(string clientName, string site, RequestMaterialPrintHistory emailPrintObjectModel)
        {
            var printProsDetail = new DataTable();
            printProsDetail.Columns.Add("TaxonomyAssociationId", typeof(int));
            printProsDetail.Columns.Add("DocumentTypeId", typeof(string));
            printProsDetail.Columns.Add("Quantity", typeof(int));

            TaxonomyAssociationData taxanomyData = emailPrintObjectModel.TaxonomyAssociationData;

            foreach (var selectedFund in taxanomyData.DocumentTypes.ToList())
            {
                printProsDetail.Rows.Add(taxanomyData.TaxonomyAssociationID, selectedFund.DocumentTypeId, 1);
            }

            DataAccess.ExecuteNonQuery(
               DBConnectionString.HostedConnectionString(clientName, DataAccess), SPSaveRequestMaterialPrintHistory,
               DataAccess.CreateParameter(DBCSiteName, DbType.String, site),
               DataAccess.CreateParameter(DBCClientFullName, DbType.String, emailPrintObjectModel.ClientFullName),
               DataAccess.CreateParameter(DBCClientCompanyName, DbType.String, emailPrintObjectModel.ClientCompanyName),
               DataAccess.CreateParameter(DBCClientFirstName, DbType.String, emailPrintObjectModel.ClientFirstName),
               DataAccess.CreateParameter(DBCClientMiddleName, DbType.String, emailPrintObjectModel.ClientMiddleName),
               DataAccess.CreateParameter(DBCClientLastName, DbType.String, emailPrintObjectModel.ClientLastName),
               DataAccess.CreateParameter(DBCAddress1, DbType.String, emailPrintObjectModel.Address1),
               DataAccess.CreateParameter(DBCAddress2, DbType.String, emailPrintObjectModel.Address2),
               DataAccess.CreateParameter(DBCStateOrProvince, DbType.String, emailPrintObjectModel.StateOrProvince),
               DataAccess.CreateParameter(DBCPostalCode, DbType.String, emailPrintObjectModel.PostalCode),
               DataAccess.CreateParameter(DBCCity, DbType.String, emailPrintObjectModel.City),
               DataAccess.CreateParameter(DBCUniqueID, DbType.Guid, emailPrintObjectModel.UniqueID),
               DataAccess.CreateParameter(DBCRequestBatchId, DbType.Guid, emailPrintObjectModel.RequestBatchId),
               DataAccess.CreateParameter(DBCRequestUriString, DbType.String, emailPrintObjectModel.RequestUriString),
               DataAccess.CreateParameter(DBCUserAgent, DbType.String, emailPrintObjectModel.UserAgent),
               DataAccess.CreateParameter(DBCIPAddress, DbType.String, emailPrintObjectModel.IPAddress),
               DataAccess.CreateParameter(DBCReferer, DbType.String, emailPrintObjectModel.Referer),
               DataAccess.CreateParameter(DBCPrintProsDetails, SqlDbType.Structured, printProsDetail)

                );
        }

        /// <summary>
        /// Updates the email click date.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="uniqueId">The unique identifier.</param>
        /// <param name="documentTypeId">The document type identifier.</param>
        /// <returns>System.Int32.</returns>
        public int UpdateEmailClickDate(string clientName, Guid uniqueId, int documentTypeId)
        {
            int taxonomyAssociationId = 0;

            DbParameterCollection collection = DataAccess.ExecuteNonQueryReturnOutputParams(
               DBConnectionString.HostedConnectionString(clientName, DataAccess), SPUpdateRequestMaterialEmailClickDate,

              DataAccess.CreateParameter(DBCUniqueId, DbType.Guid, uniqueId),
              DataAccess.CreateParameter(DBCDocumentTypeId, DbType.Int32, documentTypeId),
              DataAccess.CreateParameter(DBCTaxonomyAssociationId, DbType.Int32, taxonomyAssociationId, ParameterDirection.Output)
              );

            if (collection != null)
            {
                if (collection["TaxonomyAssociationId"].Value != DBNull.Value)
                {
                    taxonomyAssociationId = Convert.ToInt32(collection["TaxonomyAssociationId"].Value);
                }
            }

            return taxonomyAssociationId;
        }

        /// <summary>
        /// Gets the request material print requests.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="reportFromDate">The report from date.</param>
        /// <param name="reportToDate">The report to date.</param>
        /// <returns>List&lt;RequestMaterialPrintHistory&gt;.</returns>
        public List<RequestMaterialPrintHistory> GetRequestMaterialPrintRequests(string clientName, DateTime reportFromDate, DateTime reportToDate)
        {
            DataTable clientTaxonomyIDsWithDocTypeIDs = new DataTable();

            clientTaxonomyIDsWithDocTypeIDs.Columns.Add("TaxonomyID", typeof(Int32));
            clientTaxonomyIDsWithDocTypeIDs.Columns.Add("IsNameOverrideProvided", typeof(bool));
            clientTaxonomyIDsWithDocTypeIDs.Columns.Add("DocumentTypeID", typeof(Int32));
            clientTaxonomyIDsWithDocTypeIDs.Columns.Add("IsDocumentTypeNameOverrideProvided", typeof(bool));
            clientTaxonomyIDsWithDocTypeIDs.Columns.Add("IsParent", typeof(bool));

            List<RequestMaterialPrintHistory> requestMaterialPrintRequestData = GetRequestMaterialPrintRequestData(clientName, reportFromDate, reportToDate,
                                                                        clientTaxonomyIDsWithDocTypeIDs);

            if (requestMaterialPrintRequestData.Count > 0)
            {
                return iHostedVerticalPageScenarios.GetRequestMaterialPrintRequests(clientTaxonomyIDsWithDocTypeIDs, clientName,
                                                                                                            requestMaterialPrintRequestData);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the request material print request data.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="reportFromDate">The report from date.</param>
        /// <param name="reportToDate">The report to date.</param>
        /// <param name="verticalIds">The vertical ids.</param>
        /// <returns>List&lt;RequestMaterialPrintHistory&gt;.</returns>
        private List<RequestMaterialPrintHistory> GetRequestMaterialPrintRequestData(string clientName, DateTime reportFromDate, DateTime reportToDate, DataTable verticalIds)
        {
            List<RequestMaterialPrintHistory> requestMaterialPrintRequestData = new List<RequestMaterialPrintHistory>();           

            int previousRequestMaterialPrintHistoryID = -1;
            RequestMaterialPrintHistory requestMaterialPrintHistory = null;
            bool taxonomyNameOverride = false;

            DataTable results = DataAccess.ExecuteDataTable(DBConnectionString.HostedConnectionString(clientName, DataAccess),
                         SPGetRequestMaterialPrintRequestData,
                         DataAccess.CreateParameter(DBCReportFromDate, DbType.DateTime, reportFromDate),
                         DataAccess.CreateParameter(DBCReportToDate, DbType.DateTime, reportToDate)
                         );

            foreach (DataRow datarow in results.Rows)
            {
                int requestMaterialPrintHistoryID = Convert.ToInt32(datarow["RequestMaterialPrintHistoryID"]);

                if (previousRequestMaterialPrintHistoryID != requestMaterialPrintHistoryID)
                {
                    if (previousRequestMaterialPrintHistoryID != -1)
                    {
                        requestMaterialPrintRequestData.Add(requestMaterialPrintHistory);

                    }
                    requestMaterialPrintHistory = new RequestMaterialPrintHistory();
                    requestMaterialPrintHistory.TaxonomyAssociationData = new TaxonomyAssociationData();

                    previousRequestMaterialPrintHistoryID = requestMaterialPrintHistoryID;

                    requestMaterialPrintHistory.TaxonomyAssociationData.DocumentTypes = new List<HostedDocumentType>();

                    string taxonomyName = datarow["NameOverride"].ToString();

                    requestMaterialPrintHistory.RequestMaterialPrintHistoryID = requestMaterialPrintHistoryID;
                    requestMaterialPrintHistory.UniqueID = Guid.Parse(datarow["UniqueID"].ToString());
                    requestMaterialPrintHistory.RequestDateUtc = Convert.ToDateTime(datarow["RequestDateUtc"]);
                    requestMaterialPrintHistory.ClientCompanyName = datarow["ClientCompanyName"].ToString();
                    requestMaterialPrintHistory.ClientFirstName = datarow["ClientFirstName"].ToString();
                    requestMaterialPrintHistory.ClientLastName = datarow["ClientLastName"].ToString();
                    requestMaterialPrintHistory.Address1 = datarow["Address1"].ToString();
                    requestMaterialPrintHistory.Address2 = datarow["Address2"].ToString();                    
                    requestMaterialPrintHistory.City = datarow["City"].ToString();
                    requestMaterialPrintHistory.StateOrProvince = datarow["StateOrProvince"].ToString();
                    requestMaterialPrintHistory.PostalCode = datarow["PostalCode"].ToString();
                    requestMaterialPrintHistory.Quantity = int.Parse(datarow["Quantity"].ToString());


                    requestMaterialPrintHistory.TaxonomyAssociationData.TaxonomyName = taxonomyName;
                    requestMaterialPrintHistory.TaxonomyAssociationData.TaxonomyAssociationID = Convert.ToInt32(datarow["TaxonomyAssociationID"]);
                    requestMaterialPrintHistory.TaxonomyAssociationData.TaxonomyID = Convert.ToInt32(datarow["TaxonomyID"]);
                    taxonomyNameOverride = !string.IsNullOrWhiteSpace(taxonomyName);

                }
                            
                int documentTypeId = Convert.ToInt32(datarow["DocumentTypeId"]);
                string documentTypeLinkText = datarow["DocumentTypeLinkText"].ToString();


                requestMaterialPrintHistory.TaxonomyAssociationData.DocumentTypes.Add(new HostedDocumentType()
                {
                    DocumentTypeLinkText = documentTypeLinkText,
                    DocumentTypeId = documentTypeId,
                    SKUName = datarow["SKUName"].ToString() 
                });

                verticalIds.Rows.Add(Convert.ToInt32(datarow["TaxonomyID"]),
                                    taxonomyNameOverride,
                                    documentTypeId,
                                    !string.IsNullOrWhiteSpace(documentTypeLinkText),
                                    false
                                    );
            }

            if (previousRequestMaterialPrintHistoryID != -1)
            {
                requestMaterialPrintRequestData.Add(requestMaterialPrintHistory);
            }

            return requestMaterialPrintRequestData;
        }
    }
}
