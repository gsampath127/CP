
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
using RRD.FSG.RP.Model.Entities.VerticalMarket;
using RRD.FSG.RP.Model.Enumerations;
using RRD.FSG.RP.Model.Interfaces.VerticalMarket;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        /// SPGetDocumentUpdateReportData
        /// </summary>
        private const string SPGetDocumentUpdateReportData = "RPV2HostedSites_GetDocumentUpdateReportData";

        /// <summary>
        /// The sp get URL Generation
        /// </summary>
        private const string SPGetUrlGeneration = "RPV2HostedSites_GetUrlGeneration";

        /// <summary>
        /// The sp get Liquidated Merged report
        /// </summary>
        private const string SPCUSIPMergerLiqudationReportData = "RPV2HostedSites_CUSIPMergerLiqudationReportData";
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
        /// The sp get taxonomy association group docs
        /// </summary>
        private const string SPGetTaxonomyAssociationGroupDocs = "RPV2HostedSites_GetTaxonomyAssociationGroupDocs";

        /// <summary>
        /// The DBC taxonomy association link data type
        /// </summary>
        private const string DBCTaxonomyAssociationLinkDataType = "RPV2HostedSitesTaxonomyAssociationLinkDataType";
        /// <summary>
        /// The DBC taxonomy association link data type
        /// </summary>
        private const string DBCVerifyTaxonomyByMarketIdDataType = "RPV2HostedSitesVerifyTaxonomyByMarketIdDataType";
        /// <summary>
        /// The DBC taxonomy association link data type
        /// </summary>
        private const string DBCVerifyTaxonomyByTaxonomyIDAndLevelDataType = "RPV2HostedSitesVerifyTaxonomyByTaxonomyIDAndLevelDataType";
        /// <summary>
        /// StartDate
        /// </summary>
        private const string DBCStartDate = "StartDate";
        /// <summary>
        /// EndDate
        /// </summary>
        private const string DBCEndDate = "EndDate";

        /// <summary>
        /// The DBC Billing Report data type
        /// </summary>
        private const string DBCBillingReportDataType = "RPV2HostedSitesTaxonomyBillingReport";

        /// <summary>
        /// The DBC Billing Report data type
        /// </summary>
        private const string DBCUrlGenerationDataType = "RPV2HostedSitesTaxonomyUrlGeneration";

        /// <summary>
        /// The DBC TaxonomyIdMarketId Report data type
        /// </summary>
        private const string DBCTT_TaxonomyIdMarketId = "TT_TaxonomyIdMarketId";

        private const string DBCTaxonomyAssociationNameOverride = "RPV2HostedSitesTaxonomyAssociationClientDocsDataType";
        /// <summary>
        /// The DBC taxonomy association hierarchy docs
        /// </summary>
        private const string DBCTaxonomyAssociationHierarchyDocs = "RPV2HostedSitesTaxonomyAssociationHierarchyDocs";        

        /// <summary>
        /// The sp get billing report
        /// </summary>
        private const string SPVerifyTaxonomyByMarketId = "RPV2HostedSites_VerifyTaxonomyByMarketId";

        /// <summary>
        /// The DBC taxonomy identifier
        /// </summary>
        private const string DBCTaxonomyID = "TaxonomyID";
        /// <summary>
        /// The DBC FundNameFormat
        /// </summary>
        private const string DBCFundNameFormat = "FundNameFormat";



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
        /// <param name="taxonomyOrderFeatureMode">The taxonomyOrderFeatureMode.</param>
        /// <returns>List&lt;TaxonomyAssociationLinkModel&gt;.</returns>
        public List<TaxonomyAssociationLinkModel> GetTaxonomyAssociationLinksVerticalData(DataTable clientTaxonomyIDs, string clientName,
                                                                            List<TaxonomyAssociationLinkModel> taxonomyassociationlinks, FeatureEnums.FundOrder taxonomyOrderFeatureMode, FeatureEnums.FundNameFormat fundNameFormatFeature)
        {


            DataTable results = DataAccess.ExecuteDataTable(DBConnectionString.VerticalDBConnectionString(clientName,
                                                                                        DataAccess),
                        SPGetTaxonomyAssociationLinks,
                        DataAccess.CreateParameter(DBCTaxonomyAssociationLinkDataType, SqlDbType.Structured, clientTaxonomyIDs),
                        DataAccess.CreateParameter(DBCFundNameFormat, SqlDbType.VarChar, fundNameFormatFeature.ToString())
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

            switch (taxonomyOrderFeatureMode.ToString())
            {
                case "AlphabeticalAsc":
                    taxonomyassociationlinks.Sort((x, y) => x.TaxonomyAssocationName.CompareTo(y.TaxonomyAssocationName));
                    break;
                case "AlphabeticalDesc":
                    taxonomyassociationlinks.Sort((x, y) => -1 * x.TaxonomyAssocationName.CompareTo(y.TaxonomyAssocationName));
                    break;
                case "CustomizeOrder":
                    taxonomyassociationlinks.Sort((x, y) => x.FundOrder.CompareTo(y.FundOrder));
                    break;
                default:
                    taxonomyassociationlinks.Sort((x, y) => x.TaxonomyAssocationName.CompareTo(y.TaxonomyAssocationName));
                    break;
            }

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
                                                                            List<BillingReportActiveFundDetails> billingReport, DateTime startDate, DateTime endDate)
        {
            DateTime CSTEndDate = TimeZoneInfo.ConvertTimeFromUtc(endDate, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")).AddHours(-1);
            DateTime CSTStartDate = TimeZoneInfo.ConvertTimeFromUtc(startDate, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")).AddHours(-1);

            DataTable results = DataAccess.ExecuteDataTable(DBConnectionString.VerticalDBConnectionString(clientName,
                                                                                        DataAccess),
                        SPGetBillingReport,
                        DataAccess.CreateParameter(DBCBillingReportDataType, SqlDbType.Structured, marketIDs),
                        DataAccess.CreateParameter(DBCClientName, SqlDbType.NVarChar, clientName),
                        DataAccess.CreateParameter(DBCStartDate, SqlDbType.DateTime, CSTStartDate),
                        DataAccess.CreateParameter(DBCEndDate, SqlDbType.DateTime, CSTEndDate)
                        );


            string previousMarketID = "";
            List<BillingReportActiveFundDetails> lstbillingData = null;

            foreach (DataRow datarow in results.Rows)
            {

                string MarketID = datarow.Field<string>("CUSIP");                

                if (previousMarketID != MarketID)
                {
                    previousMarketID = MarketID;

                    string FundName = datarow.Field<string>("FundName");
                    string seriesID = datarow.Field<string>("SeriesID");
                    string SECFundName = datarow.Field<string>("SeriesName");

                    lstbillingData = billingReport.FindAll(p => p.MarketID == MarketID);

                    foreach (BillingReportActiveFundDetails billingData in lstbillingData)
                    {   
                        billingData.IsObjectinVerticalMarket = true;
                        if (!String.IsNullOrEmpty(FundName))
                        {
                            billingData.FundName = FundName;
                        }
                        billingData.SeriesID = seriesID;
                        billingData.SECFundName = SECFundName;
                    }
                }

                string DocumentTypeMarketId = datarow.Field<string>("DocumentTypeMarketId");
                string PDFURL = datarow.Field<string>("PDFURL");
                DateTime? DocumentUpdatedDate = datarow.Field<DateTime?>("DocumentUpdatedDate");

                foreach (BillingReportActiveFundDetails billingData in lstbillingData)
                {
                    switch (DocumentTypeMarketId)
                    {
                        case "SP":
                            billingData.IsSummaryProspectus = "Y";
                            billingData.SPPDFName = PDFURL;
                            billingData.SPDocumentUpdatedDate = DocumentUpdatedDate.HasValue? DocumentUpdatedDate.Value.ToString(): "N";
                            break;
                        case "P":
                            billingData.IsProspectus = "Y";
                            billingData.PPDFName = PDFURL;
                            billingData.PDocumentUpdatedDate = DocumentUpdatedDate.HasValue ? DocumentUpdatedDate.Value.ToString() : "N";
                            break;
                        case "S":
                            billingData.IsSAI = "Y";
                            billingData.SAIPDFName = PDFURL;
                            billingData.SAIDocumentUpdatedDate = DocumentUpdatedDate.HasValue ? DocumentUpdatedDate.Value.ToString() : "N";
                            break;
                        case "AR":
                            billingData.IsAnnualReport = "Y";
                            billingData.ARPDFName = PDFURL;
                            billingData.ARDocumentUpdatedDate = DocumentUpdatedDate.HasValue ? DocumentUpdatedDate.Value.ToString() : "N";
                            break;
                        case "SAR":
                            billingData.IsSemiAnnualReport = "Y";
                            billingData.SARPDFName = PDFURL;
                            billingData.SARDocumentUpdatedDate = DocumentUpdatedDate.HasValue ? DocumentUpdatedDate.Value.ToString() : "N";
                            break;
                        case "FS":
                            billingData.IsFactSheet = "Y";
                            billingData.FactSheetPDFName = PDFURL;
                            billingData.FSDocumentUpdatedDate = DocumentUpdatedDate.HasValue ? DocumentUpdatedDate.Value.ToString() : "N";
                            break;
                        case "PVR":
                            billingData.IsPVR = "Y";
                            billingData.PVRPDFName = PDFURL;
                            billingData.PVRDocumentUpdatedDate = DocumentUpdatedDate.HasValue ? DocumentUpdatedDate.Value.ToString() : "N";
                            break;
                        case "XBRL":
                            billingData.IsXBRL = "Y";                            
                            break;
                    }
                }
            }

            billingReport.RemoveAll(a => a.IsObjectinVerticalMarket == false);

            billingReport = billingReport.OrderBy(p => p.SiteName).ThenBy(p => p.SeriesID).ThenBy(p=>p.MarketID).ToList();

            return billingReport;
        }
        /// <summary>
        /// Gets the CUSIPMergerLiqudationReportData.
        /// </summary>
        /// <param name="marketIDs">The client market ids.</param>        
        /// <returns>List&lt;CUSIPMergerLiqudationReportObjectModel&gt;.</returns>
        public List<CUSIPMergerLiqudationReportObjectModel> GetCUSIPMergerLiqudationReportData(DataTable marketIDs)
        {
            List<CUSIPMergerLiqudationReportObjectModel> lstresults = new List<CUSIPMergerLiqudationReportObjectModel>();

            DataTable results = DataAccess.ExecuteDataTable(DBConnectionString.VerticalDBConnectionString("",
                                                                                        DataAccess),
                            SPCUSIPMergerLiqudationReportData,
                            DataAccess.CreateParameter(DBCTT_TaxonomyIdMarketId, SqlDbType.Structured, marketIDs)
                        );

                
            foreach (DataRow dr in results.Rows)
            {
                CUSIPMergerLiqudationReportObjectModel obj = new CUSIPMergerLiqudationReportObjectModel()
                {
                    MarketID = dr.Field<string>("MarketId"),
                    Status = dr.Field<string>("Status"),
                    ClientName = dr.Field<string>("ClientName")
                };

                lstresults.Add(obj);
            }
            
            return lstresults;
        }

        /// <summary>
        /// Gets Billing data from vertical db
        /// </summary>
        /// <param name="marketIDs"></param>
        /// <param name="clientName"></param>
        /// <param name="billingReport"></param>
        /// <returns></returns>
        public DataTable GetDocumentUpdateReportVerticalData(DateTime StartDate, DateTime EndDate, DataTable clientMarketIDs, string clientName)
        {
            DataTable docUpDtReportVerticalData = new DataTable();            
            docUpDtReportVerticalData.Columns.Add("MarketId", typeof(string));
            docUpDtReportVerticalData.Columns.Add("DocumentTypeID", typeof(int));
            docUpDtReportVerticalData.Columns.Add("TaxonomyName", typeof(string));
            docUpDtReportVerticalData.Columns.Add("DocumentDate", typeof(DateTime));
            docUpDtReportVerticalData.Columns.Add("DocumentUpdatedDate", typeof(DateTime));


            List<DocumentUpdateReportModel> lstDocumentUpdateReportModel = new List<DocumentUpdateReportModel>();
            DateTime CSTEndDate = TimeZoneInfo.ConvertTimeFromUtc(EndDate, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")).AddHours(-1);
            DateTime CSTStartDate = TimeZoneInfo.ConvertTimeFromUtc(StartDate, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")).AddHours(-1);

            DataTable results = DataAccess.ExecuteDataTable(DBConnectionString.VerticalDBConnectionString(clientName,
                                                                                        DataAccess),
                        SPGetDocumentUpdateReportData,
                        DataAccess.CreateParameter(DBCVerifyTaxonomyByMarketIdDataType, SqlDbType.Structured, clientMarketIDs),
                        DataAccess.CreateParameter(DBCStartDate, SqlDbType.DateTime, CSTStartDate),
                        DataAccess.CreateParameter(DBCEndDate, SqlDbType.DateTime, CSTEndDate)
                        );

            foreach (DataRow datarow in results.Rows)
            {
                docUpDtReportVerticalData.Rows.Add
                    (                        
                        datarow.Field<string>("MarketId"),
                        datarow.Field<int>("DocumentTypeID"),
                        datarow.Field<string>("TaxonomyName"),
                        datarow.Field<DateTime?>("DocumentDate"),
                        datarow.Field<DateTime?>("DocumentUpdatedDate")
                    );
            }

            return docUpDtReportVerticalData;
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
        /// <param name="taxonomyOrderFeatureMode">taxonomyOrderFeatureMode.</param>
        /// <returns>TaxonomyAssociationHierarchyModel.</returns>
        public TaxonomyAssociationHierarchyModel GetTaxonomyAssociationHierarchyDocumentsVerticalData(DataTable clientTaxonomyIDs, string clientName,
                                                            TaxonomyAssociationHierarchyModel taxonomyAssociationHierarchyModel, FeatureEnums.FundOrder taxonomyOrderFeatureMode, FeatureEnums.FundNameFormat fundNameFormatFeature)
        {

            int previousTaxonomyID = -1;
            bool isParent = false;

            DataTable results = DataAccess.ExecuteDataTable(DBConnectionString.VerticalDBConnectionString(clientName, DataAccess),
                       SPGetTaxonomyAssociationHierarchy,
                       DataAccess.CreateParameter(DBCTaxonomyAssociationHierarchyDocs, SqlDbType.Structured, clientTaxonomyIDs),
                       DataAccess.CreateParameter(DBCFundNameFormat, SqlDbType.VarChar, fundNameFormatFeature.ToString())
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

            RemoveInvalidTaxonomyAssociationHierarchyDocumentData(taxonomyAssociationHierarchyModel, taxonomyOrderFeatureMode);

            return taxonomyAssociationHierarchyModel;
        }

        /// <summary>
        /// Removes the invalid taxonomy association hierarchy document data.
        /// </summary>
        /// <param name="taxonomyAssociationHierarchyModel">The taxonomy association hierarchy model.</param>
        private void RemoveInvalidTaxonomyAssociationHierarchyDocumentData(TaxonomyAssociationHierarchyModel taxonomyAssociationHierarchyModel, FeatureEnums.FundOrder taxonomyOrderFeatureMode)
        {
            taxonomyAssociationHierarchyModel.ParentTaxonomyAssociationData.RemoveAll(p => !p.IsObjectinVerticalMarket);
            taxonomyAssociationHierarchyModel.ParentTaxonomyAssociationData.ForEach(p => p.DocumentTypes.RemoveAll(d => !d.IsObjectinVerticalMarket));
            
            taxonomyAssociationHierarchyModel.ChildTaxonomyAssociationData.RemoveAll(p => !p.IsObjectinVerticalMarket);
            taxonomyAssociationHierarchyModel.ChildTaxonomyAssociationData.ForEach(p => p.DocumentTypes.RemoveAll(d => !d.IsObjectinVerticalMarket));


            taxonomyAssociationHierarchyModel.FootNotes.RemoveAll(p => !p.IsObjectinVerticalMarket);

            switch (taxonomyOrderFeatureMode.ToString())
            {
                case "AlphabeticalAsc":
                    taxonomyAssociationHierarchyModel.ParentTaxonomyAssociationData.Sort((x, y) => x.TaxonomyName.CompareTo(y.TaxonomyName));
                    taxonomyAssociationHierarchyModel.ChildTaxonomyAssociationData.Sort((x, y) => x.TaxonomyName.CompareTo(y.TaxonomyName));
                    taxonomyAssociationHierarchyModel.FootNotes.Sort((x, y) => x.TaxonomyName.CompareTo(y.TaxonomyName));
                    break;
                case "AlphabeticalDesc":                    
                    taxonomyAssociationHierarchyModel.ParentTaxonomyAssociationData.Sort((x, y) => -1 * x.TaxonomyName.CompareTo(y.TaxonomyName));
                    taxonomyAssociationHierarchyModel.ChildTaxonomyAssociationData.Sort((x, y) => -1 * x.TaxonomyName.CompareTo(y.TaxonomyName));
                    taxonomyAssociationHierarchyModel.FootNotes.Sort((x, y) => -1 * x.TaxonomyName.CompareTo(y.TaxonomyName));
                    break;
                case "CustomizeOrder":
                    taxonomyAssociationHierarchyModel.ParentTaxonomyAssociationData.Sort((x, y) => x.FundOrder.CompareTo(y.FundOrder));
                    taxonomyAssociationHierarchyModel.ChildTaxonomyAssociationData.Sort((x, y) => x.FundOrder.CompareTo(y.FundOrder));
                    taxonomyAssociationHierarchyModel.FootNotes.Sort((x, y) => x.Order.CompareTo(y.Order));
                    break;
                default:
                    taxonomyAssociationHierarchyModel.ParentTaxonomyAssociationData.Sort((x, y) => x.TaxonomyName.CompareTo(y.TaxonomyName));
                    taxonomyAssociationHierarchyModel.ChildTaxonomyAssociationData.Sort((x, y) => x.TaxonomyName.CompareTo(y.TaxonomyName));
                    taxonomyAssociationHierarchyModel.FootNotes.Sort((x, y) => x.TaxonomyName.CompareTo(y.TaxonomyName));
                    break;
            }

            
            taxonomyAssociationHierarchyModel.ParentHeaders.Sort((x, y) => x.Order.CompareTo(y.Order));
            taxonomyAssociationHierarchyModel.ChildHeaders.Sort((x, y) => x.Order.CompareTo(y.Order));
        }


        /// <summary>
        /// Gets the taxonomy association documents vertical data.
        /// </summary>
        /// <param name="clientTaxonomyIDs">The client taxonomy i ds.</param>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="taxonomyAssociationDocumentsModel">The taxonomy association documents model.</param>
        /// <param name="taxonomyOrderFeatureMode">taxonomyOrderFeatureMode.</param>
        /// <returns>TaxonomyAssociationDocumentsModel.</returns>
        public TaxonomyAssociationDocumentsModel GetTaxonomyAssociationDocumentsVerticalData(DataTable clientTaxonomyIDs, string clientName,
                                            TaxonomyAssociationDocumentsModel taxonomyAssociationDocumentsModel, FeatureEnums.FundOrder taxonomyOrderFeatureMode)
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

            taxonomyAssociationDocumentsModel.FootNotes.RemoveAll(p => !p.IsObjectinVerticalMarket);

            switch (taxonomyOrderFeatureMode.ToString())
            {
                case "AlphabeticalAsc":
                    taxonomyAssociationDocumentsModel.TaxonomyAssociationDocumentsData.Sort((x, y) => x.TaxonomyName.CompareTo(y.TaxonomyName));
                    taxonomyAssociationDocumentsModel.FootNotes.Sort((x, y) => x.TaxonomyName.CompareTo(y.TaxonomyName));
                    break;
                case "AlphabeticalDesc":
                    taxonomyAssociationDocumentsModel.TaxonomyAssociationDocumentsData.Sort((x, y) => -1 * x.TaxonomyName.CompareTo(y.TaxonomyName));
                    taxonomyAssociationDocumentsModel.FootNotes.Sort((x, y) => -1 * x.TaxonomyName.CompareTo(y.TaxonomyName));
                    break;
                case "CustomizeOrder":
                    taxonomyAssociationDocumentsModel.TaxonomyAssociationDocumentsData.Sort((x, y) => x.FundOrder.CompareTo(y.FundOrder));
                    taxonomyAssociationDocumentsModel.FootNotes.Sort((x, y) => x.Order.CompareTo(y.Order));
                    break;
                default:
                    taxonomyAssociationDocumentsModel.TaxonomyAssociationDocumentsData.Sort((x, y) => x.TaxonomyName.CompareTo(y.TaxonomyName));
                    taxonomyAssociationDocumentsModel.FootNotes.Sort((x, y) => x.TaxonomyName.CompareTo(y.TaxonomyName));
                    break;
            }

            

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
        public TaxonomyAssociationData GetTaxonomySpecificDocumentsVerticalData(DataTable clientTaxonomyIDs, string clientName, TaxonomyAssociationData taxonomyAssociationData, FeatureEnums.FundNameFormat fundNameFormatFeature)
        {
            int previousTaxonomyID = -1;

            DataTable results = DataAccess.ExecuteDataTable(DBConnectionString.VerticalDBConnectionString(clientName, DataAccess),
                       SPGetTaxonomySpecificDocumentFrame,
                       DataAccess.CreateParameter(DBCTaxonomyAssociationHierarchyDocs, SqlDbType.Structured, clientTaxonomyIDs),
                       DataAccess.CreateParameter(DBCClientName, SqlDbType.VarChar, clientName),
                       DataAccess.CreateParameter(DBCFundNameFormat, SqlDbType.VarChar, fundNameFormatFeature.ToString())
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
                    hostedDocumentType.ContentURI = hostedDocumentType.ContentURI.Replace("http://www.rightprospectus.com/", ConfigurationManager.AppSettings["DocumentHostURL"].ToString());
                    hostedDocumentType.VerticalMarketID = datarow.Field<string>("VerticalMarketId");
                    hostedDocumentType.DocumentDate = datarow.Field<DateTime?>("DocumentDate");
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
        /// Gets TaxonomyAssociationClientDocumentsVerticalData
        /// </summary>
        /// <param name="clientTaxonomyIDs">The client taxonomy i ds.</param>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="taxonomyAssociationData">taxonomyAssociationData</param>
        /// <returns>taxonomyAssociationData</returns>
        public TaxonomyAssociationData GetTaxonomyAssociationClientDocumentsVerticalData(DataTable clientTaxonomyIDs, string clientName,
                                                                                                TaxonomyAssociationData taxonomyAssociationData)
        {


            DataTable results = DataAccess.ExecuteDataTable(DBConnectionString.VerticalDBConnectionString(clientName,
                                                                                        DataAccess),
                        SPVerifyTaxonomyByMarketId,
                        DataAccess.CreateParameter(DBCVerifyTaxonomyByMarketIdDataType, SqlDbType.Structured, clientTaxonomyIDs)
                        );


            foreach (DataRow datarow in results.Rows)
            {
                string TaxonomyName = datarow.Field<string>("TaxonomyName");

                taxonomyAssociationData.SeriesID = datarow.Field<string>("SeriesID");

                taxonomyAssociationData.IsObjectinVerticalMarket = true;
                if (!String.IsNullOrEmpty(TaxonomyName))
                {
                    taxonomyAssociationData.TaxonomyName = TaxonomyName;
                }

            }

            if (!taxonomyAssociationData.IsObjectinVerticalMarket)
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

        /// <summary>
        /// Gets the taxonomy association group documents vertical data.
        /// </summary>
        /// <param name="clientTaxonomyIDs">The client taxonomy i ds.</param>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="taxonomyAssociationHierarchyModel">The taxonomy association group model.</param>
        /// <param name="taxonomyOrderFeatureMode">TtaxonomyOrderFeatureMode.</param>
        /// <returns>TaxonomyAssociationGroupModel.</returns>
        public TaxonomyAssociationGroupModel GetTaxonomyAssociationGroupDocumentsVerticalData(DataTable clientTaxonomyIDs, string clientName,
                                            TaxonomyAssociationGroupModel taxonomyAssociationGroupModel, FeatureEnums.FundOrder taxonomyOrderFeatureMode)
        {

            int previousTaxonomyID = -1;

            DataTable results = DataAccess.ExecuteDataTable(DBConnectionString.VerticalDBConnectionString(clientName, DataAccess),
                       SPGetTaxonomyAssociationHierarchy,
                       DataAccess.CreateParameter(DBCTaxonomyAssociationHierarchyDocs, SqlDbType.Structured, clientTaxonomyIDs)
                       );

            List<TaxonomyAssociationGroupTaxonomyAssociationData> TAGTAData = null;
            foreach (DataRow datarow in results.Rows)
            {
                int taxonomyID = datarow.Field<Int32>("TaxonomyID");
                string taxonomyName = datarow.Field<string>("TaxonomyName");
                string verticalMarketId = datarow.Field<string>("VerticalMarketId");
                DateTime? documentDate = datarow.Field<DateTime?>("DocumentDate");

                if (previousTaxonomyID != taxonomyID)
                {
                    TAGTAData = taxonomyAssociationGroupModel.TAGTAData.FindAll(x => x.TaxonomyAssociationData!= null && x.TaxonomyAssociationData.Exists(y => y.TaxonomyID == taxonomyID));
                    foreach (var item in TAGTAData)
                    {
                        item.TaxonomyAssociationData.FindAll(t => t.TaxonomyID == taxonomyID).ForEach(x =>
                        {
                            x.IsObjectinVerticalMarket = true;
                            if (!String.IsNullOrEmpty(taxonomyName))
                            {
                                x.TaxonomyName = taxonomyName;
                            }                            
                        });
                    }

                    taxonomyAssociationGroupModel.FootNotes.FindAll(x => x.TaxonomyID == taxonomyID).ForEach(x =>
                    {
                        x.IsObjectinVerticalMarket = true;
                        if (!String.IsNullOrEmpty(taxonomyName))
                        {
                            x.TaxonomyName = taxonomyName;
                        }
                    });

                    previousTaxonomyID = taxonomyID;
                }

                int documentTypeId = datarow.Field<Int32>("DocumentTypeId");

                foreach (var item in TAGTAData)
                {
                    item.TaxonomyAssociationData.FindAll(t => t.TaxonomyID == taxonomyID).ForEach(x =>
                    {
                        HostedDocumentType hostedDocumentType = x.DocumentTypes.Find(t => t.DocumentTypeId == documentTypeId);

                        if (hostedDocumentType != null)
                        {
                            hostedDocumentType.IsObjectinVerticalMarket = true;
                            hostedDocumentType.VerticalMarketID = datarow.Field<string>("VerticalMarketId");
                            taxonomyAssociationGroupModel.DocumentTypeHeaders.Find(d => d.DocumentTypeId == documentTypeId).VerticalMarketID = hostedDocumentType.VerticalMarketID;
                            hostedDocumentType.DocumentDate = documentDate;
                        }
                    });
                }

            }

            RemoveInvalidTaxonomyAssociationGroupDocumentData(taxonomyAssociationGroupModel, taxonomyOrderFeatureMode);

            return taxonomyAssociationGroupModel;
        }

        /// <summary>
        /// Removes the invalid taxonomy association hierarchy document data.
        /// </summary>
        /// <param name="taxonomyAssociationHierarchyModel">The taxonomy association hierarchy model.</param>
        private void RemoveInvalidTaxonomyAssociationGroupDocumentData(TaxonomyAssociationGroupModel taxonomyAssociationGroupModel, FeatureEnums.FundOrder taxonomyOrderFeatureMode)
        {
            taxonomyAssociationGroupModel.FootNotes.RemoveAll(p => !p.IsObjectinVerticalMarket);

            taxonomyAssociationGroupModel.DocumentTypeHeaders.Sort((x, y) => x.Order.CompareTo(y.Order));
            switch (taxonomyOrderFeatureMode.ToString())
            {
                case "AlphabeticalAsc":
                    taxonomyAssociationGroupModel.FootNotes.Sort((x, y) => x.TaxonomyName.CompareTo(y.TaxonomyName));
                    break;
                case "AlphabeticalDesc":
                    taxonomyAssociationGroupModel.FootNotes.Sort((x, y) => -1 * x.TaxonomyName.CompareTo(y.TaxonomyName));
                    break;
                case "CustomizeOrder":
                    taxonomyAssociationGroupModel.FootNotes.Sort((x, y) => x.Order.CompareTo(y.Order));
                    break;
                default:
                    taxonomyAssociationGroupModel.FootNotes.Sort((x, y) => x.TaxonomyName.CompareTo(y.TaxonomyName));
                    break;
            }

            taxonomyAssociationGroupModel.TAGTAData.ForEach(tag =>
            {
                if (tag.TaxonomyAssociationData != null)
                {
                    tag.TaxonomyAssociationData.RemoveAll(p => !p.IsObjectinVerticalMarket);

                    tag.TaxonomyAssociationData.ForEach(p =>
                    {
                        p.DocumentTypes.RemoveAll(d => !d.IsObjectinVerticalMarket);
                    });

                    switch (taxonomyOrderFeatureMode.ToString())
                    {
                        case "AlphabeticalAsc":
                            tag.TaxonomyAssociationData.Sort((x, y) => x.TaxonomyName.CompareTo(y.TaxonomyName));
                            break;
                        case "AlphabeticalDesc":
                            tag.TaxonomyAssociationData.Sort((x, y) => -1 * x.TaxonomyName.CompareTo(y.TaxonomyName));
                            break;
                        case "CustomizeOrder":
                            tag.TaxonomyAssociationData.Sort((x, y) => x.FundOrder.CompareTo(y.FundOrder));
                            break;
                        default:
                            tag.TaxonomyAssociationData.Sort((x, y) => x.TaxonomyName.CompareTo(y.TaxonomyName));
                            break;
                    }
                }
            });
        }

    }
}
