
// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-09-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.HostedPages;
using RRD.FSG.RP.Model.Interfaces.VerticalMarket;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RRD.FSG.RP.Model.Factories.VerticalMarket
{
    /// <summary>
    /// Class HostedVerticalBasePageScenariosFactory.
    /// </summary>
    public class HostedVerticalBasePageScenariosFactory : HostedPageBaseFactory, IHostedVerticalPageScenariosFactory
    {

        /// <summary>
        /// The sp get taxonomy association links
        /// </summary>
        private const string SPGetTaxonomyAssociationLinks = "RPV2HostedSites_GetTaxonomyAssociationLinks";

        /// <summary>
        /// The sp get billing report
        /// </summary>
        private const string SPGetBillingReport = "RPV2HostedSites_GetBillingReport";

        /// <summary>
        /// The sp get URL Generation
        /// </summary>
        private const string SPGetUrlGeneration = "RPV2HostedSites_GetUrlGeneration";
        //endmine
        /// <summary>
        /// The sp get taxonomy association hierarchy
        /// </summary>
        private const string SPGetTaxonomyAssociationHierarchy = "RPV2HostedSites_GetTaxonomyAssociationHierarchy";

        /// <summary>
        /// The sp get taxonomy specific document frame
        /// </summary>
        private const string SPGetTaxonomySpecificDocumentFrame = "RPV2HostedSites_GetTaxonomySpecificDocumentFrame";

        /// <summary>
        /// The sp get XBRL details for taxonomy identifier
        /// </summary>
        private const string SPGetXBRLDetailsForTaxonomyID = "RPV2HostedSites_GetXBRLDetailsForTaxonomyID";

        /// <summary>
        /// The DBC taxonomy association link data type
        /// </summary>
        private const string DBCTaxonomyAssociationLinkDataType = "RPV2HostedSitesTaxonomyAssociationLinkDataType";

        /// <summary>
        /// The DBC Billing Report data type
        /// </summary>
        private const string DBCBillingReportDataType = "RPV2HostedSitesTaxonomyBillingReport";

        /// <summary>
        /// The DBC Billing Report data type
        /// </summary>
        private const string DBCUrlGenerationDataType = "RPV2HostedSitesTaxonomyUrlGeneration";
        //endmine

        /// <summary>
        /// The DBC taxonomy association hierarchy docs
        /// </summary>
        private const string DBCTaxonomyAssociationHierarchyDocs = "RPV2HostedSitesTaxonomyAssociationHierarchyDocs";

        /// <summary>
        /// The DBC taxonomy identifier
        /// </summary>
        private const string DBCTaxonomyID = "TaxonomyID";



        /// <summary>
        /// The DBC client name
        /// </summary>
        private const string DBCClientName = "ClientName";

        /// <summary>
        /// Initializes a new instance of the <see cref="HostedVerticalBasePageScenariosFactory" /> class.
        /// </summary>
        /// <param name="paramDataAccess">The parameter data access.</param>
        public HostedVerticalBasePageScenariosFactory(IDataAccess paramDataAccess)
            : base(paramDataAccess) { }



        /// <summary>
        /// Gets the taxonomy association links vertical data.
        /// </summary>
        /// <param name="clientTaxonomyIDs">The client taxonomy i ds.</param>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="taxonomyassociationlinks">The taxonomyassociationlinks.</param>
        /// <returns>List&lt;TaxonomyAssociationLinkModel&gt;.</returns>
        public List<TaxonomyAssociationLinkModel> GetTaxonomyAssociationLinksVerticalData(DataTable clientTaxonomyIDs, string clientName,
                                                                            List<TaxonomyAssociationLinkModel> taxonomyassociationlinks)
        {


            DataTable results = DataAccess.ExecuteDataTable(DBConnectionString.VerticalDBConnectionString(clientName,
                                                                                        DataAccess),
                        SPGetTaxonomyAssociationLinks,
                        DataAccess.CreateParameter(DBCTaxonomyAssociationLinkDataType, SqlDbType.Structured, clientTaxonomyIDs)
                        );


            foreach (DataRow datarow in results.Rows)
            {
                int TaxonomyID = datarow.Field<Int32>("TaxonomyID");
                string TaxonomyName = datarow.Field<string>("TaxonomyName");

                TaxonomyAssociationLinkModel taxonomyassociationlink = taxonomyassociationlinks.Find(p => p.TaxonomyID == TaxonomyID);

                if (taxonomyassociationlink != null)
                {
                    taxonomyassociationlink.IsObjectinVerticalMarket = true;
                    if (!String.IsNullOrEmpty(TaxonomyName))
                    {
                        taxonomyassociationlink.TaxonomyAssocationName = TaxonomyName;
                    }
                }
            }

            taxonomyassociationlinks.RemoveAll(a => a.IsObjectinVerticalMarket == false);

            return taxonomyassociationlinks;
        }

      /// <summary>
      /// Gets Billing data from vertical db
      /// </summary>
      /// <param name="marketIDs"></param>
      /// <param name="clientName"></param>
      /// <param name="billingReport"></param>
      /// <returns></returns>
        public List<BillingReportActiveFundDetails> GetBillingReportVerticalData(DataTable marketIDs, string clientName,
                                                                            List<BillingReportActiveFundDetails> billingReport)
        {
            DataTable results = DataAccess.ExecuteDataTable(DBConnectionString.VerticalDBConnectionString(clientName,
                                                                                        DataAccess),
                        SPGetBillingReport,
                        DataAccess.CreateParameter(DBCBillingReportDataType, SqlDbType.Structured, marketIDs)
                        );
           



            foreach (DataRow datarow in results.Rows)
            {
                string MarketID = datarow.Field<string>("CUSIP");
                string FundName = datarow.Field<string>("List of Funds");
                string seriesID = datarow.Field<string>("SeriesID");
                bool IsSummaryProspectus = datarow.Field<bool>("SummaryProspectusFlag");

                List<BillingReportActiveFundDetails> lstbillingData = billingReport.FindAll(p => p.MarketID == MarketID);

                foreach (BillingReportActiveFundDetails billingData in lstbillingData)
                {
                    if (billingData != null)
                    {
                        billingData.IsObjectinVerticalMarket = true;
                        if (!String.IsNullOrEmpty(FundName))
                        {
                            billingData.FundName = FundName;
                        }
                        billingData.SeriesID = seriesID;
                        billingData.IsSummaryProspectus = IsSummaryProspectus;
                    }
                }
                
            }

            billingReport.RemoveAll(a => a.IsObjectinVerticalMarket == false);

            return billingReport;
        }

        
        /// <summary>
        /// Gets the URL Generator vertical data.
        /// </summary>
        /// <param name="clientTaxonomyIDs">The client market ids.</param>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="taxonomyassociationlinks">The UrlGeneratordata.</param>
        /// <returns>List&lt;UrlGenerationObjectModel&gt;.</returns>
        /// <summary>
        /// Gets the URL Generator vertical data.
        /// </summary>
        /// <param name="clientTaxonomyIDs">The client market ids.</param>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="taxonomyassociationlinks">The UrlGeneratordata.</param>
        /// <returns>List&lt;UrlGenerationObjectModel&gt;.</returns>
        public List<UrlGenerationObjectModel> GetUrlGenerationVerticalData(DataTable marketIDs, string clientName,
                                                                            List<UrlGenerationObjectModel> urlGeneration)
        {
            DataTable results = DataAccess.ExecuteDataTable(DBConnectionString.VerticalDBConnectionString(clientName,
                                                                                        DataAccess),
                        SPGetUrlGeneration,
                        DataAccess.CreateParameter(DBCUrlGenerationDataType, SqlDbType.Structured, marketIDs)
                        );



            foreach (DataRow datarow in results.Rows)
            {
                int TaxonomyId = datarow.Field<int>("TaxonomyId");
                string FundName = datarow.Field<string>("List of Funds");
                List<UrlGenerationObjectModel> lstUrlGenerationData = urlGeneration.FindAll(p => p.TaxonomyId == TaxonomyId);
                foreach (UrlGenerationObjectModel urlGenerationData in lstUrlGenerationData)
                {                    
                    urlGenerationData.IsObjectinVerticalMarket = true;
                    if (!String.IsNullOrEmpty(FundName))
                    {
                        urlGenerationData.FundName = FundName;
                    }
                    
                }

            }
            urlGeneration.RemoveAll(a => a.IsObjectinVerticalMarket == false);

            urlGeneration = urlGeneration.OrderBy(p => p.SiteName).ThenBy(p => p.FundName).ThenBy(p => p.TLEExternalID).ThenBy(p => p.DocumentTypeOrder).ToList();

            return urlGeneration;
        }




        /// <summary>
        /// Gets the taxonomy association hierarchy documents vertical data.
        /// </summary>
        /// <param name="clientTaxonomyIDs">The client taxonomy i ds.</param>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="taxonomyAssociationHierarchyModel">The taxonomy association hierarchy model.</param>
        /// <returns>TaxonomyAssociationHierarchyModel.</returns>
        public TaxonomyAssociationHierarchyModel GetTaxonomyAssociationHierarchyDocumentsVerticalData(DataTable clientTaxonomyIDs, string clientName, TaxonomyAssociationHierarchyModel taxonomyAssociationHierarchyModel)
        {

            int previousTaxonomyID = -1;
            bool isParent = false;

            DataTable results = DataAccess.ExecuteDataTable(DBConnectionString.VerticalDBConnectionString(clientName, DataAccess),
                       SPGetTaxonomyAssociationHierarchy,
                       DataAccess.CreateParameter(DBCTaxonomyAssociationHierarchyDocs, SqlDbType.Structured, clientTaxonomyIDs)
                       );

            TaxonomyAssociationData taxonomyAssociationData = null;
            foreach (DataRow datarow in results.Rows)
            {
                int taxonomyID = datarow.Field<Int32>("TaxonomyID");
                string taxonomyName = datarow.Field<string>("TaxonomyName");
                isParent = datarow.Field<bool>("IsParent");

                if (previousTaxonomyID != taxonomyID)
                {
                    if (isParent)
                    {
                        taxonomyAssociationData = taxonomyAssociationHierarchyModel.ParentTaxonomyAssociationData.Find(p => p.TaxonomyID == taxonomyID);
                        taxonomyAssociationData.IsObjectinVerticalMarket = true;
                        if (!String.IsNullOrEmpty(taxonomyName))
                        {
                            taxonomyAssociationData.TaxonomyName = taxonomyName;
                        }
                    }
                    else
                    {
                        taxonomyAssociationData = taxonomyAssociationHierarchyModel.ChildTaxonomyAssociationData.Find(c => c.TaxonomyID == taxonomyID);
                        taxonomyAssociationData.IsObjectinVerticalMarket = true;
                        if (!String.IsNullOrEmpty(taxonomyName))
                        {
                            taxonomyAssociationData.TaxonomyName = taxonomyName;
                        }
                    }

                    HostedSiteFootNotes hostedSiteFootNotes = taxonomyAssociationHierarchyModel.FootNotes.Find(f => f.TaxonomyID == taxonomyID);
                    if (hostedSiteFootNotes != null)
                    {
                        hostedSiteFootNotes.IsObjectinVerticalMarket = true;
                        if (!String.IsNullOrEmpty(taxonomyName))
                        {
                            hostedSiteFootNotes.TaxonomyName = taxonomyName;
                        }
                    }

                }

                previousTaxonomyID = taxonomyID;

                int documentTypeId = datarow.Field<Int32>("DocumentTypeId");

                HostedDocumentType hostedDocumentType = taxonomyAssociationData.DocumentTypes.Find(t => t.DocumentTypeId == documentTypeId);
                if (hostedDocumentType != null)
                {
                    hostedDocumentType.IsObjectinVerticalMarket = true;
                    hostedDocumentType.VerticalMarketID = datarow.Field<string>("VerticalMarketId");
                    if (isParent)
                    {
                        taxonomyAssociationHierarchyModel.ParentHeaders.Find(p => p.DocumentTypeId == documentTypeId).VerticalMarketID = hostedDocumentType.VerticalMarketID;
                    }
                    else
                    {
                        taxonomyAssociationHierarchyModel.ChildHeaders.Find(p => p.DocumentTypeId == documentTypeId).VerticalMarketID = hostedDocumentType.VerticalMarketID;
                    }
                }

            }

            RemoveInvalidTaxonomyAssociationHierarchyDocumentData(taxonomyAssociationHierarchyModel);

            return taxonomyAssociationHierarchyModel;
        }

        /// <summary>
        /// Removes the invalid taxonomy association hierarchy document data.
        /// </summary>
        /// <param name="taxonomyAssociationHierarchyModel">The taxonomy association hierarchy model.</param>
        private void RemoveInvalidTaxonomyAssociationHierarchyDocumentData(TaxonomyAssociationHierarchyModel taxonomyAssociationHierarchyModel)
        {
            taxonomyAssociationHierarchyModel.ParentTaxonomyAssociationData.RemoveAll(p => !p.IsObjectinVerticalMarket);
            taxonomyAssociationHierarchyModel.ParentTaxonomyAssociationData.ForEach(p => p.DocumentTypes.RemoveAll(d => !d.IsObjectinVerticalMarket));
            taxonomyAssociationHierarchyModel.ParentTaxonomyAssociationData.Sort((x, y) => x.TaxonomyName.CompareTo(y.TaxonomyName));
            taxonomyAssociationHierarchyModel.ChildTaxonomyAssociationData.RemoveAll(p => !p.IsObjectinVerticalMarket);
            taxonomyAssociationHierarchyModel.ChildTaxonomyAssociationData.ForEach(p => p.DocumentTypes.RemoveAll(d => !d.IsObjectinVerticalMarket));
            taxonomyAssociationHierarchyModel.ChildTaxonomyAssociationData.Sort((x, y) => x.TaxonomyName.CompareTo(y.TaxonomyName));
            taxonomyAssociationHierarchyModel.FootNotes.RemoveAll(p => !p.IsObjectinVerticalMarket);
            taxonomyAssociationHierarchyModel.FootNotes.Sort((x, y) => x.TaxonomyName.CompareTo(y.TaxonomyName));

            taxonomyAssociationHierarchyModel.ParentHeaders.Sort((x, y) => x.Order.CompareTo(y.Order));
            taxonomyAssociationHierarchyModel.ChildHeaders.Sort((x, y) => x.Order.CompareTo(y.Order));
        }


        /// <summary>
        /// Gets the taxonomy association documents vertical data.
        /// </summary>
        /// <param name="clientTaxonomyIDs">The client taxonomy i ds.</param>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="taxonomyAssociationDocumentsModel">The taxonomy association documents model.</param>
        /// <returns>TaxonomyAssociationDocumentsModel.</returns>
        public TaxonomyAssociationDocumentsModel GetTaxonomyAssociationDocumentsVerticalData(DataTable clientTaxonomyIDs, string clientName, TaxonomyAssociationDocumentsModel taxonomyAssociationDocumentsModel)
        {
            int previousTaxonomyID = -1;

            DataTable results = DataAccess.ExecuteDataTable(DBConnectionString.VerticalDBConnectionString(clientName, DataAccess),
                       SPGetTaxonomyAssociationHierarchy,
                       DataAccess.CreateParameter(DBCTaxonomyAssociationHierarchyDocs, SqlDbType.Structured, clientTaxonomyIDs)
                       );

            TaxonomyAssociationData taxonomyAssociationData = null;
            foreach (DataRow datarow in results.Rows)
            {
                int taxonomyID = datarow.Field<Int32>("TaxonomyID");
                string taxonomyName = datarow.Field<string>("TaxonomyName");

                if (previousTaxonomyID != taxonomyID)
                {

                    taxonomyAssociationData = taxonomyAssociationDocumentsModel.TaxonomyAssociationDocumentsData.Find(p => p.TaxonomyID == taxonomyID);
                    taxonomyAssociationData.IsObjectinVerticalMarket = true;
                    if (!String.IsNullOrEmpty(taxonomyName))
                    {
                        taxonomyAssociationData.TaxonomyName = taxonomyName;
                    }


                    HostedSiteFootNotes hostedSiteFootNotes = taxonomyAssociationDocumentsModel.FootNotes.Find(f => f.TaxonomyID == taxonomyID);
                    if (hostedSiteFootNotes != null)
                    {
                        hostedSiteFootNotes.IsObjectinVerticalMarket = true;
                        if (!String.IsNullOrEmpty(taxonomyName))
                        {
                            hostedSiteFootNotes.TaxonomyName = taxonomyName;
                        }
                    }
                }

                previousTaxonomyID = taxonomyID;

                int documentTypeId = datarow.Field<Int32>("DocumentTypeId");
                HostedDocumentType hostedDocumentType = taxonomyAssociationData.DocumentTypes.Find(t => t.DocumentTypeId == documentTypeId);
                if (hostedDocumentType != null)
                {
                    hostedDocumentType.IsObjectinVerticalMarket = true;
                    hostedDocumentType.VerticalMarketID = datarow.Field<string>("VerticalMarketId");
                    taxonomyAssociationDocumentsModel.DocumentTypeHeaders.Find(p => p.DocumentTypeId == documentTypeId).VerticalMarketID = hostedDocumentType.VerticalMarketID;
                }
            }

            taxonomyAssociationDocumentsModel.TaxonomyAssociationDocumentsData.RemoveAll(p => !p.IsObjectinVerticalMarket);

            taxonomyAssociationDocumentsModel.TaxonomyAssociationDocumentsData.ForEach(p => p.DocumentTypes.RemoveAll(d => !d.IsObjectinVerticalMarket));

            taxonomyAssociationDocumentsModel.TaxonomyAssociationDocumentsData.Sort((x, y) => x.TaxonomyName.CompareTo(y.TaxonomyName));

            taxonomyAssociationDocumentsModel.FootNotes.RemoveAll(p => !p.IsObjectinVerticalMarket);
            taxonomyAssociationDocumentsModel.FootNotes.Sort((x, y) => x.TaxonomyName.CompareTo(y.TaxonomyName));
            
            taxonomyAssociationDocumentsModel.DocumentTypeHeaders.Sort((x, y) => x.Order.CompareTo(y.Order));

            return taxonomyAssociationDocumentsModel;
        }


        /// <summary>
        /// Gets the taxonomy specific documents vertical data.
        /// </summary>
        /// <param name="clientTaxonomyIDs">The client taxonomy i ds.</param>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="taxonomyAssociationData">The taxonomy association data.</param>
        /// <returns>TaxonomyAssociationData.</returns>
        public TaxonomyAssociationData GetTaxonomySpecificDocumentsVerticalData(DataTable clientTaxonomyIDs, string clientName, TaxonomyAssociationData taxonomyAssociationData)
        {
            int previousTaxonomyID = -1;

            DataTable results = DataAccess.ExecuteDataTable(DBConnectionString.VerticalDBConnectionString(clientName, DataAccess),
                       SPGetTaxonomySpecificDocumentFrame,
                       DataAccess.CreateParameter(DBCTaxonomyAssociationHierarchyDocs, SqlDbType.Structured, clientTaxonomyIDs),
                       DataAccess.CreateParameter(DBCClientName, SqlDbType.VarChar, clientName)
                       );

            foreach (DataRow datarow in results.Rows)
            {
                int taxonomyID = datarow.Field<Int32>("TaxonomyID");
                string taxonomyName = datarow.Field<string>("TaxonomyName");

                if (previousTaxonomyID == -1 && taxonomyAssociationData.TaxonomyID == taxonomyID)
                {
                    taxonomyAssociationData.IsObjectinVerticalMarket = true;
                    if (!String.IsNullOrEmpty(taxonomyName))
                    {
                        taxonomyAssociationData.TaxonomyName = taxonomyName;
                    }
                }

                previousTaxonomyID = taxonomyID;

                int documentTypeId = datarow.Field<Int32>("DocumentTypeId");
                HostedDocumentType hostedDocumentType = taxonomyAssociationData.DocumentTypes.Find(t => t.DocumentTypeId == documentTypeId);
                if (hostedDocumentType != null)
                {
                    hostedDocumentType.IsObjectinVerticalMarket = true;
                    hostedDocumentType.ContentURI = datarow.Field<string>("ContentUri");
                    hostedDocumentType.VerticalMarketID = datarow.Field<string>("VerticalMarketId");
                }
            }

            if (taxonomyAssociationData.IsObjectinVerticalMarket)
            {
                taxonomyAssociationData.DocumentTypes.RemoveAll(p => !p.IsObjectinVerticalMarket);
                taxonomyAssociationData.DocumentTypes = taxonomyAssociationData.DocumentTypes.OrderBy(x => x.DocumentTypeOrder).ToList();
            }
            else
            {
                taxonomyAssociationData = null;
            }

            return taxonomyAssociationData;

        }


        /// <summary>
        /// Gets the XBRL details for taxonomy identifier.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="taxonomyId">The taxonomy identifier.</param>
        /// <param name="taxonomyName">Name of the taxonomy.</param>
        /// <returns>List&lt;XBRLObjectModel&gt;.</returns>
        public List<XBRLObjectModel> GetXBRLDetailsForTaxonomyID(string clientName, int taxonomyId, string taxonomyName)
        {
            List<XBRLObjectModel> xbrlObjectModels = new List<XBRLObjectModel>();

            DataTable results = DataAccess.ExecuteDataTable(DBConnectionString.VerticalDBConnectionString(clientName, DataAccess),
                       SPGetXBRLDetailsForTaxonomyID,
                       DataAccess.CreateParameter(DBCTaxonomyID, SqlDbType.Int, taxonomyId)                       
                       );

            foreach (DataRow datarow in results.Rows)
            {
                xbrlObjectModels.Add(
                        new XBRLObjectModel()
                        {
                            TaxonomyId = taxonomyId,
                            AccessionNumber = datarow.Field<string>("Acc#"),
                            ZipFileName = datarow.Field<string>("ZipFileName"),
                            Path = datarow.Field<string>("Path"),
                            CompanyName = datarow.Field<string>("CompanyName"),
                            TaxonomyName = string.IsNullOrWhiteSpace(taxonomyName) ? datarow.Field<string>("TaxonomyName") : taxonomyName,
                            DocumentDate = datarow.Field<DateTime?>("DocDate"),
                            FilingDate = datarow.Field<DateTime>("FilingDate"),
                            OrderDate = datarow.Field<DateTime>("OrderDate"),
                            FormType = datarow.Field<string>("FormType"),
                            CreatedDate = datarow.Field<DateTime>("CreatedDate"),
                            DocumentType = datarow.Field<string>("DocumentType"),
                            IsViewerEnabled = datarow["XBRLViewerCompany"] == DBNull.Value ? false : datarow.Field<bool>("XBRLViewerCompany"),
                            IsViewerReadyForXBRL = datarow["IsXBRLViewerReady"] == DBNull.Value ? false : datarow.Field<bool>("IsXBRLViewerReady")
                        }
                    );
            }

            return xbrlObjectModels;

        }




        /// <summary>
        /// Gets the request material print requests.
        /// </summary>
        /// <param name="clientTaxonomyIDsWithDocTypeIDs">The client taxonomy i ds with document type i ds.</param>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="requestMaterialPrintRequestData">The request material print request data.</param>
        /// <returns>List&lt;RequestMaterialPrintHistory&gt;.</returns>
        public List<RequestMaterialPrintHistory> GetRequestMaterialPrintRequests(DataTable clientTaxonomyIDsWithDocTypeIDs, string clientName, List<RequestMaterialPrintHistory> requestMaterialPrintRequestData)
        {
            int previousTaxonomyID = -1;

            DataTable results = DataAccess.ExecuteDataTable(DBConnectionString.VerticalDBConnectionString(clientName, DataAccess),
                       SPGetTaxonomyAssociationHierarchy,
                       DataAccess.CreateParameter(DBCTaxonomyAssociationHierarchyDocs, SqlDbType.Structured, clientTaxonomyIDsWithDocTypeIDs)
                       );
            
            List<RequestMaterialPrintHistory> lstPrintTaxonomyAssociationData = null;
            foreach (DataRow datarow in results.Rows)
            {
                int taxonomyID = datarow.Field<Int32>("TaxonomyID");
                string taxonomyName = datarow.Field<string>("TaxonomyName");

                if (previousTaxonomyID != taxonomyID)
                {

                    lstPrintTaxonomyAssociationData = requestMaterialPrintRequestData.FindAll(p => p.TaxonomyAssociationData.TaxonomyID == taxonomyID);
                    foreach (var item in lstPrintTaxonomyAssociationData)
                    {
                        item.TaxonomyAssociationData.IsObjectinVerticalMarket = true;
                        if (!String.IsNullOrEmpty(taxonomyName))
                        {
                            item.TaxonomyAssociationData.TaxonomyName = taxonomyName;
                        }
                    }
                    
                }

                previousTaxonomyID = taxonomyID;

                foreach (var item in lstPrintTaxonomyAssociationData)
                {
                    int documentTypeId = datarow.Field<Int32>("DocumentTypeId");
                    HostedDocumentType hostedDocumentType = item.TaxonomyAssociationData.DocumentTypes.Find(t => t.DocumentTypeId == documentTypeId);
                    if (hostedDocumentType != null)
                    {
                        hostedDocumentType.IsObjectinVerticalMarket = true;
                        hostedDocumentType.VerticalMarketID = datarow.Field<string>("VerticalMarketId");
                        hostedDocumentType.ContentURI = datarow.Field<string>("ContentURI");
                    }
                }
                
            }

            requestMaterialPrintRequestData.RemoveAll(p => !p.TaxonomyAssociationData.IsObjectinVerticalMarket);
            
            requestMaterialPrintRequestData.ForEach(p => p.TaxonomyAssociationData.DocumentTypes.RemoveAll(d => !d.IsObjectinVerticalMarket));

            return requestMaterialPrintRequestData;
        }
    }

}
