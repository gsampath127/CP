using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.HostedPages;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Enumerations;
using RRD.FSG.RP.Model.Interfaces.HostedPages;
using RRD.FSG.RP.Model.Interfaces.System;
using RRD.FSG.RP.Model.Interfaces.VerticalMarket;
using RRD.FSG.RP.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace RRD.FSG.RP.Model.Factories.HostedPages
{
    /// <summary>
    /// Class HostedClientPageScenariosFactory.
    /// </summary>
    public class HostedClientPageScenariosFactory : HostedPageBaseFactory, IHostedClientPageScenariosFactory
    {

        /// <summary>
        /// The sp get billing report data
        /// </summary>
        private const string SPGetBillingData = "RPV2HostedSites_BillingReport";

        /// <summary>
        /// The sp get removed cusip's 
        /// </summary>
        private const string SPGetRemovedCUSIPData = "RPV2HostedAdmin_RemovedCUSIPs";


        /// <summary>
        /// Changes
        /// The sp get taxonomy association links
        /// </summary>
        private const string SPGetTaxonomyAssociationUrlGeneration = "RPV2HostedSites_URLGeneration";
        /// <summary>
        /// The sp get taxonomy association links
        /// </summary>
        private const string SPGetTaxonomyAssociationLinks = "RPV2HostedSites_GetTaxonomyAssociationLinks";
        /// <summary>
        /// The sp get taxonomy association hierarchy documents
        /// </summary>
        private const string SPGetTaxonomyAssociationHierarchyDocuments = "RPV2HostedSites_GetTaxonomyAssociationHierarchy";
        /// <summary>
        /// The sp get taxonomy association documents
        /// </summary>
        private const string SPGetTaxonomyAssociationDocuments = "RPV2HostedSites_GetTaxonomyAssociationDocuments";
        /// <summary>
        /// The sp get taxonomy specific document frame
        /// </summary>
        private const string SPGetTaxonomySpecificDocumentFrame = "RPV2HostedSites_GetTaxonomySpecificDocumentFrame";
        /// <summary>
        /// The sp get clients site data
        /// </summary>
        private const string SPGetClientsSiteData = "RPV2HostedSites_GetClientsSiteData";


        /// <summary>
        /// The sp get taxonomy identifier by taxonomy association identifier or external identifier
        /// </summary>
        private const string SPGetTaxonomyIDByTaxonomyAssociationIDOrExternalID = "RPV2HostedSites_GetTaxonomyIDByTaxonomyAssociationIDOrExternalID";

        /// <summary>
        /// The sp clients site data_ cache dependency check
        /// </summary>
        private const string SPClientsSiteData_CacheDependencyCheck = "RPV2HostedSites_ClientsSiteData_CacheDependencyCheck";


        /// <summary>
        /// The DCB Start Date for removed CUSIP
        /// </summary>
        private const string DCBStartDate = "StartDate";

        /// <summary>
        /// The DCB End Date for removed CUSIP
        /// </summary>
        private const string DCBEndDate = "EndDate";

        /// <summary>
        /// The DCB added fund count
        /// </summary>
        private const string DCBNewFundCount = "NewFundCount";

        /// <summary>
        /// The DCB removed fund count
        /// </summary>
        private const string DCBRemovedFundCount = "RemovedFundCount";


        /// <summary>
        /// The DBC external identifier
        /// </summary>
        private const string DBCExternalID = "ExternalID";

        /// <summary>
        /// The DBC external identifier
        /// </summary>
        private const string DBCSearchSiteName = "SearchSiteName";

        /// <summary>
        /// The DBC external identifier
        /// </summary>
        private const string DBCMarketID = "MarketID";
        /// <summary>
        /// The DBC level
        /// </summary>
        private const string DBCLevel = "Level";

        /// <summary>
        /// The dbctaid
        /// </summary>
        private const string DBCTAID = "TAID";

        /// <summary>
        /// The DBC internal ptaid
        /// </summary>
        private const string DBCInternalPTAID = "InternalPTAID";

        /// <summary>
        /// The DBC site name
        /// </summary>
        private const string DBCSiteName = "SiteName";

        /// <summary>
        /// The i hosted vertical page scenarios
        /// </summary>
        private IHostedVerticalPageScenariosFactory iHostedVerticalPageScenarios;

        /// <summary>
        /// The i system common factory
        /// </summary>
        private ISystemCommonFactory iSystemCommonFactory;


        /// <summary>
        /// Initializes a new instance of the <see cref="HostedClientPageScenariosFactory" /> class.
        /// </summary>
        /// <param name="paramDataAccess">The parameter data access.</param>
        /// <param name="paramHostedVerticalPageScenarios">The parameter hosted vertical page scenarios.</param>
        /// <param name="paramSystemCommonFactory">The parameter system common factory.</param>
        public HostedClientPageScenariosFactory(IDataAccess paramDataAccess,
                    IHostedVerticalPageScenariosFactory paramHostedVerticalPageScenarios,
            ISystemCommonFactory paramSystemCommonFactory)
            : base(paramDataAccess)
        {
            iHostedVerticalPageScenarios = paramHostedVerticalPageScenarios;
            iSystemCommonFactory = paramSystemCommonFactory;
        }

        # region "Private Data Retrieval Methods"

        /// <summary>
        /// Gets the clients site data.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <returns>ClientSiteData.</returns>
        private ClientSiteData GetClientsSiteData(string clientName)
        {
            ClientSiteData clientSiteData = new ClientSiteData();

            DataSet results = DataAccess.ExecuteDataSet(DBConnectionString.HostedConnectionString(clientName, DataAccess),
                         SPGetClientsSiteData
                         );

            if (results != null && results.Tables.Count > 0)
            {

                List<HostedSiteText> siteTexts = new List<HostedSiteText>();
                foreach (DataRow datarow in results.Tables[0].Rows)
                {
                    siteTexts.Add(new HostedSiteText()
                    {
                        IsCurrentProductionVersion = datarow.Field<int>("IsCurrentProductionVersion") == 1,
                        ResourceKey = datarow.Field<string>("ResourceKey"),
                        Text = datarow.Field<string>("Text"),
                        SiteID = datarow.Field<int>("SiteID")
                    });
                }

                clientSiteData.SiteTexts = siteTexts;



                List<HostedPageText> pageTexts = new List<HostedPageText>();
                foreach (DataRow datarow in results.Tables[1].Rows)
                {
                    pageTexts.Add(new HostedPageText()
                    {
                        IsCurrentProductionVersion = datarow.Field<int>("IsCurrentProductionVersion") == 1,
                        ResourceKey = datarow.Field<string>("ResourceKey"),
                        Text = datarow.Field<string>("Text"),
                        SiteID = datarow.Field<int>("SiteID"),
                        PageID = datarow.Field<int>("PageID")
                    });
                }

                clientSiteData.PageTexts = pageTexts;

                List<HostedSite> sites = new List<HostedSite>();
                foreach (DataRow datarow in results.Tables[2].Rows)
                {
                    sites.Add(new HostedSite()
                    {
                        SiteId = datarow.Field<int>("SiteID"),
                        SiteName = datarow.Field<string>("SiteName"),
                        DefaultPageId = datarow.Field<int>("DefaultPageId"),
                        TemplateId = datarow.Field<int>("TemplateId"),
                        IsDefaultSite = datarow.Field<int>("IsDefaultSite") == 1
                    });
                }

                clientSiteData.Sites = sites;

                List<HostedStaticResource> staticResources = new List<HostedStaticResource>();
                foreach (DataRow datarow in results.Tables[3].Rows)
                {
                    staticResources.Add(new HostedStaticResource()
                        {
                            FileName = datarow.Field<string>("FileName"),
                            Size = datarow.Field<int>("Size"),
                        MimeType = datarow.Field<string>("MimeType"),
                            Data = datarow.Field<byte[]>("Data"),
                            UtcModifiedDate = datarow.Field<DateTime>("UtcModifiedDate")
                        });
                }

                clientSiteData.StaticResources = staticResources;

                List<HostedSiteNavigation> siteNavigation = new List<HostedSiteNavigation>();
                foreach (DataRow datarow in results.Tables[4].Rows)
                {
                    siteNavigation.Add(new HostedSiteNavigation()
                    {
                        SiteID = datarow.Field<int>("SiteID"),
                        NavigationKey = datarow.Field<string>("NavigationKey"),
                        NavigationXml = datarow.Field<string>("NavigationXml"),
                        IsCurrentProductionVersion = datarow.Field<int>("IsCurrentProductionVersion") == 1
                    });
                }

                clientSiteData.SiteNavigations = siteNavigation;

                List<HostedPageNavigation> pageNavigation = new List<HostedPageNavigation>();
                foreach (DataRow datarow in results.Tables[5].Rows)
                {
                    pageNavigation.Add(new HostedPageNavigation()
                    {
                        SiteID = datarow.Field<int>("SiteID"),
                        PageID = datarow.Field<int>("PageId"),
                        NavigationKey = datarow.Field<string>("NavigationKey"),
                        NavigationXml = datarow.Field<string>("NavigationXml"),
                        IsCurrentProductionVersion = datarow.Field<int>("IsCurrentProductionVersion") == 1
                    });
                }

                clientSiteData.PageNavigations = pageNavigation;

                List<HostedSiteFeature> hostedSiteFeature = new List<HostedSiteFeature>();
                foreach (DataRow datarow in results.Tables[6].Rows)
                {
                    hostedSiteFeature.Add(new HostedSiteFeature()
                    {
                        SiteID = datarow.Field<int>("SiteID"),
                        FeatureKey = datarow.Field<string>("Key"),
                        FeatureMode = datarow.Field<int>("FeatureMode"),
                    });
                }
                clientSiteData.SiteFeatures = hostedSiteFeature;

                List<HostedPageFeature> hostedPageFeature = new List<HostedPageFeature>();
                foreach (DataRow datarow in results.Tables[7].Rows)
                {
                    hostedPageFeature.Add(new HostedPageFeature()
                    {
                        SiteID = datarow.Field<int>("SiteID"),
                        PageID = datarow.Field<int>("PageId"),
                        FeatureKey = datarow.Field<string>("Key"),
                        FeatureMode = datarow.Field<int>("FeatureMode"),
                    });
                }
                clientSiteData.PageFeatures = hostedPageFeature;

            }

            return clientSiteData;
        }

        /// <summary>
        /// Gets the clients site data from cache.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <returns>ClientSiteData.</returns>
        private ClientSiteData GetClientsSiteDataFromCache(string clientName)
        {
            bool isReturnedFromCache = false;

            return DependencyHelper.GetCachedDataWithSqlDependency(clientName + HostedCacheItems.ClientsSiteData,
                            DBConnectionString.HostedConnectionString(clientName, DataAccess),
                            SPClientsSiteData_CacheDependencyCheck,
                            ConfigValues.ClientDBCacheTimeOut, () => GetClientsSiteData(clientName),
                            out isReturnedFromCache);

        }

        /// <summary>
        /// Gets the taxonomy association links client data.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <param name="verticalIds">The vertical ids.</param>
        /// <returns>List&lt;TaxonomyAssociationLinkModel&gt;.</returns>
        private List<TaxonomyAssociationLinkModel> GetTaxonomyAssociationLinksClientData(string clientName,
                                                                                string siteName,
                                                                                 DataTable verticalIds)
        {
            List<TaxonomyAssociationLinkModel> taxonomyassociationlinks = new List<TaxonomyAssociationLinkModel>();
            DataTable results = DataAccess.ExecuteDataTable(DBConnectionString.HostedConnectionString(clientName, DataAccess),
                         SPGetTaxonomyAssociationLinks,
                         DataAccess.CreateParameter(DBCSiteName, DbType.String, siteName)
                         );

            foreach (DataRow datarow in results.Rows)
            {
                int taxonomyID = datarow.Field<int>("TaxonomyID");
                string nameOverride = datarow.Field<string>("NameOverride");
                verticalIds.Rows.Add(taxonomyID, !string.IsNullOrWhiteSpace(nameOverride));
                TaxonomyAssociationLinkModel taxonomyassociationlink = new TaxonomyAssociationLinkModel();
                taxonomyassociationlink.TaxonomyID = taxonomyID;
                taxonomyassociationlink.ParentTaxonomyAssociaitonID = datarow.Field<int>("ParentTaxonomyAssociationID");
                taxonomyassociationlink.TaxonomyAssocationName = nameOverride;
                //taxonomyassociationlink.ExternalID = datarow.Field<string>("ExternalID");
                taxonomyassociationlink.TaxonomyDescriptionOverride = datarow.Field<string>("DescriptionOverride");
                taxonomyassociationlinks.Add(taxonomyassociationlink);
            }
            //call vertical db with database
            return taxonomyassociationlinks;
        }

        //mine
        private List<UrlGenerationObjectModel> GetTaxonomyAssociationforUrlGenerationClientData(string clientName, string marketID, string siteName, DataTable verticalIds)
        {

            List<UrlGenerationObjectModel> lstUrlGeneratedData = new List<UrlGenerationObjectModel>();


            DataTable results = new DataTable();
            results = DataAccess.ExecuteDataTable(DBConnectionString.HostedConnectionString(clientName, DataAccess),
                         SPGetTaxonomyAssociationUrlGeneration,
                         DataAccess.CreateParameter(DBCSearchSiteName, DbType.String, string.IsNullOrWhiteSpace(siteName) ? null : siteName),
                         DataAccess.CreateParameter(DBCMarketID, DbType.String, string.IsNullOrWhiteSpace(marketID) ? null : marketID)
                         );

            string previousSiteName = string.Empty;
            bool isShowXBRLInNewTab = false;
            
            foreach (DataRow datarow in results.Rows)
            {
                string SiteName = datarow.Field<string>("SiteName");
                string FundName = datarow.Field<string>("FundName");
                int TaxonomyId = datarow.Field<int>("TaxonomyId");
                string TAExternalID = datarow.Field<string>("TAExternalID");
                string DTExternalID = datarow.Field<string>("DTExternalID");
                
                if (previousSiteName != SiteName)
                {                    
                    isShowXBRLInNewTab = ((FeatureEnums.XBRL)datarow.Field<int>("XBRLFeatureMode")).HasFlag(FeatureEnums.XBRL.ShowXBRLInNewTab);

                    previousSiteName = SiteName;
                }

                string PublicUrl = ConfigurationManager.AppSettings["HostedEngineURL"] + clientName + "/TADF/" + TAExternalID + "/" + DTExternalID + "?site=" + SiteName;

                if (isShowXBRLInNewTab && string.Equals(DTExternalID, "XBRL", StringComparison.OrdinalIgnoreCase))
                {
                    PublicUrl = ConfigurationManager.AppSettings["HostedEngineURL"] + clientName + "/XBRL/" + TAExternalID + "?site=" + SiteName;
                }

                string PrivateUrl = isShowXBRLInNewTab && string.Equals(DTExternalID, "XBRL", StringComparison.OrdinalIgnoreCase) ? "" : PublicUrl + "&SPV=true";

                verticalIds.Rows.Add(TaxonomyId, !string.IsNullOrWhiteSpace(FundName));
                UrlGenerationObjectModel objUrlGeneration = new UrlGenerationObjectModel();
                //objUrlGeneration.MarketID = MarketID;
                objUrlGeneration.TaxonomyId = TaxonomyId;
                objUrlGeneration.TLEExternalID = TAExternalID;
                objUrlGeneration.FundName = FundName;
                objUrlGeneration.SiteName = SiteName;
                objUrlGeneration.PublicUrl = PublicUrl;
                objUrlGeneration.PrivateUrl = PrivateUrl;
                objUrlGeneration.DocumentType = datarow.Field<string>("DocumentType").Replace("newline", " ");
                objUrlGeneration.DocumentTypeOrder = datarow.Field<int>("DocumentTypeOrder");
                lstUrlGeneratedData.Add(objUrlGeneration);

            }
            return lstUrlGeneratedData;
        }        

        /// <summary>
        /// Gets the taxonomy association hierarchy documents client data.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <param name="externalID">The external identifier.</param>
        /// <param name="internalPTAID">The internal ptaid.</param>
        /// <param name="verticalIds">The vertical ids.</param>
        /// <returns>TaxonomyAssociationHierarchyModel.</returns>
        private TaxonomyAssociationHierarchyModel GetTaxonomyAssociationHierarchyDocumentsClientData(string clientName, string siteName,
                                        string externalID, int? internalPTAID, DataTable verticalIds)
        {
            TaxonomyAssociationHierarchyModel taxonomyAssociationHierarchyModel = new TaxonomyAssociationHierarchyModel();
            taxonomyAssociationHierarchyModel.ParentTaxonomyAssociationData = new List<TaxonomyAssociationData>();
            taxonomyAssociationHierarchyModel.ChildTaxonomyAssociationData = new List<TaxonomyAssociationData>();
            taxonomyAssociationHierarchyModel.FootNotes = new List<HostedSiteFootNotes>();
            taxonomyAssociationHierarchyModel.ParentHeaders = new List<HostedDocumentTypeHeader>();
            taxonomyAssociationHierarchyModel.ChildHeaders = new List<HostedDocumentTypeHeader>();
            int previousTaxonomyID = -1;            

            DataTable results = DataAccess.ExecuteDataTable(DBConnectionString.HostedConnectionString(clientName, DataAccess),
                         SPGetTaxonomyAssociationHierarchyDocuments,
                         !string.IsNullOrWhiteSpace(externalID) ? DataAccess.CreateParameter(DBCExternalID, DbType.String, externalID) :
                                                                  DataAccess.CreateParameter(DBCInternalPTAID, DbType.Int32, internalPTAID),
                          DataAccess.CreateParameter(DBCSiteName, DbType.String, siteName)
                         );

            TaxonomyAssociationData taxonomyAssociationDocumentModel = null;            
            bool IsParent = false;

            bool taxonomyNameOverride = false;

            foreach (DataRow datarow in results.Rows)
            {

                int taxonomyID = datarow.Field<int>("TaxonomyID");
                IsParent = datarow.Field<int>("IsParent") == 1;


                if (previousTaxonomyID != taxonomyID)
                {
                    taxonomyAssociationDocumentModel = new TaxonomyAssociationData();
                    if (IsParent)
                    {
                        taxonomyAssociationHierarchyModel.ParentTaxonomyAssociationData.Add(taxonomyAssociationDocumentModel);
                    }
                    else
                    {
                        taxonomyAssociationHierarchyModel.ChildTaxonomyAssociationData.Add(taxonomyAssociationDocumentModel);
                    }                    

                    string FootNote = datarow.Field<string>("FootnoteText");
                    string taxonomyName = datarow.Field<string>("NameOverride");

                    if (!string.IsNullOrWhiteSpace(FootNote))
                    {

                        taxonomyAssociationHierarchyModel.FootNotes.Add(new HostedSiteFootNotes()
                        {
                            Text = FootNote,
                            Order = datarow.Field<int>("FootnoteOrder"),
                            TaxonomyID = taxonomyID,
                            TaxonomyName = taxonomyName
                        });
                    }

                    previousTaxonomyID = taxonomyID;

                    taxonomyAssociationDocumentModel.DocumentTypes = new List<HostedDocumentType>();

                    

                    taxonomyAssociationDocumentModel.TaxonomyID = taxonomyID;

                    taxonomyAssociationDocumentModel.TaxonomyName = taxonomyName;

                    taxonomyAssociationDocumentModel.TaxonomyAssociationID = datarow.Field<int>("TaxonomyAssociationID");

                    taxonomyAssociationDocumentModel.TaxonomyDescriptionOverride = datarow.Field<string>("DescriptionOverride");

                    taxonomyAssociationDocumentModel.TaxonomyCssClass = datarow.Field<string>("TaxonomyCssClass");

                    taxonomyNameOverride = !string.IsNullOrWhiteSpace(taxonomyName);

                }

                int documentTypeId = datarow.Field<int>("DocumentTypeId");
              

                string documentTypeHeaderText = datarow.Field<string>("DocumentTypeHeaderText");
                int documentTypeOrder = datarow.Field<int>("DocumentTypeOrder");
                string documentTypeMarketId = datarow.Field<string>("DocumentTypeMarketId");
                string documentTypeLinkText = datarow.Field<string>("DocumentTypeLinkText");

                List<HostedDocumentTypeHeader> currentheader = null;

                if (IsParent)
                {
                    currentheader = taxonomyAssociationHierarchyModel.ParentHeaders;
                }
                else
                {
                    currentheader = taxonomyAssociationHierarchyModel.ChildHeaders;

                }

                if (currentheader.Find(f => f.HeaderName == documentTypeHeaderText) == null)
                {
                    currentheader.Add(new HostedDocumentTypeHeader()
                    {
                        HeaderName = documentTypeHeaderText,
                        Order = documentTypeOrder,
                        DocumentTypeId = documentTypeId,
                        VerticalMarketID = documentTypeMarketId

                    });
                }

                taxonomyAssociationDocumentModel.DocumentTypes.Add(new HostedDocumentType
                {
                    DocumentTypeLinkText = documentTypeLinkText,
                    DocumentTypeDescriptionOverride = datarow.Field<string>("DocumentTypeDescriptionOverride"),
                    DocumentTypeCssClass = datarow.Field<string>("DocumentTypeCssClass"),
                    DocumentTypeOrder = documentTypeOrder,
                    DocumentTypeId = documentTypeId,
                    VerticalMarketID = documentTypeMarketId
                });                

                verticalIds.Rows.Add(taxonomyID,
                                    taxonomyNameOverride,
                                    documentTypeId,
                                    !string.IsNullOrWhiteSpace(documentTypeLinkText),
                                    IsParent);
            }
            
            return taxonomyAssociationHierarchyModel;
        }


        /// <summary>
        /// Gets the taxonomy association documents client data.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <param name="verticalIds">The vertical ids.</param>
        /// <returns>TaxonomyAssociationDocumentsModel.</returns>
        private TaxonomyAssociationDocumentsModel GetTaxonomyAssociationDocumentsClientData(string clientName,
                                                                                 string siteName, DataTable verticalIds)
        {
            TaxonomyAssociationDocumentsModel taxonomyAssociationDocumentsModel = new TaxonomyAssociationDocumentsModel();
            taxonomyAssociationDocumentsModel.TaxonomyAssociationDocumentsData = new List<TaxonomyAssociationData>();
            taxonomyAssociationDocumentsModel.FootNotes = new List<HostedSiteFootNotes>();
            taxonomyAssociationDocumentsModel.DocumentTypeHeaders = new List<HostedDocumentTypeHeader>();

            int previousTaxonomyID = -1;            
            TaxonomyAssociationData taxonomyAssociationData = null;            
            bool taxonomyNameOverride = false;

            DataTable results = DataAccess.ExecuteDataTable(DBConnectionString.HostedConnectionString(clientName, DataAccess),
                         SPGetTaxonomyAssociationDocuments,
                         DataAccess.CreateParameter(DBCSiteName, DbType.String, siteName)
                         );

            foreach (DataRow datarow in results.Rows)
            {
                int taxonomyID = datarow.Field<int>("TaxonomyID");

                if (previousTaxonomyID != taxonomyID)
                {
                    taxonomyAssociationData = new TaxonomyAssociationData();                    
                    taxonomyAssociationDocumentsModel.TaxonomyAssociationDocumentsData.Add(taxonomyAssociationData);
                    

                    string FootNote = datarow.Field<string>("FootnoteText");
                    string taxonomyName = datarow.Field<string>("NameOverride");

                    if (!string.IsNullOrWhiteSpace(FootNote))
                    {

                        taxonomyAssociationDocumentsModel.FootNotes.Add(new HostedSiteFootNotes()
                        {
                            Text = FootNote,
                            Order = datarow.Field<int>("FootnoteOrder"),
                            TaxonomyID = taxonomyID,
                            TaxonomyName = taxonomyName
                        });
                    }

                    previousTaxonomyID = taxonomyID;

                    taxonomyAssociationData.DocumentTypes = new List<HostedDocumentType>();

                    

                    taxonomyAssociationData.TaxonomyID = taxonomyID;

                    taxonomyAssociationData.TaxonomyName = taxonomyName;

                    taxonomyAssociationData.TaxonomyAssociationID = datarow.Field<int>("TaxonomyAssociationID");

                    taxonomyAssociationData.TaxonomyDescriptionOverride = datarow.Field<string>("DescriptionOverride");

                    taxonomyAssociationData.TaxonomyCssClass = datarow.Field<string>("TaxonomyCssClass");

                    taxonomyNameOverride = !string.IsNullOrWhiteSpace(taxonomyName);

                }

                int documentTypeId = datarow.Field<int>("DocumentTypeId");
                                 

                string documentTypeHeaderText = datarow.Field<string>("DocumentTypeHeaderText");
                int documentTypeOrder = datarow.Field<int>("DocumentTypeOrder");

                string documentTypeLinkText = datarow.Field<string>("DocumentTypeLinkText");
                string documentTypeMarketId = datarow.Field<string>("DocumentTypeMarketId");

                if (taxonomyAssociationDocumentsModel.DocumentTypeHeaders.Find(f => f.HeaderName == documentTypeHeaderText) == null)
                {
                    taxonomyAssociationDocumentsModel.DocumentTypeHeaders.Add(new HostedDocumentTypeHeader()
                    {
                        HeaderName = documentTypeHeaderText,
                        Order = documentTypeOrder,
                        DocumentTypeId = documentTypeId,
                        VerticalMarketID = documentTypeMarketId

                    });
                }

                taxonomyAssociationData.DocumentTypes.Add(new HostedDocumentType()
                {
                    DocumentTypeLinkText = documentTypeLinkText,
                    DocumentTypeDescriptionOverride = datarow.Field<string>("DocumentTypeDescriptionOverride"),
                    DocumentTypeCssClass = datarow.Field<string>("DocumentTypeCssClass"),
                    DocumentTypeOrder = documentTypeOrder,
                    DocumentTypeId = documentTypeId,
                    VerticalMarketID = documentTypeMarketId
                });           

                verticalIds.Rows.Add(taxonomyID,
                                    taxonomyNameOverride,
                                    documentTypeId,
                                    !string.IsNullOrWhiteSpace(documentTypeLinkText),
                                    false
                                    );
                
            }

            return taxonomyAssociationDocumentsModel;
        }


        /// <summary>
        /// Gets the taxonomy specific documents client data.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <param name="externalID">The external identifier.</param>
        /// <param name="internalTAID">The internal taid.</param>
        /// <param name="verticalIds">The vertical ids.</param>
        /// <returns>TaxonomyAssociationData.</returns>
        private TaxonomyAssociationData GetTaxonomySpecificDocumentsClientData(string clientName, string siteName, string externalID, int? internalTAID, DataTable verticalIds)
        {
            int previousTaxonomyID = -1;
            int previousDocumentTypeID = -1;
            TaxonomyAssociationData taxonomyAssociationData = null;
            HostedDocumentType hostedDocumentType = null;
            bool taxonomyNameOverride = false;

            DataTable results = DataAccess.ExecuteDataTable(DBConnectionString.HostedConnectionString(clientName, DataAccess),
                         SPGetTaxonomySpecificDocumentFrame,
                         !string.IsNullOrWhiteSpace(externalID) ? DataAccess.CreateParameter("ExternalId", DbType.String, externalID) :
                                                                  DataAccess.CreateParameter("TAID", DbType.Int32, internalTAID),
                         DataAccess.CreateParameter(DBCSiteName, DbType.String, siteName)
                         );

            foreach (DataRow datarow in results.Rows)
            {
                int taxonomyID = datarow.Field<int>("TaxonomyID");

                if (previousTaxonomyID == -1)
                {
                    taxonomyAssociationData = new TaxonomyAssociationData();
                    taxonomyAssociationData.DocumentTypes = new List<HostedDocumentType>();

                    string taxonomyName = datarow.Field<string>("TaxonomyNameOverRide");

                    taxonomyAssociationData.TaxonomyID = taxonomyID;

                    taxonomyAssociationData.TaxonomyName = taxonomyName;

                    taxonomyAssociationData.TaxonomyAssociationID = datarow.Field<int>("TaxonomyAssociationID");

                    taxonomyAssociationData.TaxonomyDescriptionOverride = datarow.Field<string>("TaxonomyDesciptionOverRide");

                    taxonomyAssociationData.TaxonomyCssClass = datarow.Field<string>("TaxonomyCssClass");

                    taxonomyNameOverride = !string.IsNullOrWhiteSpace(taxonomyName);
                }

                previousTaxonomyID = taxonomyID;
                int documentTypeId = datarow.Field<int>("DocumentTypeId");

                if (previousDocumentTypeID != documentTypeId)
                {                    
                    previousDocumentTypeID = documentTypeId;

                    string documentTypeLinkText = datarow.Field<string>("DocumentTypeNameOverride");

                    hostedDocumentType = new HostedDocumentType()
                    {
                        DocumentTypeLinkText = documentTypeLinkText,
                        DocumentTypeDescriptionOverride = datarow.Field<string>("DocumentTypeDescriptionOverride"),
                        DocumentTypeOrder = datarow.Field<int>("DocumentTypeOrder"),
                        DocumentTypeId = documentTypeId,
                        DocumentTypeExternalID = new List<string>()
                    };
                    
                    taxonomyAssociationData.DocumentTypes.Add(hostedDocumentType);
                    
                    verticalIds.Rows.Add(taxonomyID,
                                        taxonomyNameOverride,
                                        documentTypeId,
                                        !string.IsNullOrWhiteSpace(documentTypeLinkText),
                                        false
                                        );
                }
                hostedDocumentType.DocumentTypeExternalID.Add(datarow.Field<string>("DocumentTypeExternalID")); 
            }

            return taxonomyAssociationData;
        }


        # endregion

        /// <summary>
        /// Gets the static resources from cache.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <returns>List&lt;HostedStaticResource&gt;.</returns>
        public List<HostedStaticResource> GetStaticResourcesFromCache(string clientName)
        {
            return GetClientsSiteDataFromCache(clientName).StaticResources;
        }

        /// <summary>
        /// Gets the site text from cache.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <returns>List&lt;HostedSiteText&gt;.</returns>
        public List<HostedSiteText> GetSiteTextFromCache(string clientName, string siteName)
        {
            ClientSiteData clientSiteData = GetClientsSiteDataFromCache(clientName);

            List<HostedSite> sites = clientSiteData.Sites;

            List<HostedSiteText> siteTexts = clientSiteData.SiteTexts;

            int siteIdToSearchOn;


            if (string.IsNullOrWhiteSpace(siteName))
            {
                siteIdToSearchOn = sites.Find(s => s.IsDefaultSite).SiteId;

            }
            else
            {
                siteIdToSearchOn = sites.Find(s => s.SiteName.ToLower() == siteName.ToLower()).SiteId;
            }

            return siteTexts.FindAll(st => st.SiteID == siteIdToSearchOn);

        }

        /// <summary>
        /// Gets the page text from cache.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <param name="pageId">The page identifier.</param>
        /// <returns>List&lt;HostedPageText&gt;.</returns>
        public List<HostedPageText> GetPageTextFromCache(string clientName, string siteName, int pageId)
        {
            ClientSiteData clientSiteData = GetClientsSiteDataFromCache(clientName);

            List<HostedSite> sites = clientSiteData.Sites;

            int siteIdToSearchOn;


            List<HostedPageText> pageTexts = clientSiteData.PageTexts;

            if (string.IsNullOrWhiteSpace(siteName))
            {
                siteIdToSearchOn = sites.Find(s => s.IsDefaultSite).SiteId;

            }
            else
            {
                siteIdToSearchOn = sites.Find(s => s.SiteName.ToLower() == siteName.ToLower()).SiteId;
            }

            return pageTexts.FindAll(pt => pt.SiteID == siteIdToSearchOn && pt.PageID == pageId);

        }

        /// <summary>
        /// Gets the sites from cache.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <returns>List&lt;HostedSite&gt;.</returns>
        public List<HostedSite> GetSitesFromCache(string clientName)
        {
            return GetClientsSiteDataFromCache(clientName).Sites;
        }

        /// <summary>
        /// Gets the current site information.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <returns>HostedSite.</returns>
        public HostedSite GetCurrentSiteInfo(string clientName, string siteName)
        {
            if (string.IsNullOrWhiteSpace(siteName))
                return GetClientsSiteDataFromCache(clientName).Sites.Find(s => s.IsDefaultSite);
            else
                return GetClientsSiteDataFromCache(clientName).Sites.Find(s => s.SiteName.ToLower() == siteName.ToLower());

        }

        /// <summary>
        /// Gets the name of the current page.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <returns>System.String.</returns>
        public string GetCurrentPageName(string clientName, string siteName)
        {
            HostedSite site = GetCurrentSiteInfo(clientName, siteName);

            return iSystemCommonFactory.GetClientsDataFromCache().TemplatePages.Find(tp => tp.TemplateID == site.TemplateId
                                                                                    && tp.PageID == site.DefaultPageId).PageName;
        }

        /// <summary>
        /// Gets the name of the page identifier for page.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <returns>System.Int32.</returns>
        public int GetPageIDForPageName(string pageName)
        {
            return iSystemCommonFactory.GetClientsDataFromCache().TemplatePages.Find(tp => tp.PageName == pageName).PageID;
        }

        /// <summary>
        /// Gets the name of the template.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <returns>System.String.</returns>
        public string GetTemplateName(string clientName, string siteName)
        {
            int templateId = GetCurrentSiteInfo(clientName, siteName).TemplateId;

            return iSystemCommonFactory.GetClientsDataFromCache().TemplatePages.Find(tp => tp.TemplateID == templateId).TemplateName;
        }


        /// <summary>
        /// Gets the taxonomy association links.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="site">The site.</param>
        /// <returns>List&lt;TaxonomyAssociationLinkModel&gt;.</returns>
        public List<TaxonomyAssociationLinkModel> GetTaxonomyAssociationLinks(string clientName, string site)
        {
            GetTaxonomyAssociationforBillingreport(clientName,DateTime.Now,DateTime.Now);

            DataTable clientTaxonomyIDs = new DataTable();


            clientTaxonomyIDs.Columns.Add("TaxonomyID", typeof(Int32));
            clientTaxonomyIDs.Columns.Add("IsNameOverrideProvided", typeof(bool));

            List<TaxonomyAssociationLinkModel> taxonomyassociationlinks = GetTaxonomyAssociationLinksClientData(clientName,
                                                                                    site, clientTaxonomyIDs);

            if (taxonomyassociationlinks.Count > 0)
            {
                return iHostedVerticalPageScenarios.GetTaxonomyAssociationLinksVerticalData(clientTaxonomyIDs, clientName, taxonomyassociationlinks);
            }
            else
            {
                return null;
            }


        }

        /// <summary>
        /// Gets the taxonomy association links for URL Generation.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <returns>List&lt;TaxonomyAssociationLinkModel&gt;.</returns>
        public List<UrlGenerationObjectModel> GetTaxonomyAssociationforUrlGeneration(string clientName, string marketID, string siteName)
        {
            DataTable clientTaxonomyIDs = new DataTable();
            clientTaxonomyIDs.Columns.Add("TaxonomyId", typeof(int));
            clientTaxonomyIDs.Columns.Add("IsNameOverrideProvided", typeof(bool));
            List<UrlGenerationObjectModel> urlgenerationdata = GetTaxonomyAssociationforUrlGenerationClientData(clientName, marketID, siteName,
                                                                                   clientTaxonomyIDs);
            if (urlgenerationdata.Count > 0)
            {
                return iHostedVerticalPageScenarios.GetUrlGenerationVerticalData(clientTaxonomyIDs, clientName, urlgenerationdata);
            }
            else
            {
                return null;
            }


        }

      /// <summary>
      /// Gets details of billing report
      /// </summary>
      /// <param name="clientName"></param>
      /// <param name="startDate"></param>
      /// <param name="endDate"></param>
      /// <returns></returns>
        public BillingReportModel GetTaxonomyAssociationforBillingreport(string clientName, DateTime startDate, DateTime endDate)
        {
            DataTable clientTaxonomyIDs = new DataTable();
            BillingReportModel billingData = new BillingReportModel();
            List<BillingReportActiveFundDetails> lstActiveFunds = new List<BillingReportActiveFundDetails>();
            List<BillingReportRemovedFundDetails> lstRemovedFunds = new List<BillingReportRemovedFundDetails>();

            clientTaxonomyIDs.Columns.Add("MarketID", typeof(string));
            clientTaxonomyIDs.Columns.Add("IsNameOverrideProvided", typeof(bool));

            DataSet results = new DataSet();
            results = DataAccess.ExecuteDataSet(DBConnectionString.HostedConnectionString(clientName, DataAccess),
                         SPGetBillingData,
                         DataAccess.CreateParameter(DCBStartDate, DbType.DateTime, startDate),
                         DataAccess.CreateParameter(DCBEndDate, DbType.DateTime, endDate)
                         );



            foreach (DataRow datarow in results.Tables[0].Rows)
            {
                string MarketID = datarow.Field<string>("MarketID");
                string nameOverride = datarow.Field<string>("NameOverride");
                string siteName = datarow.Field<string>("SiteName");
                if (!lstActiveFunds.Exists(p => p.MarketID == MarketID))
                {
                    clientTaxonomyIDs.Rows.Add(MarketID, !string.IsNullOrWhiteSpace(nameOverride));
                }
                BillingReportActiveFundDetails objBillingReport = new BillingReportActiveFundDetails();
                objBillingReport.MarketID = MarketID;
                objBillingReport.FundName = nameOverride;
                objBillingReport.SiteName = siteName;
                lstActiveFunds.Add(objBillingReport);

            }

            foreach (DataRow datarow in results.Tables[3].Rows)
            {
                BillingReportRemovedFundDetails removedFundDetail = new BillingReportRemovedFundDetails();
                removedFundDetail.MarketID = datarow.Field<string>("OldValue");
                removedFundDetail.removedDate = datarow.Field<DateTime>("UtcCUDDate");
                lstRemovedFunds.Add(removedFundDetail);
            }

            if (lstActiveFunds.Count > 0)
            {
                List<BillingReportActiveFundDetails> lstActive = iHostedVerticalPageScenarios.GetBillingReportVerticalData(clientTaxonomyIDs, clientName, lstActiveFunds);
                int summaryProspectusCount = lstActive.FindAll(t => t.IsSummaryProspectus).Count;

                billingData.LstBillingReportActiveFundDetails = lstActive;
                billingData.LstBillingReportRemovedFundDetails = lstRemovedFunds;
                billingData.ActiveFundCount = lstActive.Count;
                billingData.RemovedFundCount = lstRemovedFunds.Count;
                billingData.SummaryProspectusCount = summaryProspectusCount;
                billingData.NewFundCount = Convert.ToInt32(results.Tables[1].Rows[0][0].ToString());
                return billingData;
                
            }
            else
            {
                return null;
            }

            
        }

        /// <summary>
        /// Gets the taxonomy association hierarchy documents.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="site">The site.</param>
        /// <param name="externalID">The external identifier.</param>
        /// <param name="internalPTAID">The internal ptaid.</param>
        /// <returns>TaxonomyAssociationHierarchyModel.</returns>
        public TaxonomyAssociationHierarchyModel GetTaxonomyAssociationHierarchyDocuments(string clientName, string site,
                                            string externalID, int? internalPTAID)
        {

            DataTable clientTaxonomyIDsWithDocTypeIDs = new DataTable();

            clientTaxonomyIDsWithDocTypeIDs.Columns.Add("TaxonomyID", typeof(Int32));
            clientTaxonomyIDsWithDocTypeIDs.Columns.Add("IsNameOverrideProvided", typeof(bool));
            clientTaxonomyIDsWithDocTypeIDs.Columns.Add("DocumentTypeID", typeof(Int32));
            clientTaxonomyIDsWithDocTypeIDs.Columns.Add("IsDocumentTypeNameOverrideProvided", typeof(bool));
            clientTaxonomyIDsWithDocTypeIDs.Columns.Add("IsParent", typeof(bool));


            TaxonomyAssociationHierarchyModel taxonomyAssociationHierarchyModel = GetTaxonomyAssociationHierarchyDocumentsClientData(
                                                                         clientName, site,
                                                                        externalID, internalPTAID,
                                                                        clientTaxonomyIDsWithDocTypeIDs);

            if (taxonomyAssociationHierarchyModel.ParentTaxonomyAssociationData.Count > 0)
            {
                return iHostedVerticalPageScenarios.GetTaxonomyAssociationHierarchyDocumentsVerticalData(clientTaxonomyIDsWithDocTypeIDs, clientName,
                                                                                                            taxonomyAssociationHierarchyModel);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Gets the taxonomy association documents.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <returns>TaxonomyAssociationDocumentsModel.</returns>
        public TaxonomyAssociationDocumentsModel GetTaxonomyAssociationDocuments(string clientName, string siteName)
        {
            DataTable clientTaxonomyIDsWithDocTypeIDs = new DataTable();

            clientTaxonomyIDsWithDocTypeIDs.Columns.Add("TaxonomyID", typeof(Int32));
            clientTaxonomyIDsWithDocTypeIDs.Columns.Add("IsNameOverrideProvided", typeof(bool));
            clientTaxonomyIDsWithDocTypeIDs.Columns.Add("DocumentTypeID", typeof(Int32));
            clientTaxonomyIDsWithDocTypeIDs.Columns.Add("IsDocumentTypeNameOverrideProvided", typeof(bool));
            clientTaxonomyIDsWithDocTypeIDs.Columns.Add("IsParent", typeof(bool));

            TaxonomyAssociationDocumentsModel taxonomyAssociationDocumentsModel = GetTaxonomyAssociationDocumentsClientData(
                                                                        clientName, siteName,
                                                                        clientTaxonomyIDsWithDocTypeIDs);

            if (taxonomyAssociationDocumentsModel.TaxonomyAssociationDocumentsData.Count > 0)
            {
                return iHostedVerticalPageScenarios.GetTaxonomyAssociationDocumentsVerticalData(clientTaxonomyIDsWithDocTypeIDs, clientName,
                                                                                                            taxonomyAssociationDocumentsModel);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Gets the taxonomy specific documents.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <param name="externalID">The external identifier.</param>
        /// <param name="internalTAID">The internal taid.</param>
        /// <returns>TaxonomyAssociationData.</returns>
        public TaxonomyAssociationData GetTaxonomySpecificDocuments(string clientName, string siteName, string externalID,
                                                                    int? internalTAID)
        {
            DataTable clientTaxonomyIDsWithDocTypeIDs = new DataTable();

            clientTaxonomyIDsWithDocTypeIDs.Columns.Add("TaxonomyID", typeof(Int32));
            clientTaxonomyIDsWithDocTypeIDs.Columns.Add("IsNameOverrideProvided", typeof(bool));
            clientTaxonomyIDsWithDocTypeIDs.Columns.Add("DocumentTypeID", typeof(Int32));
            clientTaxonomyIDsWithDocTypeIDs.Columns.Add("IsDocumentTypeNameOverrideProvided", typeof(bool));
            clientTaxonomyIDsWithDocTypeIDs.Columns.Add("IsParent", typeof(bool));

            TaxonomyAssociationData taxonomyAssociationData = GetTaxonomySpecificDocumentsClientData(
                                                                         clientName, siteName,
                                                                        externalID, internalTAID,
                                                                        clientTaxonomyIDsWithDocTypeIDs);

            if (taxonomyAssociationData != null && taxonomyAssociationData.DocumentTypes != null && taxonomyAssociationData.DocumentTypes.Count > 0)
            {
                taxonomyAssociationData = iHostedVerticalPageScenarios.GetTaxonomySpecificDocumentsVerticalData(clientTaxonomyIDsWithDocTypeIDs, clientName,
                                                                                                            taxonomyAssociationData);
            }

            return taxonomyAssociationData;
        }


        /// <summary>
        /// Gets the site navigation from cache.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <param name="siteNavigationKey">The site navigation key.</param>
        /// <param name="isProofing">if set to <c>true</c> [is proofing].</param>
        /// <param name="pageID">The page identifier.</param>
        /// <returns>Tuple&lt;System.String, System.String&gt;.</returns>
        public Tuple<string, string> GetSiteNavigationFromCache(string clientName, string siteName, string siteNavigationKey, bool isProofing, int? pageID)
        {
            bool isCurrentProductionVersion = true;
            if (isProofing)
            {
                isCurrentProductionVersion = false;
            }

            ClientSiteData clientSiteData = GetClientsSiteDataFromCache(clientName);
            List<HostedSiteNavigation> siteNavigationDetails = clientSiteData.SiteNavigations;
            int siteIdToSearchOn;
            int templateID = 0;

            if (string.IsNullOrWhiteSpace(siteName))
            {
                siteIdToSearchOn = clientSiteData.Sites.Find(s => s.IsDefaultSite).SiteId;
                templateID = clientSiteData.Sites.Find(s => s.IsDefaultSite).TemplateId;
            }
            else
            {
                siteIdToSearchOn = clientSiteData.Sites.Find(s => s.SiteName.ToLower() == siteName.ToLower()).SiteId;
                templateID = clientSiteData.Sites.Find(s => s.SiteName.ToLower() == siteName.ToLower()).TemplateId;
            }

            string NavigationXML = string.Empty;
            HostedSiteNavigation hostedSiteNavigation = siteNavigationDetails.Find(x => x.SiteID == siteIdToSearchOn && x.NavigationKey == siteNavigationKey && x.IsCurrentProductionVersion == isCurrentProductionVersion);
            if (hostedSiteNavigation != null)
            {
                NavigationXML = hostedSiteNavigation.NavigationXml;

            }

            ClientDataFromSystem clientDataFromSystem = iSystemCommonFactory.GetClientsDataFromCache();
            if (string.IsNullOrWhiteSpace(NavigationXML))
            {
                NavigationXML = clientDataFromSystem.HostedTemplateNavigations.Find(x => x.TemplateID == templateID && x.NavigationKey == siteNavigationKey).DefaultNavigationXml;
            }

            string XslTransform = string.Empty;
            XslTransform = clientDataFromSystem.HostedTemplateNavigations.Find(x => x.TemplateID == templateID && x.NavigationKey == siteNavigationKey).XslTransform;
            return new Tuple<string, string>(NavigationXML, XslTransform);
        }


        /// <summary>
        /// Gets the page navigation from cache.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <param name="pageNavigationKey">The page navigation key.</param>
        /// <param name="isProofing">if set to <c>true</c> [is proofing].</param>
        /// <param name="pageID">The page identifier.</param>
        /// <returns>Tuple&lt;System.String, System.String&gt;.</returns>
        public Tuple<string, string> GetPageNavigationFromCache(string clientName, string siteName, string pageNavigationKey, bool isProofing, int pageID)
        {
            bool isCurrentProductionVersion = true;
            if (isProofing)
            {
                isCurrentProductionVersion = false;
            }

            ClientSiteData clientSiteData = GetClientsSiteDataFromCache(clientName);
            List<HostedPageNavigation> pageNavigationDetails = clientSiteData.PageNavigations;
            int siteIdToSearchOn;
            int templateID = 0;

            if (string.IsNullOrWhiteSpace(siteName))
            {
                siteIdToSearchOn = clientSiteData.Sites.Find(s => s.IsDefaultSite).SiteId;
                templateID = clientSiteData.Sites.Find(s => s.IsDefaultSite).TemplateId;
            }
            else
            {
                siteIdToSearchOn = clientSiteData.Sites.Find(s => s.SiteName.ToLower() == siteName.ToLower()).SiteId;
                templateID = clientSiteData.Sites.Find(s => s.SiteName.ToLower() == siteName.ToLower()).TemplateId;
            }

            string NavigationXML = string.Empty;
            HostedPageNavigation hostedPageNavigation = pageNavigationDetails.Find(x => x.SiteID == siteIdToSearchOn && x.PageID == pageID && x.NavigationKey == pageNavigationKey && x.IsCurrentProductionVersion == isCurrentProductionVersion);
            if (hostedPageNavigation != null)
            {
                NavigationXML = hostedPageNavigation.NavigationXml;

            }

            ClientDataFromSystem clientDataFromSystem = iSystemCommonFactory.GetClientsDataFromCache();
            if (string.IsNullOrWhiteSpace(NavigationXML))
            {
                NavigationXML = clientDataFromSystem.HostedTemplatePageNavigations.Find(x => x.TemplateID == templateID && x.PageID == pageID && x.NavigationKey == pageNavigationKey).DefaultNavigationXml;
            }

            string XslTransform = string.Empty;
            XslTransform = clientDataFromSystem.HostedTemplatePageNavigations.Find(x => x.TemplateID == templateID && x.PageID == pageID && x.NavigationKey == pageNavigationKey).XslTransform;
            return new Tuple<string, string>(NavigationXML, XslTransform);
        }


        /// <summary>
        /// Gets the XBRL by taxonomy association identifier or external identifier.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="taxonomyAssociationId">The taxonomy association identifier.</param>
        /// <param name="externalId">The external identifier.</param>
        /// <param name="pid">if set to <c>true</c> [pid].</param>
        /// <returns>List&lt;XBRLObjectModel&gt;.</returns>
        public List<XBRLObjectModel> GetXBRLByTaxonomyAssociationIDOrExternalID(string clientName, int? taxonomyAssociationId, string externalId, bool pid)
        {

            List<XBRLObjectModel> returnData = null;
            if (pid)
            {
                int taxonomyID = 0;
                if (int.TryParse(externalId, out taxonomyID))
                {
                    returnData = iHostedVerticalPageScenarios.GetXBRLDetailsForTaxonomyID(clientName, taxonomyID, null);
                }
            }
            else
            {
                DataTable taxonomyIDDT = DataAccess.ExecuteDataTable(DBConnectionString.HostedConnectionString(clientName, DataAccess),
                             SPGetTaxonomyIDByTaxonomyAssociationIDOrExternalID,
                             DataAccess.CreateParameter(DBCTAID, DbType.Int32, taxonomyAssociationId),
                             DataAccess.CreateParameter(DBCExternalID, DbType.String, externalId),
                             DataAccess.CreateParameter(DBCLevel, DbType.Int32, 1)
                             );

                if (taxonomyIDDT.Rows.Count > 0)
                {
                    DataRow datarow = taxonomyIDDT.Rows[0];
                    int taxonomyID = datarow.Field<int>("TaxonomyID");
                    string taxonomyName = datarow.Field<string>("NameOverride");

                    returnData = iHostedVerticalPageScenarios.GetXBRLDetailsForTaxonomyID(clientName, taxonomyID, taxonomyName);
                }
            }
            return returnData;
        }

        /// <summary>
        /// Gets the site feature mode from cache.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <param name="siteFeatureKey">The site feature key.</param>
        /// <returns>System.Int32.</returns>
        public int GetSiteFeatureModeFromCache(string clientName, string siteName, string siteFeatureKey)
        {
            int featureMode = 0;

            ClientSiteData clientSiteData = GetClientsSiteDataFromCache(clientName);
            List<HostedSiteFeature> siteFeatures = clientSiteData.SiteFeatures;
            int siteIdToSearchOn;
            //int templateID = 0;

            if (string.IsNullOrWhiteSpace(siteName))
            {
                siteIdToSearchOn = clientSiteData.Sites.Find(s => s.IsDefaultSite).SiteId;
                //templateID = clientSiteData.Sites.Find(s => s.IsDefaultSite).TemplateId;
            }
            else
            {
                siteIdToSearchOn = clientSiteData.Sites.Find(s => s.SiteName.ToLower() == siteName.ToLower()).SiteId;
                //templateID = clientSiteData.Sites.Find(s => s.SiteName.ToLower() == siteName.ToLower()).TemplateId;
            }

            HostedSiteFeature hostedSiteFeature = siteFeatures.Find(x => x.SiteID == siteIdToSearchOn && x.FeatureKey == siteFeatureKey);
            if (hostedSiteFeature != null)
            {
                featureMode = hostedSiteFeature.FeatureMode;
            }

            return featureMode;
        }

        /// <summary>
        /// Gets the page feature mode from cache.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <param name="pageID">The page identifier.</param>
        /// <param name="pageFeatureKey">The page feature key.</param>
        /// <returns>System.Int32.</returns>
        public int GetPageFeatureModeFromCache(string clientName, string siteName, int pageID, string pageFeatureKey)
        {
            int featureMode = 0;

            ClientSiteData clientSiteData = GetClientsSiteDataFromCache(clientName);
            List<HostedPageFeature> pageFeatures = clientSiteData.PageFeatures;
            int siteIdToSearchOn;
            //int templateID = 0;

            if (string.IsNullOrWhiteSpace(siteName))
            {
                siteIdToSearchOn = clientSiteData.Sites.Find(s => s.IsDefaultSite).SiteId;
                //templateID = clientSiteData.Sites.Find(s => s.IsDefaultSite).TemplateId;
            }
            else
            {
                siteIdToSearchOn = clientSiteData.Sites.Find(s => s.SiteName.ToLower() == siteName.ToLower()).SiteId;
                //templateID = clientSiteData.Sites.Find(s => s.SiteName.ToLower() == siteName.ToLower()).TemplateId;
            }

            HostedPageFeature hostedPageFeature = pageFeatures.Find(x => x.SiteID == siteIdToSearchOn && x.PageID == pageID && x.FeatureKey == pageFeatureKey);
            if (hostedPageFeature != null)
            {
                featureMode = hostedPageFeature.FeatureMode;
            }

            return featureMode;
        }

    }
}
